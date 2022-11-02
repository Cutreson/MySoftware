using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace MySoftware.GUI
{
    public partial class FrmMain : Form
    {
        private FrmView mainView;
        public FrmMain()
        {
            InitializeComponent();
            Init();
            //////////////////////////
            Form1 f1 = new Form1();
            f1.Show(dockPanel, DockState.DockLeft);
            Form2 f2 = new Form2();
            f2.Show(dockPanel, DockState.DockRight);
            //////////////////////////
        }
        private void Init()
        {
            mainView = new FrmView();
            mainView.Show(dockPanel);
        }

        private void cameraSettingToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            FrmSettingCamera frmSettingCamera = new FrmSettingCamera();
            frmSettingCamera.Show();
        }
    }
}
