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
using System.Text.RegularExpressions;

/// <summary>
/// AjaxFilter 的摘要说明
/// </summary>
public class ExportCSVFilter : Stream
{
#region properties
    Stream responseStream;
    long position;
    string m_szID = null;
    Encoding m_ContentEncoding;
    int nStep = 0;
#endregion
#region constructor
    public ExportCSVFilter(Stream inputStream, Encoding ContentEncoding, string szID, string szName)
    {
        responseStream = inputStream;
        m_ContentEncoding = ContentEncoding;
        m_szID = szID;
        nStep = 0;
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
        if (nStep == 0)
        {
            Match m = Regex.Match(sBuffer, "<table[ \\t]+id=[\\\"']"+m_szID+"[\\\"'].*>");
            if (m.Success)
            {
                sBuffer = sBuffer.Substring(m.Index);
                nStep = 1;
            }
            else
            {
                return;
            }
        }
        if (nStep == 1)
        {
            Match m = Regex.Match(sBuffer, "</tbody>");
            if (m.Success)
            {
                sBuffer = sBuffer.Substring(0, m.Index+m.Length) + "</table>";
                nStep = 2;
            }
            else
            {
                m = Regex.Match(sBuffer, "</table>");
                if (m.Success)
                {
                    sBuffer = sBuffer.Substring(0, m.Index + m.Length);
                    nStep = 2;
                }
            }
        }
        else
        {
            return;
        }


        //-----------------
        byte[] data = m_ContentEncoding.GetBytes(sBuffer);
        responseStream.Write(data, 0, data.Length);
    }
#endregion
}