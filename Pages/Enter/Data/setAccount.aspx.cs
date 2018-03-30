using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class searchCourse : UniWebLib.UniPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
       string szTerm = Request["term"];
        string szType = Request["Type"];
        string szHandPhone = Request["handphone"];
        string szEmail = Request["email"];
        Response.CacheControl = "no-cache";

        ACCREQ vrGet = new ACCREQ();
        UNIACCOUNT[] vtAccount;
        if (szType == null || szType == "")
        {
            vrGet.szTrueName = szTerm;
           // vrGet.dwGetType = (uint)ACCREQ.DWGETTYPE.ACCGET_BYTRUENAME;
        }
        else if (szType.ToLower() == "logonname")
        {
            vrGet.szLogonName = szTerm;// (uint)ACCREQ.DWGETTYPE.ACCGET_BYLOGONNAME;
        }

        uint dwIdent = ToUint(Request["dwIdent"]);
        if (Request["dwIdent"] != null && dwIdent != 0)
        {
            vrGet.dwIdent = dwIdent;
        }

       // vrGet.szGetID = szTerm;
        vrGet.szReqExtInfo.dwNeedLines = 10; //最多10条

        if (m_Request.Account.Get(vrGet, out vtAccount) == REQUESTCODE.EXECUTE_SUCCESS && vtAccount != null)
        {
            UNIACCOUNT setAccount = new UNIACCOUNT();
            setAccount = vtAccount[0];
            if (szHandPhone != null)
            {
                setAccount.szHandPhone = szHandPhone;
            }
            if (szEmail != null)
            {
                setAccount.szEmail = szEmail;
            }
            if (m_Request.Account.Set(setAccount, out setAccount) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                Response.Write("success");
            }
            else
            {
                Response.Write(m_Request.szErrMessage); 
            }
        }
        else
        {
 
        }
    }
}