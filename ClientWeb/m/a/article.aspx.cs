using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_m_all_article : UniClientPage
{
    protected string title = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = Request["id"];
        string type = Request["type"];
        string back=Request["back"];
        if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(type))
        {
            XmlCtrl.XmlInfo info = GetXmlInfo(id, type);
            if (!string.IsNullOrEmpty(info.title))
                title =  info.title;
            string dt = "";
            if (!string.IsNullOrEmpty(info.alter))
            {
                DateTime tmp = Util.Converter.StrToDate(info.alter);
                dt = "<div class='info_date'>" + tmp.ToString("yyyy/MM/dd HH:mm") + "</div>";
            }
            divContent.InnerHtml =dt + info.content;
        }
    }
}