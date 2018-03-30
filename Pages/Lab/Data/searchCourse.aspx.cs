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
        string szCode = Request["Code"];
        Response.CacheControl = "no-cache";

        COURSEREQ vrGetCls = new COURSEREQ();
        UNICOURSE[] vtCls;
        if (szTerm != null && szTerm != "")
        {
            vrGetCls.szCourseName = szTerm;
        }
        if (szCode != null && szCode != "")
        {
            vrGetCls.szCourseCode = szCode;
        }
        vrGetCls.szReqExtInfo.dwNeedLines = 10; //最多10条

        if (m_Request.Reserve.GetCourse(vrGetCls, out vtCls) == REQUESTCODE.EXECUTE_SUCCESS && vtCls != null)
        {
            MyString szOut = new MyString();
            szOut += "[";
            for (int i = 0; i < vtCls.Length; i++)
            {
                szOut += "{\"id\":\"" + vtCls[i].dwCourseID + "\",\"label\": \"" + vtCls[i].szCourseName + "\",\"testhour\": \"" + vtCls[i].dwTestHour + "\",\"code\": \"" + vtCls[i].szCourseCode + "\"}";
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