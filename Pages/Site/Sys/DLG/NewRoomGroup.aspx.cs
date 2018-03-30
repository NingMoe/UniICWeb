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
    protected string m_szDept = "";
    protected string m_szLabKind = "";
    protected string m_szRoom = "";
	protected void Page_Load(object sender, EventArgs e)
    {
        ROOMGROUP newRoomGroup;
        if (IsPostBack)
        {
            GetHTTPObj(out newRoomGroup);
            ArrayList list = new ArrayList();
            string szRoomID = Request["roomID"];
            string[] szRoomList = szRoomID.Split(',');
            for (int i = 0; i < szRoomList.Length; i++)
            {
                RGMEMBER member = new RGMEMBER();
                member.dwRoomID = Parse(szRoomList[i]);
                UNIROOM room;
                GetRoomID(szRoomList[i], out room);
                member.szRoomName = room.szRoomName;
                list.Add(member);
            }
            newRoomGroup.rgMember = new RGMEMBER[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                RGMEMBER temp = (RGMEMBER)list[i];
                newRoomGroup.rgMember[i] = temp;
            }
            if (m_Request.Device.RoomGroupSet(newRoomGroup, out newRoomGroup) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "新建" + ConfigConst.GCLabName + "失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox("新建" + ConfigConst.GCRoomName + "组合成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
        }
        UNIROOM[] roomList = GetAllRoom();
        if (roomList != null && roomList.Length > 0)
        {
            for (int i = 0; i < roomList.Length; i++)
            {
                string szCheck = "";

                m_szRoom += "<label><input class=\"enum\"" + szCheck + "type=\"checkbox\" name=\"" + "roomID" + "\" value=\"" + roomList[i].dwRoomID.ToString() + "\" /> " + roomList[i].szRoomName + "</label>";
            }
        }
        {
            m_Title = "新建" + ConfigConst.GCRoomName+"组合";

        }
    }
}
