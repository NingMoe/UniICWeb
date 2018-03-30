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
        UNILAB[] lab = GetAllLab();
        if (lab != null && lab.Length > 0)
        {
            m_szLab += "<option value='0'>选择" + ConfigConst.GCLabName + "</option>";
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
        FULLROOMREQ vrParameter = new FULLROOMREQ();
        FULLROOM[] vrResult;            
        if (Request["lab"] != null&&Parse(Request["lab"])!=0)
        {
            vrParameter.dwLabID = Parse(Request["lab"]);
        }
        if (Request["delID"] != null)
        {
            DelRoom(Request["delID"]);
        }        
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (m_Request.Device.FullRoomGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-labid='" + vrResult[i].dwLabID.ToString()+ "' data-roomno='" + vrResult[i].szRoomNo.ToString() + "' data-roomName=" + vrResult[i].szRoomName.ToString() + " data-roomid=" + vrResult[i].dwRoomID.ToString() + " data-id=" + vrResult[i].dwRoomID.ToString() + ">" + vrResult[i].szRoomNo + "</td>";
                m_szOut += "<td class='lnkRoom' data-id='" + vrResult[i].dwRoomID + "' title='查看房间信息'>" + vrResult[i].szRoomName + "</td>";
                m_szOut += "<td class='lnkLab' data-id='" + vrResult[i].dwLabID + "' title='查看实验室信息'>" + vrResult[i].szLabName + "</td>";
                m_szOut += "<td>" + vrResult[i].dwTotalDevNum + "</td>";
                uint uStatus = (uint)vrResult[i].dwStatus;
                if (uStatus == 0)
                {
                    m_szOut += "<td>" + "未启用" + "</td>";
                }
                else {
                    m_szOut += "<td>" + GetJustName(uStatus, "Unidcs_dwStatus") + "</td>";
                }
                
                uint uManMode=(uint)vrResult[i].dwManMode;
               
                /*
                if (((uManMode&(uint)UNIROOM.DWMANMODE.ROOMMAN_DOORLOCK) > 0)&&((uManMode&(uint)UNIROOM.DWMANMODE.ROOMMAN_CAMERA) > 0))
                {
                m_szOut += "<td><div class='OPTD doorCar'></div></td>";
                }
                else if((uManMode&(uint)UNIROOM.DWMANMODE.ROOMMAN_DOORLOCK) > 0)
                {
                    m_szOut += "<td><div class='OPTD door'></div></td>";
                }
                else if ((uManMode & (uint)UNIROOM.DWMANMODE.ROOMMAN_CAMERA) > 0)
                {
                    m_szOut += "<td><div class='OPTD Car'></div></td>";
                }
                else {
                    m_szOut += "<td><div class='OPTD none'></div></td>";
                }
                
                if ((uManMode & (uint)UNIROOM.DWMANMODE.ROOMMAN_DOORLOCK) > 0)
                {
                    m_szOut += "<td><div class='OPTD doorCar'></div></td>";
                }
                else
                {
                    m_szOut += "<td><div class='OPTD none'></div></td>";
                }
                */
                m_szOut += "<td><div class='OPTD doorCar'></div></td>";
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
