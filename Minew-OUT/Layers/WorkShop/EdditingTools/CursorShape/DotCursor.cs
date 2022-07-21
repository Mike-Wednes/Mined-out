using Core;

namespace WinFormsUI
{
    public class DotCursor : CursorShape
    {
        public override void DoWithClick(Action<Cell> action, Cell location)
        {
            action(location);
        }
    }
}
