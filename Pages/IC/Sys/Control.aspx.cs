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
        CONREQ vrParameter = new CONREQ();
        
        UNICONSOLE[] vrResult;
        
        if (Request["delID"] != null)
        {

            DelTerm(Request["delID"]);
        }
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (m_Request.Console.ConGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id=\"" + vrResult[i].dwConsoleSN.ToString() + "\">" + vrResult[i].dwConsoleSN + "</td>";
                m_szOut += "<td data-id=\"" + vrResult[i].dwConsoleSN.ToString() + "\">" + vrResult[i].szConsoleName + "</td>";
                m_szOut += "<td>" +(vrResult[i].szIP) + "</td>";
                m_szOut += "<td>" + GetTimeStr(vrResult[i].dwOpenTime) + "到" + GetTimeStr(vrResult[i].dwCloseTime) + "</td>";
                string szKindName = GetJustName(vrResult[i].dwKind, "Console_Kind_Total");
                if ((((uint)vrResult[i].dwKind) & (uint)UNICONSOLE.DWKIND.CONKIND_WITHAG) > 0)
                {
                    szKindName="通道机控制预约柜";
                }
                m_szOut += "<td>" +szKindName  + "</td>";
                m_szOut += "<td>" + GetJustName(vrResult[i].dwKind, "Console_Kind_Detail") + "</td>";
                m_szOut += "<td>" + GetJustName(vrResult[i].dwStatus, "Console_Status") + "</td>";
                string szDivOPTD = "OPTD";               
                m_szOut += "<td><div class='" + szDivOPTD + "'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Console);
        }
        PutBackValue();        
    }
    private void DelTerm(string szID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        UNICONSOLE delRule = new UNICONSOLE();
        delRule.dwConsoleSN = Parse(szID);
        uResponse=m_Request.Console.ConDel(delRule);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {

            MessageBox(m_Request.szErrMessage, "提示", MSGBOX.ERROR);
        }
    }
}
