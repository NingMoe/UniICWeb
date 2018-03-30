using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_all_article : UniClientPage
{
    protected string isBack = "none";
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = Request["id"];
        string type = Request["type"];
        string back=Request["back"];
        if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(type))
        {
            if (back == "true")//默认隐藏
                isBack = "";
            XmlCtrl.XmlInfo info = GetXmlInfo(id, type);
            string str = "";
            if (!string.IsNullOrEmpty(info.title))
                str = "<h1 class='h_title'>" + info.title + "</h1>";
            string dt = "";
            if (!string.IsNullOrEmpty(info.alter))
            {
                DateTime tmp = Util.Converter.StrToDate(info.alter);
                dt = "<div class='info_date'>" + tmp.ToString("yyyy/MM/dd HH:mm") + "</div>";
            }
            divContent.InnerHtml = str + dt + info.content;
        }
    }
}