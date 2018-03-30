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
        string szType = Request["type"];
        string szCamp = Request["szCamp"];
        string szBuilding = Request["building"];
        string szDevName = Request["szDevName"];
        string szRoomid=Request["room"];
        Response.CacheControl = "no-cache";
        bool bKind = false;
        if (szType != null && szType == "kind")
        {
            bKind = true;
        }

        DEVREQ vrGet = new DEVREQ();
        UNIDEVICE[] vtDev;
        if (szCamp != null && szCamp != "")
        {
            vrGet.szCampusIDs = szCamp;
        }
        if (szBuilding != null && szBuilding != ""&& szBuilding!="0")
        {
            vrGet.szBuildingIDs = szBuilding;
        }
        if (szDevName != null && szDevName != "")
        {
            vrGet.szSearchKey = szDevName;
        }
        vrGet.szReqExtInfo.dwNeedLines = 1000; //最多10条
        if (m_Request.Device.Get(vrGet, out vtDev) == REQUESTCODE.EXECUTE_SUCCESS && vtDev != null)
        {
            MyString szOut = new MyString();
            szOut += "[";
            for (int i = 0; i < vtDev.Length; i++)
            {
                string szValue = vtDev[i].dwDevID.ToString();
                UNIROOM room;
                string szManGroupID = vtDev[i].dwManGroupID.ToString();
                if (GetRoomID(vtDev[i].dwRoomID.ToString(), out room))
                {
                    szManGroupID = room.dwManGroupID.ToString();
                }
               
                if (bKind)
                {
                    szValue = vtDev[i].dwKindID.ToString();
                }
                if (szRoomid != null && szRoomid != "")
                {
                    szValue = vtDev[i].dwOpenRuleSN.ToString();
                }

                szOut += "{\"id\":\"" + szValue + "\",\"szManGroupID\": \"" + szManGroupID + "\",\"label\": \"" + vtDev[i].szDevName + "\",\"szBuilding\": \"" + vtDev[i].szBuildingName + "\",\"szCamp\": \"" + vtDev[i].szCampusName + "\",\"szRoom\": \"" + vtDev[i].szRoomName + "\",\"szDevKind\": \"" + vtDev[i].szKindName + "\"}";
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