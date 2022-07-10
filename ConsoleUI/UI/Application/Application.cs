using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    internal class Application
    {
        ConsoleDisplayer displayer;

        private Dictionary<ModeType, Menu> MenuStorage { get; } = new Dictionary<ModeType, Menu>()
        {
            {ModeType.Start, new StartMenu()},
            {ModeType.Game, new GameMenu()},
            {ModeType.Settings, new SettingsMenu()},
            {ModeType.Redactor, new RedactorMenu()},
        };

        public Application()
        {
            Console.SetWindowSize(100, 31);
            displayer = new ConsoleDisplayer();
        }

        public void Run()
        {
            displayer.DisplayGraficCollection("Opening", new Core.Cell(33, 8));
            ModeType? currentMode = ModeType.Start;
            do
            {
                currentMode = MenuStorage[(ModeType)currentMode].Handle();
            }
            while (currentMode != null);
        }
    }
}
