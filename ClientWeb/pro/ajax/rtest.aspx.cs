using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using System.Xml;

public partial class ClientWeb_pro_ajax_rtest : UniClientAjax
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LoadPage() && IsLoginReady())
        {
            if (act == "new")
            {
                CreateRTest("in");
            }
            else if (act == "get_rt_info")
            {
                GetInfo();
            }
            else if (act == "srch_rt")
            {
                SearchRT();
            }
            else if (act == "del")
            {
                DelRTest();
            }
            else if (act == "apply_rt_lab_userole")
            {
                ApplyRTLabUseRole();
            }
            else if (act == "up_rt_info")
            {
                AlterRTInfo();
            }
            else if (act == "get_rt_mb")
            {
                GetRTMember();
            }
            else if (act == "add_rt_mb")
            {
                RTMember("add");
            }
            else if (act == "del_rt_mb")
            {
                RTMember("del");
            }
            else if (act == "add_g_mb")
            {
                string id = Request["id"];
                string groupId = Request["group_id"];
                UNIACCOUNT[] accs = GetAccById(id);
                if (accs != null && accs.Length > 0)
                {
                    if (AddMember(groupId, id))
                    {
                        SucRlt(ToAcc(accs[0]));
                        return;
                    }
                }
                ErrMsg("返回异常 " + m_Request.szErrMsg);
            }
            else if (act == "del_g_mb")
            {
                string id = Request["id"];
                string groupId = Request["group_id"];
                if (DelMember(groupId, id))
                    SucMsg();
                else
                    ErrMsg();
            }
        }
    }

    private void ApplyRTLabUseRole()
    {
        string roleid = Request["role_id"];
        string labid = Request["lab_id"];
        string applyid = Request["apply_id"];
        string rtid = Request["rt_id"];
        string usetime = Request["usetime"];
        string testName = Request["test_name"];
        string onceTime = Request["once_time"];
        string testNum = Request["test_num"];
        string userNum = Request["user_num"];
        string upFile = Request["save_file_name"];
        string memo = Request["memo"];
        string reApply = Request["re_apply"];
        if (string.IsNullOrEmpty(labid) || string.IsNullOrEmpty(rtid) || string.IsNullOrEmpty(usetime) || string.IsNullOrEmpty(roleid))
        {
            ErrMsg("参数有误");
            return;
        }
        SFROLEINFO para = new SFROLEINFO();
        UNILAB[] labs = GetLab(ToUInt(labid));
        if (reApply == "true")
        {
            SFROLEINFO[] tmp = GetLabRTRole(ToUInt(labid), ToUInt(rtid));
            if (tmp != null && tmp.Length > 0)
            {
                para = tmp[0];
            }
        }
        if (labs != null && labs.Length == 1)
        {
            para.dwAuthType = (uint)SYSFUNCRULE.DWAUTHTYPE.AUTHBY_REARCHTEST;
            para.dwSFRuleID = ToUInt(roleid);
            para.dwTargetID = ToUInt(rtid);
            para.dwLabID = labs[0].dwLabID;
            para.szLabName = labs[0].szLabName;
            if (!string.IsNullOrEmpty(applyid) && applyid.Trim() != "0")
                para.dwApplyID = ToUInt(applyid);
            uint t;
            if (uint.TryParse(usetime, out t))
            {
                if (reApply == "true")
                    para.dwApplyUseTime += t;
                else
                    para.dwApplyUseTime = t;
            }
            if (!string.IsNullOrEmpty(testName))
                para.szTargetName = testName;
            if (!string.IsNullOrEmpty(onceTime))
                para.dwUseMinATime = ToUInt(onceTime);
            if (!string.IsNullOrEmpty(testNum))
                para.dwUseTimes = ToUInt(testNum);
            if (!string.IsNullOrEmpty(userNum))
                para.dwTesteeNum = ToUInt(userNum);
            if (!string.IsNullOrEmpty(upFile))
            {
                if (GetConfig("multiApplyFile") == "1" && reApply == "true")
                {
                    para.szApplyURL += "&" + upFile;
                }
                else
                {
                    para.szApplyURL = upFile;
                }
            }
            if (!string.IsNullOrEmpty(memo))
                para.szMemo = memo;
            REQUESTCODE cd = m_Request.System.SFRoleApply(para);
            if (cd == REQUESTCODE.EXECUTE_SUCCESS)
            {
                SucRlt(para);
            }
            else
            {
                ErrMsg(m_Request.szErrMsg);
            }
        }
        else
        {
            ErrMsg("获取实验室有误");
        }
    }

    private void SearchRT()
    {
        string rtId = Request["rt_id"];
        string name = Request["rt_name"];
        string tutorId = Request["tutor_accno"];
        string leaderId = Request["leader_accno"];
        string mbId = Request["mb_accno"];
        RESEARCHTEST[] list = GetRTestes(rtId, name, tutorId, leaderId, mbId);
        if (list != null && list.Length > 0)
        {
            string tests = "[";
            for (int i = 0; i < list.Length; i++)
            {
                RESEARCHTEST rtest = list[i];
                string data = "{\"rt_id\":\"" + rtest.dwRTID + "\",";
                data += "\"rt_name\":\"" + rtest.szRTName.Replace('"', '”') + "\",";
                data += "\"rt_level\":\"" + rtest.dwRTLevel + "\",";
                data += "\"rt_tutor\":\"" + rtest.szHolderName + "\",";
                data += "\"leader_name\":\"" + rtest.szLeaderName + "\",";
                data += "\"leader_accno\":\"" + rtest.dwLeaderID + "\",";
                data += "\"group_id\":\"" + rtest.dwGroupID + "\"}";
                tests += data + ",";
            }
            tests = tests.Substring(0, tests.Length - 1);
            tests += "]";
            SucRlt(tests);
        }
        else
        {
            ErrMsg("没有查询到任何项目！");
        }
    }

    private void RTMember(string cmd)
    {
        string rtId = Request["rt_id"];
        string groupId = Request["group_id"];
        string id = Request["id"];
        UNIACCOUNT[] accs = GetAccById(id);
        if (accs != null && accs.Length > 0)
        {
            if (cmd == "add")
            {
                if (AddRTMember(rtId, groupId, accs[0].dwAccNo.ToString(), accs[0].szTrueName))
                {
                    SucRlt(ToAcc(accs[0]));
                    return;
                }
            }
            else if (cmd == "del")
            {
                if (DelRTMember(rtId, groupId, accs[0].dwAccNo.ToString()))
                {
                    SucMsg();
                    return;
                }
            }
            ErrMsg();
        }
        else
        {
            ErrMsg("获取账户出错！");
        }
    }

    private void DelRTest()
    {
        uint id = Convert.ToUInt32(Request["id"]);
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        RESEARCHTEST setvalue = new RESEARCHTEST();
        setvalue.dwRTID = id;
        uResponse = m_Request.Reserve.DelResearchTest(setvalue);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            SucRlt("{\"id\":\"" + id + "\"}");
        }
        else
        {
            ErrMsg(m_Request.szErrMsg);
        }
    }

    private void GetInfo()
    {
        string id = Request["rt_id"];
        RESEARCHTEST[] vtResult = GetRTestes(id, "", "", "", "");
        if (vtResult != null && vtResult.Length > 0)
        {
            RESEARCHTEST rtest = vtResult[0];
            string data = "{\"rt_id\":\"" + rtest.dwRTID + "\",";
            data += "\"rt_name\":\"" + rtest.szRTName.Replace('"', '”') + "\",";
            data += "\"rt_level\":\"" + rtest.dwRTLevel + "\",";
            data += "\"rt_tutor\":\"" + rtest.szHolderName + "\",";
            data += "\"leader_name\":\"" + rtest.szLeaderName + "\",";
            data += "\"leader_accno\":\"" + rtest.dwLeaderID + "\",";
            data += "\"group_id\":\"" + rtest.dwGroupID + "\"}";
            SucRlt(data);
        }
        else
        {
            ErrMsg(m_Request.szErrMsg);
        }
    }

    private void AlterRTInfo()
    {
        string rtId = Request["rt_id"];
        string rtName = Request["rt_name"];
        string level = Request["rt_level"];
        string leader = Request["leader_name"];
        uint leaderAcc = Convert.ToUInt32(Request["leader_accno"]);
        string leaderLgName = Request["leader_id"];
        string groupId = Request["group_id"];

        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        RESEARCHTESTREQ vrGet = new RESEARCHTESTREQ();
        vrGet.dwRTID = Convert.ToUInt32(rtId);
        RESEARCHTEST[] vtResult;
        m_Request.Reserve.GetResearchTest(vrGet, out vtResult);
        RESEARCHTEST setvalue = new RESEARCHTEST();
        setvalue = vtResult[0];
        setvalue.szRTName = rtName;
        setvalue.dwRTLevel = ToUInt(level);
        if (!string.IsNullOrEmpty(leaderLgName))//leaderLgName为空 则未更新
        {
            setvalue.dwLeaderID = leaderAcc;
            setvalue.szLeaderName = leader;
            if (!AddMember(groupId, leaderLgName))
            {
                ErrMsg("添加委托人进入项目组出错！");
                return;
            }
        }
        uResponse = m_Request.Reserve.SetResearchTest(setvalue, out setvalue);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            SucMsg();
        }
        else
        {
            ErrMsg(m_Request.szErrMsg);
        }
    }
    private void AlterRTMb()
    {
        string groupId = Request["group_id"];
        string addmemList = Request["addmem_list"];
        string delmemList = Request["delmem_list"];

        if (!string.IsNullOrEmpty(addmemList))
        {
            addmemList = addmemList.Substring(0, addmemList.Length - 1);
        }
        else if (!string.IsNullOrEmpty(delmemList))
        {
            delmemList = delmemList.Substring(0, delmemList.Length - 1);
        }
        if (AlterGroup(groupId, addmemList, delmemList))
        {
            SucMsg();
        }
        else
        {
            ErrMsg();
        }
    }

    private void CreateRTest(string type)
    {
        string rtName = Request["rt_name"];
        string rtSN = Request["rt_sn"];
        string level = Request["rt_level"];
        string rtFee = Request["rt_fee"];
        string ldName = Request["ld_name"];
        string ldAccno = Request["ld_accno"];
        string holder = Request["holder_name"];
        string holderId = Request["holder_id"];
        string memList = Request["mb_list"];
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        RESEARCHTEST setvalue = new RESEARCHTEST();
        setvalue.szRTName = rtName;
        if (!string.IsNullOrEmpty(rtSN))
            setvalue.szRTSN = rtSN;
        if (!string.IsNullOrEmpty(rtFee))
            setvalue.szMemo = rtFee;//临时 项目经费
        else
            setvalue.szMemo = "0";
        if (string.IsNullOrEmpty(holderId))
        {
            setvalue.dwHolderID = curAcc.dwAccNo;
            setvalue.szHolderName = curAcc.szTrueName;
        }
        else
        {
            setvalue.szHolderName = holder;
            setvalue.dwHolderID = ToUInt(holderId);
        }
        if (string.IsNullOrEmpty(ldAccno))
        {
            setvalue.szLeaderName = curAcc.szTrueName;
            setvalue.dwLeaderID = curAcc.dwAccNo;
        }
        else
        {
            setvalue.szLeaderName = ldName;
            setvalue.dwLeaderID = ToUInt(ldAccno);
        }
        if (!string.IsNullOrEmpty(level))
            setvalue.dwRTLevel = ToUInt(level);
        else
            setvalue.dwRTLevel = (uint)RESEARCHTEST.DWRTLEVEL.RTLEVEL_OTHER;
        memList += curAcc.szLogonName.ToString();
        uint groupId = NewGroup(rtName, memList);
        setvalue.dwGroupID = groupId;
        uResponse = m_Request.Reserve.SetResearchTest(setvalue, out setvalue);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            SucMsg();
            if (setvalue.dwHolderID != curAcc.dwAccNo)
            {
                AddMemByAccNo(groupId.ToString(), setvalue.dwHolderID.ToString());
            }
            if (setvalue.dwLeaderID != curAcc.dwAccNo)
            {
                AddMemByAccNo(groupId.ToString(), setvalue.dwLeaderID.ToString());
            }
        }
        else
        {
            ErrMsg(m_Request.szErrMsg);
        }
    }

    private void GetRTMember()
    {
        string id = Request["rt_id"];
        RESEARCHTEST[] vtResult = GetRTestes(id, "", "", "", "");
        if (vtResult != null && vtResult.Length > 0)
        {
            RESEARCHTEST rtest = vtResult[0];
            int group_id = (int)rtest.dwGroupID;
            RTMEMBER[] mbList = rtest.RTMembers;
            unirtmb[] rtmbList = new unirtmb[mbList.Length];
            for (int i = 0; i < mbList.Length; i++)
            {
                UNIACCOUNT acc = GetAccByAccNo(mbList[i].dwAccNo.ToString())[0];
                if (acc.szLogonName != null)
                {
                    unirtmb rtmb = new unirtmb();
                    rtmb.id = acc.szLogonName;
                    rtmb.accno = acc.dwAccNo.ToString();
                    rtmb.name = acc.szTrueName;
                    rtmb.dept = acc.szDeptName;
                    if (IsStat(acc.dwIdent, (uint)UNIACCOUNT.DWIDENT.EXTIDENT_OUTER))
                    {
                        rtmb.ident = "out";//校外用户
                    }
                    if (IsStat(acc.dwIdent, (uint)UNIACCOUNT.DWIDENT.EXTIDENT_RTLEADER) || curAcc.dwAccNo == mbList[i].dwAccNo)
                    {
                        rtmb.ident = "special";//导师或本人
                    }
                    rtmb.rtid = mbList[i].dwRTID.ToString();
                    rtmb.groupid = mbList[i].dwGroupID.ToString();
                    if ((mbList[i].dwStatus & 2) > 0)
                    {
                        rtmb.rtstatus = "yes";
                    }
                    else
                    {
                        rtmb.rtstatus = "no";
                    }
                    rtmbList[i] = rtmb;
                }
            }
            SucRlt(1, rtmbList, "\"" + rtest.dwGroupID + "\"");
        }
        else
        {
            ErrMsg("获取项目时出错！");
        }
    }
     uniacc ToAcc(UNIACCOUNT acc)
    {
        uniacc a = new uniacc();
        a.id = acc.szLogonName;
        a.accno = acc.dwAccNo.ToString();
        a.name = acc.szTrueName;
        a.phone = acc.szHandPhone;
        a.email = acc.szEmail;
        a.ident = acc.dwIdent.ToString();
        a.dept = acc.szDeptName;
        a.deptid = acc.dwDeptID.ToString();
        a.cls = acc.szClassName;

        return a;
    }    
    //用户
    struct uniacc
    {
        public string id;
        public string name;
        public string accno;
        public string phone;
        public string email;
        public string ident;
        public string dept;
        public string deptid;
        public string cls;
    }
    struct unirtmb
    {
        public string id;
        public string name;
        public string accno;
        public string phone;
        public string email;
        public string ident;
        public string dept;
        public string rtid;
        public string groupid;
        public string rtstatus;
    }
}
