using GraficRedactor;

namespace ConsoleUI
{
    internal class RedactorMenu : Menu
    {
        public override ModeType ThisMode { get { return ModeType.Redactor; } }

        public RedactorMenu()
        {
        }

        protected override void StartDisplaying()
        {    
            Redactor redactor = new Redactor();
            redactor.Start();
        }

        protected override ModeType? KeyRespond(ConsoleKey key)
        {
            return ModeType.Start;
        }
    }
}
