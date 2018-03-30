using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_m_a_logout : UniClientPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LOGIN_ACCINFO"] != null)
        {
            UNIACCOUNT vrAccInfo = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
            ADMINLOGOUTREQ vrParameter = new ADMINLOGOUTREQ();
            ADMINLOGOUTRES vrResult;
            vrParameter.dwAccNo = vrAccInfo.dwAccNo;
            vrParameter.szLogonName = vrAccInfo.szLogonName;
            m_Request.Admin.Logout(vrParameter, out vrResult);
        }
        common.ClearLogin();
    }
}