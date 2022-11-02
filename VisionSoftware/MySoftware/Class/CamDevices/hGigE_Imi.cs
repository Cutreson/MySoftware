using MySoftware.ClassCommon;
using NeptuneClassLibWrap;
using OpenCvSharp;
//using SvMip.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace MySoftware.Class.CamDevices
{
	public class GigE_IMITech : ICamDevice, System.IDisposable
	{
		public delegate void TriggerRunHandler(object sender, double time);
		private const uint openTimeout = 5000u;
		private const uint grabTimeout = 500u;
		private const uint queryTimeout = 250u;
		private static NeptuneClassLibCLR neptuneClass = null;
		private static CameraInstance[] cameraInstance = null;
		private uint camTotalCount;
		private FrameDataPtr frameData = new FrameDataPtr();
		private object AcquisitionLock = new object();
		private NEPTUNE_IMAGE_SIZE maxSize = default(NEPTUNE_IMAGE_SIZE);
		private NEPTUNE_CAM_INFO[] pinfo = null;
		private Mat image = null;
		private Mat imageGrab = null;
		private uint camIndex;
		private float rcvFrameRate = 5f;
		private bool isHwTrigger = false;
		private System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
		private int mExposureTime;
		private int mBeforeGrabDelay;
		private bool disposedValue = false;
		public event GigE_IMITech.TriggerRunHandler TriggerRun;
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
				NEPTUNE_TRIGGER trigger = default(NEPTUNE_TRIGGER);
				GigE_IMITech.cameraInstance[(int)((System.UIntPtr)this.camIndex)].GetTrigger(ref trigger);
				if (!this.isHwTrigger)
				{
					trigger.Mode = 0;
					trigger.nParam = 1;
					trigger.Polarity = ENeptunePolarity.NEPTUNE_POLARITY_FALLINGEDGE;
					trigger.Source = ENeptuneTriggerSource.NEPTUNE_TRIGGER_SOURCE_SW;
					trigger.OnOff = ENeptuneBoolean.NEPTUNE_BOOL_TRUE;
				}
				else
				{
					trigger.Mode = 0;
					trigger.nParam = 1;
					trigger.Polarity = 0;
					trigger.Source = 0;
					trigger.OnOff = ENeptuneBoolean.NEPTUNE_BOOL_TRUE; ;
					this.DelayHWTrigger();
				}
				GigE_IMITech.cameraInstance[(int)((System.UIntPtr)this.camIndex)].SetTrigger(trigger);
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
		private void DelayHWTrigger()
		{
			NEPTUNE_XML_FLOAT_VALUE_INFO Info = default(NEPTUNE_XML_FLOAT_VALUE_INFO);
			XMLDescription Desc = GigE_IMITech.cameraInstance[(int)((System.UIntPtr)this.camIndex)].GetXMLInstance();
			Desc.GetNodeFloat("ExposureTimeAbs", ref Info);
			int nDelay = (int)(Info.dValue / 1000.0) + 76;
			GigE_IMITech.Delay(nDelay * 2);
		}
		private static System.DateTime Delay(int MS)
		{
			System.DateTime thisMoment = System.DateTime.Now;
			System.TimeSpan duration = new System.TimeSpan(0, 0, 0, 0, MS);
			System.DateTime afterMoment = thisMoment.Add(duration);
			while (afterMoment >= thisMoment)
			{
				Application.DoEvents();
				thisMoment = System.DateTime.Now;
			}
			return System.DateTime.Now;
		}
		public GigE_IMITech()
		{
			if (GigE_IMITech.neptuneClass == null)
			{
				GigE_IMITech.neptuneClass = new NeptuneClassLibCLR();
				GigE_IMITech.neptuneClass.InitLibrary();
			}
			this.camTotalCount = DeviceManager.Instance.GetTotalCamera();
			if (GigE_IMITech.cameraInstance == null)
			{
				GigE_IMITech.cameraInstance = new CameraInstance[(int)((System.UIntPtr)this.camTotalCount)];
			}
			else
			{
				if ((long)GigE_IMITech.cameraInstance.Length != (long)((ulong)this.camTotalCount))
				{
					GigE_IMITech.cameraInstance = new CameraInstance[(int)((System.UIntPtr)this.camTotalCount)];
				}
			}
			this.pinfo = new NEPTUNE_CAM_INFO[(int)((System.UIntPtr)this.camTotalCount)];
			DeviceManager.Instance.GetCameraList(this.pinfo, this.camTotalCount);
			int i = 0;
			while ((long)i < (long)((ulong)this.camTotalCount))
			{
				if (!string.IsNullOrEmpty(this.pinfo[i].strIP))
				{
					int j = i;
					while ((long)j < (long)((ulong)this.camTotalCount))
					{
						if (!string.IsNullOrEmpty(this.pinfo[j].strIP))
						{
							if (this.ConvertIPToLong(this.pinfo[i].strIP) > this.ConvertIPToLong(this.pinfo[j].strIP))
							{
								NEPTUNE_CAM_INFO temp = this.pinfo[i];
								this.pinfo[i] = this.pinfo[j];
								this.pinfo[j] = temp;
							}
						}
						j++;
					}
				}
				i++;
			}
		}
		private long ConvertIPToLong(string ipAddress)
		{
			System.Net.IPAddress ip;
			long result;
			if (System.Net.IPAddress.TryParse(ipAddress, out ip))
			{
				byte[] bytes = ip.GetAddressBytes();
				result = (long)(16777216uL * (ulong)bytes[0] + 65536uL * (ulong)bytes[1] + 256uL * (ulong)bytes[2] + (ulong)bytes[3]);
			}
			else
			{
				result = 0L;
			}
			return result;
		}
		public void CameraOpen(int camNumber, bool ok)
		{
			this.camIndex = (uint)camNumber;
			if (this.camIndex < 0u || this.camIndex > this.camTotalCount)
			{
				throw new System.Exception("Cam Index 잘못");
			}
			NeptuneDevice neptuneDeivce = DeviceManager.Instance.GetDeviceFromSerial(this.pinfo[(int)((System.UIntPtr)this.camIndex)].strSerial);
			if (neptuneDeivce == null)
			{
				throw new System.Exception("There isn't camera device (IMI Tech)");
			}
			if (GigE_IMITech.cameraInstance[camNumber] == null)
			{
				GigE_IMITech.cameraInstance[(int)((System.UIntPtr)this.camIndex)] = new CameraInstance();
				System.DateTime s = System.DateTime.Now;
				while (true)
				{
					ENeptuneError error = GigE_IMITech.cameraInstance[(int)((System.UIntPtr)this.camIndex)].CameraOpen(neptuneDeivce, 0);
					if (error == 0)
					{
						break;
					}
					if (System.DateTime.Now.Subtract(s).TotalMilliseconds > 5000.0)
					{
						goto Block_6;
					}
				}
				ENeptuneUserSet userSetNum = ENeptuneUserSet.NEPTUNE_USERSET_1;
				NEPTUNE_USERSET userSet = default(NEPTUNE_USERSET);
				if (GigE_IMITech.cameraInstance[(int)((System.UIntPtr)this.camIndex)].GetUserSet(ref userSet) != null || (userSet.SupportUserSet >> (int)userSetNum & 1) != 1)
				{
					goto IL_17A;
				}
				userSet.UserSetIndex = userSetNum;
				userSet.Command = 0;
				if (GigE_IMITech.cameraInstance[(int)((System.UIntPtr)this.camIndex)].SetUserSet(userSet) != 0)
				{
					throw new System.Exception("Cam setting load fail");
				}
				goto IL_17A;
			Block_6:
				throw new System.Exception("Cam Open TimeOut");
			IL_17A:
				ENeptuneDevType devType = neptuneDeivce.GetDeviceType();
				if (devType == ENeptuneDevType.NEPTUNE_DEV_TYPE_USB3)
				{
					GigE_IMITech.cameraInstance[(int)((System.UIntPtr)this.camIndex)].SetFrameRate("10.0");
				}
				else
				{
					if (devType == 0)
					{
						GigE_IMITech.cameraInstance[(int)((System.UIntPtr)this.camIndex)].WriteRegister(3332u, 8000u);
						GigE_IMITech.cameraInstance[(int)((System.UIntPtr)this.camIndex)].SetFrameRate("5.0");
					}
				}
				GigE_IMITech.cameraInstance[(int)((System.UIntPtr)this.camIndex)].SetPixelFormat(ENeptunePixelFormat.Mono8);
				GigE_IMITech.cameraInstance[(int)((System.UIntPtr)this.camIndex)].GetMaxImageSize(ref this.maxSize);
				GigE_IMITech.cameraInstance[(int)((System.UIntPtr)this.camIndex)].SetImageSize(this.maxSize);
				this.IsHwTrigger = false;
				this.AcqusitionStart();
			}
		}
		public bool FlushBuffer()
		{
			return GigE_IMITech.cameraInstance[(int)((System.UIntPtr)this.camIndex)].FlushBuffer() == 0;
		}
		public void CameraClose()
		{
			this.TriggerRun = null;
			if (GigE_IMITech.cameraInstance != null && (long)GigE_IMITech.cameraInstance.Length > (long)((ulong)this.camIndex) && GigE_IMITech.cameraInstance[(int)((System.UIntPtr)this.camIndex)] != null)
			{
				lock (this.AcquisitionLock)
				{
					ENeptuneError error = GigE_IMITech.cameraInstance[(int)((System.UIntPtr)this.camIndex)].CameraClose();
					GigE_IMITech.cameraInstance[(int)((System.UIntPtr)this.camIndex)].Dispose();
					GigE_IMITech.cameraInstance[(int)((System.UIntPtr)this.camIndex)] = null;
				}
			}
		}
		public void ShowControlDialog()
		{
			if (GigE_IMITech.cameraInstance[(int)((System.UIntPtr)this.camIndex)] != null)
			{
				GigE_IMITech.cameraInstance[(int)((System.UIntPtr)this.camIndex)].ShowControlDialog();
			}
		}
		public Mat QueryFrame()
		{
			Mat result;
			lock (this.AcquisitionLock)
			{
				if (GigE_IMITech.cameraInstance.Length == 0)
				{
					result = null;
				}
				else
				{
					if (GigE_IMITech.cameraInstance[(int)((System.UIntPtr)this.camIndex)] == null)
					{
						result = null;
					}
					else
					{
						this.AcqusitionStart();
						System.DateTime s = System.DateTime.Now;
						ENeptuneError err = GigE_IMITech.cameraInstance[(int)((System.UIntPtr)this.camIndex)].WaitEventDataStream(ref this.frameData, 250u);
						if (err == 0)
						{
							this.image = new Mat((int)this.frameData.GetHeight(), (int)this.frameData.GetWidth(), 0, this.frameData.GetBufferPtr(), 0L);
							GigE_IMITech.cameraInstance[(int)((System.UIntPtr)this.camIndex)].QueueBufferDataStream(this.frameData.GetBufferIndex());
						}
						result = this.image;
					}
				}
			}
			return result;
		}
		private void NotifyTriggerRun(long time)
		{
			if (this.TriggerRun != null)
			{
				this.TriggerRun(this, (double)time);
			}
		}
		public Mat GrabImage()
		{
			this.sw.Restart();
			System.DateTime startTime = System.DateTime.Now;
			while (System.DateTime.Now.Subtract(startTime).TotalMilliseconds < (double)this.BeforeGrabDelay)
			{
				System.Threading.Thread.Sleep(1);
			}
			Mat result;
			lock (this.AcquisitionLock)
			{
				if (GigE_IMITech.cameraInstance == null || GigE_IMITech.cameraInstance.Length <= 0 || GigE_IMITech.cameraInstance[(int)((System.UIntPtr)this.camIndex)] == null)
				{
					result = null;
				}
				else
				{
					System.Diagnostics.Debug.WriteLine(string.Format("[GrabImage] CameraNo : {0}, Trigger -> FlushBuffer call..", this.camIndex));
					if (!this.IsHwTrigger)
					{
						GigE_IMITech.cameraInstance[(int)((System.UIntPtr)this.camIndex)].FlushBuffer();
					}
					System.DateTime s = System.DateTime.Now;
					ENeptuneError shotError = ENeptuneError.NEPTUNE_ERR_AccessTimeOut;
					ENeptuneError err = ENeptuneError.NEPTUNE_ERR_AccessTimeOut;
					if (!this.isHwTrigger)
					{
						if (shotError != 0)
						{
							System.Diagnostics.Debug.WriteLine(string.Format("[GrabImage] CameraNo : {0}   트리거 시작 시간 : {1}", this.camIndex, System.DateTime.Now.Subtract(s).TotalMilliseconds));
							SvLogger.Log.Data(string.Format("Grab Start", new object[0]), System.DateTime.Now);
							shotError = GigE_IMITech.cameraInstance[(int)((System.UIntPtr)this.camIndex)].RunSoftwareTrigger();
						}
						System.Diagnostics.Debug.WriteLine(string.Format("[GrabImage] CameraNo : {0}   트리거 완료 시간 : {1}", this.camIndex, System.DateTime.Now.Subtract(s).TotalMilliseconds));
						SvLogger.Log.Data(string.Format("Grab End", new object[0]), System.DateTime.Now);
						this.sw.Stop();
						this.NotifyTriggerRun(this.sw.ElapsedMilliseconds);
						System.Diagnostics.Debug.WriteLine(string.Format("=================================trigger time : {0}", this.sw.ElapsedMilliseconds));
						if (shotError != 0)
						{
							System.Diagnostics.Debug.WriteLine(string.Format("Software Trigger send fail", new object[0]));
							result = null;
							return result;
						}
					}
					for (int i = 0; i < 5; i++)
					{
						err = GigE_IMITech.cameraInstance[(int)((System.UIntPtr)this.camIndex)].WaitEventDataStream(ref this.frameData, 500u);
						if (err == 0)
						{
							break;
						}
						if (err == ENeptuneError.NEPTUNE_ERR_TLInitFail)
						{
						}
					}
					if (err == ENeptuneError.NEPTUNE_ERR_TLInitFail)
					{
						System.Diagnostics.Debug.WriteLine(string.Format("Grab Error", new object[0]));
					}
					else
					{
						if (err == 0)
						{
							System.Diagnostics.Debug.WriteLine(string.Format("Grab Success", new object[0]));
						}
					}
					System.Diagnostics.Debug.WriteLine(string.Format("[GrabImage] CameraNo : {0}   이미지 취득 완료 시간 : {1}", this.camIndex, System.DateTime.Now.Subtract(s).TotalMilliseconds));
					if (err == 0)
					{
						Mat img = new Mat((int)this.frameData.GetHeight(), (int)this.frameData.GetWidth(), 0, this.frameData.GetBufferPtr(), 0L).Clone();
						GigE_IMITech.cameraInstance[(int)((System.UIntPtr)this.camIndex)].QueueBufferDataStream(this.frameData.GetBufferIndex());
						System.Diagnostics.Debug.WriteLine(string.Format("[GrabImage] CameraNo : {0}   촬상시간 : {1}", this.camIndex, System.DateTime.Now.Subtract(s).TotalMilliseconds));
						GigE_IMITech.cameraInstance[(int)((System.UIntPtr)this.camIndex)].FlushBuffer();
						result = img;
					}
					else
					{
						System.Diagnostics.Debug.WriteLine(string.Format("[GrabImage] CameraNo : {0} Grab Timeout : {1}", this.camIndex, System.DateTime.Now.Subtract(s).TotalMilliseconds));
						GigE_IMITech.cameraInstance[(int)((System.UIntPtr)this.camIndex)].FlushBuffer();
						result = this.imageGrab;
					}
				}
			}
			return result;
		}
		public void AcqusitionStart()
		{
			NEPTUNE_TRIGGER stTrigger = default(NEPTUNE_TRIGGER);
			GigE_IMITech.cameraInstance[(int)((System.UIntPtr)this.camIndex)].GetTrigger(ref stTrigger);
			if (stTrigger.OnOff == ENeptuneBoolean.NEPTUNE_BOOL_TRUE)
			{
				stTrigger.OnOff = ENeptuneBoolean.NEPTUNE_BOOL_FALSE;
				GigE_IMITech.cameraInstance[(int)((System.UIntPtr)this.camIndex)].SetTrigger(stTrigger);
			}
			GigE_IMITech.cameraInstance[(int)((System.UIntPtr)this.camIndex)].AcquisitionStart(0);
		}
		public void AcqusitionStop()
		{
			NEPTUNE_TRIGGER trigger = default(NEPTUNE_TRIGGER);
			GigE_IMITech.cameraInstance[(int)((System.UIntPtr)this.camIndex)].GetTrigger(ref trigger);
			trigger.Mode = 0;
			trigger.nParam = 1;
			trigger.OnOff = ENeptuneBoolean.NEPTUNE_BOOL_TRUE;
			trigger.Polarity = ENeptunePolarity.NEPTUNE_POLARITY_FALLINGEDGE;
			trigger.Source = ENeptuneTriggerSource.NEPTUNE_TRIGGER_SOURCE_SW;
			ENeptuneError result = GigE_IMITech.cameraInstance[(int)((System.UIntPtr)this.camIndex)].SetTrigger(trigger);
			if (result == 0)
			{
				System.Diagnostics.Debug.WriteLine("[GrabImage] Live -> Trigger 모드 변경 성공");
			}
			else
			{
				System.Diagnostics.Debug.WriteLine("[GrabImage] Live -> Trigger 모드 변경 실패");
			}
		}
		public float Getfps()
		{
			return this.rcvFrameRate;
		}
		public int GetConnectedCamCount()
		{
			return (int)this.camTotalCount;
		}
		public string GetCameraSeiralNo()
		{
			string result;
			if (this.pinfo == null)
			{
				result = null;
			}
			else
			{
				result = this.pinfo[(int)((System.UIntPtr)this.camIndex)].strSerial;
			}
			return result;
		}
		public string GetCameraSeiralNo(int camNumber)
		{
			string result;
			if (this.pinfo == null)
			{
				result = null;
			}
			else
			{
				if (this.pinfo.Length <= camNumber)
				{
					result = null;
				}
				else
				{
					result = this.pinfo[camNumber].strSerial;
				}
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
        public string GetIPCam()
        {
            return "";
        }
        public void Dispose()
		{
			this.Dispose(true);
		}
        public List<CamInfor> GetCamInfors()
        {
            return new List<CamInfor>();
        }
    }
}
