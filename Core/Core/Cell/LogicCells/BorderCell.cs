using System.Runtime.Serialization;

namespace Core
{
    [DataContract]
    public class BorderCell : LogicCell, IImpassable
    {
        public BorderCell()
        { }

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
