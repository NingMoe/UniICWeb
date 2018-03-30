
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

[SoapDocumentService(RoutingStyle = SoapServiceRoutingStyle.RequestElement)]   
public partial class UniLab : UniService
{

	//#region Admin部分
	/*管理员管理，系统配置*/	
	
	public struct Admin_StaLoginResult
	{
		public uint code;
		public string Message;
		public  ADMINLOGINRES vrRes;
	}

    [WebMethod(EnableSession = true, Description = "获取房间信息")]
    //[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
    [System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
    public ExtRoomInfo[]
    Device_GetExtRoomInfo()
    {
        if (Context.Session != null)
        {
            if (Context.Session["SessionID"] != null)
            {
                soaphead.SessionID = (uint)Context.Session["SessionID"];
                soaphead.StationSN = (uint)Context.Session["StationSN"];
            }
        }
        UniRequest m_Request = GetRequest();
        ADMINLOGINREQ vrLogin = new ADMINLOGINREQ();
        ADMINLOGINRES vrLoginRes;
        vrLogin.szLogonName = "guest";
        vrLogin.szPassword = "P";
        vrLogin.dwLoginRole = (uint)ADMINLOGINREQ.DWLOGINROLE.LOGIN_USER;
        vrLogin.szVersion = ((uint)ADMINLOGINREQ.SZVERSION.INTVER_MAIN).ToString() + "." + ((uint)ADMINLOGINREQ.SZVERSION.INTVER_RELEASE).ToString("00") + "." + ((uint)ADMINLOGINREQ.SZVERSION.INTVER_INTERNAL).ToString();
        vrLogin.dwStaSN = 1;
        m_Request.m_UniDCom.StaSN = 1;
        m_Request.m_UniDCom.SessionID = 0;
        if (m_Request.Admin.StaLogin(vrLogin, out vrLoginRes) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            m_Request.m_UniDCom.SessionID = (uint)vrLoginRes.dwSessionID;
            vrLogin.szVersion = ((uint)ADMINLOGINREQ.SZVERSION.INTVER_MAIN).ToString() + "." + ((uint)ADMINLOGINREQ.SZVERSION.INTVER_RELEASE).ToString("00") + "." + ((uint)ADMINLOGINREQ.SZVERSION.INTVER_INTERNAL).ToString();
            vrLogin.dwStaSN = 1;
            m_Request.m_UniDCom.StaSN = 1;

            REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
            FULLROOMREQ vrGet = new FULLROOMREQ();
            FULLROOM[] vtRes;
            uResponse = m_Request.Device.FullRoomGet(vrGet, out vtRes);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
            {
                ExtRoomInfo[] res = new ExtRoomInfo[vtRes.Length];
                for (int i = 0; vtRes != null && i < vtRes.Length; i++)
                {
                    res[i] = new ExtRoomInfo();
                    res[i].szRoomName = vtRes[i].szRoomName;
                    res[i].uIdelNum = (uint)vtRes[i].dwIdleDevNum;
                    res[i].uTotalNum = (uint)vtRes[i].dwTotalDevNum;
                }
                return res;
            }
            else
            {
                return null;
            }
        }

        return null;


    }
    public struct ExtRoomInfo
    {
        public string szRoomName;
        public uint uIdelNum;
        public uint uTotalNum;
    }
    
	[WebMethod (EnableSession = true, Description = "登录")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_StaLoginResult 
	Admin_StaLogin(ADMINLOGINREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
        vrParameter.dwStaSN = 0;
        vrParameter.dwLoginRole = 4;
        vrParameter.szVersion = ((uint)ADMINLOGINREQ.SZVERSION.INTVER_MAIN).ToString() + "." + ((uint)ADMINLOGINREQ.SZVERSION.INTVER_RELEASE).ToString("00") + "." + ((uint)ADMINLOGINREQ.SZVERSION.INTVER_INTERNAL).ToString();
		vrParameter.szPassword = "P" + vrParameter.szPassword;
		Admin_StaLoginResult ret = new Admin_StaLoginResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.StaLogin(vrParameter, out  ret.vrRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vrRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		if(Context.Session != null)
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
		public  UNIACCOUNT vrRes;
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

		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
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

        uResponse = m_Request.Account.Set(AccInfo, out  ret.vrRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vrRes == null)
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
        uResponse = m_Request.Account.AccCheck(req, out  acc);
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
}
//