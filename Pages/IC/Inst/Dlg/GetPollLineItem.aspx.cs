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

public partial class _Default :UniPage
{
    protected bool bSet = false;
    protected string m_Title = "";
    protected string sz_Out = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        POLLONLINEREQ vrGet = new POLLONLINEREQ();
        vrGet.dwPollID = Parse(Request["ID"]);
        
        POLLONLINE[] vtRes;
        if (m_Request.Admin.GetPollOnLine(vrGet, out vtRes) != REQUESTCODE.EXECUTE_SUCCESS)
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
                POLLITEM[] itemList = vtRes[0].PollItems;
                for (int i = 0; itemList != null && i < itemList.Length; i++)
                {
                    sz_Out += "<tr><td>" + itemList[i].szItemName +"</td><td>"+itemList[i].dwVotes.ToString()+ "</td></tr>";
                }
            }
            m_Title = "查看投票信息";


        }
    }
}
