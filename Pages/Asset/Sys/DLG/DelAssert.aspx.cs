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
    protected string m_szPorperty = "";   
    protected void Page_Load(object sender, EventArgs e)
    {
        string szOp = Request["op"];
        string szOpName = "入库";
        m_szPorperty = GetInputHtmlFromXml(0, CONSTHTML.checkBox, "dwProperty", "UNIDEVICE_Property", true);
        if (IsPostBack)
        {
            if (szOp == "del")
            {
                szOpName = "删除";
            }
            UNIASSERT delAssert;
            GetHTTPObj(out delAssert);
            delAssert.dwPurchaseDate = GetDate(Request["dwPurchaseDate"]);
            if (m_Request.Assert.AssertDel(delAssert) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "提示", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                return;
            }
            else
            {
                MessageBox(szOpName + "成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }

        }
        if (szOp == "del")
        {
            bSet = true;

            ASSERTREQ vrGet = new ASSERTREQ();
            vrGet.dwDevID = Parse(Request["id"]);
            UNIASSERT[] vtRes;
            if (m_Request.Assert.AssertGet(vrGet, out vtRes) != REQUESTCODE.EXECUTE_SUCCESS || vtRes == null || vtRes.Length < 1)
            {
                MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else
            {
                PutJSObj(vtRes[0]);
                m_Title = "删除资产【" + vtRes[0].szDevName + "】";
            }
        }
        else
        {
            m_Title = "资产入库";

        }
    }
}
