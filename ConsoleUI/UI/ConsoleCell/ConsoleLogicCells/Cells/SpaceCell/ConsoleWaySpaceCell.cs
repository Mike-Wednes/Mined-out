using Core;

namespace ConsoleUI
{
    public class ConsoleWaySpaceCell : WaySpaceCell, IConsoleLogicCell
    {
        public ConsoleWaySpaceCell(LogicCell cell)
            : base(cell)
        { }

        GraficCell IConsoleLogicCell.Identify()
        {
            var cell = new GraficCell(X, Y);
            if (this.View == CellView.Visible)
            {
                cell.Color = ConsoleColor.White;
            }
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
