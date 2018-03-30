using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using UniWebLib;

public partial class Page_Account : UniClientPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Buffer = true;
        Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1); 
        Response.Expires = 0;
        Response.CacheControl = "no-cache";

        base.LoadPage();
        if (Request["act"] == "GetReacher")
        {
            string szReacher = "";
            string szActivityHistory="";
            string ActivityDate = Request["ActivityDate"];
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            string  m_szInfo = "";
            DateTime dDatePre = DateTime.Parse(ActivityDate.ToString());
            DateTime dDateNext = DateTime.Parse(ActivityDate.ToString());
            int nDatePre = dDatePre.Year * 10000 + dDatePre.Month * 100 + dDatePre.Day;
            int nDateNext = dDateNext.Year * 10000 + dDateNext.Month * 100 + dDateNext.Day;
            RESVSHOWREQ vrResvGet = new RESVSHOWREQ();
            string szActivityDate = ActivityDate.Replace("-", "");
            vrResvGet.dwBeginDate = uint.Parse(szActivityDate);
            vrResvGet.dwEndDate = uint.Parse(szActivityDate);
            int test = 2;
            if (test == 1)
            {
                vrResvGet.dwClassKind = 8;
            }
            else if (test == 2)
            {
                vrResvGet.dwDevKind = 605;
                vrResvGet.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL;
                vrResvGet.dwCheckStat = 2;
            }

            RESVSHOW[] vtReserve;
            uResponse = m_Request.Reserve.GetReserveForShow(vrResvGet, out vtReserve);
            szReacher = "";
            for (int i = 0; vtReserve != null && i < vtReserve.Length; i++)
            {
                string szRepory = "";

                string szConetnt = vtReserve[i].szTestName.ToString();
                if (szConetnt.Length > 30)
                {
                    szConetnt = szConetnt.Substring(0, 30) + "...";
                }
                szActivityHistory += "<tr><td><font title=\"" + vtReserve[i].szTestName.ToString() + "\">" + szConetnt + "</font></span></td>";

                szActivityHistory += "<td>" + (vtReserve[i].dwPreDate % 10000) / 100 + "月" + (vtReserve[i].dwPreDate % 100) + "日" + "</td></tr>";
                                      
            }
            Response.Write(szActivityHistory);                          

            //if (common.Login(Request["id"], Request["pwd"]))
            //{
            //    UNIACCOUNT> vrAccInfo = null;
            //    vrAccInfo = (UNIACCOUNT>)Session["LOGIN_ACCINFO"];
            //    if (vrAccInfo.szEmail.ToString() == "" || vrAccInfo.szHandPhone.ToString() == "")
            //    {
            //        Response.Write("{\"MsgId\":1,\"Message\":\"该用户尚未激活,请先激活\"}");
            //        if (vrAccInfo.szHandPhone.ToString() == "" || vrAccInfo.szEmail.ToString() == "")
            //        {
            //            HttpContext.Current.Session["LOGIN_ACCINFO"] = null;
            //        }
            //    }
            //    else
            //    {
            //        Response.Write("{\"MsgId\":0,\"Message\":\"\"}");
            //    }
            //}           
        }
    }
}
