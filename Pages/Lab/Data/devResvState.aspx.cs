using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class Resv_searchCls :UniPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string date = Request["date"];
        string devClassKind = Request["devClassKind"];
        string devID = Request["devID"];
        string devKindID=Request["kindID"];
        string purpose = Request["purpose"];

        Response.CacheControl = "no-cache";

        DEVRESVSTATREQ vrGet = new DEVRESVSTATREQ();
        vrGet.szDates = (date);
        if (devID != null&&devID!="0")
        {
            //vrGet.dwGetType = (uint)DEVRESVSTATREQ.DWGETTYPE.DEVRESVSTAT_DEVID;
            vrGet.dwDevID =Parse(devID);
        }
        else if (devKindID != null)
        {
            //vrGet.dwGetType = (uint)DEVRESVSTATREQ.DWGETTYPE.DEVRESVSTAT_ROOMID;
            vrGet.szRoomIDs = devKindID.ToString();
        }
        else {
            //vrGet.dwGetType = (uint)DEVRESVSTATREQ.DWGETTYPE.DEVRESVSTAT_ALL;
        }
        if (devClassKind != null)
        {
            vrGet.dwClassKind =Parse(devClassKind);
        }

        DEVRESVSTAT[] vtRes;
       // vrGet.dwGetType = 0;
        vrGet.dwResvPurpose =Parse(purpose);
        vrGet.szReqExtInfo.dwNeedLines = 100000;
        vrGet.szReqExtInfo.dwStartLine = 0;
        if (m_Request.Device.GetDevResvStat(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
           
            string szDevInfo = "[";
            string szOpenTime = "";
            string szResvInfo = "[";
            string szOut="";
            szOut += "[";
            for (int i = 0; i < vtRes.Length; i++)
            {
                szOpenTime ="";
                if (vtRes[0].szOpenInfo != null && vtRes[0].szOpenInfo.Length>0)
                {
                    szOpenTime=((uint)vtRes[0].szOpenInfo[0].dwBegin * 10000 + (uint)vtRes[0].szOpenInfo[0].dwEnd).ToString();
                }
                string devNameTemp = vtRes[i].szDevName;
                string devIDTemp = vtRes[i].dwDevID.ToString();
                DEVRESVTIME[] resvInfo=vtRes[i].szResvInfo;
                if (resvInfo != null && resvInfo.Length > 0)
                {
                    for (int j = 0; j < resvInfo.Length; j++)
                    {
                        uint uResvTime = ((uint)resvInfo[j].dwBegin * 10000 + (uint)resvInfo[j].dwEnd);
                        szResvInfo = szResvInfo + "{" + "\"name\":\"" + devNameTemp + "\"," + "\"value\":" + devIDTemp + "," + "\"devID\":" + devIDTemp + "," + "\"value\":" + uResvTime.ToString() + "}";
                        if (i < vtRes.Length - 1)
                        {
                            szResvInfo += ",";
                        }
                    }
                }
                szDevInfo = szDevInfo + "{" + "\"devName\":\"" + devNameTemp + "\"," + "\"devID\":" + devIDTemp + "}";
                if (i < vtRes.Length - 1)
                {
                    szDevInfo += ",";
                }
            }
            szDevInfo += "]";
           
            if (szResvInfo.EndsWith(","))
            {
                szResvInfo = szResvInfo.Substring(0, szResvInfo.Length - 1);
            }
            szResvInfo += "]";
            szOut = "{" + "\"resvInfo\":" + szResvInfo + ",\"DevList\":" + szDevInfo + ",\"OpenTime\":" + szOpenTime + "}";
            Response.Write(szOut);
        
        }
        else
        {
            Response.Write("[ ]");
        }
    }
}