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
    protected string m_szLab = "";
    protected string m_checkLab = "";
    private int nLabRcount = 5;
    private int nRoomRcount = 8;

    protected string m_adminKind = "";
    protected string m_adminLevle = "";

    protected string szCamp = "";
    protected string szBuilding = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        UNICAMPUS[] vtCamp = GetAllCampus();
        if (vtCamp != null && vtCamp.Length > 0)
        {
            for (int i = 0; i < vtCamp.Length; i++)
            {
                szCamp += GetInputItemHtml(CONSTHTML.option, "", vtCamp[i].szCampusName, vtCamp[i].dwCampusID.ToString());
            }
        }
        UNIBUILDING[] vtBuilding = getAllBuilding();
        szBuilding += GetInputItemHtml(CONSTHTML.option, "", "全部", "");
        for (int i = 0; i < vtBuilding.Length; i++)
        {
           
            if (vtBuilding[i].dwCampusID.ToString() == vtCamp[0].dwCampusID.ToString())
            {
                szBuilding += GetInputItemHtml(CONSTHTML.option, "", vtBuilding[i].szBuildingName.ToString(), vtBuilding[i].dwBuildingID.ToString());
            }
        }
        if (IsPostBack)
        {
            UNIADMIN newAdmin;
            GetHTTPObj(out newAdmin);
            newAdmin.dwProperty = CharListToUint(Request["dwProperty"]);
            if (!((newAdmin.dwProperty & (uint)UNIADMIN.DWPROPERTY.ADMINPROP_SYS) > 0))
            {
                newAdmin.dwProperty = newAdmin.dwProperty | (uint)UNIADMIN.DWPROPERTY.ADMINPROP_SYS;
            }
            newAdmin.dwManRole = (uint)ADMINLOGINRES.DWMANROLE.MANSCOPE_ROOM + (uint)ADMINLOGINRES.DWMANROLE.MANROLE_OPERATOR + (uint)ADMINLOGINRES.DWMANROLE.MANIDENT_EANDA + (uint)ADMINLOGINRES.DWMANROLE.MANIDENT_ADMIN;
            if (m_Request.Admin.Set(newAdmin, out newAdmin) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "修改失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                if (SetRoomGroupFromClient(newAdmin.dwAccNo))
                {
                    MessageBox("修改成功", "修改成功", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                    UNIACCOUNT setAcount;
                    if (GetAccByAccno(newAdmin.dwAccNo.ToString(), out setAcount))
                    {
                        setAcount.szHandPhone = newAdmin.szHandPhone;
                        m_Request.Account.Set(setAcount, out setAcount);
                    }
                }
                else
                {
                    MessageBox("基本信息修改成功，设置管理" + ConfigConst.GCRoomName + "失败", "修改失败", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                }
                return;
            }

        }


        m_adminKind = GetInputHtmlFromXml(0, CONSTHTML.checkBox, "dwProperty", "Admin_CheckKind", true);
        m_adminLevle = GetInputHtmlFromXml(0, CONSTHTML.option, "", "Admin_Level", true);
        UNILAB[] labList = GetAllLab();
        if (labList != null && labList.Length > 0)
        {
              szManRole = GetInputHtmlFromXml(0, CONSTHTML.option, "", "Admin_ManRole2", true);
            if (Request["op"] == "set")
            {
                bSet = true;

                ADMINREQ vrAdminReq = new ADMINREQ();
                vrAdminReq.dwAccNo = Parse(Request["dwID"]);
                UNIADMIN[] vrAdminRes;
                if (m_Request.Admin.Get(vrAdminReq, out vrAdminRes) != REQUESTCODE.EXECUTE_SUCCESS)
                {
                    MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                }
                else
                {
                    if (vrAdminRes.Length == 0)
                    {
                        MessageBox("获取失败", "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                    }
                    else
                    {
                        ViewState["szLogonName"] = vrAdminRes[0].szLogonName.ToString();

                        m_Title = "修改" + "【" + vrAdminRes[0].szTrueName + "】";

                        string szRoomGroupID = "";
                        string szRoomGroupName = "";

                        MANROOMREQ manRoomGet = new MANROOMREQ();
                        manRoomGet.dwAccNo = vrAdminReq.dwAccNo;
                        manRoomGet.dwManFlag = 1;
                        MANROOM[] vtResManRoom;
                        if (m_Request.Admin.GetManRoom(manRoomGet, out vtResManRoom) == REQUESTCODE.EXECUTE_SUCCESS)
                        {
                            string szManGroupID = "";
                            for (int i = 0; i < vtResManRoom.Length; i++)
                            {
                                szManGroupID +=  ","+vtResManRoom[i].dwManGroupID.ToString() + ",";
                                szRoomGroupID += vtResManRoom[i].dwRoomID.ToString() + ",";
                                szRoomGroupName += vtResManRoom[i].szRoomName.ToString() + ",";
                            }
                            ViewState["szRoomID"] = szRoomGroupID;
                            ViewState["szManGroupID"] = szManGroupID;
                        }
                        string vwLabID="";
                        /*
                        for (int k = 0; k < labList.Length; k++)
                        {
                            if (labList[k].dwManGroupID != null && (IsInGroupPersonMember(labList[k].dwManGroupID, vrAdminReq.dwAccNo, (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL)))
                            {
                                vwLabID+=labList[k].dwLabID.ToString()+",";
                            }
                        }
                       */
                        ViewState["szLabID"] = vwLabID;
                        hidenManrole.Value = vrAdminRes[0].dwManRole.ToString();
                        PutJSObj(vrAdminRes[0]);
                        ViewState["dwProperty"] = vrAdminRes[0].dwProperty.ToString();
                    }
                }
            }
            else
            {
                uint? uMax = 0;
                uint uID = PRStation.DOORCTRLSRV_BASE | PRDoorCtrlSrv.MSREQ_DCS_SET;

                if (GetMaxValue(ref uMax, uID, "dwSN"))
                {
                    UNIADMIN setValue = new UNIADMIN();
                    ViewState["szLogonName"] = setValue.szLogonName.ToString();
                    PutJSObj(setValue);
                }
                m_Title = "新建管理员";
            }
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreLoad(e);        
        if(ViewState["szLogonName"]!=null&&ViewState["szLogonName"].ToString()!="")
        {
            PutMemberValue("szLogonNamePut", ViewState["szLogonName"].ToString());
        }
        if (ViewState["szRoomID"] != null && ViewState["szRoomID"].ToString() != "")
        {
            PutMemberValue("roomID", ViewState["szRoomID"].ToString());
            PutMemberValue("hiddenRoomID", ViewState["szRoomID"].ToString());
          
        }
        if(ViewState["szManGroupID"]!=null&&ViewState["szManGroupID"].ToString()!="")
        {
            PutMemberValue("hiddenRoomIDTemp", ViewState["szManGroupID"].ToString());
            PutMemberValue("hiddenRoomID", ViewState["szManGroupID"].ToString());
        }
        if (ViewState["szLabID"] != null && ViewState["szLabID"].ToString() != "")
        {
            PutMemberValue("labCheckList", ViewState["szLabID"].ToString());
        }
        if (ViewState["dwProperty"] != null && ViewState["dwProperty"].ToString() != "")
        {
            PutMemberValue("dwProperty", ViewState["dwProperty"].ToString());
        }
    }

    bool SetRoomGroupFromClient(uint? dwAccNo)
    {
        if (IsNullOrZero(dwAccNo))
        {
            return false;
        }
        string szManRole = Request["dwManRole"];
        szManRole = "1049089";
        if (szManRole == null)
        {
            return false;
        }
        else if (szManRole == "1049089")//房间管理员
        {
            string szOldMangroup = Request["hiddenRoomID"];
            string szNewMangroup = Request["hiddenRoomIDTemp"];
            string[] szOldMangroupList = szOldMangroup.Split(',');
            string[] szNewMangroupList = szNewMangroup.Split(',');
            for (int i = 0; i < szNewMangroupList.Length; i++)
            {
                string szNewTemp = szNewMangroupList[i];
                if (szNewTemp == null || szNewTemp == "")
                {
                    continue;
                }
                bool bIsAdd = true;
                for (int j = 0; j < szOldMangroupList.Length; j++)
                {
                    if (szOldMangroupList[j] == szNewTemp)
                    {
                        bIsAdd = false;
                        break;
                    }
                }
                if (bIsAdd)
                {
                    AddGroupMember(Parse(szNewTemp), dwAccNo,(uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL);
                }

            }

            for (int i = 0; i < szOldMangroupList.Length; i++)
            {
                string szNewTemp = szOldMangroupList[i];
                if (szNewTemp == null || szNewTemp == "")
                {
                    continue;
                }
                bool bIsDel = true;
                for (int j = 0; j < szNewMangroupList.Length; j++)
                {
                    if (szNewMangroupList[j] == szNewTemp)
                    {
                        bIsDel = false;
                        break;
                    }
                }
                if (bIsDel)
                {
                    DelGroupMember(Parse(szNewTemp), dwAccNo, (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL);
                }

            }


        }
        return true;
    }
}
