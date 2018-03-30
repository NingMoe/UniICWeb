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
   
    protected string szVersion = ((uint)ADMINLOGINREQ.SZVERSION.INTVER_MAIN).ToString() + "." + ((uint)ADMINLOGINREQ.SZVERSION.INTVER_RELEASE).ToString("00") + "." + ((uint)ADMINLOGINREQ.SZVERSION.INTVER_INTERNAL).ToString();
    protected string szPasswd = "uniFound808";


    protected void Page_Load(object sender, EventArgs e)
    {
        string url = HttpContext.Current.Request.Url.Query;

        string szSignKey = Request["signkey"];
        string szuid = Request["uid"];
        if ((!string.IsNullOrEmpty(szSignKey)) && (!string.IsNullOrEmpty(szuid)))
        {
            Logger.trace("szSignKey=" + szSignKey + ";szuid=" + szuid);
            Logger.trace("szuid=" + szuid);
            GetUserInfoFromUrl(szuid, szSignKey);
        }
      
    }
    private bool GetUserInfoFromUrl(string uid, string szSignKey)
    {
        string szKey = "X(J@L*!IA";

        string szDate = DateTime.Now.ToString("yyyyMMdd");

        string ma5 = uid + szKey;
        string ma5Next = ma5+ DateTime.Now.AddDays(1).ToString("yyyyMMdd");
        ma5 = ma5 + szDate;
        string EnPswdStr = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(ma5, "MD5");
        string EnPswdStrNext = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(ma5Next, "MD5");
        if (szSignKey.ToLower() == EnPswdStr.ToLower()|| szSignKey.ToLower() == EnPswdStrNext.ToLower())
        {
            LoginUseInfo info = new LoginUseInfo();
            info.szPassword = szPasswd;
            info.szLogoName = uid;
            Session["LoginUseInfo"] = info;

            Logger.trace(uid + "微信跳转登录成功");
            Logger.trace("登录账户：" + uid);
            // Response.Write(uid+"__"+szPasswd);
            // return true;
            string szUrl = "clientweb/m/ic2/default.aspx?version=" + szVersion;
            if (!string.IsNullOrEmpty(Request["syskind"]))
            {
                szUrl = szUrl + "&syskind=" + Request["syskind"];
            }
            Response.Redirect(szUrl);

            return true;
        }
        else
        {
            Logger.trace(uid + "微信跳转登录失败；本地加密:" + EnPswdStr + ";传入加密值:" + szSignKey);
            return false;
        }

    }
    protected void Logout()
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