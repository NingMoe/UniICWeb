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
        uint dwYearTerm =Parse(Request["YearTerm"]);
      
        Response.CacheControl = "no-cache";

        UNITEACHERREQ vrGet = new UNITEACHERREQ();
        UNITEACHER[] vtAccount;
        if (dwYearTerm != 0)
        {
           vrGet.dwYearTerm = dwYearTerm;
        }
        vrGet.dwReqProp = (uint)UNITEACHERREQ.DWREQPROP.DEVREQ_NEEDCOURSE;
        vrGet.szReqExtInfo.dwNeedLines = 100; //最多10条

        if (m_Request.Account.TeacherGet(vrGet, out vtAccount) == REQUESTCODE.EXECUTE_SUCCESS && vtAccount != null)
        {
            MyString szOut = new MyString();
            szOut += "[";
            for (int i = 0; i < vtAccount.Length; i++)
            {
               
                {
                    szOut += "{\"id\":\"" + vtAccount[i].dwAccNo + "\",\"label\": \"" + vtAccount[i].szTrueName + "(" + vtAccount[i].szPID + "," + vtAccount[i].szDeptName + ")" + "\",\"szLogonName\": \"" + vtAccount[i].szPID.ToString() + "\",\"szTrueName\": \"" + vtAccount[i].szTrueName + "\",\"szHandPhone\": \"" + vtAccount[i].szHandPhone + "\",\"szDeptName\": \"" + vtAccount[i].szDeptName + "\",\"szTel\": \"" + vtAccount[i].szTel + "\",\"szEmail\": \"" + vtAccount[i].szEmail + "\"}";
                   
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