using System;
using UniWebLib;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;
public partial class Sub_Course : UniPage
{
    protected MyString m_szOut = new MyString();
    public class TempBigDev
    {
        public uint dwDevID;
        public uint dwTUseTime;
        public uint dwRUseTime;
        public uint dwSUseTime;
        public uint dwOUseTime;
        public uint dwSampleNum;
        public uint dwUseStudents;
        public uint dwUseTeachers;
        public uint dwUseOthers;
        public uint dwRItemNum;
        public uint dwTItemNum;
        public uint dwSItemNum;
        public uint dwNReward;
        public uint dwPReward;
        public uint dwTPatent;
        public uint dwSPatent;
        public uint dwThreeIndex;
        public uint dwKernelJournal;
        public string szAttendantName;

    };
    public bool bLeader = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        uint uYearTerm = 20131401;
        if ((((ADMINLOGINRES)Session["LoginResult"]).dwManRole & (uint)ADMINLOGINRES.DWMANROLE.MANROLE_LEADER) > 0)
        {
            bLeader = true;
        }
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        BIGDEVREQ vrParameter = new BIGDEVREQ();
        vrParameter.dwUnitPrice = 100000;
        BIGDEV[] vrResult;
        string opSub = Request["opSub"];
        if (IsPostBack && opSub == "1")
        {
            vrParameter.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_SAVE;
            vrParameter.dwYearTerm = uYearTerm;
            uResponse = m_Request.Report.GetBigDev(vrParameter, out vrResult);

            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null && vrResult.Length > 0)
            {
                for (int i = 0; i < vrResult.Length; i++)
                {
                    BIGDEV setValue2 = vrResult[i];
                    setValue2.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_DEPLOY;
                    m_Request.Report.SetBigDev(setValue2);
                }
            }
        }
        string szValue = Request["changeInfo"];
        if (IsPostBack && szValue != "" && opSub != "1")
        {

            szValue = "[" + szValue + "]";
            List<BIGDEV> devlist = JsonConvert.DeserializeObject<List<BIGDEV>>(szValue);
            for (int i = 0; i < devlist.Count; i++)
            {
                BIGDEV tempValue = devlist[i];
                BIGDEV setValue = (BIGDEV)SetEmpty0ToNull<BIGDEV>(tempValue);
                setValue.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_SAVE;
                setValue.dwYearTerm = uYearTerm;
                m_Request.Report.SetBigDev(setValue);
            }
        }

        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        vrParameter.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_SAVE;
        vrParameter.dwYearTerm = uYearTerm;
        uResponse = m_Request.Report.GetBigDev(vrParameter, out vrResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null && vrResult.Length > 0)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id=" + vrResult[i].dwDevID.ToString() + ">" + ConfigConst.GCSchoolCode.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szAssertSN.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szClassSN.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szDevName.ToString() + "</td>";
                m_szOut += "<td>" + (uint)vrResult[i].dwUnitPrice + "</td>";
                m_szOut += "<td>" + vrResult[i].szModel.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szSpecification.ToString() + "</td>";
                m_szOut += "<td class='tdSet' data-type='dwTUseTime'>" + ((uint)vrResult[i].dwTUseTime) + "</td>";
                m_szOut += "<td class='tdSet' data-type='dwRUseTime'>" + ((uint)vrResult[i].dwRUseTime) + "</td>";
                m_szOut += "<td class='tdSet' data-type='dwSUseTime'>" + ((uint)vrResult[i].dwSUseTime) + "</td>";
                m_szOut += "<td class='tdSet' data-type='dwOUseTime'>" + ((uint)vrResult[i].dwOUseTime) + "</td>";
                m_szOut += "<td class='tdSet' data-type='dwSampleNum'>" + vrResult[i].dwSampleNum.ToString() + "</td>";
                m_szOut += "<td class='tdSet' data-type='dwUseStudents'>" + vrResult[i].dwUseStudents.ToString() + "</td>";
                m_szOut += "<td class='tdSet' data-type='dwUseTeachers'>" + vrResult[i].dwUseTeachers.ToString() + "</td>";
                m_szOut += "<td class='tdSet' data-type='dwUseOthers'>" + vrResult[i].dwUseOthers.ToString() + "</td>";
                m_szOut += "<td class='tdSet' data-type='dwRItemNum'>" + vrResult[i].dwRItemNum.ToString() + "</td>";
                m_szOut += "<td class='tdSet' data-type='dwTItemNum'>" + vrResult[i].dwTItemNum.ToString() + "</td>";
                m_szOut += "<td class='tdSet' data-type='dwSItemNum'>" + vrResult[i].dwSItemNum.ToString() + "</td>";
                m_szOut += "<td class='tdSet' data-type='dwNReward'>" + vrResult[i].dwNReward.ToString() + "</td>";
                m_szOut += "<td class='tdSet' data-type='dwPReward'>" + vrResult[i].dwPReward.ToString() + "</td>";
                m_szOut += "<td class='tdSet' data-type='dwTPatent'>" + vrResult[i].dwTPatent.ToString() + "</td>";
                m_szOut += "<td  class='tdSet' data-type='dwSPatent'>" + vrResult[i].dwSPatent.ToString() + "</td>";
                m_szOut += "<td  class='tdSet' data-type='dwThreeIndex'>" + vrResult[i].dwThreeIndex.ToString() + "</td>";
                m_szOut += "<td  class='tdSet' data-type='dwKernelJournal'>" + vrResult[i].dwKernelJournal.ToString() + "</td>";
                m_szOut += "<td  class='tdSet' data-type='szAttendantName'>" + vrResult[i].szAttendantName.ToString() + "</td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Report);
        }
        PutBackValue();
    }
}
