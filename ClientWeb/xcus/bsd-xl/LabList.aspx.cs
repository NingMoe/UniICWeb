using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_bsd_xl_LabList : UniClientPage
{
    protected string LabList = "";
    protected string ClsTitle = "";
    protected string ClsList = "";
    protected string HideCls = "";
    protected string HideLab = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadPage();
        string clskind = Request["clsKind"];
        if (clskind != null)
        {
            if (clskind == "4096")//实验室
            {
                ClsTitle = "实验室";
                HideCls = "display:none";
                LabList = GetLab();
            }
            else if (clskind == "2048")//研讨室
            {
                ClsTitle = "研讨室";
                HideLab = "display:none";
                ClsList = GetDevCls();
            }
        }
    }

    private string GetDevCls()
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        DEVCLSREQ vrGet = new DEVCLSREQ();
        vrGet.dwKind = (uint)UNIDEVCLS.DWKIND.CLSCOMMONS_MEETINGROOM;//筛选会议室
        vrGet.szReqExtInfo.szOrderKey = "szClassName";
        vrGet.szReqExtInfo.szOrderMode = "ASC";
        UNIDEVCLS[] vtResult;
        uResponse = m_Request.Device.DevClsGet(vrGet, out vtResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtResult != null)
        {
            string rel = "";
            for (int i = 0; i < vtResult.Length; i++)
            {
                rel += "<option value='" + vtResult[i].dwClassID + "'>" + vtResult[i].szClassName + "</option>";
            }
            return rel;
        }
        else
        {
            MsgBox(m_Request.szErrMsg);
        }
        return "";
    }
    string GetLab()
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        LABREQ req = new LABREQ();
        req.szReqExtInfo.szOrderKey = "szLabName";
        req.szReqExtInfo.szOrderMode = "ASC";
        UNILAB[] vtResult;
        uResponse = m_Request.Device.LabGet(req, out vtResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtResult != null && vtResult.Length > 0)
        {
            string rel = "";
            for (int i = 0; i < vtResult.Length; i++)
            {
                rel += "<option value='" + vtResult[i].dwLabID + "'>" + vtResult[i].szLabName + "</option>";
            }
            return rel;
        }
        return "";
    }
    string GetManager()
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        ACCREQ vrGet = new ACCREQ();
        vrGet.dwIdent = (int)UNIACCOUNT.DWIDENT.EXTIDENT_MANAGER;
        UNIACCOUNT[] vtResult;
        uResponse = m_Request.Account.Get(vrGet, out vtResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtResult != null && vtResult.Length > 0)
        {
            string rel = "";
            for (int i = 0; i < vtResult.Length; i++)
            {
                rel += "<option value='" + vtResult[i].dwAccNo + "'>" + vtResult[i].szTrueName + "</option>";
            }
            return rel;
        }
        return "";
    }
}