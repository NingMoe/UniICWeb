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

public partial class SupSys_DCS : UniPage
{
    protected MyString m_szOut = new MyString();

    protected void Page_Load(object sender, EventArgs e)
    {
        DEVMONITORREQ vrParameter = new DEVMONITORREQ();
        DEVMONITOR[] vrResult;
        string szID = Request["delID"];
        if (szID != null && szID != "")
        {
            del(szID);
        }
        if (m_Request.Device.DevMonitorGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id='"+vrResult[i].dwMonitorID+"'>" + vrResult[i].szMonitorName + "</td>";
                m_szOut += "<td>" + vrResult[i].szIP + "</td>";
                m_szOut += "<td>" + vrResult[i].dwPort + "</td>";
                if ((uint)vrResult[i].dwMonitorType == 1)
                {
                    m_szOut += "<td>" + "摩托FX7400" + "</td>";
                }
                else if ((uint)vrResult[i].dwMonitorType == 2)
                {
                    m_szOut += "<td>" + "营信YXU2881" + "</td>";
                }
                m_szOut += "<td>" + vrResult[i].szMemo + "</td>";
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
        }

        PutBackValue();
    }
    private void del(string szID)
    {
        DEVMONITOR del = new DEVMONITOR();
     
        del.dwMonitorID = Parse(szID);
        m_Request.Device.DevMonitorDel(del);
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
