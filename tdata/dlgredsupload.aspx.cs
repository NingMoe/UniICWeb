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
using System.Threading;
using System.IO;
using System.Diagnostics;
using UniWebLib;

public partial class tdata_redsupload : UniPage
{
    protected string m_szOut = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string id =Request["id"];
        if (id == null || id == "")
        {
            MessageBox("实验项目信息为空不能上传", "提示", MSGBOX.ERROR);
           
        }
        else
        {
            ViewState["id"] = id;
        }
        if (IsPostBack)
        {
            UNIACCOUNT accno = (UNIACCOUNT)Session["loginAcc"];
            TESTITEMINFOREQ vrGet = new TESTITEMINFOREQ();
            vrGet.dwAccNo = accno.dwAccNo;
            vrGet.dwTestItemID = Parse(id);
            TESTITEMINFO[] testitemInfo;
            if (!(m_Request.Reserve.GetTestItemInfo(vrGet, out testitemInfo) == REQUESTCODE.EXECUTE_SUCCESS && testitemInfo != null && testitemInfo.Length > 0))
            {
               MessageBox("获取不到实验项目信息为空不能上传", "提示", MSGBOX.ERROR);
               
            }
            string filePath = HttpContext.Current.Server.MapPath("~\\ClientWeb\\upload\\UpLoadFile\\" + testitemInfo[0].dwTestItemID.ToString());
            if (Directory.Exists(filePath) == false)//如果不存在就创建file文件夹
            {
                try
                {
                    Directory.CreateDirectory(filePath);
                }
                catch
                {
                   MessageBox("创建目录失败，请稍后再试", "提示", MSGBOX.ERROR);
                   
                }
            }

            if (AttachFile.HasFile)
            {
                string FileName = this.AttachFile.FileName;//获取上传文件的文件名,包括后缀
                string ExtenName = System.IO.Path.GetExtension(FileName);//获取扩展名
                string filePathInfo = filePath + "\\" + accno.szLogonName + "_" + accno.szTrueName + "_" + DateTime.Now.ToString("yyyyMMddhhmm") + ExtenName;
                string SaveFileName = System.IO.Path.Combine(filePath, accno.szLogonName + "_" + accno.szTrueName + "_" + DateTime.Now.ToString("yyyyMMddhhmm") + ExtenName);//合并两个路径为上传到服务器上的全路径
                if (this.AttachFile.ContentLength > 0)
                {
                    try
                    {
                        this.AttachFile.MoveTo(SaveFileName, Brettle.Web.NeatUpload.MoveToOptions.Overwrite);
                    }
                    catch (Exception ex)
                    {

                       MessageBox(ex.ToString(), "提示", MSGBOX.ERROR);
                       
                    }
                }
                string url = filePathInfo;  //文件保存的路径
                float FileSize = (float)System.Math.Round((float)AttachFile.ContentLength / 1024000, 1); //获取文件大小并保留小数点后一位,单位是M

                REPORTUPLOAD upload = new REPORTUPLOAD();
                upload.dwAccNo = accno.dwAccNo;
                upload.dwSID = testitemInfo[0].dwSID;
                upload.szReportURL = accno.szLogonName + "_" + accno.szTrueName + "_" + DateTime.Now.ToString("yyyyMMddhhmm") + ExtenName;
                if (!(m_Request.Reserve.UploadReport(upload) == REQUESTCODE.EXECUTE_SUCCESS))
                {
                   MessageBox(m_Request.szErrMessage, "提示", MSGBOX.ERROR);
                   
                }
                else
                {
                  //  Response.Write("<script>alert('');opener.parent.mainFrame.location.reload();</script>");
                    MessageBox("上传成功", "提示", MSGBOX.SUCCESS,MSGBOX_ACTION.OK);
                    Session["tabs"] = 0;
                }
            }
        }
    }
}