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
    protected string m_szLab = "";
    protected string m_szOpenRule = "";
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
            string szManMode=Request["dwManMode"];
            newRoom.dwManMode = CharListToUint(szManMode);
            uint uManGroupID = 0;
            UNIGROUP newGroup = new UNIGROUP();
            if (!(ConfigConst.GroomNumMode == 1))
            {
                if (!NewGroup(newRoom.szRoomName + "管理员组", (uint)UNIGROUP.DWKIND.GROUPKIND_MAN, out newGroup))
                {
                    MessageBox(m_Request.szErrMessage, "新建" + ConfigConst.GCRoomName + "失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                    DelGroup(newGroup.dwGroupID);
                    return;
                }
                uManGroupID = (uint)newGroup.dwGroupID;
            }
            else
            {
                uManGroupID = Parse(Request["dwManGroupID2"]);
            }
            newRoom.dwManGroupID = uManGroupID;

            CAMPUSREQ campGet = new CAMPUSREQ();
                  
            UNICAMPUS[] vtCampres;
            if (m_Request.Account.CampusGet(campGet, out vtCampres) == REQUESTCODE.EXECUTE_SUCCESS && vtCampres != null && vtCampres.Length > 0)
            {
                newRoom.dwCampusID = vtCampres[0].dwCampusID;
            }
            
            {

                if (string.IsNullOrEmpty(newRoom.szRoomURL))
                {
                    newRoom.szMemo = "";
                    newRoom.szRoomURL = "";
                }
                if (m_Request.Device.RoomSet(newRoom, out newRoom) != REQUESTCODE.EXECUTE_SUCCESS)
                {
                    MessageBox(m_Request.szErrMessage, "新建" + ConfigConst.GCRoomName + "失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                    
                }
                else
                {
                    MessageBox("新建" + ConfigConst.GCRoomName + "成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                    if (Session["LoginResult"] != null)
                    {
                        ADMINLOGINRES admin = (ADMINLOGINRES)Session["LoginResult"];
                        AddGroupMember(newGroup.dwGroupID, admin.AdminInfo.dwAccNo, (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL);
                    }
                    return;
                }
            }
        }      
        uint uRoomMode = (uint)(UNIROOM.DWMANMODE.ROOMMAN_CAMERA | UNIROOM.DWMANMODE.ROOMMAN_DOORLOCK | UNIROOM.DWMANMODE.ROOMMAN_FREEINOUT);
        m_szRoomMode = GetInputHtml(uRoomMode, CONSTHTML.checkBox, "dwManMode", "Room_dwManMode");
        UNILAB[] vtLab = GetAllLab();
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
            vrRoomReq.dwRoomID=ToUint(Request["id"]);
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
            m_Title = "新建" + ConfigConst.GCRoomName;

        }                      
	}
}
