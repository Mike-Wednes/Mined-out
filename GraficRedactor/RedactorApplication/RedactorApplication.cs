using Core;

namespace GraficRedactor
{
    public class RedactorApplication
    {
        private Redactor redactor;

        private RedactMode? mode;

        private Dictionary<RedactMode, Mode> ModeStorage;

        public RedactorApplication()
        {
            redactor = new Redactor();
            SetStorage(redactor);
        }

        private void SetStorage(Redactor redactor)
        {
            ModeStorage = new Dictionary<RedactMode, Mode>()
            {
                {RedactMode.GeneralMode, new GeneralMode(redactor) },
                {RedactMode.ColorMode, new ColorMode(redactor) },
                {RedactMode.TextColorMode, new TextColorMode(redactor) },
                {RedactMode.TextMode, new TextMode(redactor) },
                {RedactMode.HotKeysMode, new HotKeysMode(redactor) },
                {RedactMode.DelayMode, new DelayMode(redactor) },
                {RedactMode.ClosingMode, new ClosingMode(redactor) },
            };
        }

        public void Start()
        {
            Console.Clear();
            redactor.SetDefault();
            redactor.RecordPath();
            if (!redactor.CheckPath())
            {
                return;
            }
            RedactingProcess();
        }

        private void RedactingProcess()
        {
            mode = RedactMode.GeneralMode;
            Console.SetWindowSize(200, 50);
            redactor.ClearAndPrintStandart();
            HandleModes();
        }

        private void HandleModes()
        {
            do
            {
                if (mode == null)
                {
                    return;
                }
                mode = ModeStorage[(RedactMode)mode].Handle();
            }
            while (mode != null);
            Console.SetWindowSize(100, 31);
            Start();
        }
    }
}
