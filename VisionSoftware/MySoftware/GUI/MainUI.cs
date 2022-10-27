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
    public partial class MainUI : Form
    {
        private MainView mainView;
        private CameraLive cameraLive;
        public MainUI()
        {
            InitializeComponent();
            Init();
            //////////////////////////
            Form2 f1 = new Form2();         
            f1.Show(dockPanel, DockState.DockLeftAutoHide);
            f1.Text = "Hide";
            f1.TabText = "Tab hide";
            f1.BackColor = Color.Green;
            Form1 f3 = new Form1();
            f3.Show(dockPanel, DockState.DockRight);
            //////////////////////////
        }
        private void Init()
        {
            cameraLive = new CameraLive();
            cameraLive.Show(dockPanel, DockState.DockLeft); 
            mainView = new MainView();
            mainView.Show(dockPanel);

            cameraLive.ImageReadyEvent += CameraLive_ImageReadyEvent;
        }

        private void CameraLive_ImageReadyEvent(object sender, ImageReadyEventArgs e)
        {
            mainView.ShowImage(e.ImgSrc);
        }
    }
}
