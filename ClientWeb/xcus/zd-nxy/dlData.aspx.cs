using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.IO;
using System.Diagnostics;
using UniWebLib;
using UniStruct;

public partial class DevWeb_dlData : UniClientPage
{
    protected string m_szFilePath = @"E:\实验室管理系统\FileServer\FILES";
    protected bool m_bEncode = false;


    protected string m_szErrMsg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        base.LoadPage();
        if (Session["LOGIN_ACCINFO"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        //if (!Directory.Exists(m_szFilePath))
        //{
        //    Response.Write("文件路径错误！");
        //    return;
        //}

        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;

        TESTDATAREQ vrReq = new TESTDATAREQ();
        vrReq.dwSID = Convert.ToUInt32(Request["ID"]);
        UNITESTDATA[] vtResult;
        uResponse = m_Request.Account.TestDataGet(vrReq, out vtResult);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
            m_szErrMsg = "下载失败:" + m_Request.szErrMessage;
        }
        else if (vtResult.Length == 0)
        {
            m_szErrMsg = "找不到文件";
        }
        else
        {
            vtResult[0].dwStatus = new UniDW((uint)UNITESTDATA.DWSTATUS.TDSTAT_DOWNLOADED);
            uResponse = m_Request.Account.TestDataChgStat(vtResult[0]);

            string szUniFSPath = vtResult[0].szLocation.ToString();
            string szUserFileName = vtResult[0].szDisplayName.ToString();
            string szsubDate = vtResult[0].dwSubmitDate.ToString();
            int nLast = szUniFSPath.LastIndexOf("\\");
            if (szUserFileName.LastIndexOf(".") <= 0)
            {
                string szUniFSPath2;
                if (szUniFSPath.EndsWith(".uni"))
                {
                    szUniFSPath2 = szUniFSPath.Substring(0, szUniFSPath.Length - 4);
                }
                else
                {
                    szUniFSPath2 = szUniFSPath;
                }
                int nLast2 = szUniFSPath2.LastIndexOf(".");
                if (nLast2 > nLast)
                {
                    szUserFileName += szUniFSPath2.Substring(nLast2);
                }
            }
            string szWebPath = "~/temp/";
            if (szUniFSPath[nLast] == '\\')
            {
                nLast++;
            }
            szWebPath += szsubDate;
            string szResult = "";
            string szCmd;

            string szFilePath = Server.MapPath(szWebPath);
            if (!Directory.Exists(szFilePath))
            {
                Directory.CreateDirectory(szFilePath);
            }
            szFilePath += "\\" + szUniFSPath.Substring(nLast);
            string szEncodeFilepath = "";
            if (m_bEncode)
            {
                szEncodeFilepath = szFilePath;//+ ".uni";
            }
            else
            {
                szEncodeFilepath = szFilePath;//+ ".unf";
            }
            if (string.IsNullOrEmpty(m_szFilePath))
            {
                szCmd = Server.MapPath(".") + "\\UniFTPClient.exe GET \"" + szEncodeFilepath + "\" \"" + szUniFSPath + "\"";
                szResult = WinExec(szCmd);
            }
            else
            {
                int nIndex = szUniFSPath.IndexOf('\\');
                //string szFileServerFILES = m_szFilePath + szUniFSPath.Substring(nIndex);
                string szFileServerFILES = szFilePath;//从上边的绝对路径下获取文件改到相对路径下\\tmp内获取文件   20141125改

                if (!File.Exists(szFileServerFILES))
                {
                    Response.Write("文件不存在！");
                    return;
                }
                //if (!File.Exists(szEncodeFilepath))
                //{
                //    File.Copy(szFileServerFILES, szEncodeFilepath);
                //}
                szResult = "OK";
            }

            if (szResult.IndexOf("ERROR") >= 0)
            {
                m_szErrMsg = "下载文件失败";
            }
            else
            {
                if (m_bEncode)
                {
                    szCmd = Server.MapPath(".") + "\\EncodeFile.exe Decode #password# \"" + szEncodeFilepath + "\" \"" + szFilePath + "\"";
                    szResult = WinExec(szCmd);
                    File.Delete(szEncodeFilepath);
                }
                else
                {
                    this.Response.Write("szEncodeFilepath=" + szEncodeFilepath + "<br />" + szFilePath);
                    // File.Move(szEncodeFilepath, szFilePath);

                    szResult = "OK";
                }

                if (szResult.IndexOf("ERROR") >= 0)
                {
                    m_szErrMsg = "下载文件失败";
                }
                else
                {
                    FileInfo fi = new FileInfo(szFilePath);
                    if (fi.Length > 1024 * 1024)
                    {
                        //Response.Redirect(szWebPath);

                        System.IO.Stream iStream = null;
                        byte[] buffer = new Byte[10000];
                        int length;
                        long dataToRead;
                        try
                        {
                            iStream = new System.IO.FileStream(szFilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
                            dataToRead = iStream.Length;
                            Response.Clear();
                            Response.ContentType = "application/octet-stream";
                            Response.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(szUserFileName, System.Text.Encoding.UTF8));
                            Response.AppendHeader("Content-Length", dataToRead.ToString());
                            while (dataToRead > 0)
                            {
                                if (Response.IsClientConnected)
                                {
                                    length = iStream.Read(buffer, 0, 10000);
                                    Response.OutputStream.Write(buffer, 0, length);
                                    Response.Flush();
                                    buffer = new Byte[10000];
                                    dataToRead = dataToRead - length;
                                }
                                else
                                {
                                    dataToRead = -1;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Response.Write("Error : " + ex.Message);
                        }
                        if (iStream != null)
                        {
                            iStream.Close();
                        }
                        File.Delete(szFilePath);
                    }
                    else
                    {
                        Response.Clear();
                        Response.ContentType = "application/x-download";       //application/octet-stream
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(szUserFileName, System.Text.Encoding.UTF8));
                        Response.Flush();

                        Response.WriteFile(szFilePath);
                        Response.Flush();
                        File.Delete(szFilePath);
                        Response.End();
                    }
                }
            }
        }

    }
    string WinExec(string szCmd)
    {
        Logger.Trace("szCmd=" + szCmd);

        string ResultStr;
        Process p = new Process();
        p.StartInfo.FileName = "cmd.exe";
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.RedirectStandardInput = true;
        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.RedirectStandardError = true;
        p.StartInfo.CreateNoWindow = true;
        p.StartInfo.ErrorDialog = false;
        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        p.Start();
        p.StandardInput.WriteLine(szCmd);
        p.StandardInput.WriteLine("exit");
        ResultStr = p.StandardOutput.ReadToEnd();
        p.Close();
        return ResultStr;
    }

    protected string GetRealIP()
    {
        string ip;
        try
        {
            if (Request.ServerVariables["HTTP_VIA"] != null)
            {
                ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString().Split(',')[0].Trim();
            }
            else
            {
                ip = Request.UserHostAddress;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
        return ip;
    }

    protected string GetViaIP()
    {
        string viaIp = null;
        try
        {
            if (Request.ServerVariables["HTTP_VIA"] != null)
            {
                viaIp = Request.UserHostAddress;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
        return viaIp;
    }

    protected string GetFileCount()
    {
        uint nFileCount = 0;
        if (Application["UploadFileCount"] != null && Application["UploadFileCount"].GetType() == typeof(uint))
        {
            nFileCount = (uint)Application["UploadFileCount"];
        }
        nFileCount++;
        Application["UploadFileCount"] = nFileCount;
        return nFileCount + "_";
    }
}