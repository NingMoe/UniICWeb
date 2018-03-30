using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using Util;

public partial class ClientWeb_xcus_bsd_xl_LabDetail : UniClientPage
{
    uint devID;
    protected string ovDetail = "";
    protected string ovPosition = "";
    protected string ovHelp = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadPage();
        if (Request["dev"] == null) return;
        devID = Convert.ToUInt32(Request["dev"]);
        InitDevInfo(devID);
        InitOV();
    }

    private void InitOV()
    {
        ovDetail = GetContent("sy1");
        ovPosition = GetContent("sy2");
        ovHelp = GetContent("sy3");
    }
    string GetContent(string type)
    {
        return GetXmlContent(devID.ToString(), type);
    }
    private void InitDevInfo(uint devID)
    {
        Session["CUR_DEV"] = null;
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        DEVREQ vrGet = new DEVREQ();
        UNIDEVICE[] vtResult;
        vrGet.dwDevID = devID;
        uResponse = m_Request.Device.Get(vrGet, out vtResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtResult.Length > 0)
        {
            UNIDEVICE dev = vtResult[0];
            UNIDEVKIND kind = GetDevKind(dev.dwKindID);
            Session["CUR_DEV"] = vtResult[0];
        }
    }
    string ConvertStr(object obj)
    {
        if (obj == null)
        {
            return "";
        }
        else
        {
            return obj.ToString();
        }
    }
}