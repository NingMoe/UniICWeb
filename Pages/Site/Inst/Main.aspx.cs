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
    protected uint uShowCheck = 1;
    protected uint uManMenu = 0;//1超级管理员，2物管，4归口，8服务，16保卫,32没有审核权限
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginResult"] != null)
        {
            ADMINLOGINRES adminAcc = (ADMINLOGINRES)Session["LoginResult"];
            uint uManRole = (uint)adminAcc.dwManRole;
            uint uPropety = (uint)adminAcc.AdminInfo.dwProperty;
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
            if ((uManRole & (uint)ADMINLOGINRES.DWMANROLE.MANIDENT_EANDA) == 0 && (uManRole & (uint)ADMINLOGINRES.DWMANROLE.MANSCOPE_STATION)==0)
            {
                uShowCheck = 1;
            }
            /*
             <option value="256">物管审核</option>
     <option value="2048">归口审核</option>
     <option value="4096">服务审核</option>
     <option value="536870912">保卫处</option>
     <option value="1073741824">无审核权限</option>
             */
            //1超级管理员，2物管，4归口，8服务，16保卫,32没有审核权限
            if ((uManRole & (uint)ADMINLOGINRES.DWMANROLE.MANROLE_SUPER) > 0)
            {
                uManMenu = uManMenu | 1;
            }

            if ((uPropety & 256) > 0)
            {
                uManMenu = uManMenu | 2;
            }
            if ((uPropety & 2048) > 0)
            {
                uManMenu = uManMenu | 4;
            }
            if ((uPropety & 4096) > 0)
            {
                uManMenu = uManMenu | 8;
            }
            if ((uPropety & 536870912) > 0)
            {
                uManMenu = uManMenu | 16;
            }
            if ((uPropety & 1073741824) > 0)
            {
                uManMenu = uManMenu | 32;
            }


        }
    }
}
