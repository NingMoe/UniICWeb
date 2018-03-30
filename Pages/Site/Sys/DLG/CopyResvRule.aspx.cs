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
    protected string m_szDevKind = "";
    protected string m_szDevCls= "";
	protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            
        }
        UNIDEVKIND[] vtDevKind= GetAllDevKind();
        for (int i = 0; i < vtDevKind.Length; i++)
        {
            m_szDevKind += GetInputItemHtml(CONSTHTML.checkBox, "dwKindID", vtDevKind[i].szKindName, vtDevKind[i].dwKindID.ToString());
        }
        UNIDEVCLS[] vtDevCls = GetAllDevCls();
        for (int i = 0; i < vtDevCls.Length; i++)
        {
            m_szDevCls += GetInputItemHtml(CONSTHTML.checkBox, "dwClsID", vtDevCls[i].szClassName, vtDevCls[i].dwClassID.ToString());
        }
        if (IsPostBack)
        {
            RESVRULEREQ vrGet = new RESVRULEREQ();
            vrGet.dwRuleSN = Parse(Request["dwID"]);
            uint uSN = Parse(Request["sn"]);
            UNIRESVRULE[] vtRes;
            REQUESTCODE uResponse = m_Request.Reserve.ResvRuleGet(vrGet, out vtRes);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
            {

                string szDevKind = Request["dwClsID"];
                string[] szDevKindList = szDevKind.Split(',');
                for (int i = 0; i < szDevKindList.Length; i++)
                {
                    if (szDevKindList[i] == null || szDevKindList[i] == "")
                    {
                        continue;
                    }
                    UNIRESVRULE setResvRule= vtRes[0];
                    UNIDEVCLS devKind;
                    if(GetDevCLSByID(szDevKindList[i],out devKind))
                    {
                        setResvRule.szRuleName = devKind.szClassName.ToString() + "预约规则";
                    }
                    setResvRule.dwRuleSN = null;
                    setResvRule.dwDevClass = Parse(szDevKindList[i]);
                    m_Request.Reserve.ResvRuleSet(setResvRule, out setResvRule);
                    uSN=uSN+1;
                }
            }
            MessageBox("复制完毕", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
            return;
        }
        }
}
