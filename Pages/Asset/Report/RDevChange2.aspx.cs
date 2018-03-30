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
public partial class Sub_Course : UniPage
{
    public class DEVCHG2
    {
        public uint  dwYearTerm;		/*学期编号*/

        public uint  dwReportStat;		/*报表状态*/

        public uint  dwBDevNum;		/*期初数量*/

        public uint  dwBMoney;		/*期初金额*/

        public uint  dwBBigDevNum;		/*大型仪器期初数量*/

        public uint  dwBBigMoney;		/*大型仪器期初金额*/

        public uint  dwIncDevNum;		/*增加数量*/

        public uint  dwIncMoney;		/*增加金额*/

        public uint  dwIncBigDevNum;		/*大型仪器增加数量*/

        public uint  dwIncBigMoney;		/*大型仪器增加金额*/

        public uint  dwDecDevNum;		/*减少数量*/

        public uint  dwDecMoney;		/*减少金额*/

        public uint  dwDecBigDevNum;		/*大型仪器减少数量*/

        public uint  dwDecBigMoney;		/*大型仪器减少金额*/

        public uint  dwEDevNum;		/*期末数量*/

        public uint  dwEMoney;		/*期末金额*/

        public uint  dwEBigDevNum;		/*大型仪器期末数量*/

        public uint  dwEBigMoney;		/*大型仪器期末金额*/
    };

    protected MyString m_szOut = new MyString();
    public bool bLeader = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        DEVCHGREQ vrParameter = new DEVCHGREQ();
        if ((((ADMINLOGINRES)Session["LoginResult"]).dwManRole & (uint)ADMINLOGINRES.DWMANROLE.MANROLE_LEADER) > 0)
        {
            bLeader = true;
        }
        uint uYearTerm = 20131401;
        DEVCHG vrResult;   
        string szValue = Request["changeInfo"];
        string opSub = Request["opSub"];
        if (IsPostBack && opSub == "1")
        {
            vrParameter.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_SAVE;
            vrParameter.dwYearTerm = uYearTerm;
            uResponse = m_Request.Report.GetDevChg(vrParameter, out vrResult);

            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrResult.dwYearTerm != null)
            {
                DEVCHG setValue2 = vrResult;
                setValue2.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_DEPLOY;
                m_Request.Report.SetDevChg(setValue2);
            }
        }
        if (IsPostBack && szValue != null && szValue != "" && opSub != "1")
        {
           // szValue = szValue.Substring(0, szValue.Length - 1);
            //szValue = "[" + szValue + "]";

            DEVCHG2 vrResult2 = (DEVCHG2)JsonConvert.DeserializeObject(szValue, typeof(DEVCHG2));

            DEVCHG setValue = new DEVCHG();
            setValue.dwYearTerm = uYearTerm;
            setValue.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_SAVE;
            setValue.dwBDevNum =vrResult2.dwBDevNum==0?null:(uint?)vrResult2.dwBDevNum;
            setValue.dwBMoney = vrResult2.dwBMoney == 0 ? null : (uint?)vrResult2.dwBMoney;
            setValue.dwBBigDevNum = vrResult2.dwBBigDevNum == 0 ? null : (uint?)vrResult2.dwBBigDevNum;
            setValue.dwBBigMoney = vrResult2.dwBBigMoney == 0 ? null : (uint?)vrResult2.dwBBigMoney;
            setValue.dwIncDevNum = vrResult2.dwIncDevNum == 0 ? null : (uint?)vrResult2.dwIncDevNum;
            setValue.dwIncMoney = vrResult2.dwIncMoney == 0 ? null : (uint?)vrResult2.dwIncMoney;
            setValue.dwIncBigDevNum = vrResult2.dwIncBigDevNum == 0 ? null : (uint?)vrResult2.dwIncBigDevNum;
            setValue.dwIncBigMoney = vrResult2.dwIncBigMoney == 0 ? null : (uint?)vrResult2.dwIncBigMoney;
            setValue.dwEDevNum = vrResult2.dwEDevNum == 0 ? null : (uint?)vrResult2.dwEDevNum;
            setValue.dwEMoney = vrResult2.dwEMoney == 0 ? null : (uint?)vrResult2.dwEMoney;
            setValue.dwEBigDevNum = vrResult2.dwEBigDevNum == 0 ? null : (uint?)vrResult2.dwEBigDevNum;
            setValue.dwEBigMoney = vrResult2.dwEBigMoney == 0 ? null : (uint?)vrResult2.dwEBigMoney;
            setValue.dwDecBigDevNum = vrResult2.dwDecBigDevNum == 0 ? null : (uint?)vrResult2.dwDecBigDevNum;
            setValue.dwDecBigMoney = vrResult2.dwDecBigMoney == 0 ? null : (uint?)vrResult2.dwDecBigMoney;
            setValue.dwDecDevNum = vrResult2.dwDecDevNum == 0 ? null : (uint?)vrResult2.dwDecDevNum;
            setValue.dwDecMoney = vrResult2.dwDecMoney == 0 ? null : (uint?)vrResult2.dwDecMoney;
            uResponse = m_Request.Report.SetDevChg(setValue);
        }

       
            vrParameter.dwYearTerm = uYearTerm;
            vrParameter.dwReportStat = (uint)DEVLIST.DWREPORTSTAT.REPORTSTAT_SAVE;
            uResponse = m_Request.Report.GetDevChg(vrParameter, out vrResult);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrResult.dwYearTerm != null)
            {
                for (int i = 0; i < 1; i++)
                {
                    m_szOut += "<tr>";
                    m_szOut += "<td>" + ConfigConst.GCSchoolCode.ToString() + "</td>";
                    m_szOut += "<td class='tdSet' data-type='dwBDevNum'>" + vrResult.dwBDevNum.ToString() + "</td>";
                    m_szOut += "<td class='tdSet' data-type='dwBMoney'>" + ((uint)vrResult.dwBMoney) + "</td>";
                    m_szOut += "<td class='tdSet' data-type='dwBBigDevNum'>" + vrResult.dwBBigDevNum.ToString() + "</td>";
                    m_szOut += "<td class='tdSet' data-type='dwBBigMoney'>" + ((uint)vrResult.dwBBigMoney) + "</td>";
                    m_szOut += "<td class='tdSet' data-type='dwIncDevNum'>" + vrResult.dwIncDevNum.ToString() + "</td>";
                    m_szOut += "<td class='tdSet' data-type='dwIncMoney'>" + ((uint)vrResult.dwIncMoney) + "</td>";
                    m_szOut += "<td class='tdSet' data-type='dwDecDevNum'>" + vrResult.dwDecDevNum.ToString() + "</td>";
                    m_szOut += "<td class='tdSet' data-type='dwDecMoney'>" + ((uint)vrResult.dwDecMoney) + "</td>";
                    m_szOut += "<td class='tdSet' data-type='dwEDevNum'>" + vrResult.dwEDevNum.ToString() + "</td>";
                    m_szOut += "<td class='tdSet' data-type='dwEMoney'>" + ((uint)vrResult.dwEMoney) + "</td>";
                    m_szOut += "<td class='tdSet' data-type='dwEBigDevNum'>" + vrResult.dwEBigDevNum.ToString() + "</td>";
                    m_szOut += "<td class='tdSet' data-type='dwEBigMoney'>" + ((uint)vrResult.dwEBigMoney) + "</td>";
                    m_szOut += "</tr>";
                }
            }
            UpdatePageCtrl(m_Request.Report);
        
       
        PutBackValue();
    }
}
