using System;
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
    protected string m_szCtrlMode = "";
    protected string m_szLab = "";
    protected string m_szManager = "";
    protected string m_szRoom = "";
    protected string m_szDevKind = "";
    protected string m_szPorperty = "";
    protected string m_szOpenRule = "";
    protected string m_szRoomMode = "";
    protected string szOption = "";
    protected string szSubRoom = "";
    protected string szCamp = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        for (int i = 1; i < 100; i++)
        {
            szOption += GetInputItemHtml(CONSTHTML.option, "", i.ToString(), i.ToString());
        }
        UNIROOM[] allRoom = GetRoomByClassKind((uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS);
        for (int i = 0; i < allRoom.Length; i++)
        {
            if (!((((uint)allRoom[i].dwProperty) & (uint)UNIROOM.DWPROPERTY.ROOMPROP_COMBINE) > 0))
            {
                szSubRoom += GetInputItemHtml(CONSTHTML.checkBox, "subRoom", allRoom[i].szRoomName, allRoom[i].szRoomNo.ToString());
            }
        }
        UNICAMPUS[] vtCamp = GetAllCampus();
        for (int i = 0; i < vtCamp.Length; i++)
        {
            szCamp += GetInputItemHtml(CONSTHTML.option, "", vtCamp[i].szCampusName, vtCamp[i].dwCampusID.ToString());
        }
        UNIDEVICE newDev;
        uint? uMax=0;        
        uint uID= PRDevice.DEVICE_BASE|PRDevice.MSREQ_DEVICE_SET;
        if (GetMaxValue(ref uMax, uID, "dwDevSN"))
        {

        }
        if (IsPostBack)
        {
            GetHTTPObj(out newDev);
            
          //  newDev.dwProperty = CharListToUint(Request["dwProperty"]);
            uint uProperty = CharListToUint(Request["dwProperty2"]);
            if ((uProperty & (uint)UNIDEVICE.DWPROPERTY.DEVPROP_MAIN) > 0)
            {
                if (newDev.dwProperty == null)
                {
                    newDev.dwProperty = 0;
                }
                newDev.dwProperty = (uint)newDev.dwProperty | (uint)UNIDEVICE.DWPROPERTY.DEVPROP_MAIN;
            }
            string szOpen = Request["chkopen"];
            if (newDev.dwProperty == null)
            {
                newDev.dwProperty = 0;
            }
            if (szOpen != null && szOpen == "1")
            {
                newDev.dwProperty = (uint)newDev.dwProperty | (uint)UNIDEVICE.DWPROPERTY.DEVPROP_NORESV;
            }
            else
            {
                if ((((uint)newDev.dwProperty) & (uint)UNIDEVICE.DWPROPERTY.DEVPROP_NORESV) > 0)
                {
                    newDev.dwProperty = (uint)newDev.dwProperty - (uint)UNIDEVICE.DWPROPERTY.DEVPROP_NORESV;
                }
            }
            newDev.dwManMode = CharListToUint(Request["dwManMode"]);   
            UNIGROUP newUseGroup;
            if (NewGroup(newDev.szDevName.ToString() + "使用组", (uint)UNIGROUP.DWKIND.GROUPKIND_DEV, out newUseGroup))
            {
                newDev.dwUseGroupID = newUseGroup.dwGroupID;
            }
            else
            {
                return;
            }
            UNIGROUP newManGroup;
            UNIROOM newRoom = new UNIROOM();
            if (NewGroup(newDev.szDevName.ToString() + "管理组", (uint)UNIGROUP.DWKIND.GROUPKIND_MAN, out newManGroup))
            {
                newRoom.dwManGroupID = newManGroup.dwGroupID;
            }
            else
            {
                return;
            }
            if (!string.IsNullOrEmpty(newDev.szDevURL))
            {
                newRoom.szMemo = newDev.szDevURL;
            }
            newRoom.dwLabID = newDev.dwLabID;
            newRoom.szRoomNo = newDev.szRoomNo;
            newRoom.szRoomName = newDev.szDevName;
            newRoom.dwOpenRuleSN = newDev.dwOpenRuleSN;
            newRoom.dwCampusID = Parse(Request["dwCampusID"]);
            string szRoom=Request["subRoom"];
            if (szRoom != null && szRoom != "")
            {
                newRoom.szSubRooms = szRoom;
                newRoom.dwProperty = (uint)UNIROOM.DWPROPERTY.ROOMPROP_COMBINE;
            }
            else {
                newRoom.dwProperty = (uint)UNIROOM.DWPROPERTY.ROOMPROP_SUBROOM;
            }
            newRoom.dwManMode = newDev.dwManMode;
            newRoom.dwInClassKind = (uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS;

            for (int i = 0; i < allRoom.Length; i++)
            {
                string szRoomNameTemp = newRoom.szRoomName;
                string szRoomNoTemp = newRoom.szRoomNo.ToString();
                if (allRoom[i].szRoomName == szRoomNameTemp)
                {
                    MessageBox("房间名称已存在", "新建" + ConfigConst.GCSysKindRoom + "失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                    return;
                }
                if (allRoom[i].szRoomNo == szRoomNoTemp)
                {
                    MessageBox("编号已存在", "新建" + ConfigConst.GCSysKindRoom + "失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                    return;
                }
            }

            if (m_Request.Device.RoomSet(newRoom, out newRoom) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "新建" + ConfigConst.GCSysKindRoom + "失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                return;
            }
            else
            {
                newDev.dwRoomID = newRoom.dwRoomID;
            }
            
            uint uDevNum = Parse(Request["devNum"]);
            for (int i = 0; i < uDevNum; i++)
            {
                UNIDEVICE devTemp = new UNIDEVICE();
                devTemp = newDev;
                devTemp.dwDevSN = GetDevSN();
                if (uDevNum > 1)
                {
                    devTemp.szDevName = devTemp.szDevName + (i+1).ToString();
                }
                
                if (m_Request.Device.Set(devTemp, out devTemp) != REQUESTCODE.EXECUTE_SUCCESS)
                {
                    MessageBox(m_Request.szErrMessage, "新建" + ConfigConst.GCSysKindRoom + "失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                    return;
                }
                else
                {
                    MessageBox("新建" + ConfigConst.GCSysKindRoom + "成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                    UNIACCOUNT accInfo;
                    if (newDev.dwAttendantID != null && GetAccByAccno(newDev.dwAttendantID.ToString(), out accInfo) == true)
                    {
                        DEVATTENDANT devAttendSet = new DEVATTENDANT();
                        devAttendSet.dwAttendantID = accInfo.dwAccNo;
                        devAttendSet.szAttendantName = accInfo.szTrueName;
                        devAttendSet.szAttendantTel = Request["szAttendantTel"];
                        devAttendSet.dwDevID = devTemp.dwDevID;
                        devAttendSet.dwLabID = devTemp.dwLabID;
                        m_Request.Device.AttendantSet(devAttendSet);
                       
                    }

                }
            }
            return;
        }
        uint uRoomMode = (uint)(UNIROOM.DWMANMODE.ROOMMAN_CAMERA | UNIROOM.DWMANMODE.ROOMMAN_DOORLOCK | UNIROOM.DWMANMODE.ROOMMAN_FREEINOUT);
        m_szRoomMode = GetInputHtml(uRoomMode, CONSTHTML.checkBox, "dwManMode", "Room_dwManMode");
        DEVOPENRULE[] vtOpenRule = GetAllOpenRule();
        if (vtOpenRule != null && vtOpenRule.Length > 0)
        {
            for (int i = 0; i < vtOpenRule.Length; i++)
            {
                m_szOpenRule += "<option value='" + vtOpenRule[i].dwRuleSN + "'>" + vtOpenRule[i].szRuleName + "</option>";
            }
        }
        m_szCtrlMode = GetAllInputHtml(CONSTHTML.option, "", "UNIDEVICE_CtrlMode");
        UNIADMIN[] adminlist;
        if (GetAdmin(out adminlist) == true)
        {
            for (int i = 0; i < adminlist.Length; i++)
            {
                m_szManager += "<option value='" + adminlist[i].dwAccNo.ToString() + "'>" + adminlist[i].szTrueName + "</option>";
            }
        }
        UNILAB[] vtLab = GetLabByClass((uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS);
        if (vtLab != null && vtLab.Length > 0)
        {
            for (int i = 0; i < vtLab.Length; i++)
            {
                m_szLab += "<option value='" + vtLab[i].dwLabID + "'>" + vtLab[i].szLabName + "</option>";
            }
        }
        m_szPorperty = GetInputHtmlFromXml(0, CONSTHTML.checkBox, "dwProperty", "UNIDEVICE_Property", true);
        if (Request["op"] == "set")
        {
            bSet = true;

            DEVREQ vrDevReq = new DEVREQ();
            vrDevReq.dwDevID = Parse(Request["id"]);
            UNIDEVICE[] vtDev;
            if (m_Request.Device.Get(vrDevReq, out vtDev) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else
            {
                if (vtDev.Length == 0)
                {
                    MessageBox("获取失败", "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                }
                else
                {
                    PutJSObj(vtDev[0]);
                    m_Title = "修改【" + vtDev[0].szPCName + "】";
                }
            }
        }
        else
        {
            m_Title = "新建"+ConfigConst.GCSysKindRoom;

        }                      
	}  
}
