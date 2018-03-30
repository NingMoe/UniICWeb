using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_pro_net_include : UniClientModule
{
    protected string url;
    protected void Page_Load(object sender, EventArgs e)
    {
        url = this.ResolveClientUrl("~/ClientWeb/");
    }
}