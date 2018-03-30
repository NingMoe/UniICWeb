using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using System.Xml;
using System.Collections;
using UniWebLib;
using System.Security.Cryptography;
using System.Net.Security;

public partial class LoginAll : UniPage
{
    public class LoginRes
    {
        public int nRes;//0表示失败，1表示成功
        public string szResInfo;
        public account accInfo;
    };
    public class account
    {
        public string szLogonNmae;
        public string szTrueName;
        public int ident;
        public string szClassName;
    };
    protected void Page_Load(object sender, EventArgs e)
    {
        string szLogonName = Request["logonname"];
        string szPassword = Request["password"];
        LoginRes loginRes = new LoginRes();
        if (!string.IsNullOrEmpty(szLogonName) && !string.IsNullOrEmpty(szPassword))
        {
            ADMINLOGINREQ vrLogin = new ADMINLOGINREQ();
            ADMINLOGINRES vrLoginRes;
            vrLogin.szLogonName = szLogonName;
            vrLogin.szPassword = "P" + szPassword;
            vrLogin.dwLoginRole = (uint)ADMINLOGINREQ.DWLOGINROLE.LOGIN_USER;
            vrLogin.szVersion = ((uint)ADMINLOGINREQ.SZVERSION.INTVER_MAIN).ToString() + "." + ((uint)ADMINLOGINREQ.SZVERSION.INTVER_RELEASE).ToString("00") + "." + ((uint)ADMINLOGINREQ.SZVERSION.INTVER_INTERNAL).ToString();
            vrLogin.szIP = "";
            vrLogin.dwStaSN = 1;
            m_Request.m_UniDCom.StaSN = 1;
            m_Request.m_UniDCom.SessionID = 0;
            vrLogin.dwLoginRole = vrLogin.dwLoginRole + (uint)ADMINLOGINREQ.DWLOGINROLE.LOGINEXT_HP;
            if (m_Request.Admin.StaLogin(vrLogin, out vrLoginRes) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                
                HttpContext.Current.Session["ADMINLOGINREQ"] = vrLogin;
                HttpContext.Current.Session["LoginResult"] = vrLoginRes;

                m_Request.m_UniDCom.SessionID = (uint)vrLoginRes.dwSessionID;
                m_Request.m_UniDCom.StaSN = 1;

                Session["SessionID"] = vrLoginRes.dwSessionID;
                loginRes.nRes = 1;

                loginRes.accInfo = new account();
                loginRes.accInfo.szClassName = vrLoginRes.AccInfo.szClassName;
                loginRes.accInfo.szLogonNmae = vrLoginRes.AccInfo.szLogonName;
                loginRes.accInfo.szTrueName = vrLoginRes.AccInfo.szTrueName;
                loginRes.accInfo.ident = (int)vrLoginRes.AccInfo.dwIdent;
            }
            else
            {
                loginRes.nRes = 0;
                loginRes.szResInfo = m_Request.szErrMessage;
            }
        }
        else {
            loginRes.nRes = 0;
            loginRes.szResInfo = "用户名或者密码不能为空";
        }
        Logout();
        Response.Write(JsonConvert.SerializeObject(loginRes));
        Response.End();

    }
   
    protected void Logout()
    {
        if (Session["SessionID"] == null) return;
        if (Session["LoginResult"] == null) return;

        ADMINLOGOUTREQ vrParameter = new ADMINLOGOUTREQ();
        vrParameter.dwAccNo = ((ADMINLOGINRES)Session["LoginResult"]).AdminInfo.dwAccNo;
        vrParameter.szLogonName = ((ADMINLOGINRES)Session["LoginResult"]).AdminInfo.szLogonName; 

        ADMINLOGOUTRES vrResult;
        m_Request.m_UniDCom.SessionID = (uint)Session["SessionID"];
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