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

public partial class Sub_Room : UniPage
{
    protected MyString m_szOut = new MyString();
    protected string szStatus = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["delID"] != null)
        {
            DelDevCls(Request["delID"]);
        }
        RESEARCHTESTREQ vrParameter = new RESEARCHTESTREQ();
        RESEARCHTEST[] vrResult;
        szStatus += GetInputItemHtml(CONSTHTML.option, "", "全部", "0");
        szStatus += GetInputHtmlFromXml(0, CONSTHTML.option, "", "ResearchTest_Level", true);
        GetHTTPObj(out vrParameter);
        if (vrParameter.dwRTLevel != null && vrParameter.dwRTLevel == 0)
        {
            vrParameter.dwRTLevel = null;
        }
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (m_Request.Reserve.GetResearchTest(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-groupid='" + vrResult[i].dwGroupID.ToString()+ "' data-id=" + vrResult[i].dwRTID.ToString() + ">" + vrResult[i].szRTSN + "</td>";
                m_szOut += "<td>" + vrResult[i].szRTName + "</td>";
                m_szOut += "<td>" + vrResult[i].szLeaderName + "</td>";
                m_szOut += "<td>" + vrResult[i].szLeaderName + "</td>";
                m_szOut += "<td>" + vrResult[i].szDeptName + "</td>";
                m_szOut += "<td>" +GetDateStr(vrResult[i].dwBeginDate) + "</td>";
                m_szOut += "<td>" + vrResult[i].szFromUnit + "</td>";
                m_szOut += "<td>" + GetJustNameEqual(vrResult[i].dwRTLevel, "ResearchTest_Level") + "</td>";   
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Reserve);
        }

        PutBackValue();
    }
    private void DelDevCls(string szID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        RESEARCHTEST vrGet = new RESEARCHTEST();
        vrGet.dwRTID = Parse(szID);
        uResponse = m_Request.Reserve.DelResearchTest(vrGet);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {

            MessageBox(m_Request.szErrMessage, "提示", MSGBOX.ERROR);
        }
    }
}
