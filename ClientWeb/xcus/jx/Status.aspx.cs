using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_jx_Status : UniClientPage
{
    protected string curTerm = "";
    protected string termList = "";
    private UNITERM yearTerm;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(ClientRedirect("Login.aspx"))
        InitTerm();
    }
    private void InitTerm()
    {
        UNITERM term;
        UNITERM[] rlt = InitTermList(out term, Request["term"]);
        curTerm = term.szMemo;
        yearTerm = term;
        Master.Year = (uint)term.dwYearTerm;
        for (int i = 0; i < rlt.Length; i++)
        {
            termList += "<li><a onclick='selTermYear(\"" + rlt[i].dwYearTerm + "\")'>" + rlt[i].szMemo + "</a></li>";
        }
        //TERMREQ req = new TERMREQ();
        //req.szReqExtInfo.szOrderKey = "dwYearTerm";
        //req.szReqExtInfo.szOrderMode = "DESC";
        //UNITERM[] rlt;
        //if (m_Request.Reserve.GetTerm(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        //{
        //    uint? term = 0;
        //    if (!string.IsNullOrEmpty(Request["term"])) term = ToUInt(Request["term"]);
        //    for (int i = 0; i < rlt.Length; i++)
        //    {
        //        if (term == 0)
        //        {
        //            if ((rlt[i].dwStatus & (uint)UNITERM.DWSTATUS.TERMSTAT_FORCE) > 0)
        //            {
        //                term = rlt[i].dwYearTerm;
        //                curTerm = rlt[i].szMemo;
        //                yearTerm = rlt[i];
        //            }
        //        }
        //        else if (term == rlt[i].dwYearTerm)
        //        {
        //            curTerm = rlt[i].szMemo;
        //            yearTerm = rlt[i];
        //        }
        //        termList += "<li><a onclick='selTermYear(\"" + rlt[i].dwYearTerm + "\")'>" + rlt[i].szMemo + "</a></li>";
        //    }
        //    Master.Year = (uint)term;
        //}
        //else
        //    MsgBox(m_Request.szErrMsg);
    }
}