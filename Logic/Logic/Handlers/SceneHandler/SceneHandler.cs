using System;
using Core;
using UI;
using System.Runtime.Serialization.Json;
using GraficRedactor;

namespace Logic
{
    internal class SceneHandler
    {
        private IInterface _interface;

        private int cursorLevel;

        private int gorizontalCursoLevel;

        private bool isSceneDisplayed;

        private Scene[] scenes;

        public SceneHandler(IInterface UI)
        {
            _interface = UI;
            cursorLevel = 0;
            gorizontalCursoLevel = 1;
            isSceneDisplayed = false;
            scenes = GetAllScenes();
        }

        public SceneType? Handle(SceneType sceneType, ConsoleKey? key)
        {
            return (SceneType?)GetType().GetMethod(sceneType.ToString())?.Invoke(this, new object?[] { key });
        }

        public SceneType? GraficRedactor(ConsoleKey? key)
        {
            FirstSceneDisplay();
            Redactor redactor = new Redactor();
            redactor.Start();
            isSceneDisplayed = false;
            return Handle(SceneType.StartMenu, null);
        }

        public SceneType? StartMenu(ConsoleKey? key)
        {
            FirstSceneDisplay();
            var scene = scenes.Where(g => g.Type == SceneType.StartMenu).First();
            DisplayButtons(SceneType.StartMenu, ButtonTag.Unselected);
            return StartMenuKeyListened(key);

        }

        private void FirstSceneDisplay()
        {
            if (!isSceneDisplayed)
            {
                cursorLevel = 0;
                _interface.Clear();
            }
            isSceneDisplayed = true;
        }

        private SceneType? StartMenuKeyListened(ConsoleKey? key)
        {
            var scene = GetScene(SceneType.StartMenu);
            switch (key)
            {
                case ConsoleKey.DownArrow:
                    if (cursorLevel < 3)
                    {
                        ChangeAndDisplayButton(SceneType.StartMenu, 1);
                    }
                    return scene.Type;
                case ConsoleKey.UpArrow:
                    if (cursorLevel > 0)
                    {
                        ChangeAndDisplayButton(SceneType.StartMenu, -1);
                    }
                    return scene.Type;
                case ConsoleKey.Enter:
                    isSceneDisplayed = false;
                    return Handle((SceneType)cursorLevel, null);
                default:
                    ChangeAndDisplayButton(SceneType.StartMenu, 0);
                    return SceneType.StartMenu;
            }
        }

        private void ChangeAndDisplayButton(SceneType type, int change)
        {
            DisplayButtons(type, ButtonTag.Unselected);
            cursorLevel += change;
            DisplayButtons(type, ButtonTag.Selected);
        }

        private void DisplayButtons(SceneType type, ButtonTag tag)
        {
            var scene = scenes.Where(g => g.Type == type).First();
            SceneElement button;
            if (cursorLevel == 0)
            {
                var sceneElements = scene.Elements.Where(g => g.GraficName.Contains("Unselected"));
                foreach (var sceneElement in sceneElements)
                {
                    _interface.PrintGrafic(sceneElement.GraficName, adaptSceneOffset(scene, sceneElement));
                }
            }
            else
            {
                button = scene.Elements.Where(g => g.GraficName.Contains("Button" + cursorLevel + tag.ToString())).First();
                PrintElement(type, button.GraficName);
            }
        }
        
        private void PrintElement(SceneType type, string elementName)
        {
            var scene = scenes.Where(g => g.Type == type).First();
            var element = scene.Elements.Where(g => g.GraficName == elementName).First();
            _interface.PrintGrafic(elementName, adaptSceneOffset(scene, element));
        }

        //private void PrintElementByTag(SceneType type, string tag)
        //{
        //    var scene = scenes.Where(g => g.Type == SceneType.StartMenu).First();
        //}

        private Cell adaptSceneOffset(Scene scene, SceneElement element)
        {
            return new Cell(scene.Offset.X + element.Offset.X, scene.Offset.Y + element.Offset.Y);
        }

        internal Scene GetScene(SceneType type)
        {
            var scene = scenes.Where(g => g.Type == type).First();
            return scene;
        }

        private Scene[] GetAllScenes()
        {
            DirectoryInfo dir = new DirectoryInfo(@"../../../../../Grafics/Scenes");
            var files = dir.GetFiles();
            List<Scene> list = new List<Scene>();
            foreach (var file in files)
            {
                var scene = Deserialize(file.FullName);
                if (scene != null)
                {
                    list.Add(scene);
                }
            }
            return list.ToArray();
        }

        private Scene? Deserialize(string path)
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
            FirstSceneDisplay();
            GameHandler gameHandler = new GameHandler(_interface);
            gameHandler.Start();
            isSceneDisplayed = false;
            return Handle(SceneType.StartMenu, null);
        }

        public SceneType? Settings(ConsoleKey key)
        {
            FirstSceneDisplay();
            var scene = scenes.Where(g => g.Type == SceneType.Settings).First();
            DisplayButtons(SceneType.Settings, ButtonTag.Unselected);
            return SettingsKeyListened(key);
        }

        private SceneType? SettingsKeyListened(ConsoleKey? key)
        {
            var scene = GetScene(SceneType.Settings);
            switch (key)
            {
                case ConsoleKey.DownArrow:
                    if (cursorLevel < 2)
                    {
                        ChangeAndDisplayButton(SceneType.Settings, 1);
                    }
                    return scene.Type;
                case ConsoleKey.UpArrow:
                    if (cursorLevel > 0)
                    {
                        ChangeAndDisplayButton(SceneType.Settings, -1);
                    }
                    return scene.Type;
                case ConsoleKey.Enter:
                    ChangingSliderPos(IntToSettingsProperty(cursorLevel));
                    return scene.Type;
                case ConsoleKey.Escape:
                    isSceneDisplayed = false;
                    return Handle(SceneType.StartMenu, null);
                default:
                    ChangeAndDisplayButton(SceneType.Settings, 0);
                    DisplaySliderFields(SceneType.Settings);
                    return SceneType.Settings;
            }
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
            DisplaySliderField(SceneType.Settings, propName, true);
            ConsoleKey key = Console.ReadKey(true).Key;
            if (key != ConsoleKey.Enter && key != ConsoleKey.Escape)
            {
                ChangeSliderPos(key, SceneType.Settings, propName);
                ChangingSliderPos(propName);
            }
            else
            {
                DisplaySliderField(SceneType.Settings, propName);
            }
        }

        private void ChangeSliderPos(ConsoleKey key, SceneType type, string propertyName)
        {
            var scene = GetScene(type);
            var slider = scene.Elements.Where(g => g.GraficName.Contains(propertyName + "Slider")).First();
            var settings = new Settings();
            var propValue = settings.GetType().GetProperty(propertyName)?.GetValue(settings);
            if(propValue != null)
            {
                Cell sliderDefaultPos = new Cell(adaptSceneOffset(scene, slider));
                sliderDefaultPos.X++;
                sliderDefaultPos.Y++;
                _interface.PrintGrafic("ClearedSlider", sliderDefaultPos);
                int value = ChangeValueByArrows((int)propValue, 1, 3, key);
                DisplaySlider(sliderDefaultPos, value, true);
                settings.GetType().GetProperty(propertyName)?.SetValue(settings, value);
                settings.Record();
            }
        }

        private void DisplaySliderField(SceneType type, string propertyName, bool selected = false)
        {
            var scene = GetScene(type);
            var slider = scene.Elements.Where(g => g.GraficName.Contains(propertyName + "Slider")).First();
            var settings = new Settings();
            var propValue = settings.GetType().GetProperty(propertyName)?.GetValue(settings);
            _interface.PrintGrafic(slider.GraficName, adaptSceneOffset(scene, slider));
            Cell sliderDefaultPos = new Cell(adaptSceneOffset(scene, slider));
            sliderDefaultPos.X++;
            sliderDefaultPos.Y++;
            if (propValue != null)
            {
                DisplaySlider(sliderDefaultPos, (int)propValue, selected);
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
                DisplaySliderField(type, prop.Name);
            }
        }

        private void DisplaySlider(Cell defaultPos, int propValue, bool selected)
        {
            string name = selected
                ? "SliderSelected"
                : "SliderUnselected";
            defaultPos.X += --propValue * 3;
            _interface.PrintGrafic(name, defaultPos);
        }
    }
}
