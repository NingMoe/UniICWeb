using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class searchAccount : UniPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";
        MyString szOut = new MyString();

        string feeSN = Request["feeSN"];
        string szIdent = Request["ident"];

        string useFeeUint = Request["useFeeUint"];
        string useTimeUint = Request["useTimeUint"];

        string conFeeUint = Request["conFeeUint"];
        string conTimeUint = Request["conTimeUint"];

        string entFeeUint = Request["entFeeUint"];
        string entTimeUint = Request["entTimeUint"];

        string sampleFeeUint = Request["sampleFeeUint"];
        string sampleTimeUint = Request["sampleTimeUint"];

        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        FEEREQ vrGet = new FEEREQ();
        vrGet.dwFeeSN = Parse(feeSN);
       // vrGet.dwIdent = Parse(szIdent);
        UNIFEE[] vtRes;
        uResponse = m_Request.Fee.Get(vrGet, out vtRes);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            int uLen = vtRes[0].szFeeDetail.Length;
            UNIFEE setValue = vtRes[0];
            for (int i = 0; i < uLen; i++)
            {
                uint uFeetType = (uint)setValue.szFeeDetail[i].dwFeeType;
                if (uFeetType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_USEDEV)
                {
                   setValue.szFeeDetail[i].dwUnitFee = Parse(useFeeUint);
                    setValue.szFeeDetail[i].dwUnitTime = Parse(useTimeUint);
                }
                //else if (uFeetType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_CONSUMABLE) 20141017
                //{
                //    setValue.szFeeDetail[i].dwUnitFee = Parse(conFeeUint);
                //    setValue.szFeeDetail[i].dwUnitTime = Parse(conTimeUint);
                //}
                else if (uFeetType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_ENTRUST)
                {
                    setValue.szFeeDetail[i].dwUnitFee = Parse(entFeeUint);
                    setValue.szFeeDetail[i].dwUnitTime = Parse(entTimeUint);
                }
                else if (uFeetType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_SAMPLE)
                {
                    setValue.szFeeDetail[i].dwUnitFee = Parse(sampleFeeUint);
                    setValue.szFeeDetail[i].dwUnitTime = Parse(sampleTimeUint);
                }
            }
            
            uResponse = m_Request.Fee.Set(setValue, out setValue);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
            {
                Response.Write("{\"message\":\"succ\"}");
            }
            else
            {
                Response.Write("{\"message\":\"" + m_Request.szErrMessage + "\"}");
            }
        }
    }
        
}