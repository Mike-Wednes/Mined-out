namespace Core
{
    public class GraficCell : Cell
    {
        public ConsoleColor Color { get; set; }

        public ConsoleColor TextColor { get; set; }

        public int Delay { get; set; }

        public char Text { get; set; }

        public GraficCell(int x, int y, ConsoleColor color)
        {
            X = x;
            Y = y;
            Delay = 0;
            Color = color;
            Text = ' ';
            TextColor = ConsoleColor.Gray;
        }

        public GraficCell(int x, int y, ConsoleColor color, char text, ConsoleColor textColor)
        {
            X = x;
            Y = y;
            Delay = 0;
            Color = color;
            Text = text;
            TextColor = textColor;
        }

        public GraficCell(int x, int y, ConsoleColor color, int delay)
        {
            X = x;
            Y = y;
            Delay = delay;
            Color = color;
            Text = ' ';
            TextColor = ConsoleColor.Gray;
        }

        public GraficCell(int x, int y, ConsoleColor color, int delay,  char text, ConsoleColor textColor)
        {
            X = x;
            Y = y;
            Delay = delay;
            Color = color;
            Text = text;
            TextColor = TextColor;
        }

    }
}
