using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class searchCourse : UniWebLib.UniPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        m_bRemember = false;

        string kind = Request["kind"];
        string name = Request["term"];
        Response.CacheControl = "no-cache";

        COMPANYREQ vrGet = new COMPANYREQ();
        UNICOMPANY[] vtCls;
        if (name != null && name != "")
        {
            vrGet.szComName = name;
        }
        if (kind != null && kind != "" && kind!="0")
        {
            vrGet.dwComKind = Parse(kind) ;
        }
        vrGet.szReqExtInfo.dwNeedLines = 15; //最多10条

        if (m_Request.Assert.GetCompany(vrGet, out vtCls) == REQUESTCODE.EXECUTE_SUCCESS && vtCls != null)
        {
            MyString szOut = new MyString();
            szOut += "[";
            for (int i = 0; i < vtCls.Length; i++)
            {
                szOut += "{\"id\":\"" + vtCls[i].dwComID + "\",\"label\": \"" + vtCls[i].szComName + "\"}";
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