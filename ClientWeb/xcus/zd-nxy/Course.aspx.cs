using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using UniStruct;
using System.Xml;
using Newtonsoft.Json;

public partial class DevWeb_Course : UniClientPage
{
    protected string rtList = "";
    protected string stuList = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        base.LoadPage();
        if (Session["LOGIN_ACCINFO"] == null||!IsLogined())
        {
            Response.Redirect("Default.aspx");
        }
        //获取课题列表
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        RESEARCHTESTREQ vrGet = new RESEARCHTESTREQ();
        RESEARCHTEST[] vrResult;
        UNIACCOUNT acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
        vrGet.dwHolderID = acc.dwAccNo;
        uResponse = m_Request.Reserve.GetResearchTest(vrGet, out vrResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                if (i % 2 == 0)
                {
                    rtList += "<tr id='" + vrResult[i].dwRTID + "' class='odd'>";
                }
                else
                {
                    rtList += "<tr id='" + vrResult[i].dwRTID + "' >";
                }
                rtList += "<td ><input type='hidden' class='courseId' value='" + vrResult[i].dwRTID + "'/>" + vrResult[i].szRTName + "</td><td >" + vrResult[i].szLeaderName + "</td><td>" + ConvertToZero(vrResult[i].dwTestTimes) + "</td><td>"
                    + ConvertToZero(vrResult[i].dwTestMinutes) + "</td><td><input type='hidden' class='rtGroupId' value='" + vrResult[i].dwGroupID + "'/>" + vrResult[i].dwGroupUsers + "</td><td><a class='alterCourse' href='#'>管理</a>|<a class='delCourse' href='#'>删除</a></td></tr>";
            }
        }
        else
        {
            Util.MessageBox.Show(this, m_Request.szErrMsg);
            return;
        }
        GetStudents();
    }

    private void GetStudents()
    {
        if (Session["LOGIN_ACCINFO"] == null)
        {
            Util.MessageBox.Show(this, "登录超时！");
            return;
        }
        UNIACCOUNT tutor = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        TUTORSTUDENTREQ vrGet = new TUTORSTUDENTREQ();
        vrGet.dwTutorID = tutor.dwAccNo;
        vrGet.szReqExtInfo.szOrderKey = "dwStatus";
        vrGet.szReqExtInfo.szOrderMode = "ASC";
        //vrGet.dwStatus = (uint)ADMINCHECK.DWSUBJECTTYPE.CHECK_TUTORSTUDENT;
        TUTORSTUDENT[] vrResult;
        uResponse = m_Request.Account.TutorStudentGet(vrGet, out vrResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null)
        {
            string list = "";
            for (int i = 0; i < vrResult.Length; i++)
            {
                TUTORSTUDENT stu = vrResult[i];
                if (i % 2 == 0)
                {
                    list += "<tr class='odd'>";
                }
                else
                {
                    list += "<tr>";
                }
                UNIACCOUNT acc = GetACCByAccNo(stu.dwAccNo);
                if (acc.dwAccNo == null)
                {
                    break;
                }
                list += "<td><span class='stu_name'>" + acc.szTrueName + "</span></td><td>" + acc.szLogonName + "<input type='hidden' class='stu_accno' value='"+acc.dwAccNo+"'/></td><td>" + acc.szDeptName + "</td><td>" + acc.szHandPhone +
                    "</td>" +CheckStatus(stu.dwStatus)+"</tr>";
            }
            stuList = list;
        }
    }

    UNIACCOUNT GetACCByAccNo(uint? accno)
    {
        ACCREQ vrGet = new ACCREQ();
        vrGet.dwAccNo = accno;
        UNIACCOUNT[] vrResult;
        if (m_Request.Account.Get(vrGet, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null && vrResult.Length > 0)
        {
            return vrResult[0];
        }
        return new UNIACCOUNT();
    }
    string CheckStatus(uint? sta)
    {
        if ((sta & (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK) > 0)
        {
            return "<td><span style='color: green'>已批准</span></td><td><a class='tutor_check fail'>撤销</a></td>";
        }
        else if ((sta & (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL) > 0)
        {
            return "<td><span style='color: red'>不批准</span></td><td><a class='tutor_check ok'>批准</a>|<a class='tutor_check del'>删除</a></td>";
        }
        return "<td><span style='color: red'>未审核</span></td><td><a class='tutor_check ok'>批准</a>|<a class='tutor_check del'>删除</a></td>";
    }

    string ConvertToZero(object obj)
    {
        if (obj == null || obj.ToString() == "")
        {
            return "0";
        }
        else
        {
            return obj.ToString();
        }
    }
}