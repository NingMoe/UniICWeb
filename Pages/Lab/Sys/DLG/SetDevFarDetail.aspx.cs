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
    protected string m_szDept = "";
    protected string m_szLabKind = "";
	protected void Page_Load(object sender, EventArgs e)
    {
        m_Title = ConfigConst.GCDevName + "经费分配比例";
        uint uDevID=Parse(Request["id"]);
        if (uDevID == 0)
        {
            return;
        }
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        if (IsPostBack)
        {
            DEVFAR devFarUse = new DEVFAR();
            devFarUse.dwDevID = uDevID;
            devFarUse.dwFeeType = (uint)FEEDETAIL.DWFEETYPE.FEETYPE_USEDEV;
            devFarUse.dwTestRate = Parse(useTestRate.Value);
            devFarUse.dwOpenFundRate = Parse(useOpenRate.Value);
            devFarUse.dwServiceRate = Parse(useServiceRate.Value);
            uResponse=m_Request.Device.DevFARSet(devFarUse);

            DEVFAR devFarSample = new DEVFAR();
            devFarSample.dwDevID = uDevID;
            devFarSample.dwFeeType = (uint)FEEDETAIL.DWFEETYPE.FEETYPE_SAMPLE;
            devFarSample.dwTestRate = Parse(sampleTestRate.Value);
            devFarSample.dwOpenFundRate = Parse(sampleOpenRate.Value);
            devFarSample.dwServiceRate = Parse(sampleServiceRate.Value);
            uResponse=m_Request.Device.DevFARSet(devFarSample);

            DEVFAR devFarEnt= new DEVFAR();
            devFarEnt.dwDevID = uDevID;
            devFarEnt.dwFeeType = (uint)FEEDETAIL.DWFEETYPE.FEETYPE_ENTRUST;
            devFarEnt.dwTestRate = Parse(entTestRate.Value);
            devFarEnt.dwOpenFundRate = Parse(entOpenRate.Value);
            devFarEnt.dwServiceRate = Parse(entServiceRate.Value);
            uResponse = m_Request.Device.DevFARSet(devFarEnt);

            DEVFAR devConsumable= new DEVFAR();
            devConsumable.dwDevID = uDevID;
            devConsumable.dwFeeType = (uint)FEEDETAIL.DWFEETYPE.FEETYPE_SAMPLE;
            devConsumable.dwTestRate = Parse(consTestRate.Value);
            devConsumable.dwOpenFundRate = Parse(consOpenRate.Value);
            devConsumable.dwServiceRate = Parse(consServiceRate.Value);
            uResponse = m_Request.Device.DevFARSet(devConsumable);

            if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "设置失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox("设置成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
                
        }
      
        if (Request["op"] == "set")
        {
            bSet = true;

            DEVFARREQ vrGet = new DEVFARREQ();
            vrGet.dwDevID = Parse(Request["id"]);
            DEVFAR[] vtRes;
            if (m_Request.Device.DevFARGet(vrGet, out vtRes) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else
            {
                for (int i = 0; i < vtRes.Length; i++)
                {
                    if ((uint)vtRes[i].dwFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_USEDEV)
                    {
                        useTestRate.Value=vtRes[i].dwTestRate.ToString();
                        useOpenRate.Value=vtRes[i].dwOpenFundRate.ToString();
                        useServiceRate.Value=vtRes[i].dwServiceRate.ToString();
                    }
                    else if ((uint)vtRes[i].dwFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_SAMPLE)
                    {
                        sampleTestRate.Value = vtRes[i].dwTestRate.ToString();
                        sampleOpenRate.Value = vtRes[i].dwOpenFundRate.ToString();
                        sampleServiceRate.Value = vtRes[i].dwServiceRate.ToString();
                    }
                    else if ((uint)vtRes[i].dwFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_ENTRUST)
                    {
                        entTestRate.Value = vtRes[i].dwTestRate.ToString();
                        entOpenRate.Value = vtRes[i].dwOpenFundRate.ToString();
                        entServiceRate.Value = vtRes[i].dwServiceRate.ToString();
                    }
                    else if ((uint)vtRes[i].dwFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_SAMPLE)
                    {
                        consTestRate.Value = vtRes[i].dwTestRate.ToString();
                        consOpenRate.Value = vtRes[i].dwOpenFundRate.ToString();
                        consServiceRate.Value = vtRes[i].dwServiceRate.ToString();
                    }
                }
            }
        }       
    }
}
