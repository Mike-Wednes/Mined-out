using Core;

namespace WinFormsUI
{
    internal class AddingTool : EdditingTool
    {
        public Type cellType { get; set; }

        public AddingTool(Type cellType)
        {
            this.cellType = cellType;
        }

        public override LogicCell? GetEddited(Cell cell)
        {
            var addingCell = LogicCell.CreateCell(cellType, cell.X, cell.Y);
            addingCell.View = CellView.Visible;
            return addingCell;
        }
    }
}
