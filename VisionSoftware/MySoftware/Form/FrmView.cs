using System;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace MySoftware
{
    public partial class FrmView : DockContent
    {
        private cCamImage sourceImage;
        public FrmView(cCamImage image = null)
        {
            InitializeComponent();
            this.sourceImage = image;
        }
        private void OpenImage()
        {
            OpenFileDialog open = new OpenFileDialog();
            // image filters  
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                Bitmap bitmap = new Bitmap(open.FileName);
                hWindow.pbWindow.Image = bitmap;
            }

        }
        private void openImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenImage();
        }
        private void grabImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sourceImage.SnapMat(hWindow, true, true);
        }

        private void liveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(!sourceImage.IsLive)
            {
                sourceImage.Live(hWindow.pbWindow);
                this.contextMenu.Items[3].Text = "Stop Live";
            }
            else
            {
                sourceImage.StopLive();
                this.contextMenu.Items[3].Text = "Live";
            }
        }
    }
}
