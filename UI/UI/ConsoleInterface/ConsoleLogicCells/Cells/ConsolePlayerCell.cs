using Core;

namespace UI
{
    internal class ConsolePlayerCell : PlayerCell, IConsoleLogicCell
    {
        public ConsolePlayerCell(PlayerCell cell)
            : base(cell)
        { }

        GraficCell IConsoleLogicCell.Identify(Field field)
        {
            return new GraficCell(this.X, this.Y, ConsoleColor.DarkGray, field.MinesAroundPlayer().ToString().First(), ConsoleColor.Black);
        }
    }
}