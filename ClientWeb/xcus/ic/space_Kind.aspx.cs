using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using UniWebLib;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;

public partial class Page_ : UniClientPage
{
    protected string szActDisplay = "display";
    protected string cmpHide = "";
    protected string classId = "";
    protected string szSrc = "";
    protected string szBackPage = "";
    public string szKindUrl = "";
    public string szPicPath = "";
    public string szPicZoom = "";
    public string szPicHard = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        base.LoadPage();
        if (IsLogined())
        {
            szActDisplay = "none";
        }
        else
        {
            szActDisplay = "display";
        }
        string clsKind = Request["classKind"];
        //if (clsKind == "2")//清华  电子阅览室不显示预约
        //    cmpHide = "none";
        classId = Request["classId"];
        if (classId == null)
        {
            if (Session["classKindIDkindPage"] != null)
            {
                classId = Session["classKindIDkindPage"].ToString();
            }
        }
        if (classId != null)
        {
            GetKindImg(classId);
            Session["classKindID"] = classId;
            DEVKINDREQ vrGet = new DEVKINDREQ();
            vrGet.dwKindID = ToUInt(classId);
            UNIDEVKIND[] vtRes;
            m_Request.Device.DevKindGet(vrGet, out vtRes);
            if (vtRes != null && vtRes.Length > 0)
            {
                //szKindUrl = vtRes[0].szDevKindURL.ToString();
                classId = vtRes[0].dwClassID.ToString();
                szKindUrl = GetContent(classId, "intro");
            }
            Session["classKindIDkindPage"] = classId;


            //为浙大页面修改的bydevclass
            szKindUrl = GetContent(classId, "intro");
        }
        string szByKind = Request["isbyKind"];
        string szReqLongResv = Request["isLongResv"];
        string szBackPage = HttpContext.Current.Request.Url.AbsolutePath;
        Session["szBackPage"] = "space_Kind_research.aspx#space_tab_3";
    }

    private string GetContent(string id, string type)
    {
        return GetXmlContent(id, type, "ics_data");
    }

    private void GetKindImg(string classId)
    {
        string tmp = GetContent(classId, "slide");
        List<string> list = GetSrcFromHtml(tmp);
        for (int i = 0; i < list.Count; i++)
        {
            string src = list[i];
            if (i == 0)
            {
                szPicZoom = "<img src='" + src + "' width=\"510\" height=\"350\">  ";
                szPicPath += "<li><a href='' class='cur' ><img src='" + src + "' width='84' height='55'></a></li>";
            }
            else
            {
                szPicPath += "<li><a href=''><img src='" + src + "' width='84' height='55'></a></li>";
            }
        }
        tmp = GetContent(classId, "hard");
        szPicHard = tmp;
    }
}
