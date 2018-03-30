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

public partial class SupSys_DoorCtrl : UniPage
{
    protected MyString m_szOut = new MyString();

    protected void Page_Load(object sender, EventArgs e)
    {
        DOORCTRLREQ vrParameter = new DOORCTRLREQ();
        UNIDOORCTRL[] vrResult;
        vrParameter.dwGetType = (uint)DOORCTRLREQ.DWGETTYPE.DOORCTRLGET_BYALL;
        vrParameter.dwDCSKind = (uint)UNIDCS.DWDCSKIND.DCSKIND_DOORCTRL;
        string szID = Request["delID"];
        if (szID != null && szID != "")
        {
            del(szID);
        }
        if (m_Request.DoorCtrlSrv.GetDoorCtrl(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-roomno='" + vrResult[i].szRoomNo + "' data-id='" + vrResult[i].dwCtrlSN + "' data-sn='" + vrResult[i].dwDCSSN + "'>" + vrResult[i].dwCtrlSN + "</td>";
                m_szOut += "<td>" + vrResult[i].dwDCSSN + "</td>";                
                m_szOut += "<td>" + vrResult[i].szDCSName + "</td>";
                m_szOut += "<td class='lnkRoom' data-id='" + vrResult[i].szRoomNo + "'>" + vrResult[i].szRoomNo + "</td>";
                m_szOut += "<td class='lnkSta' data-id='" + vrResult[i].dwStaSN + "'>" + vrResult[i].szStaName + "</td>";
                m_szOut += "<td>" + vrResult[i].szMemo + "</td>";
                string szDivOPTD = "OPTD";
                m_szOut += "<td><div class='" + szDivOPTD + "'></div></td>";
                m_szOut += "</tr>";
            }
        }

        PutBackValue();
    }
    private void del(string szID)
    {
        UNIDOORCTRL del = new UNIDOORCTRL();
        del.dwDCSKind = (uint)UNIDCS.DWDCSKIND.DCSKIND_DOORCTRL;
        del.dwCtrlSN = Parse(szID);
        del.dwDCSSN = Parse(Request["delsn"]);
        m_Request.DoorCtrlSrv.DelDoorCtrl(del);
    }
    string GetSysName(uint dwSubSysSN)
    {
        if (dwSubSysSN == (uint)UNISTATION.DWSUBSYSSN.SUBSYS_LAB)
        {
            return "通用实验室";
        }
        else if (dwSubSysSN == (uint)UNISTATION.DWSUBSYSSN.SUBSYS_IC)
        {
            return "IC学习空间";
        }
        else if (dwSubSysSN == (uint)UNISTATION.DWSUBSYSSN.SUBSYS_OPENLAB)
        {
            return "开放实验室";
        }
        else if (dwSubSysSN == (uint)UNISTATION.DWSUBSYSSN.SUBSYS_TEACHINGLAB)
        {
            return "教学实验室";
        }
        return "";
    }
}
