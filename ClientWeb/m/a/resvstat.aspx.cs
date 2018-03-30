using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_m_all_resvstat : UniClientPage
{
    protected string unit = "";
    protected bool isFloorPlan = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadPage();
        if (Request["islong"] == "true")
        {
            unit = "("+Translate("单位")+": "+Translate("日期")+")";
        }
        else
        {
            unit = "(" + Translate("单位") + ": " + Translate("小时") + ")";
        }
        if ((ToUInt(Request["classkind"]) & ToUInt(GetConfig("floorPlanClsKind"))) > 0)
            isFloorPlan = true;
    }
}