namespace Core
{
    public class Field
    {
        private LogicCell[,] logicCells;

        private PlayerCell player;

        public int Weidth { get; set; }

        public int Height { get; set; }

        public int Difficulty { get; set; }

        public LogicCell GetPlayer()
        {
            return new PlayerCell(player);
        }

        public Field(int size, int difficulty)
        {
            Weidth = 17 + size * 4;
            Height = 17 + size * 4;
            Difficulty = difficulty;
            logicCells = new LogicCell[Height, Weidth];
            player = new PlayerCell((Weidth - 1) / 2, Height - 1);
            GenerateField();
        }

        private void GenerateField()
        {
            MakeGorizontalWall(typeof(FinishSpaceCell), 0);
            MakeSideWall(0);
            MakeSideWall(Weidth - 1);
            MakeGorizontalWall(typeof(BasicSpaceCell), Height - 1);
            AddSpace();
            GenerateWay();
            AddMines();
            SetPlayer();
        }

        private void SetPlayer()
        {
            player.MinesAround = MinesAroundPlayer();          
        }

        private void MakeSideWall(int x)
        {
            for (int j = 1; j < Height - 1; j++)
            {
                logicCells[j, x] = new BorderCell(x, j);
            }
        }

        private void MakeGorizontalWall(Type spaceCellType, int y)
        {
            for (int i = 0; i < Weidth; i++)
            {
                if (i >= (Weidth - 3) / 2 && i <= (Weidth + 1) / 2)
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
                for (int i = 1; i < Weidth - 1; i++)
                {
                    logicCells[j, i] = new BasicSpaceCell(i, j);
                }
            }
        }

        private void AddMines()
        {
            int minesAmount = (Weidth - 2) * (Height - 2) / (5 - Difficulty);
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
                cell = new Cell(rnd.Next(1, Weidth - 1), rnd.Next(1, Height - 1));
            }
            while (logicCells[cell.Y, cell.X].DoesFitType(typeof(MineCell))
                || logicCells[cell.Y, cell.X].DoesFitType(typeof(WaySpaceCell)));
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
            if(IsInField(cell) && IsPassable(cell))
            {
                return true;
            }
            return false;
        }

        private bool IsInField(Cell cell)
        {
            if (cell.X < this.Weidth && cell.Y < this.Height)
            {
                return true;
            }
            return false;
        }

        private bool IsPassable(Cell cell)
        {
            if((logicCells[cell.Y, cell.X] as IImpassable) != null)
            {
                return false;
            }
            return true;
        }

        public int MinesAroundPlayer()
        {
            int count = 0;
            for (int j = player.Y - 1; j < player.Y + 2; j++)
            {
                for (int i = player.X - 1; i < player.X + 2; i++)
                {
                    if (i < this.Weidth && j < this.Height && i > 0 && j > 0)
                    {
                        if (logicCells[j, i].DoesFitType(typeof(MineCell)))
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
            if(logicCells[cell.Y - 1, cell.X] as IFinish != null)
            {
                return true;
            }
            return false;
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
                if (cell.DoesFitType(typeof(WaySpaceCell)))
                {
                    LogicCell.MapToType(ref logicCells[cell.Y, cell.X], typeof(BasicSpaceCell));
                }
            }
        }

        private bool IsDirectedFromExit(List<LogicCell> stepHistory, Cell newPosition)
        {
            if (stepHistory.Last().Y == 2 && (stepHistory.Last().X >= (Weidth - 3) / 2 && stepHistory.Last().X <= (Weidth + 1) / 2))
            {
                if (newPosition.X >= (Weidth - 3) / 2 && newPosition.X <= (Weidth + 1) / 2 && stepHistory.Last().Y < newPosition.Y)
                {
                    if ((stepHistory.Last().X == (Weidth - 3) / 2 || stepHistory.Last().X <= (Weidth + 1) / 2) && stepHistory[^2].X == (Weidth - 1) / 2)
                    {
                        return true;
                    }
                }
                if (newPosition.Y == 2 && (newPosition.X < (Weidth - 3) / 2 || newPosition.X > (Weidth + 1) / 2))
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
                    if (logicCells[j, cell.X].DoesFitType(typeof(WaySpaceCell)))
                    {
                        count++;
                    }
                }
            }
            for (int i = cell.X - 1; i < cell.X + 2; i++)
            {
                if (IsInField(new Cell(i, cell.Y)))
                {
                    if (logicCells[cell.Y, i].DoesFitType(typeof(WaySpaceCell)))
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
                if (cell.DoesFitType(type))
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
