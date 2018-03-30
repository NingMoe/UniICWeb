using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_jx_Art : UniClientPage
{
    protected string artTitle;
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = Request["id"];
        string type = Request["type"];
        if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(type))
        {
            XmlCtrl.XmlInfo info = GetXmlInfo(id, type);
            if (!string.IsNullOrEmpty(info.title))
                artTitle = info.title;
            divContent.InnerHtml = info.content;
        }
    }
}