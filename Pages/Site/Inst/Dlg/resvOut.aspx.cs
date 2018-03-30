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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            

            YARDRESVREQ vrGet = new YARDRESVREQ();
            string szResvID = Request["id"];
            string szText = Request["szMessageInfo"];
            if (szResvID == null || szResvID == "")
            {                
                MessageBox("设置失败", "提示", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                return;
            }
            vrGet.dwResvID = Parse(szResvID);
            YARDRESV[] vtRes;
            if (m_Request.Reserve.GetYardResv(vrGet,out vtRes)==REQUESTCODE.EXECUTE_SUCCESS&& vtRes!=null&&vtRes.Length>0)
            {
                ADMINCREDIT setValue = new ADMINCREDIT();
                setValue.dwAccNo = vtRes[0].dwApplicantID;
                setValue.szTrueName = vtRes[0].szApplicantName;
                string szScore=Request["score"];
                if (szScore == "")
                {
                    MessageBox("请填写扣除分数", "提示", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                    return;
                }
                setValue.dwCreditScore = Parse(szScore);
                setValue.dwCTSN = (uint)CREDITKIND.DWCREDITSN.CREDIT_DEREGULATION;
                setValue.dwCreditSN = (uint)CREDITKIND.DWCREDITSN.CREDIT_DEREGULATION;
                setValue.dwSubjectID = Parse(szResvID);
                string szMessageInfo = Request["szMessageInfo"];
                if (szMessageInfo == "")
                {
                    MessageBox("必须填写原因", "提示", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                    return;
                }
                setValue.szReason = "不按时使用";
                setValue.szMemo = "不按时使用";
                if (m_Request.System.AdminCreditDo(setValue) == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    MessageBox("强制违约成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                }
                else
                {
                    MessageBox(m_Request.szErrMessage.ToString(), "提示", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
 
                }
            }
        }

    }
}
