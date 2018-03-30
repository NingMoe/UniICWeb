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
        if (IsPostBack)
        {
            string szDevID = Request["dwDevid"];
            string szRoomID = Request["dwRoomID"];
            string szPriceStart = Request["dwPricStart"];
            string szPriceEnd = Request["dwPriceEnd"];
            string szDateStart = Request["dwDateStart"];
            string szDateEnd = Request["dwDateEnd"];
            string szApproveID = Request["dwApproveID"];
            string szName = "";
            DEVREQ devGet = new DEVREQ();
            if (szDevID != null && szDevID != "")
            {
                string szDevName = Request["szDevName"];
                szName += "资产:" + szDevName + ";";
                devGet.dwDevID = Parse(szDevID);
            }
            if (szRoomID != null && szRoomID != "")
            {
                string szROOMNAME = Request["szRoomName"];
                szName += "实验室:" + szROOMNAME + ";";
                devGet.szRoomIDs = szRoomID;
            }
            if (szPriceStart != null && szPriceStart != "" && szPriceEnd != null && szPriceEnd != "")
            {
                szName += "价格区间:" + szPriceStart + "-" + szPriceEnd + ";";
                devGet.dwMinUnitPrice = Parse(szPriceStart);
                devGet.dwMinUnitPrice = Parse(szPriceEnd);
            }
            if (szDateStart != null && szDateStart != "" && szDateEnd != null && szDateEnd != "")
            {
                szName += "购置日期:" + szPriceStart + "-" + szPriceEnd + ";";
                devGet.dwSPurchaseDate = DateToUint(szDateStart);
                devGet.dwEPurchaseDate = DateToUint(szDateEnd);
            }
            UNIDEVICE[] vtDevList;

            if (m_Request.Device.Get(devGet, out vtDevList) == REQUESTCODE.EXECUTE_SUCCESS && vtDevList != null && vtDevList.Length > 0)
            {
                OOSDEV[] oosdevList = new OOSDEV[vtDevList.Length];
                for (int i = 0; i < oosdevList.Length; i++)
                {
                    oosdevList[i] = new OOSDEV();
                    oosdevList[i].dwDevID = vtDevList[i].dwDevID;

                }
                OUTOFSERVICE setOutOfSer = new OUTOFSERVICE();
                setOutOfSer.dwOOSStat = (uint)OUTOFSERVICE.DWOOSSTAT.OOSSTAT_APPROVE;
                setOutOfSer.OOSDev = new OOSDEV[oosdevList.Length];
                setOutOfSer.OOSDev = oosdevList;
                setOutOfSer.dwApproveDate = 0;
                setOutOfSer.dwApproveID = Parse(szApproveID);
                setOutOfSer.szOOSInfo = szName;
                if (Request.Files!=null&&Request.Files.Count > 0)
                {
                    string fileName = Request.Files["fileurl"].FileName;
                    if (fileName == null)
                    {
                        fileName = "";
                    }
                    string szFileExtName = "";
                    if (fileName.LastIndexOf('.') > -1)
                    {
                        szFileExtName = fileName.Substring(fileName.LastIndexOf('.'));
                    }
                    string szTempPath = MyVPath + "Upload/Assert/" + GetDevSN() + szFileExtName;
                    setOutOfSer.szMemo = szTempPath;
                    if (m_Request.Assert.OutOfSericeApply(setOutOfSer, out setOutOfSer) != REQUESTCODE.EXECUTE_SUCCESS)
                    {
                        MessageBox(m_Request.szErrMessage, "报废失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);

                    }
                    else
                    {
                        MessageBox("报废成功", "报废成功", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                        string szTempRawPath = Server.MapPath(szTempPath);
                        Request.Files[0].SaveAs(szTempRawPath);
                        return;
                    }
                }
            }
            else
            {
                MessageBox("没有符合条件的资产信息", "报废失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                return;
            }
        }


        if (Request["op"] == "set")
        {
           
        }
        else
        {
            m_Title = ConfigConst.GCDevName + "报废";
        }
	}
}
