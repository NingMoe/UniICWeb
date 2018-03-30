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
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            YARDRESVCHECKINFOREQ vrPar = new YARDRESVCHECKINFOREQ();
            GetHTTPObj(out vrPar);
            string szYardKind = Request["yardKind"];
            uint uYardKind = Parse(szYardKind);

            uint uBeginDate = GetDate(Request["dwStartDate"]);
            uint uEndDate = GetDate(Request["dwEndDate"]);
            vrPar.dwNeedYardResv = 1;
            if (vrPar.dwCheckStat == null || ((uint)vrPar.dwCheckStat) == 0)
            {
                vrPar.dwCheckStat = (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_CANDO;
            }
            YARDRESVCHECKINFO[] vtRes;
            string szResvTime = "";
            string szResvTimeAll = "";
            ArrayList listResvID = new ArrayList();
            uResponse = m_Request.Reserve.GetYardResvCheckInfo(vrPar, out vtRes);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
            {
                System.IO.StringWriter swCSV = new System.IO.StringWriter();
                swCSV.WriteLine("活动名称,申请人,申请资源,申请时间,申请部门,状态,审核时间,审核员");
                uint uResvID = (uint)vtRes[0].YardResv.dwResvID;
                for (int i = 0; i < vtRes.Length; i++)
                {
                    uint uResvDate = (uint)vtRes[i].YardResv.dwPreDate;
                    if (uResvDate > uEndDate || uResvDate < uBeginDate)
                    {
                        continue;
                    }
                    if (uYardKind != 0 && uYardKind != (uint)vtRes[i].YardResv.dwKind)
                    {
                        continue;
                    }
                    bool bISExist = false;
                    for (int k = 0; k < listResvID.Count; k++)
                    {
                        uint uResvIDTemp = (uint)listResvID[k];
                        if (uResvIDTemp == uResvID)
                        {
                            bISExist = true;
                            continue;
                        }
                    }
                    if (bISExist)
                    {
                        continue;
                    }
                    listResvID.Add(uResvID);
                    System.Text.StringBuilder sbText = new System.Text.StringBuilder();
                    sbText = AppendCSVFields(sbText, vtRes[i].YardResv.szActivityName);
                    sbText = AppendCSVFields(sbText, vtRes[i].YardResv.szApplicantName);
                    sbText = AppendCSVFields(sbText, vtRes[i].YardResv.szDevName);
                    YARDRESV[] vtResvGroup = GetYardResvByGroupID((uint)vtRes[0].YardResv.dwResvID);
                    if (vtResvGroup != null && vtResvGroup.Length > 0)
                    {

                        if (vtResvGroup.Length > 1)
                        {
                            szResvTime += "【" + vtResvGroup.Length + "】条:" + "<br/>";
                        }
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
                            // else
                            {
                                szResvTimeAll += Get1970Date(vtResvGroup[m].dwBeginTime) + "至" + Get1970Date(vtResvGroup[m].dwEndTime) + "；";
                            }
                        }
                    }
                    sbText = AppendCSVFields(sbText, szResvTimeAll);

                    string szCheckName = "";
                    CHECKTYPE checkType = new CHECKTYPE();
                    if (GetCheckType((uint)vtRes[i].dwCheckKind, out checkType))
                    {
                        szCheckName = GetJustNameEqual((uint)checkType.dwMainKind, "CheckType_Kind");
                    }
                    sbText = AppendCSVFields(sbText, (vtRes[i].szCheckName) + ":" + szCheckName);
                    sbText = AppendCSVFields(sbText, GetJustNameEqual(vtRes[i].dwCheckStat, "Admin_CheckStatus"));
                    sbText = AppendCSVFields(sbText, Get1970Date(vtRes[i].dwCheckTime));
                    sbText = AppendCSVFields(sbText, vtRes[i].szAdminName);

                    //去掉尾部的逗号
                    sbText.Remove(sbText.Length - 1, 1);

                    //写datatable的一行
                    swCSV.WriteLine(sbText.ToString());
                }
                DownloadFile(Response, swCSV.GetStringBuilder(), "RuleDaySum.csv");
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
                strResHeader = "inline; filename=" + System.Web.HttpUtility.UrlPathEncode("审核信息.csv");
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