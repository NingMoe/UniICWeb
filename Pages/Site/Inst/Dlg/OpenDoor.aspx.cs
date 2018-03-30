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
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            ROOMCTRLREQ roomctrlInfo = new ROOMCTRLREQ();
            roomctrlInfo.dwDCSKind = (uint)UNIDCS.DWDCSKIND.DCSKIND_DOORCTRL;
            roomctrlInfo.szRoomNo = Request["id"];
            UNIDOORCTRL[] ctrlRes;
            m_Request.Device.GetRoomCtrlInfo(roomctrlInfo, out ctrlRes);
            if(!(ctrlRes!=null&&ctrlRes.Length>0))
            {
                MessageBox("开门失败不存在控制器", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }


            string szType = Request["type"];
            string szLabid=Request["labid"];
            ROOMCTRLINFO devCtrl = new ROOMCTRLINFO();
            devCtrl.dwCtrlSN = ctrlRes[0].dwCtrlSN;
            devCtrl.dwRoomID = ctrlRes[0].dwRoomID;
            devCtrl.dwCmd = (uint)DEVCTRLINFO.DWCMD.DEVCMD_DOOROPEN;
            devCtrl.szParam = "3";
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
