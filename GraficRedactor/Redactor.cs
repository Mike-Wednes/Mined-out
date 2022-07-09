using Core;
using System.Runtime.Serialization.Json;
using System.Reflection;
using System.Linq;

namespace GraficRedactor
{
    internal class Redactor
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

        internal Palette paletteColor;

        internal Palette paletteTextColor;

        internal Redactor()
        {
            LabelsInfo = GetAllStringInfos(LabelsInfoPath);
            DataPlaceInfo = GetAllStringInfos(DataPlaceInfoPath);
        }

        internal void RecordPath()
        {
            Console.Clear();
            Console.WriteLine("Enter name:");
            string? name = Console.ReadLine();
            if (name != "")
            {
                path = graficRootPath + name + ".json";
            }
            else
            {
                path = null;
            }
        }

        internal bool CheckPath()
        {
            if (path == null)
            {
                return false;
            }
            if (File.Exists(path))
            {
                currentCollection = GetCollection(path);
            }
            else
            {
                if (!CreatingNeed())
                {
                    Console.Clear();
                    RecordPath();
                }
            }
            return true;
        }

        internal void SetDefault()
        {
            path = "";
            cursor = new Cell(0, 0);
            currentEditingCell = new GraficCell();
            paletteColor = new Palette(new Cell(140, 8),
                GetCollection(@"AdditionalElements/Palette/paletteTable.json"));
            paletteTextColor = new Palette(new Cell(140, 8),
                GetCollection(@"AdditionalElements/Palette/paletteTable.json"));
            currentCollection = new List<GraficCell>();
        }

       internal void ClearAndPrintStandart()
        {
            Console.Clear();
            DisplayLabelsByTag("PL");
            DisplayCollection(currentCollection);
        }

        internal string GetRootPath()
        {
            return graficRootPath;
        }

        private Cell ChangedPosition(Cell cell, Cell offset)
        {
            if (CheckForMove(new Cell (cell + offset)))
            {
                return new Cell(cell + offset);
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

        private void MoveCursor(Cell Offset)
        {
            var changedPos = ChangedPosition(cursor, Offset);
            cursor.MakeEqual(changedPos);
            GraficCell? stepedCell = GetCell(changedPos);
            ResetDatafields();
            if (stepedCell != null)
            {
                DisplayCellData(stepedCell);
            }
            Console.SetCursorPosition(changedPos.X, changedPos.Y);
        }

        private void ResetDatafields()
        {
            foreach(StringInfo info in DataPlaceInfo)
            {
                Cell coordinates = new Cell(info.Coordinates);
                coordinates.X--;
                DisplayText("                ", coordinates);
            }
        }

        private GraficCell? GetCell(Cell coordinates)
        {
            foreach(GraficCell graficCell in currentCollection)
            {
                if (graficCell.Equals(coordinates))
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

        internal void ClearEnteringArea()
        {
            for(int i = 0; i < paletteColor.Rows; i++)
            {
                DisplayText("                          ", paletteColor.OffSet.X, paletteColor.OffSet.Y + i);
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
            if (!currentEditingCell.Equals(new GraficCell()))
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

        internal void ChangeProperty(string changedProperty, object? change)
        {
            typeof(GraficCell).GetProperty(changedProperty)?.SetValue(currentEditingCell, change);
            currentEditingCell.MakeEqual(cursorOutMenus);
            DisplayCell(currentEditingCell);
            ReturnToStandart(ClearEnteringArea);
        }

        internal void DisplayPalette(Palette palette)
        {
            ClearEnteringArea();
            DisplayCollection(palette.GetPaletteList(), palette.OffSet);
            cursorOutMenus = new Cell(cursor);
            cursor.MakeEqual(palette.LastPosition);
        }

        internal void ReturnToStandart(Action clearing, bool returnCursor = true)
        {
            clearing();
            if (returnCursor)
            {
                cursor.MakeEqual(cursorOutMenus);
            }
            DisplayCellIfEditing();
            Console.SetCursorPosition(cursor.X, cursor.Y);
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
            if (start == null)
            {
                return;
            }
            Cell finish = new Cell(cursor);
            if (start.Equals(finish))
            {
                return;
            }
            string differentProperty = GetDifferentProperty(start, finish);
            if (differentProperty == "both")
            {
                return;
            }
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
            if (start == null)
            {
                return;
            }
            Cell finish = new Cell(cursor);
            if (start.Equals(finish))
            {
                return;
            }
            string differentProperty = GetDifferentProperty(start, finish);
            if (differentProperty == "both")
            {
                return;
            }
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

        internal void PrintLabel(string name)
        {
            var label = LabelsInfo.Where(g => g.Name == name).First();
            DisplayText(label.Text, label.Coordinates);
        }

        internal void Move(ConsoleKey key, bool doEnterCheck = false)
        {
            MoveCursor(KeyArrowToOffset.Get(key));
            if (doEnterCheck)
            {
                EnterCellIfEditing();
            }
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

        private bool CreatingNeed()
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
                return CreatingNeed();
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
