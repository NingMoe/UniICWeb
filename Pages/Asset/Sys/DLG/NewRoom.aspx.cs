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
    protected string m_szRoomMode = "";
    protected string m_szLab = "";
    protected string m_szOpenRule = "";
    protected string m_szLabKind = "";
    protected string m_szLabFromCode = "";
    protected string m_dwDecam = "";
    protected string m_szLabLevelCode = "";
	protected void Page_Load(object sender, EventArgs e)
	{
        string szOpName = "新建";
        if (Request["op"] == "set")
        {
            bSet=true;
            szOpName = "修改";
        }
        UNIROOM newRoom;
        CODINGTABLE[] vtCodeTable= getCodeTableByType((uint)CODINGTABLE.DWCODETYPE.CODE_LABKIND);
        if (vtCodeTable != null && vtCodeTable.Length > 0)
        {
            for (int i = 0; i < vtCodeTable.Length; i++)
            {
                m_szLabKind += GetInputItemHtml(CONSTHTML.option, "", vtCodeTable[i].szCodeName, vtCodeTable[i].szCodeSN.ToString());
            }
        }
        vtCodeTable = getCodeTableByType((uint)CODINGTABLE.DWCODETYPE.CODE_LABFROM);
        if (vtCodeTable != null && vtCodeTable.Length > 0)
        {
            for (int i = 0; i < vtCodeTable.Length; i++)
            {
                m_szLabFromCode += GetInputItemHtml(CONSTHTML.option, "", vtCodeTable[i].szCodeName, vtCodeTable[i].szCodeSN.ToString());
            }
        }
        vtCodeTable = getCodeTableByType((uint)CODINGTABLE.DWCODETYPE.CODE_ACADEMICSUBJECT);
        if (vtCodeTable != null && vtCodeTable.Length > 0)
        {
            for (int i = 0; i < vtCodeTable.Length; i++)
            {
                m_dwDecam += GetInputItemHtml(CONSTHTML.option, "", vtCodeTable[i].szCodeName, vtCodeTable[i].szCodeSN.ToString());
            }
        }
         vtCodeTable = getCodeTableByType((uint)CODINGTABLE.DWCODETYPE.CODE_LABLEVEL);
        if (vtCodeTable != null && vtCodeTable.Length > 0)
        {
            for (int i = 0; i < vtCodeTable.Length; i++)
            {
                m_szLabLevelCode += GetInputItemHtml(CONSTHTML.option, "", vtCodeTable[i].szCodeName, vtCodeTable[i].szCodeSN.ToString());
            }
        }

        if (IsPostBack)
        {
            GetHTTPObj(out newRoom);
            UNILAB setLab = new UNILAB();
            GetHTTPObj(out setLab);
            string szManMode = Request["dwManMode"];
            newRoom.dwManMode = CharListToUint(szManMode);
            if (bSet == true)
            {
                UNIGROUP newGroup = new UNIGROUP();
                if (!NewGroup(newRoom.szRoomName + "管理员组", (uint)UNIGROUP.DWKIND.GROUPKIND_MAN, out newGroup))
                {
                    MessageBox(m_Request.szErrMessage, "新建实验室失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                    DelGroup(newGroup.dwGroupID);
                    return;
                }
                newRoom.dwManGroupID = newGroup.dwGroupID;
                setLab.dwManGroupID = newGroup.dwGroupID;
            }
            CAMPUSREQ campGet = new CAMPUSREQ();

            setLab.dwDeptID = newRoom.dwDeptID;
            setLab.szDeptName = newRoom.szDeptName;
            setLab.szLabName = newRoom.szRoomName;
            setLab.szLabSN = newRoom.szRoomNo;
            setLab.dwLabClass = newRoom.dwInClassKind;
            UNICAMPUS[] vtCampres;
            if (!bSet)
            {
                if (m_Request.Account.CampusGet(campGet, out vtCampres) == REQUESTCODE.EXECUTE_SUCCESS && vtCampres != null && vtCampres.Length > 0)
                {
                    newRoom.dwCampusID = vtCampres[0].dwCampusID;
                }
            }
            if (m_Request.Device.LabSet(setLab, out setLab) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, szOpName + "实验室失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                if (Request["dwRoomSize"] != null && newRoom.dwRoomSize != null)
                {
                    newRoom.dwRoomSize = (uint)newRoom.dwRoomSize;
                }
                newRoom.dwLabID = setLab.dwLabID;
                if (m_Request.Device.RoomSet(newRoom, out newRoom) != REQUESTCODE.EXECUTE_SUCCESS)
                {
                    MessageBox(m_Request.szErrMessage, szOpName + "实验室失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                    m_Request.Device.LabDel(setLab);
                }
                else
                {
                    UNIDEVICE[] devTempList = GetDevByRoomId(newRoom.dwRoomID);
                    bool bIsAllNew = false;
                    if (devTempList != null && devTempList.Length == 1)
                    {
                        bIsAllNew = true;
                    }
                    string szNewDevic = Request["chkNewDev"];
                    if (szNewDevic != null && szNewDevic == "1")
                    {
                        UNIDEVCLS newDevCls = new UNIDEVCLS();
                        if (bIsAllNew)
                        {
                            newDevCls.dwClassID = devTempList[0].dwClassID;
                        }
                        newDevCls.dwKind = (uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS;
                        newDevCls.szClassName = setLab.szLabName;
                        if (m_Request.Device.DevClsSet(newDevCls, out newDevCls) == REQUESTCODE.EXECUTE_SUCCESS)
                        {
                            UNIDEVKIND newDevKind = new UNIDEVKIND();
                            if (bIsAllNew)
                            {
                                newDevKind.dwKindID = devTempList[0].dwKindID;
                            }
                            newDevKind.dwClassID = newDevCls.dwClassID;
                            newDevKind.szKindName = setLab.szLabName;
                            newDevKind.dwMaxUsers = 1;
                            newDevKind.dwMinUsers = 1;
                            if (m_Request.Device.DevKindSet(newDevKind, out newDevKind) == REQUESTCODE.EXECUTE_SUCCESS)
                            {
                                UNIDEVICE newDevAll = new UNIDEVICE();
                                if (bIsAllNew)
                                {
                                    newDevAll.dwDevID = devTempList[0].dwDevID;
                                }
                                else {
                                    newDevAll.dwDevSN = GetDevSN();
                                    newDevAll.szAssertSN = GetDevSN().ToString();
                                }
                                newDevAll.szDevName = newDevKind.szKindName;
                                newDevAll.dwKindID = newDevKind.dwKindID;
                                newDevAll.dwRoomID = newRoom.dwRoomID;
                               
                                if (m_Request.Device.Set(newDevAll, out newDevAll) == REQUESTCODE.EXECUTE_SUCCESS)
                                {
                                    MessageBox(szOpName + "实验室成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                                    return;
                                }
                                else
                                {
                                    MessageBox(m_Request.szErrMessage, szOpName + "实验室失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                                    m_Request.Device.DevClsDel(newDevCls);
                                    m_Request.Device.LabDel(setLab);
                                    m_Request.Device.RoomDel(newRoom);
                                    m_Request.Device.DevKindDel(newDevKind);
                                }
                            }
                            else
                            {
                                m_Request.Device.DevClsDel(newDevCls);
                                m_Request.Device.LabDel(setLab);
                                m_Request.Device.RoomDel(newRoom);
                                MessageBox(m_Request.szErrMessage, szOpName + "实验室失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);

                            }
                        }

                        return;
                    }
                    MessageBox(szOpName + "实验室成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                    if (Session["LoginResult"] != null)
                    {
                        ADMINLOGINRES admin = (ADMINLOGINRES)Session["LoginResult"];
                        if (!bSet)
                        {
                            AddGroupMember(newRoom.dwManGroupID, admin.AdminInfo.dwAccNo, (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL);
                        }
                    }
                    return;
                }
            }
        }   
        uint uRoomMode = (uint)(UNIROOM.DWMANMODE.ROOMMAN_CAMERA | UNIROOM.DWMANMODE.ROOMMAN_DOORLOCK | UNIROOM.DWMANMODE.ROOMMAN_FREEINOUT);
        m_szRoomMode = GetInputHtml(uRoomMode, CONSTHTML.checkBox, "dwManMode", "Room_dwManMode");
        UNILAB[] vtLab = GetAllLab();
        if (vtLab!=null&&vtLab.Length > 0)
        {
            for (int i = 0; i < vtLab.Length; i++)
            {
                m_szLab += "<option value='" + vtLab[i].dwLabID + "'>" + vtLab[i].szLabName + "</option>";
            }
        }
        DEVOPENRULE[] vtOpenRule=GetAllOpenRule();    
        if (vtOpenRule!=null&&vtOpenRule.Length > 0)
        {
            for (int i = 0; i < vtOpenRule.Length; i++)
            {
                m_szOpenRule+= "<option value='" + vtOpenRule[i].dwRuleSN + "'>" + vtOpenRule[i].szRuleName + "</option>";
            }
        }

        if (Request["op"] == "set")
        {
          
            ROOMREQ vrRoomReq = new ROOMREQ();
            vrRoomReq.dwRoomID = ToUint(Request["roomid"]);
            UNIROOM[] vtRoom;
            if (m_Request.Device.RoomGet(vrRoomReq, out vtRoom) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else
            {
                if (vtRoom.Length == 0)
                {
                    MessageBox("获取失败", "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                }
                else
                {
                    PutJSObj(vtRoom[0]);
                    UNIDEVICE[] devList = GetDevByRoomId(vtRoom[0].dwRoomID);
                    if (devList != null && devList.Length == 1)
                    {
                        PutMemberValue("chkNewDev", "1");
                    }
                    m_Title = "修改【" + vtRoom[0].szRoomName + "】";
                }
            }
        }
        else
        {
            UNISTATION station = new UNISTATION();
            //station.dwStaSN = uMax;
            PutJSObj(station);
            m_Title = szOpName+"实验室";

        }                      
	}
    
}
