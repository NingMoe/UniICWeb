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
        USERFEEDBACKREQ vrPar = new USERFEEDBACKREQ();
        GetHTTPObj(out vrPar);
         if (!IsPostBack)
        {
            vrPar.dwBeginDate = GetDate(DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"));
            vrPar.dwEndDate = GetDate(DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"));

            dwStartDate.Value = DateTime.Now.AddDays(0).ToString("yyyy-MM-dd");
            dwEndDate.Value = DateTime.Now.AddDays(0).ToString("yyyy-MM-dd");
        }
        if (dwStartDate.Value != "" && dwEndDate.Value != "")
        {
            vrPar.dwBeginDate = GetDate(dwStartDate.Value);
            vrPar.dwEndDate = GetDate(dwEndDate.Value);
        }
        if (vrPar.dwFeedStat == null || ((uint)vrPar.dwFeedStat) == 0)
        {
            vrPar.dwFeedStat = null;// (uint)USERFEEDBACK.DWFEEDSTAT.FEEDSTAT_REPLIED;
        }
        string[] szCon={"","★","★★","★★★","★★★★","★★★★★"};
        USERFEEDBACK[] vtRes;
        uResponse = m_Request.Admin.GetUserFeedback(vrPar,out vtRes);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            for (int i = 0; i < vtRes.Length; i++)
            {
                m_szOut += "<tr>";

                m_szOut += "<td data-id=" + vtRes[i].dwSNum.ToString() + ">" + vtRes[i].szTrueName + "(" + vtRes[i].szUserDeptName + "," + vtRes[i].szHandPhone + ",)" + "</td>";
                m_szOut += "<td>" +Get1970Date(vtRes[i].dwOccurTime) + "</td>";
                string szDetail=vtRes[i].szIntroInfo;
                if(szDetail.Length>10)
                {
                    szDetail=szDetail.Substring(0,10)+"...";
                }
                m_szOut += "<td title='"+vtRes[i].szIntroInfo+"'>" +szDetail + "</td>";
                m_szOut += "<td>" + GetJustNameEqual(vtRes[i].dwFeedStat,"dwFeedStat") + "</td>";
                string szReplyInfo = vtRes[i].szReplyInfo;
                if (szReplyInfo.Length > 10)
                {
                    szReplyInfo = szReplyInfo.Substring(0, 10) + "...";
                }
                m_szOut += "<td title='" + vtRes[i].szReplyInfo + "'>" + szReplyInfo + "</td>";
                string szOp = "";
                if (((uint)vtRes[i].dwFeedStat & ((uint)USERFEEDBACK.DWFEEDSTAT.FEEDSTAT_WAITREPLY)) > 0)
                {
                    szOp = "OPTD";
                }
                else
                {
                 
                }
                m_szOut += "<td><div class='" + szOp + "'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Admin);
        }
        PutBackValue();
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreLoad(e);
     
    }   
}
