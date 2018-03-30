using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_pro_ajax_check : UniClientAjax
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LoadPage())
        {
            if (act == "get_check_type")
            {
                GetCkType();
            }
        }
    }

    private void GetCkType()
    {
        string kind = Request["ck_kind"];
        string main = Request["ck_main"];
        CHECKTYPE[] rlt = GetCheckType(ToUInt(kind), ToUInt(main));
        SucRlt(rlt);
    }
}