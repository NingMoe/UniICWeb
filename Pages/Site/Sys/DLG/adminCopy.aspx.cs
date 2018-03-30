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
    protected string m_szSta = "";
    protected string szManRole = "";
    protected string m_szRoom = "";
    protected string m_szLab = "";
    protected string m_checkLab = "";
    protected string m_adminKind = "";
    protected string m_adminLevle= "";
	protected void Page_Load(object sender, EventArgs e)
	{
        if (IsPostBack)
        {
            ADMINREQ req = new ADMINREQ();
            req.dwAccNo = Parse(Request["dwID"]);
            UNIADMIN[] adminList;
            if (m_Request.Admin.Get(req, out adminList) == REQUESTCODE.EXECUTE_SUCCESS && adminList != null && adminList.Length > 0)
            {
                UNIADMIN newAdmin = new UNIADMIN();
                GetHTTPObj(out newAdmin);
                newAdmin.dwManLevel = adminList[0].dwManLevel;
                newAdmin.dwManRole = adminList[0].dwManRole;
                newAdmin.dwProperty = adminList[0].dwProperty;
                if (m_Request.Admin.Set(newAdmin, out newAdmin) != REQUESTCODE.EXECUTE_SUCCESS)
                {
                    MessageBox(m_Request.szErrMessage, "新建失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                    return;
                }
                else
                {

                    MANROOMREQ manRoomGet = new MANROOMREQ();
                    manRoomGet.dwAccNo = adminList[0].dwAccNo;
                    manRoomGet.dwManFlag = 1;
                    MANROOM[] vtResManRoom;
                    if (m_Request.Admin.GetManRoom(manRoomGet, out vtResManRoom) == REQUESTCODE.EXECUTE_SUCCESS && vtResManRoom != null && vtResManRoom.Length > 0)
                    {
                        for (int i = 0; i < vtResManRoom.Length; i++)
                        {
                            AddGroupMember(vtResManRoom[i].dwManGroupID, newAdmin.dwAccNo, (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL);
                        }
                    }
                    MessageBox("复制成功", "复制成功", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
        
                }


            }
        }
	}
}