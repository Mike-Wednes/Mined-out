namespace ConsoleUI
{
    internal abstract class ButtonMenu : Menu
    {
        protected ButtonDisplayer displayer;

        protected int cursorLevel;

        public ButtonMenu()
        {
            displayer = new ButtonDisplayer();
            cursorLevel = 1;
        }

        private void ChangeButton(ModeType type, int change)
        {
            cursorLevel += change;
            displayer.DisplayButtons(type, cursorLevel);
        }

        protected void ChangeCursorLevel(ModeType modeType, int min, int max, ConsoleKey? key)
        {
            switch (key)
            {
                case ConsoleKey.DownArrow:
                    if (cursorLevel < max)
                    {
                        ChangeButton(modeType, 1);
                    }
                    break;
                case ConsoleKey.UpArrow:
                    if (cursorLevel > min)
                    {
                        ChangeButton(modeType, -1);
                    }
                    break;
            }
        }

        protected override void StartDisplaying()
        {
            displayer.Clear();
            displayer.DisplayButtons(ThisMode, cursorLevel);
        }
    }
}
