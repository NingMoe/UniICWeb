using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using Util;

public partial class ClientWeb_xcus_all_kinddetail : UniClientPage
{
    string kindID;
    protected string CurKindName = "";
    protected string imgUrl = "";
    protected string feedback = "";
    protected string szPicZoom = "";
    protected string szPicPath = "";
    protected string isBack = "";
    protected string devProFactory = "";
    protected string devProPlace = "";
    protected string devNum = "";
    protected string devDate = "";
    protected string devUsers = "";
    protected string devModel = "";
    protected string devCam = "";
    protected string devCol = "";
    protected string deploy = "";
    protected string devSta = "";
    protected string noResv = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadPage();
        kindID = Request["kind"];
        string back = Request["back"];
        if (back == "false")//默认显示
            isBack = "none";
        if (string.IsNullOrEmpty(kindID)) return;
        InitKind(kindID);
    }

    private void InitKind(string id)
    {
        UNIDEVKIND kind = GetDevKind(ToUInt(id));
        if (kind.dwKindID != null)
        {
            //info
            devProFactory = ConvertStr(kind.szProducer);
            devProPlace = ConvertStr(kind.dwNationCode);//must custom
            CurKindName = kind.szKindName;
            //
            uint clsKind = ToUInt(GetConfig("openClsKind"));
            if (clsKind != 0 && !IsStat(kind.dwClassKind, clsKind))
            {
                noResv = "none";
                Cld.Disable = "true";//不使用
                return;
            }
            Cld.ClassKind = kind.dwClassKind.ToString();
            if (GetConfig("resvAllDay") == "1" && (kind.dwProperty & (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_LONGTERMRESV) > 0)
            {
                Cld.IsLong = "true";
            }
        }
        Cld.IsKind = "true";
        Cld.KindId = id;
        Cld.SrcType = "kind";
    }
    //未使用
    private void InitDevInfo(string id)
    {
        DEVREQ req = new DEVREQ();
        req.szKindIDs = id;
        UNIDEVICE[] rlt;
        if (m_Request.Device.Get(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            if (rlt.Length > 0)
            {
                UNIDEVICE dev = rlt[0];
                devNum = dev.szAssertSN;
                devModel = dev.szModel;
                devUsers = dev.dwMinUsers + (dev.dwMinUsers == dev.dwMaxUsers ? "" : " - " + dev.dwMaxUsers);
                if (dev.dwPurchaseDate != null && dev.dwPurchaseDate > 10000000)
                {
                    string str = dev.dwPurchaseDate.ToString();
                    devDate = str.Substring(0, 4) + "-" + str.Substring(4, 2) + "-" + str.Substring(6, 2);
                }
                devCam = dev.szCampusName;
                devCol = dev.szDeptName;
                deploy = dev.szExtInfo;
                devLoc.InnerHtml = dev.szLabName + " , " + dev.szRoomName;
                imgUrl = GetImg(dev.dwDevSN);
                //仪器状态
                if (Converter.GetDevStat(dev.dwDevStat))
                {
                    devSta = Converter.GetDevRunStat(dev.dwRunStat);
                }
                else
                {
                    devSta = "<span class='red'>" + Translate("仪器不可用") + "</span>";
                }
                //获取管理员
                devMan.InnerHtml = dev.szAttendantName;
                devCon.InnerHtml = dev.szAttendantTel;
                //初始化
                if (Request["disable"] == "true")
                {
                    noResv = "none";
                    Cld.Disable = "true";
                }
            }
        }


    }
}