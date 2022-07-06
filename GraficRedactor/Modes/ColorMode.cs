namespace GraficRedactor
{
    internal class ColorMode : Mode
    {
        public ColorMode(Redactor redactor)
            : base(redactor)
        { }

        public override RedactMode? Handle()
        {
            redactor.DisplayPalette(redactor.paletteColor);
            ConsoleKey key;
            do
            {
                key = Console.ReadKey(true).Key;
                MoveOnSide(key);
            }
            while (key != ConsoleKey.Enter && key != ConsoleKey.Escape);
            EnterColor(key, redactor.paletteColor, "Color");
            redactor.ReturnToStandart(redactor.ClearAndPrintStandart);
            return RedactMode.GeneralMode;
        }
    }
}
