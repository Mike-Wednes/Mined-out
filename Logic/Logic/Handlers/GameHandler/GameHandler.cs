using Core;
using UI;

namespace Logic
{
    internal class GameHandler
    {
        private readonly Cell fieldOffset = new Cell { X = 38, Y = 6 };

        private Field field;

        private readonly IInterface _interface;

        public GameHandler(IInterface UI)
        {
            _interface = UI;
        }

        public void Start()
        {
            Settings settings = new Settings();
            field = new Field(settings.FieldSize, settings.DifficultyLevel);
            _interface.Clear();
            DisplayField();
            while (ListenKeys()) { }
        }

        private bool ListenKeys()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (typeof(Core.KeysGroups.MoveKeys).DoesEnumContainKey(key))
            {
                return MovePlayer(key);
            }
            if (typeof(Core.KeysGroups.MarkKeys).DoesEnumContainKey(key))
            {
                Mark(key);
            }
            /*case ConsoleKey.D1:
                    ChangeViewMode(CellType.Mine, CellView.Visible);
            break;*/
            return true;
        }

        public void DisplayField()
        {
            Settings settings = new Settings();
            fieldOffset.Y += 2 - settings.FieldSize * 2;
            fieldOffset.X += 1 - settings.FieldSize * 1;
            LogicCell[,] cells = field.GetCells();
            foreach (LogicCell cell in cells)
            {
                _interface.DisplayCell(cell, field, fieldOffset);
            }
        }

        private bool MovePlayer(ConsoleKeyInfo key)
        {
            var steppedCell = field.GetChangedPosition(field.GetPlayer(), KeyArrowToOffset.Get(key.Key));
            var afterStepMethod = GetAfterStepMethod(steppedCell);

            var moveLine = field.GetMoveLine(KeyArrowToOffset.Get(key.Key));
            foreach(var logicCell in moveLine)
            {
                _interface.DisplayCell(logicCell, field, fieldOffset);
            }

            return DoesContinue(afterStepMethod);
        }

        private bool DoesContinue(Action? afterStepMethod)
        {
            if (afterStepMethod == null)
            {
                return true;
            }
            else
            {
                afterStepMethod?.Invoke();
                return false;
            }
        }

        private Action? GetAfterStepMethod(Cell steppedCell)
        {
            var  steppedCellType = GetTypeByCoordinations(steppedCell);
            if (steppedCellType == typeof(FinishSpaceCell))
            {
                return new Action(EndByFinish);
            }
            if (steppedCellType == typeof(MineCell))
            {
                return new Action(EndByExplosion);
            }
            return null;
        }

        private Type GetTypeByCoordinations(Cell cell)
        {
            return field.GetCells()[cell.Y, cell.X].GetType();
        }

        private void Mark(ConsoleKeyInfo key)
        {
            _interface.DisplayCell(field.MarkCell(KeyArrowToOffset.Get(key.Key)), field, fieldOffset);
        }

        private void EndByExplosion()
        {
            Cell adaptedCoordinates = field.GetPlayer();
            adaptedCoordinates.X = adaptedCoordinates.X + fieldOffset.X - 1;
            adaptedCoordinates.Y = adaptedCoordinates.Y + fieldOffset.Y - 1;
            _interface.PrintGrafic("Explosion", adaptedCoordinates);
            ChangeViewMode(typeof(MineCell), CellView.Visible);
            Thread.Sleep(2000);
            _interface.Clear();
            _interface.PrintGrafic("GameOver", new Cell(23, 5));
            Thread.Sleep(1000);
        }

        private void EndByFinish()
        {
            ChangeViewMode(typeof(MineCell), CellView.Visible);
            Thread.Sleep(2000);
            _interface.Clear();
            _interface.PrintGrafic("Finish", new Cell(23, 5));
            Thread.Sleep(1000);
        }

        private void ChangeViewMode(Type type, CellView view)
        {
            field.ChangeViewMode(type, view);
            _interface.DisplayCellsByType(type, field, fieldOffset);
        }

        private void DisplayField(Field field)
        {
            LogicCell[,] cells = field.GetCells();
            foreach (LogicCell cell in cells)
            {
                _interface.DisplayCell(cell, field, fieldOffset);
            }
        }
    }
}
