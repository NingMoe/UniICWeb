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
    protected string m_szIdent = "";
    protected string m_szDevKind = "";
    protected string m_szResvPurpose= "";
    protected string m_dwQuotaRule = "";
    protected string m_dwPriority = "";
	protected void Page_Load(object sender, EventArgs e)
    {
        UNIFEE newFee;
        uint? uMax = 0;
        uint uID = PRDevice.DEVICE_BASE | PRDevice.MSREQ_LAB_SET;
        if (GetMaxValue(ref uMax, uID, "dwLabID"))
        {

        }
        if (IsPostBack)
        {
            GetHTTPObj(out newFee);
            newFee.dwPurpose = CharListToUint(Request["dwPurpose"]);
            if (m_Request.Fee.Set(newFee, out newFee) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "新建收费标准失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox("新建收费标准成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
        }
        m_dwQuotaRule = GetAllInputHtml(CONSTHTML.option, "", "Fee_QuotaRule");
        m_szIdent = GetAllInputHtml(CONSTHTML.option, "", "Fee_Ident");
        UNIDEVKIND[] vtDevKind = GetAllDevKind();
        m_szDevKind += "<option value='" + "0" + "'>" + "全部适用" + "</option>";
        if (vtDevKind != null && vtDevKind.Length > 0)
        {
          
            for (int i = 0; i < vtDevKind.Length; i++)
            {
                m_szDevKind += "<option value='" + vtDevKind[i].dwKindID + "'>" + vtDevKind[i].szKindName + "</option>";
            }
        }
        m_szResvPurpose = GetAllInputHtml(CONSTHTML.checkBox, "dwPurpose", "ResvPurpose");
        m_dwPriority = GetAllInputHtml(CONSTHTML.option, "", "Priority");
        if (Request["op"] == "set")
        {
           
        }
        else
        {
           
        }
    }
}
