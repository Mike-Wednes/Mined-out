using Core;

namespace ConsoleUI
{
    internal class ConsoleSettings
    {
        private ConsoleColor currentBackground;

        private ConsoleColor currentForeground;

        public ConsoleSettings(GraficCell grafic)
        {
            Set(grafic);
        }

        internal void Set(GraficCell grafic)
        {
            SaveCurrent();
            Console.BackgroundColor = grafic.Color;
            Console.ForegroundColor = grafic.TextColor;
        }

        internal void Return()
        {
            Console.BackgroundColor = currentBackground;
            Console.ForegroundColor = currentForeground;
            Console.SetCursorPosition(0, 20);
        }

        private void SaveCurrent()
        {
            currentBackground = Console.BackgroundColor;
            currentForeground = Console.ForegroundColor;
        }
    }
}
