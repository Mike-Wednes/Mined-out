namespace Minew_OUT
{
    public partial class MainForm : Form
    {
        private int currentLayerIndex;

        private UserControl currentLayer;

        public MainForm()
        {
            InitializeComponent();
            KeyPreview = true;
            this.MaximumSize = new Size(1200, 900);
            this.MinimumSize = this.MaximumSize;
            currentLayer = new Layers.StartMenu(this);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ChangeLayer(new Layers.StartMenu(this));
        }

        public void ChangeLayer(UserControl layer)
        {
            this.Controls.Remove(currentLayer);
            this.Controls.Add(layer);
            currentLayer = layer;
        }
    }
}
