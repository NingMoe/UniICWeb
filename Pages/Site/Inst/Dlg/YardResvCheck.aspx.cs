﻿using System;
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
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            string szCheckIDs = Request["ID"];
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
                    if (i > 0)
                    {
                        CHECKTYPE checktype = new CHECKTYPE();
                        if (GetCheckType((uint)vtRes[0].dwCheckKind, out checktype))
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
                    }
                    uResponse = CheckAll(vtRes);
                }
            }
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
            {
                Session["checkid"] = szCheckIDs;
                MessageBox("审核通过", "", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
            }
            else
            {
                
                MessageBox(m_Request.szErrMessage, "审核失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
        }

        if (Request["op"] == "set")
        {
            bSet = true;
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            YARDRESVCHECKINFOREQ vrPar = new YARDRESVCHECKINFOREQ();
            string szCheckIDs = Request["ID"];
            PutMemberValue("ID", szCheckIDs);
            string[] szCheckIDList = szCheckIDs.Split(',');

            vrPar.dwCheckID = Parse(szCheckIDList[0]);

            vrPar.dwNeedYardResv = 1;
            string szCheckURl = Request["checkstate"];
            if (szCheckURl != null && szCheckURl != "")
            {
                PutMemberValue("szCheckURl", szCheckURl);
            }
            YARDRESVCHECKINFO[] vtRes;
            uResponse = m_Request.Reserve.GetYardResvCheckInfo(vrPar, out vtRes);
            if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else if (vtRes.Length > 0 && vtRes[0].YardResv.dwActivitySN != null)
            {

                YARDRESV yardResv = vtRes[0].YardResv;
                szMemoYardResv = vtRes[0].YardResv.szMemo;
                uint uResvPro = (uint)yardResv.dwProperty;
                if ((uResvPro & (uint)UNIRESERVE.DWPROPERTY.RESVPROP_PROFIT) > 0)
                {
                    PutMemberValue("resvPro1", "1");

                }
                else
                {
                    PutMemberValue("resvPro1", "2");
                }
                if ((uResvPro & (uint)UNIRESERVE.DWPROPERTY.RESVPROP_UNOPEN) > 0)
                {
                    PutMemberValue("resvPro2", "1");
                }
                else
                {
                    PutMemberValue("resvPro2", "2");
                }
                PutMemberValue("YardActivitySN", yardResv.dwActivitySN.ToString());
                PutMemberValue("dwKind", yardResv.dwKind.ToString());
                if (vtRes[0].YardResv.szCycRule == "")
                {
                    yardResv.szCycRule = Get1970Date(yardResv.dwBeginTime) + "至" + Get1970Date(yardResv.dwEndTime) + "；";
                }
                PutHTTPObj(yardResv);
                if (vtRes.Length == 0)
                {
                    MessageBox("获取失败", "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                }
                else if (vtRes.Length > 0)
                {
                    YARDRESVCHECKINFOREQ vrParCheck = new YARDRESVCHECKINFOREQ();
                    vrParCheck.dwResvID = vtRes[0].dwResvID;
                    vrPar.dwNeedYardResv = 1;
                    YARDRESVCHECKINFO[] vtResCheck;
                    uResponse = m_Request.Reserve.GetYardResvCheckInfo(vrParCheck, out vtResCheck);
                    if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtResCheck.Length > 0)
                    {
                        szPreCheckDetail += "<table>";
                        for (int m = 0; m < vtResCheck.Length; m++)
                        {

                            if ((vtResCheck[m].dwCheckStat & ((uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_OK)) > 0)
                            {
                                UNIDEPT dept;
                                string szDept = "";
                                szPreCheckDetail += "<tr>";
                                if (GetDeptByID(vtResCheck[m].dwCheckDeptID.ToString(), out dept))
                                {
                                    szDept = dept.szName.ToString();
                                }
                                szPreCheckDetail += "<td>" + szDept + "</td>";
                                szPreCheckDetail += "<td>" + vtResCheck[m].szCheckName + "</td>";
                                szPreCheckDetail += "<td>" + Get1970Date(vtResCheck[m].dwCheckTime) + "</td>";
                                szPreCheckDetail += "<td>" + vtResCheck[m].szCheckDetail + "</td>";
                                szPreCheckDetail += "<td>" + vtResCheck[m].szAdminName + "</td>";
                                szPreCheckDetail += "</tr>";
                            }
                        }
                        szPreCheckDetail += "</table>";
                    }
                    CHECKTYPEREQ vrGetcheckTypeTemp = new CHECKTYPEREQ();
                    vrGetcheckTypeTemp.dwCheckKind = vtRes[0].dwCheckKind;
                    CHECKTYPE[] vtCheckTypeTemp;
                    if (m_Request.Admin.CheckTypeGet(vrGetcheckTypeTemp, out vtCheckTypeTemp) == REQUESTCODE.EXECUTE_SUCCESS && vtCheckTypeTemp != null && vtCheckTypeTemp.Length > 0)
                    {
                        ArrayList listDev = new ArrayList();
                        string[] szDevIDList = (vtRes[0].YardResv.dwDevID.ToString() + "," + vtRes[0].YardResv.szSpareDevIDs).Split(',');
                        for (int k = 0; k < szDevIDList.Length && szDevIDList[k] != ""; k++)
                        {
                            DEVREQ vrGetDev = new DEVREQ();
                            vrGetDev.dwDevID = Parse(szDevIDList[k]);
                            UNIDEVICE[] vtDev;
                            uResponse = m_Request.Device.Get(vrGetDev, out vtDev);
                            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtDev != null && vtDev.Length > 0)
                            {
                                listDev.Add(vtDev[0]);
                            }
                        }
                        if ((((uint)vtCheckTypeTemp[0].dwMainKind) & ((uint)CHECKTYPE.DWMAINKIND.ADMINCHECK_DEVMAN)) > 0)
                        {
                            if (listDev.Count > 0)
                            {
                                for (int i = 0; i < listDev.Count; i++)
                                {
                                    UNIDEVICE objDev = (UNIDEVICE)listDev[i];
                                    szDevList += GetInputItemHtml(CONSTHTML.option, "", objDev.szDevName.ToString(), objDev.dwDevID.ToString());
                                }
                                szIsCheck = "true";
                                PutMemberValue("devID", vtRes[0].YardResv.dwDevID.ToString());
                            }
                        }
                    }

                }
                szResvDevName = vtRes[0].YardResv.szDevName;
                szResvDevDept = vtRes[0].YardResv.szDeptName;
                szApplyName = vtRes[0].szApplicantName;
                szActivity = vtRes[0].YardResv.szActivityName;
                szPeople = vtRes[0].YardResv.dwMinAttendance.ToString() + "-" + vtRes[0].YardResv.dwMaxAttendance.ToString() + "人";
                szOrganiger = vtRes[0].YardResv.szOrganization;
                szOrganization = vtRes[0].YardResv.szOrganization;
                PutMemberValue("dwCheckID", vtRes[0].dwCheckID.ToString());
                szPreDate = vtRes[0].YardResv.dwPreDate.ToString();
                szResvBegin = Get1970Date(vtRes[0].YardResv.dwBeginTime, "HHmm");
                szResvEnd = Get1970Date(vtRes[0].YardResv.dwEndTime, "HHmm");

                PutMemberValue("dwSecurityLevel", vtRes[0].YardResv.dwSecurityLevel.ToString());
                szLeveal = GetJustNameEqual(vtRes[0].YardResv.dwActivityLevel, "Yard_ActivityLevel");
                szSecurityLevel = GetJustNameEqual(vtRes[0].YardResv.dwSecurityLevel, "Yard_dwSecurityLevel");
                szDirectors = GetJustNameEqual(vtRes[0].YardResv.dwCheckKinds, "Yard_dwDirectors");


                uint uCheckKinds = (uint)vtRes[0].YardResv.dwCheckKinds;
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
                if ((vtRes[0].YardResv.dwProperty & ((uint)UNIRESERVE.DWPROPERTY.RESVPROP_NEEDVIDEO)) > 0)
                {
                    szNeedCameor = "需要";
                }
                UNIACCOUNT accno;
                if (GetAccByAccno(vtRes[0].dwApplicantID.ToString(), out accno))
                {
                    accno.szTrueName = accno.szTrueName + "(" + accno.szLogonName + ")";
                    PutMemberValue("szTrueName", accno.szTrueName);
                    szMemo = vtRes[0].YardResv.szMemo;
                    
                    string[] szMemoList = szMemo.Split('$');
                    if (szMemoList != null && szMemoList.Length > 0)
                    {
                        szMemo = szMemoList[0];
                        szMemoExt = szMemoList[0].Replace("&", ",");
                    }
                }
                szResvTime = vtRes[0].YardResv.szCycRule;
                if (szResvTime == "")
                {
                    szResvTime = Get1970Date(vtRes[0].YardResv.dwBeginTime) + "至" + Get1970Date(vtRes[0].YardResv.dwEndTime) + "；";
                }
                if (vtRes[0].YardResv.szApplicationURL != null && vtRes[0].YardResv.szApplicationURL != "")
                {
                    PutMemberValue("szApplicationURL", vtRes[0].YardResv.szApplicationURL);
                }
                m_Title = "审核";
            }

        }
        else
        {
            m_Title = "审核";

        }
    
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
        uint openProp =Parse(Request["resvPro2"]);//是否开放
        uint kindPro = Parse(Request["dwKind"]);//活动类型
        uint openYL = Parse(Request["resvPro1"]);//盈利
        uint openAttendUser = Parse(Request["dwMaxAttendance"]);//参与人数
        YARDRESVCHECK setValue = new YARDRESVCHECK();
        setValue.dwCheckID = vtRes[0].dwCheckID;
        setValue.dwCheckKind = vtRes[0].dwCheckKind;
        setValue.dwResvID = vtRes[0].dwResvID;
        setValue.YardResv = vtRes[0].YardResv;
        setValue.YardResv.dwActivityLevel = Parse(Request["dwActivityLevel"]);
        setValue.YardResv.dwSecurityLevel = Parse(Request["dwSecurityLevel"]);
        setValue.YardResv.dwCheckKinds = Parse(Request["dwDirectors"]);
        setValue.szCheckDetail = Request["szCheckInfo"];
        uint uResvProp = (uint)setValue.YardResv.dwProperty;

        //开放
        if (openProp == 1)
        {
            uResvProp = uResvProp | (uint)UNIRESERVE.DWPROPERTY.RESVPROP_UNOPEN;
        }
        else
        {
            if ((uResvProp & (uint)UNIRESERVE.DWPROPERTY.RESVPROP_UNOPEN) > 0)
            {
                uResvProp = uResvProp - (uint)UNIRESERVE.DWPROPERTY.RESVPROP_UNOPEN;
            }
        }

        //盈利
        if (openYL == 1)
        {
            uResvProp = uResvProp | (uint)UNIRESERVE.DWPROPERTY.RESVPROP_PROFIT;
        }
        else
        {
            if ((uResvProp & (uint)UNIRESERVE.DWPROPERTY.RESVPROP_PROFIT) > 0)
            {
                uResvProp = uResvProp - (uint)UNIRESERVE.DWPROPERTY.RESVPROP_PROFIT;
            }
        }
        setValue.YardResv.dwProperty = uResvProp;
        if (openAttendUser != 0)
        { 
            setValue.YardResv.dwMaxAttendance = openAttendUser;
        }
        if (kindPro != 0)
        {
            setValue.YardResv.dwKind = kindPro;
        }
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
              //  m_Request.Reserve.SetYardResv(setYardResv, out setYardResv);
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
                        //setYardResv = vtYardResv[i];
                        setYardResv = vtYardResv[0];
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
