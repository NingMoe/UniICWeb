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
    protected string szTerm = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        TEACHINGRESVRECREQ vrParameter = new TEACHINGRESVRECREQ();
        GetHTTPObj(out vrParameter);

        UNITERM[] termList = GetAllTerm();
        if (termList != null && termList.Length > 0)
        {
            szTerm += GetInputItemHtml(CONSTHTML.option, "", "未选择", "0");
            for (int i = 0; i < termList.Length; i++)
            {
                szTerm += GetInputItemHtml(CONSTHTML.option, "", termList[i].szMemo, (termList[i].dwBeginDate.ToString() + termList[i].dwEndDate.ToString()));
            }
        }

        if (!IsPostBack)
        {
            dwStartDate.Value = DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd");
            dwEndDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
        }
        vrParameter.dwStartDate = DateToUint(dwStartDate.Value);
        vrParameter.dwEndDate = DateToUint(dwEndDate.Value);
        TEACHINGRESVREC[] vrResult;
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (m_Request.Report.GetTeachingResvRec(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td>" + vrResult[i].szCourseName + "</td>";
                m_szOut += "<td>" + vrResult[i].szTeacherName + "</td>";
               
                m_szOut += "<td>" + vrResult[i].szLabName+ "</td>";
                m_szOut += "<td>" + vrResult[i].szGroupName + "</td>";
                m_szOut += "<td>" + vrResult[i].dwGroupUsers + "</td>";
                m_szOut += "<td>" + vrResult[i].dwAttendUsers + "</td>";
                m_szOut += "<td>" + GetTeachingTime((uint)vrResult[i].dwTeachingTime) + "</td>";
               // m_szOut += "<td>" + vrResult[i].szTestName + "</td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Report);
        }

        PutBackValue();
    }   
}
