using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace MySoftware
{
    public partial class FrmMain : Form
    {
        private MainProcess mainProcess;
        public FrmMain()
        {
            InitializeComponent();
            Init();
        }
        private void Init()
        {
            mainProcess = new MainProcess(dockPanel);
        }

        private void cameraSettingToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            mainProcess.ShowCamSetting();
        }
    }
}
