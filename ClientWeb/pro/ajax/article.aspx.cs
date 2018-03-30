using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_pro_ajax_article : UniClientAjax
{
    ArticleBLL ibll = new ArticleBLL();
    ArticleConverter articleConverter = new ArticleConverter();
    protected void Page_Load(object sender, EventArgs e)
    {
        string act = Request["act"];
        if (act != null)
        {
            Logger.trace("act="+act);
            if (act == "get_art_list_cl")
            {
                GetArtListByCls(-1);
            }
            else if (act == "get_all_art_list_cl")
            {
                GetArtListByCls(-2);
            }
            else if (act == "get_art_list_key")
            {
                GetArtListByKey();
            }
            else if (act == "get_con_art_list_cl")
            {
                GetArtListByCls(1);
            }
            else if (act == "get_art_id")
            {
                GetArtById();
            }
            else if (act == "new")
            {
                CreateArt();
            }
            else if (act == "save_xml_art")
            {
                if (Session["LOGIN_ACCINFO"] != null)
                {
                    UNIACCOUNT acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
                    //ADMINLOGINRES res = (ADMINLOGINRES)Session["LoginRes"];
                    if (!IsStat(acc.dwIdent, (uint)UNIACCOUNT.DWIDENT.EXTIDENT_MANAGER) && !IsStat(acc.dwIdent,(uint)UNIACCOUNT.DWIDENT.EXTIDENT_TEACHER))//管理员和教师 res.dwManRole, (uint)ADMINLOGINRES.DWMANROLE.MANIDENT_TEACHER
                    {
                        ErrMsg("没有权限");
                        return;
                    }
                    string data = Request["content"];
                    Logger.trace("data=" + data);
                    string id = Request["id"];
                    string title = Request["title"];
                    string type = Request["type"];
                    string attrs = Request["attrs"];
                    string state = Request["state"];
                    string file = Request["file"];
                    if (!string.IsNullOrEmpty(data) && !string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(type))
                    {
                        if (SaveXmlData(id, data, type, file, title, attrs, state))
                            SucMsg();
                        else
                            ErrMsg("存储失败");
                    }
                    else
                    {
                        ErrMsg("参数有误");
                    }
                }
                else
                {
                    ErrMsg("未登录或登录超时，请重新登录");
                }
            }
            else if (act == "get_xml_art")
            {
                string id = Request["id"];
                string type = Request["type"];
                string startline = Request["start_line"];
                string need = Request["need"];
                string start = Request["start"];
                string end = Request["end"];
                string file = Request["file"];
                string state = Request["state"];
                string byContent = Request["by_content"];
                if (!string.IsNullOrEmpty(type))
                {
                    if (string.IsNullOrEmpty(id))
                    {
                        bool by = false;
                        if (!string.IsNullOrEmpty(byContent) && byContent.ToLower() == "true")
                        {
                            by = true;
                        }
                        int m_startline = 1;
                        if (!string.IsNullOrEmpty(startline))
                        {
                            m_startline = Convert.ToInt32(startline);
                        }
                        int m_need = 0;
                        if (!string.IsNullOrEmpty(need))
                        {
                            m_need = Convert.ToInt32(need);
                        }
                        int m_start = 0;
                        if (!string.IsNullOrEmpty(start))
                        {
                            m_start = Convert.ToInt32(start);
                        }
                        int m_end = Int32.MaxValue;
                        if (!string.IsNullOrEmpty(end))
                        {
                            m_end = Convert.ToInt32(end);
                        }
                        XmlCtrl.XmlInfo[] list = GetXmlInfoList(type, file, m_startline, m_need, m_start, m_end, by);
                        SucRlt(list);
                    }
                    else
                    {
                        XmlCtrl.XmlInfo info = GetXmlInfo(id, type);
                        SucRlt(info);
                    }
                }
                else
                {
                    ErrMsg("参数有误");
                }
            }
        }
    }

    private void CreateArt()
    {
    }

    private void GetArtById()
    {
        if (Request["id"] == null)
        {
            ErrMsg("参数错误！");
            return;
        }
        int id = Convert.ToInt32(Request["id"]);
        InfoItem art = ibll.GetArticleByKey(id);
        if (art != null)
        {
            SucRlt(art);
            return;
        }
        ErrMsg("获取内容失败！");
    }

    private void GetArtListByKey()
    {
        if (Request["key"] == null)
        {
            ErrMsg("参数错误！");
            return;
        }
        string key = Server.UrlDecode(Request["key"]);
        int start = Convert.ToInt32(Request["start"]);
        int need = Convert.ToInt32(Request["need"]);
        List<InfoItem> arts = ibll.SearchAriticle(key, start, need);
        List<uniart> list = new List<uniart>();
        if (arts.Count > 0)
        {
            ToArt(ref list, arts);
            unipctrl pc = new unipctrl();
            pc.total = ibll.GetSearchAmount(key);
            pc.start = start;
            pc.need = need;
            SucRltPCtrl(list, pc);
            return;
        }
        ErrMsg("没有搜索到任何数据！");
    }

    private void GetArtListByCls(int sta)
    {
        if (string.IsNullOrEmpty(Request["cl"]))
        {
            ErrMsg("参数错误！");
            return;
        }
        int cls = Convert.ToInt32(Request["cl"]);
        int start = Convert.ToInt32(Request["start"]);
        int need = Convert.ToInt32(Request["need"]);
        List<InfoItem> arts;
        if (sta == -1)
        {
            arts = ibll.GetPubArticlesTitleByClass(cls, start, need);
        }
        else if (sta == -2)
        {
            arts = ibll.GetAllArticlesTitleByClass(cls, start, need);
        }
        else if (sta == 1)
        {
            arts = ibll.GetPubArticlesByClass(cls, start, need);
        }
        else
        {
            arts = new List<InfoItem>();
        }
        List<uniart> list = new List<uniart>();
        ToArt(ref list, arts);
        unipctrl pc = new unipctrl();
        pc.total = ibll.GetPubArticleNumberByClass(cls);
        pc.start = start;
        pc.need = need;
        SucRltPCtrl(list, pc);
    }
    private void ToArt(ref List<uniart> list, List<InfoItem> arts)
    {
        for (int i = 0; i < arts.Count; i++)
        {
            uniart art = new uniart();
            art.id = arts[i].Infoid.ToString();
            art.gr = arts[i].Infogroup.ToString();
            art.cl = arts[i].Infoclass.ToString();
            art.title = arts[i].Title;
            art.content = arts[i].Content;
            art.date = arts[i].Happendate > 0 ? articleConverter.ValueToDateStringConverter(arts[i].Happendate) : articleConverter.ValueToDateConverter(arts[i].Credate);
            list.Add(art);
        }
    }
    protected void SucRltPCtrl(object data, unipctrl pc)
    {
        JsRet(1, "ok", CodeJson(data), "{\"total\":\"" + pc.total + "\",\"start\":\"" + pc.start + "\",\"need\":\"" + pc.need + "\",\"name\":\"" + pc.name + "\"}");
    }
}

class uniart
{
    public string id;
    public string gr;
    public string grname;
    public string cl;
    public string clname;
    public string title;
    public string content;
    public string date;
}