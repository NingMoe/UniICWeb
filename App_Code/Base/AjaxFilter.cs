using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;

/// <summary>
/// AjaxFilter 的摘要说明
/// </summary>
public class AjaxFilter : Stream
{
#region properties
    Stream responseStream;
    long position;
    StringBuilder html = new StringBuilder();
    string m_szBeginID = null;
    string m_szEndID = null;
    Encoding m_ContentEncoding;
    UniWebLib.UniPage m_page;
#endregion
#region constructor
    public AjaxFilter(UniWebLib.UniPage page,Stream inputStream, Encoding ContentEncoding, string szID)
    {
        m_page = page;
        responseStream = inputStream;
        m_ContentEncoding = ContentEncoding;
        m_szBeginID = "<!--[B_AJAXID=" + szID + "]-->";
        m_szEndID = "<!--[E_AJAXID=" + szID + "]-->";
    }
#endregion
#region implemented abstract members
    public override bool CanRead
    {
        get { return true; }
    }
    public override bool CanSeek
    {
        get { return true; }
    }
    public override bool CanWrite
    {
        get { return true; }
    }
    public override void Close()
    {
        responseStream.Close();
    }
    public override void Flush()
    {
        responseStream.Flush();
    }
    public override long Length
    {
        get { return 0; }
    }
    public override long Position
    {
        get { return position; }
        set { position = value; }
    }
    public override long Seek(long offset, System.IO.SeekOrigin direction)
    {
        return responseStream.Seek(offset, direction);
    }
    public override void SetLength(long length)
    {
        responseStream.SetLength(length);
    }
    public override int Read(byte[] buffer, int offset, int count)
    {
        return responseStream.Read(buffer, offset, count);
    }
#endregion
#region write method
    public override void Write(byte[] buffer, int offset, int count)
    {
        //m_ContentEncoding = System.Text.UTF8Encoding.UTF8;
        string sBuffer = m_ContentEncoding.GetString(buffer, offset, count);
        if (m_szBeginID != null)
        {
            int pos = sBuffer.IndexOf(m_szBeginID);
            if (pos >= 0)
            {
                sBuffer = sBuffer.Substring(pos + m_szBeginID.Length);
                m_szBeginID = null;
            }
            else
            {
                return;
            }
        }
        if (m_szEndID != null)
        {
            int pos = sBuffer.IndexOf(m_szEndID);
            if (pos >= 0)
            {
                sBuffer = sBuffer.Substring(0, pos);
                m_szEndID = null;
                if (m_page.m_szScript.Length > 0)
                {
                    sBuffer += "<script type=\"text/javascript\">" + m_page.m_szScript + "</script>";
                }
            }
        }
        else
        {
            return;
        }

        byte[] data = m_ContentEncoding.GetBytes(sBuffer);
        responseStream.Write(data, 0, data.Length);
    }
#endregion
}