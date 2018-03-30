using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using UniStruct;
using UniWebLib;
using Util;

/// <summary>
/// UniThirdInterface 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ScriptService]
public class UniThirdInterface : UniBaseService
{
    
    public UniThirdInterface()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }


    //上财IC空间 获取座位空闲数
    [WebMethod(EnableSession = true, Description = "获取座位空闲数")]
    [System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
    public ITEMSeat[] GetFreeSeat()
    {
        UniClientCommon common = new UniClientCommon();
        common.Login("guest", "");
        soaphead.SessionID = common.m_Request.m_UniDCom.SessionID;
        soaphead.StationSN = common.m_Request.m_UniDCom.StaSN;

        List<ITEMSeat> list = new List<ITEMSeat>();
        FULLROOMREQ req = new FULLROOMREQ();
        req.dwInClassKind = 8;//座位
        req.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_SEAT;
        FULLROOM[] rlt;
        if (m_Request.Device.FullRoomGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                ITEMSeat it = new ITEMSeat();
                it.name = rlt[i].szRoomName;
                it.dwIdleDevNum = rlt[i].dwIdleDevNum;
                it.dwUsableDevNum = rlt[i].dwUsableDevNum;
                it.szLabName = rlt[i].szLabName;
                list.Add(it);
            }
        }
        return list.ToArray();
    }

    //上财IC空间 获取座位空闲数
    [WebMethod(EnableSession = true, Description = "获取研修间空闲数")]
    [System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
    public ITEMRoom[] GetRoomSeat()
    {
        UniClientCommon common = new UniClientCommon();
        common.Login("guest", "");
        soaphead.SessionID = common.m_Request.m_UniDCom.SessionID;
        soaphead.StationSN = common.m_Request.m_UniDCom.StaSN;

        List<ITEMRoom> list = new List<ITEMRoom>();
        FULLLABREQ req = new FULLLABREQ();
        // req.dwInClassKind = 8;//座位
        req.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_STUDYROOM;
        FULLLAB[] rlt;
        if (m_Request.Device.FullLabGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                if (((uint)rlt[i].dwLabClass & 1) > 0)
                {
                    ITEMRoom it = new ITEMRoom();
                    it.name = rlt[i].szLabName;
                    it.dwIdleDevNum = rlt[i].dwIdleDevNum;
                    it.dwUsableDevNum = rlt[i].dwUsableDevNum;
                    list.Add(it);
                }
            }
        }
        return list.ToArray();
    }



    public struct RESVINFO
    {
        public string id;
        public string type;
        public string position;
        public string themeType;
        public string theme;
        public string user;
        public string userID;
        public string time;
    }


    public struct REPORT
    {
        public int id;
        public string type;
        public ITEMSeat[] items;
    }
    public struct ITEMSeat
    {
        public string name;
        public uint? dwIdleDevNum;
        public uint? dwUsableDevNum;
        public string szLabName;
    }
    public struct ITEMRoom
    {
        public string name;
        public uint? dwIdleDevNum;
        public uint? dwUsableDevNum;
    }
}
