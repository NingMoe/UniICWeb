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
using Newtonsoft.Json;
using System.Collections.Generic;
public partial class Sub_Course : UniPage
{
    protected MyString m_szOut = new MyString();
    protected string m_szDev = "";
    protected string m_szLab= "";
    protected string m_szRoom= "";
    protected string m_szKind= "";
    public bool bLeader = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        uint uYearTerm = 20131401;
        if ((((ADMINLOGINRES)Session["LoginResult"]).dwManRole & (uint)ADMINLOGINRES.DWMANROLE.MANROLE_LEADER) > 0)
        {
            bLeader = true;
        }
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        LABINFOREQ vrParameter = new LABINFOREQ();

        LABINFO[] vrResult;        
        GetPageCtrlValue(out vrParameter.szReqExtInfo);

        string szValue = Request["changeInfo"];
        if (IsPostBack && szValue != "")
        {
            szValue = "[" + szValue + "]";
            List<LABINFO> devlist = JsonConvert.DeserializeObject<List<LABINFO>>(szValue);
            for (int i = 0; i < devlist.Count; i++)
            {
                LABINFO tempValue = devlist[i];
                LABINFO setValue = (LABINFO)SetEmpty0ToNull<LABINFO>(tempValue);
                setValue.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_SAVE;
                setValue.dwYearTerm = uYearTerm;
                m_Request.Report.SetLabInfo(setValue);
            }
        }

      
        vrParameter.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_DEPLOY;
        vrParameter.dwYearTerm = uYearTerm;
        uResponse=m_Request.Report.GetLabInfo(vrParameter,out vrResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null && vrResult.Length > 0)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id='"+vrResult[i].dwLabID+"'>" + ConfigConst.GCSchoolCode.ToString() + "</td>"; 
                m_szOut += "<td>" + vrResult[i].szLabSN.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szLabName.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].dwLabClass.ToString() + "</td>";
                m_szOut += "<td>" +GetDateStr(vrResult[i].dwCreateDate)+ "</td>";
                m_szOut += "<td>" + "0" + "</td>";//面积
                m_szOut += "<td>" + vrResult[i].szAcademicSubjectCode.ToString() + "</td>";
                m_szOut += "<td class='' data-type='dwTNReward'>" + vrResult[i].dwTNReward.ToString() + "</td>";
                m_szOut += "<td  class='' data-type='dwTPReward'>" + vrResult[i].dwTPReward.ToString() + "</td>";
                m_szOut += "<td  class='' data-type='dwTPatent'>" + vrResult[i].dwTPatent.ToString() + "</td>";

                uint uStudentReword = (uint)vrResult[i].dwSNReward + (uint)vrResult[i].dwSPReward + (uint)vrResult[i].dwSPatent;
                m_szOut += "<td  class='' data-type='uStudentReword'>" + uStudentReword.ToString() + "</td>";

                m_szOut += "<td  class='' data-type='dwTThreeIndex'>" + vrResult[i].dwTThreeIndex.ToString() + "</td>";
                m_szOut += "<td  class='' data-type='dwRThreeIndex'>" + vrResult[i].dwRThreeIndex.ToString() + "</td>";

                m_szOut += "<td  class='' data-type='dwTKernelJournal'>" + vrResult[i].dwTKernelJournal.ToString() + "</td>";
                m_szOut += "<td  class='' data-type='dwRKernelJournal'>" + vrResult[i].dwRKernelJournal.ToString() + "</td>";

                m_szOut += "<td  class='' data-type='dwTestBookNum'>" + vrResult[i].dwTestBookNum.ToString() + "</td>";

                m_szOut += "<td  class='' data-type='dwPRItemNum'>" + vrResult[i].dwPRItemNum.ToString() + "</td>";
                m_szOut += "<td  class='' data-type='dwRItemNum'>" + vrResult[i].dwRItemNum.ToString() + "</td>";

                m_szOut += "<td  class='' data-type='dwSItemNum'>" + vrResult[i].dwSItemNum.ToString() + "</td>";

                m_szOut += "<td  class='' data-type='dwPRItemNum'>" + vrResult[i].dwPRItemNum.ToString() + "</td>";
                m_szOut += "<td  class='' data-type='dwPTItemNum'>" + vrResult[i].dwPTItemNum.ToString() + "</td>";

                m_szOut += "<td  class='' data-type='dwBKThesisUsers'>" + vrResult[i].dwBKThesisUsers.ToString() + "</td>";
                m_szOut += "<td  class='' data-type='dwZKThesisUsers'>" + vrResult[i].dwZKThesisUsers.ToString() + "</td>";
                m_szOut += "<td  class='' data-type='dwSSThesisUsers'>" + vrResult[i].dwSSThesisUsers.ToString() + "</td>";
              //  m_szOut += "<td>" + vrResult[i].dwBSThesisUsers.ToString() + "</td>";

                m_szOut += "<td  class='' data-type='dwItemNum'>" + vrResult[i].dwItemNum.ToString() + "</td>";
                m_szOut += "<td  class='' data-type='dwOtherItemNum'>" + vrResult[i].dwOtherItemNum.ToString() + "</td>";
                m_szOut += "<td   class='' data-type='dwUseUsers'>" + vrResult[i].dwUseUsers.ToString() + "</td>";
                m_szOut += "<td  class='' data-type='dwOtherUsers'>" + vrResult[i].dwOtherUsers.ToString() + "</td>";
                m_szOut += "<td  class='' data-type='dwUseTime'>" + vrResult[i].dwUseTime.ToString() + "</td>";
                m_szOut += "<td  class='' data-type='dwOtherTime'>" + vrResult[i].dwOtherTime.ToString() + "</td>";

                m_szOut += "<td  class='' data-type='dwPartTimeUsers'>" + vrResult[i].dwPartTimeUsers.ToString() + "</td>";

                m_szOut += "<td  class='' data-type='dwTotalCost'>" + vrResult[i].dwTotalCost.ToString() + "</td>";
                m_szOut += "<td  class='' data-type='dwConsumeCost'>" + vrResult[i].dwConsumeCost.ToString() + "</td>";
                
                    
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Report);
        }   
        
        PutBackValue();
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreLoad(e);
        string szlab = Request["szLab"];
        string szDevKind = Request["szDevKind"];
        string szRoom = Request["szRoom"];
        if (szlab != null && szlab != "")
        {
            PutMemberValue2("szLab", szlab);
        }      
        if (szRoom != null && szRoom != "")
        {
            PutMemberValue2("szRoom", szRoom);
        }
    }
}
