using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_all_votedetail : UniClientPage
{
    protected string isBack = "none";
    protected string voteHeader = "";
    protected string voteItems = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        GetDetail();
        GetCastVote();
    }

    private void GetCastVote()
    {
        POLLONLINEREQ req = new POLLONLINEREQ();
        req.dwPollID = ToUInt(Request["id"]);
        POLLONLINE[] rlt;
        if (m_Request.Admin.GetPollOnLine(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            if (rlt.Length > 0)
            {
                POLLONLINE vote = rlt[0];
                voteHeader = "<h1 class='h_title'>投票信息</h1><div class='line'></div><h3 class='text-center'>" + vote.szPollSubject + "</h3>";
                uint done = (uint)POLLONLINE.DWVOTESTAT.VOTESTAT_DONE | (uint)POLLONLINE.DWVOTESTAT.VOTESTAT_CLOSED | (uint)POLLONLINE.DWVOTESTAT.VOTESTAT_UNOPEN;
                bool isDone = (vote.dwVoteStat & done) > 0;
                voteItems = "<a class='click' onclick='castVote(" + vote.dwPollID + ")'>" + (isDone ? "查看" : "投票") + "</a>";
                POLLITEM[] list = vote.PollItems;
                if (list != null && list.Length > 0)
                {
                    voteItems = "<div id='vote_" + vote.dwPollID + "'><div class='vote_items'>" +
                        "<p class='grey'>" + vote.szMemo + "</p><ul vote='" + vote.dwPollID + "'>";
                    for (int i = 0; i < list.Length; i++)
                    {
                        POLLITEM item = list[i];
                        uint? max = vote.dwTotalUsers * item.dwMaxTickItems;
                        double rate = (double)item.dwVotes / (double)max * 100;
                        voteItems += "<li><label class='" + (isDone ? "grey" : "avail") + "'><input name='vote_" + vote.dwPollID + "' class='vote_item' type='radio' value='" + item.dwItemID + "' style='display:" + (isDone ? "none" : "") + "'/>"
                         + item.szItemName + "</label><div class='progress'><div class='progress-bar  progress-bar-success' role='progressbar' aria-valuenow='"
                            + item.dwVotes + "' aria-valuemin='0' aria-valuemax='" + max + "' style='width: " + rate + "%;'>"
                            + item.dwVotes + "</div></div></li>";
                    }
                    string dt = ToDate(vote.dwBeginDate) + " - " + ToDate(vote.dwEndDate);
                    voteItems += "</ul><div class='grey text-right'>投票日期：" + dt + "</div><div class='text-center'>" + (isDone ? "" : "<input type='button'' class='btn btn-info' value='提交' onclick='castVote("+vote.dwPollID+")'/>") + "</div></div></div>";
                }
            }
        }
        else
        {
            MsgBoxH(m_Request.szErrMsg);
        }
    }

    private void GetDetail()
    {
        string id = Request["id"];
        string type = Request["type"];
        string back = Request["back"];
            if (back == "true")//默认隐藏
                isBack = "";
        if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(type))
        {
            XmlCtrl.XmlInfo info = GetXmlInfo(id, type);
            divDetail.InnerHtml = info.content;
        }
    }
    private string ToDate(uint? dt)
    {
        if (dt == null) return "";
        uint? y = dt / 10000;
        uint? m = (dt / 100) % 100;
        uint? d = dt % 100;
        return y + "/" + m + "/" + d;
    }
}