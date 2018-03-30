
/* ------------------------------------------------------
  ��Ȩ��Ϣ�� ����������Ϣ�������޹�˾��2008-2011
  �� �� ���� UniInterface.h
  ����ʱ�䣺 2008.08.25
  ���������� ���屾ϵͳ��ģ�������ģ���ͨ�Žӿ�
  ��    �ߣ� �κ���
  --------------------------------------------------------------- 
*/
using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using UniStruct;
using UniWebLib;

/// <summary>
/// UniService ��ժҪ˵��
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public partial class UniService : UniBaseService
{
    public UniService () {

        //���ʹ����Ƶ��������ȡ��ע�������� 
        //InitializeComponent(); 
    }

	[WebMethod (Description = "��ȡ����Ա�б�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_Get(out uint code,out string Message ,ADMINREQ vrParameter,out UNIADMIN[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.Get(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ��ǰ����Ա���������Ϣ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_GetIF(out uint code,out string Message ,IFPARAMREQ vrParameter,out IFPARAM[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.GetIF(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ����Ա������־")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_GetAdminLog(out uint code,out string Message ,ADMINLOGREQ vrParameter,out ADMINLOG[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.GetAdminLog(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_GetManRoom(out uint code,out string Message ,MANROOMREQ vrParameter,out MANROOM[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.GetManRoom(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�½����޸Ĺ���Ա")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_Set(out uint code,out string Message ,UNIADMIN vrParameter,out UNIADMIN vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.Set(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "���浱ǰ����Ա���������Ϣ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_SaveIF(out uint code,out string Message ,IFPARAM vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.SaveIF(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "���IP��ַ������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_ClearIPBlackList(out uint code,out string Message ,IPBLACKLIST vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.ClearIPBlackList(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�޸Ĺ���Ա����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_AdminChgPasswd(out uint code,out string Message ,ADMINCHGPASSWD vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.AdminChgPasswd(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "���ù�����Ա��Ϣ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_StaffSet(out uint code,out string Message ,STAFFINFO vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.StaffSet(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�޸Ĺ���Ա����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_Del(out uint code,out string Message ,UNIADMIN vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.Del(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�����Ϣ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_AdminCheckGet(out uint code,out string Message ,CHECKREQ vrParameter,out CHECKINFO[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.AdminCheckGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����Ա���")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_AdminCheck(out uint code,out string Message ,ADMINCHECK vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.AdminCheck(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�����Ϣ��־")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_AdminCheckLogGet(out uint code,out string Message ,CHECKLOGREQ vrParameter,out CHECKLOG[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.AdminCheckLogGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����Ա��¼")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_Login(out uint code,out string Message ,ADMINLOGINREQ vrParameter,out ADMINLOGINRES vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.Login(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�ֻ���¼")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_MobileStaLogin(out uint code,out string Message ,MOBILELOGINREQ vrParameter,out ADMINLOGINRES vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.MobileStaLogin(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����Ա�˳�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_Logout(out uint code,out string Message ,ADMINLOGOUTREQ vrParameter,out ADMINLOGOUTRES vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.Logout(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡϵͳ֧�ֵ�UID")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_UIDInfoGet(out uint code,out string Message ,UIDINFOREQ vrParameter,out UIDINFO[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.UIDInfoGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ����Ȩ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_OPPrivGet(out uint code,out string Message ,OPPRIVREQ vrParameter,out OPPRIV[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.OPPrivGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�����޸Ĳ���Ȩ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_OPPrivSet(out uint code,out string Message ,OPPRIV vrParameter,out OPPRIV vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.OPPrivSet(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ������Ȩ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_OPPrivDel(out uint code,out string Message ,OPPRIV vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.OPPrivDel(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�û���ɫ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_UserRoleGet(out uint code,out string Message ,USERROLEREQ vrParameter,out USERROLE[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.UserRoleGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�����û���ɫ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_UserRoleSet(out uint code,out string Message ,USERROLE vrParameter,out USERROLE vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.UserRoleSet(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ���û���ɫ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_UserRoleDel(out uint code,out string Message ,USERROLE vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.UserRoleDel(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�ͻ�������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_CltPWGet(out uint code,out string Message ,out CLTPASSWD[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.CltPWGet( out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�������ÿͻ�������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_CltPWSet(out uint code,out string Message ,CLTPASSWD vrParameter,out CLTPASSWD vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.CltPWSet(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡˢ�±�־")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_AdminRefreshFlagGet(out uint code,out string Message ,REFRESHFLAGREQ vrParameter,out REFRESHFLAGINFO[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.AdminRefreshFlagGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�ڼ���")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_HolidDayGet(out uint code,out string Message ,HOLIDAYREQ vrParameter,out UNIHOLIDAY[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.HolidDayGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "���ýڼ���")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_HolidDaySet(out uint code,out string Message ,UNIHOLIDAY vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.HolidDaySet(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ���ڼ���")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_HolidDayDel(out uint code,out string Message ,UNIHOLIDAY vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.HolidDayDel(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����˽�����ü��ĳ��ֵ�Ƿ���ڣ������ڷ��سɹ������ڷ��ش���")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_CheckExist(out uint code,out string Message ,CHECKEXISTREQ vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.CheckExist(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡĳ���ֶε����ֵ����֧����ֵ���ֶΣ�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_GetMaxValue(out uint code,out string Message ,MAXVALUEREQ vrParameter,out MAXVALUE vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.GetMaxValue(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡϵͳ����ͳ����Ϣ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_GetBasicStatInfo(out uint code,out string Message ,out BASICSTAT vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.GetBasicStatInfo( out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ����������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_CheckTypeGet(out uint code,out string Message ,CHECKTYPEREQ vrParameter,out CHECKTYPE[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.CheckTypeGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�޸�������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_CheckTypeSet(out uint code,out string Message ,CHECKTYPE vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.CheckTypeSet(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ���������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_GetUserFeedback(out uint code,out string Message ,USERFEEDBACKREQ vrParameter,out USERFEEDBACK[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.GetUserFeedback(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�û��������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_DoUserFeedback(out uint code,out string Message ,USERFEEDBACK vrParameter,out USERFEEDBACK vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.DoUserFeedback(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�ظ��û��������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_ReplyUserFeedback(out uint code,out string Message ,USERFEEDBACK vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.ReplyUserFeedback(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�����������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_ServiceTypeGet(out uint code,out string Message ,SERVICETYPEREQ vrParameter,out UNISERVICETYPE[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.ServiceTypeGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�޸ķ������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_ServiceTypeSet(out uint code,out string Message ,UNISERVICETYPE vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.ServiceTypeSet(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ����ͶƱ��Ϣ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_GetPollOnLine(out uint code,out string Message ,POLLONLINEREQ vrParameter,out POLLONLINE[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.GetPollOnLine(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�½��޸�����ͶƱ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_SetPollOnLine(out uint code,out string Message ,POLLONLINE vrParameter,out POLLONLINE vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.SetPollOnLine(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��������ͶƱ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Admin_VotePollOnLine(out uint code,out string Message ,POLLVOTE[] vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Admin.VotePollOnLine(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡվ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Station_GetStation(out uint code,out string Message ,STATIONREQ vrParameter,out UNISTATION[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Station.GetStation(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����վ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Station_SetStation(out uint code,out string Message ,UNISTATION vrParameter,out UNISTATION vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Station.SetStation(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ��վ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Station_DelStation(out uint code,out string Message ,UNISTATION vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Station.DelStation(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�����б�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_DeptGet(out uint code,out string Message ,DEPTREQ vrParameter,out UNIDEPT[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.DeptGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�޸Ĳ�������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_DeptSet(out uint code,out string Message ,UNIDEPT vrParameter,out UNIDEPT vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.DeptSet(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_DeptDel(out uint code,out string Message ,UNIDEPT vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.DeptDel(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡУ���б�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_CampusGet(out uint code,out string Message ,CAMPUSREQ vrParameter,out UNICAMPUS[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.CampusGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�޸�У������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_CampusSet(out uint code,out string Message ,UNICAMPUS vrParameter,out UNICAMPUS vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.CampusSet(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ��У��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_CampusDel(out uint code,out string Message ,UNICAMPUS vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.CampusDel(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�༶�б�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_ClassGet(out uint code,out string Message ,CLASSREQ vrParameter,out UNICLASS[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.ClassGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�޸İ༶����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_ClassSet(out uint code,out string Message ,UNICLASS vrParameter,out UNICLASS vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.ClassSet(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ���༶")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_ClassDel(out uint code,out string Message ,UNICLASS vrParameter,out UNICLASS vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.ClassDel(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�ʻ��б�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_Get(out uint code,out string Message ,ACCREQ vrParameter,out UNIACCOUNT[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.Get(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ��ʦ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_TutorGet(out uint code,out string Message ,TUTORREQ vrParameter,out UNITUTOR[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.TutorGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ��ʦ��ѧ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_TutorStudentGet(out uint code,out string Message ,TUTORSTUDENTREQ vrParameter,out TUTORSTUDENT[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.TutorStudentGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ��չ�����Ա��Ϣ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_ExtIdentAccGet(out uint code,out string Message ,EXTIDENTACCREQ vrParameter,out EXTIDENTACC[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.ExtIdentAccGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�û���Ϣ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_AccInfoGet(out uint code,out string Message ,ACCINFOREQ vrParameter,out UNIACCOUNT vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.AccInfoGet(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�޸��ʻ�����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_Set(out uint code,out string Message ,UNIACCOUNT vrParameter,out UNIACCOUNT vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.Set(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "������չ�����Ա")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_ExtIdentAccSet(out uint code,out string Message ,EXTIDENTACC vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.ExtIdentAccSet(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "���õ�ʦ��ѧ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_TutorStudentSet(out uint code,out string Message ,TUTORSTUDENT vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.TutorStudentSet(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ʦ���ѧ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_TutorStudentCheck(out uint code,out string Message ,TUTORSTUDENTCHECK vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.TutorStudentCheck(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ���ʻ�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_Del(out uint code,out string Message ,UNIACCOUNT vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.Del(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ����չ�����Ա")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_ExtIdentAccDel(out uint code,out string Message ,EXTIDENTACC vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.ExtIdentAccDel(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ����ʦ��ѧ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_TutorStudentDel(out uint code,out string Message ,TUTORSTUDENT vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.TutorStudentDel(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�û���֤")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_AccCheck(out uint code,out string Message ,ACCCHECKREQ vrParameter,out UNIACCOUNT vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.AccCheck(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "���˿��������ѻ�ʱ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_Deposit(out uint code,out string Message ,UNIDEPOSIT vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.Deposit(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "֧�������ύ������ˮ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_Payment(out uint code,out string Message ,UNIPAYMENT vrParameter,out UNIPAYMENT vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.Payment(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ������Ա�����໥֪ͨ����Ϣ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_AccNoticeGet(out uint code,out string Message ,NOTICEREQ vrParameter,out NOTICEINFO[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.AccNoticeGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ȷ��֪ͨ��Ϣ���յ�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_AccNoticeAffirm(out uint code,out string Message ,NOTICEAFFIRM vrParameter,out NOTICEAFFIRM vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.AccNoticeAffirm(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�����Ϣ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_AccExtInfoGet(out uint code,out string Message ,ACCREQ vrParameter,out UNIACCEXTINFO[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.AccExtInfoGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡרҵ�б�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_MajorGet(out uint code,out string Message ,MAJORREQ vrParameter,out UNIMAJOR[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.MajorGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�޸�רҵ����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_MajorSet(out uint code,out string Message ,UNIMAJOR vrParameter,out UNIMAJOR vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.MajorSet(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ��רҵ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_MajorDel(out uint code,out string Message ,UNIMAJOR vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.MajorDel(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡʵ�����ݼ�¼")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_TestDataGet(out uint code,out string Message ,TESTDATAREQ vrParameter,out UNITESTDATA[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.TestDataGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�ϴ�ʵ������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_TestDataUpload(out uint code,out string Message ,UNITESTDATA vrParameter,out UNITESTDATA vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.TestDataUpload(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�޸�ʵ������״̬")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_TestDataChgStat(out uint code,out string Message ,UNITESTDATA vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.TestDataChgStat(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����Ա�ϴ�ʵ������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_TestDataAdminUpload(out uint code,out string Message ,ADMINTESTDATA vrParameter,out UNITESTDATA vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.TestDataAdminUpload(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "������Ӳ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_CloudDiskOpen(out uint code,out string Message ,CLOUDDISKREQ vrParameter,out CLOUDDISK[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.CloudDiskOpen(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�����ļ�������Ӳ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_CloudDiskSave(out uint code,out string Message ,CLOUDDISK vrParameter,out CLOUDDISK vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.CloudDiskSave(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "������Ӳ��ɾ���ļ�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_CloudDiskDel(out uint code,out string Message ,CLOUDDISK vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.CloudDiskDel(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����Ӳ��ʹ��ͳ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_CloudDiskStat(out uint code,out string Message ,CDISKSTATREQ vrParameter,out CDISKSTAT vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.CloudDiskStat(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�ον�ʦ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_TeacherGet(out uint code,out string Message ,UNITEACHERREQ vrParameter,out UNITEACHER[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.TeacherGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�����ον�ʦ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_TeacherSet(out uint code,out string Message ,UNITEACHER vrParameter,out UNITEACHER vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.TeacherSet(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ���ον�ʦ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_TeacherDel(out uint code,out string Message ,UNITEACHER vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.TeacherDel(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ��ǰ�û�ʹ�����(���ƿ���̨ˢ����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Account_GetUserCurInfo(out uint code,out string Message ,USERCURINFOREQ vrParameter,out USERCURINFO vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Account.GetUserCurInfo(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡʵ�����б�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_LabGet(out uint code,out string Message ,LABREQ vrParameter,out UNILAB[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.LabGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡʵ����ȫ��Ϣ�б�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_FullLabGet(out uint code,out string Message ,FULLLABREQ vrParameter,out FULLLAB[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.FullLabGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����ʵ������Ϣ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_LabSet(out uint code,out string Message ,UNILAB vrParameter,out UNILAB vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.LabSet(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ��ʵ����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_LabDel(out uint code,out string Message ,UNILAB vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.LabDel(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�豸�б�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_Get(out uint code,out string Message ,DEVREQ vrParameter,out UNIDEVICE[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.Get(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ��ԤԼ�豸�б�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_GetDevForResv(out uint code,out string Message ,DEVFORRESVREQ vrParameter,out DEVFORRESV[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.GetDevForResv(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ��ԤԼ�豸�б�(������)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_GetDevKindForResv(out uint code,out string Message ,DEVKINDFORRESVREQ vrParameter,out DEVKINDFORRESV[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.GetDevKindForResv(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ����ЧԤԼ�����豸�б�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_ResvUsableDevGet(out uint code,out string Message ,RESVUSABLEDEVREQ vrParameter,out RESVUSABLEDEV[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.ResvUsableDevGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�豸ԤԼ״̬")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_GetDevResvStat(out uint code,out string Message ,DEVRESVSTATREQ vrParameter,out DEVRESVSTAT[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.GetDevResvStat(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡʵ����ԤԼ״̬")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_GetLabResvStat(out uint code,out string Message ,LABRESVSTATREQ vrParameter,out LABRESVSTAT[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.GetLabResvStat(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�豸����ԤԼ״̬")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_GetDevLongResvStat(out uint code,out string Message ,DEVLONGRESVSTATREQ vrParameter,out DEVLONGRESVSTAT[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.GetDevLongResvStat(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�ɳ���ԤԼ�豸�б�(������)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_GetDevKindForLongResv(out uint code,out string Message ,DEVKINDFORLONGRESVREQ vrParameter,out DEVKINDFORRESV[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.GetDevKindForLongResv(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ����ԤԼ״̬")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_GetRoomResvStat(out uint code,out string Message ,ROOMRESVSTATREQ vrParameter,out ROOMRESVSTAT[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.GetRoomResvStat(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�豸ʹ�÷ѵľ��ѷ������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_DevFARGet(out uint code,out string Message ,DEVFARREQ vrParameter,out DEVFAR[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.DevFARGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�豸���ñ�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_DevCfgGet(out uint code,out string Message ,DEVCFGREQ vrParameter,out DEVCFG[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.DevCfgGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�豸��������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_DevCfgKindGet(out uint code,out string Message ,DEVCFGKINDREQ vrParameter,out DEVCFGKIND[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.DevCfgKindGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ��ԤԼ�豸�б�(������)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_GetRoomForResv(out uint code,out string Message ,ROOMFORRESVREQ vrParameter,out ROOMFORRESV[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.GetRoomForResv(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�������ԤԼ״̬")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_GetRGResvStat(out uint code,out string Message ,RGRESVSTATREQ vrParameter,out RGRESVSTAT[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.GetRGResvStat(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�����豸��Ϣ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_Set(out uint code,out string Message ,UNIDEVICE vrParameter,out UNIDEVICE vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.Set(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�����豸ֵ��Ա")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_AttendantSet(out uint code,out string Message ,DEVATTENDANT vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.AttendantSet(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�����豸ʹ�÷ѵľ��ѷ������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_DevFARSet(out uint code,out string Message ,DEVFAR vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.DevFARSet(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�����豸���ñ�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_DevCfgSet(out uint code,out string Message ,DEVCFG vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.DevCfgSet(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�ϴ����ܼ����λ״̬")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_UploadSeatDetectStat(out uint code,out string Message ,SEATDETECTSTAT[] vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.UploadSeatDetectStat(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ���豸")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_Del(out uint code,out string Message ,UNIDEVICE vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.Del(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�����豸���ñ�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_DevCfgDel(out uint code,out string Message ,DEVCFG vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.DevCfgDel(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����Ա�˹������豸ʹ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_DevManUse(out uint code,out string Message ,DEVMANUSE vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.DevManUse(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�ͻ���ע��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_DevRegist(out uint code,out string Message ,DEVREGISTREQ vrParameter,out DEVREGISTRES vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.DevRegist(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�û��ӿͻ��˵�¼")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_DevLogon(out uint code,out string Message ,DEVLOGONREQ vrParameter,out DEVLOGONRES vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.DevLogon(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�û��ӿͻ��˲�ѯʹ����Ϣ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_DevQuery(out uint code,out string Message ,DEVQUERYREQ vrParameter,out UNIACCTINFO vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.DevQuery(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�û��ӿͻ���ע��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_DevLogout(out uint code,out string Message ,DEVLOGOUTREQ vrParameter,out DEVLOGOUTRES vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.DevLogout(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ʹ���еĿͻ��������ʱͨ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_DevHandShake(out uint code,out string Message ,DEVHANDSHAKEREQ vrParameter,out DEVHANDSHAKERES vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.DevHandShake(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�ϴ����������Ϣ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_PCSWUpload(out uint code,out string Message ,PCPROGRAM vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.PCSWUpload(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "������֤���Ƿ�������ʣ�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_CheckURL(out uint code,out string Message ,URLCHECKINFO vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.CheckURL(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�ͻ����޸�����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_ClientChgPw(out uint code,out string Message ,CLTCHGPWINFO vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.ClientChgPw(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����ʼ����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_ProgramBegin(out uint code,out string Message ,PCPROGRAM vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.ProgramBegin(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����ֹͣ����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_ProgramEnd(out uint code,out string Message ,PROGEND[] vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.ProgramEnd(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����˿����豸��Ҫ���豸ִ��ĳ����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_DevCtrl(out uint code,out string Message ,DEVCTRLINFO vrParameter,out DEVCTRLINFO vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.DevCtrl(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�����������ģʽ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_URLCtrl(out uint code,out string Message ,CTRLREQ vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.URLCtrl(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����������ģʽ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_SWCtrl(out uint code,out string Message ,CTRLREQ vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.SWCtrl(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�������г���")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_GetRunApp(out uint code,out string Message ,RUNAPPREQ vrParameter,out RUNAPP[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.GetRunApp(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�ϴ����������Ϣ����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_PCSWUploadEnd(out uint code,out string Message ,SWUPLOADEND vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.PCSWUploadEnd(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����豸")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_DevLoan(out uint code,out string Message ,DEVLOANREQ vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.DevLoan(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�黹�豸")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_DevReturn(out uint code,out string Message ,DEVRETURNREQ vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.DevReturn(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�豸�𻵼�¼")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_DevDamageRecGet(out uint code,out string Message ,DEVDAMAGERECREQ vrParameter,out DEVDAMAGEREC[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.DevDamageRecGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�豸ά�޴���")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_DeviceRepair(out uint code,out string Message ,DEVDAMAGEREC vrParameter,out DEVDAMAGEREC vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.DeviceRepair(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�豸�Կ��ƽ���ķ���")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_DevCtrlRes(out uint code,out string Message ,DEVCTRLINFO vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.DevCtrlRes(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�豸��������б�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_DevClsGet(out uint code,out string Message ,DEVCLSREQ vrParameter,out UNIDEVCLS[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.DevClsGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�����豸���������Ϣ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_DevClsSet(out uint code,out string Message ,UNIDEVCLS vrParameter,out UNIDEVCLS vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.DevClsSet(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ���豸�������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_DevClsDel(out uint code,out string Message ,UNIDEVCLS vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.DevClsDel(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�豸��������б�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_DevKindGet(out uint code,out string Message ,DEVKINDREQ vrParameter,out UNIDEVKIND[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.DevKindGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�����豸���������Ϣ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_DevKindSet(out uint code,out string Message ,UNIDEVKIND vrParameter,out UNIDEVKIND vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.DevKindSet(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ���豸�������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_DevKindDel(out uint code,out string Message ,UNIDEVKIND vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.DevKindDel(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ¥����Ϣ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_BuildingGet(out uint code,out string Message ,BUILDINGREQ vrParameter,out UNIBUILDING[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.BuildingGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����¥��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_BuildingSet(out uint code,out string Message ,UNIBUILDING vrParameter,out UNIBUILDING vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.BuildingSet(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ��¥��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_BuildingDel(out uint code,out string Message ,UNIBUILDING vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.BuildingDel(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ������Ϣ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_RoomGet(out uint code,out string Message ,ROOMREQ vrParameter,out UNIROOM[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.RoomGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "���÷���")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_RoomSet(out uint code,out string Message ,UNIROOM vrParameter,out UNIROOM vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.RoomSet(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_RoomDel(out uint code,out string Message ,UNIROOM vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.RoomDel(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����˿��Ʒ��䣬Ҫ�󷿼�ִ��ĳ����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_RoomCtrl(out uint code,out string Message ,ROOMCTRLINFO vrParameter,out ROOMCTRLINFO vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.RoomCtrl(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�û��ɽ��뷿��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_GetPermitRoom(out uint code,out string Message ,PERMITROOMREQ vrParameter,out PERMITROOMINFO[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.GetPermitRoom(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ���������Ϣ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_GetRoomCtrlInfo(out uint code,out string Message ,ROOMCTRLREQ vrParameter,out UNIDOORCTRL[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.GetRoomCtrlInfo(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ����������Ϣ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_FullRoomGet(out uint code,out string Message ,FULLROOMREQ vrParameter,out FULLROOM[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.FullRoomGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ����������Ϣ�������������ѡ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_BasicRoomGet(out uint code,out string Message ,BASICROOMREQ vrParameter,out BASICROOM[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.BasicRoomGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡͨ������Ϣ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_ChannelGateGet(out uint code,out string Message ,CHANNELGATEREQ vrParameter,out UNICHANNELGATE[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.ChannelGateGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����ͨ����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_ChannelGateSet(out uint code,out string Message ,UNICHANNELGATE vrParameter,out UNICHANNELGATE vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.ChannelGateSet(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ��ͨ����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_ChannelGateDel(out uint code,out string Message ,UNICHANNELGATE vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.ChannelGateDel(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����˿���ͨ���ţ�Ҫ��ͨ����ִ��ĳ����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_ChannelGateCtrl(out uint code,out string Message ,CHANNELGATECTRLINFO vrParameter,out CHANNELGATECTRLINFO vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.ChannelGateCtrl(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_RoomGroupGet(out uint code,out string Message ,ROOMGROUPREQ vrParameter,out ROOMGROUP[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.RoomGroupGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "���÷������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_RoomGroupSet(out uint code,out string Message ,ROOMGROUP vrParameter,out ROOMGROUP vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.RoomGroupSet(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ���������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_RoomGroupDel(out uint code,out string Message ,ROOMGROUP vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.RoomGroupDel(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�豸�����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_DevMonitorGet(out uint code,out string Message ,DEVMONITORREQ vrParameter,out DEVMONITOR[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.DevMonitorGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�����豸�����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_DevMonitorSet(out uint code,out string Message ,DEVMONITOR vrParameter,out DEVMONITOR vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.DevMonitorSet(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ���豸�����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_DevMonitorDel(out uint code,out string Message ,DEVMONITOR vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.DevMonitorDel(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ��������豸�Ķ�Ӧ��ϵ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_MonDevGet(out uint code,out string Message ,MONDEVREQ vrParameter,out MONDEV[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.MonDevGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "���ü�������豸�Ķ�Ӧ��ϵ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_MonDevSet(out uint code,out string Message ,MONDEV vrParameter,out MONDEV vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.MonDevSet(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ����������豸�Ķ�Ӧ��ϵ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_MonDevDel(out uint code,out string Message ,MONDEV vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.MonDevDel(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�豸����ʱ���")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_DevOpenRuleGet(out uint code,out string Message ,DEVOPENRULEREQ vrParameter,out DEVOPENRULE[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.DevOpenRuleGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�����豸����ʱ���")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_DevOpenRuleSet(out uint code,out string Message ,DEVOPENRULE vrParameter,out DEVOPENRULE vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.DevOpenRuleSet(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ���豸����ʱ���")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_DevOpenRuleDel(out uint code,out string Message ,DEVOPENRULE vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.DevOpenRuleDel(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�����鿪��ʱ���")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_GroupOpenRuleSet(out uint code,out string Message ,CHANGEGROUPOPENRULE vrParameter,out CHANGEGROUPOPENRULE vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.GroupOpenRuleSet(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ���鿪��ʱ���")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_GroupOpenRuleDel(out uint code,out string Message ,CHANGEGROUPOPENRULE vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.GroupOpenRuleDel(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�鿪��ʱ���")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_GroupOpenRuleGet(out uint code,out string Message ,GROUPOPENRULEREQ vrParameter,out GROUPOPENRULE[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.GroupOpenRuleGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����ʱ���ڼ俪��ʱ���")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_PeriodOpenRuleSet(out uint code,out string Message ,CHANGEPERIODOPENRULE vrParameter,out CHANGEPERIODOPENRULE vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.PeriodOpenRuleSet(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ��ʱ���ڼ俪��ʱ���")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_PeriodOpenRuleDel(out uint code,out string Message ,CHANGEPERIODOPENRULE vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.PeriodOpenRuleDel(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡʱ���ڼ俪��ʱ���")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_PeriodOpenRuleGet(out uint code,out string Message ,PERIODOPENRULEREQ vrParameter,out PERIODOPENRULE[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.PeriodOpenRuleGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ��ǰ�豸ͳ����Ϣ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_CurDevStat(out uint code,out string Message ,out CURDEVSTAT vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.CurDevStat( out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ��ѧ���豸���ڴ�ͳ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_DevForTeachingStat(out uint code,out string Message ,DEVFORTREQ vrParameter,out DEVSECINFO[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.DevForTeachingStat(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ��ѧ���豸")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_TeachingDevGet(out uint code,out string Message ,TEACHINGDEVREQ vrParameter,out TEACHINGDEV[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.TeachingDevGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�񽱼�¼")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_RewardRecGet(out uint code,out string Message ,REWARDRECREQ vrParameter,out REWARDREC[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.RewardRecGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "���û񽱼�¼")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_RewardRecSet(out uint code,out string Message ,REWARDREC vrParameter,out REWARDREC vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.RewardRecSet(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ���񽱼�¼")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_RewardRecDel(out uint code,out string Message ,REWARDREC vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.RewardRecDel(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ���ü�¼")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_CostRecGet(out uint code,out string Message ,COSTRECREQ vrParameter,out COSTREC[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.CostRecGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "���÷��ü�¼")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_CostRecSet(out uint code,out string Message ,COSTREC vrParameter,out COSTREC vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.CostRecSet(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ�����ü�¼")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Device_CostRecDel(out uint code,out string Message ,COSTREC vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Device.CostRecDel(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�Ž�������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	DoorCtrlSrv_GetDCS(out uint code,out string Message ,DCSREQ vrParameter,out UNIDCS[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.DoorCtrlSrv.GetDCS(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�����Ž�������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	DoorCtrlSrv_SetDCS(out uint code,out string Message ,UNIDCS vrParameter,out UNIDCS vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.DoorCtrlSrv.SetDCS(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ���Ž�������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	DoorCtrlSrv_DelDCS(out uint code,out string Message ,UNIDCS vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.DoorCtrlSrv.DelDCS(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�Ž���������¼")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	DoorCtrlSrv_Login(out uint code,out string Message ,DCSLOGINREQ vrParameter,out DCSLOGINRES vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.DoorCtrlSrv.Login(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�Ž��������˳�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	DoorCtrlSrv_Logout(out uint code,out string Message ,DCSLOGOUTREQ vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.DoorCtrlSrv.Logout(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ʱͨ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	DoorCtrlSrv_Pulse(out uint code,out string Message ,DCSPULSEREQ vrParameter,out DCSPULSERES vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.DoorCtrlSrv.Pulse(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�û����Ž�ˢ������ˢ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	DoorCtrlSrv_DoorCard(out uint code,out string Message ,DOORCARDREQ vrParameter,out DOORCARDRES vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.DoorCtrlSrv.DoorCard(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�û����ֻ�ɨ��ά�뿪��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	DoorCtrlSrv_MobilOpenDoor(out uint code,out string Message ,MOBILEOPENDOORREQ vrParameter,out MOBILEOPENDOORRES vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.DoorCtrlSrv.MobilOpenDoor(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�Ž�������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	DoorCtrlSrv_GetDoorCtrl(out uint code,out string Message ,DOORCTRLREQ vrParameter,out UNIDOORCTRL[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.DoorCtrlSrv.GetDoorCtrl(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�����Ž�������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	DoorCtrlSrv_SetDoorCtrl(out uint code,out string Message ,UNIDOORCTRL vrParameter,out UNIDOORCTRL vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.DoorCtrlSrv.SetDoorCtrl(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ���Ž�������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	DoorCtrlSrv_DelDoorCtrl(out uint code,out string Message ,UNIDOORCTRL vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.DoorCtrlSrv.DelDoorCtrl(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "���Ž�������������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	DoorCtrlSrv_DoorCtrlCmd(out uint code,out string Message ,DOORCTRLCMD vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.DoorCtrlSrv.DoorCtrlCmd(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Group_GetGroup(out uint code,out string Message ,GROUPREQ vrParameter,out UNIGROUP[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Group.GetGroup(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�޸���")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Group_SetGroup(out uint code,out string Message ,UNIGROUP vrParameter,out UNIGROUP vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Group.SetGroup(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Group_DelGroup(out uint code,out string Message ,UNIGROUP vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Group.DelGroup(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "������Ա")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Group_SetGroupMember(out uint code,out string Message ,GROUPMEMBER vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Group.SetGroupMember(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ�����Ա")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Group_DelGroupMember(out uint code,out string Message ,GROUPMEMBER vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Group.DelGroupMember(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ���Ա��ϸ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Group_GetGroupMemDetail(out uint code,out string Message ,GROUPMEMDETAILREQ vrParameter,out GROUPMEMDETAIL[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Group.GetGroupMemDetail(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡԤԼ�б�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_Get(out uint code,out string Message ,RESVREQ vrParameter,out UNIRESERVE[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.Get(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡԤԼ�б�������վ��ʾ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_GetReserveForShow(out uint code,out string Message ,RESVSHOWREQ vrParameter,out RESVSHOW[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.GetReserveForShow(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ��ѧԤԼ�б�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_GetTeachingResv(out uint code,out string Message ,TEACHINGRESVREQ vrParameter,out TEACHINGRESV[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.GetTeachingResv(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ����ʵ��ԤԼ�б�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_GetRTResv(out uint code,out string Message ,RTRESVREQ vrParameter,out RTRESV[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.GetRTResv(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ����ʵ���˵�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_GetRTResvBill(out uint code,out string Message ,RTRESVBILLREQ vrParameter,out RTRESVBILL vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.GetRTResvBill(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ����ʵ���˵�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_GetResvTime(out uint code,out string Message ,RESVTIMEREQ vrParameter,out RESVTIME[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.GetResvTime(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����ԤԼ��Ϣ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_Set(out uint code,out string Message ,UNIRESERVE vrParameter,out UNIRESERVE vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.Set(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�Զ�ԤԼ��ϵͳ�Զ������豸")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_Auto(out uint code,out string Message ,AUTORESVREQ vrParameter,out UNIRESERVE vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.Auto(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�żٵ���(����10��5�գ������壩����9��29�գ���������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_HolidayShift(out uint code,out string Message ,HOLIDAYSHIFT vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.HolidayShift(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����ԤԼС��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_JoinToResvMember(out uint code,out string Message ,RESVMEMBER vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.JoinToResvMember(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�˳�ԤԼС��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_ExitFromResvMember(out uint code,out string Message ,RESVMEMBER vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.ExitFromResvMember(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ȡ��ԤԼǩ������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_CancelResvSign(out uint code,out string Message ,UNIRESERVE vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.CancelResvSign(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�½��޸Ŀ���ʵ��ԤԼ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_SetRTResv(out uint code,out string Message ,RTRESV vrParameter,out RTRESV vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.SetRTResv(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����ʵ��ԤԼ���")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_RTResvCheck(out uint code,out string Message ,RTRESVCHECK vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.RTResvCheck(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����ʵ��Ԥ�շ�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_PrepayRTResv(out uint code,out string Message ,RTPREPAY vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.PrepayRTResv(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����ʵ���˵����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_RTBillCheck(out uint code,out string Message ,RTBILLCHECK vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.RTBillCheck(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����ʵ���˵�����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_RTBillSettle(out uint code,out string Message ,RTBILLSETTLE vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.RTBillSettle(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����ʵ���˵�����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_RTBillReceive(out uint code,out string Message ,RTBILLRECEIVE vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.RTBillReceive(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�������¼ԤԼ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_AnonymousResvSet(out uint code,out string Message ,ANONYMOUSRESV vrParameter,out ANONYMOUSRESV vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.AnonymousResvSet(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����ȫ��ѧ��ԤԼ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_AllUserResvSet(out uint code,out string Message ,ALLUSERRESV vrParameter,out ALLUSERRESV vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.AllUserResvSet(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ��ԤԼ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_Del(out uint code,out string Message ,UNIRESERVE vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.Del(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ������ʵ��ԤԼ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_DelRTResv(out uint code,out string Message ,RTRESV vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.DelRTResv(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ǰ����ԤԼ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_ResvEarlyEnd(out uint code,out string Message ,UNIRESERVE vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.ResvEarlyEnd(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����ԤԼ����ʱ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_ResvChgEndTime(out uint code,out string Message ,RESVENDTIME vrParameter,out RESVENDTIME vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.ResvChgEndTime(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�豸ԤԼ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_DevResvGet(out uint code,out string Message ,DEVRESVREQ vrParameter,out DEVRESVINFO[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.DevResvGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ԤԼ���ú���,���㱾��ԤԼʹ�ú��ʵ�ʷ������á�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_ResvCostAdjust(out uint code,out string Message ,RESVCOSTADJUST vrParameter,out RESVCOSTADJUST vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.ResvCostAdjust(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ԤԼ���ý���,��ʹ���߽��㱾��ԤԼ�����ķ��á�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_ResvCheckOut(out uint code,out string Message ,RESVCHECKOUT vrParameter,out RESVCHECKOUT vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.ResvCheckOut(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡѧ�ڱ�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_GetTerm(out uint code,out string Message ,TERMREQ vrParameter,out UNITERM[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.GetTerm(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����ѧ�ڱ�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_SetTerm(out uint code,out string Message ,UNITERM vrParameter,out UNITERM vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.SetTerm(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ��ѧ�ڱ�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_DelTerm(out uint code,out string Message ,UNITERM vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.DelTerm(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ��Ϣ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_GetClassTimeTable(out uint code,out string Message ,CTSREQ vrParameter,out CLASSTIMETABLE[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.GetClassTimeTable(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "������Ϣ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_SetClassTimeTable(out uint code,out string Message ,CLASSTIMETABLE[] vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.SetClassTimeTable(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�γ�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_GetCourse(out uint code,out string Message ,COURSEREQ vrParameter,out UNICOURSE[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.GetCourse(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "���ÿγ�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_SetCourse(out uint code,out string Message ,UNICOURSE vrParameter,out UNICOURSE vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.SetCourse(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ���γ�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_DelCourse(out uint code,out string Message ,UNICOURSE vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.DelCourse(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡʵ����Ŀ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_GetTestCard(out uint code,out string Message ,TESTCARDREQ vrParameter,out TESTCARD[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.GetTestCard(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����ʵ����Ŀ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_SetTestCard(out uint code,out string Message ,TESTCARD vrParameter,out TESTCARD vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.SetTestCard(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ��ʵ����Ŀ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_DelTestCard(out uint code,out string Message ,TESTCARD vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.DelTestCard(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡʵ��ƻ�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_GetTestPlan(out uint code,out string Message ,TESTPLANREQ vrParameter,out UNITESTPLAN[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.GetTestPlan(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����ʵ��ƻ�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_SetTestPlan(out uint code,out string Message ,UNITESTPLAN vrParameter,out UNITESTPLAN vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.SetTestPlan(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ��ʵ��ƻ�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_DelTestPlan(out uint code,out string Message ,UNITESTPLAN vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.DelTestPlan(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡʵ����Ŀ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_GetTestItem(out uint code,out string Message ,TESTITEMREQ vrParameter,out UNITESTITEM[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.GetTestItem(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����ʵ����Ŀ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_SetTestItem(out uint code,out string Message ,UNITESTITEM vrParameter,out UNITESTITEM vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.SetTestItem(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ��ʵ����Ŀ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_DelTestItem(out uint code,out string Message ,UNITESTITEM vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.DelTestItem(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡʵ����Ŀ������ԤԼ��ϸ��Ϣ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_GetTestItemMemResv(out uint code,out string Message ,TESTITEMMEMRESVREQ vrParameter,out TESTITEMMEMRESV[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.GetTestItemMemResv(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡʵ����Ŀ��ϸ��Ϣ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_GetTestItemInfo(out uint code,out string Message ,TESTITEMINFOREQ vrParameter,out TESTITEMINFO[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.GetTestItemInfo(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ʦ�ύʵ�鱨��ģ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_UploadReportForm(out uint code,out string Message ,REPORTFORMUPLOAD vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.UploadReportForm(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ѧ����ʵ�鱨��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_UploadReport(out uint code,out string Message ,REPORTUPLOAD vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.UploadReport(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����ʵ�鱨��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_CorrectReport(out uint code,out string Message ,REPORTCORRECT vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.CorrectReport(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_GetActivityPlan(out uint code,out string Message ,ACTIVITYPLANREQ vrParameter,out UNIACTIVITYPLAN[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.GetActivityPlan(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "���û����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_SetActivityPlan(out uint code,out string Message ,UNIACTIVITYPLAN vrParameter,out UNIACTIVITYPLAN vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.SetActivityPlan(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ�������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_DelActivityPlan(out uint code,out string Message ,UNIACTIVITYPLAN vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.DelActivityPlan(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ����ŵ���λ�б�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_GetAPSeat(out uint code,out string Message ,APSEATREQ vrParameter,out APSEAT[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.GetAPSeat(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����μӻ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_EnrollActivity(out uint code,out string Message ,ACTIVITYENROLL vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.EnrollActivity(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�˳������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_ExitActivity(out uint code,out string Message ,ACTIVITYEXIT vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.ExitActivity(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����Ա����ǩ����Ա����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_ActivityMemberOffLineSign(out uint code,out string Message ,AOFFLINESIGN vrParameter,out AOFFLINESIGN vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.ActivityMemberOffLineSign(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡԤԼ�����б�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_ResvRuleGet(out uint code,out string Message ,RESVRULEREQ vrParameter,out UNIRESVRULE[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.ResvRuleGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����Ա��ȡԤԼ�����б�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_ResvRuleAdminGet(out uint code,out string Message ,RESVRULEADMINREQ vrParameter,out UNIRESVRULE[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.ResvRuleAdminGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����ԤԼ����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_ResvRuleSet(out uint code,out string Message ,UNIRESVRULE vrParameter,out UNIRESVRULE vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.ResvRuleSet(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ��ԤԼ����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_ResvRuleDel(out uint code,out string Message ,UNIRESVRULE vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.ResvRuleDel(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ����ʵ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_GetResearchTest(out uint code,out string Message ,RESEARCHTESTREQ vrParameter,out RESEARCHTEST[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.GetResearchTest(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�½�/�޸Ŀ���ʵ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_SetResearchTest(out uint code,out string Message ,RESEARCHTEST vrParameter,out RESEARCHTEST vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.SetResearchTest(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ������ʵ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_DelResearchTest(out uint code,out string Message ,RESEARCHTEST vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.DelResearchTest(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "���ÿ���ʵ���Ա")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_SetRTMember(out uint code,out string Message ,RTMEMBER vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.SetRTMember(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ������ʵ���Ա")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_DelRTMember(out uint code,out string Message ,RTMEMBER vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.DelRTMember(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡʵ����Ʒ��Ϣ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_GetSampleInfo(out uint code,out string Message ,SAMPLEINFOREQ vrParameter,out SAMPLEINFO[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.GetSampleInfo(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�½�/�޸�ʵ����Ʒ��Ϣ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_SetSampleInfo(out uint code,out string Message ,SAMPLEINFO vrParameter,out SAMPLEINFO vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.SetSampleInfo(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ��ʵ����Ʒ��Ϣ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_DelSampleInfo(out uint code,out string Message ,SAMPLEINFO vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.DelSampleInfo(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ����ԤԼ�б�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_GetYardResv(out uint code,out string Message ,YARDRESVREQ vrParameter,out YARDRESV[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.GetYardResv(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�½��޸ĳ���ԤԼ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_SetYardResv(out uint code,out string Message ,YARDRESV vrParameter,out YARDRESV vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.SetYardResv(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ������ԤԼ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_DelYardResv(out uint code,out string Message ,YARDRESV vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.DelYardResv(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ����ԤԼ����б�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_GetYardResvCheckInfo(out uint code,out string Message ,YARDRESVCHECKINFOREQ vrParameter,out YARDRESVCHECKINFO[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.GetYardResvCheckInfo(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����ԤԼ���")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_YardResvCheck(out uint code,out string Message ,YARDRESVCHECK vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.YardResvCheck(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ����ԤԼ����б�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_GetResvCheckInfo(out uint code,out string Message ,RESVCHECKINFOREQ vrParameter,out RESVCHECKINFO[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.GetResvCheckInfo(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����ԤԼ���")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_ResvCheck(out uint code,out string Message ,RESVCHECKINFO vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.ResvCheck(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ���ݻ�б�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_GetYardActivity(out uint code,out string Message ,YARDACTIVITYREQ vrParameter,out YARDACTIVITY[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.GetYardActivity(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�½����ݻ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_SetYardActivity(out uint code,out string Message ,YARDACTIVITY vrParameter,out YARDACTIVITY vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.SetYardActivity(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ�����ݻ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_DelYardActivity(out uint code,out string Message ,YARDACTIVITY vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.DelYardActivity(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "������ԤԼ�����豸�������Դ��ͻ�����ز�ִ�е�����ԤԼ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_ThirdResvShareDev(out uint code,out string Message ,THIRDRESVSHAREDEV vrParameter,out THIRDRESVSHAREDEV vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.ThirdResvShareDev(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "������ɾ��ԤԼ�����豸")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_ThirdResvDel(out uint code,out string Message ,THIRDRESVDEL vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.ThirdResvDel(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ������ԤԼ�б�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Reserve_GetThirdResv(out uint code,out string Message ,THIRDRESVREQ vrParameter,out THIRDRESV[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Reserve.GetThirdResv(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ��ط����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Control_GetCtrlClass(out uint code,out string Message ,CTRLCLASSREQ vrParameter,out UNICTRLCLASS[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Control.GetCtrlClass(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�޸ļ�ط����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Control_SetCtrlClass(out uint code,out string Message ,UNICTRLCLASS vrParameter,out UNICTRLCLASS vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Control.SetCtrlClass(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ����ط����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Control_DelCtrlClass(out uint code,out string Message ,UNICTRLCLASS vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Control.DelCtrlClass(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ��ַ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Control_GetCtrlURL(out uint code,out string Message ,CTRLURLREQ vrParameter,out UNICTRLURL[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Control.GetCtrlURL(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�޸���ַ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Control_SetCtrlURL(out uint code,out string Message ,UNICTRLURL vrParameter,out UNICTRLURL vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Control.SetCtrlURL(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ����ַ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Control_DelCtrlURL(out uint code,out string Message ,UNICTRLURL vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Control.DelCtrlURL(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Control_GetCtrlSW(out uint code,out string Message ,CTRLSWREQ vrParameter,out UNICTRLSW[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Control.GetCtrlSW(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�޸������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Control_SetCtrlSW(out uint code,out string Message ,UNICTRLSW vrParameter,out UNICTRLSW vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Control.SetCtrlSW(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ�������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Control_DelCtrlSW(out uint code,out string Message ,UNICTRLSW vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Control.DelCtrlSW(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ���")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Control_GetSoftware(out uint code,out string Message ,SOFTWAREREQ vrParameter,out UNISOFTWARE[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Control.GetSoftware(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�޸����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Control_SetSoftware(out uint code,out string Message ,UNISOFTWARE vrParameter,out UNISOFTWARE vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Control.SetSoftware(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Control_GetProgram(out uint code,out string Message ,PROGRAMREQ vrParameter,out UNIPROGRAM[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Control.GetProgram(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�޸ĳ���")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Control_SetProgram(out uint code,out string Message ,UNIPROGRAM vrParameter,out UNIPROGRAM vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Control.SetProgram(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ��������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Control_GetPCSWinfo(out uint code,out string Message ,PCSWINFOREQ vrParameter,out UNIPCSWINFO[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Control.GetPCSWinfo(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ��������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Control_GetRoomSWinfo(out uint code,out string Message ,ROOMSWINFOREQ vrParameter,out UNIROOMSWINFO[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Control.GetRoomSWinfo(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��¼�ӿڷ�����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	THIRDIF_ThirdLogin(out uint code,out string Message ,THIRDLOGINREQ vrParameter,out THIRDLOGINRES vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.THIRDIF.ThirdLogin(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�˳��ӿڷ�����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	THIRDIF_ThirdLogout(out uint code,out string Message )
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.THIRDIF.ThirdLogout();
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�ʻ��б�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	THIRDIF_ThirdGetAcc(out uint code,out string Message ,THIRDACCREQ vrParameter,out UNIACCOUNT[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.THIRDIF.ThirdGetAcc(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�����ʻ���Ϣ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	THIRDIF_SyncAcc(out uint code,out string Message ,SYNCACCREQ vrParameter,out SYNCACCINFO vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.THIRDIF.SyncAcc(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�շѱ�׼�б�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Fee_Get(out uint code,out string Message ,FEEREQ vrParameter,out UNIFEE[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Fee.Get(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ������Ŀ��Ӧ���豸���շѱ�׼")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Fee_RTDevFeeGet(out uint code,out string Message ,RTDEVFEEREQ vrParameter,out UNIFEE vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Fee.RTDevFeeGet(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ������Ŀ��Ӧ���豸����Ʒ������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Fee_RTDevSampleGet(out uint code,out string Message ,RTDEVSAMPLEREQ vrParameter,out RTDEVSAMPLE[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Fee.RTDevSampleGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�����շѱ�׼��Ϣ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Fee_Set(out uint code,out string Message ,UNIFEE vrParameter,out UNIFEE vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Fee.Set(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ���շѱ�׼")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Fee_Del(out uint code,out string Message ,UNIFEE vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Fee.Del(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ��ʱʹ�ù���")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Fee_FTRuleGet(out uint code,out string Message ,FTRULEREQ vrParameter,out FREETIMERULE[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Fee.FTRuleGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�޸Ļ�ʱʹ�ù���")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Fee_FTRuleSet(out uint code,out string Message ,FREETIMERULE vrParameter,out FREETIMERULE vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Fee.FTRuleSet(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ����ʱʹ�ù���")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Fee_FTRuleDel(out uint code,out string Message ,FREETIMERULE vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Fee.FTRuleDel(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�˵�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Fee_BillGet(out uint code,out string Message ,BILLREQ vrParameter,out UNIBILL[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Fee.BillGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�����˵�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Fee_BillSet(out uint code,out string Message ,UNIBILL vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Fee.BillSet(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�˵��ɷ�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Fee_BillPay(out uint code,out string Message ,BILLPAY vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Fee.BillPay(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ����̨�б�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Console_ConGet(out uint code,out string Message ,CONREQ vrParameter,out UNICONSOLE[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Console.ConGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "���ÿ���̨��Ϣ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Console_ConSet(out uint code,out string Message ,UNICONSOLE vrParameter,out UNICONSOLE vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Console.ConSet(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ������̨")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Console_ConDel(out uint code,out string Message ,UNICONSOLE vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Console.ConDel(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����̨��¼")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Console_ConLogin(out uint code,out string Message ,CONLOGINREQ vrParameter,out CONLOGINRES vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Console.ConLogin(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����̨�˳�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Console_ConLogout(out uint code,out string Message ,CONLOGOUTREQ vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Console.ConLogout(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ʱͨ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Console_ConPulse(out uint code,out string Message ,CONPULSEREQ vrParameter,out CONPULSERES vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Console.ConPulse(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "������̨����Ϣ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Console_ConShowMsg(out uint code,out string Message ,CONMESSAGE vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Console.ConShowMsg(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����̨ˢ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Console_ConUserCard(out uint code,out string Message ,ACCCHECKREQ vrParameter,out CONUSERINFO vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Console.ConUserCard(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����̨��ʦ��¼")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Console_ConTeacherIn(out uint code,out string Message ,ACCCHECKREQ vrParameter,out CONTEACHERINFO vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Console.ConTeacherIn(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����̨ˢ���ϻ�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Console_ConCardForPC(out uint code,out string Message ,CARDFORPCREQ vrParameter,out CARDFORPCRES vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Console.ConCardForPC(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ͨ����ˢ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Console_AutoGateCard(out uint code,out string Message ,AUTOGATECARDREQ vrParameter,out AUTOGATECARDRES vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Console.AutoGateCard(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�ֻ�ɨ���ά��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Console_MobileScan(out uint code,out string Message ,MOBILESCANREQ vrParameter,out MOBILESCANRES vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Console.MobileScan(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����̨ˢ����ʼʹ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Console_ConUserIn(out uint code,out string Message ,CONUSERINREQ vrParameter,out CONUSERINRES vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Console.ConUserIn(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�ֻ�ɨ�迪ʼʹ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Console_MobileUserIn(out uint code,out string Message ,MOBILEUSERINREQ vrParameter,out MOBILEUSERINRES vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Console.MobileUserIn(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�ֻ���ʱ����Լ)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Console_MobileDelay(out uint code,out string Message ,MOBILEDELAYREQ vrParameter,out MOBILEDELAYRES vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Console.MobileDelay(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����̨ˢ������ʹ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Console_ConUserOut(out uint code,out string Message ,CONUSEROUTREQ vrParameter,out CONUSEROUTRES vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Console.ConUserOut(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�ֻ�ɨ��ˢ������ʹ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Console_MobileUserOut(out uint code,out string Message ,MOBILEUSEROUTREQ vrParameter,out MOBILEUSEROUTRES vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Console.MobileUserOut(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�ֻ���¼ǩ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Console_ResvUserComeIn(out uint code,out string Message ,RESVUSERCOMEINREQ vrParameter,out RESVUSERCOMEINRES vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Console.ResvUserComeIn(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�ֻ���¼��ʱ����Լ)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Console_ResvUserDelay(out uint code,out string Message ,RESVUSERDELAYREQ vrParameter,out RESVUSERDELAYRES vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Console.ResvUserDelay(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�ֻ���¼�뿪")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Console_ResvUserGoOut(out uint code,out string Message ,RESVUSERGOOUTREQ vrParameter,out RESVUSERGOOUTRES vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Console.ResvUserGoOut(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡԤԼ��¼")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_ResvRecGet(out uint code,out string Message ,RESVRECREQ vrParameter,out UNIRESVREC[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.ResvRecGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ԤԼ����ͳ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_GetResvKindStat(out uint code,out string Message ,RESVKINDSTATREQ vrParameter,out RESVKINDSTAT[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.GetResvKindStat(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ԤԼ��ʽͳ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_GetResvModeStat(out uint code,out string Message ,RESVMODESTATREQ vrParameter,out RESVMODESTAT[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.GetResvModeStat(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡʹ�ü�¼��ϸ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_GetUseRec(out uint code,out string Message ,USERECREQ vrParameter,out DEVUSEREC[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.GetUseRec(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�Ž�ˢ����¼��ϸ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_GetDoorCardRec(out uint code,out string Message ,DOORCARDRECREQ vrParameter,out DOORCARDREC[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.GetDoorCardRec(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ʹ����ͳ�Ʊ�������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_GetUserStat(out uint code,out string Message ,REPORTREQ vrParameter,out USERSTAT[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.GetUserStat(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ʵ����ͳ�Ʊ�������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_GetLabStat(out uint code,out string Message ,REPORTREQ vrParameter,out LABSTAT[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.GetLabStat(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�豸����ͳ�Ʊ�������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_GetDevKindStat(out uint code,out string Message ,REPORTREQ vrParameter,out DEVKINDSTAT[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.GetDevKindStat(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�豸ͳ�Ʊ�������(CUniStruct[REQEXTINFO],szExtInfo����DEVSTAT�ܺϼ�)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_GetDevStat(out uint code,out string Message ,REPORTREQ vrParameter,out DEVSTAT[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.GetDevStat(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ʵ����Ŀ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_GetTestItemStat(out uint code,out string Message ,TESTITEMSTATREQ vrParameter,out TESTITEMSTAT[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.GetTestItemStat(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�豸����ͳ�Ʊ�������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_GetDevClassStat(out uint code,out string Message ,REPORTREQ vrParameter,out DEVCLASSSTAT[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.GetDevClassStat(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ѧԺʹ��ͳ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_GetDeptStat(out uint code,out string Message ,REPORTREQ vrParameter,out DEPTSTAT[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.GetDeptStat(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "���ʹ��ͳ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_GeIdentStat(out uint code,out string Message ,IDENTSTATREQ vrParameter,out IDENTSTAT[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.GeIdentStat(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ��ѧԤԼ��¼")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_GetTeachingResvRec(out uint code,out string Message ,TEACHINGRESVRECREQ vrParameter,out TEACHINGRESVREC[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.GetTeachingResvRec(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ʵ����(����)ͳ�Ʊ�������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_GetRoomStat(out uint code,out string Message ,REPORTREQ vrParameter,out ROOMSTAT[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.GetRoomStat(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�豸ʹ���ʲ�ѯ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_GetDevUsingRate(out uint code,out string Message ,DEVUSINGRATEREQ vrParameter,out DEVUSINGRATE vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.GetDevUsingRate(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�豸��ʹ��ͳ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_GetDevMonthStat(out uint code,out string Message ,DEVMONTHSTATREQ vrParameter,out DEVMONTHSTAT[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.GetDevMonthStat(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�豸����ʵ��ͳ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_GetRTUseStat(out uint code,out string Message ,RTUSESTATREQ vrParameter,out RTUSESTAT[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.GetRTUseStat(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�豸����ʵ����ϸ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_GetRTUseDetail(out uint code,out string Message ,RTUSEDETAILREQ vrParameter,out RTUSEDETAIL[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.GetRTUseDetail(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�豸����ʵ����÷���ͳ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_GetRTFAStat(out uint code,out string Message ,RTFASTATREQ vrParameter,out RTFASTAT[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.GetRTFAStat(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�豸����ʵ����÷�����ϸ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_GetRTFADetail(out uint code,out string Message ,RTFADETAILREQ vrParameter,out RTFADETAIL[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.GetRTFADetail(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ΥԼͳ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_GetDefaultStat(out uint code,out string Message ,DEFAULTSTATREQ vrParameter,out DEFAULTSTAT[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.GetDefaultStat(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�豸��ʹ���ʲ�ѯ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_GetDevWeekUsingRate(out uint code,out string Message ,DEVWEEKUSINGRATEREQ vrParameter,out DEVWEEKUSINGRATE vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.GetDevWeekUsingRate(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "���ݻ����ͳ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_GetYardActivityStat(out uint code,out string Message ,YARDACTIVITYSTATREQ vrParameter,out YARDACTIVITYSTAT[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.GetYardActivityStat(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ѧ���������豸�� (SJ1)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_GetDevList(out uint code,out string Message ,DEVLISTREQ vrParameter,out DEVLIST[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.GetDevList(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ѧ���������豸�����䶯�����(SJ2)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_GetDevChg(out uint code,out string Message ,DEVCHGREQ vrParameter,out DEVCHG vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.GetDevChg(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "���������豸��(SJ3)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_GetBigDev(out uint code,out string Message ,BIGDEVREQ vrParameter,out BIGDEV[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.GetBigDev(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ʵ����Ŀ��(SJ4)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_GetTestItemReport(out uint code,out string Message ,TESTITEMREPORTREQ vrParameter,out TESTITEMREPORT[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.GetTestItemReport(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ר��ʵ������Ա��(SJ5)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_GetStaffInfo(out uint code,out string Message ,STAFFINFOREQ vrParameter,out STAFFINFO[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.GetStaffInfo(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ʵ���һ��������(SJ6)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_GetLabInfo(out uint code,out string Message ,LABINFOREQ vrParameter,out LABINFO[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.GetLabInfo(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ʵ���Ҿ��������(SJ7)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_GetLabAllCost(out uint code,out string Message ,LABALLCOSTREQ vrParameter,out LABALLCOST vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.GetLabAllCost(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�ߵ�ѧУʵ�����ۺ���Ϣ��(SZ1)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_GetLabSummary(out uint code,out string Message ,LABSUMMARYREQ vrParameter,out LABSUMMARY vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.GetLabSummary(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�ߵ�ѧУʵ�����ۺ���Ϣ��2(SZ2)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_GetLabSummary2(out uint code,out string Message ,LABSUMMARY2REQ vrParameter,out LABSUMMARY2 vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.GetLabSummary2(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ѧ���������豸�� (SJ1)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_SetDevList(out uint code,out string Message ,DEVLIST vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.SetDevList(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ѧ���������豸�����䶯�����(SJ2)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_SetDevChg(out uint code,out string Message ,DEVCHG vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.SetDevChg(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "���������豸��(SJ3)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_SetBigDev(out uint code,out string Message ,BIGDEV vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.SetBigDev(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ʵ����Ŀ��(SJ4)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_SetTestItemReport(out uint code,out string Message ,TESTITEMREPORT vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.SetTestItemReport(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ר��ʵ������Ա��(SJ5)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_SetStaffInfo(out uint code,out string Message ,STAFFINFO vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.SetStaffInfo(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ʵ���һ��������(SJ6)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_SetLabInfo(out uint code,out string Message ,LABINFO vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.SetLabInfo(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ʵ���Ҿ��������(SJ7)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_SetLabAllCost(out uint code,out string Message ,LABALLCOST vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.SetLabAllCost(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�ߵ�ѧУʵ�����ۺ���Ϣ��(SZ1)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_SetLabSummary(out uint code,out string Message ,LABSUMMARY vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.SetLabSummary(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�ߵ�ѧУʵ�����ۺ���Ϣ��2(SZ2)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Report_SetLabSummary2(out uint code,out string Message ,LABSUMMARY2 vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Report.SetLabSummary2(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡϵͳ�����ļ�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	System_CfgGet(out uint code,out string Message ,CFGREQ vrParameter,out CFGINFO[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.System.CfgGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�޸Ĳ�����ϵͳ�����ļ�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	System_CfgSet(out uint code,out string Message ,CFGINFO vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.System.CfgSet(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ��������������б�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	System_CreditTypeGet(out uint code,out string Message ,CREDITTYPEREQ vrParameter,out CREDITTYPE[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.System.CreditTypeGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "���ö������������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	System_CreditTypeSet(out uint code,out string Message ,CREDITTYPE vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.System.CreditTypeSet(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ���÷��������б�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	System_CreditScoreGet(out uint code,out string Message ,CREDITSCOREREQ vrParameter,out CREDITSCORE[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.System.CreditScoreGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�������÷�������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	System_CreditScoreSet(out uint code,out string Message ,CREDITSCORE vrParameter,out CREDITSCORE vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.System.CreditScoreSet(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�ҵ����û���")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	System_MyCreditScoreGet(out uint code,out string Message ,MYCREDITSCOREREQ vrParameter,out MYCREDITSCORE[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.System.MyCreditScoreGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�˹����ù���")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	System_AdminCreditDo(out uint code,out string Message ,ADMINCREDIT vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.System.AdminCreditDo(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ���ü�¼")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	System_CreditRecGet(out uint code,out string Message ,CREDITRECREQ vrParameter,out CREDITREC[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.System.CreditRecGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡϵͳ�����б�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	System_SysFuncGet(out uint code,out string Message ,SYSFUNCREQ vrParameter,out SYSFUNC[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.System.SysFuncGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡϵͳ����ʹ�ù����б�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	System_SysFuncRuleGet(out uint code,out string Message ,SYSFUNCRULEREQ vrParameter,out SYSFUNCRULE[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.System.SysFuncRuleGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�޸�ϵͳ����ʹ�ù���")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	System_SysFuncRuleSet(out uint code,out string Message ,SYSFUNCRULE vrParameter,out SYSFUNCRULE vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.System.SysFuncRuleSet(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡϵͳ�����ʸ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	System_SFRoleGet(out uint code,out string Message ,SFROLEINFOREQ vrParameter,out SFROLEINFO[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.System.SFRoleGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ϵͳ�����ʸ�����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	System_SFRoleApply(out uint code,out string Message ,SFROLEINFO vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.System.SFRoleApply(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ϵͳ�����ʸ����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	System_SFRoleCheck(out uint code,out string Message ,SFROLEINFO vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.System.SFRoleCheck(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	System_GetCodingTable(out uint code,out string Message ,CODINGTABLEREQ vrParameter,out CODINGTABLE[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.System.GetCodingTable(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "���ñ����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	System_SetCodingTable(out uint code,out string Message ,CODINGTABLE vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.System.SetCodingTable(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ�������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	System_DelCodingTable(out uint code,out string Message ,CODINGTABLE vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.System.DelCodingTable(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�����԰�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	System_GetMultiLanLib(out uint code,out string Message ,MULTILANLIBREQ vrParameter,out UNIMULTILANLIB[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.System.GetMultiLanLib(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����ϵͳ״̬")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	System_SystemRefresh(out uint code,out string Message ,SYSREFRESHREQ vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.System.SystemRefresh(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�ʲ��б�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Assert_AssertGet(out uint code,out string Message ,ASSERTREQ vrParameter,out UNIASSERT[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Assert.AssertGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�ʲ����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Assert_AssertWarehousing(out uint code,out string Message ,UNIASSERT vrParameter,out UNIASSERT vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Assert.AssertWarehousing(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "RFID����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Assert_AssertRFIDBind(out uint code,out string Message ,RFIDBIND vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Assert.AssertRFIDBind(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Assert_AssertChgRoom(out uint code,out string Message ,ROOMCHG vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Assert.AssertChgRoom(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�����˱��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Assert_AssertChgKeeper(out uint code,out string Message ,KEEPERCHG vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Assert.AssertChgKeeper(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ���ʲ�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Assert_AssertDel(out uint code,out string Message ,UNIASSERT vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Assert.AssertDel(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�ʲ��̵��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Assert_StockTakingGet(out uint code,out string Message ,STOCKTAKINGREQ vrParameter,out STOCKTAKING[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Assert.StockTakingGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�ʲ��̵�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Assert_StockTakingDo(out uint code,out string Message ,STOCKTAKING vrParameter,out STOCKTAKING vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Assert.StockTakingDo(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�̵��ʲ���ϸ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Assert_STDetailGet(out uint code,out string Message ,STDETAILREQ vrParameter,out STDETAIL[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Assert.STDetailGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�����ʲ��̵�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Assert_STDetailDo(out uint code,out string Message ,STDETAIL vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Assert.STDetailDo(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�豸���ϼ�¼��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Assert_OutOfSericeGet(out uint code,out string Message ,OUTOFSERVICEREQ vrParameter,out OUTOFSERVICE[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Assert.OutOfSericeGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�����豸����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Assert_OutOfSericeApply(out uint code,out string Message ,OUTOFSERVICE vrParameter,out OUTOFSERVICE vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Assert.OutOfSericeApply(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��׼�豸����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Assert_OutOfSericeApprove(out uint code,out string Message ,OUTOFSERVICE vrParameter,out OUTOFSERVICE vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Assert.OutOfSericeApprove(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�����豸��������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Assert_OutOfSericeDel(out uint code,out string Message ,OUTOFSERVICE vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Assert.OutOfSericeDel(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�����豸��ϸ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Assert_OOSDetailGet(out uint code,out string Message ,OOSDETAILREQ vrParameter,out OOSDETAIL[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Assert.OOSDetailGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�豸�����¼")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Assert_RepareRecGet(out uint code,out string Message ,DEVDAMAGERECREQ vrParameter,out DEVDAMAGEREC[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Assert.RepareRecGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�豸����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Assert_RepareApply(out uint code,out string Message ,REPAIRAPPLY vrParameter,out REPAIRAPPLY vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Assert.RepareApply(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�豸�������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Assert_RepareOver(out uint code,out string Message ,REPAIROVER vrParameter,out REPAIROVER vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Assert.RepareOver(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�����豸����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Assert_RepareCancel(out uint code,out string Message ,REPAIRCANCEL vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Assert.RepareCancel(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ��Ӧ��(������������ά��)")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Assert_GetCompany(out uint code,out string Message ,COMPANYREQ vrParameter,out UNICOMPANY[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Assert.GetCompany(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�½��޸Ĺ�Ӧ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Assert_SetCompany(out uint code,out string Message ,UNICOMPANY vrParameter,out UNICOMPANY vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Assert.SetCompany(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ����Ӧ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Assert_DelCompany(out uint code,out string Message ,UNICOMPANY vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Assert.DelCompany(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ�豸��ʷ����")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Assert_AssertLogGet(out uint code,out string Message ,ASSERTLOGREQ vrParameter,out ASSERTLOG[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Assert.AssertLogGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ���ڹ���")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Attendance_GetAttendRule(out uint code,out string Message ,ATTENDRULEREQ vrParameter,out ATTENDRULE[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Attendance.GetAttendRule(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�޸Ŀ��ڹ���")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Attendance_SetAttendRule(out uint code,out string Message ,ATTENDRULE vrParameter,out ATTENDRULE vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Attendance.SetAttendRule(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "ɾ�����ڹ���")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Attendance_DelAttendRule(out uint code,out string Message ,ATTENDRULE vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Attendance.DelAttendRule(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ���ڼ�¼")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Attendance_GetAttendRec(out uint code,out string Message ,ATTENDRECREQ vrParameter,out ATTENDREC[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Attendance.GetAttendRec(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ����ͳ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	Attendance_GetAttendStat(out uint code,out string Message ,ATTENDSTATREQ vrParameter,out ATTENDSTAT[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.Attendance.GetAttendStat(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ϵͳ��¼")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	SubSys_SubSysLogin(out uint code,out string Message ,SUBSYSLOGINREQ vrParameter,out SUBSYSLOGINRES vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.SubSys.SubSysLogin(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ϵͳע��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	SubSys_SubSysLogout(out uint code,out string Message ,SUBSYSLOGOUTREQ vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.SubSys.SubSysLogout(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�ϴ�IC�ռ�ʹ�ü�¼")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	SubSys_UploadICUseRec(out uint code,out string Message ,ICUSERECUP[] vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.SubSys.UploadICUseRec(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�ϴ���ӡ��ӡɨ��ʹ�ü�¼")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	SubSys_UploadPrintRec(out uint code,out string Message ,PRINTRECUP[] vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.SubSys.UploadPrintRec(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�ϴ�ͼ�鳬�ڽɷѼ�¼")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	SubSys_UploadBookOverdueRec(out uint code,out string Message ,BOOKOVERDUERECUP[] vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.SubSys.UploadBookOverdueRec(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�ϴ�ΥԼ��¼")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	SubSys_UploadBreachRec(out uint code,out string Message ,BREACHRECUP[] vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.SubSys.UploadBreachRec(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ���޼䵱ǰ״̬")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	SubIC_GetStudyRoomStat(out uint code,out string Message ,STUDYROOMSTATREQ vrParameter,out STUDYROOMSTAT[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.SubIC.GetStudyRoomStat(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ��λ��ǰ״̬")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	SubIC_GetSeatStat(out uint code,out string Message ,SEATSTATREQ vrParameter,out SEATSTAT[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.SubIC.GetSeatStat(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ȡ����ʵ������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	SubRT_GetRTData(out uint code,out string Message ,RTDATAREQ vrParameter,out RTDATA[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.SubRT.GetRTData(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�ڵ��˳�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	UniSta_StaLogout(out uint code,out string Message ,STALOGOUTREQ vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.UniSta.StaLogout(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�ڵ�����֤��������ʱͨ��")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	UniSta_StaHandShake(out uint code,out string Message ,HANDSHAKEREQ vrParameter,out HANDSHAKERES vrRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.UniSta.StaHandShake(vrParameter, out  vrRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vrRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�ϴ�ģ������Ϣ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	UniSta_UploadModMoni(out uint code,out string Message ,MODMONIUP[] vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.UniSta.UploadModMoni(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�ϴ����ָ����Ϣ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	UniSta_UploadMoniIndex(out uint code,out string Message ,MONINDEXUP[] vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.UniSta.UploadMoniIndex(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�ϴ����ָ���¼")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	UniSta_UploadMoniRec(out uint code,out string Message ,MONIRECUP[] vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.UniSta.UploadMoniRec(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ϵͳ��ȡ�����Ϣȱʡֵ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	UniMoni_SubMoniFromSrv(out uint code,out string Message ,out MONINDEX[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.UniMoni.SubMoniFromSrv( out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "��ϵͳ��������Ϣȱʡֵ��������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	UniMoni_SubMoniToSrv(out uint code,out string Message ,MONINDEX[] vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.UniMoni.SubMoniToSrv(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�����Ϣ״̬�ı�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	UniMoni_OneMoniChg(out uint code,out string Message ,MONINDEX vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.UniMoni.OneMoniChg(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "�����Ϣ״̬�ı�")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	UniMoni_MoniChg(out uint code,out string Message ,MONINDEX[] vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.UniMoni.MoniChg(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����Ա��ȡ��ϸ�����Ϣ")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	UniMoni_MoniGet(out uint code,out string Message ,MONIREQ vrParameter,out MODMONI[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.UniMoni.MoniGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����Ա��ȡ��ϸ�����Ϣ��־")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	UniMoni_MoniRecGet(out uint code,out string Message ,MONIRECREQ vrParameter,out MONIREC[] vtRes)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.UniMoni.MoniRecGet(vrParameter, out  vtRes);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		if ((object) vtRes == null)
		{
			Trace("vrResult == null");
			code = 1;
			return;
		}
		
		code = 0;
		return;
	}
	
	[WebMethod (Description = "����֧�ִ������")]
	//[SoapHeader("soaphead", Direction = SoapHeaderDirection.InOut)]
	[System.Web.Services.Protocols.SoapHeader("soaphead")]
	public void 
	UniMoni_MoniDealErr(out uint code,out string Message ,MONIDEALERR vrParameter)
	{
	    REQUESTCODE uResponse = REQUESTCODE.DBERR_OPENFAIL;
		code = 2;

        uResponse = m_Request.UniMoni.MoniDealErr(vrParameter);
		Message = m_Request.szErrMsg;
		
		if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
			code = 1;
            return;
        }

		
		code = 0;
		return;
	}
	
}
//