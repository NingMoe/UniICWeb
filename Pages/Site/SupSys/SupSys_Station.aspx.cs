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

public partial class Sub_Station : UniPage
{
    protected MyString m_szOut = new MyString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["delID"] != null)
        {
            DelStation(Request["delID"]);
        }

        STATIONREQ vrParameter = new STATIONREQ();
        UNISTATION[] vrResult;
        vrParameter.dwGetType = (uint)STATIONREQ.DWGETTYPE.STATIONGET_BYALL;

        if (m_Request.Station.GetStation(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td>" + vrResult[i].dwStaSN + "</td>";
                m_szOut += "<td>" + vrResult[i].szStaName + "</td>";
                m_szOut += "<td>" + GetSysName((uint)vrResult[i].dwSubSysSN) + "</td>";
                m_szOut += "<td>" + vrResult[i].szDeptName + "</td>";
                m_szOut += "<td class='lnkUser' data-id='" + vrResult[i].dwManagerID + "'>" + vrResult[i].szManName + "</td>";
                m_szOut += "<td class='lnkUser' data-id='" + vrResult[i].dwAttendantID + "'>" + vrResult[i].szAttendantName + "</td>";
                m_szOut += "<td>" + vrResult[i].dwStatus + "</td>";
                m_szOut += "<td>" + vrResult[i].szMemo + "</td>";
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Station);
        }

        PutBackValue();
    }
    public REQUESTCODE DelStation(string szID)
    {
        REQUESTCODE uResopnese;
        UNISTATION sta = new UNISTATION();
        sta.dwStaSN = Parse(szID);
        uResopnese = m_Request.Station.DelStation(sta);
        return uResopnese;
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
