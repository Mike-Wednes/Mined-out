﻿using Core;

namespace ConsoleUI
{
    public class ConsoleFinishSpaceCell : FinishSpaceCell, IConsoleLogicCell
    {
        public ConsoleFinishSpaceCell(FinishSpaceCell cell)
            : base(cell)
        { }

        GraficCell IConsoleLogicCell.Identify()
        {
            var cell = new GraficCell(X, Y);
            if (this.View == CellView.Visible)
            {
                cell.Color = ConsoleColor.Green;
            }
            return cell;
        }
    }
}
