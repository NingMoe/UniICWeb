
/* ------------------------------------------------------
  ��Ȩ��Ϣ�� ����������Ϣ�������޹�˾��2008-2011
  �� �� ���� UniInterface.h
  ����ʱ�䣺 2008.08.25
  ���������� ���屾ϵͳ��ģ�������ģ���ͨ�Žӿ�
  ��    �ߣ� �κ���
  --------------------------------------------------------------- 
*/
using System;
using System.Data;
using System.Reflection;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using UniStruct;

/// <summary>
/// Provides a description for an enumerated type.
/// </summary>
[AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field, AllowMultiple = false)]
public sealed class EnumDescriptionAttribute : Attribute
{
	private string m_Description;
	public string Description
	{
		get
		{
			return this.m_Description;
		}
	}
	public EnumDescriptionAttribute(string description)
		: base()
	{
		this.m_Description = description;
	}
}
/// <summary>
/// Provides a static utility object of methods and properties to interact
/// with enumerated types.
/// </summary>
public static class EnumHelper
{
	/// <summary>
	/// Gets the <see cref=��DescriptionAttribute�� /> of an <see cref=��Enum�� /> type value.
	/// </summary>
	public static string GetDescription(Enum value)
	{
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		string description = value.ToString();
		FieldInfo fieldInfo = value.GetType().GetField(description);
		EnumDescriptionAttribute[] attributes = (EnumDescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
		if (attributes != null && attributes.Length > 0)
		{
			description = attributes[0].Description;
		}
		return description;
	}
	
	/// <summary>
	/// Converts the <see cref=��Enum�� /> type to an <see cref=��IList�� /> compatible object. 
	/// </summary>
	public static IList ToList(Type type)
	{
		if (type == null)
		{
			throw new ArgumentNullException("type");
		}
		ArrayList list = new ArrayList();
		Array enumValues = Enum.GetValues(type);
		foreach (Enum value in enumValues)
		{
			list.Add(new KeyValuePair<Enum, string>(value, GetDescription(value)));
		}
		return list;
	}
}

namespace UniWebLib
{

	//ģ���б�
	public delegate void ErrorHandler(PRModule module,REQUESTCODE ret);
	public partial class PRModule
	{
		public ErrorHandler OnError;

		/*����Ա����*/
		public const uint ADMIN_BASE	= (0x010000);

		/*վ�����*/
		public const uint STATION_BASE	= (0x020000);

		/*�ʻ�����*/
		public const uint ACCOUNT_BASE	= (0x030000);

		/*�豸����*/
		public const uint DEVICE_BASE	= (0x040000);

		/*�Ž����ù���*/
		public const uint DOORCTRLSRV_BASE	= (0x050000);

		/*�û������*/
		public const uint GROUP_BASE	= (0x060000);

		/*ԤԼ����*/
		public const uint RESERVE_BASE	= (0x070000);

		/*������Ϸ����*/
		public const uint CONTROL_BASE	= (0x080000);

		/*�������ӿڹ���*/
		public const uint THIRDIF_BASE	= (0x090000);

		/*�շѹ���*/
		public const uint FEE_BASE	= (0x0A0000);

		/*����̨����*/
		public const uint CONSOLE_BASE	= (0x0B0000);

		/*�����ѯͳ��*/
		public const uint REPORT_BASE	= (0x0C0000);

		/*ϵͳ����*/
		public const uint SYSTEM_BASE	= (0x0D0000);

		/*�ʲ�����*/
		public const uint ASSERT_BASE	= (0x0E0000);

		/*���ڹ���*/
		public const uint ATTENDANCE_BASE	= (0x0F0000);

		/*��ϵͳͨ�Žӿ�*/
		public const uint SUBSYS_BASE	= (0x900000);

		/*IC�ռ���ϵͳ�ӿ�*/
		public const uint SUBIC_BASE	= (0x910000);

		/*����ʵ����ϵͳ�ӿ�*/
		public const uint SUBRT_BASE	= (0x920000);

		/*�ڵ����*/
		public const uint UNISTA_BASE	= (0x1000000);

		/*�Զ����*/
		public const uint UNIMONI_BASE	= (0x2000000);

	}
	/*��ʼ�ӿ�--Admin--����Ա����*/
/*��ʼ����*/
	public partial class PRAdmin
	{

		/*��ȡ����Ա�б�,	����:ADMINREQ,	���:UNIADMIN(Array)*/
		public const uint MSREQ_ADMIN_GET 	= 0x10;

		/*��ȡ��ǰ����Ա���������Ϣ,	����:IFPARAMREQ,	���:IFPARAM(Array)*/
		public const uint MSREQ_ADMIN_IFGET 	= 0x11;

		/*��ȡ����Ա������־,	����:ADMINLOGREQ,	���:ADMINLOG(Array)*/
		public const uint MSREQ_ADMINLOG_GET 	= 0x12;

		/*��ȡ������,	����:MANROOMREQ,	���:MANROOM(Array)*/
		public const uint MSREQ_ADMIN_MANROOMGET 	= 0x13;

		/*�½����޸Ĺ���Ա,	����:UNIADMIN,	���:UNIADMIN*/
		public const uint MSREQ_ADMIN_SET 	= 0x20;

		/*���浱ǰ����Ա���������Ϣ,	����:IFPARAM,	���:null*/
		public const uint MSREQ_ADMIN_IFSAVE 	= 0x21;

		/*���IP��ַ������,	����:IPBLACKLIST,	���:null*/
		public const uint MSREQ_IPBLACKLIST_CLEAR 	= 0x22;

		/*�޸Ĺ���Ա����,	����:ADMINCHGPASSWD,	���:null*/
		public const uint MSREQ_ADMIN_CHGPW 	= 0x23;

		/*���ù�����Ա��Ϣ,	����:STAFFINFO,	���:null*/
		public const uint MSREQ_STAFF_SET 	= 0x24;

		/*�޸Ĺ���Ա����,	����:UNIADMIN,	���:null*/
		public const uint MSREQ_ADMIN_DEL 	= 0x30;

		/*��ȡ�����Ϣ,	����:CHECKREQ,	���:CHECKINFO(Array)*/
		public const uint MSREQ_ADMIN_CHECKGET 	= 0x50;

		/*����Ա���,	����:ADMINCHECK,	���:null*/
		public const uint MSREQ_ADMIN_CHECK 	= 0x51;

		/*��ȡ�����Ϣ��־,	����:CHECKLOGREQ,	���:CHECKLOG(Array)*/
		public const uint MSREQ_ADMIN_CHECKLOGGET 	= 0x52;

		/*����Ա��¼,	����:ADMINLOGINREQ,	���:ADMINLOGINRES*/
		public const uint MSREQ_ADMIN_LOGIN 	= 0x110;

		/*����Ա��¼��ȡ����վ��,	����:ADMINLOGINREQ,	���:ADMINLOGINRES*/
		public const uint MSREQ_ADMIN_STALOGIN 	= 0x111;

		/*�ֻ���¼,	����:MOBILELOGINREQ,	���:ADMINLOGINRES*/
		public const uint MSREQ_MOBILE_STALOGIN 	= 0x112;

		/*�ֻ�ҡһҡ��¼,	����:SHAKELOGINREQ,	���:SHAKELOGINRES*/
		public const uint MSREQ_MOBILE_SHAKELOGIN 	= 0x113;

		/*����Ա�˳�,	����:ADMINLOGOUTREQ,	���:ADMINLOGOUTRES*/
		public const uint MSREQ_ADMIN_LOGOUT 	= 0x120;

		/*��ȡϵͳ֧�ֵ�UID,	����:UIDINFOREQ,	���:UIDINFO(Array)*/
		public const uint MSREQ_UIDINFO_GET 	= 0x301;

		/*��ȡ����Ȩ��,	����:OPPRIVREQ,	���:OPPRIV(Array)*/
		public const uint MSREQ_OPPRIV_GET 	= 0x310;

		/*�����޸Ĳ���Ȩ��,	����:OPPRIV,	���:OPPRIV*/
		public const uint MSREQ_OPPRIV_SET 	= 0x320;

		/*ɾ������Ȩ��,	����:OPPRIV,	���:null*/
		public const uint MSREQ_OPPRIV_DEL 	= 0x330;

		/*��ȡ�û���ɫ,	����:USERROLEREQ,	���:USERROLE(Array)*/
		public const uint MSREQ_USERROLE_GET 	= 0x340;

		/*�����û���ɫ,	����:USERROLE,	���:USERROLE*/
		public const uint MSREQ_USERROLE_SET 	= 0x343;

		/*ɾ���û���ɫ,	����:USERROLE,	���:null*/
		public const uint MSREQ_USERROLE_DEL 	= 0x346;

		/*��ȡ�ͻ�������,	����:,	���:CLTPASSWD(Array)*/
		public const uint MSREQ_ADMIN_CLTPWGET 	= 0x410;

		/*�������ÿͻ�������,	����:CLTPASSWD,	���:CLTPASSWD*/
		public const uint MSREQ_ADMIN_CLTPWSET 	= 0x411;

		/*��ȡˢ�±�־,	����:REFRESHFLAGREQ,	���:REFRESHFLAGINFO(Array)*/
		public const uint MSREQ_ADMIN_REFRESHFLAGGET 	= 0x500;

		/*��ȡ�ڼ���,	����:HOLIDAYREQ,	���:UNIHOLIDAY(Array)*/
		public const uint MSREQ_HOLIDAY_GET 	= 0x510;

		/*���ýڼ���,	����:UNIHOLIDAY,	���:null*/
		public const uint MSREQ_HOLIDAY_SET 	= 0x511;

		/*ɾ���ڼ���,	����:UNIHOLIDAY,	���:null*/
		public const uint MSREQ_HOLIDAY_DEL 	= 0x512;

		/*����˽�����ü��ĳ��ֵ�Ƿ���ڣ������ڷ��سɹ������ڷ��ش���,	����:CHECKEXISTREQ,	���:null*/
		public const uint MSREQ_CHECK_EXIST 	= 0x521;

		/*��ȡĳ���ֶε����ֵ����֧����ֵ���ֶΣ�,	����:MAXVALUEREQ,	���:MAXVALUE*/
		public const uint MSREQ_MAXVALUE_GET 	= 0x522;

		/*��ȡϵͳ����ͳ����Ϣ,	����:,	���:BASICSTAT*/
		public const uint MSREQ_BASICSTAT_GET 	= 0x530;

		/*��ȡ����������,	����:CHECKTYPEREQ,	���:CHECKTYPE(Array)*/
		public const uint MSREQ_CHECKTYPE_GET 	= 0x550;

		/*�޸�������,	����:CHECKTYPE,	���:null*/
		public const uint MSREQ_CHECKTYPE_SET 	= 0x555;

		/*��ȡ���������,	����:USERFEEDBACKREQ,	���:USERFEEDBACK(Array)*/
		public const uint MSREQ_USERFEEDBACK_GET 	= 0x561;

		/*�û��������,	����:USERFEEDBACK,	���:USERFEEDBACK*/
		public const uint MSREQ_USERFEEDBACK_DO 	= 0x565;

		/*�ظ��û��������,	����:USERFEEDBACK,	���:null*/
		public const uint MSREQ_USERFEEDBACK_REPLY 	= 0x569;

		/*��ȡ�����������,	����:SERVICETYPEREQ,	���:UNISERVICETYPE(Array)*/
		public const uint MSREQ_SERVICETYPE_GET 	= 0x570;

		/*�޸ķ������,	����:UNISERVICETYPE,	���:null*/
		public const uint MSREQ_SERVICETYPE_SET 	= 0x575;

		/*��ȡ����ͶƱ��Ϣ,	����:POLLONLINEREQ,	���:POLLONLINE(Array)*/
		public const uint MSREQ_POLLONLINE_GET 	= 0x601;

		/*�½��޸�����ͶƱ,	����:POLLONLINE,	���:POLLONLINE*/
		public const uint MSREQ_POLLONLINE_SET 	= 0x605;

		/*��������ͶƱ,	����:POLLVOTE,	���:null*/
		public const uint MSREQ_POLLONLINE_VOTE 	= 0x610;


	}
/*��������*/
	/*�����ӿ�--Admin*/
	/*��ʼ�ӿ�--Station--վ�����*/
/*��ʼ����*/
	public partial class PRStation
	{

		/*��ȡվ��,	����:STATIONREQ,	���:UNISTATION(Array)*/
		public const uint MSREQ_STATION_GET 	= 0x10;

		/*����վ��,	����:UNISTATION,	���:UNISTATION*/
		public const uint MSREQ_STATION_SET 	= 0x20;

		/*ɾ��վ��,	����:UNISTATION,	���:null*/
		public const uint MSREQ_STATION_DEL 	= 0x30;


	}
/*��������*/
	/*�����ӿ�--Station*/
	/*��ʼ�ӿ�--Account--�ʻ�����*/
/*��ʼ����*/
	public partial class PRAccount
	{

		/*��ȡ�����б�,	����:DEPTREQ,	���:UNIDEPT(Array)*/
		public const uint MSREQ_DEPT_GET 	= 0x10;

		/*�޸Ĳ�������,	����:UNIDEPT,	���:UNIDEPT*/
		public const uint MSREQ_DEPT_SET 	= 0x20;

		/*ɾ������,	����:UNIDEPT,	���:null*/
		public const uint MSREQ_DEPT_DEL 	= 0x30;

		/*��ȡУ���б�,	����:CAMPUSREQ,	���:UNICAMPUS(Array)*/
		public const uint MSREQ_CAMPUS_GET 	= 0x40;

		/*�޸�У������,	����:UNICAMPUS,	���:UNICAMPUS*/
		public const uint MSREQ_CAMPUS_SET 	= 0x45;

		/*ɾ��У��,	����:UNICAMPUS,	���:null*/
		public const uint MSREQ_CAMPUS_DEL 	= 0x49;

		/*��ȡ�༶�б�,	����:CLASSREQ,	���:UNICLASS(Array)*/
		public const uint MSREQ_CLS_GET 	= 0x110;

		/*�޸İ༶����,	����:UNICLASS,	���:UNICLASS*/
		public const uint MSREQ_CLS_SET 	= 0x120;

		/*ɾ���༶,	����:UNICLASS,	���:UNICLASS*/
		public const uint MSREQ_CLS_DEL 	= 0x130;

		/*��ȡ�ʻ��б�,	����:ACCREQ,	���:UNIACCOUNT(Array)*/
		public const uint MSREQ_ACC_GET 	= 0x210;

		/*��ȡ��ʦ,	����:TUTORREQ,	���:UNITUTOR(Array)*/
		public const uint MSREQ_TUTOR_GET 	= 0x211;

		/*��ȡ��ʦ��ѧ��,	����:TUTORSTUDENTREQ,	���:TUTORSTUDENT(Array)*/
		public const uint MSREQ_TUTORSTUDENT_GET 	= 0x212;

		/*��ȡ��չ�����Ա��Ϣ,	����:EXTIDENTACCREQ,	���:EXTIDENTACC(Array)*/
		public const uint MSREQ_EXTIDENTACC_GET 	= 0x213;

		/*��ȡ�û���Ϣ,	����:ACCINFOREQ,	���:UNIACCOUNT*/
		public const uint MSREQ_ACCINFO_GET 	= 0x214;

		/*�޸��ʻ�����,	����:UNIACCOUNT,	���:UNIACCOUNT*/
		public const uint MSREQ_ACC_SET 	= 0x220;

		/*������չ�����Ա,	����:EXTIDENTACC,	���:null*/
		public const uint MSREQ_EXTIDENTACC_SET 	= 0x221;

		/*���õ�ʦ��ѧ��,	����:TUTORSTUDENT,	���:null*/
		public const uint MSREQ_TUTORSTUDENT_SET 	= 0x222;

		/*��ʦ���ѧ��,	����:TUTORSTUDENTCHECK,	���:null*/
		public const uint MSREQ_TUTORSTUDENT_CHECK 	= 0x223;

		/*ɾ���ʻ�,	����:UNIACCOUNT,	���:null*/
		public const uint MSREQ_ACC_DEL 	= 0x230;

		/*ɾ����չ�����Ա,	����:EXTIDENTACC,	���:null*/
		public const uint MSREQ_EXTIDENTACC_DEL 	= 0x231;

		/*ɾ����ʦ��ѧ��,	����:TUTORSTUDENT,	���:null*/
		public const uint MSREQ_TUTORSTUDENT_DEL 	= 0x232;

		/*�û���֤,	����:ACCCHECKREQ,	���:UNIACCOUNT*/
		public const uint MSREQ_ACC_CHECK 	= 0x240;

		/*���˿��������ѻ�ʱ,	����:UNIDEPOSIT,	���:null*/
		public const uint MSREQ_ACC_DEPOSIT 	= 0x250;

		/*֧�������ύ������ˮ,	����:UNIPAYMENT,	���:UNIPAYMENT*/
		public const uint MSREQ_ACC_PAYMENT 	= 0x260;

		/*��ȡ������Ա�����໥֪ͨ����Ϣ,	����:NOTICEREQ,	���:NOTICEINFO(Array)*/
		public const uint MSREQ_ACC_NOTICEGET 	= 0x270;

		/*ȷ��֪ͨ��Ϣ���յ�,	����:NOTICEAFFIRM,	���:NOTICEAFFIRM*/
		public const uint MSREQ_ACC_NOTICEAFFIRM 	= 0x271;

		/*��ȡ�����Ϣ,	����:ACCREQ,	���:UNIACCEXTINFO(Array)*/
		public const uint MSREQ_EXTINFO_GET 	= 0x280;

		/*��ȡרҵ�б�,	����:MAJORREQ,	���:UNIMAJOR(Array)*/
		public const uint MSREQ_MAJOR_GET 	= 0x310;

		/*�޸�רҵ����,	����:UNIMAJOR,	���:UNIMAJOR*/
		public const uint MSREQ_MAJOR_SET 	= 0x311;

		/*ɾ��רҵ,	����:UNIMAJOR,	���:null*/
		public const uint MSREQ_MAJOR_DEL 	= 0x312;

		/*��ȡʵ�����ݼ�¼,	����:TESTDATAREQ,	���:UNITESTDATA(Array)*/
		public const uint MSREQ_TESTDATA_GET 	= 0x601;

		/*�ϴ�ʵ������,	����:UNITESTDATA,	���:UNITESTDATA*/
		public const uint MSREQ_TESTDATA_UPLOAD 	= 0x602;

		/*�޸�ʵ������״̬,	����:UNITESTDATA,	���:null*/
		public const uint MSREQ_TESTDATA_CHGSTAT 	= 0x603;

		/*����Ա�ϴ�ʵ������,	����:ADMINTESTDATA,	���:UNITESTDATA*/
		public const uint MSREQ_TESTDATA_ADMINUPLOAD 	= 0x604;

		/*������Ӳ��,	����:CLOUDDISKREQ,	���:CLOUDDISK(Array)*/
		public const uint MSREQ_CLOUDDISK_OPEN 	= 0x608;

		/*�����ļ�������Ӳ��,	����:CLOUDDISK,	���:CLOUDDISK*/
		public const uint MSREQ_CLOUDDISK_SAVE 	= 0x609;

		/*������Ӳ��ɾ���ļ�,	����:CLOUDDISK,	���:null*/
		public const uint MSREQ_CLOUDDISK_DEL 	= 0x60a;

		/*����Ӳ��ʹ��ͳ��,	����:CDISKSTATREQ,	���:CDISKSTAT*/
		public const uint MSREQ_CLOUDDISK_STAT 	= 0x60b;

		/*��ȡ�ον�ʦ,	����:UNITEACHERREQ,	���:UNITEACHER(Array)*/
		public const uint MSREQ_TEACHER_GET 	= 0x611;

		/*�����ον�ʦ,	����:UNITEACHER,	���:UNITEACHER*/
		public const uint MSREQ_TEACHER_SET 	= 0x612;

		/*ɾ���ον�ʦ,	����:UNITEACHER,	���:null*/
		public const uint MSREQ_TEACHER_DEL 	= 0x613;

		/*��ȡ��ǰ�û�ʹ�����(���ƿ���̨ˢ����,	����:USERCURINFOREQ,	���:USERCURINFO*/
		public const uint MSREQ_USERCURINFO_GET 	= 0x620;


	}
/*��������*/
	/*�����ӿ�--Account*/
	/*��ʼ�ӿ�--Device--�豸����*/
/*��ʼ����*/
	public partial class PRDevice
	{

		/*��ȡʵ�����б�,	����:LABREQ,	���:UNILAB(Array)*/
		public const uint MSREQ_LAB_GET 	= 0x10;

		/*��ȡʵ����ȫ��Ϣ�б�,	����:FULLLABREQ,	���:FULLLAB(Array)*/
		public const uint MSREQ_FULLLAB_GET 	= 0x11;

		/*����ʵ������Ϣ,	����:UNILAB,	���:UNILAB*/
		public const uint MSREQ_LAB_SET 	= 0x20;

		/*ɾ��ʵ����,	����:UNILAB,	���:null*/
		public const uint MSREQ_LAB_DEL 	= 0x30;

		/*��ȡ�豸�б�,	����:DEVREQ,	���:UNIDEVICE(Array)*/
		public const uint MSREQ_DEVICE_GET 	= 0x110;

		/*��ȡ��ԤԼ�豸�б�,	����:DEVFORRESVREQ,	���:DEVFORRESV(Array)*/
		public const uint MSREQ_DEVFORRESV_GET 	= 0x111;

		/*��ȡ��ԤԼ�豸�б�(������),	����:DEVKINDFORRESVREQ,	���:DEVKINDFORRESV(Array)*/
		public const uint MSREQ_DEVKINDFORRESV_GET 	= 0x112;

		/*��ȡ����ЧԤԼ�����豸�б�,	����:RESVUSABLEDEVREQ,	���:RESVUSABLEDEV(Array)*/
		public const uint MSREQ_RESVUSABLEDEV_GET 	= 0x113;

		/*��ȡ�豸ԤԼ״̬,	����:DEVRESVSTATREQ,	���:DEVRESVSTAT(Array)*/
		public const uint MSREQ_DEVRESVSTAT_GET 	= 0x114;

		/*��ȡʵ����ԤԼ״̬,	����:LABRESVSTATREQ,	���:LABRESVSTAT(Array)*/
		public const uint MSREQ_LABRESVSTAT_GET 	= 0x115;

		/*��ȡ�豸����ԤԼ״̬,	����:DEVLONGRESVSTATREQ,	���:DEVLONGRESVSTAT(Array)*/
		public const uint MSREQ_DEVLONGRESVSTAT_GET 	= 0x116;

		/*��ȡ�ɳ���ԤԼ�豸�б�(������),	����:DEVKINDFORLONGRESVREQ,	���:DEVKINDFORRESV(Array)*/
		public const uint MSREQ_DEVKINDFORLONGRESV_GET 	= 0x117;

		/*��ȡ����ԤԼ״̬,	����:ROOMRESVSTATREQ,	���:ROOMRESVSTAT(Array)*/
		public const uint MSREQ_ROOMRESVSTAT_GET 	= 0x118;

		/*��ȡ�豸ʹ�÷ѵľ��ѷ������,	����:DEVFARREQ,	���:DEVFAR(Array)*/
		public const uint MSREQ_DEVALLOCATIONRATE_GET 	= 0x119;

		/*��ȡ�豸���ñ�,	����:DEVCFGREQ,	���:DEVCFG(Array)*/
		public const uint MSREQ_DEVCFG_GET 	= 0x11A;

		/*��ȡ�豸��������,	����:DEVCFGKINDREQ,	���:DEVCFGKIND(Array)*/
		public const uint MSREQ_DEVCFGKIND_GET 	= 0x11B;

		/*��ȡ��ԤԼ�豸�б�(������),	����:ROOMFORRESVREQ,	���:ROOMFORRESV(Array)*/
		public const uint MSREQ_ROOMFORRESV_GET 	= 0x11C;

		/*��ȡ�������ԤԼ״̬,	����:RGRESVSTATREQ,	���:RGRESVSTAT(Array)*/
		public const uint MSREQ_RGRESVSTAT_GET 	= 0x11D;

		/*�����豸��Ϣ,	����:UNIDEVICE,	���:UNIDEVICE*/
		public const uint MSREQ_DEVICE_SET 	= 0x120;

		/*�����豸ֵ��Ա,	����:DEVATTENDANT,	���:null*/
		public const uint MSREQ_DEVATTENDANT_SET 	= 0x121;

		/*�����豸ʹ�÷ѵľ��ѷ������,	����:DEVFAR,	���:null*/
		public const uint MSREQ_DEVALLOCATIONRATE_SET 	= 0x122;

		/*�����豸���ñ�,	����:DEVCFG,	���:null*/
		public const uint MSREQ_DEVCFG_SET 	= 0x123;

		/*�ϴ����ܼ����λ״̬,	����:SEATDETECTSTAT,	���:null*/
		public const uint MSREQ_SEATDETECTSTAT_UPLOAD 	= 0x124;

		/*ɾ���豸,	����:UNIDEVICE,	���:null*/
		public const uint MSREQ_DEVICE_DEL 	= 0x130;

		/*�����豸���ñ�,	����:DEVCFG,	���:null*/
		public const uint MSREQ_DEVCFG_DEL 	= 0x131;

		/*����Ա�˹������豸ʹ��,	����:DEVMANUSE,	���:null*/
		public const uint MSREQ_DEVICE_MANUSE 	= 0x132;

		/*�ͻ���ע��,	����:DEVREGISTREQ,	���:DEVREGISTRES*/
		public const uint CSREQ_DEVICE_REGIST 	= 0x140;

		/*�û��ӿͻ��˵�¼,	����:DEVLOGONREQ,	���:DEVLOGONRES*/
		public const uint CSREQ_DEVICE_LOGON 	= 0x141;

		/*�û��ӿͻ��˲�ѯʹ����Ϣ,	����:DEVQUERYREQ,	���:UNIACCTINFO*/
		public const uint CSREQ_DEVICE_QUERY 	= 0x142;

		/*�û��ӿͻ���ע��,	����:DEVLOGOUTREQ,	���:DEVLOGOUTRES*/
		public const uint CSREQ_DEVICE_LOGOUT 	= 0x143;

		/*ʹ���еĿͻ��������ʱͨ��,	����:DEVHANDSHAKEREQ,	���:DEVHANDSHAKERES*/
		public const uint CSREQ_DEVICE_HANDSHAKE 	= 0x144;

		/*�ϴ����������Ϣ,	����:PCPROGRAM,	���:null*/
		public const uint CSREQ_PCSW_UPLOAD 	= 0x145;

		/*������֤���Ƿ�������ʣ�,	����:URLCHECKINFO,	���:null*/
		public const uint CSREQ_URL_CHECK 	= 0x146;

		/*�ͻ����޸�����,	����:CLTCHGPWINFO,	���:null*/
		public const uint CSREQ_CLIENT_CHGPW 	= 0x147;

		/*����ʼ����,	����:PCPROGRAM,	���:null*/
		public const uint CSREQ_PROGRAM_BEGIN 	= 0x148;

		/*����ֹͣ����,	����:PROGEND,	���:null*/
		public const uint CSREQ_PROGRAM_END 	= 0x149;

		/*����˿����豸��Ҫ���豸ִ��ĳ����,	����:DEVCTRLINFO,	���:DEVCTRLINFO*/
		public const uint MSREQ_DEVICE_CTRL 	= 0x150;

		/*�����������ģʽ,	����:CTRLREQ,	���:null*/
		public const uint MSREQ_DEVICE_URLCTRL 	= 0x151;

		/*����������ģʽ,	����:CTRLREQ,	���:null*/
		public const uint MSREQ_DEVICE_SWCTRL 	= 0x152;

		/*��ȡ�������г���,	����:RUNAPPREQ,	���:RUNAPP(Array)*/
		public const uint MSREQ_RUNAPP_GET 	= 0x153;

		/*�ϴ����������Ϣ����,	����:SWUPLOADEND,	���:null*/
		public const uint CSREQ_PCSW_UPLOAD_END 	= 0x154;

		/*����豸,	����:DEVLOANREQ,	���:null*/
		public const uint MSREQ_DEVICE_LOAN 	= 0x160;

		/*�黹�豸,	����:DEVRETURNREQ,	���:null*/
		public const uint MSREQ_DEVICE_RETURN 	= 0x161;

		/*��ȡ�豸�𻵼�¼,	����:DEVDAMAGERECREQ,	���:DEVDAMAGEREC(Array)*/
		public const uint MSREQ_DEVDAMAGEREC_GET 	= 0x170;

		/*�豸ά�޴���,	����:DEVDAMAGEREC,	���:DEVDAMAGEREC*/
		public const uint MSREQ_DEVICE_REPAIR 	= 0x171;

		/*�豸�Կ��ƽ���ķ���,	����:DEVCTRLINFO,	���:null*/
		public const uint CSREQ_DEVICE_CTRLRES 	= 0x1a0;

		/*��ȡ�豸��������б�,	����:DEVCLSREQ,	���:UNIDEVCLS(Array)*/
		public const uint MSREQ_DEVCLS_GET 	= 0x210;

		/*�����豸���������Ϣ,	����:UNIDEVCLS,	���:UNIDEVCLS*/
		public const uint MSREQ_DEVCLS_SET 	= 0x220;

		/*ɾ���豸�������,	����:UNIDEVCLS,	���:null*/
		public const uint MSREQ_DEVCLS_DEL 	= 0x230;

		/*��ȡ�豸��������б�,	����:DEVKINDREQ,	���:UNIDEVKIND(Array)*/
		public const uint MSREQ_DEVKIND_GET 	= 0x310;

		/*�����豸���������Ϣ,	����:UNIDEVKIND,	���:UNIDEVKIND*/
		public const uint MSREQ_DEVKIND_SET 	= 0x320;

		/*ɾ���豸�������,	����:UNIDEVKIND,	���:null*/
		public const uint MSREQ_DEVKIND_DEL 	= 0x330;

		/*��ȡ¥����Ϣ,	����:BUILDINGREQ,	���:UNIBUILDING(Array)*/
		public const uint MSREQ_BUILDING_GET 	= 0x340;

		/*����¥��,	����:UNIBUILDING,	���:UNIBUILDING*/
		public const uint MSREQ_BUILDING_SET 	= 0x341;

		/*ɾ��¥��,	����:UNIBUILDING,	���:null*/
		public const uint MSREQ_BUILDING_DEL 	= 0x342;

		/*��ȡ������Ϣ,	����:ROOMREQ,	���:UNIROOM(Array)*/
		public const uint MSREQ_ROOM_GET 	= 0x350;

		/*���÷���,	����:UNIROOM,	���:UNIROOM*/
		public const uint MSREQ_ROOM_SET 	= 0x351;

		/*ɾ������,	����:UNIROOM,	���:null*/
		public const uint MSREQ_ROOM_DEL 	= 0x352;

		/*����˿��Ʒ��䣬Ҫ�󷿼�ִ��ĳ����,	����:ROOMCTRLINFO,	���:ROOMCTRLINFO*/
		public const uint MSREQ_ROOM_CTRL 	= 0x353;

		/*��ȡ�û��ɽ��뷿��,	����:PERMITROOMREQ,	���:PERMITROOMINFO(Array)*/
		public const uint MSREQ_ROOM_PERMITROOM 	= 0x354;

		/*��ȡ���������Ϣ,	����:ROOMCTRLREQ,	���:UNIDOORCTRL(Array)*/
		public const uint MSREQ_ROOM_CTRLINFO 	= 0x355;

		/*��ȡ����������Ϣ,	����:FULLROOMREQ,	���:FULLROOM(Array)*/
		public const uint MSREQ_FULLROOM_GET 	= 0x356;

		/*��ȡ����������Ϣ�������������ѡ��,	����:BASICROOMREQ,	���:BASICROOM(Array)*/
		public const uint MSREQ_BASICROOM_GET 	= 0x357;

		/*��ȡͨ������Ϣ,	����:CHANNELGATEREQ,	���:UNICHANNELGATE(Array)*/
		public const uint MSREQ_CHANNELGATE_GET 	= 0x360;

		/*����ͨ����,	����:UNICHANNELGATE,	���:UNICHANNELGATE*/
		public const uint MSREQ_CHANNELGATE_SET 	= 0x361;

		/*ɾ��ͨ����,	����:UNICHANNELGATE,	���:null*/
		public const uint MSREQ_CHANNELGATE_DEL 	= 0x362;

		/*����˿���ͨ���ţ�Ҫ��ͨ����ִ��ĳ����,	����:CHANNELGATECTRLINFO,	���:CHANNELGATECTRLINFO*/
		public const uint MSREQ_CHANNELGATE_CTRL 	= 0x363;

		/*��ȡ�������,	����:ROOMGROUPREQ,	���:ROOMGROUP(Array)*/
		public const uint MSREQ_ROOMGROUP_GET 	= 0x370;

		/*���÷������,	����:ROOMGROUP,	���:ROOMGROUP*/
		public const uint MSREQ_ROOMGROUP_SET 	= 0x371;

		/*ɾ���������,	����:ROOMGROUP,	���:null*/
		public const uint MSREQ_ROOMGROUP_DEL 	= 0x372;

		/*��ȡ�豸�����,	����:DEVMONITORREQ,	���:DEVMONITOR(Array)*/
		public const uint MSREQ_DEVMONITOR_GET 	= 0x380;

		/*�����豸�����,	����:DEVMONITOR,	���:DEVMONITOR*/
		public const uint MSREQ_DEVMONITOR_SET 	= 0x381;

		/*ɾ���豸�����,	����:DEVMONITOR,	���:null*/
		public const uint MSREQ_DEVMONITOR_DEL 	= 0x382;

		/*��ȡ��������豸�Ķ�Ӧ��ϵ,	����:MONDEVREQ,	���:MONDEV(Array)*/
		public const uint MSREQ_MONDEV_GET 	= 0x385;

		/*���ü�������豸�Ķ�Ӧ��ϵ,	����:MONDEV,	���:MONDEV*/
		public const uint MSREQ_MONDEV_SET 	= 0x386;

		/*ɾ����������豸�Ķ�Ӧ��ϵ,	����:MONDEV,	���:null*/
		public const uint MSREQ_MONDEV_DEL 	= 0x387;

		/*��ȡ�豸����ʱ���,	����:DEVOPENRULEREQ,	���:DEVOPENRULE(Array)*/
		public const uint MSREQ_DEVOPENRULE_GET 	= 0x410;

		/*�����豸����ʱ���,	����:DEVOPENRULE,	���:DEVOPENRULE*/
		public const uint MSREQ_DEVOPENRULE_SET 	= 0x420;

		/*ɾ���豸����ʱ���,	����:DEVOPENRULE,	���:null*/
		public const uint MSREQ_DEVOPENRULE_DEL 	= 0x430;

		/*�����鿪��ʱ���,	����:CHANGEGROUPOPENRULE,	���:CHANGEGROUPOPENRULE*/
		public const uint MSREQ_GROUPOPENRULE_SET 	= 0x440;

		/*ɾ���鿪��ʱ���,	����:CHANGEGROUPOPENRULE,	���:null*/
		public const uint MSREQ_GROUPOPENRULE_DEL 	= 0x441;

		/*��ȡ�鿪��ʱ���,	����:GROUPOPENRULEREQ,	���:GROUPOPENRULE(Array)*/
		public const uint MSREQ_GROUPOPENRULE_GET 	= 0x412;

		/*����ʱ���ڼ俪��ʱ���,	����:CHANGEPERIODOPENRULE,	���:CHANGEPERIODOPENRULE*/
		public const uint MSREQ_PERIODOPENRULE_SET 	= 0x445;

		/*ɾ��ʱ���ڼ俪��ʱ���,	����:CHANGEPERIODOPENRULE,	���:null*/
		public const uint MSREQ_PERIODOPENRULE_DEL 	= 0x446;

		/*��ȡʱ���ڼ俪��ʱ���,	����:PERIODOPENRULEREQ,	���:PERIODOPENRULE(Array)*/
		public const uint MSREQ_PERIODOPENRULE_GET 	= 0x417;

		/*��ȡ��ǰ�豸ͳ����Ϣ,	����:,	���:CURDEVSTAT*/
		public const uint MSREQ_CURDEV_STAT 	= 0x501;

		/*��ȡ��ѧ���豸���ڴ�ͳ��,	����:DEVFORTREQ,	���:DEVSECINFO(Array)*/
		public const uint MSREQ_DEVFORT_STAT 	= 0x502;

		/*��ȡ��ѧ���豸,	����:TEACHINGDEVREQ,	���:TEACHINGDEV(Array)*/
		public const uint MSREQ_TEACHINGDEV_GET 	= 0x503;

		/*��ȡ�񽱼�¼,	����:REWARDRECREQ,	���:REWARDREC(Array)*/
		public const uint MSREQ_REWARDREC_GET 	= 0x511;

		/*���û񽱼�¼,	����:REWARDREC,	���:REWARDREC*/
		public const uint MSREQ_REWARDREC_SET 	= 0x512;

		/*ɾ���񽱼�¼,	����:REWARDREC,	���:null*/
		public const uint MSREQ_REWARDREC_DEL 	= 0x513;

		/*��ȡ���ü�¼,	����:COSTRECREQ,	���:COSTREC(Array)*/
		public const uint MSREQ_COSTREC_GET 	= 0x521;

		/*���÷��ü�¼,	����:COSTREC,	���:COSTREC*/
		public const uint MSREQ_COSTREC_SET 	= 0x522;

		/*ɾ�����ü�¼,	����:COSTREC,	���:null*/
		public const uint MSREQ_COSTREC_DEL 	= 0x523;


	}
/*��������*/
	/*�����ӿ�--Device*/
	/*��ʼ�ӿ�--DoorCtrlSrv--�Ž����ù���*/
/*��ʼ����*/
	public partial class PRDoorCtrlSrv
	{

		/*��ȡ�Ž�������,	����:DCSREQ,	���:UNIDCS(Array)*/
		public const uint MSREQ_DCS_GET 	= 0x10;

		/*�����Ž�������,	����:UNIDCS,	���:UNIDCS*/
		public const uint MSREQ_DCS_SET 	= 0x20;

		/*ɾ���Ž�������,	����:UNIDCS,	���:null*/
		public const uint MSREQ_DCS_DEL 	= 0x30;

		/*�Ž���������¼,	����:DCSLOGINREQ,	���:DCSLOGINRES*/
		public const uint MSREQ_DCS_LOGIN 	= 0x110;

		/*�Ž��������˳�,	����:DCSLOGOUTREQ,	���:null*/
		public const uint MSREQ_DCS_LOGOUT 	= 0x120;

		/*��ʱͨ��,	����:DCSPULSEREQ,	���:DCSPULSERES*/
		public const uint MSREQ_DCS_PULSE 	= 0x130;

		/*�û����Ž�ˢ������ˢ��,	����:DOORCARDREQ,	���:DOORCARDRES*/
		public const uint MSREQ_DCS_DOORCARD 	= 0x140;

		/*�û����ֻ�ɨ��ά�뿪��,	����:MOBILEOPENDOORREQ,	���:MOBILEOPENDOORRES*/
		public const uint MSREQ_MOBILE_OPENDOOR 	= 0x141;

		/*��ȡ�Ž�������,	����:DOORCTRLREQ,	���:UNIDOORCTRL(Array)*/
		public const uint MSREQ_DOORCTRL_GET 	= 0x210;

		/*�����Ž�������,	����:UNIDOORCTRL,	���:UNIDOORCTRL*/
		public const uint MSREQ_DOORCTRL_SET 	= 0x220;

		/*ɾ���Ž�������,	����:UNIDOORCTRL,	���:null*/
		public const uint MSREQ_DOORCTRL_DEL 	= 0x230;

		/*���Ž�������������,	����:DOORCTRLCMD,	���:null*/
		public const uint MSREQ_DOORCTRL_CMD 	= 0x240;


	}
/*��������*/
	/*�����ӿ�--DoorCtrlSrv*/
	/*��ʼ�ӿ�--Group--�û������*/
/*��ʼ����*/
	public partial class PRGroup
	{

		/*��ȡ��,	����:GROUPREQ,	���:UNIGROUP(Array)*/
		public const uint MSREQ_GROUP_GET 	= 0x10;

		/*�޸���,	����:UNIGROUP,	���:UNIGROUP*/
		public const uint MSREQ_GROUP_SET 	= 0x20;

		/*ɾ����,	����:UNIGROUP,	���:null*/
		public const uint MSREQ_GROUP_DEL 	= 0x30;

		/*������Ա,	����:GROUPMEMBER,	���:null*/
		public const uint MSREQ_GROUPMEMBER_SET 	= 0x110;

		/*ɾ�����Ա,	����:GROUPMEMBER,	���:null*/
		public const uint MSREQ_GROUPMEMBER_DEL 	= 0x120;

		/*��ȡ���Ա��ϸ,	����:GROUPMEMDETAILREQ,	���:GROUPMEMDETAIL(Array)*/
		public const uint MSREQ_GROUPMEMDETAIL_GET 	= 0x130;


	}
/*��������*/
	/*�����ӿ�--Group*/
	/*��ʼ�ӿ�--Reserve--ԤԼ����*/
/*��ʼ����*/
	public partial class PRReserve
	{

		/*��ȡԤԼ�б�,	����:RESVREQ,	���:UNIRESERVE(Array)*/
		public const uint MSREQ_RESERVE_GET 	= 0x10;

		/*��ȡԤԼ�б�������վ��ʾ,	����:RESVSHOWREQ,	���:RESVSHOW(Array)*/
		public const uint MSREQ_RESERVE_GETSHOW 	= 0x11;

		/*��ȡ��ѧԤԼ�б�,	����:TEACHINGRESVREQ,	���:TEACHINGRESV(Array)*/
		public const uint MSREQ_TEACHINGRESV_GET 	= 0x12;

		/*��ȡ����ʵ��ԤԼ�б�,	����:RTRESVREQ,	���:RTRESV(Array)*/
		public const uint MSREQ_RTRESV_GET 	= 0x13;

		/*��ȡ����ʵ���˵�,	����:RTRESVBILLREQ,	���:RTRESVBILL*/
		public const uint MSREQ_RTBILL_GET 	= 0x14;

		/*��ȡ����ʵ���˵�,	����:RESVTIMEREQ,	���:RESVTIME(Array)*/
		public const uint MSREQ_RESVTIME_GET 	= 0x15;

		/*����ԤԼ��Ϣ,	����:UNIRESERVE,	���:UNIRESERVE*/
		public const uint MSREQ_RESERVE_SET 	= 0x20;

		/*�Զ�ԤԼ��ϵͳ�Զ������豸,	����:AUTORESVREQ,	���:UNIRESERVE*/
		public const uint MSREQ_RESERVE_AUTO 	= 0x21;

		/*�żٵ���(����10��5�գ������壩����9��29�գ���������,	����:HOLIDAYSHIFT,	���:null*/
		public const uint MSREQ_RESERVE_HOLIDAYSHIFT 	= 0x22;

		/*����ԤԼС��,	����:RESVMEMBER,	���:null*/
		public const uint MSREQ_RESVMEMBER_JOIN 	= 0x23;

		/*�˳�ԤԼС��,	����:RESVMEMBER,	���:null*/
		public const uint MSREQ_RESVMEMBER_EXIT 	= 0x24;

		/*ȡ��ԤԼǩ������,	����:UNIRESERVE,	���:null*/
		public const uint MSREQ_RESERVE_CANCELSIGN 	= 0x25;

		/*�½��޸Ŀ���ʵ��ԤԼ,	����:RTRESV,	���:RTRESV*/
		public const uint MSREQ_RTRESV_SET 	= 0x26;

		/*����ʵ��ԤԼ���,	����:RTRESVCHECK,	���:null*/
		public const uint MSREQ_RTRESV_CHECK 	= 0x27;

		/*����ʵ��Ԥ�շ�,	����:RTPREPAY,	���:null*/
		public const uint MSREQ_RTRESV_PREPAY 	= 0x28;

		/*����ʵ���˵����,	����:RTBILLCHECK,	���:null*/
		public const uint MSREQ_RTBILL_CHECK 	= 0x29;

		/*����ʵ���˵�����,	����:RTBILLSETTLE,	���:null*/
		public const uint MSREQ_RTBILL_SETTLE 	= 0x2A;

		/*����ʵ���˵�����,	����:RTBILLRECEIVE,	���:null*/
		public const uint MSREQ_RTBILL_RECEIVE 	= 0x2B;

		/*�������¼ԤԼ,	����:ANONYMOUSRESV,	���:ANONYMOUSRESV*/
		public const uint MSREQ_ANONYMOUSRESV_SET 	= 0x2C;

		/*����ȫ��ѧ��ԤԼ,	����:ALLUSERRESV,	���:ALLUSERRESV*/
		public const uint MSREQ_ALLUSERRESV_SET 	= 0x2D;

		/*ɾ��ԤԼ,	����:UNIRESERVE,	���:null*/
		public const uint MSREQ_RESERVE_DEL 	= 0x30;

		/*ɾ������ʵ��ԤԼ,	����:RTRESV,	���:null*/
		public const uint MSREQ_RTRESV_DEL 	= 0x31;

		/*��ǰ����ԤԼ,	����:UNIRESERVE,	���:null*/
		public const uint MSREQ_RESERVE_EARLYEND 	= 0x32;

		/*����ԤԼ����ʱ��,	����:RESVENDTIME,	���:RESVENDTIME*/
		public const uint MSREQ_RESERVE_CHGENDTIME 	= 0x40;

		/*��ȡ�豸ԤԼ��,	����:DEVRESVREQ,	���:DEVRESVINFO(Array)*/
		public const uint MSREQ_DEVRESV_GET 	= 0x50;

		/*ԤԼ���ú���,���㱾��ԤԼʹ�ú��ʵ�ʷ������á�,	����:RESVCOSTADJUST,	���:RESVCOSTADJUST*/
		public const uint MSREQ_RESERVE_COSTADJUST 	= 0x61;

		/*ԤԼ���ý���,��ʹ���߽��㱾��ԤԼ�����ķ��á�,	����:RESVCHECKOUT,	���:RESVCHECKOUT*/
		public const uint MSREQ_RESERVE_CHECKOUT 	= 0x62;

		/*��ȡѧ�ڱ�,	����:TERMREQ,	���:UNITERM(Array)*/
		public const uint MSREQ_TERM_GET 	= 0x110;

		/*����ѧ�ڱ�,	����:UNITERM,	���:UNITERM*/
		public const uint MSREQ_TERM_SET 	= 0x120;

		/*ɾ��ѧ�ڱ�,	����:UNITERM,	���:null*/
		public const uint MSREQ_TERM_DEL 	= 0x130;

		/*��ȡ��Ϣ��,	����:CTSREQ,	���:CLASSTIMETABLE(Array)*/
		public const uint MSREQ_CTS_GET 	= 0x140;

		/*������Ϣ��,	����:CLASSTIMETABLE,	���:null*/
		public const uint MSREQ_CTS_SET 	= 0x141;

		/*��ȡ�γ�,	����:COURSEREQ,	���:UNICOURSE(Array)*/
		public const uint MSREQ_COURSE_GET 	= 0x210;

		/*���ÿγ�,	����:UNICOURSE,	���:UNICOURSE*/
		public const uint MSREQ_COURSE_SET 	= 0x220;

		/*ɾ���γ�,	����:UNICOURSE,	���:null*/
		public const uint MSREQ_COURSE_DEL 	= 0x230;

		/*��ȡʵ����Ŀ��,	����:TESTCARDREQ,	���:TESTCARD(Array)*/
		public const uint MSREQ_TESTCARD_GET 	= 0x250;

		/*����ʵ����Ŀ��,	����:TESTCARD,	���:TESTCARD*/
		public const uint MSREQ_TESTCARD_SET 	= 0x251;

		/*ɾ��ʵ����Ŀ��,	����:TESTCARD,	���:null*/
		public const uint MSREQ_TESTCARD_DEL 	= 0x252;

		/*��ȡʵ��ƻ�,	����:TESTPLANREQ,	���:UNITESTPLAN(Array)*/
		public const uint MSREQ_TESTPLAN_GET 	= 0x310;

		/*����ʵ��ƻ�,	����:UNITESTPLAN,	���:UNITESTPLAN*/
		public const uint MSREQ_TESTPLAN_SET 	= 0x311;

		/*ɾ��ʵ��ƻ�,	����:UNITESTPLAN,	���:null*/
		public const uint MSREQ_TESTPLAN_DEL 	= 0x312;

		/*��ȡʵ����Ŀ,	����:TESTITEMREQ,	���:UNITESTITEM(Array)*/
		public const uint MSREQ_TESTITEM_GET 	= 0x320;

		/*����ʵ����Ŀ,	����:UNITESTITEM,	���:UNITESTITEM*/
		public const uint MSREQ_TESTITEM_SET 	= 0x321;

		/*ɾ��ʵ����Ŀ,	����:UNITESTITEM,	���:null*/
		public const uint MSREQ_TESTITEM_DEL 	= 0x322;

		/*��ȡʵ����Ŀ������ԤԼ��ϸ��Ϣ,	����:TESTITEMMEMRESVREQ,	���:TESTITEMMEMRESV(Array)*/
		public const uint MSREQ_TESTITEMMEMRESV_GET 	= 0x323;

		/*��ȡʵ����Ŀ��ϸ��Ϣ,	����:TESTITEMINFOREQ,	���:TESTITEMINFO(Array)*/
		public const uint MSREQ_TESTITEMINFO_GET 	= 0x328;

		/*��ʦ�ύʵ�鱨��ģ��,	����:REPORTFORMUPLOAD,	���:null*/
		public const uint MSREQ_REPORTFORM_UPLOAD 	= 0x331;

		/*ѧ����ʵ�鱨��,	����:REPORTUPLOAD,	���:null*/
		public const uint MSREQ_REPORT_UPLOAD 	= 0x332;

		/*����ʵ�鱨��,	����:REPORTCORRECT,	���:null*/
		public const uint MSREQ_REPORT_CORRECT 	= 0x333;

		/*��ȡ�����,	����:ACTIVITYPLANREQ,	���:UNIACTIVITYPLAN(Array)*/
		public const uint MSREQ_ACTIVITYPLAN_GET 	= 0x350;

		/*���û����,	����:UNIACTIVITYPLAN,	���:UNIACTIVITYPLAN*/
		public const uint MSREQ_ACTIVITYPLAN_SET 	= 0x351;

		/*ɾ�������,	����:UNIACTIVITYPLAN,	���:null*/
		public const uint MSREQ_ACTIVITYPLAN_DEL 	= 0x352;

		/*��ȡ����ŵ���λ�б�,	����:APSEATREQ,	���:APSEAT(Array)*/
		public const uint MSREQ_APSEAT_GET 	= 0x353;

		/*����μӻ,	����:ACTIVITYENROLL,	���:null*/
		public const uint MSREQ_ACTIVITY_ENROLL 	= 0x354;

		/*�˳������,	����:ACTIVITYEXIT,	���:null*/
		public const uint MSREQ_ACTIVITY_EXIT 	= 0x355;

		/*����Ա����ǩ����Ա����,	����:AOFFLINESIGN,	���:AOFFLINESIGN*/
		public const uint MSREQ_ACTIVITY_OFFLINESIGN 	= 0x365;

		/*��ȡԤԼ�����б�,	����:RESVRULEREQ,	���:UNIRESVRULE(Array)*/
		public const uint MSREQ_RESVRULE_GET 	= 0x410;

		/*����Ա��ȡԤԼ�����б�,	����:RESVRULEADMINREQ,	���:UNIRESVRULE(Array)*/
		public const uint MSREQ_RESVRULE_ADMINGET 	= 0x411;

		/*����ԤԼ����,	����:UNIRESVRULE,	���:UNIRESVRULE*/
		public const uint MSREQ_RESVRULE_SET 	= 0x420;

		/*ɾ��ԤԼ����,	����:UNIRESVRULE,	���:null*/
		public const uint MSREQ_RESVRULE_DEL 	= 0x430;

		/*��ȡ����ʵ��,	����:RESEARCHTESTREQ,	���:RESEARCHTEST(Array)*/
		public const uint MSREQ_RESEARCHTEST_GET 	= 0x451;

		/*�½�/�޸Ŀ���ʵ��,	����:RESEARCHTEST,	���:RESEARCHTEST*/
		public const uint MSREQ_RESEARCHTEST_SET 	= 0x452;

		/*ɾ������ʵ��,	����:RESEARCHTEST,	���:null*/
		public const uint MSREQ_RESEARCHTEST_DEL 	= 0x453;

		/*���ÿ���ʵ���Ա,	����:RTMEMBER,	���:null*/
		public const uint MSREQ_RTMEMBER_SET 	= 0x454;

		/*ɾ������ʵ���Ա,	����:RTMEMBER,	���:null*/
		public const uint MSREQ_RTMEMBER_DEL 	= 0x455;

		/*��ȡʵ����Ʒ��Ϣ,	����:SAMPLEINFOREQ,	���:SAMPLEINFO(Array)*/
		public const uint MSREQ_SAMPLEINFO_GET 	= 0x461;

		/*�½�/�޸�ʵ����Ʒ��Ϣ,	����:SAMPLEINFO,	���:SAMPLEINFO*/
		public const uint MSREQ_SAMPLEINFO_SET 	= 0x462;

		/*ɾ��ʵ����Ʒ��Ϣ,	����:SAMPLEINFO,	���:null*/
		public const uint MSREQ_SAMPLEINFO_DEL 	= 0x463;

		/*��ȡ����ԤԼ�б�,	����:YARDRESVREQ,	���:YARDRESV(Array)*/
		public const uint MSREQ_YARDRESV_GET 	= 0x501;

		/*�½��޸ĳ���ԤԼ,	����:YARDRESV,	���:YARDRESV*/
		public const uint MSREQ_YARDRESV_SET 	= 0x511;

		/*ɾ������ԤԼ,	����:YARDRESV,	���:null*/
		public const uint MSREQ_YARDRESV_DEL 	= 0x521;

		/*��ȡ����ԤԼ����б�,	����:YARDRESVCHECKINFOREQ,	���:YARDRESVCHECKINFO(Array)*/
		public const uint MSREQ_YARDRESVCHECKINFO_GET 	= 0x531;

		/*����ԤԼ���,	����:YARDRESVCHECK,	���:null*/
		public const uint MSREQ_YARDRESV_CHECK 	= 0x535;

		/*��ȡ����ԤԼ����б�,	����:RESVCHECKINFOREQ,	���:RESVCHECKINFO(Array)*/
		public const uint MSREQ_RESVCHECKINFO_GET 	= 0x536;

		/*����ԤԼ���,	����:RESVCHECKINFO,	���:null*/
		public const uint MSREQ_RESV_CHECK 	= 0x537;

		/*��ȡ���ݻ�б�,	����:YARDACTIVITYREQ,	���:YARDACTIVITY(Array)*/
		public const uint MSREQ_YARDACTIVITY_GET 	= 0x541;

		/*�½����ݻ,	����:YARDACTIVITY,	���:YARDACTIVITY*/
		public const uint MSREQ_YARDACTIVITY_SET 	= 0x545;

		/*ɾ�����ݻ,	����:YARDACTIVITY,	���:null*/
		public const uint MSREQ_YARDACTIVITY_DEL 	= 0x548;

		/*������ԤԼ�����豸�������Դ��ͻ�����ز�ִ�е�����ԤԼ��,	����:THIRDRESVSHAREDEV,	���:THIRDRESVSHAREDEV*/
		public const uint MSREQ_THIRDRESV_SHAREDEV 	= 0x551;

		/*������ɾ��ԤԼ�����豸,	����:THIRDRESVDEL,	���:null*/
		public const uint MSREQ_THIRDRESV_DEL 	= 0x552;

		/*��ȡ������ԤԼ�б�,	����:THIRDRESVREQ,	���:THIRDRESV(Array)*/
		public const uint MSREQ_THIRDRESV_GET 	= 0x555;


	}
/*��������*/
	/*�����ӿ�--Reserve*/
	/*��ʼ�ӿ�--Control--������Ϸ����*/
/*��ʼ����*/
	public partial class PRControl
	{

		/*��ȡ��ط����,	����:CTRLCLASSREQ,	���:UNICTRLCLASS(Array)*/
		public const uint MSREQ_CTRLCLASS_GET 	= 0x10;

		/*�޸ļ�ط����,	����:UNICTRLCLASS,	���:UNICTRLCLASS*/
		public const uint MSREQ_CTRLCLASS_SET 	= 0x20;

		/*ɾ����ط����,	����:UNICTRLCLASS,	���:null*/
		public const uint MSREQ_CTRLCLASS_DEL 	= 0x30;

		/*��ȡ��ַ��,	����:CTRLURLREQ,	���:UNICTRLURL(Array)*/
		public const uint MSREQ_CTRLURL_GET 	= 0x110;

		/*�޸���ַ��,	����:UNICTRLURL,	���:UNICTRLURL*/
		public const uint MSREQ_CTRLURL_SET 	= 0x120;

		/*ɾ����ַ��,	����:UNICTRLURL,	���:null*/
		public const uint MSREQ_CTRLURL_DEL 	= 0x130;

		/*��ȡ�����,	����:CTRLSWREQ,	���:UNICTRLSW(Array)*/
		public const uint MSREQ_CTRLSW_GET 	= 0x210;

		/*�޸������,	����:UNICTRLSW,	���:UNICTRLSW*/
		public const uint MSREQ_CTRLSW_SET 	= 0x220;

		/*ɾ�������,	����:UNICTRLSW,	���:null*/
		public const uint MSREQ_CTRLSW_DEL 	= 0x230;

		/*��ȡ���,	����:SOFTWAREREQ,	���:UNISOFTWARE(Array)*/
		public const uint MSREQ_SOFTWARE_GET 	= 0x310;

		/*�޸����,	����:UNISOFTWARE,	���:UNISOFTWARE*/
		public const uint MSREQ_SOFTWARE_SET 	= 0x320;

		/*��ȡ����,	����:PROGRAMREQ,	���:UNIPROGRAM(Array)*/
		public const uint MSREQ_PROGRAM_GET 	= 0x330;

		/*�޸ĳ���,	����:UNIPROGRAM,	���:UNIPROGRAM*/
		public const uint MSREQ_PROGRAM_SET 	= 0x340;

		/*��ȡ��������,	����:PCSWINFOREQ,	���:UNIPCSWINFO(Array)*/
		public const uint MSREQ_PCSWINFO_GET 	= 0x410;

		/*��ȡ��������,	����:ROOMSWINFOREQ,	���:UNIROOMSWINFO(Array)*/
		public const uint MSREQ_ROOMSWINFO_GET 	= 0x420;


	}
/*��������*/
	/*�����ӿ�--Control*/
	/*��ʼ�ӿ�--THIRDIF--�������ӿڹ���*/
/*��ʼ����*/
	public partial class PRTHIRDIF
	{

		/*��¼�ӿڷ�����,	����:THIRDLOGINREQ,	���:THIRDLOGINRES*/
		public const uint THIRDIF_LOGIN 	= 0x10;

		/*�˳��ӿڷ�����,	����:,	���:null*/
		public const uint THIRDIF_LOGOUT 	= 0x20;

		/*��ȡ�ʻ��б�,	����:THIRDACCREQ,	���:UNIACCOUNT(Array)*/
		public const uint THIRDIF_ACC_GET 	= 0x30;

		/*��ȡ�����ʻ���Ϣ,	����:SYNCACCREQ,	���:SYNCACCINFO*/
		public const uint THIRDIF_SYNCACC 	= 0x110;


	}
/*��������*/
	/*�����ӿ�--THIRDIF*/
	/*��ʼ�ӿ�--Fee--�շѹ���*/
/*��ʼ����*/
	public partial class PRFee
	{

		/*��ȡ�շѱ�׼�б�,	����:FEEREQ,	���:UNIFEE(Array)*/
		public const uint MSREQ_FEE_GET 	= 0x10;

		/*��ȡ������Ŀ��Ӧ���豸���շѱ�׼,	����:RTDEVFEEREQ,	���:UNIFEE*/
		public const uint MSREQ_RTDEVFEE_GET 	= 0x11;

		/*��ȡ������Ŀ��Ӧ���豸����Ʒ������,	����:RTDEVSAMPLEREQ,	���:RTDEVSAMPLE(Array)*/
		public const uint MSREQ_RTDEVSAMPLE_GET 	= 0x12;

		/*�����շѱ�׼��Ϣ,	����:UNIFEE,	���:UNIFEE*/
		public const uint MSREQ_FEE_SET 	= 0x20;

		/*ɾ���շѱ�׼,	����:UNIFEE,	���:null*/
		public const uint MSREQ_FEE_DEL 	= 0x30;

		/*��ȡ��ʱʹ�ù���,	����:FTRULEREQ,	���:FREETIMERULE(Array)*/
		public const uint MSREQ_FREETIMERULE_GET 	= 0x110;

		/*�޸Ļ�ʱʹ�ù���,	����:FREETIMERULE,	���:FREETIMERULE*/
		public const uint MSREQ_FREETIMERULE_SET 	= 0x120;

		/*ɾ����ʱʹ�ù���,	����:FREETIMERULE,	���:null*/
		public const uint MSREQ_FREETIMERULE_DEL 	= 0x130;

		/*��ȡ�˵�,	����:BILLREQ,	���:UNIBILL(Array)*/
		public const uint MSREQ_BILL_GET 	= 0x201;

		/*�����˵�,	����:UNIBILL,	���:null*/
		public const uint MSREQ_BILL_SET 	= 0x202;

		/*�˵��ɷ�,	����:BILLPAY,	���:null*/
		public const uint MSREQ_BILL_PAY 	= 0x203;


	}
/*��������*/
	/*�����ӿ�--Fee*/
	/*��ʼ�ӿ�--Console--����̨����*/
/*��ʼ����*/
	public partial class PRConsole
	{

		/*��ȡ����̨�б�,	����:CONREQ,	���:UNICONSOLE(Array)*/
		public const uint MSREQ_CONSOLE_GET 	= 0x10;

		/*���ÿ���̨��Ϣ,	����:UNICONSOLE,	���:UNICONSOLE*/
		public const uint MSREQ_CONSOLE_SET 	= 0x20;

		/*ɾ������̨,	����:UNICONSOLE,	���:null*/
		public const uint MSREQ_CONSOLE_DEL 	= 0x30;

		/*����̨��¼,	����:CONLOGINREQ,	���:CONLOGINRES*/
		public const uint MSREQ_CONSOLE_LOGIN 	= 0x110;

		/*����̨�˳�,	����:CONLOGOUTREQ,	���:null*/
		public const uint MSREQ_CONSOLE_LOGOUT 	= 0x120;

		/*��ʱͨ��,	����:CONPULSEREQ,	���:CONPULSERES*/
		public const uint MSREQ_CONSOLE_PULSE 	= 0x130;

		/*������̨����Ϣ,	����:CONMESSAGE,	���:null*/
		public const uint MSREQ_CONSOLE_SHOWMSG 	= 0x140;

		/*����̨ˢ��,	����:ACCCHECKREQ,	���:CONUSERINFO*/
		public const uint MSREQ_CONSOLE_USERCARD 	= 0x210;

		/*����̨��ʦ��¼,	����:ACCCHECKREQ,	���:CONTEACHERINFO*/
		public const uint MSREQ_CONSOLE_TEACHERIN 	= 0x211;

		/*����̨ˢ���ϻ�,	����:CARDFORPCREQ,	���:CARDFORPCRES*/
		public const uint MSREQ_CONSOLE_CARDFORPC 	= 0x212;

		/*ͨ����ˢ��,	����:AUTOGATECARDREQ,	���:AUTOGATECARDRES*/
		public const uint MSREQ_CONSOLE_AUTOGATECARD 	= 0x213;

		/*�ֻ�ɨ���ά��,	����:MOBILESCANREQ,	���:MOBILESCANRES*/
		public const uint MSREQ_MOBILE_SCAN 	= 0x214;

		/*����̨ˢ����ʼʹ��,	����:CONUSERINREQ,	���:CONUSERINRES*/
		public const uint MSREQ_CONSOLE_USERIN 	= 0x220;

		/*�ֻ�ɨ�迪ʼʹ��,	����:MOBILEUSERINREQ,	���:MOBILEUSERINRES*/
		public const uint MSREQ_MOBILE_USERIN 	= 0x221;

		/*�ֻ���ʱ����Լ),	����:MOBILEDELAYREQ,	���:MOBILEDELAYRES*/
		public const uint MSREQ_MOBILE_DELAY 	= 0x222;

		/*����̨ˢ������ʹ��,	����:CONUSEROUTREQ,	���:CONUSEROUTRES*/
		public const uint MSREQ_CONSOLE_USEROUT 	= 0x230;

		/*�ֻ�ɨ��ˢ������ʹ��,	����:MOBILEUSEROUTREQ,	���:MOBILEUSEROUTRES*/
		public const uint MSREQ_MOBILE_USEROUT 	= 0x231;

		/*�ֻ���¼ǩ��,	����:RESVUSERCOMEINREQ,	���:RESVUSERCOMEINRES*/
		public const uint MSREQ_RESVUSER_COMEIN 	= 0x241;

		/*�ֻ���¼��ʱ����Լ),	����:RESVUSERDELAYREQ,	���:RESVUSERDELAYRES*/
		public const uint MSREQ_RESVUSER_DELAY 	= 0x245;

		/*�ֻ���¼�뿪,	����:RESVUSERGOOUTREQ,	���:RESVUSERGOOUTRES*/
		public const uint MSREQ_RESVUSER_GOOUT 	= 0x248;

		/*ҡһҡǩ����ʼʹ��,	����:SHAKECHECKINREQ,	���:SHAKECHECKINRES*/
		public const uint MSREQ_SHAKE_CHECKIN 	= 0x250;

		/*ҡһҡ���,	����:SHAKECOMEINREQ,	���:SHAKECOMEINRES*/
		public const uint MSREQ_SHAKE_COMEIN 	= 0x251;


	}
/*��������*/
	/*�����ӿ�--Console*/
	/*��ʼ�ӿ�--Report--�����ѯͳ��*/
/*��ʼ����*/
	public partial class PRReport
	{

		/*��ȡԤԼ��¼,	����:RESVRECREQ,	���:UNIRESVREC(Array)*/
		public const uint MSREQ_REPORT_RESVREC_GET 	= 0x1;

		/*ԤԼ����ͳ��,	����:RESVKINDSTATREQ,	���:RESVKINDSTAT(Array)*/
		public const uint MSREQ_REPORT_RESVREC_KINDSTAT 	= 0x2;

		/*ԤԼ��ʽͳ��,	����:RESVMODESTATREQ,	���:RESVMODESTAT(Array)*/
		public const uint MSREQ_RESVMODESTAT_GET 	= 0x3;

		/*��ȡʹ�ü�¼��ϸ,	����:USERECREQ,	���:DEVUSEREC(Array)*/
		public const uint MSREQ_REPORT_USEREC_GET 	= 0x10;

		/*��ȡ�Ž�ˢ����¼��ϸ,	����:DOORCARDRECREQ,	���:DOORCARDREC(Array)*/
		public const uint MSREQ_REPORT_DOORCARDREC_GET 	= 0x11;

		/*ʹ����ͳ�Ʊ�������,	����:REPORTREQ,	���:USERSTAT(Array)*/
		public const uint MSREQ_REPORT_USEREC_USERSTAT 	= 0x20;

		/*ʵ����ͳ�Ʊ�������,	����:REPORTREQ,	���:LABSTAT(Array)*/
		public const uint MSREQ_REPORT_USEREC_LABSTAT 	= 0x21;

		/*�豸����ͳ�Ʊ�������,	����:REPORTREQ,	���:DEVKINDSTAT(Array)*/
		public const uint MSREQ_REPORT_USEREC_DEVKINDSTAT 	= 0x22;

		/*�豸ͳ�Ʊ�������(CUniStruct[REQEXTINFO],szExtInfo����DEVSTAT�ܺϼ�),	����:REPORTREQ,	���:DEVSTAT(Array)*/
		public const uint MSREQ_REPORT_USEREC_DEVSTAT 	= 0x23;

		/*ʵ����Ŀ��,	����:TESTITEMSTATREQ,	���:TESTITEMSTAT(Array)*/
		public const uint MSREQ_REPORT_USEREC_TESTITEMSTAT 	= 0x24;

		/*�豸����ͳ�Ʊ�������,	����:REPORTREQ,	���:DEVCLASSSTAT(Array)*/
		public const uint MSREQ_REPORT_USEREC_DEVCLASSSTAT 	= 0x25;

		/*ѧԺʹ��ͳ��,	����:REPORTREQ,	���:DEPTSTAT(Array)*/
		public const uint MSREQ_REPORT_USEREC_DEPTSTAT 	= 0x26;

		/*���ʹ��ͳ��,	����:IDENTSTATREQ,	���:IDENTSTAT(Array)*/
		public const uint MSREQ_REPORT_USEREC_IDENTSTAT 	= 0x27;

		/*��ȡ��ѧԤԼ��¼,	����:TEACHINGRESVRECREQ,	���:TEACHINGRESVREC(Array)*/
		public const uint MSREQ_REPORT_TEACHINGRESVREC 	= 0x28;

		/*ʵ����(����)ͳ�Ʊ�������,	����:REPORTREQ,	���:ROOMSTAT(Array)*/
		public const uint MSREQ_REPORT_ROOMSTAT 	= 0x29;

		/*��ȡ�豸ʹ���ʲ�ѯ,	����:DEVUSINGRATEREQ,	���:DEVUSINGRATE*/
		public const uint MSREQ_REPORT_DEVUSINGRATE 	= 0x30;

		/*��ȡ�豸��ʹ��ͳ��,	����:DEVMONTHSTATREQ,	���:DEVMONTHSTAT(Array)*/
		public const uint MSREQ_REPORT_DEVMONTHSTAT 	= 0x31;

		/*��ȡ�豸����ʵ��ͳ��,	����:RTUSESTATREQ,	���:RTUSESTAT(Array)*/
		public const uint MSREQ_REPORT_RTUSESTAT 	= 0x32;

		/*��ȡ�豸����ʵ����ϸ,	����:RTUSEDETAILREQ,	���:RTUSEDETAIL(Array)*/
		public const uint MSREQ_REPORT_RTUSEDETAIL 	= 0x33;

		/*��ȡ�豸����ʵ����÷���ͳ��,	����:RTFASTATREQ,	���:RTFASTAT(Array)*/
		public const uint MSREQ_REPORT_RTFASTAT 	= 0x34;

		/*��ȡ�豸����ʵ����÷�����ϸ,	����:RTFADETAILREQ,	���:RTFADETAIL(Array)*/
		public const uint MSREQ_REPORT_RTFADETAIL 	= 0x35;

		/*ΥԼͳ��,	����:DEFAULTSTATREQ,	���:DEFAULTSTAT(Array)*/
		public const uint MSREQ_REPORT_DEFAULTSTAT 	= 0x36;

		/*��ȡ�豸��ʹ���ʲ�ѯ,	����:DEVWEEKUSINGRATEREQ,	���:DEVWEEKUSINGRATE*/
		public const uint MSREQ_REPORT_DEVWEEKUSINGRATE 	= 0x37;

		/*���ݻ����ͳ��,	����:YARDACTIVITYSTATREQ,	���:YARDACTIVITYSTAT(Array)*/
		public const uint MSREQ_REPORT_YARDACTIVITYSTAT 	= 0x38;

		/*��ѧ���������豸�� (SJ1),	����:DEVLISTREQ,	���:DEVLIST(Array)*/
		public const uint MSREQ_REPORT_DEVLIST 	= 0x101;

		/*��ѧ���������豸�����䶯�����(SJ2),	����:DEVCHGREQ,	���:DEVCHG*/
		public const uint MSREQ_REPORT_DEVCHG 	= 0x102;

		/*���������豸��(SJ3),	����:BIGDEVREQ,	���:BIGDEV(Array)*/
		public const uint MSREQ_REPORT_BIGDEV 	= 0x103;

		/*ʵ����Ŀ��(SJ4),	����:TESTITEMREPORTREQ,	���:TESTITEMREPORT(Array)*/
		public const uint MSREQ_REPORT_TESTITEMREPORT 	= 0x104;

		/*ר��ʵ������Ա��(SJ5),	����:STAFFINFOREQ,	���:STAFFINFO(Array)*/
		public const uint MSREQ_REPORT_STAFFINFO 	= 0x105;

		/*ʵ���һ��������(SJ6),	����:LABINFOREQ,	���:LABINFO(Array)*/
		public const uint MSREQ_REPORT_LABINFO 	= 0x106;

		/*ʵ���Ҿ��������(SJ7),	����:LABALLCOSTREQ,	���:LABALLCOST*/
		public const uint MSREQ_REPORT_LABALLCOST 	= 0x107;

		/*�ߵ�ѧУʵ�����ۺ���Ϣ��(SZ1),	����:LABSUMMARYREQ,	���:LABSUMMARY*/
		public const uint MSREQ_REPORT_LABSUMMARY 	= 0x108;

		/*�ߵ�ѧУʵ�����ۺ���Ϣ��2(SZ2),	����:LABSUMMARY2REQ,	���:LABSUMMARY2*/
		public const uint MSREQ_REPORT_LABSUMMARY2 	= 0x109;

		/*��ѧ���������豸�� (SJ1),	����:DEVLIST,	���:null*/
		public const uint MSREQ_REPORT_DEVLIST_SET 	= 0x131;

		/*��ѧ���������豸�����䶯�����(SJ2),	����:DEVCHG,	���:null*/
		public const uint MSREQ_REPORT_DEVCHG_SET 	= 0x132;

		/*���������豸��(SJ3),	����:BIGDEV,	���:null*/
		public const uint MSREQ_REPORT_BIGDEV_SET 	= 0x133;

		/*ʵ����Ŀ��(SJ4),	����:TESTITEMREPORT,	���:null*/
		public const uint MSREQ_REPORT_TESTITEMREPORT_SET 	= 0x134;

		/*ר��ʵ������Ա��(SJ5),	����:STAFFINFO,	���:null*/
		public const uint MSREQ_REPORT_STAFFINFO_SET 	= 0x135;

		/*ʵ���һ��������(SJ6),	����:LABINFO,	���:null*/
		public const uint MSREQ_REPORT_LABINFO_SET 	= 0x136;

		/*ʵ���Ҿ��������(SJ7),	����:LABALLCOST,	���:null*/
		public const uint MSREQ_REPORT_LABALLCOST_SET 	= 0x137;

		/*�ߵ�ѧУʵ�����ۺ���Ϣ��(SZ1),	����:LABSUMMARY,	���:null*/
		public const uint MSREQ_REPORT_LABSUMMARY_SET 	= 0x138;

		/*�ߵ�ѧУʵ�����ۺ���Ϣ��2(SZ2),	����:LABSUMMARY2,	���:null*/
		public const uint MSREQ_REPORT_LABSUMMARY2_SET 	= 0x139;


	}
/*��������*/
	/*�����ӿ�--Report*/
	/*��ʼ�ӿ�--System--ϵͳ����*/
/*��ʼ����*/
	public partial class PRSystem
	{

		/*��ȡϵͳ�����ļ�,	����:CFGREQ,	���:CFGINFO(Array)*/
		public const uint MSREQ_SYSTEM_CFGGET 	= 0x10;

		/*�޸Ĳ�����ϵͳ�����ļ�,	����:CFGINFO,	���:null*/
		public const uint MSREQ_SYSTEM_CFGSET 	= 0x15;

		/*��ȡ��������������б�,	����:CREDITTYPEREQ,	���:CREDITTYPE(Array)*/
		public const uint MSREQ_CREDITTYPE_GET 	= 0x20;

		/*���ö������������,	����:CREDITTYPE,	���:null*/
		public const uint MSREQ_CREDITTYPE_SET 	= 0x25;

		/*��ȡ���÷��������б�,	����:CREDITSCOREREQ,	���:CREDITSCORE(Array)*/
		public const uint MSREQ_CREDITSCORE_GET 	= 0x40;

		/*�������÷�������,	����:CREDITSCORE,	���:CREDITSCORE*/
		public const uint MSREQ_CREDITSCORE_SET 	= 0x45;

		/*��ȡ�ҵ����û���,	����:MYCREDITSCOREREQ,	���:MYCREDITSCORE(Array)*/
		public const uint MSREQ_MYCREDITSCORE_GET 	= 0x48;

		/*�˹����ù���,	����:ADMINCREDIT,	���:null*/
		public const uint MSREQ_ADMINCREDIT_DO 	= 0x50;

		/*��ȡ���ü�¼,	����:CREDITRECREQ,	���:CREDITREC(Array)*/
		public const uint MSREQ_CREDITREC_GET 	= 0x55;

		/*��ȡϵͳ�����б�,	����:SYSFUNCREQ,	���:SYSFUNC(Array)*/
		public const uint MSREQ_SYSFUNC_GET 	= 0x60;

		/*��ȡϵͳ����ʹ�ù����б�,	����:SYSFUNCRULEREQ,	���:SYSFUNCRULE(Array)*/
		public const uint MSREQ_SYSFUNCRULE_GET 	= 0x65;

		/*�޸�ϵͳ����ʹ�ù���,	����:SYSFUNCRULE,	���:SYSFUNCRULE*/
		public const uint MSREQ_SYSFUNCRULE_SET 	= 0x66;

		/*��ȡϵͳ�����ʸ��,	����:SFROLEINFOREQ,	���:SFROLEINFO(Array)*/
		public const uint MSREQ_SFROLE_GET 	= 0x70;

		/*ϵͳ�����ʸ�����,	����:SFROLEINFO,	���:null*/
		public const uint MSREQ_SFROLE_APPLY 	= 0x75;

		/*ϵͳ�����ʸ����,	����:SFROLEINFO,	���:null*/
		public const uint MSREQ_SFROLE_CHECK 	= 0x76;

		/*��ȡ�����,	����:CODINGTABLEREQ,	���:CODINGTABLE(Array)*/
		public const uint MSREQ_CODINGTABLE_GET 	= 0x100;

		/*���ñ����,	����:CODINGTABLE,	���:null*/
		public const uint MSREQ_CODINGTABLE_SET 	= 0x101;

		/*ɾ�������,	����:CODINGTABLE,	���:null*/
		public const uint MSREQ_CODINGTABLE_DEL 	= 0x102;

		/*��ȡ�����԰�,	����:MULTILANLIBREQ,	���:UNIMULTILANLIB(Array)*/
		public const uint MSREQ_MULTILANLIB_GET 	= 0x201;

		/*����ϵͳ״̬,	����:SYSREFRESHREQ,	���:null*/
		public const uint MSREQ_SYSTEM_REFRESH 	= 0x230;


	}
/*��������*/
	/*�����ӿ�--System*/
	/*��ʼ�ӿ�--Assert--�ʲ�����*/
/*��ʼ����*/
	public partial class PRAssert
	{

		/*��ȡ�ʲ��б�,	����:ASSERTREQ,	���:UNIASSERT(Array)*/
		public const uint MSREQ_ASSERT_GET 	= 0x10;

		/*�ʲ����,	����:UNIASSERT,	���:UNIASSERT*/
		public const uint MSREQ_ASSERT_WAREHOUSING 	= 0x20;

		/*RFID����,	����:RFIDBIND,	���:null*/
		public const uint MSREQ_ASSERT_RFIDBIND 	= 0x21;

		/*������,	����:ROOMCHG,	���:null*/
		public const uint MSREQ_ASSERT_CHGROOM 	= 0x22;

		/*�����˱��,	����:KEEPERCHG,	���:null*/
		public const uint MSREQ_ASSERT_CHGKEEPER 	= 0x23;

		/*ɾ���ʲ�,	����:UNIASSERT,	���:null*/
		public const uint MSREQ_ASSERT_DEL 	= 0x30;

		/*��ȡ�ʲ��̵��,	����:STOCKTAKINGREQ,	���:STOCKTAKING(Array)*/
		public const uint MSREQ_STOCKTAKING_GET 	= 0x110;

		/*�ʲ��̵�,	����:STOCKTAKING,	���:STOCKTAKING*/
		public const uint MSREQ_STOCKTAKING_DO 	= 0x120;

		/*��ȡ�̵��ʲ���ϸ��,	����:STDETAILREQ,	���:STDETAIL(Array)*/
		public const uint MSREQ_STDETAIL_GET 	= 0x130;

		/*�����ʲ��̵�,	����:STDETAIL,	���:null*/
		public const uint MSREQ_STDETAIL_DO 	= 0x135;

		/*��ȡ�豸���ϼ�¼��,	����:OUTOFSERVICEREQ,	���:OUTOFSERVICE(Array)*/
		public const uint MSREQ_OUTOFSERVICE_GET 	= 0x140;

		/*�����豸����,	����:OUTOFSERVICE,	���:OUTOFSERVICE*/
		public const uint MSREQ_OUTOFSERVICE_APPLY 	= 0x141;

		/*��׼�豸����,	����:OUTOFSERVICE,	���:OUTOFSERVICE*/
		public const uint MSREQ_OUTOFSERVICE_APPROVE 	= 0x142;

		/*�����豸��������,	����:OUTOFSERVICE,	���:null*/
		public const uint MSREQ_OUTOFSERVICE_DEL 	= 0x143;

		/*��ȡ�����豸��ϸ��,	����:OOSDETAILREQ,	���:OOSDETAIL(Array)*/
		public const uint MSREQ_OOSDETAIL_GET 	= 0x145;

		/*��ȡ�豸�����¼,	����:DEVDAMAGERECREQ,	���:DEVDAMAGEREC(Array)*/
		public const uint MSREQ_REPAIRREC_GET 	= 0x160;

		/*�豸����,	����:REPAIRAPPLY,	���:REPAIRAPPLY*/
		public const uint MSREQ_REPAIR_APPLY 	= 0x161;

		/*�豸�������,	����:REPAIROVER,	���:REPAIROVER*/
		public const uint MSREQ_REPAIR_OVER 	= 0x162;

		/*�����豸����,	����:REPAIRCANCEL,	���:null*/
		public const uint MSREQ_REPAIR_CANCEL 	= 0x163;

		/*��ȡ��Ӧ��(������������ά��),	����:COMPANYREQ,	���:UNICOMPANY(Array)*/
		public const uint MSREQ_COMPANY_GET 	= 0x310;

		/*�½��޸Ĺ�Ӧ��,	����:UNICOMPANY,	���:UNICOMPANY*/
		public const uint MSREQ_COMPANY_SET 	= 0x320;

		/*ɾ����Ӧ��,	����:UNICOMPANY,	���:null*/
		public const uint MSREQ_COMPANY_DEL 	= 0x330;

		/*��ȡ�豸��ʷ����,	����:ASSERTLOGREQ,	���:ASSERTLOG(Array)*/
		public const uint MSREQ_ASSERTLOG_GET 	= 0x410;


	}
/*��������*/
	/*�����ӿ�--Assert*/
	/*��ʼ�ӿ�--Attendance--���ڹ���*/
/*��ʼ����*/
	public partial class PRAttendance
	{

		/*��ȡ���ڹ���,	����:ATTENDRULEREQ,	���:ATTENDRULE(Array)*/
		public const uint MSREQ_ATTENDRULE_GET 	= 0x10;

		/*�޸Ŀ��ڹ���,	����:ATTENDRULE,	���:ATTENDRULE*/
		public const uint MSREQ_ATTENDRULE_SET 	= 0x20;

		/*ɾ�����ڹ���,	����:ATTENDRULE,	���:null*/
		public const uint MSREQ_ATTENDRULE_DEL 	= 0x30;

		/*��ȡ���ڼ�¼,	����:ATTENDRECREQ,	���:ATTENDREC(Array)*/
		public const uint MSREQ_ATTENDREC_GET 	= 0x100;

		/*��ȡ����ͳ��,	����:ATTENDSTATREQ,	���:ATTENDSTAT(Array)*/
		public const uint MSREQ_ATTENDSTAT_GET 	= 0x120;


	}
/*��������*/
	/*�����ӿ�--Attendance*/
	/*��ʼ�ӿ�--SubSys--��ϵͳͨ�Žӿ�*/
/*��ʼ����*/
	public partial class PRSubSys
	{

		/*��ϵͳ��¼,	����:SUBSYSLOGINREQ,	���:SUBSYSLOGINRES*/
		public const uint MSREQ_SUBSYS_LOGIN 	= 0x10;

		/*��ϵͳע��,	����:SUBSYSLOGOUTREQ,	���:null*/
		public const uint MSREQ_SUBSYS_LOGOUT 	= 0x11;

		/*�ϴ�IC�ռ�ʹ�ü�¼,	����:ICUSERECUP,	���:null*/
		public const uint MSREQ_ICUSEREC_UPLOAD 	= 0x200;

		/*�ϴ���ӡ��ӡɨ��ʹ�ü�¼,	����:PRINTRECUP,	���:null*/
		public const uint MSREQ_PRINTREC_UPLOAD 	= 0x300;

		/*�ϴ�ͼ�鳬�ڽɷѼ�¼,	����:BOOKOVERDUERECUP,	���:null*/
		public const uint MSREQ_BOOKOVERDUEREC_UPLOAD 	= 0x400;

		/*�ϴ�ΥԼ��¼,	����:BREACHRECUP,	���:null*/
		public const uint MSREQ_BREACHREC_UPLOAD 	= 0x500;


	}
/*��������*/
	/*�����ӿ�--SubSys*/
	/*��ʼ�ӿ�--SubIC--IC�ռ���ϵͳ�ӿ�*/
/*��ʼ����*/
	public partial class PRSubIC
	{

		/*��ȡ���޼䵱ǰ״̬,	����:STUDYROOMSTATREQ,	���:STUDYROOMSTAT(Array)*/
		public const uint MSREQ_STUDYROOMSTAT_GET 	= 0x500;

		/*��ȡ��λ��ǰ״̬,	����:SEATSTATREQ,	���:SEATSTAT(Array)*/
		public const uint MSREQ_SEATSTAT_GET 	= 0x510;


	}
/*��������*/
	/*�����ӿ�--SubIC*/
	/*��ʼ�ӿ�--SubRT--����ʵ����ϵͳ�ӿ�*/
/*��ʼ����*/
	public partial class PRSubRT
	{

		/*��ȡ����ʵ������,	����:RTDATAREQ,	���:RTDATA(Array)*/
		public const uint MSREQ_RTDATA_GET 	= 0x10;


	}
/*��������*/
	/*�����ӿ�--SubRT*/
	/*��ʼ�ӿ�--UniSta--�ڵ����*/
/*��ʼ����*/
	public partial class PRUniSta
	{

		/*�ڵ��¼,	����:STALOGINREQ,	���:STALOGINRES*/
		public const uint MSREQ_UNISTA_LOGIN 	= 0x10;

		/*�ڵ��˳�,	����:STALOGOUTREQ,	���:null*/
		public const uint MSREQ_UNISTA_LOGOUT 	= 0x20;

		/*�ڵ�����֤��������ʱͨ��,	����:HANDSHAKEREQ,	���:HANDSHAKERES*/
		public const uint MSREQ_UNISTA_HANDSHAKE 	= 0x30;

		/*�ϴ�ģ������Ϣ,	����:MODMONIUP,	���:null*/
		public const uint MSREQ_MODMONI_UPLOAD 	= 0x200;

		/*�ϴ����ָ����Ϣ,	����:MONINDEXUP,	���:null*/
		public const uint MSREQ_MONIINDEX_UPLOAD 	= 0x201;

		/*�ϴ����ָ���¼,	����:MONIRECUP,	���:null*/
		public const uint MSREQ_MONIREC_UPLOAD 	= 0x202;


	}
/*��������*/
	/*�����ӿ�--UniSta*/
	/*��ʼ�ӿ�--UniMoni--�Զ����*/
/*��ʼ����*/
	public partial class PRUniMoni
	{

		/*��ϵͳ��ȡ�����Ϣȱʡֵ,	����:,	���:MONINDEX(Array)*/
		public const uint MSREQ_SUBMONI_FROMSRV 	= 0x10;

		/*��ϵͳ��������Ϣȱʡֵ��������,	����:MONINDEX,	���:null*/
		public const uint MSREQ_SUBMONI_TOSRV 	= 0x11;

		/*�����Ϣ״̬�ı�,	����:MONINDEX,	���:null*/
		public const uint MSREQ_SUBMONI_ONEINDEXCHG 	= 0x20;

		/*�����Ϣ״̬�ı�,	����:MONINDEX,	���:null*/
		public const uint MSREQ_SUBMONI_INDEXCHG 	= 0x21;

		/*����Ա��ȡ��ϸ�����Ϣ,	����:MONIREQ,	���:MODMONI(Array)*/
		public const uint MSREQ_MONI_GET 	= 0x101;

		/*����Ա��ȡ��ϸ�����Ϣ��־,	����:MONIRECREQ,	���:MONIREC(Array)*/
		public const uint MSREQ_MONIREC_GET 	= 0x111;

		/*����֧�ִ������,	����:MONIDEALERR,	���:null*/
		public const uint MSREQ_MONI_DEALERR 	= 0x211;


	}
/*��������*/
	/*�����ӿ�--UniMoni*/

}
//