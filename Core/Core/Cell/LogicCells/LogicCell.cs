using System.Runtime.Serialization;
using System.Reflection;

namespace Core
{
    [DataContract, KnownType(typeof(BorderCell)), KnownType(typeof(BasicSpaceCell)), KnownType(typeof(FinishSpaceCell)), 
        KnownType(typeof(MineCell)), KnownType(typeof(PlayerCell))]
    public abstract class LogicCell : Cell
    {
        [DataMember]
        public CellView View { get; set; }

        [DataMember]
        public bool IsVisited { get; set; }

        [DataMember]
        public bool IsMarked { get; set; }

        public LogicCell()
            :base()
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
            sameCell.MapProperties(cell);
            cell = sameCell;
        }

        public void MapProperties(LogicCell sample)
        {
            var thisProperties = this.GetType().GetProperties();
            var sampleProperties = sample.GetType().GetProperties();
            var intersect = thisProperties.Intersect(sampleProperties);
            foreach (var property in intersect)
            {
                this.GetType().GetProperty(property.Name)?.SetValue(this, property.GetValue(sample, null));
                SetProperty(property, property.GetValue(sample));
            }
        }


        public void SetProperty(PropertyInfo propInfo, object? value)
        {
            this.GetType().GetProperty(propInfo.Name)?.SetValue(this, value);
        }
    }
}
