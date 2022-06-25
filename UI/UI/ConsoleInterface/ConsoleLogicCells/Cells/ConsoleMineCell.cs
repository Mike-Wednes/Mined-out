using Core;

namespace UI
{
    public class ConsoleMineCell : MineCell, IConsoleLogicCell
    {
        public ConsoleMineCell(MineCell cell)
            : base(cell)
        { }

        GraficCell IConsoleLogicCell.Identify(Field field)
        {
            var cell = new GraficCell(X, Y);
            if (this.IsMarked == true)
            {
                cell.Color = ConsoleColor.Red;
            }
            if (this.View == CellView.Visible)
            {
                cell.Color = ConsoleColor.Black;
                cell.TextColor = ConsoleColor.Red;
                cell.Text = 'x';
            }
            return cell;
        }
    }
}