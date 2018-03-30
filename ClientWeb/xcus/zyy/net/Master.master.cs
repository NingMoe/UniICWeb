using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_Templates_Master : UniClientMaster
{
    protected string url;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (true)
        {
            Uri  uri= Request.Url;
        }
        url = this.ResolveClientUrl("~/ClientWeb/");
    }
}
