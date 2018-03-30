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

public partial class Sub_Device : UniPage
{
    protected string m_szOpts = "";
    protected MyString m_szOut = new MyString();

    protected string m_szLab = "";
    protected string m_szRoom = "";
    protected string m_szCamp = "";
    protected string m_szBuilding = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string szlab = Request["lab"];
        string szroom = null;// Request["room"];
        UNICAMPUS[] camp = GetAllCampus();
        if (camp != null && camp.Length > 0)
        {

            m_szCamp += "<option value='0'>" + "全部" + "</option>";
            for (int i = 0; i < camp.Length; i++)
            {
                m_szCamp += "<option value='" + camp[i].dwCampusID + "'";
                m_szCamp += ">" + camp[i].szCampusName + "</option>";
            }
        }
        m_szBuilding = "";
        UNIBUILDING[] buliding = getAllBuilding();
        if (buliding != null && buliding.Length > 0)
        {

            m_szBuilding += "<option value='0'>" + "全部" + "</option>";
            for (int i = 0; i < buliding.Length; i++)
            {
                m_szBuilding += "<option value='" + buliding[i].dwBuildingID + "'";
                m_szBuilding += ">" + buliding[i].szBuildingName + "</option>";
            }
        }

        //=========================
        UNILAB[] lab = GetAllLab();
        if (lab != null && lab.Length > 0)
        {

            m_szLab += "<option value='0'>" + "全部" + "</option>";
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

        ROOMREQ reqRoom = new ROOMREQ();
        UNIROOM[] room;
        m_Request.Device.RoomGet(reqRoom, out room);
        if (room != null && room.Length > 0)
        {

            {
                string szorgroom = szroom;
                szroom = room[0].dwRoomID.ToString();
                for (int i = 0; i < room.Length; i++)
                {
                    if (szorgroom == room[i].dwRoomID.ToString())
                    {
                        szroom = szorgroom;
                        break;
                    }
                }
            }

            for (int i = 0; i < room.Length; i++)
            {
                m_szRoom += "<option value='" + room[i].dwRoomID + "'";
                if (szroom == room[i].dwRoomID.ToString())
                {
                    m_szRoom += "checked='checked'";
                }
                m_szRoom += ">" + room[i].szRoomName + "</option>";
            }
        }
        //=========================


        DEVREQ vrParameter = new DEVREQ();
        GetHTTPObj(out  vrParameter);
        if (vrParameter.szBuildingIDs != null && vrParameter.szBuildingIDs.ToString() == "0")
        {
            vrParameter.szBuildingIDs = null;
        }
        if (vrParameter.szCampusIDs != null && vrParameter.szCampusIDs.ToString() == "0")
        {
            vrParameter.szCampusIDs = null;
        }
        if (szroom != null)
        {
            m_szOpts += "机房号：" + szroom;
            //vrParameter.szRoomIDs = (szroom);
        }
        if (szlab != null)
        {
            m_szOpts += "实验室：" + szlab;
            vrParameter.szLabIDs = (szlab);
        }
        if (vrParameter.szLabIDs == "0")
        {
            vrParameter.szLabIDs = null;
        }
        if (vrParameter.szRoomIDs == "")
        {
            vrParameter.szRoomIDs = null;
        }
        if (Request["delID"] != null)
        {
            Del(Request["delID"], Request["delParentID"]);
        }

        UNIDEVICE[] vrResult;
        //vrParameter.dwProperty = (uint)(UNIDEVKIND.DWPROPERTY.DEVPROP_EXCLUSIVE | UNIDEVKIND.DWPROPERTY.DEVPROP_SHARE);
        GetPageCtrlValue(out vrParameter.szReqExtInfo);

        if (m_Request.Device.Get(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            UpdatePageCtrl(m_Request.Device);
            for (int i = 0; i < vrResult.Length; i++)
            {
                UNIROOM getRoom;
                uint uRoomManGroupID=0;
                if(GetRoomID(vrResult[i].dwRoomID.ToString(),out getRoom))
                {
                    uRoomManGroupID = (uint)getRoom.dwManGroupID;
                }
                m_szOut += "<tr>";
                m_szOut += "<td data-openid='" + vrResult[i].dwOpenRuleSN + "' data-id='" + vrResult[i].dwDevID.ToString() + "' data-ManGroupID='" + uRoomManGroupID.ToString() + "'>" + vrResult[i].szAssertSN + "</td>";
                m_szOut += "<td>" + vrResult[i].szDevName.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szRoomNo + "</td>";
                m_szOut += "<td>" + vrResult[i].dwMaxUsers + "</td>";
                m_szOut += "<td>" + vrResult[i].szBuildingName + "</td>";
                m_szOut += "<td>" + vrResult[i].szCampusName + "</td>";
                m_szOut += "<td>" + vrResult[i].szClassName + "</td>";
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
          
        }
        PutBackValue();
    }
    private void Del(string szID, string szLabID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        UNIDEVICE obj = new UNIDEVICE();
        obj.dwDevID = Parse(szID);
        obj.dwLabID = Parse(szLabID);
        uResponse = m_Request.Device.Del(obj);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
            MessageBox(m_Request.szErrMessage, "提示", MSGBOX.ERROR);
        }
    }
}
