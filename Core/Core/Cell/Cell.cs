namespace Core
{
    public class Cell
    {
        public int X { get; set; }

        public int Y { get; set; }

        public Cell() {}

        public Cell(Cell cell)
        {
            this.X = cell.X;
            this.Y = cell.Y;
        }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void EqualizeCoordinates(Cell cell)
        {
            this.X = cell.X;
            this.Y = cell.Y;
        }

        public override bool Equals(object obj)
        {
            Cell cell = (Cell)obj;
            return X == cell.X && Y == cell.Y;
        }
    }
}
