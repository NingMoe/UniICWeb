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
    protected string m_szCTS= "";
    protected string m_szResvPurpose= "";
    protected string m_szForClsKind = "";
    protected string m_szTitle = "";
	protected void Page_Load(object sender, EventArgs e)
    {
        CREDITTYPE newCreditKind;
        string szOP = "新建";
        if (Request["op"] == "set")
        {
            szOP = "修改";
        }
        if (IsPostBack)
        {
            GetHTTPObj(out newCreditKind);
            newCreditKind.dwUsePurpose = CharListToUint(Request["dwUsePurpose"]);
            newCreditKind.dwCTStat = (uint)CREDITTYPE.DWCTSTAT.CTSTAT_INUSE;
            if (m_Request.System.CreditTypeSet(newCreditKind) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, szOP+"信用失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox(szOP+"信用成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
        }
        m_szCTS = GetAllInputHtml(CONSTHTML.option, "", "CREDITKIND");
        m_szResvPurpose = GetAllInputHtml(CONSTHTML.checkBox, "dwUsePurpose", "ResvPurpose");
        m_szForClsKind = GetAllInputHtml(CONSTHTML.option, "", "DevClass_dwKind");
        if (Request["op"] == "set")
        {
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            CREDITTYPEREQ vrGet = new CREDITTYPEREQ();
            vrGet.dwCTSN = Parse(Request["dwID"]);
            CREDITTYPE[] vtRes;
            uResponse = m_Request.System.CreditTypeGet(vrGet, out vtRes);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
            {
                PutHTTPObj(vtRes[0]);
            }
            m_szTitle = "修改";
        }
        else
        {
            m_szTitle = "新建";
        }
        
    }
}
