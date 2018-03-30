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
    protected string m_szSta = "";
	protected void Page_Load(object sender, EventArgs e)
	{        
        if (IsPostBack)
        {
            REPAIROVER setValue;
               // setValue.dwDamageTime = Get1970Seconds(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            GetHTTPObj(out setValue);
            setValue.dwDevID = Parse(Request["devid"]); ;
            setValue.dwRepareDate = GetDate(DateTime.Now.ToString("yyyy-MM-dd"));
            if (Request["dwRepareCost"] != null && Request["dwRepareCost"] != "")
            {
                setValue.dwRepareCost = (uint)(FloatParse(Request["dwRepareCost"]) * 100);
            }
            if (Request["dwPay1"] != null && Request["dwPay1"] != "")
            {
                setValue.dwPay1 = (uint)(FloatParse(Request["dwPay1"]) * 100);
            }
            if (Request["dwPay2"] != null && Request["dwPay2"] != "")
            {
                setValue.dwPay2 = (uint)(FloatParse(Request["dwPay2"]) * 100);
            }
            if (m_Request.Assert.RepareOver(setValue, out setValue) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "录入失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox("录入成功", "录入成功", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
        }
        m_szSta += GetInputHtmlFromXml((uint)DEVDAMAGEREC.DWSTATUS.REPARE_FAIL + (uint)DEVDAMAGEREC.DWSTATUS.REPARE_OK, CONSTHTML.option, "", "DEVDAMAGEREC_status", false);
       

        if (Request["op"] == "set")
        {
            bSet = true;

            DEVDAMAGERECREQ vrGet = new DEVDAMAGERECREQ();
            vrGet.dwSID = Parse(Request["dwSID"]);
            DEVDAMAGEREC[] vtRes;
            if (m_Request.Device.DevDamageRecGet(vrGet, out vtRes) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else
            {
                if (vtRes.Length == 0)
                {
                    MessageBox("获取失败", "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                }
                else
                {
                   
                    PutJSObj(vtRes[0]);
                    if (vtRes[0].dwRepareCost != null)
                    {
                        ViewState["dwRepareCost"] = GetFee(vtRes[0].dwRepareCost);
                    }
                    if (vtRes[0].dwPay1 != null)
                    {
                        ViewState["dwPay1"] = GetFee(vtRes[0].dwPay1);
                    }
                    if (vtRes[0].dwPay2 != null)
                    {
                        ViewState["dwPay2"] = GetFee(vtRes[0].dwPay2); 
                    }
                    m_Title = "修复" + "【" + vtRes[0].szDevName + "】";
                   
                }
            }
        }
        else
        {
            m_Title = "修复设置";
        }
	}
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreLoad(e);
        if (ViewState["dwRepareCost"] != null && ViewState["dwRepareCost"].ToString() != "")
        {
            PutMemberValue("dwRepareCost", ViewState["dwRepareCost"].ToString().Replace("元", ""));
        }
        if (ViewState["dwPay1"] != null && ViewState["dwPay1"].ToString() != "")
        {
            PutMemberValue("dwPay1", ViewState["dwPay1"].ToString().Replace("元", ""));
        }
        if (ViewState["dwPay2"] != null && ViewState["dwPay2"] != "")
        {
            PutMemberValue("dwPay2", ViewState["dwPay2"].ToString().Replace("元",""));
        }
    }
}
