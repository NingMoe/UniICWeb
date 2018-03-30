using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using Newtonsoft.Json;

public partial class login :UniPage
{
    /// <summary>
    /// 每种空间的总数，空闲数
    /// </summary>
    public class RoomTotalInfo {
        public uint uRoomTotal;
        public uint uRoomIdle;
        public int uRoomRate;
        public uint uSeatTotal;
        public uint uSeatIdel;
        public int uSeatRate;
        public uint uPCTotal;
        public uint uPCIdel;
        public int uPCRate;
        public uint uLendTotal;
        public uint uLendIdel;
        public int uLendRate;
    }
    /// <summary>
    /// 研修间各个房间是否空闲
    /// </summary>
    public class StudyRoomUseInfo
    {
        public string szRoomNO;
        public string szRoomName;
        public int nStatus;
        public int nKindID;
        public string szKindName;
        public int nLabID;
        public string szLabName;
        public int nOrderKey;
        
    }
    /// <summary>
    /// 当前生效预约
    /// </summary>
    public class resvShowInfo
    {
        public string szDevName;
        public string szTrueName;
        public int nStatus;
        public string szBeginTime;
        public string szEndTime;
        public int nClassKind;
        public string szTestName;

    }
    public class LoginRes
    {
        public int nStatus;//1表示成功，4表示失败
        public enum Status : int
        {
            //[EnumDescription("成功")]
            SUCCESS = 1,

            //[EnumDescription("失败")]
            ERROR = 4,

        };
        public int nErrorCode;//1表示成功，4表示失败
        public string szError;
        public string total;
        public object rows;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        LoginRes res = new LoginRes();
        res.nStatus = (int)LoginRes.Status.ERROR;
        res.szError = "未找到对应的方法";
        string szOP = Request["opname"];

        if (!string.IsNullOrEmpty(szOP))
        {
            szOP = szOP.ToLower();
            switch (szOP)
            {
                case "getroomtotaluse":
                    GetRoomTotalUse();//只返回每一个大类的总数，空闲数目
                    break;
                case "getroomuseinfo":
                    GetRoomUseInfo();// 获取房间下面的设备总数，空闲数目，只针对座位和电脑
                    break;
                case "getstudyroomuse":
                    GetStudyRoomUse();//每个研修间的状态
                    break;
                case "getdoingresv":
                    GetDoingResv();//当前生效预约
                    break;
                case "getdevusetotal":
                    GetDevUseTotal();//获取设备使用统计排行榜
                    break;
                case "getlabfloor":
                    GetLabFloor();//获取设备使用统计排行榜
                    break;
                default:
                    break;
            }
        }
        Response.Write(JsonConvert.SerializeObject(res));
        Response.End();
    }
    /// <summary>
    /// 只返回每一个大类的总数，空闲数目
    /// </summary>
    public void GetRoomTotalUse()
    {
        LoginRes res = new LoginRes();
        ADMINLOGINREQ vrLogin = new ADMINLOGINREQ();
        ADMINLOGINRES vrLoginRes;
        vrLogin.szLogonName = "Guest";
        vrLogin.szPassword = "P";
        uint role = (uint)ADMINLOGINREQ.DWLOGINROLE.LOGIN_USER | (uint)ADMINLOGINREQ.DWLOGINROLE.LOGINEXT_HP;
        vrLogin.dwLoginRole = role;
        vrLogin.szVersion = ((uint)ADMINLOGINREQ.SZVERSION.INTVER_MAIN).ToString() + "." + ((uint)ADMINLOGINREQ.SZVERSION.INTVER_RELEASE).ToString("00") + "." + ((uint)ADMINLOGINREQ.SZVERSION.INTVER_INTERNAL).ToString();
        vrLogin.szIP = "";
        vrLogin.dwStaSN = 1;
        m_Request.m_UniDCom.StaSN = 1;
        m_Request.m_UniDCom.SessionID = 0;

        FULLROOMREQ vrParameter = new FULLROOMREQ();
        FULLROOM[] vrResult;

        if (m_Request.Admin.StaLogin(vrLogin, out vrLoginRes) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            m_Request.m_UniDCom.SessionID = (uint)vrLoginRes.dwSessionID;
            m_Request.m_UniDCom.StaSN = 1;
        }
        else
        {
            res.nStatus = (int)LoginRes.Status.ERROR;
            res.szError = m_Request.szErrMessage;
            //res.objInfo = null;
        }

        uint uRoomTotal = 0;
        uint uRoomIdle = 0;
        uint uSeatTotal = 0;
        uint uSeatIdel = 0;
        uint uPCTotal = 0;
        uint uPCIdel = 0;
        uint uLendTotal = 0;
        uint uLendIdel = 0;
        vrParameter.dwInClassKind = (uint)UNIDEVCLS.DWKIND.CLSKIND_SEAT;
        RoomTotalInfo resRoom = new RoomTotalInfo();
        if (m_Request.Device.FullRoomGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {

            for (int i = 0; i < vrResult.Length; i++)
            {
                uint uKind = (uint)vrResult[i].dwInClassKind;
                if ((uKind & (uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS) > 0)
                {
                    uRoomTotal += (uint)vrResult[i].dwUsableDevNum;
                    uRoomIdle += (uint)vrResult[i].dwIdleDevNum;

                }
                else if ((uKind & (uint)UNIDEVCLS.DWKIND.CLSKIND_COMPUTER) > 0)
                {
                    uPCTotal += (uint)vrResult[i].dwUsableDevNum;
                    uPCIdel += (uint)vrResult[i].dwIdleDevNum;
                }
                else if ((uKind & (uint)UNIDEVCLS.DWKIND.CLSKIND_SEAT) > 0)
                {
                    uSeatTotal += (uint)vrResult[i].dwUsableDevNum;
                    uSeatIdel += (uint)vrResult[i].dwIdleDevNum;
                }
                else if ((uKind & (uint)UNIDEVCLS.DWKIND.CLSKIND_LOAN) > 0)
                {
                    uLendTotal += (uint)vrResult[i].dwUsableDevNum;
                    uLendIdel += (uint)vrResult[i].dwIdleDevNum;
                }
            }

            DEVREQ devreq = new DEVREQ();
            UNIDEVICE[] devList;
            if (m_Request.Device.Get(devreq, out devList) == REQUESTCODE.EXECUTE_SUCCESS && devList != null && devList.Length > 0)
            {
                for (int i = 0; i < devList.Length; i++)
                {
                    uint uKind = (uint)devList[i].dwClassKind;
                    uint uRunState = (uint)devList[i].dwRunStat;
                    if ((uKind & (uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS) > 0)
                    {
                        uRoomTotal += 1;
                        if ((uRunState & (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_INUSE) > 0)
                        {
                            uRoomIdle += 1;
                        }

                    }
                    else if ((uKind & (uint)UNIDEVCLS.DWKIND.CLSKIND_COMPUTER) > 0)
                    {
                        uPCTotal += 1;

                        if ((uRunState & (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_INUSE) > 0)
                        {
                            uPCIdel += 1;
                        }
                    }
                }
            }
            uRoomIdle = uRoomTotal - uRoomIdle;
            uPCIdel = uPCTotal - uPCIdel;

            resRoom.uRoomTotal = uRoomTotal;
            resRoom.uRoomIdle = uRoomIdle;
            if (uRoomTotal == 0)
            {
                uRoomTotal = 1;
            }
            resRoom.uRoomRate = Convert.ToInt32((uRoomTotal - uRoomIdle) / (uRoomTotal * 1.0f) * 100);

            resRoom.uSeatTotal = uSeatTotal;
            resRoom.uSeatIdel = uSeatIdel;
            if (uSeatTotal == 0)
            {
                uSeatTotal = 1;
            }
            resRoom.uSeatRate = Convert.ToInt32((uSeatTotal - uSeatIdel) / (uSeatTotal * 1.0f) * 100);

            resRoom.uPCTotal = uPCTotal;
            resRoom.uPCIdel = uPCIdel;
            if (uPCTotal == 0)
            {
                uPCTotal = 1;
            }
            resRoom.uPCRate = Convert.ToInt32((uPCTotal - uPCIdel) / (uPCTotal * 1.0f) * 100);

            resRoom.uLendTotal = uLendTotal;
            resRoom.uLendIdel = uLendIdel;
            if (uLendTotal == 0)
            {
                uLendTotal = 1;
            }
            resRoom.uLendRate = Convert.ToInt32((uLendTotal - uLendIdel) / (uLendTotal * 1.0f) * 100);


            res.nStatus = (int)LoginRes.Status.SUCCESS;
            res.szError = m_Request.szErrMessage;
            res.rows = resRoom;

            Logout();

        }
        else
        {
            res.nStatus =((int)LoginRes.Status.ERROR);
            res.szError = m_Request.szErrMessage;
           // res.objInfo = resRoom;
        }
        Response.Write(JsonConvert.SerializeObject(res));
        Response.End();
    }
    /// <summary>
    /// 获取房间下面的设备总数，空闲数目，只针对座位和电脑
    /// </summary>
    public void GetRoomUseInfo()
    {
        LoginRes res = new LoginRes();
        FULLROOMREQ vrParameter = new FULLROOMREQ();
        FULLROOM[] vrResult;
        uint uSessionID = GusetLogin();
        if (uSessionID == 0)
        {
            res.nStatus = (int)LoginRes.Status.ERROR;
            res.szError = m_Request.szErrMessage;
            Response.Write(JsonConvert.SerializeObject(res));
            Response.End();
            return;
        }
        m_Request.m_UniDCom.SessionID = uSessionID;
        List<FULLROOM> fullRoomRes = new List<FULLROOM>();
        FULLROOM[] roomRes;
        if (m_Request.Device.FullRoomGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            uint uCount = 0;
            for (int i = 0; i < vrResult.Length; i++)
            {
                uint uClasssKind = (uint)vrResult[i].dwInClassKind;
                if ((uClasssKind & (uint)UNIDEVCLS.DWKIND.CLSKIND_COMPUTER)>0)
                {
                    fullRoomRes.Add(vrResult[i]);
                    uCount = uCount + 1;
                }
                else if ((uClasssKind & (uint)UNIDEVCLS.DWKIND.CLSKIND_SEAT) > 0)
                {
                    fullRoomRes.Add(vrResult[i]);
                    uCount = uCount + 1;
                }
            }
            roomRes = fullRoomRes.ToArray();
            res.nStatus = (int)LoginRes.Status.SUCCESS;
            res.total = uCount.ToString();
            res.rows = roomRes;
            Response.Write(JsonConvert.SerializeObject(res));
            Response.End();
            return;
        }
        Logout();
    }
    public void GetLabFloor()
    {
        LoginRes res = new LoginRes();
        LABREQ vrParameter = new LABREQ();
        UNILAB[] vrResult;
        uint uSessionID = GusetLogin();
        if (uSessionID == 0)
        {
            res.nStatus = (int)LoginRes.Status.ERROR;
            res.szError = m_Request.szErrMessage;
            Response.Write(JsonConvert.SerializeObject(res));
            Response.End();
            return;
        }
        m_Request.m_UniDCom.SessionID = uSessionID;
        
        if (m_Request.Device.LabGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            res.nStatus = (int)LoginRes.Status.SUCCESS;
            res.total = vrResult.Length.ToString();
            res.rows = vrResult;
            Response.Write(JsonConvert.SerializeObject(res));
            Response.End();
            return;
        }
        Logout();

    }
    /// <summary>
    /// 
    /// </summary>
    public void GetDoingResv()
    {
        LoginRes res = new LoginRes();
        RESVSHOWREQ vrParameter = new RESVSHOWREQ();
        vrParameter.dwCheckStat = (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DOING;
        RESVSHOW[] vrResult;
        uint uSessionID = GusetLogin();
        if (uSessionID == 0)
        {
            res.nStatus = (int)LoginRes.Status.ERROR;
            res.szError = m_Request.szErrMessage;
            Response.Write(JsonConvert.SerializeObject(res));
            Response.End();
            return;
        }
        m_Request.m_UniDCom.SessionID = uSessionID;
        List<resvShowInfo> resvRes = new List<resvShowInfo>();

        if (m_Request.Reserve.GetReserveForShow(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            uint uCount = 0;
            for (int i = 0; i < vrResult.Length; i++)
            {
                resvShowInfo resvTemp = new resvShowInfo();
                uint uPurpose = 0;
                uint uPurposeTemp = (uint)vrResult[i].dwPurpose;
                if ((uPurposeTemp & (uint)UNIRESERVE.DWPURPOSE.USEFOR_STUDYROOM) > 0)
                {
                    uPurpose = (uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS;
                }
                else if ((uPurposeTemp & (uint)UNIRESERVE.DWPURPOSE.USEFOR_PC) > 0)
                {
                    uPurpose = (uint)UNIDEVCLS.DWKIND.CLSCOMPUTER_PC;
                }
                else if ((uPurposeTemp & (uint)UNIRESERVE.DWPURPOSE.USEFOR_SEAT) > 0)
                {
                    uPurpose = (uint)UNIDEVCLS.DWKIND.CLSKIND_SEAT;
                }
                resvTemp.nClassKind = (int)uPurpose;
                resvTemp.nStatus = (int)vrResult[i].dwStatus;
                resvTemp.szBeginTime = Get1970Date(vrResult[i].dwBeginTime);
                resvTemp.szEndTime = Get1970Date(vrResult[i].dwEndTime);
                resvTemp.szDevName = vrResult[i].szDevName;
                resvTemp.szTestName = vrResult[i].szTestName;
                resvTemp.szTrueName = vrResult[i].szOwnerName;
                resvRes.Add(resvTemp);
                uCount = uCount + 1;
            }
            res.nStatus = (int)LoginRes.Status.SUCCESS;
            res.total = uCount.ToString();
            res.rows = resvRes.ToArray(); ;
          
        }
        else {
            res.nStatus = (int)LoginRes.Status.ERROR;
            res.szError = m_Request.szErrMessage;
        }
        Response.Write(JsonConvert.SerializeObject(res));
        Response.End();
        
        Logout();
        return;
    }
    /// <summary>
    /// 获取设备使用统计排行榜
    /// </summary>
    public void GetDevUseTotal()
    {
        LoginRes res = new LoginRes();
        uint uSessionID = GusetLogin();
        if (uSessionID == 0)
        {
            res.nStatus = (int)LoginRes.Status.ERROR;
            res.szError = m_Request.szErrMessage;
            Response.Write(JsonConvert.SerializeObject(res));
            Response.End();
            return;
        }
        m_Request.m_UniDCom.SessionID = uSessionID;
        REPORTREQ vrParameter = new REPORTREQ();
        vrParameter.szReqExtInfo.szOrderKey = "dwTotalUseTime";
        vrParameter.szReqExtInfo.szOrderMode = "desc";
        string szClassKind = Request["classkind"];
        if (!string.IsNullOrEmpty(szClassKind))
        {
            uint uClassKind = Parse(szClassKind);
            if (uClassKind == (uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS)
            {
                vrParameter.dwClassKind = (uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS;
            }
            else if (uClassKind == (uint)UNIDEVCLS.DWKIND.CLSKIND_SEAT)
            {
                vrParameter.dwClassKind = (uint)UNIDEVCLS.DWKIND.CLSKIND_SEAT;
            }
            else if (uClassKind == (uint)UNIDEVCLS.DWKIND.CLSKIND_COMPUTER)
            {
                vrParameter.dwClassKind = (uint)UNIDEVCLS.DWKIND.CLSKIND_COMPUTER;
            }
        }
      
        DEVSTAT[] vrResult;
        if (m_Request.Report.GetDevStat(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            res.nStatus = (int)LoginRes.Status.SUCCESS;
            res.total = vrResult.Length.ToString();
            res.rows = vrResult;
        }
        else
        {
            res.nStatus = (int)LoginRes.Status.ERROR;
            res.szError = m_Request.szErrMessage;
        }
        Response.Write(JsonConvert.SerializeObject(res));
        Response.End();

        Logout();
        return;
        
    }
    /// <summary>
    /// 获取每个研修间的使用状态
    /// </summary>
    public void GetStudyRoomUse()
    {
        LoginRes res = new LoginRes();
        uint uSessionID = GusetLogin();
        if (uSessionID == 0)
        {
            res.nStatus = (int)LoginRes.Status.ERROR;
            res.szError = m_Request.szErrMessage;
            Response.Write(JsonConvert.SerializeObject(res));
            Response.End();
            return;
        }
        m_Request.m_UniDCom.SessionID = uSessionID;
        DEVREQ vrParameter = new DEVREQ();
        string szLabid=Request["labid"];
        if (!string.IsNullOrEmpty(szLabid))
        {
            vrParameter.szLabIDs = szLabid;
        }
        vrParameter.dwClassKind = (uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS;
        UNIDEVICE[] vrResult;

        if (m_Request.Device.Get(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            uint uCount = 0;
            List<StudyRoomUseInfo> roomRes = new List<StudyRoomUseInfo>();
            for (int i = 0; i < vrResult.Length; i++)
            {
                uCount = uCount + 1;
                StudyRoomUseInfo room = new StudyRoomUseInfo();
                room.szRoomNO = vrResult[i].szRoomNo; ;
                room.szRoomName = vrResult[i].szDevName;
                room.nStatus = 1;
                room.szKindName = vrResult[i].szKindName;
                room.nKindID = (int)vrResult[i].dwKindID;
                room.nLabID = (int)vrResult[i].dwLabID;
                room.szLabName = vrResult[i].szLabName;
                uint uPrice = 0;
                if (vrResult[i].dwUnitPrice != null)
                {
                    uPrice = (uint)vrResult[i].dwUnitPrice;
                }
                room.nOrderKey = (int)uPrice;

                uint uStatue = 0;
                if (vrResult[i].dwRunStat != null)
                {
                    uint uStatueTemp = (uint)vrResult[i].dwRunStat;
                    if ((uStatueTemp & (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_INUSE) > 0)
                    {
                        uStatue = (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_INUSE;
                    }
                }
                room.nStatus = (int)uStatue;
                roomRes.Add(room);
            }
       
            res.nStatus = (int)LoginRes.Status.SUCCESS;
            res.total = uCount.ToString();
            res.rows = roomRes.ToArray();
           
        }
        else
        {
            res.nStatus = (int)LoginRes.Status.ERROR;
            res.szError = m_Request.szErrMessage;
        }
        Response.Write(JsonConvert.SerializeObject(res));
        Response.End();

        Logout();
        return;
    }
    public uint GusetLogin()
    {
        ADMINLOGINREQ vrLogin = new ADMINLOGINREQ();
        ADMINLOGINRES vrLoginRes;
        vrLogin.szLogonName = "Guest";
        vrLogin.szPassword = "P";
        uint role = (uint)ADMINLOGINREQ.DWLOGINROLE.LOGIN_USER | (uint)ADMINLOGINREQ.DWLOGINROLE.LOGINEXT_HP;
        vrLogin.dwLoginRole = role;
        vrLogin.szVersion = ((uint)ADMINLOGINREQ.SZVERSION.INTVER_MAIN).ToString() + "." + ((uint)ADMINLOGINREQ.SZVERSION.INTVER_RELEASE).ToString("00") + "." + ((uint)ADMINLOGINREQ.SZVERSION.INTVER_INTERNAL).ToString();
        vrLogin.szIP = "";
        vrLogin.dwStaSN = 1;
        m_Request.m_UniDCom.StaSN = 1;
        m_Request.m_UniDCom.SessionID = 0;

        if (m_Request.Admin.StaLogin(vrLogin, out vrLoginRes) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            return (uint)vrLoginRes.dwSessionID;
           
        }
        return 0;
    }
    void Logout()
    {
        if (Session["SessionID"] == null) return;
        if (Session["LoginResult"] == null) return;

        ADMINLOGOUTREQ vrParameter = new ADMINLOGOUTREQ();
        vrParameter.dwAccNo = ((ADMINLOGINRES)Session["LoginResult"]).AdminInfo.dwAccNo;
        vrParameter.szLogonName = ((ADMINLOGINRES)Session["LoginResult"]).AdminInfo.szLogonName;

        ADMINLOGOUTRES vrResult;
        REQUESTCODE ret1 = m_Request.Admin.Logout(vrParameter, out vrResult);
        if (ret1 == REQUESTCODE.EXECUTE_SUCCESS)
        {
        }
        Session["SessionID"] = null;
        Session["LoginResult"] = null;
        Session["URLHistoryStack"] = null;
        m_Request.m_UniDCom.Close();
    }
}