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
            string szLabid=Request["labid"];
            UNIDEVICE devCtrl = new UNIDEVICE();          
            devCtrl.dwDevStat = (uint)UNIDEVICE.DWDEVSTAT.DEVSTAT_MAINTAIN;
            devCtrl.dwDevID = Parse(Request["id"]);
            m_Request.Device.Set(devCtrl, out devCtrl);
            MessageBox("设备报修成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);


        }
	}
}
