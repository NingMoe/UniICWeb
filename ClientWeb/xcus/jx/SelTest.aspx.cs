using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_jx_SelTest : UniClientPage
{
    protected string resultList = "";
    protected string testPlanList = "";
    UNIACCOUNT acc;
    UNITERM yearTerm;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LoadPage())
        {
            acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
            InitTerm();
            InitTestPlan();
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    //private void test()
    //{
    //    TESTITEMINFOREQ req = new TESTITEMINFOREQ();
    //    req.dwPlanKind = (uint)UNITESTPLAN.DWKIND.TESTPLANKIND_OPEN;
    //    req.dwYearTerm = yearTerm.dwYearTerm;
    //    TESTITEMINFO[] rlt;
    //    if(m_Request.Reserve.GetTestItemInfo(req,out rlt)==REQUESTCODE.EXECUTE_SUCCESS){
    //        for (int i = 0; i < rlt.Length; i++)
    //        {
    //            TESTITEMINFO info = rlt[i];
    //        }
    //    }
    //}

    private void InitTestPlan()
    {
        string teacher = Request["tch"];
        TESTPLANREQ req = new TESTPLANREQ();
        req.dwYearTerm = yearTerm.dwYearTerm;
        if (!string.IsNullOrEmpty(teacher) && teacher != "0")
        {
            req.dwGetType = (uint)TESTPLANREQ.DWGETTYPE.TESTPLANGET_BYTEACHER;
            req.szGetKey = teacher;
        }
        else
        {
            req.dwGetType = (uint)TESTPLANREQ.DWGETTYPE.TESTPLANGET_BYALL;
        }
        req.dwKind = (uint)UNITESTPLAN.DWKIND.TESTPLANKIND_OPEN;
        req.dwStatus = (uint)UNITESTPLAN.DWSTATUS.TESTPLANSTAT_OPENING;
        req.szReqExtInfo.szOrderKey = "szCourseName";
        req.szReqExtInfo.szOrderMode = "ASC";
        UNITESTPLAN[] rlt;
        if (m_Request.Reserve.GetTestPlan(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            uint now = ToUInt(DateTime.Now.ToString("yyyyMMdd"));
            testPlanList+= "<table class='table' style='margin-bottom:60px;'><thead style='background-color:#F3F3F3;'><tr>" +
"<th><span class='text-primary'><span class='glyphicon glyphicon-list'></span> 课程列表</span></th>" +
"<th>教师</th><th>学时</th><th>项目数</th><th>班级容量</th><th>已加入</th><th>状态</th><th>加入截止日期</th><th></th><th></th><th></th></tr></thead><tbody>";
            for (int i = 0; i < rlt.Length; i++)
            {
                UNITESTPLAN plan = rlt[i];
                if (now > plan.dwEnrollDeadline) continue;//已过期
                int num=GetTestItem(plan);
                string act = "<button type='button' plan_name='\"" + plan.szTeacherName + "，" + plan.szTestPlanName + "\"' group_id='" + plan.dwGroupID + "' class='group_act btn btn-xs " + ((plan.dwStatus & (uint)UNITESTPLAN.DWSTATUS.TESTPLANSTAT_SELECTED) > 0 ? "btn-warning out'>退出" : "btn-info join'>加入") + "</button>";
                testPlanList += "<tr id='plan_it_" + plan.dwTestPlanID + "' class='tr_tch tch_"+plan.dwTeacherID+"'><td style='font-weight: bold;'>" + plan.szTestPlanName + "</td><td style='font-weight: bold;'>" + plan.szTeacherName + " </td>" +
            "<td>" + plan.dwTestHour + " 学时</td>" +
            "<td>" +num+ "</td>" +
            "<td>" + plan.dwMaxUsers + "</td><td>" + plan.dwGroupUsers + "</td><td>" + ((plan.dwStatus & (uint)UNITESTPLAN.DWSTATUS.TESTPLANSTAT_SELECTED) > 0 ? "<span class='green'>已加入</span>" : "<span class='orange'>未加入</span>") + "</td>" +
            "<td>"+Util.Converter.UintToDateStr(plan.dwEnrollDeadline)+"</td>" +
            "<td><div><a class='click btn_test_resv' plan_id='" + plan.dwTestPlanID + "' plan_name='"+plan.szTestPlanName+"'>上课时间安排 <span class='caret'></span></a></div></td>" +
            "<td><a class='click'  target='_blank' href='Art.aspx?type=course_intro&id=" + plan.dwTestPlanID + "'>详细介绍</a></td>" +
            "<td>"+act+"</td></tr>";
            }
            if (testPlanList != "") testPlanList += "</tbody></table></div>";
        }
    }
    private int GetTestItem(UNITESTPLAN plan)
    {
        TESTITEMREQ req = new TESTITEMREQ();
        req.dwGetType = (uint)TESTITEMREQ.DWGETTYPE.TESTITEMGET_BYPLANID;
        req.szGetKey = plan.dwTestPlanID.ToString();
        req.dwPlanKind = (uint)UNITESTPLAN.DWKIND.TESTPLANKIND_OPEN;
        UNITESTITEM[] rlt;
        if (m_Request.Reserve.GetTestItem(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            string v = "<div id='panel_resv_" + plan.dwTestPlanID + "' class='resv_list'><div class='panel panel-default' style='margin-bottom:5px;'>" +
"<div class='panel-body' style='padding:10px 15px;'><span class='text-info panel_test_name'>计划：" + plan.szTestPlanName + "</span>" +
"</div><table class='table table-striped'><tbody>";
            for (int i = 0; i < rlt.Length; i++)
            {
                    v += GetResv(rlt[i]);
            }
            resultList += v + "</tbody></table></div></div>";
            return rlt.Length;
        }
        return 0;
    }
    private void InitTerm()
    {
        TERMREQ req = new TERMREQ();
        req.dwStatus = (uint)UNITERM.DWSTATUS.TERMSTAT_FORCE;
        UNITERM[] rlt;
        if (m_Request.Reserve.GetTerm(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            if (rlt.Length > 0)
            {
                yearTerm = rlt[0];
                Master.Year = (uint)rlt[0].dwYearTerm;
            }
        }
        else
            MsgBox(m_Request.szErrMsg);
    }

    string GetResv(UNITESTITEM test)
    {
        UNIRESERVE[] resvs = test.ResvInfo;
        //usehour = 0;
        string[] week = { "一", "二", "三", "四", "五", "六", "日" };
        string ret = "";
        for (int i = 0; i < resvs.Length; i++)
        {
            UNIRESERVE resv = resvs[i];
            string date = "";
            if (resv.dwPreDate > 0)
            {
                date = (resv.dwPreDate / 100 % 100) + "月" + (resv.dwPreDate % 100) + "日";
            }
            uint? tchl = resv.dwTeachingTime;
            int start = (int)(tchl % 10000) / 100;
            int end = (int)tchl % 100;
            //usehour += resv.dwTestHour;
            string rooms = GetRoomsFromResvDev(resv.ResvDev);
            string time ="(第"+ (int)tchl / 100000 + "周)【" + "星期" + week[(int)((tchl / 10000) % 10)] + "】第" + start + (start == end ? "" : ("-" + end)) + "节";
            ret += "<tr><td class='text-primary'>" + date + time + "</td>" +
                                    "<td>" + resv.szLabName + "</td>" +
                                    "<td>" + rooms + "</td>" +
                                    "<td><span class='text-primary'>" + resv.dwTestHour + "</span> 学时</td>" +
                                    "<td>" + Util.Converter.ResvStatusConverter(resv.dwStatus) + "</td>" +
                                    "</tr>";
        }
        return ret;
    }
}