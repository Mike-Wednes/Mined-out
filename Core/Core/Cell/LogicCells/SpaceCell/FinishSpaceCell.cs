using System.Runtime.Serialization;

namespace Core
{
    [DataContract]
    public class FinishSpaceCell : SpaceCell, IFinish
    {
        public FinishSpaceCell()
        {

        }

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
