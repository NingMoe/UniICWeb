using System;
using System.Collections.Generic;
using System.Web;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class apkuploadfile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        string szFileName = Request.Form["filename"];

        HttpFileCollection fileCollection = Request.Files;
        string szFilePath = "";
        {
            HttpPostedFile uploadFile = fileCollection[0];
            
            try
            {
                Logger.trace("文件个数:" + fileCollection.Count.ToString());
                for (int i = 0; i < fileCollection.Count; i++)
                {
                    Logger.trace("文件名" + fileCollection[i].FileName);

                    //Logger.trace("上传文件路劲:" + Server.MapPath("~/")  + szFileName);
                    if (System.IO.Directory.Exists(Server.MapPath("~/Log/androidlog")) == false)//如果不存在就创建file文件夹
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Log/androidlog"));
                    }
                    uploadFile.SaveAs(Server.MapPath("~/Log/androidlog/") + fileCollection[i].FileName);
                }
            }
            catch (Exception ex)
            {
                Logger.trace(ex.ToString());
            }
        }
    }
 
}