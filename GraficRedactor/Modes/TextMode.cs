namespace GraficRedactor
{
    internal class TextMode : Mode
    {
        public TextMode(Redactor redactor)
            : base(redactor)
        { }

        public override RedactMode? Handle()
        {
            redactor.ClearEnteringArea();
            redactor.PrintLabel("TextEditingLabel");
            var answer = redactor.GetValueFromUser("TextEditingLabel");
            char ch = ConvertToChar(answer);
            redactor.ChangeProperty("Text", ch);
            return RedactMode.GeneralMode;
        }

        private char ConvertToChar(string? text)
        {
            char res = ' ';
            if (text == null)
            {
                return res;
            }
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
            return res;
        }
    }
}