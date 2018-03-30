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
    protected string dwKindList = "";
    protected string szKindObject = "";
    protected string m_szRoom = "";
	protected void Page_Load(object sender, EventArgs e)
	{       
        if (IsPostBack)
        {
            UNICONSOLE newConsole;
            GetHTTPObj(out newConsole);
            newConsole.dwOpenTime = GetTime(Request["dwOpenTime"]);
            newConsole.dwCloseTime = GetTime(Request["dwCloseTime"]);
            newConsole.szManRooms = GetRoomNoCtrlList(Request["szManRooms"]);
            newConsole.dwKind = CharListToUint(Request["dwKindListObject"]) + Parse(Request["dwKind"]);
            if (m_Request.Console.ConSet(newConsole, out newConsole) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "新建失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox("新建成功", "新建控制台", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }

        }
        dwKindList = GetAllInputHtml(CONSTHTML.option, "", "Console_Kind");
        szKindObject = GetAllInputHtml(CONSTHTML.checkBox, "dwKindListObject", "Console_Kind_Object");
        UNIROOM[] roomList = GetAllRoom();
        if (roomList != null && roomList.Length > 0)
        {
            for (int i = 0; i < roomList.Length; i++)
            {
                string szCheck = "";

                m_szRoom += "<label><input class=\"enum\"" + szCheck + "type=\"checkbox\" name=\"" + "szManRooms" + "\" value=\"" + roomList[i].szRoomNo.ToString() + "\" /> " + roomList[i].szRoomName + "</label>";
            }
        }
        if (Request["op"] == "set")
        {
            bSet = true;

            CONREQ vrConGet = new CONREQ();
            vrConGet.dwConsoleSN = ToUint(Request["dwID"]);
            UNICONSOLE[] vrConRes;
            if (m_Request.Console.ConGet(vrConGet, out vrConRes) != REQUESTCODE.EXECUTE_SUCCESS)
            {                
                MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else
            {
                if (vrConRes.Length == 0)
                {
                    MessageBox("获取失败", "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                }
                else
                {
                    PutJSObj(vrConRes[0]);
                    ViewState["dwKind"] = vrConRes[0].dwKind.ToString();
                    m_Title = "新建" + "【" + vrConRes[0].szConsoleName + "】";
                }
            }
        }
        else
        {
            uint? uMax = 0;
            uint uID = PRStation.DOORCTRLSRV_BASE | PRDoorCtrlSrv.MSREQ_DCS_SET;
     
            if (GetMaxValue(ref uMax, uID, "dwSN"))
            {
                UNICONSOLE setValue = new UNICONSOLE();
                setValue.dwConsoleSN = uMax;
                PutJSObj(setValue);
                
            }
            m_Title = "新建控制台";
        }
	}
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        if (ViewState["dwKind"] != null)
        {
            PutMemberValue("dwKind", UintToCharList(Parse(ViewState["dwKind"].ToString()), "Console_Kind"));
            PutMemberValue("dwKindListObject", UintToCharList(Parse(ViewState["dwKind"].ToString()), "Console_Kind_Object"));
        }        
    }

}
