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
    public string m_szGroupID = "";
    public string m_szGroupName = "";

    public string m_szTestItemJSData = "[]";

	protected void Page_Load(object sender, EventArgs e)
	{
        m_bRemember = false;


        UNITESTPLAN vrParameter = new UNITESTPLAN();
        GetHTTPObj(out vrParameter);
        vrParameter.dwKind = (uint)UNITESTPLAN.DWKIND.TESTPLANKIND_TEACHING;

        if(Request["IsSubmit"] == "true")
        {
            UNITESTPLAN vrResult;
            vrParameter.dwYearTerm = GetTerm(Request["dwYearTerm"]);
            if (!SetGroupFromClient(ref vrParameter.dwGroupID))
            {
                return;
            }
            bool bOK = true;
            bool SetTestItem = (Request["SetTestItem"] == "true");

            if (m_Request.Reserve.SetTestPlan(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                if (SetTestItem)
                {
                    //删除旧的实验项目
                    TESTITEMREQ vrTestItemReq = new TESTITEMREQ();
                    UNITESTITEM[] vrTestItemRet;
                    vrTestItemReq.dwGetType = (uint)TESTITEMREQ.DWGETTYPE.TESTITEMGET_BYPLANID;
                    vrTestItemReq.szGetKey = vrResult.dwTestPlanID.ToString();
                    if (m_Request.Reserve.GetTestItem(vrTestItemReq, out vrTestItemRet) == REQUESTCODE.EXECUTE_SUCCESS)
                    {
                        for (int i = 0; i < vrTestItemRet.Length; i++)
                        {
                            m_Request.Reserve.DelTestItem(vrTestItemRet[i]);
                        }
                    }

                    //添加实验项目
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
                            TESTCARD vrTestCardRet;
                            vrTestCard.szTestName = testreq["szTestName"];
                            vrTestCard.dwGroupPeopleNum = ToUint(testreq["dwGroupPeopleNum"]);
                            vrTestCard.dwTestHour = ToUint(testreq["dwTestHour"]);
                            vrTestCard.szMemo = testreq["szMemo"];

                            if (m_Request.Reserve.SetTestCard(vrTestCard, out vrTestCardRet) == REQUESTCODE.EXECUTE_SUCCESS)
                            {

                                UNITESTITEM vrTestItem = new UNITESTITEM();
                                UNITESTITEM vrTestItemResult;
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
            if (bOK)
            {
                MessageBox("添加成功", "添加成功",MSGBOX.SUCCESS,MSGBOX_ACTION.OK);
            }
            else
            {
                MessageBox("添加失败" + m_Request.szErrMessage, "添加失败", MSGBOX.ERROR,MSGBOX_ACTION.NONE);
            }
        }

        if (Request["op"] == "set" && Request["IsSubmit"] != "true")
        {
            TESTPLANREQ  vrGetParameter = new TESTPLANREQ();
            UNITESTPLAN[] vrGetResult;
            vrGetParameter.dwGetType = (uint)TESTPLANREQ.DWGETTYPE.TESTPLANGET_BYID;
            vrGetParameter.szGetKey = Request["id"];
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
                vrTestItemReq.szGetKey = Request["id"];
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
