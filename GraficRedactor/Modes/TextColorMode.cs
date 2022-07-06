namespace GraficRedactor
{
    internal class TextColorMode : Mode
    {
        public TextColorMode(Redactor redactor)
            : base(redactor)
        { }

        public override RedactMode? Handle()
        {
            redactor.DisplayPalette(redactor.paletteTextColor);
            ConsoleKey key;
            do
            {
                key = Console.ReadKey(true).Key;
                MoveOnSide(key);
            }
            while (key != ConsoleKey.Enter && key != ConsoleKey.Escape);
            EnterColor(key, redactor.paletteTextColor, "TextColor");
            redactor.ReturnToStandart(redactor.ClearAndPrintStandart);
            return RedactMode.GeneralMode;
        }
    }
}
