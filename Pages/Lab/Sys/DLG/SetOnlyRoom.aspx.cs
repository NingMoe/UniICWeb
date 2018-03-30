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
    protected string m_szLab = "";
    protected string m_szOpenRule = "";
    protected string m_szRoomMode = "";
    protected string szManModeList = "";
    protected string szManGroup = "";
	protected void Page_Load(object sender, EventArgs e)
	{
        
        UNIROOM newRoom;
        uint? uMax=0;        
        uint uID= PRDevice.DEVICE_BASE|PRDevice.MSREQ_DEVICE_SET;
        if (GetMaxValue(ref uMax, uID, "szRoomNo"))
        {
            
        }
        UNIGROUP[] manGroup = GetGroupByKind((uint)UNIGROUP.DWKIND.GROUPKIND_MAN);
        if (manGroup != null && manGroup.Length > 0)
        {
            for (int i = 0; i < manGroup.Length; i++)
            {
                szManGroup += GetInputItemHtml(CONSTHTML.option, "", manGroup[i].szName, manGroup[i].dwGroupID.ToString());
            }
        }
        szManModeList = GetInputHtmlFromXml(0, CONSTHTML.checkBox, "dwManMode", "Room_dwManMode", true);
        if (IsPostBack)
        {
            GetHTTPObj(out newRoom);
            newRoom.dwInClassKind = (uint)UNIDEVCLS.DWKIND.CLSKIND_COMPUTER;
            string szManMode = Request["dwManMode"];
            newRoom.dwManMode = CharListToUint(szManMode);
            if (newRoom.dwManGroupID == null || newRoom.dwManGroupID.ToString() == "0")
            {
                UNIGROUP newGroup = new UNIGROUP();
                if (!NewGroup(newRoom.szRoomName + "管理员组", (uint)UNIGROUP.DWKIND.GROUPKIND_MAN, out newGroup))
                {
                    MessageBox(m_Request.szErrMessage, "修改" + ConfigConst.GCRoomName + "失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                    DelGroup(newGroup.dwGroupID);
                    return;
                }
                newRoom.dwManGroupID = newGroup.dwGroupID;
            }
            if (ConfigConst.GroomNumMode == 1)
            {
                uint uManGroupID2 = Parse(Request["manGroupID2"]);
                newRoom.dwManGroupID = uManGroupID2;
            }
            {
                if (string.IsNullOrEmpty(newRoom.szRoomURL))
                {
                    newRoom.szMemo = "";
                    newRoom.szRoomURL = "";
                }
                string szProperty = Request["dwProp"];
                if (szProperty != null && szProperty == "1")
                {
                    newRoom.dwProperty = (uint)UNIROOM.DWPROPERTY.ROOMPROP_NORESV;
                }
                else if (szProperty==null)
                {
                    newRoom.dwProperty = 0;
                }
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
                    if (ConfigConst.GroomNumMode == 1)
                    {
                        PutMemberValue("manGroupID2", vtRoom[0].dwManGroupID.ToString());
                    }
                    uint? uProp = vtRoom[0].dwProperty;
                    if (uProp != null && (uProp & (uint)UNIROOM.DWPROPERTY.ROOMPROP_NORESV) > 0)
                    {
                        PutMemberValue("dwProp", "1");
                    }
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
