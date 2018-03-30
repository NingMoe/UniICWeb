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
using System.Collections;
using Newtonsoft.Json;

public partial class _Default : UniPage
{
    protected bool bSet = false;
    protected string m_Title = "";
    protected string m_szRoom = "";
    protected string m_szDev = "";
    public class ctrlRoom
    {
        public string szDcsNO;
        public string szCtrlNo;
        public string szRoomNo;
    };
    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack)
        {
            DOORCTRLREQ vrParameter = new DOORCTRLREQ();
            vrParameter.dwDCSKind = (uint)UNIDCS.DWDCSKIND.DCSKIND_DOORCTRL;
            UNIDOORCTRL[] vrResult;
            ArrayList list = new System.Collections.ArrayList();
            string szError = "";
            if (m_Request.DoorCtrlSrv.GetDoorCtrl(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                for (int i = 0; i < vrResult.Length; i++)
                {
                    ctrlRoom value = new ctrlRoom();
                    value.szDcsNO = vrResult[i].dwDCSSN.ToString();
                    value.szCtrlNo = vrResult[i].dwCtrlSN.ToString();
                    value.szRoomNo = vrResult[i].szRoomNo.ToString();
                    list.Add(value);
                }
            }

            string json = JsonConvert.SerializeObject(list);
            string path = Server.MapPath("~/") + ("padtxt\\dcsRoom.txt");
            FileStream myFs = new FileStream(path, FileMode.Create);//txtFilePath为生成txt文件的路径
            StreamWriter mySw = new StreamWriter(myFs);
            mySw.Write(json);//writeStr为要写入的字符串
            mySw.Close();
            myFs.Close();
            MessageBox("提示:" + vrResult.Length + "条", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
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