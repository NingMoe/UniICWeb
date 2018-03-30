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
    protected string m_szLab = "";
    protected string m_szRoom = "";
    protected string m_szDevKind = "";
    protected string m_szCtrlMode = "";
    protected string m_szManager = "";
    protected string m_szPorperty = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        UNIDEVICE newDev;
        uint? uMax = 0;
        uint uID = PRDevice.DEVICE_BASE | PRDevice.MSREQ_DEVICE_SET;
        if (GetMaxValue(ref uMax, uID, "dwDevSN"))
        {

        }
        if (IsPostBack)
        {
            GetHTTPObj(out newDev);
            newDev.dwProperty = CharListToUint(Request["dwProperty"]);
            newDev.dwPurchaseDate = DateToUint(Request["dwPurchaseDate"]);
            ViewState["dwPurchaseDate "] = Get1970Date((uint)newDev.dwPurchaseDate);

            newDev.szExtInfo = GetDevExt();
            if (m_Request.Device.Set(newDev, out newDev) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "修改" + ConfigConst.GCDevName + "失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox("修改" + ConfigConst.GCDevName + "成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                DEVATTENDANT devAttendSet = new DEVATTENDANT();
                devAttendSet.dwAttendantID = Parse(Request["dwAttendantID"]);
                devAttendSet.dwDevID = newDev.dwDevID;
                devAttendSet.dwLabID = newDev.dwLabID;
                devAttendSet.szAttendantName = Request["szAttendantName"];
                devAttendSet.szAttendantTel = Request["szAttendantTel"];
                m_Request.Device.AttendantSet(devAttendSet);
                return;
            }
        }
        UNIADMIN[] adminlist;
        if (GetAdmin(out adminlist) == true)
        {
            for (int i = 0; i < adminlist.Length; i++)
            {
                m_szManager += "<option value='" + adminlist[i].dwAccNo.ToString() + "'>" + adminlist[i].szTrueName + "</option>";
            }
        }
        m_szCtrlMode = GetAllInputHtml(CONSTHTML.option, "", "UNIDEVICE_CtrlMode");
        UNILAB[] vtLab = GetAllLab();
        if (vtLab != null && vtLab.Length > 0)
        {
            for (int i = 0; i < vtLab.Length; i++)
            {
                m_szLab += "<option value='" + vtLab[i].dwLabID + "'>" + vtLab[i].szLabName + "</option>";
            }
        }
        m_szPorperty = GetInputHtmlFromXml(0, CONSTHTML.checkBox, "dwProperty", "UNIDEVICE_Property", true);
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
                    ContDevExt(vtDev[0].szExtInfo.ToString());
                    m_Title = "修改【" + vtDev[0].szDevName + "】";
                }
            }
        }
        else
        {
            m_Title = "新建" + ConfigConst.GCDevName;
        }
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreLoad(e);       
        if (ViewState["szLanguage"] != null && ViewState["szLanguage"].ToString() != "")
        {
            PutMemberValue("szLanguage", ViewState["szLanguage"].ToString());
        }
        if (ViewState["szPerform"] != null && ViewState["szPerform"].ToString() != "")
        {
            PutMemberValue("szPerform", ViewState["szPerform"].ToString());
        }
        if (ViewState["szSample"] != null && ViewState["szSample"].ToString() != "")
        {
            PutMemberValue("szSample", ViewState["szSample"].ToString());
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
        szRes = "{Manufacturers:" + szManufacturers + "},{szNation:" +szNation+"},{szLanguage:"+szLanguage+"},{szPerform:" + szPerform + "},{szSample:" + szSample + "},";
        return szRes;
    }
    private void ContDevExt(string szMemo)
    {
        try
        {
            int uStart = -1;
            int uEnd = -1;
            string szTemp = "";
            szTemp = "{szLanguage:";
            uStart = szMemo.IndexOf(szTemp);
            uEnd = szMemo.IndexOf("},", uStart);
            string szLanguage = "";
            if (uStart > -1 && uEnd > -1)
            {
                szLanguage = szMemo.Substring(uStart + szTemp.Length, uEnd - uStart - szTemp.Length);
                ViewState["szLanguage"] = szLanguage;
            }
            szTemp = "{szPerform:";
            uStart = szMemo.IndexOf(szTemp);
            uEnd = szMemo.IndexOf("},", uStart);
            string szPerform = "";
            if (uStart > -1 && uEnd > -1)
            {
                szPerform = szMemo.Substring(uStart + szTemp.Length, uEnd - uStart - szTemp.Length);
                ViewState["szPerform"] = szPerform;
            }

            szTemp = "{szSample:";
            uStart = szMemo.IndexOf(szTemp);
            uEnd = szMemo.IndexOf("},", uStart);
            string szSample = "";
            if (uStart > -1 && uEnd > -1)
            {
                szSample = szMemo.Substring(uStart + szTemp.Length, uEnd - uStart - szTemp.Length);
                ViewState["szSample"] = szSample;
            }
        }
        catch { 
        }

    }
}
