using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class searchCourse : UniClientAjax
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string term = Request["term"];

        Response.CacheControl = "no-cache";

        COURSEREQ vrGetCls = new COURSEREQ();
        UNICOURSE[] vtCls;
        vrGetCls.szCourseCode = term;
        vrGetCls.szReqExtInfo.szOrderKey = "szCourseName";
        vrGetCls.szReqExtInfo.szOrderMode = "ASC";
        vrGetCls.szReqExtInfo.dwNeedLines = 10; //最多10条
        REQUESTCODE cd = m_Request.Reserve.GetCourse(vrGetCls, out vtCls);
        if (cd == REQUESTCODE.EXECUTE_SUCCESS && vtCls != null)
        {
            if (vtCls.Length == 0)
            {
                vrGetCls.szCourseCode = null;
                vrGetCls.szCourseName = term;
                if (m_Request.Reserve.GetCourse(vrGetCls, out vtCls) != REQUESTCODE.EXECUTE_SUCCESS)
                {
                    Response.Write("[ ]");
                    return;
                }
            }
            List<course> list = new List<course>();
            for (int i = 0; i < vtCls.Length; i++)
            {
                course cs = new course();
                cs.id = vtCls[i].dwCourseID.ToString();
                cs.name = vtCls[i].szCourseName;
                cs.code = vtCls[i].szCourseCode;
                cs.label = vtCls[i].szCourseName+"("+vtCls[i].szCourseCode+")";
                cs.dept = vtCls[i].szDeptName;
                cs.type = vtCls[i].szType;
                cs.testnum = vtCls[i].dwTestNum;
                cs.period = vtCls[i].dwTheoryHour;
                cs.testhour = vtCls[i].dwTestHour;
                list.Add(cs);
            }
            Response.Write(CodeJson(list.ToArray()));
        }
        else
        {
            Response.Write("[ ]");
        }
    }
    struct course
    {
        public string id;
        public string name;
        public string label;
        public string code;
        public string type;
        public string dept;
        public uint? testnum;
        public uint? period;
        public uint? testhour;
    }
}