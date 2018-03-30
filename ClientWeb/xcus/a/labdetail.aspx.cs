using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using System.IO;

public partial class ClientWeb_xcus_all_labdetail : UniClientPage
{
    protected string infoIntro = "";
    protected string infoHard = "";
    protected string infoRule = "";
    protected string infoTitle = "";
    protected string szPicZoom = "";
    protected string szPicPath = "";
    protected string noResv = "";
    protected string loanList = "";
    DEVRESVSTAT[] devstas;
    UNIDEVICE[] devs;
    protected string devDetail = "";
    protected uint clsKind;
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadPage();
        labId.Value = Request["labId"];
        labName.Value = infoTitle = Server.UrlDecode(Request["labName"]);
        clsKind = ToUInt(Request["classKind"]);
        InitCld();
        InitContent();
        if (GetConfig("resvLoanDetail") == "1")
            InitLoan();
    }

    private void InitCld()
    {
        if (Request["disable"] == "true")
        {
            noResv = "none";
            Cld.Disable = "true";
            return;
        }
        string resvStateMode = GetConfig("resvStateMode");//兼容旧20150915前
        uint fpClsKind = ToUInt(GetConfig("floorPlanClsKind"));
        bool isFloorPlan=((fpClsKind&clsKind)>0)||(resvStateMode=="1"&&fpClsKind==0);
        Cld.LabId = labId.Value;
        UNILAB[] labs= GetLab(ToUInt(labId.Value));
        if (labs != null && labs.Length == 1)
        {
            if (isFloorPlan)
            {
                Cld.DisplayMode = "fp";
                string path = ToUploadUrl("DevImg/FloorPlan/" + labId.Value + ".jpg");
                Cld.Img = path;
                if (!File.Exists(Server.MapPath(path)))
                    MsgBox("缺少平面图<br/>" + path);
            }
            else
            {
                Cld.DisplayMode = "dlg";
                Cld.SrcType = "lab";
                Cld.LabId = labId.Value;
            }
        }
    }

    private void InitContent()
    {
        infoIntro = GetXmlContent(labId.Value, "lab_intro");
        infoRule = GetXmlContent(labId.Value, "lab_rule");
        infoHard = GetXmlContent(labId.Value, "lab_hard");
        InitXmlSlide(labId.Value, "lab_slide", ref szPicZoom, ref szPicPath);
    }
    private void InitLoan()
    {
        DEVKINDFORLONGRESVREQ req = new DEVKINDFORLONGRESVREQ();
        req.dwClassKind = (uint)UNIDEVCLS.DWKIND.CLSKIND_LOAN;
        req.szLabIDs = labId.Value;
        req.dwResvPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL | (uint)UNIRESERVE.DWPURPOSE.USEFOR_TEACHING | (uint)UNIRESERVE.DWPURPOSE.USEFOR_RESERVED;
        req.dwStartDate = ToUInt(DateTime.Now.ToString("yyyyMMdd"));
        req.dwEndDate = req.dwStartDate;
        DEVKINDFORRESV[] rlt;
        if (m_Request.Device.GetDevKindForLongResv(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                DEVKINDFORRESV kind = rlt[i];
                GetDevStatList(kind.dwKindID);
                string num = kind.szUsableNumArray;
                if (num == "A") num = "9+";
                else if (num == "U") num = "<span class='grey'>不开放</span>";
                loanList += "<tr><td><a class='click_detail' kind='"+kind.dwKindID+"'>" + kind.szKindName + "</a></td><td class='text-center'>" + kind.szModel + "</td>"
                    + "<td class='text-center'>" + kind.dwTotalNum + "</td><td class='text-center'>" + num + "</td><td class='text-center'><a onclick='loanResv(" + kind.dwKindID + ")'>租借</a></td></tr>";

            }
        }
        else
        {
            MsgBoxH(m_Request.szErrMsg);
        }
    }
    private UNIDEVICE[] GetDeviceByLab()
    {
        DEVREQ req = new DEVREQ();
        req.szLabIDs = labId.Value;
        UNIDEVICE[] rlt;
        if (m_Request.Device.Get(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
            return rlt;
        else
            return null;
    }
    //private DEVRESVSTAT[] GetDevResvStatByLab()
    //{
    //    DEVRESVSTATREQ req = new DEVRESVSTATREQ();
    //    req.szLabIDs = labId.Value;
    //    req.dwClassKind = (uint)UNIDEVCLS.DWKIND.CLSKIND_LOAN;
    //    req.dwResvPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL | (uint)UNIRESERVE.DWPURPOSE.USEFOR_TEACHING | (uint)UNIRESERVE.DWPURPOSE.USEFOR_RESERVED;
    //    req.szDates = DateTime.Now.ToString("yyyyMMdd");
    //    DEVRESVSTAT[] rlt;
    //    m_Request.Device.GetDevResvStat(req, out rlt);
    //    return rlt;
    //}
    private void GetDevStatList(uint? kind)
    {
        if (devs == null)
        {
            devs = GetDeviceByLab();
        }
        List<UNIDEVICE> list = new List<UNIDEVICE>();
        if (devs != null && devs.Length > 0)
        {
            for (int i = 0; i < devs.Length; i++)
            {
                if (kind == devs[i].dwKindID)
                {
                    list.Add(devs[i]);
                }
            }
            devDetail += "<div id='detail_" + kind + "'><table class='detail_tbl table table-striped table-condensed'><thead><th>设备号</th><th>采购日期</th><th>采购价格</th><th>预约状态</th></thead><tbody>";
            for (int j = 0; j < list.Count; j++)
            {
                UNIDEVICE dev=list[j];
                devDetail += "<tr><td>" + dev.dwDevID + "</td><td>" + ToDate(dev.dwPurchaseDate) + "</td><td>" + dev.dwUnitPrice + " 元</td><td>" + ((dev.dwRunStat & (int)UNIDEVICE.DWRUNSTAT.DEVSTAT_RESERVE) > 0 ? "<span class='green'>有预约</span>" : "<span class='grey'>无预约</span>") + "</td></tr>";
            }
            devDetail += "</tbody></table></div>";
        }
    }
    private string ToDate(uint? date)
    {
        if (date == null||date==0) return "";
        int dt = (int)date;
        int y = dt / 10000;
        int m = (dt % 10000) / 100;
        int d = dt % 100;
        return y +"-"+ m.ToString("00") +"-"+ d.ToString("00");
    }
}