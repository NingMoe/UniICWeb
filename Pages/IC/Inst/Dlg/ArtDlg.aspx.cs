using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Util;
using Model;
using BLL;
using UniWebLib;

public partial class Pages_DLG_ArtDlg : UniPage
{
    InfoClassBLL cbll = new InfoClassBLL();
    ArticleBLL ibll = new ArticleBLL();
    ArticleConverter articleConverter = new ArticleConverter();
    protected string previewUrl = "";
    protected string clsName = "";
    ModelClass cls;
    protected void Page_Load(object sender, EventArgs e)
    {
        string op = Request["op"];
        if (string.IsNullOrEmpty(op) || string.IsNullOrEmpty(Request["cl"]))
        {
            return;
        }
        cls = cbll.GetClassBySN(Convert.ToInt32(Request["cl"]));
        clsName = cls.Title;
        curClsSn.Value = cls.Sn.ToString();
        if (cls.Group == Util.GROUPENUM.news)//新闻类别
        {
            divNewDate.Attributes["class"] = "div_option";
            ListItem item=new ListItem("图片新闻","2");
            rblPublish.Items.Add(item);
        }
        if (op == "edit")
        {
            if (!string.IsNullOrEmpty(Request["id"]))
            {
                bind(Convert.ToInt32(Request["id"]));
            }
        }
    }
    private void bind(int artId)
    {
        InfoItem article = new InfoItem();
        article = ibll.GetArticleByKey(artId);
        curArtId.Value = article.Infoid.ToString();
        curClsSn.Value = article.Infoclass.ToString();
        curGrpSn.Value = article.Infogroup.ToString();
        tbTitle.Text = article.Title;
        tbSummary.Text = article.Summary;
        hfEditor.Value = article.Content;
        tbAuthor.Text = article.Author;
        if (article.Happendate>0)
        {
            ipNewsDate.Value = articleConverter.ValueToDateStringConverter(article.Happendate);
        }
        tbCreDate.Text = article.Credate.ToString();
        rblPublish.SelectedValue = article.Infostatus.ToString();
        Session["article"] = article;
    }
    protected void btSubmit_ServerClick(object sender, EventArgs e)
    {
        if (Request["op"] == "edit")
        {
            InfoItem article = new InfoItem();
            article.Infoid = Convert.ToInt32(curArtId.Value);
            article.Infoclass = Convert.ToInt32(curClsSn.Value);
            article.Infogroup = Convert.ToInt32(curGrpSn.Value);
            article.Title = Request[tbTitle.UniqueID];
            article.Summary = Request[tbSummary.UniqueID];
            article.Content = Request[hfEditor.UniqueID];
            article.Infostatus = Convert.ToInt32(Request[rblPublish.UniqueID]);
            article.Author = Request[tbAuthor.UniqueID];
            if (!string.IsNullOrEmpty(ipNewsDate.Value))
            {
                article.Happendate = articleConverter.DateStringToValueConverter(ipNewsDate.Value);
            }
            article.Credate = Convert.ToInt64(Request[tbCreDate.UniqueID]);
            if (ibll.UpdateArticle(article))
            {
                MessageBox("修改成功！", "提示", MSGBOX.SUCCESS);
                //Util.MessageBox.Show(this, "修改成功！");
            }
            else
            {
                MessageBox("修改失败！", "提示", MSGBOX.ERROR);
                //Util.MessageBox.Show(this, "修改失败！");
            }
        }
        else if (Request["op"] == "new")
        {
            InfoItem a = new InfoItem();
            int classsn = cls.Sn;
            int groupsn = cls.Group;
            a.Infoclass = classsn;
            a.Infogroup = groupsn;
            a.Title = tbTitle.Text;
            a.Summary = tbSummary.Text;
            a.Content = hfEditor.Value;
            a.Credate = DateTime.Now.ToFileTime();
            if (!string.IsNullOrEmpty(ipNewsDate.Value))
            {
                a.Happendate = articleConverter.DateStringToValueConverter(ipNewsDate.Value);
            }
            a.Infostatus = Convert.ToInt32(rblPublish.SelectedValue);
            a.Author = tbAuthor.Text;
            if (ibll.AddArticle(a) > 0)
            {
                MessageBox("添加成功！", "提示", MSGBOX.SUCCESS);
                //Util.MessageBox.Show(this, "添加成功！");
            }
            else
            {
                MessageBox("添加失败！", "提示", MSGBOX.ERROR);
                //Util.MessageBox.Show(this, "添加失败！");
            }
        }
    }
}