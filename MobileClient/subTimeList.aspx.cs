using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using UniWebLib;
using System.Collections;
public partial class _Default : PageBase
{
    protected MyString m_szOut = new MyString();
    protected MyString m_szOutResv = new MyString();
    protected uint roomid = 0;
    protected string szSearchDate="";
    protected string szSelectBeginTime= "";
    protected string szSelectEndTime= "";
    protected override void OnLoadComplete(EventArgs e)
    {
        roomid = Parse(Request["roomid"]);
        szSearchDate = Request["dwDate"];// DateTime.Now.ToString("yyyyMMdd");
        string szSearchTime = Request["begintime"];
       
        DEVRESVSTATREQ devResvStaReq = new DEVRESVSTATREQ();
        devResvStaReq.szDates = szSearchDate;
        int uBegTime = 2359;
        int uEndTime = 0;
        devResvStaReq.szRoomIDs = roomid.ToString();
        devResvStaReq.dwResvPurpose = 47;// (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL + (uint)UNIRESERVE.DWPURPOSE.USEFOR_RESERVED;
        devResvStaReq.szReqExtInfo.dwNeedLines = 10000;
        devResvStaReq.dwReqProp = (uint)DEVRESVSTATREQ.DWREQPROP.DRREQ_NEEDALLDAYOPENRULE;
        devResvStaReq.szReqExtInfo.dwStartLine = 0;
        DEVRESVSTAT[] vtDevResvState;
        m_Request.m_UniDCom.StaSN = 1;
        int[] timeDay = new int[1440];
        for (int j = 0; j < timeDay.Length; j++)
        {
            timeDay[j] = 0;
        }
        if (m_Request.Device.GetDevResvStat(devResvStaReq, out vtDevResvState) == REQUESTCODE.EXECUTE_SUCCESS && vtDevResvState != null && vtDevResvState.Length > 0)
        {
            uint uDevNum = (uint)vtDevResvState.Length;
            DAYOPENRULE[] dayOpenRule = vtDevResvState[0].szOpenInfo;
            for (int i = 0; i < dayOpenRule.Length; i++)
            {
                if (uBegTime > (int)dayOpenRule[i].dwBegin)
                {
                    uBegTime = (int)dayOpenRule[i].dwBegin;
                }
                if (uEndTime < (int)dayOpenRule[i].dwEnd)
                {
                    uEndTime = (int)dayOpenRule[i].dwEnd;
                }

                
            }
            for (int k = 0; k < vtDevResvState.Length; k++)
            {
                DEVRESVSTAT DevResvTemp = new DEVRESVSTAT();
                DevResvTemp = vtDevResvState[k];
                DEVRESVTIME[] resvTime = DevResvTemp.szResvInfo;
                if (resvTime != null && resvTime.Length > 0)
                {
                    for (int n = 0; n < resvTime.Length; n++)
                    {
                       
                        uint uBegin = ((uint)resvTime[n].dwBegin) / 100 * 60 + ((uint)resvTime[n].dwBegin) % 100;
                        uint uEnd = ((uint)resvTime[n].dwEnd) / 100 * 60 + ((uint)resvTime[n].dwEnd) % 100;
                        for (uint m = uBegin; m < uEnd; m++)
                        {
                            timeDay[m] = timeDay[m] + 1;//开放空闲
                        }
                        
                    }
                }
            }
            //以上计算出房间一天每一分钟的预约状况
            m_szOutResv += "<div class=\"LGraphics\"><span>预约状态图</span>";

            string szOpenStartTime = uBegTime / 100 + "." + (uBegTime % 100).ToString("00"); ;

            string szEndTime = uEndTime / 100 + "." + (uBegTime % 100).ToString("00"); ;
            ArrayList resvInfo = new ArrayList();
            bool bIsBusy = false;
            uint uBeginTemp = 0;
            uint uEndTemp = 0;
            for (uint m = 0; m < timeDay.Length; m++)
            {

                if (timeDay[m] == uDevNum && bIsBusy == false)
                {
                    bIsBusy = true;
                    uBeginTemp = m;
                }
                if (timeDay[m] < uDevNum && bIsBusy == true)
                {
                    bIsBusy = false;
                    uEndTemp = m;
                    DEVRESVTIME temp = new DEVRESVTIME();
                    temp.dwBegin = uBeginTemp / 60 * 100 + uBeginTemp % 60;
                    temp.dwEnd = uEndTemp / 60 * 100 + uEndTemp % 60;
                    resvInfo.Add(temp);
                }
            }
            string szResvInfo = "[";
            if (resvInfo != null && resvInfo.Count > 0)
            {
                for (int m = 0; m < resvInfo.Count; m++)
                {
                    DEVRESVTIME temp = new DEVRESVTIME();
                    temp = (DEVRESVTIME)resvInfo[m];
                    szResvInfo += ((int)temp.dwBegin * 10000 + (int)temp.dwEnd);
                    if ((m + 1) < (resvInfo.Count))
                    {
                        szResvInfo += ",";
                    }
                }
            }
            szResvInfo += "]";
            m_szOutResv += "<canvas id='cav" + ""+ "' data-start =\"" + szOpenStartTime + "\" data-end =\"" + szEndTime + "\" data-list=\"" + szResvInfo + "\"></canvas></div>";
            m_szOutResv += "<div class=\"LBtn\"></div>";

        }
        string szOptionAll = "";
        for (int i = uBegTime; i <= uEndTime; i = i + 10)
        {
            if (i%100==60)
            {
                i = (i + 100)/100*100;
                //continue;
            }
            szOptionAll += GetInputItemHtml(CONSTHTML.option, "", i/100+"："+(i%100).ToString("00"), i.ToString());
        }
        if (szSearchTime == "0")
        {
            if (szSearchDate == DateTime.Now.ToString("yyyyMMdd"))
            {
                uint uNowHM = Parse(DateTime.Now.ToString("HHmm"));
                if (uNowHM > uBegTime)
                {
                    szSelectBeginTime = (uNowHM / 100 + 1) + "00"; 
                }
                else {
                    szSelectBeginTime = uBegTime.ToString();
                }
            }
            else
            {
                szSelectBeginTime = uBegTime.ToString();
            }
        }
        else {
            if (szSearchDate == DateTime.Now.ToString("yyyyMMdd"))
            {
                uint uNowHM = Parse(DateTime.Now.ToString("HHmm"));
                if (uNowHM >(Parse(szSearchTime) * 100))
                {
                    szSelectBeginTime = (uNowHM / 100 + 1) + "00";
                }
                else
                {
                    szSelectBeginTime = szSearchTime + "00";
                }
            }
            else
            {
                szSelectBeginTime = szSearchTime + "00";
            }
        }
        szSelectEndTime = uEndTime.ToString();
        m_szOut += "<div class=\"Item\" value=" + (uBegTime * 10000 + uEndTime) + ">";
        m_szOut += "<div style='margin:20px'>开始时间：<select id='dwBeginTime' name='dwBeginTime'>" + szOptionAll + "</select></div>";
        m_szOut += "<div style='margin:20px'>结束时间：<select id='dwEndTime' name='dwEndTime'>" + szOptionAll + "</select></div>";
        m_szOut += "<div style='margin:20px'><button roomid=" + roomid + " class='btnTimeClass' id='allDay'>查询→</button></div>";
        m_szOut += "</div>";

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
    }
}
