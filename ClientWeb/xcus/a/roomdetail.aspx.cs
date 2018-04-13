using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_all_roomdetail : UniClientPage
{
    protected string infoIntro = "";
    protected string infoHard = "";
    protected string infoRule = "";
    protected string infoTitle = "";
    protected string szPicZoom = "";
    protected string szPicPath = "";
    protected string noResv = "";
    protected uint clsKind;
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadPage();
        roomId.Value = Request["roomId"];
        roomName.Value = infoTitle = Server.UrlDecode(Request["roomName"]);
        clsKind = ToUInt(Request["classKind"]);
        InitCld();
        InitContent();
    }

    private void InitCld()
   {
        Cld.SrcType = "rm";
        Cld.Disable = "true";
        if (Request["disable"] == "true")
        {
            noResv = "none";
            return;
        }
        string resvStateMode = GetConfig("resvStateMode");//兼容旧20150915前
        uint fpClsKind = ToUInt(GetConfig("floorPlanClsKind"));
        DEVREQ req = new DEVREQ();
        req.szRoomIDs = roomId.Value;
        UNIDEVICE[] rlt;
        if (m_Request.Device.Get(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS && rlt.Length > 0)
        {
            UNIDEVKIND kind = GetDevKind(rlt[0].dwKindID);
            if (kind.dwKindID != null)
            {
                if (clsKind != 0 && !IsStat(kind.dwClassKind, clsKind))
                {
                    noResv = "none";
                    return;
                }
                Cld.Disable = "false";
                Cld.RoomId = roomId.Value;
                //平面图
                if ((resvStateMode == "1" && fpClsKind == 0) || (fpClsKind & kind.dwClassKind) > 0)
                {
                    Cld.DisplayMode = "fp";
                    string path = ToUploadUrl("DevImg/FloorPlan/rm" + roomId.Value + ".jpg");
                    Cld.Img = path;
                    if (!File.Exists(Server.MapPath(path)))
                        MsgBox("缺少平面图<br/>" + path);
                }
                else
                {//列表
                    Cld.DisplayMode = "dlg";
                    Cld.ClassKind = kind.dwClassKind.ToString();
                    if (GetConfig("resvAllDay") == "1" && (kind.dwProperty & (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_LONGTERMRESV) > 0)
                    {
                        Cld.IsLong = "true";
                    }
                    if ((kind.dwProperty & (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_KINDRESV) > 0)
                    {
                        Cld.IsKind = "true";
                    }
                    else
                        Cld.IsKind = "false";
                }
            }
        }
    }

    private void InitContent()
    {
        string prefix = "";
        uint ef = ToUInt(GetConfig("editForSubsys"));
        if (clsKind == 8 && (ef & 8) > 0) prefix = "seat";
        else if (clsKind == 2 && (ef & 2) > 0) prefix = "cpt";
        infoIntro = GetXmlContent(roomId.Value, "rm_" + prefix + "intro");
        infoRule = GetXmlContent(roomId.Value, "rm_" + prefix + "rule");
        infoHard = GetXmlContent(roomId.Value, "rm_" + prefix + "hard");
        InitXmlSlide(roomId.Value, "rm_" + prefix + "slide", ref szPicZoom, ref szPicPath);
    }
}