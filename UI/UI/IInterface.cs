using Core;

namespace UI
{
    public interface IInterface
    {
        public int GetFieldSize(string linearSize);

        public void Move(List<LogicCell> vector, Field field, Cell fieldOffset);

        public void DisplayCell(LogicCell cell, Field field, Cell fieldOffset);

        public void DisplayCellsByType(CellType type, Field field, Cell fieldOffset);

        public void PrintGrafic(string name, Cell offset);

        public void Clear();
    }
}
