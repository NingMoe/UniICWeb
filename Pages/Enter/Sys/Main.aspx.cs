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

public partial class _Default :UniPage
{

    protected string szAdminPar = "";
    protected int nIsAdminSup = 0;
    protected string szDevList = "Device";//设备列表的选择
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ConfigConst.GCDevAndKind == 1)
        {
            szDevList = "DeviceAndKind";
        }
        else
        {
            szDevList = "Device";
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
            if ((uManRole & ((uint)UNIACCOUNT.DWIDENT.EXTIDENT_MANAGER + (uint)ADMINLOGINRES.DWMANROLE.MANSCOPE_STATION + (uint)ADMINLOGINRES.DWMANROLE.MANROLE_SUPER)) > 0)
            {
                nIsAdminSup = 1;
            }
        }

    }
}
