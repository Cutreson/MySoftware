using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace MySoftware
{
	public interface ICamDevice : IDisposable
	{
		bool IsHwTrigger
		{
			get;
			set;
		}
		int ExposureTime
		{
			get;
			set;
		}
		int BeforeGrabDelay
		{
			get;
			set;
		}
		Mat QueryFrame();
		Mat GrabImage();
		void CameraOpen(int camNumber, bool evt = true);
		void CameraClose();
		void AcqusitionStart();
		void AcqusitionStop();
		void ShowControlDialog();
		float Getfps();
		int GetConnectedCamCount();
		string GetCameraSeiralNo();
		string GetIPCam();
        List<CamInfor> GetCamInfors();
		string GetCameraSeiralNo(int camNumber);
		bool FlushBuffer();
		bool IsConnected();
	}
}
