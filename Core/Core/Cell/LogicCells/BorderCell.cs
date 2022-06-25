namespace Core
{
    public class BorderCell : LogicCell
    {
        public BorderCell(Cell cell)
            : this(cell.X, cell.Y)
        { }

        public BorderCell(int x, int y)
            : base(x, y)
        { }

        public BorderCell(int x, int y, bool isVisited, bool isMarked)
            : base(x, y, isVisited, isMarked)
        { }
    }
}
