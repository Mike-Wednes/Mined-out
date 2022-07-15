using Core;

namespace WinFormsUI
{
    public class WorkShopCellConverter : CellConverter
    {
        public override Bitmap Convert(LogicCell cell)
        {
            var visibility = cell.View;
            cell.View = CellView.Visible;
            var image = base.Convert(cell);
            cell.View = visibility;
            if (visibility == CellView.Invisible)
            {
                addInvisibilityMark(image);
            }
            return image;
        }

        private void addInvisibilityMark(Image image)
        {
            Graphics imageGraphics = Graphics.FromImage(image);
            float side = image.Size.Width;
            imageGraphics.DrawEllipse(new Pen(Color.CadetBlue, side / 10), 0, 0, side, side);
        }
    }
}
