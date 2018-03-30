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
    protected string m_sKind = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        UNICOMPANY newValuse;

        m_sKind = GetInputHtmlFromXml(0, CONSTHTML.option, "", "companyKind", true);
       
        if (IsPostBack)
        {
            GetHTTPObj(out newValuse);
            if (m_Request.Assert.SetCompany(newValuse, out newValuse) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "设置失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox("操作成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
        }
       
        if (Request["op"] == "set")
        {
            bSet = true;

            COMPANYREQ vrGet = new COMPANYREQ();
            vrGet.dwComID= Parse(Request["dwLabID"]);
             UNICOMPANY[] vtRes;
             if (m_Request.Assert.GetCompany(vrGet, out vtRes) != REQUESTCODE.EXECUTE_SUCCESS)
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
                    m_Title = "修改【" + vtRes[0].szComName + "】";
                }
            }
        }
        else
        {
            m_Title = "";

        }
    }
}
