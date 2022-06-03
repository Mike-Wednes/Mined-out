using Core;
using System.Runtime.Serialization.Json;
using System.Reflection;
using System.Linq;

namespace GraficRedactor
{
    public class Redactor
    {
        private static string graficRootPath = @"../../../../../Grafics/";

        private static string ScreenTextInfoPath = @"../../../../../GraficRedactor/ScreenInfo/StringLog.txt";

        private string path = "";

        private Cell cursor;

        private List<GraficCell> currentCollection;

        private RedactMode mode = RedactMode.General;

        public Redactor()
        {
            cursor = new Cell(0, 0);
            currentCollection = new List<GraficCell>();
        }

        public void Start()
        {            
            path = RecordAndGetPath();
            if (!File.Exists(path))
            {
                if (CheckFileCreatingNeed() == false) 
                {
                    Start();
                }
            }
            else
            {
                currentCollection = GetCollection();
            }
            RedactingProcess();            
        }

        private Cell ChangedPosition(Cell cell, Cell offset)
        {
            if (CheckForMove(new Cell { X = cell.X + offset.X, Y = cell.Y + offset.Y }))
            {
                return new Cell { X = cell.X + offset.X, Y = cell.Y + offset.Y };
            }
            return cell;
        }

        private bool CheckForMove(Cell cell)
        {
            if (cell.X < 0 || cell.Y < 0)
            {
                return false;
            }
            return true;
        }

        private void MoveCursor(Direction dir)
        {
            var changedPos = ChangedPosition(cursor, GetCellOffset(dir));
            cursor.EqualizeCoordinates(changedPos);
            GraficCell? stepedCell = GetCellWithCoordinates(changedPos);
            ResetCellDatafields();
            if (stepedCell != null)
            {
                DisplayCellData(stepedCell);
            }
            Console.SetCursorPosition(changedPos.X, changedPos.Y);
        }

        private void ResetCellDatafields()
        {
            DisplayText("            ", StringInfos.TextDataCoordinates);
            DisplayText("            ", new Cell(StringInfos.ColorDataCoordinates.X - 1, StringInfos.ColorDataCoordinates.Y));
            DisplayText("            ", StringInfos.TextColorDataCoordinates);
            DisplayText("            ", StringInfos.DelayDataCoordinates);
        }

        private GraficCell? GetCellWithCoordinates(Cell cell)
        {
            foreach(GraficCell graficCell in currentCollection)
            {
                if (graficCell.Equals(cell))
                {
                    return graficCell;
                }
            }
            return null;
        }

        private void DisplayCellData(GraficCell cell)
        {
            DisplayText(cell.Text.ToString(), StringInfos.TextDataCoordinates);
            DisplayText(cell.Color.ToString(), StringInfos.ColorDataCoordinates);
            DisplayCell(new GraficCell(StringInfos.ColorDataCoordinates.X - 1, StringInfos.ColorDataCoordinates.Y, cell.Color));
            DisplayText(cell.TextColor.ToString(), StringInfos.TextColorDataCoordinates);
            DisplayText(cell.Delay.ToString(), StringInfos.DelayDataCoordinates);
        }

        private Cell GetCellOffset(Direction dir)
        {
            switch (dir)
            {
                case Direction.Right:
                    return new Cell { X = 1, Y = 0 };
                case Direction.Left:
                    return new Cell { X = -1, Y = 0 };
                case Direction.Up:
                    return new Cell { X = 0, Y = -1 };
                default:
                    return new Cell { X = 0, Y = 1 };
            }
        }

        private void RedactingProcess()
        {
            Console.Clear();
            DisplayHints();
            DisplayCollection();
            while (ListenKeys()) { }

        }

        private bool ListenKeys()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            if(mode == RedactMode.General)
            {
                HandleKeyGeneralMode(key);
                return true;
            }
            return false;
        }

        private void HandleKeyGeneralMode(ConsoleKeyInfo key)
        {
            if (typeof(GraficRedactor.KeysGroups.MoveKeys).DoesEnumContainKey(key))
            {
                Move(key);
            }
            else if (typeof(GraficRedactor.KeysGroups.ChooseModeKeys).DoesEnumContainKey(key))
            {
                ChangeMode(key);
            }
        }

        private void ChangeMode(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.D1:
                    mode = RedactMode.ColorMode;
                    break;
                case ConsoleKey.D2:
                    mode = RedactMode.TextMode;
                    break;
                case ConsoleKey.D3:
                    mode = RedactMode.TextColorMode;
                    break;
                case ConsoleKey.D4:
                    mode = RedactMode.DelayMode;
                    break;
            }
        }

        private void Move(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.RightArrow:
                    MoveCursor(Direction.Right);
                    break;
                case ConsoleKey.LeftArrow:
                    MoveCursor(Direction.Left);
                    break;
                case ConsoleKey.UpArrow:
                    MoveCursor(Direction.Up);
                    break;
                case ConsoleKey.DownArrow:
                    MoveCursor(Direction.Down);
                    break;
            }
        }



        private void DisplayCollection()
        {
            foreach(GraficCell cell in currentCollection)
            {
                DisplayCell(cell);
            }
        }

        private void DisplayCollectionAnimation()
        {
            foreach (GraficCell cell in currentCollection)
            {
                DisplayCell(cell);
                Thread.Sleep(cell.Delay);
            }
        }

        private void DisplayCell(GraficCell cell)
        {
            Console.BackgroundColor = cell.Color;
            Console.ForegroundColor = cell.TextColor;
            Console.SetCursorPosition(cell.X, cell.Y);
            Console.Write(cell.Text);
            Console.SetCursorPosition(cursor.X, cursor.Y);
            ResetConsoleColors();
        }

        private void ResetConsoleColors()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private void DisplayText(string text, int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine(text);
            Console.SetCursorPosition(cursor.X, cursor.Y);
        }

        private void DisplayText(string text, Cell offset)
        {
            Console.SetCursorPosition(offset.X, offset.Y);
            Console.Write(text);
            Console.SetCursorPosition(cursor.X, cursor.Y);
        }

        private void DisplayHints()
        {
            var stringInfos = GetAllStringInfos();
            foreach (StringInfo stringInfo in stringInfos)
            {
                DisplayText(stringInfo.Text, stringInfo.Coordinates);
            }

        }

        private StringInfo[] GetAllStringInfos()
        {
            var strings = GetLinesFromTxt();
            var info = strings.Select(g => StringInfo.Parse(g)).ToArray();
            return info;
        }

        private List<string> GetLinesFromTxt()
        {
            List<string> lines = new List<string>();
            try
            {
                using (var sr = new StreamReader(ScreenTextInfoPath))
                {
                    while (!sr.EndOfStream)
                    {
                        string? line = sr.ReadLine();
                        if (line != null)
                        {
                            lines.Add(line);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return lines;
        }

        private string RecordAndGetPath()
        {
            Console.WriteLine("Enter name:");
            string? name = Console.ReadLine();
            path = graficRootPath + name + ".json";
            return path;
        }

        private bool CheckFileCreatingNeed()
        {
            Console.WriteLine("File doesnt exist. Create new?");
            string? answer = Console.ReadLine();
            if (answer?.ToLower() == "no")
            {
                return false;
            }
            else if (answer == "" || answer?.ToLower() == "yes")
            {
                return true;
            }
            else
            {
                Console.WriteLine("incorrect answer");
                return CheckFileCreatingNeed();
            }
        }

        private List<GraficCell> GetCollection()
        {
            var jsonformatter = new DataContractJsonSerializer(typeof(List<GraficCell>));

            try
            {
                using (var file = new FileStream(path, FileMode.OpenOrCreate))
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

        private void Record(List<GraficCell> collection)
        {
            var jsonformatter = new DataContractJsonSerializer(typeof(List<GraficCell>));
            try
            {
                using (var file = new FileStream(path, FileMode.OpenOrCreate))
                {
                    jsonformatter.WriteObject(file, currentCollection);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


    }
}
