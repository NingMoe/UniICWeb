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
    private uint mode;
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadPage();
        mode = ToUInt(Request["mode"]);
        classKind.Value = Request["classKind"];
        infoId.Value = Request["id"];
        infoTitle = infoName.Value = Server.UrlDecode(Request["name"]);
        InitCld();
        InitContent();
    }

    private void InitCld()
    {
        if ((mode & 2) > 0)
        {
            Cld.KindId = infoId.Value;
            Cld.SrcType = "kind";
            UNIDEVKIND kind = GetDevKind(ToUInt(infoId.Value));
            if (kind.dwProperty != null && kind.dwProperty > 0)
            {
                if ((kind.dwProperty & (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_LONGTERMRESV) > 0 && GetConfig("resvAllDay") == "1") Cld.IsLong = "true";
                if ((kind.dwProperty & (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_KINDRESV) > 0) Cld.IsKind = "true";
            }
        }
        else
        {
            Cld.DevClassId = infoId.Value;
            Cld.SrcType = "cls";
            DEVKINDREQ req = new DEVKINDREQ();
            if (!string.IsNullOrEmpty(classKind.Value) && classKind.Value != "0")
                req.dwClassKind = ToUInt(classKind.Value);
            UNIDEVKIND[] kinds;
            if (m_Request.Device.DevKindGet(req, out kinds) == REQUESTCODE.EXECUTE_SUCCESS && kinds.Length > 0)
            {
                for (int i = 0; i < kinds.Length; i++)
                {
                    if (kinds[i].dwClassID == ToUInt(infoId.Value))
                    {
                        if ((kinds[i].dwProperty & (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_LONGTERMRESV) > 0 && GetConfig("resvAllDay") == "1") Cld.IsLong = "true";
                        if ((kinds[i].dwProperty & (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_KINDRESV) > 0) Cld.IsKind = "true";
                        break;
                    }
                }
            }
        }
        Cld.ClassKind = classKind.Value;
        Cld.Width=Request["w"];
    }

    private void InitContent()
    {
        string prefix = "";

        //子系统
        uint clsKind = ToUInt(classKind.Value);
        uint ef = ToUInt(GetConfig("editForSubsys"));
        if (clsKind == 8 && (ef & 8) > 0) prefix = "seat";
        else if (clsKind == 2 && (ef & 2) > 0) prefix = "cpt"; 
        //资源分类
        if ((mode & 2) > 0)
        {
            prefix += "kind_";
        }
        infoIntro = GetXmlContent(infoId.Value, prefix + "intro");
        infoRule = GetXmlContent(infoId.Value, prefix + "rule");
        infoHard = GetXmlContent(infoId.Value, prefix + "hard");
        InitXmlSlide(infoId.Value, prefix + "slide", ref szPicZoom, ref szPicPath);
    }
}