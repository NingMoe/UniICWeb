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
    protected string szSearchDate = "";
    protected uint uDate = 0;
    protected override void OnLoadComplete(EventArgs e)
    {
        uint uClassKind = Parse(Request["classKind"]);
        FULLROOMREQ vrGet = new FULLROOMREQ();
        vrGet.dwInClassKind = (uint)UNIDEVCLS.DWKIND.CLSKIND_SEAT;
        vrGet.szReqExtInfo.dwNeedLines = 10000;
        vrGet.szReqExtInfo.dwStartLine = 0;
        
        uDate = Parse(Request["dwDate"]);
        if (uDate == 0)
        {
            szSearchDate = DateTime.Now.ToString("yyyy-MM-dd");
            uDate = Parse(DateTime.Now.ToString("yyyyMMdd"));
        }
        else {
            szSearchDate = uDate / 10000 + "-" + (uDate % 10000 / 100).ToString("00") + "-" + (uDate % 100).ToString("00");
        }
        FULLROOM[] vtRes;
        m_Request.m_UniDCom.StaSN = 1;
        if (m_Request.Device.FullRoomGet(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            for (int i = 0; i < vtRes.Length; i++)
            {
                bool bIsOpen = true;
                uint uBegOpenTime = 2359;
                uint uEndOpenTime = 0;
                uint uDevNum = (uint)vtRes[i].dwTotalDevNum;
                DEVRESVSTATREQ devResvStaReq = new DEVRESVSTATREQ();
                devResvStaReq.szDates = uDate.ToString();
                devResvStaReq.szRoomIDs = vtRes[i].dwRoomID.ToString();
                devResvStaReq.dwResvPurpose = 47;// (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL + (uint)UNIRESERVE.DWPURPOSE.USEFOR_RESERVED;
                devResvStaReq.szReqExtInfo.dwNeedLines = 100000;
                devResvStaReq.dwReqProp = (uint)DEVRESVSTATREQ.DWREQPROP.DRREQ_NEEDALLDAYOPENRULE;
                devResvStaReq.szReqExtInfo.dwStartLine = 0;
                DEVRESVSTAT[] vtDevResvState;
                //-1表示一天20小时，0表示开放时间(所有设备空闲)，>0表示下被预约的设备数目
                int[] timeDay = new int[1440];
                for (int j = 0; j < timeDay.Length; j++)
                {
                    timeDay[j] = -1;
                }
                if (m_Request.Device.GetDevResvStat(devResvStaReq, out vtDevResvState) == REQUESTCODE.EXECUTE_SUCCESS && vtDevResvState != null && vtDevResvState.Length > 0 && vtDevResvState[0].szOpenInfo != null)
                {
                    DAYOPENRULE[] openRule = vtDevResvState[0].szOpenInfo;
                    if (openRule == null || (openRule[0].dwBegin == openRule[0].dwEnd))
                    {
                        bIsOpen = false;
                    }
                    if (!bIsOpen)
                    {
                        continue;
                    }
                    if(openRule!=null&&openRule.Length>0)
                    for (int j = 0; j < openRule.Length;j++)
                    {
                        uint uBegin = ((uint)openRule[j].dwBegin) / 100 * 60 + ((uint)openRule[j].dwBegin) % 100;
                        uint uEnd = ((uint)openRule[j].dwEnd) / 100 * 60 + ((uint)openRule[j].dwEnd) % 100;
                        for (uint m = uBegin; m < uEnd; m++)
                        {
                            timeDay[m] = 0;//开放空闲
                        }
                        if (uBegOpenTime > (uint)openRule[j].dwBegin)
                        {
                            uBegOpenTime = (uint)openRule[j].dwBegin;
                        }
                        if (uEndOpenTime < (uint)openRule[j].dwEnd)
                        {
                            uEndOpenTime = (uint)openRule[j].dwEnd;
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
                                    timeDay[m] = timeDay[m]+1;//开放空闲
                                }
                            }
                        }
                    }
                }
                //以上计算出房间一天每一分钟的预约状况
                m_szOut += "<div class=\"Item\" data-kindid=\"" + vtRes[i].dwRoomID + "\" data-id=\"" + vtRes[i].dwRoomID + "\">";
                m_szOut += "<div class=\"LHead\">" + vtRes[i].szRoomName + "</div>";
                m_szOut += "<div class=\"LGraphics\"><span>预约状态图，</span><span class=\"enableStat\">■点击可预约</span>";

                string szOpenStartTime = uBegOpenTime / 100 + "." + (uBegOpenTime % 100).ToString("00"); ;

                string szEndTime = uEndOpenTime / 100 + "." + (uEndOpenTime % 100).ToString("00"); ;
                ArrayList resvInfo = new ArrayList();
                bool bIsBusy = false;
                uint uBeginTemp = 0;
                uint uEndTemp = 0;
                for (uint m = 0; m < timeDay.Length; m++)
                {
                  
                    if (timeDay[m] == uDevNum && bIsBusy==false)
                    {
                        bIsBusy = true;
                        uBeginTemp = m;
                    }
                    if (timeDay[m] < uDevNum && bIsBusy == true)
                    {
                        bIsBusy = false;
                        uEndTemp = m;
                        DEVRESVTIME temp = new DEVRESVTIME();
                        temp.dwBegin = uBeginTemp / 60 * 100 + uBeginTemp%60;
                        temp.dwEnd = uEndTemp / 60 * 100 + uEndTemp%60;
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
                m_szOut += "<canvas id='cav" + i.ToString() + "' data-start =\"" + szOpenStartTime + "\" data-end =\"" + szEndTime + "\" data-list=\"" + szResvInfo + "\"></canvas></div>";
                m_szOut += "<div class=\"LBtn\"><button begintime=" + szOpenStartTime + ">预约→</button></div>";
                m_szOut += "</div>";
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
		Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
    }
    protected string GetRoomResvState()
    {

        return "";
    }
}
