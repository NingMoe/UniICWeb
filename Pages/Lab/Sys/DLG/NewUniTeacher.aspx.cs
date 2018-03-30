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
        UNITEACHER newValue;
       
        if (IsPostBack)
        {
            GetHTTPObj(out newValue);

            if (m_Request.Account.TeacherSet(newValue, out newValue) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "设置教师失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                UNIACCOUNT accinfo = new UNIACCOUNT();
                if (GetAccByAccno(newValue.dwAccNo.ToString(), out accinfo))
                {
                    accinfo.dwIdent = accinfo.dwIdent | (uint)UNIACCOUNT.DWIDENT.EXTIDENT_TEACHER;
                    REQUESTCODE uResponse = m_Request.Account.Set(accinfo, out accinfo);
                }
                MessageBox("设置教师成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
        }
        if (Request["op"] == "set")
        {
            bSet = true;

            UNITEACHERREQ vrGet = new UNITEACHERREQ();
            vrGet.dwAccNo = Parse(Request["dwID"]);
            UNITEACHER[] vtRes;
            if (m_Request.Account.TeacherGet(vrGet, out vtRes) != REQUESTCODE.EXECUTE_SUCCESS)
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
                    m_Title = "修改【" + vtRes[0].szTrueName + "】";
                }
            }
        }
        else
        {
            m_Title = "教师";

        }
    }
}
