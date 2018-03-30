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
    protected string szResvRate = "";
    public class identTotal {
        public string szName;
        public uint uTotal;
    }
    protected string szDept = "";
    protected string m_YardActivity = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        YARDACTIVITYREQ activityReq = new YARDACTIVITYREQ();
        YARDACTIVITY[] YardActivity;
        if (m_Request.Reserve.GetYardActivity(activityReq, out YardActivity) == REQUESTCODE.EXECUTE_SUCCESS && YardActivity != null & YardActivity.Length > 0)
        {
            m_YardActivity += GetInputItemHtml(CONSTHTML.option, "", "全部", "0");
            for (int i = 0; i < YardActivity.Length; i++)
            {
                m_YardActivity += GetInputItemHtml(CONSTHTML.option, "", YardActivity[i].szActivityName, YardActivity[i].dwActivitySN.ToString());
            }
        }
        {
            IDENTSTATREQ vrParameter = new IDENTSTATREQ();
            IDENTSTAT[] vrResult;
            GetPageCtrlValue(out vrParameter.szReqExtInfo);
            if (!IsPostBack)
            {
                dwStartDate.Value = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                dwEndDate.Value = DateTime.Now.ToString("yyyy-MM-dd");

            }
            UNIDEPT[] alldept = GetAllDept();
            szDept += GetInputItemHtml(CONSTHTML.option, "", "全部", "0");
            if (alldept != null && alldept.Length > 0)
            {
                for (int i = 0; i < alldept.Length; i++)
                {
                    szDept += GetInputItemHtml(CONSTHTML.option, "", alldept[i].szName, alldept[i].dwID.ToString());
                }
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
            uint uActibitySN = Parse(Request["dwActivitySN"]);
            if (uActibitySN != 0)
            {
                vrParameter.dwActivitySN = uActibitySN;
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
            uint uDept = Parse(Request["dwDept"]);
            if (uDept != 0)
            {
                vrParameter.dwDeptID = uDept;
            }
            if (m_Request.Report.GeIdentStat(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                UpdatePageCtrl(m_Request.Report);
                identTotal other = new identTotal();
                other.szName = "其他";
                other.uTotal = 0;
                ArrayList list = new ArrayList();
                for (int i = 0; i < vrResult.Length; i++)
                {
					string szIdent= GetJustNameEqual(vrResult[i].dwIdent, "Ident",true);
					if(szIdent=="")
					{
                        other.uTotal = other.uTotal + (uint)vrResult[i].dwTotalUsers;
                        continue;
					}
                    identTotal temp = new identTotal();
                    temp.szName = szIdent;
                    temp.uTotal = (uint)vrResult[i].dwTotalUsers; ;
                    list.Add(temp);
                }
                if(other.uTotal>0)
                { 
                    list.Add(other);
                }
                for (int i = 0; i < list.Count; i++)
                {
                    identTotal temp=new identTotal();
                    temp=(identTotal)list[i];
                    szResvRate += "<p data-value="+temp.uTotal.ToString()+">" + temp.szName+ "</p>";
                }
				
            }
        }
      
        PutBackValue();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {

       
    }
   
}
