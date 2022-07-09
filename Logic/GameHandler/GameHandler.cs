using Core;

namespace Logic
{
    public class GameHandler
    {
        private Field field;

        private Action<LogicCell> displayCell;

        public GameHandler(Action<LogicCell> displaying)
        {
            this.displayCell = displaying;
            Settings settings = new Settings();
            field = new Field(settings.FieldSize, settings.DifficultyLevel);
        }

        public void MovePlayer(Direction direction)
        {
            var moveLine = field.GetMovePairCell(DirectionToOffset.Get(direction));
            foreach(var logicCell in moveLine)
            {
                displayCell(logicCell);
            }
        }

        public LogicCell GetCurrentCell()
        {
            return field.GetCell(field.GetPlayer());            
        }

        public void Mark(Direction direction)
        {
            var markedCell = field.MarkCell(DirectionToOffset.Get(direction));
            displayCell(markedCell);
        }

        public void ChangeViewMode(Type type, CellView view)
        {
            var changedCells = field.ReturnChangedCell(type, view);
            foreach(var cell in changedCells)
            {
                displayCell(cell);
            }            
        }

        private object locker = new();

        public void DisplayField()
        {
            LogicCell[,] cells = field.GetCells();
            foreach (LogicCell cell in cells)
            {
                displayCell(cell);
            }
            displayCell(field.GetPlayer());
        }

        public Cell GetFieldSize()
        {
            return new Cell(field.Weidth, field.Height);
        }
    }
}
