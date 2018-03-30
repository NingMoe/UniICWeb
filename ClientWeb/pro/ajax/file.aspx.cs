using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
public partial class ClientWeb_pro_ajax_file : UniClientAjax
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LoadPage())
        {
            if (act == "upload")
            {
                if (IsLoginReady())
                {
                    string key = Request["file"];
                    string dir = Request["dir"];
                    string limit = Request["limit"];
                    string[] ban = { "cs", "cpp", "c", "asp", "aspx", "bat", "ini", "exe" };//文件黑名单
                    string name = Request["ren"];//重命名 *表示=文件名
                    if (dir == null) dir = "";
                    if (string.IsNullOrEmpty(key))
                    {
                        ErrMsg("参数有误");
                        return;
                    }
                    if (Request.Files.Count > 0 && Request.Files[0] != null)
                    {
                        HttpPostedFile file = Request.Files[0];
                        Logger.trace("文件存在");
                        if (string.IsNullOrEmpty(file.FileName))
                        {
                            ErrMsg("未选择文件！若实际已选择，可能页面已超时(刷新重试)。");
                            return;
                        }
                        uint len=ToUInt(GetConfig("fileLength"));
                        if (len == 0) len = 4;//默认4M
                        if (file.ContentLength >= (len*1024*1024))
                        {
                            ErrMsg("文件大小不能大于"+len+"M");
                            return;
                        }
                        string szFilename = Server.UrlDecode(file.FileName);
                        //检查格式限制
                        string[] _tp = szFilename.Split('.');
                        string suffix = (_tp[_tp.Length - 1]).ToLower();
                        for (int i = 0; i < ban.Length; i++)
                        {
                            if (ban[i] == suffix)
                            {
                                ErrMsg("拒绝接受不安全的上传文件");
                                return;
                            }
                        }
                        if (!string.IsNullOrEmpty(limit))
                        {
                            limit = limit.ToLower();
                            if (_tp.Length < 2 || limit.IndexOf(suffix) < 0)
                            {
                                ErrMsg("文件格式不支持！支持的文件格式为：" + limit);
                                return;
                            }
                        }
                        szFilename = szFilename.Substring(szFilename.LastIndexOf("\\") + 1);
                        string pictureName = "";
                        if (szFilename != "")
                        {
                            int idx = szFilename.LastIndexOf(".");
                            suffix = szFilename.Substring(idx);//获得上传文件的后缀名 
                            if (string.IsNullOrEmpty(name))
                            {
                                string t = DateTime.Now.ToString("yyyyMMddHHmmssfff");//以当前时间为文件名，确保文件名没有重复
                                UNIACCOUNT acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
                                pictureName = acc.szTrueName+"_" +"申请报告" + "_" + t + suffix;  //szFilename.Substring(0, idx) + "_" + t + suffix; 
                            }
                            else if (name == "*")
                            {
                                pictureName = szFilename.Substring(0, idx) + suffix;
                            }
                            else
                            {
                                pictureName = name + suffix;
                            }
                        }
                        try
                        {
                            string path = Server.MapPath("~/ClientWeb/upload/UpLoadFile/" + dir);
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            file.SaveAs(path + pictureName);
                            SucRlt("上传成功", "{\"save\":\"" + pictureName + "\",\"name\":\"" + szFilename + "\"}");
                            if (GetConfig("upfileLog") == "1")
                            {
                                UNIACCOUNT acc;
                                string user = "noname";
                                if (Session["LOGIN_ACCINFO"] != null)
                                {
                                    acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
                                    user = acc.szTrueName + "(" + acc.szTrueName + ")";
                                }
                                SaveXmlData(pictureName, szFilename, "upfile_log", user, path);
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrMsg(ex.Message);
                        }
                    }
                    else
                    {
                        Logger.trace("文件不存在");
                        ErrMsg("上传失败");
                    }
                }
            }
            else if (act == "xml_set")
            {
                string id = Request["id"];
                string data = Request["data"];
                string type = Request["type"];
                string file = Request["file"];
                string title = Request["title"];
                string attrs = Request["attrs"];
                string state = Request["state"];

                if (SaveXmlData(id, data, type, file, title, attrs, state))
                {
                    SucMsg("保存成功");
                }
                else
                {
                    ErrMsg("保存失败");
                }
            }
            else if (act == "import_acc")
            {
                string key = Request["file"];
                string dir = Request["dir"];
                string limit = "csv";//Request["limit"];暂时仅支持csv格式
                if (dir == null) dir = "";
                if (string.IsNullOrEmpty(key))
                {
                    ErrMsg("参数有误");
                    return;
                }
                
                if (Request.Files.Count>0&&Request.Files[0] != null)
                {
                    HttpPostedFile file = Request.Files[0];
                    if (string.IsNullOrEmpty(file.FileName))// || file.ContentLength < 1 大小暂时不判断
                    {
                        ErrMsg("未选择文件！若实际已选择，可能页面已超时(刷新重试)。");
                        return;
                    }
                    string szFilename = Server.UrlDecode(file.FileName);
                    //检查格式限制
                    string[] _tp = szFilename.Split('.');
                    string suffix = (_tp[_tp.Length - 1]).ToLower();
                    if (!string.IsNullOrEmpty(limit))
                    {
                        limit = limit.ToLower();
                        if (_tp.Length < 2 || limit.IndexOf(suffix) < 0)
                        {
                            ErrMsg("文件格式不支持！支持的文件格式为：" + limit);
                            return;
                        }
                        if (suffix == "csv")
                        {
                            try
                            {
                                //string path = Server.MapPath("~/ClientWeb/upload/UpLoadFile/");
                                //if (!Directory.Exists(path))
                                //{
                                //    Directory.CreateDirectory(path);
                                //}
                                //file.SaveAs(path + "import_file.csv");
                                string[][] list = Csv2DataSet(file.InputStream);
                                SucRlt("导入成功", "{\"save\":\"\",\"name\":\"" + szFilename + "\",\"array\":"+CodeJson(list)+"}");
                                return;
                            }
                            catch (Exception ex)
                            {
                                ErrMsg(ex.Message);
                                return;
                            }
                        }
                    }
                }
                ErrMsg("操作失败");
            }
        }
    }
    public string[][] Csv2DataSet(Stream stream)
    {
        string strline;
        List<string[]> list = new List<string[]>();
        System.IO.StreamReader mysr = new System.IO.StreamReader(stream, System.Text.Encoding.Default);
        while ((strline = mysr.ReadLine()) != null)
        {
            list.Add(strToAry(strline));
        }
        mysr.Close();
        stream.Close();
        return list.ToArray();
    }

    // 请注意：以下为新添加的子函数
    private static string[] strToAry(string strLine)
    {
        string strItem = "";
        int iFenHao = 0;
        System.Collections.ArrayList lstStr = new System.Collections.ArrayList();

        for (int i = 0; i < strLine.Length; i++)
        {
            string strA = strLine.Substring(i, 1);

            if (strA == "\"")
            {
                iFenHao = iFenHao + 1;
            }

            if (iFenHao == 2)
            {
                iFenHao = 0;
            }

            if (strA == "," && iFenHao == 0)
            {
                lstStr.Add(strItem);
                strItem = "";
            }
            else
            {
                strItem = strItem + strA;
            }
        }

        if (strItem.Length > 0)
            lstStr.Add(strItem);

        return (String[])lstStr.ToArray(typeof(string));
    }
}