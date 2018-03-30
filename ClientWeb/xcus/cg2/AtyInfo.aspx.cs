using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_cg2_AtyInfo : UniClientPage
{
    protected string CampusList = "";
    protected string BuildingList = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadPage();
        CampusList = GetCampusHtm("radio");
        BuildingList = GetBuildingHtm("opt");
    }
}