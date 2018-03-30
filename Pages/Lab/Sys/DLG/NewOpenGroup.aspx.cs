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
    private uint uKind = (uint)UNIGROUP.DWKIND.GROUPKIND_OPENRULE;
    protected string m_Title = "";
    protected string m_szSta = "";
    protected string szIdent = "";
    protected string szGroupMember = "";
	protected void Page_Load(object sender, EventArgs e)
	{       
        if (IsPostBack)
        {
            UNIGROUP newGroup;
            GetHTTPObj(out newGroup);
            string szGroupKind = Request["dwKind"];
            if (szGroupKind != null)
            {
                uKind = Parse(szGroupKind);
            }
          //  newGroup.dwKind = uKind;
            if (m_Request.Group.SetGroup(newGroup, out newGroup) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "新建失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox("新建成功", "新建成功", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
         
        }
        m_Title = "新建开放对象";
	}
  
}
