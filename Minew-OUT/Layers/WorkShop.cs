using Core;
using Logic;

namespace WinFormsUI.Layers
{
    public partial class WorkShop : UserControl
    {
        private Level level;

        private MainForm mainForm;

        private CellDisplayer displayer;

        private LogicCell edditingCell;

        public WorkShop(MainForm mainForm)
        {
            InitializeComponent();
            level = new Level();
            level.Size.Y = (int)heightNumeric.Value;
            level.Size.X = (int)widthNumeric.Value;
            displayer = new CellDisplayer();
            edditingCell = new BasicSpaceCell();
            this.mainForm = mainForm;
        }

        private void WorkShop_Load(object sender, EventArgs e)
        {
            displayGrid();
        }

        private void heighNumeric_ValueChanged(object sender, EventArgs e)
        {
            level.Size.Y = (int)heightNumeric.Value;
            displayGrid();
        }

        private void widthNumeric_ValueChanged(object sender, EventArgs e)
        {
            level.Size.X = (int)widthNumeric.Value;
            displayGrid();
        }

        private void displayGrid()
        {
            setScale();
            clearField();
            for (int j = 0; j < level.Size.Y; j++)
            {
                for (int i = 0; i < level.Size.X; i++)
                {
                    displayGridElement(i, j);
                }
            }
            FieldArea.Refresh();
        }

        private void displayLevel()
        {
            clearField();
            displayGrid();
            foreach (LogicCell cell in level.fieldCells)
            {
                displayCell(cell);
            }
        }

        private void clearField()
        {
            displayer.Clear(FieldArea.Image);
        }

        private void setScale()
        {
            int maxParameter = Math.Max(level.Size.X, level.Size.Y);
            displayer.Scale = 630 / maxParameter;
        }

        private void displayCell(LogicCell cell)
        {
            displayer.DrawCell(FieldArea.Image, cell);
            displayer.DrawGridElement(FieldArea.Image, cell);
            FieldArea.Refresh();
        }

        private void displayGridElement(int x, int y)
        {
            displayer.DrawGridElement(FieldArea.Image, new Cell(x, y), true);
            FieldArea.Refresh();
        }

        private void FieldArea_Click(object sender, EventArgs e)
        {
            var cursorPosition = cellUnderCursor();
            edditingCell.X = cursorPosition.X;
            edditingCell.Y = cursorPosition.Y;
            var addingCell = LogicCell.CreateCell(edditingCell.GetType());
            addingCell.MapProperties(edditingCell);
            level.fieldCells.Add(addingCell);
            displayCell(addingCell);
        }

        private Cell cellUnderCursor()
        {
            Point location = this.PointToClient(Cursor.Position);
            location.X -= FieldArea.Location.X;
            location.Y -= FieldArea.Location.Y;
            int x = (int)Math.Floor((double)location.X / displayer.Scale);
            int y = (int)Math.Floor((double)location.Y / displayer.Scale);
            return new Cell(x, y);
        }

        private void borderPicture_Click(object sender, EventArgs e)
        {
            edditingCell = new BorderCell();
        }

        private void nameBox_TextChanged(object sender, EventArgs e)
        {
            level.name = (string)nameBox.Text;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            level.Save();
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "Levels/";
                openFileDialog.Filter = "json files (*.json)| *.json";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = openFileDialog.FileName;
                    level = new Level(path);
                }
            }
            displayLevel();
        }
    }
}
