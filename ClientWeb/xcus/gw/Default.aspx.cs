using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_all_Info : UniClientPage
{
    protected string itemList = "";
    protected string closeDevCls = "";
    YARDACTIVITY activity;
    protected bool islogin = false;
    protected string openAty = "";
    List<uint?> closeCls = new List<uint?>();
    protected void Page_Load(object sender, EventArgs e)
    {
        islogin = IsLogined();
        openAty = GetConfig("openActivity");
        string lg = GetConfig("mustLogin");
        if (lg == "1" && !islogin)
            Response.Redirect("../ic2/Login.aspx?sys=person");
        InitCloseCls();
        InitItemList();
    }

    private void InitCloseCls()
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        DEVCLSREQ vrGet = new DEVCLSREQ();
        UNIDEVCLS[] vtRes;
        uResponse = m_Request.Device.DevClsGet(vrGet, out vtRes);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            for (int i = 0; i < vtRes.Length; i++)
            {
                if (vtRes[i].szMemo != null && vtRes[i].szMemo == "false")
                {
                    closeCls.Add(vtRes[i].dwClassID);
                }
            }
        }
    }
    private void InitItemList()
    {
        DEVREQ req = new DEVREQ();
        req.szReqExtInfo.szOrderKey = "dwCampusID";
        req.szReqExtInfo.szOrderMode = "ASC";
        UNIDEVICE[] rlt;
        if (m_Request.Device.Get(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            List<string> campus =new List<string>();
            Dictionary<uint?, Dictionary<uint, Dictionary<uint?, string>>> dict = new Dictionary<uint?, Dictionary<uint, Dictionary<uint?, string>>>();
            uint rm = (uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS;
            uint seat = (uint)UNIDEVCLS.DWKIND.CLSKIND_SEAT;
            uint cpt = (uint)UNIDEVCLS.DWKIND.CLSKIND_COMPUTER;
            uint loan = (uint)UNIDEVCLS.DWKIND.CLSKIND_LOAN;
            uint rscMode = ToUInt(GetConfig("resourceMode"));
            uint clsKind = ToUInt(GetConfig("openClsKind"));
            uint rm_rsc = ToUInt(GetConfig("subRmResourceMode"));
            uint seat_rsc = ToUInt(GetConfig("subSeatResourceMode"));
            uint cpt_rsc = ToUInt(GetConfig("subCptResourceMode"));
            uint loan_rsc = ToUInt(GetConfig("subLoanResourceMode"));

            for (int i = 0; i < rlt.Length; i++)
            {
                UNIDEVICE dev = rlt[i];
                Dictionary<uint, Dictionary<uint?, string>> list;
                if (dict.ContainsKey(dev.dwCampusID))
                {
                    list = dict[dev.dwCampusID];
                }
                else
                {
                    list = new Dictionary<uint, Dictionary<uint?, string>>();
                    dict[dev.dwCampusID] = list;
                    campus.Add("<h5>"+dev.szCampusName+"</h5>");
                    if (clsKind == 0)
                    {
                        list[0] = new Dictionary<uint?, string>();
                    }
                    else
                    {
                        list[rm] = new Dictionary<uint?, string>();
                        list[seat] = new Dictionary<uint?, string>();
                        list[cpt] = new Dictionary<uint?, string>();
                        list[loan] = new Dictionary<uint?, string>();
                    }
                }
                if (clsKind == 0)//全部
                {
                    GetSubItem(list[0], dev, rscMode);
                }
                else
                {
                    if ((clsKind & rm) > 0&&(dev.dwClassKind & rm) > 0)//空间
                    {
                            GetSubItem(list[rm], dev, rm_rsc > 0 ? rm_rsc : rscMode);
                            continue;
                    }
                    if ((clsKind & seat) > 0&&(dev.dwClassKind & seat) > 0)//座位
                    {
                            GetSubItem(list[seat], dev, seat_rsc > 0 ? seat_rsc : rscMode);
                            continue;
                    }
                    if ((clsKind & cpt) > 0&&(dev.dwClassKind & cpt) > 0)//电子阅览室
                    {
                            GetSubItem(list[cpt], dev, cpt_rsc > 0 ? cpt_rsc : rscMode);
                            continue;
                    }
                    if ((clsKind & loan) > 0&&(dev.dwClassKind & loan) > 0)//外借
                    {
                            GetSubItem(list[cpt], dev, loan_rsc > 0 ? loan_rsc : rscMode);
                            continue;
                    }
                }
            }
            int index=0;
            foreach (Dictionary<uint, Dictionary<uint?, string>> item in dict.Values)
            {
                itemList += campus[index];
                index++;
                foreach (Dictionary<uint?, string> sub in item.Values)
                {
                    itemList += "<li class='nav_cls_li'><ul class='it_list nav'>";
                    foreach (string li in sub.Values)
                    {
                        itemList += li;
                    }
                    itemList += "</li></ul>";
                }
            }
        }
    }
    private UNICAMPUS[] GetCampus()
    {
        CAMPUSREQ req = new CAMPUSREQ();
        UNICAMPUS[] rlt;
        m_Request.Account.CampusGet(req, out rlt);
        return rlt;
    }

    private Dictionary<uint?, string> GetSubItem(Dictionary<uint?, string> dict, UNIDEVICE dev, uint mode)
    {
        //设备
        if (mode == 16)
        {
            if (!dict.ContainsKey(dev.dwDevID))
                dict[dev.dwDevID] = GetDev(dev);
        }
        //房间 / 实验室 + 房间
        else if ((mode & 4) > 0)
        {
            if (!dict.ContainsKey(dev.dwRoomID))
                dict[dev.dwRoomID] = GetRoom(dev, mode);
        }
        else if ((mode & 2) > 0)
        {
            if (!dict.ContainsKey(dev.dwKindID))
                dict[dev.dwKindID] = GetKinds(dev);
        }
        //设备类别 类别
        else if (!dict.ContainsKey(dev.dwClassID))
            dict[dev.dwClassID] = GetDevCls(dev);
        return dict;
    }

    private string GetKinds(UNIDEVICE dev)
    {

        return "<li class='it' it='devcls' url=\"../a/dftdetail.aspx?mode=2&classKind=" + dev.dwClassKind + "&id=" + dev.dwKindID +
            "&name=" + Server.UrlEncode(dev.szKindName) + "\"><a><span>" + dev.szKindName + "</span></a></li>";
    }
    private string GetDev(UNIDEVICE dev)
    {
        if (IsStat(dev.dwProperty, (uint)UNIDEVICE.DWPROPERTY.DEVPROP_NORESV))
        {
            return "";
        }
        {
            return "<li class='it' it='dev' devId='" + dev.dwDevID + "' url=\"../a/devdetail.aspx?back=false&classKind=" + dev.dwClassKind + "&dev=" + dev.dwDevID + "&sn=" + dev.dwDevSN + "\"><a><span>" + dev.szDevName + "</span></a></li>";
        }
    }
    private string GetRoom(UNIDEVICE dev, uint? campusId)
    {
        return "<li class='it' it='lab_" + dev.dwLabID + "' url=\"../a/roomdetail.aspx?classKind=" + dev.dwClassKind + "&roomId=" + dev.dwRoomID + "&roomName=" + Server.UrlEncode(dev.szRoomName) + "\"><a><span>" + dev.szRoomName + "</span></a></li>";

    }
    private string GetDevCls(UNIDEVICE dev)
    {
        for (int i = 0; i < closeCls.Count; i++)
        {
            if (closeCls[i] == dev.dwClassID)
                return "";
        }
        return "<li class='it' it='devcls' url=\"../a/dftdetail.aspx?classKind=" + dev.dwClassKind + "&id=" + dev.dwClassID +
            "&name=" + Server.UrlEncode(dev.szClassName) + "\"><a><span>" + dev.szClassName + "</span></a></li>";

    }
}