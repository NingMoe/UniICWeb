using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_all_courselist : UniClientPage
{
    protected string infoIntro = "";
    protected string infoTitle = "课程列表";
    protected string courseList = "";
    protected string deptList = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string name = Request["deptName"];
        string id = Request["deptId"];

        if (!string.IsNullOrEmpty(name))
        {
            infoTitle = Server.UrlDecode(name);
            deptName.Value = infoTitle;
        }
        if (!string.IsNullOrEmpty(id))
            deptId.Value = id;
        InitDept();
        InitCourse(deptId.Value);
    }

    private void InitDept()
    {
        DEPTREQ req = new DEPTREQ();
        UNIDEPT[] rlt;
        if (m_Request.Account.DeptGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            deptList = "<option value='0'>全部</option>";
            for (int i = 0; i < rlt.Length; i++)
            {
                deptList += "<option value='"+rlt[i].dwID+"'>"+rlt[i].szName+"</option>";
            }
        }
    }

    private void InitCourse(string dept)
    {
        COURSEREQ req = new COURSEREQ();
        if (!string.IsNullOrEmpty(dept))
            req.dwOwnerDept = ToUInt(dept);
        req.szReqExtInfo.szOrderKey = "szCourseName";
        req.szReqExtInfo.szOrderMode = "ASC";
        UNICOURSE[] rlt;
        if (m_Request.Reserve.GetCourse(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                UNICOURSE cu = rlt[i];
                courseList += "<li class='it click_load' dept=" + cu.dwOwnerDept + "  url=\"../a/coursedetail.aspx?courseId=" + cu.dwCourseID + "\" cache='#cache_con' con='#detail_con' class=\"click_load\" title=\"点击查看详情\"><div class='title'>" + cu.szCourseName + "</div></li>";
            }
        }
    }
}