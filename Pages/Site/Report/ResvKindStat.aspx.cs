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
    protected string szDept = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        CODINGTABLE[] vtCode = getCodeTableByType((uint)CODINGTABLE.DWCODETYPE.CODE_RESVKIND);

        RESVKINDSTATREQ vrParameter = new RESVKINDSTATREQ();
        vrParameter.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL + (uint)UNIRESERVE.DWPURPOSE.USEFOR_STUDYROOM;
        RESVKINDSTAT[] vrResult;
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
        vrParameter.dwStartDate = DateToUint(dwStartDate.Value);
        vrParameter.dwEndDate = DateToUint(dwEndDate.Value);

        if (m_Request.Report.GetResvKindStat(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            UpdatePageCtrl(m_Request.Report);

            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";


                m_szOut += "<td data-id=" + vrResult[i].dwKind.ToString() + ">" + GetCode(vtCode,(uint)vrResult[i].dwKind) + "</td>";
                m_szOut += "<td>" + vrResult[i].dwResvTimes.ToString() + "</td>";//学院人数
                m_szOut += "<td>" + vrResult[i].dwResvMinutes.ToString() + "</td>";/*使用人数*/
                m_szOut += "</tr>";

            }
        }

        PutBackValue();

    }
    protected string GetCode(CODINGTABLE[] vtRes,uint uKind)
    {
        string szRes = "";
        if (vtRes != null && vtRes.Length > 0)
        {
            for (int i = 0; i < vtRes.Length; i++)
            {
                if (Parse(vtRes[i].szCodeSN.ToString()) == uKind)
                {
                    return vtRes[i].szCodeName.ToString();
                }
            }
        }
        return szRes;
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {

       
    }
   
}
