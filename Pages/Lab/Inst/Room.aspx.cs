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
        ROOMREQ vrParameter = new ROOMREQ();
        UNIROOM[] vrResult;      
        vrParameter.szReqExtInfo.dwStartLine = 0;
        vrParameter.szReqExtInfo.dwNeedLines = 2;
        if (Request["lab"] != null)
        {
            vrParameter.dwLabID = Parse(Request["lab"]);
        }
        if (Request["delID"] != null)
        {
            DelRoom(Request["delID"]);
        }
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (m_Request.Device.RoomGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id="+vrResult[i].dwRoomID.ToString()+">" + vrResult[i].szRoomNo + "</td>";
                m_szOut += "<td class='lnkRoom' data-id='" + vrResult[i].dwRoomID + "' title='查看房间信息'>" + vrResult[i].szRoomName + "</td>";
                m_szOut += "<td class='lnkLab' data-id='" + vrResult[i].dwLabID + "' title='查看实验室信息'>" + vrResult[i].szLabName + "</td>";
                m_szOut += "<td>" + 0 + "</td>";
                m_szOut += "<td>" + 0 + "</td>";               
                m_szOut += "<td>" + "openrule"+ "</td>";
                m_szOut += "<td>" + 0 + "</td>";
                m_szOut += "<td>" + GetJustName((uint)vrResult[i].dwManMode, "Room_dwManMode") + "</td>";
                m_szOut += "<td><div class='OPTD class2'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Device);
        }

        PutBackValue();
    }
    private void DelRoom(string szID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        UNIROOM room = new UNIROOM();
        room.dwRoomID = Parse(szID);
        uResponse = m_Request.Device.RoomDel(room);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {

            MessageBox(m_Request.szErrMessage, "提示", MSGBOX.ERROR);
        }
    }
}
