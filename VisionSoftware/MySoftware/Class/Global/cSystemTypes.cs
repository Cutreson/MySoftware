///<summary>Class nhằm lưu trữ toàn bộ các thông tin của chương trình</summary>
///<remarks>Created by DATRUONG</remarks>
//using RTCVision2101.Enums;
//using RTCVision2101.Variables;
using System;
using System.IO;
using System.Management;
using System.Windows.Forms;

namespace MySoftware
{
    public enum EViewCamInMainFormMode
    {
        Normal = 0,
        GroupCAMWithTab = 1,
        GroupCAMWithoutTab = 2
    }
    public class cOSInfo
    {
        public string WindowName { get; set; }
        public string OSVersion { get; set; }
        public string OSPlatform { get; set; }
        public string OSServicePack { get; set; }
        public string OSVersionString { get; set; }
        public string MajorVersion { get; set; }
        public string MajorRevision { get; set; }
        public string MinorVersion { get; set; }
        public string MinorRevision { get; set; }
        public string Build { get; set; }

        public string FullInfo
        {
            get
            {
                return string.Join(";", WindowName, "Version: " + OSVersion, "Platform: " + OSPlatform, "ServicePack: " + OSServicePack, "Build: " + Build);
            }
        }
        //public string GetOSFriendlyName()
        //{
        //    string result = string.Empty;
        //    try
        //    {
        //        ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem");
        //        foreach (ManagementObject os in searcher.Get())
        //        {
        //            result = os["Caption"].ToString();
        //            break;
        //        }
        //    }
        //    catch (Exception) { }
        //    return result;
        //}
        //public cOSInfo()
        //{
        //    WindowName = GetOSFriendlyName();
        //}

    }
    /// <summary>
    /// Đối tượng lưu thông tin các đường dẫn thư mục.
    /// </summary>
    public class cPaths
    {
        /// <summary>
        /// Thự mục thực thi của chương trình.
        /// </summary>
        public string AppPath { get; set; }
        /// <summary>
        /// Thư mục chứa các procedure Halcon.
        /// </summary>
        public string Procedures { get; set; }
        /// <summary>
        /// Thư mục chứa các file mẫu của chương trình.
        /// </summary>
        public string Templates { get; set; }
        /// <summary>
        /// Thư mục lưu project.
        /// </summary>
        public string Projects { get; set; }
        public string History { get; set; }
        /// <summary>
        /// Thư mục nhớ lưu file thiết lập groupactions.
        /// </summary>
        public string OldPathSaveGroup { get; set; }
        /// <summary>
        /// Thư mục nhớ mở file thiết lập groupactions.
        /// </summary>
        public string OldPathOpenGroup { get; set; }

        public string Log { get; set; }

    }
    /// <summary>
    /// Đối tượng chứa thông tin các đường dẫn file.
    /// </summary>
    public class cFiles
    {
        /// <summary>
        /// Tên file db common
        /// </summary>
        public string Common { get; set; }
        /// <summary>
        /// Tên file mẫu db lưu project
        /// </summary>
        public string SaveTemplate { get; set; }
        /// <summary>
        /// Tên file Cursor làm biểu tượng zoom
        /// </summary>
        public string CursorZoom { get; set; }
        /// <summary>
        /// Tên file config
        /// </summary>
        public string Config { get; set; }
        /// <summary>
        /// Tên file mẫu history
        /// </summary>
        public string HistoryTemplate { get; set; }
        public string HistoryLogo { get; set; }
    }
    /// <summary>
    /// Đối tượng chứa thông tin các thiết lập của chương trình.
    /// </summary>
    public class cHistoryOptions
    {
        /// <summary>
        /// Cờ báo có lưu file lịch sử lên FTP hay ko
        /// </summary>
        public bool IsSaveToFTP { get; set; }
        /// <summary>
        /// Tên host
        /// </summary>
        public string HostName { get; set; }
        /// <summary>
        /// Tài khoản đăng nhập
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Mật khẩu FTP
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Tự động tạo folder chứa file lịch sử theo tên project
        /// </summary>
        public bool IsAutoCreateFolderByProjectName { get; set; }
        public cHistoryOptions()
        {
            IsSaveToFTP = false;
            HostName = string.Empty;
            UserName = string.Empty;
            Password = string.Empty;
            IsAutoCreateFolderByProjectName = false;
        }

    }
    public class cOptions
    {
        /// <summary>
        /// Cờ báo khi apply thông tin của 1 CAM cho CAM khác có apply thông tin các tools hay không.
        /// </summary>
        public bool ApplyActions_Tools { get; set; }
        /// <summary>
        /// Cờ báo khi apply thông tin của 1 CAM cho CAM khác có apply thông tin ảnh hay không.
        /// </summary>
        public bool ApplyActions_Images { get; set; }
        /// <summary>
        /// Thời gian để tạo khoảng trễ khi RUN multicam nhằm ghi ra kết quả trên màn hình
        /// <para>Đơn vị: ms</para>
        /// </summary>
        public int TimeDelayWhenRunCAM { get; set; }
        /// <summary>
        /// Hiển thị group cam trên màn hình theo tab hay không.
        /// <para>True: Có; False: Không</para>
        /// </summary>
        public EViewCamInMainFormMode ViewCamInMainFormMode { get; set; }
        /// <summary>
        /// Cờ báo có đánh lại tên ROIs hay không.
        /// <para>True: Có; False: Không</para>
        /// </summary>
        public bool RetypeNameROIs { get; set; }
        /// <summary>
        /// Cờ báo chương trình có đóng vai trò là 1 server hay ko
        /// </summary>
        public bool IsServer { get; set; }
        /// <summary>
        /// Cổng port khi đóng vai trò là server
        /// </summary>
        public int PortNumber { get; set; }
    }
    public class cSecurityOptions
    {
        /// <summary>
        /// Cờ báo có sử dụng mật khẩu cho việc chỉnh sửa thông tin tool hay không.
        /// </summary>
        public bool IsHavePassOfToolSetting { get; set; }
        /// <summary>
        /// Cờ báo không hỏi lại password
        /// </summary>
        public bool IsKeepLoginToolSetting { get; set; }
        /// <summary>
        /// Cờ báo đã đăng nhập thành công
        /// </summary>
        public bool IsLoginToolSettingSuccess { get; set; }
        /// <summary>
        /// Mật khẩu chỉnh sửa tool
        /// </summary>
        public string PassWordOfToolSetting { get; set; }
        
        public cSecurityOptions()
        {
            IsLoginToolSettingSuccess = false;
            IsKeepLoginToolSetting = false;
            IsHavePassOfToolSetting = true;
            PassWordOfToolSetting = "sdv2021@";
        }
    }
    /// <summary>
    /// Đối tượng chứa thông tin của toàn bộ chương trình khi chạy.
    /// </summary>
    public class cSystemTypes
    {
        //private ELanguage _ELanguage=ELanguage.Eng;
        ///// <summary>
        ///// Ngôn ngữ sử dụng của chương trình
        ///// </summary>
        //public ELanguage Language
        //{
        //    get { return _ELanguage; }
        //    set { _ELanguage = value; }
        //}
        /// <summary>
        /// Thông tin path
        /// </summary>
        public cPaths Paths { get; set; }
        /// <summary>
        /// Thông tin về files
        /// </summary>
        public cFiles Files { get; set; }
        /// <summary>
        /// Thông tin Windows
        /// </summary>
        public cOSInfo OSInfo { get; set; }
        /// <summary>
        /// Thông tin Options
        /// </summary>
        public cOptions Options { get; set; }
        /// <summary>
        /// Thiết lập lịch sử
        /// </summary>
        public cHistoryOptions HistoryOptions { get; set; }
        /// <summary>
        /// Thiết lập bảo mật
        /// </summary>
        public cSecurityOptions SecurityOptions { get; set; }
        public cSystemTypes()
        {
            Paths = new cPaths();
            Files = new cFiles();
            OSInfo = new cOSInfo();
            Options = new cOptions();
            HistoryOptions = new cHistoryOptions();
            SecurityOptions = new cSecurityOptions();
        }
        /// <summary>
        /// Đọc thông tin các path sử dụng
        /// </summary>
        private void ReadDefault_Paths()
        {
            Paths.AppPath = AppDomain.CurrentDomain.BaseDirectory;
            Paths.Procedures = AppDomain.CurrentDomain.BaseDirectory + "Procedures";
            Paths.Templates = AppDomain.CurrentDomain.BaseDirectory + "Templates";
            Paths.Projects = AppDomain.CurrentDomain.BaseDirectory + "Projects";
            Paths.History = AppDomain.CurrentDomain.BaseDirectory + "History";
            Paths.Log = AppDomain.CurrentDomain.BaseDirectory + "Log";
            Paths.OldPathSaveGroup = Paths.AppPath;
            Paths.OldPathOpenGroup = Paths.AppPath;
        }
        /// <summary>
        /// Đọc thông tin các file sử dụng
        /// </summary>
        private void ReadDefault_Files()
        {
            Files.Common = Paths.Templates + Path.DirectorySeparatorChar + "Common.db";
            Files.SaveTemplate = Paths.Templates + Path.DirectorySeparatorChar + "SaveTemplate.db";
            Files.CursorZoom = Paths.Templates + Path.DirectorySeparatorChar + "CursorZoom.cur";
            Files.Config = Paths.AppPath + Path.DirectorySeparatorChar + "Config.ini";
            Files.HistoryTemplate = Paths.Templates + Path.DirectorySeparatorChar + "HistoryReport.xlsx";
            Files.HistoryLogo = Paths.Templates + Path.DirectorySeparatorChar + "HistoryReportLogo.png";
        }
        /// <summary>
        /// Đọc thông tin Windows
        /// </summary>
        private void ReadDefault_OSInfo()
        {
            OperatingSystem os = Environment.OSVersion;
            OSInfo.OSVersion = os.Version.ToString();
            OSInfo.OSPlatform = os.Platform.ToString();
            OSInfo.OSServicePack = os.ServicePack.ToString();
            OSInfo.OSVersionString = os.VersionString.ToString();

            Version ver = os.Version;
            OSInfo.MajorVersion = ver.Major.ToString();
            OSInfo.MajorRevision = ver.MajorRevision.ToString();
            OSInfo.MinorVersion = ver.Minor.ToString();
            OSInfo.MinorRevision = ver.MinorRevision.ToString();
            OSInfo.Build = ver.Build.ToString();
        }
        /// <summary>
        /// Đọc thông tin default Options
        /// </summary>
        private void ReadDefault_Options()
        {
            Options.ApplyActions_Images = true;
            Options.ApplyActions_Tools = true;
            Options.TimeDelayWhenRunCAM = 10;
            Options.ViewCamInMainFormMode = EViewCamInMainFormMode.Normal;
            Options.RetypeNameROIs = true;
            Options.IsServer = true;
            Options.PortNumber = 3000;
        }
        /// <summary>
        /// Thiết lập default
        /// </summary>
        public void ReadDefault()
        {
            ReadDefault_Paths();
            ReadDefault_Files();
            //ReadDefault_OSInfo();
            //ReadDefault_Options();
            //if (System.IO.File.Exists(Files.CursorZoom))
            //    GlobVar.ZoomCursor = new Cursor(Files.CursorZoom);
            //else
            //    GlobVar.ZoomCursor = Cursors.Default;
        }
        /// <summary>
        /// Lưu thiết lập chương trình thành file
        /// </summary>
        //public void SaveIniConfig()
        //{
        //    cIniFile oINIFile = new cIniFile(Files.Config);
        //    try
        //    {
        //        {
        //            oINIFile.WriteString("Paths", "OldPathSaveGroup", Paths.OldPathSaveGroup);
        //            oINIFile.WriteString("Paths", "OldPathOpenGroup", Paths.OldPathOpenGroup);
        //            oINIFile.WriteString("Paths", "Projects", Paths.Projects);
        //            oINIFile.WriteString("Paths", "History", Paths.History);

        //            oINIFile.WriteBoolean("Options", "ApplyActions_Tools", Options.ApplyActions_Tools);
        //            oINIFile.WriteBoolean("Options", "ApplyActions_Images", Options.ApplyActions_Images);
        //            oINIFile.WriteInteger("Options", "TimeDelayWhenRunCAM", Options.TimeDelayWhenRunCAM);
        //            oINIFile.WriteInteger("Options", "ViewGroupCAMInTab", (int)Options.ViewCamInMainFormMode);
        //            oINIFile.WriteBoolean("Options", "RetypeNameROIs", Options.RetypeNameROIs);
        //            oINIFile.WriteBoolean("Options", "IsServer", Options.IsServer);
        //            oINIFile.WriteInteger("Options", "PortNumber", Options.PortNumber);

        //            oINIFile.WriteBoolean("HistoryOptions", "IsSaveToFTP", HistoryOptions.IsSaveToFTP);
        //            oINIFile.WriteBoolean("HistoryOptions", "IsAutoCreateFolderByProjectName", HistoryOptions.IsAutoCreateFolderByProjectName);
        //            oINIFile.WriteString("HistoryOptions", "HostName", HistoryOptions.HostName);
        //            //oINIFile.WriteString("HistoryOptions", "UserName", cEncryptDecrypt.Encrypt(HistoryOptions.UserName));
        //            //oINIFile.WriteString("HistoryOptions", "Password", cEncryptDecrypt.Encrypt(HistoryOptions.Password));

        //            //oINIFile.WriteBoolean("SecurityOptions", "IsHavePassOfToolSetting", SecurityOptions.IsHavePassOfToolSetting);
        //            //oINIFile.WriteString("SecurityOptions", "PassWordOfToolSetting", cEncryptDecrypt.Encrypt(SecurityOptions.PassWordOfToolSetting));
        //        }
        //    }
        //    finally
        //    {
        //        oINIFile = null;
        //    }
        //}
        /// <summary>
        /// Đọc thiết lập chương trình từ file
        /// </summary>
        //public void ReadIniConfig()
        //{
        //    ReadDefault();
        //    cIniFile oINIFile = new cIniFile(Files.Config);
        //    try
        //    {
        //        Paths.OldPathOpenGroup = oINIFile.GetString("Paths", "OldPathOpenGroup", Paths.OldPathOpenGroup);
        //        Paths.OldPathSaveGroup = oINIFile.GetString("Paths", "OldPathSaveGroup", Paths.OldPathSaveGroup);
        //        Paths.Projects = oINIFile.GetString("Paths", "Projects", Paths.Projects);
        //        Paths.History = oINIFile.GetString("Paths", "History", Paths.History);

        //        Options.ApplyActions_Images = oINIFile.GetBoolean("Options", "ApplyActions_Images", Options.ApplyActions_Images);
        //        Options.ApplyActions_Tools = oINIFile.GetBoolean("Options", "ApplyActions_Tools", Options.ApplyActions_Tools);
        //        Options.TimeDelayWhenRunCAM = oINIFile.GetInteger("Options", "TimeDelayWhenRunCAM", Options.TimeDelayWhenRunCAM);
        //        Options.ViewCamInMainFormMode =(EViewCamInMainFormMode)oINIFile.GetInteger("Options", "ViewGroupCAMInTab", (int)Options.ViewCamInMainFormMode);
        //        Options.RetypeNameROIs = oINIFile.GetBoolean("Options", "RetypeNameROIs", Options.RetypeNameROIs);
        //        Options.IsServer = oINIFile.GetBoolean("Options", "IsServer", Options.IsServer);
        //        Options.PortNumber = oINIFile.GetInteger("Options", "PortNumber", Options.PortNumber);

        //        HistoryOptions.IsSaveToFTP = oINIFile.GetBoolean("HistoryOptions", "IsSaveToFTP", HistoryOptions.IsSaveToFTP);
        //        HistoryOptions.IsAutoCreateFolderByProjectName = oINIFile.GetBoolean("HistoryOptions", "IsAutoCreateFolderByProjectName", HistoryOptions.IsAutoCreateFolderByProjectName);
        //        HistoryOptions.HostName = oINIFile.GetString("HistoryOptions", "HostName", HistoryOptions.HostName);
        //        HistoryOptions.UserName = cEncryptDecrypt.Decrypt(oINIFile.GetString("HistoryOptions", "UserName", HistoryOptions.UserName));
        //        HistoryOptions.Password =cEncryptDecrypt.Decrypt(oINIFile.GetString("HistoryOptions", "Password", HistoryOptions.Password));

        //        SecurityOptions.IsHavePassOfToolSetting = oINIFile.GetBoolean("SecurityOptions", "IsHavePassOfToolSetting", SecurityOptions.IsHavePassOfToolSetting);
        //        SecurityOptions.PassWordOfToolSetting = cEncryptDecrypt.Decrypt(oINIFile.GetString("SecurityOptions", "PassWordOfToolSetting", cEncryptDecrypt.Encrypt(SecurityOptions.PassWordOfToolSetting)));
        //    }
        //    finally
        //    {
        //        oINIFile = null;
        //    }
        //}

    }
}
