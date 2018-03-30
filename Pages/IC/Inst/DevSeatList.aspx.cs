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
    protected string m_szDevKind = "";    
    protected string m_szDevStat = "";
    protected bool isSmart = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        DEVMONITORREQ vrParameterMon = new DEVMONITORREQ();
        DEVMONITOR[] vrResultMon;
        if (m_Request.Device.DevMonitorGet(vrParameterMon, out vrResultMon) == REQUESTCODE.EXECUTE_SUCCESS && vrResultMon != null && vrResultMon.Length > 0)
        {
            isSmart = true;
        }
        string szlab = Request["lab"];
        string szCampus = Request["campus"];
        string szRunState = Request["dwRunStat"];
        string szRoom = Request["szRoom"];
        string szDevCls = Request["szDevCls"];
        string szDevName = Request["szDevName"];
        string szDevKinds = Request["szDevKinds"];
        //=========================
        UNILAB[] lab = GetAllLab();
        if (lab != null && lab.Length > 0)
        {
            for (int i = 0; i < lab.Length; i++)
            {
                m_szLab += "<label><input class=\"enum\" type=\"" + "checkbox" + "\" name=\"" + "lab" + "\" value=\"" + lab[i].dwLabID.ToString() + "\" /> " + lab[i].szLabName.ToString() + "</label>";
            }
        }
        UNIROOM[] room = GetRoomByClassKind((uint)UNIDEVCLS.DWKIND.CLSKIND_SEAT);
        if (room != null && room.Length > 0)
        {
            for (int i = 0; i < room.Length; i++)
            {
                m_szRoom += "<label><input class=\"enum\" type=\"" + "checkbox" + "\" name=\"" + "szRoom" + "\" value=\"" + room[i].dwRoomID.ToString() + "\" /> " + room[i].szRoomName.ToString() + "</label>";
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
        UNIDEVKIND[] devKind = GetDevKindByKind((uint)UNIDEVCLS.DWKIND.CLSKIND_SEAT);
        if (devKind != null && devKind.Length > 0)
        {
            for (int i = 0; i < devKind.Length; i++)
            {
                m_szDevKind += "<label><input class=\"enum\" type=\"" + "checkbox" + "\" name=\"" + "szDevKinds" + "\" value=\"" + devKind[i].dwKindID.ToString() + "\" /> " + devKind[i].szKindName.ToString() + "</label>";
            }
        }
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        DEVREQ vrParameter = new DEVREQ();
        if (szDevName != null && szDevName != "")
        {
            vrParameter.szSearchKey = szDevName;
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
        if (szDevKinds != null && szDevKinds != "")
        {
            vrParameter.szKindIDs = (szDevKinds);
        }

        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        vrParameter.dwClassKind = (uint)UNIDEVCLS.DWKIND.CLSKIND_SEAT;
        // vrParameter.dwRunStat = ~((uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_INUSE);
        vrParameter.dwReqProp = (uint)DEVREQ.DWREQPROP.DEVREQ_NEEDDEVUSE;
        uint uNoNeed = Parse(Request["dwRunStat2"]);
        if (uNoNeed == 1)
        {
            vrParameter.dwUnNeedRunStat = (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_INUSE;
            // vrParameter.dwRunStat = null;
        }
        /*
        if (vrParameter.dwRunStat!=null&&uNoNeed == 1 && (((uint)vrParameter.dwRunStat) & (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_INUSE) > 0)
        {
            vrParameter.dwUnNeedRunStat = null;
            vrParameter.dwRunStat = null;
        }
        */
        uint uStateTemp = 0;
        if (vrParameter.dwRunStat != null)
        {
            uStateTemp = (uint)vrParameter.dwRunStat;
        }
        if ((uStateTemp & (uint)UNIDEVICE.DWRUNSTAT.DRUNSTAT_NOPERSON) > 0 && ((uStateTemp & (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_INUSE)) > 0)
        {
            //vrParameter.dwRunStat =(uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_LEAVEFW;
        }
        vrParameter.dwRunStat = CharListToUint((Request["dwRunStat"]));
        UNIDEVICE[] vrResult;
        uResponse = m_Request.Device.Get(vrParameter, out vrResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                uint uState = (uint)vrResult[i].dwDevStat;
                uint uCtrlMode = (uint)vrResult[i].dwCtrlMode;
                uint uRunState = 0;
                uint uRunStateConst = (uint)vrResult[i].dwRunStat;
                if (vrResult[i].dwRunStat != null)
                {
                    uRunState = (uint)vrResult[i].dwRunStat;
                }
                if ((uCtrlMode & (uint)UNIDEVICE.DWCTRLMODE.DEVCTRL_PERSONDETECT) > 0)//智能识别
                {
                    if ((uRunState & (uint)UNIDEVICE.DWRUNSTAT.DRUNSTAT_CONTROLLER_TROUBLE) > 0)//故障)
                    {
                        uRunState = (uint)UNIDEVICE.DWRUNSTAT.DRUNSTAT_CONTROLLER_TROUBLE;
                    }
                    else
                    {
                        if ((uRunState & (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_INUSE) > 0)//是否使用中
                        {
                            if ((uRunState & (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_RESERVE) > 0)//是否有预约
                            {
                                if ((uRunState & (uint)UNIDEVICE.DWRUNSTAT.DRUNSTAT_WITHPERSON) > 0)//有人
                                {
                                    //已分配(有人)
                                    uRunState = (uint)UNIDEVICE.DWRUNSTAT.DRUNSTAT_WITHPERSON + (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_INUSE;//| (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_INUSE;
                                }
                                else if ((uRunState & (uint)UNIDEVICE.DWRUNSTAT.DRUNSTAT_NOPERSON) > 0)//无人
                                {
                                    //已分配(无人)
                                    if ((uRunState & (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_LEAVEFW) > 0)//暂时离开
                                    {
                                        uRunState = (uint)UNIDEVICE.DWRUNSTAT.DRUNSTAT_NOPERSON + (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_INUSE + (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_LEAVEFW;
                                    }
                                    else
                                    {
                                        uRunState = (uint)UNIDEVICE.DWRUNSTAT.DRUNSTAT_NOPERSON + (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_INUSE;
                                    }
                                }
                            }
                            else
                            {
                                uRunState = 0;
                            }
                        }
                        else
                        {
                            if ((uRunState & (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_RESERVE) > 0)//是否有预约
                            {
                                if ((uRunState & (uint)UNIDEVICE.DWRUNSTAT.DRUNSTAT_WITHPERSON) > 0)//有人
                                {
                                    //未分配(有人)
                                    uRunState = (uint)UNIDEVICE.DWRUNSTAT.DRUNSTAT_WITHPERSON;
                                }
                                else if ((uRunState & (uint)UNIDEVICE.DWRUNSTAT.DRUNSTAT_NOPERSON) > 0)//无人
                                {
                                    //未分配(无人)
                                    if ((uRunState & (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_LEAVEFW) > 0)//暂时离开
                                    {
                                        uRunState = (uint)UNIDEVICE.DWRUNSTAT.DRUNSTAT_NOPERSON + (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_LEAVEFW;
                                    }
                                    else
                                    {
                                        uRunState = (uint)UNIDEVICE.DWRUNSTAT.DRUNSTAT_NOPERSON;
                                    }
                                }
                            }
                            else
                            {
                                uRunState = 0;
                            }
                        }
                    }
                }
                else//人工管理
                {
                    if ((uRunState & (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_INUSE) > 0 && (uRunState & (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_LEAVEFW) > 0)//有人
                    {
                        uRunState = (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_INUSE + (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_LEAVEFW;
                    }
                    if ((uRunState & (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_INUSE) > 0)//有人
                    {
                        uRunState = (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_INUSE;
                    }

                }



                /*
                if ((uRunState & (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_INUSE) > 0)
                {
                    uRunState = (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_INUSE;
                    if ((uRunStateConst & (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_LEAVEFW) > 0)
                    {
                        uRunState |= (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_LEAVEFW;
                    }
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
                */
                string szRunStateTitle = "";
                szRunStateTitle = GetJustName(uRunState, "DEVICE_RunState");
                string szState = "devRunState";
                if ((uRunState & (uint)UNIDEVICE.DWRUNSTAT.DRUNSTAT_WITHPERSON) > 0)//有人
                {
                    szState += ((uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_INUSE).ToString();
                }
                else
                {
                    szState += "0";
                }


                m_szOut += "<tr>";
                m_szOut += "<td  data-groupid=\"" + vrResult[i].dwUseGroupID.ToString() + "\"  data-name=\"" + vrResult[i].szDevName.ToString() + "\"  data-labid=\"" + vrResult[i].dwLabID.ToString() + "\" data-id=\"" + vrResult[i].dwDevID.ToString() + "\">" + "<img src=\"../../../themes/icon_s/" + szState + ".ico\" class=\"imgico\" />" + "</td>";

                m_szOut += "<td  data-id='" + vrResult[i].dwDevID.ToString() + "'>" + vrResult[i].szDevName.ToString() + "</td>";
                if (((uint)vrResult[i].dwCtrlMode & ((uint)UNIDEVICE.DWCTRLMODE.DEVCTRL_BYHAND)) > 0)
                {
                    m_szOut += "<td title=" + vrResult[i].dwRunStat.ToString() + ">" + GetJustName((uint)vrResult[i].dwRunStat, "DEVICE_RunState") + "</td>";
                }
                else
                {
                    string szDevRunState = GetJustNameEqual(uRunState, "DEVICEDevDec_RunState", false);
                    m_szOut += "<td title=" + vrResult[i].dwRunStat.ToString() + ">" + szDevRunState + "</td>";
                }
                m_szOut += "<td>" + vrResult[i].szRoomName.ToString() + "</td>";

                m_szOut += "<td>" + vrResult[i].szKindName.ToString() + "</td>";
                DEVUSEINFO[] vtDevUseInfo = vrResult[i].DevUse;
                bool bIsUse = true;
                if (vtDevUseInfo == null || vtDevUseInfo.Length == 0)
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
                m_szOut += "</tr>";
                UpdatePageCtrl(m_Request.Device);
            }
            PutBackValue();
        }
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
