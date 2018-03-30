using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class Article : UniClientPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = Request["id"];
        string type = Request["type"];
        if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(type))
        {
            XmlCtrl.XmlInfo info = GetXmlInfo(id, type);
            string str = "";
            if (!string.IsNullOrEmpty(info.title))
                str = "<div class='info_title'>" + info.title + "</div>";
            string dt = "";
            if (!string.IsNullOrEmpty(info.alter))
            {
                DateTime tmp = Util.Converter.StrToDate(info.alter);
                dt = "<div class='info_date'>" + tmp.ToString("yyyy年MM月dd日 HH时mm分") + "</div>";
            }
            divContent.InnerHtml = str +dt+ info.content;
            //DateTime dt = Util.Converter.StrToDate(info.date);
            //str += "<div class='info_date'>"+dt.ToString("yyyy年MM月dd日 HH:mm")+"</div>";
            //divTitle.InnerHtml = str;
        }
    }
}