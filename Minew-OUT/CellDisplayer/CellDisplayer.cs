using Core;
using Logic;

namespace WinFormsUI
{
    public class CellDisplayer
    {
        private CellConverter converter;

        public int Scale { get; set; }

        public CellDisplayer()
            :this(default)
        { }

        public CellDisplayer(int scale)
        {
            converter = new CellConverter();
            Scale = scale;
        }

        public void DrawCell(Image fieldImage, LogicCell cell)
        {
            var cellBitmap = converter.Convert(cell);
            DrawPicture(fieldImage, cellBitmap, cell);
        }

        public void DrawPicture(Image fieldImage, Image image, Cell location)
        {
            var fieldImageGraphics = Graphics.FromImage(fieldImage);
            fieldImageGraphics.DrawImage(image, location.X * Scale, location.Y * Scale, Scale, Scale);
        }

        public void DrawGridElement(Image fieldImage, Cell location, bool doFill = false)
        {
            var fieldImageGraphics = Graphics.FromImage(fieldImage);
            if (doFill)
            {
                fieldImageGraphics.FillRectangle(new SolidBrush(Color.Gray), location.X * Scale, location.Y * Scale, Scale, Scale);
            }
            fieldImageGraphics.DrawRectangle(new Pen(Color.White),  location.X * Scale, location.Y * Scale, Scale, Scale);
        }

        public void Clear(Image fieldImage)
        {
            var fieldImageGraphics = Graphics.FromImage(fieldImage);
            fieldImageGraphics.FillRectangle(new SolidBrush(Color.Black), 0, 0, 635, 635);
        }
    }
}
