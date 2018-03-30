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
    protected string m_szOutDCS = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        DCSREQ vrGet = new DCSREQ();
        UNIDCS[] vtRes;
        vrGet.dwDCSKind=(uint)UNIDCS.DWDCSKIND.DCSKIND_DOORCTRL;
        if (m_Request.DoorCtrlSrv.GetDCS(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            m_szOutDCS += GetInputItemHtml(CONSTHTML.option, "", "全部", "0");
            for (int i = 0; i < vtRes.Length; i++)
            {
                m_szOutDCS += GetInputItemHtml(CONSTHTML.option, "", vtRes[i].szName.ToString(), vtRes[i].dwSN.ToString());
            }
        }
        uint dwDCSSN =Parse(Request["dwDCSSN"]);
        
        DOORCTRLREQ vrParameter = new DOORCTRLREQ();
        if (dwDCSSN != 0)
        {
            vrParameter.dwGetType = (uint)DCSREQ.DWGETTYPE.DCSGET_BYSN;
            vrParameter.szGetKey = dwDCSSN.ToString();
        }
        else {
            vrParameter.dwGetType = (uint)DOORCTRLREQ.DWGETTYPE.DOORCTRLGET_BYALL;
        }
        UNIDOORCTRL[] vrResult;
        
        vrParameter.dwDCSKind = (uint)UNIDCS.DWDCSKIND.DCSKIND_DOORCTRL;
        if (m_Request.DoorCtrlSrv.GetDoorCtrl(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td>" + vrResult[i].dwCtrlSN + "</td>";
                m_szOut += "<td>" + vrResult[i].dwDCSSN + "</td>";                
                m_szOut += "<td>" + vrResult[i].szDCSName + "</td>";
                m_szOut += "<td class='lnkRoom' data-id='" + vrResult[i].szRoomNo + "'>" + vrResult[i].szRoomNo + "</td>";
                m_szOut += "<td class='lnkSta' data-id='" + vrResult[i].dwStaSN + "'>" + vrResult[i].szStaName + "</td>";
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
