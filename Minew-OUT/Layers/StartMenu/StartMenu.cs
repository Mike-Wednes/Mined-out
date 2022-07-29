namespace WinFormsUI.Layers
{
    public partial class StartMenu : UserControl
    {
        private MainForm mainForm;

        public StartMenu(MainForm main)
        {
            InitializeComponent();
            mainForm = main;
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            if (GuideStateChecker.IsDone())
            {
                mainForm.ChangeLayer(new Game(mainForm));
            }
            else
            {
                GuideStateChecker.WriteDone();
                mainForm.ChangeLayer(new Guide(mainForm));
            }            
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            mainForm.ChangeLayer(new Layers.Settings(mainForm));
        }

        private void workShopButton_Click(object sender, EventArgs e)
        {
            mainForm.ChangeLayer(new Layers.WorkShop(mainForm));
        }
    }
}
