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
        string szlogonname = Request["logonname"];
        string szPaassword = Request["password"];
        if (string.IsNullOrEmpty(szlogonname) || string.IsNullOrEmpty(szPaassword))
        {
            res.resStatus = unchecked((uint)REQUESTCODE.EXECUTE_FAIL);
            res.szErrormsg = "比如输入用户名和密码";
            res.objInfo = new UNIACCOUNT();
            Response.Write(JsonConvert.SerializeObject(res));
            Response.End();
        }
        LoginIn(szlogonname, szPaassword);

    }
    protected void LoginIn(string szLogonName, string szPassword)
    {
        LoginRes res = new LoginRes();
        ADMINLOGINREQ vrLogin = new ADMINLOGINREQ();
        ADMINLOGINRES vrLoginRes;
        vrLogin.szLogonName = szLogonName;
        vrLogin.szPassword = "P" + szPassword;
        uint role = (uint)ADMINLOGINREQ.DWLOGINROLE.LOGIN_USER|(uint)ADMINLOGINREQ.DWLOGINROLE.LOGINEXT_HP;
        vrLogin.dwLoginRole = role;
        vrLogin.szVersion = ((uint)ADMINLOGINREQ.SZVERSION.INTVER_MAIN).ToString() + "." + ((uint)ADMINLOGINREQ.SZVERSION.INTVER_RELEASE).ToString("00") + "." + ((uint)ADMINLOGINREQ.SZVERSION.INTVER_INTERNAL).ToString();
        vrLogin.szIP = "";
        vrLogin.dwStaSN = 1;
        m_Request.m_UniDCom.StaSN = 1;
        m_Request.m_UniDCom.SessionID = 0;

        if (m_Request.Admin.StaLogin(vrLogin, out vrLoginRes) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            res.resStatus = (uint)REQUESTCODE.EXECUTE_SUCCESS;
            res.objInfo = vrLoginRes.AccInfo;
            Response.Write(JsonConvert.SerializeObject(res));
            Response.End();
            Logout();
        }
        else
        {
            res.resStatus = unchecked((uint)REQUESTCODE.EXECUTE_FAIL);
            res.szErrormsg = m_Request.szErrMessage;
            res.objInfo = new UNIACCOUNT();
            Response.Write(JsonConvert.SerializeObject(res));
            Response.End();
        }
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