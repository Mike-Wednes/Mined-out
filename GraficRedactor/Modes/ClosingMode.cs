using Core;

namespace GraficRedactor
{
    internal class ClosingMode : Mode
    {
        public ClosingMode(Redactor redactor)
            : base(redactor)
        { }

        public override RedactMode? Handle()
        {
            var path = redactor.GetRootPath() + "CloseRedactor.json";
            var icon = redactor.GetCollection(path);
            redactor.DisplayCollection(icon, new Cell(70, 20));
            ConsoleKey key;
            do
            {
                key = Console.ReadKey(true).Key;
                AcceptSave(key);
            }
            while(key != ConsoleKey.Enter && key != ConsoleKey.Escape);
            return null;
        }

        private void AcceptSave(ConsoleKey key)
        {
            if (key == ConsoleKey.Enter)
            {
                redactor.Record(redactor.currentCollection);
                redactor.ClearEnteringArea();
            }
        }
    }
}