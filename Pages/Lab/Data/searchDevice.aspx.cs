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
        string szRoomid=Request["roomid"];

        Response.CacheControl = "no-cache";
        uint uNeedLine = 10;
        string szNeedLine = Request["needLine"];
        if (Parse(szNeedLine) != 0)
        {
            uNeedLine = Parse(szNeedLine);
        }
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
        if (Parse(szRoomid) != 0)
        {
            vrGet.szRoomIDs = szRoomid;
        }
       // vrGet.szGetID = szTerm;
        vrGet.szReqExtInfo.dwNeedLines = uNeedLine; //最多10条
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