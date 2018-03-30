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
        if (IsPostBack)
        {
            string szMessageInfo = Request.Form["szMessageInfo"];
            string szDevID = Request["id"];
            string szLabID = Request["labid"];
            if (szDevID == null || szDevID == "")
            {                
                MessageBox("发送失败", "提示", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                return;
            }

            DEVCTRLINFO devCtrl = new DEVCTRLINFO();
            uint uDevID = Parse(szDevID);
            if (uDevID == 0)
            {
                MessageBox("发送失败", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.CANCEL);
                return;
            }
            devCtrl.dwCmd = (uint)DEVCTRLINFO.DWCMD.DEVCMD_ADMINMSG;
            devCtrl.dwDevID = uDevID;
            devCtrl.dwLabID = Parse(szLabID);
            devCtrl.szParam = szMessageInfo;
            m_Request.Device.DevCtrl(devCtrl, out devCtrl);
            MessageBox("发送成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.CANCEL);
        }

    }
}
