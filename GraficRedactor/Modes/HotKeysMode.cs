namespace GraficRedactor
{
    internal class HotKeysMode : Mode
    {
        public HotKeysMode(Redactor redactor)
            : base(redactor)
        { }

        public override RedactMode? Handle()
        {
            Console.Clear();
            redactor.DisplayLabelsByTag("HK");
            ConsoleKey key = Console.ReadKey(true).Key;
            EscapeToStandart(key);
            return RedactMode.GeneralMode;
        }

        internal void EscapeToStandart(ConsoleKey key)
        {
            if (key == ConsoleKey.Escape)
            {
                redactor.ReturnToStandart(redactor.ClearAndPrintStandart, true);
            }
        }
    }
}