using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_jx_TestPlan : UniClientPage
{
    protected string termList = "";
    protected string curTerm = "";
    private UNITERM yearTerm;
    protected string testPlanTotal = "";
    protected string testTotal = "";
    protected string period = "";
    protected string testPlanList = "";
    protected string sideList = "";
    protected string resvPanelList = "";
    protected string courseKind = "";
    protected uint testPlanKind = 0;
    protected bool isHitem = false;
    UNIACCOUNT acc;
    Dictionary<uint?, string> dic = new Dictionary<uint?, string>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LoadPage())
        {
            acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
            //ADMINLOGINRES res = (ADMINLOGINRES)Session["LoginRes"];
            if ((acc.dwIdent & 536870912) == 0)
            {
                Response.Redirect("TestItem.aspx");
                return;
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
        testPlanKind = ToUInt(GetConfig("testPlanKind"));
        isHitem = GetConfig("scheduleMode") == "2";
        courseKind = GetConfig("courseKind")=="2"?"授课":"实验";
        InitTerm();
        InitTestPlan();
        //FinishResvPanel(yearTerm.dwBeginDate, yearTerm.dwEndDate);
    }

    private void InitTestPlan()
    {
        string kind = Request["type"];
        TESTPLANREQ req = new TESTPLANREQ();
        req.dwGetType = (uint)TESTPLANREQ.DWGETTYPE.TESTPLANGET_BYTEACHER;
        req.dwYearTerm = yearTerm.dwYearTerm;
        req.szGetKey = acc.dwAccNo.ToString();
        if (string.IsNullOrEmpty(kind))
            req.dwKind = (uint)UNITESTPLAN.DWKIND.TESTPLANKIND_TEACHING;
        else
        {
            if (kind == "open")
                req.dwKind = (uint)UNITESTPLAN.DWKIND.TESTPLANKIND_OPEN;
            else if (kind == "self")
                req.dwKind = (uint)UNITESTPLAN.DWKIND.TESTPLANKIND_TEACHFORSELF;
            else
                req.dwKind = (uint)UNITESTPLAN.DWKIND.TESTPLANKIND_TEACHING;
        }
        UNITESTPLAN[] rlt;
        if (m_Request.Reserve.GetTestPlan(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            int testnum = 0;
            uint? periodnum = 0;
            for (int i = 0; i < rlt.Length; i++)
            {
                int testhour = 0;
                int itemnum = 0;
                UNITESTPLAN plan = rlt[i];
                string deadline=Util.Converter.UintToDateStr(plan.dwEnrollDeadline);
                string its = GetTestItem(plan, out testhour,out itemnum);
                bool flag=(rlt[i].dwStatus & (uint)UNITESTPLAN.DWSTATUS.TESTPLANSTAT_OPENING) > 0;
                string staAct = (rlt[i].dwKind & (uint)UNITESTPLAN.DWKIND.TESTPLANKIND_OPEN) > 0 ?
                    ("&nbsp;&nbsp;&nbsp;学生选课：<button type='button' deadline='" + deadline + "' class='btn btn-" + (flag ? "info" : "warning") + " btn-xs' onclick='changePlanStatus(\"" + rlt[i].dwTestPlanID + "\",\"" + plan.dwMaxUsers + "\",\"" + deadline + "\",\"" +
                    (flag ? 512 + "\")'>开放到" + deadline + " "
                    : 256 + "\")'>未开放") + " <span class='glyphicon glyphicon-cog'></span></button>") : "";
                if (its == "") its = "<tr><td class='text-center'>没有实验项目，请点击【添加实验项目】</td></tr>";
                testnum += itemnum;
                periodnum += rlt[i].dwTestHour;
                sideList += "<li><a href='#plan_" + plan.dwTestPlanID + "' class='plan_md_" + plan.dwTestPlanID + "'>" + plan.szTestPlanName + "</a></li>";
                testPlanList += "<div class='panel-body plan_md plan_md_" + plan.dwTestPlanID + "'>";
                testPlanList += "<div class='plan_h' id='plan_" + plan.dwTestPlanID + "' planid="+plan.dwTestPlanID+" testhour="+plan.dwTestHour+" donehour="+testhour+"><h2 class='h_title'>" + plan.szTestPlanName + "</h2></div>";
                testPlanList += "<div class='info'>课程代码：" + plan.szCourseCode +
                    "<span style='padding: 0 20px;'>计划学时：<span class='text-primary'>" + plan.dwTestHour + "</span> 学时"+(isHitem?"":"(已安排<span class='red'>" + testhour + "</span>学时)") +
                    "</span>实验项目数：<span class='text-primary'>" + itemnum + "</span>" +
                    "<span style='padding-left:20px;'>上课班级：<button type='button' class='btn btn-info btn-xs' title='设置上课班级' onclick='openGroup(\"" + plan.dwGroupID + "\")' id='btn_g_" + plan.dwGroupID + "'><span class='group_name'>" + plan.szGroupName + "</span>("+((rlt[i].dwKind & (uint)UNITESTPLAN.DWKIND.TESTPLANKIND_OPEN) > 0 ?plan.dwMaxUsers+"/":"")+
                    "<span class='group_num'>"+plan.dwGroupUsers+"</span>) <span class='glyphicon glyphicon-cog'></span></button>" + staAct + "</span>" +
                    "<span class='pull-right text-info'><a target='_blank' href='" + ResolveClientUrl("~/ClientWeb/editContent.aspx") + "?id=" + plan.dwTestPlanID + "&type=course_intro&w=1134'>编辑课程介绍</a>" + 
                    "&nbsp;&nbsp;&nbsp;<a onclick='openCreTest({\"plan_id\":" + plan.dwTestPlanID + ",\"usable\":" + ((int)plan.dwTestHour - testhour) + "})' class='" + (isHitem ? "hidden" : "") + "'><span class='glyphicon glyphicon-plus'></span>添加实验项目</a>" +
                    "&nbsp;&nbsp;&nbsp;<span class='glyphicon glyphicon-trash' " + (testhour == 0||isHitem ? "title='删除' onclick='delPlan(\"" + plan.dwTestPlanID + "\")' style='cursor:pointer;'" : "style='color:#ddd;'") + "></span></span></div>";
                //<span title='修改' class='glyphicon glyphicon-cog  click'></span>  
                testPlanList += "</div>";
                //实验项目
                testPlanList += "<table class='table table-striped plan_md_" + plan.dwTestPlanID + "'>";
                testPlanList += "<tbody>" + its;
                testPlanList += "</tbody></table>";
            }
            if (rlt.Length == 0)//没有实验验计划
            {
                testPlanList += "<div class='panel-body plan_md'><h2 class='h_title'><span style='font-size:14px;'>没有" + courseKind + "计划，请点击上方【新建" + courseKind + "计划】</span></h2></div>";
            }
            testPlanTotal = rlt.Length.ToString();
            testTotal = testnum.ToString();
            period = periodnum.ToString();
        }
        else
        {
            MsgBox(m_Request.szErrMsg);
        }
    }

    private string GetTestItem(UNITESTPLAN plan, out int testhour,out int itemnum)
    {
        string ret = "";
        testhour = 0;
        itemnum = 0;
        TESTITEMREQ req = new TESTITEMREQ();
        req.dwGetType = (uint)TESTITEMREQ.DWGETTYPE.TESTITEMGET_BYPLANID;
        req.szGetKey = plan.dwTestPlanID.ToString();
        req.dwCourseID = plan.dwCourseID;
        UNITESTITEM[] rlt;
        if (m_Request.Reserve.GetTestItem(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            itemnum = rlt.Length;
            for (int i = 0; i < rlt.Length; i++)
            {
                UNITESTITEM it = rlt[i];
                testhour += (int)it.dwTestHour;
                uint? usehour;//手动计算的预约时间总和
                string rsv=InitResvPanel(it, out usehour);
                string uped = string.IsNullOrEmpty(it.szReportFormURL) ? "(未上传)" : "(已上传)";
                ret += "<tr class='tr_test_it' id='test_it_" + it.dwTestItemID + "' style='border-bottom: 2px #31b0d5 solid;'><td>" + it.szTestName + "</td>" +
                            "<td>" + it.dwTestHour + " 学时(已用<span class='red'>" + it.dwResvTestHour + "</span>学时)</td>" +
                            //"<td>" + Util.Converter.GetTestItemState(it.dwStatus) + "</td>" +
                            "<td><button type='button' style='color:#428bca;' class='btn btn-default btn-xs' planid="+it.dwTestPlanID+" testid="+it.dwTestItemID+
                            " onclick='linkResv(\"Reserve.aspx?term=" + yearTerm.dwYearTerm + "&test_id=" + it.dwTestItemID + "\",this)'>预约排课 <span class='glyphicon glyphicon-forward'></span></button></td>" +
                            "<td><a href='Report.aspx?test_id=" + it.dwTestItemID + "' target='_blank'>实验报告>></a></td>" +
                            "<td><span class='pull-right text-info'><a class='click' test_id='" + it.dwTestItemID + "' onclick='uploadFile(this)'>"+uped+"上传报告模版 <span class='glyphicon glyphicon-upload'></span></a>"
                            +"<a class='click' test_id='" + it.dwTestItemID + "' onclick='setTestitem(this)'>更改设置 <span class='glyphicon glyphicon-cog'></span></a>  <span class='glyphicon glyphicon-trash " + (isHitem?"hidden":"") + "' "
                            + (it.ResvInfo.Length == 0 ? "title='删除' onclick='delTestitem(\"" + it.dwTestItemID + "\",\"" + it.dwTestCardID + "\")' style='cursor:pointer;'" : "style='color:#ddd;'") + "></span></span></td></tr>";
                ret += "<tr><td colspan='8' style='padding:0;'>"+rsv+"</td></tr>";
            }
        }
        return ret;
    }

    private void InitTerm()
    {
        UNITERM term;
        UNITERM[] rlt = InitTermList(out term, Request["term"]);
        curTerm = term.szMemo;
        yearTerm = term;
        Master.Year = (uint)term.dwYearTerm;
        for (int i = 0; i < rlt.Length; i++)
        {
            termList += "<li><a onclick='selTermYear(\"" + rlt[i].dwYearTerm + "\")'>" + rlt[i].szMemo + "</a></li>";
        }
    }
    private string InitResvPanel(UNITESTITEM test, out uint? usehour)
    {
        string v = "<div class='panel_resv_list'>" +
//"<button type='button' class='btn btn-primary pull-right' onclick='location.href=\"Reserve.aspx?term=" + yearTerm.dwYearTerm + "&test_id=" + test.dwTestItemID + "\"'>预约实验</button>" +
"<table class='table' style='margin-bottom:0;'><tbody>"
        + GetResv(test, out usehour)
        + "</tbody></table></div>";
        //resvPanelList += v;
        return v;
        //dic.Add(test.dwTestItemID, v);
    }
    string GetResv(UNITESTITEM test, out uint? usehour)
    {
        UNIRESERVE[] resvs = test.ResvInfo;
        usehour = 0;
        string[] week = { "一", "二", "三", "四", "五", "六", "日" };
        string ret = "";
        for (int i = 0; i < resvs.Length; i++)
        {
            UNIRESERVE resv = resvs[i];
            uint? tchl = resv.dwTeachingTime;
            int start = (int)(tchl % 10000) / 100;
            int end = (int)tchl % 100;
            usehour += resv.dwTestHour;
            string rooms = GetRoomsFromResvDev(resv.ResvDev);
            string time =(int)tchl / 100000 + "周【" + "星期" + week[(int)((tchl / 10000) % 10)] + "】第" + start + (start == end ? "" : ("-" + end)) + "节";
            ret += "<tr><td class='text-primary'>" + time + "</td>" +
                                    "<td>" + rooms + "</td>" +
                                    "<td>" + resv.dwResvUsers + " 人</td>" +
                                    "<td><span class='text-primary'>" + resv.dwTestHour + "</span> 学时</td>" +
                                    "<td>" + Util.Converter.ResvStatusConverter(resv.dwStatus) + "</td>" +
                                    "<td>" + (!IsStat(resv.dwStatus, (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNDO) ? "<a href='Attendance.aspx?plan_id=" + test.dwTestPlanID + "&test_id=" + test.dwTestItemID + "&resv_id=" + resv.dwResvID + "&start=" + yearTerm.dwBeginDate + "&end=" + yearTerm.dwEndDate + "' target='_blank'>学生考勤>></a>" : "<span class='grey'>学生考勤>></span>") + "</td>" +
                                    "<td><span class='pull-right text-info'><span title='删除' class='glyphicon glyphicon-trash click' rsv_id='" + resv.dwResvID + "' onclick='delResv(this);'></span></span></td></tr>";
        }
        if (ret == "") ret = "<tr><td colspan='10' class='text-center'>请点击【预约排课】预约上课</td></tr>";
        return ret;
    }
}