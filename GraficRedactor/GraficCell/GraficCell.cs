using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Core;

namespace GraficRedactor
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

        public static List<GraficCell> GetCollection(string name)
        {
            name = @"Grafics/" + name + ".json";
            var jsonformatter = new DataContractJsonSerializer(typeof(List<GraficCell>));

            try
            {
                using (var file = new FileStream(name, FileMode.Open))
                {
                    var collection = jsonformatter.ReadObject(file) as List<GraficCell>;
                    if (collection != null)
                    {
                        return collection;
                    }
                    else
                    {
                        Console.WriteLine("ERROR Deserialized as null");
                        return new List<GraficCell>();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<GraficCell>();
            }
        }

    }
}
