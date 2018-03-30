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
    private int nAfterMin = 10;//上课后十分钟到课率
    private int nLowRate = 30;//下课前低于30的时间
    protected string szTerm = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        TEACHINGRESVRECREQ vrParameter = new TEACHINGRESVRECREQ();
        //GetHTTPObj(out vrParameter);
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
            UpdatePageCtrl(m_Request.Report);
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td dwResvID='" + vrResult[i].dwResvID + "' time='" + vrResult[i].dwBeginTime + "' dwTestPlanID='" + vrResult[i].dwTestPlanID + "' dwCourseID='" + vrResult[i].dwCourseID + "' dwTeacherID='" + vrResult[i].dwTeacherID + "' >" + vrResult[i].szCourseName + "</td>";
                m_szOut += "<td>" + vrResult[i].szTestName + "</td>";
                m_szOut += "<td>" + vrResult[i].szTeacherName + "</td>";
                m_szOut += "<td>" + vrResult[i].szLabName + "</td>";
                m_szOut += "<td>" + vrResult[i].szGroupName + "</td>";
               // m_szOut += "<td>" + vrResult[i].dwGroupUsers + "</td>";
                USERSPERMINUTE[] vtUserPerMinte = vrResult[i].UsersPerMinute;

                uint uTotal = (uint)vrResult[i].dwGroupUsers;
                bool bIs = false;
                string szFloatRate = "";
                string szFTemMin = "";
                string szLessTemPeople = "";
                if (vtUserPerMinte.Length > 10)
                {
                    uint u = 0;
                    uint.TryParse(vtUserPerMinte[9].dwUsers.ToString(), out u);
                    vrResult[i].dwAttendUsers = u;
                    if (uTotal == 0)
                    {
                        uTotal = 1;
                    }
                    szFTemMin = (((uint)(u) / (uTotal * 1.0)) * 100).ToString(".0");
                }
                int k = 0;
                bool bIsEnd = true;
                for (k = (vtUserPerMinte.Length - 1); k > 0; k--)
                {
                    if (bIsEnd && vtUserPerMinte[k].dwUsers != null && ((uint)vtUserPerMinte[k].dwUsers < 10))
                    {


                    }
                    else
                    {
                        k = k + 1;
                        bIsEnd = false;
                        break;
                    }
                }
                if (k == 0)
                {
                    szLessTemPeople = "0";
                }
                else
                {
                    szLessTemPeople = (vtUserPerMinte.Length - k).ToString();
                }
                vrResult[i].szCourseCode = "";
                string szInfo = "";
                if (!bIs)
                {
                    szInfo = ("上课后10分钟到课率" + szFTemMin + "%;<br />下课前" + szLessTemPeople + "分钟人数少于10");
                }
                m_szOut += "<td class='attendUser'>" + vrResult[i].dwAttendUsers + "</td>";
                m_szOut += "<td>" + GetTeachingTime((uint)vrResult[i].dwTeachingTime) + "</td>";
               // m_szOut += "<td>" + (szFTemMin + "%") + "</td>";//daokelv
                // m_szOut += "<td>" + szInfo + "</td>";

                //  m_szOut += "<td>" + szLessTemPeople + "</td>";//zaotuiqingkuang

                m_szOut += "</tr>";
            }
         
        }

        PutBackValue();
    }   
}
