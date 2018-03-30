using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_pro_net_include : UniClientModule
{
    protected string uiCss = "bootstrap/jquery-ui-1.10.3.custom.css";
    protected string uiJs = "jquery-ui-1.10.3.custom.min.js";
    protected void Page_Load(object sender, EventArgs e)
    {
        string theme = GetConfig("sysTheme");//启用的主题   未使用
    }
}