using Core;

namespace GraficRedactor
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

        internal static Dictionary<ConsoleKey, RedactMode> KeyToMode = new Dictionary<ConsoleKey, RedactMode>()
        {
            {ConsoleKey.Escape, RedactMode.ClosingMode},
            {ConsoleKey.D1, RedactMode.ColorMode},
            {ConsoleKey.D2, RedactMode.TextMode},
            {ConsoleKey.D3, RedactMode.TextColorMode},
            {ConsoleKey.D4, RedactMode.DelayMode},
            {ConsoleKey.H, RedactMode.HotKeysMode},
        };
    }
}
