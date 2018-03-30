using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using Newtonsoft.Json;

public partial class ClientWeb_xcus_jx_net_ajax_items : UniClientAjax
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LoadPage())
        {
            GetTestItem();
        }
    }
    private void GetTestItem()
    {
        string v = "";
        string id = Request["plan_id"];
        string name = Request["plan_name"];
        TESTITEMREQ req = new TESTITEMREQ();
        req.dwGetType = (uint)TESTITEMREQ.DWGETTYPE.TESTITEMGET_BYPLANID;
        req.szGetKey = id;
        req.dwPlanKind = (uint)UNITESTPLAN.DWKIND.TESTPLANKIND_OPEN;
        UNITESTITEM[] rlt;
        if (m_Request.Reserve.GetTestItem(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            v = "<div id='panel_resv_" + id + "' class='resv_list'><div class='panel panel-default' style='margin-bottom:5px;'>" +
"<div class='panel-body' style='padding:10px 15px;'><span class='text-info panel_test_name'>计划：" + name + " 项目数：" + rlt.Length + "</span>" +
"</div><table class='table table-striped'><tbody>";
            for (int i = 0; i < rlt.Length; i++)
            {
                v += GetResv(rlt[i]);
            }
            if (rlt.Length == 0)
            {
                v += "<tr><td colspan='10'>无预约</td></tr>";
            }
            v += "</tbody></table></div></div>";
        }
        else
        {
            v += m_Request.szErrMsg;
        }
        SucRlt("\""+v+"\"");
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
            string time = "(第" + (int)tchl / 100000 + "周)【" + "星期" + week[(int)((tchl / 10000) % 10)] + "】第" + start + (start == end ? "" : ("-" + end)) + "节";
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