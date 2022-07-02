namespace Core
{
    public static class KeyArrowToOffset
    {
        private static Dictionary<ConsoleKey, Cell> storage = new Dictionary<ConsoleKey, Cell>
        {
            {ConsoleKey.UpArrow, new Cell(0, -1) },
            {ConsoleKey.DownArrow, new Cell(0, 1) },
            {ConsoleKey.RightArrow, new Cell(1, 0) },
            {ConsoleKey.LeftArrow, new Cell(-1, 0) },
            {ConsoleKey.W, new Cell(0, -1) },
            {ConsoleKey.S, new Cell(0, 1) },
            {ConsoleKey.D, new Cell(1, 0) },
            {ConsoleKey.A, new Cell(-1, 0) },
        };

        public static Cell Get(ConsoleKey key)
        {
            return storage[key];
        }
    }
}
