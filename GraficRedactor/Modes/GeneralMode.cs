namespace GraficRedactor
{
    internal class GeneralMode : Mode
    {
        public GeneralMode(Redactor redactor)
            : base(redactor)
        { }

        public override RedactMode? Handle()
        {
            RedactMode mode;
            if (!redactor.currentEditingCell.Equals(new GraficCell()))
            {
                redactor.PrintLabel("AddCellLabel");
            }
            do
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                MoveInGrafic(key);
                EnterCell(key);
                DeleteLast(key);
                DisplayAnimated(key);
                Delete(key);
                DeleteLine(key);
                PasteCell(key);
                AddLine(key);
                mode = ChangeMode(key);
            }
            while (mode == RedactMode.GeneralMode);
            return mode;
        }

        private RedactMode ChangeMode(ConsoleKey key)
        {
            if (KeyStorage.KeyToMode.ContainsKey(key))
            {
                return KeyStorage.KeyToMode[key];
            }
            return RedactMode.GeneralMode;
        }

        private void EnterCell(ConsoleKey key)
        {
            if (key == ConsoleKey.Enter)
            {
                redactor.EnterCell();
            }
        }

        private void DeleteLast(ConsoleKey key)
        {
            if (key == ConsoleKey.Backspace)
            {
                if (redactor.currentCollection.Count < 1)
                {
                    return;
                }

                if (redactor.currentEditingCell.Equals(new GraficCell()))
                {
                    redactor.currentCollection.RemoveAt(redactor.currentCollection.Count - 1);
                }
                else
                {
                    redactor.currentEditingCell = new GraficCell();
                }

                redactor.ClearAndPrintStandart();
            }
        }

        internal void DisplayAnimated(ConsoleKey key)
        {
            if (key == ConsoleKey.A)
            {
                Console.Clear();
                redactor.DisplayLabelsByTag("PL");
                redactor.DisplayCollectionAnimation(redactor.currentCollection);
            }
        }

        internal void Delete(ConsoleKey key)
        {
            if (key == ConsoleKey.Delete)
            {
                redactor.Delete();
            }
        }

        internal void DeleteLine(ConsoleKey key)
        {
            if (key == ConsoleKey.T)
            {
                redactor.DeleteLine();
            }
        }

        internal void PasteCell(ConsoleKey key)
        {
            if (key == ConsoleKey.V)
            {
                var last = new GraficCell(redactor.currentCollection.Last());
                if (last == null)
                {
                    return;
                }
                last.MakeEqual(redactor.cursor);
                redactor.currentCollection.Add(last);
                redactor.DisplayCell(last);
            }
        }

        internal void AddLine(ConsoleKey key)
        {
            if (key == ConsoleKey.R)
            {
                redactor.AddLine();
            }
        }
    }
}
