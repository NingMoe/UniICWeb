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
        TEACHINGRESV[] vtTeachingResv ;
        m_Request.Reserve.GetTeachingResv(vrTeachingResv, out vtTeachingResv);
        if (vtTeachingResv != null && vtTeachingResv.Length > 0)
        {
            for (int i = 0; i < vtTeachingResv.Length; i++)
            {
                m_szResvTeaching += GetInputItemHtml(CONSTHTML.checkBox, "resvid", vtTeachingResv[i].dwResvID.ToString(), vtTeachingResv[i].szCourseName.ToString() + "(" + vtTeachingResv[i].szTeacherName.ToString() + "," + vtTeachingResv[i].szGroupName.ToString() + ")");
            }
        }

        TEACHINGDEVREQ vrParameter = new TEACHINGDEVREQ();//表单
        BASICROOMREQ vrRoomReq = new BASICROOMREQ();
        BASICROOM[] vtBasicRoom;


        DEVFORTREQ vrParameterT = new DEVFORTREQ();//图标
        if (Request["room"] != null && Request["room"].ToString() != "")
        {
            m_szOpts += "机房号：" + Request["room"];
            vrParameter.szRoomIDs = Request["room"].ToString();
            vrParameterT.szRoomIDs = Request["room"].ToString();
        }
       // vrRoomReq.dwUseStat = (uint)BASICROOMREQ.DWUSESTAT.ROOMUSESTAT_INUSE;
        vrRoomReq.dwUsePurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_TEACHING;        
        m_Request.Device.BasicRoomGet(vrRoomReq, out vtBasicRoom);
        if (vtBasicRoom != null && vtBasicRoom.Length > 0)
        {
            for(int i=0;i<vtBasicRoom.Length;i++)
            {
                m_szRoom += GetInputItemHtml(CONSTHTML.checkBox, "room", vtBasicRoom[i].szRoomName.ToString(), vtBasicRoom[i].szRoomName.ToString());
            }
        }
        string szRunState = Request["state"];
        if (szRunState != null)
        {
            if (szRunState == "total")
            {
                // vrParameter.dwRunStat = (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_RESERVE;
            }
            else if (szRunState == "idle")
            {
                vrParameter.dwRunStat = (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_RESERVE;
            }
            else if (szRunState == "use")
            {
                vrParameter.dwRunStat = (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_RESERVE + (uint)UNIDEVICE.DWRUNSTAT.DEVSTAT_INUSE;
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
                string szState="devRunState"+uState.ToString();
               
                m_szOut += "<tr>";
                m_szOut += "<td>" + "<img src=\"../../../themes/icon_s/" + szState + ".ico\" class=\"imgico\"  title=\"" + GetJustName(uRunState, "DEVICE_RunState") + "\"/>" + vrResult[i].dwDevSN.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szRoomName.ToString() + "</td>";
                m_szOut += "<td>" + GetJustName(uRunState, "DEVICE_RunState") + "</td>";
                m_szOut += "<td>" + vrResult[i].szCurTrueName.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szGroupName.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szCourseName.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szTeacherName.ToString() + "</td>";
                m_szOut += "<td>" + Get1970Teaching((vrResult[i].dwTeachingTime)) + "</td>";
                m_szOut += "<td>" + "登录时间无" + "</td>";                
                m_szOut += "<td><div class='OPTD'></div></td>";
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
                    szResvUsers = vrResultT[i].dwRealUsers.ToString();
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
