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
using System.IO;
using System.Text;
public partial class Sub_Course : UniPage
{
    protected MyString m_szOut = new MyString();
    protected string m_TermList = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        {
            REPORTREQ vrParameter = new REPORTREQ();
            USERSTAT[] vrResult;
            GetPageCtrlValue(out vrParameter.szReqExtInfo);
            if (!IsPostBack)
            {
                dwStartDate.Value = DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd");
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
            string szKey = Request["orderkey"];
            if (szKey == null)
            {
                vrParameter.szReqExtInfo.szOrderKey = "dwUseTime";
                vrParameter.szReqExtInfo.szOrderMode = "desc";
            }
            else {
                vrParameter.szReqExtInfo.szOrderKey = szKey;
                vrParameter.szReqExtInfo.szOrderMode = "desc";
            }
            uint dwClassKind = Parse(Request["dwClassKind"]);
            if (dwClassKind != 0)
            {
                vrParameter.dwClassKind = dwClassKind;
            }
            if (m_Request.Report.GetUserStat(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                UpdatePageCtrl(m_Request.Report);
                for (int i = 0; i < vrResult.Length; i++)
                {
                    m_szOut += "<tr>";
                    m_szOut += "<td data-id=" + vrResult[i].dwAccNo.ToString() + ">" + vrResult[i].szPID + "</td>";
                    m_szOut += "<td>" + vrResult[i].szTrueName + "</td>";
                    UNIACCOUNT accinfo;
                    if (GetAccByAccno(vrResult[i].dwAccNo.ToString(), out accinfo))
                    {
                        m_szOut += "<td>" + accinfo.szClassName.ToString() + "</td>";
                    }
                    else
                    {
                        m_szOut += "<td></td>";
                    }
                    m_szOut += "<td>" + vrResult[i].szDeptName.ToString() + "</td>";
                    m_szOut += "<td>" + vrResult[i].dwUseTimes.ToString() + "</td>";
                    uint uUseTime = (uint)vrResult[i].dwUseTime;
                    m_szOut += "<td>" + uUseTime / 60 + "小时" + uUseTime % 60 + "分钟" + "</td>";
                    m_szOut += "</tr>";
                }
              
            }
        }
      
        PutBackValue();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {

       
    }
   
}
