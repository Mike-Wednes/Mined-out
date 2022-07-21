using Core;
using Logic;

namespace WinFormsUI.Layers
{
    public partial class WorkShop : UserControl
    {
        private Level level;

        private MainForm mainForm;

        private CellDisplayer displayer;

        private EdditingTool? edditingTool;

        private Highlighter toolHighlighter;

        private Highlighter cursorHighlighter;

        private CursorShape cursor;

        public WorkShop(MainForm mainForm)
        {
            InitializeComponent();
            level = new Level();
            level.Size.Y = (int)heightNumeric.Value;
            level.Size.X = (int)widthNumeric.Value;
            displayer = new CellDisplayer(new WorkShopCellConverter());
            this.mainForm = mainForm;
            edditingTool = null;
            toolHighlighter = new Highlighter(highlighterBox1);
            cursorHighlighter = new Highlighter(highlighterBox2);
            dotCursorBox_Click(this, new EventArgs());
        }

        private void WorkShop_Load(object sender, EventArgs e)
        {
            DoWithDelay(displayGrid, 10);
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
                displayCell(level.PlayerCell, false);
            }
            FieldArea.Refresh();
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
            cursor.DoWithClick(useTool, locationUnderCursor());
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

        private Cell locationUnderCursor()
        {
            Point location = this.PointToClient(Cursor.Position);
            location.X -= FieldArea.Location.X;
            location.Y -= FieldArea.Location.Y;
            int x = (int)Math.Floor((double)location.X / displayer.Scale);
            int y = (int)Math.Floor((double)location.Y / displayer.Scale);
            return new Cell(x, y);
        }

        private void nameBox_TextChanged(object sender, EventArgs e)
        {
            level.name = (string)nameBox.Text;
        }

        private void typeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeTool(new AddingTool(ComboBoxesDictionary.type[typeBox.Text]), typeBox);
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

        private void DoWithDelay(Action action, int delay)
        {
            var timer = new System.Windows.Forms.Timer();
            timer.Tick += (Object? myObject, EventArgs myEventArgs) => { action(); timer.Stop(); };
            timer.Interval = delay;
            timer.Start();
        }

        private void clearPicture_Click(object sender, EventArgs e)
        {
            changeTool(new ClearingTool(), clearPicture);
        }

        private void changeTool(EdditingTool tool, Control sender)
        {
            edditingTool = tool;
            toolHighlighter.Highlight(sender);
        }

        private void changeCursor(CursorShape cursor, Control sender)
        {
            this.cursor = cursor;
            cursorHighlighter.Highlight(sender);
        }

        private void useTool(Cell location)
        {
            if (edditingTool == null)
            {
                return;     
            }
            var cell = level.Get(location);
            LogicCell? eddited;
            if (cell != null)
            {
                deleteAt(cell);
                eddited = edditingTool.GetEddited(cell);
            }
            else
            {
                eddited = edditingTool.GetEddited(location);
            }
            if (eddited == null)
            {
                return;
            }
            add(eddited);
        }

        private void add(LogicCell cell)
        {
            if (cell.GetType() == typeof(PlayerCell))
            {
                addPlayer((PlayerCell)cell);
            }
            else
            {
                level.Add(cell);
            }
            displayCell(cell);
        }

        private void visibilityPicture_Click(object sender, EventArgs e)
        {
            changeTool(new VisibilityTool(), visibilityPicture);
        }

        private void dotCursorBox_Click(object sender, EventArgs e)
        {
            changeCursor(new DotCursor(), dotCursorBox);
        }

        private void lineCursorBox_Click(object sender, EventArgs e)
        {
            changeCursor(new LineCursor(), lineCursorBox);
        }
    }
}
