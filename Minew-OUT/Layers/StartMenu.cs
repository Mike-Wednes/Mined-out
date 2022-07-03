﻿using System;
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
            if (CheckGuideDone())
            {
                mainForm.ChangeLayer(new Game(mainForm));
            }
            else
            {
                WriteGuideDone();
                mainForm.ChangeLayer(new Guide(mainForm));
            }            
        }

        private bool CheckGuideDone()
        {
            using(StreamReader sr = new StreamReader(@"UIEventsSettings\guideDone.txt"))
            {
                if (sr.ReadToEnd() == "1")
                {
                    return true;
                }
            }
            return false;
        }

        private void WriteGuideDone()
        {
            using (StreamWriter sw = new StreamWriter(@"UIEventsSettings\guideDone.txt", false))
            {
                sw.Write("1");
            }
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            mainForm.ChangeLayer(new Layers.Settings(mainForm));
        }
    }
}
