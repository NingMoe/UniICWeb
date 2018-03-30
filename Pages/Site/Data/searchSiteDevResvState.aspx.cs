using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class searchAccount : UniPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string szCamp = Request["szCamp"];
        string szBuilding = Request["szBuilding"];
        string szDate = Request["dates"];
        string szDevName = Request["devName"];
        string szKinds=Request["kinds"];
        string szBeginTime=Request["beginTime"];
        string szEndTime = Request["endTime"];
        string YardActivitySN = Request["YardActivitySN"];
        DEVRESVSTATREQ vrGet = new DEVRESVSTATREQ();
        vrGet.szDates = szDate;

        if (szCamp != null && szCamp != "" && szCamp != "0")
        {
            vrGet.szCampusIDs = szCamp;
        }
        if (szBuilding != null && szBuilding != "" && szBuilding != "0")
        {
            vrGet.szBuildingIDs = szBuilding;
        }
        if (szDevName != null && szDevName != "")
        {
            vrGet.szDevName = (szDevName);
        }
        if (szKinds != null && szKinds != "" && szKinds != "0")
        {
            vrGet.szClassIDs = szKinds;
        }
        if (YardActivitySN != null && YardActivitySN != "" && YardActivitySN != "0")
        {
            vrGet.dwExtRelatedID = Parse(YardActivitySN);
        }
        DEVRESVSTAT[] vtDev;
        Response.CacheControl = "no-cache";
        MyString szOut = new MyString();
        szOut += "[";
        vrGet.szReqExtInfo.dwNeedLines = 100000;
        vrGet.szReqExtInfo.dwStartLine = 1;
        if (m_Request.Device.GetDevResvStat(vrGet, out vtDev) == REQUESTCODE.EXECUTE_SUCCESS && vtDev != null)
        {
            uint uBeginTime = Parse(szBeginTime);//7
            uint uEndTime = Parse(szEndTime);//8

            for (int i = 0; i < vtDev.Length; i++)
            {
                int bRes = 0;
                DEVRESVTIME[] devResvTime = vtDev[i].szResvInfo;
                if (devResvTime == null || devResvTime.Length == 0)
                {
                    szOut += "{\"id\":\"" + vtDev[i].dwDevID + "\",\"statue\": \"" + bRes + "\",\"szCampName\": \"" + vtDev[i].szCampusName + "\",\"szDevName\": \"" + vtDev[i].szDevName + "\",\"szBuildingName\": \"" + vtDev[i].szBuildingName + "\",\"szKindName\": \"" + vtDev[i].szClassName + "\",\"dwMaxUser\": \"" + vtDev[i].dwMaxUsers  + "\"}";
                    if (i < vtDev.Length - 1)
                    {
                        szOut += ",";
                    }
                }
                else
                {
                    for (int j = 0; j < devResvTime.Length; j++)
                    {
                        uint uStateResv = (uint)devResvTime[j].dwStatus;
                        uint uBeginTimeResv = (uint)devResvTime[j].dwBegin;//6.5
                        uint uEndTimeResv = (uint)devResvTime[j].dwEnd;//7
                        if ((uStateResv & (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK) > 0)
                        {
                            if (uEndTimeResv <= uBeginTime || uBeginTimeResv >= uEndTime)//classic
                            {
                                continue;
                            }
                            else
                            {
                                bRes = -1;
                                break;
                            }
                        }
                        szOut += "{\"id\":\"" + vtDev[i].dwDevID + "\",\"statue\": \"" + bRes + "\",\"szCampName\": \"" + vtDev[i].szCampusName + "\",\"szDevName\": \"" + vtDev[i].szDevName + "\",\"szBuildingName\": \"" + vtDev[i].szBuildingName + "\",\"szKinds\": \"" + vtDev[i].szKindName + "\",\"dwMaxUser\": \"" + vtDev[i].dwMaxUsers + "\",\"dwMinUser\": \"" + vtDev[i].dwMinUsers + "\"}";
                        if (i < vtDev.Length - 1)
                        {
                            szOut += ",";
                        }
                    }
                }
            }
          
        }
        szOut += "]";
        Response.Write(szOut);
    }
        
}