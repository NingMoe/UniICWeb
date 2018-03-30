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
        if (IsPostBack)
        {
            string szRoomIDs = Request["id"];
            string[] szRoomIDList = szRoomIDs.Split(',');
            uint uCount = 0;
            for (int i = 0; i < szRoomIDList.Length; i++)
            {
                if (szRoomIDList[i] != null && szRoomIDList[i] != "")
                {
                    UNIROOM setRoom = new UNIROOM();
                    if (GetRoomID(szRoomIDList[i], out setRoom))
                    {
                        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
                        ROOMCTRLREQ roomctrlInfo = new ROOMCTRLREQ();
                        roomctrlInfo.dwDCSKind = (uint)UNIDCS.DWDCSKIND.DCSKIND_DOORCTRL;
                        roomctrlInfo.szRoomNo = setRoom.szRoomNo;
                        UNIDOORCTRL[] ctrlRes;
                        uResponse = m_Request.Device.GetRoomCtrlInfo(roomctrlInfo, out ctrlRes);
                        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && ctrlRes != null && ctrlRes.Length > 0)
                        {
                            ROOMCTRLINFO devCtrl = new ROOMCTRLINFO();
                            if (ctrlRes != null && ctrlRes.Length == 1)
                            {
                                devCtrl.dwCtrlSN = ctrlRes[0].dwCtrlSN;
                            }
                            else
                            {
                                devCtrl.dwCtrlSN = Parse(Request["ctrlsn"]);
                            }
                            devCtrl.dwRoomID = setRoom.dwRoomID;// ctrlRes[0].dwRoomID;
                            devCtrl.dwCmd = (uint)DEVCTRLINFO.DWCMD.DEVCMD_DOOROPEN;
                            devCtrl.szParam = "4";
                            uResponse = m_Request.Device.RoomCtrl(devCtrl, out devCtrl);
                            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
                            {
                                uCount = uCount + 1;
                            }
                        }
                    }

                }
            }
            if (uCount > 0)
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
