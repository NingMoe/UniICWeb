using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_jx_Login : UniClientPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsLogined())
        {
            Response.Redirect("Default.aspx");//已登录 跳转主页
        }
        else
        {
            string third = GetConfig("thirdLogin");//未登录 重定向登录
            if (!string.IsNullOrEmpty(third))
            {
                Response.Redirect(third);
            }
        }
    }
}