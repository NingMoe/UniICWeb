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
    protected string szDevList = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        {
            string szDevID = Request["devID"];
            IDENTSTATREQ vrParameter = new IDENTSTATREQ();
            IDENTSTAT[] vrResult;
            GetPageCtrlValue(out vrParameter.szReqExtInfo);
            if (!IsPostBack)
            {
                dwStartDate.Value = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                dwEndDate.Value = DateTime.Now.ToString("yyyy-MM-dd");

            }
            UNIDEPT[] alldept = GetAllDept();
            szDept+=GetInputItemHtml(CONSTHTML.option,"","全部","0");
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
            UNIDEVKIND[] devList = GetDevKindByKind((uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS);
            szDevList += GetInputItemHtml(CONSTHTML.option, "", "全部", "0");
            for (int i = 0; i < devList.Length; i++)
            {
                szDevList += GetInputItemHtml(CONSTHTML.option, "", devList[i].szKindName, devList[i].dwKindID.ToString());
            }
            if (szDevID != null && szDevID != "0")
            {
                //vrParameter.dwGetType = (uint)REPORTREQ.DWGETTYPE.USERECGET_BYDEVID;
                vrParameter.dwDevKind = Parse(szDevID);
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
                uint nOtherTotalUsers = 0;
                uint nOtherTotalPIDNum = 0;
                uint nOtherTotalUseTimes = 0;
                uint nOtherdwTotalUseTime = 0;
                for (int i = 0; i < vrResult.Length; i++)
                {
                    string szIdent = GetJustNameEqual(vrResult[i].dwIdent, "Ident");
                    if (szIdent == "")
                    {
                        nOtherTotalUsers = nOtherTotalUsers + (uint)vrResult[i].dwTotalUsers;
                        nOtherTotalPIDNum = nOtherTotalPIDNum + (uint)vrResult[i].dwPIDNum;
                        nOtherTotalUseTimes = nOtherTotalUseTimes + (uint)vrResult[i].dwUseTimes;
                        nOtherdwTotalUseTime = nOtherdwTotalUseTime + (uint)vrResult[i].dwTotalUseTime;
                        continue;
                    }
                    m_szOut += "<tr>";
                    m_szOut += "<td data-id=" + vrResult[i].dwIdent.ToString() + ">" + GetJustNameEqual(vrResult[i].dwIdent, "Ident", true) + "</td>";
                    m_szOut += "<td>" + vrResult[i].dwTotalUsers.ToString() + "</td>";//学院人数
                    m_szOut += "<td>" + vrResult[i].dwPIDNum.ToString() + "</td>";/*使用人数*/
                    m_szOut += "<td>" + vrResult[i].dwUseTimes.ToString() + "</td>";/*使用人次*/
                    uint uUseTime = (uint)vrResult[i].dwTotalUseTime;
                    m_szOut += "<td>" + uUseTime / 60 + "小时" + uUseTime % 60 + "分钟" + "</td>";
                    m_szOut += "<td>" + (uUseTime / ((uint)vrResult[i].dwTotalUsers + 1.0)).ToString(".00") + "</td>";/*使用人次*/
                    m_szOut += "</tr>";
                }
                if (nOtherTotalUsers > 0)
                {
                    m_szOut += "<tr>";
                    m_szOut += "<td>" + "其他" + "</td>";
                    m_szOut += "<td>" + nOtherTotalUsers.ToString() + "</td>";//学院人数
                    m_szOut += "<td>" + nOtherTotalPIDNum.ToString() + "</td>";/*使用人数*/
                    m_szOut += "<td>" + nOtherTotalUseTimes.ToString() + "</td>";/*使用人次*/
                    m_szOut += "<td>" + nOtherdwTotalUseTime / 60 + "小时" + nOtherdwTotalUseTime % 60 + "分钟" + "</td>";
                    m_szOut += "<td>" + (nOtherdwTotalUseTime / (nOtherTotalUsers * 1.0)).ToString(".00") + "</td>";/*使用人次*/
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
