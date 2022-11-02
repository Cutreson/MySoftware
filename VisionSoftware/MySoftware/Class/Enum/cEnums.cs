using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySoftware
{
    [Serializable]
    public enum ECameraType
    {
        GigE_Basler,
        GigE_Hikvision,
        GigE_PointGrey,
        WebCam,
        Image,
        GigE_Crevis,
        NIK
    }
    public enum EDRAWINGTYPE
    {
        RECTANGLE2 = 0,
        ELLIPSE = 1,
    }
    public enum ECONNECTTYPE
    {
        DIFFERENCE = 0,
        UNION2 = 1,
        INTERSECTION = 2,
        NORMAL = 3
    }

    public enum EROIFUNCIONTYPE
    {
        TRAIN = 0,
        SEARCH = 1,
        BLOB = 2,
    }

    public enum ECAMFUNCIONTYPE
    {
        PREBONDER = 0,
        LOADER = 1,
        HEADTIP = 2,
    }
    public enum ECAMINDEX
    {
        CAM1 = 0,
        CAM2 = 1,
    }
    public enum EMARKFUNCION
    {
        PANELMARK = 0,
        CHIPMARK = 1,
    }
    public enum EROIDISPLAYTYPE
    {
        MARK = 0,
        SEARCH = 1,
    }
    public enum SELECTPARTCALIB
    {
        CHIP = 0,
        STAGE1 = 1,
        STAGE2 = 2

    }
    public enum EALIGNTYPE
    {
        ONPICKUP = 0,
        ONTOOL = 1,
    }
    public enum EMarkTypes
    {
        Search = 0,
        Train = 1,
        None = 99
    }
    public enum EComunicationType
    {
        TCPIP = 0,
        SLMP = 1,
        Serial  = 2
    }
    public enum ESLMPValueType
    {
        Bit = 0,
        Word = 1,
        DWord = 2
    }
    public enum EDeviceType
    {
        Bit = 0,
        Word = 1,
        DWord = 2
    }
    public enum EAlignCommuType
    {
        TCPIP = 0,
        SLMP = 1,
    }
    
   
}
