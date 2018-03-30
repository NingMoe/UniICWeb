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
        
        string szTerm = Request["term"];

        Response.CacheControl = "no-cache";

        DEPTREQ vrGet = new DEPTREQ();
        UNIDEPT[] vtDept;
        vrGet.dwKind = (uint)ConfigConst.GCDeptKind;
        //vrGet.dwGetType = (uint)DEPTREQ.DWGETTYPE.DEPTGET_BYNAME;
        vrGet.szName = szTerm;
        if (szTerm == "")
        {
            //vrGet.dwGetType = (uint)DEPTREQ.DWGETTYPE.DEPTGET_BYALL;
            //vrGet.szGetKey = szTerm;
        }
        vrGet.szReqExtInfo.dwNeedLines = 10; //最多10条

        if (m_Request.Account.DeptGet(vrGet, out vtDept) == REQUESTCODE.EXECUTE_SUCCESS && vtDept != null)
        {
            MyString szOut = new MyString();
            szOut += "[";
           
            szOut += "{\"id\":\"" + "0" + "\",\"label\": \"" + "全部" + "\"},";
            for (int i = 0; i < vtDept.Length; i++)
            {

                szOut += "{\"id\":\"" + vtDept[i].dwID + "\",\"label\": \"" + vtDept[i].szName + "\"}";
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