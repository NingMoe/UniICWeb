using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_m_all_auto : UniClientPage
{
    protected string title = "搜索";
    protected string placeholder = "Search";
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!string.IsNullOrEmpty( Request["title"]))title=Request["title"];
        if (!string.IsNullOrEmpty(Request["placeholder"])) placeholder = Translate(Request["placeholder"]);
    }
}