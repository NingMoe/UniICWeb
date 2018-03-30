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

public partial class Sub_Lab : UniPage
{
    protected string m_szOpts = "";
    protected MyString m_szOut = new MyString();
    protected string m_szCamp = "";
    protected string m_szBuilding = "";
    protected string szDateNow = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        szDateNow = DateTime.Now.ToString("yyy-MM-dd");
        UNICAMPUS[] vtCamp = GetAllCampus();
        m_szCamp += GetInputItemHtml(CONSTHTML.option, "", "全部", "0");
        for (int i = 0; i < vtCamp.Length; i++)
        {
            m_szCamp += GetInputItemHtml(CONSTHTML.option, "", vtCamp[i].szCampusName, vtCamp[i].dwCampusID.ToString());
        }
        m_szBuilding = "";
        UNIBUILDING[] buliding = getAllBuilding();
        if (buliding != null && buliding.Length > 0)
        {

            m_szBuilding += "<option value='0'>" + "全部" + "</option>";
            for (int i = 0; i < buliding.Length; i++)
            {
                m_szBuilding += "<option value='" + buliding[i].dwBuildingID + "'";
                m_szBuilding += ">" + buliding[i].szBuildingName + "</option>";
            }
        }


        PutBackValue();
    }
    private void DelLab(string szID)
    {
       
    }
}
