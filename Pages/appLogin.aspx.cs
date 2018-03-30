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
using Newtonsoft.Json;
public partial class _Default : UniPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string szActivitySN = Request["sn"];
        string szOp = Request["op"];

        if (!string.IsNullOrEmpty(szActivitySN))
        {
            ViewState["activitysn"] = szActivitySN;
        }
        if (!string.IsNullOrEmpty(szOp))
        {
            ViewState["op"] = szOp;
        }


        if (!IsPostBack)
        {
            string szLogonName = Request["logonname"];
            string szPassword = Request["password"];
            
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
                    HttpContext.Current.Session["LoginRes"] = vrLoginRes;
                    HttpContext.Current.Session["ADMINLOGINREQ"] = vrLogin;
                    m_Request.m_UniDCom.SessionID = (uint)vrLoginRes.dwSessionID;
                    m_Request.m_UniDCom.StaSN = 1;

                    Session["SessionID"] = vrLoginRes.dwSessionID;
                    Session["StationSN"] = 1;

                    if (ViewState["op"] != null && ViewState["op"].ToString()!="")
                    {
                        if (szOp.ToLower() == "groupmember")
                        {
                            Response.Redirect("groupmember.aspx?sn="+ ViewState["activitysn"].ToString());
                            Response.End();
                        }
                        else if (szOp.ToLower() == "joinout")
                        {
                            Response.Redirect("join.aspx?sn="+ ViewState["activitysn"].ToString());
                            Response.End();
                        }
                    }
                }
            }
            else
            {

            }
        }
    }


}