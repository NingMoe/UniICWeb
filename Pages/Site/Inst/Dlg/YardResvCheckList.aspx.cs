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
    protected string szFileName = "";
    protected string sz = "";
    protected string szPeople = "";
    protected string szApplyName = "";
    protected string szResvDevName = "";
    protected string szResvTime = "";
    protected string szResvTimeAll = "";
    protected string szResvDevDept = "";
    protected string szNeedCameor= "不需要";
    protected string szLeveal = "";
    protected string szSecurityLevel = "";
    protected string szDirectors = "";
    protected string szDevList = "";
    protected string szMemo = "";
    protected string szActivity = "";
    protected string szOrganiger = "";
    protected string szOrganization = "";
    protected string szResvTimeInfo = "";
    protected string szServiceType = "";
    protected string szMemoExt = "";
    protected string szPreCheckDetail = "";
    protected string szPreDate = "";
    protected string szResvBegin= "";
    protected string szResvEnd = "";
    protected string szYardKind = "";
    protected string szCamp = "";
    protected string szBuilding = "";
    protected string szKinds = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string szCheckIDs = Request["ID"];
        PutMemberValue("ID", szCheckIDs);
        if (IsPostBack)
        {
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            
        
            string[] szCheckIDList = szCheckIDs.Split(',');
            for (int i = 0; i < szCheckIDList.Length; i++)
            {
                uint uTempID = Parse(szCheckIDList[i]);
                if (uTempID == 0)
                {
                    continue; ;
                }
                YARDRESVCHECKINFOREQ vrPar = new YARDRESVCHECKINFOREQ();
                vrPar.dwCheckID = uTempID;
                vrPar.dwNeedYardResv = 1;
                YARDRESVCHECKINFO[] vtRes;
                uResponse = m_Request.Reserve.GetYardResvCheckInfo(vrPar, out vtRes);
                if (uResponse == REQUESTCODE.EXECUTE_SUCCESS == vtRes.Length > 0)
                {
                    uResponse = CheckAll(vtRes);
                }
            }
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox("审核完成", "", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
            }
            else
            {
                MessageBox(m_Request.szErrMessage, "审核失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
        }
        m_Title = "批量审核";
    }
    private REQUESTCODE CheckAll(YARDRESVCHECKINFO[] vtRes)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        YARDRESVCHECKINFOREQ vrPar = new YARDRESVCHECKINFOREQ();
        string szCheckIDs = Request["ID"];
        vrPar.dwCheckID = (vtRes[0].dwCheckID);
        vrPar.dwCheckStat=(uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_CANDO;
        vrPar.dwNeedYardResv = 1;
        YARDRESVCHECKINFO[] vtRes1;
        uResponse = m_Request.Reserve.GetYardResvCheckInfo(vrPar, out vtRes1);
        if (vtRes1 != null && vtRes1.Length == 0)
        {
            return REQUESTCODE.EXECUTE_SUCCESS;
        }
        YARDRESVCHECK setValue = new YARDRESVCHECK();
        setValue.dwCheckID = vtRes[0].dwCheckID;
        setValue.dwCheckKind = vtRes[0].dwCheckKind;
        setValue.dwResvID = vtRes[0].dwResvID;
        setValue.YardResv = vtRes[0].YardResv;
        //setValue.YardResv.dwActivityLevel =vtRes[0].YardResv.dw Parse(Request["dwActivityLevel"]);
        //setValue.YardResv.dwSecurityLevel = Parse(Request["dwSecurityLevel"]);
        setValue.YardResv.dwCheckKinds = Parse(Request["dwDirectors"]);
        setValue.szCheckDetail = Request["szCheckInfo"];
        string szDevID = Request["devID"];
        if (szDevID != null && szDevID != "")
        {
            UNIDEVICE dev;
            if (getDevByID(szDevID, out dev))
            {
                setValue.YardResv.dwDevID = dev.dwDevID;
                setValue.YardResv.dwDevKind = dev.dwKindID;
                setValue.YardResv.dwDevSN = dev.dwDevSN;
                setValue.YardResv.szDevName = dev.szDevName;
                setValue.YardResv.dwLabID = dev.dwLabID;
                setValue.YardResv.dwRoomID = dev.dwRoomID;
                setValue.YardResv.szRoomName = dev.szRoomName;
            }
            YARDRESVREQ vrYardGet = new YARDRESVREQ();
            vrYardGet.dwResvID = vtRes[0].dwResvID;
            YARDRESV[] vtYardResv;
            if (m_Request.Reserve.GetYardResv(vrYardGet, out vtYardResv) == REQUESTCODE.EXECUTE_SUCCESS && vtYardResv != null && vtYardResv.Length > 0)
            {
                YARDRESV setYardResv = new YARDRESV();
                setYardResv = vtYardResv[0];
                setYardResv.dwDevID = dev.dwDevID;
                m_Request.Reserve.SetYardResv(setYardResv, out setYardResv);
            }

        }
        string szCheckName = Request["checkstate"];
        if (szCheckName == "16")
        {
            setValue.dwCheckStat = (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_OK;

        }
        else
        {
            setValue.dwCheckStat = (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_OK + (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_REDO;

        }
        setValue.szCheckDetail = Request["szCheckDetail"];
        uResponse = m_Request.Reserve.YardResvCheck(setValue);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            uResponse = CheckByResvID(setValue.dwResvID.ToString(), setValue.dwCheckStat, setValue.szCheckDetail);
            return REQUESTCODE.EXECUTE_SUCCESS;
        }
        else {
            return REQUESTCODE.EXECUTE_FAIL;
        }
        
    }
    private REQUESTCODE CheckByResvID(string szResvID, uint? uCheckState, string szMemo)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        YARDRESVCHECKINFOREQ vrPar = new YARDRESVCHECKINFOREQ();
        vrPar.dwResvID = Parse(szResvID);
        vrPar.dwCheckStat=(uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_CANDO;
        vrPar.dwNeedYardResv = 1;
        YARDRESVCHECKINFO[] vtRes;
        uResponse = m_Request.Reserve.GetYardResvCheckInfo(vrPar, out vtRes);
        REQUESTCODE bRes = REQUESTCODE.EXECUTE_FAIL;
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS == vtRes.Length > 0)
        {
            for (int i = 0; i < vtRes.Length; i++)
            {
                if (!((vtRes[i].dwCheckStat & (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_CANDO) > 0))
                {
                    continue;
                }
                ADMINLOGINRES accno = (ADMINLOGINRES)Session["LoginResult"];
                if (accno.AccInfo.dwAccNo != null && (accno.AccInfo.dwDeptID != vtRes[i].dwCheckDeptID))
                {
                    continue;
                }
                CHECKTYPE checktype = new CHECKTYPE();
                if (GetCheckType((uint)vtRes[i].dwCheckKind, out checktype))
                {
                    if ((checktype.dwMainKind & (uint)CHECKTYPE.DWMAINKIND.ADMINCHECK_SERVICE) > 0)
                    {
                        continue;
                    }
                }
                else
                {
                    continue;
                }

                YARDRESVCHECK setValue = new YARDRESVCHECK();
                setValue.dwCheckID = vtRes[i].dwCheckID;
                setValue.dwCheckKind = vtRes[i].dwCheckKind;
                setValue.dwResvID = vtRes[i].dwResvID;
                setValue.YardResv = vtRes[i].YardResv;
                setValue.YardResv.dwActivityLevel = Parse(Request["dwActivityLevel"]);
                setValue.YardResv.dwSecurityLevel = Parse(Request["dwSecurityLevel"]);
                setValue.YardResv.dwCheckKinds = Parse(Request["dwDirectors"]);
                setValue.szCheckDetail = Request["szCheckInfo"];
                string szDevID = Request["devID"];
                if (szDevID != null && szDevID != "")
                {
                    UNIDEVICE dev;
                    if (getDevByID(szDevID, out dev))
                    {
                        setValue.YardResv.dwDevID = dev.dwDevID;
                        setValue.YardResv.dwDevKind = dev.dwKindID;
                        setValue.YardResv.dwDevSN = dev.dwDevSN;
                        setValue.YardResv.szDevName = dev.szDevName;
                        setValue.YardResv.dwLabID = dev.dwLabID;
                        setValue.YardResv.dwRoomID = dev.dwRoomID;
                        setValue.YardResv.szRoomName = dev.szRoomName;

                    }
                    YARDRESVREQ vrYardGet = new YARDRESVREQ();
                    vrYardGet.dwResvID = vtRes[i].dwResvID;
                    YARDRESV[] vtYardResv;
                    if (m_Request.Reserve.GetYardResv(vrYardGet, out vtYardResv) == REQUESTCODE.EXECUTE_SUCCESS && vtYardResv != null && vtYardResv.Length > 0)
                    {
                        YARDRESV setYardResv = new YARDRESV();
                        setYardResv = vtYardResv[i];
                        setYardResv.dwDevID = dev.dwDevID;
                        m_Request.Reserve.SetYardResv(setYardResv, out setYardResv);
                    }

                }
                setValue.dwCheckStat = uCheckState;
                setValue.szCheckDetail = szMemo;
                uResponse = m_Request.Reserve.YardResvCheck(setValue);
                if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    bRes = REQUESTCODE.EXECUTE_SUCCESS;
                }

            }
            return bRes;
        }
        return REQUESTCODE.EXECUTE_FAIL;
    }
    private UNIRESERVE[] GetResvByGroupID(uint uGroupID)
    {
        RESVREQ vrGet = new RESVREQ();
        vrGet.dwResvGroupID = uGroupID;
        UNIRESERVE[] vtRes;
        if (m_Request.Reserve.Get(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            return vtRes;
        }
        return null;
        
    }
    private YARDRESV[] GetYardResvByGroupID(uint uGroupID)
    {
        YARDRESVREQ vrGet = new YARDRESVREQ();
        vrGet.dwResvGroupID = uGroupID;
        YARDRESV[] vtRes;
        if (m_Request.Reserve.GetYardResv(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            return vtRes;
        }
        return null;

    }
    private bool GetCheckType(uint uID, out CHECKTYPE setValue)
    {
        setValue = new CHECKTYPE();
        CHECKTYPEREQ vrGet = new CHECKTYPEREQ();
        vrGet.dwCheckKind = uID;
        CHECKTYPE[] vtCheck;
        if (m_Request.Admin.CheckTypeGet(vrGet, out vtCheck) == REQUESTCODE.EXECUTE_SUCCESS && vtCheck != null && vtCheck.Length > 0)
        {
            setValue = vtCheck[0];
            return true;
        }
        return false;
    }
}
