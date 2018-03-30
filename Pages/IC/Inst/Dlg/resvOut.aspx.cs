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


            RESVREQ vrGet = new RESVREQ();
            string szResvID = Request["id"];
            string szText = Request["szMessageInfo"];
            if (szResvID == null || szResvID == "")
            {                
                MessageBox("设置失败", "提示", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                return;
            }
            vrGet.dwResvID = Parse(szResvID);
            vrGet.dwStatFlag = (uint)RESVREQ.DWSTATFLAG.STATFLAG_DEL + (uint)RESVREQ.DWSTATFLAG.STATFLAG_INUSE + (uint)RESVREQ.DWSTATFLAG.STATFLAG_OVER;
            UNIRESERVE[] vtRes;
            if (m_Request.Reserve.Get(vrGet,out vtRes)==REQUESTCODE.EXECUTE_SUCCESS&& vtRes!=null&&vtRes.Length>0)
            {
                ADMINCREDIT setValue = new ADMINCREDIT();
                setValue.dwAccNo = vtRes[0].dwOwner;
                setValue.szTrueName = vtRes[0].szOwnerName;
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
                setValue.szReason = szMessageInfo;
                setValue.szMemo = szMessageInfo;
                if (m_Request.System.AdminCreditDo(setValue) == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    MessageBox("人工违约处理成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                }
                else
                {
                    MessageBox(m_Request.szErrMessage.ToString(), "提示", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
 
                }
            }
        }

    }
}
