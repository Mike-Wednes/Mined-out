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
                checker.DeleteLine(keyConverted);
                checker.PasteCell(keyConverted);
                checker.AddLine(keyConverted);
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
                checker.EscapeToStandart(keyConverted);
            }
        }

        public void TextMode(ConsoleKeyInfo? key)
        {
            redactor.ClearEnteringArea();
            redactor.PrintLabel("TextEditingLabel");
            var answer = redactor.GetValueFromUser("TextEditingLabel");
            char ch = ConvertToChar(answer);
            redactor.ChangeProperty("Text", ch);
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
                else if (text.Count() == 1)
                {
                    res = text.First();
                }
                else
                {
                    res = ' ';
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
                checker.EscapeToStandart(keyConverted);
            }
        }

        public void HotKeysMode(ConsoleKeyInfo? key)
        {
            Console.Clear();
            redactor.DisplayLabelsByTag("HK");
            if (key != null)
            {
                ConsoleKeyInfo keyConverted = (ConsoleKeyInfo)key;
                checker.EscapeToStandart(keyConverted, false);
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
            redactor.ChangeProperty("Delay", delay);
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
