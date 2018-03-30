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
    private string szKindName = "";
    protected string szWeek = "";
    protected string TimeHour = "";
    protected string TimeMin = "";
    protected string szCamp = "";
    protected string szBuilding = "";
    protected string szKinds = "";
    protected void Page_Load(object sender, EventArgs e)
    {
       
        UNICAMPUS[] vtCamp = GetAllCampus();
        if (vtCamp != null && vtCamp.Length > 0)
        {
            for (int i = 0; i < vtCamp.Length; i++)
            {
                szCamp += GetInputItemHtml(CONSTHTML.option, "", vtCamp[i].szCampusName, vtCamp[i].dwCampusID.ToString());
            }
        }
        UNIBUILDING[] vtBuilding = getAllBuilding();
        for (int i = 0; i < vtBuilding.Length; i++)
        {
            if (vtBuilding[i].dwCampusID.ToString() == vtCamp[0].dwCampusID.ToString())
            {
                szBuilding += GetInputItemHtml(CONSTHTML.option, "", vtBuilding[i].szBuildingName.ToString(), vtBuilding[i].dwBuildingID.ToString());
            }
        }


        if (IsPostBack)
        {
            string szDevIDs = Request["devidchk"];
            string[] szDevList = szDevIDs.Split(',');
            uint uFee = Parse(Request["dwUniFee"]);
            uint uUintTime = Parse(Request["dwUniTime"]);
            for (int i = 0; i < szDevList.Length; i++)
            {
                
                UNIDEVICE setDev;
                if (szDevList[i] != ""&&getDevByID(szDevList[i].ToString(),out setDev))
                {
                    uint uDevKind = (uint)setDev.dwKindID;
                    FEEREQ feeGet = new FEEREQ();
                    UNIFEE[] feeList;
                    feeGet.dwDevKind = uDevKind;
                    UNIFEE FeeValue = new UNIFEE();
                    if (m_Request.Fee.Get(feeGet, out feeList) == REQUESTCODE.EXECUTE_SUCCESS && feeList != null && feeList.Length > 0)
                    {
                        //修改
                        FeeValue = feeList[0];
                        FEEDETAIL detail = new FEEDETAIL();
                        detail.dwFeeType = (uint)FEEDETAIL.DWFEETYPE.FEETYPE_USEDEV;
                        detail.dwUnitFee = uFee * 100;
                        detail.dwUnitTime = uUintTime;
                        FeeValue.szFeeDetail = new FEEDETAIL[1];
                        FeeValue.szFeeDetail[0] = new FEEDETAIL();
                        FeeValue.szFeeDetail[0] = detail;
                    }
                    else {
                        //新建收费标准
                        uint? uMax = 0;
                        uint uID = PRFee.FEE_BASE | PRFee.MSREQ_FEE_SET;
                        if (GetMaxValue(ref uMax, uID, "dwFEESN"))
                        {
                        }
                        FeeValue.dwFeeSN = uMax;
                        FeeValue.dwDevKind = uDevKind;
                        FeeValue.dwPriority = 2;
                        FeeValue.dwPurpose = 55;
                        FeeValue.szFeeName = setDev.szDevName.ToString() + "收费标准";

                        FEEDETAIL detail = new FEEDETAIL();
                        detail.dwFeeType = (uint)FEEDETAIL.DWFEETYPE.FEETYPE_USEDEV;
                        detail.dwUnitFee = uFee * 100;
                        detail.dwUnitTime = uUintTime;
                        FeeValue.szFeeDetail = new FEEDETAIL[1];
                        FeeValue.szFeeDetail[0] = new FEEDETAIL();
                        FeeValue.szFeeDetail[0] = detail;
                    }
                    if (m_Request.Fee.Set(FeeValue, out FeeValue) != REQUESTCODE.EXECUTE_SUCCESS)
                    {
                        Logger.trace(m_Request.szErrMessage);
                    }
                }
            }
            MessageBox("新建成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
            return;

        }
        m_Title = "新建收费标准";

    }
}
