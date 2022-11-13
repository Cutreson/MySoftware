using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeifenLuo.WinFormsUI.Docking;

namespace MySoftware
{
    public class MainProcess
    {
        public cCamImage sourceImage;
        private FrmCamSetting frmCamSetting;
        private FrmView mainView;
        public MainProcess(DockPanel dockPanel)
        {
            Init(dockPanel);
        }
        public void Init(DockPanel dockPanel)
        {
            this.sourceImage = new cCamImage();
            Show(dockPanel);
        }
        public void Show(DockPanel dockPanel)
        {        
            mainView = new FrmView(sourceImage);
            mainView.Show(dockPanel);
            Form1 f1 = new Form1();
            f1.Show(dockPanel, DockState.DockLeft);
            Form2 f2 = new Form2();
            f2.Show(dockPanel, DockState.DockRight);
        }
        public void ShowCamSetting()
        {
            frmCamSetting = new FrmCamSetting(sourceImage);
            frmCamSetting.Show();
        }
    }
}
