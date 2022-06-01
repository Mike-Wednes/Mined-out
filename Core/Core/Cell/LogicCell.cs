namespace Core
{
    public class LogicCell : Cell
    {
        public CellType Type { get; set; }

        public CellView View { get; set; }

        public bool IsVisited { get; set; } = false;

        public bool IsMarked { get; set; } = false;

        public LogicCell()
        {}

        public LogicCell(LogicCell cell)
            :base(cell)
        {
            this.Type = cell.Type;
        }

        public LogicCell(int x, int y)
            :base(x, y)
        {}

        public LogicCell(int x, int y, CellType type)
            :base(x, y)
        {
            Type = type;
        }

        public LogicCell(int x, int y, CellType type, CellView view)
            :this(x, y, type)
        {
            View = view;
        }
    }
}
