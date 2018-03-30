using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_pro_page_testitem_create : UniClientPage
{
    protected string planId;
    protected string usable;
    UNIACCOUNT acc;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsFrameLogin())
        {
            planId = Request["plan_id"];
            usable = Request["usable"];
            if (string.IsNullOrEmpty(planId))
            {
                MsgBox("未指定实验计划", "CloseDlg();");
                return;
            }
            else if (string.IsNullOrEmpty(usable) || Convert.ToInt32(usable) <= 0)
            {
                MsgBox("无可用学时", "CloseDlg();");
                return;
            }
        }
    }
    protected void submit_test_ServerClick(object sender, EventArgs e)
    {
        string name = testName.Text;
        string hour = testHour.Text;
        string num = userNum.Text;
        string report = Request["up_file"]; 
        if (string.IsNullOrEmpty(name)||string.IsNullOrEmpty(hour)){
            MsgBox("必填项不能为空");
            return;
        }
        UNIACCOUNT acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
        //TESTCARD card = new TESTCARD();
        UNITESTPLAN plan=GetTestPlanByID(planId);
        if(plan.dwTestPlanID==null){
            MsgBox("获取实验计划失败");
            return;
        }

        //card.szTestName = planId +"_"+ DateTime.Now.Ticks ;
        //if (m_Request.Reserve.SetTestCard(card, out card) == REQUESTCODE.EXECUTE_SUCCESS)
        //{
        UNITESTITEM para = new UNITESTITEM();
        para.szTestName = name;
        para.dwTestPlanID = plan.dwTestPlanID;
        para.szTestPlanName = plan.szTestPlanName;
        para.dwTeacherID = acc.dwAccNo;
        //para.dwTestCardID = card.dwTestCardID;   
        if (!string.IsNullOrEmpty(num))
            para.dwGroupPeopleNum = ToUInt(num);
        else
            para.dwGroupPeopleNum = (uint)GetGroupMemCount(plan.dwGroupID);
        para.dwTestHour = ToUInt(hour);
        para.dwGroupID = plan.dwGroupID;
        if (!string.IsNullOrEmpty(report))
            para.szReportFormURL = report;
        if (m_Request.Reserve.SetTestItem(para, out para) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            MsgBox("新建成功", "parent.location.reload();CloseDlg();");
            return;
        }
        //}
            MsgBox(m_Request.szErrMsg);
    }
}