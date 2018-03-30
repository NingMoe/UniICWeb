using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class searchDevice : UniClientAjax
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string szTerm = Request["term"];
        string szType = Request["type"];
        string kinds = Request["kinds"];
        Response.CacheControl = "no-cache";

        DEVREQ vrGet = new DEVREQ();
        UNIDEVICE[] vtDev;
        if ((szType==null||szType=="")&& szTerm != null && szTerm != "")
        {
            vrGet.szSearchKey = szTerm;
        }
        if (szType.ToLower() == "devsn")
        {
            vrGet.dwDevSN = ToUInt(szTerm);
        }
        else if (szType.ToLower()=="assertsn")
        {
            vrGet.szAssertSN = szTerm;
        }
        if (!string.IsNullOrEmpty(kinds))
            vrGet.szKindIDs = kinds;
        vrGet.szReqExtInfo.dwNeedLines = 10; //最多10条
        if (m_Request.Device.Get(vrGet, out vtDev) == REQUESTCODE.EXECUTE_SUCCESS && vtDev != null)
        {
            MyString szOut = new MyString();
            szOut += "[";
            for (int i = 0; i < vtDev.Length; i++)
            {
                szOut += "{\"id\":\"" + vtDev[i].dwDevID + "\",\"label\": \"" + vtDev[i].szDevName + "\",\"name\": \"" + vtDev[i].szDevName + "\",\"sn\": \"" + vtDev[i].dwDevSN + "\",\"assert\": \"" + vtDev[i].szAssertSN + "\",\"room\": \"" + vtDev[i].szRoomName + "\",\"lab\": \"" + vtDev[i].szLabName + "\",\"campus\": \"" + vtDev[i].szCampusName + "\",\"kindId\": \"" + vtDev[i].dwKindID + "\",\"kindName\": \"" + vtDev[i].szKindName + "\"}";
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