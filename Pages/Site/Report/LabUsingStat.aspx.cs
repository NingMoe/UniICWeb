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
    protected string m_TermList = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        REPORTREQ vrParameter = new REPORTREQ();
        LABSTAT[] vrResult;
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (!IsPostBack)
        {
            dwStartDate.Value = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
            dwEndDate.Value = DateTime.Now.ToString("yyyy-MM-dd");

        }

        UNITERM[] termList = GetAllTerm();
        string szYearTerm = Request["dwYearTerm"];
        uint uYeartermNow = Parse(szYearTerm);
        if (termList != null)
        {
            m_TermList += GetInputItemHtml(CONSTHTML.option, "", "选择学期", "0");
            for (int i = 0; i < termList.Length; i++)
            {
                m_TermList += GetInputItemHtml(CONSTHTML.option, "", termList[i].szMemo.ToString(), termList[i].dwYearTerm.ToString());
                uint uYearTermState = (uint)termList[i].dwStatus;
                if (szYearTerm == null && (uYearTermState & (uint)UNITERM.DWSTATUS.TERMSTAT_FORCE) > 0)
                {
                    uYeartermNow = (uint)termList[i].dwYearTerm;
                }
            }
        }
        vrParameter.dwStartDate = DateToUint(dwStartDate.Value);
        vrParameter.dwEndDate = DateToUint(dwEndDate.Value);
        if (ConfigConst.GCICTypeMode == 1)
        {
            UNITERM[] termnow = GetTermByID(Parse(Request["dwYearTerm"]));
            if (termnow != null && termnow.Length > 0)
            {
                vrParameter.dwStartDate = termnow[0].dwBeginDate;
                vrParameter.dwEndDate = termnow[0].dwEndDate;
            }
            else
            {
                vrParameter.dwStartDate = null;
                vrParameter.dwEndDate = null;

            }
        }   
        vrParameter.dwPurpose = ((uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL + (uint)UNIRESERVE.DWPURPOSE.USEFOR_RESEACH + (uint)UNIRESERVE.DWPURPOSE.USEFOR_TEACHING);
        if (vrParameter.szReqExtInfo.szOrderKey == null || vrParameter.szReqExtInfo.szOrderKey == "")
        {
            vrParameter.szReqExtInfo.szOrderKey = "dwTotalUseTime";
            vrParameter.szReqExtInfo.szOrderMode = "desc";
        }

        if (m_Request.Report.GetLabStat(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id=" + vrResult[i].dwLabID.ToString() + ">" + vrResult[i].szLabSN + "</td>";
                m_szOut += "<td>" + vrResult[i].szLabName + "</td>";
                m_szOut += "<td>" + vrResult[i].dwPIDNum.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwTotalNum.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwUseTimes.ToString() + "</td>";
                uint uUseTime = (uint)vrResult[i].dwTotalUseTime;
                m_szOut += "<td>" + uUseTime / 60 + "小时" + uUseTime % 60 + "分钟" + "</td>";
                double df = (double)(vrResult[i].dwTotalUseTime /(vrResult[i].dwTotalNum*1.0));
                m_szOut += "<td>" + df.ToString(".00") + "</td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Report);
        }

        PutBackValue();
    }
}
