namespace GraficRedactor
{
    internal class DelayMode : Mode
    {
        public DelayMode(Redactor redactor)
            : base(redactor)
        { }

        public override RedactMode? Handle()
        {
            redactor.ClearEnteringArea();
            redactor.PrintLabel("DelayEditingLabel");
            var answer = redactor.GetValueFromUser("DelayEditingLabel");
            int delay;
            if (answer != null)
            {
                if (!int.TryParse(answer, out delay))
                {
                    delay = 0;
                }
            }
            else
            {
                delay = 0;
            }
            redactor.ChangeProperty("Delay", delay);
            return RedactMode.GeneralMode;
        }
    }
}