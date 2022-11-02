using MySoftware.Class.ClassCommon;
using log4net;
using log4net.Appender;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using NeptuneClassLibWrap;
using OpenCvSharp;
//using SvMip.Common;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySoftware.ClassCommon
{
	public class SvLogger
	{
		public enum LogKind
		{
			System,
			Sequence,
			Recipe,
			Align,
			Debug,
			Error,
			Exception
		}
		public enum ImageKind
		{
			Normal,
			Abnormal,
			Retry,
			Live
		}
		public enum LogType
		{
			SEQUENCE,
			ERROR,
			DEBUG,
			DATA,
			RECIPE,
			TACT
		}
		public enum eSaveImageType
		{
			OK,
			NG,
			OKDisplay,
			NGDisplay,
			Retry,
			Tape
		}
		public static SvLogger Log = new SvLogger();

		private DateTime m_Today_Log;
		private DateTime m_Today_Image;
		public string m_strRoot;
		public string m_strLog;
		public string m_strImage;
		public string m_strOK;
		public string m_strNG;
		public string m_strOKDisplay;
		public string m_strNGDisplay;
		public string m_strRE;
		public string m_strTP;
		public bool m_bSaveImageOk = true;
		public bool m_bSaveImageNg = true;
		public bool m_bSaveDisplayImageOk = true;
		public bool m_bSaveDisplayImageNg = true;
		public bool m_bSaveLog = true;
		public bool m_bSkipAlign = true;
		public bool m_bChangeTapePos = true;
		public bool m_bUseDIO = true;
		public int m_imaxDayImages = 2;
		public int m_imaxDayOkImages = 2;
		public int m_imaxDayNgImages = 2;
		public int m_imaxDayOkDisplayImages = 2;
		public int m_imaxDayNgDisplayImages = 2;
		public int m_iMaxSizeSizeBackups = 0;
		public int m_imaxSizeDateBackups = 2;
		private string m_strMaximumFileSize = "30MB";
		private ILog log;
        private Hierarchy m_Hierarchy;

        public SvLogger()
		{
			string dir = "C:\\SVL_Data\\System\\SystemData.ini";
			if (!Directory.Exists(dir))
			{
				if (Directory.Exists("D:\\SVL_Data\\System\\SystemData.ini"))
				{
					dir = "D:\\SVL_Data\\System\\SystemData.ini";
				}
			}
			SvIni ini = new SvIni(dir);
			this.m_strRoot = ini.ReadValue("Base", "RootPath", "C:\\") + "SVL_Data";
			this.m_strLog = this.m_strRoot + "\\log";
			this.m_strImage = this.m_strRoot + "\\Image";
			this.m_strOK = this.m_strImage + "\\Image";
			this.m_strNG = this.m_strImage + "\\Error";
			this.m_strOKDisplay = this.m_strImage + "\\OKDisplayImage";
			this.m_strNGDisplay = this.m_strImage + "\\NGDisplayImage";
			this.m_strRE = this.m_strImage + "\\Check";
			this.m_strTP = this.m_strImage + "\\Tape";
			if (!Directory.Exists(this.m_strOK))
			{
				Directory.CreateDirectory(this.m_strOK);
			}
			if (!Directory.Exists(this.m_strNG))
			{
				Directory.CreateDirectory(this.m_strNG);
			}
			if (!Directory.Exists(this.m_strRE))
			{
				Directory.CreateDirectory(this.m_strRE);
			}
			if (!Directory.Exists(this.m_strTP))
			{
				Directory.CreateDirectory(this.m_strTP);
			}
			this.log = LogManager.GetLogger("log");
			this.m_Hierarchy = (Hierarchy)LogManager.GetRepository();
			this.m_Hierarchy.Configured = true;
			this.AddApender(this.log, "%date{yyyy-MM-dd},%date{HH:mm:ss.fff},%message%newline");
			this.CleanLogFolder();
			this.m_Today_Log = DateTime.Now;
			this.m_Today_Image = DateTime.Now;
		}

		private void AddApender(ILog log, string strPattern)
		{
			Logger logger = (Logger)log.Logger;
			RollingFileAppender appender = new RollingFileAppender();
			appender.File = Path.Combine(this.m_strLog, "Log_.log");
			appender.PreserveLogFileNameExtension = true;
			appender.StaticLogFileName = false;
			appender.Encoding = Encoding.Unicode;
			appender.AppendToFile = true;
			appender.LockingModel = new FileAppender.MinimalLock();
			appender.RollingStyle = RollingFileAppender.RollingMode.Composite;
			appender.MaxSizeRollBackups = this.m_iMaxSizeSizeBackups;
			appender.MaximumFileSize = this.m_strMaximumFileSize;
			appender.DatePattern = "yyyyMMdd";
			appender.Layout = new PatternLayout(strPattern);
			appender.ActivateOptions();
			logger.AddAppender(appender);
			logger.Hierarchy = this.m_Hierarchy;
			logger.Level = logger.Hierarchy.LevelMap["ALL"];
		}
		public void Sequence(string message)
		{
			if (this.m_bSaveLog)
			{
				this.RollOverDate(this.log);
				this.log.Debug("SEQUENCE," + message);
			}
		}
		public void Error(string message)
		{
			if (this.m_bSaveLog)
			{
				this.RollOverDate(this.log);
				this.log.Debug("ERROR," + message);
			}
		}
		public void Tact(string message)
		{
			if (this.m_bSaveLog)
			{
				this.RollOverDate(this.log);
				this.log.Debug("TACT," + message);
			}
		}
		public void Debug(string message)
		{
			if (this.m_bSaveLog)
			{
				this.RollOverDate(this.log);
				this.log.Debug("DEBUG," + message);
			}
		}
		public void Recipe(string message)
		{
			if (this.m_bSaveLog)
			{
				this.RollOverDate(this.log);
				this.log.Debug("RECIPE," + message);
			}
		}
		public void Data(string message, DateTime time)
		{
			if (this.m_bSaveLog)
			{
				this.RollOverDate(this.log);
				this.log.Debug("DATA," + message);
			}
		}
		public void Image(Mat img, string cellid = "unknown", string _pathAdded = null, int _saveCount = -1)
		{
			if (this.m_bSaveImageOk)
			{
				if (img != null)
				{
					Mat mImage = img.Clone();
					Task.Factory.StartNew(delegate
					{
						this.SaveImage(mImage, string.Format("Img_{0}_{1}.jpg", DateTime.Now.ToString("HHmmss.fff"), cellid), SvLogger.eSaveImageType.OK, _saveCount, _pathAdded);
					}
					);
				}
			}
		}
		public void ErrorImage(Mat img, string cellid = "unknown", string _pathAdded = null, int _saveCount = -1)
		{
			if (this.m_bSaveImageNg)
			{
				if (img != null)
				{
					Mat mError = img.Clone();
					Task.Factory.StartNew(delegate
					{
						this.SaveImage(mError, string.Format("Err_{0}_{1}.jpg", DateTime.Now.ToString("HHmmss.fff"), cellid), SvLogger.eSaveImageType.NG, _saveCount, _pathAdded);
					}
					);
				}
			}
		}
		public void OKDisplayImage(Mat img, string cellid = "unknown", string _pathAdded = null, int _saveCount = -1)
		{
			if (img != null)
			{
				Mat mOKDisplayImage = img.Clone();
				Task.Factory.StartNew(delegate
				{
					this.SaveImage(mOKDisplayImage, string.Format("DisplayOKImg_{0}_{1}.jpg", DateTime.Now.ToString("HHmmss.fff"), cellid), SvLogger.eSaveImageType.OKDisplay, _saveCount, _pathAdded);
				}
				);
			}
		}
		public void NGDisplayImage(Mat img, string cellid = "unknown", string _pathAdded = null, int _saveCount = -1)
		{
			if (img != null)
			{
				Mat mNGDisplayImage = img.Clone();
				Task.Factory.StartNew(delegate
				{
					this.SaveImage(mNGDisplayImage, string.Format("DisplayNGImg_{0}_{1}.jpg", DateTime.Now.ToString("HHmmss.fff"), cellid), SvLogger.eSaveImageType.NGDisplay, _saveCount, _pathAdded);
				}
				);
			}
		}
		public void RetryImage(Mat img)
		{
			if (this.m_bSaveImageNg)
			{
				if (img != null)
				{
					Mat mRetryImage = img.Clone();
					Task.Factory.StartNew(delegate
					{
						this.SaveImage(mRetryImage, string.Format("Retry_{0}.png", DateTime.Now.ToString("HHmmss.fff")), SvLogger.eSaveImageType.Retry, -1, null);
					}
					);
				}
			}
		}
		public void TapeImage(Mat img)
		{
			if (this.m_bSaveImageNg)
			{
				if (img != null)
				{
					Mat src = img.Clone();
					Task.Factory.StartNew(delegate
					{
						this.SaveImage(src, string.Format("Tape_{0}.png", DateTime.Now.ToString("HHmmss.fff")), SvLogger.eSaveImageType.Tape, -1, null);
					}
					);
				}
			}
		}
		private void SaveImage(Mat img, string fileName, SvLogger.eSaveImageType _saveimagetype, int saveCount = -1, string path = null)
		{
			try
			{
				string temp;
				switch (_saveimagetype)
				{
					case SvLogger.eSaveImageType.OK:
						temp = this.m_strOK;
						break;

					case SvLogger.eSaveImageType.NG:
						temp = this.m_strNG;
						break;

					case SvLogger.eSaveImageType.OKDisplay:
						temp = this.m_strOKDisplay;
						break;

					case SvLogger.eSaveImageType.NGDisplay:
						temp = this.m_strNGDisplay;
						break;

					case SvLogger.eSaveImageType.Retry:
						temp = this.m_strRE;
						break;

					default:
						temp = this.m_strTP;
						break;
				}
				if (path != null)
				{
					temp = Path.Combine(temp, path);
				}
				string ImgPath = Path.Combine(temp, DateTime.Now.ToString("yyyyMMdd"));
				if (saveCount > 0)
				{
					this.RollOverImage(temp, false, saveCount);
				}
				if (!Directory.Exists(ImgPath))
				{
					Directory.CreateDirectory(ImgPath);
				}
				img.SaveImage(Path.Combine(ImgPath, fileName), new ImageEncodingParam[]
				{
					new ImageEncodingParam(ImwriteFlags.PngCompression, saveCount)
				});
			}
			catch
			{
				this.log.Debug("DEBUG," + fileName);
			}
			finally
			{
				img.Dispose();
			}
		}
		public void RollOverLog()
		{
			this.RollOverDate(this.log, true);
		}
		private void RollOverDate(ILog log)
		{
			this.RollOverDate(log, false);
		}
		public void RollOverDate(ILog log, bool bInitFlag)
		{
			try
			{
				if (DateTime.Now.Subtract(this.m_Today_Log).TotalDays >= 1.0 || bInitFlag)
				{
					if (Directory.Exists(this.m_strLog))
					{
						string[] strLogFiles = Directory.GetFiles(this.m_strLog);
						int iCountOfFilesInDir = strLogFiles.GetLength(0);
						if (iCountOfFilesInDir > 1)
						{
							int iOverCount = iCountOfFilesInDir - this.m_imaxSizeDateBackups;
							if (iOverCount > 0)
							{
								for (int i = 0; i < iCountOfFilesInDir; i++)
								{
									for (int j = iCountOfFilesInDir - 1; j > i; j--)
									{
										if (strLogFiles[j - 1].CompareTo(strLogFiles[j]) > 0)
										{
											string strTemp = strLogFiles[j - 1];
											strLogFiles[j - 1] = strLogFiles[j];
											strLogFiles[j] = strTemp;
										}
									}
								}
								for (int i = 0; i < iOverCount; i++)
								{
									if (File.Exists(strLogFiles[i]))
									{
										File.Delete(strLogFiles[i]);
									}
								}
							}
							if (!bInitFlag)
							{
								this.m_Today_Log = DateTime.Now;
							}
						}
					}
				}
			}
			catch (Exception deleteEx)
			{
				Console.WriteLine(deleteEx.ToString());
			}
		}
		private void CleanLogFolder()
		{
			if (!Directory.Exists(this.m_strLog))
			{
				Directory.CreateDirectory(this.m_strLog);
			}
			string[] strLogFiles = Directory.GetFiles(this.m_strLog);
			string[] array = strLogFiles;
			for (int i = 0; i < array.Length; i++)
			{
				string strLogFile = array[i];
				string strLogFileExtension = Path.GetExtension(strLogFile);
				if (strLogFileExtension.ToLower() != ".log")
				{
					File.Delete(strLogFile);
				}
			}
		}
		public void RollOverImage(string strDir, bool bInitFlag, int maxCount)
		{
			try
			{
				string[] strImageDirs = Directory.GetDirectories(strDir);
				int iCountOfDir = strImageDirs.GetLength(0);
				if (iCountOfDir > 1)
				{
					int iOverCount = iCountOfDir - maxCount;
					if (iOverCount > 0)
					{
						for (int i = 0; i < iCountOfDir; i++)
						{
							for (int j = iCountOfDir - 1; j > i; j--)
							{
								if (strImageDirs[j - 1].CompareTo(strImageDirs[j]) > 0)
								{
									string strTemp = strImageDirs[j - 1];
									strImageDirs[j - 1] = strImageDirs[j];
									strImageDirs[j] = strTemp;
								}
							}
						}
						for (int i = 0; i < iOverCount; i++)
						{
							if (Directory.Exists(strImageDirs[i]))
							{
								Directory.Delete(strImageDirs[i], true);
							}
						}
					}
					if (!bInitFlag)
					{
						this.m_Today_Image = DateTime.Now;
					}
				}
			}
			catch (Exception deleteEx)
			{
				Console.WriteLine(deleteEx.ToString());
			}
		}
		public void SetSaveImageOkEnable(bool Enable)
		{
			this.m_bSaveImageOk = Enable;
		}
		public void SetSaveImageNgEnable(bool Enable)
		{
			this.m_bSaveImageNg = Enable;
		}
		public void SetSaveDisplayImageOkEnable(bool Enable)
		{
			this.m_bSaveDisplayImageOk = Enable;
		}
		public void SetSaveDisplayImageNgEnable(bool Enable)
		{
			this.m_bSaveDisplayImageNg = Enable;
		}
		public void SetSaveLogEnable(bool Enable)
		{
			this.m_bSaveLog = Enable;
		}
		public void SetSaveUseDIOEnable(bool Enable)
		{
			this.m_bUseDIO = Enable;
		}
		public void SetSaveSkipAlignEnable(bool Enable)
		{
			this.m_bSkipAlign = Enable;
		}
		public void SetChangeTapePosEnable(bool Enable)
		{
			this.m_bChangeTapePos = Enable;
		}
		public void SetSaveMaxImage(int max)
		{
			this.m_imaxDayImages = max;
		}
		public void SetSaveMaxImageOk(int max)
		{
			this.m_imaxDayOkImages = max;
		}
		public void SetSaveMaxImageNg(int max)
		{
			this.m_imaxDayNgImages = max;
		}
		public void SetSaveMaxDisplayImageOk(int max)
		{
			this.m_imaxDayOkDisplayImages = max;
		}
		public void SetSaveMaxDisplayImageNg(int max)
		{
			this.m_imaxDayNgDisplayImages = max;
		}
		public void SetSaveMaxLog(int max)
		{
			this.m_imaxSizeDateBackups = max;
		}
	}
}
