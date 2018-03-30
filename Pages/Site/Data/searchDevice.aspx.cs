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
        string szTerm = Request["term"];
        string szType = Request["Type"];
        Response.CacheControl = "no-cache";

        DEVREQ vrGet = new DEVREQ();
        UNIDEVICE[] vtDev;
        if ((szType == null || szType == "") && szTerm != null)
        {
            vrGet.szSearchKey = szTerm;
        }
        else if (szType.ToLower() == "devsn")
        {
            vrGet.dwDevSN = Parse(szTerm);
        }
        else if (szType.ToLower()=="assertsn")
        {
            vrGet.szAssertSN = szTerm;
        }
       // vrGet.szGetID = szTerm;
        vrGet.szReqExtInfo.dwNeedLines = 10; //最多10条
        if (m_Request.Device.Get(vrGet, out vtDev) == REQUESTCODE.EXECUTE_SUCCESS && vtDev != null)
        {
            MyString szOut = new MyString();
            szOut += "[";
            for (int i = 0; i < vtDev.Length; i++)
            {
                szOut += "{\"id\":\"" + vtDev[i].dwDevID + "\",\"label\": \"" + vtDev[i].szDevName + "\",\"szDevSN\": \"" + vtDev[i].dwDevSN + "\",\"szAssertSN\": \"" + vtDev[i].szAssertSN + "\",\"szRoom\": \"" + vtDev[i].szRoomName + "\",\"szDevKind\": \"" + vtDev[i].szKindName + "\"}";
                if (i < vtDev.Length - 1)
                {
                    szOut += ",";
                }
            }
            szOut += "]";
            Response.Write(szOut);
        }
        else
        {
            Response.Write("[{}]");
        }
    }
        
}