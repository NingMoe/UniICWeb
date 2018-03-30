
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


/// <summary>
/// UniLab 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ScriptService]
public partial class UniLabAll : UniBaseService
{
    public UniLabAll () {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }


	//#region Admin部分
	/*管理员管理*/
	
	public struct Admin_GetResult
	{
		public uint code;
		public string Message;
		public  UNIADMIN[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取管理员列表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_GetResult 
	Admin_Get(ADMINREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_GetResult ret = new Admin_GetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.Get(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Admin_GetIFResult
	{
		public uint code;
		public string Message;
		public  IFPARAM[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取当前管理员界面参数信息")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_GetIFResult 
	Admin_GetIF(IFPARAMREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_GetIFResult ret = new Admin_GetIFResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.GetIF(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Admin_GetAdminLogResult
	{
		public uint code;
		public string Message;
		public  ADMINLOG[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取管理员操作日志")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_GetAdminLogResult 
	Admin_GetAdminLog(ADMINLOGREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_GetAdminLogResult ret = new Admin_GetAdminLogResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.GetAdminLog(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Admin_GetManRoomResult
	{
		public uint code;
		public string Message;
		public  MANROOM[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取管理房间")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_GetManRoomResult 
	Admin_GetManRoom(MANROOMREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_GetManRoomResult ret = new Admin_GetManRoomResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.GetManRoom(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Admin_SetResult
	{
		public uint code;
		public string Message;
		public  UNIADMIN vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "新建或修改管理员")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_SetResult 
	Admin_Set(UNIADMIN vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_SetResult ret = new Admin_SetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.Set(vrParameter, out  ret.vrRes);
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
	
	
	public struct Admin_SaveIFResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "保存当前管理员界面参数信息")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_SaveIFResult 
	Admin_SaveIF(IFPARAM vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_SaveIFResult ret = new Admin_SaveIFResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.SaveIF(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Admin_ClearIPBlackListResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "清除IP地址黑名单")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_ClearIPBlackListResult 
	Admin_ClearIPBlackList(IPBLACKLIST vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_ClearIPBlackListResult ret = new Admin_ClearIPBlackListResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.ClearIPBlackList(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Admin_AdminChgPasswdResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "修改管理员密码")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_AdminChgPasswdResult 
	Admin_AdminChgPasswd(ADMINCHGPASSWD vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_AdminChgPasswdResult ret = new Admin_AdminChgPasswdResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.AdminChgPasswd(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Admin_StaffSetResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "设置工作人员信息")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_StaffSetResult 
	Admin_StaffSet(STAFFINFO vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_StaffSetResult ret = new Admin_StaffSetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.StaffSet(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Admin_DelResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "修改管理员属性")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_DelResult 
	Admin_Del(UNIADMIN vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_DelResult ret = new Admin_DelResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.Del(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Admin_AdminCheckGetResult
	{
		public uint code;
		public string Message;
		public  CHECKINFO[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取审查信息")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_AdminCheckGetResult 
	Admin_AdminCheckGet(CHECKREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_AdminCheckGetResult ret = new Admin_AdminCheckGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.AdminCheckGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Admin_AdminCheckResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "管理员审查")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_AdminCheckResult 
	Admin_AdminCheck(ADMINCHECK vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_AdminCheckResult ret = new Admin_AdminCheckResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.AdminCheck(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Admin_AdminCheckLogGetResult
	{
		public uint code;
		public string Message;
		public  CHECKLOG[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取审查信息日志")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_AdminCheckLogGetResult 
	Admin_AdminCheckLogGet(CHECKLOGREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_AdminCheckLogGetResult ret = new Admin_AdminCheckLogGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.AdminCheckLogGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Admin_LoginResult
	{
		public uint code;
		public string Message;
		public  ADMINLOGINRES vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "管理员登录")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_LoginResult 
	Admin_Login(ADMINLOGINREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_LoginResult ret = new Admin_LoginResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.Login(vrParameter, out  ret.vrRes);
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
	
	
	public struct Admin_MobileStaLoginResult
	{
		public uint code;
		public string Message;
		public  ADMINLOGINRES vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "手机登录")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_MobileStaLoginResult 
	Admin_MobileStaLogin(MOBILELOGINREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_MobileStaLoginResult ret = new Admin_MobileStaLoginResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.MobileStaLogin(vrParameter, out  ret.vrRes);
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
	
	
	public struct Admin_LogoutResult
	{
		public uint code;
		public string Message;
		public  ADMINLOGOUTRES vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "管理员退出")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_LogoutResult 
	Admin_Logout(ADMINLOGOUTREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_LogoutResult ret = new Admin_LogoutResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.Logout(vrParameter, out  ret.vrRes);
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
	
	
	public struct Admin_UIDInfoGetResult
	{
		public uint code;
		public string Message;
		public  UIDINFO[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取系统支持的UID")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_UIDInfoGetResult 
	Admin_UIDInfoGet(UIDINFOREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_UIDInfoGetResult ret = new Admin_UIDInfoGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.UIDInfoGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Admin_OPPrivGetResult
	{
		public uint code;
		public string Message;
		public  OPPRIV[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取操作权限")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_OPPrivGetResult 
	Admin_OPPrivGet(OPPRIVREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_OPPrivGetResult ret = new Admin_OPPrivGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.OPPrivGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Admin_OPPrivSetResult
	{
		public uint code;
		public string Message;
		public  OPPRIV vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "设置修改操作权限")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_OPPrivSetResult 
	Admin_OPPrivSet(OPPRIV vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_OPPrivSetResult ret = new Admin_OPPrivSetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.OPPrivSet(vrParameter, out  ret.vrRes);
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
	
	
	public struct Admin_OPPrivDelResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除操作权限")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_OPPrivDelResult 
	Admin_OPPrivDel(OPPRIV vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_OPPrivDelResult ret = new Admin_OPPrivDelResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.OPPrivDel(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Admin_UserRoleGetResult
	{
		public uint code;
		public string Message;
		public  USERROLE[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取用户角色")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_UserRoleGetResult 
	Admin_UserRoleGet(USERROLEREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_UserRoleGetResult ret = new Admin_UserRoleGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.UserRoleGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Admin_UserRoleSetResult
	{
		public uint code;
		public string Message;
		public  USERROLE vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "设置用户角色")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_UserRoleSetResult 
	Admin_UserRoleSet(USERROLE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_UserRoleSetResult ret = new Admin_UserRoleSetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.UserRoleSet(vrParameter, out  ret.vrRes);
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
	
	
	public struct Admin_UserRoleDelResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除用户角色")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_UserRoleDelResult 
	Admin_UserRoleDel(USERROLE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_UserRoleDelResult ret = new Admin_UserRoleDelResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.UserRoleDel(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Admin_CltPWGetResult
	{
		public uint code;
		public string Message;
		public  CLTPASSWD[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取客户端密码")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_CltPWGetResult 
	Admin_CltPWGet()
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_CltPWGetResult ret = new Admin_CltPWGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.CltPWGet( out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Admin_CltPWSetResult
	{
		public uint code;
		public string Message;
		public  CLTPASSWD vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "重新设置客户端密码")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_CltPWSetResult 
	Admin_CltPWSet(CLTPASSWD vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_CltPWSetResult ret = new Admin_CltPWSetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.CltPWSet(vrParameter, out  ret.vrRes);
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
	
	
	public struct Admin_AdminRefreshFlagGetResult
	{
		public uint code;
		public string Message;
		public  REFRESHFLAGINFO[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取刷新标志")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_AdminRefreshFlagGetResult 
	Admin_AdminRefreshFlagGet(REFRESHFLAGREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_AdminRefreshFlagGetResult ret = new Admin_AdminRefreshFlagGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.AdminRefreshFlagGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Admin_HolidDayGetResult
	{
		public uint code;
		public string Message;
		public  UNIHOLIDAY[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取节假日")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_HolidDayGetResult 
	Admin_HolidDayGet(HOLIDAYREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_HolidDayGetResult ret = new Admin_HolidDayGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.HolidDayGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Admin_HolidDaySetResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "设置节假日")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_HolidDaySetResult 
	Admin_HolidDaySet(UNIHOLIDAY vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_HolidDaySetResult ret = new Admin_HolidDaySetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.HolidDaySet(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Admin_HolidDayDelResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除节假日")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_HolidDayDelResult 
	Admin_HolidDayDel(UNIHOLIDAY vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_HolidDayDelResult ret = new Admin_HolidDayDelResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.HolidDayDel(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Admin_CheckExistResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "管理端界面调用检查某个值是否存在，不存在返回成功，存在返回错误")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_CheckExistResult 
	Admin_CheckExist(CHECKEXISTREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_CheckExistResult ret = new Admin_CheckExistResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.CheckExist(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Admin_GetMaxValueResult
	{
		public uint code;
		public string Message;
		public  MAXVALUE vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取某个字段的最大值（仅支持数值型字段）")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_GetMaxValueResult 
	Admin_GetMaxValue(MAXVALUEREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_GetMaxValueResult ret = new Admin_GetMaxValueResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.GetMaxValue(vrParameter, out  ret.vrRes);
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
	
	
	public struct Admin_GetBasicStatInfoResult
	{
		public uint code;
		public string Message;
		public  BASICSTAT vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取系统基本统计信息")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_GetBasicStatInfoResult 
	Admin_GetBasicStatInfo()
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_GetBasicStatInfoResult ret = new Admin_GetBasicStatInfoResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.GetBasicStatInfo( out  ret.vrRes);
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
	
	
	public struct Admin_CheckTypeGetResult
	{
		public uint code;
		public string Message;
		public  CHECKTYPE[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取审核类别请求")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_CheckTypeGetResult 
	Admin_CheckTypeGet(CHECKTYPEREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_CheckTypeGetResult ret = new Admin_CheckTypeGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.CheckTypeGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Admin_CheckTypeSetResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "修改审核类别")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_CheckTypeSetResult 
	Admin_CheckTypeSet(CHECKTYPE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_CheckTypeSetResult ret = new Admin_CheckTypeSetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.CheckTypeSet(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Admin_GetUserFeedbackResult
	{
		public uint code;
		public string Message;
		public  USERFEEDBACK[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取意见反馈表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_GetUserFeedbackResult 
	Admin_GetUserFeedback(USERFEEDBACKREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_GetUserFeedbackResult ret = new Admin_GetUserFeedbackResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.GetUserFeedback(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Admin_DoUserFeedbackResult
	{
		public uint code;
		public string Message;
		public  USERFEEDBACK vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "用户意见反馈")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_DoUserFeedbackResult 
	Admin_DoUserFeedback(USERFEEDBACK vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_DoUserFeedbackResult ret = new Admin_DoUserFeedbackResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.DoUserFeedback(vrParameter, out  ret.vrRes);
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
	
	
	public struct Admin_ReplyUserFeedbackResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "回复用户意见反馈")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_ReplyUserFeedbackResult 
	Admin_ReplyUserFeedback(USERFEEDBACK vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_ReplyUserFeedbackResult ret = new Admin_ReplyUserFeedbackResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.ReplyUserFeedback(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Admin_ServiceTypeGetResult
	{
		public uint code;
		public string Message;
		public  UNISERVICETYPE[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取服务类别请求")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_ServiceTypeGetResult 
	Admin_ServiceTypeGet(SERVICETYPEREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_ServiceTypeGetResult ret = new Admin_ServiceTypeGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.ServiceTypeGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Admin_ServiceTypeSetResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "修改服务类别")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_ServiceTypeSetResult 
	Admin_ServiceTypeSet(UNISERVICETYPE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_ServiceTypeSetResult ret = new Admin_ServiceTypeSetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.ServiceTypeSet(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Admin_GetPollOnLineResult
	{
		public uint code;
		public string Message;
		public  POLLONLINE[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取网上投票信息")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_GetPollOnLineResult 
	Admin_GetPollOnLine(POLLONLINEREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_GetPollOnLineResult ret = new Admin_GetPollOnLineResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.GetPollOnLine(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Admin_SetPollOnLineResult
	{
		public uint code;
		public string Message;
		public  POLLONLINE vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "新建修改网上投票")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Admin_SetPollOnLineResult 
	Admin_SetPollOnLine(POLLONLINE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Admin_SetPollOnLineResult ret = new Admin_SetPollOnLineResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Admin.SetPollOnLine(vrParameter, out  ret.vrRes);
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
	
	
	public struct Admin_VotePollOnLineResult
	{
		public uint code;
		public string Message;
		
	}
	
	//#endregion Admin部分
	

	//#region Station部分
	/*站点管理*/
	
	public struct Station_GetStationResult
	{
		public uint code;
		public string Message;
		public  UNISTATION[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取站点")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Station_GetStationResult 
	Station_GetStation(STATIONREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Station_GetStationResult ret = new Station_GetStationResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Station.GetStation(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Station_SetStationResult
	{
		public uint code;
		public string Message;
		public  UNISTATION vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "设置站点")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Station_SetStationResult 
	Station_SetStation(UNISTATION vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Station_SetStationResult ret = new Station_SetStationResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Station.SetStation(vrParameter, out  ret.vrRes);
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
	
	
	public struct Station_DelStationResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除站点")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Station_DelStationResult 
	Station_DelStation(UNISTATION vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Station_DelStationResult ret = new Station_DelStationResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Station.DelStation(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	//#endregion Station部分
	

	//#region Account部分
	/*帐户管理*/
	
	public struct Account_DeptGetResult
	{
		public uint code;
		public string Message;
		public  UNIDEPT[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取部门列表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_DeptGetResult 
	Account_DeptGet(DEPTREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_DeptGetResult ret = new Account_DeptGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.DeptGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Account_DeptSetResult
	{
		public uint code;
		public string Message;
		public  UNIDEPT vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "修改部门属性")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_DeptSetResult 
	Account_DeptSet(UNIDEPT vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_DeptSetResult ret = new Account_DeptSetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.DeptSet(vrParameter, out  ret.vrRes);
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
	
	
	public struct Account_DeptDelResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除部门")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_DeptDelResult 
	Account_DeptDel(UNIDEPT vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_DeptDelResult ret = new Account_DeptDelResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.DeptDel(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Account_CampusGetResult
	{
		public uint code;
		public string Message;
		public  UNICAMPUS[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取校区列表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_CampusGetResult 
	Account_CampusGet(CAMPUSREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_CampusGetResult ret = new Account_CampusGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.CampusGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Account_CampusSetResult
	{
		public uint code;
		public string Message;
		public  UNICAMPUS vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "修改校区属性")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_CampusSetResult 
	Account_CampusSet(UNICAMPUS vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_CampusSetResult ret = new Account_CampusSetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.CampusSet(vrParameter, out  ret.vrRes);
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
	
	
	public struct Account_CampusDelResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除校区")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_CampusDelResult 
	Account_CampusDel(UNICAMPUS vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_CampusDelResult ret = new Account_CampusDelResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.CampusDel(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Account_ClassGetResult
	{
		public uint code;
		public string Message;
		public  UNICLASS[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取班级列表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_ClassGetResult 
	Account_ClassGet(CLASSREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_ClassGetResult ret = new Account_ClassGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.ClassGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Account_ClassSetResult
	{
		public uint code;
		public string Message;
		public  UNICLASS vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "修改班级属性")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_ClassSetResult 
	Account_ClassSet(UNICLASS vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_ClassSetResult ret = new Account_ClassSetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.ClassSet(vrParameter, out  ret.vrRes);
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
	
	
	public struct Account_ClassDelResult
	{
		public uint code;
		public string Message;
		public  UNICLASS vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "删除班级")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_ClassDelResult 
	Account_ClassDel(UNICLASS vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_ClassDelResult ret = new Account_ClassDelResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.ClassDel(vrParameter, out  ret.vrRes);
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
	
	
	public struct Account_GetResult
	{
		public uint code;
		public string Message;
		public  UNIACCOUNT[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取帐户列表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_GetResult 
	Account_Get(ACCREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_GetResult ret = new Account_GetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.Get(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Account_TutorGetResult
	{
		public uint code;
		public string Message;
		public  UNITUTOR[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取导师")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_TutorGetResult 
	Account_TutorGet(TUTORREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_TutorGetResult ret = new Account_TutorGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.TutorGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Account_TutorStudentGetResult
	{
		public uint code;
		public string Message;
		public  TUTORSTUDENT[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取导师的学生")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_TutorStudentGetResult 
	Account_TutorStudentGet(TUTORSTUDENTREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_TutorStudentGetResult ret = new Account_TutorStudentGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.TutorStudentGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Account_ExtIdentAccGetResult
	{
		public uint code;
		public string Message;
		public  EXTIDENTACC[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取扩展身份人员信息")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_ExtIdentAccGetResult 
	Account_ExtIdentAccGet(EXTIDENTACCREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_ExtIdentAccGetResult ret = new Account_ExtIdentAccGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.ExtIdentAccGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Account_AccInfoGetResult
	{
		public uint code;
		public string Message;
		public  UNIACCOUNT vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取用户信息")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_AccInfoGetResult 
	Account_AccInfoGet(ACCINFOREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_AccInfoGetResult ret = new Account_AccInfoGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.AccInfoGet(vrParameter, out  ret.vrRes);
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
	
	
	public struct Account_ExtIdentAccSetResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "设置扩展身份人员")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_ExtIdentAccSetResult 
	Account_ExtIdentAccSet(EXTIDENTACC vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_ExtIdentAccSetResult ret = new Account_ExtIdentAccSetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.ExtIdentAccSet(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Account_TutorStudentSetResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "设置导师的学生")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_TutorStudentSetResult 
	Account_TutorStudentSet(TUTORSTUDENT vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_TutorStudentSetResult ret = new Account_TutorStudentSetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.TutorStudentSet(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Account_TutorStudentCheckResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "导师审核学生")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_TutorStudentCheckResult 
	Account_TutorStudentCheck(TUTORSTUDENTCHECK vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_TutorStudentCheckResult ret = new Account_TutorStudentCheckResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.TutorStudentCheck(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Account_DelResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除帐户")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_DelResult 
	Account_Del(UNIACCOUNT vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_DelResult ret = new Account_DelResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.Del(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Account_ExtIdentAccDelResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除扩展身份人员")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_ExtIdentAccDelResult 
	Account_ExtIdentAccDel(EXTIDENTACC vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_ExtIdentAccDelResult ret = new Account_ExtIdentAccDelResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.ExtIdentAccDel(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Account_TutorStudentDelResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除导师的学生")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_TutorStudentDelResult 
	Account_TutorStudentDel(TUTORSTUDENT vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_TutorStudentDelResult ret = new Account_TutorStudentDelResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.TutorStudentDel(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Account_AccCheckResult
	{
		public uint code;
		public string Message;
		public  UNIACCOUNT vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "用户认证")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_AccCheckResult 
	Account_AccCheck(ACCCHECKREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_AccCheckResult ret = new Account_AccCheckResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.AccCheck(vrParameter, out  ret.vrRes);
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
	
	
	public struct Account_DepositResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "存退款，补助，免费机时")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_DepositResult 
	Account_Deposit(UNIDEPOSIT vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_DepositResult ret = new Account_DepositResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.Deposit(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Account_PaymentResult
	{
		public uint code;
		public string Message;
		public  UNIPAYMENT vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "支付结算提交消费流水")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_PaymentResult 
	Account_Payment(UNIPAYMENT vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_PaymentResult ret = new Account_PaymentResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.Payment(vrParameter, out  ret.vrRes);
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
	
	
	public struct Account_AccNoticeGetResult
	{
		public uint code;
		public string Message;
		public  NOTICEINFO[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取各类人员间需相互通知的信息")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_AccNoticeGetResult 
	Account_AccNoticeGet(NOTICEREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_AccNoticeGetResult ret = new Account_AccNoticeGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.AccNoticeGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Account_AccNoticeAffirmResult
	{
		public uint code;
		public string Message;
		public  NOTICEAFFIRM vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "确认通知消息已收到")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_AccNoticeAffirmResult 
	Account_AccNoticeAffirm(NOTICEAFFIRM vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_AccNoticeAffirmResult ret = new Account_AccNoticeAffirmResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.AccNoticeAffirm(vrParameter, out  ret.vrRes);
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
	
	
	public struct Account_AccExtInfoGetResult
	{
		public uint code;
		public string Message;
		public  UNIACCEXTINFO[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取审查信息")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_AccExtInfoGetResult 
	Account_AccExtInfoGet(ACCREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_AccExtInfoGetResult ret = new Account_AccExtInfoGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.AccExtInfoGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Account_MajorGetResult
	{
		public uint code;
		public string Message;
		public  UNIMAJOR[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取专业列表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_MajorGetResult 
	Account_MajorGet(MAJORREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_MajorGetResult ret = new Account_MajorGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.MajorGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Account_MajorSetResult
	{
		public uint code;
		public string Message;
		public  UNIMAJOR vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "修改专业属性")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_MajorSetResult 
	Account_MajorSet(UNIMAJOR vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_MajorSetResult ret = new Account_MajorSetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.MajorSet(vrParameter, out  ret.vrRes);
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
	
	
	public struct Account_MajorDelResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除专业")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_MajorDelResult 
	Account_MajorDel(UNIMAJOR vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_MajorDelResult ret = new Account_MajorDelResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.MajorDel(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Account_TestDataGetResult
	{
		public uint code;
		public string Message;
		public  UNITESTDATA[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取实验数据记录")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_TestDataGetResult 
	Account_TestDataGet(TESTDATAREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_TestDataGetResult ret = new Account_TestDataGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.TestDataGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Account_TestDataUploadResult
	{
		public uint code;
		public string Message;
		public  UNITESTDATA vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "上传实验数据")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_TestDataUploadResult 
	Account_TestDataUpload(UNITESTDATA vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_TestDataUploadResult ret = new Account_TestDataUploadResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.TestDataUpload(vrParameter, out  ret.vrRes);
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
	
	
	public struct Account_TestDataChgStatResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "修改实验数据状态")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_TestDataChgStatResult 
	Account_TestDataChgStat(UNITESTDATA vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_TestDataChgStatResult ret = new Account_TestDataChgStatResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.TestDataChgStat(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Account_TestDataAdminUploadResult
	{
		public uint code;
		public string Message;
		public  UNITESTDATA vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "管理员上传实验数据")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_TestDataAdminUploadResult 
	Account_TestDataAdminUpload(ADMINTESTDATA vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_TestDataAdminUploadResult ret = new Account_TestDataAdminUploadResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.TestDataAdminUpload(vrParameter, out  ret.vrRes);
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
	
	
	public struct Account_CloudDiskOpenResult
	{
		public uint code;
		public string Message;
		public  CLOUDDISK[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "打开网络硬盘")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_CloudDiskOpenResult 
	Account_CloudDiskOpen(CLOUDDISKREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_CloudDiskOpenResult ret = new Account_CloudDiskOpenResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.CloudDiskOpen(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Account_CloudDiskSaveResult
	{
		public uint code;
		public string Message;
		public  CLOUDDISK vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "保存文件到网络硬盘")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_CloudDiskSaveResult 
	Account_CloudDiskSave(CLOUDDISK vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_CloudDiskSaveResult ret = new Account_CloudDiskSaveResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.CloudDiskSave(vrParameter, out  ret.vrRes);
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
	
	
	public struct Account_CloudDiskDelResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "从网络硬盘删除文件")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_CloudDiskDelResult 
	Account_CloudDiskDel(CLOUDDISK vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_CloudDiskDelResult ret = new Account_CloudDiskDelResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.CloudDiskDel(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Account_CloudDiskStatResult
	{
		public uint code;
		public string Message;
		public  CDISKSTAT vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "网络硬盘使用统计")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_CloudDiskStatResult 
	Account_CloudDiskStat(CDISKSTATREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_CloudDiskStatResult ret = new Account_CloudDiskStatResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.CloudDiskStat(vrParameter, out  ret.vrRes);
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
	
	
	public struct Account_TeacherGetResult
	{
		public uint code;
		public string Message;
		public  UNITEACHER[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取任课教师")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_TeacherGetResult 
	Account_TeacherGet(UNITEACHERREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_TeacherGetResult ret = new Account_TeacherGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.TeacherGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Account_TeacherSetResult
	{
		public uint code;
		public string Message;
		public  UNITEACHER vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "设置任课教师")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_TeacherSetResult 
	Account_TeacherSet(UNITEACHER vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_TeacherSetResult ret = new Account_TeacherSetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.TeacherSet(vrParameter, out  ret.vrRes);
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
	
	
	public struct Account_TeacherDelResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除任课教师")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_TeacherDelResult 
	Account_TeacherDel(UNITEACHER vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_TeacherDelResult ret = new Account_TeacherDelResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.TeacherDel(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Account_GetUserCurInfoResult
	{
		public uint code;
		public string Message;
		public  USERCURINFO vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取当前用户使用情况(类似控制台刷卡）")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Account_GetUserCurInfoResult 
	Account_GetUserCurInfo(USERCURINFOREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Account_GetUserCurInfoResult ret = new Account_GetUserCurInfoResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Account.GetUserCurInfo(vrParameter, out  ret.vrRes);
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
	
	//#endregion Account部分
	

	//#region Device部分
	/*设备管理*/
	
	public struct Device_LabGetResult
	{
		public uint code;
		public string Message;
		public  UNILAB[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取实验室列表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_LabGetResult 
	Device_LabGet(LABREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_LabGetResult ret = new Device_LabGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.LabGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_FullLabGetResult
	{
		public uint code;
		public string Message;
		public  FULLLAB[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取实验室全信息列表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_FullLabGetResult 
	Device_FullLabGet(FULLLABREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_FullLabGetResult ret = new Device_FullLabGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.FullLabGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_LabSetResult
	{
		public uint code;
		public string Message;
		public  UNILAB vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "设置实验室信息")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_LabSetResult 
	Device_LabSet(UNILAB vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_LabSetResult ret = new Device_LabSetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.LabSet(vrParameter, out  ret.vrRes);
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
	
	
	public struct Device_LabDelResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除实验室")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_LabDelResult 
	Device_LabDel(UNILAB vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_LabDelResult ret = new Device_LabDelResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.LabDel(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_GetResult
	{
		public uint code;
		public string Message;
		public  UNIDEVICE[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取设备列表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_GetResult 
	Device_Get(DEVREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_GetResult ret = new Device_GetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.Get(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_GetDevForResvResult
	{
		public uint code;
		public string Message;
		public  DEVFORRESV[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取可预约设备列表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_GetDevForResvResult 
	Device_GetDevForResv(DEVFORRESVREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_GetDevForResvResult ret = new Device_GetDevForResvResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.GetDevForResv(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_GetDevKindForResvResult
	{
		public uint code;
		public string Message;
		public  DEVKINDFORRESV[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取可预约设备列表(按类型)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_GetDevKindForResvResult 
	Device_GetDevKindForResv(DEVKINDFORRESVREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_GetDevKindForResvResult ret = new Device_GetDevKindForResvResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.GetDevKindForResv(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_ResvUsableDevGetResult
	{
		public uint code;
		public string Message;
		public  RESVUSABLEDEV[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取已生效预约可用设备列表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_ResvUsableDevGetResult 
	Device_ResvUsableDevGet(RESVUSABLEDEVREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_ResvUsableDevGetResult ret = new Device_ResvUsableDevGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.ResvUsableDevGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_GetDevResvStatResult
	{
		public uint code;
		public string Message;
		public  DEVRESVSTAT[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取设备预约状态")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_GetDevResvStatResult 
	Device_GetDevResvStat(DEVRESVSTATREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_GetDevResvStatResult ret = new Device_GetDevResvStatResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.GetDevResvStat(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_GetLabResvStatResult
	{
		public uint code;
		public string Message;
		public  LABRESVSTAT[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取实验室预约状态")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_GetLabResvStatResult 
	Device_GetLabResvStat(LABRESVSTATREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_GetLabResvStatResult ret = new Device_GetLabResvStatResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.GetLabResvStat(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_GetDevLongResvStatResult
	{
		public uint code;
		public string Message;
		public  DEVLONGRESVSTAT[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取设备长期预约状态")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_GetDevLongResvStatResult 
	Device_GetDevLongResvStat(DEVLONGRESVSTATREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_GetDevLongResvStatResult ret = new Device_GetDevLongResvStatResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.GetDevLongResvStat(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_GetDevKindForLongResvResult
	{
		public uint code;
		public string Message;
		public  DEVKINDFORRESV[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取可长期预约设备列表(按类型)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_GetDevKindForLongResvResult 
	Device_GetDevKindForLongResv(DEVKINDFORLONGRESVREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_GetDevKindForLongResvResult ret = new Device_GetDevKindForLongResvResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.GetDevKindForLongResv(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_GetRoomResvStatResult
	{
		public uint code;
		public string Message;
		public  ROOMRESVSTAT[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取房间预约状态")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_GetRoomResvStatResult 
	Device_GetRoomResvStat(ROOMRESVSTATREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_GetRoomResvStatResult ret = new Device_GetRoomResvStatResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.GetRoomResvStat(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_DevFARGetResult
	{
		public uint code;
		public string Message;
		public  DEVFAR[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取设备使用费的经费分配比例")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_DevFARGetResult 
	Device_DevFARGet(DEVFARREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_DevFARGetResult ret = new Device_DevFARGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.DevFARGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_DevCfgGetResult
	{
		public uint code;
		public string Message;
		public  DEVCFG[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取设备配置表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_DevCfgGetResult 
	Device_DevCfgGet(DEVCFGREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_DevCfgGetResult ret = new Device_DevCfgGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.DevCfgGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_DevCfgKindGetResult
	{
		public uint code;
		public string Message;
		public  DEVCFGKIND[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取设备配置类别表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_DevCfgKindGetResult 
	Device_DevCfgKindGet(DEVCFGKINDREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_DevCfgKindGetResult ret = new Device_DevCfgKindGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.DevCfgKindGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_GetRoomForResvResult
	{
		public uint code;
		public string Message;
		public  ROOMFORRESV[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取可预约设备列表(按房间)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_GetRoomForResvResult 
	Device_GetRoomForResv(ROOMFORRESVREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_GetRoomForResvResult ret = new Device_GetRoomForResvResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.GetRoomForResv(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_GetRGResvStatResult
	{
		public uint code;
		public string Message;
		public  RGRESVSTAT[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取房间组合预约状态")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_GetRGResvStatResult 
	Device_GetRGResvStat(RGRESVSTATREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_GetRGResvStatResult ret = new Device_GetRGResvStatResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.GetRGResvStat(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_SetResult
	{
		public uint code;
		public string Message;
		public  UNIDEVICE vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "设置设备信息")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_SetResult 
	Device_Set(UNIDEVICE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_SetResult ret = new Device_SetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.Set(vrParameter, out  ret.vrRes);
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
	
	
	public struct Device_AttendantSetResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "设置设备值班员")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_AttendantSetResult 
	Device_AttendantSet(DEVATTENDANT vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_AttendantSetResult ret = new Device_AttendantSetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.AttendantSet(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_DevFARSetResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "设置设备使用费的经费分配比例")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_DevFARSetResult 
	Device_DevFARSet(DEVFAR vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_DevFARSetResult ret = new Device_DevFARSetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.DevFARSet(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_DevCfgSetResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "设置设备配置表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_DevCfgSetResult 
	Device_DevCfgSet(DEVCFG vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_DevCfgSetResult ret = new Device_DevCfgSetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.DevCfgSet(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_UploadSeatDetectStatResult
	{
		public uint code;
		public string Message;
		
	}
	
	
	public struct Device_DelResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除设备")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_DelResult 
	Device_Del(UNIDEVICE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_DelResult ret = new Device_DelResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.Del(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_DevCfgDelResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "设置设备配置表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_DevCfgDelResult 
	Device_DevCfgDel(DEVCFG vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_DevCfgDelResult ret = new Device_DevCfgDelResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.DevCfgDel(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_DevManUseResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "管理员人工管理设备使用")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_DevManUseResult 
	Device_DevManUse(DEVMANUSE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_DevManUseResult ret = new Device_DevManUseResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.DevManUse(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_DevQueryResult
	{
		public uint code;
		public string Message;
		public  UNIACCTINFO vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "用户从客户端查询使用信息")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_DevQueryResult 
	Device_DevQuery(DEVQUERYREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_DevQueryResult ret = new Device_DevQueryResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.DevQuery(vrParameter, out  ret.vrRes);
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
	
	
	public struct Device_PCSWUploadResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "上传机器软件信息")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_PCSWUploadResult 
	Device_PCSWUpload(PCPROGRAM vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_PCSWUploadResult ret = new Device_PCSWUploadResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.PCSWUpload(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_CheckURLResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "上网认证（是否允许访问）")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_CheckURLResult 
	Device_CheckURL(URLCHECKINFO vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_CheckURLResult ret = new Device_CheckURLResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.CheckURL(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_ClientChgPwResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "客户端修改密码")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_ClientChgPwResult 
	Device_ClientChgPw(CLTCHGPWINFO vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_ClientChgPwResult ret = new Device_ClientChgPwResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.ClientChgPw(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_ProgramBeginResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "程序开始运行")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_ProgramBeginResult 
	Device_ProgramBegin(PCPROGRAM vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_ProgramBeginResult ret = new Device_ProgramBeginResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.ProgramBegin(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_ProgramEndResult
	{
		public uint code;
		public string Message;
		
	}
	
	
	public struct Device_DevCtrlResult
	{
		public uint code;
		public string Message;
		public  DEVCTRLINFO vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "服务端控制设备，要求设备执行某操作")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_DevCtrlResult 
	Device_DevCtrl(DEVCTRLINFO vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_DevCtrlResult ret = new Device_DevCtrlResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.DevCtrl(vrParameter, out  ret.vrRes);
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
	
	
	public struct Device_URLCtrlResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "设置上网监控模式")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_URLCtrlResult 
	Device_URLCtrl(CTRLREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_URLCtrlResult ret = new Device_URLCtrlResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.URLCtrl(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_SWCtrlResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "设置软件监控模式")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_SWCtrlResult 
	Device_SWCtrl(CTRLREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_SWCtrlResult ret = new Device_SWCtrlResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.SWCtrl(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_GetRunAppResult
	{
		public uint code;
		public string Message;
		public  RUNAPP[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取正在运行程序")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_GetRunAppResult 
	Device_GetRunApp(RUNAPPREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_GetRunAppResult ret = new Device_GetRunAppResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.GetRunApp(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_PCSWUploadEndResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "上传机器软件信息结束")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_PCSWUploadEndResult 
	Device_PCSWUploadEnd(SWUPLOADEND vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_PCSWUploadEndResult ret = new Device_PCSWUploadEndResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.PCSWUploadEnd(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_DevLoanResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "借出设备")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_DevLoanResult 
	Device_DevLoan(DEVLOANREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_DevLoanResult ret = new Device_DevLoanResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.DevLoan(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_DevReturnResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "归还设备")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_DevReturnResult 
	Device_DevReturn(DEVRETURNREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_DevReturnResult ret = new Device_DevReturnResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.DevReturn(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_DevDamageRecGetResult
	{
		public uint code;
		public string Message;
		public  DEVDAMAGEREC[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取设备损坏记录")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_DevDamageRecGetResult 
	Device_DevDamageRecGet(DEVDAMAGERECREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_DevDamageRecGetResult ret = new Device_DevDamageRecGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.DevDamageRecGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_DeviceRepairResult
	{
		public uint code;
		public string Message;
		public  DEVDAMAGEREC vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "设备维修处理")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_DeviceRepairResult 
	Device_DeviceRepair(DEVDAMAGEREC vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_DeviceRepairResult ret = new Device_DeviceRepairResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.DeviceRepair(vrParameter, out  ret.vrRes);
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
	
	
	public struct Device_DevCtrlResResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "设备对控制结果的反馈")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_DevCtrlResResult 
	Device_DevCtrlRes(DEVCTRLINFO vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_DevCtrlResResult ret = new Device_DevCtrlResResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.DevCtrlRes(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_DevClsGetResult
	{
		public uint code;
		public string Message;
		public  UNIDEVCLS[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取设备功能类别列表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_DevClsGetResult 
	Device_DevClsGet(DEVCLSREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_DevClsGetResult ret = new Device_DevClsGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.DevClsGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_DevClsSetResult
	{
		public uint code;
		public string Message;
		public  UNIDEVCLS vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "设置设备功能类别信息")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_DevClsSetResult 
	Device_DevClsSet(UNIDEVCLS vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_DevClsSetResult ret = new Device_DevClsSetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.DevClsSet(vrParameter, out  ret.vrRes);
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
	
	
	public struct Device_DevClsDelResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除设备功能类别")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_DevClsDelResult 
	Device_DevClsDel(UNIDEVCLS vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_DevClsDelResult ret = new Device_DevClsDelResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.DevClsDel(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_DevKindGetResult
	{
		public uint code;
		public string Message;
		public  UNIDEVKIND[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取设备名称类别列表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_DevKindGetResult 
	Device_DevKindGet(DEVKINDREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_DevKindGetResult ret = new Device_DevKindGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.DevKindGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_DevKindSetResult
	{
		public uint code;
		public string Message;
		public  UNIDEVKIND vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "设置设备名称类别信息")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_DevKindSetResult 
	Device_DevKindSet(UNIDEVKIND vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_DevKindSetResult ret = new Device_DevKindSetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.DevKindSet(vrParameter, out  ret.vrRes);
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
	
	
	public struct Device_DevKindDelResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除设备名称类别")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_DevKindDelResult 
	Device_DevKindDel(UNIDEVKIND vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_DevKindDelResult ret = new Device_DevKindDelResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.DevKindDel(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_BuildingGetResult
	{
		public uint code;
		public string Message;
		public  UNIBUILDING[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取楼宇信息")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_BuildingGetResult 
	Device_BuildingGet(BUILDINGREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_BuildingGetResult ret = new Device_BuildingGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.BuildingGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_BuildingSetResult
	{
		public uint code;
		public string Message;
		public  UNIBUILDING vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "设置楼宇")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_BuildingSetResult 
	Device_BuildingSet(UNIBUILDING vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_BuildingSetResult ret = new Device_BuildingSetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.BuildingSet(vrParameter, out  ret.vrRes);
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
	
	
	public struct Device_BuildingDelResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除楼宇")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_BuildingDelResult 
	Device_BuildingDel(UNIBUILDING vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_BuildingDelResult ret = new Device_BuildingDelResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.BuildingDel(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_RoomGetResult
	{
		public uint code;
		public string Message;
		public  UNIROOM[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取房间信息")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_RoomGetResult 
	Device_RoomGet(ROOMREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_RoomGetResult ret = new Device_RoomGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.RoomGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_RoomSetResult
	{
		public uint code;
		public string Message;
		public  UNIROOM vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "设置房间")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_RoomSetResult 
	Device_RoomSet(UNIROOM vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_RoomSetResult ret = new Device_RoomSetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.RoomSet(vrParameter, out  ret.vrRes);
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
	
	
	public struct Device_RoomDelResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除房间")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_RoomDelResult 
	Device_RoomDel(UNIROOM vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_RoomDelResult ret = new Device_RoomDelResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.RoomDel(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_RoomCtrlResult
	{
		public uint code;
		public string Message;
		public  ROOMCTRLINFO vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "服务端控制房间，要求房间执行某操作")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_RoomCtrlResult 
	Device_RoomCtrl(ROOMCTRLINFO vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_RoomCtrlResult ret = new Device_RoomCtrlResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.RoomCtrl(vrParameter, out  ret.vrRes);
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
	
	
	public struct Device_GetPermitRoomResult
	{
		public uint code;
		public string Message;
		public  PERMITROOMINFO[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取用户可进入房间")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_GetPermitRoomResult 
	Device_GetPermitRoom(PERMITROOMREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_GetPermitRoomResult ret = new Device_GetPermitRoomResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.GetPermitRoom(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_GetRoomCtrlInfoResult
	{
		public uint code;
		public string Message;
		public  UNIDOORCTRL[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取房间控制信息")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_GetRoomCtrlInfoResult 
	Device_GetRoomCtrlInfo(ROOMCTRLREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_GetRoomCtrlInfoResult ret = new Device_GetRoomCtrlInfoResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.GetRoomCtrlInfo(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_FullRoomGetResult
	{
		public uint code;
		public string Message;
		public  FULLROOM[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取房间完整信息")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_FullRoomGetResult 
	Device_FullRoomGet(FULLROOMREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_FullRoomGetResult ret = new Device_FullRoomGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.FullRoomGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_BasicRoomGetResult
	{
		public uint code;
		public string Message;
		public  BASICROOM[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取房间名称信息（用于下拉框或复选框）")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_BasicRoomGetResult 
	Device_BasicRoomGet(BASICROOMREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_BasicRoomGetResult ret = new Device_BasicRoomGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.BasicRoomGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_ChannelGateGetResult
	{
		public uint code;
		public string Message;
		public  UNICHANNELGATE[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取通道门信息")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_ChannelGateGetResult 
	Device_ChannelGateGet(CHANNELGATEREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_ChannelGateGetResult ret = new Device_ChannelGateGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.ChannelGateGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_ChannelGateSetResult
	{
		public uint code;
		public string Message;
		public  UNICHANNELGATE vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "设置通道门")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_ChannelGateSetResult 
	Device_ChannelGateSet(UNICHANNELGATE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_ChannelGateSetResult ret = new Device_ChannelGateSetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.ChannelGateSet(vrParameter, out  ret.vrRes);
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
	
	
	public struct Device_ChannelGateDelResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除通道门")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_ChannelGateDelResult 
	Device_ChannelGateDel(UNICHANNELGATE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_ChannelGateDelResult ret = new Device_ChannelGateDelResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.ChannelGateDel(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_ChannelGateCtrlResult
	{
		public uint code;
		public string Message;
		public  CHANNELGATECTRLINFO vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "服务端控制通道门，要求通道门执行某操作")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_ChannelGateCtrlResult 
	Device_ChannelGateCtrl(CHANNELGATECTRLINFO vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_ChannelGateCtrlResult ret = new Device_ChannelGateCtrlResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.ChannelGateCtrl(vrParameter, out  ret.vrRes);
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
	
	
	public struct Device_RoomGroupGetResult
	{
		public uint code;
		public string Message;
		public  ROOMGROUP[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取房间组合")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_RoomGroupGetResult 
	Device_RoomGroupGet(ROOMGROUPREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_RoomGroupGetResult ret = new Device_RoomGroupGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.RoomGroupGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_RoomGroupSetResult
	{
		public uint code;
		public string Message;
		public  ROOMGROUP vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "设置房间组合")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_RoomGroupSetResult 
	Device_RoomGroupSet(ROOMGROUP vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_RoomGroupSetResult ret = new Device_RoomGroupSetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.RoomGroupSet(vrParameter, out  ret.vrRes);
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
	
	
	public struct Device_RoomGroupDelResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除房间组合")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_RoomGroupDelResult 
	Device_RoomGroupDel(ROOMGROUP vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_RoomGroupDelResult ret = new Device_RoomGroupDelResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.RoomGroupDel(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_DevMonitorGetResult
	{
		public uint code;
		public string Message;
		public  DEVMONITOR[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取设备监控器")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_DevMonitorGetResult 
	Device_DevMonitorGet(DEVMONITORREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_DevMonitorGetResult ret = new Device_DevMonitorGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.DevMonitorGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_DevMonitorSetResult
	{
		public uint code;
		public string Message;
		public  DEVMONITOR vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "设置设备监控器")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_DevMonitorSetResult 
	Device_DevMonitorSet(DEVMONITOR vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_DevMonitorSetResult ret = new Device_DevMonitorSetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.DevMonitorSet(vrParameter, out  ret.vrRes);
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
	
	
	public struct Device_DevMonitorDelResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除设备监控器")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_DevMonitorDelResult 
	Device_DevMonitorDel(DEVMONITOR vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_DevMonitorDelResult ret = new Device_DevMonitorDelResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.DevMonitorDel(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_MonDevGetResult
	{
		public uint code;
		public string Message;
		public  MONDEV[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取监控器与设备的对应关系")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_MonDevGetResult 
	Device_MonDevGet(MONDEVREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_MonDevGetResult ret = new Device_MonDevGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.MonDevGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_MonDevSetResult
	{
		public uint code;
		public string Message;
		public  MONDEV vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "设置监控器与设备的对应关系")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_MonDevSetResult 
	Device_MonDevSet(MONDEV vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_MonDevSetResult ret = new Device_MonDevSetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.MonDevSet(vrParameter, out  ret.vrRes);
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
	
	
	public struct Device_MonDevDelResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除监控器与设备的对应关系")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_MonDevDelResult 
	Device_MonDevDel(MONDEV vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_MonDevDelResult ret = new Device_MonDevDelResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.MonDevDel(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_DevOpenRuleGetResult
	{
		public uint code;
		public string Message;
		public  DEVOPENRULE[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取设备开放时间表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_DevOpenRuleGetResult 
	Device_DevOpenRuleGet(DEVOPENRULEREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_DevOpenRuleGetResult ret = new Device_DevOpenRuleGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.DevOpenRuleGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_DevOpenRuleSetResult
	{
		public uint code;
		public string Message;
		public  DEVOPENRULE vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "设置设备开放时间表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_DevOpenRuleSetResult 
	Device_DevOpenRuleSet(DEVOPENRULE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_DevOpenRuleSetResult ret = new Device_DevOpenRuleSetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.DevOpenRuleSet(vrParameter, out  ret.vrRes);
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
	
	
	public struct Device_DevOpenRuleDelResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除设备开放时间表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_DevOpenRuleDelResult 
	Device_DevOpenRuleDel(DEVOPENRULE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_DevOpenRuleDelResult ret = new Device_DevOpenRuleDelResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.DevOpenRuleDel(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_GroupOpenRuleSetResult
	{
		public uint code;
		public string Message;
		public  CHANGEGROUPOPENRULE vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "设置组开放时间表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_GroupOpenRuleSetResult 
	Device_GroupOpenRuleSet(CHANGEGROUPOPENRULE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_GroupOpenRuleSetResult ret = new Device_GroupOpenRuleSetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.GroupOpenRuleSet(vrParameter, out  ret.vrRes);
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
	
	
	public struct Device_GroupOpenRuleDelResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除组开放时间表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_GroupOpenRuleDelResult 
	Device_GroupOpenRuleDel(CHANGEGROUPOPENRULE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_GroupOpenRuleDelResult ret = new Device_GroupOpenRuleDelResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.GroupOpenRuleDel(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_GroupOpenRuleGetResult
	{
		public uint code;
		public string Message;
		public  GROUPOPENRULE[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取组开放时间表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_GroupOpenRuleGetResult 
	Device_GroupOpenRuleGet(GROUPOPENRULEREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_GroupOpenRuleGetResult ret = new Device_GroupOpenRuleGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.GroupOpenRuleGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_PeriodOpenRuleSetResult
	{
		public uint code;
		public string Message;
		public  CHANGEPERIODOPENRULE vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "设置时间期间开放时间表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_PeriodOpenRuleSetResult 
	Device_PeriodOpenRuleSet(CHANGEPERIODOPENRULE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_PeriodOpenRuleSetResult ret = new Device_PeriodOpenRuleSetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.PeriodOpenRuleSet(vrParameter, out  ret.vrRes);
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
	
	
	public struct Device_PeriodOpenRuleDelResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除时间期间开放时间表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_PeriodOpenRuleDelResult 
	Device_PeriodOpenRuleDel(CHANGEPERIODOPENRULE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_PeriodOpenRuleDelResult ret = new Device_PeriodOpenRuleDelResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.PeriodOpenRuleDel(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_PeriodOpenRuleGetResult
	{
		public uint code;
		public string Message;
		public  PERIODOPENRULE[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取时间期间开放时间表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_PeriodOpenRuleGetResult 
	Device_PeriodOpenRuleGet(PERIODOPENRULEREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_PeriodOpenRuleGetResult ret = new Device_PeriodOpenRuleGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.PeriodOpenRuleGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_CurDevStatResult
	{
		public uint code;
		public string Message;
		public  CURDEVSTAT vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取当前设备统计信息")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_CurDevStatResult 
	Device_CurDevStat()
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_CurDevStatResult ret = new Device_CurDevStatResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.CurDevStat( out  ret.vrRes);
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
	
	
	public struct Device_DevForTeachingStatResult
	{
		public uint code;
		public string Message;
		public  DEVSECINFO[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取教学用设备按节次统计")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_DevForTeachingStatResult 
	Device_DevForTeachingStat(DEVFORTREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_DevForTeachingStatResult ret = new Device_DevForTeachingStatResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.DevForTeachingStat(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_TeachingDevGetResult
	{
		public uint code;
		public string Message;
		public  TEACHINGDEV[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取教学用设备")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_TeachingDevGetResult 
	Device_TeachingDevGet(TEACHINGDEVREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_TeachingDevGetResult ret = new Device_TeachingDevGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.TeachingDevGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_RewardRecGetResult
	{
		public uint code;
		public string Message;
		public  REWARDREC[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取获奖记录")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_RewardRecGetResult 
	Device_RewardRecGet(REWARDRECREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_RewardRecGetResult ret = new Device_RewardRecGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.RewardRecGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_RewardRecSetResult
	{
		public uint code;
		public string Message;
		public  REWARDREC vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "设置获奖记录")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_RewardRecSetResult 
	Device_RewardRecSet(REWARDREC vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_RewardRecSetResult ret = new Device_RewardRecSetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.RewardRecSet(vrParameter, out  ret.vrRes);
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
	
	
	public struct Device_RewardRecDelResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除获奖记录")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_RewardRecDelResult 
	Device_RewardRecDel(REWARDREC vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_RewardRecDelResult ret = new Device_RewardRecDelResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.RewardRecDel(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_CostRecGetResult
	{
		public uint code;
		public string Message;
		public  COSTREC[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取费用记录")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_CostRecGetResult 
	Device_CostRecGet(COSTRECREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_CostRecGetResult ret = new Device_CostRecGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.CostRecGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Device_CostRecSetResult
	{
		public uint code;
		public string Message;
		public  COSTREC vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "设置费用记录")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_CostRecSetResult 
	Device_CostRecSet(COSTREC vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_CostRecSetResult ret = new Device_CostRecSetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.CostRecSet(vrParameter, out  ret.vrRes);
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
	
	
	public struct Device_CostRecDelResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除费用记录")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Device_CostRecDelResult 
	Device_CostRecDel(COSTREC vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Device_CostRecDelResult ret = new Device_CostRecDelResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Device.CostRecDel(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	//#endregion Device部分
	

	//#region DoorCtrlSrv部分
	/*门禁设置管理*/
	
	public struct DoorCtrlSrv_GetDCSResult
	{
		public uint code;
		public string Message;
		public  UNIDCS[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取门禁集控器")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public DoorCtrlSrv_GetDCSResult 
	DoorCtrlSrv_GetDCS(DCSREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		DoorCtrlSrv_GetDCSResult ret = new DoorCtrlSrv_GetDCSResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.DoorCtrlSrv.GetDCS(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct DoorCtrlSrv_SetDCSResult
	{
		public uint code;
		public string Message;
		public  UNIDCS vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "设置门禁集控器")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public DoorCtrlSrv_SetDCSResult 
	DoorCtrlSrv_SetDCS(UNIDCS vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		DoorCtrlSrv_SetDCSResult ret = new DoorCtrlSrv_SetDCSResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.DoorCtrlSrv.SetDCS(vrParameter, out  ret.vrRes);
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
	
	
	public struct DoorCtrlSrv_DelDCSResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除门禁集控器")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public DoorCtrlSrv_DelDCSResult 
	DoorCtrlSrv_DelDCS(UNIDCS vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		DoorCtrlSrv_DelDCSResult ret = new DoorCtrlSrv_DelDCSResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.DoorCtrlSrv.DelDCS(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct DoorCtrlSrv_LoginResult
	{
		public uint code;
		public string Message;
		public  DCSLOGINRES vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "门禁集控器登录")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public DoorCtrlSrv_LoginResult 
	DoorCtrlSrv_Login(DCSLOGINREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		DoorCtrlSrv_LoginResult ret = new DoorCtrlSrv_LoginResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.DoorCtrlSrv.Login(vrParameter, out  ret.vrRes);
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
	
	
	public struct DoorCtrlSrv_LogoutResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "门禁集控器退出")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public DoorCtrlSrv_LogoutResult 
	DoorCtrlSrv_Logout(DCSLOGOUTREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		DoorCtrlSrv_LogoutResult ret = new DoorCtrlSrv_LogoutResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.DoorCtrlSrv.Logout(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct DoorCtrlSrv_PulseResult
	{
		public uint code;
		public string Message;
		public  DCSPULSERES vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "定时通信")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public DoorCtrlSrv_PulseResult 
	DoorCtrlSrv_Pulse(DCSPULSEREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		DoorCtrlSrv_PulseResult ret = new DoorCtrlSrv_PulseResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.DoorCtrlSrv.Pulse(vrParameter, out  ret.vrRes);
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
	
	
	public struct DoorCtrlSrv_DoorCardResult
	{
		public uint code;
		public string Message;
		public  DOORCARDRES vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "用户在门禁刷卡器上刷卡")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public DoorCtrlSrv_DoorCardResult 
	DoorCtrlSrv_DoorCard(DOORCARDREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		DoorCtrlSrv_DoorCardResult ret = new DoorCtrlSrv_DoorCardResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.DoorCtrlSrv.DoorCard(vrParameter, out  ret.vrRes);
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
	
	
	public struct DoorCtrlSrv_MobilOpenDoorResult
	{
		public uint code;
		public string Message;
		public  MOBILEOPENDOORRES vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "用户用手机扫二维码开门")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public DoorCtrlSrv_MobilOpenDoorResult 
	DoorCtrlSrv_MobilOpenDoor(MOBILEOPENDOORREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		DoorCtrlSrv_MobilOpenDoorResult ret = new DoorCtrlSrv_MobilOpenDoorResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.DoorCtrlSrv.MobilOpenDoor(vrParameter, out  ret.vrRes);
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
	
	
	public struct DoorCtrlSrv_GetDoorCtrlResult
	{
		public uint code;
		public string Message;
		public  UNIDOORCTRL[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取门禁控制器")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public DoorCtrlSrv_GetDoorCtrlResult 
	DoorCtrlSrv_GetDoorCtrl(DOORCTRLREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		DoorCtrlSrv_GetDoorCtrlResult ret = new DoorCtrlSrv_GetDoorCtrlResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.DoorCtrlSrv.GetDoorCtrl(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct DoorCtrlSrv_SetDoorCtrlResult
	{
		public uint code;
		public string Message;
		public  UNIDOORCTRL vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "设置门禁控制器")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public DoorCtrlSrv_SetDoorCtrlResult 
	DoorCtrlSrv_SetDoorCtrl(UNIDOORCTRL vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		DoorCtrlSrv_SetDoorCtrlResult ret = new DoorCtrlSrv_SetDoorCtrlResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.DoorCtrlSrv.SetDoorCtrl(vrParameter, out  ret.vrRes);
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
	
	
	public struct DoorCtrlSrv_DelDoorCtrlResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除门禁控制器")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public DoorCtrlSrv_DelDoorCtrlResult 
	DoorCtrlSrv_DelDoorCtrl(UNIDOORCTRL vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		DoorCtrlSrv_DelDoorCtrlResult ret = new DoorCtrlSrv_DelDoorCtrlResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.DoorCtrlSrv.DelDoorCtrl(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct DoorCtrlSrv_DoorCtrlCmdResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "给门禁控制器发命令")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public DoorCtrlSrv_DoorCtrlCmdResult 
	DoorCtrlSrv_DoorCtrlCmd(DOORCTRLCMD vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		DoorCtrlSrv_DoorCtrlCmdResult ret = new DoorCtrlSrv_DoorCtrlCmdResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.DoorCtrlSrv.DoorCtrlCmd(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	//#endregion DoorCtrlSrv部分
	

	//#region Group部分
	/*用户组管理*/
	
	public struct Group_GetGroupResult
	{
		public uint code;
		public string Message;
		public  UNIGROUP[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取组")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Group_GetGroupResult 
	Group_GetGroup(GROUPREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Group_GetGroupResult ret = new Group_GetGroupResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Group.GetGroup(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Group_SetGroupResult
	{
		public uint code;
		public string Message;
		public  UNIGROUP vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "修改组")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Group_SetGroupResult 
	Group_SetGroup(UNIGROUP vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Group_SetGroupResult ret = new Group_SetGroupResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Group.SetGroup(vrParameter, out  ret.vrRes);
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
	
	
	public struct Group_DelGroupResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除组")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Group_DelGroupResult 
	Group_DelGroup(UNIGROUP vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Group_DelGroupResult ret = new Group_DelGroupResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Group.DelGroup(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Group_SetGroupMemberResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "添加组成员")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Group_SetGroupMemberResult 
	Group_SetGroupMember(GROUPMEMBER vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Group_SetGroupMemberResult ret = new Group_SetGroupMemberResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Group.SetGroupMember(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Group_DelGroupMemberResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除组成员")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Group_DelGroupMemberResult 
	Group_DelGroupMember(GROUPMEMBER vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Group_DelGroupMemberResult ret = new Group_DelGroupMemberResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Group.DelGroupMember(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Group_GetGroupMemDetailResult
	{
		public uint code;
		public string Message;
		public  GROUPMEMDETAIL[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取组成员明细")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Group_GetGroupMemDetailResult 
	Group_GetGroupMemDetail(GROUPMEMDETAILREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Group_GetGroupMemDetailResult ret = new Group_GetGroupMemDetailResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Group.GetGroupMemDetail(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	//#endregion Group部分
	

	//#region Reserve部分
	/*预约管理*/
	
	public struct Reserve_GetResult
	{
		public uint code;
		public string Message;
		public  UNIRESERVE[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取预约列表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_GetResult 
	Reserve_Get(RESVREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_GetResult ret = new Reserve_GetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.Get(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_GetReserveForShowResult
	{
		public uint code;
		public string Message;
		public  RESVSHOW[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取预约列表用于网站显示")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_GetReserveForShowResult 
	Reserve_GetReserveForShow(RESVSHOWREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_GetReserveForShowResult ret = new Reserve_GetReserveForShowResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.GetReserveForShow(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_GetTeachingResvResult
	{
		public uint code;
		public string Message;
		public  TEACHINGRESV[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取教学预约列表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_GetTeachingResvResult 
	Reserve_GetTeachingResv(TEACHINGRESVREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_GetTeachingResvResult ret = new Reserve_GetTeachingResvResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.GetTeachingResv(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_GetRTResvResult
	{
		public uint code;
		public string Message;
		public  RTRESV[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取科研实验预约列表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_GetRTResvResult 
	Reserve_GetRTResv(RTRESVREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_GetRTResvResult ret = new Reserve_GetRTResvResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.GetRTResv(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_GetRTResvBillResult
	{
		public uint code;
		public string Message;
		public  RTRESVBILL vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取科研实验账单")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_GetRTResvBillResult 
	Reserve_GetRTResvBill(RTRESVBILLREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_GetRTResvBillResult ret = new Reserve_GetRTResvBillResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.GetRTResvBill(vrParameter, out  ret.vrRes);
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
	
	
	public struct Reserve_GetResvTimeResult
	{
		public uint code;
		public string Message;
		public  RESVTIME[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取科研实验账单")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_GetResvTimeResult 
	Reserve_GetResvTime(RESVTIMEREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_GetResvTimeResult ret = new Reserve_GetResvTimeResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.GetResvTime(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_SetResult
	{
		public uint code;
		public string Message;
		public  UNIRESERVE vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "设置预约信息")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_SetResult 
	Reserve_Set(UNIRESERVE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_SetResult ret = new Reserve_SetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.Set(vrParameter, out  ret.vrRes);
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
	
	
	public struct Reserve_AutoResult
	{
		public uint code;
		public string Message;
		public  UNIRESERVE vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "自动预约，系统自动分配设备")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_AutoResult 
	Reserve_Auto(AUTORESVREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_AutoResult ret = new Reserve_AutoResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.Auto(vrParameter, out  ret.vrRes);
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
	
	
	public struct Reserve_HolidayShiftResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "放假调休(比如10月5日（星期五）调到9月29日（星期六）")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_HolidayShiftResult 
	Reserve_HolidayShift(HOLIDAYSHIFT vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_HolidayShiftResult ret = new Reserve_HolidayShiftResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.HolidayShift(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_JoinToResvMemberResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "加入预约小组")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_JoinToResvMemberResult 
	Reserve_JoinToResvMember(RESVMEMBER vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_JoinToResvMemberResult ret = new Reserve_JoinToResvMemberResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.JoinToResvMember(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_ExitFromResvMemberResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "退出预约小组")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_ExitFromResvMemberResult 
	Reserve_ExitFromResvMember(RESVMEMBER vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_ExitFromResvMemberResult ret = new Reserve_ExitFromResvMemberResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.ExitFromResvMember(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_CancelResvSignResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "取消预约签到限制")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_CancelResvSignResult 
	Reserve_CancelResvSign(UNIRESERVE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_CancelResvSignResult ret = new Reserve_CancelResvSignResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.CancelResvSign(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_SetRTResvResult
	{
		public uint code;
		public string Message;
		public  RTRESV vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "新建修改科研实验预约")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_SetRTResvResult 
	Reserve_SetRTResv(RTRESV vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_SetRTResvResult ret = new Reserve_SetRTResvResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.SetRTResv(vrParameter, out  ret.vrRes);
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
	
	
	public struct Reserve_RTResvCheckResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "科研实验预约审核")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_RTResvCheckResult 
	Reserve_RTResvCheck(RTRESVCHECK vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_RTResvCheckResult ret = new Reserve_RTResvCheckResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.RTResvCheck(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_PrepayRTResvResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "科研实验预收费")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_PrepayRTResvResult 
	Reserve_PrepayRTResv(RTPREPAY vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_PrepayRTResvResult ret = new Reserve_PrepayRTResvResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.PrepayRTResv(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_RTBillCheckResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "科研实验账单审核")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_RTBillCheckResult 
	Reserve_RTBillCheck(RTBILLCHECK vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_RTBillCheckResult ret = new Reserve_RTBillCheckResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.RTBillCheck(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_RTBillSettleResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "科研实验账单结算")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_RTBillSettleResult 
	Reserve_RTBillSettle(RTBILLSETTLE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_RTBillSettleResult ret = new Reserve_RTBillSettleResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.RTBillSettle(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_RTBillReceiveResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "科研实验账单入账")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_RTBillReceiveResult 
	Reserve_RTBillReceive(RTBILLRECEIVE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_RTBillReceiveResult ret = new Reserve_RTBillReceiveResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.RTBillReceive(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_AnonymousResvSetResult
	{
		public uint code;
		public string Message;
		public  ANONYMOUSRESV vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "设置免登录预约")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_AnonymousResvSetResult 
	Reserve_AnonymousResvSet(ANONYMOUSRESV vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_AnonymousResvSetResult ret = new Reserve_AnonymousResvSetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.AnonymousResvSet(vrParameter, out  ret.vrRes);
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
	
	
	public struct Reserve_AllUserResvSetResult
	{
		public uint code;
		public string Message;
		public  ALLUSERRESV vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "设置全体学生预约")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_AllUserResvSetResult 
	Reserve_AllUserResvSet(ALLUSERRESV vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_AllUserResvSetResult ret = new Reserve_AllUserResvSetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.AllUserResvSet(vrParameter, out  ret.vrRes);
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
	
	
	public struct Reserve_DelResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除预约")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_DelResult 
	Reserve_Del(UNIRESERVE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_DelResult ret = new Reserve_DelResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.Del(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_DelRTResvResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除科研实验预约")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_DelRTResvResult 
	Reserve_DelRTResv(RTRESV vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_DelRTResvResult ret = new Reserve_DelRTResvResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.DelRTResv(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_ResvEarlyEndResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "提前结束预约")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_ResvEarlyEndResult 
	Reserve_ResvEarlyEnd(UNIRESERVE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_ResvEarlyEndResult ret = new Reserve_ResvEarlyEndResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.ResvEarlyEnd(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_ResvChgEndTimeResult
	{
		public uint code;
		public string Message;
		public  RESVENDTIME vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "调整预约结束时间")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_ResvChgEndTimeResult 
	Reserve_ResvChgEndTime(RESVENDTIME vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_ResvChgEndTimeResult ret = new Reserve_ResvChgEndTimeResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.ResvChgEndTime(vrParameter, out  ret.vrRes);
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
	
	
	public struct Reserve_DevResvGetResult
	{
		public uint code;
		public string Message;
		public  DEVRESVINFO[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取设备预约表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_DevResvGetResult 
	Reserve_DevResvGet(DEVRESVREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_DevResvGetResult ret = new Reserve_DevResvGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.DevResvGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_ResvCostAdjustResult
	{
		public uint code;
		public string Message;
		public  RESVCOSTADJUST vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "预约费用核算,计算本次预约使用后的实际发生费用。")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_ResvCostAdjustResult 
	Reserve_ResvCostAdjust(RESVCOSTADJUST vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_ResvCostAdjustResult ret = new Reserve_ResvCostAdjustResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.ResvCostAdjust(vrParameter, out  ret.vrRes);
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
	
	
	public struct Reserve_ResvCheckOutResult
	{
		public uint code;
		public string Message;
		public  RESVCHECKOUT vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "预约费用结算,与使用者结算本次预约发生的费用。")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_ResvCheckOutResult 
	Reserve_ResvCheckOut(RESVCHECKOUT vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_ResvCheckOutResult ret = new Reserve_ResvCheckOutResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.ResvCheckOut(vrParameter, out  ret.vrRes);
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
	
	
	public struct Reserve_GetTermResult
	{
		public uint code;
		public string Message;
		public  UNITERM[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取学期表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_GetTermResult 
	Reserve_GetTerm(TERMREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_GetTermResult ret = new Reserve_GetTermResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.GetTerm(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_SetTermResult
	{
		public uint code;
		public string Message;
		public  UNITERM vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "设置学期表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_SetTermResult 
	Reserve_SetTerm(UNITERM vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_SetTermResult ret = new Reserve_SetTermResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.SetTerm(vrParameter, out  ret.vrRes);
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
	
	
	public struct Reserve_DelTermResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除学期表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_DelTermResult 
	Reserve_DelTerm(UNITERM vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_DelTermResult ret = new Reserve_DelTermResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.DelTerm(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_GetClassTimeTableResult
	{
		public uint code;
		public string Message;
		public  CLASSTIMETABLE[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取作息表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_GetClassTimeTableResult 
	Reserve_GetClassTimeTable(CTSREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_GetClassTimeTableResult ret = new Reserve_GetClassTimeTableResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.GetClassTimeTable(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_SetClassTimeTableResult
	{
		public uint code;
		public string Message;
		
	}
	
	
	public struct Reserve_GetCourseResult
	{
		public uint code;
		public string Message;
		public  UNICOURSE[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取课程")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_GetCourseResult 
	Reserve_GetCourse(COURSEREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_GetCourseResult ret = new Reserve_GetCourseResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.GetCourse(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_SetCourseResult
	{
		public uint code;
		public string Message;
		public  UNICOURSE vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "设置课程")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_SetCourseResult 
	Reserve_SetCourse(UNICOURSE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_SetCourseResult ret = new Reserve_SetCourseResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.SetCourse(vrParameter, out  ret.vrRes);
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
	
	
	public struct Reserve_DelCourseResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除课程")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_DelCourseResult 
	Reserve_DelCourse(UNICOURSE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_DelCourseResult ret = new Reserve_DelCourseResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.DelCourse(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_GetTestCardResult
	{
		public uint code;
		public string Message;
		public  TESTCARD[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取实验项目卡")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_GetTestCardResult 
	Reserve_GetTestCard(TESTCARDREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_GetTestCardResult ret = new Reserve_GetTestCardResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.GetTestCard(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_SetTestCardResult
	{
		public uint code;
		public string Message;
		public  TESTCARD vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "设置实验项目卡")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_SetTestCardResult 
	Reserve_SetTestCard(TESTCARD vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_SetTestCardResult ret = new Reserve_SetTestCardResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.SetTestCard(vrParameter, out  ret.vrRes);
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
	
	
	public struct Reserve_DelTestCardResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除实验项目卡")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_DelTestCardResult 
	Reserve_DelTestCard(TESTCARD vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_DelTestCardResult ret = new Reserve_DelTestCardResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.DelTestCard(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_GetTestPlanResult
	{
		public uint code;
		public string Message;
		public  UNITESTPLAN[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取实验计划")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_GetTestPlanResult 
	Reserve_GetTestPlan(TESTPLANREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_GetTestPlanResult ret = new Reserve_GetTestPlanResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.GetTestPlan(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_SetTestPlanResult
	{
		public uint code;
		public string Message;
		public  UNITESTPLAN vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "设置实验计划")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_SetTestPlanResult 
	Reserve_SetTestPlan(UNITESTPLAN vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_SetTestPlanResult ret = new Reserve_SetTestPlanResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.SetTestPlan(vrParameter, out  ret.vrRes);
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
	
	
	public struct Reserve_DelTestPlanResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除实验计划")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_DelTestPlanResult 
	Reserve_DelTestPlan(UNITESTPLAN vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_DelTestPlanResult ret = new Reserve_DelTestPlanResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.DelTestPlan(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_GetTestItemResult
	{
		public uint code;
		public string Message;
		public  UNITESTITEM[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取实验项目")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_GetTestItemResult 
	Reserve_GetTestItem(TESTITEMREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_GetTestItemResult ret = new Reserve_GetTestItemResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.GetTestItem(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_SetTestItemResult
	{
		public uint code;
		public string Message;
		public  UNITESTITEM vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "设置实验项目")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_SetTestItemResult 
	Reserve_SetTestItem(UNITESTITEM vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_SetTestItemResult ret = new Reserve_SetTestItemResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.SetTestItem(vrParameter, out  ret.vrRes);
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
	
	
	public struct Reserve_DelTestItemResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除实验项目")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_DelTestItemResult 
	Reserve_DelTestItem(UNITESTITEM vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_DelTestItemResult ret = new Reserve_DelTestItemResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.DelTestItem(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_GetTestItemMemResvResult
	{
		public uint code;
		public string Message;
		public  TESTITEMMEMRESV[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取实验项目试验者预约详细信息")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_GetTestItemMemResvResult 
	Reserve_GetTestItemMemResv(TESTITEMMEMRESVREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_GetTestItemMemResvResult ret = new Reserve_GetTestItemMemResvResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.GetTestItemMemResv(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_GetTestItemInfoResult
	{
		public uint code;
		public string Message;
		public  TESTITEMINFO[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取实验项目详细信息")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_GetTestItemInfoResult 
	Reserve_GetTestItemInfo(TESTITEMINFOREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_GetTestItemInfoResult ret = new Reserve_GetTestItemInfoResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.GetTestItemInfo(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_UploadReportFormResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "老师提交实验报告模板")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_UploadReportFormResult 
	Reserve_UploadReportForm(REPORTFORMUPLOAD vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_UploadReportFormResult ret = new Reserve_UploadReportFormResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.UploadReportForm(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_UploadReportResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "学生交实验报告")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_UploadReportResult 
	Reserve_UploadReport(REPORTUPLOAD vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_UploadReportResult ret = new Reserve_UploadReportResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.UploadReport(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_CorrectReportResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "批改实验报告")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_CorrectReportResult 
	Reserve_CorrectReport(REPORTCORRECT vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_CorrectReportResult ret = new Reserve_CorrectReportResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.CorrectReport(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_GetActivityPlanResult
	{
		public uint code;
		public string Message;
		public  UNIACTIVITYPLAN[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取活动安排")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_GetActivityPlanResult 
	Reserve_GetActivityPlan(ACTIVITYPLANREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_GetActivityPlanResult ret = new Reserve_GetActivityPlanResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.GetActivityPlan(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_SetActivityPlanResult
	{
		public uint code;
		public string Message;
		public  UNIACTIVITYPLAN vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "设置活动安排")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_SetActivityPlanResult 
	Reserve_SetActivityPlan(UNIACTIVITYPLAN vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_SetActivityPlanResult ret = new Reserve_SetActivityPlanResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.SetActivityPlan(vrParameter, out  ret.vrRes);
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
	
	
	public struct Reserve_DelActivityPlanResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除活动安排")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_DelActivityPlanResult 
	Reserve_DelActivityPlan(UNIACTIVITYPLAN vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_DelActivityPlanResult ret = new Reserve_DelActivityPlanResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.DelActivityPlan(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_GetAPSeatResult
	{
		public uint code;
		public string Message;
		public  APSEAT[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取活动安排的座位列表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_GetAPSeatResult 
	Reserve_GetAPSeat(APSEATREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_GetAPSeatResult ret = new Reserve_GetAPSeatResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.GetAPSeat(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_EnrollActivityResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "申请参加活动")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_EnrollActivityResult 
	Reserve_EnrollActivity(ACTIVITYENROLL vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_EnrollActivityResult ret = new Reserve_EnrollActivityResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.EnrollActivity(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_ExitActivityResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "退出活动申请")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_ExitActivityResult 
	Reserve_ExitActivity(ACTIVITYEXIT vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_ExitActivityResult ret = new Reserve_ExitActivityResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.ExitActivity(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_ActivityMemberOffLineSignResult
	{
		public uint code;
		public string Message;
		public  AOFFLINESIGN vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "管理员导入签到人员名单")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_ActivityMemberOffLineSignResult 
	Reserve_ActivityMemberOffLineSign(AOFFLINESIGN vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_ActivityMemberOffLineSignResult ret = new Reserve_ActivityMemberOffLineSignResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.ActivityMemberOffLineSign(vrParameter, out  ret.vrRes);
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
	
	
	public struct Reserve_ResvRuleGetResult
	{
		public uint code;
		public string Message;
		public  UNIRESVRULE[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取预约规则列表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_ResvRuleGetResult 
	Reserve_ResvRuleGet(RESVRULEREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_ResvRuleGetResult ret = new Reserve_ResvRuleGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.ResvRuleGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_ResvRuleAdminGetResult
	{
		public uint code;
		public string Message;
		public  UNIRESVRULE[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "管理员获取预约规则列表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_ResvRuleAdminGetResult 
	Reserve_ResvRuleAdminGet(RESVRULEADMINREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_ResvRuleAdminGetResult ret = new Reserve_ResvRuleAdminGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.ResvRuleAdminGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_ResvRuleSetResult
	{
		public uint code;
		public string Message;
		public  UNIRESVRULE vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "设置预约规则")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_ResvRuleSetResult 
	Reserve_ResvRuleSet(UNIRESVRULE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_ResvRuleSetResult ret = new Reserve_ResvRuleSetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.ResvRuleSet(vrParameter, out  ret.vrRes);
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
	
	
	public struct Reserve_ResvRuleDelResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除预约规则")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_ResvRuleDelResult 
	Reserve_ResvRuleDel(UNIRESVRULE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_ResvRuleDelResult ret = new Reserve_ResvRuleDelResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.ResvRuleDel(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_GetResearchTestResult
	{
		public uint code;
		public string Message;
		public  RESEARCHTEST[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取科研实验")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_GetResearchTestResult 
	Reserve_GetResearchTest(RESEARCHTESTREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_GetResearchTestResult ret = new Reserve_GetResearchTestResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.GetResearchTest(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_SetResearchTestResult
	{
		public uint code;
		public string Message;
		public  RESEARCHTEST vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "新建/修改科研实验")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_SetResearchTestResult 
	Reserve_SetResearchTest(RESEARCHTEST vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_SetResearchTestResult ret = new Reserve_SetResearchTestResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.SetResearchTest(vrParameter, out  ret.vrRes);
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
	
	
	public struct Reserve_DelResearchTestResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除科研实验")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_DelResearchTestResult 
	Reserve_DelResearchTest(RESEARCHTEST vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_DelResearchTestResult ret = new Reserve_DelResearchTestResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.DelResearchTest(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_SetRTMemberResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "设置科研实验成员")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_SetRTMemberResult 
	Reserve_SetRTMember(RTMEMBER vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_SetRTMemberResult ret = new Reserve_SetRTMemberResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.SetRTMember(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_DelRTMemberResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除科研实验成员")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_DelRTMemberResult 
	Reserve_DelRTMember(RTMEMBER vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_DelRTMemberResult ret = new Reserve_DelRTMemberResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.DelRTMember(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_GetSampleInfoResult
	{
		public uint code;
		public string Message;
		public  SAMPLEINFO[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取实验样品信息")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_GetSampleInfoResult 
	Reserve_GetSampleInfo(SAMPLEINFOREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_GetSampleInfoResult ret = new Reserve_GetSampleInfoResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.GetSampleInfo(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_SetSampleInfoResult
	{
		public uint code;
		public string Message;
		public  SAMPLEINFO vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "新建/修改实验样品信息")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_SetSampleInfoResult 
	Reserve_SetSampleInfo(SAMPLEINFO vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_SetSampleInfoResult ret = new Reserve_SetSampleInfoResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.SetSampleInfo(vrParameter, out  ret.vrRes);
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
	
	
	public struct Reserve_DelSampleInfoResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除实验样品信息")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_DelSampleInfoResult 
	Reserve_DelSampleInfo(SAMPLEINFO vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_DelSampleInfoResult ret = new Reserve_DelSampleInfoResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.DelSampleInfo(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_GetYardResvResult
	{
		public uint code;
		public string Message;
		public  YARDRESV[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取场馆预约列表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_GetYardResvResult 
	Reserve_GetYardResv(YARDRESVREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_GetYardResvResult ret = new Reserve_GetYardResvResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.GetYardResv(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_SetYardResvResult
	{
		public uint code;
		public string Message;
		public  YARDRESV vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "新建修改场馆预约")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_SetYardResvResult 
	Reserve_SetYardResv(YARDRESV vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_SetYardResvResult ret = new Reserve_SetYardResvResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.SetYardResv(vrParameter, out  ret.vrRes);
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
	
	
	public struct Reserve_DelYardResvResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除场馆预约")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_DelYardResvResult 
	Reserve_DelYardResv(YARDRESV vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_DelYardResvResult ret = new Reserve_DelYardResvResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.DelYardResv(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_GetYardResvCheckInfoResult
	{
		public uint code;
		public string Message;
		public  YARDRESVCHECKINFO[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取场馆预约审核列表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_GetYardResvCheckInfoResult 
	Reserve_GetYardResvCheckInfo(YARDRESVCHECKINFOREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_GetYardResvCheckInfoResult ret = new Reserve_GetYardResvCheckInfoResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.GetYardResvCheckInfo(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_YardResvCheckResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "场馆预约审核")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_YardResvCheckResult 
	Reserve_YardResvCheck(YARDRESVCHECK vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_YardResvCheckResult ret = new Reserve_YardResvCheckResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.YardResvCheck(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_GetResvCheckInfoResult
	{
		public uint code;
		public string Message;
		public  RESVCHECKINFO[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取场馆预约审核列表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_GetResvCheckInfoResult 
	Reserve_GetResvCheckInfo(RESVCHECKINFOREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_GetResvCheckInfoResult ret = new Reserve_GetResvCheckInfoResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.GetResvCheckInfo(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_ResvCheckResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "场馆预约审核")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_ResvCheckResult 
	Reserve_ResvCheck(RESVCHECKINFO vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_ResvCheckResult ret = new Reserve_ResvCheckResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.ResvCheck(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_GetYardActivityResult
	{
		public uint code;
		public string Message;
		public  YARDACTIVITY[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取场馆活动列表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_GetYardActivityResult 
	Reserve_GetYardActivity(YARDACTIVITYREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_GetYardActivityResult ret = new Reserve_GetYardActivityResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.GetYardActivity(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_SetYardActivityResult
	{
		public uint code;
		public string Message;
		public  YARDACTIVITY vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "新建场馆活动")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_SetYardActivityResult 
	Reserve_SetYardActivity(YARDACTIVITY vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_SetYardActivityResult ret = new Reserve_SetYardActivityResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.SetYardActivity(vrParameter, out  ret.vrRes);
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
	
	
	public struct Reserve_DelYardActivityResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除场馆活动")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_DelYardActivityResult 
	Reserve_DelYardActivity(YARDACTIVITY vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_DelYardActivityResult ret = new Reserve_DelYardActivityResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.DelYardActivity(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_ThirdResvShareDevResult
	{
		public uint code;
		public string Message;
		public  THIRDRESVSHAREDEV vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "第三方预约共享设备（解决资源冲突，本地不执行第三方预约）")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_ThirdResvShareDevResult 
	Reserve_ThirdResvShareDev(THIRDRESVSHAREDEV vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_ThirdResvShareDevResult ret = new Reserve_ThirdResvShareDevResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.ThirdResvShareDev(vrParameter, out  ret.vrRes);
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
	
	
	public struct Reserve_ThirdResvDelResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "第三方删除预约共享设备")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_ThirdResvDelResult 
	Reserve_ThirdResvDel(THIRDRESVDEL vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_ThirdResvDelResult ret = new Reserve_ThirdResvDelResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.ThirdResvDel(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Reserve_GetThirdResvResult
	{
		public uint code;
		public string Message;
		public  THIRDRESV[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取第三方预约列表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Reserve_GetThirdResvResult 
	Reserve_GetThirdResv(THIRDRESVREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Reserve_GetThirdResvResult ret = new Reserve_GetThirdResvResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Reserve.GetThirdResv(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	//#endregion Reserve部分
	

	//#region Control部分
	/*上网游戏控制*/
	
	public struct Control_GetCtrlClassResult
	{
		public uint code;
		public string Message;
		public  UNICTRLCLASS[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取监控分类库")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Control_GetCtrlClassResult 
	Control_GetCtrlClass(CTRLCLASSREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Control_GetCtrlClassResult ret = new Control_GetCtrlClassResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Control.GetCtrlClass(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Control_SetCtrlClassResult
	{
		public uint code;
		public string Message;
		public  UNICTRLCLASS vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "修改监控分类库")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Control_SetCtrlClassResult 
	Control_SetCtrlClass(UNICTRLCLASS vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Control_SetCtrlClassResult ret = new Control_SetCtrlClassResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Control.SetCtrlClass(vrParameter, out  ret.vrRes);
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
	
	
	public struct Control_DelCtrlClassResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除监控分类库")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Control_DelCtrlClassResult 
	Control_DelCtrlClass(UNICTRLCLASS vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Control_DelCtrlClassResult ret = new Control_DelCtrlClassResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Control.DelCtrlClass(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Control_GetCtrlURLResult
	{
		public uint code;
		public string Message;
		public  UNICTRLURL[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取网址组")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Control_GetCtrlURLResult 
	Control_GetCtrlURL(CTRLURLREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Control_GetCtrlURLResult ret = new Control_GetCtrlURLResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Control.GetCtrlURL(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Control_SetCtrlURLResult
	{
		public uint code;
		public string Message;
		public  UNICTRLURL vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "修改网址组")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Control_SetCtrlURLResult 
	Control_SetCtrlURL(UNICTRLURL vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Control_SetCtrlURLResult ret = new Control_SetCtrlURLResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Control.SetCtrlURL(vrParameter, out  ret.vrRes);
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
	
	
	public struct Control_DelCtrlURLResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除网址组")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Control_DelCtrlURLResult 
	Control_DelCtrlURL(UNICTRLURL vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Control_DelCtrlURLResult ret = new Control_DelCtrlURLResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Control.DelCtrlURL(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Control_GetCtrlSWResult
	{
		public uint code;
		public string Message;
		public  UNICTRLSW[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取软件组")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Control_GetCtrlSWResult 
	Control_GetCtrlSW(CTRLSWREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Control_GetCtrlSWResult ret = new Control_GetCtrlSWResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Control.GetCtrlSW(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Control_SetCtrlSWResult
	{
		public uint code;
		public string Message;
		public  UNICTRLSW vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "修改软件组")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Control_SetCtrlSWResult 
	Control_SetCtrlSW(UNICTRLSW vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Control_SetCtrlSWResult ret = new Control_SetCtrlSWResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Control.SetCtrlSW(vrParameter, out  ret.vrRes);
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
	
	
	public struct Control_DelCtrlSWResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除软件组")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Control_DelCtrlSWResult 
	Control_DelCtrlSW(UNICTRLSW vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Control_DelCtrlSWResult ret = new Control_DelCtrlSWResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Control.DelCtrlSW(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Control_GetSoftwareResult
	{
		public uint code;
		public string Message;
		public  UNISOFTWARE[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取软件")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Control_GetSoftwareResult 
	Control_GetSoftware(SOFTWAREREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Control_GetSoftwareResult ret = new Control_GetSoftwareResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Control.GetSoftware(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Control_SetSoftwareResult
	{
		public uint code;
		public string Message;
		public  UNISOFTWARE vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "修改软件")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Control_SetSoftwareResult 
	Control_SetSoftware(UNISOFTWARE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Control_SetSoftwareResult ret = new Control_SetSoftwareResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Control.SetSoftware(vrParameter, out  ret.vrRes);
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
	
	
	public struct Control_GetProgramResult
	{
		public uint code;
		public string Message;
		public  UNIPROGRAM[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取程序")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Control_GetProgramResult 
	Control_GetProgram(PROGRAMREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Control_GetProgramResult ret = new Control_GetProgramResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Control.GetProgram(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Control_SetProgramResult
	{
		public uint code;
		public string Message;
		public  UNIPROGRAM vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "修改程序")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Control_SetProgramResult 
	Control_SetProgram(UNIPROGRAM vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Control_SetProgramResult ret = new Control_SetProgramResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Control.SetProgram(vrParameter, out  ret.vrRes);
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
	
	
	public struct Control_GetPCSWinfoResult
	{
		public uint code;
		public string Message;
		public  UNIPCSWINFO[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取机器程序")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Control_GetPCSWinfoResult 
	Control_GetPCSWinfo(PCSWINFOREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Control_GetPCSWinfoResult ret = new Control_GetPCSWinfoResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Control.GetPCSWinfo(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Control_GetRoomSWinfoResult
	{
		public uint code;
		public string Message;
		public  UNIROOMSWINFO[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取机房程序")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Control_GetRoomSWinfoResult 
	Control_GetRoomSWinfo(ROOMSWINFOREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Control_GetRoomSWinfoResult ret = new Control_GetRoomSWinfoResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Control.GetRoomSWinfo(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	//#endregion Control部分
	
	//THIRDIF部分
	

	//#region Fee部分
	/*收费管理*/
	
	public struct Fee_GetResult
	{
		public uint code;
		public string Message;
		public  UNIFEE[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取收费标准列表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Fee_GetResult 
	Fee_Get(FEEREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Fee_GetResult ret = new Fee_GetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Fee.Get(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Fee_RTDevFeeGetResult
	{
		public uint code;
		public string Message;
		public  UNIFEE vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取科研项目对应的设备的收费标准")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Fee_RTDevFeeGetResult 
	Fee_RTDevFeeGet(RTDEVFEEREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Fee_RTDevFeeGetResult ret = new Fee_RTDevFeeGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Fee.RTDevFeeGet(vrParameter, out  ret.vrRes);
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
	
	
	public struct Fee_RTDevSampleGetResult
	{
		public uint code;
		public string Message;
		public  RTDEVSAMPLE[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取科研项目对应的设备的样品及费率")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Fee_RTDevSampleGetResult 
	Fee_RTDevSampleGet(RTDEVSAMPLEREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Fee_RTDevSampleGetResult ret = new Fee_RTDevSampleGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Fee.RTDevSampleGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Fee_SetResult
	{
		public uint code;
		public string Message;
		public  UNIFEE vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "设置收费标准信息")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Fee_SetResult 
	Fee_Set(UNIFEE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Fee_SetResult ret = new Fee_SetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Fee.Set(vrParameter, out  ret.vrRes);
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
	
	
	public struct Fee_DelResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除收费标准")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Fee_DelResult 
	Fee_Del(UNIFEE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Fee_DelResult ret = new Fee_DelResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Fee.Del(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Fee_FTRuleGetResult
	{
		public uint code;
		public string Message;
		public  FREETIMERULE[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取机时使用规则")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Fee_FTRuleGetResult 
	Fee_FTRuleGet(FTRULEREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Fee_FTRuleGetResult ret = new Fee_FTRuleGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Fee.FTRuleGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Fee_FTRuleSetResult
	{
		public uint code;
		public string Message;
		public  FREETIMERULE vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "修改机时使用规则")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Fee_FTRuleSetResult 
	Fee_FTRuleSet(FREETIMERULE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Fee_FTRuleSetResult ret = new Fee_FTRuleSetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Fee.FTRuleSet(vrParameter, out  ret.vrRes);
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
	
	
	public struct Fee_FTRuleDelResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除机时使用规则")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Fee_FTRuleDelResult 
	Fee_FTRuleDel(FREETIMERULE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Fee_FTRuleDelResult ret = new Fee_FTRuleDelResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Fee.FTRuleDel(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Fee_BillGetResult
	{
		public uint code;
		public string Message;
		public  UNIBILL[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取账单")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Fee_BillGetResult 
	Fee_BillGet(BILLREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Fee_BillGetResult ret = new Fee_BillGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Fee.BillGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Fee_BillSetResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "设置账单")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Fee_BillSetResult 
	Fee_BillSet(UNIBILL vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Fee_BillSetResult ret = new Fee_BillSetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Fee.BillSet(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Fee_BillPayResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "账单缴费")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Fee_BillPayResult 
	Fee_BillPay(BILLPAY vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Fee_BillPayResult ret = new Fee_BillPayResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Fee.BillPay(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	//#endregion Fee部分
	

	//#region Console部分
	/*控制台管理*/
	
	public struct Console_ConGetResult
	{
		public uint code;
		public string Message;
		public  UNICONSOLE[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取控制台列表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Console_ConGetResult 
	Console_ConGet(CONREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Console_ConGetResult ret = new Console_ConGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Console.ConGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Console_ConSetResult
	{
		public uint code;
		public string Message;
		public  UNICONSOLE vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "设置控制台信息")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Console_ConSetResult 
	Console_ConSet(UNICONSOLE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Console_ConSetResult ret = new Console_ConSetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Console.ConSet(vrParameter, out  ret.vrRes);
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
	
	
	public struct Console_ConDelResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除控制台")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Console_ConDelResult 
	Console_ConDel(UNICONSOLE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Console_ConDelResult ret = new Console_ConDelResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Console.ConDel(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Console_ConLoginResult
	{
		public uint code;
		public string Message;
		public  CONLOGINRES vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "控制台登录")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Console_ConLoginResult 
	Console_ConLogin(CONLOGINREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Console_ConLoginResult ret = new Console_ConLoginResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Console.ConLogin(vrParameter, out  ret.vrRes);
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
	
	
	public struct Console_ConLogoutResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "控制台退出")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Console_ConLogoutResult 
	Console_ConLogout(CONLOGOUTREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Console_ConLogoutResult ret = new Console_ConLogoutResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Console.ConLogout(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Console_ConPulseResult
	{
		public uint code;
		public string Message;
		public  CONPULSERES vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "定时通信")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Console_ConPulseResult 
	Console_ConPulse(CONPULSEREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Console_ConPulseResult ret = new Console_ConPulseResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Console.ConPulse(vrParameter, out  ret.vrRes);
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
	
	
	public struct Console_ConShowMsgResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "给控制台发消息")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Console_ConShowMsgResult 
	Console_ConShowMsg(CONMESSAGE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Console_ConShowMsgResult ret = new Console_ConShowMsgResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Console.ConShowMsg(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Console_ConUserCardResult
	{
		public uint code;
		public string Message;
		public  CONUSERINFO vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "控制台刷卡")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Console_ConUserCardResult 
	Console_ConUserCard(ACCCHECKREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Console_ConUserCardResult ret = new Console_ConUserCardResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Console.ConUserCard(vrParameter, out  ret.vrRes);
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
	
	
	public struct Console_ConTeacherInResult
	{
		public uint code;
		public string Message;
		public  CONTEACHERINFO vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "控制台教师登录")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Console_ConTeacherInResult 
	Console_ConTeacherIn(ACCCHECKREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Console_ConTeacherInResult ret = new Console_ConTeacherInResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Console.ConTeacherIn(vrParameter, out  ret.vrRes);
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
	
	
	public struct Console_ConCardForPCResult
	{
		public uint code;
		public string Message;
		public  CARDFORPCRES vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "控制台刷卡上机")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Console_ConCardForPCResult 
	Console_ConCardForPC(CARDFORPCREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Console_ConCardForPCResult ret = new Console_ConCardForPCResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Console.ConCardForPC(vrParameter, out  ret.vrRes);
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
	
	
	public struct Console_AutoGateCardResult
	{
		public uint code;
		public string Message;
		public  AUTOGATECARDRES vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "通道机刷卡")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Console_AutoGateCardResult 
	Console_AutoGateCard(AUTOGATECARDREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Console_AutoGateCardResult ret = new Console_AutoGateCardResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Console.AutoGateCard(vrParameter, out  ret.vrRes);
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
	
	
	public struct Console_MobileScanResult
	{
		public uint code;
		public string Message;
		public  MOBILESCANRES vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "手机扫描二维码")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Console_MobileScanResult 
	Console_MobileScan(MOBILESCANREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Console_MobileScanResult ret = new Console_MobileScanResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Console.MobileScan(vrParameter, out  ret.vrRes);
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
	
	
	public struct Console_ConUserInResult
	{
		public uint code;
		public string Message;
		public  CONUSERINRES vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "控制台刷卡开始使用")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Console_ConUserInResult 
	Console_ConUserIn(CONUSERINREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Console_ConUserInResult ret = new Console_ConUserInResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Console.ConUserIn(vrParameter, out  ret.vrRes);
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
	
	
	public struct Console_MobileUserInResult
	{
		public uint code;
		public string Message;
		public  MOBILEUSERINRES vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "手机扫描开始使用")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Console_MobileUserInResult 
	Console_MobileUserIn(MOBILEUSERINREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Console_MobileUserInResult ret = new Console_MobileUserInResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Console.MobileUserIn(vrParameter, out  ret.vrRes);
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
	
	
	public struct Console_MobileDelayResult
	{
		public uint code;
		public string Message;
		public  MOBILEDELAYRES vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "手机延时（续约)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Console_MobileDelayResult 
	Console_MobileDelay(MOBILEDELAYREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Console_MobileDelayResult ret = new Console_MobileDelayResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Console.MobileDelay(vrParameter, out  ret.vrRes);
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
	
	
	public struct Console_ConUserOutResult
	{
		public uint code;
		public string Message;
		public  CONUSEROUTRES vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "控制台刷卡结束使用")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Console_ConUserOutResult 
	Console_ConUserOut(CONUSEROUTREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Console_ConUserOutResult ret = new Console_ConUserOutResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Console.ConUserOut(vrParameter, out  ret.vrRes);
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
	
	
	public struct Console_MobileUserOutResult
	{
		public uint code;
		public string Message;
		public  MOBILEUSEROUTRES vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "手机扫描刷卡结束使用")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Console_MobileUserOutResult 
	Console_MobileUserOut(MOBILEUSEROUTREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Console_MobileUserOutResult ret = new Console_MobileUserOutResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Console.MobileUserOut(vrParameter, out  ret.vrRes);
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
	
	
	public struct Console_ResvUserComeInResult
	{
		public uint code;
		public string Message;
		public  RESVUSERCOMEINRES vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "手机登录签到")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Console_ResvUserComeInResult 
	Console_ResvUserComeIn(RESVUSERCOMEINREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Console_ResvUserComeInResult ret = new Console_ResvUserComeInResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Console.ResvUserComeIn(vrParameter, out  ret.vrRes);
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
	
	
	public struct Console_ResvUserDelayResult
	{
		public uint code;
		public string Message;
		public  RESVUSERDELAYRES vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "手机登录延时（续约)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Console_ResvUserDelayResult 
	Console_ResvUserDelay(RESVUSERDELAYREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Console_ResvUserDelayResult ret = new Console_ResvUserDelayResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Console.ResvUserDelay(vrParameter, out  ret.vrRes);
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
	
	
	public struct Console_ResvUserGoOutResult
	{
		public uint code;
		public string Message;
		public  RESVUSERGOOUTRES vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "手机登录离开")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Console_ResvUserGoOutResult 
	Console_ResvUserGoOut(RESVUSERGOOUTREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Console_ResvUserGoOutResult ret = new Console_ResvUserGoOutResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Console.ResvUserGoOut(vrParameter, out  ret.vrRes);
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
	
	//#endregion Console部分
	

	//#region Report部分
	/*报表查询统计*/
	
	public struct Report_ResvRecGetResult
	{
		public uint code;
		public string Message;
		public  UNIRESVREC[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取预约记录")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_ResvRecGetResult 
	Report_ResvRecGet(RESVRECREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_ResvRecGetResult ret = new Report_ResvRecGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.ResvRecGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Report_GetResvKindStatResult
	{
		public uint code;
		public string Message;
		public  RESVKINDSTAT[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "预约类型统计")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_GetResvKindStatResult 
	Report_GetResvKindStat(RESVKINDSTATREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_GetResvKindStatResult ret = new Report_GetResvKindStatResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.GetResvKindStat(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Report_GetResvModeStatResult
	{
		public uint code;
		public string Message;
		public  RESVMODESTAT[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "预约方式统计")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_GetResvModeStatResult 
	Report_GetResvModeStat(RESVMODESTATREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_GetResvModeStatResult ret = new Report_GetResvModeStatResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.GetResvModeStat(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Report_GetUseRecResult
	{
		public uint code;
		public string Message;
		public  DEVUSEREC[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取使用记录明细")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_GetUseRecResult 
	Report_GetUseRec(USERECREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_GetUseRecResult ret = new Report_GetUseRecResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.GetUseRec(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Report_GetDoorCardRecResult
	{
		public uint code;
		public string Message;
		public  DOORCARDREC[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取门禁刷卡记录明细")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_GetDoorCardRecResult 
	Report_GetDoorCardRec(DOORCARDRECREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_GetDoorCardRecResult ret = new Report_GetDoorCardRecResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.GetDoorCardRec(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Report_GetUserStatResult
	{
		public uint code;
		public string Message;
		public  USERSTAT[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "使用者统计报表数据")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_GetUserStatResult 
	Report_GetUserStat(REPORTREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_GetUserStatResult ret = new Report_GetUserStatResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.GetUserStat(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Report_GetLabStatResult
	{
		public uint code;
		public string Message;
		public  LABSTAT[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "实验室统计报表数据")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_GetLabStatResult 
	Report_GetLabStat(REPORTREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_GetLabStatResult ret = new Report_GetLabStatResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.GetLabStat(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Report_GetDevKindStatResult
	{
		public uint code;
		public string Message;
		public  DEVKINDSTAT[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "设备类型统计报表数据")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_GetDevKindStatResult 
	Report_GetDevKindStat(REPORTREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_GetDevKindStatResult ret = new Report_GetDevKindStatResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.GetDevKindStat(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Report_GetDevStatResult
	{
		public uint code;
		public string Message;
		public  DEVSTAT[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "设备统计报表数据(CUniStruct[REQEXTINFO],szExtInfo返回DEVSTAT总合计)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_GetDevStatResult 
	Report_GetDevStat(REPORTREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_GetDevStatResult ret = new Report_GetDevStatResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.GetDevStat(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Report_GetTestItemStatResult
	{
		public uint code;
		public string Message;
		public  TESTITEMSTAT[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "实验项目表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_GetTestItemStatResult 
	Report_GetTestItemStat(TESTITEMSTATREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_GetTestItemStatResult ret = new Report_GetTestItemStatResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.GetTestItemStat(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Report_GetDevClassStatResult
	{
		public uint code;
		public string Message;
		public  DEVCLASSSTAT[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "设备类型统计报表数据")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_GetDevClassStatResult 
	Report_GetDevClassStat(REPORTREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_GetDevClassStatResult ret = new Report_GetDevClassStatResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.GetDevClassStat(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Report_GetDeptStatResult
	{
		public uint code;
		public string Message;
		public  DEPTSTAT[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "学院使用统计")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_GetDeptStatResult 
	Report_GetDeptStat(REPORTREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_GetDeptStatResult ret = new Report_GetDeptStatResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.GetDeptStat(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Report_GeIdentStatResult
	{
		public uint code;
		public string Message;
		public  IDENTSTAT[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "身份使用统计")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_GeIdentStatResult 
	Report_GeIdentStat(IDENTSTATREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_GeIdentStatResult ret = new Report_GeIdentStatResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.GeIdentStat(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Report_GetTeachingResvRecResult
	{
		public uint code;
		public string Message;
		public  TEACHINGRESVREC[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取教学预约记录")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_GetTeachingResvRecResult 
	Report_GetTeachingResvRec(TEACHINGRESVRECREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_GetTeachingResvRecResult ret = new Report_GetTeachingResvRecResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.GetTeachingResvRec(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Report_GetRoomStatResult
	{
		public uint code;
		public string Message;
		public  ROOMSTAT[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "实验室(房间)统计报表数据")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_GetRoomStatResult 
	Report_GetRoomStat(REPORTREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_GetRoomStatResult ret = new Report_GetRoomStatResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.GetRoomStat(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Report_GetDevUsingRateResult
	{
		public uint code;
		public string Message;
		public  DEVUSINGRATE vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取设备使用率查询")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_GetDevUsingRateResult 
	Report_GetDevUsingRate(DEVUSINGRATEREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_GetDevUsingRateResult ret = new Report_GetDevUsingRateResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.GetDevUsingRate(vrParameter, out  ret.vrRes);
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
	
	
	public struct Report_GetDevMonthStatResult
	{
		public uint code;
		public string Message;
		public  DEVMONTHSTAT[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取设备月使用统计")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_GetDevMonthStatResult 
	Report_GetDevMonthStat(DEVMONTHSTATREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_GetDevMonthStatResult ret = new Report_GetDevMonthStatResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.GetDevMonthStat(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Report_GetRTUseStatResult
	{
		public uint code;
		public string Message;
		public  RTUSESTAT[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取设备科研实验统计")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_GetRTUseStatResult 
	Report_GetRTUseStat(RTUSESTATREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_GetRTUseStatResult ret = new Report_GetRTUseStatResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.GetRTUseStat(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Report_GetRTUseDetailResult
	{
		public uint code;
		public string Message;
		public  RTUSEDETAIL[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取设备科研实验明细")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_GetRTUseDetailResult 
	Report_GetRTUseDetail(RTUSEDETAILREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_GetRTUseDetailResult ret = new Report_GetRTUseDetailResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.GetRTUseDetail(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Report_GetRTFAStatResult
	{
		public uint code;
		public string Message;
		public  RTFASTAT[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取设备科研实验费用分配统计")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_GetRTFAStatResult 
	Report_GetRTFAStat(RTFASTATREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_GetRTFAStatResult ret = new Report_GetRTFAStatResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.GetRTFAStat(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Report_GetRTFADetailResult
	{
		public uint code;
		public string Message;
		public  RTFADETAIL[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取设备科研实验费用分配明细")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_GetRTFADetailResult 
	Report_GetRTFADetail(RTFADETAILREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_GetRTFADetailResult ret = new Report_GetRTFADetailResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.GetRTFADetail(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Report_GetDefaultStatResult
	{
		public uint code;
		public string Message;
		public  DEFAULTSTAT[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "违约统计")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_GetDefaultStatResult 
	Report_GetDefaultStat(DEFAULTSTATREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_GetDefaultStatResult ret = new Report_GetDefaultStatResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.GetDefaultStat(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Report_GetDevWeekUsingRateResult
	{
		public uint code;
		public string Message;
		public  DEVWEEKUSINGRATE vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取设备周使用率查询")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_GetDevWeekUsingRateResult 
	Report_GetDevWeekUsingRate(DEVWEEKUSINGRATEREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_GetDevWeekUsingRateResult ret = new Report_GetDevWeekUsingRateResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.GetDevWeekUsingRate(vrParameter, out  ret.vrRes);
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
	
	
	public struct Report_GetYardActivityStatResult
	{
		public uint code;
		public string Message;
		public  YARDACTIVITYSTAT[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "场馆活动类型统计")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_GetYardActivityStatResult 
	Report_GetYardActivityStat(YARDACTIVITYSTATREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_GetYardActivityStatResult ret = new Report_GetYardActivityStatResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.GetYardActivityStat(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Report_GetDevListResult
	{
		public uint code;
		public string Message;
		public  DEVLIST[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "教学科研仪器设备表 (SJ1)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_GetDevListResult 
	Report_GetDevList(DEVLISTREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_GetDevListResult ret = new Report_GetDevListResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.GetDevList(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Report_GetDevChgResult
	{
		public uint code;
		public string Message;
		public  DEVCHG vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "教学科研仪器设备增减变动情况表(SJ2)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_GetDevChgResult 
	Report_GetDevChg(DEVCHGREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_GetDevChgResult ret = new Report_GetDevChgResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.GetDevChg(vrParameter, out  ret.vrRes);
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
	
	
	public struct Report_GetBigDevResult
	{
		public uint code;
		public string Message;
		public  BIGDEV[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "贵重仪器设备表(SJ3)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_GetBigDevResult 
	Report_GetBigDev(BIGDEVREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_GetBigDevResult ret = new Report_GetBigDevResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.GetBigDev(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Report_GetTestItemReportResult
	{
		public uint code;
		public string Message;
		public  TESTITEMREPORT[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "实验项目表(SJ4)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_GetTestItemReportResult 
	Report_GetTestItemReport(TESTITEMREPORTREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_GetTestItemReportResult ret = new Report_GetTestItemReportResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.GetTestItemReport(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Report_GetStaffInfoResult
	{
		public uint code;
		public string Message;
		public  STAFFINFO[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "专任实验室人员表(SJ5)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_GetStaffInfoResult 
	Report_GetStaffInfo(STAFFINFOREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_GetStaffInfoResult ret = new Report_GetStaffInfoResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.GetStaffInfo(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Report_GetLabInfoResult
	{
		public uint code;
		public string Message;
		public  LABINFO[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "实验室基本情况表(SJ6)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_GetLabInfoResult 
	Report_GetLabInfo(LABINFOREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_GetLabInfoResult ret = new Report_GetLabInfoResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.GetLabInfo(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Report_GetLabAllCostResult
	{
		public uint code;
		public string Message;
		public  LABALLCOST vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "实验室经费情况表(SJ7)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_GetLabAllCostResult 
	Report_GetLabAllCost(LABALLCOSTREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_GetLabAllCostResult ret = new Report_GetLabAllCostResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.GetLabAllCost(vrParameter, out  ret.vrRes);
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
	
	
	public struct Report_GetLabSummaryResult
	{
		public uint code;
		public string Message;
		public  LABSUMMARY vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "高等学校实验室综合信息表(SZ1)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_GetLabSummaryResult 
	Report_GetLabSummary(LABSUMMARYREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_GetLabSummaryResult ret = new Report_GetLabSummaryResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.GetLabSummary(vrParameter, out  ret.vrRes);
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
	
	
	public struct Report_GetLabSummary2Result
	{
		public uint code;
		public string Message;
		public  LABSUMMARY2 vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "高等学校实验室综合信息表2(SZ2)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_GetLabSummary2Result 
	Report_GetLabSummary2(LABSUMMARY2REQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_GetLabSummary2Result ret = new Report_GetLabSummary2Result();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.GetLabSummary2(vrParameter, out  ret.vrRes);
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
	
	
	public struct Report_SetDevListResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "教学科研仪器设备表 (SJ1)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_SetDevListResult 
	Report_SetDevList(DEVLIST vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_SetDevListResult ret = new Report_SetDevListResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.SetDevList(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Report_SetDevChgResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "教学科研仪器设备增减变动情况表(SJ2)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_SetDevChgResult 
	Report_SetDevChg(DEVCHG vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_SetDevChgResult ret = new Report_SetDevChgResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.SetDevChg(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Report_SetBigDevResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "贵重仪器设备表(SJ3)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_SetBigDevResult 
	Report_SetBigDev(BIGDEV vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_SetBigDevResult ret = new Report_SetBigDevResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.SetBigDev(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Report_SetTestItemReportResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "实验项目表(SJ4)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_SetTestItemReportResult 
	Report_SetTestItemReport(TESTITEMREPORT vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_SetTestItemReportResult ret = new Report_SetTestItemReportResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.SetTestItemReport(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Report_SetStaffInfoResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "专任实验室人员表(SJ5)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_SetStaffInfoResult 
	Report_SetStaffInfo(STAFFINFO vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_SetStaffInfoResult ret = new Report_SetStaffInfoResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.SetStaffInfo(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Report_SetLabInfoResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "实验室基本情况表(SJ6)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_SetLabInfoResult 
	Report_SetLabInfo(LABINFO vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_SetLabInfoResult ret = new Report_SetLabInfoResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.SetLabInfo(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Report_SetLabAllCostResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "实验室经费情况表(SJ7)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_SetLabAllCostResult 
	Report_SetLabAllCost(LABALLCOST vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_SetLabAllCostResult ret = new Report_SetLabAllCostResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.SetLabAllCost(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Report_SetLabSummaryResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "高等学校实验室综合信息表(SZ1)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_SetLabSummaryResult 
	Report_SetLabSummary(LABSUMMARY vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_SetLabSummaryResult ret = new Report_SetLabSummaryResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.SetLabSummary(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Report_SetLabSummary2Result
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "高等学校实验室综合信息表2(SZ2)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Report_SetLabSummary2Result 
	Report_SetLabSummary2(LABSUMMARY2 vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Report_SetLabSummary2Result ret = new Report_SetLabSummary2Result();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Report.SetLabSummary2(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	//#endregion Report部分
	

	//#region System部分
	/*系统管理*/
	
	public struct System_CfgGetResult
	{
		public uint code;
		public string Message;
		public  CFGINFO[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取系统配置文件")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public System_CfgGetResult 
	System_CfgGet(CFGREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		System_CfgGetResult ret = new System_CfgGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.System.CfgGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct System_CfgSetResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "修改并保存系统配置文件")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public System_CfgSetResult 
	System_CfgSet(CFGINFO vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		System_CfgSetResult ret = new System_CfgSetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.System.CfgSet(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct System_CreditTypeGetResult
	{
		public uint code;
		public string Message;
		public  CREDITTYPE[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取独立的信用类别列表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public System_CreditTypeGetResult 
	System_CreditTypeGet(CREDITTYPEREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		System_CreditTypeGetResult ret = new System_CreditTypeGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.System.CreditTypeGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct System_CreditTypeSetResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "设置独立的信用类别")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public System_CreditTypeSetResult 
	System_CreditTypeSet(CREDITTYPE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		System_CreditTypeSetResult ret = new System_CreditTypeSetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.System.CreditTypeSet(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct System_CreditScoreGetResult
	{
		public uint code;
		public string Message;
		public  CREDITSCORE[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取信用分数规则列表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public System_CreditScoreGetResult 
	System_CreditScoreGet(CREDITSCOREREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		System_CreditScoreGetResult ret = new System_CreditScoreGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.System.CreditScoreGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct System_CreditScoreSetResult
	{
		public uint code;
		public string Message;
		public  CREDITSCORE vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "设置信用分数规则")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public System_CreditScoreSetResult 
	System_CreditScoreSet(CREDITSCORE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		System_CreditScoreSetResult ret = new System_CreditScoreSetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.System.CreditScoreSet(vrParameter, out  ret.vrRes);
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
	
	
	public struct System_MyCreditScoreGetResult
	{
		public uint code;
		public string Message;
		public  MYCREDITSCORE[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取我的信用积分")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public System_MyCreditScoreGetResult 
	System_MyCreditScoreGet(MYCREDITSCOREREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		System_MyCreditScoreGetResult ret = new System_MyCreditScoreGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.System.MyCreditScoreGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct System_AdminCreditDoResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "人工信用管理")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public System_AdminCreditDoResult 
	System_AdminCreditDo(ADMINCREDIT vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		System_AdminCreditDoResult ret = new System_AdminCreditDoResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.System.AdminCreditDo(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct System_CreditRecGetResult
	{
		public uint code;
		public string Message;
		public  CREDITREC[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取信用记录")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public System_CreditRecGetResult 
	System_CreditRecGet(CREDITRECREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		System_CreditRecGetResult ret = new System_CreditRecGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.System.CreditRecGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct System_SysFuncGetResult
	{
		public uint code;
		public string Message;
		public  SYSFUNC[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取系统功能列表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public System_SysFuncGetResult 
	System_SysFuncGet(SYSFUNCREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		System_SysFuncGetResult ret = new System_SysFuncGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.System.SysFuncGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct System_SysFuncRuleGetResult
	{
		public uint code;
		public string Message;
		public  SYSFUNCRULE[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取系统功能使用规则列表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public System_SysFuncRuleGetResult 
	System_SysFuncRuleGet(SYSFUNCRULEREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		System_SysFuncRuleGetResult ret = new System_SysFuncRuleGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.System.SysFuncRuleGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct System_SysFuncRuleSetResult
	{
		public uint code;
		public string Message;
		public  SYSFUNCRULE vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "修改系统功能使用规则")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public System_SysFuncRuleSetResult 
	System_SysFuncRuleSet(SYSFUNCRULE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		System_SysFuncRuleSetResult ret = new System_SysFuncRuleSetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.System.SysFuncRuleSet(vrParameter, out  ret.vrRes);
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
	
	
	public struct System_SFRoleGetResult
	{
		public uint code;
		public string Message;
		public  SFROLEINFO[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取系统功能资格表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public System_SFRoleGetResult 
	System_SFRoleGet(SFROLEINFOREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		System_SFRoleGetResult ret = new System_SFRoleGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.System.SFRoleGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct System_SFRoleApplyResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "系统功能资格申请")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public System_SFRoleApplyResult 
	System_SFRoleApply(SFROLEINFO vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		System_SFRoleApplyResult ret = new System_SFRoleApplyResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.System.SFRoleApply(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct System_SFRoleCheckResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "系统功能资格审核")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public System_SFRoleCheckResult 
	System_SFRoleCheck(SFROLEINFO vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		System_SFRoleCheckResult ret = new System_SFRoleCheckResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.System.SFRoleCheck(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct System_GetCodingTableResult
	{
		public uint code;
		public string Message;
		public  CODINGTABLE[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取编码表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public System_GetCodingTableResult 
	System_GetCodingTable(CODINGTABLEREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		System_GetCodingTableResult ret = new System_GetCodingTableResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.System.GetCodingTable(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct System_SetCodingTableResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "设置编码表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public System_SetCodingTableResult 
	System_SetCodingTable(CODINGTABLE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		System_SetCodingTableResult ret = new System_SetCodingTableResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.System.SetCodingTable(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct System_DelCodingTableResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除编码表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public System_DelCodingTableResult 
	System_DelCodingTable(CODINGTABLE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		System_DelCodingTableResult ret = new System_DelCodingTableResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.System.DelCodingTable(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct System_GetMultiLanLibResult
	{
		public uint code;
		public string Message;
		public  UNIMULTILANLIB[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取多语言包")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public System_GetMultiLanLibResult 
	System_GetMultiLanLib(MULTILANLIBREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		System_GetMultiLanLibResult ret = new System_GetMultiLanLibResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.System.GetMultiLanLib(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct System_SystemRefreshResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "更新系统状态")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public System_SystemRefreshResult 
	System_SystemRefresh(SYSREFRESHREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		System_SystemRefreshResult ret = new System_SystemRefreshResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.System.SystemRefresh(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	//#endregion System部分
	

	//#region Assert部分
	/*资产管理*/
	
	public struct Assert_AssertGetResult
	{
		public uint code;
		public string Message;
		public  UNIASSERT[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取资产列表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Assert_AssertGetResult 
	Assert_AssertGet(ASSERTREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Assert_AssertGetResult ret = new Assert_AssertGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Assert.AssertGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Assert_AssertWarehousingResult
	{
		public uint code;
		public string Message;
		public  UNIASSERT vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "资产入库")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Assert_AssertWarehousingResult 
	Assert_AssertWarehousing(UNIASSERT vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Assert_AssertWarehousingResult ret = new Assert_AssertWarehousingResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Assert.AssertWarehousing(vrParameter, out  ret.vrRes);
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
	
	
	public struct Assert_AssertRFIDBindResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "RFID发卡")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Assert_AssertRFIDBindResult 
	Assert_AssertRFIDBind(RFIDBIND vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Assert_AssertRFIDBindResult ret = new Assert_AssertRFIDBindResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Assert.AssertRFIDBind(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Assert_AssertChgRoomResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "房间变更")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Assert_AssertChgRoomResult 
	Assert_AssertChgRoom(ROOMCHG vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Assert_AssertChgRoomResult ret = new Assert_AssertChgRoomResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Assert.AssertChgRoom(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Assert_AssertChgKeeperResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "责任人变更")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Assert_AssertChgKeeperResult 
	Assert_AssertChgKeeper(KEEPERCHG vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Assert_AssertChgKeeperResult ret = new Assert_AssertChgKeeperResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Assert.AssertChgKeeper(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Assert_AssertDelResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除资产")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Assert_AssertDelResult 
	Assert_AssertDel(UNIASSERT vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Assert_AssertDelResult ret = new Assert_AssertDelResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Assert.AssertDel(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Assert_StockTakingGetResult
	{
		public uint code;
		public string Message;
		public  STOCKTAKING[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取资产盘点表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Assert_StockTakingGetResult 
	Assert_StockTakingGet(STOCKTAKINGREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Assert_StockTakingGetResult ret = new Assert_StockTakingGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Assert.StockTakingGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Assert_StockTakingDoResult
	{
		public uint code;
		public string Message;
		public  STOCKTAKING vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "资产盘点")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Assert_StockTakingDoResult 
	Assert_StockTakingDo(STOCKTAKING vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Assert_StockTakingDoResult ret = new Assert_StockTakingDoResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Assert.StockTakingDo(vrParameter, out  ret.vrRes);
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
	
	
	public struct Assert_STDetailGetResult
	{
		public uint code;
		public string Message;
		public  STDETAIL[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取盘点资产明细表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Assert_STDetailGetResult 
	Assert_STDetailGet(STDETAILREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Assert_STDetailGetResult ret = new Assert_STDetailGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Assert.STDetailGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Assert_STDetailDoResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "具体资产盘点")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Assert_STDetailDoResult 
	Assert_STDetailDo(STDETAIL vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Assert_STDetailDoResult ret = new Assert_STDetailDoResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Assert.STDetailDo(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Assert_OutOfSericeGetResult
	{
		public uint code;
		public string Message;
		public  OUTOFSERVICE[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取设备报废记录表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Assert_OutOfSericeGetResult 
	Assert_OutOfSericeGet(OUTOFSERVICEREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Assert_OutOfSericeGetResult ret = new Assert_OutOfSericeGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Assert.OutOfSericeGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Assert_OutOfSericeApplyResult
	{
		public uint code;
		public string Message;
		public  OUTOFSERVICE vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "申请设备报废")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Assert_OutOfSericeApplyResult 
	Assert_OutOfSericeApply(OUTOFSERVICE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Assert_OutOfSericeApplyResult ret = new Assert_OutOfSericeApplyResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Assert.OutOfSericeApply(vrParameter, out  ret.vrRes);
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
	
	
	public struct Assert_OutOfSericeApproveResult
	{
		public uint code;
		public string Message;
		public  OUTOFSERVICE vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "批准设备报废")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Assert_OutOfSericeApproveResult 
	Assert_OutOfSericeApprove(OUTOFSERVICE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Assert_OutOfSericeApproveResult ret = new Assert_OutOfSericeApproveResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Assert.OutOfSericeApprove(vrParameter, out  ret.vrRes);
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
	
	
	public struct Assert_OutOfSericeDelResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "撤销设备报废申请")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Assert_OutOfSericeDelResult 
	Assert_OutOfSericeDel(OUTOFSERVICE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Assert_OutOfSericeDelResult ret = new Assert_OutOfSericeDelResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Assert.OutOfSericeDel(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Assert_OOSDetailGetResult
	{
		public uint code;
		public string Message;
		public  OOSDETAIL[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取报废设备明细表")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Assert_OOSDetailGetResult 
	Assert_OOSDetailGet(OOSDETAILREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Assert_OOSDetailGetResult ret = new Assert_OOSDetailGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Assert.OOSDetailGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Assert_RepareRecGetResult
	{
		public uint code;
		public string Message;
		public  DEVDAMAGEREC[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取设备修理记录")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Assert_RepareRecGetResult 
	Assert_RepareRecGet(DEVDAMAGERECREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Assert_RepareRecGetResult ret = new Assert_RepareRecGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Assert.RepareRecGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Assert_RepareApplyResult
	{
		public uint code;
		public string Message;
		public  REPAIRAPPLY vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "设备报修")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Assert_RepareApplyResult 
	Assert_RepareApply(REPAIRAPPLY vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Assert_RepareApplyResult ret = new Assert_RepareApplyResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Assert.RepareApply(vrParameter, out  ret.vrRes);
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
	
	
	public struct Assert_RepareOverResult
	{
		public uint code;
		public string Message;
		public  REPAIROVER vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "设备修理结束")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Assert_RepareOverResult 
	Assert_RepareOver(REPAIROVER vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Assert_RepareOverResult ret = new Assert_RepareOverResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Assert.RepareOver(vrParameter, out  ret.vrRes);
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
	
	
	public struct Assert_RepareCancelResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "撤销设备报修")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Assert_RepareCancelResult 
	Assert_RepareCancel(REPAIRCANCEL vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Assert_RepareCancelResult ret = new Assert_RepareCancelResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Assert.RepareCancel(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Assert_GetCompanyResult
	{
		public uint code;
		public string Message;
		public  UNICOMPANY[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取供应商(生产、供货、维保)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Assert_GetCompanyResult 
	Assert_GetCompany(COMPANYREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Assert_GetCompanyResult ret = new Assert_GetCompanyResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Assert.GetCompany(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Assert_SetCompanyResult
	{
		public uint code;
		public string Message;
		public  UNICOMPANY vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "新建修改供应商")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Assert_SetCompanyResult 
	Assert_SetCompany(UNICOMPANY vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Assert_SetCompanyResult ret = new Assert_SetCompanyResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Assert.SetCompany(vrParameter, out  ret.vrRes);
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
	
	
	public struct Assert_DelCompanyResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除供应商")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Assert_DelCompanyResult 
	Assert_DelCompany(UNICOMPANY vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Assert_DelCompanyResult ret = new Assert_DelCompanyResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Assert.DelCompany(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Assert_AssertLogGetResult
	{
		public uint code;
		public string Message;
		public  ASSERTLOG[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取设备历史档案")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Assert_AssertLogGetResult 
	Assert_AssertLogGet(ASSERTLOGREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Assert_AssertLogGetResult ret = new Assert_AssertLogGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Assert.AssertLogGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	//#endregion Assert部分
	

	//#region Attendance部分
	/*考勤管理*/
	
	public struct Attendance_GetAttendRuleResult
	{
		public uint code;
		public string Message;
		public  ATTENDRULE[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取考勤规则")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Attendance_GetAttendRuleResult 
	Attendance_GetAttendRule(ATTENDRULEREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Attendance_GetAttendRuleResult ret = new Attendance_GetAttendRuleResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Attendance.GetAttendRule(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Attendance_SetAttendRuleResult
	{
		public uint code;
		public string Message;
		public  ATTENDRULE vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "修改考勤规则")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Attendance_SetAttendRuleResult 
	Attendance_SetAttendRule(ATTENDRULE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Attendance_SetAttendRuleResult ret = new Attendance_SetAttendRuleResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Attendance.SetAttendRule(vrParameter, out  ret.vrRes);
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
	
	
	public struct Attendance_DelAttendRuleResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "删除考勤规则")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Attendance_DelAttendRuleResult 
	Attendance_DelAttendRule(ATTENDRULE vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Attendance_DelAttendRuleResult ret = new Attendance_DelAttendRuleResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Attendance.DelAttendRule(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Attendance_GetAttendRecResult
	{
		public uint code;
		public string Message;
		public  ATTENDREC[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取考勤记录")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Attendance_GetAttendRecResult 
	Attendance_GetAttendRec(ATTENDRECREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Attendance_GetAttendRecResult ret = new Attendance_GetAttendRecResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Attendance.GetAttendRec(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct Attendance_GetAttendStatResult
	{
		public uint code;
		public string Message;
		public  ATTENDSTAT[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取考勤统计")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public Attendance_GetAttendStatResult 
	Attendance_GetAttendStat(ATTENDSTATREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		Attendance_GetAttendStatResult ret = new Attendance_GetAttendStatResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.Attendance.GetAttendStat(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	//#endregion Attendance部分
	

	//#region SubSys部分
	/*子系统通信接口*/
	
	public struct SubSys_SubSysLoginResult
	{
		public uint code;
		public string Message;
		public  SUBSYSLOGINRES vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "子系统登录")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public SubSys_SubSysLoginResult 
	SubSys_SubSysLogin(SUBSYSLOGINREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		SubSys_SubSysLoginResult ret = new SubSys_SubSysLoginResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.SubSys.SubSysLogin(vrParameter, out  ret.vrRes);
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
	
	
	public struct SubSys_SubSysLogoutResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "子系统注销")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public SubSys_SubSysLogoutResult 
	SubSys_SubSysLogout(SUBSYSLOGOUTREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		SubSys_SubSysLogoutResult ret = new SubSys_SubSysLogoutResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.SubSys.SubSysLogout(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct SubSys_UploadICUseRecResult
	{
		public uint code;
		public string Message;
		
	}
	
	
	public struct SubSys_UploadPrintRecResult
	{
		public uint code;
		public string Message;
		
	}
	
	
	public struct SubSys_UploadBookOverdueRecResult
	{
		public uint code;
		public string Message;
		
	}
	
	
	public struct SubSys_UploadBreachRecResult
	{
		public uint code;
		public string Message;
		
	}
	
	//#endregion SubSys部分
	

	//#region SubIC部分
	/*IC空间子系统接口*/
	
	public struct SubIC_GetStudyRoomStatResult
	{
		public uint code;
		public string Message;
		public  STUDYROOMSTAT[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取研修间当前状态")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public SubIC_GetStudyRoomStatResult 
	SubIC_GetStudyRoomStat(STUDYROOMSTATREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		SubIC_GetStudyRoomStatResult ret = new SubIC_GetStudyRoomStatResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.SubIC.GetStudyRoomStat(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct SubIC_GetSeatStatResult
	{
		public uint code;
		public string Message;
		public  SEATSTAT[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取座位当前状态")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public SubIC_GetSeatStatResult 
	SubIC_GetSeatStat(SEATSTATREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		SubIC_GetSeatStatResult ret = new SubIC_GetSeatStatResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.SubIC.GetSeatStat(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	//#endregion SubIC部分
	

	//#region SubRT部分
	/*科研实验子系统接口*/
	
	public struct SubRT_GetRTDataResult
	{
		public uint code;
		public string Message;
		public  RTDATA[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "获取科研实验数据")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public SubRT_GetRTDataResult 
	SubRT_GetRTData(RTDATAREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		SubRT_GetRTDataResult ret = new SubRT_GetRTDataResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.SubRT.GetRTData(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	//#endregion SubRT部分
	

	//#region UniSta部分
	/*节点管理*/
	
	public struct UniSta_StaLoginResult
	{
		public uint code;
		public string Message;
		public  STALOGINRES vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "节点登录")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public UniSta_StaLoginResult 
	UniSta_StaLogin(STALOGINREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		UniSta_StaLoginResult ret = new UniSta_StaLoginResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.UniSta.StaLogin(vrParameter, out  ret.vrRes);
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
	
	
	public struct UniSta_StaLogoutResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "节点退出")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public UniSta_StaLogoutResult 
	UniSta_StaLogout(STALOGOUTREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		UniSta_StaLogoutResult ret = new UniSta_StaLogoutResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.UniSta.StaLogout(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct UniSta_StaHandShakeResult
	{
		public uint code;
		public string Message;
		public  HANDSHAKERES vrRes;
	}
	
	[WebMethod (EnableSession = true, Description = "节点与认证服务器定时通信")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public UniSta_StaHandShakeResult 
	UniSta_StaHandShake(HANDSHAKEREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		UniSta_StaHandShakeResult ret = new UniSta_StaHandShakeResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.UniSta.StaHandShake(vrParameter, out  ret.vrRes);
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
	
	
	public struct UniSta_UploadModMoniResult
	{
		public uint code;
		public string Message;
		
	}
	
	
	public struct UniSta_UploadMoniIndexResult
	{
		public uint code;
		public string Message;
		
	}
	
	
	public struct UniSta_UploadMoniRecResult
	{
		public uint code;
		public string Message;
		
	}
	
	//#endregion UniSta部分
	

	//#region UniMoni部分
	/*自动监控*/
	
	public struct UniMoni_SubMoniFromSrvResult
	{
		public uint code;
		public string Message;
		public  MONINDEX[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "子系统获取监控信息缺省值")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public UniMoni_SubMoniFromSrvResult 
	UniMoni_SubMoniFromSrv()
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		UniMoni_SubMoniFromSrvResult ret = new UniMoni_SubMoniFromSrvResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.UniMoni.SubMoniFromSrv( out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct UniMoni_SubMoniToSrvResult
	{
		public uint code;
		public string Message;
		
	}
	
	
	public struct UniMoni_OneMoniChgResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "监控信息状态改变")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public UniMoni_OneMoniChgResult 
	UniMoni_OneMoniChg(MONINDEX vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		UniMoni_OneMoniChgResult ret = new UniMoni_OneMoniChgResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.UniMoni.OneMoniChg(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	
	public struct UniMoni_MoniChgResult
	{
		public uint code;
		public string Message;
		
	}
	
	
	public struct UniMoni_MoniGetResult
	{
		public uint code;
		public string Message;
		public  MODMONI[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "管理员获取详细监控信息")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public UniMoni_MoniGetResult 
	UniMoni_MoniGet(MONIREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		UniMoni_MoniGetResult ret = new UniMoni_MoniGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.UniMoni.MoniGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct UniMoni_MoniRecGetResult
	{
		public uint code;
		public string Message;
		public  MONIREC[] vtRes;
	}
	
	[WebMethod (EnableSession = true, Description = "管理员获取详细监控信息日志")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public UniMoni_MoniRecGetResult 
	UniMoni_MoniRecGet(MONIRECREQ vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		UniMoni_MoniRecGetResult ret = new UniMoni_MoniRecGetResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.UniMoni.MoniRecGet(vrParameter, out  ret.vtRes);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		if ((object) ret.vtRes == null)
		{
			Trace("vrResult == null");
			ret.code = 1;
			return ret;
		}
		
		ret.code = 0;
		return ret;
	}
	
	
	public struct UniMoni_MoniDealErrResult
	{
		public uint code;
		public string Message;
		
	}
	
	[WebMethod (EnableSession = true, Description = "技术支持处理错误")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
    [ScriptMethod]
	public UniMoni_MoniDealErrResult 
	UniMoni_MoniDealErr(MONIDEALERR vrParameter)
	{
		if(Context.Session != null)
		{
			if(Context.Session["SessionID"] != null)
			{
				soaphead.SessionID = (uint)Context.Session["SessionID"];
				soaphead.StationSN = (uint)Context.Session["StationSN"];
			}
		}
		
		UniMoni_MoniDealErrResult ret = new UniMoni_MoniDealErrResult();
		UniRequest m_Request = GetRequest();
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		ret.code = 2;

        uResponse = m_Request.UniMoni.MoniDealErr(vrParameter);
		ret.Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			ret.code = 1;
            return ret;
        }

		
		ret.code = 0;
		return ret;
	}
	
	//#endregion UniMoni部分
	
}
//