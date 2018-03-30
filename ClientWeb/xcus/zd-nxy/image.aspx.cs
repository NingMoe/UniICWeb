using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Util;

public partial class image : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ValidateNumber s = new ValidateNumber();
        string str = s.CreateValidateNumber(4);
        Session["Vnumber"] = str;
        s.CreateValidateGraphic(this, str);
    }
}
