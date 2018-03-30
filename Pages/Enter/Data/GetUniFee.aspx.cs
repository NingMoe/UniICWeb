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
      
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        FEEREQ vrGet = new FEEREQ();
        vrGet.dwFeeSN = Parse(feeSN);
        UNIFEE[] vtRes;
        uResponse = m_Request.Fee.Get(vrGet, out vtRes);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            string szRes = "{\"message\":\"succ\"";
            int uLen = vtRes[0].szFeeDetail.Length;
            UNIFEE setValue = vtRes[0];
            for (int i = 0; i < uLen; i++)
            {
                uint uFeetType = (uint)setValue.szFeeDetail[i].dwFeeType;
                uint uFeeUint = (uint)setValue.szFeeDetail[i].dwUnitFee;
                uint uFeeTime = (uint)setValue.szFeeDetail[i].dwUnitTime;
                if (uFeetType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_USEDEV)
                {

                    szRes += ", \"useFeeUint\":\"" + uFeeUint.ToString() + "\"" + ", \"useTimeUint\":\"" + uFeeTime.ToString() + "\"";
                }
                else if (uFeetType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_SAMPLE)
                {
                    szRes += ", \"conFeeUint\":\"" + uFeeUint.ToString() + "\"" + ", \"conTimeUint\":\"" + uFeeTime.ToString() + "\"";
                }
                else if (uFeetType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_ENTRUST)
                {
                    szRes += ", \"entFeeUint\":\"" + uFeeUint.ToString() + "\"" + ", \"entTimeUint\":\"" + uFeeTime.ToString() + "\"";
                }
                else if (uFeetType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_SAMPLE)
                {
                    szRes += ", \"sampleFeeUint\":\""+uFeeUint.ToString()+"\""+ ", \"sampleTimeUint\":\""+uFeeTime.ToString()+"\"";
                }
              
            }
            szRes = szRes + "}";
            Response.Write(szRes);
           
        }
        else
        {
            Response.Write("{\"message\":\"" + m_Request.szErrMessage + "\"}");
        }
    }
        
}