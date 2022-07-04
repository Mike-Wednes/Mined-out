using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    internal class Application
    {
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
        }

        public void Run()
        {
            ModeType? currentMode = ModeType.Start;
            do
            {
                currentMode = MenuStorage[(ModeType)currentMode].Handle();
            }
            while (currentMode != null);
        }
    }
}
