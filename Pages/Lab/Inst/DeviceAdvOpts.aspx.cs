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

public partial class Sub_Device : UniPage
{
    protected string m_szLab = "";
    protected string m_szRoom = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Buffer = true;
        Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(60);

        UNILAB[] lab = GetAllLab();
        if (lab != null)
        {
            for (int i = 0; i < lab.Length; i++)
            {
                m_szLab += "<label><input name='lab' value='" + lab[i].dwLabID + "' type='checkbox' />" + lab[i].szLabName + "</label>  ";
            }
        }
        UNIROOM[] room = GetAllRoom();
        if (room != null)
        {
            for (int i = 0; i < room.Length; i++)
            {
                m_szRoom += "<label><input name='room' value='" + room[i].dwRoomID + "' type='checkbox' />" + room[i].szRoomName + "</label>  ";
            }
        }
    }
}
