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
    protected string m_szDeviceList= "";
    protected string szGroupMember = "";
    protected string m_szGroupMemberList = "";
	protected void Page_Load(object sender, EventArgs e)
	{
        string szGroupID = Request["dwID"];
        UNIGROUP[] groupList=GetGroupByID(uint.Parse(szGroupID));
        if(groupList==null||groupList.Length==0)
        {
            return;
        }
        UNIGROUP setGroup = groupList[0];
        GROUPMEMBER[] groupMemberList = setGroup.szMembers;
        if (groupMemberList != null && groupMemberList.Length > 0)
        {
            for (int i = 0; i < groupMemberList.Length; i++)
            {
                string szNameTemp = groupMemberList[i].szName;
                if (szNameTemp == null|| szNameTemp == "")
                {
                    continue;
                }
                if (szNameTemp.IndexOf('(') < 0)
                {
                    continue;
                }
                string szName=szNameTemp.Substring(0,szNameTemp.IndexOf('('));
                m_szGroupMemberList += GetInputItemHtml(CONSTHTML.checkBox, "groupMemberList", szName, groupMemberList[i].dwMemberID.ToString());
            }
        }
        DEVREQ devReq=new DEVREQ();
        devReq.dwClassKind=(uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS;
        UNIDEVICE[] deviceList;
        if(m_Request.Device.Get(devReq,out deviceList)==REQUESTCODE.EXECUTE_SUCCESS&&deviceList!=null&&deviceList.Length>0)
        {
            for(int i=0;i<deviceList.Length;i++)
            {
                if(szGroupID!=deviceList[i].dwUseGroupID.ToString())
                {
                m_szDeviceList+=GetInputItemHtml(CONSTHTML.checkBox,"groupList",deviceList[i].szDevName,deviceList[i].dwUseGroupID.ToString());
                }
            }
        }

        if (IsPostBack)
        {
            string szgroupMember = Request["groupMemberList"];
            string szgroup = Request["groupList"];
            string[] szgroupMemberList = szgroupMember.Split(',');
            string[] szgroupList = szgroup.Split(',');
            for (int i = 0; i < szgroupMemberList.Length; i++)
            {
                UNIACCOUNT accinfo;
                if (GetAccByAccno(szgroupMemberList[i], out accinfo))
                {
                    for (int j = 0; j < szgroupList.Length; j++)
                    {
                        uint uGroupID = Parse(szgroupList[j]);
                        AddGroupMember(uGroupID, accinfo.dwAccNo, (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL, accinfo.szTrueName + "(" + accinfo.szLogonName+","+accinfo.szDeptName + ")");
                    }
                }
            }
            MessageBox("复制成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
            return; 
        }
      
        m_Title = "复制免预约使用人员";
	}
}
