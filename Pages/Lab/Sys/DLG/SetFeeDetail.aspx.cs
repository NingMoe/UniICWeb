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
    protected ArrayList listFeeType;
    protected ArrayList listPayKind;
    protected string szUsablePayKind = "";
    protected string m_checkInfo = "";
    protected int nIsAdminSup = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginResult"] != null)
        {
            ADMINLOGINRES adminAcc = (ADMINLOGINRES)Session["LoginResult"];
            uint uManRole = (uint)adminAcc.dwManRole;
            if ((uManRole & (uint)ADMINLOGINRES.DWMANROLE.MANSCOPE_LABCTR) > 0)
            {
                nIsAdminSup = 0;
            }
            else if ((uManRole & (uint)ADMINLOGINRES.DWMANROLE.MANSCOPE_LAB) > 0)
            {
                nIsAdminSup = 0;
            }
            else if ((uManRole & (uint)ADMINLOGINRES.DWMANROLE.MANSCOPE_ROOM) > 0)
            {
                nIsAdminSup = 0;
            }
        }
        string szOP=Request["op"];
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        FEEREQ vrGet = new FEEREQ();
        vrGet.dwFeeSN = Parse(Request["FeeSN"]);
        UNIFEE[] vtRes;
        UNIFEE setValue = new UNIFEE();
        uResponse = m_Request.Fee.Get(vrGet, out vtRes);
        int nFlag = -1;
        if (listPayKind == null)
        {
            listPayKind = GetListFromXml("UNIBILL_PayKind", 0, true);
        }
        for (int i = 0; i < listPayKind.Count; i++)
        {
            CStatue obj = (CStatue)listPayKind[i];
            string szTemp = "<label><input class=\"enum\" type=\"checkbox\" name=\"dwUsablePayKind\" value=\"" + obj.szValue + "\" /> " + obj.szName + "</label>";
            szUsablePayKind += szTemp;
        }
        m_checkInfo = GetInputHtmlFromXml(0, CONSTHTML.option, "", "FEEDETAIL_CHECKED", true);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            setValue = vtRes[0];
            FEEDETAIL[] feeDetail = setValue.szFeeDetail;
            if (feeDetail != null && feeDetail.Length > 0)
            {
                for (int i = 0; i < feeDetail.Length; i++)
                {
                    if ((uint)feeDetail[i].dwFeeType == Parse(Request["FeeType"]))
                    {
                        ConFeeDetail(feeDetail[i]);
                        nFlag = i;
                        break;
                    }
                }
            }
        }
        if (IsPostBack)
        {
            if (Request["op"] == "set")
            {
                if (setValue.dwFeeSN == null)
                {
                    MessageBox("未找到对于收费类别", "修改失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                    return;
                }
                FEEDETAIL feeDetalHtml;              
                GetHTTPObj(out feeDetalHtml);
                feeDetalHtml.dwUsablePayKind = CharListToUint(Request["dwUsablePayKind"]);
                if (feeDetalHtml.dwFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_TIMEOUT)//超时费特殊处理
                {
                    if (feeDetalHtml.dwDefaultCheckStat.ToString() == "0")
                    {
                        feeDetalHtml.dwDefaultCheckStat = ((uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_OK);
                    }
                    else
                    {
                        feeDetalHtml.dwDefaultCheckStat = (0);
                    }
                }
                if (nFlag > -1)
                {
                    feeDetalHtml.dwFeeType = setValue.szFeeDetail[nFlag].dwFeeType;
                    setValue.szFeeDetail[nFlag] = feeDetalHtml;
                }
                uResponse = m_Request.Fee.Set(setValue, out setValue);
                if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    MessageBox("修改成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                    return;
                }
                else
                {
                    MessageBox(m_Request.szErrMessage.ToString(), "提示", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                    return;
                }
            }
            else
            {
                if (setValue.dwFeeSN == null)
                {
                    MessageBox("未找到对于收费类别", "修改失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                    return;
                }
                FEEDETAIL feeDetalHtml;
                GetHTTPObj(out feeDetalHtml);
                if (feeDetalHtml.dwFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_TIMEOUT)//超时费特殊处理
                {
                    if (feeDetalHtml.dwDefaultCheckStat.ToString() == "0")
                    {
                        feeDetalHtml.dwDefaultCheckStat = ((uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_OK);
                    }
                    else
                    {
                        feeDetalHtml.dwDefaultCheckStat = (0);
                    }
                }
                int uLen=setValue.szFeeDetail.Length;
                setValue.szFeeDetail[uLen] = feeDetalHtml;

                uResponse = m_Request.Fee.Set(setValue, out setValue);
                if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    MessageBox("新建成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                    return;
                }
            } 
        }      
       
    }
    private void ConFeeDetail(FEEDETAIL feeDetail)
    {
        feeType.InnerText = GetFeeTypeName((uint)feeDetail.dwFeeType);
        if (feeDetail.dwFeeType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_TIMEOUT)//超时费特殊处理
        {
            if (feeDetail.dwDefaultCheckStat.ToString() == "0")
            {
                feeDetail.dwDefaultCheckStat = ((uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_OK);
            }
            else
            {
                feeDetail.dwDefaultCheckStat = (0);
            }
        }
        PutHTTPObj(feeDetail);
    }
    private string GetFeeTypeName(uint szValue)
    {
        string szRes = "";
        if (listFeeType == null)
        {
            listFeeType = GetListFromXml("FEEDETAIL_FeeType", 0, true);
        }
        for (int i = 0; i < listFeeType.Count; i++)
        {
            CStatue temp = (CStatue)listFeeType[i];
            if (temp.szValue == szValue.ToString())
            {
                return temp.szName;
            }
        }
        return szRes;
    }
}
