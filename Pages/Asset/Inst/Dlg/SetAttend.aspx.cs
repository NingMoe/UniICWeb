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

public partial class _Default : UniPage
{
    protected bool bSet = false;
    protected string m_Title = "";
    protected string m_szSta = "";
	protected void Page_Load(object sender, EventArgs e)
	{

        string szOpExt ="";
        if (Request["opext"] == "ff")
        {
            szOpExt = "分发";
        }
        if (Request["opext"] == "lc")
        {
            szOpExt = "流转";
        }

        if (IsPostBack)
        {
           DEVATTENDANT devAttendSet = new DEVATTENDANT();
           
           GetHTTPObj(out devAttendSet);
           REQUESTCODE uResponse=m_Request.Device.AttendantSet(devAttendSet);
           if (uResponse==REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(szOpExt + "成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                UNIDEVICE dev = new UNIDEVICE();
                if (Request.Files.Count > 0&&  getDevByID(devAttendSet.dwDevID.ToString(), out dev))
                {
                    string fileName = Request.Files["fileurl"].FileName;
                    string szFileExtName = "";
                    szFileExtName = fileName.Substring(fileName.LastIndexOf('.'));
                    string szTempPath = MyVPath + "Upload/Assert/" + dev.szDevName.ToString() + dev.dwDevID.ToString() + szFileExtName;
                    dev.szDevURL = szTempPath;
                    m_Request.Device.Set(dev, out dev);
                    string szTempRawPath = Server.MapPath(szTempPath);
                    Request.Files[0].SaveAs(szTempRawPath);
                }
            }
            else
            {
                MessageBox(m_Request.szErrMessage, szOpExt+ "失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                return;
            }
        }

     
        if (Request["op"] == "set")
        {
            bSet = true;
            DEVREQ vrDevReq = new DEVREQ();
            vrDevReq.dwDevID = Parse(Request["id"]);
            UNIDEVICE[] vtDev;
            if (m_Request.Device.Get(vrDevReq, out vtDev) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else
            {
                if (vtDev.Length == 0)
                {
                    MessageBox("获取失败", "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                }
                else
                {
                    PutJSObj(vtDev[0]); 
                    m_Title = szOpExt+"【" + vtDev[0].szDevName + "】";
                }
            }
        }
        else
        {
            m_Title = szOpExt+"";
        }
	}
}
