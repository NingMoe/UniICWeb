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
    protected string m_szSample = "";

	protected void Page_Load(object sender, EventArgs e)
    {
        m_Title = "测试内容";
        uint uDevID=Parse(Request["id"]);
        if (uDevID == 0)
        {
            return;
        }
        SAMPLEINFOREQ sampleReq = new SAMPLEINFOREQ();
        sampleReq.szReqExtInfo.szOrderKey = "szSampleName";
        sampleReq.szReqExtInfo.szOrderMode = "asc";
        SAMPLEINFO[] vtSample;
        if (m_Request.Reserve.GetSampleInfo(sampleReq, out vtSample) == REQUESTCODE.EXECUTE_SUCCESS && vtSample != null && vtSample.Length > 0)
        {
            for (int i = 0; i < vtSample.Length; i++)
            {
                string szName = vtSample[i].szSampleName + "(" + "一类:" + GetFee(vtSample[i].dwUnitFee1) + "二类:" + GetFee(vtSample[i].dwUnitFee3) + ")";
                m_szSample += GetInputItemHtml(CONSTHTML.checkBox, "sampleList",szName, vtSample[i].dwSampleSN.ToString());
            }
        }
        UNIDEVICE setDev;
        if(!getDevByID(uDevID.ToString(),out setDev))
        {

        }
        m_Title ="设置["+setDev.szDevName.ToString()+ "]样品库";
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        if (IsPostBack)
        {
            string szSample = Request["sampleList"];
            if (szSample != null && szSample != "")
            {
               string[] szSampleID=szSample.Split(',');
               SAMPLEINFO[] devSampleList = new SAMPLEINFO[szSampleID.Length];
               for (int i = 0; i < szSampleID.Length; i++)
               {
                   devSampleList[i] = new SAMPLEINFO();
                   devSampleList[i].dwSampleSN = Parse(szSampleID[i]);
                   devSampleList[i].dwDevID = uDevID;
               }
               setDev.DevSample = devSampleList;
            }
            uResponse = m_Request.Device.Set(setDev,out setDev);

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
            DEVREQ vrGet = new DEVREQ();
            vrGet.dwDevID = Parse(Request["id"]);
            UNIDEVICE[] vtRes;
            if (m_Request.Device.Get(vrGet, out vtRes) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else
            {
                string szSampleID = "";
                for (int i = 0;vtRes[0].DevSample!=null&&i < vtRes[0].DevSample.Length; i++)
                {
                    szSampleID += vtRes[0].DevSample[i].dwSampleSN.ToString() + ",";   
                }
                PutMemberValue("sampleList", szSampleID);
            }
        }       
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        if (ViewState["dwYearTermCode"] != null)
        {
            
        }
    }
}
