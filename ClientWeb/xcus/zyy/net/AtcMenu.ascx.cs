using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL;
using Util;

public partial class ClientWeb_Modules_AtcMenu : System.Web.UI.UserControl
{
    protected string gName = "";
    string cls;
    string art;
    ArticleBLL ibll = new ArticleBLL();
    InfoClassBLL cbll = new InfoClassBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        cls = Request["cl"];
        art = Request["art"];
        if (!string.IsNullOrEmpty(art))
        {
            int clsid= ibll.GetArticleByKey(Convert.ToInt32(art)).Infoclass;
            InitMenu(clsid.ToString());
        }
        else if (!string.IsNullOrEmpty(cls))
        {
            InitMenu(cls);
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }

    private void InitMenu(string cls)
    {
        if (cls=="rsch")
        {
            gName = "文章搜索";
            cList.InnerHtml = "搜索结果";
            return;
        }
        ModelClass cl = cbll.GetClassBySN(Convert.ToInt32(cls));
            if (cl != null)
            {
                gName = cl.GroupTitle;
                List<ModelClass> list = new List<ModelClass>();
                list = cbll.GetClassesByGroupSN(cl.Group);
                string str = "";
                for (int i = 0; i < list.Count; i++)
                {
                    str += "<li><a  class='click'  href='ArticleList.aspx?gr="+list[i].Group+"&cl=" + list[i].Sn + "'>" + list[i].Title + "</a></li>";
                }
                cList.InnerHtml = str;
            }
    }
}