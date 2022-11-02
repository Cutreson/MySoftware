using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using RTCVision2101.Classes;
//using RTCVision2101.Consts;
//using RTCVision2101.Forms;

namespace MySoftware
{
    /// <summary>
    /// Global Variables
    /// </summary>
    static class GlobVar
    {
        public static bool LockEvents=false;
        //public static List<cProject> Projects = null;
        public static bool IsDebugHalcon;
        public static bool OnlySetValue=false;
        public static Dictionary<string, cCamImage> DicCams;

        /// <summary>
        /// Đối tượng chứa thông tin của toàn hệ thống
        /// </summary>
        public static cSystemTypes SystemFiles;
        /// <summary>
        /// Engine nhằm khởi tạo môi trường làm việc cho HALCON
        /// </summary>
        public static bool IsLinkMode;
        public static bool IsLinkStringBuilderItemMode;
        public static long ID_ROICurrent ;
        /// <summary>
        /// Cửa sổ đồ họa window
        /// </summary>
        //public static FrmHsmartWindow fHsmartWindow;
        /// <summary>
        /// Là biến trung chuyển dữ liệu xuyên suốt toàn bộ chương trình
        /// </summary>
        // public static cGroupActions GroupActions;
        /// <summary>
        /// Form Action
        /// </summary>
        //public static RTCVision2101.Forms.FrmActions FormActions;

        public static Cursor ZoomCursor = null;
        /// <summary>
        /// Cờ báo chỉ chạy 1 action thuần túy, không can thiệp đồ họa
        /// </summary>
        public static bool RunSimple = false;
        /// <summary>
        /// File này chuyên dùng để thực hiện việc DeepCopy
        /// <para>Do cơ chế gán chuyển hoặc SerializeObject bình thường vẫn bị lỗi con trỏ nhớ hoặc bị lỗi mất kiểu sau khi chuyển</para>
        /// <para>nên xây dựng kiểu deepcopy thông qua việc lưu mở dữ liệu</para>
        /// </summary>
        public static string DeepCopyFileName = string.Empty;
        public static ImageList imlActionType32 = null;
        public static ImageList imlActionType24 = null;
        public static ImageList imlActionType16 = null;

        /// <summary>
        /// Cờ báo form wait đang hoạt động
        /// </summary>
        public static bool IsWaitFormActive = false;
        /// <summary>
        /// Đối tượng quản lý hoạt động của wait form, splash screen
        /// </summary>
        /// <summary>
        /// Đối tượng đóng vai trò khởi tạo 1 server ảo nhằm trung chuyển dữ liệu khi là kết nối TCP/IP
        /// </summary>
       
        public static Dictionary<string, byte> DicASCII = null;
        public static int CurrentTool=-1;
        public static bool DebugActionInHalcon=false;
        public static bool DebugMode=true;
        public static Process ProcessComunication = null;
        public static bool SelectMode = false;
        public static Dictionary<string,cCamImage> SourceImages;


        //public static cDataCam DataCam = new cDataCam();
        //public static cServer Server;
        public static TCPIPClient TCPClient;
        public static TCPServer TCPServer;
        //public static cSLMP SLMP ;
        //public static cProject ProjectCurr ;

        public static int ConstantRound = 3;
        public static List<string> ListStatusLog = new List<string>();

    }
}
