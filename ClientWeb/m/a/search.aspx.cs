using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_m_all_search : UniClientPage
{
    protected uint classkind;
    protected string title;
    protected string itemList="";
    protected uint prop;
    protected uint purpose;
    protected string availableKinds="";
    protected string closeRoom = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        classkind = ToUInt(Request["classkind"]);
        prop = ToUInt(Request["prop"]);
        purpose = ToUInt(Request["purpose"]);
        title = Request["title"];
        InitFilter();
    }

    private void InitFilter()
    {
        InitCloseRoom();//临时 过滤不开放的房间
        itemList += GetDevKinds();
        itemList += GetLabs();
    }

    private string GetDevKinds()
    {
        DEVKINDREQ req = new DEVKINDREQ();
        req.dwClassKind = classkind;
        if (prop > 0)
            req.dwProperty = prop;
        //if (purpose > 0)
        //    req.dwPurpose = purpose;

        //
        //uint special = (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_LONGTERMRESV | (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_KINDRESV | (uint)UNIDEVICE.DWPROPERTY.DEVPROP_NORESV;
        //20170609zy
        uint special = (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_KINDRESV | (uint)UNIDEVICE.DWPROPERTY.DEVPROP_NORESV;

        UNIDEVKIND[] rlt;
        string ret = "<div class='content-block-title'>类型</div>";
        req.szReqExtInfo.szOrderKey = "dwNationCode";
        req.szReqExtInfo.szOrderMode = "asc";

        if (m_Request.Device.DevKindGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS && rlt.Length > 0)
        {
            ret+="<div class='list-block'><ul>";
            for (int i = 0; i < rlt.Length; i++)
            {
                /*原本修改内容，不现实长期预约
                UNIDEVKIND kind=rlt[i];
                if (prop == 0 && (kind.dwProperty & special) > 0) continue;//过滤长期、按类型预约、不支持预约
                availableKinds += kind.dwKindID + ",";
                ret += "<li><label class='label-checkbox item-content'><input type='checkbox' name='kind_id' class='ck_kind' value='" + kind.dwKindID + "'/><div class='item-media'>"+
          "<i class='icon icon-form-checkbox'></i></div><div class='item-inner'><div class='item-title'>" + kind.szKindName + "</div></div></label></li>";
          */
                //20170609zy
                UNIDEVKIND kind = rlt[i];
                if (prop == 0 && (kind.dwProperty & special) > 0)
                {
                    continue;//按类型预约、不支持预约
                }
                if ((kind.dwProperty & ((uint)UNIDEVKIND.DWPROPERTY.DEVPROP_LONGTERMRESV)) > 0)
                {
                    availableKinds += kind.dwKindID + ",";
                    ret += "<li><label class='label-checkbox item-content'><div class='item-inner'><div class='item-title'>" + kind.szKindName + "(请在电脑端预约)</div></div></label></li>";
                    continue;
                }
                else
                {


                    availableKinds += kind.dwKindID + ",";
                    ret += "<li><label class='label-checkbox item-content'><input type='checkbox' name='kind_id' class='ck_kind' value='" + kind.dwKindID + "'/><div class='item-media'>" +
              "<i class='icon icon-form-checkbox'></i></div><div class='item-inner'><div class='item-title'>" + kind.szKindName + "</div></div></label></li>";

                }
            }
            if (availableKinds.Length > 0) availableKinds=availableKinds.Substring(0,availableKinds.Length - 1);//去逗号
            ret += "</ul></div>";
        }
        return ret;
    }

    private string GetLabs()
    {
        LABREQ req = new LABREQ();
        req.dwLabClass = classkind;
        //if (purpose > 0)
        //    req.dwPurpose = purpose;
        UNILAB[] rlt;
        string ret = "<div class='content-block-title'>位置</div>";
        if (m_Request.Device.LabGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            ret += "<div class='list-block'><ul>";
            for (int i = 0; i < rlt.Length; i++)
            {
                UNILAB lab = rlt[i];
                ret += "<li><label class='label-checkbox item-content'><input type='checkbox' name='lab_id' class='ck_lab' value='" + lab.dwLabID + "'/><div class='item-media'>" +
"<i class='icon icon-form-checkbox'></i></div><div class='item-inner'><div class='item-title'>" + lab.szLabName + "</div></div></label></li>";
            }
            ret += "</ul></div>";
        }
        return ret;
    }

    private string GetRooms()
    {
        ROOMREQ req = new ROOMREQ();
        req.dwInClassKind = classkind;
        //if (purpose > 0)
        //    req.dwPurpose = purpose;
        UNIROOM[] rlt;
        string ret = "<div class='content-block-title'>位置</div>";
        if (m_Request.Device.RoomGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            ret += "<div class='list-block'><ul>";
            for (int i = 0; i < rlt.Length; i++)
            {
                UNIROOM rm = rlt[i];
                ret += "<li><label class='label-checkbox item-content'><input type='checkbox' name='room_id' class='ck_room' value='" + rm.dwRoomID + "'/><div class='item-media'>" +
"<i class='icon icon-form-checkbox'></i></div><div class='item-inner'><div class='item-title'>" + rm.szRoomName + "</div></div></label></li>";
            }
            ret += "</ul></div>";
        }
        return ret;
    }
    void InitCloseRoom()
    {
        ROOMREQ req = new ROOMREQ();
        req.dwInClassKind = classkind;
        UNIROOM[] rlt;
        if (m_Request.Device.RoomGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                if((rlt[i].dwProperty & 0x800000) > 0)//临时  0x800000=不开放
                {
                    closeRoom += rlt[i].dwRoomID+",";
                }
            }
        }
    }
}