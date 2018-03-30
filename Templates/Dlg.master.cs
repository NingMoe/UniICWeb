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

public partial class Templates_Dlg : UniMaster
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           ((UniWebLib.UniPage)Page).m_bRemember = false;
        }
        catch (Exception ee)
        {
        }

    }
}
