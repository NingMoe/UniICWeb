using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_jx_User : UniClientPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ClientRedirect("Login.aspx");
    }
}