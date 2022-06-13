using Core;

namespace GraficRedactor
{
    internal class KeyChecker
    {
        private Redactor redactor = new Redactor();

        public KeyChecker(Redactor currRedactor)
        {
            redactor = currRedactor;
        }

        internal void DoMove(ConsoleKeyInfo key)
        {
            if (typeof(GraficRedactor.KeysGroups.MoveKeys).DoesEnumContainKey(key))
            {
                redactor.Move(key);
            }
        }

        internal void EnterColor(ConsoleKeyInfo key, Palette palette, string propName)
        {
            if (key.Key == ConsoleKey.Enter)
            {
                if (IsCursorAtPalette())
                {
                    redactor.SaveAndDisplayCellChanges(propName, palette.GetColor(redactor.cursor));
                }
            }
        }

        internal void AddLine(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.R)
            {
                redactor.AddLine();
            }
        }

        internal void EscapeToStandart(ConsoleKeyInfo key, bool returnCursor = true)
        {
            if (key.Key == ConsoleKey.Escape)
            {
                redactor.ReturnToStandart(redactor.ClearAndPrintStandart, returnCursor);
            }
        }

        internal void ChangeMode(ConsoleKeyInfo key)
        {
            if (typeof(GraficRedactor.KeysGroups.ChooseModeKeys).DoesEnumContainKey(key))
            {
                redactor.ChangeMode(key);
            }
        }

        internal void DeleteLast(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.Backspace)
            {
                if (redactor.currentCollection.Count > 0)
                {
                    if (redactor.currentEditingCell.Equals(new GraficCell()))
                    {
                        redactor.currentCollection.RemoveAt(redactor.currentCollection.Count - 1);
                    }
                    else
                    {
                        redactor.currentEditingCell = new GraficCell();
                    }
                }
                redactor.ClearAndPrintStandart();
            }
        }

        internal void DisplayAnimated(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.A)
            {
                Console.Clear();
                redactor.DisplayLabelsByTag("PL");
                redactor.DisplayCollectionAnimation(redactor.currentCollection);
            }
        }

        internal void Delete(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.Delete)
            {
                var index = redactor.currentCollection.FindLastIndex(g => g.Equals(redactor.cursor));
                if (index != -1)
                {
                    redactor.currentCollection.RemoveAt(index);
                    redactor.ClearAndPrintStandart();
                }
            }
        }

        internal void PasteCell(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.V)
            {
                var last = new GraficCell(redactor.currentCollection.Last());
                if (last != null)
                {
                    last.EqualizeCoordinates(redactor.cursor);
                    redactor.currentCollection.Add(last);
                    redactor.DisplayCell(last);
                }
            }
        }

        internal void AcceptSave(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.Enter)
            {
                redactor.Record(redactor.currentCollection);
                redactor.ClearEnteringArea();
                redactor.Start();
            }
        }

        internal void DeclineSave(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.Escape)
            {
                redactor.Start();
            }
        }

        internal void EnterCell(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.Enter)
            {
                redactor.EnterCell();
            }
        }

        private bool IsCursorAtPalette()
        {
            if (redactor.cursor.X >= redactor.paletteColor.OffSet.X && redactor.cursor.X < redactor.paletteColor.OffSet.X + redactor.paletteColor.Cols)
            {
                if (redactor.cursor.Y >= redactor.paletteColor.OffSet.Y && redactor.cursor.Y < redactor.paletteColor.OffSet.Y + redactor.paletteColor.Rows)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
