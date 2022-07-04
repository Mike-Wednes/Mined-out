using Core;

namespace ConsoleUI
{
    internal class ButtonDisplayer : MenuDisplayer
    {
        internal void DisplayButtons(ModeType type, int cursorLevel)
        {
            DisplayElements(type, ElementType.Button, ElementTag.Unselected);
            DisplayElement(type, "Button" + cursorLevel + "Selected");
        }
    }
}
