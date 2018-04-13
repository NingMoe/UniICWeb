using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class Pages_SharkSign : UniPage
{
    public uint m_nType=0;
    public string m_szMsg= "";
    public uint m_szTimes = 0;
    public uint dwStaSN = 1;
    public string m_szTitle = "";

  // public uint? resvid = 0;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        string Openid = Request["Openid"];//oMi17t-JQ6xHDHqjw2R-KSpThfE8";
       string Distance = Request["Distance"]; //"0.1599477739542671";//Request["Distance"];
        string sid = Request["sid"];
        if (!string.IsNullOrEmpty(Openid))
        {
            Session["Openid"] = Openid;
        }
        if (!string.IsNullOrEmpty(Distance))
        {
            Session["Distance"] = Distance;
        }
        string sta = Request["sta"];
        if (string.IsNullOrEmpty(sta))
        {
            dwStaSN = 1;
        }
        else
        {
            dwStaSN = ToUint(sta);
        }
        m_Request.m_UniDCom.StaSN = dwStaSN;
        string status = Request["status"];
        if (status == "true")
        {
            CheckIn();
            return;
        }
        else if(status == "false")
        {
            ComeIn();
            return;
        }
         if (Session["Openid"]!=null)
        {
            if (Request["DoLogon"] == "true")
            {
                login();
            }
            else if(Request["DoUserOut"]!=null)//暂离                           //修改中
            {
                RESVUSERGOOUTREQ req = new RESVUSERGOOUTREQ();
                if (Session["resvid"]!=null)
                {
                    req.dwResvID = ToUint(Session["resvid"].ToString());
                    DEVREQ req2 = new DEVREQ();
                    req2.dwResvID = ToUint(Session["resvid"].ToString());
                    UNIDEVICE[] rlt2;
                    if (m_Request.Device.Get(req2, out rlt2) == REQUESTCODE.EXECUTE_SUCCESS && rlt2.Length > 0)
                    {
                        req.dwDevID = rlt2[0].dwDevID;
                        req.dwLabID = rlt2[0].dwLabID;
                    }
                    else
                    {
                        m_szMsg = "获取预约的设备失败";
                        m_nType = 32;
                    }
                }
                else
                {
                    m_szMsg = "预约信息失效";
                    m_nType = 32;
                }
                if (Request["DoUserOut"] == "1")//暂离
                {
                    req.dwOutType = (uint)RESVUSERGOOUTREQ.DWOUTTYPE.RESVUSEROUT_LEAVE;
                    Session["RESVUSEROUT_LEAVE"] = true;
                }
                if (Request["DoUserOut"] == "2")//离开
                {
                    req.dwOutType = (uint)RESVUSERGOOUTREQ.DWOUTTYPE.RESVUSEROUT_EXIT;
                }
                RESVUSERGOOUTRES rlt;
                if (m_Request.Console.ResvUserGoOut(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    m_szMsg = "暂离成功,请在预约结束前返回!";
                    m_nType = 32;
                }
                else
                {
                    m_szMsg = m_Request.szErrMsg;
                    m_nType = 32;
                }
            }
            else {//openid登录
                SHAKELOGINREQ req = new SHAKELOGINREQ();
                req.szOpenId = Openid;
                req.szIP = GetRealIP();
                req.dwStaSN = dwStaSN;
                req.szVersion = ((uint)ADMINLOGINREQ.SZVERSION.INTVER_MAIN).ToString() + "." + ((uint)ADMINLOGINREQ.SZVERSION.INTVER_RELEASE).ToString("00") + "." + ((uint)ADMINLOGINREQ.SZVERSION.INTVER_INTERNAL).ToString();
                SHAKELOGINRES res = new SHAKELOGINRES();
                REQUESTCODE uResponse = m_Request.Admin.MobileShakeLogin(req, out res);
                if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)//未绑定微信
                {
                    m_nType = 2;
                }
                else
                {
                    m_Request.m_UniDCom.SessionID = (uint)res.dwSessionID;
                    GetResv();
                }
            }
           
        }
    }

    //登录
    public void login() {
        m_Request.m_UniDCom.SessionID = 0;
        SHAKELOGINREQ req = new SHAKELOGINREQ();
        req.szOpenId = Session["Openid"].ToString();
        req.szLogonName = Request["szLogonName"];
        req.szPassword = "P" + Request["szPassword"];
        req.dwStaSN = dwStaSN;
        req.szIP = GetRealIP();
        req.szVersion = ((uint)ADMINLOGINREQ.SZVERSION.INTVER_MAIN).ToString() + "." + ((uint)ADMINLOGINREQ.SZVERSION.INTVER_RELEASE).ToString("00") + "." + ((uint)ADMINLOGINREQ.SZVERSION.INTVER_INTERNAL).ToString();
        if (Request["dwBind"] == "1")
        {
            req.dwProperty = (uint)MOBILEOPENDOORREQ.DWPROPERTY.MODPROP_BINDMSN;
        }
        SHAKELOGINRES res = new SHAKELOGINRES();
        REQUESTCODE uResponse = m_Request.Admin.MobileShakeLogin(req, out res);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)//登录成功
        {
            m_Request.m_UniDCom.SessionID = (uint)res.dwSessionID;

            GetResv();
        }
        else
        {
            m_nType = 2;
            m_szMsg = "账户或密码错误!";
            return;
        }
    }
    //获取预约信息
    public void GetResv()
    {
        if (Session["RESVUSEROUT_LEAVE"]!=null)
        {
            if (Session["RESVUSEROUT_LEAVE"].ToString() == "True")
            {
                ResvCheckIn();
            }
           
        }
        RESVREQ resvreq = new RESVREQ();
        resvreq.dwBeginDate = uint.Parse(DateTime.Now.Date.ToString("yyyyMMdd"));
        resvreq.dwEndDate = uint.Parse(DateTime.Now.Date.ToString("yyyyMMdd"));
        UNIRESERVE[] rec;
        REQUESTCODE uResponse = m_Request.Reserve.Get(resvreq, out rec);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)//获取预约信息
        {
            if (rec.Length != 0)//有预约
            {
                for (int i = 0; i < rec.Length; i++)
                {
                    UNIRESERVE reci = rec[i];
                    if ((reci.dwStatus & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DOING) > 0)//预约生效
                    {
                        if ((reci.dwStatus & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_SIGNED) > 0)//已签到
                        {
                            m_nType = 4;
                            m_szMsg = "预约截止时间:" + Get1970Date(reci.dwEndTime); //Session["szDispInfo"].ToString();
                            Session["resvid"] = reci.dwResvID.ToString();
                            return;
                        }
                        Session["resvid"] = reci.dwResvID.ToString();
                        m_nType = 8;
                        m_szTitle = "预约生效";
                        m_szMsg = "您有一条生效中的预约!";
                        comein.Style["display"] = "none";
                        return;
                    }
                }
                if (Session["resvid"] == null)
                {
                    m_nType = 16;
                    m_szTitle = "预约未生效";
                    m_szMsg = "确定设置入馆!";
                    checkin.Style["display"] = "none";
                    return;
                }
            }
            else//没有预约
            {
                m_nType = 8;
                m_szTitle = "入馆设置";
                m_szMsg = "确定设置入馆!";
                checkin.Style["display"] = "none";
                return;
            }
        }
        else
        {
            m_nType = 32;
            m_szMsg = "获取预约信息失败!";
            return;
        }
    }

    //暂离返回
    private void ResvCheckIn()
    {
        string resvId = Session["resvid"].ToString();
        //string devId = Request["dev_id"];
        //string labId = Request["lab_id"];
        //string type = Request["type"];
        RESVUSERCOMEINREQ req = new RESVUSERCOMEINREQ();
        req.dwResvID = ToUint(resvId);
        //req.dwDevID = ToUInt(devId);
        //req.dwLabID = ToUInt(labId);
        req.dwInType =1;
        RESVUSERCOMEINRES rlt;
        if (m_Request.Console.ResvUserComeIn(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            m_nType = 4;
            m_szMsg ="暂离后返回成功!";
            Session["RESVUSEROUT_LEAVE"] = false;
            return;

        }
        else
        {
            m_nType = 32;
            m_szMsg = m_Request.szErrMsg;
            return;
        }
    }

    //入馆
    public void ComeIn() {
        SHAKECOMEINREQ COMEINREQ = new SHAKECOMEINREQ();
        SHAKECOMEINRES COMEINRES = new SHAKECOMEINRES();
        m_Request.m_UniDCom.StaSN = dwStaSN;
        REQUESTCODE uResponse = m_Request.Console.ShakeComeIn(COMEINREQ, out COMEINRES);
       
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
            m_nType = 32;
            m_szMsg = "在馆状态设置失败!";
            return;
        }
        else
        {
            m_nType = 32;
            m_szMsg = COMEINRES.szDispInfo;
            return;
        }
          
    }

    //签到
    public void CheckIn() {
        SHAKECHECKINREQ CHECKINREQ = new SHAKECHECKINREQ();
        if (Session["resvid"]!=null)
        {
            string ada = Session["resvid"].ToString();
            CHECKINREQ.dwResvID = uint.Parse(Session["resvid"].ToString());
        }
        else
        {
            m_nType = 32;
            m_szMsg = "预约信息传递有误!";
            return;
        }
        SHAKECHECKINRES CHECKINRES = new SHAKECHECKINRES();
        m_Request.m_UniDCom.StaSN = dwStaSN;
       REQUESTCODE uResponse = m_Request.Console.ShakeCheckIn(CHECKINREQ, out CHECKINRES);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
            {
                m_nType = 4;
                m_szMsg = CHECKINRES.szDispInfo;
                //Session["szDispInfo"]= CHECKINRES.szDispInfo;//签到信息存储
        }
            else
            {
                m_nType = 32;
                m_szMsg = "签到失败!";
            }
    }

    protected string GetRealIP()
    {
        try
        {
            string ip = "";
            if (Context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                ip = Context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            else
            {
                ip = Context.Request.ServerVariables["REMOTE_ADDR"];
            }
            return ip;
        }
        catch (Exception)//e)
        {
            //throw e;
        }
        return "";
    }

    public uint ToUint(string obj) {
        try
        {
            return Convert.ToUInt32(obj);
        }
        catch (Exception)
        {

            return 0;
        }
    }
}