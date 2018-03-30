using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// UniPage 的摘要说明
/// </summary>
public class UniControl : System.Web.UI.UserControl
{
    protected string MyVPath = "/";

    public UniControl()
    {
	}
    protected override void OnLoad(EventArgs e)
    {
        if (Request.ApplicationPath != "/")
        {
            MyVPath = Request.ApplicationPath + "/";
        }
        else
        {
            MyVPath = "/";
        }
        base.OnLoad(e);
    }
    public UniCommon Common = new UniCommon();
}
