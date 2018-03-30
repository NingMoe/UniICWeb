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
        uint? uRoomID = uint.Parse(szRoomID);
        Response.CacheControl = "no-cache";

        UNIDEVICE[] devList = GetDevByRoomId(uRoomID);
        m_szDev+="[";
        if (devList != null && devList.Length > 0)
        {
            for (int i = 0; i < devList.Length; i++)
            {
                m_szDev += "{\"id\":\"" + devList[i].dwDevID.ToString() + "\",\"name\":\"" + devList[i].szDevName.ToString() + "\"}"; //"<label><input class=\"enum\" type=\"checkbox\" name=\"" + "devID" + "\" value=\"" + devList[i].dwDevID.ToString() + "\" /> " + devList[i].szDevName + "</label>,";
                m_szDev += ",";
            }
            if (m_szDev.EndsWith(","))
            {
                m_szDev = m_szDev.Substring(0, m_szDev.Length-1);
            }
           
        }
        m_szDev += "]";
        //m_szDev="[{\"data\":\"hello\",\"type\":\"1234\"}, {\"data\":\"hello1\",\"type\":\"12234\"}]";
       //
        //Newtonsoft.Json.Serialization.
        Response.Write(m_szDev);
    }
}