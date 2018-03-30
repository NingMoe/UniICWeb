using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class searchGroup : UniClientAjax
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string key = Request["term"];
        string deadLine = Request["line"];
        string kind = Request["kind"];
        string need=Request["need"];
        Response.CacheControl = "no-cache";

        GROUPREQ req = new GROUPREQ();
        if (!string.IsNullOrEmpty(kind) && kind != "0")
        {
            req.dwKind = ToUInt(kind);
        }
        if (!string.IsNullOrEmpty(deadLine) && deadLine != "0")
        {
            req.dwMinDeadLine = req.dwMaxDeadLine = ToUInt(deadLine);
        }
        req.szName = key;
        if (!string.IsNullOrEmpty(need) && need != "0")
            req.szReqExtInfo.dwNeedLines = ToUInt(need);
        else
            req.szReqExtInfo.dwNeedLines = 10; //最多10条
        UNIGROUP[] rlt;
        if (m_Request.Group.GetGroup(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS && rlt != null)
        {
                MyString szOut = new MyString();
                szOut += "[";
                for (int i = 0; i < rlt.Length; i++)
                {
                    szOut += "{\"id\":\"" + rlt[i].dwGroupID + "\",\"name\": \"" + rlt[i].szName + "\",\"label\": \"" + rlt[i].szName + "\",\"deadLine\": \"" + rlt[i].dwDeadLine + "\",\"enrollLine\": \"" + rlt[i].dwEnrollDeadline + "\"}";
                    if (i < rlt.Length - 1)
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