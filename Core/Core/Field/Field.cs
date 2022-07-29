namespace Core
{
    public class Field
    {
        private LogicCell[,] logicCells;

        private PlayerCell player;

        public int Width { get; set; }

        public int Height { get; set; }

        public int Difficulty { get; set; }

        public Field(LogicCell[,] cells, PlayerCell player)
        {
            logicCells = cells;
            Height = cells.GetLength(0);
            Width = cells.GetLength(1);
            this.player = player;
            SetPlayer();
        }

        public Field(int size, int difficulty)
        {
            Width = 17 + size * 4;
            Height = 17 + size * 4;
            Difficulty = difficulty;
            logicCells = new LogicCell[Height, Width];
            player = new PlayerCell((Width - 1) / 2, Height - 1);
            GenerateField();
        }

        private void GenerateField()
        {
            MakeHorizontalWall(typeof(FinishSpaceCell), 0);
            MakeVerticalWall(0);
            MakeVerticalWall(Width - 1);
            MakeHorizontalWall(typeof(BasicSpaceCell), Height - 1);
            AddSpace();
            GenerateWay();
            AddMines();
            SetPlayer();
        }

        public LogicCell GetPlayer()
        {
            return new PlayerCell(player);
        }

        private void SetPlayer()
        {
            player.MinesAround = MinesAroundPlayer();          
        }

        private void MakeVerticalWall(int x)
        {
            for (int j = 1; j < Height - 1; j++)
            {
                logicCells[j, x] = new BorderCell(x, j);
            }
        }

        private void MakeHorizontalWall(Type spaceCellType, int y)
        {
            for (int i = 0; i < Width; i++)
            {
                if (i >= (Width - 3) / 2 && i <= (Width + 1) / 2)
                {
                    logicCells[y, i] = LogicCell.CreateCell(spaceCellType, i, y);
                }
                else
                {
                    logicCells[y, i] = new BorderCell(i, y);
                }
            }
        }

        private void AddSpace()
        {
            for (int j = 1; j < Height - 1; j++)
            {
                for (int i = 1; i < Width - 1; i++)
                {
                    logicCells[j, i] = new BasicSpaceCell(i, j);
                }
            }
        }

        private void AddMines()
        {
            int minesAmount = (Width - 2) * (Height - 2) / (5 - Difficulty);
            for (int i = 0; i < minesAmount; i++)
            {
                SetProperMine();
            }
        }

        private void SetProperMine()
        {
            Random rnd = new Random();
            Cell cell;
            do
            {
                cell = new Cell(rnd.Next(1, Width - 1), rnd.Next(1, Height - 1));
            }
            while (logicCells[cell.Y, cell.X] is MineCell
                || logicCells[cell.Y, cell.X] is WaySpaceCell);
            LogicCell.MapToType(ref logicCells[cell.Y, cell.X], typeof(MineCell));
        }

        public LogicCell[,] GetCells()
        {
            return logicCells;
        }

        public LogicCell GetCell(Cell position)
        {
            return logicCells[position.Y, position.X];
        }

        public List<LogicCell> GetMovePairCell(Cell offset)
        {
            CurrentPositionVisited();
            List<LogicCell> moveLine = new List<LogicCell>()
            {
                logicCells[player.Y, player.X],
                player
            };
            MovePlayer(offset);
            return moveLine;
        }

        private void MovePlayer(Cell offset)
        {
            player.MakeEqual(GetChangedPosition(player, offset));
            logicCells[player.Y, player.X].IsMarked = false;
            player.MinesAround = MinesAroundPlayer();
        }

        public List<LogicCell> ExplodeCells(Cell location)
        {
            List<LogicCell> cellList = new List<LogicCell>();
            for (int j = location.Y - 1; j <= location.Y + 1; j++)
            {
                for (int i = location.X - 1; i <= location.X + 1; i++)
                {
                    if ((j != location.Y || i != location.X) && IsInField(new Cell(i, j)))
                    {
                        LogicCell.MapToType(ref logicCells[j, i], typeof(BasicSpaceCell));
                        cellList.Add(logicCells[j, i]);
                    }        
                }
            }
            player.MinesAround = MinesAroundPlayer() - 1;
            cellList.Add(player);
            return cellList;
        }

        public void ChangeType(Cell cell, Type type)
        {
            LogicCell.MapToType(ref logicCells[cell.Y, cell.X], type);
        }

        private void CurrentPositionVisited()
        {
            LogicCell.MapToType(ref logicCells[player.Y, player.X], typeof(BasicSpaceCell));
            logicCells[player.Y, player.X].IsVisited = true;
            logicCells[player.Y, player.X].IsMarked = false;
        }

        public Cell GetChangedPosition(Cell cellPosition, Cell offset)
        {
            if (CheckForMove(new Cell(cellPosition + offset)))
            {
                return new Cell(cellPosition + offset);
            }
            return new Cell(cellPosition);
        }

        private bool CheckForMove(Cell cell)
        {
            return IsInField(cell) && IsPassable(cell);
        }

        private bool IsInField(Cell cell)
        {
            return (cell.X < this.Width && cell.X >= 0 && cell.Y < this.Height && cell.Y >= 0);
        }

        private bool IsPassable(Cell cell)
        {
            return !(logicCells[cell.Y, cell.X] is IImpassable);
        }

        public int MinesAroundPlayer()
        {
            int count = 0;
            for (int j = player.Y - 1; j < player.Y + 2; j++)
            {
                for (int i = player.X - 1; i < player.X + 2; i++)
                {
                    if (i < this.Width && j < this.Height && i > 0 && j > 0)
                    {
                        if (logicCells[j, i] is MineCell)
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }

        private void GenerateWay(List<LogicCell>? stepHistory = null)
        {
            if (stepHistory == null)
            {
                stepHistory = new List<LogicCell>();
                stepHistory.Add(player);
                GenerateWay(stepHistory);
                return;
            }
            if (IsNearFinish(stepHistory.Last()))
            {
                return;
            }

            var properDirection = FindProperDirection(stepHistory);
            if (properDirection == null)
            {
                ClearWay();
                GenerateWay();
                return;
            }

            var offset = DirectionToOffset.Get((Direction)properDirection);
            WaySpaceCell newPosition = new WaySpaceCell(GetChangedPosition(stepHistory.Last(), offset));
            stepHistory.Add(newPosition);
            LogicCell.MapToType(ref logicCells[newPosition.Y, newPosition.X], typeof(WaySpaceCell));
            GenerateWay(stepHistory);   
        }

        private bool IsNearFinish(Cell cell)
        {
            return logicCells[cell.Y - 1, cell.X] is IFinish;
        }

        private Direction? FindProperDirection(List<LogicCell> stepHistory)
        {
            var deadDirections = new List<Direction>();
            while(deadDirections.Count < 4)
            {
                var aliveDirection = GetAliveDirection(deadDirections);
                if (IsProperDirection(aliveDirection, stepHistory))
                {
                    return aliveDirection;
                }
                deadDirections.Add(aliveDirection);
            }
            return null;
        }

        private bool IsProperDirection(Direction dir, List<LogicCell> stepHistory)
        {
            Cell newPosition = new Cell(GetChangedPosition(stepHistory.Last(), DirectionToOffset.Get(dir)));

            if (WayCellCount(newPosition) > 1
                || stepHistory.Last().Equals(newPosition)
                || IsDirectedFromExit(stepHistory, newPosition))
            {
                return false;
            }
            return true;
        }

        private Direction GetAliveDirection(List<Direction> deadDirections)
        {
            Random rnd = new Random();
            rnd.Next();

            Direction dir;
            do
            {
                dir = (Direction)rnd.Next(0, 4);
            }
            while (deadDirections.Contains(dir));
            return dir;
        }

        private void ClearWay()
        {
            foreach (LogicCell cell in logicCells)
            {
                if (cell is WaySpaceCell)
                {
                    LogicCell.MapToType(ref logicCells[cell.Y, cell.X], typeof(BasicSpaceCell));
                }
            }
        }

        private bool IsDirectedFromExit(List<LogicCell> stepHistory, Cell newPosition)
        {
            if (stepHistory.Last().Y == 2 && (stepHistory.Last().X >= (Width - 3) / 2 && stepHistory.Last().X <= (Width + 1) / 2))
            {
                if (newPosition.X >= (Width - 3) / 2 && newPosition.X <= (Width + 1) / 2 && stepHistory.Last().Y < newPosition.Y)
                {
                    if ((stepHistory.Last().X == (Width - 3) / 2 || stepHistory.Last().X <= (Width + 1) / 2) && stepHistory[^2].X == (Width - 1) / 2)
                    {
                        return true;
                    }
                }
                if (newPosition.Y == 2 && (newPosition.X < (Width - 3) / 2 || newPosition.X > (Width + 1) / 2))
                {
                    return true;
                }
            }
            return false;
        }

        private int WayCellCount(Cell cell)
        {            
            int count = 0;

            for (int j = cell.Y - 1; j < cell.Y + 2; j++)
            {
                if (IsInField(new Cell(cell.X, j)))
                {
                    if (logicCells[j, cell.X] is WaySpaceCell)
                    {
                        count++;
                    }
                }
            }
            for (int i = cell.X - 1; i < cell.X + 2; i++)
            {
                if (IsInField(new Cell(i, cell.Y)))
                {
                    if (logicCells[cell.Y, i] is WaySpaceCell)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public List<LogicCell> ReturnChangedCell(Type type, CellView view)
        {
            var cells = new List<LogicCell>();
            foreach (LogicCell cell in logicCells)
            {
                if (cell.GetType() == type)
                {
                    cell.View = view;
                    cells.Add(cell);
                }
            }
            return cells;
        }

        public LogicCell MarkCell(Cell offset)
        {
            Cell marked = GetChangedPosition(logicCells[player.Y, player.X], offset);
            if (!marked.Equals(player))
            {
                logicCells[marked.Y, marked.X].IsMarked = true;
            }
            return logicCells[marked.Y, marked.X];
        }
    }
}
