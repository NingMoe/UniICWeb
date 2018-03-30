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
    protected string szDevName = "";
    protected uint uClassKind = 0;
    protected string m_szRooms = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string szlab = Request["lab"];
        //=========================
        uClassKind = Parse(Request["classkind"]);
        szDevName = GetJustNameEqual(uClassKind, "DevClass_dwKind", false);
     
        UNIDEVICE newDev;      
        if (IsPostBack)
        {
            GetHTTPObj(out newDev);

            UNIDEVKIND[] vtDevKind=GetDevKindByKind((uint)UNIDEVCLS.DWKIND.CLSCOMMONS_CONSULTING+(uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS);
            if (vtDevKind != null && vtDevKind.Length > 0)
            {
                newDev.dwKindID = vtDevKind[0].dwKindID;
            }
            else
            {
                MessageBox("不存在默认类型", "新建" + szDevName + "失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                return; 
            }

            newDev.dwDevSN = GetDevSN();
            newDev.dwProperty = CharListToUint(Request["dwProperty"]);
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
            newRoom.dwLabID = newDev.dwLabID;
            newRoom.szRoomNo = newDev.szRoomNo;
            newRoom.szRoomName = newDev.szDevName;
            newRoom.dwOpenRuleSN = newDev.dwOpenRuleSN;
            newRoom.dwManMode = newDev.dwManMode;

            newRoom.szSubRooms = GetRoomNoCtrlList( Request["szSubRooms"]);
            newRoom.dwInClassKind = uClassKind;

            if (m_Request.Device.RoomSet(newRoom, out newRoom) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "新建" + szDevName + "失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                return;
            }
            else
            {
                newDev.dwRoomID = newRoom.dwRoomID;
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
            if(m_Request.Device.Set(newDev, out newDev) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "新建" + szDevName + "失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                return;
            }
            else
            {
                MessageBox("新建" + szDevName + "成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);                            
            }
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
        UNILAB[] vtLab=GetAllLab();
        if (vtLab != null && vtLab.Length > 0)
        {
            for (int i = 0; i < vtLab.Length; i++)
            {
                m_szLab += "<option value='" + vtLab[i].dwLabID + "'>" + vtLab[i].szLabName + "</option>";
            }
        }
        UNIROOM[] vtRoom = GetAllRoom();
        for (int i = 0; vtRoom != null && i < vtRoom.Length; i++)
        {
            m_szRooms += GetInputItemHtml(CONSTHTML.checkBox, "szSubRooms", vtRoom[i].szRoomName, vtRoom[i].szRoomNo);
        }
        UNICHANNELGATE[] vtChannel = GetAllChannelGate();
        for (int i = 0; vtChannel != null && i < vtChannel.Length; i++)
        {
            //m_szRooms += GetInputItemHtml(CONSTHTML.checkBox, "szSubRooms", vtChannel[i].szChannelGateName, vtChannel[i].szChannelGateNo);
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
                    m_Title = "修改【" + vtDev[0].szDevName + "】";
                }
            }
        }
        else
        {
            m_Title = "新建" + szDevName;

        }                      
	}  
}
