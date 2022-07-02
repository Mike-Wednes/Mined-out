namespace Core
{
    public abstract class LogicCell : Cell
    {
        public CellView View { get; set; }

        public bool IsVisited { get; set; }

        public bool IsMarked { get; set; }

        public LogicCell()
        { }

        public LogicCell(Cell cell)
            :this(cell.X, cell.Y)
        { }

        public LogicCell(int x, int y)
            :this(x, y, false)
        { }

        public LogicCell(int x, int y, bool isVisited)
            : this(x, y, isVisited, false, default)
        { }

        public LogicCell(int x, int y, bool isVisited, bool isMarked)
            : this(x, y, isVisited, isMarked, default)
        { }

        public LogicCell(int x, int y, bool isVisited, bool isMarked, CellView view)
            :base(x, y)
        {
            IsVisited = isVisited;
            IsMarked = isMarked;
            View = view;
        }

        public static LogicCell CreateCell(Type cellType, params object?[] args)
        {
            var cell = Activator.CreateInstance(cellType, args) as LogicCell;
            if (cell == null)
            {
                throw new Exception("cant create cell");
            }
            return cell;
        }

        public bool DoesFitType(Type type)
        {
            if (this.GetType() == type)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void MapToType(ref LogicCell cell, Type type)
        {
            var sameCell = CreateCell(type, cell);
            if(sameCell == null) 
            { 
                return; 
            }
            sameCell.MapProperies(cell);
            cell = sameCell;
        }

        public void MapProperies(LogicCell sample)
        {
            var properties = this.GetType().GetProperties();
            foreach (var property in properties)
            {
                if (sample.GetType().GetProperty(property.Name) != null)
                {
                    this.GetType().GetProperty(property.Name)?.SetValue(this, property.GetValue(sample, null));
                }
            }
        }
    }
}
