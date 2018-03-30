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
    protected string m_szCTS = "";
    protected string m_szCTSN = "";
    protected string szop = "扣除";
	protected void Page_Load(object sender, EventArgs e)
    {
      
        CREDITSCORE newObject;
        
        if (IsPostBack)
        {
            GetHTTPObj(out newObject);
            if ((uint)newObject.dwID == 0)
            {
                newObject.dwID = null;
            }
            if (m_Request.System.CreditScoreSet(newObject, out newObject) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "设置信用失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox("设置信用成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
        }
        if (Parse(Request["dwCreditSN"]) == (uint)CREDITKIND.DWCREDITSN.CREDIT_NORMALUSE)
        {
            szop="奖励";
        }
        m_szCTS = GetAllInputHtml(CONSTHTML.option, "", "CREDITKIND");
        
        CREDITTYPEREQ vrParameter = new CREDITTYPEREQ();
        CREDITTYPE[] vrResult;

        if (m_Request.System.CreditTypeGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null && vrResult.Length > 0)
        {
            for(int i=0;i<vrResult.Length;i++)
            {
                m_szCTSN += GetInputItemHtml(CONSTHTML.option,"", vrResult[i].szCTName.ToString(), vrResult[i].dwCTSN.ToString());
            }
        }
     
        if (Request["op"] == "set")
        {
            CREDITSCOREREQ vrGet = new CREDITSCOREREQ();
            CREDITSCORE[] vtRes;
            vrGet.dwID=Parse(Request["dwID"]);
            m_Request.System.CreditScoreGet(vrGet, out vtRes);
            if (vtRes != null && vtRes.Length > 0)
            {
                PutHTTPObj(vtRes[0]);
           }
        }
        else
        {
           
        }
        
    }
}
