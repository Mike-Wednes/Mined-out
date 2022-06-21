using Core;
using System;
using System.Threading;
using System.Runtime.Serialization.Json;

namespace UI
{
    public class UserInterface : IInterface
    {
        private char IdentifyCell(LogicCell cell, Field field)
        {   
            if (cell.View == CellView.Invisible)
            {
                return ' ';
            }
            switch (cell.Type)
            {
                case CellType.Border:
                    return ' ';
                case CellType.Space:
                    return ' ';
                case CellType.Player:
                    return field.MinesAroundCount(cell).ToString()[0];
                case CellType.OriginalWay:
                    return 'O';
                case CellType.Mine:
                    return 'x';
                case CellType.Finish:
                    return ' ';
                default:
                    return '?';
            }
        }

        private ConsoleColor IdentifyCellForeGround(LogicCell cell, Field field)
        {
            switch (cell.Type)
            {
                case CellType.Mine:
                    if (cell.IsMarked)
                    {
                        return ConsoleColor.Black;
                    }
                    return ConsoleColor.Red;
                case CellType.Player:
                    return ConsoleColor.Black;
                default:
                    return ConsoleColor.Gray;
            }
        }

        private ConsoleColor IdentifyCellBackGround(LogicCell cell, Field field)
        {
            if (cell.IsMarked == true)
            {
                return ConsoleColor.Red;
            }
            else if (cell.View == CellView.Invisible)
            {
                return ConsoleColor.Black;
            }
            if (cell.IsVisited == true)
            {
                return ConsoleColor.DarkGray;
            }
            switch (cell.Type)
            {
                case CellType.Border:
                    return ConsoleColor.DarkBlue;
                case CellType.Player:
                    return ConsoleColor.DarkGray;
                default:
                    return ConsoleColor.Black;
            }
        }

        public void DisplayCell(LogicCell cell, Field field, Cell fieldOffset)
        {
            Console.SetCursorPosition(cell.X + fieldOffset.X, cell.Y + fieldOffset.Y);
            WriteColored(IdentifyCell(cell, field).ToString(), IdentifyCellBackGround(cell, field), IdentifyCellForeGround(cell, field));
            Console.SetCursorPosition(0, 20);
        }

        private void WriteColored(string str, ConsoleColor backGround, ConsoleColor foreGround, bool doLine = false)
        {
            ConsoleColor currentBackground = Console.BackgroundColor;
            ConsoleColor currentForeground = Console.ForegroundColor;

            Console.BackgroundColor = backGround;
            Console.ForegroundColor = foreGround;

            if (doLine)
            {
                Console.WriteLine(str);
            }
            else
            {
                Console.Write(str);
            }

            Console.BackgroundColor = currentBackground;
            Console.ForegroundColor = currentForeground;
        }

        public void Move(List<LogicCell> vector, Field field, Cell fieldOffset)
        {
            DisplayCell(vector[0], field, fieldOffset);
            DisplayCell(vector[1], field, fieldOffset);
        }

        private bool IsOdd(int size)
        {
            return size % 2 == 1;
        }


        public void DisplayCellsByType(CellType type, Field field, Cell fieldOffset)
        {
            foreach(LogicCell cell in field.GetCells())
            {
                if (cell.Type == type)
                {
                    DisplayCell(cell, field, fieldOffset);
                }
            }
        }

        public void PrintGrafic(string name, Cell offset)
        {
            var grafic = GetCollection(name);
            foreach(GraficCell cell in grafic)
            {
                Console.SetCursorPosition(cell.X + offset.X, cell.Y + offset.Y);
                WriteColored(cell.Text.ToString(), cell.Color, cell.TextColor);
                Console.SetCursorPosition(0, 20);
                Thread.Sleep(cell.Delay);
            }
        }

        public void Clear()
        {
            Console.Clear();
        }

        private List<GraficCell> GetCollection(string name)
        {
            name = @"../../../../../Grafics/" + name + ".json";
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
