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
using System.Text;
using System.Reflection;
using System.IO;
using LumenWorks.Framework.IO.Csv;

public partial class _Default : UniPage
{
    protected bool bSet = false;
    protected string m_Title = "";
    protected string m_szOut = "";
    protected string szOut = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        nDefaultNeedLine = 20;
        isImport.Value = "0";
        m_Title = "设置成员";

        uint uActivityID = 0;
        uint uGroupID = 0;
        if (Request["id"] != null)
        {
            uGroupID = Parse(Request["dwID"]);
        }
        if (Request["activityid"] != null)
        {
            uActivityID = Parse(Request["activityid"]);
        }

        GROUPMEMDETAILREQ vrGet = new GROUPMEMDETAILREQ();
        string logonname = Request["logonname"];
        UNIACCOUNT accInfo;
        if (logonname != null && logonname != "" && GetAccByLogonName(logonname, out accInfo))
        {
            vrGet.dwAccNo = accInfo.dwAccNo;
        }
        GetPageCtrlValue(out vrGet.szReqExtInfo);
        vrGet.szReqExtInfo.dwNeedLines = 10000;
        string szOrderKey = vrGet.szReqExtInfo.szOrderKey;
        string szOrderMode = vrGet.szReqExtInfo.szOrderMode;
        if (szOrderKey != null && szOrderKey != "" && szOrderKey != "," && szOrderMode != null && szOrderMode != "" && szOrderMode != ",")
        {
            vrGet.szReqExtInfo.szOrderKey = szOrderKey.Split(',')[0];
            vrGet.szReqExtInfo.szOrderMode = szOrderMode.Split(',')[0];
        }
        if (szOrderKey == "," || szOrderMode == ",")
        {
            vrGet.szReqExtInfo.szOrderKey = null;
            vrGet.szReqExtInfo.szOrderMode = null;
        }
        vrGet.dwGroupID = Parse(Request["dwID"]);
        vrGet.dwReqProp = (uint)GROUPMEMDETAILREQ.DWREQPROP.GROUPMEMDETAILREQ_NEEDDEL;
        GROUPMEMDETAIL[] vtRes;
        PutMemberValue("id", vrGet.dwGroupID.ToString());

        UNIRESVREC[] vtResvRes;

        ACTIVITYPLANREQ planyReq = new ACTIVITYPLANREQ();
        planyReq.dwGetType = (uint)ACTIVITYPLANREQ.DWGETTYPE.ACTIVITYPLANGET_BYID;
        planyReq.szGetKey = uActivityID.ToString();
        UNIACTIVITYPLAN[] planRes;
        uint? uplanDate = 0;
        uint? uResvID = 0;
        if (m_Request.Reserve.GetActivityPlan(planyReq, out planRes) == REQUESTCODE.EXECUTE_SUCCESS && planRes != null && planRes.Length > 0)
        {
            uResvID = planRes[0].dwResvID;
            uplanDate = planRes[0].dwActivityDate;
        }


        RESVRECREQ vrResvGet = new RESVRECREQ();
        vrResvGet.dwGetType = (uint)RESVRECREQ.DWGETTYPE.RESVRECGET_BYID;
        vrResvGet.szGetKey = uResvID.ToString();
        vrResvGet.dwStartDate = uplanDate;
        vrResvGet.dwEndDate = uplanDate;

        REQUESTCODE ucode = m_Request.Report.ResvRecGet(vrResvGet, out vtResvRes);

        REQUESTCODE uResponse = m_Request.Group.GetGroupMemDetail(vrGet, out vtRes);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {

            for (int i = 0; i < vtRes.Length; i++)
            {
                string szTurLogonName = "";
                string szTtrueName = "";
                UNIACCOUNT accTurtor = new UNIACCOUNT();
                if (GetAccByAccno(vtRes[i].dwTutorID.ToString(), out accTurtor))
                {
                    szTurLogonName = accTurtor.szLogonName;
                    szTtrueName = accTurtor.szTrueName;
                }

                m_szOut += "<tr>";
                m_szOut += "<td  data-szTtrueName=\"" + (szTtrueName) + "\" data-sLogonName=\"" + (vtRes[i].szPID) + "\" data-truename=\"" + (vtRes[i].szTrueName) + "\" data-end=\"" + GetDateStr((uint)vtRes[i].dwEndDate) + "\" data-begin=\"" + GetDateStr((uint)vtRes[i].dwBeginDate) + "\" data-tLogonName=\"" + szTurLogonName.ToString() + "\"  data-accno=\"" + vtRes[i].dwAccNo.ToString() + "\" data-handphone=\"" + vtRes[i].szHandPhone.ToString() + "\" data-email=\"" + vtRes[i].szEmail.ToString() + "\">" + "<input type='checkbox' name='checkAccno' value=" + vtRes[i].dwAccNo + " />" + "</td>";
                m_szOut += "<td  data-szTtrueName=\"" + (szTtrueName) + "\" data-sLogonName=\"" + (vtRes[i].szPID) + "\" data-truename=\"" + (vtRes[i].szTrueName) + "\" data-end=\"" + GetDateStr((uint)vtRes[i].dwEndDate) + "\" data-begin=\"" + GetDateStr((uint)vtRes[i].dwBeginDate) + "\" data-tLogonName=\"" + szTurLogonName.ToString() + "\"  data-accno=\"" + vtRes[i].dwAccNo.ToString() + "\" data-handphone=\"" + vtRes[i].szHandPhone.ToString() + "\" data-email=\"" + vtRes[i].szEmail.ToString() + "\">" + vtRes[i].szTrueName + "</td>";
                m_szOut += "<td>" + vtRes[i].szPID + "</td>";
                m_szOut += "<td>" + vtRes[i].szClassName + "</td>";
                m_szOut += "<td>" + vtRes[i].szDeptName + "</td>";
                m_szOut += "<td>" + vtRes[i].szHandPhone + "</td>";
                m_szOut += "<td>" + vtRes[i].szEmail + "</td>";
                string szStaus = "未出席";
                if (vtResvRes != null)
                {
                    for (int m = 0; m < vtResvRes.Length; m++)
                    {
                        if (vtResvRes[m].dwAccNo == vtRes[i].dwAccNo && (vtResvRes[m].dwStatus & (uint)UNIRESVREC.DWSTATUS.RESVRECSTAT_ATTEND) > 0)
                        {
                            szStaus = "出席";
                            break;
                        }
                    }
                }
                
                m_szOut += "<td>" + szStaus + "</td>";
                //m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
        }
    }
}

