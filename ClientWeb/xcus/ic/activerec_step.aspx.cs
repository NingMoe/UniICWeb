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
using System.Text.RegularExpressions;
using System.Reflection;
using UniWebLib;
using System.IO;

public partial class Page_ : UniClientPage
{
    protected string szInfo = "空间不存在";
    protected int dwMinUsersin = 0;
    protected int dwMaxUsersin = 0;
    protected string szResult = "";
    UNIDEVICE CurDev;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request["devkind"]) || string.IsNullOrEmpty(Request["dev"]))
        {
            MsgBox("参数有误");
            return;
        }

        if (Session["LOGIN_ACCINFO"] != null)
        {
            UNIACCOUNT acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
            szContact.Value = acc.szTrueName;
            szHandPhone.Value = acc.szHandPhone;
            szEmail.Value = acc.szEmail;
        }
        dwBeginTime.Value = Request["time"];
        dwPublishDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
        if (!string.IsNullOrEmpty(Request["date"]))
        {
            string str = Request["date"];
            string dt = str.Substring(0, 4) + "-" + str.Substring(4, 2) + "-" + str.Substring(6, 2);
            dwActivityDate.Value = dt;
            dwEnrollDeadline.Value = dt;
        }
        base.LoadPage();
        uint dwKindID = 0;
        uint.TryParse(Request["devkind"], out dwKindID);
        UNIDEVICE[] devs = GetDevById(Request["dev"]);
        if (devs != null && devs.Length > 0)
            CurDev = devs[0];
        DEVKINDFORRESVREQ vrParameter = new DEVKINDFORRESVREQ();
        DEVKINDFORRESV[] vrResult;
        vrParameter.szKindIDs = dwKindID.ToString();
        vrParameter.dwClassKind = (uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS;
        vrParameter.dwResvPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL;
        vrParameter.dwDate = (uint)(DateTime.Now.Year * 10000 + DateTime.Now.Month * 100 + DateTime.Now.Day);
        if (m_Request.Device.GetDevKindForResv(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            if (vrResult.Length > 0)
            {
                szInfo = "";
                UNIRESVRULE Ruleinfo = vrResult[0].szRuleInfo;
                DAYOPENRULE[] OpenInfo = vrResult[0].szOpenInfo;

                string szOpen = "";
                string szClose = "";
                for (int i = 0; i < OpenInfo.Length; i++)
                {
                    if (OpenInfo[i].dwOpenPurpose != 0)
                    {
                        szOpen = OpenInfo[i].dwBegin / 100 % 100 + ":" + OpenInfo[i].dwBegin % 100;
                        szClose = OpenInfo[i].dwEnd / 100 % 100 + ":" + ((uint)(OpenInfo[i].dwEnd % 100)).ToString("00");
                    }
                }

                szInfo += "<b>" + vrResult[0].szKindName.ToString() + "</b>【" + CurDev.szDevName + "】<br/>";
                if (Ruleinfo.dwLatestResvTime > 0)
                {
                    szInfo += "最少提前【" + GetTimeSpan((uint)Ruleinfo.dwLatestResvTime) + "】预约&nbsp;&nbsp;";
                }
                if (Ruleinfo.dwEarliestResvTime > 0)
                {
                    szInfo += "最多提前【" + GetTimeSpan((uint)Ruleinfo.dwEarliestResvTime) + "】预约&nbsp;&nbsp;";
                }
                szInfo += "<br/>每次预约";
                if (Ruleinfo.dwMinResvTime > 0)
                {
                    szInfo += "不少于【" + GetTimeSpan((uint)Ruleinfo.dwMinResvTime) + "】&nbsp;&nbsp;";
                }
                if (Ruleinfo.dwMaxResvTime > 0)
                {
                    szInfo += "不大于【" + GetTimeSpan((uint)Ruleinfo.dwMaxResvTime) + "】&nbsp;&nbsp;";
                }
                szInfo += "<br/>成员人数";
                if (vrResult[0].dwMinUsers > 0)
                {
                    dwMinUsersin = (int)vrResult[0].dwMinUsers;
                    szInfo += "【" + vrResult[0].dwMinUsers.ToString() + "】人&nbsp;";
                }
                if (vrResult[0].dwMaxUsers > 0)
                {
                    dwMaxUsersin = (int)vrResult[0].dwMaxUsers;
                    szInfo += "到&nbsp;【" + vrResult[0].dwMaxUsers.ToString() + "】人";
                }
                szInfo += "<br/>";

                if (szOpen != "" && szClose != "")
                {
                    szInfo += "开放时间:" + szOpen + "&nbsp; 至&nbsp;" + szClose + "<br/>";
                }
                dwMinUsers.Value = vrResult[0].dwMinUsers.ToString();
                dwMaxUsers.Value = vrResult[0].dwMaxUsers.ToString();
            }
        }
    }

    string GetTimeSpan(uint dwValue)
    {
        string szValue = "";
        if (dwValue >= 1440)
        {
            szValue = (dwValue / 1440) + "天";
            dwValue = dwValue % 1440;
        }
        if (dwValue >= 60)
        {
            szValue += (dwValue / 60) + "小时";
            dwValue = dwValue % 60;
        }
        if (dwValue < 60 && dwValue > 0)
        {
            szValue += dwValue + "分钟";
        }
        if (dwValue == 0 && szValue == "")
        {
            szValue = "0分钟";
        }
        return szValue;
    }
    protected void btnSubmint_Click(object sender, EventArgs e)
    {
        UNIACTIVITYPLAN vrParameter = new UNIACTIVITYPLAN();
        //vrParameter.dwActivityPlanID = UniSZ2DW(Request["dwActivityPlanID"]);
        vrParameter.szActivityPlanName = Request["szActivityPlanName"];
        vrParameter.szHostUnit = Request["szHostUnit"];
        vrParameter.szOrganizer = Request["szOrganizer"];
        vrParameter.szPresenter = Request["szPresenter"];
        vrParameter.szDesiredUser = Request["szDesiredUser"];
        //vrParameter.dwCheckRequirment = UniSZ2DW(Request["dwCheckRequirment"]);
        vrParameter.szContact = Request["szContact"];
        //vrParameter.szTel = Request["szTel"];
        vrParameter.szHandPhone = Request["szHandPhone"];
        vrParameter.dwCheckRequirment = (uint)UNIACTIVITYPLAN.DWCHECKREQUIRMENT.ACTIVITYPLAN_NOAPPLY;
        vrParameter.dwKind = (uint)UNIACTIVITYPLAN.DWKIND.ACTIVITYPLANKIND_SALON;
        vrParameter.szEmail = Request["szEmail"];
        //vrParameter.dwResvID = UniSZ2DW(Request["dwResvID"]);
        //vrParameter.dwGroupID = UniSZ2DW(Request["dwGroupID"]);
        vrParameter.dwMaxUsers = UniSZ2DW(Request["dwMaxUsers"]);
        vrParameter.dwMinUsers = UniSZ2DW(Request["dwMinUsers"]);
        //vrParameter.dwEnrollUsers = UniSZ2DW(Request["dwEnrollUsers"]);
        vrParameter.dwEnrollDeadline = UniSZ2DW(Request["dwEnrollDeadline"]);
        vrParameter.dwPublishDate = UniSZ2DW(Request["dwPublishDate"]);
        vrParameter.dwActivityDate = UniSZ2DW(Request["dwActivityDate"]);
        vrParameter.dwBeginTime = UniSZ2DW(Request["dwBeginTime"]);
        vrParameter.dwEndTime = UniSZ2DW(Request["dwEndTime"]);
        vrParameter.szSite = CurDev.szLabName + "【" + CurDev.szDevName + "】";//Request["szSite"];
        vrParameter.dwDevID = CurDev.dwDevID;
        vrParameter.szRoomNo = CurDev.szRoomNo;
        //vrParameter.dwKind = UniSZ2DW(Request["dwKind"]);
        vrParameter.dwStatus = (uint)UNITESTPLAN.DWSTATUS.TESTPLANSTAT_UNOPEN;
        vrParameter.szIntroInfo = Request["szIntroInfo"];
        vrParameter.szMemo = Request["szMemo"];
        if (vrParameter.szActivityPlanName.ToString() == "")
        {
            MsgBox("请填写活动名称");
            return;
        }
        if (vrParameter.szHostUnit.ToString() == "")
        {
            MsgBox("请填写主办单位");
            return;
        }
        if (vrParameter.szPresenter.ToString() == "")
        {
            MsgBox("请填写主持人");
            return;
        }
        if (vrParameter.szContact.ToString() == "")
        {
            MsgBox("请填写联系人");
            return;
        }
        if (vrParameter.szHandPhone.ToString() == "")
        {
            MsgBox("请填写联系电话");
            return;
        }
        if (vrParameter.dwActivityDate.ToString() == "")
        {
            MsgBox("请填写活动日期");
            return;
        }
        if (vrParameter.dwBeginTime.ToString() == "")
        {
            MsgBox("请填写开始时间");
            return;
        }
        if (vrParameter.dwEndTime.ToString() == "")
        {
            MsgBox("请填写结束时间");
            return;
        }
        if (vrParameter.szIntroInfo.ToString() == "")
        {
            MsgBox("请填写介绍内容");
            return;
        }
        if (string.IsNullOrEmpty(InputFile.Value))
        {
            MsgBox("请上传申请材料");
            return;
        }
        else
        {
            string ret = UploadFile(InputFile);
            if (ret == "fail")
            {
                MsgBox("上传文件失败");
                return;
            }
            else
            {
                vrParameter.szApplicationURL = ret;
            }
        }
        if (!string.IsNullOrEmpty(szActivityPlanURL.Value))
        {
            string ret = UploadFile(szActivityPlanURL);
            if (ret == "fail")
            {
                MsgBox("上传文件失败");
                return;
            }
            else
            {
                vrParameter.szActivityPlanURL = ret;
            }
        }
        SetActivityPlan(vrParameter);
    }
    private void SetActivityPlan(UNIACTIVITYPLAN vrParameter)
    {
        if (m_Request.Reserve.SetActivityPlan(vrParameter, out vrParameter) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            object obj = Session["LOGIN_ACCINFO"];
            UNIACCOUNT acc = new UNIACCOUNT();
            if (obj != null)
            {
                acc = (UNIACCOUNT)(obj);
            }

            GROUPMEMBER setValueMember = new GROUPMEMBER();
            setValueMember.dwKind = (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL;
            setValueMember.dwMemberID = acc.dwAccNo;
            setValueMember.szName = acc.szTrueName.ToString();
            setValueMember.szMemo = acc.szLogonName.ToString() + ":" + acc.szTrueName.ToString();
            setValueMember.dwGroupID = vrParameter.dwGroupID;
            uResponse = m_Request.Group.SetGroupMember(setValueMember);
            MsgBox("你的申请预约已提交审核，审核结果将由短信形式发送，请注意查收");
        }
        else
        {
            if (vrParameter.dwResvID != null && vrParameter.dwResvID != 0)
            {
                UNIRESERVE para = new UNIRESERVE();
                para.dwResvID = vrParameter.dwResvID;
                m_Request.Reserve.Del(para);
            }
            MsgBox("对不起，申请失败" + m_Request.szErrMessage);
        }
    }
    public static uint UniSZ2DW(string szString)
    {
        uint dw = 0;
        if (string.IsNullOrEmpty(szString))
        {
            return dw;
        }
        string szString2 = Regex.Replace(szString, @"[^0-9]+", "");
        uint.TryParse(szString2, out dw);
        return dw;
    }
    protected string UploadFile(HtmlInputFile InputFile)
    {
        string ret = "fail";
        string uploadName = InputFile.Value;//获取待上传图片的完整路径，包括文件名 
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
                string path = Server.MapPath("~/ClientWeb/upload/UpLoadFile/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                InputFile.PostedFile.SaveAs(path + pictureName);
                ret = pictureName;
            }
        }
        catch (Exception ex)
        {
        }
        return ret;
    }
}
