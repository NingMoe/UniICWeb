using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class Pages_SharkSign : UniPage
{
    public uint m_nType=0;
    public string m_szMsg= "";
    public uint m_szTimes = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        string Openid = Request["Openid"];
        
    }
}