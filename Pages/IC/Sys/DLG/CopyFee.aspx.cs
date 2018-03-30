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
        if (IsPostBack)
        {
            FEEREQ vrGet = new FEEREQ();
            vrGet.dwFeeSN = Parse(Request["dwID"]);
            uint uSN = Parse(Request["sn"]);
            UNIFEE[] vtRes;
            REQUESTCODE uResponse = m_Request.Fee.Get(vrGet, out vtRes);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
            {

                string szDevKind = Request["dwKindID"];
                string[] szDevKindList = szDevKind.Split(',');
                for (int i = 0; i < szDevKindList.Length; i++)
                {
                    if (szDevKindList[i] == null || szDevKindList[i] == "")
                    {
                        continue;
                    }
                     UNIFEE setFee = vtRes[0];
                    UNIDEVKIND devKind;
                    if(GetDevKindByID(szDevKindList[i],out devKind))
                    {
                        setFee.szFeeName = devKind.szKindName.ToString() + "计费规则" + GetJustNameEqual(vtRes[0].dwIdent, "Fee_Ident");
                    }
                   
                    setFee.dwFeeSN = (uSN+1);
                    setFee.dwDevKind = Parse(szDevKindList[i]);
                    m_Request.Fee.Set(setFee, out setFee);
                    uSN=uSN+1;
                }
            }
            MessageBox("复制完毕", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
            return;
        }
        }
}
