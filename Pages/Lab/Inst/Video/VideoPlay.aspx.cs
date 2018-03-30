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

        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        DOORCTRLREQ doorGet = new DOORCTRLREQ();
        doorGet.dwGetType = (uint)DOORCTRLREQ.DWGETTYPE.DOORCTRLGET_BYALL;
        doorGet.szGetKey=Request["roomno"];
        doorGet.dwDCSKind = (uint)UNIDCS.DWDCSKIND.DCSKIND_VIDEOCTRL;
        UNIDOORCTRL[] vtDoor;
        uResponse = m_Request.DoorCtrlSrv.GetDoorCtrl(doorGet, out vtDoor);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtDoor != null && vtDoor.Length > 0)
        {
            for (int i = 0; i < vtDoor.Length; i++)
            {
                if (vtDoor[i].szRoomNo != null && vtDoor[i].szRoomNo == Request["roomno"])
                {
                    ip.Value = vtDoor[i].szDCSIP.ToString();
                    sn.Value = vtDoor[i].dwCtrlSN.ToString();
                }
            }
        }
    }
}