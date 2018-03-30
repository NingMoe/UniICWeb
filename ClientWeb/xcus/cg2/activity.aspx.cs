using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_cg2_activity : UniClientPage
{
    protected string ClsList = "";
    protected string LabList = "";
    protected string CampusList = "";
    protected string AtyTypeList = "";
    protected string BuildingList = "";
    protected string atyId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadPage();
        atyId = Request["aty_id"];
        //LabList = GetLabHtm("radio");
        AtyTypeList = GetAtyType();
        CampusList = GetCampusHtm("radio");
        BuildingList = GetBuildingHtm("opt",null,ToUInt(atyId));
    }
    string GetAtyType()
    {
        string ret = "";
        YARDACTIVITYREQ req = new YARDACTIVITYREQ();
        YARDACTIVITY[] rlt;
        if (m_Request.Reserve.GetYardActivity(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS&&rlt!=null)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                bool sel = rlt[i].dwActivitySN.ToString() == atyId;
                string str =  sel? " f_sel" : "";
                ret += "<a class='it"+str+"' value=\"" + rlt[i].dwActivitySN + "\"><input type='radio' "+(sel?"checked='checked'":"")+"/> " + rlt[i].szActivityName + "</a>";
            }
        }
        return ret;
    }
}