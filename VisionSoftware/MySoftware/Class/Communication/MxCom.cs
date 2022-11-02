using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySoftware.Class.Communication
{
    //public class MxCom
    //{
    //    public ActUtlType PLCMxCom;
    //    int StationNumber;
    //    public bool IsConnected;
    //    public MxCom()
    //    {
    //        PLCMxCom = new ActUtlType();
    //    }
    //    public MxCom(int StationNumber)
    //    {
    //        PLCMxCom = new ActUtlType();
    //        PLCMxCom.ActLogicalStationNumber = StationNumber;
    //    }
    //    public bool Open()
    //    {
    //        bool rs = false;
    //        if (PLCMxCom != null)
    //        {
    //            try
    //            {
    //                PLCMxCom.Open();
    //                rs = true;

    //            }
    //            catch (Exception)
    //            {
    //                rs = false;
    //            }
    //        }
    //        else
    //            rs = false;

    //        IsConnected = rs;
    //        return rs;

    //    }
    //    public bool Close()
    //    {
    //        bool rs = false;
    //        if (PLCMxCom != null)
    //        {
    //            try
    //            {
    //                PLCMxCom.Close();
    //                rs = true;
    //            }
    //            catch (Exception)
    //            {
    //                rs = false;
    //            }
    //        }
    //        else
    //            rs = true;

    //        IsConnected = false;

    //        return rs;

    //    }

    //    public bool SendBit(int iData, string sDevice)
    //    {
    //        PLCMxCom.SetDevice(sDevice, iData);
    //        return true;
    //    }

    //    public bool RecieveBit(string sDevice, out int iData)
    //    {
    //        PLCMxCom.GetDevice(sDevice, out iData);
    //        return true;
    //    }
    //    public int [] ReceiveBitArr(string sDevice, out int [] iData)
    //    {
    //        int [] data = new int[] { 1 };
    //        iData = new int[] { 1 };
    //        return data;

    //        //PLCMxCom.GetDevice(sDevice, out iData);
    //    }

    //}
}
