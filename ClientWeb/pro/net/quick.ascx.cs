using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using System.Xml;

public partial class ClientWeb_xcus_ic2_client_jlcj_quick : UniClientModule
{
    protected string selRoom = "<option value=''>全部</option>";
    protected string options = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        uint openClsKind = ToUInt(GetConfig("openClsKind"));
        if (openClsKind == 0)
        {
                InitRoom(0);
        }
        else
        {
            if ((openClsKind & 1) > 0)
                InitRoom(1);
            if ((openClsKind & 2) > 0)
                InitRoom(2);
            if ((openClsKind & 4) > 0)
                InitRoom(4);
            if ((openClsKind & 8) > 0)
                InitRoom(8);
        }
        InitOpt();
    }

    private void InitOpt()
    {
        XmlNodeList list = common.GetXMLConst(Server.MapPath("~/LocalFile/file.xml"), "ResvTheme");
        if (list != null)
        {
            foreach (XmlNode item in list)
            {
                string opt = item.InnerText;
                options += "<option value='" + opt + "'>" + opt + "</option>";
            }
        }
    }

    private void InitRoom(uint clsKind)
    {
        ROOMREQ req = new ROOMREQ();
        req.szReqExtInfo.szOrderKey = "szLabName";
        req.szReqExtInfo.szOrderMode = "ASC";
        if(clsKind!=0)
        req.dwInClassKind = clsKind;
        UNIROOM[] rlt;
        if (m_Request.Device.RoomGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                UNIROOM rm = rlt[i];
                selRoom += "<option value='" + rm.dwRoomID + "' class='it' clskind='"+clsKind+"'>" + rm.szRoomName + "</option>";
            }
        }
    }
    public uint ToUInt(object obj)
    {
        try
        {
            return Convert.ToUInt32(obj);
        }
        catch (Exception)
        {

            return 0;
        }
    }
}