namespace Core
{
    public class WaySpaceCell : SpaceCell
    {
        public WaySpaceCell(int x, int y)
            : base(x, y)
        { }

        public WaySpaceCell(Cell cell)
            : base(cell)
        { }

        public WaySpaceCell(int x, int y, bool isVisited, bool isMarked)
            : base(x, y, isVisited, isMarked)
        {
            View = CellView.Invisible;
        }
    }
}
