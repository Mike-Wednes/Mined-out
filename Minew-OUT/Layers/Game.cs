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

        private MainForm mainForm;

        public GameHandler gameHandler;

        private Bitmap bitmap;

        public Game(MainForm mainForm)
        {
            InitializeComponent();
            SetScale();
            this.mainForm = mainForm;
            gameHandler = new GameHandler(DisplayCell);
            converter = new CellConverter(cellScale);
            SetSizes();
            PlaceBoxes();
            bitmap = new Bitmap("Grafics/MineCell.bmp");
        }

        private void SetScale()
        {
            int basicScale = 30;
            var fieldSize = new Settings().FieldSize;
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
            StartHeadDisplaying();
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(200);
                gameHandler.DisplayField();
            });
        }


        private void DisplayCell(LogicCell cell)
        {
            var cellBitmap = converter.Convert(cell);
            FieldArea.CreateGraphics().DrawImage(cellBitmap, cell.X * cellScale, cell.Y * cellScale, cellScale, cellScale);
        }

        private void FieldArea_Paint(object sender, PaintEventArgs e)
        {

        }

        private void StartHeadDisplaying()
        {
            StartAnimation(HeadPicture, "HeadLogo", 140, 0, 330, 86);
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
                DisplayAnimation(FieldArea, "Boom", (stepped.X - 1) * cellScale, (stepped.Y - 1) * cellScale, cellScale * 3, cellScale * 3);
                gameHandler.ChangeViewMode(typeof(MineCell), CellView.Visible);
                Thread.Sleep(1000);
                mainForm.ChangeLayer(new StartMenu(mainForm));

            }
        }

        private void TryMark(Keys keyData)
        {
            if (KeyStorage.KeyToDirection.ContainsKey(keyData))
            {
                gameHandler.Mark(KeyStorage.KeyToDirection[keyData]);
            }
        }

        private void StartAnimation(PictureBox box, string name, int x, int y, int width, int height)
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    DisplayAnimation(box, name, x, y, width, height);
                }
            });
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
    }
}
