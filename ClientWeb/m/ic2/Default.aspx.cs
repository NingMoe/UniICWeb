using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_m_xcus_ic2_Default : UniClientPage
{
    protected string itemList = "";
    protected uint clsKind;
    protected uint quickResv;
    protected string rmName;
    protected string seatName;
    protected string cptName;
    protected string loanName;
    protected bool isMark = false;
    protected bool reLogin = false;
    public string schoolCode = "";
    public string aluserid = "";
    public string wxuserid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
       
        aluserid = Request["userid"];
        wxuserid = Request["wxuserid"];
        schoolCode = Request["schoolCode"];

        //Session["language"] = "en-gb";
        quickResv = ToUInt(GetConfig("quickResv"));
        if (LoadPage())
        {
            ADMINLOGINRES res = (ADMINLOGINRES)Session["LoginRes"];
            if((res.dwManRole&(uint)ADMINLOGINRES.DWMANROLE.MANEXT_HP)==0)
                reLogin = true;//非手机端则重登录
            UNIACCOUNT acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
            if ((ToUInt(GetConfig("availMobile")) & 8) > 0)
            {
                if ((acc.dwIdent & (uint)UNIACCOUNT.DWIDENT.EXTIDENT_MANAGER) > 0)
                    isMark = true;
            }
        }
        else if (GetConfig("loginAll") == "1")
        {//上财pad屏幕要求单点登录，临时注释
            //Response.Redirect("~/loginMAll.aspx");
            Response.Redirect("~/loginAll.aspx");
        }
        InitSubsys();
    }

    private void InitSubsys()
    {
        //子系统
        clsKind = ToUInt(GetConfig("openClsKind"));
        string szSysName = Request["syskind"];
        if (!string.IsNullOrEmpty(szSysName))
        {
            uint clsKindTemp = uint.Parse(szSysName);
            if (clsKindTemp > 0)
            {
                clsKind = clsKindTemp;
            }
        }
        if (clsKind != 0)
        {
                 rmName= GetConfig("SysKindRoom");
                seatName = GetConfig("SysKindSeat");
                cptName = GetConfig("SysKindPC");
                loanName = GetConfig("SysKindLend");
        }
        else
        {
            itemList += GetDevCls(0);
        }
    }
    //private void InitItemCls()
    //{
    //    //设备
    //    if ((rscMode & 16) > 0)
    //        itemList += GetDev();
    //    //实验室
    //    if ((rscMode & 32) > 0)
    //        itemList += GetLab();
    //    //设备类别 类别
    //    if ((rscMode & 1) > 0)
    //        itemList += GetFullDevCls();
    //}

    private string GetLab()
    {
        LABREQ req = new LABREQ();
        UNILAB[] rlt;
        string ret = "";//<div class='content-block-title'>根据 <span class='theme-color'>区域</span> 查找</div>
        if (m_Request.Device.LabGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            ret += "<div class='list-block'><ul>";
            for (int i = 0; i < rlt.Length; i++)
            {
                ret += "<li><a class='item-content item-link' href=\"../a/labdetail.aspx?lab_id=" + rlt[i].dwLabID + "&name=" + Server.UrlEncode(rlt[i].szLabName) + "\"><div class='item-inner'><div class='item-title'>" + rlt[i].szLabName + "</div><div class='item-after'>详细</div></div></a></li>";
            }
            ret += "</ul></div>";
        }
        return ret;
    }

    private string GetDev()
    {
        uint classkind = ToUInt(GetConfig("openClsKind"));
        DEVREQ req = new DEVREQ();
        if (classkind != 0)
            req.dwClassKind = classkind;
        UNIDEVICE[] rlt;
        string ret = "";//<div class='content-block-title'>预约 <span class='theme-color'>对象</span> 列表</div>
        if (m_Request.Device.Get(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            ret += "<div class='list-block'><ul>";
            for (int i = 0; i < rlt.Length; i++)
            {
                UNIDEVICE dev = rlt[i];
                if (IsStat(dev.dwProperty, (uint)UNIDEVICE.DWPROPERTY.DEVPROP_NORESV)) continue;
                ret += "<li><a class='item-content item-link' href=\"../a/devdetail.aspx?back=false&dev=" + dev.dwDevID + "&sn=" + dev.dwDevSN + "\"><div class='item-inner'><div class='item-title'>" + dev.szDevName + "</div><div class='item-after'>详细</div></div></a></li>";
            }
            ret += "</ul></div>";
        }
        return ret;
    }

    //private string GetFullDevCls()
    //{
    //    string ret = "<div class='list-block'><ul>";//<div class='content-block-title'>根据 <span class='theme-color'>类型</span> 查找</div>
    //    if (target == 0)
    //    {
    //        uint clsKind = ToUInt(GetConfig("openClsKind"));
    //        if (clsKind == 0)
    //        {
    //            ret += GetDevCls(0);
    //        }
    //        else
    //        {
    //            if ((clsKind & 1) > 0)
    //                ret += GetDevCls((uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS);
    //            if ((clsKind & 2) > 0)
    //                ret += GetDevCls((uint)UNIDEVCLS.DWKIND.CLSKIND_COMPUTER);
    //            if ((clsKind & 4) > 0)
    //                ret += GetDevCls((uint)UNIDEVCLS.DWKIND.CLSKIND_LOAN);
    //            if ((clsKind & 8) > 0)
    //                ret += GetDevCls((uint)UNIDEVCLS.DWKIND.CLSKIND_SEAT);
    //        }
    //    }
    //    else
    //    {
    //        ret += GetDevCls(0);
    //    }
    //    ret += "</ul></div>";
    //    return ret;
    //}
    private string GetDevCls(uint kind)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        DEVCLSREQ vrGet = new DEVCLSREQ();
        if (kind != 0)
            vrGet.dwKind = kind;
        UNIDEVCLS[] vtRes;
        string ret = "";
        uResponse = m_Request.Device.DevClsGet(vrGet, out vtRes);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes.Length > 0)
        {
            for (int i = 0; i < vtRes.Length; i++)
            {
                if (vtRes[i].szMemo != null && vtRes[i].szMemo == "false")
                {
                    continue;
                }
                ret += "<li><a class='item-content item-link' href=\"../a/resvstat.aspx?classkind=" + vtRes[i].dwKind + "&class_id=" + vtRes[i].dwClassID +
                    "&name=" + Server.UrlEncode(vtRes[i].szClassName) + "\"><div class='item-inner'><div class='item-title'>" + vtRes[i].szClassName + "</div><div class='item-after'>详细</div></div></a></li>";
            }
        }
        return ret;
    }

    protected void imgcn_Click(object sender, EventArgs e)
    {
        Session["language"] = "zh-cn";
        currentLan.Value = "zh-cn";
    }

    protected void imgus_Click(object sender, EventArgs e)
    {
        Session["language"] = "en-gb";
        currentLan.Value = "en-us";
    }
}