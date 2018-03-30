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
    protected string szPrint = "false";
    protected string szResvGroupID = "";
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
        for (int i = 0; i < vtKind.Length; i++)
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
            YARDRESVCHECKINFOREQ vrPar = new YARDRESVCHECKINFOREQ();
            string szCheckIDs = Request["ID"];
            PutMemberValue("ID", szCheckIDs);
            string[] szCheckIDList = szCheckIDs.Split(',');

            vrPar.dwCheckID = Parse(szCheckIDList[0]);
            vrPar.dwNeedYardResv = 1;
            //vrPar.dwStatus = (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_NONE;
            //vrPar.dwAuthType = (uint)SYSFUNCRULE.DWAUTHTYPE.AUTHBY_REARCHTEST;
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
            else if (vtRes.Length>0)
            {

                YARDRESV yardResv = vtRes[0].YardResv;
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
                PutHTTPObj(yardResv);
                if (vtRes.Length == 0)
                {
                    MessageBox("获取失败", "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                }
                else if(vtRes.Length>0)
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
                                string szDept="";
                                szPreCheckDetail += "<tr>";
                                if (GetDeptByID(vtResCheck[m].dwCheckDeptID.ToString(), out dept))
                                {
                                    szDept = dept.szName.ToString();
                                }
                                if ((vtResCheck[m].szCheckName.IndexOf("物管审核")>-1)&& (((uint)vtResCheck[m].dwCheckStat) & (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK) > 0)
                                {
                                    szPrint="true";
                                }
                                szPreCheckDetail += "<td>" + szDept + "</td>";
                                szPreCheckDetail += "<td>" +Get1970Date(vtResCheck[m].dwCheckTime) + "</td>";
                                szPreCheckDetail += "<td>"+vtResCheck[m].szCheckDetail + "</td>";
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
                        string[] szDevIDList=(vtRes[0].YardResv.dwDevID.ToString()+","+ vtRes[0].YardResv.szSpareDevIDs).Split(',');
                        for(int k=0;k<szDevIDList.Length&&szDevIDList[k]!="";k++)
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
                           if(listDev.Count>0)
                           {
                                for (int i = 0; i < listDev.Count; i++)
                                {
                                    UNIDEVICE objDev=(UNIDEVICE)listDev[i];
                                    szDevList += GetInputItemHtml(CONSTHTML.option, "",objDev.szDevName.ToString(),objDev.dwDevID.ToString());
                                }
                                PutMemberValue("devID", vtRes[0].YardResv.dwDevID.ToString());
                            }
                        }
                        }

                    }
                    /*
                    ADMINLOGINRES loginRes = (ADMINLOGINRES)Session["LoginResult"];
                    if ((loginRes.AdminInfo.dwProperty & (uint)CHECKTYPE.DWMAINKIND.ADMINCHECK_DEVMAN) > 0)
                    {
                        UNIDEVICE[] vtDev = GetDevByKind(vtRes[0].YardResv.dwDevKind);
                        for (int i = 0; i < vtDev.Length; i++)
                        {
                            szDevList += GetInputItemHtml(CONSTHTML.option, "", vtDev[i].szDevName.ToString(), vtDev[i].dwDevID.ToString());
                        }
                        PutMemberValue("devID", vtRes[0].YardResv.dwDevID.ToString());
                    }
                 */
                    szResvGroupID = vtRes[0].YardResv.dwResvGroupID.ToString();
                    szResvDevName = vtRes[0].YardResv.szDevName;
                    szResvDevDept = vtRes[0].YardResv.szDeptName;
                    szApplyName = vtRes[0].szApplicantName;
                    szActivity = vtRes[0].YardResv.szActivityName;
                    szPeople = vtRes[0].YardResv.dwMinAttendance.ToString() + "-" + vtRes[0].YardResv.dwMaxAttendance.ToString() + "人";
                    szOrganiger = vtRes[0].YardResv.szOrganiger;
                    szOrganization = vtRes[0].YardResv.szOrganization;
                    PutMemberValue("dwCheckID", vtRes[0].dwCheckID.ToString());
                    szPreDate = vtRes[0].YardResv.dwPreDate.ToString();
                    szResvBegin = Get1970Date(vtRes[0].YardResv.dwBeginTime, "HHmm");
                    szResvEnd = Get1970Date(vtRes[0].YardResv.dwEndTime, "HHmm");
                    /*
                    if ((loginRes.AdminInfo.dwProperty & (uint)CHECKTYPE.DWMAINKIND.ADMINCHECK_DIRECTOR) > 0)
                    {
                        PutMemberValue("dwDirectors", vtRes[0].YardResv.dwCheckKinds.ToString());
                        PutMemberValue2("dwSecurityLevel", vtRes[0].YardResv.dwSecurityLevel.ToString());
                        PutMemberValue2("dwActivityLevel", vtRes[0].YardResv.dwActivityLevel.ToString());

                    }
                    else
                     * */
                    {
                        szLeveal = GetJustNameEqual(vtRes[0].YardResv.dwActivityLevel, "Yard_ActivityLevel");
                        szSecurityLevel = GetJustNameEqual(vtRes[0].YardResv.dwSecurityLevel, "Yard_dwSecurityLevel");
                        szDirectors = GetJustNameEqual(vtRes[0].YardResv.dwCheckKinds, "Yard_dwDirectors");
                    }

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
                    UNIRESERVE[] vtResvGroup = GetResvByGroupID((uint)vtRes[0].YardResv.dwResvGroupID);
                    szResvTime = Get1970Date(vtRes[0].YardResv.dwBeginTime) + "至" + Get1970Date(vtRes[0].YardResv.dwEndTime);
                if (vtResvGroup != null && vtResvGroup.Length > 0)
                    {
                        szResvTime = "";
                        szResvTime += "【" + vtResvGroup.Length + "】条:" + "<br/>";
                        for (int m = 0; m < vtResvGroup.Length; m++)
                        {
                            if (m < 5)
                            {
                                if (((m + 1) % 2) == 0)
                                {
                                    szResvTime += Get1970Date(vtResvGroup[m].dwBeginTime) + "至" + Get1970Date(vtResvGroup[m].dwEndTime) + "<br/>";
                                }
                                else
                                {
                                    szResvTime += Get1970Date(vtResvGroup[m].dwBeginTime) + "至" + Get1970Date(vtResvGroup[m].dwEndTime) + "；";
                                }
                            }
                            else
                            {
                                szResvTimeAll += Get1970Date(vtResvGroup[m].dwBeginTime) + "至" + Get1970Date(vtResvGroup[m].dwEndTime) + "；";
                            }
                        }
                    }
                
                    if (vtRes[0].YardResv.szApplicationURL != null && vtRes[0].YardResv.szApplicationURL != "")
                    {
                        PutMemberValue("szApplicationURL", vtRes[0].YardResv.szApplicationURL);
                        //szFileName = "../../../ClientWeb/upload/UpLoadFile/" + vtRes[0].YardResv.szApplicationURL;
                    }
                    m_Title = "查看信息";
                }
        
        }
        else
        {
            m_Title = "查看信息";

        }
    
    }
    private UNIRESERVE[] GetResvByGroupID(uint uGroupID)
    {
        RESVREQ vrGet = new RESVREQ();
        vrGet.dwStatFlag = (uint)RESVREQ.DWSTATFLAG.STATFLAG_INUSE + (uint)RESVREQ.DWSTATFLAG.STATFLAG_OVER + (uint)RESVREQ.DWSTATFLAG.STATFLAG_CHECKFAIL;
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
        vrGet.dwStatFlag = (uint)RESVREQ.DWSTATFLAG.STATFLAG_INUSE + (uint)RESVREQ.DWSTATFLAG.STATFLAG_OVER + (uint)RESVREQ.DWSTATFLAG.STATFLAG_CHECKFAIL;
  
        vrGet.dwResvGroupID = uGroupID;
        YARDRESV[] vtRes;
        if (m_Request.Reserve.GetYardResv(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            return vtRes;
        }
        return null;

    }
}
