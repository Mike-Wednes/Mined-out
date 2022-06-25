namespace Core
{
    public class PlayerCell : LogicCell
    {
        public PlayerCell(Cell cell)
            :this(cell.X, cell.Y)
        { }

        public PlayerCell(int x, int y)
            : base(x, y, true)
        { }

        public PlayerCell(int x, int y, bool isVisited, bool isMarked)
            : base(x, y, isVisited, isMarked)
        { }
    }
}