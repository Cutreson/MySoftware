using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MySoftware
{
    public class cSLMP
    {
        #region PROPERTIES
        public string IPAddress { get; set; }
        public int Port { get; set; }
        

        #endregion

        #region VARIABLE
        public bool IsConnected = false;
        public PLC3eClient PLC;
        #endregion

        public cSLMP(string IP, int port)
        {
            IPAddress = IP;
            Port = port;
        }
        public cSLMP()
        {
            IPAddress = "127.0.0.1";
            Port = 3000;
        }

        #region FUNCIONS
        public bool Connect()
        {
            bool value = false;
            if (IsConnected) return true;
            if (PLC == null)
            {
                PLC = new PLC3eClient(IPAddress, Port);
                IsConnected = PLC.Connected;
            }
            else
            {
                if (PLC.CheckSocketConnect())
                {
                    return true;
                }
                else
                {
                    PLC.PLCClientInit(IPAddress, Port);
                    IsConnected = PLC.Connected;
                }
            }
            value = IsConnected;
            return value;
        }
        public bool CloseSocket()
        {
            bool value = false;
            if (PLC.CloseSecket()) value = true; else value = false;
            return value;
        }
        public bool SendBit(byte data, string Device)
        {
            try
            {
                if (!Device.Contains(".") || !Device.Contains("D")) return false;
                string[] DeviceAndBit = Device.Split('.');
                string strDeviceNum = DeviceAndBit[0].Replace("D", string.Empty);
                string strBitNum = DeviceAndBit[1];
                if (!Int32.TryParse(strDeviceNum, out int DeviceNum) ||
                    !Int32.TryParse(strBitNum, out int BitNum)) return false;
                byte[] ValueBit = PLC.GetBitData(DeviceNum, 1);
                byte[] ValueBitSend = new byte[ValueBit.Length];

                for (int i = 0; i < ValueBit.Length; i++)
                {
                    if (i == BitNum)
                        ValueBitSend[i] = data;
                    else
                        ValueBitSend[i] = ValueBit[i];
                }

                int[] vs = new int[1];
                for (int i = 0;i < ValueBitSend.Length; i++)
                {
                    if (ValueBitSend[i] == 1)
                    {
                        vs[0] = vs[0] + (int)Math.Pow(2,i);
                    }
                }
                PLC.SendDataToPLC(vs, DeviceNum);

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool SendWord(int[] data, string Device)
        {
            try
            {
                if (!Device.Contains("D")) return false;
                string strDeviceNum = Device.Replace("D", string.Empty);
                if (!Int32.TryParse(strDeviceNum, out int DeviceNum)) return false;
                PLC.SendDataToPLC(data, DeviceNum);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool SendDWord(int[] data, string Device)
        {
            try
            {
                byte[] BufferDwordByte;
                int[] intputPLC = new int[2];
                BufferDwordByte = BitConverter.GetBytes(System.Convert.ToInt32(data[0]));
                intputPLC[0] = BitConverter.ToInt16(BufferDwordByte, 0);
                intputPLC[1] = BitConverter.ToInt16(BufferDwordByte, 2);
               // BitConverter.ToInt32


                if (!Device.Contains("D")) return false;
                string strDeviceNum = Device.Replace("D", string.Empty);
                if (!Int32.TryParse(strDeviceNum, out int DeviceNum)) return false;
                PLC.SendDataToPLC(intputPLC, DeviceNum);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        
        public bool ReceiveBit(string Device, out bool bitReuslt)
        {
            try
            {
                bitReuslt = false;
                if (!Device.Contains(".") || !Device.Contains("D")) return false;
                string[] DeviceAndBit = Device.Split('.');
                string strDeviceNum = DeviceAndBit[0].Replace("D", string.Empty);
                string strBitNum = DeviceAndBit[1];
                if (!Int32.TryParse(strDeviceNum, out int DeviceNum) ||
                    !Int32.TryParse(strBitNum, out int BitNum)) return false;
                byte[] ValueBit = PLC.GetBitData(DeviceNum, 1);
                if (ValueBit[BitNum] == 0)
                    bitReuslt = false;
                else
                    bitReuslt = true;
                return true;
            }
            catch (Exception ex)
            {
                bitReuslt = false;
                return false;
            }
        }
        public bool ReceiveBitArr(string Device, out byte[] bitReuslt)
        {
            try
            {
                bitReuslt = new byte[16];
                if (!Device.Contains("D")) return false;
                string strDeviceNum = Device.Replace("D", string.Empty);
                if (!Int32.TryParse(strDeviceNum, out int DeviceNum)) return false;
                bitReuslt = PLC.GetBitData(DeviceNum, 1);
                return true;
            }
            catch (Exception ex)
            {
                bitReuslt = new byte[16];
                return false;
            }
        }
        public bool ReceiveWord(string Device, out int[] result)
        {
            try
            {
                result = new int[16];
                if (!Device.Contains("D")) return false;
                string strDeviceNum = Device.Replace("D", string.Empty);
                if (!Int32.TryParse(strDeviceNum, out int DeviceNum)) return false;
                result = PLC.ReceiveDataFromPLC(DeviceNum, 1);
                return true;
            }
            catch (Exception ex)
            {
                result = new int[16];
                return false;
            }
        }
        public bool ReceiveDWord(string Device, out int[] result)
        {
            try
            {
                result = new int[32];
                if (!Device.Contains("D")) return false;
                string strDeviceNum = Device.Replace("D", string.Empty);
                if (!Int32.TryParse(strDeviceNum, out int DeviceNum)) return false;
                result = PLC.ReceiveDataFromPLC(DeviceNum, 2);
                result[0] = ArrayIntToDword(result);

                return true;
            }
            catch (Exception ex)
            {
                result = new int[32];
                return false;
            }
        }
        private int ArrayIntToDword(int[] dataIN)
        {
            byte[] byarrBufferByte = new byte[4];
            byte[] byarrTemp;
            int iNumber;
            for (iNumber = 0; iNumber <= 2 - 1; iNumber++)
            {
                byarrTemp = BitConverter.GetBytes(dataIN[iNumber]);
                byarrBufferByte[iNumber * 2] = byarrTemp[0];
                byarrBufferByte[iNumber * 2 + 1] = byarrTemp[1];
            }
            int outputPLC = System.Convert.ToInt32(BitConverter.ToInt32(byarrBufferByte, 0));
            return outputPLC;
        }
        #endregion
    }
}
