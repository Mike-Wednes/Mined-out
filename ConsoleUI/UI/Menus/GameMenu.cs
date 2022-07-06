using Core;
using System.Reflection;
using Logic;

namespace ConsoleUI
{
    internal class GameMenu : Menu
    {
        private ConsoleDisplayer displayer;

        private Cell fieldOffset;

        private GameHandler gameHandler;

        public override ModeType ThisMode { get { return ModeType.Game; } }

        public GameMenu()
        {
            displayer = new ConsoleDisplayer();
        }

        protected override void StartDisplaying()
        {
            Settings settings = new Settings();
            fieldOffset = new Cell { X = 38, Y = 5 };
            fieldOffset.Y += 2 - settings.FieldSize * 2;
            fieldOffset.X += 1 - settings.FieldSize * 1;
            gameHandler = new GameHandler(DisplayCell);
            displayer.Clear();
            gameHandler.DisplayField();
            
        }

        public void DisplayCell(LogicCell cell)
        {
            var mapped = MapToGrafic(cell);
            displayer.DisplayGraficCell(mapped, fieldOffset);
        }

        private GraficCell MapToGrafic(LogicCell cell)
        {
            var consoleType = Assembly.GetExecutingAssembly().GetType("ConsoleUI.Console" + cell.GetType().Name);
            var consoleCell = Activator.CreateInstance(consoleType, cell) as LogicCell;
            if (consoleCell == null)
            {
                return new GraficCell();
            }
            consoleCell.MapProperies(cell);
            return ((IConsoleLogicCell)consoleCell).Identify();
        }

        protected override ModeType? KeyRespond(ConsoleKey key)
        {
            TryMove(key);
            TryMark(key);
            return CheckCurrent();
            
        }

        private void TryMark(ConsoleKey key)
        {
            if (KeyStorage.KeyToDirection.ContainsKey(key))
            {
                gameHandler.Mark(KeyStorage.KeyToDirection[key]); ;
            }
        }

        private void TryMove(ConsoleKey key)
        {
            if (KeyStorage.ArrowToDirection.ContainsKey(key))
            {
                gameHandler.MovePlayer(KeyStorage.ArrowToDirection[key]);;
            }
        }

        private ModeType CheckCurrent()
        {
            var stepped = gameHandler.GetSteppedCell();
            if (stepped.GetType() == typeof(MineCell))
            {
                GameOver();
                return ModeType.Start;
            }
            if (stepped.GetType() == typeof(FinishSpaceCell))
            {
                Finish();
            }
            return ModeType.Game;
        }

        private void GameOver()
        {
            Cell adaptedCoordinates = new Cell(gameHandler.GetSteppedCell());
            adaptedCoordinates.X = adaptedCoordinates.X + fieldOffset.X - 1;
            adaptedCoordinates.Y = adaptedCoordinates.Y + fieldOffset.Y - 1;
            displayer.DisplayGraficCollection("Explosion", adaptedCoordinates);
            gameHandler.ChangeViewMode(typeof(MineCell), CellView.Visible);
            Thread.Sleep(2000);
            displayer.Clear();
            displayer.DisplayGraficCollection("GameOver", new Cell(23, 5));
            Thread.Sleep(1000);
        }

        private void Finish()
        {
            gameHandler.ChangeViewMode(typeof(MineCell), CellView.Visible);
            Thread.Sleep(2000);
            displayer.Clear();
            displayer.DisplayGraficCollection("Finish", new Cell(23, 5));
            Thread.Sleep(1000);
        }
    }
}
