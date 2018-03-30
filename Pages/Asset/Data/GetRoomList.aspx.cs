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
        Response.CacheControl = "no-cache";
        string szType = Request["type"];
        string szCtrlType = Request["ctrlType"];
        string szKey = Request["szKey"];
        UNIROOM[] vtRoom=new UNIROOM[]{};
        if (szType == "Lab")
        {
            vtRoom = GetRoomByLab(szKey);
        }
        else if (szType == "RoomID")
        {
            UNIROOM room;
            GetRoomID(szKey,out room);
            m_szDev += "[";
            m_szDev += "{\"id\":\"" + room.dwRoomID.ToString() + "\",\"name\":\"" + room.szRoomName.ToString() + "\"}"; 
            m_szDev += "]";
        }
        else {
            vtRoom = GetAllRoom();
        }
        if (vtRoom != null && vtRoom.Length > 0)
        {
            uint uCtrlType = 0;
            if (szCtrlType != null && szCtrlType != "")
            {
                uCtrlType = Parse(szCtrlType);
            }
            m_szDev += "[";
            for (int i = 0; i < vtRoom.Length; i++)
            {
                if (uCtrlType > 0 && ((uCtrlType &(uint)vtRoom[i].dwManMode)== 0))
                {
                    continue;
                }
                m_szDev += "{\"id\":\"" + vtRoom[i].dwRoomID.ToString() + "\",\"name\":\"" + vtRoom[i].szRoomName.ToString() + "\",\"label\":\"" + vtRoom[i].szRoomName.ToString() + "\"}"; 
                m_szDev += ",";
            }
            if (m_szDev.EndsWith(","))
            {
                m_szDev = m_szDev.Substring(0, m_szDev.Length - 1);
            }
            m_szDev += "]";
        }
    
        Response.Write(m_szDev);
    }
}