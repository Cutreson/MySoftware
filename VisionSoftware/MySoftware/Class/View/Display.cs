using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySoftware.Class.View
{
    public partial class Display : UserControl
    {
        public Display()
        {
            InitializeComponent();
        }
        //private void OpenImage()
        //{
        //    OpenFileDialog open = new OpenFileDialog();
        //    // image filters  
        //    open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
        //    if (open.ShowDialog() == DialogResult.OK)
        //    {
        //        Bitmap bitmap = new Bitmap(open.FileName);
        //        pbWindow.Image = bitmap;
        //    }

        //}
        private void imageBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (pbWindow.Image != null)
            {
                pbWindow.Text = string.Format("Size : {0}x{1}, (X,Y) = ({2},{3})", pbWindow.Image.Width,
                pbWindow.Image.Height, pbWindow.PointToImage(e.Location).X, pbWindow.PointToImage(e.Location).Y);
            }
            else
            {
                pbWindow.Text = "No Image";
            }
        }

        //private void openImageToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    OpenImage();
        //}
    }
}
