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
    protected string TimeMin= "";
    protected string szResOut = "";
	protected void Page_Load(object sender, EventArgs e)
	{
        if (IsPostBack)
        {
            szResOut += "<table>";
            UNIGROUP resvGroup;
            string[] szOwnerList = Request["szowner"].ToString().Split(';');
            string szDevID = Request["szDevID"];
            string szStartDate = Request["szStartDate"];
            string szEndDate = Request["szEndDate"];
            string szSelectWeek = Request["weekSelect"];
            string szStartTime = Request["startTimeHour"] + ":" + Request["startTimeMin"];
            string szEndTime = Request["endTimeHour"] + ":" + Request["endTimeMin"];
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


                setValue.dwProperty = (uint)UNIRESERVE.DWPROPERTY.RESVPROP_SELFDO;
                setValue.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL + (uint)UNIRESERVE.DWPURPOSE.USEFOR_RESERVED + (uint)UNIRESERVE.DWPURPOSE.USEFOR_STUDYROOM;


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

                    UNIRESERVE resvTemp = setValue;
                    if (m_Request.Reserve.Set(setValue, out setValue) == REQUESTCODE.EXECUTE_SUCCESS)
                    {
                        uCount = uCount + 1;
                    }
                    else
                    {
                        szResOut += "<tr><td>" + szStartTimeTemp + "到" + szEndTimeTemp + "</td><td>" + m_Request.szErrMessage + "</td></tr>";
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
            szResOut += "</table>";
            MessageBox("预约成功【" + uCount + "】条", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.NONE);
            return;

        }  
        for(int i=0;i<szWeekDayList.Length;i++)
        {
            szWeek+=GetInputItemHtml(CONSTHTML.checkBox,"weekSelect",szWeekDayList[i],(i+1).ToString());
        }
        for (int i = 7; i < 23; i++)
        {
            TimeHour += GetInputItemHtml(CONSTHTML.option, "", i.ToString("00"), (i).ToString());
        }

        for (int i = 0; i < 60; i=i+5)
        {
            TimeMin += GetInputItemHtml(CONSTHTML.option, "", i.ToString("00"), (i).ToString());
        }
        if (!IsPostBack)
        {
            string szDevID = Request["id"];
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
