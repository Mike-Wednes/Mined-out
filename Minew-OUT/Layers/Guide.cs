namespace WinFormsUI.Layers
{
    public partial class Guide : UserControl
    {
        private MainForm mainForm;

        private Action[] DisplayOrder;

        private int currentAction;

        public Guide(MainForm main)
        {
            InitializeComponent();
            mainForm = main;
            SetOrder();
            currentAction = 0;
        }

        private void SetOrder()
        {
            DisplayOrder = new Action[]
            {
                () =>
                {
                    introducingLabel.Visible = true;
                    NextButton.Text = "NEXT";
                },
                () =>
                {
                    introducingLabel.Visible = false;
                    Thread.Sleep(500);
                    pictureBox1.Visible = true;
                    MovingLabel.Visible = true;
                    MinesCountLabel.Visible = true;
                },
                () =>
                {
                    pictureBox1.Visible = false;
                    MovingLabel.Visible = false;
                    MinesCountLabel.Visible = false;
                    Thread.Sleep(500);
                    pictureBox2.Visible = true;
                    MarkLabel.Visible = true;
                },
                () =>
                {
                    pictureBox2.Visible = false;
                    MarkLabel.Visible = false;
                    Thread.Sleep(500);
                    LastLabel.Visible = true;
                    NextButton.Text = "GO GO";
                },
                () =>
                {
                    LastLabel.Visible = false;
                    Thread.Sleep(500);
                    mainForm.ChangeLayer(new Game(mainForm));
                }

            };
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            DisplayOrder[currentAction].Invoke();
            currentAction++;
        }

        private void Guide_Load(object sender, EventArgs e)
        {

        }
    }
}
