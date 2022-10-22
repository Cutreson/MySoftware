using System;
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
    public partial class MainUI : Form
    {
        public MainUI()
        {
            InitializeComponent();
            Form2 f2 = new Form2();
            f2.Show(dockPanel, DockState.DockLeft);
            Form1 f3 = new Form1();
            f3.Show(dockPanel, DockState.DockRight);
        }
    }
}
