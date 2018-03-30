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
    protected string m_Campu = "";
	protected void Page_Load(object sender, EventArgs e)
    {
      
        if (IsPostBack)
        {
            string szLev1 = Request["LV1"];
            string szLev2 = Request["LV2"];
            string szLev3 = Request["LV3"];
            string szValue = "LV1:," + szLev1 + ",;LV2:," + szLev2 + ",;LV3:," + szLev3 + ",;";
            IFPARAM value = new IFPARAM();
            value.dwAdminID = Parse(Request["dwID"]);

            value.szParam = szValue;
            if(m_Request.Admin.SaveIF(value)==REQUESTCODE.EXECUTE_SUCCESS)
            {
              MessageBox("设置成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
            }
            else
            {
                MessageBox("设置失败:" + m_Request.szErrMessage.ToString(), "提示", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
        }

        if (Request["op"] == "set")
        {
            UNIACCOUNT accno;
            if(GetAccByAccno(Request["dwID"],out accno))
            {
                PutMemberValue("divName", accno.szTrueName + "," + accno.szDeptName);
            }
            bSet = true;
            IFPARAMREQ vrGet = new IFPARAMREQ();
            vrGet.dwAdminID = Parse(Request["dwID"]);
            IFPARAM[] vtRes;
            if (m_Request.Admin.GetIF(vrGet,out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
            {
                PutMemberValue("LV1", vtRes[0].szParam);
                PutMemberValue("LV2", vtRes[0].szParam);
                PutMemberValue("LV3", vtRes[0].szParam);
            }
        }
        else
        {
            m_Title = "设置管理员权限";
        }
    }
}
