
/* ------------------------------------------------------
  版权信息： 杭州联创信息技术有限公司，2008-2011
  文 件 名： UniInterface.h
  创建时间： 2008.08.25
  功能描述： 定义本系统各模块与服务模块的通信接口
  作    者： 何厚武
  --------------------------------------------------------------- 
*/
using System;
using System.Data;
using System.Configuration;
using System.Xml;
using UniStruct;

namespace UniWebLib
{
	#region 共用部分
	public class UniRequest
	{
		public UniDCom m_UniDCom = null;
		public ErrorHandler OnError
		{
			set{
			Admin.OnError = value;
				Station.OnError = value;
				Account.OnError = value;
				Device.OnError = value;
				DoorCtrlSrv.OnError = value;
				Group.OnError = value;
				Reserve.OnError = value;
				Control.OnError = value;
				THIRDIF.OnError = value;
				Fee.OnError = value;
				Console.OnError = value;
				Report.OnError = value;
				System.OnError = value;
				Assert.OnError = value;
				Attendance.OnError = value;
				SubSys.OnError = value;
				SubIC.OnError = value;
				SubRT.OnError = value;
				UniSta.OnError = value;
				UniMoni.OnError = value;
				
			}
		}

		public PRAdmin Admin = null;
		public PRStation Station = null;
		public PRAccount Account = null;
		public PRDevice Device = null;
		public PRDoorCtrlSrv DoorCtrlSrv = null;
		public PRGroup Group = null;
		public PRReserve Reserve = null;
		public PRControl Control = null;
		public PRTHIRDIF THIRDIF = null;
		public PRFee Fee = null;
		public PRConsole Console = null;
		public PRReport Report = null;
		public PRSystem System = null;
		public PRAssert Assert = null;
		public PRAttendance Attendance = null;
		public PRSubSys SubSys = null;
		public PRSubIC SubIC = null;
		public PRSubRT SubRT = null;
		public PRUniSta UniSta = null;
		public PRUniMoni UniMoni = null;
		public UniRequest()
		{
			m_UniDCom = new UniDCom();
			Admin = new PRAdmin(m_UniDCom);
			Station = new PRStation(m_UniDCom);
			Account = new PRAccount(m_UniDCom);
			Device = new PRDevice(m_UniDCom);
			DoorCtrlSrv = new PRDoorCtrlSrv(m_UniDCom);
			Group = new PRGroup(m_UniDCom);
			Reserve = new PRReserve(m_UniDCom);
			Control = new PRControl(m_UniDCom);
			THIRDIF = new PRTHIRDIF(m_UniDCom);
			Fee = new PRFee(m_UniDCom);
			Console = new PRConsole(m_UniDCom);
			Report = new PRReport(m_UniDCom);
			System = new PRSystem(m_UniDCom);
			Assert = new PRAssert(m_UniDCom);
			Attendance = new PRAttendance(m_UniDCom);
			SubSys = new PRSubSys(m_UniDCom);
			SubIC = new PRSubIC(m_UniDCom);
			SubRT = new PRSubRT(m_UniDCom);
			UniSta = new PRUniSta(m_UniDCom);
			UniMoni = new PRUniMoni(m_UniDCom);

		}


		public string szErrMessage
		{
			get
			{
				string szMessage;
				m_UniDCom.GetLastErr(out szMessage);
				return szMessage;
			}
		}
		public string szErrMsg
		{
			get
			{
				string szMessage;
				m_UniDCom.GetLastErr(out szMessage);
				return szMessage;
			}
		}
		public uint GetLastErr(out string strErrMsg)
		{
			uint nRet = m_UniDCom.GetLastErr(out strErrMsg);
			return nRet;
		}
	};
	public partial class PRModule
	{
		protected uint m_nModule = 0;
		protected UniDCom m_UniDCom = null;
		byte[] detail = null;
		uint m_uLastCommand = 0;

		public PRModule(UniDCom _UniDCom)
		{
			m_UniDCom = _UniDCom;
		}
		public PRModule()
		{
			m_UniDCom = null;
			m_nModule = 0;
		}
		public uint GetCMD(uint nSubCmdCode)
		{
			uint uCmd = m_nModule | nSubCmdCode;
			m_uLastCommand = uCmd;
			return uCmd;
		}
    
    /*==================================================
    
		public bool GetDetail<T>(out CUniStruct<T> vrDetail) where T:new()
		{
			vrDetail = new CUniStruct<T>();
			if(detail == null)
			{
				return false;
			}
			Import(vrDetail, detail);
			return true;
		}
		public bool GetDetail<T>(out CUniStructArray<T> vtDetail) where T:new()
		{
			vtDetail = new CUniStructArray<T>();
			if(detail == null)
			{
				return false;
			}
			Import(vtDetail, detail);
			return true;
		}
		public REQUESTCODE Cmd<T>(uint uCmd,out CUniStructArray<T> vtResult) where T:new()
		{
			return Cmd<T,T>(uCmd,null,out vtResult);
		}
		public REQUESTCODE Cmd<T>(uint uCmd,CUniStruct<T> vrInput) where T:new()
		{
			CUniStructArray<T> result = null;
			return Cmd<T,T>(uCmd,vrInput,out result);
		}
		public REQUESTCODE Cmd<T>(uint uCmd,out CUniStruct<T> vrResult) where T:new()
		{
			CUniStruct<T> vrInput = null;
			return Cmd<T,T>(uCmd,vrInput,out vrResult);
		}
		public REQUESTCODE Cmd<T>(uint uCmd,CUniStructArray<T> vrInput) where T:new()
		{
			detail = null;
			uint uCommand = GetCMD(uCmd);
			byte[] result = null;
			byte[] input = null;
			if(vrInput != null)
			{
				input = vrInput.Export();
			}
			REQUESTCODE uRequest = (REQUESTCODE)m_UniDCom.RequestBin(uCommand, input, out result);
			return uRequest;
		}
		public REQUESTCODE Cmd(uint uCmd)
		{
			detail = null;
			uint uCommand = GetCMD(uCmd);
			byte[] result = null;
			REQUESTCODE uRequest = (REQUESTCODE)m_UniDCom.RequestBin(uCommand, null, out result);
			return uRequest;
		}
		public REQUESTCODE Cmd<T,V>(uint uCmd,CUniStruct<T> vrInput,out CUniStructArray<V> vtResult) where T:new() where V:new()
		{
			detail = null;
			uint uCommand = GetCMD(uCmd);
			vtResult = null;
			byte[] result = null;
			byte[] input = null;
			if(vrInput != null)
			{
				input = vrInput.Export();
			}
			REQUESTCODE uRequest = (REQUESTCODE)m_UniDCom.RequestBin(uCommand, input, out result);
			if (uRequest == REQUESTCODE.EXECUTE_SUCCESS)
			{
				vtResult = new CUniStructArray<V>();
				uRequest = Import(vtResult, result);
			}
			return uRequest;
		}
		public REQUESTCODE Cmd<T,V>(uint uCmd,CUniStruct<T> vrInput,out CUniStruct<V> vrResult) where T:new() where V:new()
		{
			detail = null;
			uint uCommand = GetCMD(uCmd);
			vrResult = null;
			byte[] result = null;
			byte[] input = null;
			if(vrInput != null)
			{
				input = vrInput.Export();
			}
			REQUESTCODE uRequest = (REQUESTCODE)m_UniDCom.RequestBin(uCommand, input, out result);
			if (uRequest == REQUESTCODE.EXECUTE_SUCCESS)
			{
				vrResult = new CUniStruct<V>();
				uRequest = Import(vrResult, result);
			}
			return uRequest;
		}

		protected REQUESTCODE Import<T>(CUniStructArray<T> vtRet, byte[] result) where T:new()
		{
			REQUESTCODE uRequest = REQUESTCODE.EXECUTE_SUCCESS;
			uint n = 0;
			if (result != null && result.Length > 0)
			{
				n = vtRet.Import(result);
				if (n <= 0)
				{
					n = 0;
					uRequest = REQUESTCODE.ERR_IMPORT;
				}
			}
			
			if(result != null && result.Length - n > 0)
			{
				byte[] newdetail = new byte[result.Length - n];
				Array.Copy(result,n,newdetail,0,result.Length - n);
				detail = newdetail;
			}else{
				detail = null;
			}
			return uRequest;
		}
		protected REQUESTCODE Import<T>(CUniStruct<T> vrRet, byte[] result) where T:new()
		{
			REQUESTCODE uRequest = REQUESTCODE.EXECUTE_SUCCESS;
			uint n = 0;

			if (result != null && result.Length > 0)
			{
				n = vrRet.Import(result);
				if (n <= 0)
				{
					n = 0;
					uRequest = REQUESTCODE.ERR_IMPORT;
				}
			}
			if(result != null && result.Length - n > 0)
			{
				byte[] newdetail = new byte[result.Length - n];
				Array.Copy(result,n,newdetail,0,result.Length - n);
				detail = newdetail;
			}else{
				detail = null;
			}
			return uRequest;
		}
    
    ==================================================*/
    //==================================================
    	public bool UTPeekDetail<T>(out T vrDetail) where T:new()
		{
			vrDetail = new T();
			if(detail == null)
			{
				return false;
			}
			REQUESTCODE uRequest = UTImport(out vrDetail, detail, false);
			if(uRequest != REQUESTCODE.EXECUTE_SUCCESS)
			{
				SysConsole.LogError(m_UniDCom.SessionID.ToString(),m_uLastCommand,"解析返回附加结构失败,"+typeof(T).Name);
			}
			return true;
		}
		public bool UTPeekDetail<T>(out T[] vtDetail) where T:new()
		{
			vtDetail = null;
			if(detail == null)
			{
				return false;
			}
			REQUESTCODE uRequest = UTImport(out vtDetail, detail, false);
			if(uRequest != REQUESTCODE.EXECUTE_SUCCESS)
			{
				SysConsole.LogError(m_UniDCom.SessionID.ToString(),m_uLastCommand,"解析返回附加结构失败,"+typeof(T).Name+"[]");
			}
			return true;
		}
		public bool UTGetDetail<T>(out T vrDetail) where T:new()
		{
			vrDetail = new T();
			if(detail == null)
			{
				return false;
			}
			REQUESTCODE uRequest = UTImport(out vrDetail, detail, true);
			if(uRequest != REQUESTCODE.EXECUTE_SUCCESS)
			{
				SysConsole.LogError(m_UniDCom.SessionID.ToString(),m_uLastCommand,"解析返回附加结构失败,"+typeof(T).Name);
			}
			return true;
		}
		public bool UTGetDetail<T>(out T[] vtDetail) where T:new()
		{
			vtDetail = null;
			if(detail == null)
			{
				return false;
			}
			REQUESTCODE uRequest = UTImport(out vtDetail, detail, true);
			if(uRequest != REQUESTCODE.EXECUTE_SUCCESS)
			{
				SysConsole.LogError(m_UniDCom.SessionID.ToString(),m_uLastCommand,"解析返回附加结构失败,"+typeof(T).Name+"[]");
			}
			return true;
		}
		public REQUESTCODE UTCmd<T>(uint uCmd,out T[] vtResult) where T:new()
		{
			return UTCmd<T,T>(uCmd,out vtResult);
		}
		public REQUESTCODE UTCmd<T>(uint uCmd,T vrInput) where T:new()
		{
			detail = null;
			uint uCommand = GetCMD(uCmd);
			byte[] result = null;
			byte[] input = null;
			UniStructCS.CUniStructCS uccs = new UniStructCS.CUniStructCS();
			if(vrInput != null)
			{
				input = uccs.Export(vrInput);
			}
			REQUESTCODE uRequest = (REQUESTCODE)m_UniDCom.RequestBin(uCommand, input, out result);
			return uRequest;
		}
		public REQUESTCODE UTCmd<T>(uint uCmd,out T vrResult) where T:new()
		{
			detail = null;
			uint uCommand = GetCMD(uCmd);
			vrResult = new T();
			byte[] result = null;
			byte[] input = null;

			REQUESTCODE uRequest = (REQUESTCODE)m_UniDCom.RequestBin(uCommand, input, out result);
			if (uRequest == REQUESTCODE.EXECUTE_SUCCESS)
			{
				vrResult = new T();
				uRequest = UTImport(out vrResult, result, true);
				if(uRequest != REQUESTCODE.EXECUTE_SUCCESS)
				{
					SysConsole.LogError(m_UniDCom.SessionID.ToString(),uCommand,"解析返回值失败");
				}
			}
			return uRequest;
		}
		public REQUESTCODE UTCmd<T>(uint uCmd,T[] vrInput) where T:new()
		{
			detail = null;
			uint uCommand = GetCMD(uCmd);
			byte[] result = null;
			byte[] input = null;
			UniStructCS.CUniStructArrayCS ucacs = new UniStructCS.CUniStructArrayCS();
			if(vrInput != null)
			{
				input = ucacs.Export(vrInput);
			}
			REQUESTCODE uRequest = (REQUESTCODE)m_UniDCom.RequestBin(uCommand, input, out result);
			return uRequest;
		}
		public REQUESTCODE UTCmd(uint uCmd)
		{
			detail = null;
			uint uCommand = GetCMD(uCmd);
			byte[] result = null;
			REQUESTCODE uRequest = (REQUESTCODE)m_UniDCom.RequestBin(uCommand, null, out result);
			return uRequest;
		}
		public REQUESTCODE UTCmd<T,V>(uint uCmd,T vrInput,out V[] vtResult) where T:new() where V:new()
		{
			detail = null;
			uint uCommand = GetCMD(uCmd);
			vtResult = null;
			byte[] result = null;
			byte[] input = null;
			UniStructCS.CUniStructCS uccs = new UniStructCS.CUniStructCS();
			if(vrInput != null)
			{
				input = uccs.Export(vrInput);
			}
			REQUESTCODE uRequest = (REQUESTCODE)m_UniDCom.RequestBin(uCommand, input, out result);
			if (uRequest == REQUESTCODE.EXECUTE_SUCCESS)
			{
				uRequest = UTImport(out vtResult, result, true);
				if(uRequest != REQUESTCODE.EXECUTE_SUCCESS)
				{
					SysConsole.LogError(m_UniDCom.SessionID.ToString(),uCommand,"解析返回值失败");
				}
			}
			return uRequest;
		}
		public REQUESTCODE UTCmd<T,V>(uint uCmd,out V[] vtResult) where T:new() where V:new()
		{
			detail = null;
			uint uCommand = GetCMD(uCmd);
			vtResult = null;
			byte[] result = null;
			byte[] input = null;
			UniStructCS.CUniStructCS uccs = new UniStructCS.CUniStructCS();
			REQUESTCODE uRequest = (REQUESTCODE)m_UniDCom.RequestBin(uCommand, input, out result);
			if (uRequest == REQUESTCODE.EXECUTE_SUCCESS)
			{
				uRequest = UTImport(out vtResult, result, true);
				if(uRequest != REQUESTCODE.EXECUTE_SUCCESS)
				{
					SysConsole.LogError(m_UniDCom.SessionID.ToString(),uCommand,"解析返回值失败");
				}
			}
			return uRequest;
		}
		public REQUESTCODE UTCmd<T,V>(uint uCmd,T vrInput,out V vrResult) where T:new() where V:new()
		{
			detail = null;
			uint uCommand = GetCMD(uCmd);
			vrResult = new V();
			byte[] result = null;
			byte[] input = null;
			UniStructCS.CUniStructCS uccs = new UniStructCS.CUniStructCS();
			if(vrInput != null)
			{
				input = uccs.Export(vrInput);
			}

			REQUESTCODE uRequest = (REQUESTCODE)m_UniDCom.RequestBin(uCommand, input, out result);
			if (uRequest == REQUESTCODE.EXECUTE_SUCCESS)
			{
				vrResult = new V();
				uRequest = UTImport(out vrResult, result, true);
				if(uRequest != REQUESTCODE.EXECUTE_SUCCESS)
				{
					SysConsole.LogError(m_UniDCom.SessionID.ToString(),uCommand,"解析返回值失败");
				}
			}
			return uRequest;
		}
		protected REQUESTCODE UTImport<T>(out T[] vtRet, byte[] result, bool bNoPeek) where T:new()
		{
			REQUESTCODE uRequest = REQUESTCODE.EXECUTE_SUCCESS;
			uint n = 0;
			vtRet = (T[])(object)null;
			if (result != null && result.Length > 0)
			{
				UniStructCS.CUniStructArrayCS ucacs = new UniStructCS.CUniStructArrayCS();
				n = ucacs.Import(out vtRet,result);
				if (n <= 0)
				{
					n = 0;
					uRequest = REQUESTCODE.ERR_IMPORT;
				}
			}
			
			if(bNoPeek)
			{
				if(result != null && result.Length - n > 0)
				{
					byte[] newdetail = new byte[result.Length - n];
					Array.Copy(result,n,newdetail,0,result.Length - n);
					detail = newdetail;
				}else{
					detail = null;
				}
			}
			return uRequest;
		}
		protected REQUESTCODE UTImport<T>(out T vrRet, byte[] result,bool bNoPeek) where T:new()
		{
			REQUESTCODE uRequest = REQUESTCODE.EXECUTE_SUCCESS;
			uint n = 0;
			vrRet = new T();
			if (result != null && result.Length > 0)
			{
				UniStructCS.CUniStructCS uccs = new UniStructCS.CUniStructCS();
				n = uccs.Import(out vrRet,result);
				if (n <= 0)
				{
					n = 0;
					uRequest = REQUESTCODE.ERR_IMPORT;
				}
			}
			if(bNoPeek)
			{
				if(result != null && result.Length - n > 0)
				{
					byte[] newdetail = new byte[result.Length - n];
					Array.Copy(result,n,newdetail,0,result.Length - n);
					detail = newdetail;
				}else{
					detail = null;
				}
			}
			return uRequest;
		}
	};
	#endregion 共用部分

	#region PRAdmin部分
	/*管理员管理*/
	public partial class PRAdmin:PRModule
	{
		public PRAdmin(UniDCom _UniDCom):base(_UniDCom)
		{
			m_nModule = ADMIN_BASE;
		}
		
		/*public REQUESTCODE Get(CUniStruct<ADMINREQ> vrParameter,out CUniStructArray<UNIADMIN> vrResult)
		{
			return Cmd(MSREQ_ADMIN_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE Get(ADMINREQ  vrParameter,out UNIADMIN[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ADMIN_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ADMIN_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetIF(CUniStruct<IFPARAMREQ> vrParameter,out CUniStructArray<IFPARAM> vrResult)
		{
			return Cmd(MSREQ_ADMIN_IFGET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetIF(IFPARAMREQ  vrParameter,out IFPARAM[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ADMIN_IFGET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ADMIN_IFGET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetAdminLog(CUniStruct<ADMINLOGREQ> vrParameter,out CUniStructArray<ADMINLOG> vrResult)
		{
			return Cmd(MSREQ_ADMINLOG_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetAdminLog(ADMINLOGREQ  vrParameter,out ADMINLOG[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ADMINLOG_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ADMINLOG_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetManRoom(CUniStruct<MANROOMREQ> vrParameter,out CUniStructArray<MANROOM> vrResult)
		{
			return Cmd(MSREQ_ADMIN_MANROOMGET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetManRoom(MANROOMREQ  vrParameter,out MANROOM[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ADMIN_MANROOMGET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ADMIN_MANROOMGET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE Set(CUniStruct<UNIADMIN> vrParameter,out CUniStruct<UNIADMIN> vrResult)
		{
			return Cmd(MSREQ_ADMIN_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE Set(UNIADMIN  vrParameter,out UNIADMIN  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ADMIN_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ADMIN_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE SaveIF(CUniStruct<IFPARAM> vrParameter)
		{
			return Cmd(MSREQ_ADMIN_IFSAVE,vrParameter);
		}*/
		
		public REQUESTCODE SaveIF(IFPARAM  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ADMIN_IFSAVE,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ADMIN_IFSAVE,vrParameter);
			}
		}


		/*public REQUESTCODE ClearIPBlackList(CUniStruct<IPBLACKLIST> vrParameter)
		{
			return Cmd(MSREQ_IPBLACKLIST_CLEAR,vrParameter);
		}*/
		
		public REQUESTCODE ClearIPBlackList(IPBLACKLIST  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_IPBLACKLIST_CLEAR,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_IPBLACKLIST_CLEAR,vrParameter);
			}
		}


		/*public REQUESTCODE AdminChgPasswd(CUniStruct<ADMINCHGPASSWD> vrParameter)
		{
			return Cmd(MSREQ_ADMIN_CHGPW,vrParameter);
		}*/
		
		public REQUESTCODE AdminChgPasswd(ADMINCHGPASSWD  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ADMIN_CHGPW,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ADMIN_CHGPW,vrParameter);
			}
		}


		/*public REQUESTCODE StaffSet(CUniStruct<STAFFINFO> vrParameter)
		{
			return Cmd(MSREQ_STAFF_SET,vrParameter);
		}*/
		
		public REQUESTCODE StaffSet(STAFFINFO  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_STAFF_SET,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_STAFF_SET,vrParameter);
			}
		}


		/*public REQUESTCODE Del(CUniStruct<UNIADMIN> vrParameter)
		{
			return Cmd(MSREQ_ADMIN_DEL,vrParameter);
		}*/
		
		public REQUESTCODE Del(UNIADMIN  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ADMIN_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ADMIN_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE AdminCheckGet(CUniStruct<CHECKREQ> vrParameter,out CUniStructArray<CHECKINFO> vrResult)
		{
			return Cmd(MSREQ_ADMIN_CHECKGET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE AdminCheckGet(CHECKREQ  vrParameter,out CHECKINFO[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ADMIN_CHECKGET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ADMIN_CHECKGET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE AdminCheck(CUniStruct<ADMINCHECK> vrParameter)
		{
			return Cmd(MSREQ_ADMIN_CHECK,vrParameter);
		}*/
		
		public REQUESTCODE AdminCheck(ADMINCHECK  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ADMIN_CHECK,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ADMIN_CHECK,vrParameter);
			}
		}


		/*public REQUESTCODE AdminCheckLogGet(CUniStruct<CHECKLOGREQ> vrParameter,out CUniStructArray<CHECKLOG> vrResult)
		{
			return Cmd(MSREQ_ADMIN_CHECKLOGGET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE AdminCheckLogGet(CHECKLOGREQ  vrParameter,out CHECKLOG[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ADMIN_CHECKLOGGET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ADMIN_CHECKLOGGET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE Login(CUniStruct<ADMINLOGINREQ> vrParameter,out CUniStruct<ADMINLOGINRES> vrResult)
		{
			return Cmd(MSREQ_ADMIN_LOGIN,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE Login(ADMINLOGINREQ  vrParameter,out ADMINLOGINRES  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ADMIN_LOGIN,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ADMIN_LOGIN,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE StaLogin(CUniStruct<ADMINLOGINREQ> vrParameter,out CUniStruct<ADMINLOGINRES> vrResult)
		{
			return Cmd(MSREQ_ADMIN_STALOGIN,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE StaLogin(ADMINLOGINREQ  vrParameter,out ADMINLOGINRES  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ADMIN_STALOGIN,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ADMIN_STALOGIN,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE MobileStaLogin(CUniStruct<MOBILELOGINREQ> vrParameter,out CUniStruct<ADMINLOGINRES> vrResult)
		{
			return Cmd(MSREQ_MOBILE_STALOGIN,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE MobileStaLogin(MOBILELOGINREQ  vrParameter,out ADMINLOGINRES  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_MOBILE_STALOGIN,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_MOBILE_STALOGIN,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE Logout(CUniStruct<ADMINLOGOUTREQ> vrParameter,out CUniStruct<ADMINLOGOUTRES> vrResult)
		{
			return Cmd(MSREQ_ADMIN_LOGOUT,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE Logout(ADMINLOGOUTREQ  vrParameter,out ADMINLOGOUTRES  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ADMIN_LOGOUT,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ADMIN_LOGOUT,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE UIDInfoGet(CUniStruct<UIDINFOREQ> vrParameter,out CUniStructArray<UIDINFO> vrResult)
		{
			return Cmd(MSREQ_UIDINFO_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE UIDInfoGet(UIDINFOREQ  vrParameter,out UIDINFO[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_UIDINFO_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_UIDINFO_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE OPPrivGet(CUniStruct<OPPRIVREQ> vrParameter,out CUniStructArray<OPPRIV> vrResult)
		{
			return Cmd(MSREQ_OPPRIV_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE OPPrivGet(OPPRIVREQ  vrParameter,out OPPRIV[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_OPPRIV_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_OPPRIV_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE OPPrivSet(CUniStruct<OPPRIV> vrParameter,out CUniStruct<OPPRIV> vrResult)
		{
			return Cmd(MSREQ_OPPRIV_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE OPPrivSet(OPPRIV  vrParameter,out OPPRIV  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_OPPRIV_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_OPPRIV_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE OPPrivDel(CUniStruct<OPPRIV> vrParameter)
		{
			return Cmd(MSREQ_OPPRIV_DEL,vrParameter);
		}*/
		
		public REQUESTCODE OPPrivDel(OPPRIV  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_OPPRIV_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_OPPRIV_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE UserRoleGet(CUniStruct<USERROLEREQ> vrParameter,out CUniStructArray<USERROLE> vrResult)
		{
			return Cmd(MSREQ_USERROLE_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE UserRoleGet(USERROLEREQ  vrParameter,out USERROLE[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_USERROLE_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_USERROLE_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE UserRoleSet(CUniStruct<USERROLE> vrParameter,out CUniStruct<USERROLE> vrResult)
		{
			return Cmd(MSREQ_USERROLE_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE UserRoleSet(USERROLE  vrParameter,out USERROLE  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_USERROLE_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_USERROLE_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE UserRoleDel(CUniStruct<USERROLE> vrParameter)
		{
			return Cmd(MSREQ_USERROLE_DEL,vrParameter);
		}*/
		
		public REQUESTCODE UserRoleDel(USERROLE  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_USERROLE_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_USERROLE_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE CltPWGet(out CUniStructArray<CLTPASSWD> vrResult)
		{
			return Cmd(MSREQ_ADMIN_CLTPWGET,out vrResult);
		}*/
		
		public REQUESTCODE CltPWGet(out CLTPASSWD[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ADMIN_CLTPWGET,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ADMIN_CLTPWGET,out vrResult);
			}
		}


		/*public REQUESTCODE CltPWSet(CUniStruct<CLTPASSWD> vrParameter,out CUniStruct<CLTPASSWD> vrResult)
		{
			return Cmd(MSREQ_ADMIN_CLTPWSET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE CltPWSet(CLTPASSWD  vrParameter,out CLTPASSWD  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ADMIN_CLTPWSET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ADMIN_CLTPWSET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE AdminRefreshFlagGet(CUniStruct<REFRESHFLAGREQ> vrParameter,out CUniStructArray<REFRESHFLAGINFO> vrResult)
		{
			return Cmd(MSREQ_ADMIN_REFRESHFLAGGET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE AdminRefreshFlagGet(REFRESHFLAGREQ  vrParameter,out REFRESHFLAGINFO[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ADMIN_REFRESHFLAGGET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ADMIN_REFRESHFLAGGET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE HolidDayGet(CUniStruct<HOLIDAYREQ> vrParameter,out CUniStructArray<UNIHOLIDAY> vrResult)
		{
			return Cmd(MSREQ_HOLIDAY_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE HolidDayGet(HOLIDAYREQ  vrParameter,out UNIHOLIDAY[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_HOLIDAY_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_HOLIDAY_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE HolidDaySet(CUniStruct<UNIHOLIDAY> vrParameter)
		{
			return Cmd(MSREQ_HOLIDAY_SET,vrParameter);
		}*/
		
		public REQUESTCODE HolidDaySet(UNIHOLIDAY  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_HOLIDAY_SET,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_HOLIDAY_SET,vrParameter);
			}
		}


		/*public REQUESTCODE HolidDayDel(CUniStruct<UNIHOLIDAY> vrParameter)
		{
			return Cmd(MSREQ_HOLIDAY_DEL,vrParameter);
		}*/
		
		public REQUESTCODE HolidDayDel(UNIHOLIDAY  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_HOLIDAY_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_HOLIDAY_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE CheckExist(CUniStruct<CHECKEXISTREQ> vrParameter)
		{
			return Cmd(MSREQ_CHECK_EXIST,vrParameter);
		}*/
		
		public REQUESTCODE CheckExist(CHECKEXISTREQ  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CHECK_EXIST,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CHECK_EXIST,vrParameter);
			}
		}


		/*public REQUESTCODE GetMaxValue(CUniStruct<MAXVALUEREQ> vrParameter,out CUniStruct<MAXVALUE> vrResult)
		{
			return Cmd(MSREQ_MAXVALUE_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetMaxValue(MAXVALUEREQ  vrParameter,out MAXVALUE  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_MAXVALUE_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_MAXVALUE_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetBasicStatInfo(out CUniStruct<BASICSTAT> vrResult)
		{
			return Cmd(MSREQ_BASICSTAT_GET,out vrResult);
		}*/
		
		public REQUESTCODE GetBasicStatInfo(out BASICSTAT  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_BASICSTAT_GET,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_BASICSTAT_GET,out vrResult);
			}
		}


		/*public REQUESTCODE CheckTypeGet(CUniStruct<CHECKTYPEREQ> vrParameter,out CUniStructArray<CHECKTYPE> vrResult)
		{
			return Cmd(MSREQ_CHECKTYPE_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE CheckTypeGet(CHECKTYPEREQ  vrParameter,out CHECKTYPE[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CHECKTYPE_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CHECKTYPE_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE CheckTypeSet(CUniStruct<CHECKTYPE> vrParameter)
		{
			return Cmd(MSREQ_CHECKTYPE_SET,vrParameter);
		}*/
		
		public REQUESTCODE CheckTypeSet(CHECKTYPE  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CHECKTYPE_SET,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CHECKTYPE_SET,vrParameter);
			}
		}


		/*public REQUESTCODE GetUserFeedback(CUniStruct<USERFEEDBACKREQ> vrParameter,out CUniStructArray<USERFEEDBACK> vrResult)
		{
			return Cmd(MSREQ_USERFEEDBACK_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetUserFeedback(USERFEEDBACKREQ  vrParameter,out USERFEEDBACK[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_USERFEEDBACK_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_USERFEEDBACK_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DoUserFeedback(CUniStruct<USERFEEDBACK> vrParameter,out CUniStruct<USERFEEDBACK> vrResult)
		{
			return Cmd(MSREQ_USERFEEDBACK_DO,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE DoUserFeedback(USERFEEDBACK  vrParameter,out USERFEEDBACK  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_USERFEEDBACK_DO,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_USERFEEDBACK_DO,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE ReplyUserFeedback(CUniStruct<USERFEEDBACK> vrParameter)
		{
			return Cmd(MSREQ_USERFEEDBACK_REPLY,vrParameter);
		}*/
		
		public REQUESTCODE ReplyUserFeedback(USERFEEDBACK  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_USERFEEDBACK_REPLY,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_USERFEEDBACK_REPLY,vrParameter);
			}
		}


		/*public REQUESTCODE ServiceTypeGet(CUniStruct<SERVICETYPEREQ> vrParameter,out CUniStructArray<UNISERVICETYPE> vrResult)
		{
			return Cmd(MSREQ_SERVICETYPE_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE ServiceTypeGet(SERVICETYPEREQ  vrParameter,out UNISERVICETYPE[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_SERVICETYPE_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_SERVICETYPE_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE ServiceTypeSet(CUniStruct<UNISERVICETYPE> vrParameter)
		{
			return Cmd(MSREQ_SERVICETYPE_SET,vrParameter);
		}*/
		
		public REQUESTCODE ServiceTypeSet(UNISERVICETYPE  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_SERVICETYPE_SET,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_SERVICETYPE_SET,vrParameter);
			}
		}


		/*public REQUESTCODE GetPollOnLine(CUniStruct<POLLONLINEREQ> vrParameter,out CUniStructArray<POLLONLINE> vrResult)
		{
			return Cmd(MSREQ_POLLONLINE_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetPollOnLine(POLLONLINEREQ  vrParameter,out POLLONLINE[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_POLLONLINE_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_POLLONLINE_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE SetPollOnLine(CUniStruct<POLLONLINE> vrParameter,out CUniStruct<POLLONLINE> vrResult)
		{
			return Cmd(MSREQ_POLLONLINE_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE SetPollOnLine(POLLONLINE  vrParameter,out POLLONLINE  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_POLLONLINE_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_POLLONLINE_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE VotePollOnLine(CUniStructArray<POLLVOTE> vrParameter)
		{
			return Cmd(MSREQ_POLLONLINE_VOTE,vrParameter);
		}*/
		
		public REQUESTCODE VotePollOnLine(POLLVOTE[]  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_POLLONLINE_VOTE,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_POLLONLINE_VOTE,vrParameter);
			}
		}

	
	}
	#endregion PRAdmin部分

	#region PRStation部分
	/*站点管理*/
	public partial class PRStation:PRModule
	{
		public PRStation(UniDCom _UniDCom):base(_UniDCom)
		{
			m_nModule = STATION_BASE;
		}
		
		/*public REQUESTCODE GetStation(CUniStruct<STATIONREQ> vrParameter,out CUniStructArray<UNISTATION> vrResult)
		{
			return Cmd(MSREQ_STATION_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetStation(STATIONREQ  vrParameter,out UNISTATION[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_STATION_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_STATION_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE SetStation(CUniStruct<UNISTATION> vrParameter,out CUniStruct<UNISTATION> vrResult)
		{
			return Cmd(MSREQ_STATION_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE SetStation(UNISTATION  vrParameter,out UNISTATION  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_STATION_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_STATION_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DelStation(CUniStruct<UNISTATION> vrParameter)
		{
			return Cmd(MSREQ_STATION_DEL,vrParameter);
		}*/
		
		public REQUESTCODE DelStation(UNISTATION  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_STATION_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_STATION_DEL,vrParameter);
			}
		}

	
	}
	#endregion PRStation部分

	#region PRAccount部分
	/*帐户管理*/
	public partial class PRAccount:PRModule
	{
		public PRAccount(UniDCom _UniDCom):base(_UniDCom)
		{
			m_nModule = ACCOUNT_BASE;
		}
		
		/*public REQUESTCODE DeptGet(CUniStruct<DEPTREQ> vrParameter,out CUniStructArray<UNIDEPT> vrResult)
		{
			return Cmd(MSREQ_DEPT_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE DeptGet(DEPTREQ  vrParameter,out UNIDEPT[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DEPT_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DEPT_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DeptSet(CUniStruct<UNIDEPT> vrParameter,out CUniStruct<UNIDEPT> vrResult)
		{
			return Cmd(MSREQ_DEPT_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE DeptSet(UNIDEPT  vrParameter,out UNIDEPT  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DEPT_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DEPT_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DeptDel(CUniStruct<UNIDEPT> vrParameter)
		{
			return Cmd(MSREQ_DEPT_DEL,vrParameter);
		}*/
		
		public REQUESTCODE DeptDel(UNIDEPT  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DEPT_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DEPT_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE CampusGet(CUniStruct<CAMPUSREQ> vrParameter,out CUniStructArray<UNICAMPUS> vrResult)
		{
			return Cmd(MSREQ_CAMPUS_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE CampusGet(CAMPUSREQ  vrParameter,out UNICAMPUS[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CAMPUS_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CAMPUS_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE CampusSet(CUniStruct<UNICAMPUS> vrParameter,out CUniStruct<UNICAMPUS> vrResult)
		{
			return Cmd(MSREQ_CAMPUS_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE CampusSet(UNICAMPUS  vrParameter,out UNICAMPUS  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CAMPUS_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CAMPUS_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE CampusDel(CUniStruct<UNICAMPUS> vrParameter)
		{
			return Cmd(MSREQ_CAMPUS_DEL,vrParameter);
		}*/
		
		public REQUESTCODE CampusDel(UNICAMPUS  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CAMPUS_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CAMPUS_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE ClassGet(CUniStruct<CLASSREQ> vrParameter,out CUniStructArray<UNICLASS> vrResult)
		{
			return Cmd(MSREQ_CLS_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE ClassGet(CLASSREQ  vrParameter,out UNICLASS[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CLS_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CLS_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE ClassSet(CUniStruct<UNICLASS> vrParameter,out CUniStruct<UNICLASS> vrResult)
		{
			return Cmd(MSREQ_CLS_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE ClassSet(UNICLASS  vrParameter,out UNICLASS  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CLS_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CLS_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE ClassDel(CUniStruct<UNICLASS> vrParameter,out CUniStruct<UNICLASS> vrResult)
		{
			return Cmd(MSREQ_CLS_DEL,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE ClassDel(UNICLASS  vrParameter,out UNICLASS  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CLS_DEL,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CLS_DEL,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE Get(CUniStruct<ACCREQ> vrParameter,out CUniStructArray<UNIACCOUNT> vrResult)
		{
			return Cmd(MSREQ_ACC_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE Get(ACCREQ  vrParameter,out UNIACCOUNT[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ACC_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ACC_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE TutorGet(CUniStruct<TUTORREQ> vrParameter,out CUniStructArray<UNITUTOR> vrResult)
		{
			return Cmd(MSREQ_TUTOR_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE TutorGet(TUTORREQ  vrParameter,out UNITUTOR[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_TUTOR_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_TUTOR_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE TutorStudentGet(CUniStruct<TUTORSTUDENTREQ> vrParameter,out CUniStructArray<TUTORSTUDENT> vrResult)
		{
			return Cmd(MSREQ_TUTORSTUDENT_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE TutorStudentGet(TUTORSTUDENTREQ  vrParameter,out TUTORSTUDENT[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_TUTORSTUDENT_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_TUTORSTUDENT_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE ExtIdentAccGet(CUniStruct<EXTIDENTACCREQ> vrParameter,out CUniStructArray<EXTIDENTACC> vrResult)
		{
			return Cmd(MSREQ_EXTIDENTACC_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE ExtIdentAccGet(EXTIDENTACCREQ  vrParameter,out EXTIDENTACC[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_EXTIDENTACC_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_EXTIDENTACC_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE AccInfoGet(CUniStruct<ACCINFOREQ> vrParameter,out CUniStruct<UNIACCOUNT> vrResult)
		{
			return Cmd(MSREQ_ACCINFO_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE AccInfoGet(ACCINFOREQ  vrParameter,out UNIACCOUNT  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ACCINFO_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ACCINFO_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE Set(CUniStruct<UNIACCOUNT> vrParameter,out CUniStruct<UNIACCOUNT> vrResult)
		{
			return Cmd(MSREQ_ACC_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE Set(UNIACCOUNT  vrParameter,out UNIACCOUNT  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ACC_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ACC_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE ExtIdentAccSet(CUniStruct<EXTIDENTACC> vrParameter)
		{
			return Cmd(MSREQ_EXTIDENTACC_SET,vrParameter);
		}*/
		
		public REQUESTCODE ExtIdentAccSet(EXTIDENTACC  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_EXTIDENTACC_SET,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_EXTIDENTACC_SET,vrParameter);
			}
		}


		/*public REQUESTCODE TutorStudentSet(CUniStruct<TUTORSTUDENT> vrParameter)
		{
			return Cmd(MSREQ_TUTORSTUDENT_SET,vrParameter);
		}*/
		
		public REQUESTCODE TutorStudentSet(TUTORSTUDENT  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_TUTORSTUDENT_SET,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_TUTORSTUDENT_SET,vrParameter);
			}
		}


		/*public REQUESTCODE TutorStudentCheck(CUniStruct<TUTORSTUDENTCHECK> vrParameter)
		{
			return Cmd(MSREQ_TUTORSTUDENT_CHECK,vrParameter);
		}*/
		
		public REQUESTCODE TutorStudentCheck(TUTORSTUDENTCHECK  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_TUTORSTUDENT_CHECK,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_TUTORSTUDENT_CHECK,vrParameter);
			}
		}


		/*public REQUESTCODE Del(CUniStruct<UNIACCOUNT> vrParameter)
		{
			return Cmd(MSREQ_ACC_DEL,vrParameter);
		}*/
		
		public REQUESTCODE Del(UNIACCOUNT  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ACC_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ACC_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE ExtIdentAccDel(CUniStruct<EXTIDENTACC> vrParameter)
		{
			return Cmd(MSREQ_EXTIDENTACC_DEL,vrParameter);
		}*/
		
		public REQUESTCODE ExtIdentAccDel(EXTIDENTACC  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_EXTIDENTACC_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_EXTIDENTACC_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE TutorStudentDel(CUniStruct<TUTORSTUDENT> vrParameter)
		{
			return Cmd(MSREQ_TUTORSTUDENT_DEL,vrParameter);
		}*/
		
		public REQUESTCODE TutorStudentDel(TUTORSTUDENT  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_TUTORSTUDENT_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_TUTORSTUDENT_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE AccCheck(CUniStruct<ACCCHECKREQ> vrParameter,out CUniStruct<UNIACCOUNT> vrResult)
		{
			return Cmd(MSREQ_ACC_CHECK,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE AccCheck(ACCCHECKREQ  vrParameter,out UNIACCOUNT  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ACC_CHECK,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ACC_CHECK,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE Deposit(CUniStruct<UNIDEPOSIT> vrParameter)
		{
			return Cmd(MSREQ_ACC_DEPOSIT,vrParameter);
		}*/
		
		public REQUESTCODE Deposit(UNIDEPOSIT  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ACC_DEPOSIT,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ACC_DEPOSIT,vrParameter);
			}
		}


		/*public REQUESTCODE Payment(CUniStruct<UNIPAYMENT> vrParameter,out CUniStruct<UNIPAYMENT> vrResult)
		{
			return Cmd(MSREQ_ACC_PAYMENT,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE Payment(UNIPAYMENT  vrParameter,out UNIPAYMENT  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ACC_PAYMENT,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ACC_PAYMENT,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE AccNoticeGet(CUniStruct<NOTICEREQ> vrParameter,out CUniStructArray<NOTICEINFO> vrResult)
		{
			return Cmd(MSREQ_ACC_NOTICEGET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE AccNoticeGet(NOTICEREQ  vrParameter,out NOTICEINFO[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ACC_NOTICEGET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ACC_NOTICEGET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE AccNoticeAffirm(CUniStruct<NOTICEAFFIRM> vrParameter,out CUniStruct<NOTICEAFFIRM> vrResult)
		{
			return Cmd(MSREQ_ACC_NOTICEAFFIRM,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE AccNoticeAffirm(NOTICEAFFIRM  vrParameter,out NOTICEAFFIRM  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ACC_NOTICEAFFIRM,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ACC_NOTICEAFFIRM,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE AccExtInfoGet(CUniStruct<ACCREQ> vrParameter,out CUniStructArray<UNIACCEXTINFO> vrResult)
		{
			return Cmd(MSREQ_EXTINFO_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE AccExtInfoGet(ACCREQ  vrParameter,out UNIACCEXTINFO[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_EXTINFO_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_EXTINFO_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE MajorGet(CUniStruct<MAJORREQ> vrParameter,out CUniStructArray<UNIMAJOR> vrResult)
		{
			return Cmd(MSREQ_MAJOR_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE MajorGet(MAJORREQ  vrParameter,out UNIMAJOR[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_MAJOR_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_MAJOR_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE MajorSet(CUniStruct<UNIMAJOR> vrParameter,out CUniStruct<UNIMAJOR> vrResult)
		{
			return Cmd(MSREQ_MAJOR_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE MajorSet(UNIMAJOR  vrParameter,out UNIMAJOR  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_MAJOR_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_MAJOR_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE MajorDel(CUniStruct<UNIMAJOR> vrParameter)
		{
			return Cmd(MSREQ_MAJOR_DEL,vrParameter);
		}*/
		
		public REQUESTCODE MajorDel(UNIMAJOR  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_MAJOR_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_MAJOR_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE TestDataGet(CUniStruct<TESTDATAREQ> vrParameter,out CUniStructArray<UNITESTDATA> vrResult)
		{
			return Cmd(MSREQ_TESTDATA_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE TestDataGet(TESTDATAREQ  vrParameter,out UNITESTDATA[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_TESTDATA_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_TESTDATA_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE TestDataUpload(CUniStruct<UNITESTDATA> vrParameter,out CUniStruct<UNITESTDATA> vrResult)
		{
			return Cmd(MSREQ_TESTDATA_UPLOAD,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE TestDataUpload(UNITESTDATA  vrParameter,out UNITESTDATA  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_TESTDATA_UPLOAD,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_TESTDATA_UPLOAD,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE TestDataChgStat(CUniStruct<UNITESTDATA> vrParameter)
		{
			return Cmd(MSREQ_TESTDATA_CHGSTAT,vrParameter);
		}*/
		
		public REQUESTCODE TestDataChgStat(UNITESTDATA  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_TESTDATA_CHGSTAT,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_TESTDATA_CHGSTAT,vrParameter);
			}
		}


		/*public REQUESTCODE TestDataAdminUpload(CUniStruct<ADMINTESTDATA> vrParameter,out CUniStruct<UNITESTDATA> vrResult)
		{
			return Cmd(MSREQ_TESTDATA_ADMINUPLOAD,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE TestDataAdminUpload(ADMINTESTDATA  vrParameter,out UNITESTDATA  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_TESTDATA_ADMINUPLOAD,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_TESTDATA_ADMINUPLOAD,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE CloudDiskOpen(CUniStruct<CLOUDDISKREQ> vrParameter,out CUniStructArray<CLOUDDISK> vrResult)
		{
			return Cmd(MSREQ_CLOUDDISK_OPEN,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE CloudDiskOpen(CLOUDDISKREQ  vrParameter,out CLOUDDISK[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CLOUDDISK_OPEN,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CLOUDDISK_OPEN,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE CloudDiskSave(CUniStruct<CLOUDDISK> vrParameter,out CUniStruct<CLOUDDISK> vrResult)
		{
			return Cmd(MSREQ_CLOUDDISK_SAVE,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE CloudDiskSave(CLOUDDISK  vrParameter,out CLOUDDISK  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CLOUDDISK_SAVE,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CLOUDDISK_SAVE,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE CloudDiskDel(CUniStruct<CLOUDDISK> vrParameter)
		{
			return Cmd(MSREQ_CLOUDDISK_DEL,vrParameter);
		}*/
		
		public REQUESTCODE CloudDiskDel(CLOUDDISK  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CLOUDDISK_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CLOUDDISK_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE CloudDiskStat(CUniStruct<CDISKSTATREQ> vrParameter,out CUniStruct<CDISKSTAT> vrResult)
		{
			return Cmd(MSREQ_CLOUDDISK_STAT,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE CloudDiskStat(CDISKSTATREQ  vrParameter,out CDISKSTAT  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CLOUDDISK_STAT,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CLOUDDISK_STAT,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE TeacherGet(CUniStruct<UNITEACHERREQ> vrParameter,out CUniStructArray<UNITEACHER> vrResult)
		{
			return Cmd(MSREQ_TEACHER_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE TeacherGet(UNITEACHERREQ  vrParameter,out UNITEACHER[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_TEACHER_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_TEACHER_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE TeacherSet(CUniStruct<UNITEACHER> vrParameter,out CUniStruct<UNITEACHER> vrResult)
		{
			return Cmd(MSREQ_TEACHER_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE TeacherSet(UNITEACHER  vrParameter,out UNITEACHER  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_TEACHER_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_TEACHER_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE TeacherDel(CUniStruct<UNITEACHER> vrParameter)
		{
			return Cmd(MSREQ_TEACHER_DEL,vrParameter);
		}*/
		
		public REQUESTCODE TeacherDel(UNITEACHER  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_TEACHER_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_TEACHER_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE GetUserCurInfo(CUniStruct<USERCURINFOREQ> vrParameter,out CUniStruct<USERCURINFO> vrResult)
		{
			return Cmd(MSREQ_USERCURINFO_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetUserCurInfo(USERCURINFOREQ  vrParameter,out USERCURINFO  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_USERCURINFO_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_USERCURINFO_GET,vrParameter,out vrResult);
			}
		}

	
	}
	#endregion PRAccount部分

	#region PRDevice部分
	/*设备管理*/
	public partial class PRDevice:PRModule
	{
		public PRDevice(UniDCom _UniDCom):base(_UniDCom)
		{
			m_nModule = DEVICE_BASE;
		}
		
		/*public REQUESTCODE LabGet(CUniStruct<LABREQ> vrParameter,out CUniStructArray<UNILAB> vrResult)
		{
			return Cmd(MSREQ_LAB_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE LabGet(LABREQ  vrParameter,out UNILAB[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_LAB_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_LAB_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE FullLabGet(CUniStruct<FULLLABREQ> vrParameter,out CUniStructArray<FULLLAB> vrResult)
		{
			return Cmd(MSREQ_FULLLAB_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE FullLabGet(FULLLABREQ  vrParameter,out FULLLAB[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_FULLLAB_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_FULLLAB_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE LabSet(CUniStruct<UNILAB> vrParameter,out CUniStruct<UNILAB> vrResult)
		{
			return Cmd(MSREQ_LAB_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE LabSet(UNILAB  vrParameter,out UNILAB  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_LAB_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_LAB_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE LabDel(CUniStruct<UNILAB> vrParameter)
		{
			return Cmd(MSREQ_LAB_DEL,vrParameter);
		}*/
		
		public REQUESTCODE LabDel(UNILAB  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_LAB_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_LAB_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE Get(CUniStruct<DEVREQ> vrParameter,out CUniStructArray<UNIDEVICE> vrResult)
		{
			return Cmd(MSREQ_DEVICE_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE Get(DEVREQ  vrParameter,out UNIDEVICE[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DEVICE_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DEVICE_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetDevForResv(CUniStruct<DEVFORRESVREQ> vrParameter,out CUniStructArray<DEVFORRESV> vrResult)
		{
			return Cmd(MSREQ_DEVFORRESV_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetDevForResv(DEVFORRESVREQ  vrParameter,out DEVFORRESV[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DEVFORRESV_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DEVFORRESV_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetDevKindForResv(CUniStruct<DEVKINDFORRESVREQ> vrParameter,out CUniStructArray<DEVKINDFORRESV> vrResult)
		{
			return Cmd(MSREQ_DEVKINDFORRESV_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetDevKindForResv(DEVKINDFORRESVREQ  vrParameter,out DEVKINDFORRESV[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DEVKINDFORRESV_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DEVKINDFORRESV_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE ResvUsableDevGet(CUniStruct<RESVUSABLEDEVREQ> vrParameter,out CUniStructArray<RESVUSABLEDEV> vrResult)
		{
			return Cmd(MSREQ_RESVUSABLEDEV_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE ResvUsableDevGet(RESVUSABLEDEVREQ  vrParameter,out RESVUSABLEDEV[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RESVUSABLEDEV_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RESVUSABLEDEV_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetDevResvStat(CUniStruct<DEVRESVSTATREQ> vrParameter,out CUniStructArray<DEVRESVSTAT> vrResult)
		{
			return Cmd(MSREQ_DEVRESVSTAT_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetDevResvStat(DEVRESVSTATREQ  vrParameter,out DEVRESVSTAT[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DEVRESVSTAT_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DEVRESVSTAT_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetLabResvStat(CUniStruct<LABRESVSTATREQ> vrParameter,out CUniStructArray<LABRESVSTAT> vrResult)
		{
			return Cmd(MSREQ_LABRESVSTAT_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetLabResvStat(LABRESVSTATREQ  vrParameter,out LABRESVSTAT[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_LABRESVSTAT_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_LABRESVSTAT_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetDevLongResvStat(CUniStruct<DEVLONGRESVSTATREQ> vrParameter,out CUniStructArray<DEVLONGRESVSTAT> vrResult)
		{
			return Cmd(MSREQ_DEVLONGRESVSTAT_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetDevLongResvStat(DEVLONGRESVSTATREQ  vrParameter,out DEVLONGRESVSTAT[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DEVLONGRESVSTAT_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DEVLONGRESVSTAT_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetDevKindForLongResv(CUniStruct<DEVKINDFORLONGRESVREQ> vrParameter,out CUniStructArray<DEVKINDFORRESV> vrResult)
		{
			return Cmd(MSREQ_DEVKINDFORLONGRESV_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetDevKindForLongResv(DEVKINDFORLONGRESVREQ  vrParameter,out DEVKINDFORRESV[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DEVKINDFORLONGRESV_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DEVKINDFORLONGRESV_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetRoomResvStat(CUniStruct<ROOMRESVSTATREQ> vrParameter,out CUniStructArray<ROOMRESVSTAT> vrResult)
		{
			return Cmd(MSREQ_ROOMRESVSTAT_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetRoomResvStat(ROOMRESVSTATREQ  vrParameter,out ROOMRESVSTAT[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ROOMRESVSTAT_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ROOMRESVSTAT_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DevFARGet(CUniStruct<DEVFARREQ> vrParameter,out CUniStructArray<DEVFAR> vrResult)
		{
			return Cmd(MSREQ_DEVALLOCATIONRATE_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE DevFARGet(DEVFARREQ  vrParameter,out DEVFAR[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DEVALLOCATIONRATE_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DEVALLOCATIONRATE_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DevCfgGet(CUniStruct<DEVCFGREQ> vrParameter,out CUniStructArray<DEVCFG> vrResult)
		{
			return Cmd(MSREQ_DEVCFG_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE DevCfgGet(DEVCFGREQ  vrParameter,out DEVCFG[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DEVCFG_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DEVCFG_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DevCfgKindGet(CUniStruct<DEVCFGKINDREQ> vrParameter,out CUniStructArray<DEVCFGKIND> vrResult)
		{
			return Cmd(MSREQ_DEVCFGKIND_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE DevCfgKindGet(DEVCFGKINDREQ  vrParameter,out DEVCFGKIND[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DEVCFGKIND_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DEVCFGKIND_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetRoomForResv(CUniStruct<ROOMFORRESVREQ> vrParameter,out CUniStructArray<ROOMFORRESV> vrResult)
		{
			return Cmd(MSREQ_ROOMFORRESV_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetRoomForResv(ROOMFORRESVREQ  vrParameter,out ROOMFORRESV[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ROOMFORRESV_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ROOMFORRESV_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetRGResvStat(CUniStruct<RGRESVSTATREQ> vrParameter,out CUniStructArray<RGRESVSTAT> vrResult)
		{
			return Cmd(MSREQ_RGRESVSTAT_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetRGResvStat(RGRESVSTATREQ  vrParameter,out RGRESVSTAT[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RGRESVSTAT_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RGRESVSTAT_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE Set(CUniStruct<UNIDEVICE> vrParameter,out CUniStruct<UNIDEVICE> vrResult)
		{
			return Cmd(MSREQ_DEVICE_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE Set(UNIDEVICE  vrParameter,out UNIDEVICE  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DEVICE_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DEVICE_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE AttendantSet(CUniStruct<DEVATTENDANT> vrParameter)
		{
			return Cmd(MSREQ_DEVATTENDANT_SET,vrParameter);
		}*/
		
		public REQUESTCODE AttendantSet(DEVATTENDANT  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DEVATTENDANT_SET,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DEVATTENDANT_SET,vrParameter);
			}
		}


		/*public REQUESTCODE DevFARSet(CUniStruct<DEVFAR> vrParameter)
		{
			return Cmd(MSREQ_DEVALLOCATIONRATE_SET,vrParameter);
		}*/
		
		public REQUESTCODE DevFARSet(DEVFAR  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DEVALLOCATIONRATE_SET,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DEVALLOCATIONRATE_SET,vrParameter);
			}
		}


		/*public REQUESTCODE DevCfgSet(CUniStruct<DEVCFG> vrParameter)
		{
			return Cmd(MSREQ_DEVCFG_SET,vrParameter);
		}*/
		
		public REQUESTCODE DevCfgSet(DEVCFG  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DEVCFG_SET,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DEVCFG_SET,vrParameter);
			}
		}


		/*public REQUESTCODE UploadSeatDetectStat(CUniStructArray<SEATDETECTSTAT> vrParameter)
		{
			return Cmd(MSREQ_SEATDETECTSTAT_UPLOAD,vrParameter);
		}*/
		
		public REQUESTCODE UploadSeatDetectStat(SEATDETECTSTAT[]  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_SEATDETECTSTAT_UPLOAD,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_SEATDETECTSTAT_UPLOAD,vrParameter);
			}
		}


		/*public REQUESTCODE Del(CUniStruct<UNIDEVICE> vrParameter)
		{
			return Cmd(MSREQ_DEVICE_DEL,vrParameter);
		}*/
		
		public REQUESTCODE Del(UNIDEVICE  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DEVICE_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DEVICE_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE DevCfgDel(CUniStruct<DEVCFG> vrParameter)
		{
			return Cmd(MSREQ_DEVCFG_DEL,vrParameter);
		}*/
		
		public REQUESTCODE DevCfgDel(DEVCFG  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DEVCFG_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DEVCFG_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE DevManUse(CUniStruct<DEVMANUSE> vrParameter)
		{
			return Cmd(MSREQ_DEVICE_MANUSE,vrParameter);
		}*/
		
		public REQUESTCODE DevManUse(DEVMANUSE  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DEVICE_MANUSE,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DEVICE_MANUSE,vrParameter);
			}
		}


		/*public REQUESTCODE DevRegist(CUniStruct<DEVREGISTREQ> vrParameter,out CUniStruct<DEVREGISTRES> vrResult)
		{
			return Cmd(CSREQ_DEVICE_REGIST,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE DevRegist(DEVREGISTREQ  vrParameter,out DEVREGISTRES  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(CSREQ_DEVICE_REGIST,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(CSREQ_DEVICE_REGIST,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DevLogon(CUniStruct<DEVLOGONREQ> vrParameter,out CUniStruct<DEVLOGONRES> vrResult)
		{
			return Cmd(CSREQ_DEVICE_LOGON,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE DevLogon(DEVLOGONREQ  vrParameter,out DEVLOGONRES  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(CSREQ_DEVICE_LOGON,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(CSREQ_DEVICE_LOGON,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DevQuery(CUniStruct<DEVQUERYREQ> vrParameter,out CUniStruct<UNIACCTINFO> vrResult)
		{
			return Cmd(CSREQ_DEVICE_QUERY,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE DevQuery(DEVQUERYREQ  vrParameter,out UNIACCTINFO  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(CSREQ_DEVICE_QUERY,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(CSREQ_DEVICE_QUERY,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DevLogout(CUniStruct<DEVLOGOUTREQ> vrParameter,out CUniStruct<DEVLOGOUTRES> vrResult)
		{
			return Cmd(CSREQ_DEVICE_LOGOUT,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE DevLogout(DEVLOGOUTREQ  vrParameter,out DEVLOGOUTRES  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(CSREQ_DEVICE_LOGOUT,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(CSREQ_DEVICE_LOGOUT,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DevHandShake(CUniStruct<DEVHANDSHAKEREQ> vrParameter,out CUniStruct<DEVHANDSHAKERES> vrResult)
		{
			return Cmd(CSREQ_DEVICE_HANDSHAKE,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE DevHandShake(DEVHANDSHAKEREQ  vrParameter,out DEVHANDSHAKERES  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(CSREQ_DEVICE_HANDSHAKE,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(CSREQ_DEVICE_HANDSHAKE,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE PCSWUpload(CUniStruct<PCPROGRAM> vrParameter)
		{
			return Cmd(CSREQ_PCSW_UPLOAD,vrParameter);
		}*/
		
		public REQUESTCODE PCSWUpload(PCPROGRAM  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(CSREQ_PCSW_UPLOAD,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(CSREQ_PCSW_UPLOAD,vrParameter);
			}
		}


		/*public REQUESTCODE CheckURL(CUniStruct<URLCHECKINFO> vrParameter)
		{
			return Cmd(CSREQ_URL_CHECK,vrParameter);
		}*/
		
		public REQUESTCODE CheckURL(URLCHECKINFO  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(CSREQ_URL_CHECK,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(CSREQ_URL_CHECK,vrParameter);
			}
		}


		/*public REQUESTCODE ClientChgPw(CUniStruct<CLTCHGPWINFO> vrParameter)
		{
			return Cmd(CSREQ_CLIENT_CHGPW,vrParameter);
		}*/
		
		public REQUESTCODE ClientChgPw(CLTCHGPWINFO  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(CSREQ_CLIENT_CHGPW,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(CSREQ_CLIENT_CHGPW,vrParameter);
			}
		}


		/*public REQUESTCODE ProgramBegin(CUniStruct<PCPROGRAM> vrParameter)
		{
			return Cmd(CSREQ_PROGRAM_BEGIN,vrParameter);
		}*/
		
		public REQUESTCODE ProgramBegin(PCPROGRAM  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(CSREQ_PROGRAM_BEGIN,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(CSREQ_PROGRAM_BEGIN,vrParameter);
			}
		}


		/*public REQUESTCODE ProgramEnd(CUniStructArray<PROGEND> vrParameter)
		{
			return Cmd(CSREQ_PROGRAM_END,vrParameter);
		}*/
		
		public REQUESTCODE ProgramEnd(PROGEND[]  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(CSREQ_PROGRAM_END,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(CSREQ_PROGRAM_END,vrParameter);
			}
		}


		/*public REQUESTCODE DevCtrl(CUniStruct<DEVCTRLINFO> vrParameter,out CUniStruct<DEVCTRLINFO> vrResult)
		{
			return Cmd(MSREQ_DEVICE_CTRL,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE DevCtrl(DEVCTRLINFO  vrParameter,out DEVCTRLINFO  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DEVICE_CTRL,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DEVICE_CTRL,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE URLCtrl(CUniStruct<CTRLREQ> vrParameter)
		{
			return Cmd(MSREQ_DEVICE_URLCTRL,vrParameter);
		}*/
		
		public REQUESTCODE URLCtrl(CTRLREQ  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DEVICE_URLCTRL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DEVICE_URLCTRL,vrParameter);
			}
		}


		/*public REQUESTCODE SWCtrl(CUniStruct<CTRLREQ> vrParameter)
		{
			return Cmd(MSREQ_DEVICE_SWCTRL,vrParameter);
		}*/
		
		public REQUESTCODE SWCtrl(CTRLREQ  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DEVICE_SWCTRL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DEVICE_SWCTRL,vrParameter);
			}
		}


		/*public REQUESTCODE GetRunApp(CUniStruct<RUNAPPREQ> vrParameter,out CUniStructArray<RUNAPP> vrResult)
		{
			return Cmd(MSREQ_RUNAPP_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetRunApp(RUNAPPREQ  vrParameter,out RUNAPP[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RUNAPP_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RUNAPP_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE PCSWUploadEnd(CUniStruct<SWUPLOADEND> vrParameter)
		{
			return Cmd(CSREQ_PCSW_UPLOAD_END,vrParameter);
		}*/
		
		public REQUESTCODE PCSWUploadEnd(SWUPLOADEND  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(CSREQ_PCSW_UPLOAD_END,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(CSREQ_PCSW_UPLOAD_END,vrParameter);
			}
		}


		/*public REQUESTCODE DevLoan(CUniStruct<DEVLOANREQ> vrParameter)
		{
			return Cmd(MSREQ_DEVICE_LOAN,vrParameter);
		}*/
		
		public REQUESTCODE DevLoan(DEVLOANREQ  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DEVICE_LOAN,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DEVICE_LOAN,vrParameter);
			}
		}


		/*public REQUESTCODE DevReturn(CUniStruct<DEVRETURNREQ> vrParameter)
		{
			return Cmd(MSREQ_DEVICE_RETURN,vrParameter);
		}*/
		
		public REQUESTCODE DevReturn(DEVRETURNREQ  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DEVICE_RETURN,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DEVICE_RETURN,vrParameter);
			}
		}


		/*public REQUESTCODE DevDamageRecGet(CUniStruct<DEVDAMAGERECREQ> vrParameter,out CUniStructArray<DEVDAMAGEREC> vrResult)
		{
			return Cmd(MSREQ_DEVDAMAGEREC_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE DevDamageRecGet(DEVDAMAGERECREQ  vrParameter,out DEVDAMAGEREC[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DEVDAMAGEREC_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DEVDAMAGEREC_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DeviceRepair(CUniStruct<DEVDAMAGEREC> vrParameter,out CUniStruct<DEVDAMAGEREC> vrResult)
		{
			return Cmd(MSREQ_DEVICE_REPAIR,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE DeviceRepair(DEVDAMAGEREC  vrParameter,out DEVDAMAGEREC  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DEVICE_REPAIR,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DEVICE_REPAIR,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DevCtrlRes(CUniStruct<DEVCTRLINFO> vrParameter)
		{
			return Cmd(CSREQ_DEVICE_CTRLRES,vrParameter);
		}*/
		
		public REQUESTCODE DevCtrlRes(DEVCTRLINFO  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(CSREQ_DEVICE_CTRLRES,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(CSREQ_DEVICE_CTRLRES,vrParameter);
			}
		}


		/*public REQUESTCODE DevClsGet(CUniStruct<DEVCLSREQ> vrParameter,out CUniStructArray<UNIDEVCLS> vrResult)
		{
			return Cmd(MSREQ_DEVCLS_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE DevClsGet(DEVCLSREQ  vrParameter,out UNIDEVCLS[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DEVCLS_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DEVCLS_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DevClsSet(CUniStruct<UNIDEVCLS> vrParameter,out CUniStruct<UNIDEVCLS> vrResult)
		{
			return Cmd(MSREQ_DEVCLS_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE DevClsSet(UNIDEVCLS  vrParameter,out UNIDEVCLS  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DEVCLS_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DEVCLS_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DevClsDel(CUniStruct<UNIDEVCLS> vrParameter)
		{
			return Cmd(MSREQ_DEVCLS_DEL,vrParameter);
		}*/
		
		public REQUESTCODE DevClsDel(UNIDEVCLS  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DEVCLS_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DEVCLS_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE DevKindGet(CUniStruct<DEVKINDREQ> vrParameter,out CUniStructArray<UNIDEVKIND> vrResult)
		{
			return Cmd(MSREQ_DEVKIND_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE DevKindGet(DEVKINDREQ  vrParameter,out UNIDEVKIND[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DEVKIND_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DEVKIND_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DevKindSet(CUniStruct<UNIDEVKIND> vrParameter,out CUniStruct<UNIDEVKIND> vrResult)
		{
			return Cmd(MSREQ_DEVKIND_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE DevKindSet(UNIDEVKIND  vrParameter,out UNIDEVKIND  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DEVKIND_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DEVKIND_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DevKindDel(CUniStruct<UNIDEVKIND> vrParameter)
		{
			return Cmd(MSREQ_DEVKIND_DEL,vrParameter);
		}*/
		
		public REQUESTCODE DevKindDel(UNIDEVKIND  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DEVKIND_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DEVKIND_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE BuildingGet(CUniStruct<BUILDINGREQ> vrParameter,out CUniStructArray<UNIBUILDING> vrResult)
		{
			return Cmd(MSREQ_BUILDING_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE BuildingGet(BUILDINGREQ  vrParameter,out UNIBUILDING[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_BUILDING_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_BUILDING_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE BuildingSet(CUniStruct<UNIBUILDING> vrParameter,out CUniStruct<UNIBUILDING> vrResult)
		{
			return Cmd(MSREQ_BUILDING_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE BuildingSet(UNIBUILDING  vrParameter,out UNIBUILDING  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_BUILDING_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_BUILDING_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE BuildingDel(CUniStruct<UNIBUILDING> vrParameter)
		{
			return Cmd(MSREQ_BUILDING_DEL,vrParameter);
		}*/
		
		public REQUESTCODE BuildingDel(UNIBUILDING  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_BUILDING_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_BUILDING_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE RoomGet(CUniStruct<ROOMREQ> vrParameter,out CUniStructArray<UNIROOM> vrResult)
		{
			return Cmd(MSREQ_ROOM_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE RoomGet(ROOMREQ  vrParameter,out UNIROOM[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ROOM_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ROOM_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE RoomSet(CUniStruct<UNIROOM> vrParameter,out CUniStruct<UNIROOM> vrResult)
		{
			return Cmd(MSREQ_ROOM_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE RoomSet(UNIROOM  vrParameter,out UNIROOM  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ROOM_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ROOM_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE RoomDel(CUniStruct<UNIROOM> vrParameter)
		{
			return Cmd(MSREQ_ROOM_DEL,vrParameter);
		}*/
		
		public REQUESTCODE RoomDel(UNIROOM  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ROOM_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ROOM_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE RoomCtrl(CUniStruct<ROOMCTRLINFO> vrParameter,out CUniStruct<ROOMCTRLINFO> vrResult)
		{
			return Cmd(MSREQ_ROOM_CTRL,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE RoomCtrl(ROOMCTRLINFO  vrParameter,out ROOMCTRLINFO  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ROOM_CTRL,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ROOM_CTRL,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetPermitRoom(CUniStruct<PERMITROOMREQ> vrParameter,out CUniStructArray<PERMITROOMINFO> vrResult)
		{
			return Cmd(MSREQ_ROOM_PERMITROOM,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetPermitRoom(PERMITROOMREQ  vrParameter,out PERMITROOMINFO[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ROOM_PERMITROOM,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ROOM_PERMITROOM,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetRoomCtrlInfo(CUniStruct<ROOMCTRLREQ> vrParameter,out CUniStructArray<UNIDOORCTRL> vrResult)
		{
			return Cmd(MSREQ_ROOM_CTRLINFO,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetRoomCtrlInfo(ROOMCTRLREQ  vrParameter,out UNIDOORCTRL[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ROOM_CTRLINFO,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ROOM_CTRLINFO,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE FullRoomGet(CUniStruct<FULLROOMREQ> vrParameter,out CUniStructArray<FULLROOM> vrResult)
		{
			return Cmd(MSREQ_FULLROOM_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE FullRoomGet(FULLROOMREQ  vrParameter,out FULLROOM[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_FULLROOM_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_FULLROOM_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE BasicRoomGet(CUniStruct<BASICROOMREQ> vrParameter,out CUniStructArray<BASICROOM> vrResult)
		{
			return Cmd(MSREQ_BASICROOM_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE BasicRoomGet(BASICROOMREQ  vrParameter,out BASICROOM[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_BASICROOM_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_BASICROOM_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE ChannelGateGet(CUniStruct<CHANNELGATEREQ> vrParameter,out CUniStructArray<UNICHANNELGATE> vrResult)
		{
			return Cmd(MSREQ_CHANNELGATE_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE ChannelGateGet(CHANNELGATEREQ  vrParameter,out UNICHANNELGATE[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CHANNELGATE_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CHANNELGATE_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE ChannelGateSet(CUniStruct<UNICHANNELGATE> vrParameter,out CUniStruct<UNICHANNELGATE> vrResult)
		{
			return Cmd(MSREQ_CHANNELGATE_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE ChannelGateSet(UNICHANNELGATE  vrParameter,out UNICHANNELGATE  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CHANNELGATE_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CHANNELGATE_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE ChannelGateDel(CUniStruct<UNICHANNELGATE> vrParameter)
		{
			return Cmd(MSREQ_CHANNELGATE_DEL,vrParameter);
		}*/
		
		public REQUESTCODE ChannelGateDel(UNICHANNELGATE  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CHANNELGATE_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CHANNELGATE_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE ChannelGateCtrl(CUniStruct<CHANNELGATECTRLINFO> vrParameter,out CUniStruct<CHANNELGATECTRLINFO> vrResult)
		{
			return Cmd(MSREQ_CHANNELGATE_CTRL,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE ChannelGateCtrl(CHANNELGATECTRLINFO  vrParameter,out CHANNELGATECTRLINFO  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CHANNELGATE_CTRL,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CHANNELGATE_CTRL,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE RoomGroupGet(CUniStruct<ROOMGROUPREQ> vrParameter,out CUniStructArray<ROOMGROUP> vrResult)
		{
			return Cmd(MSREQ_ROOMGROUP_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE RoomGroupGet(ROOMGROUPREQ  vrParameter,out ROOMGROUP[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ROOMGROUP_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ROOMGROUP_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE RoomGroupSet(CUniStruct<ROOMGROUP> vrParameter,out CUniStruct<ROOMGROUP> vrResult)
		{
			return Cmd(MSREQ_ROOMGROUP_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE RoomGroupSet(ROOMGROUP  vrParameter,out ROOMGROUP  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ROOMGROUP_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ROOMGROUP_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE RoomGroupDel(CUniStruct<ROOMGROUP> vrParameter)
		{
			return Cmd(MSREQ_ROOMGROUP_DEL,vrParameter);
		}*/
		
		public REQUESTCODE RoomGroupDel(ROOMGROUP  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ROOMGROUP_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ROOMGROUP_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE DevMonitorGet(CUniStruct<DEVMONITORREQ> vrParameter,out CUniStructArray<DEVMONITOR> vrResult)
		{
			return Cmd(MSREQ_DEVMONITOR_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE DevMonitorGet(DEVMONITORREQ  vrParameter,out DEVMONITOR[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DEVMONITOR_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DEVMONITOR_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DevMonitorSet(CUniStruct<DEVMONITOR> vrParameter,out CUniStruct<DEVMONITOR> vrResult)
		{
			return Cmd(MSREQ_DEVMONITOR_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE DevMonitorSet(DEVMONITOR  vrParameter,out DEVMONITOR  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DEVMONITOR_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DEVMONITOR_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DevMonitorDel(CUniStruct<DEVMONITOR> vrParameter)
		{
			return Cmd(MSREQ_DEVMONITOR_DEL,vrParameter);
		}*/
		
		public REQUESTCODE DevMonitorDel(DEVMONITOR  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DEVMONITOR_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DEVMONITOR_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE MonDevGet(CUniStruct<MONDEVREQ> vrParameter,out CUniStructArray<MONDEV> vrResult)
		{
			return Cmd(MSREQ_MONDEV_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE MonDevGet(MONDEVREQ  vrParameter,out MONDEV[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_MONDEV_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_MONDEV_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE MonDevSet(CUniStruct<MONDEV> vrParameter,out CUniStruct<MONDEV> vrResult)
		{
			return Cmd(MSREQ_MONDEV_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE MonDevSet(MONDEV  vrParameter,out MONDEV  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_MONDEV_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_MONDEV_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE MonDevDel(CUniStruct<MONDEV> vrParameter)
		{
			return Cmd(MSREQ_MONDEV_DEL,vrParameter);
		}*/
		
		public REQUESTCODE MonDevDel(MONDEV  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_MONDEV_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_MONDEV_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE DevOpenRuleGet(CUniStruct<DEVOPENRULEREQ> vrParameter,out CUniStructArray<DEVOPENRULE> vrResult)
		{
			return Cmd(MSREQ_DEVOPENRULE_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE DevOpenRuleGet(DEVOPENRULEREQ  vrParameter,out DEVOPENRULE[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DEVOPENRULE_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DEVOPENRULE_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DevOpenRuleSet(CUniStruct<DEVOPENRULE> vrParameter,out CUniStruct<DEVOPENRULE> vrResult)
		{
			return Cmd(MSREQ_DEVOPENRULE_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE DevOpenRuleSet(DEVOPENRULE  vrParameter,out DEVOPENRULE  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DEVOPENRULE_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DEVOPENRULE_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DevOpenRuleDel(CUniStruct<DEVOPENRULE> vrParameter)
		{
			return Cmd(MSREQ_DEVOPENRULE_DEL,vrParameter);
		}*/
		
		public REQUESTCODE DevOpenRuleDel(DEVOPENRULE  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DEVOPENRULE_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DEVOPENRULE_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE GroupOpenRuleSet(CUniStruct<CHANGEGROUPOPENRULE> vrParameter,out CUniStruct<CHANGEGROUPOPENRULE> vrResult)
		{
			return Cmd(MSREQ_GROUPOPENRULE_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GroupOpenRuleSet(CHANGEGROUPOPENRULE  vrParameter,out CHANGEGROUPOPENRULE  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_GROUPOPENRULE_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_GROUPOPENRULE_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GroupOpenRuleDel(CUniStruct<CHANGEGROUPOPENRULE> vrParameter)
		{
			return Cmd(MSREQ_GROUPOPENRULE_DEL,vrParameter);
		}*/
		
		public REQUESTCODE GroupOpenRuleDel(CHANGEGROUPOPENRULE  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_GROUPOPENRULE_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_GROUPOPENRULE_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE GroupOpenRuleGet(CUniStruct<GROUPOPENRULEREQ> vrParameter,out CUniStructArray<GROUPOPENRULE> vrResult)
		{
			return Cmd(MSREQ_GROUPOPENRULE_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GroupOpenRuleGet(GROUPOPENRULEREQ  vrParameter,out GROUPOPENRULE[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_GROUPOPENRULE_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_GROUPOPENRULE_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE PeriodOpenRuleSet(CUniStruct<CHANGEPERIODOPENRULE> vrParameter,out CUniStruct<CHANGEPERIODOPENRULE> vrResult)
		{
			return Cmd(MSREQ_PERIODOPENRULE_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE PeriodOpenRuleSet(CHANGEPERIODOPENRULE  vrParameter,out CHANGEPERIODOPENRULE  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_PERIODOPENRULE_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_PERIODOPENRULE_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE PeriodOpenRuleDel(CUniStruct<CHANGEPERIODOPENRULE> vrParameter)
		{
			return Cmd(MSREQ_PERIODOPENRULE_DEL,vrParameter);
		}*/
		
		public REQUESTCODE PeriodOpenRuleDel(CHANGEPERIODOPENRULE  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_PERIODOPENRULE_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_PERIODOPENRULE_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE PeriodOpenRuleGet(CUniStruct<PERIODOPENRULEREQ> vrParameter,out CUniStructArray<PERIODOPENRULE> vrResult)
		{
			return Cmd(MSREQ_PERIODOPENRULE_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE PeriodOpenRuleGet(PERIODOPENRULEREQ  vrParameter,out PERIODOPENRULE[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_PERIODOPENRULE_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_PERIODOPENRULE_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE CurDevStat(out CUniStruct<CURDEVSTAT> vrResult)
		{
			return Cmd(MSREQ_CURDEV_STAT,out vrResult);
		}*/
		
		public REQUESTCODE CurDevStat(out CURDEVSTAT  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CURDEV_STAT,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CURDEV_STAT,out vrResult);
			}
		}


		/*public REQUESTCODE DevForTeachingStat(CUniStruct<DEVFORTREQ> vrParameter,out CUniStructArray<DEVSECINFO> vrResult)
		{
			return Cmd(MSREQ_DEVFORT_STAT,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE DevForTeachingStat(DEVFORTREQ  vrParameter,out DEVSECINFO[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DEVFORT_STAT,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DEVFORT_STAT,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE TeachingDevGet(CUniStruct<TEACHINGDEVREQ> vrParameter,out CUniStructArray<TEACHINGDEV> vrResult)
		{
			return Cmd(MSREQ_TEACHINGDEV_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE TeachingDevGet(TEACHINGDEVREQ  vrParameter,out TEACHINGDEV[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_TEACHINGDEV_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_TEACHINGDEV_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE RewardRecGet(CUniStruct<REWARDRECREQ> vrParameter,out CUniStructArray<REWARDREC> vrResult)
		{
			return Cmd(MSREQ_REWARDREC_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE RewardRecGet(REWARDRECREQ  vrParameter,out REWARDREC[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REWARDREC_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REWARDREC_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE RewardRecSet(CUniStruct<REWARDREC> vrParameter,out CUniStruct<REWARDREC> vrResult)
		{
			return Cmd(MSREQ_REWARDREC_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE RewardRecSet(REWARDREC  vrParameter,out REWARDREC  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REWARDREC_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REWARDREC_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE RewardRecDel(CUniStruct<REWARDREC> vrParameter)
		{
			return Cmd(MSREQ_REWARDREC_DEL,vrParameter);
		}*/
		
		public REQUESTCODE RewardRecDel(REWARDREC  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REWARDREC_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REWARDREC_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE CostRecGet(CUniStruct<COSTRECREQ> vrParameter,out CUniStructArray<COSTREC> vrResult)
		{
			return Cmd(MSREQ_COSTREC_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE CostRecGet(COSTRECREQ  vrParameter,out COSTREC[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_COSTREC_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_COSTREC_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE CostRecSet(CUniStruct<COSTREC> vrParameter,out CUniStruct<COSTREC> vrResult)
		{
			return Cmd(MSREQ_COSTREC_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE CostRecSet(COSTREC  vrParameter,out COSTREC  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_COSTREC_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_COSTREC_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE CostRecDel(CUniStruct<COSTREC> vrParameter)
		{
			return Cmd(MSREQ_COSTREC_DEL,vrParameter);
		}*/
		
		public REQUESTCODE CostRecDel(COSTREC  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_COSTREC_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_COSTREC_DEL,vrParameter);
			}
		}

	
	}
	#endregion PRDevice部分

	#region PRDoorCtrlSrv部分
	/*门禁设置管理*/
	public partial class PRDoorCtrlSrv:PRModule
	{
		public PRDoorCtrlSrv(UniDCom _UniDCom):base(_UniDCom)
		{
			m_nModule = DOORCTRLSRV_BASE;
		}
		
		/*public REQUESTCODE GetDCS(CUniStruct<DCSREQ> vrParameter,out CUniStructArray<UNIDCS> vrResult)
		{
			return Cmd(MSREQ_DCS_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetDCS(DCSREQ  vrParameter,out UNIDCS[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DCS_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DCS_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE SetDCS(CUniStruct<UNIDCS> vrParameter,out CUniStruct<UNIDCS> vrResult)
		{
			return Cmd(MSREQ_DCS_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE SetDCS(UNIDCS  vrParameter,out UNIDCS  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DCS_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DCS_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DelDCS(CUniStruct<UNIDCS> vrParameter)
		{
			return Cmd(MSREQ_DCS_DEL,vrParameter);
		}*/
		
		public REQUESTCODE DelDCS(UNIDCS  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DCS_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DCS_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE Login(CUniStruct<DCSLOGINREQ> vrParameter,out CUniStruct<DCSLOGINRES> vrResult)
		{
			return Cmd(MSREQ_DCS_LOGIN,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE Login(DCSLOGINREQ  vrParameter,out DCSLOGINRES  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DCS_LOGIN,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DCS_LOGIN,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE Logout(CUniStruct<DCSLOGOUTREQ> vrParameter)
		{
			return Cmd(MSREQ_DCS_LOGOUT,vrParameter);
		}*/
		
		public REQUESTCODE Logout(DCSLOGOUTREQ  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DCS_LOGOUT,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DCS_LOGOUT,vrParameter);
			}
		}


		/*public REQUESTCODE Pulse(CUniStruct<DCSPULSEREQ> vrParameter,out CUniStruct<DCSPULSERES> vrResult)
		{
			return Cmd(MSREQ_DCS_PULSE,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE Pulse(DCSPULSEREQ  vrParameter,out DCSPULSERES  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DCS_PULSE,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DCS_PULSE,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DoorCard(CUniStruct<DOORCARDREQ> vrParameter,out CUniStruct<DOORCARDRES> vrResult)
		{
			return Cmd(MSREQ_DCS_DOORCARD,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE DoorCard(DOORCARDREQ  vrParameter,out DOORCARDRES  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DCS_DOORCARD,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DCS_DOORCARD,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE MobilOpenDoor(CUniStruct<MOBILEOPENDOORREQ> vrParameter,out CUniStruct<MOBILEOPENDOORRES> vrResult)
		{
			return Cmd(MSREQ_MOBILE_OPENDOOR,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE MobilOpenDoor(MOBILEOPENDOORREQ  vrParameter,out MOBILEOPENDOORRES  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_MOBILE_OPENDOOR,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_MOBILE_OPENDOOR,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetDoorCtrl(CUniStruct<DOORCTRLREQ> vrParameter,out CUniStructArray<UNIDOORCTRL> vrResult)
		{
			return Cmd(MSREQ_DOORCTRL_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetDoorCtrl(DOORCTRLREQ  vrParameter,out UNIDOORCTRL[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DOORCTRL_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DOORCTRL_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE SetDoorCtrl(CUniStruct<UNIDOORCTRL> vrParameter,out CUniStruct<UNIDOORCTRL> vrResult)
		{
			return Cmd(MSREQ_DOORCTRL_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE SetDoorCtrl(UNIDOORCTRL  vrParameter,out UNIDOORCTRL  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DOORCTRL_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DOORCTRL_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DelDoorCtrl(CUniStruct<UNIDOORCTRL> vrParameter)
		{
			return Cmd(MSREQ_DOORCTRL_DEL,vrParameter);
		}*/
		
		public REQUESTCODE DelDoorCtrl(UNIDOORCTRL  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DOORCTRL_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DOORCTRL_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE DoorCtrlCmd(CUniStruct<DOORCTRLCMD> vrParameter)
		{
			return Cmd(MSREQ_DOORCTRL_CMD,vrParameter);
		}*/
		
		public REQUESTCODE DoorCtrlCmd(DOORCTRLCMD  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DOORCTRL_CMD,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DOORCTRL_CMD,vrParameter);
			}
		}

	
	}
	#endregion PRDoorCtrlSrv部分

	#region PRGroup部分
	/*用户组管理*/
	public partial class PRGroup:PRModule
	{
		public PRGroup(UniDCom _UniDCom):base(_UniDCom)
		{
			m_nModule = GROUP_BASE;
		}
		
		/*public REQUESTCODE GetGroup(CUniStruct<GROUPREQ> vrParameter,out CUniStructArray<UNIGROUP> vrResult)
		{
			return Cmd(MSREQ_GROUP_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetGroup(GROUPREQ  vrParameter,out UNIGROUP[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_GROUP_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_GROUP_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE SetGroup(CUniStruct<UNIGROUP> vrParameter,out CUniStruct<UNIGROUP> vrResult)
		{
			return Cmd(MSREQ_GROUP_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE SetGroup(UNIGROUP  vrParameter,out UNIGROUP  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_GROUP_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_GROUP_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DelGroup(CUniStruct<UNIGROUP> vrParameter)
		{
			return Cmd(MSREQ_GROUP_DEL,vrParameter);
		}*/
		
		public REQUESTCODE DelGroup(UNIGROUP  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_GROUP_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_GROUP_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE SetGroupMember(CUniStruct<GROUPMEMBER> vrParameter)
		{
			return Cmd(MSREQ_GROUPMEMBER_SET,vrParameter);
		}*/
		
		public REQUESTCODE SetGroupMember(GROUPMEMBER  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_GROUPMEMBER_SET,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_GROUPMEMBER_SET,vrParameter);
			}
		}


		/*public REQUESTCODE DelGroupMember(CUniStruct<GROUPMEMBER> vrParameter)
		{
			return Cmd(MSREQ_GROUPMEMBER_DEL,vrParameter);
		}*/
		
		public REQUESTCODE DelGroupMember(GROUPMEMBER  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_GROUPMEMBER_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_GROUPMEMBER_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE GetGroupMemDetail(CUniStruct<GROUPMEMDETAILREQ> vrParameter,out CUniStructArray<GROUPMEMDETAIL> vrResult)
		{
			return Cmd(MSREQ_GROUPMEMDETAIL_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetGroupMemDetail(GROUPMEMDETAILREQ  vrParameter,out GROUPMEMDETAIL[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_GROUPMEMDETAIL_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_GROUPMEMDETAIL_GET,vrParameter,out vrResult);
			}
		}

	
	}
	#endregion PRGroup部分

	#region PRReserve部分
	/*预约管理*/
	public partial class PRReserve:PRModule
	{
		public PRReserve(UniDCom _UniDCom):base(_UniDCom)
		{
			m_nModule = RESERVE_BASE;
		}
		
		/*public REQUESTCODE Get(CUniStruct<RESVREQ> vrParameter,out CUniStructArray<UNIRESERVE> vrResult)
		{
			return Cmd(MSREQ_RESERVE_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE Get(RESVREQ  vrParameter,out UNIRESERVE[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RESERVE_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RESERVE_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetReserveForShow(CUniStruct<RESVSHOWREQ> vrParameter,out CUniStructArray<RESVSHOW> vrResult)
		{
			return Cmd(MSREQ_RESERVE_GETSHOW,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetReserveForShow(RESVSHOWREQ  vrParameter,out RESVSHOW[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RESERVE_GETSHOW,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RESERVE_GETSHOW,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetTeachingResv(CUniStruct<TEACHINGRESVREQ> vrParameter,out CUniStructArray<TEACHINGRESV> vrResult)
		{
			return Cmd(MSREQ_TEACHINGRESV_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetTeachingResv(TEACHINGRESVREQ  vrParameter,out TEACHINGRESV[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_TEACHINGRESV_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_TEACHINGRESV_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetRTResv(CUniStruct<RTRESVREQ> vrParameter,out CUniStructArray<RTRESV> vrResult)
		{
			return Cmd(MSREQ_RTRESV_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetRTResv(RTRESVREQ  vrParameter,out RTRESV[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RTRESV_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RTRESV_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetRTResvBill(CUniStruct<RTRESVBILLREQ> vrParameter,out CUniStruct<RTRESVBILL> vrResult)
		{
			return Cmd(MSREQ_RTBILL_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetRTResvBill(RTRESVBILLREQ  vrParameter,out RTRESVBILL  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RTBILL_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RTBILL_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetResvTime(CUniStruct<RESVTIMEREQ> vrParameter,out CUniStructArray<RESVTIME> vrResult)
		{
			return Cmd(MSREQ_RESVTIME_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetResvTime(RESVTIMEREQ  vrParameter,out RESVTIME[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RESVTIME_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RESVTIME_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE Set(CUniStruct<UNIRESERVE> vrParameter,out CUniStruct<UNIRESERVE> vrResult)
		{
			return Cmd(MSREQ_RESERVE_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE Set(UNIRESERVE  vrParameter,out UNIRESERVE  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RESERVE_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RESERVE_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE Auto(CUniStruct<AUTORESVREQ> vrParameter,out CUniStruct<UNIRESERVE> vrResult)
		{
			return Cmd(MSREQ_RESERVE_AUTO,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE Auto(AUTORESVREQ  vrParameter,out UNIRESERVE  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RESERVE_AUTO,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RESERVE_AUTO,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE HolidayShift(CUniStruct<HOLIDAYSHIFT> vrParameter)
		{
			return Cmd(MSREQ_RESERVE_HOLIDAYSHIFT,vrParameter);
		}*/
		
		public REQUESTCODE HolidayShift(HOLIDAYSHIFT  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RESERVE_HOLIDAYSHIFT,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RESERVE_HOLIDAYSHIFT,vrParameter);
			}
		}


		/*public REQUESTCODE JoinToResvMember(CUniStruct<RESVMEMBER> vrParameter)
		{
			return Cmd(MSREQ_RESVMEMBER_JOIN,vrParameter);
		}*/
		
		public REQUESTCODE JoinToResvMember(RESVMEMBER  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RESVMEMBER_JOIN,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RESVMEMBER_JOIN,vrParameter);
			}
		}


		/*public REQUESTCODE ExitFromResvMember(CUniStruct<RESVMEMBER> vrParameter)
		{
			return Cmd(MSREQ_RESVMEMBER_EXIT,vrParameter);
		}*/
		
		public REQUESTCODE ExitFromResvMember(RESVMEMBER  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RESVMEMBER_EXIT,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RESVMEMBER_EXIT,vrParameter);
			}
		}


		/*public REQUESTCODE CancelResvSign(CUniStruct<UNIRESERVE> vrParameter)
		{
			return Cmd(MSREQ_RESERVE_CANCELSIGN,vrParameter);
		}*/
		
		public REQUESTCODE CancelResvSign(UNIRESERVE  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RESERVE_CANCELSIGN,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RESERVE_CANCELSIGN,vrParameter);
			}
		}


		/*public REQUESTCODE SetRTResv(CUniStruct<RTRESV> vrParameter,out CUniStruct<RTRESV> vrResult)
		{
			return Cmd(MSREQ_RTRESV_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE SetRTResv(RTRESV  vrParameter,out RTRESV  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RTRESV_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RTRESV_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE RTResvCheck(CUniStruct<RTRESVCHECK> vrParameter)
		{
			return Cmd(MSREQ_RTRESV_CHECK,vrParameter);
		}*/
		
		public REQUESTCODE RTResvCheck(RTRESVCHECK  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RTRESV_CHECK,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RTRESV_CHECK,vrParameter);
			}
		}


		/*public REQUESTCODE PrepayRTResv(CUniStruct<RTPREPAY> vrParameter)
		{
			return Cmd(MSREQ_RTRESV_PREPAY,vrParameter);
		}*/
		
		public REQUESTCODE PrepayRTResv(RTPREPAY  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RTRESV_PREPAY,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RTRESV_PREPAY,vrParameter);
			}
		}


		/*public REQUESTCODE RTBillCheck(CUniStruct<RTBILLCHECK> vrParameter)
		{
			return Cmd(MSREQ_RTBILL_CHECK,vrParameter);
		}*/
		
		public REQUESTCODE RTBillCheck(RTBILLCHECK  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RTBILL_CHECK,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RTBILL_CHECK,vrParameter);
			}
		}


		/*public REQUESTCODE RTBillSettle(CUniStruct<RTBILLSETTLE> vrParameter)
		{
			return Cmd(MSREQ_RTBILL_SETTLE,vrParameter);
		}*/
		
		public REQUESTCODE RTBillSettle(RTBILLSETTLE  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RTBILL_SETTLE,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RTBILL_SETTLE,vrParameter);
			}
		}


		/*public REQUESTCODE RTBillReceive(CUniStruct<RTBILLRECEIVE> vrParameter)
		{
			return Cmd(MSREQ_RTBILL_RECEIVE,vrParameter);
		}*/
		
		public REQUESTCODE RTBillReceive(RTBILLRECEIVE  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RTBILL_RECEIVE,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RTBILL_RECEIVE,vrParameter);
			}
		}


		/*public REQUESTCODE AnonymousResvSet(CUniStruct<ANONYMOUSRESV> vrParameter,out CUniStruct<ANONYMOUSRESV> vrResult)
		{
			return Cmd(MSREQ_ANONYMOUSRESV_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE AnonymousResvSet(ANONYMOUSRESV  vrParameter,out ANONYMOUSRESV  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ANONYMOUSRESV_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ANONYMOUSRESV_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE AllUserResvSet(CUniStruct<ALLUSERRESV> vrParameter,out CUniStruct<ALLUSERRESV> vrResult)
		{
			return Cmd(MSREQ_ALLUSERRESV_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE AllUserResvSet(ALLUSERRESV  vrParameter,out ALLUSERRESV  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ALLUSERRESV_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ALLUSERRESV_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE Del(CUniStruct<UNIRESERVE> vrParameter)
		{
			return Cmd(MSREQ_RESERVE_DEL,vrParameter);
		}*/
		
		public REQUESTCODE Del(UNIRESERVE  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RESERVE_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RESERVE_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE DelRTResv(CUniStruct<RTRESV> vrParameter)
		{
			return Cmd(MSREQ_RTRESV_DEL,vrParameter);
		}*/
		
		public REQUESTCODE DelRTResv(RTRESV  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RTRESV_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RTRESV_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE ResvEarlyEnd(CUniStruct<UNIRESERVE> vrParameter)
		{
			return Cmd(MSREQ_RESERVE_EARLYEND,vrParameter);
		}*/
		
		public REQUESTCODE ResvEarlyEnd(UNIRESERVE  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RESERVE_EARLYEND,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RESERVE_EARLYEND,vrParameter);
			}
		}


		/*public REQUESTCODE ResvChgEndTime(CUniStruct<RESVENDTIME> vrParameter,out CUniStruct<RESVENDTIME> vrResult)
		{
			return Cmd(MSREQ_RESERVE_CHGENDTIME,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE ResvChgEndTime(RESVENDTIME  vrParameter,out RESVENDTIME  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RESERVE_CHGENDTIME,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RESERVE_CHGENDTIME,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DevResvGet(CUniStruct<DEVRESVREQ> vrParameter,out CUniStructArray<DEVRESVINFO> vrResult)
		{
			return Cmd(MSREQ_DEVRESV_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE DevResvGet(DEVRESVREQ  vrParameter,out DEVRESVINFO[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_DEVRESV_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_DEVRESV_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE ResvCostAdjust(CUniStruct<RESVCOSTADJUST> vrParameter,out CUniStruct<RESVCOSTADJUST> vrResult)
		{
			return Cmd(MSREQ_RESERVE_COSTADJUST,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE ResvCostAdjust(RESVCOSTADJUST  vrParameter,out RESVCOSTADJUST  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RESERVE_COSTADJUST,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RESERVE_COSTADJUST,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE ResvCheckOut(CUniStruct<RESVCHECKOUT> vrParameter,out CUniStruct<RESVCHECKOUT> vrResult)
		{
			return Cmd(MSREQ_RESERVE_CHECKOUT,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE ResvCheckOut(RESVCHECKOUT  vrParameter,out RESVCHECKOUT  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RESERVE_CHECKOUT,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RESERVE_CHECKOUT,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetTerm(CUniStruct<TERMREQ> vrParameter,out CUniStructArray<UNITERM> vrResult)
		{
			return Cmd(MSREQ_TERM_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetTerm(TERMREQ  vrParameter,out UNITERM[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_TERM_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_TERM_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE SetTerm(CUniStruct<UNITERM> vrParameter,out CUniStruct<UNITERM> vrResult)
		{
			return Cmd(MSREQ_TERM_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE SetTerm(UNITERM  vrParameter,out UNITERM  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_TERM_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_TERM_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DelTerm(CUniStruct<UNITERM> vrParameter)
		{
			return Cmd(MSREQ_TERM_DEL,vrParameter);
		}*/
		
		public REQUESTCODE DelTerm(UNITERM  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_TERM_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_TERM_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE GetClassTimeTable(CUniStruct<CTSREQ> vrParameter,out CUniStructArray<CLASSTIMETABLE> vrResult)
		{
			return Cmd(MSREQ_CTS_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetClassTimeTable(CTSREQ  vrParameter,out CLASSTIMETABLE[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CTS_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CTS_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE SetClassTimeTable(CUniStructArray<CLASSTIMETABLE> vrParameter)
		{
			return Cmd(MSREQ_CTS_SET,vrParameter);
		}*/
		
		public REQUESTCODE SetClassTimeTable(CLASSTIMETABLE[]  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CTS_SET,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CTS_SET,vrParameter);
			}
		}


		/*public REQUESTCODE GetCourse(CUniStruct<COURSEREQ> vrParameter,out CUniStructArray<UNICOURSE> vrResult)
		{
			return Cmd(MSREQ_COURSE_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetCourse(COURSEREQ  vrParameter,out UNICOURSE[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_COURSE_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_COURSE_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE SetCourse(CUniStruct<UNICOURSE> vrParameter,out CUniStruct<UNICOURSE> vrResult)
		{
			return Cmd(MSREQ_COURSE_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE SetCourse(UNICOURSE  vrParameter,out UNICOURSE  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_COURSE_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_COURSE_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DelCourse(CUniStruct<UNICOURSE> vrParameter)
		{
			return Cmd(MSREQ_COURSE_DEL,vrParameter);
		}*/
		
		public REQUESTCODE DelCourse(UNICOURSE  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_COURSE_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_COURSE_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE GetTestCard(CUniStruct<TESTCARDREQ> vrParameter,out CUniStructArray<TESTCARD> vrResult)
		{
			return Cmd(MSREQ_TESTCARD_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetTestCard(TESTCARDREQ  vrParameter,out TESTCARD[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_TESTCARD_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_TESTCARD_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE SetTestCard(CUniStruct<TESTCARD> vrParameter,out CUniStruct<TESTCARD> vrResult)
		{
			return Cmd(MSREQ_TESTCARD_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE SetTestCard(TESTCARD  vrParameter,out TESTCARD  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_TESTCARD_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_TESTCARD_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DelTestCard(CUniStruct<TESTCARD> vrParameter)
		{
			return Cmd(MSREQ_TESTCARD_DEL,vrParameter);
		}*/
		
		public REQUESTCODE DelTestCard(TESTCARD  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_TESTCARD_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_TESTCARD_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE GetTestPlan(CUniStruct<TESTPLANREQ> vrParameter,out CUniStructArray<UNITESTPLAN> vrResult)
		{
			return Cmd(MSREQ_TESTPLAN_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetTestPlan(TESTPLANREQ  vrParameter,out UNITESTPLAN[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_TESTPLAN_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_TESTPLAN_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE SetTestPlan(CUniStruct<UNITESTPLAN> vrParameter,out CUniStruct<UNITESTPLAN> vrResult)
		{
			return Cmd(MSREQ_TESTPLAN_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE SetTestPlan(UNITESTPLAN  vrParameter,out UNITESTPLAN  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_TESTPLAN_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_TESTPLAN_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DelTestPlan(CUniStruct<UNITESTPLAN> vrParameter)
		{
			return Cmd(MSREQ_TESTPLAN_DEL,vrParameter);
		}*/
		
		public REQUESTCODE DelTestPlan(UNITESTPLAN  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_TESTPLAN_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_TESTPLAN_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE GetTestItem(CUniStruct<TESTITEMREQ> vrParameter,out CUniStructArray<UNITESTITEM> vrResult)
		{
			return Cmd(MSREQ_TESTITEM_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetTestItem(TESTITEMREQ  vrParameter,out UNITESTITEM[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_TESTITEM_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_TESTITEM_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE SetTestItem(CUniStruct<UNITESTITEM> vrParameter,out CUniStruct<UNITESTITEM> vrResult)
		{
			return Cmd(MSREQ_TESTITEM_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE SetTestItem(UNITESTITEM  vrParameter,out UNITESTITEM  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_TESTITEM_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_TESTITEM_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DelTestItem(CUniStruct<UNITESTITEM> vrParameter)
		{
			return Cmd(MSREQ_TESTITEM_DEL,vrParameter);
		}*/
		
		public REQUESTCODE DelTestItem(UNITESTITEM  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_TESTITEM_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_TESTITEM_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE GetTestItemMemResv(CUniStruct<TESTITEMMEMRESVREQ> vrParameter,out CUniStructArray<TESTITEMMEMRESV> vrResult)
		{
			return Cmd(MSREQ_TESTITEMMEMRESV_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetTestItemMemResv(TESTITEMMEMRESVREQ  vrParameter,out TESTITEMMEMRESV[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_TESTITEMMEMRESV_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_TESTITEMMEMRESV_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetTestItemInfo(CUniStruct<TESTITEMINFOREQ> vrParameter,out CUniStructArray<TESTITEMINFO> vrResult)
		{
			return Cmd(MSREQ_TESTITEMINFO_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetTestItemInfo(TESTITEMINFOREQ  vrParameter,out TESTITEMINFO[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_TESTITEMINFO_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_TESTITEMINFO_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE UploadReportForm(CUniStruct<REPORTFORMUPLOAD> vrParameter)
		{
			return Cmd(MSREQ_REPORTFORM_UPLOAD,vrParameter);
		}*/
		
		public REQUESTCODE UploadReportForm(REPORTFORMUPLOAD  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORTFORM_UPLOAD,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORTFORM_UPLOAD,vrParameter);
			}
		}


		/*public REQUESTCODE UploadReport(CUniStruct<REPORTUPLOAD> vrParameter)
		{
			return Cmd(MSREQ_REPORT_UPLOAD,vrParameter);
		}*/
		
		public REQUESTCODE UploadReport(REPORTUPLOAD  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_UPLOAD,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_UPLOAD,vrParameter);
			}
		}


		/*public REQUESTCODE CorrectReport(CUniStruct<REPORTCORRECT> vrParameter)
		{
			return Cmd(MSREQ_REPORT_CORRECT,vrParameter);
		}*/
		
		public REQUESTCODE CorrectReport(REPORTCORRECT  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_CORRECT,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_CORRECT,vrParameter);
			}
		}


		/*public REQUESTCODE GetActivityPlan(CUniStruct<ACTIVITYPLANREQ> vrParameter,out CUniStructArray<UNIACTIVITYPLAN> vrResult)
		{
			return Cmd(MSREQ_ACTIVITYPLAN_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetActivityPlan(ACTIVITYPLANREQ  vrParameter,out UNIACTIVITYPLAN[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ACTIVITYPLAN_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ACTIVITYPLAN_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE SetActivityPlan(CUniStruct<UNIACTIVITYPLAN> vrParameter,out CUniStruct<UNIACTIVITYPLAN> vrResult)
		{
			return Cmd(MSREQ_ACTIVITYPLAN_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE SetActivityPlan(UNIACTIVITYPLAN  vrParameter,out UNIACTIVITYPLAN  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ACTIVITYPLAN_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ACTIVITYPLAN_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DelActivityPlan(CUniStruct<UNIACTIVITYPLAN> vrParameter)
		{
			return Cmd(MSREQ_ACTIVITYPLAN_DEL,vrParameter);
		}*/
		
		public REQUESTCODE DelActivityPlan(UNIACTIVITYPLAN  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ACTIVITYPLAN_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ACTIVITYPLAN_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE GetAPSeat(CUniStruct<APSEATREQ> vrParameter,out CUniStructArray<APSEAT> vrResult)
		{
			return Cmd(MSREQ_APSEAT_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetAPSeat(APSEATREQ  vrParameter,out APSEAT[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_APSEAT_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_APSEAT_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE EnrollActivity(CUniStruct<ACTIVITYENROLL> vrParameter)
		{
			return Cmd(MSREQ_ACTIVITY_ENROLL,vrParameter);
		}*/
		
		public REQUESTCODE EnrollActivity(ACTIVITYENROLL  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ACTIVITY_ENROLL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ACTIVITY_ENROLL,vrParameter);
			}
		}


		/*public REQUESTCODE ExitActivity(CUniStruct<ACTIVITYEXIT> vrParameter)
		{
			return Cmd(MSREQ_ACTIVITY_EXIT,vrParameter);
		}*/
		
		public REQUESTCODE ExitActivity(ACTIVITYEXIT  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ACTIVITY_EXIT,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ACTIVITY_EXIT,vrParameter);
			}
		}


		/*public REQUESTCODE ActivityMemberOffLineSign(CUniStruct<AOFFLINESIGN> vrParameter,out CUniStruct<AOFFLINESIGN> vrResult)
		{
			return Cmd(MSREQ_ACTIVITY_OFFLINESIGN,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE ActivityMemberOffLineSign(AOFFLINESIGN  vrParameter,out AOFFLINESIGN  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ACTIVITY_OFFLINESIGN,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ACTIVITY_OFFLINESIGN,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE ResvRuleGet(CUniStruct<RESVRULEREQ> vrParameter,out CUniStructArray<UNIRESVRULE> vrResult)
		{
			return Cmd(MSREQ_RESVRULE_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE ResvRuleGet(RESVRULEREQ  vrParameter,out UNIRESVRULE[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RESVRULE_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RESVRULE_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE ResvRuleAdminGet(CUniStruct<RESVRULEADMINREQ> vrParameter,out CUniStructArray<UNIRESVRULE> vrResult)
		{
			return Cmd(MSREQ_RESVRULE_ADMINGET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE ResvRuleAdminGet(RESVRULEADMINREQ  vrParameter,out UNIRESVRULE[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RESVRULE_ADMINGET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RESVRULE_ADMINGET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE ResvRuleSet(CUniStruct<UNIRESVRULE> vrParameter,out CUniStruct<UNIRESVRULE> vrResult)
		{
			return Cmd(MSREQ_RESVRULE_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE ResvRuleSet(UNIRESVRULE  vrParameter,out UNIRESVRULE  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RESVRULE_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RESVRULE_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE ResvRuleDel(CUniStruct<UNIRESVRULE> vrParameter)
		{
			return Cmd(MSREQ_RESVRULE_DEL,vrParameter);
		}*/
		
		public REQUESTCODE ResvRuleDel(UNIRESVRULE  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RESVRULE_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RESVRULE_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE GetResearchTest(CUniStruct<RESEARCHTESTREQ> vrParameter,out CUniStructArray<RESEARCHTEST> vrResult)
		{
			return Cmd(MSREQ_RESEARCHTEST_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetResearchTest(RESEARCHTESTREQ  vrParameter,out RESEARCHTEST[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RESEARCHTEST_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RESEARCHTEST_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE SetResearchTest(CUniStruct<RESEARCHTEST> vrParameter,out CUniStruct<RESEARCHTEST> vrResult)
		{
			return Cmd(MSREQ_RESEARCHTEST_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE SetResearchTest(RESEARCHTEST  vrParameter,out RESEARCHTEST  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RESEARCHTEST_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RESEARCHTEST_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DelResearchTest(CUniStruct<RESEARCHTEST> vrParameter)
		{
			return Cmd(MSREQ_RESEARCHTEST_DEL,vrParameter);
		}*/
		
		public REQUESTCODE DelResearchTest(RESEARCHTEST  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RESEARCHTEST_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RESEARCHTEST_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE SetRTMember(CUniStruct<RTMEMBER> vrParameter)
		{
			return Cmd(MSREQ_RTMEMBER_SET,vrParameter);
		}*/
		
		public REQUESTCODE SetRTMember(RTMEMBER  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RTMEMBER_SET,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RTMEMBER_SET,vrParameter);
			}
		}


		/*public REQUESTCODE DelRTMember(CUniStruct<RTMEMBER> vrParameter)
		{
			return Cmd(MSREQ_RTMEMBER_DEL,vrParameter);
		}*/
		
		public REQUESTCODE DelRTMember(RTMEMBER  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RTMEMBER_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RTMEMBER_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE GetSampleInfo(CUniStruct<SAMPLEINFOREQ> vrParameter,out CUniStructArray<SAMPLEINFO> vrResult)
		{
			return Cmd(MSREQ_SAMPLEINFO_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetSampleInfo(SAMPLEINFOREQ  vrParameter,out SAMPLEINFO[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_SAMPLEINFO_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_SAMPLEINFO_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE SetSampleInfo(CUniStruct<SAMPLEINFO> vrParameter,out CUniStruct<SAMPLEINFO> vrResult)
		{
			return Cmd(MSREQ_SAMPLEINFO_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE SetSampleInfo(SAMPLEINFO  vrParameter,out SAMPLEINFO  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_SAMPLEINFO_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_SAMPLEINFO_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DelSampleInfo(CUniStruct<SAMPLEINFO> vrParameter)
		{
			return Cmd(MSREQ_SAMPLEINFO_DEL,vrParameter);
		}*/
		
		public REQUESTCODE DelSampleInfo(SAMPLEINFO  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_SAMPLEINFO_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_SAMPLEINFO_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE GetYardResv(CUniStruct<YARDRESVREQ> vrParameter,out CUniStructArray<YARDRESV> vrResult)
		{
			return Cmd(MSREQ_YARDRESV_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetYardResv(YARDRESVREQ  vrParameter,out YARDRESV[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_YARDRESV_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_YARDRESV_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE SetYardResv(CUniStruct<YARDRESV> vrParameter,out CUniStruct<YARDRESV> vrResult)
		{
			return Cmd(MSREQ_YARDRESV_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE SetYardResv(YARDRESV  vrParameter,out YARDRESV  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_YARDRESV_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_YARDRESV_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DelYardResv(CUniStruct<YARDRESV> vrParameter)
		{
			return Cmd(MSREQ_YARDRESV_DEL,vrParameter);
		}*/
		
		public REQUESTCODE DelYardResv(YARDRESV  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_YARDRESV_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_YARDRESV_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE GetYardResvCheckInfo(CUniStruct<YARDRESVCHECKINFOREQ> vrParameter,out CUniStructArray<YARDRESVCHECKINFO> vrResult)
		{
			return Cmd(MSREQ_YARDRESVCHECKINFO_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetYardResvCheckInfo(YARDRESVCHECKINFOREQ  vrParameter,out YARDRESVCHECKINFO[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_YARDRESVCHECKINFO_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_YARDRESVCHECKINFO_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE YardResvCheck(CUniStruct<YARDRESVCHECK> vrParameter)
		{
			return Cmd(MSREQ_YARDRESV_CHECK,vrParameter);
		}*/
		
		public REQUESTCODE YardResvCheck(YARDRESVCHECK  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_YARDRESV_CHECK,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_YARDRESV_CHECK,vrParameter);
			}
		}


		/*public REQUESTCODE GetResvCheckInfo(CUniStruct<RESVCHECKINFOREQ> vrParameter,out CUniStructArray<RESVCHECKINFO> vrResult)
		{
			return Cmd(MSREQ_RESVCHECKINFO_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetResvCheckInfo(RESVCHECKINFOREQ  vrParameter,out RESVCHECKINFO[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RESVCHECKINFO_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RESVCHECKINFO_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE ResvCheck(CUniStruct<RESVCHECKINFO> vrParameter)
		{
			return Cmd(MSREQ_RESV_CHECK,vrParameter);
		}*/
		
		public REQUESTCODE ResvCheck(RESVCHECKINFO  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RESV_CHECK,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RESV_CHECK,vrParameter);
			}
		}


		/*public REQUESTCODE GetYardActivity(CUniStruct<YARDACTIVITYREQ> vrParameter,out CUniStructArray<YARDACTIVITY> vrResult)
		{
			return Cmd(MSREQ_YARDACTIVITY_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetYardActivity(YARDACTIVITYREQ  vrParameter,out YARDACTIVITY[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_YARDACTIVITY_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_YARDACTIVITY_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE SetYardActivity(CUniStruct<YARDACTIVITY> vrParameter,out CUniStruct<YARDACTIVITY> vrResult)
		{
			return Cmd(MSREQ_YARDACTIVITY_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE SetYardActivity(YARDACTIVITY  vrParameter,out YARDACTIVITY  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_YARDACTIVITY_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_YARDACTIVITY_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DelYardActivity(CUniStruct<YARDACTIVITY> vrParameter)
		{
			return Cmd(MSREQ_YARDACTIVITY_DEL,vrParameter);
		}*/
		
		public REQUESTCODE DelYardActivity(YARDACTIVITY  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_YARDACTIVITY_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_YARDACTIVITY_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE ThirdResvShareDev(CUniStruct<THIRDRESVSHAREDEV> vrParameter,out CUniStruct<THIRDRESVSHAREDEV> vrResult)
		{
			return Cmd(MSREQ_THIRDRESV_SHAREDEV,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE ThirdResvShareDev(THIRDRESVSHAREDEV  vrParameter,out THIRDRESVSHAREDEV  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_THIRDRESV_SHAREDEV,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_THIRDRESV_SHAREDEV,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE ThirdResvDel(CUniStruct<THIRDRESVDEL> vrParameter)
		{
			return Cmd(MSREQ_THIRDRESV_DEL,vrParameter);
		}*/
		
		public REQUESTCODE ThirdResvDel(THIRDRESVDEL  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_THIRDRESV_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_THIRDRESV_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE GetThirdResv(CUniStruct<THIRDRESVREQ> vrParameter,out CUniStructArray<THIRDRESV> vrResult)
		{
			return Cmd(MSREQ_THIRDRESV_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetThirdResv(THIRDRESVREQ  vrParameter,out THIRDRESV[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_THIRDRESV_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_THIRDRESV_GET,vrParameter,out vrResult);
			}
		}

	
	}
	#endregion PRReserve部分

	#region PRControl部分
	/*上网游戏控制*/
	public partial class PRControl:PRModule
	{
		public PRControl(UniDCom _UniDCom):base(_UniDCom)
		{
			m_nModule = CONTROL_BASE;
		}
		
		/*public REQUESTCODE GetCtrlClass(CUniStruct<CTRLCLASSREQ> vrParameter,out CUniStructArray<UNICTRLCLASS> vrResult)
		{
			return Cmd(MSREQ_CTRLCLASS_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetCtrlClass(CTRLCLASSREQ  vrParameter,out UNICTRLCLASS[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CTRLCLASS_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CTRLCLASS_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE SetCtrlClass(CUniStruct<UNICTRLCLASS> vrParameter,out CUniStruct<UNICTRLCLASS> vrResult)
		{
			return Cmd(MSREQ_CTRLCLASS_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE SetCtrlClass(UNICTRLCLASS  vrParameter,out UNICTRLCLASS  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CTRLCLASS_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CTRLCLASS_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DelCtrlClass(CUniStruct<UNICTRLCLASS> vrParameter)
		{
			return Cmd(MSREQ_CTRLCLASS_DEL,vrParameter);
		}*/
		
		public REQUESTCODE DelCtrlClass(UNICTRLCLASS  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CTRLCLASS_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CTRLCLASS_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE GetCtrlURL(CUniStruct<CTRLURLREQ> vrParameter,out CUniStructArray<UNICTRLURL> vrResult)
		{
			return Cmd(MSREQ_CTRLURL_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetCtrlURL(CTRLURLREQ  vrParameter,out UNICTRLURL[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CTRLURL_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CTRLURL_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE SetCtrlURL(CUniStruct<UNICTRLURL> vrParameter,out CUniStruct<UNICTRLURL> vrResult)
		{
			return Cmd(MSREQ_CTRLURL_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE SetCtrlURL(UNICTRLURL  vrParameter,out UNICTRLURL  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CTRLURL_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CTRLURL_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DelCtrlURL(CUniStruct<UNICTRLURL> vrParameter)
		{
			return Cmd(MSREQ_CTRLURL_DEL,vrParameter);
		}*/
		
		public REQUESTCODE DelCtrlURL(UNICTRLURL  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CTRLURL_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CTRLURL_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE GetCtrlSW(CUniStruct<CTRLSWREQ> vrParameter,out CUniStructArray<UNICTRLSW> vrResult)
		{
			return Cmd(MSREQ_CTRLSW_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetCtrlSW(CTRLSWREQ  vrParameter,out UNICTRLSW[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CTRLSW_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CTRLSW_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE SetCtrlSW(CUniStruct<UNICTRLSW> vrParameter,out CUniStruct<UNICTRLSW> vrResult)
		{
			return Cmd(MSREQ_CTRLSW_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE SetCtrlSW(UNICTRLSW  vrParameter,out UNICTRLSW  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CTRLSW_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CTRLSW_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DelCtrlSW(CUniStruct<UNICTRLSW> vrParameter)
		{
			return Cmd(MSREQ_CTRLSW_DEL,vrParameter);
		}*/
		
		public REQUESTCODE DelCtrlSW(UNICTRLSW  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CTRLSW_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CTRLSW_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE GetSoftware(CUniStruct<SOFTWAREREQ> vrParameter,out CUniStructArray<UNISOFTWARE> vrResult)
		{
			return Cmd(MSREQ_SOFTWARE_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetSoftware(SOFTWAREREQ  vrParameter,out UNISOFTWARE[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_SOFTWARE_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_SOFTWARE_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE SetSoftware(CUniStruct<UNISOFTWARE> vrParameter,out CUniStruct<UNISOFTWARE> vrResult)
		{
			return Cmd(MSREQ_SOFTWARE_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE SetSoftware(UNISOFTWARE  vrParameter,out UNISOFTWARE  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_SOFTWARE_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_SOFTWARE_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetProgram(CUniStruct<PROGRAMREQ> vrParameter,out CUniStructArray<UNIPROGRAM> vrResult)
		{
			return Cmd(MSREQ_PROGRAM_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetProgram(PROGRAMREQ  vrParameter,out UNIPROGRAM[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_PROGRAM_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_PROGRAM_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE SetProgram(CUniStruct<UNIPROGRAM> vrParameter,out CUniStruct<UNIPROGRAM> vrResult)
		{
			return Cmd(MSREQ_PROGRAM_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE SetProgram(UNIPROGRAM  vrParameter,out UNIPROGRAM  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_PROGRAM_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_PROGRAM_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetPCSWinfo(CUniStruct<PCSWINFOREQ> vrParameter,out CUniStructArray<UNIPCSWINFO> vrResult)
		{
			return Cmd(MSREQ_PCSWINFO_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetPCSWinfo(PCSWINFOREQ  vrParameter,out UNIPCSWINFO[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_PCSWINFO_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_PCSWINFO_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetRoomSWinfo(CUniStruct<ROOMSWINFOREQ> vrParameter,out CUniStructArray<UNIROOMSWINFO> vrResult)
		{
			return Cmd(MSREQ_ROOMSWINFO_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetRoomSWinfo(ROOMSWINFOREQ  vrParameter,out UNIROOMSWINFO[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ROOMSWINFO_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ROOMSWINFO_GET,vrParameter,out vrResult);
			}
		}

	
	}
	#endregion PRControl部分

	#region PRTHIRDIF部分
	/*第三方接口管理*/
	public partial class PRTHIRDIF:PRModule
	{
		public PRTHIRDIF(UniDCom _UniDCom):base(_UniDCom)
		{
			m_nModule = THIRDIF_BASE;
		}
		
		/*public REQUESTCODE ThirdLogin(CUniStruct<THIRDLOGINREQ> vrParameter,out CUniStruct<THIRDLOGINRES> vrResult)
		{
			return Cmd(THIRDIF_LOGIN,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE ThirdLogin(THIRDLOGINREQ  vrParameter,out THIRDLOGINRES  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(THIRDIF_LOGIN,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(THIRDIF_LOGIN,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE ThirdLogout()
		{
			return Cmd(THIRDIF_LOGOUT);
		}*/
		
		public REQUESTCODE ThirdLogout()
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(THIRDIF_LOGOUT);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(THIRDIF_LOGOUT);
			}
		}


		/*public REQUESTCODE ThirdGetAcc(CUniStruct<THIRDACCREQ> vrParameter,out CUniStructArray<UNIACCOUNT> vrResult)
		{
			return Cmd(THIRDIF_ACC_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE ThirdGetAcc(THIRDACCREQ  vrParameter,out UNIACCOUNT[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(THIRDIF_ACC_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(THIRDIF_ACC_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE SyncAcc(CUniStruct<SYNCACCREQ> vrParameter,out CUniStruct<SYNCACCINFO> vrResult)
		{
			return Cmd(THIRDIF_SYNCACC,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE SyncAcc(SYNCACCREQ  vrParameter,out SYNCACCINFO  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(THIRDIF_SYNCACC,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(THIRDIF_SYNCACC,vrParameter,out vrResult);
			}
		}

	
	}
	#endregion PRTHIRDIF部分

	#region PRFee部分
	/*收费管理*/
	public partial class PRFee:PRModule
	{
		public PRFee(UniDCom _UniDCom):base(_UniDCom)
		{
			m_nModule = FEE_BASE;
		}
		
		/*public REQUESTCODE Get(CUniStruct<FEEREQ> vrParameter,out CUniStructArray<UNIFEE> vrResult)
		{
			return Cmd(MSREQ_FEE_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE Get(FEEREQ  vrParameter,out UNIFEE[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_FEE_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_FEE_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE RTDevFeeGet(CUniStruct<RTDEVFEEREQ> vrParameter,out CUniStruct<UNIFEE> vrResult)
		{
			return Cmd(MSREQ_RTDEVFEE_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE RTDevFeeGet(RTDEVFEEREQ  vrParameter,out UNIFEE  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RTDEVFEE_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RTDEVFEE_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE RTDevSampleGet(CUniStruct<RTDEVSAMPLEREQ> vrParameter,out CUniStructArray<RTDEVSAMPLE> vrResult)
		{
			return Cmd(MSREQ_RTDEVSAMPLE_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE RTDevSampleGet(RTDEVSAMPLEREQ  vrParameter,out RTDEVSAMPLE[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RTDEVSAMPLE_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RTDEVSAMPLE_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE Set(CUniStruct<UNIFEE> vrParameter,out CUniStruct<UNIFEE> vrResult)
		{
			return Cmd(MSREQ_FEE_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE Set(UNIFEE  vrParameter,out UNIFEE  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_FEE_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_FEE_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE Del(CUniStruct<UNIFEE> vrParameter)
		{
			return Cmd(MSREQ_FEE_DEL,vrParameter);
		}*/
		
		public REQUESTCODE Del(UNIFEE  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_FEE_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_FEE_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE FTRuleGet(CUniStruct<FTRULEREQ> vrParameter,out CUniStructArray<FREETIMERULE> vrResult)
		{
			return Cmd(MSREQ_FREETIMERULE_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE FTRuleGet(FTRULEREQ  vrParameter,out FREETIMERULE[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_FREETIMERULE_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_FREETIMERULE_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE FTRuleSet(CUniStruct<FREETIMERULE> vrParameter,out CUniStruct<FREETIMERULE> vrResult)
		{
			return Cmd(MSREQ_FREETIMERULE_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE FTRuleSet(FREETIMERULE  vrParameter,out FREETIMERULE  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_FREETIMERULE_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_FREETIMERULE_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE FTRuleDel(CUniStruct<FREETIMERULE> vrParameter)
		{
			return Cmd(MSREQ_FREETIMERULE_DEL,vrParameter);
		}*/
		
		public REQUESTCODE FTRuleDel(FREETIMERULE  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_FREETIMERULE_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_FREETIMERULE_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE BillGet(CUniStruct<BILLREQ> vrParameter,out CUniStructArray<UNIBILL> vrResult)
		{
			return Cmd(MSREQ_BILL_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE BillGet(BILLREQ  vrParameter,out UNIBILL[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_BILL_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_BILL_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE BillSet(CUniStruct<UNIBILL> vrParameter)
		{
			return Cmd(MSREQ_BILL_SET,vrParameter);
		}*/
		
		public REQUESTCODE BillSet(UNIBILL  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_BILL_SET,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_BILL_SET,vrParameter);
			}
		}


		/*public REQUESTCODE BillPay(CUniStruct<BILLPAY> vrParameter)
		{
			return Cmd(MSREQ_BILL_PAY,vrParameter);
		}*/
		
		public REQUESTCODE BillPay(BILLPAY  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_BILL_PAY,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_BILL_PAY,vrParameter);
			}
		}

	
	}
	#endregion PRFee部分

	#region PRConsole部分
	/*控制台管理*/
	public partial class PRConsole:PRModule
	{
		public PRConsole(UniDCom _UniDCom):base(_UniDCom)
		{
			m_nModule = CONSOLE_BASE;
		}
		
		/*public REQUESTCODE ConGet(CUniStruct<CONREQ> vrParameter,out CUniStructArray<UNICONSOLE> vrResult)
		{
			return Cmd(MSREQ_CONSOLE_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE ConGet(CONREQ  vrParameter,out UNICONSOLE[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CONSOLE_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CONSOLE_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE ConSet(CUniStruct<UNICONSOLE> vrParameter,out CUniStruct<UNICONSOLE> vrResult)
		{
			return Cmd(MSREQ_CONSOLE_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE ConSet(UNICONSOLE  vrParameter,out UNICONSOLE  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CONSOLE_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CONSOLE_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE ConDel(CUniStruct<UNICONSOLE> vrParameter)
		{
			return Cmd(MSREQ_CONSOLE_DEL,vrParameter);
		}*/
		
		public REQUESTCODE ConDel(UNICONSOLE  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CONSOLE_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CONSOLE_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE ConLogin(CUniStruct<CONLOGINREQ> vrParameter,out CUniStruct<CONLOGINRES> vrResult)
		{
			return Cmd(MSREQ_CONSOLE_LOGIN,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE ConLogin(CONLOGINREQ  vrParameter,out CONLOGINRES  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CONSOLE_LOGIN,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CONSOLE_LOGIN,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE ConLogout(CUniStruct<CONLOGOUTREQ> vrParameter)
		{
			return Cmd(MSREQ_CONSOLE_LOGOUT,vrParameter);
		}*/
		
		public REQUESTCODE ConLogout(CONLOGOUTREQ  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CONSOLE_LOGOUT,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CONSOLE_LOGOUT,vrParameter);
			}
		}


		/*public REQUESTCODE ConPulse(CUniStruct<CONPULSEREQ> vrParameter,out CUniStruct<CONPULSERES> vrResult)
		{
			return Cmd(MSREQ_CONSOLE_PULSE,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE ConPulse(CONPULSEREQ  vrParameter,out CONPULSERES  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CONSOLE_PULSE,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CONSOLE_PULSE,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE ConShowMsg(CUniStruct<CONMESSAGE> vrParameter)
		{
			return Cmd(MSREQ_CONSOLE_SHOWMSG,vrParameter);
		}*/
		
		public REQUESTCODE ConShowMsg(CONMESSAGE  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CONSOLE_SHOWMSG,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CONSOLE_SHOWMSG,vrParameter);
			}
		}


		/*public REQUESTCODE ConUserCard(CUniStruct<ACCCHECKREQ> vrParameter,out CUniStruct<CONUSERINFO> vrResult)
		{
			return Cmd(MSREQ_CONSOLE_USERCARD,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE ConUserCard(ACCCHECKREQ  vrParameter,out CONUSERINFO  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CONSOLE_USERCARD,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CONSOLE_USERCARD,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE ConTeacherIn(CUniStruct<ACCCHECKREQ> vrParameter,out CUniStruct<CONTEACHERINFO> vrResult)
		{
			return Cmd(MSREQ_CONSOLE_TEACHERIN,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE ConTeacherIn(ACCCHECKREQ  vrParameter,out CONTEACHERINFO  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CONSOLE_TEACHERIN,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CONSOLE_TEACHERIN,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE ConCardForPC(CUniStruct<CARDFORPCREQ> vrParameter,out CUniStruct<CARDFORPCRES> vrResult)
		{
			return Cmd(MSREQ_CONSOLE_CARDFORPC,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE ConCardForPC(CARDFORPCREQ  vrParameter,out CARDFORPCRES  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CONSOLE_CARDFORPC,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CONSOLE_CARDFORPC,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE AutoGateCard(CUniStruct<AUTOGATECARDREQ> vrParameter,out CUniStruct<AUTOGATECARDRES> vrResult)
		{
			return Cmd(MSREQ_CONSOLE_AUTOGATECARD,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE AutoGateCard(AUTOGATECARDREQ  vrParameter,out AUTOGATECARDRES  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CONSOLE_AUTOGATECARD,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CONSOLE_AUTOGATECARD,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE MobileScan(CUniStruct<MOBILESCANREQ> vrParameter,out CUniStruct<MOBILESCANRES> vrResult)
		{
			return Cmd(MSREQ_MOBILE_SCAN,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE MobileScan(MOBILESCANREQ  vrParameter,out MOBILESCANRES  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_MOBILE_SCAN,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_MOBILE_SCAN,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE ConUserIn(CUniStruct<CONUSERINREQ> vrParameter,out CUniStruct<CONUSERINRES> vrResult)
		{
			return Cmd(MSREQ_CONSOLE_USERIN,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE ConUserIn(CONUSERINREQ  vrParameter,out CONUSERINRES  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CONSOLE_USERIN,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CONSOLE_USERIN,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE MobileUserIn(CUniStruct<MOBILEUSERINREQ> vrParameter,out CUniStruct<MOBILEUSERINRES> vrResult)
		{
			return Cmd(MSREQ_MOBILE_USERIN,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE MobileUserIn(MOBILEUSERINREQ  vrParameter,out MOBILEUSERINRES  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_MOBILE_USERIN,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_MOBILE_USERIN,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE MobileDelay(CUniStruct<MOBILEDELAYREQ> vrParameter,out CUniStruct<MOBILEDELAYRES> vrResult)
		{
			return Cmd(MSREQ_MOBILE_DELAY,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE MobileDelay(MOBILEDELAYREQ  vrParameter,out MOBILEDELAYRES  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_MOBILE_DELAY,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_MOBILE_DELAY,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE ConUserOut(CUniStruct<CONUSEROUTREQ> vrParameter,out CUniStruct<CONUSEROUTRES> vrResult)
		{
			return Cmd(MSREQ_CONSOLE_USEROUT,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE ConUserOut(CONUSEROUTREQ  vrParameter,out CONUSEROUTRES  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CONSOLE_USEROUT,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CONSOLE_USEROUT,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE MobileUserOut(CUniStruct<MOBILEUSEROUTREQ> vrParameter,out CUniStruct<MOBILEUSEROUTRES> vrResult)
		{
			return Cmd(MSREQ_MOBILE_USEROUT,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE MobileUserOut(MOBILEUSEROUTREQ  vrParameter,out MOBILEUSEROUTRES  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_MOBILE_USEROUT,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_MOBILE_USEROUT,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE ResvUserComeIn(CUniStruct<RESVUSERCOMEINREQ> vrParameter,out CUniStruct<RESVUSERCOMEINRES> vrResult)
		{
			return Cmd(MSREQ_RESVUSER_COMEIN,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE ResvUserComeIn(RESVUSERCOMEINREQ  vrParameter,out RESVUSERCOMEINRES  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RESVUSER_COMEIN,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RESVUSER_COMEIN,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE ResvUserDelay(CUniStruct<RESVUSERDELAYREQ> vrParameter,out CUniStruct<RESVUSERDELAYRES> vrResult)
		{
			return Cmd(MSREQ_RESVUSER_DELAY,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE ResvUserDelay(RESVUSERDELAYREQ  vrParameter,out RESVUSERDELAYRES  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RESVUSER_DELAY,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RESVUSER_DELAY,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE ResvUserGoOut(CUniStruct<RESVUSERGOOUTREQ> vrParameter,out CUniStruct<RESVUSERGOOUTRES> vrResult)
		{
			return Cmd(MSREQ_RESVUSER_GOOUT,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE ResvUserGoOut(RESVUSERGOOUTREQ  vrParameter,out RESVUSERGOOUTRES  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RESVUSER_GOOUT,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RESVUSER_GOOUT,vrParameter,out vrResult);
			}
		}

	
	}
	#endregion PRConsole部分

	#region PRReport部分
	/*报表查询统计*/
	public partial class PRReport:PRModule
	{
		public PRReport(UniDCom _UniDCom):base(_UniDCom)
		{
			m_nModule = REPORT_BASE;
		}
		
		/*public REQUESTCODE ResvRecGet(CUniStruct<RESVRECREQ> vrParameter,out CUniStructArray<UNIRESVREC> vrResult)
		{
			return Cmd(MSREQ_REPORT_RESVREC_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE ResvRecGet(RESVRECREQ  vrParameter,out UNIRESVREC[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_RESVREC_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_RESVREC_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetResvKindStat(CUniStruct<RESVKINDSTATREQ> vrParameter,out CUniStructArray<RESVKINDSTAT> vrResult)
		{
			return Cmd(MSREQ_REPORT_RESVREC_KINDSTAT,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetResvKindStat(RESVKINDSTATREQ  vrParameter,out RESVKINDSTAT[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_RESVREC_KINDSTAT,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_RESVREC_KINDSTAT,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetResvModeStat(CUniStruct<RESVMODESTATREQ> vrParameter,out CUniStructArray<RESVMODESTAT> vrResult)
		{
			return Cmd(MSREQ_RESVMODESTAT_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetResvModeStat(RESVMODESTATREQ  vrParameter,out RESVMODESTAT[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RESVMODESTAT_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RESVMODESTAT_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetUseRec(CUniStruct<USERECREQ> vrParameter,out CUniStructArray<DEVUSEREC> vrResult)
		{
			return Cmd(MSREQ_REPORT_USEREC_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetUseRec(USERECREQ  vrParameter,out DEVUSEREC[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_USEREC_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_USEREC_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetDoorCardRec(CUniStruct<DOORCARDRECREQ> vrParameter,out CUniStructArray<DOORCARDREC> vrResult)
		{
			return Cmd(MSREQ_REPORT_DOORCARDREC_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetDoorCardRec(DOORCARDRECREQ  vrParameter,out DOORCARDREC[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_DOORCARDREC_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_DOORCARDREC_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetUserStat(CUniStruct<REPORTREQ> vrParameter,out CUniStructArray<USERSTAT> vrResult)
		{
			return Cmd(MSREQ_REPORT_USEREC_USERSTAT,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetUserStat(REPORTREQ  vrParameter,out USERSTAT[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_USEREC_USERSTAT,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_USEREC_USERSTAT,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetLabStat(CUniStruct<REPORTREQ> vrParameter,out CUniStructArray<LABSTAT> vrResult)
		{
			return Cmd(MSREQ_REPORT_USEREC_LABSTAT,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetLabStat(REPORTREQ  vrParameter,out LABSTAT[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_USEREC_LABSTAT,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_USEREC_LABSTAT,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetDevKindStat(CUniStruct<REPORTREQ> vrParameter,out CUniStructArray<DEVKINDSTAT> vrResult)
		{
			return Cmd(MSREQ_REPORT_USEREC_DEVKINDSTAT,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetDevKindStat(REPORTREQ  vrParameter,out DEVKINDSTAT[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_USEREC_DEVKINDSTAT,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_USEREC_DEVKINDSTAT,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetDevStat(CUniStruct<REPORTREQ> vrParameter,out CUniStructArray<DEVSTAT> vrResult)
		{
			return Cmd(MSREQ_REPORT_USEREC_DEVSTAT,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetDevStat(REPORTREQ  vrParameter,out DEVSTAT[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_USEREC_DEVSTAT,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_USEREC_DEVSTAT,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetTestItemStat(CUniStruct<TESTITEMSTATREQ> vrParameter,out CUniStructArray<TESTITEMSTAT> vrResult)
		{
			return Cmd(MSREQ_REPORT_USEREC_TESTITEMSTAT,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetTestItemStat(TESTITEMSTATREQ  vrParameter,out TESTITEMSTAT[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_USEREC_TESTITEMSTAT,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_USEREC_TESTITEMSTAT,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetDevClassStat(CUniStruct<REPORTREQ> vrParameter,out CUniStructArray<DEVCLASSSTAT> vrResult)
		{
			return Cmd(MSREQ_REPORT_USEREC_DEVCLASSSTAT,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetDevClassStat(REPORTREQ  vrParameter,out DEVCLASSSTAT[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_USEREC_DEVCLASSSTAT,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_USEREC_DEVCLASSSTAT,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetDeptStat(CUniStruct<REPORTREQ> vrParameter,out CUniStructArray<DEPTSTAT> vrResult)
		{
			return Cmd(MSREQ_REPORT_USEREC_DEPTSTAT,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetDeptStat(REPORTREQ  vrParameter,out DEPTSTAT[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_USEREC_DEPTSTAT,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_USEREC_DEPTSTAT,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GeIdentStat(CUniStruct<IDENTSTATREQ> vrParameter,out CUniStructArray<IDENTSTAT> vrResult)
		{
			return Cmd(MSREQ_REPORT_USEREC_IDENTSTAT,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GeIdentStat(IDENTSTATREQ  vrParameter,out IDENTSTAT[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_USEREC_IDENTSTAT,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_USEREC_IDENTSTAT,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetTeachingResvRec(CUniStruct<TEACHINGRESVRECREQ> vrParameter,out CUniStructArray<TEACHINGRESVREC> vrResult)
		{
			return Cmd(MSREQ_REPORT_TEACHINGRESVREC,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetTeachingResvRec(TEACHINGRESVRECREQ  vrParameter,out TEACHINGRESVREC[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_TEACHINGRESVREC,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_TEACHINGRESVREC,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetRoomStat(CUniStruct<REPORTREQ> vrParameter,out CUniStructArray<ROOMSTAT> vrResult)
		{
			return Cmd(MSREQ_REPORT_ROOMSTAT,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetRoomStat(REPORTREQ  vrParameter,out ROOMSTAT[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_ROOMSTAT,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_ROOMSTAT,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetDevUsingRate(CUniStruct<DEVUSINGRATEREQ> vrParameter,out CUniStruct<DEVUSINGRATE> vrResult)
		{
			return Cmd(MSREQ_REPORT_DEVUSINGRATE,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetDevUsingRate(DEVUSINGRATEREQ  vrParameter,out DEVUSINGRATE  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_DEVUSINGRATE,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_DEVUSINGRATE,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetDevMonthStat(CUniStruct<DEVMONTHSTATREQ> vrParameter,out CUniStructArray<DEVMONTHSTAT> vrResult)
		{
			return Cmd(MSREQ_REPORT_DEVMONTHSTAT,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetDevMonthStat(DEVMONTHSTATREQ  vrParameter,out DEVMONTHSTAT[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_DEVMONTHSTAT,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_DEVMONTHSTAT,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetRTUseStat(CUniStruct<RTUSESTATREQ> vrParameter,out CUniStructArray<RTUSESTAT> vrResult)
		{
			return Cmd(MSREQ_REPORT_RTUSESTAT,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetRTUseStat(RTUSESTATREQ  vrParameter,out RTUSESTAT[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_RTUSESTAT,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_RTUSESTAT,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetRTUseDetail(CUniStruct<RTUSEDETAILREQ> vrParameter,out CUniStructArray<RTUSEDETAIL> vrResult)
		{
			return Cmd(MSREQ_REPORT_RTUSEDETAIL,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetRTUseDetail(RTUSEDETAILREQ  vrParameter,out RTUSEDETAIL[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_RTUSEDETAIL,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_RTUSEDETAIL,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetRTFAStat(CUniStruct<RTFASTATREQ> vrParameter,out CUniStructArray<RTFASTAT> vrResult)
		{
			return Cmd(MSREQ_REPORT_RTFASTAT,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetRTFAStat(RTFASTATREQ  vrParameter,out RTFASTAT[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_RTFASTAT,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_RTFASTAT,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetRTFADetail(CUniStruct<RTFADETAILREQ> vrParameter,out CUniStructArray<RTFADETAIL> vrResult)
		{
			return Cmd(MSREQ_REPORT_RTFADETAIL,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetRTFADetail(RTFADETAILREQ  vrParameter,out RTFADETAIL[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_RTFADETAIL,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_RTFADETAIL,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetDefaultStat(CUniStruct<DEFAULTSTATREQ> vrParameter,out CUniStructArray<DEFAULTSTAT> vrResult)
		{
			return Cmd(MSREQ_REPORT_DEFAULTSTAT,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetDefaultStat(DEFAULTSTATREQ  vrParameter,out DEFAULTSTAT[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_DEFAULTSTAT,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_DEFAULTSTAT,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetDevWeekUsingRate(CUniStruct<DEVWEEKUSINGRATEREQ> vrParameter,out CUniStruct<DEVWEEKUSINGRATE> vrResult)
		{
			return Cmd(MSREQ_REPORT_DEVWEEKUSINGRATE,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetDevWeekUsingRate(DEVWEEKUSINGRATEREQ  vrParameter,out DEVWEEKUSINGRATE  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_DEVWEEKUSINGRATE,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_DEVWEEKUSINGRATE,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetYardActivityStat(CUniStruct<YARDACTIVITYSTATREQ> vrParameter,out CUniStructArray<YARDACTIVITYSTAT> vrResult)
		{
			return Cmd(MSREQ_REPORT_YARDACTIVITYSTAT,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetYardActivityStat(YARDACTIVITYSTATREQ  vrParameter,out YARDACTIVITYSTAT[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_YARDACTIVITYSTAT,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_YARDACTIVITYSTAT,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetDevList(CUniStruct<DEVLISTREQ> vrParameter,out CUniStructArray<DEVLIST> vrResult)
		{
			return Cmd(MSREQ_REPORT_DEVLIST,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetDevList(DEVLISTREQ  vrParameter,out DEVLIST[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_DEVLIST,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_DEVLIST,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetDevChg(CUniStruct<DEVCHGREQ> vrParameter,out CUniStruct<DEVCHG> vrResult)
		{
			return Cmd(MSREQ_REPORT_DEVCHG,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetDevChg(DEVCHGREQ  vrParameter,out DEVCHG  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_DEVCHG,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_DEVCHG,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetBigDev(CUniStruct<BIGDEVREQ> vrParameter,out CUniStructArray<BIGDEV> vrResult)
		{
			return Cmd(MSREQ_REPORT_BIGDEV,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetBigDev(BIGDEVREQ  vrParameter,out BIGDEV[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_BIGDEV,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_BIGDEV,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetTestItemReport(CUniStruct<TESTITEMREPORTREQ> vrParameter,out CUniStructArray<TESTITEMREPORT> vrResult)
		{
			return Cmd(MSREQ_REPORT_TESTITEMREPORT,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetTestItemReport(TESTITEMREPORTREQ  vrParameter,out TESTITEMREPORT[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_TESTITEMREPORT,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_TESTITEMREPORT,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetStaffInfo(CUniStruct<STAFFINFOREQ> vrParameter,out CUniStructArray<STAFFINFO> vrResult)
		{
			return Cmd(MSREQ_REPORT_STAFFINFO,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetStaffInfo(STAFFINFOREQ  vrParameter,out STAFFINFO[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_STAFFINFO,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_STAFFINFO,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetLabInfo(CUniStruct<LABINFOREQ> vrParameter,out CUniStructArray<LABINFO> vrResult)
		{
			return Cmd(MSREQ_REPORT_LABINFO,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetLabInfo(LABINFOREQ  vrParameter,out LABINFO[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_LABINFO,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_LABINFO,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetLabAllCost(CUniStruct<LABALLCOSTREQ> vrParameter,out CUniStruct<LABALLCOST> vrResult)
		{
			return Cmd(MSREQ_REPORT_LABALLCOST,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetLabAllCost(LABALLCOSTREQ  vrParameter,out LABALLCOST  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_LABALLCOST,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_LABALLCOST,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetLabSummary(CUniStruct<LABSUMMARYREQ> vrParameter,out CUniStruct<LABSUMMARY> vrResult)
		{
			return Cmd(MSREQ_REPORT_LABSUMMARY,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetLabSummary(LABSUMMARYREQ  vrParameter,out LABSUMMARY  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_LABSUMMARY,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_LABSUMMARY,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetLabSummary2(CUniStruct<LABSUMMARY2REQ> vrParameter,out CUniStruct<LABSUMMARY2> vrResult)
		{
			return Cmd(MSREQ_REPORT_LABSUMMARY2,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetLabSummary2(LABSUMMARY2REQ  vrParameter,out LABSUMMARY2  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_LABSUMMARY2,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_LABSUMMARY2,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE SetDevList(CUniStruct<DEVLIST> vrParameter)
		{
			return Cmd(MSREQ_REPORT_DEVLIST_SET,vrParameter);
		}*/
		
		public REQUESTCODE SetDevList(DEVLIST  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_DEVLIST_SET,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_DEVLIST_SET,vrParameter);
			}
		}


		/*public REQUESTCODE SetDevChg(CUniStruct<DEVCHG> vrParameter)
		{
			return Cmd(MSREQ_REPORT_DEVCHG_SET,vrParameter);
		}*/
		
		public REQUESTCODE SetDevChg(DEVCHG  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_DEVCHG_SET,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_DEVCHG_SET,vrParameter);
			}
		}


		/*public REQUESTCODE SetBigDev(CUniStruct<BIGDEV> vrParameter)
		{
			return Cmd(MSREQ_REPORT_BIGDEV_SET,vrParameter);
		}*/
		
		public REQUESTCODE SetBigDev(BIGDEV  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_BIGDEV_SET,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_BIGDEV_SET,vrParameter);
			}
		}


		/*public REQUESTCODE SetTestItemReport(CUniStruct<TESTITEMREPORT> vrParameter)
		{
			return Cmd(MSREQ_REPORT_TESTITEMREPORT_SET,vrParameter);
		}*/
		
		public REQUESTCODE SetTestItemReport(TESTITEMREPORT  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_TESTITEMREPORT_SET,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_TESTITEMREPORT_SET,vrParameter);
			}
		}


		/*public REQUESTCODE SetStaffInfo(CUniStruct<STAFFINFO> vrParameter)
		{
			return Cmd(MSREQ_REPORT_STAFFINFO_SET,vrParameter);
		}*/
		
		public REQUESTCODE SetStaffInfo(STAFFINFO  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_STAFFINFO_SET,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_STAFFINFO_SET,vrParameter);
			}
		}


		/*public REQUESTCODE SetLabInfo(CUniStruct<LABINFO> vrParameter)
		{
			return Cmd(MSREQ_REPORT_LABINFO_SET,vrParameter);
		}*/
		
		public REQUESTCODE SetLabInfo(LABINFO  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_LABINFO_SET,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_LABINFO_SET,vrParameter);
			}
		}


		/*public REQUESTCODE SetLabAllCost(CUniStruct<LABALLCOST> vrParameter)
		{
			return Cmd(MSREQ_REPORT_LABALLCOST_SET,vrParameter);
		}*/
		
		public REQUESTCODE SetLabAllCost(LABALLCOST  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_LABALLCOST_SET,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_LABALLCOST_SET,vrParameter);
			}
		}


		/*public REQUESTCODE SetLabSummary(CUniStruct<LABSUMMARY> vrParameter)
		{
			return Cmd(MSREQ_REPORT_LABSUMMARY_SET,vrParameter);
		}*/
		
		public REQUESTCODE SetLabSummary(LABSUMMARY  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_LABSUMMARY_SET,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_LABSUMMARY_SET,vrParameter);
			}
		}


		/*public REQUESTCODE SetLabSummary2(CUniStruct<LABSUMMARY2> vrParameter)
		{
			return Cmd(MSREQ_REPORT_LABSUMMARY2_SET,vrParameter);
		}*/
		
		public REQUESTCODE SetLabSummary2(LABSUMMARY2  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPORT_LABSUMMARY2_SET,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPORT_LABSUMMARY2_SET,vrParameter);
			}
		}

	
	}
	#endregion PRReport部分

	#region PRSystem部分
	/*系统管理*/
	public partial class PRSystem:PRModule
	{
		public PRSystem(UniDCom _UniDCom):base(_UniDCom)
		{
			m_nModule = SYSTEM_BASE;
		}
		
		/*public REQUESTCODE CfgGet(CUniStruct<CFGREQ> vrParameter,out CUniStructArray<CFGINFO> vrResult)
		{
			return Cmd(MSREQ_SYSTEM_CFGGET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE CfgGet(CFGREQ  vrParameter,out CFGINFO[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_SYSTEM_CFGGET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_SYSTEM_CFGGET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE CfgSet(CUniStruct<CFGINFO> vrParameter)
		{
			return Cmd(MSREQ_SYSTEM_CFGSET,vrParameter);
		}*/
		
		public REQUESTCODE CfgSet(CFGINFO  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_SYSTEM_CFGSET,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_SYSTEM_CFGSET,vrParameter);
			}
		}


		/*public REQUESTCODE CreditTypeGet(CUniStruct<CREDITTYPEREQ> vrParameter,out CUniStructArray<CREDITTYPE> vrResult)
		{
			return Cmd(MSREQ_CREDITTYPE_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE CreditTypeGet(CREDITTYPEREQ  vrParameter,out CREDITTYPE[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CREDITTYPE_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CREDITTYPE_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE CreditTypeSet(CUniStruct<CREDITTYPE> vrParameter)
		{
			return Cmd(MSREQ_CREDITTYPE_SET,vrParameter);
		}*/
		
		public REQUESTCODE CreditTypeSet(CREDITTYPE  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CREDITTYPE_SET,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CREDITTYPE_SET,vrParameter);
			}
		}


		/*public REQUESTCODE CreditScoreGet(CUniStruct<CREDITSCOREREQ> vrParameter,out CUniStructArray<CREDITSCORE> vrResult)
		{
			return Cmd(MSREQ_CREDITSCORE_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE CreditScoreGet(CREDITSCOREREQ  vrParameter,out CREDITSCORE[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CREDITSCORE_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CREDITSCORE_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE CreditScoreSet(CUniStruct<CREDITSCORE> vrParameter,out CUniStruct<CREDITSCORE> vrResult)
		{
			return Cmd(MSREQ_CREDITSCORE_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE CreditScoreSet(CREDITSCORE  vrParameter,out CREDITSCORE  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CREDITSCORE_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CREDITSCORE_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE MyCreditScoreGet(CUniStruct<MYCREDITSCOREREQ> vrParameter,out CUniStructArray<MYCREDITSCORE> vrResult)
		{
			return Cmd(MSREQ_MYCREDITSCORE_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE MyCreditScoreGet(MYCREDITSCOREREQ  vrParameter,out MYCREDITSCORE[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_MYCREDITSCORE_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_MYCREDITSCORE_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE AdminCreditDo(CUniStruct<ADMINCREDIT> vrParameter)
		{
			return Cmd(MSREQ_ADMINCREDIT_DO,vrParameter);
		}*/
		
		public REQUESTCODE AdminCreditDo(ADMINCREDIT  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ADMINCREDIT_DO,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ADMINCREDIT_DO,vrParameter);
			}
		}


		/*public REQUESTCODE CreditRecGet(CUniStruct<CREDITRECREQ> vrParameter,out CUniStructArray<CREDITREC> vrResult)
		{
			return Cmd(MSREQ_CREDITREC_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE CreditRecGet(CREDITRECREQ  vrParameter,out CREDITREC[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CREDITREC_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CREDITREC_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE SysFuncGet(CUniStruct<SYSFUNCREQ> vrParameter,out CUniStructArray<SYSFUNC> vrResult)
		{
			return Cmd(MSREQ_SYSFUNC_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE SysFuncGet(SYSFUNCREQ  vrParameter,out SYSFUNC[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_SYSFUNC_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_SYSFUNC_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE SysFuncRuleGet(CUniStruct<SYSFUNCRULEREQ> vrParameter,out CUniStructArray<SYSFUNCRULE> vrResult)
		{
			return Cmd(MSREQ_SYSFUNCRULE_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE SysFuncRuleGet(SYSFUNCRULEREQ  vrParameter,out SYSFUNCRULE[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_SYSFUNCRULE_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_SYSFUNCRULE_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE SysFuncRuleSet(CUniStruct<SYSFUNCRULE> vrParameter,out CUniStruct<SYSFUNCRULE> vrResult)
		{
			return Cmd(MSREQ_SYSFUNCRULE_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE SysFuncRuleSet(SYSFUNCRULE  vrParameter,out SYSFUNCRULE  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_SYSFUNCRULE_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_SYSFUNCRULE_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE SFRoleGet(CUniStruct<SFROLEINFOREQ> vrParameter,out CUniStructArray<SFROLEINFO> vrResult)
		{
			return Cmd(MSREQ_SFROLE_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE SFRoleGet(SFROLEINFOREQ  vrParameter,out SFROLEINFO[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_SFROLE_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_SFROLE_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE SFRoleApply(CUniStruct<SFROLEINFO> vrParameter)
		{
			return Cmd(MSREQ_SFROLE_APPLY,vrParameter);
		}*/
		
		public REQUESTCODE SFRoleApply(SFROLEINFO  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_SFROLE_APPLY,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_SFROLE_APPLY,vrParameter);
			}
		}


		/*public REQUESTCODE SFRoleCheck(CUniStruct<SFROLEINFO> vrParameter)
		{
			return Cmd(MSREQ_SFROLE_CHECK,vrParameter);
		}*/
		
		public REQUESTCODE SFRoleCheck(SFROLEINFO  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_SFROLE_CHECK,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_SFROLE_CHECK,vrParameter);
			}
		}


		/*public REQUESTCODE GetCodingTable(CUniStruct<CODINGTABLEREQ> vrParameter,out CUniStructArray<CODINGTABLE> vrResult)
		{
			return Cmd(MSREQ_CODINGTABLE_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetCodingTable(CODINGTABLEREQ  vrParameter,out CODINGTABLE[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CODINGTABLE_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CODINGTABLE_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE SetCodingTable(CUniStruct<CODINGTABLE> vrParameter)
		{
			return Cmd(MSREQ_CODINGTABLE_SET,vrParameter);
		}*/
		
		public REQUESTCODE SetCodingTable(CODINGTABLE  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CODINGTABLE_SET,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CODINGTABLE_SET,vrParameter);
			}
		}


		/*public REQUESTCODE DelCodingTable(CUniStruct<CODINGTABLE> vrParameter)
		{
			return Cmd(MSREQ_CODINGTABLE_DEL,vrParameter);
		}*/
		
		public REQUESTCODE DelCodingTable(CODINGTABLE  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_CODINGTABLE_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_CODINGTABLE_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE GetMultiLanLib(CUniStruct<MULTILANLIBREQ> vrParameter,out CUniStructArray<UNIMULTILANLIB> vrResult)
		{
			return Cmd(MSREQ_MULTILANLIB_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetMultiLanLib(MULTILANLIBREQ  vrParameter,out UNIMULTILANLIB[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_MULTILANLIB_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_MULTILANLIB_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE SystemRefresh(CUniStruct<SYSREFRESHREQ> vrParameter)
		{
			return Cmd(MSREQ_SYSTEM_REFRESH,vrParameter);
		}*/
		
		public REQUESTCODE SystemRefresh(SYSREFRESHREQ  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_SYSTEM_REFRESH,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_SYSTEM_REFRESH,vrParameter);
			}
		}

	
	}
	#endregion PRSystem部分

	#region PRAssert部分
	/*资产管理*/
	public partial class PRAssert:PRModule
	{
		public PRAssert(UniDCom _UniDCom):base(_UniDCom)
		{
			m_nModule = ASSERT_BASE;
		}
		
		/*public REQUESTCODE AssertGet(CUniStruct<ASSERTREQ> vrParameter,out CUniStructArray<UNIASSERT> vrResult)
		{
			return Cmd(MSREQ_ASSERT_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE AssertGet(ASSERTREQ  vrParameter,out UNIASSERT[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ASSERT_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ASSERT_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE AssertWarehousing(CUniStruct<UNIASSERT> vrParameter,out CUniStruct<UNIASSERT> vrResult)
		{
			return Cmd(MSREQ_ASSERT_WAREHOUSING,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE AssertWarehousing(UNIASSERT  vrParameter,out UNIASSERT  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ASSERT_WAREHOUSING,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ASSERT_WAREHOUSING,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE AssertRFIDBind(CUniStruct<RFIDBIND> vrParameter)
		{
			return Cmd(MSREQ_ASSERT_RFIDBIND,vrParameter);
		}*/
		
		public REQUESTCODE AssertRFIDBind(RFIDBIND  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ASSERT_RFIDBIND,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ASSERT_RFIDBIND,vrParameter);
			}
		}


		/*public REQUESTCODE AssertChgRoom(CUniStruct<ROOMCHG> vrParameter)
		{
			return Cmd(MSREQ_ASSERT_CHGROOM,vrParameter);
		}*/
		
		public REQUESTCODE AssertChgRoom(ROOMCHG  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ASSERT_CHGROOM,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ASSERT_CHGROOM,vrParameter);
			}
		}


		/*public REQUESTCODE AssertChgKeeper(CUniStruct<KEEPERCHG> vrParameter)
		{
			return Cmd(MSREQ_ASSERT_CHGKEEPER,vrParameter);
		}*/
		
		public REQUESTCODE AssertChgKeeper(KEEPERCHG  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ASSERT_CHGKEEPER,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ASSERT_CHGKEEPER,vrParameter);
			}
		}


		/*public REQUESTCODE AssertDel(CUniStruct<UNIASSERT> vrParameter)
		{
			return Cmd(MSREQ_ASSERT_DEL,vrParameter);
		}*/
		
		public REQUESTCODE AssertDel(UNIASSERT  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ASSERT_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ASSERT_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE StockTakingGet(CUniStruct<STOCKTAKINGREQ> vrParameter,out CUniStructArray<STOCKTAKING> vrResult)
		{
			return Cmd(MSREQ_STOCKTAKING_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE StockTakingGet(STOCKTAKINGREQ  vrParameter,out STOCKTAKING[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_STOCKTAKING_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_STOCKTAKING_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE StockTakingDo(CUniStruct<STOCKTAKING> vrParameter,out CUniStruct<STOCKTAKING> vrResult)
		{
			return Cmd(MSREQ_STOCKTAKING_DO,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE StockTakingDo(STOCKTAKING  vrParameter,out STOCKTAKING  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_STOCKTAKING_DO,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_STOCKTAKING_DO,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE STDetailGet(CUniStruct<STDETAILREQ> vrParameter,out CUniStructArray<STDETAIL> vrResult)
		{
			return Cmd(MSREQ_STDETAIL_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE STDetailGet(STDETAILREQ  vrParameter,out STDETAIL[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_STDETAIL_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_STDETAIL_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE STDetailDo(CUniStruct<STDETAIL> vrParameter)
		{
			return Cmd(MSREQ_STDETAIL_DO,vrParameter);
		}*/
		
		public REQUESTCODE STDetailDo(STDETAIL  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_STDETAIL_DO,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_STDETAIL_DO,vrParameter);
			}
		}


		/*public REQUESTCODE OutOfSericeGet(CUniStruct<OUTOFSERVICEREQ> vrParameter,out CUniStructArray<OUTOFSERVICE> vrResult)
		{
			return Cmd(MSREQ_OUTOFSERVICE_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE OutOfSericeGet(OUTOFSERVICEREQ  vrParameter,out OUTOFSERVICE[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_OUTOFSERVICE_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_OUTOFSERVICE_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE OutOfSericeApply(CUniStruct<OUTOFSERVICE> vrParameter,out CUniStruct<OUTOFSERVICE> vrResult)
		{
			return Cmd(MSREQ_OUTOFSERVICE_APPLY,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE OutOfSericeApply(OUTOFSERVICE  vrParameter,out OUTOFSERVICE  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_OUTOFSERVICE_APPLY,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_OUTOFSERVICE_APPLY,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE OutOfSericeApprove(CUniStruct<OUTOFSERVICE> vrParameter,out CUniStruct<OUTOFSERVICE> vrResult)
		{
			return Cmd(MSREQ_OUTOFSERVICE_APPROVE,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE OutOfSericeApprove(OUTOFSERVICE  vrParameter,out OUTOFSERVICE  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_OUTOFSERVICE_APPROVE,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_OUTOFSERVICE_APPROVE,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE OutOfSericeDel(CUniStruct<OUTOFSERVICE> vrParameter)
		{
			return Cmd(MSREQ_OUTOFSERVICE_DEL,vrParameter);
		}*/
		
		public REQUESTCODE OutOfSericeDel(OUTOFSERVICE  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_OUTOFSERVICE_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_OUTOFSERVICE_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE OOSDetailGet(CUniStruct<OOSDETAILREQ> vrParameter,out CUniStructArray<OOSDETAIL> vrResult)
		{
			return Cmd(MSREQ_OOSDETAIL_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE OOSDetailGet(OOSDETAILREQ  vrParameter,out OOSDETAIL[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_OOSDETAIL_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_OOSDETAIL_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE RepareRecGet(CUniStruct<DEVDAMAGERECREQ> vrParameter,out CUniStructArray<DEVDAMAGEREC> vrResult)
		{
			return Cmd(MSREQ_REPAIRREC_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE RepareRecGet(DEVDAMAGERECREQ  vrParameter,out DEVDAMAGEREC[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPAIRREC_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPAIRREC_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE RepareApply(CUniStruct<REPAIRAPPLY> vrParameter,out CUniStruct<REPAIRAPPLY> vrResult)
		{
			return Cmd(MSREQ_REPAIR_APPLY,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE RepareApply(REPAIRAPPLY  vrParameter,out REPAIRAPPLY  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPAIR_APPLY,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPAIR_APPLY,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE RepareOver(CUniStruct<REPAIROVER> vrParameter,out CUniStruct<REPAIROVER> vrResult)
		{
			return Cmd(MSREQ_REPAIR_OVER,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE RepareOver(REPAIROVER  vrParameter,out REPAIROVER  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPAIR_OVER,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPAIR_OVER,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE RepareCancel(CUniStruct<REPAIRCANCEL> vrParameter)
		{
			return Cmd(MSREQ_REPAIR_CANCEL,vrParameter);
		}*/
		
		public REQUESTCODE RepareCancel(REPAIRCANCEL  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_REPAIR_CANCEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_REPAIR_CANCEL,vrParameter);
			}
		}


		/*public REQUESTCODE GetCompany(CUniStruct<COMPANYREQ> vrParameter,out CUniStructArray<UNICOMPANY> vrResult)
		{
			return Cmd(MSREQ_COMPANY_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetCompany(COMPANYREQ  vrParameter,out UNICOMPANY[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_COMPANY_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_COMPANY_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE SetCompany(CUniStruct<UNICOMPANY> vrParameter,out CUniStruct<UNICOMPANY> vrResult)
		{
			return Cmd(MSREQ_COMPANY_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE SetCompany(UNICOMPANY  vrParameter,out UNICOMPANY  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_COMPANY_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_COMPANY_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DelCompany(CUniStruct<UNICOMPANY> vrParameter)
		{
			return Cmd(MSREQ_COMPANY_DEL,vrParameter);
		}*/
		
		public REQUESTCODE DelCompany(UNICOMPANY  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_COMPANY_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_COMPANY_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE AssertLogGet(CUniStruct<ASSERTLOGREQ> vrParameter,out CUniStructArray<ASSERTLOG> vrResult)
		{
			return Cmd(MSREQ_ASSERTLOG_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE AssertLogGet(ASSERTLOGREQ  vrParameter,out ASSERTLOG[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ASSERTLOG_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ASSERTLOG_GET,vrParameter,out vrResult);
			}
		}

	
	}
	#endregion PRAssert部分

	#region PRAttendance部分
	/*考勤管理*/
	public partial class PRAttendance:PRModule
	{
		public PRAttendance(UniDCom _UniDCom):base(_UniDCom)
		{
			m_nModule = ATTENDANCE_BASE;
		}
		
		/*public REQUESTCODE GetAttendRule(CUniStruct<ATTENDRULEREQ> vrParameter,out CUniStructArray<ATTENDRULE> vrResult)
		{
			return Cmd(MSREQ_ATTENDRULE_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetAttendRule(ATTENDRULEREQ  vrParameter,out ATTENDRULE[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ATTENDRULE_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ATTENDRULE_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE SetAttendRule(CUniStruct<ATTENDRULE> vrParameter,out CUniStruct<ATTENDRULE> vrResult)
		{
			return Cmd(MSREQ_ATTENDRULE_SET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE SetAttendRule(ATTENDRULE  vrParameter,out ATTENDRULE  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ATTENDRULE_SET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ATTENDRULE_SET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE DelAttendRule(CUniStruct<ATTENDRULE> vrParameter)
		{
			return Cmd(MSREQ_ATTENDRULE_DEL,vrParameter);
		}*/
		
		public REQUESTCODE DelAttendRule(ATTENDRULE  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ATTENDRULE_DEL,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ATTENDRULE_DEL,vrParameter);
			}
		}


		/*public REQUESTCODE GetAttendRec(CUniStruct<ATTENDRECREQ> vrParameter,out CUniStructArray<ATTENDREC> vrResult)
		{
			return Cmd(MSREQ_ATTENDREC_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetAttendRec(ATTENDRECREQ  vrParameter,out ATTENDREC[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ATTENDREC_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ATTENDREC_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetAttendStat(CUniStruct<ATTENDSTATREQ> vrParameter,out CUniStructArray<ATTENDSTAT> vrResult)
		{
			return Cmd(MSREQ_ATTENDSTAT_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetAttendStat(ATTENDSTATREQ  vrParameter,out ATTENDSTAT[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ATTENDSTAT_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ATTENDSTAT_GET,vrParameter,out vrResult);
			}
		}

	
	}
	#endregion PRAttendance部分

	#region PRSubSys部分
	/*子系统通信接口*/
	public partial class PRSubSys:PRModule
	{
		public PRSubSys(UniDCom _UniDCom):base(_UniDCom)
		{
			m_nModule = SUBSYS_BASE;
		}
		
		/*public REQUESTCODE SubSysLogin(CUniStruct<SUBSYSLOGINREQ> vrParameter,out CUniStruct<SUBSYSLOGINRES> vrResult)
		{
			return Cmd(MSREQ_SUBSYS_LOGIN,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE SubSysLogin(SUBSYSLOGINREQ  vrParameter,out SUBSYSLOGINRES  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_SUBSYS_LOGIN,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_SUBSYS_LOGIN,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE SubSysLogout(CUniStruct<SUBSYSLOGOUTREQ> vrParameter)
		{
			return Cmd(MSREQ_SUBSYS_LOGOUT,vrParameter);
		}*/
		
		public REQUESTCODE SubSysLogout(SUBSYSLOGOUTREQ  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_SUBSYS_LOGOUT,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_SUBSYS_LOGOUT,vrParameter);
			}
		}


		/*public REQUESTCODE UploadICUseRec(CUniStructArray<ICUSERECUP> vrParameter)
		{
			return Cmd(MSREQ_ICUSEREC_UPLOAD,vrParameter);
		}*/
		
		public REQUESTCODE UploadICUseRec(ICUSERECUP[]  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_ICUSEREC_UPLOAD,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_ICUSEREC_UPLOAD,vrParameter);
			}
		}


		/*public REQUESTCODE UploadPrintRec(CUniStructArray<PRINTRECUP> vrParameter)
		{
			return Cmd(MSREQ_PRINTREC_UPLOAD,vrParameter);
		}*/
		
		public REQUESTCODE UploadPrintRec(PRINTRECUP[]  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_PRINTREC_UPLOAD,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_PRINTREC_UPLOAD,vrParameter);
			}
		}


		/*public REQUESTCODE UploadBookOverdueRec(CUniStructArray<BOOKOVERDUERECUP> vrParameter)
		{
			return Cmd(MSREQ_BOOKOVERDUEREC_UPLOAD,vrParameter);
		}*/
		
		public REQUESTCODE UploadBookOverdueRec(BOOKOVERDUERECUP[]  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_BOOKOVERDUEREC_UPLOAD,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_BOOKOVERDUEREC_UPLOAD,vrParameter);
			}
		}


		/*public REQUESTCODE UploadBreachRec(CUniStructArray<BREACHRECUP> vrParameter)
		{
			return Cmd(MSREQ_BREACHREC_UPLOAD,vrParameter);
		}*/
		
		public REQUESTCODE UploadBreachRec(BREACHRECUP[]  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_BREACHREC_UPLOAD,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_BREACHREC_UPLOAD,vrParameter);
			}
		}

	
	}
	#endregion PRSubSys部分

	#region PRSubIC部分
	/*IC空间子系统接口*/
	public partial class PRSubIC:PRModule
	{
		public PRSubIC(UniDCom _UniDCom):base(_UniDCom)
		{
			m_nModule = SUBIC_BASE;
		}
		
		/*public REQUESTCODE GetStudyRoomStat(CUniStruct<STUDYROOMSTATREQ> vrParameter,out CUniStructArray<STUDYROOMSTAT> vrResult)
		{
			return Cmd(MSREQ_STUDYROOMSTAT_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetStudyRoomStat(STUDYROOMSTATREQ  vrParameter,out STUDYROOMSTAT[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_STUDYROOMSTAT_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_STUDYROOMSTAT_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE GetSeatStat(CUniStruct<SEATSTATREQ> vrParameter,out CUniStructArray<SEATSTAT> vrResult)
		{
			return Cmd(MSREQ_SEATSTAT_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetSeatStat(SEATSTATREQ  vrParameter,out SEATSTAT[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_SEATSTAT_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_SEATSTAT_GET,vrParameter,out vrResult);
			}
		}

	
	}
	#endregion PRSubIC部分

	#region PRSubRT部分
	/*科研实验子系统接口*/
	public partial class PRSubRT:PRModule
	{
		public PRSubRT(UniDCom _UniDCom):base(_UniDCom)
		{
			m_nModule = SUBRT_BASE;
		}
		
		/*public REQUESTCODE GetRTData(CUniStruct<RTDATAREQ> vrParameter,out CUniStructArray<RTDATA> vrResult)
		{
			return Cmd(MSREQ_RTDATA_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE GetRTData(RTDATAREQ  vrParameter,out RTDATA[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_RTDATA_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_RTDATA_GET,vrParameter,out vrResult);
			}
		}

	
	}
	#endregion PRSubRT部分

	#region PRUniSta部分
	/*节点管理*/
	public partial class PRUniSta:PRModule
	{
		public PRUniSta(UniDCom _UniDCom):base(_UniDCom)
		{
			m_nModule = UNISTA_BASE;
		}
		
		/*public REQUESTCODE StaLogin(CUniStruct<STALOGINREQ> vrParameter,out CUniStruct<STALOGINRES> vrResult)
		{
			return Cmd(MSREQ_UNISTA_LOGIN,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE StaLogin(STALOGINREQ  vrParameter,out STALOGINRES  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_UNISTA_LOGIN,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_UNISTA_LOGIN,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE StaLogout(CUniStruct<STALOGOUTREQ> vrParameter)
		{
			return Cmd(MSREQ_UNISTA_LOGOUT,vrParameter);
		}*/
		
		public REQUESTCODE StaLogout(STALOGOUTREQ  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_UNISTA_LOGOUT,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_UNISTA_LOGOUT,vrParameter);
			}
		}


		/*public REQUESTCODE StaHandShake(CUniStruct<HANDSHAKEREQ> vrParameter,out CUniStruct<HANDSHAKERES> vrResult)
		{
			return Cmd(MSREQ_UNISTA_HANDSHAKE,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE StaHandShake(HANDSHAKEREQ  vrParameter,out HANDSHAKERES  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_UNISTA_HANDSHAKE,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_UNISTA_HANDSHAKE,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE UploadModMoni(CUniStructArray<MODMONIUP> vrParameter)
		{
			return Cmd(MSREQ_MODMONI_UPLOAD,vrParameter);
		}*/
		
		public REQUESTCODE UploadModMoni(MODMONIUP[]  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_MODMONI_UPLOAD,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_MODMONI_UPLOAD,vrParameter);
			}
		}


		/*public REQUESTCODE UploadMoniIndex(CUniStructArray<MONINDEXUP> vrParameter)
		{
			return Cmd(MSREQ_MONIINDEX_UPLOAD,vrParameter);
		}*/
		
		public REQUESTCODE UploadMoniIndex(MONINDEXUP[]  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_MONIINDEX_UPLOAD,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_MONIINDEX_UPLOAD,vrParameter);
			}
		}


		/*public REQUESTCODE UploadMoniRec(CUniStructArray<MONIRECUP> vrParameter)
		{
			return Cmd(MSREQ_MONIREC_UPLOAD,vrParameter);
		}*/
		
		public REQUESTCODE UploadMoniRec(MONIRECUP[]  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_MONIREC_UPLOAD,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_MONIREC_UPLOAD,vrParameter);
			}
		}

	
	}
	#endregion PRUniSta部分

	#region PRUniMoni部分
	/*自动监控*/
	public partial class PRUniMoni:PRModule
	{
		public PRUniMoni(UniDCom _UniDCom):base(_UniDCom)
		{
			m_nModule = UNIMONI_BASE;
		}
		
		/*public REQUESTCODE SubMoniFromSrv(out CUniStructArray<MONINDEX> vrResult)
		{
			return Cmd(MSREQ_SUBMONI_FROMSRV,out vrResult);
		}*/
		
		public REQUESTCODE SubMoniFromSrv(out MONINDEX[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_SUBMONI_FROMSRV,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_SUBMONI_FROMSRV,out vrResult);
			}
		}


		/*public REQUESTCODE SubMoniToSrv(CUniStructArray<MONINDEX> vrParameter)
		{
			return Cmd(MSREQ_SUBMONI_TOSRV,vrParameter);
		}*/
		
		public REQUESTCODE SubMoniToSrv(MONINDEX[]  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_SUBMONI_TOSRV,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_SUBMONI_TOSRV,vrParameter);
			}
		}


		/*public REQUESTCODE OneMoniChg(CUniStruct<MONINDEX> vrParameter)
		{
			return Cmd(MSREQ_SUBMONI_ONEINDEXCHG,vrParameter);
		}*/
		
		public REQUESTCODE OneMoniChg(MONINDEX  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_SUBMONI_ONEINDEXCHG,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_SUBMONI_ONEINDEXCHG,vrParameter);
			}
		}


		/*public REQUESTCODE MoniChg(CUniStructArray<MONINDEX> vrParameter)
		{
			return Cmd(MSREQ_SUBMONI_INDEXCHG,vrParameter);
		}*/
		
		public REQUESTCODE MoniChg(MONINDEX[]  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_SUBMONI_INDEXCHG,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_SUBMONI_INDEXCHG,vrParameter);
			}
		}


		/*public REQUESTCODE MoniGet(CUniStruct<MONIREQ> vrParameter,out CUniStructArray<MODMONI> vrResult)
		{
			return Cmd(MSREQ_MONI_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE MoniGet(MONIREQ  vrParameter,out MODMONI[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_MONI_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_MONI_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE MoniRecGet(CUniStruct<MONIRECREQ> vrParameter,out CUniStructArray<MONIREC> vrResult)
		{
			return Cmd(MSREQ_MONIREC_GET,vrParameter,out vrResult);
		}*/
		
		public REQUESTCODE MoniRecGet(MONIRECREQ  vrParameter,out MONIREC[]  vrResult)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_MONIREC_GET,vrParameter,out vrResult);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_MONIREC_GET,vrParameter,out vrResult);
			}
		}


		/*public REQUESTCODE MoniDealErr(CUniStruct<MONIDEALERR> vrParameter)
		{
			return Cmd(MSREQ_MONI_DEALERR,vrParameter);
		}*/
		
		public REQUESTCODE MoniDealErr(MONIDEALERR  vrParameter)
		{
			if(OnError != null)
			{
				REQUESTCODE ret = UTCmd(MSREQ_MONI_DEALERR,vrParameter);
				if(ret != REQUESTCODE.EXECUTE_SUCCESS)
				{
					OnError(this,ret);
				}
				return ret;
			}else{
				return UTCmd(MSREQ_MONI_DEALERR,vrParameter);
			}
		}

	
	}
	#endregion PRUniMoni部分
}
//