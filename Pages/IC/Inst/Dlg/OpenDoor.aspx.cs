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
            int nTime = 1;
            string openTime = Request["opentime"];
            if (!string.IsNullOrEmpty(openTime))
            {
                nTime = IntParse(openTime);
            }
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            string szType = Request["type"];
            string szLabid=Request["labid"];
            ROOMCTRLINFO devCtrl = new ROOMCTRLINFO();
            devCtrl.dwRoomID = Parse(Request["id"]);
            devCtrl.dwCmd = (uint)DEVCTRLINFO.DWCMD.DEVCMD_DOOROPEN;
            devCtrl.szParam = (nTime * 60).ToString();
            uResponse=m_Request.Device.RoomCtrl(devCtrl, out devCtrl);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox("开门成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
            }
            else
            {
                MessageBox(m_Request.szErrMessage, "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
            }


        }
	}
}
