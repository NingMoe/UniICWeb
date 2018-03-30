using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class Resv_searchCls : UniWebLib.UniPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        m_bRemember = false;

        string szTerm = Request["term"];
        string szDeptID = Request["deptID"];
        Response.CacheControl = "no-cache";

        CLASSREQ  vrGetCls = new CLASSREQ();
        UNICLASS[] vtCls;
       // vrGetCls.dwGetType = (uint)CLASSREQ.DWGETTYPE.CLSGET_BYNAME;
        vrGetCls.szClassName = szTerm;
        vrGetCls.szReqExtInfo.dwNeedLines = 10; //最多10条
        if (szDeptID != null && szDeptID != "" && szDeptID != "0")
        {
            vrGetCls.dwDeptID = Parse(szDeptID);
        }
        if (m_Request.Account.ClassGet(vrGetCls, out vtCls) == REQUESTCODE.EXECUTE_SUCCESS && vtCls != null)
        {
            MyString szOut = new MyString();
            szOut += "[";
            for (int i = 0; i < vtCls.Length; i++)
            {
                szOut += "{\"id\":\"" + vtCls[i].dwClassID + "\",\"label\": \"" + vtCls[i].szClassName + "\"}";
                if (i < vtCls.Length - 1)
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