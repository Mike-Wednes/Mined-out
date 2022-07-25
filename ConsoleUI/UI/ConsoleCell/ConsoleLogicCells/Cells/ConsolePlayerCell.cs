using Core;

namespace ConsoleUI
{
    internal class ConsolePlayerCell : PlayerCell, IConsoleLogicCell
    {
        public ConsolePlayerCell(PlayerCell cell)
            : base(cell)
        { }

        GraficCell IConsoleLogicCell.Identify()
        {
            return new GraficCell(this.X, this.Y, ConsoleColor.DarkGray, this.MinesAround.ToString().First(), ConsoleColor.Black);
        }
    }
}