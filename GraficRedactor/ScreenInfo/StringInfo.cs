using Core;
using System.Text.RegularExpressions;

namespace GraficRedactor
{
    internal class StringInfo
    {
        public Cell Coordinates { get; set; }

        public string Text { get; set; }

        public string Name;

        public StringInfo(string name, string text, Cell cell)
        {
            Name = name;
            Text = text;
            Coordinates = new Cell(cell);
        }

        public StringInfo(string name, string text, int x, int y)
        {
            Name = name;
            Text = text;
            Coordinates = new Cell(x, y);
        }

        public override string ToString()
        {
            return $"Name:{Name};Coordinates:{Coordinates.ToString()};Text:{Text};";
        }

        public static StringInfo Parse(string input)
        {
            Regex filter = new Regex(@"Name:(?<Name>.+);Coordinates:(?<Coordinates>X:\d+;Y:\d+;)Text:(?<Text>.+);");
            var match = filter.Matches(input).First();

            string name = match.Groups["Name"].Value;
            string coordinates = match.Groups["Coordinates"].Value;
            Cell cell = Cell.Parse(coordinates);
            string text = match.Groups["Text"].Value;

            return new StringInfo(name, text, cell);
        }
    }
}
