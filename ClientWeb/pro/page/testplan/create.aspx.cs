using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_pro_page_testplan_create : UniClientPage
{
    protected string termList = "";
    protected string curTerm = "";
    protected uint planKind = 0;
    protected UNITERM term;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsFrameLogin())
            InitTerm();
        //实验计划类型
        string type = Request["type"];
        if (type == "self") planKind = 4;
        else if (type == "open") planKind = 2;
        else planKind = 0;
    }
    private void InitTerm()
    {
        UNITERM[] rlt = InitTermList(out term, Request["term"]);
        for (int i = 0; i < rlt.Length; i++)
        {
            termList += "<li><a onclick='selTermYear(\"" + rlt[i].dwYearTerm + "\")'>" + rlt[i].szMemo + "</a></li>";
        }
        curTerm = term.szMemo;
        term_year.Value = term.dwYearTerm.ToString();
    }
    protected void cre_course_ServerClick(object sender, EventArgs e)
    {
        UNIACCOUNT acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
        string id = Request["plan_id"];
        string name = Request["plan_name"];
        string courseId = Request["course_id"];
        string courseName = Request["course_name"];
        string testHour=Request["test_hour"];
        string testNum=Request["test_num"];
        string type = Request["type"];
        string groupId = Request["group_id"];
        string file = Request["up_file"];
        string user_kind = Request["user_kind"];
        uint? max = ToUInt(Request["mb_max"]);
        string deadline = Request["deadline"];
        UNITESTPLAN para = new UNITESTPLAN();
        if (string.IsNullOrEmpty(term_year.Value) || term_year.Value == "0")
        {
            MsgBox("请选择学期");
            return;
        }
        else
        {
            para.dwYearTerm = ToUInt(term_year.Value);
        }
        if (string.IsNullOrEmpty(courseId) || string.IsNullOrEmpty(courseName))
        {
            MsgBox("请选择课程");
            return;
        }
        else
        {
            para.dwCourseID = ToUInt(courseId);
            para.szCourseName = courseName;
        }
        if (!string.IsNullOrEmpty(user_kind))
            para.dwTesteeKind = ToUInt(user_kind);
        if (!string.IsNullOrEmpty(name))
            para.szTestPlanName = name;
        if (!string.IsNullOrEmpty(testHour))
        {
            uint tmp=ToUInt(testHour);
            if (tmp == 0)
            {
                MsgBox("请填入正确的学时数");
                return;
            }
            para.dwTestHour = tmp;
        }
        if (!string.IsNullOrEmpty(testNum))
            para.dwTestNum = ToUInt(testNum);
        para.dwTeacherID = acc.dwAccNo;
        if (!string.IsNullOrEmpty(type))
        {
            if (type == "self") para.dwKind = (uint)UNITESTPLAN.DWKIND.TESTPLANKIND_TEACHFORSELF;
            else if (type == "open")
            {
                if (max == 0 || string.IsNullOrEmpty(deadline))
                {
                    MsgBox("请正确填写班级信息");
                    return;
                }
                para.dwStatus = (uint)UNITESTPLAN.DWSTATUS.TESTPLANSTAT_UNOPEN;
                para.dwKind = (uint)UNITESTPLAN.DWKIND.TESTPLANKIND_OPEN;
            }
            else para.dwKind = (uint)UNITESTPLAN.DWKIND.TESTPLANKIND_TEACHING;
        }
        if (!string.IsNullOrEmpty(groupId))
        {
            para.dwGroupID = ToUInt(groupId);
        }
        else if (max > 0 && !string.IsNullOrEmpty(deadline))//开放实验 组设置
        {
            uint? gId = SetGroup(max, deadline);
            if (gId == null)
            {
                MsgBox("设置班级时出错");
                return;
            }
            else
                para.dwGroupID = gId;
        }
        if (!string.IsNullOrEmpty(id))//修改操作
        {
            para.dwTestPlanID = ToUInt(id);
        }
        else//新建操作
        {
            para.szTestPlanName = acc.szTrueName + "_" + courseName;
        }
        if (m_Request.Reserve.SetTestPlan(para, out para) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            if (!string.IsNullOrEmpty(id))
            {
                MsgBox("修改实验计划成功", "parent.location.reload();");
            }
            else
            {
                if (GetConfig("scheduleMode") == "2")//自动创建项目
                {
                    UNITESTPLAN plan = GetTestPlanByID(para.dwTestPlanID.ToString());
                    if (plan.dwTestPlanID != null && plan.dwTestPlanID != 0)
                    {
                        TESTCARD card = new TESTCARD();
                        card.dwGroupPeopleNum = plan.dwGroupUsers;
                        card.dwTestHour = plan.dwTestHour;
                        card.szTestName = plan.szTestPlanName;
                        if (m_Request.Reserve.SetTestCard(card, out card) == REQUESTCODE.EXECUTE_SUCCESS)
                        {
                            UNITESTITEM test = new UNITESTITEM();
                            test.dwTestPlanID = plan.dwTestPlanID;
                            test.dwTeacherID = acc.dwAccNo;
                            test.dwTestCardID = card.dwTestCardID;
                            test.dwGroupID = plan.dwGroupID;
                            if (m_Request.Reserve.SetTestItem(test, out test) == REQUESTCODE.EXECUTE_SUCCESS)
                            {
                                MsgBox("创建实验计划成功", "parent.crePlanSuc?parent.crePlanSuc('"+type+"'):parent.location.reload();");
                                return;
                            }
                        }
                    }
                    MsgBox("创建实验项目时出错：" + m_Request.szErrMsg, "parent.location.reload();");
                    m_Request.Reserve.DelTestPlan(para);
                }
                else
                {
                    MsgBox("创建实验计划成功", "parent.crePlanSuc?parent.crePlanSuc('" + type + "'):parent.location.reload();");
                }
            }
        }
        else
            MsgBox(m_Request.szErrMsg);
    }
    private uint? SetGroup(uint? max, string deadline)
    {
        string name = Request["cls_name"];
        string id = Request["plan_id"];
        UNIGROUP set = new UNIGROUP();
        if (!string.IsNullOrEmpty(name))
            set.szName = name;
        else
        {
            UNIACCOUNT acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
            set.szName =acc.szTrueName+ "_"+Request["course_name"];
        }
        if (!string.IsNullOrEmpty(id))
            set.dwGroupID = ToUInt(id);
        set.dwMinUsers = 0;
        set.dwMaxUsers = max;
        set.dwEnrollDeadline = ToUInt(deadline.Replace("-", ""));
        set.dwDeadLine = term.dwEndDate;
        set.dwKind = (uint)UNIGROUP.DWKIND.GROUPKIND_RERV;
        if (m_Request.Group.SetGroup(set, out set) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            return set.dwGroupID;
        }
        else
        {
            return null;
        }
    }
}