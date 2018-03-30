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
        TESTCARDREQ vrParameter = new TESTCARDREQ();

        TESTCARD[] vrResult;
        if (Request["delID"] != null)
        {
            Del(Request["delID"]);
        }
        GetPageCtrlValue(out vrParameter.szReqExtInfo);

        if (m_Request.Reserve.GetTestCard(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id=\"" + vrResult[i].dwTestCardID+ "\">" + vrResult[i].szTestName + "</td>";
                m_szOut += "<td>" + vrResult[i].szCategoryName + "</td>";
                m_szOut += "<td>" + vrResult[i].dwGroupPeopleNum + "</td>";
                m_szOut += "<td>" +vrResult[i].dwTestHour + "</td>";
                m_szOut += "<td>" + vrResult[i].szMemo + "</td>";
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Reserve);
        }
        PutBackValue();        
    }
    private void Del(string szID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        TESTCARD delRule = new TESTCARD();
        delRule.dwTestCardID = Parse(szID);
        uResponse=m_Request.Reserve.DelTestCard(delRule);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
            MessageBox(m_Request.szErrMessage, "提示", MSGBOX.ERROR);
        }
    }
}
