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
            string[] szOwnerList=Request["szowner"].ToString().Split(';');
            string szDevID=Request["szDevID"];
            string szStartDate = Request["szStartDate"];
            string szEndDate = Request["szEndDate"];
            string szSelectWeek = Request["weekSelect"];
            string szStartTime=Request["startTimeHour"]+":"+Request["startTimeMin"];
            string szEndTime=Request["endTimeHour"]+":"+Request["endTimeMin"];
            UNIRESERVE setValue = new UNIRESERVE();
            ArrayList list = new ArrayList();
            if (NewGroup("管理员新建预约", (uint)UNIGROUP.DWKIND.GROUPKIND_RERV, out resvGroup))
            {
                for (int i = 0; i < szOwnerList.Length; i++)
                {
                    UNIACCOUNT acc = new UNIACCOUNT();
                    if (GetAccByAccno(szOwnerList[i], out acc))
                    {
                        GROUPMEMBER member=new GROUPMEMBER();
                        member.dwMemberID=acc.dwAccNo;
                        member.szName=acc.szTrueName;
                        AddGroupMember(resvGroup.dwGroupID, acc.dwAccNo, (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL,acc.szTrueName);
                        list.Add(member);
                    }
                }


                setValue.dwMemberID = resvGroup.dwGroupID;
                setValue.dwMemberKind = (uint)UNIRESERVE.DWMEMBERKIND.MEMBERKIND_GROUP;

                UNIACCOUNT vrAccInfo = ((ADMINLOGINRES)Session["LoginResult"]).AccInfo;
                setValue.dwOwner = vrAccInfo.dwAccNo;
                setValue.szOwnerName = vrAccInfo.szTrueName;
                setValue.szTestName=Request["szMemo"];
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
                //setValue.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL;

                //管理员占用导致预约不能生效 所以去掉(uint)UNIRESERVE.DWPURPOSE.USEFOR_RESERVED 
                //setValue.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_RESERVED + (uint)UNIRESERVE.DWPURPOSE.USEFOR_STUDYROOM + (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL;

                setValue.dwPurpose =  (uint)UNIRESERVE.DWPURPOSE.USEFOR_STUDYROOM+ (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL;
                
                setValue.dwUseMode = (uint)UNIRESERVE.DWUSEMODE.RESVUSEEXT_PC + (uint)UNIRESERVE.DWUSEMODE.RESVUSE_USEDEV;
            }
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;

            DateTime startDate = DateTime.Parse(szStartDate);
            DateTime endDate = DateTime.Parse(szEndDate);
            TimeSpan span = endDate - startDate;
            uint uCount = 0;
            uint uCountFali = 0;
            while (span.Days > -1)
            {
                UNIGROUP groupresv = new UNIGROUP();
                groupresv = resvGroup;
                groupresv.dwGroupID = null;
                m_Request.Group.SetGroup(groupresv, out groupresv);
               setValue.dwMemberID = groupresv.dwGroupID;
               if (list!= null && list.Count > 0)
               {
                   for (int m = 0; m < list.Count; m++)
                   {
                       GROUPMEMBER member = (GROUPMEMBER)list[m];
                       AddGroupMember(groupresv.dwGroupID, member.dwMemberID, (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL, member.szName);
                   }
               }
                int uweek=(int)startDate.DayOfWeek;
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
                        uCountFali=uCountFali+1;
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

            MessageBox("预约成功【" + uCount + "】", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.NONE);
            szResOut += "</table>";
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
        m_Title = "管理员新建预约";
      
	}
}
