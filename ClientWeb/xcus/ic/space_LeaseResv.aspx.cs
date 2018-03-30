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
    protected void Page_Load(object sender, EventArgs e)
    {
        base.LoadPage();
        Bind ddlBind = new Bind();
        if (!this.Page.IsPostBack)
        {
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            string szKindID = Request["devkind"];
            string szDate = Request["date"];
            string szTime = Request["time"];
            DEVKINDFORRESVREQ vrGet = new DEVKINDFORRESVREQ();
            vrGet.szKindIDs = szKindID;
            vrGet.dwDate = ToUInt(szDate);
            lblDate.Text=(int.Parse(szDate))/10000+"-"+((int.Parse(szDate))%10000)/100+"-"+((int.Parse(szDate))%10000)%100;
            DateTime dtNow = DateTime.Now;
            DateTime dtSelect;
            try
            {
                dtSelect=DateTime.Parse(lblDate.Text + " " + szTime);
            }
            catch
            {
                dtSelect = dtNow;
            }
            TimeSpan sp = dtNow - dtSelect;
            if (dtNow.Date == dtSelect.Date&&sp.TotalMinutes>0)
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
            UNIRESVRULE setResvRule = new UNIRESVRULE();
            setResvRule=setValue.szRuleInfo;
            DAYOPENRULE[] vtDayOpenRule = setValue.szOpenInfo;    
            bool bIsLongTime = false;
            bool bIsLease = false;
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
                ViewState["bIsLease"] = "true";
                bIsLease = true;
            }
            else
            {
                ViewState["bIsLease"] = "false";
            }
            string content = "人数限制：最少" + setValue.dwMinUsers.ToString() + "人&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "最多:" + setValue.dwMaxUsers.ToString() + "人"+"<br />";;
                       
            int intDate = 0;
            if (bIsLongTime)
            {
                divFreeTime.Style.Add("display", "none");
                divLimit.Style.Add("display", "none");
                divLongTime.Style.Add("display", "block");
                int nDate = int.Parse(szDate);
                DateTime dtDate = new DateTime(nDate / 10000, (nDate % 10000) / 100, nDate % 100);
                //提前预约具体时间
                content += "预约限制：" + DateTime.Now.AddDays((uint)setResvRule.dwLatestResvTime / 1440).ToString("yyyy-MM-dd") + "到" + DateTime.Now.AddDays((uint)setResvRule.dwEarliestResvTime / 1440).ToString("yyyy-MM-dd")+" &nbsp;&nbsp;&nbsp;&nbsp; 每次预约不少于" + ((uint)setResvRule.dwMinResvTime / 1440) + "天" + "&nbsp;&nbsp;&nbsp;&nbsp;" + "不大于" + ((uint)setResvRule.dwMaxResvTime / 1440) + "天";
                content += "<br />";
            }
            else
            {
                ViewState["dwMaxResvTime"] = (uint)setResvRule.dwMaxResvTime;
                content += "预约限制：" + DateTime.Now.AddDays((uint)setResvRule.dwLatestResvTime / 1440).ToString("yyyy-MM-dd") + "到" + DateTime.Now.AddDays((uint)setResvRule.dwEarliestResvTime / 1440).ToString("yyyy-MM-dd")+ " &nbsp;&nbsp;&nbsp;&nbsp; 每次预约不少于" + ((uint)setResvRule.dwMinResvTime / 60) + "小时" + "&nbsp;&nbsp;&nbsp;&nbsp;" + "不大于" + ((uint)setResvRule.dwMaxResvTime / 60) + "个小时";
                content += "<br />";
            }
          
            string szCheck = "";
            //20140504前服务为 if (((uint)setResvRule.dwLimit & (uint)UNIRESVRULE.DWLIMIT.RESVLIMIT_CENSOR) > 0)
                if (setResvRule.CheckTbl != null)
            {
                szCheck += "需管理员审核<br />";
            }
            if (((uint)setResvRule.dwLimit & (uint)UNIRESVRULE.DWLIMIT.RESVLIMIT_NEEDAPP) > 0)
            {
                // AppDocDiv.Style.Add("display", "block");
                szCheck += "&nbsp;&nbsp;需提交申请报告";
            }
            else
            {
                divUpLoadFile.Style.Add("display", "none");
            }
            if (szCheck != "")
            {
                content += "<br />审核要求：" + szCheck;
            }
            int nLimit = (int)setValue.dwOpenLimit;
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
                szMemo.Text = vtDevCls[0].szMemo.ToString();
                int nKind=(int)vtDevCls[0].dwClassKind;
                if (((nKind) & ((int)UNIDEVCLS.DWKIND.CLSKIND_COMPUTER)) > 0 || ((nKind) & ((int)UNIDEVCLS.DWKIND.CLSKIND_SEAT)) > 0)
                {
                    ViewState["isAutoAssign"] = "false";
                }
                if (((nKind) & ((int)UNIDEVCLS.DWKIND.CLSKIND_COMMONS)) > 0)
                {
                    string szlblMemo = "";
                    if (vtRes[0].szKindName.ToString().IndexOf("研究") >= 0)
                    {
                        szlblMemo = "研讨内容：";
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
                    ViewState["IsMemo"] ="true";
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
            if (!bIsLongTime&&vtDayOpenRule != null && vtDayOpenRule.Length > 0 && (nLimit & (uint)GROUPOPENRULE.DWOPENLIMIT.OPENLIMIT_FIXEDTIME) == 0)
            {
                divFreeTime.Style.Add("display", "block");
                divLimit.Style.Add("display","none");
                divLongTime.Style.Add("display", "none");
                int nStart = (int)vtDayOpenRule[0].dwBegin;
                int nEnd = (int)vtDayOpenRule[0].dwEnd;

                ArrayList alistStart = new ArrayList();
                string szUse=setValue.szUsableNumArray.ToString();
                ViewState["szUsableNumArray"] = szUse;
                ViewState["dwEnd"] = nEnd;
                uint n=uint.Parse(szTime.Replace(":",""));
                alistStart = ddlBind.GetBeginTimeList(n / 100 * 60 + n % 100, (uint)(nEnd / 100 * 60 + nEnd % 100), (uint)setResvRule.dwMinResvTime, 30, szUse);
                if (n < nEnd)
                {
                    for (int i = 0; i <= alistStart.Count - 1; i++)
                    {
                        uint nTemp = (uint)alistStart[i];
                        ListItem item = new ListItem((nTemp / 100).ToString("00") + ":" + (nTemp % 100).ToString("00"), nTemp.ToString());
                        ddlHourStart.Items.Add(item);
                    }
                    if (alistStart.Count == 0)
                    {
                        ListItem item = new ListItem("无合适时间", "0");
                        ddlHourStart.Items.Add(item);
                    }
                }
                else
                {
                    ListItem item = new ListItem("无合适时间", "0");
                    ddlHourStart.Items.Add(item);
                }
                string szEndF = ddlHourStart.SelectedValue.ToString();
                uint nEndF = 0;
                if (szEndF != null && szEndF != "")
                {
                    nEndF = uint.Parse(szEndF);
                }
               
                ArrayList alistEnd = new ArrayList();
                uint nEndReal = 0;
                if ((nEndF / 100 * 60 + nEndF % 100 + (uint)setResvRule.dwMaxResvTime) < (nEnd / 100 * 60 + nEnd % 100))
                {
                    nEndReal = nEndF / 100 * 60 + nEndF % 100 + (uint)setResvRule.dwMaxResvTime;
                }
                else
                {
                    nEndReal = (uint)(nEnd / 100 * 60 + nEnd % 100);
                }

                alistEnd = ddlBind.GetEndTimeList(nEndF / 100 * 60 + nEndF % 100, nEndReal, (uint)setResvRule.dwMinResvTime, 30, szUse);
               
                if (nEndF != 0)
                {
                    for (int i = 0; i <= alistEnd.Count - 1; i++)
                    {
                        uint nTemp = (uint)alistEnd[i];
                        ListItem item = new ListItem((nTemp / 100).ToString("00") + ":" + (nTemp % 100).ToString("00"), nTemp.ToString());
                        Logger.trace("end1:"+item.Value.ToString());
                        ddlHourEnd.Items.Add(item);
                    }
                    if (alistEnd.Count == 0)
                    {
                        ListItem item = new ListItem("无合适时间", "0");
                        
                        ddlHourEnd.Items.Add(item);
                    }
                }
                else
                {
                    ListItem item = new ListItem("无合适时间", "0");
                    ddlHourEnd.Items.Add(item);
                }                
                content += "开放时间：" + nStart / 100 + ":" + (nStart % 100).ToString("00") + "到" + nEnd / 100 + ":" + (nEnd % 100).ToString("00");
            }
            else if (!bIsLongTime&&vtDayOpenRule != null && vtDayOpenRule.Length > 0 && (nLimit & (uint)GROUPOPENRULE.DWOPENLIMIT.OPENLIMIT_FIXEDTIME) > 0)
            {

                divFreeTime.Style.Add("display", "none");
                divLimit.Style.Add("display", "block");
                divLongTime.Style.Add("display", "none");
                int len = vtDayOpenRule.Length;
                List<DAYOPENRULE> list = new List<DAYOPENRULE>();
                
                for (int i = 0; i < len; i++)
                {
                    
                    uint nStart=(uint)vtDayOpenRule[i].dwBegin;
                    nStart = (nStart / 100) * 60 + nStart % 100;
                    uint nEnd=(uint)vtDayOpenRule[i].dwEnd;
                    nEnd = (nEnd / 100) * 60 + nEnd % 100;
                    if(!ddlBind.GetIsReserve(nStart,nEnd,setValue.szUsableNumArray.ToString()))
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
                    ListItem itemAll = new ListItem("无合适时间","0");
                    ddlPartTime.Items.Add(itemAll);
                }
            }
            divUserLimit.InnerHtml = content;
           // aBack.Attributes.Add("href",(string)Session["szBackPage"]);

        }
    }
    protected void Button1_ServerClick(object sender, EventArgs e)
    {
        Bind ddlBind = new Bind();
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        if (ViewState["IsMemo"] != null && (string)ViewState["IsMemo"]=="true")
        {
            if (txtMemo.Text == "" || txtMemo.Text.IndexOf("必填内容") >= 0)
            {
                string szMemoVW = (string)ViewState["szlblMemo"];
                if (szMemoVW != null)
                {
                    ddlBind.MessageBoxShow(szMemoVW.Replace("：", "") + "必须填写", this.Page);
                    return;
                }
            }
        }
        string devID = Request["dev"];// radDevList.SelectedItem.Value.ToString();
        UNIDEVICE setDev = new UNIDEVICE();
        if (devID != null && devID != "")
        {
            DEVREQ vrGet = new DEVREQ();
            vrGet.dwDevID = ToUInt(devID);
            UNIDEVICE[] vtDev;
            uResponse = m_Request.Device.Get(vrGet, out vtDev);
            if (uResponse != REQUESTCODE.EXECUTE_SUCCESS || vtDev == null || vtDev.Length <= 0)
            {
                return;
            }
            setDev = vtDev[0];
        }
        string kindID = Request["devkind"];

        if (kindID != null && kindID != "")
        {
            DEVKINDREQ vrDevKindReq = new DEVKINDREQ();
            vrDevKindReq.dwKindID = ToUInt(kindID);
            UNIDEVKIND[] vtDevKindRes;
            uResponse = m_Request.Device.DevKindGet(vrDevKindReq, out vtDevKindRes);
            if(vtDevKindRes!=null&&vtDevKindRes.Length>0)
            {
                setDev.szDevName = vtDevKindRes[0].szKindName.ToString();
            }
            setDev.dwKindID = ToUInt(kindID);
        }

        object objAccno = Session["LOGIN_ACCINFO"];
        if (objAccno == null)
        {
            return;//保存信息有丢失
        }
        UNIACCOUNT vrAccno = (UNIACCOUNT)objAccno;

        UNIRESERVE resvSet = new UNIRESERVE();
        bool bIsByKind = false;
        if (kindID != null && kindID != "")
        {
            bIsByKind = true;

        }
        if (ViewState["isAutoAssign"] == null)
        {
            if (bIsByKind)
            {
                resvSet.dwProperty = (uint)UNIRESERVE.DWPROPERTY.RESVPROP_AUTOASSIGN;
            }
        }
        resvSet.dwUseMode = (uint)UNIRESERVE.DWUSEMODE.RESVUSE_LEASE;
        if (groupIDHidden.Value.ToString() != "")
        {
            resvSet.dwMemberKind = (uint)UNIRESERVE.DWMEMBERKIND.MEMBERKIND_GROUP;
            resvSet.dwMemberID = ToUInt(groupIDHidden.Value.ToString());
            resvSet.szOwnerName = vrAccno.szTrueName.ToString() + "group:" + groupIDHidden.Value.ToString();
        }
        else
        {
            resvSet.dwMemberKind = (uint)UNIRESERVE.DWMEMBERKIND.MEMBERKIND_PERSONNAL;
            resvSet.dwMemberID = vrAccno.dwAccNo;
            resvSet.szMemberName = vrAccno.szTrueName.ToString();
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
            if (setDev.szDevName != null)
            {
                resvDev[0].szDevName = setDev.szDevName.ToString();
            }
            //实验室
            string szLabID = Request["labid"];
            if (szLabID == null || szLabID == "")
            {
                ddlBind.MessageBoxShow("", this.Page);
                return;
            }
            resvSet.dwLabID = ToUInt(szLabID);

        }
        else
        {
            resvDev[0] = new RESVDEV();
            resvDev[0].dwDevStart = setDev.dwDevSN;
            resvDev[0].dwDevEnd = setDev.dwDevSN;
            //实验室
            resvSet.szLabName = setDev.szLabName.ToString();
            resvSet.dwLabID = setDev.dwLabID;

        }
        //resvSet.szResvDevs=resvDev; 20140821前
        resvSet.ResvDev=resvDev;

        //设置时间
      
        string szDate = (string)Request["date"];
        if (szDate == null || szDate == "")
        {
            //ddlBind.MessageBoxShow("预约失败请重新选择空间预约", this.Page);
            return;
        }
        int intDate = int.Parse(szDate);
        if (ViewState["nLimit"] != null && ViewState["nLimit"].ToString() == "2")
        {
            szDate = (int.Parse(szDate) / 10000).ToString() + "-" + ((int.Parse(szDate) % 10000) / 100).ToString("00") + "-" + ((int.Parse(szDate) % 10000) % 100).ToString("00");

            string szDDLSelectValue = ddlPartTime.SelectedValue;
            int intDDLSelectValue = int.Parse(szDDLSelectValue);
            int intStartHour = ((intDDLSelectValue / 10000) / 100);
            int intStartMin = ((intDDLSelectValue / 10000) % 100);

            int intEndHour = ((intDDLSelectValue % 10000) / 100);
            int intEndMin = ((intDDLSelectValue % 10000) % 100);
        
            resvSet.dwBeginTime = ToUInt(ddlBind.Get1970Seconds(szDate) + intStartHour * 3600 + intStartMin * 60);
            resvSet.dwEndTime = ToUInt(ddlBind.Get1970Seconds(szDate) + intEndHour * 3600 + intEndMin * 60);

        }
        else
        {
            
            if (ViewState["bIsLongTime"].ToString() == "true")
            {
                string strStartTime = lblDate.Text.ToString();
                string strEndTime = lblDate.Text.ToString();
                resvSet.dwBeginTime =ToUInt( Get1970Seconds(strStartTime));
                resvSet.dwEndTime =ToUInt( Get1970Seconds(strEndTime));

            }
            else
            {
                string strStartTime = lblDate.Text + " " + ddlHourStart.SelectedItem.Text.ToString();
                string strEndTime = lblDate.Text + " " + ddlHourEnd.SelectedItem.Text.ToString();// "2012-07-23" ddlBind.ConvertDateToDisplay(intDate)
                resvSet.dwBeginTime =ToUInt( Get1970Seconds(strStartTime));
                resvSet.dwEndTime =ToUInt( Get1970Seconds(strEndTime));

            }
        }
        if(  ViewState["bIsLongTime"]!=null&& ViewState["bIsLongTime"].ToString() == "true")
        {
            string strStartTime = startDate.Value.ToString();
            string strEndTime = endDate.Value.ToString();
            resvSet.dwBeginTime =ToUInt( Get1970Seconds(strStartTime));
            resvSet.dwEndTime =ToUInt( Get1970Seconds(strEndTime));
        }
        //其他
        /*
        resvSet.dwBeginSec = 1);
        resvSet.dwEndSec = 2);
        resvSet.dwUseMode = 3);
        resvSet.dwDevKind = 4);
        resvSet.dwDevNum = 5);
        */
        if (ViewState["AppResvDoc"] != null)
        {
            resvSet.szApplicationURL = ViewState["AppResvDoc"].ToString();
        }
        resvSet.szTestName = txtMemo.Text.ToString();
        //TODO测试使用
       // resvSet.dwBeginTime = Get1970Seconds("2012-11-14 16:53"));
        //resvSet.dwEndTime = Get1970Seconds("2012-11-14 17:53"));

        uResponse = m_Request.Reserve.Set(resvSet, out resvSet);
        Bind ddlbind = new Bind();
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {

            string error = m_Request.szErrMessage.ToString();
            ddlbind.MessageBoxShow(error, this.Page);
            //预约不成功
        }
        else
        {
            RESVDEV[] vtResvDev;
            //vtResvDev=resvSet.szResvDevs; 20140821前
            vtResvDev=resvSet.ResvDev;
            ddlbind.MessageBoxShow("您已成功预约" + vtResvDev[0].szDevName.ToString(), this.Page);
            if (ViewState["AppResvDoc"] != null)
            {
                string szFilePath = ViewState["AppResvDoc"].ToString();
                if (System.IO.File.Exists(szFilePath))
                {
                    int idx = szFilePath.LastIndexOf(".");
                    string suffix = szFilePath.Substring(idx);
                    int index = szFilePath.LastIndexOf("\\");
                    string szFilePathNew = szFilePath.Substring(0, index) + "\\" + resvSet.dwResvID.ToString() + suffix;
                    // System.IO.File.Move(szFilePath, szFilePathNew);

                }
            }

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
    protected void Button2_ServerClick(object sender, EventArgs e)
    {      
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
        setValue.szMemo = vtRes[0].szLogonName.ToString() + ":" + vtRes[0].szTrueName.ToString();
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
            if (groupID != "")
            {

            }
            else
            {
              
                ddlBind.MessageBoxShow("添加人员失败，请重新预约", this.Page);
                return REQUESTCODE.EXECUTE_FAIL;
            }
        }
        res = ToUInt(groupIDHidden.Value.ToString());
        GROUPMEMBER setGroupMemberRes = new GROUPMEMBER();
        setGroupMemberRes = setGroupMember;
        setGroupMemberRes.dwStatus = (uint)GROUPMEMBER.DWSTATUS.GROUPMEMBERSTAT_FORCE;
        setGroupMemberRes.dwGroupID = res;
        uResponse = m_Request.Group.SetGroupMember(setGroupMemberRes);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {           
            ddlBind.MessageBoxShow(m_Request.szErrMessage.ToString(), this.Page);
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
            if (vtGroupMember != null)
            {
                //DataTable dt;
                //ddlBind.VtTableConvert(vtGroupMember, out dt, null, null);//must
                //GV.DataSource = dt;
                //GV.DataBind();
            }
        }
    }
    protected uint newGroup()
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        object obj = Session["LOGIN_ACCINFO"];
        UNIACCOUNT acc = new UNIACCOUNT();
        if (obj != null)
        {
            acc = (UNIACCOUNT)obj;
        }
        UNIGROUP setValue = new UNIGROUP();
        setValue.dwKind = ((uint)UNIGROUP.DWKIND.GROUPKIND_RERV);
        setValue.szName = acc.szLogonName.ToString() + DateTime.Now.ToLongTimeString();
        setValue.dwEnrollDeadline = ToUInt(DateTime.Now.AddYears(10).ToString("yyyyMMdd"));
        uResponse = m_Request.Group.SetGroup(setValue, out setValue);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && setValue.dwGroupID != null)
        {
            groupIDHidden.Value = setValue.dwGroupID.ToString();

            GROUPMEMBER setValueMember = new GROUPMEMBER();
            setValueMember.dwKind = (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL;
            setValueMember.dwMemberID = acc.dwAccNo;
            setValueMember.szName = acc.szTrueName.ToString();
            setValueMember.szMemo = acc.szLogonName.ToString() + ":" + acc.szTrueName.ToString();
            SetGroup(setValueMember);

            return (uint)setValue.dwGroupID;
        }
        else
        {
            return 0;
        }
    }
    protected void GV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument.ToString());
        string memberid = GV.Rows[index].Cells[0].Text.ToString();
        string kind = GV.Rows[index].Cells[3].Text.ToString();
        string groupID = groupIDHidden.Value.ToString();
        GROUPMEMBER setValue = new GROUPMEMBER();
        setValue.dwGroupID = ToUInt(groupID);
        setValue.dwKind = ToUInt(kind);
        setValue.dwMemberID = ToUInt(memberid);
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        uResponse = m_Request.Group.DelGroupMember(setValue);
        showGroupMember(groupID);
    }
    protected void UploadButton_Click(object sender, EventArgs e)
    {
        Bind ddlBind = new Bind();
        string uploadName = InputFile.Value;//获取待上传图片的完整路径，包括文件名 
        //string uploadName = InputFile.PostedFile.FileName; 
        string pictureName = DateTime.Now.ToString("yyyyMMddHHmmss");//上传后的图片名，以当前时间为文件名，确保文件名没有重复 
        if (InputFile.Value != "")
        {
            int idx = uploadName.LastIndexOf(".");
            string suffix = uploadName.Substring(idx);//获得上传的图片的后缀名 
            pictureName = pictureName + suffix;
        }
        try
        {
            if (uploadName != "")
            {
                string path = Server.MapPath("~/UpLoadFile/AppResvDoc/");
                InputFile.PostedFile.SaveAs(path + pictureName);
                ViewState["AppResvDoc"] = "../UpLoadFile/AppResvDoc/" + pictureName;
                ddlBind.MessageBoxShow("上传成功", this.Page);
            }
        }
        catch (Exception ex)
        {
            Session["UpLoadPic"] = "";
            Response.Write(ex);
        }
    }
    protected void ddlHourStart_SelectedIndexChanged1(object sender, EventArgs e)
    {
        ddlHourEnd.Items.Clear();
        Bind ddlBind = new Bind();
        string szUse=ViewState["szUsableNumArray"].ToString();
        string szEnd=ViewState["dwEnd"].ToString();
        string szStartHor = ddlHourStart.SelectedValue.ToString();
        uint n = uint.Parse(szStartHor);
        int nEndF = int.Parse(szEnd);
        ArrayList alistEnd = new ArrayList();
        uint nAddMin=480;
        if (ViewState["dwMaxResvTime"] != null)
        {
            nAddMin =uint.Parse(ViewState["dwMaxResvTime"].ToString());
        }
        uint nEnd = uint.Parse(szEnd);
        uint nEndReal=0;
        if(nEnd/100*60+nEnd%100< (n / 100 * 60 + n % 100) + nAddMin)
        {
            nEndReal=nEnd/100*60+nEnd%100;
        }   
        else
        {
            nEndReal=(n / 100 * 60 + n % 100) + nAddMin;
        }
        alistEnd = ddlBind.GetEndTimeList(n / 100 * 60 + n % 100, nEndReal, 60, 30, szUse);
        for (int i = 0; i <= alistEnd.Count - 1; i++)
        {
            uint nTemp = (uint)alistEnd[i];
            ListItem item = new ListItem((nTemp / 100).ToString("00") + ":" + (nTemp % 100).ToString("00"), nTemp.ToString());
            Logger.trace("end2:" + item.Value.ToString());
            ddlHourEnd.Items.Add(item);
        }
        if(alistEnd.Count==0)
        {
            ListItem item = new ListItem("无合适时间", "0");
            ddlHourEnd.Items.Add(item);
        }
    }
    protected void btnBack_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect((string)Session["szBackPage"]);
        //aBack.Attributes.Add("href", );
    }
}
