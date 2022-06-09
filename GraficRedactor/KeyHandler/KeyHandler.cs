using Core;

namespace GraficRedactor
{
    internal class KeyHandler
    {
        private Redactor redactor;

        private KeyChecker checker;

        public KeyHandler(Redactor currRedactor)
        {
            redactor = currRedactor;
            checker = new KeyChecker(redactor);
        }

        public void GeneralMode(ConsoleKeyInfo? key)
        {
            if (!redactor.currentEditingCell.Equals(new GraficCell()))
            {
                redactor.PrintLabel("AddCellLabel");
            }
            if (key != null)
            {
                ConsoleKeyInfo keyConverted = (ConsoleKeyInfo)key;
                checker.DoMove(keyConverted);
                checker.ChangeMode(keyConverted);
                checker.EnterCell(keyConverted);
                checker.DeleteLast(keyConverted);
                checker.DisplayAnimated(keyConverted);
                checker.Delete(keyConverted);
                checker.PasteCell(keyConverted);
            }
        }

        public void ColorMode(ConsoleKeyInfo? key)
        {
            redactor.DisplayPaletteIfNeeded(redactor.paletteColor);
            if (key != null)
            {
                ConsoleKeyInfo keyConverted = (ConsoleKeyInfo)key;
                checker.DoMove(keyConverted);
                checker.EnterColor(keyConverted, redactor.paletteColor, "Color");
                checker.EscapeColor(keyConverted);
            }
        }

        public void TextMode(ConsoleKeyInfo? key)
        {
            redactor.ClearEnteringArea();
            redactor.PrintLabel("TextEditingLabel");
            var answer = redactor.GetValueFromUser("TextEditingLabel");
            char ch = ConvertToChar(answer);
            redactor.SaveAndDisplayCellChanges("Text", ch);
        }

        private char ConvertToChar(string? text)
        {
            char res;
            if (text != null)
            {
                if (text.Count() > 1)
                {
                    try
                    {
                        res = Convert.ToChar(text);
                    }
                    catch
                    {
                        res = ' ';
                    }
                }
                else
                {
                    res = text.First();
                }
            }
            else
            {
                res = ' ';
            }
            return res;
        }

        public void TextColorMode(ConsoleKeyInfo? key)
        {
            redactor.DisplayPaletteIfNeeded(redactor.paletteTextColor);
            if (key != null)
            {
                ConsoleKeyInfo keyConverted = (ConsoleKeyInfo)key;
                checker.DoMove(keyConverted);
                checker.EnterColor(keyConverted, redactor.paletteTextColor, "TextColor");
                checker.EscapeColor(keyConverted);
            }
        }

        public void DelayMode(ConsoleKeyInfo? key)
        {
            redactor.ClearEnteringArea();
            redactor.PrintLabel("DelayEditingLabel");
            var answer = redactor.GetValueFromUser("DelayEditingLabel");
            int delay;
            if (answer != null)
            {
                if (int.TryParse(answer, out delay))
                {
                }
                else
                {
                    delay = 0;
                }
            }
            else
            {
                delay = 0;
            }
            redactor.SaveAndDisplayCellChanges("Delay", delay);
        }

        public void ClosingMode(ConsoleKeyInfo? key)
        {
            var path = redactor.GetRootPath() + "CloseRedactor.json";
            var icon = redactor.GetCollection(path);
            redactor.DisplayCollection(icon, new Cell(70, 20));
            if (key != null)
            {
                ConsoleKeyInfo keyConverted = (ConsoleKeyInfo)key;
                checker.AcceptSave(keyConverted);
                checker.DeclineSave(keyConverted);
            }

        }
    }
}
