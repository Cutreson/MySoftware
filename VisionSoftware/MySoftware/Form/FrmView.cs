using System;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace MySoftware.GUI
{
    public partial class FrmView : DockContent
    {
        public FrmView()
        {
            InitializeComponent();
            Init();
        }
        private void Init()
        {

        }
        public void ShowImage(Image img)
        {
            imageBox.Image = img;
        }
        private void OpenImage()
        {
            OpenFileDialog open = new OpenFileDialog();
            // image filters  
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                Bitmap bitmap = new Bitmap(open.FileName);
                imageBox.Image = bitmap;
            }

        }
        private void imageBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (imageBox.Image != null)
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
