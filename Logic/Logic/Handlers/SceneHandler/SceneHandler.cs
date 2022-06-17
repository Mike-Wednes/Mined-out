using System;
using Core;
using UI;
using System.Runtime.Serialization.Json;
using GraficRedactor;
using System.Diagnostics;

namespace Logic
{
    internal class SceneHandler
    {
        private IInterface _interface;

        private Dictionary<SceneType, int> cursorLevels;

        private bool isSceneDisplayed;

        private Scene[] scenes;

        public SceneHandler(IInterface UI)
        {
            _interface = UI;
            cursorLevels = new Dictionary<SceneType, int>();
            SetCursorLevels();
            isSceneDisplayed = false;
            scenes = GetAllScenes();
        }

        private void SetCursorLevels()
        {
            var sceneTypes = typeof(SceneType).GetEnumValues();
            foreach (SceneType type in sceneTypes)
            {
                cursorLevels.Add(type, 1);
            }
        }

        public SceneType? Handle(SceneType sceneType, ConsoleKey? key)
        {
            return (SceneType?)GetType().GetMethod(sceneType.ToString())?.Invoke(this, new object?[] { key });
        }

        public SceneType? GraficRedactor(ConsoleKey? key)
        {
            FirstSceneDisplay(SceneType.GraficRedactor);
            Redactor redactor = new Redactor();
            redactor.Start();
            isSceneDisplayed = false;
            return Handle(SceneType.StartMenu, null);
        }

        public SceneType? StartMenu(ConsoleKey? key)
        {
            bool wasDisplayed = isSceneDisplayed;
            SceneDefault(SceneType.StartMenu, key);
            if (wasDisplayed)
            {
                return StartMenuKeyListened(key);
            }
            else
            {
                return SceneType.StartMenu;
            }
        }

        public SceneType? Settings(ConsoleKey key)
        {
            SceneDefault(SceneType.Settings, key);
            return SettingsKeyListened(key);
        }

        private void SceneDefault(SceneType type, ConsoleKey? key)
        {
            var scene = scenes.Where(g => g.Type == type).First();
            FirstSceneDisplay(scene.Type);
        }

        private void FirstSceneDisplay(SceneType type)
        {
            if (!isSceneDisplayed)
            {
                _interface.Clear();
                isSceneDisplayed = true;
                DisplayElements(type, ElementType.Button, ElementTag.Unselected);

                    DisplayElement(type, "Button" + cursorLevels[type] + "Selected");

            }
        }

        private SceneType? SettingsKeyListened(ConsoleKey? key)
        {
            var scene = GetScene(SceneType.Settings);
            if (scene != null)
            {
                ChangeCursorLevel(scene.Type, 1, 2, key);
                DisplaySliderFields(SceneType.Settings);
                if (key == ConsoleKey.Enter)
                {
                    ChangingSliderPos(IntToSettingsProperty(cursorLevels[SceneType.Settings]));
                }
                if (key == ConsoleKey.Escape)
                {
                    isSceneDisplayed = false;
                    return Handle(SceneType.StartMenu, null);
                }
            }       
            return SceneType.Settings;
        }

        private SceneType? StartMenuKeyListened(ConsoleKey? key)
        {
            var scene = GetScene(SceneType.StartMenu);
            if(scene != null)
            {
                ChangeCursorLevel(scene.Type, 1, 3, key);
                if (key == ConsoleKey.Enter)
                {
                    isSceneDisplayed = false;
                    return Handle((SceneType)cursorLevels[scene.Type], null);
                }
                if (key == ConsoleKey.Escape)
                {
                     Process.GetCurrentProcess().Kill();
                }
            }
            return SceneType.StartMenu;
        }

        private void ChangeCursorLevel(SceneType sceneType, int min, int max, ConsoleKey? key)
        {
            switch (key)
            {
                case ConsoleKey.DownArrow:
                    if (cursorLevels[sceneType] < max)
                    {
                        ChangeAndDisplayButton(sceneType, 1);
                    }
                    break;
                case ConsoleKey.UpArrow:
                    if (cursorLevels[sceneType] > min)
                    {
                        ChangeAndDisplayButton(sceneType, -1);
                    }
                    break;
            }
        }

        private void ChangeAndDisplayButton(SceneType type, int change)
        {
            DisplayButtons(type, ElementTag.Unselected);
            cursorLevels[type] += change;
            DisplayButtons(type, ElementTag.Selected);
        }

        private void DisplayButtons(SceneType type, ElementTag tag)
        {
            var scene = scenes.Where(g => g.Type == type).First();
            SceneElement button;
            if (cursorLevels[type] == 0)
            {
                var sceneElements = scene.Elements.Where(g => g.GraficName.Contains("Unselected"));
                foreach (var sceneElement in sceneElements)
                {
                    _interface.PrintGrafic(sceneElement.GraficName, adaptSceneOffset(scene, sceneElement));
                }
            }
            else
            {
                button = scene.Elements.Where(g => g.GraficName.Contains("Button" + cursorLevels[type] + tag.ToString())).First();
                DisplayElement(type, button.GraficName);
            }
        }
        
        private void DisplayElement(SceneType type, string elementName)
        {
            var scene = GetScene(type);
            if (scene != null)
            {
                var element = scene.Elements.Where(g => g.GraficName.Contains(elementName)).FirstOrDefault();
                if (element != null)
                {
                    _interface.PrintGrafic(element.GraficName, adaptSceneOffset(scene, element));
                }
            }
        }

        private void DisplayElements(SceneType sceneType, ElementType elementType, ElementTag tag)
        {
            var scene = GetScene(sceneType);
            if (scene != null)
            {
                var elements = scene.Elements.Where(g => g.GraficName.Contains(elementType.ToString()) && g.GraficName.Contains(tag.ToString()));
                foreach (var element in elements)
                {
                    DisplayElement(sceneType, element.GraficName);
                }
            }
        }

        private Cell adaptSceneOffset(Scene scene, SceneElement element)
        {
            return new Cell(scene.Offset.X + element.Offset.X, scene.Offset.Y + element.Offset.Y);
        }

        internal Scene? GetScene(SceneType type)
        {
            var scene = scenes.Where(g => g.Type == type).FirstOrDefault();
            return scene;
        }

        private Scene[] GetAllScenes()
        {
            DirectoryInfo dir = new DirectoryInfo(@"../../../../../Grafics/Scenes");
            var files = dir.GetFiles();
            List<Scene> list = new List<Scene>();
            foreach (var file in files)
            {
                var scene = SceneDeserialize(file.FullName);
                if (scene != null)
                {
                    list.Add(scene);
                }
            }
            return list.ToArray();
        }

        private Scene? SceneDeserialize(string path)
        {
            var jsonformatter = new DataContractJsonSerializer(typeof(Scene));

            try
            {
                using (var file = new FileStream(path, FileMode.Open))
                {
                    var scene = jsonformatter.ReadObject(file) as Scene;
                    return scene;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public SceneType? Game(ConsoleKey? key)
        {
            FirstSceneDisplay(SceneType.Game);
            GameHandler gameHandler = new GameHandler(_interface);
            gameHandler.Start();
            isSceneDisplayed = false;
            return Handle(SceneType.StartMenu, null);
        }

        private string IntToSettingsProperty(int i)
        {
            switch (i)
            {
                case 1:
                    return "FieldSize";
                case 2:
                    return "DifficultyLevel";
                default:
                    return "none";
            }
        }

        private void ChangingSliderPos(string propName)
        {
            DisplaySliderField(SceneType.Settings, propName, ElementTag.Selected);
            ConsoleKey key = Console.ReadKey(true).Key;
            if (key != ConsoleKey.Enter && key != ConsoleKey.Escape)
            {
                ChangeSliderPos(key, SceneType.Settings, propName);
                ChangingSliderPos(propName);
            }
            else
            {
                DisplaySliderField(SceneType.Settings, propName, ElementTag.Unselected);
            }
        }

        private void ChangeSliderPos(ConsoleKey key, SceneType type, string propertyName)
        {
            var scene = GetScene(type);
            if (scene != null)
            {
                var slider = scene.Elements.Where(g => g.GraficName.Contains(propertyName + "Slider")).First();
                var settings = new Settings();
                var propValue = settings.GetType().GetProperty(propertyName)?.GetValue(settings);
                if (propValue != null)
                {
                    int value = ChangeValueByArrows((int)propValue, 1, 3, key);
                    DisplaySlider(adaptSceneOffset(scene, slider), value, ElementTag.Selected);

                    settings.GetType().GetProperty(propertyName)?.SetValue(settings, value);
                    settings.Record();
                }
            }
        }

        private void ClearSlider(Cell cell)
        {
            _interface.PrintGrafic("ClearedSlider", cell);
        }

        private void DisplaySliderField(SceneType type, string propertyName, ElementTag tag)
        {
            var scene = GetScene(type);
            if (scene != null)
            {
                var slider = scene.Elements.Where(g => g.GraficName.Contains(propertyName + "Slider")).First();
                var settings = new Settings();
                var propValue = settings.GetType().GetProperty(propertyName)?.GetValue(settings);
                _interface.PrintGrafic(slider.GraficName, adaptSceneOffset(scene, slider));
                if (propValue != null)
                {
                    DisplaySlider(adaptSceneOffset(scene, slider), (int)propValue, tag);
                }
            }
        }

        private int ChangeValueByArrows(int value, int min, int max, ConsoleKey key)
        {
            if (key == ConsoleKey.RightArrow && value < max)
            {
                return ++value;
            }
            if (key == ConsoleKey.LeftArrow && value > min)
            {
                return --value;
            }
            return value;
        }

        private void DisplaySliderFields(SceneType type)
        {
            var propNames = typeof(Settings).GetProperties();
            foreach (var prop in propNames)
            {
                DisplaySliderField(type, prop.Name, ElementTag.Unselected);
            }
        }

        private void DisplaySlider(Cell fieldDefaultPos, int propValue, ElementTag tag)
        {
            fieldDefaultPos++;
            ClearSlider(fieldDefaultPos);
            fieldDefaultPos.X += --propValue * 3;
            _interface.PrintGrafic("Slider" + tag.ToString(), fieldDefaultPos);
        }
    }
}
