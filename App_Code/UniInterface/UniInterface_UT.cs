
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
using System.Xml.Serialization;


namespace UniWebLib
{

	/*��ʼ���ݽṹ*/

	/*�汾��Ϣ*/
	public struct UNIVERSION
	{
		private Reserved reserved;
		
		public string szVersion;		/*�汾	XX.XX.XXXXXXXX*/
	
		public uint? dwWarrant;		/*��һ��ͨ�Խ�ģʽ*/
	
	public UNILICENSE szLicInfo;		/*��Ȩ��Ϣ(UNILICENSE�ṹ)*/
		};

	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/

	/*����Ա��¼����*/
	public struct ADMINLOGINREQ
	{
		private Reserved reserved;
		
		public uint? dwStaSN;		/*վ����*/
	
		public uint? dwLoginRole;		/*��¼ģʽ*/
	
		[FlagsAttribute]
		public enum DWLOGINROLE : uint
		{
			
				[EnumDescription("����Ա��¼")]
				LOGIN_MANAGER = 0x1,
			
				[EnumDescription("��ʦ��¼")]
				LOGIN_TEACHER = 0x2,
			
				[EnumDescription("�û���¼")]
				LOGIN_USER = 0x4,
			
				[EnumDescription("ҡһҡ")]
				LOGIN_SHAKE = 0x8,
			
				[EnumDescription("�û���¼����")]
				LOGIN_MASK = 0xFF,
			
				[EnumDescription("���Ե�¼")]
				LOGINEXT_PC = 0x100,
			
				[EnumDescription("�ֻ���΢�ţ���¼")]
				LOGINEXT_HP = 0x200,
			
				[EnumDescription("����̨��¼")]
				LOGINEXT_CONSOLE = 0x400,
			
				[EnumDescription("�û���¼��չ����")]
				LOGINEXT_MASK = 0xFF00,
			
		}

	
		public string szVersion;		/*�汾	XX.XX.XXXXXXXX,���°汾��������*/
	
		[FlagsAttribute]
		public enum SZVERSION : uint
		{
			
				[EnumDescription("���汾")]
				INTVER_MAIN = 3,
			
				[EnumDescription("�ΰ汾")]
				INTVER_RELEASE = 0,
			
				[EnumDescription("�ڲ��汾")]
				INTVER_INTERNAL = 20161208,
			
		}

	
		public string szLogonName;		/*��¼��*/
	
		public string szPassword;		/*����*/
	
		public string szIP;		/*IP��ַ*/
	
		public string szMSN;		/*΢�ź�*/
		};

	/*����Ա��¼Ӧ��*/
	public struct ADMINLOGINRES
	{
		private Reserved reserved;
		
		public uint? dwSessionID;		/*�����������SessionID*/
	
	public UNIVERSION SrvVer;		/*UNIVERSION �ṹ*/
	
		public uint? dwSupportSubSys;		/*��Ȩ��ϵͳ*/
	
		[FlagsAttribute]
		public enum DWSUPPORTSUBSYS : uint
		{
			
				[EnumDescription("���޼����ϵͳ")]
				SUBSYS_STUDYROOM = 0x1,
			
				[EnumDescription("���������ҹ���ϵͳ")]
				SUBSYS_EROOM = 0x2,
			
				[EnumDescription("��λ����ϵͳ")]
				SUBSYS_SEAT = 0x4,
			
				[EnumDescription("�豸��ӹ���")]
				SUBSYS_LOAN = 0x8,
			
				[EnumDescription("ʵ���ҹ���ϵͳ")]
				SUBSYS_TEACHING = 0x10,
			
				[EnumDescription("����ʵ�飨���ǣ�����")]
				SUBSYS_RESEARCH = 0x20,
			
				[EnumDescription("���ݹ���ϵͳ")]
				SUBSYS_SITE = 0x40,
			
				[EnumDescription("�ʲ�����ϵͳ")]
				SUBSYS_ASSERT = 0x80,
			
		}

	
		public uint? dwManRole;		/*����Ա��ɫ*/
	
		[FlagsAttribute]
		public enum DWMANROLE : uint
		{
			
				[EnumDescription("����Ա")]
				MANIDENT_ADMIN = 0x1,
			
				[EnumDescription("ѧ��(�û�)")]
				MANIDENT_USER = 0x2,
			
				[EnumDescription("��ʦ")]
				MANIDENT_TEACHER = 0x4,
			
				[EnumDescription("��ʦ")]
				MANIDENT_TUTOR = 0x8,
			
				[EnumDescription("����(examine and approve)")]
				MANIDENT_EANDA = 0x10,
			
				[EnumDescription("�����û�")]
				MANIDENT_ANONYMOUS = 0x80,
			
				[EnumDescription("��������Ա")]
				MANROLE_SUPER = 0x100,
			
				[EnumDescription("����Ա")]
				MANROLE_OPERATOR = 0x200,
			
				[EnumDescription("�쵼")]
				MANROLE_LEADER = 0x400,
			
				[EnumDescription("ֵ��Ա")]
				MANROLE_ATTENDANT = 0x800,
			
				[EnumDescription("ϵͳ������Ա")]
				MANSCOPE_SYSTEM = 0x10000,
			
				[EnumDescription("վ�㼶����Ա")]
				MANSCOPE_STATION = 0x20000,
			
				[EnumDescription("ʵ�����ļ�����Ա")]
				MANSCOPE_LABCTR = 0x40000,
			
				[EnumDescription("ʵ���Ҽ�����Ա")]
				MANSCOPE_LAB = 0x80000,
			
				[EnumDescription("���伶����Ա")]
				MANSCOPE_ROOM = 0x100000,
			
				[EnumDescription("�豸������Ա")]
				MANSCOPE_DEV = 0x200000,
			
				[EnumDescription("������")]
				MANEXT_DCS = 0x1000000,
			
				[EnumDescription("���Կͻ���")]
				MANEXT_DEVCLIENT = 0x2000000,
			
				[EnumDescription("����̨")]
				MANEXT_CONSOLE = 0x4000000,
			
				[EnumDescription("���Ե�¼")]
				MANEXT_PC = 0x8000000,
			
				[EnumDescription("�ֻ���¼")]
				MANEXT_HP = 0x10000000,
			
				[EnumDescription("�ֻ�΢�ŵ�¼")]
				MANEXT_MSN = 0x20000000,
			
				[EnumDescription("��������ϵͳ��¼")]
				MANEXT_SUBSYS = 0x40000000,
			
				[EnumDescription("�������")]
				MANMASK_IDENT = 0xFF,
			
				[EnumDescription("��ɫ����")]
				MANMASK_ROLE = 0xFF00,
			
				[EnumDescription("��Χ����")]
				MANMASK_SCOPE = 0xFF0000,
			
				[EnumDescription("��չ����")]
				MANMASK_EXT = 0xFF000000,
			
		}

	
		public uint? dwUserStat;		/*�û�״̬*/
	
		[FlagsAttribute]
		public enum DWUSERSTAT : uint
		{
			
				[EnumDescription("δָ����ʦ")]
				USTAT_NOTUTOR = 0x10,
			
				[EnumDescription("��ʦδȷ��")]
				USTAT_TUTORUNDO = 0x20,
			
				[EnumDescription("��ʦ�Ѿܽ�")]
				USTAT_TUTORREJECT = 0x40,
			
				[EnumDescription("��ʦ������")]
				USTAT_TUTOROK = 0x80,
			
		}

	
	public UNIADMIN AdminInfo;		/*UNIADMIN �ṹ*/
	
	public USERROLE[] UserRole;		/*USERROLE��*/
	
	public UNISTATION[] StaInfo;		/*CUniTable[UNISTATION]*/
	
	public UNIACCOUNT AccInfo;		/*�û���Ϣ(UNIACCOUNT�ṹ)*/
	
	public UNITUTOR TutorInfo;		/*�û���Ϣ(UNITUTOR�ṹ)*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*�ֻ���¼����*/
	public struct MOBILELOGINREQ
	{
		private Reserved reserved;
		
		public uint? dwStaSN;		/*վ����*/
	
		public uint? dwLoginRole;		/*��¼ģʽ*/
	
		public string szVersion;		/*�汾	XX.XX.XXXXXXXX*/
	
		public string szLogonName;		/*��¼��*/
	
		public string szPassword;		/*����*/
	
		public string szIP;		/*IP��ַ*/
	
		public string szMSN;		/*΢�ź�*/
	
		public uint? dwProperty;		/*��չ����*/
	
		[FlagsAttribute]
		public enum DWPROPERTY : uint
		{
			
				[EnumDescription("��֤�ɹ���΢�ź�")]
				MINPROP_BINDMSN = 1,
			
		}

		};

	/*����̨�˳�����*/
	public struct ADMINLOGOUTREQ
	{
		private Reserved reserved;
		
		public uint? dwAccNo;		/*�˺�*/
	
		public string szLogonName;		/*��¼��*/
	
		public string szIP;		/*IP��ַ*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*����̨�˳���Ӧ*/
	public struct ADMINLOGOUTRES
	{
		private Reserved reserved;
		
		public string szMemo;		/*˵����Ϣ*/
		};

	/*�ֻ�ҡһҡ��¼����*/
	public struct SHAKELOGINREQ
	{
		private Reserved reserved;
		
		public uint? dwStaSN;		/*վ����*/
	
		public string szVersion;		/*�汾	XX.XX.XXXXXXXX*/
	
		public string szLogonName;		/*��¼��*/
	
		public string szPassword;		/*����*/
	
		public string szIP;		/*IP��ַ*/
	
		public string szOpenId;		/*ҡһҡ΢�ź�OpenID*/
	
		public uint? dwProperty;		/*��չ����*/
	
		[FlagsAttribute]
		public enum DWPROPERTY : uint
		{
			
				[EnumDescription("��֤�ɹ���ҡһҡ΢�ź�")]
				MINPROP_BINDOPENID = 1,
			
		}

		};

	/*ҡһҡ��¼Ӧ��*/
	public struct SHAKELOGINRES
	{
		private Reserved reserved;
		
		public uint? dwSessionID;		/*�����������SessionID*/
	
	public UNIVERSION SrvVer;		/*UNIVERSION �ṹ*/
	
		public string szPID;		/*ѧ����*/
	
		public string szTrueName;		/*����*/
	
		public string szDispInfo;		/*��ʾ��Ϣ*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*��ȡϵͳ֧�ֵ�UID*/
	public struct UIDINFOREQ
	{
		private Reserved reserved;
		
		public uint? dwUidSN;		/*UID���*/
	
		public uint? dwFuncSN;		/*��������ģ����*/
	
		public uint? dwUIDType;		/*��������*/
	
		public string szUIDName;		/*UID����*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*ϵͳ֧�ֵ�UID*/
	public struct UIDINFO
	{
		private Reserved reserved;
		
		public uint? dwUidSN;		/*UID���*/
	
		public uint? dwFuncSN;		/*��������ģ����*/
	
		public string szFuncName;		/*��������ģ������*/
	
		public uint? dwUIDType;		/*��������*/
	
		[FlagsAttribute]
		public enum DWUIDTYPE : uint
		{
			
				[EnumDescription("��ѯ")]
				UIDTYPE_QUERY = 0x1,
			
				[EnumDescription("�½�")]
				UIDTYPE_NEW = 0x2,
			
				[EnumDescription("�޸�")]
				UIDTYPE_CHG = 0x4,
			
				[EnumDescription("ɾ��")]
				UIDTYPE_DEL = 0x8,
			
		}

	
		public string szUIDName;		/*UID����*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*�ֶ�����*/
	public struct FIELDLIMIT
	{
		private Reserved reserved;
		
		public string szFieldName;		/*�ֶ�����*/
	
		public string szMask;		/*�Ը��ֶδ�����*/
		};

	/*UIDȨ����ϸ*/
	public struct PRIVUID
	{
		private Reserved reserved;
		
		public uint? dwPrivID;		/*Ȩ��ID*/
	
		public uint? dwUidSN;		/*UID���*/
	
		public uint? dwUIDType;		/*��������*/
	
		public uint? dwFuncSN;		/*��������ģ����*/
	
		public string szFuncName;		/*��������ģ������*/
	
		public string szUIDName;		/*UID����*/
	
		public uint? dwWarrantType;		/*��ɷ�ʽ*/
	
		[FlagsAttribute]
		public enum DWWARRANTTYPE : uint
		{
			
				[EnumDescription("ȫ�����")]
				WARRANTTYPE_FULL = 0x1,
			
				[EnumDescription("������ɣ�����������FIELDLIMIT���壩")]
				WARRANTTYPE_PART = 0x2,
			
				[EnumDescription("���ܷ���")]
				WARRANTTYPE_FORBID = 0x100,
			
		}

	
	public FIELDLIMIT[] FieldLimit;		/*��ӦUID�ĸ��ֶ����ƹ���*/
		};

	/*��ȡ����Ȩ������*/
	public struct OPPRIVREQ
	{
		private Reserved reserved;
		
		public uint? dwOPID;		/*����Ȩ��ID*/
	
		public string szOPName;		/*����Ȩ�����ƣ�ģ��ƥ�䣩*/
	
		public uint? dwFuncSN;		/*��������ģ����*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*����Ȩ��*/
	public struct OPPRIV
	{
		private Reserved reserved;
		
		public uint? dwOPID;		/*����Ȩ��ID*/
	
		public string szOPName;		/*����Ȩ������*/
	
		public uint? dwDefWarType;		/*ȱʡ��ɷ�ʽ�������PRIVUID��*/
	
		public uint? dwSysFuncMask;		/*֧��ϵͳ����ģ��*/
	
		public uint? dwFuncSN;		/*��������ģ����*/
	
		public string szFuncName;		/*��������ģ������*/
	
	public PRIVUID[] PrivUID;		/*��UIDȨ����ϸ��(CUniTable<PRIVUID>)*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*��ȡ�û���ɫ*/
	public struct USERROLEREQ
	{
		private Reserved reserved;
		
		public uint? dwRoleID;		/*��ɫID*/
	
		public string szRoleName;		/*��ɫ���ƣ�ģ��ƥ�䣩*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*�û���ɫ*/
	public struct USERROLE
	{
		private Reserved reserved;
		
		public uint? dwRoleID;		/*��ɫID*/
	
		public string szRoleName;		/*��ɫ����*/
	
		public uint? dwDefWarType;		/*ȱʡ��ɷ�ʽ�������PRIVUID��*/
	
		public uint? dwSysFuncMask;		/*֧��ϵͳ����ģ��*/
	
	public OPPRIV[] OpPriv;		/*����Ȩ�ޱ�(CUniTable<OPPRIV>)*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*�ͻ���ж������ṹ*/
	public struct CLTPASSWD
	{
		private Reserved reserved;
		
		public uint? dwPassWdCode;		/*����CODE*/
	
		public string szPassword;		/*����*/
	
		public uint? dwSetDate;		/*��������*/
	
		public string szOperator;		/*���ù���Ա*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/**/
	public struct ADMINREQ
	{
		private Reserved reserved;
		
		public uint? dwStaSN;		/*�ڵ���*/
	
		public uint? dwAccNo;		/*�ʺ�*/
	
		public string szLogonName;		/*��¼��*/
	
		public string szTrueName;		/*����*/
	
		public uint? dwDeptID;		/*����ID*/
	
		public string szDeptName;		/*����*/
	
		public uint? dwManRole;		/*����Ա��ɫ*/
	
		public uint? dwIdent;		/*����Ա���*/
	
		public uint? dwProperty;		/*����Ա���ԣ����¶���+������ͣ�*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*����Ա��Ϣ*/
	public struct UNIADMIN
	{
		private Reserved reserved;
		
		public uint? dwStaSN;		/*�ڵ���*/
	
		public uint? dwAccNo;		/*�ʺ�*/
	
		public uint? dwManRole;		/*����Ա��ɫ*/
	
		public uint? dwStatus;		/*״̬*/
	
		public uint? dwProperty;		/*����Ա���ԣ����¶���+������ͣ�*/
	
		[FlagsAttribute]
		public enum DWPROPERTY : uint
		{
			
				[EnumDescription("ϵͳר�ù���Ա")]
				ADMINPROP_SYS = 1,
			
				[EnumDescription("���û���ݵĹ���Ա")]
				ADMINPROP_USER = 2,
			
		}

	
		public uint? dwExpDate;		/*������*/
	
		public string szLogonName;		/*��¼��*/
	
		public string szTrueName;		/*����*/
	
		public uint? dwIdent;		/*����Ա���*/
	
		public uint? dwManLevel;		/*����Ա���𣨱���ѧԺ����У����)*/
	
		[FlagsAttribute]
		public enum DWMANLEVEL : uint
		{
			
				[EnumDescription("ѧԺ������Ա")]
				MANLEVEL_DEPT = 5,
			
				[EnumDescription("У������Ա")]
				MANLEVEL_SCHOOL = 10,
			
		}

	
		public uint? dwDeptID;		/*����ID*/
	
		public string szDeptName;		/*����*/
	
		public string szTel;		/*�绰*/
	
		public string szHandPhone;		/*�ֻ�*/
	
		public string szEmail;		/*����*/
	
	public USERROLE[] UserRole;		/*��ɫ��(CUniTable<USERROLE>)*/
	
		public string szMemo;		/*��ע*/
		};

	/*��ȡ����������*/
	public struct MANROOMREQ
	{
		private Reserved reserved;
		
		public uint? dwAccNo;		/*�ʺ�*/
	
		public uint? dwManFlag;		/*�����־(�ձ�ʾ��ȡȫ����0��ȡδ�����䣬1��ȡ������)*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*����Ա������*/
	public struct MANROOM
	{
		private Reserved reserved;
		
		public uint? dwRoomID;		/*����ID*/
	
		public string szRoomNo;		/*������*/
	
		public string szRoomName;		/*��������*/
	
		public uint? dwManGroupID;		/*����Ա��ID*/
	
		public uint? dwManFlag;		/*�����־(0û��Ȩ�ޣ�1�й���Ȩ��)*/
		};

	/**/
	public struct ADMINCHECK
	{
		private Reserved reserved;
		
		public uint? dwSubjectID;		/*ȷ������ID*/
	
		public uint? dwSubjectType;		/*ȷ���������*/
	
		[FlagsAttribute]
		public enum DWSUBJECTTYPE : uint
		{
			
				[EnumDescription("���ԤԼ")]
				CHECK_RESV = 1,
			
				[EnumDescription("���ʵ��ƻ�")]
				CHECK_TESTPLAN = 2,
			
				[EnumDescription("�����ļ���Ա")]
				CHECK_GROUPMEMBER = 3,
			
				[EnumDescription("����˵�")]
				CHECK_BILL = 4,
			
				[EnumDescription("����豸ʹ����� szSubjectInfo �ɴ��USEDDEVCHECKINFO")]
				CHECK_USEDDEV = 5,
			
				[EnumDescription("��˻����")]
				CHECK_ACTIVITYPLAN = 6,
			
				[EnumDescription("��ʦ���ѧ��")]
				CHECK_TUTORSTUDENT = 7,
			
				[EnumDescription("ϵͳ����ʹ���ʸ����")]
				CHECK_SYSFUNCROLE = 8,
			
				[EnumDescription("����ԤԼ���")]
				CHECK_YARDRESV = 9,
			
		}

	
		public uint? dwCheckStat;		/*����Ա���״̬(��չ�ɸ�������ȷ��ʱ����)*/
	
		[FlagsAttribute]
		public enum DWCHECKSTAT : uint
		{
			
				[EnumDescription("����Ա�������")]
				ADMINCHECK_MASK = 0xFF,
			
				[EnumDescription("�����")]
				CHECKSTAT_DOING = 0x1,
			
				[EnumDescription("����Ա���ͨ��")]
				CHECKSTAT_ADMINOK = 0x2,
			
				[EnumDescription("����Աδ���ͨ��")]
				CHECKSTAT_ADMINFAIL = 0x4,
			
				[EnumDescription("�ȴ��������������������ˣ�")]
				CHECKSTAT_WAIT = 0x8,
			
				[EnumDescription("�����")]
				CHECKSTAT_CANDO = 0x10,
			
				[EnumDescription("�貹����Ϻ������")]
				CHECKSTAT_NEEDMORE = 0x20,
			
				[EnumDescription("�����(��ǰ��ĳɹ���ʧ������)")]
				CHECKSTAT_REDO = 0x40,
			
				[EnumDescription("��������ˣ��������ţ�")]
				CHECKSTAT_NOFINAL = 0x80,
			
				[EnumDescription("���ͨ��")]
				CHECKSTAT_OK = (CHECKSTAT_ADMINOK),
			
				[EnumDescription("���δͨ��")]
				CHECKSTAT_FAIL = (CHECKSTAT_DOING|CHECKSTAT_ADMINFAIL),
			
		}

	
		public uint? dwApplicantID;		/*�������˺�*/
	
		public string szApplicantName;		/*����������*/
	
		public string szCheckDetail;		/*���˵��*/
	
		public string szMemo;		/*��ע*/
	
		public string szSubjectInfo;		/*��Ӧ��ȷ�����ɽṹ��ϸ��Ϣ*/
		};

	/*�豸ʹ�������Ϣ*/
	public struct USEDDEVCHECKINFO
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*��ˮ��*/
	
		public uint? dwAccNo;		/*ʹ����*/
	
		public string szTrueName;		/*ʹ��������*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public uint? dwDevID;		/*�豸ID*/
	
		public uint? dwCheckStat;		/*�豸״̬*/
	
		public uint? dwCompensation;		/*�⳥���*/
	
		public uint? dwPunishScore;		/*���ÿ۷�*/
	
		public string szDamageInfo;		/*��˵��*/
	
		public string szExtInfo;		/*�豸������*/
		};

	/*��ȡ�����Ϣ*/
	public struct CHECKREQ
	{
		private Reserved reserved;
		
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
		public uint? dwCheckStat;		/*״̬*/
	
		public uint? dwSubjectID;		/*����ID*/
	
		public uint? dwSubjectType;		/*�������*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*�����Ϣ*/
	public struct CHECKINFO
	{
		private Reserved reserved;
		
		public uint? dwSubjectID;		/*ȷ������ID*/
	
		public uint? dwSubjectType;		/*ȷ���������*/
	
		public uint? dwCheckStat;		/*����Ա���״̬(��չ�ɸ�������ȷ��ʱ����)*/
	
		public uint? dwOccurDate;		/*��ʼ����*/
	
		public uint? dwOccurTime;		/*����ʱ��*/
	
		public uint? dwApplicantID;		/*�������˺�*/
	
		public string szApplicantName;		/*����������*/
	
		public string szMemo;		/*��ע*/
		};

	/*��ȡ�����Ϣ��־*/
	public struct CHECKLOGREQ
	{
		private Reserved reserved;
		
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
		public uint? dwCheckStat;		/*״̬*/
	
		public uint? dwSubjectID;		/*����ID*/
	
		public uint? dwSubjectType;		/*�������*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*�����Ϣ��־*/
	public struct CHECKLOG
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*��ˮ��*/
	
		public uint? dwCheckStat;		/*����Ա���״̬(��չ�ɸ�������ȷ��ʱ����)*/
	
		public uint? dwSubjectID;		/*ȷ������ID*/
	
		public uint? dwSubjectType;		/*ȷ���������*/
	
		public uint? dwOccurDate;		/*��ʼ����*/
	
		public uint? dwOccurTime;		/*���ʱ��*/
	
		public uint? dwAdminID;		/*������ʺ�*/
	
		public string szAdminName;		/*�����*/
	
		public uint? dwApplicantID;		/*�������˺�*/
	
		public string szApplicantName;		/*����������*/
	
		public string szCheckDetail;		/*���˵��*/
	
		public string szMemo;		/*��ע*/
		};

	/*��ȡˢ�±�־����*/
	public struct REFRESHFLAGREQ
	{
		private Reserved reserved;
		
		public uint? dwRefreshType;		/*ˢ�����*/
	
		[FlagsAttribute]
		public enum DWREFRESHTYPE : uint
		{
			
				[EnumDescription("�豸�б�")]
				REFRESHTYPE_DEVICE = 0x1,
			
				[EnumDescription("ͨ��+�����б�")]
				REFRESHTYPE_ROOM = 0x2,
			
				[EnumDescription("����б�")]
				REFRESHTYPE_CHECK = 0x4,
			
		}

		};

	/*��ȡˢ�±�־����*/
	public struct REFRESHFLAGINFO
	{
		private Reserved reserved;
		
		public uint? dwRefreshType;		/*ˢ�����*/
	
		public uint? dwRefreshFlag;		/*ˢ�±�־*/
		};

	/*��ȡ�ڼ���*/
	public struct HOLIDAYREQ
	{
		private Reserved reserved;
		
		public string szName;		/*���ƣ�ģ��ƥ�䣩*/
	
		public uint? dwDate;		/*����*/
		};

	/*�ڼ�����Ϣ*/
	public struct UNIHOLIDAY
	{
		private Reserved reserved;
		
		public string szName;		/*���ƣ�ģ��ƥ�䣩*/
	
		public uint? dwStartDay;		/*��ʼ����(MMDD��YYYYMMDD)*/
	
		public uint? dwEndDay;		/*��������(MMDD��YYYYMMDD)*/
	
		public string szMemo;		/*����˵��*/
		};

	/*���ĳ��ֵ�Ƿ��������*/
	public struct CHECKEXISTREQ
	{
		private Reserved reserved;
		
		public uint? dwUID;		/*�����½��޸ĵ�ID������MSREQ_ADMIN_SET*/
	
		public string szName;		/*�жϵ��ֶ�����(�����Ӧ�Ľṹ���������ͬ,����szLogonName*/
	
		public string szValue;		/*��Ҫ����ֵ�������ֵ�ת�����ַ���*/
	
		public string szCon;		/*SQL�������ֵ����Ϊ��*/
		};

	/*��ȡĳ���ֶε����ֵ����*/
	public struct MAXVALUEREQ
	{
		private Reserved reserved;
		
		public uint? dwUID;		/*�����½��޸ĵ�ID������MSREQ_ADMIN_SET*/
	
		public string szName;		/*�жϵ��ֶ�����(�����Ӧ�Ľṹ���������ͬ,����szLogonName*/
	
		public string szCon;		/*SQL�������ֵ����Ϊ��*/
		};

	/*�������ֵ*/
	public struct MAXVALUE
	{
		private Reserved reserved;
		
		public uint? dwValue;		/*���ص����ֵ*/
		};

	/*����������Ա���������Ϣ����*/
	public struct IFPARAMREQ
	{
		private Reserved reserved;
		
		public uint? dwAdminID;		/*����ԱID*/
		};

	/*����������Ա���������Ϣ*/
	public struct IFPARAM
	{
		private Reserved reserved;
		
		public uint? dwAdminID;		/*����ԱID*/
	
		public string szParam;		/*����*/
	
		public string szMemo;		/*��ע*/
		};

	/*��ȡ����Ա������־�����*/
	public struct ADMINLOGREQ
	{
		private Reserved reserved;
		
		public uint? dwStaSN;		/*�ڵ���*/
	
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
		public uint? dwAdminID;		/*����ԱID*/
	
		public string szTrueName;		/*��ʵ������ģ��ƥ�䣩*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*����Ա������־*/
	public struct ADMINLOG
	{
		private Reserved reserved;
		
		public uint? dwStaSN;		/*�ڵ���*/
	
		public uint? dwAdminID;		/*����ԱID*/
	
		public string szTrueName;		/*��ʵ����*/
	
		public uint? dwOpDate;		/*��������*/
	
		public uint? dwOpTime;		/*����ʱ��*/
	
		public uint? dwOpUID;		/*�����ӿ�ID*/
	
		public string szOpInfo;		/*��������*/
	
		public string szOpDetail;		/*������ϸ˵��*/
		};

	/*IP��ַ������*/
	public struct IPBLACKLIST
	{
		private Reserved reserved;
		
		public string szIP;		/*ip��ַ*/
	
		public string szTryAdmin;		/*�����˺�*/
	
		public uint? dwTryTimes;		/*���Դ���*/
	
		public uint? dwLockEndTime;		/*��������ʱ��*/
		};

	/*����Ա�޸�����*/
	public struct ADMINCHGPASSWD
	{
		private Reserved reserved;
		
		public string szCurAdminPw;		/*��ǰ��¼�Ĺ���Ա����*/
	
		public uint? dwAdminID;		/*����Ա�ʺ�*/
	
		public string szNewPw;		/*������*/
	
		public string szMemo;		/*��ע����չ�ã�*/
		};

	/*ϵͳ״̬*/
	public struct BSYSINFO
	{
		private Reserved reserved;
		
		public uint? dwStatus;		/*��״̬*/
	
	public MODMONI[] ParamStat;		/*���ָ��״̬*/
	
		public string szStatInfo;		/*״̬˵��*/
		};

	/*״̬ͳ����Ϣ*/
	public struct STATUSINFO
	{
		private Reserved reserved;
		
		public uint? dwStatus;		/*״ֵ̬*/
	
		public uint? dwNum;		/*����*/
		};

	/*�����Ϣͳ��*/
	public struct BCHECKSTAT
	{
		private Reserved reserved;
		
		public uint? dwStatus;		/*��״̬*/
	
	public STATUSINFO[] CheckStatInfo;		/*���ͳ�Ʊ�*/
	
		public string szStatInfo;		/*״̬˵��*/
		};

	/*�豸��Ϣͳ��*/
	public struct BDEVSTAT
	{
		private Reserved reserved;
		
		public uint? dwStatus;		/*��״̬*/
	
	public STATUSINFO[] DevStatInfo;		/*�豸״̬ͳ�Ʊ�*/
	
		public string szStatInfo;		/*״̬˵��*/
		};

	/*������Ϣͳ��*/
	public struct BROOMSTAT
	{
		private Reserved reserved;
		
		public uint? dwStatus;		/*��״̬*/
	
	public STATUSINFO[] RoomStatInfo;		/*����״̬ͳ�Ʊ�*/
	
		public string szStatInfo;		/*״̬˵��*/
		};

	/*���տγ�ͳ��*/
	public struct BTODAYRESVSTAT
	{
		private Reserved reserved;
		
		public uint? dwStatus;		/*��״̬*/
	
	public STATUSINFO[] TodayResvStatInfo;		/*���տγ�״̬ͳ�Ʊ�*/
	
		public string szStatInfo;		/*״̬˵��*/
		};

	/*�����ϻ�ͳ��*/
	public struct BFREEUSESTAT
	{
		private Reserved reserved;
		
		public uint? dwStatus;		/*��״̬*/
	
		public uint? dwCurUsers;		/*��ǰ����*/
	
	public STATUSINFO[] FreeTodayUseStat;		/*���������ϻ�ͳ��(��Сʱ)*/
	
		public string szStatInfo;		/*״̬˵��*/
		};

	/*������������Ϣ*/
	public struct BASICSTAT
	{
		private Reserved reserved;
		
		public uint? dwChgNum;		/*��Ϣ�����ı��ͳ����Ŀ��*/
	
		public uint? dwStatus;		/*״̬*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("����")]
				MONISTAT_OK = 1,
			
				[EnumDescription("����")]
				MONISTAT_INFO = 2,
			
				[EnumDescription("����")]
				MONISTAT_WARNNING = 4,
			
				[EnumDescription("����")]
				MONISTAT_ERROR = 8,
			
		}

	
	public BSYSINFO SysStat;		/*ϵͳ״̬*/
	
	public BCHECKSTAT CheckStat;		/*�����Ϣͳ��*/
	
	public BDEVSTAT DevStat;		/*�豸��Ϣͳ��*/
	
	public BROOMSTAT RoomStat;		/*������Ϣͳ��*/
	
	public BTODAYRESVSTAT TodayResvStat;		/*���տγ�ͳ��*/
	
	public BFREEUSESTAT FreeUseStat;		/*�����ϻ�ͳ��*/
	
		public string szStatInfo;		/*״̬˵��*/
		};

	/*����������*/
	public struct CHECKTYPEREQ
	{
		private Reserved reserved;
		
		public uint? dwCheckKind;		/*�������(�ɶ��)*/
	
		public uint? dwMainKind;		/*��˴���*/
		};

	/*������*/
	public struct CHECKTYPE
	{
		private Reserved reserved;
		
		public uint? dwCheckKind;		/*��˱�ţ��½�ʱ��ϵͳ�Զ����䣩*/
	
		public uint? dwMainKind;		/*��˴���*/
	
		[FlagsAttribute]
		public enum DWMAINKIND : uint
		{
			
				[EnumDescription("�豸�������")]
				ADMINCHECK_DEVMAN = 0x100,
			
				[EnumDescription("�������")]
				ADMINCHECK_SECURITY = 0x200,
			
				[EnumDescription("�������")]
				ADMINCHECK_PUBLICITY = 0x400,
			
				[EnumDescription("���ܵ�λ��ˣ����½���������ϵͳ���䣩")]
				ADMINCHECK_DIRECTOR = 0x800,
			
				[EnumDescription("��������")]
				ADMINCHECK_SERVICE = 0x1000,
			
				[EnumDescription("����������")]
				ADMINCHECKTYPE_MASK = 0xFFFF00,
			
		}

	
		public string szCheckName;		/*�������*/
	
		public uint? dwCheckLevel;		/*��˼���(ͬUNIADMIN.dwManLevel���壩*/
	
		public uint? dwDeptID;		/*���β���ID��ѧԺ�������ã������������Զ�ƥ�䣩*/
	
		public string szDeptName;		/*���β���*/
	
		public string szMemo;		/*״̬˵��*/
		};

	/*��ȡ�û������������*/
	public struct USERFEEDBACKREQ
	{
		private Reserved reserved;
		
		public uint? dwFeedKind;		/*��������*/
	
		public uint? dwFeedStat;		/*״̬*/
	
		public uint? dwPurpose;		/*��;*/
	
		public uint? dwResvID;		/*ԤԼID*/
	
		public uint? dwSNum;		/*��ˮ��*/
	
		public uint? dwAccNo;		/*�ʺ�*/
	
		public uint? dwDevID;		/*ʹ���豸*/
	
		public uint? dwMinScore;		/*�û��������*/
	
		public uint? dwMaxScore;		/*�û��������*/
	
		public uint? dwBeginDate;		/*ԤԼ��ʼ����*/
	
		public uint? dwEndDate;		/*ԤԼ��������*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*�û��������*/
	public struct USERFEEDBACK
	{
		private Reserved reserved;
		
		public uint? dwSNum;		/*��ˮ��*/
	
		public uint? dwAccNo;		/*�ʺ�*/
	
		public string szTrueName;		/*����*/
	
		public string szTel;		/*�绰*/
	
		public string szHandPhone;		/*�ֻ�*/
	
		public string szEmail;		/*����*/
	
		public uint? dwUserDeptID;		/*����ID*/
	
		public string szUserDeptName;		/*����*/
	
		public uint? dwFeedKind;		/*��������*/
	
		[FlagsAttribute]
		public enum DWFEEDKIND : uint
		{
			
				[EnumDescription("ʹ������")]
				FEEDKIND_EVALUATE = 1,
			
				[EnumDescription("�������")]
				FEEDKIND_ADVICE = 2,
			
				[EnumDescription("Ͷ��")]
				FEEDKIND_COMPLAIN = 4,
			
		}

	
		public uint? dwFeedStat;		/*״̬*/
	
		[FlagsAttribute]
		public enum DWFEEDSTAT : uint
		{
			
				[EnumDescription("�ȴ��ظ�")]
				FEEDSTAT_WAITREPLY = 1,
			
				[EnumDescription("�ѻظ�")]
				FEEDSTAT_REPLIED = 2,
			
		}

	
		public uint? dwPurpose;		/*��;*/
	
		public uint? dwResvID;		/*ԤԼID*/
	
		public uint? dwDevID;		/*ʹ���豸*/
	
		public uint? dwDevKind;		/*�豸����*/
	
		public uint? dwDevSN;		/*�豸���*/
	
		public string szDevName;		/*�豸����*/
	
		public uint? dwRoomID;		/*����ID*/
	
		public string szRoomName;		/*��������*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabName;		/*ʵ��������*/
	
		public uint? dwScore;		/*�û�����*/
	
		public uint? dwOccurDate;		/*��������*/
	
		public uint? dwOccurTime;		/*����ʱ��*/
	
		public string szIntroInfo;		/*������Ϣ*/
	
		public string szReplyInfo;		/*�ظ���Ϣ*/
	
		public uint? dwReplyDate;		/*�ظ�����*/
	
		public uint? dwReplyTime;		/*�ظ�ʱ��*/
	
		public uint? dwAnswererID;		/*�ظ��ʺ�*/
	
		public string szAnswerer;		/*�ظ���*/
	
		public string szMemo;		/*״̬˵��*/
		};

	/*�����������*/
	public struct SERVICETYPEREQ
	{
		private Reserved reserved;
		
		public uint? dwServiceKind;		/*�������(�ɶ��)*/
		};

	/*�������*/
	public struct UNISERVICETYPE
	{
		private Reserved reserved;
		
		public uint? dwServiceKind;		/*�����ţ��½�ʱ��ϵͳ�Զ����䣩*/
	
		public string szServiceName;		/*��������*/
	
		public uint? dwServiceLevel;		/*�����ż���(ͬUNIADMIN.dwManLevel���壩*/
	
		public uint? dwDeptID;		/*������ID��ѧԺ�������ã������������Զ�ƥ�䣩*/
	
		public string szDeptName;		/*������*/
	
		public uint? dwProperty;		/*�������*/
	
		[FlagsAttribute]
		public enum DWPROPERTY : uint
		{
			
				[EnumDescription("���������ͨ����ԤԼ������Ч")]
				SERVICEPROP_NEEDCHECK = 0x1,
			
		}

	
		public string szMemo;		/*˵��*/
		};

	/*��ȡ�û������������*/
	public struct POLLONLINEREQ
	{
		private Reserved reserved;
		
		public uint? dwPollID;		/*��ˮ��*/
	
		public uint? dwVoteStat;		/*ͶƱ״̬*/
	
		public uint? dwBeginDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*ͶƱѡ��*/
	public struct POLLITEM
	{
		private Reserved reserved;
		
		public uint? dwItemID;		/*ѡ���*/
	
		public string szItemName;		/*ѡ������*/
	
		public uint? dwGroupID;		/*��ID*/
	
		public uint? dwPollKind;		/*�������*/
	
		public uint? dwMaxTickItems;		/*���ɹ�ѡѡ��*/
	
		public uint? dwVotes;		/*��Ʊ��*/
	
		public string szMemo;		/*״̬˵��*/
		};

	/*����ͶƱ��Ϣ*/
	public struct POLLONLINE
	{
		private Reserved reserved;
		
		public uint? dwPollID;		/*ͶƱID*/
	
		public uint? dwAccNo;		/*�����ˣ������ˣ��ʺ�*/
	
		public string szTrueName;		/*����*/
	
		public string szTel;		/*�绰*/
	
		public string szHandPhone;		/*�ֻ�*/
	
		public string szEmail;		/*����*/
	
		public uint? dwVoteStat;		/*ͶƱ״̬*/
	
		[FlagsAttribute]
		public enum DWVOTESTAT : uint
		{
			
				[EnumDescription("δ����")]
				VOTESTAT_UNOPEN = 0x1,
			
				[EnumDescription("������")]
				VOTESTAT_OPENING = 0x2,
			
				[EnumDescription("�ѹر�")]
				VOTESTAT_CLOSED = 0x4,
			
				[EnumDescription("��ͶƱ")]
				VOTESTAT_DONE = 0x100,
			
		}

	
		public uint? dwPollScope;		/*�����Χ*/
	
		[FlagsAttribute]
		public enum DWPOLLSCOPE : uint
		{
			
				[EnumDescription("�����鿴")]
				POLLSCOPE_ANONYMOUS_LOOK = 0x1,
			
				[EnumDescription("ʵ���鿴")]
				POLLSCOPE_MEMBER_LOOK = 0x2,
			
				[EnumDescription("����ͶƱ")]
				POLLSCOPE_ANONYMOUS_VOTE = 0x100,
			
				[EnumDescription("ʵ��ͶƱ")]
				POLLSCOPE_MEMBER_VOTE = 0x200,
			
		}

	
		public uint? dwPollKind;		/*�������*/
	
		[FlagsAttribute]
		public enum DWPOLLKIND : uint
		{
			
				[EnumDescription("��ѡһ")]
				POLLKIND_MTICKS = 0x1,
			
				[EnumDescription("��ѡ��")]
				POLLKIND_MTICKM = 0x2,
			
				[EnumDescription("֧�ַ���")]
				POLLKIND_SUBGROUP = 0x4,
			
		}

	
		public uint? dwMaxTickItems;		/*���ɹ�ѡѡ��*/
	
		public string szPollSubject;		/*ͶƱ����*/
	
		public uint? dwBeginDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��ֹ����*/
	
		public uint? dwTotalUsers;		/*ͶƱ������*/
	
	public POLLITEM[] PollItems;		/*CUniTable[POLLITEM]*/
	
		public string szMemo;		/*״̬˵��*/
		};

	/*ͶƱ*/
	public struct POLLVOTE
	{
		private Reserved reserved;
		
		public uint? dwPollID;		/*ͶƱ���*/
	
		public uint? dwItemID;		/*ѡ���*/
	
		public string szMemo;		/*״̬˵��*/
		};

	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/

	/*��ȡվ��������*/
	public struct STATIONREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*��ʽ*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("ȫ��")]
				STATIONGET_BYALL = 0,
			
				[EnumDescription("�γ̴���")]
				STATIONGET_BYSN = 1,
			
		}

	
		public string szGetKey;		/*����ֵ*/
		};

	/*�豸��Ӳ��������Ϣ*/
	public struct DEVICECONFIG
	{
		private Reserved reserved;
		
		public uint? dwStaSN;		/*վ����*/
	
		public uint? dwCfgType;		/*��������*/
	
		[FlagsAttribute]
		public enum DWCFGTYPE : uint
		{
			
				[EnumDescription("����������")]
				CFGTYPE_SERVER = 1,
			
				[EnumDescription("���ݿ�����")]
				CFGTYPE_DATABASE = 2,
			
				[EnumDescription("��������")]
				CFGTYPE_NET = 4,
			
				[EnumDescription("�������")]
				CFGTYPE_SOFTWARE = 8,
			
				[EnumDescription("վ�����������")]
				CFGTYPE_STAMASK = 0xFF,
			
				[EnumDescription("�ն�����")]
				CFGTYPE_TERMINAL = 0x10000,
			
		}

	
		public string szCfgName;		/*��������*/
	
		public string szBrand;		/*Ʒ��*/
	
		public string szModel;		/*����ͺ�*/
	
		public string szSpecification;		/*�豸���*/
	
		public string szPurpose;		/*��;˵��*/
	
		public string szIndicators;		/*��Ҫָ��˵��*/
	
		public uint? dwPurchaseDate;		/*�ɹ����� YYYYMMDD*/
	
		public uint? dwStartUseDate;		/*Ͷ��ʹ������ YYYYMMDD*/
	
		public uint? dwDevLife;		/*�豸ʹ������*/
	
		public string szCPU;		/*CPU����ͺ�*/
	
		public uint? dwMemSize;		/*�ڴ��С��M��*/
	
		public uint? dwDiskSize;		/*Ӳ�̴�С(G)*/
	
		public uint? dwOsVer;		/*����ϵͳ�汾(dwMajorVersion*1000000 + dwMinorVersion*10000 + wProductType*100 +ϵͳ����(32λ��64λ) )*/
	
		public string szMemo;		/*��ע*/
	
		public uint? dwDelFlag;		/*ɾ����־*/
		};

	/**/
	public struct UNISTATION
	{
		private Reserved reserved;
		
		public uint? dwStaSN;		/*վ����*/
	
		public string szStaName;		/*վ������*/
	
		public string szLicSN;		/*��ɱ��*/
	
		public uint? dwSubSysSN;		/*��ϵͳ���*/
	
		[FlagsAttribute]
		public enum DWSUBSYSSN : uint
		{
			
				[EnumDescription("ͨ��ʵ����")]
				SUBSYS_LAB = 0x100,
			
				[EnumDescription("ICѧϰ�ռ䣨����ͼ������޼䡢���������Һ͸���ѧϰ�ռ䣩")]
				SUBSYS_IC = 0x200,
			
				[EnumDescription("����ʵ���ң����985���͵�ʵ���ң�")]
				SUBSYS_OPENLAB = (SUBSYS_LAB+0x1),
			
				[EnumDescription("��ѧʵ���ң��Խ�ѧΪ���ģ������ѧ�����ţ�")]
				SUBSYS_TEACHINGLAB = (SUBSYS_LAB+0x2),
			
				[EnumDescription("֧����ϵͳ����")]
				SUBSYS_VALIDMASK = 0x300,
			
		}

	
		public uint? dwStatus;		/*վ��״̬*/
	
		public uint? dwOwnerDept;		/*��������*/
	
		public uint? dwManagerID;		/*�������˺�*/
	
		public uint? dwAttendantID;		/*ֵ��Ա�˺�*/
	
		public string szDeptName;		/*��������*/
	
		public string szManName;		/*����������*/
	
		public string szAttendantName;		/*ֵ��Ա����*/
	
		public string szMemo;		/*��ע*/
		};

	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/

	/**/
	public struct UNIDEPT
	{
		private Reserved reserved;
		
		public uint? dwID;		/*ID*/
	
		public string szDeptSN;		/*��λ���*/
	
		public string szName;		/*����*/
	
		public uint? dwKind;		/*����*/
	
		[FlagsAttribute]
		public enum DWKIND : uint
		{
			
				[EnumDescription("ѧԺ")]
				DEPTKIND_SCHOOL = 0x100,
			
				[EnumDescription("��������")]
				DEPTKIND_SERVICE = 0x200,
			
				[EnumDescription("���ز���")]
				DEPTKIND_LOCAL = 0x1,
			
				[EnumDescription("ͬ������")]
				DEPTKIND_SYNC = 0x2,
			
				[EnumDescription("У�ⲿ��")]
				DEPTKIND_OUTER = 0x4,
			
				[EnumDescription("���ɼ�")]
				DEPTKIND_INVISIBLE = 0x10,
			
				[EnumDescription("ʹ�ɼ�")]
				DEPTKIND_SETVISIBLE = 0x80000000,
			
		}

	
		public string szMemo;		/*��ע*/
		};

	/**/
	public struct DEPTREQ
	{
		private Reserved reserved;
		
		public uint? dwID;		/*ID*/
	
		public string szName;		/*����*/
	
		public uint? dwKind;		/*����(�������Ż�ѧԺ)*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*У��*/
	public struct UNICAMPUS
	{
		private Reserved reserved;
		
		public uint? dwCampusID;		/*У��ID*/
	
		public string szCampusName;		/*У������*/
	
		public uint? dwCampusKind;		/*У�����ͣ���չ��*/
	
		public string szMemo;		/*��ע*/
		};

	/*��ȡУ������*/
	public struct CAMPUSREQ
	{
		private Reserved reserved;
		
		public uint? dwCampusID;		/*У��ID*/
	
		public string szCampusName;		/*У������*/
	
		public uint? dwCampusKind;		/*У�����ͣ���չ��*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/**/
	public struct CLASSREQ
	{
		private Reserved reserved;
		
		public uint? dwClassID;		/*ID*/
	
		public string szClassName;		/*����*/
	
		public uint? dwMajorID;		/*רҵID*/
	
		public uint? dwEnrolYear;		/*��ѧ���*/
	
		public uint? dwClassKind;		/*����*/
	
		public uint? dwDeptID;		/*����ID*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/**/
	public struct UNICLASS
	{
		private Reserved reserved;
		
		public uint? dwClassID;		/*ID*/
	
		public string szClassSN;		/*�༶���*/
	
		public string szClassName;		/*����*/
	
		public uint? dwClassKind;		/*����*/
	
		public uint? dwDeptID;		/*����ID*/
	
		public uint? dwMajorID;		/*רҵID*/
	
		public uint? dwEnrolYear;		/*��ѧ���*/
	
		public string szMemo;		/*��ע*/
		};

	/*��ȡ�˻��б��������*/
	public struct ACCREQ
	{
		private Reserved reserved;
		
		public uint? dwCardID;		/*��ID��*/
	
		public uint? dwAccNo;		/*�˺�*/
	
		public string szPID;		/*ѧ����*/
	
		public string szLogonName;		/*��¼��(ѧ����)*/
	
		public string szCardNo;		/*����*/
	
		public string szTrueName;		/*����*/
	
		public uint? dwIdent;		/*���*/
	
		public uint? dwStatus;		/*״̬*/
	
		public uint? dwEnrolYear;		/*��ѧ���(XX��)*/
	
		public uint? dwClassID;		/*�༶ID*/
	
		public uint? dwDeptID;		/*����ID*/
	
		public string szMSN;		/*MSN*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*�˻���Ϣ*/
	public struct UNIACCOUNT
	{
		private Reserved reserved;
		
		public uint? dwAccNo;		/*�˺�*/
	
		public string szPID;		/*ѧ����*/
	
		public string szLogonName;		/*��¼��(ѧ����)*/
	
		public string szCardNo;		/*����*/
	
		public uint? dwCardID;		/*��ID��*/
	
		public string szIDCard;		/*���֤��*/
	
		public string szTrueName;		/*����*/
	
		public string szPasswd;		/*��������*/
	
		public uint? dwClassID;		/*�༶ID*/
	
		public string szClassName;		/*�༶*/
	
		public uint? dwDeptID;		/*����ID*/
	
		public string szDeptName;		/*����*/
	
		public uint? dwMajorID;		/*רҵID*/
	
		public string szMajorName;		/*רҵ*/
	
		public uint? dwSex;		/*�Ա��UniCommon.h*/
	
		public uint? dwIdent;		/*��� ��UniCommon.h*/
	
		[FlagsAttribute]
		public enum DWIDENT : uint
		{
			
				[EnumDescription("��ʵ�����Чλ")]
				REALIDENT_MASK = 0x000FFFFF,
			
				[EnumDescription("��չ�����Чλ")]
				EXTIDENT_MASK = 0xFFF00000,
			
				[EnumDescription("��ʦ")]
				EXTIDENT_TUTOR = 0x100000,
			
				[EnumDescription("У�ڱ�������Ա")]
				EXTIDENT_DEPT = 0x200000,
			
				[EnumDescription("У����Ա")]
				EXTIDENT_INNER = 0x400000,
			
				[EnumDescription("У����Ա")]
				EXTIDENT_OUTER = 0x800000,
			
				[EnumDescription("������Ŀ������")]
				EXTIDENT_RTLEADER = 0x1000000,
			
				[EnumDescription("����Ա")]
				EXTIDENT_MANAGER = 0x10000000,
			
				[EnumDescription("��ʦ")]
				EXTIDENT_TEACHER = 0x20000000,
			
		}

	
		public uint? dwKind;		/*����*/
	
		[FlagsAttribute]
		public enum DWKIND : uint
		{
			
				[EnumDescription("�����û�������Чλ")]
				BASICKIND_MASK = 0xFFFF,
			
				[EnumDescription("��չ�û�������Чλ")]
				EXTKIND_MASK = 0xFFFF0000,
			
				[EnumDescription("���������Ѷ���")]
				EXTKIND_NOMSG = 0x10000,
			
		}

	
		public uint? dwBirthday;		/*��������*/
	
		public uint? dwEnrolYear;		/*��ѧ���(XX��)*/
	
		public uint? dwSchoolYears;		/*ѧ��*/
	
		public uint? dwBalance;		/*���*/
	
		public uint? dwSubsidy;		/*����*/
	
		public uint? dwFreeTime;		/*���ʱ��(��ʱ)*/
	
		public uint? dwUseQuota;		/*�����޶�*/
	
		public uint? dwStatus;		/*״̬ �����UniCommon.h*/
	
		public uint? dwExpiredDate;		/*������*/
	
		public string szTel;		/*�绰*/
	
		public string szHandPhone;		/*�ֻ�*/
	
		public string szEmail;		/*����*/
	
		public string szMSN;		/*MSN*/
	
		public string szQQ;		/*QQ*/
	
		public string szHomeAddr;		/*��ͥסַ*/
	
		public string szCurAddr;		/*У��סַ*/
	
		public string szMemo;		/*˵����Ϣ*/
	
		public string szCurZip;		/*������ַ�ʱ�(��ʦ��Ҫ)*/
	
		public uint? dwTutorID;		/*��ʦ�˺�*/
	
		public string szTutorName;		/*��ʦ����*/
		};

	/*��չ�˻���Ϣ*/
	public struct UNIACCEXTINFO
	{
		private Reserved reserved;
		
		public string pPhoto;		/*��Ƭ*/
		};

	/*��ȡ��ʦ*/
	public struct TUTORREQ
	{
		private Reserved reserved;
		
		public uint? dwTutorID;		/*��ʦ�˺�*/
	
		public uint? dwStudentAccNo;		/*ѧ���˺�*/
	
		public string szTrueName;		/*��ʦ����*/
	
		public uint? dwDeptID;		/*����ID*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*��ʦ��Ϣ*/
	public struct UNITUTOR
	{
		private Reserved reserved;
		
		public uint? dwAccNo;		/*�˺�*/
	
		public string szTrueName;		/*����*/
	
		public string szTel;		/*�绰*/
	
		public string szHandPhone;		/*�ֻ�*/
	
		public string szEmail;		/*����*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*��ȡ��չ�����Ա��Ϣ*/
	public struct EXTIDENTACCREQ
	{
		private Reserved reserved;
		
		public uint? dwIdent;		/*���*/
	
		public uint? dwAccNo;		/*�˺�*/
	
		public string szTrueName;		/*��ʦ����*/
	
		public uint? dwDeptID;		/*����ID*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*��ȡ�û���Ϣ*/
	public struct ACCINFOREQ
	{
		private Reserved reserved;
		
		public uint? dwAccNo;		/*�˺�*/
	
		public string szLogonName;		/*��¼��*/
		};

	/*��չ�����Ա��Ϣ*/
	public struct EXTIDENTACC
	{
		private Reserved reserved;
		
		public uint? dwAccNo;		/*�˺�*/
	
		public uint? dwIdent;		/*���*/
	
		public string szPID;		/*ѧ����*/
	
		public string szLogonName;		/*��¼��*/
	
		public string szTrueName;		/*����*/
	
		public uint? dwSex;		/*�Ա�*/
	
		public uint? dwDeptID;		/*����ID*/
	
		public string szDeptName;		/*����*/
	
		public string szTel;		/*�绰*/
	
		public string szHandPhone;		/*�ֻ�*/
	
		public string szEmail;		/*����*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*��ȡ��ʦ��ѧ��*/
	public struct TUTORSTUDENTREQ
	{
		private Reserved reserved;
		
		public uint? dwTutorID;		/*��ʦ�˺�*/
	
		public uint? dwStatus;		/*ѧ��״̬*/
	
		public uint? dwKind;		/*ѧ������*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*��ʦѧ����Ϣ*/
	public struct TUTORSTUDENT
	{
		private Reserved reserved;
		
		public uint? dwTutorID;		/*��ʦ�˺�*/
	
		public string szTutorName;		/*��ʦ����*/
	
		public uint? dwStatus;		/*ѧ��״̬�����״̬��ADMINCHECK��*/
	
		public uint? dwKind;		/*ѧ������*/
	
		[FlagsAttribute]
		public enum DWKIND : uint
		{
			
				[EnumDescription("˶ʿ")]
				TSKIND_MASTER = 1,
			
				[EnumDescription("��ʿ")]
				TSKIND_DOCTOR = 2,
			
		}

	
		public uint? dwAccNo;		/*�˺�*/
	
		public string szPID;		/*ѧ����*/
	
		public string szTrueName;		/*����*/
	
		public uint? dwEnrolYear;		/*��ѧ���(XX��)*/
	
		public string szTel;		/*�绰*/
	
		public string szHandPhone;		/*�ֻ�*/
	
		public string szEmail;		/*����*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*��ʦ���ѧ��*/
	public struct TUTORSTUDENTCHECK
	{
		private Reserved reserved;
		
		public uint? dwTutorID;		/*��ʦID*/
	
		public uint? dwCheckStat;		/*���״̬(ADMINCHECK����)*/
	
		public uint? dwStudentAccNo;		/*ѧ���˺�*/
	
		public string szStudentName;		/*ѧ������*/
	
		public string szCheckDetail;		/*���˵��*/
	
		public string szMemo;		/*��ע*/
		};

	/**/
	public struct ACCCHECKREQ
	{
		private Reserved reserved;
		
		public uint? dwCheckType;		/*��ʽ*/
	
		[FlagsAttribute]
		public enum DWCHECKTYPE : uint
		{
			
				[EnumDescription("ѧ����")]
				ACCCHECK_BYPERSONALID = 1,
			
				[EnumDescription("��¼��")]
				ACCCHECK_BYLOGONNAME = 2,
			
				[EnumDescription("����")]
				ACCCHECK_BYCARDNO = 4,
			
				[EnumDescription("��Web��ͨ����������֤")]
				ACCCHECK_BYWEB = 8,
			
				[EnumDescription("�ʺ�")]
				ACCCHECK_BYACCNO = 0x10,
			
				[EnumDescription("ά��32����")]
				ACCCHECK_BYCARDWG32 = 0x20,
			
				[EnumDescription("΢��ID��")]
				ACCCHECK_BYMSN = 0x40,
			
				[EnumDescription("΢��OpenID��(ҡһҡ)")]
				ACCCHECK_BYOPENID = 0x80,
			
				[EnumDescription("����֤����")]
				ACCCHECK_WITHPW = 0x100,
			
				[EnumDescription("�û��޸ĸ�����Ϣ")]
				ACCCHECK_SETINFO = 0x200,
			
				[EnumDescription("�ӿ���̨��¼")]
				ACCCHECK_CONLOGIN = 0x400,
			
				[EnumDescription("����֤�Ƿ���ڣ���������Ϣ")]
				ACCCHECK_NORETURN = 0x800,
			
				[EnumDescription("ǩ��")]
				ACCCHECK_SIGNIN = 0x1000,
			
		}

	
		public string szCheckKey;		/*��֤�ؼ���*/
	
		public string szCheckPW;		/*��֤����*/
	
	public UNIACCOUNT szAccInfo;		/*(UNIACCOUNT�ṹ)*/
		};

	/*���˿�ṹ*/
	public struct UNIDEPOSIT
	{
		private Reserved reserved;
		
		public uint? dwKind;		/*���*/
	
		[FlagsAttribute]
		public enum DWKIND : uint
		{
			
				[EnumDescription("�ֽ�ģʽ")]
				DPTKIND_CASH = 0x1000,
			
				[EnumDescription("�ֽ��ֵ")]
				DPTKIND_CASHADD = DPTKIND_CASH+0x001,
			
				[EnumDescription("�ֽ��˿�")]
				DPTKIND_CASHMINUS = DPTKIND_CASH+0x002,
			
				[EnumDescription("ת��")]
				DPTKIND_CASHTRANS = DPTKIND_CASH+0x003,
			
				[EnumDescription("�����ֽ�")]
				DPTKIND_DIRECTCASH = DPTKIND_CASH+0x004,
			
				[EnumDescription("����ģʽ")]
				DPTKIND_SUBSIDY = 0x2000,
			
				[EnumDescription("�Ӳ���")]
				DPTKIND_SUBSIDYADD = DPTKIND_SUBSIDY+0x001,
			
				[EnumDescription("������")]
				DPTKIND_SUBSIDYMINUS = DPTKIND_SUBSIDY+0x002,
			
				[EnumDescription("��������")]
				DPTKIND_SUBSIDYCLEAR = DPTKIND_SUBSIDY+0x004,
			
				[EnumDescription("��ʱģʽ")]
				DPTKIND_TIME = 0x3000,
			
				[EnumDescription("�ӻ�ʱ")]
				DPTKIND_TIMEADD = DPTKIND_TIME+0x001,
			
				[EnumDescription("����ʱ")]
				DPTKIND_TIMEMINUS = DPTKIND_TIME+0x002,
			
				[EnumDescription("��������")]
				DPTKIND_TIMECLEAR = DPTKIND_TIME+0x004,
			
		}

	
		public uint? dwAccNo;		/*�˺�*/
	
		public string szPID;		/*ѧ����*/
	
		public string szTrueName;		/*��ʵ����*/
	
		public uint? dwAmount;		/*����*/
	
		public uint? dwAdminID;		/*����Ա*/
	
		public string szMemo;		/*��ע*/
		};

	/*֧�������ύ������ˮ�ṹ*/
	public struct UNIPAYMENT
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*��ˮ��*/
	
		public uint? dwAccNo;		/*�ʺ�*/
	
		public uint? dwCostDate;		/*��������*/
	
		public uint? dwCostTime;		/*����ʱ��*/
	
		public uint? dwCardTime;		/*���۷�ʱ��*/
	
		public uint? dwDealTime;		/*�ύ��ˮʱ��*/
	
		public string szPID;		/*ѧ����*/
	
		public string szCardNo;		/*���俨��*/
	
		public string szTrueName;		/*��ʵ����*/
	
		public uint? dwFeeType;		/*�������(FEEDETAIL����)*/
	
		public uint? dwCostMoney;		/*���ѽ��*/
	
		public uint? dwCostSubsidy;		/*���Ѳ���*/
	
		public uint? dwCostFreeTime;		/*���Ѳ���ʱ��*/
	
		public string szPosInfo;		/*��һ��ͨ��Ӧ���̻���Ϣ*/
	
		public string szCardCostInfo;		/*���۷ѷ�����Ϣ����ͬ��һ��ͨ��ʽ�����ݶ���ͬ*/
	
		public uint? dwRetStatus;		/*�ύ����״̬���ο�UniCommon.h����*/
	
		public uint? dwRetDealSID;		/*���ص�������ˮ��*/
	
		public string szRetDealInfo;		/*�ύ��ˮ������Ϣ����ͬ��һ��ͨ��ʽ�����ݶ���ͬ*/
		};

	/*��ȡ������Ϣ*/
	public struct NOTICEREQ
	{
		private Reserved reserved;
		
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
		public uint? dwSID;		/*��ˮ��*/
	
		public uint? dwStatus;		/*״̬*/
	
		public uint? dwSubjectID;		/*֪ͨ����ID*/
	
		public uint? dwSubjectType;		/*֪ͨ�������*/
	
		public uint? dwSender;		/*���ͷ�*/
	
		public uint? dwRecipient;		/*���ܷ�*/
		};

	/*������Ϣ*/
	public struct NOTICEINFO
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*��ˮ��*/
	
		public uint? dwSubSysSN;		/*��ϵͳ���*/
	
		public uint? dwStatus;		/*״̬*/
	
		public uint? dwSubjectID;		/*֪ͨ����ID*/
	
		public uint? dwSubjectType;		/*֪ͨ�������,��˲�����֪ͨ��������(CHECKINFO::dwSubjectType)ͬ*/
	
		public uint? dwSender;		/*���ͷ��ʺ�*/
	
		public uint? dwRecipient;		/*���շ��ʺ�*/
	
		public uint? dwNoticeMode;		/*֪ͨ��ʽ*/
	
		public uint? dwOccurTime;		/*����ʱ��*/
	
		public uint? dwSendTime;		/*����ʱ��*/
	
		public uint? dwAffirmTime;		/*ȷ��ʱ��*/
	
		public string szMemo;		/*��ע*/
	
		public uint? dwNoticeKind;		/*֪ͨ���*/
	
		public uint? dwCheckStat;		/*���״̬*/
	
		public string szRecvName;		/*����������*/
	
		public string szRecvMobile;		/*�������ֻ�*/
	
		public string szRecvMail;		/*����������*/
	
		public string szSenderName;		/*����������*/
	
		public string szSenderMobile;		/*�������ֻ�*/
	
		public string szSenderMail;		/*����������*/
	
		public string szSessionID;		/*SessionID*/
	
		public string szReason;		/*ԭ��*/
	
		public string szFullSendInfo;		/*��������*/
		};

	/*֪ͨ����ȷ��*/
	public struct NOTICEAFFIRM
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*��ˮ��*/
	
		public uint? dwAffirmStat;		/*ȷ��״̬*/
	
		public uint? dwNoticeMode;		/*֪ͨ��ʽ*/
	
		public uint? dwAffirmTime;		/*ȷ��ʱ��*/
	
		public string szMemo;		/*��ע*/
		};

	/**/
	public struct MAJORREQ
	{
		private Reserved reserved;
		
		public uint? dwMajorID;		/*ID*/
	
		public string szMajorSN;		/*���*/
	
		public string szMajorName;		/*����*/
	
		public uint? dwKind;		/*����*/
	
		public uint? dwDeptID;		/*����ID*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/**/
	public struct UNIMAJOR
	{
		private Reserved reserved;
		
		public uint? dwMajorID;		/*ID*/
	
		public string szMajorSN;		/*���*/
	
		public string szMajorName;		/*����*/
	
		public uint? dwKind;		/*����*/
	
		public uint? dwDeptID;		/*����ID*/
	
		public string szDeptName;		/*��������*/
	
		public uint? dwSchoolYears;		/*��ѧ���*/
	
		public string szMemo;		/*��ע*/
		};

	/**/
	public struct TESTDATAREQ
	{
		private Reserved reserved;
		
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
		public uint? dwStatus;		/*����״̬*/
	
		public uint? dwSID;		/*SID*/
	
		public uint? dwAccNo;		/*�˺�*/
	
		public uint? dwDevID;		/*�豸ID*/
	
		public uint? dwRoomID;		/*����ID*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/**/
	public struct UNITESTDATA
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*SID*/
	
		public uint? dwDevID;		/*�豸ID*/
	
		public string szDevName;		/*�豸����*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabName;		/*ʵ��������*/
	
		public uint? dwRoomID;		/*����ID*/
	
		public string szRoomName;		/*��������*/
	
		public uint? dwAccNo;		/*�˺�*/
	
		public string szPID;		/*ѧ����*/
	
		public string szTrueName;		/*��ʵ����*/
	
		public uint? dwSubmitDate;		/*�ύ����*/
	
		public uint? dwSubmitTime;		/*�ύʱ��*/
	
		public uint? dwFileSize;		/*�ļ���С*/
	
		public string szDisplayName;		/*��ʾ����*/
	
		public string szLocation;		/*���λ��*/
	
		public uint? dwStatus;		/*״̬*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("�����ϴ�")]
				TDSTAT_UPLOADING = 1,
			
				[EnumDescription("���ϴ�")]
				TDSTAT_UPLOADED = 2,
			
				[EnumDescription("������")]
				TDSTAT_DOWNLOADED = 4,
			
				[EnumDescription("�ļ���ɾ��")]
				TDSTAT_FILEDEL = 0x8,
			
		}

	
		public string szMemo;		/*˵��*/
		};

	/*����Ա�ϴ�ʵ������*/
	public struct ADMINTESTDATA
	{
		private Reserved reserved;
		
		public string szLogonName;		/*��¼��*/
	
		public string szPassword;		/*����*/
	
	public UNITESTDATA TestData;		/*ʵ������(UNITESTDATA)*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/**/
	public struct CLOUDDISKREQ
	{
		private Reserved reserved;
		
		public uint? dwAccNo;		/*�˺�*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/**/
	public struct CLOUDDISK
	{
		private Reserved reserved;
		
		public uint? dwFileID;		/*�ļ�ID*/
	
		public string szFileName;		/*�ļ�����*/
	
		public uint? dwAccNo;		/*�˺�*/
	
		public string szPID;		/*ѧ����*/
	
		public string szTrueName;		/*��ʵ����*/
	
		public uint? dwSubmitDate;		/*�ύ����*/
	
		public uint? dwFileSize;		/*�ļ���С*/
	
		public string szLocation;		/*���λ��*/
	
		public string szMemo;		/*˵��*/
		};

	/**/
	public struct CDISKSTATREQ
	{
		private Reserved reserved;
		
		public uint? dwAccNo;		/*�˺�*/
		};

	/**/
	public struct CDISKSTAT
	{
		private Reserved reserved;
		
		public uint? dwAccNo;		/*�˺�*/
	
		public string szPID;		/*ѧ����*/
	
		public string szTrueName;		/*��ʵ����*/
	
		public uint? dwTotalSize;		/*�ܴ�С*/
	
		public uint? dwUsedSize;		/*���ÿռ�*/
	
		public uint? dwFileNum;		/*�ļ�����*/
	
		public string szMemo;		/*˵��*/
		};

	/*��ȡ�ον�ʦ��Ϣ*/
	public struct UNITEACHERREQ
	{
		private Reserved reserved;
		
		public uint? dwAccNo;		/*�˺�*/
	
		public string szPID;		/*����*/
	
		public string szTrueName;		/*��ʦ����(ģ��ƥ��)*/
	
		public uint? dwDeptID;		/*����ID*/
	
		public uint? dwCourseID;		/*�γ�ID*/
	
		public string szCourseName;		/*�γ�����(ģ��ƥ��)*/
	
		public uint? dwYearTerm;		/*ѧ�ڱ��*/
	
		public uint? dwReqProp;		/*���󸽼�����*/
	
		[FlagsAttribute]
		public enum DWREQPROP : uint
		{
			
				[EnumDescription("��Ҫ���ڿγ�")]
				DEVREQ_NEEDCOURSE = 0x1,
			
		}

	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*���ڿγ�*/
	public struct TEACHCOURSE
	{
		private Reserved reserved;
		
		public uint? dwCourseID;		/*�γ�ID*/
	
		public string szCourseCode;		/*�γ̴���*/
	
		public string szCourseName;		/*�γ�����*/
	
		public string szMemo;		/*��ע*/
		};

	/*�ον�ʦ��Ϣ*/
	public struct UNITEACHER
	{
		private Reserved reserved;
		
		public uint? dwAccNo;		/*�˺�*/
	
		public string szPID;		/*ѧ����*/
	
		public string szTrueName;		/*����*/
	
		public uint? dwSex;		/*�Ա�*/
	
		public uint? dwDeptID;		/*ѧԺ�����ţ�ID*/
	
		public string szDeptName;		/*����ѧԺ*/
	
		public string szTel;		/*�绰*/
	
		public string szHandPhone;		/*�ֻ�*/
	
		public string szEmail;		/*����*/
	
	public TEACHCOURSE[] TeachCourse;		/*�е��γ�(CUniTable<TEACHCOURSE>)*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*�����û�ʹ����Ϣ*/
	public struct USERCURINFOREQ
	{
		private Reserved reserved;
		
		public uint? dwAccNo;		/*�˺�*/
		};

	/*�û�ʹ����Ϣ(ͬCONUSERINFO)*/
	public struct USERCURINFO
	{
		private Reserved reserved;
		
		public uint? dwUserStat;		/*�û�״̬*/
	
	public UNIACCOUNT AccInfo;		/*UNIACCOUNT �ṹ*/
	
	public UNIRESERVE ResvInfo;		/*UNIRESERVE �ṹ*/
	
	public UNIDEVICE DevInfo;		/*UNIDEVICE �ṹ*/
	
	public UNIBILL[] BillInfo;		/*�˵���(CUniTable<UNIBILL>)*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/

	/**/
	public struct UNILAB
	{
		private Reserved reserved;
		
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabSN;		/*ʵ���ұ��*/
	
		public string szLabName;		/*ʵ��������*/
	
		public uint? dwDeptID;		/*����ID*/
	
		public string szDeptName;		/*��������*/
	
		public string szLabKindCode;		/*ʵ�������ͱ���*/
	
		public string szLabLevelCode;		/*ʵ���ҽ���ˮƽ����*/
	
		public string szLabFromCode;		/*ʵ������Դ����*/
	
		public string szAcademicSubjectCode;		/*����ѧ�Ʊ���*/
	
		public uint? dwLabClass;		/*ʵ�������*/
	
		public uint? dwManGroupID;		/*����Ա��ID*/
	
		public uint? dwCreateDate;		/*��������*/
	
		public string szLabURL;		/*ʵ���Ҽ��URL*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/**/
	public struct LABREQ
	{
		private Reserved reserved;
		
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabSN;		/*ʵ���ұ��*/
	
		public string szLabName;		/*ʵ��������*/
	
		public uint? dwDeptID;		/*����ID*/
	
		public uint? dwLabClass;		/*ʵ���������*/
	
		public uint? dwPurpose;		/*��;(��UNIRESERVE����)*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/**/
	public struct FULLLAB
	{
		private Reserved reserved;
		
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabSN;		/*ʵ���ұ��*/
	
		public string szLabName;		/*ʵ��������*/
	
		public uint? dwDeptID;		/*����ID*/
	
		public string szDeptName;		/*��������*/
	
		public string szLabKindCode;		/*ʵ�������ͱ���*/
	
		public string szLabLevelCode;		/*ʵ���ҽ���ˮƽ����*/
	
		public string szLabFromCode;		/*ʵ������Դ����*/
	
		public string szAcademicSubjectCode;		/*����ѧ�Ʊ���*/
	
		public uint? dwLabClass;		/*ʵ�������*/
	
		public string szLabURL;		/*ʵ���Ҽ��URL*/
	
		public string szMemo;		/*˵����Ϣ*/
	
		public uint? dwAttendantID;		/*ֵ��Ա�˺�*/
	
		public string szAttendantName;		/*ֵ��Ա����*/
	
		public uint? dwTotalDevNum;		/*�豸����*/
	
		public uint? dwUsableDevNum;		/*�����豸��*/
	
		public uint? dwIdleDevNum;		/*�����豸��*/
		};

	/**/
	public struct FULLLABREQ
	{
		private Reserved reserved;
		
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabSN;		/*ʵ���ұ��*/
	
		public string szLabName;		/*ʵ��������*/
	
		public uint? dwDeptID;		/*����ID*/
	
		public uint? dwLabClass;		/*ʵ���������*/
	
		public uint? dwPurpose;		/*��;(��UNIRESERVE����)*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/**/
	public struct DEVCLSREQ
	{
		private Reserved reserved;
		
		public uint? dwClassID;		/*�豸(ʵ����)���ID*/
	
		public string szClassName;		/*�豸(ʵ����)�������*/
	
		public uint? dwKind;		/*���*/
	
		public uint? dwPurpose;		/*��;(��UNIRESERVE����)*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*�豸���*/
	public struct UNIDEVCLS
	{
		private Reserved reserved;
		
		public uint? dwClassID;		/*�豸(ʵ����)���ID*/
	
		public string szClassSN;		/*�豸�����*/
	
		public string szClassName;		/*�豸(ʵ����)�������*/
	
		public string szMemo;		/*˵����Ϣ*/
	
		public uint? dwKind;		/*�������*/
	
		[FlagsAttribute]
		public enum DWKIND : uint
		{
			
				[EnumDescription("�ռ�")]
				CLSKIND_COMMONS = 0x1,
			
				[EnumDescription("���ԣ�����������)")]
				CLSKIND_COMPUTER = 0x2,
			
				[EnumDescription("����豸")]
				CLSKIND_LOAN = 0x4,
			
				[EnumDescription("��λ")]
				CLSKIND_SEAT = 0x8,
			
				[EnumDescription("ʵ������")]
				CLSKIND_INSTRUMENT = 0x10,
			
				[EnumDescription("��������")]
				CLSKIND_MASK = 0xFF,
			
				[EnumDescription("�������޼�")]
				CLSCOMMONS_SINGLE = (CLSKIND_COMMONS + 0x100),
			
				[EnumDescription("�����(����������)")]
				CLSCOMMONS_GROUP = (CLSKIND_COMMONS + 0x200),
			
				[EnumDescription("���Ż��")]
				CLSCOMMONS_ACTIVITY = (CLSKIND_COMMONS + 0x400),
			
				[EnumDescription("�������޼�")]
				CLSCOMMONS_MULTIPLE = (CLSKIND_COMMONS + 0x800),
			
				[EnumDescription("������ѯ��")]
				CLSCOMMONS_CONSULTING = (CLSKIND_COMMONS + 0x1000),
			
				[EnumDescription("����")]
				CLSCOMMONS_CLASSROOM = (CLSKIND_COMMONS + 0x2000),
			
				[EnumDescription("������")]
				CLSCOMMONS_MEETINGROOM = (CLSKIND_COMMONS + 0x4000),
			
				[EnumDescription("��ͨ����")]
				CLSCOMPUTER_PC = (CLSKIND_COMPUTER + 0x100),
			
				[EnumDescription("ƻ������")]
				CLSCOMPUTER_MAC = (CLSKIND_COMPUTER + 0x200),
			
		}

	
		public uint? dwResv1;		/*�����ֶ�1*/
	
		public uint? dwResv2;		/*�����ֶ�2*/
	
		public string szDevClsURL;		/*���URL*/
	
		public string szExtInfo;		/*��չ��Ϣ*/
		};

	/**/
	public struct DEVKINDREQ
	{
		private Reserved reserved;
		
		public uint? dwKindID;		/*�豸�������ID*/
	
		public string szKindName;		/*�豸����*/
	
		public string szClassName;		/*�����(*/
	
		public uint? dwProperty;		/*�豸����*/
	
		public uint? dwClassKind;		/*���(��UNIDEVCLS��Kind����)*/
	
		public string szKindIDs;		/*��������,����ö��Ÿ���*/
	
		public uint? dwExtRelatedID;		/*��չ����ID*/
	
		public uint? dwPurpose;		/*��;(��UNIRESERVE����)*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*�豸�������*/
	public struct UNIDEVKIND
	{
		private Reserved reserved;
		
		public uint? dwKindID;		/*�豸�������ID*/
	
		public string szKindName;		/*�豸����*/
	
		public string szFuncCode;		/*�豸������;����*/
	
		public string szModel;		/*�豸�ͺ�*/
	
		public string szSpecification;		/*�豸���*/
	
		public string szProducer;		/*������*/
	
		public uint? dwNationCode;		/*������*/
	
		public uint? dwProperty;		/*�豸����*/
	
		[FlagsAttribute]
		public enum DWPROPERTY : uint
		{
			
				[EnumDescription("������ռ��ͬһʱ���֧��һ��С������ԤԼ��")]
				DEVPROP_EXCLUSIVE = 0x1,
			
				[EnumDescription("������ͬʱ֧�ֶ��С������ԤԼ��")]
				DEVPROP_SHARE = 0x2,
			
				[EnumDescription("����ԤԼ(���죬������������)")]
				DEVPROP_LONGTERMRESV = 0x4,
			
				[EnumDescription("������ԤԼ")]
				DEVPROP_KINDRESV = 0x8,
			
				[EnumDescription("���")]
				DEVPROP_LEASE = 0x100,
			
				[EnumDescription("ʹ���������Ա���ȷ��")]
				DEVPROP_USEDCHECK = 0x200,
			
				[EnumDescription("DEVKIND�������ȡǰ16��")]
				DEVKINDPROP_MASK = 0xFFFF,
			
		}

	
		public uint? dwClassID;		/*�����������ID*/
	
		public string szClassSN;		/*�豸�����*/
	
		public string szClassName;		/*�����(*/
	
		public uint? dwClassKind;		/*���(��UNIDEVCLS��Kind����)*/
	
		public uint? dwMaxUsers;		/*��ԤԼ���ͬʱʹ������*/
	
		public uint? dwMinUsers;		/*ÿ��ԤԼ����ͬʱʹ������*/
	
		public uint? dwTotalNum;		/*�豸����*/
	
		public uint? dwUsableNum;		/*�����豸����ԤԼ�ж��ã�*/
	
		public string szOperaCert;		/*����֤��*/
	
		public string szDevKindURL;		/*�豸��ϸ���ܵ�URL��ַ*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*��ȡ¥����Ϣ����*/
	public struct BUILDINGREQ
	{
		private Reserved reserved;
		
		public uint? dwBuildingID;		/*¥��ID*/
	
		public string szBuildingName;		/*¥������*/
	
		public string szBuildingNo;		/*¥���*/
	
		public string szCampusIDs;		/*У��ID,����ö��Ÿ���*/
	
		public uint? dwActivitySN;		/*����ͱ��*/
	
		public uint? dwPurpose;		/*��;(��UNIRESERVE����)*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*¥������*/
	public struct UNIBUILDING
	{
		private Reserved reserved;
		
		public uint? dwBuildingID;		/*¥��ID*/
	
		public string szBuildingName;		/*¥������*/
	
		public string szBuildingNo;		/*¥���*/
	
		public string szMapIndex;		/*��ͼ����*/
	
		public string szBuildingURL;		/*��ϸ���ܵ�URL��ַ*/
	
		public uint? dwCampusID;		/*У��ID*/
	
		public string szCampusName;		/*У������*/
	
		public uint? dwCampusKind;		/*У�����ͣ���չ��*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*��ȡ������Ϣ����*/
	public struct ROOMREQ
	{
		private Reserved reserved;
		
		public uint? dwRoomID;		/*����ID*/
	
		public string szRoomName;		/*��������*/
	
		public string szRoomNo;		/*�����*/
	
		public uint? dwLabClass;		/*ʵ���������*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szBuildingIDs;		/*¥��ID,����ö��Ÿ���*/
	
		public string szCampusIDs;		/*У��ID,����ö��Ÿ���*/
	
		public uint? dwInClassKind;		/*����豸���(��UNIDEVCLS��Kind����)*/
	
		public uint? dwPurpose;		/*��;(��UNIRESERVE����)*/
	
		public uint? dwProperty;		/*��չ����*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*��������*/
	public struct UNIROOM
	{
		private Reserved reserved;
		
		public uint? dwRoomID;		/*����ID*/
	
		public string szRoomName;		/*��������*/
	
		public string szRoomNo;		/*�����*/
	
		public uint? dwInClassKind;		/*����豸���(��UNIDEVCLS��Kind����)*/
	
		public string szFloorNo;		/*����¥��*/
	
		public uint? dwBuildingID;		/*¥��ID*/
	
		public string szBuildingNo;		/*��¥���(*/
	
		public string szBuildingName;		/*��¥����(*/
	
		public uint? dwRoomSize;		/*�������(ƽ��)*/
	
		public string szMapIndex;		/*��ͼ����*/
	
		public string szRoomURL;		/*��ϸ���ܵ�URL��ַ*/
	
		public string szSubRooms;		/*��������(�����ţ��ɶ�������Ÿ���)*/
	
		public string szLabKindCode;		/*ʵ�������ͱ���*/
	
		public string szLabLevelCode;		/*ʵ���ҽ���ˮƽ����*/
	
		public string szLabFromCode;		/*ʵ������Դ����*/
	
		public string szAcademicSubjectCode;		/*����ѧ�Ʊ���*/
	
		public uint? dwLabClass;		/*ʵ�������*/
	
		public uint? dwCreateDate;		/*��������*/
	
		public uint? dwOpenRuleSN;		/*���Ź�����*/
	
		public string szOpenRuleName;		/*���Ź�������*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabSN;		/*ʵ���ұ��*/
	
		public string szLabName;		/*ʵ��������*/
	
		public string szLabURL;		/*��ϸ���ܵ�URL��ַ*/
	
		public uint? dwDeptID;		/*����ID*/
	
		public string szDeptName;		/*��������*/
	
		public uint? dwManGroupID;		/*�������Ա��ID*/
	
		public string szManGroupName;		/*����Ա������*/
	
		public uint? dwManMode;		/*���Ʒ�ʽ*/
	
		[FlagsAttribute]
		public enum DWMANMODE : uint
		{
			
				[EnumDescription("�Ž�����")]
				ROOMMAN_DOORLOCK = 1,
			
				[EnumDescription("������")]
				ROOMMAN_CAMERA = 2,
			
				[EnumDescription("����ϵͳ")]
				ROOMMAN_SOUND = 4,
			
				[EnumDescription("��δ�ر���")]
				ROOMMAN_DOORWARNING = 0x100,
			
				[EnumDescription("�����������ڿ���ʱ�����")]
				ROOMMAN_FREEINOUT = 0x200,
			
		}

	
		public uint? dwCtrlSN;		/*���������*/
	
		public uint? dwCampusID;		/*У��ID*/
	
		public string szCampusName;		/*У������*/
	
		public uint? dwCampusKind;		/*У�����ͣ���չ��*/
	
		public string szMemo;		/*˵����Ϣ*/
	
		public string szMAIP;		/*�ֻ�ǩ��IP��*/
	
		public uint? dwProperty;		/*��չ����*/
	
		[FlagsAttribute]
		public enum DWPROPERTY : uint
		{
			
				[EnumDescription("�ֻ�ǩ����ͨ��������")]
				ROOMPROPHP_AUTOGATE = 0x1,
			
				[EnumDescription("�ֻ�ǩ���жϽ���IP")]
				ROOMPROPHP_IP = 0x2,
			
				[EnumDescription("�ֻ�ǩ��GPS��λ")]
				ROOMPROPHP_GPS = 0x4,
			
				[EnumDescription("��֧���ֻ�ɨ��ԤԼ")]
				ROOMPROPHP_NORESV = 0x10,
			
				[EnumDescription("��֧���ֻ�ɨ��ǩ��")]
				ROOMPROPHP_NOSIGN = 0x20,
			
				[EnumDescription("��֧���ֻ�ɨ����ʱ�뿪")]
				ROOMPROPHP_NOLEAVE = 0x40,
			
				[EnumDescription("��֧���ֻ�ɨ�����ʹ��")]
				ROOMPROPHP_NOEXIT = 0x80,
			
				[EnumDescription("��֧������ԤԼ")]
				ROOMPROP_NORESV = 0x100000,
			
				[EnumDescription("��Ϸ���(RESVPROP_COMBINEROOM)")]
				ROOMPROP_COMBINE = 0x2000000,
			
				[EnumDescription("�ӷ��䣨�����RESVPROP_SUBROOM��")]
				ROOMPROP_SUBROOM = 0x4000000,
			
		}

		};

	/*��ȡ������Ϣ����*/
	public struct FULLROOMREQ
	{
		private Reserved reserved;
		
		public uint? dwRoomID;		/*����ID*/
	
		public string szRoomName;		/*��������*/
	
		public string szRoomNo;		/*�����*/
	
		public uint? dwLabClass;		/*ʵ���������*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szBuildingIDs;		/*¥��ID,����ö��Ÿ���*/
	
		public string szCampusIDs;		/*У��ID,����ö��Ÿ���*/
	
		public uint? dwInClassKind;		/*����豸���(��UNIDEVCLS��Kind����)*/
	
		public uint? dwPurpose;		/*��;(��UNIRESERVE����)*/
	
		public uint? dwProperty;		/*��չ����*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*����������Ϣ*/
	public struct FULLROOM
	{
		private Reserved reserved;
		
		public uint? dwRoomID;		/*����ID*/
	
		public string szRoomName;		/*��������*/
	
		public string szRoomNo;		/*�����*/
	
		public uint? dwInClassKind;		/*����豸���(��UNIDEVCLS��Kind����)*/
	
		public string szFloorNo;		/*����¥��*/
	
		public uint? dwBuildingID;		/*¥��ID*/
	
		public string szBuildingNo;		/*��¥���(*/
	
		public string szBuildingName;		/*��¥����(*/
	
		public uint? dwRoomSize;		/*�������(ƽ��)*/
	
		public string szMapIndex;		/*��ͼ����*/
	
		public string szRoomURL;		/*��ϸ���ܵ�URL��ַ*/
	
		public uint? dwOpenRuleSN;		/*���Ź�����*/
	
		public string szOpenRuleName;		/*���Ź�������*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabSN;		/*ʵ���ұ��*/
	
		public string szLabName;		/*ʵ��������*/
	
		public string szLabKindCode;		/*ʵ�������ͱ���*/
	
		public string szLabLevelCode;		/*ʵ���ҽ���ˮƽ����*/
	
		public string szLabFromCode;		/*ʵ������Դ����*/
	
		public string szAcademicSubjectCode;		/*����ѧ�Ʊ���*/
	
		public uint? dwLabClass;		/*ʵ�������*/
	
		public uint? dwCreateDate;		/*��������*/
	
		public string szLabURL;		/*��ϸ���ܵ�URL��ַ*/
	
		public uint? dwDeptID;		/*����ID*/
	
		public string szDeptName;		/*��������*/
	
		public uint? dwManGroupID;		/*����Ա��ID*/
	
		public string szManGroupName;		/*����Ա������*/
	
		public uint? dwAttendantID;		/*ֵ��Ա�˺�*/
	
		public string szAttendantName;		/*ֵ��Ա����*/
	
		public uint? dwStatus;		/*�����Ž�״̬�� UNIDCS����*/
	
		public uint? dwStatChgTime;		/*״̬�ı俪ʼʱ��(time������)*/
	
		public string szStatInfo;		/*״̬����*/
	
		public uint? dwManMode;		/*���Ʒ�ʽ*/
	
		public uint? dwCtrlSN;		/*���������*/
	
		public uint? dwCampusID;		/*У��ID*/
	
		public string szCampusName;		/*У������*/
	
		public uint? dwCampusKind;		/*У�����ͣ���չ��*/
	
		public uint? dwTotalDevNum;		/*�豸����*/
	
		public uint? dwUsableDevNum;		/*�����豸��*/
	
		public uint? dwIdleDevNum;		/*�����豸��*/
	
		public string szMemo;		/*˵����Ϣ*/
	
		public uint? dwProperty;		/*��չ����*/
		};

	/*��ȡ���������Ϣ����*/
	public struct BASICROOMREQ
	{
		private Reserved reserved;
		
		public uint? dwRoomID;		/*����ID*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public uint? dwInClassKind;		/*����豸���(��UNIDEVCLS��Kind����)*/
	
		public uint? dwDoorStat;		/*�����Ž�״̬�� UNIDCS����*/
	
		public uint? dwUseStat;		/*����ʹ��״̬*/
	
		[FlagsAttribute]
		public enum DWUSESTAT : uint
		{
			
				[EnumDescription("����")]
				ROOMUSESTAT_IDLE = 0x1,
			
				[EnumDescription("�����Ͽ�")]
				ROOMUSESTAT_INUSE = 0x2,
			
		}

	
		public uint? dwUsePurpose;		/*��UNIDEVICE����*/
	
		public uint? dwProperty;		/*��չ����*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*���������Ϣ*/
	public struct BASICROOM
	{
		private Reserved reserved;
		
		public uint? dwRoomID;		/*����ID*/
	
		public string szRoomName;		/*��������*/
	
		public string szRoomNo;		/*�����*/
		};

	/*��ȡͨ������Ϣ����*/
	public struct CHANNELGATEREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*��ʽ*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("ȫ��")]
				CHANNELGATEGET_BYALL = 0,
			
				[EnumDescription("ID")]
				CHANNELGATEGET_BYID = 1,
			
				[EnumDescription("����")]
				CHANNELGATEGET_BYNAME = 2,
			
				[EnumDescription("ͨ�����")]
				CHANNELGATEGET_BYSN = 3,
			
				[EnumDescription("����¥��")]
				CHANNELGATEGET_FLOOR = 4,
			
		}

	
		public string szGetKey;		/*����ֵ*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*ͨ����*/
	public struct UNICHANNELGATE
	{
		private Reserved reserved;
		
		public uint? dwChannelGateID;		/*ͨ����ID*/
	
		public string szChannelGateName;		/*ͨ��������*/
	
		public string szChannelGateNo;		/*ͨ���ű��*/
	
		public string szRelatedRooms;		/*��������(�����ţ��ɶ�������Ÿ���)*/
	
		public string szFloorNo;		/*����¥��*/
	
		public uint? dwBuildingID;		/*¥��ID*/
	
		public string szBuildingNo;		/*��¥���(*/
	
		public string szBuildingName;		/*��¥����(*/
	
		public uint? dwManGroupID;		/*����Ա��ID*/
	
		public string szManGroupName;		/*����Ա������*/
	
		public uint? dwUseGroupID;		/*�����û���ID*/
	
		public string szUseGroupName;		/*�����û�������*/
	
		public uint? dwOpenRuleSN;		/*���Ź�����*/
	
		public uint? dwStatus;		/*�Ž�״̬�� UNIDCS����*/
	
		public uint? dwStatChgTime;		/*״̬�ı俪ʼʱ��(time������)*/
	
		public string szStatInfo;		/*״̬����*/
	
		public uint? dwManMode;		/*������Ʒ�ʽ����UNIROOM*/
	
		public uint? dwCtrlSN;		/*���������*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*���Ʒ�������*/
	public struct CHANNELGATECTRLINFO
	{
		private Reserved reserved;
		
		public uint? dwChannelGateID;		/*ͨ����ID*/
	
		public string szChannelGateNo;		/*ͨ���ű��*/
	
		public uint? dwCmd;		/*��������,�ο�DEVCTRLINFO����*/
	
		public string szParam;		/*���Ʋ���*/
	
		public string szMemo;		/*��ע*/
		};

	/*��ȡ�����������*/
	public struct ROOMGROUPREQ
	{
		private Reserved reserved;
		
		public uint? dwRoomNum;		/*��Ϸ�����*/
	
		public uint? dwRoomID;		/*����ID*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*������ϳ�Ա*/
	public struct RGMEMBER
	{
		private Reserved reserved;
		
		public uint? dwRoomID;		/*����ID*/
	
		public string szRoomNo;		/*�����*/
	
		public string szRoomName;		/*��������*/
	
		public uint? dwDevNum;		/*�豸��*/
		};

	/*�������*/
	public struct ROOMGROUP
	{
		private Reserved reserved;
		
		public uint? dwRGID;		/*�������ID*/
	
		public string szRGName;		/*�����������*/
	
		public uint? dwRoomNum;		/*��Ϸ�����*/
	
	public RGMEMBER[] rgMember;		/*CUniTable[RGMEMBER]*/
		};

	/*��ȡ�豸�б�*/
	public struct DEVREQ
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*�豸ID*/
	
		public string szSearchKey;		/*�����ؼ���*/
	
		public uint? dwDevSN;		/*�豸���*/
	
		public string szAssertSN;		/*�û����ʲ����*/
	
		public string szTagID;		/*RFID��ǩID*/
	
		public string szLabIDs;		/*ʵ����ID,����ö��Ÿ���*/
	
		public string szRoomIDs;		/*����ID,����ö��Ÿ���*/
	
		public uint? dwResvID;		/*ԤԼID*/
	
		public string szKindIDs;		/*��������,����ö��Ÿ���*/
	
		public string szClassIDs;		/*�����������ID,����ö��Ÿ���*/
	
		public string szDeptIDs;		/*ѧԺID,����ö��Ÿ���*/
	
		public string szBuildingIDs;		/*¥��ID,����ö��Ÿ���*/
	
		public string szCampusIDs;		/*У��ID,����ö��Ÿ���*/
	
		public uint? dwDevStat;		/*�豸״̬*/
	
		public uint? dwRunStat;		/*�豸����״̬*/
	
		public uint? dwUnNeedRunStat;		/*����������״̬*/
	
		public uint? dwCtrlMode;		/*���Ʒ�ʽ*/
	
		public uint? dwProperty;		/*�豸����*/
	
		public uint? dwClassKind;		/*���(��UNIDEVCLS��Kind����)*/
	
		public uint? dwManGroupID;		/*����Ա��ID*/
	
		public uint? dwAttendantID;		/*ֵ��ԱID*/
	
		public string szAttendantName;		/*ֵ��Ա����(ģ��)*/
	
		public uint? dwMinUnitPrice;		/*��ͼ۸�*/
	
		public uint? dwMaxUnitPrice;		/*���۸�*/
	
		public uint? dwSPurchaseDate;		/*��ʼ�ɹ�����*/
	
		public uint? dwEPurchaseDate;		/*��ֹ�ɹ�����*/
	
		public uint? dwReqProp;		/*���󸽼�����*/
	
		[FlagsAttribute]
		public enum DWREQPROP : uint
		{
			
				[EnumDescription("��ҪԤԼ��Ϣ")]
				DEVREQ_NEEDDEVRESV = 0x1,
			
				[EnumDescription("��Ҫ��ǰʹ����Ϣ")]
				DEVREQ_NEEDDEVUSE = 0x2,
			
				[EnumDescription("��Ҫ������Ʒ��Ϣ")]
				DEVREQ_NEEDSAMPLE = 0x4,
			
				[EnumDescription("��Ҫ������Ʒ��Ϣ")]
				DEVREQ_NEEDFULLSAMPLE = 0x8,
			
		}

	
		public uint? dwPurpose;		/*��;(��UNIRESERVE����)*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*�Ʒ���Ϣ��ϸ*/
	public struct FDINFO
	{
		private Reserved reserved;
		
		public uint? dwBeginTime;		/*��ʼʱ��*/
	
		public uint? dwEndTime;		/*����ʱ��*/
	
		public uint? dwFeeType;		/*�շ����(FEEDETAIL����)*/
	
		public uint? dwUsablePayKind;		/*���ýɷѷ�ʽ(��UNIBILL����)*/
	
		public uint? dwDefaultCheckStat;		/*CHECKINFO����Ĺ���Ա���״̬*/
	
		public uint? dwUnitFee;		/*����*/
	
		public uint? dwUnitTime;		/*��λʱ��*/
	
		public uint? dwRoundOff;		/*����ֽ��(С�ڵ�λʱ��)*/
	
		public uint? dwIgnoreTime;		/*���Ʒ�ʱ��*/
	
		public uint? dwHolidayCoef;		/*����ϵ��*/
	
		public string szPosInfo;		/*��һ��ͨ��Ӧ���̻���Ϣ*/
	
		public uint? dwUseTime;		/*ʹ��ʱ��*/
	
		public uint? dwFeeTime;		/*�Ʒ�ʱ��*/
	
		public uint? dwCostMoney;		/*����*/
	
		public uint? dwCostSubsidy;		/*ʹ�ò���*/
	
		public uint? dwCostFreeTime;		/*ʹ�����ʱ��(��ʱ)*/
		};

	/*�Ʒ���Ϣ*/
	public struct UNIACCTINFO
	{
		private Reserved reserved;
		
		public uint? dwBeginTime;		/*��ʼʱ��*/
	
		public uint? dwEndTime;		/*����ʱ��*/
	
		public uint? dwUseTime;		/*ʹ��ʱ��*/
	
		public uint? dwIdent;		/*��ݣ�0��ʾ�����ƣ�*/
	
		public uint? dwDeptID;		/*���ţ�0��ʾ�����ƣ�*/
	
		public uint? dwDevKind;		/*�豸���ͣ�0��ʾ�����ƣ�*/
	
		public uint? dwGroupID;		/*ָ���û��飨0��ʾ�����ƣ�*/
	
		public uint? dwPurpose;		/*��;*/
	
		public uint? dwResvID;		/*ԤԼ��*/
	
		public uint? dwAccNo;		/*�û��˺�*/
	
		public string szPID;		/*ѧ����*/
	
		public string szLogonName;		/*��¼��*/
	
		public string szCardNo;		/*����*/
	
		public string szTrueName;		/*����*/
	
		public string szClassName;		/*�༶*/
	
		public string szDeptName;		/*����*/
	
		public uint? dwSex;		/*�Ա�*/
	
		public uint? dwDevID;		/*�豸ID*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public uint? dwDevSN;		/*�豸���*/
	
		public string szLabSN;		/*ʵ���ұ��*/
	
		public uint? dwSID;		/*������ˮ��*/
	
		public uint? dwBalance;		/*���*/
	
		public uint? dwSubsidy;		/*����*/
	
		public uint? dwFreeTime;		/*���ʱ��*/
	
		public uint? dwUseQuota;		/*�����޶�*/
	
		public uint? dwFeeSN;		/*���ʱ��*/
	
		public uint? dwDeadLine;		/*���û������ʱ��*/
	
	public FDINFO[] szFDInfo;		/*CUniTable[FDINFO]*/
	
		public string szMemo;		/*��ע����չ�ã�*/
		};

	/*��ȡ�豸���������*/
	public struct DEVMONITORREQ
	{
		private Reserved reserved;
		
		public uint? dwMonitorID;		/*�����ID*/
	
		public uint? dwMonitorType;		/*��������*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*�豸�����*/
	public struct DEVMONITOR
	{
		private Reserved reserved;
		
		public uint? dwMonitorID;		/*�����ID*/
	
		public uint? dwMonitorType;		/*��������*/
	
		[FlagsAttribute]
		public enum DWMONITORTYPE : uint
		{
			
				[EnumDescription("RFID Ħ��FX7400")]
				MONITOR_FX7400 = 1,
			
				[EnumDescription("RFID Ӫ��YXU2881")]
				MONITOR_YXU2881 = 2,
			
		}

	
		public string szMonitorName;		/*���������*/
	
		public string szIP;		/*IP��ַ*/
	
		public uint? dwPort;		/*�˿�*/
	
		public string szMemo;		/*��ע*/
		};

	/*��ȡ��������豸�Ķ�Ӧ��ϵ����*/
	public struct MONDEVREQ
	{
		private Reserved reserved;
		
		public uint? dwMonitorID;		/*�����ID*/
	
		public uint? dwMonitorType;		/*��������*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*��������豸�Ķ�Ӧ��ϵ*/
	public struct MONDEV
	{
		private Reserved reserved;
		
		public uint? dwMonitorID;		/*�����ID*/
	
		public uint? dwMonitorType;		/*��������*/
	
		public string szMonitorName;		/*���������*/
	
		public uint? dwLabID;		/*�����ID*/
	
		public uint? dwDevID;		/*�豸ID*/
	
		public uint? dwDevSN;		/*�豸���*/
	
		public string szTagID;		/*RFID��ǩID*/
	
		public string szDevName;		/*ʵ���豸����*/
	
		public string szMemo;		/*��ע*/
		};

	/*�豸���״̬*/
	public struct DEVMONITORSTAT
	{
		private Reserved reserved;
		
		public uint? dwLabID;		/*�����ID*/
	
		public uint? dwDevID;		/*�豸ID*/
	
		public uint? dwMonitorStat;		/*���״̬(dwRunStat ���������˶��壩*/
		};

	/*�豸ԤԼ��ϸ*/
	public struct DEVRESV
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*ԤԼID*/
	
		public uint? dwUsePurpose;		/*ͬUNIRESERVE��dwPurpose*/
	
		public uint? dwResvBeginTime;		/*ԤԼ��ʼʱ��*/
	
		public uint? dwResvEndTime;		/*ԤԼ����ʱ��*/
	
		public string szResvMemberName;		/*ԤԼ��Ա��*/
	
		public uint? dwTeacherID;		/*�ον�ʦID*/
	
		public string szTeacherName;		/*�ον�ʦ����*/
		};

	/*�豸ʹ����Ϣ*/
	public struct DEVUSEINFO
	{
		private Reserved reserved;
		
		public uint? dwAccNo;		/*�û��˺�*/
	
		public string szTrueName;		/*����*/
	
		public uint? dwResvID;		/*ԤԼ��*/
	
		public uint? dwBeginTime;		/*��ʼʱ��*/
	
	public UNIACCTINFO FeeInfo;		/*CUniStruct[UNIACCTINFO]*/
	
		public uint? dwUserUseStat;		/*�û�ʹ��״̬*/
	
		public uint? dwLeaveTime;		/*��ʱ�뿪ʱ��*/
	
		public uint? dwLeaveHoldSec;		/*��ʱ�뿪����ʱ��(�룩*/
	
		public uint? dwQuotaRule;		/*���ƹ���(���ۼƣ����ۼƣ�����æ��(ȱʡ0))*/
	
		public uint? dwQuotaTime;		/*����ʹ��ʱ��(ȱʡ-1)*/
	
		public uint? dwLoanAdminID;		/*������ԱID*/
	
		public string szLoanAdminName;		/*������Ա����*/
	
		public uint? dwReturnAdminID;		/*�黹����ԱID*/
	
		public string szReturnAdminName;		/*�黹����Ա����*/
	
		public uint? dwTutorID;		/*��ʦ�˺�*/
	
		public string szTutorName;		/*��ʦ����*/
	
		public string szMemo;		/*��ע����չ�ã�*/
		};

	/*�豸��Ϣ*/
	public struct UNIDEVICE
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*�豸ID*/
	
		public uint? dwDevSN;		/*�豸���*/
	
		public string szAssertSN;		/*�û����ʲ����*/
	
		public string szTagID;		/*RFID��ǩID*/
	
		public string szOriginSN;		/*ԭ��ϵ�к�*/
	
		public string szDevName;		/*ʵ���豸����*/
	
		public uint? dwUnitPrice;		/*�豸����*/
	
		public uint? dwPurchaseDate;		/*�ɹ����� YYYYMMDD*/
	
		public uint? dwDevStat;		/*�豸״̬*/
	
		[FlagsAttribute]
		public enum DWDEVSTAT : uint
		{
			
				[EnumDescription("�豸����")]
				DEVSTAT_DISABLED = 0x1,
			
				[EnumDescription("�豸����")]
				DEVSTAT_DAMAGED = 0x2,
			
				[EnumDescription("�豸ά����")]
				DEVSTAT_MAINTAIN = 0x4,
			
				[EnumDescription("�豸������")]
				DEVSTAT_UNAVAILABLE = 0xFF,
			
				[EnumDescription("�����𻵣������ã�")]
				DEVSTAT_PARTDAMAGED = 0x100,
			
				[EnumDescription("���ϴ����")]
				DEVSTAT_SWNEEDUPLOAD = 0x1000,
			
		}

	
		public uint? dwCtrlMode;		/*���Ʒ�ʽ*/
	
		[FlagsAttribute]
		public enum DWCTRLMODE : uint
		{
			
				[EnumDescription("�Ž�����")]
				DEVCTRL_DOORCTRL = 0x1,
			
				[EnumDescription("��¼��֤")]
				DEVCTRL_LOGIN = 0x2,
			
				[EnumDescription("ˢ����֤")]
				DEVCTRL_CARD = 0x4,
			
				[EnumDescription("�˹�����")]
				DEVCTRL_BYHAND = 0x8,
			
				[EnumDescription("ԤԼ��Ч�Զ���ʼʹ��")]
				DEVCTRL_AUTOUSE = 0x20,
			
				[EnumDescription("��Դ����")]
				DEVCTRL_POWERCTRL = 0x10,
			
				[EnumDescription("ר�ÿ�(ʹ������ԤԼ�󻻿�)")]
				DEVCTRL_PRIVATECARD = 0x100,
			
				[EnumDescription("���������ж�")]
				DEVCTRL_PERSONDETECT = 0x200,
			
		}

	
		public uint? dwRunStat;		/*�豸״̬*/
	
		[FlagsAttribute]
		public enum DWRUNSTAT : uint
		{
			
				[EnumDescription("�豸������")]
				DEVSTAT_RUNNING = 0x1,
			
				[EnumDescription("�豸ʹ����")]
				DEVSTAT_INUSE = 0x2,
			
				[EnumDescription("�豸ԤԼ��")]
				DEVSTAT_RESERVE = 0x4,
			
				[EnumDescription("�豸��ʱ����")]
				DEVSTAT_TIMEOUT = 0x8,
			
				[EnumDescription("���¼ģʽ")]
				DEVSTAT_NOLOGON = 0x10,
			
				[EnumDescription("������")]
				DEVSTAT_LOCKED = 0x20,
			
				[EnumDescription("����������")]
				DEVSTAT_CDLOCKED = 0x40,
			
				[EnumDescription("U��������")]
				DEVSTAT_USBLOCKED = 0x80,
			
				[EnumDescription("��ʱ�뿪(Leave for a while)")]
				DEVSTAT_LEAVEFW = 0x100,
			
				[EnumDescription("�ѽ��")]
				DEVSTAT_LOAN = 0x200,
			
				[EnumDescription("�㲥��")]
				DEVSTAT_TELECAST = 0x400,
			
				[EnumDescription("���¼��������¼��")]
				DEVSTAT_NOLOGONFACE = 0x800,
			
				[EnumDescription("���URL Cache")]
				DEVSTAT_CLEARURLCACHE = 0x1000,
			
				[EnumDescription("��Դδ��ͨ")]
				DRUNSTAT_POWEROFF = 0x2000,
			
				[EnumDescription("��Դ�ѽ�ͨ")]
				DRUNSTAT_POWERON = 0x4000,
			
				[EnumDescription("��Դ������")]
				DRUNSTAT_POWERWORKING = 0x8000,
			
				[EnumDescription("�ſ�")]
				DRUNSTAT_DOOROPEN = 0x10000,
			
				[EnumDescription("�Ź�")]
				DRUNSTAT_DOORCLOSED = 0x20000,
			
				[EnumDescription("����������")]
				DRUNSTAT_CONTROLLER_TROUBLE = 0x40000,
			
				[EnumDescription("�����ж�����")]
				DRUNSTAT_WITHPERSON = 0x80000,
			
				[EnumDescription("�����ж�����")]
				DRUNSTAT_NOPERSON = 0x100000,
			
				[EnumDescription("��ʱ�뿪��֪ͨ")]
				DEVSTAT_LEAVENOTICED = 0x200000,
			
		}

	
		public uint? dwStatChgTime;		/*״̬�ı俪ʼʱ��(time������)*/
	
		public string szStatInfo;		/*״̬����*/
	
		public uint? dwClassID;		/*�豸�������*/
	
		public string szClassSN;		/*�豸�����*/
	
		public string szClassName;		/*�豸�������*/
	
		public uint? dwClassKind;		/*���(��UNIDEVCLS��Kind����)*/
	
		public uint? dwKindID;		/*�豸����*/
	
		public string szKindName;		/*�豸����*/
	
		public string szFuncCode;		/*�豸������;����*/
	
		public string szModel;		/*�豸�ͺ�*/
	
		public string szSpecification;		/*�豸���*/
	
		public uint? dwProperty;		/*�豸���ԣ�ǰ16��ΪUNIDEVKIND����*/
	
		[FlagsAttribute]
		public enum DWPROPERTY : uint
		{
			
				[EnumDescription("֧�ֻ����")]
				DEVPROP_ACTIVITYPLAN = 0x10000,
			
				[EnumDescription("����ԤԼ��ֱ�ӵ�¼")]
				DEVPROP_DIRECTLOGIN = 0x20000,
			
				[EnumDescription("��֧��ǿ���»�")]
				DEVPROP_UNFORCEOUT = 0x40000,
			
				[EnumDescription("��֧������ԤԼ")]
				DEVPROP_NORESV = 0x80000,
			
				[EnumDescription("��ѧ�豸")]
				DEVPROP_FORTEACHING = 0x100000,
			
				[EnumDescription("�����豸")]
				DEVPROP_FORRESEARCH = 0x200000,
			
				[EnumDescription("���豸�ĸ����豸")]
				DEVPROP_SUB = 0x400000,
			
				[EnumDescription("��֧�ֹ���Աֱ�ӵ�¼")]
				DEVPROP_NOMANAGERIN = 0x800000,
			
				[EnumDescription("ϵͳ������Ļ�㲥")]
				DEVPROP_IDLECAST = 0x1000000,
			
				[EnumDescription("ӵ���豸���Ź���")]
				DEVPROP_DEVOPENRULE = 0x2000000,
			
				[EnumDescription("�ɶ������ŵ����豸")]
				DEVPROP_MAIN = 0x4000000,
			
				[EnumDescription("������������豸")]
				DEVPROP_THIRDSHARE = 0x8000000,
			
				[EnumDescription("UNIDEVICE�������ȡ��16��")]
				DEVPROP_MASK = 0xFFFF0000,
			
		}

	
		public uint? dwMaxUsers;		/*���ͬʱʹ������*/
	
		public uint? dwMinUsers;		/*����ͬʱʹ������*/
	
		public string szOperaCert;		/*����֤��*/
	
		public string szDevKindURL;		/*�豸������ϸ���ܵ�URL��ַ*/
	
		public string szDevURL;		/*�豸��ϸ���ܵ�URL��ַ*/
	
		public uint? dwManGroupID;		/*����Ա��ID*/
	
		public string szManGroupName;		/*����Ա������*/
	
		public uint? dwUseGroupID;		/*�豸ʹ����ID*/
	
		public uint? dwOpenRuleSN;		/*���Ź�����*/
	
		public string szPCName;		/*��¼�豸������*/
	
		public string szIP;		/*�����IP��ַ*/
	
		public string szMAC;		/*������ַ*/
	
		public string szMemo;		/*˵����Ϣ*/
	
		public uint? dwRoomID;		/*����ID*/
	
		public string szRoomNo;		/*����ţ����ظ�,���ڹ����Ž�����ʽB-F-N,�������-¥���-�����ţ�*/
	
		public string szRoomName;		/*��������*/
	
		public uint? dwManMode;		/*���Ʒ�ʽ(UNIROOM�ж���)*/
	
		public string szFloorNo;		/*����¥��*/
	
		public uint? dwBuildingID;		/*¥��ID*/
	
		public string szBuildingNo;		/*��¥���(*/
	
		public string szBuildingName;		/*��¥����(*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabSN;		/*ʵ���ұ��*/
	
		public string szLabName;		/*ʵ��������*/
	
		public uint? dwDeptID;		/*����ID*/
	
		public string szDeptName;		/*����*/
	
		public uint? dwCampusID;		/*У��ID*/
	
		public string szCampusName;		/*У������*/
	
		public uint? dwCampusKind;		/*У�����ͣ���չ��*/
	
		public uint? dwVisitTimes;		/*�������*/
	
		public uint? dwUsePurpose;		/*ͬUniFee��dwPurpose*/
	
		public string szExtInfo;		/*��չ��Ϣ*/
	
		public uint? dwURLCtrl;		/*�������ģʽ*/
	
		public uint? dwURLCtrlParam;		/*��������趨ֵ�����ݲ�ͬ�ļ��ģʽ���岻һ��)*/
	
		public uint? dwURLEndTime;		/*��ֹʱ��*/
	
		public string szURLCtrlName;		/*�����������*/
	
		public uint? dwSWCtrl;		/*������ģʽ*/
	
		public uint? dwSWCtrlParam;		/*�������趨ֵ�����ݲ�ͬ�ļ��ģʽ���岻һ��)*/
	
		public uint? dwSWEndTime;		/*��ؽ���ʱ��*/
	
		public string szSWCtrlName;		/*����������*/
	
		public uint? dwAttendantID;		/*ֵ��Ա�˺�*/
	
		public string szAttendantName;		/*ֵ��Ա����*/
	
		public string szAttendantTel;		/*ֵ��Ա�绰*/
	
	public DEVRESV[] DevResv;		/*CUniTable[DEVRESV](dwReqProp��DEVREQ_NEEDDEVRESVʱ�ŷ���)*/
	
	public DEVUSEINFO[] DevUse;		/*CUniTable[DEVUSEINFO](dwReqProp��DEVREQ_NEEDDEVUSEʱ�ŷ���)*/
	
	public SAMPLEINFO[] DevSample;		/*CUniTable[SAMPLEINFO](dwReqProp��DEVREQ_NEEDSAMPLEʱ�ŷ���)*/
		};

	/*��ȡ�豸���ñ�����*/
	public struct DEVCFGREQ
	{
		private Reserved reserved;
		
		public uint? dwMainDevID;		/*���豸ID*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*�豸���ñ�*/
	public struct DEVCFG
	{
		private Reserved reserved;
		
		public uint? dwMainDevID;		/*���豸ID*/
	
		public uint? dwSubDevType;		/*�����豸���*/
	
		[FlagsAttribute]
		public enum DWSUBDEVTYPE : uint
		{
			
				[EnumDescription("�����豸���̶����ã�")]
				SUBDEVTYPE_STANDARD = 0x1,
			
				[EnumDescription("��ѡ�豸��ԤԼǰ�����룬�ɹ�����Ա��ǰ׼����")]
				SUBDEVTYPE_SELECTABLE = 0x2,
			
		}

	
		public uint? dwDevID;		/*�豸ID*/
	
		public uint? dwDevSN;		/*�豸���*/
	
		public string szAssertSN;		/*�û����ʲ����*/
	
		public string szOriginSN;		/*ԭ��ϵ�к�*/
	
		public string szDevName;		/*ʵ���豸����*/
	
		public uint? dwUnitPrice;		/*�豸����*/
	
		public uint? dwPurchaseDate;		/*�ɹ����� YYYYMMDD*/
	
		public uint? dwDevStat;		/*�豸״̬*/
	
		public uint? dwClassID;		/*�豸�������*/
	
		public string szClassSN;		/*�豸�����*/
	
		public string szClassName;		/*�豸�������*/
	
		public uint? dwClassKind;		/*���(��UNIDEVCLS��Kind����)*/
	
		public uint? dwKindID;		/*�豸����*/
	
		public string szKindName;		/*�豸����*/
	
		public string szFuncCode;		/*�豸������;����*/
	
		public string szModel;		/*�豸�ͺ�*/
	
		public string szSpecification;		/*�豸���*/
	
		public string szDevKindURL;		/*�豸������ϸ���ܵ�URL��ַ*/
	
		public string szDevURL;		/*�豸��ϸ���ܵ�URL��ַ*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*���ܼ����λ״̬*/
	public struct SEATDETECTSTAT
	{
		private Reserved reserved;
		
		public string szFloorNo;		/*����¥��*/
	
		public string szRoomNo;		/*�����*/
	
		public uint? dwDevSN;		/*��λ���*/
	
		public string szTagID;		/*RFID��ǩID*/
	
		public uint? dwMonitorStat;		/*���״̬(dwRunStat ���������˶��壩*/
	
		public uint? dwChangeTime;		/*״̬�ı�ʱ��*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*����Ա�˹������豸ʹ��*/
	public struct DEVMANUSE
	{
		private Reserved reserved;
		
		public uint? dwLabID;		/*ʵ����ID*/
	
		public uint? dwDevID;		/*�豸ID*/
	
		public uint? dwResvID;		/*ԤԼID*/
	
		public uint? dwMode;		/*����ģʽ*/
	
		[FlagsAttribute]
		public enum DWMODE : uint
		{
			
				[EnumDescription("��ʼʹ��)")]
				MANMODE_STARTUSE = 0x1,
			
				[EnumDescription("ֹͣʹ��")]
				MANMODE_STOPUSE = 0x2,
			
				[EnumDescription("��ʱ�뿪")]
				MANMODE_LEAVE = 0x4,
			
		}

	
		public string szExtInfo;		/*��չ��Ϣ*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*��ȡ�豸������������*/
	public struct DEVCFGKINDREQ
	{
		private Reserved reserved;
		
		public uint? dwMainDevID;		/*���豸ID*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*�豸��������*/
	public struct DEVCFGKIND
	{
		private Reserved reserved;
		
		public uint? dwMainDevID;		/*���豸ID*/
	
		public uint? dwSubDevType;		/*�����豸���*/
	
		public uint? dwSubDevNum;		/*�����豸����*/
	
		public uint? dwKindID;		/*�豸����*/
	
		public string szKindName;		/*�豸����*/
	
		public string szFuncCode;		/*�豸������;����*/
	
		public string szModel;		/*�豸�ͺ�*/
	
		public string szSpecification;		/*�豸���*/
	
		public string szDevKindURL;		/*�豸������ϸ���ܵ�URL��ַ*/
	
		public uint? dwClassID;		/*�豸�������*/
	
		public string szClassSN;		/*�豸�����*/
	
		public string szClassName;		/*�豸�������*/
	
		public uint? dwClassKind;		/*���(��UNIDEVCLS��Kind����)*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*�����豸ֵ��Ա*/
	public struct DEVATTENDANT
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*�豸ID*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public uint? dwAttendantID;		/*ֵ��Ա�˺�*/
	
		public string szAttendantName;		/*ֵ��Ա����*/
	
		public string szAttendantTel;		/*ֵ��Ա�绰*/
		};

	/*�豸ʹ�÷ѵľ��ѷ����������*/
	public struct DEVFARREQ
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*�豸ID*/
		};

	/*�豸ʹ�÷ѵľ��ѷ������*/
	public struct DEVFAR
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*�豸ID*/
	
		public uint? dwFeeType;		/*�շ����(FEEDETAIL����)*/
	
		public uint? dwTestRate;		/*�������Էѱ���*/
	
		public uint? dwOpenFundRate;		/*���Ż������*/
	
		public uint? dwServiceRate;		/*����ѱ���*/
	
		public string szMemo;		/*��ע*/
		};

	/*��ȡԤԼ�豸�б�*/
	public struct DEVFORRESVREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*��ʽ*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("ȫ���豸")]
				DEVFORRESV_ALL = 0,
			
				[EnumDescription("�豸ID")]
				DEVFORRESV_DEVID = 1,
			
				[EnumDescription("����")]
				DEVFORRESV_BYNAME = 7,
			
				[EnumDescription("ʵ����ID")]
				DEVFORRESV_LABID = 0x101,
			
				[EnumDescription("����ID")]
				DEVFORRESV_ROOMID = 0x102,
			
		}

	
		public string szKey;		/*��ѯ������*/
	
		public string szSubKey;		/*��ѯ��������DEVFORRESV_DEVIDʱ��LabID*/
	
		public uint? dwKindID;		/*��������*/
	
		public uint? dwClassID;		/*�����������ID*/
	
		public uint? dwResvPurpose;		/*ԤԼ��;*/
	
		public uint? dwBeginTime;		/*��ʼʱ��*/
	
		public uint? dwEndTime;		/*����ʱ��*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*�豸��Ϣ*/
	public struct DEVFORRESV
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*�豸ID*/
	
		public uint? dwDevSN;		/*�豸���*/
	
		public string szDevName;		/*ʵ���豸����*/
	
		public uint? dwClassID;		/*�豸�������*/
	
		public string szClassName;		/*�豸�������*/
	
		public uint? dwKindID;		/*�豸����*/
	
		public string szKindName;		/*�豸��������*/
	
		public uint? dwMaxUsers;		/*���ͬʱʹ������*/
	
		public uint? dwMinUsers;		/*����ͬʱʹ������*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabSN;		/*ʵ���ұ��*/
	
		public string szLabName;		/*ʵ��������*/
	
		public uint? dwRoomID;		/*����ID*/
	
		public string szRoomNo;		/*����ţ����ظ�,���ڹ����Ž�����ʽB-F-N,�������-¥���-�����ţ�*/
	
		public string szRoomName;		/*��������*/
	
		public string szDevKindURL;		/*�豸��ϸ���ܵ�URL��ַ*/
	
		public string szDevURL;		/*�豸URL��ַ*/
	
		public string szExtInfo;		/*��չ��Ϣ*/
	
		public uint? dwResvRate;		/*ԤԼ��(1-100)*/
	
		public uint? dwResvRuleSN;		/*����ԤԼ����*/
	
		public uint? dwOpenRuleSN;		/*��������ʱ���*/
	
		public uint? dwFeeSN;		/*�����շѱ�׼*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*��ѯ�豸ԤԼ״̬����*/
	public struct DEVRESVSTATREQ
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*�豸ID*/
	
		public string szDevName;		/*ʵ���豸����*/
	
		public string szLabIDs;		/*ʵ����ID,����ö��Ÿ���*/
	
		public string szRoomIDs;		/*����ID,����ö��Ÿ���*/
	
		public string szKindIDs;		/*��������,����ö��Ÿ���*/
	
		public string szClassIDs;		/*�����������ID,����ö��Ÿ���*/
	
		public string szDeptIDs;		/*ѧԺID,����ö��Ÿ���*/
	
		public string szBuildingIDs;		/*¥��ID,����ö��Ÿ���*/
	
		public string szCampusIDs;		/*У��ID,����ö��Ÿ���*/
	
		public uint? dwResvPurpose;		/*ԤԼ��;*/
	
		public uint? dwProperty;		/*�豸����(�ο�UNIDEVKIND����)*/
	
		public string szDates;		/*��ѯ����,����ö��Ÿ���*/
	
		public uint? dwClassKind;		/*���(��UNIDEVCLS��Kind����)*/
	
		public uint? dwResvUsers;		/*ԤԼ����*/
	
		public uint? dwExtRelatedID;		/*��չ����ID*/
	
		public uint? dwReqProp;		/*���󸽼�����*/
	
		[FlagsAttribute]
		public enum DWREQPROP : uint
		{
			
				[EnumDescription("��Ҫÿ�쿪�Ź���")]
				DRREQ_NEEDALLDAYOPENRULE = 0x1,
			
				[EnumDescription("�鿴�����豸")]
				DRREQ_VIEWALL = 0x2,
			
				[EnumDescription("���ȡ�����豸ԤԼ��Ϣ")]
				DRREQ_NEEDTHIRDSHAREDEV = 0x4,
			
		}

	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*�豸ԤԼ��Ϣ*/
	public struct DEVRESVTIME
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*ԤԼID*/
	
		public uint? dwPurpose;		/*ԤԼ��;*/
	
		public uint? dwStatus;		/*ԤԼ״̬*/
	
		public uint? dwPreDate;		/*ԤԼ��ʼ����*/
	
		public uint? dwBegin;		/*��ʼʱ��(HHMM��YYYYMMDD)*/
	
		public uint? dwEnd;		/*����ʱ��(HHMM��YYYYMMDD)*/
	
		public uint? dwOwner;		/*ԤԼ��(������)*/
	
		public string szOwnerName;		/*ԤԼ������*/
	
		public string szMemberName;		/*��Ա����*/
	
		public string szTestName;		/*ʵ������*/
	
		public uint? dwSex;		/*ԤԼ���Ա�*/
		};

	/*�豸ԤԼ״̬*/
	public struct DEVRESVSTAT
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*�豸ID*/
	
		public uint? dwDevSN;		/*�豸���*/
	
		public string szDevName;		/*ʵ���豸����*/
	
		public uint? dwDevStat;		/*�豸״̬*/
	
		public uint? dwRunStat;		/*����״̬*/
	
		public uint? dwClassID;		/*�豸�������*/
	
		public string szClassName;		/*�豸�������*/
	
		public uint? dwKindID;		/*�豸����*/
	
		public string szKindName;		/*�豸��������*/
	
		public uint? dwMaxUsers;		/*���ͬʱʹ������*/
	
		public uint? dwMinUsers;		/*����ͬʱʹ������*/
	
		public uint? dwProperty;		/*�豸����(�ο�UNIDEVKIND����)*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabSN;		/*ʵ���ұ��*/
	
		public string szLabName;		/*ʵ��������*/
	
		public uint? dwRoomID;		/*����ID*/
	
		public string szRoomNo;		/*����ţ����ظ�,���ڹ����Ž�����ʽB-F-N,�������-¥���-�����ţ�*/
	
		public string szRoomName;		/*��������*/
	
		public string szFloorNo;		/*����¥��*/
	
		public uint? dwBuildingID;		/*¥��ID*/
	
		public string szBuildingNo;		/*��¥���(*/
	
		public string szBuildingName;		/*��¥����(*/
	
		public uint? dwDeptID;		/*����ID*/
	
		public string szDeptName;		/*����*/
	
		public uint? dwCampusID;		/*У��ID*/
	
		public string szCampusName;		/*У������*/
	
		public uint? dwCampusKind;		/*У�����ͣ���չ��*/
	
		public string szDevKindURL;		/*�豸��ϸ���ܵ�URL��ַ*/
	
		public string szDevURL;		/*�豸URL��ַ*/
	
		public string szExtInfo;		/*��չ��Ϣ*/
	
		public uint? dwOpenLimit;		/*�������Ƽ�GROUPOPENRULE����+���涨��*/
	
		[FlagsAttribute]
		public enum DWOPENLIMIT : uint
		{
			
				[EnumDescription("��ѡʱ�䲻��ԤԼ")]
				OPENLIMIT_NORESV = 0x1000,
			
		}

	
	public UNIRESVRULE szRuleInfo;		/*CUniStruct[UNIRESVRULE]*/
	
	public DAYOPENRULE[] szOpenInfo;		/*CUniTable[DAYOPENRULE]*/
	
	public DEVRESVTIME[] szResvInfo;		/*CUniTable[DEVRESVTIME]*/
		};

	/*��ѯ�豸����ԤԼ״̬����*/
	public struct DEVLONGRESVSTATREQ
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*�豸ID*/
	
		public string szDevName;		/*ʵ���豸����*/
	
		public string szLabIDs;		/*ʵ����ID,����ö��Ÿ���*/
	
		public string szRoomIDs;		/*����ID,����ö��Ÿ���*/
	
		public string szKindIDs;		/*��������,����ö��Ÿ���*/
	
		public string szClassIDs;		/*�����������ID,����ö��Ÿ���*/
	
		public string szDeptIDs;		/*ѧԺID,����ö��Ÿ���*/
	
		public string szBuildingIDs;		/*¥��ID,����ö��Ÿ���*/
	
		public string szCampusIDs;		/*У��ID,����ö��Ÿ���*/
	
		public uint? dwResvPurpose;		/*ԤԼ��;*/
	
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��ʼ����*/
	
		public uint? dwClassKind;		/*���(��UNIDEVCLS��Kind����)*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*�豸����ԤԼ״̬*/
	public struct DEVLONGRESVSTAT
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*�豸ID*/
	
		public uint? dwDevSN;		/*�豸���*/
	
		public string szDevName;		/*ʵ���豸����*/
	
		public uint? dwClassID;		/*�豸�������*/
	
		public string szClassName;		/*�豸�������*/
	
		public uint? dwKindID;		/*�豸����*/
	
		public string szKindName;		/*�豸��������*/
	
		public uint? dwMaxUsers;		/*���ͬʱʹ������*/
	
		public uint? dwMinUsers;		/*����ͬʱʹ������*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabSN;		/*ʵ���ұ��*/
	
		public string szLabName;		/*ʵ��������*/
	
		public uint? dwRoomID;		/*����ID*/
	
		public string szRoomNo;		/*����ţ����ظ�,���ڹ����Ž�����ʽB-F-N,�������-¥���-�����ţ�*/
	
		public string szRoomName;		/*��������*/
	
		public string szFloorNo;		/*����¥��*/
	
		public uint? dwBuildingID;		/*¥��ID*/
	
		public string szBuildingNo;		/*��¥���(*/
	
		public string szBuildingName;		/*��¥����(*/
	
		public uint? dwDeptID;		/*����ID*/
	
		public string szDeptName;		/*����*/
	
		public uint? dwCampusID;		/*У��ID*/
	
		public string szCampusName;		/*У������*/
	
		public uint? dwCampusKind;		/*У�����ͣ���չ��*/
	
		public string szDevKindURL;		/*�豸��ϸ���ܵ�URL��ַ*/
	
		public string szDevURL;		/*�豸URL��ַ*/
	
		public string szExtInfo;		/*��չ��Ϣ*/
	
	public UNIRESVRULE szRuleInfo;		/*CUniStruct[UNIRESVRULE]*/
	
	public DEVRESVTIME[] szResvInfo;		/*CUniTable[DEVRESVTIME]*/
		};

	/*��ѯʵ����ԤԼ״̬����*/
	public struct LABRESVSTATREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*��ʽ*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("ȫ��ʵ����")]
				LABRESVSTAT_ALL = 0,
			
				[EnumDescription("ʵ����ID")]
				LABRESVSTAT_LABID = 1,
			
		}

	
		public string szGetKey;		/*��ѯ������*/
	
		public uint? dwDate;		/*��ѯ����*/
		};

	/*��ѧԤԼ��ϸ��Ϣ*/
	public struct TEACHINGRESVINFO
	{
		private Reserved reserved;
		
		public uint? dwTestItemID;		/*ʵ����ĿID*/
	
		public uint? dwTestCardID;		/*ʵ����Ŀ��ID*/
	
		public string szTestName;		/*ʵ������*/
	
		public uint? dwTestPlanID;		/*ʵ��ƻ�ID*/
	
		public string szTestPlanName;		/*ʵ��ƻ�����*/
	
		public uint? dwTeacherID;		/*��ʦ���ʺţ�*/
	
		public string szTeacherName;		/*��ʦ����*/
	
		public uint? dwCourseID;		/*�γ�ID*/
	
		public string szCourseName;		/*�γ�����*/
	
		public uint? dwGroupID;		/*�Ͽΰ༶*/
	
		public string szGroupName;		/*�Ͽΰ༶����*/
	
		public uint? dwTeachingTime;		/*��ѧʱ��(��ʽ��UNIRESERVE)*/
	
		public uint? dwResvStat;		/*ԤԼ״̬(��ʽ��UNIRESERVE)*/
		};

	/*ʵ����ԤԼ״̬*/
	public struct LABRESVSTAT
	{
		private Reserved reserved;
		
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabSN;		/*ʵ���ұ��*/
	
		public string szLabName;		/*ʵ��������*/
	
		public uint? dwDevNum;		/*�豸��*/
	
	public TEACHINGRESVINFO[] szResvInfo;		/*CUniTable[TEACHINGRESVINFO]*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*��ѯ����ԤԼ״̬����*/
	public struct ROOMRESVSTATREQ
	{
		private Reserved reserved;
		
		public string szRoomName;		/*��������*/
	
		public string szLabIDs;		/*ʵ����ID,����ö��Ÿ���*/
	
		public string szRoomIDs;		/*����ID,����ö��Ÿ���*/
	
		public string szKindIDs;		/*��������,����ö��Ÿ���*/
	
		public uint? dwRoomProp;		/*��������*/
	
		public uint? dwUnNeedRoomProp;		/*��������������*/
	
		public uint? dwMinDevNum;		/*�����豸��*/
	
		public uint? dwMaxDevNum;		/*����豸��*/
	
		public uint? dwDate;		/*��ѯ����*/
		};

	/*����ԤԼ״̬*/
	public struct ROOMRESVSTAT
	{
		private Reserved reserved;
		
		public uint? dwRoomID;		/*����ID*/
	
		public string szRoomNo;		/*������*/
	
		public string szRoomName;		/*��������*/
	
		public uint? dwRoomProp;		/*��������*/
	
		public uint? dwDevNum;		/*�豸��*/
	
	public TEACHINGRESVINFO[] szResvInfo;		/*CUniTable[TEACHINGRESVINFO]*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*��ѯ�������ԤԼ״̬����*/
	public struct RGRESVSTATREQ
	{
		private Reserved reserved;
		
		public uint? dwMinDevNum;		/*�����豸��*/
	
		public uint? dwMaxDevNum;		/*����豸��*/
	
		public uint? dwDate;		/*��ѯ����*/
		};

	/*�������ԤԼ״̬*/
	public struct RGRESVSTAT
	{
		private Reserved reserved;
		
		public uint? dwRGID;		/*����ID*/
	
		public string szRGName;		/*�����������*/
	
		public uint? dwRoomNum;		/*��Ϸ�����*/
	
		public uint? dwDevNum;		/*�豸��*/
	
	public TEACHINGRESVINFO[] szResvInfo;		/*CUniTable[TEACHINGRESVINFO]*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*��ȡԤԼ�豸�б�(������)*/
	public struct DEVKINDFORRESVREQ
	{
		private Reserved reserved;
		
		public string szKindName;		/*ʵ���豸����*/
	
		public string szLabIDs;		/*ʵ����ID,����ö��Ÿ���*/
	
		public string szRoomIDs;		/*����ID,����ö��Ÿ���*/
	
		public string szKindIDs;		/*��������,����ö��Ÿ���*/
	
		public string szClassIDs;		/*�����������ID,����ö��Ÿ���*/
	
		public string szDeptIDs;		/*ѧԺID,����ö��Ÿ���*/
	
		public string szBuildingIDs;		/*¥��ID,����ö��Ÿ���*/
	
		public string szCampusIDs;		/*У��ID,����ö��Ÿ���*/
	
		public uint? dwClassKind;		/*���(��UNIDEVCLS��Kind����)*/
	
		public uint? dwResvPurpose;		/*ԤԼ��;*/
	
		public uint? dwProperty;		/*�豸����(�ο�UNIDEVKIND����)*/
	
		public uint? dwDate;		/*��ѯ����*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*��ȡ����ԤԼ�豸�б�(������)*/
	public struct DEVKINDFORLONGRESVREQ
	{
		private Reserved reserved;
		
		public string szKindName;		/*ʵ���豸����*/
	
		public string szLabIDs;		/*ʵ����ID,����ö��Ÿ���*/
	
		public string szRoomIDs;		/*����ID,����ö��Ÿ���*/
	
		public string szKindIDs;		/*��������,����ö��Ÿ���*/
	
		public string szClassIDs;		/*�����������ID,����ö��Ÿ���*/
	
		public string szDeptIDs;		/*ѧԺID,����ö��Ÿ���*/
	
		public string szBuildingIDs;		/*¥��ID,����ö��Ÿ���*/
	
		public string szCampusIDs;		/*У��ID,����ö��Ÿ���*/
	
		public uint? dwClassKind;		/*���(��UNIDEVCLS��Kind����)*/
	
		public uint? dwResvPurpose;		/*ԤԼ��;*/
	
		public uint? dwProperty;		/*�豸����(�ο�UNIDEVKIND����)*/
	
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��ʼ����*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*�豸��Ϣ*/
	public struct DEVKINDFORRESV
	{
		private Reserved reserved;
		
		public uint? dwKindID;		/*�豸����*/
	
		public string szKindName;		/*ʵ���豸����*/
	
		public uint? dwClassID;		/*�豸�������*/
	
		public string szClassName;		/*�豸�������*/
	
		public string szFuncCode;		/*�豸������;����*/
	
		public string szModel;		/*�豸�ͺ�*/
	
		public string szSpecification;		/*�豸���*/
	
		public uint? dwProperty;		/*�豸����*/
	
		public uint? dwMaxUsers;		/*���ͬʱʹ������*/
	
		public uint? dwMinUsers;		/*����ͬʱʹ������*/
	
		public uint? dwTotalNum;		/*�豸����*/
	
		public string szOperaCert;		/*����֤��*/
	
		public string szDevKindURL;		/*�豸��ϸ���ܵ�URL��ַ*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabSN;		/*ʵ���ұ��*/
	
		public string szLabName;		/*ʵ��������*/
	
		public uint? dwRoomID;		/*����ID*/
	
		public string szRoomNo;		/*����ţ����ظ�,���ڹ����Ž�����ʽB-F-N,�������-¥���-�����ţ�*/
	
		public string szRoomName;		/*��������*/
	
		public string szFloorNo;		/*����¥��*/
	
		public uint? dwBuildingID;		/*¥��ID*/
	
		public string szBuildingNo;		/*��¥���(*/
	
		public string szBuildingName;		/*��¥����(*/
	
		public uint? dwDeptID;		/*����ID*/
	
		public string szDeptName;		/*����*/
	
		public uint? dwCampusID;		/*У��ID*/
	
		public string szCampusName;		/*У������*/
	
		public uint? dwCampusKind;		/*У�����ͣ���չ��*/
	
		public string szUsableNumArray;		/* ����ԤԼÿ�ֽڴ���0-24���Ӧ�ķ��ӵĿ����豸��(����1440),
            ����ԤԼ��ʾÿ��Ŀ����豸������Ϊ��ѯ������=A��ʾ����9̨�����豸,U��ʾ������ */
	
		public uint? dwOpenLimit;		/*�������Ƽ�GROUPOPENRULE����+DEVRESVSTAT����*/
	
	public UNIRESVRULE szRuleInfo;		/*CUniStruct[UNIRESVRULE]*/
	
	public DAYOPENRULE[] szOpenInfo;		/*CUniTable[DAYOPENRULE]*/
	
	public UNIFEE szFeeInfo;		/*CUniStruct[UNIFEE]*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*��ȡԤԼ�豸�б�(������)*/
	public struct ROOMFORRESVREQ
	{
		private Reserved reserved;
		
		public string szRoomName;		/*ʵ���豸����*/
	
		public string szFloorNo;		/*����¥��*/
	
		public string szRoomIDs;		/*����ID,����ö��Ÿ���*/
	
		public uint? dwKindID;		/*��������*/
	
		public string szBuildingIDs;		/*¥��ID,����ö��Ÿ���*/
	
		public string szCampusIDs;		/*У��ID,����ö��Ÿ���*/
	
		public uint? dwClassKind;		/*���(��UNIDEVCLS��Kind����)*/
	
		public uint? dwResvPurpose;		/*ԤԼ��;*/
	
		public uint? dwDate;		/*��ѯ����*/
	
		public uint? dwBeginTime;		/*ԤԼ��ʼʱ��*/
	
		public uint? dwEndTime;		/*ԤԼ����ʱ��*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*����ԤԼ��Ϣ*/
	public struct ROOMFORRESV
	{
		private Reserved reserved;
		
		public uint? dwRoomID;		/*����ID*/
	
		public string szRoomName;		/*��������*/
	
		public string szRoomNo;		/*�����*/
	
		public string szFloorNo;		/*����¥��*/
	
		public uint? dwOpenBegin;		/*��ʼʱ��(HHMM)*/
	
		public uint? dwOpenEnd;		/*����ʱ��(HHMM)*/
	
		public uint? dwTotalNum;		/*�豸����*/
	
		public uint? dwUsableNum;		/*�����豸��*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*��ȡԤԼ�����豸�����*/
	public struct RESVUSABLEDEVREQ
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*ԤԼID*/
		};

	/*����豸��ϸ*/
	public struct RESVUSABLEDEV
	{
		private Reserved reserved;
		
		public uint? dwLabID;		/*ʵ����ID*/
	
		public uint? dwDevID;		/*�豸ID*/
	
		public uint? dwDevSN;		/*�豸SN*/
	
		public uint? dwDevKind;		/*�豸����*/
	
		public string szDevName;		/*�豸��*/
	
		public string szKindName;		/*�豸�����*/
	
		public uint? dwRoomID;		/*����ID*/
	
		public string szRoomNo;		/*������*/
	
		public string szRoomName;		/*��������*/
	
		public string szExtInfo;		/*��չ��Ϣ*/
		};

	/*�ͻ���ע�ᵽ������*/
	public struct DEVREGISTREQ
	{
		private Reserved reserved;
		
		public uint? dwStaSN;		/*վ����*/
	
		public string szCltVersion;		/*�ͻ��˰汾*/
	
		public string szPCName;		/*��������*/
	
		public string szIP;		/*IP��ַ*/
	
		public string szMAC;		/*������ַ*/
	
	public DEVICECONFIG[] szCfgInfo;		/*������ϢCUniTable[DEVICECONFIG]*/
		};

	/*�������Կͻ���ע�����Ӧ*/
	public struct DEVREGISTRES
	{
		private Reserved reserved;
		
		public uint? dwSessionID;		/*�����������SessionID*/
	
	public UNIVERSION SrvVer;		/*UNIVERSION �ṹ*/
	
		public string szCurTime;		/*������ʱ�� YYYY-MM-DD HH:MM:SS*/
	
		public uint? dwDevSN;		/*���豸���*/
	
		public uint? dwDevID;		/*���豸�ɣĺ�*/
	
		public uint? dwLabID;		/*��ʵ���ңɣĺ�*/
	
		public uint? dwFunc;		/*ʹ�ù���ģ��*/
	
		public uint? dwParam;		/*��������*/
	
		[FlagsAttribute]
		public enum DWPARAM : uint
		{
			
				[EnumDescription("�������ʱ��ʹ��")]
				CLIENTPARA_NETFAULT = 1,
			
				[EnumDescription("ע��ʱ�Զ��ػ�")]
				CLIENTPARA_AUTOSHUTDOWN = 2,
			
				[EnumDescription("֧����Ѻ��շ�ģʽѡ��")]
				CLIENTPARA_MULTIUSEMODE = 4,
			
				[EnumDescription("������")]
				CLIENTPARA_CLOUDDISK = 0x100,
			
				[EnumDescription("��ֹ�ͻ����޸�����")]
				CLIENTPARA_PSFORBID = 0x1000,
			
		}

	
		public uint? dwDevStat;		/*�豸״̬*/
	
		public uint? dwRunStat;		/*����״̬*/
	
		public uint? dwPasswdCode;		/*ж����������*/
	
		public string szLabName;		/*ʵ��������*/
	
		public string szDisplayInfo;		/*��¼������ʾ��Ϣ*/
	
		public string szCastParam;		/*��Ļ�㲥����*/
	
	public UNIDEVICE DevInfo;		/*UNIDEVICE �ṹ*/
		};

	/*�ͻ��˵�¼����*/
	public struct DEVLOGONREQ
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*�ͻ����豸��ID��*/
	
		public uint? dwLabID;		/*ʵ���ҵ�ID��*/
	
		public uint? dwLogonType;		/*��¼���*/
	
		[FlagsAttribute]
		public enum DWLOGONTYPE : uint
		{
			
				[EnumDescription("ϵͳ�Զ���¼")]
				LOGONTYPE_SYSTEM = 1,
			
				[EnumDescription("�û���¼")]
				LOGONTYPE_USER = 2,
			
				[EnumDescription("�շ�ģʽ")]
				LOGONTYPE_FEEMODE = 0x100,
			
				[EnumDescription("���ģʽ")]
				LOGONTYPE_FREEMODE = 0x200,
			
		}

	
		public string szLogonName;		/*��¼��*/
	
		public string szPasswd;		/*�û�����*/
	
		public string szMemo;		/*��ע����չ�ã�*/
		};

	/*�ͻ��˵�¼�������Ӧ*/
	public struct DEVLOGONRES
	{
		private Reserved reserved;
		
	public UNIACCOUNT szAccInfo;		/*ʹ������Ϣ(UNIACCOUNT�ṹ)*/
	
		public uint? dwPurpose;		/*��;*/
	
		public string szDeclareInfo;		/*����ϵͳ��������Ϣ*/
	
	public UNIRESERVE ResvInfo;		/*ԤԼ��Ϣ*/
		};

	/*�ͻ��˲�ѯ��ǰ��Ϣ*/
	public struct DEVQUERYREQ
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*�ͻ����豸��ID��*/
	
		public uint? dwLabID;		/*ʵ���ҵ�ID��*/
	
		public uint? dwAccNo;		/*�ʺ�*/
	
		public string szMemo;		/*��ע����չ�ã�*/
		};

	/*�ͻ���ע��ʱ�û�ѡ��ļƷ���Ϣ*/
	public struct USERFEECHECK
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*ԤԼ��*/
	
		public uint? dwAccNo;		/*�û��˺�*/
	
		public uint? dwFeeMode;		/*�շѷ�ʽ(������UNIRESVRULE)*/
	
	public RESVSAMPLE[] ResvSample;		/*CUniTable[RESVSAMPLE]*/
	
	public UNIACCTINFO FeeInfo;		/*CUniStruct[UNIACCTINFO]*/
	
		public string szMemo;		/*��ע����չ�ã�*/
		};

	/*�ͻ���ע������*/
	public struct DEVLOGOUTREQ
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*�ͻ����豸��ID��*/
	
		public uint? dwLabID;		/*ʵ���ҵ�ID��*/
	
		public uint? dwAccNo;		/*�ʺ�*/
	
		public uint? dwParam;		/*�˳��������*/
	
		[FlagsAttribute]
		public enum DWPARAM : uint
		{
			
				[EnumDescription("�������ر�")]
				LOGOUTPARAM_SHUTDOWN = 1,
			
				[EnumDescription("ʹ�ý���")]
				LOGOUTPARAM_USEEND = 2,
			
				[EnumDescription("��������")]
				LOGOUTPARAM_RESTART = 4,
			
		}

	
	public byte[] FeeCheck;		/*������Ϣ(ֻ��֧�� ע��ʱѡ���շ�ģʽ���豸��Ч)*/
		};

	/*�ͻ���ע������*/
	public struct DEVLOGOUTRES
	{
		private Reserved reserved;
		
	public UNIACCOUNT szAccInfo;		/*ʹ������Ϣ(UNIACCOUNT�ṹ)*/
	
		public uint? dwParam;		/*�˳�����*/
	
		[FlagsAttribute]
		public enum DWPARAM : uint
		{
			
				[EnumDescription("����ʹ����Ϣ")]
				LOGOUTPARAM_HIDEUSEINFO = 0x1,
			
		}

	
		public string szMemo;		/*��ע����չ�ã�*/
		};

	/*�ͻ������������ʱ��������*/
	public struct DEVHANDSHAKEREQ
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*�ͻ����豸��ID��*/
	
		public uint? dwLabID;		/*ʵ���ҵ�ID��*/
	
		public uint? dwAccNo;		/*�ʺ�*/
	
		public string szDevChgInfo;		/*�豸��Ӳ�������Ϣ*/
	
		public string szMemo;		/*��ע����չ�ã�*/
		};

	/*�������Կͻ��˶�ʱ���ֵ���Ӧ*/
	public struct DEVHANDSHAKERES
	{
		private Reserved reserved;
		
		public uint? dwFunc;		/*ʹ�ù���ģ��*/
	
		public uint? dwDevStat;		/*�豸״̬*/
	
		public uint? dwRunStat;		/*����״̬*/
	
		public uint? dwUndoCmd;		/*δ���������*/
	
		public uint? dwLockWaitTime;		/*�ȴ������ͻ���ʱ��*/
	
		public string szMemo;		/*��ע*/
		};

	/*�ͻ������������ʱ��������*/
	public struct CLTCHGPWINFO
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*�ͻ����豸��ID��*/
	
		public uint? dwLabID;		/*ʵ���ҵ�ID��*/
	
		public uint? dwAccNo;		/*�ʺ�*/
	
		public string szOldPw;		/*������*/
	
		public string szNewPw;		/*������*/
	
		public string szMemo;		/*��ע����չ�ã�*/
		};

	/*�����豸����*/
	public struct DEVCTRLINFO
	{
		private Reserved reserved;
		
		public uint? dwLabID;		/*ʵ����ID��*/
	
		public uint? dwDevID;		/*�ͻ����豸��ID��*/
	
		public uint? dwCmd;		/*��������*/
	
		[FlagsAttribute]
		public enum DWCMD : uint
		{
			
				[EnumDescription("������Դ")]
				DEVCMD_POWERON = 1,
			
				[EnumDescription("�رյ�Դ")]
				DEVCMD_POWEROFF = 2,
			
				[EnumDescription("����")]
				DEVCMD_CAPTURESCREEN = 3,
			
				[EnumDescription("Զ�̻���")]
				DEVCMD_WAKEUP = 11,
			
				[EnumDescription("Զ�̹ػ�")]
				DEVCMD_SHUTDOWN = 12,
			
				[EnumDescription("Զ������")]
				DEVCMD_RESTART = 13,
			
				[EnumDescription("Զ�̹ػ�")]
				DEVCMD_SYSSHUTDOWN = 14,
			
				[EnumDescription("����Աǿ�Ƶ�¼")]
				DEVCMD_LOGON = 21,
			
				[EnumDescription("����Աǿ��ע��")]
				DEVCMD_LOGOUT = 22,
			
				[EnumDescription("����Աǿ��ж�ؿͻ���")]
				DEVCMD_UNINSTALL = 23,
			
				[EnumDescription("ϵͳǿ��ע��")]
				DEVCMD_SYSLOGOUT = 24,
			
				[EnumDescription("����")]
				DEVCMD_DISABLE = 31,
			
				[EnumDescription("���")]
				DEVCMD_EANBLE = 32,
			
				[EnumDescription("����")]
				DEVCMD_PCLOCK = 41,
			
				[EnumDescription("����")]
				DEVCMD_PCUNLOCK = 42,
			
				[EnumDescription("��������")]
				DEVCMD_CDLOCK = 43,
			
				[EnumDescription("��������")]
				DEVCMD_CDUNLOCK = 44,
			
				[EnumDescription("U������")]
				DEVCMD_USBLOCK = 45,
			
				[EnumDescription("U�̽���")]
				DEVCMD_USBUNLOCK = 46,
			
				[EnumDescription("��Ҫ�û���¼")]
				DEVCMD_NEEDLOGON = 51,
			
				[EnumDescription("�����û���¼")]
				DEVCMD_NOLOGON = 52,
			
				[EnumDescription("�Ž�����")]
				DEVCMD_DOOROPEN = 61,
			
				[EnumDescription("�Ž�����������ʱ����ڿ��Ʋ�����")]
				DEVCMD_DOORALARM = 62,
			
				[EnumDescription("�Ž�����������Ϣ")]
				DEVCMD_DOORSOUNDMSG = 63,
			
				[EnumDescription("ԤԼ��ʼ szParamΪRESVSTARTINFO")]
				DEVCMD_RESVSTART = 71,
			
				[EnumDescription("ԤԼ���� szParamΪRESVENDINFO")]
				DEVCMD_RESVEND = 72,
			
				[EnumDescription("ϵͳ֪ͨԤԼ������Ϣ")]
				DEVCMD_RESVENDMSG = 81,
			
				[EnumDescription("����Ա��Ϣ")]
				DEVCMD_ADMINMSG = 82,
			
				[EnumDescription("Զ��ǿ���˳����� szParamΪQUITAPPINFO")]
				DEVCMD_QUITAPP = 83,
			
				[EnumDescription("ϵͳ��Ϣ")]
				DEVCMD_SYSTEMMSG = 84,
			
				[EnumDescription("��ʼ��Ļ�㲥,szParamΪ�㲥��IP")]
				DEVCMD_SETTELECAST = 91,
			
				[EnumDescription("������Ļ�㲥")]
				DEVCMD_UNSETTELECAST = 92,
			
				[EnumDescription("��ʼ��ʾ")]
				DEVCMD_SETDEMO = 93,
			
				[EnumDescription("������ʾ")]
				DEVCMD_UNSETDEMO = 94,
			
				[EnumDescription("��ʼ����")]
				DEVCMD_SETCTRL = 95,
			
				[EnumDescription("��������")]
				DEVCMD_UNSETCTRL = 96,
			
		}

	
		public string szParam;		/*���Ʋ���*/
	
		public string szMemo;		/*��ע*/
		};

	/*���Ʒ�������*/
	public struct ROOMCTRLINFO
	{
		private Reserved reserved;
		
		public uint? dwCtrlSN;		/*���������*/
	
		public uint? dwRoomID;		/*����ID��*/
	
		public uint? dwCmd;		/*��������,�ο�DEVCTRLINFO����*/
	
		public string szParam;		/*���Ʋ���*/
	
		public string szMemo;		/*��ע*/
		};

	/*�û��ɽ��뷿������*/
	public struct PERMITROOMREQ
	{
		private Reserved reserved;
		
		public uint? dwAccNo;		/*�ʺ�*/
		};

	/*�û��ɽ��뷿����Ϣ*/
	public struct PERMITROOMINFO
	{
		private Reserved reserved;
		
		public uint? dwRoomKind;		/*��������*/
	
		[FlagsAttribute]
		public enum DWROOMKIND : uint
		{
			
				[EnumDescription("����")]
				ROOMKIND_ROOM = 1,
			
				[EnumDescription("ͨ��")]
				ROOMKIND_CHANNELGATE = 2,
			
		}

	
		public uint? dwPermitMode;		/*����ģʽ*/
	
		[FlagsAttribute]
		public enum DWPERMITMODE : uint
		{
			
				[EnumDescription("����Ա")]
				ROOMPERMIT_MANAGER = 1,
			
				[EnumDescription("ʹ������")]
				ROOMPERMIT_USEDEV = 2,
			
				[EnumDescription("����")]
				ROOMPERMIT_OTHER = 4,
			
		}

	
		public uint? dwRoomID;		/*����ID*/
	
		public string szRoomNo;		/*������*/
	
		public string szRoomName;		/*��������*/
		};

	/*��ȡ���������Ϣ�����*/
	public struct ROOMCTRLREQ
	{
		private Reserved reserved;
		
		public string szRoomNo;		/*�����*/
	
		public uint? dwDCSKind;		/*�Ž�����������*/
		};

	/*�����豸����*/
	public struct CTRLREQ
	{
		private Reserved reserved;
		
		public uint? dwLabID;		/*ʵ����ID��*/
	
		public uint? dwDevID;		/*�ͻ����豸��ID��(Ϊ�ձ�����������ʵ����)*/
	
		public uint? dwCtrl;		/*��ǰ���ģʽ*/
	
		public uint? dwCtrlParam;		/*����趨ֵ�����ݲ�ͬ�ļ��ģʽ���岻һ����*/
	
		public uint? dwEndTime;		/*��ֹʱ��*/
	
		public string szCtrlName;		/*�����*/
		};

	/*����豸��ϸ*/
	public struct LOANDEV
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*�豸ID*/
	
		public uint? dwDevSN;		/*�豸���*/
	
		public uint? dwDevKind;		/*�豸����*/
	
		public string szDevName;		/*�豸��*/
	
		public string szRoomNo;		/*���ڷ���*/
	
		public uint? dwDevClsKind;		/*�豸������*/
		};

	/*��ȡ���г�������*/
	public struct RUNAPPREQ
	{
		private Reserved reserved;
		
		public uint? dwLabID;		/*ʵ����ID*/
	
		public uint? dwDevID;		/*�豸ID*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*���г���*/
	public struct RUNAPP
	{
		private Reserved reserved;
		
		public uint? dwRunNum;		/*��ǰ���������ϼ�*/
	
		public uint? dwProcID;		/*����ID*/
	
		public uint? dwProperty;		/*��������*/
	
		public string szProductName;		/*��Ʒ����*/
	
		public string szExeName;		/*Exe�ļ���*/
	
		public string szSWVersion;		/*����汾*/
	
		public string szDispProductName;		/*��ʾ��������*/
	
		public string szDispSWName;		/*��ʾ��Ʒ����*/
	
		public string szDispSWCompany;		/*��ʾ��˾����*/
	
		public string szInstName;		/*��װ����*/
	
		public string szInstPath;		/*��װ·��*/
	
		public string szIcon;		/*ͼ��*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*�ϴ����������Ϣ����*/
	public struct SWUPLOADEND
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*�ͻ����豸��ID��*/
	
		public uint? dwLabID;		/*ʵ���ҵ�ID��*/
	
		public uint? dwTotalNum;		/*�ܼ�¼��*/
	
		public uint? dwCollectSecond;		/*�ռ���ʱ(����)*/
	
		public uint? dwUploadSecond;		/*�ϴ���ʱ(����)*/
	
		public string szMemo;		/*��ע����չ�ã�*/
		};

	/*�豸�������*/
	public struct DEVLOANREQ
	{
		private Reserved reserved;
		
		public uint? dwLabID;		/*ʵ����ID*/
	
		public uint? dwResvID;		/*ԤԼ��*/
	
		public uint? dwLender;		/*�����*/
	
		public string szLenderName;		/*���������*/
	
		public uint? dwBeginTime;		/*��迪ʼʱ��*/
	
		public uint? dwEndTime;		/*������ʱ��*/
	
	public LOANDEV[] szLoanDevs;		/*����豸��ϸ��(CUniTable<LOANDEV>)*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*�豸�黹����*/
	public struct DEVRETURNREQ
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*ԤԼ��*/
	
		public uint? dwLender;		/*�����*/
	
		public string szLenderName;		/*���������*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public uint? dwDevID;		/*�豸ID*/
	
		public uint? dwCheckStat;		/*�豸״̬*/
	
		public uint? dwCompensation;		/*�⳥���*/
	
		public uint? dwPunishScore;		/*���ÿ۷�*/
	
		public string szDamageInfo;		/*��˵��*/
	
		public string szExtInfo;		/*�豸������*/
		};

	/**/
	public struct DEVDAMAGERECREQ
	{
		private Reserved reserved;
		
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
		public uint? dwSID;		/*SID*/
	
		public uint? dwDevID;		/*�豸ID*/
	
		public string szLabIDs;		/*ʵ����ID,����ö��Ÿ���*/
	
		public string szRoomIDs;		/*����ID,����ö��Ÿ���*/
	
		public string szKindIDs;		/*��������,����ö��Ÿ���*/
	
		public string szClassIDs;		/*�����������ID,����ö��Ÿ���*/
	
		public uint? dwProperty;		/*�豸����*/
	
		public uint? dwClassKind;		/*���(��UNIDEVCLS��Kind����)*/
	
		public uint? dwUnitPrice;		/*���������۸����*/
	
		public uint? dwStatus;		/*ά��״̬*/
	
		public uint? dwManID;		/*������ID*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/**/
	public struct DEVDAMAGEREC
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*SID*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabName;		/*ʵ��������*/
	
		public uint? dwKindID;		/*�豸����*/
	
		public string szKindName;		/*�豸������*/
	
		public uint? dwDevID;		/*�豸ID*/
	
		public uint? dwDevSN;		/*�豸���*/
	
		public string szDevName;		/*�豸����*/
	
		public string szAssertSN;		/*�û����ʲ����*/
	
		public uint? dwUnitPrice;		/*�豸����*/
	
		public uint? dwPurchaseDate;		/*�ɹ�����*/
	
		public uint? dwStatus;		/*��UNIBILL����+���¶���*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("�ȴ�ά��")]
				REPARE_WAIT = 0x10000,
			
				[EnumDescription("��ά�޺�")]
				REPARE_OK = 0x20000,
			
				[EnumDescription("ά��ʧ�ܣ��豸����")]
				REPARE_FAIL = 0x40000,
			
				[EnumDescription("��������")]
				REPARE_CANCEL = 0x80000,
			
		}

	
		public uint? dwDamageDate;		/*������*/
	
		public uint? dwDamageTime;		/*��ʱ��*/
	
		public string szDamageInfo;		/*��˵��*/
	
		public uint? dwRepareDate;		/*ά������*/
	
		public uint? dwRepareTime;		/*ά��ʱ��*/
	
		public string szRepareInfo;		/*ά��˵��*/
	
		public uint? dwRepareCost;		/*ά�޷���*/
	
		public string szFundsNo1;		/*���ѿ����1*/
	
		public uint? dwPay1;		/*���ѿ�1֧��*/
	
		public string szFundsNo2;		/*���ѿ����2*/
	
		public uint? dwPay2;		/*���ѿ�2֧��*/
	
		public string szRepareCom;		/*ά�޵�λ*/
	
		public string szRepareComTel;		/*ά�޵�λ��ϵ��ʽ*/
	
		public uint? dwManID;		/*������ID*/
	
		public string szManName;		/*����������*/
	
		public uint? dwAccNo;		/*�˺�*/
	
		public string szTrueName;		/*��ʵ����*/
	
		public uint? dwCompensation;		/*�⳥���*/
	
		public uint? dwPunishScore;		/*��������*/
	
		public string szMemo;		/*˵��*/
		};

	/**/
	public struct DEVOPENRULEREQ
	{
		private Reserved reserved;
		
		public uint? dwRuleSN;		/*���*/
	
		public string szRuleName;		/*����*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*ÿ�տ���ʱ���*/
	public struct DAYOPENRULE
	{
		private Reserved reserved;
		
		public uint? dwDate;		/*����*/
	
		public uint? dwOpenLimit;		/*�������Ƽ�GROUPOPENRULE����+DEVRESVSTAT����*/
	
		public uint? dwOpenPurpose;		/*������;*/
	
		public uint? dwBegin;		/*��ʼʱ��(HHMM)*/
	
		public uint? dwEnd;		/*����ʱ��(HHMM)*/
	
		public string szFixedTime;		/*ԤԼ�̶�ʱ���(HHMM)������ö��Ÿ���*/
		};

	/*ָ���û��鿪��ʱ���*/
	public struct PERIODOPENRULEREQ
	{
		private Reserved reserved;
		
		public uint? dwRuleSN;		/*���*/
	
		public uint? dwGroupID;		/*��ID*/
	
		public uint? dwStartDay;		/*��ʼ����(��һ������ ��0-6���ڼ�����8���ƶ�������YYYYMMDD)*/
		};

	/*ʱ���ڼ俪�Ź���*/
	public struct PERIODOPENRULE
	{
		private Reserved reserved;
		
		public uint? dwStartDay;		/*��ʼ����(��һ������ ��0-6���ڼ�����8���ƶ�������YYYYMMDD)*/
	
		public uint? dwEndDay;		/*��������(ͬ dwStartDay����)*/
	
	public DAYOPENRULE[] DayOpenRule;		/*����ʱ���(CUniTable[DAYOPENRULE])*/
	
		public string szMemo;		/*����˵��*/
		};

	/*ָ���û��鿪��ʱ���*/
	public struct GROUPOPENRULEREQ
	{
		private Reserved reserved;
		
		public uint? dwRuleSN;		/*���*/
	
		public uint? dwGroupID;		/*��ID*/
		};

	/*ָ���û��鿪��ʱ���*/
	public struct GROUPOPENRULE
	{
		private Reserved reserved;
		
	public UNIGROUP szGroup;		/*����Ϣ(CUniStruct[UNIGROUP])*/
	
		public uint? dwPriority;		/*���ȼ�(���ִ�������ȼ���)*/
	
		public uint? dwOpenLimit;		/*��������*/
	
		[FlagsAttribute]
		public enum DWOPENLIMIT : uint
		{
			
				[EnumDescription("ʹ����ֻ��ѡ����ʱ��Σ���������ѡ��ʱ��")]
				OPENLIMIT_FIXEDTIME = 2,
			
				[EnumDescription("ÿ����࿪��ʱ���")]
				MAX_OPEN_TIMES = 4,
			
				[EnumDescription("һ������")]
				WEEK_DAYS = 7,
			
				[EnumDescription("�ڼ�����PERIODOPENRULE���ʱ���ʾ")]
				HOLIDAY_DAY = 8,
			
		}

	
	public PERIODOPENRULE[] PeriodOpenRule;		/*ʱ���ڼ俪�Ź���(CUniTable[PERIODOPENRULE])*/
		};

	/*ָ���û��鿪��ʱ���*/
	public struct CHANGEGROUPOPENRULE
	{
		private Reserved reserved;
		
		public uint? dwRuleSN;		/*���*/
	
		public uint? dwOldGroupID;		/*ԭ��ID*/
	
		public uint? dwGroupID;		/*��ID*/
	
		public uint? dwPriority;		/*���ȼ�(���ִ�������ȼ���)*/
	
		public uint? dwOpenLimit;		/*��������(GROUPOPENRULE)*/
	
	public PERIODOPENRULE[] PeriodOpenRule;		/*ʱ���ڼ俪�Ź���(CUniTable[PERIODOPENRULE])*/
		};

	/*ʱ���ڼ俪�Ź���*/
	public struct CHANGEPERIODOPENRULE
	{
		private Reserved reserved;
		
		public uint? dwRuleSN;		/*���*/
	
		public uint? dwGroupID;		/*��ID*/
	
		public uint? dwOldStartDay;		/*ԭ��ʼ����(��һ������ ��0-6���ڼ�����8���ƶ�������YYYYMMDD)*/
	
		public uint? dwStartDay;		/*��ʼ����(��һ������ ��0-6���ڼ�����8���ƶ�������YYYYMMDD)*/
	
		public uint? dwEndDay;		/*��������(ͬ dwStartDay����)*/
	
	public DAYOPENRULE[] DayOpenRule;		/*����ʱ���(CUniTable[DAYOPENRULE])*/
	
		public string szMemo;		/*����˵��*/
		};

	/*�豸����ʱ���*/
	public struct DEVOPENRULE
	{
		private Reserved reserved;
		
		public uint? dwRuleSN;		/*���*/
	
		public string szRuleName;		/*����*/
	
	public GROUPOPENRULE[] GroupOpenRule;		/*ָ���û��鿪��ʱ���(CUniTable[GROUPOPENRULE])*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*��ǰ�豸ͳ����Ϣ*/
	public struct CURDEVSTAT
	{
		private Reserved reserved;
		
		public uint? dwChgNum;		/*��Ϣ�����ı��ͳ����Ŀ��*/
	
	public TEACHINGDEVSTAT TeachingDevStat;		/*�豸��Ϣͳ��*/
		};

	/*��ѧ�豸ͳ��*/
	public struct TEACHINGDEVSTAT
	{
		private Reserved reserved;
		
		public uint? dwTotalNum;		/*���豸��*/
	
		public uint? dwUseNum;		/*����ʹ���豸��*/
	
		public uint? dwIdleNum;		/*�����豸��*/
		};

	/*��ȡ��ѧ���豸���ڴ�ͳ��*/
	public struct DEVFORTREQ
	{
		private Reserved reserved;
		
		public uint? dwDate;		/*����*/
	
		public string szRoomIDs;		/*����ID,����ö��Ÿ���*/
		};

	/*�豸�ڴν�ѧʹ����Ϣ*/
	public struct DEVSECINFO
	{
		private Reserved reserved;
		
		public uint? dwSecIndex;		/*�ڴα��(1��ʼ��)*/
	
		public string szSecName;		/*�ڴ�����*/
	
		public uint? dwResvDevs;		/*ԤԼ������*/
	
		public uint? dwUseDevs;		/*ʵ���û���*/
	
		public uint? dwResvUsers;		/*�Ͽ�������*/
	
		public uint? dwRealUsers;		/*ʵ�ʵ�������*/
		};

	/*��ȡ��ѧ���豸*/
	public struct TEACHINGDEVREQ
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*�豸ID*/
	
		public string szDevName;		/*ʵ���豸����*/
	
		public uint? dwDevSN;		/*�豸���*/
	
		public string szLabIDs;		/*ʵ����ID,����ö��Ÿ���*/
	
		public string szRoomIDs;		/*����ID,����ö��Ÿ���*/
	
		public string szResvIDs;		/*ԤԼID,����ö��Ÿ���*/
	
		public string szKindIDs;		/*��������,����ö��Ÿ���*/
	
		public string szClassIDs;		/*�����������ID,����ö��Ÿ���*/
	
		public uint? dwDevStat;		/*�豸״̬*/
	
		public uint? dwRunStat;		/*�豸״̬*/
	
		public uint? dwUsePurpose;		/*��UNIDEVICE����*/
	
		public uint? dwCtrlMode;		/*���Ʒ�ʽ*/
	
		public uint? dwProperty;		/*�豸����*/
	
		public uint? dwClassKind;		/*���(��UNIDEVCLS��Kind����)*/
	
		public uint? dwManGroupID;		/*����Ա��ID*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*��ѧ�豸��Ϣ*/
	public struct TEACHINGDEV
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*�豸ID*/
	
		public uint? dwDevSN;		/*�豸���*/
	
		public string szDevName;		/*ʵ���豸����*/
	
		public uint? dwDevStat;		/*�豸״̬*/
	
		public uint? dwCtrlMode;		/*���Ʒ�ʽ*/
	
		public uint? dwRunStat;		/*�豸״̬*/
	
		public string szKindName;		/*�豸����*/
	
		public string szFuncCode;		/*�豸������;����*/
	
		public string szModel;		/*�豸�ͺ�*/
	
		public string szSpecification;		/*�豸���*/
	
		public uint? dwProperty;		/*�豸���ԣ�ǰ16��ΪUNIDEVKIND����*/
	
		public uint? dwMaxUsers;		/*���ͬʱʹ������*/
	
		public uint? dwMinUsers;		/*����ͬʱʹ������*/
	
		public uint? dwCurUsers;		/*��ǰʹ������*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabSN;		/*ʵ���ұ��*/
	
		public string szLabName;		/*ʵ��������*/
	
		public uint? dwRoomID;		/*����ID*/
	
		public string szRoomNo;		/*����ţ����ظ�,���ڹ����Ž�����ʽB-F-N,�������-¥���-�����ţ�*/
	
		public string szRoomName;		/*��������*/
	
		public uint? dwUsePurpose;		/*ͬUniFee��dwPurpose*/
	
		public uint? dwCurAccNo;		/*��ǰ�û��˺�*/
	
		public string szCurTrueName;		/*��ǰ�û�����*/
	
		public uint? dwResvID;		/*ԤԼID*/
	
		public uint? dwTestItemID;		/*ʵ����ĿID*/
	
		public string szTestName;		/*ʵ������*/
	
		public uint? dwTestPlanID;		/*ʵ��ƻ�ID*/
	
		public string szTestPlanName;		/*ʵ��ƻ�����*/
	
		public uint? dwTeacherID;		/*��ʦ���ʺţ�*/
	
		public string szTeacherName;		/*��ʦ����*/
	
		public uint? dwCourseID;		/*�γ�ID*/
	
		public string szCourseName;		/*�γ�����*/
	
		public uint? dwGroupID;		/*�Ͽΰ༶*/
	
		public string szGroupName;		/*�Ͽΰ༶����*/
	
		public uint? dwTeachingTime;		/*��ѧʱ��(WWDSSEE)�ڼ��ܣ�WW�����ڼ�(D),��ʼ�ڴΣ�SS�������ڴΣ�EE)*/
		};

	/*��ȡ�񽱼�¼*/
	public struct REWARDRECREQ
	{
		private Reserved reserved;
		
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
		public uint? dwRewardID;		/*��ID*/
	
		public uint? dwRTID;		/*����ʵ����ĿID*/
	
		public uint? dwHolderID;		/*�����ˣ��ʺţ�*/
	
		public uint? dwLeaderID;		/*������ID*/
	
		public uint? dwOpID;		/*¼��ԱID*/
	
		public uint? dwRewardType;		/*�񽱷���*/
	
		public uint? dwRewardKind;		/*������*/
	
		public uint? dwRewardLevel;		/*�񽱼���*/
	
		public uint? dwDevID;		/*�豸ID*/
	
		public string szLabIDs;		/*ʵ����ID,����ö��Ÿ���*/
	
		public string szRoomIDs;		/*����ID,����ö��Ÿ���*/
	
		public string szKindIDs;		/*��������,����ö��Ÿ���*/
	
		public string szClassIDs;		/*�����������ID,����ö��Ÿ���*/
	
		public uint? dwProperty;		/*�豸����*/
	
		public uint? dwClassKind;		/*���(��UNIDEVCLS��Kind����)*/
	
		public uint? dwUnitPrice;		/*���������۸����*/
	
		public uint? dwReqProp;		/*���󸽼�����*/
	
		[FlagsAttribute]
		public enum DWREQPROP : uint
		{
			
				[EnumDescription("��Ҫ�豸��Ϣ")]
				RRREQ_NEEDDEV = 0x1,
			
				[EnumDescription("��¼��ʱ���ѯ")]
				RRREQ_BYOPDATE = 0x2,
			
		}

	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*��ʹ���豸*/
	public struct REWARDUSEDEV
	{
		private Reserved reserved;
		
		public uint? dwRewardID;		/*��ID*/
	
		public uint? dwDevID;		/*�豸ID*/
	
		public uint? dwKindID;		/*�豸����*/
	
		public uint? dwDevSN;		/*�豸���*/
	
		public string szDevName;		/*�豸����*/
	
		public uint? dwRoomID;		/*����ID*/
	
		public string szRoomName;		/*��������*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabName;		/*ʵ��������*/
	
		public string szAttendantName;		/*ֵ��Ա����*/
	
		public uint? dwStatus;		/*�豸����Աȷ��״̬*/
	
		public uint? dwTestTimes;		/*ʵ�����*/
	
		public uint? dwTestHour;		/*ʵ��ѧʱ��*/
	
		public uint? dwRelyRate;		/*������(�ٷֱ�)����չ*/
	
		public string szMemo;		/*��ע*/
		};

	/*�񽱼�¼*/
	public struct REWARDREC
	{
		private Reserved reserved;
		
		public uint? dwRewardID;		/*��ID*/
	
		public uint? dwRTID;		/*����ʵ����ĿID*/
	
		public string szRTName;		/*����ʵ������*/
	
		public uint? dwHolderID;		/*�����ˣ��ʺţ�*/
	
		public string szHolderName;		/*����������*/
	
		public uint? dwLeaderID;		/*������ID*/
	
		public string szLeaderName;		/*����������*/
	
		public string szMemberNames;		/*����Ա���������Ÿ�����*/
	
		public uint? dwRewardDate;		/*������*/
	
		public uint? dwOpDate;		/*¼������*/
	
		public uint? dwOpID;		/*¼��ԱID*/
	
		public string szOpName;		/*¼��Ա����*/
	
		public uint? dwRewardType;		/*�񽱷���*/
	
		[FlagsAttribute]
		public enum DWREWARDTYPE : uint
		{
			
				[EnumDescription("��ѧ")]
				RETYPE_TEACHING = 1,
			
				[EnumDescription("����")]
				RETYPE_RESEARCH = 2,
			
				[EnumDescription("����")]
				RETYPE_OTHER = 4,
			
		}

	
		public uint? dwRewardKind;		/*������*/
	
		[FlagsAttribute]
		public enum DWREWARDKIND : uint
		{
			
				[EnumDescription("�Ƽ���")]
				REKIND_PRIZE = 1,
			
				[EnumDescription("ר��")]
				REKIND_PATENT = 2,
			
				[EnumDescription("���ļ���")]
				REKIND_THESISINDEX = 4,
			
				[EnumDescription("���ķ���")]
				REKIND_THESISISSUE = 8,
			
				[EnumDescription("�̲�")]
				REKIND_TEXTBOOK = 0x10,
			
		}

	
		public string szRewardName;		/*��������*/
	
		public uint? dwRewardLevel;		/*�񽱼���*/
	
		[FlagsAttribute]
		public enum DWREWARDLEVEL : uint
		{
			
				[EnumDescription("���Ҽ�")]
				RELEVEL_NATIONAL = 1,
			
				[EnumDescription("ʡ����")]
				RELEVEL_PROVINCE = 2,
			
				[EnumDescription("�������")]
				RELEVEL_THREEINDEX = 4,
			
				[EnumDescription("���Ŀ���")]
				RELEVEL_KERNELJOURNAL = 8,
			
				[EnumDescription("ר��")]
				RELEVEL_PATENT = 0x10,
			
		}

	
		public string szAuthOrg;		/*��֤����*/
	
		public string szCertID;		/*֤����*/
	
		public string szExtInfo;		/*��չ��Ϣ*/
	
	public REWARDUSEDEV[] UseDev;		/*CUniTable[REWARDUSEDEV]*/
	
		public string szMemo;		/*��ע*/
		};

	/*��ȡ���ü�¼*/
	public struct COSTRECREQ
	{
		private Reserved reserved;
		
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
		public uint? dwCostType;		/*��������*/
	
		public string szLabIDs;		/*ʵ����ID,����ö��Ÿ���*/
	
		public string szRoomIDs;		/*����ID,����ö��Ÿ���*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*���ü�¼*/
	public struct COSTREC
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*��ˮ��*/
	
		public uint? dwCostType;		/*��������*/
	
		[FlagsAttribute]
		public enum DWCOSTTYPE : uint
		{
			
				[EnumDescription("���÷�")]
				COSTTYPE_BUY = 0x1000,
			
				[EnumDescription("ά����")]
				COSTTYPE_KEEP = 0x2000,
			
				[EnumDescription("���з�")]
				COSTTYPE_RUN = 0x4000,
			
				[EnumDescription("�Ĳķ�")]
				COSTTYPE_CONSUME = 0x4001,
			
				[EnumDescription("�����")]
				COSTTYPE_BUILD = 0x8000,
			
				[EnumDescription("�о���ĸﾭ��")]
				COSTTYPE_RANDR = 0x10000,
			
				[EnumDescription("����")]
				COSTTYPE_OTHER = 0x20000,
			
		}

	
		public uint? dwPurpose;		/*��;*/
	
		[FlagsAttribute]
		public enum DWPURPOSE : uint
		{
			
				[EnumDescription("��ѧ")]
				COSTFOR_TEACHING = 0x1,
			
				[EnumDescription("����")]
				COSTFOR_OTHER = 0x2,
			
		}

	
		public uint? dwRoomID;		/*����ID*/
	
		public string szRoomName;		/*��������*/
	
		public uint? dwSubID;		/*�����豸ID����չ��*/
	
		public uint? dwCost;		/*���ã�Ԫ��*/
	
		public string szExtInfo;		/*����������Ϣ*/
	
		public uint? dwCostDate;		/*��������*/
	
		public uint? dwOpTime;		/*¼��ʱ��*/
	
		public uint? dwOpID;		/*¼�����ԱID*/
	
		public string szOpName;		/*¼�����Ա����*/
	
		public string szMemo;		/*��ע*/
		};

	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/

	/*��ȡ�Ž��������������*/
	public struct DCSREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*��ʽ*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("ȫ��")]
				DCSGET_BYALL = 0,
			
				[EnumDescription("���������")]
				DCSGET_BYSN = 1,
			
				[EnumDescription("����վ��")]
				DCSGET_BYSTASN = 4,
			
		}

	
		public string szGetKey;		/*����ֵ*/
	
		public uint? dwDCSKind;		/*�Ž�����������*/
		};

	/**/
	public struct UNIDCS
	{
		private Reserved reserved;
		
		public uint? dwSN;		/*�Ž����������*/
	
		public uint? dwDCSKind;		/*�Ž�����������*/
	
		[FlagsAttribute]
		public enum DWDCSKIND : uint
		{
			
				[EnumDescription("�Ž����")]
				DCSKIND_DOORCTRL = 1,
			
				[EnumDescription("��Ƶ���")]
				DCSKIND_VIDEOCTRL = 2,
			
		}

	
		public string szName;		/*�Ž�����������*/
	
		public string szRoomNo;		/*�Ž����ڷ����*/
	
		public string szIP;		/*�Ž�������IP��ַ*/
	
		public uint? dwStatus;		/*�Ž�������״̬*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("δ����")]
				DCSSTAT_CLOSED = 0,
			
				[EnumDescription("������")]
				DCSSTAT_RUNNING = 1,
			
				[EnumDescription("����")]
				DCSSTAT_TROUBLE = 2,
			
				[EnumDescription("�ſ�")]
				DCSSTAT_DOOROPEN = 0x100,
			
				[EnumDescription("�Ź�")]
				DCSSTAT_DOORCLOSED = 0x200,
			
				[EnumDescription("��Դδ��ͨ")]
				DCSSTAT_POWEROFF = 0x2000,
			
				[EnumDescription("��Դ�ѽ�ͨ")]
				DCSSTAT_POWERON = 0x4000,
			
				[EnumDescription("��Դ������")]
				DCSSTAT_POWERWORKING = 0x8000,
			
				[EnumDescription("����")]
				DCSSTAT_DISABLED = 0x10000,
			
		}

	
		public uint? dwStaSN;		/*����վ����*/
	
		public string szStaName;		/*վ������*/
	
		public uint? dwStatChgTime;		/*״̬�ı�ʱ��*/
	
		public string szStatInfo;		/*״̬����*/
	
		public string szMemo;		/*��ע*/
		};

	/*�Ž���Ϣ��*/
	public struct DOORCTRLINFO
	{
		private Reserved reserved;
		
		public uint? dwCtrlSN;		/*���������*/
	
		public string szCtrlModel;		/*���������ͣ�����汾�ȣ�*/
	
		public uint? dwCtrlKind;		/*���������ͼ�UNIDOORCTRL����*/
	
		public string szCtrlIP;		/*������IP��ַ*/
		};

	/*��������¼����*/
	public struct DCSLOGINREQ
	{
		private Reserved reserved;
		
		public string szVersion;		/*�汾	XX.XX.XXXXXXXX*/
	
		public string szIP;		/*IP��ַ*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*��������¼��Ӧ*/
	public struct DCSLOGINRES
	{
		private Reserved reserved;
		
		public uint? dwSessionID;		/*�����������SessionID*/
	
	public UNIVERSION SrvVer;		/*UNIVERSION �ṹ*/
	
		public string szCurTime;		/*������ʱ�� YYYY-MM-DD HH:MM:SS*/
	
		public uint? dwDCSSN;		/*����̨���*/
	
		public string szDCSName;		/*����̨����*/
	
		public string szMemo;		/*˵����Ϣ*/
	
	public DOORCTRLINFO[] szManCtrls;		/*�����Ž��б�CUniTable[DOORCTRLINFO]*/
		};

	/*�������˳�����*/
	public struct DCSLOGOUTREQ
	{
		private Reserved reserved;
		
		public uint? dwDCSSN;		/*���������*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*������״̬*/
	public struct DOORCTRLSTAT
	{
		private Reserved reserved;
		
		public uint? dwCtrlSN;		/*���������*/
	
		public uint? dwStatus;		/*DCSSTAT_TROUBLE,DCSSTAT_DOOROPEN,DCSSTAT_DOORCLOSED*/
	
		public string szMemo;		/*��ע*/
		};

	/*��������ʱͨ������*/
	public struct DCSPULSEREQ
	{
		private Reserved reserved;
		
		public uint? dwDCSSN;		/*����̨���*/
	
	public DOORCTRLSTAT[] szControllerStat;		/*������״̬��ϢCUniTable[DOORCTRLSTAT]*/
		};

	/*��������ʱͨ��Ӧ��*/
	public struct DCSPULSERES
	{
		private Reserved reserved;
		
		public uint? dwChanged;		/*�������Ƿ�ı�*/
	
		public string szMemo;		/*��ע*/
		};

	/*�Ž�ˢ������*/
	public struct DOORCARDREQ
	{
		private Reserved reserved;
		
		public uint? dwDCSSN;		/*���������*/
	
		public uint? dwCtrlSN;		/*���������*/
	
		public uint? dwCardMode;		/*����ˢ�����������(3�ֽڣ�4�ֽڵ�)*/
	
		[FlagsAttribute]
		public enum DWCARDMODE : uint
		{
			
				[EnumDescription("����ˢ��,�������")]
				DOORCARD_IN = 1,
			
				[EnumDescription("����ˢ��,�����ȥ")]
				DOORCARD_OUT = 2,
			
				[EnumDescription("��ť")]
				DOORCARD_BUTTON = 0x1000,
			
				[EnumDescription("�ֻ�����")]
				MOBILE_OPENDOOR = 0x10000,
			
		}

	
		public string szCardNo;		/*����*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*�Ž�ˢ����Ӧ*/
	public struct DOORCARDRES
	{
		private Reserved reserved;
		
		public uint? dwUserKind;		/*�û�����*/
	
		[FlagsAttribute]
		public enum DWUSERKIND : uint
		{
			
				[EnumDescription("����Ա")]
				CARDUSER_MANAGER = 1,
			
				[EnumDescription("ָ�����û����Ա")]
				CARDUSER_GROUP = 2,
			
				[EnumDescription("ʹ���豸")]
				CARDUSER_USEDEV = 4,
			
				[EnumDescription("����ͨ��")]
				CARDUSER_PASSCHANNEL = 8,
			
				[EnumDescription("���Ź������Ա(������Ա)")]
				CARDUSER_OPENGROUP = 0x10,
			
				[EnumDescription("�������Ա")]
				CARDUSER_ATTENDGROUP = 0x20,
			
				[EnumDescription("������")]
				CARDUSER_PERMIT = 0x100,
			
				[EnumDescription("��ֹ����")]
				CARDUSER_FORBID = 0x200,
			
		}

	
		public uint? dwSoundSN;		/*��������*/
	
		public uint? dwDeadLine;		/*�����ֹʱ��*/
	
		public string szPID;		/*ѧ����*/
	
		public string szCardNo;		/*����*/
	
		public string szTrueName;		/*����*/
	
		public string szDispInfo;		/*��ʾ��Ϣ*/
		};

	/*�ֻ���������*/
	public struct MOBILEOPENDOORREQ
	{
		private Reserved reserved;
		
		public string szMSN;		/*MSN*/
	
		public string szLogonName;		/*��¼��*/
	
		public string szPassword;		/*����*/
	
		public string szIP;		/*IP��ַ*/
	
		public uint? dwProperty;		/*��չ����*/
	
		[FlagsAttribute]
		public enum DWPROPERTY : uint
		{
			
				[EnumDescription("��֤�ɹ���΢�ź�")]
				MODPROP_BINDMSN = 1,
			
		}

	
		public uint? dwDCSSN;		/*���������*/
	
		public uint? dwCtrlSN;		/*���������*/
	
		public uint? dwCardMode;		/*����ˢ��*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*�ֻ�������Ӧ*/
	public struct MOBILEOPENDOORRES
	{
		private Reserved reserved;
		
		public uint? dwUserKind;		/*�û�����(DOORCARDRES����)*/
	
		public uint? dwFailedType;		/*ʧ������*/
	
		[FlagsAttribute]
		public enum DWFAILEDTYPE : uint
		{
			
				[EnumDescription("΢�ź�δ���û�")]
				MODFAILED_NOBIND = 0x1,
			
				[EnumDescription("�û���֤ʧ��")]
				MODFAILED_CHECKWRONG = 0x2,
			
				[EnumDescription("δ��Ȩ�û�")]
				MODFAILED_UNAUTH = 0x4,
			
				[EnumDescription("����IP��ƥ��")]
				MODFAILED_IPUNMAP = 0x8,
			
		}

	
		public string szPID;		/*ѧ����*/
	
		public string szTrueName;		/*����*/
	
		public string szDispInfo;		/*��ʾ��Ϣ*/
		};

	/*��ȡ�Ž��������������*/
	public struct DOORCTRLREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*��ʽ*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("ȫ��")]
				DOORCTRLGET_BYALL = 0,
			
				[EnumDescription("���������")]
				DOORCTRLGET_BYDCSSN = 1,
			
				[EnumDescription("�Ž����������")]
				DOORCTRLGET_BYSN = 2,
			
				[EnumDescription("������")]
				DOORCTRLGET_BYROOMNO = 3,
			
				[EnumDescription("����վ��")]
				DOORCTRLGET_BYSTASN = 4,
			
		}

	
		public string szGetKey;		/*����ֵ*/
	
		public uint? dwDCSKind;		/*�Ž�����������*/
		};

	/**/
	public struct UNIDOORCTRL
	{
		private Reserved reserved;
		
		public uint? dwDCSSN;		/*���������*/
	
		public uint? dwDCSKind;		/*�Ž�����������*/
	
		public string szDCSName;		/*�Ž�����������*/
	
		public string szDCSIP;		/*�Ž�������IP��ַ*/
	
		public uint? dwCtrlSN;		/*���������*/
	
		public uint? dwCtrlKind;		/*����������*/
	
		[FlagsAttribute]
		public enum DWCTRLKIND : uint
		{
			
				[EnumDescription("�����Ž�")]
				DCKIND_ROOM = 0x1,
			
				[EnumDescription("ͨ�������Ž�")]
				DCKIND_CHANNELGATE = 0x2,
			
				[EnumDescription("�������")]
				DCKIND_NETCTRL = 0x4,
			
				[EnumDescription("��Դ������")]
				DCKIND_POWERCTRL = 0x10,
			
				[EnumDescription("˫���Ž�")]
				DCKIND_DOUBLE = 0x100,
			
				[EnumDescription("����ˢ������֤")]
				DCKIND_OUTCHECK = 0x200,
			
				[EnumDescription("�п���̨����")]
				DCKIND_ASCONSOLE = 0x400,
			
				[EnumDescription("�ж�������ʾ��")]
				DCKIND_WITHDISPLAY = 0x800,
			
				[EnumDescription("�ſ���״̬�ͱ�׼�෴")]
				DCKIND_DOOROPENREVERSE = 0x1000,
			
				[EnumDescription("�п��ڹ���")]
				DCKIND_WITHATTENDANCE = 0x2000,
			
		}

	
		public string szRoomNo;		/*�����*/
	
		public string szCtrlName;		/*����������*/
	
		public string szCtrlModel;		/*�������ͺţ�����汾�ȣ�*/
	
		public string szCtrlIP;		/*������IP��ַ*/
	
		public uint? dwStaSN;		/*����վ����*/
	
		public string szStaName;		/*վ������*/
	
		public uint? dwStatus;		/*�Ž�״̬�� UNIDCS����*/
	
		public uint? dwStatChgTime;		/*״̬�ı�ʱ��*/
	
		public string szStatInfo;		/*״̬����*/
	
		public string szMODIP;		/*�����ֻ�����IP��*/
	
		public string szMemo;		/*��ע*/
	
		public uint? dwRoomID;		/*����ID*/
	
		public string szRoomName;		/*��������*/
	
		public string szFloorNo;		/*����¥��*/
	
		public uint? dwBuildingID;		/*¥��ID*/
	
		public string szBuildingNo;		/*��¥���(*/
	
		public string szBuildingName;		/*��¥����(*/
		};

	/*���Ž�������������*/
	public struct DOORCTRLCMD
	{
		private Reserved reserved;
		
		public uint? dwCtrlSN;		/*���������*/
	
		public uint? dwCmd;		/*��������(�ο�DEVCTRLINFO::dwCmd)*/
	
		public string szParam;		/*���Ʋ���*/
	
		public string szMemo;		/*��ע*/
		};

	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/

	/*��ȡ����Ϣ�������*/
	public struct GROUPREQ
	{
		private Reserved reserved;
		
		public uint? dwGroupID;		/*��ID*/
	
		public string szName;		/*����*/
	
		public uint? dwKind;		/*����*/
	
		public uint? dwAccNo;		/*���Ա�ʺ�*/
	
		public uint? dwMinDeadLine;		/*��С��ֹ����*/
	
		public uint? dwMaxDeadLine;		/*����ֹ����*/
	
		public uint? dwReqProp;		/*���󸽼�����*/
	
		[FlagsAttribute]
		public enum DWREQPROP : uint
		{
			
				[EnumDescription("��Ҫ��ɾ����")]
				GROUPREQ_NEEDDEL = 0x1,
			
		}

	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*���Ա*/
	public struct GROUPMEMBER
	{
		private Reserved reserved;
		
		public uint? dwGroupID;		/*����*/
	
		public uint? dwKind;		/*��Ա���*/
	
		[FlagsAttribute]
		public enum DWKIND : uint
		{
			
				[EnumDescription("�༶")]
				MEMBERKIND_CLASS = 1,
			
				[EnumDescription("����")]
				MEMBERKIND_PERSONAL = 2,
			
				[EnumDescription("����")]
				MEMBERKIND_DEPT = 4,
			
				[EnumDescription("���")]
				MEMBERKIND_IDENT = 8,
			
				[EnumDescription("��")]
				MEMBERKIND_SUBGROUP = 16,
			
		}

	
		public uint? dwMemberID;		/*��ԱID*/
	
		public string szName;		/*��Ա����*/
	
		public uint? dwBeginDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��ֹ����*/
	
		public uint? dwStatus;		/*״̬��ǰ8����������CHECKINFO����Ĺ���Ա���״̬)*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("δ��Ч")]
				GROUPMEMBERSTAT_UNFORCE = 0x100,
			
				[EnumDescription("��Ч��")]
				GROUPMEMBERSTAT_FORCE = 0x200,
			
				[EnumDescription("�ѹ���")]
				GROUPMEMBERSTAT_EXPIRED = 0x400,
			
				[EnumDescription("��ѡ��")]
				GROUPMEMBERSTAT_SEAT = 0x1000,
			
		}

	
		public string szExtInfo;		/*����˵��*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/**/
	public struct UNIGROUP
	{
		private Reserved reserved;
		
		public uint? dwGroupID;		/*��ID*/
	
		public string szName;		/*����*/
	
		public uint? dwKind;		/*����*/
	
		[FlagsAttribute]
		public enum DWKIND : uint
		{
			
				[EnumDescription("ԤԼ��")]
				GROUPKIND_RERV = 0x100,
			
				[EnumDescription("�豸ָ��ʹ����")]
				GROUPKIND_DEV = 0x200,
			
				[EnumDescription("����Ա��")]
				GROUPKIND_MAN = 0x400,
			
				[EnumDescription("�����û���")]
				GROUPKIND_USER = 0x800,
			
				[EnumDescription("��ļ��")]
				GROUPKIND_RECRUIT = 0x1000,
			
				[EnumDescription("���Ź�����")]
				GROUPKIND_OPENRULE = 0x2000,
			
				[EnumDescription("������")]
				GROUPKIND_ATTEND = 0x4000,
			
				[EnumDescription("�������С��")]
				GROUPKIND_SUBGROUP = 0x40000000,
			
				[EnumDescription("��Ա�����Ա���")]
				GROUPKIND_MEMBERNEEDCHECK = 0x80000000,
			
		}

	
		public uint? dwMaxUsers;		/*����û���*/
	
		public uint? dwMinUsers;		/*�����û���*/
	
		public uint? dwDeadLine;		/*��ֹ����*/
	
		public uint? dwEnrollDeadline;		/*��������ֹ��*/
	
		public uint? dwAssociateID;		/*��չ�ã�����ID������ԤԼ���ԤԼID��*/
	
		public string szGroupURL;		/*����*/
	
	public GROUPMEMBER[] szMembers;		/*��Ա��ϸ��(CUniTable<GROUPMEMBER>)*/
	
		public string szMemo;		/*��ע*/
		};

	/*��ȡ���Ա��ϸ�������*/
	public struct GROUPMEMDETAILREQ
	{
		private Reserved reserved;
		
		public uint? dwGroupKind;		/*�����*/
	
		public uint? dwGroupID;		/*����*/
	
		public uint? dwStatus;		/*���״̬*/
	
		public uint? dwAccNo;		/*��Ա�˺�*/
	
		public uint? dwReqProp;		/*���󸽼�����*/
	
		[FlagsAttribute]
		public enum DWREQPROP : uint
		{
			
				[EnumDescription("��Ҫ��ɾ����")]
				GROUPMEMDETAILREQ_NEEDDEL = 0x1,
			
		}

	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*���Ա��ϸ*/
	public struct GROUPMEMDETAIL
	{
		private Reserved reserved;
		
		public uint? dwGroupID;		/*����*/
	
		public uint? dwBeginDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��ֹ����*/
	
		public uint? dwStatus;		/*���״̬*/
	
		public string szExtInfo;		/*����˵��*/
	
		public uint? dwAccNo;		/*��Ա�˺�*/
	
		public string szPID;		/*ѧ����*/
	
		public string szTrueName;		/*����*/
	
		public uint? dwClassID;		/*�༶ID*/
	
		public string szClassName;		/*�༶*/
	
		public uint? dwDeptID;		/*����ID*/
	
		public string szDeptName;		/*����*/
	
		public uint? dwMajorID;		/*רҵID*/
	
		public string szMajorName;		/*רҵ*/
	
		public uint? dwSex;		/*�Ա��UniCommon.h*/
	
		public uint? dwIdent;		/*��� ��UniCommon.h*/
	
		public uint? dwEnrolYear;		/*��ѧ���(XX��)*/
	
		public uint? dwSchoolYears;		/*ѧ��*/
	
		public string szTel;		/*�绰*/
	
		public string szHandPhone;		/*�ֻ�*/
	
		public string szEmail;		/*����*/
	
		public uint? dwTutorID;		/*��ʦ�˺�*/
	
		public string szTutorName;		/*��ʦ����*/
		};

	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/

	/*��ȡԤԼ��Ϣ�������*/
	public struct RESVREQ
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*ԤԼ��*/
	
		public uint? dwMemberKind;		/*��Ա���*/
	
		public uint? dwOwner;		/*ԤԼ��(������)*/
	
		public uint? dwMemberID;		/*��ԱID*/
	
		public uint? dwDevID;		/*�豸ID*/
	
		public uint? dwManagerID;		/*����ԱID*/
	
		public uint? dwAccNo;		/*��Ա�˺ţ���ȡ��Ա��ص�����ʵ�鰲��*/
	
		public uint? dwTestPlanID;		/*��ȡTestPlan��ص�����ʵ�鰲��*/
	
		public uint? dwCourseID;		/*��ȡ�γ���ص�����ʵ�鰲��*/
	
		public uint? dwTestItemID;		/*��ȡTestItem��ص�����ʵ�鰲��*/
	
		public uint? dwCheckStat;		/*ȷ��״̬*/
	
		public uint? dwUnNeedStat;		/*������״̬*/
	
		public uint? dwUseMode;		/*ʹ�÷���*/
	
		public uint? dwPurpose;		/*ԤԼ��;*/
	
		public uint? dwDevKind;		/*�豸����*/
	
		public uint? dwClassKind;		/*���(��UNIDEVCLS��Kind����)*/
	
		public uint? dwBeginDate;		/*ԤԼ��ʼ����*/
	
		public uint? dwEndDate;		/*ԤԼ��������*/
	
		public string szRoomNos;		/*������,����ö��Ÿ���*/
	
		public uint? dwResvGroupID;		/*ԤԼ��ID*/
	
		public uint? dwStatFlag;		/*״̬��־*/
	
		[FlagsAttribute]
		public enum DWSTATFLAG : uint
		{
			
				[EnumDescription("��Ч״̬")]
				STATFLAG_INUSE = 0x1,
			
				[EnumDescription("����״̬")]
				STATFLAG_OVER = 0x2,
			
				[EnumDescription("ɾ��״̬")]
				STATFLAG_DEL = 0x4,
			
				[EnumDescription("���ʧ��")]
				STATFLAG_CHECKFAIL = 0x8,
			
		}

	
		public uint? dwKind;		/*ԤԼ����*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*ԤԼ��*/
	public struct UNIRESERVE
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*ԤԼ��*/
	
		public uint? dwMemberKind;		/*��Ա���*/
	
		[FlagsAttribute]
		public enum DWMEMBERKIND : uint
		{
			
				[EnumDescription("��ԱΪ�û���")]
				MEMBERKIND_GROUP = 0x1,
			
				[EnumDescription("��ԱΪԤԼ���ʺ�")]
				MEMBERKIND_PERSONNAL = 0x2,
			
		}

	
		public uint? dwUseMode;		/*ʹ�÷���*/
	
		[FlagsAttribute]
		public enum DWUSEMODE : uint
		{
			
				[EnumDescription("���ʽԤԼ")]
				RESVUSE_LEASE = 0x100,
			
				[EnumDescription("ʹ���豸ԤԼ")]
				RESVUSE_USEDEV = 0x200,
			
				[EnumDescription("����ռ���豸ԤԼ")]
				RESVUSE_LONGTERM = 0x400,
			
				[EnumDescription("����ԤԼ")]
				RESVUSEEXT_PC = 0x1000,
			
				[EnumDescription("�ֻ�ԤԼ")]
				RESVUSEEXT_HP = 0x2000,
			
				[EnumDescription("����̨ԤԼ")]
				RESVUSEEXT_CONSOLE = 0x4000,
			
				[EnumDescription("�����ϻ�����RESVUSE_USEDEVһ��ʹ�ã�")]
				RESVUSEEXT_USEPC = 0x10000,
			
				[EnumDescription("ֱ�ӵ�¼ԤԼ")]
				RESVUSE_DIRECTPC = 0x20000,
			
		}

	
		public uint? dwPurpose;		/*ԤԼ��;*/
	
		[FlagsAttribute]
		public enum DWPURPOSE : uint
		{
			
				[EnumDescription("��ѧԤԼ")]
				USEFOR_TEACHING = 0x1,
			
				[EnumDescription("����")]
				USEFOR_PERSONNAL = 0x2,
			
				[EnumDescription("���Ż")]
				USEFOR_ACTIVITY = 0x4,
			
				[EnumDescription("��ѧ�о�")]
				USEFOR_RESEACH = 0x8,
			
				[EnumDescription("��������")]
				USEFOR_YARD = 0x10,
			
				[EnumDescription("����ԱԤ��")]
				USEFOR_RESERVED = 0x20,
			
				[EnumDescription("ԤԼʹ����λ")]
				USEFOR_SEAT = 0x40,
			
				[EnumDescription("ԤԼʹ�õ���")]
				USEFOR_PC = 0x80,
			
				[EnumDescription("���")]
				USEFOR_LOAN = 0x100,
			
				[EnumDescription("�շ�ģʽ")]
				USEFOR_FEEMODE = 0x200,
			
				[EnumDescription("ԤԼʹ�����޼�")]
				USEFOR_STUDYROOM = 0x400,
			
				[EnumDescription("����ά��")]
				USEFOR_SERVICING = 0x1000,
			
				[EnumDescription("����(���¼)")]
				USEFOR_ANONYMOUS = 0x2000,
			
				[EnumDescription("ȫ��ѧ��")]
				USEFOR_ALLUSER = 0x4000,
			
				[EnumDescription("Ժ��")]
				USEBY_DEPT = 0x10000,
			
				[EnumDescription("У��")]
				USEBY_INNER = 0x20000,
			
				[EnumDescription("У��(������)")]
				USEBY_OUTSIDE = 0x40000,
			
				[EnumDescription("���ۿ��е�ʵ��")]
				USEFOR_WITHTHEORY = 0x100000,
			
				[EnumDescription("���������ʵ���")]
				USEFOR_NOTHEORY = 0x200000,
			
		}

	
		public uint? dwKind;		/*ԤԼ����(����ͳ����)*/
	
		public uint? dwOwner;		/*ԤԼ��(������)*/
	
		public string szOwnerName;		/*ԤԼ������*/
	
		public uint? dwMemberID;		/*��ԱID*/
	
		public string szMemberName;		/*��Ա����*/
	
		public uint? dwResvRuleSN;		/*����ԤԼ����*/
	
		public uint? dwFeeSN;		/*�������շѱ�׼*/
	
		public uint? dwOpenRuleSN;		/*��������ʱ���*/
	
		public uint? dwFeeMode;		/*�շѷ�ʽ*/
	
		public uint? dwUseFee;		/*ԤԼ�ܷ���*/
	
		public uint? dwPreDate;		/*ԤԼ��ʼ����*/
	
		public uint? dwOccurTime;		/*ԤԼ����ʱ��*/
	
		public uint? dwBeginTime;		/*ԤԼ��ʼʱ��*/
	
		public uint? dwEndTime;		/*ԤԼ����ʱ��*/
	
		public uint? dwYearTerm;		/*ѧ�ڱ��*/
	
		public uint? dwTeachingTime;		/*��ѧʱ��(WWDSSEE)�ڼ��ܣ�WW�����ڼ�(D),��ʼ�ڴΣ�SS�������ڴΣ�EE)*/
	
		public uint? dwCheckTime;		/*���ʱ��*/
	
		public uint? dwAdvanceCheckTime;		/*��ǰ���ʱ��*/
	
		public uint? dwResvGroupID;		/*��ID(��ʱ��ԤԼ��ID��ͬ������ΪԤԼID)*/
	
		public uint? dwCheckKinds;		/*�������(�ο�CHECKTYPE���壬�ɶ����*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabName;		/*ʵ��������*/
	
	public RESVDEV[] ResvDev;		/*ԤԼ�豸��ϸ��(CUniTable<RESVDEV>)*/
	
		public uint? dwStatus;		/*ԤԼ״̬(������飬�Ƿ���Ч���Ƿ���ȡ����)*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("����ԤԼ������ʽԤԼ,5������δȷ�Ͻ��Զ�ɾ��")]
				RESVSTAT_INFORMAL = 0x40,
			
				[EnumDescription("��ʽԤԼ")]
				RESVSTAT_FORMAL = 0x80,
			
				[EnumDescription("ԤԼδ��Ч")]
				RESVSTAT_UNDO = 0x100,
			
				[EnumDescription("ԤԼ����ִ��")]
				RESVSTAT_DOING = 0x200,
			
				[EnumDescription("δ�ɷ�")]
				RESVSTAT_UNPAID = 0x400,
			
				[EnumDescription("��ЧԤԼ��ȡ��")]
				RESVSTAT_CANCEL = 0x800,
			
				[EnumDescription("ԤԼ�ѷ�������豸")]
				RESVSTAT_DEV = 0x1000,
			
				[EnumDescription("ԤԼ�ѳ�ʱ")]
				RESVSTAT_TIMEOUT = 0x2000,
			
				[EnumDescription("��ֹ�޸�ԤԼ")]
				RESVSTAT_NOCHG = 0x4000,
			
				[EnumDescription("�ѽɷ�")]
				RESVSTAT_PAID = 0x8000,
			
				[EnumDescription("��ʦ�����Աδǩ��")]
				RESVSTAT_UNSIGN = 0x10000,
			
				[EnumDescription("��ʦ�����Ա��ǩ��")]
				RESVSTAT_SIGNED = 0x20000,
			
				[EnumDescription("ԤԼΥԼ")]
				RESVSTAT_DEFAULT = 0x40000,
			
				[EnumDescription("δ����")]
				RESVSTAT_UNSETTLE = 0x80000,
			
				[EnumDescription("�ѽ���")]
				RESVSTAT_SETTLED = 0x100000,
			
				[EnumDescription("δ����")]
				RESVSTAT_UNRECEIVE = 0x200000,
			
				[EnumDescription("������")]
				RESVSTAT_RECEIVED = 0x400000,
			
				[EnumDescription("δ����������")]
				RESVSTAT_NOFEED = 0x800000,
			
				[EnumDescription("���»�")]
				RESVSTAT_PCCLOSED = 0x1000000,
			
				[EnumDescription("ԤԼ��ִ�н���")]
				RESVSTAT_DONE = 0x40000000,
			
		}

	
		public uint? dwProperty;		/*ԤԼ����*/
	
		[FlagsAttribute]
		public enum DWPROPERTY : uint
		{
			
				[EnumDescription("�������")]
				RESVPROP_SELFDO = 1,
			
				[EnumDescription("����Ա��ͬ")]
				RESVPROP_WITHMAN = 2,
			
				[EnumDescription("����Ա����")]
				RESVPROP_MANDO = 4,
			
				[EnumDescription("��ռ���䣨�����ڵ������豸�������ã�")]
				RESVPROP_LOCKROOM = 8,
			
				[EnumDescription("ϵͳ�Զ������豸")]
				RESVPROP_AUTOASSIGN = 0x10,
			
				[EnumDescription("��ǩ���󷽿ɵ�¼")]
				RESVPROP_NEEDSIGN = 0x20,
			
				[EnumDescription("������豸��ͻ")]
				RESVPROP_NOCONFLICTCHECK = 0x40,
			
				[EnumDescription("�Դ��Ĳ�")]
				RESVPROP_SELFCONSUMABLE = 0x80,
			
				[EnumDescription("ǿ�ƹػ�")]
				RESVPROP_ENDSHUTDOWN = 0x100,
			
				[EnumDescription("ǿ��ע��")]
				RESVPROP_ENDLOGOUT = 0x200,
			
				[EnumDescription("��ʱ֪ͨ")]
				RESVPROP_ENDNOTICE = 0x400,
			
				[EnumDescription("��ʱ��")]
				RESVPROP_MULTTIME = 0x800,
			
				[EnumDescription("�෿��")]
				RESVPROP_MULTROOM = 0x1000,
			
				[EnumDescription("�������豸")]
				RESVPROP_MULTDEV = 0x2000,
			
				[EnumDescription("��Լ")]
				RESVPROP_CONTINUE = 0x4000,
			
				[EnumDescription("��Լ��ͬ�豸")]
				RESVPROP_CONTINUESAME = 0x8000,
			
				[EnumDescription("���豸����ԤԼ")]
				RESVPROP_BYDEVKIND = 0x10000,
			
				[EnumDescription("��¼��")]
				RESVPROP_NEEDVIDEO = 0x20000,
			
				[EnumDescription("�뿪ʱ����ˢ��")]
				RESVPROP_EXITCARD = 0x40000,
			
				[EnumDescription("��ʱ�뿪��ʱԤԼ�Ա���������UNIRESVRULE.dwLimitָ����")]
				RESVPROP_LEAVEHOLD = 0x80000,
			
				[EnumDescription("������")]
				RESVPROP_UNOPEN = 0x100000,
			
				[EnumDescription("ӯ��")]
				RESVPROP_PROFIT = 0x200000,
			
				[EnumDescription("ʹ�����ر�ʱ��")]
				RESVPROP_TOCLOSETIME = 0x400000,
			
				[EnumDescription("������ԤԼ")]
				RESVPROP_BYTHIRD = 0x800000,
			
				[EnumDescription("������������豸")]
				RESVPROP_THIRDSHARE = 0x1000000,
			
				[EnumDescription("ԤԼ����Ϸ���")]
				RESVPROP_COMBINEROOM = 0x2000000,
			
				[EnumDescription("ԤԼ����Ϸ�����ӷ���")]
				RESVPROP_SUBROOM = 0x4000000,
			
				[EnumDescription("��Ҫ�Գ�Ա����(����)")]
				RESVPROP_ATTENDANCE = 0x8000000,
			
				[EnumDescription("VIPԤԼ")]
				RESVPROP_VIP = 0x10000000,
			
		}

	
		public uint? dwTestItemID;		/*ʵ����ĿID*/
	
		public string szTestName;		/*ʵ������*/
	
		public uint? dwTestHour;		/*ʵ��ѧʱ��*/
	
		public uint? dwSignTime;		/*��ʦǩ��ʱ��*/
	
		public uint? dwResvDevs;		/*ԤԼ������*/
	
		public uint? dwUseDevs;		/*ʵ���û���*/
	
		public uint? dwResvUsers;		/*�Ͽ�������*/
	
		public uint? dwRealUsers;		/*ʵ�ʵ�������*/
	
		public string szApplicationURL;		/*�ύ�������������*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*��ȡ����ʵ��ԤԼ�������*/
	public struct RTRESVREQ
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*ԤԼ��*/
	
		public uint? dwDevID;		/*�豸ID*/
	
		public uint? dwDevKind;		/*�豸����*/
	
		public uint? dwClassKind;		/*���(��UNIDEVCLS��Kind����)*/
	
		public uint? dwRTID;		/*����ʵ��ƻ�ID*/
	
		public uint? dwHolderID;		/*�����ˣ��ʺţ�*/
	
		public uint? dwMAccNo;		/*���Ա�ʺ�*/
	
		public uint? dwLeaderID;		/*������ID*/
	
		public uint? dwCheckStat;		/*ȷ��״̬*/
	
		public uint? dwUnNeedStat;		/*������״̬*/
	
		public uint? dwUseMode;		/*ʹ�÷���*/
	
		public uint? dwPurpose;		/*ԤԼ��;*/
	
		public uint? dwBeginDate;		/*ԤԼ��ʼ����*/
	
		public uint? dwEndDate;		/*ԤԼ��������*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*ԤԼ��Ʒ��*/
	public struct RESVSAMPLE
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*ԤԼID*/
	
		public uint? dwSampleSN;		/*��Ʒ���*/
	
		public string szSampleName;		/*��Ʒ����*/
	
		public string szUnitName;		/*�Ʒѵ�λ*/
	
		public uint? dwUnitFee;		/*����*/
	
		public uint? dwSampleNum;		/*����*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*ԤԼ��*/
	public struct RTRESV
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*ԤԼ��*/
	
		public string szTestName;		/*����ʵ������*/
	
		public uint? dwUseMode;		/*ʹ�÷���*/
	
		public uint? dwPurpose;		/*ԤԼ��;*/
	
		public uint? dwProperty;		/*ԤԼ����*/
	
		public uint? dwStatus;		/*ԤԼ״̬(������飬�Ƿ���Ч���Ƿ���ȡ����)*/
	
		public uint? dwOwner;		/*ԤԼ��(������)*/
	
		public string szOwnerName;		/*ԤԼ������*/
	
		public uint? dwResvRuleSN;		/*����ԤԼ����*/
	
		public uint? dwOpenRuleSN;		/*��������ʱ���*/
	
		public uint? dwFeeSN;		/*����SN*/
	
		public uint? dwFeeMode;		/*�շѷ�ʽ*/
	
		public uint? dwUseFee;		/*ԤԼ�ܷ���*/
	
		public uint? dwPreDate;		/*ԤԼ��ʼ����*/
	
		public uint? dwOccurTime;		/*ԤԼ����ʱ��*/
	
		public uint? dwBeginTime;		/*ԤԼ��ʼʱ��*/
	
		public uint? dwEndTime;		/*ԤԼ����ʱ��*/
	
		public uint? dwCheckTime;		/*���ʱ��*/
	
		public uint? dwAdvanceCheckTime;		/*��ǰ���ʱ��*/
	
		public uint? dwDevID;		/*�豸ID*/
	
		public uint? dwDevKind;		/*�豸����*/
	
		public uint? dwDevSN;		/*�豸���*/
	
		public string szDevName;		/*�豸����*/
	
		public uint? dwRoomID;		/*����ID*/
	
		public string szRoomName;		/*��������*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabName;		/*ʵ��������*/
	
		public uint? dwBuildingID;		/*¥��ID*/
	
		public string szBuildingNo;		/*��¥���(*/
	
		public string szBuildingName;		/*��¥����(*/
	
		public uint? dwCampusID;		/*У��ID*/
	
		public string szCampusName;		/*У������*/
	
		public uint? dwRTID;		/*����ʵ����ĿID*/
	
		public string szRTName;		/*����ʵ������*/
	
		public uint? dwHolderID;		/*�����ˣ��ʺţ�*/
	
		public string szHolderName;		/*����������*/
	
		public uint? dwUserDeptID;		/*ʹ���˲���ID*/
	
		public string szUserDeptName;		/*ʹ���˲���*/
	
		public uint? dwLeaderID;		/*������ID*/
	
		public string szLeaderName;		/*����������*/
	
		public uint? dwGroupID;		/*��ID*/
	
		public uint? dwManID;		/*����ԱID*/
	
		public string szManName;		/*����Ա����*/
	
		public uint? dwEstimatedTime;		/*Ԥ��ʱ��(����)*/
	
		public uint? dwTestTimes;		/*ʵ�����*/
	
		public uint? dwRealUseTime;		/*ʵ��ʵ��ʱ��(����)*/
	
		public uint? dwReceivableCost;		/*Ӧ�ɷ���*/
	
		public uint? dwRealCost;		/*ʵ�ʽ��ɷ���*/
	
		public uint? dwPrepayment;		/*Ԥ�տ���*/
	
	public RESVSAMPLE[] ResvSample;		/*CUniTable[RESVSAMPLE](��ȡһ��ԤԼʱ�ŷ���)*/
	
		public string szConsumables;		/*����Ĳ��嵥*/
	
		public uint? dwBeforePersons;		/*ǰ���Ŷ�����*/
	
		public string szFundsNo;		/*���ѿ����*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*����ʵ�����*/
	public struct RTRESVCHECK
	{
		private Reserved reserved;
		
		public uint? dwCheckStat;		/*����Ա���״̬(������ADMINCHECK)*/
	
		public string szCheckDetail;		/*���˵��*/
	
	public RTRESV RTResv;		/*CUniStruct[RTRESV]*/
	
	public RTBILL[] BillInfo;		/*CUniTable[RTBILL]*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*����ʵ���˵����*/
	public struct RTPREPAY
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*ԤԼ��*/
	
		public uint? dwPrepayment;		/*Ԥ�տ���*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*��ȡ����ʵ���˵��������*/
	public struct RTRESVBILLREQ
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*ԤԼ��*/
		};

	/*����ʵ���˵�*/
	public struct RTRESVBILL
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*ԤԼ��*/
	
		public uint? dwPrepayment;		/*Ԥ�տ���*/
	
	public RTBILL[] BillInfo;		/*CUniTable[RTBILL]*/
		};

	/*����ʵ���˵�*/
	public struct RTBILL
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*��ˮ��*/
	
		public uint? dwResvID;		/*ԤԼ��*/
	
		public uint? dwFeeType;		/*�շ����(FEEDETAIL����)*/
	
		public uint? dwUnitFee;		/*����*/
	
		public uint? dwReceivableCost;		/*Ӧ�ɷ���*/
	
		public uint? dwRealCost;		/*ʵ�ʽ��ɷ���*/
	
		public uint? dwPayKind;		/*�ɷѷ�ʽ(UNIBILL����)*/
	
		public uint? dwStatus;		/*CHECKINFO����Ĺ���Ա���״̬+UNIBILL����*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*����ʵ���˵����*/
	public struct RTBILLCHECK
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*ԤԼ��*/
	
	public RTBILL[] BillInfo;		/*CUniTable[RTBILL]*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*����ʵ���˵�����*/
	public struct RTBILLSETTLE
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*ԤԼ��*/
	
		public uint? dwPurpose;		/*ԤԼ��;(��UNIRESERVE����USEBY_XXX��*/
	
		public uint? dwPayKind;		/*�ɷѷ�ʽ*/
	
		public uint? dwTotalCost;		/*�ɷѺϼ�*/
	
		public string szFundsNo;		/*���ѿ���ţ�����ö��Ÿ���)*/
	
		public string szCostInfo;		/*�۷���Ϣ*/
	
	public RTBILL[] BillInfo;		/*CUniTable[RTBILL]*/
	
	public RESVSAMPLE[] ResvSample;		/*CUniTable[RESVSAMPLE]*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*����ʵ���˵�����*/
	public struct RTBILLRECEIVE
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*ԤԼ��*/
	
		public uint? dwReceiveDate;		/*��������*/
	
		public uint? dwTotalCost;		/*�ɷѺϼ�*/
	
		public uint? dwTestFee;		/*�������Է�*/
	
		public uint? dwOpenFundFee;		/*���Ż���*/
	
		public uint? dwServiceFee;		/*�����*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*���¼ԤԼ*/
	public struct ANONYMOUSRESV
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*ԤԼ��*/
	
		public uint? dwPurpose;		/*ԤԼ��;*/
	
		public uint? dwPreDate;		/*ԤԼ��ʼ����*/
	
		public uint? dwBeginTime;		/*ԤԼ��ʼʱ��*/
	
		public uint? dwEndTime;		/*ԤԼ����ʱ��*/
	
		public uint? dwYearTerm;		/*ѧ�ڱ��*/
	
		public uint? dwTeachingTime;		/*��ѧʱ��(WWDSSEE)�ڼ��ܣ�WW�����ڼ�(D),��ʼ�ڴΣ�SS�������ڴΣ�EE)*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabName;		/*ʵ��������*/
	
		public string szTestName;		/*ʵ������*/
	
	public RESVDEV[] ResvDev;		/*ԤԼ�豸��ϸ��(CUniTable<RESVDEV>)*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*ȫ��ѧ��ԤԼ*/
	public struct ALLUSERRESV
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*ԤԼ��*/
	
		public uint? dwPurpose;		/*ԤԼ��;*/
	
		public uint? dwPreDate;		/*ԤԼ��ʼ����*/
	
		public uint? dwBeginTime;		/*ԤԼ��ʼʱ��*/
	
		public uint? dwEndTime;		/*ԤԼ����ʱ��*/
	
		public uint? dwYearTerm;		/*ѧ�ڱ��*/
	
		public uint? dwTeachingTime;		/*��ѧʱ��(WWDSSEE)�ڼ��ܣ�WW�����ڼ�(D),��ʼ�ڴΣ�SS�������ڴΣ�EE)*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabName;		/*ʵ��������*/
	
		public string szTestName;		/*ʵ������*/
	
	public RESVDEV[] ResvDev;		/*ԤԼ�豸��ϸ��(CUniTable<RESVDEV>)*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*��ȡԤԼ�б�������վ��ʾ�������*/
	public struct RESVSHOWREQ
	{
		private Reserved reserved;
		
		public uint? dwBeginDate;		/*ԤԼ��ʼ����*/
	
		public uint? dwEndDate;		/*ԤԼ��������*/
	
		public uint? dwDevKind;		/*�豸����*/
	
		public uint? dwClassKind;		/*���(��UNIDEVCLS��Kind����)*/
	
		public uint? dwCheckStat;		/*ȷ��״̬*/
	
		public uint? dwPurpose;		/*ԤԼ��;*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*ԤԼ��*/
	public struct RESVSHOW
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*ԤԼ��*/
	
		public uint? dwPurpose;		/*ԤԼ��;*/
	
		public uint? dwKind;		/*ԤԼ����(����ͳ����)*/
	
		public uint? dwOwner;		/*ԤԼ��(������)*/
	
		public string szOwnerName;		/*ԤԼ������*/
	
		public uint? dwPreDate;		/*ԤԼ��ʼ����*/
	
		public uint? dwBeginTime;		/*ԤԼ��ʼʱ��*/
	
		public uint? dwEndTime;		/*ԤԼ����ʱ��*/
	
		public uint? dwStatus;		/*ԤԼ״̬(������飬�Ƿ���Ч���Ƿ���ȡ����)*/
	
		public uint? dwProperty;		/*ԤԼ����*/
	
		public string szTestName;		/*ʵ������*/
	
		public string szRoomNo;		/*�����*/
	
		public uint? dwDevSN;		/*�豸���*/
	
		public string szDevName;		/*�豸��*/
		};

	/*��ȡ��ѧԤԼ�б�������*/
	public struct TEACHINGRESVREQ
	{
		private Reserved reserved;
		
		public uint? dwBeginDate;		/*ԤԼ��ʼ����*/
	
		public uint? dwEndDate;		/*ԤԼ��������*/
	
		public uint? dwTeacherID;		/*��ʦ���ʺţ�*/
	
		public uint? dwAccNo;		/*��Ա�˺ţ���ȡ��Ա��ص�����ʵ�鰲��*/
	
		public uint? dwTestPlanID;		/*��ȡTestPlan��ص�����ʵ�鰲��*/
	
		public uint? dwCourseID;		/*��ȡ�γ���ص�����ʵ�鰲��*/
	
		public uint? dwTestItemID;		/*��ȡTestItem��ص�����ʵ�鰲��*/
	
		public uint? dwYearTerm;		/*ѧ�ڱ��*/
	
		public uint? dwPlanKind;		/*�ƻ�����*/
	
		public uint? dwResvStat;		/*ԤԼ״̬*/
	
		public string szRoomNo;		/*�����*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*��ѧԤԼ*/
	public struct TEACHINGRESV
	{
		private Reserved reserved;
		
		public uint? dwTestItemID;		/*ʵ����ĿID*/
	
		public string szTestName;		/*ʵ������*/
	
		public uint? dwTestPlanID;		/*ʵ��ƻ�ID*/
	
		public string szTestPlanName;		/*ʵ��ƻ�����*/
	
		public uint? dwPlanKind;		/*�ƻ�����*/
	
		public uint? dwTeacherID;		/*��ʦ���ʺţ�*/
	
		public string szTeacherName;		/*��ʦ����*/
	
		public uint? dwCourseID;		/*�γ�ID*/
	
		public string szCourseName;		/*�γ�����*/
	
		public uint? dwGroupID;		/*�Ͽΰ༶*/
	
		public string szGroupName;		/*�Ͽΰ༶����*/
	
		public uint? dwGroupUsers;		/*���û���*/
	
		public uint? dwResvID;		/*ԤԼID*/
	
		public uint? dwResvStat;		/*ԤԼ״̬*/
	
		public uint? dwPreDate;		/*ԤԼ����*/
	
		public uint? dwBeginTime;		/*ԤԼ��ʼʱ��*/
	
		public uint? dwEndTime;		/*ԤԼ����ʱ��*/
	
		public uint? dwTeachingTime;		/*��ѧʱ��(WWDSSEE)�ڼ��ܣ�WW�����ڼ�(D),��ʼ�ڴΣ�SS�������ڴΣ�EE)*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabName;		/*ʵ��������*/
	
	public RESVDEV[] ResvDev;		/*ԤԼ�豸��ϸ��(CUniTable<RESVDEV>)*/
	
		public uint? dwCurUsers;		/*��ǰ����������Ŀǰ��Ч��ԤԼ��Ч��*/
		};

	/*�żٵ���*/
	public struct HOLIDAYSHIFT
	{
		private Reserved reserved;
		
		public uint? dwOldDate;		/*ԭ�Ͽ�����*/
	
		public uint? dwNewDate;		/*���Ͽ�����*/
	
		public uint? dwNoticeFlag;		/*֪ͨ��־*/
	
		public string szMemo;		/*˵��*/
		};

	/*ԤԼ���Ա*/
	public struct RESVMEMBER
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*ԤԼ��*/
	
		public uint? dwAccNo;		/*��Ա�˺�*/
	
		public string szTrueName;		/*����*/
		};

	/*ԤԼ��ʼ��Ϣ*/
	public struct RESVSTARTINFO
	{
		private Reserved reserved;
		
		public uint? dwForceLogoff;		/*�Ƿ�ע���û�*/
	
		public uint? dwNoLogon;		/*���¼��־*/
	
		public uint? dwPurpose;		/*ԤԼĿ��*/
	
		public string szClassName;		/*ԤԼ�༶*/
	
		public string szUsers;		/*ԤԼѧ��*/
		};

	/*ԤԼ��ʼ��Ϣ*/
	public struct RESVENDINFO
	{
		private Reserved reserved;
		
		public uint? dwEndCmd;		/*������ʽ*/
	
		[FlagsAttribute]
		public enum DWENDCMD : uint
		{
			
				[EnumDescription("ϵͳǿ�ƽ���")]
				REVEND_BYSYS = 1,
			
				[EnumDescription("�û����н���")]
				REVEND_BYUSER = 2,
			
		}

	
		public string szMsg;		/*��Ϣ*/
		};

	/*�Զ�ԤԼ����ϵͳ�Զ������豸����ʱ�������ȴ��û�ȷ��*/
	public struct AUTORESVREQ
	{
		private Reserved reserved;
		
		public uint? dwOwner;		/*ԤԼ��(������)*/
	
		public uint? dwKind;		/*ԤԼ��Ա���*/
	
		public uint? dwPurpose;		/*ԤԼ��;*/
	
		public uint? dwPreDate;		/*ԤԼ��������*/
	
		public uint? dwEarlyBeginTime;		/*ԤԼ���翪ʼʱ��*/
	
		public uint? dwLateBeginTime;		/*ԤԼ����ʼʱ��*/
	
		public uint? dwUseMin;		/*ʹ��ʱ��*/
	
		public uint? dwRoomID;		/*����ID*/
	
		public uint? dwDevKind;		/*�豸����*/
	
		public string szUserLimit;		/*ʹ����Լ������δʹ��)*/
	
		public string szDateLimit;		/*ʱ��Լ������δʹ��)*/
	
		public string szDevLimit;		/*�豸Լ������δʹ��)*/
		};

	/*ԤԼ�豸��ϸ*/
	public struct RESVDEV
	{
		private Reserved reserved;
		
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabName;		/*ʵ��������*/
	
		public uint? dwRoomID;		/*����ID*/
	
		public string szRoomNo;		/*������*/
	
		public string szRoomName;		/*��������*/
	
		public uint? dwDevKind;		/*�豸����*/
	
		public uint? dwDevNum;		/*�豸����*/
	
		public uint? dwDevStart;		/*�豸��ʼ���*/
	
		public uint? dwDevEnd;		/*�豸�������*/
	
		public string szDevName;		/*�豸��*/
	
		public string szModel;		/*�豸�ͺ�*/
	
		public string szSpecification;		/*�豸���*/
	
		public uint? dwDevClsKind;		/*�豸������*/
	
		public string szMemo;		/*��ע*/
		};

	/*ԤԼʱ�����ϸ*/
	public struct RESVTIMEREQ
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*ԤԼ��*/
	
		public uint? dwResvGroupID;		/*��ID*/
		};

	/*ԤԼʱ�����ϸ*/
	public struct RESVTIME
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*ԤԼ��*/
	
		public uint? dwResvGroupID;		/*��ID*/
	
		public uint? dwPreDate;		/*ԤԼ��������*/
	
		public uint? dwBeginTime;		/*ԤԼ��ʼʱ��*/
	
		public uint? dwEndTime;		/*ԤԼ����ʱ��*/
	
		public uint? dwTeachingTime;		/*��ѧʱ��(WWDSSEE)�ڼ��ܣ�WW�����ڼ�(D),��ʼ�ڴΣ�SS�������ڴΣ�EE)*/
	
		public string szMemo;		/*��ע*/
		};

	/*ԤԼ���ú���*/
	public struct RESVCOSTADJUST
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*ԤԼ��*/
	
		public uint? dwAdjustFee;		/*��������*/
	
		public uint? dwSampleFee;		/*��Ʒ��*/
	
		public uint? dwConfirmorID;		/*����Ա�ʺ�*/
	
		public string szConfirmor;		/*����Ա����*/
	
		public string szMemo;		/*��ע��������õ�����˵��)*/
		};

	/*ԤԼ���ý���*/
	public struct RESVCHECKOUT
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*ԤԼ��*/
	
		public uint? dwBillID;		/*�˵���*/
	
		public uint? dwRealCheckFee;		/*ʵ�ʽ������*/
	
		public uint? dwConfirmorID;		/*������Ա�ʺ�*/
	
		public string szConfirmor;		/*������Ա����*/
	
		public string szMemo;		/*��ע�����㸽�ӷ��õ�˵��)*/
		};

	/*ԤԼ����ʱ��*/
	public struct RESVENDTIME
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*ԤԼID*/
	
		public uint? dwEndTime;		/*����ʱ��*/
		};

	/*��ȡ�豸ԤԼ��Ϣ�������*/
	public struct DEVRESVREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*��ʽ*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("�豸ID")]
				DEVRESVGET_BYID = 1,
			
		}

	
		public string szGetKey;		/*����ֵ*/
	
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*�豸ԤԼ��Ϣ*/
	public struct DEVRESVINFO
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*�豸ID*/
	
		public uint? dwResvID;		/*ԤԼ��*/
	
		public uint? dwBeginTime;		/*ԤԼ��ʼʱ��*/
	
		public uint? dwEndTime;		/*ԤԼ����ʱ��*/
		};

	/*��ȡ��Ϣʱ��������*/
	public struct CTSREQ
	{
		private Reserved reserved;
		
		public uint? dwSN;		/*��Ϣ����*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*��Ϣʱ���*/
	public struct CLASSTIMETABLE
	{
		private Reserved reserved;
		
		public uint? dwSN;		/*��Ϣ����*/
	
		public uint? dwSecIndex;		/*�ڴα��(1��ʼ��)*/
	
		public string szSecName;		/*�ڴ�����*/
	
		public uint? dwBeginTime;		/*��ʼʱ��(HHMM)*/
	
		public uint? dwEndTime;		/*����ʱ��(HHMM)*/
		};

	/*��ȡѧ����Ϣ�������*/
	public struct TERMREQ
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*ѧ�ڱ�ţ�20091001 2009��10���һѧ�ڣ�*/
	
		public uint? dwStatus;		/*ѧ��״̬*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/**/
	public struct UNITERM
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*ѧ�ڱ�ţ�20091001 2009��10���һѧ�ڣ�*/
	
		public uint? dwStatus;		/*ѧ��״̬*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("�ѹ���")]
				TERMSTAT_OVER = 1,
			
				[EnumDescription("��Ч��")]
				TERMSTAT_FORCE = 2,
			
				[EnumDescription("δ��Ч")]
				TERMSTAT_UNFORCE = 4,
			
		}

	
		public uint? dwBeginDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
		public uint? dwFirstWeekDays;		/*��ʼ������*/
	
		public uint? dwTotalWeeks;		/*һ���ж�����*/
	
		public uint? dwSecNum;		/*ÿ�սڴ�����*/
	
	public CLASSTIMETABLE[] szCTS1;		/*��Ϣʱ���1(CUniTable<CLASSTIMETABLE>)*/
	
		public uint? dwCTS1Begin;		/*ʱ���1��ʼ��Ч����*/
	
		public uint? dwCTS1End;		/*ʱ���1������Ч����*/
	
	public CLASSTIMETABLE[] szCTS2;		/*��Ϣʱ���2(CUniTable<CLASSTIMETABLE>)*/
	
		public uint? dwCTS2Begin;		/*ʱ���2��ʼ��Ч����*/
	
		public uint? dwCTS2End;		/*ʱ���2������Ч����*/
	
		public string szMemo;		/*��ע*/
		};

	/*��ȡ�γ̵������*/
	public struct COURSEREQ
	{
		private Reserved reserved;
		
		public uint? dwCourseID;		/*�γ�ID*/
	
		public string szCourseCode;		/*�γ̴���*/
	
		public string szCourseName;		/*�γ�����*/
	
		public uint? dwOwnerDept;		/*����ѧԺ*/
	
		public uint? dwMajorID;		/*����רҵID*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*�γ�*/
	public struct UNICOURSE
	{
		private Reserved reserved;
		
		public uint? dwCourseID;		/*�γ�ID*/
	
		public string szCourseCode;		/*�γ̴���*/
	
		public string szCourseName;		/*�γ�����*/
	
		public uint? dwCourseProperty;		/*�γ�����*/
	
		[FlagsAttribute]
		public enum DWCOURSEPROPERTY : uint
		{
			
				[EnumDescription("���ۿ��е�ʵ��")]
				COURSEPROP_WITHTHEORY = 1,
			
				[EnumDescription("���������ʵ���")]
				COURSEPROP_NOTHEORY = 2,
			
		}

	
		public uint? dwOwnerDept;		/*����ѧԺ*/
	
		public string szDeptName;		/*����ѧԺ����*/
	
		public uint? dwMajorID;		/*����רҵID*/
	
		public string szMajorName;		/*����רҵ����*/
	
		public string szType;		/*�γ����ѡ�ޣ����޵ȣ�*/
	
		public uint? dwHardCoef;		/*�Ѷ�ϵ��*/
	
		public uint? dwCreditHour;		/*ѧ��*/
	
		public uint? dwTheoryHour;		/*����ѧʱ��*/
	
		public uint? dwTestHour;		/*ʵ��ѧʱ��*/
	
		public uint? dwPracticeHour;		/*ʵ��ѧʱ��*/
	
		public uint? dwTestNum;		/*ʵ�����*/
	
		public string szMemo;		/*��ע*/
		};

	/*��ȡʵ��ƻ������*/
	public struct TESTPLANREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*��ʽ*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("ȫ��")]
				TESTPLANGET_BYALL = 0,
			
				[EnumDescription("ID")]
				TESTPLANGET_BYID = 1,
			
				[EnumDescription("�γ�ID")]
				TESTPLANGET_BYCOURSEID = 2,
			
				[EnumDescription("�γ�����")]
				TESTPLANGET_BYNAME = 3,
			
				[EnumDescription("����ѧԺ")]
				TESTPLANGET_BYDEPT = 4,
			
				[EnumDescription("רҵ")]
				TESTPLANGET_BYMAJOR = 5,
			
				[EnumDescription("��ʦ")]
				TESTPLANGET_BYTEACHER = 6,
			
				[EnumDescription("��Ա�˺ţ���ȡ��Ա��ص�����ʵ�鰲��")]
				TESTPLANGET_BYMEMBERACCNO = 7,
			
		}

	
		public string szGetKey;		/*����ֵ*/
	
		public uint? dwYearTerm;		/*ѧ�ڱ��*/
	
		public uint? dwKind;		/*ʵ��ƻ�����*/
	
		public uint? dwStatus;		/*ʵ��ƻ�״̬*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/**/
	public struct UNITESTPLAN
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*ѧ�ڱ��*/
	
		public uint? dwTestPlanID;		/*ʵ��ƻ�ID*/
	
		public string szTestPlanName;		/*ʵ��ƻ�����*/
	
		public string szAcademicSubjectCode;		/*����ѧ�Ʊ���*/
	
		public uint? dwTesteeKind;		/*ʵ�������*/
	
		[FlagsAttribute]
		public enum DWTESTEEKIND : uint
		{
			
				[EnumDescription("��ʿ��")]
				TESTEEKIND_DOCTOR = 1,
			
				[EnumDescription("˶ʿ��")]
				TESTEEKIND_MASTER = 2,
			
				[EnumDescription("������")]
				TESTEEKIND_COLLEGE = 3,
			
				[EnumDescription("ר����")]
				TESTEEKIND_JUNIOR = 4,
			
				[EnumDescription("����")]
				TESTEEKIND_OTHER = 5,
			
		}

	
		public uint? dwTeacherID;		/*��ʦ���ʺţ�*/
	
		public string szTeacherName;		/*��ʦ����*/
	
		public uint? dwTeacherDeptID;		/*��ʦ��������ID*/
	
		public string szTeacherDeptName;		/*��ʦ��������*/
	
		public uint? dwCourseID;		/*�γ�ID*/
	
		public string szCourseCode;		/*�γ̴���*/
	
		public string szCourseName;		/*�γ�����*/
	
		public uint? dwCourseProperty;		/*�γ�����*/
	
		public uint? dwTheoryHour;		/*����ѧʱ��*/
	
		public uint? dwTestHour;		/*ʵ��ѧʱ��*/
	
		public uint? dwPracticeHour;		/*ʵ��ѧʱ��*/
	
		public uint? dwTestNum;		/*ʵ����Ŀ��*/
	
		public uint? dwGroupID;		/*�Ͽΰ༶*/
	
		public string szGroupName;		/*�Ͽΰ༶����*/
	
		public uint? dwMaxUsers;		/*����û���*/
	
		public uint? dwMinUsers;		/*�����û���*/
	
		public uint? dwGroupUsers;		/*�Ͽ�ѧ����*/
	
		public uint? dwEnrollDeadline;		/*��������ֹ��*/
	
		public uint? dwKind;		/*ʵ��ƻ�����*/
	
		[FlagsAttribute]
		public enum DWKIND : uint
		{
			
				[EnumDescription("��ѧͳһ����")]
				TESTPLANKIND_TEACHING = 1,
			
				[EnumDescription("��ѧ����ʵ��")]
				TESTPLANKIND_OPEN = 2,
			
				[EnumDescription("��ѧ��������")]
				TESTPLANKIND_TEACHFORSELF = 4,
			
		}

	
		public uint? dwStatus;		/*ʵ��ƻ�״̬��ǰ8����������CHECKINFO����Ĺ���Ա���״̬)*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("δ����")]
				TESTPLANSTAT_UNOPEN = 0x100,
			
				[EnumDescription("������")]
				TESTPLANSTAT_OPENING = 0x200,
			
				[EnumDescription("�ѹر�")]
				TESTPLANSTAT_CLOSED = 0x400,
			
				[EnumDescription("��ѡ��")]
				TESTPLANSTAT_SELECTED = 0x10000,
			
		}

	
		public string szTestPlanURL;		/*��ϸ����*/
	
		public uint? dwTotalTestHour;		/*��ѧʱ��*/
	
		public uint? dwResvTestHour;		/*��ԤԼѧʱ��*/
	
		public uint? dwDoneTestHour;		/*�����ѧʱ��*/
	
		public string szUsableLab;		/*����ʵ����ID������ö��Ÿ���)*/
	
		public string szMemo;		/*��ע*/
		};

	/*��ȡʵ����Ŀ�������*/
	public struct TESTCARDREQ
	{
		private Reserved reserved;
		
		public uint? dwTestCardID;		/*ʵ����Ŀ��ID*/
	
		public string szTestName;		/*ʵ�����ƣ�ģ��ƥ��)*/
	
		public uint? dwTestClass;		/*ʵ�����*/
	
		public uint? dwTestKind;		/*ʵ������*/
	
		public uint? dwRequirement;		/*ʵ��Ҫ��*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*ʵ����Ŀ��*/
	public struct TESTCARD
	{
		private Reserved reserved;
		
		public uint? dwTestCardID;		/*ʵ����Ŀ��ID*/
	
		public string szTestSN;		/*ʵ����*/
	
		public string szTestName;		/*ʵ������*/
	
		public string szCategoryName;		/*�����(*/
	
		public uint? dwGroupPeopleNum;		/*ÿ������*/
	
		public uint? dwTestHour;		/*��ʵ����Ŀѧʱ��*/
	
		public uint? dwTestClass;		/*ʵ������ǰ�ʵ����Ŀ����������ʷ�������������������רҵ��������רҵ�����С�������������Ϊ��ҵ���ġ���ҵ��ơ����������������������ʵ�飩��*/
	
		[FlagsAttribute]
		public enum DWTESTCLASS : uint
		{
			
				[EnumDescription("����")]
				TESTCLASS_BASIS = 1,
			
				[EnumDescription("רҵ����")]
				TESTCLASS_PROFBASIS = 2,
			
				[EnumDescription("רҵ")]
				TESTCLASS_PROFESSION = 3,
			
				[EnumDescription("����")]
				TESTCLASS_OTHER = 4,
			
		}

	
		public uint? dwTestKind;		/*ʵ�����ͣ���ʾ����֤���ۺϡ���ƣ�*/
	
		[FlagsAttribute]
		public enum DWTESTKIND : uint
		{
			
				[EnumDescription("��ʾ��")]
				TESTKIND_DEMO = 1,
			
				[EnumDescription("��֤��")]
				TESTKIND_VERIFY = 2,
			
				[EnumDescription("�ۺ���")]
				TESTKIND_INTEGRITY = 3,
			
				[EnumDescription("�о����")]
				TESTKIND_RESEARCH = 4,
			
				[EnumDescription("����")]
				TESTKIND_OTHER = 5,
			
		}

	
		public uint? dwRequirement;		/*ʵ��Ҫ������ޡ�ѡ�ޡ����������в��ԡ��������񡢼��������ȣ���*/
	
		[FlagsAttribute]
		public enum DWREQUIREMENT : uint
		{
			
				[EnumDescription("����")]
				TESTREQUIRE_REQUIRED = 1,
			
				[EnumDescription("ѡ��")]
				TESTREQUIRE_OPTIONAL = 2,
			
				[EnumDescription("����")]
				TESTREQUIRE_OTHER = 3,
			
		}

	
		public string szConstraints;		/*Լ������������ʱ�����ƺ���Ҫ���豸��,��ʽ��ר���ļ�����*/
	
		public string szTestItemURL;		/*��ϸ����*/
	
		public string szMemo;		/*��ע*/
		};

	/*��ȡʵ����Ŀ�����*/
	public struct TESTITEMREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*��ʽ*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("ȫ��")]
				TESTITEMGET_BYALL = 0,
			
				[EnumDescription("ID")]
				TESTITEMGET_BYID = 1,
			
				[EnumDescription("��ID")]
				TESTITEMGET_BYPLANID = 2,
			
				[EnumDescription("��Ŀ����")]
				TESTITEMGET_BYNAME = 3,
			
		}

	
		public string szGetKey;		/*����ֵ*/
	
		public uint? dwStatus;		/*״̬��ǰ8�������������״̬)*/
	
		public uint? dwCourseID;		/*�γ�ID*/
	
		public uint? dwTeacherID;		/*��ʦ���ʺţ�*/
	
		public uint? dwAccNo;		/*�ʺ�*/
	
		public uint? dwPlanKind;		/*�ƻ�����*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*ʵ�鰲����Ŀ��*/
	public struct UNITESTITEM
	{
		private Reserved reserved;
		
		public uint? dwTestItemID;		/*ʵ����ĿID*/
	
		public uint? dwTestPlanID;		/*ʵ��ƻ�ID*/
	
		public string szTestPlanName;		/*ʵ��ƻ�����*/
	
		public string szAcademicSubjectCode;		/*����ѧ�Ʊ���*/
	
		public uint? dwTesteeKind;		/*ʵ�������*/
	
		public uint? dwTotalTestHour;		/*��ʵ��ƻ���ѧʱ��*/
	
		public uint? dwTeacherID;		/*��ʦ���ʺţ�*/
	
		public string szTeacherName;		/*��ʦ����*/
	
		public uint? dwCourseID;		/*�γ�ID*/
	
		public string szCourseCode;		/*�γ̴���*/
	
		public string szCourseName;		/*�γ�����*/
	
		public uint? dwCourseProperty;		/*�γ�����*/
	
		public uint? dwGroupID;		/*�Ͽΰ༶*/
	
		public string szGroupName;		/*�Ͽΰ༶����*/
	
		public uint? dwGroupUsers;		/*���û���*/
	
		public uint? dwPlanKind;		/*�ƻ�����*/
	
		public uint? dwPlanStatus;		/*�ƻ�״̬*/
	
		public uint? dwTestCardID;		/*ʵ����Ŀ��ID*/
	
		public string szTestSN;		/*ʵ����*/
	
		public string szTestName;		/*ʵ������*/
	
		public string szCategoryName;		/*�����(*/
	
		public uint? dwStatus;		/*״̬��ǰ8�������������״̬)*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("��ԤԼ")]
				TESTITEMSTAT_RESVED = 0x100,
			
				[EnumDescription("��ִ��")]
				TESTITEMSTAT_DONE = 0x200,
			
				[EnumDescription("������ԤԼ")]
				TESTITEMSTAT_PARTRESVED = 0x400,
			
				[EnumDescription("������ִ��")]
				TESTITEMSTAT_PARTDONE = 0x800,
			
				[EnumDescription("�ѽ�ʵ�鱨��")]
				TESTITEMSTAT_REPORTDONE = 0x1000,
			
				[EnumDescription("ʵ�鱨��������")]
				TESTITEMSTAT_REPORTSCORE = 0x2000,
			
		}

	
		public uint? dwGroupPeopleNum;		/*ÿ������*/
	
		public uint? dwTestHour;		/*��ʵ����Ŀѧʱ��*/
	
		public uint? dwTestClass;		/*ʵ�����*/
	
		public uint? dwTestKind;		/*ʵ������*/
	
		public uint? dwRequirement;		/*ʵ��Ҫ��*/
	
		public string szTestItemURL;		/*��ϸ����*/
	
	public UNIRESERVE[] ResvInfo;		/*ԤԼ��ϸ��Ϣ*/
	
		public uint? dwMaxResvTimes;		/*���ԤԼ����*/
	
		public uint? dwResvTestHour;		/*��ԤԼѧʱ��*/
	
		public uint? dwDoneTestHour;		/*�����ѧʱ��*/
	
		public string szReportFormURL;		/*ʵ�鱨��ģ��*/
	
		public string szConstraints;		/*Լ��������������Ҫ���豸�ȣ�*/
	
		public string szMemo;		/*��ע*/
		};

	/*��ȡʵ����Ŀ������ԤԼ��Ϣ�����*/
	public struct TESTITEMMEMRESVREQ
	{
		private Reserved reserved;
		
		public uint? dwTestPlanID;		/*ʵ��ƻ�ID*/
	
		public uint? dwTestItemID;		/*ʵ����ĿID*/
	
		public uint? dwGroupID;		/*�Ͽΰ༶*/
	
		public uint? dwResvTestHour;		/*��ԤԼѧʱ��*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*ʵ����Ŀ������ԤԼ��Ϣ*/
	public struct TESTITEMMEMRESV
	{
		private Reserved reserved;
		
		public uint? dwAccNo;		/*�ʺ�*/
	
		public string szPID;		/*ѧ����*/
	
		public string szTrueName;		/*����*/
	
		public uint? dwResvTestHour;		/*��ԤԼѧʱ��*/
		};

	/*��ȡʵ����Ŀ��ϸ��Ϣ�����*/
	public struct TESTITEMINFOREQ
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*��¼ID*/
	
		public uint? dwTeacherID;		/*��ʦ���ʺţ�*/
	
		public uint? dwAccNo;		/*�ʺ�*/
	
		public uint? dwTestPlanID;		/*ʵ��ƻ�ID*/
	
		public uint? dwTestItemID;		/*ʵ����ĿID*/
	
		public uint? dwYearTerm;		/*ѧ�ڱ��*/
	
		public uint? dwPlanKind;		/*�ƻ�����*/
	
		public uint? dwStatus;		/*״̬��ǰ8�������������״̬)*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*ʵ����Ŀ��ϸ��Ϣ*/
	public struct TESTITEMINFO
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*��¼ID*/
	
		public uint? dwStatus;		/*��UNITESTITEM����*/
	
		public uint? dwAccNo;		/*�ʺ�*/
	
		public string szPID;		/*ѧ����*/
	
		public string szTrueName;		/*����*/
	
		public uint? dwClassID;		/*�༶ID*/
	
		public string szClassName;		/*�༶*/
	
		public uint? dwDeptID;		/*����ID*/
	
		public string szDeptName;		/*����*/
	
		public uint? dwMaxResvTimes;		/*���ԤԼ����*/
	
		public string szReportFormURL;		/*ʵ�鱨��ģ��*/
	
		public string szReportURL;		/*�ύ��ʵ�鱨��*/
	
		public uint? dwReportScore;		/*ʵ�鱨������*/
	
		public string szReportMarkInfo;		/*ʵ�鱨��������Ϣ*/
	
		public uint? dwYearTerm;		/*ѧ�ڱ��*/
	
		public uint? dwTestPlanID;		/*ʵ��ƻ�ID*/
	
		public string szTestPlanName;		/*ʵ��ƻ�����*/
	
		public uint? dwPlanKind;		/*�ƻ�����*/
	
		public uint? dwPlanStatus;		/*�ƻ�״̬*/
	
		public string szAcademicSubjectCode;		/*����ѧ�Ʊ���*/
	
		public uint? dwTesteeKind;		/*ʵ�������*/
	
		public uint? dwTotalTestHour;		/*��ʵ��ƻ���ѧʱ��*/
	
		public uint? dwTeacherID;		/*��ʦ���ʺţ�*/
	
		public string szTeacherName;		/*��ʦ����*/
	
		public uint? dwCourseID;		/*�γ�ID*/
	
		public string szCourseName;		/*�γ�����*/
	
		public uint? dwTestItemID;		/*ʵ����ĿID*/
	
		public string szConstraints;		/*Լ��������������Ҫ���豸�ȣ�*/
	
		public uint? dwTestCardID;		/*ʵ����Ŀ��ID*/
	
		public string szTestName;		/*ʵ������*/
	
		public string szCategoryName;		/*�����(*/
	
		public uint? dwGroupPeopleNum;		/*ÿ������*/
	
		public uint? dwTestHour;		/*��ʵ����Ŀѧʱ��*/
	
		public uint? dwTestClass;		/*ʵ�����*/
	
		public uint? dwTestKind;		/*ʵ������*/
	
		public uint? dwRequirement;		/*ʵ��Ҫ��*/
	
		public string szTestItemURL;		/*��ϸ����*/
	
		public uint? dwResvTestHour;		/*��ԤԼѧʱ��*/
	
		public uint? dwDoneTestHour;		/*�����ѧʱ��*/
	
	public UNIRESERVE[] ResvInfo;		/*ԤԼ��ϸ��Ϣ*/
	
		public string szMemo;		/*��ע*/
		};

	/*�ύʵ�鱨��ģ��*/
	public struct REPORTFORMUPLOAD
	{
		private Reserved reserved;
		
		public uint? dwTestItemID;		/*ʵ����ĿID*/
	
		public uint? dwTeacherID;		/*��ʦ���ʺţ�*/
	
		public string szReportFormURL;		/*ʵ�鱨��ģ��*/
		};

	/*�ύʵ�鱨��*/
	public struct REPORTUPLOAD
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*��¼ID*/
	
		public uint? dwAccNo;		/*�ʺ�*/
	
		public string szReportURL;		/*�ύ��ʵ�鱨��*/
		};

	/*����ʵ�鱨��*/
	public struct REPORTCORRECT
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*��¼ID*/
	
		public uint? dwAccNo;		/*�ʺ�*/
	
		public uint? dwTeacherID;		/*��ʦ���ʺţ�*/
	
		public uint? dwReportScore;		/*ʵ�鱨������*/
	
		public string szReportMarkInfo;		/*ʵ�鱨��������Ϣ*/
		};

	/*�豸���*/
	public struct DEVGROUP
	{
		private Reserved reserved;
		
		public uint? dwParentID;		/*������ID(����ʵ����ĿID)*/
	
		public uint? dwDevKind;		/*�豸����*/
	
		public string szDevName;		/*�豸����*/
	
		public uint? dwDevNum;		/*�豸����*/
	
		public uint? dwProperty;		/*�豸���ԣ�������ϵ�������ѡ����ѡ�ȣ�*/
	
		public string szMemo;		/*��ע*/
		};

	/*��ȡ����������*/
	public struct ACTIVITYPLANREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*��ʽ*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("ȫ��")]
				ACTIVITYPLANGET_BYALL = 0,
			
				[EnumDescription("ID")]
				ACTIVITYPLANGET_BYID = 1,
			
				[EnumDescription("�γ�����")]
				ACTIVITYPLANGET_BYNAME = 3,
			
		}

	
		public string szGetKey;		/*����ֵ*/
	
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwKind;		/*���������*/
	
		public uint? dwStatus;		/*�����״̬*/
	
		public uint? dwOwner;		/*ԤԼ��(������)*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/**/
	public struct UNIACTIVITYPLAN
	{
		private Reserved reserved;
		
		public uint? dwActivityPlanID;		/*�����ID*/
	
		public string szActivityPlanName;		/*���������*/
	
		public string szHostUnit;		/*���쵥λ*/
	
		public string szOrganizer;		/*�а쵥λ*/
	
		public string szPresenter;		/*������*/
	
		public string szDesiredUser;		/*������Ҫ��*/
	
		public uint? dwCheckRequirment;		/*���������Ҫ��*/
	
		[FlagsAttribute]
		public enum DWCHECKREQUIRMENT : uint
		{
			
				[EnumDescription("������������ʸ�")]
				ACTIVITYPLANCHECK_NEED = 0x1,
			
				[EnumDescription("�������뼴�ɲμ�")]
				ACTIVITYPLAN_NOAPPLY = 0x2,
			
				[EnumDescription("֧��ѡ��")]
				ACTIVITYPLAN_SUPPORTSEAT = 0x4,
			
				[EnumDescription("��Գ�ϯ�߿���")]
				ACTIVITYPLAN_ATTENDANCE = 0x8,
			
		}

	
		public uint? dwOwner;		/*ԤԼ��(������)*/
	
		public string szContact;		/*��ϵ��*/
	
		public string szTel;		/*��ϵ�绰*/
	
		public string szHandPhone;		/*��ϵ�ֻ�*/
	
		public string szEmail;		/*��ϵ��������*/
	
		public uint? dwResvID;		/*ԤԼ��*/
	
		public uint? dwGroupID;		/*��ID�����ڻ�ȡ���Ա��ϸ��*/
	
		public uint? dwMaxUsers;		/*�����������*/
	
		public uint? dwMinUsers;		/*������������*/
	
		public uint? dwEnrollUsers;		/*����������*/
	
		public uint? dwEnrollDeadline;		/*��������ֹ��*/
	
		public uint? dwPublishDate;		/*��������*/
	
		public uint? dwActivityDate;		/*�����*/
	
		public uint? dwBeginTime;		/*��ʼʱ��(HHMM)*/
	
		public uint? dwEndTime;		/*����ʱ��(HHMM)*/
	
		public string szSite;		/*����ص�*/
	
		public uint? dwDevID;		/*�ռ䣨����ص㣩ID*/
	
		public uint? dwDevSN;		/*�豸���*/
	
		public string szDevName;		/*�豸����*/
	
		public uint? dwRoomID;		/*����ID*/
	
		public string szRoomNo;		/*������*/
	
		public string szRoomName;		/*��������*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabName;		/*ʵ��������*/
	
		public uint? dwBuildingID;		/*¥��ID*/
	
		public string szBuildingNo;		/*��¥���(*/
	
		public string szBuildingName;		/*��¥����(*/
	
		public uint? dwCampusID;		/*У��ID*/
	
		public string szCampusName;		/*У������*/
	
		public uint? dwKind;		/*���������*/
	
		[FlagsAttribute]
		public enum DWKIND : uint
		{
			
				[EnumDescription("�²�Ʒ�¼����û�����")]
				ACTIVITYPLANKIND_EXPERIENCE = 1,
			
				[EnumDescription("ѧ������")]
				ACTIVITYPLANKIND_LECTURE = 2,
			
				[EnumDescription("�Ļ�ɳ��")]
				ACTIVITYPLANKIND_SALON = 4,
			
				[EnumDescription("����")]
				ACTIVITYPLANKIND_MEETING = 8,
			
		}

	
		public uint? dwStatus;		/*�����״̬��ǰ8����������CHECKINFO����Ĺ���Ա���״̬)*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("δ����")]
				ACTIVITYPLANSTAT_UNOPEN = 0x100,
			
				[EnumDescription("������")]
				ACTIVITYPLANSTAT_OPENING = 0x200,
			
				[EnumDescription("�ѹر�")]
				ACTIVITYPLANSTAT_CLOSED = 0x400,
			
				[EnumDescription("�Ѽ���û")]
				ACTIVITYPLANSTAT_ENROLLED = 0x10000,
			
				[EnumDescription("��ѡ��λ")]
				ACTIVITYPLANSTAT_SEAT = 0x20000,
			
		}

	
		public string szIntroInfo;		/*�����*/
	
		public string szActivityPlanURL;		/*��ϸ����URL*/
	
		public string szApplicationURL;		/*�ύ�������������*/
	
		public uint? dwRealUsers;		/*ʵ������*/
	
		public string szMemo;		/*��ע*/
		};

	/*��ȡ����ŵ���λ�����*/
	public struct APSEATREQ
	{
		private Reserved reserved;
		
		public uint? dwActivityPlanID;		/*�����ID*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*����ŵ���λ��Ϣ*/
	public struct APSEAT
	{
		private Reserved reserved;
		
		public uint? dwActivityPlanID;		/*�����ID*/
	
		public uint? dwDevID;		/*�豸ID*/
	
		public uint? dwDevSN;		/*�豸���*/
	
		public string szDevName;		/*�豸����*/
	
		public uint? dwStatus;		/*��λ״̬*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("����")]
				APSTAT_IDLE = 0x1,
			
				[EnumDescription("��Ԥ��")]
				APSTAT_BOOKED = 0x2,
			
		}

	
		public uint? dwAccNo;		/*�˺�*/
	
		public string szTrueName;		/*��Ա����*/
	
		public string szMemo;		/*��ע*/
		};

	/*����μӻ*/
	public struct ACTIVITYENROLL
	{
		private Reserved reserved;
		
		public uint? dwActivityPlanID;		/*�����ID*/
	
		public uint? dwDevID;		/*�豸ID*/
	
		public uint? dwDevSN;		/*�豸���*/
	
		public uint? dwAccNo;		/*�˺�*/
	
		public string szTrueName;		/*��Ա����*/
	
		public string szMemo;		/*��ע*/
		};

	/*�˳������*/
	public struct ACTIVITYEXIT
	{
		private Reserved reserved;
		
		public uint? dwActivityPlanID;		/*�����ID*/
	
		public uint? dwAccNo;		/*�˺�*/
	
		public string szTrueName;		/*��Ա����*/
	
		public string szMemo;		/*��ע*/
		};

	/*ǩ����Ա����*/
	public struct ASIGNUSER
	{
		private Reserved reserved;
		
		public uint? dwCardID;		/*��ID��*/
	
		public uint? dwInTime;		/*ǩ��ʱ��(HHMM)*/
	
		public uint? dwRetStat;		/*����״̬*/
	
		[FlagsAttribute]
		public enum DWRETSTAT : uint
		{
			
				[EnumDescription("ǩ���ɹ�")]
				SIGNRETSTAT_OK = 0x1,
			
				[EnumDescription("δ�ҵ��ÿ��Ŷ�Ӧ��ѧ��")]
				SIGNRETSTAT_NOCARDID = 0x100,
			
				[EnumDescription("δ���Ǹû�ĳ�Ա")]
				SIGNRETSTAT_NOMEMBER = 0x200,
			
				[EnumDescription("ԤԼ�ѽ���")]
				SIGNRETSTAT_RESVDONE = 0x400,
			
		}

	
		public uint? dwAccNo;		/*�˺�*/
	
		public string szLogonName;		/*��¼��(ѧ����)*/
	
		public string szCardNo;		/*����*/
	
		public string szTrueName;		/*��Ա����*/
	
		public string szMemo;		/*��ע*/
		};

	/*ǩ����Ա����*/
	public struct AOFFLINESIGN
	{
		private Reserved reserved;
		
		public uint? dwActivityPlanID;		/*�����ID*/
	
		public uint? dwResvID;		/*ԤԼ��*/
	
	public ASIGNUSER[] SignUser;		/*ǩ����CUniTable[ASIGNUSER]*/
	
		public string szMemo;		/*��ע*/
		};

	/**/
	public struct RESVRULEREQ
	{
		private Reserved reserved;
		
		public uint? dwRuleSN;		/*�豸������*/
	
		public uint? dwDevClass;		/*�豸���0��ʾ�����ƣ�*/
	
		public uint? dwDevKind;		/*�豸���ͣ�0��ʾ�����ƣ�*/
	
		public uint? dwDevID;		/*�豸ID��0��ʾ�����ƣ�*/
	
		public uint? dwResvPurpose;		/*ԤԼ��;*/
	
		public uint? dwClassKind;		/*���(��UNIDEVCLS��Kind����)*/
	
		public uint? dwExtValue;		/*��ͬԤԼ���Ͷ������չֵ��0��ʾ�����ƣ�*/
		};

	/**/
	public struct RESVRULEADMINREQ
	{
		private Reserved reserved;
		
		public uint? dwRuleSN;		/*�豸������*/
	
		public uint? dwDevClass;		/*�豸���0��ʾ�����ƣ�*/
	
		public uint? dwDevKind;		/*�豸���ͣ�0��ʾ�����ƣ�*/
	
		public uint? dwDevID;		/*�豸ID��0��ʾ�����ƣ�*/
	
		public uint? dwResvPurpose;		/*ԤԼ��;*/
	
		public uint? dwClassKind;		/*���(��UNIDEVCLS��Kind����)*/
	
		public uint? dwExtValue;		/*��ͬԤԼ���Ͷ������չֵ��0��ʾ�����ƣ�*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*��˱�*/
	public struct RULECHECKINFO
	{
		private Reserved reserved;
		
		public uint? dwResvRuleSN;		/*�豸������*/
	
		public uint? dwCheckKind;		/*������ͣ��½�ʱ��ϵͳ�Զ����䣩*/
	
		public uint? dwBeforeKind;		/*��������������(�ɶ����*/
	
		public uint? dwNeedMinTime;		/*�����Ҫ�����ʱ��*/
	
		public uint? dwMapValue;		/*�������ֵ����ԤԼ������أ�*/
	
		public uint? dwMainKind;		/*��˴���*/
	
		public string szCheckName;		/*�������*/
	
		public uint? dwCheckLevel;		/*��˼���(ͬUNIADMIN.dwManLevel���壩*/
	
		public uint? dwDeptID;		/*���β���ID��ѧԺ�������ã������������Զ�ƥ�䣩*/
	
		public string szDeptName;		/*���β���*/
	
		public uint? dwProperty;		/*�������*/
	
		[FlagsAttribute]
		public enum DWPROPERTY : uint
		{
			
				[EnumDescription("�������")]
				CHECKPROP_MAIN = 0x1,
			
				[EnumDescription("������Ϊ��ˣ���ͨ����Ӱ�����У�������ĳЩ��Ϊ���ޣ�")]
				CHECKPROP_SUB = 0x2,
			
		}

	
		public string szMemo;		/*״̬˵��*/
		};

	/*�豸ʹ�ù���ṹ*/
	public struct UNIRESVRULE
	{
		private Reserved reserved;
		
		public uint? dwRuleSN;		/*�豸������*/
	
		public string szRuleName;		/*�豸��������*/
	
		public uint? dwIdent;		/*��ݣ�0��ʾ�����ƣ�*/
	
		public uint? dwDeptID;		/*���ţ�0��ʾ�����ƣ�*/
	
		public uint? dwDevClass;		/*�豸���0��ʾ�����ƣ�*/
	
		public uint? dwDevKind;		/*�豸���ͣ�0��ʾ�����ƣ�*/
	
		public uint? dwDevID;		/*�豸ID��0��ʾ�����ƣ�*/
	
		public uint? dwGroupID;		/*ָ���û��飨0��ʾ�����ƣ�*/
	
		public uint? dwResvPurpose;		/*ԤԼ��;*/
	
		public uint? dwExtValue;		/*��ͬԤԼ���Ͷ������չֵ��0��ʾ�����ƣ�*/
	
		public uint? dwCreditRating;		/*���õȼ�*/
	
		public uint? dwPriority;		/*���ȼ�(���ִ�������ȼ���)*/
	
		public uint? dwLimit;		/*ԤԼ����*/
	
		[FlagsAttribute]
		public enum DWLIMIT : uint
		{
			
				[EnumDescription("������")]
				RESVLIMIT_FREE = 0x0,
			
				[EnumDescription("�����й���Ա����ʦ�ѽ��뷽��")]
				RESVLIMIT_NEEDMANAGER = 0x1,
			
				[EnumDescription("����̨ˢ���󷽿�")]
				RESVLIMIT_CONSULECARD = 0x2,
			
				[EnumDescription("��ʱ�뿪��ʱԤԼ�Ա���")]
				RESVLIMIT_LEAVEHOLD = 0x4,
			
				[EnumDescription("�踽�����뱨���")]
				RESVLIMIT_NEEDAPP = 0x8,
			
				[EnumDescription("�뿪ʱ����ˢ��")]
				RESVLIMIT_EXITCARD = 0x10,
			
				[EnumDescription("��ΥԼ���տɼ���ʹ��")]
				RESVLIMIT_DEFAULTCANUSE = 0x20,
			
				[EnumDescription("ӵ�������ԱȨ�޵�VIP�û�(�����ԤԼ�������ƣ���ͬʱԤԼ�������)")]
				RESVLIMIT_VIP = 0x40,
			
				[EnumDescription("ֻ��ԤԼĳ�����豸")]
				RESVLIMIT_DEVKIND = 0x100,
			
				[EnumDescription("ԤԼʱ����ѡ������豸")]
				RESVLIMIT_DEV = 0x200,
			
				[EnumDescription("ԤԼʱ������豸��ͻ(����ʵ�鲻��ʱ�Ŷӳ���)")]
				RESVLIMIT_NOCONFLICTCHECK = 0x400,
			
				[EnumDescription("�½����޸ģ�ɾ��ԤԼ����Ҫ�����ʼ�������")]
				RESVLIMIT_NONOTICE = 0x800,
			
		}

	
		public uint? dwEarlyInTime;		/*������ǰ����ʱ��(����)*/
	
		public uint? dwEarliestResvTime;		/*������ǰԤԼʱ��(����)�����ֱ������*/
	
		public uint? dwLatestResvTime;		/*�����ǰԤԼʱ��(����)�����ֱ�����С*/
	
		public uint? dwMinResvTime;		/*���ԤԼʱ��(����)*/
	
		public uint? dwMaxResvTime;		/*�ԤԼʱ��(����)*/
	
		public uint? dwResvEndNewTime;		/*��ǰԤԼ����ǰָ��ʱ��(����)�ڿ��½�ԤԼ*/
	
		public uint? dwResvBeforeNoticeTime;		/*ԤԼ��Ч��ǰ֪ͨʱ��(����)*/
	
		public uint? dwResvAfterNoticeTime;		/*ԤԼ��Ч����֪ͨʱ��(����)*/
	
		public uint? dwResvEndNoticeTime;		/*ԤԼ������ǰ֪ͨʱ��(����)*/
	
		public uint? dwSeriesTimeLimit;		/*����ԤԼʱ����(����)*/
	
		public uint? dwTimeLimitForPurpose;		/*ʱ������ص�ԤԼ����(������λ�������������ң����޼�)*/
	
		public uint? dwTimeConflictForPurpose;		/*ʱ���ͻ��ԤԼ����(������λ�������������ң����޼�ԤԼʱ�䲻���໥��ͻ)*/
	
		public uint? dwLatestSensorTime;		/*����Ƶ�ԤԼ������ʱ��(����)*/
	
		public uint? dwCancelTime;		/*ԤԼ�����Զ�ȡ��ԤԼʱ��(����)*/
	
		public uint? dwMinUseRate;		/*Ҫ�����ʹ����(%)*/
	
		public uint? dwFeeMode;		/*�շѷ�ʽ*/
	
		[FlagsAttribute]
		public enum DWFEEMODE : uint
		{
			
				[EnumDescription("��ʹ��ʱ���շ�")]
				FEEMODE_BYUSETIME = 1,
			
				[EnumDescription("����Ʒ���շ�")]
				FEEMODE_BYSAMPLE = 2,
			
				[EnumDescription("Э���շ�")]
				FEEMODE_BYNEGOTIATION = 4,
			
				[EnumDescription("��¼ʱѡ��")]
				FEEMODEBY_LOGONSEL = 0x10000,
			
				[EnumDescription("ʹ�ý�����������Ʒ��")]
				FEENEED_INPUTSAMPLE = 0x20000,
			
		}

	
		public uint? dwMaxDevKind;		/*��ԤԼ�豸����*/
	
		public uint? dwMaxDevNum;		/*��ԤԼ�豸��*/
	
		public string szOtherCons;		/*����Լ��������ʱ�����ƺ���Ҫ���豸��,��ʽ��ר���ļ����壩*/
	
	public RULECHECKINFO[] CheckTbl;		/*��˱�CUniTable[RULECHECKINFO]*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*��ȡ����ʵ�������*/
	public struct RESEARCHTESTREQ
	{
		private Reserved reserved;
		
		public uint? dwRTID;		/*����ʵ��ID*/
	
		public uint? dwHolderID;		/*�����ˣ��ʺţ�*/
	
		public uint? dwMemberID;		/*��Ա���ʺţ�*/
	
		public uint? dwLeaderID;		/*������ID*/
	
		public uint? dwDeptID;		/*����ID*/
	
		public string szRTName;		/*����ʵ������*/
	
		public uint? dwRTLevel;		/*���м���*/
	
		public uint? dwStatus;		/*״̬*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*����ʵ���Ա*/
	public struct RTMEMBER
	{
		private Reserved reserved;
		
		public uint? dwRTID;		/*����ʵ��ID*/
	
		public uint? dwGroupID;		/*��ID*/
	
		public uint? dwStatus;		/*��Ա״̬(GROUPMEMBER����)*/
	
		public uint? dwAccNo;		/*�˺�*/
	
		public string szTrueName;		/*��Ա����*/
		};

	/*����ʵ��*/
	public struct RESEARCHTEST
	{
		private Reserved reserved;
		
		public uint? dwRTID;		/*����ʵ��ID*/
	
		public string szRTSN;		/*����ʵ����*/
	
		public string szRTName;		/*����ʵ������*/
	
		public string szFromUnit;		/*�·���λ*/
	
		public uint? dwRTKind;		/*��������*/
	
		[FlagsAttribute]
		public enum DWRTKIND : uint
		{
			
				[EnumDescription("���п���")]
				RTKIND_RTASK = 0x1,
			
				[EnumDescription("���п���")]
				RTKIND_TTASK = 0x2,
			
				[EnumDescription("��ҵ����")]
				RTKIND_THESIS = 0x4,
			
				[EnumDescription("������")]
				RTKIND_OUTSIDE = 0x100,
			
				[EnumDescription("У�ڿ���")]
				RTKIND_INNER = 0x10000,
			
				[EnumDescription("У�����")]
				RTKIND_OUTER = 0x20000,
			
		}

	
		public uint? dwRTLevel;		/*���м���*/
	
		[FlagsAttribute]
		public enum DWRTLEVEL : uint
		{
			
				[EnumDescription("���Ҽ�")]
				RTLEVEL_NATIONAL = 1,
			
				[EnumDescription("ʡ����")]
				RTLEVEL_PROVINCE = 2,
			
				[EnumDescription("���ּ�")]
				RTLEVEL_DEPT = 3,
			
				[EnumDescription("У��")]
				RTLEVEL_SCHOOL = 4,
			
				[EnumDescription("����")]
				RTLEVEL_OTHER = 0x1000,
			
		}

	
		public uint? dwBeginDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��ֹ����*/
	
		public uint? dwHolderID;		/*�����ˣ��ʺţ�*/
	
		public string szHolderName;		/*����������*/
	
		public uint? dwLeaderID;		/*������ID*/
	
		public string szLeaderName;		/*����������*/
	
		public uint? dwDeptID;		/*����ID*/
	
		public string szDeptName;		/*����*/
	
		public uint? dwTestTimes;		/*ʵ�����*/
	
		public uint? dwTestMinutes;		/*ʵ���ۼ�ʱ��*/
	
		public uint? dwBalance;		/*�������*/
	
		public uint? dwTotalFee;		/*�ۼƷ���*/
	
		public uint? dwUnpayFee;		/*δ�������*/
	
		public uint? dwGroupID;		/*��ID*/
	
		public uint? dwGroupUsers;		/*���Ա����*/
	
		public uint? dwStatus;		/*״̬��ǰ8����������CHECKINFO����Ĺ���Ա���״̬)*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("�ѹر�")]
				RTSTAT_CLOSED = 0x100,
			
		}

	
	public RTMEMBER[] RTMembers;		/*��Ա��ϸ��(CUniTable<RTMEMBER>)*/
	
		public string szOtherCons;		/*����Լ��������ʱ�����ƺ���Ҫ���豸��,��ʽ��ר���ļ����壩*/
	
		public string szFundsNo;		/*���ѿ���ţ�����ö��Ÿ���)*/
	
		public string szMemo;		/*��ע*/
		};

	/*��ȡ��Ʒ��Ϣ�����*/
	public struct SAMPLEINFOREQ
	{
		private Reserved reserved;
		
		public uint? dwSampleSN;		/*��Ʒ���*/
	
		public string szSampleName;		/*��Ʒ����*/
	
		public uint? dwSamStat;		/*��Ʒ״̬*/
	
		public uint? dwDevID;		/*�豸ID����ȡĳ�豸ר�ã�*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*��Ʒ��Ϣ*/
	public struct SAMPLEINFO
	{
		private Reserved reserved;
		
		public uint? dwSampleSN;		/*��Ʒ���*/
	
		public string szSampleName;		/*��Ʒ����*/
	
		public string szUnitName;		/*�Ʒѵ�λ*/
	
		public uint? dwUnitFee1;		/*����1*/
	
		public uint? dwUnitFee2;		/*����2*/
	
		public uint? dwUnitFee3;		/*����3*/
	
		public uint? dwUnitFee4;		/*����4*/
	
		public uint? dwUnitFee5;		/*����5*/
	
		public uint? dwSamStat;		/*��Ʒ״̬*/
	
		[FlagsAttribute]
		public enum DWSAMSTAT : uint
		{
			
				[EnumDescription("δ����")]
				SAMSTAT_INUSE = 0x1,
			
				[EnumDescription("δ����")]
				SAMSTAT_UNUSE = 0x100,
			
		}

	
		public uint? dwDevID;		/*�豸ID����һ�豸ר�ø�ֵ�豸ID��ͨ��Ϊ0��*/
	
		public string szMemo;		/*��ע*/
		};

	/*��ȡ����ԤԼ�������*/
	public struct YARDRESVREQ
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*ԤԼ��*/
	
		public uint? dwApplicantID;		/*�������˺�*/
	
		public uint? dwDevID;		/*�豸ID*/
	
		public uint? dwDevKind;		/*�豸����*/
	
		public uint? dwClassKind;		/*���(��UNIDEVCLS��Kind����)*/
	
		public uint? dwClassID;		/*�豸���ID*/
	
		public uint? dwCheckStat;		/*ȷ��״̬*/
	
		public uint? dwUnNeedStat;		/*������״̬*/
	
		public uint? dwBeginDate;		/*ԤԼ��ʼ����*/
	
		public uint? dwEndDate;		/*ԤԼ��������*/
	
		public uint? dwResvGroupID;		/*ԤԼ��ID*/
	
		public uint? dwStatFlag;		/*״̬��־*/
	
		public string szLabIDs;		/*ʵ����ID,����ö��Ÿ���*/
	
		public string szRoomIDs;		/*����ID,����ö��Ÿ���*/
	
		public string szBuildingIDs;		/*¥��ID,����ö��Ÿ���*/
	
		public string szCampusIDs;		/*У��ID,����ö��Ÿ���*/
	
		public uint? dwActivitySN;		/*����ͱ��*/
	
		public uint? dwProperty;		/*����*/
	
		public uint? dwUnNeedProperty;		/*����Ҫ����*/
	
		public string szResvName;		/*ʹ����;����*/
	
		public uint? dwReqProp;		/*���󸽼�����*/
	
		[FlagsAttribute]
		public enum DWREQPROP : uint
		{
			
				[EnumDescription("ֻҪ��ԤԼ��dwResvID=dwResvGroupID)")]
				YARDREQ_ONLYMAINRESV = 0x1,
			
		}

	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*����ԤԼ*/
	public struct YARDRESV
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*ԤԼ��*/
	
		public uint? dwResvGroupID;		/*��ID(��ʱ��ԤԼ��ID��ͬ������ΪԤԼID)*/
	
		public string szResvName;		/*ʹ����;����*/
	
		public uint? dwPurpose;		/*ԤԼ��;*/
	
		public uint? dwProperty;		/*ԤԼ���ԣ�������Ҫ����*/
	
		public uint? dwActivitySN;		/*����ͱ��*/
	
		public string szActivityName;		/*���������*/
	
		public string szOrganization;		/*��֯*/
	
		public string szOrganiger;		/*��֯��*/
	
		public string szHostUnit;		/*���쵥λ*/
	
		public string szPresenter;		/*������*/
	
		public string szDesiredUser;		/*������Ҫ��*/
	
		public string szContact;		/*��ϵ��*/
	
		public string szTel;		/*��ϵ�绰*/
	
		public string szHandPhone;		/*��ϵ�ֻ�*/
	
		public string szEmail;		/*��ϵ��������*/
	
		public uint? dwKind;		/*����*/
	
		public string szIntroInfo;		/*�����*/
	
		public string szCycRule;		/*ԤԼʱ���������*/
	
		public uint? dwActivityLevel;		/*����𣨺͹���Ա����һ�£�*/
	
		public uint? dwCheckKinds;		/*�������(�ο�CHECKTYPE���壬�ɶ����*/
	
		public uint? dwSecurityLevel;		/*�������𣨲ο�CHECKTYPE�������Ƿ��ύ��������ˣ�*/
	
		public uint? dwMinAttendance;		/*���ٲμ�������Ԥ����*/
	
		public uint? dwMaxAttendance;		/*���μ�������Ԥ����*/
	
		public uint? dwStatus;		/*ԤԼ״̬(������飬�Ƿ���Ч���Ƿ���ȡ����)*/
	
		public uint? dwPreDate;		/*ԤԼ��ʼ����*/
	
		public uint? dwOccurTime;		/*ԤԼ����ʱ��*/
	
		public uint? dwBeginTime;		/*ԤԼ��ʼʱ��*/
	
		public uint? dwEndTime;		/*ԤԼ����ʱ��*/
	
		public uint? dwCheckTime;		/*���ʱ��(���½�ԤԼָ��RESVPROP_BYTHIRDʱ����ʾdwThirdResvID)*/
	
		public uint? dwAdvanceCheckTime;		/*��ǰ���ʱ��*/
	
		public uint? dwDevID;		/*�豸ID*/
	
		public uint? dwDevKind;		/*�豸����*/
	
		public uint? dwDevSN;		/*�豸���*/
	
		public string szDevName;		/*�豸����*/
	
		public uint? dwRoomID;		/*����ID*/
	
		public string szRoomName;		/*��������*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabName;		/*ʵ��������*/
	
		public uint? dwBuildingID;		/*¥��ID*/
	
		public string szBuildingNo;		/*��¥���(*/
	
		public string szBuildingName;		/*��¥����(*/
	
		public uint? dwCampusID;		/*У��ID*/
	
		public string szCampusName;		/*У������*/
	
		public uint? dwDeptID;		/*������������ID*/
	
		public string szDeptName;		/*����������������*/
	
		public uint? dwApplicantID;		/*�������˺�*/
	
		public string szApplicantName;		/*����������*/
	
		public uint? dwUserDeptID;		/*�����˲���ID*/
	
		public string szUserDeptName;		/*�����˲���*/
	
		public uint? dwResvRuleSN;		/*����ԤԼ����*/
	
		public uint? dwOpenRuleSN;		/*��������ʱ���*/
	
		public uint? dwFeeSN;		/*����SN*/
	
		public string szApplicationURL;		/*�ύ�������������*/
	
		public string szSpareDevIDs;		/*��ѡ�豸ID��������Ÿ�����*/
	
		public string szMemo;		/*˵����Ϣ*/
	
		public uint? dwFeedStat;		/*״̬*/
	
		public uint? dwFeedKind;		/*��������*/
	
		public uint? dwScore;		/*�û�����*/
	
		public string szFeedInfo;		/*������Ϣ*/
	
		public string szReplyInfo;		/*�ظ���Ϣ*/
		};

	/*��ȡ����ԤԼ�����Ϣ�������*/
	public struct YARDRESVCHECKINFOREQ
	{
		private Reserved reserved;
		
		public uint? dwCheckID;		/*���ID*/
	
		public uint? dwResvID;		/*ԤԼ��*/
	
		public uint? dwResvGroupID;		/*ԤԼ��ID*/
	
		public string szResvName;		/*ԤԼ����*/
	
		public uint? dwCheckDeptID;		/*��˲���ID*/
	
		public uint? dwApplicantID;		/*�������˺�*/
	
		public uint? dwCheckStat;		/*ȷ��״̬*/
	
		public uint? dwNeedYardResv;		/*��Ҫ��ȡ����ԤԼ����*/
	
		public uint? dwBeginDate;		/*ԤԼ��ʼ����*/
	
		public uint? dwEndDate;		/*ԤԼ��������*/
	
		public uint? dwKind;		/*����ԤԼ����*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*����ԤԼ�����Ϣ*/
	public struct YARDRESVCHECKINFO
	{
		private Reserved reserved;
		
		public uint? dwCheckID;		/*���ID*/
	
		public uint? dwResvID;		/*ԤԼ��*/
	
		public uint? dwCheckKind;		/*�������*/
	
		public uint? dwCheckLevel;		/*��˼���(ͬUNIADMIN.dwManLevel���壩*/
	
		public uint? dwCheckDeptID;		/*��˲���ID*/
	
		public uint? dwWaitKind;		/*�ȴ�������(�ɶ����*/
	
		public string szCheckName;		/*�������*/
	
		public uint? dwBeforeKind;		/*��������������(�ɶ����*/
	
		public uint? dwNeedMinTime;		/*�����Ҫ�����ʱ��*/
	
		public uint? dwCheckStat;		/*����Ա���״̬(������ADMINCHECK)*/
	
		public string szCheckDetail;		/*���˵��*/
	
		public uint? dwCheckBeginDate;		/*��˿�ʼ����*/
	
		public uint? dwCheckDeadLine;		/*��˽�ֹ����*/
	
		public uint? dwCheckDate;		/*�������*/
	
		public uint? dwCheckTime;		/*���ʱ��*/
	
		public uint? dwAdminID;		/*������ʺ�*/
	
		public string szAdminName;		/*�����*/
	
		public uint? dwApplicantID;		/*�������˺�*/
	
		public string szApplicantName;		/*����������*/
	
	public YARDRESV YardResv;		/*CUniStruct[YARDRESV]*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*����ԤԼ���*/
	public struct YARDRESVCHECK
	{
		private Reserved reserved;
		
		public uint? dwCheckID;		/*���ID*/
	
		public uint? dwResvID;		/*ԤԼ��*/
	
		public uint? dwCheckKind;		/*�������*/
	
		public uint? dwCheckStat;		/*����Ա���״̬(������ADMINCHECK)*/
	
		public string szCheckDetail;		/*���˵��*/
	
	public YARDRESV YardResv;		/*CUniStruct[YARDRESV]*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*��ȡԤԼ�����Ϣ�������*/
	public struct RESVCHECKINFOREQ
	{
		private Reserved reserved;
		
		public uint? dwCheckID;		/*���ID*/
	
		public uint? dwResvID;		/*ԤԼ��*/
	
		public uint? dwCheckDeptID;		/*��˲���ID*/
	
		public uint? dwApplicantID;		/*�������˺�*/
	
		public uint? dwCheckStat;		/*ȷ��״̬*/
	
		public uint? dwNeedResv;		/*��Ҫ��ȡԤԼ����*/
	
		public uint? dwBeginDate;		/*ԤԼ��ʼ����*/
	
		public uint? dwEndDate;		/*ԤԼ��������*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*ԤԼ�����Ϣ*/
	public struct RESVCHECKINFO
	{
		private Reserved reserved;
		
		public uint? dwCheckID;		/*���ID*/
	
		public uint? dwResvID;		/*ԤԼ��*/
	
		public uint? dwCheckKind;		/*�������*/
	
		public uint? dwCheckLevel;		/*��˼���(ͬUNIADMIN.dwManLevel���壩*/
	
		public uint? dwCheckDeptID;		/*��˲���ID*/
	
		public uint? dwWaitKind;		/*�ȴ�������(�ɶ����*/
	
		public string szCheckName;		/*�������*/
	
		public uint? dwBeforeKind;		/*��������������(�ɶ����*/
	
		public uint? dwNeedMinTime;		/*�����Ҫ�����ʱ��*/
	
		public uint? dwCheckStat;		/*����Ա���״̬(������ADMINCHECK)*/
	
		public string szCheckDetail;		/*���˵��*/
	
		public uint? dwCheckBeginDate;		/*��˿�ʼ����*/
	
		public uint? dwCheckDeadLine;		/*��˽�ֹ����*/
	
		public uint? dwCheckDate;		/*�������*/
	
		public uint? dwCheckTime;		/*���ʱ��*/
	
		public uint? dwAdminID;		/*������ʺ�*/
	
		public string szAdminName;		/*�����*/
	
		public uint? dwApplicantID;		/*�������˺�*/
	
		public string szApplicantName;		/*����������*/
	
	public UNIRESERVE ResvInfo;		/*CUniStruct[UNIRESERVE]*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*ԤԼ���*/
	public struct RESVCHECK
	{
		private Reserved reserved;
		
		public uint? dwCheckID;		/*���ID*/
	
		public uint? dwResvID;		/*ԤԼ��*/
	
		public uint? dwCheckKind;		/*�������*/
	
		public uint? dwCheckStat;		/*����Ա���״̬(������ADMINCHECK)*/
	
		public string szCheckDetail;		/*���˵��*/
	
	public UNIRESERVE ResvInfo;		/*CUniStruct[UNIRESERVE]*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*��ȡ���ݻ�������*/
	public struct YARDACTIVITYREQ
	{
		private Reserved reserved;
		
		public uint? dwActivitySN;		/*����ͱ��*/
	
		public uint? dwActivityLevel;		/*����𣨺͹���Ա����һ�£�*/
	
		public uint? dwCheckKinds;		/*�������(�ο�CHECKTYPE���壬�ɶ����*/
	
		public uint? dwSecurityLevel;		/*�������𣨲ο�CHECKTYPE�������Ƿ��ύ��������ˣ�*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*���ݻ�����豸����*/
	public struct YADEVKIND
	{
		private Reserved reserved;
		
		public uint? dwActivitySN;		/*����ͱ��*/
	
		public uint? dwKindID;		/*�豸����ID*/
		};

	/*���ݻ*/
	public struct YARDACTIVITY
	{
		private Reserved reserved;
		
		public uint? dwActivitySN;		/*����ͱ��*/
	
		public string szActivityName;		/*���������*/
	
		public uint? dwActivityLevel;		/*����𣨺͹���Ա����һ�£�*/
	
		public uint? dwCheckKinds;		/*�������(�ο�CHECKTYPE���壬�ɶ����*/
	
		public string szCheckNames;		/*�����������,����ö��Ÿ���*/
	
		public uint? dwSecurityLevel;		/*�������𣨲ο�CHECKTYPE�������Ƿ��ύ��������ˣ�*/
	
		[FlagsAttribute]
		public enum DWSECURITYLEVEL : uint
		{
			
				[EnumDescription("����Ҫ���")]
				SECLEVEL_NONEED = 0x1,
			
				[EnumDescription("��Ҫ���")]
				SECLEVEL_CHECK = 0x2,
			
				[EnumDescription("��Ҫ����������")]
				SECLEVEL_HELP = 0x4,
			
		}

	
	public YADEVKIND[] UsableDevKind;		/*CUniTable[YADEVKIND]*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*������ԤԼ�豸*/
	public struct TRESVDEV
	{
		private Reserved reserved;
		
		public string szAssertSN;		/*�ʲ����*/
		};

	/*������ԤԼʱ���*/
	public struct TRESVTIME
	{
		private Reserved reserved;
		
		public uint? dwResvDate;		/*ԤԼ����*/
	
		public uint? dwStartHM;		/*ԤԼ��ʼʱ��*/
	
		public uint? dwEndHM;		/*ԤԼ����ʱ��*/
		};

	/*������ԤԼ�����豸*/
	public struct THIRDRESVSHAREDEV
	{
		private Reserved reserved;
		
		public uint? dwThirdResvID;		/*������ԤԼID*/
	
		public string szResvTitle;		/*ԤԼ����*/
	
	public TRESVDEV[] DevTbl;		/*ԤԼ�豸��*/
	
	public TRESVTIME[] TimeTbl;		/*ԤԼʱ���*/
		};

	/*������ɾ��ԤԼ*/
	public struct THIRDRESVDEL
	{
		private Reserved reserved;
		
		public uint? dwThirdResvID;		/*������ԤԼID*/
		};

	/*��ȡ������ԤԼ�������*/
	public struct THIRDRESVREQ
	{
		private Reserved reserved;
		
		public uint? dwThirdResvID;		/*������ԤԼID*/
	
		public string szPID;		/*ѧ����*/
	
		public uint? dwStatus;		/*״̬*/
	
		public uint? dwBeginDate;		/*ԤԼ��ʼ����*/
	
		public uint? dwEndDate;		/*ԤԼ��������*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*������ԤԼ*/
	public struct THIRDRESV
	{
		private Reserved reserved;
		
		public uint? dwThirdResvID;		/*������ԤԼID*/
	
		public string szResvTitle;		/*ԤԼ����*/
	
		public uint? dwResvDate;		/*ԤԼ����*/
	
		public uint? dwStartHM;		/*ԤԼ��ʼʱ��*/
	
		public uint? dwEndHM;		/*ԤԼ����ʱ��*/
	
		public string szOrganization;		/*��֯*/
	
		public string szOrganiger;		/*��֯��*/
	
		public string szHostUnit;		/*���쵥λ*/
	
		public string szPresenter;		/*������*/
	
		public string szDesiredUser;		/*������Ҫ��*/
	
		public string szIntroInfo;		/*�����*/
	
		public string szPID;		/*������ѧ����*/
	
		public string szTrueName;		/*����������*/
	
		public string szTel;		/*��ϵ�绰*/
	
		public string szHandPhone;		/*��ϵ�ֻ�*/
	
		public string szEmail;		/*��ϵ��������*/
	
		public uint? dwMinAttendance;		/*���ٲμ�������Ԥ����*/
	
		public uint? dwMaxAttendance;		/*���μ�������Ԥ����*/
	
		public uint? dwStatus;		/*ԤԼ״̬((0��ʾδԤԼ��������飬�Ƿ���Ч���Ƿ���ȡ����)*/
	
		public string szAssertSN;		/*�ʲ����*/
	
		public uint? dwResvID;		/*ԤԼ��*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*�豸ԤԼ��Ϣ��*/
	public struct DEVICERESV
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*�豸ID*/
	
		public uint? dwResvFrom;		/*��Դ*/
	
		[FlagsAttribute]
		public enum DWRESVFROM : uint
		{
			
				[EnumDescription("���Ա�ϵͳ")]
				RESVFROM_SYS = 0x1,
			
				[EnumDescription("���Ե�����ϵͳ")]
				RESVFROM_THIRD = 0x100,
			
		}

	
		public uint? dwResvID;		/*ԤԼID*/
	
		public uint? dwResvDate;		/*ԤԼ����*/
	
		public uint? dwStartHM;		/*ԤԼ��ʼʱ��*/
	
		public uint? dwEndHM;		/*ԤԼ����ʱ��*/
	
		public uint? dwResvMin;		/*ԤԼʱ��*/
	
		public uint? dwAccNo;		/*ԤԼ���ʺ�*/
	
		public uint? dwSex;		/*ԤԼ���Ա�*/
	
		public string szPID;		/*������ѧ����*/
	
		public string szTrueName;		/*����������*/
	
		public string szMemberName;		/*����*/
	
		public string szResvTitle;		/*ԤԼ����*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/

	/*��ȡ��ط����������*/
	public struct CTRLCLASSREQ
	{
		private Reserved reserved;
		
		public uint? dwCtrlSN;		/*��ط������*/
	
		public uint? dwCtrlKind;		/*���Ʒ���*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*��ط����ṹ*/
	public struct UNICTRLCLASS
	{
		private Reserved reserved;
		
		public uint? dwCtrlSN;		/*��ط������*/
	
		public uint? dwCtrlKind;		/*���Ʒ���*/
	
		[FlagsAttribute]
		public enum DWCTRLKIND : uint
		{
			
				[EnumDescription("��ַ")]
				CTRLKIND_URL = 0x1,
			
				[EnumDescription("���������")]
				CTRLKIND_SW = 0x2,
			
		}

	
		public uint? dwCtrlLevel;		/*���Ʒ��༶�𣬿��Զ���*/
	
		public string szCtrlName;		/*��ط��������*/
	
		public uint? dwCtrlMode;		/*���Ʒ�ʽ*/
	
		[FlagsAttribute]
		public enum DWCTRLMODE : uint
		{
			
				[EnumDescription("ȫ������(������)")]
				CTRLMODE_NOLIMIT = 0,
			
				[EnumDescription("��ֹ")]
				CTRLMODE_FORBID = 1,
			
				[EnumDescription("����")]
				CTRLMODE_PERMIT = 2,
			
				[EnumDescription("ȫ����ֹ")]
				CTRLMODE_FORBIDALL = 4,
			
				[EnumDescription("��ֹ������ָ������")]
				CTRLMODE_LEVEL = 0x100,
			
				[EnumDescription("��ֹ������ָ���鼰���ڼ���")]
				CTRLMODE_CLASSLEVEL = 0x200,
			
				[EnumDescription("ֻ��ֹ������ָ����")]
				CTRLMODE_CLASS = 0x400,
			
		}

	
		public uint? dwForAges;		/*���������(FFTT 0713��ʾ7-13����)*/
	
		public string szMemo;		/*��ע*/
		};

	/*��ȡ��ַ��������*/
	public struct CTRLURLREQ
	{
		private Reserved reserved;
		
		public uint? dwCtrlLevel;		/*���Ʒ��༶�𣬿��Զ���*/
	
		public uint? dwClassSN;		/*��ط������*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*��ַ��ṹ*/
	public struct UNICTRLURL
	{
		private Reserved reserved;
		
		public uint? dwClassSN;		/*��ط������*/
	
		public uint? dwCtrlLevel;		/*���Ʒ��༶�𣬿��Զ���*/
	
		public string szCtrlName;		/*��ַ������*/
	
		public uint? dwCtrlMode;		/*���Ʒ�ʽ*/
	
		public uint? dwForAges;		/*���������(FFTT 0713��ʾ7-13����)*/
	
		public uint? dwID;		/*��ַID*/
	
		public string szURL;		/*URL(֧��ͨ���)*/
	
		public uint? dwPort;		/*�˿�*/
	
		public uint? dwStatus;		/*״̬*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("��Ч")]
				URLSTAT_UNVALID = 0,
			
				[EnumDescription("δ���")]
				URLSTAT_UNCHECK = 1,
			
				[EnumDescription("�����")]
				URLSTAT_CHECKED = 2,
			
		}

	
		public string szMemo;		/*��ע*/
		};

	/*��ȡ�����������*/
	public struct CTRLSWREQ
	{
		private Reserved reserved;
		
		public uint? dwCtrlLevel;		/*���Ʒ��༶�𣬿��Զ���*/
	
		public uint? dwClassSN;		/*��ط������*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*�����ṹ*/
	public struct UNICTRLSW
	{
		private Reserved reserved;
		
		public uint? dwClassSN;		/*��ط������*/
	
		public uint? dwCtrlLevel;		/*���Ʒ��༶�𣬿��Զ���*/
	
		public string szCtrlName;		/*���������*/
	
		public uint? dwCtrlMode;		/*���Ʒ�ʽ*/
	
		public uint? dwForAges;		/*���������(FFTT 0713��ʾ7-13����)*/
	
		public uint? dwID;		/*key*/
	
		public string szName;		/*������Ա����*/
	
		public uint? dwMemberID;		/*���ݲ�ͬ����ʾ��ͬ����*/
	
		public uint? dwKind;		/*��Ա����*/
	
		[FlagsAttribute]
		public enum DWKIND : uint
		{
			
				[EnumDescription("����")]
				CSWKIND_PROGRAM = 0x2,
			
				[EnumDescription("������")]
				CSWKIND_SWCLASS = 0x4,
			
		}

	
		public uint? dwStatus;		/*״̬*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("��Ч")]
				SWSTAT_UNVALID = 0,
			
				[EnumDescription("δ���")]
				SWSTAT_UNCHECK = 1,
			
				[EnumDescription("�����")]
				SWSTAT_CHECKED = 2,
			
		}

	
		public string szMemo;		/*��ע*/
		};

	/*��ȡ����������*/
	public struct SOFTWAREREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*��ʽ*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("ȫ��")]
				SOFTWAREGET_BYALL = 0,
			
				[EnumDescription("���ID")]
				SOFTWAREGET_BYID = 1,
			
				[EnumDescription("�����(֧��ͨ��)")]
				SOFTWAREGET_BYNAME = 2,
			
				[EnumDescription("�������")]
				SOFTWAREGET_BYKIND = 4,
			
				[EnumDescription("���±�־")]
				SOFTWAREGET_BYCHGFLAG = 8,
			
		}

	
		public string szGetKey;		/*����ֵ*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*����ṹ*/
	public struct UNISOFTWARE
	{
		private Reserved reserved;
		
		public uint? dwSWID;		/*���ID*/
	
		public uint? dwKind;		/*��Ա����*/
	
		[FlagsAttribute]
		public enum DWKIND : uint
		{
			
				[EnumDescription("ϵͳ���")]
				SWKIND_OS = 0x10000,
			
				[EnumDescription("Ӧ�����")]
				SWKIND_APP = 0x20000,
			
				[EnumDescription("�������")]
				SWKIND_TOOL = 0x40000,
			
				[EnumDescription("��Ϸ���")]
				SWKIND_GAME = 0x80000,
			
				[EnumDescription("����(δ����)")]
				SWKIND_OTHER = 0x100000,
			
				[EnumDescription("������Ա��װ")]
				SWKIND_INSTALLED = 0x10000000,
			
		}

	
		public string szSWName;		/*�������*/
	
		public string szSWVersion;		/*����汾*/
	
		public string szSWCompany;		/*��˾*/
	
		public string szDispSWName;		/*��ʾ��Ʒ���ƣ����޸�*/
	
		public string szDispSWCompany;		/*��ʾ��˾���ƣ����޸�*/
	
		public uint? dwFrom;		/*�������ͬ����Σ�*/
	
		public uint? dwChgFlag;		/*�޸ĸ��±�־*/
	
		public string szMemo;		/*��ע*/
		};

	/*��ȡ����������*/
	public struct PROGRAMREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*��ʽ*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("ȫ��")]
				PROGRAMGET_BYALL = 0,
			
				[EnumDescription("����ID")]
				PROGRAMGET_BYID = 1,
			
				[EnumDescription("������(֧��ͨ��)")]
				PROGRAMGET_BYNAME = 2,
			
				[EnumDescription("���±�־")]
				PROGRAMGET_BYCHGFLAG = 8,
			
				[EnumDescription("���ID")]
				PROGRAMGET_BYSWID = 16,
			
				[EnumDescription("�����(֧��ͨ��)")]
				PROGRAMGET_BYSWNAME = 32,
			
		}

	
		public string szGetKey;		/*����ֵ*/
	
		public uint? dwKind;		/*��Ա����*/
	
		public uint? dwProperty;		/*��������*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*����ṹ*/
	public struct UNIPROGRAM
	{
		private Reserved reserved;
		
		public uint? dwID;		/*����ID*/
	
		public uint? dwSubID;		/*��ID*/
	
		public uint? dwSWID;		/*�������ID*/
	
		public uint? dwKind;		/*�������*/
	
		public uint? dwProperty;		/*��������*/
	
		[FlagsAttribute]
		public enum DWPROPERTY : uint
		{
			
				[EnumDescription("������")]
				PGPROP_MAIN = 1,
			
				[EnumDescription("��������")]
				PGPROP_ASSIST = 2,
			
		}

	
		public string szProductName;		/*��Ʒ����*/
	
		public string szExeName;		/*Exe�ļ���*/
	
		public string szSWVersion;		/*����汾*/
	
		public string szDispProductName;		/*��ʾ�������ƣ����޸�*/
	
		public string szDispSWName;		/*��ʾ��Ʒ���ƣ����޸�*/
	
		public string szDispSWCompany;		/*��ʾ��˾���ƣ����޸�*/
	
		public uint? dwFrom;		/*�������ͬ����Σ�*/
	
		public uint? dwChgFlag;		/*�޸ĸ��±�־*/
	
		public string szIcon;		/*ͼ��*/
	
		public string szMemo;		/*��ע*/
		};

	/*��ȡ��������������*/
	public struct PCSWINFOREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*��ʽ*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("����ID")]
				PCSWGET_BYPCID = 1,
			
		}

	
		public string szGetKey;		/*����ֵ*/
	
		public uint? dwKind;		/*�������*/
	
		public uint? dwProperty;		/*��������*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*��������ṹ*/
	public struct UNIPCSWINFO
	{
		private Reserved reserved;
		
		public string szProgramInfo;		/*CUniStruct(<UNIPROGRAM>)*/
	
		public uint? dwPCID;		/*����ID*/
	
		public string szInstName;		/*��װ����*/
	
		public string szInstPath;		/*��װ·��*/
	
		public uint? dwRunLatestDate;		/*�����������*/
	
		public uint? dwRunTimes;		/*���д���*/
	
		public uint? dwRunMinutes;		/*�ۼ����з�����*/
	
		public string szMemo;		/*��ע*/
		};

	/*��ȡ��������������*/
	public struct ROOMSWINFOREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*��ʽ*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("����ID����ȡ�û����¶���װ�����")]
				ROOMSWGET_BYID = 1,
			
		}

	
		public string szGetKey;		/*����ֵ*/
	
		public uint? dwKind;		/*�������*/
	
		public uint? dwProperty;		/*��������*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*��������ṹ*/
	public struct UNIROOMSWINFO
	{
		private Reserved reserved;
		
		public string szProgramInfo;		/*CUniStruct(<UNIPROGRAM>)*/
	
		public uint? dwRoomID;		/*����ID*/
	
		public uint? dwInstSWNum;		/*��װ�����������*/
	
		public uint? dwRunTimes;		/*���д���*/
	
		public uint? dwRunMinutes;		/*�ۼ����з�����*/
	
		public string szMemo;		/*��ע*/
		};

	/*��������ṹ���ϴ��ã�*/
	public struct PCPROGRAM
	{
		private Reserved reserved;
		
		public string szProgramInfo;		/*CUniStruct(<UNIPROGRAM>)*/
	
		public uint? dwDevID;		/*�ͻ����豸��ID��*/
	
		public uint? dwLabID;		/*ʵ���ҵ�ID��*/
	
		public uint? dwPID;		/*����ID*/
	
		public string szInstName;		/*��װ����*/
	
		public string szInstPath;		/*��װ·��*/
	
		public string szMemo;		/*��ע*/
		};

	/*�������������Ϣ���ϴ��ã�*/
	public struct PROGEND
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*�ͻ����豸��ID��*/
	
		public uint? dwLabID;		/*ʵ���ҵ�ID��*/
	
		public uint? dwProcID;		/*�����ID��*/
	
		public uint? dwPID;		/*����ID*/
		};

	/*�˳�������Ϣ*/
	public struct QUITAPPINFO
	{
		private Reserved reserved;
		
		public uint? dwProcID;		/*����ID*/
	
		public string szMemo;		/*��ע*/
		};

	/*��ַ��Ϣ*/
	public struct URLCHECKINFO
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*�ͻ����豸��ID��*/
	
		public uint? dwLabID;		/*ʵ���ҵ�ID��*/
	
		public uint? dwAccNo;		/*�ʺ�*/
	
		public uint? dwRemoteIp;		/*����IP*/
	
		public uint? dwPort;		/*���ʶ˿�*/
	
		public string szDomainName;		/*����*/
	
		public string szURL;		/*��ַ*/
	
		public string szMemo;		/*��ע*/
		};

	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/

	/*��¼�����*/
	public struct THIRDLOGINREQ
	{
		private Reserved reserved;
		
		public uint? dwSysID;		/*ϵͳ���*/
	
		public string szVersion;		/*�汾*/
	
		public string szExtInfo;		/*��չ��Ϣ*/
		};

	/*��¼Ӧ���*/
	public struct THIRDLOGINRES
	{
		private Reserved reserved;
		
		public string szVersion;		/*�汾*/
	
		public string szExtInfo;		/*��չ��Ϣ*/
		};

	/*��ȡ�˻��б��������*/
	public struct THIRDACCREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*��ʽ*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("��ȡ�����ʻ���Ϣ")]
				ACCGET_BYALL = 0x100,
			
				[EnumDescription("����ͬ��")]
				ACCGET_BYSYNC = 0x200,
			
		}

	
		public string szParam;		/*����*/
		};

	/*ͬ���ʻ������*/
	public struct SYNCACCREQ
	{
		private Reserved reserved;
		
		public uint? dwType;		/*ͬ����ʽ*/
	
		[FlagsAttribute]
		public enum DWTYPE : uint
		{
			
				[EnumDescription("ͬ���ʻ�")]
				SYNCTYPE_DO = 0x1,
			
				[EnumDescription("��ѯͬ���ʻ�״̬")]
				SYNCTYPE_QUERY = 0x2,
			
		}

	
		public string szMemo;		/*��չ��Ϣ*/
		};

	/*��ȡ�����ʻ���Ϣ״̬*/
	public struct SYNCACCINFO
	{
		private Reserved reserved;
		
		public uint? dwStatus;		/*��ǰ״̬*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("׼����ͬ��")]
				SYNCSTAT_PREPARE = 0x0,
			
				[EnumDescription("�ȴ��ӿڷ���������")]
				SYNCSTAT_WAITING = 0x1,
			
				[EnumDescription("���ڴ����ؽ��")]
				SYNCSTAT_DOING = 0x2,
			
				[EnumDescription("�������")]
				SYNCSTAT_DONE = 0x4,
			
				[EnumDescription("����")]
				SYNCSTAT_ERROR = SYNCSTAT_DONE+0x08 ,
			
		}

	
		public uint? dwStartTime;		/*��ʼʱ��(time����)*/
	
		public uint? dwUseTime;		/*����ʱ��(��)*/
	
		public uint? dwEstmateTime;		/*����������ʱ��(��)*/
	
		public uint? dwTotalAcc;		/*���û���*/
	
		public uint? dwDealAcc;		/*�Ѵ�������û���*/
	
		public uint? dwDiffAcc;		/*��Ϣ�뱾�ز�ͬ�û���*/
	
		public string szInfo;		/*��չ��Ϣ*/
		};

	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/

	/**/
	public struct PERIODFEE
	{
		private Reserved reserved;
		
		public uint? dwPStart;		/*ʱ�ο�ʼʱ��*/
	
		public uint? dwPEnd;		/*ʱ�ν���ʱ��*/
	
		public uint? dwPUnitFee;		/*ʱ�ε�λ����*/
	
		public uint? dwPAssistFee;		/*ʱ�ι���Աָ����*/
		};

	/**/
	public struct FEEREQ
	{
		private Reserved reserved;
		
		public uint? dwFeeSN;		/*����SN*/
	
		public uint? dwIdent;		/*��ݣ�0��ʾ�����ƣ�*/
	
		public uint? dwDeptID;		/*���ţ�0��ʾ�����ƣ�*/
	
		public uint? dwDevKind;		/*�豸���ͣ�0��ʾ�����ƣ�*/
	
		public uint? dwGroupID;		/*ָ���û��飨0��ʾ�����ƣ�*/
	
		public uint? dwPurpose;		/*��;*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/**/
	public struct RTDEVFEEREQ
	{
		private Reserved reserved;
		
		public uint? dwRTID;		/*����ʵ����ĿID*/
	
		public uint? dwDevID;		/*�豸ID*/
		};

	/*�շѱ�׼��ϸ��Ϣ*/
	public struct FEEDETAIL
	{
		private Reserved reserved;
		
		public uint? dwFeeType;		/*�շ����*/
	
		[FlagsAttribute]
		public enum DWFEETYPE : uint
		{
			
				[EnumDescription("ʹ�÷ѣ�ʹ�ú����ɵ��˵���")]
				FEETYPE_USEDEV = 1,
			
				[EnumDescription("ռ�÷�")]
				FEETYPE_OCCUPY = 2,
			
				[EnumDescription("Э����")]
				FEETYPE_ASSIST = 3,
			
				[EnumDescription("��ʱ��")]
				FEETYPE_TIMEOUT = 4,
			
				[EnumDescription("�⳥��")]
				FEETYPE_DAMAGE = 5,
			
				[EnumDescription("����")]
				FEETYPE_FINE = 6,
			
				[EnumDescription("��Ʒ��")]
				FEETYPE_SAMPLE = 7,
			
				[EnumDescription("ԤԼʹ���豸�ѣ���ɷѺ󷽿�ʹ�ã�")]
				FEETYPE_RESVDEV = 8,
			
				[EnumDescription("�����")]
				FEETYPE_ENTRUST = 9,
			
				[EnumDescription("Э���շ�")]
				FEETYPE_NEGOTIATION = 10,
			
		}

	
		public uint? dwUsablePayKind;		/*���ýɷѷ�ʽ(��UNIBILL����)*/
	
		public uint? dwDefaultCheckStat;		/*CHECKINFO����Ĺ���Ա���״̬*/
	
		public uint? dwUnitFee;		/*��λʹ�÷���(Сʱ ȱʡ100)*/
	
		public uint? dwUnitTime;		/*��λʱ��(ȱʡ1)*/
	
		public uint? dwRoundOff;		/*����ֽ��(С�ڵ�λʱ��)*/
	
		public uint? dwIgnoreTime;		/*���Ʒ�ʱ��(ȱʡ0,����ʾ���Ʒ�ʱ�䣬����ʾ����ʹ��ʱ��)*/
	
		public uint? dwHolidayCoef;		/*����ϵ��*/
	
		public string szPosInfo;		/*��һ��ͨ��Ӧ���̻���Ϣ*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*�շѱ�׼��ϸ��Ϣ*/
	public struct UNIFEE
	{
		private Reserved reserved;
		
		public uint? dwFeeSN;		/*����SN*/
	
		public string szFeeName;		/*����*/
	
		public uint? dwPriority;		/*���ȼ�(���ִ�������ȼ���)*/
	
		public uint? dwIdent;		/*��ݣ�0��ʾ�����ƣ�*/
	
		public uint? dwDeptID;		/*���ţ�0��ʾ�����ƣ�*/
	
		public uint? dwDevKind;		/*�豸���ͣ�0��ʾ�����ƣ�*/
	
		public uint? dwGroupID;		/*ָ���û��飨0��ʾ�����ƣ�*/
	
		public uint? dwPurpose;		/*ԤԼ��;*/
	
		public uint? dwOverDraft;		/*����͸֧��*/
	
		public uint? dwMinInTime;		/*���������û���Ϳ���ʱ��*/
	
		public uint? dwQuotaRule;		/*���ƹ���(���ۼƣ����ۼƣ�����æ��(ȱʡ0))*/
	
		[FlagsAttribute]
		public enum DWQUOTARULE : uint
		{
			
				[EnumDescription("���ۼ�")]
				FEEQUOTA_BYDAY = 0x1,
			
				[EnumDescription("���ۼ�")]
				FEEQUOTA_BYTIMES = 0x2,
			
				[EnumDescription("����æ��Ч")]
				FEEQUOTA_ONLYBUSY = 0x10000,
			
		}

	
		public uint? dwQuotaTime;		/*����ʹ��ʱ��(ȱʡ-1)*/
	
	public FEEDETAIL[] szFeeDetail;		/*�շѱ�׼��ϸ��CUniTable[FEEDETAIL]*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/**/
	public struct RTDEVSAMPLEREQ
	{
		private Reserved reserved;
		
		public uint? dwRTID;		/*����ʵ����ĿID*/
	
		public uint? dwDevID;		/*�豸ID*/
		};

	/*������Ŀ��Ӧ���豸����Ʒ�����ʱ�*/
	public struct RTDEVSAMPLE
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*�豸ID*/
	
		public uint? dwSampleSN;		/*��Ʒ���*/
	
		public string szSampleName;		/*��Ʒ����*/
	
		public string szUnitName;		/*�Ʒѵ�λ*/
	
		public uint? dwUnitFee;		/*����*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*��ȡ�˵�����*/
	public struct BILLREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*��ʽ*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("ȫ��")]
				BILLGET_BYALL = 0,
			
				[EnumDescription("SID")]
				BILLGET_BYSID = 1,
			
				[EnumDescription("����SID")]
				BILLGET_BYCOSTSID = 2,
			
				[EnumDescription("�˺�")]
				BILLGET_BYACCNO = 3,
			
				[EnumDescription("ʵ����ID")]
				BILLGET_BYLABID = 4,
			
				[EnumDescription("�豸����")]
				BILLGET_BYDEVKIND = 5,
			
				[EnumDescription("ԤԼ��")]
				BILLGET_BYRESVID = 6,
			
		}

	
		public string szGetKey;		/*����ֵ*/
	
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
		public uint? dwFeeType;		/*�շ����(FEEDETAIL����)*/
	
		public uint? dwPayKind;		/*�ɷѷ�ʽ*/
	
		public uint? dwStatus;		/*״̬*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*�û��˵�*/
	public struct UNIBILL
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*��ˮ��*/
	
		public uint? dwResvID;		/*ԤԼ��*/
	
		public uint? dwCostSID;		/*ʹ����ˮ��*/
	
		public string szPosInfo;		/*��һ��ͨ��Ӧ���̻���Ϣ*/
	
		public uint? dwAccNo;		/*�ʺ�*/
	
		public string szPID;		/*ѧ����*/
	
		public string szTrueName;		/*����*/
	
		public uint? dwDevKind;		/*�豸����*/
	
		public string szKindName;		/*�豸����*/
	
		public string szModel;		/*�豸�ͺ�*/
	
		public string szSpecification;		/*�豸���*/
	
		public uint? dwFeeType;		/*�շ����(FEEDETAIL����)*/
	
		public uint? dwBeginTime;		/*��ʼʱ��*/
	
		public uint? dwEndTime;		/*����ʱ��*/
	
		public uint? dwUnitFee;		/*����*/
	
		public uint? dwUnitTime;		/*��λʱ��*/
	
		public uint? dwRoundOff;		/*����ֽ��(С�ڵ�λʱ��)*/
	
		public uint? dwIgnoreTime;		/*���Ʒ�ʱ��*/
	
		public uint? dwHolidayCoef;		/*����ϵ��*/
	
		public uint? dwUseTime;		/*ʹ��ʱ��*/
	
		public uint? dwFeeTime;		/*�Ʒ�ʱ��*/
	
		public uint? dwCostMoney;		/*Ӧ�ɷ���*/
	
		public uint? dwCostSubsidy;		/*����*/
	
		public uint? dwCostFreeTime;		/*��ʱ*/
	
		public uint? dwRealCost;		/*ʵ�ʽ��ɷ���*/
	
		public uint? dwUsablePayKind;		/*���ýɷѷ�ʽ*/
	
		[FlagsAttribute]
		public enum DWUSABLEPAYKIND : uint
		{
			
				[EnumDescription("һ��ͨ")]
				PAYKIND_ONECARD = 0x1000000,
			
				[EnumDescription("Ԥ���ֽ�")]
				PAYKIND_STOREDCASH = 0x2000000,
			
				[EnumDescription("�����ֽ�")]
				PAYKIND_DIRECTCASH = 0x4000000,
			
				[EnumDescription("ʹ�ò���")]
				PAYKIND_SUBSIDY = 0x8000000,
			
				[EnumDescription("У��֧Ʊ")]
				PAYKIND_CHECK = 0x10000000,
			
		}

	
		public uint? dwUsedPayKind;		/*ʵ�ʽɷѷ�ʽ*/
	
		public uint? dwStatus;		/*CHECKINFO����Ĺ���Ա���״̬+���¶���*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("δ֧��")]
				BILLSTAT_UNPAID = 0x100,
			
				[EnumDescription("��֧��")]
				BILLSTAT_PAID = 0x200,
			
				[EnumDescription("�ѽ���")]
				BILLSTAT_CHECKOUT = 0x400,
			
				[EnumDescription("�����Զ�֧�����豾��ͨ�������ɷ�̨��ɷѵ�֧��")]
				BILLSTAT_NOAUTO = 0x800,
			
		}

	
		public uint? dwBillDate;		/*�˵�����*/
	
		public uint? dwBillTime;		/*�˵�ʱ��*/
	
		public uint? dwAuditorID;		/*���Ա*/
	
		public uint? dwTollID;		/*�շ�Ա��һ��ͨtblThirdSyncCost����ˮ��*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*�˵��ɷ�*/
	public struct BILLPAY
	{
		private Reserved reserved;
		
		public uint? dwPayKind;		/*�ɷѷ�ʽ*/
	
		public uint? dwTotalCost;		/*�ɷѺϼ�*/
	
		public uint? dwOneCardSID;		/*һ��ͨ��ˮ��*/
	
		public string szCardCostInfo;		/*��ͨ���۷���Ϣ����ͬ��һ��ͨ��ʽ�����ݶ���ͬ*/
	
	public UNIBILL[] szBillInfo;		/*CUniTable[UNIBILL]*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*��ʱʹ�ù�������*/
	public struct FTRULEREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*��ʽ*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("ȫ��")]
				FTRULEGET_BYALL = 0,
			
				[EnumDescription("FTRuleSN")]
				FTRULEGET_BYSN = 1,
			
				[EnumDescription("��ʱ���")]
				FTRULEGET_BYFTTYPE = 2,
			
				[EnumDescription("רҵ")]
				FTRULEGET_BYMAJOR = 3,
			
		}

	
		public string szGetKey;		/*����ֵ*/
	
		public string szSubKey;		/*ѡ��רҵ��ʽʱ������Ϊ��ѧ���*/
		};

	/*��ʱʹ�ù���*/
	public struct FREETIMERULE
	{
		private Reserved reserved;
		
		public uint? dwSN;		/*��ʱ����SN*/
	
		public string szName;		/*����*/
	
		public uint? dwFTType;		/*��ʱ���*/
	
		public uint? dwMajorID;		/*רҵ*/
	
		public string szMajorName;		/*רҵ����*/
	
		public uint? dwEnrolYear;		/*��ѧ���*/
	
		public uint? dwPeriod;		/*���ڣ�ѧ�ڣ�ѧ�꣬������ѧ�ڼ䣩*/
	
		[FlagsAttribute]
		public enum DWPERIOD : uint
		{
			
				[EnumDescription("ѧ��")]
				FTPERIOD_TERM = 1,
			
				[EnumDescription("ѧ��")]
				FTPERIOD_YEAR = 2,
			
				[EnumDescription("��Уֱ����ҵ")]
				FTPERIOD_GRADUATE = 3,
			
		}

	
		public uint? dwPlanFT;		/*�ƻ���ʱ��*/
	
		public uint? dwDayLimit;		/*ÿ��ʹ���޶�*/
	
		public uint? dwPlanUseTimes;		/*�ƻ���ʹ�ô���*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/

	/*��ȡ����̨�������*/
	public struct CONREQ
	{
		private Reserved reserved;
		
		public uint? dwConsoleSN;		/*����̨���*/
	
		public string szConsoleName;		/*����̨����*/
	
		public uint? dwKind;		/*����̨����*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*����̨��Ϣ*/
	public struct UNICONSOLE
	{
		private Reserved reserved;
		
		public uint? dwConsoleSN;		/*����̨���*/
	
		public string szConsoleName;		/*����̨����*/
	
		public uint? dwKind;		/*����̨����*/
	
		[FlagsAttribute]
		public enum DWKIND : uint
		{
			
				[EnumDescription("����һ�����ˢ����֤���ֳ�ԤԼ����Ϣ������")]
				CONKIND_TOUCHONE = 1,
			
				[EnumDescription("��ʾ����(���ܽ�����ֻ�ܷ�����Ϣ��")]
				CONKIND_DISPLAY = 2,
			
				[EnumDescription("��ʦ��(�����ʦ�������)")]
				CONKIND_TEACHER = 4,
			
				[EnumDescription("����")]
				CONKIND_LOAN = 8,
			
				[EnumDescription("ֵ��̨")]
				CONKIND_ATTENDANT = 0x10,
			
				[EnumDescription("ͨ����")]
				CONKIND_AUTOGATE = 0x20,
			
				[EnumDescription("ԤԼ̨")]
				CONKIND_RESERVE = 0x1000,
			
				[EnumDescription("�е�¼��֤���豸�����ԣ�ԤԼ")]
				CONKIND_LOGINRESV = 0x2000,
			
				[EnumDescription("�˵�����")]
				CONKIND_BILLCHECKOUT = 0x4000,
			
				[EnumDescription("ԤԼ�ռ�")]
				CONKIND_COMMONS = 0x100000,
			
				[EnumDescription("ԤԼ����")]
				CONKIND_COMPUTER = 0x200000,
			
				[EnumDescription("ԤԼ��λ")]
				CONKIND_SEAT = 0x400000,
			
				[EnumDescription("��ͨ��������(����ͨ����ˢ������󷽿�ʹ��)")]
				CONKIND_WITHAG = 0x800000,
			
		}

	
		public uint? dwStatus;		/*����̨״̬���ο�CommonIF.xmlCONSTAT_XXX����)*/
	
		public uint? dwOpenTime;		/*��ʼʱ��*/
	
		public uint? dwCloseTime;		/*�ر�ʱ��*/
	
		public string szIP;		/*IP��ַ*/
	
		public string szManRooms;		/*������(�����ţ��ɶ�������Ÿ���)*/
	
		public string szDispInfoURL;		/*��ʾ��Ϣ����*/
	
		public string szLocation;		/*����̨���λ��*/
	
		public string szMemo;		/*˵����Ϣ*/
	
	public MODMONI MoniInfo;		/*�����Ϣ*/
		};

	/*����̨��¼����*/
	public struct CONLOGINREQ
	{
		private Reserved reserved;
		
		public string szVersion;		/*�汾	XX.XX.XXXXXXXX*/
	
		public uint? dwStaSN;		/*վ����*/
	
		public string szIP;		/*IP��ַ*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*����̨��¼��Ӧ*/
	public struct CONLOGINRES
	{
		private Reserved reserved;
		
		public uint? dwSessionID;		/*�����������SessionID*/
	
	public UNIVERSION SrvVer;		/*UNIVERSION �ṹ*/
	
		public string szCurTime;		/*������ʱ�� YYYY-MM-DD HH:MM:SS*/
	
		public uint? dwConsoleSN;		/*����̨���*/
	
		public string szConsoleName;		/*����̨����*/
	
		public uint? dwKind;		/*����̨����*/
	
		public uint? dwOpenTime;		/*����ʱ��*/
	
		public uint? dwCloseTime;		/*�ر�ʱ��*/
	
		public string szDispInfoURL;		/*��ʾ��Ϣ����*/
	
		public string szMemo;		/*˵����Ϣ*/
	
		public string szManRooms;		/*������(�����ţ�������Ÿ���)*/
	
	public UNIDEVICE[] szManDevs;		/*�����豸�б�CUniTable[UNIDEVICE]*/
		};

	/*����̨�˳�����*/
	public struct CONLOGOUTREQ
	{
		private Reserved reserved;
		
		public uint? dwConsoleSN;		/*����̨���*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*����̨��ʱͨ������*/
	public struct CONPULSEREQ
	{
		private Reserved reserved;
		
		public uint? dwConsoleSN;		/*����̨���*/
	
		public uint? dwStatus;		/*����̨״̬*/
	
		public string szStatInfo;		/*״̬��Ϣ*/
		};

	/*����̨��ʱͨ����Ӧ*/
	public struct CONPULSERES
	{
		private Reserved reserved;
		
		public uint? dwChanged;		/*����̨�Ƿ��Ѹ���*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*����̨��ʾ��Ϣ�ṹ*/
	public struct CONMESSAGE
	{
		private Reserved reserved;
		
		public uint? dwMsgKind;		/*��Ϣ����*/
	
		[FlagsAttribute]
		public enum DWMSGKIND : uint
		{
			
				[EnumDescription("��ʾ��Ϣ��MsgInfoΪ�ַ���")]
				CONMSG_INFO = 0x1,
			
				[EnumDescription("������Ϣ��MsgInfoΪ�ַ���")]
				CONMSG_WARNING = 0x2,
			
				[EnumDescription("������Ϣ��MsgInfoΪ�ַ���")]
				CONMSG_ERROR = 0x4,
			
				[EnumDescription("�Ž�ˢ��,MsgInfoΪCUniStruct[DOORCARDRES]")]
				CONMSG_DOORCARD = 0x100,
			
				[EnumDescription("Զ�̻���,MsgInfoΪCUniStruct[UNIDEVICE]")]
				CONMSG_WAKEUP = 0x200,
			
		}

	
		public string MsgInfo;		/*��Ϣ���ݣ����ݲ�ͬ�����Ͷ�Ӧ��ͬ������*/
		};

	/*����̨ˢ�������û���Ϣ*/
	public struct CONUSERINFO
	{
		private Reserved reserved;
		
		public uint? dwUserStat;		/*�û�״̬*/
	
		[FlagsAttribute]
		public enum DWUSERSTAT : uint
		{
			
				[EnumDescription("û��ԤԼ")]
				CONUSERSTAT_NORESV = 1,
			
				[EnumDescription("��ԤԼ")]
				CONUSERSTAT_RESV = 2,
			
				[EnumDescription("ʹ����")]
				CONUSERSTAT_INUSE = 4,
			
				[EnumDescription("��δ֧���˵�")]
				CONUSERSTAT_BILL = 8,
			
				[EnumDescription("ǩ���ɹ�")]
				CONUSERSTAT_SIGNOK = 0x10,
			
				[EnumDescription("ϵͳδ������ʱ��")]
				CONUSERSTAT_SYSUNOPEN = 0x20,
			
				[EnumDescription("ϵͳ�ѹ�����ʱ��")]
				CONUSERSTAT_SYSCLOSED = 0x40,
			
				[EnumDescription("ˢ���˳�")]
				CONUSERSTAT_EXIT = 0x80,
			
				[EnumDescription("�ȴ�����ˢ��")]
				CONUSERSTAT_WAITEXITCARD = 0x100,
			
				[EnumDescription("�ȴ�ǩ��")]
				CONUSERSTAT_WAITSIGN = 0x200,
			
		}

	
	public UNIACCOUNT AccInfo;		/*UNIACCOUNT �ṹ*/
	
	public UNIRESERVE ResvInfo;		/*UNIRESERVE �ṹ*/
	
	public UNIDEVICE DevInfo;		/*UNIDEVICE �ṹ*/
	
	public UNIBILL[] BillInfo;		/*�˵���(CUniTable<UNIBILL>)*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*����̨��ʦ��¼������Ϣ*/
	public struct CONTEACHERINFO
	{
		private Reserved reserved;
		
	public UNIACCOUNT AccInfo;		/*UNIACCOUNT �ṹ*/
	
	public UNIRESERVE ResvInfo;		/*UNIRESERVE �ṹ*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*ˢ���ϻ�����*/
	public struct CARDFORPCREQ
	{
		private Reserved reserved;
		
		public uint? dwDevKind;		/*�豸����(Ϊ���ɺ�̨�Զ����䣩*/
	
		public uint? dwLabID;		/*ʵ����ID��(Ϊ���ɺ�̨�Զ����䣩*/
	
		public uint? dwRoomID;		/*��ѡ�����ID��(Ϊ���ɺ�̨�Զ����䣩*/
	
		public uint? dwDevID;		/*�ͻ����豸��ID��(Ϊ���ɺ�̨�Զ����䣩*/
	
	public ACCCHECKREQ CheckReq;		/*(ACCCHECKREQ�ṹ)*/
		};

	/*ˢ���ϻ�Ӧ��*/
	public struct CARDFORPCRES
	{
		private Reserved reserved;
		
		public uint? dwMode;		/*��������*/
	
		[FlagsAttribute]
		public enum DWMODE : uint
		{
			
				[EnumDescription("ˢ���ϻ�(ExtInfo����UNIRESERVE�ṹ)")]
				CARDMODE_IN = 0x1,
			
				[EnumDescription("ˢ���»�(ExtInfo����UNIBILL��)")]
				CARDMODE_OUT = 0x2,
			
				[EnumDescription("���˽���(ExtInfo����UNIBILL��)")]
				CARDMODE_DEALMONEY = 0x4,
			
		}

	
	public byte[] ExtInfo;		/*���ݲ�ͬ�ķ������Ͷ�Ӧ��ͬ������*/
		};

	/*ͨ����ˢ������*/
	public struct AUTOGATECARDREQ
	{
		private Reserved reserved;
		
		public uint? dwCardMode;		/*����ˢ��*/
	
		[FlagsAttribute]
		public enum DWCARDMODE : uint
		{
			
				[EnumDescription("����ˢ��")]
				AUTOGATECARD_IN = 1,
			
				[EnumDescription("��ȥˢ��")]
				AUTOGATECARD_OUT = 2,
			
		}

	
		public string szLogonName;		/*��¼��(ѧ���ţ�*/
	
		public string szCardNo;		/*����*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*ͨ����ˢ����Ӧ*/
	public struct AUTOGATECARDRES
	{
		private Reserved reserved;
		
		public string szTrueName;		/*����*/
	
		public string szInfo;		/*��ʾ��Ϣ*/
		};

	/*����̨ˢ������*/
	public struct CONUSERINREQ
	{
		private Reserved reserved;
		
		public uint? dwInType;		/*��������*/
	
		[FlagsAttribute]
		public enum DWINTYPE : uint
		{
			
				[EnumDescription("��ʱ�뿪����")]
				CONUSERIN_GOBACK = 1,
			
		}

	
		public uint? dwResvID;		/*ԤԼID��*/
	
		public uint? dwLabID;		/*ʵ����ID��*/
	
		public uint? dwDevID;		/*�ͻ����豸��ID��*/
	
		public uint? dwDevKind;		/*�豸����ID*/
	
		public uint? dwEndTime;		/*ʹ�ý���ʱ��*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*����̨ˢ������*/
	public struct CONUSERINRES
	{
		private Reserved reserved;
		
	public UNIRESERVE ResvInfo;		/*UNIRESERVE �ṹ*/
	
	public UNIDEVICE DevInfo;		/*UNIDEVICE �ṹ*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*����̨ˢ���˳�����*/
	public struct CONUSEROUTREQ
	{
		private Reserved reserved;
		
		public uint? dwOutType;		/*�뿪����*/
	
		[FlagsAttribute]
		public enum DWOUTTYPE : uint
		{
			
				[EnumDescription("��ʱ�뿪")]
				CONUSEROUT_LEAVE = 1,
			
		}

	
		public uint? dwLabID;		/*ʵ����ID��*/
	
		public uint? dwDevID;		/*�ͻ����豸��ID��*/
		};

	/*����̨ˢ���˳�Ӧ��*/
	public struct CONUSEROUTRES
	{
		private Reserved reserved;
		
	public UNIACCTINFO AcctInfo;		/*ʹ����Ϣ��UNIACCTINFO �ṹ*/
	
	public UNIBILL[] BillInfo;		/*�˵���(CUniTable<UNIBILL>)*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*�ֻ�ɨ������*/
	public struct MOBILESCANREQ
	{
		private Reserved reserved;
		
		public string szMSN;		/*MSN*/
	
		public string szLogonName;		/*��¼��*/
	
		public string szPassword;		/*����*/
	
		public string szIP;		/*IP��ַ*/
	
		public uint? dwProperty;		/*��չ����*/
	
		[FlagsAttribute]
		public enum DWPROPERTY : uint
		{
			
				[EnumDescription("��֤�ɹ���΢�ź�")]
				MSCANPROP_BINDMSN = 1,
			
		}

	
		public uint? dwStaSN;		/*վ����*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public uint? dwDevID;		/*�豸ID*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*�ֻ�ɨ����Ӧ*/
	public struct MOBILESCANRES
	{
		private Reserved reserved;
		
		public uint? dwSessionID;		/*�����������SessionID*/
	
		public uint? dwUserStat;		/*�û�״̬*/
	
		[FlagsAttribute]
		public enum DWUSERSTAT : uint
		{
			
				[EnumDescription("ǩ���ɹ�(�������������")]
				MSUSERSTAT_SIGNOK = 0x1,
			
				[EnumDescription("�豸����(ʹ�������MSREQ_MOBILE_USERIN��")]
				MSUSERSTAT_IDLE = 0x2,
			
				[EnumDescription("ʹ����(�ɵ���MSREQ_MOBILE_USEROUT���к���������")]
				MSUSERSTAT_INUSE = 0x4,
			
				[EnumDescription("ˢ���˳��ɹ����������������")]
				MSUSERSTAT_EXIT = 0x8,
			
				[EnumDescription("��ʱ�뿪���سɹ�(�������������")]
				MSUSERSTAT_GOBACK = 0x10,
			
				[EnumDescription("ԤԼδ��Ч(�������������")]
				MSUSERSTAT_RESVUNDO = 0x20,
			
				[EnumDescription("����Լ(��ʹ����һ�𷵻أ��������MSREQ_MOBILE_DELAY��")]
				MSUSERSTAT_CANDELAY = 0x40,
			
		}

	
		public uint? dwMinUseMin;		/*����ʹ��ʱ��(����)*/
	
		public uint? dwMaxUseMin;		/*�ʹ��ʱ��(����)*/
	
		public string szPID;		/*ѧ����*/
	
		public string szTrueName;		/*����*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public uint? dwDevID;		/*�豸ID*/
	
		public string szDevName;		/*�豸����*/
	
		public string szDispInfo;		/*��ʾ��Ϣ*/
		};

	/*�ֻ���ʼʹ������*/
	public struct MOBILEUSERINREQ
	{
		private Reserved reserved;
		
		public uint? dwUseMin;		/*ʹ��ʱ��(����)*/
	
		public string szMemo;		/*��ע*/
		};

	/*�ֻ�������Ӧ*/
	public struct MOBILEUSERINRES
	{
		private Reserved reserved;
		
		public string szDispInfo;		/*��ʾ��Ϣ*/
		};

	/*�ֻ���ʼʹ������*/
	public struct MOBILEDELAYREQ
	{
		private Reserved reserved;
		
		public uint? dwDelayMin;		/*�ӳ�ʱ��(����)*/
	
		public string szMemo;		/*��ע*/
		};

	/*�ֻ�������Ӧ*/
	public struct MOBILEDELAYRES
	{
		private Reserved reserved;
		
		public string szDispInfo;		/*��ʾ��Ϣ*/
		};

	/*�ֻ��˳�����*/
	public struct MOBILEUSEROUTREQ
	{
		private Reserved reserved;
		
		public uint? dwOutType;		/*�뿪����*/
	
		[FlagsAttribute]
		public enum DWOUTTYPE : uint
		{
			
				[EnumDescription("��ʱ�뿪")]
				MSUSEROUT_LEAVE = 1,
			
				[EnumDescription("����ʹ��")]
				MSUSEROUT_EXIT = 2,
			
				[EnumDescription("��ʱʹ��")]
				MSUSEROUT_DELAY = 4,
			
		}

	
		public uint? dwDelayMin;		/*��ʱʱ��(����)*/
	
		public string szMemo;		/*��ע*/
		};

	/*�ֻ��˳���Ӧ*/
	public struct MOBILEUSEROUTRES
	{
		private Reserved reserved;
		
		public string szDispInfo;		/*��ʾ��Ϣ*/
		};

	/*�ֻ���¼ǩ������*/
	public struct RESVUSERCOMEINREQ
	{
		private Reserved reserved;
		
		public uint? dwInType;		/*��������*/
	
		[FlagsAttribute]
		public enum DWINTYPE : uint
		{
			
				[EnumDescription("��ʱ�뿪����")]
				RESVUSERIN_GOBACK = 1,
			
		}

	
		public uint? dwResvID;		/*ԤԼID��*/
	
		public uint? dwLabID;		/*ʵ����ID��*/
	
		public uint? dwDevID;		/*�ͻ����豸��ID��*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*�ֻ���¼ǩ����Ӧ*/
	public struct RESVUSERCOMEINRES
	{
		private Reserved reserved;
		
		public string szDispInfo;		/*��ʾ��Ϣ*/
		};

	/*�ֻ���¼��ʱ����*/
	public struct RESVUSERDELAYREQ
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*ԤԼID��*/
	
		public uint? dwLabID;		/*ʵ����ID��*/
	
		public uint? dwDevID;		/*�ͻ����豸��ID��*/
	
		public uint? dwMaxDelayMin;		/*����ӳ�ʱ��(����)*/
	
		public string szMemo;		/*��ע*/
		};

	/*�ֻ���¼��ʱ��Ӧ*/
	public struct RESVUSERDELAYRES
	{
		private Reserved reserved;
		
		public string szDispInfo;		/*��ʾ��Ϣ*/
		};

	/*�ֻ���¼�˳�����*/
	public struct RESVUSERGOOUTREQ
	{
		private Reserved reserved;
		
		public uint? dwOutType;		/*�뿪����*/
	
		[FlagsAttribute]
		public enum DWOUTTYPE : uint
		{
			
				[EnumDescription("��ʱ�뿪")]
				RESVUSEROUT_LEAVE = 1,
			
				[EnumDescription("����ʹ��")]
				RESVUSEROUT_EXIT = 2,
			
		}

	
		public uint? dwResvID;		/*ԤԼID��*/
	
		public uint? dwLabID;		/*ʵ����ID��*/
	
		public uint? dwDevID;		/*�ͻ����豸��ID��*/
	
		public string szMemo;		/*��ע*/
		};

	/*�ֻ���¼�˳���Ӧ*/
	public struct RESVUSERGOOUTRES
	{
		private Reserved reserved;
		
		public string szDispInfo;		/*��ʾ��Ϣ*/
		};

	/*ҡһҡǩ������*/
	public struct SHAKECHECKINREQ
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*ԤԼID��*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*ҡһҡǩ��Ӧ��*/
	public struct SHAKECHECKINRES
	{
		private Reserved reserved;
		
		public string szDispInfo;		/*��ʾ��Ϣ*/
		};

	/*ҡһҡ��������*/
	public struct SHAKECOMEINREQ
	{
		private Reserved reserved;
		
		public uint? dwRoomID;		/*RoomID����չ��*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*ҡһҡ����Ӧ��*/
	public struct SHAKECOMEINRES
	{
		private Reserved reserved;
		
		public string szDispInfo;		/*��ʾ��Ϣ*/
		};

	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/

	/*��ȡԤԼ��¼*/
	public struct RESVRECREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*��ʽ*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("ȫ��")]
				RESVRECGET_BYALL = 0,
			
				[EnumDescription("ResvID")]
				RESVRECGET_BYID = 1,
			
				[EnumDescription("�豸ID")]
				RESVRECGET_BYDEVID = 2,
			
				[EnumDescription("����ID")]
				RESVRECGET_BYROOMID = 3,
			
				[EnumDescription("ʵ����ID")]
				RESVRECGET_BYLABID = 4,
			
				[EnumDescription("ʵ��ƻ�ID")]
				RESVRECGET_BYTESTPLANID = 5,
			
				[EnumDescription("ʵ����ĿID")]
				RESVRECGET_BYTESTITEMID = 6,
			
		}

	
		public string szGetKey;		/*����ֵ*/
	
		public uint? dwAccNo;		/*�˺�*/
	
		public uint? dwUseMode;		/*ʹ��ģʽ*/
	
		public uint? dwPurpose;		/*ԤԼ��;*/
	
		public uint? dwDevKind;		/*�豸����*/
	
		public uint? dwStatus;		/*״̬*/
	
		public uint? dwCheckStat;		/*���״̬*/
	
		public uint? dwCommentStat;		/*����״̬*/
	
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*�豸ԤԼ��Ϣ*/
	public struct UNIRESVREC
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*��ˮ��*/
	
		public uint? dwResvID;		/*ԤԼ��*/
	
		public uint? dwPreDate;		/*ԤԼ����*/
	
		public uint? dwPreBegin;		/*ԤԼ��ʼʱ��*/
	
		public uint? dwPreEnd;		/*ԤԼ����ʱ��*/
	
		public uint? dwUseMode;		/*ʹ��ģʽ*/
	
		public uint? dwPurpose;		/*ԤԼ��;*/
	
		public uint? dwAccNo;		/*�˺�*/
	
		public string szPID;		/*ѧ����*/
	
		public string szTrueName;		/*����*/
	
		public uint? dwDevID;		/*�豸ID*/
	
		public uint? dwDevKind;		/*�豸����*/
	
		public uint? dwDevSN;		/*�豸���*/
	
		public string szDevName;		/*�豸����*/
	
		public uint? dwRoomID;		/*����ID*/
	
		public string szRoomName;		/*��������*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabName;		/*ʵ��������*/
	
		public uint? dwStatus;		/*״̬*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("ȱϯ")]
				RESVRECSTAT_ABSENT = 0x1,
			
				[EnumDescription("����")]
				RESVRECSTAT_SICK = 0x2,
			
				[EnumDescription("�¼�")]
				RESVRECSTAT_PRIVATE = 0x4,
			
				[EnumDescription("������")]
				RESVRECSTAT_ATTENDDOING = 0x8,
			
				[EnumDescription("δǩ��")]
				RESVRECSTAT_UNSIGN = 0x10,
			
				[EnumDescription("��ǩ��")]
				RESVRECSTAT_SIGNED = 0x20,
			
				[EnumDescription("�ѵ�¼")]
				RESVRECSTAT_LOGINED = 0x40,
			
				[EnumDescription("δˢ���뿪")]
				RESVRECSTAT_LEAVENOCARD = 0x80,
			
				[EnumDescription("��ϯ")]
				RESVRECSTAT_ATTEND = 0x100,
			
				[EnumDescription("�ٵ�")]
				RESVRECSTAT_LATE = 0x200,
			
				[EnumDescription("����")]
				RESVRECSTAT_LEAVE = 0x400,
			
				[EnumDescription("ʹ��ʱ�䲻���")]
				RESVRECSTAT_USELESS = 0x800,
			
				[EnumDescription("�Ѻ���")]
				RESVRECSTAT_ADJUST = 0x10000,
			
				[EnumDescription("�ѽ���")]
				RESVRECSTAT_CHECKOUT = 0x20000,
			
		}

	
		public uint? dwResvTime;		/*ԤԼ��ʱ��*/
	
		public uint? dwUseTime;		/*ʹ����ʱ��*/
	
		public uint? dwCheckStat;		/*���״̬*/
	
		public uint? dwCommentStat;		/*�û�����״̬*/
	
		[FlagsAttribute]
		public enum DWCOMMENTSTAT : uint
		{
			
				[EnumDescription("�û�δ����")]
				RCSTAT_UNDO = 0x1,
			
				[EnumDescription("�û�������")]
				RCSTAT_DONE = 0x2,
			
		}

	
		public uint? dwTotalFee;		/*���úϼ�*/
	
		public string szMemo;		/*��ע*/
	
		public uint? dwInTime;		/*ǩ��ʱ��*/
	
		public uint? dwInMode;		/*ǩ����ʽ*/
	
		[FlagsAttribute]
		public enum DWINMODE : uint
		{
			
				[EnumDescription("����̨")]
				RCMODE_CONSOLE = 0x1,
			
				[EnumDescription("ͨ����")]
				RCMODE_AG = 0x2,
			
				[EnumDescription("�ֻ�")]
				RCMODE_HP = 0x4,
			
				[EnumDescription("������λ����豸")]
				RCMODE_MONITOR = 0x8,
			
				[EnumDescription("���Ե�¼")]
				RCMODE_PC = 0x10,
			
				[EnumDescription("�Ž�ˢ��")]
				RCMODE_DOOR = 0x20,
			
				[EnumDescription("�Զ�ǩ��")]
				RCMODE_AUTO = 0x40,
			
				[EnumDescription("����Ա")]
				RCMODE_ADMIN = 0x80,
			
		}

	
		public uint? dwOutTime;		/*�˳�ʱ��*/
	
		public uint? dwOutMode;		/*�˳���ʽ*/
	
		public uint? dwLeaveTime;		/*��ʱ�뿪ʱ��*/
	
		public uint? dwLeaveMode;		/*��ʱ�뿪��ʽ*/
	
		public uint? dwBackTime;		/*����ʱ��*/
	
		public uint? dwBackMode;		/*���ط�ʽ*/
		};

	/*��ȡԤԼ����ͳ�Ƽ�¼*/
	public struct RESVKINDSTATREQ
	{
		private Reserved reserved;
		
		public uint? dwPurpose;		/*ԤԼ��;*/
	
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*ԤԼ����ͳ��*/
	public struct RESVKINDSTAT
	{
		private Reserved reserved;
		
		public uint? dwKind;		/*ԤԼ����*/
	
		public uint? dwResvTimes;		/*ԤԼ����*/
	
		public uint? dwResvMinutes;		/*ԤԼ��ʱ��(����)*/
	
		public uint? dwTestHour;		/*ʵ��ѧʱ��*/
	
		public uint? dwResvDevs;		/*ԤԼ������*/
	
		public uint? dwUseDevs;		/*ʵ���û���*/
	
		public uint? dwResvUsers;		/*�Ͽ�������*/
	
		public uint? dwRealUsers;		/*ʵ�ʵ�������*/
		};

	/*��ȡԤԼ��ʽͳ�Ƶ������*/
	public struct RESVMODESTATREQ
	{
		private Reserved reserved;
		
		public uint? dwOwner;		/*ԤԼ��(������)*/
	
		public uint? dwMemberID;		/*��ԱID*/
	
		public uint? dwDevID;		/*�豸ID*/
	
		public uint? dwUseMode;		/*ʹ�÷���*/
	
		public uint? dwPurpose;		/*ԤԼ��;*/
	
		public uint? dwDevKind;		/*�豸����*/
	
		public uint? dwClassKind;		/*���(��UNIDEVCLS��Kind����)*/
	
		public uint? dwBeginDate;		/*ԤԼ��ʼ����*/
	
		public uint? dwEndDate;		/*ԤԼ��������*/
	
		public string szRoomNos;		/*������,����ö��Ÿ���*/
	
		public uint? dwKind;		/*ԤԼ����*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*ԤԼ��ʽͳ��*/
	public struct RESVMODESTAT
	{
		private Reserved reserved;
		
		public uint? dwUseMode;		/*ԤԼ��ʽ*/
	
		public uint? dwUsers;		/*ԤԼ����*/
	
		public uint? dwResvTimes;		/*ԤԼ����*/
	
		public uint? dwResvMinutes;		/*ԤԼ��ʱ��(����)*/
		};

	/*��ѯͳ�Ƶ� ����*/
	public struct REPORTREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*��ʽ*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("ȫ��")]
				USERECGET_BYALL = 0,
			
				[EnumDescription("�豸ID")]
				USERECGET_BYDEVID = 2,
			
				[EnumDescription("����ID")]
				USERECGET_BYROOMID = 3,
			
				[EnumDescription("ʵ����ID")]
				USERECGET_BYLABID = 4,
			
		}

	
		public string szGetKey;		/*����ֵ*/
	
		public uint? dwAccNo;		/*�˺�*/
	
		public uint? dwPurpose;		/*��;*/
	
		public uint? dwDevKind;		/*�豸����*/
	
		public uint? dwClassKind;		/*���(��UNIDEVCLS��Kind����)*/
	
		public uint? dwCheckStat;		/*����Ա���״̬(CHECKINFO����)*/
	
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
		public uint? dwMinPrice;		/*��͵���*/
	
		public uint? dwMaxPrice;		/*��ߵ���*/
	
		public uint? dwStartPurchaseDate;		/*���繺������*/
	
		public uint? dwEndPurchaseDate;		/*��ֹ��������*/
	
		public string szRoomIDs;		/*����ID,����ö��Ÿ���*/
	
		public string szBuildingIDs;		/*¥��ID,����ö��Ÿ���*/
	
		public string szCampusIDs;		/*У��ID,����ö��Ÿ���*/
	
		public uint? dwActivitySN;		/*����ͱ��*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*�豸ʹ�ü�¼ ����*/
	public struct USERECREQ
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*�豸ID*/
	
		public uint? dwRoomID;		/*����ID*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public uint? dwAccNo;		/*�˺�*/
	
		public uint? dwDeptID;		/*����ID*/
	
		public uint? dwPurpose;		/*��;*/
	
		public uint? dwDevKind;		/*�豸����*/
	
		public uint? dwClassKind;		/*���(��UNIDEVCLS��Kind����)*/
	
		public uint? dwCheckStat;		/*����Ա���״̬(CHECKINFO����)*/
	
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
		public uint? dwMinPrice;		/*��͵���*/
	
		public uint? dwMaxPrice;		/*��ߵ���*/
	
		public uint? dwStartPurchaseDate;		/*���繺������*/
	
		public uint? dwEndPurchaseDate;		/*��ֹ��������*/
	
		public uint? dwAttendantID;		/*ֵ��Ա�˺�*/
	
		public string szMAC;		/*������ַ*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*�豸ʹ�ü�¼*/
	public struct DEVUSEREC
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*��ˮ��*/
	
		public uint? dwAccNo;		/*�ʺ�*/
	
		public string szPID;		/*ѧ����*/
	
		public string szTrueName;		/*����*/
	
		public uint? dwTutorID;		/*��ʦ���ʺţ�*/
	
		public string szTutorName;		/*��ʦ����*/
	
		public uint? dwDeptID;		/*����ID*/
	
		public string szDeptName;		/*����*/
	
		public uint? dwDevID;		/*�豸ID*/
	
		public uint? dwDevSN;		/*�豸���*/
	
		public string szDevName;		/*�豸����*/
	
		public string szMAC;		/*������ַ*/
	
		public string szKindName;		/*�豸����*/
	
		public string szClassName;		/*�豸�������*/
	
		public string szModel;		/*�豸�ͺ�*/
	
		public string szSpecification;		/*�豸���*/
	
		public uint? dwUnitPrice;		/*�豸����(Ԫ)*/
	
		public uint? dwPurchaseDate;		/*�ɹ����� YYYYMMDD*/
	
		public uint? dwRoomID;		/*����ID*/
	
		public string szRoomName;		/*�����*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabName;		/*ʵ��������*/
	
		public uint? dwAttendantID;		/*ֵ��Ա�˺�*/
	
		public string szAttendantName;		/*ֵ��Ա����*/
	
		public uint? dwPurpose;		/*��;*/
	
		public uint? dwBeginTime;		/*��ʼʱ��*/
	
		public uint? dwEndTime;		/*����ʱ��*/
	
		public uint? dwUseTime;		/*ʹ��ʱ��*/
	
		public uint? dwTotalCost;		/*�ܷ���*/
	
		public uint? dwBeginAdmin;		/*������ԱID*/
	
		public string szBeginAdminName;		/*������Ա����*/
	
		public uint? dwEndAdmin;		/*�黹����ԱID*/
	
		public string szEndAdminName;		/*�黹����Ա����*/
	
		public uint? dwCheckStat;		/*����Ա���״̬*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*��ѯ��ϸ������*/
	public struct DOORCARDRECREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*��ʽ*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("ȫ��")]
				DOORCARDRECGET_BYALL = 0,
			
				[EnumDescription("ָ���û���")]
				DOORCARDRECGET_BYGROUP = 1,
			
				[EnumDescription("����ID")]
				DOORCARDRECGET_BYROOMID = 3,
			
				[EnumDescription("ʵ����ID")]
				DOORCARDRECGET_BYLABID = 4,
			
		}

	
		public string szGetKey;		/*����ֵ*/
	
		public uint? dwAccNo;		/*�˺�*/
	
		public uint? dwCardMode;		/*��DOORCARDREQ�ṹ����*/
	
		public uint? dwUserKind;		/*��DOORCARDRES����*/
	
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
		public uint? dwStartTime;		/*��ʼʱ��*/
	
		public uint? dwEndTime;		/*����ʱ��*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*�Ž�ˢ����¼*/
	public struct DOORCARDREC
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*��ˮ��*/
	
		public uint? dwAccNo;		/*�ʺ�*/
	
		public string szPID;		/*ѧ����*/
	
		public string szCardNo;		/*����*/
	
		public string szTrueName;		/*����*/
	
		public uint? dwTutorID;		/*��ʦ���ʺţ�*/
	
		public string szTutorName;		/*��ʦ����*/
	
		public uint? dwDeptID;		/*����ID*/
	
		public string szDeptName;		/*����*/
	
		public uint? dwUsedDate;		/*ˢ������*/
	
		public uint? dwCardTime;		/*ˢ��ʱ��*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabName;		/*ʵ��������*/
	
		public uint? dwRoomID;		/*����ID*/
	
		public string szRoomNo;		/*������*/
	
		public string szRoomName;		/*��������*/
	
		public uint? dwManMode;		/*���Ʒ�ʽ(UNIROOM�ж���)*/
	
		public uint? dwCardMode;		/*ˢ��ģʽ*/
	
		public uint? dwUserKind;		/*�û�����*/
	
		public uint? dwResvID;		/*ԤԼID*/
	
		public string szMemo;		/*��չ��Ϣ*/
		};

	/*����ʹ��ͳ��*/
	public struct USERSTAT
	{
		private Reserved reserved;
		
		public uint? dwAccNo;		/*�ʺ�*/
	
		public string szPID;		/*ѧ����*/
	
		public string szTrueName;		/*����*/
	
		public string szDeptName;		/*����*/
	
		public uint? dwUseTimes;		/*ʹ���˴�*/
	
		public uint? dwUseTime;		/*ʹ����ʱ��*/
		};

	/*ʵ����ʹ����ͳ��*/
	public struct LABSTAT
	{
		private Reserved reserved;
		
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabSN;		/*���*/
	
		public string szLabName;		/*����*/
	
		public uint? dwTotalNum;		/*����(�������豸��)*/
	
		public uint? dwTotalTestHour;		/*ʹ������ѧʱ��*/
	
		public uint? dwPIDNum;		/*����ʹ������*/
	
		public uint? dwUseTimes;		/*����ʹ���˴�*/
	
		public uint? dwTotalUseTime;		/*����ʹ����ʱ��*/
		};

	/*ʵ����(����)ʹ����ͳ��*/
	public struct ROOMSTAT
	{
		private Reserved reserved;
		
		public uint? dwRoomID;		/*����ID*/
	
		public string szLabSN;		/*ʵ���ұ��*/
	
		public string szLabName;		/*ʵ��������*/
	
		public string szRoomNo;		/*�����*/
	
		public string szRoomName;		/*��������*/
	
		public uint? dwTotalNum;		/*����(�豸��)*/
	
		public uint? dwTotalTestHour;		/*ʹ������ѧʱ��*/
	
		public uint? dwTestUseTimes;		/*��ѧʵ��ʹ���˴�*/
	
		public uint? dwUseTimes;		/*����ʹ���˴�*/
	
		public uint? dwTotalUseTime;		/*����ʹ����ʱ��*/
		};

	/*�豸����ʹ����ͳ��*/
	public struct DEVKINDSTAT
	{
		private Reserved reserved;
		
		public uint? dwKindID;		/*�豸����ID*/
	
		public string szKindName;		/*�豸����*/
	
		public string szModel;		/*�豸�ͺ�*/
	
		public string szSpecification;		/*�豸���*/
	
		public uint? dwTotalNum;		/*����(�������豸��)*/
	
		public uint? dwTotalTestHour;		/*ʹ������ѧʱ��*/
	
		public uint? dwPIDNum;		/*����ʹ������*/
	
		public uint? dwUseTimes;		/*����ʹ���˴�*/
	
		public uint? dwTotalUseTime;		/*����ʹ����ʱ��*/
		};

	/*�豸���ʹ����ͳ��*/
	public struct DEVCLASSSTAT
	{
		private Reserved reserved;
		
		public uint? dwClassID;		/*�豸���ID*/
	
		public uint? dwClassKind;		/*���(��UNIDEVCLS��Kind����)*/
	
		public string szClassName;		/*�豸�������*/
	
		public uint? dwTotalNum;		/*����(�������豸��)*/
	
		public uint? dwTotalTestHour;		/*ʹ������ѧʱ��*/
	
		public uint? dwPIDNum;		/*����ʹ������*/
	
		public uint? dwUseTimes;		/*����ʹ���˴�*/
	
		public uint? dwTotalUseTime;		/*����ʹ����ʱ��*/
		};

	/*�豸ʹ����ͳ��*/
	public struct DEVSTAT
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*�豸ID*/
	
		public uint? dwDevSN;		/*�豸���*/
	
		public string szDevName;		/*�豸����*/
	
		public string szKindName;		/*�豸�������*/
	
		public string szClassName;		/*�豸�������*/
	
		public string szModel;		/*�豸�ͺ�*/
	
		public string szSpecification;		/*�豸���*/
	
		public uint? dwUnitPrice;		/*�豸����*/
	
		public uint? dwPurchaseDate;		/*�ɹ����� YYYYMMDD*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabSN;		/*ʵ���ұ��*/
	
		public string szLabName;		/*ʵ��������*/
	
		public uint? dwDeptID;		/*����ID*/
	
		public string szDeptName;		/*����*/
	
		public uint? dwAttendantID;		/*ֵ��Ա�˺�*/
	
		public string szAttendantName;		/*ֵ��Ա����*/
	
		public uint? dwTotalTestHour;		/*ʹ������ѧʱ��*/
	
		public uint? dwPIDNum;		/*����ʹ������*/
	
		public uint? dwUseTimes;		/*����ʹ���˴�*/
	
		public uint? dwTotalUseTime;		/*����ʹ����ʱ��*/
	
		public uint? dwTotalCost;		/*�ܷ���*/
		};

	/*ѧԺʹ��ͳ��*/
	public struct DEPTSTAT
	{
		private Reserved reserved;
		
		public uint? dwDeptID;		/*ѧԺID*/
	
		public string szDeptSN;		/*ѧԺ���*/
	
		public string szDeptName;		/*ѧԺ����*/
	
		public uint? dwTotalUsers;		/*ѧԺ����*/
	
		public uint? dwPIDNum;		/*ʹ������*/
	
		public uint? dwUseTimes;		/*ʹ���˴�*/
	
		public uint? dwTotalUseTime;		/*ʹ����ʱ��*/
		};

	/*��ѯ���ͳ�Ƶ� ����*/
	public struct IDENTSTATREQ
	{
		private Reserved reserved;
		
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
		public uint? dwPurpose;		/*��;*/
	
		public uint? dwDevKind;		/*�豸����*/
	
		public uint? dwClassKind;		/*���(��UNIDEVCLS��Kind����)*/
	
		public uint? dwCheckStat;		/*����Ա���״̬(CHECKINFO����)*/
	
		public uint? dwDeptID;		/*��Ա��������ID*/
	
		public uint? dwActivitySN;		/*����ͱ��*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*���ʹ��ͳ��*/
	public struct IDENTSTAT
	{
		private Reserved reserved;
		
		public uint? dwIdent;		/*���*/
	
		public uint? dwTotalUsers;		/*������*/
	
		public uint? dwPIDNum;		/*ʹ������*/
	
		public uint? dwUseTimes;		/*ʹ���˴�*/
	
		public uint? dwTotalUseTime;		/*ʹ����ʱ��*/
		};

	/*��ȡʵ����Ŀ��*/
	public struct TESTITEMSTATREQ
	{
		private Reserved reserved;
		
		public uint? dwTeacherID;		/*��ʦ���ʺţ�*/
	
		public uint? dwCourseID;		/*�γ�ID*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*ʵ����Ŀ��*/
	public struct TESTITEMSTAT
	{
		private Reserved reserved;
		
		public uint? dwTestItemID;		/*ʵ����ĿID*/
	
		public uint? dwTestCardID;		/*ʵ����Ŀ��ID*/
	
		public string szTestName;		/*ʵ������*/
	
		public uint? dwGroupPeopleNum;		/*ÿ������*/
	
		public uint? dwTestHour;		/*��ʵ����Ŀѧʱ��*/
	
		public uint? dwTestClass;		/*ʵ�����*/
	
		public uint? dwTestKind;		/*ʵ������*/
	
		public uint? dwRequirement;		/*ʵ��Ҫ��*/
	
		public uint? dwTestPlanID;		/*ʵ��ƻ�ID*/
	
		public string szAcademicSubjectCode;		/*����ѧ�Ʊ���*/
	
		public uint? dwTesteeKind;		/*ʵ�������*/
	
		public uint? dwTeacherID;		/*��ʦ���ʺţ�*/
	
		public string szTeacherName;		/*��ʦ����*/
	
		public uint? dwCourseID;		/*�γ�ID*/
	
		public string szCourseCode;		/*�γ̴���*/
	
		public string szCourseName;		/*�γ�����*/
	
		public uint? dwCourseProperty;		/*�γ�����*/
	
		public uint? dwGroupID;		/*�Ͽΰ༶*/
	
		public string szGroupName;		/*�Ͽΰ༶����*/
	
		public uint? dwGroupUsers;		/*���û���*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabSN;		/*ʵ���ұ��*/
	
		public string szLabName;		/*ʵ��������*/
	
		public uint? dwDevNum;		/*ԤԼ�豸��*/
		};

	/*��ȡ��ѧԤԼ��¼*/
	public struct TEACHINGRESVRECREQ
	{
		private Reserved reserved;
		
		public uint? dwTeacherID;		/*��ʦ���ʺţ�*/
	
		public uint? dwCourseID;		/*�γ�ID*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public uint? dwTestPlanID;		/*ʵ��ƻ�ID*/
	
		public uint? dwYearTerm;		/*ѧ�ڱ��*/
	
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
		public uint? dwMinUseRate;		/*ʵ�����ʹ����*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*ÿ����ʵ������*/
	public struct USERSPERMINUTE
	{
		private Reserved reserved;
		
		public uint? dwUsers;		/*ʵ������*/
		};

	/*��ѧԤԼ��¼*/
	public struct TEACHINGRESVREC
	{
		private Reserved reserved;
		
		public uint? dwTestItemID;		/*ʵ����ĿID*/
	
		public uint? dwTestCardID;		/*ʵ����Ŀ��ID*/
	
		public string szTestName;		/*ʵ������*/
	
		public uint? dwGroupPeopleNum;		/*ÿ������*/
	
		public uint? dwTestHour;		/*��ʵ����Ŀѧʱ��*/
	
		public uint? dwTestClass;		/*ʵ�����*/
	
		public uint? dwTestKind;		/*ʵ������*/
	
		public uint? dwRequirement;		/*ʵ��Ҫ��*/
	
		public uint? dwTestPlanID;		/*ʵ��ƻ�ID*/
	
		public string szAcademicSubjectCode;		/*����ѧ��*/
	
		public uint? dwTesteeKind;		/*ʵ�������*/
	
		public uint? dwTeacherID;		/*��ʦ���ʺţ�*/
	
		public string szTeacherName;		/*��ʦ����*/
	
		public uint? dwCourseID;		/*�γ�ID*/
	
		public string szCourseCode;		/*�γ̴���*/
	
		public string szCourseName;		/*�γ�����*/
	
		public uint? dwCourseProperty;		/*�γ�����*/
	
		public uint? dwGroupID;		/*�Ͽΰ༶*/
	
		public string szGroupName;		/*�Ͽΰ༶����*/
	
		public uint? dwGroupUsers;		/*���û���*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabSN;		/*ʵ���ұ��*/
	
		public string szLabName;		/*ʵ��������*/
	
		public uint? dwResvID;		/*ԤԼID*/
	
		public uint? dwResvStat;		/*ԤԼ״̬*/
	
		public uint? dwPreDate;		/*ԤԼ��ʼ����*/
	
		public uint? dwBeginTime;		/*ԤԼ��ʼʱ��*/
	
		public uint? dwEndTime;		/*ԤԼ����ʱ��*/
	
		public uint? dwTeachingTime;		/*��ѧʱ��(��ʽ��UNIRESERVE)*/
	
		public uint? dwDevNum;		/*ԤԼ�豸��*/
	
		public uint? dwAttendUsers;		/*ʵ������*/
	
	public USERSPERMINUTE[] UsersPerMinute;		/*CUniTable[USERSPERMINUTE]*/
		};

	/*��ȡ�豸ʹ��������*/
	public struct DEVUSINGRATEREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*��ʽ*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("ȫ��")]
				DEVUSINGRATE_BYALL = 0,
			
				[EnumDescription("�豸ID")]
				DEVUSINGRATE_BYDEVID = 2,
			
				[EnumDescription("����ID")]
				DEVUSINGRATE_BYROOMID = 3,
			
				[EnumDescription("ʵ����ID")]
				DEVUSINGRATE_BYLABID = 4,
			
		}

	
		public string szGetKey;		/*����ֵ*/
	
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
		public uint? dwClassKind;		/*���(��UNIDEVCLS��Kind����)*/
	
		public string szRoomIDs;		/*����ID,����ö��Ÿ���*/
	
		public string szBuildingIDs;		/*¥��ID,����ö��Ÿ���*/
	
		public string szCampusIDs;		/*У��ID,����ö��Ÿ���*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*�豸ʹ�������ݱ�*/
	public struct DEVUSINGTABLE
	{
		private Reserved reserved;
		
		public uint? dwUseTimes;		/*�豸ʹ���ܴ���*/
	
		public uint? dwResvTimes;		/*�豸ԤԼ�ܴ���*/
		};

	/*�豸ʹ����ͳ�Ʊ�*/
	public struct DEVUSINGRATE
	{
		private Reserved reserved;
		
		public uint? dwDevNums;		/*ͳ���豸����*/
	
		public uint? dwDays;		/*ͳ������*/
	
	public DEVUSINGTABLE[] szUsingTable;		/*�豸ʹ�������ݱ�(CUniTable[DEVUSINGTABLE]),ά��Ϊ24*60*/
		};

	/*��ȡ�豸��ʹ��������*/
	public struct DEVWEEKUSINGRATEREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*��ʽ*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("ȫ��")]
				DEVUSINGRATE_BYALL = 0,
			
				[EnumDescription("�豸ID")]
				DEVUSINGRATE_BYDEVID = 2,
			
				[EnumDescription("����ID")]
				DEVUSINGRATE_BYROOMID = 3,
			
				[EnumDescription("ʵ����ID")]
				DEVUSINGRATE_BYLABID = 4,
			
		}

	
		public string szGetKey;		/*����ֵ*/
	
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwWeeks;		/*��ѯ����*/
	
		public uint? dwClassKind;		/*���(��UNIDEVCLS��Kind����)*/
	
		public string szRoomIDs;		/*����ID,����ö��Ÿ���*/
	
		public string szBuildingIDs;		/*¥��ID,����ö��Ÿ���*/
	
		public string szCampusIDs;		/*У��ID,����ö��Ÿ���*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*�豸��ʹ����ͳ�Ʊ�*/
	public struct DEVWEEKUSINGRATE
	{
		private Reserved reserved;
		
		public uint? dwDevNums;		/*ͳ���豸����*/
	
		public uint? dwWeeks;		/*ͳ������*/
	
	public DEVUSINGTABLE[] szUsingTable;		/*�豸ʹ�������ݱ�(CUniTable[DEVUSINGTABLE]),ά��Ϊ7*/
		};

	/*���ݻ����ͳ�� ����*/
	public struct YARDACTIVITYSTATREQ
	{
		private Reserved reserved;
		
		public uint? dwDevKind;		/*�豸����*/
	
		public uint? dwClassKind;		/*���(��UNIDEVCLS��Kind����)*/
	
		public uint? dwCheckStat;		/*����Ա���״̬(CHECKINFO����)*/
	
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
		public string szRoomIDs;		/*����ID,����ö��Ÿ���*/
	
		public string szBuildingIDs;		/*¥��ID,����ö��Ÿ���*/
	
		public string szCampusIDs;		/*У��ID,����ö��Ÿ���*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*���ݻ����ͳ��*/
	public struct YARDACTIVITYSTAT
	{
		private Reserved reserved;
		
		public uint? dwActivitySN;		/*����ͱ��*/
	
		public string szActivityName;		/*���������*/
	
		public uint? dwPIDNum;		/*ʹ������*/
	
		public uint? dwUseTimes;		/*ʹ���˴�*/
	
		public uint? dwTotalUseTime;		/*ʹ����ʱ��*/
		};

	/*��ȡ�豸��ʹ��ͳ��*/
	public struct DEVMONTHSTATREQ
	{
		private Reserved reserved;
		
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*�豸��ʹ��ͳ��*/
	public struct DEVMONTHSTAT
	{
		private Reserved reserved;
		
		public uint? dwYearMonth;		/*ͳ���·�*/
	
		public uint? dwWResvTime;		/*�������豸ԤԼ��ʱ��(����)*/
	
		public uint? dwRResvTime;		/*�������豸ԤԼ��ʱ��(����)*/
	
		public uint? dwWUseTime;		/*�������豸ʹ����ʱ��(����)*/
	
		public uint? dwRUseTime;		/*�ǹ������豸ʹ����ʱ��(����)*/
		};

	/*��ȡ�豸����ʵ��ͳ������*/
	public struct RTUSESTATREQ
	{
		private Reserved reserved;
		
		public uint? dwStatType;		/*ͳ�Ʒ�ʽ*/
	
		[FlagsAttribute]
		public enum DWSTATTYPE : uint
		{
			
				[EnumDescription("�豸")]
				RTSTATBY_DEV = 1,
			
				[EnumDescription("�༶")]
				RTSTATBY_CLASS = 2,
			
				[EnumDescription("ѧԺ�����ţ�")]
				RTSTATBY_DEPT = 3,
			
				[EnumDescription("ʹ����")]
				RTSTATBY_USER = 4,
			
				[EnumDescription("��Ŀ�����ˣ���ʦ��")]
				RTSTATBY_HOLDER = 5,
			
				[EnumDescription("ѧ��������szHomeAddr��ʾ��")]
				RTSTATBY_FACULTY = 6,
			
		}

	
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
		public uint? dwDeptID;		/*�豸��������ID*/
	
		public string szLabIDs;		/*ʵ����ID,����ö��Ÿ���*/
	
		public string szRoomIDs;		/*����ID,����ö��Ÿ���*/
	
		public string szKindIDs;		/*��������,����ö��Ÿ���*/
	
		public string szClassIDs;		/*�����������ID,����ö��Ÿ���*/
	
		public string szDevIDs;		/*�豸ID,����ö��Ÿ���*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO],szExtInfo����RTUSESTAT�ϼ�*/
		};

	/*�豸����ʵ��ͳ��*/
	public struct RTUSESTAT
	{
		private Reserved reserved;
		
		public uint? dwStatID;		/*ͳ�ƶ���ID*/
	
		public string szStatName;		/*ͳ�ƶ�������*/
	
		public string szExtName;		/*��չ��Ϣ(������������Ա��*/
	
		public uint? dwResvTimes;		/*ԤԼ����*/
	
		public uint? dwResvMinutes;		/*ԤԼ��ʱ��(����)*/
	
		public uint? dwUseTimes;		/*ʹ�ô���*/
	
		public uint? dwUseMinutes;		/*�豸ʹ����ʱ��(����)*/
	
		public uint? dwSampleNum;		/*������Ʒ��*/
	
		public uint? dwReceivableCost;		/*Ӧ�ɷ���*/
	
		public uint? dwUseFee;		/*ϵͳ�Զ����㣨Ӧ�ɷ��ã�*/
	
		public uint? dwRealCost;		/*�������*/
	
		public uint? dwDevUseFee;		/*�豸ʹ�÷�*/
	
		public uint? dwSampleFee;		/*��Ʒ��*/
	
		public uint? dwAssistFee;		/*Э����*/
	
		public uint? dwEntrustFee;		/*�����*/
	
		public uint? dwNegotiationFee;		/*Э���շ�*/
		};

	/*��ȡ�豸����ʵ����ϸ����*/
	public struct RTUSEDETAILREQ
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*�豸ID*/
	
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*�豸����ʵ����ϸ*/
	public struct RTUSEDETAIL
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*�豸ID*/
	
		public string szDevName;		/*�豸����*/
	
		public string szAttendantName;		/*��������Ա*/
	
		public uint? dwResvID;		/*ԤԼ��*/
	
		public string szTestName;		/*����ʵ������*/
	
		public uint? dwOwner;		/*ԤԼ��(������)*/
	
		public string szOwnerName;		/*ԤԼ������*/
	
		public uint? dwPreDate;		/*ԤԼ��ʼ����*/
	
		public uint? dwBeginTime;		/*ԤԼ��ʼʱ��*/
	
		public uint? dwRTID;		/*����ʵ����ĿID*/
	
		public string szRTName;		/*����ʵ������*/
	
		public uint? dwHolderID;		/*�����ˣ��ʺţ�*/
	
		public string szHolderName;		/*����������*/
	
		public uint? dwManID;		/*����ԱID*/
	
		public string szManName;		/*����Ա����*/
	
		public uint? dwResvMinutes;		/*ԤԼ��ʱ��(����)*/
	
		public uint? dwUseMinutes;		/*�豸ʹ����ʱ��(����)*/
	
		public uint? dwSampleNum;		/*������Ʒ��*/
	
		public uint? dwReceivableCost;		/*Ӧ�ɷ���*/
	
		public uint? dwUseFee;		/*ϵͳ�Զ����㣨Ӧ�ɷ��ã�*/
	
		public uint? dwRealCost;		/*�������*/
	
		public uint? dwDevUseFee;		/*�豸ʹ�÷�*/
	
		public uint? dwSampleFee;		/*��Ʒ��*/
	
		public uint? dwAssistFee;		/*Э����*/
	
		public uint? dwEntrustFee;		/*�����*/
	
		public uint? dwNegotiationFee;		/*Э���շ�*/
		};

	/*��ȡ�豸����ʵ�龭�ѷ���ͳ������*/
	public struct RTFASTATREQ
	{
		private Reserved reserved;
		
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
		public uint? dwDeptID;		/*�豸��������ID*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO],szExtInfo����RTFASTAT�ϼ�*/
		};

	/*�豸����ʵ�龭�ѷ���ͳ��*/
	public struct RTFASTAT
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*�豸ID*/
	
		public string szDevName;		/*�豸����*/
	
		public string szAttendantName;		/*��������Ա*/
	
		public uint? dwResvTimes;		/*ԤԼ����*/
	
		public uint? dwResvMinutes;		/*ԤԼ��ʱ��(����)*/
	
		public uint? dwUseTimes;		/*ʹ�ô���*/
	
		public uint? dwUseMinutes;		/*�豸ʹ����ʱ��(����)*/
	
		public uint? dwSampleNum;		/*������Ʒ��*/
	
		public uint? dwTotalFee;		/*�շ��ܽ��*/
	
		public uint? dwTestFee;		/*�������Է�*/
	
		public uint? dwOpenFundFee;		/*���Ż���*/
	
		public uint? dwServiceFee;		/*�����*/
		};

	/*��ȡ�豸����ʵ�龭�ѷ�����ϸ����*/
	public struct RTFADETAILREQ
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*�豸ID*/
	
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*�豸����ʵ�龭�ѷ�����ϸ*/
	public struct RTFADETAIL
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*�豸ID*/
	
		public string szDevName;		/*�豸����*/
	
		public string szAttendantName;		/*��������Ա*/
	
		public uint? dwResvID;		/*ԤԼ��*/
	
		public string szTestName;		/*����ʵ������*/
	
		public uint? dwOwner;		/*ԤԼ��(������)*/
	
		public string szOwnerName;		/*ԤԼ������*/
	
		public uint? dwPreDate;		/*ԤԼ��ʼ����*/
	
		public uint? dwBeginTime;		/*ԤԼ��ʼʱ��*/
	
		public uint? dwRTID;		/*����ʵ����ĿID*/
	
		public string szRTName;		/*����ʵ������*/
	
		public uint? dwHolderID;		/*�����ˣ��ʺţ�*/
	
		public string szHolderName;		/*����������*/
	
		public uint? dwManID;		/*����ԱID*/
	
		public string szManName;		/*����Ա����*/
	
		public uint? dwResvMinutes;		/*ԤԼ��ʱ��(����)*/
	
		public uint? dwUseMinutes;		/*�豸ʹ����ʱ��(����)*/
	
		public uint? dwSampleNum;		/*������Ʒ��*/
	
		public uint? dwTotalFee;		/*�շ��ܽ��*/
	
		public uint? dwTestFee;		/*�������Է�*/
	
		public uint? dwOpenFundFee;		/*���Ż���*/
	
		public uint? dwServiceFee;		/*�����*/
		};

	/*��ѯΥԼͳ�Ƶ�����*/
	public struct DEFAULTSTATREQ
	{
		private Reserved reserved;
		
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
		public uint? dwCTSN;		/*���������*/
	
		public uint? dwUsePurpose;		/*��;*/
	
		public uint? dwForClsKind;		/*�����豸���*/
	
		public uint? dwDeptID;		/*��Ա��������ID*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*ΥԼͳ��*/
	public struct DEFAULTSTAT
	{
		private Reserved reserved;
		
		public uint? dwCreditSN;		/*�������ͱ��*/
	
		public string szCreditName;		/*������������*/
	
		public uint? dwResvTimes;		/*ԤԼ����*/
	
		public uint? dwDefaultTimes;		/*ΥԼ����*/
		};

	/*��ѧ���������豸��*/
	public struct DEVLISTREQ
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*ѧ�ڱ��*/
	
		public uint? dwReportStat;		/*����״̬*/
	
		public string szLabIDs;		/*ʵ����ID,����ö��Ÿ���*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*��ѧ���������豸�嵥*/
	public struct DEVLIST
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*ѧ�ڱ��*/
	
		public uint? dwReportStat;		/*����״̬*/
	
		[FlagsAttribute]
		public enum DWREPORTSTAT : uint
		{
			
				[EnumDescription("ԭʼ����")]
				REPORTSTAT_ORIGINAL = 1,
			
				[EnumDescription("�洢����")]
				REPORTSTAT_SAVE = 2,
			
				[EnumDescription("��������")]
				REPORTSTAT_DEPLOY = 0x100,
			
		}

	
		public uint? dwDevID;		/*�豸ID*/
	
		public string szAssertSN;		/*�û����ʲ����*/
	
		public string szClassSN;		/*�豸�����*/
	
		public string szDevName;		/*ʵ���豸����*/
	
		public string szModel;		/*�豸�ͺ�*/
	
		public string szSpecification;		/*�豸���*/
	
		public uint? dwComeFrom;		/*������Դ*/
	
		public uint? dwNationCode;		/*������*/
	
		public uint? dwUnitPrice;		/*�豸����*/
	
		public uint? dwPurchaseDate;		/*�ɹ�����*/
	
		public uint? dwStatCode;		/*��״��*/
	
		public uint? dwUseFor;		/*ʹ�÷���*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabSN;		/*ʵ���ұ��*/
	
		public string szLabName;		/*ʵ��������*/
	
		public uint? dwDeptID;		/*����ID*/
	
		public string szDeptSN;		/*���ű��*/
	
		public string szDeptName;		/*����*/
		};

	/*��ѧ���������豸�����䶯�����*/
	public struct DEVCHGREQ
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*ѧ�ڱ��*/
	
		public uint? dwReportStat;		/*����״̬*/
	
		public uint? dwUnitPrice;		/*���������۸����*/
	
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
		};

	/*��ѧ���������豸�����䶯�����*/
	public struct DEVCHG
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*ѧ�ڱ��*/
	
		public uint? dwReportStat;		/*����״̬*/
	
		public uint? dwBDevNum;		/*�ڳ�����*/
	
		public uint? dwBMoney;		/*�ڳ����*/
	
		public uint? dwBBigDevNum;		/*���������ڳ�����*/
	
		public uint? dwBBigMoney;		/*���������ڳ����*/
	
		public uint? dwIncDevNum;		/*��������*/
	
		public uint? dwIncMoney;		/*���ӽ��*/
	
		public uint? dwIncBigDevNum;		/*����������������*/
	
		public uint? dwIncBigMoney;		/*�����������ӽ��*/
	
		public uint? dwDecDevNum;		/*��������*/
	
		public uint? dwDecMoney;		/*���ٽ��*/
	
		public uint? dwDecBigDevNum;		/*����������������*/
	
		public uint? dwDecBigMoney;		/*�����������ٽ��*/
	
		public uint? dwEDevNum;		/*��ĩ����*/
	
		public uint? dwEMoney;		/*��ĩ���*/
	
		public uint? dwEBigDevNum;		/*����������ĩ����*/
	
		public uint? dwEBigMoney;		/*����������ĩ���*/
		};

	/*���������豸��*/
	public struct BIGDEVREQ
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*ѧ�ڱ��*/
	
		public uint? dwReportStat;		/*����״̬*/
	
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
		public uint? dwUnitPrice;		/*���������۸����*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*���������豸��*/
	public struct BIGDEV
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*ѧ�ڱ��*/
	
		public uint? dwReportStat;		/*����״̬*/
	
		public uint? dwDevID;		/*�豸ID*/
	
		public string szAssertSN;		/*�û����ʲ����*/
	
		public string szClassSN;		/*�豸�����*/
	
		public string szDevName;		/*ʵ���豸����*/
	
		public uint? dwUnitPrice;		/*�豸����*/
	
		public string szModel;		/*�豸�ͺ�*/
	
		public string szSpecification;		/*�豸���*/
	
		public string szAttendantName;		/*����������*/
	
		public uint? dwSampleNum;		/*������*/
	
		public uint? dwTUseTime;		/*��ѧ��ʱ*/
	
		public uint? dwRUseTime;		/*���л�ʱ*/
	
		public uint? dwSUseTime;		/*����ʱ*/
	
		public uint? dwOUseTime;		/*���Ż�ʱ*/
	
		public uint? dwUseTeachers;		/*ʹ�ý�ʦ����*/
	
		public uint? dwUseStudents;		/*ʹ��ѧ������*/
	
		public uint? dwUseOthers;		/*ʹ����������*/
	
		public uint? dwTItemNum;		/*��ѧʵ����Ŀ*/
	
		public uint? dwRItemNum;		/*����ʵ����Ŀ*/
	
		public uint? dwSItemNum;		/*���ʵ����Ŀ*/
	
		public uint? dwNReward;		/*���Ҽ�����*/
	
		public uint? dwPReward;		/*ʡ������*/
	
		public uint? dwTPatent;		/*��ʦר��*/
	
		public uint? dwSPatent;		/*ѧ��ר��*/
	
		public uint? dwThreeIndex;		/*�������*/
	
		public uint? dwKernelJournal;		/*���Ŀ���*/
		};

	/*��ȡʵ����Ŀ��*/
	public struct TESTITEMREPORTREQ
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*ѧ�ڱ��*/
	
		public uint? dwReportStat;		/*����״̬*/
	
		public string szLabIDs;		/*ʵ����ID,����ö��Ÿ���*/
	
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*ʵ����Ŀ��*/
	public struct TESTITEMREPORT
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*ѧ�ڱ��*/
	
		public uint? dwReportStat;		/*����״̬*/
	
		public uint? dwTestCardID;		/*ʵ����Ŀ��ID*/
	
		public string szTestSN;		/*ʵ����*/
	
		public string szTestName;		/*ʵ������*/
	
		public uint? dwTestClass;		/*ʵ�����*/
	
		public uint? dwTestKind;		/*ʵ������*/
	
		public uint? dwRequirement;		/*ʵ��Ҫ��*/
	
		public string szAcademicSubjectCode;		/*����ѧ�Ʊ���*/
	
		public uint? dwTesteeKind;		/*ʵ�������*/
	
		public uint? dwGroupPeopleNum;		/*ÿ������*/
	
		public uint? dwTestHour;		/*��ʵ����Ŀѧʱ��*/
	
		public uint? dwTesteeNum;		/*����������*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabSN;		/*ʵ���ұ��*/
	
		public string szLabName;		/*ʵ��������*/
		};

	/*ר��ʵ������Ա��*/
	public struct STAFFINFOREQ
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*ѧ�ڱ��*/
	
		public uint? dwReportStat;		/*����״̬*/
	
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
		public string szLabIDs;		/*ʵ����ID,����ö��Ÿ���*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*ר��ʵ������Ա��*/
	public struct STAFFINFO
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*ѧ�ڱ��*/
	
		public uint? dwReportStat;		/*����״̬*/
	
		public uint? dwAccNo;		/*�ʺ�*/
	
		public string szPID;		/*��Ա���(ѧ����)*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabSN;		/*ʵ���ұ��*/
	
		public string szLabName;		/*ʵ��������*/
	
		public string szTrueName;		/*����*/
	
		public uint? dwSex;		/*�Ա��UniCommon.h*/
	
		public uint? dwBirthDate;		/*��������*/
	
		public uint? dwJobTitle;		/*ְ�Ʊ���*/
	
		[FlagsAttribute]
		public enum DWJOBTITLE : uint
		{
			
				[EnumDescription("�߼�ְ��")]
				JTITLE_SENIOR = 1,
			
				[EnumDescription("�м�ְ��")]
				JTITLE_MIDDLE = 2,
			
				[EnumDescription("����")]
				JTITLE_OTHER = 0x100,
			
		}

	
		public uint? dwDuty;		/*ְ��*/
	
		[FlagsAttribute]
		public enum DWDUTY : uint
		{
			
				[EnumDescription("ʵ�鼼����Ա")]
				SDUTY_MANAGER = 1,
			
				[EnumDescription("��ʦ")]
				SDUTY_TEACHER = 2,
			
				[EnumDescription("����")]
				SDUTY_OTHER = 0x100,
			
		}

	
		public uint? dwJobType;		/*��������*/
	
		[FlagsAttribute]
		public enum DWJOBTYPE : uint
		{
			
				[EnumDescription("רְ")]
				JOB_FULLTIME = 1,
			
				[EnumDescription("��ְ")]
				JOB_PARTTIME = 2,
			
		}

	
		public string szAcademicSubjectCode;		/*����ѧ�Ʊ���*/
	
		public uint? dwProfessionalTitle;		/*רҵ����ְ��*/
	
		public uint? dwEducation;		/*�Ļ��̶�*/
	
		public uint? dwExpertType;		/*ר�����*/
	
		public uint? dwInlandUduTime;		/*����ѧ������ʱ��*/
	
		public uint? dwInlandOtherTime;		/*���ڷ�ѧ������ʱ��*/
	
		public uint? dwAbroadUduTime;		/*����ѧ������ʱ��*/
	
		public uint? dwAbroadOtherTime;		/*�����ѧ������ʱ��*/
	
		public string szMemo;		/*��ע*/
		};

	/*ʵ���һ��������*/
	public struct LABINFOREQ
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*ѧ�ڱ��*/
	
		public uint? dwReportStat;		/*����״̬*/
	
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
		public string szLabIDs;		/*ʵ����ID,����ö��Ÿ���*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*ʵ���һ��������*/
	public struct LABINFO
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*ѧ�ڱ��*/
	
		public uint? dwReportStat;		/*����״̬*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabSN;		/*ʵ���ұ��*/
	
		public string szLabName;		/*ʵ��������*/
	
		public string szLabKindCode;		/*ʵ�������ͱ���*/
	
		public string szLabLevelCode;		/*ʵ���ҽ���ˮƽ����*/
	
		public string szLabFromCode;		/*ʵ������Դ����*/
	
		public string szAcademicSubjectCode;		/*����ѧ�Ʊ���*/
	
		public uint? dwLabClass;		/*ʵ�������*/
	
		public uint? dwCreateDate;		/*�������*/
	
		public uint? dwTNReward;		/*��ʦ���Ҽ�����*/
	
		public uint? dwTPReward;		/*��ʦʡ������*/
	
		public uint? dwTPatent;		/*��ʦר��*/
	
		public uint? dwSNReward;		/*ѧ�����Ҽ�����*/
	
		public uint? dwSPReward;		/*ѧ��ʡ������*/
	
		public uint? dwSPatent;		/*ѧ��ר��*/
	
		public uint? dwTThreeIndex;		/*��ѧ�������*/
	
		public uint? dwTKernelJournal;		/*��ѧ�����ڿ�*/
	
		public uint? dwRThreeIndex;		/*�����������*/
	
		public uint? dwRKernelJournal;		/*���к����ڿ�*/
	
		public uint? dwTestBookNum;		/*ʵ��̲���*/
	
		public uint? dwTItemNum;		/*��ѧʵ����Ŀ*/
	
		public uint? dwRItemNum;		/*����ʵ����Ŀ*/
	
		public uint? dwPTItemNum;		/*ʡ�������Ͻ�ѧʵ����Ŀ*/
	
		public uint? dwPRItemNum;		/*ʡ�������Ͽ���ʵ����Ŀ*/
	
		public uint? dwSItemNum;		/*���ʵ����Ŀ*/
	
		public uint? dwZKThesisUsers;		/*ר����������*/
	
		public uint? dwBKThesisUsers;		/*������������*/
	
		public uint? dwSSThesisUsers;		/*˶ʿ�о�����������*/
	
		public uint? dwBSThesisUsers;		/*��ʿ�о�����������*/
	
		public uint? dwItemNum;		/*ʵ�����*/
	
		public uint? dwOtherItemNum;		/*У��ʵ�����*/
	
		public uint? dwUseUsers;		/*ʵ������*/
	
		public uint? dwOtherUsers;		/*У��ʵ������*/
	
		public uint? dwUseTime;		/*ʵ����ʱ��*/
	
		public uint? dwOtherTime;		/*У��ʵ����ʱ��*/
	
		public uint? dwPartTimeUsers;		/*��ְ��Ա��*/
	
		public uint? dwTotalCost;		/*���з���*/
	
		public uint? dwConsumeCost;		/*�Ĳķ�*/
		};

	/*ʵ���Ҿ��������*/
	public struct LABALLCOSTREQ
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*ѧ�ڱ��*/
	
		public uint? dwReportStat;		/*����״̬*/
	
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
		};

	/*ʵ���Ҿ��������*/
	public struct LABALLCOST
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*ѧ�ڱ��*/
	
		public uint? dwReportStat;		/*����״̬*/
	
		public uint? dwLabNum;		/*ʵ���Ҹ���*/
	
		public uint? dwLabArea;		/*ʵ�������*/
	
		public uint? dwTotalCost;		/*�ܼ�(Ԫ)*/
	
		public uint? dwBuyCost;		/*���÷�(Ԫ)*/
	
		public uint? dwTBuyCost;		/*���ǹ��÷�(Ԫ)*/
	
		public uint? dwKeepCost;		/*ά����(Ԫ)*/
	
		public uint? dwTKeepCost;		/*����ά����(Ԫ)*/
	
		public uint? dwRunCost;		/*���з�(Ԫ)*/
	
		public uint? dwCRunCost;		/*�Ĳķ�(Ԫ)*/
	
		public uint? dwBuildCost;		/*�����(Ԫ)*/
	
		public uint? dwRAndRCost;		/*�о���ĸ��(Ԫ)*/
	
		public uint? dwOtherCost;		/*������(Ԫ)*/
		};

	/*�ߵ�ѧУʵ�����ۺ���Ϣ��*/
	public struct LABSUMMARYREQ
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*ѧ�ڱ��*/
	
		public uint? dwReportStat;		/*����״̬*/
	
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
		public uint? dwUnitPrice;		/*�豸����*/
		};

	/*�ߵ�ѧУʵ�����ۺ���Ϣ��*/
	public struct LABSUMMARY
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*ѧ�ڱ��*/
	
		public uint? dwReportStat;		/*����״̬*/
	
		public uint? dwLabNum;		/*ʵ���Ҹ���*/
	
		public uint? dwLabArea;		/*ʵ�������*/
	
		public uint? dwDevNum;		/*��������*/
	
		public uint? dwDevMoney;		/*�������*/
	
		public uint? dwBigDevNum;		/*������������*/
	
		public uint? dwBigMoney;		/*�����������*/
	
		public uint? dwTItemNum;		/*��ѧʵ����Ŀ*/
	
		public uint? dwTUseTime;		/*��ѧʵ����ʱ��*/
	
		public uint? dwDUseTime;		/*��ʿ��ʱ��*/
	
		public uint? dwMUseTime;		/*˶ʿ��ʱ��*/
	
		public uint? dwUUseTime;		/*������ʱ��*/
	
		public uint? dwJUseTime;		/*ר����ʱ��*/
	
		public uint? dwRItemNum;		/*����ʵ����Ŀ*/
	
		public uint? dwHTStaff;		/*�߼���ʦ������Ա*/
	
		public uint? dwHSStaff;		/*�߼�ʵ�鼼����Ա*/
	
		public uint? dwMTStaff;		/*�м���ʦ������Ա*/
	
		public uint? dwMSStaff;		/*�м�ʵ�鼼����Ա*/
	
		public uint? dwOtherStaff;		/*������Ա*/
	
		public uint? dwPartTimeStaff;		/*��ְ��Ա*/
	
		public uint? dwPaperNum;		/*������*/
	
		public uint? dwTReward;		/*��ʦ����*/
	
		public uint? dwSReward;		/*ѧ������*/
		};

	/*�ߵ�ѧУʵ�����ۺ���Ϣ��2*/
	public struct LABSUMMARY2REQ
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*ѧ�ڱ��*/
	
		public uint? dwReportStat;		/*����״̬*/
	
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
		public uint? dwUnitPrice;		/*�豸����*/
		};

	/*�ߵ�ѧУʵ�����ۺ���Ϣ��2*/
	public struct LABSUMMARY2
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*ѧ�ڱ��*/
	
		public uint? dwReportStat;		/*����״̬*/
	
		public uint? dwLabNum;		/*ʵ���Ҹ���*/
	
		public uint? dwLabArea;		/*ʵ�������*/
	
		public uint? dwDevNum;		/*��������*/
	
		public uint? dwDevMoney;		/*�������*/
	
		public uint? dwBigDevNum;		/*������������*/
	
		public uint? dwBigMoney;		/*�����������*/
		};

	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/

	/*��ȡ�����������*/
	public struct CFGREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*��ȡ�������*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("��������")]
				CFGGET_KIND = 0x1,
			
		}

	
		public string szGetKey;		/*��ȡ����ֵ*/
		};

	/*����������*/
	public struct CFGINFO
	{
		private Reserved reserved;
		
		public uint? dwKindSN;		/*������������*/
	
		public uint? dwCfgSN;		/*����������*/
	
		[FlagsAttribute]
		public enum DWCFGSN : uint
		{
			
				[EnumDescription("ϵͳ��������")]
				CFGKINDNAME_SYS = 0x100,
			
				[EnumDescription("�����ϻ���������")]
				CFGKINDNAME_CCM = 0x200,
			
				[EnumDescription("�����������صĲ�������")]
				CFGKINDNAME_CTRL = 0x400,
			
				[EnumDescription("����֧�ֲ���")]
				CFGKINDNAME_SUP = 0x800,
			
				[EnumDescription("������ϴ�����ֵ��,1��ʹ�ã�0����ʹ��")]
				CFGCCM_NETFAULT = (CFGKINDNAME_SYS+1),
			
				[EnumDescription("ע��ʱ�Զ��ػ�����ֵ��,1�ػ���0���ػ�")]
				CFGCCM_AUTOSHUTDOWN = (CFGKINDNAME_SYS+2),
			
				[EnumDescription("�ȴ���¼ʱ��(����)")]
				CFGCCM_WAITLOGONTIME = (CFGKINDNAME_SYS+3),
			
				[EnumDescription("�ͻ��˿����Զ�ע��ʱ��(����)")]
				CFGCCM_IDLELOGOUTTIME = (CFGKINDNAME_SYS+4),
			
				[EnumDescription("��ʱ�뿪����ʱ��(����)")]
				CFGLEAVE_HOLDTIME = (CFGKINDNAME_SYS+5),
			
				[EnumDescription("�ͻ���ע������ʱ��(����)")]
				CFGLOGOUT_HOLDTIME = (CFGKINDNAME_SYS+6),
			
				[EnumDescription("ԤԼ�����ȴ�ˢ��ȷ��ʱ��(����)")]
				CFGRESVOVER_WAITCARDTIME = (CFGKINDNAME_SYS+7),
			
				[EnumDescription("���ܼ��ÿ�γ���ʱ��(��)")]
				CFGMONSTAT_DURATIONTIME = (CFGKINDNAME_SYS+8),
			
				[EnumDescription("���ܼ������״̬�ı�ļ�����")]
				CFGMONSTAT_CHECKTIMESCHG = (CFGKINDNAME_SYS+9),
			
				[EnumDescription("�ʸ���˷�Χ����")]
				CFGSFROLE_SCOPEKIND = (CFGKINDNAME_SYS+10),
			
				[EnumDescription("֧���ϴ�ʵ������")]
				CFGTESTDATA_UPLOAD = (CFGKINDNAME_SYS+11),
			
				[EnumDescription("��ʽ����������Ϊ��һ��)")]
				CFGCALENDA_WEST = (CFGKINDNAME_SYS+12),
			
				[EnumDescription("�Ͳͱ���ʱ��(����)")]
				CFGDINNER_HOLDTIME = (CFGKINDNAME_SYS+13),
			
				[EnumDescription("���ʱ��(HHMMHHMM ����12001300)")]
				CFGDINNER_LUNCHTIME = (CFGKINDNAME_SYS+14),
			
				[EnumDescription("���ʱ��(HHMMHHMM ����16001800)")]
				CFGDINNER_SUPPERTIME = (CFGKINDNAME_SYS+15),
			
				[EnumDescription("ͨ����ˢ������ԤԼ��ǰ��Чʱ��(����)")]
				CFGCHGREV_ADVANCETIME = (CFGKINDNAME_SYS+16),
			
				[EnumDescription("�Ž���ť(����ˢ��������¼��ʱ�뿪")]
				CFGDOOR_CARDNOLEAVE = (CFGKINDNAME_SYS+17),
			
				[EnumDescription("��ʱ�뿪��ǰ֪ͨʱ��")]
				CFGLEAVEEND_NOTICETIME = (CFGKINDNAME_SYS+18),
			
				[EnumDescription("�Ž�ˢ��ͨ��������ʵʱ��֤")]
				CFGDOORCARD_BYTHIRD = (CFGKINDNAME_SYS+19),
			
				[EnumDescription("ͨ����ˢ����Чʱ��(����)")]
				CFGAUTOGATE_VALIDMIN = (CFGKINDNAME_SYS+20),
			
				[EnumDescription("��ֹ�ͻ����޸�����")]
				CFGPASSWD_FORBID = (CFGKINDNAME_CCM+1),
			
				[EnumDescription("֧����Ѻ��շ�ģʽѡ��")]
				CFGPASSWD_MULTIUSEMODE = (CFGKINDNAME_CCM+2),
			
				[EnumDescription("ѧ������Ӳ�̴�С")]
				CFGCDISK_STUDENTSPACE = (CFGKINDNAME_CCM+3),
			
				[EnumDescription("��ʦ����Ӳ�̴�С")]
				CFGCDISK_TEACHERSPACE = (CFGKINDNAME_CCM+4),
			
				[EnumDescription("ȱʡ�������,�ο�UNICTRLCLASS::dwCtrlMode")]
				CFGCTRL_URLCTRL = (CFGKINDNAME_CTRL+1),
			
				[EnumDescription("����趨ֵ,�ο�UNICTRLCLASS::dwCtrlMode")]
				CFGCTRL_URLCTRLPARAM = (CFGKINDNAME_CTRL+2),
			
				[EnumDescription("��δ�Ƿ���,0,����أ�1���")]
				CFGCTRL_URLWEEKEND = (CFGKINDNAME_CTRL+3),
			
				[EnumDescription("�Ƿ��¼������־,0,����¼��1��¼")]
				CFGCTRL_URLLOGED = (CFGKINDNAME_CTRL+4),
			
				[EnumDescription("ȱʡ�������,�ο�UNICTRLCLASS::dwCtrlMode")]
				CFGCTRL_SWCTRL = (CFGKINDNAME_CTRL+51),
			
				[EnumDescription("����趨ֵ,�ο�UNICTRLCLASS::dwCtrlMode")]
				CFGCTRL_SWCTRLPARAM = (CFGKINDNAME_CTRL+52),
			
				[EnumDescription("��δ�Ƿ���,0,����أ�1���")]
				CFGCTRL_SWWEEKEND = (CFGKINDNAME_CTRL+53),
			
				[EnumDescription("�Ƿ��¼����������־,0,����¼��1��¼")]
				CFGCTRL_SWLOGED = (CFGKINDNAME_CTRL+54),
			
				[EnumDescription("�ۺ����Email")]
				CFGSUP_EMAIL = (CFGKINDNAME_SUP+1),
			
				[EnumDescription("�ۺ�����ֻ���")]
				CFGSUP_HANDPHONE = (CFGKINDNAME_SUP+2),
			
				[EnumDescription("�ͻ���д")]
				CFGSUP_CUSTOM = (CFGKINDNAME_SUP+3),
			
		}

	
		public uint? dwValueKind;		/*ֵ����*/
	
		[FlagsAttribute]
		public enum DWVALUEKIND : uint
		{
			
				[EnumDescription("��ֵ��")]
				CFGVALUEKIND_NUMBER = 1,
			
				[EnumDescription("�ַ�����")]
				CFGVALUEKIND_STRING = 2,
			
		}

	
		public uint? dwNumberValue;		/*������ֵ����ֵ����Ч*/
	
		public string szStringValue;		/*������ֵ���ַ�������Ч*/
	
		public string szMemo;		/*˵��*/
		};

	/*��ȡ�������*/
	public struct CREDITTYPEREQ
	{
		private Reserved reserved;
		
		public uint? dwCTSN;		/*���������*/
	
		public uint? dwCTStat;		/*״̬*/
		};

	/*�������������*/
	public struct CREDITTYPE
	{
		private Reserved reserved;
		
		public uint? dwCTSN;		/*���������*/
	
		public string szCTName;		/*�����������*/
	
		public uint? dwForClsKind;		/*�����豸���*/
	
		public uint? dwUsePurpose;		/*��;*/
	
		public uint? dwMaxScore;		/*������û���*/
	
		public uint? dwScoreCycle;		/*���üƷ�����*/
	
		[FlagsAttribute]
		public enum DWSCORECYCLE : uint
		{
			
				[EnumDescription("ÿ��")]
				SCORECYCLE_YEAR = 1,
			
				[EnumDescription("ÿѧ��")]
				SCORECYCLE_TERM = 2,
			
		}

	
		public uint? dwForbidUseTime;		/*���û���Ϊ0��ֹʹ��ʱ�䣨�죩*/
	
		public uint? dwCTStat;		/*״̬*/
	
		[FlagsAttribute]
		public enum DWCTSTAT : uint
		{
			
				[EnumDescription("ʹ����")]
				CTSTAT_INUSE = 1,
			
				[EnumDescription("δʹ��")]
				CTSTAT_UNUSE = 2,
			
		}

	
		public string szMemo;		/*˵��*/
		};

	/*��������*/
	public struct CREDITKIND
	{
		private Reserved reserved;
		
		public uint? dwCreditSN;		/*�������ͱ��*/
	
		[FlagsAttribute]
		public enum DWCREDITSN : uint
		{
			
				[EnumDescription("ԤԼ����")]
				CREDIT_RESVLATE = 1,
			
				[EnumDescription("ʹ���ʲ����")]
				CREDIT_USERATELOW = 2,
			
				[EnumDescription("��費��ʱ�黹")]
				CREDIT_RETURNLATE = 3,
			
				[EnumDescription("���豸")]
				CREDIT_DAMAGEDEV = 4,
			
				[EnumDescription("ȡ��ԤԼ")]
				CREDIT_RESVCANCEL = 5,
			
				[EnumDescription("ԤԼ����δˢ���뿪")]
				CREDIT_RESVENDNOCARD = 6,
			
				[EnumDescription("ʹ�����������")]
				CREDIT_USERLOW = 7,
			
				[EnumDescription("ʹ��Υ�棨�˹�������")]
				CREDIT_DEREGULATION = 8,
			
				[EnumDescription("��ʱ�뿪δˢ��")]
				CREDIT_LEAVENOCARD = 9,
			
				[EnumDescription("��ʱ�뿪δ��ʱ����")]
				CREDIT_LEAVENOBACK = 10,
			
				[EnumDescription("δ��ʱ�μӻΥԼ")]
				CREDIT_ACTIVITYLATEORABSENT = 11,
			
				[EnumDescription("����ʹ��")]
				CREDIT_NORMALUSE = 1001,
			
				[EnumDescription("����Ա����")]
				CREDIT_CORRECTERR = 2001,
			
		}

	
		public uint? dwScoreType;		/*���ִ���ʽ*/
	
		[FlagsAttribute]
		public enum DWSCORETYPE : uint
		{
			
				[EnumDescription("�����û���")]
				SCORE_DEDUCT = 1,
			
				[EnumDescription("�����û���")]
				SCORE_AWARD = 2,
			
		}

	
		public uint? dwCKStat;		/*״̬*/
	
		[FlagsAttribute]
		public enum DWCKSTAT : uint
		{
			
				[EnumDescription("ʹ����")]
				CREDITSTAT_INUSE = 1,
			
				[EnumDescription("δʹ��")]
				CREDITSTAT_UNUSE = 2,
			
		}

	
		public string szCreditName;		/*������������*/
	
		public string szMemo;		/*˵��*/
		};

	/*��ȡ���üƷֱ�*/
	public struct CREDITSCOREREQ
	{
		private Reserved reserved;
		
		public uint? dwID;		/*ID*/
	
		public uint? dwCTSN;		/*���������*/
	
		public uint? dwCreditSN;		/*�������ͱ��*/
	
		public uint? dwForClsKind;		/*�����豸���*/
	
		public uint? dwUsePurpose;		/*��;*/
		};

	/*���üƷֱ�*/
	public struct CREDITSCORE
	{
		private Reserved reserved;
		
		public uint? dwID;		/*ID*/
	
		public uint? dwCTSN;		/*���������*/
	
		public string szCTName;		/*�����������*/
	
		public uint? dwForClsKind;		/*�����豸���*/
	
		public uint? dwUsePurpose;		/*��;*/
	
		public uint? dwMaxScore;		/*������û���*/
	
		public uint? dwScoreCycle;		/*���üƷ�����*/
	
		public uint? dwForbidUseTime;		/*���û���Ϊ0��ֹʹ��ʱ�䣨�죩*/
	
		public uint? dwCreditSN;		/*�������ͱ��*/
	
		public string szCreditName;		/*������������*/
	
		public uint? dwScoreType;		/*���ִ���ʽ*/
	
		public uint? dwUseNum;		/*�������ö���*/
	
		public uint? dwMinValue1;		/*����������Сֵ1*/
	
		public uint? dwMaxValue1;		/*�����������ֵ1*/
	
		public uint? dwCreditScore1;		/*�ۻ򽱻���1*/
	
		public uint? dwMinValue2;		/*����������Сֵ2*/
	
		public uint? dwMaxValue2;		/*�����������ֵ2*/
	
		public uint? dwCreditScore2;		/*�ۻ򽱻���2*/
	
		public uint? dwMinValue3;		/*����������Сֵ3*/
	
		public uint? dwMaxValue3;		/*�����������ֵ3*/
	
		public uint? dwCreditScore3;		/*�ۻ򽱻���3*/
	
		public uint? dwMinValue4;		/*����������Сֵ4*/
	
		public uint? dwMaxValue4;		/*�����������ֵ4*/
	
		public uint? dwCreditScore4;		/*�ۻ򽱻���4*/
	
		public uint? dwMinValue5;		/*����������Сֵ5*/
	
		public uint? dwMaxValue5;		/*�����������ֵ5*/
	
		public uint? dwCreditScore5;		/*�ۻ򽱻���*/
	
		public uint? dwMinValue6;		/*����������Сֵ6*/
	
		public uint? dwMaxValue6;		/*�����������ֵ6*/
	
		public uint? dwCreditScore6;		/*�ۻ򽱻���6*/
	
		public string szMemo;		/*˵��*/
		};

	/*��ȡ�ҵ����û���*/
	public struct MYCREDITSCOREREQ
	{
		private Reserved reserved;
		
		public uint? dwAccNo;		/*�˺�*/
	
		public uint? dwCTSN;		/*���������*/
		};

	/*�ҵ����û���*/
	public struct MYCREDITSCORE
	{
		private Reserved reserved;
		
		public uint? dwAccNo;		/*�˺�*/
	
		public string szPID;		/*ѧ����*/
	
		public string szTrueName;		/*����*/
	
		public uint? dwCTSN;		/*���������*/
	
		public string szCTName;		/*�����������*/
	
		public uint? dwForClsKind;		/*�����豸���*/
	
		public uint? dwUsePurpose;		/*��;*/
	
		public uint? dwMaxScore;		/*������û���*/
	
		public uint? dwScoreCycle;		/*���üƷ�����*/
	
		public uint? dwForbidUseTime;		/*���û���Ϊ0��ֹʹ��ʱ�䣨�죩*/
	
		public uint? dwLeftCScore;		/*ʣ�����*/
	
		public uint? dwForbidStartDate;		/*���ÿ�ʼ����*/
	
		public uint? dwForbidEndDate;		/*���ý�������*/
	
		public string szMemo;		/*˵��*/
		};

	/*�˹����ù���*/
	public struct ADMINCREDIT
	{
		private Reserved reserved;
		
		public uint? dwCTSN;		/*���������*/
	
		public uint? dwCreditSN;		/*�������ͱ��*/
	
		public uint? dwCreditScore;		/*�ۻ򽱻���*/
	
		public uint? dwSubjectID;		/*������ID*/
	
		public uint? dwAccNo;		/*�˺�*/
	
		public string szTrueName;		/*����*/
	
		public string szReason;		/*ԭ�������*/
	
		public uint? dwPara1;		/*����1����չ��*/
	
		public uint? dwPara2;		/*����2����չ��*/
	
		public string szMemo;		/*˵��*/
		};

	/*���ü�¼����*/
	public struct CREDITRECREQ
	{
		private Reserved reserved;
		
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
		public uint? dwSID;		/*��ˮ��*/
	
		public uint? dwAccNo;		/*�˺�*/
	
		public uint? dwCTSN;		/*���������*/
	
		public uint? dwCreditSN;		/*�������ͱ��*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*���ü�¼*/
	public struct CREDITREC
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*SID*/
	
		public uint? dwCTSN;		/*���������*/
	
		public string szCTName;		/*�����������*/
	
		public uint? dwCreditSN;		/*�������ͱ��*/
	
		public string szCreditName;		/*������������*/
	
		public uint? dwScoreType;		/*���ִ���ʽ*/
	
		public uint? dwAccNo;		/*�˺�*/
	
		public string szPID;		/*ѧ����*/
	
		public string szTrueName;		/*��ʵ����*/
	
		public uint? dwTutorID;		/*��ʦ���ʺţ�*/
	
		public string szTutorName;		/*��ʦ����*/
	
		public uint? dwDeptID;		/*����ID*/
	
		public string szDeptName;		/*����*/
	
		public uint? dwDevID;		/*�豸ID*/
	
		public string szDevName;		/*�豸����*/
	
		public uint? dwRoomID;		/*����ID*/
	
		public string szRoomName;		/*�����*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabName;		/*ʵ��������*/
	
		public uint? dwAttendantID;		/*ֵ��Ա�˺�*/
	
		public string szAttendantName;		/*ֵ��Ա����*/
	
		public string szAttendantTel;		/*ֵ��Ա�绰*/
	
		public uint? dwSubjectID;		/*����ID*/
	
		public uint? dwOccurDate;		/*��������*/
	
		public uint? dwOccurTime;		/*����ʱ��*/
	
		public uint? dwThisUseCScore;		/*����ʹ�û���*/
	
		public uint? dwLeftCScore;		/*�ۼƷ���*/
	
		public uint? dwUserCStat;		/*�û�����״̬*/
	
		[FlagsAttribute]
		public enum DWUSERCSTAT : uint
		{
			
				[EnumDescription("��Ч")]
				USERCSTAT_VALID = 1,
			
				[EnumDescription("����Աȡ��")]
				USERCSTAT_CANCEL = 2,
			
				[EnumDescription("�ѹ���������")]
				USERCSTAT_OVER = 4,
			
		}

	
		public uint? dwForbidStartDate;		/*���ÿ�ʼʱ��*/
	
		public uint? dwForbidEndDate;		/*���ý���ʱ��*/
	
		public string szMemo;		/*˵��*/
		};

	/*��ȡϵͳ��������*/
	public struct SYSFUNCREQ
	{
		private Reserved reserved;
		
		public uint? dwSFSN;		/*���ܱ��*/
		};

	/*ϵͳ���ܶ���*/
	public struct SYSFUNC
	{
		private Reserved reserved;
		
		public uint? dwSFSN;		/*���ܱ��*/
	
		public string szSFName;		/*��������*/
	
		public string szURL;		/*ʹ����ϸ���ܵ�URL*/
	
		public string szMemo;		/*˵��*/
		};

	/*��ȡ�ʸ����*/
	public struct SYSFUNCRULEREQ
	{
		private Reserved reserved;
		
		public uint? dwSFRuleID;		/*����ʹ�ù���ID*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public uint? dwAuthType;		/*��Ȩ���*/
	
		public uint? dwScopeKind;		/*���÷�Χ����*/
	
		public uint? dwScopeID;		/*��ΧID(����dwScopeKind���岻ͬ)*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*ϵͳ����ʹ�ù���*/
	public struct SYSFUNCRULE
	{
		private Reserved reserved;
		
		public uint? dwSFRuleID;		/*����ʹ�ù���ID*/
	
		public string szSFRuleName;		/*��������*/
	
		public uint? dwSFSN;		/*���ܱ��*/
	
		public string szSFName;		/*��������*/
	
		public string szSFURL;		/*ʹ����ϸ���ܵ�URL*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabName;		/*ʵ��������*/
	
		public uint? dwScopeKind;		/*���÷�Χ����*/
	
		[FlagsAttribute]
		public enum DWSCOPEKIND : uint
		{
			
				[EnumDescription("ʵ���Ҽ�")]
				SFSCOPE_LAB = 0x1,
			
				[EnumDescription("���伶")]
				SFSCOPE_ROOM = 0x2,
			
				[EnumDescription("�豸����")]
				SFSCOPE_DEVKIND = 0x4,
			
				[EnumDescription("�豸")]
				SFSCOPE_DEVID = 0x8,
			
		}

	
		public uint? dwScopeID;		/*��ΧID(����dwScopeKind���岻ͬ)*/
	
		public uint? dwIdent;		/*��ݣ�0��ʾ�����ƣ�*/
	
		public uint? dwDeptID;		/*���ţ�0��ʾ�����ƣ�*/
	
		public uint? dwGroupID;		/*ָ���û��飨0��ʾ�����ƣ�*/
	
		public uint? dwPriority;		/*���ȼ�(���ִ�������ȼ���)*/
	
		public uint? dwAuthType;		/*��Ȩ���*/
	
		[FlagsAttribute]
		public enum DWAUTHTYPE : uint
		{
			
				[EnumDescription("ʹ���ʸ����")]
				AUTHBY_USER = 0x1,
			
				[EnumDescription("����ʵ����Ŀ���")]
				AUTHBY_REARCHTEST = 0x2,
			
		}

	
		public uint? dwAuthMode;		/*��Ȩģʽ*/
	
		[FlagsAttribute]
		public enum DWAUTHMODE : uint
		{
			
				[EnumDescription("�Զ�����")]
				SFMODE_AUTO = 0x1,
			
				[EnumDescription("��Ҫ����")]
				SFMODE_NEEDAPPLY = 0x2,
			
		}

	
		public string szIntrInfo;		/*ʹ��˵��*/
	
		public uint? dwDefaultPeriod;		/*ȱ����Ч����(��)*/
	
		public string szMemo;		/*˵��*/
		};

	/*��ȡ�û�ϵͳ�����ʸ��*/
	public struct SFROLEINFOREQ
	{
		private Reserved reserved;
		
		public uint? dwSFSN;		/*���ܱ��*/
	
		public uint? dwSFRuleID;		/*����ʹ�ù���ID*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public uint? dwScopeKind;		/*���÷�Χ����*/
	
		public uint? dwScopeID;		/*��ΧID(����dwScopeKind���岻ͬ)*/
	
		public uint? dwStatus;		/*״̬*/
	
		public uint? dwAuthType;		/*��Ȩ���*/
	
		public uint? dwApplyID;		/*����ID*/
	
		public uint? dwAccNo;		/*�˺�*/
	
		public uint? dwTargetID;		/*�������ID(ʹ�����˺Ż������ĿID�ţ�*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*�û�ϵͳ�����ʸ��*/
	public struct SFROLEINFO
	{
		private Reserved reserved;
		
		public uint? dwSFSN;		/*���ܱ��*/
	
		public string szSFName;		/*��������*/
	
		public string szSFURL;		/*ʹ����ϸ���ܵ�URL*/
	
		public uint? dwSFRuleID;		/*����ʹ�ù���ID*/
	
		public string szSFRuleName;		/*��������*/
	
		public string szIntrInfo;		/*ʹ��˵��*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabName;		/*ʵ��������*/
	
		public uint? dwAuthType;		/*��Ȩ���*/
	
		public uint? dwStatus;		/*״̬��ǰ8�ֹ���Ա���״̬��*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("�Զ���ͨ����ʹ�ã�")]
				SFROLESTAT_AUTO = 0x1000,
			
				[EnumDescription("δ����")]
				SFROLESTAT_NOAPPLY = 0x2000,
			
				[EnumDescription("��˾ܾ�(����������)")]
				SFROLESTAT_CHECKREJECT = 0x4000,
			
				[EnumDescription("��Ȩ��(��������)")]
				SFROLESTAT_FORBID = 0x8000,
			
				[EnumDescription("�ѹ���")]
				SFROLESTAT_EXPIRED = 0x1000000,
			
		}

	
		public uint? dwApplyID;		/*����ID*/
	
		public uint? dwAccNo;		/*�˺�*/
	
		public string szPID;		/*ѧ����*/
	
		public string szTrueName;		/*����*/
	
		public uint? dwDeptID;		/*����ID*/
	
		public string szDeptName;		/*����*/
	
		public string szTel;		/*�绰*/
	
		public string szHandPhone;		/*�ֻ�*/
	
		public string szEmail;		/*����*/
	
		public uint? dwTutorID;		/*��ʦ�˺�*/
	
		public string szTutorName;		/*��ʦ����*/
	
		public uint? dwTargetID;		/*�������ID(ʹ�����˺Ż������ĿID�ţ�*/
	
		public string szTargetName;		/*�����������(ʹ���������������Ŀ���ƣ�*/
	
		public uint? dwApplyDate;		/*��������*/
	
		public uint? dwApplyTime;		/*����ʱ��*/
	
		public uint? dwApplyUseTime;		/*����ʹ��ʱ�䣨���ӣ�*/
	
		public uint? dwTesteeNum;		/*ʹ������*/
	
		public uint? dwUseTimes;		/*����ʹ�ô���*/
	
		public uint? dwUseMinATime;		/*����ÿ��ʹ��ʱ��(����)*/
	
		public string szApplyInfo;		/*��ϸ����*/
	
		public string szApplyURL;		/*�������뱨���URL*/
	
		public uint? dwAdminID;		/*����Ա�˺�*/
	
		public uint? dwCheckDate;		/*�������*/
	
		public uint? dwCheckTime;		/*���ʱ��*/
	
		public uint? dwPermitUseTime;		/*����ʹ��ʱ�䣨���ӣ�*/
	
		public uint? dwDeadLine;		/*�����ֹʱ��*/
	
		public string szCheckInfo;		/*������*/
	
		public uint? dwUsedTimes;		/*��ʹ�ô���*/
	
		public uint? dwUsedTime;		/*�Ѿ�ʹ��ʱ�䣨���ӣ�*/
	
		public string szMemo;		/*˵��*/
		};

	/*��ȡ������Ϣ�������*/
	public struct CODINGTABLEREQ
	{
		private Reserved reserved;
		
		public uint? dwCodeType;		/*�������*/
	
		public string szCodeSN;		/*����*/
	
		public string szCodeName;		/*��������*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*������Ϣ��*/
	public struct CODINGTABLE
	{
		private Reserved reserved;
		
		public uint? dwCodeType;		/*�������*/
	
		[FlagsAttribute]
		public enum DWCODETYPE : uint
		{
			
				[EnumDescription("ʵ��������")]
				CODE_LABKIND = 1,
			
				[EnumDescription("ʵ������Դ")]
				CODE_LABFROM = 2,
			
				[EnumDescription("ʵ���ҽ���ˮƽ")]
				CODE_LABLEVEL = 3,
			
				[EnumDescription("ѧ�ƹ���")]
				CODE_ACADEMICSUBJECT = 4,
			
				[EnumDescription("�豸��;")]
				CODE_DEVFUNC = 5,
			
				[EnumDescription("����ԤԼ����")]
				CODE_YARDRESVKIND = 6,
			
				[EnumDescription("ԤԼ����")]
				CODE_RESVKIND = 7,
			
				[EnumDescription("�����")]
				CODE_ACTIVITYKIND = 8,
			
				[EnumDescription("ԤԼ����")]
				CODE_RESVSEIVICE = 9,
			
		}

	
		public string szCodeSN;		/*����*/
	
		public string szCodeName;		/*��������*/
	
		public string szExtValue;		/*��չ*/
	
		public string szMemo;		/*��ע*/
		};

	/*��ȡ�����԰��������*/
	public struct MULTILANLIBREQ
	{
		private Reserved reserved;
		
		public uint? dwLanSN;		/*���Ա��*/
	
		public uint? dwSubSysSN;		/*��ϵͳ���*/
	
		public uint? dwTextID;		/*����ID*/
		};

	/*�����԰�*/
	public struct UNIMULTILANLIB
	{
		private Reserved reserved;
		
		public uint? dwLanSN;		/*���Ա��*/
	
		[FlagsAttribute]
		public enum DWLANSN : uint
		{
			
				[EnumDescription("����")]
				LAN_CHINESE = 1,
			
				[EnumDescription("Ӣ��")]
				LAN_ENGLISH = 2,
			
				[EnumDescription("������Ա���")]
				MAXLAN_SN = 2,
			
		}

	
		public uint? dwSubSysSN;		/*��ϵͳ���*/
	
		[FlagsAttribute]
		public enum DWSUBSYSSN : uint
		{
			
				[EnumDescription("������")]
				SUBSYS_SERVER = 1,
			
				[EnumDescription("�����")]
				SUBSYS_MANAGER = 2,
			
				[EnumDescription("ˢ����")]
				SUBSYS_CARD = 3,
			
				[EnumDescription("��ӡ����")]
				SUBSYS_DRIVER = 4,
			
		}

	
		public uint? dwTextID;		/*����ID*/
	
		[FlagsAttribute]
		public enum DWTEXTID : uint
		{
			
				[EnumDescription("TextID���ֵ")]
				MAX_TEXTID = 10000000,
			
		}

	
		public string szTextInfo;		/*��������*/
	
		public string szMemo;		/*��ע*/
		};

	/*ϵͳˢ������*/
	public struct SYSREFRESHREQ
	{
		private Reserved reserved;
		
		public uint? dwRefreshMod;		/*ˢ��ģ��(��չ)*/
		};

	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/

	/*��ȡ�ʲ��б�*/
	public struct ASSERTREQ
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*�豸ID*/
	
		public string szDevName;		/*ʵ���豸����*/
	
		public string szAssertSN;		/*�ʲ����*/
	
		public string szTagID;		/*RFID��ǩID*/
	
		public string szLabIDs;		/*ʵ����ID,����ö��Ÿ���*/
	
		public string szRoomIDs;		/*����ID,����ö��Ÿ���*/
	
		public string szKindIDs;		/*��������,����ö��Ÿ���*/
	
		public string szClassIDs;		/*�����������ID,����ö��Ÿ���*/
	
		public string szDeptIDs;		/*ѧԺID,����ö��Ÿ���*/
	
		public string szBuildingIDs;		/*¥��ID,����ö��Ÿ���*/
	
		public string szCampusIDs;		/*У��ID,����ö��Ÿ���*/
	
		public uint? dwDevStat;		/*�豸״̬*/
	
		public uint? dwProperty;		/*�豸����*/
	
		public uint? dwClassKind;		/*���(��UNIDEVCLS��Kind����)*/
	
		public uint? dwMinUnitPrice;		/*��ͼ۸�*/
	
		public uint? dwMaxUnitPrice;		/*���۸�*/
	
		public uint? dwSPurchaseDate;		/*��ʼ�ɹ�����*/
	
		public uint? dwEPurchaseDate;		/*��ֹ�ɹ�����*/
	
		public uint? dwKeeperID;		/*�������˺�*/
	
		public string szKeeperName;		/*����������*/
	
		public uint? dwProducerID;		/*������ID*/
	
		public string szProducerName;		/*����������*/
	
		public uint? dwSellerID;		/*��Ӧ��ID*/
	
		public string szSellerName;		/*��Ӧ������*/
	
		public uint? dwServiceID;		/*ά����λID*/
	
		public string szServiceName;		/*ά����λ����*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*�ʲ�������*/
	public struct ROOMCHG
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*�豸ID*/
	
		public uint? dwOldRoomID;		/*�ɷ���ID*/
	
		public string szOldRoomName;		/*�ɷ�������*/
	
		public uint? dwNewRoomID;		/*�·���ID*/
	
		public string szNewRoomName;		/*�·�������*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*�ʲ������˱��*/
	public struct KEEPERCHG
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*�豸ID*/
	
		public uint? dwOldKeeperID;		/*���������˺�*/
	
		public string szOldKeeperName;		/*������������*/
	
		public uint? dwNewKeeperID;		/*���������˺�*/
	
		public string szNewKeeperName;		/*������������*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*�ʲ���Ϣ*/
	public struct UNIASSERT
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*�豸ID*/
	
		public string szAssertSN;		/*�û����ʲ����*/
	
		public string szTagID;		/*RFID��ǩID*/
	
		public string szOriginSN;		/*ԭ��ϵ�к�*/
	
		public string szDevName;		/*ʵ���豸����*/
	
		public uint? dwUnitPrice;		/*�豸����*/
	
		public uint? dwPurchaseDate;		/*�ɹ����� YYYYMMDD*/
	
		public uint? dwDevStat;		/*�豸״̬*/
	
		public uint? dwClassID;		/*�豸�������*/
	
		public string szClassSN;		/*�豸�����*/
	
		public string szClassName;		/*�豸�������*/
	
		public uint? dwClassKind;		/*���(��UNIDEVCLS��Kind����)*/
	
		public uint? dwKindID;		/*�豸����*/
	
		public string szKindName;		/*�豸����*/
	
		public string szFuncCode;		/*�豸������;����*/
	
		public string szModel;		/*�豸�ͺ�*/
	
		public string szSpecification;		/*�豸���*/
	
		public uint? dwProperty;		/*�豸���ԣ�ǰ16��ΪUNIDEVKIND����*/
	
		public uint? dwRoomID;		/*����ID*/
	
		public string szRoomNo;		/*����ţ����ظ�,���ڹ����Ž�����ʽB-F-N,�������-¥���-�����ţ�*/
	
		public string szRoomName;		/*��������*/
	
		public string szFloorNo;		/*����¥��*/
	
		public uint? dwBuildingID;		/*¥��ID*/
	
		public string szBuildingNo;		/*��¥���(*/
	
		public string szBuildingName;		/*��¥����(*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabSN;		/*ʵ���ұ��*/
	
		public string szLabName;		/*ʵ��������*/
	
		public uint? dwDeptID;		/*����ID*/
	
		public string szDeptName;		/*����*/
	
		public uint? dwCampusID;		/*У��ID*/
	
		public string szCampusName;		/*У������*/
	
		public uint? dwCampusKind;		/*У�����ͣ���չ��*/
	
		public uint? dwKeeperID;		/*�������˺�*/
	
		public string szKeeperName;		/*����������*/
	
		public string szKeeperTel;		/*�����˵绰*/
	
		public uint? dwProducerID;		/*������ID*/
	
		public string szProducerName;		/*����������*/
	
		public uint? dwSellerID;		/*��Ӧ��ID*/
	
		public string szSellerName;		/*��Ӧ������*/
	
		public uint? dwServiceID;		/*ά����λID*/
	
		public string szServiceName;		/*ά����λ����*/
	
		public string szServiceTel;		/*ά���绰*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*�ʲ�����*/
	public struct RFIDBIND
	{
		private Reserved reserved;
		
		public uint? dwLabID;		/*ʵ����ID*/
	
		public uint? dwDevID;		/*�豸ID*/
	
		public string szTagID;		/*RFID��ǩID*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*��ȡ�ʲ��̵��*/
	public struct STOCKTAKINGREQ
	{
		private Reserved reserved;
		
		public uint? dwSTID;		/*�ʲ��̵�ID*/
	
		public uint? dwSTStat;		/*�̵�״̬*/
	
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*�ʲ��̵��*/
	public struct STOCKTAKING
	{
		private Reserved reserved;
		
		public uint? dwSTID;		/*�ʲ��̵�ID*/
	
		public uint? dwSTDate;		/*�ʲ��̵�����*/
	
		public uint? dwSTEndDate;		/*�ʲ��̵��������*/
	
		public uint? dwSTStat;		/*�̵�״̬*/
	
		[FlagsAttribute]
		public enum DWSTSTAT : uint
		{
			
				[EnumDescription("�̵���")]
				STSTAT_DOING = 0x1,
			
				[EnumDescription("�̵����")]
				STSTAT_DONE = 0x2,
			
		}

	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabName;		/*ʵ��������*/
	
		public uint? dwRoomID;		/*����ID*/
	
		public string szRoomName;		/*��������*/
	
		public uint? dwKindID;		/*��������*/
	
		public string szKindName;		/*�豸����*/
	
		public uint? dwClassID;		/*�����������ID*/
	
		public string szClassName;		/*�豸�������*/
	
		public uint? dwClassKind;		/*���(��UNIDEVCLS��Kind����)*/
	
		public uint? dwAttendantID;		/*������ID*/
	
		public string szAttendantName;		/*����������*/
	
		public uint? dwMinUnitPrice;		/*��ͼ۸�*/
	
		public uint? dwMaxUnitPrice;		/*���۸�*/
	
		public uint? dwLeaderID;		/*�̵㸺����ID*/
	
		public string szLeaderName;		/*�̵㸺��������*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*��ȡ�̵��ʲ���ϸ��*/
	public struct STDETAILREQ
	{
		private Reserved reserved;
		
		public uint? dwSTID;		/*�ʲ��̵�ID*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*�ʲ��̵���ϸ��Ϣ*/
	public struct STDETAIL
	{
		private Reserved reserved;
		
		public uint? dwSTID;		/*�ʲ��̵�ID*/
	
		public uint? dwDevID;		/*�豸ID*/
	
		public uint? dwSTStat;		/*�̵�״̬*/
	
		[FlagsAttribute]
		public enum DWSTSTAT : uint
		{
			
				[EnumDescription("�ȴ��̵�")]
				STSTAT_WAITING = 0x100,
			
				[EnumDescription("�̵�����")]
				STSTAT_OK = 0x200,
			
				[EnumDescription("�̵��������")]
				STSTAT_PROBLEM = 0x1000,
			
		}

	
		public uint? dwSTDate;		/*�ʲ��̵�����*/
	
		public uint? dwLeaderID;		/*�̵㸺����ID*/
	
		public string szLeaderName;		/*�̵㸺��������*/
	
		public uint? dwRoomID;		/*����ID*/
	
		public uint? dwAttendantID;		/*�������˺�*/
	
		public string szAttendantName;		/*����������*/
	
		public string szSTInfo;		/*�̵��������*/
	
		public string szAssertSN;		/*�û����ʲ����*/
	
		public string szTagID;		/*RFID��ǩID*/
	
		public string szOriginSN;		/*ԭ��ϵ�к�*/
	
		public string szDevName;		/*ʵ���豸����*/
	
		public uint? dwUnitPrice;		/*�豸����*/
	
		public uint? dwPurchaseDate;		/*�ɹ����� YYYYMMDD*/
	
		public uint? dwClassID;		/*�豸�������*/
	
		public string szClassSN;		/*�豸�����*/
	
		public string szClassName;		/*�豸�������*/
	
		public uint? dwClassKind;		/*���(��UNIDEVCLS��Kind����)*/
	
		public uint? dwKindID;		/*�豸����*/
	
		public string szKindName;		/*�豸����*/
	
		public string szFuncCode;		/*�豸������;����*/
	
		public string szModel;		/*�豸�ͺ�*/
	
		public string szSpecification;		/*�豸���*/
	
		public uint? dwProperty;		/*�豸���ԣ�ǰ16��ΪUNIDEVKIND����*/
	
		public string szRoomNo;		/*����ţ����ظ�,���ڹ����Ž�����ʽB-F-N,�������-¥���-�����ţ�*/
	
		public string szRoomName;		/*��������*/
	
		public string szFloorNo;		/*����¥��*/
	
		public uint? dwBuildingID;		/*¥��ID*/
	
		public string szBuildingNo;		/*��¥���(*/
	
		public string szBuildingName;		/*��¥����(*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabSN;		/*ʵ���ұ��*/
	
		public string szLabName;		/*ʵ��������*/
	
		public uint? dwDeptID;		/*����ID*/
	
		public string szDeptName;		/*����*/
	
		public uint? dwCampusID;		/*У��ID*/
	
		public string szCampusName;		/*У������*/
	
		public uint? dwCampusKind;		/*У�����ͣ���չ��*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*��ȡ�豸���ϼ�¼��*/
	public struct OUTOFSERVICEREQ
	{
		private Reserved reserved;
		
		public uint? dwOOSID;		/*�豸���ϼ�¼ID*/
	
		public uint? dwOOSStat;		/*����״̬*/
	
		public uint? dwOOSType;		/*��������*/
	
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*�����豸*/
	public struct OOSDEV
	{
		private Reserved reserved;
		
		public uint? dwOOSID;		/*�豸���ϼ�¼ID*/
	
		public uint? dwDevID;		/*�豸ID*/
	
		public string szAssertSN;		/*�û����ʲ����*/
	
		public string szDevName;		/*ʵ���豸����*/
	
		public uint? dwUnitPrice;		/*�豸����*/
	
		public uint? dwPurchaseDate;		/*�ɹ����� YYYYMMDD*/
	
		public string szKindName;		/*�豸����*/
	
		public string szRoomName;		/*��������*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabName;		/*ʵ��������*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*�豸���ϼ�¼��*/
	public struct OUTOFSERVICE
	{
		private Reserved reserved;
		
		public uint? dwOOSID;		/*�豸���ϼ�¼ID*/
	
		public uint? dwOOSStat;		/*����״̬*/
	
		[FlagsAttribute]
		public enum DWOOSSTAT : uint
		{
			
				[EnumDescription("������")]
				OOSSTAT_APPLY = 0x1,
			
				[EnumDescription("����׼")]
				OOSSTAT_APPROVE = 0x2,
			
				[EnumDescription("����׼")]
				OOSSTAT_REJECT = 0x4,
			
		}

	
		public uint? dwOOSType;		/*��������*/
	
		public string szOOSInfo;		/*������Ϣ*/
	
		public uint? dwApplyDate;		/*��������*/
	
		public uint? dwApplyID;		/*������ID*/
	
		public string szApplyName;		/*����������*/
	
		public uint? dwApproveDate;		/*��������*/
	
		public uint? dwApproveID;		/*������ID*/
	
		public string szApproveName;		/*����������*/
	
		public string szMemo;		/*˵����Ϣ*/
	
	public OOSDEV[] OOSDev;		/*CUniTable[OOSDEV]*/
		};

	/*��ȡ�����豸��ϸ��*/
	public struct OOSDETAILREQ
	{
		private Reserved reserved;
		
		public uint? dwOOSID;		/*�豸���ϼ�¼ID*/
	
		public uint? dwOOSStat;		/*����״̬*/
	
		public uint? dwOOSType;		/*��������*/
	
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*�����豸��ϸ*/
	public struct OOSDETAIL
	{
		private Reserved reserved;
		
		public uint? dwOOSID;		/*�豸���ϼ�¼ID*/
	
		public uint? dwOOSStat;		/*����״̬*/
	
		public uint? dwOOSType;		/*��������*/
	
		public string szOOSInfo;		/*������Ϣ*/
	
		public uint? dwApplyDate;		/*��������*/
	
		public uint? dwApplyID;		/*������ID*/
	
		public string szApplyName;		/*����������*/
	
		public uint? dwApproveDate;		/*��������*/
	
		public uint? dwApproveID;		/*������ID*/
	
		public string szApproveName;		/*����������*/
	
		public uint? dwDevID;		/*�豸ID*/
	
		public string szAssertSN;		/*�û����ʲ����*/
	
		public string szTagID;		/*RFID��ǩID*/
	
		public string szOriginSN;		/*ԭ��ϵ�к�*/
	
		public string szDevName;		/*ʵ���豸����*/
	
		public uint? dwUnitPrice;		/*�豸����*/
	
		public uint? dwPurchaseDate;		/*�ɹ����� YYYYMMDD*/
	
		public uint? dwClassID;		/*�豸�������*/
	
		public string szClassSN;		/*�豸�����*/
	
		public string szClassName;		/*�豸�������*/
	
		public uint? dwClassKind;		/*���(��UNIDEVCLS��Kind����)*/
	
		public uint? dwKindID;		/*�豸����*/
	
		public string szKindName;		/*�豸����*/
	
		public string szFuncCode;		/*�豸������;����*/
	
		public string szModel;		/*�豸�ͺ�*/
	
		public string szSpecification;		/*�豸���*/
	
		public uint? dwProperty;		/*�豸���ԣ�ǰ16��ΪUNIDEVKIND����*/
	
		public uint? dwRoomID;		/*����ID*/
	
		public string szRoomNo;		/*����ţ����ظ�,���ڹ����Ž�����ʽB-F-N,�������-¥���-�����ţ�*/
	
		public string szRoomName;		/*��������*/
	
		public string szFloorNo;		/*����¥��*/
	
		public uint? dwBuildingID;		/*¥��ID*/
	
		public string szBuildingNo;		/*��¥���(*/
	
		public string szBuildingName;		/*��¥����(*/
	
		public uint? dwLabID;		/*ʵ����ID*/
	
		public string szLabSN;		/*ʵ���ұ��*/
	
		public string szLabName;		/*ʵ��������*/
	
		public uint? dwDeptID;		/*����ID*/
	
		public string szDeptName;		/*����*/
	
		public uint? dwCampusID;		/*У��ID*/
	
		public string szCampusName;		/*У������*/
	
		public uint? dwCampusKind;		/*У�����ͣ���չ��*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*�豸��������*/
	public struct REPAIRAPPLY
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*����ID*/
	
		public uint? dwDevID;		/*�豸ID*/
	
		public string szDevName;		/*�豸����*/
	
		public string szAssertSN;		/*�û����ʲ����*/
	
		public uint? dwDamageDate;		/*������*/
	
		public uint? dwDamageTime;		/*��ʱ��*/
	
		public string szDamageInfo;		/*��˵��*/
	
		public uint? dwManID;		/*������ID*/
	
		public string szManName;		/*����������*/
	
		public string szMemo;		/*˵��*/
		};

	/**/
	public struct REPAIROVER
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*SID*/
	
		public uint? dwDevID;		/*�豸ID*/
	
		public string szDevName;		/*�豸����*/
	
		public string szAssertSN;		/*�û����ʲ����*/
	
		public uint? dwStatus;		/*DEVDAMAGEREC����*/
	
		public uint? dwRepareDate;		/*ά������*/
	
		public uint? dwRepareTime;		/*ά��ʱ��*/
	
		public string szRepareInfo;		/*ά��˵��*/
	
		public uint? dwRepareCost;		/*ά�޷���*/
	
		public string szFundsNo1;		/*���ѿ����1*/
	
		public uint? dwPay1;		/*���ѿ�1֧��*/
	
		public string szFundsNo2;		/*���ѿ����2*/
	
		public uint? dwPay2;		/*���ѿ�2֧��*/
	
		public string szRepareCom;		/*ά�޵�λ*/
	
		public string szRepareComTel;		/*ά�޵�λ��ϵ��ʽ*/
	
		public string szMemo;		/*˵��*/
		};

	/**/
	public struct REPAIRCANCEL
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*SID*/
	
		public uint? dwDevID;		/*�豸ID*/
	
		public string szDevName;		/*�豸����*/
	
		public string szAssertSN;		/*�û����ʲ����*/
	
		public uint? dwDevStat;		/*�������豸״̬*/
	
		public string szCancelInfo;		/*����˵��*/
	
		public string szMemo;		/*˵��*/
		};

	/**/
	public struct COMPANYREQ
	{
		private Reserved reserved;
		
		public uint? dwComID;		/*��λID*/
	
		public uint? dwComKind;		/*��λ����*/
	
		public uint? dwProperty;		/*����*/
	
		public string szComName;		/*��λ��*/
	
		public string szSearchKey;		/*�����ؼ���*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/**/
	public struct UNICOMPANY
	{
		private Reserved reserved;
		
		public uint? dwComID;		/*��λID*/
	
		public string szComName;		/*��λ��*/
	
		public uint? dwComKind;		/*��λ����*/
	
		[FlagsAttribute]
		public enum DWCOMKIND : uint
		{
			
				[EnumDescription("�豸������")]
				COM_PRODUCER = 0x1,
			
				[EnumDescription("������")]
				COM_SELLER = 0x2,
			
				[EnumDescription("ά����λ")]
				COM_SERVICE = 0x4,
			
		}

	
		public uint? dwProperty;		/*����*/
	
		public string szTrueName;		/*��ϵ������*/
	
		public string szJobTitle;		/*ְ��*/
	
		public string szTel;		/*�绰*/
	
		public string szHandPhone;		/*�ֻ�*/
	
		public string szEmail;		/*����*/
	
		public string szQQ;		/*QQ*/
	
		public string szAddress;		/*��ַ*/
	
		public string szOtherContact;		/*������ϵ��ʽ*/
	
		public string szKeyWords;		/*�ؼ���*/
	
		public string szMemo;		/*��ע*/
		};

	/*��ȡ�豸��ʷ������ϸ��*/
	public struct ASSERTLOGREQ
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*�豸���ϼ�¼ID*/
	
		public uint? dwDevID;		/*�豸ID*/
	
		public uint? dwOpKind;		/*��־����*/
	
		public uint? dwOperatorID;		/*����ԱID*/
	
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*�豸��ʷ����*/
	public struct ASSERTLOG
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*��־ID*/
	
		public uint? dwOpKind;		/*��־����*/
	
		[FlagsAttribute]
		public enum DWOPKIND : uint
		{
			
				[EnumDescription("�ʲ��Ǽ�")]
				ASSERTLOG_WAREHOUSING = 10,
			
				[EnumDescription("�ʲ�λ�ñ��")]
				ASSERTLOG_CHGROOM = 20,
			
				[EnumDescription("�ʲ������˱��")]
				ASSERTLOG_CHGKEEPER = 30,
			
				[EnumDescription("�ʲ��̵�")]
				ASSERTLOG_STOCKTAKING = 40,
			
				[EnumDescription("�ʲ�ά��")]
				ASSERTLOG_REPARING = 50,
			
				[EnumDescription("�ʲ�����")]
				ASSERTLOG_OOS = 100,
			
		}

	
		public uint? dwOpDate;		/*����*/
	
		public uint? dwOpTime;		/*ʱ��*/
	
		public string szOpDetail;		/*��ϸ��Ϣ*/
	
		public uint? dwOperatorID;		/*����ԱID*/
	
		public string szOperatorName;		/*����Ա����*/
	
		public uint? dwDevID;		/*�豸ID*/
	
		public string szAssertSN;		/*�û����ʲ����*/
	
		public string szDevName;		/*ʵ���豸����*/
	
		public uint? dwClassID;		/*�豸�������*/
	
		public string szClassName;		/*�豸�������*/
	
		public uint? dwClassKind;		/*���(��UNIDEVCLS��Kind����)*/
	
		public uint? dwKindID;		/*�豸����*/
	
		public string szKindName;		/*�豸����*/
	
		public string szFuncCode;		/*�豸������;����*/
	
		public string szModel;		/*�豸�ͺ�*/
	
		public string szSpecification;		/*�豸���*/
		};

	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/

	/*���ڷ���*/
	public struct ATTENDROOM
	{
		private Reserved reserved;
		
		public uint? dwRoomID;		/*����ID*/
	
		public string szRoomName;		/*��������*/
	
		public string szRoomNo;		/*�����*/
	
		public string szFloorNo;		/*����¥��*/
	
		public uint? dwBuildingID;		/*¥��ID*/
	
		public string szBuildingNo;		/*��¥���(*/
	
		public string szBuildingName;		/*��¥����(*/
	
		public string szMemo;		/*��ע*/
		};

	/*��ȡ���ڹ���������*/
	public struct ATTENDRULEREQ
	{
		private Reserved reserved;
		
		public uint? dwAttendID;		/*���ڹ���ID*/
	
		public string szAttendName;		/*���ڹ�������*/
	
		public uint? dwKind;		/*��������*/
	
		public string szRoomIDs;		/*����ID,����ö��Ÿ���*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*���ڹ���*/
	public struct ATTENDRULE
	{
		private Reserved reserved;
		
		public uint? dwAttendID;		/*���ڹ���ID*/
	
		public string szAttendName;		/*���ڹ�������*/
	
		public uint? dwKind;		/*��������*/
	
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
		public uint? dwGroupID;		/*��ID*/
	
		public uint? dwOpenRuleSN;		/*���Ź�����*/
	
		public uint? dwEarlyInTime;		/*�������ʱ��(HHMM)*/
	
		public uint? dwLateInTime;		/*�������ʱ��(HHMM)*/
	
		public uint? dwEarlyOutTime;		/*�����뿪ʱ��(HHMM),С�ڽ���ʱ���������*/
	
		public uint? dwLateOutTime;		/*�����뿪ʱ��(HHMM),С�ڽ���ʱ���������*/
	
		public uint? dwMinStayTime;		/*����ͣ��ʱ��*/
	
	public ATTENDROOM[] AttendRoom;		/*ATTENDROOM��*/
	
		public string szMemo;		/*��ע*/
		};

	/*��ȡ���ڼ�¼�������*/
	public struct ATTENDRECREQ
	{
		private Reserved reserved;
		
		public uint? dwAttendID;		/*���ڹ���ID*/
	
		public string szAttendName;		/*���ڹ�������*/
	
		public uint? dwKind;		/*��������*/
	
		public string szRoomIDs;		/*����ID,����ö��Ÿ���*/
	
		public uint? dwAccNo;		/*�˺�*/
	
		public uint? dwAttendStat;		/*����״̬*/
	
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*���ڼ�¼*/
	public struct ATTENDREC
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*��ˮ��*/
	
		public uint? dwAttendID;		/*���ڹ���ID*/
	
		public string szAttendName;		/*���ڹ�������*/
	
		public uint? dwAccNo;		/*�˺�*/
	
		public string szPID;		/*ѧ����*/
	
		public string szTrueName;		/*����*/
	
		public uint? dwSex;		/*�Ա�*/
	
		public uint? dwAttendDate;		/*��������*/
	
		public uint? dwAttendStat;		/*����״̬(������UNIRESVREC)*/
	
		public uint? dwRoomID;		/*����ID*/
	
		public string szRoomName;		/*��������*/
	
		public uint? dwInTime;		/*����ʱ��(HHMM)*/
	
		public uint? dwOutTime;		/*�뿪ʱ��(HHMM)*/
	
		public uint? dwLatestInTime;		/*�������ʱ��*/
	
		public uint? dwStayMin;		/*ͣ��ʱ��(����)*/
	
		public uint? dwCardTimes;		/*ˢ������*/
	
		public uint? dwRFLID;		/*request for leave ID*/
	
		public string szMemo;		/*��ע*/
		};

	/*������Ϣ*/
	public struct ATTENDINFO
	{
		private Reserved reserved;
		
		public uint? dwAttendMode;		/*���ڽ���ģʽ*/
	
		[FlagsAttribute]
		public enum DWATTENDMODE : uint
		{
			
				[EnumDescription("ǩ������")]
				ATTENDMODE_IN = 1,
			
				[EnumDescription("�˳�")]
				ATTENDMODE_OUT = 2,
			
				[EnumDescription("�ٴν���")]
				ATTENDMODE_REIN = 4,
			
				[EnumDescription("ˢ���˻��Ž�����ȷ���ǽ����˳�")]
				ATTENDMODE_UNIKNOWN = 8,
			
		}

	
		public uint? dwAccNo;		/*�˺�*/
	
		public uint? dwRoomID;		/*����ID*/
	
		public uint? dwAttendDate;		/*��������*/
	
		public uint? dwSID;		/*��ˮ��*/
	
		public uint? dwAttendID;		/*���ڹ���ID*/
	
		public string szAttendName;		/*���ڹ�������*/
	
		public uint? dwAttendStat;		/*����״̬(������UNIRESVREC)*/
	
		public uint? dwInTime;		/*����ʱ��(HHMM)*/
	
		public uint? dwOutTime;		/*�뿪ʱ��(HHMM)*/
	
		public uint? dwLatestInTime;		/*�������ʱ��*/
	
		public uint? dwStayMin;		/*ͣ��ʱ��(����)*/
	
		public uint? dwCardTimes;		/*ˢ������*/
	
		public uint? dwRFLID;		/*request for leave ID*/
	
		public string szMemo;		/*��ע*/
		};

	/*��ȡ����ͳ�Ƶ������*/
	public struct ATTENDSTATREQ
	{
		private Reserved reserved;
		
		public uint? dwAttendID;		/*���ڹ���ID*/
	
		public uint? dwAccNo;		/*�˺�*/
	
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*����ͳ��*/
	public struct ATTENDSTAT
	{
		private Reserved reserved;
		
		public uint? dwAccNo;		/*�˺�*/
	
		public string szPID;		/*ѧ����*/
	
		public string szTrueName;		/*����*/
	
		public uint? dwSex;		/*�Ա�*/
	
		public uint? dwTotalTimes;		/*�ܴ���*/
	
		public uint? dwAttendTimes;		/*���ڴ���*/
	
		public uint? dwAbsentTimes;		/*ȱ�ڴ���*/
	
		public uint? dwLateTimes;		/*�ٵ�����*/
	
		public uint? dwLeaveTimes;		/*���˴���*/
	
		public uint? dwLLTimes;		/*�ٵ������˴���*/
	
		public uint? dwSickTimes;		/*���ٴ���*/
	
		public uint? dwPrivateTimes;		/*�¼ٴ���*/
	
		public uint? dwUseLessTimes;		/*ʹ��ʱ�䲻������*/
	
		public uint? dwLeaveNoCardTimes;		/*δˢ���뿪����*/
	
		public uint? dwTotalMin;		/*������ʱ�䣨���ӣ�*/
		};

	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/

	/*��ϵͳ��Ϣ*/
	public struct SUBSYS
	{
		private Reserved reserved;
		
		public uint? dwStaSN;		/*��ϵͳ���*/
	
		public string szSubSysName;		/*��ϵͳ����*/
	
		public uint? dwStatus;		/*״̬*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("����")]
				SUBSYSSTAT_OFFLINE = 0x0,
			
				[EnumDescription("����")]
				SUBSYSSTAT_ONLINE = 0x1,
			
		}

	
		public string szVersion;		/*��ϵͳ�������汾*/
	
		public string szIP;		/*IP��ַ*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*��ϵͳ��¼����*/
	public struct SUBSYSLOGINREQ
	{
		private Reserved reserved;
		
		public uint? dwStaSN;		/*��ϵͳ���*/
	
		public string szVersion;		/*��ϵͳ�������汾*/
	
		public string szKey;		/*��Կ(��չ)*/
	
		public string szIP;		/*IP��ַ*/
	
		public string szMAC;		/*������ַ*/
	
		public uint? dwOldSessionID;		/*�ϴη����sessionֵ*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*��ϵͳ��¼Ӧ��*/
	public struct SUBSYSLOGINRES
	{
		private Reserved reserved;
		
		public uint? dwSessionID;		/*�����������SessionID*/
	
		public string ExtInfo;		/*������չ��Ϣ*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*��ϵͳ�˳�����*/
	public struct SUBSYSLOGOUTREQ
	{
		private Reserved reserved;
		
		public uint? dwSessionID;		/*�����������SessionID*/
	
		public uint? dwStaSN;		/*��ϵͳ���*/
		};

	/*IC�ռ�ʹ�ü�¼�ϴ�*/
	public struct ICUSERECUP
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*��ˮ��*/
	
		public uint? dwStaSN;		/*��ϵͳ���*/
	
		public uint? dwSubStaSN;		/*��վ����*/
	
		public string szLogonName;		/*ѧ����*/
	
		public string szTrueName;		/*����*/
	
		public uint? dwUseDate;		/*ʹ������*/
	
		public uint? dwResvBTime;		/*ԤԼ��ʼʱ��*/
	
		public uint? dwResvETime;		/*ԤԼ����ʱ��*/
	
		public uint? dwRealInTime;		/*ʵ�ʿ�ʼʱ��*/
	
		public uint? dwRealOutTime;		/*ʵ�ʽ���ʱ��*/
	
		public uint? dwUseMinutes;		/*ʹ��ʱ��(����)*/
	
		public string szUseDev;		/*ʹ���豸*/
	
		public uint? dwDevClsKind;		/*�������(����UNIDEVCLS:dwKind���壩*/
	
		public uint? dwDevKind;		/*�豸����*/
	
		public uint? dwUseMode;		/*ʹ�÷������ο�UNIRESERVE���壩*/
	
		public uint? dwPurpose;		/*��;���ο�UNIRESERVE���壩*/
	
		public uint? dwRealCost;		/*ʵ�ʽ��ɷ���(��)*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*��ӡ��ӡɨ���¼�ϴ�*/
	public struct PRINTRECUP
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*��ˮ��*/
	
		public uint? dwStaSN;		/*��ϵͳ���*/
	
		public uint? dwSubStaSN;		/*��վ����*/
	
		public string szLogonName;		/*ѧ����*/
	
		public string szTrueName;		/*����*/
	
		public uint? dwUseDate;		/*ʹ������*/
	
		public uint? dwUseTime;		/*ʹ��ʱ��*/
	
		public string szUseDev;		/*ʹ���豸*/
	
		public uint? dwPages;		/*��ӡ����(��ɨ���С��*/
	
		public uint? dwPaperType;		/*ֽ��*/
	
		public uint? dwPrintType;		/*��ӡ����*/
	
		public uint? dwProperty;		/*����*/
	
		public uint? dwRealCost;		/*ʵ�ʽ��ɷ���(��)*/
	
		public uint? dwUnitFee;		/*����*/
	
		public uint? dwPaperNum;		/*ֽ����*/
	
		public uint? dwMaterialFee;		/*���Ϸ�*/
	
		public uint? dwManualFee;		/*�˹���*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*ͼ�鳬�ڽɷѼ�¼�ϴ�*/
	public struct BOOKOVERDUERECUP
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*��ˮ��*/
	
		public uint? dwStaSN;		/*��ϵͳ���*/
	
		public uint? dwSubStaSN;		/*��վ����*/
	
		public string szLogonName;		/*ѧ����*/
	
		public string szTrueName;		/*����*/
	
		public uint? dwUseDate;		/*ʹ������*/
	
		public uint? dwUseTime;		/*ʹ��ʱ��*/
	
		public string szUseDev;		/*ʹ���豸*/
	
		public string szBookName;		/*����*/
	
		public uint? dwRealCost;		/*ʵ�ʽ��ɷ���(��)*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*ΥԼ��¼�ϴ�*/
	public struct BREACHRECUP
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*��ˮ��*/
	
		public uint? dwStaSN;		/*��ϵͳ���*/
	
		public uint? dwSubStaSN;		/*��վ����*/
	
		public string szLogonName;		/*ѧ����*/
	
		public string szTrueName;		/*����*/
	
		public uint? dwOccurDate;		/*ΥԼ����*/
	
		public uint? dwOccurTime;		/*ΥԼʱ��*/
	
		public string szBreachName;		/*ΥԼ������*/
	
		public uint? dwPunishScore;		/*���η���*/
	
		public uint? dwTotalScore;		/*�ۼƷ���*/
	
		public uint? dwThresholdScore;		/*�ﵽ������׼�ķ���*/
	
		public uint? dwStatus;		/*����״̬*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("��Ч")]
				BREACHSTAT_VALID = 1,
			
				[EnumDescription("����Աȡ��")]
				BREACHSTAT_CANCEL = 2,
			
				[EnumDescription("�ѹ�����������")]
				BREACHSTAT_OVER = 4,
			
		}

	
		public string szPunishName;		/*������ʽ*/
	
		public uint? dwPStartDate;		/*������ʼʱ��*/
	
		public uint? dwPEndDate;		/*��������ʱ��*/
	
		public string szMemo;		/*˵����Ϣ*/
		};

	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/

	/*��ȡ���޼䵱ǰ״̬����*/
	public struct STUDYROOMSTATREQ
	{
		private Reserved reserved;
		
		public uint? dwRoomKinds;		/*���޼��������*/
	
		public string szBuildingNo;		/*��¥���*/
		};

	/*���޼䵱ǰ״̬*/
	public struct STUDYROOMSTAT
	{
		private Reserved reserved;
		
		public uint? dwRoomKind;		/*���޼�����*/
	
		[FlagsAttribute]
		public enum DWROOMKIND : uint
		{
			
				[EnumDescription("�������޼�")]
				SROOMKIND_SINGLE = 0x101,
			
				[EnumDescription("�����(����������)")]
				SROOMKIND_GROUP = 0x201,
			
				[EnumDescription("���Ż��")]
				SROOMKIND_ACTIVITY = 0x401,
			
				[EnumDescription("�������޼�")]
				SROOMKIND_MULTIPLE = 0x801,
			
		}

	
		public uint? dwTotalNum;		/*����*/
	
		public uint? dwIdleNum;		/*������*/
		};

	/*��ȡ��λ��ǰ״̬����*/
	public struct SEATSTATREQ
	{
		private Reserved reserved;
		
		public string szBuildingNo;		/*��¥���*/
	
		public string szFloorNo;		/*����¥��*/
		};

	/*��λ��ǰ״̬*/
	public struct SEATSTAT
	{
		private Reserved reserved;
		
		public string szRoomNo;		/*�����*/
	
		public string szRoomName;		/*��������*/
	
		public uint? dwTotalNum;		/*����*/
	
		public uint? dwIdleNum;		/*������*/
		};

	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/

	/*��ȡ����ʵ����������*/
	public struct RTDATAREQ
	{
		private Reserved reserved;
		
		public uint? dwBeginDate;		/*ԤԼ��ʼ����*/
	
		public uint? dwEndDate;		/*ԤԼ��������*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*��ȡ����ʵ������*/
	public struct RTDATA
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*ԤԼ��*/
	
		public uint? dwProperty;		/*ԤԼ����*/
	
		public uint? dwDevID;		/*�豸ID*/
	
		public string szAssertSN;		/*�ʲ����*/
	
		public string szTestName;		/*����ʵ������*/
	
		public uint? dwBeginTime;		/*ԤԼ��ʼʱ��*/
	
		public uint? dwEndTime;		/*ԤԼ����ʱ��*/
	
		public uint? dwOwner;		/*ʹ����(������)*/
	
		public string szPID;		/*ʹ����ѧ����*/
	
		public string szOwnerName;		/*ʹ��������*/
	
		public uint? dwIdent;		/*ʹ������ݣ�У�ڣ������У���ʦ��У�⣩*/
	
		public uint? dwUserDeptID;		/*ʹ���˲���ID*/
	
		public string szUserDeptName;		/*ʹ���˲���*/
	
		public string szTel;		/*�绰*/
	
		public string szHandPhone;		/*�ֻ�*/
	
		public string szEmail;		/*����*/
	
		public uint? dwRTID;		/*����ʵ����ĿID*/
	
		public uint? dwRTKind;		/*��������*/
	
		public string szRTName;		/*����ʵ������*/
	
		public uint? dwSampleNum;		/*��Ʒ��*/
	
		public uint? dwManID;		/*����ԱID*/
	
		public string szManName;		/*����Ա����*/
	
		public uint? dwReceivableCost;		/*Ӧ�ɷ���*/
	
		public uint? dwRealCost;		/*ʵ�ʽ��ɷ���*/
	
		public string szDescription;		/*ʵ������*/
		};

	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/

	/**/
	public struct STALOGINREQ
	{
		private Reserved reserved;
		
		public string szLicSN;		/*���ϵ�к�*/
	
		public string szVersion;		/*�汾*/
	
		public string szIP;		/*IP��ַ*/
	
		public string szMAC;		/*������ַ*/
	
		public string szKey;		/*��Կ(��չ)*/
	
		public uint? dwOldSessionID;		/*�ϴη����sessionֵ*/
		};

	/**/
	public struct STALOGINRES
	{
		private Reserved reserved;
		
		public uint? dwStaID;		/*�ڵ�ID*/
	
		public uint? dwSessionID;		/**/
	
	public UNILICENSE LicInfo;		/*��������Ȩ��ϢUNILICENSE*/
	
		public string szMemo;		/*��ע*/
		};

	/**/
	public struct STALOGOUTREQ
	{
		private Reserved reserved;
		
		public uint? dwSessionID;		/**/
	
		public string szLicSN;		/*���ϵ�к�*/
		};

	/**/
	public struct HANDSHAKEREQ
	{
		private Reserved reserved;
		
		public uint? dwChgFlag;		/*���ر�����޸ĸ��±�־*/
		};

	/**/
	public struct HANDSHAKERES
	{
		private Reserved reserved;
		
		public uint? dwChgFlag;		/*���������صĵ��޸ĸ��±�־*/
	
		public string szResChgStat;		/*���صĶ�Ӧ��Ϣ���ޱ�־���ַ�0��ʾ�ޣ��ַ�1��ʾ��*/
	
		[FlagsAttribute]
		public enum SZRESCHGSTAT : uint
		{
			
				[EnumDescription("���������Ϣ(CUniStruct[UNILICENSE])")]
				CHGINDEX_LICENSE = 1,
			
				[EnumDescription("���°汾��Ϣ(CUniStruct[UNIPRODUCT])")]
				CHGINDEX_NEWVER = 2,
			
				[EnumDescription("����µ�������")]
				MAXCHG_TYPE = 10,
			
		}

		};

	/*ģ������Ϣ�ϴ�*/
	public struct MODMONIUP
	{
		private Reserved reserved;
		
		public uint? dwModSN;		/*��ض˱�ţ�������Ϊ0�����¶���+StaSN*65536 + ��ض˱��)*/
	
		public string szModName;		/*��ض�����*/
	
		public uint? dwStatus;		/*��״̬*/
	
		public uint? dwStartTime;		/*��״̬��ʼʱ��*/
	
		public string szStatInfo;		/*״̬˵��*/
	
		public string szMemo;		/*��ע*/
		};

	/*���ָ���ϴ�*/
	public struct MONINDEXUP
	{
		private Reserved reserved;
		
		public uint? dwModSN;		/*��ض˱�ţ�������Ϊ0������Ϊ����ı�Ż�ID��)*/
	
		public uint? dwMoniSN;		/*���ָ����*/
	
		public string szIndexName;		/*���ָ������*/
	
		public uint? dwNormalValue;		/*����ֵ*/
	
		public uint? dwCurValue;		/*��ǰֵ*/
	
		public uint? dwStatus;		/*״̬*/
	
		public uint? dwNormalTime;		/*����ʱ��(����)*/
	
		public uint? dwAbnormalTime;		/*�쳣ʱ��(����)*/
	
		public uint? dwAbnormalTimes;		/*�쳣����*/
	
		public uint? dwStartTime;		/*��״̬��ʼʱ��*/
	
		public string szStatInfo;		/*״̬˵��*/
	
		public string szMemo;		/*��ע*/
		};

	/*��ؼ�¼�ϴ�*/
	public struct MONIRECUP
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*������ˮ��*/
	
		public uint? dwModSN;		/*��ض˱�ţ�������Ϊ0������Ϊ����ı�Ż�ID��)*/
	
		public uint? dwMoniSN;		/*���ָ����*/
	
		public string szIndexName;		/*���ָ������*/
	
		public uint? dwCurValue;		/*��ǰֵ*/
	
		public uint? dwOccurDate;		/*��ʼ����*/
	
		public uint? dwOccurTime;		/*����ʱ��*/
	
		public uint? dwStatus;		/*״̬*/
	
		public uint? dwNormalTime;		/*����ʱ��(����)*/
	
		public uint? dwAbnormalTime;		/*�쳣ʱ��(����)*/
	
		public uint? dwAbnormalTimes;		/*�쳣����*/
	
		public string szStatInfo;		/*״̬˵��*/
		};

	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/

	/*��ȡ�����Ϣȱʡֵ*/
	public struct MONIREQ
	{
		private Reserved reserved;
		
		public uint? dwModKind;		/*��ģ�����MODKIND_XXX����)*/
	
		public uint? dwStaSN;		/*վ����*/
	
		public uint? dwModSN;		/*��ض˱��*/
	
		public uint? dwStatus;		/*״̬*/
	
		public uint? dwReqProp;		/*���󸽼�����*/
	
		[FlagsAttribute]
		public enum DWREQPROP : uint
		{
			
				[EnumDescription("��Ҫָ���б�")]
				MONIREQ_NEEDINDEXTBL = 0x1,
			
		}

	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*���ָ����*/
	public struct MONINDEX
	{
		private Reserved reserved;
		
		public uint? dwModSN;		/*��ض˱�ţ�������Ϊ0������Ϊ����ı�Ż�ID��)*/
	
		public string szModName;		/*��ض�����*/
	
		public uint? dwMoniSN;		/*���ָ����*/
	
		[FlagsAttribute]
		public enum DWMONISN : uint
		{
			
				[EnumDescription("����")]
				MONIINDEX_NET = 0x100,
			
				[EnumDescription("����״̬")]
				NET_ADAPTERSTAT = (MONIINDEX_NET + 1),
			
				[EnumDescription("IP��ַ")]
				NET_IP = (MONIINDEX_NET + 2),
			
				[EnumDescription("��������")]
				NET_MASK = (MONIINDEX_NET + 3),
			
				[EnumDescription("����")]
				NET_GATE = (MONIINDEX_NET + 4),
			
				[EnumDescription("��������")]
				NET_INFLOW = (MONIINDEX_NET + 5),
			
				[EnumDescription("��������")]
				NET_OUTFLOW = (MONIINDEX_NET + 6),
			
				[EnumDescription("������")]
				NET_CONNS = (MONIINDEX_NET + 10),
			
				[EnumDescription("��ؽ���������")]
				NET_MYCONNS = (MONIINDEX_NET + 11),
			
				[EnumDescription("����������(״̬ΪESTABLISHED���������)")]
				NET_OTHERCONNS = (MONIINDEX_NET + 12),
			
				[EnumDescription("DNS")]
				NET_DNS = (MONIINDEX_NET + 13),
			
				[EnumDescription("CPU")]
				MONIINDEX_CPU = 0x200,
			
				[EnumDescription("CPU��ʹ����")]
				CPU_SYSUSAGE = (MONIINDEX_CPU + 1),
			
				[EnumDescription("�ҵ�ʹ����")]
				CPU_MYUSAGE = (MONIINDEX_CPU + 2),
			
				[EnumDescription("Ӳ��")]
				MONIINDEX_HD = 0x400,
			
				[EnumDescription("���ô��̿ռ�(M)")]
				HD_FREESIZE = (MONIINDEX_HD + 1),
			
				[EnumDescription("�ڴ�")]
				MONIINDEX_MEM = 0x800,
			
				[EnumDescription("ϵͳ�����ڴ�(M)")]
				MEM_SYSFREE = (MONIINDEX_MEM + 1),
			
				[EnumDescription("�ҵ�ʹ����(M)")]
				MEM_MYUSE = (MONIINDEX_MEM + 2),
			
				[EnumDescription("������")]
				MONIINDEX_MYSELF = 0x1000,
			
				[EnumDescription("��ʷ״̬")]
				MY_HISTORY = (MONIINDEX_MYSELF + 1),
			
				[EnumDescription("�����")]
				MY_HANDLE = (MONIINDEX_MYSELF + 2),
			
				[EnumDescription("GDI����")]
				MY_GDI = (MONIINDEX_MYSELF + 3),
			
				[EnumDescription("����ǩ��")]
				MY_SIGNATURE = (MONIINDEX_MYSELF + 4),
			
				[EnumDescription("���ݿ�״̬")]
				DB_STAT = (MONIINDEX_MYSELF + 5),
			
				[EnumDescription("���ݿ����")]
				DB_OP = (MONIINDEX_MYSELF + 6),
			
				[EnumDescription("��֤����")]
				REQ_AUTH = (MONIINDEX_MYSELF + 7),
			
				[EnumDescription("������״̬")]
				THIRD_STAT = (MONIINDEX_MYSELF + 8),
			
				[EnumDescription("����������")]
				THIRD_OP = (MONIINDEX_MYSELF + 9),
			
				[EnumDescription("��������״̬")]
				SSC_STAT = (MONIINDEX_MYSELF + 10),
			
				[EnumDescription("�������Ĳ���")]
				SSC_OP = (MONIINDEX_MYSELF + 11),
			
				[EnumDescription("������������״̬")]
				UNISRV_STAT = (MONIINDEX_MYSELF + 12),
			
				[EnumDescription("�����������Ĳ���")]
				UNISRV_OP = (MONIINDEX_MYSELF + 13),
			
				[EnumDescription("���״̬")]
				LICENSE_STAT = (MONIINDEX_MYSELF + 14),
			
				[EnumDescription("����״̬")]
				SERVICE_STAT = (MONIINDEX_MYSELF + 15),
			
				[EnumDescription("��ϵͳ����״̬")]
				SUBCON_STAT = (MONIINDEX_MYSELF + 16),
			
				[EnumDescription("�߳�״̬")]
				THREAD_STAT = (MONIINDEX_MYSELF + 17),
			
		}

	
		public string szIndexName;		/*���ָ������*/
	
		public uint? dwNormalValue;		/*����ֵ*/
	
		public uint? dwCurValue;		/*��ǰֵ*/
	
		public uint? dwStatus;		/*״̬*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("����")]
				MONISTAT_OK = 1,
			
				[EnumDescription("����")]
				MONISTAT_INFO = 2,
			
				[EnumDescription("����")]
				MONISTAT_WARNNING = 4,
			
				[EnumDescription("����")]
				MONISTAT_ERROR = 8,
			
		}

	
		public uint? dwNormalTime;		/*����ʱ��(����)*/
	
		public uint? dwAbnormalTime;		/*�쳣ʱ��(����)*/
	
		public uint? dwAbnormalTimes;		/*�쳣����*/
	
		public uint? dwStartTime;		/*��״̬��ʼʱ��*/
	
		public string szStatInfo;		/*״̬˵��*/
	
		public string szMemo;		/*��ע*/
		};

	/*ģ������Ϣ*/
	public struct MODMONI
	{
		private Reserved reserved;
		
		public uint? dwModSN;		/*��ض˱�ţ�������Ϊ0�����¶���+StaSN*65536 + ��ض˱��)*/
	
		[FlagsAttribute]
		public enum DWMODSN : uint
		{
			
				[EnumDescription("�����")]
				MODKIND_SERVER = 0x1000000,
			
				[EnumDescription("�Ž�������")]
				MODKIND_DCS = 0x2000000,
			
				[EnumDescription("�ֳ������նˣ�ˢ���ˣ�")]
				MODKIND_STT = 0x4000000,
			
				[EnumDescription("�ͻ���")]
				MODKIND_CLT = 0x8000000,
			
		}

	
		public string szModName;		/*��ض�����*/
	
		public uint? dwStatus;		/*��״̬*/
	
		public uint? dwStartTime;		/*��״̬��ʼʱ��*/
	
		public string szStatInfo;		/*״̬˵��*/
	
	public MONINDEX[] MoniIndexTbl;		/*ָ���б�*/
	
		public string szMemo;		/*��ע*/
		};

	/*��ȡ�����Ϣȱʡֵ*/
	public struct MONIRECREQ
	{
		private Reserved reserved;
		
		public uint? dwStartDate;		/*��ʼ����*/
	
		public uint? dwEndDate;		/*��������*/
	
		public uint? dwModKind;		/*��ģ�����MODKIND_XXX����)*/
	
		public uint? dwStaSN;		/*վ����*/
	
		public uint? dwModSN;		/*ģ����*/
	
		public uint? dwMoniSN;		/*���ָ����*/
	
		public uint? dwStatus;		/*״̬*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*��ؼ�¼*/
	public struct MONIREC
	{
		private Reserved reserved;
		
		public uint? dwModSN;		/*��ض˱�ţ�������Ϊ0������Ϊ����ı�Ż�ID��)*/
	
		public string szModName;		/*��ض�����*/
	
		public uint? dwMoniSN;		/*���ָ����*/
	
		public string szIndexName;		/*���ָ������*/
	
		public uint? dwNormalValue;		/*����ֵ*/
	
		public uint? dwCurValue;		/*��ǰֵ*/
	
		public uint? dwOccurDate;		/*��ʼ����*/
	
		public uint? dwOccurTime;		/*����ʱ��*/
	
		public uint? dwStatus;		/*״̬*/
	
		public uint? dwNormalTime;		/*����ʱ��(����)*/
	
		public uint? dwAbnormalTime;		/*�쳣ʱ��(����)*/
	
		public uint? dwAbnormalTimes;		/*�쳣����*/
	
		public string szStatInfo;		/*״̬˵��*/
		};

	/*������*/
	public struct MONIDEALERR
	{
		private Reserved reserved;
		
		public uint? dwModSN;		/*��ض˱�ţ�������Ϊ0������Ϊ����ı�Ż�ID��)*/
	
		public uint? dwMoniSN;		/*���ָ����*/
	
		public uint? dwNormalValue;		/*����ֵ*/
	
		public uint? dwCurValue;		/*��ǰֵ*/
	
		public string szDealInfo;		/*˵����Ϣ*/
		};

	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/

	/*����������ڵ�ͨ�Ų���*/
	public struct UNISTAPARAM
	{
		private Reserved reserved;
		
		public uint? dwReqUID;		/*����UID*/
	
		public string szReqData;		/*��������*/
	
		public uint? dwResCode;		/*�����룬0��ʾ�ɹ�*/
	
		public string szResData;		/*��������*/
		};

	/*��ɹ���ģ����Ϣ*/
	public struct LICMOD
	{
		private Reserved reserved;
		
		public uint? dwFuncSN;		/*����ģ����*/
	
		[FlagsAttribute]
		public enum DWFUNCSN : uint
		{
			
				[EnumDescription("�Ž�����")]
				LICMOD_DOORCTRL = 1,
			
				[EnumDescription("PC��")]
				LICMOD_PC = 2,
			
				[EnumDescription("��Ƶ���")]
				LICMOD_VIDEOCTRL = 3,
			
				[EnumDescription("����ϵͳ")]
				LICMOD_SOUNDCTRL = 4,
			
				[EnumDescription("��Դ����")]
				LICMOD_POWERCTRL = 5,
			
				[EnumDescription("�ռ����")]
				LICMOD_COMMONS = 6,
			
				[EnumDescription("ƻ����")]
				LICMOD_MAC = 7,
			
				[EnumDescription("��λ����")]
				LICMOD_SEAT = 8,
			
				[EnumDescription("�����")]
				LICMOD_ACTIVITY = 9,
			
				[EnumDescription("����ʵ��")]
				LICMOD_OPENTEST = 10,
			
				[EnumDescription("�豸���")]
				LICMOD_LOAN = 11,
			
				[EnumDescription("��ѧ����")]
				LICMOD_TEACHING = 12,
			
				[EnumDescription("��ʦ����")]
				LICMOD_TELECAST = 13,
			
				[EnumDescription("ʵ�鱨��")]
				LICMOD_REPORT = 14,
			
				[EnumDescription("�ʲ�����")]
				LICMOD_ASSET = 15,
			
				[EnumDescription("ʵ�����ݹ���")]
				LICMOD_TESTDATA = 16,
			
		}

	
		public uint? dwLicNum;		/*��Ӧ����ģ��ڵ���*/
	
		public string szModName;		/*��Ȩģ������*/
		};

	/*�����Ϣ*/
	public struct UNILICENSE
	{
		private Reserved reserved;
		
		public string szLicSN;		/*��ɱ��*/
	
		public uint? dwInstDate;		/*��װ����*/
	
		public uint? dwLicExpDate;		/*��ɵ�����*/
	
		public uint? dwServiceExpDate;		/*��������*/
	
		public string szLicTo;		/*��Ȩ�ͻ�����*/
	
		public string szLicProName;		/*��Ȩ��Ʒ����*/
	
		public string szCompanyName;		/*��˾����*/
	
		public uint? dwWarrant;		/*��һ��ͨ�Խ�ģʽ*/
	
		[FlagsAttribute]
		public enum DWWARRANT : uint
		{
			
				[EnumDescription("���Խ�")]
				WARRANT_NO_THIRD = 0x1,
			
				[EnumDescription("ͬ���ʻ�")]
				WARRANT_SYNC_ACC = 0x2,
			
				[EnumDescription("ͬ�����")]
				WARRANT_SYNC_BALANCE = 0x4,
			
				[EnumDescription("ͬ������")]
				WARRANT_SYNC_PASSWD = 0x8,
			
				[EnumDescription("������Ǯ��")]
				WARRANT_CARD_MONEY = 0x10,
			
				[EnumDescription("����������")]
				WARRANT_WITH_CAC = 0x20,
			
				[EnumDescription("ֻͬ�����ţ���ͬ���༶")]
				WARRANT_SYNC_CARDONLY = 0x40,
			
				[EnumDescription("��ֱ�ӿ۷�ģʽ")]
				WARRANT_CARD_REAL = 0x80,
			
		}

	
		public uint? dwLicStaNum;		/*���վ����*/
	
	public LICMOD[] LicMod;		/*LICMOD�ṹ��*/
	
		public string szCtrlCode;		/*������*/
		};

	/*��ȡ����������Ϣ*/
	public struct REQEXTINFO
	{
		private Reserved reserved;
		
		public uint? dwStartLine;		/*��ʼ��*/
	
		public uint? dwNeedLines;		/*���ȡ����*/
	
		public uint? dwTotolLines;		/*����˷���������*/
	
		public string szOrderKey;		/*�����ֶ�*/
	
		public string szOrderMode;		/*����ʽ(ASC��DESC)*/
	
	public byte[] ExtInfo;		/*���ݲ�ͬ�����������չ��Ϣ*/
		};

	/*�������ݽṹ*/

}
//