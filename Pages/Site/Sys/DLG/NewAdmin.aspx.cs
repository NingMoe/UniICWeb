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
    protected string m_adminKind = "";
    protected string m_adminLevle= "";
	protected void Page_Load(object sender, EventArgs e)
	{       
        if (IsPostBack)
        {
            UNIADMIN newAdmin;
            GetHTTPObj(out newAdmin);
            uint uManroleTemp = 0;
            uint uRoleArea = 0;
            uint ucheck = 0;
            uint  uProperty= CharListToUint(Request["dwProperty"]);
            if ((uProperty & 536870912) > 0)
            {
                uManroleTemp=(uint)ADMINLOGINRES.DWMANROLE.MANROLE_LEADER;//领导
                uRoleArea = (uint)ADMINLOGINRES.DWMANROLE.MANSCOPE_STATION;
                //uProperty = uProperty - 536870912;
            }
            else{
                uManroleTemp=(uint)ADMINLOGINRES.DWMANROLE.MANROLE_OPERATOR;
                uRoleArea=(uint)ADMINLOGINRES.DWMANROLE.MANSCOPE_ROOM;
            }
            if ((uProperty & 1073741824) > 0)
            {
                //uProperty = uProperty - 1073741824;
            }
            else {
                ucheck = (uint)ADMINLOGINRES.DWMANROLE.MANIDENT_EANDA;
            }
            newAdmin.dwProperty = uProperty;
            if(!((newAdmin.dwProperty & (uint)UNIADMIN.DWPROPERTY.ADMINPROP_SYS) > 0))
            {
                newAdmin.dwProperty = newAdmin.dwProperty | (uint)UNIADMIN.DWPROPERTY.ADMINPROP_SYS;
            }
            newAdmin.dwManRole = uRoleArea + uManroleTemp + ucheck + (uint)ADMINLOGINRES.DWMANROLE.MANIDENT_ADMIN;
            if (m_Request.Admin.Set(newAdmin, out newAdmin) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "新建失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                return;
            }
            else
            {
                MessageBox("新建成功", "新建成功", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
               // SetRoomGroupFromClient(newAdmin.dwAccNo);
                UNIACCOUNT setAcount;
                if (GetAccByAccno(newAdmin.dwAccNo.ToString(), out setAcount))
                {
                    setAcount.szHandPhone = newAdmin.szHandPhone;
                    m_Request.Account.Set(setAcount,out setAcount);
                }
                return;
            }

        }
        UNILAB[] labList = GetAllLab();
        if (labList != null && labList.Length > 0)
        {
            for (int i = 0; i < labList.Length; i++)
            {
                if (i == 0)
                {
                    m_szLab += GetInputItemHtml(CONSTHTML.radioButton, "labList", labList[i].szLabName, labList[i].dwLabID.ToString(), true);
                }
                else
                {
                    m_szLab += GetInputItemHtml(CONSTHTML.radioButton, "labList", labList[i].szLabName, labList[i].dwLabID.ToString());
                }
                m_checkLab += GetInputItemHtml(CONSTHTML.checkBox, "labCheckList", labList[i].szLabName, labList[i].dwLabID.ToString());
                UNIROOM[] roomList = GetRoomByLab(labList[i].dwLabID.ToString());
                if (roomList != null && roomList.Length > 0)
                {
                    m_szRoom += "<div class='labClass' id=\"divLab" + labList[i].dwLabID.ToString() + "\">管理" + ConfigConst.GCRoomName + ":";
                    for (int m = 0; m < roomList.Length; m++)
                    {
                        string szCheck = "";

                        m_szRoom += "<label><input class=\"enum\"" + szCheck + "type=\"checkbox\" name=\"" + "roomID" + "\" value=\"" + roomList[m].dwRoomID.ToString() + "\" /> " + roomList[m].szRoomName + "</label>";


                    }
                    m_szRoom += "</div>";
                }

            }
        }

        m_adminKind = GetInputHtmlFromXml(0, CONSTHTML.checkBox, "dwProperty", "Admin_CheckKind", true);
        m_adminLevle = GetInputHtmlFromXml(0, CONSTHTML.option, "", "Admin_Level", true);
       // uint uManRole = (uint)ADMINLOGINRES.DWMANROLE.MAN_STALEADER + (uint)ADMINLOGINRES.DWMANROLE.MAN_DEVCHARGE + (uint)ADMINLOGINRES.DWMANROLE.MAN_ATTENDANT;
        szManRole = GetInputHtmlFromXml(2, CONSTHTML.option, "", "Admin_ManRole2", true);
        if (Request["op"] == "set")
        {
            bSet = true;
            ADMINREQ vrAdminReq = new ADMINREQ();
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
                    PutJSObj(vrAdminRes[0]);
                    m_Title = "修改" + "【" + vrAdminRes[0].szTrueName + "】";
                }
            }
        }
        else
        {   
            m_Title = "新建管理员";
        }
	}
    bool SetRoomGroupFromClient(uint? dwAccNo)
    {
        if (IsNullOrZero(dwAccNo))
        {
            return false;
        }
       
        string szManRole = Request["dwManRole"];
        szManRole = "524801";
        if (szManRole == null)
        {
            return false;
        }
        else if (szManRole == "132097")//站点领导不需要添加
        {
            return true;
        }
        else if (szManRole == "524801")//实验室管理员
        {
            string szLabList = Request["labCheckList"];
            if (string.IsNullOrEmpty(szLabList))
            {
                szLabList = "";
            }
            string[] arrayLab = szLabList.Split(new char[] { ',' });
            for (int i = 0; i < arrayLab.Length; i++)
            {
                if (arrayLab[i] != "")
                {
                    UNILAB lab;
                    if (GetLabByID((uint?)Parse(arrayLab[i]), out lab))
                    {
                        if (!IsInGroupMember(lab.dwManGroupID, dwAccNo, (uint)UNIGROUP.DWKIND.GROUPKIND_MAN))
                        {
                            AddGroupMember(lab.dwManGroupID, dwAccNo, (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL);
                        }
                    }
                }
            }
            UNILAB[] allLab = GetAllLab();
            if (allLab != null)
            {
                for (int i = 0; i < allLab.Length; i++)
                {
                    bool bIsIn = false;
                    uint uLabid = (uint)allLab[i].dwLabID;
                    for (int k = 0; k < arrayLab.Length; k++)
                    {
                        if (Parse(arrayLab[k]) == uLabid)
                        {
                            bIsIn = true;
                            break;
                        }
                    }
                    if (bIsIn == false)
                    {
                        DelGroupMember(allLab[i].dwManGroupID, dwAccNo, (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL);
                    }
                }
            }
            return true;
        }
        else if (szManRole == "1049089")//房间管理员
        {
            string szGroup = Request["roomID"];
            if (string.IsNullOrEmpty(szGroup))
            {
                szGroup = "";
            }
            if (szGroup == "" || szGroup == ",")
            {
                return true;
            }
            uint[] arrayGroupID = null;
            string szRoomGroup = Request["roomID"];
            if (szRoomGroup == null)
            {
                szRoomGroup = "";
            }
            string[] arrayGroupName = szRoomGroup.Split(new char[] { ',' });
            string[] arrayGroup = szGroup.Split(new char[] { ',' });
            arrayGroupID = new uint[arrayGroup.Length];
            for (int i = 0; i < arrayGroup.Length; i++)
            {
                uint nClsID = 0;
                uint.TryParse(arrayGroup[i], out nClsID);
                arrayGroupID[i] = nClsID;
            }

            MANROOMREQ manRoomGet = new MANROOMREQ();
            manRoomGet.dwAccNo = dwAccNo;
            manRoomGet.dwManFlag = 1;
            MANROOM[] vtResManRoom;
            m_Request.Admin.GetManRoom(manRoomGet, out vtResManRoom);

            bool bError = false;

            //删除房间管理组成员
            if (vtResManRoom != null && vtResManRoom.Length > 0)
            {
                for (int i = 0; i < vtResManRoom.Length; i++)
                {
                    int nFind = -1;
                    for (int j = 0; j < arrayGroupID.Length; j++)
                    {
                        if (vtResManRoom[i].dwRoomID == arrayGroupID[j])
                        {
                            nFind = j;
                            break;
                        }
                    }
                    if (nFind >= 0)
                    {
                        arrayGroupID[nFind] = 0;
                    }
                    else
                    {
                        GROUPMEMBER vrGrpMember = new GROUPMEMBER();
                        vrGrpMember.dwGroupID = vtResManRoom[i].dwManGroupID;
                        vrGrpMember.dwMemberID = dwAccNo;
                        vrGrpMember.dwKind = (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL;
                        if (m_Request.Group.DelGroupMember(vrGrpMember) != REQUESTCODE.EXECUTE_SUCCESS)
                        {
                            bError = true;
                            break;
                        }
                    }
                }
            }
            //添加管理组成员
            for (int i = 0; i < arrayGroupID.Length; i++)
            {
                if (arrayGroupID[i] == 0)
                {
                    continue;
                }
                ROOMREQ vrGetRoomGrpReq = new ROOMREQ();
                UNIROOM[] vrRoomGroupRet;
                vrGetRoomGrpReq.dwRoomID = arrayGroupID[i];
                if (m_Request.Device.RoomGet(vrGetRoomGrpReq, out vrRoomGroupRet) == REQUESTCODE.EXECUTE_SUCCESS && vrRoomGroupRet != null && vrRoomGroupRet.Length != 0)
                {
                    if (IsNullOrZero(vrRoomGroupRet[0].dwManGroupID))
                    {
                        //创建管理组
                        UNIGROUP vrNewGroup = new UNIGROUP();
                        vrNewGroup.szName = vrRoomGroupRet[0].szRoomName + "管理组";
                        m_Request.Group.SetGroup(vrNewGroup, out vrNewGroup);
                        if (IsNullOrZero(vrNewGroup.dwGroupID))
                        {
                            bError = true;
                            break;
                        }
                        vrRoomGroupRet[0].dwManGroupID = vrNewGroup.dwGroupID;
                        if (m_Request.Device.RoomSet(vrRoomGroupRet[0], out vrRoomGroupRet[0]) != REQUESTCODE.EXECUTE_SUCCESS)
                        {
                            bError = true;
                            break;
                        }
                    }

                    GROUPMEMBER vrGrpMember = new GROUPMEMBER();
                    vrGrpMember.dwGroupID = vrRoomGroupRet[0].dwManGroupID;
                    vrGrpMember.dwMemberID = dwAccNo;
                    vrGrpMember.dwKind = (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL;
                    if (m_Request.Group.SetGroupMember(vrGrpMember) != REQUESTCODE.EXECUTE_SUCCESS)
                    {
                        bError = true;
                        break;
                    }
                }
            }
            return !bError;
        }
        return true;
    }
}