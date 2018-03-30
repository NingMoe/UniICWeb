
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
	/// Gets the <see cref=”DescriptionAttribute” /> of an <see cref=”Enum” /> type value.
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
	/// Converts the <see cref=”Enum” /> type to an <see cref=”IList” /> compatible object. 
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

	//模块列表
	public delegate void ErrorHandler(PRModule module,REQUESTCODE ret);
	public partial class PRModule
	{
		public ErrorHandler OnError;

		/*管理员管理*/
		public const uint ADMIN_BASE	= (0x010000);

		/*站点管理*/
		public const uint STATION_BASE	= (0x020000);

		/*帐户管理*/
		public const uint ACCOUNT_BASE	= (0x030000);

		/*设备管理*/
		public const uint DEVICE_BASE	= (0x040000);

		/*门禁设置管理*/
		public const uint DOORCTRLSRV_BASE	= (0x050000);

		/*用户组管理*/
		public const uint GROUP_BASE	= (0x060000);

		/*预约管理*/
		public const uint RESERVE_BASE	= (0x070000);

		/*上网游戏控制*/
		public const uint CONTROL_BASE	= (0x080000);

		/*第三方接口管理*/
		public const uint THIRDIF_BASE	= (0x090000);

		/*收费管理*/
		public const uint FEE_BASE	= (0x0A0000);

		/*控制台管理*/
		public const uint CONSOLE_BASE	= (0x0B0000);

		/*报表查询统计*/
		public const uint REPORT_BASE	= (0x0C0000);

		/*系统管理*/
		public const uint SYSTEM_BASE	= (0x0D0000);

		/*资产管理*/
		public const uint ASSERT_BASE	= (0x0E0000);

		/*考勤管理*/
		public const uint ATTENDANCE_BASE	= (0x0F0000);

		/*子系统通信接口*/
		public const uint SUBSYS_BASE	= (0x900000);

		/*IC空间子系统接口*/
		public const uint SUBIC_BASE	= (0x910000);

		/*科研实验子系统接口*/
		public const uint SUBRT_BASE	= (0x920000);

		/*节点管理*/
		public const uint UNISTA_BASE	= (0x1000000);

		/*自动监控*/
		public const uint UNIMONI_BASE	= (0x2000000);

	}
	/*开始接口--Admin--管理员管理*/
/*开始命令*/
	public partial class PRAdmin
	{

		/*获取管理员列表,	参数:ADMINREQ,	结果:UNIADMIN(Array)*/
		public const uint MSREQ_ADMIN_GET 	= 0x10;

		/*获取当前管理员界面参数信息,	参数:IFPARAMREQ,	结果:IFPARAM(Array)*/
		public const uint MSREQ_ADMIN_IFGET 	= 0x11;

		/*获取管理员操作日志,	参数:ADMINLOGREQ,	结果:ADMINLOG(Array)*/
		public const uint MSREQ_ADMINLOG_GET 	= 0x12;

		/*获取管理房间,	参数:MANROOMREQ,	结果:MANROOM(Array)*/
		public const uint MSREQ_ADMIN_MANROOMGET 	= 0x13;

		/*新建或修改管理员,	参数:UNIADMIN,	结果:UNIADMIN*/
		public const uint MSREQ_ADMIN_SET 	= 0x20;

		/*保存当前管理员界面参数信息,	参数:IFPARAM,	结果:null*/
		public const uint MSREQ_ADMIN_IFSAVE 	= 0x21;

		/*清除IP地址黑名单,	参数:IPBLACKLIST,	结果:null*/
		public const uint MSREQ_IPBLACKLIST_CLEAR 	= 0x22;

		/*修改管理员密码,	参数:ADMINCHGPASSWD,	结果:null*/
		public const uint MSREQ_ADMIN_CHGPW 	= 0x23;

		/*设置工作人员信息,	参数:STAFFINFO,	结果:null*/
		public const uint MSREQ_STAFF_SET 	= 0x24;

		/*修改管理员属性,	参数:UNIADMIN,	结果:null*/
		public const uint MSREQ_ADMIN_DEL 	= 0x30;

		/*获取审查信息,	参数:CHECKREQ,	结果:CHECKINFO(Array)*/
		public const uint MSREQ_ADMIN_CHECKGET 	= 0x50;

		/*管理员审查,	参数:ADMINCHECK,	结果:null*/
		public const uint MSREQ_ADMIN_CHECK 	= 0x51;

		/*获取审查信息日志,	参数:CHECKLOGREQ,	结果:CHECKLOG(Array)*/
		public const uint MSREQ_ADMIN_CHECKLOGGET 	= 0x52;

		/*管理员登录,	参数:ADMINLOGINREQ,	结果:ADMINLOGINRES*/
		public const uint MSREQ_ADMIN_LOGIN 	= 0x110;

		/*管理员登录获取管理站点,	参数:ADMINLOGINREQ,	结果:ADMINLOGINRES*/
		public const uint MSREQ_ADMIN_STALOGIN 	= 0x111;

		/*手机登录,	参数:MOBILELOGINREQ,	结果:ADMINLOGINRES*/
		public const uint MSREQ_MOBILE_STALOGIN 	= 0x112;

		/*管理员退出,	参数:ADMINLOGOUTREQ,	结果:ADMINLOGOUTRES*/
		public const uint MSREQ_ADMIN_LOGOUT 	= 0x120;

		/*获取系统支持的UID,	参数:UIDINFOREQ,	结果:UIDINFO(Array)*/
		public const uint MSREQ_UIDINFO_GET 	= 0x301;

		/*获取操作权限,	参数:OPPRIVREQ,	结果:OPPRIV(Array)*/
		public const uint MSREQ_OPPRIV_GET 	= 0x310;

		/*设置修改操作权限,	参数:OPPRIV,	结果:OPPRIV*/
		public const uint MSREQ_OPPRIV_SET 	= 0x320;

		/*删除操作权限,	参数:OPPRIV,	结果:null*/
		public const uint MSREQ_OPPRIV_DEL 	= 0x330;

		/*获取用户角色,	参数:USERROLEREQ,	结果:USERROLE(Array)*/
		public const uint MSREQ_USERROLE_GET 	= 0x340;

		/*设置用户角色,	参数:USERROLE,	结果:USERROLE*/
		public const uint MSREQ_USERROLE_SET 	= 0x343;

		/*删除用户角色,	参数:USERROLE,	结果:null*/
		public const uint MSREQ_USERROLE_DEL 	= 0x346;

		/*获取客户端密码,	参数:,	结果:CLTPASSWD(Array)*/
		public const uint MSREQ_ADMIN_CLTPWGET 	= 0x410;

		/*重新设置客户端密码,	参数:CLTPASSWD,	结果:CLTPASSWD*/
		public const uint MSREQ_ADMIN_CLTPWSET 	= 0x411;

		/*获取刷新标志,	参数:REFRESHFLAGREQ,	结果:REFRESHFLAGINFO(Array)*/
		public const uint MSREQ_ADMIN_REFRESHFLAGGET 	= 0x500;

		/*获取节假日,	参数:HOLIDAYREQ,	结果:UNIHOLIDAY(Array)*/
		public const uint MSREQ_HOLIDAY_GET 	= 0x510;

		/*设置节假日,	参数:UNIHOLIDAY,	结果:null*/
		public const uint MSREQ_HOLIDAY_SET 	= 0x511;

		/*删除节假日,	参数:UNIHOLIDAY,	结果:null*/
		public const uint MSREQ_HOLIDAY_DEL 	= 0x512;

		/*管理端界面调用检查某个值是否存在，不存在返回成功，存在返回错误,	参数:CHECKEXISTREQ,	结果:null*/
		public const uint MSREQ_CHECK_EXIST 	= 0x521;

		/*获取某个字段的最大值（仅支持数值型字段）,	参数:MAXVALUEREQ,	结果:MAXVALUE*/
		public const uint MSREQ_MAXVALUE_GET 	= 0x522;

		/*获取系统基本统计信息,	参数:,	结果:BASICSTAT*/
		public const uint MSREQ_BASICSTAT_GET 	= 0x530;

		/*获取审核类别请求,	参数:CHECKTYPEREQ,	结果:CHECKTYPE(Array)*/
		public const uint MSREQ_CHECKTYPE_GET 	= 0x550;

		/*修改审核类别,	参数:CHECKTYPE,	结果:null*/
		public const uint MSREQ_CHECKTYPE_SET 	= 0x555;

		/*获取意见反馈表,	参数:USERFEEDBACKREQ,	结果:USERFEEDBACK(Array)*/
		public const uint MSREQ_USERFEEDBACK_GET 	= 0x561;

		/*用户意见反馈,	参数:USERFEEDBACK,	结果:USERFEEDBACK*/
		public const uint MSREQ_USERFEEDBACK_DO 	= 0x565;

		/*回复用户意见反馈,	参数:USERFEEDBACK,	结果:null*/
		public const uint MSREQ_USERFEEDBACK_REPLY 	= 0x569;

		/*获取服务类别请求,	参数:SERVICETYPEREQ,	结果:UNISERVICETYPE(Array)*/
		public const uint MSREQ_SERVICETYPE_GET 	= 0x570;

		/*修改服务类别,	参数:UNISERVICETYPE,	结果:null*/
		public const uint MSREQ_SERVICETYPE_SET 	= 0x575;

		/*获取网上投票信息,	参数:POLLONLINEREQ,	结果:POLLONLINE(Array)*/
		public const uint MSREQ_POLLONLINE_GET 	= 0x601;

		/*新建修改网上投票,	参数:POLLONLINE,	结果:POLLONLINE*/
		public const uint MSREQ_POLLONLINE_SET 	= 0x605;

		/*进行网上投票,	参数:POLLVOTE,	结果:null*/
		public const uint MSREQ_POLLONLINE_VOTE 	= 0x610;


	}
/*结束命令*/
	/*结束接口--Admin*/
	/*开始接口--Station--站点管理*/
/*开始命令*/
	public partial class PRStation
	{

		/*获取站点,	参数:STATIONREQ,	结果:UNISTATION(Array)*/
		public const uint MSREQ_STATION_GET 	= 0x10;

		/*设置站点,	参数:UNISTATION,	结果:UNISTATION*/
		public const uint MSREQ_STATION_SET 	= 0x20;

		/*删除站点,	参数:UNISTATION,	结果:null*/
		public const uint MSREQ_STATION_DEL 	= 0x30;


	}
/*结束命令*/
	/*结束接口--Station*/
	/*开始接口--Account--帐户管理*/
/*开始命令*/
	public partial class PRAccount
	{

		/*获取部门列表,	参数:DEPTREQ,	结果:UNIDEPT(Array)*/
		public const uint MSREQ_DEPT_GET 	= 0x10;

		/*修改部门属性,	参数:UNIDEPT,	结果:UNIDEPT*/
		public const uint MSREQ_DEPT_SET 	= 0x20;

		/*删除部门,	参数:UNIDEPT,	结果:null*/
		public const uint MSREQ_DEPT_DEL 	= 0x30;

		/*获取校区列表,	参数:CAMPUSREQ,	结果:UNICAMPUS(Array)*/
		public const uint MSREQ_CAMPUS_GET 	= 0x40;

		/*修改校区属性,	参数:UNICAMPUS,	结果:UNICAMPUS*/
		public const uint MSREQ_CAMPUS_SET 	= 0x45;

		/*删除校区,	参数:UNICAMPUS,	结果:null*/
		public const uint MSREQ_CAMPUS_DEL 	= 0x49;

		/*获取班级列表,	参数:CLASSREQ,	结果:UNICLASS(Array)*/
		public const uint MSREQ_CLS_GET 	= 0x110;

		/*修改班级属性,	参数:UNICLASS,	结果:UNICLASS*/
		public const uint MSREQ_CLS_SET 	= 0x120;

		/*删除班级,	参数:UNICLASS,	结果:UNICLASS*/
		public const uint MSREQ_CLS_DEL 	= 0x130;

		/*获取帐户列表,	参数:ACCREQ,	结果:UNIACCOUNT(Array)*/
		public const uint MSREQ_ACC_GET 	= 0x210;

		/*获取导师,	参数:TUTORREQ,	结果:UNITUTOR(Array)*/
		public const uint MSREQ_TUTOR_GET 	= 0x211;

		/*获取导师的学生,	参数:TUTORSTUDENTREQ,	结果:TUTORSTUDENT(Array)*/
		public const uint MSREQ_TUTORSTUDENT_GET 	= 0x212;

		/*获取扩展身份人员信息,	参数:EXTIDENTACCREQ,	结果:EXTIDENTACC(Array)*/
		public const uint MSREQ_EXTIDENTACC_GET 	= 0x213;

		/*获取用户信息,	参数:ACCINFOREQ,	结果:UNIACCOUNT*/
		public const uint MSREQ_ACCINFO_GET 	= 0x214;

		/*修改帐户属性,	参数:UNIACCOUNT,	结果:UNIACCOUNT*/
		public const uint MSREQ_ACC_SET 	= 0x220;

		/*设置扩展身份人员,	参数:EXTIDENTACC,	结果:null*/
		public const uint MSREQ_EXTIDENTACC_SET 	= 0x221;

		/*设置导师的学生,	参数:TUTORSTUDENT,	结果:null*/
		public const uint MSREQ_TUTORSTUDENT_SET 	= 0x222;

		/*导师审核学生,	参数:TUTORSTUDENTCHECK,	结果:null*/
		public const uint MSREQ_TUTORSTUDENT_CHECK 	= 0x223;

		/*删除帐户,	参数:UNIACCOUNT,	结果:null*/
		public const uint MSREQ_ACC_DEL 	= 0x230;

		/*删除扩展身份人员,	参数:EXTIDENTACC,	结果:null*/
		public const uint MSREQ_EXTIDENTACC_DEL 	= 0x231;

		/*删除导师的学生,	参数:TUTORSTUDENT,	结果:null*/
		public const uint MSREQ_TUTORSTUDENT_DEL 	= 0x232;

		/*用户认证,	参数:ACCCHECKREQ,	结果:UNIACCOUNT*/
		public const uint MSREQ_ACC_CHECK 	= 0x240;

		/*存退款，补助，免费机时,	参数:UNIDEPOSIT,	结果:null*/
		public const uint MSREQ_ACC_DEPOSIT 	= 0x250;

		/*支付结算提交消费流水,	参数:UNIPAYMENT,	结果:UNIPAYMENT*/
		public const uint MSREQ_ACC_PAYMENT 	= 0x260;

		/*获取各类人员间需相互通知的信息,	参数:NOTICEREQ,	结果:NOTICEINFO(Array)*/
		public const uint MSREQ_ACC_NOTICEGET 	= 0x270;

		/*确认通知消息已收到,	参数:NOTICEAFFIRM,	结果:NOTICEAFFIRM*/
		public const uint MSREQ_ACC_NOTICEAFFIRM 	= 0x271;

		/*获取审查信息,	参数:ACCREQ,	结果:UNIACCEXTINFO(Array)*/
		public const uint MSREQ_EXTINFO_GET 	= 0x280;

		/*获取专业列表,	参数:MAJORREQ,	结果:UNIMAJOR(Array)*/
		public const uint MSREQ_MAJOR_GET 	= 0x310;

		/*修改专业属性,	参数:UNIMAJOR,	结果:UNIMAJOR*/
		public const uint MSREQ_MAJOR_SET 	= 0x311;

		/*删除专业,	参数:UNIMAJOR,	结果:null*/
		public const uint MSREQ_MAJOR_DEL 	= 0x312;

		/*获取实验数据记录,	参数:TESTDATAREQ,	结果:UNITESTDATA(Array)*/
		public const uint MSREQ_TESTDATA_GET 	= 0x601;

		/*上传实验数据,	参数:UNITESTDATA,	结果:UNITESTDATA*/
		public const uint MSREQ_TESTDATA_UPLOAD 	= 0x602;

		/*修改实验数据状态,	参数:UNITESTDATA,	结果:null*/
		public const uint MSREQ_TESTDATA_CHGSTAT 	= 0x603;

		/*管理员上传实验数据,	参数:ADMINTESTDATA,	结果:UNITESTDATA*/
		public const uint MSREQ_TESTDATA_ADMINUPLOAD 	= 0x604;

		/*打开网络硬盘,	参数:CLOUDDISKREQ,	结果:CLOUDDISK(Array)*/
		public const uint MSREQ_CLOUDDISK_OPEN 	= 0x608;

		/*保存文件到网络硬盘,	参数:CLOUDDISK,	结果:CLOUDDISK*/
		public const uint MSREQ_CLOUDDISK_SAVE 	= 0x609;

		/*从网络硬盘删除文件,	参数:CLOUDDISK,	结果:null*/
		public const uint MSREQ_CLOUDDISK_DEL 	= 0x60a;

		/*网络硬盘使用统计,	参数:CDISKSTATREQ,	结果:CDISKSTAT*/
		public const uint MSREQ_CLOUDDISK_STAT 	= 0x60b;

		/*获取任课教师,	参数:UNITEACHERREQ,	结果:UNITEACHER(Array)*/
		public const uint MSREQ_TEACHER_GET 	= 0x611;

		/*设置任课教师,	参数:UNITEACHER,	结果:UNITEACHER*/
		public const uint MSREQ_TEACHER_SET 	= 0x612;

		/*删除任课教师,	参数:UNITEACHER,	结果:null*/
		public const uint MSREQ_TEACHER_DEL 	= 0x613;

		/*获取当前用户使用情况(类似控制台刷卡）,	参数:USERCURINFOREQ,	结果:USERCURINFO*/
		public const uint MSREQ_USERCURINFO_GET 	= 0x620;


	}
/*结束命令*/
	/*结束接口--Account*/
	/*开始接口--Device--设备管理*/
/*开始命令*/
	public partial class PRDevice
	{

		/*获取实验室列表,	参数:LABREQ,	结果:UNILAB(Array)*/
		public const uint MSREQ_LAB_GET 	= 0x10;

		/*获取实验室全信息列表,	参数:FULLLABREQ,	结果:FULLLAB(Array)*/
		public const uint MSREQ_FULLLAB_GET 	= 0x11;

		/*设置实验室信息,	参数:UNILAB,	结果:UNILAB*/
		public const uint MSREQ_LAB_SET 	= 0x20;

		/*删除实验室,	参数:UNILAB,	结果:null*/
		public const uint MSREQ_LAB_DEL 	= 0x30;

		/*获取设备列表,	参数:DEVREQ,	结果:UNIDEVICE(Array)*/
		public const uint MSREQ_DEVICE_GET 	= 0x110;

		/*获取可预约设备列表,	参数:DEVFORRESVREQ,	结果:DEVFORRESV(Array)*/
		public const uint MSREQ_DEVFORRESV_GET 	= 0x111;

		/*获取可预约设备列表(按类型),	参数:DEVKINDFORRESVREQ,	结果:DEVKINDFORRESV(Array)*/
		public const uint MSREQ_DEVKINDFORRESV_GET 	= 0x112;

		/*获取已生效预约可用设备列表,	参数:RESVUSABLEDEVREQ,	结果:RESVUSABLEDEV(Array)*/
		public const uint MSREQ_RESVUSABLEDEV_GET 	= 0x113;

		/*获取设备预约状态,	参数:DEVRESVSTATREQ,	结果:DEVRESVSTAT(Array)*/
		public const uint MSREQ_DEVRESVSTAT_GET 	= 0x114;

		/*获取实验室预约状态,	参数:LABRESVSTATREQ,	结果:LABRESVSTAT(Array)*/
		public const uint MSREQ_LABRESVSTAT_GET 	= 0x115;

		/*获取设备长期预约状态,	参数:DEVLONGRESVSTATREQ,	结果:DEVLONGRESVSTAT(Array)*/
		public const uint MSREQ_DEVLONGRESVSTAT_GET 	= 0x116;

		/*获取可长期预约设备列表(按类型),	参数:DEVKINDFORLONGRESVREQ,	结果:DEVKINDFORRESV(Array)*/
		public const uint MSREQ_DEVKINDFORLONGRESV_GET 	= 0x117;

		/*获取房间预约状态,	参数:ROOMRESVSTATREQ,	结果:ROOMRESVSTAT(Array)*/
		public const uint MSREQ_ROOMRESVSTAT_GET 	= 0x118;

		/*获取设备使用费的经费分配比例,	参数:DEVFARREQ,	结果:DEVFAR(Array)*/
		public const uint MSREQ_DEVALLOCATIONRATE_GET 	= 0x119;

		/*获取设备配置表,	参数:DEVCFGREQ,	结果:DEVCFG(Array)*/
		public const uint MSREQ_DEVCFG_GET 	= 0x11A;

		/*获取设备配置类别表,	参数:DEVCFGKINDREQ,	结果:DEVCFGKIND(Array)*/
		public const uint MSREQ_DEVCFGKIND_GET 	= 0x11B;

		/*获取可预约设备列表(按房间),	参数:ROOMFORRESVREQ,	结果:ROOMFORRESV(Array)*/
		public const uint MSREQ_ROOMFORRESV_GET 	= 0x11C;

		/*获取房间组合预约状态,	参数:RGRESVSTATREQ,	结果:RGRESVSTAT(Array)*/
		public const uint MSREQ_RGRESVSTAT_GET 	= 0x11D;

		/*设置设备信息,	参数:UNIDEVICE,	结果:UNIDEVICE*/
		public const uint MSREQ_DEVICE_SET 	= 0x120;

		/*设置设备值班员,	参数:DEVATTENDANT,	结果:null*/
		public const uint MSREQ_DEVATTENDANT_SET 	= 0x121;

		/*设置设备使用费的经费分配比例,	参数:DEVFAR,	结果:null*/
		public const uint MSREQ_DEVALLOCATIONRATE_SET 	= 0x122;

		/*设置设备配置表,	参数:DEVCFG,	结果:null*/
		public const uint MSREQ_DEVCFG_SET 	= 0x123;

		/*上传智能检测座位状态,	参数:SEATDETECTSTAT,	结果:null*/
		public const uint MSREQ_SEATDETECTSTAT_UPLOAD 	= 0x124;

		/*删除设备,	参数:UNIDEVICE,	结果:null*/
		public const uint MSREQ_DEVICE_DEL 	= 0x130;

		/*设置设备配置表,	参数:DEVCFG,	结果:null*/
		public const uint MSREQ_DEVCFG_DEL 	= 0x131;

		/*管理员人工管理设备使用,	参数:DEVMANUSE,	结果:null*/
		public const uint MSREQ_DEVICE_MANUSE 	= 0x132;

		/*客户端注册,	参数:DEVREGISTREQ,	结果:DEVREGISTRES*/
		public const uint CSREQ_DEVICE_REGIST 	= 0x140;

		/*用户从客户端登录,	参数:DEVLOGONREQ,	结果:DEVLOGONRES*/
		public const uint CSREQ_DEVICE_LOGON 	= 0x141;

		/*用户从客户端查询使用信息,	参数:DEVQUERYREQ,	结果:UNIACCTINFO*/
		public const uint CSREQ_DEVICE_QUERY 	= 0x142;

		/*用户从客户端注销,	参数:DEVLOGOUTREQ,	结果:DEVLOGOUTRES*/
		public const uint CSREQ_DEVICE_LOGOUT 	= 0x143;

		/*使用中的客户端与服务定时通信,	参数:DEVHANDSHAKEREQ,	结果:DEVHANDSHAKERES*/
		public const uint CSREQ_DEVICE_HANDSHAKE 	= 0x144;

		/*上传机器软件信息,	参数:PCPROGRAM,	结果:null*/
		public const uint CSREQ_PCSW_UPLOAD 	= 0x145;

		/*上网认证（是否允许访问）,	参数:URLCHECKINFO,	结果:null*/
		public const uint CSREQ_URL_CHECK 	= 0x146;

		/*客户端修改密码,	参数:CLTCHGPWINFO,	结果:null*/
		public const uint CSREQ_CLIENT_CHGPW 	= 0x147;

		/*程序开始运行,	参数:PCPROGRAM,	结果:null*/
		public const uint CSREQ_PROGRAM_BEGIN 	= 0x148;

		/*程序停止运行,	参数:PROGEND,	结果:null*/
		public const uint CSREQ_PROGRAM_END 	= 0x149;

		/*服务端控制设备，要求设备执行某操作,	参数:DEVCTRLINFO,	结果:DEVCTRLINFO*/
		public const uint MSREQ_DEVICE_CTRL 	= 0x150;

		/*设置上网监控模式,	参数:CTRLREQ,	结果:null*/
		public const uint MSREQ_DEVICE_URLCTRL 	= 0x151;

		/*设置软件监控模式,	参数:CTRLREQ,	结果:null*/
		public const uint MSREQ_DEVICE_SWCTRL 	= 0x152;

		/*获取正在运行程序,	参数:RUNAPPREQ,	结果:RUNAPP(Array)*/
		public const uint MSREQ_RUNAPP_GET 	= 0x153;

		/*上传机器软件信息结束,	参数:SWUPLOADEND,	结果:null*/
		public const uint CSREQ_PCSW_UPLOAD_END 	= 0x154;

		/*借出设备,	参数:DEVLOANREQ,	结果:null*/
		public const uint MSREQ_DEVICE_LOAN 	= 0x160;

		/*归还设备,	参数:DEVRETURNREQ,	结果:null*/
		public const uint MSREQ_DEVICE_RETURN 	= 0x161;

		/*获取设备损坏记录,	参数:DEVDAMAGERECREQ,	结果:DEVDAMAGEREC(Array)*/
		public const uint MSREQ_DEVDAMAGEREC_GET 	= 0x170;

		/*设备维修处理,	参数:DEVDAMAGEREC,	结果:DEVDAMAGEREC*/
		public const uint MSREQ_DEVICE_REPAIR 	= 0x171;

		/*设备对控制结果的反馈,	参数:DEVCTRLINFO,	结果:null*/
		public const uint CSREQ_DEVICE_CTRLRES 	= 0x1a0;

		/*获取设备功能类别列表,	参数:DEVCLSREQ,	结果:UNIDEVCLS(Array)*/
		public const uint MSREQ_DEVCLS_GET 	= 0x210;

		/*设置设备功能类别信息,	参数:UNIDEVCLS,	结果:UNIDEVCLS*/
		public const uint MSREQ_DEVCLS_SET 	= 0x220;

		/*删除设备功能类别,	参数:UNIDEVCLS,	结果:null*/
		public const uint MSREQ_DEVCLS_DEL 	= 0x230;

		/*获取设备名称类别列表,	参数:DEVKINDREQ,	结果:UNIDEVKIND(Array)*/
		public const uint MSREQ_DEVKIND_GET 	= 0x310;

		/*设置设备名称类别信息,	参数:UNIDEVKIND,	结果:UNIDEVKIND*/
		public const uint MSREQ_DEVKIND_SET 	= 0x320;

		/*删除设备名称类别,	参数:UNIDEVKIND,	结果:null*/
		public const uint MSREQ_DEVKIND_DEL 	= 0x330;

		/*获取楼宇信息,	参数:BUILDINGREQ,	结果:UNIBUILDING(Array)*/
		public const uint MSREQ_BUILDING_GET 	= 0x340;

		/*设置楼宇,	参数:UNIBUILDING,	结果:UNIBUILDING*/
		public const uint MSREQ_BUILDING_SET 	= 0x341;

		/*删除楼宇,	参数:UNIBUILDING,	结果:null*/
		public const uint MSREQ_BUILDING_DEL 	= 0x342;

		/*获取房间信息,	参数:ROOMREQ,	结果:UNIROOM(Array)*/
		public const uint MSREQ_ROOM_GET 	= 0x350;

		/*设置房间,	参数:UNIROOM,	结果:UNIROOM*/
		public const uint MSREQ_ROOM_SET 	= 0x351;

		/*删除房间,	参数:UNIROOM,	结果:null*/
		public const uint MSREQ_ROOM_DEL 	= 0x352;

		/*服务端控制房间，要求房间执行某操作,	参数:ROOMCTRLINFO,	结果:ROOMCTRLINFO*/
		public const uint MSREQ_ROOM_CTRL 	= 0x353;

		/*获取用户可进入房间,	参数:PERMITROOMREQ,	结果:PERMITROOMINFO(Array)*/
		public const uint MSREQ_ROOM_PERMITROOM 	= 0x354;

		/*获取房间控制信息,	参数:ROOMCTRLREQ,	结果:UNIDOORCTRL(Array)*/
		public const uint MSREQ_ROOM_CTRLINFO 	= 0x355;

		/*获取房间完整信息,	参数:FULLROOMREQ,	结果:FULLROOM(Array)*/
		public const uint MSREQ_FULLROOM_GET 	= 0x356;

		/*获取房间名称信息（用于下拉框或复选框）,	参数:BASICROOMREQ,	结果:BASICROOM(Array)*/
		public const uint MSREQ_BASICROOM_GET 	= 0x357;

		/*获取通道门信息,	参数:CHANNELGATEREQ,	结果:UNICHANNELGATE(Array)*/
		public const uint MSREQ_CHANNELGATE_GET 	= 0x360;

		/*设置通道门,	参数:UNICHANNELGATE,	结果:UNICHANNELGATE*/
		public const uint MSREQ_CHANNELGATE_SET 	= 0x361;

		/*删除通道门,	参数:UNICHANNELGATE,	结果:null*/
		public const uint MSREQ_CHANNELGATE_DEL 	= 0x362;

		/*服务端控制通道门，要求通道门执行某操作,	参数:CHANNELGATECTRLINFO,	结果:CHANNELGATECTRLINFO*/
		public const uint MSREQ_CHANNELGATE_CTRL 	= 0x363;

		/*获取房间组合,	参数:ROOMGROUPREQ,	结果:ROOMGROUP(Array)*/
		public const uint MSREQ_ROOMGROUP_GET 	= 0x370;

		/*设置房间组合,	参数:ROOMGROUP,	结果:ROOMGROUP*/
		public const uint MSREQ_ROOMGROUP_SET 	= 0x371;

		/*删除房间组合,	参数:ROOMGROUP,	结果:null*/
		public const uint MSREQ_ROOMGROUP_DEL 	= 0x372;

		/*获取设备监控器,	参数:DEVMONITORREQ,	结果:DEVMONITOR(Array)*/
		public const uint MSREQ_DEVMONITOR_GET 	= 0x380;

		/*设置设备监控器,	参数:DEVMONITOR,	结果:DEVMONITOR*/
		public const uint MSREQ_DEVMONITOR_SET 	= 0x381;

		/*删除设备监控器,	参数:DEVMONITOR,	结果:null*/
		public const uint MSREQ_DEVMONITOR_DEL 	= 0x382;

		/*获取监控器与设备的对应关系,	参数:MONDEVREQ,	结果:MONDEV(Array)*/
		public const uint MSREQ_MONDEV_GET 	= 0x385;

		/*设置监控器与设备的对应关系,	参数:MONDEV,	结果:MONDEV*/
		public const uint MSREQ_MONDEV_SET 	= 0x386;

		/*删除监控器与设备的对应关系,	参数:MONDEV,	结果:null*/
		public const uint MSREQ_MONDEV_DEL 	= 0x387;

		/*获取设备开放时间表,	参数:DEVOPENRULEREQ,	结果:DEVOPENRULE(Array)*/
		public const uint MSREQ_DEVOPENRULE_GET 	= 0x410;

		/*设置设备开放时间表,	参数:DEVOPENRULE,	结果:DEVOPENRULE*/
		public const uint MSREQ_DEVOPENRULE_SET 	= 0x420;

		/*删除设备开放时间表,	参数:DEVOPENRULE,	结果:null*/
		public const uint MSREQ_DEVOPENRULE_DEL 	= 0x430;

		/*设置组开放时间表,	参数:CHANGEGROUPOPENRULE,	结果:CHANGEGROUPOPENRULE*/
		public const uint MSREQ_GROUPOPENRULE_SET 	= 0x440;

		/*删除组开放时间表,	参数:CHANGEGROUPOPENRULE,	结果:null*/
		public const uint MSREQ_GROUPOPENRULE_DEL 	= 0x441;

		/*获取组开放时间表,	参数:GROUPOPENRULEREQ,	结果:GROUPOPENRULE(Array)*/
		public const uint MSREQ_GROUPOPENRULE_GET 	= 0x412;

		/*设置时间期间开放时间表,	参数:CHANGEPERIODOPENRULE,	结果:CHANGEPERIODOPENRULE*/
		public const uint MSREQ_PERIODOPENRULE_SET 	= 0x445;

		/*删除时间期间开放时间表,	参数:CHANGEPERIODOPENRULE,	结果:null*/
		public const uint MSREQ_PERIODOPENRULE_DEL 	= 0x446;

		/*获取时间期间开放时间表,	参数:PERIODOPENRULEREQ,	结果:PERIODOPENRULE(Array)*/
		public const uint MSREQ_PERIODOPENRULE_GET 	= 0x417;

		/*获取当前设备统计信息,	参数:,	结果:CURDEVSTAT*/
		public const uint MSREQ_CURDEV_STAT 	= 0x501;

		/*获取教学用设备按节次统计,	参数:DEVFORTREQ,	结果:DEVSECINFO(Array)*/
		public const uint MSREQ_DEVFORT_STAT 	= 0x502;

		/*获取教学用设备,	参数:TEACHINGDEVREQ,	结果:TEACHINGDEV(Array)*/
		public const uint MSREQ_TEACHINGDEV_GET 	= 0x503;

		/*获取获奖记录,	参数:REWARDRECREQ,	结果:REWARDREC(Array)*/
		public const uint MSREQ_REWARDREC_GET 	= 0x511;

		/*设置获奖记录,	参数:REWARDREC,	结果:REWARDREC*/
		public const uint MSREQ_REWARDREC_SET 	= 0x512;

		/*删除获奖记录,	参数:REWARDREC,	结果:null*/
		public const uint MSREQ_REWARDREC_DEL 	= 0x513;

		/*获取费用记录,	参数:COSTRECREQ,	结果:COSTREC(Array)*/
		public const uint MSREQ_COSTREC_GET 	= 0x521;

		/*设置费用记录,	参数:COSTREC,	结果:COSTREC*/
		public const uint MSREQ_COSTREC_SET 	= 0x522;

		/*删除费用记录,	参数:COSTREC,	结果:null*/
		public const uint MSREQ_COSTREC_DEL 	= 0x523;


	}
/*结束命令*/
	/*结束接口--Device*/
	/*开始接口--DoorCtrlSrv--门禁设置管理*/
/*开始命令*/
	public partial class PRDoorCtrlSrv
	{

		/*获取门禁集控器,	参数:DCSREQ,	结果:UNIDCS(Array)*/
		public const uint MSREQ_DCS_GET 	= 0x10;

		/*设置门禁集控器,	参数:UNIDCS,	结果:UNIDCS*/
		public const uint MSREQ_DCS_SET 	= 0x20;

		/*删除门禁集控器,	参数:UNIDCS,	结果:null*/
		public const uint MSREQ_DCS_DEL 	= 0x30;

		/*门禁集控器登录,	参数:DCSLOGINREQ,	结果:DCSLOGINRES*/
		public const uint MSREQ_DCS_LOGIN 	= 0x110;

		/*门禁集控器退出,	参数:DCSLOGOUTREQ,	结果:null*/
		public const uint MSREQ_DCS_LOGOUT 	= 0x120;

		/*定时通信,	参数:DCSPULSEREQ,	结果:DCSPULSERES*/
		public const uint MSREQ_DCS_PULSE 	= 0x130;

		/*用户在门禁刷卡器上刷卡,	参数:DOORCARDREQ,	结果:DOORCARDRES*/
		public const uint MSREQ_DCS_DOORCARD 	= 0x140;

		/*用户用手机扫二维码开门,	参数:MOBILEOPENDOORREQ,	结果:MOBILEOPENDOORRES*/
		public const uint MSREQ_MOBILE_OPENDOOR 	= 0x141;

		/*获取门禁控制器,	参数:DOORCTRLREQ,	结果:UNIDOORCTRL(Array)*/
		public const uint MSREQ_DOORCTRL_GET 	= 0x210;

		/*设置门禁控制器,	参数:UNIDOORCTRL,	结果:UNIDOORCTRL*/
		public const uint MSREQ_DOORCTRL_SET 	= 0x220;

		/*删除门禁控制器,	参数:UNIDOORCTRL,	结果:null*/
		public const uint MSREQ_DOORCTRL_DEL 	= 0x230;

		/*给门禁控制器发命令,	参数:DOORCTRLCMD,	结果:null*/
		public const uint MSREQ_DOORCTRL_CMD 	= 0x240;


	}
/*结束命令*/
	/*结束接口--DoorCtrlSrv*/
	/*开始接口--Group--用户组管理*/
/*开始命令*/
	public partial class PRGroup
	{

		/*获取组,	参数:GROUPREQ,	结果:UNIGROUP(Array)*/
		public const uint MSREQ_GROUP_GET 	= 0x10;

		/*修改组,	参数:UNIGROUP,	结果:UNIGROUP*/
		public const uint MSREQ_GROUP_SET 	= 0x20;

		/*删除组,	参数:UNIGROUP,	结果:null*/
		public const uint MSREQ_GROUP_DEL 	= 0x30;

		/*添加组成员,	参数:GROUPMEMBER,	结果:null*/
		public const uint MSREQ_GROUPMEMBER_SET 	= 0x110;

		/*删除组成员,	参数:GROUPMEMBER,	结果:null*/
		public const uint MSREQ_GROUPMEMBER_DEL 	= 0x120;

		/*获取组成员明细,	参数:GROUPMEMDETAILREQ,	结果:GROUPMEMDETAIL(Array)*/
		public const uint MSREQ_GROUPMEMDETAIL_GET 	= 0x130;


	}
/*结束命令*/
	/*结束接口--Group*/
	/*开始接口--Reserve--预约管理*/
/*开始命令*/
	public partial class PRReserve
	{

		/*获取预约列表,	参数:RESVREQ,	结果:UNIRESERVE(Array)*/
		public const uint MSREQ_RESERVE_GET 	= 0x10;

		/*获取预约列表用于网站显示,	参数:RESVSHOWREQ,	结果:RESVSHOW(Array)*/
		public const uint MSREQ_RESERVE_GETSHOW 	= 0x11;

		/*获取教学预约列表,	参数:TEACHINGRESVREQ,	结果:TEACHINGRESV(Array)*/
		public const uint MSREQ_TEACHINGRESV_GET 	= 0x12;

		/*获取科研实验预约列表,	参数:RTRESVREQ,	结果:RTRESV(Array)*/
		public const uint MSREQ_RTRESV_GET 	= 0x13;

		/*获取科研实验账单,	参数:RTRESVBILLREQ,	结果:RTRESVBILL*/
		public const uint MSREQ_RTBILL_GET 	= 0x14;

		/*获取科研实验账单,	参数:RESVTIMEREQ,	结果:RESVTIME(Array)*/
		public const uint MSREQ_RESVTIME_GET 	= 0x15;

		/*设置预约信息,	参数:UNIRESERVE,	结果:UNIRESERVE*/
		public const uint MSREQ_RESERVE_SET 	= 0x20;

		/*自动预约，系统自动分配设备,	参数:AUTORESVREQ,	结果:UNIRESERVE*/
		public const uint MSREQ_RESERVE_AUTO 	= 0x21;

		/*放假调休(比如10月5日（星期五）调到9月29日（星期六）,	参数:HOLIDAYSHIFT,	结果:null*/
		public const uint MSREQ_RESERVE_HOLIDAYSHIFT 	= 0x22;

		/*加入预约小组,	参数:RESVMEMBER,	结果:null*/
		public const uint MSREQ_RESVMEMBER_JOIN 	= 0x23;

		/*退出预约小组,	参数:RESVMEMBER,	结果:null*/
		public const uint MSREQ_RESVMEMBER_EXIT 	= 0x24;

		/*取消预约签到限制,	参数:UNIRESERVE,	结果:null*/
		public const uint MSREQ_RESERVE_CANCELSIGN 	= 0x25;

		/*新建修改科研实验预约,	参数:RTRESV,	结果:RTRESV*/
		public const uint MSREQ_RTRESV_SET 	= 0x26;

		/*科研实验预约审核,	参数:RTRESVCHECK,	结果:null*/
		public const uint MSREQ_RTRESV_CHECK 	= 0x27;

		/*科研实验预收费,	参数:RTPREPAY,	结果:null*/
		public const uint MSREQ_RTRESV_PREPAY 	= 0x28;

		/*科研实验账单审核,	参数:RTBILLCHECK,	结果:null*/
		public const uint MSREQ_RTBILL_CHECK 	= 0x29;

		/*科研实验账单结算,	参数:RTBILLSETTLE,	结果:null*/
		public const uint MSREQ_RTBILL_SETTLE 	= 0x2A;

		/*科研实验账单入账,	参数:RTBILLRECEIVE,	结果:null*/
		public const uint MSREQ_RTBILL_RECEIVE 	= 0x2B;

		/*设置免登录预约,	参数:ANONYMOUSRESV,	结果:ANONYMOUSRESV*/
		public const uint MSREQ_ANONYMOUSRESV_SET 	= 0x2C;

		/*设置全体学生预约,	参数:ALLUSERRESV,	结果:ALLUSERRESV*/
		public const uint MSREQ_ALLUSERRESV_SET 	= 0x2D;

		/*删除预约,	参数:UNIRESERVE,	结果:null*/
		public const uint MSREQ_RESERVE_DEL 	= 0x30;

		/*删除科研实验预约,	参数:RTRESV,	结果:null*/
		public const uint MSREQ_RTRESV_DEL 	= 0x31;

		/*提前结束预约,	参数:UNIRESERVE,	结果:null*/
		public const uint MSREQ_RESERVE_EARLYEND 	= 0x32;

		/*调整预约结束时间,	参数:RESVENDTIME,	结果:RESVENDTIME*/
		public const uint MSREQ_RESERVE_CHGENDTIME 	= 0x40;

		/*获取设备预约表,	参数:DEVRESVREQ,	结果:DEVRESVINFO(Array)*/
		public const uint MSREQ_DEVRESV_GET 	= 0x50;

		/*预约费用核算,计算本次预约使用后的实际发生费用。,	参数:RESVCOSTADJUST,	结果:RESVCOSTADJUST*/
		public const uint MSREQ_RESERVE_COSTADJUST 	= 0x61;

		/*预约费用结算,与使用者结算本次预约发生的费用。,	参数:RESVCHECKOUT,	结果:RESVCHECKOUT*/
		public const uint MSREQ_RESERVE_CHECKOUT 	= 0x62;

		/*获取学期表,	参数:TERMREQ,	结果:UNITERM(Array)*/
		public const uint MSREQ_TERM_GET 	= 0x110;

		/*设置学期表,	参数:UNITERM,	结果:UNITERM*/
		public const uint MSREQ_TERM_SET 	= 0x120;

		/*删除学期表,	参数:UNITERM,	结果:null*/
		public const uint MSREQ_TERM_DEL 	= 0x130;

		/*获取作息表,	参数:CTSREQ,	结果:CLASSTIMETABLE(Array)*/
		public const uint MSREQ_CTS_GET 	= 0x140;

		/*设置作息表,	参数:CLASSTIMETABLE,	结果:null*/
		public const uint MSREQ_CTS_SET 	= 0x141;

		/*获取课程,	参数:COURSEREQ,	结果:UNICOURSE(Array)*/
		public const uint MSREQ_COURSE_GET 	= 0x210;

		/*设置课程,	参数:UNICOURSE,	结果:UNICOURSE*/
		public const uint MSREQ_COURSE_SET 	= 0x220;

		/*删除课程,	参数:UNICOURSE,	结果:null*/
		public const uint MSREQ_COURSE_DEL 	= 0x230;

		/*获取实验项目卡,	参数:TESTCARDREQ,	结果:TESTCARD(Array)*/
		public const uint MSREQ_TESTCARD_GET 	= 0x250;

		/*设置实验项目卡,	参数:TESTCARD,	结果:TESTCARD*/
		public const uint MSREQ_TESTCARD_SET 	= 0x251;

		/*删除实验项目卡,	参数:TESTCARD,	结果:null*/
		public const uint MSREQ_TESTCARD_DEL 	= 0x252;

		/*获取实验计划,	参数:TESTPLANREQ,	结果:UNITESTPLAN(Array)*/
		public const uint MSREQ_TESTPLAN_GET 	= 0x310;

		/*设置实验计划,	参数:UNITESTPLAN,	结果:UNITESTPLAN*/
		public const uint MSREQ_TESTPLAN_SET 	= 0x311;

		/*删除实验计划,	参数:UNITESTPLAN,	结果:null*/
		public const uint MSREQ_TESTPLAN_DEL 	= 0x312;

		/*获取实验项目,	参数:TESTITEMREQ,	结果:UNITESTITEM(Array)*/
		public const uint MSREQ_TESTITEM_GET 	= 0x320;

		/*设置实验项目,	参数:UNITESTITEM,	结果:UNITESTITEM*/
		public const uint MSREQ_TESTITEM_SET 	= 0x321;

		/*删除实验项目,	参数:UNITESTITEM,	结果:null*/
		public const uint MSREQ_TESTITEM_DEL 	= 0x322;

		/*获取实验项目试验者预约详细信息,	参数:TESTITEMMEMRESVREQ,	结果:TESTITEMMEMRESV(Array)*/
		public const uint MSREQ_TESTITEMMEMRESV_GET 	= 0x323;

		/*获取实验项目详细信息,	参数:TESTITEMINFOREQ,	结果:TESTITEMINFO(Array)*/
		public const uint MSREQ_TESTITEMINFO_GET 	= 0x328;

		/*老师提交实验报告模板,	参数:REPORTFORMUPLOAD,	结果:null*/
		public const uint MSREQ_REPORTFORM_UPLOAD 	= 0x331;

		/*学生交实验报告,	参数:REPORTUPLOAD,	结果:null*/
		public const uint MSREQ_REPORT_UPLOAD 	= 0x332;

		/*批改实验报告,	参数:REPORTCORRECT,	结果:null*/
		public const uint MSREQ_REPORT_CORRECT 	= 0x333;

		/*获取活动安排,	参数:ACTIVITYPLANREQ,	结果:UNIACTIVITYPLAN(Array)*/
		public const uint MSREQ_ACTIVITYPLAN_GET 	= 0x350;

		/*设置活动安排,	参数:UNIACTIVITYPLAN,	结果:UNIACTIVITYPLAN*/
		public const uint MSREQ_ACTIVITYPLAN_SET 	= 0x351;

		/*删除活动安排,	参数:UNIACTIVITYPLAN,	结果:null*/
		public const uint MSREQ_ACTIVITYPLAN_DEL 	= 0x352;

		/*获取活动安排的座位列表,	参数:APSEATREQ,	结果:APSEAT(Array)*/
		public const uint MSREQ_APSEAT_GET 	= 0x353;

		/*申请参加活动,	参数:ACTIVITYENROLL,	结果:null*/
		public const uint MSREQ_ACTIVITY_ENROLL 	= 0x354;

		/*退出活动申请,	参数:ACTIVITYEXIT,	结果:null*/
		public const uint MSREQ_ACTIVITY_EXIT 	= 0x355;

		/*管理员导入签到人员名单,	参数:AOFFLINESIGN,	结果:AOFFLINESIGN*/
		public const uint MSREQ_ACTIVITY_OFFLINESIGN 	= 0x365;

		/*获取预约规则列表,	参数:RESVRULEREQ,	结果:UNIRESVRULE(Array)*/
		public const uint MSREQ_RESVRULE_GET 	= 0x410;

		/*管理员获取预约规则列表,	参数:RESVRULEADMINREQ,	结果:UNIRESVRULE(Array)*/
		public const uint MSREQ_RESVRULE_ADMINGET 	= 0x411;

		/*设置预约规则,	参数:UNIRESVRULE,	结果:UNIRESVRULE*/
		public const uint MSREQ_RESVRULE_SET 	= 0x420;

		/*删除预约规则,	参数:UNIRESVRULE,	结果:null*/
		public const uint MSREQ_RESVRULE_DEL 	= 0x430;

		/*获取科研实验,	参数:RESEARCHTESTREQ,	结果:RESEARCHTEST(Array)*/
		public const uint MSREQ_RESEARCHTEST_GET 	= 0x451;

		/*新建/修改科研实验,	参数:RESEARCHTEST,	结果:RESEARCHTEST*/
		public const uint MSREQ_RESEARCHTEST_SET 	= 0x452;

		/*删除科研实验,	参数:RESEARCHTEST,	结果:null*/
		public const uint MSREQ_RESEARCHTEST_DEL 	= 0x453;

		/*设置科研实验成员,	参数:RTMEMBER,	结果:null*/
		public const uint MSREQ_RTMEMBER_SET 	= 0x454;

		/*删除科研实验成员,	参数:RTMEMBER,	结果:null*/
		public const uint MSREQ_RTMEMBER_DEL 	= 0x455;

		/*获取实验样品信息,	参数:SAMPLEINFOREQ,	结果:SAMPLEINFO(Array)*/
		public const uint MSREQ_SAMPLEINFO_GET 	= 0x461;

		/*新建/修改实验样品信息,	参数:SAMPLEINFO,	结果:SAMPLEINFO*/
		public const uint MSREQ_SAMPLEINFO_SET 	= 0x462;

		/*删除实验样品信息,	参数:SAMPLEINFO,	结果:null*/
		public const uint MSREQ_SAMPLEINFO_DEL 	= 0x463;

		/*获取场馆预约列表,	参数:YARDRESVREQ,	结果:YARDRESV(Array)*/
		public const uint MSREQ_YARDRESV_GET 	= 0x501;

		/*新建修改场馆预约,	参数:YARDRESV,	结果:YARDRESV*/
		public const uint MSREQ_YARDRESV_SET 	= 0x511;

		/*删除场馆预约,	参数:YARDRESV,	结果:null*/
		public const uint MSREQ_YARDRESV_DEL 	= 0x521;

		/*获取场馆预约审核列表,	参数:YARDRESVCHECKINFOREQ,	结果:YARDRESVCHECKINFO(Array)*/
		public const uint MSREQ_YARDRESVCHECKINFO_GET 	= 0x531;

		/*场馆预约审核,	参数:YARDRESVCHECK,	结果:null*/
		public const uint MSREQ_YARDRESV_CHECK 	= 0x535;

		/*获取场馆预约审核列表,	参数:RESVCHECKINFOREQ,	结果:RESVCHECKINFO(Array)*/
		public const uint MSREQ_RESVCHECKINFO_GET 	= 0x536;

		/*场馆预约审核,	参数:RESVCHECKINFO,	结果:null*/
		public const uint MSREQ_RESV_CHECK 	= 0x537;

		/*获取场馆活动列表,	参数:YARDACTIVITYREQ,	结果:YARDACTIVITY(Array)*/
		public const uint MSREQ_YARDACTIVITY_GET 	= 0x541;

		/*新建场馆活动,	参数:YARDACTIVITY,	结果:YARDACTIVITY*/
		public const uint MSREQ_YARDACTIVITY_SET 	= 0x545;

		/*删除场馆活动,	参数:YARDACTIVITY,	结果:null*/
		public const uint MSREQ_YARDACTIVITY_DEL 	= 0x548;

		/*第三方预约共享设备（解决资源冲突，本地不执行第三方预约）,	参数:THIRDRESVSHAREDEV,	结果:THIRDRESVSHAREDEV*/
		public const uint MSREQ_THIRDRESV_SHAREDEV 	= 0x551;

		/*第三方删除预约共享设备,	参数:THIRDRESVDEL,	结果:null*/
		public const uint MSREQ_THIRDRESV_DEL 	= 0x552;

		/*获取第三方预约列表,	参数:THIRDRESVREQ,	结果:THIRDRESV(Array)*/
		public const uint MSREQ_THIRDRESV_GET 	= 0x555;


	}
/*结束命令*/
	/*结束接口--Reserve*/
	/*开始接口--Control--上网游戏控制*/
/*开始命令*/
	public partial class PRControl
	{

		/*获取监控分类库,	参数:CTRLCLASSREQ,	结果:UNICTRLCLASS(Array)*/
		public const uint MSREQ_CTRLCLASS_GET 	= 0x10;

		/*修改监控分类库,	参数:UNICTRLCLASS,	结果:UNICTRLCLASS*/
		public const uint MSREQ_CTRLCLASS_SET 	= 0x20;

		/*删除监控分类库,	参数:UNICTRLCLASS,	结果:null*/
		public const uint MSREQ_CTRLCLASS_DEL 	= 0x30;

		/*获取网址组,	参数:CTRLURLREQ,	结果:UNICTRLURL(Array)*/
		public const uint MSREQ_CTRLURL_GET 	= 0x110;

		/*修改网址组,	参数:UNICTRLURL,	结果:UNICTRLURL*/
		public const uint MSREQ_CTRLURL_SET 	= 0x120;

		/*删除网址组,	参数:UNICTRLURL,	结果:null*/
		public const uint MSREQ_CTRLURL_DEL 	= 0x130;

		/*获取软件组,	参数:CTRLSWREQ,	结果:UNICTRLSW(Array)*/
		public const uint MSREQ_CTRLSW_GET 	= 0x210;

		/*修改软件组,	参数:UNICTRLSW,	结果:UNICTRLSW*/
		public const uint MSREQ_CTRLSW_SET 	= 0x220;

		/*删除软件组,	参数:UNICTRLSW,	结果:null*/
		public const uint MSREQ_CTRLSW_DEL 	= 0x230;

		/*获取软件,	参数:SOFTWAREREQ,	结果:UNISOFTWARE(Array)*/
		public const uint MSREQ_SOFTWARE_GET 	= 0x310;

		/*修改软件,	参数:UNISOFTWARE,	结果:UNISOFTWARE*/
		public const uint MSREQ_SOFTWARE_SET 	= 0x320;

		/*获取程序,	参数:PROGRAMREQ,	结果:UNIPROGRAM(Array)*/
		public const uint MSREQ_PROGRAM_GET 	= 0x330;

		/*修改程序,	参数:UNIPROGRAM,	结果:UNIPROGRAM*/
		public const uint MSREQ_PROGRAM_SET 	= 0x340;

		/*获取机器程序,	参数:PCSWINFOREQ,	结果:UNIPCSWINFO(Array)*/
		public const uint MSREQ_PCSWINFO_GET 	= 0x410;

		/*获取机房程序,	参数:ROOMSWINFOREQ,	结果:UNIROOMSWINFO(Array)*/
		public const uint MSREQ_ROOMSWINFO_GET 	= 0x420;


	}
/*结束命令*/
	/*结束接口--Control*/
	/*开始接口--THIRDIF--第三方接口管理*/
/*开始命令*/
	public partial class PRTHIRDIF
	{

		/*登录接口服务器,	参数:THIRDLOGINREQ,	结果:THIRDLOGINRES*/
		public const uint THIRDIF_LOGIN 	= 0x10;

		/*退出接口服务器,	参数:,	结果:null*/
		public const uint THIRDIF_LOGOUT 	= 0x20;

		/*获取帐户列表,	参数:THIRDACCREQ,	结果:UNIACCOUNT(Array)*/
		public const uint THIRDIF_ACC_GET 	= 0x30;

		/*获取所有帐户信息,	参数:SYNCACCREQ,	结果:SYNCACCINFO*/
		public const uint THIRDIF_SYNCACC 	= 0x110;


	}
/*结束命令*/
	/*结束接口--THIRDIF*/
	/*开始接口--Fee--收费管理*/
/*开始命令*/
	public partial class PRFee
	{

		/*获取收费标准列表,	参数:FEEREQ,	结果:UNIFEE(Array)*/
		public const uint MSREQ_FEE_GET 	= 0x10;

		/*获取科研项目对应的设备的收费标准,	参数:RTDEVFEEREQ,	结果:UNIFEE*/
		public const uint MSREQ_RTDEVFEE_GET 	= 0x11;

		/*获取科研项目对应的设备的样品及费率,	参数:RTDEVSAMPLEREQ,	结果:RTDEVSAMPLE(Array)*/
		public const uint MSREQ_RTDEVSAMPLE_GET 	= 0x12;

		/*设置收费标准信息,	参数:UNIFEE,	结果:UNIFEE*/
		public const uint MSREQ_FEE_SET 	= 0x20;

		/*删除收费标准,	参数:UNIFEE,	结果:null*/
		public const uint MSREQ_FEE_DEL 	= 0x30;

		/*获取机时使用规则,	参数:FTRULEREQ,	结果:FREETIMERULE(Array)*/
		public const uint MSREQ_FREETIMERULE_GET 	= 0x110;

		/*修改机时使用规则,	参数:FREETIMERULE,	结果:FREETIMERULE*/
		public const uint MSREQ_FREETIMERULE_SET 	= 0x120;

		/*删除机时使用规则,	参数:FREETIMERULE,	结果:null*/
		public const uint MSREQ_FREETIMERULE_DEL 	= 0x130;

		/*获取账单,	参数:BILLREQ,	结果:UNIBILL(Array)*/
		public const uint MSREQ_BILL_GET 	= 0x201;

		/*设置账单,	参数:UNIBILL,	结果:null*/
		public const uint MSREQ_BILL_SET 	= 0x202;

		/*账单缴费,	参数:BILLPAY,	结果:null*/
		public const uint MSREQ_BILL_PAY 	= 0x203;


	}
/*结束命令*/
	/*结束接口--Fee*/
	/*开始接口--Console--控制台管理*/
/*开始命令*/
	public partial class PRConsole
	{

		/*获取控制台列表,	参数:CONREQ,	结果:UNICONSOLE(Array)*/
		public const uint MSREQ_CONSOLE_GET 	= 0x10;

		/*设置控制台信息,	参数:UNICONSOLE,	结果:UNICONSOLE*/
		public const uint MSREQ_CONSOLE_SET 	= 0x20;

		/*删除控制台,	参数:UNICONSOLE,	结果:null*/
		public const uint MSREQ_CONSOLE_DEL 	= 0x30;

		/*控制台登录,	参数:CONLOGINREQ,	结果:CONLOGINRES*/
		public const uint MSREQ_CONSOLE_LOGIN 	= 0x110;

		/*控制台退出,	参数:CONLOGOUTREQ,	结果:null*/
		public const uint MSREQ_CONSOLE_LOGOUT 	= 0x120;

		/*定时通信,	参数:CONPULSEREQ,	结果:CONPULSERES*/
		public const uint MSREQ_CONSOLE_PULSE 	= 0x130;

		/*给控制台发消息,	参数:CONMESSAGE,	结果:null*/
		public const uint MSREQ_CONSOLE_SHOWMSG 	= 0x140;

		/*控制台刷卡,	参数:ACCCHECKREQ,	结果:CONUSERINFO*/
		public const uint MSREQ_CONSOLE_USERCARD 	= 0x210;

		/*控制台教师登录,	参数:ACCCHECKREQ,	结果:CONTEACHERINFO*/
		public const uint MSREQ_CONSOLE_TEACHERIN 	= 0x211;

		/*控制台刷卡上机,	参数:CARDFORPCREQ,	结果:CARDFORPCRES*/
		public const uint MSREQ_CONSOLE_CARDFORPC 	= 0x212;

		/*通道机刷卡,	参数:AUTOGATECARDREQ,	结果:AUTOGATECARDRES*/
		public const uint MSREQ_CONSOLE_AUTOGATECARD 	= 0x213;

		/*手机扫描二维码,	参数:MOBILESCANREQ,	结果:MOBILESCANRES*/
		public const uint MSREQ_MOBILE_SCAN 	= 0x214;

		/*控制台刷卡开始使用,	参数:CONUSERINREQ,	结果:CONUSERINRES*/
		public const uint MSREQ_CONSOLE_USERIN 	= 0x220;

		/*手机扫描开始使用,	参数:MOBILEUSERINREQ,	结果:MOBILEUSERINRES*/
		public const uint MSREQ_MOBILE_USERIN 	= 0x221;

		/*手机延时（续约),	参数:MOBILEDELAYREQ,	结果:MOBILEDELAYRES*/
		public const uint MSREQ_MOBILE_DELAY 	= 0x222;

		/*控制台刷卡结束使用,	参数:CONUSEROUTREQ,	结果:CONUSEROUTRES*/
		public const uint MSREQ_CONSOLE_USEROUT 	= 0x230;

		/*手机扫描刷卡结束使用,	参数:MOBILEUSEROUTREQ,	结果:MOBILEUSEROUTRES*/
		public const uint MSREQ_MOBILE_USEROUT 	= 0x231;

		/*手机登录签到,	参数:RESVUSERCOMEINREQ,	结果:RESVUSERCOMEINRES*/
		public const uint MSREQ_RESVUSER_COMEIN 	= 0x241;

		/*手机登录延时（续约),	参数:RESVUSERDELAYREQ,	结果:RESVUSERDELAYRES*/
		public const uint MSREQ_RESVUSER_DELAY 	= 0x245;

		/*手机登录离开,	参数:RESVUSERGOOUTREQ,	结果:RESVUSERGOOUTRES*/
		public const uint MSREQ_RESVUSER_GOOUT 	= 0x248;


	}
/*结束命令*/
	/*结束接口--Console*/
	/*开始接口--Report--报表查询统计*/
/*开始命令*/
	public partial class PRReport
	{

		/*获取预约记录,	参数:RESVRECREQ,	结果:UNIRESVREC(Array)*/
		public const uint MSREQ_REPORT_RESVREC_GET 	= 0x1;

		/*预约类型统计,	参数:RESVKINDSTATREQ,	结果:RESVKINDSTAT(Array)*/
		public const uint MSREQ_REPORT_RESVREC_KINDSTAT 	= 0x2;

		/*预约方式统计,	参数:RESVMODESTATREQ,	结果:RESVMODESTAT(Array)*/
		public const uint MSREQ_RESVMODESTAT_GET 	= 0x3;

		/*获取使用记录明细,	参数:USERECREQ,	结果:DEVUSEREC(Array)*/
		public const uint MSREQ_REPORT_USEREC_GET 	= 0x10;

		/*获取门禁刷卡记录明细,	参数:DOORCARDRECREQ,	结果:DOORCARDREC(Array)*/
		public const uint MSREQ_REPORT_DOORCARDREC_GET 	= 0x11;

		/*使用者统计报表数据,	参数:REPORTREQ,	结果:USERSTAT(Array)*/
		public const uint MSREQ_REPORT_USEREC_USERSTAT 	= 0x20;

		/*实验室统计报表数据,	参数:REPORTREQ,	结果:LABSTAT(Array)*/
		public const uint MSREQ_REPORT_USEREC_LABSTAT 	= 0x21;

		/*设备类型统计报表数据,	参数:REPORTREQ,	结果:DEVKINDSTAT(Array)*/
		public const uint MSREQ_REPORT_USEREC_DEVKINDSTAT 	= 0x22;

		/*设备统计报表数据(CUniStruct[REQEXTINFO],szExtInfo返回DEVSTAT总合计),	参数:REPORTREQ,	结果:DEVSTAT(Array)*/
		public const uint MSREQ_REPORT_USEREC_DEVSTAT 	= 0x23;

		/*实验项目表,	参数:TESTITEMSTATREQ,	结果:TESTITEMSTAT(Array)*/
		public const uint MSREQ_REPORT_USEREC_TESTITEMSTAT 	= 0x24;

		/*设备类型统计报表数据,	参数:REPORTREQ,	结果:DEVCLASSSTAT(Array)*/
		public const uint MSREQ_REPORT_USEREC_DEVCLASSSTAT 	= 0x25;

		/*学院使用统计,	参数:REPORTREQ,	结果:DEPTSTAT(Array)*/
		public const uint MSREQ_REPORT_USEREC_DEPTSTAT 	= 0x26;

		/*身份使用统计,	参数:IDENTSTATREQ,	结果:IDENTSTAT(Array)*/
		public const uint MSREQ_REPORT_USEREC_IDENTSTAT 	= 0x27;

		/*获取教学预约记录,	参数:TEACHINGRESVRECREQ,	结果:TEACHINGRESVREC(Array)*/
		public const uint MSREQ_REPORT_TEACHINGRESVREC 	= 0x28;

		/*实验室(房间)统计报表数据,	参数:REPORTREQ,	结果:ROOMSTAT(Array)*/
		public const uint MSREQ_REPORT_ROOMSTAT 	= 0x29;

		/*获取设备使用率查询,	参数:DEVUSINGRATEREQ,	结果:DEVUSINGRATE*/
		public const uint MSREQ_REPORT_DEVUSINGRATE 	= 0x30;

		/*获取设备月使用统计,	参数:DEVMONTHSTATREQ,	结果:DEVMONTHSTAT(Array)*/
		public const uint MSREQ_REPORT_DEVMONTHSTAT 	= 0x31;

		/*获取设备科研实验统计,	参数:RTUSESTATREQ,	结果:RTUSESTAT(Array)*/
		public const uint MSREQ_REPORT_RTUSESTAT 	= 0x32;

		/*获取设备科研实验明细,	参数:RTUSEDETAILREQ,	结果:RTUSEDETAIL(Array)*/
		public const uint MSREQ_REPORT_RTUSEDETAIL 	= 0x33;

		/*获取设备科研实验费用分配统计,	参数:RTFASTATREQ,	结果:RTFASTAT(Array)*/
		public const uint MSREQ_REPORT_RTFASTAT 	= 0x34;

		/*获取设备科研实验费用分配明细,	参数:RTFADETAILREQ,	结果:RTFADETAIL(Array)*/
		public const uint MSREQ_REPORT_RTFADETAIL 	= 0x35;

		/*违约统计,	参数:DEFAULTSTATREQ,	结果:DEFAULTSTAT(Array)*/
		public const uint MSREQ_REPORT_DEFAULTSTAT 	= 0x36;

		/*获取设备周使用率查询,	参数:DEVWEEKUSINGRATEREQ,	结果:DEVWEEKUSINGRATE*/
		public const uint MSREQ_REPORT_DEVWEEKUSINGRATE 	= 0x37;

		/*场馆活动类型统计,	参数:YARDACTIVITYSTATREQ,	结果:YARDACTIVITYSTAT(Array)*/
		public const uint MSREQ_REPORT_YARDACTIVITYSTAT 	= 0x38;

		/*教学科研仪器设备表 (SJ1),	参数:DEVLISTREQ,	结果:DEVLIST(Array)*/
		public const uint MSREQ_REPORT_DEVLIST 	= 0x101;

		/*教学科研仪器设备增减变动情况表(SJ2),	参数:DEVCHGREQ,	结果:DEVCHG*/
		public const uint MSREQ_REPORT_DEVCHG 	= 0x102;

		/*贵重仪器设备表(SJ3),	参数:BIGDEVREQ,	结果:BIGDEV(Array)*/
		public const uint MSREQ_REPORT_BIGDEV 	= 0x103;

		/*实验项目表(SJ4),	参数:TESTITEMREPORTREQ,	结果:TESTITEMREPORT(Array)*/
		public const uint MSREQ_REPORT_TESTITEMREPORT 	= 0x104;

		/*专任实验室人员表(SJ5),	参数:STAFFINFOREQ,	结果:STAFFINFO(Array)*/
		public const uint MSREQ_REPORT_STAFFINFO 	= 0x105;

		/*实验室基本情况表(SJ6),	参数:LABINFOREQ,	结果:LABINFO(Array)*/
		public const uint MSREQ_REPORT_LABINFO 	= 0x106;

		/*实验室经费情况表(SJ7),	参数:LABALLCOSTREQ,	结果:LABALLCOST*/
		public const uint MSREQ_REPORT_LABALLCOST 	= 0x107;

		/*高等学校实验室综合信息表(SZ1),	参数:LABSUMMARYREQ,	结果:LABSUMMARY*/
		public const uint MSREQ_REPORT_LABSUMMARY 	= 0x108;

		/*高等学校实验室综合信息表2(SZ2),	参数:LABSUMMARY2REQ,	结果:LABSUMMARY2*/
		public const uint MSREQ_REPORT_LABSUMMARY2 	= 0x109;

		/*教学科研仪器设备表 (SJ1),	参数:DEVLIST,	结果:null*/
		public const uint MSREQ_REPORT_DEVLIST_SET 	= 0x131;

		/*教学科研仪器设备增减变动情况表(SJ2),	参数:DEVCHG,	结果:null*/
		public const uint MSREQ_REPORT_DEVCHG_SET 	= 0x132;

		/*贵重仪器设备表(SJ3),	参数:BIGDEV,	结果:null*/
		public const uint MSREQ_REPORT_BIGDEV_SET 	= 0x133;

		/*实验项目表(SJ4),	参数:TESTITEMREPORT,	结果:null*/
		public const uint MSREQ_REPORT_TESTITEMREPORT_SET 	= 0x134;

		/*专任实验室人员表(SJ5),	参数:STAFFINFO,	结果:null*/
		public const uint MSREQ_REPORT_STAFFINFO_SET 	= 0x135;

		/*实验室基本情况表(SJ6),	参数:LABINFO,	结果:null*/
		public const uint MSREQ_REPORT_LABINFO_SET 	= 0x136;

		/*实验室经费情况表(SJ7),	参数:LABALLCOST,	结果:null*/
		public const uint MSREQ_REPORT_LABALLCOST_SET 	= 0x137;

		/*高等学校实验室综合信息表(SZ1),	参数:LABSUMMARY,	结果:null*/
		public const uint MSREQ_REPORT_LABSUMMARY_SET 	= 0x138;

		/*高等学校实验室综合信息表2(SZ2),	参数:LABSUMMARY2,	结果:null*/
		public const uint MSREQ_REPORT_LABSUMMARY2_SET 	= 0x139;


	}
/*结束命令*/
	/*结束接口--Report*/
	/*开始接口--System--系统管理*/
/*开始命令*/
	public partial class PRSystem
	{

		/*获取系统配置文件,	参数:CFGREQ,	结果:CFGINFO(Array)*/
		public const uint MSREQ_SYSTEM_CFGGET 	= 0x10;

		/*修改并保存系统配置文件,	参数:CFGINFO,	结果:null*/
		public const uint MSREQ_SYSTEM_CFGSET 	= 0x15;

		/*获取独立的信用类别列表,	参数:CREDITTYPEREQ,	结果:CREDITTYPE(Array)*/
		public const uint MSREQ_CREDITTYPE_GET 	= 0x20;

		/*设置独立的信用类别,	参数:CREDITTYPE,	结果:null*/
		public const uint MSREQ_CREDITTYPE_SET 	= 0x25;

		/*获取信用分数规则列表,	参数:CREDITSCOREREQ,	结果:CREDITSCORE(Array)*/
		public const uint MSREQ_CREDITSCORE_GET 	= 0x40;

		/*设置信用分数规则,	参数:CREDITSCORE,	结果:CREDITSCORE*/
		public const uint MSREQ_CREDITSCORE_SET 	= 0x45;

		/*获取我的信用积分,	参数:MYCREDITSCOREREQ,	结果:MYCREDITSCORE(Array)*/
		public const uint MSREQ_MYCREDITSCORE_GET 	= 0x48;

		/*人工信用管理,	参数:ADMINCREDIT,	结果:null*/
		public const uint MSREQ_ADMINCREDIT_DO 	= 0x50;

		/*获取信用记录,	参数:CREDITRECREQ,	结果:CREDITREC(Array)*/
		public const uint MSREQ_CREDITREC_GET 	= 0x55;

		/*获取系统功能列表,	参数:SYSFUNCREQ,	结果:SYSFUNC(Array)*/
		public const uint MSREQ_SYSFUNC_GET 	= 0x60;

		/*获取系统功能使用规则列表,	参数:SYSFUNCRULEREQ,	结果:SYSFUNCRULE(Array)*/
		public const uint MSREQ_SYSFUNCRULE_GET 	= 0x65;

		/*修改系统功能使用规则,	参数:SYSFUNCRULE,	结果:SYSFUNCRULE*/
		public const uint MSREQ_SYSFUNCRULE_SET 	= 0x66;

		/*获取系统功能资格表,	参数:SFROLEINFOREQ,	结果:SFROLEINFO(Array)*/
		public const uint MSREQ_SFROLE_GET 	= 0x70;

		/*系统功能资格申请,	参数:SFROLEINFO,	结果:null*/
		public const uint MSREQ_SFROLE_APPLY 	= 0x75;

		/*系统功能资格审核,	参数:SFROLEINFO,	结果:null*/
		public const uint MSREQ_SFROLE_CHECK 	= 0x76;

		/*获取编码表,	参数:CODINGTABLEREQ,	结果:CODINGTABLE(Array)*/
		public const uint MSREQ_CODINGTABLE_GET 	= 0x100;

		/*设置编码表,	参数:CODINGTABLE,	结果:null*/
		public const uint MSREQ_CODINGTABLE_SET 	= 0x101;

		/*删除编码表,	参数:CODINGTABLE,	结果:null*/
		public const uint MSREQ_CODINGTABLE_DEL 	= 0x102;

		/*获取多语言包,	参数:MULTILANLIBREQ,	结果:UNIMULTILANLIB(Array)*/
		public const uint MSREQ_MULTILANLIB_GET 	= 0x201;

		/*更新系统状态,	参数:SYSREFRESHREQ,	结果:null*/
		public const uint MSREQ_SYSTEM_REFRESH 	= 0x230;


	}
/*结束命令*/
	/*结束接口--System*/
	/*开始接口--Assert--资产管理*/
/*开始命令*/
	public partial class PRAssert
	{

		/*获取资产列表,	参数:ASSERTREQ,	结果:UNIASSERT(Array)*/
		public const uint MSREQ_ASSERT_GET 	= 0x10;

		/*资产入库,	参数:UNIASSERT,	结果:UNIASSERT*/
		public const uint MSREQ_ASSERT_WAREHOUSING 	= 0x20;

		/*RFID发卡,	参数:RFIDBIND,	结果:null*/
		public const uint MSREQ_ASSERT_RFIDBIND 	= 0x21;

		/*房间变更,	参数:ROOMCHG,	结果:null*/
		public const uint MSREQ_ASSERT_CHGROOM 	= 0x22;

		/*责任人变更,	参数:KEEPERCHG,	结果:null*/
		public const uint MSREQ_ASSERT_CHGKEEPER 	= 0x23;

		/*删除资产,	参数:UNIASSERT,	结果:null*/
		public const uint MSREQ_ASSERT_DEL 	= 0x30;

		/*获取资产盘点表,	参数:STOCKTAKINGREQ,	结果:STOCKTAKING(Array)*/
		public const uint MSREQ_STOCKTAKING_GET 	= 0x110;

		/*资产盘点,	参数:STOCKTAKING,	结果:STOCKTAKING*/
		public const uint MSREQ_STOCKTAKING_DO 	= 0x120;

		/*获取盘点资产明细表,	参数:STDETAILREQ,	结果:STDETAIL(Array)*/
		public const uint MSREQ_STDETAIL_GET 	= 0x130;

		/*具体资产盘点,	参数:STDETAIL,	结果:null*/
		public const uint MSREQ_STDETAIL_DO 	= 0x135;

		/*获取设备报废记录表,	参数:OUTOFSERVICEREQ,	结果:OUTOFSERVICE(Array)*/
		public const uint MSREQ_OUTOFSERVICE_GET 	= 0x140;

		/*申请设备报废,	参数:OUTOFSERVICE,	结果:OUTOFSERVICE*/
		public const uint MSREQ_OUTOFSERVICE_APPLY 	= 0x141;

		/*批准设备报废,	参数:OUTOFSERVICE,	结果:OUTOFSERVICE*/
		public const uint MSREQ_OUTOFSERVICE_APPROVE 	= 0x142;

		/*撤销设备报废申请,	参数:OUTOFSERVICE,	结果:null*/
		public const uint MSREQ_OUTOFSERVICE_DEL 	= 0x143;

		/*获取报废设备明细表,	参数:OOSDETAILREQ,	结果:OOSDETAIL(Array)*/
		public const uint MSREQ_OOSDETAIL_GET 	= 0x145;

		/*获取设备修理记录,	参数:DEVDAMAGERECREQ,	结果:DEVDAMAGEREC(Array)*/
		public const uint MSREQ_REPAIRREC_GET 	= 0x160;

		/*设备报修,	参数:REPAIRAPPLY,	结果:REPAIRAPPLY*/
		public const uint MSREQ_REPAIR_APPLY 	= 0x161;

		/*设备修理结束,	参数:REPAIROVER,	结果:REPAIROVER*/
		public const uint MSREQ_REPAIR_OVER 	= 0x162;

		/*撤销设备报修,	参数:REPAIRCANCEL,	结果:null*/
		public const uint MSREQ_REPAIR_CANCEL 	= 0x163;

		/*获取供应商(生产、供货、维保),	参数:COMPANYREQ,	结果:UNICOMPANY(Array)*/
		public const uint MSREQ_COMPANY_GET 	= 0x310;

		/*新建修改供应商,	参数:UNICOMPANY,	结果:UNICOMPANY*/
		public const uint MSREQ_COMPANY_SET 	= 0x320;

		/*删除供应商,	参数:UNICOMPANY,	结果:null*/
		public const uint MSREQ_COMPANY_DEL 	= 0x330;

		/*获取设备历史档案,	参数:ASSERTLOGREQ,	结果:ASSERTLOG(Array)*/
		public const uint MSREQ_ASSERTLOG_GET 	= 0x410;


	}
/*结束命令*/
	/*结束接口--Assert*/
	/*开始接口--Attendance--考勤管理*/
/*开始命令*/
	public partial class PRAttendance
	{

		/*获取考勤规则,	参数:ATTENDRULEREQ,	结果:ATTENDRULE(Array)*/
		public const uint MSREQ_ATTENDRULE_GET 	= 0x10;

		/*修改考勤规则,	参数:ATTENDRULE,	结果:ATTENDRULE*/
		public const uint MSREQ_ATTENDRULE_SET 	= 0x20;

		/*删除考勤规则,	参数:ATTENDRULE,	结果:null*/
		public const uint MSREQ_ATTENDRULE_DEL 	= 0x30;

		/*获取考勤记录,	参数:ATTENDRECREQ,	结果:ATTENDREC(Array)*/
		public const uint MSREQ_ATTENDREC_GET 	= 0x100;

		/*获取考勤统计,	参数:ATTENDSTATREQ,	结果:ATTENDSTAT(Array)*/
		public const uint MSREQ_ATTENDSTAT_GET 	= 0x120;


	}
/*结束命令*/
	/*结束接口--Attendance*/
	/*开始接口--SubSys--子系统通信接口*/
/*开始命令*/
	public partial class PRSubSys
	{

		/*子系统登录,	参数:SUBSYSLOGINREQ,	结果:SUBSYSLOGINRES*/
		public const uint MSREQ_SUBSYS_LOGIN 	= 0x10;

		/*子系统注销,	参数:SUBSYSLOGOUTREQ,	结果:null*/
		public const uint MSREQ_SUBSYS_LOGOUT 	= 0x11;

		/*上传IC空间使用记录,	参数:ICUSERECUP,	结果:null*/
		public const uint MSREQ_ICUSEREC_UPLOAD 	= 0x200;

		/*上传打印复印扫描使用记录,	参数:PRINTRECUP,	结果:null*/
		public const uint MSREQ_PRINTREC_UPLOAD 	= 0x300;

		/*上传图书超期缴费记录,	参数:BOOKOVERDUERECUP,	结果:null*/
		public const uint MSREQ_BOOKOVERDUEREC_UPLOAD 	= 0x400;

		/*上传违约记录,	参数:BREACHRECUP,	结果:null*/
		public const uint MSREQ_BREACHREC_UPLOAD 	= 0x500;


	}
/*结束命令*/
	/*结束接口--SubSys*/
	/*开始接口--SubIC--IC空间子系统接口*/
/*开始命令*/
	public partial class PRSubIC
	{

		/*获取研修间当前状态,	参数:STUDYROOMSTATREQ,	结果:STUDYROOMSTAT(Array)*/
		public const uint MSREQ_STUDYROOMSTAT_GET 	= 0x500;

		/*获取座位当前状态,	参数:SEATSTATREQ,	结果:SEATSTAT(Array)*/
		public const uint MSREQ_SEATSTAT_GET 	= 0x510;


	}
/*结束命令*/
	/*结束接口--SubIC*/
	/*开始接口--SubRT--科研实验子系统接口*/
/*开始命令*/
	public partial class PRSubRT
	{

		/*获取科研实验数据,	参数:RTDATAREQ,	结果:RTDATA(Array)*/
		public const uint MSREQ_RTDATA_GET 	= 0x10;


	}
/*结束命令*/
	/*结束接口--SubRT*/
	/*开始接口--UniSta--节点管理*/
/*开始命令*/
	public partial class PRUniSta
	{

		/*节点登录,	参数:STALOGINREQ,	结果:STALOGINRES*/
		public const uint MSREQ_UNISTA_LOGIN 	= 0x10;

		/*节点退出,	参数:STALOGOUTREQ,	结果:null*/
		public const uint MSREQ_UNISTA_LOGOUT 	= 0x20;

		/*节点与认证服务器定时通信,	参数:HANDSHAKEREQ,	结果:HANDSHAKERES*/
		public const uint MSREQ_UNISTA_HANDSHAKE 	= 0x30;

		/*上传模块监控信息,	参数:MODMONIUP,	结果:null*/
		public const uint MSREQ_MODMONI_UPLOAD 	= 0x200;

		/*上传监控指标信息,	参数:MONINDEXUP,	结果:null*/
		public const uint MSREQ_MONIINDEX_UPLOAD 	= 0x201;

		/*上传监控指标记录,	参数:MONIRECUP,	结果:null*/
		public const uint MSREQ_MONIREC_UPLOAD 	= 0x202;


	}
/*结束命令*/
	/*结束接口--UniSta*/
	/*开始接口--UniMoni--自动监控*/
/*开始命令*/
	public partial class PRUniMoni
	{

		/*子系统获取监控信息缺省值,	参数:,	结果:MONINDEX(Array)*/
		public const uint MSREQ_SUBMONI_FROMSRV 	= 0x10;

		/*子系统保存监控信息缺省值到服务器,	参数:MONINDEX,	结果:null*/
		public const uint MSREQ_SUBMONI_TOSRV 	= 0x11;

		/*监控信息状态改变,	参数:MONINDEX,	结果:null*/
		public const uint MSREQ_SUBMONI_ONEINDEXCHG 	= 0x20;

		/*监控信息状态改变,	参数:MONINDEX,	结果:null*/
		public const uint MSREQ_SUBMONI_INDEXCHG 	= 0x21;

		/*管理员获取详细监控信息,	参数:MONIREQ,	结果:MODMONI(Array)*/
		public const uint MSREQ_MONI_GET 	= 0x101;

		/*管理员获取详细监控信息日志,	参数:MONIRECREQ,	结果:MONIREC(Array)*/
		public const uint MSREQ_MONIREC_GET 	= 0x111;

		/*技术支持处理错误,	参数:MONIDEALERR,	结果:null*/
		public const uint MSREQ_MONI_DEALERR 	= 0x211;


	}
/*结束命令*/
	/*结束接口--UniMoni*/

}
//