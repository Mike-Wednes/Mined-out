using System.Runtime.Serialization;

namespace Core
{
    [DataContract]
    public class GraficCell : Cell
    {
        [DataMember]
        public ConsoleColor Color { get; set; }

        [DataMember]
        public ConsoleColor TextColor { get; set; }

        [DataMember]
        public int Delay { get; set; }

        [DataMember]
        public char Text { get; set; }

        public GraficCell() 
            :this(-1, -1, default)
        { }

        public GraficCell(int x, int y)
            : this(x, y, ConsoleColor.Black, ' ', default)
        { }

        public GraficCell(int x, int y, ConsoleColor color)
            :this(x, y, color, ' ', default)
        { }

        public GraficCell(int x, int y, ConsoleColor color, char text, ConsoleColor textColor)
            :this(x, y, color, 0, text, textColor)
        { }

        public GraficCell(int x, int y, ConsoleColor color, int delay)
            :this(x, y, color, delay, ' ', default)
        { }

        public GraficCell(GraficCell cell)
            :this(cell.X, cell.Y, cell.Color, cell.Delay, cell.Text, cell.TextColor)
        { }

        public GraficCell(int x, int y, ConsoleColor color, int delay, char text, ConsoleColor textColor)
        {
            X = x;
            Y = y;
            Delay = delay;
            Color = color;
            Text = text;
            TextColor = textColor;
        }

    }
}
