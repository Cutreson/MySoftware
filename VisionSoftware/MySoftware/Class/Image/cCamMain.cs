using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Data;
using System.Drawing;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using MySoftware.Class.CamDevices;
using System.Windows.Forms;
using MySoftware.Class.ClassView;

namespace MySoftware
{
    public class cCamMain
    {
        public string Name;

        //public cCamMain(e name)
        //{
        //    Name = name;

        //}    
    }
    public class cPropCompare
    {
        /// <summary>   Type of the data. </summary>
        public Type DataType;
        /// <summary>   Name of the RTC property camera. </summary>
        public string RTCPropCAMName;
        /// <summary>   Name of the property camera. </summary>
        public string PropCAMName;
        /// <summary>   The value. </summary>
        public decimal DValue;
        /// <summary>   The minimum value. </summary>
        public decimal DMinValue;
        /// <summary>   The maximum value. </summary>
        public decimal DMaxValue;
        /// <summary>   The sarr value. </summary>
        public string[] SARRValue;
        /// <summary>   The value. </summary>
        public string SValue;
        /// <summary>   True to read only. </summary>
        public bool ReadOnly;
        /// <summary>   True if is live, false if not. </summary>
        public bool IsLive;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   DATRUONG, 12/11/2021. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public cPropCompare()
        {
            DataType = typeof(string);
            PropCAMName = string.Empty;
            DValue = 0;
            DMinValue = 0;
            DMaxValue = 0;
            SValue = string.Empty;
            ReadOnly = false;
            SARRValue = new string[] { };
            IsLive = true;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   DATRUONG, 12/11/2021. </remarks>
        ///
        /// <param name="_DataType">    Type of the data. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public cPropCompare(Type _DataType)
        {
            DataType = _DataType;
            PropCAMName = string.Empty;
            DValue = 0;
            DMinValue = 0;
            DMaxValue = 0;
            SValue = string.Empty;
            ReadOnly = false;
            SARRValue = new string[] { };
            IsLive = true;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   DATRUONG, 12/11/2021. </remarks>
        ///
        /// <param name="_DataType">    Type of the data. </param>
        /// <param name="_PropCAMName"> Name of the property camera. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public cPropCompare(Type _DataType, string _PropCAMName)
        {
            DataType = _DataType;
            PropCAMName = _PropCAMName;
            DValue = 0;
            DMinValue = 0;
            DMaxValue = 0;
            SValue = string.Empty;
            ReadOnly = false;
            SARRValue = new string[] { };
            IsLive = true;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   DATRUONG, 12/11/2021. </remarks>
        ///
        /// <param name="_DataType">    Type of the data. </param>
        /// <param name="_PropCAMName"> Name of the property camera. </param>
        /// <param name="_DValue">      The value. </param>
        /// <param name="_DMinValue">   The minimum value. </param>
        /// <param name="_DMaxValue">   The maximum value. </param>
        /// <param name="_SValue">      The value. </param>
        /// <param name="_SARRValue">   The sarr value. </param>
        /// <param name="_ReadOnly">    True to read only. </param>
        /// <param name="_IsLive">      True if is live, false if not. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public cPropCompare(Type _DataType, string _PropCAMName, decimal _DValue, decimal _DMinValue, decimal _DMaxValue, string _SValue, string[] _SARRValue, bool _ReadOnly, bool _IsLive)
        {
            DataType = _DataType;
            PropCAMName = _PropCAMName;
            DValue = _DValue;
            DMinValue = _DMinValue;
            DMaxValue = _DMaxValue;
            SARRValue = _SARRValue;
            SValue = _SValue;
            ReadOnly = _ReadOnly;
            IsLive = _IsLive;
        }
    }
    public class CamInfor
    {
        public string CamName { get; set; }
        public string CamType { get; set; }
        public string CamIP { get; set; }
        public int IndexCam { get; set; }
    }
    public class cImage
    {
        /// <summary>   Filename of the file. </summary>
        public string FileName;
        /// <summary>   The value. </summary>
        /// <summary>   True if passed. </summary>
        public bool Passed;
        /// <summary>   True to ran. </summary>
        public bool Ran;
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   DATRUONG, 11/11/2021. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public cImage()
        {
            FileName = string.Empty;
            Passed = false;
            Ran = false;
        }
    }
    public class cCameraSettings
    {
        public double ExposureTime { get; set; }
        public double Gain { get; set; }
        public double TriggerDelay { get; set; }
        public string ExposureMode { get; set; }
        public string PixelFormat { get; set; }
        public string AcquisitionMode { get; set; }
        public string TriggerMode { get; set; }
        public string TriggerSource { get; set; }
        public string TriggerActivation { get; set; }
    }
    public class cComputerSettings
    {
        public bool IsFolder { get; set; }
        public string FolderPath { get; set; }
        public List<cImage> Images { get; set; }
        public int CurrentImgIndex { get; set; }
        public cComputerSettings()
        {
            IsFolder = true;
            FolderPath = string.Empty;
            Images = new List<cImage>();
            CurrentImgIndex = -1;
        }
        public class cSerial
        {
            public Guid ID { get; set; }
            public int OrderNum { get; set; }
            public string Name { get; set; }
        }
    }
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
        public string GrabberMode { get; set; }
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
            GrabberMode = cGrabberMode.Sync;
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

        public Dictionary<string, cPropCompare> PropCompare = new Dictionary<string, cPropCompare>();
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

                List<CamInfor> camInfors =   CamDevice.GetCamInfors();
                int countDevice = camInfors.Count;

                if (countDevice < 1)
                {
                    cMessageBox.Notification("Not find any device");
                    return false;
                }
                for (int i = 0; i < countDevice; i++)
                {
                    CamInfor camInfor = camInfors[i];
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
                    //case CameraType.GigE_IMITech:
                    //    this.camDevice = new GigE_IMITech();
                    //    break;
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
                mat = new Mat(fileName , ImreadModes.Grayscale);
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
