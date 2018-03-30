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
    protected string szAdminPar = "";
    protected int nIsAdminSup = 0;
    protected string szResvList = "";
    protected string szfunctionMode= System.Web.Configuration.WebConfigurationManager.AppSettings["functionMode"];
    protected void Page_Load(object sender, EventArgs e)
    {
        int uSysKind=ConfigConst.GCSysKind;
        if ((uSysKind & 1) > 0)
        {
            szResvList = "ReserveRoomList.aspx";
        }
        else {
            if ((uSysKind & 2) > 0)
            {
                szResvList = "ReservePCList.aspx";
            }
            else if ((uSysKind & 4) > 0)
            {
                szResvList = "ReserveLendList.aspx";
            }
            else if ((uSysKind &8) > 0)
            {
                szResvList = "ReserveSeatList.aspx";
            }
        }
        if (Session["LoginResult"] != null)
        {
            ADMINLOGINRES adminAcc = (ADMINLOGINRES)Session["LoginResult"];
            uint uManRole = (uint)adminAcc.dwManRole;
            if ((uManRole & ((uint)ADMINLOGINRES.DWMANROLE.MANIDENT_ADMIN + (uint)ADMINLOGINRES.DWMANROLE.MANSCOPE_STATION)) > 0)
            {
                IFPARAMREQ vrGet = new IFPARAMREQ();
                vrGet.dwAdminID = adminAcc.AccInfo.dwAccNo;
                IFPARAM[] vtRes;
                if (m_Request.Admin.GetIF(vrGet,out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
                {
                    szAdminPar = vtRes[0].szParam;
                }
                else
                {
                    szAdminPar = "null";
                }
            }
            else
            {
                szAdminPar = "null";
            }
            if ((uManRole & (uint)ADMINLOGINRES.DWMANROLE.MANSCOPE_LABCTR) > 0)
            {
                nIsAdminSup = 0;
            }
            else if ((uManRole & (uint)ADMINLOGINRES.DWMANROLE.MANSCOPE_LAB) > 0)
            {
                nIsAdminSup = 0;
            }
            else if ((uManRole & (uint)ADMINLOGINRES.DWMANROLE.MANSCOPE_ROOM) > 0)
            {
                nIsAdminSup = 0;
            }

            if ((uManRole & ((uint)UNIACCOUNT.DWIDENT.EXTIDENT_MANAGER+(uint)ADMINLOGINRES.DWMANROLE.MANSCOPE_STATION + (uint)ADMINLOGINRES.DWMANROLE.MANROLE_SUPER)) > 0)
            {
                nIsAdminSup = 1;
            }
        }
    }
}
