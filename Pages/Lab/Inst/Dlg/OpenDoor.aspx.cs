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
    protected string sz_Doorctrl = "";
	protected void Page_Load(object sender, EventArgs e)
	{

        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        ROOMCTRLREQ roomctrlInfo = new ROOMCTRLREQ();
        roomctrlInfo.dwDCSKind = (uint)UNIDCS.DWDCSKIND.DCSKIND_DOORCTRL;
        roomctrlInfo.szRoomNo = Request["id"];
        UNIDOORCTRL[] ctrlRes;
        uResponse=m_Request.Device.GetRoomCtrlInfo(roomctrlInfo, out ctrlRes);
        if (uResponse==REQUESTCODE.EXECUTE_SUCCESS&&ctrlRes!=null&&ctrlRes.Length>1)
        {
            for (int i = 0; i < ctrlRes.Length; i++)
            {
                sz_Doorctrl += GetInputItemHtml(CONSTHTML.radioButton, "ctrlsn", ctrlRes[i].szCtrlName.ToString(), ctrlRes[i].dwCtrlSN.ToString());
            }
        }
      
        if (IsPostBack)
        {

            ROOMREQ roomGet = new ROOMREQ();
            roomGet.szRoomNo = Request["id"];
            UNIROOM[] vtRoom;
            if (m_Request.Device.RoomGet(roomGet, out vtRoom) == REQUESTCODE.EXECUTE_SUCCESS && vtRoom != null && vtRoom.Length > 0)
            {
                string szType = Request["type"];
                string szLabid = Request["labid"];
                ROOMCTRLINFO devCtrl = new ROOMCTRLINFO();
                if (ctrlRes != null && ctrlRes.Length == 1)
                {
                    devCtrl.dwCtrlSN = ctrlRes[0].dwCtrlSN;
                }
                else
                {
                    devCtrl.dwCtrlSN = Parse(Request["ctrlsn"]);
                }
                devCtrl.dwRoomID = vtRoom[0].dwRoomID;// ctrlRes[0].dwRoomID;
                devCtrl.dwCmd = (uint)DEVCTRLINFO.DWCMD.DEVCMD_DOOROPEN;
                devCtrl.szParam = "4";
                uResponse = m_Request.Device.RoomCtrl(devCtrl, out devCtrl);
                if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    MessageBox("开门成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                }
                else
                {
                    MessageBox(m_Request.szErrMessage, "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                }
            }
            else { 
                 MessageBox("开门失败找不到对应房间", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
               return;
            }
            

        }
	}
}
