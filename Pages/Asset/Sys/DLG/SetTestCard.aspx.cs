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
    protected string m_Title = "编辑实验项目卡";
	protected void Page_Load(object sender, EventArgs e)
    {
        string szOPName = "编辑";
        if (Request["op"] == "set")
        {
            bSet = true;
            szOPName = "修改";
        }
        else
        {
            szOPName = "新建";
        }

        TESTCARD testCard;
        if (IsPostBack)
        {
            GetHTTPObj(out testCard);
            if (m_Request.Reserve.SetTestCard(testCard, out testCard) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, szOPName+"实验项目卡失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox(szOPName+"实验项目卡成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
        }

        if (bSet)
        {
            TESTCARDREQ vrGetReq = new TESTCARDREQ();
            vrGetReq.dwTestCardID = ToUint(Request["id"]);
            TESTCARD[] vtRet;
            if (m_Request.Reserve.GetTestCard(vrGetReq, out vtRet) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else
            {
                if (vtRet.Length == 0)
                {
                    MessageBox("获取失败", "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                }
                else
                {
                    PutJSObj(vtRet[0]);
                    m_Title = szOPName+"实验项目卡【" + vtRet[0].szTestName + "】";
                }
            }
        }
        else
        {
            m_Title = szOPName + "实验项目卡";

        }
    }
}
