using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logic;
using Core;

namespace Minew_OUT.Layers
{
    public partial class Game : UserControl
    {
        private int cellScale;

        private CellConverter converter;

        private bool button1Clicked = false;

        private MainForm mainForm;

        private int currentLogoNumber = 0;

        public GameHandler gameHandler;

        public Game(MainForm mainForm)
        {
            InitializeComponent();
            SetScale();
            this.mainForm = mainForm;
            gameHandler = new GameHandler(DisplayCell);
            converter = new CellConverter(cellScale);
            SetSizes();
            PlaceBoxes(); 
        }

        private void SetScale()
        {
            int basicScale = 30;
            var fieldSize = new Core.Settings().FieldSize;
            cellScale = basicScale * 21 / (17 + fieldSize * 4);
        }

        private void SetSizes()
        {
            this.Size = mainForm.Size;
            var fieldSizeScaled = (gameHandler.GetFieldSize() * cellScale) + 5;
            FieldArea.Size = new Size(fieldSizeScaled.X, fieldSizeScaled.Y);

        }

        private void PlaceBoxes()
        {
            Point formMiddle = new Point(mainForm.Width / 2, mainForm.Height / 2);
            Point fieldMiddle = new Point(FieldArea.Width / 2, FieldArea.Height / 2);
            int headWeight = HeadPicture.Width / 2;
            FieldArea.Location = new Point(formMiddle.X - fieldMiddle.X, formMiddle.Y - fieldMiddle.Y);
            HeadPicture.Location = new Point(formMiddle.X - headWeight);
        }

        private void Game_Load(object sender, EventArgs e)
        {
            //StartHeadDisplaying();
            //Task.Factory.StartNew(() =>
            //{
            //    Thread.Sleep(200);
            //    gameHandler.DisplayField();
            //});
        }


        private void DisplayCell(LogicCell cell)
        {
            var cellBitmap = converter.Convert(cell);
            FieldArea.CreateGraphics().DrawImage(cellBitmap, cell.X * cellScale, cell.Y * cellScale, cellScale, cellScale);
            //FieldArea.Image = FieldArea.CreateGraphics();
        }

        private void FieldArea_Paint(object sender, PaintEventArgs e)
        {

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            TryMove(keyData);
            TryMark(keyData);
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
            var stepped = gameHandler.GetSteppedCell();
            if (stepped.GetType() == typeof(MineCell))
            {
                GameOver(stepped);
            }
            if (stepped.GetType() == typeof(FinishSpaceCell))
            {
                Finish();
            }
        }

        private void GameOver(Cell cell)
        {
            DisplayAnimation(FieldArea, "Boom", (cell.X - 1) * cellScale, (cell.Y - 1) * cellScale, cellScale * 3, cellScale * 3);
            gameHandler.ChangeViewMode(typeof(MineCell), CellView.Visible);
            DoWithDelay(() => { FieldArea.Refresh(); }, 1000);
            DoWithDelay(() => { GameOverLabel1.Visible = true; }, 2000);
            DoWithDelay(() => { GameOverLabel2.Visible = true;}, 3000);
            DoWithDelay(() => { mainForm.ChangeLayer(new StartMenu(mainForm)); }, 5000);
        }

        private void Finish()
        {
            DoWithDelay(() => { FieldArea.Refresh(); }, 1000);
            DoWithDelay(() => { FinishLabel.Visible = true; }, 2000);
            DoWithDelay(() => { mainForm.ChangeLayer(new StartMenu(mainForm)); }, 4000);
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

        private void Game_Enter(object sender, EventArgs e)
        {
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!button1Clicked)
            {
                timer1.Start();
                button1.Text = "go back";
                gameHandler.DisplayField();
                button1Clicked = true;
            }
            else
            {
                mainForm.ChangeLayer(new StartMenu(mainForm));
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DrawLogo();
        }

        private void DrawLogo()
        {
            Bitmap picture = new Bitmap(@"Grafics\Animations\HeadLogo\" + currentLogoNumber + ".bmp");
            HeadPicture.CreateGraphics().DrawImage(picture, 140, 0, 330, 86);
            currentLogoNumber = 1 - currentLogoNumber;
        }
    }
}
