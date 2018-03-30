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
using System.Collections.Specialized;

public partial class _Default : UniPage
{
    public string m_szTitle = "添加实验计划";
    public string m_szGroupID = "";
    public string m_szGroupName = "";
    protected string m_TermList ="";
    protected string szAcademicSubject = "";
    protected string szTesteeKind = "";
    public string m_szTestItemJSData = "[]";
    protected string szTeacherDeptName = "";
    protected string szIsTogether = "";//修改实验计划是否修改项目
	protected void Page_Load(object sender, EventArgs e)
	{
        uint uTermNow = 0;
        if (Request["op"] == "set")
        {
            m_szTitle = "修改实验计划";
        }
        UNITERM[] termList = GetAllTerm();
        if (termList != null)
        {
            for (int i = 0; i < termList.Length; i++)
            {
                m_TermList += GetInputItemHtml(CONSTHTML.option, "", termList[i].szMemo.ToString(), termList[i].dwYearTerm.ToString());
                uint uYearTermState = (uint)termList[i].dwStatus;
                if ((uYearTermState & (uint)UNITERM.DWSTATUS.TERMSTAT_FORCE) > 0)
                {
                    uTermNow = (uint)termList[i].dwYearTerm;

                }
            }
        }
        szAcademicSubject = GetInputHtmlFromXml(0, CONSTHTML.option, "", "dwAcademicSubject", true);
        szTesteeKind = GetInputHtmlFromXml(0, CONSTHTML.option, "", "dwTesteeKind", true);
        string szID = Request["id"];
        if (string.IsNullOrEmpty(szID) || szID == "0")
        {
            szID = Request["dwTestPlanID"];
        }

        UNITESTPLAN vrParameter = new UNITESTPLAN();
        GetHTTPObj(out vrParameter);
        vrParameter.dwKind = (uint)UNITESTPLAN.DWKIND.TESTPLANKIND_TEACHING;
        UNIACCOUNT accTeacher = new UNIACCOUNT();
        vrParameter.dwTestHour = Parse(Request["dwTotalTestHour2"]);
        if (GetAccByAccno(vrParameter.dwTeacherID.ToString(), out accTeacher))
        {
            vrParameter.szTestPlanName = accTeacher.szTrueName + "_" + Request["szCourseName"];
        }
        if(IsPostBack)
        {
            if (m_Request.Reserve.SetTestPlan(vrParameter, out vrParameter) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                if (ConfigConst.GCscheduleMode==2)//修改实验项目
                {
                    TESTITEMREQ vrTestItemGet = new TESTITEMREQ();
                    vrTestItemGet.dwGetType = (uint)TESTITEMREQ.DWGETTYPE.TESTITEMGET_BYPLANID;
                    vrTestItemGet.szGetKey = vrParameter.dwTestPlanID.ToString();
                    UNITESTITEM[] vtTestItem;
                    if (m_Request.Reserve.GetTestItem(vrTestItemGet, out vtTestItem) == REQUESTCODE.EXECUTE_SUCCESS && vtTestItem != null && vtTestItem.Length > 0)
                    {
                        TESTCARD setTestCard = new TESTCARD();
                        setTestCard.dwTestCardID = vtTestItem[0].dwTestCardID;
                      
                        setTestCard.szTestName = vrParameter.szTestPlanName;
                        setTestCard.dwTestHour = vrParameter.dwTestHour;
                       // if (m_Request.Reserve.SetTestCard(setTestCard, out setTestCard) == REQUESTCODE.EXECUTE_SUCCESS)
                        {
 
                        }
                    }
                    
                }
                MessageBox(m_szTitle + "成功", m_szTitle + "成功", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                UNIGROUP setGroup = new UNIGROUP();
                setGroup.dwGroupID = vrParameter.dwGroupID;
                setGroup.szName = vrParameter.szGroupName;
                m_Request.Group.SetGroup(setGroup,out setGroup);
            }
            else
            {
                MessageBox(m_szTitle + "失败," + m_Request.szErrMessage, m_szTitle + "失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }      
        }
        if (!string.IsNullOrEmpty(szID) && szID != "0")
        {
            TESTPLANREQ  vrGetParameter = new TESTPLANREQ();
            UNITESTPLAN[] vrGetResult;
            vrGetParameter.dwGetType = (uint)TESTPLANREQ.DWGETTYPE.TESTPLANGET_BYID;
            vrGetParameter.szGetKey = szID;
            if (m_Request.Reserve.GetTestPlan(vrGetParameter, out vrGetResult) == REQUESTCODE.EXECUTE_SUCCESS && vrGetResult.Length > 0)
            {
                vrParameter = vrGetResult[0];

                szTeacherDeptName = vrParameter.szTeacherDeptName;
                //获取班级列表
                GROUPREQ  vrGroupReq = new GROUPREQ();
                UNIGROUP[] vrGroupRet;
               // vrGroupReq.dwGetType = (uint)GROUPREQ.DWGETTYPE.GROUPGET_BYID;
                vrGroupReq.dwGroupID = vrParameter.dwGroupID;//.ToString();
                if (m_Request.Group.GetGroup(vrGroupReq, out vrGroupRet) == REQUESTCODE.EXECUTE_SUCCESS && vrGroupRet.Length > 0)
                {
                    for (int i = 0; i < vrGroupRet[0].szMembers.Length; i++)
                    {
                        m_szGroupID += vrGroupRet[0].szMembers[i].dwMemberID + ",";
                        m_szGroupName += vrGroupRet[0].szMembers[i].szName + ",";
                    }
                }
                PutMemberValue("dwGroupIDTemp", vrParameter.dwGroupID.ToString());
                PutMemberValue("dwGroupID", vrParameter.dwGroupID.ToString());
                PutMemberValue("dwTotalTestHour2", vrParameter.dwTestHour.ToString());
                //获取项目列表
                TESTITEMREQ vrTestItemReq = new TESTITEMREQ();
                UNITESTITEM[] vrTestItemRet;
                vrTestItemReq.dwGetType = (uint)TESTITEMREQ.DWGETTYPE.TESTITEMGET_BYPLANID;
                vrTestItemReq.szGetKey = szID;
                if (m_Request.Reserve.GetTestItem(vrTestItemReq, out vrTestItemRet) == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    m_szTestItemJSData = "[";
                    for (int i = 0; i < vrTestItemRet.Length; i++)
                    {
                        m_szTestItemJSData += UniLibrary.ObjHelper.OBJ2JS(vrTestItemRet[i])+",";
                    }
                    m_szTestItemJSData += "null]";
                }
            }
            else
            {
                MessageBox("获取失败,无此记录", "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
        }
        PutJSObj(vrParameter);
	}

    bool SetGroupFromClient( ref uint? dwGroupID)
    {
        string szGroup = Request["GroupList"];
        if (string.IsNullOrEmpty(szGroup))
        {
            MessageBox("班级组不能为空", "创建预约失败", MSGBOX.ERROR);
            return false;
        }

        if (!IsNullOrZero(dwGroupID))
        {
            GROUPREQ vrGetGroup = new GROUPREQ();
           // vrGetGroup.dwGetType = (uint)GROUPREQ.DWGETTYPE.GROUPGET_BYID;
            vrGetGroup.dwGroupID = dwGroupID;

            UNIGROUP[] vrGetGroupRet;
            if (m_Request.Group.GetGroup(vrGetGroup, out vrGetGroupRet) == REQUESTCODE.EXECUTE_SUCCESS && vrGetGroupRet.Length > 0)
            {
                for (int i = 0; i < vrGetGroupRet[0].szMembers.Length; i++)
                {
                    m_Request.Group.DelGroupMember(vrGetGroupRet[0].szMembers[i]);
                }
            }
            else
            {
                dwGroupID = 0;
            }
        }


        UNIGROUP vrGroup = new UNIGROUP();
        if (IsNullOrZero(dwGroupID))
        {
            vrGroup.dwKind = (uint)UNIGROUP.DWKIND.GROUPKIND_RERV;

            vrGroup.szName = "" + DateTime.Now.Ticks;
            if (m_Request.Group.SetGroup(vrGroup, out vrGroup) != REQUESTCODE.EXECUTE_SUCCESS || vrGroup.dwGroupID == 0)
            {
                MessageBox("创建预约班级组失败", "创建预约失败", MSGBOX.ERROR);
                return false;
            }
            dwGroupID = vrGroup.dwGroupID;
        }
        else
        {
            vrGroup.dwGroupID = dwGroupID;
        }


        string[] arrayGroupName = Request["GroupListName"].Split(new char[] { ',' });
        string[] arrayGroup = szGroup.Split(new char[] { ',' });
        for (int i = 0; i < arrayGroup.Length; i++)
        {
            uint nClsID = 0;
            uint.TryParse(arrayGroup[i], out nClsID);
            if (nClsID != 0)
            {
                GROUPMEMBER vrGrpMember = new GROUPMEMBER();
                vrGrpMember.dwGroupID = vrGroup.dwGroupID;
                vrGrpMember.dwMemberID = nClsID;
                vrGrpMember.dwKind = (uint)GROUPMEMBER.DWKIND.MEMBERKIND_CLASS;
                vrGrpMember.szName = arrayGroupName[i];
                if (m_Request.Group.SetGroupMember(vrGrpMember) != REQUESTCODE.EXECUTE_SUCCESS)
                {
                    m_Request.Group.DelGroup(vrGroup);
                    dwGroupID = 0;
                    MessageBox("设置预约班级组失败", "创建预约失败", MSGBOX.ERROR);
                    return false;
                }
            }
        }

        return true;
    }
}
