using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_pro_page_testitem_upload : UniClientPage
{
    protected string fileName = "";
    string test_id;
   static UNITESTITEM test;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsFrameLogin())
        {
            test_id = Request["test_id"];
            if (string.IsNullOrEmpty(test_id))
            {
                MsgBox("未指定实验");
                return;
            }
            else if (!IsPostBack)
            {
                InitTest();
            }
        }
    }

    private void InitTest()
    {
        test = GetTestItemByID(test_id);
        if (test.dwTestItemID != null)
        {
            if (!string.IsNullOrEmpty(test.szReportFormURL))
                fileName = "<a href='"+Page.ResolveClientUrl("~/ClientWeb/")+"upload/UpLoadFile/"+test.szReportFormURL+"'>模版已存在，点击下载</a>";
        }
        else
            MsgBox(m_Request.szErrMsg);
    }
    protected void submit_test_ServerClick(object sender, EventArgs e)
    {
        string report = Request["up_file"];
        if (string.IsNullOrEmpty(report))
        {
            MsgBox("还未上传文件");
            return;
        }
        UNITESTITEM para = test;
        para.dwTestItemID = ToUInt(test_id);
        para.szReportFormURL = report;
        if (m_Request.Reserve.SetTestItem(para, out para) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            MsgBox("保存成功","CloseDlg();");
        }
        else
            MsgBox(m_Request.szErrMsg);
    }
}