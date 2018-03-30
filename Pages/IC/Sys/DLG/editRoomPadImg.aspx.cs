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
    protected string szType = "";
	protected void Page_Load(object sender, EventArgs e)
    {
        string szOP = "新建";
        if (Request["op"] == "set")
        {
            szOP = "修改";
        }
        string szName = GetJustNameEqual(Parse(Request["dwCodeTypeRes"]), "CodeType", false);
        CODINGTABLE newValue;
       
        szType += GetInputItemHtml(CONSTHTML.option, "", "预约类型", ((uint)CODINGTABLE.DWCODETYPE.CODE_RESVKIND).ToString());
        szType += GetInputItemHtml(CONSTHTML.option, "", "活动类型", ((uint)CODINGTABLE.DWCODETYPE.CODE_ACTIVITYKIND).ToString());
        szType += GetInputItemHtml(CONSTHTML.option, "", "服务类型", ((uint)CODINGTABLE.DWCODETYPE.CODE_RESVSEIVICE).ToString());
        

    
        if (IsPostBack)
        {
            if (Request["op"] == "set")
            {
                bSet = true;
                CODINGTABLEREQ vrget = new CODINGTABLEREQ();
                vrget.szCodeSN = Request["szCodeSN"];
                CODINGTABLE[] vtRes;
                m_Request.System.GetCodingTable(vrget, out vtRes);
                if (vtRes != null && vtRes.Length > 0)
                {
                    GetHTTPObj(out newValue);
                    CODINGTABLE set = new CODINGTABLE();
                    set = vtRes[0];
                }
            }
            else
            {
                GetHTTPObj(out newValue);
                newValue.dwCodeType = Parse(Request["dwCodeTypeRes"]);
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
        }
        if (Request["op"] == "set")
        {
            bSet = true;
            CODINGTABLEREQ vrget = new CODINGTABLEREQ();
            vrget.szCodeSN = Request["szCodeSN"];
            CODINGTABLE[] vtRes;
            m_Request.System.GetCodingTable(vrget, out vtRes);
            if (vtRes != null && vtRes.Length > 0)
            {

                CODINGTABLEREQ vrParameter = new CODINGTABLEREQ();
                // vrParameter.dwCodeType = Parse(Request["dwCodeTypeRes"]);
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

}
