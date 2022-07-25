using Core;

namespace ConsoleUI
{
    internal class StartMenu : ButtonMenu
    {
        public override ModeType ThisMode { get { return ModeType.Start; } }


        protected override ModeType? KeyRespond(ConsoleKey key)
        {
            ChangeCursorLevel(ThisMode, 1, 3, key);
            if (key == ConsoleKey.Enter)
            {
                return (ModeType)cursorLevel;
            }
            if (key == ConsoleKey.Escape)
            {
                return null;
            }
            return ThisMode;
        }
    }
}
