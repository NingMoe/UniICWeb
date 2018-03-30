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
    protected string m_Title = "审核";
    protected string szID="";
    protected string szGroupStudent = "";
    protected string szOwnerName = "";
    protected string szOwneTel="";
    protected string szTurtorTel = "";
    protected string szTestName = "";
    protected string szResvTime = "";
    protected string szDevName = "";
    protected string szURl = "";
    protected string szOpHref = "";
    protected string szMemo="";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            szID = Request["id"];
            szidh.Value = szID;
            RESVREQ vrGet = new RESVREQ();
            
            vrGet.dwResvID=Parse(szID);
            UNIRESERVE[] vtRes;
            uResponse = m_Request.Reserve.Get(vrGet, out vtRes);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
            {
                if (vtRes[0].szTestName != null && vtRes[0].szTestName != "")
                {
                    szTestName = vtRes[0].szTestName.ToString();
                }
                if (vtRes[0].szMemo != null && vtRes[0].szMemo != "")
                {
                    szMemo = vtRes[0].szMemo.ToString();
                }
                UNIACCOUNT setTur;
                GetAccByAccno(vtRes[0].dwOwner.ToString(), out setTur);
                if (setTur.dwAccNo != null)
                {
                    szOwnerID.Value = vtRes[0].dwOwner.ToString();
                    szOwnerName = setTur.szTrueName;
                    szOwnerNameH.Value = szOwnerName;
                    szOwneTel = setTur.szHandPhone.ToString() + ";" + setTur.szEmail.ToString();
                }
                if ((((uint)vtRes[0].dwMemberKind) & ((uint)UNIRESERVE.DWMEMBERKIND.MEMBERKIND_GROUP)) > 0)
                {
                    szGroupStudent = GetGroupMemberName((uint)vtRes[0].dwMemberID);
                }
                szResvTime = Get1970Date((uint)vtRes[0].dwBeginTime, "yyyy-MM-dd HH:mm") + "至" + Get1970Date((uint)vtRes[0].dwEndTime, "yyyy-MM-dd HH:mm") + "；共" + GetTime((((uint)vtRes[0].dwEndTime - (uint)vtRes[0].dwBeginTime) / 60)) + "";
                lblszResvTime.InnerText = szResvTime;
                szDevName = vtRes[0].ResvDev[0].szDevName.ToString();
                if (vtRes[0].szApplicationURL != null && vtRes[0].szApplicationURL != "")
                {
                    szOpHref = "点击下载";
                    szURl = "../../../../../ClientWeb/upload/UpLoadFile/" + vtRes[0].szApplicationURL;
                }
            }
        }

    }     
    protected void btnCheckTempOK_Click(object sender, EventArgs e)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        ADMINCHECK setCheck = new ADMINCHECK();
        setCheck.dwApplicantID = Parse(szOwnerID.Value);
        setCheck.dwCheckStat = (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK;
        setCheck.dwSubjectType = (uint)ADMINCHECK.DWSUBJECTTYPE.CHECK_RESV;
        setCheck.dwSubjectID = Parse(szidh.Value); 
        setCheck.szApplicantName = szOwnerNameH.Value;
        uResponse = m_Request.Admin.AdminCheck(setCheck);     
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {           
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
            {               
                MessageBox("审核通过", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
            }
            else
            {
                MessageBox("审核失败:" + m_Request.szErrMessage.ToString(), "提示", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
        }
    }   
    private string GetTime(uint Value)
    {
        string szValue = "";
        uint dwValue = Value;
        if (dwValue >= 1440)
        {
            szValue = (dwValue / 1440) + "天";
            dwValue = dwValue % 1440;

        }
        if (dwValue >= 60)
        {
            szValue += (dwValue / 60) + "小时";
            dwValue = dwValue % 60;
        }
        if (dwValue < 60 && dwValue > 0)
        {
            szValue += dwValue + "分钟";
        }
        return szValue;
    }
}
