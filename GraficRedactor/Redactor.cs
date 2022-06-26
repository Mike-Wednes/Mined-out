using Core;
using System.Runtime.Serialization.Json;
using System.Reflection;
using System.Linq;

namespace GraficRedactor
{
    public class Redactor
    {
        internal static string graficRootPath = @"Grafics/";

        private static string LabelsInfoPath = @"AdditionalElements/ScreenInfo/LabelsInfo.txt";

        private static string DataPlaceInfoPath = @"AdditionalElements/ScreenInfo/DataPlaceInfo.txt";

        private StringInfo[] LabelsInfo;

        private StringInfo[] DataPlaceInfo;

        private string? path;

        internal Cell cursor;

        private Cell cursorOutMenus;

        internal List<GraficCell> currentCollection;

        internal GraficCell currentEditingCell;

        private RedactMode mode;

        private KeyHandler keyHandler;

        internal Palette paletteColor;

        internal Palette paletteTextColor;

        public Redactor()
        {
            LabelsInfo = GetAllStringInfos(LabelsInfoPath);
            DataPlaceInfo = GetAllStringInfos(DataPlaceInfoPath);
        }

        private void SetDefault()
        {
            path = "";
            keyHandler = new KeyHandler(this);
            mode = RedactMode.GeneralMode;
            cursor = new Cell(0, 0);
            currentEditingCell = new GraficCell();
            paletteColor = new Palette(new Cell(140, 8), 
                GetCollection(@"AdditionalElements/Palette/paletteTable.json"));
            paletteTextColor = new Palette(new Cell(140, 8), 
                GetCollection(@"AdditionalElements/Palette/paletteTable.json"));
            currentCollection = new List<GraficCell>();
        }

        public void Start()
        {
            SetDefault();
            path = RecordAndGetPath();
            if (path != null)
            {
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
        }

        internal string GetRootPath()
        {
            return graficRootPath;
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
            Console.SetWindowSize(100, 25);
        }

        internal void ClearAndPrintStandart()
        {
            Console.Clear();
            DisplayLabelsByTag("PL");
            DisplayCollection(currentCollection);
        }

        internal void ClearEnteringArea()
        {
            for(int i = 0; i < paletteColor.Rows; i++)
            {
                DisplayText("                          ", paletteColor.OffSet.X, paletteColor.OffSet.Y + i);
            }
        }

        private bool ListenKeys()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            keyHandler.GetType().GetMethod(mode.ToString())?.Invoke(keyHandler, new object[] { key });
            if(mode == RedactMode.ClosingMode)
            {
                key = Console.ReadKey(true);
                keyHandler.GetType().GetMethod(mode.ToString())?.Invoke(keyHandler, new object[] { key });
                return false;
            }
            else
            {
                return true;
            }
        }

        internal string? GetValueFromUser(string labelName)
        {
            cursorOutMenus = new Cell(cursor);
            var textHint = LabelsInfo.Where(g => g.Name == labelName).First();
            Cell coordintes = new Cell(textHint.Coordinates.X + textHint.Text.Length + 2, textHint.Coordinates.Y);
            Console.SetCursorPosition(coordintes.X, coordintes.Y);
            string? text = Console.ReadLine();
            return text;
        }

        private void EnterCellIfEditing()
        {
            if (!currentEditingCell.Equals(new GraficCell()) && mode.Equals(RedactMode.GeneralMode))
            {
                EnterCell();
            }
        }

        internal void EnterCell()
        {
            currentCollection.Add(currentEditingCell);
            currentEditingCell = new GraficCell();
            ReturnToStandart(ClearEnteringArea, false);
        }

        internal void SaveAndDisplayCellChanges(string changedProperty, object? change)
        {
            typeof(GraficCell).GetProperty(changedProperty)?.SetValue(currentEditingCell, change);
            currentEditingCell.EqualizeCoordinates(cursorOutMenus);
            DisplayCell(currentEditingCell);
            ReturnToStandart(ClearEnteringArea);
        }

        internal void DisplayPaletteIfNeeded(Palette palette)
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

        internal void ReturnToStandart(Action clearing, bool returnCursor = true)
        {
            SetPalettesNotDisplayed();
            mode = RedactMode.GeneralMode;
            clearing();
            if (returnCursor)
            {
                cursor.EqualizeCoordinates(cursorOutMenus);
            }
            DisplayCellIfEditing();
            Console.SetCursorPosition(cursor.X, cursor.Y);
            keyHandler.GeneralMode(null);
        }

        private void DisplayCellIfEditing()
        {
            if (!currentEditingCell.Equals(new GraficCell()))
            {
                DisplayCell(currentEditingCell);
            }
        }

        internal void AddLine()
        {
            GraficCell start = new GraficCell(currentCollection.Last());
            if (start != null)
            {
                Cell finish = new Cell(cursor);
                string differentProperty = GetDifferentProperty(start, finish);
                if (differentProperty != "both")
                {
                    int startValue = GetCellPropValue(differentProperty, start);
                    int endValue = GetCellPropValue(differentProperty, finish);
                    int added = 0;
                    int length = Math.Abs(startValue - endValue);
                    IncrementDoToDirecion(ref startValue, endValue);
                    do
                    {
                        GraficCell newCell = new GraficCell(start);
                        typeof(GraficCell).GetProperty(differentProperty)?.SetValue(newCell, startValue);
                        currentCollection.Add(newCell);
                        DisplayCell(newCell);
                        IncrementDoToDirecion(ref startValue, endValue);
                        added++;
                    }
                    while (added != length);
                }
            }
        }

        internal void Delete()
        {
            Delete(cursor);
        }

        internal void Delete(Cell cell, bool doClear = true)
        {
            var index = currentCollection.FindLastIndex(g => g.Equals(cell));
            if (index != -1)
            {
                currentCollection.RemoveAt(index);
                if (doClear)
                {
                    ClearAndPrintStandart();
                }
            }
        }

        internal void DeleteLine()
        {
            GraficCell start = new GraficCell(currentCollection.Last());
            if (start != null)
            {
                Cell finish = new Cell(cursor);
                if (!start.Equals(finish))
                {
                    string differentProperty = GetDifferentProperty(start, finish);
                    if (differentProperty != "both")
                    {
                        int startValue = GetCellPropValue(differentProperty, start);
                        int endValue = GetCellPropValue(differentProperty, finish);
                        int changed = 0;
                        int length = Math.Abs(startValue - endValue);
                        do
                        {
                            GraficCell cell = new GraficCell(start);
                            typeof(GraficCell).GetProperty(differentProperty)?.SetValue(cell, startValue);
                            Delete(cell, false);
                            IncrementDoToDirecion(ref startValue, endValue);
                            changed++;
                        }
                        while (changed != length);

                    }
                }
            }
            ClearAndPrintStandart();
        }

        internal void IncrementDoToDirecion(ref int startValue, int endValue)
        {
            if (Math.Max(startValue, endValue) == endValue)
            {
                startValue++;
            }
            else
            {
                startValue--;
            }
        }

        internal void DisplayLabelsByTag(string tag)
        {
            var labels = GetLabelsByTag(tag);
            foreach (var label in labels)
            {
                DisplayText(label.Text, label.Coordinates);
            }
        }

        private int GetCellPropValue(string propName, object cell)
        {
            var value = cell.GetType().GetProperty(propName)?.GetValue(cell);
            if (value != null)
            {
                return (int)value;
            }
            else
            {
                throw new Exception("Didn't find");
            }
        }

        private string GetDifferentProperty(Cell one, Cell second)
        {
            if (one.X == second.X)
            {
                return "Y";
            }
            else if (one.Y == second.Y)
            {
                return "X";
            }
            return "both";
        }

        private void SetPalettesNotDisplayed()
        {
            paletteColor.IsDisplayed = false;
            paletteTextColor.IsDisplayed = false;
        }

        internal void PrintLabel(string name)
        {
            var label = LabelsInfo.Where(g => g.Name == name).First();
            DisplayText(label.Text, label.Coordinates);
        }

        internal void ChangeMode(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.D1:
                    mode = RedactMode.ColorMode;
                    keyHandler.ColorMode(null);
                    break;
                case ConsoleKey.D2:
                    mode = RedactMode.TextMode;
                    keyHandler.TextMode(null);
                    break;
                case ConsoleKey.D3:
                    mode = RedactMode.TextColorMode;
                    keyHandler.TextColorMode(null);
                    break;
                case ConsoleKey.D4:
                    mode = RedactMode.DelayMode;
                    keyHandler.DelayMode(null);
                    break;
                case ConsoleKey.Escape:
                    mode = RedactMode.ClosingMode;
                    keyHandler.ClosingMode(null);
                    break;
                case ConsoleKey.H:
                    mode = RedactMode.HotKeysMode;
                    keyHandler.HotKeysMode(null);
                    break;
            }
        }

        internal void Move(ConsoleKeyInfo key)
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
            EnterCellIfEditing();
        }

        internal void DisplayCollection(List<GraficCell> collection, Cell? offset = null)
        {
            foreach(GraficCell cell in collection)
            {
                DisplayCell(cell, offset);
            }
        }

        internal void DisplayCollectionAnimation(List<GraficCell> collection)
        {
            foreach (GraficCell cell in collection)
            {
                DisplayCell(cell);
                Thread.Sleep(cell.Delay);
            }
        }

        internal void DisplayCell(GraficCell cell, Cell? offset = null)
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

        internal void DisplayText(string text, Cell offset)
        {
            Console.SetCursorPosition(offset.X, offset.Y);
            Console.Write(text);
            Console.SetCursorPosition(cursor.X, cursor.Y);
        }

        internal StringInfo[] GetLabelsByTag(string tag)
        {
            var stringInfos = LabelsInfo.Where(g => g.Name.Contains(tag + "_")).ToArray();
            return stringInfos;
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

        private string? RecordAndGetPath()
        {
            Console.Clear();
            Console.WriteLine("Enter name:");
            string? name = Console.ReadLine();
            if (name != "")
            {
                path = graficRootPath + name + ".json";
                return path;
            }
            else
            {
                return null;
            }
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

        internal List<GraficCell> GetCollection(string path)
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
