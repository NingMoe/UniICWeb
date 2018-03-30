using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_edit : UniClientPage
{
    protected bool islogin = false;
    protected string itemList = "";
    protected uint editForSubsys;
    uint availMobile;
    protected void Page_Load(object sender, EventArgs e)
    {
        islogin = LoadPage();
        availMobile = ToUInt(GetConfig("availMobile"));
        editForSubsys = ToUInt(GetConfig("editForSubsys"));
        InitSubsys();
        InitRuleList();
    }

    private void InitRuleList()
    {
        string ruleType = GetConfig("needToKnow");
        if(ruleType == "3"){
            DEVKINDREQ req = new DEVKINDREQ();
            itemList += "<div class='line'></div><h2>类型预约须知</h2><ul>";
            UNIDEVKIND[] rlt;
            if (m_Request.Device.DevKindGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS && rlt != null && rlt.Length > 0)
            {
                for (int i = 0; i < rlt.Length; i++)
                {
                    UNIDEVKIND kind = rlt[i];
                    itemList += "<li>" + kind.szKindName + "(ID:" + kind.dwKindID + ")&nbsp;" +
                        "<a href='editcontent.aspx?name=" + Server.UrlEncode(kind.szKindName + "(预约须知)") + "&type=kind_rule&id=" + kind.dwKindID + "' target='_blank'>预约须知</a></li>";
                }
            }
            itemList += "</ul>";
        }
    }

    private void InitSubsys()
    {
        uint rscMode = ToUInt(GetConfig("resourceMode"));
        //不支持子系统
        if (rscMode == 32)
        {
            InitLab();//实验室
            return;
        }
        if (rscMode == 64)
        {
            InitYardAty();
            return;
        }

        uint clsKind = ToUInt(GetConfig("openClsKind"));
        if (clsKind == 0)//全部
            GetSubItem(0, rscMode);
        else
        {
            uint rm = (uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS;
            uint seat = (uint)UNIDEVCLS.DWKIND.CLSKIND_SEAT;
            uint cpt = (uint)UNIDEVCLS.DWKIND.CLSKIND_COMPUTER;
            uint loan = (uint)UNIDEVCLS.DWKIND.CLSKIND_LOAN;
            if ((clsKind & rm) > 0)//空间
            {
                uint rsc = ToUInt(GetConfig("subRmResourceMode"));
                string name = GetConfig("SysKindRoom");
                itemList += "<div class='line'></div><h2>" + (name == "" ? "空间" : name) + "</h5>";
                GetSubItem(rm, rsc > 0 ? rsc : rscMode);
            }
            if ((clsKind & seat) > 0)//座位
            {
                uint rsc = ToUInt(GetConfig("subSeatResourceMode"));
                string name = GetConfig("SysKindSeat");
                itemList += "<div class='line'></div><h2>" + (name == "" ? "座位" : name) + "</h5>";
                if ((editForSubsys & seat) > 0)
                {
                    itemList += "请使用：通用设置可在，其它-->座位子系统通用<br />";
                   
                }
                else
                    GetSubItem(seat, rsc > 0 ? rsc : rscMode);
            }
            if ((clsKind & cpt) > 0)//电子阅览室
            {
                uint rsc = ToUInt(GetConfig("subCptResourceMode"));
                string name = GetConfig("SysKindPC");
                itemList += "<div class='line'></div><h2>" + (name == "" ? "电子阅览室" : name) + "</h5>";
                if ((editForSubsys & cpt) > 0)
                    itemList += "请使用：其它-->电子阅览室子系统通用";
                else
                GetSubItem(cpt, rsc > 0 ? rsc : rscMode);
            }
            if ((clsKind & loan) > 0)//外借
            {
                uint rsc = ToUInt(GetConfig("subLoanResourceMode"));
                string name = GetConfig("SysKindLend");
                itemList += "<div class='line'></div><h2>" + (name == "" ? "外借设备" : name) + "</h5>";
                GetSubItem(loan, rsc > 0 ? rsc : rscMode);
            }
        }
    }

    private void GetSubItem(uint clsKind, uint mode)
    {
        //设备
        if (mode == 16)
            InitDev(clsKind);
        //房间 / 实验室 + 房间
        else if ((mode & 4) > 0)
            InitRoom(clsKind);
        else if ((mode & 2) > 0)
            InitKinds(clsKind);
        //设备类别 类别
        else
            InitDevCls(clsKind);
    }

    private void InitKinds(uint classkind)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        DEVKINDREQ req = new DEVKINDREQ();
        if (classkind != 0)
            req.dwClassKind = classkind;
        UNIDEVKIND[] rlt;
        itemList += "<ul>";
        uResponse = m_Request.Device.DevKindGet(req, out rlt);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && rlt != null && rlt.Length > 0)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                if (rlt[i].szMemo != null && rlt[i].szMemo == "false") continue;
                UNIDEVKIND kind = rlt[i];
                itemList += "<li>" + kind.szKindName + "(ID:" + kind.dwKindID + ")&nbsp;<a href='editcontent.aspx?name=" + Server.UrlEncode(kind.szKindName + "(介绍)") + "&type=kind_intro&id=" + kind.dwKindID + "' target='_blank'>介绍</a> / <a href='editcontent.aspx?name=" + Server.UrlEncode(kind.szKindName + "(硬件配置)") + "&type=kind_hard&id=" + kind.dwKindID + "' target='_blank'>硬件配置</a> / <a href='editcontent.aspx?name=" + Server.UrlEncode(kind.szKindName + "(相册)") + "&type=kind_slide&id=" + kind.dwKindID + "' target='_blank'>相册</a>" +
                    " / <a href='editcontent.aspx?name=" + Server.UrlEncode(kind.szKindName + "(预约须知)") + "&type=kind_rule&id=" + kind.dwKindID + "' target='_blank'>须知</a>" + ((availMobile & 5) > 0 ? " / <a href='editcontent.aspx?name=" + Server.UrlEncode(kind.szKindName + "(移动端介绍)") + "&type=kind_mIntro&id=" + kind.dwKindID + "' target='_blank'>(移动端)介绍</a>" : "") + "</li>";
            }
        }
        itemList += "</ul>";
    }

    private void InitYardAty()
    {
        YARDACTIVITYREQ req = new YARDACTIVITYREQ();
        YARDACTIVITY[] rlt;
        REQUESTCODE cd = m_Request.Reserve.GetYardActivity(req, out rlt);
        if (cd == REQUESTCODE.EXECUTE_SUCCESS)
        {
            itemList = "<div class='line'></div><h2>活动场景列表</h2><ul>";
            for (int i = 0; i < rlt.Length; i++)
            {
                YARDACTIVITY aty = rlt[i];
                itemList += "<li>" + aty.szActivityName + "(ID:" + aty.dwActivitySN + ")&nbsp;<a href='editcontent.aspx?name=" + Server.UrlEncode(aty.szActivityName + "(介绍)") + "&type=aty_intro&id=" + aty.dwActivitySN + "' target='_blank'>介绍</a>" +
                    " / <a href='editcontent.aspx?name=" + Server.UrlEncode(aty.szActivityName + "(预约规则)") + "&type=aty_rule&id=" + aty.dwActivitySN + "' target='_blank'>预约规则</a></li>";
            }
            itemList += "</ul>";
        }
    }
    private void InitLab()
    {
        LABREQ req = new LABREQ();
        UNILAB[] rlt;
        if (m_Request.Device.LabGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            itemList = "<div class='line'></div><h2>" + GetConfig("LabName") + "列表</h2><ul>";
            for (int i = 0; i < rlt.Length; i++)
            {
                UNILAB lab = rlt[i];
                itemList += "<li>" + lab.szLabName + "(ID:" + lab.dwLabID + ")&nbsp;<a href='editcontent.aspx?name=" + Server.UrlEncode(lab.szLabName + "(介绍)") + "&type=lab_intro&id=" + lab.dwLabID + "' target='_blank'>介绍</a> / <a href='editcontent.aspx?name=" + Server.UrlEncode(lab.szLabName + "(硬件配置)") + "&type=lab_hard&id=" + lab.dwLabID + "' target='_blank'>硬件配置</a> / <a href='editcontent.aspx?name=" + Server.UrlEncode(lab.szLabName + "(相册)") + "&type=lab_slide&id=" + lab.dwLabID + "' target='_blank'>相册</a>" +
                    " / <a href='editcontent.aspx?name=" + Server.UrlEncode(lab.szLabName + "(预约须知)") + "&type=lab_rule&id=" + lab.dwLabID + "' target='_blank'>须知</a>"+((availMobile&5)>0?" / <a href='editcontent.aspx?name=" + Server.UrlEncode(lab.szLabName + "(移动端介绍)") + "&type=lab_mIntro&id=" + lab.dwLabID + "' target='_blank'>(移动端)介绍</a>":"")+"</li>";
            }
            itemList += "</ul>";
        }
    }
    private void InitDev(uint classkind)
    {
        DEVREQ req = new DEVREQ();
        if (classkind != 0)
        req.dwClassKind = classkind;
        UNIDEVICE[] rlt;
        if (m_Request.Device.Get(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            itemList += "<ul>";
            for (int i = 0; i < rlt.Length; i++)
            {
                UNIDEVICE dev = rlt[i];
                itemList += "<li>" + dev.szDevName + "(ID:" + dev.dwDevID + ";SN:" + dev.dwDevSN + ")&nbsp;<a onclick='uploadFile({\"ren\":\"" + dev.dwDevSN + "\",\"dir\":\"../DevImg/\"},\"上传 " + dev.szDevName + "\")'>照片</a> / <a href='editcontent.aspx?name=" + Server.UrlEncode(dev.szDevName + "(相册)") + "&type=dev_slide&id=" + dev.dwDevID + "' target='_blank'>相册</a>" +
                    " / <a href='editcontent.aspx?name=" + Server.UrlEncode(dev.szDevName + "(预约须知)") + "&type=dev_rule&id=" + dev.dwDevID + "' target='_blank'>须知</a>"+((availMobile&5)>0?" / <a href='editcontent.aspx?name=" + Server.UrlEncode(dev.szDevName + "(移动端介绍)") + "&type=dev_mIntro&id=" + dev.dwDevID + "' target='_blank'>(移动端)介绍</a>":"")+"</li>";
            }
            itemList += "</ul>";
        }
    }
    private void InitRoom(uint classkind)
    {
        ROOMREQ req = new ROOMREQ();
        if (classkind != 0)
        req.dwInClassKind = classkind;
        UNIROOM[] rlt;
        if (m_Request.Device.RoomGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            itemList += "<ul>";
            for (int i = 0; i < rlt.Length; i++)
            {
                UNIROOM rm = rlt[i];
                itemList += "<li>" + rm.szRoomName + "(ID:" + rm.dwRoomID + ")&nbsp;<a href='editcontent.aspx?name=" + Server.UrlEncode(rm.szRoomName + "(介绍)") + "&type=rm_intro&id=" + rm.dwRoomID + "' target='_blank'>介绍</a> / <a href='editcontent.aspx?name=" + Server.UrlEncode(rm.szRoomName + "(硬件配置)") + "&type=rm_hard&id=" + rm.dwRoomID + "' target='_blank'>硬件配置</a> / <a href='editcontent.aspx?name=" + Server.UrlEncode(rm.szRoomName + "(相册)") + "&type=rm_slide&id=" + rm.dwRoomID + "' target='_blank'>相册</a>" +
                    " / <a href='editcontent.aspx?name=" + Server.UrlEncode(rm.szRoomName + "(预约须知)") + "&type=rm_rule&id=" + rm.dwRoomID + "' target='_blank'>须知</a>"+((availMobile&5)>0?" / <a href='editcontent.aspx?name=" + Server.UrlEncode(rm.szRoomName + "(移动端介绍)") + "&type=rm_mIntro&id=" + rm.dwRoomID + "' target='_blank'>(移动端)介绍</a>":"")+"</li>";
            }
            itemList += "</ul>";
        }
    }
    private void InitDevCls(uint classkind)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        DEVCLSREQ vrGet = new DEVCLSREQ();
        if(classkind!=0)
        vrGet.dwKind = classkind;
        UNIDEVCLS[] vtRes;
        itemList += "<ul>";
        uResponse = m_Request.Device.DevClsGet(vrGet, out vtRes);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            for (int i = 0; i < vtRes.Length; i++)
            {
                if (vtRes[i].szMemo != null && vtRes[i].szMemo == "false") continue;
                UNIDEVCLS cls = vtRes[i];
                itemList += "<li>" + cls.szClassName + "(ID:" + cls.dwClassID + ")&nbsp;<a href='editcontent.aspx?name=" + Server.UrlEncode(cls.szClassName + "(介绍)") + "&type=intro&id=" + cls.dwClassID + "' target='_blank'>介绍</a> / <a href='editcontent.aspx?name=" + Server.UrlEncode(cls.szClassName + "(硬件配置)") + "&type=hard&id=" + cls.dwClassID + "' target='_blank'>硬件配置</a> / <a href='editcontent.aspx?name=" + Server.UrlEncode(cls.szClassName + "(相册)") + "&type=slide&id=" + cls.dwClassID + "' target='_blank'>相册</a>" +
                    " / <a href='editcontent.aspx?name=" + Server.UrlEncode(cls.szClassName + "(预约须知)") + "&type=rule&id=" + cls.dwClassID + "' target='_blank'>须知</a>"+((availMobile&5)>0?" / <a href='editcontent.aspx?name=" + Server.UrlEncode(cls.szClassName + "(移动端介绍)") + "&type=mIntro&id=" + cls.dwClassID + "' target='_blank'>(移动端)介绍</a>":"")+"</li>";
            }
        }
        itemList += "</ul>";
    }
}