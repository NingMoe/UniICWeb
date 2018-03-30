using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class Resv_searchCls : UniWebLib.UniPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        m_bRemember = false;

        string szTerm = Request["term"];
        string szID = Request["devID"];
        uint uKind=Parse(Request["kind"]);
        Response.CacheControl = "no-cache";

        DEVREQ vrq = new DEVREQ();
        UNIDEVICE[] vtRes;
        vrq.dwDevID = Parse(szID);
        if (m_Request.Device.Get(vrq, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null)
        {
            SAMPLEINFO[] devSample = vtRes[0].DevSample;
         
            MyString szOut = new MyString();
            szOut += "[";
            string szRes ="";
            for (int i = 0; devSample != null && i < devSample.Length; i++)
            {
                if (devSample[i].szSampleName.IndexOf(szTerm) > -1)
                {
                    uint uFee = 0;
                    if (uKind == 1)
                    {
                        uFee = (uint)devSample[i].dwUnitFee1;
                    }
                    else if (uKind == 2)
                    {
                        uFee = (uint)devSample[i].dwUnitFee2;
                    }
                    else if (uKind == 3)
                    {
                        uFee = (uint)devSample[i].dwUnitFee3;
                    }
                    szRes += "{\"id\":\"" + devSample[i].dwSampleSN + "\",\"label\": \"" + devSample[i].szSampleName + "\",\"UnitFee\":\"" + uFee.ToString()+ "\"}";
                    if (i < devSample.Length - 1)
                    {
                        szRes += ",";
                    }
                }
            }
            if(szRes.EndsWith(","))
            {
                szRes=szRes.Substring(0,szRes.Length-1);
            }
            szOut += szRes;

            szOut += "]";
            Response.Write(szOut);
        }
        else
        {
            Response.Write("[ ]");
        }
    }
}