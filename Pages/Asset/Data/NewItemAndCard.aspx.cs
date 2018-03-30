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

        uint uTestPlanID = Parse(Request["testPlanID"]);
        string szTestName = Request["szTestName"];
        uint dwGroupPeopleNum = Parse(Request["dwGroupPeopleNum"]);
        uint dwTestItemTestHour = Parse(Request["dwTestItemTestHour"]);
        string szTestItemMemo = Request["szTestItemMemo"];
        uint dwTestClass = Parse(Request["dwTestClass"]);
        uint dwTestKind = Parse(Request["dwTestKind"]);
        TESTCARD newTestCard = new TESTCARD();
        newTestCard.dwGroupPeopleNum = dwGroupPeopleNum;
        newTestCard.szTestName = szTestName;
        newTestCard.szMemo = szTestItemMemo;
        newTestCard.dwTestClass = dwTestClass;
        newTestCard.dwTestKind = dwTestKind;
        newTestCard.dwTestHour = dwTestItemTestHour;

        REQUESTCODE uResponse = m_Request.Reserve.SetTestCard(newTestCard, out newTestCard);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            UNITESTITEM testItem = new UNITESTITEM();
            testItem.dwTestPlanID = uTestPlanID;
            testItem.dwTestCardID = newTestCard.dwTestCardID;
            testItem.szTestName = szTestName;
            uResponse = m_Request.Reserve.SetTestItem(testItem, out testItem);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
            {
                Response.Write(testItem.dwTestItemID + "," + newTestCard.dwTestCardID);
                return;
            }
            else
            {
                if (m_Request.szErrMessage != null)
                {
                    string szError= m_Request.szErrMessage.ToString();
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