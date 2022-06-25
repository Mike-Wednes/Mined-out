using Core;

namespace UI
{
    public class ConsoleWaySpaceCell : WaySpaceCell, IConsoleLogicCell
    {
        public ConsoleWaySpaceCell(WaySpaceCell cell)
            : base(cell)
        { }

        GraficCell IConsoleLogicCell.Identify(Field field)
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
