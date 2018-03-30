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
using System.IO;
using System.Text;
public partial class _Default : UniPage
{
    protected bool bSet = false;
    protected string m_Title = "";
    protected string m_szRoom="";
    protected string m_szDev = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack)
        {
            RESVREQ vrParameter = new RESVREQ();
            string szCheckStat = Request["dwCheckStat"];
            string szKey = Request["szGetKey"];
            if (szKey != null && szKey != "")
            {

            }
           
            vrParameter.dwBeginDate = GetDate(Request["dwStartDate"]);
            vrParameter.dwEndDate = GetDate(Request["dwEndDate"]);

            vrParameter.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_TEACHING;
            if (!(szCheckStat == null || szCheckStat == "" || szCheckStat == "0"))
            {
                vrParameter.dwCheckStat = Parse(szCheckStat);
            }
            string szRoomNo = Request["szRoomNo"];
            if (!string.IsNullOrEmpty(szRoomNo))
            {
                vrParameter.szRoomNos = szRoomNo;
            }
            string szPid = Request["dwPID"];
            if (szPid != null && szPid != "")
            {
                UNIACCOUNT accno;
                if (GetAccByLogonName((szPid.ToString().Trim()), out accno, true))
                {
                    vrParameter.dwOwner = accno.dwAccNo;
                }
            }
            System.IO.StringWriter swCSV = new System.IO.StringWriter();
            UNIRESERVE[] vrResult;
            vrParameter.dwStatFlag = (uint)RESVREQ.DWSTATFLAG.STATFLAG_OVER | (uint)RESVREQ.DWSTATFLAG.STATFLAG_INUSE;
        //   GetPageCtrlValue(out vrParameter.szReqExtInfo);
            if (m_Request.Reserve.Get(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                swCSV.WriteLine("项目名称,上课教师,房间,班级,状态,上课时间,绝对时间");
                uint uTimeNow = Get1970Seconds(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                for (int i = 0; i < vrResult.Length; i++)
                {
                  
                    System.Text.StringBuilder sbText = new System.Text.StringBuilder();
                   
                    uint uState = (uint)vrResult[i].dwStatus;
                    sbText = AppendCSVFields(sbText, vrResult[i].szTestName);
                    sbText = AppendCSVFields(sbText, vrResult[i].szOwnerName.ToString());
                    UNIROOM[] roomList;
                    RESVDEV[] resvdev = vrResult[i].ResvDev;
                    string szRoomName = "";
                    for (int k = 0; k < resvdev.Length; k++)
                    {
                        roomList = GetRoomByNO(resvdev[k].szRoomNo, vrResult[i].dwLabID);
                        if (roomList != null && roomList.Length > 0)
                        {
                            if (szRoomName.IndexOf(roomList[0].szRoomName) < 0)
                            {
                                szRoomName += roomList[0].szRoomName + ",";
                            }
                        }

                    }
                    if (szRoomName.EndsWith(","))
                    {
                        szRoomName = szRoomName.Substring(0, szRoomName.Length - 1);
                    }
                    sbText = AppendCSVFields(sbText, szRoomName );
                    sbText = AppendCSVFields(sbText,  vrResult[i].szMemberName);
                    sbText = AppendCSVFields(sbText, GetJustName((vrResult[i].dwStatus), "Reserve_Status"));
                   
                    UNIRESERVE setValue = vrResult[i];
                    string szBeginTime = Get1970Date(setValue.dwBeginTime, "yyyy-MM-dd HH:mm");
                    string szEndtime = Get1970Date(setValue.dwEndTime, "HH:mm");
                    string szTeachTime = GetTeachingTime((uint)setValue.dwTeachingTime);
                    sbText = AppendCSVFields(sbText, szTeachTime);
                    sbText = AppendCSVFields(sbText, szBeginTime + "到" + szEndtime);
                    sbText.Remove(sbText.Length - 1, 1);

                    //写datatable的一行
                    swCSV.WriteLine(sbText.ToString());
                }
            

                DownloadFile(Response, swCSV.GetStringBuilder(), "teachplan.csv");
                swCSV.Close();
                Response.End();
            }
        }
        
    }
    private StringBuilder AppendCSVFields(StringBuilder argSource, string argFields)
    {
        return argSource.Append(argFields.Replace(",", " ").Trim()).Append(",");
    }
    public void DownloadFile(HttpResponse argResp, StringBuilder argFileStream, string strFileName)
    {
        try
        {
            string strResHeader = "attachment; filename=" + Guid.NewGuid().ToString() + ".csv";
            if (!string.IsNullOrEmpty(strFileName))
            {
                strResHeader = "inline; filename=" + System.Web.HttpUtility.UrlPathEncode("预约状况.csv");
            }
            argResp.AppendHeader("Content-Disposition", strResHeader);//attachment说明以附件下载，inline说明在线打开
            argResp.ContentType = "application/ms-excel";
            argResp.ContentEncoding = Encoding.GetEncoding("GB2312"); // Encoding.UTF8;//
            argResp.Write(argFileStream);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}