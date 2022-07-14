using System.Runtime.Serialization;

namespace Core
{
    [DataContract]
    public class BasicSpaceCell : SpaceCell
    {
        public BasicSpaceCell()
        {

        }

        public BasicSpaceCell(Cell cell)
            : this(cell.X, cell.Y)
        { }

        public BasicSpaceCell(int x, int y)
            : base(x, y)
        { }

        public BasicSpaceCell(int x, int y, bool isVisited, bool isMarked)
            : base(x, y, isVisited, isMarked)
        { }
    }
}
