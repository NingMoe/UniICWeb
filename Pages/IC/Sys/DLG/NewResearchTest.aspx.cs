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
    protected string m_szLevel = "";
    protected string m_szDept = "";
	protected void Page_Load(object sender, EventArgs e)
    {
        RESEARCHTEST newResearch;
        uint? uMax = 0;
        uint uID = PRDevice.DEVICE_BASE | PRDevice.MSREQ_LAB_SET;
        if (GetMaxValue(ref uMax, uID, "dwLabID"))
        {

        }
        if (IsPostBack)
        {
            GetHTTPObj(out newResearch);
            string szFoundNo1 = Request["szFoundNo1"];
            string szFoundNo2 = Request["szFoundNo2"];
            string szFoundNo3 = Request["szFoundNo3"];
            newResearch.szFundsNo = szFoundNo1 + "," + szFoundNo1 +","+ szFoundNo3;
            newResearch.dwBeginDate = GetDate(Request["dwBeginDate"]);
            UNIGROUP setGroup;
            if (NewGroup(newResearch.szRTName, (uint)UNIGROUP.DWKIND.GROUPKIND_RERV, out setGroup))
            {
                newResearch.dwGroupID = setGroup.dwGroupID;
             
            }
            else
            {
                MessageBox(m_Request.szErrMessage, "新建" + ConfigConst.GCReachTestName + "失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                return;
            }
            if (m_Request.Reserve.SetResearchTest(newResearch, out newResearch) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "新建"+ConfigConst.GCReachTestName+"失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                
            }
            else
            {
                MessageBox("新建" + ConfigConst.GCReachTestName + "成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                AddGroupMember(setGroup.dwGroupID, newResearch.dwLeaderID, (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL);
                return;
            }
        }
        m_szLevel = GetAllInputHtml(CONSTHTML.option, "", "ResearchTest_Level");
      
        if (Request["op"] == "set")
        {
            bSet = true;

            RESEARCHTESTREQ vrGet = new RESEARCHTESTREQ();
            vrGet.dwRTID = Parse(Request["dwID"]);
            RESEARCHTEST[] vtRes;
            if (m_Request.Reserve.GetResearchTest(vrGet, out vtRes) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else
            {
                if (vtRes.Length == 0)
                {
                    MessageBox("获取失败", "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                }
                else
                {
                    PutJSObj(vtRes[0]);
                    m_Title = "修改【" + vtRes[0].szRTName + "】";
                }
            }
        }
        else
        {
            m_Title = "新建"+ ConfigConst.GCReachTestName;

        }
    }
}
