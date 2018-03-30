using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using Newtonsoft.Json;
public partial class Resv_searchCls :UniPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        MONIREQ vrGet = new MONIREQ();
        vrGet.dwStaSN = 1;
       // vrGet.dwReqProp = (uint)MONIREQ.DWREQPROP.MONIREQ_NEEDINDEXTBL;
        MODMONI[] vtRes;
        uResponse = m_Request.UniMoni.MoniGet(vrGet, out vtRes);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            MyString szOut = new MyString();
            vtRes[0].szStatInfo = vtRes[0].szStatInfo.Replace(";", "<br />");
            szOut += JsonConvert.SerializeObject(vtRes); ;
            Response.Write(szOut);
        }
    }
}