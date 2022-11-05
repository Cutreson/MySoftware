using Basler.Pylon;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCam
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
			return Cam.IsConnected;
        }
		public GigE_Basler()
        {
			this.Init();
			Environment.SetEnvironmentVariable("PYLON_GIGE_HEARTBEAT", "500");
        }
		private void Init()
        {
			if (CamList == null)
            {
				CamList = new List<ICamera>();
            }
			else
            {
				if(CamInfoList == CameraFinder.Enumerate())
                {
					return;
                }
				foreach(ICamera current in CamList)
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
	}
}
