using Core;

namespace WinFormsUI
{
    public class LineCursor : CursorShape
    {
        private Cell? lastCell;

        public LineCursor()
        {
            lastCell = null;
        }

        public override void DoWithClick(Action<Cell> action, Cell location)
        {
            if (lastCell == null)
            {
                lastCell = location;
            }
            Cell difference = location - lastCell;
            difference.X = Math.Abs(difference.X);
            difference.Y = Math.Abs(difference.Y);
            do
            {
                action(lastCell);
                lastCell.X = IncrementDueToDirecion(lastCell.X, location.X);
                lastCell.Y = IncrementDueToDirecion(lastCell.Y, location.Y);
                difference--;
            }
            while (difference.X >= 0 || difference.Y >= 0);
            lastCell = location;
        }

        private int IncrementDueToDirecion(int startValue, int endValue)
        {
            int difference = endValue - startValue;
            if(difference == 0)
            {
                return startValue;
            }
            return startValue + difference / Math.Abs(difference);
        }
    }
}
