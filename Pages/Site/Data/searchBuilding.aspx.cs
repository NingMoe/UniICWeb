﻿using System;
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
        string szCampID = Request["campid"];
        Response.CacheControl = "no-cache";

        BUILDINGREQ vrGet = new BUILDINGREQ();
        UNIBUILDING[] vtDev;
        if (szCampID != null && szCampID != "" && szCampID != "0")
        {
            vrGet.szCampusIDs = szCampID;
        }
        vrGet.szReqExtInfo.dwNeedLines =0;
        vrGet.szReqExtInfo.dwNeedLines = 10000; //最多10条
        if (m_Request.Device.BuildingGet(vrGet, out vtDev) == REQUESTCODE.EXECUTE_SUCCESS && vtDev != null)
        {
            MyString szOut = new MyString();
            szOut += "[";
            if (vtDev.Length == 0)
            {
                szOut += "{\"id\":\"" + "0" + "\",\"label\": \"" + "全部" + "\"}";
            }
            else
            {
                szOut += "{\"id\":\"" + "0" + "\",\"label\": \"" + "全部" + "\"},";
            }
            for (int i = 0; i < vtDev.Length; i++)
            {
                szOut += "{\"id\":\"" + vtDev[i].dwBuildingID + "\",\"label\": \"" + vtDev[i].szBuildingName + "\"}";
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