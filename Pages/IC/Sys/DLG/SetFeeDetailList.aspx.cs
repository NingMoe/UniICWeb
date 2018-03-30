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

public partial class _Default : UniPage
{
    protected bool bSet = false;
    protected string m_Title = "";
    protected string m_szIdent = "";
    protected string m_szDevKind = "";
    protected string m_szResvPurpose= "";
    protected string m_dwPriority = "";
    protected string m_szFee = "";
    protected string m_szFeeHtml = "";
    protected string m_szFeeDetailType = "";
    protected string m_szBillPayKind = "";
    protected string m_szOut = "";
    protected ArrayList list;
	protected void Page_Load(object sender, EventArgs e)
    {              
        FeeSN.Value = Request["dwID"];
        if (Request["FeeType"] != null)
        {
            Del(Request["FeeSN"]);          
        }
        else if (Request["op"] == "set")
        {
            bSet = true;
            FEEREQ vrFeeGet = new FEEREQ();
            vrFeeGet.dwFeeSN = Parse(Request["dwID"]);
            UNIFEE[] vtFee;
            if (m_Request.Fee.Get(vrFeeGet, out vtFee) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else
            {
                if (vtFee.Length == 0)
                {
                    MessageBox("获取失败", "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                }
                else
                {
                    PutJSObj(vtFee[0]);
                    m_Title = "设置收费明细";
                    PutFeeDetailToHtml(vtFee[0]);
                }
            }
        }
       
    }
    private void Del(string szID)
    {        
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        FEEREQ vrGet = new FEEREQ();
        vrGet.dwFeeSN = Parse(szID);
        UNIFEE[] vtRes;
        UNIFEE setVale = new UNIFEE();
        uResponse = m_Request.Fee.Get(vrGet, out vtRes);
        uint uFeeType=Parse(Request["FeeType"]);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            setVale = vtRes[0];
            int uLen = 0;
            uLen = vtRes[0].szFeeDetail.Length;
            if (uLen > 1)
            {
                uLen = uLen - 1;
                FEEDETAIL[] feeDetailList = new FEEDETAIL[uLen];
                int uCount = 0;
                for (int i = 0; i < vtRes[0].szFeeDetail.Length; i++)
                {
                    if (!((uint)vtRes[0].szFeeDetail[i].dwFeeType == uFeeType))
                    {
                        feeDetailList[uCount] = new FEEDETAIL();
                        feeDetailList[uCount] = vtRes[0].szFeeDetail[i];
                        uCount++;
                    }
                }

                setVale.szFeeDetail = feeDetailList;
            }
            else
            {
                setVale.szFeeDetail = null;
               
            }
           
            uResponse = m_Request.Fee.Set(setVale, out setVale);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox("删除成功", "删除成功", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
            }
            else
            {
                MessageBox(m_Request.szErrMessage, "删除失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
             
            }
        }

       
    }
    private void PutFeeDetailToHtml(UNIFEE uniFee)
    {       
        FEEDETAIL[] vtFee = uniFee.szFeeDetail;
        if (vtFee == null || vtFee.Length == 0)
        {
            return;
        }
      
        for (int i = 0; i < vtFee.Length; i++)
        {
            m_szOut += "<tr>";
            m_szOut += "<td data-id=" + (uint)vtFee[i].dwFeeType + ">" + GetFeeTypeName((uint)vtFee[i].dwFeeType) + "</td>";
            m_szOut += "<td>" + vtFee[i].dwUnitFee + "</td>";
            m_szOut += "<td>" + vtFee[i].dwUnitTime + "</td>";
            m_szOut += "<td>" + GetJustName((uint)vtFee[i].dwUsablePayKind, "UNIBILL_PayKind") + "</td>";
            if ((uint)vtFee[i].dwFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_TIMEOUT)
            {
                if (vtFee[i].dwDefaultCheckStat.ToString() == "0")
                {
                    vtFee[i].dwDefaultCheckStat = ((uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_OK);
                }
                else
                {
                    vtFee[i].dwDefaultCheckStat = (0);
                }
            }
            if ((uint)vtFee[i].dwDefaultCheckStat==0)
            {
                m_szOut += "<td>" +"无需审核" + "</td>";            
            }
            else
            {
            m_szOut += "<td>" + GetJustName((uint)vtFee[i].dwDefaultCheckStat, "FEEDETAIL_CHECKED") + "</td>";            
            }
            m_szOut += "<td><div class='OPTD'></div></td>";
            m_szOut += "</tr>";         
        }
     
    }
    private string GetFeeTypeName(uint szValue)
    {
        string szRes = "";
        if (list == null)
        {
            list = GetListFromXml("FEEDETAIL_FeeType", 0, true);
        }
        for (int i = 0; i < list.Count; i++)
        {
            CStatue temp = (CStatue)list[i];
            if (temp.szValue == szValue.ToString())
            {
                return temp.szName;
            }
        }
        return szRes;
    }
}
