using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCam
{
    public interface ICamDevice : IDisposable
    {
        bool IsHwTrigger { get; set; }
        int ExposureTime { get; set; }
        int BeforeGrabDelay { get; set; }
        bool IsConnected();
        Mat QueryFrame();
        Mat GrabImage();
        void CameraOpen(int camNumber, bool evt = true);
        void CameraClose();
        void AcqusitionStart();
        void AcqusitionStop();
        void ShowControlDialog();
        float GetFps();
        int GetConnectedCamCount();
        string GetCameraSerialNo();
        string GetCameraSerialNo(int camNumeber);
        string GetIPCam();
        List<CamInfor> GetCamInfors();
        bool FlushBuffer();

    }
}
