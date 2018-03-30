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
using System.Collections.Generic;
using UniWebLib;
using System.Xml;
using System.IO;

public partial class Page_ : UniClientPage
{
    public string ov_intro = "";
    public string ov_detail = "";
    public string ov_help = "";
    public string ov_rule = "";
    public string ov_contact = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        base.LoadPage();
        if (!IsPostBack)
        {
            ov_intro = GetContent("0", "ov");
            ov_detail = GetContent("1", "ov");
            ov_help = GetContent("2", "ov");
            ov_rule = GetContent("3", "ov");
            ov_contact = GetContent("4", "ov");
        }
    }
    private string GetContent(string id, string type)
    {
        return GetXmlContent(id, type, "ics_data");
    }
    protected void ddlDevClass_SelectedIndexChanged(object sender, EventArgs e)
    {
      
    }
}
