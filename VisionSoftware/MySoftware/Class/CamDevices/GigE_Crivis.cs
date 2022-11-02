using Crevis.VirtualFG40Library;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace MySoftware.Class.CamDevices
{
	public class GigE_Crevis : ICamDevice, System.IDisposable
	{
		private VirtualFG40Library _virtualFG40 = new VirtualFG40Library();
		private int _hDevice = 0;
		private int _width = 0;
		private int _height = 0;
		private int _bufferSize = 0;
		private bool _isOpen = false;
		private bool _isRun = false;
		private IntPtr _pImage = new IntPtr();
		private bool isHwTrigger = false;
		private int CamNumber = 0;
		private bool disposedValue = false;
		private int mExposureTime;
		private int mBeforeGrabDelay;
		private int mIsConnected;
		public bool IsConnected()
		{
			return false;
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
				return this.mExposureTime;
			}
			set
			{
				if (this.mExposureTime != value)
				{
					this.mExposureTime = value;
				}
			}
		}
		public int BeforeGrabDelay
		{
			get;
			set;
		}
		static GigE_Crevis()
		{
		}
		public GigE_Crevis()
		{
			try
			{
				int status = this._virtualFG40.InitSystem();
				if (status != 0)
				{
					throw new System.Exception(string.Format("System Initialize failed : {0}", status));
				}
			}
			catch (System.Exception ex)
			{
				System.Diagnostics.Debug.WriteLine("[Interface] " + ex.Message);
			}
		}
		public void CameraOpen(int camNumber)
		{
			uint camNum = 0u;
			bool initstatus = false;
			this._virtualFG40.IsInitSystem(ref initstatus);
			if (initstatus)
			{
				if (!this._isOpen)
				{
					try
					{
						int status = this._virtualFG40.UpdateDevice();
						if (status != 0)
						{
							this._virtualFG40.FreeSystem();
							throw new System.Exception(string.Format("Update Device list failed : {0}", status));
						}
						status = this._virtualFG40.GetAvailableCameraNum(ref camNum);
						if (camNum <= 0u)
						{
							this._virtualFG40.FreeSystem();
							throw new System.Exception("The camera can not be connected.");
						}
						status = this._virtualFG40.OpenDevice((uint)camNumber, ref this._hDevice);
						if (status != 0)
						{
							this._virtualFG40.FreeSystem();
							throw new System.Exception(string.Format("Open device failed : {0}", status));
						}
						this._isOpen = true;
						this.SetFeature();
						status = this._virtualFG40.GetIntReg(this._hDevice, "Width", ref this._width);
						if (status != 0)
						{
							throw new System.Exception(string.Format("Read Register failed : {0}", status));
						}
						status = this._virtualFG40.GetIntReg(this._hDevice, "Height", ref this._height);
						if (status != 0)
						{
							throw new System.Exception(string.Format("Read Register failed : {0}", status));
						}
						this._bufferSize = this._width * this._height;
						this._pImage = System.Runtime.InteropServices.Marshal.AllocHGlobal(this._bufferSize);
						this.AcqusitionStart();
					}
					catch (System.Exception ex)
					{
						System.Diagnostics.Debug.WriteLine("[Interface] " + ex.Message);
					}
				}
			}
		}
		public void CameraClose()
		{
			if (this._isOpen)
			{
				if (this._isRun)
				{
					this.AcqusitionStop();
				}
				if (this._pImage != System.IntPtr.Zero)
				{
					System.Runtime.InteropServices.Marshal.FreeHGlobal(this._pImage);
					this._pImage = System.IntPtr.Zero;
				}
				this._virtualFG40.CloseDevice(this._hDevice);
			}
			this._virtualFG40.FreeSystem();
		}
		private void SetFeature()
		{
			try
			{
				int status = this._virtualFG40.SetEnumReg(this._hDevice, "TriggerMode", "On");
				if (status != 0)
				{
					throw new System.Exception(string.Format("Write Register failed : {0}", status));
				}
				status = this._virtualFG40.SetEnumReg(this._hDevice, "TriggerSource", "Software");
				if (status != 0)
				{
					throw new System.Exception(string.Format("Write Register failed : {0}", status));
				}
				status = this._virtualFG40.SetFloatReg(this._hDevice, "TriggerDelay", 1.0);
				if (status != 0)
				{
					throw new System.Exception(string.Format("Write Register failed : {0}", status));
				}
				status = this._virtualFG40.SetEnumReg(this._hDevice, "PixelFormat", "Mono8");
				if (status != 0)
				{
					throw new System.Exception(string.Format("Write Register failed : {0}", status));
				}
			}
			catch (System.Exception ex)
			{
				System.Diagnostics.Debug.WriteLine("[Interface] " + ex.Message);
			}
		}
		public Mat QueryFrame()
		{
			return null;
		}
		public Mat GrabImage()
		{
			Mat result;
			if (!this._isOpen)
			{
				result = null;
			}
			else
			{
				if (!this._isRun)
				{
					this.AcqusitionStart();
				}
				try
				{
					int status = this._virtualFG40.SetCmdReg(this._hDevice, "TriggerSoftware");
					if (status != 0)
					{
						throw new System.Exception(string.Format("Software trigger command failed : {0}", status));
					}
					System.DateTime startTime = System.DateTime.Now;
					while (System.DateTime.Now.Subtract(startTime).TotalMilliseconds < (double)this.BeforeGrabDelay)
					{
						System.Threading.Thread.Sleep(1);
					}
					status = this._virtualFG40.GrabImageAsync(this._hDevice, this._pImage, (uint)this._bufferSize, 4294967295u);
					if (status != 0)
					{
						throw new System.Exception(string.Format("GrabImage failed : {0}", status));
					}
					Mat GrabImg = new Mat(this._height, this._width, 0, this._pImage, 0L).Clone();
					result = GrabImg;
				}
				catch (System.Exception ex)
				{
					System.Diagnostics.Debug.WriteLine("[Interface] " + ex.Message);
					result = null;
				}
			}
			return result;
		}
		public void AcqusitionStart()
		{
			if (this._isOpen)
			{
				try
				{
					int status = this._virtualFG40.SetEnumReg(this._hDevice, "AcquisitionMode", "Continuous");
					if (status != 0)
					{
						throw new System.Exception(string.Format("Write Register failed : {0}", status));
					}
					status = this._virtualFG40.AcqStart(this._hDevice);
					if (status != 0)
					{
						throw new System.Exception(string.Format("Acqusition Start failed : {0}", status));
					}
					this._isRun = true;
					this._virtualFG40.GrabStartAsync(this._hDevice, 4294967295u);
				}
				catch (System.Exception ex)
				{
					System.Diagnostics.Debug.WriteLine("[Interface] " + ex.Message);
				}
			}
		}
		public void AcqusitionStop()
		{
			try
			{
				int status = this._virtualFG40.AcqStop(this._hDevice);
				if (status != 0)
				{
					throw new System.Exception(string.Format("Acqusition Start failed : {0}", status));
				}
			}
			catch (System.Exception ex)
			{
				System.Diagnostics.Debug.WriteLine("[Interface] " + ex.Message);
			}
		}
		public void ShowControlDialog()
		{
			throw new System.NotImplementedException();
		}
		public float Getfps()
		{
			throw new System.NotImplementedException();
		}
		public int GetConnectedCamCount()
		{
			uint camNum = 0u;
			int result;
			try
			{
				int status = this._virtualFG40.UpdateDevice();
				if (status != 0)
				{
					this._virtualFG40.FreeSystem();
					throw new System.Exception(string.Format("Update Device list failed : {0}", status));
				}
				status = this._virtualFG40.GetAvailableCameraNum(ref camNum);
				result = (int)camNum;
			}
			catch (System.Exception ex)
			{
				System.Diagnostics.Debug.WriteLine("[Interface] " + ex.Message);
				result = 0;
			}
			return result;
		}
		public string GetCameraSeiralNo()
		{
			return this.GetCameraSeiralNo(this.CamNumber);
		}
		public string GetCameraSeiralNo(int camNumber)
		{
			uint size = 0u;
			string result;
			try
			{
				int status = this._virtualFG40.GetStrReg(this._hDevice, "DeviceModelName", null, ref size);
				if (status != 0)
				{
					throw new System.Exception(string.Format("Read Register failed : {0}", status));
				}
				byte[] pInfo = new byte[(int)((System.UIntPtr)size)];
				this._virtualFG40.GetStrReg(this._hDevice, "DeviceModelName", pInfo, ref size);
				string modelName = System.Text.Encoding.Default.GetString(pInfo);
				result = modelName;
			}
			catch (System.Exception ex)
			{
				System.Diagnostics.Debug.WriteLine("[Interface] " + ex.Message);
				result = null;
			}
			return result;
		}
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposedValue)
			{
				if (disposing)
				{
					this.CameraClose();
				}
				this.disposedValue = true;
			}
		}
		public void Dispose()
		{
			this.Dispose(true);
		}
		public bool FlushBuffer()
		{
			return true;
		}
        public string GetIPCam()
        {
            return "";
        }
        public void CameraOpen(int camNumber, bool evt = true)
        {
            throw new NotImplementedException();
        }
        public List <CamInfor> GetCamInfors()
        {
            return new List<CamInfor>();
        }
    }
}
