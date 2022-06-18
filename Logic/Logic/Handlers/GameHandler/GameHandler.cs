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
                return Move(key);
            }
            else if (typeof(Core.KeysGroups.MarkKeys).DoesEnumContainKey(key))
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

        private bool Move(ConsoleKeyInfo key)
        {
            LogicCell stepped = new LogicCell();
            switch (key.Key)
            {
                case ConsoleKey.RightArrow:
                    _interface.Move(field.MovePlayer(Direction.Right, out stepped), field, fieldOffset);
                    break;
                case ConsoleKey.LeftArrow:
                    _interface.Move(field.MovePlayer(Direction.Left, out stepped), field, fieldOffset);
                    break;
                case ConsoleKey.UpArrow:
                    _interface.Move(field.MovePlayer(Direction.Up, out stepped), field, fieldOffset);
                    break;
                case ConsoleKey.DownArrow:
                    _interface.Move(field.MovePlayer(Direction.Down, out stepped), field, fieldOffset);
                    break;
            }
            if (!CheckCellType(stepped))
            {
                return true;
            }
            return false;
        }

        private void Mark(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.W:
                    _interface.DisplayCell(field.MarkCell(Direction.Up), field, fieldOffset);
                    break;
                case ConsoleKey.D:
                    _interface.DisplayCell(field.MarkCell(Direction.Right), field, fieldOffset);
                    break;
                case ConsoleKey.A:
                    _interface.DisplayCell(field.MarkCell(Direction.Left), field, fieldOffset);
                    break;
                case ConsoleKey.S:
                    _interface.DisplayCell(field.MarkCell(Direction.Down), field, fieldOffset);
                    break;
            }
        }

        private bool CheckCellType(LogicCell cell)
        {
            switch (cell.Type)
            {
                case CellType.Mine:
                    EndByExplosion();
                    return true;
                case CellType.Finish:
                    EndByFinish();
                    return true;
                default:
                    return false;
            }
        }

        private void EndByExplosion()
        {
            Cell adaptedCoordinates = field.GetPlayer();
            adaptedCoordinates.X = adaptedCoordinates.X + fieldOffset.X - 1;
            adaptedCoordinates.Y = adaptedCoordinates.Y + fieldOffset.Y - 1;
            _interface.PrintGrafic("Explosion", adaptedCoordinates);
            ChangeViewMode(CellType.Mine, CellView.Visible);
            Thread.Sleep(2000);
            _interface.Clear();
            _interface.PrintGrafic("GameOver", new Cell(23, 5));
            Thread.Sleep(1000);
        }

        private void EndByFinish()
        {
            ChangeViewMode(CellType.Mine, CellView.Visible);
            Thread.Sleep(2000);
            _interface.Clear();
            _interface.PrintGrafic("Finish", new Cell(23, 5));
            Thread.Sleep(1000);
        }

        private void ChangeViewMode(CellType type, CellView view)
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
