using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MySoftware
{
    /// <summary>
    /// Đối tượng đảm nhiệm vai trò khởi tạo môi trường máy chủ
    /// </summary>
    public class TCPServer
    {
        public delegate void ReceiveDataEvents(string _Data);
        /// <summary>
        /// Variable
        /// </summary>
        #region VARIABLES
        Socket serverSocket;
        List<Socket> clientSockets;
        int BUFFER_SIZE = 1024;
        byte[] buffer;
        #endregion
        /// <summary>
        /// Properties
        /// </summary>
        #region PROPERTIES
        public string IPAddress { get; set; } = "127.0.0.1";
        public int Port { get; set; } = 3000;
        public char SplitString { get; set; } = ',';
        public bool isConnected { get; set; } = false;

        public ReceiveDataEvents OnReceiveDataEvents;
        #endregion

        #region FUNCTIONS
        /// <summary>
        /// Khởi tạo đối tượng
        /// </summary>
        public TCPServer()
        {
            buffer = new byte[BUFFER_SIZE];
            clientSockets = new List<Socket>();
        }
        /// <summary>
        /// Khởi tạo đối tượng
        /// </summary>
        /// <param name="_PortNumber">Địa chỉ Cổng kết nối</param>
        public TCPServer(int _PortNumber)
        {
            Port = _PortNumber;
            buffer = new byte[BUFFER_SIZE];
            clientSockets = new List<Socket>();
        }
        bool IsSocketConnected(Socket s)
        {
            try
            {
                return !((s.Poll(1000, SelectMode.SelectRead) && (s.Available == 0)) || !s.Connected);
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Gửi dữ liệu cho toàn bộ client
        /// </summary>
        /// <param name="data">Dữ liệu gửi</param>
        void sendAll(byte[] data)
        {
            try
            {
                foreach (Socket socket in clientSockets)
                {
                    try
                    {
                        socket.Send(data);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch
            {
            }
        }
        /// <summary>
        /// Hàm nhận dữ liệu từ client
        /// </summary>
        /// <param name="AR"></param>
        private void ReceiveCallback(IAsyncResult AR)
        {
            Socket current = (Socket)AR.AsyncState;
            int received;
            //string ip = current.RemoteEndPoint.ToString();
            if (!IsSocketConnected(current))
            {
                current.Close();
                clientSockets.Remove(current);
                return;
            }
            try
            {
                received = current.EndReceive(AR);
            }
            catch (SocketException)
            {
                current.Close();
                clientSockets.Remove(current);
                return;
            }

            byte[] recBuf = new byte[received];
            Array.Copy(buffer, recBuf, received);
            string text = Encoding.ASCII.GetString(recBuf);

            if (!string.IsNullOrEmpty(text))
            {
                if (OnReceiveDataEvents != null)
                {
                    OnReceiveDataEvents(text);
                }
                //sendAll(recBuf);
            }

            try
            {
                current.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, current);
            }
            catch (Exception)
            {
                current.Close();
                clientSockets.Remove(current);
                return;
            }
        }
        private void AcceptCallback(IAsyncResult AR)
        {
            Socket socket;
            if (serverSocket == null) return;
            try
            {
                socket = serverSocket.EndAccept(AR);
            }
            catch (ObjectDisposedException) // I cannot seem to avoid this (on exit when properly closing sockets)
            {
                return;
            }

            clientSockets.Add(socket);
            socket.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, socket);
            serverSocket.BeginAccept(AcceptCallback, null);
        }
        /// <summary>
        /// Đóng lại toàn bộ kết nối với client
        /// </summary>
        private void CloseAllSockets()
        {
            try
            {
                foreach (Socket socket in clientSockets)
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
                clientSockets.Clear();
                serverSocket.Close();
            }
            catch
            {
            }
        }

        #region PUBLIC FUNCTIONS        
        /// <summary>
        /// Khởi chạy server
        /// </summary>
        ///<returns>
        ///<para>True: Thành công</para>
        ///<para>False: Thất bại</para>
        ///</returns>
        public bool Start()
        {
            try
            {
                if (serverSocket == null) serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                serverSocket.Bind(new IPEndPoint(System.Net.IPAddress.Parse(IPAddress), Port));
                serverSocket.Listen(1000);
                serverSocket.BeginAccept(AcceptCallback, null);
                serverSocket.SendTimeout = 1000;
                serverSocket.ReceiveTimeout = 1000;
                isConnected = true;
                return true;
            }
            catch (Exception ex)
            {
                isConnected = false;
                return false;
            }
        }
        /// <summary>
        /// Đóng server
        /// </summary>
        public void Stop()
        {
            CloseAllSockets();
            serverSocket = null;
            isConnected = false;
        }
        /// <summary>
        /// Gửi dữ liệu cho Client
        /// </summary>
        /// <param name="_SendData">Dữ liệu gửi đi</param>
        /// <returns></returns>
        public bool SendData(string _SendData)
        {
            bool _Result = false;

            if (!isConnected) return _Result;

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(_SendData);
            sendAll(data);
            //serverSocket.Send(data);
            return true;
        }
        public void SaveIniConfig()
        {
            //string _FileName = AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + "RTCCommunicationConfig.ini";
            //cIniFile oINIFile = new cIniFile(_FileName);
            //try
            //{
            //    {
            //        oINIFile.WriteInteger("Server", "PortNumber", RTCPortNumber);
            //    }
            //}
            //finally
            //{
            //    oINIFile = null;
            //}
        }
        public void ReadIniConfig()
        {
            //RTCPortNumber = 3000;
            //string _FileName = AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + "RTCCommunicationConfig.ini";
            //cIniFile oINIFile = new cIniFile(_FileName);
            //try
            //{
            //    RTCPortNumber = oINIFile.GetInteger("Server", "PortNumber", RTCPortNumber);
            //}
            //finally
            //{
            //    oINIFile = null;
            //}
        }
        #endregion

        #endregion
    }
}
