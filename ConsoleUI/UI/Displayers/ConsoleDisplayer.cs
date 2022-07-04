using Core;

namespace ConsoleUI
{
    internal class ConsoleDisplayer
    {
        internal void Clear()
        {
            Console.Clear();
        }

        internal void DisplayGraficCell(GraficCell grafic, Cell offset)
        {
            var console = new ConsoleSettings(grafic);
            Console.SetCursorPosition(grafic.X + offset.X, grafic.Y + offset.Y);
            Console.Write(grafic.Text);
            console.Return();
        }

        internal void DisplayGraficCollection(string name, Cell offset)
        {
            var grafic = GraficCell.GetCollection(name);
            foreach (GraficCell cell in grafic)
            {
                DisplayGraficCell(cell, offset);
                Thread.Sleep(cell.Delay);
            }
        }
    }
}
