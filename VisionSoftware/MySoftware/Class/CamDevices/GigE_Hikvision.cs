using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Basler.Pylon;
using MySoftware.Class.CamDevices;
using OpenCvSharp;
using MvCamCtrl.NET;
using System.Drawing;
using System.Drawing.Imaging;
using OpenCvSharp.Extensions;

namespace MySoftware.Class.CamDevices
{
    public class GigE_Hikvision : ICamDevice, IDisposable
    {
        private int CamIndex;
        UInt32 m_nBufSizeForDriver = 3072 * 2048 * 3;
        byte[] m_pBufForDriver = new byte[3072 * 2048 * 3];

        // ch:用于保存图像的缓存 | en:Buffer for saving image
        UInt32 m_nBufSizeForSaveImage = 3072 * 2048 * 3 * 3 + 2048;
        byte[] m_pBufForSaveImage = new byte[3072 * 2048 * 3 * 3 + 2048];

        bool m_bGrabbing;
        MyCamera.MV_CC_DEVICE_INFO_LIST m_pDeviceList;
        private MyCamera m_pMyCamera;
        private Mat Image;
        private bool isHwTrigger = false;
        private bool IsGrabSuccess = false;
        private bool IsGrabFail = false;
        private object mLock = new object();
        //private static List<ICameraInfo> Caminfolist;
        //private static List<ICamera> Camlist;
        private Stopwatch stopWatch = new Stopwatch();
        private static object _imgLock = new object();
        private int mExposureTime;

        private int mBeforeGrabDelay;
        public bool IsHwTrigger
        {
            get
            {
                return this.isHwTrigger;
            }
            set
            {
                this.isHwTrigger = value;
            }
        }
        public int ExposureTime
        {
            get
            {
                return this.mExposureTime / 35;
            }
            set
            {
                this.mExposureTime = value * 35;
            }
        }
        public int BeforeGrabDelay
        {
            get;
            set;
        }
        public GigE_Hikvision()
        {
            this.InitializeCamera();
            Environment.SetEnvironmentVariable("PYLON_GIGE_HEARTBEAT", "500");
        }
        private void InitializeCamera()
        {
            DeviceListAcq();
        }
        public void AcqusitionStart()
        {
            int nRet;

            // ch:开始采集 | en:Start Grabbing
            nRet = m_pMyCamera.MV_CC_StartGrabbing_NET();
            if (MyCamera.MV_OK != nRet)
            {
                cMessageBox.Error("Trigger Fail!");
                return;
            }
        }
        public void AcqusitionStop()
        {
            try
            {
                int nRet = -1;
                // ch:停止采集 | en:Stop Grabbing
                nRet = m_pMyCamera.MV_CC_StopGrabbing_NET();
                if (nRet != MyCamera.MV_OK)
                {
                    cMessageBox.Error("Stop Grabbing Fail!");
                }

                // ch:标志位设为false | en:Set flag bit false
                m_bGrabbing = false;
            }
            catch (Exception)
            {

            }
            
        }
        public void CameraClose()
        {
            // ch:关闭设备 | en:Close Device
            int nRet;
            AcqusitionStop();
            nRet = m_pMyCamera.MV_CC_CloseDevice_NET();
            if (MyCamera.MV_OK != nRet)
            {
                //return;
            }

            nRet = m_pMyCamera.MV_CC_DestroyDevice_NET();
            if (MyCamera.MV_OK != nRet)
            {
                //return;
            }

            // ch:控件操作 | en:Control Operation
            //SetCtrlWhenClose();

            // ch:取流标志位清零 | en:Reset flow flag bit
            m_bGrabbing = false;
            mIsConnected = false;
        }
        public void CameraOpen(int camNumber, bool ev = true)
        {
            mIsConnected = false;
            if (camNumber == -1)
            {
                cMessageBox.Error("No device, please select");
                return;
            }
            int nRet = -1;

            // ch:获取选择的设备信息 | en:Get selected device information
            MyCamera.MV_CC_DEVICE_INFO device =
                (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_pDeviceList.pDeviceInfo[camNumber],
                                                              typeof(MyCamera.MV_CC_DEVICE_INFO));

            // ch:打开设备 | en:Open device
            if (null == m_pMyCamera)
            {
                m_pMyCamera = new MyCamera();
                if (null == m_pMyCamera)
                {
                    return;
                }
            }

            nRet = m_pMyCamera.MV_CC_CreateDevice_NET(ref device);
            if (MyCamera.MV_OK != nRet)
            {
                return;
            }

            nRet = m_pMyCamera.MV_CC_OpenDevice_NET();
            if (MyCamera.MV_OK != nRet)
            {
                m_pMyCamera.MV_CC_DestroyDevice_NET();
                cMessageBox.Error("Device open fail!");
                return;
            }

            // ch:探测网络最佳包大小(只对GigE相机有效) | en:Detection network optimal package size(It only works for the GigE camera)
            if (device.nTLayerType == MyCamera.MV_GIGE_DEVICE)
            {
                int nPacketSize = m_pMyCamera.MV_CC_GetOptimalPacketSize_NET();
                if (nPacketSize > 0)
                {
                    nRet = m_pMyCamera.MV_CC_SetIntValue_NET("GevSCPSPacketSize", (uint)nPacketSize);
                    if (nRet != MyCamera.MV_OK)
                    {
                        Console.WriteLine("Warning: Set Packet Size failed {0:x8}", nRet);
                    }
                }
                else
                {
                    Console.WriteLine("Warning: Get Packet Size failed {0:x8}", nPacketSize);
                }
            }

            // ch:设置采集连续模式 | en:Set Continues Aquisition Mode
            m_pMyCamera.MV_CC_SetEnumValue_NET("AcquisitionMode", 2);// ch:工作在连续模式 |0: Off ,1: On 
            m_pMyCamera.MV_CC_SetEnumValue_NET("TriggerMode", 0);
            //m_pMyCamera.MV_CC_SetEnumValue_NET("TriggerMode", 7);    // ch:连续模式 | en:Continuous
            AcqusitionStart();
            mIsConnected = true;
            //bnGetParam_Click(null, null);// ch:获取参数 | en:Get parameters

            //// ch:控件操作 | en:Control operation
            //SetCtrlWhenOpen();
        }
        public string GetCameraSeiralNo()
        {
            return this.GetCameraSeiralNo(this.CamIndex);
        }
        public string GetCameraSeiralNo(int camNumber)
        {
            //bool flag = GigE_Basler.Caminfolist == null;
            //string result;
            //if (flag)
            //{
            //	result = null;
            //}
            //else
            //{
            //	bool flag2 = GigE_Basler.Caminfolist.Count <= camNumber;
            //	if (flag2)
            //	{
            //		result = null;
            //	}
            //	else
            //	{
            //		result = GigE_Basler.Caminfolist[camNumber]["SerialNumber"];
            //	}
            //}
            //return result;

            return "";
        }
        private void Cam_ConnectionLost(object sender, EventArgs e)
        {
            //this.Cam.StreamGrabber.Stop();
            this.CameraClose();
            Thread.Sleep(100);
        }
        public int GetConnectedCamCount()
        {
            //return GigE_Basler.Caminfolist.Count;
            return 0;
        }
        private void OnImageGrabbed(object sender)
        {
            //try
            //{
            //	//object imgLock = GigE_Basler._imgLock;			
            //	object imgLock = new object();
            //	lock (imgLock)
            //	{
            //		IGrabResult grabResult = e.GrabResult;
            //		bool grabSucceeded = grabResult.GrabSucceeded;
            //		if (grabSucceeded)
            //		{
            //			this.Image = new Mat(grabResult.Height, grabResult.Width, MatType.CV_8UC1, grabResult.PixelData as byte[], 0L);
            //			this.IsGrabSuccess = true;
            //		}
            //		else
            //		{
            //			this.IsGrabFail = true;
            //			//SvLogger.Log.Error("Basler Camera Grab Fail");
            //		}
            //	}
            //}
            //catch (Exception ex)
            //{
            //	//SvLogger.Log.Error(ex.ToString());
            //}
            //finally
            //{
            //	e.DisposeGrabResultIfClone();
            //}

        }
        public Mat QueryFrame()
        {
            throw new NotImplementedException();
        }
        public void ShowControlDialog()
        {
            throw new NotImplementedException();
        }
        public float Getfps()
        {
            throw new NotImplementedException();
        }
        protected virtual void Dispose(bool disposing)
        {
            bool flag = this.m_pMyCamera == null;
            if (!flag)
            {
                bool flag2 = this.Image != null;
                if (flag2)
                {
                    this.Image.Dispose();
                }
                //bool isOpen = this.m_pMyCamera.IsOpen;
                this.CameraClose();

                //if (isOpen)
                //{
                //	this.CameraClose();
                //}
                m_pMyCamera.MV_CC_DestroyDevice_NET();
            }
        }
        public void Dispose()
        {
            this.Dispose(true);
        }
        public Mat GrabImage()
        {
            object obj = this.mLock;
            Mat result;
            if (m_pMyCamera ==  null)
            {
                return null;
            }
            try
            {
                //int nRet = m_pMyCamera.MV_CC_SetCommandValue_NET("TriggerSoftware");
                //if (MyCamera.MV_OK != nRet)
                //{
                //    cMessageBox.Error("Trigger Fail!");
                //}
                Thread.Sleep(50);
                result = Grap();
                //result = Grap();
            }
            catch 
            {
                //SvLogger.Log.Error(ex.ToString());
                result = null;
            }
            finally
            {
                //this.Cam.Close();
            }
            //lock (obj)
            //{
                
            //}
            return result;
        }
        public string GetIPCam()
        {
            return "";
        }
        public bool FlushBuffer()
        {
            return true;
        }
        private bool mIsConnected; 
        public bool IsConnected()
        {
            return mIsConnected;
        }

        private List<CamInfor> camInfors;
        public List<CamInfor> GetCamInfors()
        {
            return camInfors;
        }

        private void DeviceListAcq()
        {
            int nRet;

            camInfors = new List<CamInfor>();

            // ch:创建设备列表 en:Create Device List
            System.GC.Collect();
            //cbDeviceList.Items.Clear();
            nRet = MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref m_pDeviceList);
            if (0 != nRet)
            {
                cMessageBox.Error("Enumerate devices fail!");
                return;
            }

            // ch:在窗体列表中显示设备名 | en:Display device name in the form list
            for (int i = 0; i < m_pDeviceList.nDeviceNum; i++)
            {
                MyCamera.MV_CC_DEVICE_INFO device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_pDeviceList.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));
                if (device.nTLayerType == MyCamera.MV_GIGE_DEVICE)
                {
                    CamInfor camInfor = new CamInfor();
                    IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device.SpecialInfo.stGigEInfo, 0);
                    MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_GIGE_DEVICE_INFO));
                    if (gigeInfo.chUserDefinedName != "")
                    {
                        //cbDeviceList.Items.Add("GigE: " + gigeInfo.chUserDefinedName + " (" + gigeInfo.chSerialNumber + ")");
                        camInfor.CamName = "GigE: " + gigeInfo.chUserDefinedName + " (" + gigeInfo.chSerialNumber + ")";
                        camInfor.IndexCam = camInfors.Count + 1;
                        //camInfor.CamNumber = gigeInfo.nCurrentSubNetMask;
                        camInfors.Add(camInfor);

                    }
                    else
                    {
                        //cbDeviceList.Items.Add("GigE: " + gigeInfo.chManufacturerName + " " + gigeInfo.chModelName + " (" + gigeInfo.chSerialNumber + ")");
                        camInfor.CamName = ("GigE: " + gigeInfo.chManufacturerName + " " + gigeInfo.chModelName + " (" + gigeInfo.chSerialNumber + ")");
                        camInfor.IndexCam = camInfors.Count + 1;
                        camInfors.Add(camInfor);
                    }
                }
                else if (device.nTLayerType == MyCamera.MV_USB_DEVICE)
                {
                    CamInfor camInfor = new CamInfor();
                    IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device.SpecialInfo.stUsb3VInfo, 0);
                    MyCamera.MV_USB3_DEVICE_INFO usbInfo = (MyCamera.MV_USB3_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_USB3_DEVICE_INFO));
                    if (usbInfo.chUserDefinedName != "")
                    {
                        //cbDeviceList.Items.Add("USB: " + usbInfo.chUserDefinedName + " (" + usbInfo.chSerialNumber + ")");
                        camInfor.CamName = ("USB: " + usbInfo.chUserDefinedName + " (" + usbInfo.chSerialNumber + ")");
                        camInfor.IndexCam = camInfors.Count + 1;
                        camInfors.Add(camInfor);
                    }
                    else
                    {
                        //cbDeviceList.Items.Add("USB: " + usbInfo.chManufacturerName + " " + usbInfo.chModelName + " (" + usbInfo.chSerialNumber + ")");
                        camInfor.CamName = ("USB: " + usbInfo.chManufacturerName + " " + usbInfo.chModelName + " (" + usbInfo.chSerialNumber + ")");
                        camInfor.IndexCam = camInfors.Count + 1;
                        camInfors.Add(camInfor);
                    }
                }
            }

            // ch:选择第一项 | en:Select the first item
            if (m_pDeviceList.nDeviceNum != 0)
            {
                //cbDeviceList.SelectedIndex = 0;
            }
        }
        private Boolean IsColorData(MyCamera.MvGvspPixelType enGvspPixelType)
        {
            switch (enGvspPixelType)
            {
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_YUV422_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_YUV422_YUYV_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_YCBCR411_8_CBYYCRYY:
                    return true;

                default:
                    return false;
            }
        }
        private Boolean IsMonoData(MyCamera.MvGvspPixelType enGvspPixelType)
        {
            switch (enGvspPixelType)
            {
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono12_Packed:
                    return true;

                default:
                    return false;
            }
        }
        int Count = 0;
        private Mat Grap()
        {
            Mat matRs = new Mat();
            int nRet;
            UInt32 nPayloadSize = 0;
            MyCamera.MVCC_INTVALUE stParam = new MyCamera.MVCC_INTVALUE();
            nRet = m_pMyCamera.MV_CC_GetIntValue_NET("PayloadSize", ref stParam);
            if (MyCamera.MV_OK != nRet)
            {
                cMessageBox.Error("Get PayloadSize failed!");
                return null;
            }
            nPayloadSize = stParam.nCurValue;
            if (nPayloadSize > m_nBufSizeForDriver)
            {
                m_nBufSizeForDriver = nPayloadSize;
                m_pBufForDriver = new byte[m_nBufSizeForDriver];

                // ch:同时对保存图像的缓存做大小判断处理 | en:Determine the buffer size to save image
                // ch:BMP图片大小：width * height * 3 + 2048(预留BMP头大小) | en:BMP image size: width * height * 3 + 2048 (Reserved for BMP header)
                m_nBufSizeForSaveImage = m_nBufSizeForDriver * 3 + 2048;
                m_pBufForSaveImage = new byte[m_nBufSizeForSaveImage];
            }

            IntPtr pData = Marshal.UnsafeAddrOfPinnedArrayElement(m_pBufForDriver, 0);
            MyCamera.MV_FRAME_OUT_INFO_EX stFrameInfo = new MyCamera.MV_FRAME_OUT_INFO_EX();
            // ch:超时获取一帧，超时时间为1秒 | en:Get one frame timeout, timeout is 1 sec
            nRet = m_pMyCamera.MV_CC_GetOneFrameTimeout_NET(pData, m_nBufSizeForDriver, ref stFrameInfo, 3000);
            if (MyCamera.MV_OK != nRet)
            {
                Count++;
                cMessageBox.Error("No Data!");
                for (int i = 0; i < 5; i++)
                {
                    Grap();
                    if (Count == 5)
                    {
                        Count = 0;
                        break;
                    }
                }
                return null;
            }
            Count = 0;
            MyCamera.MvGvspPixelType enDstPixelType;
            if (IsMonoData(stFrameInfo.enPixelType))
            {
                enDstPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8;
            }
            else if (IsColorData(stFrameInfo.enPixelType))
            {
                enDstPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed;
            }
            else
            {
                cMessageBox.Error("No such pixel type!");
                return null;
            }

            IntPtr pImage = Marshal.UnsafeAddrOfPinnedArrayElement(m_pBufForSaveImage, 0);
            //MyCamera.MV_SAVE_IMAGE_PARAM_EX stSaveParam = new MyCamera.MV_SAVE_IMAGE_PARAM_EX();
            MyCamera.MV_PIXEL_CONVERT_PARAM stConverPixelParam = new MyCamera.MV_PIXEL_CONVERT_PARAM();
            stConverPixelParam.nWidth = stFrameInfo.nWidth;
            stConverPixelParam.nHeight = stFrameInfo.nHeight;
            stConverPixelParam.pSrcData = pData;
            stConverPixelParam.nSrcDataLen = stFrameInfo.nFrameLen;
            stConverPixelParam.enSrcPixelType = stFrameInfo.enPixelType;
            stConverPixelParam.enDstPixelType = enDstPixelType;
            stConverPixelParam.pDstBuffer = pImage;
            stConverPixelParam.nDstBufferSize = m_nBufSizeForSaveImage;
            nRet = m_pMyCamera.MV_CC_ConvertPixelType_NET(ref stConverPixelParam);
            if (MyCamera.MV_OK != nRet)
            {
                return null;
            }

            if (enDstPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8)
            {
                //************************Mono8 转 Bitmap*******************************
                Bitmap bmp = new Bitmap(stFrameInfo.nWidth, stFrameInfo.nHeight, stFrameInfo.nWidth * 1, PixelFormat.Format8bppIndexed, pImage);

                ColorPalette cp = bmp.Palette;
                // init palette
                for (int i = 0; i < 256; i++)
                {
                    cp.Entries[i] = Color.FromArgb(i, i, i);
                }
                // set palette back
                bmp.Palette = cp;
                matRs = bmp.ToMat();
                //bmp.Save("image.bmp", ImageFormat.Bmp);
            }
            else
            {
                //*********************RGB8 转 Bitmap**************************
                for (int i = 0; i < stFrameInfo.nHeight; i++)
                {
                    for (int j = 0; j < stFrameInfo.nWidth; j++)
                    {
                        byte chRed = m_pBufForSaveImage[i * stFrameInfo.nWidth * 3 + j * 3];
                        m_pBufForSaveImage[i * stFrameInfo.nWidth * 3 + j * 3] = m_pBufForSaveImage[i * stFrameInfo.nWidth * 3 + j * 3 + 2];
                        m_pBufForSaveImage[i * stFrameInfo.nWidth * 3 + j * 3 + 2] = chRed;
                    }
                }
                try
                {
                    Bitmap bmp = new Bitmap(stFrameInfo.nWidth, stFrameInfo.nHeight, stFrameInfo.nWidth * 3, PixelFormat.Format24bppRgb, pImage);
                    matRs = bmp.ToMat();
                    //bmp.Save("image.bmp", ImageFormat.Bmp);
                }
                catch
                {
                }
            }
            return matRs;
            //ShowErrorMsg("Save Succeed!", 0);
        }

    }
}
