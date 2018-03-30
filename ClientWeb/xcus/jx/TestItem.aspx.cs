using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_jx_TestItem : UniClientPage
{
    protected string resvPanelList = "";
    protected string resultList = "";
    UNIACCOUNT acc;
    UNITERM yearTerm;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LoadPage())
        {
            acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
            InitTerm();
            InitTestItem();
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void InitTestItem()
    {
        string planId = Request["plan_id"];
        TESTITEMINFOREQ req = new TESTITEMINFOREQ();
        if (!string.IsNullOrEmpty(planId))
            req.dwTestPlanID = ToUInt(planId);
        req.dwYearTerm = yearTerm.dwYearTerm;
        req.dwAccNo = acc.dwAccNo;
        //req.dwPlanKind = (uint)UNITESTPLAN.DWKIND.TESTPLANKIND_TEACHING;
        req.szReqExtInfo.szOrderKey = "szTestPlanName";
        req.szReqExtInfo.szOrderMode = "ASC";
        TESTITEMINFO[] rlt;
        if (m_Request.Reserve.GetTestItemInfo(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            string head = "<table class='table' style='margin-bottom:60px;'><thead style='background-color:#F3F3F3;'><tr>" +
                            "<th><span class='text-primary'><span class='glyphicon glyphicon-list'></span> 实验列表</span></th>" +
                            "<th>教师</th><th>学时</th><th></th><th>实验报告模版</th><th>实验报告状态</th><th></th><th>实验评分</th></tr></thead><tbody>";
            for (int i = 0; i < rlt.Length; i++)
            {
                TESTITEMINFO it = rlt[i];
                //if (yearTerm.dwYearTerm != it.dwYearTerm) continue;
                uint? usehour;
                InitResvPanel(it, out usehour);
                if (i == 0 || it.dwTestPlanID != rlt[i - 1].dwTestPlanID)
                {
                    uint? kind =it.dwPlanKind;
                    resultList += (resultList == "" ? "" : "</tbody></table></div>") + "<div class='plan_kind_" + kind + "'><h2 class='h_title'>" + it.szCourseName + " <small style='font-size:16px;'>"+(kind==1?"教学统一安排":(kind==2?"教学开放实验":""))+"</small></h2>" + head;// (i == rlt.Length - 1 ? "" : head);
                }
                resultList += "<tr id='test_it_" + it.dwTestItemID + "'><td>" + it.szTestName + "</td><td>" + it.szTeacherName + " </td>" +
                            "<td>" + it.dwTestHour + " 学时</td>" +
                            "<td><div><a class='click btn_test_resv' test_id='" + it.dwTestItemID + "'>上课时间安排 <span class='caret'></span></a></div></td>" +
                    //"<td>" + Util.Converter.GetTestItemState(it.dwStatus) + "</td>" +
                            "<td>" + (it.szReportFormURL == "" ? "<span class='orange'>缺少实验报告模版</span>" : "<a href='" + Page.ResolveClientUrl("~/ClientWeb/") + "upload/UpLoadFile/" + it.szReportFormURL + "'>下载实验报告模版</a>") + "</td>" +
                            "<td>" + (it.szReportURL == "" ? "<span class='orange'>未上传实验报告</span>" : "<a href='" + Page.ResolveClientUrl("~/ClientWeb/") + "upload/UpLoadFile/" + it.dwTestItemID + "/" + it.szReportURL + "'>已上传，点击下载</a>") + "</td>" +
                            "<td><a class='click' onclick='upload(\"" + it.dwSID + "\",\"" + it.dwTestItemID + "\",\"" + it.szTestName + "\")'>上传实验报告  <span class='glyphicon glyphicon-upload'></span></a></td>" +
                            "<td>" + (it.dwReportScore == 0 ? "<div class='orange text-center'>未评分</div>" : "<div class='text-info text-center'>" + it.dwReportScore + " 分 <span title='" + it.szReportMarkInfo + "'>[评语]</span></div>") + "</td></tr>";
            }
            if (resultList != "") resultList += "</tbody></table></div>";
        }
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
    private void InitResvPanel(TESTITEMINFO test, out uint? usehour)
    {
        //<button type='button' class='btn btn-primary pull-right' onclick='location.href=\"Reserve.aspx?term=" + yearTerm.dwYearTerm + "&test_id=" + test.dwTestItemID + "\"'>预约实验</button>
        string v = "<div id='panel_resv_" + test.dwTestItemID + "' class='resv_list'><div class='panel panel-default' style='margin-bottom:5px;'>" +
"<div class='panel-body' style='padding:10px 15px;'><span class='text-info panel_test_name'>实验：" + test.szTestName + "</span>" +
"</div>" +
"<table class='table table-striped'><tbody>"
        + GetResv(test, out usehour)
        + "</tbody></table></div></div>";
        resvPanelList += v;
    }
    string GetResv(TESTITEMINFO test, out uint? usehour)
    {
        UNIRESERVE[] resvs = test.ResvInfo;
        usehour = 0;
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
            usehour += resv.dwTestHour;
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