using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_pro_ajax_app : UniClientAjax
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LoadPage())
        {
            NoBuffer();
            if (act == "init_app")
            {
                appSet set = new appSet();
                if (IsLogined())
                {
                    set.acc = ToProAcc((UNIACCOUNT)Session["LOGIN_ACCINFO"]);
                }
                set.config.sysName = GetConfig("SysName");
                set.config.clientName = GetConfig("SysAutoSchoolName");
                SucRlt(set);
            }
            else
            {
                NoAct();
            }
        }

    }
    struct appSet
    {
        public proacc acc;
        public appConfig config;
    }
    struct appConfig
    {
        public string sysName;
        public string clientName;
    }
}