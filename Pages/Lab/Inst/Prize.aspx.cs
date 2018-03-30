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

public partial class Sub_Lab : UniPage
{
    protected string m_szOpts = "";
    protected MyString m_szOut = new MyString();
    protected string szPrizeName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        uint uType = Parse(Request["Type"]);
        if (uType == (uint)REWARDREC.DWREWARDKIND.REKIND_PRIZE)
        {
            szPrizeName="获奖";
            
        }else if(uType == (uint)REWARDREC.DWREWARDKIND.REKIND_PATENT)
        {
            szPrizeName = "专利";
        }
        else if(uType == (uint)REWARDREC.DWREWARDKIND.REKIND_THESISINDEX)
        {
            szPrizeName = "论文检索";
        }
        else if(uType == (uint)REWARDREC.DWREWARDKIND.REKIND_THESISISSUE)
        {
            szPrizeName = "论文发表";
        }
        else if (uType == (uint)REWARDREC.DWREWARDKIND.REKIND_TEXTBOOK)
        {
            szPrizeName = "教材";
        }
        hiddenType.Value=uType.ToString();
        if (!IsPostBack)
        {
            dwStartDate.Value = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            dwEndDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
        }   
        REWARDRECREQ vrParameter = new REWARDRECREQ();
       // vrParameter.szKindIDs = uRewordType.ToString();
        if (dwStartDate.Value != "" && dwEndDate.Value != "")
        {
            vrParameter.dwStartDate = DateToUint(dwStartDate.Value);
            vrParameter.dwEndDate = DateToUint(dwEndDate.Value);
        }
     
        REWARDREC[] vrResult;
        if (m_Request.Device.RewardRecGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                uint uKind = (uint)vrResult[i].dwRewardKind;
                if (uKind != uType)
                {
                    continue;
                }
                m_szOut += "<tr>";
                m_szOut += "<td data-id=\"" + vrResult[i].dwRewardID + "\">" + vrResult[i].dwRewardID + "</td>";
                m_szOut += "<td>" + vrResult[i].szRewardName + "</td>";
                m_szOut += "<td>" + GetJustNameEqual((uint)vrResult[i].dwRewardLevel, "Reword_RewardLevel") + "</td>";
                m_szOut += "<td>" + vrResult[i].szAuthOrg + "</td>";
                m_szOut += "<td>" + GetJustNameEqual((uint)vrResult[i].dwRewardType, "Reword_RewardType") + "</td>";
                m_szOut += "<td>" + vrResult[i].szCertID + "</td>";
                m_szOut += "<td>" + vrResult[i].szRTName + "</td>";
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Device);
        }
        PutBackValue();     
    }
    private void DelLab(string szID)
    {
       
    }
}
