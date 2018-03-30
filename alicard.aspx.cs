using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class alicard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("https://memberprod.alipay.com/account/openform/activecard.htm?app_id=2016050901378158&template_id=20170822000000000427128000300967&__webview_options__=canPullDown%3dNO%26transparentTitle%3dauto&out_string=1$&callback=http://update.unifound.net/unialipay/openCard.aspx");
           
    }
}