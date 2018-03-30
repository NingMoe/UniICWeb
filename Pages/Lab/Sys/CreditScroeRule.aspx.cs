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
        
        if (Request["delID"] != null)
        {
            
            DelFee(Request["delID"]);
        }
        CREDITSCOREREQ vrParameter = new CREDITSCOREREQ();
        
        CREDITSCORE[] vrResult;

        if (m_Request.System.CreditScoreGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null && vrResult.Length>0)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                uint uNum = (uint)vrResult[i].dwUseNum;
                m_szOut += "<tr>";
                m_szOut += "<td data-id=\"" + vrResult[i].dwID.ToString() + "\" data-creditsn=\"" + vrResult[i].dwCreditSN.ToString() + "\" data-ctsn=\"" + vrResult[i].dwCTSN.ToString() + "\">" + vrResult[i].szCTName + "</td>";
                m_szOut += "<td>" + vrResult[i].szCreditName + "</td>";
                m_szOut += "<td>" + GetJustName(vrResult[i].dwUsePurpose, "ResvPurpose") + "</td>";
                uint uScoreCycle = (uint)vrResult[i].dwScoreCycle;
                if (uScoreCycle == 1)
                {
                    m_szOut += "<td>每年</td>";
                }
                else
                {
                    m_szOut += "<td>每学期</td>";
                }
                m_szOut += "<td>" + vrResult[i].dwMaxScore + "</td>";
                m_szOut += "<td>" + vrResult[i].dwForbidUseTime + "</td>";

                if (vrResult[i].dwCreditSN == (uint)CREDITKIND.DWCREDITSN.CREDIT_RESVCANCEL)
                {
                     m_szOut += "<td>";
                     if (uNum>=1&&vrResult[i].dwMinValue1 != null)
                    {
                        m_szOut +="提前" + vrResult[i].dwMinValue1 + "分钟到" + vrResult[i].dwMaxValue1 + "分钟取消预约减" + vrResult[i].dwCreditScore1 + "分；";
                    }

                     if (uNum >=2 && vrResult[i].dwMinValue2 != null)
                    {
                        m_szOut += "提前" + vrResult[i].dwMinValue2 + "分钟到" + vrResult[i].dwMaxValue2 + "分钟取消预约减" + vrResult[i].dwCreditScore2 + "分；";
                    }

                     if (uNum >=3 && vrResult[i].dwMinValue3 != null)
                    {
                        m_szOut += "提前" + vrResult[i].dwMinValue3 + "分钟到" + vrResult[i].dwMaxValue3 + "分钟取消预约减" + vrResult[i].dwCreditScore3 + "分；";
                    }

                     if (uNum >=4 && vrResult[i].dwMinValue4 != null)
                    {
                        m_szOut += "提前" + vrResult[i].dwMinValue4 + "分钟到" + vrResult[i].dwMaxValue4 + "分钟取消预约减" + vrResult[i].dwCreditScore4 + "分；";
                    }
                    m_szOut += "</td>";

                }
                else if (vrResult[i].dwCreditSN == (uint)CREDITKIND.DWCREDITSN.CREDIT_RESVLATE)
                {
                    m_szOut += "<td>";
                 
                    if (uNum>=1&&vrResult[i].dwMinValue1 != null)
                    {
                        m_szOut += "预约迟到" + vrResult[i].dwMinValue1 + "分钟到" + vrResult[i].dwMaxValue1 + "分钟减" + vrResult[i].dwCreditScore1 + "分；";
                    }

                    if (uNum >= 2 && vrResult[i].dwMinValue2 != null)
                    {
                        m_szOut += "预约迟到" + vrResult[i].dwMinValue2 + "分钟到" + vrResult[i].dwMaxValue2 + "分钟减" + vrResult[i].dwCreditScore2 + "分；";
                    }

                    if (uNum >= 3 && vrResult[i].dwMinValue3 != null)
                    {
                        m_szOut += "预约迟到" + vrResult[i].dwMinValue3 + "分钟到" + vrResult[i].dwMaxValue3 + "分钟减" + vrResult[i].dwCreditScore3 + "分；";
                    }

                    if (uNum >= 4 && vrResult[i].dwMinValue4 != null)
                    {
                        m_szOut += "预约迟到" + vrResult[i].dwMinValue4 + "分钟到" + vrResult[i].dwMaxValue4 + "分钟减" + vrResult[i].dwCreditScore4 + "分；";
                    }
                    m_szOut += "</td>";

                }
                else if (vrResult[i].dwCreditSN == (uint)CREDITKIND.DWCREDITSN.CREDIT_NORMALUSE)
                {
                    m_szOut += "<td>";
                    m_szOut += "正常使用加" + vrResult[i].dwCreditScore1 + "分";
                    m_szOut += "</td>";
                }
                else
                {
                    m_szOut += "<td></td>";
                }
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.System);
        }
        PutBackValue();
      
    }
    private void DelFee(string szID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        UNIFEE fee = new UNIFEE();
        fee.dwFeeSN = Parse(szID);
        uResponse = m_Request.Fee.Del(fee);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
            
            MessageBox(m_Request.szErrMessage,"提示",MSGBOX.ERROR);
        }
    }
}
