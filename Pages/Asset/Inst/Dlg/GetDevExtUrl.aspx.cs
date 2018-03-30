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

public partial class _Default : UniPage
{
    protected bool bSet = false;
    protected string m_Title = "";
    protected string m_szSta = "";
    protected string szImgUrl = ""; 
	protected void Page_Load(object sender, EventArgs e)
	{
        string szID = Request["id"];
        
        if (szID != null && szID != "")
        {
            UNIDEVICE dev;
            if (getDevByID(szID, out dev)&&dev.szDevURL!=null)
            {
                szImgUrl = dev.szDevURL.ToString();
            }
        }
	}
}
