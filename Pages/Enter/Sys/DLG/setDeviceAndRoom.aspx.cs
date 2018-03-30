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
	protected void Page_Load(object sender, EventArgs e)
	{
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

            newRoom.dwRoomID= newDev.dwRoomID;
            newRoom.dwLabID = newDev.dwLabID;
            newRoom.szRoomNo = newDev.szRoomNo;
            newRoom.szRoomName = newDev.szDevName;
            newRoom.dwOpenRuleSN = newDev.dwOpenRuleSN;
            newRoom.dwManMode = newDev.dwManMode;

            if (m_Request.Device.RoomSet(newRoom, out newRoom) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "修改" + ConfigConst.GCSysKindRoom + "失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                return;
            }
            else
            {
                newDev.dwRoomID = newRoom.dwRoomID;
            }
           
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
        UNILAB[] vtLab=GetAllLab();
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
