
/* ------------------------------------------------------
  版权信息： 杭州联创信息技术有限公司，2008-2011
  文 件 名： UniInterface.h
  创建时间： 2008.08.25
  功能描述： 定义本系统各模块与服务模块的通信接口
  作    者： 何厚武
  --------------------------------------------------------------- 
*/
using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Services;
using UniStruct;
using UniWebLib;


public partial class UniLabAll : UniBaseService
{

    //#region Admin部分
    /*管理员管理，系统配置*/

    public struct Admin_StaLoginResult
    {
        public uint code;
        public string Message;
        public ADMINLOGINRES vrRes;
    }

    [WebMethod(EnableSession = true, Description = "登录")]
    //[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
    [System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
    public Admin_StaLoginResult
    Admin_StaLogin(ADMINLOGINREQ vrParameter)
    {
        if (Context.Session != null)
        {
            if (Context.Session["SessionID"] != null)
            {
                soaphead.SessionID = (uint)Context.Session["SessionID"];
                soaphead.StationSN = (uint)Context.Session["StationSN"];
            }
        }

        vrParameter.dwStaSN = 1;
        vrParameter.dwLoginRole = 4;
        vrParameter.szVersion = ((uint)ADMINLOGINREQ.SZVERSION.INTVER_MAIN).ToString() + "." + ((uint)ADMINLOGINREQ.SZVERSION.INTVER_RELEASE).ToString("00") + "." + ((uint)ADMINLOGINREQ.SZVERSION.INTVER_INTERNAL).ToString();
        vrParameter.szPassword = "P" + vrParameter.szPassword;
        Admin_StaLoginResult ret = new Admin_StaLoginResult();
        UniRequest m_Request = GetRequest();
        REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
        ret.code = 2;
        uResponse = m_Request.Admin.StaLogin(vrParameter, out ret.vrRes);
        ret.Message = m_Request.szErrMsg;

        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
            ret.code = 1;
            return ret;
        }


        if ((object)ret.vrRes == null)
        {
            Trace("vrResult == null");
            ret.code = 1;
            return ret;
        }

        if (Context.Session != null)
        {
            Context.Session["SessionID"] = ret.vrRes.dwSessionID;
            Context.Session["StationSN"] = vrParameter.dwStaSN;
            UNIACCOUNT acc = new UNIACCOUNT();
            acc.dwAccNo = ret.vrRes.AdminInfo.dwAccNo;
            acc.dwIdent = ret.vrRes.AdminInfo.dwIdent;
            acc.dwStatus = ret.vrRes.AdminInfo.dwStatus;
            acc.szEmail = ret.vrRes.AdminInfo.szEmail;
            acc.szHandPhone = ret.vrRes.AdminInfo.szHandPhone;
            acc.szLogonName = ret.vrRes.AdminInfo.szLogonName;
            acc.szMemo = ret.vrRes.AdminInfo.szMemo;
            acc.szTel = ret.vrRes.AdminInfo.szTel;
            acc.szTrueName = ret.vrRes.AdminInfo.szTrueName;
            Context.Session["AccInfo"] = acc;
        }

        ret.code = 0;
        return ret;
    }

    public struct Account_SetResult
    {
        public uint code;
        public string Message;
        public UNIACCOUNT vrRes;
    }
    public struct ACT
    {
        public string szLogonName;		/*登录名*/

        public string szPassword;		/*密码*/

        public string szTel;		/*电话*/

        public string szHandPhone;		/*手机*/

        public string szEmail;		/*电邮*/

        public string szMSN;		/*MSN*/

        public string szQQ;		/*QQ*/

        public string szHomeAddr;		/*家庭住址*/

        public string szCurAddr;		/*校内住址*/

        public string szMemo;		/*说明信息*/
    };

    [WebMethod(EnableSession = true, Description = "激活、修改帐户属性")]
    //[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
    [System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
    public Account_SetResult
    Account_Act(ACT vrParameter)
    {
        Account_SetResult ret = new Account_SetResult();

        UNIACCOUNT AccInfo = new UNIACCOUNT();

        if (Context.Session != null)
        {
            if (Context.Session["SessionID"] != null)
            {
                soaphead.SessionID = (uint)Context.Session["SessionID"];
                soaphead.StationSN = (uint)Context.Session["StationSN"];
                AccInfo = (UNIACCOUNT)Context.Session["AccInfo"];
            }
        }

        ADMINLOGINREQ vrParameterLogin = new ADMINLOGINREQ();
        vrParameterLogin.szLogonName = vrParameter.szLogonName;
        vrParameterLogin.szPassword = vrParameter.szPassword;
        Admin_StaLoginResult loginret = Admin_StaLogin(vrParameterLogin);
        if (loginret.code != 0)
        {
            ret.code = loginret.code;
            ret.Message = loginret.Message;
            return ret;
        }


        AccInfo.szTel = vrParameter.szTel;
        AccInfo.szHandPhone = vrParameter.szHandPhone;
        AccInfo.szEmail = vrParameter.szEmail;
        AccInfo.szMSN = vrParameter.szMSN;
        AccInfo.szQQ = vrParameter.szQQ;
        AccInfo.szHomeAddr = vrParameter.szHomeAddr;
        AccInfo.szCurAddr = vrParameter.szCurAddr;
        AccInfo.szMemo = vrParameter.szMemo;

        UniRequest m_Request = GetRequest();
        REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
        ret.code = 2;

        uResponse = m_Request.Account.Set(AccInfo, out ret.vrRes);
        ret.Message = m_Request.szErrMsg;

        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
            ret.code = 1;
            return ret;
        }


        if ((object)ret.vrRes == null)
        {
            Trace("vrResult == null");
            ret.code = 1;
            return ret;
        }

        ret.code = 0;
        return ret;
    }


    public struct Account_CheckResult
    {
        public uint code;
        public string Message;
    }
    public struct CHECK
    {
        public string szLogonName;
    };

    [WebMethod(EnableSession = true, Description = "检查帐户是否存在")]
    //[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
    [System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
    public Account_CheckResult
    Account_Check(CHECK vrParameter)
    {
        if (Context.Session != null)
        {
            if (Context.Session["SessionID"] != null)
            {
                soaphead.SessionID = (uint)Context.Session["SessionID"];
                soaphead.StationSN = (uint)Context.Session["StationSN"];
            }
        }
        Account_CheckResult ret = new Account_CheckResult();

        ACCCHECKREQ req = new ACCCHECKREQ();
        req.dwCheckType = new UniDW((uint)ACCCHECKREQ.DWCHECKTYPE.ACCCHECK_BYLOGONNAME);
        req.szCheckKey = vrParameter.szLogonName;
        UNIACCOUNT acc;

        UniRequest m_Request = GetRequest();
        REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
        ret.code = 2;
        uResponse = m_Request.Account.AccCheck(req, out acc);
        ret.Message = m_Request.szErrMsg;

        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
            ret.code = 1;
            ret.Message = "账户不存在";
            return ret;
        }

        ret.code = 0;
        return ret;
    }

    //#endregion Account部分



    public struct Device_DevRegistResult
    {
        public uint code;
        public string Message;
        public DEVREGISTRES vrRes;
    }

    [WebMethod(EnableSession = true, Description = "设备注册")]
    //[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
    [System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
    public Device_DevRegistResult
    Device_DevRegist(DEVREGISTREQ vrParameter)
    {
      
        uint code2;
        code2 = 0;
        string Message2;
        Release(out code2, out Message2);

        if (Context.Session != null)
        {
            if (Context.Session["SessionID"] != null)
            {
                soaphead.SessionID = (uint)Context.Session["SessionID"];
                soaphead.StationSN = (uint)Context.Session["StationSN"];
            }
        }
        Logger.trace("start get req strart");
        Device_DevRegistResult ret = new Device_DevRegistResult();
        UniRequest m_Request = GetRequest();
        Logger.trace("start get req end");
        REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
        ret.code = 2;

        soaphead.StationSN = (uint)vrParameter.dwStaSN;
        Context.Session["StationSN"] = (uint)vrParameter.dwStaSN;
        m_Request.m_UniDCom.StaSN = (uint)vrParameter.dwStaSN;

        uResponse = m_Request.Device.DevRegist(vrParameter, out ret.vrRes);
        Logger.trace("uResponse"+ uResponse.ToString());
        ret.Message = m_Request.szErrMsg;

        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
            ret.code = 1;
            return ret;
        }


        if ((object)ret.vrRes == null)
        {
            Trace("vrResult == null");
            ret.code = 1;
            return ret;
        }

        if (Context.Session != null)
        {
            Context.Session["SessionID"] = ret.vrRes.dwSessionID;
            Context.Session["StationSN"] = vrParameter.dwStaSN;
            Context.Session["DevRegist"] = ret.vrRes;
        }

        ret.code = 0;
        return ret;
    }

    public struct Device_DevLogonResult
    {
        public uint code;
        public string Message;
        public DEVLOGONRES vrRes;
    }

    [WebMethod(EnableSession = true, Description = "用户从客户端登录")]
    //[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
    [System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
    public Device_DevLogonResult
    Device_DevLogon(DEVLOGONREQ vrParameter)
    {
        DEVREGISTRES vrDevRegistRes = new DEVREGISTRES();
        if (Context.Session != null)
        {
            if (Context.Session["SessionID"] != null)
            {
                soaphead.SessionID = (uint)Context.Session["SessionID"];
                soaphead.StationSN = (uint)Context.Session["StationSN"];
                vrDevRegistRes = (DEVREGISTRES)Context.Session["DevRegist"];
            }
        }
        vrParameter.dwDevID = vrDevRegistRes.dwDevID;
        vrParameter.dwLabID = vrDevRegistRes.dwLabID;
        vrParameter.dwLogonType = (uint)DEVLOGONREQ.DWLOGONTYPE.LOGONTYPE_USER;
        vrParameter.szPasswd = "P" + vrParameter.szPasswd;
        Device_DevLogonResult ret = new Device_DevLogonResult();
        UniRequest m_Request = GetRequest();
        REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
        ret.code = 2;

        uResponse = m_Request.Device.DevLogon(vrParameter, out ret.vrRes);
        ret.Message = m_Request.szErrMsg;

        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
            ret.code = 1;
            return ret;
        }


        if ((object)ret.vrRes == null)
        {
            Trace("vrResult == null");
            ret.code = 1;
            return ret;
        }

        if (Context.Session != null)
        {
            try
            {
                Context.Session["DevAccInfo"] = ret.vrRes.szAccInfo;
                UNIACCOUNT accno = (UNIACCOUNT)Context.Session["DevAccInfo"];
            }
            catch
            {
                Logger.trace("loginres非uniaccount");
            }


        }

        ret.code = 0;
        return ret;
    }


    public struct Device_DevLogoutResult
    {
        public uint code;
        public string Message;
        public DEVLOGOUTRES vrRes;
    }

    [WebMethod(EnableSession = true, Description = "用户从客户端注销")]
    //[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
    [System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
    public Device_DevLogoutResult
    Device_DevLogout()
    {
        DEVREGISTRES vrDevRegistRes = new DEVREGISTRES();
        UNIACCOUNT AccInfo = new UNIACCOUNT();

        if (Context.Session != null)
        {
            if (Context.Session["SessionID"] != null)
            {
                soaphead.SessionID = (uint)Context.Session["SessionID"];
                soaphead.StationSN = (uint)Context.Session["StationSN"];
                if (Context.Session["DevRegist"] != null)
                {
                    vrDevRegistRes = (DEVREGISTRES)Context.Session["DevRegist"];
                }
                if (Context.Session["DevAccInfo"] != null)
                {
                    AccInfo = (UNIACCOUNT)Context.Session["DevAccInfo"];
                }
            }
        }
        DEVLOGOUTREQ vrParameter = new DEVLOGOUTREQ();
        vrParameter.dwDevID = vrDevRegistRes.dwDevID;
        vrParameter.dwLabID = vrDevRegistRes.dwLabID;
        vrParameter.dwAccNo = AccInfo.dwAccNo;
        vrParameter.dwParam = 1;

        Device_DevLogoutResult ret = new Device_DevLogoutResult();
        UniRequest m_Request = GetRequest();
        REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
        ret.code = 2;

        uResponse = m_Request.Device.DevLogout(vrParameter, out ret.vrRes);
        ret.Message = m_Request.szErrMsg;

        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
            ret.code = 1;
            return ret;
        }


        if ((object)ret.vrRes == null)
        {
            Trace("vrResult == null");
            ret.code = 1;
            return ret;
        }

        uint code2;
        string Message2;
        Release(out code2, out Message2);

        ret.code = 0;
        return ret;
    }


    public struct Device_DevHandShakeResult
    {
        public uint code;
        public string Message;
        public DEVHANDSHAKERES vrRes;
    }

    [WebMethod(EnableSession = true, Description = "使用中的客户端与服务定时通信")]
    //[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
    [System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
    public Device_DevHandShakeResult
    Device_DevHandShake()
    {
        DEVREGISTRES vrDevRegistRes = new DEVREGISTRES();
        UNIACCOUNT AccInfo = new UNIACCOUNT();

        if (Context.Session != null)
        {
            if (Context.Session["SessionID"] != null)
            {
                soaphead.SessionID = (uint)Context.Session["SessionID"];
                soaphead.StationSN = (uint)Context.Session["StationSN"];
                if (Context.Session["DevRegist"] != null)
                {
                    vrDevRegistRes = (DEVREGISTRES)Context.Session["DevRegist"];
                }
                if (Context.Session["DevAccInfo"] != null)
                {
                    AccInfo = (UNIACCOUNT)Context.Session["DevAccInfo"];
                    //Logger.trace("login-success");
                }
            }
        }
        DEVHANDSHAKEREQ vrParameter = new DEVHANDSHAKEREQ();
        vrParameter.dwDevID = vrDevRegistRes.dwDevID;
        vrParameter.dwLabID = vrDevRegistRes.dwLabID;
        vrParameter.dwAccNo = AccInfo.dwAccNo;
        Logger.Trace(vrParameter);
        Device_DevHandShakeResult ret = new Device_DevHandShakeResult();
        UniRequest m_Request = GetRequest();
        Logger.Trace(m_Request);
        REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
        ret.code = 2;
        if (vrDevRegistRes.dwDevID != null && Application[vrDevRegistRes.dwDevID.ToString()] != null)
        {
            DateTime dtTime = (DateTime)Application[vrDevRegistRes.dwDevID.ToString()];
            if (dtTime != null)
            {
                TimeSpan span = (TimeSpan)(DateTime.Now - dtTime);
                if (span.Seconds > 20)
                {
                    uResponse = m_Request.Device.DevHandShake(vrParameter, out ret.vrRes);
                    
                    ret.code = 0;
                    Application[vrDevRegistRes.dwDevID.ToString()] = DateTime.Now;
                    Application[vrDevRegistRes.dwDevID.ToString() + "res"] = ret;
                    Logger.trace(m_Request.m_UniDCom.SessionID.ToString() + ":重连");
                    if(uResponse==REQUESTCODE.EXECUTE_SUCCESS)
                    { 
                    return ret;
                    }
                }
                else
                {
                    return (Device_DevHandShakeResult)Application[vrDevRegistRes.dwDevID.ToString() + "res"];
                   
                }
            }
        }
       
        uResponse = m_Request.Device.DevHandShake(vrParameter, out ret.vrRes);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
            uResponse = m_Request.Device.DevHandShake(vrParameter, out ret.vrRes);
           
        }
        else
        {
            Logger.trace(m_Request.m_UniDCom.SessionID.ToString() + "：连接");
            ret.code = 0;
            Application[vrDevRegistRes.dwDevID.ToString()] = DateTime.Now;
            Application[vrDevRegistRes.dwDevID.ToString() + "res"] = ret;
        }
        ret.Message = m_Request.szErrMsg;

        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
            Trace(vrDevRegistRes.dwDevID.ToString()+":握手失败");
            ret.code = 1;
            return ret;
        }


        if ((object)ret.vrRes == null)
        {
            Trace("vrResult == null");
            ret.code = 1;
            return ret;
        }

        ret.code = 0;
        return ret;
    }
}
//