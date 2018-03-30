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

public partial class Sub_Course : UniPage
{
    protected string m_szOpts = "";
    protected MyString m_szOut = new MyString();
    protected string m_szCamp = "";
    protected string m_szLab = "";
    protected string m_szDevStat = "";
    protected void Page_Load(object sender, EventArgs e)
    {       
        string szlab = Request["lab"];      
        string szCampus = Request["campus"];
        string szRunState = Request["dwRunStat"];
        //=========================
        UNILAB[] lab = GetAllLab();
        if (lab != null && lab.Length > 0)
        {
            for (int i = 0; i < lab.Length; i++)
            {                
                m_szLab += "<label><input class=\"enum\" type=\"" + "checkbox" + "\" name=\"" + "lab" + "\" value=\"" + lab[i].dwLabID.ToString() + "\" /> " + lab[i].szLabName.ToString() + "</label>";
            }
        }
        UNICAMPUS[] camp = GetAllCampus();
        if (camp != null && camp.Length > 0)
        {
            for (int i = 0; i < camp.Length; i++)
            {
                m_szCamp += "<label><input class=\"enum\" type=\"" + "checkbox" + "\" name=\"" + "campus" + "\" value=\"" + camp[i].dwCampusID.ToString() + "\" /> " + camp[i].szCampusName.ToString() + "</label>";
            }
        }
        
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        DEVREQ vrParameter = new DEVREQ();
        if (szlab!=null&&szlab != "")
        {
            vrParameter.szLabIDs = szlab;
        }
        if (szCampus != null && szCampus != "")
        {
            vrParameter.szCampusIDs = szCampus;
        }
        if (szRunState != null && szRunState != "")
        {
            vrParameter.dwRunStat = CharListToUint(szRunState);
        }
        GetPageCtrlValue(out vrParameter.szReqExtInfo);    
        UNIDEVICE[] vrResult;
        uResponse= m_Request.Device.Get(vrParameter, out vrResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                uint uState = (uint)vrResult[i].dwDevStat;
                uint uRunState = 0;
                if (vrResult[i].dwRunStat != null)
                {
                    uRunState = (uint)vrResult[i].dwRunStat;
                }
                string szRunStateTitle = "";
                if ((uRunState & (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_INUSE) > 0)
                {
                    uRunState = (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_INUSE;
                    szRunStateTitle = GetJustName((uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_INUSE, "DEVICE_RunState");
                }
                else
                {
                    if (vrResult[i].dwRunStat != null)
                    {
                        uRunState = (uint)vrResult[i].dwRunStat;
                    }
                    else
                    {
                        uRunState = 0;
                    }
                    szRunStateTitle = GetJustName(uRunState, "DEVICE_RunState");
                }
                string szState = "devRunState" + uRunState.ToString();

                m_szOut += "<tr>";
                m_szOut += "<td data-labid=\"" + vrResult[i].dwLabID.ToString() + "\" data-id=\"" + vrResult[i].dwDevID.ToString() + "\">" + "<img src=\"../../../themes/icon_s/" + szState + ".ico\" class=\"imgico\"  title=\"" + szRunStateTitle + "\"/>" + "</td>";
                m_szOut += "<td >" + vrResult[i].dwDevSN.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szDevName.ToString() + "</td>";
                m_szOut += "<td>" + GetJustName((uint)vrResult[i].dwCtrlMode, "UNIDEVICE_CtrlMode") + "</td>";
                if (((uint)vrResult[i].dwCtrlMode & ((uint)UNIDEVICE.DWCTRLMODE.DEVCTRL_POWERCTRL)) > 0)
                {
                    m_szOut += "<td>" + GetJustName((uint)vrResult[i].dwDevStat, "UNIDEVICE_DevStat") + "</td>";
                }
                else
                {
                    m_szOut += "<td>" + GetJustName(uRunState, "DEVICE_RunState") + "</td>";
                }
                m_szOut += "<td>" + vrResult[i].szKindName.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szLabName.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szDeptName + "</td>";
                m_szOut += "<td class='lnkAccount' data-id='" + vrResult[i].dwAttendantID.ToString() + "'>" + vrResult[i].szAttendantName.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szAttendantTel + "</td>";
                DEVUSEINFO[] vtDevUseInfo = vrResult[i].DevUse;
                bool bIsUse = true;
                if (vtDevUseInfo == null || vtDevUseInfo.Length > 0)
                {
                    bIsUse = false;
                }
                if (bIsUse)
                {
                    m_szOut += "<td class='lnkAccount' data-id='" + vtDevUseInfo[0].dwAccNo + "'>" + vtDevUseInfo[0].szTrueName + "</td>";

                }
                else
                {
                    m_szOut += "<td>" + "" + "</td>";
                }
                string szTotalUseMin = "";
                if (ConfigConst.GCDevLoginTime == 1)
                {

                    if (bIsUse && vtDevUseInfo[0].dwBeginTime != null && (uint)vtDevUseInfo[0].dwBeginTime != 0)
                    {
                        uint uTotalUseMin = (Get1970Seconds(DateTime.Now.ToString("yyyy-MM-dd HH:mm")) - (uint)vtDevUseInfo[0].dwBeginTime);
                        if (uTotalUseMin > 0 && uTotalUseMin <= 5184000)
                        {
                            szTotalUseMin = GetTimeForSecond(uTotalUseMin);
                        }
                        m_szOut += "<td>" + szTotalUseMin + "</td>";
                    }
                    else
                    {
                        m_szOut += "<td></td>";
                    }

                }
                else
                {
                    if (bIsUse)
                    {
                        szTotalUseMin = Get1970Date(vtDevUseInfo[0].dwBeginTime, "MM-dd HH:mm");
                        m_szOut += "<td>" + szTotalUseMin + "</td>";
                    }
                    else
                    {
                        m_szOut += "<td>" + "" + "</td>";
                    }
                }

                if (ConfigConst.GCDevListCol == 1)
                {
                    if (bIsUse && (uint)vtDevUseInfo[0].dwBeginTime != 0)
                    {
                        m_szOut += "<td>" + Get1970Date((uint)vtDevUseInfo[0].dwBeginTime, "MM-dd HH:mm") + "到" + Get1970Date((uint)vtDevUseInfo[0].dwLeaveTime, "MM-dd HH:mm") + "</td>";
                    }
                    else
                    {
                        m_szOut += "<td></td>";
                    }
                }
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Device);
        }
        PutBackValue();
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreLoad(e);
        string szlab = Request["lab"];
        string szCampus = Request["campus"];
        if (szlab != null && szlab != "")
        {
            PutMemberValue2("lab", szlab);
        }
        if (szCampus != null && szCampus != "")
        {
            PutMemberValue2("campus", szCampus);
        }
    }
}
