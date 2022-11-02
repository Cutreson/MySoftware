using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using RTCVision2101.Template;
//using RTCVision2101.Variables;
//using RTCVision2101.Classes;
//using RTCVision2101.Enums;

using System.Windows.Forms;
using System.Reflection;
//using RTCVision2101.Consts;
using System.IO;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using System.IO.Ports;
using System.Net.Sockets;
using System.Net;
using OpenCvSharp;
//using RJCP.IO.Ports;
//using RTCVision2101.Forms;

namespace MySoftware
{
    public static partial class GlobFuncs
    {
        #region "SYSTEM"
        /// <summary>
        /// Lưu lỗi
        /// </summary>
        /// <param name="AEx">Đối tượng lỗi</param>
        /// <param name="AMoreInfo">Thông tin gắn thêm</param>
        //public static void SaveErr(Exception AEx, string AMoreInfo = "")
        //{
        //    GlobVar.ErrHandle.SaveErrors(AEx, GlobVar.RTCVision.OSInfo.FullInfo, AMoreInfo);
        //}

        //public static void SaveErr(string ErrMsg, string AMoreInfo = "")
        //{
        //    Exception AEx = new Exception(ErrMsg);
        //    GlobVar.ErrHandle.SaveErrors(AEx, GlobVar.RTCVision.OSInfo.FullInfo, AMoreInfo);
        //}
        /// <summary>
        /// Đọc thông số chương trình trước khi chạy
        /// </summary>
        /// <summary>
        /// Thiết lập các môi trường làm việc của chương trình
        /// </summary>
        public static void SetupEnvironment()
        {

            //GlobVar.MyEngine = new HDevEngine();
            //GlobVar.MyEngine.SetProcedurePath(GlobVar.SystemFiles.Paths.Procedures);
            //GlobVar.MyEngine.SetEngineAttribute("debug_password", "1");
            //GlobVar.MyEngine.SetEngineAttribute("debug_port", new HTuple(8688));
            //GlobVar.MyEngine.SetEngineAttribute("debug_wait_for_connection", "true");
            //HOperatorSet.SetSystem("clip_region", "true");

            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            customCulture.NumberFormat.NumberGroupSeparator = ",";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            /* KHỞI TẠO MÔI TRƯỜNG SERVER NẾU CÓ */
            if (GlobVar.SystemFiles.Options.IsServer)
            {
                //GlobVar.Server = new cServer();
                //GlobVar.Server.Start();
            }

            /* KHỞI TẠO DỮ LIỆU ASCII */
            GlobFuncs.GenerateDicASCII();
        }
        #endregion
        #region "HSMARTWINDOW"
        /// <summary>
        /// Thiết lập kích thước cửa sổ HsmartWindow
        /// </summary>
        /// <param name="Image">Ảnh cần view</param>
        /// <param name="HSWindow">Đối tượng HsmartWindow</param>
       
        #endregion
        #region "TREELIST"
        #endregion
        #region "OTHERS"
        public static string FixedDirSepChar(string path)
        {
            if (path != "" && !path.EndsWith(Path.DirectorySeparatorChar.ToString()))
                return path + Path.DirectorySeparatorChar;
            return path;
        }
        public static string CreateSaveFolderWithDay(string _Path)
        {
            if (_Path == "") return _Path;
            return FixedDirSepChar(FixedDirSepChar(_Path) + DateTime.Now.Date.ToString("ddMMyyyy"));
        }
        public static string FirstCharToUpper(this string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input.First().ToString().ToUpper() + input.Substring(1);
            }
        }
        public static void SaveAllControlEnableStatus(Control _Control)
        {
            if (_Control != null)
            {
                _Control.Tag = _Control.Enabled ? 1 : 0;
                if (_Control.Controls != null && _Control.Controls.Count > 0)
                    foreach (Control item in _Control.Controls)
                        SaveAllControlEnableStatus(item);
            }
        }
        public static void DisableAllControls(Control _Control, bool _SaveOldEnable = true)
        {
            if (_Control != null)
            {
                _Control.Enabled = false;
                if (_Control.Controls != null && _Control.Controls.Count > 0)
                    foreach (Control item in _Control.Controls)
                        DisableAllControls(item, _SaveOldEnable);
            }
        }

        public static void EnableAllControls(Control _Control, bool _SetOldEnable = true)
        {
            if (_Control != null)
            {
                if (_SetOldEnable)
                    _Control.Enabled = (_Control.Tag != null && int.Parse(_Control.Tag.ToString()) == 1);
                else
                    _Control.Enabled = true;
                if (_Control.Controls != null && _Control.Controls.Count > 0)
                    foreach (Control item in _Control.Controls)
                        EnableAllControls(item, _SetOldEnable);
            }
        }
        /// <summary>
        /// Gán giá trị cho 1 ô text bằng 1 giá trị HTuple
        /// </summary>
        /// <param name="_TextEdit">Ô text cần gán giá trị</param>
        /// <param name="_Value">Dữ liệu gán dạng HTuple</param>

        public static List<string> String2ListString(string _Value, char _Sep)
        {
            string[] s = _Value.Split(_Sep);
            List<string> obj = new List<string>();
            obj.AddRange(s);
            return obj;
        }
        private const int WM_SETREDRAW = 11;

        /// Suspends painting for the target control. Do NOT forget to call EndControlUpdate!!!
        /// </summary>
        /// <param name="control">visual control</param>
        public static void BeginControlUpdate(Control control)
        {
            Message msgSuspendUpdate = Message.Create(control.Handle, WM_SETREDRAW, IntPtr.Zero,
                  IntPtr.Zero);

            NativeWindow window = NativeWindow.FromHandle(control.Handle);
            window.DefWndProc(ref msgSuspendUpdate);
        }

        /// <summary>
        /// Resumes painting for the target control. Intended to be called following a call to BeginControlUpdate()
        /// </summary>
        /// <param name="control">visual control</param>
        public static void EndControlUpdate(Control control)
        {
            // Create a C "true" boolean as an IntPtr
            IntPtr wparam = new IntPtr(1);
            Message msgResumeUpdate = Message.Create(control.Handle, WM_SETREDRAW, wparam,
                  IntPtr.Zero);

            NativeWindow window = NativeWindow.FromHandle(control.Handle);
            window.DefWndProc(ref msgResumeUpdate);
            control.Invalidate();
            control.Refresh();
        }
        public static void EnableTransparency(Control c)
        {
            MethodInfo method = c.GetType().GetMethod("SetStyle", BindingFlags.NonPublic | BindingFlags.Instance);
            method.Invoke(c, new object[] { ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true });
            c.BackColor = Color.Transparent;
            c.ForeColor = Color.Black;
        }

        public static IEnumerable<Control> GetAll(Control control, Type type, bool withRTCPrefix = true)
        {
            var controls = control.Controls.Cast<Control>();
            if (withRTCPrefix)
            {
                return controls.SelectMany(ctrl => GetAll(ctrl, type, withRTCPrefix))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type && c.Name.StartsWith("RTC"));
            }
            else
                return controls.SelectMany(ctrl => GetAll(ctrl, type, withRTCPrefix))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type && !c.Name.StartsWith("RTC"));
        }
        public static IEnumerable<Control> GetAllControls(Control control)
        {
            var controls = control.Controls.Cast<Control>();
            return controls.SelectMany(ctrl => GetAllControls(ctrl)
                                      .Concat(controls));
        }
        public static IEnumerable<Control> GetAllControls(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();
            return controls.SelectMany(ctrl => GetAllControls(ctrl, type)
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type));
        }


       
        /// <summary>
        /// HTupleElement To Double
        /// </summary>
        /// <param name="hTupleElements"></param>
        /// <returns></returns>




        public static bool IsNumeric(string _sNeedCheck)
        {
            return double.TryParse(_sNeedCheck, out double _dNeedCheck);
        }
        public static bool IsInt(string _sNeedCheck)
        {
            return int.TryParse(_sNeedCheck, out int _dNeedCheck);
        }
        public static bool IsIntList(string[] _sValues)
        {
            foreach (string item in _sValues)
            {
                if (!int.TryParse(item, out int result))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool IsDoubleList(string[] _sValues)
        {
            foreach (string item in _sValues)
            {
                if (!double.TryParse(item, out double result))
                {
                    return false;
                }
            }
            return true;
        }

        public static Dictionary<TKey, TValue> CloneDictionaryROIs<TKey, TValue>
                      (Dictionary<TKey, TValue> original) where TValue : ICloneable
        {
            Dictionary<TKey, TValue> ret = new Dictionary<TKey, TValue>(original.Count,
                                                                    original.Comparer);
            foreach (KeyValuePair<TKey, TValue> entry in original)
            {
                ret.Add(entry.Key, (TValue)entry.Value.Clone());
            }
            return ret;
        }

        public static List<string> GetDirectories(string path, string searchPattern = "*",
        SearchOption searchOption = SearchOption.AllDirectories)
        {
            if (searchOption == SearchOption.TopDirectoryOnly)
                return Directory.GetDirectories(path, searchPattern).ToList();

            var directories = new List<string>(GetDirectories(path, searchPattern));

            for (var i = 0; i < directories.Count; i++)
                directories.AddRange(GetDirectories(directories[i], searchPattern));

            return directories;
        }

        private static List<string> GetDirectories(string path, string searchPattern)
        {
            try
            {
                return Directory.GetDirectories(path, searchPattern).ToList();
            }
            catch (UnauthorizedAccessException)
            {
                return new List<string>();
            }
        }
        /// <summary>
        /// Chuyển từ có dấu sang không dấu
        /// </summary>
        /// <param name="_Text">Chuỗi có dấu cần chuyển</param>
        /// <returns></returns>
        public static string SwitchToUnsigned(string _Text)
        {
            string[] CoDau = new[] { "aàáảãạăằắẳẵặâầấẩẫậ", "AÀÁẢÃẠĂẰẮẲẴẶÂẦẤẨẪẬ", "đ", "Đ", "eèéẻẽẹêềếểễệ", "EÈÉẺẼẸÊỀẾỂỄỆ", "iìíỉĩị", "IÌÍỈĨỊ", "oòóỏõọôồốổỗộơờớởỡợ", "ÒÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢ", "uùúủũụưừứửữự", "UÙÚỦŨỤƯỪỨỬỮỰ", "yỳýỷỹỵ", "YỲÝỶỸỴ" };
            string[] KoDau = new[] { "a", "A", "d", "D", "e", "E", "i", "I", "o", "O", "u", "U", "y", "Y" };
            string str = _Text;
            string strReturn = "";
            for (int i = 0; i <= str.Length - 1; i++)
            {
                string iStr = str.Substring(i, 1);
                string rStr = iStr;
                for (int j = 0; j <= CoDau.Length - 1; j++)
                {
                    if (CoDau[j].IndexOf(iStr) >= 0)
                    {
                        rStr = KoDau[j];
                        break;
                    }
                }
                strReturn += rStr;
            }
            return strReturn;
        }
        public static bool FileIsUsed(string filename)
        {
            bool Locked = false;
            try
            {
                FileStream fs =
                    File.Open(filename, FileMode.OpenOrCreate,
                    FileAccess.ReadWrite, FileShare.None);
                fs.Close();
            }
            catch (IOException)
            {
                Locked = true;
            }
            return Locked;
        }
        public static T DeepClone<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;
                return (T)formatter.Deserialize(ms);
            }
        }

        #endregion

        #region "ACTIONS FUNCTIONS"
        public static object GetPropDefaultValueByBaseType(Type PropType)
        {
            object obj = null;

            switch (PropType.Name)
            {
                case "SString":
                    obj = string.Empty;
                    break;
                case "SInt":
                    obj = 0;
                    break;
                case "SDouble":
                    obj = 0;
                    break;
                case "SBool":
                    obj = false;
                    break;
                case "Guild":
                    obj = Guid.Empty;
                    break;
                default:
                    break;
            }

            return obj;
        }

        public static T Clone<T>(T source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(serialized);
        }
        #endregion

        #region "CAMERA"
        public static string GetVendorName(string _InterfaceName)
        {
            string Result = string.Empty;

            if (_InterfaceName == "") return Result;
            string[] sp = new string[] { " | " };
            string[] s = _InterfaceName.Split(sp, StringSplitOptions.RemoveEmptyEntries);

            foreach (string item in s)
            {
                if (item.StartsWith("vendor:"))
                {
                    Result = item.Substring(7);
                    break;
                }
            }
            return Result;
        }
        public static string GenCameraName(string _InterfaceName)
        {
            string Result = string.Empty;

            if (_InterfaceName == "") return Result;
            string[] sp = new string[] { " | " };
            string[] s = _InterfaceName.Split(sp, StringSplitOptions.RemoveEmptyEntries);
            string sVendor = string.Empty;
            string sDevice = string.Empty;
            foreach (string item in s)
            {
                if (item.StartsWith("vendor:"))
                {
                    sVendor = item.Substring(7);
                }
                else if (item.StartsWith("device:"))
                {
                    sDevice = item.Substring(7);
                }
            }
            Result = sVendor != "" ? sVendor + " - " + (sDevice != "" ? sDevice : "") : "";
            if (Result == "")
            {
                Result = _InterfaceName;
            }
            return Result;
        }
        #endregion

        #region "DATAROW"
        public static Guid GetDataRowValue_Guid(DataRow _Row, string _ColName)
        {
            Guid Result = Guid.Empty;
            if (_Row == null) return Result;
            if (!Guid.TryParse(_Row[_ColName].ToString(), out Result)) Result = Guid.Empty;
            return Result;
        }
        public static string GetDataRowValue_String(DataRow _Row, string _ColName)
        {
            string Result = string.Empty;
            if (_Row == null) return Result;
            Result = _Row[_ColName].ToString();
            return Result;
        }
        public static bool GetDataRowValue_Boolean(DataRow _Row, string _ColName)
        {
            bool Result = false;
            if (_Row == null) return Result;
            string outData = _Row[_ColName].ToString();
            if (!bool.TryParse(_Row[_ColName].ToString(), out Result)) Result = false;
            return Result;
        }
        public static int GetDataRowValue_Int(DataRow _Row, string _ColName)
        {
            int Result = 0;
            if (_Row == null) return Result;
            if (!int.TryParse(_Row[_ColName].ToString(), out Result)) Result = 0;
            return Result;
        }
        public static long GetDataRowValue_Long(DataRow _Row, string _ColName)
        {
            long Result = 0;
            if (_Row == null) return Result;
            if (!long.TryParse(_Row[_ColName].ToString(), out Result)) Result = 0;
            return Result;
        }
        public static double GetDataRowValue_Double(DataRow _Row, string _ColName)
        {
            double Result = 0;
            if (_Row == null) return Result;
            if (!double.TryParse(_Row[_ColName].ToString(), out Result)) Result = 0;
            return Result;
        }
        #endregion

        #region CONNECTION
        public static bool CheckConnectTCPIP(string _IPAddress, int _Port, out string _ErrMessage)
        {
            Socket SocketConn = null;
            try
            {
                _ErrMessage = string.Empty;

                IPAddress ipaddress = IPAddress.Parse(_IPAddress);
                IPEndPoint ipe = new IPEndPoint(ipaddress, _Port);

                SocketConn = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                SocketConn.Connect(ipe);
                return SocketConn.Connected;
            }
            catch (Exception ex)
            {
                _ErrMessage = ex.Message;
                return false;
            }
            finally
            {
                if (SocketConn != null && SocketConn.Connected)
                    SocketConn.Close();
                SocketConn = null;
            }
        }
        public static bool CheckStartServer(int _Port, out string _ErrMessage)
        {
            _ErrMessage = string.Empty;
            Socket SocketConn = null;
            try
            {
                if (SocketConn == null) SocketConn = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                SocketConn.Bind(new IPEndPoint(IPAddress.Any, _Port));
                SocketConn.Listen(0);

                SocketConn.Close();
                SocketConn = null;

                return true;
            }
            catch (Exception ex)
            {
                _ErrMessage = ex.Message;
                return false;
            }
            finally
            {
                if (SocketConn != null && SocketConn.Connected)
                    SocketConn.Close();
                SocketConn = null;
            }
        }
        private static void CloseCOM(SerialPort _COMConn, bool _currentCOMPortRemoved = false)
        {
            //if (_COMConn == null) return;
            ////if (_currentCOMPortRemoved)
            ////{
            ////    _COMConn.DtrEnable = false;
            ////    _COMConn.RtsEnable = false;
            ////    _COMConn.DiscardInBuffer();
            ////    _COMConn.DiscardOutBuffer();
            ////}
            ////else
            ////{
            ////    _COMConn.Close();
            ////}

            //if (_COMConn.IsOpen)
            //{
            //    while (_COMConn.BytesToWrite > 0) { }
            //    _COMConn.ErrorReceived -= SerialErrorReceivedEventHandler;
            //    _COMConn.DataReceived -= SerialDataReceivedEventHandler;
            //    _COMConn.DiscardInBuffer();
            //    _COMConn.Close();
            //    _COMConn = null;
            //}
        }

        public static void TestInsert()
        {
            DataTable BangDL = new DataTable();
            BangDL.Columns.Add("C1", typeof(string));
            BangDL.Columns.Add("C2", typeof(int));
            BangDL.Columns.Add("C3", typeof(double));
            BangDL.Columns.Add("C4", typeof(bool));


            BangDL.Rows.Add(new object[] { "Test", 1, 2.13, true });
            BangDL.Rows.Add(new object[] { "Test", 1, 2.13, true });
            BangDL.Rows.Add(new object[] { "Test", 1, 2.13, true });
            BangDL.Rows.Add(new object[] { "Test", 1, 2.13, true });
            BangDL.Rows.Add(new object[] { "Test", 1, 2.13, true });

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("BEGIN \n");
            string sSQL = "INSERT INTO TenBang VALUES('{0}',{1},{2},'{3}')";

            foreach (DataRow r in BangDL.Rows)
            {
                stringBuilder.Append(string.Format(sSQL,
                    new object[] {GetDataRowValue_String(r,"C1"),
                                  GetDataRowValue_Int(r,"C2"),
                                   GetDataRowValue_Double(r,"C3"),
                                    GetDataRowValue_Boolean(r,"C4")}) + ";\n");

            }
            stringBuilder.Append("END");

            Clipboard.SetText(stringBuilder.ToString());
        }
        public static string GetIPAddress()
        {
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }

                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion

        #region WAITFORM
        /// <summary>
        /// Hàm show wait form
        /// </summary>
        /// <param name="_Caption">Caption cần đổi</param>
        /// <param name="_Description">Nội dung cần đổi</param>

        #endregion

        #region FORM ASCII TABLE
        /// <summary>
        /// Hiển thị bảng ASCII để người dùng lựa chọn
        /// </summary>
        /// <param name="_TextEditSetValue">Textbox nhận giá trị</param>
        public static byte[] ASCII = { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f,
            0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1a, 0x1b, 0x1c, 0x1d, 0x1e, 0x1f,
            0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27, 0x28, 0x29, 0x2a, 0x2b, 0x2c, 0x2d, 0x2e, 0x2f,
            0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x3a, 0x3b, 0x3c, 0x3d, 0x3e, 0x3f,
            0x40, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x48, 0x49, 0x4a, 0x4b, 0x4c, 0x4d, 0x4e, 0x4f,
            0x50, 0x51, 0x52, 0x53, 0x54, 0x55, 0x56, 0x57, 0x58, 0x59, 0x5a, 0x5b, 0x5c, 0x5d, 0x5e, 0x5f,
            0x60, 0x61, 0x62, 0x63, 0x64, 0x65, 0x66, 0x67, 0x68, 0x69, 0x6a, 0x6b, 0x6c, 0x6d, 0x6e, 0x6f,
            0x70, 0x71, 0x72, 0x73, 0x74, 0x75, 0x76, 0x77, 0x78, 0x79, 0x7a, 0x7b, 0x7c, 0x7d, 0x7e, 0x7f,
            0x80, 0x81, 0x82, 0x83, 0x84, 0x85, 0x86, 0x87, 0x88, 0x89, 0x8a, 0x8b, 0x8c, 0x8d, 0x8e, 0x8f,
            0x90, 0x91, 0x92, 0x93, 0x94, 0x95, 0x96, 0x97, 0x98, 0x99, 0x9a, 0x9b, 0x9c, 0x9d, 0x9e, 0x9f,
            0xa0, 0xa1, 0xa2, 0xa3, 0xa4, 0xa5, 0xa6, 0xa7, 0xa8, 0xa9, 0xaa, 0xab, 0xac, 0xad, 0xae, 0xaf,
            0xb0, 0xb1, 0xb2, 0xb3, 0xb4, 0xb5, 0xb6, 0xb7, 0xb8, 0xb9, 0xba, 0xbb, 0xbc, 0xbd, 0xbe, 0xbf,
            0xc0, 0xc1, 0xc2, 0xc3, 0xc4, 0xc5, 0xc6, 0xc7, 0xc8, 0xc9, 0xca, 0xcb, 0xcc, 0xcd, 0xce, 0xcf,
            0xd0, 0xd1, 0xd2, 0xd3, 0xd4, 0xd5, 0xd6, 0xd7, 0xd8, 0xd9, 0xda, 0xdb, 0xdc, 0xdd, 0xde, 0xdf,
            0xe0, 0xe1, 0xe2, 0xe3, 0xe4, 0xe5, 0xe6, 0xe7, 0xe8, 0xe9, 0xea, 0xeb, 0xec, 0xed, 0xee, 0xef,
            0xf0, 0xf1, 0xf2, 0xf3, 0xf4, 0xf5, 0xf6, 0xf7, 0xf8, 0xf9, 0xfa, 0xfb, 0xfc, 0xfd, 0xfe, 0xff };
        /// <summary>
        /// Chuyển đổi từ byte sang HEX
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        static string ToHex(byte[] bytes)
        {
            char[] c = new char[bytes.Length * 2];

            byte b;

            for (int bx = 0, cx = 0; bx < bytes.Length; ++bx, ++cx)
            {
                b = ((byte)(bytes[bx] >> 4));
                c[cx] = (char)(b > 9 ? b - 10 + 'A' : b + '0');

                b = ((byte)(bytes[bx] & 0x0F));
                c[++cx] = (char)(b > 9 ? b - 10 + 'A' : b + '0');
            }

            string result = new string(c);
            return "0x" + result;
        }
        /// <summary>
        /// Khởi tạo thư viện dữ liệu ASCII
        /// </summary>
        public static void GenerateDicASCII()
        {
            if (GlobVar.DicASCII != null) return;
            GlobVar.DicASCII = new Dictionary<string, byte>();
            for (int i = 0; i < ASCII.Length; i++)
            {
                GlobVar.DicASCII.Add(ToHex(new byte[] { ASCII[i] }), ASCII[i]);
            }
        }
        #endregion

        public static bool SaveLogFile(string data)
        {
            try
            {
                if (!Directory.Exists(GlobVar.SystemFiles.Paths.Log))
                {
                    Directory.CreateDirectory(GlobVar.SystemFiles.Paths.Log);
                }

                File.WriteAllText(GlobVar.SystemFiles.Paths.Log, data);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static void ReadAppInfo()
        {
            GlobVar.SystemFiles = new cSystemTypes();
            GlobVar.SystemFiles.ReadDefault();
            //GlobVar.SystemFiles.ReadIniConfig();
            //GlobVar.DeepCopyFileName = Path.GetTempFileName();
            //try
            //{
            //    DeepCopyData.Disconnect();
            //    File.Copy(GlobVar.RTCVision.Files.SaveTemplate, GlobVar.DeepCopyFileName, true);
            //    DeepCopyData.DataFileName = GlobVar.DeepCopyFileName;
            //}
            //catch
            //{
            //    if (File.Exists(GlobVar.RTCVision.Files.SaveTemplate))
            //        GlobVar.DeepCopyFileName = GlobVar.RTCVision.Files.SaveTemplate;
            //    else
            //        GlobVar.DeepCopyFileName = string.Empty;
            //}
        }
        public static double String2Double(string sData)
        {
            double result = 0;
            result = Convert.ToDouble(sData);
            return result;
        }
        public static double String2Int32(string sData)
        {
            double result = 0;
            result = Convert.ToInt32(sData);
            return result;
        }
        public static long[] Str2LongArr(string _Value, char _SEP = cStrings.SepPhay)
        {
            long[] Result = new long[] { };
            string[] _Values = _Value.Split(_SEP);
            Result = new long[_Values.Length];
            for (int i = 0; i < _Values.Length; i++)
            {
                Result[i] = long.Parse(_Values[i]);
            }
            return Result;
        }
        public static string[] Str2StringArr(string _Value, char _SEP = cStrings.SepPhay)
        {
            return _Value.Split(_SEP);
        }
        public static double[] Str2DoubleArr(string _Value, char _SEP = cStrings.SepPhay)
        {
            double[] Result = new double[] { };
            string[] _Values = _Value.Split(_SEP);
            Result = new double[_Values.Length];
            for (int i = 0; i < _Values.Length; i++)
            {
                Result[i] = double.Parse(_Values[i]);
            }
            return Result;
        }
        public static object[] Str2MixArr(string _Value, char _SEP = cStrings.SepPhay)
        {
            object[] Result = new object[] { };
            string[] _Values = _Value.Split(_SEP);
            Result = new object[_Values.Length];
            for (int i = 0; i < _Values.Length; i++)
            {
                Result[i] = (object)_Values[i];
            }
            return Result;
        }
        public static int[] Str2IntArr(string _Value, char _SEP = cStrings.SepPhay)
        {
            int[] Result = new int[] { };
            string[] _Values = _Value.Split(_SEP);
            Result = new int[_Values.Length];
            for (int i = 0; i < _Values.Length; i++)
            {
                Result[i] = int.Parse(_Values[i]);
            }
            return Result;
        }
        public static List<int> Str2LstInt(string _Value, char _SEP = cStrings.SepPhay)
        {
            return Str2IntArr(_Value).ToList();
        }
        public static List<double> Str2LstDouble(string _Value, char _SEP = cStrings.SepPhay)
        {
            return Str2DoubleArr(_Value).ToList();
        }
        public static string ListDouble2StrWithType(List<double> lDouble)
        {
            if (lDouble == null || lDouble.Count <= 0)
                return string.Empty;

            string Result = string.Empty;
            for (int i = 0; i < lDouble.Count; i++)
            {
                if (Result == string.Empty)
                    Result = lDouble[i].ToString();
                else
                    Result = Result + "," + lDouble[i];
            }

            if (Result != "") Result = Result + cStrings.SepGDung + lDouble[0].GetType().ToString();

            return Result;
        }
        public static string ListInt2StrWithType(List<int> lInt)
        {
            if (lInt == null || lInt.Count <= 0)
                return string.Empty;

            string Result = string.Empty;
            for (int i = 0; i < lInt.Count; i++)
            {
                if (Result == string.Empty)
                    Result = lInt[i].ToString();
                else
                    Result = Result + "," + lInt[i];
            }

            if (Result != "") Result = Result + cStrings.SepGDung + lInt[0].GetType().ToString();

            return Result;
        }
        public static int RoundDatax1000(double _Value, char _SEP = cStrings.SepPhay)
        {
            int Result = 0;
            _Value = Math.Round(_Value, GlobVar.ConstantRound) * Math.Pow(10, GlobVar.ConstantRound);
            Result = (int)_Value;
            return Result;
        }
        public static void UpdateIPClient()
        {
            if (GlobVar.TCPClient == null)
            {
                GlobVar.TCPClient = new TCPIPClient();
            }
            //GlobVar.TCPClient.HostName = GlobVar.ProjectCurr.TCPIPSettings.HostName;
            //GlobVar.TCPClient.Port = GlobVar.ProjectCurr.TCPIPSettings.Port;
        }
        static public void FillPictureBox(PictureBox pbox, Bitmap bmp)
        {
            pbox.SizeMode = PictureBoxSizeMode.Normal;
            bool source_is_wider = (float)bmp.Width / bmp.Height > (float)pbox.Width / pbox.Height;

            var resized = new Bitmap(pbox.Width, pbox.Height);
            var g = Graphics.FromImage(resized);
            var dest_rect = new Rectangle(0, 0, pbox.Width, pbox.Height);
            Rectangle src_rect;
            if (source_is_wider)
            {
                float size_ratio = (float)pbox.Height / bmp.Height;
                int sample_width = (int)(pbox.Width / size_ratio);
                src_rect = new Rectangle((bmp.Width - sample_width) / 2, 0, sample_width, bmp.Height);
            }
            else
            {
                float size_ratio = (float)pbox.Width / bmp.Width;
                int sample_height = (int)(pbox.Height / size_ratio);
                src_rect = new Rectangle(0, (bmp.Height - sample_height) / 2, bmp.Width, sample_height);
            }

            g.DrawImage(bmp, dest_rect, src_rect, GraphicsUnit.Pixel);
            g.Dispose();

            pbox.Image = resized;
        }
        static public void TCPServer_Open()
        {
            if (GlobVar.TCPServer == null)
            {
                //GlobVar.TCPServer = new TCPServer(GlobVar.ProjectCurr.TCPIPSettings.Port);
                GlobVar.TCPServer.Start();
            }
        }
        static public void TCPServer_Close()
        {
            if (GlobVar.TCPServer == null)
            {
                GlobVar.TCPServer.Stop();
            }
        }
        public static double RoundData_Double(double _Value, int _ConstantRound = 3)
        {
            _Value = Math.Round(_Value, _ConstantRound);
            return _Value;
        }
        public static Rectangle GetRectangle_PbtoMat(PictureBox pbWindow, Rectangle roi, int widthImg = 1, int heightImg = 1)
        {
            int ratioX = 1, ratioY = 1;
            //int widthImg = 1, heightImg = 1;
            if (pbWindow.Image != null)
            {
                //widthImg = pbWindow.Image.Width;
                //heightImg = pbWindow.Image.Height;
                ratioY = pbWindow.Image.Width / pbWindow.Width;
                ratioX = pbWindow.Image.Height / pbWindow.Height;
            }

            bool bSelect = false;
            bool bBackground = true;
            Bitmap temp = null;
            double scale = 1;
            int offsetX = 0;
            int offsetY = 0;

            if (bBackground)
            {
                scale = (double)heightImg / pbWindow.ClientRectangle.Height;
                double s2 = (double)widthImg / pbWindow.ClientRectangle.Width;
                if (s2 > scale)
                {
                    scale = s2;
                    int picH = (int)(heightImg / scale);
                    offsetY = (pbWindow.Height - picH) / 2;
                }
                else
                {
                    int picW = (int)(widthImg / scale);
                    offsetX = (pbWindow.Width - picW) / 2;
                }

            }
            Rectangle roiSelect = new Rectangle((int)((roi.X - offsetX) * scale), (int)((roi.Y - offsetY) * scale), (int)(roi.Width * scale), (int)(roi.Height * scale));

            return roiSelect;
        }
        public static Rectangle GetRectangle_MattoPb(PictureBox pbWindow, Rectangle rect, int widthImg = 1, int heightImg = 1)
        {
            int ratioX = 1, ratioY = 1;
            //int widthImg = 1, heightImg = 1;
            if (pbWindow.Image != null)
            {
                //widthImg = pbWindow.Image.Width;
                //heightImg = pbWindow.Image.Height;
                ratioY = pbWindow.Image.Width / pbWindow.ClientSize.Width;
                ratioX = pbWindow.Image.Height / pbWindow.ClientSize.Height;
            }
            //return new Rectangle(rect.X* ratioX, rect.Y *ratioY, rect.Width * ratioY, rect.Height * ratioY);

            bool bSelect = false;
            bool bBackground = true;
            Bitmap temp = null;
            double scale = 1;
            int offsetX = 0;
            int offsetY = 0;

            if (bBackground)
            {
                scale = (double)heightImg / pbWindow.ClientRectangle.Height;
                double s2 = (double)widthImg / pbWindow.ClientRectangle.Width;

                if (s2 > scale)
                {
                    scale = s2;
                    int picH = (int)(heightImg);
                    offsetY = (int)(pbWindow.ClientSize.Height * (scale) - picH) / 2;
                }
                else
                {
                    int picW = (int)(widthImg);
                    offsetX = (int)(pbWindow.ClientSize.Width * scale - picW) / 2;
                }

            }
            Rectangle roiSelect = new Rectangle((int)((rect.X + offsetX) / scale), (int)((rect.Y + offsetY) / scale), (int)(rect.Width / scale), (int)(rect.Height / scale));

            return roiSelect;
        }

        public static string GetStatusLog(string data)
        {
            string result = "";
            string dateTime = DateTime.Now.ToString() + ": ";
            result = dateTime + data;
            return result;
        }
        public static string GetStatusLog(double x, double y, double angle, string header = "")
        {
            string result = "";
            string dateTime = DateTime.Now.ToLongTimeString() + ": ";
            string data = $"{header}: X: {x},Y: {y}, Angle: {angle}";
            result = dateTime + data;
            return result;
        }
    }
}
