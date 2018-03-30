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
    protected string m_szIdent = "";
    protected string m_szDevKind = "";
    protected string m_szResvPurpose= "";
    protected string m_dwPriority = "";
	protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack)
        {
            uint uFee = Parse(Request["dwUniFee"]);
            uint uUintTime = Parse(Request["dwUniTime"]);

            UNIFEE FeeValue = new UNIFEE();
            FEEREQ vrFeeGet = new FEEREQ();
            vrFeeGet.dwFeeSN = Parse(Request["dwFeeSN"]);
            UNIFEE[] feeList;
            if (m_Request.Fee.Get(vrFeeGet, out feeList) == REQUESTCODE.EXECUTE_SUCCESS && feeList != null && feeList.Length > 0)
            {
                FeeValue = feeList[0];
                FEEDETAIL detail = new FEEDETAIL();
                detail.dwFeeType = (uint)FEEDETAIL.DWFEETYPE.FEETYPE_USEDEV;
                detail.dwUnitFee = uFee * 100;
                detail.dwUnitTime = uUintTime;
                FeeValue.szFeeDetail = new FEEDETAIL[1];
                FeeValue.szFeeDetail[0] = new FEEDETAIL();
                FeeValue.szFeeDetail[0] = detail;
            }
            else
            {
                //新建收费标准
                uint? uMax = 0;
                uint uID = PRFee.FEE_BASE | PRFee.MSREQ_FEE_SET;
                if (GetMaxValue(ref uMax, uID, "dwFEESN"))
                {
                }
                FeeValue.dwFeeSN = uMax;
                UNIDEVICE setValue;
                if (!getDevByID(Request["dwID"].ToString(), out setValue))
                {
                    MessageBox("资源信息获取失败", "资源信息获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                    return;
                }
                FeeValue.dwDevKind = setValue.dwKindID;
                FeeValue.dwPriority = 2;
                FeeValue.dwPurpose = 55;
                FeeValue.szFeeName = setValue.szDevName.ToString() + "收费标准";

                FEEDETAIL detail = new FEEDETAIL();
                detail.dwFeeType = (uint)FEEDETAIL.DWFEETYPE.FEETYPE_USEDEV;
                detail.dwUnitFee = uFee * 100;
                detail.dwUnitTime = uUintTime;
                FeeValue.szFeeDetail = new FEEDETAIL[1];
                FeeValue.szFeeDetail[0] = new FEEDETAIL();
                FeeValue.szFeeDetail[0] = detail;
            }
            if (m_Request.Fee.Set(FeeValue, out FeeValue) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                Logger.trace(m_Request.szErrMessage);
                MessageBox("设置成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
            }
        }
        
        if (Request["op"] == "set")
        {
            UNIDEVICE setValue;
            if (!getDevByID(Request["dwID"].ToString(), out setValue))
            {
                MessageBox("获取失败", "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                return;
            }
            bSet = true;
            FEEREQ vrFeeGet = new FEEREQ();
            vrFeeGet.dwDevKind = setValue.dwKindID ;
            UNIFEE[] vtFee;
            if (m_Request.Fee.Get(vrFeeGet, out vtFee) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else
            {
                if (vtFee.Length == 0)
                {
                    //MessageBox("获取失败", "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                }
                else
                {
                    PutMemberValue("dwFeeSN", vtFee[0].dwFeeSN.ToString());
                    PutMemberValue("dwUniFee", (vtFee[0].szFeeDetail[0].dwUnitFee/100).ToString());
                    PutMemberValue("dwUniTime", (vtFee[0].szFeeDetail[0].dwUnitTime).ToString());


                    m_Title = "修改收费标准";
                }
            }
        }
        else
        {
           
        }
    }
}
