using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : PageBase
{
    protected string szMsg = "";
    protected LOCALUSER curUser;
    protected string szClassKind = "0";
    protected override void OnLoadComplete(EventArgs e)
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        szClassKind = ConfigurationManager.AppSettings["MobileSysKind"];
		Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
        Response.Expires = 0;
        Response.CacheControl = "no-cache";

       
    }

    protected void Button_OK_Click(object sender, EventArgs e)
    {
    }

    protected void Button_Set_Click(object sender, EventArgs e)
    {
    }
}
