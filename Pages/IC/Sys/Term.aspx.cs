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
    protected string m_szOut = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        TERMREQ vrParameter = new TERMREQ();
       // vrParameter.dwGetType = (uint)TERMREQ.DWGETTYPE.TERMGET_BYALL;
        UNITERM[] vrResult;
        if (Request["delID"] != null)
        {

            DelTerm(Request["delID"]);
        }
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (m_Request.Reserve.GetTerm(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id=\"" + vrResult[i].dwYearTerm.ToString()+ "\">" + vrResult[i].szMemo + "</td>";
                m_szOut += "<td>" + GetDateStr(vrResult[i].dwBeginDate) + "到" + GetDateStr(vrResult[i].dwEndDate) + "</td>";
                
                m_szOut += "<td>" + vrResult[i].dwFirstWeekDays+ "</td>";
                m_szOut += "<td>" + vrResult[i].dwTotalWeeks+ "</td>";
                m_szOut += "<td>" + GetJustName(vrResult[i].dwStatus, "Term_Status") + "</td>";
                string szDivOPTD = "OPTD";
                if (((vrResult[i].dwStatus & (uint)UNITERM.DWSTATUS.TERMSTAT_OVER) > 0))
                {
                    szDivOPTD = "";
                }
                m_szOut += "<td><div class='" + szDivOPTD + "'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Reserve);
        }
        PutBackValue();        
    }
    private void DelTerm(string szID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        UNIRESVRULE delRule = new UNIRESVRULE();
        delRule.dwRuleSN = Parse(szID);
        uResponse=m_Request.Reserve.ResvRuleDel(delRule);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {

            MessageBox(m_Request.szErrMessage, "提示", MSGBOX.ERROR);
        }
    }
}
