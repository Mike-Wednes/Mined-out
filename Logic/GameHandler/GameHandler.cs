using Core;

namespace Logic
{
    public class GameHandler
    {
        private Field field;

        private Action<LogicCell> displaying;

        public GameHandler(Action<LogicCell> displaying)
        {
            this.displaying = displaying;
            Settings settings = new Settings();
            field = new Field(settings.FieldSize, settings.DifficultyLevel);
        }

        public void MovePlayer(Direction direction)
        {
            var moveLine = field.GetMoveLine(DirectionToOffset.Get(direction));
            foreach(var logicCell in moveLine)
            {
                displaying(logicCell);
            }
        }

        public LogicCell GetSteppedCell()
        {
            return field.GetCell(field.GetPlayer());
            
        }

        public void Mark(Direction direction)
        {
            var markedCell = field.MarkCell(DirectionToOffset.Get(direction));
            displaying(markedCell);
        }

        public void ChangeViewMode(Type type, CellView view)
        {
            var changedCells = field.ReturnChangedCell(type, view);
            foreach(var cell in changedCells)
            {
                displaying(cell);
            }
            
        }

        private object locker = new();

        public void DisplayField()
        {
            LogicCell[,] cells = field.GetCells();
            foreach (LogicCell cell in cells)
            {
                displaying(cell);
            }
            displaying(field.GetPlayer());
        }

        public Cell GetFieldSize()
        {
            return new Cell(field.Weidth, field.Height);
        }
    }
}
