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
    protected string m_szPie = "";
    protected string m_szRoom = "";
    protected string m_szResvTeaching= "";
    protected void Page_Load(object sender, EventArgs e)
    {
        TEACHINGRESVREQ vrTeachingResv = new TEACHINGRESVREQ();//获取当前的预约
        vrTeachingResv.dwResvStat = (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DOING;
        vrTeachingResv.dwBeginDate = Parse(DateTime.Now.ToString("yyyyMMdd"));
        vrTeachingResv.dwEndDate = Parse(DateTime.Now.ToString("yyyyMMdd"));
        TEACHINGRESV[] vtTeachingResv ;
        m_Request.Reserve.GetTeachingResv(vrTeachingResv, out vtTeachingResv);
        if (vtTeachingResv != null && vtTeachingResv.Length > 0)
        {
            for (int i = 0; i < vtTeachingResv.Length; i++)
            {
                m_szResvTeaching += GetInputItemHtml(CONSTHTML.checkBox, "resvid", vtTeachingResv[i].szCourseName.ToString() + "(" + vtTeachingResv[i].szTeacherName.ToString() + "," + vtTeachingResv[i].szGroupName.ToString() + ")", vtTeachingResv[i].dwResvID.ToString());
            }
        }
        else
        {
            m_szResvTeaching = "无";
        }
        TEACHINGDEVREQ vrParameter = new TEACHINGDEVREQ();//表单
        BASICROOMREQ vrRoomReq = new BASICROOMREQ();
        BASICROOM[] vtBasicRoom;


        DEVFORTREQ vrParameterT = new DEVFORTREQ();//图标
        if (Request["room"] != null && Request["room"].ToString() != "")
        {
            string szRoomIDs = Request["room"].ToString();
            if (szRoomIDs.EndsWith(","))
            {
                szRoomIDs = szRoomIDs.Substring(0, szRoomIDs.Length - 1);
            }
            vrParameter.szRoomIDs = szRoomIDs;
            vrParameterT.szRoomIDs = szRoomIDs;
        }
        if (Request["devRunState"] != null && Request["devRunState"].ToString() != "")
        {
            vrParameter.dwRunStat = Parse(Request["devRunState"].ToString());
        }
        if (Request["resvid"] != null && Request["resvid"].ToString() != "")
        {

            string szResvIDs = Request["resvid"].ToString();
            if (szResvIDs.EndsWith(","))
            {
                szResvIDs = szResvIDs.Substring(0, szResvIDs.Length - 1);
            }
            vrParameter.szResvIDs = szResvIDs;
        }
       
        vrRoomReq.dwUsePurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_TEACHING;        
        m_Request.Device.BasicRoomGet(vrRoomReq, out vtBasicRoom);
        if (vtBasicRoom != null && vtBasicRoom.Length > 0)
        {
            for (int i = 0; i < vtBasicRoom.Length; i++)
            {
                m_szRoom += GetInputItemHtml(CONSTHTML.checkBox, "room", vtBasicRoom[i].szRoomName.ToString(), vtBasicRoom[i].dwRoomID.ToString());
            }
        }
        else
        {
            m_szRoom = "无";
        }
        string szRunState = Request["state"];
        if (szRunState != null)
        {
            if (szRunState == "total")
            {
                // vrParameter.dwRunStat = (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_RESERVE;
            }
            else if (szRunState == "use")
            {
                vrParameter.dwRunStat = (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_INUSE;
            }
            else if (szRunState == "unUse")
            {
                vrParameter.dwRunStat = 0x8000000 + (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_INUSE;//空闲中
            }
        }
        vrParameter.dwUsePurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_TEACHING;
        TEACHINGDEV[] vrResult;
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        vrParameter.dwUsePurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_TEACHING;
        if (m_Request.Device.TeachingDevGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                uint uState = (uint)vrResult[i].dwDevStat;
                uint uRunState = (uint)vrResult[i].dwRunStat;
                string szRunStateTitle="";
                if ((uRunState & (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_INUSE) > 0)
                {
                    uRunState = (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_INUSE;
                    szRunStateTitle = GetJustName((uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_INUSE, "DEVICE_RunState");
                }
                else
                {
                    uRunState = (uint)vrResult[i].dwRunStat;
                    szRunState = GetJustName(uRunState, "DEVICE_RunState");
                    szRunStateTitle = GetJustName(uRunState, "DEVICE_RunState");
                }

                string szState = "devRunState" + uRunState.ToString();
               
                m_szOut += "<tr>";
                m_szOut += "<td data-id=\"" + vrResult[i].dwDevID.ToString() + "\"+data-labid=\"" + vrResult[i].dwLabID.ToString() + "\">" + "<img src=\"../../../themes/icon_s/" + szState + ".ico\" class=\"imgico\"  title=\"" + szRunStateTitle + "\"/>" + vrResult[i].dwDevSN.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szRoomName.ToString() + "</td>";
                m_szOut += "<td>" + szRunStateTitle + "</td>";
                m_szOut += "<td>" + vrResult[i].szCurTrueName.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szGroupName.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szCourseName.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szTeacherName.ToString() + "</td>";
                m_szOut += "<td>" + Get1970Teaching((vrResult[i].dwTeachingTime)) + "</td>";
                m_szOut += "<td>" + "登录时间无" + "</td>";
                if (((uint)vrResult[i].dwRunStat & (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_RUNNING) > 0)
                {
                    m_szOut += "<td><div class='OPTD OPTDUSING'></div></td>";
                }
                else
                {
                    m_szOut += "<td><div class='OPTD OPTDUN'></div></td>";
                }
               
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Device);
        }
        
        DEVSECINFO[] vrResultT;
        vrParameterT.dwDate = Parse(DateTime.Now.ToString("yyyyMMdd"));
        m_Request.Device.DevForTeachingStat(vrParameterT, out vrResultT);
        
        if (vrResult != null && vrResultT.Length > 0)
        {

            for (int i = 0; i < vrResultT.Length; i++)
            {
                string szResvDev = "";
                if (vrResultT[i].dwResvDevs == null)
                {
                    szResvDev = "0";
                }
                else
                {
                    szResvDev = vrResultT[i].dwResvDevs.ToString();
                }
                string szUseDevs = "";
                if (vrResultT[i].dwUseDevs == null)
                {
                    szUseDevs = "0";
                }
                else
                {
                    szUseDevs = vrResultT[i].dwUseDevs.ToString();
                }
                string szResvUsers = "";
                if (vrResultT[i].dwRealUsers == null)
                {
                    szResvUsers = "0";
                }
                else
                {
                    szResvUsers = vrResultT[i].dwResvUsers.ToString();
                }
                string szRealUsers = "";
                if (vrResultT[i].dwRealUsers == null)
                {
                    szRealUsers = "0";
                }
                else
                {
                    szRealUsers = vrResultT[i].dwRealUsers.ToString();
                }
                m_szPie += "<p><span>" + vrResultT[i].szSecName.ToString()
                        + "</span><strong>" + szResvDev + "</strong><strong>"
                        + szUseDevs + "</strong><strong>"
                        + szResvUsers + "</strong><strong>"
                        + szRealUsers + "</strong></p>";
            }
        }

        PutBackValue();
    }
   
}
