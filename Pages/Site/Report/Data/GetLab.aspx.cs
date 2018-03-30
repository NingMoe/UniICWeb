using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using UniWebLib;
public partial class searchAccount : UniPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";
        //string m_szDevRes = "{totalCount:1\}";
        string m_szDevRes = "Ext.data.JsonP.callback1({\"totalCount\":\"1\",\"topics\":[{\"title\":\"XTemplate\",\"threadid\":\"133690\",\"username\":\"kpr@emco\",\"userid\":\"272497\",\"dateline\":\"1305604761\",\"postid\":\"602876\",\"forumtitle\":\"Ext 3.xHelp\",\"forumid\":\"40\",\"replycount\":\"2\",\"lastpost\":\"1305857807\",\"lastposter\":\"kpr@emco\",\"excerpt\":\"Hi ,\"}]})";
        Response.Write(m_szDevRes);
    }
}