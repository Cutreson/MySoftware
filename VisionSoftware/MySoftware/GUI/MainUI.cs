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
            Form2 f1 = new Form2();         
            f1.Show(dockPanel, DockState.DockLeftAutoHide);
            f1.Text = "Hide";
            f1.TabText = "Tab hide";
            f1.BackColor = Color.Green;
            Form1 f3 = new Form1();
            f3.Show(dockPanel, DockState.DockRight);
            MainView mainView = new MainView();
            mainView.Show(dockPanel);
        }
    }
}
