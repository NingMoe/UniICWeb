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
        string m_szRoom= "";
        string szLabID = Request["labid"];
        uint? ulabID = uint.Parse(szLabID);
        Response.CacheControl = "no-cache";

        UNIROOM[] roomList= GetRoomByLab(ulabID.ToString());
        if (roomList != null && roomList.Length > 0)
        {
            for (int i = 0; i < roomList.Length; i++)
            {
                m_szRoom += GetInputItemHtml(CONSTHTML.checkBox, "roomID", roomList[i].szRoomName, roomList[i].dwRoomID.ToString());
            }
        }
        Response.Write(m_szRoom);
    }
}