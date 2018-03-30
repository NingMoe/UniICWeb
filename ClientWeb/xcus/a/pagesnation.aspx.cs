using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ClientWeb_xcus_a_pages : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string szActivtyPage = Request["activityPage"];
        Session["activityPage"] = szActivtyPage;
      //  Session["activityPageold"] = szActivtyPage;
    }
}