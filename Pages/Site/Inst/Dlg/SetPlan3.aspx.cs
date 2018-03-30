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
    public string m_szTitle = "添加实验计划 - 1.编辑基本信息";
    public string m_szGroupID = "";
    public string m_szGroupName = "";
    public bool m_CreateOK = false;
    public string m_szOKBtnText = "下一步";
    public string m_szCancelBtnText = "关闭";
    public string m_TermText = "";
    public string m_szTesteeKind = "";
    public bool bSet = false;
    public string m_szTestItemJSData = "[]";
    protected uint m_TermList = 0;

	protected void Page_Load(object sender, EventArgs e)
	{
        m_TermList = 7;// GetTermList();

        bSet = Request["op"] == "set";
        if (bSet)
        {
            m_szTitle = "修改实验计划";
            m_szOKBtnText = "确定";
            m_szCancelBtnText = "关闭";
        }
        if (string.IsNullOrEmpty(Request["Step"]) || Request["Step"] == "0")
        {
            m_CreateOK = false;
        }
        else
        {
            m_CreateOK = true;
        }
        string szID = Request["id"];

        UNITESTPLAN vrParameter = new UNITESTPLAN();
        GetHTTPObj(out vrParameter);
        vrParameter.dwKind = (uint)UNITESTPLAN.DWKIND.TESTPLANKIND_TEACHING;

        if(Request["IsSubmit"] == "true")
        {
            UNITESTPLAN vrResult = new UNITESTPLAN();
            vrParameter.dwYearTerm = GetTerm(Request["dwYearTerm"]);
            
            bool bOK = true;
            bool SetTestItem = (Request["SetTestItem"] == "true");

            if (!m_CreateOK)
            {
                SetTestItem = false;
            }
            if (!SetGroupFromClient(ref vrParameter.dwGroupID))
            {
                bOK = false;
            }else{
                if ((m_CreateOK && !bSet) || m_Request.Reserve.SetTestPlan(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    if (!IsNullOrZero(vrResult.dwTestPlanID))
                    {
                        szID = vrResult.dwTestPlanID.ToString();
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(szID) || szID == "0")
                        {
                            szID = vrParameter.dwTestPlanID.ToString();
                        }
                        if (string.IsNullOrEmpty(szID) || szID == "0")
                        {
                            szID = Request["id"];
                        }
                        vrResult = vrParameter;
                    }

                    if (SetTestItem)
                    {
                        //处理被删除的实验项目
                        string szTestItemDelList = Request["TestItemDelList"];
                        if (!string.IsNullOrEmpty(szTestItemDelList))
                        {
                            string[] arrTestItemDelList = szTestItemDelList.Split(new char[] { ',' });
                            foreach (string szTestItemID in arrTestItemDelList)
                            {
                                string[] arrID = szTestItemID.Split(new char[] { ':' });
                                if (arrID.Length == 2)
                                {
                                    uint testitemID = ToUint(arrID[0]);
                                    uint testcardID = ToUint(arrID[1]);
                                    UNITESTITEM delItem = new UNITESTITEM();
                                    delItem.dwTestItemID = testitemID;
                                    delItem.dwTestCardID = testcardID;
                                    if (m_Request.Reserve.DelTestItem(delItem) != REQUESTCODE.EXECUTE_SUCCESS)
                                    {

                                    }

                                    TESTCARD delCard = new TESTCARD();
                                    delCard.dwTestCardID = testcardID;
                                    if (m_Request.Reserve.DelTestCard(delCard) != REQUESTCODE.EXECUTE_SUCCESS)
                                    {
                                    }
                                }
                            }
                        }

                        //添加修改实验项目
                        string szItemAllData = Request["ItemAllData"];
                        if (!string.IsNullOrEmpty(szItemAllData))
                        {
                            NameValueCollection result = HttpUtility.ParseQueryString(szItemAllData);
                            int ItemDataCount = int.Parse(result["ItemDataCount"]);
                            for (int i = 0; i < ItemDataCount; i++)
                            {
                                string item = result["ItemData" + i];
                                item = HttpUtility.UrlDecode(item);
                                NameValueCollection testreq = HttpUtility.ParseQueryString(item);

                                TESTCARD vrTestCard = new TESTCARD();
                                vrTestCard = (TESTCARD)UniLibrary.ObjHelper.NameValue2OBJ(testreq, "", typeof(TESTCARD));
                                if (vrTestCard.dwTestCardID == 0) { vrTestCard.dwTestCardID = null; }

                                TESTCARD vrTestCardRet;
                                if (m_Request.Reserve.SetTestCard(vrTestCard, out vrTestCardRet) == REQUESTCODE.EXECUTE_SUCCESS)
                                {
                                    UNITESTITEM vrTestItem = new UNITESTITEM();
                                    UNITESTITEM vrTestItemResult;
                                    vrTestItem.dwTestItemID = ToUint(testreq["dwTestItemID"]); if (vrTestItem.dwTestItemID == 0) { vrTestItem.dwTestItemID = null; }
                                    vrTestItem.dwTestPlanID = vrResult.dwTestPlanID;
                                    vrTestItem.szTestPlanName = vrResult.szTestPlanName;
                                    vrTestItem.dwTotalTestHour = vrTestCardRet.dwTestHour;
                                    //vrTestItem.dwTeacherID = ((ADMINLOGINRES)Session["LoginResult"]).AdminInfo.dwAccNo;
                                    vrTestItem.dwTestCardID = vrTestCardRet.dwTestCardID;
                                    vrTestItem.dwCourseID = vrResult.dwCourseID;
                                    vrTestItem.dwGroupID = vrResult.dwGroupID;
                                    vrTestItem.szMemo = vrTestCardRet.szMemo;

                                    if (m_Request.Reserve.SetTestItem(vrTestItem, out vrTestItemResult) == REQUESTCODE.EXECUTE_SUCCESS)
                                    {

                                    }
                                    else
                                    {
                                        bOK = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    bOK = false;
                                    break;
                                }

                            }
                        }
                    }
                }
                else
                {
                    bOK = false;
                }
            }
            if (bOK)
            {
                if (m_CreateOK)
                {
                    MessageBox(m_szTitle + "成功", m_szTitle + "成功", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                }
                else
                {
                    m_CreateOK = true;
                }
            }
            else
            {
                MessageBox(m_szTitle + "失败," + m_Request.szErrMessage, m_szTitle + "失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            if (m_CreateOK)
            {
                if (Request["op"] == "set")
                {
                    m_szTitle = "修改实验计划";
                }
                else
                {
                    m_szTitle = "添加实验计划 - 2.编辑实验项目";
                }
                m_szOKBtnText = "确定";
                m_szCancelBtnText = "关闭";
            }
        }

        if (m_CreateOK || (Request["op"] == "set" && Request["IsSubmit"] != "true"))
        {
            TESTPLANREQ  vrGetParameter = new TESTPLANREQ();
            UNITESTPLAN[] vrGetResult;
            vrGetParameter.dwGetType = (uint)TESTPLANREQ.DWGETTYPE.TESTPLANGET_BYID;
            vrGetParameter.szGetKey = szID;
            if (m_Request.Reserve.GetTestPlan(vrGetParameter, out vrGetResult) == REQUESTCODE.EXECUTE_SUCCESS && vrGetResult.Length > 0)
            {
                vrParameter = vrGetResult[0];
                if (vrParameter.dwYearTerm == 0)
                {
                    vrParameter.dwYearTerm = GetDefaultTerm(null);
                }

                //获取班级列表
                GROUPREQ  vrGroupReq = new GROUPREQ();
                UNIGROUP[] vrGroupRet;
                //vrGroupReq.dwGetType = (uint)GROUPREQ.DWGETTYPE.GROUPGET_BYID;
                vrGroupReq.dwGroupID = vrParameter.dwGroupID;//.ToString();
                if (m_Request.Group.GetGroup(vrGroupReq, out vrGroupRet) == REQUESTCODE.EXECUTE_SUCCESS && vrGroupRet.Length > 0)
                {
                    for (int i = 0; i < vrGroupRet[0].szMembers.Length; i++)
                    {
                        m_szGroupID += vrGroupRet[0].szMembers[i].dwMemberID + ",";
                        m_szGroupName += vrGroupRet[0].szMembers[i].szName + ",";
                    }
                }


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
        TranTerm(ref vrParameter.dwYearTerm);
        m_TermText = GetTermText(vrParameter.dwYearTerm);
        m_szTesteeKind = GetTesteeKind(vrParameter.dwTesteeKind);

        PutJSObj(vrParameter);
	}

    string GetTesteeKind(uint? dwTesteeKind)
    {
        if (dwTesteeKind == 1)
        {
            return "博士生";
        }
        else if (dwTesteeKind == 2)
        {
            return "硕士生";
        }
        else if (dwTesteeKind == 3)
        {
            return "本科生";
        }
        else if (dwTesteeKind == 4)
        {
            return "专科生";
        }
        else
        {
            return "其它";
        }
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
            //vrGetGroup.dwGetType = (uint)GROUPREQ.DWGETTYPE.GROUPGET_BYID;
            vrGetGroup.dwGroupID = dwGroupID;//.ToString();

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
