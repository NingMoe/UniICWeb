using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _SysLog : System.Web.UI.Page
{
    protected uint menuv = 1;
    protected uint mode = 1;
    protected uint viewtype = 0;
    protected MyString szOut = new MyString();
    protected uint filter = 0;
    protected string szStationStatOut = "";
    protected string szModuleStatOut = "";
    protected string szCmdStatOut = "";
    protected string szSDListOut = "";
    protected string szURLListOut = "";

    protected PageCtrl pageCtrl = new PageCtrl();

    uint nMaxDate = 99999999;
    uint nMinDate = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        pageCtrl.Page_Load(this, PageDiv);
        nMaxDate = 99999999;
        nMinDate = 0;

        GetMinMaxDate();
    }

    void GetMinMaxDate()
    {
        uint dateType = 1;
        uint.TryParse(ListViewDate.SelectedValue, out dateType);
        DateTime Max = DateTime.Now;
        DateTime Min = new DateTime(0);
        if (dateType == 1)
        {
            Max = DateTime.Now;
            Min = DateTime.Now;
        }
        else if (dateType == 2)
        {
            Min = DateTime.Now.AddDays(-7);
        }
        else if (dateType == 3)
        {
            Min = DateTime.Now.AddMonths(-1);
        }
        else if (dateType == 4)
        {
            Min = DateTime.Now.AddYears(-1);
        }
        nMaxDate = (uint)(Max.Year * 10000 + Max.Month * 100 + Max.Day);
        nMinDate = (uint)(Min.Year * 10000 + Min.Month * 100 + Min.Day);
    }

    protected override void OnPreRender(EventArgs e)
    {
        GetMinMaxDate();

        uint.TryParse(RBmenuList.SelectedValue, out menuv);
        if (menuv == 1)
        {
            SYSLOG_STATIONSTAT[] stastat = SysConsole.GetLogStationStat(nMinDate, nMaxDate);
            for (int i = 0; i < stastat.Length; i++)
            {
                szStationStatOut += "<tr>";
                if (string.IsNullOrEmpty(stastat[i].szStationSN))
                {
                    szStationStatOut += "<td>空</td>";
                }
                else
                {
                    szStationStatOut += "<td>" + stastat[i].szStationSN + "</td>";
                }
                szStationStatOut += "<td>" + stastat[i].dwOKCount + "</td>";
                szStationStatOut += "<td>" + stastat[i].dwErrorCount + "</td>";
                szStationStatOut += "<td>" + GetUseTime(stastat[i].dwTotalUseTime) + "</td>";
                szStationStatOut += "<td>" + GetMemoryStr(stastat[i].dwParamTotalSize) + "</td>";
                szStationStatOut += "<td>" + GetMemoryStr(stastat[i].dwResultTotalSize) + "</td>";
                szStationStatOut += "</tr>";
            }

            SYSLOG_CMDSTAT[] stat = SysConsole.GetLogCmdStat(nMinDate, nMaxDate, 1);
            for (int i = 0; i < stat.Length; i++)
            {
                szModuleStatOut += "<tr>";
                szModuleStatOut += "<td>" + GetModuleName(stat[i].dwCmdModule) + "</td>";
                szModuleStatOut += "<td>" + stat[i].dwOKCount + "</td>";
                szModuleStatOut += "<td>" + stat[i].dwErrorCount + "</td>";
                szModuleStatOut += "<td>" + GetUseTime(stat[i].dwTotalUseTime) + "</td>";
                szModuleStatOut += "<td>" + GetMemoryStr(stat[i].dwParamTotalSize) + "</td>";
                szModuleStatOut += "<td>" + GetMemoryStr(stat[i].dwResultTotalSize) + "</td>";
                szModuleStatOut += "<td>" + stat[i].dwStationCount + "</td>";
                szModuleStatOut += "</tr>";
            }

            SYSLOG_CMDSTAT[] cmdstat = SysConsole.GetLogCmdStat(nMinDate, nMaxDate, 0);
            for (int i = 0; i < cmdstat.Length; i++)
            {
                string szDefine;
                string szMemo;
                string szCmdName = GetCmdName(cmdstat[i].dwCmdModule, out szDefine, out szMemo);

                szCmdStatOut += "<tr>";
                szCmdStatOut += "<td>" + szCmdName + "</td>";
                szCmdStatOut += "<td>" + cmdstat[i].dwOKCount + "</td>";
                szCmdStatOut += "<td>" + cmdstat[i].dwErrorCount + "</td>";
                szCmdStatOut += "<td>" + GetUseTime(cmdstat[i].dwTotalUseTime) + "</td>";
                szCmdStatOut += "<td>" + GetUseTime(cmdstat[i].dwTotalUseTime / (cmdstat[i].dwOKCount + cmdstat[i].dwErrorCount)) + "</td>";
                szCmdStatOut += "<td>" + GetMemoryStr(cmdstat[i].dwParamTotalSize) + "</td>";
                szCmdStatOut += "<td>" + GetMemoryStr(cmdstat[i].dwResultTotalSize) + "</td>";
                szCmdStatOut += "<td>" + cmdstat[i].dwStationCount + "</td>";
                szCmdStatOut += "</tr>";
            }


            SessionData[] sdlist = SysConsole.GetSessionList();
            for (int i = 0; i < sdlist.Length; i++)
            {
                szSDListOut += "<tr>";
                szSDListOut += "<td>" + sdlist[i].szStationSN + "</td>";
                szSDListOut += "<td>" + sdlist[i].szSessionID + "</td>";
                szSDListOut += "<td>" + new DateTime(sdlist[i].tick).ToString() + "</td>";
                szSDListOut += "<td>" + sdlist[i].szUserName + "</td>";
                szSDListOut += "<td>" + sdlist[i].ip + "</td>";
                szSDListOut += "</tr>";
            }
        }
        else if (menuv == 2)
        {
            if (CheckBoxALL.Checked)
            {
                mode = 0;
            }
            else
            {
                mode = 1;
            }
            uint.TryParse(ListViewType.SelectedValue, out viewtype);
            uint.TryParse(FILTER_MODULE.SelectedValue, out filter);

            /*
            if (filter == 1)
            {
                filter = UniWebLib.PRModule.PUBEROOM_BASE;
            }
            else if (filter == 2)
            {
                filter = UniWebLib.PRModule.STATION_BASE;
            }
            else if (filter == 3)
            {
                filter = UniWebLib.PRModule.DEVICE_BASE;
            }
            else if (filter == 4)
            {
                filter = UniWebLib.PRModule.ADMIN_BASE;
            }
            else if (filter == 5)
            {
                filter = UniWebLib.PRModule.RCNAVI_BASE;
            }
            else if (filter == 6)
            {
                filter = UniWebLib.PRModule.CONTROL_BASE;
            }
            else if (filter == 7)
            {
                filter = UniWebLib.PRModule.USERULE_BASE;
            }
            else if (filter == 8)
            {
                filter = UniWebLib.PRModule.ACCOUNT_BASE;
            }
            else if (filter == 9)
            {
                filter = UniWebLib.PRModule.CARD_BASE;
            }*/

            if (viewtype == 1)
            {
                OnPreRender_Icon();
            }
            else if (viewtype == 2)
            {
                OnPreRender_Table();
            }
        }
        else if (menuv == 3)
        {
            uint rs = 1;
            uint.TryParse(ListViewRS.SelectedValue, out rs);

            SYSLOG_URLSTAT[] urlstat = SysConsole.GetLogURLStat(nMinDate, nMaxDate, rs);
            for (int i = 0; i < urlstat.Length; i++)
            {
                szURLListOut += "<tr>";
                szURLListOut += "<td>" + urlstat[i].szURL + "</td>";
                szURLListOut += "<td>" + urlstat[i].dwCount + "</td>";
                szURLListOut += "<td>" + GetUseTime(urlstat[i].dwAvgUseTime) + "</td>";
                szURLListOut += "<td>" + urlstat[i].dwIndex + "</td>";
                szURLListOut += "<td>" + urlstat[i].szMemo + "</td>";
                szURLListOut += "</tr>";
            }
        }

        base.OnPreRender(e);
    }

    void OnPreRender_Table()
    {
        SYSLOG[] logs = SysConsole.GetLog(nMinDate, nMaxDate, mode, filter, ref pageCtrl.m_ext);

        if (logs != null)
        {
            for (int i = 0; i < logs.Length; i++)
            {
                string szDefine;
                string szMemo;
                string szCmdName = GetCmdName(logs[i].dwCmd, out szDefine, out szMemo);
                if (logs[i].dwRetCode != 0)
                {
                    szOut += "<tr class='S_" + logs[i].szSessionID + " error'>";
                }
                else
                {
                    szOut += "<tr class='S_" + logs[i].szSessionID + "'>";
                }
                szOut += "<td>" + logs[i].dwID + "</td>";
                szOut += "<td>" + GetDateStr((uint)logs[i].dwDate) + " " + GetTimeStr((uint)logs[i].dwTime) + "</td>";
                szOut += "<td>" + szCmdName + "</td>";
                szOut += "<td>" + szMemo + "</td>";
                szOut += "<td>" + GetCode(logs[i].dwRetCode) + "</td>";
                szOut += "<td>" + GetUseTime((long)logs[i].dwUseTime) + "</td>";
                szOut += "<td>" + logs[i].szMessage + "</td>";
                szOut += "<td>" + GetMemoryStr(logs[i].dwParamSize) + "</td>";
                szOut += "<td>" + GetMemoryStr(logs[i].dwResultSize) + "</td>";
                szOut += "<td>" + logs[i].szStationSN + "</td>";
                szOut += "<td>" + logs[i].szSessionID + "</td></tr>";
            }
        }
    }

    void OnPreRender_Icon()
    {
        SYSLOG[] logs = SysConsole.GetLog(nMinDate, nMaxDate, mode, filter, ref pageCtrl.m_ext);

        if (logs != null)
        {
            for (int i = 0; i < logs.Length; i++)
            {
                string szDefine;
                string szMemo;
                string szCmdName = GetCmdName(logs[i].dwCmd, out szDefine, out szMemo);
                if (logs[i].dwRetCode != 0)
                {
                    szOut += "<div class='Icon IconError' ";
                }
                else
                {
                    szOut += "<div class='Icon IconOK' ";
                }
                szOut += " title='日期: " + GetDateStr((uint)logs[i].dwDate) + " " + GetTimeStr((uint)logs[i].dwTime) + "\n站点: " + logs[i].szStationSN + "\n接口: " + szMemo + "\n描述: " + logs[i].szMessage + "'>" + szCmdName + "</div> ";
            }
        }
    }

    protected void ButtonRefresh_Click(object sender, EventArgs e)
    {
        SysConsole.ReleaseConn();
    }

    protected void ButtonClear_Click(object sender, EventArgs e)
    {
        GetMinMaxDate();
        SysConsole.ClearLog(nMinDate, nMaxDate);
    }
    protected void ListViewType_SelectedIndexChanged(object sender, EventArgs e)
    {
        uint.TryParse(ListViewType.SelectedValue, out viewtype);
        if (viewtype == 1)
        {
            pageCtrl.List_PageItemCount.SelectedIndex = 5;
        }
        else if (viewtype == 2)
        {
            pageCtrl.List_PageItemCount.SelectedIndex = 1;
        }
    }
    protected void ListViewRS_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void ListViewDate_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    

    public void Alert(string paramAction, string parmaName)
    {
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        string strCheckInput = "<script>alert('" + paramAction + "')</script>";
        if (!cs.IsStartupScriptRegistered(cstype, parmaName))
        {
            cs.RegisterStartupScript(cstype, parmaName, strCheckInput);
        }
    }

    public string GetDateStr(uint? nDate)
    {
        if (nDate == null || nDate == 0)
        {
            return "";
        }
        string szDate = (nDate / 10000).ToString() + "-" + string.Format("{0:D2}", ((nDate / 100) % 100)) + "-" + string.Format("{0:D2}", (nDate % 100));
        return szDate;
    }


    string GetTimeStr(uint nTime)
    {
        string szTime = string.Format("{0:D2}", nTime / 10000) + ":" + string.Format("{0:D2}", (nTime / 100) % 100) + ":" + string.Format("{0:D2}", nTime % 100);
        return szTime;
    }

    string GetMemoryStr(long nSize)
    {
        string szSize = "";
        if (nSize >= 715827883)//GB
        {
            double nFB = ((double)nSize / 1073741824);
            szSize += nFB.ToString("f2") +"GB";
        }
        else if (nSize >= 699051)//MB
        {
            double nFB = ((double)nSize / 1048576);
            szSize += nFB.ToString("f2") + "MB";
        }
        else if (nSize >= 512)//KB
        {
            double nFB = ((double)nSize / 1024);
            szSize += nFB.ToString("f2") + "KB";
        }
        else
        {
            szSize = nSize + "B";
        }
        return szSize;
    }

    string GetUseTime(long dwUseTime)
    {
        if (dwUseTime == 0) return "0";
        string szTime = "";
        if (dwUseTime > 60000)
        {
            double nFT = ((double)dwUseTime / 60000);
            szTime += nFT.ToString("f1") + "分钟";
        }
        else if (dwUseTime > 1000)
        {
            double nFT = ((double)dwUseTime / 1000);
            szTime += nFT.ToString("f1") + "秒";
        }
        else
        {
            szTime += dwUseTime + "毫秒";
        }
        return szTime;
    }

    string GetCode(uint dwRetCode)
    {
        string szCode = "";
        if (dwRetCode == 0)
        {
            return "<font color='green'>成功</font>";
        }
            /*
        else if (dwRetCode == (uint)REQUESTCODE.ERR_IMPORT)
        {
            szCode = "导入出错";
        }
        else if (dwRetCode == (uint)REQUESTCODE.ERR_REQ_CONFLICT)
        {
            szCode = "所做的操作的其它冲突";
        }
        else if (dwRetCode == (uint)REQUESTCODE.ERR_REQ_VERNOMATCH)
        {
            szCode = "版本不匹配";
        }
        else if (dwRetCode == (uint)REQUESTCODE.ERR_REQ_PWERR)
        {
            szCode = "密码不正确";
        }
        else if (dwRetCode == (uint)REQUESTCODE.ERR_REQ_STRUCT)
        {
            szCode = "请求包错误";
        }
        else if (dwRetCode == (uint)REQUESTCODE.ERR_REQ_PARA)
        {
            szCode = "请求包参数错误";
        }
        else if (dwRetCode == (uint)REQUESTCODE.ERR_REQ_NONE)
        {
            szCode = "所请求的对象不存在";
        }
             * */
        else
        {
            szCode = "错误";
        }
        return "<font class='errcode'>" + szCode + "</font>";
    }

    string GetModuleName(uint dwCmd)
    {
        dwCmd = dwCmd & 0xffff0000;
        /*
        if (dwCmd == (uint)UniWebLib.PRModule.PUBEROOM_BASE)
        {
            return "PubERoom";
        }
        else if (dwCmd == (uint)UniWebLib.PRModule.STATION_BASE)
        {
            return "Station";
        }
        else if (dwCmd == (uint)UniWebLib.PRModule.DEVICE_BASE)
        {
            return "Device";
        }
        else if (dwCmd == (uint)UniWebLib.PRModule.ADMIN_BASE)
        {
            return "Admin";
        }
        else if (dwCmd == (uint)UniWebLib.PRModule.RCNAVI_BASE)
        {
            return "Rcnavi";
        }
        else if (dwCmd == (uint)UniWebLib.PRModule.CONTROL_BASE)
        {
            return "Control";
        }
        else if (dwCmd == (uint)UniWebLib.PRModule.USERULE_BASE)
        {
            return "UseRule";
        }
        else if (dwCmd == (uint)UniWebLib.PRModule.ACCOUNT_BASE)
        {
            return "Account";
        }
        else if (dwCmd == (uint)UniWebLib.PRModule.CARD_BASE)
        {
            return "Card";
        }
        else*/
        {
            return "";
        }
    }

    string GetCmdName(uint dwCmd, out string szDefine, out string szMemo)
    {
        szDefine = "";
        szMemo = "";
        /*
        uint nCount = (uint)CmdList.g_cmdlist.Length - 1;
        for (int i = 0; i < nCount; i += 4)
        {
            object v = CmdList.g_cmdlist[i];
            Type t = v.GetType();
            uint nV = 0;
            if (t == typeof(int))
            {
                nV = (uint)(int)v;
            }
            else if (t == typeof(uint))
            {
                nV = (uint)v;
            }
            else if (t == typeof(long))
            {
                nV = (uint)(long)v;
            }
            else
            {
                continue;
            }
            if (nV == dwCmd)
            {
                string szCmd = (string)CmdList.g_cmdlist[i + 1];
                szDefine = (string)CmdList.g_cmdlist[i + 2];
                szMemo = (string)CmdList.g_cmdlist[i + 3];
                return szCmd;
            }
        }*/
        return dwCmd.ToString();
    }
    protected void FILTER_MODULE_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void CheckBoxALL_CheckedChanged(object sender, EventArgs e)
    {

    }
}

public class PageCtrl
{
    ///   <summary>   
    ///   分页参数,只能在Page的OnPreRender里被使用
    ///   </summary> 
    public ParamExt m_ext = new ParamExt();

    ///   <summary>   
    ///   初始化PageCtrl,在Page的Page_Load函数里被调用
    ///   </summary> 
    public void Page_Load(System.Web.UI.Page _page, Control ParentControl)
    {
        page = _page;
        LinkButton_FirstPage.Text = "首页";
        LinkButton_Pre.Text = "上一页";
        LinkButton_Next.Text = "下一页";
        LinkButton_LastPage.Text = "尾页";
        Label_CurPageText.Text = "当前页:";
        Label_CurPage.Text = "0";
        Label_TotalPageText.Text = ",&nbsp; 总页数:";
        Label_TotalPage.Text = "0";
        List_PageItemCount.Items.Add(new ListItem("每页10条", "10"));
        List_PageItemCount.Items.Add(new ListItem("每页20条", "20"));
        List_PageItemCount.Items.Add(new ListItem("每页30条", "30"));
        List_PageItemCount.Items.Add(new ListItem("每页40条", "40"));
        List_PageItemCount.Items.Add(new ListItem("每页50条", "50"));
        List_PageItemCount.Items.Add(new ListItem("每页100条", "100"));
        List_PageItemCount.Items.Add(new ListItem("每页200条", "200"));
        List_PageItemCount.Items.Add(new ListItem("每页500条", "500"));
        List_PageItemCount.Items.Add(new ListItem("每页1000条", "1000"));
        List_PageItemCount.SelectedIndex = 5;
        List_PageItemCount.AutoPostBack = true;

        orderField.ID = "orderField";
        orderMode.ID = "orderMode";

        LinkButton_FirstPage.CssClass = "PageCtrl";
        LinkButton_Pre.CssClass = "PageCtrl";
        LinkButton_Next.CssClass = "PageCtrl";
        LinkButton_LastPage.CssClass = "PageCtrl";

        LinkButton_FirstPage.Click += new EventHandler(OnFirst);
        LinkButton_Pre.Click += new EventHandler(OnPre);
        LinkButton_Next.Click += new EventHandler(OnNext);
        LinkButton_LastPage.Click += new EventHandler(OnLast);
        List_PageItemCount.SelectedIndexChanged += new EventHandler(OnPageCountChanged);

        ParentControl.Controls.Add(LinkButton_FirstPage);
        ParentControl.Controls.Add(LinkButton_Pre);
        ParentControl.Controls.Add(LinkButton_Next);
        ParentControl.Controls.Add(LinkButton_LastPage);
        ParentControl.Controls.Add(List_PageItemCount);
        ParentControl.Controls.Add(Label_CurPageText);
        ParentControl.Controls.Add(Label_CurPage);
        ParentControl.Controls.Add(Label_TotalPageText);
        ParentControl.Controls.Add(Label_TotalPage);
        ParentControl.Controls.Add(orderField);
        ParentControl.Controls.Add(orderMode);

        page.LoadComplete += new EventHandler(OnLoadComplete);
        page.PreRenderComplete += new EventHandler(OnPreRenderComplete);


        Type cstype = page.GetType();
        ClientScriptManager cs = page.ClientScript;
        string strCheckInput =
        @"<script type='text/javascript'>$(function(){
            var ths = $('thead th')
            ths.click(function(){
                var szID = $(this).attr('id');
                if(!szID || szID == '')
                {
                    return;
                }
                if(__doPostBack)
                {
                    __doPostBack('ChangePageOrder',szID);
                }
            });
            ths.html(function(nindex,szhtml){
	            var thisID = $(this).attr('id');
	            if(thisID != null && thisID != '')
	            {
	                if(thisID == $('#orderField').val())
	                {
	                    if($('#orderMode').val() == 'asc')
	                    {
                            return '<div class=\'canOrder\'><img src=\'UI_themes/images/icons/list_px_up.gif\'/>' + szhtml+'</div>';  //当前排序行，正序
	                    }else{
                            return '<div class=\'canOrder\'><img src=\'UI_themes/images/icons/list_px_down.gif\'/>' + szhtml+'</div>';  //当前排序行，侄序
	                    }
	                }else{
                        return '<div class=\'canOrder\'><img src=\'UI_themes/images/icons/list_px.gif\'/>' + szhtml+'</div>';  //可排序行
	                }
                }else{
                    return szhtml;  //不能排序行，原样输出
                }
            });
        });</script>";

        if (!cs.IsStartupScriptRegistered(cstype, "PageCtrl_tableInit"))
        {
            cs.RegisterStartupScript(cstype, "PageCtrl_tableInit", strCheckInput);
        }
    }
    System.Web.UI.Page page;
    HiddenField orderField = new HiddenField();
    HiddenField orderMode = new HiddenField();
    LinkButton LinkButton_FirstPage = new LinkButton();
    LinkButton LinkButton_Pre = new LinkButton();
    LinkButton LinkButton_Next = new LinkButton();
    LinkButton LinkButton_LastPage = new LinkButton();
    public DropDownList List_PageItemCount = new DropDownList();
    Label Label_CurPageText = new Label();
    Label Label_CurPage = new Label();
    Label Label_TotalPageText = new Label();
    Label Label_TotalPage = new Label();

    void OnLoadComplete(object sender, EventArgs e)
    {
        OnRenderBegin();
    }
    void OnPreRenderComplete(object sender, EventArgs e)
    {
        OnRenderEnd();
    }

    void OnRenderBegin()
    {
        uint nCurPage = 0;
        m_ext.nPageCount = 10;

        uint.TryParse(Label_CurPage.Text, out nCurPage);
        uint.TryParse(List_PageItemCount.SelectedValue, out m_ext.nPageCount);

        if (nCurPage < 1) nCurPage = 1;

        m_ext.nStartPage = (nCurPage - 1) * m_ext.nPageCount;


        m_ext.szOrderField = orderField.Value;
        m_ext.szOrder = orderMode.Value;
        if (page.IsPostBack)
        {
            if (page.Request["__EVENTTARGET"] == "ChangePageOrder")
            {
                if (m_ext.szOrderField == page.Request["__EVENTARGUMENT"])
                {
                    if (m_ext.szOrder == "asc")
                    {
                        m_ext.szOrder = "desc";
                    }
                    else
                    {
                        m_ext.szOrder = "asc";
                    }
                }
                else
                {
                    m_ext.szOrder = "asc";
                    m_ext.szOrderField = page.Request["__EVENTARGUMENT"];
                }
                orderField.Value = m_ext.szOrderField;
                orderMode.Value = m_ext.szOrder;
            }
        }
    }

    void OnRenderEnd()
    {
        uint nCurPage = (m_ext.nStartPage / m_ext.nPageCount) + 1;
        uint nTotalPage = (m_ext.nTotalCount + m_ext.nPageCount - 1) / m_ext.nPageCount;
        if (nCurPage <= 1)
        {
            LinkButton_Pre.Enabled = false;
            LinkButton_FirstPage.Enabled = false;
        }
        else
        {
            LinkButton_Pre.Enabled = true;
            LinkButton_FirstPage.Enabled = true;
        }
        if (nCurPage >= nTotalPage)
        {
            nCurPage = nTotalPage;
            LinkButton_Next.Enabled = false;
            LinkButton_LastPage.Enabled = false;
        }
        else
        {
            LinkButton_Next.Enabled = true;
            LinkButton_LastPage.Enabled = true;
        }
        Label_CurPage.Text = nCurPage.ToString();
        Label_TotalPage.Text = nTotalPage.ToString();
    }

    void OnPre(object sender, EventArgs e)
    {
        uint nCurPage = 0;
        uint.TryParse(Label_CurPage.Text, out nCurPage);
        nCurPage--;
        if (nCurPage < 0) nCurPage = 0;
        Label_CurPage.Text = nCurPage.ToString();
    }
    void OnNext(object sender, EventArgs e)
    {
        uint nCurPage = 0;
        uint.TryParse(Label_CurPage.Text, out nCurPage);
        nCurPage++;
        Label_CurPage.Text = nCurPage.ToString();
    }
    void OnFirst(object sender, EventArgs e)
    {
        Label_CurPage.Text = "1";
    }
    void OnLast(object sender, EventArgs e)
    {
        Label_CurPage.Text = Label_TotalPage.Text;
    }
    void OnPageCountChanged(object sender, EventArgs e)
    {
        Label_CurPage.Text = "1";
    }
}