using Core;

namespace WinFormsUI
{
    public class PolyLineCursor : LineCursor
    {
        private Cell? lastCell;

        public PolyLineCursor()
        {
            lastCell = null;
        }

        public override void DoWithClick(Action<Cell> action, Cell location)
        {
            if (lastCell == null)
            {
                lastCell = location;
            }
            doInLine(action, lastCell, location);
            lastCell = location;
        }
    }
}
