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
    protected string m_szDept = "";
    protected string m_szLabKind = "";
	protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack)
        {
            string szFeeDetail = Request["szFunc"];
            USERFEEDBACKREQ vrGet = new USERFEEDBACKREQ();
            vrGet.dwSNum = Parse(Request["dwSNum"]);
            USERFEEDBACK[] vtRes;
            if (m_Request.Admin.GetUserFeedback(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
            {
                vtRes[0].szReplyInfo = szFeeDetail;
                REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
                uResponse = m_Request.Admin.ReplyUserFeedback(vtRes[0]);
                if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    MessageBox("回复成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);

                }
                else
                {
                    MessageBox(m_Request.szErrMessage, "回复失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                }
            }
            else
            {
                MessageBox("获取不到信息", "回复失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
        }
        else
        {
            USERFEEDBACKREQ vrGet = new USERFEEDBACKREQ();
            vrGet.dwSNum = Parse(Request["dwSNum"]);
            USERFEEDBACK[] vtRes;
            if (m_Request.Admin.GetUserFeedback(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
            {
                PutHTTPObj(vtRes[0]);
            }
        }
    }
}
