using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using Util;

public partial class ClientWeb_xcus_all_coursedetail : UniClientPage
{
    string courseId;
    protected string CourseName = "";
    protected string courseIntro = "";
    protected string testPlanList = "";
    protected string planCount = "0";
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadPage();
        courseId = Request["courseId"];
        if (string.IsNullOrEmpty(courseId)) return;
        InitCourseInfo(courseId);
        InitTestPlan(courseId);
        courseIntro = GetXmlContent(courseId, "course_intro");
    }
    private void InitCourseInfo(string id)
    {
        COURSEREQ req = new COURSEREQ();
        req.dwCourseID = ToUInt(id);
        UNICOURSE[] rlt;
        if (m_Request.Reserve.GetCourse(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            if (rlt == null || rlt.Length == 0) return;
            UNICOURSE cu = rlt[0];
            CourseName = cu.szCourseName;
            CourseCode.InnerHtml = cu.szCourseCode;
            Dept.InnerHtml = cu.szDeptName;
            Major.InnerHtml = cu.szMajorName;
            CourseType.InnerHtml = cu.szType;
            Credit.InnerHtml = cu.dwCreditHour.ToString();
            TheoryHour.InnerHtml = cu.dwTheoryHour.ToString();
            TestHour.InnerHtml = cu.dwTestHour.ToString();
            PracticeHour.InnerHtml = cu.dwPracticeHour.ToString();
            TestNum.InnerHtml = cu.dwTestNum.ToString();
        }
    }
    private void InitTestPlan(string courseId)
    {
        UNITERM term = GetTerm();
        if (term.dwYearTerm != null)
        {
            TESTPLANREQ req = new TESTPLANREQ();
            req.dwYearTerm = term.dwYearTerm;
            req.dwGetType = (uint)TESTPLANREQ.DWGETTYPE.TESTPLANGET_BYCOURSEID;
            req.szGetKey = courseId;
            UNITESTPLAN[] rlt;
            if (m_Request.Reserve.GetTestPlan(req, out rlt)==REQUESTCODE.EXECUTE_SUCCESS)
            {
                planCount = rlt.Length.ToString();
                for (int i = 0; i < rlt.Length; i++)
                {
                    UNITESTPLAN plan=rlt[i];
                    testPlanList += "<li><h4>"+plan.szTestPlanName+"</h4><div class='grey songti'><span class='glyphicon glyphicon-home'></span>&nbsp;班级："+plan.szGroupName+"&nbsp;&nbsp;<span class='glyphicon glyphicon-user'></span>&nbsp;教师："+plan.szTeacherName+" <span class='pull-right'>计划实验项目：<code>"+plan.dwTestNum+"</code> 个，计划实验学时：<code>"+plan.dwTestHour+"</code> 小时</span></div><div class='line'></div></li>";
                }
            }
        }
    }
}