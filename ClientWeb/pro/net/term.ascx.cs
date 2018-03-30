using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
public partial class ClientWeb_pro_net_term : UniClientModule
{
    protected string year;
    protected string name;
    protected string status;
    protected string start;
    protected string end;
    protected int firstweek;
    protected int totalweek;
    protected int secnum;
    protected string cts1;
    protected string cts1start;
    protected string cts1end;
    protected string cts2;
    protected string cts2start;
    protected string cts2end;
    private uint yearTerm=0;
    public uint YearTerm
    {
        get { return yearTerm; }
        set { yearTerm = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (yearTerm == 0) return;
        TERMREQ req = new TERMREQ();
        req.dwYearTerm = yearTerm;
        UNITERM[] rlt;
        if (m_Request.Reserve.GetTerm(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS && rlt != null)
        {
            if (rlt.Length > 0)
            {
                UNITERM term = rlt[0];
                year = term.dwYearTerm.ToString();
                name = term.szMemo;
                status = term.dwStatus.ToString();
                start = Util.Converter.UintToDateStr(term.dwBeginDate);
                end = Util.Converter.UintToDateStr(term.dwEndDate);
                firstweek = (int)term.dwFirstWeekDays;
                totalweek = (int)term.dwTotalWeeks;
                secnum = (int)term.dwSecNum;
                cts1 = "{}";
                cts1start = term.dwCTS1Begin.ToString();
                cts1end = term.dwCTS1End.ToString();
                cts2 = "{}";
                cts2start = term.dwCTS2Begin.ToString();
                cts2end = term.dwCTS2End.ToString();
            }
        }
        else
        {
            MsgBox(m_Request.szErrMsg);
        }
    }
}