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
    public string szStation = "";
    public string szTitle = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["delID"] != null)
        {
            DelDCS(Request["delID"], Request["dwStaSN"]);
        }

        DCSREQ vrParameter = new DCSREQ();
        UNIDCS[] vrResult;      
        string type = Request["dcsKind"];
        if (type == "1")
        {
             vrParameter.dwDCSKind = (uint)UNIDCS.DWDCSKIND.DCSKIND_DOORCTRL;
             szTitle = "门禁集控器";
        }
        else if (type == "2")
        {
            vrParameter.dwDCSKind = (uint)UNIDCS.DWDCSKIND.DCSKIND_VIDEOCTRL;
            szTitle = "摄像机";
        }        
        vrParameter.dwGetType = (uint)DCSREQ.DWGETTYPE.DCSGET_BYALL;       
        if (m_Request.DoorCtrlSrv.GetDCS(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
           // GetAdvOpts("id", "name", vrResult);
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td dwStaSN=\""+vrResult[i].dwStaSN+"\">" + vrResult[i].dwSN + "</td>";
                m_szOut += "<td>" + vrResult[i].szName + "</td>";               
                m_szOut += "<td class='lnkSta' data-id='" + vrResult[i].dwStaSN + "'>" + vrResult[i].szStaName + "</td>";
                m_szOut += "<td>" + "" + "</td>";
                m_szOut += "<td>" + vrResult[i].szMemo + "</td>";
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
        }

        PutBackValue();
    }
    public REQUESTCODE DelDCS(string szID, string dwStaSN)
    {
        REQUESTCODE uResopnese=REQUESTCODE.EXECUTE_FAIL;
        UNIDCS sta = new UNIDCS();
        sta.dwStaSN = Parse(dwStaSN);
        sta.dwSN = Parse(szID);
        uResopnese = m_Request.DoorCtrlSrv.DelDCS(sta);
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
