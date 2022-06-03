using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace Core
{
    [DataContract]
    public class Cell
    {
        [DataMember]
        public int X { get; set; }

        [DataMember]
        public int Y { get; set; }

        public Cell() {}

        public Cell(Cell cell)
            :this(cell.X, cell.Y)
        {}

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

        public override string ToString()
        {
            return $"X:{X};Y:{Y}";
        }

        public static Cell Parse(string input)
        {
            Regex filter = new Regex(@"X:(?<X>\d+);Y:(?<Y>\d+);");
            var match = filter.Matches(input).First();
            int x = int.Parse(match.Groups["X"].Value);
            int y = int.Parse(match.Groups["Y"].Value);
            return new Cell(x, y);
        }
    }
}
