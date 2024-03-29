﻿using System.Runtime.Serialization;

namespace Core
{
    [DataContract]
    public class MineCell : LogicCell, IDamaging
    {
        public MineCell()
        { }

        public MineCell(Cell cell)
            : this(cell.X, cell.Y)
        { }

        public MineCell(int x, int y)
            : base(x, y)
        {
            View = CellView.Invisible;
        }

        public MineCell(int x, int y, bool isVisited, bool isMarked)
            : base(x, y, isVisited, isMarked)
        {
            View = CellView.Invisible;
        }
    }
}