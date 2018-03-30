using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Inst_tempFile_downloadFile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string name = Request["name"];
        Response.ContentType = "application/x-zip-compressed";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + name + "");
        string filename = Server.MapPath("./"+name);
        Response.TransmitFile(filename);
    }
}