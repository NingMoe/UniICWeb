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

        if (m_Request.Reserve.GetTestItem(vrGetCls, out vtCls) == REQUESTCODE.EXECUTE_SUCCESS && vtCls != null)
        {
            string szOut=Newtonsoft.Json.JsonConvert.SerializeObject(vtCls);  
            Response.Write(szOut);
        }
        else
        {
            Response.Write("");
        }
    }
}