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
    protected uint uClassKind = 0;
    protected string m_szRooms = "";
    protected string szDevName = "";
	protected void Page_Load(object sender, EventArgs e)
	{
        UNIDEVICE newDev;
        uClassKind = (Parse(Request["classkind"]));
        szDevName = GetJustNameEqual(uClassKind, "DevClass_dwKind", false);
        uClassKind = (Parse(Request["classkind"]) - 1);
        if (IsPostBack)
        {
            GetHTTPObj(out newDev);
           // newDev.dwDevSN = GetDevSN();
            newDev.dwProperty = CharListToUint(Request["dwProperty"]);
            newDev.dwManMode = CharListToUint(Request["dwManMode"]);
            UNIROOM newRoom = new UNIROOM();

            newRoom.dwRoomID= newDev.dwRoomID;
            newRoom.dwLabID = newDev.dwLabID;
            newRoom.szRoomNo = newDev.szRoomNo;
            newRoom.szRoomName = newDev.szDevName;
            newRoom.dwOpenRuleSN = newDev.dwOpenRuleSN;
            newRoom.dwManMode = newDev.dwManMode;
          
            newRoom.szSubRooms = GetRoomNoCtrlList(Request["szSubRooms"]);
            if (m_Request.Device.RoomSet(newRoom, out newRoom) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "修改" + szDevName + "失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                return;
            }
            else
            {
                newDev.dwRoomID = newRoom.dwRoomID;
            }
            if(m_Request.Device.Set(newDev, out newDev) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "修改" + szDevName + "失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                return;
            }
            else
            {
                MessageBox("修改" + szDevName + "成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);                            
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

        UNILAB[] vtLab=GetAllLab();
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
                    UNIROOM setRoom;
                    if (GetRoomID(vtDev[0].dwRoomID.ToString(), out setRoom))
                    {
                        PutMemberValue("szSubRooms", setRoom.szSubRooms.ToString());
                    }
                    m_Title = "修改【" + vtDev[0].szDevName + "】";
                }
            }
        }
        else
        {
            m_Title = "修改" + szDevName;

        }                      
	}  
}
