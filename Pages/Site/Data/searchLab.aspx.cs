using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class Resv_searchCls :UniPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        m_bRemember = false;

        string szTerm = Request["term"];
        string szKind = Request["kind"];
        string szAll = Request["InAll"];//szAll=false不包含全部
        Response.CacheControl = "no-cache";

        LABREQ vrGetCls = new LABREQ();
        UNILAB[] vtCls;      
        vrGetCls.szLabName = szTerm;
        vrGetCls.szReqExtInfo.dwNeedLines = 10; //最多10条
        if (szKind !=null && szKind != "")
        {
            vrGetCls.dwLabClass = Parse(szKind);
        }
        if (m_Request.Device.LabGet(vrGetCls, out vtCls) == REQUESTCODE.EXECUTE_SUCCESS && vtCls != null)
        {
            MyString szOut = new MyString();
            szOut += "[";
            if (szAll == null || szAll == "")
            {
                szOut += "{\"id\":\"" + "0" + "\",\"label\": \"" + "全部" + "\"},";
            }
            for (int i = 0; i < vtCls.Length; i++)
            {
                szOut += "{\"id\":\"" + vtCls[i].dwLabID + "\",\"label\": \"" + vtCls[i].szLabName + "\"}";
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