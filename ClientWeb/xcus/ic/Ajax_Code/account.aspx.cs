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
using System.Collections.Generic;
using UniWebLib;

public partial class Page_Account : UniClientPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Buffer = true;
        Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1); 
        Response.Expires = 0;
        Response.CacheControl = "no-cache";

        base.LoadPage();
        if (Request["act"] == "login")
        {
            if ((Request["pwd"]).Trim() == "uniFound808")
            {
                Response.Write("{\"MsgId\":1,\"Message\":\"密码不可用\"}");
                return;
            }
            if (common.Login(Request["id"], Request["pwd"]))
            {
                UNIACCOUNT vrAccInfo;
                vrAccInfo = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
                string allow = GetConfig("allowIdent");
                if (allow != "" && allow != "0")
                {
                    uint ident = ToUInt(allow);
                    if ((vrAccInfo.dwIdent & ident) == 0)
                    {
                        Response.Write("{\"MsgId\":1,\"Message\":\"对不起，您的身份不允许登录。\"}");
                        common.ClearLogin();
                        return;
                    }
                }
                if (GetConfig("mustAct")=="1"&&(vrAccInfo.szEmail.ToString().Trim() == "" || vrAccInfo.szHandPhone.ToString().Trim() == ""))
                {                  
                    Response.Write("{\"MsgId\":1,\"Message\":\"您是第一次登录，请点上面“新用户请先激活”进行激活\"}");
                    common.ClearLogin();
                }
                else
                {
                   
                    string szRes = "";                 
                    if (szRes != "")
                    {
                        Response.Write("{\"MsgId\":1,\"Message\":\"" +"登陆成功,"+ szRes +"有违约，请关闭后刷新"+ "\"}");
                    }
                    else {
                        Response.Write("{\"MsgId\":0,\"Message\":\"\"}");
                    }
                }
            }
            else
            {
                Response.Write("{\"MsgId\":1,\"Message\":\"" + m_Request.szErrMessage + "\"}");
            }
        }
        else if (Request["act"] == "act")
        {
            if (common.Login(Request["id"], Request["pwd"]))
            {
                UNIACCOUNT vrAccInfo;
                vrAccInfo = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
                if (vrAccInfo.szEmail.ToString().Trim() != "" && vrAccInfo.szHandPhone.ToString().Trim() != "")
                {
                    Response.Write("{\"MsgId\":1,\"Message\":\"用户已激活，请勿重复操作\"}");
                }
                else
                {
                    UNIACCOUNT vrParameter = new UNIACCOUNT();
                    vrParameter.dwAccNo = vrAccInfo.dwAccNo;
                    vrParameter.szLogonName = vrAccInfo.szLogonName.ToString();
                    vrParameter.szHandPhone = Request["phone"];
                    vrParameter.szEmail = Request["mail"];
                    if (m_Request.Account.Set(vrParameter, out vrParameter) == REQUESTCODE.EXECUTE_SUCCESS)
                    {
                        Response.Write("{\"MsgId\":0,\"Message\":\"\"}");
                    }
                    else
                    {
                        Response.Write("{\"MsgId\":1,\"Message\":\"" + m_Request.szErrMessage + "\"}");
                    }
                }
            }
            else
            {
                Response.Write("{\"MsgId\":1,\"Message\":\"" + m_Request.szErrMessage + "\"}");
            }
        }
        else if (Request["act"] == "update")
        {
            if (Session["LOGIN_ACCINFO"] != null)
            {
                UNIACCOUNT vrAccInfo;
                vrAccInfo = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
                UNIACCOUNT vrParameter = new UNIACCOUNT();
                vrParameter.dwAccNo = vrAccInfo.dwAccNo;
                vrParameter.szLogonName = vrAccInfo.szLogonName.ToString();
                vrParameter.szHandPhone = Request["phone"];
                vrParameter.szEmail = Request["mail"];
                if (m_Request.Account.Set(vrParameter, out vrParameter) == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    vrAccInfo.szHandPhone = vrParameter.szHandPhone;
                    vrAccInfo.szEmail = vrParameter.szEmail;
                    Response.Write("{\"MsgId\":0,\"Message\":\"\"}");
                }
                else
                {
                    Response.Write("{\"MsgId\":1,\"Message\":\"" + m_Request.szErrMessage+ "\"}");
                }
            }
            else
            {
                Response.Write("{\"MsgId\":1,\"Message\":\"未登录\"}");
            }

        }
        else if (Request["act"] == "logout")
        {
            if (Session["LOGIN_ACCINFO"] != null)
            {
                UNIACCOUNT vrAccInfo;
                vrAccInfo = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
                ADMINLOGOUTREQ vrParameter = new ADMINLOGOUTREQ();
                ADMINLOGOUTRES vrResult;
                vrParameter.dwAccNo = vrAccInfo.dwAccNo;
                vrParameter.szLogonName = vrAccInfo.szLogonName;
                REQUESTCODE cd= m_Request.Admin.Logout(vrParameter, out vrResult);
            }
            common.ClearLogin();

            Response.Write("{\"MsgId\":0,\"Message\":\"\"}");
        }
        else if (Request["act"] == "check")
        {
            ACCREQ vrParameter = new ACCREQ();
            UNIACCOUNT[] vrResult;
            vrParameter.szPID=Request["id"];
            if (m_Request.Account.Get(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS&&vrResult.Length>0)
            {
                Response.Write("{\"MsgId\":0,\"Message\":\"\"}");
            }
            else
            {
                Response.Write("{\"MsgId\":1,\"Message\":\"" + m_Request .szErrMessage+ "\"}");
            }
        }
    }
}
