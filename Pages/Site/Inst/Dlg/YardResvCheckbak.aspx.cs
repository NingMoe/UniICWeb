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
	protected void Page_Load(object sender, EventArgs e)
    {
        UNILAB newLab;
    
        if (IsPostBack)
        {
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            YARDRESVCHECKINFOREQ vrPar = new YARDRESVCHECKINFOREQ();
            vrPar.dwCheckID = Parse(Request["ID"]);
            vrPar.dwNeedYardResv = 1;

            YARDRESVCHECKINFO[] vtRes;
            uResponse = m_Request.Reserve.GetYardResvCheckInfo(vrPar, out vtRes);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS==vtRes.Length>0)
            {
                YARDRESVCHECK setValue = new YARDRESVCHECK();
                setValue.dwCheckID = vtRes[0].dwCheckID;
                setValue.dwCheckKind = vtRes[0].dwCheckKind;
                setValue.dwResvID = vtRes[0].dwResvID;
                setValue.YardResv = vtRes[0].YardResv;
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
                }
                
                setValue.dwCheckStat = (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_OK;
                uResponse = m_Request.Reserve.YardResvCheck(setValue);
                if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    MessageBox("审核通过", "", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                }
                else {
                    MessageBox(m_Request.szErrMessage, "审核失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                }
            }
        }
       
        if (Request["op"] == "set")
        {
            bSet = true;
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            YARDRESVCHECKINFOREQ vrPar = new YARDRESVCHECKINFOREQ();
            vrPar.dwCheckID = Parse(Request["ID"]);
            vrPar.dwNeedYardResv = 1;
           // vrPar.dwStatus = (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_NONE;
            //vrPar.dwAuthType = (uint)SYSFUNCRULE.DWAUTHTYPE.AUTHBY_REARCHTEST;

            YARDRESVCHECKINFO[] vtRes;
            uResponse = m_Request.Reserve.GetYardResvCheckInfo(vrPar, out vtRes);
            if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
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
                    szResvTime = Get1970Date(vtRes[0].YardResv.dwBeginTime) + "至" + Get1970Date(vtRes[0].YardResv.dwEndTime);
                    szResvDevName=vtRes[0].YardResv.szDevName;
                    szResvDevDept = vtRes[0].YardResv.szDeptName;
                    szApplyName = vtRes[0].szApplicantName;
                    szActivity = vtRes[0].YardResv.szActivityName;
                    szPeople = vtRes[0].YardResv.dwMinAttendance.ToString() + "-" + vtRes[0].YardResv.dwMaxAttendance.ToString()+"人";
                    szOrganiger = vtRes[0].YardResv.szOrganiger;
                    szOrganization = vtRes[0].YardResv.szOrganization;
                    PutMemberValue("dwCheckID", vtRes[0].dwCheckID.ToString());
                  
                    if ((loginRes.AdminInfo.dwProperty & (uint)CHECKTYPE.DWMAINKIND.ADMINCHECK_DIRECTOR) > 0)
                    {
                        PutMemberValue("dwDirectors", vtRes[0].YardResv.dwCheckKinds.ToString());
                        PutMemberValue2("dwSecurityLevel", vtRes[0].YardResv.dwSecurityLevel.ToString());
                        PutMemberValue2("dwActivityLevel", vtRes[0].YardResv.dwActivityLevel.ToString());
                    
                    }
                    else
                    {
                        szLeveal = GetJustNameEqual(vtRes[0].YardResv.dwActivityLevel, "Yard_ActivityLevel");
                        szSecurityLevel = GetJustNameEqual(vtRes[0].YardResv.dwSecurityLevel, "Yard_dwSecurityLevel");
                        szDirectors = GetJustNameEqual(vtRes[0].YardResv.dwCheckKinds, "Yard_dwDirectors");
                    }
                     
                  
                   
                    if ((vtRes[0].YardResv.dwProperty & ((uint)UNIRESERVE.DWPROPERTY.RESVPROP_NEEDVIDEO)) > 0)
                    {
                        szNeedCameor = "需要";
                    }
                    UNIACCOUNT accno;
                    if (GetAccByAccno(vtRes[0].dwApplicantID.ToString(), out accno))
                    {
                        accno.szTrueName = accno.szTrueName + "(" + accno.szLogonName+ ")";
                        PutJSObj(accno);
                        szMemo = vtRes[0].YardResv.szMemo;
                    }
                    if (vtRes[0].YardResv.szApplicationURL != null && vtRes[0].YardResv.szApplicationURL != "")
                    {
                        szFileName = "../../../ClientWeb/upload/UpLoadFile/" + vtRes[0].YardResv.szApplicationURL;
                    }
                    m_Title = "审核（会议室借用）";
                }
            }
        }
        else
        {
            m_Title = "审核（会议室借用）";

        }
    }
}
