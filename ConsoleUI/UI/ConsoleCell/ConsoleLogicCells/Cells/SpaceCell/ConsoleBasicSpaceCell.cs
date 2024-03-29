﻿using Core;

namespace ConsoleUI
{
    public class ConsoleBasicSpaceCell : BasicSpaceCell, IConsoleLogicCell
    {
        public ConsoleBasicSpaceCell(BasicSpaceCell cell)
            : base(cell)
        { }

        GraficCell IConsoleLogicCell.Identify()
        {
            var cell = new GraficCell(this.X, this.Y);
            if (this.IsVisited == true)
            {
                cell.Color = ConsoleColor.DarkGray;
            }
            if (this.IsMarked == true)
            {
                cell.Color = ConsoleColor.Red;
            }
            return cell;
        }
    }
}
