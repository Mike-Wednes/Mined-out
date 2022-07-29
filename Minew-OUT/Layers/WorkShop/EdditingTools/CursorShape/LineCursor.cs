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
            bool isThisStart = false;
            if (lastCell == null)
            {
                lastCell = location;
                isThisStart = true;
            }
            doInLine(action, lastCell, location);
            if (!isThisStart)
            {
                lastCell = null;
            }
        }

        protected int IncrementDueToDirecion(int startValue, int endValue)
        {
            int difference = endValue - startValue;
            if (difference == 0)
            {
                return startValue;
            }
            return startValue + difference / Math.Abs(difference);
        }

        protected void doInLine(Action<Cell> action, Cell start, Cell end)
        {
            Cell difference = end - start;
            difference.X = Math.Abs(difference.X);
            difference.Y = Math.Abs(difference.Y);
            do
            {
                action(start);
                start.X = IncrementDueToDirecion(start.X, end.X);
                start.Y = IncrementDueToDirecion(start.Y, end.Y);
                difference--;
            }
            while (difference.X >= 0 || difference.Y >= 0);
        }
    }
}
