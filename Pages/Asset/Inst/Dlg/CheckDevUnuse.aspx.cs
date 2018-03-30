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
    protected string szHref = "";
	protected void Page_Load(object sender, EventArgs e)
	{
        if (IsPostBack)
        {
            OUTOFSERVICEREQ vrGet = new OUTOFSERVICEREQ();
            vrGet.dwOOSID = Parse(Request["id"]);
            OUTOFSERVICE[] vtSer;
            if (m_Request.Assert.OutOfSericeGet(vrGet, out vtSer) == REQUESTCODE.EXECUTE_SUCCESS && vtSer != null && vtSer.Length > 0)
            {
              
                OUTOFSERVICE setOutOfSer = new OUTOFSERVICE();
                setOutOfSer = vtSer[0];
                string szState = Request["dwstae"];
                setOutOfSer.dwOOSStat = Parse(szState);
                UNIACCOUNT vrAccInfo = ((ADMINLOGINRES)Session["LoginResult"]).AccInfo;
                if (setOutOfSer.dwApplyID == vrAccInfo.dwAccNo)
                {
                    MessageBox("申请人和审批人不能同一个", "审批失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                    return;
                }
                if (m_Request.Assert.OutOfSericeApprove(setOutOfSer, out setOutOfSer) != REQUESTCODE.EXECUTE_SUCCESS)
                {
                    MessageBox(m_Request.szErrMessage, "审批失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                }
                else
                {
                    MessageBox("审批成功", "审批成功", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                    return;
                }

            }

        }


        if (Request["op"] == "set")
        {
            OUTOFSERVICEREQ vrGet = new OUTOFSERVICEREQ();
            vrGet.dwOOSID = Parse(Request["id"]);
            OUTOFSERVICE[] vtSer;
            if (m_Request.Assert.OutOfSericeGet(vrGet, out vtSer) == REQUESTCODE.EXECUTE_SUCCESS && vtSer != null && vtSer.Length > 0)
            {
                szHref = vtSer[0].szMemo;
                string dwApplyDate2 = GetDateStr((uint)vtSer[0].dwApplyDate);
                PutHTTPObj(vtSer[0]);
                PutMemberValue("dwApplyDate2", dwApplyDate2);
            }
        }
        else
        {
            m_Title = ConfigConst.GCDevName + "审批";
        }
	}
}
