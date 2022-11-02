using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MySoftware
{
    public class TCP1EClient
    {
        public Socket soket;/* = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);*/
        public string IP;
        public int Port;
        public TCP1EClient()
        {
            IP = "127.0.0.1";
            Port = 3000;
        }
        public TCP1EClient(string ip, int port)
        {
            IP = ip;
            Port = port;
        }
        public bool Connect()
        {
            if (soket != null)
            {
                soket.Close();
                soket = null;
            }

            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(IP), Port);
            soket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            bool ret = false;
            if (soket.Connected == false)
            {
                try
                {
                    soket.Connect(ipe);
                    ret = soket.Connected;
                    ret = IsConnected;
                    //WriteDWord("D1501", 205);
                    //int data = ReadDWord("D1500");
                }
                catch
                {
                    ret = false;

                }
            }
            else
            {
                ret = true;
            }
            return ret;
        }
        public bool IsConnected
        {
            //get
            //{
            //    try
            //    {
            //        if (soket == null)
            //            return false;
            //        bool part1 = soket.Poll(1000, SelectMode.SelectRead);
            //        bool part2 = (soket.Available == 0);
            //        if (part1 && part2)
            //            return false;
            //        else
            //            return true;
            //    }
            //    catch
            //    {
            //        return false;
            //    }
            //}
            get
            {
                if (soket == null)
                {
                    return false;
                }
                else
                    return soket.Connected;
            }
        }
        public bool Disconnect()
        {
            bool ret = false;
            if (soket.Connected == true)
            {
                try
                {
                    soket.Close();
                    ret = true;
                }
                catch
                {

                }
            }
            else
            {
                ret = true;
            }
            return ret;
        }
        private bool SendData(string s)
        {
            bool ret = false;
            if (soket.Connected == false)
            {
                Connect();
            }
            //receive_data(cn);
            if (soket.Connected)
            {
                try
                {
                    byte[] data_sent = Encoding.ASCII.GetBytes(s);

                    int result_s = soket.Send(data_sent, SocketFlags.None);
                    //ns.Write(data_sent, 0, data_sent.Length);
                    //ns.Flush();

                    if (result_s == data_sent.Length)
                    {
                        ret = true;
                    }

                    // ret = true;
                }
                catch (Exception ex)
                {

                }
            }
            return ret;

        }

        private string ReceiveData()
        {
            string s = "";
            if (soket.Connected == false)
            {
                Connect();
            }
            if (soket.Connected)
            {
                try
                {
                    byte[] data_receive = new byte[4096];
                    int rev = soket.Receive(data_receive);
                    // ns.Read(data_receive, 0, data_receive.Length);
                    s = Encoding.ASCII.GetString(data_receive);
                }
                catch
                {

                }
            }
            return s;
        }
        private string CommandData_Write(string addr, int numDevice)
        {
            string data_to_sent = "", adr = "", device = "";
            string de = addr.Substring(0, 1);
            switch (de)
            {
                case "D":
                    {
                        device = "4420";
                        break;
                    }
                case "R":
                    {
                        device = "5220";
                        break;
                    }
                case "X":
                    {
                        device = "5820";
                        break;
                    }
                case "Y":
                    {
                        device = "5920";
                        break;
                    }
                case "M":
                    {
                        device = "4D20";
                        break;
                    }
                case "S":
                    {
                        device = "5320";
                        break;
                    }
                case "d":
                    {
                        device = "4420";
                        break;
                    }
                case "r":
                    {
                        device = "5220";
                        break;
                    }
                case "x":
                    {
                        device = "5820";
                        break;
                    }
                case "y":
                    {
                        device = "5920";
                        break;
                    }
                case "m":
                    {
                        device = "4D20";
                        break;
                    }
                case "s":
                    {
                        device = "5320";
                        break;
                    }
                default: break;
            }

            if (device == "")
            {
                de = addr.Substring(0, 2);
                switch (de)
                {
                    case "TN":
                        {
                            device = "544e";
                            break;
                        }
                    case "TS":
                        {
                            device = "5453";
                            break;
                        }
                    case "CN":
                        {
                            device = "434E";
                            break;
                        }
                    case "CS":
                        {
                            device = "4353";
                            break;
                        }
                    case "tn":
                        {
                            device = "544e";
                            break;
                        }
                    case "ts":
                        {
                            device = "5453";
                            break;
                        }
                    case "cn":
                        {
                            device = "434E";
                            break;
                        }
                    case "cs":
                        {
                            device = "4353";
                            break;
                        }
                    default: break;
                }
                if (device != "")
                {
                    adr = addr.Substring(2, addr.Length - 1);
                }

            }
            else
            {
                adr = addr.Substring(1, addr.Length - 1);
            }

            if (device != "")
            {
                int adr_hex = Convert.ToInt16(adr);
                string adr_str = (adr_hex.ToString("x")).PadLeft(8, '0');
                string nub_str = (((2 * numDevice).ToString("x")).PadLeft(2, '0'));

                //string data_set = "";
                //for (int i = 0; i < number_write; i++)
                //{
                //    data_set += ((data_out[i] % 65536).ToString("x")).PadLeft(4, '0') + ((data_out[i] / 65536).ToString("x")).PadLeft(4, '0');
                //}
                //data_to_sent = ("03FF000A" + device + adr_str + nub_str + "00" + data_set).ToUpper();
                data_to_sent = ("03FF000A" + device + adr_str + nub_str + "00").ToUpper();
            }
            return data_to_sent;
        }
        private string CommandData_Read(string addr, int numDevice)
        {
            string data_to_sent = "", adr = "", device = "";
            string de = addr.Substring(0, 1);
            switch (de)
            {
                case "D":
                    {
                        device = "4420";
                        break;
                    }
                case "R":
                    {
                        device = "5220";
                        break;
                    }
                case "X":
                    {
                        device = "5820";
                        break;
                    }
                case "Y":
                    {
                        device = "5920";
                        break;
                    }
                case "M":
                    {
                        device = "4D20";
                        break;
                    }
                case "S":
                    {
                        device = "5320";
                        break;
                    }
                case "d":
                    {
                        device = "4420";
                        break;
                    }
                case "r":
                    {
                        device = "5220";
                        break;
                    }
                case "x":
                    {
                        device = "5820";
                        break;
                    }
                case "y":
                    {
                        device = "5920";
                        break;
                    }
                case "m":
                    {
                        device = "4D20";
                        break;
                    }
                case "s":
                    {
                        device = "5320";
                        break;
                    }
                default: break;
            }

            if (device == "")
            {
                de = addr.Substring(0, 2);
                switch (de)
                {
                    case "TN":
                        {
                            device = "544e";
                            break;
                        }
                    case "TS":
                        {
                            device = "5453";
                            break;
                        }
                    case "CN":
                        {
                            device = "434E";
                            break;
                        }
                    case "CS":
                        {
                            device = "4353";
                            break;
                        }
                    case "tn":
                        {
                            device = "544e";
                            break;
                        }
                    case "ts":
                        {
                            device = "5453";
                            break;
                        }
                    case "cn":
                        {
                            device = "434E";
                            break;
                        }
                    case "cs":
                        {
                            device = "4353";
                            break;
                        }
                    default: break;
                }
                if (device != "")
                {
                    adr = addr.Substring(2, addr.Length - 1);
                }

            }
            else
            {
                adr = addr.Substring(1, addr.Length - 1);
            }

            if (device != "")
            {
                int adr_hex = Convert.ToInt16(adr);
                string adr_str = (adr_hex.ToString("x")).PadLeft(8, '0');
                string nub_str = (((2 * numDevice).ToString("x")).PadLeft(2, '0'));

                //string data_set = "";
                //for (int i = 0; i < number_write; i++)
                //{
                //    data_set += ((data_out[i] % 65536).ToString("x")).PadLeft(4, '0') + ((data_out[i] / 65536).ToString("x")).PadLeft(4, '0');
                //}
                //data_to_sent = ("03FF000A" + device + adr_str + nub_str + "00" + data_set).ToUpper();
                data_to_sent = ("01FF000A" + device + adr_str + nub_str + "00").ToUpper();
            }
            return data_to_sent;
        }
        public bool WriteDWord_Arr(string addr, int[] data_out, int number_write)
        {
            string command = "";
            string sDataSend = "";
            CommandData_Write(addr, number_write);
            if (command == "")
            {
                return false;
            }
            else
            {
                string dataSet = "";
                for (int i = 0; i < number_write; i++)
                {
                    dataSet += ((data_out[i] % 65536).ToString("x")).PadLeft(4, '0') + ((data_out[i] / 65536).ToString("x")).PadLeft(4, '0');
                }
                sDataSend = command + dataSet;
                bool rs = SendData(sDataSend);
                return rs;
            }
        }
        public bool WriteDWord(string addr, int data_out)
        {
            int number_write = 1;
            string command = "";
            string sDataSend = "";
            command = CommandData_Write(addr, number_write);
            if (command == "")
            {
                return false;
            }
            else
            {
                string dataSet = "";
                dataSet += ((data_out % 65536).ToString("x")).PadLeft(4, '0') + ((data_out / 65536).ToString("x")).PadLeft(4, '0');
                sDataSend = command + dataSet;

                bool rs = SendData(sDataSend);

                Thread.Sleep(30);
                string r;
                if (rs)
                {
                    lock (oLock2)
                    {
                        r = ReceiveData();
                    }
                }
               
                return rs;
            }
        }
        object oLock = new object();
        object oLock2 = new object();
        int count = 0;
        public int[] ReadDataPLC(string addr, int numDevice)
        {
            Retry:
            int[] read_result = new int[numDevice + 1];
            read_result[0] = 0;
            string command = "";
            string sDataSend = "";
            command = CommandData_Read(addr, numDevice);
            if (command == "")
            {
                return null;
            }
            else
            {
            
                int a = 1;
                bool rs = SendData(command);
                if (rs == false)
                {
                    return null;
                }
                else
                {
                    Thread.Sleep(30);
                    string r;
                    lock (oLock)
                    {
                        r = ReceiveData();
                    }
                    bool bRemove8300Temp = r.Contains("8300");
                    if (bRemove8300Temp & count <= 5)
                    {
                        count++;
                        goto Retry;
                    }
                    count = 0;
                    bool bRemove8300 = r.Contains("8300");
                    while (bRemove8300)
                    {
                        bRemove8300 = r.Contains("8300");
                        if (bRemove8300)
                        {
                            r = r.Replace("8300", "");
                        }
                    }
                    string result = "";
                    try
                    {
                        result = r.Substring(0, 4);
                    }
                    catch
                    {

                    }
                    // MessageBox.Show(result);

                    if ((result) == "8100")
                    {
                        string data = r.Substring(4, (numDevice * 4));
                        int index = 0;
                        for (int i = 0; i < data.Length; i += 4)
                        {
                            string sConvert = data.Substring(i, 4);
                            read_result[index] = Convert.ToInt32(sConvert, 16);
                            //read_result[i] = Convert.ToInt16(sConvert, 8);

                            // MessageBox.Show(read_result[index].ToString());
                            index++;
                        }
                        return read_result;
                    }
                    else
                        return null;
                }
            }
        }
        public int ReadWord(string addr)
        {
            int rs = ReadDataPLC(addr, 1)[0];
            return rs;
        }
        public int ReadDWord(string addr)
        {
            int[] dataTemp = ReadDataPLC(addr, 2);
            int rs = ArrayIntToDword(dataTemp);
            return rs;
        }
        private int ArrayIntToDword(int[] dataIN)
        {
            if (dataIN == null)
            {
                return -1;
            }
            try
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
            catch (Exception)
            {
                return 0;
            }

        }
    }
}
