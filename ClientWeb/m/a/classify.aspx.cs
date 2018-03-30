using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_m_all_classify : UniClientPage
{
    protected string itemList;
    protected string title;
    protected void Page_Load(object sender, EventArgs e)
    {
        title = Server.UrlDecode(Request["title"]);
        InitClassify();
    }

    private void InitClassify()
    {
        uint clsKind = ToUInt(Request["classkind"]);
        uint mode = ToUInt(Request["mode"]);
        uint rscMode = ToUInt(GetConfig("resourceMode"));
        if (mode == 0) mode = rscMode;
        if ((mode & 4) > 0)
        {
            itemList = GetRooms(clsKind);
        }
        else if((mode & 2)>0){
            itemList = GetKinds(clsKind);
        }
        else if((mode&16)>0){
            itemList = GetDevs(clsKind);
        }
        else
        {
            itemList = GetDevCls(clsKind);
        }
    }

    private string GetDevs(uint clsKind)
    {
        DEVREQ req = new DEVREQ();
        if (clsKind != 0)
            req.dwClassKind = clsKind;
        UNIDEVICE[] rlt;
        string ret = "";
        if (m_Request.Device.Get(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                ret += "<li><a class='item-content item-link' href=\"../a/detail.aspx?right=resv&classkind=" + rlt[i].dwClassKind + "&dev_id=" + rlt[i].dwDevID +
                    "&name=" + Server.UrlEncode(rlt[i].szDevName) + "\"><div class='item-inner'><div class='item-title'>" + rlt[i].szDevName + "</div><div class='item-after'>详细</div></div></a></li>";
            }
        }
        return ret;
    }

    private string GetKinds(uint clsKind)
    {
        DEVKINDREQ req = new DEVKINDREQ();
        if(clsKind!=0)
        req.dwClassKind = clsKind;
        UNIDEVKIND[] rlt;
        string ret = "";
        req.szReqExtInfo.szOrderKey = "dwNationCode";
        req.szReqExtInfo.szOrderMode = "asc";


        if (m_Request.Device.DevKindGet(req,out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                string szExtKind = "";
                if ((rlt[i].dwProperty & (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_LONGTERMRESV) > 0)
                {
                    //szExtKind = "(请在电脑端预约)";
                }
                ret += "<li><a class='item-content item-link' href=\"../a/detail.aspx?right=resv&classkind=" + rlt[i].dwClassKind + "&kind_id=" + rlt[i].dwKindID +
                    "&name=" + Server.UrlEncode(rlt[i].szKindName) + "\"><div class='item-inner'><div class='item-title'>" + rlt[i].szKindName + szExtKind + "</div><div class='item-after'>详细</div></div></a></li>";
            }
        }
        return ret;
    }

    private string GetRooms(uint kind)
    {
        ROOMREQ req = new ROOMREQ();
        if (kind != 0)
            req.dwInClassKind = kind;
        //    req.szReqExtInfo.szOrderKey = "szRoomName";
        //    req.szReqExtInfo.szOrderMode = "ASC";
        UNIROOM[] rlt;
        string ret = "";
        if (m_Request.Device.RoomGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                UNIROOM rm = rlt[i];
                ret += "<li><a class='item-content item-link' href=\"../a/detail.aspx?right=resv&classkind=" + kind + "&room_id=" + rm.dwRoomID +
                    "&name=" + Server.UrlEncode(rm.szRoomName) + "\"><div class='item-inner'><div class='item-title'>" + rm.szRoomName + "</div><div class='item-after'>详细</div></div></a></li>";
            }
        }
        return ret;
    }
    private string GetDevCls(uint kind)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        DEVCLSREQ vrGet = new DEVCLSREQ();
        if (kind != 0)
            vrGet.dwKind = kind;
        UNIDEVCLS[] vtRes;
        string ret = "";
        uResponse = m_Request.Device.DevClsGet(vrGet, out vtRes);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes.Length > 0)
        {
            for (int i = 0; i < vtRes.Length; i++)
            {
                if (vtRes[i].szMemo != null && vtRes[i].szMemo == "false")
                {
                    continue;
                }
                ret += "<li><a class='item-content item-link' href=\"../a/detail.aspx?right=resv&classkind=" + vtRes[i].dwKind + "&class_id=" + vtRes[i].dwClassID +
                    "&name=" + Server.UrlEncode(vtRes[i].szClassName) + "\"><div class='item-inner'><div class='item-title'>" + vtRes[i].szClassName + "</div><div class='item-after'>详细</div></div></a></li>";
            }
        }
        return ret;
    }
}