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

public partial class _Default : UniPage
{
    protected bool bSet = false;
    protected string m_Title = "";
    protected string m_szDoorCtrl = "";
    protected string m_szLab = "";
    protected string m_szRoom = "";
    protected string m_szDevKind = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        UNILAB[] vtLab = GetAllLab();
        if (vtLab != null && vtLab.Length > 0)
        {
            for (int i = 0; i < vtLab.Length; i++)
            {
                m_szLab += "<option value='" + vtLab[i].dwLabID + "'>" + vtLab[i].szLabName + "</option>";
            }
        }

        UNIROOM[] vtRoom = GetAllRoom();
        if (vtRoom != null && vtRoom.Length > 0)
        {
            for (int i = 0; i < vtRoom.Length; i++)
            {
                m_szRoom += "<option value='" + vtRoom[i].dwRoomID + "'>" + vtRoom[i].szRoomName + "</option>";
            }
        }

        UNIDEVKIND[] vtDevKind = GetAllDevKind();
        if (vtDevKind != null && vtDevKind.Length > 0)
        {
            for (int i = 0; i < vtRoom.Length; i++)
            {
                m_szDevKind += "<option value='" + vtDevKind[i].dwKindID + "'>" + vtDevKind[i].szKindName + "</option>";
            }
        }



        DEVREQ vrDevReq = new DEVREQ();
        vrDevReq.dwDevID = Parse(Request["id"]);
        UNIDEVICE[] vtDev;
        if (m_Request.Device.Get(vrDevReq, out vtDev) != REQUESTCODE.EXECUTE_SUCCESS)
        {
            MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
        }
        else
        {
            if (vtDev.Length == 0)
            {
                MessageBox("获取失败", "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else
            {
                PutJSObj(vtDev[0]);
            }
            m_Title = "查看" + ConfigConst.GCDevName;

        }
    }
}
