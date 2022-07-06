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
            RedactorApplication app = new RedactorApplication();
            app.Start();
        }

        protected override ModeType? KeyRespond(ConsoleKey key)
        {
            return ModeType.Start;
        }
    }
}
