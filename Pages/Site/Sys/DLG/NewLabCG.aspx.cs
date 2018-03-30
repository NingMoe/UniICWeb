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
    protected string m_Campu = "";
    protected string m_szOP = "新建";
    protected void Page_Load(object sender, EventArgs e)
    {
        UNILAB newLab;
        uint? uMax = 0;
        uint uID = PRDevice.DEVICE_BASE | PRDevice.MSREQ_LAB_SET;
        if (GetMaxValue(ref uMax, uID, "dwLabID"))
        {

        }
        if (Request["op"] != null && Request["op"] == "set")
        {
            m_szOP = "修改";
        }
        if (IsPostBack)
        {
            GetHTTPObj(out newLab);
            CAMPUSREQ campGet = new CAMPUSREQ();
            UNICAMPUS[] vtCampres;
            if (m_Request.Account.CampusGet(campGet, out vtCampres) == REQUESTCODE.EXECUTE_SUCCESS && vtCampres != null && vtCampres.Length>0)
            {
                          
            }
            UNIGROUP newGroup=new UNIGROUP();
            if (!NewGroup(newLab.szLabName+"管理员组",(uint)UNIGROUP.DWKIND.GROUPKIND_MAN,out newGroup))
            {
                MessageBox(m_Request.szErrMessage, m_szOP + ConfigConst.GCLabName + "失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                DelGroup(newGroup.dwGroupID);
                return;
            }
           
            DEPTREQ vrGet = new DEPTREQ();
            vrGet.dwKind = 768;
            vrGet.dwID = newLab.dwDeptID;
            UNIDEPT[] dept;
            if (m_Request.Account.DeptGet(vrGet, out dept) == REQUESTCODE.EXECUTE_SUCCESS && dept != null && dept.Length > 0)
            {
                newLab.szLabName = dept[0].szName;
            }
            newLab.dwManGroupID = newGroup.dwGroupID;
            if (m_Request.Device.LabSet(newLab, out newLab) != REQUESTCODE.EXECUTE_SUCCESS)
            {

                MessageBox(m_Request.szErrMessage, m_szOP + ConfigConst.GCLabName + "失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                UNIROOM room = new UNIROOM();
                DEVOPENRULEREQ openRuleReq = new DEVOPENRULEREQ();
                DEVOPENRULE[] vtOpenRule;
                if (m_Request.Device.DevOpenRuleGet(openRuleReq, out vtOpenRule) == REQUESTCODE.EXECUTE_SUCCESS && vtOpenRule != null && vtOpenRule.Length > 0)
                {
                    room.dwOpenRuleSN = vtOpenRule[0].dwRuleSN;
                }
                room.dwCampusID = vtCampres[0].dwCampusID;    
                room.dwLabID = newLab.dwLabID;
                room.szRoomName = newLab.szLabName;
                room.szRoomNo = newLab.szLabSN;
                m_Request.Device.RoomSet(room, out room);
                MessageBox(m_szOP + ConfigConst.GCLabName + "成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
        }
       
        if (Request["op"] == "set")
        {
            bSet = true;

            LABREQ vrGetLab = new LABREQ();
            vrGetLab.dwLabID = Parse(Request["dwLabID"]);
            UNILAB[] vtLab;
            if (m_Request.Device.LabGet(vrGetLab, out vtLab) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else
            {
                if (vtLab.Length == 0)
                {
                    MessageBox("获取失败", "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                }
                else
                {
                    PutJSObj(vtLab[0]);
                    m_Title = "修改【" + vtLab[0].szLabName + "】";
                }
            }
        }
        else
        {
            m_Title = "新建" + ConfigConst.GCLabName;

        }
    }
}
