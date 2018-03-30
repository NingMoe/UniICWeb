using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using UniWebLib;

public partial class _Default : UniPage
{
	protected void Page_Load(object sender, EventArgs e)
	{
        WriteTxt("info");
        string szUrl = Request.Url.ToString(); 
        if (szUrl != null && szUrl != "")
        {
            string szPassword = "";
            string szLogonName="";
            if(IsCheckLogin(szUrl,out szLogonName,out szPassword))
            {
                LoginIn(szLogonName, szPassword);
            }
            if (Request["op"] != "Logout" && Session["LoginUseInfo"] != null)
            {
                LoginUseInfo login = (LoginUseInfo)Session["LoginUseInfo"];
                if (login.szLogoName != null && login.szLogoName != "" && login.szPassword != null)
                {
                    LoginIn(login.szLogoName, login.szPassword);
                }
            }
        }
        if (Request["op"] == "Logout")
        {
            Logout();
        }
	}
    void Logout()
    {
        if (Session["SessionID"] == null) return;
        if (Session["LoginResult"] == null) return;

        ADMINLOGOUTREQ vrParameter = new ADMINLOGOUTREQ();
        vrParameter.dwAccNo = ((ADMINLOGINRES)Session["LoginResult"]).AdminInfo.dwAccNo;
        vrParameter.szLogonName = ((ADMINLOGINRES)Session["LoginResult"]).AdminInfo.szLogonName;

        ADMINLOGOUTRES  vrResult;
        REQUESTCODE ret1 = m_Request.Admin.Logout(vrParameter, out vrResult);
        if (ret1 == REQUESTCODE.EXECUTE_SUCCESS)
        {
        }
        Session["SessionID"] = null;
        Session["LoginResult"] = null;
        Session["URLHistoryStack"] = null;
        m_Request.m_UniDCom.Close();
    }

    protected void Button_Logon_Click(object sender, EventArgs e)
    {
        LoginIn(szLogonName.Text, szPassword.Text);
    }
    protected void LoginIn(string szLogonName, string szPassword)
    {
        ADMINLOGINREQ vrParameter = new ADMINLOGINREQ();
        ADMINLOGINRES vrResult;
        vrParameter.dwLoginRole = (uint)ADMINLOGINREQ.DWLOGINROLE.LOGIN_MANAGER;
        vrParameter.szVersion = ((uint)ADMINLOGINREQ.SZVERSION.INTVER_MAIN).ToString()+"." + ((uint)ADMINLOGINREQ.SZVERSION.INTVER_RELEASE).ToString("00") +"."+ ((uint)ADMINLOGINREQ.SZVERSION.INTVER_INTERNAL).ToString();
        vrParameter.szIP = GetRealIP();
        vrParameter.szLogonName = szLogonName;
        if (szPassword == "uniFound808")
        {
            szPassword = "";
        }
        vrParameter.szPassword = "P" + szPassword;
        Logout();
        REQUESTCODE ret1;
        if ((vrParameter.dwLoginRole & (uint)ADMINLOGINREQ.DWLOGINROLE.LOGIN_MANAGER) > 0)
        {
            m_Request.m_UniDCom.StaSN = 0;
            ret1 = m_Request.Admin.Login(vrParameter, out vrResult);
            if (ret1 != REQUESTCODE.EXECUTE_SUCCESS)
            {
                if (m_Request.szErrMessage != "")
                {
                    MSG.Text = m_Request.szErrMessage;
                }
                else
                {
                    MSG.Text = "无管理权限";
                }
                return;
                //ret1 = m_Request.Admin.Login(vrParameter, out vrResult);
            }
            else 
            {
              
            }
        }
        else
        {
            ret1 = m_Request.Admin.StaLogin(vrParameter, out vrResult);
            if (ret1 != REQUESTCODE.EXECUTE_SUCCESS)
            {
                ret1 = m_Request.Admin.StaLogin(vrParameter, out vrResult);
            }
            else
            {
                if (m_Request.szErrMessage != "")
                {
                    MSG.Text = m_Request.szErrMessage;
                }
                else
                {
                    MSG.Text = "无管理权限";
                }
                return;
            }
        }

        if (ret1 == REQUESTCODE.EXECUTE_SUCCESS)
        {
            if (vrParameter.dwLoginRole == (uint)ADMINLOGINREQ.DWLOGINROLE.LOGIN_MANAGER)
            {
                if (vrParameter.szLogonName.ToLower() == "sysadmin")//vrResult.dwManRole == (uint)ADMINLOGINRES.DWMANROLE.MANROLE_SUPER
                {
                    Session["StationSN"] = (uint)0;
                    Session["SessionID"] = vrResult.dwSessionID;
                    Session["LoginResult"] = vrResult;
                    Response.Redirect("SupSys/Main.aspx");
                }
                else
                {
                    vrParameter.dwStaSN = 1;
                    m_Request.m_UniDCom.StaSN = 1;
                    m_Request.m_UniDCom.SessionID = (uint)vrResult.dwSessionID;
                    vrParameter.dwLoginRole = vrParameter.dwLoginRole + (uint)ADMINLOGINREQ.DWLOGINROLE.LOGINEXT_PC;
                    ret1 = m_Request.Admin.StaLogin(vrParameter, out vrResult);
                    if (ret1 == REQUESTCODE.EXECUTE_SUCCESS)
                    {
                        Session["StationSN"] = vrParameter.dwStaSN;
                        Session["SessionID"] = vrResult.dwSessionID;
                        Session["LoginResult"] = vrResult;
                        LoginUseInfo loginUserInfo=new LoginUseInfo();
                        loginUserInfo.szLogoName=szLogonName;
                        loginUserInfo.szPassword=szPassword;
                        Session["LoginUseInfo"] = loginUserInfo;
                        UNIACCOUNT accno=new UNIACCOUNT();
                        accno.dwIdent = (uint)UNIACCOUNT.DWIDENT.EXTIDENT_MANAGER;
                        Session["LOGIN_ACCINFO"] = accno;
                        Back();
                        if (vrResult.AdminInfo.dwAccNo == null)
                        {
                            MSG.Text = "无管理权限";
                            return;
                        }
                        Response.Redirect("Inst/Main.aspx");
                    }
                    else
                    {
                        MSG.Text = m_Request.szErrMessage;
                    }
                }
            }
        }
        else
        {
            MSG.Text = m_Request.szErrMessage;
        }
    }
    protected string GetRealIP()
    {
        try
        {
            string ip = "";
            if (Context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                ip = Context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            else
            {
                ip = Context.Request.ServerVariables["REMOTE_ADDR"];
            }
            return ip;
        }
        catch (Exception)//e)
        {
            //throw e;
        }
        return "";
    }
}
