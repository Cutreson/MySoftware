using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Basler.Pylon;
using MySoftware.Class.CamDevices;
using OpenCvSharp;

namespace MySoftware.Class.CamDevices
{
	public class GigE_Basler : ICamDevice, IDisposable
	{
		private int CamIndex;
		private Camera Cam;
		private Mat Image;
		private bool isHwTrigger = false;
		private bool IsGrabSuccess = false;
		private bool IsGrabFail = false;
		private object mLock = new object();
		private static List<ICameraInfo> Caminfolist;
		private static List<ICamera> Camlist;
		private Stopwatch stopWatch = new Stopwatch();
		private static object _imgLock = new object();
		private int mExposureTime;
		private int mBeforeGrabDelay;
		public bool IsConnected()
		{
			//Son code
			if (Cam == null) return false;
			//
			return Cam.IsConnected; 
		}
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
		public GigE_Basler()
		{
			this.InitializeCamera();
			Environment.SetEnvironmentVariable("PYLON_GIGE_HEARTBEAT", "500");
		}
		private void InitializeCamera()
		{
			bool flag = Camlist == null;
			if (flag)
			{
				Camlist = new List<ICamera>();
			}
			else
			{
				bool flag2 = Caminfolist == CameraFinder.Enumerate();
				if (flag2)
				{
					return;
				}
				foreach (ICamera current in Camlist)  
				{
					current.Close();
					current.Dispose();
				}
				Camlist.Clear();
			}
			Caminfolist = CameraFinder.Enumerate();

		}
		public void AcqusitionStart()
		{
			this.Cam.StreamGrabber.Start(GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
			bool flag = this.Cam.WaitForFrameTriggerReady(100, TimeoutHandling.ThrowException);
			if (flag)
			{
				this.Cam.ExecuteSoftwareTrigger();
			}
		}

		public void AcqusitionStop()
		{
			this.Cam.StreamGrabber.Stop();
		}
		public void CameraClose()
		{
			bool flag = this.Cam == null;
			if (!flag)
			{
				bool flag2 = this.Cam.StreamGrabber != null;
				if (flag2)
				{
					this.Cam.StreamGrabber.ImageGrabbed -= new EventHandler<ImageGrabbedEventArgs>(this.OnImageGrabbed);
					this.Cam.StreamGrabber.Stop();
				}
				this.Cam.Close();
			}
		}
		public void CameraOpen(int camNumber, bool ev = true)
		{
			bool flag = camNumber < 0 || camNumber >= GigE_Basler.Caminfolist.Count;
			if (!flag)
			{
				this.CamIndex = camNumber;
				int num = GigE_Basler.Camlist.FindIndex((ICamera ss) => GigE_Basler.Caminfolist[this.CamIndex]["SerialNumber"] == ss.CameraInfo["SerialNumber"]);
				bool flag2 = num < 0;
				if (flag2)
				{
					this.Cam = new Camera(GigE_Basler.Caminfolist[this.CamIndex]);
					GigE_Basler.Camlist.Add(this.Cam);
				}
				else
				{
					this.Cam = (GigE_Basler.Camlist[num] as Camera);
				}
				if (ev)
				{
					this.Cam.StreamGrabber.ImageGrabbed -= new EventHandler<ImageGrabbedEventArgs>(this.OnImageGrabbed);
					this.Cam.StreamGrabber.ImageGrabbed += new EventHandler<ImageGrabbedEventArgs>(this.OnImageGrabbed);
				}
				object obj = this.mLock;
				lock (obj)
				{
					bool flag4 = !this.Cam.IsOpen;
					if (flag4)
					{
						this.Cam.Open();
					}
					bool flag5 = !this.Cam.IsConnected;
					if (flag5)
					{
						this.Cam.Close();
						Thread.Sleep(500);
						this.Cam.Open();

						//SvLogger.Log.Debug("camera re-connecting.. : " + this.Cam.ToString());
					}
				}
			}
		}
		public string GetCameraSeiralNo()
		{
			return this.GetCameraSeiralNo(this.CamIndex);
		}
		public string GetCameraSeiralNo(int camNumber)
		{
			bool flag = GigE_Basler.Caminfolist == null;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = GigE_Basler.Caminfolist.Count <= camNumber;
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = GigE_Basler.Caminfolist[camNumber]["SerialNumber"];
				}
			}
			return result;
		}
		private void Cam_ConnectionLost(object sender, EventArgs e)
		{
			this.Cam.StreamGrabber.Stop();
			this.CameraClose();
			Thread.Sleep(100);
		}
		public int GetConnectedCamCount()
		{
			return GigE_Basler.Caminfolist.Count;
		}
		private void OnImageGrabbed(object sender, ImageGrabbedEventArgs e)
		{
			try
			{
				object imgLock = GigE_Basler._imgLock;
				lock (imgLock)
				{
					IGrabResult grabResult = e.GrabResult;
					bool grabSucceeded = grabResult.GrabSucceeded;
					if (grabSucceeded)
					{
                        //this.Image = new Mat(grabResult.Height, grabResult.Width, MatType.CV_8UC1, (byte) grabResult.PixelData,0L);
                        this.Image = new Mat();

                        this.Image = new Mat(grabResult.Height, grabResult.Width, MatType.CV_8UC1, grabResult.PixelData as byte[], 0L);
						this.IsGrabSuccess = true;
					}
					else
					{
						this.IsGrabFail = true;
						//SvLogger.Log.Error("Basler Camera Grab Fail");
					}
				}
			}
			catch (Exception ex)
			{
				//SvLogger.Log.Error(ex.ToString());
			}
			finally
			{
				e.DisposeGrabResultIfClone();
			}
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
			bool flag = this.Cam == null;
			if (!flag)
			{
				bool flag2 = this.Image != null;
				if (flag2)
				{
					this.Image.Dispose();
				}
				bool isOpen = this.Cam.IsOpen;
				if (isOpen)
				{
					this.Cam.Close();
				}
				this.Cam.Dispose();
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
			lock (obj)
			{
				try
				{
					this.IsGrabSuccess = false;
					this.IsGrabFail = false;
					bool flag2 = this.Cam == null;
					if (flag2)
					{
						result = null;
					}
					else
					{
						bool flag3 = Image != null ;
						if (flag3)
						{
							this.Image.Dispose();
						}
						DateTime now = DateTime.Now;
						while (DateTime.Now.Subtract(now).TotalMilliseconds < (double)this.BeforeGrabDelay)
						{
							Thread.Sleep(1);
						}
						bool flag4 = !this.Cam.IsOpen;
						if (flag4)
						{
							this.Cam.Open();
						}
						bool flag5 = this.mExposureTime > 0;
						if (flag5)
						{
							this.Cam.Parameters[PLCamera.ExposureTimeRaw].TrySetValue((long)this.mExposureTime);
						}
						this.Cam.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.SingleFrame);
						this.Cam.StreamGrabber.Start(1L, GrabStrategy.LatestImages, GrabLoop.ProvidedByStreamGrabber);
						this.stopWatch.Reset();
						this.stopWatch.Start();
						while (true)
						{
							bool isGrabSuccess = this.IsGrabSuccess;
							if (isGrabSuccess)
							{
								break;
							}
							bool isGrabFail = this.IsGrabFail;
							if (isGrabFail)
							{
								goto Block_12;
							}
							bool flag6 = this.stopWatch.ElapsedMilliseconds > 2000L;
							if (flag6)
							{
								goto Block_13;
							}
							Thread.Sleep(10);
						}
						result = this.Image;
						return result;
					Block_12:
						result = null;
						return result;
					Block_13:
						//SvLogger.Log.Error("Basler Camera Grab TimeOut.");
						result = null;

					}
				}
				catch (Exception ex)
				{
					//SvLogger.Log.Error(ex.ToString());
					result = null;
				}
				finally
				{
					this.Cam.Close();
				}
			}
			return result;
		}
		public bool FlushBuffer()
		{
			return true;
		}
        public string GetIPCam()
        {
            return GetIPCam(this.CamIndex);
        }
        public string GetIPCam(int camNumber)
        {
            bool flag = GigE_Basler.Caminfolist == null;
            string result;
            if (flag)
            {
                result = null;
            }
            else
            {
                bool flag2 = GigE_Basler.Caminfolist.Count <= camNumber;
                if (flag2)
                {
                    result = null;
                }
                else
                {
                    result = GigE_Basler.Caminfolist[camNumber][CameraInfoKey.DeviceIpAddress];
                }
            }
            return result;
        }
        public List<CamInfor> GetCamInfors()
        {
            List<CamInfor> camInfors = new List<CamInfor>();
            foreach (var item in Caminfolist)
            {
                
                try
                {
                    CamInfor camInfor = new CamInfor();
                    if (item[CameraInfoKey.DeviceIpAddress]!=null)
                    {
                        camInfor.CamIP = item[CameraInfoKey.DeviceIpAddress];
                    }
                    camInfor.CamName = item[CameraInfoKey.SerialNumber];
                    camInfors.Add(camInfor);
                }
                catch (Exception)
                {

                }
            }
            return camInfors;
        }
    }
}
