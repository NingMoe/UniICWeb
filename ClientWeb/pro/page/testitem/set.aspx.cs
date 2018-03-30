using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_pro_page_testitem_set : UniClientPage
{
    string test_id;
    static UNITESTITEM testItem;
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
        testItem = GetTestItemByID(test_id);
        if (testItem.dwTestItemID != null)
        {
            testName.Text = testItem.szTestName;
            testHour.Text = testItem.dwTestHour.ToString();
            userNum.Text = testItem.dwGroupPeopleNum.ToString();
        }
        else
            MsgBox(m_Request.szErrMsg);
    }
    protected void submit_test_ServerClick(object sender, EventArgs e)
    {
        string name = testName.Text;
        string hour = testHour.Text;
        string num = userNum.Text;
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(hour))
        {
            MsgBox("必填项不能为空");
            return;
        }
        uint h;
        uint n;
        if (!uint.TryParse(hour, out h)||!uint.TryParse(num,out n))
        {
            MsgBox("学时格式有误");
            return;
        }
        UNITESTITEM para = new UNITESTITEM();
        para.dwTestItemID = ToUInt(test_id);
        para.dwTestCardID = testItem.dwTestCardID;
        para.szTestName = name;
        para.dwTestHour = h;
        para.dwGroupPeopleNum = n;
        if (m_Request.Reserve.SetTestItem(para, out para) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            MsgBox("修改成功","parent.location.reload();CloseDlg();");
        }
        else
            MsgBox(m_Request.szErrMsg);
    }
}