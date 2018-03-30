using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class searchCourse : UniPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        m_bRemember = false;

        string szTerm = Request["term"];
        string szUniTerm=Request["uniterm"];
		string TestPlanID=Request["TestPlanID"];
        if (szTerm == null)
        {
            szTerm = "";
        }
        string szTearche = Request["TeacherID"];
       
        Response.CacheControl = "no-cache";

        TESTPLANREQ vrGetCls = new TESTPLANREQ();
        UNITESTPLAN[] vtCls;
        vrGetCls.dwGetType = (uint)TESTPLANREQ.DWGETTYPE.TESTPLANGET_BYALL;
        if (szTearche != null && szTearche != "")
        {
            vrGetCls.dwGetType = (uint)TESTPLANREQ.DWGETTYPE.TESTPLANGET_BYTEACHER;
            vrGetCls.szGetKey = szTearche;
        }
		if(TestPlanID!=null&&TestPlanID!="")
		{
			 vrGetCls.dwGetType = (uint)TESTPLANREQ.DWGETTYPE.TESTPLANGET_BYID;
            vrGetCls.szGetKey = TestPlanID;
		}
        UNITERM[] termList=GetTermNow();
        if(termList!=null&&termList.Length>0)
        {
            vrGetCls.dwYearTerm = termList[0].dwYearTerm;
        }
        uint uTerm=Parse(szUniTerm);
        if (uTerm != 0)
        {
            vrGetCls.dwYearTerm = uTerm;
        }
        //vrGetCls.dwGetType = (uint)TESTPLANREQ.DWGETTYPE.TESTPLANGET_BYNAME;
        //vrGetCls.szGetKey = szTerm;
        vrGetCls.szReqExtInfo.dwNeedLines = 10; //最多10条
        vrGetCls.szReqExtInfo.dwStartLine = 0;
        if (m_Request.Reserve.GetTestPlan(vrGetCls, out vtCls) == REQUESTCODE.EXECUTE_SUCCESS && vtCls != null)
        {
            MyString szOut = new MyString();
            szOut += "[";
            for (int i = 0; i < vtCls.Length; i++)
            {
                szOut += "{\"id\":\"" + vtCls[i].dwTestPlanID  + "\",\"szTeacherName\": \"" + vtCls[i].szTeacherName+ "\",\"dwTeacherID\": \"" + vtCls[i].dwTeacherID+ "\",\"label\": \"" + vtCls[i].szTestPlanName + "\"}";
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