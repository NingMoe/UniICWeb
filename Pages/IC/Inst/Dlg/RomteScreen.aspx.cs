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
    protected bool bSet = false;
    protected string m_Title = "";
	protected void Page_Load(object sender, EventArgs e)
	{
        if (IsPostBack)
        {
            string szType = Request["type"];
            if (szType == "11")
            {
                m_Title = "远程开机";
            }
            else if (szType == "12")
            {
                m_Title = "远程关机";
            }
            else if (szType == "12")
            {
                m_Title = "远程重启";
            }
            else if (szType == "51")
            {
                m_Title = "免登录";
            }
            else if (szType == "52")
            {
                m_Title = "需要登录";
            }
            DEVCTRLINFO devSenInfo = new DEVCTRLINFO();
            devSenInfo.dwDevID = Parse(Request["id"]);
            devSenInfo.dwLabID = 167;
            devSenInfo.dwCmd = Parse(szType);
            m_Request.Device.DevCtrl(devSenInfo, out devSenInfo);
//            Response.Write(" <script language=\"javascript\" type=\"text/javascript\"> setTimeout(function(){Dlg_Cancel();}, 1);</script>");
        }
	}
}
