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
    protected string szPrizeName = "";
    protected string szRewardLevel = "";
    protected string szResearch = "";
    protected string szRewardType = "";
	protected void Page_Load(object sender, EventArgs e)
    {
        uint uType = Parse(Request["type"]);
        if (uType == (uint)REWARDREC.DWREWARDKIND.REKIND_PRIZE)
        {
            szPrizeName = "获奖";
            szRewardLevel = GetInputHtmlFromXml(0, CONSTHTML.option, "", "Reword_RewardLevel_PRIZE", true);
        }
        else if (uType == (uint)REWARDREC.DWREWARDKIND.REKIND_PATENT)
        {
            szPrizeName = "专利";
            szRewardLevel = GetInputHtmlFromXml(0, CONSTHTML.option, "", "Reword_RewardLevel_PATENT", true);
        }
        else if (uType == (uint)REWARDREC.DWREWARDKIND.REKIND_THESISINDEX)
        {
            szPrizeName = "论文检索";
            szRewardLevel = GetInputHtmlFromXml(0, CONSTHTML.option, "", "Reword_RewardLevel_THESISINDEX", true);
            divCert.Style.Add("display", "none");

        }
        else if (uType == (uint)REWARDREC.DWREWARDKIND.REKIND_THESISISSUE)
        {
            szPrizeName = "论文发表";
            szRewardLevel = GetInputHtmlFromXml(0, CONSTHTML.option, "", "Reword_RewardLevel_THESISINDEX", true);
            divCert.Style.Add("display", "none");

        }
        else if (uType == (uint)REWARDREC.DWREWARDKIND.REKIND_TEXTBOOK)
        {
            szPrizeName = "教材";
            szRewardLevel = GetInputHtmlFromXml(0, CONSTHTML.option, "", "Reword_RewardLevel", true);
            divCert.Style.Add("display", "none");

        }
      
        REWARDREC newReword;
       
        if (IsPostBack)
        {
            GetHTTPObj(out newReword);
            if (m_Request.Device.RewardRecSet(newReword, out newReword) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "新建" + szPrizeName + "失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox("新建" + szPrizeName + "成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
        }

        szRewardType = GetInputHtmlFromXml(0, CONSTHTML.option, "", "Reword_RewardType", true);
        RESEARCHTESTREQ vrReserchGet = new RESEARCHTESTREQ();
        RESEARCHTEST[] vtRes;
        m_Request.Reserve.GetResearchTest(vrReserchGet, out vtRes);
        if (vtRes != null && vtRes.Length > 0)
        {
            for (int i = 0; i < vtRes.Length; i++)
            {
                szResearch += GetInputItemHtml(CONSTHTML.option, "", vtRes[i].szRTName.ToString(), vtRes[i].dwRTID.ToString());// ;
            }
        }
        if (Request["op"] == "set")
        {
            bSet = true;

            REWARDRECREQ vrRewordReq = new REWARDRECREQ();
            vrRewordReq.dwStartDate = Parse(DateTime.Now.AddDays(-10).ToString("yyyyMMdd"));
            vrRewordReq.dwEndDate = Parse(DateTime.Now.ToString("yyyyMMdd"));           
            REWARDREC[] vtResRew;
            if (m_Request.Device.RewardRecGet(vrRewordReq, out vtResRew) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else
            {
                if (vtResRew.Length == 0)
                {
                    MessageBox("获取失败", "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                }
                else
                {
                    PutJSObj(vtResRew[0]);
                    m_Title = "修改站点【" + vtResRew[0].szRewardName + "】";
                }
            }
        }
        else
        {
            m_Title = "新建" + szPrizeName;

        }
    }
}
