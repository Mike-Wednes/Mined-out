namespace Core
{
    public class LogicCell : Cell
    {
        public CellType Type { get; set; }

        public CellView View { get; set; }

        public bool IsVisited { get; set; } = false;

        public bool IsMarked { get; set; } = false;

        public LogicCell()
        {
        }

        public LogicCell(LogicCell cell)
        {
            this.X = cell.X;
            this.Y = cell.Y;
            this.Type = cell.Type;
        }

        public LogicCell(int x, int y)
        {
            X = x;
            Y = y;
        }

        public LogicCell(int x, int y, CellType type)
        {
            X = x;
            Y = y;
            Type = type;
        }

        public LogicCell(int x, int y, CellType type, CellView view)
        {
            X = x;
            Y = y;
            Type = type;
            View = view;
        }
    }
}
