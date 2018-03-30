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
        szIdent = GetInputHtmlFromXml(0, CONSTHTML.option, "", "ACCNO_IDENT", true);
        szSex= GetInputHtmlFromXml(0, CONSTHTML.option, "", "ACCNO_sex", true);
        if (IsPostBack)
        {
            int nCard = IntParse(Request["dwCardID"]);
            //UInt32 ucard32 = UInt32.Parse(Convert.ToString(nCard, 8));
            GetHTTPObj(out newAccno);
            newAccno.szPID = newAccno.szLogonName;
            //  newAccno.dwCardID = newAccno.dwCardID;
            newAccno.dwCardID = (uint)nCard;
            newAccno.szPasswd = "P" + newAccno.szPID;
            newAccno.szCardNo = newAccno.szLogonName;
            newAccno.dwMajorID = 0;
            if (m_Request.Account.Set(newAccno, out newAccno) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "设置账户失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox("设置账户成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
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
            m_Title = "新建本地";

        }
    }
}
