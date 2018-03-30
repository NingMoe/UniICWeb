<%@ WebHandler Language="C#" Class="uploadHandler" %>

using System;
using System.Web;
using System.IO;
using System.Collections.Generic;

public class uploadHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
            /*任意上传文件漏洞 暂时关闭
        context.Response.ContentType = "text/plain";
        context.Response.Charset = "utf-8";

        HttpPostedFile file = context.Request.Files["Filedata"];
        string uploadPath =
            HttpContext.Current.Server.MapPath(@context.Request["folder"]) + "\\";

        if (file != null)
        {
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            file.SaveAs(uploadPath + file.FileName);
            //下面这句代码缺少的话，上传成功后上传队列的显示不会自动消失
            context.Response.Write("1");
        }
        else
        {
            context.Response.Write("0");
        }  
        */
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }





    //protected void upEnclosure_FileUploaded(object sender, CuteEditor.UploaderEventArgs args)
    //{

    //    Manager u = (Manager)Session["manager"];
    //    if (u != null)
    //    {
    //        string username = u.Logname;
    //        string dir = Server.MapPath("..\\uploads\\") + username;
    //        if (!Directory.Exists(dir))
    //        {
    //            Directory.CreateDirectory(dir);
    //        }
    //        string filename = DateTime.Now.ToString("yyyyMMddHHmmssff") + args.FileName;
    //        args.CopyTo(dir + "\\" + filename);
    //        lbFilePath.Value = "/Uploads/" + username + '/' + filename;
    //        MessageBox.Show(this, "上传成功！");
    //        spEnclosureText.InnerHtml = "文件【" + args.FileName + "】已成功上传!";
    //    }
    //    else
    //    {
    //        Response.Redirect("Login.aspx");
    //    }
    //}
    //protected void WordUp_FileUploaded(object sender, CuteEditor.UploaderEventArgs args)
    //{

    //    Manager u = (Manager)Session["manager"];
    //    if (u != null)
    //    {
    //        string[] strlist = args.FileName.Split('.');
    //        if (strlist.Length == 1)
    //        {
    //            MessageBox.Show(this, "文件格式不正确！word文件请先另存为htm/html格式的网页文件，按操作说明顺序上传！");
    //            return;
    //        }
    //        else
    //        {
    //            string ext = strlist[strlist.Length - 1];

    //            List<string> set = new List<string>();
    //            set.Add("htm");
    //            set.Add("HTM");
    //            set.Add("html");
    //            set.Add("HTML");
    //            List<string> img = new List<string>();
    //            img.Add("jpg");
    //            img.Add("JPG");
    //            img.Add("gif");
    //            img.Add("GIF");
    //            img.Add("png");
    //            img.Add("PNG");
    //            img.Add("xml");
    //            img.Add("XML");

    //            if (set.Contains(ext))
    //            {
    //                string dir = Server.MapPath("..\\Words");
    //                if (!Directory.Exists(dir))
    //                {
    //                    Directory.CreateDirectory(dir);
    //                }
    //                string filename = DateTime.Now.ToString("yyyyMMddHHmmssff") + args.FileName;
    //                char[] filesream = args.ClientData.ToCharArray();
    //                string filepath = dir + "\\" + filename;
    //                args.CopyTo(filepath);
    //                FileStream fs = new FileStream(filepath, FileMode.Open);
    //                byte[] bytes = new byte[(int)fs.Length];
    //                fs.Read(bytes, 0, bytes.Length);
    //                fs.Close();
    //                fs.Dispose();
    //                string oldfile = System.Text.Encoding.GetEncoding("gb2312").GetString(bytes);
    //                //StreamReader reader = new StreamReader(fs);
    //                string newfile = injectScript(oldfile);
    //                File.WriteAllText(filepath, newfile, System.Text.Encoding.GetEncoding("gb2312"));
    //                wordFilePath.Value = ResolveUrl("~/Words/") + filename;

    //                MessageBox.Show(this, "上传成功！");
    //                wordEnclosureText.InnerHtml = "网页文件【" + args.FileName + "】已成功上传!";
    //            }
    //            else if (img.Contains(ext) && !string.IsNullOrEmpty(wordFilePath.Value))
    //            {
    //                string[] list = wordFilePath.Value.Split('/');
    //                if (list.Length > 1)
    //                {
    //                    string filename = list[list.Length - 1].Substring(16).Split('.')[0];
    //                    string dir = Server.MapPath("..\\Words\\") + filename + ".files";
    //                    if (!Directory.Exists(dir))
    //                    {
    //                        Directory.CreateDirectory(dir);
    //                    }
    //                    string filepath = dir + "\\" + args.FileName;
    //                    args.CopyTo(filepath);
    //                    MessageBox.Show(this, "上传成功！");
    //                    wordEnclosureText.InnerHtml = "网页附加文件【" + args.FileName + "】已成功上传!";
    //                }
    //                else
    //                {
    //                    MessageBox.Show(this, "路径错误，上传失败！");
    //                }
    //            }
    //            else
    //            {
    //                MessageBox.Show(this, "文件格式不正确！word文件请先另存为htm/html格式的网页文件，按操作说明顺序上传！");
    //                return;
    //            }
    //        }
    //    }
    //    else
    //    {
    //        Response.Redirect("Login.aspx");
    //    }
    //}
    ////注入脚本
    //private string injectScript(string str)
    //{
    //    char[] bt = str.ToCharArray();
    //    string script1 = "<script type=\"text/javascript\">function iframeResizeHeight(frame_name, body_name, offset) {parent.document.getElementById(frame_name).height = document.getElementById(body_name).offsetHeight + offset;}function Resize() {var frame_name = \"myContent\";var body_name = \"myBody\";if (parent.document.getElementById(frame_name)) {return iframeResizeHeight(frame_name, body_name, 30);}}window.onload = Resize;</script>";
    //    string script2 = "  id=\"myBody\"";
    //    string pre = "";
    //    int n = 0;
    //    string mid = "";
    //    string tail = "";
    //    for (int i = 0; i < bt.Length; i++)
    //    {
    //        if (bt[i] == '<')
    //        {
    //            if (bt[i + 1] == '/')
    //            {
    //                if (bt[i + 2] == 'h' || bt[i + 2] == 'H')
    //                {
    //                    if ((bt[i + 3] == 'e' || bt[i + 3] == 'E') && (bt[i + 4] == 'a' || bt[i + 4] == 'A') && (bt[i + 5] == 'd' || bt[i + 5] == 'D'))
    //                    {
    //                        pre = str.Substring(0, i);
    //                        n = i;
    //                    }
    //                }
    //            }
    //            if (bt[i + 1] == 'b' || bt[i + 1] == 'B')
    //            {
    //                if ((bt[i + 2] == 'o' || bt[i + 2] == 'O') && (bt[i + 3] == 'd' || bt[i + 3] == 'D') && (bt[i + 4] == 'y' || bt[i + 4] == 'Y'))
    //                {
    //                    mid = str.Substring(n, i + 5 - n);
    //                    tail = str.Substring(i + 5);
    //                    break;
    //                }
    //            }
    //        }
    //    }
    //    return pre + script1 + mid + script2 + tail; ;
    //}
}