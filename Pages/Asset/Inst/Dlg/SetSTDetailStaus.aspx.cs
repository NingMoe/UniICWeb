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
    protected string m_szDept = "";
    protected string m_szLabKind = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            STDETAILREQ vrDetailGet = new STDETAILREQ();
            vrDetailGet.dwSTID = Parse(Request["delID"]);
            uint uDetailDevID = Parse(Request["devid"]);

            STDETAIL[] vtDetailRes;
            STDETAIL doDetailDetail = new STDETAIL();
            string szMemo = Request["szMemo"];
            if (m_Request.Assert.STDetailGet(vrDetailGet, out vtDetailRes) == REQUESTCODE.EXECUTE_SUCCESS && vtDetailRes != null && vtDetailRes.Length > 0)
            {
                for (int i = 0; i < vtDetailRes.Length; i++)
                {

                    if (uDetailDevID == ((uint)vtDetailRes[i].dwDevID))
                    {
                        doDetailDetail = vtDetailRes[i];
                        doDetailDetail.szSTInfo = szMemo;
                        doDetailDetail.dwSTStat = (uint)STDETAIL.DWSTSTAT.STSTAT_PROBLEM;
                        if (m_Request.Assert.STDetailDo(doDetailDetail) == REQUESTCODE.EXECUTE_SUCCESS)
                        {
                            MessageBox("设置盘点异常成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                            return;
                            
                        }
                        else
                        {
                            MessageBox(m_Request.szErrMessage, "设置盘点异常失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                            return;
                        }
                    }
                }
            }
        }
        if (Request["op"] == "set")
        {
            bSet = true;

            STDETAILREQ vrGet = new STDETAILREQ();
            vrGet.dwSTID = Parse(Request["delID"]);
            uint uDevID = Parse(Request["devid"]);
            STDETAIL[] vtRes;
            STDETAIL doDetail = new STDETAIL();
            bool bIsExist = false;
            if (m_Request.Assert.STDetailGet(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
            {
                for (int i = 0; i < vtRes.Length; i++)
                {
                    if (uDevID == ((uint)vtRes[i].dwDevID))
                    {
                        doDetail = vtRes[0];
                        bIsExist = true;
                        PutJSObj(doDetail);
                        break;
                    }
                }
            }
        }
        else
        {
            m_Title = "设置盘点异常";

        }

    }
}
