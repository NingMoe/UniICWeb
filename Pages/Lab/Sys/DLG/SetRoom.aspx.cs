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

public partial class _Default : UniPage
{
    protected bool bSet = false;
    protected string m_Title = "";
    protected string m_szDoorCtrl = "";
    protected string m_szLab = "";
    protected string m_szOpenRule = "";
    protected string m_szRoomMode = "";
	protected void Page_Load(object sender, EventArgs e)
	{
        UNIROOM newRoom;
        uint? uMax=0;        
        uint uID= PRDevice.DEVICE_BASE|PRDevice.MSREQ_DEVICE_SET;
        if (GetMaxValue(ref uMax, uID, "szRoomNo"))
        {

        }
        if (IsPostBack)
        {
            GetHTTPObj(out newRoom);
            string szManMode = Request["dwManMode"];
            newRoom.dwManMode = CharListToUint(szManMode);
            if (newRoom.dwManGroupID == null || newRoom.dwManGroupID.ToString() == "0")
            {
                UNIGROUP newGroup = new UNIGROUP();
                if (!NewGroup(newRoom.szRoomName + "管理员组", (uint)UNIGROUP.DWKIND.GROUPKIND_MAN, out newGroup))
                {
                    MessageBox(m_Request.szErrMessage, "修改"+ConfigConst.GCRoomName+"失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                    DelGroup(newGroup.dwGroupID);
                    return;
                }
                newRoom.dwManGroupID = newGroup.dwGroupID;
            }
            UNILAB setLab = new UNILAB();
            setLab.dwLabID = newRoom.dwLabID;
            setLab.dwManGroupID = newRoom.dwManGroupID;
            setLab.dwDeptID = newRoom.dwDeptID;
            setLab.szDeptName = newRoom.szDeptName;
            setLab.szLabName = newRoom.szRoomName;
            setLab.szLabSN = newRoom.szRoomNo;
            //setLab.dwCampusID = newRoom.dwCampusID;
            if (m_Request.Device.LabSet(setLab, out setLab) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "修改" + ConfigConst.GCRoomName + "失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                if (m_Request.Device.RoomSet(newRoom, out newRoom) != REQUESTCODE.EXECUTE_SUCCESS)
                {
                    MessageBox(m_Request.szErrMessage, "修改" + ConfigConst.GCRoomName + "失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                }
                else
                {
                    MessageBox("修改" + ConfigConst.GCRoomName + "成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                    return;
                }
            }
        }
        DOORCTRLREQ vrParameter = new DOORCTRLREQ();
        uint uRoomMode = (uint)(UNIROOM.DWMANMODE.ROOMMAN_CAMERA | UNIROOM.DWMANMODE.ROOMMAN_DOORLOCK | UNIROOM.DWMANMODE.ROOMMAN_FREEINOUT);
        m_szRoomMode = GetInputHtml(uRoomMode, CONSTHTML.checkBox, "dwManMode", "Room_dwManMode");
        vrParameter.dwDCSKind = (uint)UNIDOORCTRL.DWCTRLKIND.DCKIND_ROOM;
        vrParameter.dwGetType = (uint)DOORCTRLREQ.DWGETTYPE.DOORCTRLGET_BYALL;           
       
        UNILAB[] vtLab=GetAllLab();
        if (vtLab!=null&&vtLab.Length > 0)
        {
            for (int i = 0; i < vtLab.Length; i++)
            {
                m_szLab += "<option value='" + vtLab[i].dwLabID + "'>" + vtLab[i].szLabName + "</option>";
            }
        }
        DEVOPENRULE[] vtOpenRule=GetAllOpenRule();    
        if (vtOpenRule!=null&&vtOpenRule.Length > 0)
        {
            for (int i = 0; i < vtOpenRule.Length; i++)
            {
                m_szOpenRule+= "<option value='" + vtOpenRule[i].dwRuleSN + "'>" + vtOpenRule[i].szRuleName + "</option>";
            }
        }

        if (Request["op"] == "set")
        {
            bSet = true;

            ROOMREQ vrRoomReq = new ROOMREQ();
            vrRoomReq.dwRoomID= ToUint(Request["roomid"]);
            UNIROOM[] vtRoom;
            if (m_Request.Device.RoomGet(vrRoomReq, out vtRoom) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else
            {
                if (vtRoom.Length == 0)
                {
                    MessageBox("获取失败", "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                }
                else
                {
                    PutJSObj(vtRoom[0]);
                    m_Title = "修改【" + vtRoom[0].szRoomName + "】";
                }
            }
        }
        else
        {
            UNISTATION station = new UNISTATION();
            station.dwStaSN = uMax;
            PutJSObj(station);
            m_Title = "新建" +ConfigConst.GCRoomName;

        }                      
	}
}
