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

public partial class Sub_Lab : UniPage
{
    protected string m_szOpts = "";
    protected MyString m_szOut = new MyString();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        CREDITRECREQ vrParameter = new CREDITRECREQ();        
        string szKey = Request["szGetKey"];
        string szID = Request["delID"];
        string szOp = Request["op"];
        if (szKey != null && szKey != "")
        {
           //vrParameter.dwGetType = (uint)DISCIRECREQ.DWGETTYPE.DISCIRECGET_BYACCNO;
           vrParameter.dwAccNo =Parse(szKey);
        }
        if (szID != null && szID != "")
        {
            Del(szID);
        }
        else
        {
           // vrParameter.dwGetType = (uint)PUNISHRECREQ.DWGETTYPE.PUNISHRECGET_BYALL;
        }
        if (szOp == "all")
        {
            DelAll();
        }
        if (!IsPostBack)
        {
            dwStartDate.Value = DateTime.Now.AddMonths(0).ToString("yyyy-MM-dd");
            dwEndDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
        }

        if (dwStartDate.Value != "" && dwEndDate.Value != "")
        {
            vrParameter.dwStartDate = DateToUint(dwStartDate.Value);
            vrParameter.dwEndDate = DateToUint(dwEndDate.Value);
        }
        CREDITREC[] vrResult;       
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        vrParameter.szReqExtInfo.szOrderKey = "dwOccurTime";
        vrParameter.szReqExtInfo.szOrderMode = "desc";
        if (m_Request.System.CreditRecGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {            
            uint uTimeNow = Get1970Seconds(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            for (int i = 0; i < vrResult.Length; i++)
            {               
                m_szOut += "<tr>";
                m_szOut += "<td data-dwCreditSN=\""+vrResult[i].dwCreditSN.ToString()+"\" data-dwCTSN=\""+vrResult[i].dwCTSN.ToString()+"\" data-id=\"" + vrResult[i].dwSID.ToString() + "\">" + vrResult[i].dwSID.ToString() + "</td>";
                m_szOut += "<td class='lnkAccount' data-id='" + vrResult[i].dwAccNo.ToString() + "' title='查看个人信息'><a href=\"#\">" +vrResult[i].szTrueName+"("+vrResult[i].szPID.ToString() + ")</a></td>";
                m_szOut += "<td>" + vrResult[i].szDevName + "</td>";
                int nThisUseCScore = (int)vrResult[i].dwThisUseCScore;
                m_szOut += "<td>" + nThisUseCScore + "</td>";
                int nLef = (int)vrResult[i].dwLeftCScore;
               // m_szOut += "<td>" + nLef + "</td>";
                m_szOut += "<td>" + Get1970Date(vrResult[i].dwOccurTime) + "</td>";
                m_szOut += "<td>" +GetJustName(vrResult[i].dwUserCStat,"UserCStat") + "</td>";
                m_szOut += "<td>" + (vrResult[i].szMemo) + "</td>";
                if (((uint)vrResult[i].dwUserCStat & (uint)CREDITREC.DWUSERCSTAT.USERCSTAT_VALID) > 0)
                {
                    m_szOut += "<td><div class='OPTD'></div></td>";
                }
                else
                {
                    m_szOut += "<td></td>";
                }
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.System);
        }
        PutBackValue();
    }
    protected void Del(string szID)
    {
        string[] szIDList = szID.Split(',');
        for (int i = 0; i < szIDList.Length; i++)
        {
            string szIDTemp = szIDList[i];
            if (szIDTemp == null || szIDTemp == "")
            {
                continue;
            }
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            CREDITRECREQ vrGet = new CREDITRECREQ();
            vrGet.dwSID = Parse(szIDTemp);
            CREDITREC[] vtRes;

            uResponse = m_Request.System.CreditRecGet(vrGet, out vtRes);
            if (vtRes != null && vtRes.Length > 0)
            {
                ADMINCREDIT setvale = new ADMINCREDIT();
                setvale.dwAccNo = vtRes[0].dwAccNo;
                setvale.szTrueName = vtRes[0].szTrueName;
                setvale.dwCTSN = vtRes[0].dwCTSN;
                setvale.dwCreditSN = (uint)CREDITKIND.DWCREDITSN.CREDIT_CORRECTERR;
                setvale.dwSubjectID = vtRes[0].dwSID;
                setvale.szReason = "取消违约";
                uResponse = m_Request.System.AdminCreditDo(setvale);
            }
        }
    }
    protected void DelAll()
    {
        CREDITRECREQ vrParameter = new CREDITRECREQ();
        vrParameter.dwStartDate = DateToUint(dwStartDate.Value);
        vrParameter.dwEndDate = DateToUint(dwEndDate.Value);
        CREDITREC[] vrResult;
        vrParameter.szReqExtInfo.dwNeedLines = 10000;
        vrParameter.szReqExtInfo.dwStartLine = 0;
        if (m_Request.System.CreditRecGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                ADMINCREDIT setvale = new ADMINCREDIT();
                setvale.dwAccNo = vrResult[i].dwAccNo;
                setvale.szTrueName = vrResult[i].szTrueName;
                setvale.dwCTSN = vrResult[i].dwCTSN;
                setvale.dwCreditSN = (uint)CREDITKIND.DWCREDITSN.CREDIT_CORRECTERR;
                setvale.dwSubjectID = vrResult[i].dwSID;
                setvale.szReason = "取消违约";
                 m_Request.System.AdminCreditDo(setvale);
            }

        }
    }
}
