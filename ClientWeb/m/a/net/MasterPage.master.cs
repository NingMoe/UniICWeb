using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_m_a_net_MasterPage : UniClientMaster
{
    protected string url;
    protected void Page_Load(object sender, EventArgs e)
    {
        url = this.ResolveClientUrl("~/ClientWeb/");
    }
}
