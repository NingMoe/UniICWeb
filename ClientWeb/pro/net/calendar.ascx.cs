using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using Newtonsoft.Json;
using System.Collections;
using System.IO;
using System.Xml;

public partial class ClientWeb_pro_net_calendar : UniClientModule
{
    protected string id = "";
    protected string dload = "";
    protected string themeOptions = "";
    protected string resvKinds = "";
    UNIRESERVE.DWPURPOSE usetype = UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL;
    string isLong = "";
    public string IsLong
    {
        get { return isLong; }
        set { isLong = value; }
    }
    string displayMode = "cld";//模式 cld为列表 fp为平面图
    public string DisplayMode
    {
        get { return displayMode; }
        set { displayMode = value; }
    }
    string width = "";
    public string Width
    {
        get { return width; }
        set { width = value; }
    }
    string height = "";
    public string Height
    {
        get { return height; }
        set { height = value; }
    }
    string img = "";
    public string Img
    {
        get { return img; }
        set { img = value; }
    }
    string isKind = "";
    public string IsKind
    {
        get { return isKind; }
        set { isKind = value; }
    }
    string devClassId = "";
    public string DevClassId
    {
        get { return devClassId; }
        set { devClassId = value; }
    }
    string labId = "";
    public string LabId
    {
        get { return labId; }
        set { labId = value; }
    }
    string roomId = "";
    public string RoomId
    {
        get { return roomId; }
        set { roomId = value; }
    }
    string classKind = "";
    public string ClassKind
    {
        get { return classKind; }
        set { classKind = value; }
    }
    string mode = "d";
    public string Mode
    {
        get { return mode; }
        set { mode = value; }
    }
    string disable = "";
    public string Disable
    {
        get { return disable; }
        set { disable = value; }
    }
    string dev = "";
    public string Dev
    {
        get { return dev; }
        set { dev = value; }
    }
    string kindId = "";
    public string KindId
    {
        get { return kindId; }
        set { kindId = value; }
    }
    string alone = "false";
    public string Alone
    {
        get { return alone; }
        set { alone = value; }
    }
    string name = "default";
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    string purpose = "";
    public string Purpose
    {
        get { return purpose; }
        set { purpose = value; }
    }
    string srcType = "";
    public string SrcType
    {
        get { return srcType; }
        set { srcType = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //子系统配值
        //检查房间类型是否可以有申请报告链接
        
        string szKindID = kindId;
        Logger.trace("szKindID="+ szKindID);
        if (szKindID != null&& szKindID != "")
        {
            DEVKINDREQ kindreq = new DEVKINDREQ();
            kindreq.dwKindID = ToUInt(szKindID);
            UNIDEVKIND[] kindRes;
            if (m_Request.Device.DevKindGet(kindreq, out kindRes) == REQUESTCODE.EXECUTE_SUCCESS && kindRes != null && kindRes.Length > 0)
            {
                Logger.trace("szKinddevurl=" + kindRes[0].szDevKindURL.ToString());
                string path = Server.MapPath("~/" + kindRes[0].szDevKindURL.ToString());
                if (File.Exists(path))
                {
                    dload = ResolveClientUrl("~/" + kindRes[0].szDevKindURL.ToString());
                }
            }
            else {
                Logger.trace("获取devkindurl信息失败");
            }
        }

        if (!string.IsNullOrEmpty(Request["classKind"]))
            classKind = Request["classKind"];

        id = DateTime.Now.Ticks.ToString();
        //下载路径
        string up = "~/ClientWeb/upload/";
        string dir = Server.MapPath(up + "info/xmlData/");
        XmlCtrl ctrl = new XmlCtrl("ics_data", dir);
        string file = ctrl.GetXmlContent("resv_file_template", "other").content;
        if (!string.IsNullOrEmpty(file)&&dload=="")
        {
            string path = Server.MapPath(up + "UpLoadFile/") + file;
            if (File.Exists(path))
            {
                dload = ResolveClientUrl(up + "UpLoadFile/") + file;
            }
        }
        ///

        //可选主题
        string themes = GetConfig("fixTheme");
        if (themes == "1")
        {
            XmlNodeList list = common.GetXMLConst(Server.MapPath("~/LocalFile/file.xml"), "ResvTheme");
            if (list != null)
            {
				if((ToUInt(GetConfig("resvKind")) & 1) > 0)
				{
					                    themeOptions += "<option value='0'>" + "未选择" + "</option>";
				}
                foreach (XmlNode item in list)
                {
                    string opt = item.InnerText;
                    themeOptions += "<option value='" + opt + "'>" + opt + "</option>";
                }
            }
        }
        //预约类型
        if ((ToUInt(GetConfig("resvKind")) & 1) > 0)
        {
            CODINGTABLEREQ req = new CODINGTABLEREQ();
            req.dwCodeType = (uint)CODINGTABLE.DWCODETYPE.CODE_RESVKIND;
            CODINGTABLE[] rlt;
            if (m_Request.System.GetCodingTable(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                for (int i = 0; i < rlt.Length; i++)
                {
                    resvKinds += "<option value='" + rlt[i].szCodeSN + "'>" + rlt[i].szCodeName + "</option>";
                }
            }
        }
    }
    public uint ToUInt(object obj)
    {
        try
        {
            return Convert.ToUInt32(obj);
        }
        catch (Exception)
        {
            return 0;
        }
    }
}