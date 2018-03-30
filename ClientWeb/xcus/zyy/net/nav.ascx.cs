using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using UniWebLib;
using UniStruct;

public partial class WebUserControl : UniClientModule
{
    protected string bDisplay1 = "none";
    protected string bDisplay2 = "none";
    protected string bDisplay3 = "none";
    protected string szTrueName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LOGIN_ACCINFO"] != null && IsClientLogin())
        {
            UNIACCOUNT vrAccInfo = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
            szTrueName = vrAccInfo.szTrueName;
            if ((Convert.ToUInt32(vrAccInfo.dwIdent) & (uint)UNIACCOUNT.DWIDENT.EXTIDENT_TUTOR)>0)
            {
                bDisplay3 = "display";
            }
            else
            {
                bDisplay1 = "display";
            }
        }
        else
        {
            bDisplay2 = "display";
        }
    }

}
