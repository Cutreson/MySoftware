////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	1. Classes\Connect\cTCPIPClient.cs
//
// summary:	Implements the TCP/IP client class
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using static MySoftware.TCPServer;
//using RTCVision2101.PublicFunctions;

namespace MySoftware
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A TCP/IP client. </summary>
    ///
    /// <remarks>   DATRUONG, 19/11/2021. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class TCPIPClient
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the name of the host. </summary>
        ///
        /// <value> The name of the host. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string HostName { get; set; }
        public char SplitString { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the port. </summary>
        ///
        /// <value> The port. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public int Port { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets a value indicating whether this  is hexadecimal. </summary>
        ///
        /// <value> True if this  is hexadecimal, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool IsHex { get; set; }

        /// <summary>   The client. </summary>
        private Socket Client;

        /// <summary>   The buffer. </summary>
        private byte[] buffer;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets a value indicating whether this  is connected. </summary>
        ///
        /// <value> True if this  is connected, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool IsConnected
        {
            get
            {
                try
                {
                    if (Client == null)
                        return false;
                    bool part1 = Client.Poll(1000, SelectMode.SelectRead);
                    bool part2 = (Client.Available == 0);
                    if (part1 && part2)
                        return false;
                    else
                        return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets a message describing the error. </summary>
        ///
        /// <value> A message describing the error. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string ErrMessage { get; set; }

        /// <summary>   The on receive data events. </summary>
        public ReceiveDataEvents OnReceiveDataEvents;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   DATRUONG, 19/11/2021. </remarks>
        ///
        /// <param name="_HostName">    Name of the host. </param>
        /// <param name="_Port">        The port. </param>
        /// <param name="_IsHex">       True if is hexadecimal, false if not. </param>
        /// <param name="_IsWithStart"> (Optional) True if is with start, false if not. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public TCPIPClient(string _HostName, int _Port, bool _IsHex, bool _IsWithStart = true)
        {
            ErrMessage = string.Empty;
            HostName = _HostName;
            Port = _Port;
            IsHex = _IsHex;
            if (_IsWithStart)
                Connect();
            SplitString = ',';
        }
        public TCPIPClient()
        {
            HostName = "127.0.0.1";
            Port = 3000;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Starts this.  </summary>
        ///
        /// <remarks>   DATRUONG, 19/11/2021. </remarks>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool Connect()
        {
            try
            {
                ErrMessage = string.Empty;
                Client = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);
                var endPoint = new IPEndPoint(IPAddress.Parse(HostName), Port);
                Client.BeginConnect(endPoint,
                new AsyncCallback(ConnectCallback), Client);

                return true;
            }
            catch (Exception ex)
            {
                ErrMessage = ex.Message;
                //GlobFuncs.SaveErr(ex);
                return false;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Stops this.  </summary>
        ///
        /// <remarks>   DATRUONG, 19/11/2021. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void Disconnect()
        {
            if (Client != null)
                Client.Close();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Async callback, called on completion of connect callback. </summary>
        ///
        /// <remarks>   DATRUONG, 19/11/2021. </remarks>
        ///
        /// <param name="AR">   The result of the asynchronous operation. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void ConnectCallback(IAsyncResult AR)
        {
            try
            {
                Client = (Socket)AR.AsyncState;
                Client.EndConnect(AR);
                buffer = new byte[Client.ReceiveBufferSize];
                Client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), Client);
            }
            catch (SocketException ex)
            {
                //GlobFuncs.SaveErr(ex);
            }
            catch (ObjectDisposedException ex)
            {
                //GlobFuncs.SaveErr(ex);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Async callback, called on completion of receive callback. </summary>
        ///
        /// <remarks>   DATRUONG, 19/11/2021. </remarks>
        ///
        /// <param name="AR">   The result of the asynchronous operation. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void ReceiveCallback(IAsyncResult AR)
        {
            try
            {
                Client = (Socket)AR.AsyncState;

                int received = Client.EndReceive(AR);

                if (received == 0)
                    return;

                string text = string.Empty;
                byte[] recBuf = new byte[received];
                Array.Copy(buffer, recBuf, received);

                if (IsHex)
                    text = BitConverter.ToString(recBuf).Replace("-", "");
                else
                    text = Encoding.ASCII.GetString(recBuf);

                if (!string.IsNullOrEmpty(text))
                {
                    if (OnReceiveDataEvents != null)
                    {
                        OnReceiveDataEvents(text);
                    }
                }

                Client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), Client);
            }
            catch (SocketException ex)
            {
                //GlobFuncs.SaveErr(ex);
            }
            catch (ObjectDisposedException ex)
            {
                //GlobFuncs.SaveErr(ex);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Sends a data. </summary>
        ///
        /// <remarks>   DATRUONG, 19/11/2021. </remarks>
        ///
        /// <param name="value">    The value. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool SendData(string value)
        {
            try
            {
                if (Client == null || !Client.Connected)
                    return false;

                if (IsHex)
                {
                    byte[] data = Encoding.Default.GetBytes(value);
                    string hexString = BitConverter.ToString(data);
                    hexString = hexString.Replace("-", "");
                    ASCIIEncoding encoding = new ASCIIEncoding();
                    data = encoding.GetBytes(hexString);
                    Client.Send(data);
                }
                else
                {
                    ASCIIEncoding encoding = new ASCIIEncoding();
                    byte[] data = encoding.GetBytes(value);
                    Client.Send(data);
                }

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

    }
}
