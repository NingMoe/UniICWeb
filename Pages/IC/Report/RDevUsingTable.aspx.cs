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
    protected string szResvRate = "";
    protected string m_TermList = "";
    protected string szUsiingRate = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        DEVUSINGRATEREQ vrParameter = new DEVUSINGRATEREQ();

        DEVUSINGRATE vrResultValue;
      
       
        if (!IsPostBack)
        {
            dwStartDate.Value = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            dwEndDate.Value = DateTime.Now.ToString("yyyy-MM-dd");           
        }
      
        if (dwStartDate.Value != "" && dwEndDate.Value != "")
        {
            vrParameter.dwStartDate = DateToUint(dwStartDate.Value);
            vrParameter.dwEndDate = DateToUint(dwEndDate.Value);
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
            if (ConfigConst.GCICTypeMode == 1)
            {
                UNITERM[] termnow = GetTermByID(Parse(Request["dwYearTerm"]));
                if (termnow != null && termnow.Length > 0)
                {
                    vrParameter.dwStartDate = termnow[0].dwBeginDate;
                    vrParameter.dwEndDate = termnow[0].dwEndDate;
                }
                else {
                    vrParameter.dwStartDate = 20120101;
                    vrParameter.dwEndDate = 20120101;
 
                }
            }
            uint dwClassKind = Parse(Request["dwClassKind"]);
            if (dwClassKind != 0)
            {
                vrParameter.dwClassKind = dwClassKind;
            }
        uResponse = m_Request.Report.GetDevUsingRate(vrParameter, out vrResultValue);
       if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            if (vrResultValue.szUsingTable != null && vrResultValue.szUsingTable.Length > 0)
            {
                DEVUSINGTABLE[] vrResult = vrResultValue.szUsingTable;
                for (int i = 360; i < 1440; i = i + 60)
                {
                    string time = i / 60 + ":" + (i % 60).ToString("00");
                    object times = vrResult[i].dwUseTimes;
                    if (times == null)
                    {
                        szResvRate += "<p><span>" + time + "</span><span>0</span></p>";
                    }
                    else {
                        szResvRate += "<p><span>" + time + "</span><span>" + times.ToString() + "</span></p>";
                    }

                }
            }
        }      
    
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreLoad(e);
       
    }
}
