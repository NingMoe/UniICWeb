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
     
        if (IsPostBack)
        {

            UNIACCOUNT accno = (UNIACCOUNT)Session["loginAcc"];

            string filePath = HttpContext.Current.Server.MapPath("~\\ClientWeb\\upload\\UpLoadFile\\MYDISK\\" + accno.szLogonName.ToString());
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
                if (isExist(FileName,(uint)accno.dwAccNo))
                {
                    MessageBox("文件已经存在，想要继续上传请先删除文件名【" + FileName + "】'", "提示", MSGBOX.ERROR);

                  
                }

                string SaveFileName = System.IO.Path.Combine(filePath, FileName);//合并两个路径为上传到服务器上的全路径
                if (this.AttachFile.ContentLength > 0)
                {
                    try
                    {
                        this.AttachFile.MoveTo(SaveFileName, Brettle.Web.NeatUpload.MoveToOptions.Overwrite);
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('" + ex + "');</script>");
                      
                    }
                }
                string url = filePath+FileName;  //文件保存的路径
                float FileSize = (float)System.Math.Round((float)AttachFile.ContentLength / 1024000, 1); //获取文件大小并保留小数点后一位,单位是M

                CLOUDDISK upload = new CLOUDDISK();
                upload.dwAccNo = accno.dwAccNo;
                //upload.dwSubmitDate =GetDate(dat)
                upload.dwFileSize = ((uint)FileSize);
                upload.szFileName = FileName;
                upload.szLocation = FileName;
                upload.szMemo = Request["szmemo"];
                if (!(m_Request.Account.CloudDiskSave(upload, out upload) == REQUESTCODE.EXECUTE_SUCCESS))
                {
                    Response.Write("<script>alert('" + m_Request.szErrMessage + FileSize.ToString() + "');</script>");
                  
                }
                else
                {
                    MessageBox("上传成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                    Session["tabs"] = 1;
                }
            }
        }
    }
    protected bool isExist(string fileName, uint accno)
    {
        CLOUDDISKREQ cloudreq = new CLOUDDISKREQ();
        cloudreq.dwAccNo = accno;
        CLOUDDISK[] disk;
        if (m_Request.Account.CloudDiskOpen(cloudreq, out disk) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < disk.Length; i++)
            {
                if (disk[i].szFileName.ToString() == fileName)
                {
                    return true;
                }
            }
        }
        return false;
    }
     
}