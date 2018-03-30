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
    protected int nIsAdminSup = 1;
    protected int nJLRoomManager = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginResult"] != null)
        {
            ADMINLOGINRES adminAcc = (ADMINLOGINRES)Session["LoginResult"];
            uint uManRole = (uint)adminAcc.dwManRole;
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

            if (ConfigConst.GCTeacResvMode == 1 && (uManRole & (uint)ADMINLOGINRES.DWMANROLE.MANSCOPE_ROOM)>0)
            {
                nJLRoomManager = 1;
            }
        }
        
    }
}
