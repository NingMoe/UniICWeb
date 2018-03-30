using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public class UniMaster : System.Web.UI.MasterPage
{
    protected string MyVPath = "/";

    public UniMaster()
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
