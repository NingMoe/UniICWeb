using System;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Xml;
using UniWebLib;
using Util;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;

public partial class ClientWeb_pro_ajax_account : UniClientAjax
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LoadPage())
        {
            if (Request["act"] == "update_openid")
            {
                upOpenid();
            }
            else if (IsLoginReady())
            {
                if (act == "update_contact")
                {
                    UNIACCOUNT set = new UNIACCOUNT();
                    set.dwAccNo = curAcc.dwAccNo;
                    set.szLogonName = curAcc.szLogonName;
                    set.szHandPhone = Request["phone"];
                    set.szEmail = Request["email"];
                    //短信提醒
                    if (Request["note_alert"] == "false")
                        set.dwKind = (uint)UNIACCOUNT.DWKIND.EXTKIND_NOMSG;//set.dwKind | (uint)UNIACCOUNT.DWKIND.EXTKIND_NOMSG;
                    else if (Request["note_alert"] == "true")
                        set.dwKind = 0;//set.dwKind & (~(uint)UNIACCOUNT.DWKIND.EXTKIND_NOMSG);
                    //
                    UpdateAcc(set);
                }
                else if (act == "update_pwd")
                {
                    UNIACCOUNT set = new UNIACCOUNT();
                    set.szLogonName = curAcc.szLogonName;
                    set.dwAccNo = curAcc.dwAccNo;
                    string pwd = Request["pwd"];
                    set.szPasswd = "P" + pwd;
                    UpdateAcc(set);
                }
                else if (act == "update_tutor")
                {
                    UpAccWithTutor(0);
                }
                else if (act == "set_tutor")
                {
                }
                else if (act == "update_tutor_cked")
                {
                    UpAccWithTutor((uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK);//默认无需导师确认
                }
                else if(act=="del_wechat"){
                    if (IsLoginReady())
                    {
                        DelWeChat();
                    }
                }
                else if (act == "apply_lab_use_role")
                {
                    ApplyUseRole();
                }
                else if (act == "apply_use_role")
                {
                    ApplyUseRole();
                }
                else if (act == "assign_tutor")
                {
                    AssignTutor(0);
                }
                else if (act == "assign_tutor_cked")
                {
                    AssignTutor((uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK);
                }
                else if (act == "get_acc_id")
                {
                    string id = Request["id"];
                    GetAccById(id, 0);
                }
                else if (act == "get_acc_name")
                {
                    string name = Request["name"];
                    uint? ident = ToUInt(Request["ident"]);
                    GetAccByName(name, ident);
                }
                else if (act == "get_acc_accno")
                {
                    GetAccByAccNo(Request["accno"], 0);
                }
                else if (act == "get_tutor_name")
                {
                    string name = Request["stu_name"];
                    GetAccByName(name, (uint)UNIACCOUNT.DWIDENT.EXTIDENT_TUTOR);
                }
                else if (act == "tutor_rel")
                {
                    TutorRel();
                }
            }
        }
    }

    private void DelWeChat()
    {
        UNIACCOUNT set = new UNIACCOUNT();
        set.szMSN = "";
        UpdateAcc(set);
    }

    private void upOpenid()
    {
        string id = Request["ID"];
        string openid = Request["openid"];
        uint session = ToUInt(Request["session"]);
        if (string.IsNullOrEmpty(id) || session == 0)
        {
            ErrMsgP();
            return;
        }
        m_Request.m_UniDCom.SessionID = session;//重置session
        UNIACCOUNT[] accs = GetAccById(id);
        if (accs != null)
        {
            UNIACCOUNT set = accs[0];
            set.szMSN = openid;
            set.szPasswd = null;
            UpdateAcc(set);
        }
        else
        {
            ErrMsg("获取账户失败");
        }
    }

    private void ApplyUseRole()
    {
        string labid = Request["lab_id"];
        string roleid = Request["role_id"];
        string applyid = Request["apply_id"];
        string upFile = Request["up_file"];
        if (string.IsNullOrEmpty(roleid))
        {
            ErrMsg("参数有误");
            return;
        }
        SFROLEINFO para = new SFROLEINFO();
        if (!string.IsNullOrEmpty(labid))
            para.dwLabID = ToUInt(labid);
        para.dwSFRuleID = ToUInt(roleid);
        if (!string.IsNullOrEmpty(applyid) && applyid.Trim() != "0")
            para.dwApplyID = ToUInt(applyid);
        if (!string.IsNullOrEmpty(upFile))
            para.szApplyURL = upFile;
        para.dwAccNo = curAcc.dwAccNo;
        para.dwTargetID = curAcc.dwAccNo;
        para.szTargetName = curAcc.szTrueName;
        para.szTrueName = curAcc.szTrueName;
        para.dwAuthType = (uint)SYSFUNCRULE.DWAUTHTYPE.AUTHBY_USER;
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

    private void AssignTutor(uint ckStat)
    {
        if (UpdateTutor(ToUInt(Request["stu_accno"]), Request["stu_name"], ckStat))
        {
            SucMsg();
            if (Request["rtest"] == "auto")
            {
                RESEARCHTEST setvalue = new RESEARCHTEST();
                REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
                string name = curAcc.szTrueName;
                if (curAcc.dwTutorID != null && curAcc.dwTutorID != 0)
                {
                    setvalue.dwHolderID = curAcc.dwTutorID;
                    setvalue.szHolderName = curAcc.szTutorName;
                    name += "_" + curAcc.szTutorName;
                }
                else
                {
                    setvalue.dwHolderID = curAcc.dwAccNo;
                    setvalue.szHolderName = curAcc.szTrueName;
                }
                setvalue.szRTName = name + "科研课题";
                setvalue.szLeaderName = curAcc.szTrueName;
                setvalue.dwLeaderID = curAcc.dwAccNo;
                setvalue.dwRTLevel = (uint)RESEARCHTEST.DWRTLEVEL.RTLEVEL_OTHER;
                setvalue.dwRTKind=(curAcc.dwIdent&(uint)UNIACCOUNT.DWIDENT.EXTIDENT_OUTER)>0?
                    ((uint)RESEARCHTEST.DWRTKIND.RTKIND_OUTER | (uint)RESEARCHTEST.DWRTKIND.RTKIND_OUTSIDE) : ((uint)RESEARCHTEST.DWRTKIND.RTKIND_RTASK | (uint)RESEARCHTEST.DWRTKIND.RTKIND_INNER);
                uint groupId = NewGroup(setvalue.szRTName + "项目组", curAcc.szLogonName);
                setvalue.dwGroupID = groupId;
                uResponse = m_Request.Reserve.SetResearchTest(setvalue, out setvalue);
            }
        }
        else
        {
            ErrMsg();
        }

    }
    private void GetAccByAccNo(string accno, uint? ident)
    {
        GetAcc(accno, 3, ident);
    }
    private void GetAccByName(string name, uint? ident)
    {
        GetAcc(name, 2, ident);
    }
    private void GetAccById(string id, uint? ident)
    {
        GetAcc(id, 1, ident);
    }
    private void GetAcc(string key, int type, uint? ident)
    {
        ACCREQ req = new ACCREQ();
        UNIACCOUNT[] rlt;
        if (string.IsNullOrEmpty(key))
        {
            ErrMsg();
            return;
        }
        if (type == 1)
        {
            req.szPID = key;
        }
        else if (type == 2)
        {
            req.szTrueName = key;
        }
        else if (type == 3)
        {
            req.dwAccNo = ToUInt(key);
        }
        if (ident != null && ident != 0)
        {
            req.dwIdent = ident;
        }
        if (m_Request.Account.Get(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            if (rlt.Length > 0)
            {
                SucRlt(ToAccList(rlt));
            }
            else
            {
                ErrMsg("获取账户失败，请确认输入内容是否正确！");
            }
        }
        else
        {
            ErrMsg(m_Request.szErrMessage);
        }
    }
    //更新当前用户（必须已登录）
    private void UpdateAcc(UNIACCOUNT acc)
    {
        if (m_Request.Account.Set(acc, out acc) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            //由于acc仅返回修改过的字段，不得不遍历acc把修改字段手动更新到curAcc 
            //临时方法，若能返回完整acc，可直接把acc赋值curAcc
            object obj_set = curAcc;
            System.Reflection.FieldInfo[] infos = typeof(UNIACCOUNT).GetFields();
            for (int i = 0; i < infos.Length; i++)
            {
                System.Reflection.FieldInfo info = infos[i];
                object value = info.GetValue(acc);
                if (value != null && value.ToString() != "0")
                {
                    info.SetValue(obj_set, value);
                }
            }
            curAcc = (UNIACCOUNT)obj_set;
            //////////
            curAcc.szPasswd = null;
            Session["LOGIN_ACCINFO"] = curAcc;
            SucMsg();
        }
        else
        {
            ErrMsg(m_Request.szErrMessage);
        }
    }
    private void UpAccWithTutor(uint ckStat)
    {
        UNIACCOUNT set = new UNIACCOUNT();
        set.szHandPhone = Request["phone"];
        set.szEmail = Request["email"];
        //修改导师
        if (!string.IsNullOrEmpty(Request["stu_accno"]))
        {
            if (!UpdateTutor(ToUInt(Request["stu_accno"]), Request["stu_name"], ckStat))
            {
                ErrMsg("修改导师时出现异常！");
                return;
            }
        }
        UpdateAcc(set);
    }
    private bool UpdateTutor(uint accno, string name, uint ckStat)
    {
        string auto = ConfigurationManager.AppSettings["autoToTutor"];//若教师不是导师则自动转为导师
        if (auto == null || auto == "1")
        {
            UNIACCOUNT[] tmp = GetAccByAccNo(accno.ToString());
            if (tmp != null && tmp.Length > 0)
            {
                UNIACCOUNT acc = tmp[0];
                if (!IsStat(acc.dwIdent, (uint)UNIACCOUNT.DWIDENT.EXTIDENT_TUTOR))
                {
                    EXTIDENTACC para = new EXTIDENTACC();
                    para.dwAccNo = accno;
                    para.szTrueName = name;
                    para.dwIdent = (uint)UNIACCOUNT.DWIDENT.EXTIDENT_TUTOR;
                    m_Request.Account.ExtIdentAccSet(para);
                }
            }
        }
        //临时 如果已有导师则解除关系
        if (curAcc.dwTutorID != null && curAcc.dwTutorID > 0)
        {
            TUTORSTUDENT para = new TUTORSTUDENT();
            para.dwTutorID = curAcc.dwTutorID;
            para.dwAccNo = curAcc.dwAccNo;
            m_Request.Account.TutorStudentDel(para);
        }
        ////////
        TUTORSTUDENT set = new TUTORSTUDENT();
        set.dwTutorID = accno;
        set.szTutorName = name;
        set.dwAccNo = curAcc.dwAccNo;
        set.szTrueName = curAcc.szTrueName;
        if (ckStat != 0)
        {
            set.dwStatus = ckStat;
        }
        if (m_Request.Account.TutorStudentSet(set) != REQUESTCODE.EXECUTE_SUCCESS)
        {
            return false;
        }
        //更新session
        curAcc.dwTutorID = accno;
        curAcc.szTutorName = name;
        Session["LOGIN_ACCINFO"] = curAcc;
        return true;
    }
    private void TutorRel()
    {
        if (!IsStat(curAcc.dwIdent, (uint)UNIACCOUNT.DWIDENT.EXTIDENT_TUTOR))
        {
            ErrMsg("你不是导师，不能操作！");
            return;
        }
        uint? key;
        if (Request["stu_id"] != null)
        {

            key = GetAccNoById(Request["stu_id"]);
        }
        else
        {
            key = Convert.ToUInt32(Request["stu_accno"]);
        }
        string order = Request["order"];
        //清除申请
        if (order == "del")
        {
            TUTORSTUDENT vrDel = new TUTORSTUDENT();
            vrDel.dwAccNo = key;
            vrDel.dwTutorID = curAcc.dwAccNo;
            if (m_Request.Account.TutorStudentDel(vrDel) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                SucMsg();
            }
            else
            {
                ErrMsg(m_Request.szErrMsg);
            }
            return;
        }
        TUTORSTUDENTCHECK vrSet = new TUTORSTUDENTCHECK();
        //否认关系
        if (order == "fail")
        {
            vrSet.dwCheckStat = (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL;
        }
        //确认关系
        else if (order == "ok")
        {
            vrSet.dwCheckStat = (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK;
        }
        vrSet.dwStudentAccNo = key;
        vrSet.szStudentName = Request["stu_name"];
        vrSet.dwTutorID = curAcc.dwAccNo;
        if (m_Request.Account.TutorStudentCheck(vrSet) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            SucMsg();
            //若flw_handle为add，成员自动加入导师所有项目组
            if (Request["flw_handle"] != null && Request["flw_handle"] == "add")
            {
                RESEARCHTESTREQ vrRt = new RESEARCHTESTREQ();
                vrRt.dwHolderID = curAcc.dwAccNo;
                RESEARCHTEST[] vtRst;
                m_Request.Reserve.GetResearchTest(vrRt, out vtRst);
                if (vtRst != null && vtRst.Length > 0)
                {
                    string accno = Request["stu_accno"];
                    for (int i = 0; i < vtRst.Length; i++)
                    {
                        string group = vtRst[i].dwGroupID.ToString();
                        if (order == "ok")
                        {
                            //加入项目成员组
                            AddMemByAccNo(group, accno);
                        }
                        else if (order == "fail")
                        {
                            //移除项目成员组
                            DelMemByAccNo(group, accno);
                        }
                    }
                }
            }
        }
        else
        {
            ErrMsg(m_Request.szErrMessage);
        }
    }

    private List<uniacc> ToAccList(UNIACCOUNT[] accs)
    {
        if (accs == null)
        {
            return new List<uniacc>();
        }
        List<uniacc> list = new List<uniacc>();
        for (int i = 0; i < accs.Length; i++)
        {
            uniacc a = new uniacc();
            a.id = accs[i].szLogonName;
            a.accno = accs[i].dwAccNo.ToString();
            a.name = accs[i].szTrueName;
            a.phone = accs[i].szHandPhone;
            a.email = accs[i].szEmail;
            a.ident = accs[i].dwIdent.ToString();
            a.dept = accs[i].szDeptName;
            a.tutor = accs[i].szTutorName;
            a.tutorid = accs[i].dwTutorID;
            list.Add(a);
        }
        return list;
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
        public string tutor;
        public uint? tutorid;
    }
}
