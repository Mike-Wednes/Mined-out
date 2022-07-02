namespace Core
{
    public class FinishSpaceCell : SpaceCell
    {
        public FinishSpaceCell(Cell cell)
            : this(cell.X, cell.Y)
        { }

        public FinishSpaceCell(int x, int y)
            : base(x, y)
        { }

        public FinishSpaceCell(int x, int y, bool isVisited, bool isMarked)
            : base(x, y, isVisited, isMarked)
        {
            View = CellView.Invisible;
        }
    }
}
