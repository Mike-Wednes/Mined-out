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

        private bool isSceneDisplayed;

        private Scene[] scenes;

        public SceneHandler(IInterface UI)
        {
            _interface = UI;
            cursorLevel = 0;
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

        public SceneType StartMenu(ConsoleKey? key)
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

        private SceneType StartMenuKeyListened(ConsoleKey? key)
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
                    Handle((SceneType)cursorLevel, null);
                    return (SceneType)cursorLevel;
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
            var scene = scenes.Where(g => g.Type == SceneType.StartMenu).First();
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
    }
}
