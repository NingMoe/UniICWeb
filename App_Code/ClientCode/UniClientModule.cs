using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.IO;
using System.Collections;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Collections.Generic;
using UniStruct;
using Util;
using System.Diagnostics;


namespace UniWebLib
{
    public partial class UniClientCommon
    {
        public UniRequest m_Request = null;
        public UniConfig m_Config = new UniConfig();

        public UniClientCommon()
        {
        }
        ~UniClientCommon()
        {
        }

        public bool LoadPage()
        {
            GetRequest();
            return IsLogined();
        }
        public void MsgBox(string p, string script, Page page)
        {
            MsgBox(p, script, page, false);
        }
        public void MsgBox(string p, string script, Page page, bool isH)
        {
            if (p.IndexOf("网络连接失败") > -1)
            {
                isH = true;
            }
            else if (p.IndexOf("管理员未登录") > -1 || p.IndexOf("无法访问已释放的对象") > -1 || p.IndexOf("匿名用户不能进行本操作") > -1)
            {
                if (HttpContext.Current.Session["LoginUseInfo"] != null)//重登录
                {
                    HttpContext.Current.Session["LOGIN_ACCINFO"] = null;
                    IsLogined();
                }
                else
                    ClearLogin();
                isH = true;
                p = "登录超时，页面将重新加载。";
                script = "location.reload(); ";
            }
            else
            {
                p = ConvertMsg(p, ref script);
            }
            if (isH)
            {
                page.Response.Write("<script>if (typeof (uni) != 'undefined') uni.msgBox('" + p + "', '', function () { " + script + " }); else {alert('" + p + "');" + script + "};</script>");
            }
            else
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), "msg", "<script>if (typeof (uni) != 'undefined') uni.msgBox('" + p + "', '', function () { " + script + " }); else {window.onload = function () {alert('" + p + "');" + script + "}};</script>");
            }
        }
        public void MsgBoxH(string p, string script, Page page)//如异步加载的页面需用H弹框
        {
            MsgBox(p, script, page, true);
        }
        private string ConvertMsg(string msg, ref string script)
        {
            string p = msg;
            if (GetConfig("debugger") == "true")
            {
                string method = "";
                for (int i = 0; i < 6; i++)
                {
                    StackFrame frame = new StackFrame(3 - i);
                    if (frame.GetMethod() != null)
                        method += frame.GetMethod().Name + "+";
                }
                p += "debugger：" + method;
            }
            if (p == "") p = "无相关信息返回";
            else p = Regex.Replace(p, "[\"|\']+", "’");
            return p;
        }
        //未登录客户端跳转
        public bool ClientRedirect(string url, Page page)
        {
            bool ret = IsLogined();
            if (!ret)
                page.Response.Write("<script>if (typeof (uni) != 'undefined') uni.msgBoxRT('登录超时，将重新加载页面。', '超时提醒', '" + url + "'); else {alert('登录超时，将重新加载页面。'); window.top.location.href = '" + url + "';};</script>");
            return ret;
        }
        public UniRequest GetRequest()
        {
            if (m_Request != null)
            {
                return m_Request;
            }
            if (m_Config.m_bKeepLive)
            {
                m_Request = (UniRequest)HttpContext.Current.Session["UniRequest"];
                if (m_Request == null)
                {
                    m_Request = new UniRequest();
                    HttpContext.Current.Session["UniRequest"] = m_Request;
                    HttpContext.Current.Session.Timeout = m_Config.m_Timeout;
                }
            }
            else
            {
                m_Request = new UniRequest();
            }
            return m_Request;
        }
        public bool Login(string szLogonName, string szPassword)
        {
            return Login(szLogonName, szPassword, (uint)ADMINLOGINREQ.DWLOGINROLE.LOGIN_USER);
        }
        public bool Login(string szLogonName, string szPassword, uint role)
        {
            if (m_Request == null)
            {
                GetRequest();
            }
            ADMINLOGINREQ vrLogin = new ADMINLOGINREQ();
            ADMINLOGINRES vrLoginRes;
            vrLogin.szLogonName = szLogonName;
            vrLogin.szPassword = "P" + szPassword;
            if (role == 0) role = (uint)ADMINLOGINREQ.DWLOGINROLE.LOGIN_USER;
            if ((role & (uint)ADMINLOGINREQ.DWLOGINROLE.LOGINEXT_HP) == 0)//不含手机端登录则电脑端登录
                role = role | (uint)ADMINLOGINREQ.DWLOGINROLE.LOGINEXT_PC;
            vrLogin.dwLoginRole = role;
            vrLogin.szVersion = ((uint)ADMINLOGINREQ.SZVERSION.INTVER_MAIN).ToString() + "." + ((uint)ADMINLOGINREQ.SZVERSION.INTVER_RELEASE).ToString("00") + "." + ((uint)ADMINLOGINREQ.SZVERSION.INTVER_INTERNAL).ToString();
            vrLogin.szIP = GetRealIP();
            vrLogin.dwStaSN = 1;
            m_Request.m_UniDCom.StaSN = 1;
            m_Request.m_UniDCom.SessionID = 0;
            //Logger.Trace("user:" + szLogonName + "/" + szPassword + "");
            if (m_Request.Admin.StaLogin(vrLogin, out vrLoginRes) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                HttpContext.Current.Session["LoginRes"] = vrLoginRes;
                HttpContext.Current.Session["ADMINLOGINREQ"] = vrLogin;
                m_Request.m_UniDCom.SessionID = (uint)vrLoginRes.dwSessionID;
                m_Request.m_UniDCom.StaSN = 1;
                StaLogin();
                UNIACCOUNT vrAccInfo = vrLoginRes.AccInfo;
                vrAccInfo.szPasswd = null;
                if (szLogonName != "guest")
                {
                    LoginUseInfo info = new LoginUseInfo();
                    info.szLogoName = szLogonName;
                    info.szPassword = szPassword;
                    HttpContext.Current.Session["LoginUseInfo"] = info;
                    HttpContext.Current.Session["LOGIN_ACCINFO"] = vrAccInfo;
                }
                else
                {
                    HttpContext.Current.Session["LoginUseInfo"] = null;
                    HttpContext.Current.Session["LOGIN_ACCINFO"] = null;//记录guest
                }
                return true;
            }
            Logger.Trace("登录失败:" + m_Request.szErrMsg);
            ClearLogin();
            //HttpContext.Current.Session["LoginUseInfo"] = null;
            return false;
        }
        void StaLogin()
        {
            if (HttpContext.Current.Session["ADMINLOGINREQ"] != null)
            {
                ADMINLOGINREQ vrLogin = (ADMINLOGINREQ)HttpContext.Current.Session["ADMINLOGINREQ"];
                vrLogin.dwStaSN = m_Request.m_UniDCom.StaSN;

                ADMINLOGINRES vrLoginRes = new ADMINLOGINRES();
                m_Request.Admin.StaLogin(vrLogin, out vrLoginRes);
            }
        }
        //清除登录session
        public void ClearLogin()
        {
            if (m_Request != null)
            {
                m_Request.m_UniDCom.SessionID = 0;
            }
            HttpContext.Current.Session["LoginRes"] = null;
            HttpContext.Current.Session["StaInfo"] = null;
            HttpContext.Current.Session["LOGIN_ACCINFO"] = null;
            HttpContext.Current.Session["ADMINLOGINREQ"] = null;
            HttpContext.Current.Session["TutorInfo"] = null;
            HttpContext.Current.Session["CUR_DEV"] = null;
            HttpContext.Current.Session["LoginUseInfo"] = null;
        }
        //返回是否登录
        public bool IsLogined()
        {
            return IsLogined(null);
        }
        public bool IsLogined(uint? sys)
        {
            if (HttpContext.Current.Session["LoginUseInfo"] != null)
            {
                LoginUseInfo info = (LoginUseInfo)HttpContext.Current.Session["LoginUseInfo"];
                if (HttpContext.Current.Session["LOGIN_ACCINFO"] != null)
                {
                    UNIACCOUNT acc = (UNIACCOUNT)HttpContext.Current.Session["LOGIN_ACCINFO"];
                    if ((acc.szLogonName != null && info.szLogoName != null) && acc.szLogonName.ToLower() == info.szLogoName.ToLower())
                    {
                        if (sys == null)//快速返回
                        {
                            return true;
                        }
                        ADMINLOGINREQ logreq = (ADMINLOGINREQ)HttpContext.Current.Session["ADMINLOGINREQ"];
                        if (sys == (uint)UNISTATION.DWSUBSYSSN.SUBSYS_TEACHINGLAB)//教学系统
                        {
                            //ADMINLOGINRES res = (ADMINLOGINRES)HttpContext.Current.Session["LoginRes"];//(acc.dwIdent & 512) > 0 
                            if ((acc.dwIdent & (uint)UNIACCOUNT.DWIDENT.EXTIDENT_TEACHER) > 0 && ((uint)ADMINLOGINREQ.DWLOGINROLE.LOGIN_TEACHER & logreq.dwLoginRole) == 0)//教师非教师身份登录 重登录
                            {
                                return Login(info.szLogoName, info.szPassword, (uint)ADMINLOGINREQ.DWLOGINROLE.LOGIN_TEACHER);
                            }
                        }
                        else if (((uint)ADMINLOGINREQ.DWLOGINROLE.LOGIN_TEACHER & logreq.dwLoginRole) > 0)//非教学系统教师身份登录  重登录
                        {
                            return Login(info.szLogoName, info.szPassword);
                        }
                        return true;
                    }
                }
                return Login(info.szLogoName, info.szPassword);
            }
            if (HttpContext.Current.Session["LoginRes"] == null || (m_Request != null && m_Request.m_UniDCom != null && m_Request.m_UniDCom.SessionID == 0))
            {
                if (GetConfig("loginAll") != "1")
                {
                    //使用guest登录，并返回 false.
                    Login("guest", "");
                }
                return false;
            }
            else
            {
                if (HttpContext.Current.Session["LOGIN_ACCINFO"] == null)//guest
                {
                    return false;
                }
                return true;
            }
        }
        public bool IsLogined(HttpResponse Response, string url)
        {
            bool lg = IsLogined();
            if (lg)
                return true;
            else
            {
                Response.Redirect(url);
                return false;
            }
        }
        //语言翻译
        public string Translate(string key, string language)
        {
            Dictionary<string, string> dic = GetLanguageDic(language);
            if (dic != null && dic.ContainsKey(key))
            {
                return dic[key];
            }
            else
            {
                if (dic != null)
                {
                    foreach (KeyValuePair<string, string> pair in dic)
                    {
                        string szResTemp = GetSimilStr(key, pair.Key, pair.Value);
                        if (szResTemp != "")
                        {
                            return szResTemp;
                        }
                    }
                }
            }
             return key;


        }
        public string GetSimilStr(string szSer, string szXml, string szValue)
        {
            if (szValue.IndexOf("$") == -1)
            {
                return "";
            }
            string[] szList = szXml.Split('$');
            string[] szValueList = szValue.Split('$');
            bool bResSimel = true;
            for (int i = 0; i < szList.Length; i++)
            {
                if (!(szSer.Trim().IndexOf(szList[i].Trim()) > -1))
                {
                    bResSimel = false;
                    return "";
                }
            }
            for (int i = 0; i < szValueList.Length; i++)
            {
                try
                {
                    szSer = szSer.Replace(szList[i], szValueList[i]);
                }
                catch (Exception e)
                {
                }
            }
            return szSer;
        }
        //设置会话语言
        public bool SetLanguage(string language)
        {
            if (GetConfig("supMultilanguage") == "1" && !string.IsNullOrEmpty(language))
            {
                HttpContext.Current.Session["language"] = language;
                if (HttpRuntime.Cache.Get("Multilanguage_" + language) == null)
                    InitMultilanguage(language);
                return true;
            }
            return false;
        }
        //获取语言字典
        public Dictionary<string, string> GetLanguageDic(string language)
        {
            if (GetConfig("supMultilanguage") != "1") return null;
            if (string.IsNullOrEmpty(language))
            {
                if (HttpContext.Current.Session["language"] != null)
                    language = (string)HttpContext.Current.Session["language"];
                else
                    language = GetConfig("dftLanguage");
            }
            object lang = HttpRuntime.Cache.Get("Multilanguage_" + language);
            if (lang == null)
            {
                InitMultilanguage(language);
                lang = HttpRuntime.Cache.Get("Multilanguage_" + language);
            }
            return (Dictionary<string, string>)lang;
        }
        private void InitMultilanguage(string language)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string path = HttpContext.Current.Server.MapPath("~/ClientWeb/") + "language.xml";
            if (File.Exists(path))
            {
                try
                {
                    XmlDocument xml = new XmlDocument();
                    xml.Load(path);
                    XmlNode root = xml.SelectSingleNode("UNI");
                    XmlNode typeNode = root.SelectSingleNode(language);
                    //根据id搜索节点并返回内容
                    XmlNodeList nodes = typeNode.SelectNodes("item");
                    string str;
                    foreach (XmlNode item in nodes)
                    {
                        if (item.Attributes["key"] != null)
                        {
                            string key = item.Attributes["key"].Value;
                            if (!dic.ContainsKey(key))
                                dic.Add(key, item.InnerText);
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            HttpRuntime.Cache.Insert("Multilanguage_" + language, dic, new System.Web.Caching.CacheDependency(path));
        }
        protected string GetRealIP()
        {
            try
            {
                string ip = "";
                if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                {
                    ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                }
                else
                {
                    ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                return ip;
            }
            catch (Exception)//e)
            {
                //throw e;
            }
            return "";
        }
        public bool IsClientLogin()
        {
            if (!IsLogined())
            {
                ClearLogin();
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool IsClientLogin(HttpResponse Response, string url)
        {
            if (!IsLogined())
            {
                ClearLogin();
                Response.Redirect(url);
                return false;
            }
            else
            {
                return true;
            }
        }
        //获取配置
        public string GetConfig(string cfg)
        {
            string ret = ConfigurationManager.AppSettings[cfg];
            if (ret == null) return "";
            else return ret;
        }
        //类型转换
        public uint ToUInt(object obj)
        {
            try
            {
                return Convert.ToUInt32(obj);
            }
            catch (Exception)
            {

                return 0;
            }
        }
        //转换用户信息为前端结构
        public proacc ToProAcc(UNIACCOUNT acc)
        {
            if (m_Request == null)
            {
                GetRequest();
            }
            proacc a = new proacc();
            a.id = acc.szLogonName;
            a.accno = acc.dwAccNo.ToString();
            a.name = acc.szTrueName;
            a.phone = acc.szHandPhone;
            a.email = acc.szEmail;
            a.msn = acc.szMSN;
            a.ident = acc.dwIdent.ToString();
            a.dept = acc.szDeptName;
            a.deptid = acc.dwDeptID.ToString();
            a.cls = acc.szClassName;
            a.clsid = acc.dwClassID.ToString();
            a.receive = (acc.dwKind & (uint)UNIACCOUNT.DWKIND.EXTKIND_NOMSG) == 0;
            if ((ToUInt(GetConfig("proTarget")) & 4) > 0)//需要科研信息
            {
                a.tsta = GetTutorCheckStatus(acc, HttpContext.Current.Session);
                a.rtsta = GetRtestStatus(acc);
                a.pro = GetProject(acc);
            }
            GetCredit(ref a, acc);
            ADMINLOGINRES res = (ADMINLOGINRES)HttpContext.Current.Session["LoginRes"];
            a.role = res.dwManRole.ToString();
            return a;
        }
        private void GetCredit(ref proacc acc, UNIACCOUNT curAcc)
        {
            MYCREDITSCOREREQ req = new MYCREDITSCOREREQ();
            //req.dwAccNo = curAcc.dwAccNo;
            MYCREDITSCORE[] rlt;
            REQUESTCODE cd = m_Request.System.MyCreditScoreGet(req, out rlt);
            if (cd == REQUESTCODE.EXECUTE_SUCCESS && rlt.Length > 0)
            {
                List<string[]> list = new List<string[]>();
                for (int i = 0; i < rlt.Length; i++)
                {
                    MYCREDITSCORE score = rlt[i];
                    uint? leftCScore = score.dwLeftCScore;
                    if (score.dwLeftCScore > score.dwMaxScore)
                    {
                        leftCScore = score.dwMaxScore;
                    }
                    if (i == 0)
                    {
                        acc.score = (int)leftCScore;
                        if (leftCScore == 0 && score.dwForbidUseTime != null)
                        {
                            acc.score = -(int)score.dwForbidUseTime;
                        }
                    }
                    string forbid = leftCScore == 0 ? Util.Converter.UintToDateStr(score.dwForbidStartDate) + Translate("至", null) + Util.Converter.UintToDateStr(score.dwForbidEndDate) : "";
                    list.Add(new string[] { score.szCTName, ToInt64(leftCScore).ToString(), score.dwMaxScore.ToString(), forbid });
                }
                acc.credit = list.ToArray();
            }
            else
            {
                acc.credit = new string[][] { };
                acc.score = 0;
            }
        }
        private string GetTutorCheckStatus(UNIACCOUNT curAcc, System.Web.SessionState.HttpSessionState Session)
        {
            Session["TutorInfo"] = null;
            if (Session["LOGIN_ACCINFO"] == null)
            {
                return "5";
            }
            if ((curAcc.dwIdent & (uint)UNIACCOUNT.DWIDENT.EXTIDENT_TUTOR) > 0)
            {
                return "0";//导师
            }
            //获取导师
            TUTORREQ vrPra = new TUTORREQ();
            vrPra.dwStudentAccNo = curAcc.dwAccNo;
            UNITUTOR[] vrTutor;
            if (m_Request.Account.TutorGet(vrPra, out vrTutor) == REQUESTCODE.EXECUTE_SUCCESS && vrTutor != null && vrTutor.Length > 0)
            {
                //获取状态
                TUTORSTUDENTREQ vrStuGet = new TUTORSTUDENTREQ();
                vrStuGet.dwTutorID = vrTutor[0].dwAccNo;
                TUTORSTUDENT[] vrStu;
                if (m_Request.Account.TutorStudentGet(vrStuGet, out vrStu) == REQUESTCODE.EXECUTE_SUCCESS && vrStu != null)
                {
                    for (int i = 0; i < vrStu.Length; i++)
                    {
                        if (vrStu[i].dwAccNo == curAcc.dwAccNo)
                        {
                            Session["TutorInfo"] = vrStu[i];
                            if ((vrStu[i].dwStatus & (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK) > 0)
                            {
                                return "4";//审核通过
                            }
                            else if ((vrStu[i].dwStatus & (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL) > 0)
                            {
                                return "3";//审核未通过
                            }
                            else
                            {
                                return "2";//未审核
                            }
                        }
                    }
                }
                return "5";//获取导师审核状态出现异常
            }
            else if (vrTutor != null && vrTutor.Length == 0)
            {
                return "1";//未指定导师
            }
            else
            {
                return "5";//未获取导师审核状态
            }
        }
        private string GetRtestStatus(UNIACCOUNT curAcc)
        {
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            RESEARCHTESTREQ vrGet = new RESEARCHTESTREQ();
            vrGet.dwMemberID = curAcc.dwAccNo;
            RESEARCHTEST[] rlt;
            uResponse = m_Request.Reserve.GetResearchTest(vrGet, out rlt);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && rlt != null && rlt.Length > 0)
            {
                //临时方法，查询成员状态
                for (int i = 0; i < rlt.Length; i++)
                {

                    RTMEMBER[] mbs = rlt[i].RTMembers;
                    for (int j = 0; j < mbs.Length; j++)
                    {
                        if (mbs[j].dwAccNo == curAcc.dwAccNo && ((mbs[j].dwStatus & 2) > 0))
                        {
                            return "1";//有课题
                        }
                    }
                }
            }
            return "0";
        }
        private string GetProject(UNIACCOUNT curAcc)
        {
            string pro = "0";
            if ((curAcc.dwIdent & (uint)UNIACCOUNT.DWIDENT.EXTIDENT_OUTER) > 0) return pro;
            RESEARCHTESTREQ vrGet = new RESEARCHTESTREQ();
            RESEARCHTEST[] vrResult;
            vrGet.dwLeaderID = curAcc.dwAccNo;
            m_Request.Reserve.GetResearchTest(vrGet, out vrResult);
            if (vrResult != null && vrResult.Length > 0)
            {
                pro = "1";//有负责项目
            }
            return pro;
        }


        //xml操作
        public XmlNodeList GetXMLConst(string dir, string name)
        {
            if (File.Exists(dir) && !string.IsNullOrEmpty(name))
            {
                try
                {
                    XmlDocument xml = new XmlDocument();
                    xml.Load(dir);
                    XmlNode root = xml.SelectSingleNode("const");
                    XmlNodeList nodes = root.SelectNodes("field");
                    foreach (XmlNode item in nodes)
                    {
                        if (item.Attributes["name"].Value == name)
                        {
                            return item.SelectNodes("option");
                        }
                    }
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }
        //值转换
        //无符号转有符号
        public Int64 ToInt64(uint? v)
        {
            Int64 test = (Int64)v;
            return test - ((test < Int32.MaxValue) ? 0 : UInt32.MaxValue);
        }
    }
    public partial class UniClientModule : System.Web.UI.UserControl
    {
        public UniClientCommon common = new UniClientCommon();
        public void LoadPage()
        {
            common.LoadPage();
        }
        public UniRequest m_Request
        {
            get
            {
                return GetRequest();
            }
        }
        public UniRequest GetRequest()
        {
            return common.GetRequest();
        }
        public string GetConfig(string cfg)
        {
            return common.GetConfig(cfg);
        }
        public bool Login(string szLogonName, string szPassword)
        {
            return common.Login(szLogonName, szPassword);
        }
        public bool IsLogined()
        {
            return common.IsLogined();
        }
        public bool IsLogined(string url)
        {
            return common.IsLogined(Response, url);
        }
        public bool IsClientLogin()
        {
            return common.IsClientLogin();
        }
        public bool IsClientLogin(string url)
        {
            return common.IsClientLogin(Response, url);
        }
        public void MsgBox(string p)
        {
            common.MsgBox(p, "", this.Page);
        }
        public void MsgBox(string p, string script)
        {
            common.MsgBox(p, script, this.Page);
        }
        public string GetTutorCheckStatus()
        {
            Session["TutorInfo"] = null;
            if (Session["LOGIN_ACCINFO"] == null)
            {
                return "5";
            }
            UNIACCOUNT vrAccInfo = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
            if ((vrAccInfo.dwIdent & (uint)UNIACCOUNT.DWIDENT.EXTIDENT_TUTOR) > 0)
            {
                return "0";//导师
            }
            //获取导师
            TUTORREQ vrPra = new TUTORREQ();
            vrPra.dwStudentAccNo = vrAccInfo.dwAccNo;
            UNITUTOR[] vrTutor;
            if (m_Request.Account.TutorGet(vrPra, out vrTutor) == REQUESTCODE.EXECUTE_SUCCESS && vrTutor != null && vrTutor.Length > 0)
            {
                //获取状态
                TUTORSTUDENTREQ vrStuGet = new TUTORSTUDENTREQ();
                vrStuGet.dwTutorID = vrTutor[0].dwAccNo;
                TUTORSTUDENT[] vrStu;
                if (m_Request.Account.TutorStudentGet(vrStuGet, out vrStu) == REQUESTCODE.EXECUTE_SUCCESS && vrStu != null)
                {
                    for (int i = 0; i < vrStu.Length; i++)
                    {
                        if (vrStu[i].dwAccNo == vrAccInfo.dwAccNo)
                        {
                            Session["TutorInfo"] = vrStu[i];
                            if ((vrStu[i].dwStatus & (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK) > 0)
                            {
                                return "4";//审核通过
                            }
                            else if ((vrStu[i].dwStatus & (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINFAIL) > 0)
                            {
                                return "3";//审核未通过
                            }
                            else
                            {
                                return "2";//未审核
                            }
                        }
                    }
                }
                return "5";//获取导师审核状态出现异常
            }
            else if (vrTutor != null && vrTutor.Length == 0)
            {
                return "1";//未指定导师
            }
            else
            {
                return "5";//未获取导师审核状态
            }
        }
        //翻译
        public string Translate(string key)
        {
            return common.Translate(key, null);
        }
        public string Translate(string key, string language)
        {
            
            return common.Translate(key, language);
        }
        //设置会话语言
        public bool SetLanguage(string language)
        {
            return common.SetLanguage(language);
        }
    }

    public partial class UniClientPage : System.Web.UI.Page
    {
        public UniClientCommon common = new UniClientCommon();
        private string xmlDir = "upload/info/xmlData/";//默认新加xml文件路径
        private string xmlFile = "ics_data";//默认xml文件名
        public bool LoadPage()
        {
            return common.LoadPage();
        }
        public UniRequest m_Request
        {
            get
            {
                return GetRequest();
            }
        }
        public UniRequest GetRequest()
        {
            return common.GetRequest();
        }
        public bool Login(string szLogonName, string szPassword)
        {
            return common.Login(szLogonName, szPassword);
        }
        public bool Login(string szLogonName, string szPassword, uint role)
        {
            return common.Login(szLogonName, szPassword, role);
        }
        public bool IsLogined()
        {
            return common.IsLogined();
        }
        public bool IsLogined(string url)
        {
            return common.IsLogined(Response, url);
        }
        public bool IsLogined(uint? sys)
        {
            return common.IsLogined(sys);
        }
        public bool Logout(uint? dwAccNo, string szLogonName)
        {
            ADMINLOGOUTREQ vrParameter = new ADMINLOGOUTREQ();
            ADMINLOGOUTRES vrResult;
            vrParameter.dwAccNo = dwAccNo;
            vrParameter.szLogonName = szLogonName;
            if (m_Request.Admin.Logout(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                common.ClearLogin();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsCheckLogin(string szURL, out string szLogonName, out string szPssWord)
        {
            szPssWord = "";
            string verify, strSysDatetime, jsName, strKey, verify1, userid, url;
            strKey = "zjtcm_zfsoft";
            verify = buildurl(szURL, "verify");
            strSysDatetime = buildurl(szURL, "strSysDatetime");
            jsName = buildurl(szURL, "jsName");
            userid = buildurl(szURL, "userName");
            szLogonName = userid;
            url = Server.UrlDecode(buildurl(szURL, "url"));
            verify1 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(userid + strKey + strSysDatetime + jsName, "MD5");
            if (verify1.Equals(verify))
            {
                szPssWord = "uniFound808";
                return true;
            }
            return false;
        }
        public string buildurl(string url, string param)
        {
            string url1 = url;
            if (url.IndexOf(param) > 0)
            {
                if (url.IndexOf("&", url.IndexOf(param) + param.Length) > 0)
                {
                    url1 = url.Substring(url.IndexOf(param) + param.Length + 1, url.IndexOf("&", url.IndexOf(param)) - url.IndexOf(param) - param.Length - 1);
                }
                else
                {
                    url1 = "";
                }
                return url1;
            }
            else
            {
                return url1;
            }
        }
        //获取配置
        public string GetConfig(string cfg)
        {
            return common.GetConfig(cfg);
        }
        //截取字符串
        public string CutStr(string str, int i)
        {
            if (str.Length > i)
            {
                str = str.Substring(0, i - 1) + "...";
            }
            return str;
        }
        public string CutStrT(string str, int i)
        {
            if (str.Length > i)
            {
                str = "<span title='" + str + "'>" + str.Substring(0, i - 1) + "...</span>";
            }
            return str;
        }
        //获取图片路径
        public List<string> GetSrcFromHtml(string str)
        {
            List<string> list = new List<string>();
            MatchCollection mths = Regex.Matches(str, "<IMG.*?src=\"(.*?.*?)\".*?>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            for (int i = 0; i < mths.Count; i++)
            {
                if (mths[i].Groups.Count > 1)
                {
                    string src = mths[i].Groups[1].ToString();
                    list.Add(src);
                }
            }
            return list;
        }
        //去除图片
        public string DelImgFromHtml(string str)
        {
            return Regex.Replace(str, "<IMG.*?src=\"(.*?.*?)\".*?>", "[" + Translate("图") + "]", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        }
        //转到上传文件路径
        public string ToUploadUrl(string str)
        {
            return ResolveClientUrl("~/ClientWeb/upload/") + str;
        }
        //获取根路径
        public string GetClientUrl()
        {
            return ResolveClientUrl("~/ClientWeb/");
        }
        //获取设备小尺寸图片路径 bySN
        public string GetImgS(uint? id)
        {
            //小尺寸图片带s
            return GetMedia(id, "s", "img");
        }
        //获取设备图片路径 bySN
        public string GetImg(uint? id)
        {
            return GetMedia(id, "", "img");
        }
        //获取视频路径
        public string GetVadio(UNIDEVICE dev)
        {
            return GetMedia(dev.dwDevSN, "", "vadio");
        }
        private string GetMedia(uint? id, string v, string type)
        {
            //文件名后缀
            string f = id + v;
            // 文件路径
            string path;
            //初始化文件路径
            string reldir = ResolveClientUrl("~/ClientWeb/upload/");
            string dft = ResolveClientUrl("~/ClientWeb/pro/dft/");
            string folder = "";
            //默认文件
            string ret = "";
            //初始化支持的文件格式
            List<string> list = new List<string>();
            if (type == "img")
            {
                ret = dft + "DevImg/dft.jpg";
                folder = "DevImg";
                list.Add(".jpg");
                list.Add(".gif");
                list.Add(".png");
            }
            else if (type == "vadio")
            {
                ret = dft + "DevVadio/mediaplayer.swf?file=dft.flv";
                folder = "DevVadio";
                list.Add(".flv");
            }
            reldir = reldir + folder + "/";
            //获取文件夹路径
            string dir = HttpContext.Current.Server.MapPath("~/ClientWeb/upload/" + folder);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
                return "";
            }
            path = dir + "\\" + f;

            for (int i = 0; i < list.Count; i++)
            {
                if (File.Exists(path + list[i]))
                {
                    string str = "";
                    if (list[i] == ".flv")
                        str = "mediaplayer.swf?file=";
                    ret = reldir + str + f + list[i];
                    break;
                }
            }
            return ret;
        }
        //xml 获取内容
        public string GetXmlContent(string id, string type, string fileName)
        {
            XmlCtrl.XmlInfo info = GetXmlInfo(id, type, fileName);
            if (info.id == null)
            {
                string[] tp = type.Split('_');
                if (GetConfig("isDemo") == "1")
                {
                    info = GetDftXmlInfo(tp[tp.Length - 1], "dft");
                    info.content = (info.content).Replace("flag_img_path/", GetClientUrl() + "pro/dft/");
                }
                else if (tp.Length == 2)
                {
                    if ((tp[1].IndexOf("seat") >= 0 && (ToUInt(GetConfig("editForSubsys"))&8)>0)||(tp[1].IndexOf("cpt") >= 0 && (ToUInt(GetConfig("editForSubsys"))&8)>0))
                    {
                        info = GetDftXmlInfo(tp[1], "dft");
                    }
                }
            }
            //else
            //    info.content = ConvertImgPath(info.content);
            return info.content;
        }
        //默认xml文件 获取内容
        public string GetXmlContent(string id, string type)
        {
            return GetXmlContent(id, type, xmlFile);
        }
        //xml 获取信息对象
        public XmlCtrl.XmlInfo GetXmlInfo(string id, string type, string fileName)
        {
            XmlCtrl ctrl = GetXmlCtrl(fileName);
            XmlCtrl.XmlInfo info = ctrl.GetXmlContent(id, type);
            info.content = ConvertImgPath(info.content);
            return info;
        }
        //转换内容图片路径
        public string ConvertImgPath(string con)
        {
            if (con == null) return "";
            string p = "src=\"[\\da-z\\./:_-]*upload/info/";
            Regex rgx = new Regex(p, RegexOptions.IgnoreCase);
            return rgx.Replace(con, "src=\"" + ToUploadUrl("info/"));
        }
        //xml 获取默认信息对象
        public XmlCtrl.XmlInfo GetDftXmlInfo(string id, string type)
        {
            XmlCtrl ctrl = GetXmlCtrl(xmlFile);
            XmlCtrl.XmlInfo info = ctrl.GetXmlContent(id, type);
            if (string.IsNullOrEmpty(info.content))
            {
                XmlCtrl ctrlr = GetDftXml();
                info = ctrlr.GetXmlContent(id, type);
            }
            info.content = ConvertImgPath(info.content);
            return info;
        }
        //默认xml文件 获取信息对象
        public XmlCtrl.XmlInfo GetXmlInfo(string id, string type)
        {
            return GetXmlInfo(id, type, xmlFile);
        }
        //xml 获取信息对象列表
        public XmlCtrl.XmlInfo[] GetXmlInfoList(string type, string fileName, int startline, int need, int start, int end, bool byContent)
        {
            XmlCtrl ctrl = GetXmlCtrl(fileName);
            return ctrl.GetXmlList(type, startline, need, start, end, byContent);
        }
        public XmlCtrl.XmlInfo[] GetXmlInfoList(string type, int need)
        {
            XmlCtrl ctrl = GetXmlCtrl(xmlFile);
            return ctrl.GetXmlList(type, need);
        }
        //xml存储 保存数据
        public bool SaveXmlData(string id, string data, string type, string fileName, string title, string attrs, string state)
        {
            XmlCtrl ctrl = GetXmlCtrl(fileName);
            return ctrl.SaveXmlData(id, data, type, title, attrs, state);
        }
        private XmlCtrl GetXmlCtrl(string fileName)
        {
            //获取数据文件路径
            string clientPath = GetClientUrl();
            string dir = Server.MapPath(clientPath + xmlDir);
            if (string.IsNullOrEmpty(fileName)) fileName = xmlFile;
            return new XmlCtrl(fileName, dir);
        }
        private XmlCtrl GetDftXml()
        {
            string dir = Server.MapPath(GetClientUrl() + "pro/dft/");
            return new XmlCtrl("dft_data", dir);
        }
        //默认xml文件 保存数据
        public bool SaveXmlData(string id, string data, string type)
        {
            return SaveXmlData(id, data, type, xmlFile, null, null, null);
        }
        public bool SaveXmlData(string id, string data, string type, string title)
        {
            return SaveXmlData(id, data, type, xmlFile, title, null, null);
        }
        public bool SaveXmlData(string id, string data, string type, string title, string attrs)
        {
            return SaveXmlData(id, data, type, xmlFile, title, attrs, null);
        }
        //默认xml文件 删除数据
        public bool DelXmlData(string id, string type, string fileName)
        {
            XmlCtrl ctrl = GetXmlCtrl(fileName);
            return ctrl.DelXmlInfo(id, type) == "ok";
        }
        public bool DelXmlData(string id, string type)
        {
            return DelXmlData(id, type, null);
        }
        public bool DelXmlData(string type)
        {
            return DelXmlData(null, type, null);
        }
        //类型转换
        public uint ToUInt(object obj)
        {
            try
            {
                return Convert.ToUInt32(obj);
            }
            catch (Exception)
            {

                return 0;
            }
        }
        //判断状态
        public bool IsStat(uint? i, uint sta)
        {
            if ((i & sta) > 0)
                return true;
            else
                return false;
        }
        //组相关操作
        public bool AlterGroup(string id, string addmemList, string delmemList)
        {
            if (addmemList.Substring(addmemList.Length - 1) == ",")
            {
                addmemList = addmemList.Substring(0, addmemList.Length - 1);
            }
            if (delmemList.Substring(delmemList.Length - 1) == ",")
            {
                delmemList = delmemList.Substring(0, delmemList.Length - 1);
            }
            //删除
            string[] memList = delmemList.Split(',');
            for (int i = 0; i < memList.Length; i++)
            {
                if (!DelMember(id, memList[i]))
                    return false;
            }
            //添加
            string szLogonNameList = addmemList;
            string[] szLogonName = szLogonNameList.Split(',');
            for (int i = 0; i < szLogonName.Length; i++)
            {
                if (!AddMember(id, szLogonName[i]))
                    return false;
            }
            return true;
        }
        //新建组
        public uint NewGroup(string szName, string memList)
        {
            return NewGroup(szName, memList, null, null, false);
        }
        public uint NewGroup(string szName, string memList, uint? min, uint? max)
        {
            return NewGroup(szName, memList, min, max, false);
        }
        public uint NewGroup(string szName, string memList, uint? min, uint? max, bool isAccno)
        {
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            UNIGROUP setGroup = new UNIGROUP();
            setGroup.dwKind = (uint)UNIGROUP.DWKIND.GROUPKIND_RERV;
            setGroup.szName = szName;
            if (min != null && min != 0) setGroup.dwMinUsers = min;
            if (max != null && max != 0) setGroup.dwMaxUsers = max;
            uResponse = m_Request.Group.SetGroup(setGroup, out setGroup);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && setGroup.dwGroupID != null)
            {
                string[] list = memList.Split(',');
                for (int i = 0; i < list.Length; i++)
                {
                    if (list[i].Trim() != "")
                    {
                        if (isAccno)
                            AddMemByAccNo(setGroup.dwGroupID.ToString(), list[i]);
                        else
                            AddMember(setGroup.dwGroupID.ToString(), list[i]);
                    }
                }
                return (uint)setGroup.dwGroupID;
            }
            return 0;
        }
        //获取组成员
        public GROUPMEMDETAIL[] GetMembers(uint? groupId)
        {
            return GetMembers(groupId, null);
        }
        //获取组成员
        public GROUPMEMDETAIL[] GetMembers(uint? groupId, uint? kind)
        {
            GROUPMEMDETAILREQ req = new GROUPMEMDETAILREQ();
            req.dwGroupID = groupId;
            if (kind != null && kind != 0)
                req.dwGroupKind = kind;
            req.dwReqProp = (uint)GROUPMEMDETAILREQ.DWREQPROP.GROUPMEMDETAILREQ_NEEDDEL;
            GROUPMEMDETAIL[] rlt;
            m_Request.Group.GetGroupMemDetail(req, out rlt);
            return rlt;
        }
        public bool AddMember(string groupId, string szLogoName)
        {
            return AddMember(groupId, szLogoName, (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL, "");
        }
        public bool AddMember(string groupId, string PID, uint? kind, string name)
        {
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            GROUPMEMBER setGroupMember = new GROUPMEMBER();
            if (string.IsNullOrEmpty(PID) || string.IsNullOrEmpty(groupId))
            {
                return false;
            }
            uint? mbId = ToUInt(PID);
            string memo = "";
            //添加个人
            if ((kind & (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL) > 0)
            {
                ACCREQ vrGetAcc = new ACCREQ();
                vrGetAcc.szPID = PID;
                UNIACCOUNT[] vrAccResult;
                uResponse = m_Request.Account.Get(vrGetAcc, out vrAccResult);
                if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrAccResult != null && vrAccResult.Length > 0)
                {
                    mbId = vrAccResult[0].dwAccNo;
                    name = vrAccResult[0].szTrueName;
                    memo = vrAccResult[0].szLogonName + ":" + vrAccResult[0].szTrueName;
                }
                else
                    return false;
            }
            else if ((kind & (uint)GROUPMEMBER.DWKIND.MEMBERKIND_CLASS) > 0)
            {
                memo = "班级:" + name;
            }
            setGroupMember.dwGroupID = Convert.ToUInt32(groupId);
            setGroupMember.dwKind = kind;
            setGroupMember.dwMemberID = mbId;
            setGroupMember.szName = name;
            setGroupMember.szMemo = memo;
            uResponse = m_Request.Group.SetGroupMember(setGroupMember);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
                return true;
            else
                return false;
        }
        public bool AddMemByAccNo(string groupId, string AccNo)
        {
            if (string.IsNullOrEmpty(groupId) || string.IsNullOrEmpty(AccNo))
            {
                return false;
            }
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            GROUPMEMBER setGroupMember = new GROUPMEMBER();
            ACCREQ vrGetAcc = new ACCREQ();
            vrGetAcc.dwAccNo = Convert.ToUInt32(AccNo);
            UNIACCOUNT[] vrAccResult;
            uResponse = m_Request.Account.Get(vrGetAcc, out vrAccResult);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrAccResult != null && vrAccResult.Length > 0)
            {
                setGroupMember.dwGroupID = Convert.ToUInt32(groupId);
                setGroupMember.dwKind = (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL;
                setGroupMember.dwMemberID = vrAccResult[0].dwAccNo;
                setGroupMember.szName = vrAccResult[0].szTrueName;
                setGroupMember.szMemo = vrAccResult[0].szLogonName + ":" + vrAccResult[0].szTrueName;
            }
            uResponse = m_Request.Group.SetGroupMember(setGroupMember);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DelMember(string groupId, string szLogoName)
        {
            return DelMember(groupId, szLogoName, (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL);
        }
        public bool DelMember(string groupId, string PID, uint? kind)
        {
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            GROUPMEMBER setGroupMember = new GROUPMEMBER();
            if (string.IsNullOrEmpty(PID) || string.IsNullOrEmpty(groupId))
            {
                return false;
            }
            uint? id = ToUInt(PID);
            if ((kind & (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL) > 0)
            {
                ACCREQ vrGetAcc = new ACCREQ();
                vrGetAcc.szPID = PID;
                UNIACCOUNT[] vrAccResult;
                uResponse = m_Request.Account.Get(vrGetAcc, out vrAccResult);
                if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrAccResult != null && vrAccResult.Length > 0)
                {
                    id = vrAccResult[0].dwAccNo;
                }
                else
                    return false;
            }
            setGroupMember.dwGroupID = Convert.ToUInt32(groupId);
            setGroupMember.dwMemberID = id;
            setGroupMember.dwKind = ToUInt(kind);
            uResponse = m_Request.Group.DelGroupMember(setGroupMember);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DelMemByAccNo(string groupId, string AccNo)
        {
            if (string.IsNullOrEmpty(groupId) || string.IsNullOrEmpty(AccNo))
            {
                return false;
            }
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            GROUPMEMBER setGroupMember = new GROUPMEMBER();
            setGroupMember.dwGroupID = Convert.ToUInt32(groupId);
            setGroupMember.dwMemberID = Convert.ToUInt32(AccNo);
            setGroupMember.dwKind = (uint)GROUPMEMBER.DWKIND.MEMBERKIND_PERSONAL;
            uResponse = m_Request.Group.DelGroupMember(setGroupMember);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool AddRTMember(string rtId, string groupId, string accno, string name)
        {
            if (string.IsNullOrEmpty(rtId) || string.IsNullOrEmpty(accno) || string.IsNullOrEmpty(name))
            {
                return false;
            }
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            RTMEMBER para = new RTMEMBER();
            para.dwRTID = ToUInt(rtId);
            para.dwGroupID = ToUInt(groupId);
            para.dwAccNo = ToUInt(accno);
            para.szTrueName = name;
            uResponse = m_Request.Reserve.SetRTMember(para);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DelRTMember(string rtId, string groupId, string accno)
        {
            if (string.IsNullOrEmpty(rtId) || string.IsNullOrEmpty(accno))
            {
                return false;
            }
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            RTMEMBER para = new RTMEMBER();
            para.dwRTID = ToUInt(rtId);
            para.dwGroupID = ToUInt(groupId);
            para.dwAccNo = ToUInt(accno);
            uResponse = m_Request.Reserve.DelRTMember(para);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //获取组成员数
        public int GetGroupMemCount(uint? groupId)
        {
            int cnt = 0;
            GROUPMEMDETAILREQ req = new GROUPMEMDETAILREQ();
            req.dwGroupID = groupId;
            GROUPMEMDETAIL[] rlt;
            if (m_Request.Group.GetGroupMemDetail(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                cnt = rlt.Length;
            }
            return cnt;
        }

        //获取账户
        public UNIACCOUNT[] GetAccByAccNo(string accno)
        {
            return GetAcc(accno, 3, 0);
        }
        public UNIACCOUNT[] GetAccByName(string name)
        {
            return GetAcc(name, 2, 0);
        }
        public UNIACCOUNT[] GetAccById(string id)
        {
            return GetAcc(id, 1, 0);
        }
        public string GetIdByAccNo(uint? accno)
        {
            UNIACCOUNT[] acc = GetAccByAccNo(accno.ToString());
            if (acc != null && acc.Length > 0)
            {
                return acc[0].szLogonName;
            }
            else
            {
                return "";
            }
        }
        public uint? GetAccNoById(string id)
        {
            UNIACCOUNT[] acc = GetAccById(id);
            if (acc != null && acc.Length > 0)
            {
                return acc[0].dwAccNo;
            }
            else
            {
                return null;
            }
        }
        private UNIACCOUNT[] GetAcc(string key, int type, uint? ident)
        {
            ACCREQ req = new ACCREQ();
            UNIACCOUNT[] rlt;
            if (string.IsNullOrEmpty(key))
            {
                return null;
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
            if (ident != 0)
            {
                req.dwIdent = ident;
            }
            if (m_Request.Account.Get(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                if (rlt.Length > 0)
                {
                    return rlt;
                }
            }
            return null;
        }
        //转换用户信息
        public proacc ToProAcc(UNIACCOUNT acc)
        {
            return common.ToProAcc(acc);
        }
        //获取用户权限
        public SFROLEINFO[] GetSFRole()
        {
            return GetSFRole(null, null, null, null, null);
        }
        public SFROLEINFO[] GetSFRole(uint? type, uint? labid, uint? targetid)
        {
            return GetSFRole(null, null, type, labid, targetid);
        }
        public SFROLEINFO[] GetSFRole(uint? scopeKind, uint? scopeId, uint? type, uint? labid, uint? targetid)
        {
            SFROLEINFOREQ req = new SFROLEINFOREQ();
            if (type != null && type != 0)
            {
                req.dwAuthType = type;
                if (IsStat(type, (uint)SYSFUNCRULE.DWAUTHTYPE.AUTHBY_USER))
                {
                    req.szReqExtInfo.szOrderKey = "szLabName";
                    req.szReqExtInfo.szOrderMode = "ASC";
                }
            }
            if (labid != null && labid != 0)
                req.dwLabID = labid;
            if (scopeKind != null && scopeKind != 0)
                req.dwScopeKind = scopeKind;
            if (scopeId != null && scopeId != 0)
                req.dwScopeID = scopeId;
            if (targetid != null && targetid != 0)
                req.dwTargetID = targetid;
            SFROLEINFO[] rlt;
            //if (Session["LOGIN_ACCINFO"] != null)
            //{
            //    UNIACCOUNT acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
            //    req.dwAccNo = acc.dwAccNo;
            //}
            m_Request.System.SFRoleGet(req, out rlt);
            return rlt;
        }
        public SFROLEINFO[] GetUseRole()
        {
            return GetSFRole((uint)SYSFUNCRULE.DWAUTHTYPE.AUTHBY_USER, null, null);
        }
        public SFROLEINFO[] GetUseRole(uint? scopeKind, uint? scopeId, uint? labid)
        {
            return GetSFRole(scopeKind, scopeId, (uint)SYSFUNCRULE.DWAUTHTYPE.AUTHBY_USER, labid, null);
        }
        public SFROLEINFO[] GetLabUseRole(uint? labid)
        {
            return GetSFRole((uint)SYSFUNCRULE.DWAUTHTYPE.AUTHBY_USER, labid, null);
        }
        public SFROLEINFO[] GetRTRole()
        {
            return GetSFRole((uint)SYSFUNCRULE.DWAUTHTYPE.AUTHBY_REARCHTEST, null, null);
        }
        public SFROLEINFO[] GetRTRole(uint? rtid)
        {
            return GetSFRole((uint)SYSFUNCRULE.DWAUTHTYPE.AUTHBY_REARCHTEST, null, rtid);
        }
        public SFROLEINFO[] GetLabRTRole(uint? labid, uint? rtid)
        {
            return GetSFRole((uint)SYSFUNCRULE.DWAUTHTYPE.AUTHBY_REARCHTEST, labid, rtid);
        }
        //获取用户所有信用记录
        public CREDITREC[] GetCreditRecByAccNo(uint? accno)
        {
            CREDITRECREQ req = new CREDITRECREQ();
            req.dwAccNo = accno;
            req.dwStartDate = ToUInt(DateTime.Now.AddYears(-4).ToString("yyyyMMdd"));
            req.dwEndDate = ToUInt(DateTime.Now.AddMonths(1).ToString("yyyyMMdd"));
            req.szReqExtInfo.szOrderKey = "dwOccurTime";
            req.szReqExtInfo.szOrderMode = "DESC";
            CREDITREC[] rlt;
            if (m_Request.System.CreditRecGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                return rlt;
            }
            return null;
        }
        //获取设备
        public UNIDEVICE[] GetDevById(string id)
        {
            DEVREQ req = new DEVREQ();
            req.dwDevID = ToUInt(id);
            UNIDEVICE[] dev;
            if (m_Request.Device.Get(req, out dev) == REQUESTCODE.EXECUTE_SUCCESS && dev.Length > 0)
                return dev;
            return null;
        }
        public UNIDEVICE GetDevBySN(uint? sn, string roomId)
        {
            DEVREQ req = new DEVREQ();
            req.dwDevSN = sn;
            req.szRoomIDs = roomId;
            UNIDEVICE[] rlt;
            m_Request.Device.Get(req, out rlt);
            if (rlt != null && rlt.Length > 0)
                return rlt[0];
            return new UNIDEVICE();
        }
        public UNIDEVKIND GetDevKind(uint? kindid)
        {
            DEVKINDREQ req = new DEVKINDREQ();
            req.dwKindID = kindid;
            UNIDEVKIND[] rlt;
            m_Request.Device.DevKindGet(req, out rlt);
            UNIDEVKIND kind = new UNIDEVKIND();
            if (rlt != null && rlt.Length > 0)
            {
                kind = rlt[0];
            }
            return kind;
        }
        public UNIDEVCLS[] GetDevCls(uint? classkind)
        {
            DEVCLSREQ req = new DEVCLSREQ();
            if (classkind != null && classkind != 0)
                req.dwKind = classkind;
            req.szReqExtInfo.szOrderKey = "szClassName";
            req.szReqExtInfo.szOrderMode = "ASC";
            UNIDEVCLS[] rlt;
            m_Request.Device.DevClsGet(req, out rlt);
            return rlt;
        }
        public UNILAB[] GetLab(uint? labid)
        {
            LABREQ req = new LABREQ();
            if (labid != null)
                req.dwLabID = labid;
            UNILAB[] rlt;
            m_Request.Device.LabGet(req, out rlt);
            return rlt;
        }
        public string GetRoomsFromResvDev(RESVDEV[] rsvdev)
        {
            string rooms = "";
            for (int i = 0; i < rsvdev.Length; i++)
            {
                if (i > 0 && rsvdev[i - 1].dwRoomID == rsvdev[i].dwRoomID) continue;
                if (rooms != "") rooms += ",";
                rooms += rsvdev[i].szRoomName;
            }
            return rooms;
        }
        public ROOMGROUP[] GetRoomGroup(uint? roomId, uint? num)
        {
            ROOMGROUPREQ req = new ROOMGROUPREQ();
            if (roomId != null && roomId != 0)
                req.dwRoomID = ToUInt(roomId);
            if (num != null && num != 0)
                req.dwRoomNum = ToUInt(num);
            ROOMGROUP[] rlt;
            m_Request.Device.RoomGroupGet(req, out rlt);
            return rlt;
        }
        //设备预约状态
        public DEVRESVSTAT[] GetDevRsvSta(string id)
        {
            return GetDevRsvSta(DateTime.Now.ToString("yyyyMMdd"), id);
        }
        public DEVRESVSTAT[] GetDevRsvSta(string date, string id)
        {
            return GetDevRsvSta(date, id, null);
        }
        public DEVRESVSTAT[] GetDevRsvSta(string date, string id, uint? purpose)
        {
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            DEVRESVSTATREQ req = new DEVRESVSTATREQ();
            req.dwDevID = ToUInt(id);
            req.szDates = date;
            if (purpose != null && purpose != 0)
                req.dwResvPurpose = purpose;
            else
                req.dwResvPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL | (uint)UNIRESERVE.DWPURPOSE.USEFOR_TEACHING | (uint)UNIRESERVE.DWPURPOSE.USEFOR_RESEACH | (uint)UNIRESERVE.DWPURPOSE.USEFOR_RESERVED | (uint)UNIRESERVE.DWPURPOSE.USEFOR_ACTIVITY;
            req.dwClassKind = (uint)(UNIDEVCLS.DWKIND.CLSKIND_COMPUTER | UNIDEVCLS.DWKIND.CLSKIND_COMMONS | UNIDEVCLS.DWKIND.CLSKIND_LOAN | UNIDEVCLS.DWKIND.CLSKIND_SEAT);
            req.szReqExtInfo.dwStartLine = 0;
            req.szReqExtInfo.dwNeedLines = 1;
            DEVRESVSTAT[] rlt;
            List<string> list = new List<string>();
            uResponse = m_Request.Device.GetDevResvStat(req, out rlt);
            return rlt;
        }
        public UNIRESVRULE GetDevRsvRule(string id)
        {
            DEVRESVSTAT[] rsvSta = GetDevRsvSta(id);
            if (rsvSta != null && rsvSta.Length > 0)
            {
                return rsvSta[0].szRuleInfo;
            }
            else
            {
                return new UNIRESVRULE();
            }
        }
        //获取参考费用
        public string GetRefFee(RTRESV resv)
        {
            uint isCheck = 0;
            if (((resv.dwProperty) & ((uint)UNIRESERVE.DWPROPERTY.RESVPROP_MANDO)) > 0)
            {
                isCheck = 1;
            }
            uint timeSpan = (uint)(resv.dwEndTime - resv.dwBeginTime);
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            FEEREQ vrGet = new FEEREQ();
            vrGet.dwFeeSN = resv.dwFeeSN;
            UNIFEE[] vtRes;
            uResponse = m_Request.Fee.Get(vrGet, out vtRes);
            double uFeeTotal = 0;
            double useFee = 0;
            double subFee = 0;
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
            {
                UNIFEE setFee = new UNIFEE();
                setFee = vtRes[0];
                FEEDETAIL[] vtFeeDetail = setFee.szFeeDetail;
                for (int i = 0; i < vtFeeDetail.Length; i++)
                {
                    FEEDETAIL fd = vtFeeDetail[i];
                    uint type = (uint)fd.dwFeeType;
                    uint fee = 0;
                    uint time = 1;
                    if (fd.dwUnitFee != null && fd.dwUnitTime != null && fd.dwUnitTime > 0)
                    {
                        fee = (uint)fd.dwUnitFee;
                        time = (uint)fd.dwUnitTime;
                    }
                    double fUint60 = double.Parse(((fee * 60 * 1.0) / (100 * time)).ToString("F2"));
                    double fTotalTemp = fUint60 * timeSpan / (60 * 60);

                    string szFeeTemp = ChinaRound(fTotalTemp, 2).ToString();
                    string szFeeUint = ((fee * 1.0) / 100 * 60 / time).ToString("0.00"); ;
                    if ((type == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_USEDEV))
                    {
                        useFee += ChinaRound(fTotalTemp, 2);
                    }
                    else if ((type == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_ENTRUST))
                    {
                        subFee += ChinaRound(fTotalTemp, 2) * isCheck;
                    }
                }
                uFeeTotal = useFee + subFee;
            }
            //样品费
            double splFee = 0;
            RESVSAMPLE[] spls = resv.ResvSample;
            if (spls != null)
            {
                for (int i = 0; i < spls.Length; i++)
                {
                    splFee += (float)(spls[i].dwUnitFee * spls[i].dwSampleNum);
                }
                splFee = ChinaRound((splFee / 100), 2);
            }

            string total = (uFeeTotal + splFee).ToString("F2");
            return "<span class='click' title='使用费：" + useFee + "元；代检费：" + subFee + "元；样品费：" + splFee + "元。'>" + total + "</span>";
        }
        //课题
        //获取课题列表，需要检测Session["LOGIN_ACCINFO"]
        public RESEARCHTEST[] GetrtList(string type, out string msg)
        {
            //获取项目列表
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            UNIACCOUNT acc = (UNIACCOUNT)Session["LOGIN_ACCINFO"];
            RESEARCHTESTREQ vrGet = new RESEARCHTESTREQ();
            if (type == "tutor")
            {
                if ((acc.dwIdent & (uint)UNIACCOUNT.DWIDENT.EXTIDENT_TUTOR) > 0)
                {
                    msg = "导师不显示";
                    return null;
                }
                if (Session["TutorInfo"] == null || ((TUTORSTUDENT)Session["TutorInfo"]).dwStatus != (uint)ADMINCHECK.DWCHECKSTAT.CHECKSTAT_ADMINOK)
                {
                    msg = "未获得导师批准";
                    return null;
                }
                vrGet.dwHolderID = ((TUTORSTUDENT)Session["TutorInfo"]).dwTutorID;
            }
            else if (type == "rtest")
            {
                vrGet.dwMemberID = acc.dwAccNo;
            }
            RESEARCHTEST[] vrResult;
            uResponse = m_Request.Reserve.GetResearchTest(vrGet, out vrResult);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vrResult != null)
            {
                msg = "ok";
                return vrResult;
            }
            else
            {
                msg = m_Request.szErrMsg;
                return null;
            }
        }
        //搜索条件获取课题
        public RESEARCHTEST[] GetRTestes(string id, string name, string holderId, string leaderId, string mbId)
        {
            RESEARCHTESTREQ vrGet = new RESEARCHTESTREQ();
            RESEARCHTEST[] vtResult;
            if (!string.IsNullOrEmpty(id))
            {
                vrGet.dwRTID = ToUInt(id);
            }
            if (!string.IsNullOrEmpty(name))
            {
                vrGet.szRTName = name;
            }
            if (!string.IsNullOrEmpty(holderId))
            {
                vrGet.dwMemberID = ToUInt(holderId);
                //vrGet.dwHolderID = ToUInt(holderId);
            }
            if (!string.IsNullOrEmpty(leaderId))
            {
                vrGet.dwLeaderID = ToUInt(leaderId);
            }
            if (!string.IsNullOrEmpty(mbId))
            {
                vrGet.dwMemberID = ToUInt(mbId);
            }
            vrGet.dwStatus = 0;
            m_Request.Reserve.GetResearchTest(vrGet, out vtResult);
            return vtResult;
        }
        //预约相关
        //删除普通预约
        public string DelReserve(string id)
        {
            return DelReserve(id, null);
        }
        public string DelReserve(string id, string type)
        {
            //先获取预约查看是否属于开放活动的预约 是则连带删除开放活动  耗费资源 应从服务端判断
            //RESVREQ vrGet = new RESVREQ();
            //vrGet.dwResvID = ToUInt(id);
            //vrGet.dwStatFlag = (uint)RESVREQ.DWSTATFLAG.STATFLAG_INUSE;
            //UNIRESERVE[] vtResv;
            //REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            //uResponse = m_Request.Reserve.Get(vrGet, out vtResv);
            //if (uResponse != REQUESTCODE.EXECUTE_SUCCESS || vtResv == null || vtResv.Length <= 0)
            //{
            //    return "找不到预约！";
            //}
            //if ((uint)vtResv[0].dwPurpose == (uint)UNIRESERVE.DWPURPOSE.USEFOR_ACTIVITY)
            //{
            //    string rsvId = vtResv[0].dwResvID.ToString();
            //    UNIACTIVITYPLAN[] plans = GetActivityByRsvId(rsvId);
            //    if (plans != null)
            //    {
            //        for (int i = 0; i < plans.Length; i++)
            //        {
            //            if (plans[i].dwResvID.ToString() == rsvId)
            //            {

            //                uResponse = m_Request.Reserve.DelActivityPlan(plans[i]);
            //            }
            //        }
            //    }
            //}
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            UNIRESERVE resv = new UNIRESERVE();
            if (type == "group")
                resv.dwResvGroupID = ToUInt(id);
            else
                resv.dwResvID = ToUInt(id);
            uResponse = m_Request.Reserve.Del(resv);
            if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
            {
                string error = m_Request.szErrMessage.ToString();
                return error;
            }
            else
            {
                return "ok";
            }
        }
        //获取预约审核状态
        public YARDRESVCHECKINFO[] GetResvCheckInfo(string resvId, out string ret)
        {
            YARDRESVCHECKINFOREQ req = new YARDRESVCHECKINFOREQ();
            if (!string.IsNullOrEmpty(resvId))
                req.dwResvID = ToUInt(resvId);
            req.szReqExtInfo.szOrderKey = "dwCheckStat";
            req.szReqExtInfo.szOrderMode = "DESC";
            YARDRESVCHECKINFO[] rlt;
            if (m_Request.Reserve.GetYardResvCheckInfo(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS && rlt != null)
            {
                ret = "ok";
            }
            else
            {
                ret = m_Request.szErrMsg;
            }
            return rlt;
        }
        //获取预约规则
        public UNIRESVRULE GetResvRuleBySN(uint? sn)
        {
            RESVRULEREQ req = new RESVRULEREQ();
            req.dwRuleSN = sn;
            UNIRESVRULE[] rlt;
            if (m_Request.Reserve.ResvRuleGet(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS && rlt.Length > 0)
            {
                return rlt[0];
            }
            return new UNIRESVRULE();
        }
        //获取活动安排
        public UNIACTIVITYPLAN[] GetActivityByRsvId(string szResvID)
        {
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            ACTIVITYPLANREQ vrGet = new ACTIVITYPLANREQ();
            vrGet.dwGetType = (uint)ACTIVITYPLANREQ.DWGETTYPE.ACTIVITYPLANGET_BYALL;
            UNIACTIVITYPLAN[] vtResult;
            uResponse = m_Request.Reserve.GetActivityPlan(vrGet, out vtResult);
            return vtResult;
        }
        //获取学期
        public UNITERM GetTerm(uint? year)
        {
            TERMREQ req = new TERMREQ();
            req.dwYearTerm = year;
            UNITERM[] rlt;
            m_Request.Reserve.GetTerm(req, out  rlt);
            if (rlt != null && rlt.Length > 0) return rlt[0];
            else return new UNITERM();
        }
        public UNITERM GetTerm()
        {
            TERMREQ req = new TERMREQ();
            UNITERM[] rlt;
            if (m_Request.Reserve.GetTerm(req, out  rlt) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                for (int i = 0; i < rlt.Length; i++)
                {
                    if ((rlt[i].dwStatus & (uint)UNITERM.DWSTATUS.TERMSTAT_FORCE) > 0)
                        return rlt[i];
                }
            }
            return new UNITERM();
        }
        //获取学期列表 不预先选定则selTerm为空
        public UNITERM[] InitTermList(out UNITERM curTerm, string selTerm)
        {
            curTerm = new UNITERM();
            curTerm.dwYearTerm = 0;
            curTerm.szMemo = "未选择学期";
            TERMREQ req = new TERMREQ();
            req.szReqExtInfo.szOrderKey = "dwYearTerm";
            req.szReqExtInfo.szOrderMode = "DESC";
            UNITERM[] rlt;
            if (m_Request.Reserve.GetTerm(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                uint? term = 0;
                if (!string.IsNullOrEmpty(selTerm)) term = ToUInt(selTerm);
                for (int i = 0; i < rlt.Length; i++)
                {
                    if (term == 0)
                    {
                        if ((rlt[i].dwStatus & (uint)UNITERM.DWSTATUS.TERMSTAT_FORCE) > 0)
                        {
                            curTerm = rlt[i];
                            break;
                        }
                    }
                    else if (term == rlt[i].dwYearTerm)
                    {
                        curTerm = rlt[i];
                        break;
                    }
                }
            }
            if (rlt == null || rlt.Length == 0) rlt = new UNITERM[0];
            else if (curTerm.dwYearTerm == 0) curTerm = rlt[0];
            return rlt;
        }
        //获取实验计划
        public UNITESTPLAN GetTestPlanByID(string planId)
        {
            TESTPLANREQ req = new TESTPLANREQ();
            req.dwGetType = (uint)TESTPLANREQ.DWGETTYPE.TESTPLANGET_BYID;
            req.szGetKey = planId;
            UNITESTPLAN[] rlt;
            m_Request.Reserve.GetTestPlan(req, out rlt);
            if (rlt != null && rlt.Length > 0)
            {
                return rlt[0];
            }
            return new UNITESTPLAN();
        }
        //获取实验项目
        public UNITESTITEM GetTestItemByID(string testId)
        {
            TESTITEMREQ req = new TESTITEMREQ();
            req.dwGetType = (uint)TESTITEMREQ.DWGETTYPE.TESTITEMGET_BYID;
            req.szGetKey = testId;
            UNITESTITEM[] rlt;
            m_Request.Reserve.GetTestItem(req, out rlt);
            if (rlt != null && rlt.Length > 0)
            {
                return rlt[0];
            }
            return new UNITESTITEM();
        }
        //获取项目成员预约状态
        public TESTITEMMEMRESV[] GetTestMemResv(uint? testId, uint? planId, uint? groupId, uint? resvHour)
        {
            TESTITEMMEMRESVREQ req = new TESTITEMMEMRESVREQ();
            req.dwTestItemID = testId;
            req.dwTestPlanID = planId;
            req.dwGroupID = groupId;
            if (resvHour != null)
                req.dwResvTestHour = resvHour;
            req.szReqExtInfo.szOrderKey = "dwResvTestHour";
            req.szReqExtInfo.szOrderMode = "ASC";
            TESTITEMMEMRESV[] rlt;
            if (m_Request.Reserve.GetTestItemMemResv(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                return rlt;
            }
            else
            {
                return null;
            }
        }
        //获取场馆预约
        public YARDRESV GetYardResvById(uint? id)
        {
            YARDRESVREQ req = new YARDRESVREQ();
            req.dwResvID = id;
            req.dwStatFlag = (uint)RESVREQ.DWSTATFLAG.STATFLAG_INUSE | (uint)RESVREQ.DWSTATFLAG.STATFLAG_OVER;
            YARDRESV[] rlt;
            if (m_Request.Reserve.GetYardResv(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                if (rlt.Length > 0) return rlt[0];
            }
            return new YARDRESV();
        }
        //转换
        //四舍五入
        public double ChinaRound(double value, int decimals)
        {
            if (value < 0)
            {
                return Math.Round(value + 5 / Math.Pow(10, decimals + 1), decimals, MidpointRounding.AwayFromZero);
            }
            else
            {
                return Math.Round(value, decimals, MidpointRounding.AwayFromZero);
            }
        }
        //空字符串转0
        public string ToZero(object obj)
        {
            if (obj == null || obj.ToString() == "")
            {
                return "0";
            }
            else
            {
                return obj.ToString();
            }
        }
        //分钟数转小时格式
        public string MinToHour(uint? i)
        {
            if (i == null) return "";
            return i > 60 ? ((i / 60) + "时" + (i % 60) + "分") : (i + "分钟");
        }
        //返回和1970的差距秒数
        public int Get1970Seconds(string Date)
        {
            return Util.Converter.Get1970Seconds(Date);
        }
        //根据差距秒数 算出现在是日期
        public string Get1970Date(int TotalSeconds)
        {
            return Util.Converter.Get1970Date(TotalSeconds);
        }
        public string Get1970Date(int TotalSeconds, string format)
        {
            return Util.Converter.Get1970Date(TotalSeconds, format);
        }
        public string Get1970Date(uint? TotalSeconds)
        {
            if (TotalSeconds == null) return "";
            return Util.Converter.Get1970Date((int)TotalSeconds);
        }
        public string Get1970Date(uint? TotalSeconds, string format)
        {
            if (TotalSeconds == null) return "";
            return Util.Converter.Get1970Date((int)TotalSeconds, format);
        }
        //日期字符串转连续数字
        public uint DateToUint(string date)
        {
            return Util.Converter.DateToUint(date);
        }
        //连续数字转日期字符串
        public string UintToDateStr(uint? date)
        {
            return Util.Converter.UintToDateStr(date);
        }
        public string ConvertStr(object obj)
        {
            if (obj == null)
            {
                return "";
            }
            else
            {
                return obj.ToString();
            }
        }
        //获取字典表内容
        public CODINGTABLE[] GetCodeTable(uint? type, string sn)
        {
            CODINGTABLEREQ req = new CODINGTABLEREQ();
            if (type != null && type != 0)
                req.dwCodeType = type;
            if (!string.IsNullOrEmpty(sn) && sn != "0")
                req.szCodeSN = sn;
            CODINGTABLE[] rlt;
            if (m_Request.System.GetCodingTable(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
                return rlt;
            else
                return null;
        }
        public bool IsClientLogin()
        {
            return common.IsClientLogin();
        }
        public bool IsClientLogin(string url)
        {
            return common.IsClientLogin(Response, url);
        }
        //提示框
        public void MsgBox(string p)
        {
            common.MsgBox(p, "", this.Page);
        }
        public void MsgBox(string p, string script)
        {
            common.MsgBox(p, script, this.Page);
        }
        public void MsgBoxH(string p)
        {
            common.MsgBoxH(p, "", this.Page);
        }
        public void MsgBoxH(string p, string script)
        {
            common.MsgBoxH(p, script, this.Page);
        }
        //未登录客户端跳转
        public bool ClientRedirect(string url)
        {
            return common.ClientRedirect(url, this.Page);
        }
        //未登录内嵌页面刷新
        public bool IsFrameLogin()
        {
            bool ret = IsClientLogin();
            if (!ret)
            {
                MsgBox("登录超时，请重新登录。", "CloseDlg();parent.location.reload();");
            }
            return ret;
        }
        //获取审核类型
        public CHECKTYPE[] GetCheckType(uint? kind, uint? main)
        {
            CHECKTYPEREQ req = new CHECKTYPEREQ();
            if (kind != null && kind != 0)
                req.dwCheckKind = kind;
            if (main != null && main != 0)
                req.dwMainKind = main;
            CHECKTYPE[] rlt;
            m_Request.Admin.CheckTypeGet(req, out rlt);
            if (rlt == null) rlt = new CHECKTYPE[0];
            return rlt;
        }
        //第三方  分页控件 未使用
        public unipctrl GetPCtrl()
        {
            //只可用于服务器模式
            unipctrl pc = new unipctrl();
            pc.name = "pCtrl";
            pc.start = 0;
            pc.need = 0;
            string name = Request["pCtrlName"];
            if (!string.IsNullOrEmpty(name))
            {
                pc.name = name;
                pc.start = Convert.ToInt32(Request[name + "_start"]);
                pc.need = Convert.ToInt32(Request[name + "_need"]);
            }
            return pc;
        }
        public void UpdatePageCtrl(int total, int start, int need)
        {
            UpdatePageCtrl(total, start, need, "pCtrl");
        }
        public void UpdatePageCtrl(int total, int start, int need, string name)
        {
            //只可用于服务器模式
            Response.Write("<script>window.onload=function(){updatePageCtrl(" + total + ", " + start + ", " + need + ",\"" + name + "\");};</script>");
        }
        //翻译
        public string Translate(string key)
        {
            return common.Translate(key, null);
        }
        public string Translate(string key, string language)
        {
            return common.Translate(key, language);
        }
        //设置会话语言
        public bool SetLanguage(string language)
        {
            return common.SetLanguage(language);
        }
    }

    public partial class UniClientMaster : System.Web.UI.MasterPage
    {
        public UniClientCommon common = new UniClientCommon();
        public void LoadPage()
        {
            common.LoadPage();
        }
        public UniRequest m_Request
        {
            get
            {
                return GetRequest();
            }
        }
        public string GetConfig(string cfg)
        {
            return common.GetConfig(cfg);
        }
        public void MsgBox(string p)
        {
            common.MsgBox(p, "", this.Page);
        }
        public void MsgBox(string p, string script)
        {
            common.MsgBox(p, script, this.Page);
        }
        public void ClientRedirect(string url)
        {
            common.ClientRedirect(url, this.Page);
        }
        public UniRequest GetRequest()
        {
            return common.GetRequest();
        }
        public bool Login(string szLogonName, string szPassword)
        {
            return common.Login(szLogonName, szPassword);
        }
        public bool IsLogined()
        {
            return common.IsLogined();
        }

        public bool IsClientLogin()
        {
            return common.IsClientLogin();
        }
        public bool IsClientLogin(string url)
        {
            return common.IsClientLogin(Response, url);
        }
        //翻译
        public string Translate(string key)
        {
            return common.Translate(key, null);
        }
        public string Translate(string key, string language)
        {
            return common.Translate(key, language);
        }
        //设置会话语言
        public bool SetLanguage(string language)
        {
            return common.SetLanguage(language);
        }
    }
    //分页
    public class unipctrl
    {
        public string name;
        public int total;
        public int start;
        public int need;
        public unipctrl()
        {
            name = "pCtrl";
            total = 0;
            start = 0;
            need = 0;
        }
    }
    //前端用户结构
    public struct proacc//定义同前端 pro.acc
    {
        public string id;
        public string accno;
        public string name;
        public string phone;
        public string email;
        public string msn;
        public string ident;
        public string dept;
        public string deptid;
        public string tutor;
        public string tutorid;
        public string cls;
        public string clsid;
        public bool receive;//接收提醒
        public string tsta;//导师状态
        public string rtsta;//课题状态
        public string pro;//项目状态
        public int score;//信用积分
        public string[][] credit;//信用记录
        public string role;
    }
}