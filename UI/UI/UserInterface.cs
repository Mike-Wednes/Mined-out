using Core;
using System;
using System.Threading;

namespace UI
{
    public class UserInterface : IInterface
    {
        private readonly Cell fieldOffset = new Cell { X = 40, Y = 6 };

        public void DisplayField(Field field)
        {
            //char[,] table = RecordField(field);
            LogicCell[,] cells = field.GetCells();
            foreach(LogicCell cell in cells)
            {
                DisplayCell(cell, field, fieldOffset);
            }
        }

        /*
        private char[,] RecordField(Field field)
        {
            char[,] table = new char[field.Y, field.X];
            List<Cell> cells = field.GetCells();
            foreach(Cell cell in cells)
            {
                table[cell.Y, cell.X] = IdentifyCell(cell, field);
            }
            return table;
        }*/

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
                    return field.MinesAroundCount(cell).ToString()[0];             //need to update
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

        public void DisplayCell(LogicCell cell, Field field)
        {
            DisplayCell(cell, field, fieldOffset);
        }

        public void DisplayCell(LogicCell cell, Field field, Cell offset)
        {
            Console.SetCursorPosition(cell.X + offset.X, cell.Y + offset.Y);
            WriteColored(IdentifyCell(cell, field).ToString(), IdentifyCellBackGround(cell, field), IdentifyCellForeGround(cell, field));
            Console.SetCursorPosition(0, field.Y + fieldOffset.Y);
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

        public void Move(List<LogicCell> vector, Field field)
        {
            DisplayCell(vector[0], field, fieldOffset);
            DisplayCell(vector[1], field, fieldOffset);
        }

        public void DisplayKeys()
        {
            Console.WriteLine("Use arrows to move");
            Console.WriteLine("Use WASD to mark mines");
        }

        public int GetFieldSize(string linearSize)
        {
            if (linearSize.ToLower() == "x")
            {
                Console.Write("Enter a field's length: ");
            }
            else
            {
                Console.Write("Enter a field's hight: ");
            }
            int.TryParse(Console.ReadLine(), out int size);
            if (IsOdd(size) && size >= 7 && size <= 27)
            {
                return size;
            }
            else if (size < 7)
            {
                Console.WriteLine("Enter a number bigger than 6");
                return GetFieldSize(linearSize);
            }
            else if (size > 27)
            {
                Console.WriteLine("Enter a number smaller than 27");
                return GetFieldSize(linearSize);
            }
            else
            {
                Console.WriteLine("Enter an odd numbers");
                return GetFieldSize(linearSize);
            }
        }

        private bool IsOdd(int size)
        {
            return size % 2 == 1;
        }

        public void GameOver()
        {
            PrintGrafic(GraficCollection.GameOver, new Cell(50, 10));
        }

        private void PrintGrafic(List<GraficCell> grafic, Cell offset)
        {
            Console.Clear();
            foreach(GraficCell cell in grafic)
            {
                Console.SetCursorPosition(cell.X + offset.X, cell.Y + offset.Y);
                WriteColored(cell.Text.ToString(), cell.Color, cell.TextColor);
                Console.SetCursorPosition(0, 20 + fieldOffset.Y);
                Thread.Sleep(cell.Delay);
            }
        }

        public void Success()
        {
            Console.WriteLine();
            Console.WriteLine("\t\t\t\t\t\tSUCCESS");
        }
    }
}
