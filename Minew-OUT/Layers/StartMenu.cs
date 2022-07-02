using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minew_OUT.Layers
{
    public partial class StartMenu : UserControl
    {
        private MainForm mainForm;

        public StartMenu(MainForm main)
        {
            InitializeComponent();
            mainForm = main;
        }

        private void StartMenu_Load(object sender, EventArgs e)
        {

        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            mainForm.ChangeLayer(new Game(mainForm));
        }
    }
}
