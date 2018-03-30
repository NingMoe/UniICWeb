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
    protected string szIdent = "";
    protected string szSex = "";
	protected void Page_Load(object sender, EventArgs e)
    {
        UNIACCOUNT newAccno;
        if (IsPostBack)
        {
            string szPasswd1 = Request["szPasswd1"];
            string szPasswd2 = Request["szPasswd2"];
            if (szPasswd1 != szPasswd2)
            {
                MessageBox("两次密码输入不一致", "提示", MSGBOX.ERROR);
                return;
            }
            GetHTTPObj(out newAccno);
            if (GetAccByAccno(newAccno.dwAccNo.ToString(),out newAccno))
            {
                newAccno.szPasswd = "P" + szPasswd2;
                if (m_Request.Account.Set(newAccno, out newAccno) != REQUESTCODE.EXECUTE_SUCCESS)
                {
                    MessageBox(m_Request.szErrMessage, "重置密码失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                }
                else
                {
                    MessageBox("重置密码成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                    return;
                }
            }
        }
        if (Request["op"] == "set")
        {
            bSet = true;

            ACCREQ vrGet = new ACCREQ();
            vrGet.dwAccNo = Parse(Request["dwID"]);
            UNIACCOUNT[] vtRes;
            if (m_Request.Account.Get(vrGet, out vtRes) != REQUESTCODE.EXECUTE_SUCCESS)
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
            m_Title = "重置密码";

        }
    }
}
