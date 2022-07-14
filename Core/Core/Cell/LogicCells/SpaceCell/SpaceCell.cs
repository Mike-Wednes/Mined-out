using System.Runtime.Serialization;

namespace Core
{
    [DataContract]
    public abstract class SpaceCell : LogicCell
    {
        public SpaceCell()
        {

        }

        public SpaceCell(Cell cell)
            : this(cell.X, cell.Y)
        { }

        public SpaceCell(int x, int y)
            : base(x, y)
        {
            View = CellView.Invisible;
        }

        public SpaceCell(int x, int y, bool isVisited, bool isMarked)
            : base(x, y, isVisited, isMarked)
        { }
    }
}