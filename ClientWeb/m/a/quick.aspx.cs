using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_m_all_view : UniClientPage
{
    protected string title;
    protected string qkRange = "";
    protected string qkInterval = "";
    protected string qkKind = "";
    protected string qkFilter = "";
    protected string hideKind = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["title"]))
        {
            title = Server.UrlDecode(Request["title"]);
        }
        InitRange();
        InitInterval();
        InitKinds();
        //InitFilter();
    }

    private void InitFilter()
    {
        string classkind = Request["classkind"];
        ROOMREQ req = new ROOMREQ();
        req.dwInClassKind = ToUInt(classkind);
        UNIROOM[] rlt;
        if (m_Request.Device.RoomGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            qkFilter += "<li><a href='#' class='item-link smart-select' data-back-on-select='true'  data-page-title='"+Translate("所处区域")+"' data-back-text='"+Translate("确认")+
                "'><select name='room_id'><option class='uni_trans' value='' selected>不限制</option>";
            for (int i = 0; i < rlt.Length; i++)
            {
                qkFilter += "<option class='uni_trans' value='" + rlt[i].dwRoomID + "'>" + rlt[i].szRoomName + "</option>";
            }
            qkFilter += "</select><div class='item-content'><div class='item-inner'><div class='item-title'>所处区域</div><div class='item-after'>不限制</div></div></div></a></li>";
        }
    }

    private void InitKinds()
    {
        string classkind = Request["classkind"];
        DEVKINDREQ req = new DEVKINDREQ();
        req.dwClassKind = ToUInt(classkind);
        UNIDEVKIND[] rlt;
        if (m_Request.Device.DevKindGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            if (rlt.Length == 1) hideKind = "hidden";
            for (int i = 0; i < rlt.Length; i++)
            {
                UNIDEVKIND kind = rlt[i];
                qkKind += "<li><label class='label-radio item-content'><input type='radio' name='kind_id' value='" + kind.dwKindID + "'" + (i == 0 ? " checked='checked'" : "") + ">"
+ "<div class='item-inner'><div class='item-title'>" + kind.szKindName + "</div></div></label></li>";
            }
        }
    }

    private void InitInterval()
    {
        string interval = GetConfig("resvInterval");
        string[] tvs = interval.Split(',');
        for (int i = 0; i < tvs.Length; i++)
        {
            uint tv = ToUInt(tvs[i]);
            if (tv != 0)
            {
                string str = ((tv / 60) > 0 ? (tv / 60 + Translate("小时")) : "") + ((tv % 60) > 0 ? ((tv % 60) + Translate("分")) : "");
                qkInterval += "<li><label class='label-radio item-content'><input type='radio' name='interval' value='" + tv + "'" + (i == 0 ? " checked='checked'" : "") + ">"
                + "<div class='item-inner'><div class='item-title'>" + str + "</div></div></label></li>";
            }
        }
    }

    private void InitRange()
    {
        string range = GetConfig("resvRange");
        string[] rgs = range.Split(',');
        for (int i = 0; i < rgs.Length; i++)
        {
            string rg = rgs[i];
            string[] tmp = rg.Split('@');
            qkRange += "<li><label class='label-radio item-content'><input type='radio' class='radio_range' name='range' value='" + tmp[1] + "'" + (i == 0 ? " checked='checked'" : "") + ">"
                            + "<div class='item-inner'><div class='item-title'>" + tmp[0] + "<span class='grey'>(" + tmp[1] + ")</span></div></div></label></li>";
        }
    }
}