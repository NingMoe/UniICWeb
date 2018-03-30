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
        string devID = Request["devID"];
        uint uResvBegin = Parse(Request["BeginTime"]);
        uint uResvEnd= Parse(Request["EndTime"]);
        Response.CacheControl = "no-cache";

        DEVRESVSTATREQ vrGet = new DEVRESVSTATREQ();
        vrGet.szDates = (date);
        if (devID != null)
        {
           // vrGet.dwGetType = (uint)DEVRESVSTATREQ.DWGETTYPE.DEVRESVSTAT_DEVID;
            vrGet.dwDevID =Parse(devID);
        }
      
        DEVRESVSTAT[] vtRes;
        vrGet.dwResvPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL;
        vrGet.szReqExtInfo.dwNeedLines = 100000;
        vrGet.szReqExtInfo.dwStartLine = 0;
        if (m_Request.Device.GetDevResvStat(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            for (int i = 0; i < vtRes.Length; i++)
            {
                uint uOpenTimeBegin = (uint)vtRes[0].szOpenInfo[0].dwBegin;
                uint uOpenTimeEnd= (uint)vtRes[0].szOpenInfo[0].dwEnd;
                if (uResvBegin < uOpenTimeBegin || uResvEnd > uOpenTimeEnd)
                {
                    Response.Write("false");
                    return;
                }

                DEVRESVTIME[] resvInfo=vtRes[i].szResvInfo;
                if (resvInfo != null && resvInfo.Length > 0)
                {
                    for (int j = 0; j < resvInfo.Length; j++)
                    {
                        uint uResvedBegin = (uint)resvInfo[j].dwBegin;
                        uint uResvedEnd = (uint)resvInfo[j].dwEnd;
                        if (!(uResvedEnd < uResvBegin || uResvedEnd < uResvedBegin))
                        {
                            Response.Write("false");
                            return;
                        }
                    }
                }
               
            }
           
            
        
        }
        else
        {
            Response.Write("true");
            return;
        }
    }
}