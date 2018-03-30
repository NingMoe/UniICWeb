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
    protected string m_KindProperty = "";
    protected string m_dwClsKind= "";
	protected void Page_Load(object sender, EventArgs e)
    {
        string szOP = "新建";
        if (Request["op"] == "set")
        {
            szOP = "修改";
        }
        string szName=GetJustNameEqual(Parse(Request["dwCodeType"]),"CodeType",false);
        CODINGTABLE newValue;

    
        if (IsPostBack)
        {
            GetHTTPObj(out newValue);
            newValue.dwCodeType = Parse(Request["dwCodeType"]);
            if (m_Request.System.SetCodingTable(newValue) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, szOP + szName + "失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox(szOP + szName + "成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
        }
        if(Request["op"] == "set")
        {
            bSet = true;
            CODINGTABLEREQ vrParameter = new CODINGTABLEREQ();
            vrParameter.dwCodeType = Parse(Request["dwCodeType"]);
            vrParameter.szCodeSN = Request["szCodeSN"];
              CODINGTABLE[] vrResult;
              if (m_Request.System.GetCodingTable(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
              {
                  PutHTTPObj(vrResult[0]);
              }
        }
        else
        {
            m_Title = szOP + szName;
        }
    }

}
