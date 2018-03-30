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
using System.Collections.Generic;
using UniWebLib;
using System.Reflection;

public partial class Page_ : UniClientPage
{
    public struct UNIRESERVE_EXT
    {
        public uint dwID;
        public string szOwnerName;
        public uint? dwDevKind;
        public uint? dwDevSN;
        public uint? dwRoomID;
        public string szDevName;
        public string szBeginTime;
        public string szEndTime;
        public string szTeachingTime;
        public string szLabName;
        public string szState;
        public string szStateOut;
        public string szOwner;
        public string groupId;
        public string groupKind;
        public string szMembers;
    };
    protected UNIACCOUNT vrAccInfo;
    protected string rsvList = "";
    protected string rsvList2 = "";
    protected string changePsw = "none";
    protected void Page_Load(object sender, EventArgs e)
    {
        base.LoadPage();
        string url = "index.aspx";
        if (ConfigurationManager.AppSettings["mustLogin"] == "1")
            url = "Login.aspx";
        if (IsLogined())
        {
            vrAccInfo = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
            if (GetConfig("needChangePsw") == "1" && vrAccInfo.szLogonName.ToLower() != "staadmin001" && vrAccInfo.szLogonName.ToLower() != "sysadmin") changePsw = "";
        }
        else
        {
            Response.Redirect(url);
        }
        if (!IsPostBack)
        {
            GetResv();
        }
        GetBreakResv();
    }
    protected void GetResv()
    {
        REQUESTCODE uResponse;
        RESVREQ vrGet = new RESVREQ();
        object objAccno = Session["LOGIN_ACCINFO"];
        if (objAccno == null)
        {
            return;//保存信息有丢失
        }
        UNIACCOUNT vrAccno = (UNIACCOUNT)objAccno;
        //vrGet.dwGetType = (uint)RESVREQ.DWGETTYPE.RESVGET_BYALL; 20140821前
        vrGet.szReqExtInfo.szOrderKey = "dwOccurTime";
        vrGet.szReqExtInfo.szOrderMode = "DESC";
        //vrGet.dwPurpose = (uint)UNIRESERVE_CONST.USEFOR_PERSONNAL);
        if (radState.SelectedValue.ToString() != "0")
        {
            int state = int.Parse(radState.SelectedValue);
            vrGet.dwCheckStat = (uint)state;
        }
        DateTime today = DateTime.Now;
        vrGet.dwBeginDate = ToUInt((today.AddMonths(-12)).ToString("yyyyMMdd"));
        vrGet.dwEndDate = ToUInt((today.AddMonths(12)).ToString("yyyyMMdd"));
        vrGet.dwStatFlag = (uint)RESVREQ.DWSTATFLAG.STATFLAG_INUSE | (uint)RESVREQ.DWSTATFLAG.STATFLAG_OVER | (uint)RESVREQ.DWSTATFLAG.STATFLAG_CHECKFAIL;
        UNIRESERVE[] vtResult;
        UNIRESERVE_EXT[] vtResult_Ext;

        uResponse = m_Request.Reserve.Get(vrGet, out vtResult);
        {
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtResult != null && vtResult.Length > 0)
            {
                vtResult_Ext = GetExtStruct(vtResult);
                for (int i = 0; i < vtResult_Ext.Length; i++)
                {
                    UNIRESERVE_EXT rsv = vtResult_Ext[i];
                    rsvList += "<tr rsvId='" + rsv.dwID + "' owner='" + rsv.szOwner + "'>";
                    rsvList += "<td>" + rsv.dwID + "</td>";
                    rsvList += "<td>" + rsv.szOwnerName + "</td>";
                    rsvList += "<td>" + rsv.szMembers + "</td>";
                    rsvList += "<td>" + rsv.szDevName + "</td>";
                    rsvList += "<td>" + rsv.szStateOut + "</td>";
                    rsvList += "<td>" + rsv.szBeginTime.Substring(5) + "</td>";
                    rsvList += "<td>" + rsv.szEndTime.Substring(5) + "</td>";
                    string act = "";
                    vrAccInfo = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
                    if (rsv.szOwner == vrAccInfo.dwAccNo.ToString())
                    {
                        DateTime end = DateTime.Parse(rsv.szEndTime);
                        DateTime start = DateTime.Parse(rsv.szBeginTime);
                        DateTime now = DateTime.Now;
                        if (start > now && (ToUInt(rsv.szState) & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNDO) > 0)
                        {
                            act += "[<span class='click' rsvId='" + rsv.dwID + "' onclick='delRsv(this);'>删除</span>]";
                            string devId = "";
                            if (rsv.dwDevKind == 0)
                            {
                                UNIDEVICE dev = GetDevBySN(rsv.dwDevSN,rsv.dwRoomID.ToString());
                                if (dev.dwDevID != null) devId = dev.dwDevID.ToString();
                            }

                            act += "[<span class='click' rsvId='" + rsv.dwID + "' devId='" + devId + "' devKind='" + rsv.dwDevKind + "' start='" + rsv.szBeginTime + "' end='" + rsv.szEndTime + "' onclick='alterRsv(this);'>修改</span>]";
                        }
                        if (end > now && (ToUInt(rsv.szState) & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DOING) > 0 && GetConfig("showResvFinish") == "1")
                        {
                            if (act != "") act += "<br/>";
                            act += "[<span class='click' onclick='pro.j.rsv.finish(\"" + rsv.dwID + "\");'>提前结束</span>]";
                        }
                        if (rsv.groupKind == "group" && end > now)
                        {
                            act += "<br/>[<span class='click' onclick='pro.d.group.MbM(\"" + rsv.groupId + "\",\"" + "预约成员组\",\"" + rsv.szState + "\")'>成员管理</span>]";
                        }
                    }
                    rsvList += "<td>" + act + "</td>";
                    rsvList += "</tr>";
                }
            }
        }

    }
    protected void GetBreakResv()
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        RESVREQ vrGet1 = new RESVREQ();
        vrGet1.dwBeginDate = uint.Parse(DateTime.Now.AddMonths(-1).ToString("yyyyMMdd"));
        vrGet1.dwEndDate = uint.Parse(DateTime.Now.ToString("yyyyMMdd"));
        vrGet1.dwCheckStat = (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DEFAULT;
        vrGet1.dwStatFlag = (uint)RESVREQ.DWSTATFLAG.STATFLAG_INUSE | (uint)RESVREQ.DWSTATFLAG.STATFLAG_OVER;
        UNIRESERVE[] vtRes1;
        uResponse = m_Request.Reserve.Get(vrGet1, out vtRes1);
        UNIRESERVE_EXT[] vtResult_Ext;
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes1 != null && vtRes1.Length > 0)
        {
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes1 != null && vtRes1.Length > 0)
            {
                vtResult_Ext = GetExtStruct(vtRes1);
                for (int i = 0; i < vtResult_Ext.Length; i++)
                {
                    UNIRESERVE_EXT rsv = vtResult_Ext[i];
                    rsvList2 += "<tr rsvId='" + rsv.dwID + "' owner='" + rsv.szOwner + "'>";
                    rsvList2 += "<td>" + rsv.dwID + "</td>";
                    rsvList2 += "<td>" + rsv.szOwnerName + "</td>";
                    rsvList2 += "<td>" + rsv.szDevName + "</td>";
                    rsvList2 += "<td>" + rsv.szStateOut + "</td>";
                    rsvList2 += "<td>" + rsv.szBeginTime + "</td>";
                    rsvList2 += "<td>" + rsv.szEndTime + "</td>";
                    rsvList2 += "</tr>";
                }
            }
        }
    }
    UNIRESERVE_EXT[] GetExtStruct(UNIRESERVE[] vtResultPass)
    {
        UNIRESERVE_EXT[] vtResult_Ext = new UNIRESERVE_EXT[vtResultPass.Length];
        RESVDEV[] resvDev;
        for (int i = 0; i < vtResultPass.Length; i++)
        {
            vtResult_Ext[i] = new UNIRESERVE_EXT();

            vtResult_Ext[i].dwID = (uint)vtResultPass[i].dwResvID;
            vtResult_Ext[i].szOwnerName = vtResultPass[i].szOwnerName;
            vtResult_Ext[i].szBeginTime = Get1970Date((int)vtResultPass[i].dwBeginTime);
            vtResult_Ext[i].szEndTime = Get1970Date((int)vtResultPass[i].dwEndTime);
            vtResult_Ext[i].szState = vtResultPass[i].dwStatus.ToString();
            vtResult_Ext[i].szOwner = vtResultPass[i].dwOwner.ToString();
            vtResult_Ext[i].groupKind = (vtResultPass[i].dwMemberKind & (uint)UNIRESERVE.DWMEMBERKIND.MEMBERKIND_GROUP) > 0 ? "group" : "person";
            if (vtResult_Ext[i].groupKind == "group")
            {
                vtResult_Ext[i].groupId = vtResultPass[i].dwMemberID.ToString();
                GROUPMEMDETAIL[] mbs = GetMembers(vtResultPass[i].dwMemberID);
                if (mbs != null && mbs.Length > 0)
                {
                    string str = "";
                    for (int j = 0; j < mbs.Length; j++)
                    {
                        str += mbs[j].szTrueName + ",";
                    }
                    vtResult_Ext[i].szMembers = str.Substring(0, str.Length - 1);
                }
            }
            vtResult_Ext[i].dwDevKind = 0;
            //resvDev = vtResultPass[i].szResvDevs; 20140821前
            resvDev = vtResultPass[i].ResvDev;
            if (resvDev != null && resvDev.Length > 0)
            {
                string devName = string.Empty;
                for (int j = 0; j < resvDev.Length; j++)
                {
                    devName = devName + resvDev[j].szDevName.ToString();// +" X " + resvDev[j].dwDevNum.ToString() + ";  ";
                }
                if (devName != "")
                {
                    vtResult_Ext[i].szDevName = devName;
                }
                else
                {
                    vtResult_Ext[i].szDevName = "请到现场分配";
                }
                uint? sn = resvDev[0].dwDevStart;
                if (sn != null && sn > 0 && resvDev[0].dwDevNum > 0)
                {
                    vtResult_Ext[i].dwRoomID = resvDev[0].dwRoomID;
                    vtResult_Ext[i].dwDevSN = sn;
                }
                else
                    vtResult_Ext[i].dwDevKind = resvDev[0].dwDevKind;
            }
            string szState = "";
            int nStatus = (int)vtResultPass[i].dwStatus;
            if ((nStatus & ((uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK)) > 0)
            {
                szState = "审核通过";
            }
            else if ((nStatus & ((uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL)) > 0)
            {
                szState = "审核不通过";
            }
            else
            {
                szState = "未审核";
            }
            if ((nStatus & ((uint)UNIRESERVE.DWSTATUS.RESVSTAT_FORMAL)) > 0)
            {
                szState += ",预约成功";
            }
            if ((nStatus & ((uint)UNIRESERVE.DWSTATUS.RESVSTAT_DOING)) > 0)
            {
                szState += Translate("已生效");
            }
            if ((nStatus & ((uint)UNIRESERVE.DWSTATUS.RESVSTAT_CANCEL)) > 0)
            {
                szState += ",已取消";
            }
            if ((nStatus & ((uint)UNIRESERVE.DWSTATUS.RESVSTAT_DONE)) > 0)
            {
                szState += ",已结束";
            }
            if ((nStatus & ((uint)UNIRESERVE.DWSTATUS.RESVSTAT_DEFAULT)) > 0)
            {
                szState += ",预约违约";
            }

            vtResult_Ext[i].szStateOut = szState;
            string teachingTime = vtResultPass[i].dwTeachingTime.ToString();
            vtResult_Ext[i].szLabName = vtResultPass[i].szLabName.ToString();
        }
        return vtResult_Ext;
    }
    GROUPMEMDETAIL[] GetMembers(uint? groupId)
    {
        GROUPMEMDETAILREQ req = new GROUPMEMDETAILREQ();
        req.dwGroupID = groupId;
        GROUPMEMDETAIL[] rlt;
        m_Request.Group.GetGroupMemDetail(req, out rlt);
        return rlt;
    }
    private int Get1970Seconds(string Date)//返回和1970的差距秒数
    {
        int result = 0;
        DateTime dt1970 = new DateTime(1970, 01, 01, 08, 0, 0);
        try
        {
            DateTime dtDate = DateTime.Parse(Date);
            TimeSpan spDate = dtDate.Subtract(dt1970);
            result = (int)spDate.TotalSeconds;
        }
        catch
        {
            return -1;
        }
        return result;
    }
    private string Get1970Date(int TotalSeconds)//根据差距秒数 算出现在是日期
    {
        string result = string.Empty;
        DateTime dt1970 = new DateTime(1970, 01, 01, 08, 0, 0);
        DateTime dtNow = dt1970.AddSeconds(TotalSeconds);
        return result = dtNow.ToString("yyyy-MM-dd HH:mm");
    }
    protected void radState_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetResv();
    }
    protected void updateAccount_Click(object sender, EventArgs e)
    {
        if (Request["szPasswd1"] != Request["szPasswd2"])
        {
            Bind ddlBind = new Bind();
            ddlBind.MessageBoxShow("两次密码不一样", this.Page);
            return;
        }
        vrAccInfo = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
        if (vrAccInfo.dwAccNo != null)
        {

            UNIACCOUNT vrParameter = new UNIACCOUNT();
            vrParameter.dwAccNo = vrAccInfo.dwAccNo;
            vrParameter.szLogonName = vrAccInfo.szLogonName.ToString();
            if (!string.IsNullOrEmpty(Request["szPasswd2"]))
                vrParameter.szPasswd = "P" + Request["szPasswd2"];

            vrParameter.szHandPhone = Request["phone"];
            vrParameter.szEmail = Request["mail"];
            if (m_Request.Account.Set(vrParameter, out vrParameter) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                vrAccInfo.szHandPhone = vrParameter.szHandPhone;
                vrAccInfo.szEmail = vrParameter.szEmail;
                Session["LOGIN_ACCINFO"] = vrAccInfo;
            }
            else
            {
                MsgBox(m_Request.szErrMsg);
            }
        }
        else
        {
            MsgBox("登录超时，请重新登录");
        }
    }
    public string GetConfig(string cfg)
    {
        string ret = ConfigurationManager.AppSettings[cfg];
        if (ret == null) return "";
        else return ret;
    }
}
