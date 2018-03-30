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

public partial class Sub_Device : UniPage
{
    protected string m_szOut = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        
        COURSEREQ vrParameter = new COURSEREQ();
    
        UNICOURSE[] vrResult;
        if (Request["delID"] != null)
        {
            DelCourse(Request["delID"]);
        }
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (m_Request.Reserve.GetCourse(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id=\"" + vrResult[i].dwCourseID.ToString() + "\">" + vrResult[i].szCourseCode + "</td>";
                m_szOut += "<td>" + vrResult[i].szCourseName + "</td>";
                m_szOut += "<td>" + GetJustName(vrResult[i].dwCourseProperty, "Course_Property") + "</td>";
                m_szOut += "<td>" + vrResult[i].dwCreditHour + "</td>";
                m_szOut += "<td>" + vrResult[i].dwTestNum + "</td>";
                m_szOut += "<td>" + vrResult[i].dwTestHour + "</td>";
                m_szOut += "<td>" + vrResult[i].dwTheoryHour + "</td>";
                m_szOut += "<td>" + vrResult[i].dwPracticeHour + "</td>";
                string szDivOPTD = "OPTD";
                
                m_szOut += "<td><div class='" + szDivOPTD + "'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Reserve);
        }
        PutBackValue();        
    }
    private void DelCourse(string szID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        UNICOURSE delCourse = new UNICOURSE();
        delCourse.dwCourseID = Parse(szID);
        uResponse = m_Request.Reserve.DelCourse(delCourse);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {

            MessageBox(m_Request.szErrMessage, "提示", MSGBOX.ERROR);
        }
    }
}
