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
    protected string m_szLab = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string szlab = Request["lab"];
        //=========================
        UNILAB[] lab = GetAllLab();
        if (lab != null && lab.Length > 0)
        {
            m_szLab += "<option value='0'>选择" + "空间类别" + "</option>";
            for (int i = 0; i < lab.Length; i++)
            {
                
                m_szLab += "<option value='" + lab[i].dwLabID + "'";
                if (szlab == lab[i].dwLabID.ToString())
                {
                    m_szLab += "checked='checked'";
                }
                m_szLab += ">" + lab[i].szLabName + "</option>";
            }
        }
        else
        {
            m_szLab += "<option value='0'>选择" + "空间类别" + "</option>";      
        }
        //=========================

        FULLROOMREQ vrParameter = new FULLROOMREQ();
        FULLROOM[] vrResult;
        if (szlab != null && szlab!="0")
        {
            vrParameter.dwLabID = Parse(szlab);
        }
        if (Request["delID"] != null)
        {
            DelRoom(Request["delID"]);
        }
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (m_Request.Device.FullRoomGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
              UpdatePageCtrl(m_Request.Device);
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td class=\"1\" data-id=" + vrResult[i].dwRoomID.ToString() + " data-ManGroupID=" + vrResult[i].dwManGroupID.ToString() + ">" + vrResult[i].szRoomNo + "</td>";
                m_szOut += "<td class='lnkRoom' data-id='" + vrResult[i].dwRoomID + "' title='查看房间信息'>" + vrResult[i].szRoomName + "</td>";
                m_szOut += "<td class='lnkLab' data-id='" + vrResult[i].dwLabID + "' title='查看实验室信息'>" + vrResult[i].szLabName + "</td>";
                m_szOut += "<td>" + vrResult[i].dwTotalDevNum.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwUsableDevNum.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szOpenRuleName + "</td>";
               
                string szRoomOpenInfo="";
                if ((uint)vrResult[i].dwStatus > 0)
                {
                    ROOMCTRLREQ roomctrlInfo = new ROOMCTRLREQ();
                    roomctrlInfo.dwDCSKind = (uint)UNIDCS.DWDCSKIND.DCSKIND_DOORCTRL;
                    roomctrlInfo.szRoomNo = vrResult[i].szRoomNo.ToString();

                    UNIDOORCTRL[] ctrlRes;

                    m_Request.Device.GetRoomCtrlInfo(roomctrlInfo, out ctrlRes);
                    if (ctrlRes != null && ctrlRes.Length > 0)
                    {
                        for (int j = 0; j < ctrlRes.Length; j++)
                        {
                            szRoomOpenInfo += ctrlRes[j].szCtrlName.ToString() + ":" + GetJustName(ctrlRes[j].dwStatus, "Unidcs_dwStatus")+",";
                        }
                    }
                }
               
                m_szOut += "<td title='" + szRoomOpenInfo + "'>" + GetJustName(vrResult[i].dwStatus, "Unidcs_dwStatus") + "</td>";

               

                m_szOut += "<td>" + GetJustName((uint)vrResult[i].dwManMode, "Room_dwManMode") + "</td>";
                m_szOut += "<td><div class='OPTD class2'></div></td>";
                m_szOut += "</tr>";
            }
          
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
