using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Xml;
using UniWebLib;
using UniStruct;
using Util;

public partial class DevDetail : UniClientPage
{
    protected uint devID;
    protected string imgUrl = "";
    protected string pagePosition = "";
    protected string CurDevName = "";
    protected string CurUserName = "匿名";
    protected string CurUserID = "";
    protected string devLab = "";
    protected string devMan = "";
    protected string useRole = "y";
    protected string sfroleInfo = "";
    protected string enable = "true";
    protected string detailIntro = "";
    uint? labID;
    protected UNIACCOUNT curAcc = new UNIACCOUNT();
    protected void Page_Load(object sender, EventArgs e)
    {
        base.LoadPage();
        if (Request["dev"] == null) return;
        devID = Convert.ToUInt32(Request["dev"]);
        if (Session["LOGIN_ACCINFO"] != null)
        {
            sfroleInfo = "预约请在日程表相应时间处点击或拖拽";
            UNIACCOUNT acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
            CurUserName = acc.szTrueName;
            CurUserID = acc.szLogonName;
            curAcc = acc;
            InitSFRole();
        }
        else
            sfroleInfo = "预约请先登录 <a onclick='isloginL();'>点击登录</a>";
        InitDevInfo(devID);
        InitAchieve();
    }

    private void InitSFRole()
    {
        SFROLEINFO[] role = GetUseRole((uint)SYSFUNCRULE.DWSCOPEKIND.SFSCOPE_DEVID, devID,labID);
        if (role != null && role.Length > 0)
        {
            SFROLEINFO r = role[0];
            uint? sta = r.dwStatus;
            if (!IsStat(sta, (uint)SFROLEINFO.DWSTATUS.SFROLESTAT_AUTO) && !IsStat(sta, (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK))
            {
                sfroleInfo = "没有仪器的使用资格，资格申请状态：" + Util.Converter.GetRoleState(sta);
                useRole = "n";
            }
            if ((sta & (uint)SFROLEINFO.DWSTATUS.SFROLESTAT_NOAPPLY) > 0 || (sta & (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL) > 0)
            {
                sfroleInfo = "没有仪器的使用资格，" + ((sta & (uint)SFROLEINFO.DWSTATUS.SFROLESTAT_CHECKREJECT) > 0 ? "<span class='orange'>管理员拒绝</span> " : "点击[<span class='click' onclick='applyDevUseRole(" + r.dwSFRuleID + "," + r.dwApplyID + ")'>申请使用资格</span>]");
            }
            if (((sta & (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL) > 0 || (sta & (uint)SFROLEINFO.DWSTATUS.SFROLESTAT_CHECKREJECT) > 0) && !string.IsNullOrEmpty(r.szCheckInfo))
            {
                sfroleInfo += " [<span class='click' onclick='uni.msgBox(\"" + r.szCheckInfo + "\")'>审核反馈</span>]";
            }
        }
    }

    private void InitAchieve()
    {
        REWARDRECREQ req = new REWARDRECREQ();
        req.dwDevID = devID;
        req.dwStartDate = 0;
        req.dwEndDate = 20990909;
        REWARDREC[] rlt;
        if (m_Request.Device.RewardRecGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                REWARDREC rec = rlt[i];
                string tr = "<tr><td>" + rec.szRewardName + "</td>" +
                                                "<td>" + rec.szMemberNames + "</td>" +
                                                "<td>" + rec.szAuthOrg + "</td>" +
                                                "<td>" + ConvertLevel(rec.dwRewardLevel) + "</td>";
                string detail="<td><a href='AchiDetail.aspx?dev=" + devID + "&achi=" + rec.dwRewardID + "' target='_blank'>详细信息</a></td></tr>";
                if ((rec.dwRewardKind & (uint)REWARDREC.DWREWARDKIND.REKIND_PRIZE) > 0)
                    prize.InnerHtml += tr+detail;
                else if ((rec.dwRewardKind & (uint)REWARDREC.DWREWARDKIND.REKIND_THESISISSUE) > 0)
                {
                    thesis.InnerHtml += tr+"<td>"+rec.szExtInfo+"</td>"+detail;
                }
            }
        }
        else
            MsgBox(m_Request.szErrMsg);
    }
    private void InitDevInfo(uint devID)
    {
        Session["CUR_DEV"] = null;
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        DEVREQ vrGet = new DEVREQ();
        UNIDEVICE[] vtResult;
        vrGet.dwDevID = devID;
        uResponse = m_Request.Device.Get(vrGet, out vtResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtResult.Length > 0)
        {
            UNIDEVICE dev = vtResult[0];
            labID = dev.dwLabID;
            Session["CUR_DEV"] = vtResult[0];
            devNum.InnerHtml = dev.dwDevSN.ToString();
            pagePosition = dev.szLabName + " | " + dev.szDevName;
            devName.InnerHtml = dev.szDevName;
            CurDevName = dev.szDevName;
            devModel.InnerHtml = dev.szModel;
            //仪器预约介绍
            string intro=dev.szMemo;
            intro=intro.Replace(";", "<br/>");
            intro=intro.Replace("；", "<br/>");
            detailIntro = intro;
            //启用日期  *
            if (dev.dwPurchaseDate != null && dev.dwPurchaseDate.ToString().Length > 7)
            {
                string str = dev.dwPurchaseDate.ToString();
                devDate.InnerHtml = str.Substring(0, 4) + "年" + str.Substring(4, 2) + "月" + str.Substring(6, 2) + "日";
            }
            ContDevExt(dev.szExtInfo);
            devEgName.InnerHtml = "";//英文名 *
            devAssertSN.InnerHtml = dev.szAssertSN;
            devPara.InnerHtml = ConvertStr(ViewState["szPerform"]);
            devSpecimen.InnerHtml = ConvertStr(ViewState["szSample"]);
            devFun.InnerHtml = "";//dev.szFunc;
            devKind.InnerHtml = dev.szKindName;
            devLab = dev.szLabName;
            devLoc.InnerHtml = dev.szRoomName;
            imgUrl = GetImg(dev.dwDevSN);
            //仪器状态
            if (Converter.GetDevStat(dev.dwDevStat))
            {
                devSta.InnerHtml = Converter.GetDevRunStat(dev.dwRunStat);
            }
            else
            {
                devSta.InnerHtml = "<span style='color:red'>仪器不可用</span>";
                sfroleInfo = "仪器不可用";
                enable = "false";
            }
            if ((dev.dwProperty & (uint)UNIDEVICE.DWPROPERTY.DEVPROP_NORESV) > 0)
            {
                sfroleInfo = "仪器不支持网上预约，预约请联系管理员："+dev.szAttendantTel;
                enable = "false";
            }
            //获取管理员
            GROUPMEMDETAIL[] mbs=GetMember(dev.dwManGroupID);
            if (mbs != null && mbs.Length > 0)
            {
                for (int i = 0; i < mbs.Length; i++)
                {
                    //devMan +=(i+1)+": "+ mbs[i].szTrueName + "&nbsp;&nbsp;";
                    devMan += mbs[i].szTrueName + "(" + mbs[i].szHandPhone + ")&nbsp;&nbsp;";
                }
            }
            else
            {
                devMan = dev.szAttendantName+"("+dev.szAttendantTel+")";
            }
            
        }
        else
            MsgBox(m_Request.szErrMsg);
    }
    private GROUPMEMDETAIL[] GetMember(uint? id)
    {
        GROUPMEMDETAILREQ req = new GROUPMEMDETAILREQ();
        req.dwGroupID = id;
        GROUPMEMDETAIL[] rlt;
        if (m_Request.Group.GetGroupMemDetail(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            List<GROUPMEMDETAIL> list = new List<GROUPMEMDETAIL>();
            for (int i = 0; i < rlt.Length; i++)
            {
                list.Add(rlt[i]);
            }
            list.Sort(delegate(GROUPMEMDETAIL a, GROUPMEMDETAIL b)
            {
                int ret = a.szExtInfo.CompareTo(b.szExtInfo);
                return ret;
            });
            rlt = list.ToArray();
        }
        return rlt;
    }
    private void ContDevExt(string szMemo)
    {
        //szManufacturers生产厂商
        //szNation国别
        //szLanguage操作语言
        //szPerform性能指标
        //szSample样品要求
        //主要应用: unidevice本来就有的szfun
        int uStart = -1;
        int uEnd = -1;
        string szTemp = "";
        szTemp = "{Manufacturers:";
        uStart = szMemo.IndexOf(szTemp);
        uEnd = szMemo.IndexOf("},", uStart);
        string szManufacturers = "";
        if (uStart > -1 && uEnd > -1)
        {
            szManufacturers = szMemo.Substring(uStart + szTemp.Length, uEnd - uStart - szTemp.Length);
            ViewState["szManufacturers"] = szManufacturers;
        }
        szTemp = "{szNation:";
        uStart = szMemo.IndexOf(szTemp);
        uEnd = szMemo.IndexOf("},", uStart);
        string szNation = "";
        if (uStart > -1 && uEnd > -1)
        {
            szNation = szMemo.Substring(uStart + szTemp.Length, uEnd - uStart - szTemp.Length);
            ViewState["szNation"] = szNation;
        }

        szTemp = "{szLanguage:";
        uStart = szMemo.IndexOf(szTemp);
        uEnd = szMemo.IndexOf("},", uStart);
        string szLanguage = "";
        if (uStart > -1 && uEnd > -1)
        {
            szLanguage = szMemo.Substring(uStart + szTemp.Length, uEnd - uStart - szTemp.Length);
            ViewState["szLanguage"] = szLanguage;
        }
        szTemp = "{szPerform:";
        uStart = szMemo.IndexOf(szTemp);
        uEnd = szMemo.IndexOf("},", uStart);
        string szPerform = "";
        if (uStart > -1 && uEnd > -1)
        {
            szPerform = szMemo.Substring(uStart + szTemp.Length, uEnd - uStart - szTemp.Length);
            ViewState["szPerform"] = szPerform;
        }

        szTemp = "{szSample:";
        uStart = szMemo.IndexOf(szTemp);
        uEnd = szMemo.IndexOf("},", uStart);
        string szSample = "";
        if (uStart > -1 && uEnd > -1)
        {
            szSample = szMemo.Substring(uStart + szTemp.Length, uEnd - uStart - szTemp.Length);
            ViewState["szSample"] = szSample;
        }
    }
    string ConvertStr(object obj)
    {
        if (obj == null)
        {
            return "";
        }
        else
        {
            return obj.ToString();
        }
    }
    public string ConvertLevel(uint? level)
    {
        if (level == 1) return "SCI";
        if (level == 2) return "国家级";
        if (level == 3) return "省部级";
        if (level == 101) return "国家级";
        if (level == 102) return "省部级";
        if (level == 103) return "院校级";
        return "其它";
    }
}