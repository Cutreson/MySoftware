using MySoftware.Camera;
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
            Init();
        }
        private void Init()
        {

        }
        public void ShowImage()
        {
            imageBox.Refresh();
            imageBox.Image = StaticData.imgSrc;
        }
        private void OpenImage()
        {
            OpenFileDialog open = new OpenFileDialog();
            // image filters  
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                Bitmap bitmap = new Bitmap(open.FileName);
                //StaticData.imgSrc = bitmap;
                imageBox.Image = bitmap;
                bitmap.Dispose();
            }    
                   
        }
        private void imageBox_MouseMove(object sender, MouseEventArgs e)
        {
            if(imageBox.Image != null)
            {
                imageBox.Text = string.Format("Size : {0}x{1}, (X,Y) = ({2},{3})", imageBox.Image.Width,
                imageBox.Image.Height, imageBox.PointToImage(e.Location).X, imageBox.PointToImage(e.Location).Y);
            }
            else
            {
                imageBox.Text = "No Image";
            }
        }

        private void openImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenImage();
        }
    }
}
