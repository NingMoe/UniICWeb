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

public partial class Sub_Room : UniPage
{
    protected MyString m_szOut = new MyString();

    protected void Page_Load(object sender, EventArgs e)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        ROOMREQ vrGet = new ROOMREQ();      
        UNIROOM[] vtRes;
        uResponse = m_Request.Device.RoomGet(vrGet, out vtRes);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            /*
            for (int i = 0; i < vtRes.Length; i++)
            {
                if (!(((uint)vtRes[i].dwManMode & (uint)UNIROOM.DWMANMODE.ROOMMAN_CAMERA) > 0))
                {
                    continue;
                }
                 UNIDOORCTRL doorCtrl;
                 if (!GetDoorCtrl(vtRes[i].szRoomNo, out doorCtrl))
                 {
                     continue;
                 }
                string szID = vtRes[i].dwRoomID.ToString();
                string szName = vtRes[i].szRoomName.ToString();

                m_szOut += "<input data-ctrlsn=\"" + doorCtrl.dwCtrlSN.ToString() + "\" data-ip=\"" + doorCtrl.szDCSIP.ToString() + "\" class=\"enum\" id=\"" + szID + "\" type=\"radio\" name=\"" + "szRoom" + "\" value=\"" + szID + "\" /> <label for=\"" + szID + "\">" + szName + "</label>";
            }
             */
        }

        DOORCTRLREQ doorGet = new DOORCTRLREQ();
        doorGet.dwGetType = (uint)DOORCTRLREQ.DWGETTYPE.DOORCTRLGET_BYALL;
        doorGet.dwDCSKind = (uint)UNIDCS.DWDCSKIND.DCSKIND_VIDEOCTRL;
        UNIDOORCTRL[] vtDoor;
        uResponse = m_Request.DoorCtrlSrv.GetDoorCtrl(doorGet, out vtDoor);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtDoor != null && vtDoor.Length > 0)
        {
            for (int i = 0; i < vtDoor.Length; i++)
            {
                UNIDOORCTRL doorCtrl = vtDoor[i];
                string szID = vtDoor[i].dwRoomID.ToString();
                if (szID == "0")
                {
                    szID = "roomid" + i.ToString();
                }
                string szName = vtDoor[i].szCtrlModel.ToString();

                m_szOut += "<input data-ctrlsn=\"" + doorCtrl.dwCtrlSN.ToString() + "\" data-ip=\"" + doorCtrl.szDCSIP.ToString() + "\" class=\"enum\" id=\"" + szID + "\" type=\"radio\" name=\"" + "szRoom" + "\" value=\"" + szID + "\" /> <label for=\"" + szID + "\">" + szName + "</label>";
            }
        }
    }
    private bool GetDoorCtrl(string szRoomNo,out UNIDOORCTRL[] doorCtrl)
    {
        //-todo
         REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        ROOMCTRLREQ roomGet = new ROOMCTRLREQ();
        roomGet.dwDCSKind = (uint)UNIDCS.DWDCSKIND.DCSKIND_VIDEOCTRL;
        roomGet.szRoomNo = szRoomNo;
        uResponse = m_Request.Device.GetRoomCtrlInfo(roomGet, out doorCtrl);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            return true;
        }
        return false;
    }
}
   
