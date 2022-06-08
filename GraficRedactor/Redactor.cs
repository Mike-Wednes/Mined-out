using Core;
using System.Runtime.Serialization.Json;
using System.Reflection;
using System.Linq;

namespace GraficRedactor
{
    public class Redactor
    {
        private static string graficRootPath = @"../../../../../Grafics/";

        private static string LabelsInfoPath = @"../../../../../GraficRedactor/ScreenInfo/LabelsInfo.txt";

        private static string DataPlaceInfoPath = @"../../../../../GraficRedactor/ScreenInfo/DataPlaceInfo.txt";

        private StringInfo[] LabelsInfo;

        private StringInfo[] DataPlaceInfo;

        private string path = "";

        private Cell cursor;

        private Cell cursorOutMenus;

        private List<GraficCell> currentCollection;

        private GraficCell currentEdditingCell;

        private RedactMode mode = RedactMode.General;

        private Palette palette;

        public Redactor()
        {
            cursor = new Cell(0, 0);
            currentEdditingCell = new GraficCell();
            palette = new Palette(new Cell(90, 8));
            currentCollection = new List<GraficCell>();
            LabelsInfo = GetAllStringInfos(LabelsInfoPath);
            DataPlaceInfo = GetAllStringInfos(DataPlaceInfoPath);
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
                currentCollection = GetCollection(path);
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
            foreach(StringInfo info in DataPlaceInfo)
            {
                Cell coordinates = new Cell(info.Coordinates);
                coordinates.X--;
                DisplayText("                ", coordinates);
            }
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
            foreach(StringInfo info in DataPlaceInfo)
            {
                Cell coordinates = info.Coordinates;
                string dataName = info.Name;
                string value = GetPropertyValue(cell, dataName);
                DisplayText(value, coordinates);   
                if(dataName == "Color")
                {
                    GraficCell cellColoredCopy = new GraficCell(coordinates.X - 1, coordinates.Y, cell.Color);
                    DisplayCell(cellColoredCopy);
                }
            }

        }

        private string GetPropertyValue(Cell cell, string propertyName)
        {
            string? propertyValue = cell.GetType().GetProperty(propertyName)?.GetValue(cell)?.ToString();
            if (propertyValue != null)
            {
                return propertyValue;
            }
            else
            {
                return "ERROR while getting propertyValue";
            }
            
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
            Console.SetWindowSize(200, 50);
            ClearAndPrintStandart();
            while (ListenKeys()) { }

        }

        private void ClearAndPrintStandart()
        {
            Console.Clear();
            DisplayHints();
            DisplayCollection(currentCollection);
        }

        private void ClearEnteringArea()
        {
            for(int i = 0; i < palette.Rows; i++)
            {
                DisplayText("                          ", palette.OffSet.X, palette.OffSet.Y + i);
            }
        }

        private bool ListenKeys()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            if(mode == RedactMode.General)
            {
                HandleKeyGeneralMode(key);
                return true;
            }
            if (mode == RedactMode.ColorMode)
            {
                HandleKeyColorMode(key);
                return true;
            }
            return false;
        }

        private void HandleKeyColorMode(ConsoleKeyInfo? key)
        {
            DisplayPaletteIfNeeded();
            if (key != null)
            {
                ConsoleKeyInfo keyConverted = (ConsoleKeyInfo)key;
                CheckKeyDoMove(keyConverted);
                CheckKeyEnterColor(keyConverted);
                CheckKeyDoEscapeColor(keyConverted);
            }
        }

        private void CheckKeyEnterColor(ConsoleKeyInfo key)
        {
            if (IsNeedEntering(key))
            {
                if (IsCursorAtPalette())
                {
                    SaveAndDisplayCellChanges("Color", palette.GetColor(cursor));
                    ReturnToStandart(ClearEnteringArea);
                }
            }
        }

        private bool IsCursorAtPalette()
        {
            if(cursor.X >= palette.OffSet.X && cursor.X < palette.OffSet.X + palette.Cols)
            {
                if (cursor.Y >= palette.OffSet.Y && cursor.Y < palette.OffSet.Y + palette.Rows)
                {
                    return true;
                }
            }
            return false;
        }

        private void CheckKeyEnterCell(ConsoleKeyInfo key)
        {
            if (IsNeedEntering(key))
            {
                EnterCell();
            }
        }

        private void EnterCellIfEdditing()
        {
            if (!currentEdditingCell.Equals(new GraficCell()))
            {
                EnterCell();
            }
        }

        private void EnterCell()
        {
            currentCollection.Add(currentEdditingCell);
            currentEdditingCell = new GraficCell();
            ReturnToStandart(ClearEnteringArea);
        }

        private void CheckKeyDoCloseRedacting(ConsoleKeyInfo key)
        {
            if (IsNeedLeaving(key))
            {
                Record(currentCollection);
                var label = LabelsInfo.Where(g => g.Name == "SaveCollectionLabel").First();
                DisplayText(label.Text, label.Coordinates);
            }
        }

        private void SaveAndDisplayCellChanges(string changedProperty, object? change)
        {
            typeof(GraficCell).GetProperty(changedProperty)?.SetValue(currentEdditingCell, change);
            currentEdditingCell.EqualizeCoordinates(cursorOutMenus);
            DisplayCell(currentEdditingCell);
        }

        private void DisplayPaletteIfNeeded()
        {
            if (!palette.IsDisplayed)
            {
                ClearEnteringArea();
                DisplayCollection(palette.GetPaletteList(), palette.OffSet);
                palette.IsDisplayed = true;
                cursorOutMenus = new Cell(cursor);
                cursor.EqualizeCoordinates(palette.LastPosition);
            }
        }

        private void CheckKeyDoEscapeColor(ConsoleKeyInfo key)
        {
            if (IsNeedLeaving(key))
            {
                ReturnToStandart(ClearAndPrintStandart);
            }
        }

        private void ReturnToStandart(Action clearing)
        {
            SetMenuNotDisplayed();
            mode = RedactMode.General;
            clearing();
            cursor.EqualizeCoordinates(cursorOutMenus);
            DisplayCellIfEdditing();
            Console.SetCursorPosition(cursor.X, cursor.Y);
            HandleKeyGeneralMode(null);
        }

        private void DisplayCellIfEdditing()
        {
            if (!currentEdditingCell.Equals(new GraficCell()))
            {
                DisplayCell(currentEdditingCell);
            }
        }

        private void SetMenuNotDisplayed()
        {
            palette.IsDisplayed = false;
        }

        private void HandleKeyGeneralMode(ConsoleKeyInfo? key)
        {
            if(!currentEdditingCell.Equals(new GraficCell()))
            {
                var enteringHintLabel = LabelsInfo.Where(g => g.Name == "AddCellLabel").First();
                DisplayText(enteringHintLabel.Text, enteringHintLabel.Coordinates);
            }
            if (key != null)
            {
                ConsoleKeyInfo keyConverted = (ConsoleKeyInfo)key;
                CheckKeyDoMove(keyConverted);
                CheckKeyChangeMode(keyConverted);
                CheckKeyEnterCell(keyConverted);
                CheckKeyDoCloseRedacting(keyConverted);
            }
        }

        private void HandleClosingMode(ConsoleKeyInfo key)
        {

        }

        private void CheckKeyDoMove(ConsoleKeyInfo key)
        {
            if (typeof(GraficRedactor.KeysGroups.MoveKeys).DoesEnumContainKey(key))
            {
                Move(key);
            }
        }

        private void CheckKeyChangeMode(ConsoleKeyInfo key)
        {
            if (typeof(GraficRedactor.KeysGroups.ChooseModeKeys).DoesEnumContainKey(key))
            {
                ChangeMode(key);
            }
        }

        private bool IsNeedEntering(ConsoleKeyInfo key) 
            => key.Key == ConsoleKey.Enter;

        private bool IsNeedLeaving(ConsoleKeyInfo key)
            => key.Key == ConsoleKey.Escape;

        private void ChangeMode(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.D1:
                    mode = RedactMode.ColorMode;
                    HandleKeyColorMode(null);
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
                case ConsoleKey.Escape:
                    mode = RedactMode.ClosingMode;
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
            EnterCellIfEdditing();
        }

        private void DisplayCollection(List<GraficCell> collection, Cell? offset = null)
        {
            foreach(GraficCell cell in collection)
            {
                DisplayCell(cell, offset);
            }
        }

        private void DisplayCollectionAnimation(List<GraficCell> collection)
        {
            foreach (GraficCell cell in collection)
            {
                DisplayCell(cell);
                Thread.Sleep(cell.Delay);
            }
        }

        private void DisplayCell(GraficCell cell, Cell? offset = null)
        {
            if (offset == null)
            {
                offset = new Cell();
            }
            Console.BackgroundColor = cell.Color;
            Console.ForegroundColor = cell.TextColor;
            Console.SetCursorPosition(cell.X + offset.X, cell.Y + offset.Y);
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
            var labels = LabelsInfo.Take(7);
            foreach (StringInfo stringInfo in labels)
            {
                DisplayText(stringInfo.Text, stringInfo.Coordinates);
            }

        }

        private StringInfo[] GetAllStringInfos(string path)
        {
            var strings = GetLinesFromTxt(path);
            var info = strings.Where(g => !g.Contains('/')).Select(g => StringInfo.Parse(g)).ToArray();
            return info;
        }

        private List<string> GetLinesFromTxt(string path)
        {
            List<string> lines = new List<string>();
            try
            {
                using (var sr = new StreamReader(path))
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

        internal static List<GraficCell> GetCollection(string path)
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

        public void Record(List<GraficCell> collection)
        {
            var jsonformatter = new DataContractJsonSerializer(typeof(List<GraficCell>));
            try
            {
                FileStream file;
                if (!File.Exists(path))
                {
                    file = new FileStream(path, FileMode.Create);
                }
                else
                {
                    file = new FileStream(path, FileMode.Truncate);
                }
                jsonformatter.WriteObject(file, currentCollection);
                file.Dispose();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


    }
}
