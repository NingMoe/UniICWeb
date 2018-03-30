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
    protected string m_szDoorCtrl = "";
    protected string m_szRoomMode = "";
    protected string m_szRooms = "";
    protected string m_szOpenRule = "";
	protected void Page_Load(object sender, EventArgs e)
	{
        UNICHANNELGATE newGate;
        string szOp = "新建";
        if (Request["op"] == "set")
        {
            szOp = "修改";
        }
        if (IsPostBack)
        {
            GetHTTPObj(out newGate);
            string szManMode = Request["dwManMode"];
            newGate.dwManMode = CharListToUint(szManMode);
            UNIGROUP newGroup = new UNIGROUP();
            if (!NewGroup(newGate.szChannelGateName + "管理员组", (uint)UNIGROUP.DWKIND.GROUPKIND_MAN, out newGroup))
            {
                MessageBox(m_Request.szErrMessage, szOp + "通道门", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                DelGroup(newGroup.dwGroupID);
                return;
            }
            newGate.dwManGroupID = newGroup.dwGroupID;
            UNIGROUP newUseGroup = new UNIGROUP();
            if (!NewGroup(newGate.szChannelGateName + "使用组", (uint)UNIGROUP.DWKIND.GROUPKIND_DEV, out newUseGroup))
            {
                MessageBox(m_Request.szErrMessage, szOp + "通道门", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                DelGroup(newUseGroup.dwGroupID);
                return;
            }
            newGate.dwUseGroupID = newUseGroup.dwGroupID;
            newGate.szRelatedRooms = GetRoomNoCtrlList(Request["szRelatedRooms"]);

            if (m_Request.Device.ChannelGateSet(newGate, out newGate) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "通道门失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                return;
            }
            else
            {
                MessageBox(szOp + "道门成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                if (Session["LoginResult"] != null)
                {
                    ADMINLOGINRES admin = (ADMINLOGINRES)Session["LoginResult"];
                    AddGroupMember(newGroup.dwGroupID, admin.AdminInfo.dwAccNo, (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL);
                }
                return;
            }
        }    
        uint uRoomMode = (uint)(UNIROOM.DWMANMODE.ROOMMAN_CAMERA | UNIROOM.DWMANMODE.ROOMMAN_DOORLOCK | UNIROOM.DWMANMODE.ROOMMAN_FREEINOUT);
        m_szRoomMode = GetInputHtml(uRoomMode, CONSTHTML.checkBox, "dwManMode", "Room_dwManMode");

        UNIROOM[] vtRoom = GetAllRoom();
        for (int i = 0; vtRoom!=null&&i < vtRoom.Length; i++)
        {
            m_szRooms += GetInputItemHtml(CONSTHTML.checkBox, "szRelatedRooms", vtRoom[i].szRoomName, vtRoom[i].szRoomNo);
        }

         CHANNELGATEREQ vrParameter = new CHANNELGATEREQ();
        vrParameter.dwGetType = (uint)CHANNELGATEREQ.DWGETTYPE.CHANNELGATEGET_BYALL;
        UNICHANNELGATE[] vrResult;
        if (m_Request.Device.ChannelGateGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS && vrResult!=null)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szRooms += GetInputItemHtml(CONSTHTML.checkBox, "szRelatedRooms", vrResult[i].szChannelGateName, vrResult[i].szChannelGateNo);
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
            CHANNELGATEREQ vrGet = new CHANNELGATEREQ();
            vrGet.dwGetType = (uint)CHANNELGATEREQ.DWGETTYPE.CHANNELGATEGET_BYID;
            vrGet.szGetKey = Request["id"];
            UNICHANNELGATE[] vtRes;
            if (m_Request.Device.ChannelGateGet(vrGet, out vtRes) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else
            {
                if (vtRes.Length == 0)
                {
                    MessageBox("获取失败", "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                }
                else
                {
                    PutJSObj(vtRes[0]);
                    m_Title = "修改【" + vtRes[0].szChannelGateName + "】";
                }
            }
        }
        else
        {
            UNISTATION station = new UNISTATION();
            PutJSObj(station);
            m_Title = szOp+"通道门";

        }                      
	}
}
