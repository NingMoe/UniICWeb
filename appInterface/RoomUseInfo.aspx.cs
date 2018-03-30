using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using Newtonsoft.Json;

public partial class login :UniPage
{
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
      
        if (m_Request.Device.FullRoomGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            res.resStatus = (uint)REQUESTCODE.EXECUTE_SUCCESS;
            res.szErrormsg = m_Request.szErrMessage;
            res.objInfo = vrResult;

            Logout();

        }
        else {
            res.resStatus = unchecked((uint)REQUESTCODE.EXECUTE_FAIL);
            res.szErrormsg = m_Request.szErrMessage;
            res.objInfo = vrResult;
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