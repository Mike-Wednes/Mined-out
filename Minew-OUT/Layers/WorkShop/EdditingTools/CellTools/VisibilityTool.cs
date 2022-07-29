using Core;

namespace WinFormsUI
{
    internal class VisibilityTool : EdditingTool
    {
        public override LogicCell? GetEdited(Cell cell)
        {
            var logicCell = cell as LogicCell;
            if(logicCell == null)
            {
                return null;
            }
            logicCell.View = reverseView(logicCell.View);
            return logicCell;
            
        }

        private CellView reverseView(CellView view)
        {
            view = view == CellView.Visible
                ? CellView.Invisible
                : CellView.Visible;
            return view;
        }
    }
}

