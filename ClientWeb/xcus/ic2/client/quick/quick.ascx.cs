using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using System.Xml;

public partial class ClientWeb_xcus_ic2_client_jlcj_quick : UniClientModule
{
    protected string selLab = "";
    protected string selRoom = "";
    protected string options = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        InitRoom();
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

    private void InitRoom()
    {
        ROOMREQ req = new ROOMREQ();
        req.szReqExtInfo.szOrderKey = "szLabName";
        req.szReqExtInfo.szOrderMode = "ASC";
        UNIROOM[] rlt;
        if (m_Request.Device.RoomGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            selLab += "<option value=''>未选择</option>";
            selRoom += "<option value=''>不限制</option>";
            for (int i = 0; i < rlt.Length; i++)
            {
                UNIROOM rm = rlt[i];
                if (i == 0 || rm.dwLabID != rlt[i - 1].dwLabID)
                {
                    selLab += "<option value='" + rm.dwLabID + "'>" + rm.szLabName + "</option>";
                }
                selRoom += "<option value='" + rm.dwRoomID + "' class='it lab_" + rm.dwLabID + "'>" + rm.szRoomName + "</option>";
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