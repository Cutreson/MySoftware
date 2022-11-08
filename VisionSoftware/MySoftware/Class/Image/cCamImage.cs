using MySoftware.Class.CamDevices;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySoftware.Class.Image
{
    public class cCamImage
    {
        #region PROPERTIES
        public string Name { get; set; }
        public Guid ID { get; set; }
        //public int STT { get; set; }
        //public string IP { get; set; }
        //public int Port { get; set; }

        public string CamType { get; set; }
        public string PathSaveImage { get; set; }
        public string ImageType { get; set; }

        private string _DeviceNameOrigin;
        public string CamName { get; set; }
        public bool IsSaveImage { get; set; }
        public int CamIndex { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the device name origin. </summary>
        ///
        /// <value> The device name origin. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public string DeviceOrigin
        {
            get { return _DeviceNameOrigin; }
            set
            {
                _DeviceNameOrigin = value;
                //Vendor = GlobalFuntion.GetVendorName(_DeviceNameOrigin);
            }
        }
        public string Vendor { get; set; }
        //public List<cROIs> LstROI { get; set; }
        public string Application { get; set; }
        public int NumBuffers { get; set; }
        public cCameraSettings Setting;
        public cCameraSettings SettingDefault;
        public bool IsAutoSetDefault { get; set; }
        public bool IsHaveDefaultSetting { get; set; }
        public bool IsCamera { get; set; }
        public int CurrentImgIndex { get; set; }
        public ECameraType ECameraType;
        public Bitmap OutputImage = null;
        public List<string> Images;
        public ICamDevice CamDevice;
        private ICamDevice camDevice;
        #endregion
        public cCamImage()
        {
            ID = Guid.NewGuid();
            IsCamera = true;

            Images = new List<string>();
            NumBuffers = 3;
            CurrentImgIndex = 0;
            Setting = new cCameraSettings();
            SettingDefault = new cCameraSettings();
            IsAutoSetDefault = true;
            IsHaveDefaultSetting = false;
            CamDevice = new GigE_Basler();

            //CameraType = GlobVar.SystemFiles.Paths.AppPath + "DCF" + Path.DirectorySeparatorChar + "XCL-500.dcf";
        }

        #region VALIABLE

        public bool IsConnected
        {
            get
            {
                if (IsCamera)
                {
                    return CamDevice.IsConnected();
                }
                else
                    return true;
            }
        }

       //public Dictionary<string, cPropCompare> PropCompare = new Dictionary<string, cPropCompare>();
        public bool IsLive { get; set; } = false;

        #endregion

        #region FUNCITION
        public void LoadAllPropCamera()
        {

        }

        public bool Connect()
        {
            try
            {
                if (IsConnected)
                    return true;
                if (!IsCamera)
                {
                    //IsConnected = true;
                    return true;
                }
                if (CamDevice == null)
                {
                    CamDevice = MakeCamDevice();
                }

                if (GlobVar.DicCams != null &&
                    GlobVar.DicCams.ContainsKey(CamName) &&
                    GlobVar.DicCams[CamName].IsConnected)
                {
                    CamDevice = GlobVar.DicCams[CamName].CamDevice;
                    //IsConnected = true;
                    return true;
                }

                List<cCamInfor> camInfors = CamDevice.GetCamInfors();
                int countDevice = camInfors.Count;

                if (countDevice < 1)
                {
                    cMessageBox.Notification("Not find any device");
                    return false;
                }
                for (int i = 0; i < countDevice; i++)
                {
                    cCamInfor camInfor = camInfors[i];
                    if (camInfor.CamName == CamName)
                    {
                        CamIndex = i;
                        break;
                    }
                }

                CamDevice.CameraOpen(CamIndex);


                //IsConnected = true;
                if (GlobVar.DicCams == null)
                    GlobVar.DicCams = new Dictionary<string, cCamImage>();
                if (!GlobVar.DicCams.ContainsKey(CamName))
                    GlobVar.DicCams.Add(CamName, this);
                if (CamDevice.IsConnected())
                {
                    return true;
                }
                else
                    return false;
            }

            catch (Exception ex)
            {
                GlobFuncs.SaveLogFile(ex.ToString());
                cMessageBox.Error(ex.ToString());
                return false;
            }
        }
        public void Disconnect()
        {
            if (IsCamera)
            {
                CamDevice.CameraClose();
                //IsConnected = false;
                if (GlobVar.DicCams == null)
                    GlobVar.DicCams = new Dictionary<string, cCamImage>();
                //if (GlobVar.DicCams.ContainsKey(Device))
                //    GlobVar.DicCams.Remove(Device);
            }
        }
        private void InitialCamDevice()
        {

            //CamDevice = new GigE_Hikvision();
        }
        private ICamDevice MakeCamDevice()
        {
            try
            {
                bool flag = this.camDevice != null;
                if (flag)
                {
                    this.camDevice.Dispose();
                    this.camDevice = null;
                }
                switch (this.ECameraType)
                {
                    case ECameraType.GigE_IMITech:
                        //this.camDevice = new GigE_IMITech();
                        break;
                    case ECameraType.GigE_Basler:
                        this.camDevice = new GigE_Basler();
                        break;
                    case ECameraType.GigE_PointGrey:
                        //this.camDeivce = new GigE_PointGrey();
                        break;
                    case ECameraType.WebCam:
                        //this.camDeivce = new WebCam();
                        break;
                    case ECameraType.GigE_Hikvision:
                        //this.camDevice = new GigE_Hikvision();
                        break;
                    default:
                        this.camDevice = null;
                        break;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(string.Format("카메라 만드는 부분. 이거뜨면 디버깅 해야함. 뜨면안됨\r\n\r\n{0}", ex.ToString()));
            }
            return this.camDevice;
        }
        public Bitmap SnapImage(PictureBox smartWindowControl = null, bool isSetPart = true, bool isPreview = false)
        {
            if (IsCamera)
                return SnapImage_Camera(smartWindowControl, isSetPart, isPreview);
            else
                return SnapImage_Computer(smartWindowControl, isSetPart, isPreview);
        }
        object mLock = new object();

        public Bitmap SnapImage_Camera(PictureBox smartWindowControl = null, bool isSetPart = true, bool isPreview = false)
        {
            OutputImage = null;

            if (!IsConnected)
                Connect();
            int i = 0;
            do
            {
                try
                {
                    //if (GrabberMode == cGrabberMode.ASync)
                    //{
                    //    //HOperatorSet.GrabImageAsync(out hObject, SourceImageSettings.CameraSettings.FrameGrabber,
                    //    //        -1);
                    //    for (int i1 = 0; i1 < NumBuffers; i1++)
                    //        HOperatorSet.GrabImageAsync(out hObject, Framgraber,
                    //            -1);
                    //}
                    //else
                    //    HOperatorSet.GrabImage(out hObject, Framgraber);

                    //OutputImage = new HImage(hObject);

                    Mat image = CamDevice.GrabImage();
                    if (image != null)
                    {
                        OutputImage = image.ToBitmap();
                        if (smartWindowControl != null)
                        {
                            if (smartWindowControl.InvokeRequired)
                            {
                                smartWindowControl.Invoke((MethodInvoker)delegate
                                {
                                    lock (mLock)
                                    {
                                        //Bitmap hImage = OutputImage;
                                        smartWindowControl.Image = OutputImage;

                                    }
                                });
                            }
                            else
                            {
                                lock (mLock)
                                {
                                    //Bitmap hImage = OutputImage;
                                    smartWindowControl.Image = OutputImage;
                                }
                            }
                        }

                        //GlobFuncs.FillPictureBox(pbWindow, bitmap);
                    }
                    break;
                }
                catch (Exception ex)
                {
                    OutputImage = null;
                    continue;
                }
                finally
                {
                    i += 1;
                }
            } while (i <= 2);

            if (OutputImage != null && smartWindowControl != null)
            {
                //if (isSetPart)
                //GlobFuncs.SmartSetPart(OutputImage, smartWindowControl);

                if (isPreview)
                {
                    StopLive();

                }
            }
            return OutputImage;
        }
        public Bitmap SnapImage_Computer(PictureBox smartWindowControl = null, bool isSetPart = true, bool isPreview = false)
        {
            OutputImage = null;

            if (CurrentImgIndex < 0 || (Images != null &&
                CurrentImgIndex >= Images.Count))
                CurrentImgIndex = 0;

            if (Images != null &&
                Images.Count > 0 &&
                File.Exists(Images[CurrentImgIndex]))
            {
                //OutputImage = new HImage(Images[CurrentImgIndex]);
                //CurrentImgIndex += 1;
            }

            if (OutputImage != null && smartWindowControl != null)
            {
                //if (isSetPart)
                //    GlobFuncs.SmartSetPart(OutputImage, smartWindowControl);

                if (isPreview)
                {
                    //smartWindowControl.HalconWindow.ClearWindow();
                    //GlobFuncs.SmartSetPart(OutputImage, smartWindowControl);
                    //smartWindowControl.HalconWindow.DispObj(OutputImage);
                }
            }
            return OutputImage;
        }
        private void LiveProcess(PictureBox window)
        {
            if (!IsConnected)
                Connect();
            if (!IsConnected || window == null)
                return;
            IsLive = true;

            SnapImage(window);

            while (IsLive)
            {
                try
                {
                    if (GlobVar.LockEvents) continue;
                    if (!IsLive)
                        return;
                    SnapImage(window);
                }
                catch
                {
                    continue;
                }
            }
        }

        public void Live(PictureBox window)
        {
            if (IsCamera)
                Task.Factory.StartNew(() => LiveProcess(window));
        }
        public void StopLive()
        {
            IsLive = false;
            if (GlobVar.DicCams != null && GlobVar.DicCams.Count > 0)
            {
                foreach (var item in GlobVar.DicCams.Values)
                    if (item.CamName == CamName)
                    {
                        item.IsLive = false;
                    }
            }

        }
        public Mat SnapMat(Display window = null, bool isSetPart = true, bool isPreview = false)
        {
            if (IsCamera)
                return SnapMat_Camera(window, isSetPart, isPreview);
            else
                return SnapMat_Computer(window, isSetPart, isPreview);
        }

        public Mat SnapMat_Camera(Display window = null, bool isSetPart = true, bool isPreview = false)
        {
            OutputImage = null;
            Mat image;
            if (!IsConnected)
                Connect();
            int i = 0;
            do
            {
                try
                {
                    //if (GrabberMode == cGrabberMode.ASync)
                    //{
                    //    //HOperatorSet.GrabImageAsync(out hObject, SourceImageSettings.CameraSettings.FrameGrabber,
                    //    //        -1);
                    //    for (int i1 = 0; i1 < NumBuffers; i1++)
                    //        HOperatorSet.GrabImageAsync(out hObject, Framgraber,
                    //            -1);
                    //}
                    //else
                    //    HOperatorSet.GrabImage(out hObject, Framgraber);

                    //OutputImage = new HImage(hObject);

                    image = CamDevice.GrabImage();
                    if (image != null)
                    {
                        //OutputImage = image.ToBitmap();
                        //if (smartWindowControl != null)
                        //{
                        //    if (smartWindowControl.InvokeRequired)
                        //    {
                        //        smartWindowControl.Invoke((MethodInvoker)delegate
                        //        {
                        //            lock (mLock)
                        //            {
                        //                //Bitmap hImage = OutputImage;
                        //                //smartWindowControl.Image = hImage;

                        //            }
                        //        });
                        //    }
                        //    else
                        //    {
                        //        lock (mLock)
                        //        {
                        //            //Bitmap hImage = OutputImage;
                        //            //smartWindowControl.Image = hImage;
                        //        }
                        //    }
                        //}

                        //GlobFuncs.FillPictureBox(pbWindow, bitmap);
                    }
                    break;
                }
                catch (Exception ex)
                {
                    image = null;
                    continue;
                }
                finally
                {
                    i += 1;
                }
            } while (i <= 2);

            if (image != null && window != null)
            {
                //if (isSetPart)
                //GlobFuncs.SmartSetPart(OutputImage, smartWindowControl);

                if (isPreview)
                {
                    StopLive();
                    Mat matView = image.Clone();
                    window.pbWindow.Image = matView.ToBitmap();
                    matView.Dispose();
                }
            }
            return image;
        }
        public Mat SnapMat_Computer(Display window = null, bool isSetPart = true, bool isPreview = false)
        {
            Mat mat = null;
            if (CurrentImgIndex < 0 || (Images != null &&
                CurrentImgIndex >= Images.Count))
                CurrentImgIndex = 0;

            if (Images != null &&
                Images.Count > 0 &&
                File.Exists(Images[CurrentImgIndex]))
            {
                string fileName = Images[CurrentImgIndex];
                mat = new Mat(fileName, ImreadModes.Grayscale);
                CurrentImgIndex += 1;
            }

            if (mat != null && window != null)
            {
                //if (isSetPart)
                //    GlobFuncs.SmartSetPart(OutputImage, smartWindowControl);

                if (isPreview)
                {
                    Mat matView = mat.Clone();
                    window.pbWindow.Image = matView.ToBitmap();
                }
            }
            return mat;
        }


        #endregion
    }
}
