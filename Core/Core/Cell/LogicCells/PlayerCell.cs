﻿namespace Core
{
    public class PlayerCell : LogicCell
    {
        public int MinesAround { get; set; }

        public PlayerCell(PlayerCell cell)
            :this(cell.X, cell.Y)
        { 
            MinesAround = cell.MinesAround;
        }

        public PlayerCell(int x, int y)
            : base(x, y, true)
        { }

        public PlayerCell(int x, int y, bool isVisited, bool isMarked, int minesAround)
            : base(x, y, isVisited, isMarked)
        {
            MinesAround = minesAround;
        }
    }
}