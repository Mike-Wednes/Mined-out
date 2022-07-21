using Core;

namespace WinFormsUI
{
    public abstract class CursorShape
    {
        public abstract void DoWithClick(Action<Cell> action, Cell location);
    }
}
