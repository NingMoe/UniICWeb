using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class searchAccount : UniWebLib.UniPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string accno = Request["accno"];
        string logonname= Request["logonname"];
        Response.CacheControl = "no-cache";

        ACCREQ vrGet = new ACCREQ();
        UNIACCOUNT[] vtAccount;
        if (accno!=null)
        {
            vrGet.dwAccNo = Parse(accno);
        }
        else if (logonname!=null)
        {
            vrGet.szLogonName = logonname;
        }
            if (m_Request.Account.Get(vrGet, out vtAccount) == REQUESTCODE.EXECUTE_SUCCESS && vtAccount != null && vtAccount.Length > 0)
            {
                lblLogonName.InnerText = vtAccount[0].szLogonName;
                lblTrueName.InnerText = vtAccount[0].szTrueName;

                lblDeptName.InnerText = vtAccount[0].szDeptName;

                lblHandPhone.InnerText = vtAccount[0].szHandPhone;
                lblTel.InnerText = vtAccount[0].szTel;
                lblMail.InnerText = vtAccount[0].szEmail;
            }
            else
            {

            }
    }
        
}