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
    protected string m_szDoorCtrl = "";
    protected string m_szLab = "";
    protected string m_szRoom = "";
    protected string m_szDevKind = "";
	protected void Page_Load(object sender, EventArgs e)
	{
       
        if (IsPostBack)
        {
            string szPid=Request["dwPollID"];
            uint uPID=Parse(szPid);
            POLLONLINE newValue = new POLLONLINE();
            GetHTTPObj(out newValue);
            newValue.dwPollKind = (uint)POLLONLINE.DWPOLLKIND.POLLKIND_MTICKS;
            newValue.dwPollScope = (uint)POLLONLINE.DWPOLLSCOPE.POLLSCOPE_MEMBER_LOOK + (uint)POLLONLINE.DWPOLLSCOPE.POLLSCOPE_MEMBER_VOTE;
            POLLITEM[] itemList = (POLLITEM[])Session["POLLITEM"];
            if (itemList != null)
            {
                uint uGroupID = 1;
                for (uint i = 0; i < itemList.Length; i++)
                {
                    itemList[i].dwGroupID = uGroupID;
                    uGroupID = uGroupID + 1;
                    if (itemList[i].dwItemID == null)
                    {
                        itemList[i].dwPollKind = (uint)POLLONLINE.DWPOLLKIND.POLLKIND_MTICKS;
                        itemList[i].dwMaxTickItems = 1;
                    }
                }
            }
            newValue.PollItems = itemList;

            ADMINLOGINRES vrAccInfo = (ADMINLOGINRES)Session["LoginResult"];

            newValue.dwAccNo = vrAccInfo.AdminInfo.dwAccNo;
            //return;
            if (m_Request.Admin.SetPollOnLine(newValue, out newValue) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "设置失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox("设置成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                Session["POLLITEM"] = null;
                return;
            }
        }
        if (Request["op"] == "set")
        {
            bSet = true;

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
                    PutJSObj(vtRes[0]);
                    m_Title = "修改投票";
                    Session["POLLITEM"] = vtRes[0].PollItems;
                }
            }
        }
        else
        {
            m_Title = "新建投票";

        }                      
	}
}
