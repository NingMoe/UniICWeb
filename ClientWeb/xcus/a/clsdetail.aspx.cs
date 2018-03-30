using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_all_detail : UniClientPage
{
    protected string infoIntro = "";
    protected string infoHard = "";
    protected string infoRule = "";
    protected string infoTitle = "";
    protected string szPicZoom = "";
    protected string szPicPath = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadPage();
        classId.Value = Request["classId"];
        classKind.Value = Request["classKind"];
        isLong.Value = Request["isLong"];
        isKind.Value = Request["isKind"];
        infoTitle = className.Value = Server.UrlDecode(Request["className"]);
        InitCld();
        InitContent();
    }

    private void InitCld()
    {
        Cld.ClassKind = classKind.Value;
        Cld.DevClassId = classId.Value;
        Cld.IsLong = isLong.Value;
        Cld.IsKind = isKind.Value;
        Cld.Width=Request["w"];
    }

    private void InitContent()
    {
     
        infoIntro = GetXmlContent(classId.Value, "intro");
        infoRule = GetXmlContent(classId.Value, "rule");
        infoHard = GetXmlContent(classId.Value, "hard");
        InitXmlSlide(classId.Value, "slide", ref szPicZoom, ref szPicPath);
    }
}