using System;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace MySoftware.Class.ClassCommon
{
	public class SvIni
	{
		private const int defaultSize = 255;
		public string FilePath;
		private StringBuilder sbBuffer;
		[DllImport("kernel32")]
		private static extern long WritePrivateProfileString(string strSection, string strKey, string strValue, string strFilePath);
		[DllImport("kernel32")]
		private static extern int GetPrivateProfileString(string strSection, string strKey, string strDefault, StringBuilder retVal, int iSize, string strFilePath);
		public SvIni(string filePath)
		{
			this.FilePath = filePath;
			this.sbBuffer = new StringBuilder(255);
			if (!File.Exists(filePath))
			{
				string dc = Path.GetDirectoryName(filePath);
				if (!Directory.Exists(dc))
				{
					Directory.CreateDirectory(dc);
				}
				using (File.Create(filePath))
				{
				}
			}
		}
		public void WriteValue(string section, string key, string value)
		{
			SvIni.WritePrivateProfileString(section, key, value, this.FilePath);
		}
		public void WriteValue(string section, string key, object iValue)
		{
			SvIni.WritePrivateProfileString(section, key, iValue.ToString(), this.FilePath);
		}
		public string ReadValue(string section, string key, string defaultValue)
		{
			this.sbBuffer.Clear();
			SvIni.GetPrivateProfileString(section, key, defaultValue, this.sbBuffer, 255, this.FilePath);
			return this.sbBuffer.ToString();
		}
		public double ReadValue(string section, string key, double defaultValue)
		{
			this.sbBuffer.Clear();
			SvIni.GetPrivateProfileString(section, key, defaultValue.ToString(), this.sbBuffer, 255, this.FilePath);
			double value;
			double result;
			if (double.TryParse(this.sbBuffer.ToString(), out value))
			{
				result = value;
			}
			else
			{
				result = -1.0;
			}
			return result;
		}
	}
}
