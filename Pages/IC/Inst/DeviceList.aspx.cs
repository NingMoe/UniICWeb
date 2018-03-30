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
    protected string m_szRoom = "";
    protected string m_szDevCls = "";
    protected string m_szDevStat = "";
    protected void Page_Load(object sender, EventArgs e)
    {       
        string szlab = Request["lab"];      
        string szCampus = Request["campus"];
        string szRunState = Request["dwRunStat"];
        string szRoom = Request["szRoom"];
        string szDevCls = Request["szDevCls"];
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
        UNIDEVCLS[] devCls = GetDevCLS(0);
        if (devCls != null && devCls.Length > 0)
        {
            for (int i = 0; i < devCls.Length; i++)
            {
                m_szDevCls += "<label><input class=\"enum\" type=\"" + "checkbox" + "\" name=\"" + "szDevCls" + "\" value=\"" + devCls[i].dwClassID.ToString() + "\" /> " + devCls[i].szClassName.ToString() + "</label>";
            }
        }
        FULLROOM[] roomList = GetAllFullRoom();
        if (roomList != null && roomList.Length > 0)
        {
            for (int i = 0; i < roomList.Length; i++)
            {
                if ((uint)roomList[i].dwTotalDevNum > 0)
                {
                    m_szRoom += "<label><input class=\"enum\" type=\"" + "checkbox" + "\" name=\"" + "szRoom" + "\" value=\"" + roomList[i].dwRoomID.ToString() + "\" /> " + roomList[i].szRoomName.ToString() + "</label>";
                }
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
        if (szlab != null && szlab != "")
        {
            vrParameter.szLabIDs = szlab;
        }
        if (szRoom != null && szRoom != "")
        {
            vrParameter.szRoomIDs = szRoom;
        }
        if (szRunState != null && szRunState != "")
        {
            vrParameter.dwRunStat = CharListToUint(szRunState);
        }
        if (szDevCls != null && szDevCls != "")
        {
            vrParameter.szClassIDs = (szDevCls);
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
                uint uRunStateConst=(uint)vrResult[i].dwRunStat;
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
                if (szRunStateTitle == "")
                {
                    szRunStateTitle = GetJustNameEqual(0, "DEVICE_RunState");
                }
                m_szOut += "<tr>";
                m_szOut += "<td  data-roomno=\"" + vrResult[i].szRoomNo.ToString() + "\" data-groupid=\"" + vrResult[i].dwUseGroupID.ToString() + "\"  data-name=\"" + vrResult[i].szDevName.ToString() + "\"  data-labid=\"" + vrResult[i].dwLabID.ToString() + "\" data-id=\"" + vrResult[i].dwDevID.ToString() + "\">" + "<img src=\"../../../themes/icon_s/" + szState + ".ico\" class=\"imgico\" />" + "</td>";
                m_szOut += "<td class='lnkDevice' data-id='" + vrResult[i].dwDevID.ToString() + "'>" + vrResult[i].szAssertSN.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szDevName.ToString() + "</td>";
                string szModel = vrResult[i].szModel + "/" + vrResult[i].szSpecification;
                string szModelRes = "";
                
                if (GetMinStr(szModel, out szModelRes))
                {
                    //型号规格
                   // m_szOut += "<td title=\"" + szModel + "\">" + szModelRes + "</td>";
                }
                else
                {
                   // m_szOut += "<td>" + szModel + "</td>";
                }
                if (((uint)vrResult[i].dwCtrlMode & ((uint)UNIDEVICE.DWCTRLMODE.DEVCTRL_POWERCTRL)) > 0)
                {
                    m_szOut += "<td>" + GetJustName((uint)vrResult[i].dwRunStat, "UNIDEVICE_DevStat") + "</td>";
                }
                else
                {
                    string szDevRunState = GetJustName(uRunState, "DEVICE_RunState");
                    if (szDevRunState == "")
                    {
                        szDevRunState = GetJustNameEqual(0, "DEVICE_RunState");
                    }
                    m_szOut += "<td>" + szDevRunState + "</td>";
                }
                string szRoomState = GetJustName(uRunStateConst, "Unidcs_dwStatusDev");
                if (szRoomState == "")
                {
                    m_szOut += "<td>" + "未启用" + "</td>";
                }
                else
                {
                    m_szOut += "<td>" + szRoomState + "</td>";
                }
               // m_szOut += "<td>" + GetJustName((uint)vrResult[i].dwCtrlMode, "UNIDEVICE_CtrlMode") + "</td>";
                m_szOut += "<td>" + vrResult[i].szRoomName.ToString() + "</td>";
                
               // m_szOut += "<td>" + vrResult[i].szLabName.ToString() + "</td>";                                
                UNIACCOUNT accAttend;
                if (vrResult[i].dwAttendantID!=null)
                {
                    m_szOut += "<td class='lnkAccount' data-id='" + vrResult[i].dwAttendantID.ToString() + "'>" + vrResult[i].szAttendantName.ToString() + "</td>";
                }
                else
                {
                    m_szOut += "<td class='lnkAccount' data-id='" + vrResult[i].dwAttendantID.ToString() + "'>" + ""+ "</td>";
                }

              //  m_szOut += "<td>" + vrResult[i].szAttendantTel + "</td>";

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
               // m_szOut += "<td class='thCenter'><div class='OPTD'></div></td>";
               // m_szOut += "<td class='thCenter'><div class='OPTD OPTDBtnSet'></div></td>";
                m_szOut += "<td class='thCenter'><div class='OPTD OPTDBtnRec'></div></td>";
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
        string szRoom = Request["szRoom"];
        string szDevCls = Request["szDevCls"];
        string szDevKinds = Request["szDevKinds"];
        if (szlab != null && szlab != "")
        {
            PutMemberValue2("lab", szlab);
        }
        if (szCampus != null && szCampus != "")
        {
            PutMemberValue2("campus", szCampus);
        }
        if (szRoom != null && szRoom != "")
        {
            PutMemberValue2("szRoom", szRoom);
        }
        if (szDevCls != null && szDevCls != "")
        {
            PutMemberValue2("szDevCls", szDevCls);
        }
        if (szDevKinds != null && szDevKinds != "")
        {
            PutMemberValue2("szDevKinds", szDevKinds);
        }
    }   
}
