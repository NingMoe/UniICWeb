using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using Util;

public partial class ClientWeb_xcus_all_devdetail : UniClientPage
{
    string devID;
    protected string CurDevName = "";
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
        devID = Request["dev"];
        string back = Request["back"];
        if (back == "false")//默认显示
            isBack = "none";
        if (string.IsNullOrEmpty(devID)) return;
        InitDevInfo(devID);
        //InitFeedback(devID);
        InitXmlSlide(devID,"dev_slide",ref szPicZoom,ref szPicPath);
    }
    private void InitFeedback(string id)
    {
        USERFEEDBACKREQ req = new USERFEEDBACKREQ();
        req.dwDevID = ToUInt(id);
        req.szReqExtInfo.szOrderKey = "dwOccurTime";
        req.szReqExtInfo.szOrderMode = "DESC";
        req.dwBeginDate = ToUInt(DateTime.Now.AddMonths(-60).ToString("yyyyMMdd"));//五年记录
        req.dwEndDate = ToUInt(DateTime.Now.AddMonths(3).ToString("yyyyMMdd"));
        USERFEEDBACK[] rlt;
        if (m_Request.Admin.GetUserFeedback(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                USERFEEDBACK fb = rlt[i];
                YARDRESV resv = GetYardResvById(fb.dwResvID);
                string sc = "★★★★★";
                if (resv.dwResvID == null) continue;
                feedback += "<tr><td><ul><li class='title'><span class='text-primary'>" + resv.szResvName + "</span><span class='score'>" + sc.Substring(0, (int)fb.dwScore) + "</span><span class='grey pull-right'>" + Get1970Date((int)fb.dwOccurTime)+ "</span>"
                    + "</li><li class='feedback'><p>" + fb.szIntroInfo + "</p></li>" + (fb.szReplyInfo == "" ? "" : "<li class='replay'><div class='grey'>" + fb.szAnswerer + " &nbsp;" + Translate("回复") + "：</div><p>" + fb.szReplyInfo + "</p><div class='grey'>" + Util.Converter.UintToDateStr(fb.dwReplyDate) + "</div></li>")
                    + "<li class='detail'><span class='pull-right'>" + Translate("活动类型") + "：<span class='text-primary'>" + resv.szActivityName + "</span>" + Translate("活动时间") + "： <span class='grey'>" + Get1970Date((int)resv.dwBeginTime) + " -- " + Get1970Date((int)resv.dwEndTime) + "</span></span></li></ul></td></tr>";
            }
        }
        else
            MsgBoxH(m_Request.szErrMsg);
    }
    private void InitDevInfo(string id)
    {
        UNIDEVICE[] vtResult=GetDevById(id);
        if (vtResult == null || vtResult.Length == 0) return;
            UNIDEVICE dev = vtResult[0];
            UNIDEVKIND kind = GetDevKind(dev.dwKindID);
            devNum = dev.szAssertSN;
            devName.InnerText = dev.szDevName;
            CurDevName = dev.szDevName;
            devModel = dev.szModel;
            devUsers = dev.dwMinUsers+(dev.dwMinUsers==dev.dwMaxUsers?"":" - "+dev.dwMaxUsers);
            if (dev.dwPurchaseDate != null && dev.dwPurchaseDate > 10000000)
            {
                string str = dev.dwPurchaseDate.ToString();
                devDate = str.Substring(0, 4) + "-" + str.Substring(4, 2) + "-" + str.Substring(6, 2);
            }
            devProFactory = ConvertStr(kind.szProducer);
            devProPlace = ConvertStr(kind.dwNationCode);//must custom
            devCam = dev.szCampusName;
            devCol = dev.szDeptName;
            deploy = dev.szExtInfo;
            devLoc.InnerHtml = dev.szLabName + " , " + dev.szRoomName;
            imgUrl = GetImg(dev.dwDevSN);
            //预约信息
            UNIRESVRULE rule = GetDevRsvRule(dev.dwDevID.ToString());
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
            //GROUPMEMDETAIL[] mbs = GetMembers(dev.dwManGroupID);
            //if (mbs != null && mbs.Length > 0)
            //{
            //    for (int i = 0; i < mbs.Length; i++)
            //    {
            //        devMan.InnerHtml += mbs[i].szTrueName + "&nbsp;&nbsp;";
            //        devCon.InnerHtml += mbs[i].szHandPhone + "&nbsp;&nbsp;";
            //    }
            //}
            //else
            //{
                devMan.InnerHtml = dev.szAttendantName;
                devCon.InnerHtml = dev.szAttendantTel;
            //}
        //初始化
            if (Request["disable"] != "true")
                InitDev(dev);
            else
            {
                noResv = "none";
                Cld.Disable = "true";
            }
    }

    private void InitDev(UNIDEVICE dev)
    {
            UNIDEVKIND kind = GetDevKind(dev.dwKindID);
            if (kind.dwKindID != null)
            {
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
                if ((kind.dwProperty & (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_KINDRESV) > 0)
                {
                    Cld.IsKind = "true";
                }
            }
            Cld.Dev = dev.dwDevID.ToString();
            Cld.SrcType = "dev";
    }
}