using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class Resv_searchCls : UniClientPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string szTerm = Request["term"];
        string szDeptID = Request["dept"];
        string type = Request["type"];
        Response.CacheControl = "no-cache";

        CLASSREQ  vrGetCls = new CLASSREQ();
        UNICLASS[] vtCls;
        vrGetCls.szClassName = szTerm;
        vrGetCls.szReqExtInfo.dwNeedLines = 10; //最多10条
        if (szDeptID != null && szDeptID != "" && szDeptID != "0")
        {
            vrGetCls.dwDeptID = ToUInt(szDeptID);
        }
        if (m_Request.Account.ClassGet(vrGetCls, out vtCls) == REQUESTCODE.EXECUTE_SUCCESS && vtCls != null)
        {
            MyString szOut = new MyString();
            szOut += "[";
            for (int i = 0; i < vtCls.Length; i++)
            {
                string label = vtCls[i].szClassName;
                szOut += "{\"id\":\"" + vtCls[i].dwClassID + "\",\"label\": \"" + label + "\",\"name\": \"" + vtCls[i].szClassName + "\"}";
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