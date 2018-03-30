using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Newtonsoft.Json;
using UniWebLib;
public partial class searchAccount : UniPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string m_szDev = "";
        string szRoomID = Request["roomid"];
       // uint? uRoomID = uint.Parse(szRoomID);
        Response.CacheControl = "no-cache";
        Response.Write(szRoomID);
        return;
       
        //m_szDev="[{\"data\":\"hello\",\"type\":\"1234\"}, {\"data\":\"hello1\",\"type\":\"12234\"}]";
       //
        //Newtonsoft.Json.Serialization.
     
    }
}