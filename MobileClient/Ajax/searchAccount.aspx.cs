using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using Newtonsoft.Json;
using System.Reflection;

public partial class MobileClient_Ajax_ALogin : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string szTerm = Request["logonName"];
      
        Response.CacheControl = "no-cache";

        ACCREQ vrGet = new ACCREQ();
        UNIACCOUNT[] vtAccount;
        
        vrGet.szLogonName = szTerm;// (uint)ACCREQ.DWGETTYPE.ACCGET_BYLOGONNAME;

       
        vrGet.szReqExtInfo.dwNeedLines = 10; //最多10条
        m_Request.m_UniDCom.StaSN = 1;
        if (m_Request.Account.Get(vrGet, out vtAccount) == REQUESTCODE.EXECUTE_SUCCESS && vtAccount != null)
        {
            MyString szOut = new MyString();
            szOut += "[";
            for (int i = 0; i < vtAccount.Length; i++)
            {
               
                {
                    szOut += "{\"id\":\"" + vtAccount[i].dwAccNo + "\",\"label\": \"" + vtAccount[i].szTrueName + "(" + vtAccount[i].szLogonName + "," + vtAccount[i].szDeptName + ")" + "\",\"szLogonName\": \"" + vtAccount[i].szLogonName.ToString() + "\",\"szTrueName\": \"" + vtAccount[i].szTrueName + "\",\"szHandPhone\": \"" + vtAccount[i].szHandPhone + "\",\"szTel\": \"" + vtAccount[i].szTel + "\",\"szEmail\": \"" + vtAccount[i].szEmail + "\"}";
                   
                }
                if (i < vtAccount.Length - 1)
                {
                    szOut += ",";
                }
            }
            szOut += "]";
            Response.Write(szOut);
        }
        else
        {
            Response.Write("[{}]");
        }
    }
        
}