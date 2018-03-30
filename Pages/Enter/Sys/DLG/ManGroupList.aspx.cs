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
    protected string szManRole = "";
    protected string m_szRoom = "";
	protected void Page_Load(object sender, EventArgs e)
	{
        if (IsPostBack)
        {
            GROUPMEMDETAILREQ vrGet = new GROUPMEMDETAILREQ();
            vrGet.dwGroupID = Parse(Request["dwID"]);
            GROUPMEMDETAIL[] vtRes;
            if (m_Request.Group.GetGroupMemDetail(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                if (SetRoomGroupFromClient(vtRes))
                {
                    MessageBox("修改成功", "修改成功", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                    return;
                }
            }

        }
        GROUPMEMDETAILREQ vrGet1 = new GROUPMEMDETAILREQ();
        vrGet1.dwGroupID = Parse(Request["dwID"]);
        GROUPMEMDETAIL[] vtRes1;
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        uResponse=m_Request.Group.GetGroupMemDetail(vrGet1, out vtRes1);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
            MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
        }
        else if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes1 != null && vtRes1.Length > 0)
        {
            string szID = "";
            string szName = "";
            for (int i = 0; i < vtRes1.Length; i++)
            {
                szID += vtRes1[i].dwAccNo.ToString() + ",";
                szName += vtRes1[i].szTrueName.ToString() + ",";
            }
            SetReserved(ref vtRes1[0], "RoomGroup", szID);
            SetReserved(ref vtRes1[0], "RoomGroupName", szName);
            PutJSObj(vtRes1[0]);
            m_Title = "管理员名单";
        }
       
	}

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreLoad(e);        
        if(ViewState["szLogonName"]!=null&&ViewState["szLogonName"].ToString()!="")
        {
            PutMemberValue("szLogonNamePut", ViewState["szLogonName"].ToString());
        }
    }

    bool SetRoomGroupFromClient(GROUPMEMDETAIL[] vtGroupMember)
    {
        string szGroup = Request["reserved.RoomGroup"];
        if (string.IsNullOrEmpty(szGroup))
        {
            szGroup = "";
            //return false;
        }
        uint uGroupID= Parse(Request["dwID"]);
        string[] arrayGroup = szGroup.Split(',' );
        for (int i = 0; i < arrayGroup.Length; i++)
        {
            uint uAccNo=Parse(arrayGroup[i]);
            if (!IsExistGroupMember(vtGroupMember, uAccNo))
            {
                AddGroupMember(uGroupID, uAccNo, (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL);
            }
        }
        IsExistGroupMember2(arrayGroup, vtGroupMember, uGroupID);
        return true;
    }
    bool IsExistGroupMember(GROUPMEMDETAIL[] vtGroupMember,uint uAccno)
    {
        bool bRes = false;
        for (int i = 0; i < vtGroupMember.Length; i++)
        {
            if ((uint)vtGroupMember[i].dwAccNo == uAccno)
            {
                return true;
            }
        }
        return bRes;
    }
    void IsExistGroupMember2(string[] szAccnNoList, GROUPMEMDETAIL[] vtGroupMember,uint uGroupID)
    {
        bool bRes = false;
        for (int i = 0; i < vtGroupMember.Length; i++)
        {
            uint uTemp =(uint)vtGroupMember[i].dwAccNo;
            bool bExist = false;
            for (int j = 0; j < szAccnNoList.Length; j++)
            {
                uint uTemp2=Parse(szAccnNoList[j]);
               
                if (uTemp == uTemp2)
                {
                    bExist = true;
                    break;
                }
            }
            if (bExist == false)
            {
                DelGroupMember(uGroupID, uTemp, (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL);
            }

        }
      
    }
}
