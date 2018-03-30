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

public partial class ListByKind : UniPage
{
    protected string m_szOpts = "";
    protected MyString m_szOut = new MyString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["room"] != null)
        {
            m_szOpts += "机房号：" + Request["room"];
        }

        TEACHINGRESVREQ  vrParameter = new TEACHINGRESVREQ();
        TEACHINGRESV[] vrResult;
        vrParameter.dwBeginDate = 0;
        vrParameter.dwEndDate = 99999999;
        vrParameter.dwResvStat = (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DOING;
        vrParameter.szRoomNo = Request["room"];

        vrParameter.szReqExtInfo.dwNeedLines = 10;
        if (m_Request.Reserve.GetTeachingResv(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td>第" + ((vrResult[i].dwTeachingTime / 100) % 100).ToString() + "节</td>";
                m_szOut += "<td class='lnkTeacher' data-id='" + vrResult[i].dwTeacherID + "'>" + vrResult[i].szTeacherName + "</td>";
                m_szOut += "<td class='lnkCourse' data-id='" + vrResult[i].dwCourseID + "'>" + vrResult[i].szCourseName + "</td>";
                m_szOut += "<td>" + vrResult[i].szGroupName + "</td>";
                m_szOut += "<td>" + vrResult[i].szLabName + "</td>";
                m_szOut += "<td>" + vrResult[i].dwCurUsers + "/" + vrResult[i].dwGroupUsers + "</td>";
                m_szOut += "<td>" + (vrResult[i].dwTeachingTime % 100).ToString() + "</td>";
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
        }

        PutBackValue();
    }
}
