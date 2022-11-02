using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.IO;

namespace MySoftware
{
    //------------------------------------------------------------
    // Kết nối MC Protocol 3E với PLC:
    // - Cài đặt Comment trong file Commnunication\PLCCommandSettings.csv => Trả ra event ReceivedCommandFromServerEvent(command_name) với xung lên PLC
    // - Khai báo (Địa chỉ Input, số Word, địa chỉ Output, số Word)
    // - Đọc gửi liên tục PLC => ReceiveDataArr, SendDataArr => PLC
    //------------------------------------------------------------
    public class PLC3eClient
    {
        private TcpClient PLCSocket;
        private Thread ContinuesReceiveThread;
        private IPEndPoint ServerEndPoint;
        public bool Connected { get; set; }
        //------------------------------------------------------------
        // Địa chỉ, độ dài vùng dữ liệu truyền thông với PLC
        //------------------------------------------------------------
        public int OutputToPLCAddress { get; set; }
        public int InputOffset { get; set; }
        public int InputFromPLCAddress { get; set; }
        public int OutputOffset { get; set; }
        public int[] ReceiveDataArr { get; set; }
        public int[] SendDataArr { get; set; }
        private bool[][] CurrentBitArr;
        private bool[][] OldBitArr;
        //------------------------------------------------------------
        // Command Array
        //------------------------------------------------------------
        public string[][] CommandNameArr { get; set; }
        //------------------------------------------------------------
        // Event mất kết nối TCP
        //------------------------------------------------------------
        public delegate void TCP4DisconnectEventHandle();
        public TCP4DisconnectEventHandle ServerDisconnectEvent;
        //------------------------------------------------------------
        // Event gửi dữ liệu khi nhận được qua TCP
        //------------------------------------------------------------
        public delegate void TCP4ReceivedCommandEventHandle(string commandString);
        public TCP4ReceivedCommandEventHandle ReceivedCommandFromServerEvent;

        //------------------------------------------------------------
        // Khởi tạo
        //------------------------------------------------------------
        public PLC3eClient()
        {
            PLCSocket = null;

            //------------------------------------------------------------
            // Khởi tạo mảng lưu command PLC
            //------------------------------------------------------------
            CommandNameArr = new string[10][];
            for (int i = 0; i < 10; i++)
            {
                CommandNameArr[i] = new string[16];
            }
            //------------------------------------------------------------
            // Load dữ liệu command từ file plcCommands.csv
            //------------------------------------------------------------
            UpdateCommandToArrayFromFile();
        }

        public PLC3eClient(string plcAddress, int plcPort, string dataIn, int dataInLength, string dataOut, int dataOutLenght) : this()
        {
            UpdateDataLinkAddress(dataIn, dataInLength, dataOut, dataOutLenght);
            PLCClientInit(plcAddress, plcPort);
        }

        public PLC3eClient(string plcAddress, int plcPort)
        {
            PLCClientInit(plcAddress, plcPort);
        }

        //------------------------------------------------------------
        // Cập nhật tên command khai báo trong file csv
        //------------------------------------------------------------
        private void UpdateCommandToArrayFromFile()
        {
            string commandFileUrl = System.AppDomain.CurrentDomain.BaseDirectory + "Communication\\PLCCommandSettings.csv";
            if (File.Exists(commandFileUrl))
            {
                string[] commandStrings = File.ReadAllLines(commandFileUrl);
                int countNumberCommand = 0;
                foreach (string commandLine in commandStrings)
                {
                    if (!commandLine.Contains(',')) continue;
                    string[] detail = commandLine.Split(',');
                    try
                    {
                        string command = detail[0];
                        int index1 = Int32.Parse(detail[1]);
                        int index2 = Int32.Parse(detail[2]);
                        CommandNameArr[index1][index2] = command;
                        countNumberCommand += 1;
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
        }

        //------------------------------------------------------------
        // 1. Kết nối đến địa chỉ IP/Port của PLC tương ứng
        // 2. Mở thread nhận dữ liệu liên tục từ socket vừa kết nối
        //------------------------------------------------------------
        public void PLCClientInit(string ipAddress, int port)
        {
            //------------------------------------------------------------
            // Đóng socket cũ nếu đã tồn tại
            //------------------------------------------------------------
            
            //------------------------------------------------------------
            // Kết nối đến socket
            //------------------------------------------------------------
            try
            {
                if (PLCSocket != null)
                {
                    PLCSocket.Client.Shutdown(SocketShutdown.Both);
                    PLCSocket.Close();
                    //PLCSocket.Dispose();
                }

                IPAddress serverAddress = IPAddress.Parse(ipAddress);
                ServerEndPoint = new IPEndPoint(serverAddress, port);
                PLCSocket = new TcpClient(AddressFamily.InterNetwork);
                PLCSocket.Connect(ServerEndPoint);
                PLCSocket.ReceiveTimeout = 1000;
                Connected = true;
            }
            catch (Exception ex)
            {
                Connected = false;
            }
            //------------------------------------------------------------
            // Nếu đã kết nối thành công, tạo Thread nhận dữ liệu từ socket
            //------------------------------------------------------------
            //if (Connected)
            //{
            //    ContinuesReceiveThread = new Thread(new ThreadStart(ContinuesReceiveFromTCPSocket));
            //    ContinuesReceiveThread.IsBackground = true;
            //    ContinuesReceiveThread.Start();
            //}
        }

        //------------------------------------------------------------
        // Nhận dữ liệu từ Server và gửi Event
        //------------------------------------------------------------
        private void ContinuesReceiveFromTCPSocket()
        {

            while (true)
            {
                try
                {
                    if (false)
                    {
                        if ((InputFromPLCAddress < 0) || (OutputToPLCAddress < 0)) continue;
                        //------------------------------------------------------------
                        // Get Data From PLC
                        //------------------------------------------------------------
                        ReceiveDataArr = ReceiveDataFromPLC(InputFromPLCAddress, InputOffset);
                        if (CurrentBitArr == null)
                        {
                            OldBitArr = PLC3e.ConvertWordToBitArray(ReceiveDataArr, 5);
                        }
                        else
                        {
                            OldBitArr = (bool[][])CurrentBitArr.Clone();
                        }
                        CurrentBitArr = PLC3e.ConvertWordToBitArray(ReceiveDataArr, 5);
                        //------------------------------------------------------------
                        // Calculate Raise Pulse and Send Event
                        //------------------------------------------------------------
                        bool[][] pulseBitArr = PLC3e.CompareBitArrarToRaisePulse(OldBitArr, CurrentBitArr);
                        for (int i = 0; i < pulseBitArr.Length; i++)
                        {
                            for (int j = 0; j < 16; j++)
                            {
                                if (pulseBitArr[i][j])
                                {
                                    if (CommandNameArr[i][j] != null)
                                    {
                                        ReceivedCommandFromServerEvent?.Invoke(CommandNameArr[i][j]);
                                    }
                                }
                            }
                        }
                    }
                    //------------------------------------------------------------
                    // Send Data To PLC
                    //------------------------------------------------------------
                    //SendDataToPLC(SendDataArr, OutputToPLCAddress);
                    SendDataToPLC(new int[] { 2 }, 200);
                    SendDataToPLC(new int[] { 3 }, 201);
                    SendDataToPLC(new int[] { 4 }, 202);
                    int[] temp = ReceiveDataFromPLC(200, 3);
                    if (temp == null)
                    {

                    }
                    foreach (var item in temp)
                    {
                        Console.Write(item.ToString());
                    }
                    Console.WriteLine("===============");

                }
                catch
                {

                }
                finally
                {
                    //Thread.Sleep(10);
                }
            }
        }

        //------------------------------------------------------------
        // Nhận dữ liệu từ PLC. Bắt đầu từ wordAddress, số lượng Word: numberData
        //------------------------------------------------------------
        public int[] ReceiveDataFromPLC(int wordAddress, int numberData)
        {
            //------------------------------------------------------------
            // Tạo stream từ kết nối TCP Client
            //------------------------------------------------------------ 
            NetworkStream stream = PLCSocket.GetStream();
            //------------------------------------------------------------
            // Tạo dữ liệu đúng định dạng
            // Gửi qua kết nối stream sang PLC
            //------------------------------------------------------------
            byte[] sent_Data = PLC3e.Cal_ReadFromPLCMessage_Dx_Multi_3E_Frame(wordAddress, numberData);
            stream.Write(sent_Data, 0, sent_Data.Length);
            stream.Flush();
            //------------------------------------------------------------
            // Nhận dữ liệu xác nhận từ PLC
            //------------------------------------------------------------
            byte[] receive_Data = new byte[0x3e8];
            stream.ReadTimeout = 500;
            Thread.Sleep(10);
            try
            {
                stream.Read(receive_Data, 0, 0x3e8);
            }
            catch (Exception ex)
            {

                return null;
            }
            //try
            //{
            //    stream.Read(receive_Data, 0, 0x3e8);
            //}
            //catch { return null; }
            //------------------------------------------------------------
            // Check xác nhận từ PLC
            // So sánh chuỗi để xác nhận không có lỗi gửi nhận
            //------------------------------------------------------------
            byte[] compareArr = { 0xD0, 0x00, 0x00, 0xFF, 0xFF, 0x03, 0x00, 0x02, 0x00, 0x00, 0x00 };
            Array.Copy(BitConverter.GetBytes(numberData * 2 + 2), 0, compareArr, 7, 2);
            byte[] receiveArr = new byte[compareArr.Length];
            Array.Copy(receive_Data, receiveArr, receiveArr.Length);
            if (!receiveArr.SequenceEqual(compareArr)) return null;
            //------------------------------------------------------------
            // Tách dữ liệu PLC từ chuỗi nhận về
            //------------------------------------------------------------
            byte[] dataOutArr_byte = new byte[numberData * 2];
            Array.Copy(receive_Data, compareArr.Length, dataOutArr_byte, 0, dataOutArr_byte.Length);
            //------------------------------------------------------------
            // Chuyển dữ liệu về kiểu interger
            //------------------------------------------------------------
            int[] dataOutArr = new int[numberData];
            for (int i = 0; i < numberData; i++)
            {
                dataOutArr[i] = (int)BitConverter.ToInt16(dataOutArr_byte, i * 2);
            }
            //------------------------------------------------------------
            // Gửi chuỗi interger đọc từ PLC
            //------------------------------------------------------------
            return dataOutArr;
        }


        public byte[] GetBitData(int wordAddress, int numberData, bool is16Bit = true)
        {
            int Data = ReceiveDataFromPLC(wordAddress, numberData)[0];
            byte[] value = new byte[16];
            for (int i1 = 0; i1 < 16; i1++)
            {
                if (Data % 2 == 1)
                    value[i1] = 1;
                else
                    value[i1] = 0;
                Data = Data / 2;
            }
            return value;

            //byte[] value;
            //value = BitConverter.GetBytes(System.Convert.ToInt32(Data));
            //var result = Convert.ToString((data[0]), 2);
            //
        }

        public string ReceiveDataStringFromPLC(int wordAddress, int numberData)
        {
            //------------------------------------------------------------
            // Tạo stream từ kết nối TCP Client
            //------------------------------------------------------------ 
            NetworkStream stream = PLCSocket.GetStream();
            //------------------------------------------------------------
            // Tạo dữ liệu đúng định dạng
            // Gửi qua kết nối stream sang PLC
            //------------------------------------------------------------
            byte[] sent_Data = PLC3e.Cal_ReadFromPLCMessage_Dx_Multi_3E_Frame(wordAddress, numberData);
            stream.Write(sent_Data, 0, sent_Data.Length);
            stream.Flush();
            //------------------------------------------------------------
            // Nhận dữ liệu xác nhận từ PLC
            //------------------------------------------------------------
            byte[] receive_Data = new byte[0x3e8];
            stream.ReadTimeout = 500;
            Thread.Sleep(10);
            try
            {
                stream.Read(receive_Data, 0, 0x3e8);
            }
            catch (Exception ex)
            {

                return null;
            }
            //try
            //{
            //    stream.Read(receive_Data, 0, 0x3e8);
            //}
            //catch { return null; }
            //------------------------------------------------------------
            // Check xác nhận từ PLC
            // So sánh chuỗi để xác nhận không có lỗi gửi nhận
            //------------------------------------------------------------
            byte[] compareArr = { 0xD0, 0x00, 0x00, 0xFF, 0xFF, 0x03, 0x00, 0x02, 0x00, 0x00, 0x00 };
            Array.Copy(BitConverter.GetBytes(numberData * 2 + 2), 0, compareArr, 7, 2);
            byte[] receiveArr = new byte[compareArr.Length];
            Array.Copy(receive_Data, receiveArr, receiveArr.Length);
            if (!receiveArr.SequenceEqual(compareArr)) return null;
            //------------------------------------------------------------
            // Tách dữ liệu PLC từ chuỗi nhận về
            //------------------------------------------------------------
            byte[] dataOutArr_byte = new byte[numberData * 2];
            Array.Copy(receive_Data, compareArr.Length, dataOutArr_byte, 0, dataOutArr_byte.Length);
            //------------------------------------------------------------
            // Chuyển dữ liệu về kiểu interger
            //------------------------------------------------------------
            int[] dataOutArr = new int[numberData];
            for (int i = 0; i < numberData; i++)
            {
                dataOutArr[i] = (int)BitConverter.ToInt16(dataOutArr_byte, i * 2);
            }
            //------------------------------------------------------------
            // Gửi chuỗi interger đọc từ PLC
            //------------------------------------------------------------
            string dataOut = PLC3e.GetStringFromWordArr(dataOutArr, 0, numberData);
            string outputPLC = Encoding.Default.GetString(dataOutArr_byte);
            return outputPLC;
        }

        //------------------------------------------------------------
        // Gửi data sang PLC dạng Array Word
        //------------------------------------------------------------
        public bool SendDataToPLC(int[] wordData, int wordAddress)
        {
            // Gửi dữ liệu sang PLC
            // Tạo stream từ kết nối TCP Client
            NetworkStream stream = PLCSocket.GetStream();

            // Tạo dữ liệu và gửi qua kết nối stream sang PLC
            byte[] sent_Data = PLC3e.Cal_WriteToPLCMessage_Dx_Multi_3E_Frame(wordAddress, wordData);
            stream.Write(sent_Data, 0, sent_Data.Length);
            stream.Flush();

            // Nhận dữ liệu xác nhận từ PLC
            byte[] receive_Data = new byte[0x3e8];
            stream.ReadTimeout = 500;
            try
            {
                stream.Read(receive_Data, 0, 0x3e8);
            }
            catch
            {
                return false;
            }
            // Check xác nhận từ PLC
            byte[] compareArr = { 0xD0, 0x00, 0x00, 0xFF, 0xFF, 0x03, 0x00, 0x02, 0x00, 0x00, 0x00 };
            byte[] receiveArr = new byte[compareArr.Length];
            Array.Copy(receive_Data, receiveArr, receiveArr.Length);
            if (!receiveArr.SequenceEqual(compareArr)) return false;

            return true;
        }

        //------------------------------------------------------------
        // Cập nhật địa chỉ gửi nhận dữ liệu với PLC
        //------------------------------------------------------------
        public void UpdateDataLinkAddress(string dataIn, int inOffset, string dataOut, int outOffset)
        {
            try
            {
                OutputToPLCAddress = Int32.Parse(dataOut.Substring(1));
                SendDataArr = new int[outOffset];
            }
            catch
            {
                InputFromPLCAddress = -1;
            }
            try
            {
                InputFromPLCAddress = Int32.Parse(dataIn.Substring(1));
                ReceiveDataArr = new int[inOffset];
            }
            catch
            {
                InputFromPLCAddress = -1;
            }
            InputOffset = inOffset;
            OutputOffset = outOffset;
        }

        //------------------------------------------------------------
        // Kiểm tra kết nối khi không nhận được chuỗi về
        //------------------------------------------------------------
        private bool CheckConnection()
        {
            //------------------------------------------------------------
            // checker1 kiểm tra kết nối, true nếu Closed, hoặc Data availble
            // checker2 kiểm tra true nếu không có Data availble
            //------------------------------------------------------------
            bool checker1 = false;
            bool checker2 = false;
            try
            {

                checker1 = PLCSocket.Client.Poll(1000, SelectMode.SelectRead);
                checker2 = (PLCSocket.Available == 0);
            }
            catch { }

            // Nếu cả 2 cùng true => Kết nối đang đóng
            if (checker1 && checker2)
            {
                Connected = false;
                return false;
            }
            else
            {
                //------------------------------------------------------------
                // Kiểm tra lại bằng cách gửi dữ liệu (kiểm tra đứt cáp, ngắt đột ngột phía host)
                //------------------------------------------------------------
                try
                {
                    PLCSocket.Client.Send(new byte[] { 1 });
                }
                catch
                {
                    Connected = false;
                    return false;
                }
                Connected = true;
                return true;
            }
        }

        //------------------------------------------------------------
        // Thử kết nối lại vào Server 5 lần
        //------------------------------------------------------------
        private void Reconnect5Times()
        {
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    //------------------------------------------------------------
                    // Đóng socket cũ nếu đã tồn tại
                    //------------------------------------------------------------
                    if (PLCSocket != null)
                    {
                        PLCSocket.Client.Shutdown(SocketShutdown.Both);
                        PLCSocket.Close();
                        //PLCSocket.Dispose();
                    }
                    //------------------------------------------------------------
                    // Kết nối đến socket
                    //------------------------------------------------------------
                    try
                    {
                        PLCSocket = new TcpClient(AddressFamily.InterNetwork);
                        PLCSocket.Connect(ServerEndPoint);
                        PLCSocket.ReceiveTimeout = 1000;
                        Connected = true;
                        break;
                    }
                    catch
                    {
                        Connected = false;
                    }
                }
                catch
                {

                }
            }
        }

        public bool CloseSecket()
        {
            bool data = false;
            if (PLCSocket != null)
            {
                try
                {
                    PLCSocket.Client.Shutdown(SocketShutdown.Both);
                    PLCSocket.Close();
                    //PLCSocket.Dispose();
                    data = true;
                }
                catch (Exception)
                {
                    data = false;
                }

            }
            return data;
        }

        public bool CheckSocketConnect()
        {
            bool data = true;
            if (PLCSocket != null)
            {
                try
                {
                    if (PLCSocket.Connected == false)
                    {
                        data = false;
                    }
                }
                catch (Exception)
                {
                    data = false;
                }
            }
            return data;
        }

        private string GetStringfromPLC(int[] data, int length)
        {
            byte[] arrDataGet = new byte[length * 2];
            byte[] arrTemp;
            int index = 0;
            for (index = 0; index < length; index++)
            {
                arrTemp = BitConverter.GetBytes(data[index]);
                arrDataGet[0] = arrTemp[2 * index];
                arrDataGet[1] = arrTemp[2 * index + 1];
            }
            string dataOut = "";
            return dataOut;
        }
        private string ReadStringFromPLC(short[] dataIN, int lenghtWord)
        {
            byte[] BufferStringByte = new byte[lenghtWord * 2];
            byte[] byarrTemp;
            int iNumber;
            for (iNumber = 0; iNumber <= lenghtWord - 1; iNumber++)
            {
                byarrTemp = BitConverter.GetBytes(dataIN[iNumber]);

                BufferStringByte[iNumber * 2] = byarrTemp[0];
                BufferStringByte[iNumber * 2 + 1] = byarrTemp[1];
            }
            string outputPLC = Encoding.Default.GetString(BufferStringByte);
            return outputPLC;
        }
    }
}
