using Core;

namespace GraficRedactor
{
    internal class Palette
    {
        private GraficCell[,] paletteTable;

        private List<GraficCell> paletteList;

        public int Rows { get; set; }

        public int Cols { get; set; }

        public Cell OffSet { get; set; }

        public bool IsDisplayed { get; set; }

        public Cell LastPosition { get; set; }

        public Palette(Cell offsetInput)
        {
            Rows = 4;
            Cols = 4;
            IsDisplayed = false;
            LastPosition = new Cell(offsetInput);
            OffSet = new Cell(offsetInput);
            string path = @"../../../../../GraficRedactor/Palette/paletteTable.json";
            paletteList = Redactor.GetCollection(path);
            paletteTable = new GraficCell[Rows, Cols];
            PlaceListInArray();
        }

        private void PlaceListInArray()
        {
            int k = 0;
            for(int j = 0; j < Rows; j++)
            {
                for(int i = 0; i < Cols; i++)
                {
                    paletteTable[j, i] = paletteList[k];
                    k++;
                }
            }
        }

        internal ConsoleColor GetColor(Cell cell)
        {
            Cell adaptedCell = SubstractOffset(cell);
            GraficCell searchedCell = paletteTable[adaptedCell.Y, adaptedCell.X];
            LastPosition.EqualizeCoordinates(cell);
            return searchedCell.Color;            
        }

        private Cell SubstractOffset(Cell cell)
        {
            Cell newCell = new Cell(cell);
            newCell.X -= OffSet.X;
            newCell.Y -= OffSet.Y;
            return newCell;
        }

        internal List<GraficCell> GetPaletteList()
        {
            return paletteList;
        }
    }
}
