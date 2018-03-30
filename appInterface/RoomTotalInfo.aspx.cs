using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using Newtonsoft.Json;

public partial class login :UniPage
{
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
    public class LoginRes {
        public uint resStatus;
        public string szErrormsg;
        public object objInfo;
    }

    protected void Page_Load(object sender, EventArgs e)
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
        else {
            res.resStatus = unchecked((uint)REQUESTCODE.EXECUTE_FAIL);
            res.szErrormsg = m_Request.szErrMessage;
            res.objInfo = null;
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
                    //http://localhost:8295/appinterface/RoomTotalInfo.aspx
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

            resRoom.uRoomTotal= uRoomTotal;
            resRoom.uRoomIdle = uRoomIdle;
            if (uRoomTotal == 0)
            {
                uRoomTotal = 1;
            }
            resRoom.uRoomRate = Convert.ToInt32((uRoomTotal - uRoomIdle) / (uRoomTotal * 1.0f)*100);
           
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


            res.resStatus = (uint)REQUESTCODE.EXECUTE_SUCCESS;
            res.szErrormsg = m_Request.szErrMessage;
            res.objInfo = resRoom;

            Logout();

        }
        else {
            res.resStatus = unchecked((uint)REQUESTCODE.EXECUTE_FAIL);
            res.szErrormsg = m_Request.szErrMessage;
            res.objInfo = resRoom;
        }
        Response.Write(JsonConvert.SerializeObject(res));
        Response.End();


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