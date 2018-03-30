using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class searchCourse : UniWebLib.UniPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        m_bRemember = false;

        string szTerm = Request["term"];
        uint dwAccNo = ToUint(Request["dwAccNo"]);

        Response.CacheControl = "no-cache";

        MANROOMREQ vrGetCls = new MANROOMREQ();
        MANROOM[] vtCls;
        vrGetCls.dwAccNo = dwAccNo;
        vrGetCls.dwManFlag = 0;

        if (m_Request.Admin.GetManRoom(vrGetCls, out vtCls) == REQUESTCODE.EXECUTE_SUCCESS && vtCls != null)
        {
            MyString szOut = new MyString();
            szOut += "[";
            for (int i = 0; i < vtCls.Length; i++)
            {
                szOut += "{\"id\":\"" + vtCls[i].dwRoomID + "\",\"label\": \"" + vtCls[i].szRoomName + "\"}";
                if (i < vtCls.Length - 1)
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