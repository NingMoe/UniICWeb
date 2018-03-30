using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_coords : UniClientPage
{
    protected string content;
    protected void Page_Load(object sender, EventArgs e)
    {
        XmlCtrl.XmlInfo[] list=GetXmlInfoList("cfg_coords", 100);
        for (int i = 0; i < list.Length; i++)
        {
            content += "<li>"+list[i].content+"</li>";
        }
    }
}