namespace Core
{
    public class Field
    {
        private LogicCell[,] LogicCells;

        private LogicCell playerCell;

        public int X { get; set; }

        public int Y { get; set; }

        public int Difficulty { get; set; }

        public LogicCell GetPlayer()
        {
            return new LogicCell(playerCell);
        }

        public Field(int size, int difficulty) 
        {
            X = 17 + size * 4;
            Y = 13 + size * 2;
            Difficulty = difficulty;
            LogicCells = new LogicCell[Y, X];
            playerCell = new LogicCell((X - 1) / 2, Y - 1, CellType.Player);
            GenerateField();
        }

        private void GenerateField()
        {
            MakeGorizontalWall(CellType.Finish, CellView.Invisible);
            MakeSideWall(0);
            MakeSideWall(X - 1);
            MakeGorizontalWall(CellType.Space, CellView.Invisible, Y - 1);
            AddSpace();
            GenerateWay();
            AddMines();
            AddPlayer();
        }

        private void AddPlayer()
        {
            LogicCells[playerCell.Y, playerCell.X] = new LogicCell(playerCell);
        }

        private void MakeSideWall(int x)
        {
            for (int j = 1; j < Y - 1; j++)
            {
                LogicCells[j, x] = new LogicCell(x, j, CellType.Border);
            }
        }

        private void MakeGorizontalWall(CellType spaceType, CellView view, int y = 0)
        {
            for (int i = 0; i < X; i++)
            {
                if (i >= (X - 3) / 2 && i <= (X + 1) / 2)
                {
                    LogicCells[y, i] = new LogicCell(i, y, spaceType, view);
                }
                else
                {
                    LogicCells[y, i] = new LogicCell(i, y, CellType.Border);
                }                
            }
        }

        private void AddSpace()
        {
            for (int j = 1; j < Y - 1; j++)
            {
                for (int i = 1; i < X - 1; i++)
                {
                    LogicCells[j, i] = new LogicCell(i, j, CellType.Space);
                }
            }
        }

        private void AddMines()
        {

            Random rnd = new Random();            
            int minesAmount = (X - 2) * (Y - 2) / (5 - Difficulty);
            for (int i = 0; i < minesAmount; i++)
            {
                LogicCell cell;
                do
                {
                    cell = new LogicCell(rnd.Next(1, X - 1), rnd.Next(1, Y - 1));
                }
                while (cell.Type == CellType.Mine || LogicCells[cell.Y, cell.X].Type == CellType.OriginalWay);
                LogicCells[cell.Y, cell.X].Type = CellType.Mine;
                LogicCells[cell.Y, cell.X].View = CellView.Invisible;
            }
        }

        public LogicCell[,] GetCells()
        {
            return LogicCells;
        }

        public List<LogicCell> MovePlayer(Direction dir, out LogicCell stepped)
        {
            return ChangePlayerPosition(GetCellOffset(dir), out stepped); 
        }

        private List<LogicCell> ChangePlayerPosition(Cell offset, out LogicCell stepped)
        {
            List<LogicCell> vector = new List<LogicCell>();
            var changedPos = ChangedPosition(playerCell, offset);
            stepped = new LogicCell(LogicCells[changedPos.Y, changedPos.X]);

            LogicCells[playerCell.Y, playerCell.X].Type = CellType.Space;
            LogicCells[playerCell.Y, playerCell.X].IsVisited = true;
            LogicCells[playerCell.Y, playerCell.X].View = CellView.Visible;
            vector.Add(LogicCells[playerCell.Y, playerCell.X]);

            if (stepped.Type == CellType.Mine)
            {
                LogicCells[stepped.Y, stepped.X].IsMarked = true;
            }
            else
            {
                LogicCells[stepped.Y, stepped.X].IsMarked = false;
            }
            LogicCells[stepped.Y, stepped.X].Type = CellType.Player;
            LogicCells[stepped.Y, stepped.X].View = CellView.Visible;
            vector.Add(LogicCells[stepped.Y, stepped.X]);

            playerCell.EqualizeCoordinates(stepped);

            return vector;
        }

        private Cell GetCellOffset(Direction dir)
        {
            switch (dir)
            {
                case Direction.Right:
                    return new Cell { X = 1, Y = 0 };
                case Direction.Left:
                    return new Cell { X = -1, Y = 0 };
                case Direction.Up:
                    return new Cell { X = 0, Y = -1 };
                default:
                    return new Cell { X = 0, Y = 1 };
            }
        }

        private LogicCell ChangedPosition(LogicCell cell, Cell offset)
        {
            if (CheckForMove(new LogicCell { X = cell.X + offset.X, Y = cell.Y + offset.Y }))
            {
                return new LogicCell { X = cell.X + offset.X, Y = cell.Y + offset.Y };
            }
            return cell;
        }

        private bool CheckForMove(LogicCell cell)
        {
            if (cell.X >= this.X || cell.Y >= this.Y)
            {
                return false;
            }
            if (LogicCells[cell.Y, cell.X].Type == CellType.Border)
            {
                return false;
            }
            return true;
        }

        public int MinesAroundCount(LogicCell player)
        {
            int count = 0;
            for (int j = player.Y - 1; j < player.Y + 2; j++)
            {
                for (int i = player.X - 1; i < player.X + 2; i++)
                {
                    if (i < this.X && j < this.Y && i > 0 && j > 0)
                    {
                        if (LogicCells[j, i].Type == CellType.Mine)
                        {
                            count++;
                        }
                    }                    
                }
            }
            return count;
        }

        private void GenerateWay(Random? rnd = null, List<LogicCell>? stepHistory = null, List<Direction>? deadDirections = null)
        {
            rnd = rnd == null
                ? new Random()
                :rnd;

            deadDirections = deadDirections == null
                ? new List<Direction>()
                : deadDirections;

            stepHistory = stepHistory == null
                ? new List<LogicCell>()
                : stepHistory;

            if (stepHistory.Count == 0)
            {
                stepHistory.Add(playerCell);
                GenerateWay(rnd, stepHistory, deadDirections);
            }
            else if (stepHistory.Last().Y == 1 && (stepHistory.Last().X >= (X - 3) / 2 && stepHistory.Last().X <= (X + 1) / 2))
            {
            }
            else
            {
                Direction dir = new Direction();
                if (deadDirections.Count == 4)
                {
                    deadDirections.Clear();
                    stepHistory.Clear();
                    ClearWay();
                    GenerateWay(rnd);
                }
                else
                {
                    do
                    {
                        dir = (Direction)rnd.Next(0, 4);
                    }
                    while (deadDirections.Contains(dir));

                    LogicCell newPosition = ChangedPosition(stepHistory.Last(), GetCellOffset(dir));

                    if (WayCellCount(newPosition) <= 1 && !(stepHistory.Last().Equals(newPosition)) && !IsDirectedFromExit(stepHistory, newPosition))
                    {
                        deadDirections.Clear();
                        stepHistory.Add(newPosition);
                        LogicCells[newPosition.Y, newPosition.X].Type = CellType.OriginalWay;
                        LogicCells[newPosition.Y, newPosition.X].View = CellView.Invisible;
                    }
                    else
                    {
                        deadDirections.Add(dir);
                    }
                    GenerateWay(rnd, stepHistory, deadDirections);

                }
            }
        }

        private void ClearWay()
        {
            foreach (LogicCell cell in LogicCells)
            {
                if (cell.Type == CellType.OriginalWay)
                {
                    cell.Type = CellType.Space;
                }
            }
        }

        private bool IsDirectedFromExit(List<LogicCell> stepHistory, LogicCell newPosition)
        {
            if (stepHistory.Last().Y == 2 && (stepHistory.Last().X >= (X - 3) / 2 && stepHistory.Last().X <= (X + 1) / 2))
            {
                if (newPosition.X >= (X - 3) / 2 && newPosition.X <= (X + 1) / 2 && stepHistory.Last().Y < newPosition.Y)
                {
                    if ((stepHistory.Last().X == (X - 3) / 2 || stepHistory.Last().X <= (X + 1) / 2) && stepHistory[^2].X == (X - 1) / 2)
                    {
                        return true;
                    }
                }
                if (newPosition.Y == 2 && (newPosition.X < (X - 3) / 2 || newPosition.X > (X + 1) / 2))
                {
                    return true;
                }
            }
            return false;
        }

        private int WayCellCount(LogicCell cell)
        {            
            int count = 0;
            if (cell.X < this.X && cell.Y < this.Y)
            {
                for (int j = cell.Y - 1; j < cell.Y + 2; j++)
                {
                    if (LogicCells[cell.Y, cell.X].Type == CellType.OriginalWay)
                    {
                        count++;
                    }
                }
                for (int i = cell.X - 1; i < cell.X + 2; i++)
                {
                    if (LogicCells[cell.Y, cell.X].Type == CellType.OriginalWay)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public void ChangeViewMode(CellType type, CellView view)
        {
            foreach (LogicCell cell in LogicCells)
            {
                if (cell.Type == type)
                {
                    cell.View = view;
                }
            }
        }

        public LogicCell MarkCell(Direction dir)
        {
            LogicCell marked = ChangedPosition(LogicCells[playerCell.Y, playerCell.X], GetCellOffset(dir));
            if (!marked.Equals(playerCell))
            {
                LogicCells[marked.Y, marked.X].IsMarked = true;
            }
            return LogicCells[marked.Y, marked.X];
        }
    }
}
