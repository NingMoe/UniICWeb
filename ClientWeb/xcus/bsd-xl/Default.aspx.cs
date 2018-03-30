using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using Util;
using System.Text.RegularExpressions;

public partial class ClientWeb_Default : UniClientPage
{
    uint needLines = 7;
    protected string rUseTimes = "";
    protected string wUseTimes = "";
    protected string tooltip = "";
    protected string noticeList = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //base.LoadPage();
        IsLogined((uint)UNISTATION.DWSUBSYSSN.SUBSYS_LAB);
        //urlLogin();
        InitTooltip();
        //InitNewDevs();
        //InitDevMonthStat();
        InitNotice();
    }

    private void InitNotice()
    {
        noticeList = "";
        XmlCtrl.XmlInfo[] list = GetXmlInfoList("notice", 8);
        if (list != null && list.Length > 0)
        {
            for (int i = 0; i < list.Length; i++)
            {
                string title = list[i].title;
                string date = list[i].date;
                if (string.IsNullOrEmpty(date)) date = "0";
                DateTime dt = Util.Converter.StrToDate(date);
                noticeList+="<li date='"+date+"'>▪ <a href='ArticleList.aspx?id="+list[i].id+"&type="+list[i].type+"&title="+Server.UrlEncode("通知公告")+"'>"+CutStrT(title,30)+"</a><span class='f-fr'>"+dt.ToString("yyyy年MM月dd日 HH时mm分")+"</span></li>";
            }
        }
    }

    private bool urlLogin()
    {
        string szUrl = Request.Url.ToString();
        if (!string.IsNullOrEmpty(szUrl) && szUrl.IndexOf("verify") > 0)
        {
            string szPassword = "";
            string szLogonName = "";
            if (IsCheckLogin(szUrl, out szLogonName, out szPassword))
            {
                if (common.Login(szLogonName, szPassword))
                {
                    UNIACCOUNT curAcc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
                    if (curAcc.szEmail.ToString().Trim() == "" || curAcc.szHandPhone.ToString().Trim() == "")
                    {
                        MsgBox("新用户请先激活！");
                        common.ClearLogin();
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    private void InitTooltip()
    {
        if (Session["LOGIN_ACCINFO"] == null)
        {
            return;
        }
        UNIACCOUNT acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
        string msg;
        int num = 0;
        RESEARCHTEST[] rlt = GetrtList("rtest", out msg);
        if (rlt != null && rlt.Length > 0)
        {
            //临时方法，查询成员状态
            for (int j = 0; j < rlt.Length; j++)
            {
                RTMEMBER[] mbs = rlt[j].RTMembers;
                for (int m = 0; m < mbs.Length; m++)
                {
                    if (mbs[m].dwAccNo == acc.dwAccNo && ((mbs[m].dwStatus & 2) > 0))
                    {
                        num++;
                    }
                }
            }
        }
        tooltip += "<div class='tip'>已参与<span class='color3'> " + num + " </span>个科研项目。</div>";
    }
}