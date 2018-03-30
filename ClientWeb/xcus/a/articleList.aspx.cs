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
        string noticeList = "";
        XmlCtrl.XmlInfo[] list = GetXmlInfoList("notice",1000);
        if (list != null && list.Length > 0)
        {
            for (int i = 0; i < list.Length; i++)
            {
                string id = list[i].id;
                string title = list[i].title;
                string date = list[i].date;
                string content = list[i].content;
                if (string.IsNullOrEmpty(date)) date = "0";
                string dt = Util.Converter.StrToDate(date).ToString("yyyy/MM/dd HH:mm");
                noticeList += "<li class='noticeInfoli' date='" + date + "' id='" + id + "'><div class='title'>▪ <a class='detail' url='123article.aspx?back=true&type=notice&id=" + id + "' cache='#cache_con'>" + title + "</a></div><div class='grey songti' style='border-bottom:1px dotted #ddd;'>" + dt + "</div></li>";

            }
        }

        divContent.InnerHtml = noticeList;


    }
}