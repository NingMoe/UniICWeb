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
        Session["GroupOpenRuleList"] = null;
        DEVOPENRULEREQ vrParameter = new DEVOPENRULEREQ();
        DEVOPENRULE[] vrResult;
        if (Request["delID"] != null)
        {

            DelOpenRule(Request["delID"]);
        }
        if (m_Request.Device.DevOpenRuleGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id=\"" + vrResult[i].dwRuleSN + "\">" + vrResult[i].szRuleName + "</td>";
                m_szOut += "<td>" + vrResult[i].szMemo + "</td>";
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
            //UpdatePageCtrl(m_Request.Device);
        }
        PutBackValue();        
    }
    private void DelOpenRule(string szID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        DEVOPENRULE delRule = new DEVOPENRULE();
        delRule.dwRuleSN = Parse(szID);
        uResponse=m_Request.Device.DevOpenRuleDel(delRule);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {

            MessageBox(m_Request.szErrMessage, "提示", MSGBOX.ERROR);
        }
    }
}
