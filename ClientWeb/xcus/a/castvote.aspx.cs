using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_cg2_castvote : UniClientPage
{
    protected string voteList;
    UNIACCOUNT acc;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LoadPage())
        {
            acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
            GetCastVote();
        }
        else
        {
            MsgBoxH(Translate("未登录或登录超时，请重新登录"), "location.reload();");
        }
    }

    private void GetCastVote()
    {
        POLLONLINEREQ req = new POLLONLINEREQ();
        req.dwBeginDate = ToUInt(DateTime.Now.AddYears(-1).ToString("yyyyMMdd"));
        req.dwEndDate = ToUInt(DateTime.Now.AddYears(1).ToString("yyyyMMdd"));
        req.dwVoteStat = (uint)POLLONLINE.DWVOTESTAT.VOTESTAT_OPENING | (uint)POLLONLINE.DWVOTESTAT.VOTESTAT_DONE;
        POLLONLINE[] rlt;
        if (m_Request.Admin.GetPollOnLine(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            voteList = "";
            for (int i = 0; i < rlt.Length; i++)
            {
                POLLONLINE vote = rlt[i];
                bool isDone = (vote.dwVoteStat & (uint)POLLONLINE.DWVOTESTAT.VOTESTAT_DONE) > 0;
                voteList += "<tr class='it'><td style='padding:10px 5px;'>" + vote.szPollSubject + "</td>" +
                                        "<td style='font-size:12px;vertical-align:top;'>" + vote.szMemo + "</td>" +
                                        "<td class='text-center'>" +ToDate(vote.dwEndDate)+ "</td>" +
                                        "<td class='text-center'>" + vote.dwTotalUsers + "</td>" +
                                        "<td class='text-center'>" + ToState(vote.dwVoteStat) + "</td>" +
                                        "<td class='text-center'><a class='to-cast' url='../a/votedetail.aspx?back=true&type=poll&id=" + vote.dwPollID + "' cache='#cache_con'>"+(isDone?"查看":"投票")+"</a></td></tr>";
            }
        }
        else
        {
            MsgBoxH(m_Request.szErrMsg);
        }

    }
    private string ToKind(uint? sta)
    {
        if (sta == (uint)POLLONLINE.DWPOLLKIND.POLLKIND_MTICKM)
        {
            return "<span class='green'>单选</span>";
        }
        else if (sta == (uint)POLLONLINE.DWPOLLKIND.POLLKIND_MTICKS)
        {
            return "<span class='green'>多选</span>";
        }
        else if (sta == (uint)POLLONLINE.DWPOLLKIND.POLLKIND_SUBGROUP)
        {
            return "<span class='green'>分组</span>";
        }
        else
        {
            return "";
        }
    }
    private string ToState(uint? sta)
    {
        string ret = "";

        if((sta&(uint)POLLONLINE.DWVOTESTAT.VOTESTAT_CLOSED)>0){
            ret="<span class='grey'>已关闭</span>";
        }
        else if ((sta & (uint)POLLONLINE.DWVOTESTAT.VOTESTAT_DONE) > 0)
        {
            ret = "<span class='orange'>已投票</span>";
        }
        else if ((sta & (uint)POLLONLINE.DWVOTESTAT.VOTESTAT_OPENING) > 0)
        {
            ret = "<span class='green'>开放中</span>";
        }
        else if ((sta & (uint)POLLONLINE.DWVOTESTAT.VOTESTAT_UNOPEN) > 0)
        {
            ret = "<span class='red'>未开放</span>";
        }
        return ret;
    }
    //暂未用
    private string ToAct(POLLONLINE vote) {
        string ret = "";
        uint open=(uint)POLLONLINE.DWVOTESTAT.VOTESTAT_OPENING;
        uint done=(uint)POLLONLINE.DWVOTESTAT.VOTESTAT_DONE;
        if ((vote.dwVoteStat & (open | done)) > 0)
        {
            bool isDone = (vote.dwVoteStat & done) > 0;
            ret += "<a class='click' onclick='castVote(" + vote.dwPollID + ")'>"+(isDone?"查看":"投票")+"</a>";
            POLLITEM[] list = vote.PollItems;
            if (list != null && list.Length > 0)
            {
                ret += "<div class='hidden' id='vote_" + vote.dwPollID + "'><div class='vote_items'>"+
                    "<h3>"+vote.szPollSubject+"</h3>"+
                    "<p class='grey'>" + vote.szMemo + "</p>" +
                    "<ul vote='" + vote.dwPollID + "'>";
                for (int i = 0; i < list.Length; i++)
                {
                    POLLITEM item = list[i];
                    uint? max=vote.dwTotalUsers*item.dwMaxTickItems;
                    double rate=(double)item.dwVotes/(double)max*100;
                    ret += "<li><label class='"+(isDone?"grey":"avail")+"'><input name='vote_" + vote.dwPollID + "' class='vote_item' type='radio' value='" + item.dwItemID + "' style='display:"+(isDone?"none":"")+"'/>"
                     + item.szItemName+ "</label><div class='progress'><div class='progress-bar  progress-bar-success' role='progressbar' aria-valuenow='"
                        + item.dwVotes + "' aria-valuemin='0' aria-valuemax='" + max + "' style='width: " + rate + "%;'>"
                        + item.dwVotes + "</div></div></li>";
                }
                string dt = ToDate(vote.dwBeginDate) + " - " + ToDate(vote.dwEndDate);
                ret += "</ul><div class='grey text-right'>投票日期："+dt+"</div><div class='text-center'>"+(isDone?"":"<input type='button'' class='btn btn-info cast' value='提交'/>")+"</div></div></div>";
            }
        }
        else
        {
            ret = "<span class='grey'>不能投票</span>";
        }
        return ret;
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