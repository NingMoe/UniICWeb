using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_pro_page_other_notice : UniClientPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string type = Request["type"];
        string id = Request["id"];
        if (string.IsNullOrEmpty(type) || string.IsNullOrEmpty(id)) {
            type = "other";
            id = "rule";
        }
        //string str = "";
        //if (!string.IsNullOrEmpty(info.title))
        //    str = "<h1>" + info.title + "</h1>";
        //string dt = "";
        //if (!string.IsNullOrEmpty(info.alter))
        //{
        //    DateTime tmp = Util.Converter.StrToDate(info.alter);
        //    dt = "<div class='info_date'>" + tmp.ToString("yyyy年MM月dd日 HH时mm分") + "</div>";
        //}
        noticeCon.InnerHtml = GetXmlContent(id, type);
    }
}