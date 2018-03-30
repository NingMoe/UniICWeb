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
using System.Xml ;
using System.Web.Configuration;

public partial class _Default : UniPage
{
    protected bool bSet = false;
    protected string m_Title = "";
    protected string m_szCtrlMode = "";
    protected string m_szLab = "";
    protected string m_szManager = "";
    protected string m_szRoom = "";
    protected string m_szDevKind = "";
    protected string m_szDevCls = "";
    protected string m_szPorperty = "";
    protected string m_szKindPorperty = "";
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
            newDev.dwDevSN=GetDevSN();
            newDev.dwProperty = CharListToUint(Request["dwProperty"]) + CharListToUint(Request["dwKindProperty"]);
            newDev.dwPurchaseDate = DateToUint(Request["dwPurchaseDate"]);
            newDev.szExtInfo = GetDevExt();
            UNIDEVKIND devKind = new UNIDEVKIND();
            if (!NewDevKind(out devKind))
            {
                MessageBox(m_Request.szErrMessage, "新建" + ConfigConst.GCDevName + "失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                return;
            }
            newDev.dwKindID = devKind.dwKindID;
            UNIGROUP newUseGroup;
            if (NewGroup(newDev.szDevName.ToString()+"使用组", (uint)UNIGROUP.DWKIND.GROUPKIND_DEV, out newUseGroup))
            {
                newDev.dwUseGroupID = newUseGroup.dwGroupID;
            }
            if (m_Request.Device.Set(newDev, out newDev) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "新建"+ConfigConst.GCDevName+"失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                m_Request.Device.DevKindDel(devKind);
                return;
            }
            else
            {
                MessageBox("新建" + ConfigConst.GCDevName + "成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                
                DEVATTENDANT devAttendSet = new DEVATTENDANT();
                devAttendSet.dwAttendantID =Parse(Request["dwAttendantID"]);
                devAttendSet.dwDevID = newDev.dwDevID;
                devAttendSet.dwLabID = newDev.dwLabID;
                devAttendSet.szAttendantName = Request["szAttendantName"];
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

        UNIDEVCLS[] vtDevCls = GetAllDevCls();
        if (vtDevCls != null && vtDevCls.Length > 0)
        {
            for (int i = 0; i < vtDevCls.Length; i++)
            {
                m_szDevCls += "<option value='" + vtDevCls[i].dwClassID + "'>" + vtDevCls[i].szClassName + "</option>";
            }
        }
        m_szPorperty = GetInputHtmlFromXml(0, CONSTHTML.checkBox, "dwProperty", "UNIDEVICE_Property", true);
        m_szKindPorperty = GetInputHtmlFromXml(0, CONSTHTML.checkBox, "dwKindProperty", "DevKind_dwProperty", true);
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
    private bool NewDevKind(out UNIDEVKIND setDevKind)
    {
        setDevKind = new UNIDEVKIND();
        GetHTTPObj(out setDevKind);
        setDevKind.szKindName = Request["szDevName"];
        if(setDevKind.dwClassID == null || setDevKind.dwClassID == 0)
        {
            return false;
        }
        setDevKind.dwProperty = CharListToUint(Request["dwKindProperty"]);
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        uResponse = m_Request.Device.DevKindSet(setDevKind, out setDevKind);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            return true;
        }
        return false;
    }
}
