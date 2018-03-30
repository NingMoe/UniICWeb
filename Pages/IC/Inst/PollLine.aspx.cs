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

public partial class Sub_Course : UniPage
{
    protected MyString m_szOut = new MyString();

    protected void Page_Load(object sender, EventArgs e)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        POLLONLINEREQ vrPar = new POLLONLINEREQ();
        GetHTTPObj(out vrPar);
         if (!IsPostBack)
        {
            vrPar.dwBeginDate = GetDate(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"));
            vrPar.dwEndDate = GetDate(DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd"));

            dwStartDate.Value = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            dwEndDate.Value = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd");
        }
        if (dwStartDate.Value != "" && dwEndDate.Value != "")
        {
            vrPar.dwBeginDate = GetDate(dwStartDate.Value);
            vrPar.dwEndDate = GetDate(dwEndDate.Value);
        }
        if (vrPar.dwVoteStat == null || ((uint)vrPar.dwVoteStat) == 0)
        {
            vrPar.dwVoteStat = null;// (uint)USERFEEDBACK.DWFEEDSTAT.FEEDSTAT_REPLIED;
        }
        string[] szCon={"","★","★★","★★★","★★★★","★★★★★"};
        POLLONLINE[] vtRes;
        if (vrPar.dwVoteStat == 0)
        {
            vrPar.dwVoteStat = null;
        }
        uResponse = m_Request.Admin.GetPollOnLine(vrPar,out vtRes);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            for (int i = 0; i < vtRes.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id=" + vtRes[i].dwPollID.ToString() + ">" + vtRes[i].szPollSubject + "</td>";
                m_szOut += "<td>" +GetDateStr(vtRes[i].dwBeginDate)+"到"+ GetDateStr(vtRes[i].dwEndDate) + "</td>";
                m_szOut += "<td>" + vtRes[i].dwTotalUsers + "</td>";
                m_szOut += "<td>" +GetJustName(vtRes[i].dwVoteStat,"PollLine_State") + "</td>";
                m_szOut += "<td>" + "<a class='getPollInfo' id="+vtRes[i].dwPollID.ToString()+" >查看投票信息</a>" + "</td>";
                string szOp = "OPTD";
                m_szOut += "<td><div class='" + szOp + "'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Admin);
        }
        PutBackValue();
    }
    protected void Del(string delID)
    {
 
    }   
}
