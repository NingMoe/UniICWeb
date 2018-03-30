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
    protected string m_szRoom = "";
    protected string m_szDev = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack)
        {

            DOORCTRLREQ vrParameter = new DOORCTRLREQ();
            UNIDOORCTRL[] vrResult;
            vrParameter.dwGetType = (uint)DOORCTRLREQ.DWGETTYPE.DOORCTRLGET_BYALL;
            vrParameter.dwDCSKind = (uint)UNIDCS.DWDCSKIND.DCSKIND_DOORCTRL;
            if (m_Request.DoorCtrlSrv.GetDoorCtrl(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                System.IO.StringWriter swCSV = new System.IO.StringWriter();
                swCSV.WriteLine("集控器编号,控制器编号,门禁刷卡方向,名称");
                for (int i = 0; i < vrResult.Length; i++)
                {
                    uint uPropy=(uint)vrResult[i].dwCtrlKind;
                    System.Text.StringBuilder sbText = new System.Text.StringBuilder();
                    sbText = AppendCSVFields(sbText, vrResult[i].dwDCSSN.ToString());
                    sbText = AppendCSVFields(sbText, vrResult[i].dwCtrlSN.ToString());
                    sbText = AppendCSVFields(sbText, "InDoor");
                    sbText = AppendCSVFields(sbText, System.Web.HttpUtility.HtmlEncode(ConfigConst.GCSysKindRoom + ":" + vrResult[i].szRoomNo.ToString()));
                    //去掉尾部的逗号
                    sbText.Remove(sbText.Length - 1, 1);

                    //写datatable的一行
                    swCSV.WriteLine(sbText.ToString());
                    if((uPropy&(uint)UNIDOORCTRL.DWCTRLKIND.DCKIND_DOUBLE)>0)
                    {
                        System.Text.StringBuilder sbText1 = new System.Text.StringBuilder();
                        sbText1 = AppendCSVFields(sbText1, vrResult[i].dwDCSSN.ToString());
                        sbText1 = AppendCSVFields(sbText1, vrResult[i].dwCtrlSN.ToString());
                        sbText1 = AppendCSVFields(sbText1, "OutDoor");
                        sbText1 = AppendCSVFields(sbText1, System.Web.HttpUtility.HtmlEncode(ConfigConst.GCSysKindRoom+":" + vrResult[i].szRoomNo.ToString()));
                        sbText1.Remove(sbText1.Length - 1, 1);
                        swCSV.WriteLine(sbText1.ToString());
                    }
                   
                }
                DownloadFile(Response, swCSV.GetStringBuilder(), "门禁信息.csv");
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
                strResHeader = "inline; filename=" + System.Web.HttpUtility.UrlPathEncode("门禁信息.csv");
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