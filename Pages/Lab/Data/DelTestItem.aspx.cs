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
       
        Response.CacheControl = "no-cache";

        TESTITEMREQ vrGetCls = new TESTITEMREQ();
        if (szTestItem != null && szTestItem != "")
        {
            vrGetCls.dwGetType = (uint)TESTITEMREQ.DWGETTYPE.TESTITEMGET_BYID;
            vrGetCls.szGetKey = szTestItem;
        }
        UNITESTITEM[] vtCls;
        
        vrGetCls.szReqExtInfo.dwNeedLines = 10; //最多10条

        if (m_Request.Reserve.GetTestItem(vrGetCls, out vtCls) == REQUESTCODE.EXECUTE_SUCCESS && vtCls != null && vtCls.Length>0)
        {

            UNITESTITEM testitem = new UNITESTITEM();
            testitem.dwTestItemID = vtCls[0].dwTestItemID;
            if (m_Request.Reserve.DelTestItem(testitem) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                Response.Write("success");
                return;
                /*
                TESTCARD testcard = new TESTCARD();
                testcard.dwTestCardID = vtCls[0].dwTestCardID;
                if (m_Request.Reserve.DelTestCard(testcard) == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    
                }
                else
                {
                    Response.Write("错误:" + m_Request.szErrMessage);
                }
                 * */
            }
            else
            {
                Response.Write("错误:" + m_Request.szErrMessage);
            }
            Response.Write("success");
        }
        else
        {
            Response.Write("错误:" + m_Request.szErrMessage);
        }
    }
}