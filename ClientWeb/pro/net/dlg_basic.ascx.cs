using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_pro_net_dlg_basic : UniClientModule
{
    public string szSearchKey = "姓名/登录名搜索";
    protected void Page_Load(object sender, EventArgs e)
    {
        string szAccLogonName = GetConfig("searchAccLogonName");
        if (szAccLogonName == "4")
        {
            szSearchKey = "校园卡号/读者证号/学号/工号搜索";
        }
    }
}