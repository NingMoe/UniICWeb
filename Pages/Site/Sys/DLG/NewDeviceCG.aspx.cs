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

            newDev.dwDevSN = GetDevSN();
            newDev.dwProperty = CharListToUint(Request["dwProperty"]);
            UNIROOM room;
            if (GetRoomID(newDev.dwRoomID.ToString(),out room))
            {
                newDev.dwLabID=room.dwLabID;
            }
            //newDev.dwPurchaseDate = DateToUint(Request["dwPurchaseDate"]);
            newDev.szExtInfo = GetDevExt();
            UNIGROUP newUseGroup;
            if (NewGroup(newDev.szDevName.ToString() + "使用组", (uint)UNIGROUP.DWKIND.GROUPKIND_DEV, out newUseGroup))
            {
                newDev.dwUseGroupID = newUseGroup.dwGroupID;
            }
            if (m_Request.Device.Set(newDev, out newDev) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "设置"+ConfigConst.GCDevName+"失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox("设置" + ConfigConst.GCDevName + "成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
               
                DEVATTENDANT devAttendSet = new DEVATTENDANT();
                devAttendSet.dwAttendantID =Parse(Request["dwAttendantID"]);
                devAttendSet.dwDevID = newDev.dwDevID;
                devAttendSet.dwLabID = newDev.dwLabID;
                if (devAttendSet.dwAttendantID != null)
                {
                    UNIACCOUNT acc;
                    if (GetAccByAccno(devAttendSet.dwAttendantID.ToString(), out acc))
                    {
                        devAttendSet.szAttendantName = acc.szTrueName;
                    }
                }
              
                devAttendSet.szAttendantTel = Request["szAttendantTel"];
                m_Request.Device.AttendantSet(devAttendSet);
                return;
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
        if (vtLab!=null&&vtLab.Length > 0)
        {
            for (int i = 0; i < vtLab.Length; i++)
            {
                m_szLab += "<option value='" + vtLab[i].dwLabID + "'>" + vtLab[i].szLabName + "</option>";
            }
        }

        UNIROOM[] vtRoom = GetAllRoom();
        if (vtRoom != null && vtRoom.Length > 0)
        {
            for (int i = 0; i < vtRoom.Length; i++)
            {
                m_szRoom += "<option value='" + vtRoom[i].dwRoomID + "'>" + vtRoom[i].szRoomName + "</option>";
            }
        }

        UNIDEVKIND[] vtDevKind = GetAllDevKind();
        if (vtDevKind != null && vtDevKind.Length > 0)
        {
            for (int i = 0; i < vtDevKind.Length; i++)
            {
                m_szDevKind += "<option value='" + vtDevKind[i].dwKindID + "'>" + vtDevKind[i].szKindName + "</option>";
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
                    m_Title = "修改【" + vtDev[0].szPCName + "】";
                }
            }
        }
        else
        {
            m_Title = "新建"+ConfigConst.GCDevName;

        }                      
	}
    private string GetDevExt()
    {
        string szRes = "";
        string szManufacturers = Request["szManufacturers"];
        string szNation = Request["szNation"];
        string szLanguage = Request["szLanguage"];
        string szPerform = Request["szPerform"];
        string szSample = Request["szSample"];
        szRes = "{Manufacturers:" + szManufacturers + "},{szNation:" + szNation + "},{szLanguage:" + szLanguage + "},{szPerform:" + szPerform + "},{szSample:" + szSample + "},";
        return szRes;
    }
}
