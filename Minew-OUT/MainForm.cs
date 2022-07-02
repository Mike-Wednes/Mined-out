using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minew_OUT
{
    public partial class MainForm : Form
    {
        private int currentLayerIndex;

        public MainForm()
        {
            InitializeComponent();
            KeyPreview = true;
            this.MaximumSize = new Size(1200, 900);
            this.MinimumSize = this.MaximumSize;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ChangeLayer(new Layers.StartMenu(this));
        }

        public void ChangeLayer(UserControl layer)
        {
            this.Controls.Add(layer);
            if (this.Controls.Count > 1)
            {
                this.Controls.RemoveAt(this.Controls.Count - 2);
            }
        }
    }
}
