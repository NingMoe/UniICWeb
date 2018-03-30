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
    protected string szTerm = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        TESTITEMSTATREQ vrParameter = new TESTITEMSTATREQ();
        TESTITEMSTAT[] vrResult;
        UNITERM[] termList = GetAllTerm();
        if (termList != null && termList.Length > 0)
        {
            szTerm += GetInputItemHtml(CONSTHTML.option, "", "未选择", "0");
            for (int i = 0; i < termList.Length; i++)
            {
                szTerm += GetInputItemHtml(CONSTHTML.option, "", termList[i].szMemo, (termList[i].dwBeginDate.ToString() + termList[i].dwEndDate.ToString()));
            }
        }
        GetHTTPObj(out vrParameter);
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (!IsPostBack)
        {
            dwStartDate.Value = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
            dwEndDate.Value = DateTime.Now.ToString("yyyy-MM-dd");

        }

        vrParameter.dwStartDate = DateToUint(dwStartDate.Value);
        vrParameter.dwEndDate = DateToUint(dwEndDate.Value);
        if (vrParameter.szReqExtInfo.szOrderKey == null)
        {
            vrParameter.szReqExtInfo.szOrderKey = "dwTestItemID";
            vrParameter.szReqExtInfo.szOrderMode = "desc";
        }
        if (m_Request.Report.GetTestItemStat(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
              
                m_szOut += "<td>" + vrResult[i].szCourseCode.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szCourseName.ToString() + "</td>";
                m_szOut += "<td>" + GetJustName(vrResult[i].dwCourseProperty, "Course_Property",false) + "</td>";
                m_szOut += "<td data-id=" + vrResult[i].dwTestItemID.ToString() + ">" + vrResult[i].szTestName + "</td>";
                m_szOut += "<td>" + vrResult[i].dwGroupPeopleNum.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwTestHour.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szTeacherName.ToString() + "</td>";
               
                m_szOut += "<td>" + vrResult[i].szGroupName.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwGroupUsers.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szLabName.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwDevNum.ToString() + "</td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Report);
        }

        PutBackValue();
    }
}
