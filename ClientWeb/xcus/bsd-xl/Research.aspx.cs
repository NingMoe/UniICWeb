using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_bsd_xl_Research : UniClientPage
{
    protected string selRtList = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsLogined((uint)UNISTATION.DWSUBSYSSN.SUBSYS_LAB))
        {
            Response.Redirect("Default.aspx");
        }
        //项目列表
        InitrtList();
    }

    private void InitrtList()
    {
        //获取项目列表
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        RESEARCHTESTREQ vrGet = new RESEARCHTESTREQ();
        vrGet.szReqExtInfo.szOrderKey = "szRTName";
        vrGet.szReqExtInfo.szOrderMode = "ASC";
        RESEARCHTEST[] vrResult;
        UNIACCOUNT acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
        vrGet.dwMemberID = acc.dwAccNo;
        uResponse = m_Request.Reserve.GetResearchTest(vrGet, out vrResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                RESEARCHTEST test = vrResult[i];
                RTMEMBER[] mbs = test.RTMembers;
                for (int m = 0; m < mbs.Length; m++)
                {
                    if (mbs[m].dwAccNo == acc.dwAccNo && ((mbs[m].dwStatus & 2) > 0))
                    {
                        selRtList += "<li><a href='javascript:void(0);' class='get_item' con='#act_qzone' url='RTest.aspx?rtId=" + test.dwRTID + "'><span class='ui-icon ui-icon-calculator'></span>"+test.szRTName+"</a></li>";
                    }
                }
            }
        }
        else
        {
            MsgBox(m_Request.szErrMsg);
            return;
        }
    }
}