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
    protected string m_Title = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        m_Title = "修改密码";
        ADMINLOGINRES adminAcc = new ADMINLOGINRES();
        try
        {
            adminAcc = (ADMINLOGINRES)Session["LoginResult"];
        }
        catch { 
        }
        if (IsPostBack)
        {
             string szPasswd1 = Request["passwd"];
            string szPasswd2 = Request["confirmpasswd"];
            if (szPasswd1 != szPasswd2)
            {
                MessageBox("两次密码输入不一致", "提示", MSGBOX.ERROR);
                PutHTTPObj(adminAcc.AccInfo);
                return;
            }
            if (szPasswd1 == "" || szPasswd1==null)
            {
                MessageBox("新密码不能为空", "提示", MSGBOX.ERROR);
                PutHTTPObj(adminAcc.AccInfo);
                return;
            }
            ADMINCHGPASSWD admin=new ADMINCHGPASSWD();
            admin.dwAdminID=Parse(Request["dwAccno"]);
            admin.szCurAdminPw ="db4e4b64e6ce1e";
            admin.szNewPw="P"+szPasswd1;
            if (m_Request.Admin.AdminChgPasswd(admin)!= REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "重置密码失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE); 
                PutHTTPObj(adminAcc.AccInfo);
                return;
            }
            else
            {
                adminAcc.AccInfo.szPasswd = szPasswd1;
                Session["LoginResult"] = adminAcc;
                Response.Write("<script>alert('密码修改成功');top.location='../../default.aspx'</script>");
               //MessageBox("重置密码成功", "提示", MSGBOX.SUCCESS, "<script>window.top='../../default.aspx'</script>");

                return;
            }
           
        }
      
        PutHTTPObj(adminAcc.AccInfo);
        

    }
}