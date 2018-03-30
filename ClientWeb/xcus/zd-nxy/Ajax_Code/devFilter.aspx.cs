using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Xml;
using UniWebLib;
using UniStruct;
using Util;

public partial class DevWeb_Ajax_Code_devFilter : UniClientPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.LoadPage();
        string con = Request["con"];

        string[] list = con.Split('&');
        string clskey = "";
        string labkey = "";
        string mankey = "";
        //string devcls = "";
        string searchkey = "";
        string pctrlId = "";
        uint pctrlStar = 0;
        uint pctrlNeed = 15;
        uint showMode = 1;
        if (list != null && list.Length > 0)
        {
            searchkey = list[0];
            mankey = list[1];
            clskey = list[2];
            labkey = list[3];
            //devkind = subCon(list[2]);
            //devcls = subCon(list[3]);

            pctrlId = list[list.Length - 3];
            pctrlStar = Convert.ToUInt32(list[list.Length - 3]);
            pctrlStar = pctrlStar > 0 ? pctrlStar - 1 : 0;
            pctrlNeed = Convert.ToUInt32(list[list.Length - 2]);
            if (!string.IsNullOrEmpty(list[list.Length - 1]))
            {
                showMode = Convert.ToUInt32(list[list.Length - 1]);
            }
        }



        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        DEVREQ vrGet = new DEVREQ();
        if (clskey != "")
        {
            vrGet.szClassIDs = clskey;
        }
        else
        {
            //临时 过滤掉非设备类别
            vrGet.szClassIDs = "56,57,58,103795651";
        }
        if (labkey != "")
        {
            vrGet.szLabIDs = labkey;
        }
        if (!string.IsNullOrEmpty(mankey))
        {
            vrGet.szAttendantName = mankey;
        }
        if (!string.IsNullOrEmpty(searchkey))
        {
            vrGet.szSearchKey = searchkey;
        }
        //分页
        REQEXTINFO vrGetExtInfo = new REQEXTINFO();
        vrGetExtInfo.dwNeedLines = pctrlNeed;
        vrGetExtInfo.dwStartLine = pctrlStar;
        vrGet.szReqExtInfo = vrGetExtInfo;
        UNIDEVICE[] vtResult;

        dwRlt rlt = new dwRlt();
        rlt.needLines = (int)pctrlNeed;
        rlt.pageCtrlID = "pCtrl";
        List<dwDevIntro> devs = new List<dwDevIntro>();
        uResponse = m_Request.Device.Get(vrGet, out vtResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtResult.Length > 0)
        {
            for (int i = 0; i < vtResult.Length; i++)
            {
                dwDevIntro dev = new dwDevIntro();
                dev.id = vtResult[i].dwDevID.ToString();
                dev.name = vtResult[i].szDevName;
                dev.model = vtResult[i].szModel;
                dev.campus = vtResult[i].szCampusName;
                dev.col = vtResult[i].szDeptName;
                dev.cls = vtResult[i].szClassName;
                dev.lab = vtResult[i].szLabName;
                dev.manager = vtResult[i].szAttendantName;
                dev.phone = vtResult[i].szAttendantTel;
                dev.intro = vtResult[i].szCampusName.ToString() + "，" + vtResult[i].szClassName.ToString();
                dev.url = GetImgS(vtResult[i].dwDevSN);
                //仪器状态
                if (Converter.GetDevStat(vtResult[i].dwDevStat))
                {
                    dev.devstat = Converter.GetDevRunStat(vtResult[i].dwRunStat);
                }
                else
                {
                    dev.devstat = "<span style='color:red'>不可用</span>";
                }
                devs.Add(dev);
            }
            //获取分页
            REQEXTINFO new_ext;
            if (m_Request.Device.UTGetDetail(out new_ext) && new_ext.dwTotolLines != null && new_ext.dwTotolLines > 0)
            {
                rlt.totolLines = (int)new_ext.dwTotolLines;
                rlt.startLine = (int)new_ext.dwStartLine;
            }
            else
            {
                rlt.totolLines = vtResult.Length;
                rlt.startLine = 0;
            }
        }
        else
        {
            rlt.totolLines = 0;
            rlt.startLine = 0;
        }
        rlt.devs = devs;
        rlt.showMode = 0;//显示模式 0 模块 1 列表
        Response.ContentType = "application/Json";
        Response.Write(JsonConvert.SerializeObject(rlt));
    }
    string subCon(string str)
    {
        if (!string.IsNullOrEmpty(str))
        {
            return str.Substring(0, str.Length - 1);
        }
        return "";
    }
}
class dwDevIntro
{
    public string id;
    public string url;
    public string name;
    public string model;
    public string campus;
    public string col;
    public string cls;
    public string lab;
    public string manager;
    public string phone;
    public string intro;
    public string runstat;
    public string devstat;
}
class dwRlt
{
    public List<dwDevIntro> devs;
    public string pageCtrlID;
    public int totolLines;
    public int startLine;
    public int needLines;
    public int showMode;
}
