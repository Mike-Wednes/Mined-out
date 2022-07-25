namespace WinFormsUI.Layers
{
    public partial class Settings : UserControl
    {
        private MainForm mainForm;

        public Settings(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            sizeTrackBar.Value = new Core.Settings().FieldSize;
            difficultyTrackBar.Value = new Core.Settings().DifficultyLevel;
        }

        private void Settings_Load(object sender, EventArgs e)
        {

        }

        private void sizeTrackBar_Scroll(object sender, EventArgs e)
        {
            new Core.Settings().SetProperty("FieldSize", sizeTrackBar.Value);
        }

        private void difficultyTrackBar_Scroll(object sender, EventArgs e)
        {
            new Core.Settings().SetProperty("DifficultyLevel", difficultyTrackBar.Value);
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            mainForm.ChangeLayer(new StartMenu(mainForm));
        }
    }
}
