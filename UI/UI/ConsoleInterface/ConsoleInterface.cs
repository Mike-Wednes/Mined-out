using Core;
using System;
using System.Threading;
using System.Runtime.Serialization.Json;
using System.Reflection;

namespace UI
{
    public class ConsoleInterface : IInterface
    {
        public void DisplayCell(LogicCell cell, Field field, Cell fieldOffset)
        {
            Console.SetCursorPosition(cell.X + fieldOffset.X, cell.Y + fieldOffset.Y);
            var graficCell = MapToGrafic(cell, field);
            WriteColored(graficCell.Text.ToString(), graficCell.Color, graficCell.TextColor);
            Console.SetCursorPosition(0, 20);
        }

        private GraficCell MapToGrafic(LogicCell cell, Field field)
        {
            var consoleType = Assembly.GetExecutingAssembly().GetType("UI.Console" + cell.GetType().Name);
            var consoleCell = Activator.CreateInstance(consoleType, cell) as LogicCell;
            if (consoleCell == null)
            {
                return new GraficCell();
            }
            consoleCell.MapProperies(cell);
            return ((IConsoleLogicCell)consoleCell).Identify(field);
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


        public void DisplayCellsByType(Type type, Field field, Cell fieldOffset)
        {
            foreach(LogicCell cell in field.GetCells())
            {
                if (cell.DoesFitType(type))
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
