using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySoftware.Class.CamDevices;
using MySoftware.Class.hTools;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using WeifenLuo.WinFormsUI.Docking;

namespace MySoftware
{
    public partial class FrmSettingCamera : Form
    {
        string BlockToolID = string.Empty;
        cCamImage sourceImage ;
        cCamImage sourceImageBka;
        string ID;
        CamInfor camInfor;
        List<CamInfor> camInfors = new List<CamInfor>();
        public FrmSettingCamera(cCamImage camImage =  null, string _ID = "")
        {
            InitializeComponent();
            //sourceImage = camImage;
            sourceImage = new cCamImage();
            sourceImageBka = new cCamImage();
            ID = _ID;
        }
        List<string> Interfaces;
       
        Dictionary<string, string> NameDevices;
        //ICamDevice camDevice;
        ECameraType eCameraType;
      
        private void cbCamTypeEditValueChanged()
        {
            if (GlobVar.LockEvents)
                return;
            cboCamIP.Items.Clear();
            if (sourceImage == null)
                return;

            string noName = cboCamName.Text;
            
        }
        private void btnDetect_Click(object sender, EventArgs e)
        {
            MakeCamDevice();
            UpdateCamInfo();

            //if (sourceImage ==  null)
            //{
            //    sourceImage = new cCamImage();
            //}
            //cboInCamNo.Items.Clear();
            //foreach (string item in Interfaces)
            //    if (!cboInCamNo.Items.Contains(item))
            //        cboInCamNo.Items.Add(item);

            //if (Interfaces.Count > 0)
            //{
                //GlobVar.LockEvents = false;
                //if (sourceImage.CamNumber != string.Empty &&
                //    cboInCamNo.Items.IndexOf(sourceImage.CamNumber) >= 0)
                //    cboInCamNo.SelectedIndex = cboInCamNo.Items.IndexOf(sourceImage.Name);
                //else
                //    cboInCamNo.SelectedIndex = cboInCamNo.Items.Count - 1;

                //sourceImage.Interface = cboInCamNo.Text;
            //}
        }
        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            //GlobFuncs.SaveCamSourceImage(sourceImage, ID);
        }
        private void DisableAllPropControl()
        {
            RTCExposureTimeRaw.Enabled = false;
            RTCGainRaw.Enabled = false;
            RTCExposureMode.Enabled = false;
            RTCPixelFormat.Enabled = false;
            RTCAcquisitionMode.Enabled = false;
            RTCTriggerSource.Enabled = false;
            RTCTriggerSource.Enabled = false;
            RTCTriggerActivation.Enabled = false;
            RTCTriggerDelayRaw.Enabled = false;
        }
        private void ViewPropValue()
        {
            try
            {
                GlobVar.LockEvents = true;
                DisableAllPropControl();

                if (sourceImage == null || sourceImage.PropCompare == null)
                    return;

                var c = GlobFuncs.GetAll(this, typeof(TextBox));
                if (c != null && c.Count() > 0)
                {
                    TextBox textbox = null;
                    foreach (Control item in c)
                    {
                        textbox = (TextBox)item;
                        string PropertyName = textbox.Name.Substring(3, textbox.Name.Length - 3);
                        if (sourceImage.PropCompare.ContainsKey(PropertyName))
                        {
                            cPropCompare propCompare = sourceImage.PropCompare[PropertyName];
                            textbox.Text = propCompare.DValue.ToString();
                            textbox.Enabled = true;
                            textbox.TextChanged += null;
                        }
                    }
                }

                c = GlobFuncs.GetAll(this, typeof(ComboBox));
                if (c != null && c.Count() > 0)
                {
                    ComboBox combo = null;
                    foreach (Control item in c)
                    {
                        combo = (ComboBox)item;
                        string PropertyName = combo.Name.Substring(3, combo.Name.Length - 3);
                        if (sourceImage.PropCompare.ContainsKey(PropertyName))
                        {
                            cPropCompare propCompare = sourceImage.PropCompare[PropertyName];
                            combo.Items.Clear();
                            foreach (string s in propCompare.SARRValue)
                            {
                                combo.Items.Add(s);
                            }
                            combo.Text = propCompare.SValue;
                            combo.Enabled = true;
                            combo.SelectedIndexChanged += null;
                        }
                    }
                }
            }
            finally
            {
                GlobVar.LockEvents = false;
            }
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
                    btnConnect.Text = cStrings.Disconnect;
                }
                else
                {
                    int camIndex = cboCamName.SelectedIndex;
                    sourceImage.CamDevice.CameraClose();
                    btnConnect.Text = cStrings.Connect;
                }    
            }
            catch (Exception ex)
            {
                cMessageBox.Error(ex.Message);
            }
        }
        private void tbAcqusition_Click(object sender, EventArgs e)
        {

        }
        
        private void cbDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GlobVar.LockEvents)
                return;
            if (NameDevices != null && NameDevices.ContainsKey(cboCamIP.Text))
                sourceImage.DeviceOrigin = NameDevices[cboCamIP.Text];
        }
        private void ViewCamSettings()
        {
            if (GlobVar.LockEvents)
                return;
            GlobVar.LockEvents = true;

            if (sourceImage == null)
                sourceImage = new cCamImage();
            //cboInterfaces.SelectedItem = sourceImage.Interface;
            //cboDevices.SelectedItem = sourceImage.Device;

            if (sourceImage.Images != null)
            {
                lstImages.Items.Clear();
                foreach (var item in sourceImage.Images)
                    lstImages.Items.Add(item);
            }

            cbGrabMode.SelectedIndex = cbGrabMode.Items.IndexOf(sourceImage.GrabberMode);
            //cboCamType.SelectedIndex = sourceImage.IsCamera ? 0 : 1;
            txtNumberBuffer.Text = sourceImage.NumBuffers.ToString();
            chkAutoSetDefaultSettings.Checked = sourceImage.IsAutoSetDefault;
            cboCamType.SelectedItem = sourceImage.CamType;


            ckbIsSaveImage.Checked = sourceImage.IsSaveImage;
            txtFolderImage.Text = sourceImage.PathSaveImage;
            cboImageType.SelectedItem = sourceImage.ImageType;
            cboCamName.SelectedText = sourceImage.CamName;
            cboCamName.SelectedItem = sourceImage.CamName;
            int index = cboCamName.SelectedIndex;

            GlobVar.LockEvents = false;
        }
        private void FrmSettingCamera_Load(object sender, EventArgs e)
        {
            ViewCamSettings();
        }
        private void UpdateCamInfo()
        {
            if (sourceImage.CamDevice == null)
                return;

            camInfors = new List<CamInfor>();
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

            if (cboCamName.Items.Count > 0 && cboCamIP.Items.Count >0)
            {
                cboCamName.SelectedIndex = 0;
                cboCamIP.SelectedIndex = 0;
            }
            
        }
        private void UpdateCamInfor_Item()
        {
            if (camInfor == null)
            {
                camInfor = new CamInfor();
            }
            foreach (var item in camInfors)
            {
                if (item.IndexCam -1 == cboCamName.SelectedIndex )
                {
                    
                    camInfor = item;
                    break;
                }
            }
            //camInfor = (CamInfor)camInfors.Where(o => o.IndexCam == cboInCamNo.SelectedIndex);
            if (camInfor !=null)
            {
                cboCamIP.SelectedItem = camInfor.CamIP;
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
        private void cboCamType_SelectedIndexChanged(object sender, EventArgs e)
        {
            eCameraType = (ECameraType)cboCamType.SelectedIndex;
            UpdateCamInfo();
            //sourceImage.IsCamera = cboCamType.SelectedIndex == 0 ? true : false;
            if (cboCamType.SelectedIndex != 0)
            {
                //btnConnect.Enabled = false;
                //btnDetect.Enabled = false;
                tbSettingsImage.Hide();

            }
            else
            {
                tbSettingsImage.Show();
                //btnConnect.Enabled = true;
                //btnDetect.Enabled = true;
            }
        }

       

        private void btnLoadImages_Click(object sender, EventArgs e)
        {
            openImageFiles.RestoreDirectory = true;
            if (openImageFiles.ShowDialog() == DialogResult.OK)
                if (openImageFiles.FileNames != null && openImageFiles.FileNames.Count() > 0)
                    foreach (var item in openImageFiles.FileNames)
                    {
                        lstImages.Items.Add(item);
                        if (sourceImage != null && sourceImage.Images != null)
                            sourceImage.Images.Add(item);
                    }
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            if (lstImages.SelectedIndex == 0)
                return;
            string fileNameBAK = sourceImage.Images[lstImages.SelectedIndex];
            sourceImage.Images[lstImages.SelectedIndex] = sourceImage.Images[lstImages.SelectedIndex - 1];
            sourceImage.Images[lstImages.SelectedIndex - 1] = fileNameBAK;
            lstImages.Items[lstImages.SelectedIndex] = sourceImage.Images[lstImages.SelectedIndex];
            lstImages.Items[lstImages.SelectedIndex - 1] = sourceImage.Images[lstImages.SelectedIndex - 1];
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            if (lstImages.SelectedIndex == lstImages.Items.Count - 1)
                return;
            string fileNameBAK = sourceImage.Images[lstImages.SelectedIndex];
            sourceImage.Images[lstImages.SelectedIndex] = sourceImage.Images[lstImages.SelectedIndex + 1];
            sourceImage.Images[lstImages.SelectedIndex + 1] = fileNameBAK;
            lstImages.Items[lstImages.SelectedIndex] = sourceImage.Images[lstImages.SelectedIndex];
            lstImages.Items[lstImages.SelectedIndex + 1] = sourceImage.Images[lstImages.SelectedIndex + 1];
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstImages.SelectedIndex < 0)
                return;

            if (sourceImage != null && sourceImage.Images != null)
            {
                sourceImage.Images.RemoveAt(lstImages.SelectedIndex);
                lstImages.Items.RemoveAt(lstImages.SelectedIndex);
            }
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            if (cMessageBox.Question_YesNo(cMessageContent.Que_ClearListImage) == DialogResult.Yes)
            {
                lstImages.Items.Clear();
                if (sourceImage != null)
                    sourceImage.Images = new List<string>();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            sourceImage = sourceImageBka;
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


            sourceImage.IsSaveImage = ckbIsSaveImage.Checked;
            sourceImage.PathSaveImage = txtFolderImage.Text;
            sourceImage.ImageType = cboImageType.SelectedItem?.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateCameraParam();
        }

        private void btnLoadImageTest_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "BMP File|*.bmp";
            if (ofd.ShowDialog() ==  DialogResult.OK)
            {
                string fileImage = ofd.FileName;
                Bitmap bitmap = new Bitmap(fileImage);
                hWindow.pbWindow.Image = bitmap;
            }
        }

        private void btnSnap_Click(object sender, EventArgs e)
        {
            sourceImage.SnapMat(hWindow , true ,true);
            //if (image!=null)
            //{
            //    hWindow.pbWindow.Image = image;
            //    //GlobFuncs.FillPictureBox(pbWindow, bitmap);
            //}
        }

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
                btnLive.Text = cStrings.Stop;
                btnLive.BackColor = Color.LightGreen;
                btnConnect.Enabled = false;
                btnDetect.Enabled = false;
            }
            else
            {
                sourceImage.StopLive();
                IsLive = false;
                btnLive.Text = cStrings.Live;
                btnLive.BackColor = DefaultBackColor;
                btnConnect.Enabled = true;
                btnDetect.Enabled = true;
            }
        }
        bool IsLive = false;
        object liveLock = new object();
        private void cboInCamNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCamInfor_Item();
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtFolderImage.Text = fbd.SelectedPath;
                }
            }
        }
    }
}
