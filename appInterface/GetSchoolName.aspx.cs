using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using Newtonsoft.Json;

public partial class login :UniPage
{
    public class GetRes
    {
        public uint code;
        public string message;
        public object data;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        GetRes res = new GetRes();
        res.code = 0;
        res.message = "";
        string schoolName = System.Web.Configuration.WebConfigurationManager.AppSettings["schoolname"];
        Response.Write(schoolName);
        Response.End();
    }   
   
}