using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using UniWebLib;

public partial class Page_ : UniClientPage
{
    protected string memList = "";
    DAYOPENRULE[] vtDayOpenRule;
    protected void Page_Load(object sender, EventArgs e)
    {
        base.LoadPage();
        Bind ddlBind = new Bind();
        if (!string.IsNullOrEmpty(Request["ddlHourStart"]) && !string.IsNullOrEmpty(Request["ddlHourEnd"]))
        {
            old_start.Value = Request["ddlHourStart"];
            old_end.Value = Request["ddlHourEnd"];
        }
        if (!this.Page.IsPostBack)
        {
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            string szKindID = Request["devkind"];
            string szDate = Request["date"];
            string szTime = Request["time"];
            if (string.IsNullOrEmpty(szTime))
            {
                szTime=DateTime.Now.Hour + ":00";
            }
            string szRoomID = "";
            uint clsKind = 0;
            string szDevid = Request["dev"];
            if (szDevid != null && szDevid != "")
            {
                DEVREQ vrDevGet = new DEVREQ();
                vrDevGet.dwDevID = uint.Parse(szDevid);
                UNIDEVICE[] vtDevRes;
                if (m_Request.Device.Get(vrDevGet, out vtDevRes) == REQUESTCODE.EXECUTE_SUCCESS && vtDevRes != null && vtDevRes.Length > 0)
                {
                    hint.InnerHtml = vtDevRes[0].szDevURL;
                    szRoomID = vtDevRes[0].dwRoomID.ToString();
                    curObj.Text = vtDevRes[0].szLabName + " " + vtDevRes[0].szDevName;
                    clsKind = (uint)vtDevRes[0].dwClassKind;
                }
            }

            DEVKINDFORRESVREQ vrGet = new DEVKINDFORRESVREQ();

            if (szRoomID != "")
            {
                vrGet.szRoomIDs = szRoomID;
            }
            vrGet.szKindIDs = szKindID;
            vrGet.dwDate = uint.Parse(szDate);
            lblDate.Text = (uint.Parse(szDate)) / 10000 + "-" + ((uint.Parse(szDate)) % 10000) / 100 + "-" + ((uint.Parse(szDate)) % 10000) % 100;
            DateTime dtNow = DateTime.Now;
            DateTime dtSelect;
            try
            {
                dtSelect = DateTime.Parse(lblDate.Text + " " + szTime);
            }
            catch
            {
                dtSelect = dtNow;
            }
            TimeSpan sp = dtNow - dtSelect;
            if (dtNow.Date == dtSelect.Date && sp.TotalMinutes > 0)
            {
                int nNowMin = dtNow.Minute;
                if (nNowMin > 0 && nNowMin <= 30)
                {
                    szTime = dtNow.Hour + ":" + "30";
                }
                else if (nNowMin > 30 && nNowMin <= 60)
                {
                    szTime = dtNow.AddHours(1.0).Hour + ":" + "00";
                }
            }
            vrGet.dwResvPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL;
            DEVKINDFORRESV[] vtRes;
            uResponse = m_Request.Device.GetDevKindForResv(vrGet, out vtRes);
            if (uResponse == REQUESTCODE.EXECUTE_FAIL || vtRes == null || vtRes.Length == 0)
            {
                return;
            }
            DEVKINDFORRESV setValue = new DEVKINDFORRESV();
            setValue = vtRes[0];
            Session["DEVKINDFORRESV"] = setValue;
            UNIRESVRULE setResvRule = setValue.szRuleInfo;
            vtDayOpenRule = setValue.szOpenInfo;
            bool bIsLongTime = false;
            ViewState["bIsLongTime"] = "false";
            if ((((uint)setValue.dwProperty) & ((uint)UNIDEVKIND.DWPROPERTY.DEVPROP_LONGTERMRESV)) > 0)
            {
                ViewState["bIsLongTime"] = "true";
                bIsLongTime = true;
            }
            else
            {
                ViewState["bIsLongTime"] = "false";
            }
            if ((((uint)setValue.dwProperty) & ((uint)UNIDEVKIND.DWPROPERTY.DEVPROP_LEASE)) > 0)
            {
                //ViewState["bIsLease"] = "true";
                //bIsLongTime = true;
            }
            else
            {
                ViewState["bIsLease"] = "false";
            }
            if (string.IsNullOrEmpty(szDevid))
            {
                curObj.Text = setValue.szLabName + " " + setValue.szKindName;
            }
            string content = "人数限制：最少" + setValue.dwMinUsers.ToString() + "人&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "最多:" + setValue.dwMaxUsers.ToString() + "人" + "<br />"; ;
            
            string szFee = "不收费";
            UNIFEE setFee = setValue.szFeeInfo;
            if (setFee.szFeeDetail != null)
            {
                FEEDETAIL[] vtFeeDetail = setFee.szFeeDetail;
                if (vtFeeDetail != null && vtFeeDetail.Length > 0)
                {
                    szFee = "";
                    string szType = "";
                    uint uLenFeeDetail = (uint)vtFeeDetail.Length;
                    switch (uLenFeeDetail)
                    {
                        case 1:
                            szType = "：";
                            break;
                        case 2:
                            szType = "占用费：";
                            break;
                        case 8:
                            szType = "：";
                            break;
                        default:
                            szType = "";
                            break;
                    }
                    for (int k = 0; k < uLenFeeDetail; k++)
                    {
                        uint uKind = (uint)vtFeeDetail[k].dwFeeType;
                        string szTimeFee = "";
                        if ((uint)vtFeeDetail[k].dwUnitTime == 0)
                        {
                            szFee += szType + "0元";
                        }
                        else
                        {
                            szTimeFee = (((uint)vtFeeDetail[k].dwUnitFee / 100.0) / ((uint)vtFeeDetail[k].dwUnitTime / 60.0)).ToString();
                            szFee += szType + "";// "每小时" + szTimeFee + "元";
                        }

                    }
                }
            }
            // content += szFee+"<br />";
            //预约规则
            latest.Value = setResvRule.dwLatestResvTime.ToString();
            earliest.Value = setResvRule.dwEarliestResvTime.ToString();
            max.Value = setResvRule.dwMaxResvTime.ToString();
            min.Value = setResvRule.dwMinResvTime.ToString();
            int intDate = 0;
            if (bIsLongTime)
            {
                divFreeTime.Style.Add("display", "none");
                divLimit.Style.Add("display", "none");
                divLongTime.Style.Add("display", "block");
                startDate.Value = (new DateTime((int.Parse(szDate)) / 10000, ((int.Parse(szDate)) % 10000) / 100, (int.Parse(szDate) % 100))).ToString("yyyy-MM-dd");
                int nDate = int.Parse(szDate);
                DateTime dtDate = new DateTime(nDate / 10000, (nDate % 10000) / 100, nDate % 100);
                //提前预约具体时间
                content += "预约限制：" + DateTime.Now.AddDays((uint)setResvRule.dwLatestResvTime / 1440).ToString("yyyy-MM-dd") + "到" + DateTime.Now.AddDays(((uint)setResvRule.dwEarliestResvTime / 1440)-1).ToString("yyyy-MM-dd") + " &nbsp;&nbsp;&nbsp;&nbsp; 每次预约不少于" + ((uint)setResvRule.dwMinResvTime / 1440) + "天" + "&nbsp;&nbsp;&nbsp;&nbsp;" + "不大于" + ((uint)setResvRule.dwMaxResvTime / 1440) + "天";
            }
            else
            {
                ViewState["dwMaxResvTime"] = (uint)setResvRule.dwMaxResvTime;
                content += "预约限制：" + DateTime.Now.AddDays((uint)setResvRule.dwLatestResvTime / 1440).ToString("yyyy-MM-dd") + "到" + DateTime.Now.AddDays((uint)setResvRule.dwEarliestResvTime / 1440).ToString("yyyy-MM-dd") + " &nbsp;&nbsp;&nbsp;&nbsp; 每次预约不少于" + MinToHour((uint)setResvRule.dwMinResvTime) + "&nbsp;&nbsp;&nbsp;&nbsp;" + "不大于" + MinToHour((uint)setResvRule.dwMaxResvTime);
            }
            content +=" 迟到 "+ setResvRule.dwCancelTime + " 分钟取消预约";
            content += "<br />";
            string attach = GetConfig("showResvAttach");
            string szCheck = "";
            //20140504前服务为 if (((uint)setResvRule.dwLimit & (uint)UNIRESVRULE.DWLIMIT.RESVLIMIT_CENSOR) > 0)
            if (setResvRule.CheckTbl != null && setResvRule.CheckTbl.Length > 0 && (setResvRule.CheckTbl[0].dwProperty & (uint)RULECHECKINFO.DWPROPERTY.CHECKPROP_MAIN) > 0)
            {
                ViewState["IsCheck"] = "true";
                szCheck += "需管理员审核<br />";
            }
            if (((uint)setResvRule.dwLimit & (uint)UNIRESVRULE.DWLIMIT.RESVLIMIT_NEEDAPP) > 0)
            {
                szCheck += "&nbsp;&nbsp;需提交申请报告";
                need_file.Value = "true";
            }
            else if (attach == null || attach != "1" || (clsKind & (uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS) == 0)
            {
                divUpLoadFile.Style.Add("display", "none");
            }
            string downloadKinds = GetConfig("downloadKinds");
            if (!string.IsNullOrEmpty(downloadKinds)&& downloadKinds.IndexOf(szKindID) < 0)
            {
                divUpLoadFile.Style.Add("display", "none");
            }
            if (szCheck != "")
            {
                content += "<br />审核要求：" + szCheck;
            }
            int nLimit = (int)setValue.dwOpenLimit;
            nLimit &= ~(int)GROUPOPENRULE.DWOPENLIMIT.OPENLIMIT_FIXEDTIME;
            ViewState["nLimit"] = nLimit;
            if ((((uint)setValue.dwProperty) & ((uint)UNIDEVKIND.DWPROPERTY.DEVPROP_LEASE)) > 0)
            {
                ViewState["isLoad"] = "true";
                ViewState["isAutoAssign"] = "false";
            }
            DEVKINDREQ vrGetDevCls = new DEVKINDREQ();
            vrGetDevCls.dwKindID = ToUInt(szKindID);
            UNIDEVKIND[] vtDevCls;
            uResponse = m_Request.Device.DevKindGet(vrGetDevCls, out vtDevCls);
            if (vtDevCls != null && vtDevCls.Length > 0)
            {
                // szMemo.Text = vtDevCls[0].szMemo.ToString();
                int nKind = (int)vtDevCls[0].dwClassKind;
                if (((nKind) & ((int)UNIDEVCLS.DWKIND.CLSKIND_COMPUTER)) > 0 || ((nKind) & ((int)UNIDEVCLS.DWKIND.CLSKIND_SEAT)) > 0)
                {
                    ViewState["isAutoAssign"] = "false";
                }
                if ((((nKind) & ((int)UNIDEVCLS.DWKIND.CLSKIND_COMMONS)) > 0) &&(GetConfig("resvTheme") == "1" || GetConfig("resvTheme") == "2"))//&& ((int)vtDevCls[0].dwMaxUsers < 2)
                {
                    if (GetConfig("resvTheme") == "2")
                    {
                        txtMemo.Attributes["IsMust"] = "true";
                    }
                    else
                    {
                        txtMemo.Attributes["IsMust"] = "false";
                    }
                    string szlblMemo = "";
                    if (vtRes[0].szKindName.ToString().IndexOf("研究") >= 0)
                    {
                        szlblMemo = "研讨主题：";
                    }
                    else if (vtRes[0].szKindName.ToString().IndexOf("体验") >= 0)
                    {
                        szlblMemo = "体验内容：";
                    }
                    else
                    {
                        szlblMemo = "申请说明：";
                    }
                    ViewState["szlblMemo"] = szlblMemo;
                    ViewState["IsMemo"] = "true";
                    lblszMemo.Text = szlblMemo;
                }
                else
                {
                    ViewState["IsMemo"] = "false";
                    divMemo.Style.Add("display", "none");
                }
            }
            if (setValue.dwMaxUsers <= 1)
            {
                divMemberAdd1.Style.Add("display", "none");
                divMemberAdd2.Style.Add("display", "none");
            }
            if (vtDayOpenRule != null&&vtDayOpenRule.Length>0)
            {
                string ps = "";
                string[] open = GetOpenArray(setValue.szOpenInfo, ref ps);
                if (open.Length > 1)
                {
                    cls_time.Value = ps;
                    open_start.Value = open[0];
                    open_end.Value = open[1];
                    string start = startDate.Value;
                    if (!string.IsNullOrEmpty(start) && GetConfig("resvAllDay")!="1")
                    {
                        if (start == DateTime.Now.ToString("yyyy-MM-dd"))
                            start += " " + DateTime.Now.ToString("HH:mm");
                        else
                            start += " " + open[0];
                        startDate.Value = start;
                    }
                }
            }
            if (vtDayOpenRule != null && vtDayOpenRule.Length > 0 && (nLimit & (uint)GROUPOPENRULE.DWOPENLIMIT.OPENLIMIT_FIXEDTIME) == 0)
            {
                content += "开放时间：";
                uint uBeginTime = (uint)vtDayOpenRule[0].dwBegin;
                uint uEndTime = (uint)vtDayOpenRule[0].dwEnd;
                for (int i = 0; i < vtDayOpenRule.Length; i++)
                {
                    if (vtDayOpenRule[i].dwBegin != null)
                    {
                        uint uBegin = (uint)vtDayOpenRule[i].dwBegin;
                        uint uEnd = (uint)vtDayOpenRule[i].dwEnd;
                        content += uBegin / 100 + ":" + (uBegin % 100).ToString("00") + "到" + uEnd / 100 + ":" + (uEnd % 100).ToString("00") + ",";
                        uEndTime = uEnd;
                    }
                }
                if (!bIsLongTime)
                {
                    divFreeTime.Style.Add("display", "block");
                    divLimit.Style.Add("display", "none");
                    divLongTime.Style.Add("display", "none");
                    //int nStart = (int)(vtDayOpenRule[0].dwBegin);
                    //int nEnd = (int)(vtDayOpenRule[0].dwEnd);
                    ArrayList alistStart = new ArrayList();
                    string szUse = setValue.szUsableNumArray.ToString();
                    ViewState["szUsableNumArray"] = szUse;
                    ViewState["dwEnd"] = uEndTime;
                    uint n = uint.Parse(szTime.Replace(":", ""));
                    uint unit = 10;
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["resvTimeUnit"]))
                        unit = Convert.ToUInt32(ConfigurationManager.AppSettings["resvTimeUnit"]);
                    string now = DateTime.Now.ToString("yyyyMMdd");
                    if (szDate == now)
                    {
                        uBeginTime = Convert.ToUInt32(DateTime.Now.ToString("HHmm"));
                        uint u = uBeginTime % unit;
                        if (u > 0)
                        {
                            uBeginTime = uBeginTime - u + unit;
                        }
                    }
                    t_unit.Value = unit.ToString();
                    uint uBeginTimeInt = uBeginTime / 100 * 60 + uBeginTime % 100;
                    uint uEndTimeInt = uEndTime / 100 * 60 + uEndTime % 100;
                    for (uint i = uBeginTimeInt; i <= uEndTimeInt; i = i + unit)
                    {
                        uint nTemp = (uint)i / 60 * 100 + i % 60;
                        ListItem item = new ListItem((nTemp / 100).ToString("00") + ":" + (nTemp % 100).ToString("00"), nTemp.ToString());
                        ddlHourStart.Items.Add(item);
                        tempHourEnd.Items.Add(item);
                    }
                    string h = (dtSelect.Hour * 100).ToString();
                    ddlHourStart.SelectedValue = tempHourEnd.SelectedValue = h;
                    tempHourEnd.Style.Add("display", "none");
                }
            }
            else if (!bIsLongTime && vtDayOpenRule != null && vtDayOpenRule.Length > 0 && (nLimit & (uint)GROUPOPENRULE.DWOPENLIMIT.OPENLIMIT_FIXEDTIME) > 0)
            {

                divFreeTime.Style.Add("display", "none");
                divLimit.Style.Add("display", "block");
                divLongTime.Style.Add("display", "none");
                int len = vtDayOpenRule.Length;
                List<DAYOPENRULE> list = new List<DAYOPENRULE>();
                for (int i = 0; i < len; i++)
                {
                    uint nStart = (uint)vtDayOpenRule[i].dwBegin;
                    nStart = (nStart / 100) * 60 + nStart % 100;
                    uint nEnd = (uint)vtDayOpenRule[i].dwEnd;
                    nEnd = (nEnd / 100) * 60 + nEnd % 100;
                    if (!ddlBind.GetIsReserve(nStart, nEnd, setValue.szUsableNumArray.ToString()))
                    {
                        list.Add(vtDayOpenRule[i]);
                    }
                }
                len = list.Count;
                vtDayOpenRule = list.ToArray();
                if (len >= 0)
                {
                    for (int i = 0; i < len; i++)
                    {
                        string szTimedll = ddlBind.GetTimeToDisplay((int)vtDayOpenRule[i].dwBegin, (int)vtDayOpenRule[i].dwEnd);
                        int intValue = (int)vtDayOpenRule[i].dwBegin * 10000 + (int)vtDayOpenRule[i].dwEnd;
                        ListItem item = new ListItem(szTimedll, intValue.ToString());
                        ddlPartTime.Items.Add(item);
                        if (i < len - 1)
                        {
                            szTimedll = ddlBind.GetTimeToDisplay((int)vtDayOpenRule[i].dwBegin, (int)vtDayOpenRule[i + 1].dwEnd);
                            intValue = (int)vtDayOpenRule[i].dwBegin * 10000 + (int)vtDayOpenRule[i].dwEnd;
                            item = new ListItem(szTimedll, intValue.ToString());
                            ddlPartTime.Items.Add(item);
                        }

                    }

                    string szTimeALL = ddlBind.GetTimeToDisplay((int)vtDayOpenRule[0].dwBegin, (int)vtDayOpenRule[len - 1].dwEnd);
                    int intValueAll = (int)vtDayOpenRule[0].dwBegin * 10000 + (int)vtDayOpenRule[len - 1].dwEnd;
                    ListItem itemAll = new ListItem(szTimeALL, intValueAll.ToString());
                    ddlPartTime.Items.Add(itemAll);
                }
                else
                {
                    ListItem itemAll = new ListItem("无合适时间", "0");
                    ddlPartTime.Items.Add(itemAll);
                }
            }
            divUserLimit.InnerHtml = content;
            // aBack.Attributes.Add("href",(string)Session["szBackPage"]);

        }
        if (groupIDHidden.Value != "")
        {
            showGroupMember(groupIDHidden.Value);
        }
    }
    protected void Sub_ServerClick(object sender, EventArgs e)
    {
        old_start.Value = Request["ddlHourStart"];
        old_end.Value = Request["ddlHourEnd"];
        Bind ddlBind = new Bind();
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        if (ViewState["IsMemo"] != null && (string)ViewState["IsMemo"] == "true")
        {
            //if (txtMemo.Attributes["IsMust"] == "true")
            //{
            //    string szMemoVW = (string)ViewState["szlblMemo"];
            //    if (szMemoVW != null)
            //    {
            //        MsgBox(szMemoVW.Replace("：", "") + "必须填写");
            //        return;
            //    }
            //}
        }
        string devID = Request["dev"];
        UNIDEVICE setDev = new UNIDEVICE();
        if (devID != null && devID != "")
        {
            DEVREQ vrGet = new DEVREQ();
            vrGet.dwDevID = uint.Parse(devID);
            UNIDEVICE[] vtDev;
            uResponse = m_Request.Device.Get(vrGet, out vtDev);
            if (uResponse != REQUESTCODE.EXECUTE_SUCCESS || vtDev == null || vtDev.Length <= 0)
            {
                MsgBox("获取设备时出错："+m_Request.szErrMsg);
                return;
            }
            setDev = vtDev[0];
        }
        string kindID = Request["devkind"];
        bool bIsByKind = false;
        if (!string.IsNullOrEmpty(kindID) && string.IsNullOrEmpty(devID))
        {
            bIsByKind = true;

        }
        if (bIsByKind && kindID != null && kindID != "")
        {
            DEVKINDREQ vrDevKindReq = new DEVKINDREQ();
            vrDevKindReq.dwKindID = ToUInt(kindID);
            UNIDEVKIND[] vtDevKindRes;
            uResponse = m_Request.Device.DevKindGet(vrDevKindReq, out vtDevKindRes);
            if (vtDevKindRes != null && vtDevKindRes.Length > 0)
            {
                setDev.szDevName = vtDevKindRes[0].szKindName.ToString();
                setDev.dwClassKind = vtDevKindRes[0].dwClassKind;
            }
            else
            {
                MsgBox(m_Request.szErrMsg);
                return;
            }
            //临时方法，查找房间号
            DEVREQ req = new DEVREQ();
            req.szKindIDs = kindID;
            UNIDEVICE[] rlt;
            if (m_Request.Device.Get(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS && rlt.Length > 0)
            {
                setDev.szRoomNo = rlt[0].szRoomNo;
                setDev.szLabName = rlt[0].szLabName;
            }
            else
            {
                MsgBox(m_Request.szErrMsg);
                return;
            }
            ////
            setDev.dwKindID = uint.Parse(kindID);
        }

        object objAccno = Session["LOGIN_ACCINFO"];
        if (objAccno == null)
        {
            return;//保存信息有丢失
        }
        UNIACCOUNT vrAccno = (UNIACCOUNT)objAccno;

        UNIRESERVE resvSet = new UNIRESERVE();

        if (ViewState["isAutoAssign"] == null)
        {
            if (bIsByKind)
            {
                resvSet.dwProperty = (uint)UNIRESERVE.DWPROPERTY.RESVPROP_AUTOASSIGN;
            }
        }
        if ((setDev.dwClassKind & (uint)UNIDEVCLS.DWKIND.CLSKIND_LOAN) > 0)
        {
            resvSet.dwUseMode = (uint)UNIRESERVE.DWUSEMODE.RESVUSE_LEASE;
        }
        else
        {
            resvSet.dwUseMode = (uint)UNIRESERVE.DWUSEMODE.RESVUSE_USEDEV;
        }
        if (groupIDHidden.Value.ToString() != "")
        {
            resvSet.dwMemberKind = (uint)UNIRESERVE.DWMEMBERKIND.MEMBERKIND_GROUP;
            resvSet.dwMemberID = uint.Parse(groupIDHidden.Value.ToString());
            resvSet.szOwnerName = vrAccno.szTrueName.ToString() + "group:" + groupIDHidden.Value.ToString();
        }
        else
        {
            resvSet.dwMemberKind = (uint)UNIRESERVE.DWMEMBERKIND.MEMBERKIND_PERSONNAL;
            resvSet.dwMemberID = vrAccno.dwAccNo;
            resvSet.szOwnerName = vrAccno.szTrueName.ToString();
        }
        resvSet.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL;



        //设置预约设备
        RESVDEV[] resvDev = new RESVDEV[1];
        if (bIsByKind)
        {
            resvDev[0] = new RESVDEV();
            resvDev[0].dwDevKind = (uint)setDev.dwKindID;
            resvDev[0].dwDevNum = 1;
            resvDev[0].szRoomNo = setDev.szRoomNo;
            if (setDev.szDevName != null)
            {
                resvDev[0].szDevName = setDev.szDevName.ToString();
            }
            //实验室
            string szLabID = Request["labid"];
            if (szLabID == null || szLabID == "")
            {
                MsgBox("未指定实验室");
                return;
            }
            resvSet.dwLabID = uint.Parse(szLabID);
            resvSet.szLabName = setDev.szLabName;
        }
        else
        {
            resvDev[0] = new RESVDEV();
            resvDev[0].dwDevStart = setDev.dwDevSN;
            resvDev[0].dwDevEnd = setDev.dwDevSN;
            resvDev[0].dwDevKind = setDev.dwKindID;
            resvDev[0].szRoomNo = setDev.szRoomNo;
            resvDev[0].dwDevNum = 1;
            //实验室
            resvDev[0].szDevName = setDev.szDevName.ToString();
            resvSet.szLabName = setDev.szLabName;
            resvSet.dwLabID = setDev.dwLabID;
        }
        //resvSet.szResvDevs = resvDev; 20140821前
        resvSet.ResvDev = resvDev;

        //设置时间

        string szDate = (string)Request["date"];
        if (szDate == null || szDate == "")
        {
            //MsgBox("预约失败请重新选择空间预约", this.Page);
            return;
        }
        int intDate = int.Parse(szDate);
        if (ViewState["nLimit"] != null && ViewState["nLimit"].ToString() == "2")
        {
            szDate = (uint.Parse(szDate) / 10000).ToString() + "-" + ((uint.Parse(szDate) % 10000) / 100).ToString("00") + "-" + ((uint.Parse(szDate) % 10000) % 100).ToString("00");

            string szDDLSelectValue = ddlPartTime.SelectedValue;
            int intDDLSelectValue = int.Parse(szDDLSelectValue);
            int intStartHour = ((intDDLSelectValue / 10000) / 100);
            int intStartMin = ((intDDLSelectValue / 10000) % 100);

            int intEndHour = ((intDDLSelectValue % 10000) / 100);
            int intEndMin = ((intDDLSelectValue % 10000) % 100);

            resvSet.dwBeginTime = ToUInt(Get1970Seconds(szDate) + intStartHour * 3600 + intStartMin * 60);
            resvSet.dwEndTime = ToUInt(Get1970Seconds(szDate) + intEndHour * 3600 + intEndMin * 60);
        }
        else
        {

            if (ViewState["bIsLongTime"].ToString() == "true")
            {
                string strStartTime = lblDate.Text.ToString();
                string strEndTime = lblDate.Text.ToString();
                resvSet.dwBeginTime = ToUInt(Get1970Seconds(strStartTime));
                resvSet.dwEndTime = ToUInt(Get1970Seconds(strEndTime));

            }
            else
            {
                string strStartTime = lblDate.Text + " " + ddlHourStart.SelectedItem.Text.ToString();
                string end = Request[ddlHourEnd.UniqueID];
                if (string.IsNullOrEmpty(end) || end.Length < 3)
                {
                    MsgBox("请选择结束时间");
                    return;
                }
                string strEndTime = lblDate.Text + " " + end.Substring(0, end.Length - 2) + ":" + end.Substring(end.Length - 2);
                resvSet.dwBeginTime = ToUInt(Get1970Seconds(strStartTime));
                resvSet.dwEndTime = ToUInt(Get1970Seconds(strEndTime));

            }
        }
        if (ViewState["bIsLongTime"] != null && ViewState["bIsLongTime"].ToString() == "true")
        {
            //只选择日期
            string strStartTime = startDate.Value.ToString();
            string strEndTime = endDate.Value.ToString();
            if (GetConfig("resvAllDay") == "1")
            {
                string start;
                string end;
                getTime(out start, out end);
                strStartTime +=" " + start;
                strEndTime +=" " + end;
            }
            resvSet.dwBeginTime = ToUInt(Get1970Seconds(strStartTime));
            resvSet.dwEndTime = ToUInt(Get1970Seconds(strEndTime));
        }

        //
        string rtName = Request["rtName"];
        string rtLevel = Request["rtLevel"];
        if (!string.IsNullOrEmpty(rtName) && !string.IsNullOrEmpty(rtLevel))
        {  
            resvSet.szTestName = rtName + "(级别：" + rtLevel + ")";
        }
        if (divUpLoadFile.Style["display"] != "none")
        {
            if (Request["up_file"] != null)
            {
                resvSet.szApplicationURL = Request["up_file"];
            }
        }
        string sztxtMemo = txtMemo.Text.Replace("&", " ");
        resvSet.szMemo = sztxtMemo;
        resvSet.szTestName = sztxtMemo;
        Logger.Trace("ResvSet,dwBeginTime=" + resvSet.dwBeginTime + ",dwEndTime=" + resvSet.dwEndTime);
        uResponse = m_Request.Reserve.Set(resvSet, out resvSet);
        Bind ddlbind = new Bind();
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {

            string error = m_Request.szErrMessage.ToString();
            if (error.ToLower().IndexOf("owner") > -1)
            {
                error = "登陆超时，请退出后重新登陆";
            }
            MsgBox(error);
            //预约不成功
        }
        else
        {
            //RESVDEV[] vtResvDev = resvSet.szResvDevs; 20140821前
            RESVDEV[] vtResvDev = resvSet.ResvDev;
            string szMessage = "";
            if (vtResvDev != null && vtResvDev.Length > 0)
            {
                if (ViewState["IsCheck"] != null && ViewState["IsCheck"].ToString() == "true")
                {
                    szMessage = "您已成功预约" + vtResvDev[0].szDevName.ToString() + "，需管理员审核";
                }
                else
                {
                    szMessage = "您已成功预约" + vtResvDev[0].szDevName.ToString();
                }
            }
            this.Response.Redirect("my.aspx");

        }
    }
    protected void ddlHourStart_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    public int Get1970Seconds(string Date)//返回和1970的差距秒数
    {
        int result = 0;
        DateTime dt1970 = new DateTime(1970, 01, 01, 08, 0, 0);
        try
        {
            DateTime dtDate = DateTime.Parse(Date);
            TimeSpan spDate = dtDate.Subtract(dt1970);
            result = (int)spDate.TotalSeconds;
        }
        catch
        {
            return -1;
        }
        return result;
    }
    private string Get1970Date(int TotalSeconds)//根据差距秒数 算出现在是日期
    {
        string result = string.Empty;
        DateTime dt1970 = new DateTime(1970, 01, 01, 08, 0, 0);
        DateTime dtNow = dt1970.AddSeconds(TotalSeconds);
        return result = dtNow.ToString("yyyy-MM-dd HH:mm");
    }
    protected void AddMb_ServerClick(object sender, EventArgs e)
    {
        old_start.Value = Request["ddlHourStart"];
        old_end.Value = Request["ddlHourEnd"];
        ACCREQ vrGet = new ACCREQ();
        vrGet.szPID = txtPerson.Value.ToString();
        UNIACCOUNT[] vtRes;
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        uResponse = m_Request.Account.Get(vrGet, out vtRes);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS || vtRes == null || vtRes.Length <= 0)
        {
            txtPerson.Value = "此人不存在";
            return;
        }
        GROUPMEMBER setValue = new GROUPMEMBER();
        setValue.dwKind = (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL;
        setValue.dwMemberID = vtRes[0].dwAccNo;
        setValue.szName = vtRes[0].szTrueName.ToString();
        setValue.szMemo = vtRes[0].szLogonName;
        SetGroup(setValue);
    }
    protected REQUESTCODE SetGroup(GROUPMEMBER setGroupMember)
    {
        Bind ddlBind = new Bind();
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        uint res = 0;
        UNIGROUP setValue = new UNIGROUP();
        string groupID = groupIDHidden.Value.ToString();
        if (groupID.ToString() == "")
        {
            groupID = newGroup().ToString();
            if (groupID == "0")
            {
                return REQUESTCODE.EXECUTE_FAIL;
            }
        }
        res = uint.Parse(groupIDHidden.Value.ToString());
        GROUPMEMBER setGroupMemberRes = new GROUPMEMBER();
        setGroupMemberRes = setGroupMember;
        setGroupMemberRes.dwStatus = (uint)GROUPMEMBER.DWSTATUS.GROUPMEMBERSTAT_FORCE;
        setGroupMemberRes.dwGroupID = res;
        uResponse = m_Request.Group.SetGroupMember(setGroupMemberRes);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
            MsgBox(m_Request.szErrMessage.ToString());
        }
        showGroupMember(res.ToString());
        return uResponse;
    }
    private void showGroupMember(string id)
    {
        Bind ddlBind = new Bind();
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        GROUPREQ vrGroup = new GROUPREQ();
        //vrGroup.dwGetType = (uint)GROUPREQ.DWGETTYPE.GROUPGET_BYID;
        //vrGroup.szGetKey = id;
        vrGroup.dwGroupID = ToUInt(id);
        UNIGROUP[] vtGroup;
        uResponse = m_Request.Group.GetGroup(vrGroup, out vtGroup);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtGroup != null && vtGroup.Length > 0)
        {
            GROUPMEMBER[] vtGroupMember = vtGroup[0].szMembers;
            if (vtGroupMember != null && vtGroupMember.Length > 0)
            {
                memList = "";
                UNIACCOUNT acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
                for (int i = 0; i < vtGroupMember.Length; i++)
                {
                    GROUPMEMBER mb = vtGroupMember[i];//Memo字段为帐号
                    memList += "<tr mbId='" + mb.szMemo + "' gId='" + mb.dwGroupID + "' kind='" + mb.dwKind + "'>";
                    memList += "<td>" + mb.szMemo + "</td>";
                    memList += "<td>" + mb.szName + "</td>";
                    memList += "<td>" + (acc.szLogonName == mb.szMemo ? "" : "[<span class='click' onclick='delMb(this);'>删除</span>]") + "</td>";
                    memList += "</tr>";
                }
            }
        }
    }
    protected int newGroup()
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        object obj = Session["LOGIN_ACCINFO"];
        UNIACCOUNT acc = new UNIACCOUNT();
        if (obj != null)
        {
            acc = (UNIACCOUNT)(obj);
        }
        else
        {
            MsgBox("你还未登录!");
            return 0;
        }
        UNIGROUP setValue = new UNIGROUP();
        setValue.dwKind = ((uint)UNIGROUP.DWKIND.GROUPKIND_RERV);
        setValue.szName = acc.szLogonName.ToString() + DateTime.Now.ToLongTimeString();
        setValue.dwEnrollDeadline = uint.Parse(DateTime.Now.AddYears(10).ToString("yyyyMMdd"));
        if (Session["DEVKINDFORRESV"] != null)
        {
            DEVKINDFORRESV kindf=(DEVKINDFORRESV)Session["DEVKINDFORRESV"];
            setValue.dwMinUsers = kindf.dwMinUsers;
            setValue.dwMaxUsers = kindf.dwMaxUsers;
        }
        uResponse = m_Request.Group.SetGroup(setValue, out setValue);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && setValue.dwGroupID != null)
        {
            groupIDHidden.Value = setValue.dwGroupID.ToString();

            GROUPMEMBER setValueMember = new GROUPMEMBER();
            setValueMember.dwKind = (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL;
            setValueMember.dwMemberID = acc.dwAccNo;
            setValueMember.szName = acc.szTrueName.ToString();
            setValueMember.szMemo = acc.szLogonName;
            setValueMember.dwStatus = (uint)GROUPMEMBER.DWSTATUS.GROUPMEMBERSTAT_FORCE;
            setValueMember.dwGroupID = setValue.dwGroupID;
            //SetGroup(setValueMember);
            uResponse = m_Request.Group.SetGroupMember(setValueMember);
            return (int)setValue.dwGroupID;
        }
        else
        {
            MsgBox(m_Request.szErrMsg);
            return 0;
        }
    }
    //protected void GV_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    int index = Convert.ToInt32(e.CommandArgument.ToString());
    //    string memberid = GV.Rows[index].Cells[0].Text.ToString();
    //    string kind = GV.Rows[index].Cells[3].Text.ToString();
    //    string groupID = groupIDHidden.Value.ToString();
    //    GROUPMEMBER setValue = new GROUPMEMBER();
    //    setValue.dwGroupID = uint.Parse(groupID);
    //    setValue.dwKind = uint.Parse(kind);
    //    setValue.dwMemberID = uint.Parse(memberid);
    //    REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
    //    uResponse = m_Request.Group.DelGroupMember(setValue);
    //    showGroupMember(groupID);
    //}
    protected void ddlHourStart_SelectedIndexChanged1(object sender, EventArgs e)
    {
        ddlHourEnd.Items.Clear();
        Bind ddlBind = new Bind();
        string szUse = ViewState["szUsableNumArray"].ToString();
        string szEnd = ViewState["dwEnd"].ToString();
        string szStartHor = ddlHourStart.SelectedValue.ToString();
        uint n = uint.Parse(szStartHor);
        int nEndF = int.Parse(szEnd);
        ArrayList alistEnd = new ArrayList();
        uint nAddMin = 480;
        if (ViewState["dwMaxResvTime"] != null)
        {
            nAddMin = uint.Parse(ViewState["dwMaxResvTime"].ToString());
        }
        uint nEnd = uint.Parse(szEnd);
        uint nEndReal = 0;
        if (nEnd / 100 * 60 + nEnd % 100 < (n / 100 * 60 + n % 100) + nAddMin)
        {
            nEndReal = nEnd / 100 * 60 + nEnd % 100;
        }
        else
        {
            nEndReal = (n / 100 * 60 + n % 100) + nAddMin;
        }
        alistEnd = ddlBind.GetEndTimeList(n / 100 * 60 + n % 100, nEndReal, 60, 30, szUse);
        for (int i = 0; i <= alistEnd.Count - 1; i++)
        {
            uint nTemp = (uint)alistEnd[i];
            ListItem item = new ListItem((nTemp / 100).ToString("00") + ":" + (nTemp % 100).ToString("00"), nTemp.ToString());
            Logger.trace("end2:" + item.Value.ToString());
            ddlHourEnd.Items.Add(item);
        }
        if (alistEnd.Count == 0)
        {
            ListItem item = new ListItem("无合适时间", "0");
            ddlHourEnd.Items.Add(item);
        }
    }
    void getTime(out string start,out string end){
        start = "00:00";
        end = "23:59";
        if (open_start.Value != "" && open_end.Value != "")
        {
            start = open_start.Value;
            end = open_end.Value;
        }
        if (startDate.Value == DateTime.Now.ToString("yyyy-MM-dd"))
            start = DateTime.Now.AddMinutes(5).ToString("HH:mm");
    }
    string[] GetOpenArray(DAYOPENRULE[] vtOpenInfo, ref string ps)
    {
        //开放规则
        List<string> open = new List<string>();
        List<string> starts = new List<string>();
        List<string> ends = new List<string>();
        for (int j = 0; vtOpenInfo != null && j < vtOpenInfo.Length; j++)
        {
            if ((vtOpenInfo[j].dwOpenPurpose & (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL) == 0)
            {
                continue;
            }
            string start = string.Format("{0,2:00}", ((uint)vtOpenInfo[j].dwBegin) / 100) + ":" + string.Format("{0,2:00}", ((uint)vtOpenInfo[j].dwBegin) % 100);
            string end = string.Format("{0,2:00}", ((uint)vtOpenInfo[j].dwEnd) / 100) + ":" + string.Format("{0,2:00}", ((uint)vtOpenInfo[j].dwEnd) % 100);
            starts.Add(start);
            ends.Add(end);
            //不开放时间
            if (j > 0)
            {
                string p = "{";
                p += "\"start\":\"" + ends[j - 1] + "\",";
                p += "\"end\":\"" + start + "\"";
                p+= "}";
                ps += p+",";
            }
        }
        if (ps.Length > 0) ps = ps.Substring(0,ps.Length - 1);
        //开放时间
        if (starts.Count > 0)
        {
            open.Add(starts[0]);
            open.Add(ends[ends.Count - 1]);
        }
        return open.ToArray();
    }
}
