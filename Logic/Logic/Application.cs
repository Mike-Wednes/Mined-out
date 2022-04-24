using Core;
using UI;
using System;
using System.Threading;


namespace Logic
{
    internal class Application
    {
        private readonly IInterface _interface;

        private Field field;

        public Application(IInterface ui)
        {
            _interface = ui;
        }

        public void Run()
        {
            field = new Field(_interface.GetFieldSize("x"), _interface.GetFieldSize("y"));
            Console.Clear();
            _interface.DisplayKeys();
            _interface.DisplayField(field);
            while (ListenKeys()) { }
        }


        private void DisplayField()
        {
            _interface.DisplayField(field);
        }

        private void DisplayKeys()
        {
            _interface.DisplayKeys();
        }

        private bool ListenKeys()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (KeyCharacteristic.MoveKeys.Contains(key.Key))
            {
                return Move(key);
            }
            else if (KeyCharacteristic.MarkKeys.Contains(key.Key))
            {
                Mark(key);
            }
            /*case ConsoleKey.D1:
                    ChangeViewMode(CellType.Mine, CellView.Visible);
            break;*/
            return true;

        }

        private bool Move(ConsoleKeyInfo key)
        {
            LogicCell stepped = new LogicCell();
            switch (key.Key)
            {
                case ConsoleKey.RightArrow:
                    _interface.Move(field.MovePlayer(Direction.Right, out stepped), field);
                    break;
                case ConsoleKey.LeftArrow:
                    _interface.Move(field.MovePlayer(Direction.Left, out stepped), field);
                    break;
                case ConsoleKey.UpArrow:
                    _interface.Move(field.MovePlayer(Direction.Up, out stepped), field);
                    break;
                case ConsoleKey.DownArrow:
                    _interface.Move(field.MovePlayer(Direction.Down, out stepped), field);
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
                    _interface.DisplayCell(field.MarkCell(Direction.Up), field);
                    break;
                case ConsoleKey.D:
                    _interface.DisplayCell(field.MarkCell(Direction.Right), field);
                    break;
                case ConsoleKey.A:
                    _interface.DisplayCell(field.MarkCell(Direction.Left), field);
                    break;
                case ConsoleKey.S:
                    _interface.DisplayCell(field.MarkCell(Direction.Down), field);
                    break;
            }
        }

        private bool CheckCellType(LogicCell cell)
        {
            switch (cell.Type)
            {
                case CellType.Mine:
                    ChangeViewMode(CellType.Mine, CellView.Visible);
                    Thread.Sleep(2000);
                    _interface.GameOver();
                    return true;
                case CellType.Finish:
                    _interface.Success();
                    return true;
                default:
                    return false;
            }
        }

        private void ChangeViewMode(CellType type, CellView view)
        {
            field.ChangeViewMode(type, view);
            _interface.DisplayField(field);
        }

    }
}
