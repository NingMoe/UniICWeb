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
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        Response.CacheControl = "no-cache";
        string szTestName = Request["szTestName"];
        uint dwGroupPeopleNum = Parse(Request["dwGroupPeopleNum"]);
        uint dwTestItemTestHour = Parse(Request["dwTestItemTestHour"]);
        string szTestItemMemo = Request["szTestItemMemo"];
        uint dwTestClass = Parse(Request["dwTestClass"]);
        uint dwTestKind = Parse(Request["dwTestKind"]);


        string testitemid = (Request["testitemid"]);
        TESTITEMREQ vrGetCls = new TESTITEMREQ();
        if (testitemid != null && testitemid != "")
        {
            vrGetCls.dwGetType = (uint)TESTITEMREQ.DWGETTYPE.TESTITEMGET_BYID;
            vrGetCls.szGetKey = testitemid;
        }
        UNITESTITEM[] vtCls;

        vrGetCls.szReqExtInfo.dwNeedLines = 10; //最多10条

        if (m_Request.Reserve.GetTestItem(vrGetCls, out vtCls) == REQUESTCODE.EXECUTE_SUCCESS && vtCls != null && vtCls.Length > 0)
        {
            UNITESTITEM testItem = new UNITESTITEM();
            testItem = vtCls[0];
            testItem.szTestName = szTestName;

            testItem.dwGroupPeopleNum = dwGroupPeopleNum;
            testItem.szTestName = szTestName;
            testItem.szMemo = szTestItemMemo;
            testItem.dwTestClass = dwTestClass;
            testItem.dwTestKind = dwTestKind;
            testItem.dwTestHour = dwTestItemTestHour;


            uResponse = m_Request.Reserve.SetTestItem(testItem, out testItem);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
            {
                TESTCARD newTestCard = new TESTCARD();
                newTestCard.dwTestCardID = vtCls[0].dwTestCardID;
               
                Response.Write("success");
                /*testcard不用管
                if (m_Request.Reserve.SetTestCard(newTestCard, out newTestCard) == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    Response.Write("success");
                }
                else
                {
                    string szError = m_Request.szErrMessage.ToString();

                    Response.Write("错误:" + szError);
                    return;
                }
                 * */
            }
            else
            {
                if (m_Request.szErrMessage != null)
                {
                    string szError = m_Request.szErrMessage.ToString();

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
            Response.Write("错误:获取数据失败");
        }

    }

}