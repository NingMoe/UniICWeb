using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Collections;
using System.Globalization;
using System.Reflection;
using UniWebLib;
using UniLibrary;
using System.Security.Cryptography;
using System.Text;
/// <summary>
///  的摘要说明
/// </summary>
public partial class UniPage : UniWebLib.UniPage
{
    public enum tipInfo
    {
        accnoTip = 1, devTip = 2, roomTip = 3
    }
    public uint uGCIdent = 0;
    public string GetAlinkAccno(tipInfo tipType, string szValue,string accno)
    {
        string szClass="";
        if(tipType==tipInfo.accnoTip)
        {
            szClass="accnoTipClass";
        }
        else if(tipType==tipInfo.devTip)
        {
            szClass="devTipClass";
        }
        else if(tipType==tipInfo.roomTip)
        {
            szClass="roomTipClass";
        }
        string szRes = "";
        szRes = "<a class=" + szClass + " logonname='0' accno='"+accno+"'>"+szValue+ "</a>";
        return szRes;
    }
    public string GetAlinkLogonName(tipInfo tipType, string szValue, string logonName)
    {
        string szClass = "";
        if (tipType == tipInfo.accnoTip)
        {
            szClass = "accnoTipClass";
        }
        else if (tipType == tipInfo.devTip)
        {
            szClass = "devTipClass";
        }
        else if (tipType == tipInfo.roomTip)
        {
            szClass = "roomTipClass";
        }
        string szRes = "";
        szRes = "<a class=" + szClass + " accno='0' logonname='"+logonName+"'>" + szValue + "</a>";
        return szRes;
    }
    public UNILAB[] GetAllLab()
    {
        LABREQ vrParameter = new LABREQ();
        UNILAB[] vrResult;
        vrParameter.szReqExtInfo.szOrderKey = "szLabName";
        vrParameter.szReqExtInfo.szOrderMode = "asc";
        m_Request.Device.LabGet(vrParameter, out vrResult);
        return vrResult;      
    }
    public UNILAB[] GetLabByClass(uint uClsss)
    {
        LABREQ vrParameter = new LABREQ();
        vrParameter.dwLabClass = uClsss;
        UNILAB[] vrResult;

        m_Request.Device.LabGet(vrParameter, out vrResult);
        return vrResult;
    }
    public bool GetLabByID(uint? uLabID, out UNILAB lab)
    {
        LABREQ vrParameter = new LABREQ();
        vrParameter.dwLabID = uLabID;
        UNILAB[] vrResult;
        lab = new UNILAB();
        if (m_Request.Device.LabGet(vrParameter, out vrResult)==REQUESTCODE.EXECUTE_SUCCESS && vrResult != null && vrResult.Length > 0)
        {
            lab = vrResult[0];
            return true;
        }
        return false;
    }
    public UNICAMPUS[] GetAllCampus()
    {
        CAMPUSREQ vrParameter = new CAMPUSREQ();
        UNICAMPUS[] vrResult;

        m_Request.Account.CampusGet(vrParameter, out vrResult);
        return vrResult;
    }
    public UNIBUILDING[] getAllBuilding()
    {
        BUILDINGREQ vrParameter = new BUILDINGREQ();
        UNIBUILDING[] vrResult;

        m_Request.Device.BuildingGet(vrParameter, out vrResult);
        return vrResult;
    }
    public UNILAB[] GetAllLabByKind(uint uKind)
    {
        LABREQ vrParameter = new LABREQ();
        UNILAB[] vrResult;
      
        vrParameter.dwLabClass = uKind;
        m_Request.Device.LabGet(vrParameter, out vrResult);
        return vrResult;
    }
    public DEVOPENRULE[] GetAllOpenRule()
    {
        DEVOPENRULEREQ vrParameter = new DEVOPENRULEREQ();
        DEVOPENRULE[] vrResult;
      
        m_Request.Device.DevOpenRuleGet(vrParameter, out vrResult);
        return vrResult;
    }
    public UNICHANNELGATE[] GetAllChannelGate()
    {
        CHANNELGATEREQ vrGet = new CHANNELGATEREQ();
        UNICHANNELGATE[] vtRes;
        m_Request.Device.ChannelGateGet(vrGet, out vtRes);
        return vtRes;
    }
    public UNIROOM[] GetAllRoom()
    {
        ROOMREQ reqRoom = new ROOMREQ();
        reqRoom.szReqExtInfo.szOrderKey = "szRoomName";
        reqRoom.szReqExtInfo.szOrderMode = "asc";
        UNIROOM[] vtRoom;
        m_Request.Device.RoomGet(reqRoom, out vtRoom);
        return vtRoom;
    }
    public FULLROOM[] GetAllFullRoom()
    {
        FULLROOMREQ reqRoom = new FULLROOMREQ();
        FULLROOM[] vtRoom;
        m_Request.Device.FullRoomGet(reqRoom, out vtRoom);
        return vtRoom;
    }
    public UNIROOM[] GetRoomByLab(string szLabID)
    {
        ROOMREQ reqRoom = new ROOMREQ();
        reqRoom.dwLabID = Parse(szLabID);
        UNIROOM[] vtRoom;
        m_Request.Device.RoomGet(reqRoom, out vtRoom);
        return vtRoom;
    }
    public UNIROOM[] GetRoomByNO(string szRoomNO,uint? uLabID)
    {
        ROOMREQ reqRoom = new ROOMREQ();
        reqRoom.szRoomNo = szRoomNO;
        reqRoom.dwLabID = uLabID;
        UNIROOM[] vtRoom;
        m_Request.Device.RoomGet(reqRoom, out vtRoom);
        return vtRoom;
    }
    public string GetTeachingTime(uint uTeachingTime)
    {
        uint uResvWeeks = uTeachingTime / 100000;
        uint uResvWeek = (uTeachingTime % 100000) / 10000;
        uint uResvBeginSec = (uTeachingTime % 10000) / 100;
        uint uResvEndSec = (uTeachingTime % 100);
        string szTeachTime = "第" + uResvWeeks + "周-" + szWeekDayList[uResvWeek] + "-第" + uResvBeginSec.ToString() + "节到第" + uResvEndSec + "节";
        return szTeachTime;
    }
    public void GetTeachingTimeDetail(uint uTeachingTime, out uint uResvWeeks, out uint uResvWeek, out uint uResvBeginSec, out uint uResvEndSec)
    {
         uResvWeeks = uTeachingTime / 100000;//周次
         uResvWeek = (uTeachingTime % 100000) / 10000;//星期
         uResvBeginSec = (uTeachingTime % 10000) / 100;//开始日期
         uResvEndSec = (uTeachingTime % 100);//结束日期
    }
    public UNIROOM[] GetRoomByClassKind(uint uKind)
    {
        ROOMREQ reqRoom = new ROOMREQ();
        reqRoom.dwInClassKind = uKind;        
        UNIROOM[] vtRoom;
        m_Request.Device.RoomGet(reqRoom, out vtRoom);
        return vtRoom;
    }
    public UNITESTITEM[] GetTestItemByID(uint uTestItemID)
    {
        TESTITEMREQ vrTestItemReq=new TESTITEMREQ();
        vrTestItemReq.dwGetType=(uint)TESTITEMREQ.DWGETTYPE.TESTITEMGET_BYID;
        vrTestItemReq.szGetKey=uTestItemID.ToString();
        UNITESTITEM[] vtTestItemRes;
        if(m_Request.Reserve.GetTestItem(vrTestItemReq,out vtTestItemRes)==REQUESTCODE.EXECUTE_SUCCESS&&vtTestItemRes!=null&&vtTestItemRes.Length>0)
        {
            return vtTestItemRes;
        }
        return null;
    }
    public UNITESTPLAN[] GetTestPlanByID(uint uTestPlan)
    {
        TESTPLANREQ vrGet = new TESTPLANREQ();
        vrGet.dwGetType = (uint)TESTPLANREQ.DWGETTYPE.TESTPLANGET_BYID;
        vrGet.szGetKey = uTestPlan.ToString();
        UNITESTPLAN[] vtRes;
        if (m_Request.Reserve.GetTestPlan(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            return vtRes;
        }
        return null;
    }
    public bool GetCourseByID(uint? uCourseID, out UNICOURSE setValue)
    {
        setValue = new UNICOURSE();
        COURSEREQ reqRoom = new COURSEREQ();
        reqRoom.dwCourseID = uCourseID;
        UNICOURSE[] vtRes;
        REQUESTCODE uResponse = m_Request.Reserve.GetCourse(reqRoom, out vtRes);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            setValue = vtRes[0];
            return true;
        }
        return false;

    }
    public bool GetRoomID(string uRoomID,out UNIROOM resRoom)
    {
        resRoom = new UNIROOM();
        ROOMREQ reqRoom = new ROOMREQ();
        reqRoom.dwRoomID =ToUint(uRoomID);
        UNIROOM[] vtRoom;
       REQUESTCODE uResponse=m_Request.Device.RoomGet(reqRoom, out vtRoom);
       if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRoom != null && vtRoom.Length > 0)
       {
           resRoom = vtRoom[0];
           return true;
       }
       return false;

    }
    public bool GetRoomByName(string roomName, out UNIROOM resRoom)
    {
        resRoom = new UNIROOM();
        ROOMREQ reqRoom = new ROOMREQ();
        reqRoom.szRoomName = roomName;
        UNIROOM[] vtRoom;
        REQUESTCODE uResponse = m_Request.Device.RoomGet(reqRoom, out vtRoom);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRoom != null && vtRoom.Length > 0)
        {
            resRoom = vtRoom[0];
            return true;
        }
        return false;

    }
    public bool GetResvByID(string szResvID, out UNIRESERVE Resv)
    {
        Resv = new UNIRESERVE();
        RESVREQ req = new RESVREQ();
        //req.dwGetType = (uint)RESVREQ.DWGETTYPE.RESVGET_BYID;
        req.dwStatFlag = (uint)RESVREQ.DWSTATFLAG.STATFLAG_INUSE + (uint)RESVREQ.DWSTATFLAG.STATFLAG_OVER;
        req.dwResvID =Parse(szResvID);
        UNIRESERVE[] vtRes;
        REQUESTCODE uResponse = m_Request.Reserve.Get(req, out vtRes);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            Resv = vtRes[0];
            return true;
        }
        return false;

    } 
    public string GetGroupMemberName(uint uGroupID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        string szRes = "";
        GROUPMEMDETAILREQ vrGet = new GROUPMEMDETAILREQ();
        vrGet.dwReqProp = (uint)GROUPMEMDETAILREQ.DWREQPROP.GROUPMEMDETAILREQ_NEEDDEL;
        vrGet.dwGroupID = (uGroupID);
        GROUPMEMDETAIL[] vtRes;
        uResponse = m_Request.Group.GetGroupMemDetail(vrGet, out vtRes);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null)
        {
            for (int i = 0; i < vtRes.Length; i++)
            {
                szRes += vtRes[i].szTrueName.ToString() + ",";
            }
        }
        return szRes;
    }
    public string GetGroupMemberName(uint uGroupID,bool inUse)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        string szRes = "";
        GROUPREQ vrGet = new GROUPREQ();
        vrGet.dwReqProp = (uint)GROUPMEMDETAILREQ.DWREQPROP.GROUPMEMDETAILREQ_NEEDDEL;
        vrGet.dwGroupID = (uGroupID);
        UNIGROUP[] vtRes;
        uResponse = m_Request.Group.GetGroup(vrGet, out vtRes);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null&& vtRes.Length>0)
        {
           GROUPMEMBER[] vtMember=vtRes[0].szMembers;
            if (vtMember != null && vtMember.Length > 0)
            {
                for (int i = 0; i < vtMember.Length; i++)
                {
                    szRes += vtMember[i].szName.ToString() + ",";
                }
            }
        }
        return szRes;
    }
    public bool GetDevKindByID(string szID,out UNIDEVKIND devKind)
    {
        devKind = new UNIDEVKIND();
        DEVKINDREQ vrDevKindGet = new DEVKINDREQ();
        vrDevKindGet.dwKindID = ToUint(szID);
        UNIDEVKIND[] vtDevKind;
        REQUESTCODE uRes = m_Request.Device.DevKindGet(vrDevKindGet, out vtDevKind);
        if (uRes == REQUESTCODE.EXECUTE_SUCCESS && vtDevKind != null && vtDevKind.Length > 0)
        {
            devKind=vtDevKind[0];
            return true;
        }
        return false;
    }
    public UNIDEVKIND[] GetAllDevKind()
    {        
        DEVKINDREQ vrDevKindGet = new DEVKINDREQ();
        vrDevKindGet.szReqExtInfo.szOrderKey = "szKindName";
        vrDevKindGet.szReqExtInfo.szOrderMode = "asc";
        UNIDEVKIND[] vtDevKind;
        REQUESTCODE uRes = m_Request.Device.DevKindGet(vrDevKindGet, out vtDevKind);
        if (uRes == REQUESTCODE.EXECUTE_SUCCESS && vtDevKind != null && vtDevKind.Length > 0)
        {
            return vtDevKind;
        }
        return null;
    }
    public UNIDEVKIND[] GetDevKindByKind(uint uKind)
    {
        DEVKINDREQ vrDevKindGet = new DEVKINDREQ();
        vrDevKindGet.dwClassKind = uKind;
        UNIDEVKIND[] vtDevKind;
        REQUESTCODE uRes = m_Request.Device.DevKindGet(vrDevKindGet, out vtDevKind);
        if (uRes == REQUESTCODE.EXECUTE_SUCCESS && vtDevKind != null && vtDevKind.Length > 0)
        {
            return vtDevKind;
        }
        return null;
    }
    public UNIDEVCLS[] GetAllDevCls()
    {
        DEVCLSREQ vrDeClsGet = new DEVCLSREQ();
        UNIDEVCLS[] vtDevCls;
        REQUESTCODE uRes = m_Request.Device.DevClsGet(vrDeClsGet, out vtDevCls);
        if (uRes == REQUESTCODE.EXECUTE_SUCCESS && vtDevCls != null && vtDevCls.Length > 0)
        {
            return vtDevCls;
        }
        return null;
    }
    public UNIDEVCLS[] GetDevClsByKind(uint uKind)
    {
        DEVCLSREQ vrDeClsGet = new DEVCLSREQ();
        vrDeClsGet.dwKind = uKind;
        UNIDEVCLS[] vtDevCls;
        REQUESTCODE uRes = m_Request.Device.DevClsGet(vrDeClsGet, out vtDevCls);
        if (uRes == REQUESTCODE.EXECUTE_SUCCESS && vtDevCls != null && vtDevCls.Length > 0)
        {
            return vtDevCls;
        }
        return null;
    }
    public bool getDevByID(string szID,out UNIDEVICE setValue)
    {
        setValue = new UNIDEVICE();

        DEVREQ vrDevReq = new DEVREQ();
        vrDevReq.dwDevID = Parse(szID);

        UNIDEVICE[] vtDev;
        REQUESTCODE uRes = m_Request.Device.Get(vrDevReq, out vtDev);
        if (uRes == REQUESTCODE.EXECUTE_SUCCESS && vtDev != null && vtDev.Length > 0)
        {
            setValue= vtDev[0];
            return true;
        }
        return false;
    }
    public bool getOpenRuleByID(string szID, out DEVOPENRULE openRule)
    {
        openRule = new DEVOPENRULE();

        DEVOPENRULEREQ vrGet = new DEVOPENRULEREQ();
        vrGet.dwRuleSN = Parse(szID);

        DEVOPENRULE[] vtRes;
        REQUESTCODE uRes = m_Request.Device.DevOpenRuleGet(vrGet, out vtRes);
        if (uRes == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            openRule = vtRes[0];
            return true;
        }
        return false;
    }
    public UNIDEVCLS[] GetDevCLS(uint uKind)
    {
        DEVCLSREQ vrParameter = new DEVCLSREQ();
        UNIDEVCLS[] vrResult;
        if (uKind != 0)
        {
            vrParameter.dwKind = uKind;
        }        
        m_Request.Device.DevClsGet(vrParameter, out vrResult);
        return vrResult;
    }
    public UNIDEVCLS[] GetDevCLSName(string szName)
    {
        DEVCLSREQ vrParameter = new DEVCLSREQ();
        UNIDEVCLS[] vrResult;
        vrParameter.szClassName = szName;
        m_Request.Device.DevClsGet(vrParameter, out vrResult);
        return vrResult;
    }
    public bool GetDevCLSByID(string szID,out UNIDEVCLS devCls)
    {
        DEVCLSREQ vrParameter = new DEVCLSREQ();
        vrParameter.dwClassID = Parse(szID);
        devCls = new UNIDEVCLS();
        UNIDEVCLS[] vrResult;
        if (m_Request.Device.DevClsGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null && vrResult.Length > 0)
        {
            devCls = vrResult[0];
            return true;
        }
        return false;
    }
    public UNIGROUP[] GetGroupByKind(uint uKind)
    {
        GROUPREQ vrParameter = new GROUPREQ();
        UNIGROUP[] vrResult;
        //vrParameter.dwGetType = (uint)GROUPREQ.DWGETTYPE.GROUPGET_BYKIND;
        vrParameter.dwKind = uKind;       
        m_Request.Group.GetGroup(vrParameter, out vrResult);
        return vrResult;
    }
    public UNIGROUP[] GetGroupByID(uint uID)
    {
        GROUPREQ vrParameter = new GROUPREQ();
        UNIGROUP[] vrResult;
        vrParameter.dwReqProp = (uint)GROUPREQ.DWREQPROP.GROUPREQ_NEEDDEL;
        //vrParameter.dwGetType = (uint)GROUPREQ.DWGETTYPE.GROUPGET_BYID;
        vrParameter.dwGroupID = uID;
        m_Request.Group.GetGroup(vrParameter, out vrResult);
        return vrResult;
    }
    public UNIGROUP[] GetGroupByName(string szName)
    {
        GROUPREQ vrParameter = new GROUPREQ();
        UNIGROUP[] vrResult;
        vrParameter.szName = szName;
        m_Request.Group.GetGroup(vrParameter, out vrResult);
        return vrResult;
    }
    public bool NewGroup(string szGroupName, uint uGroupKind,out UNIGROUP newGroup)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        newGroup = new UNIGROUP();
        newGroup.szName = szGroupName;
        newGroup.dwKind = uGroupKind;
        newGroup.dwMaxUsers = 1000;
        newGroup.dwMinUsers = 0;
        newGroup.dwDeadLine = 20990101;
        uResponse = m_Request.Group.SetGroup(newGroup, out newGroup);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            return true;
        }
        return false;
    }
    public bool NewGroup(string szGroupName, uint uGroupKind, out UNIGROUP newGroup,uint uDeadLine)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        newGroup = new UNIGROUP();
        newGroup.szName = szGroupName;
        newGroup.dwKind = uGroupKind;
        newGroup.dwMaxUsers = 1000;
        newGroup.dwMinUsers = 0;
        newGroup.dwDeadLine = uDeadLine;
        uResponse = m_Request.Group.SetGroup(newGroup, out newGroup);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            return true;
        }
        return false;
    }
    public bool DelGroup(uint? dwID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        UNIGROUP group = new UNIGROUP();
        group.dwGroupID = dwID;
        uResponse = m_Request.Group.DelGroup(group);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            return true;
        }
        return false;

    }
    public string GetGroupMember(GROUPMEMBER[] groupMember)
    {
        string szRes = "";
        if (groupMember == null || groupMember.Length == 0)
        {
            return szRes;
        }
        for (int i = 0; i < groupMember.Length; i++)
        {
            szRes += groupMember[i].szName.ToString()+",";
        }
        return szRes;
    }
    public UNIDEVICE[] GetDevByRoomId(uint? uRoomID)
    {
        DEVREQ vrParameter = new DEVREQ();
        UNIDEVICE[] vrResult;
        vrParameter.szRoomIDs = uRoomID.ToString();
        m_Request.Device.Get(vrParameter, out vrResult);
        return vrResult;
    }
    public UNIDEVICE[] GetDevByKind(uint? uKindID)
    {
        DEVREQ vrParameter = new DEVREQ();
        UNIDEVICE[] vrResult;
        vrParameter.szKindIDs = uKindID.ToString();
        m_Request.Device.Get(vrParameter, out vrResult);
        return vrResult;
    }
    public bool GetAllDev(out UNIDEVICE[] vtRes)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        DEVREQ vrParameter = new DEVREQ();
        uResponse = m_Request.Device.Get(vrParameter, out vtRes);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            return true;
        }
        return false;
    }
    public bool GetResearchTestByID(out RESEARCHTEST reserch,string szID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        reserch = new RESEARCHTEST();
        RESEARCHTESTREQ vrParameter = new RESEARCHTESTREQ();
        RESEARCHTEST[] vtRes;
        vrParameter.dwRTID = Parse(szID);
        uResponse = m_Request.Reserve.GetResearchTest(vrParameter, out vtRes);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            reserch = vtRes[0];
            return true;
        }
        return false;
    }
    public UNIDEPT[] GetAllDept()
    {
        DEPTREQ vrParameter = new DEPTREQ();
        UNIDEPT[] vrResult;
        //vrParameter.dwGetType = (uint)DEPTREQ.DWGETTYPE.DEPTGET_BYALL;
       // vrParameter.dwKind = 768;// (uint)ConfigConst.GCDeptKind;
        if (m_Request.Account.DeptGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            Session["DeptList"] = vrResult;
            return vrResult;
        }
        return null;
    }
    public string szCodeName(string szCode,uint uType)
    {
        if (Session["codeTable"] == null)
        {
            CODINGTABLEREQ vrGet = new CODINGTABLEREQ();
            CODINGTABLE[] vtRes;
            if (m_Request.System.GetCodingTable(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
            {
                Session["codeTable"] = vtRes;
            }
           
        }
        else {
            CODINGTABLE[] vtRes=((CODINGTABLE[])Session["codeTable"]);
            if (vtRes != null && vtRes.Length > 0)
            {
                for (int i = 0; i < vtRes.Length; i++)
                {
                    if (uType == (uint)vtRes[i].dwCodeType && szCode == vtRes[i].szCodeSN.ToString())
                    {
                        return vtRes[i].szCodeName.ToString();
                        
                    }
                }
            }
        }
        return "";
    }
    public bool GetDeptByName(string szName,out UNIDEPT dept)
    {
        dept = new UNIDEPT();
        DEPTREQ vrParameter = new DEPTREQ();
        UNIDEPT[] vrResult;
        vrParameter.szName = szName;
        vrParameter.dwKind = (uint)ConfigConst.GCDeptKind;
        if (m_Request.Account.DeptGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS&&vrResult!=null&&vrResult.Length>0)
        {
            dept = vrResult[0];
            return true;
        }
        return false;
    }
    public bool GetDeptBySN(string szDeptSN, out UNIDEPT dept)
    {
        dept = new UNIDEPT();
        UNIDEPT[] vrResult;
        if (Session["DeptList"] == null)
        {
            vrResult = GetAllDept();
        }
        else
        {
            vrResult = (UNIDEPT[])Session["DeptList"];
        }
        if (vrResult != null && vrResult.Length > 0)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                if (vrResult[i].szDeptSN == szDeptSN)
                {
                    dept=vrResult[i];
                    return true;
                }
            }
        }
        return false;
    }
    public bool GetDeptByID(string szDeptID, out UNIDEPT dept)
    {
        dept = new UNIDEPT();
        UNIDEPT[] vrResult;
        if (Session["DeptList"] == null)
        {
            vrResult = GetAllDept();
        }
        else
        {
            vrResult = (UNIDEPT[])Session["DeptList"];
        }
        if (vrResult != null && vrResult.Length > 0)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                if (vrResult[i].dwID.ToString() == szDeptID)
                {
                    dept = vrResult[i];
                    return true;
                }
            }
        }
        return false;
    }
    public bool IsInGroupMember(uint? uGroupID, uint? uMemberID, uint uKind)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        if (uGroupID == null || uMemberID == null || uGroupID == 0 || uMemberID == 0)
        {
            return false;
        }
        if (uKind == (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL)
        {
            GROUPMEMDETAILREQ vrGet = new GROUPMEMDETAILREQ();
            vrGet.dwGroupID = uGroupID;
            vrGet.dwAccNo = uMemberID;
            vrGet.dwGroupKind = uKind;
            GROUPMEMDETAIL[] vtRes;
            uResponse = m_Request.Group.GetGroupMemDetail(vrGet, out vtRes);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
            {
                return true;
            }
        }
        else
        {
            UNIGROUP[] groupList = GetGroupByID((uint)uGroupID);
            if (groupList != null && groupList.Length > 0&&groupList[0].szMembers!=null)
            {
                GROUPMEMBER[] groupMeberList = groupList[0].szMembers;
                for (int i = 0; i < groupMeberList.Length; i++)
                {
                    if (((uint)groupMeberList[i].dwKind) == uKind && (groupMeberList[i].dwMemberID == uMemberID))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    public bool IsInGroupPersonMember(uint? uGroupID, uint? uMemberID, uint uKind)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        if (uGroupID == null || uMemberID == null || uGroupID == 0 || uMemberID == 0)
        {
            return false;
        }
       
        {
            UNIGROUP[] groupList = GetGroupByID((uint)uGroupID);
            if (groupList != null && groupList.Length > 0 && groupList[0].szMembers != null)
            {
                GROUPMEMBER[] groupMeberList = groupList[0].szMembers;
                for (int i = 0; i < groupMeberList.Length; i++)
                {
                    if (((uint)groupMeberList[i].dwKind) == uKind && (groupMeberList[i].dwMemberID == uMemberID))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    public bool IsInClassGroupMember(uint? uGroupID, uint? uMemberID, uint uKind)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        if (uGroupID == null || uMemberID == null || uGroupID == 0 || uMemberID == 0)
        {
            return false;
        }
        if (uKind == (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL)
        {
            GROUPMEMDETAILREQ vrGet = new GROUPMEMDETAILREQ();
            vrGet.dwGroupID = uGroupID;
            vrGet.dwAccNo = uMemberID;
            vrGet.dwGroupKind = (uint)UNIGROUP.DWKIND.GROUPKIND_RERV;
            GROUPMEMDETAIL[] vtRes;
            uResponse = m_Request.Group.GetGroupMemDetail(vrGet, out vtRes);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
            {
                return true;
            }
        }
        else
        {
            UNIGROUP[] groupList = GetGroupByID((uint)uGroupID);
            if (groupList != null && groupList.Length > 0 && groupList[0].szMembers != null)
            {
                GROUPMEMBER[] groupMeberList = groupList[0].szMembers;
                for (int i = 0; i < groupMeberList.Length; i++)
                {
                    if (((uint)groupMeberList[i].dwKind) == uKind && (groupMeberList[i].dwMemberID == uMemberID))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    public REQUESTCODE AddGroupMember(uint? uGroupID, uint? uMemberID, uint uKind)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        if (uGroupID == null || uMemberID == null ||uGroupID == 0 || uMemberID == 0)
        {
            return uResponse;
        }
        GROUPMEMBER member = new GROUPMEMBER();
        member.dwGroupID = uGroupID;
        member.dwMemberID = uMemberID;
        member.dwKind = uKind;
        return m_Request.Group.SetGroupMember(member);
    }
    public REQUESTCODE AddGroupMember(uint? uGroupID, uint? uMemberID, uint uKind, string szName)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        if (uGroupID == null || uMemberID == null || uGroupID == 0 || uMemberID == 0)
        {
            return uResponse;
        }
        GROUPMEMBER member = new GROUPMEMBER();
        member.dwGroupID = uGroupID;
        member.dwMemberID = uMemberID;
        member.dwKind = uKind;
        member.szName = szName;
        return m_Request.Group.SetGroupMember(member);
    }
    public REQUESTCODE DelGroupMember(uint? uGroupID, uint? uMemberID, uint uKind)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        if (uGroupID == null || uMemberID == null || uGroupID == 0 || uMemberID == 0)
        {
            return uResponse;
        }
        GROUPMEMBER member = new GROUPMEMBER();
        member.dwGroupID = uGroupID;
        member.dwMemberID = uMemberID;
        member.dwKind = uKind;
        return m_Request.Group.DelGroupMember(member);
    }
    public bool GetAccByLogonName(string szLogonName,out UNIACCOUNT acc)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        ACCREQ vrGet = new ACCREQ();
        vrGet.szLogonName = (szLogonName);
        acc = new UNIACCOUNT();
        UNIACCOUNT[] vtRes;
        uResponse=m_Request.Account.Get(vrGet, out vtRes);
        if (uResponse== REQUESTCODE.EXECUTE_SUCCESS&&vtRes != null && vtRes.Length > 0)
        {
            acc=vtRes[0];
            return true;
        }
        return false;
    }
    public bool GetAccByLogonName(string szLogonName, out UNIACCOUNT acc,bool bPIDTrue)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        ACCREQ vrGet = new ACCREQ();
        vrGet.szPID = (szLogonName);
        acc = new UNIACCOUNT();
        UNIACCOUNT[] vtRes;
        uResponse = m_Request.Account.Get(vrGet, out vtRes);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            acc = vtRes[0];
            return true;
        }
        return false;
    }
    public bool GetAccByLogonName(string szLogonName, out UNIACCOUNT acc, out int nCount)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        nCount = 0;
        ACCREQ vrGet = new ACCREQ();
        vrGet.szLogonName = (szLogonName);
        acc = new UNIACCOUNT();
        UNIACCOUNT[] vtRes;
        uResponse = m_Request.Account.Get(vrGet, out vtRes);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            acc = vtRes[0];
            nCount = vtRes.Length;
            return true;
        }
        return false;
    }
    public bool GetAccByTrueName(string szTrueName, out UNIACCOUNT acc)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        ACCREQ vrGet = new ACCREQ();
        vrGet.szTrueName = (szTrueName);
        acc = new UNIACCOUNT();
        UNIACCOUNT[] vtRes;
        uResponse = m_Request.Account.Get(vrGet, out vtRes);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            acc = vtRes[0];
            return true;
        }
        return false;
    }
    public bool GetAccByAccno(string szAccno, out UNIACCOUNT acc)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        ACCREQ vrGet = new ACCREQ();
        vrGet.dwAccNo = Parse(szAccno);
        acc = new UNIACCOUNT();
        UNIACCOUNT[] vtRes;
        uResponse = m_Request.Account.Get(vrGet, out vtRes);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            acc = vtRes[0];
            return true;
        }
        return false;
    }
    public bool GetAccByAccno(string szAccno, out UNIACCOUNT acc,bool isSession)
    {
        acc = new UNIACCOUNT();
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        if (Session["ALLACCOUNT"] == null)
        {
            ACCREQ vrGet = new ACCREQ();

            acc = new UNIACCOUNT();
            UNIACCOUNT[] vtRes;
            uResponse = m_Request.Account.Get(vrGet, out vtRes);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && uResponse == REQUESTCODE.EXECUTE_SUCCESS)
            {
                Session["ALLACCOUNT"] = vtRes;
            }
        }
        UNIACCOUNT[] accList = (UNIACCOUNT[])Session["ALLACCOUNT"];
        for (int i = 0; i < accList.Length; i++)
        {
            if (accList[i].dwAccNo.ToString() == szAccno)
            {
                acc = accList[i];
                return true;
            }
        }
        
        return false;
    }
    public bool GetAdmin(out  UNIADMIN[] adminList)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        ADMINREQ vrAdminGet = new ADMINREQ();       
        uResponse = m_Request.Admin.Get(vrAdminGet, out adminList);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && adminList != null && adminList.Length > 0)
        {
            return true;
        }
        return false;
    }
    public bool IsCheckLogin(string szURL,out string szLogonName, out string szPssWord)
    {
        szPssWord = "";
        string verify, strSysDatetime, jsName, strKey, verify1, userid, url;
        strKey = "zjtcm_zfsoft";
        verify = buildurl(szURL, "verify");
        strSysDatetime = buildurl(szURL, "strSysDatetime");
        jsName = buildurl(szURL, "jsName");
        userid = buildurl(szURL, "userName");
        szLogonName = userid;
        url = Server.UrlDecode(buildurl(szURL, "url"));
        verify1 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(userid + strKey + strSysDatetime + jsName, "MD5");
        if (verify1.Equals(verify))
        {
            szPssWord = "uniFound808";
            return true;
        }
        return false;
    }
    public string buildurl(string url, string param)
    {
        string url1 = url;
        if (url.IndexOf(param) > 0)
        {
            if (url.IndexOf("&", url.IndexOf(param) + param.Length) > 0)
            {
                url1=url.Substring(url.IndexOf(param) + param.Length + 1, url.IndexOf("&", url.IndexOf(param)) - url.IndexOf(param) - param.Length - 1);
            }
            else
            {
               url1="";
            }
            return url1;
        }
        else
        {
            return url1;
        }
    }
    public object SetEmpty0ToNull<T>(T t)
    {
        if (t == null)
        {
            return null;
        }

        //GetHTTPObj
        object ret = Activator.CreateInstance(t.GetType());
        FieldInfo[] its = t.GetType().GetFields();

        for (int i = 0; i < its.Length; i++)
        {
            string fieldName = its[i].Name;
            object value = its[i].GetValue(t);
            Type itt = its[i].FieldType;
            itt.GetType();
            if (itt == typeof(string))
            {
                if (value == null || value.ToString() == "")
                {
                    its[i].SetValue(ret, null);
                }
                else
                {
                    its[i].SetValue(ret, value);
                }
            }
            else if (itt == typeof(uint?))
            {
                if (value == null || value.ToString() == "0")
                {
                    its[i].SetValue(ret, null);
                }
                else
                {
                    its[i].SetValue(ret, value);
                }
            }


        }
        return ret;
    }
    public CODINGTABLE[] getCodeTableByType(uint uType)
    {
        CODINGTABLEREQ vrGet = new CODINGTABLEREQ();
        vrGet.dwCodeType = uType;
        CODINGTABLE[] vtRes;
        if (m_Request.System.GetCodingTable(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            return vtRes;
        }
        return null;
    }
    public UNITERM[] GetAllYearTerm()
    {
        TERMREQ vrGet = new TERMREQ();
        UNITERM[] vtRes;
        if (m_Request.Reserve.GetTerm(vrGet, out vtRes)==REQUESTCODE.EXECUTE_SUCCESS)
        {
            return vtRes;
        }
        return null;
    }
    public bool GetTermNow(out UNITERM Term)
    {
        Term = new UNITERM();
        UNITERM[] vtRes=GetAllYearTerm();
        if (vtRes != null)
        {
            for (int i = 0; i < vtRes.Length; i++)
            {
                if ((((uint)vtRes[i].dwStatus) & (uint)UNITERM.DWSTATUS.TERMSTAT_FORCE) > 0)
                {
                    Term = vtRes[i];
                    return true;
                }
            }
        }
        return false;
    }
    public void WriteTxt(string szTxtInfo)
    {
        try
        {
            FileInfo fileinfo = new FileInfo(Server.MapPath("~/log/") + "logfile.txt");
            using (FileStream fs = fileinfo.OpenWrite())
            {
                StreamWriter sw = new StreamWriter(fs);
                sw.BaseStream.Seek(0, SeekOrigin.End);
                sw.WriteLine("=====================================");
                sw.Write("添加日期为:" + DateTime.Now.ToString("yyyy-MM-dd:HH:mm:ss") + "\r\n");
                sw.Write("日志内容为:" + szTxtInfo + "\r\n");
                sw.WriteLine("=====================================");
                sw.Flush();
                sw.Close();
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
}