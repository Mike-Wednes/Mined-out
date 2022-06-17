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
            Redactor red = new Redactor();
            currentScene = SceneType.StartMenu;
            _interface.PrintGrafic("Opening", new Cell(30, 5));
            while (ListenKeys()) { }

        }

        private bool ListenKeys()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            var sceneType = sceneHandler.Handle(currentScene, key.Key);
            if (sceneType != null)
            {
                currentScene = (SceneType)sceneType;
            }
            return true;
        }
    }
}
