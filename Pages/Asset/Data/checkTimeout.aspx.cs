using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class checkTimeout : UniWebLib.UniPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";

        if (Session["SessionID"] == null || Session["LoginResult"] == null)
        {
            Response.Write("Error");
            return;
        }



        REFRESHFLAGREQ vrParameter = new REFRESHFLAGREQ();
        vrParameter.dwRefreshType = (uint)REFRESHFLAGREQ.DWREFRESHTYPE.REFRESHTYPE_DEVICE;
        REFRESHFLAGINFO[] vrResult;
        REQUESTCODE ret = m_Request.Admin.AdminRefreshFlagGet(vrParameter, out vrResult);
        if (ret != (REQUESTCODE)0x02002001)
        {
            Response.Write("OK");
        }
        else
        {
            Response.Write("Error");
        }
        /*
        if (Session["SessionID"] != null)
        {
            Response.Write("OK");
        }
        else
        {
            Response.Write("Error");
        }*/
    }
}