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
    protected string m_szCtrlMode = "";
    protected string m_szLab = "";
    protected string m_szManager = "";
    protected string m_szRoom = "";
    protected string m_szDevKind = "";
    protected string m_szPorperty = "";
    protected string m_szOpenRule = "";
    protected string m_szRoomMode = "";
    protected string szOption = "";
    protected string szSubRoom = "";
    public string szRoomList = "";
    protected string szCamp = "";
	protected void Page_Load(object sender, EventArgs e)
	{
        for (int i = 1; i < 100; i++)
        {
            szOption += GetInputItemHtml(CONSTHTML.option, "", i.ToString(), i.ToString());
        }
        UNIROOM[] allRoom = GetRoomByClassKind((uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS);
        for (int i = 0; i < allRoom.Length; i++)
        {
            szRoomList += GetInputItemHtml(CONSTHTML.option, "",allRoom[i].szRoomName, allRoom[i].dwRoomID.ToString());
            UNIDEVICE devInfo=new UNIDEVICE();
            string szRoomnoinfo = "";
            if(getDevByID(Request["id"].ToString(),out devInfo))
            {
                szRoomnoinfo = devInfo.szRoomNo.ToString();
            }
            if (!((((uint)allRoom[i].dwProperty) & (uint)UNIROOM.DWPROPERTY.ROOMPROP_COMBINE) > 0) && allRoom[i].szRoomNo != szRoomnoinfo)
            {
                szSubRoom += GetInputItemHtml(CONSTHTML.checkBox, "subRoom", allRoom[i].szRoomName, allRoom[i].szRoomNo.ToString());
            }
        }
        UNICAMPUS[] vtCamp = GetAllCampus();
        for (int i = 0; i < vtCamp.Length; i++)
        {
            szCamp += GetInputItemHtml(CONSTHTML.option, "", vtCamp[i].szCampusName, vtCamp[i].dwCampusID.ToString());
        }
        UNIDEVICE newDev;
        uint? uMax=0;        
        uint uID= PRDevice.DEVICE_BASE|PRDevice.MSREQ_DEVICE_SET;
        if (GetMaxValue(ref uMax, uID, "dwDevSN"))
        {

        }
        if (IsPostBack)
        {
            GetHTTPObj(out newDev);
           // newDev.dwDevSN = GetDevSN();
           // newDev.dwProperty = CharListToUint(Request["dwProperty"]);
            uint uProperty = CharListToUint(Request["dwProperty2"]);
            if ((uProperty & (uint)UNIDEVICE.DWPROPERTY.DEVPROP_MAIN) > 0)
            {
                if (newDev.dwProperty == null)
                {
                    newDev.dwProperty = 0;
                }
                newDev.dwProperty = (uint)newDev.dwProperty | (uint)UNIDEVICE.DWPROPERTY.DEVPROP_MAIN;
            }
            else if (((uProperty & (uint)UNIDEVICE.DWPROPERTY.DEVPROP_MAIN) == 0))
            {

                UNIDEVICE devGet;
                if(getDevByID(newDev.dwDevID.ToString(),out devGet))
                {
                    if ((devGet.dwProperty & (uint)UNIDEVICE.DWPROPERTY.DEVPROP_MAIN) > 0)
                    {
                        newDev.dwProperty = 0;// (uint)devGet.dwProperty - (uint)UNIDEVICE.DWPROPERTY.DEVPROP_ACTIVITYPLAN;
                    }
                }
            }
            
            if (((uProperty & (uint)UNIDEVICE.DWPROPERTY.DEVPROP_ACTIVITYPLAN) > 0))
            {
                if (newDev.dwProperty == null)
                    newDev.dwProperty =(uint)UNIDEVICE.DWPROPERTY.DEVPROP_ACTIVITYPLAN;
                else
                {
                    newDev.dwProperty = (uint)newDev.dwProperty | (uint)UNIDEVICE.DWPROPERTY.DEVPROP_ACTIVITYPLAN;
                }
            }
            string szOpen = Request["chkopen"];
            if (newDev.dwProperty == null)
            {
                newDev.dwProperty = 0;
            }
            if (szOpen != null && szOpen == "1")
            {
                newDev.dwProperty = (uint)newDev.dwProperty | (uint)UNIDEVICE.DWPROPERTY.DEVPROP_NORESV;
            }
            else
            {
                if ((((uint)newDev.dwProperty) & (uint)UNIDEVICE.DWPROPERTY.DEVPROP_NORESV) > 0)
                {
                    newDev.dwProperty = (uint)newDev.dwProperty - (uint)UNIDEVICE.DWPROPERTY.DEVPROP_NORESV;
                }
            }
            newDev.dwManMode = CharListToUint(Request["dwManMode"]);      
            UNIROOM newRoom = new UNIROOM();
            if (!string.IsNullOrEmpty(newDev.szDevURL))
            {
                string szTemp = newDev.szDevURL;
                szTemp = szTemp.Replace("\"", "");
                szTemp = szTemp.Replace("\\","");
                szTemp = szTemp.Replace("n", "\\n");
                newDev.szDevURL = szTemp;
                newRoom.szMemo = szTemp;// newDev.szDevURL;
            }
            newRoom.dwRoomID= newDev.dwRoomID;
            newRoom.dwLabID = newDev.dwLabID;
            newRoom.szRoomNo = newDev.szRoomNo;
            newRoom.szRoomName = newDev.szDevName;
            newRoom.dwOpenRuleSN = newDev.dwOpenRuleSN;
            newRoom.dwManMode = newDev.dwManMode;
            newRoom.dwCampusID = Parse(Request["dwCampusID"]);
            string szRoom = Request["subRoom"];
            if (szRoom != null && szRoom != "")
            {
                newRoom.szSubRooms = ","+szRoom;
                newRoom.dwProperty = (uint)UNIROOM.DWPROPERTY.ROOMPROP_COMBINE;
            }
            else
            {
                newRoom.szSubRooms= "";
                newRoom.dwProperty = (uint)UNIROOM.DWPROPERTY.ROOMPROP_SUBROOM;
            }

            for (int i = 0; i < allRoom.Length; i++)
            {
                string szRoomNameTemp = newRoom.szRoomName;
                string szRoomNoTemp = newRoom.szRoomNo.ToString();
                if (allRoom[i].dwRoomID == newRoom.dwRoomID)
                {
                    continue;
                }
                if (allRoom[i].szRoomName == szRoomNameTemp)
                {
                    MessageBox("房间名称已存在", "新建" + ConfigConst.GCSysKindRoom + "失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                    return;
                }
                if (allRoom[i].szRoomNo == szRoomNoTemp)
                {
                    MessageBox("编号已存在", "新建" + ConfigConst.GCSysKindRoom + "失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                    return;
                }
            }


            UNIDEVICE[] devListTemp= GetDevByRoomId(newDev.dwRoomID);
            if (devListTemp.Length == 1)
            {
                if (m_Request.Device.RoomSet(newRoom, out newRoom) != REQUESTCODE.EXECUTE_SUCCESS)
                {
                    MessageBox(m_Request.szErrMessage, "修改" + ConfigConst.GCSysKindRoom + "失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                    return;
                }
                else
                {
                    newDev.dwRoomID = newRoom.dwRoomID;
                }
            }
           // newDev.dwUnitPrice=
            if (m_Request.Device.Set(newDev, out newDev) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "修改" + ConfigConst.GCSysKindRoom + "失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                return;
            }
            else
            {
                MessageBox("修改" + ConfigConst.GCSysKindRoom + "成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                UNIACCOUNT accInfo;
                if (newDev.dwAttendantID != null && GetAccByAccno(newDev.dwAttendantID.ToString(), out accInfo) == true)
                {
                    DEVATTENDANT devAttendSet = new DEVATTENDANT();
                    devAttendSet.dwAttendantID = accInfo.dwAccNo;
                    devAttendSet.szAttendantName = accInfo.szTrueName;
                    devAttendSet.szAttendantTel = Request["szAttendantTel"];
                    devAttendSet.dwDevID = newDev.dwDevID;
                    devAttendSet.dwLabID = newDev.dwLabID;
                    m_Request.Device.AttendantSet(devAttendSet);
                    return;
                }

            }
        }
        uint uRoomMode = (uint)(UNIROOM.DWMANMODE.ROOMMAN_CAMERA | UNIROOM.DWMANMODE.ROOMMAN_DOORLOCK | UNIROOM.DWMANMODE.ROOMMAN_FREEINOUT);
        m_szRoomMode = GetInputHtml(uRoomMode, CONSTHTML.checkBox, "dwManMode", "Room_dwManMode");
        DEVOPENRULE[] vtOpenRule = GetAllOpenRule();
        if (vtOpenRule != null && vtOpenRule.Length > 0)
        {
            for (int i = 0; i < vtOpenRule.Length; i++)
            {
                m_szOpenRule += "<option value='" + vtOpenRule[i].dwRuleSN + "'>" + vtOpenRule[i].szRuleName + "</option>";
            }
        }
        m_szCtrlMode = GetAllInputHtml(CONSTHTML.option, "", "UNIDEVICE_CtrlMode");
        UNIADMIN[] adminlist;
        if (GetAdmin(out adminlist) == true)
        {
            for (int i = 0; i < adminlist.Length; i++)
            {
                m_szManager += "<option value='" + adminlist[i].dwAccNo.ToString() + "'>" + adminlist[i].szTrueName + "</option>";
            }
        }
        UNILAB[] vtLab=GetLabByClass((uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS);
        if (vtLab != null && vtLab.Length > 0)
        {
            for (int i = 0; i < vtLab.Length; i++)
            {
                m_szLab += "<option value='" + vtLab[i].dwLabID + "'>" + vtLab[i].szLabName + "</option>";
            }
        }
        m_szPorperty = GetInputHtmlFromXml(0, CONSTHTML.checkBox, "dwProperty", "UNIDEVICE_Property", true);
        if (Request["op"] == "set")
        {
            bSet = true;
            DEVREQ vrDevReq = new DEVREQ();
            vrDevReq.dwDevID = Parse(Request["id"]);
            UNIDEVICE[] vtDev;
            if (m_Request.Device.Get(vrDevReq, out vtDev) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else
            {
                if (vtDev.Length == 0)
                {
                    MessageBox("获取失败", "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                }
                else
                {
                    PutJSObj(vtDev[0]);
                    UNIROOM roomInfo;
                    if (GetRoomID(vtDev[0].dwRoomID.ToString(), out roomInfo))
                    {
                        //Response.Write(roomInfo.szSubRooms.ToString());
                        //Response.End();
                        PutMemberValue("subRoom", roomInfo.szSubRooms.ToString()+",");
                    }
                    ViewState["dwProperty2"] = vtDev[0].dwProperty;
                    ViewState["chkopen"] = vtDev[0].dwProperty;
                    m_Title = "修改【" + vtDev[0].szDevName + "】";
                }
            }
        }
        else
        {
            m_Title = "修改" + ConfigConst.GCSysKindRoom;

        }                      
	}
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreLoad(e);

        if (ViewState["dwProperty2"] != null && ViewState["dwProperty2"].ToString() != "")
        {
            PutMemberValue("dwProperty2", ViewState["dwProperty2"].ToString());
        }
        if (ViewState["chkopen"] != null && ViewState["chkopen"].ToString() != "")
        {
            uint uOpen = Parse(ViewState["chkopen"].ToString());
            if ((uOpen & (uint)UNIDEVICE.DWPROPERTY.DEVPROP_NORESV) > 0)
            {
                PutMemberValue("chkopen", "1");
            }
        }
    }
}
