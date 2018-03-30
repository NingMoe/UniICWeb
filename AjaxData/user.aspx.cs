using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using Newtonsoft.Json;
public partial class Resv_searchCls :UniPage
{
    public class AjaxRes
    {
        public int nStatus;//1表示成功，4表示失败
        public enum Status : uint
        {
            //[EnumDescription("成功")]
            SUCCESS = 1,

            //[EnumDescription("失败")]
            ERROR = 4,

        };
        public string szError;
        public string total;
        public object rows;
        public AjaxRes()
        {
            nStatus = (int)AjaxRes.Status.ERROR;
            szError = "";
            total = "0";
            rows = new object();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string szOp = Request["op"];
        if (szOp.ToLower() == "scanlogin")
        {
            SacnLogin();
        }
        if (szOp.ToLower() == "getresv")
        {
            GetResv();
        }

    }
    protected void SacnLogin()
    {
        AjaxRes ajaxRes = new AjaxRes();
        ajaxRes.nStatus = (int)AjaxRes.Status.ERROR;
        string szLogonName = Request["logonName"];
        string szPassword = Request["password"];
        string szMsn = Request["msn"];
        ADMINLOGINREQ vrLogin = new ADMINLOGINREQ();
        ADMINLOGINRES vrLoginRes;
        vrLogin.szLogonName = szLogonName;
        if (!string.IsNullOrEmpty(szPassword))
        {
            vrLogin.szPassword = "P" + szPassword;
        }
        vrLogin.szIP = GetRealIP();
        if (!string.IsNullOrEmpty(szMsn))
        {
            vrLogin.szMSN = szMsn;
        }
        
        m_Request.m_UniDCom.StaSN = 1;
        
        vrLogin.dwLoginRole = (uint)ADMINLOGINREQ.DWLOGINROLE.LOGIN_USER | (uint)ADMINLOGINREQ.DWLOGINROLE.LOGINEXT_HP;
        vrLogin.szVersion = ((uint)ADMINLOGINREQ.SZVERSION.INTVER_MAIN).ToString() + "." + ((uint)ADMINLOGINREQ.SZVERSION.INTVER_RELEASE).ToString("00") + "." + ((uint)ADMINLOGINREQ.SZVERSION.INTVER_INTERNAL).ToString();
        vrLogin.szIP = GetRealIP();
        vrLogin.dwStaSN = 1;
        m_Request.m_UniDCom.StaSN = 1;
        m_Request.m_UniDCom.SessionID = 0;

        if (!(vrLogin.szLogonName == null && vrLogin.szMSN == "abc123"))
        {

            REQUESTCODE uResponse = m_Request.Admin.StaLogin(vrLogin, out vrLoginRes);
            if (uResponse == REQUESTCODE.ERR_MSN_NOBIND)
            {
                ajaxRes.nStatus = (int)AjaxRes.Status.ERROR;
                ajaxRes.szError = "该微信号未绑定账号";
            }
            else if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
            {
                Session["sessionid"] = m_Request.m_UniDCom.SessionID;
                ajaxRes.nStatus = (int)AjaxRes.Status.SUCCESS;
            }
        }
        string szRes = JsonConvert.SerializeObject(ajaxRes);
        Response.Write(szRes);
        Response.End();
    }
    public void GetResv()
    {
        AjaxRes ajaxRes = new AjaxRes();
        RESVREQ vrGet = new RESVREQ();
        vrGet.dwCheckStat = (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DOING;
        vrGet.dwClassKind = (uint)UNIDEVCLS.DWKIND.CLSKIND_SEAT;
        m_Request.m_UniDCom.SessionID = (uint)Session["sessionid"];
        UNIRESERVE[] vtRes;
        REQUESTCODE uResponse = m_Request.Reserve.Get(vrGet, out vtRes);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            ajaxRes.nStatus = (int)AjaxRes.Status.SUCCESS;
            ajaxRes.total = vtRes.Length.ToString();
            ajaxRes.rows = vtRes;
        }
        else {
            ajaxRes.nStatus = (int)AjaxRes.Status.ERROR;
            ajaxRes.szError = m_Request.szErrMessage;
        }
        string szRes = JsonConvert.SerializeObject(ajaxRes);
        Response.Write(szRes);
        Response.End();
    }
    public void checkIn()
    {

    }
    public void ResvOver()
    {

    }
    public void ResvPasteOver()
    {

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