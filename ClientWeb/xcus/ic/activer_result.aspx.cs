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

public partial class Page_ : UniClientPage
{
    protected string szResult = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        base.LoadPage();

        UNIACTIVITYPLAN vrParameter = new UNIACTIVITYPLAN();
        vrParameter.dwActivityPlanID = ToUInt(Request["dwActivityPlanID"]);
        vrParameter.szActivityPlanName = Request["szActivityPlanName"];
        vrParameter.szHostUnit = Request["szHostUnit"];
        vrParameter.szOrganizer = Request["szOrganizer"];
        vrParameter.szPresenter = Request["szPresenter"];
        vrParameter.szDesiredUser = Request["szDesiredUser"];
        vrParameter.dwCheckRequirment = ToUInt(Request["dwCheckRequirment"]);
        vrParameter.szContact = Request["szContact"];
        vrParameter.szTel = Request["szTel"];
        vrParameter.szHandPhone = Request["szHandPhone"];
        vrParameter.dwCheckRequirment = (uint)UNIACTIVITYPLAN.DWCHECKREQUIRMENT.ACTIVITYPLAN_NOAPPLY;
        vrParameter.dwKind = (uint)UNIACTIVITYPLAN.DWKIND.ACTIVITYPLANKIND_SALON;
        vrParameter.szEmail = Request["szEmail"];
        vrParameter.dwResvID = ToUInt(Request["dwResvID"]);
        vrParameter.dwGroupID = ToUInt(Request["dwGroupID"]);
        vrParameter.dwMaxUsers = ToUInt(Request["dwMaxUsers"]);
        vrParameter.dwMinUsers = ToUInt(Request["dwMinUsers"]);
        vrParameter.dwEnrollUsers = ToUInt(Request["dwEnrollUsers"]);
        vrParameter.dwEnrollDeadline = ToUInt(Request["dwEnrollDeadline"]);
        vrParameter.dwPublishDate = ToUInt(Request["dwPublishDate"]);
        vrParameter.dwActivityDate = ToUInt(Request["dwActivityDate"]);
        vrParameter.dwBeginTime = ToUInt(Request["dwBeginTime"]);
        vrParameter.dwEndTime = ToUInt(Request["dwEndTime"]);
        vrParameter.szSite = Request["szSite"];
        vrParameter.dwDevID = 576;
        vrParameter.szRoomNo = GetRoomNoFromDevID("576");
        vrParameter.dwKind = ToUInt(Request["dwKind"]);
        vrParameter.dwStatus =(uint)UNITESTPLAN.DWSTATUS.TESTPLANSTAT_UNOPEN;
        vrParameter.szIntroInfo = Request["szIntroInfo"];
        vrParameter.szMemo = Request["szMemo"];

        HttpPostedFile file = Request.Files["szActivityPlanURL"];
        if (file != null && file.ContentLength > 0)
        {
            string szFilename = Server.UrlDecode(file.FileName);
            int n = szFilename.LastIndexOf("\\");
            if (n >= 0)
            {
                szFilename = szFilename.Substring(n + 1);
            }
            else
            {
                n = szFilename.LastIndexOf("/");
                if (n >= 0)
                {
                    szFilename = szFilename.Substring(n + 1);
                }
            }
            szFilename = @"file_upload\" + DateTime.Now.Ticks + szFilename;
            string szFiles = Server.MapPath(".") + @"\" + szFilename;

            file.SaveAs(szFiles);
            szFilename = szFilename.Replace('\\', '/');
            szFiles = Request.Url.AbsoluteUri;
            n = szFiles.LastIndexOf("/");
            if (n >= 0)
            {
                szFiles = szFiles.Substring(0,n+1);
            }
            vrParameter.szActivityPlanURL = szFiles + szFilename;
        }
        file = Request.Files["szApplicationURL"];
        if (file != null && file.ContentLength > 0)
        {
            string szFilename = Server.UrlDecode(file.FileName);
            int n = szFilename.LastIndexOf("\\");
            if (n >= 0)
            {
                szFilename = szFilename.Substring(n + 1);
            }
            else
            {
                n = szFilename.LastIndexOf("/");
                if (n >= 0)
                {
                    szFilename = szFilename.Substring(n + 1);
                }
            }
            szFilename = @"file_upload\" + DateTime.Now.Ticks + szFilename;
            string szFiles = Server.MapPath(".") + @"\" + szFilename;

            file.SaveAs(szFiles);
            szFilename = szFilename.Replace('\\', '/');
            szFiles = Request.Url.AbsoluteUri;
            n = szFiles.LastIndexOf("/");
            if (n >= 0)
            {
                szFiles = szFiles.Substring(0, n+1);
            }

            vrParameter.szApplicationURL = szFiles + szFilename;
        }

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
            szResult = "<h2>你的申请预约已提交审核</h2><p>审核结果将由短信形式发送，请注意查收</p>";
        }
        else
        {
            if (m_Request.szErrMessage != "")
            {
                szResult = "<h2>对不起，申请失败</h2><p>" + m_Request.szErrMessage + "</p>";
            }
            else
            {
                szResult = "<h2>对不起，申请失败</h2><p>如有疑问，请致电：0571-xxxxxxxx</p>";
            }
        }
    }

    private string GetRoomNoFromDevID(string id)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        DEVREQ vrGet = new DEVREQ();
        vrGet.dwDevID = ToUInt(id);
        vrGet.dwClassKind = (uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS;
        UNIDEVICE[] vtRes;
        uResponse = m_Request.Device.Get(vrGet, out vtRes);
        if (vtRes != null && vtRes.Length > 0)
        {
            return vtRes[0].szRoomNo.ToString();
        }
        return "";
    }
}
