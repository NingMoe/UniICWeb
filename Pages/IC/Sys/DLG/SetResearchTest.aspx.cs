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
            newResearch.szFundsNo = szFoundNo1 + "," + szFoundNo1 + "," + szFoundNo3;
            newResearch.dwBeginDate = GetDate(Request["dwBeginDate"]);
            if (m_Request.Reserve.SetResearchTest(newResearch, out newResearch) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "修改"+ConfigConst.GCReachTestName+"失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox("修改" + ConfigConst.GCReachTestName + "成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
        }
        m_szLevel = GetAllInputHtml(CONSTHTML.option, "", "ResearchTest_Level");
        
        if (Request["op"] == "set")
        {
            bSet = true;

            RESEARCHTESTREQ vrGet = new RESEARCHTESTREQ();
            vrGet.dwRTID = Parse(Request["id"]);
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
                    ViewState["dwBeginDate"] = GetDateStr(vtRes[0].dwBeginDate);
                    ViewState["szFundsNo"] = vtRes[0].szFundsNo;
                    m_Title = "修改【" + vtRes[0].szRTName + "】";
                }
            }
        }
        else
        {
            m_Title = "新建"+ ConfigConst.GCReachTestName;

        }
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreLoad(e);
        if (ViewState["dwBeginDate"] != null && ViewState["dwBeginDate"].ToString() != "")
        {
            PutMemberValue("dwBeginDate", ViewState["dwBeginDate"].ToString());
        }
        if (ViewState["szFundsNo"] != null && ViewState["szFundsNo"].ToString() != "")
        {
            string[] szList = ViewState["szFundsNo"].ToString().Split(',');
            for (int i = 0; i < szList.Length; i++)
            {
                if (i == 0)
                {
                    PutMemberValue("szFoundNo1", szList[0]);
                }
                else if (i == 1)
                {
                    PutMemberValue("szFoundNo2", szList[1]);
                }
                else if (i == 2)
                {
                    PutMemberValue("szFoundNo3", szList[2]);
                }
            }
        }
    }
}
