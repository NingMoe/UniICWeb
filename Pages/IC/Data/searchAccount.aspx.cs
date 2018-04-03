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
        string szTerm = Request["term"];
        string szType = Request["Type"];
        string szIdent = Request["ident"];
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
            vrGet.szReqExtInfo.szOrderKey = "szLogonName";
            vrGet.szReqExtInfo.szOrderMode = "asc";
        }
        else if (szType.ToLower() == "truename")
        {
            vrGet.szTrueName = szTerm;// (uint)ACCREQ.DWGETTYPE.ACCGET_BYLOGONNAME;
        }
        else if (szType.ToLower() == "pid")
        {
            vrGet.szPID = szTerm;// (uint)ACCREQ.DWGETTYPE.ACCGET_BYLOGONNAME;
        }
        uint dwIdent = ToUint(Request["dwIdent"]);
        if (Request["dwIdent"] != null && dwIdent != 0)
        {
            vrGet.dwIdent = dwIdent;
        }
        if (szIdent != null && szIdent != "")
        {
            vrGet.dwIdent = (uint)UNIACCOUNT.DWIDENT.EXTIDENT_TUTOR;
        }
       // vrGet.szGetID = szTerm;
        vrGet.szReqExtInfo.dwNeedLines = 15; //最多10条

        if (m_Request.Account.Get(vrGet, out vtAccount) == REQUESTCODE.EXECUTE_SUCCESS && vtAccount != null)
        {
            MyString szOut = new MyString();
            szOut += "[";
            for (int i = 0; i < vtAccount.Length; i++)
            {
                if (szType==null||szType.ToLower() == "truename")
                {
                    szOut += "{\"id\":\"" + vtAccount[i].dwAccNo + "\",\"label\": \"" + vtAccount[i].szTrueName + "(" + vtAccount[i].szLogonName+"," + vtAccount[i].szDeptName + ")"  + "\",\"szLogonName\": \""  + vtAccount[i].szLogonName.ToString() + "\",\"szTrueName\": \"" + vtAccount[i].szTrueName + "\",\"szHandPhone\": \"" + vtAccount[i].szHandPhone + "\",\"szTel\": \"" + vtAccount[i].szTel + "\",\"szEmail\": \"" + vtAccount[i].szEmail + "\"}";
                }
                else
                {
                    szOut += "{\"id\":\"" + vtAccount[i].dwAccNo + "\",\"label\": \"" + vtAccount[i].szTrueName + "(" + vtAccount[i].szLogonName + "," + vtAccount[i].szDeptName + ")" + "\",\"szLogonName\": \"" + vtAccount[i].szLogonName.ToString() + "\",\"szTrueName\": \"" + vtAccount[i].szTrueName + "\",\"szHandPhone\": \"" + vtAccount[i].szHandPhone + "\",\"szTel\": \"" + vtAccount[i].szTel + "\",\"szEmail\": \"" + vtAccount[i].szEmail + "\"}";
                    //szOut += "{\"id\":\"" + vtAccount[i].dwAccNo + "\",\"label\": \"" + vtAccount[i].szTrueName + "\"}";
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