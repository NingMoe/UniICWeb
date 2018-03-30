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
    protected void Page_Load(object sender, EventArgs e)
    {
        m_Title = "加减补助";
        if (IsPostBack)
        {
            string[] szLogonNameList = Request["szLogonName"].ToString().Split(',');
            uint uMoney = Parse(Request["money"]);
            uint uKind = Parse(Request["dwKind"]);
            uint uCount=0;
            for (int i = 0; i < szLogonNameList.Length; i++)
            {
                
                UNIACCOUNT acc = new UNIACCOUNT();
                if (GetAccByLogonName(szLogonNameList[i], out acc))
                {
                    UNIDEPOSIT setValue = new UNIDEPOSIT();
                    setValue.dwAccNo = acc.dwAccNo;
                    setValue.szPID = acc.szPID;
                    setValue.szTrueName = acc.szTrueName;
                    setValue.dwKind = uKind;
                  
                    setValue.dwAmount = uMoney * 100;
                    if (uKind == (uint)UNIDEPOSIT.DWKIND.DPTKIND_SUBSIDYCLEAR)
                    {
                        m_Title = "补助清零";
                        setValue.dwAmount = 0;
                    }
                    else if (uKind == (uint)UNIDEPOSIT.DWKIND.DPTKIND_SUBSIDYADD)
                    {
                        m_Title = "加补助";
                    }
                    else {
                        m_Title = "减补助";
                    }
                    if (m_Request.Account.Deposit(setValue) == REQUESTCODE.EXECUTE_SUCCESS)
                    {
                        uCount = uCount + 1;
                    }
                }
            }
            if (uCount > 0)
            {
                MessageBox(m_Title + "成功【"+uCount+"】人", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.CANCEL);
            }
            else
            {
                MessageBox(m_Request.szErrMessage, "提示", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            return;
        }
        
    }
}