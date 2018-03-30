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

public partial class _Default : UniPage
{
    protected bool bSet = false;
    protected string m_Title = "";
    protected string m_Property = "";
	protected void Page_Load(object sender, EventArgs e)
    {
        UNICOURSE newCourse;
        uint? uMax = 0;
        uint uID = PRDevice.DEVICE_BASE | PRDevice.MSREQ_LAB_SET;
        if (GetMaxValue(ref uMax, uID, "dwLabID"))
        {

        }
        if (IsPostBack)
        {
            GetHTTPObj(out newCourse);
            if (m_Request.Reserve.SetCourse(newCourse, out newCourse) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "修改课程失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox("修改课程成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
        }
        m_Property = GetAllInputHtml(CONSTHTML.option, "", "Course_Property");
        if (Request["op"] == "set")
        {
            bSet = true;

            COURSEREQ vrGet= new COURSEREQ();
            vrGet.dwCourseID = Parse(Request["dwID"]);
            UNICOURSE[] vtRes;
            if (m_Request.Reserve.GetCourse(vrGet, out vtRes) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else
            {
                if (vtRes.Length == 0)
                {
                    MessageBox("获取失败", "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                }
                else
                {
                    PutJSObj(vtRes[0]);
                    m_Title = "修改课程【" + vtRes[0].szCourseName + "】";
                }
            }
        }
        else
        {
            m_Title = "新建课程";

        }
    }
}
