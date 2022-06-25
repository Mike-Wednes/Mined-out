using Core;

namespace UI
{
    public class ConsoleBorderCell : BorderCell, IConsoleLogicCell
    {
        public ConsoleBorderCell(BorderCell cell)
            : base(cell)
        { }

        GraficCell IConsoleLogicCell.Identify(Field field)
        {
            var cell = new GraficCell(X, Y);
            if (this.View == CellView.Visible)
            {
                cell.Color = ConsoleColor.Blue;
            }
            return cell;
        }
    }
}
