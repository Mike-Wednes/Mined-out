using Core;

namespace WinFormsUI
{
    public class Highlighter
    {
        public PictureBox box { get; set; }

        public Highlighter(PictureBox box)
        {
            this.box = box;
            box.BackColor = Color.DodgerBlue;
        }

        public void Highlight(Control control)
        {
            control.Parent.Controls.Add(box);
            box.Left = control.Left - 5;
            box.Top = control.Top - 5;
            box.Width = control.Width + 10;
            box.Height = control.Height + 10;
            box.Show();
        }
    }
}
