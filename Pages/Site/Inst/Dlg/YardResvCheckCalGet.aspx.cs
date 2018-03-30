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
    protected string szMemoYardResv = "";
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
    protected string szIsCheck = "false";
	protected void Page_Load(object sender, EventArgs e)
    {
        UNICAMPUS[] vtCamp = GetAllCampus();
        szCamp += GetInputItemHtml(CONSTHTML.option, "", "全部", "0");
        if (vtCamp != null && vtCamp.Length > 0)
        {
            for (int i = 0; i < vtCamp.Length; i++)
            {
                szCamp += GetInputItemHtml(CONSTHTML.option, "", vtCamp[i].szCampusName, vtCamp[i].dwCampusID.ToString());
            }
        }
        UNIDEVCLS[] vtKind = GetAllDevCls();
        szKinds += GetInputItemHtml(CONSTHTML.option, "", "全部", "0");
        for (int i = 0; vtKind!=null&& i < vtKind.Length; i++)
        {
            szKinds += GetInputItemHtml(CONSTHTML.option, "", vtKind[i].szClassName.ToString(), vtKind[i].dwClassID.ToString());
        }
        UNIBUILDING[] vtBuilding = getAllBuilding();
        szBuilding += GetInputItemHtml(CONSTHTML.option, "", "全部", "0");
        for (int i = 0; i < vtBuilding.Length; i++)
        {
            if (vtBuilding[i].dwCampusID.ToString() == vtCamp[0].dwCampusID.ToString())
            {
                szBuilding += GetInputItemHtml(CONSTHTML.option, "", vtBuilding[i].szBuildingName.ToString(), vtBuilding[i].dwBuildingID.ToString());
            }
        }
        CODINGTABLE[] vtCodeing = getCodeTableByType((uint)CODINGTABLE.DWCODETYPE.CODE_YARDRESVKIND);
        for (int i = 0; vtCodeing != null && i < vtCodeing.Length; i++)
        {
            szYardKind += GetInputItemHtml(CONSTHTML.radioButton, "dwKind", vtCodeing[i].szCodeName, vtCodeing[i].szCodeSN);
        }
        UNILAB newLab;

        if (IsPostBack)
        {
            
        }

        if (Request["op"] == "set")
        {
            bSet = true;
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            YARDRESVREQ vrPar = new YARDRESVREQ();
            vrPar.dwStatFlag = (uint)RESVREQ.DWSTATFLAG.STATFLAG_INUSE + (uint)RESVREQ.DWSTATFLAG.STATFLAG_OVER;
            string szResvID = Request["ID"];
            vrPar.dwResvID = Parse(szResvID);

            YARDRESV[] vtRes;
            uResponse = m_Request.Reserve.GetYardResv(vrPar, out vtRes);
            if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else if (vtRes.Length > 0 && vtRes[0].dwActivitySN != null)
            {
                YARDRESV yardResv = vtRes[0];
                szMemoYardResv = vtRes[0].szMemo;
                uint uResvPro = (uint)yardResv.dwProperty;
                
                PutHTTPObj(yardResv);

                szResvDevName = vtRes[0].szDevName;
                szResvDevDept = vtRes[0].szDeptName;
                szApplyName = vtRes[0].szApplicantName;
                szActivity = vtRes[0].szActivityName;
                szPeople = vtRes[0].dwMinAttendance.ToString() + "-" + vtRes[0].dwMaxAttendance.ToString() + "人";
                szOrganiger = vtRes[0].szOrganization;
                szOrganization = vtRes[0].szOrganization;
              
                szPreDate = vtRes[0].dwPreDate.ToString();
                szResvBegin = Get1970Date(vtRes[0].dwBeginTime, "HHmm");
                szResvEnd = Get1970Date(vtRes[0].dwEndTime, "HHmm");


                szLeveal = GetJustNameEqual(vtRes[0].dwActivityLevel, "Yard_ActivityLevel");
                szSecurityLevel = GetJustNameEqual(vtRes[0].dwSecurityLevel, "Yard_dwSecurityLevel");
                szDirectors = GetJustNameEqual(vtRes[0].dwCheckKinds, "Yard_dwDirectors");


                uint uCheckKinds = (uint)vtRes[0].dwCheckKinds;
                CHECKTYPEREQ vrCheckTypeReq = new CHECKTYPEREQ();
                vrCheckTypeReq.dwMainKind = (uint)CHECKTYPE.DWMAINKIND.ADMINCHECK_SERVICE;
                CHECKTYPE[] vtCheckType;
                if (m_Request.Admin.CheckTypeGet(vrCheckTypeReq, out vtCheckType) == REQUESTCODE.EXECUTE_SUCCESS && vtCheckType != null && vtCheckType.Length > 0)
                {
                    for (int m = 0; m < vtCheckType.Length; m++)
                    {
                        if ((uCheckKinds & (uint)vtCheckType[m].dwCheckKind) > 0)
                        {
                            szServiceType += vtCheckType[m].szCheckName + ",";
                        }
                    }
                }
                if ((vtRes[0].dwProperty & ((uint)UNIRESERVE.DWPROPERTY.RESVPROP_NEEDVIDEO)) > 0)
                {
                    szNeedCameor = "需要";
                }
                UNIACCOUNT accno;
                if (GetAccByAccno(vtRes[0].dwApplicantID.ToString(), out accno))
                {
                    accno.szTrueName = accno.szTrueName + "(" + accno.szLogonName + ")";
                    PutMemberValue("szTrueName", accno.szTrueName);
                    szMemo = vtRes[0].szMemo;
                    
                    string[] szMemoList = szMemo.Split('$');
                    if (szMemoList != null && szMemoList.Length > 0)
                    {
                        szMemo = szMemoList[0];
                        szMemoExt = szMemoList[0].Replace("&", ",");
                    }
                }
                szResvTime = vtRes[0].szCycRule;
                if (szResvTime == "")
                {
                    szResvTime = Get1970Date(vtRes[0].dwBeginTime) + "至" + Get1970Date(vtRes[0].dwEndTime) + "；";
                }
                if (vtRes[0].szApplicationURL != null && vtRes[0].szApplicationURL != "")
                {
                    PutMemberValue("szApplicationURL", vtRes[0].szApplicationURL);
                }
                
            }

        }
        else
        {
           
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
                    //continue;
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
