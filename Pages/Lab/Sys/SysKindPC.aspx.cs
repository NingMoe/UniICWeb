﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using UniWebLib;

public partial class Sub_Room : UniPage
{
    protected string m_szOpts = "";
    protected MyString m_szOut = new MyString();

    protected string m_szLab = "";
    protected string m_szRoom = "";
    protected string m_szDevKind = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string szlab = Request["lab"];
        string szroom = Request["room"];
        string szKind = Request["kind"];
        //=========================
        UNILAB[] lab = GetAllLab();
        if (lab != null && lab.Length > 0)
        {
            m_szLab += "<option value='0'>选择" + ConfigConst.GCLabName + "</option>";         
            for (int i = 0; i < lab.Length; i++)
            {
                m_szLab += "<option value='" + lab[i].dwLabID + "'";
                if (szlab == lab[i].dwLabID.ToString())
                {
                    m_szLab += "checked='checked'";
                }
                m_szLab += ">" + lab[i].szLabName + "</option>";
            }
        }
        else
        {
            m_szLab += "<option value='0'>选择" + ConfigConst.GCLabName + "</option>";
        }
        ROOMREQ reqRoom = new ROOMREQ();
        if (szlab != null && szlab != "0")
        {
            reqRoom.dwLabID = Parse(szlab);
        }
       // reqRoom.dwInClassKind = (uint)UNIDEVCLS.DWKIND.CLSKIND_COMPUTER;
        UNIROOM[] room;
        m_Request.Device.RoomGet(reqRoom, out room);
        if (room != null && room.Length > 0)
        {
            m_szRoom += "<option value='0'>选择区域</option>";
            for (int i = 0; i < room.Length; i++)
            {
                m_szRoom += "<option value='" + room[i].dwRoomID + "'";
                if (szroom == room[i].dwRoomID.ToString())
                {
                    m_szRoom += "checked='checked'";
                }
                m_szRoom += ">" + room[i].szRoomName + "</option>";
            }
        }
        else { m_szRoom += "<option value='0'>选择区域</option>"; }
        //=========================
        UNIDEVKIND[] devKind=GetAllDevKind();
        if (devKind != null && devKind.Length > 0)
        {
            m_szDevKind += "<option value='0'>选择" + ConfigConst.GCKindName + "</option>";
            for (int i = 0; i < devKind.Length; i++)
            {
                m_szDevKind += "<option value='" + devKind[i].dwKindID + "'";                
                m_szDevKind += ">" + devKind[i].szKindName + "</option>";
            }
        }
        else
        {
            m_szDevKind += "<option value='0'>选择" + ConfigConst.GCKindName+ "</option>";
        }

        DEVREQ vrParameter = new DEVREQ();
        if (szroom != null && szroom != "" && szroom != "0")
        {
            vrParameter.szRoomIDs = (szroom);
        }
        if (szlab != null && szlab != "" && szlab != "0")
        {
            vrParameter.szLabIDs = (szlab);
        }
        if (szKind != null && szKind != "" && szKind != "0")
        {
            vrParameter.szKindIDs = (szKind);
        }
        
        if (Request["delID"] != null)
        {
            Del(Request["delID"], Request["delParentID"]);
        }
        //vrParameter.dwClassKind = (uint)UNIDEVCLS.DWKIND.CLSKIND_COMPUTER;
        UNIDEVICE[] vrResult;
        //vrParameter.dwProperty = (uint)(UNIDEVKIND.DWPROPERTY.DEVPROP_EXCLUSIVE | UNIDEVKIND.DWPROPERTY.DEVPROP_SHARE);
        GetPageCtrlValue(out vrParameter.szReqExtInfo);

        if (m_Request.Device.Get(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id='" + vrResult[i].dwDevID.ToString() + "' data-labid='" + vrResult[i].dwLabID.ToString() + "'>" + vrResult[i].dwDevSN.ToString() + "</td>";
                m_szOut += "<td data-id='" + vrResult[i].dwDevID.ToString() + "' data-labid='" + vrResult[i].dwLabID.ToString() + "'>" + vrResult[i].szPCName + "</td>";
                m_szOut += "<td>" + vrResult[i].szDevName.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szIP.ToString() + "</td>";
                m_szOut += "<td >" + vrResult[i].szKindName + "</td>";
                m_szOut += "<td >" + vrResult[i].szModel + "</td>";
                m_szOut += "<td >" + vrResult[i].szSpecification + "</td>";                
                m_szOut += "<td class='lnkRoom' data-id='" + vrResult[i].dwRoomID + "'>" + vrResult[i].szRoomName + "</td>";
                m_szOut += "<td class='lnkLab' data-id='" + vrResult[i].dwLabID + "'>" + vrResult[i].szLabName + "</td>";
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Device);
        }
        PutBackValue();
    }
    private void Del(string szID, string szLabID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        UNIDEVICE obj = new UNIDEVICE();
        obj.dwDevID = Parse(szID);
        obj.dwLabID = Parse(szLabID);
        uResponse = m_Request.Device.Del(obj);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
            MessageBox(m_Request.szErrMessage, "提示", MSGBOX.ERROR);
        }
    }
}
