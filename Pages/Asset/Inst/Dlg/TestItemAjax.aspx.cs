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
using System.Collections.Specialized;

public partial class _Default : UniWebLib.UniPage
{
    
	protected void Page_Load(object sender, EventArgs e)
	{
        m_bRemember = false;
        Response.CacheControl = "no-cache";

        string szOP = Request["op"];
        if (szOP == "del")
        {
            UNITESTITEM delItem = new UNITESTITEM();
            GetHTTPObj(out delItem);
            if (m_Request.Reserve.DelTestItem(delItem) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                Response.Write("Error");
                return;
            }

            TESTCARD delCard = new TESTCARD();
            delCard.dwTestCardID = delItem.dwTestCardID;
            if (m_Request.Reserve.DelTestCard(delCard) != REQUESTCODE.EXECUTE_SUCCESS)
            {
            }
            Response.Write("OK");
            return;
        }
        else if (szOP == "set")
        {
            TESTCARD vrTestCard = new TESTCARD();
            GetHTTPObj(out vrTestCard);
            if (vrTestCard.dwTestCardID == 0) { vrTestCard.dwTestCardID = null; }

            TESTCARD vrTestCardRet;
            if (m_Request.Reserve.SetTestCard(vrTestCard, out vrTestCardRet) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                UNITESTITEM vrTestItem = new UNITESTITEM();
                UNITESTITEM vrTestItemResult;
                vrTestItem.dwTestItemID = ToUint(Request["dwTestItemID"]); if (vrTestItem.dwTestItemID == 0) { vrTestItem.dwTestItemID = null; }
                vrTestItem.dwTestPlanID = ToUint(Request["dwTestPlanID"]);
                vrTestItem.szTestPlanName = Request["szTestPlanName"];
                vrTestItem.dwTotalTestHour = vrTestCardRet.dwTestHour;
                //vrTestItem.dwTeacherID = ((ADMINLOGINRES)Session["LoginResult"]).AdminInfo.dwAccNo;
                vrTestItem.dwTestCardID = vrTestCardRet.dwTestCardID;
                vrTestItem.dwCourseID = ToUint(Request["dwCourseID"]);
                vrTestItem.dwGroupID = ToUint(Request["dwGroupID"]);
                vrTestItem.szMemo = vrTestCardRet.szMemo;

                if (m_Request.Reserve.SetTestItem(vrTestItem, out vrTestItemResult) == REQUESTCODE.EXECUTE_SUCCESS)
                {

                }
                else
                {
                    Response.Write("Error");
                    return;
                }
            }
            else
            {
                Response.Write("Error");
                return;
            }
            Response.Write("OK");
            return;
        }
        else
        {

            Response.Write("Error");
        }
	}
}
