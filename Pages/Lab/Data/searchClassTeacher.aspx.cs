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

        UNITEACHERREQ vrGet = new UNITEACHERREQ();
        UNITEACHER[] vtAccount;
        if (szType == null || szType == "")
        {
            vrGet.szTrueName = szTerm;
          
        }
        else if (szType.ToLower() == "logonname")
        {
            vrGet.szPID = szTerm;
        }
        else if (szType.ToLower() == "truename")
        {
            vrGet.szTrueName = szTerm;
        }

        uint dwIdent = ToUint(Request["dwIdent"]);
       
        vrGet.szReqExtInfo.dwNeedLines = 10; //最多10条

        if (m_Request.Account.TeacherGet (vrGet, out vtAccount) == REQUESTCODE.EXECUTE_SUCCESS && vtAccount != null)
        {
            MyString szOut = new MyString();
            szOut += "[";
            for (int i = 0; i < vtAccount.Length; i++)
            {
                if (szType==null||szType.ToLower() == "truename")
                {
                    szOut += "{\"id\":\"" + vtAccount[i].dwAccNo + "\",\"label\": \"" + vtAccount[i].szTrueName + "(" + vtAccount[i].szPID + "," + vtAccount[i].szDeptName + ")" + "\",\"szLogonName\": \"" + vtAccount[i].szPID.ToString() + "\",\"szTrueName\": \"" + vtAccount[i].szTrueName + "\",\"szHandPhone\": \"" + vtAccount[i].szHandPhone + "\",\"szDeptName\": \"" + vtAccount[i].szDeptName + "\",\"szTel\": \"" + vtAccount[i].szTel + "\",\"szEmail\": \"" + vtAccount[i].szEmail + "\"}";
                }
                else
                {
                    szOut += "{\"id\":\"" + vtAccount[i].dwAccNo + "\",\"label\": \"" + vtAccount[i].szTrueName + "(" + vtAccount[i].szPID + "," + vtAccount[i].szDeptName + ")" + "\",\"szLogonName\": \"" + vtAccount[i].szPID.ToString() + "\",\"szTrueName\": \"" + vtAccount[i].szTrueName + "\",\"szHandPhone\": \"" + vtAccount[i].szHandPhone + "\",\"szDeptName\": \"" + vtAccount[i].szDeptName + "\",\"szTel\": \"" + vtAccount[i].szTel + "\",\"szEmail\": \"" + vtAccount[i].szEmail + "\"}";
                 //   //szOut += "{\"id\":\"" + vtAccount[i].dwAccNo + "\",\"label\": \"" + vtAccount[i].szTrueName + "\"}";
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