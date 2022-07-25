using Core;

namespace ConsoleUI
{
    internal static class KeyStorage
    {
        internal static Dictionary<ConsoleKey, Direction> ArrowToDirection = new Dictionary<ConsoleKey, Direction>()
        {
            {ConsoleKey.UpArrow, Direction.Up},
            {ConsoleKey.DownArrow, Direction.Down},
            {ConsoleKey.RightArrow, Direction.Right},
            {ConsoleKey.LeftArrow, Direction.Left},
        };

        internal static Dictionary<ConsoleKey, Direction> KeyToDirection = new Dictionary<ConsoleKey, Direction>()
        {
            {ConsoleKey.W, Direction.Up},
            {ConsoleKey.S, Direction.Down},
            {ConsoleKey.D, Direction.Right},
            {ConsoleKey.A, Direction.Left},
        };
    }
}
