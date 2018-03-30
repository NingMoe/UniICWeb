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

    protected string szFileName = "";
	protected void Page_Load(object sender, EventArgs e)
    {
        UNILAB newLab;
    
        if (IsPostBack)
        {
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            SFROLEINFOREQ vrPar = new SFROLEINFOREQ();
            vrPar.dwApplyID = Parse(Request["ID"]);
           // vrPar.dwStatus = (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_NONE;
            //vrPar.dwAuthType = (uint)SYSFUNCRULE.DWAUTHTYPE.AUTHBY_REARCHTEST;

            SFROLEINFO[] vtRes;
            uResponse = m_Request.System.SFRoleGet(vrPar, out vtRes);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS==vtRes.Length>0)
            {
                vtRes[0].dwStatus = (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK;
                uResponse = m_Request.System.SFRoleCheck(vtRes[0]);
                if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    MessageBox("审核通过", "", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                }
                else {
                    MessageBox(m_Request.szErrMessage, "审核失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                }
            }
        }
       
        if (Request["op"] == "set")
        {
            bSet = true;
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            SFROLEINFOREQ vrPar = new SFROLEINFOREQ();
            vrPar.dwApplyID = Parse(Request["ID"]);
           SFROLEINFO[] vtRes;
            uResponse = m_Request.System.SFRoleGet(vrPar, out vtRes);
            if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else
            {
                if (vtRes.Length == 0)
                {
                    MessageBox("获取失败", "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                }
                else
                {
                    PutJSObj(vtRes[0]);
                    if (vtRes[0].szApplyURL != null && vtRes[0].szApplyURL != "")
                    {
                        string[] szApplyUrlList = vtRes[0].szApplyURL.Split('&');
                        for (int i = 0; i < szApplyUrlList.Length; i++)
                        {
                            string temp = szApplyUrlList[i];
                            if (temp == "")
                            {
                                continue;
                            }
                            string szFilePath = "../../../ClientWeb/upload/UpLoadFile/" + temp;
                            szFileName += "<a style='color:blue' target='_blank' href='" + szFilePath + "'>点击下载申请报告(" + (i + 1) + ")，</a>";
                        }
                    }
                    m_Title = "查看信息";
                }
            }
        }
        else
        {
            m_Title = "查看审核";

        }
    }
}
