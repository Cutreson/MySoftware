using MySoftware.Class.CamDevices;
using MySoftware.Class;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySoftware
{
    public partial class FrmCamSetting : Form
    {
        ECameraType eCameraType;
        cCamImage sourceImage;
        cCamInfor camInfor;
        List<cCamInfor> camInfors = new List<cCamInfor>();
        //public FrmCamSetting()
        //{
        //    InitializeComponent();
        //    Init();
        //}
        public FrmCamSetting(cCamImage image = null)
        {
            InitializeComponent();
            Init(image);
        }
        private void Init()
        {
            sourceImage = new cCamImage();
        }
        private void Init(cCamImage image = null)
        {
            sourceImage = image;
        }
        private void UpdateCamInfo()
        {
            if (sourceImage.CamDevice == null)
                return;

            camInfors = new List<cCamInfor>();
            camInfors = sourceImage.CamDevice.GetCamInfors();
            cboCamName.Items.Clear();
            cboCamIP.Items.Clear();

            foreach (var item in camInfors)
            {
                if (item.CamName != null)
                {
                    cboCamName.Items.Add(item.CamName);
                }
                if (item.CamIP != null)
                {
                    cboCamIP.Items.Add(item.CamIP);
                }
            }
            if (cboCamName.Items.Count > 0 && cboCamIP.Items.Count > 0)
            {
                cboCamName.SelectedIndex = 0;
                cboCamIP.SelectedIndex = 0;
            }
        }
        private ICamDevice MakeCamDevice()
        {
            try
            {
                bool flag = this.sourceImage.CamDevice != null;
                if (flag)
                {
                    this.sourceImage.CamDevice.Dispose();
                    this.sourceImage.CamDevice = null;
                }
                switch (this.eCameraType)
                {
                    case ECameraType.GigE_Hikvision:
                        //this.sourceImage.CamDevice = new GigE_Hikvision();
                        break;
                    case ECameraType.GigE_Basler:
                        this.sourceImage.CamDevice = new GigE_Basler();
                        //string camNo = sourceImage.CamDevice.GetCameraSeiralNo();
                        //string IP = sourceImage.CamDevice.GetIPCam();
                        break;

                    case ECameraType.GigE_PointGrey:
                        //this.camDeivce = new GigE_PointGrey();
                        break;
                    case ECameraType.WebCam:
                        //this.camDevice = new WebCam();
                        break;

                    case ECameraType.GigE_Crevis:
                        //this.camDevice = new GigE_Crevis();
                        break;
                    default:
                        this.sourceImage.CamDevice = null;
                        break;
                }

            }
            catch (Exception ex)
            {
                cMessageBox.Error("Can't open camera :" + ex.ToString());
            }
            return this.sourceImage.CamDevice;
        }
        private void UpdateCameraParam()
        {
            sourceImage.CamType = cboCamType.SelectedItem.ToString();
            sourceImage.ECameraType = (ECameraType)cboCamType.SelectedIndex;
            sourceImage.CamName = cboCamName.SelectedItem?.ToString();
            sourceImage.IsCamera = cboCamType.SelectedIndex == 2 ? false : true;

            //if (cboInterfaces.SelectedItem == null)
            //    sourceImage.Interface = cboInterfaces.Text?.ToString();
            //else
            //    sourceImage.Interface = cboInterfaces.SelectedItem?.ToString();

            //if (cboDevices.SelectedItem == null)
            //    sourceImage.Device = cboDevices.Text?.ToString();
            //else
            //    sourceImage.Device = cboDevices.SelectedItem?.ToString();
        }
        private void cboCamType_SelectedIndexChanged(object sender, EventArgs e)
        {
            eCameraType = (ECameraType)cboCamType.SelectedIndex;
            UpdateCamInfo();
        }

        private void btnDetect_Click(object sender, EventArgs e)
        {
            MakeCamDevice();
            UpdateCamInfo();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (!sourceImage.CamDevice.IsConnected())
                {
                    int camIndex = cboCamName.SelectedIndex;
                    sourceImage.CamDevice.CameraOpen(camIndex);
                    if (!sourceImage.CamDevice.IsConnected())
                        return;
                    btnConnect.Text = "Disconnect";
                    UpdateCameraParam();
                }
                else
                {
                    int camIndex = cboCamName.SelectedIndex;
                    sourceImage.CamDevice.CameraClose();
                    btnConnect.Text = "Connect";
                }

                UpdateCameraParam();
            }
            catch (Exception ex)
            {
                cMessageBox.Error(ex.Message);
            }
        }

        private void btnSnap_Click(object sender, EventArgs e)
        {
            sourceImage.SnapMat(hWindow, true, true);
        }

        bool IsLive = false;
        private void btnLive_Click(object sender, EventArgs e)
        {
            if (!sourceImage.IsLive)
            {
                IsLive = true;
                sourceImage.Live(hWindow.pbWindow);
                for (int i = 0; i < 10; i++)
                {
                    if (!sourceImage.IsConnected)
                    {
                        return;
                    }
                }
                btnLive.Text = "Stop";
                btnLive.BackColor = Color.LightGreen;
                btnConnect.Enabled = false;
                btnDetect.Enabled = false;
            }
            else
            {
                sourceImage.StopLive();
                IsLive = false;
                btnLive.Text = "Live";
                btnLive.BackColor = DefaultBackColor;
                btnConnect.Enabled = true;
                btnDetect.Enabled = true;
            }
        }
    }
}
