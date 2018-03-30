using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_bsd_xl_RTest : UniClientPage
{
    protected string rtRoleList = "";
    protected string rtList = "";
    protected string myRtList = "";
    protected string isList = "display:none;";
    protected string isDetail = "display:none;";
    protected string title = "科研实验预约";
    protected string isLeader = "";
    protected UNIACCOUNT curAcc;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!LoadPage())
        {
            ClientRedirect("Default.aspx");
            return;
        }
        curAcc=(UNIACCOUNT)Session["LOGIN_ACCINFO"];
        if ((curAcc.dwIdent & (int)UNIACCOUNT.DWIDENT.EXTIDENT_RTLEADER) == 0) isLeader = "none";
        string rtid = Request["rtId"];
        string labid = Request["labId"];
        string testname = Request["testName"];
        string validTime = Request["validTime"];
        if (!string.IsNullOrEmpty(labid))
        {
            MyRTCld.LabId = labid;
            MyRTCld.RtId = rtid;
            MyRTCld.ValidTime = validTime;
            if (!string.IsNullOrEmpty(testname))
                curTestName.Value = Server.UrlDecode(testname);
        }
        else
        {
            if (string.IsNullOrEmpty(rtid))
            {
                isList = "";
                InitRTList();
            }
            else
            {
                isDetail = "";
                InitRTRole(rtid);
            }
        }
    }
    private void InitRTList()
    {
        //获取项目列表
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        RESEARCHTESTREQ vrGet = new RESEARCHTESTREQ();
        vrGet.szReqExtInfo.szOrderKey = "szRTName";
        vrGet.szReqExtInfo.szOrderMode = "ASC";
        RESEARCHTEST[] vrResult;
        UNIACCOUNT acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
        vrGet.dwMemberID = acc.dwAccNo;
        uResponse = m_Request.Reserve.GetResearchTest(vrGet, out vrResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                RESEARCHTEST test = vrResult[i];
                if (test.dwLeaderID == acc.dwAccNo || acc.dwAccNo == test.dwHolderID)
                {
                    myRtList += "<tr id='" + test.dwRTID + "' ><td>" + test.szRTSN + "</td><td ><input type='hidden' class='courseId' value='" + test.dwRTID + "'/>" +
                        CutStrT(test.szRTName, 12) + "</td><td >" + test.szHolderName + "</td><td >" + test.szMemo + " 万元</td>"
                         + "<td><input type='hidden' class='rtGroupId' value='" + test.dwGroupID + "'/>" + test.dwGroupUsers + "</td><td><a class='click' onclick='pro.d.rtest.rtMbM(\"" + vrResult[i].dwRTID + "\",\""
                         + vrResult[i].szRTName.Replace('"', '”') + "\")'>成员管理</a> | <a class='click act_get' url='RTest.aspx?rtId=" + test.dwRTID + "' con='#act_qzone'>申请实验</a></td></tr>";
                }
                else
                {
                    bool isMyself = false;
                    int num;
                    string opera = GroupOperate(vrResult[i].RTMembers, acc.dwAccNo, vrResult[i].dwRTID, vrResult[i].dwGroupID, isMyself, out num);
                    rtList += "<tr id='" + vrResult[i].dwRTID + "' ><td>" + test.szRTSN + "</td><td ><input type='hidden' class='courseId' value='" + vrResult[i].dwRTID + "'/>" + CutStrT(vrResult[i].szRTName, 16) + "</td><td >" + vrResult[i].szHolderName + "</td><td >" + vrResult[i].szLeaderName + "</td>" +
                        "<td><input type='hidden' class='rtGroupId' value='" + vrResult[i].dwGroupID + "'/>" + num + "</td>" + opera + "</tr>";
                }
            }
        }
        else
        {
            MsgBox(m_Request.szErrMsg);
            return;
        }
    }
    private string GroupOperate(RTMEMBER[] list, uint? accNo, uint? rtId, uint? groupid, bool isMyself, out int mbNum)
    {
        mbNum = 0;
        string str = "";
        string mbList = "<td class='operate'  rtid='" + rtId + "' groupid='" + groupid + "'>";
        string op = "";
        bool isCk = true;
        for (int i = 0; i < list.Length; i++)
        {
            if ((list[i].dwStatus & 2) > 0)
            {
                str += list[i].szTrueName + ",";
                mbNum++;
            }
            if (list[i].dwAccNo == accNo)
            {
                op = " | <a class='click op quit'>退出</a></td>";
                if ((list[i].dwStatus & 2) == 0) isCk = false;
            }
        }
        if (op == "")
        {
            op = " | <a class='click op join'>加入</a></td>";
        }
        if (isMyself) op = "";
        if (list.Length > 0 && isCk)
        {
            str = str.Substring(0, str.Length - 1);//去掉逗号
            mbList += "<a class='group' title='" + str + "'>查看成员</a>";
        }
        if (!isCk) { mbList += "<span style='color:grey'>等待批准</span>"; }
        return mbList + op;
    }
    private void InitRTRole(string rtid)
    {
        uint? id;
        if (string.IsNullOrEmpty(rtid)) id = null;
        else id = ToUInt(rtid);

        RESEARCHTEST[] ts = GetRTestes(rtid, "", "", "", "");
        if (ts != null && ts.Length > 0) title = ts[0].szRTName;
        SFROLEINFO[] rtroles = GetRTRole(id);
        SFROLEINFO[] useroles = GetUseRole();
        if (useroles != null)
        {
            for (int i = 0; i < useroles.Length; i++)
            {
                SFROLEINFO role = useroles[i];
                string act;
                SFROLEINFO rtrole;
                rtRoleList += "<tr><td>" + CutStrT(role.szLabName, 8) + "</td><td>" + Util.Converter.GetRoleState(role.dwStatus) + applyUseAct(role.dwStatus, role) +
                    "</td><td>" + GetRTState(rtroles, role, id, out act, out rtrole) + "</td><td class='test_name'>" + CutStrT(rtrole.szTargetName, 8) + "</td><td>" + MinToHour(rtrole.dwPermitUseTime) + "</td><td>" + MinToHour(rtrole.dwUseMinATime) +
                    "</td><td>" + MinToHour((uint)rtrole.dwPermitUseTime - rtrole.dwUsedTime) + "</td><td>" + rtrole.dwTesteeNum + "</td><td>" + act + "</td></tr>";
            }
        }
        else
        {
            MsgBox(m_Request.szErrMsg);
        }
    }

    private uint GetRTestRsvTime(string rtId, uint? labId)
    {
        uint ret = 0;
        RTRESVREQ req = new RTRESVREQ();
        req.dwRTID = ToUInt(rtId);
        RTRESV[] rlt;
        m_Request.Reserve.GetRTResv(req, out rlt);
        if (rlt != null && rlt.Length > 0)
        {
            for (int i = 0; i < rlt.Length; i++)
            {
                RTRESV rsv = rlt[i];
                if (rsv.dwLabID == labId)
                {
                    uint t = (uint)(rsv.dwEndTime - rsv.dwBeginTime);
                    t = t / 60;
                    if (t > 5)
                        ret += t;
                }
            }
        }
        return ret;
    }
    private string GetRTState(SFROLEINFO[] rtroles, SFROLEINFO userole, uint? rtid,  out string act, out SFROLEINFO rtrole)
    {
        string str = "";
        rtrole = new SFROLEINFO();
        act = "<span style='color:orange'>条件不足</span>";
        if (rtroles != null)
        {
            for (int i = 0; i < rtroles.Length; i++)
            {
                SFROLEINFO role = rtroles[i];
                if (role.dwLabID == userole.dwLabID)
                {
                    rtrole = role;
                    uint tm = (uint)(role.dwPermitUseTime - role.dwUsedTime);
                    if (IsStat(role.dwStatus, (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK) && IsStat(userole.dwStatus, (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK) && tm > 0)
                    {
                        act = "<a class='click act_get' url='RTest.aspx?labId=" + role.dwLabID + "&rtId=" + role.dwTargetID + "&testName=" + Server.UrlEncode(role.szTargetName) + "&validTime=" + tm + "' con='#act_qzone'>点击预约</a>";
                    }
                    string apply = "";
                    if (IsStat(role.dwStatus, (uint)SFROLEINFO.DWSTATUS.SFROLESTAT_NOAPPLY) || IsStat(role.dwStatus, (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL))
                        apply = "[<span class='click' onclick='applyLab(" + role.dwSFRuleID + "," + rtid + "," + role.dwLabID + "," + role.dwApplyID + ")'>申请实验</span>]";
                    else if(IsStat(role.dwStatus,(uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK))
                        apply = "[<span class='click' onclick='reApplyLab(" + role.dwSFRuleID + "," + rtid + "," + role.dwLabID + "," + role.dwApplyID + ",\""+role.szTargetName+"\")'>申请延时</span>]";
                    if ((IsStat(role.dwStatus, (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL) || IsStat(role.dwStatus, (uint)SFROLEINFO.DWSTATUS.SFROLESTAT_CHECKREJECT)) && !string.IsNullOrEmpty(role.szCheckInfo))
                    {
                        string msg = "[<span class='click' onclick='uni.msgBox(\"" + role.szCheckInfo + "\")'>审核反馈</span>]";
                        apply = "<div>" + msg + apply + "</div>";
                    }
                    return "<span>" + Util.Converter.GetRoleState(role.dwStatus) + apply + "</span>";
                }
            }
        }
        return str;
    }
    //使用资格申请操作
    static public string applyUseAct(uint? sta, SFROLEINFO role)
    {
        string ret = "";
        if ((sta & (uint)SFROLEINFO.DWSTATUS.SFROLESTAT_NOAPPLY) > 0 || (sta & (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL) > 0)
        {
            ret = "[<span class='click' onclick='applyLabUseRole(" + role.dwLabID + "," + role.dwSFRuleID + "," + role.dwApplyID + ",\""+role.szLabName+"\")'>申请资格</span>]";
        }
        if (((sta & (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL) > 0 || (sta & (uint)SFROLEINFO.DWSTATUS.SFROLESTAT_CHECKREJECT) > 0) && !string.IsNullOrEmpty(role.szCheckInfo))
        {
            string msg = "[<span class='click' onclick='uni.msgBox(\"" + role.szCheckInfo + "\")'>审核反馈</span>]";
            ret = "<div>" + msg + ret + "</div>";
        }
        return ret;
    }
}