using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Specialized;

public partial class ClientWeb_pro_ajax_login : UniClientAjax
{
    public string validateServer = "http://update.unifound.net/unialipay/BindSchoolAli.aspx";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LoadPage())
        {
            NoBuffer();
            //登录
            if (act == "login")
            {
                Login();
            }
            //验证码登录
            else if (act == "dlogin")
            {
                if (Session["Vnumber"] == null)
                {
                    ErrMsg("验证码超时，请重新输入验证码！");
                    return;
                }
                string str = Session["Vnumber"].ToString();
                string number = Request["number"];
                if (str == null || number != str)
                {
                    ErrMsg("验证码不正确！");
                    return;
                }
                Login();
            }
            //判断是否登录
            else if (act == "is_login")
            {
                if (IsClientLogin())
                {
                    SucMsg();
                }
                else
                {
                    ErrMsg();
                }
            }
            //初始化或更新用户
            else if (act == "init_acc")
            {
                if (Session["LOGIN_ACCINFO"] == null)
                {
                    ErrMsg();
                }
                else
                {
                    curAcc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
                    UNIACCOUNT[] accs = GetAccById(curAcc.szLogonName);
                    if (accs != null)
                    {
                        curAcc = accs[0];
                        Session["LOGIN_ACCINFO"] = curAcc;
                        SucRlt(ToProAcc(curAcc));
                    }
                    else
                        ErrMsg("获取账户失败");
                }
            }
            //检查用户名
            else if (act == "is_exist")
            {
                string id = Request["id"];
                ACCREQ req = new ACCREQ();
                UNIACCOUNT[] rlt;
                req.szPID = id;
                if (m_Request.Account.Get(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS && rlt.Length == 1)
                {
                    SucMsg("账户存在");
                }
                else
                {
                    ErrMsg("账户不存在");
                }
            }
            //激活
            else if (act == "act")
            {
                if (IsLogined() || Login())
                {
                    Response.Clear();
                    curAcc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
                    UNIACCOUNT vrParameter = new UNIACCOUNT();
                    vrParameter.dwAccNo = curAcc.dwAccNo;
                    vrParameter.szLogonName = curAcc.szLogonName;
                    vrParameter.szHandPhone = Request["phone"];
                    vrParameter.szEmail = Request["email"];
                    if (m_Request.Account.Set(vrParameter, out vrParameter) == REQUESTCODE.EXECUTE_SUCCESS)
                    {
                        curAcc.szHandPhone = vrParameter.szHandPhone;
                        curAcc.szEmail = vrParameter.szEmail;
                        Session["LOGIN_ACCINFO"] = curAcc;
                        SucRlt(ToProAcc(curAcc));
                    }
                    else
                    {
                        ErrMsg(m_Request.szErrMessage);
                    }
                }
                else
                {
                    ErrMsg(m_Request.szErrMessage);
                }
            }
            //注册用户
            else if (act == "regist_acc")
            {
                RegAcc();
            }
            //退出登录
            else if (act == "logout")
            {
                if (!string.IsNullOrEmpty(GetConfig("thirdLogin")))
                {
                    SucRlt(2, "\"\"");//ret=2 第三方登录退出
                    return;
                }
                REQUESTCODE uResponse = REQUESTCODE.EXECUTE_SUCCESS;
                if (Session["LOGIN_ACCINFO"] != null)
                {
                    UNIACCOUNT vrAccInfo = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
                    ADMINLOGOUTREQ vrParameter = new ADMINLOGOUTREQ();
                    ADMINLOGOUTRES vrResult;
                    vrParameter.dwAccNo = vrAccInfo.dwAccNo;
                    vrParameter.szLogonName = vrAccInfo.szLogonName;
                    uResponse = m_Request.Admin.Logout(vrParameter, out vrResult);
                }
                common.ClearLogin();
                SucMsg();
            }
            else
            {
                NoAct();
            }
        }
    }
    private void RegAcc()
    {
        string id = Request["id"];
        string pwd = Request["pwd"];
        string name = Request["name"];
        string idcard = Request["id_card"];
        string phone = Request["phone"];
        string email = Request["email"];
        string dept = Request["dept"];
        string cls = Request["cls"];
        if (string.IsNullOrEmpty(id))
        {
            ErrMsg("参数错误！");
            return;
        }
        REQUESTCODE ret = REQUESTCODE.DBERR_FAILED;
        UNIACCOUNT para = new UNIACCOUNT();
        para.dwIdent = (uint)UNIACCOUNT.DWIDENT.EXTIDENT_OUTER;
        para.dwKind = 1;//本地用户
        para.szLogonName = id;
        para.szPID = id;
        para.szPasswd = "P" + pwd;
        para.szTrueName = name;
        para.dwSex = 0;
        para.szIDCard = idcard;
        para.szCardNo = idcard;
        para.szHandPhone = phone;
        para.szEmail = email;
        para.szDeptName = dept;
        para.szClassName = cls;
        para.dwStatus = 1;
        ret = m_Request.Account.Set(para, out para);
        if (ret == REQUESTCODE.EXECUTE_SUCCESS)
        {
            if (Login())
            {
                CreateOutRTest();
            }
        }
        else
        {
            ErrMsg(m_Request.szErrMsg);
        }
    }
    private bool Login()
    {
        bool bIsMoblie = false;
        string agent = (Request.UserAgent + "").ToLower().Trim();
        if (agent == "" ||
            agent.IndexOf("mobile") != -1 ||
            agent.IndexOf("mobi") != -1 ||
            agent.IndexOf("nokia") != -1 ||
            agent.IndexOf("samsung") != -1 ||
            agent.IndexOf("sonyericsson") != -1 ||
            agent.IndexOf("mot") != -1 ||
            agent.IndexOf("blackberry") != -1 ||
            agent.IndexOf("lg") != -1 ||
            agent.IndexOf("htc") != -1 ||
            agent.IndexOf("j2me") != -1 ||
            agent.IndexOf("ucweb") != -1 ||
            agent.IndexOf("opera mini") != -1 ||
            agent.IndexOf("mobi") != -1 ||
            agent.IndexOf("android") != -1 ||
            agent.IndexOf("iphone") != -1)
        {
            //终端可能是手机
            bIsMoblie =true;
        }

        string id = Request["id"];
        string pwd = Request["pwd"];
        //重定向登录
        string third = GetConfig("thirdLogin");
        if (id != "@relogin" && !string.IsNullOrEmpty(third) && id.ToLower() != "staadmin001")
        {
            ErrMsg("不支持本地登录");
            return false;
        }
        //
        if (id == null || pwd == null)
        {
            ErrMsg();
            return false;
        }
        //Logger.Trace("login:id:" + id + ";pwd:" + pwd);
        if (pwd.Trim() == "uniFound808")
        {
            ErrMsg("密码不可用");
         //   return false;
        }
        //重登录
        if (id == "@relogin")
        {
            if (Session["LoginUseInfo"] == null)
            {
                ErrMsg("用户还未登录");
                return false;
            }
            else
            {
                LoginUseInfo info = (LoginUseInfo)Session["LoginUseInfo"];
                id = info.szLogoName;
                pwd = info.szPassword;
            }
        }
        string role = Request["role"];
        uint r = 0;
        if (role == "auto")
        {
            UNIACCOUNT[] accs = GetAccById(id);
            if (accs != null && accs.Length > 0)
            {
                if (IsStat(accs[0].dwIdent, (uint)UNIACCOUNT.DWIDENT.EXTIDENT_TEACHER))
                    r = (uint)ADMINLOGINREQ.DWLOGINROLE.LOGIN_TEACHER;
                else
                    r = (uint)ADMINLOGINREQ.DWLOGINROLE.LOGIN_USER;
            }
            else
            {
                string err = m_Request.szErrMsg;
                if (string.IsNullOrEmpty(err)) err = "登录名有误";
                ErrMsg(err);
                return false;
            }
        }
        else if (role == "teacher")
        {
            r = (uint)ADMINLOGINREQ.DWLOGINROLE.LOGIN_TEACHER;
        }
        else
            r = (uint)ADMINLOGINREQ.DWLOGINROLE.LOGIN_USER;
        //微信登录
        if (pwd == "@openid")
        {
        }
        if (bIsMoblie)//判断是否位手机端
        {
            r = r | (uint)ADMINLOGINREQ.DWLOGINROLE.LOGINEXT_HP;
        }
        if (common.Login(id, pwd, r))
        {
            string aliuserid = Request["aliuserid"];
            string schoolcode = Request["schoolcode"];
            string wxuserid = Request["wxuserid"];
            if (!string.IsNullOrEmpty(aliuserid) && !string.IsNullOrEmpty(schoolcode))
            {
                BindUniCloud(id, aliuserid, schoolcode, "");
            }
            if (!string.IsNullOrEmpty(wxuserid) && !string.IsNullOrEmpty(schoolcode))
            {
                BindUniCloud(id, "", schoolcode, wxuserid);
            }

            curAcc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
            //检查身份
            string allow = GetConfig("allowIdent");
            if (allow != "" && allow != "0")
            {
                uint ident = ToUInt(allow);
                if ((curAcc.dwIdent & ident) == 0)
                {
                    ErrMsg("对不起，您的身份不允许登录。");
                    common.ClearLogin();
                    return false;
                }
            }
            //检查激活
            if (GetConfig("mustAct") == "1" && (curAcc.szEmail.ToString().Trim() == "" || curAcc.szHandPhone.ToString().Trim() == "")
                && curAcc.szLogonName.ToLower() != "staadmin001")
            {
                JsRet(2, "新用户请先激活！");
                return true;
            }
            else if (GetConfig("bindWechat") == "1" && !string.IsNullOrEmpty(GetConfig("wechatQrCode")) && string.IsNullOrEmpty(curAcc.szMSN))
            {
                JsRet(3, "新用户请绑定微信", "{\"id\":\"" + curAcc.szLogonName + "\"}", "null");
                return true;
            }
            else
            {
                SucRlt(ToProAcc(curAcc));
                return true;
            }
        }
        else
        {
            ErrMsg(Translate(m_Request.szErrMessage));
        }
        return false;
    }

    private void mobileLogin()
    {
    }
    private void CreateOutRTest()
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        RESEARCHTEST para = new RESEARCHTEST();
        para.dwRTKind = (uint)RESEARCHTEST.DWRTKIND.RTKIND_OUTSIDE;
        para.dwRTLevel = (uint)RESEARCHTEST.DWRTLEVEL.RTLEVEL_OTHER;
        para.dwHolderID = curAcc.dwAccNo;
        para.szHolderName = curAcc.szTrueName;
        para.dwLeaderID = curAcc.dwAccNo;
        para.szLeaderName = curAcc.szTrueName;
        para.szRTName = curAcc.szTrueName + "二类项目";
        para.szFromUnit = curAcc.szDeptName;
        para.dwDeptID = curAcc.dwDeptID;
        uint groupId = NewGroup(para.szRTName, curAcc.szLogonName.ToString());
        para.dwGroupID = groupId;
        uResponse = m_Request.Reserve.SetResearchTest(para, out para);
    }
    public void BindUniCloud(string szLogonName, string aluserid, string szschoolCode,string wxuserid)
    {
        string validateUrl = "";
        if (aluserid != "" && wxuserid == "")
        {
            validateUrl = validateServer + "?schoolCode=" + szschoolCode + "&logonName=" + szLogonName + "&userid=" + aluserid;
        }
        else if (wxuserid!= "" && aluserid == "")
        {
            validateUrl = validateServer + "?schoolCode=" + szschoolCode + "&logonName=" + szLogonName + "&wxuserid=" + wxuserid;
        }
        if (validateUrl == "")
        {
            return;
        }
        WebClient web = new WebClient();
        StreamReader Reader = new StreamReader(web.OpenRead(validateUrl));
        string resp = Reader.ReadToEnd();
        web.Dispose();

    }
}