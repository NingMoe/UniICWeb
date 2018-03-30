using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
public class UploadInfo
{
    public bool IsReady
    {
        get { return IsReady; }
        set { IsReady = value; }
    }
    public int ContentLength
    {
        get { return ContentLength; }
        set { ContentLength = value; }
    }
    public int UploadedLength
    {
        get { return UploadedLength; }
        set { UploadedLength = value; }
    }
    public string FileName
    {
        get { return FileName; }
        set { FileName = value; }
    }
}
