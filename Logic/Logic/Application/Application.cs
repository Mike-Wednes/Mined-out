using Core;
using UI;
using System;
using System.Threading;
using GraficRedactor;


namespace Logic
{
    internal class Application
    {
        private readonly IInterface _interface;

        private SceneHandler sceneHandler;

        private SceneType currentScene;

        public Application(IInterface ui)
        {
            _interface = ui;
            sceneHandler = new SceneHandler(ui);
        }

        public void Run()
        {
            Console.SetWindowSize(100, 25);
            //field = new Field(_interface.GetFieldSize("x"), _interface.GetFieldSize("y"));
            Redactor red = new Redactor();
            currentScene = SceneType.StartMenu;
            Console.Clear();
            //_interface.DisplayField(field);
            while (ListenKeys()) { }
            //red.Start();
            //List<SceneElement> elements = new List<SceneElement>
            //{
            //    new SceneElement("PlayButtonSelected", new Cell(0, 0)),
            //    new SceneElement("PlayButtonUnselected", new Cell(0, 0)),
            //    new SceneElement("SettingsButtonUnselected", new Cell(0, 7)),
            //    new SceneElement("SettingsButtonSselected", new Cell(0, 7)),
            //    new SceneElement("RedactorButtonSelected", new Cell(0, 15)),
            //    new SceneElement("RedactorButtonUnselected", new Cell(0, 15)),
            //};

            //Scene StartScene = new Scene(SceneType.StartMenu, new Cell(40, 6), elements);

            //var jsonformatter = new DataContractJsonSerializer(typeof(Scene));
            //using (var file = new FileStream(@"../../../../../Grafics/Scenes/StartMenu.json", FileMode.OpenOrCreate))
            //{
            //    jsonformatter.WriteObject(file, StartScene);
            //}
        }

        private bool ListenKeys()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            //if (typeof(Core.KeysGroups.MoveKeys).DoesEnumContainKey(key))
            //{
            //    return Move(key);
            //}
            //else if (typeof(Core.KeysGroups.MarkKeys).DoesEnumContainKey(key))
            //{
            //    Mark(key);
            //}
            var sceneType = sceneHandler.Handle(currentScene, key.Key);
            if (sceneType != null)
            {
                currentScene = (SceneType)sceneType;
            }
            /*case ConsoleKey.D1:
                    ChangeViewMode(CellType.Mine, CellView.Visible);
            break;*/
            return true;
        }
    }
}
