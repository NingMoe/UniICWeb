using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
public partial class searchCourse : UniWebLib.UniPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        m_bRemember = false;

        string szTestItem = Request["testItemID"];
        string szTestPlan = Request["testPlanID"];
        
        Response.CacheControl = "no-cache";

        TESTITEMREQ vrGetCls = new TESTITEMREQ();
        if (szTestItem != null && szTestItem != "")
        {
            vrGetCls.dwGetType = (uint)TESTITEMREQ.DWGETTYPE.TESTITEMGET_BYID;
            vrGetCls.szGetKey = szTestItem;
        }
        if (szTestPlan != null && szTestPlan != "")
        {
            vrGetCls.dwGetType = (uint)TESTITEMREQ.DWGETTYPE.TESTITEMGET_BYPLANID;
            vrGetCls.szGetKey = szTestPlan;
        }
        UNITESTITEM[] vtCls;
        
        vrGetCls.szReqExtInfo.dwNeedLines = 10; //最多10条

        if (m_Request.Reserve.GetTestItem(vrGetCls, out vtCls) == REQUESTCODE.EXECUTE_SUCCESS && vtCls != null)
        {
            MyString szOut = new MyString();
            szOut += "[";
            for (int i = 0; i < vtCls.Length; i++)
            {
                
                szOut += "{\"id\":\"" + vtCls[i].dwTestItemID + "\",\"label\": \"" + vtCls[i].szTestName + "\",\"dwTestKind\": \"" + vtCls[i].dwTestKind + "\",\"dwTestClass\": \"" + vtCls[i].dwTestClass + "\",\"dwGroupPeopleNum\": \"" + vtCls[i].dwGroupPeopleNum + "\",\"dwTestHour\": \"" + vtCls[i].dwTestHour + "\",\"szMemo\": \"" + vtCls[i].szMemo + "\",\"szTestName\": \"" + vtCls[i].szTestName + "\"}";
                if (i < vtCls.Length - 1)
                {
                    szOut += ",";
                }
            }
            szOut += "]";
            Response.Write(szOut);
        }
        else
        {
            Response.Write("[ ]");
        }
    }
}