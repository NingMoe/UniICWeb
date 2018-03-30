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

public partial class seatInfo : UniPage
{
    public string szDevTotal = "";
    public string szDevLev = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["LoginResult"] == null)
        {
            LoginIn("staadmin001", "unifound808");
        }
        FULLROOMREQ vrParameter = new FULLROOMREQ();
        FULLROOM[] vrResult;

        uint uTotal = 0;
        uint uLevel = 0;
        if (m_Request.Device.FullRoomGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                uTotal += (uint)vrResult[i].dwUsableDevNum;
                uLevel += (uint)vrResult[i].dwIdleDevNum;
            }
        }
        szDevTotal = uTotal.ToString();
        szDevLev = uLevel.ToString();
    }

    protected void LoginIn(string szLogonName, string szPassword)
    {
        ADMINLOGINREQ vrParameter = new ADMINLOGINREQ();
        ADMINLOGINRES vrResult;
        vrParameter.dwLoginRole = (uint)ADMINLOGINREQ.DWLOGINROLE.LOGIN_MANAGER;
        vrParameter.szVersion = ((uint)ADMINLOGINREQ.SZVERSION.INTVER_MAIN).ToString() + "." + ((uint)ADMINLOGINREQ.SZVERSION.INTVER_RELEASE).ToString("00") + "." + ((uint)ADMINLOGINREQ.SZVERSION.INTVER_INTERNAL).ToString();
        vrParameter.szIP = "127.0.0.1";
        vrParameter.szLogonName = szLogonName;
        vrParameter.szPassword = "P" + szPassword;
        REQUESTCODE ret1;
        if ((vrParameter.dwLoginRole & (uint)ADMINLOGINREQ.DWLOGINROLE.LOGIN_MANAGER) > 0)
        {
            m_Request.m_UniDCom.StaSN = 0;
            //vrParameter.dwLoginRole =vrParameter.dwLoginRole + (uint)ADMINLOGINREQ.DWLOGINROLE.LOGINEXT_PC;
            ret1 = m_Request.Admin.Login(vrParameter, out vrResult);
            if (ret1 != REQUESTCODE.EXECUTE_SUCCESS)
            {

                if (m_Request.szErrMessage != "")
                {

                }
                else
                {

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
            vrParameter.dwLoginRole = vrParameter.dwLoginRole + (uint)ADMINLOGINREQ.DWLOGINROLE.LOGINEXT_PC;
            ret1 = m_Request.Admin.StaLogin(vrParameter, out vrResult);
            if (ret1 != REQUESTCODE.EXECUTE_SUCCESS)
            {
                ret1 = m_Request.Admin.StaLogin(vrParameter, out vrResult);
            }
            else
            {
                if (m_Request.szErrMessage != "")
                {

                }
                else
                {

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
                        LoginUseInfo loginUserInfo = new LoginUseInfo();
                        loginUserInfo.szLogoName = szLogonName;
                        loginUserInfo.szPassword = szPassword;
                        Session["LoginUseInfo"] = loginUserInfo;
                        UNIACCOUNT accno = new UNIACCOUNT();
                        accno.dwIdent = (uint)UNIACCOUNT.DWIDENT.EXTIDENT_MANAGER;
                        Session["LOGIN_ACCINFO"] = accno;
                        m_Request.m_UniDCom.SessionID = (uint)vrResult.dwSessionID;

                        if (vrResult.AdminInfo.dwAccNo == null)
                        {

                            return;
                        }
                        // Response.Redirect("Inst/Main.aspx");
                    }
                    else
                    {

                    }
                }
            }
        }
        else
        {

        }
    }
}