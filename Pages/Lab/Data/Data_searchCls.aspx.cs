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
        string szTerm = Request["term"];

        Response.CacheControl = "no-cache";

        CLASSREQ  vrGetCls = new CLASSREQ();
        UNICLASS[] vtCls;
        //vrGetCls.dwGetType = (uint)CLASSREQ.DWGETTYPE.CLSGET_BYNAME;
        vrGetCls.szClassName = szTerm;
        vrGetCls.szReqExtInfo.dwNeedLines = 10; //最多10条

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