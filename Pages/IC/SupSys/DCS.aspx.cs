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
        DCSREQ vrParameter = new DCSREQ();
        UNIDCS[] vrResult;
        vrParameter.dwGetType = (uint)DCSREQ.DWGETTYPE.DCSGET_BYALL;
        vrParameter.dwDCSKind = (uint)UNIDCS.DWDCSKIND.DCSKIND_DOORCTRL;

        if (m_Request.DoorCtrlSrv.GetDCS(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td>" + vrResult[i].dwSN + "</td>";
                m_szOut += "<td>" + vrResult[i].szName + "</td>";
                m_szOut += "<td class='lnkSta' data-id='" + vrResult[i].dwStaSN + "'>" + vrResult[i].szStaName + "</td>";
                m_szOut += "<td>" + vrResult[i].szStatInfo + "</td>";
                m_szOut += "<td>" + vrResult[i].szMemo + "</td>";
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
        }

        PutBackValue();
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
