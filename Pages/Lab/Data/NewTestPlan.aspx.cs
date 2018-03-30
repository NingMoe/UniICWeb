using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class searchAccount : UniPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";
        UNITESTPLAN newTestPlan = new UNITESTPLAN();
        GetHTTPObj(out newTestPlan);
        UNIACCOUNT accTeacher = new UNIACCOUNT();
        if (GetAccByAccno(newTestPlan.dwTeacherID.ToString(), out accTeacher))
        {
            newTestPlan.szTestPlanName = accTeacher.szTrueName + "_" + newTestPlan.szCourseName;
        }
        else {
            Response.Write("错误:" +"教师信息不能为空");
            return;
        }
        if (Session["ClassGroupID"] != null && Session["ClassGroupID"].ToString() != "")
        {
            newTestPlan.dwGroupID = Parse(Session["ClassGroupID"].ToString());
        }

        if ((newTestPlan.dwKind & (uint)UNITESTPLAN.DWKIND.TESTPLANKIND_OPEN) > 0)
        {
          UNITERM[] selectTerm=GetTermByID((uint)newTestPlan.dwYearTerm);
          if (selectTerm != null && selectTerm.Length > 0)
          {
              UNIGROUP groupClass = new UNIGROUP();
              if (NewGroup(newTestPlan.szCourseName + "-" + newTestPlan.szTeacherName, (uint)UNIGROUP.DWKIND.GROUPKIND_RERV, out groupClass, (uint)selectTerm[0].dwEndDate))
              {
                  newTestPlan.dwGroupID = groupClass.dwGroupID;
              }
              else {
                  Response.Write("错误:" +m_Request.szErrMessage);
                  return;
              }
          }
          else
          {
              Response.Write("错误:" + "找不到对应学期信息");
              return;
          }
        }
        newTestPlan.dwTheoryHour = 99;
        newTestPlan.dwPracticeHour = 100;
        newTestPlan.dwTestNum= 101;
        REQUESTCODE uResponse=m_Request.Reserve.SetTestPlan(newTestPlan, out newTestPlan);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            Session["ClassGroupID"] = null; 
            
            if (ConfigConst.GCscheduleMode <= 2)//新建计划同时新建项目
            {
                TESTCARD newTestCard = new TESTCARD();
                newTestCard.dwGroupPeopleNum = 1;
                newTestCard.szTestName = newTestPlan.szTestPlanName;
                newTestCard.szMemo = newTestPlan.szMemo; ;
                newTestCard.dwTestClass = 1;
                newTestCard.dwTestKind = 1; ;
                newTestCard.dwTestHour = newTestPlan.dwTestHour; ;

                uResponse = REQUESTCODE.EXECUTE_SUCCESS;//testCARD不用管//m_Request.Reserve.SetTestCard(newTestCard, out newTestCard);
                if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    UNITESTITEM testItem = new UNITESTITEM();
                    testItem.dwGroupPeopleNum = 1;
                    testItem.dwTestPlanID = newTestPlan.dwTestPlanID;
                    testItem.szTestPlanName = newTestPlan.szTestPlanName;
                    testItem.szMemo = newTestPlan.szMemo; ;
                    testItem.dwTestClass = 1;
                    testItem.dwTestKind = 1; ;
                    testItem.dwTestHour = newTestPlan.dwTestHour; ;
                    testItem.szTestName = newTestPlan.szTestPlanName;
                    uResponse = m_Request.Reserve.SetTestItem(testItem, out testItem);
                    if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
                    {
                        Response.Write("testitemid:" + testItem.dwTestItemID + "," + newTestCard.dwTestCardID);
                        return;
                    }
                    else
                    {
                        if (m_Request.szErrMessage != null)
                        {
                            string szError = m_Request.szErrMessage.ToString();
                            m_Request.Reserve.DelTestCard(newTestCard);
                            Response.Write("错误:" + szError);
                            return;
                        }
                        else
                        {
                            Response.Write("错误:登陆超时");
                            return;
                        }
                    }
                }
                else {
                    string szError = m_Request.szErrMessage.ToString();
                    Response.Write("错误:" + szError);
                    return;
                }
            }
            Response.Write(newTestPlan.dwTestPlanID.ToString());
        }
        else
        {
            if (m_Request.szErrMessage != null)
            {
                Response.Write("错误:" + m_Request.szErrMessage.ToString());
            }
            else
            {
                Response.Write("错误:登陆超时");
            }
        }
    }
        
}