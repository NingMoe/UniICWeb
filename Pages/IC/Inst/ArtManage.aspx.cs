using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Util;
using Model;
using BLL;
using UniWebLib;
public partial class Pages_Inst_ArtManage : UniPage
{
    InfoClassBLL cbll = new InfoClassBLL();
    ArticleBLL ibll = new ArticleBLL();
    ArticleConverter converter = new ArticleConverter();
    protected string clsList = "";
    protected string artList = "";
    protected string clsName = "";
    protected string clsSn = "";
    int clsid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        List<ModelClass> list = cbll.GetClasses();
        if (list != null && list.Count > 0)
        {
            clsid = list[0].Sn;
            for (int i = 0; i < list.Count; i++)
            {
                clsList += "<a href='ArtManage.aspx?cl=" + list[i].Sn + "' class='cls_item' cls='" + list[i].Sn + "'>" + list[i].Title + "</a>";
            }
            InitArtList();
        }
    }

    private void InitArtList()
    {
        if (Request["cl"] != null)
        {
            clsid = Convert.ToInt32(Request["cl"]);
        }
        clsSn = clsid.ToString();
        REQEXTINFO ext;
        GetPageCtrlValue(out ext);
        if (ext.dwStartLine == null)
            ext.dwStartLine = 1;
        else
            ext.dwStartLine++;
        List<InfoItem> list = ibll.GetAllArticlesTitleByClass(clsid, (int)ext.dwStartLine, (int)ext.dwNeedLines);
        if (list != null && list.Count > 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                InfoItem art = list[i];
                artList += "<tr>";
                artList += "<td>" + art.Title + "</td>";
                artList += "<td>" + art.Summary + "</td>";
                artList += "<td >" + art.Author + "</td>";
                artList += "<td>" + converter.ValueToDateConverter(art.Credate) + "</td>";
                artList += "<td >" + converter.StatusConverter(art.Infostatus) + "</td>";
                artList += "<td >" + "<a class='edit' artId='" + art.Infoid + "' style='cursor:pointer;color:#0094ff;'>编辑</a> | " +
                    "<a target='_blank' href='" + ResolveClientUrl("~/ClientWeb/xcus/zyy/ArticleList.aspx") + "?gr=" + art.Infogroup + "&art=" + art.Infoid + "'>预览</a></td>";
                artList += "</tr>";
            }
            ext.dwTotolLines = (uint)ibll.GetAllArticleNumberByClass(clsid);
            updatePCtrl(ext);
        }
    }

    private void updatePCtrl(REQEXTINFO ext)
    {
        string szPageScript = UniLibrary.ObjHelper.OBJ2JS(ext, "_", ext.GetType());
        if (!string.IsNullOrEmpty(szPageScript))
        {
            System.Web.UI.HtmlControls.HtmlGenericControl ScriptInclude = new System.Web.UI.HtmlControls.HtmlGenericControl("script");
            ScriptInclude.Attributes.Add("type", "text/javascript");
            ScriptInclude.InnerHtml = "function SetPageValue(){ PutHttpValue(" + szPageScript + ");}$(SetPageValue);";
            if (Header != null && Header.Controls != null)
            {
                Header.Controls.Add(ScriptInclude);
            }
            else if (Form != null && Form.Controls != null && !Form.Controls.IsReadOnly)
            {
                Form.Controls.Add(ScriptInclude);
            }
            else
            {
                this.Controls.Add(ScriptInclude);
            }
        }
    }
}