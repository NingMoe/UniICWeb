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
    public string m_szTitle = "";
    public string m_szMsg = "";
    public string m_szMsg2 = "";
	public string m_szType = "";

    //http://update.unifound.net/wxnotice/qrcode.aspx?pcid=1&id=1&session=InDoor
	
	protected void Page_Load(object sender, EventArgs e)
	{
		string mode = Request["mode"];
		string dcs = Request["dcs"];
		string ctrl = Request["ctrl"];
		string msn = Request["msn"];

		MOBILEOPENDOORREQ req = new MOBILEOPENDOORREQ();
		MOBILEOPENDOORRES res;

       
		if(Request["DoLogon"] == "true")
		{
			if(Session["opendoorreq"] == null)
			{
				m_szTitle = "开门失败";
				m_szMsg = "请重试";
				m_szType = "0";
				Response.Redirect("WxOpenDoorMsg.aspx?type="+m_szType+"&title="+Server.UrlEncode(m_szTitle)+"&msg="+Server.UrlEncode(m_szMsg));
				return;
			}
			req = (MOBILEOPENDOORREQ)Session["opendoorreq"];
			req.szLogonName = Request["szLogonName"];
			req.szPassword = "P"+Request["szPassword"];
			if(Request["dwBind"] == "1")
			{
				req.dwProperty = (uint)MOBILEOPENDOORREQ.DWPROPERTY.MODPROP_BINDMSN;
			}
		}else{
			req.dwDCSSN = ToUint(dcs);
			req.dwCtrlSN = ToUint(ctrl);
			req.szMSN = msn;
			if(mode== "1")
			{
				req.dwCardMode = (uint)DOORCARDREQ.DWCARDMODE.DOORCARD_IN;
			}else{
				req.dwCardMode = (uint)DOORCARDREQ.DWCARDMODE.DOORCARD_OUT;
			}
            req.szIP = null;// GetRealIP();
			
			Session["opendoorreq"] = req;
		}
       
		REQUESTCODE uResponse = m_Request.DoorCtrlSrv.MobilOpenDoor(req, out res);
        if (res.szDispInfo != null)
        {
            res.szDispInfo = res.szDispInfo.Replace("微信", "该");
        }
        Session["opendoorres"] = res;
		if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && (res.dwUserKind & (uint)DOORCARDRES.DWUSERKIND.CARDUSER_PERMIT) != 0)
		{
            Logger.Trace(req);
            Logger.trace("开门成功");
			m_szTitle = "开门成功";
			m_szMsg = res.szDispInfo;
			m_szType = "1";
			
			//if(Request["dwBind"] == "1")
			//{
			//	m_szMsg2 = "√ 已绑定此微信号";
			//}
		}
		else
		{
            Logger.Trace(req);
            Logger.trace("开门失败");
            m_szTitle = "开门失败";
			m_szMsg = res.szDispInfo;
			m_szType = "0";
			
			if( (res.dwFailedType & (uint)MOBILEOPENDOORRES.DWFAILEDTYPE.MODFAILED_NOBIND) != 0)
			{
				m_szType = "2";
				if(string.IsNullOrEmpty(m_szMsg))
				{
					m_szMsg = "未绑定用户";
				}else{
					
				}
				m_szMsg += "，请输入账号和密码开门";
			}
		}

        if (m_szType == "0")
        {
            if (string.IsNullOrEmpty(m_szMsg))
            {
                if (string.IsNullOrEmpty(m_Request.szErrMessage) || m_Request.szErrMessage.IndexOf("Socket") > 0)
                {
                    m_szMsg = "操作失败，请重试";
                }
                else
                {
                    m_szMsg = m_Request.szErrMessage;
                }
            }
            if (string.IsNullOrEmpty(m_szMsg))
            {
                m_szMsg = "操作失败，请重试";
            }
        }

		Response.Redirect("WxOpenDoorMsg.aspx?type="+m_szType+"&title="+Server.UrlEncode(m_szTitle)+"&msg="+Server.UrlEncode(m_szMsg)+"&msg2="+Server.UrlEncode(m_szMsg2));
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
