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
    protected string m_szOut = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        nDefaultNeedLine = 20;
        m_Title = "设置使用人员";
        if (!IsPostBack)
        {
            dwStartDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            dwEndDate.Value = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd");
        }
        if (Request["dwID"] != null)
        { 
        }
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        GROUPMEMDETAILREQ vrGet=new GROUPMEMDETAILREQ();
       
        GetPageCtrlValue(out vrGet.szReqExtInfo);
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
        vrGet.dwGroupID = Parse(Request["id"]);
        GROUPMEMDETAIL[] vtRes;
        
        uResponse = m_Request.Group.GetGroupMemDetail(vrGet, out vtRes);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {

            for (int i = 0; i < vtRes.Length; i++)
            {
                string szTurLogonName = "";
                string szTtrueName = "";
                UNIACCOUNT accTurtor=new UNIACCOUNT();
                if(GetAccByAccno(vtRes[i].dwTutorID.ToString(),out accTurtor))
                {
                    szTurLogonName=accTurtor.szLogonName;
                    szTtrueName = accTurtor.szTrueName;
                }

                m_szOut += "<tr>";
                m_szOut += "<td  data-szTtrueName=\"" + (szTtrueName) + "\" data-sLogonName=\"" + (vtRes[i].szPID) + "\" data-truename=\"" + (vtRes[i].szTrueName) + "\" data-end=\"" + GetDateStr((uint)vtRes[i].dwEndDate) + "\" data-begin=\"" + GetDateStr((uint)vtRes[i].dwBeginDate) + "\" data-tLogonName=\"" + szTurLogonName.ToString() + "\"  data-accno=\"" + vtRes[i].dwAccNo.ToString() + "\" data-handphone=\"" + vtRes[i].szHandPhone.ToString() + "\" data-email=\"" + vtRes[i].szEmail.ToString() + "\">" + vtRes[i].szTrueName + "</td>";
                m_szOut += "<td>" + vtRes[i].szPID + "</td>";
                m_szOut += "<td>" + vtRes[i].szTutorName + "</td>";
                m_szOut += "<td>" +GetDateStr((uint)vtRes[i].dwBeginDate)+ "</td>";
                m_szOut += "<td>" + GetDateStr((uint)vtRes[i].dwEndDate) + "</td>";
                m_szOut += "<td>" + GetJustName((uint)vtRes[i].dwStatus, "GROUPMEMBER_Status") + "</td>";
                m_szOut += "<td>" + vtRes[i].szHandPhone + "</td>";
                m_szOut += "<td>" + vtRes[i].szEmail + "</td>";
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Group);         
        }
    }   
}
