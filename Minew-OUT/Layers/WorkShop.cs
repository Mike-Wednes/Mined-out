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
            displayLevel();
        }

        private void widthNumeric_ValueChanged(object sender, EventArgs e)
        {
            level.Size.X = (int)widthNumeric.Value;
            displayGrid();
            displayLevel();
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
            displayGrid();
            foreach (LogicCell cell in level.Cells)
            {
                displayCell(cell, false);
            }
            if(level.PlayerCell != null)
            {
                displayCell(level.PlayerCell);
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

        private void displayCell(LogicCell cell, bool doRefresh = true)
        {
            displayer.DrawCell(FieldArea.Image, cell);
            displayer.DrawGridElement(FieldArea.Image, cell);
            if (doRefresh)
            {
                FieldArea.Refresh();
            }
        }

        private void displayGridElement(Cell location)
        {
            displayGridElement(location.X, location.Y);
        }

        private void displayGridElement(int x, int y)
        {
            displayer.DrawGridElement(FieldArea.Image, new Cell(x, y), true);
            FieldArea.Refresh();
        }

        private void FieldArea_Click(object sender, EventArgs e)
        {
            var addingCell = convertLogicCell();
            deleteAt(addingCell);
            if(edditingCell.GetType() == typeof(PlayerCell))
            {
                addPlayer((PlayerCell)addingCell);
            }
            else
            {
                level.Add(addingCell);
            }
            displayCell(addingCell);
        }

        private LogicCell convertLogicCell()
        {
            var cursorPosition = cellUnderCursor();
            edditingCell.X = cursorPosition.X;
            edditingCell.Y = cursorPosition.Y;
            var addingCell = LogicCell.CreateCell(edditingCell.GetType());
            addingCell.MapProperties(edditingCell);
            return addingCell;
        }

        private void deleteAt(Cell location)
        {
            if (level.TryRemove(location))
            {
                displayGridElement(location);
            }
        }

        private void addPlayer(PlayerCell player)
        {
            if (level.PlayerCell != null)
            {
                displayGridElement(level.PlayerCell.X, level.PlayerCell.Y);
            }
            level.PlayerCell = player;
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

        private void typeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            edditingCell = LogicCell.CreateCell(ComboBoxesDictionary.type[typeBox.SelectedItem.ToString()]);
            setCelldefault(edditingCell);
        }

        private void setCelldefault(LogicCell cell)
        {
            cell.View = CellView.Visible;
        }

        private void setLevel(Level level)
        {
            this.level = level;
            setFormLevelFields();
            displayLevel();
        }

        private void setFormLevelFields()
        {
            nameBox.Text = level.name;
            widthNumeric.Value = level.Size.X;
            heightNumeric.Value = level.Size.Y;
        }

        private void backMenuItem_Click(object sender, EventArgs e)
        {
            mainForm.ChangeLayer(new StartMenu(mainForm));
        }

        private void OpenMenuStrip_Click(object sender, EventArgs e)
        {
            string path = Level.PathFromDirectory();
            if (path != "")
            {
                setLevel(new Level(path));
            }
        }

        private void saveMenuStip_Click(object sender, EventArgs e)
        {
            level.Save();
        }
    }
}
