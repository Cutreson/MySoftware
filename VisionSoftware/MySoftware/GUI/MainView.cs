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
    public partial class MainView : DockContent
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void imageBox_MouseMove(object sender, MouseEventArgs e)
        {
            imageBox.Text = string.Format("Width = {0}, Height = {1}, X = {2}, Y = {3}", imageBox.Image.Width,
                imageBox.Image.Height, imageBox.PointToImage(e.Location).X, imageBox.PointToImage(e.Location).Y);
        }
    }
}
