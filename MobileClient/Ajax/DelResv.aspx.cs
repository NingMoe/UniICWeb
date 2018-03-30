using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class MobileClient_Ajax_ALogin : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string szResvID=Request["resvid"];

        UNIRESERVE delResv = new UNIRESERVE();
        delResv.dwResvID = Parse(szResvID);
        m_Request.m_UniDCom.StaSN = 1;
        if (m_Request.Reserve.Del(delResv) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            Response.Write("{\"res\":1,\"error\":\"" +"" + "\"}");
        }
        else
        {
            Response.Write("{\"res\":0,\"error\":\"" + m_Request.szErrMessage + "\"}");
        }


    }
   
}