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
using System.Collections;
using UniWebLib;

public partial class _Default : UniPage
{
    protected bool bSet = false;
    protected string m_Title = "";
    protected string szOpenRule = "";
    protected string szHour = "";
    protected string szMin = "";
    protected string szRoomList = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        ATTENDRULE newRule;
        for (int i = 0; i <= 23; i++)
        {
            szHour += GetInputItemHtml(CONSTHTML.option, "", i.ToString(), i.ToString("00"));
        }
        for (int i = 0; i <= 59; i++)
        {
            szMin += GetInputItemHtml(CONSTHTML.option, "", i.ToString(), i.ToString("00"));
        }
        UNIROOM[] roomList;
        roomList = GetAllRoom();
        for (int i = 0; roomList!=null&&i < roomList.Length; i++)
        {
            szRoomList += GetInputItemHtml(CONSTHTML.checkBox, "roomIDs", roomList[i].szRoomName, roomList[i].dwRoomID.ToString());
        }
        DEVOPENRULEREQ openRuleGet = new DEVOPENRULEREQ();
        DEVOPENRULE[] vtOpenRule;
        if (m_Request.Device.DevOpenRuleGet(openRuleGet, out vtOpenRule) == REQUESTCODE.EXECUTE_SUCCESS && vtOpenRule != null && vtOpenRule.Length > 0)
        {
            for (int i = 0; i < vtOpenRule.Length; i++)
            {
                szOpenRule += GetInputItemHtml(CONSTHTML.option, "", vtOpenRule[i].szRuleName, vtOpenRule[i].dwRuleSN.ToString());
            }
        }
        if (IsPostBack)
        {
            GetHTTPObj(out newRule);
            newRule.dwStartDate = DateToUint(Request["dwStartDate"]);
            newRule.dwEndDate = DateToUint(Request["dwEndDate"]);

            newRule.dwEarlyInTime = Parse(Request["dwEarlyInTimeH"]) * 100 + Parse(Request["dwEarlyInTimeM"]);
            newRule.dwLateInTime = Parse(Request["dwLateInTimeH"]) * 100 + Parse(Request["dwLateInTimeM"]);
            newRule.dwEarlyOutTime = Parse(Request["dwEarlyOutTimeH"]) * 100 + Parse(Request["dwEarlyOutTimeM"]);
            newRule.dwLateOutTime = Parse(Request["dwLateOutTimeH"]) * 100 + Parse(Request["dwLateOutTimeM"]);
            if (newRule.dwEarlyInTime == 0)
            {
                newRule.dwEarlyInTime = null;
            }
            if (newRule.dwLateInTime == 0)
            {
                newRule.dwLateInTime = null;
            }
            if (newRule.dwEarlyOutTime == 0)
            {
                newRule.dwEarlyOutTime = null;
            }
            if (newRule.dwLateOutTime == 0)
            {
                newRule.dwLateOutTime = null;
            }
            string szRoomIDs = Request["roomIDs"];
            string[] szRoomIDList = szRoomIDs.Split(',');
            ArrayList list = new System.Collections.ArrayList();
            for (int i = 0; i < szRoomIDList.Length; i++)
            {
                if (szRoomIDList[i] != null && szRoomIDList[i] != "")
                {
                    list.Add(szRoomIDList[i]);
                }
            }
            object[] objList = list.ToArray();
            ATTENDROOM[] attendRoom = new ATTENDROOM[objList.Length];
            for (int i = 0; i < objList.Length; i++)
            {
                attendRoom[i].dwRoomID = Parse(objList[i].ToString());
            }
            newRule.AttendRoom = attendRoom;
            if (Request["op"] != "set")
            {
                UNIGROUP group;
                if (NewGroup(newRule.szAttendName + "考勤组", (uint)UNIGROUP.DWKIND.GROUPKIND_ATTEND, out group))
                {
                    newRule.dwGroupID = group.dwGroupID;
                }
                else
                {
                    MessageBox(m_Request.szErrMessage, "新建考勤规则失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                    return;
                }
            }
            if (m_Request.Attendance.SetAttendRule(newRule, out newRule) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "新建考勤规则失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox("新建考勤规则成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
        }
        if (Request["op"] == "set")
        {
            bSet = true;
        }
        else
        {
            m_Title = "新建考勤规则";
        }
    }
}
