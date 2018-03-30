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

public partial class Sub_Course : UniPage
{
    protected MyString m_szOut = new MyString();
    protected string szDept = "";
    struct REQEXTINFExt
    {
        private Reserved reserved;

        public uint? dwStartLine;		/*开始行*/

        public uint? dwNeedLines;		/*需获取行数*/

        public uint? dwTotolLines;		/*服务端返回总行数*/

        public string szOrderKey;		/*排序字段*/

        public string szOrderMode;		/*排序方式(ASC或DESC)*/

        public RTUSESTAT szExtInfo;		/*根据不同的请求相关扩展信息*/
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        RTUSESTATREQ vrParameter = new RTUSESTATREQ();
        RTUSESTAT[] vrResult;
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (!IsPostBack)
        {
            dwStartDate.Value = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
            dwEndDate.Value = DateTime.Now.ToString("yyyy-MM-dd");

        }
        UNIDEPT[] dept = GetAllDept();
        if (dept != null)
        {
            szDept += GetInputItemHtml(CONSTHTML.option, "", "全部", "0");
            for (int i = 0; i < dept.Length; i++)
            {
                szDept+=GetInputItemHtml(CONSTHTML.option,"",dept[i].szName,dept[i].dwID.ToString());
            }
        }
        uint uDeptID = Parse(Request["deptid"]);
        if (uDeptID != 0)
        {
            vrParameter.dwDeptID = uDeptID;
        }
        vrParameter.dwStartDate = DateToUint(dwStartDate.Value);
        vrParameter.dwEndDate = DateToUint(dwEndDate.Value);    
        uResponse=m_Request.Report.GetRTUseStat(vrParameter,out vrResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null && vrResult.Length>0)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td class=\"devTd\" data-id=\"" + vrResult[i].dwStatID.ToString() + "\"><a>" + vrResult[i].szStatName.ToString() + "</a></td>";
                m_szOut += "<td>" + vrResult[i].szExtName + "</td>";
                m_szOut += "<td>" + vrResult[i].dwResvTimes.ToString() + "</td>";
                uint uUseTime = (uint)vrResult[i].dwResvMinutes;

                m_szOut += "<td>" + uUseTime / 60 + "小时" + uUseTime % 60 + "分钟" + "</td>";
                m_szOut += "<td>" + vrResult[i].dwSampleNum.ToString() + "</td>";//
                m_szOut += "<td>" + GetFee(vrResult[i].dwRealCost) + "</td>";

                m_szOut += "<td>" + GetFee(vrResult[i].dwDevUseFee) + "</td>"; //  

                m_szOut += "<td>" + GetFee(vrResult[i].dwSampleFee) + "</td>"; ////

                m_szOut += "<td>" + GetFee(vrResult[i].dwEntrustFee) + "</td>"; //             
                m_szOut += "</tr>";
            }
            {
                UpdatePageCtrl(m_Request.Report);
                REQEXTINFExt ext;
                m_Request.Report.UTPeekDetail(out ext);

                m_szOut += "<tr>";
            
                m_szOut += "<td colspan='2'>" + "合计"+ "</td>";
                m_szOut += "<td>" + ext.szExtInfo.dwResvTimes.ToString() + "</td>";
                uint uUseTime = (uint)ext.szExtInfo.dwResvMinutes;

                m_szOut += "<td>" + uUseTime / 60 + "小时" + uUseTime % 60 + "分钟" + "</td>";
                m_szOut += "<td>" + ext.szExtInfo.dwSampleNum.ToString() + "</td>";//
                m_szOut += "<td>" + GetFee(ext.szExtInfo.dwRealCost) + "</td>";

                m_szOut += "<td>" + GetFee(ext.szExtInfo.dwDevUseFee) + "</td>"; //  

                m_szOut += "<td>" + GetFee(ext.szExtInfo.dwSampleFee) + "</td>"; ////

                m_szOut += "<td>" + GetFee(ext.szExtInfo.dwEntrustFee) + "</td>"; //             
                m_szOut += "</tr>";
            }
          
        }
      
        PutBackValue();
    }  
}
