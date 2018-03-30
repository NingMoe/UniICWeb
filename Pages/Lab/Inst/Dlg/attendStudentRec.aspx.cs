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
using UniWebLib;
using System.Text;
using System.Reflection;
using System.IO;
using LumenWorks.Framework.IO.Csv;

public partial class _Default : UniPage
{
    protected bool bSet = false;
    protected string m_Title = "";
    protected string m_szOut = "";
    protected string szOut = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        RESVRECREQ vrGet = new RESVRECREQ();

        vrGet.dwGetType = (uint)RESVRECREQ.DWGETTYPE.RESVRECGET_BYID;
        vrGet.dwStartDate = Parse(Get1970Date(Parse(Request["time"]), "yyyyMMdd"));
        vrGet.dwEndDate = Parse(Get1970Date(Parse(Request["time"]), "yyyyMMdd"));
        vrGet.szGetKey = (Request["dwResvID"]);
        UNIRESVREC[] vtRes;
        string szLogonName = Request["szLogonName"];
        if (szLogonName != null && szLogonName != "")
        {
            UNIACCOUNT accinfo = new UNIACCOUNT();
        }
            /*
        CUniStruct<ACCREQ> vrGetAcc = new CUniStruct<ACCREQ>();
        vrGetAcc.dwGetType = ((uint)ACCREQ_CONST.ACCGET_BYLOGONNAME);
        vrGetAcc.szGetID =(szLogonName);
        CUniStructArray<UNIACCOUNT> vtAcc;
        if (m_Request.Account.Get(vrGetAcc, out vtAcc) == REQUESTCODE.EXECUTE_SUCCESS && vtAcc != null && vtAcc.GetLength() > 0)
        {
            vrGet.dwAccNo = (vtAcc[0].dwAccNo);
            xmlSetAttribute(outDoc, "//input[@name='szLogonName']", "value", szLogonName);
        }
             */
      
        uint uFee = 0;
        uint.TryParse(Request["dwFee"], out uFee);
        uint uNomal = 0;
        uint uUno = 0;
        uint uLowDo = 0;
        uResponse = m_Request.Report.ResvRecGet(vrGet, out vtRes);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes.Length > 0)
        {
            uint uTotalResvMin = ((uint)vtRes[0].dwPreEnd - (uint)vtRes[0].dwPreBegin) / 60;

            for (int i = 0; i < vtRes.Length; i++)
            {
                uint uUseTime = (uint)(uint)vtRes[i].dwUseTime;
                uint uState = (uint)vtRes[i].dwStatus;
                if (uUseTime == 0)
                {
                    vtRes[i].szMemo = ("未出席");
                    uUno = uUno + 1;
                }
                else if (((uUseTime / (uTotalResvMin * 1.0))) * 100 < 20)
                {
                    vtRes[i].szMemo = ("早退");
                    uLowDo = uLowDo + 1;
                }
                else
                {
                    if (((uUseTime / (uTotalResvMin * 1.0))) * 100 > uFee)
                    {
                        vtRes[i].szMemo = ("出席");
                        uNomal = uNomal + 1;
                    }
                    else
                    {
                        vtRes[i].szMemo = ("早退");
                        uLowDo = uLowDo + 1;
                    }
                }
                m_szOut += "<tr>";
                m_szOut += "<td>"+vtRes[i].szPID+"</td>";
                m_szOut += "<td>" + vtRes[i].szTrueName + "</td>";
                m_szOut += "<td>" +(vtRes[i].dwUseTime) + "</td>";
                m_szOut += "<td>" + vtRes[i].szMemo + "</td>";
                m_szOut += "</tr>";
            }
   szOut= "总共" + vtRes.Length.ToString() + "人：出席" + uNomal.ToString() + "人，早退" + uLowDo.ToString() + "人，未出席" + uUno.ToString() + "人";    
            //xmlSetNodeValue(outDoc, "//label[@id='numTotal']", "
        }
    }
}

