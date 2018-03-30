using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL;
using Util;
using UniWebLib;

public partial class ArticleList : UniClientPage
{
    ArticleBLL ibll = new ArticleBLL();
    InfoClassBLL cbll = new InfoClassBLL();
    ArticleConverter articleConverter = new ArticleConverter();
    protected string pagePosition = "";
    protected string iframeUrl = "";
    protected int start = 0;
    protected int need = 0;
    protected string showMode = "list";
    string cls;
    string art;
    protected void Page_Load(object sender, EventArgs e)
    {
        cls = Request["cl"];
        art = Request["art"];
        if (!string.IsNullOrEmpty(art))
        {
            InitArticle(art);
        }
        else if (!string.IsNullOrEmpty(cls))
        {
            InitClass(Convert.ToInt32(cls));
        }
    }
    private void InitClass(int cls)
    {
        if (ibll.GetPubArticleNumberByClass(cls) == 1)
        {
            string artid = ibll.GetPubArticlesTitleByClass(cls, 1, 1)[0].Infoid.ToString();
            InitArticle(artid);
        }
        else
        {
            ModelClass cl = cbll.GetClassBySN(cls);
            if (cl != null)
            {

                pagePosition += ">>" + cl.GroupTitle + ">>" + cl.Title;
            }
        }
    }
    private void InitArticle(string artid)
    {
        showMode = "art";
        int id = Convert.ToInt32(artid);
        ibll.PlusHitCountByKey(id);
        InfoItem art = ibll.GetArticleByKey(id);
        if (art != null)
        {
            ModelClass cl = cbll.GetClassBySN(art.Infoclass);
            if (cl != null)
            {
                pagePosition += ">>" + cl.GroupTitle + ">><a href='ArticleList.aspx?gr=" + cl.Group + "&cl=" + cl.Sn + "'>" + cl.Title + "</a>>>详细信息";
            }
            lbArticleTitle.Text = art.Title;
            if (art.Happendate > 10000000)
                lbTime.Text = articleConverter.ValueToDateStringConverter(art.Happendate);
            else
                lbTime.Text = DateTime.FromFileTime(art.Credate).ToString("yyyy-MM-dd HH:mm");
            lbHit.Text = art.Hitcount.ToString();
            lbAuthor.Text = art.Author;
            iframeUrl = "Article.aspx?id=" + id;
        }
    }
    private void InitPostion(int cls)
    {
        ModelClass cl = cbll.GetClassBySN(cls);
        pagePosition += "<a href='ArticleList.aspx?gr=" + cl.Group + "&cl=" + cl.Sn + ">" + cl.Title + "</a>";
    }
}