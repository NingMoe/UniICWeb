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
        m_bRemember = false;

        string szTerm = Request["term"];
        string szKind = Request["kind"];     
        string szcampuid=Request["campuid"];
        string szBuildingid=Request["buildingid"];
        Response.CacheControl = "no-cache";

        ROOMREQ vrGet = new ROOMREQ();
        UNIROOM[] vtRes;
        vrGet.szRoomName = szTerm;
        if (!string.IsNullOrEmpty(szcampuid) && szcampuid != "0")
        {
            vrGet.szCampusIDs = szcampuid;
        }
        if (!string.IsNullOrEmpty(szBuildingid) && szBuildingid != "0")
        {
            vrGet.szBuildingIDs = szBuildingid;
        }

        vrGet.szReqExtInfo.dwNeedLines = 15; //最多10条
        if (szKind !=null && szKind != "")
        {
            vrGet.dwInClassKind = Parse(szKind);
        }
        if (m_Request.Device.RoomGet(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null)
        {
            MyString szOut = new MyString();
            szOut += "[";
            for (int i = 0; i < vtRes.Length; i++)
            {
                szOut += "{\"id\":\"" + vtRes[i].dwRoomID + "\",\"label\": \"" + vtRes[i].szRoomName + "\",\"labid\": \"" + vtRes[i].dwLabID.ToString() + "\"}";
                if (i < vtRes.Length - 1)
                {
                    szOut += ",";
                }
            }
            szOut += "]";
            Response.Write(szOut);
        }
        else
        {
            Response.Write("[ ]");
        }
    }
}