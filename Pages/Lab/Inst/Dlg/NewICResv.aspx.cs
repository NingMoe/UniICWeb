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
	protected void Page_Load(object sender, EventArgs e)
	{       
        if (IsPostBack)
        {
            UNIGROUP resvGroup;
            string[] szOwnerList=Request["szowner"].ToString().Split(';');
            string szOwner = Request["dwOwner"];
            uint uOwenr = Parse(szOwner);
            string szDevID=Request["szDevID"];
            string szStartDate = Request["szStartDate"];
            string szEndDate = Request["szEndDate"];
            string szSelectWeek = Request["weekSelect"];
            string szStartTime=Request["startTimeHour"]+":"+Request["startTimeMin"];
            string szEndTime=Request["endTimeHour"]+":"+Request["endTimeMin"];
            UNIRESERVE setValue = new UNIRESERVE();
            UNIACCOUNT accInfo;
           // if (NewGroup("管理员新建预约", (uint)UNIGROUP.DWKIND.GROUPKIND_RERV, out resvGroup))
            if (GetAccByAccno(uOwenr.ToString(),out accInfo)==true)
            {
                /*
                for (int i = 0; i < szOwnerList.Length; i++)
                {
                    UNIACCOUNT acc = new UNIACCOUNT();
                    if (GetAccByAccno(szOwnerList[i], out acc))
                    {
                        AddGroupMember(resvGroup.dwGroupID, acc.dwAccNo, (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL);
                    }
                }
                */
                setValue.szOwnerName = accInfo.szTrueName;
                setValue.dwOwner = accInfo.dwAccNo;
                setValue.dwMemberID = uOwenr;
                setValue.dwMemberKind = (uint)UNIRESERVE.DWMEMBERKIND.MEMBERKIND_PERSONNAL;
               
                /*ADMINLOGINRES admin = (ADMINLOGINRES)Session["LoginResult"];
                setValue.dwOwner = admin.AdminInfo.dwAccNo;
                setValue.szOwnerName = admin.AdminInfo.szTrueName;
                 * */
                setValue.szTestName = Request["szMemo"];
                UNIDEVICE dev = new UNIDEVICE();
                if (getDevByID(szDevID, out dev))
                {
                    setValue.ResvDev = new RESVDEV[1];
                    setValue.ResvDev[0].dwDevEnd = dev.dwDevSN;
                    setValue.ResvDev[0].dwDevStart = dev.dwDevSN;
                    setValue.ResvDev[0].dwDevNum = 1;
                    setValue.ResvDev[0].dwDevKind = dev.dwKindID;
                    setValue.ResvDev[0].szRoomNo = dev.szRoomNo;
                    setValue.ResvDev[0].szDevName = dev.szDevName;
                    setValue.dwLabID = dev.dwLabID;
                    setValue.szLabName = dev.szLabName;

                }
                setValue.dwProperty = (uint)UNIRESERVE.DWPROPERTY.RESVPROP_SELFDO;
                setValue.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL;

            }
            else {
                MessageBox("预约失败，未选择指定人", "提示", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                return;
            }
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;

            DateTime startDate = DateTime.Parse(szStartDate);
            DateTime endDate = DateTime.Parse(szEndDate);
            TimeSpan span = endDate - startDate;
            uint uCount = 0;
            while (span.Days >= 0)
            {
                int uweek=(int)startDate.DayOfWeek;
                if (szSelectWeek == null || szSelectWeek == "" || szSelectWeek.IndexOf(uweek.ToString()) > -1)
                {
                    string szStartTimeTemp = startDate.ToString("yyyy-MM-dd") + " " + szStartTime;
                    string szEndTimeTemp = startDate.ToString("yyyy-MM-dd") + " " + szEndTime;

                    setValue.dwBeginTime = Get1970Seconds(szStartTimeTemp);
                    setValue.dwEndTime = Get1970Seconds(szEndTimeTemp);

                    RESVDEV[] resvDevList = setValue.ResvDev;
                    uResponse = m_Request.Reserve.Set(setValue, out setValue);
                    if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
                    {
                        uCount = uCount + 1;
                    }
                    setValue.dwResvID = null;
                    setValue.ResvDev = resvDevList;
                    setValue.szOwnerName = accInfo.szTrueName;
                    setValue.dwOwner = accInfo.dwAccNo;
                }
                startDate = startDate.AddDays(1);
                span = endDate - startDate;
            }

          

            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox("预约成功【" + uCount+"】条", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
            else
            {
                MessageBox(m_Request.szErrMessage, "提示", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                return;
            }

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
        m_Title = "管理员新建预约";
      
	}
}
