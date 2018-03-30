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

        string szTerm = Request["term"];
        if (szTerm == null) szTerm = "";

        Response.CacheControl = "no-cache";

        TESTPLANREQ vrGetCls = new TESTPLANREQ();
        UNITESTPLAN[] vtCls;
        vrGetCls.dwGetType = (uint)TESTPLANREQ.DWGETTYPE.TESTPLANGET_BYALL;
        vrGetCls.dwYearTerm = 20131401;
        //vrGetCls.dwGetType = (uint)TESTPLANREQ.DWGETTYPE.TESTPLANGET_BYNAME;
        //vrGetCls.szGetKey = szTerm;
        vrGetCls.szReqExtInfo.dwNeedLines = 10; //最多10条

        if (m_Request.Reserve.GetTestPlan(vrGetCls, out vtCls) == REQUESTCODE.EXECUTE_SUCCESS && vtCls != null)
        {
            MyString szOut = new MyString();
            szOut += "[";
            for (int i = 0; i < vtCls.Length; i++)
            {
                szOut += "{\"id\":\"" + vtCls[i].dwTestPlanID + "\",\"label\": \"" + vtCls[i].szTestPlanName + "\"}";
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