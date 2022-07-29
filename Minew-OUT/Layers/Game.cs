using Logic;
using Core;

namespace WinFormsUI.Layers
{
    public partial class Game : UserControl
    {
        private MainForm mainForm;

        private GameHandler gameHandler;

        private Dictionary<string, int> currentAnimationCadre;

        private bool doProcessingKeys;

        private CellDisplayer displayer;

        public Game(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            displayer = new CellDisplayer(GetScale(), new CellConverter());
            PlaceBoxes();
            currentAnimationCadre = new Dictionary<string, int>()
            {
                {"HeadLogo", 1},
                {"Loading", 1},
            };
            doProcessingKeys = true;
        }

        private int GetScale()
        {
            int basicScale = 30;
            var fieldSize = new Core.Settings().FieldSize;
            return basicScale * 21 / (17 + fieldSize * 4);
        }

        private void PlaceBoxes()
        {
            Point formMiddle = new Point(mainForm.Width / 2, mainForm.Height / 2);
            Point fieldMiddle = new Point(FieldArea.Width / 2, FieldArea.Height / 2);
            int headWeight = HeadLogo.Width / 2;
            FieldArea.Location = new Point(formMiddle.X - fieldMiddle.X, formMiddle.Y - fieldMiddle.Y);
            HeadLogo.Location = new Point(formMiddle.X - headWeight);
        }

        private void Game_Load(object sender, EventArgs e)
        {
        }

        private void start()
        {
            standardButton.Hide();
            customButton.Hide();
            loadingLabel.Visible = true;
            logoTimer.Start();
            gameHandler.DisplayField();
            DoWithDelay(() => ShowField(), 500);
        }

        private void ShowField()
        {
            loadingLabel.Hide(); 
            FieldArea.Visible = true; 
            HeadLogo.Visible = true;
        }

        private void DisplayCell(LogicCell cell)
        {
            displayer.DrawCell(FieldArea.Image, cell);
            FieldArea.Refresh();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (doProcessingKeys)
            {
                TryMove(keyData);
                TryMark(keyData);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void TryMove(Keys keyData)
        {
            if (KeyStorage.ArrowToDirection.ContainsKey(keyData))
            {
                gameHandler.MovePlayer(KeyStorage.ArrowToDirection[keyData]);
                CheckStepped();
            }
        }

        private void CheckStepped()
        {
            var stepped = gameHandler.GetCurrentCell();
            if (stepped as IDamaging != null)
            {
                gameHandler.ChangeType(stepped, typeof(BasicSpaceCell));
                GameOver(stepped);
            }
            if (stepped as IFinish != null)
            {
                Finish();
            }
        }

        private void GameOver(Cell cell)
        {
            DisplayAnimation(FieldArea, "Boom", (cell.X - 1) * displayer.Scale, (cell.Y - 1) * displayer.Scale, displayer.Scale * 3, displayer.Scale * 3);
            gameHandler.ChangeViewMode(typeof(MineCell), CellView.Visible);
            DoWithDelay(() => { FieldArea.Hide(); ClearLogo(); }, 1000);
            DoWithDelay(() => { GameOverLabel1.Visible = true; }, 2000);
            DoWithDelay(() => { GameOverLabel2.Visible = true;}, 3000);
            DoWithDelay(() => { mainForm.ChangeLayer(new StartMenu(mainForm)); }, 5000);
            doProcessingKeys = false;
        }

        private void Finish()
        {
            DoWithDelay(() => { FieldArea.Hide(); ClearLogo(); }, 1000);
            DoWithDelay(() => { FinishLabel.Visible = true; }, 2000);
            DoWithDelay(() => { mainForm.ChangeLayer(new StartMenu(mainForm)); }, 4000);
            doProcessingKeys = false;
        }

        private void ClearLogo()
        {
            logoTimer.Stop();
            HeadLogo.Refresh();
        }

        private void DoWithDelay(Action action, int delay)
        {
            var timer = new System.Windows.Forms.Timer();
            timer.Tick += (Object? myObject, EventArgs myEventArgs) => { action(); timer.Stop(); };
            timer.Interval = delay;
            timer.Start();
        }

        private void TryMark(Keys keyData)
        {
            if (KeyStorage.KeyToDirection.ContainsKey(keyData))
            {
                gameHandler.Mark(KeyStorage.KeyToDirection[keyData]);
            }
        }

        private void DisplayAnimation(PictureBox box, string name, int x, int y, int width, int height)
        {
            DirectoryInfo dir = new DirectoryInfo(@"Grafics\Animations\" + name);
            var imageFiles = dir.GetFiles("*.bmp");
            var delayFile = dir.GetFiles("Delay.txt").First();

            int delay = GetDelay(delayFile);
            var bitmaps = GetBitmaps(imageFiles);
            foreach (var bitmap in bitmaps)
            {
                var grafic = box.CreateGraphics();
                grafic.DrawImage(bitmap, x, y, width, height);
                Thread.Sleep(delay);
            }
        }

        private int GetDelay(FileInfo fileInfo)
        {
            int delay = 0;
            using (StreamReader sreader = fileInfo.OpenText())
            {
                delay = int.Parse(sreader.ReadToEnd());
            }
            return delay;
        }

        private Bitmap[] GetBitmaps(FileInfo[] fileInfos)
        {
            Bitmap[] bitmaps = new Bitmap[fileInfos.Length];
            int count = 0;
            foreach (FileInfo fileInfo in fileInfos)
            {
                bitmaps[count] = new Bitmap(fileInfo.FullName);
                count++;
            }
            return bitmaps;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DrawAnimationCadre("HeadLogo", 2, new Point(140, 0), new Point(330, 86));
        }

        private void DrawAnimationCadre(string animationName, int cadreAmount, Point size, Point location)
        {
            Bitmap picture = new Bitmap(@"Grafics\Animations\" + animationName + @"\" + currentAnimationCadre[animationName] + ".bmp");
            var pictureBox = this.Controls.Find(animationName, false).First() as PictureBox;
            if (pictureBox == null)
            {
                return;
            }
            pictureBox.CreateGraphics().DrawImage(picture, size.X, size.Y, location.X, location.Y);
            currentAnimationCadre[animationName]++;
            currentAnimationCadre[animationName] = currentAnimationCadre[animationName] > cadreAmount
                ? 1
                : currentAnimationCadre[animationName];
        }

        private void standartButton_Click(object sender, EventArgs e)
        {
            gameHandler = new GameHandler(DisplayCell);
            start();
        }

        private void customButton_Click(object sender, EventArgs e)
        {
            var path = LevelDirectoryFormatter.Path();
            if(path == "")
            {
                mainForm.ChangeLayer(new StartMenu(mainForm));
                return;
            }
            var level = new Level(path);
            gameHandler = new GameHandler(DisplayCell, level.ConvertToField());
            displayer.Scale = 630 / Math.Max( level.Size.X, level.Size.Y );
            start();
        }
    }
}
