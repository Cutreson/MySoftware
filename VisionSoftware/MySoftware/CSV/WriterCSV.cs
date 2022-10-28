using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySoftware.CSV
{
    public class WriterCSV
    {
        private StreamWriter streamWriter;
        string strFoder = "D:\\Data\\Excel";
        string strPath = "D:\\Data\\Excel\\";
        string strFile;
        private void CreateFoder()
        {
            if (!Directory.Exists(strFoder))
            {
                Directory.CreateDirectory(strFoder);
            }
        }
        private void CreateFileCSV()
        {
            CreateFoder();
            DateTime dateTime = DateTime.UtcNow.Date;        
            strFile = string.Format("Result_{0}.csv", dateTime.ToString("dd.MM.yyyy"));
            if (!File.Exists(strPath + strFile))
            {
                try
                {
                    streamWriter = new StreamWriter(new FileStream(strPath + strFile, FileMode.Create));
                }
                catch
                {
                    return;
                }
                streamWriter.WriteLine("CSV DATA");
                string sLine = "Info 1, Info 2, Info 3";
                streamWriter.WriteLine(sLine);
            }
            else
            {
                try
                {
                    streamWriter = new StreamWriter(new FileStream(strPath + strFile, FileMode.Append));
                }
                catch
                {
                    streamWriter.Close();
                    return;
                }
            }
            
        }
        public void WriteFileCSV()
        {
            CreateFileCSV();
            try
            {
                string sData = string.Format("{0},{1},{2}", DataCSV.dataWrite[0], DataCSV.dataWrite[1], DataCSV.dataWrite[2]);
                streamWriter.WriteLine(sData);
                streamWriter.Close();
            }
            catch
            {
                streamWriter.Close();
                return;
            }
        }
    }
}
