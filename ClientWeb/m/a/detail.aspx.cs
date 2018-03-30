using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_m_all_detail : UniClientPage
{
    protected string introContent="";
    protected string hardContent = "";
    protected string images="";
    protected string unit = "";
    string type;
    string id;
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadPage();
        if (Request["class_id"] != null)
        {
            type = "";
            id = Request["class_id"];
        }
        else if (Request["kind_id"] != null)
        {
            type = "kind_";
            id = Request["kind_id"];
        }
        else if(Request["room_id"]!=null)
        {
            type = "rm_";
            id = Request["room_id"];
        }
        else if (Request["dev_id"] != null)
        {
            type = "dev_";
            id=Request["dev_id"];
        }
        if (Request["islong"] == "true")
        {
            unit = "("+Translate("单位")+": "+Translate("日期")+")";
        }
        else
        {
            unit = "(" + Translate("单位") + ": " + Translate("小时") + ")";
        }
        if (string.IsNullOrEmpty(id)) return;
        getContent();
    }

    private void getContent()
    {
        string prefix = "";
        uint ef = ToUInt(GetConfig("editForSubsys"));
        uint avail = ToUInt(GetConfig("availMobile"));
        string clsKind=Request["classkind"];
        if (clsKind == "8" && (ef & 8) > 0) prefix = "seat";
        else if (clsKind == "2" && (ef & 2) > 0) prefix = "cpt";
        string noImage = GetConfig("MobileNoImage");
        //介绍
        introContent = GetXmlContent(id, type+prefix+((avail&4)>0?"mIntro":"intro"));
        if (introContent.Length == 0)
        {
            infoIntro.Style.Add("display","none");
        }
        //相册
        if (noImage != "1")
        {
            string tmp = GetXmlContent(id, type +prefix+ "slide");
            List<string> list = GetSrcFromHtml(tmp);
            for (int i = 0; i < list.Count; i++)
            {
                string src = list[i];
                images += "<div class=\"swiper-slide\"><img data-src=\"" + src + "\" class=\"swiper-lazy\"><div class=\"preloader\"></div></div>";
            }
        }
        if (images.Length == 0)
        {
            infoAlbum.Style.Add("display", "none");
        }
    }
}