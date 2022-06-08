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
        {
            Text = ' ';
            X = -1;
            Y = -1;
        }

        public GraficCell(int x, int y, ConsoleColor color)
            :this(x, y, color, ' ', ConsoleColor.Gray)
        {
        }

        public GraficCell(int x, int y, ConsoleColor color, char text, ConsoleColor textColor)
        {
            X = x;
            Y = y;
            Color = color;
            Text = text;
            TextColor = textColor;
        }

        public GraficCell(int x, int y, ConsoleColor color, int delay)
            :this(x, y, color, delay, ' ', ConsoleColor.Gray)
        {}

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
