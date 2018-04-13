
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
using System.Reflection;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;


namespace UniWebLib
{

	/*开始数据结构*/

	/*版本信息*/
	
	public struct UNIVERSION
	{
		private Reserved reserved;
		
		public string szVersion;		/*版本	XX.XX.XXXXXXXX*/
	
		public uint? dwWarrant;		/*与一卡通对接模式*/
	
	public UNILICENSE szLicInfo;		/*授权信息(UNILICENSE结构)*/
		};

	/*结束数据结构*/

	/*开始数据结构*/

	/*管理员登录请求*/
	
	public struct ADMINLOGINREQ
	{
		private Reserved reserved;
		
		public uint? dwStaSN;		/*站点编号*/
	
		public uint? dwLoginRole;		/*登录模式*/
	
		[FlagsAttribute]
		public enum DWLOGINROLE : uint
		{
			
				[EnumDescription("管理员登录")]
				LOGIN_MANAGER = 0x1,
			
				[EnumDescription("教师登录")]
				LOGIN_TEACHER = 0x2,
			
				[EnumDescription("用户登录")]
				LOGIN_USER = 0x4,
			
				[EnumDescription("摇一摇")]
				LOGIN_SHAKE = 0x8,
			
				[EnumDescription("用户登录掩码")]
				LOGIN_MASK = 0xFF,
			
				[EnumDescription("电脑登录")]
				LOGINEXT_PC = 0x100,
			
				[EnumDescription("手机（微信）登录")]
				LOGINEXT_HP = 0x200,
			
				[EnumDescription("控制台登录")]
				LOGINEXT_CONSOLE = 0x400,
			
				[EnumDescription("用户登录扩展掩码")]
				LOGINEXT_MASK = 0xFF00,
			
		}

	
		public string szVersion;		/*版本	XX.XX.XXXXXXXX,最新版本定义如下*/
	
		[FlagsAttribute]
		public enum SZVERSION : uint
		{
			
				[EnumDescription("主版本")]
				INTVER_MAIN = 3,
			
				[EnumDescription("次版本")]
				INTVER_RELEASE = 0,
			
				[EnumDescription("内部版本")]
				INTVER_INTERNAL = 20161208,
			
		}

	
		public string szLogonName;		/*登录名*/
	
		public string szPassword;		/*密码*/
	
		public string szIP;		/*IP地址*/
	
		public string szMSN;		/*微信号*/
		};

	/*管理员登录应答*/
	
	public struct ADMINLOGINRES
	{
		private Reserved reserved;
		
		public uint? dwSessionID;		/*服务器分配的SessionID*/
	
	public UNIVERSION SrvVer;		/*UNIVERSION 结构*/
	
		public uint? dwSupportSubSys;		/*授权子系统*/
	
		[FlagsAttribute]
		public enum DWSUPPORTSUBSYS : uint
		{
			
				[EnumDescription("研修间管理系统")]
				SUBSYS_STUDYROOM = 0x1,
			
				[EnumDescription("电子阅览室管理系统")]
				SUBSYS_EROOM = 0x2,
			
				[EnumDescription("座位管理系统")]
				SUBSYS_SEAT = 0x4,
			
				[EnumDescription("设备外接管理")]
				SUBSYS_LOAN = 0x8,
			
				[EnumDescription("实验室管理系统")]
				SUBSYS_TEACHING = 0x10,
			
				[EnumDescription("科研实验（大仪）管理")]
				SUBSYS_RESEARCH = 0x20,
			
				[EnumDescription("场馆管理系统")]
				SUBSYS_SITE = 0x40,
			
				[EnumDescription("资产管理系统")]
				SUBSYS_ASSERT = 0x80,
			
		}

	
		public uint? dwManRole;		/*管理员角色*/
	
		[FlagsAttribute]
		public enum DWMANROLE : uint
		{
			
				[EnumDescription("管理员")]
				MANIDENT_ADMIN = 0x1,
			
				[EnumDescription("学生(用户)")]
				MANIDENT_USER = 0x2,
			
				[EnumDescription("教师")]
				MANIDENT_TEACHER = 0x4,
			
				[EnumDescription("导师")]
				MANIDENT_TUTOR = 0x8,
			
				[EnumDescription("审批(examine and approve)")]
				MANIDENT_EANDA = 0x10,
			
				[EnumDescription("匿名用户")]
				MANIDENT_ANONYMOUS = 0x80,
			
				[EnumDescription("超级管理员")]
				MANROLE_SUPER = 0x100,
			
				[EnumDescription("操作员")]
				MANROLE_OPERATOR = 0x200,
			
				[EnumDescription("领导")]
				MANROLE_LEADER = 0x400,
			
				[EnumDescription("值班员")]
				MANROLE_ATTENDANT = 0x800,
			
				[EnumDescription("系统级管理员")]
				MANSCOPE_SYSTEM = 0x10000,
			
				[EnumDescription("站点级管理员")]
				MANSCOPE_STATION = 0x20000,
			
				[EnumDescription("实验中心级管理员")]
				MANSCOPE_LABCTR = 0x40000,
			
				[EnumDescription("实验室级管理员")]
				MANSCOPE_LAB = 0x80000,
			
				[EnumDescription("房间级管理员")]
				MANSCOPE_ROOM = 0x100000,
			
				[EnumDescription("设备级管理员")]
				MANSCOPE_DEV = 0x200000,
			
				[EnumDescription("集控器")]
				MANEXT_DCS = 0x1000000,
			
				[EnumDescription("电脑客户端")]
				MANEXT_DEVCLIENT = 0x2000000,
			
				[EnumDescription("控制台")]
				MANEXT_CONSOLE = 0x4000000,
			
				[EnumDescription("电脑登录")]
				MANEXT_PC = 0x8000000,
			
				[EnumDescription("手机登录")]
				MANEXT_HP = 0x10000000,
			
				[EnumDescription("手机微信登录")]
				MANEXT_MSN = 0x20000000,
			
				[EnumDescription("第三方子系统登录")]
				MANEXT_SUBSYS = 0x40000000,
			
				[EnumDescription("身份掩码")]
				MANMASK_IDENT = 0xFF,
			
				[EnumDescription("角色掩码")]
				MANMASK_ROLE = 0xFF00,
			
				[EnumDescription("范围掩码")]
				MANMASK_SCOPE = 0xFF0000,
			
				[EnumDescription("扩展掩码")]
				MANMASK_EXT = 0xFF000000,
			
		}

	
		public uint? dwUserStat;		/*用户状态*/
	
		[FlagsAttribute]
		public enum DWUSERSTAT : uint
		{
			
				[EnumDescription("未指定导师")]
				USTAT_NOTUTOR = 0x10,
			
				[EnumDescription("导师未确认")]
				USTAT_TUTORUNDO = 0x20,
			
				[EnumDescription("导师已拒接")]
				USTAT_TUTORREJECT = 0x40,
			
				[EnumDescription("导师已设置")]
				USTAT_TUTOROK = 0x80,
			
		}

	
	public UNIADMIN AdminInfo;		/*UNIADMIN 结构*/
	
	public USERROLE[] UserRole;		/*USERROLE表*/
	
	public UNISTATION[] StaInfo;		/*CUniTable[UNISTATION]*/
	
	public UNIACCOUNT AccInfo;		/*用户信息(UNIACCOUNT结构)*/
	
	public UNITUTOR TutorInfo;		/*用户信息(UNITUTOR结构)*/
	
		public string szMemo;		/*说明信息*/
		};

	/*手机登录请求*/
	
	public struct MOBILELOGINREQ
	{
		private Reserved reserved;
		
		public uint? dwStaSN;		/*站点编号*/
	
		public uint? dwLoginRole;		/*登录模式*/
	
		public string szVersion;		/*版本	XX.XX.XXXXXXXX*/
	
		public string szLogonName;		/*登录名*/
	
		public string szPassword;		/*密码*/
	
		public string szIP;		/*IP地址*/
	
		public string szMSN;		/*微信号*/
	
		public uint? dwProperty;		/*扩展属性*/
	
		[FlagsAttribute]
		public enum DWPROPERTY : uint
		{
			
				[EnumDescription("认证成功绑定微信号")]
				MINPROP_BINDMSN = 1,
			
		}

		};

	/*控制台退出请求*/
	
	public struct ADMINLOGOUTREQ
	{
		private Reserved reserved;
		
		public uint? dwAccNo;		/*账号*/
	
		public string szLogonName;		/*登录名*/
	
		public string szIP;		/*IP地址*/
	
		public string szMemo;		/*说明信息*/
		};

	/*控制台退出响应*/
	
	public struct ADMINLOGOUTRES
	{
		private Reserved reserved;
		
		public string szMemo;		/*说明信息*/
		};

	/*手机摇一摇登录请求*/
	
	public struct SHAKELOGINREQ
	{
		private Reserved reserved;
		
		public uint? dwStaSN;		/*站点编号*/
	
		public string szVersion;		/*版本	XX.XX.XXXXXXXX*/
	
		public string szLogonName;		/*登录名*/
	
		public string szPassword;		/*密码*/
	
		public string szIP;		/*IP地址*/
	
		public string szOpenId;		/*摇一摇微信号OpenID*/
	
		public uint? dwProperty;		/*扩展属性*/
	
		[FlagsAttribute]
		public enum DWPROPERTY : uint
		{
			
				[EnumDescription("认证成功绑定摇一摇微信号")]
				MINPROP_BINDOPENID = 1,
			
		}

		};

	/*摇一摇登录应答*/
	
	public struct SHAKELOGINRES
	{
		private Reserved reserved;
		
		public uint? dwSessionID;		/*服务器分配的SessionID*/
	
	public UNIVERSION SrvVer;		/*UNIVERSION 结构*/
	
		public string szPID;		/*学工号*/
	
		public string szTrueName;		/*姓名*/
	
		public string szDispInfo;		/*显示信息*/
	
		public string szMemo;		/*说明信息*/
		};

	/*获取系统支持的UID*/
	
	public struct UIDINFOREQ
	{
		private Reserved reserved;
		
		public uint? dwUidSN;		/*UID编号*/
	
		public uint? dwFuncSN;		/*所属功能模块编号*/
	
		public uint? dwUIDType;		/*请求类型*/
	
		public string szUIDName;		/*UID名称*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*系统支持的UID*/
	
	public struct UIDINFO
	{
		private Reserved reserved;
		
		public uint? dwUidSN;		/*UID编号*/
	
		public uint? dwFuncSN;		/*所属功能模块编号*/
	
		public string szFuncName;		/*所属功能模块名称*/
	
		public uint? dwUIDType;		/*请求类型*/
	
		[FlagsAttribute]
		public enum DWUIDTYPE : uint
		{
			
				[EnumDescription("查询")]
				UIDTYPE_QUERY = 0x1,
			
				[EnumDescription("新建")]
				UIDTYPE_NEW = 0x2,
			
				[EnumDescription("修改")]
				UIDTYPE_CHG = 0x4,
			
				[EnumDescription("删除")]
				UIDTYPE_DEL = 0x8,
			
		}

	
		public string szUIDName;		/*UID名称*/
	
		public string szMemo;		/*说明信息*/
		};

	/*字段限制*/
	
	public struct FIELDLIMIT
	{
		private Reserved reserved;
		
		public string szFieldName;		/*字段名称*/
	
		public string szMask;		/*对该字段处理方法*/
		};

	/*UID权限明细*/
	
	public struct PRIVUID
	{
		private Reserved reserved;
		
		public uint? dwPrivID;		/*权限ID*/
	
		public uint? dwUidSN;		/*UID编号*/
	
		public uint? dwUIDType;		/*请求类型*/
	
		public uint? dwFuncSN;		/*所属功能模块编号*/
	
		public string szFuncName;		/*所属功能模块名称*/
	
		public string szUIDName;		/*UID名称*/
	
		public uint? dwWarrantType;		/*许可方式*/
	
		[FlagsAttribute]
		public enum DWWARRANTTYPE : uint
		{
			
				[EnumDescription("全部许可")]
				WARRANTTYPE_FULL = 0x1,
			
				[EnumDescription("部分许可（具体限制在FIELDLIMIT定义）")]
				WARRANTTYPE_PART = 0x2,
			
				[EnumDescription("不能访问")]
				WARRANTTYPE_FORBID = 0x100,
			
		}

	
	public FIELDLIMIT[] FieldLimit;		/*对应UID的各字段限制规则*/
		};

	/*获取操作权限请求*/
	
	public struct OPPRIVREQ
	{
		private Reserved reserved;
		
		public uint? dwOPID;		/*操作权限ID*/
	
		public string szOPName;		/*操作权限名称（模糊匹配）*/
	
		public uint? dwFuncSN;		/*所属功能模块编号*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*操作权限*/
	
	public struct OPPRIV
	{
		private Reserved reserved;
		
		public uint? dwOPID;		/*操作权限ID*/
	
		public string szOPName;		/*操作权限名称*/
	
		public uint? dwDefWarType;		/*缺省许可方式（定义见PRIVUID）*/
	
		public uint? dwSysFuncMask;		/*支持系统功能模块*/
	
		public uint? dwFuncSN;		/*所属功能模块编号*/
	
		public string szFuncName;		/*所属功能模块名称*/
	
	public PRIVUID[] PrivUID;		/*各UID权限明细表(CUniTable<PRIVUID>)*/
	
		public string szMemo;		/*说明信息*/
		};

	/*获取用户角色*/
	
	public struct USERROLEREQ
	{
		private Reserved reserved;
		
		public uint? dwRoleID;		/*角色ID*/
	
		public string szRoleName;		/*角色名称（模糊匹配）*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*用户角色*/
	
	public struct USERROLE
	{
		private Reserved reserved;
		
		public uint? dwRoleID;		/*角色ID*/
	
		public string szRoleName;		/*角色名称*/
	
		public uint? dwDefWarType;		/*缺省许可方式（定义见PRIVUID）*/
	
		public uint? dwSysFuncMask;		/*支持系统功能模块*/
	
	public OPPRIV[] OpPriv;		/*操作权限表(CUniTable<OPPRIV>)*/
	
		public string szMemo;		/*说明信息*/
		};

	/*客户端卸载密码结构*/
	
	public struct CLTPASSWD
	{
		private Reserved reserved;
		
		public uint? dwPassWdCode;		/*密码CODE*/
	
		public string szPassword;		/*密码*/
	
		public uint? dwSetDate;		/*设置日期*/
	
		public string szOperator;		/*设置管理员*/
	
		public string szMemo;		/*说明信息*/
		};

	/**/
	
	public struct ADMINREQ
	{
		private Reserved reserved;
		
		public uint? dwStaSN;		/*节点编号*/
	
		public uint? dwAccNo;		/*帐号*/
	
		public string szLogonName;		/*登录名*/
	
		public string szTrueName;		/*姓名*/
	
		public uint? dwDeptID;		/*部门ID*/
	
		public string szDeptName;		/*部门*/
	
		public uint? dwManRole;		/*管理员角色*/
	
		public uint? dwIdent;		/*管理员身份*/
	
		public uint? dwProperty;		/*管理员属性（以下定义+审核类型）*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*管理员信息*/
	
	public struct UNIADMIN
	{
		private Reserved reserved;
		
		public uint? dwStaSN;		/*节点编号*/
	
		public uint? dwAccNo;		/*帐号*/
	
		public uint? dwManRole;		/*管理员角色*/
	
		public uint? dwStatus;		/*状态*/
	
		public uint? dwProperty;		/*管理员属性（以下定义+审核类型）*/
	
		[FlagsAttribute]
		public enum DWPROPERTY : uint
		{
			
				[EnumDescription("系统专用管理员")]
				ADMINPROP_SYS = 1,
			
				[EnumDescription("有用户身份的管理员")]
				ADMINPROP_USER = 2,
			
		}

	
		public uint? dwExpDate;		/*到期日*/
	
		public string szLogonName;		/*登录名*/
	
		public string szTrueName;		/*姓名*/
	
		public uint? dwIdent;		/*管理员身份*/
	
		public uint? dwManLevel;		/*管理员级别（比如学院级，校级等)*/
	
		[FlagsAttribute]
		public enum DWMANLEVEL : uint
		{
			
				[EnumDescription("学院级管理员")]
				MANLEVEL_DEPT = 5,
			
				[EnumDescription("校级管理员")]
				MANLEVEL_SCHOOL = 10,
			
		}

	
		public uint? dwDeptID;		/*部门ID*/
	
		public string szDeptName;		/*部门*/
	
		public string szTel;		/*电话*/
	
		public string szHandPhone;		/*手机*/
	
		public string szEmail;		/*电邮*/
	
	public USERROLE[] UserRole;		/*角色表(CUniTable<USERROLE>)*/
	
		public string szMemo;		/*备注*/
		};

	/*获取管理房间请求*/
	
	public struct MANROOMREQ
	{
		private Reserved reserved;
		
		public uint? dwAccNo;		/*帐号*/
	
		public uint? dwManFlag;		/*管理标志(空表示获取全部，0获取未管理房间，1获取管理房间)*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*管理员管理房间*/
	
	public struct MANROOM
	{
		private Reserved reserved;
		
		public uint? dwRoomID;		/*房间ID*/
	
		public string szRoomNo;		/*房间编号*/
	
		public string szRoomName;		/*房间名称*/
	
		public uint? dwManGroupID;		/*管理员组ID*/
	
		public uint? dwManFlag;		/*管理标志(0没有权限，1有管理权限)*/
		};

	/**/
	
	public struct ADMINCHECK
	{
		private Reserved reserved;
		
		public uint? dwSubjectID;		/*确认事由ID*/
	
		public uint? dwSubjectType;		/*确认事由类别*/
	
		[FlagsAttribute]
		public enum DWSUBJECTTYPE : uint
		{
			
				[EnumDescription("审核预约")]
				CHECK_RESV = 1,
			
				[EnumDescription("审核实验计划")]
				CHECK_TESTPLAN = 2,
			
				[EnumDescription("审核招募组成员")]
				CHECK_GROUPMEMBER = 3,
			
				[EnumDescription("审核账单")]
				CHECK_BILL = 4,
			
				[EnumDescription("审核设备使用情况 szSubjectInfo 可存放USEDDEVCHECKINFO")]
				CHECK_USEDDEV = 5,
			
				[EnumDescription("审核活动安排")]
				CHECK_ACTIVITYPLAN = 6,
			
				[EnumDescription("导师审核学生")]
				CHECK_TUTORSTUDENT = 7,
			
				[EnumDescription("系统功能使用资格审核")]
				CHECK_SYSFUNCROLE = 8,
			
				[EnumDescription("场馆预约审核")]
				CHECK_YARDRESV = 9,
			
		}

	
		public uint? dwCheckStat;		/*管理员审核状态(扩展由各类别审查确认时定义)*/
	
		[FlagsAttribute]
		public enum DWCHECKSTAT : uint
		{
			
				[EnumDescription("管理员审查掩码")]
				ADMINCHECK_MASK = 0xFF,
			
				[EnumDescription("审查中")]
				CHECKSTAT_DOING = 0x1,
			
				[EnumDescription("管理员审查通过")]
				CHECKSTAT_ADMINOK = 0x2,
			
				[EnumDescription("管理员未审查通过")]
				CHECKSTAT_ADMINFAIL = 0x4,
			
				[EnumDescription("等待满足审核条件（不能审核）")]
				CHECKSTAT_WAIT = 0x8,
			
				[EnumDescription("可审核")]
				CHECKSTAT_CANDO = 0x10,
			
				[EnumDescription("需补充材料后再审核")]
				CHECKSTAT_NEEDMORE = 0x20,
			
				[EnumDescription("再审核(与前面的成功，失败连用)")]
				CHECKSTAT_REDO = 0x40,
			
				[EnumDescription("非最终审核（不发短信）")]
				CHECKSTAT_NOFINAL = 0x80,
			
				[EnumDescription("审查通过")]
				CHECKSTAT_OK = (CHECKSTAT_ADMINOK),
			
				[EnumDescription("审查未通过")]
				CHECKSTAT_FAIL = (CHECKSTAT_DOING|CHECKSTAT_ADMINFAIL),
			
		}

	
		public uint? dwApplicantID;		/*申请人账号*/
	
		public string szApplicantName;		/*申请人姓名*/
	
		public string szCheckDetail;		/*审查说明*/
	
		public string szMemo;		/*备注*/
	
		public string szSubjectInfo;		/*对应的确认事由结构详细信息*/
		};

	/*设备使用审核信息*/
	
	public struct USEDDEVCHECKINFO
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*流水号*/
	
		public uint? dwAccNo;		/*使用人*/
	
		public string szTrueName;		/*使用者姓名*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public uint? dwDevID;		/*设备ID*/
	
		public uint? dwCheckStat;		/*设备状态*/
	
		public uint? dwCompensation;		/*赔偿金额*/
	
		public uint? dwPunishScore;		/*信用扣分*/
	
		public string szDamageInfo;		/*损坏说明*/
	
		public string szExtInfo;		/*设备新描述*/
		};

	/*获取审核信息*/
	
	public struct CHECKREQ
	{
		private Reserved reserved;
		
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
		public uint? dwCheckStat;		/*状态*/
	
		public uint? dwSubjectID;		/*事由ID*/
	
		public uint? dwSubjectType;		/*事由类别*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*审核信息*/
	
	public struct CHECKINFO
	{
		private Reserved reserved;
		
		public uint? dwSubjectID;		/*确认事由ID*/
	
		public uint? dwSubjectType;		/*确认事由类别*/
	
		public uint? dwCheckStat;		/*管理员审核状态(扩展由各类别审查确认时定义)*/
	
		public uint? dwOccurDate;		/*开始日期*/
	
		public uint? dwOccurTime;		/*产生时间*/
	
		public uint? dwApplicantID;		/*申请人账号*/
	
		public string szApplicantName;		/*申请人姓名*/
	
		public string szMemo;		/*备注*/
		};

	/*获取审核信息日志*/
	
	public struct CHECKLOGREQ
	{
		private Reserved reserved;
		
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
		public uint? dwCheckStat;		/*状态*/
	
		public uint? dwSubjectID;		/*事由ID*/
	
		public uint? dwSubjectType;		/*事由类别*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*审核信息日志*/
	
	public struct CHECKLOG
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*流水号*/
	
		public uint? dwCheckStat;		/*管理员审核状态(扩展由各类别审查确认时定义)*/
	
		public uint? dwSubjectID;		/*确认事由ID*/
	
		public uint? dwSubjectType;		/*确认事由类别*/
	
		public uint? dwOccurDate;		/*开始日期*/
	
		public uint? dwOccurTime;		/*审核时间*/
	
		public uint? dwAdminID;		/*审核者帐号*/
	
		public string szAdminName;		/*审核者*/
	
		public uint? dwApplicantID;		/*申请人账号*/
	
		public string szApplicantName;		/*申请人姓名*/
	
		public string szCheckDetail;		/*审查说明*/
	
		public string szMemo;		/*备注*/
		};

	/*获取刷新标志请求*/
	
	public struct REFRESHFLAGREQ
	{
		private Reserved reserved;
		
		public uint? dwRefreshType;		/*刷新类别*/
	
		[FlagsAttribute]
		public enum DWREFRESHTYPE : uint
		{
			
				[EnumDescription("设备列表")]
				REFRESHTYPE_DEVICE = 0x1,
			
				[EnumDescription("通道+房间列表")]
				REFRESHTYPE_ROOM = 0x2,
			
				[EnumDescription("审核列表")]
				REFRESHTYPE_CHECK = 0x4,
			
		}

		};

	/*获取刷新标志请求*/
	
	public struct REFRESHFLAGINFO
	{
		private Reserved reserved;
		
		public uint? dwRefreshType;		/*刷新类别*/
	
		public uint? dwRefreshFlag;		/*刷新标志*/
		};

	/*获取节假日*/
	
	public struct HOLIDAYREQ
	{
		private Reserved reserved;
		
		public string szName;		/*名称（模糊匹配）*/
	
		public uint? dwDate;		/*日期*/
		};

	/*节假日信息*/
	
	public struct UNIHOLIDAY
	{
		private Reserved reserved;
		
		public string szName;		/*名称（模糊匹配）*/
	
		public uint? dwStartDay;		/*开始日期(MMDD或YYYYMMDD)*/
	
		public uint? dwEndDay;		/*结束日期(MMDD或YYYYMMDD)*/
	
		public string szMemo;		/*开放说明*/
		};

	/*检测某个值是否存在请求*/
	
	public struct CHECKEXISTREQ
	{
		private Reserved reserved;
		
		public uint? dwUID;		/*请求新建修改的ID，比如MSREQ_ADMIN_SET*/
	
		public string szName;		/*判断的字段名称(和相对应的结构里的名称相同,比如szLogonName*/
	
		public string szValue;		/*需要检查的值，是数字的转换成字符串*/
	
		public string szCon;		/*SQL语句条件值，可为空*/
		};

	/*获取某个字段的最大值请求*/
	
	public struct MAXVALUEREQ
	{
		private Reserved reserved;
		
		public uint? dwUID;		/*请求新建修改的ID，比如MSREQ_ADMIN_SET*/
	
		public string szName;		/*判断的字段名称(和相对应的结构里的名称相同,比如szLogonName*/
	
		public string szCon;		/*SQL语句条件值，可为空*/
		};

	/*返回最大值*/
	
	public struct MAXVALUE
	{
		private Reserved reserved;
		
		public uint? dwValue;		/*返回的最大值*/
		};

	/*工作管理人员界面参数信息请求*/
	
	public struct IFPARAMREQ
	{
		private Reserved reserved;
		
		public uint? dwAdminID;		/*管理员ID*/
		};

	/*工作管理人员界面参数信息*/
	
	public struct IFPARAM
	{
		private Reserved reserved;
		
		public uint? dwAdminID;		/*管理员ID*/
	
		public string szParam;		/*参数*/
	
		public string szMemo;		/*备注*/
		};

	/*获取管理员操作日志请求包*/
	
	public struct ADMINLOGREQ
	{
		private Reserved reserved;
		
		public uint? dwStaSN;		/*节点编号*/
	
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
		public uint? dwAdminID;		/*管理员ID*/
	
		public string szTrueName;		/*真实姓名（模糊匹配）*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*管理员操作日志*/
	
	public struct ADMINLOG
	{
		private Reserved reserved;
		
		public uint? dwStaSN;		/*节点编号*/
	
		public uint? dwAdminID;		/*管理员ID*/
	
		public string szTrueName;		/*真实姓名*/
	
		public uint? dwOpDate;		/*操作日期*/
	
		public uint? dwOpTime;		/*操作时间*/
	
		public uint? dwOpUID;		/*操作接口ID*/
	
		public string szOpInfo;		/*操作内容*/
	
		public string szOpDetail;		/*操作详细说明*/
		};

	/*IP地址黑名单*/
	
	public struct IPBLACKLIST
	{
		private Reserved reserved;
		
		public string szIP;		/*ip地址*/
	
		public string szTryAdmin;		/*重试账号*/
	
		public uint? dwTryTimes;		/*重试次数*/
	
		public uint? dwLockEndTime;		/*锁定结束时间*/
		};

	/*管理员修改密码*/
	
	public struct ADMINCHGPASSWD
	{
		private Reserved reserved;
		
		public string szCurAdminPw;		/*当前登录的管理员密码*/
	
		public uint? dwAdminID;		/*管理员帐号*/
	
		public string szNewPw;		/*新密码*/
	
		public string szMemo;		/*备注（扩展用）*/
		};

	/*系统状态*/
	
	public struct BSYSINFO
	{
		private Reserved reserved;
		
		public uint? dwStatus;		/*总状态*/
	
	public MODMONI[] ParamStat;		/*监控指标状态*/
	
		public string szStatInfo;		/*状态说明*/
		};

	/*状态统计信息*/
	
	public struct STATUSINFO
	{
		private Reserved reserved;
		
		public uint? dwStatus;		/*状态值*/
	
		public uint? dwNum;		/*数量*/
		};

	/*审核信息统计*/
	
	public struct BCHECKSTAT
	{
		private Reserved reserved;
		
		public uint? dwStatus;		/*总状态*/
	
	public STATUSINFO[] CheckStatInfo;		/*审核统计表*/
	
		public string szStatInfo;		/*状态说明*/
		};

	/*设备信息统计*/
	
	public struct BDEVSTAT
	{
		private Reserved reserved;
		
		public uint? dwStatus;		/*总状态*/
	
	public STATUSINFO[] DevStatInfo;		/*设备状态统计表*/
	
		public string szStatInfo;		/*状态说明*/
		};

	/*房间信息统计*/
	
	public struct BROOMSTAT
	{
		private Reserved reserved;
		
		public uint? dwStatus;		/*总状态*/
	
	public STATUSINFO[] RoomStatInfo;		/*房间状态统计表*/
	
		public string szStatInfo;		/*状态说明*/
		};

	/*今日课程统计*/
	
	public struct BTODAYRESVSTAT
	{
		private Reserved reserved;
		
		public uint? dwStatus;		/*总状态*/
	
	public STATUSINFO[] TodayResvStatInfo;		/*今日课程状态统计表*/
	
		public string szStatInfo;		/*状态说明*/
		};

	/*自由上机统计*/
	
	public struct BFREEUSESTAT
	{
		private Reserved reserved;
		
		public uint? dwStatus;		/*总状态*/
	
		public uint? dwCurUsers;		/*当前人数*/
	
	public STATUSINFO[] FreeTodayUseStat;		/*今日自由上机统计(按小时)*/
	
		public string szStatInfo;		/*状态说明*/
		};

	/*服务器返回信息*/
	
	public struct BASICSTAT
	{
		private Reserved reserved;
		
		public uint? dwChgNum;		/*信息发生改变的统计栏目数*/
	
		public uint? dwStatus;		/*状态*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("正常")]
				MONISTAT_OK = 1,
			
				[EnumDescription("提醒")]
				MONISTAT_INFO = 2,
			
				[EnumDescription("警告")]
				MONISTAT_WARNNING = 4,
			
				[EnumDescription("错误")]
				MONISTAT_ERROR = 8,
			
		}

	
	public BSYSINFO SysStat;		/*系统状态*/
	
	public BCHECKSTAT CheckStat;		/*审核信息统计*/
	
	public BDEVSTAT DevStat;		/*设备信息统计*/
	
	public BROOMSTAT RoomStat;		/*房间信息统计*/
	
	public BTODAYRESVSTAT TodayResvStat;		/*今日课程统计*/
	
	public BFREEUSESTAT FreeUseStat;		/*自由上机统计*/
	
		public string szStatInfo;		/*状态说明*/
		};

	/*审核类别请求*/
	
	public struct CHECKTYPEREQ
	{
		private Reserved reserved;
		
		public uint? dwCheckKind;		/*审核类型(可多个)*/
	
		public uint? dwMainKind;		/*审核大类*/
		};

	/*审核类别*/
	
	public struct CHECKTYPE
	{
		private Reserved reserved;
		
		public uint? dwCheckKind;		/*审核编号（新建时由系统自动分配）*/
	
		public uint? dwMainKind;		/*审核大类*/
	
		[FlagsAttribute]
		public enum DWMAINKIND : uint
		{
			
				[EnumDescription("设备管理审核")]
				ADMINCHECK_DEVMAN = 0x100,
			
				[EnumDescription("安保审核")]
				ADMINCHECK_SECURITY = 0x200,
			
				[EnumDescription("宣传审核")]
				ADMINCHECK_PUBLICITY = 0x400,
			
				[EnumDescription("主管单位审核（可新建，类型由系统分配）")]
				ADMINCHECK_DIRECTOR = 0x800,
			
				[EnumDescription("服务类型")]
				ADMINCHECK_SERVICE = 0x1000,
			
				[EnumDescription("审核类别掩码")]
				ADMINCHECKTYPE_MASK = 0xFFFF00,
			
		}

	
		public string szCheckName;		/*审核名称*/
	
		public uint? dwCheckLevel;		/*审核级别(同UNIADMIN.dwManLevel定义）*/
	
		public uint? dwDeptID;		/*责任部门ID（学院级不设置，根据申请人自动匹配）*/
	
		public string szDeptName;		/*责任部门*/
	
		public string szMemo;		/*状态说明*/
		};

	/*获取用户意见反馈请求*/
	
	public struct USERFEEDBACKREQ
	{
		private Reserved reserved;
		
		public uint? dwFeedKind;		/*反馈类型*/
	
		public uint? dwFeedStat;		/*状态*/
	
		public uint? dwPurpose;		/*用途*/
	
		public uint? dwResvID;		/*预约ID*/
	
		public uint? dwSNum;		/*流水号*/
	
		public uint? dwAccNo;		/*帐号*/
	
		public uint? dwDevID;		/*使用设备*/
	
		public uint? dwMinScore;		/*用户最低评分*/
	
		public uint? dwMaxScore;		/*用户最高评分*/
	
		public uint? dwBeginDate;		/*预约开始日期*/
	
		public uint? dwEndDate;		/*预约结束日期*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*用户意见反馈*/
	
	public struct USERFEEDBACK
	{
		private Reserved reserved;
		
		public uint? dwSNum;		/*流水号*/
	
		public uint? dwAccNo;		/*帐号*/
	
		public string szTrueName;		/*姓名*/
	
		public string szTel;		/*电话*/
	
		public string szHandPhone;		/*手机*/
	
		public string szEmail;		/*电邮*/
	
		public uint? dwUserDeptID;		/*部门ID*/
	
		public string szUserDeptName;		/*部门*/
	
		public uint? dwFeedKind;		/*反馈类型*/
	
		[FlagsAttribute]
		public enum DWFEEDKIND : uint
		{
			
				[EnumDescription("使用评价")]
				FEEDKIND_EVALUATE = 1,
			
				[EnumDescription("意见建议")]
				FEEDKIND_ADVICE = 2,
			
				[EnumDescription("投诉")]
				FEEDKIND_COMPLAIN = 4,
			
		}

	
		public uint? dwFeedStat;		/*状态*/
	
		[FlagsAttribute]
		public enum DWFEEDSTAT : uint
		{
			
				[EnumDescription("等待回复")]
				FEEDSTAT_WAITREPLY = 1,
			
				[EnumDescription("已回复")]
				FEEDSTAT_REPLIED = 2,
			
		}

	
		public uint? dwPurpose;		/*用途*/
	
		public uint? dwResvID;		/*预约ID*/
	
		public uint? dwDevID;		/*使用设备*/
	
		public uint? dwDevKind;		/*设备类型*/
	
		public uint? dwDevSN;		/*设备编号*/
	
		public string szDevName;		/*设备名称*/
	
		public uint? dwRoomID;		/*房间ID*/
	
		public string szRoomName;		/*房间名称*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabName;		/*实验室名称*/
	
		public uint? dwScore;		/*用户评分*/
	
		public uint? dwOccurDate;		/*发生日期*/
	
		public uint? dwOccurTime;		/*发生时间*/
	
		public string szIntroInfo;		/*申请信息*/
	
		public string szReplyInfo;		/*回复信息*/
	
		public uint? dwReplyDate;		/*回复日期*/
	
		public uint? dwReplyTime;		/*回复时间*/
	
		public uint? dwAnswererID;		/*回复帐号*/
	
		public string szAnswerer;		/*回复者*/
	
		public string szMemo;		/*状态说明*/
		};

	/*服务类别请求*/
	
	public struct SERVICETYPEREQ
	{
		private Reserved reserved;
		
		public uint? dwServiceKind;		/*审核类型(可多个)*/
		};

	/*服务类别*/
	
	public struct UNISERVICETYPE
	{
		private Reserved reserved;
		
		public uint? dwServiceKind;		/*服务编号（新建时由系统自动分配）*/
	
		public string szServiceName;		/*服务名称*/
	
		public uint? dwServiceLevel;		/*服务部门级别(同UNIADMIN.dwManLevel定义）*/
	
		public uint? dwDeptID;		/*服务部门ID（学院级不设置，根据申请人自动匹配）*/
	
		public string szDeptName;		/*服务部门*/
	
		public uint? dwProperty;		/*审核属性*/
	
		[FlagsAttribute]
		public enum DWPROPERTY : uint
		{
			
				[EnumDescription("服务部门审核通过后预约才能生效")]
				SERVICEPROP_NEEDCHECK = 0x1,
			
		}

	
		public string szMemo;		/*说明*/
		};

	/*获取用户意见反馈请求*/
	
	public struct POLLONLINEREQ
	{
		private Reserved reserved;
		
		public uint? dwPollID;		/*流水号*/
	
		public uint? dwVoteStat;		/*投票状态*/
	
		public uint? dwBeginDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*投票选项*/
	
	public struct POLLITEM
	{
		private Reserved reserved;
		
		public uint? dwItemID;		/*选项号*/
	
		public string szItemName;		/*选项名称*/
	
		public uint? dwGroupID;		/*组ID*/
	
		public uint? dwPollKind;		/*民调类型*/
	
		public uint? dwMaxTickItems;		/*最大可勾选选项*/
	
		public uint? dwVotes;		/*总票数*/
	
		public string szMemo;		/*状态说明*/
		};

	/*网上投票信息*/
	
	public struct POLLONLINE
	{
		private Reserved reserved;
		
		public uint? dwPollID;		/*投票ID*/
	
		public uint? dwAccNo;		/*发起人（负责人）帐号*/
	
		public string szTrueName;		/*姓名*/
	
		public string szTel;		/*电话*/
	
		public string szHandPhone;		/*手机*/
	
		public string szEmail;		/*电邮*/
	
		public uint? dwVoteStat;		/*投票状态*/
	
		[FlagsAttribute]
		public enum DWVOTESTAT : uint
		{
			
				[EnumDescription("未开放")]
				VOTESTAT_UNOPEN = 0x1,
			
				[EnumDescription("开放中")]
				VOTESTAT_OPENING = 0x2,
			
				[EnumDescription("已关闭")]
				VOTESTAT_CLOSED = 0x4,
			
				[EnumDescription("已投票")]
				VOTESTAT_DONE = 0x100,
			
		}

	
		public uint? dwPollScope;		/*民调范围*/
	
		[FlagsAttribute]
		public enum DWPOLLSCOPE : uint
		{
			
				[EnumDescription("匿名查看")]
				POLLSCOPE_ANONYMOUS_LOOK = 0x1,
			
				[EnumDescription("实名查看")]
				POLLSCOPE_MEMBER_LOOK = 0x2,
			
				[EnumDescription("匿名投票")]
				POLLSCOPE_ANONYMOUS_VOTE = 0x100,
			
				[EnumDescription("实名投票")]
				POLLSCOPE_MEMBER_VOTE = 0x200,
			
		}

	
		public uint? dwPollKind;		/*民调类型*/
	
		[FlagsAttribute]
		public enum DWPOLLKIND : uint
		{
			
				[EnumDescription("多选一")]
				POLLKIND_MTICKS = 0x1,
			
				[EnumDescription("多选多")]
				POLLKIND_MTICKM = 0x2,
			
				[EnumDescription("支持分组")]
				POLLKIND_SUBGROUP = 0x4,
			
		}

	
		public uint? dwMaxTickItems;		/*最大可勾选选项*/
	
		public string szPollSubject;		/*投票主题*/
	
		public uint? dwBeginDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*截止日期*/
	
		public uint? dwTotalUsers;		/*投票总人数*/
	
	public POLLITEM[] PollItems;		/*CUniTable[POLLITEM]*/
	
		public string szMemo;		/*状态说明*/
		};

	/*投票*/
	
	public struct POLLVOTE
	{
		private Reserved reserved;
		
		public uint? dwPollID;		/*投票编号*/
	
		public uint? dwItemID;		/*选项号*/
	
		public string szMemo;		/*状态说明*/
		};

	/*结束数据结构*/

	/*开始数据结构*/

	/*获取站点的请求包*/
	
	public struct STATIONREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*方式*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("全部")]
				STATIONGET_BYALL = 0,
			
				[EnumDescription("课程代码")]
				STATIONGET_BYSN = 1,
			
		}

	
		public string szGetKey;		/*条件值*/
		};

	/*设备软硬件配置信息*/
	
	public struct DEVICECONFIG
	{
		private Reserved reserved;
		
		public uint? dwStaSN;		/*站点编号*/
	
		public uint? dwCfgType;		/*配置类型*/
	
		[FlagsAttribute]
		public enum DWCFGTYPE : uint
		{
			
				[EnumDescription("服务器配置")]
				CFGTYPE_SERVER = 1,
			
				[EnumDescription("数据库配置")]
				CFGTYPE_DATABASE = 2,
			
				[EnumDescription("网络配置")]
				CFGTYPE_NET = 4,
			
				[EnumDescription("软件配置")]
				CFGTYPE_SOFTWARE = 8,
			
				[EnumDescription("站点服务器类型")]
				CFGTYPE_STAMASK = 0xFF,
			
				[EnumDescription("终端配置")]
				CFGTYPE_TERMINAL = 0x10000,
			
		}

	
		public string szCfgName;		/*配置名称*/
	
		public string szBrand;		/*品牌*/
	
		public string szModel;		/*规格型号*/
	
		public string szSpecification;		/*设备规格*/
	
		public string szPurpose;		/*用途说明*/
	
		public string szIndicators;		/*主要指标说明*/
	
		public uint? dwPurchaseDate;		/*采购日期 YYYYMMDD*/
	
		public uint? dwStartUseDate;		/*投入使用日期 YYYYMMDD*/
	
		public uint? dwDevLife;		/*设备使用年限*/
	
		public string szCPU;		/*CPU规格型号*/
	
		public uint? dwMemSize;		/*内存大小（M）*/
	
		public uint? dwDiskSize;		/*硬盘大小(G)*/
	
		public uint? dwOsVer;		/*操作系统版本(dwMajorVersion*1000000 + dwMinorVersion*10000 + wProductType*100 +系统类型(32位或64位) )*/
	
		public string szMemo;		/*备注*/
	
		public uint? dwDelFlag;		/*删除标志*/
		};

	/**/
	
	public struct UNISTATION
	{
		private Reserved reserved;
		
		public uint? dwStaSN;		/*站点编号*/
	
		public string szStaName;		/*站点名称*/
	
		public string szLicSN;		/*许可编号*/
	
		public uint? dwSubSysSN;		/*子系统编号*/
	
		[FlagsAttribute]
		public enum DWSUBSYSSN : uint
		{
			
				[EnumDescription("通用实验室")]
				SUBSYS_LAB = 0x100,
			
				[EnumDescription("IC学习空间（管理图书馆研修间、电子阅览室和各种学习空间）")]
				SUBSYS_IC = 0x200,
			
				[EnumDescription("开放实验室（浙大985类型的实验室）")]
				SUBSYS_OPENLAB = (SUBSYS_LAB+0x1),
			
				[EnumDescription("教学实验室（以教学为中心，课余对学生开放）")]
				SUBSYS_TEACHINGLAB = (SUBSYS_LAB+0x2),
			
				[EnumDescription("支持子系统掩码")]
				SUBSYS_VALIDMASK = 0x300,
			
		}

	
		public uint? dwStatus;		/*站点状态*/
	
		public uint? dwOwnerDept;		/*所属部门*/
	
		public uint? dwManagerID;		/*负责人账号*/
	
		public uint? dwAttendantID;		/*值班员账号*/
	
		public string szDeptName;		/*部门名称*/
	
		public string szManName;		/*负责人姓名*/
	
		public string szAttendantName;		/*值班员姓名*/
	
		public string szMemo;		/*备注*/
		};

	/*结束数据结构*/

	/*开始数据结构*/

	/**/
	
	public struct UNIDEPT
	{
		private Reserved reserved;
		
		public uint? dwID;		/*ID*/
	
		public string szDeptSN;		/*单位编号*/
	
		public string szName;		/*名称*/
	
		public uint? dwKind;		/*类型*/
	
		[FlagsAttribute]
		public enum DWKIND : uint
		{
			
				[EnumDescription("学院")]
				DEPTKIND_SCHOOL = 0x100,
			
				[EnumDescription("行政部门")]
				DEPTKIND_SERVICE = 0x200,
			
				[EnumDescription("本地部门")]
				DEPTKIND_LOCAL = 0x1,
			
				[EnumDescription("同步部门")]
				DEPTKIND_SYNC = 0x2,
			
				[EnumDescription("校外部门")]
				DEPTKIND_OUTER = 0x4,
			
				[EnumDescription("不可见")]
				DEPTKIND_INVISIBLE = 0x10,
			
				[EnumDescription("使可见")]
				DEPTKIND_SETVISIBLE = 0x80000000,
			
		}

	
		public string szMemo;		/*备注*/
		};

	/**/
	
	public struct DEPTREQ
	{
		private Reserved reserved;
		
		public uint? dwID;		/*ID*/
	
		public string szName;		/*名称*/
	
		public uint? dwKind;		/*类型(行政部门或学院)*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*校区*/
	
	public struct UNICAMPUS
	{
		private Reserved reserved;
		
		public uint? dwCampusID;		/*校区ID*/
	
		public string szCampusName;		/*校区名称*/
	
		public uint? dwCampusKind;		/*校区类型（扩展）*/
	
		public string szMemo;		/*备注*/
		};

	/*获取校区请求*/
	
	public struct CAMPUSREQ
	{
		private Reserved reserved;
		
		public uint? dwCampusID;		/*校区ID*/
	
		public string szCampusName;		/*校区名称*/
	
		public uint? dwCampusKind;		/*校区类型（扩展）*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/**/
	
	public struct CLASSREQ
	{
		private Reserved reserved;
		
		public uint? dwClassID;		/*ID*/
	
		public string szClassName;		/*名称*/
	
		public uint? dwMajorID;		/*专业ID*/
	
		public uint? dwEnrolYear;		/*入学年份*/
	
		public uint? dwClassKind;		/*类型*/
	
		public uint? dwDeptID;		/*部门ID*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/**/
	
	public struct UNICLASS
	{
		private Reserved reserved;
		
		public uint? dwClassID;		/*ID*/
	
		public string szClassSN;		/*班级编号*/
	
		public string szClassName;		/*名称*/
	
		public uint? dwClassKind;		/*类型*/
	
		public uint? dwDeptID;		/*部门ID*/
	
		public uint? dwMajorID;		/*专业ID*/
	
		public uint? dwEnrolYear;		/*入学年份*/
	
		public string szMemo;		/*备注*/
		};

	/*获取账户列表输入参数*/
	
	public struct ACCREQ
	{
		private Reserved reserved;
		
		public uint? dwCardID;		/*卡ID号*/
	
		public uint? dwAccNo;		/*账号*/
	
		public string szPID;		/*学工号*/
	
		public string szLogonName;		/*登录名(学工号)*/
	
		public string szCardNo;		/*卡号*/
	
		public string szTrueName;		/*姓名*/
	
		public uint? dwIdent;		/*身份*/
	
		public uint? dwStatus;		/*状态*/
	
		public uint? dwEnrolYear;		/*入学年份(XX级)*/
	
		public uint? dwClassID;		/*班级ID*/
	
		public uint? dwDeptID;		/*部门ID*/
	
		public string szMSN;		/*MSN*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*账户信息*/
	
	public struct UNIACCOUNT
	{
		private Reserved reserved;
		
		public uint? dwAccNo;		/*账号*/
	
		public string szPID;		/*学工号*/
	
		public string szLogonName;		/*登录名(学工号)*/
	
		public string szCardNo;		/*卡号*/
	
		public uint? dwCardID;		/*卡ID号*/
	
		public string szIDCard;		/*身份证号*/
	
		public string szTrueName;		/*姓名*/
	
		public string szPasswd;		/*加密密码*/
	
		public uint? dwClassID;		/*班级ID*/
	
		public string szClassName;		/*班级*/
	
		public uint? dwDeptID;		/*部门ID*/
	
		public string szDeptName;		/*部门*/
	
		public uint? dwMajorID;		/*专业ID*/
	
		public string szMajorName;		/*专业*/
	
		public uint? dwSex;		/*性别见UniCommon.h*/
	
		public uint? dwIdent;		/*身份 见UniCommon.h*/
	
		[FlagsAttribute]
		public enum DWIDENT : uint
		{
			
				[EnumDescription("真实身份有效位")]
				REALIDENT_MASK = 0x000FFFFF,
			
				[EnumDescription("扩展身份有效位")]
				EXTIDENT_MASK = 0xFFF00000,
			
				[EnumDescription("导师")]
				EXTIDENT_TUTOR = 0x100000,
			
				[EnumDescription("校内本部门人员")]
				EXTIDENT_DEPT = 0x200000,
			
				[EnumDescription("校内人员")]
				EXTIDENT_INNER = 0x400000,
			
				[EnumDescription("校外人员")]
				EXTIDENT_OUTER = 0x800000,
			
				[EnumDescription("科研项目负责人")]
				EXTIDENT_RTLEADER = 0x1000000,
			
				[EnumDescription("管理员")]
				EXTIDENT_MANAGER = 0x10000000,
			
				[EnumDescription("教师")]
				EXTIDENT_TEACHER = 0x20000000,
			
		}

	
		public uint? dwKind;		/*类型*/
	
		[FlagsAttribute]
		public enum DWKIND : uint
		{
			
				[EnumDescription("基本用户类型有效位")]
				BASICKIND_MASK = 0xFFFF,
			
				[EnumDescription("扩展用户类型有效位")]
				EXTKIND_MASK = 0xFFFF0000,
			
				[EnumDescription("不接收提醒短信")]
				EXTKIND_NOMSG = 0x10000,
			
		}

	
		public uint? dwBirthday;		/*出生日期*/
	
		public uint? dwEnrolYear;		/*入学年份(XX级)*/
	
		public uint? dwSchoolYears;		/*学制*/
	
		public uint? dwBalance;		/*余额*/
	
		public uint? dwSubsidy;		/*补助*/
	
		public uint? dwFreeTime;		/*免费时间(机时)*/
	
		public uint? dwUseQuota;		/*已用限额*/
	
		public uint? dwStatus;		/*状态 定义见UniCommon.h*/
	
		public uint? dwExpiredDate;		/*过期日*/
	
		public string szTel;		/*电话*/
	
		public string szHandPhone;		/*手机*/
	
		public string szEmail;		/*电邮*/
	
		public string szMSN;		/*MSN*/
	
		public string szQQ;		/*QQ*/
	
		public string szHomeAddr;		/*家庭住址*/
	
		public string szCurAddr;		/*校内住址*/
	
		public string szMemo;		/*说明信息*/
	
		public string szCurZip;		/*工作地址邮编(导师需要)*/
	
		public uint? dwTutorID;		/*导师账号*/
	
		public string szTutorName;		/*导师姓名*/
		};

	/*扩展账户信息*/
	
	public struct UNIACCEXTINFO
	{
		private Reserved reserved;
		
		public string pPhoto;		/*照片*/
		};

	/*获取导师*/
	
	public struct TUTORREQ
	{
		private Reserved reserved;
		
		public uint? dwTutorID;		/*导师账号*/
	
		public uint? dwStudentAccNo;		/*学生账号*/
	
		public string szTrueName;		/*导师姓名*/
	
		public uint? dwDeptID;		/*部门ID*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*导师信息*/
	
	public struct UNITUTOR
	{
		private Reserved reserved;
		
		public uint? dwAccNo;		/*账号*/
	
		public string szTrueName;		/*姓名*/
	
		public string szTel;		/*电话*/
	
		public string szHandPhone;		/*手机*/
	
		public string szEmail;		/*电邮*/
	
		public string szMemo;		/*说明信息*/
		};

	/*获取扩展身份人员信息*/
	
	public struct EXTIDENTACCREQ
	{
		private Reserved reserved;
		
		public uint? dwIdent;		/*身份*/
	
		public uint? dwAccNo;		/*账号*/
	
		public string szTrueName;		/*导师姓名*/
	
		public uint? dwDeptID;		/*部门ID*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*获取用户信息*/
	
	public struct ACCINFOREQ
	{
		private Reserved reserved;
		
		public uint? dwAccNo;		/*账号*/
	
		public string szLogonName;		/*登录名*/
		};

	/*扩展身份人员信息*/
	
	public struct EXTIDENTACC
	{
		private Reserved reserved;
		
		public uint? dwAccNo;		/*账号*/
	
		public uint? dwIdent;		/*身份*/
	
		public string szPID;		/*学工号*/
	
		public string szLogonName;		/*登录名*/
	
		public string szTrueName;		/*姓名*/
	
		public uint? dwSex;		/*性别*/
	
		public uint? dwDeptID;		/*部门ID*/
	
		public string szDeptName;		/*部门*/
	
		public string szTel;		/*电话*/
	
		public string szHandPhone;		/*手机*/
	
		public string szEmail;		/*电邮*/
	
		public string szMemo;		/*说明信息*/
		};

	/*获取导师的学生*/
	
	public struct TUTORSTUDENTREQ
	{
		private Reserved reserved;
		
		public uint? dwTutorID;		/*导师账号*/
	
		public uint? dwStatus;		/*学生状态*/
	
		public uint? dwKind;		/*学生类型*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*导师学生信息*/
	
	public struct TUTORSTUDENT
	{
		private Reserved reserved;
		
		public uint? dwTutorID;		/*导师账号*/
	
		public string szTutorName;		/*导师姓名*/
	
		public uint? dwStatus;		/*学生状态（审核状态见ADMINCHECK）*/
	
		public uint? dwKind;		/*学生类型*/
	
		[FlagsAttribute]
		public enum DWKIND : uint
		{
			
				[EnumDescription("硕士")]
				TSKIND_MASTER = 1,
			
				[EnumDescription("博士")]
				TSKIND_DOCTOR = 2,
			
		}

	
		public uint? dwAccNo;		/*账号*/
	
		public string szPID;		/*学工号*/
	
		public string szTrueName;		/*姓名*/
	
		public uint? dwEnrolYear;		/*入学年份(XX级)*/
	
		public string szTel;		/*电话*/
	
		public string szHandPhone;		/*手机*/
	
		public string szEmail;		/*电邮*/
	
		public string szMemo;		/*说明信息*/
		};

	/*导师审核学生*/
	
	public struct TUTORSTUDENTCHECK
	{
		private Reserved reserved;
		
		public uint? dwTutorID;		/*导师ID*/
	
		public uint? dwCheckStat;		/*审核状态(ADMINCHECK定义)*/
	
		public uint? dwStudentAccNo;		/*学生账号*/
	
		public string szStudentName;		/*学生姓名*/
	
		public string szCheckDetail;		/*审查说明*/
	
		public string szMemo;		/*备注*/
		};

	/**/
	
	public struct ACCCHECKREQ
	{
		private Reserved reserved;
		
		public uint? dwCheckType;		/*方式*/
	
		[FlagsAttribute]
		public enum DWCHECKTYPE : uint
		{
			
				[EnumDescription("学工号")]
				ACCCHECK_BYPERSONALID = 1,
			
				[EnumDescription("登录名")]
				ACCCHECK_BYLOGONNAME = 2,
			
				[EnumDescription("卡号")]
				ACCCHECK_BYCARDNO = 4,
			
				[EnumDescription("由Web端通过第三方认证")]
				ACCCHECK_BYWEB = 8,
			
				[EnumDescription("帐号")]
				ACCCHECK_BYACCNO = 0x10,
			
				[EnumDescription("维根32卡号")]
				ACCCHECK_BYCARDWG32 = 0x20,
			
				[EnumDescription("微信ID号")]
				ACCCHECK_BYMSN = 0x40,
			
				[EnumDescription("微信OpenID号(摇一摇)")]
				ACCCHECK_BYOPENID = 0x80,
			
				[EnumDescription("需认证密码")]
				ACCCHECK_WITHPW = 0x100,
			
				[EnumDescription("用户修改个人信息")]
				ACCCHECK_SETINFO = 0x200,
			
				[EnumDescription("从控制台登录")]
				ACCCHECK_CONLOGIN = 0x400,
			
				[EnumDescription("仅认证是否存在，不返回信息")]
				ACCCHECK_NORETURN = 0x800,
			
				[EnumDescription("签到")]
				ACCCHECK_SIGNIN = 0x1000,
			
		}

	
		public string szCheckKey;		/*认证关键字*/
	
		public string szCheckPW;		/*认证密码*/
	
	public UNIACCOUNT szAccInfo;		/*(UNIACCOUNT结构)*/
		};

	/*存退款结构*/
	
	public struct UNIDEPOSIT
	{
		private Reserved reserved;
		
		public uint? dwKind;		/*类别*/
	
		[FlagsAttribute]
		public enum DWKIND : uint
		{
			
				[EnumDescription("现金模式")]
				DPTKIND_CASH = 0x1000,
			
				[EnumDescription("现金充值")]
				DPTKIND_CASHADD = DPTKIND_CASH+0x001,
			
				[EnumDescription("现金退款")]
				DPTKIND_CASHMINUS = DPTKIND_CASH+0x002,
			
				[EnumDescription("转帐")]
				DPTKIND_CASHTRANS = DPTKIND_CASH+0x003,
			
				[EnumDescription("代收现金")]
				DPTKIND_DIRECTCASH = DPTKIND_CASH+0x004,
			
				[EnumDescription("补助模式")]
				DPTKIND_SUBSIDY = 0x2000,
			
				[EnumDescription("加补助")]
				DPTKIND_SUBSIDYADD = DPTKIND_SUBSIDY+0x001,
			
				[EnumDescription("减补助")]
				DPTKIND_SUBSIDYMINUS = DPTKIND_SUBSIDY+0x002,
			
				[EnumDescription("补助清零")]
				DPTKIND_SUBSIDYCLEAR = DPTKIND_SUBSIDY+0x004,
			
				[EnumDescription("机时模式")]
				DPTKIND_TIME = 0x3000,
			
				[EnumDescription("加机时")]
				DPTKIND_TIMEADD = DPTKIND_TIME+0x001,
			
				[EnumDescription("减机时")]
				DPTKIND_TIMEMINUS = DPTKIND_TIME+0x002,
			
				[EnumDescription("减机清零")]
				DPTKIND_TIMECLEAR = DPTKIND_TIME+0x004,
			
		}

	
		public uint? dwAccNo;		/*账号*/
	
		public string szPID;		/*学工号*/
	
		public string szTrueName;		/*真实姓名*/
	
		public uint? dwAmount;		/*数量*/
	
		public uint? dwAdminID;		/*操作员*/
	
		public string szMemo;		/*备注*/
		};

	/*支付结算提交消费流水结构*/
	
	public struct UNIPAYMENT
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*流水号*/
	
		public uint? dwAccNo;		/*帐号*/
	
		public uint? dwCostDate;		/*消费日期*/
	
		public uint? dwCostTime;		/*消费时间*/
	
		public uint? dwCardTime;		/*卡扣费时间*/
	
		public uint? dwDealTime;		/*提交流水时间*/
	
		public string szPID;		/*学工号*/
	
		public string szCardNo;		/*分配卡号*/
	
		public string szTrueName;		/*真实姓名*/
	
		public uint? dwFeeType;		/*消费类别(FEEDETAIL定义)*/
	
		public uint? dwCostMoney;		/*消费金额*/
	
		public uint? dwCostSubsidy;		/*消费补助*/
	
		public uint? dwCostFreeTime;		/*消费补助时间*/
	
		public string szPosInfo;		/*与一卡通对应的商户信息*/
	
		public string szCardCostInfo;		/*卡扣费返回信息，不同的一卡通格式和内容都不同*/
	
		public uint? dwRetStatus;		/*提交返回状态，参考UniCommon.h定义*/
	
		public uint? dwRetDealSID;		/*返回第三方流水号*/
	
		public string szRetDealInfo;		/*提交流水返回信息，不同的一卡通格式和内容都不同*/
		};

	/*获取互动信息*/
	
	public struct NOTICEREQ
	{
		private Reserved reserved;
		
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
		public uint? dwSID;		/*流水号*/
	
		public uint? dwStatus;		/*状态*/
	
		public uint? dwSubjectID;		/*通知事由ID*/
	
		public uint? dwSubjectType;		/*通知事由类别*/
	
		public uint? dwSender;		/*发送方*/
	
		public uint? dwRecipient;		/*接受方*/
		};

	/*互动信息*/
	
	public struct NOTICEINFO
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*流水号*/
	
		public uint? dwSubSysSN;		/*子系统编号*/
	
		public uint? dwStatus;		/*状态*/
	
		public uint? dwSubjectID;		/*通知事由ID*/
	
		public uint? dwSubjectType;		/*通知事由类别,审核产生的通知与审核类别(CHECKINFO::dwSubjectType)同*/
	
		public uint? dwSender;		/*发送方帐号*/
	
		public uint? dwRecipient;		/*接收方帐号*/
	
		public uint? dwNoticeMode;		/*通知方式*/
	
		public uint? dwOccurTime;		/*产生时间*/
	
		public uint? dwSendTime;		/*发送时间*/
	
		public uint? dwAffirmTime;		/*确认时间*/
	
		public string szMemo;		/*备注*/
	
		public uint? dwNoticeKind;		/*通知类别*/
	
		public uint? dwCheckStat;		/*审查状态*/
	
		public string szRecvName;		/*接收者姓名*/
	
		public string szRecvMobile;		/*接收者手机*/
	
		public string szRecvMail;		/*接收者邮箱*/
	
		public string szSenderName;		/*发送者姓名*/
	
		public string szSenderMobile;		/*发送者手机*/
	
		public string szSenderMail;		/*发送者邮箱*/
	
		public string szSessionID;		/*SessionID*/
	
		public string szReason;		/*原因*/
	
		public string szFullSendInfo;		/*发送内容*/
		};

	/*通知短信确认*/
	
	public struct NOTICEAFFIRM
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*流水号*/
	
		public uint? dwAffirmStat;		/*确认状态*/
	
		public uint? dwNoticeMode;		/*通知方式*/
	
		public uint? dwAffirmTime;		/*确认时间*/
	
		public string szMemo;		/*备注*/
		};

	/**/
	
	public struct MAJORREQ
	{
		private Reserved reserved;
		
		public uint? dwMajorID;		/*ID*/
	
		public string szMajorSN;		/*编号*/
	
		public string szMajorName;		/*名称*/
	
		public uint? dwKind;		/*类型*/
	
		public uint? dwDeptID;		/*部门ID*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/**/
	
	public struct UNIMAJOR
	{
		private Reserved reserved;
		
		public uint? dwMajorID;		/*ID*/
	
		public string szMajorSN;		/*编号*/
	
		public string szMajorName;		/*名称*/
	
		public uint? dwKind;		/*类型*/
	
		public uint? dwDeptID;		/*部门ID*/
	
		public string szDeptName;		/*部门名称*/
	
		public uint? dwSchoolYears;		/*入学年份*/
	
		public string szMemo;		/*备注*/
		};

	/**/
	
	public struct TESTDATAREQ
	{
		private Reserved reserved;
		
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
		public uint? dwStatus;		/*数据状态*/
	
		public uint? dwSID;		/*SID*/
	
		public uint? dwAccNo;		/*账号*/
	
		public uint? dwDevID;		/*设备ID*/
	
		public uint? dwRoomID;		/*房间ID*/
	
		public uint? dwLabID;		/*实验室ID*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/**/
	
	public struct UNITESTDATA
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*SID*/
	
		public uint? dwDevID;		/*设备ID*/
	
		public string szDevName;		/*设备名称*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabName;		/*实验室名称*/
	
		public uint? dwRoomID;		/*房间ID*/
	
		public string szRoomName;		/*房间名称*/
	
		public uint? dwAccNo;		/*账号*/
	
		public string szPID;		/*学工号*/
	
		public string szTrueName;		/*真实姓名*/
	
		public uint? dwSubmitDate;		/*提交日期*/
	
		public uint? dwSubmitTime;		/*提交时间*/
	
		public uint? dwFileSize;		/*文件大小*/
	
		public string szDisplayName;		/*显示名称*/
	
		public string szLocation;		/*存放位置*/
	
		public uint? dwStatus;		/*状态*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("正在上传")]
				TDSTAT_UPLOADING = 1,
			
				[EnumDescription("已上传")]
				TDSTAT_UPLOADED = 2,
			
				[EnumDescription("已下载")]
				TDSTAT_DOWNLOADED = 4,
			
				[EnumDescription("文件已删除")]
				TDSTAT_FILEDEL = 0x8,
			
		}

	
		public string szMemo;		/*说明*/
		};

	/*管理员上传实验数据*/
	
	public struct ADMINTESTDATA
	{
		private Reserved reserved;
		
		public string szLogonName;		/*登录名*/
	
		public string szPassword;		/*密码*/
	
	public UNITESTDATA TestData;		/*实验数据(UNITESTDATA)*/
	
		public string szMemo;		/*说明信息*/
		};

	/**/
	
	public struct CLOUDDISKREQ
	{
		private Reserved reserved;
		
		public uint? dwAccNo;		/*账号*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/**/
	
	public struct CLOUDDISK
	{
		private Reserved reserved;
		
		public uint? dwFileID;		/*文件ID*/
	
		public string szFileName;		/*文件名称*/
	
		public uint? dwAccNo;		/*账号*/
	
		public string szPID;		/*学工号*/
	
		public string szTrueName;		/*真实姓名*/
	
		public uint? dwSubmitDate;		/*提交日期*/
	
		public uint? dwFileSize;		/*文件大小*/
	
		public string szLocation;		/*存放位置*/
	
		public string szMemo;		/*说明*/
		};

	/**/
	
	public struct CDISKSTATREQ
	{
		private Reserved reserved;
		
		public uint? dwAccNo;		/*账号*/
		};

	/**/
	
	public struct CDISKSTAT
	{
		private Reserved reserved;
		
		public uint? dwAccNo;		/*账号*/
	
		public string szPID;		/*学工号*/
	
		public string szTrueName;		/*真实姓名*/
	
		public uint? dwTotalSize;		/*总大小*/
	
		public uint? dwUsedSize;		/*已用空间*/
	
		public uint? dwFileNum;		/*文件个数*/
	
		public string szMemo;		/*说明*/
		};

	/*获取任课教师信息*/
	
	public struct UNITEACHERREQ
	{
		private Reserved reserved;
		
		public uint? dwAccNo;		/*账号*/
	
		public string szPID;		/*工号*/
	
		public string szTrueName;		/*教师姓名(模糊匹配)*/
	
		public uint? dwDeptID;		/*部门ID*/
	
		public uint? dwCourseID;		/*课程ID*/
	
		public string szCourseName;		/*课程名称(模糊匹配)*/
	
		public uint? dwYearTerm;		/*学期编号*/
	
		public uint? dwReqProp;		/*请求附加属性*/
	
		[FlagsAttribute]
		public enum DWREQPROP : uint
		{
			
				[EnumDescription("需要教授课程")]
				DEVREQ_NEEDCOURSE = 0x1,
			
		}

	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*教授课程*/
	
	public struct TEACHCOURSE
	{
		private Reserved reserved;
		
		public uint? dwCourseID;		/*课程ID*/
	
		public string szCourseCode;		/*课程代码*/
	
		public string szCourseName;		/*课程名称*/
	
		public string szMemo;		/*备注*/
		};

	/*任课教师信息*/
	
	public struct UNITEACHER
	{
		private Reserved reserved;
		
		public uint? dwAccNo;		/*账号*/
	
		public string szPID;		/*学工号*/
	
		public string szTrueName;		/*姓名*/
	
		public uint? dwSex;		/*性别*/
	
		public uint? dwDeptID;		/*学院（部门）ID*/
	
		public string szDeptName;		/*所属学院*/
	
		public string szTel;		/*电话*/
	
		public string szHandPhone;		/*手机*/
	
		public string szEmail;		/*电邮*/
	
	public TEACHCOURSE[] TeachCourse;		/*承担课程(CUniTable<TEACHCOURSE>)*/
	
		public string szMemo;		/*说明信息*/
		};

	/*请求用户使用信息*/
	
	public struct USERCURINFOREQ
	{
		private Reserved reserved;
		
		public uint? dwAccNo;		/*账号*/
		};

	/*用户使用信息(同CONUSERINFO)*/
	
	public struct USERCURINFO
	{
		private Reserved reserved;
		
		public uint? dwUserStat;		/*用户状态*/
	
	public UNIACCOUNT AccInfo;		/*UNIACCOUNT 结构*/
	
	public UNIRESERVE ResvInfo;		/*UNIRESERVE 结构*/
	
	public UNIDEVICE DevInfo;		/*UNIDEVICE 结构*/
	
	public UNIBILL[] BillInfo;		/*账单表(CUniTable<UNIBILL>)*/
	
		public string szMemo;		/*说明信息*/
		};

	/*结束数据结构*/

	/*开始数据结构*/

	/**/
	
	public struct UNILAB
	{
		private Reserved reserved;
		
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabSN;		/*实验室编号*/
	
		public string szLabName;		/*实验室名称*/
	
		public uint? dwDeptID;		/*部门ID*/
	
		public string szDeptName;		/*部门名称*/
	
		public string szLabKindCode;		/*实验室类型编码*/
	
		public string szLabLevelCode;		/*实验室建设水平编码*/
	
		public string szLabFromCode;		/*实验室来源编码*/
	
		public string szAcademicSubjectCode;		/*所属学科编码*/
	
		public uint? dwLabClass;		/*实验室类别*/
	
		public uint? dwManGroupID;		/*管理员组ID*/
	
		public uint? dwCreateDate;		/*建立日期*/
	
		public string szLabURL;		/*实验室简介URL*/
	
		public string szMemo;		/*说明信息*/
		};

	/**/
	
	public struct LABREQ
	{
		private Reserved reserved;
		
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabSN;		/*实验室编号*/
	
		public string szLabName;		/*实验室名称*/
	
		public uint? dwDeptID;		/*部门ID*/
	
		public uint? dwLabClass;		/*实验室类别编号*/
	
		public uint? dwPurpose;		/*用途(见UNIRESERVE定义)*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/**/
	
	public struct FULLLAB
	{
		private Reserved reserved;
		
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabSN;		/*实验室编号*/
	
		public string szLabName;		/*实验室名称*/
	
		public uint? dwDeptID;		/*部门ID*/
	
		public string szDeptName;		/*部门名称*/
	
		public string szLabKindCode;		/*实验室类型编码*/
	
		public string szLabLevelCode;		/*实验室建设水平编码*/
	
		public string szLabFromCode;		/*实验室来源编码*/
	
		public string szAcademicSubjectCode;		/*所属学科编码*/
	
		public uint? dwLabClass;		/*实验室类别*/
	
		public string szLabURL;		/*实验室简介URL*/
	
		public string szMemo;		/*说明信息*/
	
		public uint? dwAttendantID;		/*值班员账号*/
	
		public string szAttendantName;		/*值班员姓名*/
	
		public uint? dwTotalDevNum;		/*设备总数*/
	
		public uint? dwUsableDevNum;		/*可用设备数*/
	
		public uint? dwIdleDevNum;		/*空闲设备数*/
		};

	/**/
	
	public struct FULLLABREQ
	{
		private Reserved reserved;
		
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabSN;		/*实验室编号*/
	
		public string szLabName;		/*实验室名称*/
	
		public uint? dwDeptID;		/*部门ID*/
	
		public uint? dwLabClass;		/*实验室类别编号*/
	
		public uint? dwPurpose;		/*用途(见UNIRESERVE定义)*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/**/
	
	public struct DEVCLSREQ
	{
		private Reserved reserved;
		
		public uint? dwClassID;		/*设备(实验室)类别ID*/
	
		public string szClassName;		/*设备(实验室)类别名称*/
	
		public uint? dwKind;		/*类别*/
	
		public uint? dwPurpose;		/*用途(见UNIRESERVE定义)*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*设备类别*/
	
	public struct UNIDEVCLS
	{
		private Reserved reserved;
		
		public uint? dwClassID;		/*设备(实验室)类别ID*/
	
		public string szClassSN;		/*设备分类号*/
	
		public string szClassName;		/*设备(实验室)类别名称*/
	
		public string szMemo;		/*说明信息*/
	
		public uint? dwKind;		/*类别类型*/
	
		[FlagsAttribute]
		public enum DWKIND : uint
		{
			
				[EnumDescription("空间")]
				CLSKIND_COMMONS = 0x1,
			
				[EnumDescription("电脑（电子阅览室)")]
				CLSKIND_COMPUTER = 0x2,
			
				[EnumDescription("外借设备")]
				CLSKIND_LOAN = 0x4,
			
				[EnumDescription("座位")]
				CLSKIND_SEAT = 0x8,
			
				[EnumDescription("实验仪器")]
				CLSKIND_INSTRUMENT = 0x10,
			
				[EnumDescription("大类掩码")]
				CLSKIND_MASK = 0xFF,
			
				[EnumDescription("单人研修间")]
				CLSCOMMONS_SINGLE = (CLSKIND_COMMONS + 0x100),
			
				[EnumDescription("团体间(多人研讨室)")]
				CLSCOMMONS_GROUP = (CLSKIND_COMMONS + 0x200),
			
				[EnumDescription("开放活动室")]
				CLSCOMMONS_ACTIVITY = (CLSKIND_COMMONS + 0x400),
			
				[EnumDescription("多人研修间")]
				CLSCOMMONS_MULTIPLE = (CLSKIND_COMMONS + 0x800),
			
				[EnumDescription("心里咨询室")]
				CLSCOMMONS_CONSULTING = (CLSKIND_COMMONS + 0x1000),
			
				[EnumDescription("教室")]
				CLSCOMMONS_CLASSROOM = (CLSKIND_COMMONS + 0x2000),
			
				[EnumDescription("会议室")]
				CLSCOMMONS_MEETINGROOM = (CLSKIND_COMMONS + 0x4000),
			
				[EnumDescription("普通电脑")]
				CLSCOMPUTER_PC = (CLSKIND_COMPUTER + 0x100),
			
				[EnumDescription("苹果电脑")]
				CLSCOMPUTER_MAC = (CLSKIND_COMPUTER + 0x200),
			
		}

	
		public uint? dwResv1;		/*保留字段1*/
	
		public uint? dwResv2;		/*保留字段2*/
	
		public string szDevClsURL;		/*简介URL*/
	
		public string szExtInfo;		/*扩展信息*/
		};

	/**/
	
	public struct DEVKINDREQ
	{
		private Reserved reserved;
		
		public uint? dwKindID;		/*设备名称类别ID*/
	
		public string szKindName;		/*设备名称*/
	
		public string szClassName;		/*类别名(*/
	
		public uint? dwProperty;		/*设备属性*/
	
		public uint? dwClassKind;		/*类别(见UNIDEVCLS的Kind定义)*/
	
		public string szKindIDs;		/*所属类型,多个用逗号隔开*/
	
		public uint? dwExtRelatedID;		/*扩展关联ID*/
	
		public uint? dwPurpose;		/*用途(见UNIRESERVE定义)*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*设备名称类别*/
	
	public struct UNIDEVKIND
	{
		private Reserved reserved;
		
		public uint? dwKindID;		/*设备名称类别ID*/
	
		public string szKindName;		/*设备名称*/
	
		public string szFuncCode;		/*设备功能用途编码*/
	
		public string szModel;		/*设备型号*/
	
		public string szSpecification;		/*设备规格*/
	
		public string szProducer;		/*制造商*/
	
		public uint? dwNationCode;		/*国别码*/
	
		public uint? dwProperty;		/*设备属性*/
	
		[FlagsAttribute]
		public enum DWPROPERTY : uint
		{
			
				[EnumDescription("排它独占（同一时间仅支持一个小组或个人预约）")]
				DEVPROP_EXCLUSIVE = 0x1,
			
				[EnumDescription("共享（可同时支持多个小组或多人预约）")]
				DEVPROP_SHARE = 0x2,
			
				[EnumDescription("长期预约(数天，数周甚至数月)")]
				DEVPROP_LONGTERMRESV = 0x4,
			
				[EnumDescription("按类型预约")]
				DEVPROP_KINDRESV = 0x8,
			
				[EnumDescription("外借")]
				DEVPROP_LEASE = 0x100,
			
				[EnumDescription("使用完需管理员检查确认")]
				DEVPROP_USEDCHECK = 0x200,
			
				[EnumDescription("DEVKIND相关属性取前16种")]
				DEVKINDPROP_MASK = 0xFFFF,
			
		}

	
		public uint? dwClassID;		/*所属功能类别ID*/
	
		public string szClassSN;		/*设备分类号*/
	
		public string szClassName;		/*类别名(*/
	
		public uint? dwClassKind;		/*类别(见UNIDEVCLS的Kind定义)*/
	
		public uint? dwMaxUsers;		/*总预约最多同时使用人数*/
	
		public uint? dwMinUsers;		/*每次预约最少同时使用人数*/
	
		public uint? dwTotalNum;		/*设备总数*/
	
		public uint? dwUsableNum;		/*可用设备数（预约判断用）*/
	
		public string szOperaCert;		/*操作证书*/
	
		public string szDevKindURL;		/*设备详细介绍的URL地址*/
	
		public string szMemo;		/*说明信息*/
		};

	/*获取楼宇信息请求*/
	
	public struct BUILDINGREQ
	{
		private Reserved reserved;
		
		public uint? dwBuildingID;		/*楼宇ID*/
	
		public string szBuildingName;		/*楼宇名称*/
	
		public string szBuildingNo;		/*楼宇号*/
	
		public string szCampusIDs;		/*校区ID,多个用逗号隔开*/
	
		public uint? dwActivitySN;		/*活动类型编号*/
	
		public uint? dwPurpose;		/*用途(见UNIRESERVE定义)*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*楼宇名称*/
	
	public struct UNIBUILDING
	{
		private Reserved reserved;
		
		public uint? dwBuildingID;		/*楼宇ID*/
	
		public string szBuildingName;		/*楼宇名称*/
	
		public string szBuildingNo;		/*楼宇号*/
	
		public string szMapIndex;		/*地图索引*/
	
		public string szBuildingURL;		/*详细介绍的URL地址*/
	
		public uint? dwCampusID;		/*校区ID*/
	
		public string szCampusName;		/*校区名称*/
	
		public uint? dwCampusKind;		/*校区类型（扩展）*/
	
		public string szMemo;		/*说明信息*/
		};

	/*获取房间信息请求*/
	
	public struct ROOMREQ
	{
		private Reserved reserved;
		
		public uint? dwRoomID;		/*房间ID*/
	
		public string szRoomName;		/*房间名称*/
	
		public string szRoomNo;		/*房间号*/
	
		public uint? dwLabClass;		/*实验室类别编号*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public string szBuildingIDs;		/*楼宇ID,多个用逗号隔开*/
	
		public string szCampusIDs;		/*校区ID,多个用逗号隔开*/
	
		public uint? dwInClassKind;		/*存放设备类别(见UNIDEVCLS的Kind定义)*/
	
		public uint? dwPurpose;		/*用途(见UNIRESERVE定义)*/
	
		public uint? dwProperty;		/*扩展属性*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*房间名称*/
	
	public struct UNIROOM
	{
		private Reserved reserved;
		
		public uint? dwRoomID;		/*房间ID*/
	
		public string szRoomName;		/*房间名称*/
	
		public string szRoomNo;		/*房间号*/
	
		public uint? dwInClassKind;		/*存放设备类别(见UNIDEVCLS的Kind定义)*/
	
		public string szFloorNo;		/*所在楼层*/
	
		public uint? dwBuildingID;		/*楼宇ID*/
	
		public string szBuildingNo;		/*大楼编号(*/
	
		public string szBuildingName;		/*大楼名称(*/
	
		public uint? dwRoomSize;		/*房间面积(平方)*/
	
		public string szMapIndex;		/*地图索引*/
	
		public string szRoomURL;		/*详细介绍的URL地址*/
	
		public string szSubRooms;		/*下属房间(房间编号，可多个，逗号隔开)*/
	
		public string szLabKindCode;		/*实验室类型编码*/
	
		public string szLabLevelCode;		/*实验室建设水平编码*/
	
		public string szLabFromCode;		/*实验室来源编码*/
	
		public string szAcademicSubjectCode;		/*所属学科编码*/
	
		public uint? dwLabClass;		/*实验室类别*/
	
		public uint? dwCreateDate;		/*建立日期*/
	
		public uint? dwOpenRuleSN;		/*开放规则编号*/
	
		public string szOpenRuleName;		/*开放规则名称*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabSN;		/*实验室编号*/
	
		public string szLabName;		/*实验室名称*/
	
		public string szLabURL;		/*详细介绍的URL地址*/
	
		public uint? dwDeptID;		/*部门ID*/
	
		public string szDeptName;		/*部门名称*/
	
		public uint? dwManGroupID;		/*房间管理员组ID*/
	
		public string szManGroupName;		/*管理员组名称*/
	
		public uint? dwManMode;		/*控制方式*/
	
		[FlagsAttribute]
		public enum DWMANMODE : uint
		{
			
				[EnumDescription("门禁控制")]
				ROOMMAN_DOORLOCK = 1,
			
				[EnumDescription("摄像监控")]
				ROOMMAN_CAMERA = 2,
			
				[EnumDescription("语音系统")]
				ROOMMAN_SOUND = 4,
			
				[EnumDescription("门未关报警")]
				ROOMMAN_DOORWARNING = 0x100,
			
				[EnumDescription("允许所有人在开放时间进出")]
				ROOMMAN_FREEINOUT = 0x200,
			
		}

	
		public uint? dwCtrlSN;		/*控制器编号*/
	
		public uint? dwCampusID;		/*校区ID*/
	
		public string szCampusName;		/*校区名称*/
	
		public uint? dwCampusKind;		/*校区类型（扩展）*/
	
		public string szMemo;		/*说明信息*/
	
		public string szMAIP;		/*手机签到IP段*/
	
		public uint? dwProperty;		/*扩展属性*/
	
		[FlagsAttribute]
		public enum DWPROPERTY : uint
		{
			
				[EnumDescription("手机签到与通道机集成")]
				ROOMPROPHP_AUTOGATE = 0x1,
			
				[EnumDescription("手机签到判断接入IP")]
				ROOMPROPHP_IP = 0x2,
			
				[EnumDescription("手机签到GPS定位")]
				ROOMPROPHP_GPS = 0x4,
			
				[EnumDescription("不支持手机扫码预约")]
				ROOMPROPHP_NORESV = 0x10,
			
				[EnumDescription("不支持手机扫码签到")]
				ROOMPROPHP_NOSIGN = 0x20,
			
				[EnumDescription("不支持手机扫码暂时离开")]
				ROOMPROPHP_NOLEAVE = 0x40,
			
				[EnumDescription("不支持手机扫码结束使用")]
				ROOMPROPHP_NOEXIT = 0x80,
			
				[EnumDescription("不支持网上预约")]
				ROOMPROP_NORESV = 0x100000,
			
				[EnumDescription("组合房间(RESVPROP_COMBINEROOM)")]
				ROOMPROP_COMBINE = 0x2000000,
			
				[EnumDescription("子房间（被组合RESVPROP_SUBROOM）")]
				ROOMPROP_SUBROOM = 0x4000000,
			
		}

		};

	/*获取房间信息请求*/
	
	public struct FULLROOMREQ
	{
		private Reserved reserved;
		
		public uint? dwRoomID;		/*房间ID*/
	
		public string szRoomName;		/*房间名称*/
	
		public string szRoomNo;		/*房间号*/
	
		public uint? dwLabClass;		/*实验室类别编号*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public string szBuildingIDs;		/*楼宇ID,多个用逗号隔开*/
	
		public string szCampusIDs;		/*校区ID,多个用逗号隔开*/
	
		public uint? dwInClassKind;		/*存放设备类别(见UNIDEVCLS的Kind定义)*/
	
		public uint? dwPurpose;		/*用途(见UNIRESERVE定义)*/
	
		public uint? dwProperty;		/*扩展属性*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*房间完整信息*/
	
	public struct FULLROOM
	{
		private Reserved reserved;
		
		public uint? dwRoomID;		/*房间ID*/
	
		public string szRoomName;		/*房间名称*/
	
		public string szRoomNo;		/*房间号*/
	
		public uint? dwInClassKind;		/*存放设备类别(见UNIDEVCLS的Kind定义)*/
	
		public string szFloorNo;		/*所在楼层*/
	
		public uint? dwBuildingID;		/*楼宇ID*/
	
		public string szBuildingNo;		/*大楼编号(*/
	
		public string szBuildingName;		/*大楼名称(*/
	
		public uint? dwRoomSize;		/*房间面积(平方)*/
	
		public string szMapIndex;		/*地图索引*/
	
		public string szRoomURL;		/*详细介绍的URL地址*/
	
		public uint? dwOpenRuleSN;		/*开放规则编号*/
	
		public string szOpenRuleName;		/*开放规则名称*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabSN;		/*实验室编号*/
	
		public string szLabName;		/*实验室名称*/
	
		public string szLabKindCode;		/*实验室类型编码*/
	
		public string szLabLevelCode;		/*实验室建设水平编码*/
	
		public string szLabFromCode;		/*实验室来源编码*/
	
		public string szAcademicSubjectCode;		/*所属学科编码*/
	
		public uint? dwLabClass;		/*实验室类别*/
	
		public uint? dwCreateDate;		/*建立日期*/
	
		public string szLabURL;		/*详细介绍的URL地址*/
	
		public uint? dwDeptID;		/*部门ID*/
	
		public string szDeptName;		/*部门名称*/
	
		public uint? dwManGroupID;		/*管理员组ID*/
	
		public string szManGroupName;		/*管理员组名称*/
	
		public uint? dwAttendantID;		/*值班员账号*/
	
		public string szAttendantName;		/*值班员姓名*/
	
		public uint? dwStatus;		/*房间门禁状态见 UNIDCS定义*/
	
		public uint? dwStatChgTime;		/*状态改变开始时间(time函数秒)*/
	
		public string szStatInfo;		/*状态描述*/
	
		public uint? dwManMode;		/*控制方式*/
	
		public uint? dwCtrlSN;		/*控制器编号*/
	
		public uint? dwCampusID;		/*校区ID*/
	
		public string szCampusName;		/*校区名称*/
	
		public uint? dwCampusKind;		/*校区类型（扩展）*/
	
		public uint? dwTotalDevNum;		/*设备总数*/
	
		public uint? dwUsableDevNum;		/*可用设备数*/
	
		public uint? dwIdleDevNum;		/*空闲设备数*/
	
		public string szMemo;		/*说明信息*/
	
		public uint? dwProperty;		/*扩展属性*/
		};

	/*获取房间基本信息请求*/
	
	public struct BASICROOMREQ
	{
		private Reserved reserved;
		
		public uint? dwRoomID;		/*房间ID*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public uint? dwInClassKind;		/*存放设备类别(见UNIDEVCLS的Kind定义)*/
	
		public uint? dwDoorStat;		/*房间门禁状态见 UNIDCS定义*/
	
		public uint? dwUseStat;		/*房间使用状态*/
	
		[FlagsAttribute]
		public enum DWUSESTAT : uint
		{
			
				[EnumDescription("空闲")]
				ROOMUSESTAT_IDLE = 0x1,
			
				[EnumDescription("正在上课")]
				ROOMUSESTAT_INUSE = 0x2,
			
		}

	
		public uint? dwUsePurpose;		/*见UNIDEVICE定义*/
	
		public uint? dwProperty;		/*扩展属性*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*房间基本信息*/
	
	public struct BASICROOM
	{
		private Reserved reserved;
		
		public uint? dwRoomID;		/*房间ID*/
	
		public string szRoomName;		/*房间名称*/
	
		public string szRoomNo;		/*房间号*/
		};

	/*获取通道门信息请求*/
	
	public struct CHANNELGATEREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*方式*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("全部")]
				CHANNELGATEGET_BYALL = 0,
			
				[EnumDescription("ID")]
				CHANNELGATEGET_BYID = 1,
			
				[EnumDescription("名称")]
				CHANNELGATEGET_BYNAME = 2,
			
				[EnumDescription("通道编号")]
				CHANNELGATEGET_BYSN = 3,
			
				[EnumDescription("所在楼层")]
				CHANNELGATEGET_FLOOR = 4,
			
		}

	
		public string szGetKey;		/*条件值*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*通道门*/
	
	public struct UNICHANNELGATE
	{
		private Reserved reserved;
		
		public uint? dwChannelGateID;		/*通道门ID*/
	
		public string szChannelGateName;		/*通道门名称*/
	
		public string szChannelGateNo;		/*通道门编号*/
	
		public string szRelatedRooms;		/*关联房间(房间编号，可多个，逗号隔开)*/
	
		public string szFloorNo;		/*所在楼层*/
	
		public uint? dwBuildingID;		/*楼宇ID*/
	
		public string szBuildingNo;		/*大楼编号(*/
	
		public string szBuildingName;		/*大楼名称(*/
	
		public uint? dwManGroupID;		/*管理员组ID*/
	
		public string szManGroupName;		/*管理员组名称*/
	
		public uint? dwUseGroupID;		/*允许用户组ID*/
	
		public string szUseGroupName;		/*允许用户组名称*/
	
		public uint? dwOpenRuleSN;		/*开放规则编号*/
	
		public uint? dwStatus;		/*门禁状态见 UNIDCS定义*/
	
		public uint? dwStatChgTime;		/*状态改变开始时间(time函数秒)*/
	
		public string szStatInfo;		/*状态描述*/
	
		public uint? dwManMode;		/*房间控制方式，见UNIROOM*/
	
		public uint? dwCtrlSN;		/*控制器编号*/
	
		public string szMemo;		/*说明信息*/
		};

	/*控制房间命令*/
	
	public struct CHANNELGATECTRLINFO
	{
		private Reserved reserved;
		
		public uint? dwChannelGateID;		/*通道门ID*/
	
		public string szChannelGateNo;		/*通道门编号*/
	
		public uint? dwCmd;		/*控制命令,参考DEVCTRLINFO定义*/
	
		public string szParam;		/*控制参数*/
	
		public string szMemo;		/*备注*/
		};

	/*获取房间组合请求*/
	
	public struct ROOMGROUPREQ
	{
		private Reserved reserved;
		
		public uint? dwRoomNum;		/*组合房间数*/
	
		public uint? dwRoomID;		/*房间ID*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*房间组合成员*/
	
	public struct RGMEMBER
	{
		private Reserved reserved;
		
		public uint? dwRoomID;		/*房间ID*/
	
		public string szRoomNo;		/*房间号*/
	
		public string szRoomName;		/*房间名称*/
	
		public uint? dwDevNum;		/*设备数*/
		};

	/*房间组合*/
	
	public struct ROOMGROUP
	{
		private Reserved reserved;
		
		public uint? dwRGID;		/*房间组合ID*/
	
		public string szRGName;		/*房间组合名称*/
	
		public uint? dwRoomNum;		/*组合房间数*/
	
	public RGMEMBER[] rgMember;		/*CUniTable[RGMEMBER]*/
		};

	/*获取设备列表*/
	
	public struct DEVREQ
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*设备ID*/
	
		public string szSearchKey;		/*搜索关键字*/
	
		public uint? dwDevSN;		/*设备编号*/
	
		public string szAssertSN;		/*用户方资产编号*/
	
		public string szTagID;		/*RFID标签ID*/
	
		public string szLabIDs;		/*实验室ID,多个用逗号隔开*/
	
		public string szRoomIDs;		/*房间ID,多个用逗号隔开*/
	
		public uint? dwResvID;		/*预约ID*/
	
		public string szKindIDs;		/*所属类型,多个用逗号隔开*/
	
		public string szClassIDs;		/*所属功能类别ID,多个用逗号隔开*/
	
		public string szDeptIDs;		/*学院ID,多个用逗号隔开*/
	
		public string szBuildingIDs;		/*楼宇ID,多个用逗号隔开*/
	
		public string szCampusIDs;		/*校区ID,多个用逗号隔开*/
	
		public uint? dwDevStat;		/*设备状态*/
	
		public uint? dwRunStat;		/*设备运行状态*/
	
		public uint? dwUnNeedRunStat;		/*不包含运行状态*/
	
		public uint? dwCtrlMode;		/*控制方式*/
	
		public uint? dwProperty;		/*设备属性*/
	
		public uint? dwClassKind;		/*类别(见UNIDEVCLS的Kind定义)*/
	
		public uint? dwManGroupID;		/*管理员组ID*/
	
		public uint? dwAttendantID;		/*值班员ID*/
	
		public string szAttendantName;		/*值班员姓名(模糊)*/
	
		public uint? dwMinUnitPrice;		/*最低价格*/
	
		public uint? dwMaxUnitPrice;		/*最大价格*/
	
		public uint? dwSPurchaseDate;		/*开始采购日期*/
	
		public uint? dwEPurchaseDate;		/*截止采购日期*/
	
		public uint? dwReqProp;		/*请求附加属性*/
	
		[FlagsAttribute]
		public enum DWREQPROP : uint
		{
			
				[EnumDescription("需要预约信息")]
				DEVREQ_NEEDDEVRESV = 0x1,
			
				[EnumDescription("需要当前使用信息")]
				DEVREQ_NEEDDEVUSE = 0x2,
			
				[EnumDescription("需要启用样品信息")]
				DEVREQ_NEEDSAMPLE = 0x4,
			
				[EnumDescription("需要所有样品信息")]
				DEVREQ_NEEDFULLSAMPLE = 0x8,
			
		}

	
		public uint? dwPurpose;		/*用途(见UNIRESERVE定义)*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*计费信息明细*/
	
	public struct FDINFO
	{
		private Reserved reserved;
		
		public uint? dwBeginTime;		/*开始时间*/
	
		public uint? dwEndTime;		/*结束时间*/
	
		public uint? dwFeeType;		/*收费类别(FEEDETAIL定义)*/
	
		public uint? dwUsablePayKind;		/*可用缴费方式(见UNIBILL定义)*/
	
		public uint? dwDefaultCheckStat;		/*CHECKINFO定义的管理员审核状态*/
	
		public uint? dwUnitFee;		/*费率*/
	
		public uint? dwUnitTime;		/*单位时间*/
	
		public uint? dwRoundOff;		/*舍入分界点(小于单位时间)*/
	
		public uint? dwIgnoreTime;		/*不计费时间*/
	
		public uint? dwHolidayCoef;		/*假日系数*/
	
		public string szPosInfo;		/*与一卡通对应的商户信息*/
	
		public uint? dwUseTime;		/*使用时间*/
	
		public uint? dwFeeTime;		/*计费时间*/
	
		public uint? dwCostMoney;		/*费用*/
	
		public uint? dwCostSubsidy;		/*使用补助*/
	
		public uint? dwCostFreeTime;		/*使用免费时间(机时)*/
		};

	/*计费信息*/
	
	public struct UNIACCTINFO
	{
		private Reserved reserved;
		
		public uint? dwBeginTime;		/*开始时间*/
	
		public uint? dwEndTime;		/*结束时间*/
	
		public uint? dwUseTime;		/*使用时间*/
	
		public uint? dwIdent;		/*身份（0表示无限制）*/
	
		public uint? dwDeptID;		/*部门（0表示无限制）*/
	
		public uint? dwDevKind;		/*设备类型（0表示无限制）*/
	
		public uint? dwGroupID;		/*指定用户组（0表示无限制）*/
	
		public uint? dwPurpose;		/*用途*/
	
		public uint? dwResvID;		/*预约号*/
	
		public uint? dwAccNo;		/*用户账号*/
	
		public string szPID;		/*学工号*/
	
		public string szLogonName;		/*登录名*/
	
		public string szCardNo;		/*卡号*/
	
		public string szTrueName;		/*姓名*/
	
		public string szClassName;		/*班级*/
	
		public string szDeptName;		/*部门*/
	
		public uint? dwSex;		/*性别*/
	
		public uint? dwDevID;		/*设备ID*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public uint? dwDevSN;		/*设备编号*/
	
		public string szLabSN;		/*实验室编号*/
	
		public uint? dwSID;		/*分配流水号*/
	
		public uint? dwBalance;		/*余额*/
	
		public uint? dwSubsidy;		/*补助*/
	
		public uint? dwFreeTime;		/*免费时间*/
	
		public uint? dwUseQuota;		/*已用限额*/
	
		public uint? dwFeeSN;		/*费率编号*/
	
		public uint? dwDeadLine;		/*该用户最长可用时间*/
	
	public FDINFO[] szFDInfo;		/*CUniTable[FDINFO]*/
	
		public string szMemo;		/*备注（扩展用）*/
		};

	/*获取设备监控器请求*/
	
	public struct DEVMONITORREQ
	{
		private Reserved reserved;
		
		public uint? dwMonitorID;		/*监控器ID*/
	
		public uint? dwMonitorType;		/*监控器类别*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*设备监控器*/
	
	public struct DEVMONITOR
	{
		private Reserved reserved;
		
		public uint? dwMonitorID;		/*监控器ID*/
	
		public uint? dwMonitorType;		/*监控器类别*/
	
		[FlagsAttribute]
		public enum DWMONITORTYPE : uint
		{
			
				[EnumDescription("RFID 摩托FX7400")]
				MONITOR_FX7400 = 1,
			
				[EnumDescription("RFID 营信YXU2881")]
				MONITOR_YXU2881 = 2,
			
		}

	
		public string szMonitorName;		/*监控器名称*/
	
		public string szIP;		/*IP地址*/
	
		public uint? dwPort;		/*端口*/
	
		public string szMemo;		/*备注*/
		};

	/*获取监控器与设备的对应关系请求*/
	
	public struct MONDEVREQ
	{
		private Reserved reserved;
		
		public uint? dwMonitorID;		/*监控器ID*/
	
		public uint? dwMonitorType;		/*监控器类别*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*监控器与设备的对应关系*/
	
	public struct MONDEV
	{
		private Reserved reserved;
		
		public uint? dwMonitorID;		/*监控器ID*/
	
		public uint? dwMonitorType;		/*监控器类别*/
	
		public string szMonitorName;		/*监控器名称*/
	
		public uint? dwLabID;		/*监控器ID*/
	
		public uint? dwDevID;		/*设备ID*/
	
		public uint? dwDevSN;		/*设备编号*/
	
		public string szTagID;		/*RFID标签ID*/
	
		public string szDevName;		/*实验设备名称*/
	
		public string szMemo;		/*备注*/
		};

	/*设备监控状态*/
	
	public struct DEVMONITORSTAT
	{
		private Reserved reserved;
		
		public uint? dwLabID;		/*监控器ID*/
	
		public uint? dwDevID;		/*设备ID*/
	
		public uint? dwMonitorStat;		/*监控状态(dwRunStat 的智能有人定义）*/
		};

	/*设备预约明细*/
	
	public struct DEVRESV
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*预约ID*/
	
		public uint? dwUsePurpose;		/*同UNIRESERVE的dwPurpose*/
	
		public uint? dwResvBeginTime;		/*预约开始时间*/
	
		public uint? dwResvEndTime;		/*预约结束时间*/
	
		public string szResvMemberName;		/*预约成员名*/
	
		public uint? dwTeacherID;		/*任课教师ID*/
	
		public string szTeacherName;		/*任课教师姓名*/
		};

	/*设备使用信息*/
	
	public struct DEVUSEINFO
	{
		private Reserved reserved;
		
		public uint? dwAccNo;		/*用户账号*/
	
		public string szTrueName;		/*姓名*/
	
		public uint? dwResvID;		/*预约号*/
	
		public uint? dwBeginTime;		/*开始时间*/
	
	public UNIACCTINFO FeeInfo;		/*CUniStruct[UNIACCTINFO]*/
	
		public uint? dwUserUseStat;		/*用户使用状态*/
	
		public uint? dwLeaveTime;		/*暂时离开时间*/
	
		public uint? dwLeaveHoldSec;		/*暂时离开保留时间(秒）*/
	
		public uint? dwQuotaRule;		/*限制规则(日累计，次累计，机器忙等(缺省0))*/
	
		public uint? dwQuotaTime;		/*限制使用时间(缺省-1)*/
	
		public uint? dwLoanAdminID;		/*外借管理员ID*/
	
		public string szLoanAdminName;		/*外借管理员姓名*/
	
		public uint? dwReturnAdminID;		/*归还管理员ID*/
	
		public string szReturnAdminName;		/*归还管理员姓名*/
	
		public uint? dwTutorID;		/*导师账号*/
	
		public string szTutorName;		/*导师姓名*/
	
		public string szMemo;		/*备注（扩展用）*/
		};

	/*设备信息*/
	
	public struct UNIDEVICE
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*设备ID*/
	
		public uint? dwDevSN;		/*设备编号*/
	
		public string szAssertSN;		/*用户方资产编号*/
	
		public string szTagID;		/*RFID标签ID*/
	
		public string szOriginSN;		/*原厂系列号*/
	
		public string szDevName;		/*实验设备名称*/
	
		public uint? dwUnitPrice;		/*设备单价*/
	
		public uint? dwPurchaseDate;		/*采购日期 YYYYMMDD*/
	
		public uint? dwDevStat;		/*设备状态*/
	
		[FlagsAttribute]
		public enum DWDEVSTAT : uint
		{
			
				[EnumDescription("设备禁用")]
				DEVSTAT_DISABLED = 0x1,
			
				[EnumDescription("设备已损坏")]
				DEVSTAT_DAMAGED = 0x2,
			
				[EnumDescription("设备维修中")]
				DEVSTAT_MAINTAIN = 0x4,
			
				[EnumDescription("设备不可用")]
				DEVSTAT_UNAVAILABLE = 0xFF,
			
				[EnumDescription("部分损坏（还可用）")]
				DEVSTAT_PARTDAMAGED = 0x100,
			
				[EnumDescription("需上传软件")]
				DEVSTAT_SWNEEDUPLOAD = 0x1000,
			
		}

	
		public uint? dwCtrlMode;		/*控制方式*/
	
		[FlagsAttribute]
		public enum DWCTRLMODE : uint
		{
			
				[EnumDescription("门禁控制")]
				DEVCTRL_DOORCTRL = 0x1,
			
				[EnumDescription("登录认证")]
				DEVCTRL_LOGIN = 0x2,
			
				[EnumDescription("刷卡认证")]
				DEVCTRL_CARD = 0x4,
			
				[EnumDescription("人工管理")]
				DEVCTRL_BYHAND = 0x8,
			
				[EnumDescription("预约生效自动开始使用")]
				DEVCTRL_AUTOUSE = 0x20,
			
				[EnumDescription("电源控制")]
				DEVCTRL_POWERCTRL = 0x10,
			
				[EnumDescription("专用卡(使用者需预约后换卡)")]
				DEVCTRL_PRIVATECARD = 0x100,
			
				[EnumDescription("智能有人判断")]
				DEVCTRL_PERSONDETECT = 0x200,
			
		}

	
		public uint? dwRunStat;		/*设备状态*/
	
		[FlagsAttribute]
		public enum DWRUNSTAT : uint
		{
			
				[EnumDescription("设备运行中")]
				DEVSTAT_RUNNING = 0x1,
			
				[EnumDescription("设备使用中")]
				DEVSTAT_INUSE = 0x2,
			
				[EnumDescription("设备预约中")]
				DEVSTAT_RESERVE = 0x4,
			
				[EnumDescription("设备超时运行")]
				DEVSTAT_TIMEOUT = 0x8,
			
				[EnumDescription("免登录模式")]
				DEVSTAT_NOLOGON = 0x10,
			
				[EnumDescription("已锁定")]
				DEVSTAT_LOCKED = 0x20,
			
				[EnumDescription("光驱已锁定")]
				DEVSTAT_CDLOCKED = 0x40,
			
				[EnumDescription("U盘已锁定")]
				DEVSTAT_USBLOCKED = 0x80,
			
				[EnumDescription("暂时离开(Leave for a while)")]
				DEVSTAT_LEAVEFW = 0x100,
			
				[EnumDescription("已借出")]
				DEVSTAT_LOAN = 0x200,
			
				[EnumDescription("广播中")]
				DEVSTAT_TELECAST = 0x400,
			
				[EnumDescription("免登录不弹出登录框")]
				DEVSTAT_NOLOGONFACE = 0x800,
			
				[EnumDescription("清除URL Cache")]
				DEVSTAT_CLEARURLCACHE = 0x1000,
			
				[EnumDescription("电源未接通")]
				DRUNSTAT_POWEROFF = 0x2000,
			
				[EnumDescription("电源已接通")]
				DRUNSTAT_POWERON = 0x4000,
			
				[EnumDescription("电源工作中")]
				DRUNSTAT_POWERWORKING = 0x8000,
			
				[EnumDescription("门开")]
				DRUNSTAT_DOOROPEN = 0x10000,
			
				[EnumDescription("门关")]
				DRUNSTAT_DOORCLOSED = 0x20000,
			
				[EnumDescription("控制器故障")]
				DRUNSTAT_CONTROLLER_TROUBLE = 0x40000,
			
				[EnumDescription("智能判断有人")]
				DRUNSTAT_WITHPERSON = 0x80000,
			
				[EnumDescription("智能判断无人")]
				DRUNSTAT_NOPERSON = 0x100000,
			
				[EnumDescription("暂时离开已通知")]
				DEVSTAT_LEAVENOTICED = 0x200000,
			
		}

	
		public uint? dwStatChgTime;		/*状态改变开始时间(time函数秒)*/
	
		public string szStatInfo;		/*状态描述*/
	
		public uint? dwClassID;		/*设备功能类别*/
	
		public string szClassSN;		/*设备分类号*/
	
		public string szClassName;		/*设备类别名称*/
	
		public uint? dwClassKind;		/*类别(见UNIDEVCLS的Kind定义)*/
	
		public uint? dwKindID;		/*设备类型*/
	
		public string szKindName;		/*设备名称*/
	
		public string szFuncCode;		/*设备功能用途编码*/
	
		public string szModel;		/*设备型号*/
	
		public string szSpecification;		/*设备规格*/
	
		public uint? dwProperty;		/*设备属性（前16种为UNIDEVKIND定义*/
	
		[FlagsAttribute]
		public enum DWPROPERTY : uint
		{
			
				[EnumDescription("支持活动安排")]
				DEVPROP_ACTIVITYPLAN = 0x10000,
			
				[EnumDescription("无需预约，直接登录")]
				DEVPROP_DIRECTLOGIN = 0x20000,
			
				[EnumDescription("不支持强制下机")]
				DEVPROP_UNFORCEOUT = 0x40000,
			
				[EnumDescription("不支持网上预约")]
				DEVPROP_NORESV = 0x80000,
			
				[EnumDescription("教学设备")]
				DEVPROP_FORTEACHING = 0x100000,
			
				[EnumDescription("科研设备")]
				DEVPROP_FORRESEARCH = 0x200000,
			
				[EnumDescription("主设备的辅助设备")]
				DEVPROP_SUB = 0x400000,
			
				[EnumDescription("不支持管理员直接登录")]
				DEVPROP_NOMANAGERIN = 0x800000,
			
				[EnumDescription("系统空闲屏幕广播")]
				DEVPROP_IDLECAST = 0x1000000,
			
				[EnumDescription("拥有设备开放规则")]
				DEVPROP_DEVOPENRULE = 0x2000000,
			
				[EnumDescription("可独立开放的主设备")]
				DEVPROP_MAIN = 0x4000000,
			
				[EnumDescription("与第三方共享设备")]
				DEVPROP_THIRDSHARE = 0x8000000,
			
				[EnumDescription("UNIDEVICE相关属性取后16种")]
				DEVPROP_MASK = 0xFFFF0000,
			
		}

	
		public uint? dwMaxUsers;		/*最多同时使用人数*/
	
		public uint? dwMinUsers;		/*最少同时使用人数*/
	
		public string szOperaCert;		/*操作证书*/
	
		public string szDevKindURL;		/*设备类型详细介绍的URL地址*/
	
		public string szDevURL;		/*设备详细介绍的URL地址*/
	
		public uint? dwManGroupID;		/*管理员组ID*/
	
		public string szManGroupName;		/*管理员组名称*/
	
		public uint? dwUseGroupID;		/*设备使用组ID*/
	
		public uint? dwOpenRuleSN;		/*开放规则编号*/
	
		public string szPCName;		/*登录设备机器名*/
	
		public string szIP;		/*计算机IP地址*/
	
		public string szMAC;		/*网卡地址*/
	
		public string szMemo;		/*说明信息*/
	
		public uint? dwRoomID;		/*房间ID*/
	
		public string szRoomNo;		/*房间号（不重复,用于关联门禁，格式B-F-N,建筑编号-楼层号-房间编号）*/
	
		public string szRoomName;		/*房间名称*/
	
		public uint? dwManMode;		/*控制方式(UNIROOM中定义)*/
	
		public string szFloorNo;		/*所在楼层*/
	
		public uint? dwBuildingID;		/*楼宇ID*/
	
		public string szBuildingNo;		/*大楼编号(*/
	
		public string szBuildingName;		/*大楼名称(*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabSN;		/*实验室编号*/
	
		public string szLabName;		/*实验室名称*/
	
		public uint? dwDeptID;		/*部门ID*/
	
		public string szDeptName;		/*部门*/
	
		public uint? dwCampusID;		/*校区ID*/
	
		public string szCampusName;		/*校区名称*/
	
		public uint? dwCampusKind;		/*校区类型（扩展）*/
	
		public uint? dwVisitTimes;		/*浏览次数*/
	
		public uint? dwUsePurpose;		/*同UniFee的dwPurpose*/
	
		public string szExtInfo;		/*扩展信息*/
	
		public uint? dwURLCtrl;		/*上网监控模式*/
	
		public uint? dwURLCtrlParam;		/*上网监控设定值（根据不同的监控模式含义不一样)*/
	
		public uint? dwURLEndTime;		/*终止时间*/
	
		public string szURLCtrlName;		/*上网监控名称*/
	
		public uint? dwSWCtrl;		/*软件监控模式*/
	
		public uint? dwSWCtrlParam;		/*软件监控设定值（根据不同的监控模式含义不一样)*/
	
		public uint? dwSWEndTime;		/*监控结束时间*/
	
		public string szSWCtrlName;		/*软件监控名称*/
	
		public uint? dwAttendantID;		/*值班员账号*/
	
		public string szAttendantName;		/*值班员姓名*/
	
		public string szAttendantTel;		/*值班员电话*/
	
	public DEVRESV[] DevResv;		/*CUniTable[DEVRESV](dwReqProp含DEVREQ_NEEDDEVRESV时才返回)*/
	
	public DEVUSEINFO[] DevUse;		/*CUniTable[DEVUSEINFO](dwReqProp含DEVREQ_NEEDDEVUSE时才返回)*/
	
	public SAMPLEINFO[] DevSample;		/*CUniTable[SAMPLEINFO](dwReqProp含DEVREQ_NEEDSAMPLE时才返回)*/
		};

	/*获取设备配置表请求*/
	
	public struct DEVCFGREQ
	{
		private Reserved reserved;
		
		public uint? dwMainDevID;		/*主设备ID*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*设备配置表*/
	
	public struct DEVCFG
	{
		private Reserved reserved;
		
		public uint? dwMainDevID;		/*主设备ID*/
	
		public uint? dwSubDevType;		/*辅助设备类别*/
	
		[FlagsAttribute]
		public enum DWSUBDEVTYPE : uint
		{
			
				[EnumDescription("标置设备（固定配置）")]
				SUBDEVTYPE_STANDARD = 0x1,
			
				[EnumDescription("可选设备（预约前需申请，由管理人员事前准备）")]
				SUBDEVTYPE_SELECTABLE = 0x2,
			
		}

	
		public uint? dwDevID;		/*设备ID*/
	
		public uint? dwDevSN;		/*设备编号*/
	
		public string szAssertSN;		/*用户方资产编号*/
	
		public string szOriginSN;		/*原厂系列号*/
	
		public string szDevName;		/*实验设备名称*/
	
		public uint? dwUnitPrice;		/*设备单价*/
	
		public uint? dwPurchaseDate;		/*采购日期 YYYYMMDD*/
	
		public uint? dwDevStat;		/*设备状态*/
	
		public uint? dwClassID;		/*设备功能类别*/
	
		public string szClassSN;		/*设备分类号*/
	
		public string szClassName;		/*设备类别名称*/
	
		public uint? dwClassKind;		/*类别(见UNIDEVCLS的Kind定义)*/
	
		public uint? dwKindID;		/*设备类型*/
	
		public string szKindName;		/*设备名称*/
	
		public string szFuncCode;		/*设备功能用途编码*/
	
		public string szModel;		/*设备型号*/
	
		public string szSpecification;		/*设备规格*/
	
		public string szDevKindURL;		/*设备类型详细介绍的URL地址*/
	
		public string szDevURL;		/*设备详细介绍的URL地址*/
	
		public string szMemo;		/*说明信息*/
		};

	/*智能检测座位状态*/
	
	public struct SEATDETECTSTAT
	{
		private Reserved reserved;
		
		public string szFloorNo;		/*所在楼层*/
	
		public string szRoomNo;		/*房间号*/
	
		public uint? dwDevSN;		/*座位编号*/
	
		public string szTagID;		/*RFID标签ID*/
	
		public uint? dwMonitorStat;		/*监控状态(dwRunStat 的智能有人定义）*/
	
		public uint? dwChangeTime;		/*状态改变时间*/
	
		public string szMemo;		/*说明信息*/
		};

	/*管理员人工管理设备使用*/
	
	public struct DEVMANUSE
	{
		private Reserved reserved;
		
		public uint? dwLabID;		/*实验室ID*/
	
		public uint? dwDevID;		/*设备ID*/
	
		public uint? dwResvID;		/*预约ID*/
	
		public uint? dwMode;		/*操作模式*/
	
		[FlagsAttribute]
		public enum DWMODE : uint
		{
			
				[EnumDescription("开始使用)")]
				MANMODE_STARTUSE = 0x1,
			
				[EnumDescription("停止使用")]
				MANMODE_STOPUSE = 0x2,
			
				[EnumDescription("暂时离开")]
				MANMODE_LEAVE = 0x4,
			
		}

	
		public string szExtInfo;		/*扩展信息*/
	
		public string szMemo;		/*说明信息*/
		};

	/*获取设备配置类别表请求*/
	
	public struct DEVCFGKINDREQ
	{
		private Reserved reserved;
		
		public uint? dwMainDevID;		/*主设备ID*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*设备配置类别表*/
	
	public struct DEVCFGKIND
	{
		private Reserved reserved;
		
		public uint? dwMainDevID;		/*主设备ID*/
	
		public uint? dwSubDevType;		/*辅助设备类别*/
	
		public uint? dwSubDevNum;		/*辅助设备数量*/
	
		public uint? dwKindID;		/*设备类型*/
	
		public string szKindName;		/*设备名称*/
	
		public string szFuncCode;		/*设备功能用途编码*/
	
		public string szModel;		/*设备型号*/
	
		public string szSpecification;		/*设备规格*/
	
		public string szDevKindURL;		/*设备类型详细介绍的URL地址*/
	
		public uint? dwClassID;		/*设备功能类别*/
	
		public string szClassSN;		/*设备分类号*/
	
		public string szClassName;		/*设备类别名称*/
	
		public uint? dwClassKind;		/*类别(见UNIDEVCLS的Kind定义)*/
	
		public string szMemo;		/*说明信息*/
		};

	/*设置设备值班员*/
	
	public struct DEVATTENDANT
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*设备ID*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public uint? dwAttendantID;		/*值班员账号*/
	
		public string szAttendantName;		/*值班员姓名*/
	
		public string szAttendantTel;		/*值班员电话*/
		};

	/*设备使用费的经费分配比例请求*/
	
	public struct DEVFARREQ
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*设备ID*/
		};

	/*设备使用费的经费分配比例*/
	
	public struct DEVFAR
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*设备ID*/
	
		public uint? dwFeeType;		/*收费类别(FEEDETAIL定义)*/
	
		public uint? dwTestRate;		/*分析测试费比例*/
	
		public uint? dwOpenFundRate;		/*开放基金比例*/
	
		public uint? dwServiceRate;		/*劳务费比例*/
	
		public string szMemo;		/*备注*/
		};

	/*获取预约设备列表*/
	
	public struct DEVFORRESVREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*方式*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("全部设备")]
				DEVFORRESV_ALL = 0,
			
				[EnumDescription("设备ID")]
				DEVFORRESV_DEVID = 1,
			
				[EnumDescription("名称")]
				DEVFORRESV_BYNAME = 7,
			
				[EnumDescription("实验室ID")]
				DEVFORRESV_LABID = 0x101,
			
				[EnumDescription("房间ID")]
				DEVFORRESV_ROOMID = 0x102,
			
		}

	
		public string szKey;		/*查询主条件*/
	
		public string szSubKey;		/*查询辅条件，DEVFORRESV_DEVID时是LabID*/
	
		public uint? dwKindID;		/*所属类型*/
	
		public uint? dwClassID;		/*所属功能类别ID*/
	
		public uint? dwResvPurpose;		/*预约用途*/
	
		public uint? dwBeginTime;		/*开始时间*/
	
		public uint? dwEndTime;		/*结束时间*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*设备信息*/
	
	public struct DEVFORRESV
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*设备ID*/
	
		public uint? dwDevSN;		/*设备编号*/
	
		public string szDevName;		/*实验设备名称*/
	
		public uint? dwClassID;		/*设备功能类别*/
	
		public string szClassName;		/*设备类别名称*/
	
		public uint? dwKindID;		/*设备类型*/
	
		public string szKindName;		/*设备类型名称*/
	
		public uint? dwMaxUsers;		/*最多同时使用人数*/
	
		public uint? dwMinUsers;		/*最少同时使用人数*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabSN;		/*实验室编号*/
	
		public string szLabName;		/*实验室名称*/
	
		public uint? dwRoomID;		/*房间ID*/
	
		public string szRoomNo;		/*房间号（不重复,用于关联门禁，格式B-F-N,建筑编号-楼层号-房间编号）*/
	
		public string szRoomName;		/*房间名称*/
	
		public string szDevKindURL;		/*设备详细介绍的URL地址*/
	
		public string szDevURL;		/*设备URL地址*/
	
		public string szExtInfo;		/*扩展信息*/
	
		public uint? dwResvRate;		/*预约率(1-100)*/
	
		public uint? dwResvRuleSN;		/*关联预约规则*/
	
		public uint? dwOpenRuleSN;		/*关联开放时间表*/
	
		public uint? dwFeeSN;		/*关联收费标准*/
	
		public string szMemo;		/*说明信息*/
		};

	/*查询设备预约状态请求*/
	
	public struct DEVRESVSTATREQ
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*设备ID*/
	
		public string szDevName;		/*实验设备名称*/
	
		public string szLabIDs;		/*实验室ID,多个用逗号隔开*/
	
		public string szRoomIDs;		/*房间ID,多个用逗号隔开*/
	
		public string szKindIDs;		/*所属类型,多个用逗号隔开*/
	
		public string szClassIDs;		/*所属功能类别ID,多个用逗号隔开*/
	
		public string szDeptIDs;		/*学院ID,多个用逗号隔开*/
	
		public string szBuildingIDs;		/*楼宇ID,多个用逗号隔开*/
	
		public string szCampusIDs;		/*校区ID,多个用逗号隔开*/
	
		public uint? dwResvPurpose;		/*预约用途*/
	
		public uint? dwProperty;		/*设备属性(参看UNIDEVKIND定义)*/
	
		public string szDates;		/*查询日期,多个用逗号隔开*/
	
		public uint? dwClassKind;		/*类别(见UNIDEVCLS的Kind定义)*/
	
		public uint? dwResvUsers;		/*预约人数*/
	
		public uint? dwExtRelatedID;		/*扩展关联ID*/
	
		public uint? dwReqProp;		/*请求附加属性*/
	
		[FlagsAttribute]
		public enum DWREQPROP : uint
		{
			
				[EnumDescription("需要每天开放规则")]
				DRREQ_NEEDALLDAYOPENRULE = 0x1,
			
				[EnumDescription("查看所有设备")]
				DRREQ_VIEWALL = 0x2,
			
				[EnumDescription("需获取共享设备预约信息")]
				DRREQ_NEEDTHIRDSHAREDEV = 0x4,
			
		}

	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*设备预约信息*/
	
	public struct DEVRESVTIME
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*预约ID*/
	
		public uint? dwPurpose;		/*预约用途*/
	
		public uint? dwStatus;		/*预约状态*/
	
		public uint? dwPreDate;		/*预约开始日期*/
	
		public uint? dwBegin;		/*开始时间(HHMM或YYYYMMDD)*/
	
		public uint? dwEnd;		/*结束时间(HHMM或YYYYMMDD)*/
	
		public uint? dwOwner;		/*预约人(所有者)*/
	
		public string szOwnerName;		/*预约人姓名*/
	
		public string szMemberName;		/*成员名称*/
	
		public string szTestName;		/*实验名称*/
	
		public uint? dwSex;		/*预约人性别*/
		};

	/*设备预约状态*/
	
	public struct DEVRESVSTAT
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*设备ID*/
	
		public uint? dwDevSN;		/*设备编号*/
	
		public string szDevName;		/*实验设备名称*/
	
		public uint? dwDevStat;		/*设备状态*/
	
		public uint? dwRunStat;		/*运行状态*/
	
		public uint? dwClassID;		/*设备功能类别*/
	
		public string szClassName;		/*设备类别名称*/
	
		public uint? dwKindID;		/*设备类型*/
	
		public string szKindName;		/*设备类型名称*/
	
		public uint? dwMaxUsers;		/*最多同时使用人数*/
	
		public uint? dwMinUsers;		/*最少同时使用人数*/
	
		public uint? dwProperty;		/*设备属性(参看UNIDEVKIND定义)*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabSN;		/*实验室编号*/
	
		public string szLabName;		/*实验室名称*/
	
		public uint? dwRoomID;		/*房间ID*/
	
		public string szRoomNo;		/*房间号（不重复,用于关联门禁，格式B-F-N,建筑编号-楼层号-房间编号）*/
	
		public string szRoomName;		/*房间名称*/
	
		public string szFloorNo;		/*所在楼层*/
	
		public uint? dwBuildingID;		/*楼宇ID*/
	
		public string szBuildingNo;		/*大楼编号(*/
	
		public string szBuildingName;		/*大楼名称(*/
	
		public uint? dwDeptID;		/*部门ID*/
	
		public string szDeptName;		/*部门*/
	
		public uint? dwCampusID;		/*校区ID*/
	
		public string szCampusName;		/*校区名称*/
	
		public uint? dwCampusKind;		/*校区类型（扩展）*/
	
		public string szDevKindURL;		/*设备详细介绍的URL地址*/
	
		public string szDevURL;		/*设备URL地址*/
	
		public string szExtInfo;		/*扩展信息*/
	
		public uint? dwOpenLimit;		/*开放限制见GROUPOPENRULE定义+下面定义*/
	
		[FlagsAttribute]
		public enum DWOPENLIMIT : uint
		{
			
				[EnumDescription("所选时间不能预约")]
				OPENLIMIT_NORESV = 0x1000,
			
		}

	
	public UNIRESVRULE szRuleInfo;		/*CUniStruct[UNIRESVRULE]*/
	
	public DAYOPENRULE[] szOpenInfo;		/*CUniTable[DAYOPENRULE]*/
	
	public DEVRESVTIME[] szResvInfo;		/*CUniTable[DEVRESVTIME]*/
		};

	/*查询设备长期预约状态请求*/
	
	public struct DEVLONGRESVSTATREQ
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*设备ID*/
	
		public string szDevName;		/*实验设备名称*/
	
		public string szLabIDs;		/*实验室ID,多个用逗号隔开*/
	
		public string szRoomIDs;		/*房间ID,多个用逗号隔开*/
	
		public string szKindIDs;		/*所属类型,多个用逗号隔开*/
	
		public string szClassIDs;		/*所属功能类别ID,多个用逗号隔开*/
	
		public string szDeptIDs;		/*学院ID,多个用逗号隔开*/
	
		public string szBuildingIDs;		/*楼宇ID,多个用逗号隔开*/
	
		public string szCampusIDs;		/*校区ID,多个用逗号隔开*/
	
		public uint? dwResvPurpose;		/*预约用途*/
	
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*开始日期*/
	
		public uint? dwClassKind;		/*类别(见UNIDEVCLS的Kind定义)*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*设备长期预约状态*/
	
	public struct DEVLONGRESVSTAT
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*设备ID*/
	
		public uint? dwDevSN;		/*设备编号*/
	
		public string szDevName;		/*实验设备名称*/
	
		public uint? dwClassID;		/*设备功能类别*/
	
		public string szClassName;		/*设备类别名称*/
	
		public uint? dwKindID;		/*设备类型*/
	
		public string szKindName;		/*设备类型名称*/
	
		public uint? dwMaxUsers;		/*最多同时使用人数*/
	
		public uint? dwMinUsers;		/*最少同时使用人数*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabSN;		/*实验室编号*/
	
		public string szLabName;		/*实验室名称*/
	
		public uint? dwRoomID;		/*房间ID*/
	
		public string szRoomNo;		/*房间号（不重复,用于关联门禁，格式B-F-N,建筑编号-楼层号-房间编号）*/
	
		public string szRoomName;		/*房间名称*/
	
		public string szFloorNo;		/*所在楼层*/
	
		public uint? dwBuildingID;		/*楼宇ID*/
	
		public string szBuildingNo;		/*大楼编号(*/
	
		public string szBuildingName;		/*大楼名称(*/
	
		public uint? dwDeptID;		/*部门ID*/
	
		public string szDeptName;		/*部门*/
	
		public uint? dwCampusID;		/*校区ID*/
	
		public string szCampusName;		/*校区名称*/
	
		public uint? dwCampusKind;		/*校区类型（扩展）*/
	
		public string szDevKindURL;		/*设备详细介绍的URL地址*/
	
		public string szDevURL;		/*设备URL地址*/
	
		public string szExtInfo;		/*扩展信息*/
	
	public UNIRESVRULE szRuleInfo;		/*CUniStruct[UNIRESVRULE]*/
	
	public DEVRESVTIME[] szResvInfo;		/*CUniTable[DEVRESVTIME]*/
		};

	/*查询实验室预约状态请求*/
	
	public struct LABRESVSTATREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*方式*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("全部实验室")]
				LABRESVSTAT_ALL = 0,
			
				[EnumDescription("实验室ID")]
				LABRESVSTAT_LABID = 1,
			
		}

	
		public string szGetKey;		/*查询主条件*/
	
		public uint? dwDate;		/*查询日期*/
		};

	/*教学预约详细信息*/
	
	public struct TEACHINGRESVINFO
	{
		private Reserved reserved;
		
		public uint? dwTestItemID;		/*实验项目ID*/
	
		public uint? dwTestCardID;		/*实验项目卡ID*/
	
		public string szTestName;		/*实验名称*/
	
		public uint? dwTestPlanID;		/*实验计划ID*/
	
		public string szTestPlanName;		/*实验计划名称*/
	
		public uint? dwTeacherID;		/*教师（帐号）*/
	
		public string szTeacherName;		/*教师姓名*/
	
		public uint? dwCourseID;		/*课程ID*/
	
		public string szCourseName;		/*课程名称*/
	
		public uint? dwGroupID;		/*上课班级*/
	
		public string szGroupName;		/*上课班级名称*/
	
		public uint? dwTeachingTime;		/*教学时间(格式见UNIRESERVE)*/
	
		public uint? dwResvStat;		/*预约状态(格式见UNIRESERVE)*/
		};

	/*实验室预约状态*/
	
	public struct LABRESVSTAT
	{
		private Reserved reserved;
		
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabSN;		/*实验室编号*/
	
		public string szLabName;		/*实验室名称*/
	
		public uint? dwDevNum;		/*设备数*/
	
	public TEACHINGRESVINFO[] szResvInfo;		/*CUniTable[TEACHINGRESVINFO]*/
	
		public string szMemo;		/*说明信息*/
		};

	/*查询房间预约状态请求*/
	
	public struct ROOMRESVSTATREQ
	{
		private Reserved reserved;
		
		public string szRoomName;		/*房间名称*/
	
		public string szLabIDs;		/*实验室ID,多个用逗号隔开*/
	
		public string szRoomIDs;		/*房间ID,多个用逗号隔开*/
	
		public string szKindIDs;		/*所属类型,多个用逗号隔开*/
	
		public uint? dwRoomProp;		/*房间属性*/
	
		public uint? dwUnNeedRoomProp;		/*不包含房间属性*/
	
		public uint? dwMinDevNum;		/*最少设备数*/
	
		public uint? dwMaxDevNum;		/*最多设备数*/
	
		public uint? dwDate;		/*查询日期*/
		};

	/*房间预约状态*/
	
	public struct ROOMRESVSTAT
	{
		private Reserved reserved;
		
		public uint? dwRoomID;		/*房间ID*/
	
		public string szRoomNo;		/*房间编号*/
	
		public string szRoomName;		/*房间名称*/
	
		public uint? dwRoomProp;		/*房间属性*/
	
		public uint? dwDevNum;		/*设备数*/
	
	public TEACHINGRESVINFO[] szResvInfo;		/*CUniTable[TEACHINGRESVINFO]*/
	
		public string szMemo;		/*说明信息*/
		};

	/*查询房间组合预约状态请求*/
	
	public struct RGRESVSTATREQ
	{
		private Reserved reserved;
		
		public uint? dwMinDevNum;		/*最少设备数*/
	
		public uint? dwMaxDevNum;		/*最多设备数*/
	
		public uint? dwDate;		/*查询日期*/
		};

	/*房间组合预约状态*/
	
	public struct RGRESVSTAT
	{
		private Reserved reserved;
		
		public uint? dwRGID;		/*房间ID*/
	
		public string szRGName;		/*房间组合名称*/
	
		public uint? dwRoomNum;		/*组合房间数*/
	
		public uint? dwDevNum;		/*设备数*/
	
	public TEACHINGRESVINFO[] szResvInfo;		/*CUniTable[TEACHINGRESVINFO]*/
	
		public string szMemo;		/*说明信息*/
		};

	/*获取预约设备列表(按类型)*/
	
	public struct DEVKINDFORRESVREQ
	{
		private Reserved reserved;
		
		public string szKindName;		/*实验设备名称*/
	
		public string szLabIDs;		/*实验室ID,多个用逗号隔开*/
	
		public string szRoomIDs;		/*房间ID,多个用逗号隔开*/
	
		public string szKindIDs;		/*所属类型,多个用逗号隔开*/
	
		public string szClassIDs;		/*所属功能类别ID,多个用逗号隔开*/
	
		public string szDeptIDs;		/*学院ID,多个用逗号隔开*/
	
		public string szBuildingIDs;		/*楼宇ID,多个用逗号隔开*/
	
		public string szCampusIDs;		/*校区ID,多个用逗号隔开*/
	
		public uint? dwClassKind;		/*类别(见UNIDEVCLS的Kind定义)*/
	
		public uint? dwResvPurpose;		/*预约用途*/
	
		public uint? dwProperty;		/*设备属性(参看UNIDEVKIND定义)*/
	
		public uint? dwDate;		/*查询日期*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*获取长期预约设备列表(按类型)*/
	
	public struct DEVKINDFORLONGRESVREQ
	{
		private Reserved reserved;
		
		public string szKindName;		/*实验设备名称*/
	
		public string szLabIDs;		/*实验室ID,多个用逗号隔开*/
	
		public string szRoomIDs;		/*房间ID,多个用逗号隔开*/
	
		public string szKindIDs;		/*所属类型,多个用逗号隔开*/
	
		public string szClassIDs;		/*所属功能类别ID,多个用逗号隔开*/
	
		public string szDeptIDs;		/*学院ID,多个用逗号隔开*/
	
		public string szBuildingIDs;		/*楼宇ID,多个用逗号隔开*/
	
		public string szCampusIDs;		/*校区ID,多个用逗号隔开*/
	
		public uint? dwClassKind;		/*类别(见UNIDEVCLS的Kind定义)*/
	
		public uint? dwResvPurpose;		/*预约用途*/
	
		public uint? dwProperty;		/*设备属性(参看UNIDEVKIND定义)*/
	
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*开始日期*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*设备信息*/
	
	public struct DEVKINDFORRESV
	{
		private Reserved reserved;
		
		public uint? dwKindID;		/*设备类型*/
	
		public string szKindName;		/*实验设备名称*/
	
		public uint? dwClassID;		/*设备功能类别*/
	
		public string szClassName;		/*设备类别名称*/
	
		public string szFuncCode;		/*设备功能用途编码*/
	
		public string szModel;		/*设备型号*/
	
		public string szSpecification;		/*设备规格*/
	
		public uint? dwProperty;		/*设备属性*/
	
		public uint? dwMaxUsers;		/*最多同时使用人数*/
	
		public uint? dwMinUsers;		/*最少同时使用人数*/
	
		public uint? dwTotalNum;		/*设备总数*/
	
		public string szOperaCert;		/*操作证书*/
	
		public string szDevKindURL;		/*设备详细介绍的URL地址*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabSN;		/*实验室编号*/
	
		public string szLabName;		/*实验室名称*/
	
		public uint? dwRoomID;		/*房间ID*/
	
		public string szRoomNo;		/*房间号（不重复,用于关联门禁，格式B-F-N,建筑编号-楼层号-房间编号）*/
	
		public string szRoomName;		/*房间名称*/
	
		public string szFloorNo;		/*所在楼层*/
	
		public uint? dwBuildingID;		/*楼宇ID*/
	
		public string szBuildingNo;		/*大楼编号(*/
	
		public string szBuildingName;		/*大楼名称(*/
	
		public uint? dwDeptID;		/*部门ID*/
	
		public string szDeptName;		/*部门*/
	
		public uint? dwCampusID;		/*校区ID*/
	
		public string szCampusName;		/*校区名称*/
	
		public uint? dwCampusKind;		/*校区类型（扩展）*/
	
		public string szUsableNumArray;		/* 短期预约每字节代表0-24点对应的分钟的可用设备数(长度1440),
            长期预约表示每天的可用设备，长度为查询天数。=A表示大于9台可用设备,U表示不开放 */
	
		public uint? dwOpenLimit;		/*开放限制见GROUPOPENRULE定义+DEVRESVSTAT定义*/
	
	public UNIRESVRULE szRuleInfo;		/*CUniStruct[UNIRESVRULE]*/
	
	public DAYOPENRULE[] szOpenInfo;		/*CUniTable[DAYOPENRULE]*/
	
	public UNIFEE szFeeInfo;		/*CUniStruct[UNIFEE]*/
	
		public string szMemo;		/*说明信息*/
		};

	/*获取预约设备列表(按房间)*/
	
	public struct ROOMFORRESVREQ
	{
		private Reserved reserved;
		
		public string szRoomName;		/*实验设备名称*/
	
		public string szFloorNo;		/*所在楼层*/
	
		public string szRoomIDs;		/*房间ID,多个用逗号隔开*/
	
		public uint? dwKindID;		/*所属类型*/
	
		public string szBuildingIDs;		/*楼宇ID,多个用逗号隔开*/
	
		public string szCampusIDs;		/*校区ID,多个用逗号隔开*/
	
		public uint? dwClassKind;		/*类别(见UNIDEVCLS的Kind定义)*/
	
		public uint? dwResvPurpose;		/*预约用途*/
	
		public uint? dwDate;		/*查询日期*/
	
		public uint? dwBeginTime;		/*预约开始时间*/
	
		public uint? dwEndTime;		/*预约结束时间*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*房间预约信息*/
	
	public struct ROOMFORRESV
	{
		private Reserved reserved;
		
		public uint? dwRoomID;		/*房间ID*/
	
		public string szRoomName;		/*房间名称*/
	
		public string szRoomNo;		/*房间号*/
	
		public string szFloorNo;		/*所在楼层*/
	
		public uint? dwOpenBegin;		/*开始时间(HHMM)*/
	
		public uint? dwOpenEnd;		/*结束时间(HHMM)*/
	
		public uint? dwTotalNum;		/*设备总数*/
	
		public uint? dwUsableNum;		/*可用设备数*/
	
		public string szMemo;		/*说明信息*/
		};

	/*获取预约可用设备请求包*/
	
	public struct RESVUSABLEDEVREQ
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*预约ID*/
		};

	/*租借设备明细*/
	
	public struct RESVUSABLEDEV
	{
		private Reserved reserved;
		
		public uint? dwLabID;		/*实验室ID*/
	
		public uint? dwDevID;		/*设备ID*/
	
		public uint? dwDevSN;		/*设备SN*/
	
		public uint? dwDevKind;		/*设备类型*/
	
		public string szDevName;		/*设备名*/
	
		public string szKindName;		/*设备类别名*/
	
		public uint? dwRoomID;		/*房间ID*/
	
		public string szRoomNo;		/*房间编号*/
	
		public string szRoomName;		/*房间名称*/
	
		public string szExtInfo;		/*扩展信息*/
		};

	/*客户端注册到服务器*/
	
	public struct DEVREGISTREQ
	{
		private Reserved reserved;
		
		public uint? dwStaSN;		/*站点编号*/
	
		public string szCltVersion;		/*客户端版本*/
	
		public string szPCName;		/*机器名称*/
	
		public string szIP;		/*IP地址*/
	
		public string szMAC;		/*网卡地址*/
	
	public DEVICECONFIG[] szCfgInfo;		/*配置信息CUniTable[DEVICECONFIG]*/
		};

	/*服务器对客户端注册的响应*/
	
	public struct DEVREGISTRES
	{
		private Reserved reserved;
		
		public uint? dwSessionID;		/*服务器分配的SessionID*/
	
	public UNIVERSION SrvVer;		/*UNIVERSION 结构*/
	
		public string szCurTime;		/*服务器时间 YYYY-MM-DD HH:MM:SS*/
	
		public uint? dwDevSN;		/*本设备编号*/
	
		public uint? dwDevID;		/*本设备ＩＤ号*/
	
		public uint? dwLabID;		/*本实验室ＩＤ号*/
	
		public uint? dwFunc;		/*使用功能模块*/
	
		public uint? dwParam;		/*参数配置*/
	
		[FlagsAttribute]
		public enum DWPARAM : uint
		{
			
				[EnumDescription("网络故障时可使用")]
				CLIENTPARA_NETFAULT = 1,
			
				[EnumDescription("注销时自动关机")]
				CLIENTPARA_AUTOSHUTDOWN = 2,
			
				[EnumDescription("支持免费和收费模式选择")]
				CLIENTPARA_MULTIUSEMODE = 4,
			
				[EnumDescription("有云盘")]
				CLIENTPARA_CLOUDDISK = 0x100,
			
				[EnumDescription("禁止客户端修改密码")]
				CLIENTPARA_PSFORBID = 0x1000,
			
		}

	
		public uint? dwDevStat;		/*设备状态*/
	
		public uint? dwRunStat;		/*运行状态*/
	
		public uint? dwPasswdCode;		/*卸载密码种子*/
	
		public string szLabName;		/*实验室名称*/
	
		public string szDisplayInfo;		/*登录界面显示信息*/
	
		public string szCastParam;		/*屏幕广播参数*/
	
	public UNIDEVICE DevInfo;		/*UNIDEVICE 结构*/
		};

	/*客户端登录请求*/
	
	public struct DEVLOGONREQ
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*客户端设备的ID号*/
	
		public uint? dwLabID;		/*实验室的ID号*/
	
		public uint? dwLogonType;		/*登录类别*/
	
		[FlagsAttribute]
		public enum DWLOGONTYPE : uint
		{
			
				[EnumDescription("系统自动登录")]
				LOGONTYPE_SYSTEM = 1,
			
				[EnumDescription("用户登录")]
				LOGONTYPE_USER = 2,
			
				[EnumDescription("收费模式")]
				LOGONTYPE_FEEMODE = 0x100,
			
				[EnumDescription("免费模式")]
				LOGONTYPE_FREEMODE = 0x200,
			
		}

	
		public string szLogonName;		/*登录名*/
	
		public string szPasswd;		/*用户密码*/
	
		public string szMemo;		/*备注（扩展用）*/
		};

	/*客户端登录请求的响应*/
	
	public struct DEVLOGONRES
	{
		private Reserved reserved;
		
	public UNIACCOUNT szAccInfo;		/*使用者信息(UNIACCOUNT结构)*/
	
		public uint? dwPurpose;		/*用途*/
	
		public string szDeclareInfo;		/*进入系统的声明信息*/
	
	public UNIRESERVE ResvInfo;		/*预约信息*/
		};

	/*客户端查询当前信息*/
	
	public struct DEVQUERYREQ
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*客户端设备的ID号*/
	
		public uint? dwLabID;		/*实验室的ID号*/
	
		public uint? dwAccNo;		/*帐号*/
	
		public string szMemo;		/*备注（扩展用）*/
		};

	/*客户端注销时用户选择的计费信息*/
	
	public struct USERFEECHECK
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*预约号*/
	
		public uint? dwAccNo;		/*用户账号*/
	
		public uint? dwFeeMode;		/*收费方式(定义在UNIRESVRULE)*/
	
	public RESVSAMPLE[] ResvSample;		/*CUniTable[RESVSAMPLE]*/
	
	public UNIACCTINFO FeeInfo;		/*CUniStruct[UNIACCTINFO]*/
	
		public string szMemo;		/*备注（扩展用）*/
		};

	/*客户端注销请求*/
	
	public struct DEVLOGOUTREQ
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*客户端设备的ID号*/
	
		public uint? dwLabID;		/*实验室的ID号*/
	
		public uint? dwAccNo;		/*帐号*/
	
		public uint? dwParam;		/*退出请求参数*/
	
		[FlagsAttribute]
		public enum DWPARAM : uint
		{
			
				[EnumDescription("机器将关闭")]
				LOGOUTPARAM_SHUTDOWN = 1,
			
				[EnumDescription("使用结束")]
				LOGOUTPARAM_USEEND = 2,
			
				[EnumDescription("重启电脑")]
				LOGOUTPARAM_RESTART = 4,
			
		}

	
	public byte[] FeeCheck;		/*费用信息(只有支持 注销时选择收费模式的设备有效)*/
		};

	/*客户端注销请求*/
	
	public struct DEVLOGOUTRES
	{
		private Reserved reserved;
		
	public UNIACCOUNT szAccInfo;		/*使用者信息(UNIACCOUNT结构)*/
	
		public uint? dwParam;		/*退出参数*/
	
		[FlagsAttribute]
		public enum DWPARAM : uint
		{
			
				[EnumDescription("隐藏使用信息")]
				LOGOUTPARAM_HIDEUSEINFO = 0x1,
			
		}

	
		public string szMemo;		/*备注（扩展用）*/
		};

	/*客户端与服务器定时握手请求*/
	
	public struct DEVHANDSHAKEREQ
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*客户端设备的ID号*/
	
		public uint? dwLabID;		/*实验室的ID号*/
	
		public uint? dwAccNo;		/*帐号*/
	
		public string szDevChgInfo;		/*设备软硬件变更信息*/
	
		public string szMemo;		/*备注（扩展用）*/
		};

	/*服务器对客户端定时握手的响应*/
	
	public struct DEVHANDSHAKERES
	{
		private Reserved reserved;
		
		public uint? dwFunc;		/*使用功能模块*/
	
		public uint? dwDevStat;		/*设备状态*/
	
		public uint? dwRunStat;		/*运行状态*/
	
		public uint? dwUndoCmd;		/*未处理的命令*/
	
		public uint? dwLockWaitTime;		/*等待锁定客户端时间*/
	
		public string szMemo;		/*备注*/
		};

	/*客户端与服务器定时握手请求*/
	
	public struct CLTCHGPWINFO
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*客户端设备的ID号*/
	
		public uint? dwLabID;		/*实验室的ID号*/
	
		public uint? dwAccNo;		/*帐号*/
	
		public string szOldPw;		/*旧密码*/
	
		public string szNewPw;		/*新密码*/
	
		public string szMemo;		/*备注（扩展用）*/
		};

	/*控制设备命令*/
	
	public struct DEVCTRLINFO
	{
		private Reserved reserved;
		
		public uint? dwLabID;		/*实验室ID号*/
	
		public uint? dwDevID;		/*客户端设备的ID号*/
	
		public uint? dwCmd;		/*控制命令*/
	
		[FlagsAttribute]
		public enum DWCMD : uint
		{
			
				[EnumDescription("开启电源")]
				DEVCMD_POWERON = 1,
			
				[EnumDescription("关闭电源")]
				DEVCMD_POWEROFF = 2,
			
				[EnumDescription("截屏")]
				DEVCMD_CAPTURESCREEN = 3,
			
				[EnumDescription("远程唤醒")]
				DEVCMD_WAKEUP = 11,
			
				[EnumDescription("远程关机")]
				DEVCMD_SHUTDOWN = 12,
			
				[EnumDescription("远程重启")]
				DEVCMD_RESTART = 13,
			
				[EnumDescription("远程关机")]
				DEVCMD_SYSSHUTDOWN = 14,
			
				[EnumDescription("管理员强制登录")]
				DEVCMD_LOGON = 21,
			
				[EnumDescription("管理员强制注销")]
				DEVCMD_LOGOUT = 22,
			
				[EnumDescription("管理员强制卸载客户端")]
				DEVCMD_UNINSTALL = 23,
			
				[EnumDescription("系统强制注销")]
				DEVCMD_SYSLOGOUT = 24,
			
				[EnumDescription("禁用")]
				DEVCMD_DISABLE = 31,
			
				[EnumDescription("解禁")]
				DEVCMD_EANBLE = 32,
			
				[EnumDescription("锁定")]
				DEVCMD_PCLOCK = 41,
			
				[EnumDescription("解锁")]
				DEVCMD_PCUNLOCK = 42,
			
				[EnumDescription("光驱锁定")]
				DEVCMD_CDLOCK = 43,
			
				[EnumDescription("光驱解锁")]
				DEVCMD_CDUNLOCK = 44,
			
				[EnumDescription("U盘锁定")]
				DEVCMD_USBLOCK = 45,
			
				[EnumDescription("U盘解锁")]
				DEVCMD_USBUNLOCK = 46,
			
				[EnumDescription("需要用户登录")]
				DEVCMD_NEEDLOGON = 51,
			
				[EnumDescription("无需用户登录")]
				DEVCMD_NOLOGON = 52,
			
				[EnumDescription("门禁开门")]
				DEVCMD_DOOROPEN = 61,
			
				[EnumDescription("门禁报警，持续时间放在控制参数里")]
				DEVCMD_DOORALARM = 62,
			
				[EnumDescription("门禁语音提醒消息")]
				DEVCMD_DOORSOUNDMSG = 63,
			
				[EnumDescription("预约开始 szParam为RESVSTARTINFO")]
				DEVCMD_RESVSTART = 71,
			
				[EnumDescription("预约结束 szParam为RESVENDINFO")]
				DEVCMD_RESVEND = 72,
			
				[EnumDescription("系统通知预约结束消息")]
				DEVCMD_RESVENDMSG = 81,
			
				[EnumDescription("管理员消息")]
				DEVCMD_ADMINMSG = 82,
			
				[EnumDescription("远程强制退出程序 szParam为QUITAPPINFO")]
				DEVCMD_QUITAPP = 83,
			
				[EnumDescription("系统消息")]
				DEVCMD_SYSTEMMSG = 84,
			
				[EnumDescription("开始屏幕广播,szParam为广播机IP")]
				DEVCMD_SETTELECAST = 91,
			
				[EnumDescription("结束屏幕广播")]
				DEVCMD_UNSETTELECAST = 92,
			
				[EnumDescription("开始演示")]
				DEVCMD_SETDEMO = 93,
			
				[EnumDescription("结束演示")]
				DEVCMD_UNSETDEMO = 94,
			
				[EnumDescription("开始控制")]
				DEVCMD_SETCTRL = 95,
			
				[EnumDescription("结束控制")]
				DEVCMD_UNSETCTRL = 96,
			
		}

	
		public string szParam;		/*控制参数*/
	
		public string szMemo;		/*备注*/
		};

	/*控制房间命令*/
	
	public struct ROOMCTRLINFO
	{
		private Reserved reserved;
		
		public uint? dwCtrlSN;		/*控制器编号*/
	
		public uint? dwRoomID;		/*房间ID号*/
	
		public uint? dwCmd;		/*控制命令,参考DEVCTRLINFO定义*/
	
		public string szParam;		/*控制参数*/
	
		public string szMemo;		/*备注*/
		};

	/*用户可进入房间请求*/
	
	public struct PERMITROOMREQ
	{
		private Reserved reserved;
		
		public uint? dwAccNo;		/*帐号*/
		};

	/*用户可进入房间信息*/
	
	public struct PERMITROOMINFO
	{
		private Reserved reserved;
		
		public uint? dwRoomKind;		/*房间类型*/
	
		[FlagsAttribute]
		public enum DWROOMKIND : uint
		{
			
				[EnumDescription("房间")]
				ROOMKIND_ROOM = 1,
			
				[EnumDescription("通道")]
				ROOMKIND_CHANNELGATE = 2,
			
		}

	
		public uint? dwPermitMode;		/*允许模式*/
	
		[FlagsAttribute]
		public enum DWPERMITMODE : uint
		{
			
				[EnumDescription("管理员")]
				ROOMPERMIT_MANAGER = 1,
			
				[EnumDescription("使用允许")]
				ROOMPERMIT_USEDEV = 2,
			
				[EnumDescription("其他")]
				ROOMPERMIT_OTHER = 4,
			
		}

	
		public uint? dwRoomID;		/*房间ID*/
	
		public string szRoomNo;		/*房间编号*/
	
		public string szRoomName;		/*房间名称*/
		};

	/*获取房间控制信息请求包*/
	
	public struct ROOMCTRLREQ
	{
		private Reserved reserved;
		
		public string szRoomNo;		/*房间号*/
	
		public uint? dwDCSKind;		/*门禁集控器类型*/
		};

	/*控制设备命令*/
	
	public struct CTRLREQ
	{
		private Reserved reserved;
		
		public uint? dwLabID;		/*实验室ID号*/
	
		public uint? dwDevID;		/*客户端设备的ID号(为空表明设置所有实验室)*/
	
		public uint? dwCtrl;		/*当前监控模式*/
	
		public uint? dwCtrlParam;		/*监控设定值（根据不同的监控模式含义不一样）*/
	
		public uint? dwEndTime;		/*终止时间*/
	
		public string szCtrlName;		/*监控名*/
		};

	/*租借设备明细*/
	
	public struct LOANDEV
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*设备ID*/
	
		public uint? dwDevSN;		/*设备编号*/
	
		public uint? dwDevKind;		/*设备类型*/
	
		public string szDevName;		/*设备名*/
	
		public string szRoomNo;		/*所在房间*/
	
		public uint? dwDevClsKind;		/*设备类别分类*/
		};

	/*获取运行程序请求*/
	
	public struct RUNAPPREQ
	{
		private Reserved reserved;
		
		public uint? dwLabID;		/*实验室ID*/
	
		public uint? dwDevID;		/*设备ID*/
	
		public string szMemo;		/*说明信息*/
		};

	/*运行程序*/
	
	public struct RUNAPP
	{
		private Reserved reserved;
		
		public uint? dwRunNum;		/*当前运行数量合计*/
	
		public uint? dwProcID;		/*程序ID*/
	
		public uint? dwProperty;		/*程序属性*/
	
		public string szProductName;		/*产品名称*/
	
		public string szExeName;		/*Exe文件名*/
	
		public string szSWVersion;		/*程序版本*/
	
		public string szDispProductName;		/*显示程序名称*/
	
		public string szDispSWName;		/*显示产品名称*/
	
		public string szDispSWCompany;		/*显示公司名称*/
	
		public string szInstName;		/*安装名称*/
	
		public string szInstPath;		/*安装路径*/
	
		public string szIcon;		/*图标*/
	
		public string szMemo;		/*说明信息*/
		};

	/*上传机器软件信息结束*/
	
	public struct SWUPLOADEND
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*客户端设备的ID号*/
	
		public uint? dwLabID;		/*实验室的ID号*/
	
		public uint? dwTotalNum;		/*总记录数*/
	
		public uint? dwCollectSecond;		/*收集用时(秒数)*/
	
		public uint? dwUploadSecond;		/*上传用时(秒数)*/
	
		public string szMemo;		/*备注（扩展用）*/
		};

	/*设备外借请求*/
	
	public struct DEVLOANREQ
	{
		private Reserved reserved;
		
		public uint? dwLabID;		/*实验室ID*/
	
		public uint? dwResvID;		/*预约号*/
	
		public uint? dwLender;		/*租借人*/
	
		public string szLenderName;		/*租借人姓名*/
	
		public uint? dwBeginTime;		/*租借开始时间*/
	
		public uint? dwEndTime;		/*租借结束时间*/
	
	public LOANDEV[] szLoanDevs;		/*租借设备明细表(CUniTable<LOANDEV>)*/
	
		public string szMemo;		/*说明信息*/
		};

	/*设备归还请求*/
	
	public struct DEVRETURNREQ
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*预约号*/
	
		public uint? dwLender;		/*租借人*/
	
		public string szLenderName;		/*租借人姓名*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public uint? dwDevID;		/*设备ID*/
	
		public uint? dwCheckStat;		/*设备状态*/
	
		public uint? dwCompensation;		/*赔偿金额*/
	
		public uint? dwPunishScore;		/*信用扣分*/
	
		public string szDamageInfo;		/*损坏说明*/
	
		public string szExtInfo;		/*设备新描述*/
		};

	/**/
	
	public struct DEVDAMAGERECREQ
	{
		private Reserved reserved;
		
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
		public uint? dwSID;		/*SID*/
	
		public uint? dwDevID;		/*设备ID*/
	
		public string szLabIDs;		/*实验室ID,多个用逗号隔开*/
	
		public string szRoomIDs;		/*房间ID,多个用逗号隔开*/
	
		public string szKindIDs;		/*所属类型,多个用逗号隔开*/
	
		public string szClassIDs;		/*所属功能类别ID,多个用逗号隔开*/
	
		public uint? dwProperty;		/*设备属性*/
	
		public uint? dwClassKind;		/*类别(见UNIDEVCLS的Kind定义)*/
	
		public uint? dwUnitPrice;		/*大型仪器价格起点*/
	
		public uint? dwStatus;		/*维修状态*/
	
		public uint? dwManID;		/*经办人ID*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/**/
	
	public struct DEVDAMAGEREC
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*SID*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabName;		/*实验室名称*/
	
		public uint? dwKindID;		/*设备类型*/
	
		public string szKindName;		/*设备类型名*/
	
		public uint? dwDevID;		/*设备ID*/
	
		public uint? dwDevSN;		/*设备编号*/
	
		public string szDevName;		/*设备名称*/
	
		public string szAssertSN;		/*用户方资产编号*/
	
		public uint? dwUnitPrice;		/*设备单价*/
	
		public uint? dwPurchaseDate;		/*采购日期*/
	
		public uint? dwStatus;		/*见UNIBILL定义+如下定义*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("等待维修")]
				REPARE_WAIT = 0x10000,
			
				[EnumDescription("已维修好")]
				REPARE_OK = 0x20000,
			
				[EnumDescription("维修失败，设备报废")]
				REPARE_FAIL = 0x40000,
			
				[EnumDescription("撤销报修")]
				REPARE_CANCEL = 0x80000,
			
		}

	
		public uint? dwDamageDate;		/*损坏日期*/
	
		public uint? dwDamageTime;		/*损坏时间*/
	
		public string szDamageInfo;		/*损坏说明*/
	
		public uint? dwRepareDate;		/*维修日期*/
	
		public uint? dwRepareTime;		/*维修时间*/
	
		public string szRepareInfo;		/*维修说明*/
	
		public uint? dwRepareCost;		/*维修费用*/
	
		public string szFundsNo1;		/*经费卡编号1*/
	
		public uint? dwPay1;		/*经费卡1支付*/
	
		public string szFundsNo2;		/*经费卡编号2*/
	
		public uint? dwPay2;		/*经费卡2支付*/
	
		public string szRepareCom;		/*维修单位*/
	
		public string szRepareComTel;		/*维修单位联系方式*/
	
		public uint? dwManID;		/*经办人ID*/
	
		public string szManName;		/*经办人姓名*/
	
		public uint? dwAccNo;		/*账号*/
	
		public string szTrueName;		/*真实姓名*/
	
		public uint? dwCompensation;		/*赔偿金额*/
	
		public uint? dwPunishScore;		/*处罚分数*/
	
		public string szMemo;		/*说明*/
		};

	/**/
	
	public struct DEVOPENRULEREQ
	{
		private Reserved reserved;
		
		public uint? dwRuleSN;		/*编号*/
	
		public string szRuleName;		/*名称*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*每日开放时间段*/
	
	public struct DAYOPENRULE
	{
		private Reserved reserved;
		
		public uint? dwDate;		/*日期*/
	
		public uint? dwOpenLimit;		/*开放限制见GROUPOPENRULE定义+DEVRESVSTAT定义*/
	
		public uint? dwOpenPurpose;		/*开放用途*/
	
		public uint? dwBegin;		/*开始时间(HHMM)*/
	
		public uint? dwEnd;		/*结束时间(HHMM)*/
	
		public string szFixedTime;		/*预约固定时间点(HHMM)，多个用逗号隔开*/
		};

	/*指定用户组开放时间表*/
	
	public struct PERIODOPENRULEREQ
	{
		private Reserved reserved;
		
		public uint? dwRuleSN;		/*编号*/
	
		public uint? dwGroupID;		/*组ID*/
	
		public uint? dwStartDay;		/*开始日期(周一到周日 用0-6，节假日用8，制定日期用YYYYMMDD)*/
		};

	/*时间期间开放规则*/
	
	public struct PERIODOPENRULE
	{
		private Reserved reserved;
		
		public uint? dwStartDay;		/*开始日期(周一到周日 用0-6，节假日用8，制定日期用YYYYMMDD)*/
	
		public uint? dwEndDay;		/*结束日期(同 dwStartDay定义)*/
	
	public DAYOPENRULE[] DayOpenRule;		/*开放时间段(CUniTable[DAYOPENRULE])*/
	
		public string szMemo;		/*开放说明*/
		};

	/*指定用户组开放时间表*/
	
	public struct GROUPOPENRULEREQ
	{
		private Reserved reserved;
		
		public uint? dwRuleSN;		/*编号*/
	
		public uint? dwGroupID;		/*组ID*/
		};

	/*指定用户组开放时间表*/
	
	public struct GROUPOPENRULE
	{
		private Reserved reserved;
		
	public UNIGROUP szGroup;		/*组信息(CUniStruct[UNIGROUP])*/
	
		public uint? dwPriority;		/*优先级(数字大代表优先级高)*/
	
		public uint? dwOpenLimit;		/*开放限制*/
	
		[FlagsAttribute]
		public enum DWOPENLIMIT : uint
		{
			
				[EnumDescription("使用者只能选开放时间段，不能自由选择时间")]
				OPENLIMIT_FIXEDTIME = 2,
			
				[EnumDescription("每天最多开放时间段")]
				MAX_OPEN_TIMES = 4,
			
				[EnumDescription("一周天数")]
				WEEK_DAYS = 7,
			
				[EnumDescription("节假日在PERIODOPENRULE里的时间表示")]
				HOLIDAY_DAY = 8,
			
		}

	
	public PERIODOPENRULE[] PeriodOpenRule;		/*时间期间开放规则(CUniTable[PERIODOPENRULE])*/
		};

	/*指定用户组开放时间表*/
	
	public struct CHANGEGROUPOPENRULE
	{
		private Reserved reserved;
		
		public uint? dwRuleSN;		/*编号*/
	
		public uint? dwOldGroupID;		/*原组ID*/
	
		public uint? dwGroupID;		/*组ID*/
	
		public uint? dwPriority;		/*优先级(数字大代表优先级高)*/
	
		public uint? dwOpenLimit;		/*开放限制(GROUPOPENRULE)*/
	
	public PERIODOPENRULE[] PeriodOpenRule;		/*时间期间开放规则(CUniTable[PERIODOPENRULE])*/
		};

	/*时间期间开放规则*/
	
	public struct CHANGEPERIODOPENRULE
	{
		private Reserved reserved;
		
		public uint? dwRuleSN;		/*编号*/
	
		public uint? dwGroupID;		/*组ID*/
	
		public uint? dwOldStartDay;		/*原开始日期(周一到周日 用0-6，节假日用8，制定日期用YYYYMMDD)*/
	
		public uint? dwStartDay;		/*开始日期(周一到周日 用0-6，节假日用8，制定日期用YYYYMMDD)*/
	
		public uint? dwEndDay;		/*结束日期(同 dwStartDay定义)*/
	
	public DAYOPENRULE[] DayOpenRule;		/*开放时间段(CUniTable[DAYOPENRULE])*/
	
		public string szMemo;		/*开放说明*/
		};

	/*设备开放时间表*/
	
	public struct DEVOPENRULE
	{
		private Reserved reserved;
		
		public uint? dwRuleSN;		/*编号*/
	
		public string szRuleName;		/*名称*/
	
	public GROUPOPENRULE[] GroupOpenRule;		/*指定用户组开放时间表(CUniTable[GROUPOPENRULE])*/
	
		public string szMemo;		/*说明信息*/
		};

	/*当前设备统计信息*/
	
	public struct CURDEVSTAT
	{
		private Reserved reserved;
		
		public uint? dwChgNum;		/*信息发生改变的统计栏目数*/
	
	public TEACHINGDEVSTAT TeachingDevStat;		/*设备信息统计*/
		};

	/*教学设备统计*/
	
	public struct TEACHINGDEVSTAT
	{
		private Reserved reserved;
		
		public uint? dwTotalNum;		/*总设备数*/
	
		public uint? dwUseNum;		/*正在使用设备数*/
	
		public uint? dwIdleNum;		/*空闲设备数*/
		};

	/*获取教学用设备按节次统计*/
	
	public struct DEVFORTREQ
	{
		private Reserved reserved;
		
		public uint? dwDate;		/*日期*/
	
		public string szRoomIDs;		/*房间ID,多个用逗号隔开*/
		};

	/*设备节次教学使用信息*/
	
	public struct DEVSECINFO
	{
		private Reserved reserved;
		
		public uint? dwSecIndex;		/*节次编号(1开始编)*/
	
		public string szSecName;		/*节次名称*/
	
		public uint? dwResvDevs;		/*预约机器数*/
	
		public uint? dwUseDevs;		/*实际用机数*/
	
		public uint? dwResvUsers;		/*上课总人数*/
	
		public uint? dwRealUsers;		/*实际到课人数*/
		};

	/*获取教学用设备*/
	
	public struct TEACHINGDEVREQ
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*设备ID*/
	
		public string szDevName;		/*实验设备名称*/
	
		public uint? dwDevSN;		/*设备编号*/
	
		public string szLabIDs;		/*实验室ID,多个用逗号隔开*/
	
		public string szRoomIDs;		/*房间ID,多个用逗号隔开*/
	
		public string szResvIDs;		/*预约ID,多个用逗号隔开*/
	
		public string szKindIDs;		/*所属类型,多个用逗号隔开*/
	
		public string szClassIDs;		/*所属功能类别ID,多个用逗号隔开*/
	
		public uint? dwDevStat;		/*设备状态*/
	
		public uint? dwRunStat;		/*设备状态*/
	
		public uint? dwUsePurpose;		/*见UNIDEVICE定义*/
	
		public uint? dwCtrlMode;		/*控制方式*/
	
		public uint? dwProperty;		/*设备属性*/
	
		public uint? dwClassKind;		/*类别(见UNIDEVCLS的Kind定义)*/
	
		public uint? dwManGroupID;		/*管理员组ID*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*教学设备信息*/
	
	public struct TEACHINGDEV
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*设备ID*/
	
		public uint? dwDevSN;		/*设备编号*/
	
		public string szDevName;		/*实验设备名称*/
	
		public uint? dwDevStat;		/*设备状态*/
	
		public uint? dwCtrlMode;		/*控制方式*/
	
		public uint? dwRunStat;		/*设备状态*/
	
		public string szKindName;		/*设备名称*/
	
		public string szFuncCode;		/*设备功能用途编码*/
	
		public string szModel;		/*设备型号*/
	
		public string szSpecification;		/*设备规格*/
	
		public uint? dwProperty;		/*设备属性（前16种为UNIDEVKIND定义*/
	
		public uint? dwMaxUsers;		/*最多同时使用人数*/
	
		public uint? dwMinUsers;		/*最少同时使用人数*/
	
		public uint? dwCurUsers;		/*当前使用人数*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabSN;		/*实验室编号*/
	
		public string szLabName;		/*实验室名称*/
	
		public uint? dwRoomID;		/*房间ID*/
	
		public string szRoomNo;		/*房间号（不重复,用于关联门禁，格式B-F-N,建筑编号-楼层号-房间编号）*/
	
		public string szRoomName;		/*房间名称*/
	
		public uint? dwUsePurpose;		/*同UniFee的dwPurpose*/
	
		public uint? dwCurAccNo;		/*当前用户账号*/
	
		public string szCurTrueName;		/*当前用户姓名*/
	
		public uint? dwResvID;		/*预约ID*/
	
		public uint? dwTestItemID;		/*实验项目ID*/
	
		public string szTestName;		/*实验名称*/
	
		public uint? dwTestPlanID;		/*实验计划ID*/
	
		public string szTestPlanName;		/*实验计划名称*/
	
		public uint? dwTeacherID;		/*教师（帐号）*/
	
		public string szTeacherName;		/*教师姓名*/
	
		public uint? dwCourseID;		/*课程ID*/
	
		public string szCourseName;		/*课程名称*/
	
		public uint? dwGroupID;		/*上课班级*/
	
		public string szGroupName;		/*上课班级名称*/
	
		public uint? dwTeachingTime;		/*教学时间(WWDSSEE)第几周（WW，星期几(D),开始节次（SS）结束节次（EE)*/
		};

	/*获取获奖记录*/
	
	public struct REWARDRECREQ
	{
		private Reserved reserved;
		
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
		public uint? dwRewardID;		/*获奖ID*/
	
		public uint? dwRTID;		/*科研实验项目ID*/
	
		public uint? dwHolderID;		/*主持人（帐号）*/
	
		public uint? dwLeaderID;		/*负责人ID*/
	
		public uint? dwOpID;		/*录入员ID*/
	
		public uint? dwRewardType;		/*获奖分类*/
	
		public uint? dwRewardKind;		/*获奖类型*/
	
		public uint? dwRewardLevel;		/*获奖级别*/
	
		public uint? dwDevID;		/*设备ID*/
	
		public string szLabIDs;		/*实验室ID,多个用逗号隔开*/
	
		public string szRoomIDs;		/*房间ID,多个用逗号隔开*/
	
		public string szKindIDs;		/*所属类型,多个用逗号隔开*/
	
		public string szClassIDs;		/*所属功能类别ID,多个用逗号隔开*/
	
		public uint? dwProperty;		/*设备属性*/
	
		public uint? dwClassKind;		/*类别(见UNIDEVCLS的Kind定义)*/
	
		public uint? dwUnitPrice;		/*大型仪器价格起点*/
	
		public uint? dwReqProp;		/*请求附加属性*/
	
		[FlagsAttribute]
		public enum DWREQPROP : uint
		{
			
				[EnumDescription("需要设备信息")]
				RRREQ_NEEDDEV = 0x1,
			
				[EnumDescription("按录入时间查询")]
				RRREQ_BYOPDATE = 0x2,
			
		}

	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*获奖使用设备*/
	
	public struct REWARDUSEDEV
	{
		private Reserved reserved;
		
		public uint? dwRewardID;		/*获奖ID*/
	
		public uint? dwDevID;		/*设备ID*/
	
		public uint? dwKindID;		/*设备类型*/
	
		public uint? dwDevSN;		/*设备编号*/
	
		public string szDevName;		/*设备名称*/
	
		public uint? dwRoomID;		/*房间ID*/
	
		public string szRoomName;		/*房间名称*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabName;		/*实验室名称*/
	
		public string szAttendantName;		/*值班员姓名*/
	
		public uint? dwStatus;		/*设备管理员确认状态*/
	
		public uint? dwTestTimes;		/*实验次数*/
	
		public uint? dwTestHour;		/*实验学时数*/
	
		public uint? dwRelyRate;		/*依赖度(百分比)，扩展*/
	
		public string szMemo;		/*备注*/
		};

	/*获奖记录*/
	
	public struct REWARDREC
	{
		private Reserved reserved;
		
		public uint? dwRewardID;		/*获奖ID*/
	
		public uint? dwRTID;		/*科研实验项目ID*/
	
		public string szRTName;		/*科研实验名称*/
	
		public uint? dwHolderID;		/*主持人（帐号）*/
	
		public string szHolderName;		/*主持人姓名*/
	
		public uint? dwLeaderID;		/*负责人ID*/
	
		public string szLeaderName;		/*负责人姓名*/
	
		public string szMemberNames;		/*获奖人员名单（逗号隔开）*/
	
		public uint? dwRewardDate;		/*获奖日期*/
	
		public uint? dwOpDate;		/*录入日期*/
	
		public uint? dwOpID;		/*录入员ID*/
	
		public string szOpName;		/*录入员姓名*/
	
		public uint? dwRewardType;		/*获奖分类*/
	
		[FlagsAttribute]
		public enum DWREWARDTYPE : uint
		{
			
				[EnumDescription("教学")]
				RETYPE_TEACHING = 1,
			
				[EnumDescription("科研")]
				RETYPE_RESEARCH = 2,
			
				[EnumDescription("其它")]
				RETYPE_OTHER = 4,
			
		}

	
		public uint? dwRewardKind;		/*获奖类型*/
	
		[FlagsAttribute]
		public enum DWREWARDKIND : uint
		{
			
				[EnumDescription("科技奖")]
				REKIND_PRIZE = 1,
			
				[EnumDescription("专利")]
				REKIND_PATENT = 2,
			
				[EnumDescription("论文检索")]
				REKIND_THESISINDEX = 4,
			
				[EnumDescription("论文发表")]
				REKIND_THESISISSUE = 8,
			
				[EnumDescription("教材")]
				REKIND_TEXTBOOK = 0x10,
			
		}

	
		public string szRewardName;		/*奖项名称*/
	
		public uint? dwRewardLevel;		/*获奖级别*/
	
		[FlagsAttribute]
		public enum DWREWARDLEVEL : uint
		{
			
				[EnumDescription("国家级")]
				RELEVEL_NATIONAL = 1,
			
				[EnumDescription("省部级")]
				RELEVEL_PROVINCE = 2,
			
				[EnumDescription("三大检索")]
				RELEVEL_THREEINDEX = 4,
			
				[EnumDescription("核心刊物")]
				RELEVEL_KERNELJOURNAL = 8,
			
				[EnumDescription("专利")]
				RELEVEL_PATENT = 0x10,
			
		}

	
		public string szAuthOrg;		/*发证机关*/
	
		public string szCertID;		/*证书编号*/
	
		public string szExtInfo;		/*扩展信息*/
	
	public REWARDUSEDEV[] UseDev;		/*CUniTable[REWARDUSEDEV]*/
	
		public string szMemo;		/*备注*/
		};

	/*获取费用记录*/
	
	public struct COSTRECREQ
	{
		private Reserved reserved;
		
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
		public uint? dwCostType;		/*费用类型*/
	
		public string szLabIDs;		/*实验室ID,多个用逗号隔开*/
	
		public string szRoomIDs;		/*房间ID,多个用逗号隔开*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*费用记录*/
	
	public struct COSTREC
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*流水号*/
	
		public uint? dwCostType;		/*费用类型*/
	
		[FlagsAttribute]
		public enum DWCOSTTYPE : uint
		{
			
				[EnumDescription("购置费")]
				COSTTYPE_BUY = 0x1000,
			
				[EnumDescription("维护费")]
				COSTTYPE_KEEP = 0x2000,
			
				[EnumDescription("运行费")]
				COSTTYPE_RUN = 0x4000,
			
				[EnumDescription("耗材费")]
				COSTTYPE_CONSUME = 0x4001,
			
				[EnumDescription("建设费")]
				COSTTYPE_BUILD = 0x8000,
			
				[EnumDescription("研究与改革经费")]
				COSTTYPE_RANDR = 0x10000,
			
				[EnumDescription("其它")]
				COSTTYPE_OTHER = 0x20000,
			
		}

	
		public uint? dwPurpose;		/*用途*/
	
		[FlagsAttribute]
		public enum DWPURPOSE : uint
		{
			
				[EnumDescription("教学")]
				COSTFOR_TEACHING = 0x1,
			
				[EnumDescription("其它")]
				COSTFOR_OTHER = 0x2,
			
		}

	
		public uint? dwRoomID;		/*房间ID*/
	
		public string szRoomName;		/*房间名称*/
	
		public uint? dwSubID;		/*比如设备ID（扩展）*/
	
		public uint? dwCost;		/*费用（元）*/
	
		public string szExtInfo;		/*关联财务信息*/
	
		public uint? dwCostDate;		/*发生日期*/
	
		public uint? dwOpTime;		/*录入时间*/
	
		public uint? dwOpID;		/*录入管理员ID*/
	
		public string szOpName;		/*录入管理员姓名*/
	
		public string szMemo;		/*备注*/
		};

	/*结束数据结构*/

	/*开始数据结构*/

	/*获取门禁集控器的请求包*/
	
	public struct DCSREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*方式*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("全部")]
				DCSGET_BYALL = 0,
			
				[EnumDescription("集控器编号")]
				DCSGET_BYSN = 1,
			
				[EnumDescription("所属站点")]
				DCSGET_BYSTASN = 4,
			
		}

	
		public string szGetKey;		/*条件值*/
	
		public uint? dwDCSKind;		/*门禁集控器类型*/
		};

	/**/
	
	public struct UNIDCS
	{
		private Reserved reserved;
		
		public uint? dwSN;		/*门禁集控器编号*/
	
		public uint? dwDCSKind;		/*门禁集控器类型*/
	
		[FlagsAttribute]
		public enum DWDCSKIND : uint
		{
			
				[EnumDescription("门禁监控")]
				DCSKIND_DOORCTRL = 1,
			
				[EnumDescription("视频监控")]
				DCSKIND_VIDEOCTRL = 2,
			
		}

	
		public string szName;		/*门禁集控器名称*/
	
		public string szRoomNo;		/*门禁所在房间号*/
	
		public string szIP;		/*门禁集控器IP地址*/
	
		public uint? dwStatus;		/*门禁集控器状态*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("未运行")]
				DCSSTAT_CLOSED = 0,
			
				[EnumDescription("运行中")]
				DCSSTAT_RUNNING = 1,
			
				[EnumDescription("故障")]
				DCSSTAT_TROUBLE = 2,
			
				[EnumDescription("门开")]
				DCSSTAT_DOOROPEN = 0x100,
			
				[EnumDescription("门关")]
				DCSSTAT_DOORCLOSED = 0x200,
			
				[EnumDescription("电源未接通")]
				DCSSTAT_POWEROFF = 0x2000,
			
				[EnumDescription("电源已接通")]
				DCSSTAT_POWERON = 0x4000,
			
				[EnumDescription("电源工作中")]
				DCSSTAT_POWERWORKING = 0x8000,
			
				[EnumDescription("禁用")]
				DCSSTAT_DISABLED = 0x10000,
			
		}

	
		public uint? dwStaSN;		/*管理站点编号*/
	
		public string szStaName;		/*站点名称*/
	
		public uint? dwStatChgTime;		/*状态改变时间*/
	
		public string szStatInfo;		/*状态描述*/
	
		public string szMemo;		/*备注*/
		};

	/*门禁信息表*/
	
	public struct DOORCTRLINFO
	{
		private Reserved reserved;
		
		public uint? dwCtrlSN;		/*控制器编号*/
	
		public string szCtrlModel;		/*控制器类型（比如版本等）*/
	
		public uint? dwCtrlKind;		/*控制器类型见UNIDOORCTRL定义*/
	
		public string szCtrlIP;		/*控制器IP地址*/
		};

	/*集控器登录请求*/
	
	public struct DCSLOGINREQ
	{
		private Reserved reserved;
		
		public string szVersion;		/*版本	XX.XX.XXXXXXXX*/
	
		public string szIP;		/*IP地址*/
	
		public string szMemo;		/*说明信息*/
		};

	/*集控器登录响应*/
	
	public struct DCSLOGINRES
	{
		private Reserved reserved;
		
		public uint? dwSessionID;		/*服务器分配的SessionID*/
	
	public UNIVERSION SrvVer;		/*UNIVERSION 结构*/
	
		public string szCurTime;		/*服务器时间 YYYY-MM-DD HH:MM:SS*/
	
		public uint? dwDCSSN;		/*控制台编号*/
	
		public string szDCSName;		/*控制台名称*/
	
		public string szMemo;		/*说明信息*/
	
	public DOORCTRLINFO[] szManCtrls;		/*管理门禁列表CUniTable[DOORCTRLINFO]*/
		};

	/*集控器退出请求*/
	
	public struct DCSLOGOUTREQ
	{
		private Reserved reserved;
		
		public uint? dwDCSSN;		/*集控器编号*/
	
		public string szMemo;		/*说明信息*/
		};

	/*控制器状态*/
	
	public struct DOORCTRLSTAT
	{
		private Reserved reserved;
		
		public uint? dwCtrlSN;		/*控制器编号*/
	
		public uint? dwStatus;		/*DCSSTAT_TROUBLE,DCSSTAT_DOOROPEN,DCSSTAT_DOORCLOSED*/
	
		public string szMemo;		/*备注*/
		};

	/*集控器定时通信请求*/
	
	public struct DCSPULSEREQ
	{
		private Reserved reserved;
		
		public uint? dwDCSSN;		/*控制台编号*/
	
	public DOORCTRLSTAT[] szControllerStat;		/*控制器状态信息CUniTable[DOORCTRLSTAT]*/
		};

	/*集控器定时通信应答*/
	
	public struct DCSPULSERES
	{
		private Reserved reserved;
		
		public uint? dwChanged;		/*集控器是否改变*/
	
		public string szMemo;		/*备注*/
		};

	/*门禁刷卡请求*/
	
	public struct DOORCARDREQ
	{
		private Reserved reserved;
		
		public uint? dwDCSSN;		/*集控器编号*/
	
		public uint? dwCtrlSN;		/*控制器编号*/
	
		public uint? dwCardMode;		/*内外刷卡及卡号类别(3字节，4字节等)*/
	
		[FlagsAttribute]
		public enum DWCARDMODE : uint
		{
			
				[EnumDescription("门外刷卡,打算进入")]
				DOORCARD_IN = 1,
			
				[EnumDescription("门内刷卡,打算出去")]
				DOORCARD_OUT = 2,
			
				[EnumDescription("按钮")]
				DOORCARD_BUTTON = 0x1000,
			
				[EnumDescription("手机开门")]
				MOBILE_OPENDOOR = 0x10000,
			
		}

	
		public string szCardNo;		/*卡号*/
	
		public string szMemo;		/*说明信息*/
		};

	/*门禁刷卡响应*/
	
	public struct DOORCARDRES
	{
		private Reserved reserved;
		
		public uint? dwUserKind;		/*用户种类*/
	
		[FlagsAttribute]
		public enum DWUSERKIND : uint
		{
			
				[EnumDescription("管理员")]
				CARDUSER_MANAGER = 1,
			
				[EnumDescription("指定的用户组成员")]
				CARDUSER_GROUP = 2,
			
				[EnumDescription("使用设备")]
				CARDUSER_USEDEV = 4,
			
				[EnumDescription("经过通道")]
				CARDUSER_PASSCHANNEL = 8,
			
				[EnumDescription("开放规则组成员(保洁人员)")]
				CARDUSER_OPENGROUP = 0x10,
			
				[EnumDescription("考勤组成员")]
				CARDUSER_ATTENDGROUP = 0x20,
			
				[EnumDescription("允许开门")]
				CARDUSER_PERMIT = 0x100,
			
				[EnumDescription("禁止开门")]
				CARDUSER_FORBID = 0x200,
			
		}

	
		public uint? dwSoundSN;		/*播放声音*/
	
		public uint? dwDeadLine;		/*允许截止时间*/
	
		public string szPID;		/*学工号*/
	
		public string szCardNo;		/*卡号*/
	
		public string szTrueName;		/*姓名*/
	
		public string szDispInfo;		/*显示信息*/
		};

	/*手机开门请求*/
	
	public struct MOBILEOPENDOORREQ
	{
		private Reserved reserved;
		
		public string szMSN;		/*MSN*/
	
		public string szLogonName;		/*登录名*/
	
		public string szPassword;		/*密码*/
	
		public string szIP;		/*IP地址*/
	
		public uint? dwProperty;		/*扩展属性*/
	
		[FlagsAttribute]
		public enum DWPROPERTY : uint
		{
			
				[EnumDescription("认证成功绑定微信号")]
				MODPROP_BINDMSN = 1,
			
		}

	
		public uint? dwDCSSN;		/*集控器编号*/
	
		public uint? dwCtrlSN;		/*控制器编号*/
	
		public uint? dwCardMode;		/*内外刷卡*/
	
		public string szMemo;		/*说明信息*/
		};

	/*手机开门响应*/
	
	public struct MOBILEOPENDOORRES
	{
		private Reserved reserved;
		
		public uint? dwUserKind;		/*用户种类(DOORCARDRES定义)*/
	
		public uint? dwFailedType;		/*失败类型*/
	
		[FlagsAttribute]
		public enum DWFAILEDTYPE : uint
		{
			
				[EnumDescription("微信号未绑定用户")]
				MODFAILED_NOBIND = 0x1,
			
				[EnumDescription("用户认证失败")]
				MODFAILED_CHECKWRONG = 0x2,
			
				[EnumDescription("未授权用户")]
				MODFAILED_UNAUTH = 0x4,
			
				[EnumDescription("接入IP不匹配")]
				MODFAILED_IPUNMAP = 0x8,
			
		}

	
		public string szPID;		/*学工号*/
	
		public string szTrueName;		/*姓名*/
	
		public string szDispInfo;		/*显示信息*/
		};

	/*获取门禁控制器的请求包*/
	
	public struct DOORCTRLREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*方式*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("全部")]
				DOORCTRLGET_BYALL = 0,
			
				[EnumDescription("集控器编号")]
				DOORCTRLGET_BYDCSSN = 1,
			
				[EnumDescription("门禁控制器编号")]
				DOORCTRLGET_BYSN = 2,
			
				[EnumDescription("房间编号")]
				DOORCTRLGET_BYROOMNO = 3,
			
				[EnumDescription("所属站点")]
				DOORCTRLGET_BYSTASN = 4,
			
		}

	
		public string szGetKey;		/*条件值*/
	
		public uint? dwDCSKind;		/*门禁集控器类型*/
		};

	/**/
	
	public struct UNIDOORCTRL
	{
		private Reserved reserved;
		
		public uint? dwDCSSN;		/*集控器编号*/
	
		public uint? dwDCSKind;		/*门禁集控器类型*/
	
		public string szDCSName;		/*门禁集控器名称*/
	
		public string szDCSIP;		/*门禁集控器IP地址*/
	
		public uint? dwCtrlSN;		/*控制器编号*/
	
		public uint? dwCtrlKind;		/*控制器类型*/
	
		[FlagsAttribute]
		public enum DWCTRLKIND : uint
		{
			
				[EnumDescription("房间门禁")]
				DCKIND_ROOM = 0x1,
			
				[EnumDescription("通道大门门禁")]
				DCKIND_CHANNELGATE = 0x2,
			
				[EnumDescription("网络控制")]
				DCKIND_NETCTRL = 0x4,
			
				[EnumDescription("电源控制器")]
				DCKIND_POWERCTRL = 0x10,
			
				[EnumDescription("双向门禁")]
				DCKIND_DOUBLE = 0x100,
			
				[EnumDescription("出门刷卡需认证")]
				DCKIND_OUTCHECK = 0x200,
			
				[EnumDescription("有控制台功能")]
				DCKIND_ASCONSOLE = 0x400,
			
				[EnumDescription("有独立的显示器")]
				DCKIND_WITHDISPLAY = 0x800,
			
				[EnumDescription("门开关状态和标准相反")]
				DCKIND_DOOROPENREVERSE = 0x1000,
			
				[EnumDescription("有考勤功能")]
				DCKIND_WITHATTENDANCE = 0x2000,
			
		}

	
		public string szRoomNo;		/*房间号*/
	
		public string szCtrlName;		/*控制器名称*/
	
		public string szCtrlModel;		/*控制器型号（比如版本等）*/
	
		public string szCtrlIP;		/*控制器IP地址*/
	
		public uint? dwStaSN;		/*所属站点编号*/
	
		public string szStaName;		/*站点名称*/
	
		public uint? dwStatus;		/*门禁状态见 UNIDCS定义*/
	
		public uint? dwStatChgTime;		/*状态改变时间*/
	
		public string szStatInfo;		/*状态描述*/
	
		public string szMODIP;		/*允许手机开门IP段*/
	
		public string szMemo;		/*备注*/
	
		public uint? dwRoomID;		/*房间ID*/
	
		public string szRoomName;		/*房间名称*/
	
		public string szFloorNo;		/*所在楼层*/
	
		public uint? dwBuildingID;		/*楼宇ID*/
	
		public string szBuildingNo;		/*大楼编号(*/
	
		public string szBuildingName;		/*大楼名称(*/
		};

	/*给门禁控制器发命令*/
	
	public struct DOORCTRLCMD
	{
		private Reserved reserved;
		
		public uint? dwCtrlSN;		/*控制器编号*/
	
		public uint? dwCmd;		/*控制命令(参考DEVCTRLINFO::dwCmd)*/
	
		public string szParam;		/*控制参数*/
	
		public string szMemo;		/*备注*/
		};

	/*结束数据结构*/

	/*开始数据结构*/

	/*获取组信息的请求包*/
	
	public struct GROUPREQ
	{
		private Reserved reserved;
		
		public uint? dwGroupID;		/*组ID*/
	
		public string szName;		/*名称*/
	
		public uint? dwKind;		/*类型*/
	
		public uint? dwAccNo;		/*组成员帐号*/
	
		public uint? dwMinDeadLine;		/*最小截止日期*/
	
		public uint? dwMaxDeadLine;		/*最大截止日期*/
	
		public uint? dwReqProp;		/*请求附加属性*/
	
		[FlagsAttribute]
		public enum DWREQPROP : uint
		{
			
				[EnumDescription("需要已删除组")]
				GROUPREQ_NEEDDEL = 0x1,
			
		}

	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*组成员*/
	
	public struct GROUPMEMBER
	{
		private Reserved reserved;
		
		public uint? dwGroupID;		/*组编号*/
	
		public uint? dwKind;		/*成员类别*/
	
		[FlagsAttribute]
		public enum DWKIND : uint
		{
			
				[EnumDescription("班级")]
				MEMBERKIND_CLASS = 1,
			
				[EnumDescription("个人")]
				MEMBERKIND_PERSONAL = 2,
			
				[EnumDescription("部门")]
				MEMBERKIND_DEPT = 4,
			
				[EnumDescription("身份")]
				MEMBERKIND_IDENT = 8,
			
				[EnumDescription("组")]
				MEMBERKIND_SUBGROUP = 16,
			
		}

	
		public uint? dwMemberID;		/*成员ID*/
	
		public string szName;		/*成员名称*/
	
		public uint? dwBeginDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*截止日期*/
	
		public uint? dwStatus;		/*状态（前8种留出用于CHECKINFO定义的管理员审核状态)*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("未生效")]
				GROUPMEMBERSTAT_UNFORCE = 0x100,
			
				[EnumDescription("生效中")]
				GROUPMEMBERSTAT_FORCE = 0x200,
			
				[EnumDescription("已过期")]
				GROUPMEMBERSTAT_EXPIRED = 0x400,
			
				[EnumDescription("已选座")]
				GROUPMEMBERSTAT_SEAT = 0x1000,
			
		}

	
		public string szExtInfo;		/*补充说明*/
	
		public string szMemo;		/*说明信息*/
		};

	/**/
	
	public struct UNIGROUP
	{
		private Reserved reserved;
		
		public uint? dwGroupID;		/*组ID*/
	
		public string szName;		/*名称*/
	
		public uint? dwKind;		/*类型*/
	
		[FlagsAttribute]
		public enum DWKIND : uint
		{
			
				[EnumDescription("预约组")]
				GROUPKIND_RERV = 0x100,
			
				[EnumDescription("设备指定使用组")]
				GROUPKIND_DEV = 0x200,
			
				[EnumDescription("管理员组")]
				GROUPKIND_MAN = 0x400,
			
				[EnumDescription("允许用户组")]
				GROUPKIND_USER = 0x800,
			
				[EnumDescription("招募组")]
				GROUPKIND_RECRUIT = 0x1000,
			
				[EnumDescription("开放规则组")]
				GROUPKIND_OPENRULE = 0x2000,
			
				[EnumDescription("考勤组")]
				GROUPKIND_ATTEND = 0x4000,
			
				[EnumDescription("大组里的小组")]
				GROUPKIND_SUBGROUP = 0x40000000,
			
				[EnumDescription("成员需管理员审核")]
				GROUPKIND_MEMBERNEEDCHECK = 0x80000000,
			
		}

	
		public uint? dwMaxUsers;		/*最大用户数*/
	
		public uint? dwMinUsers;		/*最少用户数*/
	
		public uint? dwDeadLine;		/*截止日期*/
	
		public uint? dwEnrollDeadline;		/*申请加入截止日*/
	
		public uint? dwAssociateID;		/*扩展用，关联ID，比如预约组的预约ID等*/
	
		public string szGroupURL;		/*组简介*/
	
	public GROUPMEMBER[] szMembers;		/*成员明细表(CUniTable<GROUPMEMBER>)*/
	
		public string szMemo;		/*备注*/
		};

	/*获取组成员明细的请求包*/
	
	public struct GROUPMEMDETAILREQ
	{
		private Reserved reserved;
		
		public uint? dwGroupKind;		/*组类别*/
	
		public uint? dwGroupID;		/*组编号*/
	
		public uint? dwStatus;		/*审核状态*/
	
		public uint? dwAccNo;		/*成员账号*/
	
		public uint? dwReqProp;		/*请求附加属性*/
	
		[FlagsAttribute]
		public enum DWREQPROP : uint
		{
			
				[EnumDescription("需要已删除组")]
				GROUPMEMDETAILREQ_NEEDDEL = 0x1,
			
		}

	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*组成员明细*/
	
	public struct GROUPMEMDETAIL
	{
		private Reserved reserved;
		
		public uint? dwGroupID;		/*组编号*/
	
		public uint? dwBeginDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*截止日期*/
	
		public uint? dwStatus;		/*审核状态*/
	
		public string szExtInfo;		/*补充说明*/
	
		public uint? dwAccNo;		/*成员账号*/
	
		public string szPID;		/*学工号*/
	
		public string szTrueName;		/*姓名*/
	
		public uint? dwClassID;		/*班级ID*/
	
		public string szClassName;		/*班级*/
	
		public uint? dwDeptID;		/*部门ID*/
	
		public string szDeptName;		/*部门*/
	
		public uint? dwMajorID;		/*专业ID*/
	
		public string szMajorName;		/*专业*/
	
		public uint? dwSex;		/*性别见UniCommon.h*/
	
		public uint? dwIdent;		/*身份 见UniCommon.h*/
	
		public uint? dwEnrolYear;		/*入学年份(XX级)*/
	
		public uint? dwSchoolYears;		/*学制*/
	
		public string szTel;		/*电话*/
	
		public string szHandPhone;		/*手机*/
	
		public string szEmail;		/*电邮*/
	
		public uint? dwTutorID;		/*导师账号*/
	
		public string szTutorName;		/*导师姓名*/
		};

	/*结束数据结构*/

	/*开始数据结构*/

	/*获取预约信息的请求包*/
	
	public struct RESVREQ
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*预约号*/
	
		public uint? dwMemberKind;		/*成员类别*/
	
		public uint? dwOwner;		/*预约人(所有者)*/
	
		public uint? dwMemberID;		/*成员ID*/
	
		public uint? dwDevID;		/*设备ID*/
	
		public uint? dwManagerID;		/*管理员ID*/
	
		public uint? dwAccNo;		/*成员账号，获取成员相关的所有实验安排*/
	
		public uint? dwTestPlanID;		/*获取TestPlan相关的所有实验安排*/
	
		public uint? dwCourseID;		/*获取课程相关的所有实验安排*/
	
		public uint? dwTestItemID;		/*获取TestItem相关的所有实验安排*/
	
		public uint? dwCheckStat;		/*确认状态*/
	
		public uint? dwUnNeedStat;		/*不包含状态*/
	
		public uint? dwUseMode;		/*使用方法*/
	
		public uint? dwPurpose;		/*预约用途*/
	
		public uint? dwDevKind;		/*设备类型*/
	
		public uint? dwClassKind;		/*类别(见UNIDEVCLS的Kind定义)*/
	
		public uint? dwBeginDate;		/*预约开始日期*/
	
		public uint? dwEndDate;		/*预约结束日期*/
	
		public string szRoomNos;		/*房间编号,多个用逗号隔开*/
	
		public uint? dwResvGroupID;		/*预约组ID*/
	
		public uint? dwStatFlag;		/*状态标志*/
	
		[FlagsAttribute]
		public enum DWSTATFLAG : uint
		{
			
				[EnumDescription("有效状态")]
				STATFLAG_INUSE = 0x1,
			
				[EnumDescription("结束状态")]
				STATFLAG_OVER = 0x2,
			
				[EnumDescription("删除状态")]
				STATFLAG_DEL = 0x4,
			
				[EnumDescription("审核失败")]
				STATFLAG_CHECKFAIL = 0x8,
			
		}

	
		public uint? dwKind;		/*预约类型*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*预约表*/
	
	public struct UNIRESERVE
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*预约号*/
	
		public uint? dwMemberKind;		/*成员类别*/
	
		[FlagsAttribute]
		public enum DWMEMBERKIND : uint
		{
			
				[EnumDescription("成员为用户组")]
				MEMBERKIND_GROUP = 0x1,
			
				[EnumDescription("成员为预约者帐号")]
				MEMBERKIND_PERSONNAL = 0x2,
			
		}

	
		public uint? dwUseMode;		/*使用方法*/
	
		[FlagsAttribute]
		public enum DWUSEMODE : uint
		{
			
				[EnumDescription("租借式预约")]
				RESVUSE_LEASE = 0x100,
			
				[EnumDescription("使用设备预约")]
				RESVUSE_USEDEV = 0x200,
			
				[EnumDescription("长期占用设备预约")]
				RESVUSE_LONGTERM = 0x400,
			
				[EnumDescription("电脑预约")]
				RESVUSEEXT_PC = 0x1000,
			
				[EnumDescription("手机预约")]
				RESVUSEEXT_HP = 0x2000,
			
				[EnumDescription("控制台预约")]
				RESVUSEEXT_CONSOLE = 0x4000,
			
				[EnumDescription("自由上机（和RESVUSE_USEDEV一起使用）")]
				RESVUSEEXT_USEPC = 0x10000,
			
				[EnumDescription("直接登录预约")]
				RESVUSE_DIRECTPC = 0x20000,
			
		}

	
		public uint? dwPurpose;		/*预约用途*/
	
		[FlagsAttribute]
		public enum DWPURPOSE : uint
		{
			
				[EnumDescription("教学预约")]
				USEFOR_TEACHING = 0x1,
			
				[EnumDescription("个人")]
				USEFOR_PERSONNAL = 0x2,
			
				[EnumDescription("开放活动")]
				USEFOR_ACTIVITY = 0x4,
			
				[EnumDescription("科学研究")]
				USEFOR_RESEACH = 0x8,
			
				[EnumDescription("场馆租用")]
				USEFOR_YARD = 0x10,
			
				[EnumDescription("管理员预留")]
				USEFOR_RESERVED = 0x20,
			
				[EnumDescription("预约使用座位")]
				USEFOR_SEAT = 0x40,
			
				[EnumDescription("预约使用电脑")]
				USEFOR_PC = 0x80,
			
				[EnumDescription("外借")]
				USEFOR_LOAN = 0x100,
			
				[EnumDescription("收费模式")]
				USEFOR_FEEMODE = 0x200,
			
				[EnumDescription("预约使用研修间")]
				USEFOR_STUDYROOM = 0x400,
			
				[EnumDescription("保养维护")]
				USEFOR_SERVICING = 0x1000,
			
				[EnumDescription("匿名(免登录)")]
				USEFOR_ANONYMOUS = 0x2000,
			
				[EnumDescription("全体学生")]
				USEFOR_ALLUSER = 0x4000,
			
				[EnumDescription("院内")]
				USEBY_DEPT = 0x10000,
			
				[EnumDescription("校内")]
				USEBY_INNER = 0x20000,
			
				[EnumDescription("校外(社会服务)")]
				USEBY_OUTSIDE = 0x40000,
			
				[EnumDescription("理论课中的实验")]
				USEFOR_WITHTHEORY = 0x100000,
			
				[EnumDescription("独立开设的实验课")]
				USEFOR_NOTHEORY = 0x200000,
			
		}

	
		public uint? dwKind;		/*预约类型(分类统计用)*/
	
		public uint? dwOwner;		/*预约人(所有者)*/
	
		public string szOwnerName;		/*预约人姓名*/
	
		public uint? dwMemberID;		/*成员ID*/
	
		public string szMemberName;		/*成员名称*/
	
		public uint? dwResvRuleSN;		/*关联预约规则*/
	
		public uint? dwFeeSN;		/*关联的收费标准*/
	
		public uint? dwOpenRuleSN;		/*关联开放时间表*/
	
		public uint? dwFeeMode;		/*收费方式*/
	
		public uint? dwUseFee;		/*预约总费用*/
	
		public uint? dwPreDate;		/*预约开始日期*/
	
		public uint? dwOccurTime;		/*预约发生时间*/
	
		public uint? dwBeginTime;		/*预约开始时间*/
	
		public uint? dwEndTime;		/*预约结束时间*/
	
		public uint? dwYearTerm;		/*学期编号*/
	
		public uint? dwTeachingTime;		/*教学时间(WWDSSEE)第几周（WW，星期几(D),开始节次（SS）结束节次（EE)*/
	
		public uint? dwCheckTime;		/*审查时间*/
	
		public uint? dwAdvanceCheckTime;		/*提前审查时间*/
	
		public uint? dwResvGroupID;		/*组ID(多时段预约组ID相同，其余为预约ID)*/
	
		public uint? dwCheckKinds;		/*审核类型(参考CHECKTYPE定义，可多个）*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabName;		/*实验室名称*/
	
	public RESVDEV[] ResvDev;		/*预约设备明细表(CUniTable<RESVDEV>)*/
	
		public uint? dwStatus;		/*预约状态(包括审查，是否生效，是否已取消等)*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("初步预约，非正式预约,5分钟内未确认将自动删除")]
				RESVSTAT_INFORMAL = 0x40,
			
				[EnumDescription("正式预约")]
				RESVSTAT_FORMAL = 0x80,
			
				[EnumDescription("预约未生效")]
				RESVSTAT_UNDO = 0x100,
			
				[EnumDescription("预约正在执行")]
				RESVSTAT_DOING = 0x200,
			
				[EnumDescription("未缴费")]
				RESVSTAT_UNPAID = 0x400,
			
				[EnumDescription("生效预约已取消")]
				RESVSTAT_CANCEL = 0x800,
			
				[EnumDescription("预约已分配具体设备")]
				RESVSTAT_DEV = 0x1000,
			
				[EnumDescription("预约已超时")]
				RESVSTAT_TIMEOUT = 0x2000,
			
				[EnumDescription("禁止修改预约")]
				RESVSTAT_NOCHG = 0x4000,
			
				[EnumDescription("已缴费")]
				RESVSTAT_PAID = 0x8000,
			
				[EnumDescription("教师或管理员未签到")]
				RESVSTAT_UNSIGN = 0x10000,
			
				[EnumDescription("教师或管理员已签到")]
				RESVSTAT_SIGNED = 0x20000,
			
				[EnumDescription("预约违约")]
				RESVSTAT_DEFAULT = 0x40000,
			
				[EnumDescription("未结算")]
				RESVSTAT_UNSETTLE = 0x80000,
			
				[EnumDescription("已结算")]
				RESVSTAT_SETTLED = 0x100000,
			
				[EnumDescription("未入账")]
				RESVSTAT_UNRECEIVE = 0x200000,
			
				[EnumDescription("已入账")]
				RESVSTAT_RECEIVED = 0x400000,
			
				[EnumDescription("未反馈或评价")]
				RESVSTAT_NOFEED = 0x800000,
			
				[EnumDescription("已下机")]
				RESVSTAT_PCCLOSED = 0x1000000,
			
				[EnumDescription("预约已执行结束")]
				RESVSTAT_DONE = 0x40000000,
			
		}

	
		public uint? dwProperty;		/*预约属性*/
	
		[FlagsAttribute]
		public enum DWPROPERTY : uint
		{
			
				[EnumDescription("独立完成")]
				RESVPROP_SELFDO = 1,
			
				[EnumDescription("管理员陪同")]
				RESVPROP_WITHMAN = 2,
			
				[EnumDescription("管理员操作")]
				RESVPROP_MANDO = 4,
			
				[EnumDescription("独占房间（房间内的其他设备不能再用）")]
				RESVPROP_LOCKROOM = 8,
			
				[EnumDescription("系统自动分配设备")]
				RESVPROP_AUTOASSIGN = 0x10,
			
				[EnumDescription("需签到后方可登录")]
				RESVPROP_NEEDSIGN = 0x20,
			
				[EnumDescription("不检查设备冲突")]
				RESVPROP_NOCONFLICTCHECK = 0x40,
			
				[EnumDescription("自带耗材")]
				RESVPROP_SELFCONSUMABLE = 0x80,
			
				[EnumDescription("强制关机")]
				RESVPROP_ENDSHUTDOWN = 0x100,
			
				[EnumDescription("强制注销")]
				RESVPROP_ENDLOGOUT = 0x200,
			
				[EnumDescription("定时通知")]
				RESVPROP_ENDNOTICE = 0x400,
			
				[EnumDescription("多时段")]
				RESVPROP_MULTTIME = 0x800,
			
				[EnumDescription("多房间")]
				RESVPROP_MULTROOM = 0x1000,
			
				[EnumDescription("多类型设备")]
				RESVPROP_MULTDEV = 0x2000,
			
				[EnumDescription("续约")]
				RESVPROP_CONTINUE = 0x4000,
			
				[EnumDescription("续约相同设备")]
				RESVPROP_CONTINUESAME = 0x8000,
			
				[EnumDescription("按设备类型预约")]
				RESVPROP_BYDEVKIND = 0x10000,
			
				[EnumDescription("需录像")]
				RESVPROP_NEEDVIDEO = 0x20000,
			
				[EnumDescription("离开时必须刷卡")]
				RESVPROP_EXITCARD = 0x40000,
			
				[EnumDescription("暂时离开超时预约仍保留（根据UNIRESVRULE.dwLimit指定）")]
				RESVPROP_LEAVEHOLD = 0x80000,
			
				[EnumDescription("不公开")]
				RESVPROP_UNOPEN = 0x100000,
			
				[EnumDescription("盈利")]
				RESVPROP_PROFIT = 0x200000,
			
				[EnumDescription("使用至关闭时间")]
				RESVPROP_TOCLOSETIME = 0x400000,
			
				[EnumDescription("第三方预约")]
				RESVPROP_BYTHIRD = 0x800000,
			
				[EnumDescription("与第三方共享设备")]
				RESVPROP_THIRDSHARE = 0x1000000,
			
				[EnumDescription("预约了组合房间")]
				RESVPROP_COMBINEROOM = 0x2000000,
			
				[EnumDescription("预约了组合房间的子房间")]
				RESVPROP_SUBROOM = 0x4000000,
			
				[EnumDescription("需要对成员考勤(点名)")]
				RESVPROP_ATTENDANCE = 0x8000000,
			
				[EnumDescription("VIP预约")]
				RESVPROP_VIP = 0x10000000,
			
		}

	
		public uint? dwTestItemID;		/*实验项目ID*/
	
		public string szTestName;		/*实验名称*/
	
		public uint? dwTestHour;		/*实验学时数*/
	
		public uint? dwSignTime;		/*教师签到时间*/
	
		public uint? dwResvDevs;		/*预约机器数*/
	
		public uint? dwUseDevs;		/*实际用机数*/
	
		public uint? dwResvUsers;		/*上课总人数*/
	
		public uint? dwRealUsers;		/*实际到课人数*/
	
		public string szApplicationURL;		/*提交的申请材料连接*/
	
		public string szMemo;		/*说明信息*/
		};

	/*获取科研实验预约的请求包*/
	
	public struct RTRESVREQ
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*预约号*/
	
		public uint? dwDevID;		/*设备ID*/
	
		public uint? dwDevKind;		/*设备类型*/
	
		public uint? dwClassKind;		/*类别(见UNIDEVCLS的Kind定义)*/
	
		public uint? dwRTID;		/*科研实验计划ID*/
	
		public uint? dwHolderID;		/*主持人（帐号）*/
	
		public uint? dwMAccNo;		/*组成员帐号*/
	
		public uint? dwLeaderID;		/*负责人ID*/
	
		public uint? dwCheckStat;		/*确认状态*/
	
		public uint? dwUnNeedStat;		/*不包含状态*/
	
		public uint? dwUseMode;		/*使用方法*/
	
		public uint? dwPurpose;		/*预约用途*/
	
		public uint? dwBeginDate;		/*预约开始日期*/
	
		public uint? dwEndDate;		/*预约结束日期*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*预约样品表*/
	
	public struct RESVSAMPLE
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*预约ID*/
	
		public uint? dwSampleSN;		/*样品编号*/
	
		public string szSampleName;		/*样品名称*/
	
		public string szUnitName;		/*计费单位*/
	
		public uint? dwUnitFee;		/*单价*/
	
		public uint? dwSampleNum;		/*数量*/
	
		public string szMemo;		/*说明信息*/
		};

	/*预约表*/
	
	public struct RTRESV
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*预约号*/
	
		public string szTestName;		/*科研实验名称*/
	
		public uint? dwUseMode;		/*使用方法*/
	
		public uint? dwPurpose;		/*预约用途*/
	
		public uint? dwProperty;		/*预约属性*/
	
		public uint? dwStatus;		/*预约状态(包括审查，是否生效，是否已取消等)*/
	
		public uint? dwOwner;		/*预约人(创建者)*/
	
		public string szOwnerName;		/*预约人姓名*/
	
		public uint? dwResvRuleSN;		/*关联预约规则*/
	
		public uint? dwOpenRuleSN;		/*关联开放时间表*/
	
		public uint? dwFeeSN;		/*费率SN*/
	
		public uint? dwFeeMode;		/*收费方式*/
	
		public uint? dwUseFee;		/*预约总费用*/
	
		public uint? dwPreDate;		/*预约开始日期*/
	
		public uint? dwOccurTime;		/*预约发生时间*/
	
		public uint? dwBeginTime;		/*预约开始时间*/
	
		public uint? dwEndTime;		/*预约结束时间*/
	
		public uint? dwCheckTime;		/*审查时间*/
	
		public uint? dwAdvanceCheckTime;		/*提前审查时间*/
	
		public uint? dwDevID;		/*设备ID*/
	
		public uint? dwDevKind;		/*设备类型*/
	
		public uint? dwDevSN;		/*设备编号*/
	
		public string szDevName;		/*设备名称*/
	
		public uint? dwRoomID;		/*房间ID*/
	
		public string szRoomName;		/*房间名称*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabName;		/*实验室名称*/
	
		public uint? dwBuildingID;		/*楼宇ID*/
	
		public string szBuildingNo;		/*大楼编号(*/
	
		public string szBuildingName;		/*大楼名称(*/
	
		public uint? dwCampusID;		/*校区ID*/
	
		public string szCampusName;		/*校区名称*/
	
		public uint? dwRTID;		/*科研实验项目ID*/
	
		public string szRTName;		/*科研实验名称*/
	
		public uint? dwHolderID;		/*主持人（帐号）*/
	
		public string szHolderName;		/*主持人姓名*/
	
		public uint? dwUserDeptID;		/*使用人部门ID*/
	
		public string szUserDeptName;		/*使用人部门*/
	
		public uint? dwLeaderID;		/*负责人ID*/
	
		public string szLeaderName;		/*负责人姓名*/
	
		public uint? dwGroupID;		/*组ID*/
	
		public uint? dwManID;		/*管理员ID*/
	
		public string szManName;		/*管理员姓名*/
	
		public uint? dwEstimatedTime;		/*预估时间(分钟)*/
	
		public uint? dwTestTimes;		/*实验次数*/
	
		public uint? dwRealUseTime;		/*实际实验时间(分钟)*/
	
		public uint? dwReceivableCost;		/*应缴费用*/
	
		public uint? dwRealCost;		/*实际缴纳费用*/
	
		public uint? dwPrepayment;		/*预收款金额*/
	
	public RESVSAMPLE[] ResvSample;		/*CUniTable[RESVSAMPLE](获取一条预约时才返回)*/
	
		public string szConsumables;		/*所需耗材清单*/
	
		public uint? dwBeforePersons;		/*前面排队人数*/
	
		public string szFundsNo;		/*经费卡编号*/
	
		public string szMemo;		/*说明信息*/
		};

	/*科研实验审核*/
	
	public struct RTRESVCHECK
	{
		private Reserved reserved;
		
		public uint? dwCheckStat;		/*管理员审核状态(定义在ADMINCHECK)*/
	
		public string szCheckDetail;		/*审查说明*/
	
	public RTRESV RTResv;		/*CUniStruct[RTRESV]*/
	
	public RTBILL[] BillInfo;		/*CUniTable[RTBILL]*/
	
		public string szMemo;		/*说明信息*/
		};

	/*科研实验账单审核*/
	
	public struct RTPREPAY
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*预约号*/
	
		public uint? dwPrepayment;		/*预收款金额*/
	
		public string szMemo;		/*说明信息*/
		};

	/*获取科研实验账单的请求包*/
	
	public struct RTRESVBILLREQ
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*预约号*/
		};

	/*科研实验账单*/
	
	public struct RTRESVBILL
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*预约号*/
	
		public uint? dwPrepayment;		/*预收款金额*/
	
	public RTBILL[] BillInfo;		/*CUniTable[RTBILL]*/
		};

	/*科研实验账单*/
	
	public struct RTBILL
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*流水号*/
	
		public uint? dwResvID;		/*预约号*/
	
		public uint? dwFeeType;		/*收费类别(FEEDETAIL定义)*/
	
		public uint? dwUnitFee;		/*费率*/
	
		public uint? dwReceivableCost;		/*应缴费用*/
	
		public uint? dwRealCost;		/*实际缴纳费用*/
	
		public uint? dwPayKind;		/*缴费方式(UNIBILL定义)*/
	
		public uint? dwStatus;		/*CHECKINFO定义的管理员审核状态+UNIBILL定义*/
	
		public string szMemo;		/*说明信息*/
		};

	/*科研实验账单审核*/
	
	public struct RTBILLCHECK
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*预约号*/
	
	public RTBILL[] BillInfo;		/*CUniTable[RTBILL]*/
	
		public string szMemo;		/*说明信息*/
		};

	/*科研实验账单结算*/
	
	public struct RTBILLSETTLE
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*预约号*/
	
		public uint? dwPurpose;		/*预约用途(见UNIRESERVE定义USEBY_XXX）*/
	
		public uint? dwPayKind;		/*缴费方式*/
	
		public uint? dwTotalCost;		/*缴费合计*/
	
		public string szFundsNo;		/*经费卡编号（多个用逗号隔开)*/
	
		public string szCostInfo;		/*扣费信息*/
	
	public RTBILL[] BillInfo;		/*CUniTable[RTBILL]*/
	
	public RESVSAMPLE[] ResvSample;		/*CUniTable[RESVSAMPLE]*/
	
		public string szMemo;		/*说明信息*/
		};

	/*科研实验账单入账*/
	
	public struct RTBILLRECEIVE
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*预约号*/
	
		public uint? dwReceiveDate;		/*入账日期*/
	
		public uint? dwTotalCost;		/*缴费合计*/
	
		public uint? dwTestFee;		/*分析测试费*/
	
		public uint? dwOpenFundFee;		/*开放基金*/
	
		public uint? dwServiceFee;		/*劳务费*/
	
		public string szMemo;		/*说明信息*/
		};

	/*免登录预约*/
	
	public struct ANONYMOUSRESV
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*预约号*/
	
		public uint? dwPurpose;		/*预约用途*/
	
		public uint? dwPreDate;		/*预约开始日期*/
	
		public uint? dwBeginTime;		/*预约开始时间*/
	
		public uint? dwEndTime;		/*预约结束时间*/
	
		public uint? dwYearTerm;		/*学期编号*/
	
		public uint? dwTeachingTime;		/*教学时间(WWDSSEE)第几周（WW，星期几(D),开始节次（SS）结束节次（EE)*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabName;		/*实验室名称*/
	
		public string szTestName;		/*实验名称*/
	
	public RESVDEV[] ResvDev;		/*预约设备明细表(CUniTable<RESVDEV>)*/
	
		public string szMemo;		/*说明信息*/
		};

	/*全体学生预约*/
	
	public struct ALLUSERRESV
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*预约号*/
	
		public uint? dwPurpose;		/*预约用途*/
	
		public uint? dwPreDate;		/*预约开始日期*/
	
		public uint? dwBeginTime;		/*预约开始时间*/
	
		public uint? dwEndTime;		/*预约结束时间*/
	
		public uint? dwYearTerm;		/*学期编号*/
	
		public uint? dwTeachingTime;		/*教学时间(WWDSSEE)第几周（WW，星期几(D),开始节次（SS）结束节次（EE)*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabName;		/*实验室名称*/
	
		public string szTestName;		/*实验名称*/
	
	public RESVDEV[] ResvDev;		/*预约设备明细表(CUniTable<RESVDEV>)*/
	
		public string szMemo;		/*说明信息*/
		};

	/*获取预约列表用于网站显示的请求包*/
	
	public struct RESVSHOWREQ
	{
		private Reserved reserved;
		
		public uint? dwBeginDate;		/*预约开始日期*/
	
		public uint? dwEndDate;		/*预约结束日期*/
	
		public uint? dwDevKind;		/*设备类型*/
	
		public uint? dwClassKind;		/*类别(见UNIDEVCLS的Kind定义)*/
	
		public uint? dwCheckStat;		/*确认状态*/
	
		public uint? dwPurpose;		/*预约用途*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*预约表*/
	
	public struct RESVSHOW
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*预约号*/
	
		public uint? dwPurpose;		/*预约用途*/
	
		public uint? dwKind;		/*预约类型(分类统计用)*/
	
		public uint? dwOwner;		/*预约人(所有者)*/
	
		public string szOwnerName;		/*预约人姓名*/
	
		public uint? dwPreDate;		/*预约开始日期*/
	
		public uint? dwBeginTime;		/*预约开始时间*/
	
		public uint? dwEndTime;		/*预约结束时间*/
	
		public uint? dwStatus;		/*预约状态(包括审查，是否生效，是否已取消等)*/
	
		public uint? dwProperty;		/*预约属性*/
	
		public string szTestName;		/*实验名称*/
	
		public string szRoomNo;		/*房间号*/
	
		public uint? dwDevSN;		/*设备编号*/
	
		public string szDevName;		/*设备名*/
		};

	/*获取教学预约列表的请求包*/
	
	public struct TEACHINGRESVREQ
	{
		private Reserved reserved;
		
		public uint? dwBeginDate;		/*预约开始日期*/
	
		public uint? dwEndDate;		/*预约结束日期*/
	
		public uint? dwTeacherID;		/*教师（帐号）*/
	
		public uint? dwAccNo;		/*成员账号，获取成员相关的所有实验安排*/
	
		public uint? dwTestPlanID;		/*获取TestPlan相关的所有实验安排*/
	
		public uint? dwCourseID;		/*获取课程相关的所有实验安排*/
	
		public uint? dwTestItemID;		/*获取TestItem相关的所有实验安排*/
	
		public uint? dwYearTerm;		/*学期编号*/
	
		public uint? dwPlanKind;		/*计划类型*/
	
		public uint? dwResvStat;		/*预约状态*/
	
		public string szRoomNo;		/*房间号*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*教学预约*/
	
	public struct TEACHINGRESV
	{
		private Reserved reserved;
		
		public uint? dwTestItemID;		/*实验项目ID*/
	
		public string szTestName;		/*实验名称*/
	
		public uint? dwTestPlanID;		/*实验计划ID*/
	
		public string szTestPlanName;		/*实验计划名称*/
	
		public uint? dwPlanKind;		/*计划类型*/
	
		public uint? dwTeacherID;		/*教师（帐号）*/
	
		public string szTeacherName;		/*教师姓名*/
	
		public uint? dwCourseID;		/*课程ID*/
	
		public string szCourseName;		/*课程名称*/
	
		public uint? dwGroupID;		/*上课班级*/
	
		public string szGroupName;		/*上课班级名称*/
	
		public uint? dwGroupUsers;		/*组用户数*/
	
		public uint? dwResvID;		/*预约ID*/
	
		public uint? dwResvStat;		/*预约状态*/
	
		public uint? dwPreDate;		/*预约日期*/
	
		public uint? dwBeginTime;		/*预约开始时间*/
	
		public uint? dwEndTime;		/*预约结束时间*/
	
		public uint? dwTeachingTime;		/*教学时间(WWDSSEE)第几周（WW，星期几(D),开始节次（SS）结束节次（EE)*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabName;		/*实验室名称*/
	
	public RESVDEV[] ResvDev;		/*预约设备明细表(CUniTable<RESVDEV>)*/
	
		public uint? dwCurUsers;		/*当前人数（仅对目前生效的预约有效）*/
		};

	/*放假调课*/
	
	public struct HOLIDAYSHIFT
	{
		private Reserved reserved;
		
		public uint? dwOldDate;		/*原上课日期*/
	
		public uint? dwNewDate;		/*新上课日期*/
	
		public uint? dwNoticeFlag;		/*通知标志*/
	
		public string szMemo;		/*说明*/
		};

	/*预约组成员*/
	
	public struct RESVMEMBER
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*预约号*/
	
		public uint? dwAccNo;		/*成员账号*/
	
		public string szTrueName;		/*姓名*/
		};

	/*预约开始信息*/
	
	public struct RESVSTARTINFO
	{
		private Reserved reserved;
		
		public uint? dwForceLogoff;		/*是否注销用户*/
	
		public uint? dwNoLogon;		/*免登录标志*/
	
		public uint? dwPurpose;		/*预约目的*/
	
		public string szClassName;		/*预约班级*/
	
		public string szUsers;		/*预约学号*/
		};

	/*预约开始信息*/
	
	public struct RESVENDINFO
	{
		private Reserved reserved;
		
		public uint? dwEndCmd;		/*结束方式*/
	
		[FlagsAttribute]
		public enum DWENDCMD : uint
		{
			
				[EnumDescription("系统强制结束")]
				REVEND_BYSYS = 1,
			
				[EnumDescription("用户自行结束")]
				REVEND_BYUSER = 2,
			
		}

	
		public string szMsg;		/*信息*/
		};

	/*自动预约请求，系统自动分配设备并暂时锁定，等待用户确认*/
	
	public struct AUTORESVREQ
	{
		private Reserved reserved;
		
		public uint? dwOwner;		/*预约人(所有者)*/
	
		public uint? dwKind;		/*预约成员类别*/
	
		public uint? dwPurpose;		/*预约用途*/
	
		public uint? dwPreDate;		/*预约发生日期*/
	
		public uint? dwEarlyBeginTime;		/*预约最早开始时间*/
	
		public uint? dwLateBeginTime;		/*预约最晚开始时间*/
	
		public uint? dwUseMin;		/*使用时长*/
	
		public uint? dwRoomID;		/*房间ID*/
	
		public uint? dwDevKind;		/*设备类型*/
	
		public string szUserLimit;		/*使用者约束（暂未使用)*/
	
		public string szDateLimit;		/*时间约束（暂未使用)*/
	
		public string szDevLimit;		/*设备约束（暂未使用)*/
		};

	/*预约设备明细*/
	
	public struct RESVDEV
	{
		private Reserved reserved;
		
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabName;		/*实验室名称*/
	
		public uint? dwRoomID;		/*房间ID*/
	
		public string szRoomNo;		/*房间编号*/
	
		public string szRoomName;		/*房间名称*/
	
		public uint? dwDevKind;		/*设备类型*/
	
		public uint? dwDevNum;		/*设备数量*/
	
		public uint? dwDevStart;		/*设备开始编号*/
	
		public uint? dwDevEnd;		/*设备结束编号*/
	
		public string szDevName;		/*设备名*/
	
		public string szModel;		/*设备型号*/
	
		public string szSpecification;		/*设备规格*/
	
		public uint? dwDevClsKind;		/*设备类别分类*/
	
		public string szMemo;		/*备注*/
		};

	/*预约时间段明细*/
	
	public struct RESVTIMEREQ
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*预约号*/
	
		public uint? dwResvGroupID;		/*组ID*/
		};

	/*预约时间段明细*/
	
	public struct RESVTIME
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*预约号*/
	
		public uint? dwResvGroupID;		/*组ID*/
	
		public uint? dwPreDate;		/*预约发生日期*/
	
		public uint? dwBeginTime;		/*预约开始时间*/
	
		public uint? dwEndTime;		/*预约结束时间*/
	
		public uint? dwTeachingTime;		/*教学时间(WWDSSEE)第几周（WW，星期几(D),开始节次（SS）结束节次（EE)*/
	
		public string szMemo;		/*备注*/
		};

	/*预约费用核算*/
	
	public struct RESVCOSTADJUST
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*预约号*/
	
		public uint? dwAdjustFee;		/*调整费用*/
	
		public uint? dwSampleFee;		/*样品费*/
	
		public uint? dwConfirmorID;		/*管理员帐号*/
	
		public string szConfirmor;		/*管理员姓名*/
	
		public string szMemo;		/*备注（核算费用调整的说明)*/
		};

	/*预约费用结算*/
	
	public struct RESVCHECKOUT
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*预约号*/
	
		public uint? dwBillID;		/*账单号*/
	
		public uint? dwRealCheckFee;		/*实际结算费用*/
	
		public uint? dwConfirmorID;		/*审查管理员帐号*/
	
		public string szConfirmor;		/*审查管理员姓名*/
	
		public string szMemo;		/*备注（结算附加费用的说明)*/
		};

	/*预约结束时间*/
	
	public struct RESVENDTIME
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*预约ID*/
	
		public uint? dwEndTime;		/*结束时间*/
		};

	/*获取设备预约信息的请求包*/
	
	public struct DEVRESVREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*方式*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("设备ID")]
				DEVRESVGET_BYID = 1,
			
		}

	
		public string szGetKey;		/*条件值*/
	
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*设备预约信息*/
	
	public struct DEVRESVINFO
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*设备ID*/
	
		public uint? dwResvID;		/*预约号*/
	
		public uint? dwBeginTime;		/*预约开始时间*/
	
		public uint? dwEndTime;		/*预约结束时间*/
		};

	/*获取作息时间表请求包*/
	
	public struct CTSREQ
	{
		private Reserved reserved;
		
		public uint? dwSN;		/*作息表编号*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*作息时间表*/
	
	public struct CLASSTIMETABLE
	{
		private Reserved reserved;
		
		public uint? dwSN;		/*作息表编号*/
	
		public uint? dwSecIndex;		/*节次编号(1开始编)*/
	
		public string szSecName;		/*节次名称*/
	
		public uint? dwBeginTime;		/*开始时间(HHMM)*/
	
		public uint? dwEndTime;		/*结束时间(HHMM)*/
		};

	/*获取学期信息的请求包*/
	
	public struct TERMREQ
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*学期编号（20091001 2009－10年第一学期）*/
	
		public uint? dwStatus;		/*学期状态*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/**/
	
	public struct UNITERM
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*学期编号（20091001 2009－10年第一学期）*/
	
		public uint? dwStatus;		/*学期状态*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("已过期")]
				TERMSTAT_OVER = 1,
			
				[EnumDescription("生效中")]
				TERMSTAT_FORCE = 2,
			
				[EnumDescription("未生效")]
				TERMSTAT_UNFORCE = 4,
			
		}

	
		public uint? dwBeginDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
		public uint? dwFirstWeekDays;		/*起始周天数*/
	
		public uint? dwTotalWeeks;		/*一共有多少周*/
	
		public uint? dwSecNum;		/*每日节次总数*/
	
	public CLASSTIMETABLE[] szCTS1;		/*作息时间表1(CUniTable<CLASSTIMETABLE>)*/
	
		public uint? dwCTS1Begin;		/*时间表1开始生效日期*/
	
		public uint? dwCTS1End;		/*时间表1结束生效日期*/
	
	public CLASSTIMETABLE[] szCTS2;		/*作息时间表2(CUniTable<CLASSTIMETABLE>)*/
	
		public uint? dwCTS2Begin;		/*时间表2开始生效日期*/
	
		public uint? dwCTS2End;		/*时间表2结束生效日期*/
	
		public string szMemo;		/*备注*/
		};

	/*获取课程的请求包*/
	
	public struct COURSEREQ
	{
		private Reserved reserved;
		
		public uint? dwCourseID;		/*课程ID*/
	
		public string szCourseCode;		/*课程代码*/
	
		public string szCourseName;		/*课程名称*/
	
		public uint? dwOwnerDept;		/*开课学院*/
	
		public uint? dwMajorID;		/*所属专业ID*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*课程*/
	
	public struct UNICOURSE
	{
		private Reserved reserved;
		
		public uint? dwCourseID;		/*课程ID*/
	
		public string szCourseCode;		/*课程代码*/
	
		public string szCourseName;		/*课程名称*/
	
		public uint? dwCourseProperty;		/*课程属性*/
	
		[FlagsAttribute]
		public enum DWCOURSEPROPERTY : uint
		{
			
				[EnumDescription("理论课中的实验")]
				COURSEPROP_WITHTHEORY = 1,
			
				[EnumDescription("独立开设的实验课")]
				COURSEPROP_NOTHEORY = 2,
			
		}

	
		public uint? dwOwnerDept;		/*开课学院*/
	
		public string szDeptName;		/*开课学院名称*/
	
		public uint? dwMajorID;		/*所属专业ID*/
	
		public string szMajorName;		/*所属专业名称*/
	
		public string szType;		/*课程类别（选修，必修等）*/
	
		public uint? dwHardCoef;		/*难度系数*/
	
		public uint? dwCreditHour;		/*学分*/
	
		public uint? dwTheoryHour;		/*理论学时数*/
	
		public uint? dwTestHour;		/*实验学时数*/
	
		public uint? dwPracticeHour;		/*实践学时数*/
	
		public uint? dwTestNum;		/*实验次数*/
	
		public string szMemo;		/*备注*/
		};

	/*获取实验计划请求包*/
	
	public struct TESTPLANREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*方式*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("全部")]
				TESTPLANGET_BYALL = 0,
			
				[EnumDescription("ID")]
				TESTPLANGET_BYID = 1,
			
				[EnumDescription("课程ID")]
				TESTPLANGET_BYCOURSEID = 2,
			
				[EnumDescription("课程名称")]
				TESTPLANGET_BYNAME = 3,
			
				[EnumDescription("开课学院")]
				TESTPLANGET_BYDEPT = 4,
			
				[EnumDescription("专业")]
				TESTPLANGET_BYMAJOR = 5,
			
				[EnumDescription("教师")]
				TESTPLANGET_BYTEACHER = 6,
			
				[EnumDescription("成员账号，获取成员相关的所有实验安排")]
				TESTPLANGET_BYMEMBERACCNO = 7,
			
		}

	
		public string szGetKey;		/*条件值*/
	
		public uint? dwYearTerm;		/*学期编号*/
	
		public uint? dwKind;		/*实验计划类型*/
	
		public uint? dwStatus;		/*实验计划状态*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/**/
	
	public struct UNITESTPLAN
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*学期编号*/
	
		public uint? dwTestPlanID;		/*实验计划ID*/
	
		public string szTestPlanName;		/*实验计划名称*/
	
		public string szAcademicSubjectCode;		/*所属学科编码*/
	
		public uint? dwTesteeKind;		/*实验者类别*/
	
		[FlagsAttribute]
		public enum DWTESTEEKIND : uint
		{
			
				[EnumDescription("博士生")]
				TESTEEKIND_DOCTOR = 1,
			
				[EnumDescription("硕士生")]
				TESTEEKIND_MASTER = 2,
			
				[EnumDescription("本科生")]
				TESTEEKIND_COLLEGE = 3,
			
				[EnumDescription("专科生")]
				TESTEEKIND_JUNIOR = 4,
			
				[EnumDescription("其它")]
				TESTEEKIND_OTHER = 5,
			
		}

	
		public uint? dwTeacherID;		/*教师（帐号）*/
	
		public string szTeacherName;		/*教师姓名*/
	
		public uint? dwTeacherDeptID;		/*教师所属部门ID*/
	
		public string szTeacherDeptName;		/*教师所属部门*/
	
		public uint? dwCourseID;		/*课程ID*/
	
		public string szCourseCode;		/*课程代码*/
	
		public string szCourseName;		/*课程名称*/
	
		public uint? dwCourseProperty;		/*课程属性*/
	
		public uint? dwTheoryHour;		/*理论学时数*/
	
		public uint? dwTestHour;		/*实验学时数*/
	
		public uint? dwPracticeHour;		/*实践学时数*/
	
		public uint? dwTestNum;		/*实验项目数*/
	
		public uint? dwGroupID;		/*上课班级*/
	
		public string szGroupName;		/*上课班级名称*/
	
		public uint? dwMaxUsers;		/*最大用户数*/
	
		public uint? dwMinUsers;		/*最少用户数*/
	
		public uint? dwGroupUsers;		/*上课学生数*/
	
		public uint? dwEnrollDeadline;		/*申请加入截止日*/
	
		public uint? dwKind;		/*实验计划类型*/
	
		[FlagsAttribute]
		public enum DWKIND : uint
		{
			
				[EnumDescription("教学统一安排")]
				TESTPLANKIND_TEACHING = 1,
			
				[EnumDescription("教学开放实验")]
				TESTPLANKIND_OPEN = 2,
			
				[EnumDescription("教学自主安排")]
				TESTPLANKIND_TEACHFORSELF = 4,
			
		}

	
		public uint? dwStatus;		/*实验计划状态（前8种留出用于CHECKINFO定义的管理员审核状态)*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("未开放")]
				TESTPLANSTAT_UNOPEN = 0x100,
			
				[EnumDescription("开放中")]
				TESTPLANSTAT_OPENING = 0x200,
			
				[EnumDescription("已关闭")]
				TESTPLANSTAT_CLOSED = 0x400,
			
				[EnumDescription("已选修")]
				TESTPLANSTAT_SELECTED = 0x10000,
			
		}

	
		public string szTestPlanURL;		/*详细描述*/
	
		public uint? dwTotalTestHour;		/*总学时数*/
	
		public uint? dwResvTestHour;		/*已预约学时数*/
	
		public uint? dwDoneTestHour;		/*已完成学时数*/
	
		public string szUsableLab;		/*可用实验室ID（多个用逗号隔开)*/
	
		public string szMemo;		/*备注*/
		};

	/*获取实验项目卡请求包*/
	
	public struct TESTCARDREQ
	{
		private Reserved reserved;
		
		public uint? dwTestCardID;		/*实验项目卡ID*/
	
		public string szTestName;		/*实验名称（模糊匹配)*/
	
		public uint? dwTestClass;		/*实验类别*/
	
		public uint? dwTestKind;		/*实验类型*/
	
		public uint? dwRequirement;		/*实验要求*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*实验项目卡*/
	
	public struct TESTCARD
	{
		private Reserved reserved;
		
		public uint? dwTestCardID;		/*实验项目卡ID*/
	
		public string szTestSN;		/*实验编号*/
	
		public string szTestName;		/*实验名称*/
	
		public string szCategoryName;		/*类别名(*/
	
		public uint? dwGroupPeopleNum;		/*每组人数*/
	
		public uint? dwTestHour;		/*本实验项目学时数*/
	
		public uint? dwTestClass;		/*实验类别（是按实验项目任务本身的性质分类的填基础、技术（或专业）基础、专业、科研、生产、其他（为毕业论文、毕业设计、技术开发和社会服务而开的实验））*/
	
		[FlagsAttribute]
		public enum DWTESTCLASS : uint
		{
			
				[EnumDescription("基础")]
				TESTCLASS_BASIS = 1,
			
				[EnumDescription("专业基础")]
				TESTCLASS_PROFBASIS = 2,
			
				[EnumDescription("专业")]
				TESTCLASS_PROFESSION = 3,
			
				[EnumDescription("其它")]
				TESTCLASS_OTHER = 4,
			
		}

	
		public uint? dwTestKind;		/*实验类型（演示、验证、综合、设计）*/
	
		[FlagsAttribute]
		public enum DWTESTKIND : uint
		{
			
				[EnumDescription("演示性")]
				TESTKIND_DEMO = 1,
			
				[EnumDescription("验证性")]
				TESTKIND_VERIFY = 2,
			
				[EnumDescription("综合性")]
				TESTKIND_INTEGRITY = 3,
			
				[EnumDescription("研究设计")]
				TESTKIND_RESEARCH = 4,
			
				[EnumDescription("其它")]
				TESTKIND_OTHER = 5,
			
		}

	
		public uint? dwRequirement;		/*实验要求（填必修、选修、其他（科研测试、生产服务、技术开发等））*/
	
		[FlagsAttribute]
		public enum DWREQUIREMENT : uint
		{
			
				[EnumDescription("必修")]
				TESTREQUIRE_REQUIRED = 1,
			
				[EnumDescription("选修")]
				TESTREQUIRE_OPTIONAL = 2,
			
				[EnumDescription("其它")]
				TESTREQUIRE_OTHER = 3,
			
		}

	
		public string szConstraints;		/*约束条件（比如时间限制和需要的设备等,格式有专门文件定义*/
	
		public string szTestItemURL;		/*详细描述*/
	
		public string szMemo;		/*备注*/
		};

	/*获取实验项目请求包*/
	
	public struct TESTITEMREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*方式*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("全部")]
				TESTITEMGET_BYALL = 0,
			
				[EnumDescription("ID")]
				TESTITEMGET_BYID = 1,
			
				[EnumDescription("父ID")]
				TESTITEMGET_BYPLANID = 2,
			
				[EnumDescription("项目名称")]
				TESTITEMGET_BYNAME = 3,
			
		}

	
		public string szGetKey;		/*条件值*/
	
		public uint? dwStatus;		/*状态（前8种留出用于审核状态)*/
	
		public uint? dwCourseID;		/*课程ID*/
	
		public uint? dwTeacherID;		/*教师（帐号）*/
	
		public uint? dwAccNo;		/*帐号*/
	
		public uint? dwPlanKind;		/*计划类型*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*实验安排项目卡*/
	
	public struct UNITESTITEM
	{
		private Reserved reserved;
		
		public uint? dwTestItemID;		/*实验项目ID*/
	
		public uint? dwTestPlanID;		/*实验计划ID*/
	
		public string szTestPlanName;		/*实验计划名称*/
	
		public string szAcademicSubjectCode;		/*所属学科编码*/
	
		public uint? dwTesteeKind;		/*实验者类别*/
	
		public uint? dwTotalTestHour;		/*本实验计划总学时数*/
	
		public uint? dwTeacherID;		/*教师（帐号）*/
	
		public string szTeacherName;		/*教师姓名*/
	
		public uint? dwCourseID;		/*课程ID*/
	
		public string szCourseCode;		/*课程代码*/
	
		public string szCourseName;		/*课程名称*/
	
		public uint? dwCourseProperty;		/*课程属性*/
	
		public uint? dwGroupID;		/*上课班级*/
	
		public string szGroupName;		/*上课班级名称*/
	
		public uint? dwGroupUsers;		/*组用户数*/
	
		public uint? dwPlanKind;		/*计划类型*/
	
		public uint? dwPlanStatus;		/*计划状态*/
	
		public uint? dwTestCardID;		/*实验项目卡ID*/
	
		public string szTestSN;		/*实验编号*/
	
		public string szTestName;		/*实验名称*/
	
		public string szCategoryName;		/*类别名(*/
	
		public uint? dwStatus;		/*状态（前8种留出用于审核状态)*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("已预约")]
				TESTITEMSTAT_RESVED = 0x100,
			
				[EnumDescription("已执行")]
				TESTITEMSTAT_DONE = 0x200,
			
				[EnumDescription("部分已预约")]
				TESTITEMSTAT_PARTRESVED = 0x400,
			
				[EnumDescription("部分已执行")]
				TESTITEMSTAT_PARTDONE = 0x800,
			
				[EnumDescription("已交实验报告")]
				TESTITEMSTAT_REPORTDONE = 0x1000,
			
				[EnumDescription("实验报告已批改")]
				TESTITEMSTAT_REPORTSCORE = 0x2000,
			
		}

	
		public uint? dwGroupPeopleNum;		/*每组人数*/
	
		public uint? dwTestHour;		/*本实验项目学时数*/
	
		public uint? dwTestClass;		/*实验类别*/
	
		public uint? dwTestKind;		/*实验类型*/
	
		public uint? dwRequirement;		/*实验要求*/
	
		public string szTestItemURL;		/*详细描述*/
	
	public UNIRESERVE[] ResvInfo;		/*预约详细信息*/
	
		public uint? dwMaxResvTimes;		/*最多预约次数*/
	
		public uint? dwResvTestHour;		/*已预约学时数*/
	
		public uint? dwDoneTestHour;		/*已完成学时数*/
	
		public string szReportFormURL;		/*实验报告模板*/
	
		public string szConstraints;		/*约束条件（比如需要的设备等）*/
	
		public string szMemo;		/*备注*/
		};

	/*获取实验项目试验者预约信息请求包*/
	
	public struct TESTITEMMEMRESVREQ
	{
		private Reserved reserved;
		
		public uint? dwTestPlanID;		/*实验计划ID*/
	
		public uint? dwTestItemID;		/*实验项目ID*/
	
		public uint? dwGroupID;		/*上课班级*/
	
		public uint? dwResvTestHour;		/*已预约学时数*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*实验项目试验者预约信息*/
	
	public struct TESTITEMMEMRESV
	{
		private Reserved reserved;
		
		public uint? dwAccNo;		/*帐号*/
	
		public string szPID;		/*学工号*/
	
		public string szTrueName;		/*姓名*/
	
		public uint? dwResvTestHour;		/*已预约学时数*/
		};

	/*获取实验项目详细信息请求包*/
	
	public struct TESTITEMINFOREQ
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*记录ID*/
	
		public uint? dwTeacherID;		/*教师（帐号）*/
	
		public uint? dwAccNo;		/*帐号*/
	
		public uint? dwTestPlanID;		/*实验计划ID*/
	
		public uint? dwTestItemID;		/*实验项目ID*/
	
		public uint? dwYearTerm;		/*学期编号*/
	
		public uint? dwPlanKind;		/*计划类型*/
	
		public uint? dwStatus;		/*状态（前8种留出用于审核状态)*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*实验项目详细信息*/
	
	public struct TESTITEMINFO
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*记录ID*/
	
		public uint? dwStatus;		/*见UNITESTITEM定义*/
	
		public uint? dwAccNo;		/*帐号*/
	
		public string szPID;		/*学工号*/
	
		public string szTrueName;		/*姓名*/
	
		public uint? dwClassID;		/*班级ID*/
	
		public string szClassName;		/*班级*/
	
		public uint? dwDeptID;		/*部门ID*/
	
		public string szDeptName;		/*部门*/
	
		public uint? dwMaxResvTimes;		/*最多预约次数*/
	
		public string szReportFormURL;		/*实验报告模板*/
	
		public string szReportURL;		/*提交的实验报告*/
	
		public uint? dwReportScore;		/*实验报告评分*/
	
		public string szReportMarkInfo;		/*实验报告批改信息*/
	
		public uint? dwYearTerm;		/*学期编号*/
	
		public uint? dwTestPlanID;		/*实验计划ID*/
	
		public string szTestPlanName;		/*实验计划名称*/
	
		public uint? dwPlanKind;		/*计划类型*/
	
		public uint? dwPlanStatus;		/*计划状态*/
	
		public string szAcademicSubjectCode;		/*所属学科编码*/
	
		public uint? dwTesteeKind;		/*实验者类别*/
	
		public uint? dwTotalTestHour;		/*本实验计划总学时数*/
	
		public uint? dwTeacherID;		/*教师（帐号）*/
	
		public string szTeacherName;		/*教师姓名*/
	
		public uint? dwCourseID;		/*课程ID*/
	
		public string szCourseName;		/*课程名称*/
	
		public uint? dwTestItemID;		/*实验项目ID*/
	
		public string szConstraints;		/*约束条件（比如需要的设备等）*/
	
		public uint? dwTestCardID;		/*实验项目卡ID*/
	
		public string szTestName;		/*实验名称*/
	
		public string szCategoryName;		/*类别名(*/
	
		public uint? dwGroupPeopleNum;		/*每组人数*/
	
		public uint? dwTestHour;		/*本实验项目学时数*/
	
		public uint? dwTestClass;		/*实验类别*/
	
		public uint? dwTestKind;		/*实验类型*/
	
		public uint? dwRequirement;		/*实验要求*/
	
		public string szTestItemURL;		/*详细描述*/
	
		public uint? dwResvTestHour;		/*已预约学时数*/
	
		public uint? dwDoneTestHour;		/*已完成学时数*/
	
	public UNIRESERVE[] ResvInfo;		/*预约详细信息*/
	
		public string szMemo;		/*备注*/
		};

	/*提交实验报告模板*/
	
	public struct REPORTFORMUPLOAD
	{
		private Reserved reserved;
		
		public uint? dwTestItemID;		/*实验项目ID*/
	
		public uint? dwTeacherID;		/*教师（帐号）*/
	
		public string szReportFormURL;		/*实验报告模板*/
		};

	/*提交实验报告*/
	
	public struct REPORTUPLOAD
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*记录ID*/
	
		public uint? dwAccNo;		/*帐号*/
	
		public string szReportURL;		/*提交的实验报告*/
		};

	/*批改实验报告*/
	
	public struct REPORTCORRECT
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*记录ID*/
	
		public uint? dwAccNo;		/*帐号*/
	
		public uint? dwTeacherID;		/*教师（帐号）*/
	
		public uint? dwReportScore;		/*实验报告评分*/
	
		public string szReportMarkInfo;		/*实验报告批改信息*/
		};

	/*设备组合*/
	
	public struct DEVGROUP
	{
		private Reserved reserved;
		
		public uint? dwParentID;		/*所属父ID(比如实验项目ID)*/
	
		public uint? dwDevKind;		/*设备类型*/
	
		public string szDevName;		/*设备名称*/
	
		public uint? dwDevNum;		/*设备数量*/
	
		public uint? dwProperty;		/*设备属性（依赖关系，比如必选，可选等）*/
	
		public string szMemo;		/*备注*/
		};

	/*获取活动安排请求包*/
	
	public struct ACTIVITYPLANREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*方式*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("全部")]
				ACTIVITYPLANGET_BYALL = 0,
			
				[EnumDescription("ID")]
				ACTIVITYPLANGET_BYID = 1,
			
				[EnumDescription("课程名称")]
				ACTIVITYPLANGET_BYNAME = 3,
			
		}

	
		public string szGetKey;		/*条件值*/
	
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwKind;		/*活动安排类型*/
	
		public uint? dwStatus;		/*活动安排状态*/
	
		public uint? dwOwner;		/*预约人(创建者)*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/**/
	
	public struct UNIACTIVITYPLAN
	{
		private Reserved reserved;
		
		public uint? dwActivityPlanID;		/*活动安排ID*/
	
		public string szActivityPlanName;		/*活动安排名称*/
	
		public string szHostUnit;		/*主办单位*/
	
		public string szOrganizer;		/*承办单位*/
	
		public string szPresenter;		/*主持人*/
	
		public string szDesiredUser;		/*参与者要求*/
	
		public uint? dwCheckRequirment;		/*申请人审核要求*/
	
		[FlagsAttribute]
		public enum DWCHECKREQUIRMENT : uint
		{
			
				[EnumDescription("需审核申请人资格")]
				ACTIVITYPLANCHECK_NEED = 0x1,
			
				[EnumDescription("无需申请即可参加")]
				ACTIVITYPLAN_NOAPPLY = 0x2,
			
				[EnumDescription("支持选座")]
				ACTIVITYPLAN_SUPPORTSEAT = 0x4,
			
				[EnumDescription("需对出席者考勤")]
				ACTIVITYPLAN_ATTENDANCE = 0x8,
			
		}

	
		public uint? dwOwner;		/*预约人(创建者)*/
	
		public string szContact;		/*联系人*/
	
		public string szTel;		/*联系电话*/
	
		public string szHandPhone;		/*联系手机*/
	
		public string szEmail;		/*联系电子邮箱*/
	
		public uint? dwResvID;		/*预约号*/
	
		public uint? dwGroupID;		/*组ID（用于获取组成员明细）*/
	
		public uint? dwMaxUsers;		/*最大限制人数*/
	
		public uint? dwMinUsers;		/*最少限制人数*/
	
		public uint? dwEnrollUsers;		/*已申请人数*/
	
		public uint? dwEnrollDeadline;		/*申请加入截止日*/
	
		public uint? dwPublishDate;		/*发布日期*/
	
		public uint? dwActivityDate;		/*活动日期*/
	
		public uint? dwBeginTime;		/*开始时间(HHMM)*/
	
		public uint? dwEndTime;		/*结束时间(HHMM)*/
	
		public string szSite;		/*主办地点*/
	
		public uint? dwDevID;		/*空间（主办地点）ID*/
	
		public uint? dwDevSN;		/*设备编号*/
	
		public string szDevName;		/*设备名称*/
	
		public uint? dwRoomID;		/*房间ID*/
	
		public string szRoomNo;		/*房间编号*/
	
		public string szRoomName;		/*房间名称*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabName;		/*实验室名称*/
	
		public uint? dwBuildingID;		/*楼宇ID*/
	
		public string szBuildingNo;		/*大楼编号(*/
	
		public string szBuildingName;		/*大楼名称(*/
	
		public uint? dwCampusID;		/*校区ID*/
	
		public string szCampusName;		/*校区名称*/
	
		public uint? dwKind;		/*活动安排类型*/
	
		[FlagsAttribute]
		public enum DWKIND : uint
		{
			
				[EnumDescription("新产品新技术用户体验")]
				ACTIVITYPLANKIND_EXPERIENCE = 1,
			
				[EnumDescription("学术讲座")]
				ACTIVITYPLANKIND_LECTURE = 2,
			
				[EnumDescription("文化沙龙")]
				ACTIVITYPLANKIND_SALON = 4,
			
				[EnumDescription("会议")]
				ACTIVITYPLANKIND_MEETING = 8,
			
		}

	
		public uint? dwStatus;		/*活动安排状态（前8种留出用于CHECKINFO定义的管理员审核状态)*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("未开放")]
				ACTIVITYPLANSTAT_UNOPEN = 0x100,
			
				[EnumDescription("开放中")]
				ACTIVITYPLANSTAT_OPENING = 0x200,
			
				[EnumDescription("已关闭")]
				ACTIVITYPLANSTAT_CLOSED = 0x400,
			
				[EnumDescription("已加入该活动")]
				ACTIVITYPLANSTAT_ENROLLED = 0x10000,
			
				[EnumDescription("已选座位")]
				ACTIVITYPLANSTAT_SEAT = 0x20000,
			
		}

	
		public string szIntroInfo;		/*活动介绍*/
	
		public string szActivityPlanURL;		/*详细描述URL*/
	
		public string szApplicationURL;		/*提交的申请材料连接*/
	
		public uint? dwRealUsers;		/*实到人数*/
	
		public string szMemo;		/*备注*/
		};

	/*获取活动安排的座位请求包*/
	
	public struct APSEATREQ
	{
		private Reserved reserved;
		
		public uint? dwActivityPlanID;		/*活动安排ID*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*活动安排的座位信息*/
	
	public struct APSEAT
	{
		private Reserved reserved;
		
		public uint? dwActivityPlanID;		/*活动安排ID*/
	
		public uint? dwDevID;		/*设备ID*/
	
		public uint? dwDevSN;		/*设备编号*/
	
		public string szDevName;		/*设备名称*/
	
		public uint? dwStatus;		/*座位状态*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("空闲")]
				APSTAT_IDLE = 0x1,
			
				[EnumDescription("已预定")]
				APSTAT_BOOKED = 0x2,
			
		}

	
		public uint? dwAccNo;		/*账号*/
	
		public string szTrueName;		/*成员姓名*/
	
		public string szMemo;		/*备注*/
		};

	/*申请参加活动*/
	
	public struct ACTIVITYENROLL
	{
		private Reserved reserved;
		
		public uint? dwActivityPlanID;		/*活动安排ID*/
	
		public uint? dwDevID;		/*设备ID*/
	
		public uint? dwDevSN;		/*设备编号*/
	
		public uint? dwAccNo;		/*账号*/
	
		public string szTrueName;		/*成员姓名*/
	
		public string szMemo;		/*备注*/
		};

	/*退出活动申请*/
	
	public struct ACTIVITYEXIT
	{
		private Reserved reserved;
		
		public uint? dwActivityPlanID;		/*活动安排ID*/
	
		public uint? dwAccNo;		/*账号*/
	
		public string szTrueName;		/*成员姓名*/
	
		public string szMemo;		/*备注*/
		};

	/*签到人员名单*/
	
	public struct ASIGNUSER
	{
		private Reserved reserved;
		
		public uint? dwCardID;		/*卡ID号*/
	
		public uint? dwInTime;		/*签到时间(HHMM)*/
	
		public uint? dwRetStat;		/*返回状态*/
	
		[FlagsAttribute]
		public enum DWRETSTAT : uint
		{
			
				[EnumDescription("签到成功")]
				SIGNRETSTAT_OK = 0x1,
			
				[EnumDescription("未找到该卡号对应的学生")]
				SIGNRETSTAT_NOCARDID = 0x100,
			
				[EnumDescription("未不是该活动的成员")]
				SIGNRETSTAT_NOMEMBER = 0x200,
			
				[EnumDescription("预约已结束")]
				SIGNRETSTAT_RESVDONE = 0x400,
			
		}

	
		public uint? dwAccNo;		/*账号*/
	
		public string szLogonName;		/*登录名(学工号)*/
	
		public string szCardNo;		/*卡号*/
	
		public string szTrueName;		/*成员姓名*/
	
		public string szMemo;		/*备注*/
		};

	/*签到人员名单*/
	
	public struct AOFFLINESIGN
	{
		private Reserved reserved;
		
		public uint? dwActivityPlanID;		/*活动安排ID*/
	
		public uint? dwResvID;		/*预约号*/
	
	public ASIGNUSER[] SignUser;		/*签到表CUniTable[ASIGNUSER]*/
	
		public string szMemo;		/*备注*/
		};

	/**/
	
	public struct RESVRULEREQ
	{
		private Reserved reserved;
		
		public uint? dwRuleSN;		/*设备规则编号*/
	
		public uint? dwDevClass;		/*设备类别（0表示无限制）*/
	
		public uint? dwDevKind;		/*设备类型（0表示无限制）*/
	
		public uint? dwDevID;		/*设备ID（0表示无限制）*/
	
		public uint? dwResvPurpose;		/*预约用途*/
	
		public uint? dwClassKind;		/*类别(见UNIDEVCLS的Kind定义)*/
	
		public uint? dwExtValue;		/*不同预约类型定义的扩展值（0表示无限制）*/
		};

	/**/
	
	public struct RESVRULEADMINREQ
	{
		private Reserved reserved;
		
		public uint? dwRuleSN;		/*设备规则编号*/
	
		public uint? dwDevClass;		/*设备类别（0表示无限制）*/
	
		public uint? dwDevKind;		/*设备类型（0表示无限制）*/
	
		public uint? dwDevID;		/*设备ID（0表示无限制）*/
	
		public uint? dwResvPurpose;		/*预约用途*/
	
		public uint? dwClassKind;		/*类别(见UNIDEVCLS的Kind定义)*/
	
		public uint? dwExtValue;		/*不同预约类型定义的扩展值（0表示无限制）*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*审核表*/
	
	public struct RULECHECKINFO
	{
		private Reserved reserved;
		
		public uint? dwResvRuleSN;		/*设备规则编号*/
	
		public uint? dwCheckKind;		/*审核类型（新建时由系统自动分配）*/
	
		public uint? dwBeforeKind;		/*依赖其他审核类别(可多个）*/
	
		public uint? dwNeedMinTime;		/*审核需要的最短时间*/
	
		public uint? dwMapValue;		/*触发审核值（和预约属性相关）*/
	
		public uint? dwMainKind;		/*审核大类*/
	
		public string szCheckName;		/*审核名称*/
	
		public uint? dwCheckLevel;		/*审核级别(同UNIADMIN.dwManLevel定义）*/
	
		public uint? dwDeptID;		/*责任部门ID（学院级不设置，根据申请人自动匹配）*/
	
		public string szDeptName;		/*责任部门*/
	
		public uint? dwProperty;		/*审核属性*/
	
		[FlagsAttribute]
		public enum DWPROPERTY : uint
		{
			
				[EnumDescription("必须审核")]
				CHECKPROP_MAIN = 0x1,
			
				[EnumDescription("辅助行为审核（不通过不影响活动举行，但可能某些行为受限）")]
				CHECKPROP_SUB = 0x2,
			
		}

	
		public string szMemo;		/*状态说明*/
		};

	/*设备使用规则结构*/
	
	public struct UNIRESVRULE
	{
		private Reserved reserved;
		
		public uint? dwRuleSN;		/*设备规则编号*/
	
		public string szRuleName;		/*设备规则名称*/
	
		public uint? dwIdent;		/*身份（0表示无限制）*/
	
		public uint? dwDeptID;		/*部门（0表示无限制）*/
	
		public uint? dwDevClass;		/*设备类别（0表示无限制）*/
	
		public uint? dwDevKind;		/*设备类型（0表示无限制）*/
	
		public uint? dwDevID;		/*设备ID（0表示无限制）*/
	
		public uint? dwGroupID;		/*指定用户组（0表示无限制）*/
	
		public uint? dwResvPurpose;		/*预约用途*/
	
		public uint? dwExtValue;		/*不同预约类型定义的扩展值（0表示无限制）*/
	
		public uint? dwCreditRating;		/*信用等级*/
	
		public uint? dwPriority;		/*优先级(数字大代表优先级高)*/
	
		public uint? dwLimit;		/*预约限制*/
	
		[FlagsAttribute]
		public enum DWLIMIT : uint
		{
			
				[EnumDescription("无限制")]
				RESVLIMIT_FREE = 0x0,
			
				[EnumDescription("必须有管理员或老师已进入方可")]
				RESVLIMIT_NEEDMANAGER = 0x1,
			
				[EnumDescription("控制台刷卡后方可")]
				RESVLIMIT_CONSULECARD = 0x2,
			
				[EnumDescription("暂时离开超时预约仍保留")]
				RESVLIMIT_LEAVEHOLD = 0x4,
			
				[EnumDescription("需附带申请报告查")]
				RESVLIMIT_NEEDAPP = 0x8,
			
				[EnumDescription("离开时必须刷卡")]
				RESVLIMIT_EXITCARD = 0x10,
			
				[EnumDescription("有违约当日可继续使用")]
				RESVLIMIT_DEFAULTCANUSE = 0x20,
			
				[EnumDescription("拥有类管理员权限的VIP用户(不检测预约人数限制，可同时预约多个房间)")]
				RESVLIMIT_VIP = 0x40,
			
				[EnumDescription("只需预约某类型设备")]
				RESVLIMIT_DEVKIND = 0x100,
			
				[EnumDescription("预约时必须选择具体设备")]
				RESVLIMIT_DEV = 0x200,
			
				[EnumDescription("预约时不检查设备冲突(科研实验不定时排队场合)")]
				RESVLIMIT_NOCONFLICTCHECK = 0x400,
			
				[EnumDescription("新建，修改，删除预约不需要发送邮件，短信")]
				RESVLIMIT_NONOTICE = 0x800,
			
		}

	
		public uint? dwEarlyInTime;		/*允许提前进入时间(分钟)*/
	
		public uint? dwEarliestResvTime;		/*最早提前预约时间(分钟)，数字比下面大*/
	
		public uint? dwLatestResvTime;		/*最迟提前预约时间(分钟)，数字比上面小*/
	
		public uint? dwMinResvTime;		/*最短预约时间(分钟)*/
	
		public uint? dwMaxResvTime;		/*最长预约时间(分钟)*/
	
		public uint? dwResvEndNewTime;		/*当前预约结束前指定时间(分钟)内可新建预约*/
	
		public uint? dwResvBeforeNoticeTime;		/*预约生效提前通知时间(分钟)*/
	
		public uint? dwResvAfterNoticeTime;		/*预约生效不来通知时间(分钟)*/
	
		public uint? dwResvEndNoticeTime;		/*预约结束提前通知时间(分钟)*/
	
		public uint? dwSeriesTimeLimit;		/*连续预约时间间隔(分钟)*/
	
		public uint? dwTimeLimitForPurpose;		/*时间间隔相关的预约类型(比如座位管理，电子阅览室，研修间)*/
	
		public uint? dwTimeConflictForPurpose;		/*时间冲突的预约类型(比如座位管理，电子阅览室，研修间预约时间不能相互冲突)*/
	
		public uint? dwLatestSensorTime;		/*需审计的预约最迟审计时间(分钟)*/
	
		public uint? dwCancelTime;		/*预约不来自动取消预约时间(分钟)*/
	
		public uint? dwMinUseRate;		/*要求最低使用率(%)*/
	
		public uint? dwFeeMode;		/*收费方式*/
	
		[FlagsAttribute]
		public enum DWFEEMODE : uint
		{
			
				[EnumDescription("按使用时间收费")]
				FEEMODE_BYUSETIME = 1,
			
				[EnumDescription("按样品数收费")]
				FEEMODE_BYSAMPLE = 2,
			
				[EnumDescription("协议收费")]
				FEEMODE_BYNEGOTIATION = 4,
			
				[EnumDescription("登录时选择")]
				FEEMODEBY_LOGONSEL = 0x10000,
			
				[EnumDescription("使用结束需输入样品数")]
				FEENEED_INPUTSAMPLE = 0x20000,
			
		}

	
		public uint? dwMaxDevKind;		/*可预约设备种类*/
	
		public uint? dwMaxDevNum;		/*可预约设备数*/
	
		public string szOtherCons;		/*其他约束（比如时间限制和需要的设备等,格式有专门文件定义）*/
	
	public RULECHECKINFO[] CheckTbl;		/*审核表CUniTable[RULECHECKINFO]*/
	
		public string szMemo;		/*说明信息*/
		};

	/*获取科研实验请求包*/
	
	public struct RESEARCHTESTREQ
	{
		private Reserved reserved;
		
		public uint? dwRTID;		/*科研实验ID*/
	
		public uint? dwHolderID;		/*主持人（帐号）*/
	
		public uint? dwMemberID;		/*成员（帐号）*/
	
		public uint? dwLeaderID;		/*负责人ID*/
	
		public uint? dwDeptID;		/*部门ID*/
	
		public string szRTName;		/*科研实验名称*/
	
		public uint? dwRTLevel;		/*科研级别*/
	
		public uint? dwStatus;		/*状态*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*科研实验成员*/
	
	public struct RTMEMBER
	{
		private Reserved reserved;
		
		public uint? dwRTID;		/*科研实验ID*/
	
		public uint? dwGroupID;		/*组ID*/
	
		public uint? dwStatus;		/*成员状态(GROUPMEMBER定义)*/
	
		public uint? dwAccNo;		/*账号*/
	
		public string szTrueName;		/*成员姓名*/
		};

	/*科研实验*/
	
	public struct RESEARCHTEST
	{
		private Reserved reserved;
		
		public uint? dwRTID;		/*科研实验ID*/
	
		public string szRTSN;		/*科研实验编号*/
	
		public string szRTName;		/*科研实验名称*/
	
		public string szFromUnit;		/*下发单位*/
	
		public uint? dwRTKind;		/*科研类型*/
	
		[FlagsAttribute]
		public enum DWRTKIND : uint
		{
			
				[EnumDescription("科研课题")]
				RTKIND_RTASK = 0x1,
			
				[EnumDescription("教研课题")]
				RTKIND_TTASK = 0x2,
			
				[EnumDescription("毕业论文")]
				RTKIND_THESIS = 0x4,
			
				[EnumDescription("社会服务")]
				RTKIND_OUTSIDE = 0x100,
			
				[EnumDescription("校内课题")]
				RTKIND_INNER = 0x10000,
			
				[EnumDescription("校外课题")]
				RTKIND_OUTER = 0x20000,
			
		}

	
		public uint? dwRTLevel;		/*科研级别*/
	
		[FlagsAttribute]
		public enum DWRTLEVEL : uint
		{
			
				[EnumDescription("国家级")]
				RTLEVEL_NATIONAL = 1,
			
				[EnumDescription("省部级")]
				RTLEVEL_PROVINCE = 2,
			
				[EnumDescription("厅局级")]
				RTLEVEL_DEPT = 3,
			
				[EnumDescription("校级")]
				RTLEVEL_SCHOOL = 4,
			
				[EnumDescription("其它")]
				RTLEVEL_OTHER = 0x1000,
			
		}

	
		public uint? dwBeginDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*截止日期*/
	
		public uint? dwHolderID;		/*主持人（帐号）*/
	
		public string szHolderName;		/*主持人姓名*/
	
		public uint? dwLeaderID;		/*负责人ID*/
	
		public string szLeaderName;		/*负责人姓名*/
	
		public uint? dwDeptID;		/*部门ID*/
	
		public string szDeptName;		/*部门*/
	
		public uint? dwTestTimes;		/*实验次数*/
	
		public uint? dwTestMinutes;		/*实验累计时间*/
	
		public uint? dwBalance;		/*可用余额*/
	
		public uint? dwTotalFee;		/*累计费用*/
	
		public uint? dwUnpayFee;		/*未结算费用*/
	
		public uint? dwGroupID;		/*组ID*/
	
		public uint? dwGroupUsers;		/*组成员人数*/
	
		public uint? dwStatus;		/*状态（前8种留出用于CHECKINFO定义的管理员审核状态)*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("已关闭")]
				RTSTAT_CLOSED = 0x100,
			
		}

	
	public RTMEMBER[] RTMembers;		/*成员明细表(CUniTable<RTMEMBER>)*/
	
		public string szOtherCons;		/*其他约束（比如时间限制和需要的设备等,格式有专门文件定义）*/
	
		public string szFundsNo;		/*经费卡编号（多个用逗号隔开)*/
	
		public string szMemo;		/*备注*/
		};

	/*获取样品信息请求包*/
	
	public struct SAMPLEINFOREQ
	{
		private Reserved reserved;
		
		public uint? dwSampleSN;		/*样品编号*/
	
		public string szSampleName;		/*样品名称*/
	
		public uint? dwSamStat;		/*样品状态*/
	
		public uint? dwDevID;		/*设备ID（获取某设备专用）*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*样品信息*/
	
	public struct SAMPLEINFO
	{
		private Reserved reserved;
		
		public uint? dwSampleSN;		/*样品编号*/
	
		public string szSampleName;		/*样品名称*/
	
		public string szUnitName;		/*计费单位*/
	
		public uint? dwUnitFee1;		/*单价1*/
	
		public uint? dwUnitFee2;		/*单价2*/
	
		public uint? dwUnitFee3;		/*单价3*/
	
		public uint? dwUnitFee4;		/*单价4*/
	
		public uint? dwUnitFee5;		/*单价5*/
	
		public uint? dwSamStat;		/*样品状态*/
	
		[FlagsAttribute]
		public enum DWSAMSTAT : uint
		{
			
				[EnumDescription("未启用")]
				SAMSTAT_INUSE = 0x1,
			
				[EnumDescription("未启用")]
				SAMSTAT_UNUSE = 0x100,
			
		}

	
		public uint? dwDevID;		/*设备ID（单一设备专用赋值设备ID，通用为0）*/
	
		public string szMemo;		/*备注*/
		};

	/*获取场馆预约的请求包*/
	
	public struct YARDRESVREQ
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*预约号*/
	
		public uint? dwApplicantID;		/*申请人账号*/
	
		public uint? dwDevID;		/*设备ID*/
	
		public uint? dwDevKind;		/*设备类型*/
	
		public uint? dwClassKind;		/*类别(见UNIDEVCLS的Kind定义)*/
	
		public uint? dwClassID;		/*设备类别ID*/
	
		public uint? dwCheckStat;		/*确认状态*/
	
		public uint? dwUnNeedStat;		/*不包含状态*/
	
		public uint? dwBeginDate;		/*预约开始日期*/
	
		public uint? dwEndDate;		/*预约结束日期*/
	
		public uint? dwResvGroupID;		/*预约组ID*/
	
		public uint? dwStatFlag;		/*状态标志*/
	
		public string szLabIDs;		/*实验室ID,多个用逗号隔开*/
	
		public string szRoomIDs;		/*房间ID,多个用逗号隔开*/
	
		public string szBuildingIDs;		/*楼宇ID,多个用逗号隔开*/
	
		public string szCampusIDs;		/*校区ID,多个用逗号隔开*/
	
		public uint? dwActivitySN;		/*活动类型编号*/
	
		public uint? dwProperty;		/*属性*/
	
		public uint? dwUnNeedProperty;		/*不需要属性*/
	
		public string szResvName;		/*使用用途名称*/
	
		public uint? dwReqProp;		/*请求附加属性*/
	
		[FlagsAttribute]
		public enum DWREQPROP : uint
		{
			
				[EnumDescription("只要主预约（dwResvID=dwResvGroupID)")]
				YARDREQ_ONLYMAINRESV = 0x1,
			
		}

	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*场馆预约*/
	
	public struct YARDRESV
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*预约号*/
	
		public uint? dwResvGroupID;		/*组ID(多时段预约组ID相同，其余为预约ID)*/
	
		public string szResvName;		/*使用用途名称*/
	
		public uint? dwPurpose;		/*预约用途*/
	
		public uint? dwProperty;		/*预约属性（新增需要摄像）*/
	
		public uint? dwActivitySN;		/*活动类型编号*/
	
		public string szActivityName;		/*活动类型名称*/
	
		public string szOrganization;		/*组织*/
	
		public string szOrganiger;		/*组织者*/
	
		public string szHostUnit;		/*主办单位*/
	
		public string szPresenter;		/*主持人*/
	
		public string szDesiredUser;		/*参与者要求*/
	
		public string szContact;		/*联系人*/
	
		public string szTel;		/*联系电话*/
	
		public string szHandPhone;		/*联系手机*/
	
		public string szEmail;		/*联系电子邮箱*/
	
		public uint? dwKind;		/*类型*/
	
		public string szIntroInfo;		/*活动介绍*/
	
		public string szCycRule;		/*预约时间规律描述*/
	
		public uint? dwActivityLevel;		/*活动级别（和管理员级别一致）*/
	
		public uint? dwCheckKinds;		/*审核类型(参考CHECKTYPE定义，可多个）*/
	
		public uint? dwSecurityLevel;		/*安保级别（参考CHECKTYPE，决定是否提交保卫处审核）*/
	
		public uint? dwMinAttendance;		/*最少参加人数（预估）*/
	
		public uint? dwMaxAttendance;		/*最多参加人数（预估）*/
	
		public uint? dwStatus;		/*预约状态(包括审查，是否生效，是否已取消等)*/
	
		public uint? dwPreDate;		/*预约开始日期*/
	
		public uint? dwOccurTime;		/*预约发生时间*/
	
		public uint? dwBeginTime;		/*预约开始时间*/
	
		public uint? dwEndTime;		/*预约结束时间*/
	
		public uint? dwCheckTime;		/*审查时间(当新建预约指定RESVPROP_BYTHIRD时，表示dwThirdResvID)*/
	
		public uint? dwAdvanceCheckTime;		/*提前审查时间*/
	
		public uint? dwDevID;		/*设备ID*/
	
		public uint? dwDevKind;		/*设备类型*/
	
		public uint? dwDevSN;		/*设备编号*/
	
		public string szDevName;		/*设备名称*/
	
		public uint? dwRoomID;		/*房间ID*/
	
		public string szRoomName;		/*房间名称*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabName;		/*实验室名称*/
	
		public uint? dwBuildingID;		/*楼宇ID*/
	
		public string szBuildingNo;		/*大楼编号(*/
	
		public string szBuildingName;		/*大楼名称(*/
	
		public uint? dwCampusID;		/*校区ID*/
	
		public string szCampusName;		/*校区名称*/
	
		public uint? dwDeptID;		/*场馆所属部门ID*/
	
		public string szDeptName;		/*场馆所属部门名称*/
	
		public uint? dwApplicantID;		/*申请人账号*/
	
		public string szApplicantName;		/*申请人姓名*/
	
		public uint? dwUserDeptID;		/*申请人部门ID*/
	
		public string szUserDeptName;		/*申请人部门*/
	
		public uint? dwResvRuleSN;		/*关联预约规则*/
	
		public uint? dwOpenRuleSN;		/*关联开放时间表*/
	
		public uint? dwFeeSN;		/*费率SN*/
	
		public string szApplicationURL;		/*提交的申请材料连接*/
	
		public string szSpareDevIDs;		/*备选设备ID（多个逗号隔开）*/
	
		public string szMemo;		/*说明信息*/
	
		public uint? dwFeedStat;		/*状态*/
	
		public uint? dwFeedKind;		/*反馈类型*/
	
		public uint? dwScore;		/*用户评分*/
	
		public string szFeedInfo;		/*反馈信息*/
	
		public string szReplyInfo;		/*回复信息*/
		};

	/*获取场馆预约审核信息的请求包*/
	
	public struct YARDRESVCHECKINFOREQ
	{
		private Reserved reserved;
		
		public uint? dwCheckID;		/*审核ID*/
	
		public uint? dwResvID;		/*预约号*/
	
		public uint? dwResvGroupID;		/*预约组ID*/
	
		public string szResvName;		/*预约名称*/
	
		public uint? dwCheckDeptID;		/*审核部门ID*/
	
		public uint? dwApplicantID;		/*申请人账号*/
	
		public uint? dwCheckStat;		/*确认状态*/
	
		public uint? dwNeedYardResv;		/*需要获取场馆预约详情*/
	
		public uint? dwBeginDate;		/*预约开始日期*/
	
		public uint? dwEndDate;		/*预约结束日期*/
	
		public uint? dwKind;		/*场馆预约类型*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*场馆预约审核信息*/
	
	public struct YARDRESVCHECKINFO
	{
		private Reserved reserved;
		
		public uint? dwCheckID;		/*审核ID*/
	
		public uint? dwResvID;		/*预约号*/
	
		public uint? dwCheckKind;		/*审核类型*/
	
		public uint? dwCheckLevel;		/*审核级别(同UNIADMIN.dwManLevel定义）*/
	
		public uint? dwCheckDeptID;		/*审核部门ID*/
	
		public uint? dwWaitKind;		/*等待审核类别(可多个）*/
	
		public string szCheckName;		/*审核名称*/
	
		public uint? dwBeforeKind;		/*依赖其他审核类别(可多个）*/
	
		public uint? dwNeedMinTime;		/*审核需要的最短时间*/
	
		public uint? dwCheckStat;		/*管理员审核状态(定义在ADMINCHECK)*/
	
		public string szCheckDetail;		/*审查说明*/
	
		public uint? dwCheckBeginDate;		/*审核开始日期*/
	
		public uint? dwCheckDeadLine;		/*审核截止日期*/
	
		public uint? dwCheckDate;		/*审核日期*/
	
		public uint? dwCheckTime;		/*审核时间*/
	
		public uint? dwAdminID;		/*审核者帐号*/
	
		public string szAdminName;		/*审核者*/
	
		public uint? dwApplicantID;		/*申请人账号*/
	
		public string szApplicantName;		/*申请人姓名*/
	
	public YARDRESV YardResv;		/*CUniStruct[YARDRESV]*/
	
		public string szMemo;		/*说明信息*/
		};

	/*场馆预约审核*/
	
	public struct YARDRESVCHECK
	{
		private Reserved reserved;
		
		public uint? dwCheckID;		/*审核ID*/
	
		public uint? dwResvID;		/*预约号*/
	
		public uint? dwCheckKind;		/*审核类型*/
	
		public uint? dwCheckStat;		/*管理员审核状态(定义在ADMINCHECK)*/
	
		public string szCheckDetail;		/*审查说明*/
	
	public YARDRESV YardResv;		/*CUniStruct[YARDRESV]*/
	
		public string szMemo;		/*说明信息*/
		};

	/*获取预约审核信息的请求包*/
	
	public struct RESVCHECKINFOREQ
	{
		private Reserved reserved;
		
		public uint? dwCheckID;		/*审核ID*/
	
		public uint? dwResvID;		/*预约号*/
	
		public uint? dwCheckDeptID;		/*审核部门ID*/
	
		public uint? dwApplicantID;		/*申请人账号*/
	
		public uint? dwCheckStat;		/*确认状态*/
	
		public uint? dwNeedResv;		/*需要获取预约详情*/
	
		public uint? dwBeginDate;		/*预约开始日期*/
	
		public uint? dwEndDate;		/*预约结束日期*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*预约审核信息*/
	
	public struct RESVCHECKINFO
	{
		private Reserved reserved;
		
		public uint? dwCheckID;		/*审核ID*/
	
		public uint? dwResvID;		/*预约号*/
	
		public uint? dwCheckKind;		/*审核类型*/
	
		public uint? dwCheckLevel;		/*审核级别(同UNIADMIN.dwManLevel定义）*/
	
		public uint? dwCheckDeptID;		/*审核部门ID*/
	
		public uint? dwWaitKind;		/*等待审核类别(可多个）*/
	
		public string szCheckName;		/*审核名称*/
	
		public uint? dwBeforeKind;		/*依赖其他审核类别(可多个）*/
	
		public uint? dwNeedMinTime;		/*审核需要的最短时间*/
	
		public uint? dwCheckStat;		/*管理员审核状态(定义在ADMINCHECK)*/
	
		public string szCheckDetail;		/*审查说明*/
	
		public uint? dwCheckBeginDate;		/*审核开始日期*/
	
		public uint? dwCheckDeadLine;		/*审核截止日期*/
	
		public uint? dwCheckDate;		/*审核日期*/
	
		public uint? dwCheckTime;		/*审核时间*/
	
		public uint? dwAdminID;		/*审核者帐号*/
	
		public string szAdminName;		/*审核者*/
	
		public uint? dwApplicantID;		/*申请人账号*/
	
		public string szApplicantName;		/*申请人姓名*/
	
	public UNIRESERVE ResvInfo;		/*CUniStruct[UNIRESERVE]*/
	
		public string szMemo;		/*说明信息*/
		};

	/*预约审核*/
	
	public struct RESVCHECK
	{
		private Reserved reserved;
		
		public uint? dwCheckID;		/*审核ID*/
	
		public uint? dwResvID;		/*预约号*/
	
		public uint? dwCheckKind;		/*审核类型*/
	
		public uint? dwCheckStat;		/*管理员审核状态(定义在ADMINCHECK)*/
	
		public string szCheckDetail;		/*审查说明*/
	
	public UNIRESERVE ResvInfo;		/*CUniStruct[UNIRESERVE]*/
	
		public string szMemo;		/*说明信息*/
		};

	/*获取场馆活动的请求包*/
	
	public struct YARDACTIVITYREQ
	{
		private Reserved reserved;
		
		public uint? dwActivitySN;		/*活动类型编号*/
	
		public uint? dwActivityLevel;		/*活动级别（和管理员级别一致）*/
	
		public uint? dwCheckKinds;		/*审核类型(参考CHECKTYPE定义，可多个）*/
	
		public uint? dwSecurityLevel;		/*安保级别（参考CHECKTYPE，决定是否提交保卫处审核）*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*场馆活动可用设备类型*/
	
	public struct YADEVKIND
	{
		private Reserved reserved;
		
		public uint? dwActivitySN;		/*活动类型编号*/
	
		public uint? dwKindID;		/*设备类型ID*/
		};

	/*场馆活动*/
	
	public struct YARDACTIVITY
	{
		private Reserved reserved;
		
		public uint? dwActivitySN;		/*活动类型编号*/
	
		public string szActivityName;		/*活动类型名称*/
	
		public uint? dwActivityLevel;		/*活动级别（和管理员级别一致）*/
	
		public uint? dwCheckKinds;		/*审核类型(参考CHECKTYPE定义，可多个）*/
	
		public string szCheckNames;		/*审核类型名称,多个用逗号隔开*/
	
		public uint? dwSecurityLevel;		/*安保级别（参考CHECKTYPE，决定是否提交保卫处审核）*/
	
		[FlagsAttribute]
		public enum DWSECURITYLEVEL : uint
		{
			
				[EnumDescription("不需要审核")]
				SECLEVEL_NONEED = 0x1,
			
				[EnumDescription("需要审核")]
				SECLEVEL_CHECK = 0x2,
			
				[EnumDescription("需要保卫处帮助")]
				SECLEVEL_HELP = 0x4,
			
		}

	
	public YADEVKIND[] UsableDevKind;		/*CUniTable[YADEVKIND]*/
	
		public string szMemo;		/*说明信息*/
		};

	/*第三方预约设备*/
	
	public struct TRESVDEV
	{
		private Reserved reserved;
		
		public string szAssertSN;		/*资产编号*/
		};

	/*第三方预约时间表*/
	
	public struct TRESVTIME
	{
		private Reserved reserved;
		
		public uint? dwResvDate;		/*预约日期*/
	
		public uint? dwStartHM;		/*预约开始时间*/
	
		public uint? dwEndHM;		/*预约结束时间*/
		};

	/*第三方预约共享设备*/
	
	public struct THIRDRESVSHAREDEV
	{
		private Reserved reserved;
		
		public uint? dwThirdResvID;		/*第三方预约ID*/
	
		public string szResvTitle;		/*预约标题*/
	
	public TRESVDEV[] DevTbl;		/*预约设备表*/
	
	public TRESVTIME[] TimeTbl;		/*预约时间表*/
		};

	/*第三方删除预约*/
	
	public struct THIRDRESVDEL
	{
		private Reserved reserved;
		
		public uint? dwThirdResvID;		/*第三方预约ID*/
		};

	/*获取第三方预约的请求包*/
	
	public struct THIRDRESVREQ
	{
		private Reserved reserved;
		
		public uint? dwThirdResvID;		/*第三方预约ID*/
	
		public string szPID;		/*学工号*/
	
		public uint? dwStatus;		/*状态*/
	
		public uint? dwBeginDate;		/*预约开始日期*/
	
		public uint? dwEndDate;		/*预约结束日期*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*第三方预约*/
	
	public struct THIRDRESV
	{
		private Reserved reserved;
		
		public uint? dwThirdResvID;		/*第三方预约ID*/
	
		public string szResvTitle;		/*预约标题*/
	
		public uint? dwResvDate;		/*预约日期*/
	
		public uint? dwStartHM;		/*预约开始时间*/
	
		public uint? dwEndHM;		/*预约结束时间*/
	
		public string szOrganization;		/*组织*/
	
		public string szOrganiger;		/*组织者*/
	
		public string szHostUnit;		/*主办单位*/
	
		public string szPresenter;		/*主持人*/
	
		public string szDesiredUser;		/*参与者要求*/
	
		public string szIntroInfo;		/*活动介绍*/
	
		public string szPID;		/*申请人学工号*/
	
		public string szTrueName;		/*申请人姓名*/
	
		public string szTel;		/*联系电话*/
	
		public string szHandPhone;		/*联系手机*/
	
		public string szEmail;		/*联系电子邮箱*/
	
		public uint? dwMinAttendance;		/*最少参加人数（预估）*/
	
		public uint? dwMaxAttendance;		/*最多参加人数（预估）*/
	
		public uint? dwStatus;		/*预约状态((0表示未预约，包括审查，是否生效，是否已取消等)*/
	
		public string szAssertSN;		/*资产编号*/
	
		public uint? dwResvID;		/*预约号*/
	
		public string szMemo;		/*说明信息*/
		};

	/*设备预约信息表*/
	
	public struct DEVICERESV
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*设备ID*/
	
		public uint? dwResvFrom;		/*来源*/
	
		[FlagsAttribute]
		public enum DWRESVFROM : uint
		{
			
				[EnumDescription("来自本系统")]
				RESVFROM_SYS = 0x1,
			
				[EnumDescription("来自第三方系统")]
				RESVFROM_THIRD = 0x100,
			
		}

	
		public uint? dwResvID;		/*预约ID*/
	
		public uint? dwResvDate;		/*预约日期*/
	
		public uint? dwStartHM;		/*预约开始时间*/
	
		public uint? dwEndHM;		/*预约结束时间*/
	
		public uint? dwResvMin;		/*预约时间*/
	
		public uint? dwAccNo;		/*预约人帐号*/
	
		public uint? dwSex;		/*预约人性别*/
	
		public string szPID;		/*申请人学工号*/
	
		public string szTrueName;		/*申请人姓名*/
	
		public string szMemberName;		/*组名*/
	
		public string szResvTitle;		/*预约标题*/
	
		public string szMemo;		/*说明信息*/
		};

	/*结束数据结构*/

	/*开始数据结构*/

	/*获取监控分类库的请求包*/
	
	public struct CTRLCLASSREQ
	{
		private Reserved reserved;
		
		public uint? dwCtrlSN;		/*监控分类库编号*/
	
		public uint? dwCtrlKind;		/*控制分类*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*监控分类库结构*/
	
	public struct UNICTRLCLASS
	{
		private Reserved reserved;
		
		public uint? dwCtrlSN;		/*监控分类库编号*/
	
		public uint? dwCtrlKind;		/*控制分类*/
	
		[FlagsAttribute]
		public enum DWCTRLKIND : uint
		{
			
				[EnumDescription("网址")]
				CTRLKIND_URL = 0x1,
			
				[EnumDescription("软件（程序）")]
				CTRLKIND_SW = 0x2,
			
		}

	
		public uint? dwCtrlLevel;		/*控制分类级别，可自定义*/
	
		public string szCtrlName;		/*监控分类库名称*/
	
		public uint? dwCtrlMode;		/*控制方式*/
	
		[FlagsAttribute]
		public enum DWCTRLMODE : uint
		{
			
				[EnumDescription("全部允许(不限制)")]
				CTRLMODE_NOLIMIT = 0,
			
				[EnumDescription("禁止")]
				CTRLMODE_FORBID = 1,
			
				[EnumDescription("允许")]
				CTRLMODE_PERMIT = 2,
			
				[EnumDescription("全部禁止")]
				CTRLMODE_FORBIDALL = 4,
			
				[EnumDescription("禁止或允许到指定级别")]
				CTRLMODE_LEVEL = 0x100,
			
				[EnumDescription("禁止或允许到指定组及以内级别")]
				CTRLMODE_CLASSLEVEL = 0x200,
			
				[EnumDescription("只禁止或允许指定组")]
				CTRLMODE_CLASS = 0x400,
			
		}

	
		public uint? dwForAges;		/*适用年龄段(FFTT 0713表示7-13周岁)*/
	
		public string szMemo;		/*备注*/
		};

	/*获取网址组的请求包*/
	
	public struct CTRLURLREQ
	{
		private Reserved reserved;
		
		public uint? dwCtrlLevel;		/*控制分类级别，可自定义*/
	
		public uint? dwClassSN;		/*监控分类库编号*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*网址组结构*/
	
	public struct UNICTRLURL
	{
		private Reserved reserved;
		
		public uint? dwClassSN;		/*监控分类库编号*/
	
		public uint? dwCtrlLevel;		/*控制分类级别，可自定义*/
	
		public string szCtrlName;		/*网址组名称*/
	
		public uint? dwCtrlMode;		/*控制方式*/
	
		public uint? dwForAges;		/*适用年龄段(FFTT 0713表示7-13周岁)*/
	
		public uint? dwID;		/*网址ID*/
	
		public string szURL;		/*URL(支持通配符)*/
	
		public uint? dwPort;		/*端口*/
	
		public uint? dwStatus;		/*状态*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("无效")]
				URLSTAT_UNVALID = 0,
			
				[EnumDescription("未审查")]
				URLSTAT_UNCHECK = 1,
			
				[EnumDescription("已审查")]
				URLSTAT_CHECKED = 2,
			
		}

	
		public string szMemo;		/*备注*/
		};

	/*获取软件组的请求包*/
	
	public struct CTRLSWREQ
	{
		private Reserved reserved;
		
		public uint? dwCtrlLevel;		/*控制分类级别，可自定义*/
	
		public uint? dwClassSN;		/*监控分类库编号*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*软件组结构*/
	
	public struct UNICTRLSW
	{
		private Reserved reserved;
		
		public uint? dwClassSN;		/*监控分类库编号*/
	
		public uint? dwCtrlLevel;		/*控制分类级别，可自定义*/
	
		public string szCtrlName;		/*软件组名称*/
	
		public uint? dwCtrlMode;		/*控制方式*/
	
		public uint? dwForAges;		/*适用年龄段(FFTT 0713表示7-13周岁)*/
	
		public uint? dwID;		/*key*/
	
		public string szName;		/*软件组成员名称*/
	
		public uint? dwMemberID;		/*根据不同类别表示不同含义*/
	
		public uint? dwKind;		/*成员类型*/
	
		[FlagsAttribute]
		public enum DWKIND : uint
		{
			
				[EnumDescription("程序")]
				CSWKIND_PROGRAM = 0x2,
			
				[EnumDescription("软件类别")]
				CSWKIND_SWCLASS = 0x4,
			
		}

	
		public uint? dwStatus;		/*状态*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("无效")]
				SWSTAT_UNVALID = 0,
			
				[EnumDescription("未审查")]
				SWSTAT_UNCHECK = 1,
			
				[EnumDescription("已审查")]
				SWSTAT_CHECKED = 2,
			
		}

	
		public string szMemo;		/*备注*/
		};

	/*获取软件的请求包*/
	
	public struct SOFTWAREREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*方式*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("全部")]
				SOFTWAREGET_BYALL = 0,
			
				[EnumDescription("软件ID")]
				SOFTWAREGET_BYID = 1,
			
				[EnumDescription("软件名(支持通配)")]
				SOFTWAREGET_BYNAME = 2,
			
				[EnumDescription("所属类别")]
				SOFTWAREGET_BYKIND = 4,
			
				[EnumDescription("更新标志")]
				SOFTWAREGET_BYCHGFLAG = 8,
			
		}

	
		public string szGetKey;		/*条件值*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*软件结构*/
	
	public struct UNISOFTWARE
	{
		private Reserved reserved;
		
		public uint? dwSWID;		/*软件ID*/
	
		public uint? dwKind;		/*成员类型*/
	
		[FlagsAttribute]
		public enum DWKIND : uint
		{
			
				[EnumDescription("系统软件")]
				SWKIND_OS = 0x10000,
			
				[EnumDescription("应用软件")]
				SWKIND_APP = 0x20000,
			
				[EnumDescription("工具软件")]
				SWKIND_TOOL = 0x40000,
			
				[EnumDescription("游戏软件")]
				SWKIND_GAME = 0x80000,
			
				[EnumDescription("其他(未分类)")]
				SWKIND_OTHER = 0x100000,
			
				[EnumDescription("管理人员安装")]
				SWKIND_INSTALLED = 0x10000000,
			
		}

	
		public string szSWName;		/*软件名称*/
	
		public string szSWVersion;		/*软件版本*/
	
		public string szSWCompany;		/*公司*/
	
		public string szDispSWName;		/*显示产品名称，可修改*/
	
		public string szDispSWCompany;		/*显示公司名称，可修改*/
	
		public uint? dwFrom;		/*来自哪里（同步层次）*/
	
		public uint? dwChgFlag;		/*修改更新标志*/
	
		public string szMemo;		/*备注*/
		};

	/*获取程序的请求包*/
	
	public struct PROGRAMREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*方式*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("全部")]
				PROGRAMGET_BYALL = 0,
			
				[EnumDescription("程序ID")]
				PROGRAMGET_BYID = 1,
			
				[EnumDescription("程序名(支持通配)")]
				PROGRAMGET_BYNAME = 2,
			
				[EnumDescription("更新标志")]
				PROGRAMGET_BYCHGFLAG = 8,
			
				[EnumDescription("软件ID")]
				PROGRAMGET_BYSWID = 16,
			
				[EnumDescription("软件名(支持通配)")]
				PROGRAMGET_BYSWNAME = 32,
			
		}

	
		public string szGetKey;		/*条件值*/
	
		public uint? dwKind;		/*成员类型*/
	
		public uint? dwProperty;		/*程序属性*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*程序结构*/
	
	public struct UNIPROGRAM
	{
		private Reserved reserved;
		
		public uint? dwID;		/*程序ID*/
	
		public uint? dwSubID;		/*副ID*/
	
		public uint? dwSWID;		/*所属软件ID*/
	
		public uint? dwKind;		/*所属类别*/
	
		public uint? dwProperty;		/*程序属性*/
	
		[FlagsAttribute]
		public enum DWPROPERTY : uint
		{
			
				[EnumDescription("主程序")]
				PGPROP_MAIN = 1,
			
				[EnumDescription("辅助程序")]
				PGPROP_ASSIST = 2,
			
		}

	
		public string szProductName;		/*产品名称*/
	
		public string szExeName;		/*Exe文件名*/
	
		public string szSWVersion;		/*程序版本*/
	
		public string szDispProductName;		/*显示程序名称，可修改*/
	
		public string szDispSWName;		/*显示产品名称，可修改*/
	
		public string szDispSWCompany;		/*显示公司名称，可修改*/
	
		public uint? dwFrom;		/*来自哪里（同步层次）*/
	
		public uint? dwChgFlag;		/*修改更新标志*/
	
		public string szIcon;		/*图标*/
	
		public string szMemo;		/*备注*/
		};

	/*获取机器程序的请求包*/
	
	public struct PCSWINFOREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*方式*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("机器ID")]
				PCSWGET_BYPCID = 1,
			
		}

	
		public string szGetKey;		/*条件值*/
	
		public uint? dwKind;		/*软件类型*/
	
		public uint? dwProperty;		/*程序属性*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*机器程序结构*/
	
	public struct UNIPCSWINFO
	{
		private Reserved reserved;
		
		public string szProgramInfo;		/*CUniStruct(<UNIPROGRAM>)*/
	
		public uint? dwPCID;		/*机器ID*/
	
		public string szInstName;		/*安装名称*/
	
		public string szInstPath;		/*安装路径*/
	
		public uint? dwRunLatestDate;		/*最近运行日期*/
	
		public uint? dwRunTimes;		/*运行次数*/
	
		public uint? dwRunMinutes;		/*累计运行分钟数*/
	
		public string szMemo;		/*备注*/
		};

	/*获取机房程序的请求包*/
	
	public struct ROOMSWINFOREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*方式*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("机房ID，获取该机房下都安装的软件")]
				ROOMSWGET_BYID = 1,
			
		}

	
		public string szGetKey;		/*条件值*/
	
		public uint? dwKind;		/*软件类型*/
	
		public uint? dwProperty;		/*程序属性*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*机房程序结构*/
	
	public struct UNIROOMSWINFO
	{
		private Reserved reserved;
		
		public string szProgramInfo;		/*CUniStruct(<UNIPROGRAM>)*/
	
		public uint? dwRoomID;		/*机房ID*/
	
		public uint? dwInstSWNum;		/*安装该软件机器数*/
	
		public uint? dwRunTimes;		/*运行次数*/
	
		public uint? dwRunMinutes;		/*累计运行分钟数*/
	
		public string szMemo;		/*备注*/
		};

	/*机器程序结构（上传用）*/
	
	public struct PCPROGRAM
	{
		private Reserved reserved;
		
		public string szProgramInfo;		/*CUniStruct(<UNIPROGRAM>)*/
	
		public uint? dwDevID;		/*客户端设备的ID号*/
	
		public uint? dwLabID;		/*实验室的ID号*/
	
		public uint? dwPID;		/*进程ID*/
	
		public string szInstName;		/*安装名称*/
	
		public string szInstPath;		/*安装路径*/
	
		public string szMemo;		/*备注*/
		};

	/*机器程序结束信息（上传用）*/
	
	public struct PROGEND
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*客户端设备的ID号*/
	
		public uint? dwLabID;		/*实验室的ID号*/
	
		public uint? dwProcID;		/*程序的ID号*/
	
		public uint? dwPID;		/*进程ID*/
		};

	/*退出程序信息*/
	
	public struct QUITAPPINFO
	{
		private Reserved reserved;
		
		public uint? dwProcID;		/*程序ID*/
	
		public string szMemo;		/*备注*/
		};

	/*网址信息*/
	
	public struct URLCHECKINFO
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*客户端设备的ID号*/
	
		public uint? dwLabID;		/*实验室的ID号*/
	
		public uint? dwAccNo;		/*帐号*/
	
		public uint? dwRemoteIp;		/*访问IP*/
	
		public uint? dwPort;		/*访问端口*/
	
		public string szDomainName;		/*域名*/
	
		public string szURL;		/*网址*/
	
		public string szMemo;		/*备注*/
		};

	/*结束数据结构*/

	/*开始数据结构*/

	/*登录请求包*/
	
	public struct THIRDLOGINREQ
	{
		private Reserved reserved;
		
		public uint? dwSysID;		/*系统编号*/
	
		public string szVersion;		/*版本*/
	
		public string szExtInfo;		/*扩展信息*/
		};

	/*登录应答包*/
	
	public struct THIRDLOGINRES
	{
		private Reserved reserved;
		
		public string szVersion;		/*版本*/
	
		public string szExtInfo;		/*扩展信息*/
		};

	/*获取账户列表输入参数*/
	
	public struct THIRDACCREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*方式*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("获取所有帐户信息")]
				ACCGET_BYALL = 0x100,
			
				[EnumDescription("差异同步")]
				ACCGET_BYSYNC = 0x200,
			
		}

	
		public string szParam;		/*参数*/
		};

	/*同步帐户请求包*/
	
	public struct SYNCACCREQ
	{
		private Reserved reserved;
		
		public uint? dwType;		/*同步方式*/
	
		[FlagsAttribute]
		public enum DWTYPE : uint
		{
			
				[EnumDescription("同步帐户")]
				SYNCTYPE_DO = 0x1,
			
				[EnumDescription("查询同步帐户状态")]
				SYNCTYPE_QUERY = 0x2,
			
		}

	
		public string szMemo;		/*扩展信息*/
		};

	/*获取最新帐户信息状态*/
	
	public struct SYNCACCINFO
	{
		private Reserved reserved;
		
		public uint? dwStatus;		/*当前状态*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("准备做同步")]
				SYNCSTAT_PREPARE = 0x0,
			
				[EnumDescription("等待接口服务器返回")]
				SYNCSTAT_WAITING = 0x1,
			
				[EnumDescription("正在处理返回结果")]
				SYNCSTAT_DOING = 0x2,
			
				[EnumDescription("处理结束")]
				SYNCSTAT_DONE = 0x4,
			
				[EnumDescription("出错")]
				SYNCSTAT_ERROR = SYNCSTAT_DONE+0x08 ,
			
		}

	
		public uint? dwStartTime;		/*开始时间(time函数)*/
	
		public uint? dwUseTime;		/*已用时间(秒)*/
	
		public uint? dwEstmateTime;		/*估计总所需时间(秒)*/
	
		public uint? dwTotalAcc;		/*总用户数*/
	
		public uint? dwDealAcc;		/*已处理完成用户数*/
	
		public uint? dwDiffAcc;		/*信息与本地不同用户数*/
	
		public string szInfo;		/*扩展信息*/
		};

	/*结束数据结构*/

	/*开始数据结构*/

	/**/
	
	public struct PERIODFEE
	{
		private Reserved reserved;
		
		public uint? dwPStart;		/*时段开始时间*/
	
		public uint? dwPEnd;		/*时段结束时间*/
	
		public uint? dwPUnitFee;		/*时段单位费率*/
	
		public uint? dwPAssistFee;		/*时段管理员指导费*/
		};

	/**/
	
	public struct FEEREQ
	{
		private Reserved reserved;
		
		public uint? dwFeeSN;		/*费率SN*/
	
		public uint? dwIdent;		/*身份（0表示无限制）*/
	
		public uint? dwDeptID;		/*部门（0表示无限制）*/
	
		public uint? dwDevKind;		/*设备类型（0表示无限制）*/
	
		public uint? dwGroupID;		/*指定用户组（0表示无限制）*/
	
		public uint? dwPurpose;		/*用途*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/**/
	
	public struct RTDEVFEEREQ
	{
		private Reserved reserved;
		
		public uint? dwRTID;		/*科研实验项目ID*/
	
		public uint? dwDevID;		/*设备ID*/
		};

	/*收费标准详细信息*/
	
	public struct FEEDETAIL
	{
		private Reserved reserved;
		
		public uint? dwFeeType;		/*收费类别*/
	
		[FlagsAttribute]
		public enum DWFEETYPE : uint
		{
			
				[EnumDescription("使用费（使用后生成的账单）")]
				FEETYPE_USEDEV = 1,
			
				[EnumDescription("占用费")]
				FEETYPE_OCCUPY = 2,
			
				[EnumDescription("协助费")]
				FEETYPE_ASSIST = 3,
			
				[EnumDescription("超时费")]
				FEETYPE_TIMEOUT = 4,
			
				[EnumDescription("赔偿金")]
				FEETYPE_DAMAGE = 5,
			
				[EnumDescription("罚金")]
				FEETYPE_FINE = 6,
			
				[EnumDescription("样品费")]
				FEETYPE_SAMPLE = 7,
			
				[EnumDescription("预约使用设备费（需缴费后方可使用）")]
				FEETYPE_RESVDEV = 8,
			
				[EnumDescription("代检费")]
				FEETYPE_ENTRUST = 9,
			
				[EnumDescription("协议收费")]
				FEETYPE_NEGOTIATION = 10,
			
		}

	
		public uint? dwUsablePayKind;		/*可用缴费方式(见UNIBILL定义)*/
	
		public uint? dwDefaultCheckStat;		/*CHECKINFO定义的管理员审核状态*/
	
		public uint? dwUnitFee;		/*单位使用费率(小时 缺省100)*/
	
		public uint? dwUnitTime;		/*单位时间(缺省1)*/
	
		public uint? dwRoundOff;		/*舍入分界点(小于单位时间)*/
	
		public uint? dwIgnoreTime;		/*不计费时间(缺省0,正表示不计费时间，负表示最少使用时间)*/
	
		public uint? dwHolidayCoef;		/*假日系数*/
	
		public string szPosInfo;		/*与一卡通对应的商户信息*/
	
		public string szMemo;		/*说明信息*/
		};

	/*收费标准详细信息*/
	
	public struct UNIFEE
	{
		private Reserved reserved;
		
		public uint? dwFeeSN;		/*费率SN*/
	
		public string szFeeName;		/*名称*/
	
		public uint? dwPriority;		/*优先级(数字大代表优先级高)*/
	
		public uint? dwIdent;		/*身份（0表示无限制）*/
	
		public uint? dwDeptID;		/*部门（0表示无限制）*/
	
		public uint? dwDevKind;		/*设备类型（0表示无限制）*/
	
		public uint? dwGroupID;		/*指定用户组（0表示无限制）*/
	
		public uint? dwPurpose;		/*预约用途*/
	
		public uint? dwOverDraft;		/*允许透支额*/
	
		public uint? dwMinInTime;		/*允许进入该用户最低可用时间*/
	
		public uint? dwQuotaRule;		/*限制规则(日累计，次累计，机器忙等(缺省0))*/
	
		[FlagsAttribute]
		public enum DWQUOTARULE : uint
		{
			
				[EnumDescription("日累计")]
				FEEQUOTA_BYDAY = 0x1,
			
				[EnumDescription("次累计")]
				FEEQUOTA_BYTIMES = 0x2,
			
				[EnumDescription("机器忙有效")]
				FEEQUOTA_ONLYBUSY = 0x10000,
			
		}

	
		public uint? dwQuotaTime;		/*限制使用时间(缺省-1)*/
	
	public FEEDETAIL[] szFeeDetail;		/*收费标准明细表CUniTable[FEEDETAIL]*/
	
		public string szMemo;		/*说明信息*/
		};

	/**/
	
	public struct RTDEVSAMPLEREQ
	{
		private Reserved reserved;
		
		public uint? dwRTID;		/*科研实验项目ID*/
	
		public uint? dwDevID;		/*设备ID*/
		};

	/*科研项目对应的设备的样品及费率表*/
	
	public struct RTDEVSAMPLE
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*设备ID*/
	
		public uint? dwSampleSN;		/*样品编号*/
	
		public string szSampleName;		/*样品名称*/
	
		public string szUnitName;		/*计费单位*/
	
		public uint? dwUnitFee;		/*单价*/
	
		public string szMemo;		/*说明信息*/
		};

	/*获取账单请求*/
	
	public struct BILLREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*方式*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("全部")]
				BILLGET_BYALL = 0,
			
				[EnumDescription("SID")]
				BILLGET_BYSID = 1,
			
				[EnumDescription("消费SID")]
				BILLGET_BYCOSTSID = 2,
			
				[EnumDescription("账号")]
				BILLGET_BYACCNO = 3,
			
				[EnumDescription("实验室ID")]
				BILLGET_BYLABID = 4,
			
				[EnumDescription("设备类型")]
				BILLGET_BYDEVKIND = 5,
			
				[EnumDescription("预约号")]
				BILLGET_BYRESVID = 6,
			
		}

	
		public string szGetKey;		/*条件值*/
	
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
		public uint? dwFeeType;		/*收费类别(FEEDETAIL定义)*/
	
		public uint? dwPayKind;		/*缴费方式*/
	
		public uint? dwStatus;		/*状态*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*用户账单*/
	
	public struct UNIBILL
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*流水号*/
	
		public uint? dwResvID;		/*预约号*/
	
		public uint? dwCostSID;		/*使用流水号*/
	
		public string szPosInfo;		/*与一卡通对应的商户信息*/
	
		public uint? dwAccNo;		/*帐号*/
	
		public string szPID;		/*学工号*/
	
		public string szTrueName;		/*姓名*/
	
		public uint? dwDevKind;		/*设备类型*/
	
		public string szKindName;		/*设备名称*/
	
		public string szModel;		/*设备型号*/
	
		public string szSpecification;		/*设备规格*/
	
		public uint? dwFeeType;		/*收费类别(FEEDETAIL定义)*/
	
		public uint? dwBeginTime;		/*开始时间*/
	
		public uint? dwEndTime;		/*结束时间*/
	
		public uint? dwUnitFee;		/*费率*/
	
		public uint? dwUnitTime;		/*单位时间*/
	
		public uint? dwRoundOff;		/*舍入分界点(小于单位时间)*/
	
		public uint? dwIgnoreTime;		/*不计费时间*/
	
		public uint? dwHolidayCoef;		/*假日系数*/
	
		public uint? dwUseTime;		/*使用时间*/
	
		public uint? dwFeeTime;		/*计费时间*/
	
		public uint? dwCostMoney;		/*应缴费用*/
	
		public uint? dwCostSubsidy;		/*补助*/
	
		public uint? dwCostFreeTime;		/*机时*/
	
		public uint? dwRealCost;		/*实际缴纳费用*/
	
		public uint? dwUsablePayKind;		/*可用缴费方式*/
	
		[FlagsAttribute]
		public enum DWUSABLEPAYKIND : uint
		{
			
				[EnumDescription("一卡通")]
				PAYKIND_ONECARD = 0x1000000,
			
				[EnumDescription("预存现金")]
				PAYKIND_STOREDCASH = 0x2000000,
			
				[EnumDescription("代收现金")]
				PAYKIND_DIRECTCASH = 0x4000000,
			
				[EnumDescription("使用补助")]
				PAYKIND_SUBSIDY = 0x8000000,
			
				[EnumDescription("校内支票")]
				PAYKIND_CHECK = 0x10000000,
			
		}

	
		public uint? dwUsedPayKind;		/*实际缴费方式*/
	
		public uint? dwStatus;		/*CHECKINFO定义的管理员审核状态+如下定义*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("未支付")]
				BILLSTAT_UNPAID = 0x100,
			
				[EnumDescription("已支付")]
				BILLSTAT_PAID = 0x200,
			
				[EnumDescription("已结算")]
				BILLSTAT_CHECKOUT = 0x400,
			
				[EnumDescription("不能自动支付，需本人通过自助缴费台或缴费点支付")]
				BILLSTAT_NOAUTO = 0x800,
			
		}

	
		public uint? dwBillDate;		/*账单日期*/
	
		public uint? dwBillTime;		/*账单时间*/
	
		public uint? dwAuditorID;		/*审核员*/
	
		public uint? dwTollID;		/*收费员或一卡通tblThirdSyncCost的流水号*/
	
		public string szMemo;		/*说明信息*/
		};

	/*账单缴费*/
	
	public struct BILLPAY
	{
		private Reserved reserved;
		
		public uint? dwPayKind;		/*缴费方式*/
	
		public uint? dwTotalCost;		/*缴费合计*/
	
		public uint? dwOneCardSID;		/*一卡通流水号*/
	
		public string szCardCostInfo;		/*卡通卡扣费信息，不同的一卡通格式和内容都不同*/
	
	public UNIBILL[] szBillInfo;		/*CUniTable[UNIBILL]*/
	
		public string szMemo;		/*说明信息*/
		};

	/*机时使用规则请求*/
	
	public struct FTRULEREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*方式*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("全部")]
				FTRULEGET_BYALL = 0,
			
				[EnumDescription("FTRuleSN")]
				FTRULEGET_BYSN = 1,
			
				[EnumDescription("机时类别")]
				FTRULEGET_BYFTTYPE = 2,
			
				[EnumDescription("专业")]
				FTRULEGET_BYMAJOR = 3,
			
		}

	
		public string szGetKey;		/*条件值*/
	
		public string szSubKey;		/*选择专业方式时可设置为入学年份*/
		};

	/*机时使用规则*/
	
	public struct FREETIMERULE
	{
		private Reserved reserved;
		
		public uint? dwSN;		/*机时规则SN*/
	
		public string szName;		/*名称*/
	
		public uint? dwFTType;		/*机时类别*/
	
		public uint? dwMajorID;		/*专业*/
	
		public string szMajorName;		/*专业名称*/
	
		public uint? dwEnrolYear;		/*入学年份*/
	
		public uint? dwPeriod;		/*周期（学期，学年，整个大学期间）*/
	
		[FlagsAttribute]
		public enum DWPERIOD : uint
		{
			
				[EnumDescription("学期")]
				FTPERIOD_TERM = 1,
			
				[EnumDescription("学年")]
				FTPERIOD_YEAR = 2,
			
				[EnumDescription("进校直至毕业")]
				FTPERIOD_GRADUATE = 3,
			
		}

	
		public uint? dwPlanFT;		/*计划总时间*/
	
		public uint? dwDayLimit;		/*每日使用限额*/
	
		public uint? dwPlanUseTimes;		/*计划总使用次数*/
	
		public string szMemo;		/*说明信息*/
		};

	/*结束数据结构*/

	/*开始数据结构*/

	/*获取控制台的请求包*/
	
	public struct CONREQ
	{
		private Reserved reserved;
		
		public uint? dwConsoleSN;		/*控制台编号*/
	
		public string szConsoleName;		/*控制台名称*/
	
		public uint? dwKind;		/*控制台类型*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*控制台信息*/
	
	public struct UNICONSOLE
	{
		private Reserved reserved;
		
		public uint? dwConsoleSN;		/*控制台编号*/
	
		public string szConsoleName;		/*控制台名称*/
	
		public uint? dwKind;		/*控制台类型*/
	
		[FlagsAttribute]
		public enum DWKIND : uint
		{
			
				[EnumDescription("触控一体机（刷卡认证，现场预约和信息发布）")]
				CONKIND_TOUCHONE = 1,
			
				[EnumDescription("显示大屏(不能交互，只能发布信息）")]
				CONKIND_DISPLAY = 2,
			
				[EnumDescription("教师端(允许教师助手软件)")]
				CONKIND_TEACHER = 4,
			
				[EnumDescription("外借端")]
				CONKIND_LOAN = 8,
			
				[EnumDescription("值班台")]
				CONKIND_ATTENDANT = 0x10,
			
				[EnumDescription("通道机")]
				CONKIND_AUTOGATE = 0x20,
			
				[EnumDescription("预约台")]
				CONKIND_RESERVE = 0x1000,
			
				[EnumDescription("有登录认证的设备（电脑）预约")]
				CONKIND_LOGINRESV = 0x2000,
			
				[EnumDescription("账单结算")]
				CONKIND_BILLCHECKOUT = 0x4000,
			
				[EnumDescription("预约空间")]
				CONKIND_COMMONS = 0x100000,
			
				[EnumDescription("预约电脑")]
				CONKIND_COMPUTER = 0x200000,
			
				[EnumDescription("预约座位")]
				CONKIND_SEAT = 0x400000,
			
				[EnumDescription("与通道机集成(必须通道机刷卡进入后方可使用)")]
				CONKIND_WITHAG = 0x800000,
			
		}

	
		public uint? dwStatus;		/*控制台状态（参考CommonIF.xmlCONSTAT_XXX定义)*/
	
		public uint? dwOpenTime;		/*开始时间*/
	
		public uint? dwCloseTime;		/*关闭时间*/
	
		public string szIP;		/*IP地址*/
	
		public string szManRooms;		/*管理房间(房间编号，可多个，逗号隔开)*/
	
		public string szDispInfoURL;		/*显示信息连接*/
	
		public string szLocation;		/*控制台存放位置*/
	
		public string szMemo;		/*说明信息*/
	
	public MODMONI MoniInfo;		/*监控信息*/
		};

	/*控制台登录请求*/
	
	public struct CONLOGINREQ
	{
		private Reserved reserved;
		
		public string szVersion;		/*版本	XX.XX.XXXXXXXX*/
	
		public uint? dwStaSN;		/*站点编号*/
	
		public string szIP;		/*IP地址*/
	
		public string szMemo;		/*说明信息*/
		};

	/*控制台登录响应*/
	
	public struct CONLOGINRES
	{
		private Reserved reserved;
		
		public uint? dwSessionID;		/*服务器分配的SessionID*/
	
	public UNIVERSION SrvVer;		/*UNIVERSION 结构*/
	
		public string szCurTime;		/*服务器时间 YYYY-MM-DD HH:MM:SS*/
	
		public uint? dwConsoleSN;		/*控制台编号*/
	
		public string szConsoleName;		/*控制台名称*/
	
		public uint? dwKind;		/*控制台类型*/
	
		public uint? dwOpenTime;		/*开门时间*/
	
		public uint? dwCloseTime;		/*关闭时间*/
	
		public string szDispInfoURL;		/*显示信息连接*/
	
		public string szMemo;		/*说明信息*/
	
		public string szManRooms;		/*管理房间(房间编号，多个逗号隔开)*/
	
	public UNIDEVICE[] szManDevs;		/*管理设备列表CUniTable[UNIDEVICE]*/
		};

	/*控制台退出请求*/
	
	public struct CONLOGOUTREQ
	{
		private Reserved reserved;
		
		public uint? dwConsoleSN;		/*控制台编号*/
	
		public string szMemo;		/*说明信息*/
		};

	/*控制台定时通信请求*/
	
	public struct CONPULSEREQ
	{
		private Reserved reserved;
		
		public uint? dwConsoleSN;		/*控制台编号*/
	
		public uint? dwStatus;		/*控制台状态*/
	
		public string szStatInfo;		/*状态信息*/
		};

	/*控制台定时通信响应*/
	
	public struct CONPULSERES
	{
		private Reserved reserved;
		
		public uint? dwChanged;		/*控制台是否已更新*/
	
		public string szMemo;		/*说明信息*/
		};

	/*控制台显示消息结构*/
	
	public struct CONMESSAGE
	{
		private Reserved reserved;
		
		public uint? dwMsgKind;		/*消息类型*/
	
		[FlagsAttribute]
		public enum DWMSGKIND : uint
		{
			
				[EnumDescription("提示信息，MsgInfo为字符串")]
				CONMSG_INFO = 0x1,
			
				[EnumDescription("警告信息，MsgInfo为字符串")]
				CONMSG_WARNING = 0x2,
			
				[EnumDescription("错误信息，MsgInfo为字符串")]
				CONMSG_ERROR = 0x4,
			
				[EnumDescription("门禁刷卡,MsgInfo为CUniStruct[DOORCARDRES]")]
				CONMSG_DOORCARD = 0x100,
			
				[EnumDescription("远程唤醒,MsgInfo为CUniStruct[UNIDEVICE]")]
				CONMSG_WAKEUP = 0x200,
			
		}

	
		public string MsgInfo;		/*消息内容，根据不同的类型对应不同的内容*/
		};

	/*控制台刷卡返回用户信息*/
	
	public struct CONUSERINFO
	{
		private Reserved reserved;
		
		public uint? dwUserStat;		/*用户状态*/
	
		[FlagsAttribute]
		public enum DWUSERSTAT : uint
		{
			
				[EnumDescription("没有预约")]
				CONUSERSTAT_NORESV = 1,
			
				[EnumDescription("有预约")]
				CONUSERSTAT_RESV = 2,
			
				[EnumDescription("使用中")]
				CONUSERSTAT_INUSE = 4,
			
				[EnumDescription("有未支付账单")]
				CONUSERSTAT_BILL = 8,
			
				[EnumDescription("签到成功")]
				CONUSERSTAT_SIGNOK = 0x10,
			
				[EnumDescription("系统未到开放时间")]
				CONUSERSTAT_SYSUNOPEN = 0x20,
			
				[EnumDescription("系统已过开放时间")]
				CONUSERSTAT_SYSCLOSED = 0x40,
			
				[EnumDescription("刷卡退出")]
				CONUSERSTAT_EXIT = 0x80,
			
				[EnumDescription("等待结束刷卡")]
				CONUSERSTAT_WAITEXITCARD = 0x100,
			
				[EnumDescription("等待签到")]
				CONUSERSTAT_WAITSIGN = 0x200,
			
		}

	
	public UNIACCOUNT AccInfo;		/*UNIACCOUNT 结构*/
	
	public UNIRESERVE ResvInfo;		/*UNIRESERVE 结构*/
	
	public UNIDEVICE DevInfo;		/*UNIDEVICE 结构*/
	
	public UNIBILL[] BillInfo;		/*账单表(CUniTable<UNIBILL>)*/
	
		public string szMemo;		/*说明信息*/
		};

	/*控制台教师登录返回信息*/
	
	public struct CONTEACHERINFO
	{
		private Reserved reserved;
		
	public UNIACCOUNT AccInfo;		/*UNIACCOUNT 结构*/
	
	public UNIRESERVE ResvInfo;		/*UNIRESERVE 结构*/
	
		public string szMemo;		/*说明信息*/
		};

	/*刷卡上机请求*/
	
	public struct CARDFORPCREQ
	{
		private Reserved reserved;
		
		public uint? dwDevKind;		/*设备类型(为空由后台自动分配）*/
	
		public uint? dwLabID;		/*实验室ID号(为空由后台自动分配）*/
	
		public uint? dwRoomID;		/*所选房间的ID号(为空由后台自动分配）*/
	
		public uint? dwDevID;		/*客户端设备的ID号(为空由后台自动分配）*/
	
	public ACCCHECKREQ CheckReq;		/*(ACCCHECKREQ结构)*/
		};

	/*刷卡上机应答*/
	
	public struct CARDFORPCRES
	{
		private Reserved reserved;
		
		public uint? dwMode;		/*返回类型*/
	
		[FlagsAttribute]
		public enum DWMODE : uint
		{
			
				[EnumDescription("刷卡上机(ExtInfo返回UNIRESERVE结构)")]
				CARDMODE_IN = 0x1,
			
				[EnumDescription("刷卡下机(ExtInfo返回UNIBILL表)")]
				CARDMODE_OUT = 0x2,
			
				[EnumDescription("挂账结算(ExtInfo返回UNIBILL表)")]
				CARDMODE_DEALMONEY = 0x4,
			
		}

	
	public byte[] ExtInfo;		/*根据不同的返回类型对应不同的内容*/
		};

	/*通道机刷卡请求*/
	
	public struct AUTOGATECARDREQ
	{
		private Reserved reserved;
		
		public uint? dwCardMode;		/*进出刷卡*/
	
		[FlagsAttribute]
		public enum DWCARDMODE : uint
		{
			
				[EnumDescription("进入刷卡")]
				AUTOGATECARD_IN = 1,
			
				[EnumDescription("出去刷卡")]
				AUTOGATECARD_OUT = 2,
			
		}

	
		public string szLogonName;		/*登录名(学工号）*/
	
		public string szCardNo;		/*卡号*/
	
		public string szMemo;		/*说明信息*/
		};

	/*通道机刷卡响应*/
	
	public struct AUTOGATECARDRES
	{
		private Reserved reserved;
		
		public string szTrueName;		/*姓名*/
	
		public string szInfo;		/*提示信息*/
		};

	/*控制台刷卡进入*/
	
	public struct CONUSERINREQ
	{
		private Reserved reserved;
		
		public uint? dwInType;		/*进入类型*/
	
		[FlagsAttribute]
		public enum DWINTYPE : uint
		{
			
				[EnumDescription("暂时离开返回")]
				CONUSERIN_GOBACK = 1,
			
		}

	
		public uint? dwResvID;		/*预约ID号*/
	
		public uint? dwLabID;		/*实验室ID号*/
	
		public uint? dwDevID;		/*客户端设备的ID号*/
	
		public uint? dwDevKind;		/*设备类型ID*/
	
		public uint? dwEndTime;		/*使用结束时间*/
	
		public string szMemo;		/*说明信息*/
		};

	/*控制台刷卡进入*/
	
	public struct CONUSERINRES
	{
		private Reserved reserved;
		
	public UNIRESERVE ResvInfo;		/*UNIRESERVE 结构*/
	
	public UNIDEVICE DevInfo;		/*UNIDEVICE 结构*/
	
		public string szMemo;		/*说明信息*/
		};

	/*控制台刷卡退出请求*/
	
	public struct CONUSEROUTREQ
	{
		private Reserved reserved;
		
		public uint? dwOutType;		/*离开类型*/
	
		[FlagsAttribute]
		public enum DWOUTTYPE : uint
		{
			
				[EnumDescription("暂时离开")]
				CONUSEROUT_LEAVE = 1,
			
		}

	
		public uint? dwLabID;		/*实验室ID号*/
	
		public uint? dwDevID;		/*客户端设备的ID号*/
		};

	/*控制台刷卡退出应答*/
	
	public struct CONUSEROUTRES
	{
		private Reserved reserved;
		
	public UNIACCTINFO AcctInfo;		/*使用信息，UNIACCTINFO 结构*/
	
	public UNIBILL[] BillInfo;		/*账单表(CUniTable<UNIBILL>)*/
	
		public string szMemo;		/*说明信息*/
		};

	/*手机扫描请求*/
	
	public struct MOBILESCANREQ
	{
		private Reserved reserved;
		
		public string szMSN;		/*MSN*/
	
		public string szLogonName;		/*登录名*/
	
		public string szPassword;		/*密码*/
	
		public string szIP;		/*IP地址*/
	
		public uint? dwProperty;		/*扩展属性*/
	
		[FlagsAttribute]
		public enum DWPROPERTY : uint
		{
			
				[EnumDescription("认证成功绑定微信号")]
				MSCANPROP_BINDMSN = 1,
			
		}

	
		public uint? dwStaSN;		/*站点编号*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public uint? dwDevID;		/*设备ID*/
	
		public string szMemo;		/*说明信息*/
		};

	/*手机扫描响应*/
	
	public struct MOBILESCANRES
	{
		private Reserved reserved;
		
		public uint? dwSessionID;		/*服务器分配的SessionID*/
	
		public uint? dwUserStat;		/*用户状态*/
	
		[FlagsAttribute]
		public enum DWUSERSTAT : uint
		{
			
				[EnumDescription("签到成功(无需后续操作）")]
				MSUSERSTAT_SIGNOK = 0x1,
			
				[EnumDescription("设备空闲(使用需调用MSREQ_MOBILE_USERIN）")]
				MSUSERSTAT_IDLE = 0x2,
			
				[EnumDescription("使用中(可调用MSREQ_MOBILE_USEROUT进行后续操作）")]
				MSUSERSTAT_INUSE = 0x4,
			
				[EnumDescription("刷卡退出成功（无需后续操作）")]
				MSUSERSTAT_EXIT = 0x8,
			
				[EnumDescription("暂时离开返回成功(无需后续操作）")]
				MSUSERSTAT_GOBACK = 0x10,
			
				[EnumDescription("预约未生效(无需后续操作）")]
				MSUSERSTAT_RESVUNDO = 0x20,
			
				[EnumDescription("可续约(和使用中一起返回，允许调用MSREQ_MOBILE_DELAY）")]
				MSUSERSTAT_CANDELAY = 0x40,
			
		}

	
		public uint? dwMinUseMin;		/*最少使用时间(分钟)*/
	
		public uint? dwMaxUseMin;		/*最长使用时间(分钟)*/
	
		public string szPID;		/*学工号*/
	
		public string szTrueName;		/*姓名*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public uint? dwDevID;		/*设备ID*/
	
		public string szDevName;		/*设备名称*/
	
		public string szDispInfo;		/*显示信息*/
		};

	/*手机开始使用请求*/
	
	public struct MOBILEUSERINREQ
	{
		private Reserved reserved;
		
		public uint? dwUseMin;		/*使用时间(分钟)*/
	
		public string szMemo;		/*备注*/
		};

	/*手机进入响应*/
	
	public struct MOBILEUSERINRES
	{
		private Reserved reserved;
		
		public string szDispInfo;		/*显示信息*/
		};

	/*手机开始使用请求*/
	
	public struct MOBILEDELAYREQ
	{
		private Reserved reserved;
		
		public uint? dwDelayMin;		/*延长时间(分钟)*/
	
		public string szMemo;		/*备注*/
		};

	/*手机进入响应*/
	
	public struct MOBILEDELAYRES
	{
		private Reserved reserved;
		
		public string szDispInfo;		/*显示信息*/
		};

	/*手机退出请求*/
	
	public struct MOBILEUSEROUTREQ
	{
		private Reserved reserved;
		
		public uint? dwOutType;		/*离开类型*/
	
		[FlagsAttribute]
		public enum DWOUTTYPE : uint
		{
			
				[EnumDescription("暂时离开")]
				MSUSEROUT_LEAVE = 1,
			
				[EnumDescription("结束使用")]
				MSUSEROUT_EXIT = 2,
			
				[EnumDescription("延时使用")]
				MSUSEROUT_DELAY = 4,
			
		}

	
		public uint? dwDelayMin;		/*延时时间(分钟)*/
	
		public string szMemo;		/*备注*/
		};

	/*手机退出响应*/
	
	public struct MOBILEUSEROUTRES
	{
		private Reserved reserved;
		
		public string szDispInfo;		/*显示信息*/
		};

	/*手机登录签到请求*/
	
	public struct RESVUSERCOMEINREQ
	{
		private Reserved reserved;
		
		public uint? dwInType;		/*进入类型*/
	
		[FlagsAttribute]
		public enum DWINTYPE : uint
		{
			
				[EnumDescription("暂时离开返回")]
				RESVUSERIN_GOBACK = 1,
			
		}

	
		public uint? dwResvID;		/*预约ID号*/
	
		public uint? dwLabID;		/*实验室ID号*/
	
		public uint? dwDevID;		/*客户端设备的ID号*/
	
		public string szMemo;		/*说明信息*/
		};

	/*手机登录签到响应*/
	
	public struct RESVUSERCOMEINRES
	{
		private Reserved reserved;
		
		public string szDispInfo;		/*显示信息*/
		};

	/*手机登录延时请求*/
	
	public struct RESVUSERDELAYREQ
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*预约ID号*/
	
		public uint? dwLabID;		/*实验室ID号*/
	
		public uint? dwDevID;		/*客户端设备的ID号*/
	
		public uint? dwMaxDelayMin;		/*最大延长时间(分钟)*/
	
		public string szMemo;		/*备注*/
		};

	/*手机登录延时响应*/
	
	public struct RESVUSERDELAYRES
	{
		private Reserved reserved;
		
		public string szDispInfo;		/*显示信息*/
		};

	/*手机登录退出请求*/
	
	public struct RESVUSERGOOUTREQ
	{
		private Reserved reserved;
		
		public uint? dwOutType;		/*离开类型*/
	
		[FlagsAttribute]
		public enum DWOUTTYPE : uint
		{
			
				[EnumDescription("暂时离开")]
				RESVUSEROUT_LEAVE = 1,
			
				[EnumDescription("结束使用")]
				RESVUSEROUT_EXIT = 2,
			
		}

	
		public uint? dwResvID;		/*预约ID号*/
	
		public uint? dwLabID;		/*实验室ID号*/
	
		public uint? dwDevID;		/*客户端设备的ID号*/
	
		public string szMemo;		/*备注*/
		};

	/*手机登录退出响应*/
	
	public struct RESVUSERGOOUTRES
	{
		private Reserved reserved;
		
		public string szDispInfo;		/*显示信息*/
		};

	/*摇一摇签到请求*/
	
	public struct SHAKECHECKINREQ
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*预约ID号*/
	
		public string szMemo;		/*说明信息*/
		};

	/*摇一摇签到应答*/
	
	public struct SHAKECHECKINRES
	{
		private Reserved reserved;
		
		public string szDispInfo;		/*显示信息*/
		};

	/*摇一摇进馆请求*/
	
	public struct SHAKECOMEINREQ
	{
		private Reserved reserved;
		
		public uint? dwRoomID;		/*RoomID（扩展）*/
	
		public string szMemo;		/*说明信息*/
		};

	/*摇一摇进馆应答*/
	
	public struct SHAKECOMEINRES
	{
		private Reserved reserved;
		
		public string szDispInfo;		/*显示信息*/
		};

	/*结束数据结构*/

	/*开始数据结构*/

	/*获取预约记录*/
	
	public struct RESVRECREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*方式*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("全部")]
				RESVRECGET_BYALL = 0,
			
				[EnumDescription("ResvID")]
				RESVRECGET_BYID = 1,
			
				[EnumDescription("设备ID")]
				RESVRECGET_BYDEVID = 2,
			
				[EnumDescription("房间ID")]
				RESVRECGET_BYROOMID = 3,
			
				[EnumDescription("实验室ID")]
				RESVRECGET_BYLABID = 4,
			
				[EnumDescription("实验计划ID")]
				RESVRECGET_BYTESTPLANID = 5,
			
				[EnumDescription("实验项目ID")]
				RESVRECGET_BYTESTITEMID = 6,
			
		}

	
		public string szGetKey;		/*条件值*/
	
		public uint? dwAccNo;		/*账号*/
	
		public uint? dwUseMode;		/*使用模式*/
	
		public uint? dwPurpose;		/*预约用途*/
	
		public uint? dwDevKind;		/*设备类型*/
	
		public uint? dwStatus;		/*状态*/
	
		public uint? dwCheckStat;		/*审查状态*/
	
		public uint? dwCommentStat;		/*评价状态*/
	
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*设备预约信息*/
	
	public struct UNIRESVREC
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*流水号*/
	
		public uint? dwResvID;		/*预约号*/
	
		public uint? dwPreDate;		/*预约日期*/
	
		public uint? dwPreBegin;		/*预约开始时间*/
	
		public uint? dwPreEnd;		/*预约结束时间*/
	
		public uint? dwUseMode;		/*使用模式*/
	
		public uint? dwPurpose;		/*预约用途*/
	
		public uint? dwAccNo;		/*账号*/
	
		public string szPID;		/*学工号*/
	
		public string szTrueName;		/*姓名*/
	
		public uint? dwDevID;		/*设备ID*/
	
		public uint? dwDevKind;		/*设备类型*/
	
		public uint? dwDevSN;		/*设备编号*/
	
		public string szDevName;		/*设备名称*/
	
		public uint? dwRoomID;		/*房间ID*/
	
		public string szRoomName;		/*房间名称*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabName;		/*实验室名称*/
	
		public uint? dwStatus;		/*状态*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("缺席")]
				RESVRECSTAT_ABSENT = 0x1,
			
				[EnumDescription("病假")]
				RESVRECSTAT_SICK = 0x2,
			
				[EnumDescription("事假")]
				RESVRECSTAT_PRIVATE = 0x4,
			
				[EnumDescription("考勤中")]
				RESVRECSTAT_ATTENDDOING = 0x8,
			
				[EnumDescription("未签到")]
				RESVRECSTAT_UNSIGN = 0x10,
			
				[EnumDescription("已签到")]
				RESVRECSTAT_SIGNED = 0x20,
			
				[EnumDescription("已登录")]
				RESVRECSTAT_LOGINED = 0x40,
			
				[EnumDescription("未刷卡离开")]
				RESVRECSTAT_LEAVENOCARD = 0x80,
			
				[EnumDescription("出席")]
				RESVRECSTAT_ATTEND = 0x100,
			
				[EnumDescription("迟到")]
				RESVRECSTAT_LATE = 0x200,
			
				[EnumDescription("早退")]
				RESVRECSTAT_LEAVE = 0x400,
			
				[EnumDescription("使用时间不达标")]
				RESVRECSTAT_USELESS = 0x800,
			
				[EnumDescription("已核算")]
				RESVRECSTAT_ADJUST = 0x10000,
			
				[EnumDescription("已结算")]
				RESVRECSTAT_CHECKOUT = 0x20000,
			
		}

	
		public uint? dwResvTime;		/*预约总时间*/
	
		public uint? dwUseTime;		/*使用总时间*/
	
		public uint? dwCheckStat;		/*审查状态*/
	
		public uint? dwCommentStat;		/*用户评价状态*/
	
		[FlagsAttribute]
		public enum DWCOMMENTSTAT : uint
		{
			
				[EnumDescription("用户未评价")]
				RCSTAT_UNDO = 0x1,
			
				[EnumDescription("用户已评价")]
				RCSTAT_DONE = 0x2,
			
		}

	
		public uint? dwTotalFee;		/*费用合计*/
	
		public string szMemo;		/*备注*/
	
		public uint? dwInTime;		/*签到时间*/
	
		public uint? dwInMode;		/*签到方式*/
	
		[FlagsAttribute]
		public enum DWINMODE : uint
		{
			
				[EnumDescription("控制台")]
				RCMODE_CONSOLE = 0x1,
			
				[EnumDescription("通道机")]
				RCMODE_AG = 0x2,
			
				[EnumDescription("手机")]
				RCMODE_HP = 0x4,
			
				[EnumDescription("智能座位监控设备")]
				RCMODE_MONITOR = 0x8,
			
				[EnumDescription("电脑登录")]
				RCMODE_PC = 0x10,
			
				[EnumDescription("门禁刷卡")]
				RCMODE_DOOR = 0x20,
			
				[EnumDescription("自动签到")]
				RCMODE_AUTO = 0x40,
			
				[EnumDescription("管理员")]
				RCMODE_ADMIN = 0x80,
			
		}

	
		public uint? dwOutTime;		/*退出时间*/
	
		public uint? dwOutMode;		/*退出方式*/
	
		public uint? dwLeaveTime;		/*暂时离开时间*/
	
		public uint? dwLeaveMode;		/*暂时离开方式*/
	
		public uint? dwBackTime;		/*返回时间*/
	
		public uint? dwBackMode;		/*返回方式*/
		};

	/*获取预约类型统计记录*/
	
	public struct RESVKINDSTATREQ
	{
		private Reserved reserved;
		
		public uint? dwPurpose;		/*预约用途*/
	
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*预约类型统计*/
	
	public struct RESVKINDSTAT
	{
		private Reserved reserved;
		
		public uint? dwKind;		/*预约类型*/
	
		public uint? dwResvTimes;		/*预约次数*/
	
		public uint? dwResvMinutes;		/*预约总时间(分钟)*/
	
		public uint? dwTestHour;		/*实验学时数*/
	
		public uint? dwResvDevs;		/*预约机器数*/
	
		public uint? dwUseDevs;		/*实际用机数*/
	
		public uint? dwResvUsers;		/*上课总人数*/
	
		public uint? dwRealUsers;		/*实际到课人数*/
		};

	/*获取预约方式统计的请求包*/
	
	public struct RESVMODESTATREQ
	{
		private Reserved reserved;
		
		public uint? dwOwner;		/*预约人(所有者)*/
	
		public uint? dwMemberID;		/*成员ID*/
	
		public uint? dwDevID;		/*设备ID*/
	
		public uint? dwUseMode;		/*使用方法*/
	
		public uint? dwPurpose;		/*预约用途*/
	
		public uint? dwDevKind;		/*设备类型*/
	
		public uint? dwClassKind;		/*类别(见UNIDEVCLS的Kind定义)*/
	
		public uint? dwBeginDate;		/*预约开始日期*/
	
		public uint? dwEndDate;		/*预约结束日期*/
	
		public string szRoomNos;		/*房间编号,多个用逗号隔开*/
	
		public uint? dwKind;		/*预约类型*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*预约方式统计*/
	
	public struct RESVMODESTAT
	{
		private Reserved reserved;
		
		public uint? dwUseMode;		/*预约方式*/
	
		public uint? dwUsers;		/*预约人数*/
	
		public uint? dwResvTimes;		/*预约次数*/
	
		public uint? dwResvMinutes;		/*预约总时间(分钟)*/
		};

	/*查询统计的 请求*/
	
	public struct REPORTREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*方式*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("全部")]
				USERECGET_BYALL = 0,
			
				[EnumDescription("设备ID")]
				USERECGET_BYDEVID = 2,
			
				[EnumDescription("房间ID")]
				USERECGET_BYROOMID = 3,
			
				[EnumDescription("实验室ID")]
				USERECGET_BYLABID = 4,
			
		}

	
		public string szGetKey;		/*条件值*/
	
		public uint? dwAccNo;		/*账号*/
	
		public uint? dwPurpose;		/*用途*/
	
		public uint? dwDevKind;		/*设备类型*/
	
		public uint? dwClassKind;		/*类别(见UNIDEVCLS的Kind定义)*/
	
		public uint? dwCheckStat;		/*管理员检查状态(CHECKINFO定义)*/
	
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
		public uint? dwMinPrice;		/*最低单价*/
	
		public uint? dwMaxPrice;		/*最高单价*/
	
		public uint? dwStartPurchaseDate;		/*最早购买日期*/
	
		public uint? dwEndPurchaseDate;		/*截止购买日期*/
	
		public string szRoomIDs;		/*房间ID,多个用逗号隔开*/
	
		public string szBuildingIDs;		/*楼宇ID,多个用逗号隔开*/
	
		public string szCampusIDs;		/*校区ID,多个用逗号隔开*/
	
		public uint? dwActivitySN;		/*活动类型编号*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*设备使用记录 请求*/
	
	public struct USERECREQ
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*设备ID*/
	
		public uint? dwRoomID;		/*房间ID*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public uint? dwAccNo;		/*账号*/
	
		public uint? dwDeptID;		/*部门ID*/
	
		public uint? dwPurpose;		/*用途*/
	
		public uint? dwDevKind;		/*设备类型*/
	
		public uint? dwClassKind;		/*类别(见UNIDEVCLS的Kind定义)*/
	
		public uint? dwCheckStat;		/*管理员检查状态(CHECKINFO定义)*/
	
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
		public uint? dwMinPrice;		/*最低单价*/
	
		public uint? dwMaxPrice;		/*最高单价*/
	
		public uint? dwStartPurchaseDate;		/*最早购买日期*/
	
		public uint? dwEndPurchaseDate;		/*截止购买日期*/
	
		public uint? dwAttendantID;		/*值班员账号*/
	
		public string szMAC;		/*网卡地址*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*设备使用记录*/
	
	public struct DEVUSEREC
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*流水号*/
	
		public uint? dwAccNo;		/*帐号*/
	
		public string szPID;		/*学工号*/
	
		public string szTrueName;		/*姓名*/
	
		public uint? dwTutorID;		/*导师（帐号）*/
	
		public string szTutorName;		/*导师姓名*/
	
		public uint? dwDeptID;		/*部门ID*/
	
		public string szDeptName;		/*部门*/
	
		public uint? dwDevID;		/*设备ID*/
	
		public uint? dwDevSN;		/*设备编号*/
	
		public string szDevName;		/*设备名称*/
	
		public string szMAC;		/*网卡地址*/
	
		public string szKindName;		/*设备名称*/
	
		public string szClassName;		/*设备类别名称*/
	
		public string szModel;		/*设备型号*/
	
		public string szSpecification;		/*设备规格*/
	
		public uint? dwUnitPrice;		/*设备单价(元)*/
	
		public uint? dwPurchaseDate;		/*采购日期 YYYYMMDD*/
	
		public uint? dwRoomID;		/*房间ID*/
	
		public string szRoomName;		/*房间称*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabName;		/*实验室名称*/
	
		public uint? dwAttendantID;		/*值班员账号*/
	
		public string szAttendantName;		/*值班员姓名*/
	
		public uint? dwPurpose;		/*用途*/
	
		public uint? dwBeginTime;		/*开始时间*/
	
		public uint? dwEndTime;		/*结束时间*/
	
		public uint? dwUseTime;		/*使用时间*/
	
		public uint? dwTotalCost;		/*总费用*/
	
		public uint? dwBeginAdmin;		/*外借管理员ID*/
	
		public string szBeginAdminName;		/*外借管理员姓名*/
	
		public uint? dwEndAdmin;		/*归还管理员ID*/
	
		public string szEndAdminName;		/*归还管理员姓名*/
	
		public uint? dwCheckStat;		/*管理员检查状态*/
	
		public string szMemo;		/*说明信息*/
		};

	/*查询明细的条件*/
	
	public struct DOORCARDRECREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*方式*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("全部")]
				DOORCARDRECGET_BYALL = 0,
			
				[EnumDescription("指定用户组")]
				DOORCARDRECGET_BYGROUP = 1,
			
				[EnumDescription("房间ID")]
				DOORCARDRECGET_BYROOMID = 3,
			
				[EnumDescription("实验室ID")]
				DOORCARDRECGET_BYLABID = 4,
			
		}

	
		public string szGetKey;		/*条件值*/
	
		public uint? dwAccNo;		/*账号*/
	
		public uint? dwCardMode;		/*由DOORCARDREQ结构定义*/
	
		public uint? dwUserKind;		/*在DOORCARDRES定义*/
	
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
		public uint? dwStartTime;		/*开始时间*/
	
		public uint? dwEndTime;		/*结束时间*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*门禁刷卡记录*/
	
	public struct DOORCARDREC
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*流水号*/
	
		public uint? dwAccNo;		/*帐号*/
	
		public string szPID;		/*学工号*/
	
		public string szCardNo;		/*卡号*/
	
		public string szTrueName;		/*姓名*/
	
		public uint? dwTutorID;		/*导师（帐号）*/
	
		public string szTutorName;		/*导师姓名*/
	
		public uint? dwDeptID;		/*部门ID*/
	
		public string szDeptName;		/*部门*/
	
		public uint? dwUsedDate;		/*刷卡日期*/
	
		public uint? dwCardTime;		/*刷卡时间*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabName;		/*实验室名称*/
	
		public uint? dwRoomID;		/*房间ID*/
	
		public string szRoomNo;		/*房间编号*/
	
		public string szRoomName;		/*房间名称*/
	
		public uint? dwManMode;		/*控制方式(UNIROOM中定义)*/
	
		public uint? dwCardMode;		/*刷卡模式*/
	
		public uint? dwUserKind;		/*用户种类*/
	
		public uint? dwResvID;		/*预约ID*/
	
		public string szMemo;		/*扩展信息*/
		};

	/*课外使用统计*/
	
	public struct USERSTAT
	{
		private Reserved reserved;
		
		public uint? dwAccNo;		/*帐号*/
	
		public string szPID;		/*学工号*/
	
		public string szTrueName;		/*姓名*/
	
		public string szDeptName;		/*部门*/
	
		public uint? dwUseTimes;		/*使用人次*/
	
		public uint? dwUseTime;		/*使用总时间*/
		};

	/*实验室使用率统计*/
	
	public struct LABSTAT
	{
		private Reserved reserved;
		
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabSN;		/*编号*/
	
		public string szLabName;		/*名称*/
	
		public uint? dwTotalNum;		/*数量(人数或设备数)*/
	
		public uint? dwTotalTestHour;		/*使用总人学时数*/
	
		public uint? dwPIDNum;		/*课外使用人数*/
	
		public uint? dwUseTimes;		/*课外使用人次*/
	
		public uint? dwTotalUseTime;		/*课外使用总时间*/
		};

	/*实验室(房间)使用率统计*/
	
	public struct ROOMSTAT
	{
		private Reserved reserved;
		
		public uint? dwRoomID;		/*房间ID*/
	
		public string szLabSN;		/*实验室编号*/
	
		public string szLabName;		/*实验室名称*/
	
		public string szRoomNo;		/*房间号*/
	
		public string szRoomName;		/*房间名称*/
	
		public uint? dwTotalNum;		/*数量(设备数)*/
	
		public uint? dwTotalTestHour;		/*使用总人学时数*/
	
		public uint? dwTestUseTimes;		/*教学实验使用人次*/
	
		public uint? dwUseTimes;		/*课外使用人次*/
	
		public uint? dwTotalUseTime;		/*课外使用总时间*/
		};

	/*设备类型使用率统计*/
	
	public struct DEVKINDSTAT
	{
		private Reserved reserved;
		
		public uint? dwKindID;		/*设备类型ID*/
	
		public string szKindName;		/*设备名称*/
	
		public string szModel;		/*设备型号*/
	
		public string szSpecification;		/*设备规格*/
	
		public uint? dwTotalNum;		/*数量(人数或设备数)*/
	
		public uint? dwTotalTestHour;		/*使用总人学时数*/
	
		public uint? dwPIDNum;		/*课外使用人数*/
	
		public uint? dwUseTimes;		/*课外使用人次*/
	
		public uint? dwTotalUseTime;		/*课外使用总时间*/
		};

	/*设备类别使用率统计*/
	
	public struct DEVCLASSSTAT
	{
		private Reserved reserved;
		
		public uint? dwClassID;		/*设备类别ID*/
	
		public uint? dwClassKind;		/*类别(见UNIDEVCLS的Kind定义)*/
	
		public string szClassName;		/*设备类别名称*/
	
		public uint? dwTotalNum;		/*数量(人数或设备数)*/
	
		public uint? dwTotalTestHour;		/*使用总人学时数*/
	
		public uint? dwPIDNum;		/*课外使用人数*/
	
		public uint? dwUseTimes;		/*课外使用人次*/
	
		public uint? dwTotalUseTime;		/*课外使用总时间*/
		};

	/*设备使用率统计*/
	
	public struct DEVSTAT
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*设备ID*/
	
		public uint? dwDevSN;		/*设备编号*/
	
		public string szDevName;		/*设备名称*/
	
		public string szKindName;		/*设备类别名称*/
	
		public string szClassName;		/*设备类别名称*/
	
		public string szModel;		/*设备型号*/
	
		public string szSpecification;		/*设备规格*/
	
		public uint? dwUnitPrice;		/*设备单价*/
	
		public uint? dwPurchaseDate;		/*采购日期 YYYYMMDD*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabSN;		/*实验室编号*/
	
		public string szLabName;		/*实验室名称*/
	
		public uint? dwDeptID;		/*部门ID*/
	
		public string szDeptName;		/*部门*/
	
		public uint? dwAttendantID;		/*值班员账号*/
	
		public string szAttendantName;		/*值班员姓名*/
	
		public uint? dwTotalTestHour;		/*使用总人学时数*/
	
		public uint? dwPIDNum;		/*课外使用人数*/
	
		public uint? dwUseTimes;		/*课外使用人次*/
	
		public uint? dwTotalUseTime;		/*课外使用总时间*/
	
		public uint? dwTotalCost;		/*总费用*/
		};

	/*学院使用统计*/
	
	public struct DEPTSTAT
	{
		private Reserved reserved;
		
		public uint? dwDeptID;		/*学院ID*/
	
		public string szDeptSN;		/*学院编号*/
	
		public string szDeptName;		/*学院名称*/
	
		public uint? dwTotalUsers;		/*学院人数*/
	
		public uint? dwPIDNum;		/*使用人数*/
	
		public uint? dwUseTimes;		/*使用人次*/
	
		public uint? dwTotalUseTime;		/*使用总时间*/
		};

	/*查询身份统计的 请求*/
	
	public struct IDENTSTATREQ
	{
		private Reserved reserved;
		
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
		public uint? dwPurpose;		/*用途*/
	
		public uint? dwDevKind;		/*设备类型*/
	
		public uint? dwClassKind;		/*类别(见UNIDEVCLS的Kind定义)*/
	
		public uint? dwCheckStat;		/*管理员检查状态(CHECKINFO定义)*/
	
		public uint? dwDeptID;		/*人员所属部门ID*/
	
		public uint? dwActivitySN;		/*活动类型编号*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*身份使用统计*/
	
	public struct IDENTSTAT
	{
		private Reserved reserved;
		
		public uint? dwIdent;		/*身份*/
	
		public uint? dwTotalUsers;		/*总人数*/
	
		public uint? dwPIDNum;		/*使用人数*/
	
		public uint? dwUseTimes;		/*使用人次*/
	
		public uint? dwTotalUseTime;		/*使用总时间*/
		};

	/*获取实验项目表*/
	
	public struct TESTITEMSTATREQ
	{
		private Reserved reserved;
		
		public uint? dwTeacherID;		/*教师（帐号）*/
	
		public uint? dwCourseID;		/*课程ID*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*实验项目表*/
	
	public struct TESTITEMSTAT
	{
		private Reserved reserved;
		
		public uint? dwTestItemID;		/*实验项目ID*/
	
		public uint? dwTestCardID;		/*实验项目卡ID*/
	
		public string szTestName;		/*实验名称*/
	
		public uint? dwGroupPeopleNum;		/*每组人数*/
	
		public uint? dwTestHour;		/*本实验项目学时数*/
	
		public uint? dwTestClass;		/*实验类别*/
	
		public uint? dwTestKind;		/*实验类型*/
	
		public uint? dwRequirement;		/*实验要求*/
	
		public uint? dwTestPlanID;		/*实验计划ID*/
	
		public string szAcademicSubjectCode;		/*所属学科编码*/
	
		public uint? dwTesteeKind;		/*实验者类别*/
	
		public uint? dwTeacherID;		/*教师（帐号）*/
	
		public string szTeacherName;		/*教师姓名*/
	
		public uint? dwCourseID;		/*课程ID*/
	
		public string szCourseCode;		/*课程代码*/
	
		public string szCourseName;		/*课程名称*/
	
		public uint? dwCourseProperty;		/*课程属性*/
	
		public uint? dwGroupID;		/*上课班级*/
	
		public string szGroupName;		/*上课班级名称*/
	
		public uint? dwGroupUsers;		/*组用户数*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabSN;		/*实验室编号*/
	
		public string szLabName;		/*实验室名称*/
	
		public uint? dwDevNum;		/*预约设备数*/
		};

	/*获取教学预约记录*/
	
	public struct TEACHINGRESVRECREQ
	{
		private Reserved reserved;
		
		public uint? dwTeacherID;		/*教师（帐号）*/
	
		public uint? dwCourseID;		/*课程ID*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public uint? dwTestPlanID;		/*实验计划ID*/
	
		public uint? dwYearTerm;		/*学期编号*/
	
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
		public uint? dwMinUseRate;		/*实到最低使用率*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*每分钟实到人数*/
	
	public struct USERSPERMINUTE
	{
		private Reserved reserved;
		
		public uint? dwUsers;		/*实到人数*/
		};

	/*教学预约记录*/
	
	public struct TEACHINGRESVREC
	{
		private Reserved reserved;
		
		public uint? dwTestItemID;		/*实验项目ID*/
	
		public uint? dwTestCardID;		/*实验项目卡ID*/
	
		public string szTestName;		/*实验名称*/
	
		public uint? dwGroupPeopleNum;		/*每组人数*/
	
		public uint? dwTestHour;		/*本实验项目学时数*/
	
		public uint? dwTestClass;		/*实验类别*/
	
		public uint? dwTestKind;		/*实验类型*/
	
		public uint? dwRequirement;		/*实验要求*/
	
		public uint? dwTestPlanID;		/*实验计划ID*/
	
		public string szAcademicSubjectCode;		/*所属学科*/
	
		public uint? dwTesteeKind;		/*实验者类别*/
	
		public uint? dwTeacherID;		/*教师（帐号）*/
	
		public string szTeacherName;		/*教师姓名*/
	
		public uint? dwCourseID;		/*课程ID*/
	
		public string szCourseCode;		/*课程代码*/
	
		public string szCourseName;		/*课程名称*/
	
		public uint? dwCourseProperty;		/*课程属性*/
	
		public uint? dwGroupID;		/*上课班级*/
	
		public string szGroupName;		/*上课班级名称*/
	
		public uint? dwGroupUsers;		/*组用户数*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabSN;		/*实验室编号*/
	
		public string szLabName;		/*实验室名称*/
	
		public uint? dwResvID;		/*预约ID*/
	
		public uint? dwResvStat;		/*预约状态*/
	
		public uint? dwPreDate;		/*预约开始日期*/
	
		public uint? dwBeginTime;		/*预约开始时间*/
	
		public uint? dwEndTime;		/*预约结束时间*/
	
		public uint? dwTeachingTime;		/*教学时间(格式见UNIRESERVE)*/
	
		public uint? dwDevNum;		/*预约设备数*/
	
		public uint? dwAttendUsers;		/*实到人数*/
	
	public USERSPERMINUTE[] UsersPerMinute;		/*CUniTable[USERSPERMINUTE]*/
		};

	/*获取设备使用率请求*/
	
	public struct DEVUSINGRATEREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*方式*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("全部")]
				DEVUSINGRATE_BYALL = 0,
			
				[EnumDescription("设备ID")]
				DEVUSINGRATE_BYDEVID = 2,
			
				[EnumDescription("房间ID")]
				DEVUSINGRATE_BYROOMID = 3,
			
				[EnumDescription("实验室ID")]
				DEVUSINGRATE_BYLABID = 4,
			
		}

	
		public string szGetKey;		/*条件值*/
	
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
		public uint? dwClassKind;		/*类别(见UNIDEVCLS的Kind定义)*/
	
		public string szRoomIDs;		/*房间ID,多个用逗号隔开*/
	
		public string szBuildingIDs;		/*楼宇ID,多个用逗号隔开*/
	
		public string szCampusIDs;		/*校区ID,多个用逗号隔开*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*设备使用率数据表*/
	
	public struct DEVUSINGTABLE
	{
		private Reserved reserved;
		
		public uint? dwUseTimes;		/*设备使用总次数*/
	
		public uint? dwResvTimes;		/*设备预约总次数*/
		};

	/*设备使用率统计表*/
	
	public struct DEVUSINGRATE
	{
		private Reserved reserved;
		
		public uint? dwDevNums;		/*统计设备总数*/
	
		public uint? dwDays;		/*统计天数*/
	
	public DEVUSINGTABLE[] szUsingTable;		/*设备使用率数据表(CUniTable[DEVUSINGTABLE]),维数为24*60*/
		};

	/*获取设备周使用率请求*/
	
	public struct DEVWEEKUSINGRATEREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*方式*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("全部")]
				DEVUSINGRATE_BYALL = 0,
			
				[EnumDescription("设备ID")]
				DEVUSINGRATE_BYDEVID = 2,
			
				[EnumDescription("房间ID")]
				DEVUSINGRATE_BYROOMID = 3,
			
				[EnumDescription("实验室ID")]
				DEVUSINGRATE_BYLABID = 4,
			
		}

	
		public string szGetKey;		/*条件值*/
	
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwWeeks;		/*查询周数*/
	
		public uint? dwClassKind;		/*类别(见UNIDEVCLS的Kind定义)*/
	
		public string szRoomIDs;		/*房间ID,多个用逗号隔开*/
	
		public string szBuildingIDs;		/*楼宇ID,多个用逗号隔开*/
	
		public string szCampusIDs;		/*校区ID,多个用逗号隔开*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*设备周使用率统计表*/
	
	public struct DEVWEEKUSINGRATE
	{
		private Reserved reserved;
		
		public uint? dwDevNums;		/*统计设备总数*/
	
		public uint? dwWeeks;		/*统计周数*/
	
	public DEVUSINGTABLE[] szUsingTable;		/*设备使用率数据表(CUniTable[DEVUSINGTABLE]),维数为7*/
		};

	/*场馆活动类型统计 请求*/
	
	public struct YARDACTIVITYSTATREQ
	{
		private Reserved reserved;
		
		public uint? dwDevKind;		/*设备类型*/
	
		public uint? dwClassKind;		/*类别(见UNIDEVCLS的Kind定义)*/
	
		public uint? dwCheckStat;		/*管理员检查状态(CHECKINFO定义)*/
	
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
		public string szRoomIDs;		/*房间ID,多个用逗号隔开*/
	
		public string szBuildingIDs;		/*楼宇ID,多个用逗号隔开*/
	
		public string szCampusIDs;		/*校区ID,多个用逗号隔开*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*场馆活动类型统计*/
	
	public struct YARDACTIVITYSTAT
	{
		private Reserved reserved;
		
		public uint? dwActivitySN;		/*活动类型编号*/
	
		public string szActivityName;		/*活动类型名称*/
	
		public uint? dwPIDNum;		/*使用人数*/
	
		public uint? dwUseTimes;		/*使用人次*/
	
		public uint? dwTotalUseTime;		/*使用总时间*/
		};

	/*获取设备月使用统计*/
	
	public struct DEVMONTHSTATREQ
	{
		private Reserved reserved;
		
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*设备月使用统计*/
	
	public struct DEVMONTHSTAT
	{
		private Reserved reserved;
		
		public uint? dwYearMonth;		/*统计月份*/
	
		public uint? dwWResvTime;		/*工作日设备预约总时间(分钟)*/
	
		public uint? dwRResvTime;		/*非作日设备预约总时间(分钟)*/
	
		public uint? dwWUseTime;		/*工作日设备使用总时间(分钟)*/
	
		public uint? dwRUseTime;		/*非工作日设备使用总时间(分钟)*/
		};

	/*获取设备科研实验统计请求*/
	
	public struct RTUSESTATREQ
	{
		private Reserved reserved;
		
		public uint? dwStatType;		/*统计方式*/
	
		[FlagsAttribute]
		public enum DWSTATTYPE : uint
		{
			
				[EnumDescription("设备")]
				RTSTATBY_DEV = 1,
			
				[EnumDescription("班级")]
				RTSTATBY_CLASS = 2,
			
				[EnumDescription("学院（部门）")]
				RTSTATBY_DEPT = 3,
			
				[EnumDescription("使用人")]
				RTSTATBY_USER = 4,
			
				[EnumDescription("项目主持人（导师）")]
				RTSTATBY_HOLDER = 5,
			
				[EnumDescription("学部（借用szHomeAddr表示）")]
				RTSTATBY_FACULTY = 6,
			
		}

	
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
		public uint? dwDeptID;		/*设备所属部门ID*/
	
		public string szLabIDs;		/*实验室ID,多个用逗号隔开*/
	
		public string szRoomIDs;		/*房间ID,多个用逗号隔开*/
	
		public string szKindIDs;		/*所属类型,多个用逗号隔开*/
	
		public string szClassIDs;		/*所属功能类别ID,多个用逗号隔开*/
	
		public string szDevIDs;		/*设备ID,多个用逗号隔开*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO],szExtInfo返回RTUSESTAT合计*/
		};

	/*设备科研实验统计*/
	
	public struct RTUSESTAT
	{
		private Reserved reserved;
		
		public uint? dwStatID;		/*统计对象ID*/
	
		public string szStatName;		/*统计对象名称*/
	
		public string szExtName;		/*扩展信息(比如仪器管理员）*/
	
		public uint? dwResvTimes;		/*预约次数*/
	
		public uint? dwResvMinutes;		/*预约总时间(分钟)*/
	
		public uint? dwUseTimes;		/*使用次数*/
	
		public uint? dwUseMinutes;		/*设备使用总时间(分钟)*/
	
		public uint? dwSampleNum;		/*测试样品数*/
	
		public uint? dwReceivableCost;		/*应缴费用*/
	
		public uint? dwUseFee;		/*系统自动计算（应缴费用）*/
	
		public uint? dwRealCost;		/*结算费用*/
	
		public uint? dwDevUseFee;		/*设备使用费*/
	
		public uint? dwSampleFee;		/*样品费*/
	
		public uint? dwAssistFee;		/*协助费*/
	
		public uint? dwEntrustFee;		/*代检费*/
	
		public uint? dwNegotiationFee;		/*协议收费*/
		};

	/*获取设备科研实验明细请求*/
	
	public struct RTUSEDETAILREQ
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*设备ID*/
	
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*设备科研实验明细*/
	
	public struct RTUSEDETAIL
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*设备ID*/
	
		public string szDevName;		/*设备名称*/
	
		public string szAttendantName;		/*仪器管理员*/
	
		public uint? dwResvID;		/*预约号*/
	
		public string szTestName;		/*科研实验名称*/
	
		public uint? dwOwner;		/*预约人(创建者)*/
	
		public string szOwnerName;		/*预约人姓名*/
	
		public uint? dwPreDate;		/*预约开始日期*/
	
		public uint? dwBeginTime;		/*预约开始时间*/
	
		public uint? dwRTID;		/*科研实验项目ID*/
	
		public string szRTName;		/*科研实验名称*/
	
		public uint? dwHolderID;		/*主持人（帐号）*/
	
		public string szHolderName;		/*主持人姓名*/
	
		public uint? dwManID;		/*管理员ID*/
	
		public string szManName;		/*管理员姓名*/
	
		public uint? dwResvMinutes;		/*预约总时间(分钟)*/
	
		public uint? dwUseMinutes;		/*设备使用总时间(分钟)*/
	
		public uint? dwSampleNum;		/*测试样品数*/
	
		public uint? dwReceivableCost;		/*应缴费用*/
	
		public uint? dwUseFee;		/*系统自动计算（应缴费用）*/
	
		public uint? dwRealCost;		/*结算费用*/
	
		public uint? dwDevUseFee;		/*设备使用费*/
	
		public uint? dwSampleFee;		/*样品费*/
	
		public uint? dwAssistFee;		/*协助费*/
	
		public uint? dwEntrustFee;		/*代检费*/
	
		public uint? dwNegotiationFee;		/*协议收费*/
		};

	/*获取设备科研实验经费分配统计请求*/
	
	public struct RTFASTATREQ
	{
		private Reserved reserved;
		
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
		public uint? dwDeptID;		/*设备所属部门ID*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO],szExtInfo返回RTFASTAT合计*/
		};

	/*设备科研实验经费分配统计*/
	
	public struct RTFASTAT
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*设备ID*/
	
		public string szDevName;		/*设备名称*/
	
		public string szAttendantName;		/*仪器管理员*/
	
		public uint? dwResvTimes;		/*预约次数*/
	
		public uint? dwResvMinutes;		/*预约总时间(分钟)*/
	
		public uint? dwUseTimes;		/*使用次数*/
	
		public uint? dwUseMinutes;		/*设备使用总时间(分钟)*/
	
		public uint? dwSampleNum;		/*测试样品数*/
	
		public uint? dwTotalFee;		/*收费总金额*/
	
		public uint? dwTestFee;		/*分析测试费*/
	
		public uint? dwOpenFundFee;		/*开放基金*/
	
		public uint? dwServiceFee;		/*劳务费*/
		};

	/*获取设备科研实验经费分配明细请求*/
	
	public struct RTFADETAILREQ
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*设备ID*/
	
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*设备科研实验经费分配明细*/
	
	public struct RTFADETAIL
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*设备ID*/
	
		public string szDevName;		/*设备名称*/
	
		public string szAttendantName;		/*仪器管理员*/
	
		public uint? dwResvID;		/*预约号*/
	
		public string szTestName;		/*科研实验名称*/
	
		public uint? dwOwner;		/*预约人(创建者)*/
	
		public string szOwnerName;		/*预约人姓名*/
	
		public uint? dwPreDate;		/*预约开始日期*/
	
		public uint? dwBeginTime;		/*预约开始时间*/
	
		public uint? dwRTID;		/*科研实验项目ID*/
	
		public string szRTName;		/*科研实验名称*/
	
		public uint? dwHolderID;		/*主持人（帐号）*/
	
		public string szHolderName;		/*主持人姓名*/
	
		public uint? dwManID;		/*管理员ID*/
	
		public string szManName;		/*管理员姓名*/
	
		public uint? dwResvMinutes;		/*预约总时间(分钟)*/
	
		public uint? dwUseMinutes;		/*设备使用总时间(分钟)*/
	
		public uint? dwSampleNum;		/*测试样品数*/
	
		public uint? dwTotalFee;		/*收费总金额*/
	
		public uint? dwTestFee;		/*分析测试费*/
	
		public uint? dwOpenFundFee;		/*开放基金*/
	
		public uint? dwServiceFee;		/*劳务费*/
		};

	/*查询违约统计的请求*/
	
	public struct DEFAULTSTATREQ
	{
		private Reserved reserved;
		
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
		public uint? dwCTSN;		/*信用类别编号*/
	
		public uint? dwUsePurpose;		/*用途*/
	
		public uint? dwForClsKind;		/*适用设备类别*/
	
		public uint? dwDeptID;		/*人员所属部门ID*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*违约统计*/
	
	public struct DEFAULTSTAT
	{
		private Reserved reserved;
		
		public uint? dwCreditSN;		/*信用类型编号*/
	
		public string szCreditName;		/*信用类型名称*/
	
		public uint? dwResvTimes;		/*预约总数*/
	
		public uint? dwDefaultTimes;		/*违约次数*/
		};

	/*教学科研仪器设备表*/
	
	public struct DEVLISTREQ
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*学期编号*/
	
		public uint? dwReportStat;		/*报表状态*/
	
		public string szLabIDs;		/*实验室ID,多个用逗号隔开*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*教学科研仪器设备清单*/
	
	public struct DEVLIST
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*学期编号*/
	
		public uint? dwReportStat;		/*报表状态*/
	
		[FlagsAttribute]
		public enum DWREPORTSTAT : uint
		{
			
				[EnumDescription("原始数据")]
				REPORTSTAT_ORIGINAL = 1,
			
				[EnumDescription("存储数据")]
				REPORTSTAT_SAVE = 2,
			
				[EnumDescription("发布数据")]
				REPORTSTAT_DEPLOY = 0x100,
			
		}

	
		public uint? dwDevID;		/*设备ID*/
	
		public string szAssertSN;		/*用户方资产编号*/
	
		public string szClassSN;		/*设备分类号*/
	
		public string szDevName;		/*实验设备名称*/
	
		public string szModel;		/*设备型号*/
	
		public string szSpecification;		/*设备规格*/
	
		public uint? dwComeFrom;		/*仪器来源*/
	
		public uint? dwNationCode;		/*国别码*/
	
		public uint? dwUnitPrice;		/*设备单价*/
	
		public uint? dwPurchaseDate;		/*采购日期*/
	
		public uint? dwStatCode;		/*现状码*/
	
		public uint? dwUseFor;		/*使用方向*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabSN;		/*实验室编号*/
	
		public string szLabName;		/*实验室名称*/
	
		public uint? dwDeptID;		/*部门ID*/
	
		public string szDeptSN;		/*部门编号*/
	
		public string szDeptName;		/*部门*/
		};

	/*教学科研仪器设备增减变动情况表*/
	
	public struct DEVCHGREQ
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*学期编号*/
	
		public uint? dwReportStat;		/*报表状态*/
	
		public uint? dwUnitPrice;		/*大型仪器价格起点*/
	
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
		};

	/*教学科研仪器设备增减变动情况表*/
	
	public struct DEVCHG
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*学期编号*/
	
		public uint? dwReportStat;		/*报表状态*/
	
		public uint? dwBDevNum;		/*期初数量*/
	
		public uint? dwBMoney;		/*期初金额*/
	
		public uint? dwBBigDevNum;		/*大型仪器期初数量*/
	
		public uint? dwBBigMoney;		/*大型仪器期初金额*/
	
		public uint? dwIncDevNum;		/*增加数量*/
	
		public uint? dwIncMoney;		/*增加金额*/
	
		public uint? dwIncBigDevNum;		/*大型仪器增加数量*/
	
		public uint? dwIncBigMoney;		/*大型仪器增加金额*/
	
		public uint? dwDecDevNum;		/*减少数量*/
	
		public uint? dwDecMoney;		/*减少金额*/
	
		public uint? dwDecBigDevNum;		/*大型仪器减少数量*/
	
		public uint? dwDecBigMoney;		/*大型仪器减少金额*/
	
		public uint? dwEDevNum;		/*期末数量*/
	
		public uint? dwEMoney;		/*期末金额*/
	
		public uint? dwEBigDevNum;		/*大型仪器期末数量*/
	
		public uint? dwEBigMoney;		/*大型仪器期末金额*/
		};

	/*贵重仪器设备表*/
	
	public struct BIGDEVREQ
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*学期编号*/
	
		public uint? dwReportStat;		/*报表状态*/
	
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
		public uint? dwUnitPrice;		/*大型仪器价格起点*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*贵重仪器设备表*/
	
	public struct BIGDEV
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*学期编号*/
	
		public uint? dwReportStat;		/*报表状态*/
	
		public uint? dwDevID;		/*设备ID*/
	
		public string szAssertSN;		/*用户方资产编号*/
	
		public string szClassSN;		/*设备分类号*/
	
		public string szDevName;		/*实验设备名称*/
	
		public uint? dwUnitPrice;		/*设备单价*/
	
		public string szModel;		/*设备型号*/
	
		public string szSpecification;		/*设备规格*/
	
		public string szAttendantName;		/*负责人姓名*/
	
		public uint? dwSampleNum;		/*测样数*/
	
		public uint? dwTUseTime;		/*教学机时*/
	
		public uint? dwRUseTime;		/*科研机时*/
	
		public uint? dwSUseTime;		/*社会机时*/
	
		public uint? dwOUseTime;		/*开放机时*/
	
		public uint? dwUseTeachers;		/*使用教师人数*/
	
		public uint? dwUseStudents;		/*使用学生人数*/
	
		public uint? dwUseOthers;		/*使用其他人数*/
	
		public uint? dwTItemNum;		/*教学实验项目*/
	
		public uint? dwRItemNum;		/*科研实验项目*/
	
		public uint? dwSItemNum;		/*社会实验项目*/
	
		public uint? dwNReward;		/*国家级奖励*/
	
		public uint? dwPReward;		/*省级奖励*/
	
		public uint? dwTPatent;		/*教师专利*/
	
		public uint? dwSPatent;		/*学生专利*/
	
		public uint? dwThreeIndex;		/*三大检索*/
	
		public uint? dwKernelJournal;		/*核心刊物*/
		};

	/*获取实验项目表*/
	
	public struct TESTITEMREPORTREQ
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*学期编号*/
	
		public uint? dwReportStat;		/*报表状态*/
	
		public string szLabIDs;		/*实验室ID,多个用逗号隔开*/
	
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*实验项目表*/
	
	public struct TESTITEMREPORT
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*学期编号*/
	
		public uint? dwReportStat;		/*报表状态*/
	
		public uint? dwTestCardID;		/*实验项目卡ID*/
	
		public string szTestSN;		/*实验编号*/
	
		public string szTestName;		/*实验名称*/
	
		public uint? dwTestClass;		/*实验类别*/
	
		public uint? dwTestKind;		/*实验类型*/
	
		public uint? dwRequirement;		/*实验要求*/
	
		public string szAcademicSubjectCode;		/*所属学科编码*/
	
		public uint? dwTesteeKind;		/*实验者类别*/
	
		public uint? dwGroupPeopleNum;		/*每组人数*/
	
		public uint? dwTestHour;		/*本实验项目学时数*/
	
		public uint? dwTesteeNum;		/*试验者人数*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabSN;		/*实验室编号*/
	
		public string szLabName;		/*实验室名称*/
		};

	/*专任实验室人员表*/
	
	public struct STAFFINFOREQ
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*学期编号*/
	
		public uint? dwReportStat;		/*报表状态*/
	
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
		public string szLabIDs;		/*实验室ID,多个用逗号隔开*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*专任实验室人员表*/
	
	public struct STAFFINFO
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*学期编号*/
	
		public uint? dwReportStat;		/*报表状态*/
	
		public uint? dwAccNo;		/*帐号*/
	
		public string szPID;		/*人员编号(学工号)*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabSN;		/*实验室编号*/
	
		public string szLabName;		/*实验室名称*/
	
		public string szTrueName;		/*姓名*/
	
		public uint? dwSex;		/*性别见UniCommon.h*/
	
		public uint? dwBirthDate;		/*出生日期*/
	
		public uint? dwJobTitle;		/*职称编码*/
	
		[FlagsAttribute]
		public enum DWJOBTITLE : uint
		{
			
				[EnumDescription("高级职称")]
				JTITLE_SENIOR = 1,
			
				[EnumDescription("中级职称")]
				JTITLE_MIDDLE = 2,
			
				[EnumDescription("其它")]
				JTITLE_OTHER = 0x100,
			
		}

	
		public uint? dwDuty;		/*职务*/
	
		[FlagsAttribute]
		public enum DWDUTY : uint
		{
			
				[EnumDescription("实验技术人员")]
				SDUTY_MANAGER = 1,
			
				[EnumDescription("教师")]
				SDUTY_TEACHER = 2,
			
				[EnumDescription("其它")]
				SDUTY_OTHER = 0x100,
			
		}

	
		public uint? dwJobType;		/*工作性质*/
	
		[FlagsAttribute]
		public enum DWJOBTYPE : uint
		{
			
				[EnumDescription("专职")]
				JOB_FULLTIME = 1,
			
				[EnumDescription("兼职")]
				JOB_PARTTIME = 2,
			
		}

	
		public string szAcademicSubjectCode;		/*所属学科编码*/
	
		public uint? dwProfessionalTitle;		/*专业技术职务*/
	
		public uint? dwEducation;		/*文化程度*/
	
		public uint? dwExpertType;		/*专家类别*/
	
		public uint? dwInlandUduTime;		/*国内学历教育时间*/
	
		public uint? dwInlandOtherTime;		/*国内非学历教育时间*/
	
		public uint? dwAbroadUduTime;		/*国外学历教育时间*/
	
		public uint? dwAbroadOtherTime;		/*国外非学历教育时间*/
	
		public string szMemo;		/*备注*/
		};

	/*实验室基本情况表*/
	
	public struct LABINFOREQ
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*学期编号*/
	
		public uint? dwReportStat;		/*报表状态*/
	
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
		public string szLabIDs;		/*实验室ID,多个用逗号隔开*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*实验室基本情况表*/
	
	public struct LABINFO
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*学期编号*/
	
		public uint? dwReportStat;		/*报表状态*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabSN;		/*实验室编号*/
	
		public string szLabName;		/*实验室名称*/
	
		public string szLabKindCode;		/*实验室类型编码*/
	
		public string szLabLevelCode;		/*实验室建设水平编码*/
	
		public string szLabFromCode;		/*实验室来源编码*/
	
		public string szAcademicSubjectCode;		/*所属学科编码*/
	
		public uint? dwLabClass;		/*实验室类别*/
	
		public uint? dwCreateDate;		/*建立年份*/
	
		public uint? dwTNReward;		/*教师国家级奖励*/
	
		public uint? dwTPReward;		/*教师省级奖励*/
	
		public uint? dwTPatent;		/*教师专利*/
	
		public uint? dwSNReward;		/*学生国家级奖励*/
	
		public uint? dwSPReward;		/*学生省级奖励*/
	
		public uint? dwSPatent;		/*学生专利*/
	
		public uint? dwTThreeIndex;		/*教学三大检索*/
	
		public uint? dwTKernelJournal;		/*教学核心期刊*/
	
		public uint? dwRThreeIndex;		/*科研三大检索*/
	
		public uint? dwRKernelJournal;		/*科研核心期刊*/
	
		public uint? dwTestBookNum;		/*实验教材数*/
	
		public uint? dwTItemNum;		/*教学实验项目*/
	
		public uint? dwRItemNum;		/*科研实验项目*/
	
		public uint? dwPTItemNum;		/*省部级以上教学实验项目*/
	
		public uint? dwPRItemNum;		/*省部级以上科研实验项目*/
	
		public uint? dwSItemNum;		/*社会实验项目*/
	
		public uint? dwZKThesisUsers;		/*专科论文人数*/
	
		public uint? dwBKThesisUsers;		/*本科论文人数*/
	
		public uint? dwSSThesisUsers;		/*硕士研究生论文人数*/
	
		public uint? dwBSThesisUsers;		/*博士研究生论文人数*/
	
		public uint? dwItemNum;		/*实验个数*/
	
		public uint? dwOtherItemNum;		/*校外实验个数*/
	
		public uint? dwUseUsers;		/*实验人数*/
	
		public uint? dwOtherUsers;		/*校外实验人数*/
	
		public uint? dwUseTime;		/*实验人时数*/
	
		public uint? dwOtherTime;		/*校外实验人时数*/
	
		public uint? dwPartTimeUsers;		/*兼职人员数*/
	
		public uint? dwTotalCost;		/*运行费用*/
	
		public uint? dwConsumeCost;		/*耗材费*/
		};

	/*实验室经费情况表*/
	
	public struct LABALLCOSTREQ
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*学期编号*/
	
		public uint? dwReportStat;		/*报表状态*/
	
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
		};

	/*实验室经费情况表*/
	
	public struct LABALLCOST
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*学期编号*/
	
		public uint? dwReportStat;		/*报表状态*/
	
		public uint? dwLabNum;		/*实验室个数*/
	
		public uint? dwLabArea;		/*实验室面积*/
	
		public uint? dwTotalCost;		/*总计(元)*/
	
		public uint? dwBuyCost;		/*购置费(元)*/
	
		public uint? dwTBuyCost;		/*教仪购置费(元)*/
	
		public uint? dwKeepCost;		/*维护费(元)*/
	
		public uint? dwTKeepCost;		/*教仪维护费(元)*/
	
		public uint? dwRunCost;		/*运行费(元)*/
	
		public uint? dwCRunCost;		/*耗材费(元)*/
	
		public uint? dwBuildCost;		/*建设费(元)*/
	
		public uint? dwRAndRCost;		/*研究与改革费(元)*/
	
		public uint? dwOtherCost;		/*其他费(元)*/
		};

	/*高等学校实验室综合信息表*/
	
	public struct LABSUMMARYREQ
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*学期编号*/
	
		public uint? dwReportStat;		/*报表状态*/
	
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
		public uint? dwUnitPrice;		/*设备单价*/
		};

	/*高等学校实验室综合信息表*/
	
	public struct LABSUMMARY
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*学期编号*/
	
		public uint? dwReportStat;		/*报表状态*/
	
		public uint? dwLabNum;		/*实验室个数*/
	
		public uint? dwLabArea;		/*实验室面积*/
	
		public uint? dwDevNum;		/*仪器数量*/
	
		public uint? dwDevMoney;		/*仪器金额*/
	
		public uint? dwBigDevNum;		/*大型仪器数量*/
	
		public uint? dwBigMoney;		/*大型仪器金额*/
	
		public uint? dwTItemNum;		/*教学实验项目*/
	
		public uint? dwTUseTime;		/*教学实验人时数*/
	
		public uint? dwDUseTime;		/*博士人时数*/
	
		public uint? dwMUseTime;		/*硕士人时数*/
	
		public uint? dwUUseTime;		/*本科人时数*/
	
		public uint? dwJUseTime;		/*专科人时数*/
	
		public uint? dwRItemNum;		/*科研实验项目*/
	
		public uint? dwHTStaff;		/*高级教师工作人员*/
	
		public uint? dwHSStaff;		/*高级实验技术人员*/
	
		public uint? dwMTStaff;		/*中级教师工作人员*/
	
		public uint? dwMSStaff;		/*中级实验技术人员*/
	
		public uint? dwOtherStaff;		/*其他人员*/
	
		public uint? dwPartTimeStaff;		/*兼职人员*/
	
		public uint? dwPaperNum;		/*论文数*/
	
		public uint? dwTReward;		/*教师获奖数*/
	
		public uint? dwSReward;		/*学生获奖数*/
		};

	/*高等学校实验室综合信息表2*/
	
	public struct LABSUMMARY2REQ
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*学期编号*/
	
		public uint? dwReportStat;		/*报表状态*/
	
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
		public uint? dwUnitPrice;		/*设备单价*/
		};

	/*高等学校实验室综合信息表2*/
	
	public struct LABSUMMARY2
	{
		private Reserved reserved;
		
		public uint? dwYearTerm;		/*学期编号*/
	
		public uint? dwReportStat;		/*报表状态*/
	
		public uint? dwLabNum;		/*实验室个数*/
	
		public uint? dwLabArea;		/*实验室面积*/
	
		public uint? dwDevNum;		/*仪器数量*/
	
		public uint? dwDevMoney;		/*仪器金额*/
	
		public uint? dwBigDevNum;		/*大型仪器数量*/
	
		public uint? dwBigMoney;		/*大型仪器金额*/
		};

	/*结束数据结构*/

	/*开始数据结构*/

	/*获取配置项请求包*/
	
	public struct CFGREQ
	{
		private Reserved reserved;
		
		public uint? dwGetType;		/*获取配置类别*/
	
		[FlagsAttribute]
		public enum DWGETTYPE : uint
		{
			
				[EnumDescription("配置类型")]
				CFGGET_KIND = 0x1,
			
		}

	
		public string szGetKey;		/*获取条件值*/
		};

	/*配置项内容*/
	
	public struct CFGINFO
	{
		private Reserved reserved;
		
		public uint? dwKindSN;		/*配置类型名称*/
	
		public uint? dwCfgSN;		/*配置项名称*/
	
		[FlagsAttribute]
		public enum DWCFGSN : uint
		{
			
				[EnumDescription("系统参数配置")]
				CFGKINDNAME_SYS = 0x100,
			
				[EnumDescription("机房上机参数配置")]
				CFGKINDNAME_CCM = 0x200,
			
				[EnumDescription("上网和软件监控的参数配置")]
				CFGKINDNAME_CTRL = 0x400,
			
				[EnumDescription("技术支持参数")]
				CFGKINDNAME_SUP = 0x800,
			
				[EnumDescription("网络故障处理，数值型,1可使用，0不可使用")]
				CFGCCM_NETFAULT = (CFGKINDNAME_SYS+1),
			
				[EnumDescription("注销时自动关机，数值型,1关机，0不关机")]
				CFGCCM_AUTOSHUTDOWN = (CFGKINDNAME_SYS+2),
			
				[EnumDescription("等待登录时间(分钟)")]
				CFGCCM_WAITLOGONTIME = (CFGKINDNAME_SYS+3),
			
				[EnumDescription("客户端空闲自动注销时间(分钟)")]
				CFGCCM_IDLELOGOUTTIME = (CFGKINDNAME_SYS+4),
			
				[EnumDescription("暂时离开保留时间(分钟)")]
				CFGLEAVE_HOLDTIME = (CFGKINDNAME_SYS+5),
			
				[EnumDescription("客户端注销保留时间(分钟)")]
				CFGLOGOUT_HOLDTIME = (CFGKINDNAME_SYS+6),
			
				[EnumDescription("预约结束等待刷卡确认时间(分钟)")]
				CFGRESVOVER_WAITCARDTIME = (CFGKINDNAME_SYS+7),
			
				[EnumDescription("智能监控每次持续时间(秒)")]
				CFGMONSTAT_DURATIONTIME = (CFGKINDNAME_SYS+8),
			
				[EnumDescription("智能监控作出状态改变的检测次数")]
				CFGMONSTAT_CHECKTIMESCHG = (CFGKINDNAME_SYS+9),
			
				[EnumDescription("资格审核范围类型")]
				CFGSFROLE_SCOPEKIND = (CFGKINDNAME_SYS+10),
			
				[EnumDescription("支持上传实验数据")]
				CFGTESTDATA_UPLOAD = (CFGKINDNAME_SYS+11),
			
				[EnumDescription("西式日历（周日为第一天)")]
				CFGCALENDA_WEST = (CFGKINDNAME_SYS+12),
			
				[EnumDescription("就餐保留时间(分钟)")]
				CFGDINNER_HOLDTIME = (CFGKINDNAME_SYS+13),
			
				[EnumDescription("午餐时间(HHMMHHMM 比如12001300)")]
				CFGDINNER_LUNCHTIME = (CFGKINDNAME_SYS+14),
			
				[EnumDescription("晚餐时间(HHMMHHMM 比如16001800)")]
				CFGDINNER_SUPPERTIME = (CFGKINDNAME_SYS+15),
			
				[EnumDescription("通道机刷卡允许预约提前生效时间(分钟)")]
				CFGCHGREV_ADVANCETIME = (CFGKINDNAME_SYS+16),
			
				[EnumDescription("门禁按钮(或内刷卡）不记录暂时离开")]
				CFGDOOR_CARDNOLEAVE = (CFGKINDNAME_SYS+17),
			
				[EnumDescription("暂时离开提前通知时间")]
				CFGLEAVEEND_NOTICETIME = (CFGKINDNAME_SYS+18),
			
				[EnumDescription("门禁刷卡通过第三方实时认证")]
				CFGDOORCARD_BYTHIRD = (CFGKINDNAME_SYS+19),
			
				[EnumDescription("通道机刷卡有效时间(分钟)")]
				CFGAUTOGATE_VALIDMIN = (CFGKINDNAME_SYS+20),
			
				[EnumDescription("禁止客户端修改密码")]
				CFGPASSWD_FORBID = (CFGKINDNAME_CCM+1),
			
				[EnumDescription("支持免费和收费模式选择")]
				CFGPASSWD_MULTIUSEMODE = (CFGKINDNAME_CCM+2),
			
				[EnumDescription("学生网络硬盘大小")]
				CFGCDISK_STUDENTSPACE = (CFGKINDNAME_CCM+3),
			
				[EnumDescription("教师网络硬盘大小")]
				CFGCDISK_TEACHERSPACE = (CFGKINDNAME_CCM+4),
			
				[EnumDescription("缺省监控类型,参考UNICTRLCLASS::dwCtrlMode")]
				CFGCTRL_URLCTRL = (CFGKINDNAME_CTRL+1),
			
				[EnumDescription("监控设定值,参考UNICTRLCLASS::dwCtrlMode")]
				CFGCTRL_URLCTRLPARAM = (CFGKINDNAME_CTRL+2),
			
				[EnumDescription("周未是否监控,0,不监控，1监控")]
				CFGCTRL_URLWEEKEND = (CFGKINDNAME_CTRL+3),
			
				[EnumDescription("是否记录上网日志,0,不记录，1记录")]
				CFGCTRL_URLLOGED = (CFGKINDNAME_CTRL+4),
			
				[EnumDescription("缺省监控类型,参考UNICTRLCLASS::dwCtrlMode")]
				CFGCTRL_SWCTRL = (CFGKINDNAME_CTRL+51),
			
				[EnumDescription("监控设定值,参考UNICTRLCLASS::dwCtrlMode")]
				CFGCTRL_SWCTRLPARAM = (CFGKINDNAME_CTRL+52),
			
				[EnumDescription("周未是否监控,0,不监控，1监控")]
				CFGCTRL_SWWEEKEND = (CFGKINDNAME_CTRL+53),
			
				[EnumDescription("是否记录程序运行日志,0,不记录，1记录")]
				CFGCTRL_SWLOGED = (CFGKINDNAME_CTRL+54),
			
				[EnumDescription("售后服务Email")]
				CFGSUP_EMAIL = (CFGKINDNAME_SUP+1),
			
				[EnumDescription("售后服务手机号")]
				CFGSUP_HANDPHONE = (CFGKINDNAME_SUP+2),
			
				[EnumDescription("客户缩写")]
				CFGSUP_CUSTOM = (CFGKINDNAME_SUP+3),
			
		}

	
		public uint? dwValueKind;		/*值类型*/
	
		[FlagsAttribute]
		public enum DWVALUEKIND : uint
		{
			
				[EnumDescription("数值型")]
				CFGVALUEKIND_NUMBER = 1,
			
				[EnumDescription("字符串型")]
				CFGVALUEKIND_STRING = 2,
			
		}

	
		public uint? dwNumberValue;		/*配置项值，数值型有效*/
	
		public string szStringValue;		/*配置项值，字符串型有效*/
	
		public string szMemo;		/*说明*/
		};

	/*获取信用类别*/
	
	public struct CREDITTYPEREQ
	{
		private Reserved reserved;
		
		public uint? dwCTSN;		/*信用类别编号*/
	
		public uint? dwCTStat;		/*状态*/
		};

	/*独立的信用类别*/
	
	public struct CREDITTYPE
	{
		private Reserved reserved;
		
		public uint? dwCTSN;		/*信用类别编号*/
	
		public string szCTName;		/*信用类别名称*/
	
		public uint? dwForClsKind;		/*适用设备类别*/
	
		public uint? dwUsePurpose;		/*用途*/
	
		public uint? dwMaxScore;		/*最大信用积分*/
	
		public uint? dwScoreCycle;		/*信用计分周期*/
	
		[FlagsAttribute]
		public enum DWSCORECYCLE : uint
		{
			
				[EnumDescription("每年")]
				SCORECYCLE_YEAR = 1,
			
				[EnumDescription("每学期")]
				SCORECYCLE_TERM = 2,
			
		}

	
		public uint? dwForbidUseTime;		/*信用积分为0禁止使用时间（天）*/
	
		public uint? dwCTStat;		/*状态*/
	
		[FlagsAttribute]
		public enum DWCTSTAT : uint
		{
			
				[EnumDescription("使用中")]
				CTSTAT_INUSE = 1,
			
				[EnumDescription("未使用")]
				CTSTAT_UNUSE = 2,
			
		}

	
		public string szMemo;		/*说明*/
		};

	/*信用类型*/
	
	public struct CREDITKIND
	{
		private Reserved reserved;
		
		public uint? dwCreditSN;		/*信用类型编号*/
	
		[FlagsAttribute]
		public enum DWCREDITSN : uint
		{
			
				[EnumDescription("预约不来")]
				CREDIT_RESVLATE = 1,
			
				[EnumDescription("使用率不达标")]
				CREDIT_USERATELOW = 2,
			
				[EnumDescription("外借不按时归还")]
				CREDIT_RETURNLATE = 3,
			
				[EnumDescription("损坏设备")]
				CREDIT_DAMAGEDEV = 4,
			
				[EnumDescription("取消预约")]
				CREDIT_RESVCANCEL = 5,
			
				[EnumDescription("预约结束未刷卡离开")]
				CREDIT_RESVENDNOCARD = 6,
			
				[EnumDescription("使用人数不达标")]
				CREDIT_USERLOW = 7,
			
				[EnumDescription("使用违规（人工处罚）")]
				CREDIT_DEREGULATION = 8,
			
				[EnumDescription("暂时离开未刷卡")]
				CREDIT_LEAVENOCARD = 9,
			
				[EnumDescription("暂时离开未按时返回")]
				CREDIT_LEAVENOBACK = 10,
			
				[EnumDescription("未按时参加活动违约")]
				CREDIT_ACTIVITYLATEORABSENT = 11,
			
				[EnumDescription("正常使用")]
				CREDIT_NORMALUSE = 1001,
			
				[EnumDescription("管理员纠错")]
				CREDIT_CORRECTERR = 2001,
			
		}

	
		public uint? dwScoreType;		/*积分处理方式*/
	
		[FlagsAttribute]
		public enum DWSCORETYPE : uint
		{
			
				[EnumDescription("扣信用积分")]
				SCORE_DEDUCT = 1,
			
				[EnumDescription("奖信用积分")]
				SCORE_AWARD = 2,
			
		}

	
		public uint? dwCKStat;		/*状态*/
	
		[FlagsAttribute]
		public enum DWCKSTAT : uint
		{
			
				[EnumDescription("使用中")]
				CREDITSTAT_INUSE = 1,
			
				[EnumDescription("未使用")]
				CREDITSTAT_UNUSE = 2,
			
		}

	
		public string szCreditName;		/*信用类型名称*/
	
		public string szMemo;		/*说明*/
		};

	/*获取信用计分表*/
	
	public struct CREDITSCOREREQ
	{
		private Reserved reserved;
		
		public uint? dwID;		/*ID*/
	
		public uint? dwCTSN;		/*信用类别编号*/
	
		public uint? dwCreditSN;		/*信用类型编号*/
	
		public uint? dwForClsKind;		/*适用设备类别*/
	
		public uint? dwUsePurpose;		/*用途*/
		};

	/*信用计分表*/
	
	public struct CREDITSCORE
	{
		private Reserved reserved;
		
		public uint? dwID;		/*ID*/
	
		public uint? dwCTSN;		/*信用类别编号*/
	
		public string szCTName;		/*信用类别名称*/
	
		public uint? dwForClsKind;		/*适用设备类别*/
	
		public uint? dwUsePurpose;		/*用途*/
	
		public uint? dwMaxScore;		/*最大信用积分*/
	
		public uint? dwScoreCycle;		/*信用计分周期*/
	
		public uint? dwForbidUseTime;		/*信用积分为0禁止使用时间（天）*/
	
		public uint? dwCreditSN;		/*信用类型编号*/
	
		public string szCreditName;		/*信用类型名称*/
	
		public uint? dwScoreType;		/*积分处理方式*/
	
		public uint? dwUseNum;		/*条件启用段数*/
	
		public uint? dwMinValue1;		/*满足条件最小值1*/
	
		public uint? dwMaxValue1;		/*满足条件最大值1*/
	
		public uint? dwCreditScore1;		/*扣或奖积分1*/
	
		public uint? dwMinValue2;		/*满足条件最小值2*/
	
		public uint? dwMaxValue2;		/*满足条件最大值2*/
	
		public uint? dwCreditScore2;		/*扣或奖积分2*/
	
		public uint? dwMinValue3;		/*满足条件最小值3*/
	
		public uint? dwMaxValue3;		/*满足条件最大值3*/
	
		public uint? dwCreditScore3;		/*扣或奖积分3*/
	
		public uint? dwMinValue4;		/*满足条件最小值4*/
	
		public uint? dwMaxValue4;		/*满足条件最大值4*/
	
		public uint? dwCreditScore4;		/*扣或奖积分4*/
	
		public uint? dwMinValue5;		/*满足条件最小值5*/
	
		public uint? dwMaxValue5;		/*满足条件最大值5*/
	
		public uint? dwCreditScore5;		/*扣或奖积分*/
	
		public uint? dwMinValue6;		/*满足条件最小值6*/
	
		public uint? dwMaxValue6;		/*满足条件最大值6*/
	
		public uint? dwCreditScore6;		/*扣或奖积分6*/
	
		public string szMemo;		/*说明*/
		};

	/*获取我的信用积分*/
	
	public struct MYCREDITSCOREREQ
	{
		private Reserved reserved;
		
		public uint? dwAccNo;		/*账号*/
	
		public uint? dwCTSN;		/*信用类别编号*/
		};

	/*我的信用积分*/
	
	public struct MYCREDITSCORE
	{
		private Reserved reserved;
		
		public uint? dwAccNo;		/*账号*/
	
		public string szPID;		/*学工号*/
	
		public string szTrueName;		/*姓名*/
	
		public uint? dwCTSN;		/*信用类别编号*/
	
		public string szCTName;		/*信用类别名称*/
	
		public uint? dwForClsKind;		/*适用设备类别*/
	
		public uint? dwUsePurpose;		/*用途*/
	
		public uint? dwMaxScore;		/*最大信用积分*/
	
		public uint? dwScoreCycle;		/*信用计分周期*/
	
		public uint? dwForbidUseTime;		/*信用积分为0禁止使用时间（天）*/
	
		public uint? dwLeftCScore;		/*剩余积分*/
	
		public uint? dwForbidStartDate;		/*禁用开始日期*/
	
		public uint? dwForbidEndDate;		/*禁用结束日期*/
	
		public string szMemo;		/*说明*/
		};

	/*人工信用管理*/
	
	public struct ADMINCREDIT
	{
		private Reserved reserved;
		
		public uint? dwCTSN;		/*信用类别编号*/
	
		public uint? dwCreditSN;		/*信用类型编号*/
	
		public uint? dwCreditScore;		/*扣或奖积分*/
	
		public uint? dwSubjectID;		/*关联的ID*/
	
		public uint? dwAccNo;		/*账号*/
	
		public string szTrueName;		/*姓名*/
	
		public string szReason;		/*原因或理由*/
	
		public uint? dwPara1;		/*参数1（扩展）*/
	
		public uint? dwPara2;		/*参数2（扩展）*/
	
		public string szMemo;		/*说明*/
		};

	/*信用记录请求*/
	
	public struct CREDITRECREQ
	{
		private Reserved reserved;
		
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
		public uint? dwSID;		/*流水号*/
	
		public uint? dwAccNo;		/*账号*/
	
		public uint? dwCTSN;		/*信用类别编号*/
	
		public uint? dwCreditSN;		/*信用类型编号*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*信用记录*/
	
	public struct CREDITREC
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*SID*/
	
		public uint? dwCTSN;		/*信用类别编号*/
	
		public string szCTName;		/*信用类别名称*/
	
		public uint? dwCreditSN;		/*信用类型编号*/
	
		public string szCreditName;		/*信用类型名称*/
	
		public uint? dwScoreType;		/*积分处理方式*/
	
		public uint? dwAccNo;		/*账号*/
	
		public string szPID;		/*学工号*/
	
		public string szTrueName;		/*真实姓名*/
	
		public uint? dwTutorID;		/*导师（帐号）*/
	
		public string szTutorName;		/*导师姓名*/
	
		public uint? dwDeptID;		/*部门ID*/
	
		public string szDeptName;		/*部门*/
	
		public uint? dwDevID;		/*设备ID*/
	
		public string szDevName;		/*设备名称*/
	
		public uint? dwRoomID;		/*房间ID*/
	
		public string szRoomName;		/*房间称*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabName;		/*实验室名称*/
	
		public uint? dwAttendantID;		/*值班员账号*/
	
		public string szAttendantName;		/*值班员姓名*/
	
		public string szAttendantTel;		/*值班员电话*/
	
		public uint? dwSubjectID;		/*事由ID*/
	
		public uint? dwOccurDate;		/*发生日期*/
	
		public uint? dwOccurTime;		/*发生时间*/
	
		public uint? dwThisUseCScore;		/*本次使用积分*/
	
		public uint? dwLeftCScore;		/*累计分数*/
	
		public uint? dwUserCStat;		/*用户信用状态*/
	
		[FlagsAttribute]
		public enum DWUSERCSTAT : uint
		{
			
				[EnumDescription("有效")]
				USERCSTAT_VALID = 1,
			
				[EnumDescription("管理员取消")]
				USERCSTAT_CANCEL = 2,
			
				[EnumDescription("已过积分周期")]
				USERCSTAT_OVER = 4,
			
		}

	
		public uint? dwForbidStartDate;		/*禁用开始时间*/
	
		public uint? dwForbidEndDate;		/*禁用结束时间*/
	
		public string szMemo;		/*说明*/
		};

	/*获取系统功能请求*/
	
	public struct SYSFUNCREQ
	{
		private Reserved reserved;
		
		public uint? dwSFSN;		/*功能编号*/
		};

	/*系统功能定义*/
	
	public struct SYSFUNC
	{
		private Reserved reserved;
		
		public uint? dwSFSN;		/*功能编号*/
	
		public string szSFName;		/*功能名称*/
	
		public string szURL;		/*使用详细介绍的URL*/
	
		public string szMemo;		/*说明*/
		};

	/*获取资格类别*/
	
	public struct SYSFUNCRULEREQ
	{
		private Reserved reserved;
		
		public uint? dwSFRuleID;		/*功能使用规则ID*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public uint? dwAuthType;		/*授权类别*/
	
		public uint? dwScopeKind;		/*适用范围类型*/
	
		public uint? dwScopeID;		/*范围ID(根据dwScopeKind含义不同)*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*系统功能使用规则*/
	
	public struct SYSFUNCRULE
	{
		private Reserved reserved;
		
		public uint? dwSFRuleID;		/*功能使用规则ID*/
	
		public string szSFRuleName;		/*规则名称*/
	
		public uint? dwSFSN;		/*功能编号*/
	
		public string szSFName;		/*功能名称*/
	
		public string szSFURL;		/*使用详细介绍的URL*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabName;		/*实验室名称*/
	
		public uint? dwScopeKind;		/*适用范围类型*/
	
		[FlagsAttribute]
		public enum DWSCOPEKIND : uint
		{
			
				[EnumDescription("实验室级")]
				SFSCOPE_LAB = 0x1,
			
				[EnumDescription("房间级")]
				SFSCOPE_ROOM = 0x2,
			
				[EnumDescription("设备类型")]
				SFSCOPE_DEVKIND = 0x4,
			
				[EnumDescription("设备")]
				SFSCOPE_DEVID = 0x8,
			
		}

	
		public uint? dwScopeID;		/*范围ID(根据dwScopeKind含义不同)*/
	
		public uint? dwIdent;		/*身份（0表示无限制）*/
	
		public uint? dwDeptID;		/*部门（0表示无限制）*/
	
		public uint? dwGroupID;		/*指定用户组（0表示无限制）*/
	
		public uint? dwPriority;		/*优先级(数字大代表优先级高)*/
	
		public uint? dwAuthType;		/*授权类别*/
	
		[FlagsAttribute]
		public enum DWAUTHTYPE : uint
		{
			
				[EnumDescription("使用资格许可")]
				AUTHBY_USER = 0x1,
			
				[EnumDescription("科研实验项目许可")]
				AUTHBY_REARCHTEST = 0x2,
			
		}

	
		public uint? dwAuthMode;		/*授权模式*/
	
		[FlagsAttribute]
		public enum DWAUTHMODE : uint
		{
			
				[EnumDescription("自动授予")]
				SFMODE_AUTO = 0x1,
			
				[EnumDescription("需要申请")]
				SFMODE_NEEDAPPLY = 0x2,
			
		}

	
		public string szIntrInfo;		/*使用说明*/
	
		public uint? dwDefaultPeriod;		/*缺损有效期限(天)*/
	
		public string szMemo;		/*说明*/
		};

	/*获取用户系统功能资格表*/
	
	public struct SFROLEINFOREQ
	{
		private Reserved reserved;
		
		public uint? dwSFSN;		/*功能编号*/
	
		public uint? dwSFRuleID;		/*功能使用规则ID*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public uint? dwScopeKind;		/*适用范围类型*/
	
		public uint? dwScopeID;		/*范围ID(根据dwScopeKind含义不同)*/
	
		public uint? dwStatus;		/*状态*/
	
		public uint? dwAuthType;		/*授权类别*/
	
		public uint? dwApplyID;		/*申请ID*/
	
		public uint? dwAccNo;		/*账号*/
	
		public uint? dwTargetID;		/*申请对象ID(使用人账号或科研项目ID号）*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*用户系统功能资格表*/
	
	public struct SFROLEINFO
	{
		private Reserved reserved;
		
		public uint? dwSFSN;		/*功能编号*/
	
		public string szSFName;		/*功能名称*/
	
		public string szSFURL;		/*使用详细介绍的URL*/
	
		public uint? dwSFRuleID;		/*功能使用规则ID*/
	
		public string szSFRuleName;		/*规则名称*/
	
		public string szIntrInfo;		/*使用说明*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabName;		/*实验室名称*/
	
		public uint? dwAuthType;		/*授权类别*/
	
		public uint? dwStatus;		/*状态（前8种管理员审核状态）*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("自动开通（可使用）")]
				SFROLESTAT_AUTO = 0x1000,
			
				[EnumDescription("未申请")]
				SFROLESTAT_NOAPPLY = 0x2000,
			
				[EnumDescription("审核拒绝(不可再申请)")]
				SFROLESTAT_CHECKREJECT = 0x4000,
			
				[EnumDescription("无权限(不可申请)")]
				SFROLESTAT_FORBID = 0x8000,
			
				[EnumDescription("已过期")]
				SFROLESTAT_EXPIRED = 0x1000000,
			
		}

	
		public uint? dwApplyID;		/*申请ID*/
	
		public uint? dwAccNo;		/*账号*/
	
		public string szPID;		/*学工号*/
	
		public string szTrueName;		/*姓名*/
	
		public uint? dwDeptID;		/*部门ID*/
	
		public string szDeptName;		/*部门*/
	
		public string szTel;		/*电话*/
	
		public string szHandPhone;		/*手机*/
	
		public string szEmail;		/*电邮*/
	
		public uint? dwTutorID;		/*导师账号*/
	
		public string szTutorName;		/*导师姓名*/
	
		public uint? dwTargetID;		/*申请对象ID(使用人账号或科研项目ID号）*/
	
		public string szTargetName;		/*申请对象名称(使用人姓名或科研项目名称）*/
	
		public uint? dwApplyDate;		/*申请日期*/
	
		public uint? dwApplyTime;		/*申请时间*/
	
		public uint? dwApplyUseTime;		/*申请使用时间（分钟）*/
	
		public uint? dwTesteeNum;		/*使用人数*/
	
		public uint? dwUseTimes;		/*申请使用次数*/
	
		public uint? dwUseMinATime;		/*申请每次使用时长(分钟)*/
	
		public string szApplyInfo;		/*详细介绍*/
	
		public string szApplyURL;		/*附带申请报告的URL*/
	
		public uint? dwAdminID;		/*管理员账号*/
	
		public uint? dwCheckDate;		/*审核日期*/
	
		public uint? dwCheckTime;		/*审核时间*/
	
		public uint? dwPermitUseTime;		/*允许使用时间（分钟）*/
	
		public uint? dwDeadLine;		/*允许截止时间*/
	
		public string szCheckInfo;		/*审核意见*/
	
		public uint? dwUsedTimes;		/*已使用次数*/
	
		public uint? dwUsedTime;		/*已经使用时间（分钟）*/
	
		public string szMemo;		/*说明*/
		};

	/*获取编码信息的请求包*/
	
	public struct CODINGTABLEREQ
	{
		private Reserved reserved;
		
		public uint? dwCodeType;		/*编码类别*/
	
		public string szCodeSN;		/*编码*/
	
		public string szCodeName;		/*编码名称*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*编码信息表*/
	
	public struct CODINGTABLE
	{
		private Reserved reserved;
		
		public uint? dwCodeType;		/*编码类别*/
	
		[FlagsAttribute]
		public enum DWCODETYPE : uint
		{
			
				[EnumDescription("实验室类型")]
				CODE_LABKIND = 1,
			
				[EnumDescription("实验室来源")]
				CODE_LABFROM = 2,
			
				[EnumDescription("实验室建设水平")]
				CODE_LABLEVEL = 3,
			
				[EnumDescription("学科管理")]
				CODE_ACADEMICSUBJECT = 4,
			
				[EnumDescription("设备用途")]
				CODE_DEVFUNC = 5,
			
				[EnumDescription("场馆预约类型")]
				CODE_YARDRESVKIND = 6,
			
				[EnumDescription("预约类型")]
				CODE_RESVKIND = 7,
			
				[EnumDescription("活动类型")]
				CODE_ACTIVITYKIND = 8,
			
				[EnumDescription("预约服务")]
				CODE_RESVSEIVICE = 9,
			
		}

	
		public string szCodeSN;		/*编码*/
	
		public string szCodeName;		/*编码名称*/
	
		public string szExtValue;		/*扩展*/
	
		public string szMemo;		/*备注*/
		};

	/*获取多语言包的请求包*/
	
	public struct MULTILANLIBREQ
	{
		private Reserved reserved;
		
		public uint? dwLanSN;		/*语言编号*/
	
		public uint? dwSubSysSN;		/*子系统编号*/
	
		public uint? dwTextID;		/*文字ID*/
		};

	/*多语言包*/
	
	public struct UNIMULTILANLIB
	{
		private Reserved reserved;
		
		public uint? dwLanSN;		/*语言编号*/
	
		[FlagsAttribute]
		public enum DWLANSN : uint
		{
			
				[EnumDescription("中文")]
				LAN_CHINESE = 1,
			
				[EnumDescription("英文")]
				LAN_ENGLISH = 2,
			
				[EnumDescription("最大语言编码")]
				MAXLAN_SN = 2,
			
		}

	
		public uint? dwSubSysSN;		/*子系统编号*/
	
		[FlagsAttribute]
		public enum DWSUBSYSSN : uint
		{
			
				[EnumDescription("服务器")]
				SUBSYS_SERVER = 1,
			
				[EnumDescription("管理端")]
				SUBSYS_MANAGER = 2,
			
				[EnumDescription("刷卡端")]
				SUBSYS_CARD = 3,
			
				[EnumDescription("打印驱动")]
				SUBSYS_DRIVER = 4,
			
		}

	
		public uint? dwTextID;		/*文字ID*/
	
		[FlagsAttribute]
		public enum DWTEXTID : uint
		{
			
				[EnumDescription("TextID最大值")]
				MAX_TEXTID = 10000000,
			
		}

	
		public string szTextInfo;		/*文字内容*/
	
		public string szMemo;		/*备注*/
		};

	/*系统刷新请求*/
	
	public struct SYSREFRESHREQ
	{
		private Reserved reserved;
		
		public uint? dwRefreshMod;		/*刷新模块(扩展)*/
		};

	/*结束数据结构*/

	/*开始数据结构*/

	/*获取资产列表*/
	
	public struct ASSERTREQ
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*设备ID*/
	
		public string szDevName;		/*实验设备名称*/
	
		public string szAssertSN;		/*资产编号*/
	
		public string szTagID;		/*RFID标签ID*/
	
		public string szLabIDs;		/*实验室ID,多个用逗号隔开*/
	
		public string szRoomIDs;		/*房间ID,多个用逗号隔开*/
	
		public string szKindIDs;		/*所属类型,多个用逗号隔开*/
	
		public string szClassIDs;		/*所属功能类别ID,多个用逗号隔开*/
	
		public string szDeptIDs;		/*学院ID,多个用逗号隔开*/
	
		public string szBuildingIDs;		/*楼宇ID,多个用逗号隔开*/
	
		public string szCampusIDs;		/*校区ID,多个用逗号隔开*/
	
		public uint? dwDevStat;		/*设备状态*/
	
		public uint? dwProperty;		/*设备属性*/
	
		public uint? dwClassKind;		/*类别(见UNIDEVCLS的Kind定义)*/
	
		public uint? dwMinUnitPrice;		/*最低价格*/
	
		public uint? dwMaxUnitPrice;		/*最大价格*/
	
		public uint? dwSPurchaseDate;		/*开始采购日期*/
	
		public uint? dwEPurchaseDate;		/*截止采购日期*/
	
		public uint? dwKeeperID;		/*责任人账号*/
	
		public string szKeeperName;		/*责任人姓名*/
	
		public uint? dwProducerID;		/*生产商ID*/
	
		public string szProducerName;		/*生产商名称*/
	
		public uint? dwSellerID;		/*供应商ID*/
	
		public string szSellerName;		/*供应商名称*/
	
		public uint? dwServiceID;		/*维保单位ID*/
	
		public string szServiceName;		/*维保单位名称*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*资产房间变更*/
	
	public struct ROOMCHG
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*设备ID*/
	
		public uint? dwOldRoomID;		/*旧房间ID*/
	
		public string szOldRoomName;		/*旧房间名称*/
	
		public uint? dwNewRoomID;		/*新房间ID*/
	
		public string szNewRoomName;		/*新房间名称*/
	
		public string szMemo;		/*说明信息*/
		};

	/*资产责任人变更*/
	
	public struct KEEPERCHG
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*设备ID*/
	
		public uint? dwOldKeeperID;		/*旧责任人账号*/
	
		public string szOldKeeperName;		/*旧责任人姓名*/
	
		public uint? dwNewKeeperID;		/*新责任人账号*/
	
		public string szNewKeeperName;		/*新责任人姓名*/
	
		public string szMemo;		/*说明信息*/
		};

	/*资产信息*/
	
	public struct UNIASSERT
	{
		private Reserved reserved;
		
		public uint? dwDevID;		/*设备ID*/
	
		public string szAssertSN;		/*用户方资产编号*/
	
		public string szTagID;		/*RFID标签ID*/
	
		public string szOriginSN;		/*原厂系列号*/
	
		public string szDevName;		/*实验设备名称*/
	
		public uint? dwUnitPrice;		/*设备单价*/
	
		public uint? dwPurchaseDate;		/*采购日期 YYYYMMDD*/
	
		public uint? dwDevStat;		/*设备状态*/
	
		public uint? dwClassID;		/*设备功能类别*/
	
		public string szClassSN;		/*设备分类号*/
	
		public string szClassName;		/*设备类别名称*/
	
		public uint? dwClassKind;		/*类别(见UNIDEVCLS的Kind定义)*/
	
		public uint? dwKindID;		/*设备类型*/
	
		public string szKindName;		/*设备名称*/
	
		public string szFuncCode;		/*设备功能用途编码*/
	
		public string szModel;		/*设备型号*/
	
		public string szSpecification;		/*设备规格*/
	
		public uint? dwProperty;		/*设备属性（前16种为UNIDEVKIND定义*/
	
		public uint? dwRoomID;		/*房间ID*/
	
		public string szRoomNo;		/*房间号（不重复,用于关联门禁，格式B-F-N,建筑编号-楼层号-房间编号）*/
	
		public string szRoomName;		/*房间名称*/
	
		public string szFloorNo;		/*所在楼层*/
	
		public uint? dwBuildingID;		/*楼宇ID*/
	
		public string szBuildingNo;		/*大楼编号(*/
	
		public string szBuildingName;		/*大楼名称(*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabSN;		/*实验室编号*/
	
		public string szLabName;		/*实验室名称*/
	
		public uint? dwDeptID;		/*部门ID*/
	
		public string szDeptName;		/*部门*/
	
		public uint? dwCampusID;		/*校区ID*/
	
		public string szCampusName;		/*校区名称*/
	
		public uint? dwCampusKind;		/*校区类型（扩展）*/
	
		public uint? dwKeeperID;		/*责任人账号*/
	
		public string szKeeperName;		/*责任人姓名*/
	
		public string szKeeperTel;		/*责任人电话*/
	
		public uint? dwProducerID;		/*生产商ID*/
	
		public string szProducerName;		/*生产商名称*/
	
		public uint? dwSellerID;		/*供应商ID*/
	
		public string szSellerName;		/*供应商名称*/
	
		public uint? dwServiceID;		/*维保单位ID*/
	
		public string szServiceName;		/*维保单位名称*/
	
		public string szServiceTel;		/*维保电话*/
	
		public string szMemo;		/*说明信息*/
		};

	/*资产发卡*/
	
	public struct RFIDBIND
	{
		private Reserved reserved;
		
		public uint? dwLabID;		/*实验室ID*/
	
		public uint? dwDevID;		/*设备ID*/
	
		public string szTagID;		/*RFID标签ID*/
	
		public string szMemo;		/*说明信息*/
		};

	/*获取资产盘点表*/
	
	public struct STOCKTAKINGREQ
	{
		private Reserved reserved;
		
		public uint? dwSTID;		/*资产盘点ID*/
	
		public uint? dwSTStat;		/*盘点状态*/
	
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*资产盘点表*/
	
	public struct STOCKTAKING
	{
		private Reserved reserved;
		
		public uint? dwSTID;		/*资产盘点ID*/
	
		public uint? dwSTDate;		/*资产盘点日期*/
	
		public uint? dwSTEndDate;		/*资产盘点结束日期*/
	
		public uint? dwSTStat;		/*盘点状态*/
	
		[FlagsAttribute]
		public enum DWSTSTAT : uint
		{
			
				[EnumDescription("盘点中")]
				STSTAT_DOING = 0x1,
			
				[EnumDescription("盘点结束")]
				STSTAT_DONE = 0x2,
			
		}

	
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabName;		/*实验室名称*/
	
		public uint? dwRoomID;		/*房间ID*/
	
		public string szRoomName;		/*房间名称*/
	
		public uint? dwKindID;		/*所属类型*/
	
		public string szKindName;		/*设备名称*/
	
		public uint? dwClassID;		/*所属功能类别ID*/
	
		public string szClassName;		/*设备类别名称*/
	
		public uint? dwClassKind;		/*类别(见UNIDEVCLS的Kind定义)*/
	
		public uint? dwAttendantID;		/*责任人ID*/
	
		public string szAttendantName;		/*责任人姓名*/
	
		public uint? dwMinUnitPrice;		/*最低价格*/
	
		public uint? dwMaxUnitPrice;		/*最大价格*/
	
		public uint? dwLeaderID;		/*盘点负责人ID*/
	
		public string szLeaderName;		/*盘点负责人姓名*/
	
		public string szMemo;		/*说明信息*/
		};

	/*获取盘点资产明细表*/
	
	public struct STDETAILREQ
	{
		private Reserved reserved;
		
		public uint? dwSTID;		/*资产盘点ID*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*资产盘点详细信息*/
	
	public struct STDETAIL
	{
		private Reserved reserved;
		
		public uint? dwSTID;		/*资产盘点ID*/
	
		public uint? dwDevID;		/*设备ID*/
	
		public uint? dwSTStat;		/*盘点状态*/
	
		[FlagsAttribute]
		public enum DWSTSTAT : uint
		{
			
				[EnumDescription("等待盘点")]
				STSTAT_WAITING = 0x100,
			
				[EnumDescription("盘点正常")]
				STSTAT_OK = 0x200,
			
				[EnumDescription("盘点存在问题")]
				STSTAT_PROBLEM = 0x1000,
			
		}

	
		public uint? dwSTDate;		/*资产盘点日期*/
	
		public uint? dwLeaderID;		/*盘点负责人ID*/
	
		public string szLeaderName;		/*盘点负责人姓名*/
	
		public uint? dwRoomID;		/*房间ID*/
	
		public uint? dwAttendantID;		/*责任人账号*/
	
		public string szAttendantName;		/*责任人姓名*/
	
		public string szSTInfo;		/*盘点情况描述*/
	
		public string szAssertSN;		/*用户方资产编号*/
	
		public string szTagID;		/*RFID标签ID*/
	
		public string szOriginSN;		/*原厂系列号*/
	
		public string szDevName;		/*实验设备名称*/
	
		public uint? dwUnitPrice;		/*设备单价*/
	
		public uint? dwPurchaseDate;		/*采购日期 YYYYMMDD*/
	
		public uint? dwClassID;		/*设备功能类别*/
	
		public string szClassSN;		/*设备分类号*/
	
		public string szClassName;		/*设备类别名称*/
	
		public uint? dwClassKind;		/*类别(见UNIDEVCLS的Kind定义)*/
	
		public uint? dwKindID;		/*设备类型*/
	
		public string szKindName;		/*设备名称*/
	
		public string szFuncCode;		/*设备功能用途编码*/
	
		public string szModel;		/*设备型号*/
	
		public string szSpecification;		/*设备规格*/
	
		public uint? dwProperty;		/*设备属性（前16种为UNIDEVKIND定义*/
	
		public string szRoomNo;		/*房间号（不重复,用于关联门禁，格式B-F-N,建筑编号-楼层号-房间编号）*/
	
		public string szRoomName;		/*房间名称*/
	
		public string szFloorNo;		/*所在楼层*/
	
		public uint? dwBuildingID;		/*楼宇ID*/
	
		public string szBuildingNo;		/*大楼编号(*/
	
		public string szBuildingName;		/*大楼名称(*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabSN;		/*实验室编号*/
	
		public string szLabName;		/*实验室名称*/
	
		public uint? dwDeptID;		/*部门ID*/
	
		public string szDeptName;		/*部门*/
	
		public uint? dwCampusID;		/*校区ID*/
	
		public string szCampusName;		/*校区名称*/
	
		public uint? dwCampusKind;		/*校区类型（扩展）*/
	
		public string szMemo;		/*说明信息*/
		};

	/*获取设备报废记录表*/
	
	public struct OUTOFSERVICEREQ
	{
		private Reserved reserved;
		
		public uint? dwOOSID;		/*设备报废记录ID*/
	
		public uint? dwOOSStat;		/*报废状态*/
	
		public uint? dwOOSType;		/*报废类型*/
	
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*报废设备*/
	
	public struct OOSDEV
	{
		private Reserved reserved;
		
		public uint? dwOOSID;		/*设备报废记录ID*/
	
		public uint? dwDevID;		/*设备ID*/
	
		public string szAssertSN;		/*用户方资产编号*/
	
		public string szDevName;		/*实验设备名称*/
	
		public uint? dwUnitPrice;		/*设备单价*/
	
		public uint? dwPurchaseDate;		/*采购日期 YYYYMMDD*/
	
		public string szKindName;		/*设备名称*/
	
		public string szRoomName;		/*房间名称*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabName;		/*实验室名称*/
	
		public string szMemo;		/*说明信息*/
		};

	/*设备报废记录表*/
	
	public struct OUTOFSERVICE
	{
		private Reserved reserved;
		
		public uint? dwOOSID;		/*设备报废记录ID*/
	
		public uint? dwOOSStat;		/*报废状态*/
	
		[FlagsAttribute]
		public enum DWOOSSTAT : uint
		{
			
				[EnumDescription("已申请")]
				OOSSTAT_APPLY = 0x1,
			
				[EnumDescription("已批准")]
				OOSSTAT_APPROVE = 0x2,
			
				[EnumDescription("不批准")]
				OOSSTAT_REJECT = 0x4,
			
		}

	
		public uint? dwOOSType;		/*报废类型*/
	
		public string szOOSInfo;		/*报废信息*/
	
		public uint? dwApplyDate;		/*申请日期*/
	
		public uint? dwApplyID;		/*申请人ID*/
	
		public string szApplyName;		/*申请人姓名*/
	
		public uint? dwApproveDate;		/*申请日期*/
	
		public uint? dwApproveID;		/*申请人ID*/
	
		public string szApproveName;		/*申请人姓名*/
	
		public string szMemo;		/*说明信息*/
	
	public OOSDEV[] OOSDev;		/*CUniTable[OOSDEV]*/
		};

	/*获取报废设备明细表*/
	
	public struct OOSDETAILREQ
	{
		private Reserved reserved;
		
		public uint? dwOOSID;		/*设备报废记录ID*/
	
		public uint? dwOOSStat;		/*报废状态*/
	
		public uint? dwOOSType;		/*报废类型*/
	
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*报废设备明细*/
	
	public struct OOSDETAIL
	{
		private Reserved reserved;
		
		public uint? dwOOSID;		/*设备报废记录ID*/
	
		public uint? dwOOSStat;		/*报废状态*/
	
		public uint? dwOOSType;		/*报废类型*/
	
		public string szOOSInfo;		/*报废信息*/
	
		public uint? dwApplyDate;		/*申请日期*/
	
		public uint? dwApplyID;		/*申请人ID*/
	
		public string szApplyName;		/*申请人姓名*/
	
		public uint? dwApproveDate;		/*申请日期*/
	
		public uint? dwApproveID;		/*申请人ID*/
	
		public string szApproveName;		/*申请人姓名*/
	
		public uint? dwDevID;		/*设备ID*/
	
		public string szAssertSN;		/*用户方资产编号*/
	
		public string szTagID;		/*RFID标签ID*/
	
		public string szOriginSN;		/*原厂系列号*/
	
		public string szDevName;		/*实验设备名称*/
	
		public uint? dwUnitPrice;		/*设备单价*/
	
		public uint? dwPurchaseDate;		/*采购日期 YYYYMMDD*/
	
		public uint? dwClassID;		/*设备功能类别*/
	
		public string szClassSN;		/*设备分类号*/
	
		public string szClassName;		/*设备类别名称*/
	
		public uint? dwClassKind;		/*类别(见UNIDEVCLS的Kind定义)*/
	
		public uint? dwKindID;		/*设备类型*/
	
		public string szKindName;		/*设备名称*/
	
		public string szFuncCode;		/*设备功能用途编码*/
	
		public string szModel;		/*设备型号*/
	
		public string szSpecification;		/*设备规格*/
	
		public uint? dwProperty;		/*设备属性（前16种为UNIDEVKIND定义*/
	
		public uint? dwRoomID;		/*房间ID*/
	
		public string szRoomNo;		/*房间号（不重复,用于关联门禁，格式B-F-N,建筑编号-楼层号-房间编号）*/
	
		public string szRoomName;		/*房间名称*/
	
		public string szFloorNo;		/*所在楼层*/
	
		public uint? dwBuildingID;		/*楼宇ID*/
	
		public string szBuildingNo;		/*大楼编号(*/
	
		public string szBuildingName;		/*大楼名称(*/
	
		public uint? dwLabID;		/*实验室ID*/
	
		public string szLabSN;		/*实验室编号*/
	
		public string szLabName;		/*实验室名称*/
	
		public uint? dwDeptID;		/*部门ID*/
	
		public string szDeptName;		/*部门*/
	
		public uint? dwCampusID;		/*校区ID*/
	
		public string szCampusName;		/*校区名称*/
	
		public uint? dwCampusKind;		/*校区类型（扩展）*/
	
		public string szMemo;		/*说明信息*/
		};

	/*设备保修申请*/
	
	public struct REPAIRAPPLY
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*保修ID*/
	
		public uint? dwDevID;		/*设备ID*/
	
		public string szDevName;		/*设备名称*/
	
		public string szAssertSN;		/*用户方资产编号*/
	
		public uint? dwDamageDate;		/*损坏日期*/
	
		public uint? dwDamageTime;		/*损坏时间*/
	
		public string szDamageInfo;		/*损坏说明*/
	
		public uint? dwManID;		/*经办人ID*/
	
		public string szManName;		/*经办人姓名*/
	
		public string szMemo;		/*说明*/
		};

	/**/
	
	public struct REPAIROVER
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*SID*/
	
		public uint? dwDevID;		/*设备ID*/
	
		public string szDevName;		/*设备名称*/
	
		public string szAssertSN;		/*用户方资产编号*/
	
		public uint? dwStatus;		/*DEVDAMAGEREC定义*/
	
		public uint? dwRepareDate;		/*维修日期*/
	
		public uint? dwRepareTime;		/*维修时间*/
	
		public string szRepareInfo;		/*维修说明*/
	
		public uint? dwRepareCost;		/*维修费用*/
	
		public string szFundsNo1;		/*经费卡编号1*/
	
		public uint? dwPay1;		/*经费卡1支付*/
	
		public string szFundsNo2;		/*经费卡编号2*/
	
		public uint? dwPay2;		/*经费卡2支付*/
	
		public string szRepareCom;		/*维修单位*/
	
		public string szRepareComTel;		/*维修单位联系方式*/
	
		public string szMemo;		/*说明*/
		};

	/**/
	
	public struct REPAIRCANCEL
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*SID*/
	
		public uint? dwDevID;		/*设备ID*/
	
		public string szDevName;		/*设备名称*/
	
		public string szAssertSN;		/*用户方资产编号*/
	
		public uint? dwDevStat;		/*撤销后设备状态*/
	
		public string szCancelInfo;		/*撤销说明*/
	
		public string szMemo;		/*说明*/
		};

	/**/
	
	public struct COMPANYREQ
	{
		private Reserved reserved;
		
		public uint? dwComID;		/*单位ID*/
	
		public uint? dwComKind;		/*单位类型*/
	
		public uint? dwProperty;		/*属性*/
	
		public string szComName;		/*单位名*/
	
		public string szSearchKey;		/*搜索关键字*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/**/
	
	public struct UNICOMPANY
	{
		private Reserved reserved;
		
		public uint? dwComID;		/*单位ID*/
	
		public string szComName;		/*单位名*/
	
		public uint? dwComKind;		/*单位类型*/
	
		[FlagsAttribute]
		public enum DWCOMKIND : uint
		{
			
				[EnumDescription("设备制造商")]
				COM_PRODUCER = 0x1,
			
				[EnumDescription("销售商")]
				COM_SELLER = 0x2,
			
				[EnumDescription("维保单位")]
				COM_SERVICE = 0x4,
			
		}

	
		public uint? dwProperty;		/*属性*/
	
		public string szTrueName;		/*联系人姓名*/
	
		public string szJobTitle;		/*职务*/
	
		public string szTel;		/*电话*/
	
		public string szHandPhone;		/*手机*/
	
		public string szEmail;		/*电邮*/
	
		public string szQQ;		/*QQ*/
	
		public string szAddress;		/*地址*/
	
		public string szOtherContact;		/*其它联系方式*/
	
		public string szKeyWords;		/*关键字*/
	
		public string szMemo;		/*备注*/
		};

	/*获取设备历史档案明细表*/
	
	public struct ASSERTLOGREQ
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*设备报废记录ID*/
	
		public uint? dwDevID;		/*设备ID*/
	
		public uint? dwOpKind;		/*日志类型*/
	
		public uint? dwOperatorID;		/*操作员ID*/
	
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*设备历史档案*/
	
	public struct ASSERTLOG
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*日志ID*/
	
		public uint? dwOpKind;		/*日志类型*/
	
		[FlagsAttribute]
		public enum DWOPKIND : uint
		{
			
				[EnumDescription("资产登记")]
				ASSERTLOG_WAREHOUSING = 10,
			
				[EnumDescription("资产位置变更")]
				ASSERTLOG_CHGROOM = 20,
			
				[EnumDescription("资产责任人变更")]
				ASSERTLOG_CHGKEEPER = 30,
			
				[EnumDescription("资产盘点")]
				ASSERTLOG_STOCKTAKING = 40,
			
				[EnumDescription("资产维修")]
				ASSERTLOG_REPARING = 50,
			
				[EnumDescription("资产报废")]
				ASSERTLOG_OOS = 100,
			
		}

	
		public uint? dwOpDate;		/*日期*/
	
		public uint? dwOpTime;		/*时间*/
	
		public string szOpDetail;		/*详细信息*/
	
		public uint? dwOperatorID;		/*操作员ID*/
	
		public string szOperatorName;		/*操作员姓名*/
	
		public uint? dwDevID;		/*设备ID*/
	
		public string szAssertSN;		/*用户方资产编号*/
	
		public string szDevName;		/*实验设备名称*/
	
		public uint? dwClassID;		/*设备功能类别*/
	
		public string szClassName;		/*设备类别名称*/
	
		public uint? dwClassKind;		/*类别(见UNIDEVCLS的Kind定义)*/
	
		public uint? dwKindID;		/*设备类型*/
	
		public string szKindName;		/*设备名称*/
	
		public string szFuncCode;		/*设备功能用途编码*/
	
		public string szModel;		/*设备型号*/
	
		public string szSpecification;		/*设备规格*/
		};

	/*结束数据结构*/

	/*开始数据结构*/

	/*考勤房间*/
	
	public struct ATTENDROOM
	{
		private Reserved reserved;
		
		public uint? dwRoomID;		/*房间ID*/
	
		public string szRoomName;		/*房间名称*/
	
		public string szRoomNo;		/*房间号*/
	
		public string szFloorNo;		/*所在楼层*/
	
		public uint? dwBuildingID;		/*楼宇ID*/
	
		public string szBuildingNo;		/*大楼编号(*/
	
		public string szBuildingName;		/*大楼名称(*/
	
		public string szMemo;		/*备注*/
		};

	/*获取考勤规则的请求包*/
	
	public struct ATTENDRULEREQ
	{
		private Reserved reserved;
		
		public uint? dwAttendID;		/*考勤规则ID*/
	
		public string szAttendName;		/*考勤规则名称*/
	
		public uint? dwKind;		/*考勤类型*/
	
		public string szRoomIDs;		/*房间ID,多个用逗号隔开*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*考勤规则*/
	
	public struct ATTENDRULE
	{
		private Reserved reserved;
		
		public uint? dwAttendID;		/*考勤规则ID*/
	
		public string szAttendName;		/*考勤规则名称*/
	
		public uint? dwKind;		/*考勤类型*/
	
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
		public uint? dwGroupID;		/*组ID*/
	
		public uint? dwOpenRuleSN;		/*开放规则编号*/
	
		public uint? dwEarlyInTime;		/*最早进入时间(HHMM)*/
	
		public uint? dwLateInTime;		/*最晚进入时间(HHMM)*/
	
		public uint? dwEarlyOutTime;		/*最早离开时间(HHMM),小于进入时间表明跨天*/
	
		public uint? dwLateOutTime;		/*最晚离开时间(HHMM),小于进入时间表明跨天*/
	
		public uint? dwMinStayTime;		/*最少停留时间*/
	
	public ATTENDROOM[] AttendRoom;		/*ATTENDROOM表*/
	
		public string szMemo;		/*备注*/
		};

	/*获取考勤记录的请求包*/
	
	public struct ATTENDRECREQ
	{
		private Reserved reserved;
		
		public uint? dwAttendID;		/*考勤规则ID*/
	
		public string szAttendName;		/*考勤规则名称*/
	
		public uint? dwKind;		/*考勤类型*/
	
		public string szRoomIDs;		/*房间ID,多个用逗号隔开*/
	
		public uint? dwAccNo;		/*账号*/
	
		public uint? dwAttendStat;		/*考勤状态*/
	
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*考勤记录*/
	
	public struct ATTENDREC
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*流水号*/
	
		public uint? dwAttendID;		/*考勤规则ID*/
	
		public string szAttendName;		/*考勤规则名称*/
	
		public uint? dwAccNo;		/*账号*/
	
		public string szPID;		/*学工号*/
	
		public string szTrueName;		/*姓名*/
	
		public uint? dwSex;		/*性别*/
	
		public uint? dwAttendDate;		/*考勤日期*/
	
		public uint? dwAttendStat;		/*考勤状态(定义在UNIRESVREC)*/
	
		public uint? dwRoomID;		/*房间ID*/
	
		public string szRoomName;		/*房间名称*/
	
		public uint? dwInTime;		/*进入时间(HHMM)*/
	
		public uint? dwOutTime;		/*离开时间(HHMM)*/
	
		public uint? dwLatestInTime;		/*最近进入时间*/
	
		public uint? dwStayMin;		/*停留时间(分钟)*/
	
		public uint? dwCardTimes;		/*刷卡次数*/
	
		public uint? dwRFLID;		/*request for leave ID*/
	
		public string szMemo;		/*备注*/
		};

	/*考勤信息*/
	
	public struct ATTENDINFO
	{
		private Reserved reserved;
		
		public uint? dwAttendMode;		/*考勤进出模式*/
	
		[FlagsAttribute]
		public enum DWATTENDMODE : uint
		{
			
				[EnumDescription("签到进入")]
				ATTENDMODE_IN = 1,
			
				[EnumDescription("退出")]
				ATTENDMODE_OUT = 2,
			
				[EnumDescription("再次进入")]
				ATTENDMODE_REIN = 4,
			
				[EnumDescription("刷卡端或门禁不能确定是进入退出")]
				ATTENDMODE_UNIKNOWN = 8,
			
		}

	
		public uint? dwAccNo;		/*账号*/
	
		public uint? dwRoomID;		/*房间ID*/
	
		public uint? dwAttendDate;		/*考勤日期*/
	
		public uint? dwSID;		/*流水号*/
	
		public uint? dwAttendID;		/*考勤规则ID*/
	
		public string szAttendName;		/*考勤规则名称*/
	
		public uint? dwAttendStat;		/*考勤状态(定义在UNIRESVREC)*/
	
		public uint? dwInTime;		/*进入时间(HHMM)*/
	
		public uint? dwOutTime;		/*离开时间(HHMM)*/
	
		public uint? dwLatestInTime;		/*最近进入时间*/
	
		public uint? dwStayMin;		/*停留时间(分钟)*/
	
		public uint? dwCardTimes;		/*刷卡次数*/
	
		public uint? dwRFLID;		/*request for leave ID*/
	
		public string szMemo;		/*备注*/
		};

	/*获取考勤统计的请求包*/
	
	public struct ATTENDSTATREQ
	{
		private Reserved reserved;
		
		public uint? dwAttendID;		/*考勤规则ID*/
	
		public uint? dwAccNo;		/*账号*/
	
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*考勤统计*/
	
	public struct ATTENDSTAT
	{
		private Reserved reserved;
		
		public uint? dwAccNo;		/*账号*/
	
		public string szPID;		/*学工号*/
	
		public string szTrueName;		/*姓名*/
	
		public uint? dwSex;		/*性别*/
	
		public uint? dwTotalTimes;		/*总次数*/
	
		public uint? dwAttendTimes;		/*出勤次数*/
	
		public uint? dwAbsentTimes;		/*缺勤次数*/
	
		public uint? dwLateTimes;		/*迟到次数*/
	
		public uint? dwLeaveTimes;		/*早退次数*/
	
		public uint? dwLLTimes;		/*迟到且早退次数*/
	
		public uint? dwSickTimes;		/*病假次数*/
	
		public uint? dwPrivateTimes;		/*事假次数*/
	
		public uint? dwUseLessTimes;		/*使用时间不达标次数*/
	
		public uint? dwLeaveNoCardTimes;		/*未刷卡离开次数*/
	
		public uint? dwTotalMin;		/*出勤总时间（分钟）*/
		};

	/*结束数据结构*/

	/*开始数据结构*/

	/*子系统信息*/
	
	public struct SUBSYS
	{
		private Reserved reserved;
		
		public uint? dwStaSN;		/*子系统编号*/
	
		public string szSubSysName;		/*子系统名称*/
	
		public uint? dwStatus;		/*状态*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("离线")]
				SUBSYSSTAT_OFFLINE = 0x0,
			
				[EnumDescription("在线")]
				SUBSYSSTAT_ONLINE = 0x1,
			
		}

	
		public string szVersion;		/*子系统服务器版本*/
	
		public string szIP;		/*IP地址*/
	
		public string szMemo;		/*说明信息*/
		};

	/*子系统登录请求*/
	
	public struct SUBSYSLOGINREQ
	{
		private Reserved reserved;
		
		public uint? dwStaSN;		/*子系统编号*/
	
		public string szVersion;		/*子系统服务器版本*/
	
		public string szKey;		/*密钥(扩展)*/
	
		public string szIP;		/*IP地址*/
	
		public string szMAC;		/*网卡地址*/
	
		public uint? dwOldSessionID;		/*上次分配的session值*/
	
		public string szMemo;		/*说明信息*/
		};

	/*子系统登录应答*/
	
	public struct SUBSYSLOGINRES
	{
		private Reserved reserved;
		
		public uint? dwSessionID;		/*服务器分配的SessionID*/
	
		public string ExtInfo;		/*返回扩展信息*/
	
		public string szMemo;		/*说明信息*/
		};

	/*子系统退出请求*/
	
	public struct SUBSYSLOGOUTREQ
	{
		private Reserved reserved;
		
		public uint? dwSessionID;		/*服务器分配的SessionID*/
	
		public uint? dwStaSN;		/*子系统编号*/
		};

	/*IC空间使用记录上传*/
	
	public struct ICUSERECUP
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*流水号*/
	
		public uint? dwStaSN;		/*子系统编号*/
	
		public uint? dwSubStaSN;		/*子站点编号*/
	
		public string szLogonName;		/*学工号*/
	
		public string szTrueName;		/*姓名*/
	
		public uint? dwUseDate;		/*使用日期*/
	
		public uint? dwResvBTime;		/*预约开始时间*/
	
		public uint? dwResvETime;		/*预约结束时间*/
	
		public uint? dwRealInTime;		/*实际开始时间*/
	
		public uint? dwRealOutTime;		/*实际结束时间*/
	
		public uint? dwUseMinutes;		/*使用时长(分钟)*/
	
		public string szUseDev;		/*使用设备*/
	
		public uint? dwDevClsKind;		/*类别类型(关联UNIDEVCLS:dwKind定义）*/
	
		public uint? dwDevKind;		/*设备类型*/
	
		public uint? dwUseMode;		/*使用方法（参考UNIRESERVE定义）*/
	
		public uint? dwPurpose;		/*用途（参考UNIRESERVE定义）*/
	
		public uint? dwRealCost;		/*实际缴纳费用(分)*/
	
		public string szMemo;		/*说明信息*/
		};

	/*打印复印扫描记录上传*/
	
	public struct PRINTRECUP
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*流水号*/
	
		public uint? dwStaSN;		/*子系统编号*/
	
		public uint? dwSubStaSN;		/*子站点编号*/
	
		public string szLogonName;		/*学工号*/
	
		public string szTrueName;		/*姓名*/
	
		public uint? dwUseDate;		/*使用日期*/
	
		public uint? dwUseTime;		/*使用时间*/
	
		public string szUseDev;		/*使用设备*/
	
		public uint? dwPages;		/*文印面数(或扫描大小）*/
	
		public uint? dwPaperType;		/*纸型*/
	
		public uint? dwPrintType;		/*文印类型*/
	
		public uint? dwProperty;		/*属性*/
	
		public uint? dwRealCost;		/*实际缴纳费用(分)*/
	
		public uint? dwUnitFee;		/*单价*/
	
		public uint? dwPaperNum;		/*纸张数*/
	
		public uint? dwMaterialFee;		/*材料费*/
	
		public uint? dwManualFee;		/*人工费*/
	
		public string szMemo;		/*说明信息*/
		};

	/*图书超期缴费记录上传*/
	
	public struct BOOKOVERDUERECUP
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*流水号*/
	
		public uint? dwStaSN;		/*子系统编号*/
	
		public uint? dwSubStaSN;		/*子站点编号*/
	
		public string szLogonName;		/*学工号*/
	
		public string szTrueName;		/*姓名*/
	
		public uint? dwUseDate;		/*使用日期*/
	
		public uint? dwUseTime;		/*使用时间*/
	
		public string szUseDev;		/*使用设备*/
	
		public string szBookName;		/*书名*/
	
		public uint? dwRealCost;		/*实际缴纳费用(分)*/
	
		public string szMemo;		/*说明信息*/
		};

	/*违约记录上传*/
	
	public struct BREACHRECUP
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*流水号*/
	
		public uint? dwStaSN;		/*子系统编号*/
	
		public uint? dwSubStaSN;		/*子站点编号*/
	
		public string szLogonName;		/*学工号*/
	
		public string szTrueName;		/*姓名*/
	
		public uint? dwOccurDate;		/*违约日期*/
	
		public uint? dwOccurTime;		/*违约时间*/
	
		public string szBreachName;		/*违约类型名*/
	
		public uint? dwPunishScore;		/*本次罚分*/
	
		public uint? dwTotalScore;		/*累计罚分*/
	
		public uint? dwThresholdScore;		/*达到处罚标准的分数*/
	
		public uint? dwStatus;		/*处罚状态*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("有效")]
				BREACHSTAT_VALID = 1,
			
				[EnumDescription("管理员取消")]
				BREACHSTAT_CANCEL = 2,
			
				[EnumDescription("已过积分周期期")]
				BREACHSTAT_OVER = 4,
			
		}

	
		public string szPunishName;		/*处罚方式*/
	
		public uint? dwPStartDate;		/*处罚开始时间*/
	
		public uint? dwPEndDate;		/*处罚结束时间*/
	
		public string szMemo;		/*说明信息*/
		};

	/*结束数据结构*/

	/*开始数据结构*/

	/*获取研修间当前状态请求*/
	
	public struct STUDYROOMSTATREQ
	{
		private Reserved reserved;
		
		public uint? dwRoomKinds;		/*研修间类型组合*/
	
		public string szBuildingNo;		/*大楼编号*/
		};

	/*研修间当前状态*/
	
	public struct STUDYROOMSTAT
	{
		private Reserved reserved;
		
		public uint? dwRoomKind;		/*研修间类型*/
	
		[FlagsAttribute]
		public enum DWROOMKIND : uint
		{
			
				[EnumDescription("单人研修间")]
				SROOMKIND_SINGLE = 0x101,
			
				[EnumDescription("团体间(多人研讨室)")]
				SROOMKIND_GROUP = 0x201,
			
				[EnumDescription("开放活动室")]
				SROOMKIND_ACTIVITY = 0x401,
			
				[EnumDescription("多人研修间")]
				SROOMKIND_MULTIPLE = 0x801,
			
		}

	
		public uint? dwTotalNum;		/*总数*/
	
		public uint? dwIdleNum;		/*空闲数*/
		};

	/*获取座位当前状态请求*/
	
	public struct SEATSTATREQ
	{
		private Reserved reserved;
		
		public string szBuildingNo;		/*大楼编号*/
	
		public string szFloorNo;		/*所在楼层*/
		};

	/*座位当前状态*/
	
	public struct SEATSTAT
	{
		private Reserved reserved;
		
		public string szRoomNo;		/*房间号*/
	
		public string szRoomName;		/*房间名称*/
	
		public uint? dwTotalNum;		/*总数*/
	
		public uint? dwIdleNum;		/*空闲数*/
		};

	/*结束数据结构*/

	/*开始数据结构*/

	/*获取科研实验数据请求*/
	
	public struct RTDATAREQ
	{
		private Reserved reserved;
		
		public uint? dwBeginDate;		/*预约开始日期*/
	
		public uint? dwEndDate;		/*预约结束日期*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*获取科研实验数据*/
	
	public struct RTDATA
	{
		private Reserved reserved;
		
		public uint? dwResvID;		/*预约号*/
	
		public uint? dwProperty;		/*预约属性*/
	
		public uint? dwDevID;		/*设备ID*/
	
		public string szAssertSN;		/*资产编号*/
	
		public string szTestName;		/*科研实验名称*/
	
		public uint? dwBeginTime;		/*预约开始时间*/
	
		public uint? dwEndTime;		/*预约结束时间*/
	
		public uint? dwOwner;		/*使用人(创建者)*/
	
		public string szPID;		/*使用人学工号*/
	
		public string szOwnerName;		/*使用人姓名*/
	
		public uint? dwIdent;		/*使用人身份（校内（本、研，教师和校外）*/
	
		public uint? dwUserDeptID;		/*使用人部门ID*/
	
		public string szUserDeptName;		/*使用人部门*/
	
		public string szTel;		/*电话*/
	
		public string szHandPhone;		/*手机*/
	
		public string szEmail;		/*电邮*/
	
		public uint? dwRTID;		/*科研实验项目ID*/
	
		public uint? dwRTKind;		/*科研类型*/
	
		public string szRTName;		/*科研实验名称*/
	
		public uint? dwSampleNum;		/*样品数*/
	
		public uint? dwManID;		/*管理员ID*/
	
		public string szManName;		/*管理员姓名*/
	
		public uint? dwReceivableCost;		/*应缴费用*/
	
		public uint? dwRealCost;		/*实际缴纳费用*/
	
		public string szDescription;		/*实验描述*/
		};

	/*结束数据结构*/

	/*开始数据结构*/

	/**/
	
	public struct STALOGINREQ
	{
		private Reserved reserved;
		
		public string szLicSN;		/*许可系列号*/
	
		public string szVersion;		/*版本*/
	
		public string szIP;		/*IP地址*/
	
		public string szMAC;		/*网卡地址*/
	
		public string szKey;		/*密钥(扩展)*/
	
		public uint? dwOldSessionID;		/*上次分配的session值*/
		};

	/**/
	
	public struct STALOGINRES
	{
		private Reserved reserved;
		
		public uint? dwStaID;		/*节点ID*/
	
		public uint? dwSessionID;		/**/
	
	public UNILICENSE LicInfo;		/*服务器授权信息UNILICENSE*/
	
		public string szMemo;		/*备注*/
		};

	/**/
	
	public struct STALOGOUTREQ
	{
		private Reserved reserved;
		
		public uint? dwSessionID;		/**/
	
		public string szLicSN;		/*许可系列号*/
		};

	/**/
	
	public struct HANDSHAKEREQ
	{
		private Reserved reserved;
		
		public uint? dwChgFlag;		/*本地保存的修改更新标志*/
		};

	/**/
	
	public struct HANDSHAKERES
	{
		private Reserved reserved;
		
		public uint? dwChgFlag;		/*服务器返回的的修改更新标志*/
	
		public string szResChgStat;		/*返回的对应信息有无标志，字符0表示无，字符1表示有*/
	
		[FlagsAttribute]
		public enum SZRESCHGSTAT : uint
		{
			
				[EnumDescription("最新许可信息(CUniStruct[UNILICENSE])")]
				CHGINDEX_LICENSE = 1,
			
				[EnumDescription("最新版本信息(CUniStruct[UNIPRODUCT])")]
				CHGINDEX_NEWVER = 2,
			
				[EnumDescription("需更新的最大类别")]
				MAXCHG_TYPE = 10,
			
		}

		};

	/*模块监控信息上传*/
	
	public struct MODMONIUP
	{
		private Reserved reserved;
		
		public uint? dwModSN;		/*监控端编号（服务器为0，以下定义+StaSN*65536 + 监控端编号)*/
	
		public string szModName;		/*监控端名称*/
	
		public uint? dwStatus;		/*总状态*/
	
		public uint? dwStartTime;		/*新状态开始时间*/
	
		public string szStatInfo;		/*状态说明*/
	
		public string szMemo;		/*备注*/
		};

	/*监控指标上传*/
	
	public struct MONINDEXUP
	{
		private Reserved reserved;
		
		public uint? dwModSN;		/*监控端编号（服务器为0，其余为定义的编号或ID号)*/
	
		public uint? dwMoniSN;		/*监控指标编号*/
	
		public string szIndexName;		/*监控指标名称*/
	
		public uint? dwNormalValue;		/*正常值*/
	
		public uint? dwCurValue;		/*当前值*/
	
		public uint? dwStatus;		/*状态*/
	
		public uint? dwNormalTime;		/*正常时间(分钟)*/
	
		public uint? dwAbnormalTime;		/*异常时间(分钟)*/
	
		public uint? dwAbnormalTimes;		/*异常次数*/
	
		public uint? dwStartTime;		/*新状态开始时间*/
	
		public string szStatInfo;		/*状态说明*/
	
		public string szMemo;		/*备注*/
		};

	/*监控记录上传*/
	
	public struct MONIRECUP
	{
		private Reserved reserved;
		
		public uint? dwSID;		/*分配流水号*/
	
		public uint? dwModSN;		/*监控端编号（服务器为0，其余为定义的编号或ID号)*/
	
		public uint? dwMoniSN;		/*监控指标编号*/
	
		public string szIndexName;		/*监控指标名称*/
	
		public uint? dwCurValue;		/*当前值*/
	
		public uint? dwOccurDate;		/*开始日期*/
	
		public uint? dwOccurTime;		/*产生时间*/
	
		public uint? dwStatus;		/*状态*/
	
		public uint? dwNormalTime;		/*正常时间(分钟)*/
	
		public uint? dwAbnormalTime;		/*异常时间(分钟)*/
	
		public uint? dwAbnormalTimes;		/*异常次数*/
	
		public string szStatInfo;		/*状态说明*/
		};

	/*结束数据结构*/

	/*开始数据结构*/

	/*获取监控信息缺省值*/
	
	public struct MONIREQ
	{
		private Reserved reserved;
		
		public uint? dwModKind;		/*子模块类别（MODKIND_XXX定义)*/
	
		public uint? dwStaSN;		/*站点编号*/
	
		public uint? dwModSN;		/*监控端编号*/
	
		public uint? dwStatus;		/*状态*/
	
		public uint? dwReqProp;		/*请求附加属性*/
	
		[FlagsAttribute]
		public enum DWREQPROP : uint
		{
			
				[EnumDescription("需要指标列表")]
				MONIREQ_NEEDINDEXTBL = 0x1,
			
		}

	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*监控指标编号*/
	
	public struct MONINDEX
	{
		private Reserved reserved;
		
		public uint? dwModSN;		/*监控端编号（服务器为0，其余为定义的编号或ID号)*/
	
		public string szModName;		/*监控端名称*/
	
		public uint? dwMoniSN;		/*监控指标编号*/
	
		[FlagsAttribute]
		public enum DWMONISN : uint
		{
			
				[EnumDescription("网络")]
				MONIINDEX_NET = 0x100,
			
				[EnumDescription("网卡状态")]
				NET_ADAPTERSTAT = (MONIINDEX_NET + 1),
			
				[EnumDescription("IP地址")]
				NET_IP = (MONIINDEX_NET + 2),
			
				[EnumDescription("子网掩码")]
				NET_MASK = (MONIINDEX_NET + 3),
			
				[EnumDescription("网关")]
				NET_GATE = (MONIINDEX_NET + 4),
			
				[EnumDescription("流入流量")]
				NET_INFLOW = (MONIINDEX_NET + 5),
			
				[EnumDescription("流出流量")]
				NET_OUTFLOW = (MONIINDEX_NET + 6),
			
				[EnumDescription("连接数")]
				NET_CONNS = (MONIINDEX_NET + 10),
			
				[EnumDescription("监控进程连接数")]
				NET_MYCONNS = (MONIINDEX_NET + 11),
			
				[EnumDescription("其它连接数(状态为ESTABLISHED以外的连接)")]
				NET_OTHERCONNS = (MONIINDEX_NET + 12),
			
				[EnumDescription("DNS")]
				NET_DNS = (MONIINDEX_NET + 13),
			
				[EnumDescription("CPU")]
				MONIINDEX_CPU = 0x200,
			
				[EnumDescription("CPU总使用率")]
				CPU_SYSUSAGE = (MONIINDEX_CPU + 1),
			
				[EnumDescription("我的使用率")]
				CPU_MYUSAGE = (MONIINDEX_CPU + 2),
			
				[EnumDescription("硬盘")]
				MONIINDEX_HD = 0x400,
			
				[EnumDescription("可用磁盘空间(M)")]
				HD_FREESIZE = (MONIINDEX_HD + 1),
			
				[EnumDescription("内存")]
				MONIINDEX_MEM = 0x800,
			
				[EnumDescription("系统可用内存(M)")]
				MEM_SYSFREE = (MONIINDEX_MEM + 1),
			
				[EnumDescription("我的使用量(M)")]
				MEM_MYUSE = (MONIINDEX_MEM + 2),
			
				[EnumDescription("本进程")]
				MONIINDEX_MYSELF = 0x1000,
			
				[EnumDescription("历史状态")]
				MY_HISTORY = (MONIINDEX_MYSELF + 1),
			
				[EnumDescription("句柄数")]
				MY_HANDLE = (MONIINDEX_MYSELF + 2),
			
				[EnumDescription("GDI计数")]
				MY_GDI = (MONIINDEX_MYSELF + 3),
			
				[EnumDescription("数字签名")]
				MY_SIGNATURE = (MONIINDEX_MYSELF + 4),
			
				[EnumDescription("数据库状态")]
				DB_STAT = (MONIINDEX_MYSELF + 5),
			
				[EnumDescription("数据库操作")]
				DB_OP = (MONIINDEX_MYSELF + 6),
			
				[EnumDescription("认证请求")]
				REQ_AUTH = (MONIINDEX_MYSELF + 7),
			
				[EnumDescription("第三方状态")]
				THIRD_STAT = (MONIINDEX_MYSELF + 8),
			
				[EnumDescription("第三方操作")]
				THIRD_OP = (MONIINDEX_MYSELF + 9),
			
				[EnumDescription("服务中心状态")]
				SSC_STAT = (MONIINDEX_MYSELF + 10),
			
				[EnumDescription("服务中心操作")]
				SSC_OP = (MONIINDEX_MYSELF + 11),
			
				[EnumDescription("联创管理中心状态")]
				UNISRV_STAT = (MONIINDEX_MYSELF + 12),
			
				[EnumDescription("联创管理中心操作")]
				UNISRV_OP = (MONIINDEX_MYSELF + 13),
			
				[EnumDescription("许可状态")]
				LICENSE_STAT = (MONIINDEX_MYSELF + 14),
			
				[EnumDescription("服务状态")]
				SERVICE_STAT = (MONIINDEX_MYSELF + 15),
			
				[EnumDescription("子系统连接状态")]
				SUBCON_STAT = (MONIINDEX_MYSELF + 16),
			
				[EnumDescription("线程状态")]
				THREAD_STAT = (MONIINDEX_MYSELF + 17),
			
		}

	
		public string szIndexName;		/*监控指标名称*/
	
		public uint? dwNormalValue;		/*正常值*/
	
		public uint? dwCurValue;		/*当前值*/
	
		public uint? dwStatus;		/*状态*/
	
		[FlagsAttribute]
		public enum DWSTATUS : uint
		{
			
				[EnumDescription("正常")]
				MONISTAT_OK = 1,
			
				[EnumDescription("提醒")]
				MONISTAT_INFO = 2,
			
				[EnumDescription("警告")]
				MONISTAT_WARNNING = 4,
			
				[EnumDescription("错误")]
				MONISTAT_ERROR = 8,
			
		}

	
		public uint? dwNormalTime;		/*正常时间(分钟)*/
	
		public uint? dwAbnormalTime;		/*异常时间(分钟)*/
	
		public uint? dwAbnormalTimes;		/*异常次数*/
	
		public uint? dwStartTime;		/*新状态开始时间*/
	
		public string szStatInfo;		/*状态说明*/
	
		public string szMemo;		/*备注*/
		};

	/*模块监控信息*/
	
	public struct MODMONI
	{
		private Reserved reserved;
		
		public uint? dwModSN;		/*监控端编号（服务器为0，以下定义+StaSN*65536 + 监控端编号)*/
	
		[FlagsAttribute]
		public enum DWMODSN : uint
		{
			
				[EnumDescription("服务端")]
				MODKIND_SERVER = 0x1000000,
			
				[EnumDescription("门禁集控器")]
				MODKIND_DCS = 0x2000000,
			
				[EnumDescription("现场触控终端（刷卡端）")]
				MODKIND_STT = 0x4000000,
			
				[EnumDescription("客户端")]
				MODKIND_CLT = 0x8000000,
			
		}

	
		public string szModName;		/*监控端名称*/
	
		public uint? dwStatus;		/*总状态*/
	
		public uint? dwStartTime;		/*新状态开始时间*/
	
		public string szStatInfo;		/*状态说明*/
	
	public MONINDEX[] MoniIndexTbl;		/*指标列表*/
	
		public string szMemo;		/*备注*/
		};

	/*获取监控信息缺省值*/
	
	public struct MONIRECREQ
	{
		private Reserved reserved;
		
		public uint? dwStartDate;		/*开始日期*/
	
		public uint? dwEndDate;		/*结束日期*/
	
		public uint? dwModKind;		/*子模块类别（MODKIND_XXX定义)*/
	
		public uint? dwStaSN;		/*站点编号*/
	
		public uint? dwModSN;		/*模块编号*/
	
		public uint? dwMoniSN;		/*监控指标编号*/
	
		public uint? dwStatus;		/*状态*/
	
	public REQEXTINFO szReqExtInfo;		/*CUniStruct[REQEXTINFO]*/
		};

	/*监控记录*/
	
	public struct MONIREC
	{
		private Reserved reserved;
		
		public uint? dwModSN;		/*监控端编号（服务器为0，其余为定义的编号或ID号)*/
	
		public string szModName;		/*监控端名称*/
	
		public uint? dwMoniSN;		/*监控指标编号*/
	
		public string szIndexName;		/*监控指标名称*/
	
		public uint? dwNormalValue;		/*正常值*/
	
		public uint? dwCurValue;		/*当前值*/
	
		public uint? dwOccurDate;		/*开始日期*/
	
		public uint? dwOccurTime;		/*产生时间*/
	
		public uint? dwStatus;		/*状态*/
	
		public uint? dwNormalTime;		/*正常时间(分钟)*/
	
		public uint? dwAbnormalTime;		/*异常时间(分钟)*/
	
		public uint? dwAbnormalTimes;		/*异常次数*/
	
		public string szStatInfo;		/*状态说明*/
		};

	/*错误处理*/
	
	public struct MONIDEALERR
	{
		private Reserved reserved;
		
		public uint? dwModSN;		/*监控端编号（服务器为0，其余为定义的编号或ID号)*/
	
		public uint? dwMoniSN;		/*监控指标编号*/
	
		public uint? dwNormalValue;		/*正常值*/
	
		public uint? dwCurValue;		/*当前值*/
	
		public string szDealInfo;		/*说明信息*/
		};

	/*结束数据结构*/

	/*开始数据结构*/

	/*联创服务与节点通信参数*/
	
	public struct UNISTAPARAM
	{
		private Reserved reserved;
		
		public uint? dwReqUID;		/*请求UID*/
	
		public string szReqData;		/*请求内容*/
	
		public uint? dwResCode;		/*返回码，0表示成功*/
	
		public string szResData;		/*返回数据*/
		};

	/*许可功能模块信息*/
	
	public struct LICMOD
	{
		private Reserved reserved;
		
		public uint? dwFuncSN;		/*功能模块编号*/
	
		[FlagsAttribute]
		public enum DWFUNCSN : uint
		{
			
				[EnumDescription("门禁管理")]
				LICMOD_DOORCTRL = 1,
			
				[EnumDescription("PC机")]
				LICMOD_PC = 2,
			
				[EnumDescription("视频监控")]
				LICMOD_VIDEOCTRL = 3,
			
				[EnumDescription("语音系统")]
				LICMOD_SOUNDCTRL = 4,
			
				[EnumDescription("电源控制")]
				LICMOD_POWERCTRL = 5,
			
				[EnumDescription("空间管理")]
				LICMOD_COMMONS = 6,
			
				[EnumDescription("苹果机")]
				LICMOD_MAC = 7,
			
				[EnumDescription("座位管理")]
				LICMOD_SEAT = 8,
			
				[EnumDescription("活动安排")]
				LICMOD_ACTIVITY = 9,
			
				[EnumDescription("开放实验")]
				LICMOD_OPENTEST = 10,
			
				[EnumDescription("设备外借")]
				LICMOD_LOAN = 11,
			
				[EnumDescription("教学管理")]
				LICMOD_TEACHING = 12,
			
				[EnumDescription("教师助手")]
				LICMOD_TELECAST = 13,
			
				[EnumDescription("实验报告")]
				LICMOD_REPORT = 14,
			
				[EnumDescription("资产管理")]
				LICMOD_ASSET = 15,
			
				[EnumDescription("实验数据管理")]
				LICMOD_TESTDATA = 16,
			
		}

	
		public uint? dwLicNum;		/*对应功能模块节点数*/
	
		public string szModName;		/*授权模块名称*/
		};

	/*许可信息*/
	
	public struct UNILICENSE
	{
		private Reserved reserved;
		
		public string szLicSN;		/*许可编号*/
	
		public uint? dwInstDate;		/*安装日期*/
	
		public uint? dwLicExpDate;		/*许可到期日*/
	
		public uint? dwServiceExpDate;		/*服务到期日*/
	
		public string szLicTo;		/*授权客户名称*/
	
		public string szLicProName;		/*授权产品名称*/
	
		public string szCompanyName;		/*公司名称*/
	
		public uint? dwWarrant;		/*与一卡通对接模式*/
	
		[FlagsAttribute]
		public enum DWWARRANT : uint
		{
			
				[EnumDescription("不对接")]
				WARRANT_NO_THIRD = 0x1,
			
				[EnumDescription("同步帐户")]
				WARRANT_SYNC_ACC = 0x2,
			
				[EnumDescription("同步余额")]
				WARRANT_SYNC_BALANCE = 0x4,
			
				[EnumDescription("同步密码")]
				WARRANT_SYNC_PASSWD = 0x8,
			
				[EnumDescription("卡上有钱包")]
				WARRANT_CARD_MONEY = 0x10,
			
				[EnumDescription("有账务中心")]
				WARRANT_WITH_CAC = 0x20,
			
				[EnumDescription("只同步卡号，不同步班级")]
				WARRANT_SYNC_CARDONLY = 0x40,
			
				[EnumDescription("卡直接扣费模式")]
				WARRANT_CARD_REAL = 0x80,
			
		}

	
		public uint? dwLicStaNum;		/*许可站点数*/
	
	public LICMOD[] LicMod;		/*LICMOD结构表*/
	
		public string szCtrlCode;		/*控制码*/
		};

	/*获取请求扩张信息*/
	
	public struct REQEXTINFO
	{
		private Reserved reserved;
		
		public uint? dwStartLine;		/*开始行*/
	
		public uint? dwNeedLines;		/*需获取行数*/
	
		public uint? dwTotolLines;		/*服务端返回总行数*/
	
		public string szOrderKey;		/*排序字段*/
	
		public string szOrderMode;		/*排序方式(ASC或DESC)*/
	
	public byte[] ExtInfo;		/*根据不同的请求相关扩展信息*/
		};

	/*结束数据结构*/

}
//