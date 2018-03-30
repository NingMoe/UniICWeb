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
    protected string szCamp = "";
    protected string szBuilding = "";
    protected string szKinds = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        UNICAMPUS[] vtCamp = GetAllCampus();
        if (vtCamp != null && vtCamp.Length > 0)
        {
            for (int i = 0; i < vtCamp.Length; i++)
            {
                szCamp += GetInputItemHtml(CONSTHTML.option, "", vtCamp[i].szCampusName, vtCamp[i].dwCampusID.ToString());
            }
        }
        UNIBUILDING[] vtBuilding = getAllBuilding();
        for (int i = 0; i < vtBuilding.Length; i++)
        {
            if (vtBuilding[i].dwCampusID.ToString() == vtCamp[0].dwCampusID.ToString())
            {
                szBuilding += GetInputItemHtml(CONSTHTML.option, "", vtBuilding[i].szBuildingName.ToString(), vtBuilding[i].dwBuildingID.ToString());
            }
        }


        if (IsPostBack)
        {
            UNIGROUP resvGroup;
            string[] szOwnerList = Request["szowner"].ToString().Split(';');
            string szDevIDS = Request["devidchk"];
            string szStartDate = Request["szStartDate"];
            string szEndDate = Request["szEndDate"];
            string szSelectWeek = Request["weekSelect"];
            string szStartTime = Request["startTimeHour"] + ":" + Request["startTimeMin"];
            string szEndTime = Request["endTimeHour"] + ":" + Request["endTimeMin"];
            if (szDevIDS == null || szDevIDS == "")
            {
                MessageBox("未选占用择对象", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
            string[] szDevIDList = szDevIDS.Split(',');

            uint uCount = 0;
            uint uCountFali = 0;
            uint uResvGroupID = 0;
            for (int m = 0; m < szDevIDList.Length; m++)
            {
                string szDevID = szDevIDList[m];
                if (szDevID == "")
                {
                    continue;
                }
                UNIRESERVE setValue = new UNIRESERVE();

                UNIDEVICE dev = new UNIDEVICE();
                if (getDevByID(szDevID, out dev))
                {

                    UNIACCOUNT vrAccInfo = ((ADMINLOGINRES)Session["LoginResult"]).AccInfo;
                    setValue.dwMemberID = vrAccInfo.dwAccNo;
                    setValue.szMemberName = vrAccInfo.szTrueName;
                    setValue.dwMemberKind = (uint)UNIRESERVE.DWMEMBERKIND.MEMBERKIND_PERSONNAL;

                    setValue.dwOwner = vrAccInfo.dwAccNo;
                    setValue.szOwnerName = vrAccInfo.szTrueName;
                    setValue.szTestName = Request["szMemo"];


                    setValue.ResvDev = new RESVDEV[1];
                    setValue.ResvDev[0].dwDevEnd = dev.dwDevSN;
                    setValue.ResvDev[0].dwDevStart = dev.dwDevSN;
                    setValue.ResvDev[0].dwDevNum = 1;
                    setValue.ResvDev[0].dwDevKind = dev.dwKindID;
                    setValue.ResvDev[0].szRoomNo = dev.szRoomNo;
                    setValue.ResvDev[0].szDevName = dev.szDevName;
                    setValue.dwLabID = dev.dwLabID;
                    setValue.szLabName = dev.szLabName;
                    setValue.dwResvGroupID=

                    setValue.dwProperty = (uint)UNIRESERVE.DWPROPERTY.RESVPROP_SELFDO;
                    setValue.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL + (uint)UNIRESERVE.DWPURPOSE.USEFOR_RESERVED + (uint)UNIRESERVE.DWPURPOSE.USEFOR_STUDYROOM;


                }
                REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;

                DateTime startDate = DateTime.Parse(szStartDate);
                DateTime endDate = DateTime.Parse(szEndDate);
                TimeSpan span = endDate - startDate;
           
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
                        if (uResvGroupID != 0)
                        {
                            setValue.dwResvGroupID = uResvGroupID;
                        }
                        UNIRESERVE resvTemp = setValue;
                        if (m_Request.Reserve.Set(setValue, out setValue) == REQUESTCODE.EXECUTE_SUCCESS)
                        {
                            if (uResvGroupID == 0)
                            {
                                uResvGroupID = (uint)setValue.dwResvID;
                            }
                            uCount = uCount + 1;
                        }
                        else
                        {
                            uCountFali = uCountFali + 1;
                        }
                        setValue = resvTemp;
                        setValue.dwResvID = null;

                        UNIACCOUNT vrAccInfo = ((ADMINLOGINRES)Session["LoginResult"]).AccInfo;
                        setValue.dwOwner = vrAccInfo.dwAccNo;
                        setValue.szOwnerName = vrAccInfo.szTrueName;
                    }
                    startDate = startDate.AddDays(1);
                    span = endDate - startDate;
                }
            }

            MessageBox("预约成功【" + uCount + "】条", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
            return;

        }
        for (int i = 0; i < szWeekDayList.Length; i++)
        {
            szWeek += GetInputItemHtml(CONSTHTML.checkBox, "weekSelect", szWeekDayList[i], (i + 1).ToString());
        }
        for (int i = 8; i < 23; i++)
        {
            TimeHour += GetInputItemHtml(CONSTHTML.option, "", i.ToString("00"), (i).ToString());
        }

        for (int i = 0; i < 60; i = i + 5)
        {
            TimeMin += GetInputItemHtml(CONSTHTML.option, "", i.ToString("00"), (i).ToString());
        }
        if (!IsPostBack)
        {
            string szDevID = Request["id"];
            if (szDevID == null || szDevID == "")
            {
                return;
            }
            UNIDEVICE resvDev = new UNIDEVICE();
            if (getDevByID(szDevID, out resvDev))
            {
                PutMemberValue("divdevName", resvDev.szDevName);
                PutMemberValue("szDevID", resvDev.dwDevID.ToString());
            }
        }
        m_Title = "管理员新建预约";

    }
}
