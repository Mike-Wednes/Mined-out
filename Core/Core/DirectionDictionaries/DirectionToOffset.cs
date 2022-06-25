namespace Core
{
    public static class DirectionToOffset
    {
        private static Dictionary<Direction, Cell> storage = new Dictionary<Direction, Cell>
        {
            {Direction.Up, new Cell(0, -1) },
            {Direction.Down, new Cell(0, 1) },
            {Direction.Right,  new Cell(1, 0) },
            {Direction.Left, new Cell(-1, 0) },
        };

        public static Cell Get(Direction dir)
        {
            return storage[dir];
        }
    }
}
