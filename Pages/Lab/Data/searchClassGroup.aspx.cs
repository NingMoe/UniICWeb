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
        m_bRemember = false;

        string szTerm = Request["termID"];
        TERMREQ termReq = new TERMREQ();
        termReq.dwYearTerm = Parse(szTerm);
        UNITERM[] vtTerm;
        Response.CacheControl = "no-cache";

        uint uEndDate=10;
        if(m_Request.Reserve.GetTerm(termReq,out vtTerm)==REQUESTCODE.EXECUTE_SUCCESS&&vtTerm!=null&&vtTerm.Length>0)
        {
            uEndDate=(uint)vtTerm[0].dwEndDate;
        }
        GROUPREQ vrGet = new GROUPREQ();
        vrGet.dwMaxDeadLine = uEndDate;
        vrGet.dwMinDeadLine = uEndDate;
        UNIGROUP[] vtDept;
       
        vrGet.szReqExtInfo.dwNeedLines = 100000; //最多10条

        if (m_Request.Group.GetGroup(vrGet, out vtDept) == REQUESTCODE.EXECUTE_SUCCESS && vtDept != null)
        {
            MyString szOut = new MyString();
            szOut += "[";
            for (int i = 0; i < vtDept.Length; i++)
            {

                szOut += "{\"id\":\"" + vtDept[i].dwGroupID + "\",\"label\": \"" + vtDept[i].szName + "\"}";
                if (i < vtDept.Length - 1)
                {
                    szOut += ",";
                }
            }
            szOut += "]";
            Response.Write(szOut);
        }
        else
        {
            Response.Write("[ ]");
        }
    }
}