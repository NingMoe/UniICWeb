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
        if (Session["ClassGroupID"] != null && Session["ClassGroupID"].ToString() != "")
        {
            newTestPlan.dwGroupID = Parse(Session["ClassGroupID"].ToString());
        }
        REQUESTCODE uResponse=m_Request.Reserve.SetTestPlan(newTestPlan, out newTestPlan);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            Session["ClassGroupID"] = null; 
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