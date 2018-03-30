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
using UniWebLib;

public partial class _Default : UniPage
{
    protected bool bSet = false;
    protected string m_Title = "";
    protected string m_szSta = "";
    private string szKindName = "";
    protected string szWeek = "";
    protected string TimeHour = "";
    protected string TimeMin = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            UNIGROUP resvGroup;
            string[] szOwnerList = Request["szowner"].ToString().Split(';');
            string szDevID = Request["szDevID"];
            string szStartDate = Request["szStartDate"];
            string szEndDate = Request["szEndDate"];
            string szSelectWeek = Request["weekSelect"];
            string szStartTime = Request["startTimeHour"] + ":" + Request["startTimeMin"];
            string szEndTime = Request["endTimeHour"] + ":" + Request["endTimeMin"];
            UNIRESERVE setValue = new UNIRESERVE();
            if (NewGroup("管理员新建预约", (uint)UNIGROUP.DWKIND.GROUPKIND_RERV, out resvGroup))
            {
                for (int i = 0; i < szOwnerList.Length; i++)
                {
                    UNIACCOUNT acc = new UNIACCOUNT();
                    if (GetAccByAccno(szOwnerList[i], out acc))
                    {
                        AddGroupMember(resvGroup.dwGroupID, acc.dwAccNo, (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL);
                    }
                }


                setValue.dwMemberID = resvGroup.dwGroupID;
                setValue.dwMemberKind = (uint)UNIRESERVE.DWMEMBERKIND.MEMBERKIND_GROUP;
                setValue.dwProperty=0;
                UNIACCOUNT vrAccInfo = ((ADMINLOGINRES)Session["LoginResult"]).AccInfo;
                setValue.dwOwner = vrAccInfo.dwAccNo;
                setValue.szOwnerName = vrAccInfo.szTrueName;
                setValue.szTestName = Request["szMemo"];
                UNIDEVICE[] getDevByHtmlList;
                getDevByHtmlList=GetDevByRoomId(Parse(szDevID));
                if (getDevByHtmlList != null && getDevByHtmlList.Length>0)
                {
                    UNIDEVICE getDevByHtml = new UNIDEVICE();
                    getDevByHtml = getDevByHtmlList[0];
                    UNIROOM getRoom;
                    if (GetRoomID(getDevByHtml.dwRoomID.ToString(), out getRoom))
                    {
                        UNIDEVICE[] devList = GetDevByRoomId(getRoom.dwRoomID);
                        if (devList != null && devList.Length == 1)
                        {
                            setValue.dwProperty +=  (uint)UNIRESERVE.DWPROPERTY.RESVPROP_LOCKROOM;
                        }
                    }
                    setValue.ResvDev = new RESVDEV[1];
                    setValue.ResvDev[0] = new RESVDEV();
                    setValue.ResvDev[0].dwDevEnd = getDevByHtml.dwDevSN;
                    setValue.ResvDev[0].dwDevStart = getDevByHtml.dwDevSN;
                    setValue.ResvDev[0].dwDevNum = 1;
                    setValue.ResvDev[0].dwDevKind = getDevByHtml.dwKindID;
                    setValue.ResvDev[0].szRoomNo = getDevByHtml.szRoomNo;
                    setValue.ResvDev[0].szRoomName = getDevByHtml.szRoomName;
                    setValue.ResvDev[0].szDevName = getDevByHtml.szDevName;
                    setValue.dwLabID = getDevByHtml.dwLabID;
                    setValue.szLabName = getDevByHtml.szLabName;
                }
                setValue.dwProperty = (uint)setValue.dwProperty + (uint)UNIRESERVE.DWPROPERTY.RESVPROP_SELFDO + (uint)UNIRESERVE.DWPROPERTY.RESVPROP_LOCKROOM;
                setValue.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL;
            }
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;

            DateTime startDate = DateTime.Parse(szStartDate);
            DateTime endDate = DateTime.Parse(szEndDate);
            TimeSpan span = endDate - startDate;
            uint uCount = 0;
            uint uCountFali = 0;
            while (span.Days > -1)
            {
                int uweek = (int)startDate.DayOfWeek;
                if (uweek == 0)
                {
                    uweek = 7;
                }
                if (szSelectWeek == null || szSelectWeek == "" || szSelectWeek.IndexOf(uweek.ToString()) > -1)
                {
                    string szStartTimeTemp = startDate.ToString("yyyy-MM-dd") + " " + szStartTime;
                    string szEndTimeTemp = startDate.ToString("yyyy-MM-dd") + " " + szEndTime;

                    setValue.dwBeginTime = Get1970Seconds(szStartTimeTemp);
                    setValue.dwEndTime = Get1970Seconds(szEndTimeTemp);

                    Logger.trace(setValue.ResvDev[0].dwDevStart.ToString());
                    if (m_Request.Reserve.Set(setValue, out setValue) == REQUESTCODE.EXECUTE_SUCCESS)
                    {
                        uCount = uCount + 1;
                    }
                    else
                    {
                        uCountFali = uCountFali + 1;
                    }
                    setValue.dwResvID = null;

                    UNIACCOUNT vrAccInfo = ((ADMINLOGINRES)Session["LoginResult"]).AccInfo;
                    setValue.dwOwner = vrAccInfo.dwAccNo;
                    setValue.szOwnerName = vrAccInfo.szTrueName;
                }
                startDate = startDate.AddDays(1);
                span = endDate - startDate;
            }

            MessageBox("预约成功【" + uCount + "】条", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
            return;

        }
        for (int i = 0; i < szWeekDayList.Length; i++)
        {
            szWeek += GetInputItemHtml(CONSTHTML.checkBox, "weekSelect", szWeekDayList[i], (i + 1).ToString());
        }
        for (int i = 7; i < 23; i++)
        {
            TimeHour += GetInputItemHtml(CONSTHTML.option, "", i.ToString("00"), (i).ToString());
        }

        for (int i = 0; i < 60; i = i + 5)
        {
            TimeMin += GetInputItemHtml(CONSTHTML.option, "", i.ToString("00"), (i).ToString());
        }
        m_Title = "管理员新建预约";

    }
}
