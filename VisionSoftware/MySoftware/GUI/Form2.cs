﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace MySoftware.GUI
{
    public partial class Form2 : DockContent
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void imageBox_MouseMove(object sender, MouseEventArgs e)
        {
            this.TabText = imageBox.PointToImage(e.Location).X.ToString();
        }
    }
}
