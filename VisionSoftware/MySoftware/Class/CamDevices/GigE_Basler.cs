using Basler.Pylon;
using MySoftware;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Resources.ResXFileRef;
using OpenCvSharp.Extensions;

namespace MySoftware.Class.CamDevices
{
	public class GigE_Basler : ICamDevice
	{
		private int CamIndex;
		private Camera Cam;
		private Mat Image;
		private bool isHwTrigger = false;
		private bool IsGrabSuccess = false;
		private bool IsGrabFail = false;
		private object mLock = new object();
		private static List<ICameraInfo> CamInfoList;
		private static List<ICamera> CamList;
		private Stopwatch stopWatch = new Stopwatch();
		private static object _imgLock = new object();
		private int mExposureTime;
		private int mBeforeGrabDelay;
        private PixelDataConverter converter = new PixelDataConverter();
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
			get
			{
				return mBeforeGrabDelay;

			}
			set
			{
				this.mBeforeGrabDelay = value;
			}
		}
		public bool IsConnected()
		{
			return this.Cam.IsConnected;
		}
		public GigE_Basler()
		{
			this.Init();
			Environment.SetEnvironmentVariable("PYLON_GIGE_HEARTBEAT", "500");
		}
		private void Init()
		{
			this.Cam = new Camera();
			if (CamList == null)
			{
				CamList = new List<ICamera>();
			}
			else
			{
				if (CamInfoList == CameraFinder.Enumerate())
				{
					return;
				}
				foreach (ICamera current in CamList)
				{
					current.Close();
					current.Dispose();
				}
				CamList.Clear();
			}
			CamInfoList = CameraFinder.Enumerate();
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
			bool flag = camNumber < 0 || camNumber >= GigE_Basler.CamInfoList.Count;
			if (!flag)
			{
				this.CamIndex = camNumber;
				int num = GigE_Basler.CamList.FindIndex((ICamera ss)
					=> GigE_Basler.CamInfoList[this.CamIndex]["SerialNumber"] == ss.CameraInfo["SerialNumber"]);
				bool flag2 = num < 0;
				if (flag2)
				{
					this.Cam = new Camera(GigE_Basler.CamInfoList[this.CamIndex]);
					GigE_Basler.CamList.Add(this.Cam);
				}
				else
				{
					this.Cam = GigE_Basler.CamList[num] as Camera;
				}
				if (ev)
				{
					this.Cam.StreamGrabber.ImageGrabbed -= new EventHandler<ImageGrabbedEventArgs>(this.OnImageGrabbed);
					this.Cam.StreamGrabber.ImageGrabbed += new EventHandler<ImageGrabbedEventArgs>(this.OnImageGrabbed);
				}
				object obj = this.mLock;
				lock (obj)
				{
					bool flag3 = !this.Cam.IsOpen;
					if (flag3)
					{
						this.Cam.Open();
					}
					bool flag4 = !this.Cam.IsConnected;
					if (flag4)
					{
						this.Cam.Close();
						Thread.Sleep(500);
						this.Cam.Open();
					}
				}
			}
		}
		public string GetCameraSerialNo()
		{
			return this.GetCameraSerialNo(this.CamIndex);
		}
		public string GetCameraSerialNo(int camNumber)
		{
			bool flag = GigE_Basler.CamInfoList == null;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = GigE_Basler.CamInfoList.Count <= camNumber;
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = GigE_Basler.CamInfoList[camNumber]["SerialNumber"];
				}
			}
			return result;
		}
		public int GetConnectedCamCount()
		{
			return GigE_Basler.CamInfoList.Count;
		}
        //private void OnImageGrabbed(object sender, ImageGrabbedEventArgs e)
        //{
        //	try
        //	{
        //		object imgLock = GigE_Basler._imgLock;
        //		lock (imgLock)
        //		{
        //			IGrabResult grabResult = e.GrabResult;
        //			bool grabSucceeded = grabResult.GrabSucceeded;
        //			if (true)
        //			{
        //				this.Image = new Mat();
        //				this.Image = new Mat(grabResult.Height, grabResult.Width, MatType.CV_8UC1, grabResult.PixelData as byte[], 0L);
        //				Cv2.ImShow("son", Image);
        //				Cv2.WaitKey();
        //				Cv2.DestroyAllWindows();
        //				this.IsGrabSuccess = true;
        //			}
        //			else
        //			{
        //				this.IsGrabFail = true;
        //			}
        //		}
        //	}
        //	catch (Exception ex)
        //	{
        //		Console.WriteLine(ex.Message);
        //	}
        //	finally
        //	{
        //		e.DisposeGrabResultIfClone();
        //	}
        //}
        private void OnImageGrabbed(object sender, ImageGrabbedEventArgs e)
        {
            //if (InvokeRequired)
            //{
            //    // If called from a different thread, we must use the Invoke method to marshal the call to the proper GUI thread.
            //    // The grab result will be disposed after the event call. Clone the event arguments for marshaling to the GUI thread.
            //    //BeginInvoke(new EventHandler<ImageGrabbedEventArgs>(OnImageGrabbed), sender, e.Clone());
            //    return;
            //}

            try
            {
                // Acquire the image from the camera. Only show the latest image. The camera may acquire images faster than the images can be displayed.
                // Get the grab result.
                IGrabResult grabResult = e.GrabResult;

                // Check if the image can be displayed.
                if (grabResult.IsValid)
                {
                    // Reduce the number of displayed images to a reasonable amount if the camera is acquiring images very fast.
                    if (!stopWatch.IsRunning || stopWatch.ElapsedMilliseconds > 33)
                    {
                        stopWatch.Restart();
                        Bitmap bitmap = new Bitmap(grabResult.Width, grabResult.Height, PixelFormat.Format32bppRgb);
                        // Lock the bits of the bitmap.
                        BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
						// Place the pointer to the buffer of the bitmap.
                        converter.OutputPixelFormat = PixelType.BGRA8packed;
						IntPtr ptrBmp = bmpData.Scan0;
                        converter.Convert(ptrBmp, bmpData.Stride * bitmap.Height, grabResult);
                        bitmap.UnlockBits(bmpData);

                        this.Image = new Mat();
                        Image = bitmap.ToMat();
						//Image.ImWrite(@"img.bmp");
						//this.Image = new Mat(grabResult.Height, grabResult.Width, MatType.CV_8UC1, grabResult.PixelData as byte[], 0L);
						this.IsGrabSuccess = true;

                        // Assign a temporary variable to dispose the bitmap after assigning the new bitmap to the display control.

                        //Bitmap bitmapOld = pictureBox.Image as Bitmap;
                        //// Provide the display control with the new bitmap. This action automatically updates the display.
                        //pictureBox.Image = bitmap;
                        //if (bitmapOld != null)
                        //{
                        //    // Dispose the bitmap.
                        //    bitmapOld.Dispose();
                        //}
                    }
                }
            }
            catch (Exception exception)
            {
                //ShowException(exception);
            }
            finally
            {
                // Dispose the grab result if needed for returning it to the grab loop.
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
		public float GetFps()
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
						bool flag3 = Image != null;
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
						Configuration.AcquireContinuous(Cam, null);
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
						result = null;

					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
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
			bool flag = GigE_Basler.CamInfoList == null;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = GigE_Basler.CamInfoList.Count <= camNumber;
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = GigE_Basler.CamInfoList[camNumber][CameraInfoKey.DeviceIpAddress];
				}
			}
			return result;
		}
		public List<cCamInfor> GetCamInfors()
		{
			List<cCamInfor> camInfors = new List<cCamInfor>();
			foreach (var item in CamInfoList)
			{

				try
				{
					cCamInfor camInfor = new cCamInfor();
					if (item[CameraInfoKey.DeviceIpAddress] != null)
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
