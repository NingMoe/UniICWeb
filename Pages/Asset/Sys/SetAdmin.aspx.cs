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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            UNIADMIN newAdmin;
            GetHTTPObj(out newAdmin);
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
        UNILAB[] labList = GetAllLab();
        string szManRoomIDs = ",";
        MANROOMREQ manRoomGetTemp = new MANROOMREQ();
        manRoomGetTemp.dwAccNo = Parse(Request["dwID"]);
        manRoomGetTemp.dwManFlag = 1;
        MANROOM[] vtResManRoomTemp;
        if (m_Request.Admin.GetManRoom(manRoomGetTemp, out vtResManRoomTemp) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vtResManRoomTemp.Length; i++)
            {
                szManRoomIDs += vtResManRoomTemp[i].dwRoomID.ToString() + ",";
            }
        }
        if (labList != null && labList.Length > 0)
        {
            m_szLab = "<table>";
            m_checkLab = "<table>";
            for (int i = 0; i < labList.Length; i++)
            {
                string bISInLab = "";
                UNIROOM[] roomList = GetRoomByLab(labList[i].dwLabID.ToString());
                for (int m = 0; m < roomList.Length; m++)
                {
                    if (szManRoomIDs.IndexOf("," + roomList[m].dwRoomID + ",") > -1)
                    {
                        bISInLab = "(*)";
                        break;
                    }
                }
                if (((i) % nLabRcount) == 0)
                {
                    m_szLab += "<tr>";
                    m_checkLab += "<tr>";
                }
                if (i == 0)
                {
                    m_szLab += "<td>";
                    m_szLab += GetInputItemHtml(CONSTHTML.radioButton, "labList", bISInLab + labList[i].szLabName, labList[i].dwLabID.ToString(), true);
                    m_szLab += "</td>";
                }
                else
                {
                    m_szLab += "<td>";
                    m_szLab += GetInputItemHtml(CONSTHTML.radioButton, "labList", bISInLab + labList[i].szLabName, labList[i].dwLabID.ToString());
                    m_szLab += "</td>";
                }
                
                m_checkLab += "<td>";
                m_checkLab += GetInputItemHtml(CONSTHTML.checkBox, "labCheckList", labList[i].szLabName, labList[i].dwLabID.ToString());
                m_checkLab += "</td>";
                if (i!=0&&((i+1) % nLabRcount) == 0)
                {
                    m_szLab += "</tr>";
                    m_checkLab += "</tr>";
                }

                UNIROOM[] roomListinfo = GetRoomByLab(labList[i].dwLabID.ToString());

                if (roomListinfo != null && roomListinfo.Length > 0)
                {
                    m_szRoom += "<div class='labClass' id=\"divLab" + labList[i].dwLabID.ToString() + "\">管理"+ConfigConst.GCRoomName+":";
                    m_szRoom += "<table>";
                    for (int m = 0; m < roomListinfo.Length; m++)
                    {
                        if (((m) % nRoomRcount) == 0)
                        {
                            m_szRoom += "<tr>";                          
                        }
                        string szCheck = "";
                        m_szRoom += "<td>";
                        m_szRoom += "<label><input class=\"enum\"" + szCheck + "type=\"checkbox\" name=\"" + "roomID" + "\" value=\"" + roomListinfo[m].dwRoomID.ToString() + "\" /> " + roomListinfo[m].szRoomName + "</label>";
                        m_szRoom += "</td>";
                        if (m!= 0 && ((m + 1) % nRoomRcount) == 0)
                        {
                            m_szRoom += "</tr>";                         
                        }


                    }
                    m_szRoom += "</tr>";
                    m_szRoom += "</table>";
                    m_szRoom += "</div>";
                 
                }

            }
            m_szLab += "</tr>";
            m_checkLab += "</tr>";
            m_szLab += "</table>";
            m_checkLab += "</table>";
           
            //uint uManRole = (uint)ADMINLOGINRES.DWMANROLE.MAN_STALEADER + (uint)ADMINLOGINRES.DWMANROLE.MAN_DEVCHARGE + (uint)ADMINLOGINRES.DWMANROLE.MAN_ATTENDANT;
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
                            for (int i = 0; i < vtResManRoom.Length; i++)
                            {
                                szRoomGroupID += vtResManRoom[i].dwRoomID.ToString() + ",";
                                szRoomGroupName += vtResManRoom[i].szRoomName.ToString() + ",";
                            }
                            ViewState["szRoomID"] = szRoomGroupID;
                        }
                        string vwLabID="";
                        for (int k = 0; k < labList.Length; k++)
                        {
                            if (labList[k].dwManGroupID != null && (IsInGroupPersonMember(labList[k].dwManGroupID, vrAdminReq.dwAccNo, (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL)))
                            {
                                vwLabID+=labList[k].dwLabID.ToString()+",";
                            }
                        }
                         ViewState["szLabID"] = vwLabID;
                        hidenManrole.Value = vrAdminRes[0].dwManRole.ToString();
                        PutJSObj(vrAdminRes[0]);
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
        if (ViewState["szLabID"] != null && ViewState["szLabID"].ToString() != "")
        {
            PutMemberValue("labCheckList", ViewState["szLabID"].ToString());
        }
    }

    bool SetRoomGroupFromClient(uint? dwAccNo)
    {
        if (IsNullOrZero(dwAccNo))
        {
            return false;
        }
        string szManRole = Request["dwManRole"];
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
                    if(GetLabByID((uint?)Parse(arrayLab[i]),out lab))
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
        else if (szManRole == "1049105" || szManRole == "2049")//房间管理员
        {
            string szGroup = Request["roomID"];
            if (string.IsNullOrEmpty(szGroup))
            {
                szGroup = "";
            }
            if (szGroup == "" || szGroup == ",")
            {
                //return true;
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
