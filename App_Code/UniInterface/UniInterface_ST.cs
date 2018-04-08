
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
using System.Runtime.InteropServices;


namespace UniWebLib
{

	/*开始数据结构*/
public partial class Struct_ST
{

	/*版本信息*/
	static public string[] UNIVERSION = new string[]{
		
		"szVersion",		/*版本	XX.XX.XXXXXXXX*/
	
		"dwWarrant",		/*与一卡通对接模式*/
		
	"szLicInfo",		/*授权信息(UNILICENSE结构)*/
	 ""};

}
	/*结束数据结构*/

	/*开始数据结构*/
public partial class Struct_ST
{

	/*管理员登录请求*/
	static public string[] ADMINLOGINREQ = new string[]{
		
		"dwStaSN",		/*站点编号*/
		
      "dwLoginRole",		/*登录模式*/
		
		"szVersion",		/*版本	XX.XX.XXXXXXXX,最新版本定义如下*/
	
		"szLogonName",		/*登录名*/
	
		"szPassword",		/*密码*/
	
		"szIP",		/*IP地址*/
	
		"szMSN",		/*微信号*/
	 ""};

	/*管理员登录应答*/
	static public string[] ADMINLOGINRES = new string[]{
		
		"dwSessionID",		/*服务器分配的SessionID*/
		
	"SrvVer",		/*UNIVERSION 结构*/
	
      "dwSupportSubSys",		/*授权子系统*/
		
      "dwManRole",		/*管理员角色*/
		
      "dwUserStat",		/*用户状态*/
		
	"AdminInfo",		/*UNIADMIN 结构*/
	
	"UserRole",		/*USERROLE表*/
	
	"StaInfo",		/*CUniTable[UNISTATION]*/
	
	"AccInfo",		/*用户信息(UNIACCOUNT结构)*/
	
	"TutorInfo",		/*用户信息(UNITUTOR结构)*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*手机登录请求*/
	static public string[] MOBILELOGINREQ = new string[]{
		
		"dwStaSN",		/*站点编号*/
		
		"dwLoginRole",		/*登录模式*/
		
		"szVersion",		/*版本	XX.XX.XXXXXXXX*/
	
		"szLogonName",		/*登录名*/
	
		"szPassword",		/*密码*/
	
		"szIP",		/*IP地址*/
	
		"szMSN",		/*微信号*/
	
      "dwProperty",		/*扩展属性*/
		 ""};

	/*控制台退出请求*/
	static public string[] ADMINLOGOUTREQ = new string[]{
		
		"dwAccNo",		/*账号*/
		
		"szLogonName",		/*登录名*/
	
		"szIP",		/*IP地址*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*控制台退出响应*/
	static public string[] ADMINLOGOUTRES = new string[]{
		
		"szMemo",		/*说明信息*/
	 ""};

	/*手机摇一摇登录请求*/
	static public string[] SHAKELOGINREQ = new string[]{
		
		"dwStaSN",		/*站点编号*/
		
		"szVersion",		/*版本	XX.XX.XXXXXXXX*/
	
		"szLogonName",		/*登录名*/
	
		"szPassword",		/*密码*/
	
		"szIP",		/*IP地址*/
	
		"szOpenId",		/*摇一摇微信号OpenID*/
	
      "dwProperty",		/*扩展属性*/
		 ""};

	/*摇一摇登录应答*/
	static public string[] SHAKELOGINRES = new string[]{
		
		"dwSessionID",		/*服务器分配的SessionID*/
		
	"SrvVer",		/*UNIVERSION 结构*/
	
		"szPID",		/*学工号*/
	
		"szTrueName",		/*姓名*/
	
		"szDispInfo",		/*显示信息*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*获取系统支持的UID*/
	static public string[] UIDINFOREQ = new string[]{
		
		"dwUidSN",		/*UID编号*/
		
		"dwFuncSN",		/*所属功能模块编号*/
		
		"dwUIDType",		/*请求类型*/
		
		"szUIDName",		/*UID名称*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*系统支持的UID*/
	static public string[] UIDINFO = new string[]{
		
		"dwUidSN",		/*UID编号*/
		
		"dwFuncSN",		/*所属功能模块编号*/
		
		"szFuncName",		/*所属功能模块名称*/
	
      "dwUIDType",		/*请求类型*/
		
		"szUIDName",		/*UID名称*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*字段限制*/
	static public string[] FIELDLIMIT = new string[]{
		
		"szFieldName",		/*字段名称*/
	
		"szMask",		/*对该字段处理方法*/
	 ""};

	/*UID权限明细*/
	static public string[] PRIVUID = new string[]{
		
		"dwPrivID",		/*权限ID*/
		
		"dwUidSN",		/*UID编号*/
		
		"dwUIDType",		/*请求类型*/
		
		"dwFuncSN",		/*所属功能模块编号*/
		
		"szFuncName",		/*所属功能模块名称*/
	
		"szUIDName",		/*UID名称*/
	
      "dwWarrantType",		/*许可方式*/
		
	"FieldLimit",		/*对应UID的各字段限制规则*/
	 ""};

	/*获取操作权限请求*/
	static public string[] OPPRIVREQ = new string[]{
		
		"dwOPID",		/*操作权限ID*/
		
		"szOPName",		/*操作权限名称（模糊匹配）*/
	
		"dwFuncSN",		/*所属功能模块编号*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*操作权限*/
	static public string[] OPPRIV = new string[]{
		
		"dwOPID",		/*操作权限ID*/
		
		"szOPName",		/*操作权限名称*/
	
		"dwDefWarType",		/*缺省许可方式（定义见PRIVUID）*/
		
		"dwSysFuncMask",		/*支持系统功能模块*/
		
		"dwFuncSN",		/*所属功能模块编号*/
		
		"szFuncName",		/*所属功能模块名称*/
	
	"PrivUID",		/*各UID权限明细表(CUniTable<PRIVUID>)*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*获取用户角色*/
	static public string[] USERROLEREQ = new string[]{
		
		"dwRoleID",		/*角色ID*/
		
		"szRoleName",		/*角色名称（模糊匹配）*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*用户角色*/
	static public string[] USERROLE = new string[]{
		
		"dwRoleID",		/*角色ID*/
		
		"szRoleName",		/*角色名称*/
	
		"dwDefWarType",		/*缺省许可方式（定义见PRIVUID）*/
		
		"dwSysFuncMask",		/*支持系统功能模块*/
		
	"OpPriv",		/*操作权限表(CUniTable<OPPRIV>)*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*客户端卸载密码结构*/
	static public string[] CLTPASSWD = new string[]{
		
		"dwPassWdCode",		/*密码CODE*/
		
		"szPassword",		/*密码*/
	
		"dwSetDate",		/*设置日期*/
		
		"szOperator",		/*设置管理员*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/**/
	static public string[] ADMINREQ = new string[]{
		
		"dwStaSN",		/*节点编号*/
		
		"dwAccNo",		/*帐号*/
		
		"szLogonName",		/*登录名*/
	
		"szTrueName",		/*姓名*/
	
		"dwDeptID",		/*部门ID*/
		
		"szDeptName",		/*部门*/
	
		"dwManRole",		/*管理员角色*/
		
		"dwIdent",		/*管理员身份*/
		
		"dwProperty",		/*管理员属性（以下定义+审核类型）*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*管理员信息*/
	static public string[] UNIADMIN = new string[]{
		
		"dwStaSN",		/*节点编号*/
		
		"dwAccNo",		/*帐号*/
		
		"dwManRole",		/*管理员角色*/
		
		"dwStatus",		/*状态*/
		
      "dwProperty",		/*管理员属性（以下定义+审核类型）*/
		
		"dwExpDate",		/*到期日*/
		
		"szLogonName",		/*登录名*/
	
		"szTrueName",		/*姓名*/
	
		"dwIdent",		/*管理员身份*/
		
      "dwManLevel",		/*管理员级别（比如学院级，校级等)*/
		
		"dwDeptID",		/*部门ID*/
		
		"szDeptName",		/*部门*/
	
		"szTel",		/*电话*/
	
		"szHandPhone",		/*手机*/
	
		"szEmail",		/*电邮*/
	
	"UserRole",		/*角色表(CUniTable<USERROLE>)*/
	
		"szMemo",		/*备注*/
	 ""};

	/*获取管理房间请求*/
	static public string[] MANROOMREQ = new string[]{
		
		"dwAccNo",		/*帐号*/
		
		"dwManFlag",		/*管理标志(空表示获取全部，0获取未管理房间，1获取管理房间)*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*管理员管理房间*/
	static public string[] MANROOM = new string[]{
		
		"dwRoomID",		/*房间ID*/
		
		"szRoomNo",		/*房间编号*/
	
		"szRoomName",		/*房间名称*/
	
		"dwManGroupID",		/*管理员组ID*/
		
		"dwManFlag",		/*管理标志(0没有权限，1有管理权限)*/
		 ""};

	/**/
	static public string[] ADMINCHECK = new string[]{
		
		"dwSubjectID",		/*确认事由ID*/
		
      "dwSubjectType",		/*确认事由类别*/
		
      "dwCheckStat",		/*管理员审核状态(扩展由各类别审查确认时定义)*/
		
		"dwApplicantID",		/*申请人账号*/
		
		"szApplicantName",		/*申请人姓名*/
	
		"szCheckDetail",		/*审查说明*/
	
		"szMemo",		/*备注*/
	
		"szSubjectInfo",		/*对应的确认事由结构详细信息*/
	 ""};

	/*设备使用审核信息*/
	static public string[] USEDDEVCHECKINFO = new string[]{
		
		"dwSID",		/*流水号*/
		
		"dwAccNo",		/*使用人*/
		
		"szTrueName",		/*使用者姓名*/
	
		"dwLabID",		/*实验室ID*/
		
		"dwDevID",		/*设备ID*/
		
		"dwCheckStat",		/*设备状态*/
		
		"dwCompensation",		/*赔偿金额*/
		
		"dwPunishScore",		/*信用扣分*/
		
		"szDamageInfo",		/*损坏说明*/
	
		"szExtInfo",		/*设备新描述*/
	 ""};

	/*获取审核信息*/
	static public string[] CHECKREQ = new string[]{
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
		"dwCheckStat",		/*状态*/
		
		"dwSubjectID",		/*事由ID*/
		
		"dwSubjectType",		/*事由类别*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*审核信息*/
	static public string[] CHECKINFO = new string[]{
		
		"dwSubjectID",		/*确认事由ID*/
		
		"dwSubjectType",		/*确认事由类别*/
		
		"dwCheckStat",		/*管理员审核状态(扩展由各类别审查确认时定义)*/
		
		"dwOccurDate",		/*开始日期*/
		
		"dwOccurTime",		/*产生时间*/
		
		"dwApplicantID",		/*申请人账号*/
		
		"szApplicantName",		/*申请人姓名*/
	
		"szMemo",		/*备注*/
	 ""};

	/*获取审核信息日志*/
	static public string[] CHECKLOGREQ = new string[]{
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
		"dwCheckStat",		/*状态*/
		
		"dwSubjectID",		/*事由ID*/
		
		"dwSubjectType",		/*事由类别*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*审核信息日志*/
	static public string[] CHECKLOG = new string[]{
		
		"dwSID",		/*流水号*/
		
		"dwCheckStat",		/*管理员审核状态(扩展由各类别审查确认时定义)*/
		
		"dwSubjectID",		/*确认事由ID*/
		
		"dwSubjectType",		/*确认事由类别*/
		
		"dwOccurDate",		/*开始日期*/
		
		"dwOccurTime",		/*审核时间*/
		
		"dwAdminID",		/*审核者帐号*/
		
		"szAdminName",		/*审核者*/
	
		"dwApplicantID",		/*申请人账号*/
		
		"szApplicantName",		/*申请人姓名*/
	
		"szCheckDetail",		/*审查说明*/
	
		"szMemo",		/*备注*/
	 ""};

	/*获取刷新标志请求*/
	static public string[] REFRESHFLAGREQ = new string[]{
		
      "dwRefreshType",		/*刷新类别*/
		 ""};

	/*获取刷新标志请求*/
	static public string[] REFRESHFLAGINFO = new string[]{
		
		"dwRefreshType",		/*刷新类别*/
		
		"dwRefreshFlag",		/*刷新标志*/
		 ""};

	/*获取节假日*/
	static public string[] HOLIDAYREQ = new string[]{
		
		"szName",		/*名称（模糊匹配）*/
	
		"dwDate",		/*日期*/
		 ""};

	/*节假日信息*/
	static public string[] UNIHOLIDAY = new string[]{
		
		"szName",		/*名称（模糊匹配）*/
	
		"dwStartDay",		/*开始日期(MMDD或YYYYMMDD)*/
		
		"dwEndDay",		/*结束日期(MMDD或YYYYMMDD)*/
		
		"szMemo",		/*开放说明*/
	 ""};

	/*检测某个值是否存在请求*/
	static public string[] CHECKEXISTREQ = new string[]{
		
		"dwUID",		/*请求新建修改的ID，比如MSREQ_ADMIN_SET*/
		
		"szName",		/*判断的字段名称(和相对应的结构里的名称相同,比如szLogonName*/
	
		"szValue",		/*需要检查的值，是数字的转换成字符串*/
	
		"szCon",		/*SQL语句条件值，可为空*/
	 ""};

	/*获取某个字段的最大值请求*/
	static public string[] MAXVALUEREQ = new string[]{
		
		"dwUID",		/*请求新建修改的ID，比如MSREQ_ADMIN_SET*/
		
		"szName",		/*判断的字段名称(和相对应的结构里的名称相同,比如szLogonName*/
	
		"szCon",		/*SQL语句条件值，可为空*/
	 ""};

	/*返回最大值*/
	static public string[] MAXVALUE = new string[]{
		
		"dwValue",		/*返回的最大值*/
		 ""};

	/*工作管理人员界面参数信息请求*/
	static public string[] IFPARAMREQ = new string[]{
		
		"dwAdminID",		/*管理员ID*/
		 ""};

	/*工作管理人员界面参数信息*/
	static public string[] IFPARAM = new string[]{
		
		"dwAdminID",		/*管理员ID*/
		
		"szParam",		/*参数*/
	
		"szMemo",		/*备注*/
	 ""};

	/*获取管理员操作日志请求包*/
	static public string[] ADMINLOGREQ = new string[]{
		
		"dwStaSN",		/*节点编号*/
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
		"dwAdminID",		/*管理员ID*/
		
		"szTrueName",		/*真实姓名（模糊匹配）*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*管理员操作日志*/
	static public string[] ADMINLOG = new string[]{
		
		"dwStaSN",		/*节点编号*/
		
		"dwAdminID",		/*管理员ID*/
		
		"szTrueName",		/*真实姓名*/
	
		"dwOpDate",		/*操作日期*/
		
		"dwOpTime",		/*操作时间*/
		
		"dwOpUID",		/*操作接口ID*/
		
		"szOpInfo",		/*操作内容*/
	
		"szOpDetail",		/*操作详细说明*/
	 ""};

	/*IP地址黑名单*/
	static public string[] IPBLACKLIST = new string[]{
		
		"szIP",		/*ip地址*/
	
		"szTryAdmin",		/*重试账号*/
	
		"dwTryTimes",		/*重试次数*/
		
		"dwLockEndTime",		/*锁定结束时间*/
		 ""};

	/*管理员修改密码*/
	static public string[] ADMINCHGPASSWD = new string[]{
		
		"szCurAdminPw",		/*当前登录的管理员密码*/
	
		"dwAdminID",		/*管理员帐号*/
		
		"szNewPw",		/*新密码*/
	
		"szMemo",		/*备注（扩展用）*/
	 ""};

	/*系统状态*/
	static public string[] BSYSINFO = new string[]{
		
		"dwStatus",		/*总状态*/
		
	"ParamStat",		/*监控指标状态*/
	
		"szStatInfo",		/*状态说明*/
	 ""};

	/*状态统计信息*/
	static public string[] STATUSINFO = new string[]{
		
		"dwStatus",		/*状态值*/
		
		"dwNum",		/*数量*/
		 ""};

	/*审核信息统计*/
	static public string[] BCHECKSTAT = new string[]{
		
		"dwStatus",		/*总状态*/
		
	"CheckStatInfo",		/*审核统计表*/
	
		"szStatInfo",		/*状态说明*/
	 ""};

	/*设备信息统计*/
	static public string[] BDEVSTAT = new string[]{
		
		"dwStatus",		/*总状态*/
		
	"DevStatInfo",		/*设备状态统计表*/
	
		"szStatInfo",		/*状态说明*/
	 ""};

	/*房间信息统计*/
	static public string[] BROOMSTAT = new string[]{
		
		"dwStatus",		/*总状态*/
		
	"RoomStatInfo",		/*房间状态统计表*/
	
		"szStatInfo",		/*状态说明*/
	 ""};

	/*今日课程统计*/
	static public string[] BTODAYRESVSTAT = new string[]{
		
		"dwStatus",		/*总状态*/
		
	"TodayResvStatInfo",		/*今日课程状态统计表*/
	
		"szStatInfo",		/*状态说明*/
	 ""};

	/*自由上机统计*/
	static public string[] BFREEUSESTAT = new string[]{
		
		"dwStatus",		/*总状态*/
		
		"dwCurUsers",		/*当前人数*/
		
	"FreeTodayUseStat",		/*今日自由上机统计(按小时)*/
	
		"szStatInfo",		/*状态说明*/
	 ""};

	/*服务器返回信息*/
	static public string[] BASICSTAT = new string[]{
		
		"dwChgNum",		/*信息发生改变的统计栏目数*/
		
      "dwStatus",		/*状态*/
		
	"SysStat",		/*系统状态*/
	
	"CheckStat",		/*审核信息统计*/
	
	"DevStat",		/*设备信息统计*/
	
	"RoomStat",		/*房间信息统计*/
	
	"TodayResvStat",		/*今日课程统计*/
	
	"FreeUseStat",		/*自由上机统计*/
	
		"szStatInfo",		/*状态说明*/
	 ""};

	/*审核类别请求*/
	static public string[] CHECKTYPEREQ = new string[]{
		
		"dwCheckKind",		/*审核类型(可多个)*/
		
		"dwMainKind",		/*审核大类*/
		 ""};

	/*审核类别*/
	static public string[] CHECKTYPE = new string[]{
		
		"dwCheckKind",		/*审核编号（新建时由系统自动分配）*/
		
      "dwMainKind",		/*审核大类*/
		
		"szCheckName",		/*审核名称*/
	
		"dwCheckLevel",		/*审核级别(同UNIADMIN.dwManLevel定义）*/
		
		"dwDeptID",		/*责任部门ID（学院级不设置，根据申请人自动匹配）*/
		
		"szDeptName",		/*责任部门*/
	
		"szMemo",		/*状态说明*/
	 ""};

	/*获取用户意见反馈请求*/
	static public string[] USERFEEDBACKREQ = new string[]{
		
		"dwFeedKind",		/*反馈类型*/
		
		"dwFeedStat",		/*状态*/
		
		"dwPurpose",		/*用途*/
		
		"dwResvID",		/*预约ID*/
		
		"dwSNum",		/*流水号*/
		
		"dwAccNo",		/*帐号*/
		
		"dwDevID",		/*使用设备*/
		
		"dwMinScore",		/*用户最低评分*/
		
		"dwMaxScore",		/*用户最高评分*/
		
		"dwBeginDate",		/*预约开始日期*/
		
		"dwEndDate",		/*预约结束日期*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*用户意见反馈*/
	static public string[] USERFEEDBACK = new string[]{
		
		"dwSNum",		/*流水号*/
		
		"dwAccNo",		/*帐号*/
		
		"szTrueName",		/*姓名*/
	
		"szTel",		/*电话*/
	
		"szHandPhone",		/*手机*/
	
		"szEmail",		/*电邮*/
	
		"dwUserDeptID",		/*部门ID*/
		
		"szUserDeptName",		/*部门*/
	
      "dwFeedKind",		/*反馈类型*/
		
      "dwFeedStat",		/*状态*/
		
		"dwPurpose",		/*用途*/
		
		"dwResvID",		/*预约ID*/
		
		"dwDevID",		/*使用设备*/
		
		"dwDevKind",		/*设备类型*/
		
		"dwDevSN",		/*设备编号*/
		
		"szDevName",		/*设备名称*/
	
		"dwRoomID",		/*房间ID*/
		
		"szRoomName",		/*房间名称*/
	
		"dwLabID",		/*实验室ID*/
		
		"szLabName",		/*实验室名称*/
	
		"dwScore",		/*用户评分*/
		
		"dwOccurDate",		/*发生日期*/
		
		"dwOccurTime",		/*发生时间*/
		
		"szIntroInfo",		/*申请信息*/
	
		"szReplyInfo",		/*回复信息*/
	
		"dwReplyDate",		/*回复日期*/
		
		"dwReplyTime",		/*回复时间*/
		
		"dwAnswererID",		/*回复帐号*/
		
		"szAnswerer",		/*回复者*/
	
		"szMemo",		/*状态说明*/
	 ""};

	/*服务类别请求*/
	static public string[] SERVICETYPEREQ = new string[]{
		
		"dwServiceKind",		/*审核类型(可多个)*/
		 ""};

	/*服务类别*/
	static public string[] UNISERVICETYPE = new string[]{
		
		"dwServiceKind",		/*服务编号（新建时由系统自动分配）*/
		
		"szServiceName",		/*服务名称*/
	
		"dwServiceLevel",		/*服务部门级别(同UNIADMIN.dwManLevel定义）*/
		
		"dwDeptID",		/*服务部门ID（学院级不设置，根据申请人自动匹配）*/
		
		"szDeptName",		/*服务部门*/
	
      "dwProperty",		/*审核属性*/
		
		"szMemo",		/*说明*/
	 ""};

	/*获取用户意见反馈请求*/
	static public string[] POLLONLINEREQ = new string[]{
		
		"dwPollID",		/*流水号*/
		
		"dwVoteStat",		/*投票状态*/
		
		"dwBeginDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*投票选项*/
	static public string[] POLLITEM = new string[]{
		
		"dwItemID",		/*选项号*/
		
		"szItemName",		/*选项名称*/
	
		"dwGroupID",		/*组ID*/
		
		"dwPollKind",		/*民调类型*/
		
		"dwMaxTickItems",		/*最大可勾选选项*/
		
		"dwVotes",		/*总票数*/
		
		"szMemo",		/*状态说明*/
	 ""};

	/*网上投票信息*/
	static public string[] POLLONLINE = new string[]{
		
		"dwPollID",		/*投票ID*/
		
		"dwAccNo",		/*发起人（负责人）帐号*/
		
		"szTrueName",		/*姓名*/
	
		"szTel",		/*电话*/
	
		"szHandPhone",		/*手机*/
	
		"szEmail",		/*电邮*/
	
      "dwVoteStat",		/*投票状态*/
		
      "dwPollScope",		/*民调范围*/
		
      "dwPollKind",		/*民调类型*/
		
		"dwMaxTickItems",		/*最大可勾选选项*/
		
		"szPollSubject",		/*投票主题*/
	
		"dwBeginDate",		/*开始日期*/
		
		"dwEndDate",		/*截止日期*/
		
		"dwTotalUsers",		/*投票总人数*/
		
	"PollItems",		/*CUniTable[POLLITEM]*/
	
		"szMemo",		/*状态说明*/
	 ""};

	/*投票*/
	static public string[] POLLVOTE = new string[]{
		
		"dwPollID",		/*投票编号*/
		
		"dwItemID",		/*选项号*/
		
		"szMemo",		/*状态说明*/
	 ""};

}
	/*结束数据结构*/

	/*开始数据结构*/
public partial class Struct_ST
{

	/*获取站点的请求包*/
	static public string[] STATIONREQ = new string[]{
		
      "dwGetType",		/*方式*/
		
		"szGetKey",		/*条件值*/
	 ""};

	/*设备软硬件配置信息*/
	static public string[] DEVICECONFIG = new string[]{
		
		"dwStaSN",		/*站点编号*/
		
      "dwCfgType",		/*配置类型*/
		
		"szCfgName",		/*配置名称*/
	
		"szBrand",		/*品牌*/
	
		"szModel",		/*规格型号*/
	
		"szSpecification",		/*设备规格*/
	
		"szPurpose",		/*用途说明*/
	
		"szIndicators",		/*主要指标说明*/
	
		"dwPurchaseDate",		/*采购日期 YYYYMMDD*/
		
		"dwStartUseDate",		/*投入使用日期 YYYYMMDD*/
		
		"dwDevLife",		/*设备使用年限*/
		
		"szCPU",		/*CPU规格型号*/
	
		"dwMemSize",		/*内存大小（M）*/
		
		"dwDiskSize",		/*硬盘大小(G)*/
		
		"dwOsVer",		/*操作系统版本(dwMajorVersion*1000000 + dwMinorVersion*10000 + wProductType*100 +系统类型(32位或64位) )*/
		
		"szMemo",		/*备注*/
	
		"dwDelFlag",		/*删除标志*/
		 ""};

	/**/
	static public string[] UNISTATION = new string[]{
		
		"dwStaSN",		/*站点编号*/
		
		"szStaName",		/*站点名称*/
	
		"szLicSN",		/*许可编号*/
	
      "dwSubSysSN",		/*子系统编号*/
		
		"dwStatus",		/*站点状态*/
		
		"dwOwnerDept",		/*所属部门*/
		
		"dwManagerID",		/*负责人账号*/
		
		"dwAttendantID",		/*值班员账号*/
		
		"szDeptName",		/*部门名称*/
	
		"szManName",		/*负责人姓名*/
	
		"szAttendantName",		/*值班员姓名*/
	
		"szMemo",		/*备注*/
	 ""};

}
	/*结束数据结构*/

	/*开始数据结构*/
public partial class Struct_ST
{

	/**/
	static public string[] UNIDEPT = new string[]{
		
		"dwID",		/*ID*/
		
		"szDeptSN",		/*单位编号*/
	
		"szName",		/*名称*/
	
      "dwKind",		/*类型*/
		
		"szMemo",		/*备注*/
	 ""};

	/**/
	static public string[] DEPTREQ = new string[]{
		
		"dwID",		/*ID*/
		
		"szName",		/*名称*/
	
		"dwKind",		/*类型(行政部门或学院)*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*校区*/
	static public string[] UNICAMPUS = new string[]{
		
		"dwCampusID",		/*校区ID*/
		
		"szCampusName",		/*校区名称*/
	
		"dwCampusKind",		/*校区类型（扩展）*/
		
		"szMemo",		/*备注*/
	 ""};

	/*获取校区请求*/
	static public string[] CAMPUSREQ = new string[]{
		
		"dwCampusID",		/*校区ID*/
		
		"szCampusName",		/*校区名称*/
	
		"dwCampusKind",		/*校区类型（扩展）*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/**/
	static public string[] CLASSREQ = new string[]{
		
		"dwClassID",		/*ID*/
		
		"szClassName",		/*名称*/
	
		"dwMajorID",		/*专业ID*/
		
		"dwEnrolYear",		/*入学年份*/
		
		"dwClassKind",		/*类型*/
		
		"dwDeptID",		/*部门ID*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/**/
	static public string[] UNICLASS = new string[]{
		
		"dwClassID",		/*ID*/
		
		"szClassSN",		/*班级编号*/
	
		"szClassName",		/*名称*/
	
		"dwClassKind",		/*类型*/
		
		"dwDeptID",		/*部门ID*/
		
		"dwMajorID",		/*专业ID*/
		
		"dwEnrolYear",		/*入学年份*/
		
		"szMemo",		/*备注*/
	 ""};

	/*获取账户列表输入参数*/
	static public string[] ACCREQ = new string[]{
		
		"dwCardID",		/*卡ID号*/
		
		"dwAccNo",		/*账号*/
		
		"szPID",		/*学工号*/
	
		"szLogonName",		/*登录名(学工号)*/
	
		"szCardNo",		/*卡号*/
	
		"szTrueName",		/*姓名*/
	
		"dwIdent",		/*身份*/
		
		"dwStatus",		/*状态*/
		
		"dwEnrolYear",		/*入学年份(XX级)*/
		
		"dwClassID",		/*班级ID*/
		
		"dwDeptID",		/*部门ID*/
		
		"szMSN",		/*MSN*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*账户信息*/
	static public string[] UNIACCOUNT = new string[]{
		
		"dwAccNo",		/*账号*/
		
		"szPID",		/*学工号*/
	
		"szLogonName",		/*登录名(学工号)*/
	
		"szCardNo",		/*卡号*/
	
		"dwCardID",		/*卡ID号*/
		
		"szIDCard",		/*身份证号*/
	
		"szTrueName",		/*姓名*/
	
		"szPasswd",		/*加密密码*/
	
		"dwClassID",		/*班级ID*/
		
		"szClassName",		/*班级*/
	
		"dwDeptID",		/*部门ID*/
		
		"szDeptName",		/*部门*/
	
		"dwMajorID",		/*专业ID*/
		
		"szMajorName",		/*专业*/
	
		"dwSex",		/*性别见UniCommon.h*/
		
      "dwIdent",		/*身份 见UniCommon.h*/
		
      "dwKind",		/*类型*/
		
		"dwBirthday",		/*出生日期*/
		
		"dwEnrolYear",		/*入学年份(XX级)*/
		
		"dwSchoolYears",		/*学制*/
		
		"dwBalance",		/*余额*/
		
		"dwSubsidy",		/*补助*/
		
		"dwFreeTime",		/*免费时间(机时)*/
		
		"dwUseQuota",		/*已用限额*/
		
		"dwStatus",		/*状态 定义见UniCommon.h*/
		
		"dwExpiredDate",		/*过期日*/
		
		"szTel",		/*电话*/
	
		"szHandPhone",		/*手机*/
	
		"szEmail",		/*电邮*/
	
		"szMSN",		/*MSN*/
	
		"szQQ",		/*QQ*/
	
		"szHomeAddr",		/*家庭住址*/
	
		"szCurAddr",		/*校内住址*/
	
		"szMemo",		/*说明信息*/
	
		"szCurZip",		/*工作地址邮编(导师需要)*/
	
		"dwTutorID",		/*导师账号*/
		
		"szTutorName",		/*导师姓名*/
	 ""};

	/*扩展账户信息*/
	static public string[] UNIACCEXTINFO = new string[]{
		
		"pPhoto",		/*照片*/
	 ""};

	/*获取导师*/
	static public string[] TUTORREQ = new string[]{
		
		"dwTutorID",		/*导师账号*/
		
		"dwStudentAccNo",		/*学生账号*/
		
		"szTrueName",		/*导师姓名*/
	
		"dwDeptID",		/*部门ID*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*导师信息*/
	static public string[] UNITUTOR = new string[]{
		
		"dwAccNo",		/*账号*/
		
		"szTrueName",		/*姓名*/
	
		"szTel",		/*电话*/
	
		"szHandPhone",		/*手机*/
	
		"szEmail",		/*电邮*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*获取扩展身份人员信息*/
	static public string[] EXTIDENTACCREQ = new string[]{
		
		"dwIdent",		/*身份*/
		
		"dwAccNo",		/*账号*/
		
		"szTrueName",		/*导师姓名*/
	
		"dwDeptID",		/*部门ID*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*获取用户信息*/
	static public string[] ACCINFOREQ = new string[]{
		
		"dwAccNo",		/*账号*/
		
		"szLogonName",		/*登录名*/
	 ""};

	/*扩展身份人员信息*/
	static public string[] EXTIDENTACC = new string[]{
		
		"dwAccNo",		/*账号*/
		
		"dwIdent",		/*身份*/
		
		"szPID",		/*学工号*/
	
		"szLogonName",		/*登录名*/
	
		"szTrueName",		/*姓名*/
	
		"dwSex",		/*性别*/
		
		"dwDeptID",		/*部门ID*/
		
		"szDeptName",		/*部门*/
	
		"szTel",		/*电话*/
	
		"szHandPhone",		/*手机*/
	
		"szEmail",		/*电邮*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*获取导师的学生*/
	static public string[] TUTORSTUDENTREQ = new string[]{
		
		"dwTutorID",		/*导师账号*/
		
		"dwStatus",		/*学生状态*/
		
		"dwKind",		/*学生类型*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*导师学生信息*/
	static public string[] TUTORSTUDENT = new string[]{
		
		"dwTutorID",		/*导师账号*/
		
		"szTutorName",		/*导师姓名*/
	
		"dwStatus",		/*学生状态（审核状态见ADMINCHECK）*/
		
      "dwKind",		/*学生类型*/
		
		"dwAccNo",		/*账号*/
		
		"szPID",		/*学工号*/
	
		"szTrueName",		/*姓名*/
	
		"dwEnrolYear",		/*入学年份(XX级)*/
		
		"szTel",		/*电话*/
	
		"szHandPhone",		/*手机*/
	
		"szEmail",		/*电邮*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*导师审核学生*/
	static public string[] TUTORSTUDENTCHECK = new string[]{
		
		"dwTutorID",		/*导师ID*/
		
		"dwCheckStat",		/*审核状态(ADMINCHECK定义)*/
		
		"dwStudentAccNo",		/*学生账号*/
		
		"szStudentName",		/*学生姓名*/
	
		"szCheckDetail",		/*审查说明*/
	
		"szMemo",		/*备注*/
	 ""};

	/**/
	static public string[] ACCCHECKREQ = new string[]{
		
      "dwCheckType",		/*方式*/
		
		"szCheckKey",		/*认证关键字*/
	
		"szCheckPW",		/*认证密码*/
	
	"szAccInfo",		/*(UNIACCOUNT结构)*/
	 ""};

	/*存退款结构*/
	static public string[] UNIDEPOSIT = new string[]{
		
      "dwKind",		/*类别*/
		
		"dwAccNo",		/*账号*/
		
		"szPID",		/*学工号*/
	
		"szTrueName",		/*真实姓名*/
	
		"dwAmount",		/*数量*/
		
		"dwAdminID",		/*操作员*/
		
		"szMemo",		/*备注*/
	 ""};

	/*支付结算提交消费流水结构*/
	static public string[] UNIPAYMENT = new string[]{
		
		"dwSID",		/*流水号*/
		
		"dwAccNo",		/*帐号*/
		
		"dwCostDate",		/*消费日期*/
		
		"dwCostTime",		/*消费时间*/
		
		"dwCardTime",		/*卡扣费时间*/
		
		"dwDealTime",		/*提交流水时间*/
		
		"szPID",		/*学工号*/
	
		"szCardNo",		/*分配卡号*/
	
		"szTrueName",		/*真实姓名*/
	
		"dwFeeType",		/*消费类别(FEEDETAIL定义)*/
		
		"dwCostMoney",		/*消费金额*/
		
		"dwCostSubsidy",		/*消费补助*/
		
		"dwCostFreeTime",		/*消费补助时间*/
		
		"szPosInfo",		/*与一卡通对应的商户信息*/
	
		"szCardCostInfo",		/*卡扣费返回信息，不同的一卡通格式和内容都不同*/
	
		"dwRetStatus",		/*提交返回状态，参考UniCommon.h定义*/
		
		"dwRetDealSID",		/*返回第三方流水号*/
		
		"szRetDealInfo",		/*提交流水返回信息，不同的一卡通格式和内容都不同*/
	 ""};

	/*获取互动信息*/
	static public string[] NOTICEREQ = new string[]{
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
		"dwSID",		/*流水号*/
		
		"dwStatus",		/*状态*/
		
		"dwSubjectID",		/*通知事由ID*/
		
		"dwSubjectType",		/*通知事由类别*/
		
		"dwSender",		/*发送方*/
		
		"dwRecipient",		/*接受方*/
		 ""};

	/*互动信息*/
	static public string[] NOTICEINFO = new string[]{
		
		"dwSID",		/*流水号*/
		
		"dwSubSysSN",		/*子系统编号*/
		
		"dwStatus",		/*状态*/
		
		"dwSubjectID",		/*通知事由ID*/
		
		"dwSubjectType",		/*通知事由类别,审核产生的通知与审核类别(CHECKINFO::dwSubjectType)同*/
		
		"dwSender",		/*发送方帐号*/
		
		"dwRecipient",		/*接收方帐号*/
		
		"dwNoticeMode",		/*通知方式*/
		
		"dwOccurTime",		/*产生时间*/
		
		"dwSendTime",		/*发送时间*/
		
		"dwAffirmTime",		/*确认时间*/
		
		"szMemo",		/*备注*/
	
		"dwNoticeKind",		/*通知类别*/
		
		"dwCheckStat",		/*审查状态*/
		
		"szRecvName",		/*接收者姓名*/
	
		"szRecvMobile",		/*接收者手机*/
	
		"szRecvMail",		/*接收者邮箱*/
	
		"szSenderName",		/*发送者姓名*/
	
		"szSenderMobile",		/*发送者手机*/
	
		"szSenderMail",		/*发送者邮箱*/
	
		"szSessionID",		/*SessionID*/
	
		"szReason",		/*原因*/
	
		"szFullSendInfo",		/*发送内容*/
	 ""};

	/*通知短信确认*/
	static public string[] NOTICEAFFIRM = new string[]{
		
		"dwSID",		/*流水号*/
		
		"dwAffirmStat",		/*确认状态*/
		
		"dwNoticeMode",		/*通知方式*/
		
		"dwAffirmTime",		/*确认时间*/
		
		"szMemo",		/*备注*/
	 ""};

	/**/
	static public string[] MAJORREQ = new string[]{
		
		"dwMajorID",		/*ID*/
		
		"szMajorSN",		/*编号*/
	
		"szMajorName",		/*名称*/
	
		"dwKind",		/*类型*/
		
		"dwDeptID",		/*部门ID*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/**/
	static public string[] UNIMAJOR = new string[]{
		
		"dwMajorID",		/*ID*/
		
		"szMajorSN",		/*编号*/
	
		"szMajorName",		/*名称*/
	
		"dwKind",		/*类型*/
		
		"dwDeptID",		/*部门ID*/
		
		"szDeptName",		/*部门名称*/
	
		"dwSchoolYears",		/*入学年份*/
		
		"szMemo",		/*备注*/
	 ""};

	/**/
	static public string[] TESTDATAREQ = new string[]{
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
		"dwStatus",		/*数据状态*/
		
		"dwSID",		/*SID*/
		
		"dwAccNo",		/*账号*/
		
		"dwDevID",		/*设备ID*/
		
		"dwRoomID",		/*房间ID*/
		
		"dwLabID",		/*实验室ID*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/**/
	static public string[] UNITESTDATA = new string[]{
		
		"dwSID",		/*SID*/
		
		"dwDevID",		/*设备ID*/
		
		"szDevName",		/*设备名称*/
	
		"dwLabID",		/*实验室ID*/
		
		"szLabName",		/*实验室名称*/
	
		"dwRoomID",		/*房间ID*/
		
		"szRoomName",		/*房间名称*/
	
		"dwAccNo",		/*账号*/
		
		"szPID",		/*学工号*/
	
		"szTrueName",		/*真实姓名*/
	
		"dwSubmitDate",		/*提交日期*/
		
		"dwSubmitTime",		/*提交时间*/
		
		"dwFileSize",		/*文件大小*/
		
		"szDisplayName",		/*显示名称*/
	
		"szLocation",		/*存放位置*/
	
      "dwStatus",		/*状态*/
		
		"szMemo",		/*说明*/
	 ""};

	/*管理员上传实验数据*/
	static public string[] ADMINTESTDATA = new string[]{
		
		"szLogonName",		/*登录名*/
	
		"szPassword",		/*密码*/
	
	"TestData",		/*实验数据(UNITESTDATA)*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/**/
	static public string[] CLOUDDISKREQ = new string[]{
		
		"dwAccNo",		/*账号*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/**/
	static public string[] CLOUDDISK = new string[]{
		
		"dwFileID",		/*文件ID*/
		
		"szFileName",		/*文件名称*/
	
		"dwAccNo",		/*账号*/
		
		"szPID",		/*学工号*/
	
		"szTrueName",		/*真实姓名*/
	
		"dwSubmitDate",		/*提交日期*/
		
		"dwFileSize",		/*文件大小*/
		
		"szLocation",		/*存放位置*/
	
		"szMemo",		/*说明*/
	 ""};

	/**/
	static public string[] CDISKSTATREQ = new string[]{
		
		"dwAccNo",		/*账号*/
		 ""};

	/**/
	static public string[] CDISKSTAT = new string[]{
		
		"dwAccNo",		/*账号*/
		
		"szPID",		/*学工号*/
	
		"szTrueName",		/*真实姓名*/
	
		"dwTotalSize",		/*总大小*/
		
		"dwUsedSize",		/*已用空间*/
		
		"dwFileNum",		/*文件个数*/
		
		"szMemo",		/*说明*/
	 ""};

	/*获取任课教师信息*/
	static public string[] UNITEACHERREQ = new string[]{
		
		"dwAccNo",		/*账号*/
		
		"szPID",		/*工号*/
	
		"szTrueName",		/*教师姓名(模糊匹配)*/
	
		"dwDeptID",		/*部门ID*/
		
		"dwCourseID",		/*课程ID*/
		
		"szCourseName",		/*课程名称(模糊匹配)*/
	
		"dwYearTerm",		/*学期编号*/
		
      "dwReqProp",		/*请求附加属性*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*教授课程*/
	static public string[] TEACHCOURSE = new string[]{
		
		"dwCourseID",		/*课程ID*/
		
		"szCourseCode",		/*课程代码*/
	
		"szCourseName",		/*课程名称*/
	
		"szMemo",		/*备注*/
	 ""};

	/*任课教师信息*/
	static public string[] UNITEACHER = new string[]{
		
		"dwAccNo",		/*账号*/
		
		"szPID",		/*学工号*/
	
		"szTrueName",		/*姓名*/
	
		"dwSex",		/*性别*/
		
		"dwDeptID",		/*学院（部门）ID*/
		
		"szDeptName",		/*所属学院*/
	
		"szTel",		/*电话*/
	
		"szHandPhone",		/*手机*/
	
		"szEmail",		/*电邮*/
	
	"TeachCourse",		/*承担课程(CUniTable<TEACHCOURSE>)*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*请求用户使用信息*/
	static public string[] USERCURINFOREQ = new string[]{
		
		"dwAccNo",		/*账号*/
		 ""};

	/*用户使用信息(同CONUSERINFO)*/
	static public string[] USERCURINFO = new string[]{
		
		"dwUserStat",		/*用户状态*/
		
	"AccInfo",		/*UNIACCOUNT 结构*/
	
	"ResvInfo",		/*UNIRESERVE 结构*/
	
	"DevInfo",		/*UNIDEVICE 结构*/
	
	"BillInfo",		/*账单表(CUniTable<UNIBILL>)*/
	
		"szMemo",		/*说明信息*/
	 ""};

}
	/*结束数据结构*/

	/*开始数据结构*/
public partial class Struct_ST
{

	/**/
	static public string[] UNILAB = new string[]{
		
		"dwLabID",		/*实验室ID*/
		
		"szLabSN",		/*实验室编号*/
	
		"szLabName",		/*实验室名称*/
	
		"dwDeptID",		/*部门ID*/
		
		"szDeptName",		/*部门名称*/
	
		"szLabKindCode",		/*实验室类型编码*/
	
		"szLabLevelCode",		/*实验室建设水平编码*/
	
		"szLabFromCode",		/*实验室来源编码*/
	
		"szAcademicSubjectCode",		/*所属学科编码*/
	
		"dwLabClass",		/*实验室类别*/
		
		"dwManGroupID",		/*管理员组ID*/
		
		"dwCreateDate",		/*建立日期*/
		
		"szLabURL",		/*实验室简介URL*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/**/
	static public string[] LABREQ = new string[]{
		
		"dwLabID",		/*实验室ID*/
		
		"szLabSN",		/*实验室编号*/
	
		"szLabName",		/*实验室名称*/
	
		"dwDeptID",		/*部门ID*/
		
		"dwLabClass",		/*实验室类别编号*/
		
		"dwPurpose",		/*用途(见UNIRESERVE定义)*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/**/
	static public string[] FULLLAB = new string[]{
		
		"dwLabID",		/*实验室ID*/
		
		"szLabSN",		/*实验室编号*/
	
		"szLabName",		/*实验室名称*/
	
		"dwDeptID",		/*部门ID*/
		
		"szDeptName",		/*部门名称*/
	
		"szLabKindCode",		/*实验室类型编码*/
	
		"szLabLevelCode",		/*实验室建设水平编码*/
	
		"szLabFromCode",		/*实验室来源编码*/
	
		"szAcademicSubjectCode",		/*所属学科编码*/
	
		"dwLabClass",		/*实验室类别*/
		
		"szLabURL",		/*实验室简介URL*/
	
		"szMemo",		/*说明信息*/
	
		"dwAttendantID",		/*值班员账号*/
		
		"szAttendantName",		/*值班员姓名*/
	
		"dwTotalDevNum",		/*设备总数*/
		
		"dwUsableDevNum",		/*可用设备数*/
		
		"dwIdleDevNum",		/*空闲设备数*/
		 ""};

	/**/
	static public string[] FULLLABREQ = new string[]{
		
		"dwLabID",		/*实验室ID*/
		
		"szLabSN",		/*实验室编号*/
	
		"szLabName",		/*实验室名称*/
	
		"dwDeptID",		/*部门ID*/
		
		"dwLabClass",		/*实验室类别编号*/
		
		"dwPurpose",		/*用途(见UNIRESERVE定义)*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/**/
	static public string[] DEVCLSREQ = new string[]{
		
		"dwClassID",		/*设备(实验室)类别ID*/
		
		"szClassName",		/*设备(实验室)类别名称*/
	
		"dwKind",		/*类别*/
		
		"dwPurpose",		/*用途(见UNIRESERVE定义)*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*设备类别*/
	static public string[] UNIDEVCLS = new string[]{
		
		"dwClassID",		/*设备(实验室)类别ID*/
		
		"szClassSN",		/*设备分类号*/
	
		"szClassName",		/*设备(实验室)类别名称*/
	
		"szMemo",		/*说明信息*/
	
      "dwKind",		/*类别类型*/
		
		"dwResv1",		/*保留字段1*/
		
		"dwResv2",		/*保留字段2*/
		
		"szDevClsURL",		/*简介URL*/
	
		"szExtInfo",		/*扩展信息*/
	 ""};

	/**/
	static public string[] DEVKINDREQ = new string[]{
		
		"dwKindID",		/*设备名称类别ID*/
		
		"szKindName",		/*设备名称*/
	
		"szClassName",		/*类别名(*/
	
		"dwProperty",		/*设备属性*/
		
		"dwClassKind",		/*类别(见UNIDEVCLS的Kind定义)*/
		
		"szKindIDs",		/*所属类型,多个用逗号隔开*/
	
		"dwExtRelatedID",		/*扩展关联ID*/
		
		"dwPurpose",		/*用途(见UNIRESERVE定义)*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*设备名称类别*/
	static public string[] UNIDEVKIND = new string[]{
		
		"dwKindID",		/*设备名称类别ID*/
		
		"szKindName",		/*设备名称*/
	
		"szFuncCode",		/*设备功能用途编码*/
	
		"szModel",		/*设备型号*/
	
		"szSpecification",		/*设备规格*/
	
		"szProducer",		/*制造商*/
	
		"dwNationCode",		/*国别码*/
		
      "dwProperty",		/*设备属性*/
		
		"dwClassID",		/*所属功能类别ID*/
		
		"szClassSN",		/*设备分类号*/
	
		"szClassName",		/*类别名(*/
	
		"dwClassKind",		/*类别(见UNIDEVCLS的Kind定义)*/
		
		"dwMaxUsers",		/*总预约最多同时使用人数*/
		
		"dwMinUsers",		/*每次预约最少同时使用人数*/
		
		"dwTotalNum",		/*设备总数*/
		
		"dwUsableNum",		/*可用设备数（预约判断用）*/
		
		"szOperaCert",		/*操作证书*/
	
		"szDevKindURL",		/*设备详细介绍的URL地址*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*获取楼宇信息请求*/
	static public string[] BUILDINGREQ = new string[]{
		
		"dwBuildingID",		/*楼宇ID*/
		
		"szBuildingName",		/*楼宇名称*/
	
		"szBuildingNo",		/*楼宇号*/
	
		"szCampusIDs",		/*校区ID,多个用逗号隔开*/
	
		"dwActivitySN",		/*活动类型编号*/
		
		"dwPurpose",		/*用途(见UNIRESERVE定义)*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*楼宇名称*/
	static public string[] UNIBUILDING = new string[]{
		
		"dwBuildingID",		/*楼宇ID*/
		
		"szBuildingName",		/*楼宇名称*/
	
		"szBuildingNo",		/*楼宇号*/
	
		"szMapIndex",		/*地图索引*/
	
		"szBuildingURL",		/*详细介绍的URL地址*/
	
		"dwCampusID",		/*校区ID*/
		
		"szCampusName",		/*校区名称*/
	
		"dwCampusKind",		/*校区类型（扩展）*/
		
		"szMemo",		/*说明信息*/
	 ""};

	/*获取房间信息请求*/
	static public string[] ROOMREQ = new string[]{
		
		"dwRoomID",		/*房间ID*/
		
		"szRoomName",		/*房间名称*/
	
		"szRoomNo",		/*房间号*/
	
		"dwLabClass",		/*实验室类别编号*/
		
		"dwLabID",		/*实验室ID*/
		
		"szBuildingIDs",		/*楼宇ID,多个用逗号隔开*/
	
		"szCampusIDs",		/*校区ID,多个用逗号隔开*/
	
		"dwInClassKind",		/*存放设备类别(见UNIDEVCLS的Kind定义)*/
		
		"dwPurpose",		/*用途(见UNIRESERVE定义)*/
		
		"dwProperty",		/*扩展属性*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*房间名称*/
	static public string[] UNIROOM = new string[]{
		
		"dwRoomID",		/*房间ID*/
		
		"szRoomName",		/*房间名称*/
	
		"szRoomNo",		/*房间号*/
	
		"dwInClassKind",		/*存放设备类别(见UNIDEVCLS的Kind定义)*/
		
		"szFloorNo",		/*所在楼层*/
	
		"dwBuildingID",		/*楼宇ID*/
		
		"szBuildingNo",		/*大楼编号(*/
	
		"szBuildingName",		/*大楼名称(*/
	
		"dwRoomSize",		/*房间面积(平方)*/
		
		"szMapIndex",		/*地图索引*/
	
		"szRoomURL",		/*详细介绍的URL地址*/
	
		"szSubRooms",		/*下属房间(房间编号，可多个，逗号隔开)*/
	
		"szLabKindCode",		/*实验室类型编码*/
	
		"szLabLevelCode",		/*实验室建设水平编码*/
	
		"szLabFromCode",		/*实验室来源编码*/
	
		"szAcademicSubjectCode",		/*所属学科编码*/
	
		"dwLabClass",		/*实验室类别*/
		
		"dwCreateDate",		/*建立日期*/
		
		"dwOpenRuleSN",		/*开放规则编号*/
		
		"szOpenRuleName",		/*开放规则名称*/
	
		"dwLabID",		/*实验室ID*/
		
		"szLabSN",		/*实验室编号*/
	
		"szLabName",		/*实验室名称*/
	
		"szLabURL",		/*详细介绍的URL地址*/
	
		"dwDeptID",		/*部门ID*/
		
		"szDeptName",		/*部门名称*/
	
		"dwManGroupID",		/*房间管理员组ID*/
		
		"szManGroupName",		/*管理员组名称*/
	
      "dwManMode",		/*控制方式*/
		
		"dwCtrlSN",		/*控制器编号*/
		
		"dwCampusID",		/*校区ID*/
		
		"szCampusName",		/*校区名称*/
	
		"dwCampusKind",		/*校区类型（扩展）*/
		
		"szMemo",		/*说明信息*/
	
		"szMAIP",		/*手机签到IP段*/
	
      "dwProperty",		/*扩展属性*/
		 ""};

	/*获取房间信息请求*/
	static public string[] FULLROOMREQ = new string[]{
		
		"dwRoomID",		/*房间ID*/
		
		"szRoomName",		/*房间名称*/
	
		"szRoomNo",		/*房间号*/
	
		"dwLabClass",		/*实验室类别编号*/
		
		"dwLabID",		/*实验室ID*/
		
		"szBuildingIDs",		/*楼宇ID,多个用逗号隔开*/
	
		"szCampusIDs",		/*校区ID,多个用逗号隔开*/
	
		"dwInClassKind",		/*存放设备类别(见UNIDEVCLS的Kind定义)*/
		
		"dwPurpose",		/*用途(见UNIRESERVE定义)*/
		
		"dwProperty",		/*扩展属性*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*房间完整信息*/
	static public string[] FULLROOM = new string[]{
		
		"dwRoomID",		/*房间ID*/
		
		"szRoomName",		/*房间名称*/
	
		"szRoomNo",		/*房间号*/
	
		"dwInClassKind",		/*存放设备类别(见UNIDEVCLS的Kind定义)*/
		
		"szFloorNo",		/*所在楼层*/
	
		"dwBuildingID",		/*楼宇ID*/
		
		"szBuildingNo",		/*大楼编号(*/
	
		"szBuildingName",		/*大楼名称(*/
	
		"dwRoomSize",		/*房间面积(平方)*/
		
		"szMapIndex",		/*地图索引*/
	
		"szRoomURL",		/*详细介绍的URL地址*/
	
		"dwOpenRuleSN",		/*开放规则编号*/
		
		"szOpenRuleName",		/*开放规则名称*/
	
		"dwLabID",		/*实验室ID*/
		
		"szLabSN",		/*实验室编号*/
	
		"szLabName",		/*实验室名称*/
	
		"szLabKindCode",		/*实验室类型编码*/
	
		"szLabLevelCode",		/*实验室建设水平编码*/
	
		"szLabFromCode",		/*实验室来源编码*/
	
		"szAcademicSubjectCode",		/*所属学科编码*/
	
		"dwLabClass",		/*实验室类别*/
		
		"dwCreateDate",		/*建立日期*/
		
		"szLabURL",		/*详细介绍的URL地址*/
	
		"dwDeptID",		/*部门ID*/
		
		"szDeptName",		/*部门名称*/
	
		"dwManGroupID",		/*管理员组ID*/
		
		"szManGroupName",		/*管理员组名称*/
	
		"dwAttendantID",		/*值班员账号*/
		
		"szAttendantName",		/*值班员姓名*/
	
		"dwStatus",		/*房间门禁状态见 UNIDCS定义*/
		
		"dwStatChgTime",		/*状态改变开始时间(time函数秒)*/
		
		"szStatInfo",		/*状态描述*/
	
		"dwManMode",		/*控制方式*/
		
		"dwCtrlSN",		/*控制器编号*/
		
		"dwCampusID",		/*校区ID*/
		
		"szCampusName",		/*校区名称*/
	
		"dwCampusKind",		/*校区类型（扩展）*/
		
		"dwTotalDevNum",		/*设备总数*/
		
		"dwUsableDevNum",		/*可用设备数*/
		
		"dwIdleDevNum",		/*空闲设备数*/
		
		"szMemo",		/*说明信息*/
	
		"dwProperty",		/*扩展属性*/
		 ""};

	/*获取房间基本信息请求*/
	static public string[] BASICROOMREQ = new string[]{
		
		"dwRoomID",		/*房间ID*/
		
		"dwLabID",		/*实验室ID*/
		
		"dwInClassKind",		/*存放设备类别(见UNIDEVCLS的Kind定义)*/
		
		"dwDoorStat",		/*房间门禁状态见 UNIDCS定义*/
		
      "dwUseStat",		/*房间使用状态*/
		
		"dwUsePurpose",		/*见UNIDEVICE定义*/
		
		"dwProperty",		/*扩展属性*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*房间基本信息*/
	static public string[] BASICROOM = new string[]{
		
		"dwRoomID",		/*房间ID*/
		
		"szRoomName",		/*房间名称*/
	
		"szRoomNo",		/*房间号*/
	 ""};

	/*获取通道门信息请求*/
	static public string[] CHANNELGATEREQ = new string[]{
		
      "dwGetType",		/*方式*/
		
		"szGetKey",		/*条件值*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*通道门*/
	static public string[] UNICHANNELGATE = new string[]{
		
		"dwChannelGateID",		/*通道门ID*/
		
		"szChannelGateName",		/*通道门名称*/
	
		"szChannelGateNo",		/*通道门编号*/
	
		"szRelatedRooms",		/*关联房间(房间编号，可多个，逗号隔开)*/
	
		"szFloorNo",		/*所在楼层*/
	
		"dwBuildingID",		/*楼宇ID*/
		
		"szBuildingNo",		/*大楼编号(*/
	
		"szBuildingName",		/*大楼名称(*/
	
		"dwManGroupID",		/*管理员组ID*/
		
		"szManGroupName",		/*管理员组名称*/
	
		"dwUseGroupID",		/*允许用户组ID*/
		
		"szUseGroupName",		/*允许用户组名称*/
	
		"dwOpenRuleSN",		/*开放规则编号*/
		
		"dwStatus",		/*门禁状态见 UNIDCS定义*/
		
		"dwStatChgTime",		/*状态改变开始时间(time函数秒)*/
		
		"szStatInfo",		/*状态描述*/
	
		"dwManMode",		/*房间控制方式，见UNIROOM*/
		
		"dwCtrlSN",		/*控制器编号*/
		
		"szMemo",		/*说明信息*/
	 ""};

	/*控制房间命令*/
	static public string[] CHANNELGATECTRLINFO = new string[]{
		
		"dwChannelGateID",		/*通道门ID*/
		
		"szChannelGateNo",		/*通道门编号*/
	
		"dwCmd",		/*控制命令,参考DEVCTRLINFO定义*/
		
		"szParam",		/*控制参数*/
	
		"szMemo",		/*备注*/
	 ""};

	/*获取房间组合请求*/
	static public string[] ROOMGROUPREQ = new string[]{
		
		"dwRoomNum",		/*组合房间数*/
		
		"dwRoomID",		/*房间ID*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*房间组合成员*/
	static public string[] RGMEMBER = new string[]{
		
		"dwRoomID",		/*房间ID*/
		
		"szRoomNo",		/*房间号*/
	
		"szRoomName",		/*房间名称*/
	
		"dwDevNum",		/*设备数*/
		 ""};

	/*房间组合*/
	static public string[] ROOMGROUP = new string[]{
		
		"dwRGID",		/*房间组合ID*/
		
		"szRGName",		/*房间组合名称*/
	
		"dwRoomNum",		/*组合房间数*/
		
	"rgMember",		/*CUniTable[RGMEMBER]*/
	 ""};

	/*获取设备列表*/
	static public string[] DEVREQ = new string[]{
		
		"dwDevID",		/*设备ID*/
		
		"szSearchKey",		/*搜索关键字*/
	
		"dwDevSN",		/*设备编号*/
		
		"szAssertSN",		/*用户方资产编号*/
	
		"szTagID",		/*RFID标签ID*/
	
		"szLabIDs",		/*实验室ID,多个用逗号隔开*/
	
		"szRoomIDs",		/*房间ID,多个用逗号隔开*/
	
		"dwResvID",		/*预约ID*/
		
		"szKindIDs",		/*所属类型,多个用逗号隔开*/
	
		"szClassIDs",		/*所属功能类别ID,多个用逗号隔开*/
	
		"szDeptIDs",		/*学院ID,多个用逗号隔开*/
	
		"szBuildingIDs",		/*楼宇ID,多个用逗号隔开*/
	
		"szCampusIDs",		/*校区ID,多个用逗号隔开*/
	
		"dwDevStat",		/*设备状态*/
		
		"dwRunStat",		/*设备运行状态*/
		
		"dwUnNeedRunStat",		/*不包含运行状态*/
		
		"dwCtrlMode",		/*控制方式*/
		
		"dwProperty",		/*设备属性*/
		
		"dwClassKind",		/*类别(见UNIDEVCLS的Kind定义)*/
		
		"dwManGroupID",		/*管理员组ID*/
		
		"dwAttendantID",		/*值班员ID*/
		
		"szAttendantName",		/*值班员姓名(模糊)*/
	
		"dwMinUnitPrice",		/*最低价格*/
		
		"dwMaxUnitPrice",		/*最大价格*/
		
		"dwSPurchaseDate",		/*开始采购日期*/
		
		"dwEPurchaseDate",		/*截止采购日期*/
		
      "dwReqProp",		/*请求附加属性*/
		
		"dwPurpose",		/*用途(见UNIRESERVE定义)*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*计费信息明细*/
	static public string[] FDINFO = new string[]{
		
		"dwBeginTime",		/*开始时间*/
		
		"dwEndTime",		/*结束时间*/
		
		"dwFeeType",		/*收费类别(FEEDETAIL定义)*/
		
		"dwUsablePayKind",		/*可用缴费方式(见UNIBILL定义)*/
		
		"dwDefaultCheckStat",		/*CHECKINFO定义的管理员审核状态*/
		
		"dwUnitFee",		/*费率*/
		
		"dwUnitTime",		/*单位时间*/
		
		"dwRoundOff",		/*舍入分界点(小于单位时间)*/
		
		"dwIgnoreTime",		/*不计费时间*/
		
		"dwHolidayCoef",		/*假日系数*/
		
		"szPosInfo",		/*与一卡通对应的商户信息*/
	
		"dwUseTime",		/*使用时间*/
		
		"dwFeeTime",		/*计费时间*/
		
		"dwCostMoney",		/*费用*/
		
		"dwCostSubsidy",		/*使用补助*/
		
		"dwCostFreeTime",		/*使用免费时间(机时)*/
		 ""};

	/*计费信息*/
	static public string[] UNIACCTINFO = new string[]{
		
		"dwBeginTime",		/*开始时间*/
		
		"dwEndTime",		/*结束时间*/
		
		"dwUseTime",		/*使用时间*/
		
		"dwIdent",		/*身份（0表示无限制）*/
		
		"dwDeptID",		/*部门（0表示无限制）*/
		
		"dwDevKind",		/*设备类型（0表示无限制）*/
		
		"dwGroupID",		/*指定用户组（0表示无限制）*/
		
		"dwPurpose",		/*用途*/
		
		"dwResvID",		/*预约号*/
		
		"dwAccNo",		/*用户账号*/
		
		"szPID",		/*学工号*/
	
		"szLogonName",		/*登录名*/
	
		"szCardNo",		/*卡号*/
	
		"szTrueName",		/*姓名*/
	
		"szClassName",		/*班级*/
	
		"szDeptName",		/*部门*/
	
		"dwSex",		/*性别*/
		
		"dwDevID",		/*设备ID*/
		
		"dwLabID",		/*实验室ID*/
		
		"dwDevSN",		/*设备编号*/
		
		"szLabSN",		/*实验室编号*/
	
		"dwSID",		/*分配流水号*/
		
		"dwBalance",		/*余额*/
		
		"dwSubsidy",		/*补助*/
		
		"dwFreeTime",		/*免费时间*/
		
		"dwUseQuota",		/*已用限额*/
		
		"dwFeeSN",		/*费率编号*/
		
		"dwDeadLine",		/*该用户最长可用时间*/
		
	"szFDInfo",		/*CUniTable[FDINFO]*/
	
		"szMemo",		/*备注（扩展用）*/
	 ""};

	/*获取设备监控器请求*/
	static public string[] DEVMONITORREQ = new string[]{
		
		"dwMonitorID",		/*监控器ID*/
		
		"dwMonitorType",		/*监控器类别*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*设备监控器*/
	static public string[] DEVMONITOR = new string[]{
		
		"dwMonitorID",		/*监控器ID*/
		
      "dwMonitorType",		/*监控器类别*/
		
		"szMonitorName",		/*监控器名称*/
	
		"szIP",		/*IP地址*/
	
		"dwPort",		/*端口*/
		
		"szMemo",		/*备注*/
	 ""};

	/*获取监控器与设备的对应关系请求*/
	static public string[] MONDEVREQ = new string[]{
		
		"dwMonitorID",		/*监控器ID*/
		
		"dwMonitorType",		/*监控器类别*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*监控器与设备的对应关系*/
	static public string[] MONDEV = new string[]{
		
		"dwMonitorID",		/*监控器ID*/
		
		"dwMonitorType",		/*监控器类别*/
		
		"szMonitorName",		/*监控器名称*/
	
		"dwLabID",		/*监控器ID*/
		
		"dwDevID",		/*设备ID*/
		
		"dwDevSN",		/*设备编号*/
		
		"szTagID",		/*RFID标签ID*/
	
		"szDevName",		/*实验设备名称*/
	
		"szMemo",		/*备注*/
	 ""};

	/*设备监控状态*/
	static public string[] DEVMONITORSTAT = new string[]{
		
		"dwLabID",		/*监控器ID*/
		
		"dwDevID",		/*设备ID*/
		
		"dwMonitorStat",		/*监控状态(dwRunStat 的智能有人定义）*/
		 ""};

	/*设备预约明细*/
	static public string[] DEVRESV = new string[]{
		
		"dwResvID",		/*预约ID*/
		
		"dwUsePurpose",		/*同UNIRESERVE的dwPurpose*/
		
		"dwResvBeginTime",		/*预约开始时间*/
		
		"dwResvEndTime",		/*预约结束时间*/
		
		"szResvMemberName",		/*预约成员名*/
	
		"dwTeacherID",		/*任课教师ID*/
		
		"szTeacherName",		/*任课教师姓名*/
	 ""};

	/*设备使用信息*/
	static public string[] DEVUSEINFO = new string[]{
		
		"dwAccNo",		/*用户账号*/
		
		"szTrueName",		/*姓名*/
	
		"dwResvID",		/*预约号*/
		
		"dwBeginTime",		/*开始时间*/
		
	"FeeInfo",		/*CUniStruct[UNIACCTINFO]*/
	
		"dwUserUseStat",		/*用户使用状态*/
		
		"dwLeaveTime",		/*暂时离开时间*/
		
		"dwLeaveHoldSec",		/*暂时离开保留时间(秒）*/
		
		"dwQuotaRule",		/*限制规则(日累计，次累计，机器忙等(缺省0))*/
		
		"dwQuotaTime",		/*限制使用时间(缺省-1)*/
		
		"dwLoanAdminID",		/*外借管理员ID*/
		
		"szLoanAdminName",		/*外借管理员姓名*/
	
		"dwReturnAdminID",		/*归还管理员ID*/
		
		"szReturnAdminName",		/*归还管理员姓名*/
	
		"dwTutorID",		/*导师账号*/
		
		"szTutorName",		/*导师姓名*/
	
		"szMemo",		/*备注（扩展用）*/
	 ""};

	/*设备信息*/
	static public string[] UNIDEVICE = new string[]{
		
		"dwDevID",		/*设备ID*/
		
		"dwDevSN",		/*设备编号*/
		
		"szAssertSN",		/*用户方资产编号*/
	
		"szTagID",		/*RFID标签ID*/
	
		"szOriginSN",		/*原厂系列号*/
	
		"szDevName",		/*实验设备名称*/
	
		"dwUnitPrice",		/*设备单价*/
		
		"dwPurchaseDate",		/*采购日期 YYYYMMDD*/
		
      "dwDevStat",		/*设备状态*/
		
      "dwCtrlMode",		/*控制方式*/
		
      "dwRunStat",		/*设备状态*/
		
		"dwStatChgTime",		/*状态改变开始时间(time函数秒)*/
		
		"szStatInfo",		/*状态描述*/
	
		"dwClassID",		/*设备功能类别*/
		
		"szClassSN",		/*设备分类号*/
	
		"szClassName",		/*设备类别名称*/
	
		"dwClassKind",		/*类别(见UNIDEVCLS的Kind定义)*/
		
		"dwKindID",		/*设备类型*/
		
		"szKindName",		/*设备名称*/
	
		"szFuncCode",		/*设备功能用途编码*/
	
		"szModel",		/*设备型号*/
	
		"szSpecification",		/*设备规格*/
	
      "dwProperty",		/*设备属性（前16种为UNIDEVKIND定义*/
		
		"dwMaxUsers",		/*最多同时使用人数*/
		
		"dwMinUsers",		/*最少同时使用人数*/
		
		"szOperaCert",		/*操作证书*/
	
		"szDevKindURL",		/*设备类型详细介绍的URL地址*/
	
		"szDevURL",		/*设备详细介绍的URL地址*/
	
		"dwManGroupID",		/*管理员组ID*/
		
		"szManGroupName",		/*管理员组名称*/
	
		"dwUseGroupID",		/*设备使用组ID*/
		
		"dwOpenRuleSN",		/*开放规则编号*/
		
		"szPCName",		/*登录设备机器名*/
	
		"szIP",		/*计算机IP地址*/
	
		"szMAC",		/*网卡地址*/
	
		"szMemo",		/*说明信息*/
	
		"dwRoomID",		/*房间ID*/
		
		"szRoomNo",		/*房间号（不重复,用于关联门禁，格式B-F-N,建筑编号-楼层号-房间编号）*/
	
		"szRoomName",		/*房间名称*/
	
		"dwManMode",		/*控制方式(UNIROOM中定义)*/
		
		"szFloorNo",		/*所在楼层*/
	
		"dwBuildingID",		/*楼宇ID*/
		
		"szBuildingNo",		/*大楼编号(*/
	
		"szBuildingName",		/*大楼名称(*/
	
		"dwLabID",		/*实验室ID*/
		
		"szLabSN",		/*实验室编号*/
	
		"szLabName",		/*实验室名称*/
	
		"dwDeptID",		/*部门ID*/
		
		"szDeptName",		/*部门*/
	
		"dwCampusID",		/*校区ID*/
		
		"szCampusName",		/*校区名称*/
	
		"dwCampusKind",		/*校区类型（扩展）*/
		
		"dwVisitTimes",		/*浏览次数*/
		
		"dwUsePurpose",		/*同UniFee的dwPurpose*/
		
		"szExtInfo",		/*扩展信息*/
	
		"dwURLCtrl",		/*上网监控模式*/
		
		"dwURLCtrlParam",		/*上网监控设定值（根据不同的监控模式含义不一样)*/
		
		"dwURLEndTime",		/*终止时间*/
		
		"szURLCtrlName",		/*上网监控名称*/
	
		"dwSWCtrl",		/*软件监控模式*/
		
		"dwSWCtrlParam",		/*软件监控设定值（根据不同的监控模式含义不一样)*/
		
		"dwSWEndTime",		/*监控结束时间*/
		
		"szSWCtrlName",		/*软件监控名称*/
	
		"dwAttendantID",		/*值班员账号*/
		
		"szAttendantName",		/*值班员姓名*/
	
		"szAttendantTel",		/*值班员电话*/
	
	"DevResv",		/*CUniTable[DEVRESV](dwReqProp含DEVREQ_NEEDDEVRESV时才返回)*/
	
	"DevUse",		/*CUniTable[DEVUSEINFO](dwReqProp含DEVREQ_NEEDDEVUSE时才返回)*/
	
	"DevSample",		/*CUniTable[SAMPLEINFO](dwReqProp含DEVREQ_NEEDSAMPLE时才返回)*/
	 ""};

	/*获取设备配置表请求*/
	static public string[] DEVCFGREQ = new string[]{
		
		"dwMainDevID",		/*主设备ID*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*设备配置表*/
	static public string[] DEVCFG = new string[]{
		
		"dwMainDevID",		/*主设备ID*/
		
      "dwSubDevType",		/*辅助设备类别*/
		
		"dwDevID",		/*设备ID*/
		
		"dwDevSN",		/*设备编号*/
		
		"szAssertSN",		/*用户方资产编号*/
	
		"szOriginSN",		/*原厂系列号*/
	
		"szDevName",		/*实验设备名称*/
	
		"dwUnitPrice",		/*设备单价*/
		
		"dwPurchaseDate",		/*采购日期 YYYYMMDD*/
		
		"dwDevStat",		/*设备状态*/
		
		"dwClassID",		/*设备功能类别*/
		
		"szClassSN",		/*设备分类号*/
	
		"szClassName",		/*设备类别名称*/
	
		"dwClassKind",		/*类别(见UNIDEVCLS的Kind定义)*/
		
		"dwKindID",		/*设备类型*/
		
		"szKindName",		/*设备名称*/
	
		"szFuncCode",		/*设备功能用途编码*/
	
		"szModel",		/*设备型号*/
	
		"szSpecification",		/*设备规格*/
	
		"szDevKindURL",		/*设备类型详细介绍的URL地址*/
	
		"szDevURL",		/*设备详细介绍的URL地址*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*智能检测座位状态*/
	static public string[] SEATDETECTSTAT = new string[]{
		
		"szFloorNo",		/*所在楼层*/
	
		"szRoomNo",		/*房间号*/
	
		"dwDevSN",		/*座位编号*/
		
		"szTagID",		/*RFID标签ID*/
	
		"dwMonitorStat",		/*监控状态(dwRunStat 的智能有人定义）*/
		
		"dwChangeTime",		/*状态改变时间*/
		
		"szMemo",		/*说明信息*/
	 ""};

	/*管理员人工管理设备使用*/
	static public string[] DEVMANUSE = new string[]{
		
		"dwLabID",		/*实验室ID*/
		
		"dwDevID",		/*设备ID*/
		
		"dwResvID",		/*预约ID*/
		
      "dwMode",		/*操作模式*/
		
		"szExtInfo",		/*扩展信息*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*获取设备配置类别表请求*/
	static public string[] DEVCFGKINDREQ = new string[]{
		
		"dwMainDevID",		/*主设备ID*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*设备配置类别表*/
	static public string[] DEVCFGKIND = new string[]{
		
		"dwMainDevID",		/*主设备ID*/
		
		"dwSubDevType",		/*辅助设备类别*/
		
		"dwSubDevNum",		/*辅助设备数量*/
		
		"dwKindID",		/*设备类型*/
		
		"szKindName",		/*设备名称*/
	
		"szFuncCode",		/*设备功能用途编码*/
	
		"szModel",		/*设备型号*/
	
		"szSpecification",		/*设备规格*/
	
		"szDevKindURL",		/*设备类型详细介绍的URL地址*/
	
		"dwClassID",		/*设备功能类别*/
		
		"szClassSN",		/*设备分类号*/
	
		"szClassName",		/*设备类别名称*/
	
		"dwClassKind",		/*类别(见UNIDEVCLS的Kind定义)*/
		
		"szMemo",		/*说明信息*/
	 ""};

	/*设置设备值班员*/
	static public string[] DEVATTENDANT = new string[]{
		
		"dwDevID",		/*设备ID*/
		
		"dwLabID",		/*实验室ID*/
		
		"dwAttendantID",		/*值班员账号*/
		
		"szAttendantName",		/*值班员姓名*/
	
		"szAttendantTel",		/*值班员电话*/
	 ""};

	/*设备使用费的经费分配比例请求*/
	static public string[] DEVFARREQ = new string[]{
		
		"dwDevID",		/*设备ID*/
		 ""};

	/*设备使用费的经费分配比例*/
	static public string[] DEVFAR = new string[]{
		
		"dwDevID",		/*设备ID*/
		
		"dwFeeType",		/*收费类别(FEEDETAIL定义)*/
		
		"dwTestRate",		/*分析测试费比例*/
		
		"dwOpenFundRate",		/*开放基金比例*/
		
		"dwServiceRate",		/*劳务费比例*/
		
		"szMemo",		/*备注*/
	 ""};

	/*获取预约设备列表*/
	static public string[] DEVFORRESVREQ = new string[]{
		
      "dwGetType",		/*方式*/
		
		"szKey",		/*查询主条件*/
	
		"szSubKey",		/*查询辅条件，DEVFORRESV_DEVID时是LabID*/
	
		"dwKindID",		/*所属类型*/
		
		"dwClassID",		/*所属功能类别ID*/
		
		"dwResvPurpose",		/*预约用途*/
		
		"dwBeginTime",		/*开始时间*/
		
		"dwEndTime",		/*结束时间*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*设备信息*/
	static public string[] DEVFORRESV = new string[]{
		
		"dwDevID",		/*设备ID*/
		
		"dwDevSN",		/*设备编号*/
		
		"szDevName",		/*实验设备名称*/
	
		"dwClassID",		/*设备功能类别*/
		
		"szClassName",		/*设备类别名称*/
	
		"dwKindID",		/*设备类型*/
		
		"szKindName",		/*设备类型名称*/
	
		"dwMaxUsers",		/*最多同时使用人数*/
		
		"dwMinUsers",		/*最少同时使用人数*/
		
		"dwLabID",		/*实验室ID*/
		
		"szLabSN",		/*实验室编号*/
	
		"szLabName",		/*实验室名称*/
	
		"dwRoomID",		/*房间ID*/
		
		"szRoomNo",		/*房间号（不重复,用于关联门禁，格式B-F-N,建筑编号-楼层号-房间编号）*/
	
		"szRoomName",		/*房间名称*/
	
		"szDevKindURL",		/*设备详细介绍的URL地址*/
	
		"szDevURL",		/*设备URL地址*/
	
		"szExtInfo",		/*扩展信息*/
	
		"dwResvRate",		/*预约率(1-100)*/
		
		"dwResvRuleSN",		/*关联预约规则*/
		
		"dwOpenRuleSN",		/*关联开放时间表*/
		
		"dwFeeSN",		/*关联收费标准*/
		
		"szMemo",		/*说明信息*/
	 ""};

	/*查询设备预约状态请求*/
	static public string[] DEVRESVSTATREQ = new string[]{
		
		"dwDevID",		/*设备ID*/
		
		"szDevName",		/*实验设备名称*/
	
		"szLabIDs",		/*实验室ID,多个用逗号隔开*/
	
		"szRoomIDs",		/*房间ID,多个用逗号隔开*/
	
		"szKindIDs",		/*所属类型,多个用逗号隔开*/
	
		"szClassIDs",		/*所属功能类别ID,多个用逗号隔开*/
	
		"szDeptIDs",		/*学院ID,多个用逗号隔开*/
	
		"szBuildingIDs",		/*楼宇ID,多个用逗号隔开*/
	
		"szCampusIDs",		/*校区ID,多个用逗号隔开*/
	
		"dwResvPurpose",		/*预约用途*/
		
		"dwProperty",		/*设备属性(参看UNIDEVKIND定义)*/
		
		"szDates",		/*查询日期,多个用逗号隔开*/
	
		"dwClassKind",		/*类别(见UNIDEVCLS的Kind定义)*/
		
		"dwResvUsers",		/*预约人数*/
		
		"dwExtRelatedID",		/*扩展关联ID*/
		
      "dwReqProp",		/*请求附加属性*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*设备预约信息*/
	static public string[] DEVRESVTIME = new string[]{
		
		"dwResvID",		/*预约ID*/
		
		"dwPurpose",		/*预约用途*/
		
		"dwStatus",		/*预约状态*/
		
		"dwPreDate",		/*预约开始日期*/
		
		"dwBegin",		/*开始时间(HHMM或YYYYMMDD)*/
		
		"dwEnd",		/*结束时间(HHMM或YYYYMMDD)*/
		
		"dwOwner",		/*预约人(所有者)*/
		
		"szOwnerName",		/*预约人姓名*/
	
		"szMemberName",		/*成员名称*/
	
		"szTestName",		/*实验名称*/
	
		"dwSex",		/*预约人性别*/
		 ""};

	/*设备预约状态*/
	static public string[] DEVRESVSTAT = new string[]{
		
		"dwDevID",		/*设备ID*/
		
		"dwDevSN",		/*设备编号*/
		
		"szDevName",		/*实验设备名称*/
	
		"dwDevStat",		/*设备状态*/
		
		"dwRunStat",		/*运行状态*/
		
		"dwClassID",		/*设备功能类别*/
		
		"szClassName",		/*设备类别名称*/
	
		"dwKindID",		/*设备类型*/
		
		"szKindName",		/*设备类型名称*/
	
		"dwMaxUsers",		/*最多同时使用人数*/
		
		"dwMinUsers",		/*最少同时使用人数*/
		
		"dwProperty",		/*设备属性(参看UNIDEVKIND定义)*/
		
		"dwLabID",		/*实验室ID*/
		
		"szLabSN",		/*实验室编号*/
	
		"szLabName",		/*实验室名称*/
	
		"dwRoomID",		/*房间ID*/
		
		"szRoomNo",		/*房间号（不重复,用于关联门禁，格式B-F-N,建筑编号-楼层号-房间编号）*/
	
		"szRoomName",		/*房间名称*/
	
		"szFloorNo",		/*所在楼层*/
	
		"dwBuildingID",		/*楼宇ID*/
		
		"szBuildingNo",		/*大楼编号(*/
	
		"szBuildingName",		/*大楼名称(*/
	
		"dwDeptID",		/*部门ID*/
		
		"szDeptName",		/*部门*/
	
		"dwCampusID",		/*校区ID*/
		
		"szCampusName",		/*校区名称*/
	
		"dwCampusKind",		/*校区类型（扩展）*/
		
		"szDevKindURL",		/*设备详细介绍的URL地址*/
	
		"szDevURL",		/*设备URL地址*/
	
		"szExtInfo",		/*扩展信息*/
	
      "dwOpenLimit",		/*开放限制见GROUPOPENRULE定义+下面定义*/
		
	"szRuleInfo",		/*CUniStruct[UNIRESVRULE]*/
	
	"szOpenInfo",		/*CUniTable[DAYOPENRULE]*/
	
	"szResvInfo",		/*CUniTable[DEVRESVTIME]*/
	 ""};

	/*查询设备长期预约状态请求*/
	static public string[] DEVLONGRESVSTATREQ = new string[]{
		
		"dwDevID",		/*设备ID*/
		
		"szDevName",		/*实验设备名称*/
	
		"szLabIDs",		/*实验室ID,多个用逗号隔开*/
	
		"szRoomIDs",		/*房间ID,多个用逗号隔开*/
	
		"szKindIDs",		/*所属类型,多个用逗号隔开*/
	
		"szClassIDs",		/*所属功能类别ID,多个用逗号隔开*/
	
		"szDeptIDs",		/*学院ID,多个用逗号隔开*/
	
		"szBuildingIDs",		/*楼宇ID,多个用逗号隔开*/
	
		"szCampusIDs",		/*校区ID,多个用逗号隔开*/
	
		"dwResvPurpose",		/*预约用途*/
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*开始日期*/
		
		"dwClassKind",		/*类别(见UNIDEVCLS的Kind定义)*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*设备长期预约状态*/
	static public string[] DEVLONGRESVSTAT = new string[]{
		
		"dwDevID",		/*设备ID*/
		
		"dwDevSN",		/*设备编号*/
		
		"szDevName",		/*实验设备名称*/
	
		"dwClassID",		/*设备功能类别*/
		
		"szClassName",		/*设备类别名称*/
	
		"dwKindID",		/*设备类型*/
		
		"szKindName",		/*设备类型名称*/
	
		"dwMaxUsers",		/*最多同时使用人数*/
		
		"dwMinUsers",		/*最少同时使用人数*/
		
		"dwLabID",		/*实验室ID*/
		
		"szLabSN",		/*实验室编号*/
	
		"szLabName",		/*实验室名称*/
	
		"dwRoomID",		/*房间ID*/
		
		"szRoomNo",		/*房间号（不重复,用于关联门禁，格式B-F-N,建筑编号-楼层号-房间编号）*/
	
		"szRoomName",		/*房间名称*/
	
		"szFloorNo",		/*所在楼层*/
	
		"dwBuildingID",		/*楼宇ID*/
		
		"szBuildingNo",		/*大楼编号(*/
	
		"szBuildingName",		/*大楼名称(*/
	
		"dwDeptID",		/*部门ID*/
		
		"szDeptName",		/*部门*/
	
		"dwCampusID",		/*校区ID*/
		
		"szCampusName",		/*校区名称*/
	
		"dwCampusKind",		/*校区类型（扩展）*/
		
		"szDevKindURL",		/*设备详细介绍的URL地址*/
	
		"szDevURL",		/*设备URL地址*/
	
		"szExtInfo",		/*扩展信息*/
	
	"szRuleInfo",		/*CUniStruct[UNIRESVRULE]*/
	
	"szResvInfo",		/*CUniTable[DEVRESVTIME]*/
	 ""};

	/*查询实验室预约状态请求*/
	static public string[] LABRESVSTATREQ = new string[]{
		
      "dwGetType",		/*方式*/
		
		"szGetKey",		/*查询主条件*/
	
		"dwDate",		/*查询日期*/
		 ""};

	/*教学预约详细信息*/
	static public string[] TEACHINGRESVINFO = new string[]{
		
		"dwTestItemID",		/*实验项目ID*/
		
		"dwTestCardID",		/*实验项目卡ID*/
		
		"szTestName",		/*实验名称*/
	
		"dwTestPlanID",		/*实验计划ID*/
		
		"szTestPlanName",		/*实验计划名称*/
	
		"dwTeacherID",		/*教师（帐号）*/
		
		"szTeacherName",		/*教师姓名*/
	
		"dwCourseID",		/*课程ID*/
		
		"szCourseName",		/*课程名称*/
	
		"dwGroupID",		/*上课班级*/
		
		"szGroupName",		/*上课班级名称*/
	
		"dwTeachingTime",		/*教学时间(格式见UNIRESERVE)*/
		
		"dwResvStat",		/*预约状态(格式见UNIRESERVE)*/
		 ""};

	/*实验室预约状态*/
	static public string[] LABRESVSTAT = new string[]{
		
		"dwLabID",		/*实验室ID*/
		
		"szLabSN",		/*实验室编号*/
	
		"szLabName",		/*实验室名称*/
	
		"dwDevNum",		/*设备数*/
		
	"szResvInfo",		/*CUniTable[TEACHINGRESVINFO]*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*查询房间预约状态请求*/
	static public string[] ROOMRESVSTATREQ = new string[]{
		
		"szRoomName",		/*房间名称*/
	
		"szLabIDs",		/*实验室ID,多个用逗号隔开*/
	
		"szRoomIDs",		/*房间ID,多个用逗号隔开*/
	
		"szKindIDs",		/*所属类型,多个用逗号隔开*/
	
		"dwRoomProp",		/*房间属性*/
		
		"dwUnNeedRoomProp",		/*不包含房间属性*/
		
		"dwMinDevNum",		/*最少设备数*/
		
		"dwMaxDevNum",		/*最多设备数*/
		
		"dwDate",		/*查询日期*/
		 ""};

	/*房间预约状态*/
	static public string[] ROOMRESVSTAT = new string[]{
		
		"dwRoomID",		/*房间ID*/
		
		"szRoomNo",		/*房间编号*/
	
		"szRoomName",		/*房间名称*/
	
		"dwRoomProp",		/*房间属性*/
		
		"dwDevNum",		/*设备数*/
		
	"szResvInfo",		/*CUniTable[TEACHINGRESVINFO]*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*查询房间组合预约状态请求*/
	static public string[] RGRESVSTATREQ = new string[]{
		
		"dwMinDevNum",		/*最少设备数*/
		
		"dwMaxDevNum",		/*最多设备数*/
		
		"dwDate",		/*查询日期*/
		 ""};

	/*房间组合预约状态*/
	static public string[] RGRESVSTAT = new string[]{
		
		"dwRGID",		/*房间ID*/
		
		"szRGName",		/*房间组合名称*/
	
		"dwRoomNum",		/*组合房间数*/
		
		"dwDevNum",		/*设备数*/
		
	"szResvInfo",		/*CUniTable[TEACHINGRESVINFO]*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*获取预约设备列表(按类型)*/
	static public string[] DEVKINDFORRESVREQ = new string[]{
		
		"szKindName",		/*实验设备名称*/
	
		"szLabIDs",		/*实验室ID,多个用逗号隔开*/
	
		"szRoomIDs",		/*房间ID,多个用逗号隔开*/
	
		"szKindIDs",		/*所属类型,多个用逗号隔开*/
	
		"szClassIDs",		/*所属功能类别ID,多个用逗号隔开*/
	
		"szDeptIDs",		/*学院ID,多个用逗号隔开*/
	
		"szBuildingIDs",		/*楼宇ID,多个用逗号隔开*/
	
		"szCampusIDs",		/*校区ID,多个用逗号隔开*/
	
		"dwClassKind",		/*类别(见UNIDEVCLS的Kind定义)*/
		
		"dwResvPurpose",		/*预约用途*/
		
		"dwProperty",		/*设备属性(参看UNIDEVKIND定义)*/
		
		"dwDate",		/*查询日期*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*获取长期预约设备列表(按类型)*/
	static public string[] DEVKINDFORLONGRESVREQ = new string[]{
		
		"szKindName",		/*实验设备名称*/
	
		"szLabIDs",		/*实验室ID,多个用逗号隔开*/
	
		"szRoomIDs",		/*房间ID,多个用逗号隔开*/
	
		"szKindIDs",		/*所属类型,多个用逗号隔开*/
	
		"szClassIDs",		/*所属功能类别ID,多个用逗号隔开*/
	
		"szDeptIDs",		/*学院ID,多个用逗号隔开*/
	
		"szBuildingIDs",		/*楼宇ID,多个用逗号隔开*/
	
		"szCampusIDs",		/*校区ID,多个用逗号隔开*/
	
		"dwClassKind",		/*类别(见UNIDEVCLS的Kind定义)*/
		
		"dwResvPurpose",		/*预约用途*/
		
		"dwProperty",		/*设备属性(参看UNIDEVKIND定义)*/
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*开始日期*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*设备信息*/
	static public string[] DEVKINDFORRESV = new string[]{
		
		"dwKindID",		/*设备类型*/
		
		"szKindName",		/*实验设备名称*/
	
		"dwClassID",		/*设备功能类别*/
		
		"szClassName",		/*设备类别名称*/
	
		"szFuncCode",		/*设备功能用途编码*/
	
		"szModel",		/*设备型号*/
	
		"szSpecification",		/*设备规格*/
	
		"dwProperty",		/*设备属性*/
		
		"dwMaxUsers",		/*最多同时使用人数*/
		
		"dwMinUsers",		/*最少同时使用人数*/
		
		"dwTotalNum",		/*设备总数*/
		
		"szOperaCert",		/*操作证书*/
	
		"szDevKindURL",		/*设备详细介绍的URL地址*/
	
		"dwLabID",		/*实验室ID*/
		
		"szLabSN",		/*实验室编号*/
	
		"szLabName",		/*实验室名称*/
	
		"dwRoomID",		/*房间ID*/
		
		"szRoomNo",		/*房间号（不重复,用于关联门禁，格式B-F-N,建筑编号-楼层号-房间编号）*/
	
		"szRoomName",		/*房间名称*/
	
		"szFloorNo",		/*所在楼层*/
	
		"dwBuildingID",		/*楼宇ID*/
		
		"szBuildingNo",		/*大楼编号(*/
	
		"szBuildingName",		/*大楼名称(*/
	
		"dwDeptID",		/*部门ID*/
		
		"szDeptName",		/*部门*/
	
		"dwCampusID",		/*校区ID*/
		
		"szCampusName",		/*校区名称*/
	
		"dwCampusKind",		/*校区类型（扩展）*/
		
		"szUsableNumArray",		/* 短期预约每字节代表0-24点对应的分钟的可用设备数(长度1440),
            长期预约表示每天的可用设备，长度为查询天数。=A表示大于9台可用设备,U表示不开放 */
	
		"dwOpenLimit",		/*开放限制见GROUPOPENRULE定义+DEVRESVSTAT定义*/
		
	"szRuleInfo",		/*CUniStruct[UNIRESVRULE]*/
	
	"szOpenInfo",		/*CUniTable[DAYOPENRULE]*/
	
	"szFeeInfo",		/*CUniStruct[UNIFEE]*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*获取预约设备列表(按房间)*/
	static public string[] ROOMFORRESVREQ = new string[]{
		
		"szRoomName",		/*实验设备名称*/
	
		"szFloorNo",		/*所在楼层*/
	
		"szRoomIDs",		/*房间ID,多个用逗号隔开*/
	
		"dwKindID",		/*所属类型*/
		
		"szBuildingIDs",		/*楼宇ID,多个用逗号隔开*/
	
		"szCampusIDs",		/*校区ID,多个用逗号隔开*/
	
		"dwClassKind",		/*类别(见UNIDEVCLS的Kind定义)*/
		
		"dwResvPurpose",		/*预约用途*/
		
		"dwDate",		/*查询日期*/
		
		"dwBeginTime",		/*预约开始时间*/
		
		"dwEndTime",		/*预约结束时间*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*房间预约信息*/
	static public string[] ROOMFORRESV = new string[]{
		
		"dwRoomID",		/*房间ID*/
		
		"szRoomName",		/*房间名称*/
	
		"szRoomNo",		/*房间号*/
	
		"szFloorNo",		/*所在楼层*/
	
		"dwOpenBegin",		/*开始时间(HHMM)*/
		
		"dwOpenEnd",		/*结束时间(HHMM)*/
		
		"dwTotalNum",		/*设备总数*/
		
		"dwUsableNum",		/*可用设备数*/
		
		"szMemo",		/*说明信息*/
	 ""};

	/*获取预约可用设备请求包*/
	static public string[] RESVUSABLEDEVREQ = new string[]{
		
		"dwResvID",		/*预约ID*/
		 ""};

	/*租借设备明细*/
	static public string[] RESVUSABLEDEV = new string[]{
		
		"dwLabID",		/*实验室ID*/
		
		"dwDevID",		/*设备ID*/
		
		"dwDevSN",		/*设备SN*/
		
		"dwDevKind",		/*设备类型*/
		
		"szDevName",		/*设备名*/
	
		"szKindName",		/*设备类别名*/
	
		"dwRoomID",		/*房间ID*/
		
		"szRoomNo",		/*房间编号*/
	
		"szRoomName",		/*房间名称*/
	
		"szExtInfo",		/*扩展信息*/
	 ""};

	/*客户端注册到服务器*/
	static public string[] DEVREGISTREQ = new string[]{
		
		"dwStaSN",		/*站点编号*/
		
		"szCltVersion",		/*客户端版本*/
	
		"szPCName",		/*机器名称*/
	
		"szIP",		/*IP地址*/
	
		"szMAC",		/*网卡地址*/
	
	"szCfgInfo",		/*配置信息CUniTable[DEVICECONFIG]*/
	 ""};

	/*服务器对客户端注册的响应*/
	static public string[] DEVREGISTRES = new string[]{
		
		"dwSessionID",		/*服务器分配的SessionID*/
		
	"SrvVer",		/*UNIVERSION 结构*/
	
		"szCurTime",		/*服务器时间 YYYY-MM-DD HH:MM:SS*/
	
		"dwDevSN",		/*本设备编号*/
		
		"dwDevID",		/*本设备ＩＤ号*/
		
		"dwLabID",		/*本实验室ＩＤ号*/
		
		"dwFunc",		/*使用功能模块*/
		
      "dwParam",		/*参数配置*/
		
		"dwDevStat",		/*设备状态*/
		
		"dwRunStat",		/*运行状态*/
		
		"dwPasswdCode",		/*卸载密码种子*/
		
		"szLabName",		/*实验室名称*/
	
		"szDisplayInfo",		/*登录界面显示信息*/
	
		"szCastParam",		/*屏幕广播参数*/
	
	"DevInfo",		/*UNIDEVICE 结构*/
	 ""};

	/*客户端登录请求*/
	static public string[] DEVLOGONREQ = new string[]{
		
		"dwDevID",		/*客户端设备的ID号*/
		
		"dwLabID",		/*实验室的ID号*/
		
      "dwLogonType",		/*登录类别*/
		
		"szLogonName",		/*登录名*/
	
		"szPasswd",		/*用户密码*/
	
		"szMemo",		/*备注（扩展用）*/
	 ""};

	/*客户端登录请求的响应*/
	static public string[] DEVLOGONRES = new string[]{
		
	"szAccInfo",		/*使用者信息(UNIACCOUNT结构)*/
	
		"dwPurpose",		/*用途*/
		
		"szDeclareInfo",		/*进入系统的声明信息*/
	
	"ResvInfo",		/*预约信息*/
	 ""};

	/*客户端查询当前信息*/
	static public string[] DEVQUERYREQ = new string[]{
		
		"dwDevID",		/*客户端设备的ID号*/
		
		"dwLabID",		/*实验室的ID号*/
		
		"dwAccNo",		/*帐号*/
		
		"szMemo",		/*备注（扩展用）*/
	 ""};

	/*客户端注销时用户选择的计费信息*/
	static public string[] USERFEECHECK = new string[]{
		
		"dwResvID",		/*预约号*/
		
		"dwAccNo",		/*用户账号*/
		
		"dwFeeMode",		/*收费方式(定义在UNIRESVRULE)*/
		
	"ResvSample",		/*CUniTable[RESVSAMPLE]*/
	
	"FeeInfo",		/*CUniStruct[UNIACCTINFO]*/
	
		"szMemo",		/*备注（扩展用）*/
	 ""};

	/*客户端注销请求*/
	static public string[] DEVLOGOUTREQ = new string[]{
		
		"dwDevID",		/*客户端设备的ID号*/
		
		"dwLabID",		/*实验室的ID号*/
		
		"dwAccNo",		/*帐号*/
		
      "dwParam",		/*退出请求参数*/
		
	"FeeCheck",		/*费用信息(只有支持 注销时选择收费模式的设备有效)*/
	 ""};

	/*客户端注销请求*/
	static public string[] DEVLOGOUTRES = new string[]{
		
	"szAccInfo",		/*使用者信息(UNIACCOUNT结构)*/
	
      "dwParam",		/*退出参数*/
		
		"szMemo",		/*备注（扩展用）*/
	 ""};

	/*客户端与服务器定时握手请求*/
	static public string[] DEVHANDSHAKEREQ = new string[]{
		
		"dwDevID",		/*客户端设备的ID号*/
		
		"dwLabID",		/*实验室的ID号*/
		
		"dwAccNo",		/*帐号*/
		
		"szDevChgInfo",		/*设备软硬件变更信息*/
	
		"szMemo",		/*备注（扩展用）*/
	 ""};

	/*服务器对客户端定时握手的响应*/
	static public string[] DEVHANDSHAKERES = new string[]{
		
		"dwFunc",		/*使用功能模块*/
		
		"dwDevStat",		/*设备状态*/
		
		"dwRunStat",		/*运行状态*/
		
		"dwUndoCmd",		/*未处理的命令*/
		
		"dwLockWaitTime",		/*等待锁定客户端时间*/
		
		"szMemo",		/*备注*/
	 ""};

	/*客户端与服务器定时握手请求*/
	static public string[] CLTCHGPWINFO = new string[]{
		
		"dwDevID",		/*客户端设备的ID号*/
		
		"dwLabID",		/*实验室的ID号*/
		
		"dwAccNo",		/*帐号*/
		
		"szOldPw",		/*旧密码*/
	
		"szNewPw",		/*新密码*/
	
		"szMemo",		/*备注（扩展用）*/
	 ""};

	/*控制设备命令*/
	static public string[] DEVCTRLINFO = new string[]{
		
		"dwLabID",		/*实验室ID号*/
		
		"dwDevID",		/*客户端设备的ID号*/
		
      "dwCmd",		/*控制命令*/
		
		"szParam",		/*控制参数*/
	
		"szMemo",		/*备注*/
	 ""};

	/*控制房间命令*/
	static public string[] ROOMCTRLINFO = new string[]{
		
		"dwCtrlSN",		/*控制器编号*/
		
		"dwRoomID",		/*房间ID号*/
		
		"dwCmd",		/*控制命令,参考DEVCTRLINFO定义*/
		
		"szParam",		/*控制参数*/
	
		"szMemo",		/*备注*/
	 ""};

	/*用户可进入房间请求*/
	static public string[] PERMITROOMREQ = new string[]{
		
		"dwAccNo",		/*帐号*/
		 ""};

	/*用户可进入房间信息*/
	static public string[] PERMITROOMINFO = new string[]{
		
      "dwRoomKind",		/*房间类型*/
		
      "dwPermitMode",		/*允许模式*/
		
		"dwRoomID",		/*房间ID*/
		
		"szRoomNo",		/*房间编号*/
	
		"szRoomName",		/*房间名称*/
	 ""};

	/*获取房间控制信息请求包*/
	static public string[] ROOMCTRLREQ = new string[]{
		
		"szRoomNo",		/*房间号*/
	
		"dwDCSKind",		/*门禁集控器类型*/
		 ""};

	/*控制设备命令*/
	static public string[] CTRLREQ = new string[]{
		
		"dwLabID",		/*实验室ID号*/
		
		"dwDevID",		/*客户端设备的ID号(为空表明设置所有实验室)*/
		
		"dwCtrl",		/*当前监控模式*/
		
		"dwCtrlParam",		/*监控设定值（根据不同的监控模式含义不一样）*/
		
		"dwEndTime",		/*终止时间*/
		
		"szCtrlName",		/*监控名*/
	 ""};

	/*租借设备明细*/
	static public string[] LOANDEV = new string[]{
		
		"dwDevID",		/*设备ID*/
		
		"dwDevSN",		/*设备编号*/
		
		"dwDevKind",		/*设备类型*/
		
		"szDevName",		/*设备名*/
	
		"szRoomNo",		/*所在房间*/
	
		"dwDevClsKind",		/*设备类别分类*/
		 ""};

	/*获取运行程序请求*/
	static public string[] RUNAPPREQ = new string[]{
		
		"dwLabID",		/*实验室ID*/
		
		"dwDevID",		/*设备ID*/
		
		"szMemo",		/*说明信息*/
	 ""};

	/*运行程序*/
	static public string[] RUNAPP = new string[]{
		
		"dwRunNum",		/*当前运行数量合计*/
		
		"dwProcID",		/*程序ID*/
		
		"dwProperty",		/*程序属性*/
		
		"szProductName",		/*产品名称*/
	
		"szExeName",		/*Exe文件名*/
	
		"szSWVersion",		/*程序版本*/
	
		"szDispProductName",		/*显示程序名称*/
	
		"szDispSWName",		/*显示产品名称*/
	
		"szDispSWCompany",		/*显示公司名称*/
	
		"szInstName",		/*安装名称*/
	
		"szInstPath",		/*安装路径*/
	
		"szIcon",		/*图标*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*上传机器软件信息结束*/
	static public string[] SWUPLOADEND = new string[]{
		
		"dwDevID",		/*客户端设备的ID号*/
		
		"dwLabID",		/*实验室的ID号*/
		
		"dwTotalNum",		/*总记录数*/
		
		"dwCollectSecond",		/*收集用时(秒数)*/
		
		"dwUploadSecond",		/*上传用时(秒数)*/
		
		"szMemo",		/*备注（扩展用）*/
	 ""};

	/*设备外借请求*/
	static public string[] DEVLOANREQ = new string[]{
		
		"dwLabID",		/*实验室ID*/
		
		"dwResvID",		/*预约号*/
		
		"dwLender",		/*租借人*/
		
		"szLenderName",		/*租借人姓名*/
	
		"dwBeginTime",		/*租借开始时间*/
		
		"dwEndTime",		/*租借结束时间*/
		
	"szLoanDevs",		/*租借设备明细表(CUniTable<LOANDEV>)*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*设备归还请求*/
	static public string[] DEVRETURNREQ = new string[]{
		
		"dwResvID",		/*预约号*/
		
		"dwLender",		/*租借人*/
		
		"szLenderName",		/*租借人姓名*/
	
		"dwLabID",		/*实验室ID*/
		
		"dwDevID",		/*设备ID*/
		
		"dwCheckStat",		/*设备状态*/
		
		"dwCompensation",		/*赔偿金额*/
		
		"dwPunishScore",		/*信用扣分*/
		
		"szDamageInfo",		/*损坏说明*/
	
		"szExtInfo",		/*设备新描述*/
	 ""};

	/**/
	static public string[] DEVDAMAGERECREQ = new string[]{
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
		"dwSID",		/*SID*/
		
		"dwDevID",		/*设备ID*/
		
		"szLabIDs",		/*实验室ID,多个用逗号隔开*/
	
		"szRoomIDs",		/*房间ID,多个用逗号隔开*/
	
		"szKindIDs",		/*所属类型,多个用逗号隔开*/
	
		"szClassIDs",		/*所属功能类别ID,多个用逗号隔开*/
	
		"dwProperty",		/*设备属性*/
		
		"dwClassKind",		/*类别(见UNIDEVCLS的Kind定义)*/
		
		"dwUnitPrice",		/*大型仪器价格起点*/
		
		"dwStatus",		/*维修状态*/
		
		"dwManID",		/*经办人ID*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/**/
	static public string[] DEVDAMAGEREC = new string[]{
		
		"dwSID",		/*SID*/
		
		"dwLabID",		/*实验室ID*/
		
		"szLabName",		/*实验室名称*/
	
		"dwKindID",		/*设备类型*/
		
		"szKindName",		/*设备类型名*/
	
		"dwDevID",		/*设备ID*/
		
		"dwDevSN",		/*设备编号*/
		
		"szDevName",		/*设备名称*/
	
		"szAssertSN",		/*用户方资产编号*/
	
		"dwUnitPrice",		/*设备单价*/
		
		"dwPurchaseDate",		/*采购日期*/
		
      "dwStatus",		/*见UNIBILL定义+如下定义*/
		
		"dwDamageDate",		/*损坏日期*/
		
		"dwDamageTime",		/*损坏时间*/
		
		"szDamageInfo",		/*损坏说明*/
	
		"dwRepareDate",		/*维修日期*/
		
		"dwRepareTime",		/*维修时间*/
		
		"szRepareInfo",		/*维修说明*/
	
		"dwRepareCost",		/*维修费用*/
		
		"szFundsNo1",		/*经费卡编号1*/
	
		"dwPay1",		/*经费卡1支付*/
		
		"szFundsNo2",		/*经费卡编号2*/
	
		"dwPay2",		/*经费卡2支付*/
		
		"szRepareCom",		/*维修单位*/
	
		"szRepareComTel",		/*维修单位联系方式*/
	
		"dwManID",		/*经办人ID*/
		
		"szManName",		/*经办人姓名*/
	
		"dwAccNo",		/*账号*/
		
		"szTrueName",		/*真实姓名*/
	
		"dwCompensation",		/*赔偿金额*/
		
		"dwPunishScore",		/*处罚分数*/
		
		"szMemo",		/*说明*/
	 ""};

	/**/
	static public string[] DEVOPENRULEREQ = new string[]{
		
		"dwRuleSN",		/*编号*/
		
		"szRuleName",		/*名称*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*每日开放时间段*/
	static public string[] DAYOPENRULE = new string[]{
		
		"dwDate",		/*日期*/
		
		"dwOpenLimit",		/*开放限制见GROUPOPENRULE定义+DEVRESVSTAT定义*/
		
		"dwOpenPurpose",		/*开放用途*/
		
		"dwBegin",		/*开始时间(HHMM)*/
		
		"dwEnd",		/*结束时间(HHMM)*/
		
		"szFixedTime",		/*预约固定时间点(HHMM)，多个用逗号隔开*/
	 ""};

	/*指定用户组开放时间表*/
	static public string[] PERIODOPENRULEREQ = new string[]{
		
		"dwRuleSN",		/*编号*/
		
		"dwGroupID",		/*组ID*/
		
		"dwStartDay",		/*开始日期(周一到周日 用0-6，节假日用8，制定日期用YYYYMMDD)*/
		 ""};

	/*时间期间开放规则*/
	static public string[] PERIODOPENRULE = new string[]{
		
		"dwStartDay",		/*开始日期(周一到周日 用0-6，节假日用8，制定日期用YYYYMMDD)*/
		
		"dwEndDay",		/*结束日期(同 dwStartDay定义)*/
		
	"DayOpenRule",		/*开放时间段(CUniTable[DAYOPENRULE])*/
	
		"szMemo",		/*开放说明*/
	 ""};

	/*指定用户组开放时间表*/
	static public string[] GROUPOPENRULEREQ = new string[]{
		
		"dwRuleSN",		/*编号*/
		
		"dwGroupID",		/*组ID*/
		 ""};

	/*指定用户组开放时间表*/
	static public string[] GROUPOPENRULE = new string[]{
		
	"szGroup",		/*组信息(CUniStruct[UNIGROUP])*/
	
		"dwPriority",		/*优先级(数字大代表优先级高)*/
		
      "dwOpenLimit",		/*开放限制*/
		
	"PeriodOpenRule",		/*时间期间开放规则(CUniTable[PERIODOPENRULE])*/
	 ""};

	/*指定用户组开放时间表*/
	static public string[] CHANGEGROUPOPENRULE = new string[]{
		
		"dwRuleSN",		/*编号*/
		
		"dwOldGroupID",		/*原组ID*/
		
		"dwGroupID",		/*组ID*/
		
		"dwPriority",		/*优先级(数字大代表优先级高)*/
		
		"dwOpenLimit",		/*开放限制(GROUPOPENRULE)*/
		
	"PeriodOpenRule",		/*时间期间开放规则(CUniTable[PERIODOPENRULE])*/
	 ""};

	/*时间期间开放规则*/
	static public string[] CHANGEPERIODOPENRULE = new string[]{
		
		"dwRuleSN",		/*编号*/
		
		"dwGroupID",		/*组ID*/
		
		"dwOldStartDay",		/*原开始日期(周一到周日 用0-6，节假日用8，制定日期用YYYYMMDD)*/
		
		"dwStartDay",		/*开始日期(周一到周日 用0-6，节假日用8，制定日期用YYYYMMDD)*/
		
		"dwEndDay",		/*结束日期(同 dwStartDay定义)*/
		
	"DayOpenRule",		/*开放时间段(CUniTable[DAYOPENRULE])*/
	
		"szMemo",		/*开放说明*/
	 ""};

	/*设备开放时间表*/
	static public string[] DEVOPENRULE = new string[]{
		
		"dwRuleSN",		/*编号*/
		
		"szRuleName",		/*名称*/
	
	"GroupOpenRule",		/*指定用户组开放时间表(CUniTable[GROUPOPENRULE])*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*当前设备统计信息*/
	static public string[] CURDEVSTAT = new string[]{
		
		"dwChgNum",		/*信息发生改变的统计栏目数*/
		
	"TeachingDevStat",		/*设备信息统计*/
	 ""};

	/*教学设备统计*/
	static public string[] TEACHINGDEVSTAT = new string[]{
		
		"dwTotalNum",		/*总设备数*/
		
		"dwUseNum",		/*正在使用设备数*/
		
		"dwIdleNum",		/*空闲设备数*/
		 ""};

	/*获取教学用设备按节次统计*/
	static public string[] DEVFORTREQ = new string[]{
		
		"dwDate",		/*日期*/
		
		"szRoomIDs",		/*房间ID,多个用逗号隔开*/
	 ""};

	/*设备节次教学使用信息*/
	static public string[] DEVSECINFO = new string[]{
		
		"dwSecIndex",		/*节次编号(1开始编)*/
		
		"szSecName",		/*节次名称*/
	
		"dwResvDevs",		/*预约机器数*/
		
		"dwUseDevs",		/*实际用机数*/
		
		"dwResvUsers",		/*上课总人数*/
		
		"dwRealUsers",		/*实际到课人数*/
		 ""};

	/*获取教学用设备*/
	static public string[] TEACHINGDEVREQ = new string[]{
		
		"dwDevID",		/*设备ID*/
		
		"szDevName",		/*实验设备名称*/
	
		"dwDevSN",		/*设备编号*/
		
		"szLabIDs",		/*实验室ID,多个用逗号隔开*/
	
		"szRoomIDs",		/*房间ID,多个用逗号隔开*/
	
		"szResvIDs",		/*预约ID,多个用逗号隔开*/
	
		"szKindIDs",		/*所属类型,多个用逗号隔开*/
	
		"szClassIDs",		/*所属功能类别ID,多个用逗号隔开*/
	
		"dwDevStat",		/*设备状态*/
		
		"dwRunStat",		/*设备状态*/
		
		"dwUsePurpose",		/*见UNIDEVICE定义*/
		
		"dwCtrlMode",		/*控制方式*/
		
		"dwProperty",		/*设备属性*/
		
		"dwClassKind",		/*类别(见UNIDEVCLS的Kind定义)*/
		
		"dwManGroupID",		/*管理员组ID*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*教学设备信息*/
	static public string[] TEACHINGDEV = new string[]{
		
		"dwDevID",		/*设备ID*/
		
		"dwDevSN",		/*设备编号*/
		
		"szDevName",		/*实验设备名称*/
	
		"dwDevStat",		/*设备状态*/
		
		"dwCtrlMode",		/*控制方式*/
		
		"dwRunStat",		/*设备状态*/
		
		"szKindName",		/*设备名称*/
	
		"szFuncCode",		/*设备功能用途编码*/
	
		"szModel",		/*设备型号*/
	
		"szSpecification",		/*设备规格*/
	
		"dwProperty",		/*设备属性（前16种为UNIDEVKIND定义*/
		
		"dwMaxUsers",		/*最多同时使用人数*/
		
		"dwMinUsers",		/*最少同时使用人数*/
		
		"dwCurUsers",		/*当前使用人数*/
		
		"dwLabID",		/*实验室ID*/
		
		"szLabSN",		/*实验室编号*/
	
		"szLabName",		/*实验室名称*/
	
		"dwRoomID",		/*房间ID*/
		
		"szRoomNo",		/*房间号（不重复,用于关联门禁，格式B-F-N,建筑编号-楼层号-房间编号）*/
	
		"szRoomName",		/*房间名称*/
	
		"dwUsePurpose",		/*同UniFee的dwPurpose*/
		
		"dwCurAccNo",		/*当前用户账号*/
		
		"szCurTrueName",		/*当前用户姓名*/
	
		"dwResvID",		/*预约ID*/
		
		"dwTestItemID",		/*实验项目ID*/
		
		"szTestName",		/*实验名称*/
	
		"dwTestPlanID",		/*实验计划ID*/
		
		"szTestPlanName",		/*实验计划名称*/
	
		"dwTeacherID",		/*教师（帐号）*/
		
		"szTeacherName",		/*教师姓名*/
	
		"dwCourseID",		/*课程ID*/
		
		"szCourseName",		/*课程名称*/
	
		"dwGroupID",		/*上课班级*/
		
		"szGroupName",		/*上课班级名称*/
	
		"dwTeachingTime",		/*教学时间(WWDSSEE)第几周（WW，星期几(D),开始节次（SS）结束节次（EE)*/
		 ""};

	/*获取获奖记录*/
	static public string[] REWARDRECREQ = new string[]{
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
		"dwRewardID",		/*获奖ID*/
		
		"dwRTID",		/*科研实验项目ID*/
		
		"dwHolderID",		/*主持人（帐号）*/
		
		"dwLeaderID",		/*负责人ID*/
		
		"dwOpID",		/*录入员ID*/
		
		"dwRewardType",		/*获奖分类*/
		
		"dwRewardKind",		/*获奖类型*/
		
		"dwRewardLevel",		/*获奖级别*/
		
		"dwDevID",		/*设备ID*/
		
		"szLabIDs",		/*实验室ID,多个用逗号隔开*/
	
		"szRoomIDs",		/*房间ID,多个用逗号隔开*/
	
		"szKindIDs",		/*所属类型,多个用逗号隔开*/
	
		"szClassIDs",		/*所属功能类别ID,多个用逗号隔开*/
	
		"dwProperty",		/*设备属性*/
		
		"dwClassKind",		/*类别(见UNIDEVCLS的Kind定义)*/
		
		"dwUnitPrice",		/*大型仪器价格起点*/
		
      "dwReqProp",		/*请求附加属性*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*获奖使用设备*/
	static public string[] REWARDUSEDEV = new string[]{
		
		"dwRewardID",		/*获奖ID*/
		
		"dwDevID",		/*设备ID*/
		
		"dwKindID",		/*设备类型*/
		
		"dwDevSN",		/*设备编号*/
		
		"szDevName",		/*设备名称*/
	
		"dwRoomID",		/*房间ID*/
		
		"szRoomName",		/*房间名称*/
	
		"dwLabID",		/*实验室ID*/
		
		"szLabName",		/*实验室名称*/
	
		"szAttendantName",		/*值班员姓名*/
	
		"dwStatus",		/*设备管理员确认状态*/
		
		"dwTestTimes",		/*实验次数*/
		
		"dwTestHour",		/*实验学时数*/
		
		"dwRelyRate",		/*依赖度(百分比)，扩展*/
		
		"szMemo",		/*备注*/
	 ""};

	/*获奖记录*/
	static public string[] REWARDREC = new string[]{
		
		"dwRewardID",		/*获奖ID*/
		
		"dwRTID",		/*科研实验项目ID*/
		
		"szRTName",		/*科研实验名称*/
	
		"dwHolderID",		/*主持人（帐号）*/
		
		"szHolderName",		/*主持人姓名*/
	
		"dwLeaderID",		/*负责人ID*/
		
		"szLeaderName",		/*负责人姓名*/
	
		"szMemberNames",		/*获奖人员名单（逗号隔开）*/
	
		"dwRewardDate",		/*获奖日期*/
		
		"dwOpDate",		/*录入日期*/
		
		"dwOpID",		/*录入员ID*/
		
		"szOpName",		/*录入员姓名*/
	
      "dwRewardType",		/*获奖分类*/
		
      "dwRewardKind",		/*获奖类型*/
		
		"szRewardName",		/*奖项名称*/
	
      "dwRewardLevel",		/*获奖级别*/
		
		"szAuthOrg",		/*发证机关*/
	
		"szCertID",		/*证书编号*/
	
		"szExtInfo",		/*扩展信息*/
	
	"UseDev",		/*CUniTable[REWARDUSEDEV]*/
	
		"szMemo",		/*备注*/
	 ""};

	/*获取费用记录*/
	static public string[] COSTRECREQ = new string[]{
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
		"dwCostType",		/*费用类型*/
		
		"szLabIDs",		/*实验室ID,多个用逗号隔开*/
	
		"szRoomIDs",		/*房间ID,多个用逗号隔开*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*费用记录*/
	static public string[] COSTREC = new string[]{
		
		"dwSID",		/*流水号*/
		
      "dwCostType",		/*费用类型*/
		
      "dwPurpose",		/*用途*/
		
		"dwRoomID",		/*房间ID*/
		
		"szRoomName",		/*房间名称*/
	
		"dwSubID",		/*比如设备ID（扩展）*/
		
		"dwCost",		/*费用（元）*/
		
		"szExtInfo",		/*关联财务信息*/
	
		"dwCostDate",		/*发生日期*/
		
		"dwOpTime",		/*录入时间*/
		
		"dwOpID",		/*录入管理员ID*/
		
		"szOpName",		/*录入管理员姓名*/
	
		"szMemo",		/*备注*/
	 ""};

}
	/*结束数据结构*/

	/*开始数据结构*/
public partial class Struct_ST
{

	/*获取门禁集控器的请求包*/
	static public string[] DCSREQ = new string[]{
		
      "dwGetType",		/*方式*/
		
		"szGetKey",		/*条件值*/
	
		"dwDCSKind",		/*门禁集控器类型*/
		 ""};

	/**/
	static public string[] UNIDCS = new string[]{
		
		"dwSN",		/*门禁集控器编号*/
		
      "dwDCSKind",		/*门禁集控器类型*/
		
		"szName",		/*门禁集控器名称*/
	
		"szRoomNo",		/*门禁所在房间号*/
	
		"szIP",		/*门禁集控器IP地址*/
	
      "dwStatus",		/*门禁集控器状态*/
		
		"dwStaSN",		/*管理站点编号*/
		
		"szStaName",		/*站点名称*/
	
		"dwStatChgTime",		/*状态改变时间*/
		
		"szStatInfo",		/*状态描述*/
	
		"szMemo",		/*备注*/
	 ""};

	/*门禁信息表*/
	static public string[] DOORCTRLINFO = new string[]{
		
		"dwCtrlSN",		/*控制器编号*/
		
		"szCtrlModel",		/*控制器类型（比如版本等）*/
	
		"dwCtrlKind",		/*控制器类型见UNIDOORCTRL定义*/
		
		"szCtrlIP",		/*控制器IP地址*/
	 ""};

	/*集控器登录请求*/
	static public string[] DCSLOGINREQ = new string[]{
		
		"szVersion",		/*版本	XX.XX.XXXXXXXX*/
	
		"szIP",		/*IP地址*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*集控器登录响应*/
	static public string[] DCSLOGINRES = new string[]{
		
		"dwSessionID",		/*服务器分配的SessionID*/
		
	"SrvVer",		/*UNIVERSION 结构*/
	
		"szCurTime",		/*服务器时间 YYYY-MM-DD HH:MM:SS*/
	
		"dwDCSSN",		/*控制台编号*/
		
		"szDCSName",		/*控制台名称*/
	
		"szMemo",		/*说明信息*/
	
	"szManCtrls",		/*管理门禁列表CUniTable[DOORCTRLINFO]*/
	 ""};

	/*集控器退出请求*/
	static public string[] DCSLOGOUTREQ = new string[]{
		
		"dwDCSSN",		/*集控器编号*/
		
		"szMemo",		/*说明信息*/
	 ""};

	/*控制器状态*/
	static public string[] DOORCTRLSTAT = new string[]{
		
		"dwCtrlSN",		/*控制器编号*/
		
		"dwStatus",		/*DCSSTAT_TROUBLE,DCSSTAT_DOOROPEN,DCSSTAT_DOORCLOSED*/
		
		"szMemo",		/*备注*/
	 ""};

	/*集控器定时通信请求*/
	static public string[] DCSPULSEREQ = new string[]{
		
		"dwDCSSN",		/*控制台编号*/
		
	"szControllerStat",		/*控制器状态信息CUniTable[DOORCTRLSTAT]*/
	 ""};

	/*集控器定时通信应答*/
	static public string[] DCSPULSERES = new string[]{
		
		"dwChanged",		/*集控器是否改变*/
		
		"szMemo",		/*备注*/
	 ""};

	/*门禁刷卡请求*/
	static public string[] DOORCARDREQ = new string[]{
		
		"dwDCSSN",		/*集控器编号*/
		
		"dwCtrlSN",		/*控制器编号*/
		
      "dwCardMode",		/*内外刷卡及卡号类别(3字节，4字节等)*/
		
		"szCardNo",		/*卡号*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*门禁刷卡响应*/
	static public string[] DOORCARDRES = new string[]{
		
      "dwUserKind",		/*用户种类*/
		
		"dwSoundSN",		/*播放声音*/
		
		"dwDeadLine",		/*允许截止时间*/
		
		"szPID",		/*学工号*/
	
		"szCardNo",		/*卡号*/
	
		"szTrueName",		/*姓名*/
	
		"szDispInfo",		/*显示信息*/
	 ""};

	/*手机开门请求*/
	static public string[] MOBILEOPENDOORREQ = new string[]{
		
		"szMSN",		/*MSN*/
	
		"szLogonName",		/*登录名*/
	
		"szPassword",		/*密码*/
	
		"szIP",		/*IP地址*/
	
      "dwProperty",		/*扩展属性*/
		
		"dwDCSSN",		/*集控器编号*/
		
		"dwCtrlSN",		/*控制器编号*/
		
		"dwCardMode",		/*内外刷卡*/
		
		"szMemo",		/*说明信息*/
	 ""};

	/*手机开门响应*/
	static public string[] MOBILEOPENDOORRES = new string[]{
		
		"dwUserKind",		/*用户种类(DOORCARDRES定义)*/
		
      "dwFailedType",		/*失败类型*/
		
		"szPID",		/*学工号*/
	
		"szTrueName",		/*姓名*/
	
		"szDispInfo",		/*显示信息*/
	 ""};

	/*获取门禁控制器的请求包*/
	static public string[] DOORCTRLREQ = new string[]{
		
      "dwGetType",		/*方式*/
		
		"szGetKey",		/*条件值*/
	
		"dwDCSKind",		/*门禁集控器类型*/
		 ""};

	/**/
	static public string[] UNIDOORCTRL = new string[]{
		
		"dwDCSSN",		/*集控器编号*/
		
		"dwDCSKind",		/*门禁集控器类型*/
		
		"szDCSName",		/*门禁集控器名称*/
	
		"szDCSIP",		/*门禁集控器IP地址*/
	
		"dwCtrlSN",		/*控制器编号*/
		
      "dwCtrlKind",		/*控制器类型*/
		
		"szRoomNo",		/*房间号*/
	
		"szCtrlName",		/*控制器名称*/
	
		"szCtrlModel",		/*控制器型号（比如版本等）*/
	
		"szCtrlIP",		/*控制器IP地址*/
	
		"dwStaSN",		/*所属站点编号*/
		
		"szStaName",		/*站点名称*/
	
		"dwStatus",		/*门禁状态见 UNIDCS定义*/
		
		"dwStatChgTime",		/*状态改变时间*/
		
		"szStatInfo",		/*状态描述*/
	
		"szMODIP",		/*允许手机开门IP段*/
	
		"szMemo",		/*备注*/
	
		"dwRoomID",		/*房间ID*/
		
		"szRoomName",		/*房间名称*/
	
		"szFloorNo",		/*所在楼层*/
	
		"dwBuildingID",		/*楼宇ID*/
		
		"szBuildingNo",		/*大楼编号(*/
	
		"szBuildingName",		/*大楼名称(*/
	 ""};

	/*给门禁控制器发命令*/
	static public string[] DOORCTRLCMD = new string[]{
		
		"dwCtrlSN",		/*控制器编号*/
		
		"dwCmd",		/*控制命令(参考DEVCTRLINFO::dwCmd)*/
		
		"szParam",		/*控制参数*/
	
		"szMemo",		/*备注*/
	 ""};

}
	/*结束数据结构*/

	/*开始数据结构*/
public partial class Struct_ST
{

	/*获取组信息的请求包*/
	static public string[] GROUPREQ = new string[]{
		
		"dwGroupID",		/*组ID*/
		
		"szName",		/*名称*/
	
		"dwKind",		/*类型*/
		
		"dwAccNo",		/*组成员帐号*/
		
		"dwMinDeadLine",		/*最小截止日期*/
		
		"dwMaxDeadLine",		/*最大截止日期*/
		
      "dwReqProp",		/*请求附加属性*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*组成员*/
	static public string[] GROUPMEMBER = new string[]{
		
		"dwGroupID",		/*组编号*/
		
      "dwKind",		/*成员类别*/
		
		"dwMemberID",		/*成员ID*/
		
		"szName",		/*成员名称*/
	
		"dwBeginDate",		/*开始日期*/
		
		"dwEndDate",		/*截止日期*/
		
      "dwStatus",		/*状态（前8种留出用于CHECKINFO定义的管理员审核状态)*/
		
		"szExtInfo",		/*补充说明*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/**/
	static public string[] UNIGROUP = new string[]{
		
		"dwGroupID",		/*组ID*/
		
		"szName",		/*名称*/
	
      "dwKind",		/*类型*/
		
		"dwMaxUsers",		/*最大用户数*/
		
		"dwMinUsers",		/*最少用户数*/
		
		"dwDeadLine",		/*截止日期*/
		
		"dwEnrollDeadline",		/*申请加入截止日*/
		
		"dwAssociateID",		/*扩展用，关联ID，比如预约组的预约ID等*/
		
		"szGroupURL",		/*组简介*/
	
	"szMembers",		/*成员明细表(CUniTable<GROUPMEMBER>)*/
	
		"szMemo",		/*备注*/
	 ""};

	/*获取组成员明细的请求包*/
	static public string[] GROUPMEMDETAILREQ = new string[]{
		
		"dwGroupKind",		/*组类别*/
		
		"dwGroupID",		/*组编号*/
		
		"dwStatus",		/*审核状态*/
		
		"dwAccNo",		/*成员账号*/
		
      "dwReqProp",		/*请求附加属性*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*组成员明细*/
	static public string[] GROUPMEMDETAIL = new string[]{
		
		"dwGroupID",		/*组编号*/
		
		"dwBeginDate",		/*开始日期*/
		
		"dwEndDate",		/*截止日期*/
		
		"dwStatus",		/*审核状态*/
		
		"szExtInfo",		/*补充说明*/
	
		"dwAccNo",		/*成员账号*/
		
		"szPID",		/*学工号*/
	
		"szTrueName",		/*姓名*/
	
		"dwClassID",		/*班级ID*/
		
		"szClassName",		/*班级*/
	
		"dwDeptID",		/*部门ID*/
		
		"szDeptName",		/*部门*/
	
		"dwMajorID",		/*专业ID*/
		
		"szMajorName",		/*专业*/
	
		"dwSex",		/*性别见UniCommon.h*/
		
		"dwIdent",		/*身份 见UniCommon.h*/
		
		"dwEnrolYear",		/*入学年份(XX级)*/
		
		"dwSchoolYears",		/*学制*/
		
		"szTel",		/*电话*/
	
		"szHandPhone",		/*手机*/
	
		"szEmail",		/*电邮*/
	
		"dwTutorID",		/*导师账号*/
		
		"szTutorName",		/*导师姓名*/
	 ""};

}
	/*结束数据结构*/

	/*开始数据结构*/
public partial class Struct_ST
{

	/*获取预约信息的请求包*/
	static public string[] RESVREQ = new string[]{
		
		"dwResvID",		/*预约号*/
		
		"dwMemberKind",		/*成员类别*/
		
		"dwOwner",		/*预约人(所有者)*/
		
		"dwMemberID",		/*成员ID*/
		
		"dwDevID",		/*设备ID*/
		
		"dwManagerID",		/*管理员ID*/
		
		"dwAccNo",		/*成员账号，获取成员相关的所有实验安排*/
		
		"dwTestPlanID",		/*获取TestPlan相关的所有实验安排*/
		
		"dwCourseID",		/*获取课程相关的所有实验安排*/
		
		"dwTestItemID",		/*获取TestItem相关的所有实验安排*/
		
		"dwCheckStat",		/*确认状态*/
		
		"dwUnNeedStat",		/*不包含状态*/
		
		"dwUseMode",		/*使用方法*/
		
		"dwPurpose",		/*预约用途*/
		
		"dwDevKind",		/*设备类型*/
		
		"dwClassKind",		/*类别(见UNIDEVCLS的Kind定义)*/
		
		"dwBeginDate",		/*预约开始日期*/
		
		"dwEndDate",		/*预约结束日期*/
		
		"szRoomNos",		/*房间编号,多个用逗号隔开*/
	
		"dwResvGroupID",		/*预约组ID*/
		
      "dwStatFlag",		/*状态标志*/
		
		"dwKind",		/*预约类型*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*预约表*/
	static public string[] UNIRESERVE = new string[]{
		
		"dwResvID",		/*预约号*/
		
      "dwMemberKind",		/*成员类别*/
		
      "dwUseMode",		/*使用方法*/
		
      "dwPurpose",		/*预约用途*/
		
		"dwKind",		/*预约类型(分类统计用)*/
		
		"dwOwner",		/*预约人(所有者)*/
		
		"szOwnerName",		/*预约人姓名*/
	
		"dwMemberID",		/*成员ID*/
		
		"szMemberName",		/*成员名称*/
	
		"dwResvRuleSN",		/*关联预约规则*/
		
		"dwFeeSN",		/*关联的收费标准*/
		
		"dwOpenRuleSN",		/*关联开放时间表*/
		
		"dwFeeMode",		/*收费方式*/
		
		"dwUseFee",		/*预约总费用*/
		
		"dwPreDate",		/*预约开始日期*/
		
		"dwOccurTime",		/*预约发生时间*/
		
		"dwBeginTime",		/*预约开始时间*/
		
		"dwEndTime",		/*预约结束时间*/
		
		"dwYearTerm",		/*学期编号*/
		
		"dwTeachingTime",		/*教学时间(WWDSSEE)第几周（WW，星期几(D),开始节次（SS）结束节次（EE)*/
		
		"dwCheckTime",		/*审查时间*/
		
		"dwAdvanceCheckTime",		/*提前审查时间*/
		
		"dwResvGroupID",		/*组ID(多时段预约组ID相同，其余为预约ID)*/
		
		"dwCheckKinds",		/*审核类型(参考CHECKTYPE定义，可多个）*/
		
		"dwLabID",		/*实验室ID*/
		
		"szLabName",		/*实验室名称*/
	
	"ResvDev",		/*预约设备明细表(CUniTable<RESVDEV>)*/
	
      "dwStatus",		/*预约状态(包括审查，是否生效，是否已取消等)*/
		
      "dwProperty",		/*预约属性*/
		
		"dwTestItemID",		/*实验项目ID*/
		
		"szTestName",		/*实验名称*/
	
		"dwTestHour",		/*实验学时数*/
		
		"dwSignTime",		/*教师签到时间*/
		
		"dwResvDevs",		/*预约机器数*/
		
		"dwUseDevs",		/*实际用机数*/
		
		"dwResvUsers",		/*上课总人数*/
		
		"dwRealUsers",		/*实际到课人数*/
		
		"szApplicationURL",		/*提交的申请材料连接*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*获取科研实验预约的请求包*/
	static public string[] RTRESVREQ = new string[]{
		
		"dwResvID",		/*预约号*/
		
		"dwDevID",		/*设备ID*/
		
		"dwDevKind",		/*设备类型*/
		
		"dwClassKind",		/*类别(见UNIDEVCLS的Kind定义)*/
		
		"dwRTID",		/*科研实验计划ID*/
		
		"dwHolderID",		/*主持人（帐号）*/
		
		"dwMAccNo",		/*组成员帐号*/
		
		"dwLeaderID",		/*负责人ID*/
		
		"dwCheckStat",		/*确认状态*/
		
		"dwUnNeedStat",		/*不包含状态*/
		
		"dwUseMode",		/*使用方法*/
		
		"dwPurpose",		/*预约用途*/
		
		"dwBeginDate",		/*预约开始日期*/
		
		"dwEndDate",		/*预约结束日期*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*预约样品表*/
	static public string[] RESVSAMPLE = new string[]{
		
		"dwResvID",		/*预约ID*/
		
		"dwSampleSN",		/*样品编号*/
		
		"szSampleName",		/*样品名称*/
	
		"szUnitName",		/*计费单位*/
	
		"dwUnitFee",		/*单价*/
		
		"dwSampleNum",		/*数量*/
		
		"szMemo",		/*说明信息*/
	 ""};

	/*预约表*/
	static public string[] RTRESV = new string[]{
		
		"dwResvID",		/*预约号*/
		
		"szTestName",		/*科研实验名称*/
	
		"dwUseMode",		/*使用方法*/
		
		"dwPurpose",		/*预约用途*/
		
		"dwProperty",		/*预约属性*/
		
		"dwStatus",		/*预约状态(包括审查，是否生效，是否已取消等)*/
		
		"dwOwner",		/*预约人(创建者)*/
		
		"szOwnerName",		/*预约人姓名*/
	
		"dwResvRuleSN",		/*关联预约规则*/
		
		"dwOpenRuleSN",		/*关联开放时间表*/
		
		"dwFeeSN",		/*费率SN*/
		
		"dwFeeMode",		/*收费方式*/
		
		"dwUseFee",		/*预约总费用*/
		
		"dwPreDate",		/*预约开始日期*/
		
		"dwOccurTime",		/*预约发生时间*/
		
		"dwBeginTime",		/*预约开始时间*/
		
		"dwEndTime",		/*预约结束时间*/
		
		"dwCheckTime",		/*审查时间*/
		
		"dwAdvanceCheckTime",		/*提前审查时间*/
		
		"dwDevID",		/*设备ID*/
		
		"dwDevKind",		/*设备类型*/
		
		"dwDevSN",		/*设备编号*/
		
		"szDevName",		/*设备名称*/
	
		"dwRoomID",		/*房间ID*/
		
		"szRoomName",		/*房间名称*/
	
		"dwLabID",		/*实验室ID*/
		
		"szLabName",		/*实验室名称*/
	
		"dwBuildingID",		/*楼宇ID*/
		
		"szBuildingNo",		/*大楼编号(*/
	
		"szBuildingName",		/*大楼名称(*/
	
		"dwCampusID",		/*校区ID*/
		
		"szCampusName",		/*校区名称*/
	
		"dwRTID",		/*科研实验项目ID*/
		
		"szRTName",		/*科研实验名称*/
	
		"dwHolderID",		/*主持人（帐号）*/
		
		"szHolderName",		/*主持人姓名*/
	
		"dwUserDeptID",		/*使用人部门ID*/
		
		"szUserDeptName",		/*使用人部门*/
	
		"dwLeaderID",		/*负责人ID*/
		
		"szLeaderName",		/*负责人姓名*/
	
		"dwGroupID",		/*组ID*/
		
		"dwManID",		/*管理员ID*/
		
		"szManName",		/*管理员姓名*/
	
		"dwEstimatedTime",		/*预估时间(分钟)*/
		
		"dwTestTimes",		/*实验次数*/
		
		"dwRealUseTime",		/*实际实验时间(分钟)*/
		
		"dwReceivableCost",		/*应缴费用*/
		
		"dwRealCost",		/*实际缴纳费用*/
		
		"dwPrepayment",		/*预收款金额*/
		
	"ResvSample",		/*CUniTable[RESVSAMPLE](获取一条预约时才返回)*/
	
		"szConsumables",		/*所需耗材清单*/
	
		"dwBeforePersons",		/*前面排队人数*/
		
		"szFundsNo",		/*经费卡编号*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*科研实验审核*/
	static public string[] RTRESVCHECK = new string[]{
		
		"dwCheckStat",		/*管理员审核状态(定义在ADMINCHECK)*/
		
		"szCheckDetail",		/*审查说明*/
	
	"RTResv",		/*CUniStruct[RTRESV]*/
	
	"BillInfo",		/*CUniTable[RTBILL]*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*科研实验账单审核*/
	static public string[] RTPREPAY = new string[]{
		
		"dwResvID",		/*预约号*/
		
		"dwPrepayment",		/*预收款金额*/
		
		"szMemo",		/*说明信息*/
	 ""};

	/*获取科研实验账单的请求包*/
	static public string[] RTRESVBILLREQ = new string[]{
		
		"dwResvID",		/*预约号*/
		 ""};

	/*科研实验账单*/
	static public string[] RTRESVBILL = new string[]{
		
		"dwResvID",		/*预约号*/
		
		"dwPrepayment",		/*预收款金额*/
		
	"BillInfo",		/*CUniTable[RTBILL]*/
	 ""};

	/*科研实验账单*/
	static public string[] RTBILL = new string[]{
		
		"dwSID",		/*流水号*/
		
		"dwResvID",		/*预约号*/
		
		"dwFeeType",		/*收费类别(FEEDETAIL定义)*/
		
		"dwUnitFee",		/*费率*/
		
		"dwReceivableCost",		/*应缴费用*/
		
		"dwRealCost",		/*实际缴纳费用*/
		
		"dwPayKind",		/*缴费方式(UNIBILL定义)*/
		
		"dwStatus",		/*CHECKINFO定义的管理员审核状态+UNIBILL定义*/
		
		"szMemo",		/*说明信息*/
	 ""};

	/*科研实验账单审核*/
	static public string[] RTBILLCHECK = new string[]{
		
		"dwResvID",		/*预约号*/
		
	"BillInfo",		/*CUniTable[RTBILL]*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*科研实验账单结算*/
	static public string[] RTBILLSETTLE = new string[]{
		
		"dwResvID",		/*预约号*/
		
		"dwPurpose",		/*预约用途(见UNIRESERVE定义USEBY_XXX）*/
		
		"dwPayKind",		/*缴费方式*/
		
		"dwTotalCost",		/*缴费合计*/
		
		"szFundsNo",		/*经费卡编号（多个用逗号隔开)*/
	
		"szCostInfo",		/*扣费信息*/
	
	"BillInfo",		/*CUniTable[RTBILL]*/
	
	"ResvSample",		/*CUniTable[RESVSAMPLE]*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*科研实验账单入账*/
	static public string[] RTBILLRECEIVE = new string[]{
		
		"dwResvID",		/*预约号*/
		
		"dwReceiveDate",		/*入账日期*/
		
		"dwTotalCost",		/*缴费合计*/
		
		"dwTestFee",		/*分析测试费*/
		
		"dwOpenFundFee",		/*开放基金*/
		
		"dwServiceFee",		/*劳务费*/
		
		"szMemo",		/*说明信息*/
	 ""};

	/*免登录预约*/
	static public string[] ANONYMOUSRESV = new string[]{
		
		"dwResvID",		/*预约号*/
		
		"dwPurpose",		/*预约用途*/
		
		"dwPreDate",		/*预约开始日期*/
		
		"dwBeginTime",		/*预约开始时间*/
		
		"dwEndTime",		/*预约结束时间*/
		
		"dwYearTerm",		/*学期编号*/
		
		"dwTeachingTime",		/*教学时间(WWDSSEE)第几周（WW，星期几(D),开始节次（SS）结束节次（EE)*/
		
		"dwLabID",		/*实验室ID*/
		
		"szLabName",		/*实验室名称*/
	
		"szTestName",		/*实验名称*/
	
	"ResvDev",		/*预约设备明细表(CUniTable<RESVDEV>)*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*全体学生预约*/
	static public string[] ALLUSERRESV = new string[]{
		
		"dwResvID",		/*预约号*/
		
		"dwPurpose",		/*预约用途*/
		
		"dwPreDate",		/*预约开始日期*/
		
		"dwBeginTime",		/*预约开始时间*/
		
		"dwEndTime",		/*预约结束时间*/
		
		"dwYearTerm",		/*学期编号*/
		
		"dwTeachingTime",		/*教学时间(WWDSSEE)第几周（WW，星期几(D),开始节次（SS）结束节次（EE)*/
		
		"dwLabID",		/*实验室ID*/
		
		"szLabName",		/*实验室名称*/
	
		"szTestName",		/*实验名称*/
	
	"ResvDev",		/*预约设备明细表(CUniTable<RESVDEV>)*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*获取预约列表用于网站显示的请求包*/
	static public string[] RESVSHOWREQ = new string[]{
		
		"dwBeginDate",		/*预约开始日期*/
		
		"dwEndDate",		/*预约结束日期*/
		
		"dwDevKind",		/*设备类型*/
		
		"dwClassKind",		/*类别(见UNIDEVCLS的Kind定义)*/
		
		"dwCheckStat",		/*确认状态*/
		
		"dwPurpose",		/*预约用途*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*预约表*/
	static public string[] RESVSHOW = new string[]{
		
		"dwResvID",		/*预约号*/
		
		"dwPurpose",		/*预约用途*/
		
		"dwKind",		/*预约类型(分类统计用)*/
		
		"dwOwner",		/*预约人(所有者)*/
		
		"szOwnerName",		/*预约人姓名*/
	
		"dwPreDate",		/*预约开始日期*/
		
		"dwBeginTime",		/*预约开始时间*/
		
		"dwEndTime",		/*预约结束时间*/
		
		"dwStatus",		/*预约状态(包括审查，是否生效，是否已取消等)*/
		
		"dwProperty",		/*预约属性*/
		
		"szTestName",		/*实验名称*/
	
		"szRoomNo",		/*房间号*/
	
		"dwDevSN",		/*设备编号*/
		
		"szDevName",		/*设备名*/
	 ""};

	/*获取教学预约列表的请求包*/
	static public string[] TEACHINGRESVREQ = new string[]{
		
		"dwBeginDate",		/*预约开始日期*/
		
		"dwEndDate",		/*预约结束日期*/
		
		"dwTeacherID",		/*教师（帐号）*/
		
		"dwAccNo",		/*成员账号，获取成员相关的所有实验安排*/
		
		"dwTestPlanID",		/*获取TestPlan相关的所有实验安排*/
		
		"dwCourseID",		/*获取课程相关的所有实验安排*/
		
		"dwTestItemID",		/*获取TestItem相关的所有实验安排*/
		
		"dwYearTerm",		/*学期编号*/
		
		"dwPlanKind",		/*计划类型*/
		
		"dwResvStat",		/*预约状态*/
		
		"szRoomNo",		/*房间号*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*教学预约*/
	static public string[] TEACHINGRESV = new string[]{
		
		"dwTestItemID",		/*实验项目ID*/
		
		"szTestName",		/*实验名称*/
	
		"dwTestPlanID",		/*实验计划ID*/
		
		"szTestPlanName",		/*实验计划名称*/
	
		"dwPlanKind",		/*计划类型*/
		
		"dwTeacherID",		/*教师（帐号）*/
		
		"szTeacherName",		/*教师姓名*/
	
		"dwCourseID",		/*课程ID*/
		
		"szCourseName",		/*课程名称*/
	
		"dwGroupID",		/*上课班级*/
		
		"szGroupName",		/*上课班级名称*/
	
		"dwGroupUsers",		/*组用户数*/
		
		"dwResvID",		/*预约ID*/
		
		"dwResvStat",		/*预约状态*/
		
		"dwPreDate",		/*预约日期*/
		
		"dwBeginTime",		/*预约开始时间*/
		
		"dwEndTime",		/*预约结束时间*/
		
		"dwTeachingTime",		/*教学时间(WWDSSEE)第几周（WW，星期几(D),开始节次（SS）结束节次（EE)*/
		
		"dwLabID",		/*实验室ID*/
		
		"szLabName",		/*实验室名称*/
	
	"ResvDev",		/*预约设备明细表(CUniTable<RESVDEV>)*/
	
		"dwCurUsers",		/*当前人数（仅对目前生效的预约有效）*/
		 ""};

	/*放假调课*/
	static public string[] HOLIDAYSHIFT = new string[]{
		
		"dwOldDate",		/*原上课日期*/
		
		"dwNewDate",		/*新上课日期*/
		
		"dwNoticeFlag",		/*通知标志*/
		
		"szMemo",		/*说明*/
	 ""};

	/*预约组成员*/
	static public string[] RESVMEMBER = new string[]{
		
		"dwResvID",		/*预约号*/
		
		"dwAccNo",		/*成员账号*/
		
		"szTrueName",		/*姓名*/
	 ""};

	/*预约开始信息*/
	static public string[] RESVSTARTINFO = new string[]{
		
		"dwForceLogoff",		/*是否注销用户*/
		
		"dwNoLogon",		/*免登录标志*/
		
		"dwPurpose",		/*预约目的*/
		
		"szClassName",		/*预约班级*/
	
		"szUsers",		/*预约学号*/
	 ""};

	/*预约开始信息*/
	static public string[] RESVENDINFO = new string[]{
		
      "dwEndCmd",		/*结束方式*/
		
		"szMsg",		/*信息*/
	 ""};

	/*自动预约请求，系统自动分配设备并暂时锁定，等待用户确认*/
	static public string[] AUTORESVREQ = new string[]{
		
		"dwOwner",		/*预约人(所有者)*/
		
		"dwKind",		/*预约成员类别*/
		
		"dwPurpose",		/*预约用途*/
		
		"dwPreDate",		/*预约发生日期*/
		
		"dwEarlyBeginTime",		/*预约最早开始时间*/
		
		"dwLateBeginTime",		/*预约最晚开始时间*/
		
		"dwUseMin",		/*使用时长*/
		
		"dwRoomID",		/*房间ID*/
		
		"dwDevKind",		/*设备类型*/
		
		"szUserLimit",		/*使用者约束（暂未使用)*/
	
		"szDateLimit",		/*时间约束（暂未使用)*/
	
		"szDevLimit",		/*设备约束（暂未使用)*/
	 ""};

	/*预约设备明细*/
	static public string[] RESVDEV = new string[]{
		
		"dwLabID",		/*实验室ID*/
		
		"szLabName",		/*实验室名称*/
	
		"dwRoomID",		/*房间ID*/
		
		"szRoomNo",		/*房间编号*/
	
		"szRoomName",		/*房间名称*/
	
		"dwDevKind",		/*设备类型*/
		
		"dwDevNum",		/*设备数量*/
		
		"dwDevStart",		/*设备开始编号*/
		
		"dwDevEnd",		/*设备结束编号*/
		
		"szDevName",		/*设备名*/
	
		"szModel",		/*设备型号*/
	
		"szSpecification",		/*设备规格*/
	
		"dwDevClsKind",		/*设备类别分类*/
		
		"szMemo",		/*备注*/
	 ""};

	/*预约时间段明细*/
	static public string[] RESVTIMEREQ = new string[]{
		
		"dwResvID",		/*预约号*/
		
		"dwResvGroupID",		/*组ID*/
		 ""};

	/*预约时间段明细*/
	static public string[] RESVTIME = new string[]{
		
		"dwResvID",		/*预约号*/
		
		"dwResvGroupID",		/*组ID*/
		
		"dwPreDate",		/*预约发生日期*/
		
		"dwBeginTime",		/*预约开始时间*/
		
		"dwEndTime",		/*预约结束时间*/
		
		"dwTeachingTime",		/*教学时间(WWDSSEE)第几周（WW，星期几(D),开始节次（SS）结束节次（EE)*/
		
		"szMemo",		/*备注*/
	 ""};

	/*预约费用核算*/
	static public string[] RESVCOSTADJUST = new string[]{
		
		"dwResvID",		/*预约号*/
		
		"dwAdjustFee",		/*调整费用*/
		
		"dwSampleFee",		/*样品费*/
		
		"dwConfirmorID",		/*管理员帐号*/
		
		"szConfirmor",		/*管理员姓名*/
	
		"szMemo",		/*备注（核算费用调整的说明)*/
	 ""};

	/*预约费用结算*/
	static public string[] RESVCHECKOUT = new string[]{
		
		"dwResvID",		/*预约号*/
		
		"dwBillID",		/*账单号*/
		
		"dwRealCheckFee",		/*实际结算费用*/
		
		"dwConfirmorID",		/*审查管理员帐号*/
		
		"szConfirmor",		/*审查管理员姓名*/
	
		"szMemo",		/*备注（结算附加费用的说明)*/
	 ""};

	/*预约结束时间*/
	static public string[] RESVENDTIME = new string[]{
		
		"dwResvID",		/*预约ID*/
		
		"dwEndTime",		/*结束时间*/
		 ""};

	/*获取设备预约信息的请求包*/
	static public string[] DEVRESVREQ = new string[]{
		
      "dwGetType",		/*方式*/
		
		"szGetKey",		/*条件值*/
	
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*设备预约信息*/
	static public string[] DEVRESVINFO = new string[]{
		
		"dwDevID",		/*设备ID*/
		
		"dwResvID",		/*预约号*/
		
		"dwBeginTime",		/*预约开始时间*/
		
		"dwEndTime",		/*预约结束时间*/
		 ""};

	/*获取作息时间表请求包*/
	static public string[] CTSREQ = new string[]{
		
		"dwSN",		/*作息表编号*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*作息时间表*/
	static public string[] CLASSTIMETABLE = new string[]{
		
		"dwSN",		/*作息表编号*/
		
		"dwSecIndex",		/*节次编号(1开始编)*/
		
		"szSecName",		/*节次名称*/
	
		"dwBeginTime",		/*开始时间(HHMM)*/
		
		"dwEndTime",		/*结束时间(HHMM)*/
		 ""};

	/*获取学期信息的请求包*/
	static public string[] TERMREQ = new string[]{
		
		"dwYearTerm",		/*学期编号（20091001 2009－10年第一学期）*/
		
		"dwStatus",		/*学期状态*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/**/
	static public string[] UNITERM = new string[]{
		
		"dwYearTerm",		/*学期编号（20091001 2009－10年第一学期）*/
		
      "dwStatus",		/*学期状态*/
		
		"dwBeginDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
		"dwFirstWeekDays",		/*起始周天数*/
		
		"dwTotalWeeks",		/*一共有多少周*/
		
		"dwSecNum",		/*每日节次总数*/
		
	"szCTS1",		/*作息时间表1(CUniTable<CLASSTIMETABLE>)*/
	
		"dwCTS1Begin",		/*时间表1开始生效日期*/
		
		"dwCTS1End",		/*时间表1结束生效日期*/
		
	"szCTS2",		/*作息时间表2(CUniTable<CLASSTIMETABLE>)*/
	
		"dwCTS2Begin",		/*时间表2开始生效日期*/
		
		"dwCTS2End",		/*时间表2结束生效日期*/
		
		"szMemo",		/*备注*/
	 ""};

	/*获取课程的请求包*/
	static public string[] COURSEREQ = new string[]{
		
		"dwCourseID",		/*课程ID*/
		
		"szCourseCode",		/*课程代码*/
	
		"szCourseName",		/*课程名称*/
	
		"dwOwnerDept",		/*开课学院*/
		
		"dwMajorID",		/*所属专业ID*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*课程*/
	static public string[] UNICOURSE = new string[]{
		
		"dwCourseID",		/*课程ID*/
		
		"szCourseCode",		/*课程代码*/
	
		"szCourseName",		/*课程名称*/
	
      "dwCourseProperty",		/*课程属性*/
		
		"dwOwnerDept",		/*开课学院*/
		
		"szDeptName",		/*开课学院名称*/
	
		"dwMajorID",		/*所属专业ID*/
		
		"szMajorName",		/*所属专业名称*/
	
		"szType",		/*课程类别（选修，必修等）*/
	
		"dwHardCoef",		/*难度系数*/
		
		"dwCreditHour",		/*学分*/
		
		"dwTheoryHour",		/*理论学时数*/
		
		"dwTestHour",		/*实验学时数*/
		
		"dwPracticeHour",		/*实践学时数*/
		
		"dwTestNum",		/*实验次数*/
		
		"szMemo",		/*备注*/
	 ""};

	/*获取实验计划请求包*/
	static public string[] TESTPLANREQ = new string[]{
		
      "dwGetType",		/*方式*/
		
		"szGetKey",		/*条件值*/
	
		"dwYearTerm",		/*学期编号*/
		
		"dwKind",		/*实验计划类型*/
		
		"dwStatus",		/*实验计划状态*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/**/
	static public string[] UNITESTPLAN = new string[]{
		
		"dwYearTerm",		/*学期编号*/
		
		"dwTestPlanID",		/*实验计划ID*/
		
		"szTestPlanName",		/*实验计划名称*/
	
		"szAcademicSubjectCode",		/*所属学科编码*/
	
      "dwTesteeKind",		/*实验者类别*/
		
		"dwTeacherID",		/*教师（帐号）*/
		
		"szTeacherName",		/*教师姓名*/
	
		"dwTeacherDeptID",		/*教师所属部门ID*/
		
		"szTeacherDeptName",		/*教师所属部门*/
	
		"dwCourseID",		/*课程ID*/
		
		"szCourseCode",		/*课程代码*/
	
		"szCourseName",		/*课程名称*/
	
		"dwCourseProperty",		/*课程属性*/
		
		"dwTheoryHour",		/*理论学时数*/
		
		"dwTestHour",		/*实验学时数*/
		
		"dwPracticeHour",		/*实践学时数*/
		
		"dwTestNum",		/*实验项目数*/
		
		"dwGroupID",		/*上课班级*/
		
		"szGroupName",		/*上课班级名称*/
	
		"dwMaxUsers",		/*最大用户数*/
		
		"dwMinUsers",		/*最少用户数*/
		
		"dwGroupUsers",		/*上课学生数*/
		
		"dwEnrollDeadline",		/*申请加入截止日*/
		
      "dwKind",		/*实验计划类型*/
		
      "dwStatus",		/*实验计划状态（前8种留出用于CHECKINFO定义的管理员审核状态)*/
		
		"szTestPlanURL",		/*详细描述*/
	
		"dwTotalTestHour",		/*总学时数*/
		
		"dwResvTestHour",		/*已预约学时数*/
		
		"dwDoneTestHour",		/*已完成学时数*/
		
		"szUsableLab",		/*可用实验室ID（多个用逗号隔开)*/
	
		"szMemo",		/*备注*/
	 ""};

	/*获取实验项目卡请求包*/
	static public string[] TESTCARDREQ = new string[]{
		
		"dwTestCardID",		/*实验项目卡ID*/
		
		"szTestName",		/*实验名称（模糊匹配)*/
	
		"dwTestClass",		/*实验类别*/
		
		"dwTestKind",		/*实验类型*/
		
		"dwRequirement",		/*实验要求*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*实验项目卡*/
	static public string[] TESTCARD = new string[]{
		
		"dwTestCardID",		/*实验项目卡ID*/
		
		"szTestSN",		/*实验编号*/
	
		"szTestName",		/*实验名称*/
	
		"szCategoryName",		/*类别名(*/
	
		"dwGroupPeopleNum",		/*每组人数*/
		
		"dwTestHour",		/*本实验项目学时数*/
		
      "dwTestClass",		/*实验类别（是按实验项目任务本身的性质分类的填基础、技术（或专业）基础、专业、科研、生产、其他（为毕业论文、毕业设计、技术开发和社会服务而开的实验））*/
		
      "dwTestKind",		/*实验类型（演示、验证、综合、设计）*/
		
      "dwRequirement",		/*实验要求（填必修、选修、其他（科研测试、生产服务、技术开发等））*/
		
		"szConstraints",		/*约束条件（比如时间限制和需要的设备等,格式有专门文件定义*/
	
		"szTestItemURL",		/*详细描述*/
	
		"szMemo",		/*备注*/
	 ""};

	/*获取实验项目请求包*/
	static public string[] TESTITEMREQ = new string[]{
		
      "dwGetType",		/*方式*/
		
		"szGetKey",		/*条件值*/
	
		"dwStatus",		/*状态（前8种留出用于审核状态)*/
		
		"dwCourseID",		/*课程ID*/
		
		"dwTeacherID",		/*教师（帐号）*/
		
		"dwAccNo",		/*帐号*/
		
		"dwPlanKind",		/*计划类型*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*实验安排项目卡*/
	static public string[] UNITESTITEM = new string[]{
		
		"dwTestItemID",		/*实验项目ID*/
		
		"dwTestPlanID",		/*实验计划ID*/
		
		"szTestPlanName",		/*实验计划名称*/
	
		"szAcademicSubjectCode",		/*所属学科编码*/
	
		"dwTesteeKind",		/*实验者类别*/
		
		"dwTotalTestHour",		/*本实验计划总学时数*/
		
		"dwTeacherID",		/*教师（帐号）*/
		
		"szTeacherName",		/*教师姓名*/
	
		"dwCourseID",		/*课程ID*/
		
		"szCourseCode",		/*课程代码*/
	
		"szCourseName",		/*课程名称*/
	
		"dwCourseProperty",		/*课程属性*/
		
		"dwGroupID",		/*上课班级*/
		
		"szGroupName",		/*上课班级名称*/
	
		"dwGroupUsers",		/*组用户数*/
		
		"dwPlanKind",		/*计划类型*/
		
		"dwPlanStatus",		/*计划状态*/
		
		"dwTestCardID",		/*实验项目卡ID*/
		
		"szTestSN",		/*实验编号*/
	
		"szTestName",		/*实验名称*/
	
		"szCategoryName",		/*类别名(*/
	
      "dwStatus",		/*状态（前8种留出用于审核状态)*/
		
		"dwGroupPeopleNum",		/*每组人数*/
		
		"dwTestHour",		/*本实验项目学时数*/
		
		"dwTestClass",		/*实验类别*/
		
		"dwTestKind",		/*实验类型*/
		
		"dwRequirement",		/*实验要求*/
		
		"szTestItemURL",		/*详细描述*/
	
	"ResvInfo",		/*预约详细信息*/
	
		"dwMaxResvTimes",		/*最多预约次数*/
		
		"dwResvTestHour",		/*已预约学时数*/
		
		"dwDoneTestHour",		/*已完成学时数*/
		
		"szReportFormURL",		/*实验报告模板*/
	
		"szConstraints",		/*约束条件（比如需要的设备等）*/
	
		"szMemo",		/*备注*/
	 ""};

	/*获取实验项目试验者预约信息请求包*/
	static public string[] TESTITEMMEMRESVREQ = new string[]{
		
		"dwTestPlanID",		/*实验计划ID*/
		
		"dwTestItemID",		/*实验项目ID*/
		
		"dwGroupID",		/*上课班级*/
		
		"dwResvTestHour",		/*已预约学时数*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*实验项目试验者预约信息*/
	static public string[] TESTITEMMEMRESV = new string[]{
		
		"dwAccNo",		/*帐号*/
		
		"szPID",		/*学工号*/
	
		"szTrueName",		/*姓名*/
	
		"dwResvTestHour",		/*已预约学时数*/
		 ""};

	/*获取实验项目详细信息请求包*/
	static public string[] TESTITEMINFOREQ = new string[]{
		
		"dwSID",		/*记录ID*/
		
		"dwTeacherID",		/*教师（帐号）*/
		
		"dwAccNo",		/*帐号*/
		
		"dwTestPlanID",		/*实验计划ID*/
		
		"dwTestItemID",		/*实验项目ID*/
		
		"dwYearTerm",		/*学期编号*/
		
		"dwPlanKind",		/*计划类型*/
		
		"dwStatus",		/*状态（前8种留出用于审核状态)*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*实验项目详细信息*/
	static public string[] TESTITEMINFO = new string[]{
		
		"dwSID",		/*记录ID*/
		
		"dwStatus",		/*见UNITESTITEM定义*/
		
		"dwAccNo",		/*帐号*/
		
		"szPID",		/*学工号*/
	
		"szTrueName",		/*姓名*/
	
		"dwClassID",		/*班级ID*/
		
		"szClassName",		/*班级*/
	
		"dwDeptID",		/*部门ID*/
		
		"szDeptName",		/*部门*/
	
		"dwMaxResvTimes",		/*最多预约次数*/
		
		"szReportFormURL",		/*实验报告模板*/
	
		"szReportURL",		/*提交的实验报告*/
	
		"dwReportScore",		/*实验报告评分*/
		
		"szReportMarkInfo",		/*实验报告批改信息*/
	
		"dwYearTerm",		/*学期编号*/
		
		"dwTestPlanID",		/*实验计划ID*/
		
		"szTestPlanName",		/*实验计划名称*/
	
		"dwPlanKind",		/*计划类型*/
		
		"dwPlanStatus",		/*计划状态*/
		
		"szAcademicSubjectCode",		/*所属学科编码*/
	
		"dwTesteeKind",		/*实验者类别*/
		
		"dwTotalTestHour",		/*本实验计划总学时数*/
		
		"dwTeacherID",		/*教师（帐号）*/
		
		"szTeacherName",		/*教师姓名*/
	
		"dwCourseID",		/*课程ID*/
		
		"szCourseName",		/*课程名称*/
	
		"dwTestItemID",		/*实验项目ID*/
		
		"szConstraints",		/*约束条件（比如需要的设备等）*/
	
		"dwTestCardID",		/*实验项目卡ID*/
		
		"szTestName",		/*实验名称*/
	
		"szCategoryName",		/*类别名(*/
	
		"dwGroupPeopleNum",		/*每组人数*/
		
		"dwTestHour",		/*本实验项目学时数*/
		
		"dwTestClass",		/*实验类别*/
		
		"dwTestKind",		/*实验类型*/
		
		"dwRequirement",		/*实验要求*/
		
		"szTestItemURL",		/*详细描述*/
	
		"dwResvTestHour",		/*已预约学时数*/
		
		"dwDoneTestHour",		/*已完成学时数*/
		
	"ResvInfo",		/*预约详细信息*/
	
		"szMemo",		/*备注*/
	 ""};

	/*提交实验报告模板*/
	static public string[] REPORTFORMUPLOAD = new string[]{
		
		"dwTestItemID",		/*实验项目ID*/
		
		"dwTeacherID",		/*教师（帐号）*/
		
		"szReportFormURL",		/*实验报告模板*/
	 ""};

	/*提交实验报告*/
	static public string[] REPORTUPLOAD = new string[]{
		
		"dwSID",		/*记录ID*/
		
		"dwAccNo",		/*帐号*/
		
		"szReportURL",		/*提交的实验报告*/
	 ""};

	/*批改实验报告*/
	static public string[] REPORTCORRECT = new string[]{
		
		"dwSID",		/*记录ID*/
		
		"dwAccNo",		/*帐号*/
		
		"dwTeacherID",		/*教师（帐号）*/
		
		"dwReportScore",		/*实验报告评分*/
		
		"szReportMarkInfo",		/*实验报告批改信息*/
	 ""};

	/*设备组合*/
	static public string[] DEVGROUP = new string[]{
		
		"dwParentID",		/*所属父ID(比如实验项目ID)*/
		
		"dwDevKind",		/*设备类型*/
		
		"szDevName",		/*设备名称*/
	
		"dwDevNum",		/*设备数量*/
		
		"dwProperty",		/*设备属性（依赖关系，比如必选，可选等）*/
		
		"szMemo",		/*备注*/
	 ""};

	/*获取活动安排请求包*/
	static public string[] ACTIVITYPLANREQ = new string[]{
		
      "dwGetType",		/*方式*/
		
		"szGetKey",		/*条件值*/
	
		"dwStartDate",		/*开始日期*/
		
		"dwKind",		/*活动安排类型*/
		
		"dwStatus",		/*活动安排状态*/
		
		"dwOwner",		/*预约人(创建者)*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/**/
	static public string[] UNIACTIVITYPLAN = new string[]{
		
		"dwActivityPlanID",		/*活动安排ID*/
		
		"szActivityPlanName",		/*活动安排名称*/
	
		"szHostUnit",		/*主办单位*/
	
		"szOrganizer",		/*承办单位*/
	
		"szPresenter",		/*主持人*/
	
		"szDesiredUser",		/*参与者要求*/
	
      "dwCheckRequirment",		/*申请人审核要求*/
		
		"dwOwner",		/*预约人(创建者)*/
		
		"szContact",		/*联系人*/
	
		"szTel",		/*联系电话*/
	
		"szHandPhone",		/*联系手机*/
	
		"szEmail",		/*联系电子邮箱*/
	
		"dwResvID",		/*预约号*/
		
		"dwGroupID",		/*组ID（用于获取组成员明细）*/
		
		"dwMaxUsers",		/*最大限制人数*/
		
		"dwMinUsers",		/*最少限制人数*/
		
		"dwEnrollUsers",		/*已申请人数*/
		
		"dwEnrollDeadline",		/*申请加入截止日*/
		
		"dwPublishDate",		/*发布日期*/
		
		"dwActivityDate",		/*活动日期*/
		
		"dwBeginTime",		/*开始时间(HHMM)*/
		
		"dwEndTime",		/*结束时间(HHMM)*/
		
		"szSite",		/*主办地点*/
	
		"dwDevID",		/*空间（主办地点）ID*/
		
		"dwDevSN",		/*设备编号*/
		
		"szDevName",		/*设备名称*/
	
		"dwRoomID",		/*房间ID*/
		
		"szRoomNo",		/*房间编号*/
	
		"szRoomName",		/*房间名称*/
	
		"dwLabID",		/*实验室ID*/
		
		"szLabName",		/*实验室名称*/
	
		"dwBuildingID",		/*楼宇ID*/
		
		"szBuildingNo",		/*大楼编号(*/
	
		"szBuildingName",		/*大楼名称(*/
	
		"dwCampusID",		/*校区ID*/
		
		"szCampusName",		/*校区名称*/
	
      "dwKind",		/*活动安排类型*/
		
      "dwStatus",		/*活动安排状态（前8种留出用于CHECKINFO定义的管理员审核状态)*/
		
		"szIntroInfo",		/*活动介绍*/
	
		"szActivityPlanURL",		/*详细描述URL*/
	
		"szApplicationURL",		/*提交的申请材料连接*/
	
		"dwRealUsers",		/*实到人数*/
		
		"szMemo",		/*备注*/
	 ""};

	/*获取活动安排的座位请求包*/
	static public string[] APSEATREQ = new string[]{
		
		"dwActivityPlanID",		/*活动安排ID*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*活动安排的座位信息*/
	static public string[] APSEAT = new string[]{
		
		"dwActivityPlanID",		/*活动安排ID*/
		
		"dwDevID",		/*设备ID*/
		
		"dwDevSN",		/*设备编号*/
		
		"szDevName",		/*设备名称*/
	
      "dwStatus",		/*座位状态*/
		
		"dwAccNo",		/*账号*/
		
		"szTrueName",		/*成员姓名*/
	
		"szMemo",		/*备注*/
	 ""};

	/*申请参加活动*/
	static public string[] ACTIVITYENROLL = new string[]{
		
		"dwActivityPlanID",		/*活动安排ID*/
		
		"dwDevID",		/*设备ID*/
		
		"dwDevSN",		/*设备编号*/
		
		"dwAccNo",		/*账号*/
		
		"szTrueName",		/*成员姓名*/
	
		"szMemo",		/*备注*/
	 ""};

	/*退出活动申请*/
	static public string[] ACTIVITYEXIT = new string[]{
		
		"dwActivityPlanID",		/*活动安排ID*/
		
		"dwAccNo",		/*账号*/
		
		"szTrueName",		/*成员姓名*/
	
		"szMemo",		/*备注*/
	 ""};

	/*签到人员名单*/
	static public string[] ASIGNUSER = new string[]{
		
		"dwCardID",		/*卡ID号*/
		
		"dwInTime",		/*签到时间(HHMM)*/
		
      "dwRetStat",		/*返回状态*/
		
		"dwAccNo",		/*账号*/
		
		"szLogonName",		/*登录名(学工号)*/
	
		"szCardNo",		/*卡号*/
	
		"szTrueName",		/*成员姓名*/
	
		"szMemo",		/*备注*/
	 ""};

	/*签到人员名单*/
	static public string[] AOFFLINESIGN = new string[]{
		
		"dwActivityPlanID",		/*活动安排ID*/
		
		"dwResvID",		/*预约号*/
		
	"SignUser",		/*签到表CUniTable[ASIGNUSER]*/
	
		"szMemo",		/*备注*/
	 ""};

	/**/
	static public string[] RESVRULEREQ = new string[]{
		
		"dwRuleSN",		/*设备规则编号*/
		
		"dwDevClass",		/*设备类别（0表示无限制）*/
		
		"dwDevKind",		/*设备类型（0表示无限制）*/
		
		"dwDevID",		/*设备ID（0表示无限制）*/
		
		"dwResvPurpose",		/*预约用途*/
		
		"dwClassKind",		/*类别(见UNIDEVCLS的Kind定义)*/
		
		"dwExtValue",		/*不同预约类型定义的扩展值（0表示无限制）*/
		 ""};

	/**/
	static public string[] RESVRULEADMINREQ = new string[]{
		
		"dwRuleSN",		/*设备规则编号*/
		
		"dwDevClass",		/*设备类别（0表示无限制）*/
		
		"dwDevKind",		/*设备类型（0表示无限制）*/
		
		"dwDevID",		/*设备ID（0表示无限制）*/
		
		"dwResvPurpose",		/*预约用途*/
		
		"dwClassKind",		/*类别(见UNIDEVCLS的Kind定义)*/
		
		"dwExtValue",		/*不同预约类型定义的扩展值（0表示无限制）*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*审核表*/
	static public string[] RULECHECKINFO = new string[]{
		
		"dwResvRuleSN",		/*设备规则编号*/
		
		"dwCheckKind",		/*审核类型（新建时由系统自动分配）*/
		
		"dwBeforeKind",		/*依赖其他审核类别(可多个）*/
		
		"dwNeedMinTime",		/*审核需要的最短时间*/
		
		"dwMapValue",		/*触发审核值（和预约属性相关）*/
		
		"dwMainKind",		/*审核大类*/
		
		"szCheckName",		/*审核名称*/
	
		"dwCheckLevel",		/*审核级别(同UNIADMIN.dwManLevel定义）*/
		
		"dwDeptID",		/*责任部门ID（学院级不设置，根据申请人自动匹配）*/
		
		"szDeptName",		/*责任部门*/
	
      "dwProperty",		/*审核属性*/
		
		"szMemo",		/*状态说明*/
	 ""};

	/*设备使用规则结构*/
	static public string[] UNIRESVRULE = new string[]{
		
		"dwRuleSN",		/*设备规则编号*/
		
		"szRuleName",		/*设备规则名称*/
	
		"dwIdent",		/*身份（0表示无限制）*/
		
		"dwDeptID",		/*部门（0表示无限制）*/
		
		"dwDevClass",		/*设备类别（0表示无限制）*/
		
		"dwDevKind",		/*设备类型（0表示无限制）*/
		
		"dwDevID",		/*设备ID（0表示无限制）*/
		
		"dwGroupID",		/*指定用户组（0表示无限制）*/
		
		"dwResvPurpose",		/*预约用途*/
		
		"dwExtValue",		/*不同预约类型定义的扩展值（0表示无限制）*/
		
		"dwCreditRating",		/*信用等级*/
		
		"dwPriority",		/*优先级(数字大代表优先级高)*/
		
      "dwLimit",		/*预约限制*/
		
		"dwEarlyInTime",		/*允许提前进入时间(分钟)*/
		
		"dwEarliestResvTime",		/*最早提前预约时间(分钟)，数字比下面大*/
		
		"dwLatestResvTime",		/*最迟提前预约时间(分钟)，数字比上面小*/
		
		"dwMinResvTime",		/*最短预约时间(分钟)*/
		
		"dwMaxResvTime",		/*最长预约时间(分钟)*/
		
		"dwResvEndNewTime",		/*当前预约结束前指定时间(分钟)内可新建预约*/
		
		"dwResvBeforeNoticeTime",		/*预约生效提前通知时间(分钟)*/
		
		"dwResvAfterNoticeTime",		/*预约生效不来通知时间(分钟)*/
		
		"dwResvEndNoticeTime",		/*预约结束提前通知时间(分钟)*/
		
		"dwSeriesTimeLimit",		/*连续预约时间间隔(分钟)*/
		
		"dwTimeLimitForPurpose",		/*时间间隔相关的预约类型(比如座位管理，电子阅览室，研修间)*/
		
		"dwTimeConflictForPurpose",		/*时间冲突的预约类型(比如座位管理，电子阅览室，研修间预约时间不能相互冲突)*/
		
		"dwLatestSensorTime",		/*需审计的预约最迟审计时间(分钟)*/
		
		"dwCancelTime",		/*预约不来自动取消预约时间(分钟)*/
		
		"dwMinUseRate",		/*要求最低使用率(%)*/
		
      "dwFeeMode",		/*收费方式*/
		
		"dwMaxDevKind",		/*可预约设备种类*/
		
		"dwMaxDevNum",		/*可预约设备数*/
		
		"szOtherCons",		/*其他约束（比如时间限制和需要的设备等,格式有专门文件定义）*/
	
	"CheckTbl",		/*审核表CUniTable[RULECHECKINFO]*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*获取科研实验请求包*/
	static public string[] RESEARCHTESTREQ = new string[]{
		
		"dwRTID",		/*科研实验ID*/
		
		"dwHolderID",		/*主持人（帐号）*/
		
		"dwMemberID",		/*成员（帐号）*/
		
		"dwLeaderID",		/*负责人ID*/
		
		"dwDeptID",		/*部门ID*/
		
		"szRTName",		/*科研实验名称*/
	
		"dwRTLevel",		/*科研级别*/
		
		"dwStatus",		/*状态*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*科研实验成员*/
	static public string[] RTMEMBER = new string[]{
		
		"dwRTID",		/*科研实验ID*/
		
		"dwGroupID",		/*组ID*/
		
		"dwStatus",		/*成员状态(GROUPMEMBER定义)*/
		
		"dwAccNo",		/*账号*/
		
		"szTrueName",		/*成员姓名*/
	 ""};

	/*科研实验*/
	static public string[] RESEARCHTEST = new string[]{
		
		"dwRTID",		/*科研实验ID*/
		
		"szRTSN",		/*科研实验编号*/
	
		"szRTName",		/*科研实验名称*/
	
		"szFromUnit",		/*下发单位*/
	
      "dwRTKind",		/*科研类型*/
		
      "dwRTLevel",		/*科研级别*/
		
		"dwBeginDate",		/*开始日期*/
		
		"dwEndDate",		/*截止日期*/
		
		"dwHolderID",		/*主持人（帐号）*/
		
		"szHolderName",		/*主持人姓名*/
	
		"dwLeaderID",		/*负责人ID*/
		
		"szLeaderName",		/*负责人姓名*/
	
		"dwDeptID",		/*部门ID*/
		
		"szDeptName",		/*部门*/
	
		"dwTestTimes",		/*实验次数*/
		
		"dwTestMinutes",		/*实验累计时间*/
		
		"dwBalance",		/*可用余额*/
		
		"dwTotalFee",		/*累计费用*/
		
		"dwUnpayFee",		/*未结算费用*/
		
		"dwGroupID",		/*组ID*/
		
		"dwGroupUsers",		/*组成员人数*/
		
      "dwStatus",		/*状态（前8种留出用于CHECKINFO定义的管理员审核状态)*/
		
	"RTMembers",		/*成员明细表(CUniTable<RTMEMBER>)*/
	
		"szOtherCons",		/*其他约束（比如时间限制和需要的设备等,格式有专门文件定义）*/
	
		"szFundsNo",		/*经费卡编号（多个用逗号隔开)*/
	
		"szMemo",		/*备注*/
	 ""};

	/*获取样品信息请求包*/
	static public string[] SAMPLEINFOREQ = new string[]{
		
		"dwSampleSN",		/*样品编号*/
		
		"szSampleName",		/*样品名称*/
	
		"dwSamStat",		/*样品状态*/
		
		"dwDevID",		/*设备ID（获取某设备专用）*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*样品信息*/
	static public string[] SAMPLEINFO = new string[]{
		
		"dwSampleSN",		/*样品编号*/
		
		"szSampleName",		/*样品名称*/
	
		"szUnitName",		/*计费单位*/
	
		"dwUnitFee1",		/*单价1*/
		
		"dwUnitFee2",		/*单价2*/
		
		"dwUnitFee3",		/*单价3*/
		
		"dwUnitFee4",		/*单价4*/
		
		"dwUnitFee5",		/*单价5*/
		
      "dwSamStat",		/*样品状态*/
		
		"dwDevID",		/*设备ID（单一设备专用赋值设备ID，通用为0）*/
		
		"szMemo",		/*备注*/
	 ""};

	/*获取场馆预约的请求包*/
	static public string[] YARDRESVREQ = new string[]{
		
		"dwResvID",		/*预约号*/
		
		"dwApplicantID",		/*申请人账号*/
		
		"dwDevID",		/*设备ID*/
		
		"dwDevKind",		/*设备类型*/
		
		"dwClassKind",		/*类别(见UNIDEVCLS的Kind定义)*/
		
		"dwClassID",		/*设备类别ID*/
		
		"dwCheckStat",		/*确认状态*/
		
		"dwUnNeedStat",		/*不包含状态*/
		
		"dwBeginDate",		/*预约开始日期*/
		
		"dwEndDate",		/*预约结束日期*/
		
		"dwResvGroupID",		/*预约组ID*/
		
		"dwStatFlag",		/*状态标志*/
		
		"szLabIDs",		/*实验室ID,多个用逗号隔开*/
	
		"szRoomIDs",		/*房间ID,多个用逗号隔开*/
	
		"szBuildingIDs",		/*楼宇ID,多个用逗号隔开*/
	
		"szCampusIDs",		/*校区ID,多个用逗号隔开*/
	
		"dwActivitySN",		/*活动类型编号*/
		
		"dwProperty",		/*属性*/
		
		"dwUnNeedProperty",		/*不需要属性*/
		
		"szResvName",		/*使用用途名称*/
	
      "dwReqProp",		/*请求附加属性*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*场馆预约*/
	static public string[] YARDRESV = new string[]{
		
		"dwResvID",		/*预约号*/
		
		"dwResvGroupID",		/*组ID(多时段预约组ID相同，其余为预约ID)*/
		
		"szResvName",		/*使用用途名称*/
	
		"dwPurpose",		/*预约用途*/
		
		"dwProperty",		/*预约属性（新增需要摄像）*/
		
		"dwActivitySN",		/*活动类型编号*/
		
		"szActivityName",		/*活动类型名称*/
	
		"szOrganization",		/*组织*/
	
		"szOrganiger",		/*组织者*/
	
		"szHostUnit",		/*主办单位*/
	
		"szPresenter",		/*主持人*/
	
		"szDesiredUser",		/*参与者要求*/
	
		"szContact",		/*联系人*/
	
		"szTel",		/*联系电话*/
	
		"szHandPhone",		/*联系手机*/
	
		"szEmail",		/*联系电子邮箱*/
	
		"dwKind",		/*类型*/
		
		"szIntroInfo",		/*活动介绍*/
	
		"szCycRule",		/*预约时间规律描述*/
	
		"dwActivityLevel",		/*活动级别（和管理员级别一致）*/
		
		"dwCheckKinds",		/*审核类型(参考CHECKTYPE定义，可多个）*/
		
		"dwSecurityLevel",		/*安保级别（参考CHECKTYPE，决定是否提交保卫处审核）*/
		
		"dwMinAttendance",		/*最少参加人数（预估）*/
		
		"dwMaxAttendance",		/*最多参加人数（预估）*/
		
		"dwStatus",		/*预约状态(包括审查，是否生效，是否已取消等)*/
		
		"dwPreDate",		/*预约开始日期*/
		
		"dwOccurTime",		/*预约发生时间*/
		
		"dwBeginTime",		/*预约开始时间*/
		
		"dwEndTime",		/*预约结束时间*/
		
		"dwCheckTime",		/*审查时间(当新建预约指定RESVPROP_BYTHIRD时，表示dwThirdResvID)*/
		
		"dwAdvanceCheckTime",		/*提前审查时间*/
		
		"dwDevID",		/*设备ID*/
		
		"dwDevKind",		/*设备类型*/
		
		"dwDevSN",		/*设备编号*/
		
		"szDevName",		/*设备名称*/
	
		"dwRoomID",		/*房间ID*/
		
		"szRoomName",		/*房间名称*/
	
		"dwLabID",		/*实验室ID*/
		
		"szLabName",		/*实验室名称*/
	
		"dwBuildingID",		/*楼宇ID*/
		
		"szBuildingNo",		/*大楼编号(*/
	
		"szBuildingName",		/*大楼名称(*/
	
		"dwCampusID",		/*校区ID*/
		
		"szCampusName",		/*校区名称*/
	
		"dwDeptID",		/*场馆所属部门ID*/
		
		"szDeptName",		/*场馆所属部门名称*/
	
		"dwApplicantID",		/*申请人账号*/
		
		"szApplicantName",		/*申请人姓名*/
	
		"dwUserDeptID",		/*申请人部门ID*/
		
		"szUserDeptName",		/*申请人部门*/
	
		"dwResvRuleSN",		/*关联预约规则*/
		
		"dwOpenRuleSN",		/*关联开放时间表*/
		
		"dwFeeSN",		/*费率SN*/
		
		"szApplicationURL",		/*提交的申请材料连接*/
	
		"szSpareDevIDs",		/*备选设备ID（多个逗号隔开）*/
	
		"szMemo",		/*说明信息*/
	
		"dwFeedStat",		/*状态*/
		
		"dwFeedKind",		/*反馈类型*/
		
		"dwScore",		/*用户评分*/
		
		"szFeedInfo",		/*反馈信息*/
	
		"szReplyInfo",		/*回复信息*/
	 ""};

	/*获取场馆预约审核信息的请求包*/
	static public string[] YARDRESVCHECKINFOREQ = new string[]{
		
		"dwCheckID",		/*审核ID*/
		
		"dwResvID",		/*预约号*/
		
		"dwResvGroupID",		/*预约组ID*/
		
		"szResvName",		/*预约名称*/
	
		"dwCheckDeptID",		/*审核部门ID*/
		
		"dwApplicantID",		/*申请人账号*/
		
		"dwCheckStat",		/*确认状态*/
		
		"dwNeedYardResv",		/*需要获取场馆预约详情*/
		
		"dwBeginDate",		/*预约开始日期*/
		
		"dwEndDate",		/*预约结束日期*/
		
		"dwKind",		/*场馆预约类型*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*场馆预约审核信息*/
	static public string[] YARDRESVCHECKINFO = new string[]{
		
		"dwCheckID",		/*审核ID*/
		
		"dwResvID",		/*预约号*/
		
		"dwCheckKind",		/*审核类型*/
		
		"dwCheckLevel",		/*审核级别(同UNIADMIN.dwManLevel定义）*/
		
		"dwCheckDeptID",		/*审核部门ID*/
		
		"dwWaitKind",		/*等待审核类别(可多个）*/
		
		"szCheckName",		/*审核名称*/
	
		"dwBeforeKind",		/*依赖其他审核类别(可多个）*/
		
		"dwNeedMinTime",		/*审核需要的最短时间*/
		
		"dwCheckStat",		/*管理员审核状态(定义在ADMINCHECK)*/
		
		"szCheckDetail",		/*审查说明*/
	
		"dwCheckBeginDate",		/*审核开始日期*/
		
		"dwCheckDeadLine",		/*审核截止日期*/
		
		"dwCheckDate",		/*审核日期*/
		
		"dwCheckTime",		/*审核时间*/
		
		"dwAdminID",		/*审核者帐号*/
		
		"szAdminName",		/*审核者*/
	
		"dwApplicantID",		/*申请人账号*/
		
		"szApplicantName",		/*申请人姓名*/
	
	"YardResv",		/*CUniStruct[YARDRESV]*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*场馆预约审核*/
	static public string[] YARDRESVCHECK = new string[]{
		
		"dwCheckID",		/*审核ID*/
		
		"dwResvID",		/*预约号*/
		
		"dwCheckKind",		/*审核类型*/
		
		"dwCheckStat",		/*管理员审核状态(定义在ADMINCHECK)*/
		
		"szCheckDetail",		/*审查说明*/
	
	"YardResv",		/*CUniStruct[YARDRESV]*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*获取预约审核信息的请求包*/
	static public string[] RESVCHECKINFOREQ = new string[]{
		
		"dwCheckID",		/*审核ID*/
		
		"dwResvID",		/*预约号*/
		
		"dwCheckDeptID",		/*审核部门ID*/
		
		"dwApplicantID",		/*申请人账号*/
		
		"dwCheckStat",		/*确认状态*/
		
		"dwNeedResv",		/*需要获取预约详情*/
		
		"dwBeginDate",		/*预约开始日期*/
		
		"dwEndDate",		/*预约结束日期*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*预约审核信息*/
	static public string[] RESVCHECKINFO = new string[]{
		
		"dwCheckID",		/*审核ID*/
		
		"dwResvID",		/*预约号*/
		
		"dwCheckKind",		/*审核类型*/
		
		"dwCheckLevel",		/*审核级别(同UNIADMIN.dwManLevel定义）*/
		
		"dwCheckDeptID",		/*审核部门ID*/
		
		"dwWaitKind",		/*等待审核类别(可多个）*/
		
		"szCheckName",		/*审核名称*/
	
		"dwBeforeKind",		/*依赖其他审核类别(可多个）*/
		
		"dwNeedMinTime",		/*审核需要的最短时间*/
		
		"dwCheckStat",		/*管理员审核状态(定义在ADMINCHECK)*/
		
		"szCheckDetail",		/*审查说明*/
	
		"dwCheckBeginDate",		/*审核开始日期*/
		
		"dwCheckDeadLine",		/*审核截止日期*/
		
		"dwCheckDate",		/*审核日期*/
		
		"dwCheckTime",		/*审核时间*/
		
		"dwAdminID",		/*审核者帐号*/
		
		"szAdminName",		/*审核者*/
	
		"dwApplicantID",		/*申请人账号*/
		
		"szApplicantName",		/*申请人姓名*/
	
	"ResvInfo",		/*CUniStruct[UNIRESERVE]*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*预约审核*/
	static public string[] RESVCHECK = new string[]{
		
		"dwCheckID",		/*审核ID*/
		
		"dwResvID",		/*预约号*/
		
		"dwCheckKind",		/*审核类型*/
		
		"dwCheckStat",		/*管理员审核状态(定义在ADMINCHECK)*/
		
		"szCheckDetail",		/*审查说明*/
	
	"ResvInfo",		/*CUniStruct[UNIRESERVE]*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*获取场馆活动的请求包*/
	static public string[] YARDACTIVITYREQ = new string[]{
		
		"dwActivitySN",		/*活动类型编号*/
		
		"dwActivityLevel",		/*活动级别（和管理员级别一致）*/
		
		"dwCheckKinds",		/*审核类型(参考CHECKTYPE定义，可多个）*/
		
		"dwSecurityLevel",		/*安保级别（参考CHECKTYPE，决定是否提交保卫处审核）*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*场馆活动可用设备类型*/
	static public string[] YADEVKIND = new string[]{
		
		"dwActivitySN",		/*活动类型编号*/
		
		"dwKindID",		/*设备类型ID*/
		 ""};

	/*场馆活动*/
	static public string[] YARDACTIVITY = new string[]{
		
		"dwActivitySN",		/*活动类型编号*/
		
		"szActivityName",		/*活动类型名称*/
	
		"dwActivityLevel",		/*活动级别（和管理员级别一致）*/
		
		"dwCheckKinds",		/*审核类型(参考CHECKTYPE定义，可多个）*/
		
		"szCheckNames",		/*审核类型名称,多个用逗号隔开*/
	
      "dwSecurityLevel",		/*安保级别（参考CHECKTYPE，决定是否提交保卫处审核）*/
		
	"UsableDevKind",		/*CUniTable[YADEVKIND]*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*第三方预约设备*/
	static public string[] TRESVDEV = new string[]{
		
		"szAssertSN",		/*资产编号*/
	 ""};

	/*第三方预约时间表*/
	static public string[] TRESVTIME = new string[]{
		
		"dwResvDate",		/*预约日期*/
		
		"dwStartHM",		/*预约开始时间*/
		
		"dwEndHM",		/*预约结束时间*/
		 ""};

	/*第三方预约共享设备*/
	static public string[] THIRDRESVSHAREDEV = new string[]{
		
		"dwThirdResvID",		/*第三方预约ID*/
		
		"szResvTitle",		/*预约标题*/
	
	"DevTbl",		/*预约设备表*/
	
	"TimeTbl",		/*预约时间表*/
	 ""};

	/*第三方删除预约*/
	static public string[] THIRDRESVDEL = new string[]{
		
		"dwThirdResvID",		/*第三方预约ID*/
		 ""};

	/*获取第三方预约的请求包*/
	static public string[] THIRDRESVREQ = new string[]{
		
		"dwThirdResvID",		/*第三方预约ID*/
		
		"szPID",		/*学工号*/
	
		"dwStatus",		/*状态*/
		
		"dwBeginDate",		/*预约开始日期*/
		
		"dwEndDate",		/*预约结束日期*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*第三方预约*/
	static public string[] THIRDRESV = new string[]{
		
		"dwThirdResvID",		/*第三方预约ID*/
		
		"szResvTitle",		/*预约标题*/
	
		"dwResvDate",		/*预约日期*/
		
		"dwStartHM",		/*预约开始时间*/
		
		"dwEndHM",		/*预约结束时间*/
		
		"szOrganization",		/*组织*/
	
		"szOrganiger",		/*组织者*/
	
		"szHostUnit",		/*主办单位*/
	
		"szPresenter",		/*主持人*/
	
		"szDesiredUser",		/*参与者要求*/
	
		"szIntroInfo",		/*活动介绍*/
	
		"szPID",		/*申请人学工号*/
	
		"szTrueName",		/*申请人姓名*/
	
		"szTel",		/*联系电话*/
	
		"szHandPhone",		/*联系手机*/
	
		"szEmail",		/*联系电子邮箱*/
	
		"dwMinAttendance",		/*最少参加人数（预估）*/
		
		"dwMaxAttendance",		/*最多参加人数（预估）*/
		
		"dwStatus",		/*预约状态((0表示未预约，包括审查，是否生效，是否已取消等)*/
		
		"szAssertSN",		/*资产编号*/
	
		"dwResvID",		/*预约号*/
		
		"szMemo",		/*说明信息*/
	 ""};

	/*设备预约信息表*/
	static public string[] DEVICERESV = new string[]{
		
		"dwDevID",		/*设备ID*/
		
      "dwResvFrom",		/*来源*/
		
		"dwResvID",		/*预约ID*/
		
		"dwResvDate",		/*预约日期*/
		
		"dwStartHM",		/*预约开始时间*/
		
		"dwEndHM",		/*预约结束时间*/
		
		"dwResvMin",		/*预约时间*/
		
		"dwAccNo",		/*预约人帐号*/
		
		"dwSex",		/*预约人性别*/
		
		"szPID",		/*申请人学工号*/
	
		"szTrueName",		/*申请人姓名*/
	
		"szMemberName",		/*组名*/
	
		"szResvTitle",		/*预约标题*/
	
		"szMemo",		/*说明信息*/
	 ""};

}
	/*结束数据结构*/

	/*开始数据结构*/
public partial class Struct_ST
{

	/*获取监控分类库的请求包*/
	static public string[] CTRLCLASSREQ = new string[]{
		
		"dwCtrlSN",		/*监控分类库编号*/
		
		"dwCtrlKind",		/*控制分类*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*监控分类库结构*/
	static public string[] UNICTRLCLASS = new string[]{
		
		"dwCtrlSN",		/*监控分类库编号*/
		
      "dwCtrlKind",		/*控制分类*/
		
		"dwCtrlLevel",		/*控制分类级别，可自定义*/
		
		"szCtrlName",		/*监控分类库名称*/
	
      "dwCtrlMode",		/*控制方式*/
		
		"dwForAges",		/*适用年龄段(FFTT 0713表示7-13周岁)*/
		
		"szMemo",		/*备注*/
	 ""};

	/*获取网址组的请求包*/
	static public string[] CTRLURLREQ = new string[]{
		
		"dwCtrlLevel",		/*控制分类级别，可自定义*/
		
		"dwClassSN",		/*监控分类库编号*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*网址组结构*/
	static public string[] UNICTRLURL = new string[]{
		
		"dwClassSN",		/*监控分类库编号*/
		
		"dwCtrlLevel",		/*控制分类级别，可自定义*/
		
		"szCtrlName",		/*网址组名称*/
	
		"dwCtrlMode",		/*控制方式*/
		
		"dwForAges",		/*适用年龄段(FFTT 0713表示7-13周岁)*/
		
		"dwID",		/*网址ID*/
		
		"szURL",		/*URL(支持通配符)*/
	
		"dwPort",		/*端口*/
		
      "dwStatus",		/*状态*/
		
		"szMemo",		/*备注*/
	 ""};

	/*获取软件组的请求包*/
	static public string[] CTRLSWREQ = new string[]{
		
		"dwCtrlLevel",		/*控制分类级别，可自定义*/
		
		"dwClassSN",		/*监控分类库编号*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*软件组结构*/
	static public string[] UNICTRLSW = new string[]{
		
		"dwClassSN",		/*监控分类库编号*/
		
		"dwCtrlLevel",		/*控制分类级别，可自定义*/
		
		"szCtrlName",		/*软件组名称*/
	
		"dwCtrlMode",		/*控制方式*/
		
		"dwForAges",		/*适用年龄段(FFTT 0713表示7-13周岁)*/
		
		"dwID",		/*key*/
		
		"szName",		/*软件组成员名称*/
	
		"dwMemberID",		/*根据不同类别表示不同含义*/
		
      "dwKind",		/*成员类型*/
		
      "dwStatus",		/*状态*/
		
		"szMemo",		/*备注*/
	 ""};

	/*获取软件的请求包*/
	static public string[] SOFTWAREREQ = new string[]{
		
      "dwGetType",		/*方式*/
		
		"szGetKey",		/*条件值*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*软件结构*/
	static public string[] UNISOFTWARE = new string[]{
		
		"dwSWID",		/*软件ID*/
		
      "dwKind",		/*成员类型*/
		
		"szSWName",		/*软件名称*/
	
		"szSWVersion",		/*软件版本*/
	
		"szSWCompany",		/*公司*/
	
		"szDispSWName",		/*显示产品名称，可修改*/
	
		"szDispSWCompany",		/*显示公司名称，可修改*/
	
		"dwFrom",		/*来自哪里（同步层次）*/
		
		"dwChgFlag",		/*修改更新标志*/
		
		"szMemo",		/*备注*/
	 ""};

	/*获取程序的请求包*/
	static public string[] PROGRAMREQ = new string[]{
		
      "dwGetType",		/*方式*/
		
		"szGetKey",		/*条件值*/
	
		"dwKind",		/*成员类型*/
		
		"dwProperty",		/*程序属性*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*程序结构*/
	static public string[] UNIPROGRAM = new string[]{
		
		"dwID",		/*程序ID*/
		
		"dwSubID",		/*副ID*/
		
		"dwSWID",		/*所属软件ID*/
		
		"dwKind",		/*所属类别*/
		
      "dwProperty",		/*程序属性*/
		
		"szProductName",		/*产品名称*/
	
		"szExeName",		/*Exe文件名*/
	
		"szSWVersion",		/*程序版本*/
	
		"szDispProductName",		/*显示程序名称，可修改*/
	
		"szDispSWName",		/*显示产品名称，可修改*/
	
		"szDispSWCompany",		/*显示公司名称，可修改*/
	
		"dwFrom",		/*来自哪里（同步层次）*/
		
		"dwChgFlag",		/*修改更新标志*/
		
		"szIcon",		/*图标*/
	
		"szMemo",		/*备注*/
	 ""};

	/*获取机器程序的请求包*/
	static public string[] PCSWINFOREQ = new string[]{
		
      "dwGetType",		/*方式*/
		
		"szGetKey",		/*条件值*/
	
		"dwKind",		/*软件类型*/
		
		"dwProperty",		/*程序属性*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*机器程序结构*/
	static public string[] UNIPCSWINFO = new string[]{
		
		"szProgramInfo",		/*CUniStruct(<UNIPROGRAM>)*/
	
		"dwPCID",		/*机器ID*/
		
		"szInstName",		/*安装名称*/
	
		"szInstPath",		/*安装路径*/
	
		"dwRunLatestDate",		/*最近运行日期*/
		
		"dwRunTimes",		/*运行次数*/
		
		"dwRunMinutes",		/*累计运行分钟数*/
		
		"szMemo",		/*备注*/
	 ""};

	/*获取机房程序的请求包*/
	static public string[] ROOMSWINFOREQ = new string[]{
		
      "dwGetType",		/*方式*/
		
		"szGetKey",		/*条件值*/
	
		"dwKind",		/*软件类型*/
		
		"dwProperty",		/*程序属性*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*机房程序结构*/
	static public string[] UNIROOMSWINFO = new string[]{
		
		"szProgramInfo",		/*CUniStruct(<UNIPROGRAM>)*/
	
		"dwRoomID",		/*机房ID*/
		
		"dwInstSWNum",		/*安装该软件机器数*/
		
		"dwRunTimes",		/*运行次数*/
		
		"dwRunMinutes",		/*累计运行分钟数*/
		
		"szMemo",		/*备注*/
	 ""};

	/*机器程序结构（上传用）*/
	static public string[] PCPROGRAM = new string[]{
		
		"szProgramInfo",		/*CUniStruct(<UNIPROGRAM>)*/
	
		"dwDevID",		/*客户端设备的ID号*/
		
		"dwLabID",		/*实验室的ID号*/
		
		"dwPID",		/*进程ID*/
		
		"szInstName",		/*安装名称*/
	
		"szInstPath",		/*安装路径*/
	
		"szMemo",		/*备注*/
	 ""};

	/*机器程序结束信息（上传用）*/
	static public string[] PROGEND = new string[]{
		
		"dwDevID",		/*客户端设备的ID号*/
		
		"dwLabID",		/*实验室的ID号*/
		
		"dwProcID",		/*程序的ID号*/
		
		"dwPID",		/*进程ID*/
		 ""};

	/*退出程序信息*/
	static public string[] QUITAPPINFO = new string[]{
		
		"dwProcID",		/*程序ID*/
		
		"szMemo",		/*备注*/
	 ""};

	/*网址信息*/
	static public string[] URLCHECKINFO = new string[]{
		
		"dwDevID",		/*客户端设备的ID号*/
		
		"dwLabID",		/*实验室的ID号*/
		
		"dwAccNo",		/*帐号*/
		
		"dwRemoteIp",		/*访问IP*/
		
		"dwPort",		/*访问端口*/
		
		"szDomainName",		/*域名*/
	
		"szURL",		/*网址*/
	
		"szMemo",		/*备注*/
	 ""};

}
	/*结束数据结构*/

	/*开始数据结构*/
public partial class Struct_ST
{

	/*登录请求包*/
	static public string[] THIRDLOGINREQ = new string[]{
		
		"dwSysID",		/*系统编号*/
		
		"szVersion",		/*版本*/
	
		"szExtInfo",		/*扩展信息*/
	 ""};

	/*登录应答包*/
	static public string[] THIRDLOGINRES = new string[]{
		
		"szVersion",		/*版本*/
	
		"szExtInfo",		/*扩展信息*/
	 ""};

	/*获取账户列表输入参数*/
	static public string[] THIRDACCREQ = new string[]{
		
      "dwGetType",		/*方式*/
		
		"szParam",		/*参数*/
	 ""};

	/*同步帐户请求包*/
	static public string[] SYNCACCREQ = new string[]{
		
      "dwType",		/*同步方式*/
		
		"szMemo",		/*扩展信息*/
	 ""};

	/*获取最新帐户信息状态*/
	static public string[] SYNCACCINFO = new string[]{
		
      "dwStatus",		/*当前状态*/
		
		"dwStartTime",		/*开始时间(time函数)*/
		
		"dwUseTime",		/*已用时间(秒)*/
		
		"dwEstmateTime",		/*估计总所需时间(秒)*/
		
		"dwTotalAcc",		/*总用户数*/
		
		"dwDealAcc",		/*已处理完成用户数*/
		
		"dwDiffAcc",		/*信息与本地不同用户数*/
		
		"szInfo",		/*扩展信息*/
	 ""};

}
	/*结束数据结构*/

	/*开始数据结构*/
public partial class Struct_ST
{

	/**/
	static public string[] PERIODFEE = new string[]{
		
		"dwPStart",		/*时段开始时间*/
		
		"dwPEnd",		/*时段结束时间*/
		
		"dwPUnitFee",		/*时段单位费率*/
		
		"dwPAssistFee",		/*时段管理员指导费*/
		 ""};

	/**/
	static public string[] FEEREQ = new string[]{
		
		"dwFeeSN",		/*费率SN*/
		
		"dwIdent",		/*身份（0表示无限制）*/
		
		"dwDeptID",		/*部门（0表示无限制）*/
		
		"dwDevKind",		/*设备类型（0表示无限制）*/
		
		"dwGroupID",		/*指定用户组（0表示无限制）*/
		
		"dwPurpose",		/*用途*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/**/
	static public string[] RTDEVFEEREQ = new string[]{
		
		"dwRTID",		/*科研实验项目ID*/
		
		"dwDevID",		/*设备ID*/
		 ""};

	/*收费标准详细信息*/
	static public string[] FEEDETAIL = new string[]{
		
      "dwFeeType",		/*收费类别*/
		
		"dwUsablePayKind",		/*可用缴费方式(见UNIBILL定义)*/
		
		"dwDefaultCheckStat",		/*CHECKINFO定义的管理员审核状态*/
		
		"dwUnitFee",		/*单位使用费率(小时 缺省100)*/
		
		"dwUnitTime",		/*单位时间(缺省1)*/
		
		"dwRoundOff",		/*舍入分界点(小于单位时间)*/
		
		"dwIgnoreTime",		/*不计费时间(缺省0,正表示不计费时间，负表示最少使用时间)*/
		
		"dwHolidayCoef",		/*假日系数*/
		
		"szPosInfo",		/*与一卡通对应的商户信息*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*收费标准详细信息*/
	static public string[] UNIFEE = new string[]{
		
		"dwFeeSN",		/*费率SN*/
		
		"szFeeName",		/*名称*/
	
		"dwPriority",		/*优先级(数字大代表优先级高)*/
		
		"dwIdent",		/*身份（0表示无限制）*/
		
		"dwDeptID",		/*部门（0表示无限制）*/
		
		"dwDevKind",		/*设备类型（0表示无限制）*/
		
		"dwGroupID",		/*指定用户组（0表示无限制）*/
		
		"dwPurpose",		/*预约用途*/
		
		"dwOverDraft",		/*允许透支额*/
		
		"dwMinInTime",		/*允许进入该用户最低可用时间*/
		
      "dwQuotaRule",		/*限制规则(日累计，次累计，机器忙等(缺省0))*/
		
		"dwQuotaTime",		/*限制使用时间(缺省-1)*/
		
	"szFeeDetail",		/*收费标准明细表CUniTable[FEEDETAIL]*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/**/
	static public string[] RTDEVSAMPLEREQ = new string[]{
		
		"dwRTID",		/*科研实验项目ID*/
		
		"dwDevID",		/*设备ID*/
		 ""};

	/*科研项目对应的设备的样品及费率表*/
	static public string[] RTDEVSAMPLE = new string[]{
		
		"dwDevID",		/*设备ID*/
		
		"dwSampleSN",		/*样品编号*/
		
		"szSampleName",		/*样品名称*/
	
		"szUnitName",		/*计费单位*/
	
		"dwUnitFee",		/*单价*/
		
		"szMemo",		/*说明信息*/
	 ""};

	/*获取账单请求*/
	static public string[] BILLREQ = new string[]{
		
      "dwGetType",		/*方式*/
		
		"szGetKey",		/*条件值*/
	
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
		"dwFeeType",		/*收费类别(FEEDETAIL定义)*/
		
		"dwPayKind",		/*缴费方式*/
		
		"dwStatus",		/*状态*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*用户账单*/
	static public string[] UNIBILL = new string[]{
		
		"dwSID",		/*流水号*/
		
		"dwResvID",		/*预约号*/
		
		"dwCostSID",		/*使用流水号*/
		
		"szPosInfo",		/*与一卡通对应的商户信息*/
	
		"dwAccNo",		/*帐号*/
		
		"szPID",		/*学工号*/
	
		"szTrueName",		/*姓名*/
	
		"dwDevKind",		/*设备类型*/
		
		"szKindName",		/*设备名称*/
	
		"szModel",		/*设备型号*/
	
		"szSpecification",		/*设备规格*/
	
		"dwFeeType",		/*收费类别(FEEDETAIL定义)*/
		
		"dwBeginTime",		/*开始时间*/
		
		"dwEndTime",		/*结束时间*/
		
		"dwUnitFee",		/*费率*/
		
		"dwUnitTime",		/*单位时间*/
		
		"dwRoundOff",		/*舍入分界点(小于单位时间)*/
		
		"dwIgnoreTime",		/*不计费时间*/
		
		"dwHolidayCoef",		/*假日系数*/
		
		"dwUseTime",		/*使用时间*/
		
		"dwFeeTime",		/*计费时间*/
		
		"dwCostMoney",		/*应缴费用*/
		
		"dwCostSubsidy",		/*补助*/
		
		"dwCostFreeTime",		/*机时*/
		
		"dwRealCost",		/*实际缴纳费用*/
		
      "dwUsablePayKind",		/*可用缴费方式*/
		
		"dwUsedPayKind",		/*实际缴费方式*/
		
      "dwStatus",		/*CHECKINFO定义的管理员审核状态+如下定义*/
		
		"dwBillDate",		/*账单日期*/
		
		"dwBillTime",		/*账单时间*/
		
		"dwAuditorID",		/*审核员*/
		
		"dwTollID",		/*收费员或一卡通tblThirdSyncCost的流水号*/
		
		"szMemo",		/*说明信息*/
	 ""};

	/*账单缴费*/
	static public string[] BILLPAY = new string[]{
		
		"dwPayKind",		/*缴费方式*/
		
		"dwTotalCost",		/*缴费合计*/
		
		"dwOneCardSID",		/*一卡通流水号*/
		
		"szCardCostInfo",		/*卡通卡扣费信息，不同的一卡通格式和内容都不同*/
	
	"szBillInfo",		/*CUniTable[UNIBILL]*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*机时使用规则请求*/
	static public string[] FTRULEREQ = new string[]{
		
      "dwGetType",		/*方式*/
		
		"szGetKey",		/*条件值*/
	
		"szSubKey",		/*选择专业方式时可设置为入学年份*/
	 ""};

	/*机时使用规则*/
	static public string[] FREETIMERULE = new string[]{
		
		"dwSN",		/*机时规则SN*/
		
		"szName",		/*名称*/
	
		"dwFTType",		/*机时类别*/
		
		"dwMajorID",		/*专业*/
		
		"szMajorName",		/*专业名称*/
	
		"dwEnrolYear",		/*入学年份*/
		
      "dwPeriod",		/*周期（学期，学年，整个大学期间）*/
		
		"dwPlanFT",		/*计划总时间*/
		
		"dwDayLimit",		/*每日使用限额*/
		
		"dwPlanUseTimes",		/*计划总使用次数*/
		
		"szMemo",		/*说明信息*/
	 ""};

}
	/*结束数据结构*/

	/*开始数据结构*/
public partial class Struct_ST
{

	/*获取控制台的请求包*/
	static public string[] CONREQ = new string[]{
		
		"dwConsoleSN",		/*控制台编号*/
		
		"szConsoleName",		/*控制台名称*/
	
		"dwKind",		/*控制台类型*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*控制台信息*/
	static public string[] UNICONSOLE = new string[]{
		
		"dwConsoleSN",		/*控制台编号*/
		
		"szConsoleName",		/*控制台名称*/
	
      "dwKind",		/*控制台类型*/
		
		"dwStatus",		/*控制台状态（参考CommonIF.xmlCONSTAT_XXX定义)*/
		
		"dwOpenTime",		/*开始时间*/
		
		"dwCloseTime",		/*关闭时间*/
		
		"szIP",		/*IP地址*/
	
		"szManRooms",		/*管理房间(房间编号，可多个，逗号隔开)*/
	
		"szDispInfoURL",		/*显示信息连接*/
	
		"szLocation",		/*控制台存放位置*/
	
		"szMemo",		/*说明信息*/
	
	"MoniInfo",		/*监控信息*/
	 ""};

	/*控制台登录请求*/
	static public string[] CONLOGINREQ = new string[]{
		
		"szVersion",		/*版本	XX.XX.XXXXXXXX*/
	
		"dwStaSN",		/*站点编号*/
		
		"szIP",		/*IP地址*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*控制台登录响应*/
	static public string[] CONLOGINRES = new string[]{
		
		"dwSessionID",		/*服务器分配的SessionID*/
		
	"SrvVer",		/*UNIVERSION 结构*/
	
		"szCurTime",		/*服务器时间 YYYY-MM-DD HH:MM:SS*/
	
		"dwConsoleSN",		/*控制台编号*/
		
		"szConsoleName",		/*控制台名称*/
	
		"dwKind",		/*控制台类型*/
		
		"dwOpenTime",		/*开门时间*/
		
		"dwCloseTime",		/*关闭时间*/
		
		"szDispInfoURL",		/*显示信息连接*/
	
		"szMemo",		/*说明信息*/
	
		"szManRooms",		/*管理房间(房间编号，多个逗号隔开)*/
	
	"szManDevs",		/*管理设备列表CUniTable[UNIDEVICE]*/
	 ""};

	/*控制台退出请求*/
	static public string[] CONLOGOUTREQ = new string[]{
		
		"dwConsoleSN",		/*控制台编号*/
		
		"szMemo",		/*说明信息*/
	 ""};

	/*控制台定时通信请求*/
	static public string[] CONPULSEREQ = new string[]{
		
		"dwConsoleSN",		/*控制台编号*/
		
		"dwStatus",		/*控制台状态*/
		
		"szStatInfo",		/*状态信息*/
	 ""};

	/*控制台定时通信响应*/
	static public string[] CONPULSERES = new string[]{
		
		"dwChanged",		/*控制台是否已更新*/
		
		"szMemo",		/*说明信息*/
	 ""};

	/*控制台显示消息结构*/
	static public string[] CONMESSAGE = new string[]{
		
      "dwMsgKind",		/*消息类型*/
		
		"MsgInfo",		/*消息内容，根据不同的类型对应不同的内容*/
	 ""};

	/*控制台刷卡返回用户信息*/
	static public string[] CONUSERINFO = new string[]{
		
      "dwUserStat",		/*用户状态*/
		
	"AccInfo",		/*UNIACCOUNT 结构*/
	
	"ResvInfo",		/*UNIRESERVE 结构*/
	
	"DevInfo",		/*UNIDEVICE 结构*/
	
	"BillInfo",		/*账单表(CUniTable<UNIBILL>)*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*控制台教师登录返回信息*/
	static public string[] CONTEACHERINFO = new string[]{
		
	"AccInfo",		/*UNIACCOUNT 结构*/
	
	"ResvInfo",		/*UNIRESERVE 结构*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*刷卡上机请求*/
	static public string[] CARDFORPCREQ = new string[]{
		
		"dwDevKind",		/*设备类型(为空由后台自动分配）*/
		
		"dwLabID",		/*实验室ID号(为空由后台自动分配）*/
		
		"dwRoomID",		/*所选房间的ID号(为空由后台自动分配）*/
		
		"dwDevID",		/*客户端设备的ID号(为空由后台自动分配）*/
		
	"CheckReq",		/*(ACCCHECKREQ结构)*/
	 ""};

	/*刷卡上机应答*/
	static public string[] CARDFORPCRES = new string[]{
		
      "dwMode",		/*返回类型*/
		
	"ExtInfo",		/*根据不同的返回类型对应不同的内容*/
	 ""};

	/*通道机刷卡请求*/
	static public string[] AUTOGATECARDREQ = new string[]{
		
      "dwCardMode",		/*进出刷卡*/
		
		"szLogonName",		/*登录名(学工号）*/
	
		"szCardNo",		/*卡号*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*通道机刷卡响应*/
	static public string[] AUTOGATECARDRES = new string[]{
		
		"szTrueName",		/*姓名*/
	
		"szInfo",		/*提示信息*/
	 ""};

	/*控制台刷卡进入*/
	static public string[] CONUSERINREQ = new string[]{
		
      "dwInType",		/*进入类型*/
		
		"dwResvID",		/*预约ID号*/
		
		"dwLabID",		/*实验室ID号*/
		
		"dwDevID",		/*客户端设备的ID号*/
		
		"dwDevKind",		/*设备类型ID*/
		
		"dwEndTime",		/*使用结束时间*/
		
		"szMemo",		/*说明信息*/
	 ""};

	/*控制台刷卡进入*/
	static public string[] CONUSERINRES = new string[]{
		
	"ResvInfo",		/*UNIRESERVE 结构*/
	
	"DevInfo",		/*UNIDEVICE 结构*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*控制台刷卡退出请求*/
	static public string[] CONUSEROUTREQ = new string[]{
		
      "dwOutType",		/*离开类型*/
		
		"dwLabID",		/*实验室ID号*/
		
		"dwDevID",		/*客户端设备的ID号*/
		 ""};

	/*控制台刷卡退出应答*/
	static public string[] CONUSEROUTRES = new string[]{
		
	"AcctInfo",		/*使用信息，UNIACCTINFO 结构*/
	
	"BillInfo",		/*账单表(CUniTable<UNIBILL>)*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*手机扫描请求*/
	static public string[] MOBILESCANREQ = new string[]{
		
		"szMSN",		/*MSN*/
	
		"szLogonName",		/*登录名*/
	
		"szPassword",		/*密码*/
	
		"szIP",		/*IP地址*/
	
      "dwProperty",		/*扩展属性*/
		
		"dwStaSN",		/*站点编号*/
		
		"dwLabID",		/*实验室ID*/
		
		"dwDevID",		/*设备ID*/
		
		"szMemo",		/*说明信息*/
	 ""};

	/*手机扫描响应*/
	static public string[] MOBILESCANRES = new string[]{
		
		"dwSessionID",		/*服务器分配的SessionID*/
		
      "dwUserStat",		/*用户状态*/
		
		"dwMinUseMin",		/*最少使用时间(分钟)*/
		
		"dwMaxUseMin",		/*最长使用时间(分钟)*/
		
		"szPID",		/*学工号*/
	
		"szTrueName",		/*姓名*/
	
		"dwLabID",		/*实验室ID*/
		
		"dwDevID",		/*设备ID*/
		
		"szDevName",		/*设备名称*/
	
		"szDispInfo",		/*显示信息*/
	 ""};

	/*手机开始使用请求*/
	static public string[] MOBILEUSERINREQ = new string[]{
		
		"dwUseMin",		/*使用时间(分钟)*/
		
		"szMemo",		/*备注*/
	 ""};

	/*手机进入响应*/
	static public string[] MOBILEUSERINRES = new string[]{
		
		"szDispInfo",		/*显示信息*/
	 ""};

	/*手机开始使用请求*/
	static public string[] MOBILEDELAYREQ = new string[]{
		
		"dwDelayMin",		/*延长时间(分钟)*/
		
		"szMemo",		/*备注*/
	 ""};

	/*手机进入响应*/
	static public string[] MOBILEDELAYRES = new string[]{
		
		"szDispInfo",		/*显示信息*/
	 ""};

	/*手机退出请求*/
	static public string[] MOBILEUSEROUTREQ = new string[]{
		
      "dwOutType",		/*离开类型*/
		
		"dwDelayMin",		/*延时时间(分钟)*/
		
		"szMemo",		/*备注*/
	 ""};

	/*手机退出响应*/
	static public string[] MOBILEUSEROUTRES = new string[]{
		
		"szDispInfo",		/*显示信息*/
	 ""};

	/*手机登录签到请求*/
	static public string[] RESVUSERCOMEINREQ = new string[]{
		
      "dwInType",		/*进入类型*/
		
		"dwResvID",		/*预约ID号*/
		
		"dwLabID",		/*实验室ID号*/
		
		"dwDevID",		/*客户端设备的ID号*/
		
		"szMemo",		/*说明信息*/
	 ""};

	/*手机登录签到响应*/
	static public string[] RESVUSERCOMEINRES = new string[]{
		
		"szDispInfo",		/*显示信息*/
	 ""};

	/*手机登录延时请求*/
	static public string[] RESVUSERDELAYREQ = new string[]{
		
		"dwResvID",		/*预约ID号*/
		
		"dwLabID",		/*实验室ID号*/
		
		"dwDevID",		/*客户端设备的ID号*/
		
		"dwMaxDelayMin",		/*最大延长时间(分钟)*/
		
		"szMemo",		/*备注*/
	 ""};

	/*手机登录延时响应*/
	static public string[] RESVUSERDELAYRES = new string[]{
		
		"szDispInfo",		/*显示信息*/
	 ""};

	/*手机登录退出请求*/
	static public string[] RESVUSERGOOUTREQ = new string[]{
		
      "dwOutType",		/*离开类型*/
		
		"dwResvID",		/*预约ID号*/
		
		"dwLabID",		/*实验室ID号*/
		
		"dwDevID",		/*客户端设备的ID号*/
		
		"szMemo",		/*备注*/
	 ""};

	/*手机登录退出响应*/
	static public string[] RESVUSERGOOUTRES = new string[]{
		
		"szDispInfo",		/*显示信息*/
	 ""};

	/*摇一摇签到请求*/
	static public string[] SHAKECHECKINREQ = new string[]{
		
		"dwResvID",		/*预约ID号*/
		
		"szMemo",		/*说明信息*/
	 ""};

	/*摇一摇签到应答*/
	static public string[] SHAKECHECKINRES = new string[]{
		
		"szDispInfo",		/*显示信息*/
	 ""};

	/*摇一摇进馆请求*/
	static public string[] SHAKECOMEINREQ = new string[]{
		
		"dwRoomID",		/*RoomID（扩展）*/
		
		"szMemo",		/*说明信息*/
	 ""};

	/*摇一摇进馆应答*/
	static public string[] SHAKECOMEINRES = new string[]{
		
		"szDispInfo",		/*显示信息*/
	 ""};

}
	/*结束数据结构*/

	/*开始数据结构*/
public partial class Struct_ST
{

	/*获取预约记录*/
	static public string[] RESVRECREQ = new string[]{
		
      "dwGetType",		/*方式*/
		
		"szGetKey",		/*条件值*/
	
		"dwAccNo",		/*账号*/
		
		"dwUseMode",		/*使用模式*/
		
		"dwPurpose",		/*预约用途*/
		
		"dwDevKind",		/*设备类型*/
		
		"dwStatus",		/*状态*/
		
		"dwCheckStat",		/*审查状态*/
		
		"dwCommentStat",		/*评价状态*/
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*设备预约信息*/
	static public string[] UNIRESVREC = new string[]{
		
		"dwSID",		/*流水号*/
		
		"dwResvID",		/*预约号*/
		
		"dwPreDate",		/*预约日期*/
		
		"dwPreBegin",		/*预约开始时间*/
		
		"dwPreEnd",		/*预约结束时间*/
		
		"dwUseMode",		/*使用模式*/
		
		"dwPurpose",		/*预约用途*/
		
		"dwAccNo",		/*账号*/
		
		"szPID",		/*学工号*/
	
		"szTrueName",		/*姓名*/
	
		"dwDevID",		/*设备ID*/
		
		"dwDevKind",		/*设备类型*/
		
		"dwDevSN",		/*设备编号*/
		
		"szDevName",		/*设备名称*/
	
		"dwRoomID",		/*房间ID*/
		
		"szRoomName",		/*房间名称*/
	
		"dwLabID",		/*实验室ID*/
		
		"szLabName",		/*实验室名称*/
	
      "dwStatus",		/*状态*/
		
		"dwResvTime",		/*预约总时间*/
		
		"dwUseTime",		/*使用总时间*/
		
		"dwCheckStat",		/*审查状态*/
		
      "dwCommentStat",		/*用户评价状态*/
		
		"dwTotalFee",		/*费用合计*/
		
		"szMemo",		/*备注*/
	
		"dwInTime",		/*签到时间*/
		
      "dwInMode",		/*签到方式*/
		
		"dwOutTime",		/*退出时间*/
		
		"dwOutMode",		/*退出方式*/
		
		"dwLeaveTime",		/*暂时离开时间*/
		
		"dwLeaveMode",		/*暂时离开方式*/
		
		"dwBackTime",		/*返回时间*/
		
		"dwBackMode",		/*返回方式*/
		 ""};

	/*获取预约类型统计记录*/
	static public string[] RESVKINDSTATREQ = new string[]{
		
		"dwPurpose",		/*预约用途*/
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*预约类型统计*/
	static public string[] RESVKINDSTAT = new string[]{
		
		"dwKind",		/*预约类型*/
		
		"dwResvTimes",		/*预约次数*/
		
		"dwResvMinutes",		/*预约总时间(分钟)*/
		
		"dwTestHour",		/*实验学时数*/
		
		"dwResvDevs",		/*预约机器数*/
		
		"dwUseDevs",		/*实际用机数*/
		
		"dwResvUsers",		/*上课总人数*/
		
		"dwRealUsers",		/*实际到课人数*/
		 ""};

	/*获取预约方式统计的请求包*/
	static public string[] RESVMODESTATREQ = new string[]{
		
		"dwOwner",		/*预约人(所有者)*/
		
		"dwMemberID",		/*成员ID*/
		
		"dwDevID",		/*设备ID*/
		
		"dwUseMode",		/*使用方法*/
		
		"dwPurpose",		/*预约用途*/
		
		"dwDevKind",		/*设备类型*/
		
		"dwClassKind",		/*类别(见UNIDEVCLS的Kind定义)*/
		
		"dwBeginDate",		/*预约开始日期*/
		
		"dwEndDate",		/*预约结束日期*/
		
		"szRoomNos",		/*房间编号,多个用逗号隔开*/
	
		"dwKind",		/*预约类型*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*预约方式统计*/
	static public string[] RESVMODESTAT = new string[]{
		
		"dwUseMode",		/*预约方式*/
		
		"dwUsers",		/*预约人数*/
		
		"dwResvTimes",		/*预约次数*/
		
		"dwResvMinutes",		/*预约总时间(分钟)*/
		 ""};

	/*查询统计的 请求*/
	static public string[] REPORTREQ = new string[]{
		
      "dwGetType",		/*方式*/
		
		"szGetKey",		/*条件值*/
	
		"dwAccNo",		/*账号*/
		
		"dwPurpose",		/*用途*/
		
		"dwDevKind",		/*设备类型*/
		
		"dwClassKind",		/*类别(见UNIDEVCLS的Kind定义)*/
		
		"dwCheckStat",		/*管理员检查状态(CHECKINFO定义)*/
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
		"dwMinPrice",		/*最低单价*/
		
		"dwMaxPrice",		/*最高单价*/
		
		"dwStartPurchaseDate",		/*最早购买日期*/
		
		"dwEndPurchaseDate",		/*截止购买日期*/
		
		"szRoomIDs",		/*房间ID,多个用逗号隔开*/
	
		"szBuildingIDs",		/*楼宇ID,多个用逗号隔开*/
	
		"szCampusIDs",		/*校区ID,多个用逗号隔开*/
	
		"dwActivitySN",		/*活动类型编号*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*设备使用记录 请求*/
	static public string[] USERECREQ = new string[]{
		
		"dwDevID",		/*设备ID*/
		
		"dwRoomID",		/*房间ID*/
		
		"dwLabID",		/*实验室ID*/
		
		"dwAccNo",		/*账号*/
		
		"dwDeptID",		/*部门ID*/
		
		"dwPurpose",		/*用途*/
		
		"dwDevKind",		/*设备类型*/
		
		"dwClassKind",		/*类别(见UNIDEVCLS的Kind定义)*/
		
		"dwCheckStat",		/*管理员检查状态(CHECKINFO定义)*/
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
		"dwMinPrice",		/*最低单价*/
		
		"dwMaxPrice",		/*最高单价*/
		
		"dwStartPurchaseDate",		/*最早购买日期*/
		
		"dwEndPurchaseDate",		/*截止购买日期*/
		
		"dwAttendantID",		/*值班员账号*/
		
		"szMAC",		/*网卡地址*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*设备使用记录*/
	static public string[] DEVUSEREC = new string[]{
		
		"dwSID",		/*流水号*/
		
		"dwAccNo",		/*帐号*/
		
		"szPID",		/*学工号*/
	
		"szTrueName",		/*姓名*/
	
		"dwTutorID",		/*导师（帐号）*/
		
		"szTutorName",		/*导师姓名*/
	
		"dwDeptID",		/*部门ID*/
		
		"szDeptName",		/*部门*/
	
		"dwDevID",		/*设备ID*/
		
		"dwDevSN",		/*设备编号*/
		
		"szDevName",		/*设备名称*/
	
		"szMAC",		/*网卡地址*/
	
		"szKindName",		/*设备名称*/
	
		"szClassName",		/*设备类别名称*/
	
		"szModel",		/*设备型号*/
	
		"szSpecification",		/*设备规格*/
	
		"dwUnitPrice",		/*设备单价(元)*/
		
		"dwPurchaseDate",		/*采购日期 YYYYMMDD*/
		
		"dwRoomID",		/*房间ID*/
		
		"szRoomName",		/*房间称*/
	
		"dwLabID",		/*实验室ID*/
		
		"szLabName",		/*实验室名称*/
	
		"dwAttendantID",		/*值班员账号*/
		
		"szAttendantName",		/*值班员姓名*/
	
		"dwPurpose",		/*用途*/
		
		"dwBeginTime",		/*开始时间*/
		
		"dwEndTime",		/*结束时间*/
		
		"dwUseTime",		/*使用时间*/
		
		"dwTotalCost",		/*总费用*/
		
		"dwBeginAdmin",		/*外借管理员ID*/
		
		"szBeginAdminName",		/*外借管理员姓名*/
	
		"dwEndAdmin",		/*归还管理员ID*/
		
		"szEndAdminName",		/*归还管理员姓名*/
	
		"dwCheckStat",		/*管理员检查状态*/
		
		"szMemo",		/*说明信息*/
	 ""};

	/*查询明细的条件*/
	static public string[] DOORCARDRECREQ = new string[]{
		
      "dwGetType",		/*方式*/
		
		"szGetKey",		/*条件值*/
	
		"dwAccNo",		/*账号*/
		
		"dwCardMode",		/*由DOORCARDREQ结构定义*/
		
		"dwUserKind",		/*在DOORCARDRES定义*/
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
		"dwStartTime",		/*开始时间*/
		
		"dwEndTime",		/*结束时间*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*门禁刷卡记录*/
	static public string[] DOORCARDREC = new string[]{
		
		"dwSID",		/*流水号*/
		
		"dwAccNo",		/*帐号*/
		
		"szPID",		/*学工号*/
	
		"szCardNo",		/*卡号*/
	
		"szTrueName",		/*姓名*/
	
		"dwTutorID",		/*导师（帐号）*/
		
		"szTutorName",		/*导师姓名*/
	
		"dwDeptID",		/*部门ID*/
		
		"szDeptName",		/*部门*/
	
		"dwUsedDate",		/*刷卡日期*/
		
		"dwCardTime",		/*刷卡时间*/
		
		"dwLabID",		/*实验室ID*/
		
		"szLabName",		/*实验室名称*/
	
		"dwRoomID",		/*房间ID*/
		
		"szRoomNo",		/*房间编号*/
	
		"szRoomName",		/*房间名称*/
	
		"dwManMode",		/*控制方式(UNIROOM中定义)*/
		
		"dwCardMode",		/*刷卡模式*/
		
		"dwUserKind",		/*用户种类*/
		
		"dwResvID",		/*预约ID*/
		
		"szMemo",		/*扩展信息*/
	 ""};

	/*课外使用统计*/
	static public string[] USERSTAT = new string[]{
		
		"dwAccNo",		/*帐号*/
		
		"szPID",		/*学工号*/
	
		"szTrueName",		/*姓名*/
	
		"szDeptName",		/*部门*/
	
		"dwUseTimes",		/*使用人次*/
		
		"dwUseTime",		/*使用总时间*/
		 ""};

	/*实验室使用率统计*/
	static public string[] LABSTAT = new string[]{
		
		"dwLabID",		/*实验室ID*/
		
		"szLabSN",		/*编号*/
	
		"szLabName",		/*名称*/
	
		"dwTotalNum",		/*数量(人数或设备数)*/
		
		"dwTotalTestHour",		/*使用总人学时数*/
		
		"dwPIDNum",		/*课外使用人数*/
		
		"dwUseTimes",		/*课外使用人次*/
		
		"dwTotalUseTime",		/*课外使用总时间*/
		 ""};

	/*实验室(房间)使用率统计*/
	static public string[] ROOMSTAT = new string[]{
		
		"dwRoomID",		/*房间ID*/
		
		"szLabSN",		/*实验室编号*/
	
		"szLabName",		/*实验室名称*/
	
		"szRoomNo",		/*房间号*/
	
		"szRoomName",		/*房间名称*/
	
		"dwTotalNum",		/*数量(设备数)*/
		
		"dwTotalTestHour",		/*使用总人学时数*/
		
		"dwTestUseTimes",		/*教学实验使用人次*/
		
		"dwUseTimes",		/*课外使用人次*/
		
		"dwTotalUseTime",		/*课外使用总时间*/
		 ""};

	/*设备类型使用率统计*/
	static public string[] DEVKINDSTAT = new string[]{
		
		"dwKindID",		/*设备类型ID*/
		
		"szKindName",		/*设备名称*/
	
		"szModel",		/*设备型号*/
	
		"szSpecification",		/*设备规格*/
	
		"dwTotalNum",		/*数量(人数或设备数)*/
		
		"dwTotalTestHour",		/*使用总人学时数*/
		
		"dwPIDNum",		/*课外使用人数*/
		
		"dwUseTimes",		/*课外使用人次*/
		
		"dwTotalUseTime",		/*课外使用总时间*/
		 ""};

	/*设备类别使用率统计*/
	static public string[] DEVCLASSSTAT = new string[]{
		
		"dwClassID",		/*设备类别ID*/
		
		"dwClassKind",		/*类别(见UNIDEVCLS的Kind定义)*/
		
		"szClassName",		/*设备类别名称*/
	
		"dwTotalNum",		/*数量(人数或设备数)*/
		
		"dwTotalTestHour",		/*使用总人学时数*/
		
		"dwPIDNum",		/*课外使用人数*/
		
		"dwUseTimes",		/*课外使用人次*/
		
		"dwTotalUseTime",		/*课外使用总时间*/
		 ""};

	/*设备使用率统计*/
	static public string[] DEVSTAT = new string[]{
		
		"dwDevID",		/*设备ID*/
		
		"dwDevSN",		/*设备编号*/
		
		"szDevName",		/*设备名称*/
	
		"szKindName",		/*设备类别名称*/
	
		"szClassName",		/*设备类别名称*/
	
		"szModel",		/*设备型号*/
	
		"szSpecification",		/*设备规格*/
	
		"dwUnitPrice",		/*设备单价*/
		
		"dwPurchaseDate",		/*采购日期 YYYYMMDD*/
		
		"dwLabID",		/*实验室ID*/
		
		"szLabSN",		/*实验室编号*/
	
		"szLabName",		/*实验室名称*/
	
		"dwDeptID",		/*部门ID*/
		
		"szDeptName",		/*部门*/
	
		"dwAttendantID",		/*值班员账号*/
		
		"szAttendantName",		/*值班员姓名*/
	
		"dwTotalTestHour",		/*使用总人学时数*/
		
		"dwPIDNum",		/*课外使用人数*/
		
		"dwUseTimes",		/*课外使用人次*/
		
		"dwTotalUseTime",		/*课外使用总时间*/
		
		"dwTotalCost",		/*总费用*/
		 ""};

	/*学院使用统计*/
	static public string[] DEPTSTAT = new string[]{
		
		"dwDeptID",		/*学院ID*/
		
		"szDeptSN",		/*学院编号*/
	
		"szDeptName",		/*学院名称*/
	
		"dwTotalUsers",		/*学院人数*/
		
		"dwPIDNum",		/*使用人数*/
		
		"dwUseTimes",		/*使用人次*/
		
		"dwTotalUseTime",		/*使用总时间*/
		 ""};

	/*查询身份统计的 请求*/
	static public string[] IDENTSTATREQ = new string[]{
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
		"dwPurpose",		/*用途*/
		
		"dwDevKind",		/*设备类型*/
		
		"dwClassKind",		/*类别(见UNIDEVCLS的Kind定义)*/
		
		"dwCheckStat",		/*管理员检查状态(CHECKINFO定义)*/
		
		"dwDeptID",		/*人员所属部门ID*/
		
		"dwActivitySN",		/*活动类型编号*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*身份使用统计*/
	static public string[] IDENTSTAT = new string[]{
		
		"dwIdent",		/*身份*/
		
		"dwTotalUsers",		/*总人数*/
		
		"dwPIDNum",		/*使用人数*/
		
		"dwUseTimes",		/*使用人次*/
		
		"dwTotalUseTime",		/*使用总时间*/
		 ""};

	/*获取实验项目表*/
	static public string[] TESTITEMSTATREQ = new string[]{
		
		"dwTeacherID",		/*教师（帐号）*/
		
		"dwCourseID",		/*课程ID*/
		
		"dwLabID",		/*实验室ID*/
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*实验项目表*/
	static public string[] TESTITEMSTAT = new string[]{
		
		"dwTestItemID",		/*实验项目ID*/
		
		"dwTestCardID",		/*实验项目卡ID*/
		
		"szTestName",		/*实验名称*/
	
		"dwGroupPeopleNum",		/*每组人数*/
		
		"dwTestHour",		/*本实验项目学时数*/
		
		"dwTestClass",		/*实验类别*/
		
		"dwTestKind",		/*实验类型*/
		
		"dwRequirement",		/*实验要求*/
		
		"dwTestPlanID",		/*实验计划ID*/
		
		"szAcademicSubjectCode",		/*所属学科编码*/
	
		"dwTesteeKind",		/*实验者类别*/
		
		"dwTeacherID",		/*教师（帐号）*/
		
		"szTeacherName",		/*教师姓名*/
	
		"dwCourseID",		/*课程ID*/
		
		"szCourseCode",		/*课程代码*/
	
		"szCourseName",		/*课程名称*/
	
		"dwCourseProperty",		/*课程属性*/
		
		"dwGroupID",		/*上课班级*/
		
		"szGroupName",		/*上课班级名称*/
	
		"dwGroupUsers",		/*组用户数*/
		
		"dwLabID",		/*实验室ID*/
		
		"szLabSN",		/*实验室编号*/
	
		"szLabName",		/*实验室名称*/
	
		"dwDevNum",		/*预约设备数*/
		 ""};

	/*获取教学预约记录*/
	static public string[] TEACHINGRESVRECREQ = new string[]{
		
		"dwTeacherID",		/*教师（帐号）*/
		
		"dwCourseID",		/*课程ID*/
		
		"dwLabID",		/*实验室ID*/
		
		"dwTestPlanID",		/*实验计划ID*/
		
		"dwYearTerm",		/*学期编号*/
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
		"dwMinUseRate",		/*实到最低使用率*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*每分钟实到人数*/
	static public string[] USERSPERMINUTE = new string[]{
		
		"dwUsers",		/*实到人数*/
		 ""};

	/*教学预约记录*/
	static public string[] TEACHINGRESVREC = new string[]{
		
		"dwTestItemID",		/*实验项目ID*/
		
		"dwTestCardID",		/*实验项目卡ID*/
		
		"szTestName",		/*实验名称*/
	
		"dwGroupPeopleNum",		/*每组人数*/
		
		"dwTestHour",		/*本实验项目学时数*/
		
		"dwTestClass",		/*实验类别*/
		
		"dwTestKind",		/*实验类型*/
		
		"dwRequirement",		/*实验要求*/
		
		"dwTestPlanID",		/*实验计划ID*/
		
		"szAcademicSubjectCode",		/*所属学科*/
	
		"dwTesteeKind",		/*实验者类别*/
		
		"dwTeacherID",		/*教师（帐号）*/
		
		"szTeacherName",		/*教师姓名*/
	
		"dwCourseID",		/*课程ID*/
		
		"szCourseCode",		/*课程代码*/
	
		"szCourseName",		/*课程名称*/
	
		"dwCourseProperty",		/*课程属性*/
		
		"dwGroupID",		/*上课班级*/
		
		"szGroupName",		/*上课班级名称*/
	
		"dwGroupUsers",		/*组用户数*/
		
		"dwLabID",		/*实验室ID*/
		
		"szLabSN",		/*实验室编号*/
	
		"szLabName",		/*实验室名称*/
	
		"dwResvID",		/*预约ID*/
		
		"dwResvStat",		/*预约状态*/
		
		"dwPreDate",		/*预约开始日期*/
		
		"dwBeginTime",		/*预约开始时间*/
		
		"dwEndTime",		/*预约结束时间*/
		
		"dwTeachingTime",		/*教学时间(格式见UNIRESERVE)*/
		
		"dwDevNum",		/*预约设备数*/
		
		"dwAttendUsers",		/*实到人数*/
		
	"UsersPerMinute",		/*CUniTable[USERSPERMINUTE]*/
	 ""};

	/*获取设备使用率请求*/
	static public string[] DEVUSINGRATEREQ = new string[]{
		
      "dwGetType",		/*方式*/
		
		"szGetKey",		/*条件值*/
	
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
		"dwClassKind",		/*类别(见UNIDEVCLS的Kind定义)*/
		
		"szRoomIDs",		/*房间ID,多个用逗号隔开*/
	
		"szBuildingIDs",		/*楼宇ID,多个用逗号隔开*/
	
		"szCampusIDs",		/*校区ID,多个用逗号隔开*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*设备使用率数据表*/
	static public string[] DEVUSINGTABLE = new string[]{
		
		"dwUseTimes",		/*设备使用总次数*/
		
		"dwResvTimes",		/*设备预约总次数*/
		 ""};

	/*设备使用率统计表*/
	static public string[] DEVUSINGRATE = new string[]{
		
		"dwDevNums",		/*统计设备总数*/
		
		"dwDays",		/*统计天数*/
		
	"szUsingTable",		/*设备使用率数据表(CUniTable[DEVUSINGTABLE]),维数为24*60*/
	 ""};

	/*获取设备周使用率请求*/
	static public string[] DEVWEEKUSINGRATEREQ = new string[]{
		
      "dwGetType",		/*方式*/
		
		"szGetKey",		/*条件值*/
	
		"dwStartDate",		/*开始日期*/
		
		"dwWeeks",		/*查询周数*/
		
		"dwClassKind",		/*类别(见UNIDEVCLS的Kind定义)*/
		
		"szRoomIDs",		/*房间ID,多个用逗号隔开*/
	
		"szBuildingIDs",		/*楼宇ID,多个用逗号隔开*/
	
		"szCampusIDs",		/*校区ID,多个用逗号隔开*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*设备周使用率统计表*/
	static public string[] DEVWEEKUSINGRATE = new string[]{
		
		"dwDevNums",		/*统计设备总数*/
		
		"dwWeeks",		/*统计周数*/
		
	"szUsingTable",		/*设备使用率数据表(CUniTable[DEVUSINGTABLE]),维数为7*/
	 ""};

	/*场馆活动类型统计 请求*/
	static public string[] YARDACTIVITYSTATREQ = new string[]{
		
		"dwDevKind",		/*设备类型*/
		
		"dwClassKind",		/*类别(见UNIDEVCLS的Kind定义)*/
		
		"dwCheckStat",		/*管理员检查状态(CHECKINFO定义)*/
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
		"szRoomIDs",		/*房间ID,多个用逗号隔开*/
	
		"szBuildingIDs",		/*楼宇ID,多个用逗号隔开*/
	
		"szCampusIDs",		/*校区ID,多个用逗号隔开*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*场馆活动类型统计*/
	static public string[] YARDACTIVITYSTAT = new string[]{
		
		"dwActivitySN",		/*活动类型编号*/
		
		"szActivityName",		/*活动类型名称*/
	
		"dwPIDNum",		/*使用人数*/
		
		"dwUseTimes",		/*使用人次*/
		
		"dwTotalUseTime",		/*使用总时间*/
		 ""};

	/*获取设备月使用统计*/
	static public string[] DEVMONTHSTATREQ = new string[]{
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*设备月使用统计*/
	static public string[] DEVMONTHSTAT = new string[]{
		
		"dwYearMonth",		/*统计月份*/
		
		"dwWResvTime",		/*工作日设备预约总时间(分钟)*/
		
		"dwRResvTime",		/*非作日设备预约总时间(分钟)*/
		
		"dwWUseTime",		/*工作日设备使用总时间(分钟)*/
		
		"dwRUseTime",		/*非工作日设备使用总时间(分钟)*/
		 ""};

	/*获取设备科研实验统计请求*/
	static public string[] RTUSESTATREQ = new string[]{
		
      "dwStatType",		/*统计方式*/
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
		"dwDeptID",		/*设备所属部门ID*/
		
		"szLabIDs",		/*实验室ID,多个用逗号隔开*/
	
		"szRoomIDs",		/*房间ID,多个用逗号隔开*/
	
		"szKindIDs",		/*所属类型,多个用逗号隔开*/
	
		"szClassIDs",		/*所属功能类别ID,多个用逗号隔开*/
	
		"szDevIDs",		/*设备ID,多个用逗号隔开*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO],szExtInfo返回RTUSESTAT合计*/
	 ""};

	/*设备科研实验统计*/
	static public string[] RTUSESTAT = new string[]{
		
		"dwStatID",		/*统计对象ID*/
		
		"szStatName",		/*统计对象名称*/
	
		"szExtName",		/*扩展信息(比如仪器管理员）*/
	
		"dwResvTimes",		/*预约次数*/
		
		"dwResvMinutes",		/*预约总时间(分钟)*/
		
		"dwUseTimes",		/*使用次数*/
		
		"dwUseMinutes",		/*设备使用总时间(分钟)*/
		
		"dwSampleNum",		/*测试样品数*/
		
		"dwReceivableCost",		/*应缴费用*/
		
		"dwUseFee",		/*系统自动计算（应缴费用）*/
		
		"dwRealCost",		/*结算费用*/
		
		"dwDevUseFee",		/*设备使用费*/
		
		"dwSampleFee",		/*样品费*/
		
		"dwAssistFee",		/*协助费*/
		
		"dwEntrustFee",		/*代检费*/
		
		"dwNegotiationFee",		/*协议收费*/
		 ""};

	/*获取设备科研实验明细请求*/
	static public string[] RTUSEDETAILREQ = new string[]{
		
		"dwDevID",		/*设备ID*/
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*设备科研实验明细*/
	static public string[] RTUSEDETAIL = new string[]{
		
		"dwDevID",		/*设备ID*/
		
		"szDevName",		/*设备名称*/
	
		"szAttendantName",		/*仪器管理员*/
	
		"dwResvID",		/*预约号*/
		
		"szTestName",		/*科研实验名称*/
	
		"dwOwner",		/*预约人(创建者)*/
		
		"szOwnerName",		/*预约人姓名*/
	
		"dwPreDate",		/*预约开始日期*/
		
		"dwBeginTime",		/*预约开始时间*/
		
		"dwRTID",		/*科研实验项目ID*/
		
		"szRTName",		/*科研实验名称*/
	
		"dwHolderID",		/*主持人（帐号）*/
		
		"szHolderName",		/*主持人姓名*/
	
		"dwManID",		/*管理员ID*/
		
		"szManName",		/*管理员姓名*/
	
		"dwResvMinutes",		/*预约总时间(分钟)*/
		
		"dwUseMinutes",		/*设备使用总时间(分钟)*/
		
		"dwSampleNum",		/*测试样品数*/
		
		"dwReceivableCost",		/*应缴费用*/
		
		"dwUseFee",		/*系统自动计算（应缴费用）*/
		
		"dwRealCost",		/*结算费用*/
		
		"dwDevUseFee",		/*设备使用费*/
		
		"dwSampleFee",		/*样品费*/
		
		"dwAssistFee",		/*协助费*/
		
		"dwEntrustFee",		/*代检费*/
		
		"dwNegotiationFee",		/*协议收费*/
		 ""};

	/*获取设备科研实验经费分配统计请求*/
	static public string[] RTFASTATREQ = new string[]{
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
		"dwDeptID",		/*设备所属部门ID*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO],szExtInfo返回RTFASTAT合计*/
	 ""};

	/*设备科研实验经费分配统计*/
	static public string[] RTFASTAT = new string[]{
		
		"dwDevID",		/*设备ID*/
		
		"szDevName",		/*设备名称*/
	
		"szAttendantName",		/*仪器管理员*/
	
		"dwResvTimes",		/*预约次数*/
		
		"dwResvMinutes",		/*预约总时间(分钟)*/
		
		"dwUseTimes",		/*使用次数*/
		
		"dwUseMinutes",		/*设备使用总时间(分钟)*/
		
		"dwSampleNum",		/*测试样品数*/
		
		"dwTotalFee",		/*收费总金额*/
		
		"dwTestFee",		/*分析测试费*/
		
		"dwOpenFundFee",		/*开放基金*/
		
		"dwServiceFee",		/*劳务费*/
		 ""};

	/*获取设备科研实验经费分配明细请求*/
	static public string[] RTFADETAILREQ = new string[]{
		
		"dwDevID",		/*设备ID*/
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*设备科研实验经费分配明细*/
	static public string[] RTFADETAIL = new string[]{
		
		"dwDevID",		/*设备ID*/
		
		"szDevName",		/*设备名称*/
	
		"szAttendantName",		/*仪器管理员*/
	
		"dwResvID",		/*预约号*/
		
		"szTestName",		/*科研实验名称*/
	
		"dwOwner",		/*预约人(创建者)*/
		
		"szOwnerName",		/*预约人姓名*/
	
		"dwPreDate",		/*预约开始日期*/
		
		"dwBeginTime",		/*预约开始时间*/
		
		"dwRTID",		/*科研实验项目ID*/
		
		"szRTName",		/*科研实验名称*/
	
		"dwHolderID",		/*主持人（帐号）*/
		
		"szHolderName",		/*主持人姓名*/
	
		"dwManID",		/*管理员ID*/
		
		"szManName",		/*管理员姓名*/
	
		"dwResvMinutes",		/*预约总时间(分钟)*/
		
		"dwUseMinutes",		/*设备使用总时间(分钟)*/
		
		"dwSampleNum",		/*测试样品数*/
		
		"dwTotalFee",		/*收费总金额*/
		
		"dwTestFee",		/*分析测试费*/
		
		"dwOpenFundFee",		/*开放基金*/
		
		"dwServiceFee",		/*劳务费*/
		 ""};

	/*查询违约统计的请求*/
	static public string[] DEFAULTSTATREQ = new string[]{
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
		"dwCTSN",		/*信用类别编号*/
		
		"dwUsePurpose",		/*用途*/
		
		"dwForClsKind",		/*适用设备类别*/
		
		"dwDeptID",		/*人员所属部门ID*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*违约统计*/
	static public string[] DEFAULTSTAT = new string[]{
		
		"dwCreditSN",		/*信用类型编号*/
		
		"szCreditName",		/*信用类型名称*/
	
		"dwResvTimes",		/*预约总数*/
		
		"dwDefaultTimes",		/*违约次数*/
		 ""};

	/*教学科研仪器设备表*/
	static public string[] DEVLISTREQ = new string[]{
		
		"dwYearTerm",		/*学期编号*/
		
		"dwReportStat",		/*报表状态*/
		
		"szLabIDs",		/*实验室ID,多个用逗号隔开*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*教学科研仪器设备清单*/
	static public string[] DEVLIST = new string[]{
		
		"dwYearTerm",		/*学期编号*/
		
      "dwReportStat",		/*报表状态*/
		
		"dwDevID",		/*设备ID*/
		
		"szAssertSN",		/*用户方资产编号*/
	
		"szClassSN",		/*设备分类号*/
	
		"szDevName",		/*实验设备名称*/
	
		"szModel",		/*设备型号*/
	
		"szSpecification",		/*设备规格*/
	
		"dwComeFrom",		/*仪器来源*/
		
		"dwNationCode",		/*国别码*/
		
		"dwUnitPrice",		/*设备单价*/
		
		"dwPurchaseDate",		/*采购日期*/
		
		"dwStatCode",		/*现状码*/
		
		"dwUseFor",		/*使用方向*/
		
		"dwLabID",		/*实验室ID*/
		
		"szLabSN",		/*实验室编号*/
	
		"szLabName",		/*实验室名称*/
	
		"dwDeptID",		/*部门ID*/
		
		"szDeptSN",		/*部门编号*/
	
		"szDeptName",		/*部门*/
	 ""};

	/*教学科研仪器设备增减变动情况表*/
	static public string[] DEVCHGREQ = new string[]{
		
		"dwYearTerm",		/*学期编号*/
		
		"dwReportStat",		/*报表状态*/
		
		"dwUnitPrice",		/*大型仪器价格起点*/
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		 ""};

	/*教学科研仪器设备增减变动情况表*/
	static public string[] DEVCHG = new string[]{
		
		"dwYearTerm",		/*学期编号*/
		
		"dwReportStat",		/*报表状态*/
		
		"dwBDevNum",		/*期初数量*/
		
		"dwBMoney",		/*期初金额*/
		
		"dwBBigDevNum",		/*大型仪器期初数量*/
		
		"dwBBigMoney",		/*大型仪器期初金额*/
		
		"dwIncDevNum",		/*增加数量*/
		
		"dwIncMoney",		/*增加金额*/
		
		"dwIncBigDevNum",		/*大型仪器增加数量*/
		
		"dwIncBigMoney",		/*大型仪器增加金额*/
		
		"dwDecDevNum",		/*减少数量*/
		
		"dwDecMoney",		/*减少金额*/
		
		"dwDecBigDevNum",		/*大型仪器减少数量*/
		
		"dwDecBigMoney",		/*大型仪器减少金额*/
		
		"dwEDevNum",		/*期末数量*/
		
		"dwEMoney",		/*期末金额*/
		
		"dwEBigDevNum",		/*大型仪器期末数量*/
		
		"dwEBigMoney",		/*大型仪器期末金额*/
		 ""};

	/*贵重仪器设备表*/
	static public string[] BIGDEVREQ = new string[]{
		
		"dwYearTerm",		/*学期编号*/
		
		"dwReportStat",		/*报表状态*/
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
		"dwUnitPrice",		/*大型仪器价格起点*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*贵重仪器设备表*/
	static public string[] BIGDEV = new string[]{
		
		"dwYearTerm",		/*学期编号*/
		
		"dwReportStat",		/*报表状态*/
		
		"dwDevID",		/*设备ID*/
		
		"szAssertSN",		/*用户方资产编号*/
	
		"szClassSN",		/*设备分类号*/
	
		"szDevName",		/*实验设备名称*/
	
		"dwUnitPrice",		/*设备单价*/
		
		"szModel",		/*设备型号*/
	
		"szSpecification",		/*设备规格*/
	
		"szAttendantName",		/*负责人姓名*/
	
		"dwSampleNum",		/*测样数*/
		
		"dwTUseTime",		/*教学机时*/
		
		"dwRUseTime",		/*科研机时*/
		
		"dwSUseTime",		/*社会机时*/
		
		"dwOUseTime",		/*开放机时*/
		
		"dwUseTeachers",		/*使用教师人数*/
		
		"dwUseStudents",		/*使用学生人数*/
		
		"dwUseOthers",		/*使用其他人数*/
		
		"dwTItemNum",		/*教学实验项目*/
		
		"dwRItemNum",		/*科研实验项目*/
		
		"dwSItemNum",		/*社会实验项目*/
		
		"dwNReward",		/*国家级奖励*/
		
		"dwPReward",		/*省级奖励*/
		
		"dwTPatent",		/*教师专利*/
		
		"dwSPatent",		/*学生专利*/
		
		"dwThreeIndex",		/*三大检索*/
		
		"dwKernelJournal",		/*核心刊物*/
		 ""};

	/*获取实验项目表*/
	static public string[] TESTITEMREPORTREQ = new string[]{
		
		"dwYearTerm",		/*学期编号*/
		
		"dwReportStat",		/*报表状态*/
		
		"szLabIDs",		/*实验室ID,多个用逗号隔开*/
	
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*实验项目表*/
	static public string[] TESTITEMREPORT = new string[]{
		
		"dwYearTerm",		/*学期编号*/
		
		"dwReportStat",		/*报表状态*/
		
		"dwTestCardID",		/*实验项目卡ID*/
		
		"szTestSN",		/*实验编号*/
	
		"szTestName",		/*实验名称*/
	
		"dwTestClass",		/*实验类别*/
		
		"dwTestKind",		/*实验类型*/
		
		"dwRequirement",		/*实验要求*/
		
		"szAcademicSubjectCode",		/*所属学科编码*/
	
		"dwTesteeKind",		/*实验者类别*/
		
		"dwGroupPeopleNum",		/*每组人数*/
		
		"dwTestHour",		/*本实验项目学时数*/
		
		"dwTesteeNum",		/*试验者人数*/
		
		"dwLabID",		/*实验室ID*/
		
		"szLabSN",		/*实验室编号*/
	
		"szLabName",		/*实验室名称*/
	 ""};

	/*专任实验室人员表*/
	static public string[] STAFFINFOREQ = new string[]{
		
		"dwYearTerm",		/*学期编号*/
		
		"dwReportStat",		/*报表状态*/
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
		"szLabIDs",		/*实验室ID,多个用逗号隔开*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*专任实验室人员表*/
	static public string[] STAFFINFO = new string[]{
		
		"dwYearTerm",		/*学期编号*/
		
		"dwReportStat",		/*报表状态*/
		
		"dwAccNo",		/*帐号*/
		
		"szPID",		/*人员编号(学工号)*/
	
		"dwLabID",		/*实验室ID*/
		
		"szLabSN",		/*实验室编号*/
	
		"szLabName",		/*实验室名称*/
	
		"szTrueName",		/*姓名*/
	
		"dwSex",		/*性别见UniCommon.h*/
		
		"dwBirthDate",		/*出生日期*/
		
      "dwJobTitle",		/*职称编码*/
		
      "dwDuty",		/*职务*/
		
      "dwJobType",		/*工作性质*/
		
		"szAcademicSubjectCode",		/*所属学科编码*/
	
		"dwProfessionalTitle",		/*专业技术职务*/
		
		"dwEducation",		/*文化程度*/
		
		"dwExpertType",		/*专家类别*/
		
		"dwInlandUduTime",		/*国内学历教育时间*/
		
		"dwInlandOtherTime",		/*国内非学历教育时间*/
		
		"dwAbroadUduTime",		/*国外学历教育时间*/
		
		"dwAbroadOtherTime",		/*国外非学历教育时间*/
		
		"szMemo",		/*备注*/
	 ""};

	/*实验室基本情况表*/
	static public string[] LABINFOREQ = new string[]{
		
		"dwYearTerm",		/*学期编号*/
		
		"dwReportStat",		/*报表状态*/
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
		"szLabIDs",		/*实验室ID,多个用逗号隔开*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*实验室基本情况表*/
	static public string[] LABINFO = new string[]{
		
		"dwYearTerm",		/*学期编号*/
		
		"dwReportStat",		/*报表状态*/
		
		"dwLabID",		/*实验室ID*/
		
		"szLabSN",		/*实验室编号*/
	
		"szLabName",		/*实验室名称*/
	
		"szLabKindCode",		/*实验室类型编码*/
	
		"szLabLevelCode",		/*实验室建设水平编码*/
	
		"szLabFromCode",		/*实验室来源编码*/
	
		"szAcademicSubjectCode",		/*所属学科编码*/
	
		"dwLabClass",		/*实验室类别*/
		
		"dwCreateDate",		/*建立年份*/
		
		"dwTNReward",		/*教师国家级奖励*/
		
		"dwTPReward",		/*教师省级奖励*/
		
		"dwTPatent",		/*教师专利*/
		
		"dwSNReward",		/*学生国家级奖励*/
		
		"dwSPReward",		/*学生省级奖励*/
		
		"dwSPatent",		/*学生专利*/
		
		"dwTThreeIndex",		/*教学三大检索*/
		
		"dwTKernelJournal",		/*教学核心期刊*/
		
		"dwRThreeIndex",		/*科研三大检索*/
		
		"dwRKernelJournal",		/*科研核心期刊*/
		
		"dwTestBookNum",		/*实验教材数*/
		
		"dwTItemNum",		/*教学实验项目*/
		
		"dwRItemNum",		/*科研实验项目*/
		
		"dwPTItemNum",		/*省部级以上教学实验项目*/
		
		"dwPRItemNum",		/*省部级以上科研实验项目*/
		
		"dwSItemNum",		/*社会实验项目*/
		
		"dwZKThesisUsers",		/*专科论文人数*/
		
		"dwBKThesisUsers",		/*本科论文人数*/
		
		"dwSSThesisUsers",		/*硕士研究生论文人数*/
		
		"dwBSThesisUsers",		/*博士研究生论文人数*/
		
		"dwItemNum",		/*实验个数*/
		
		"dwOtherItemNum",		/*校外实验个数*/
		
		"dwUseUsers",		/*实验人数*/
		
		"dwOtherUsers",		/*校外实验人数*/
		
		"dwUseTime",		/*实验人时数*/
		
		"dwOtherTime",		/*校外实验人时数*/
		
		"dwPartTimeUsers",		/*兼职人员数*/
		
		"dwTotalCost",		/*运行费用*/
		
		"dwConsumeCost",		/*耗材费*/
		 ""};

	/*实验室经费情况表*/
	static public string[] LABALLCOSTREQ = new string[]{
		
		"dwYearTerm",		/*学期编号*/
		
		"dwReportStat",		/*报表状态*/
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		 ""};

	/*实验室经费情况表*/
	static public string[] LABALLCOST = new string[]{
		
		"dwYearTerm",		/*学期编号*/
		
		"dwReportStat",		/*报表状态*/
		
		"dwLabNum",		/*实验室个数*/
		
		"dwLabArea",		/*实验室面积*/
		
		"dwTotalCost",		/*总计(元)*/
		
		"dwBuyCost",		/*购置费(元)*/
		
		"dwTBuyCost",		/*教仪购置费(元)*/
		
		"dwKeepCost",		/*维护费(元)*/
		
		"dwTKeepCost",		/*教仪维护费(元)*/
		
		"dwRunCost",		/*运行费(元)*/
		
		"dwCRunCost",		/*耗材费(元)*/
		
		"dwBuildCost",		/*建设费(元)*/
		
		"dwRAndRCost",		/*研究与改革费(元)*/
		
		"dwOtherCost",		/*其他费(元)*/
		 ""};

	/*高等学校实验室综合信息表*/
	static public string[] LABSUMMARYREQ = new string[]{
		
		"dwYearTerm",		/*学期编号*/
		
		"dwReportStat",		/*报表状态*/
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
		"dwUnitPrice",		/*设备单价*/
		 ""};

	/*高等学校实验室综合信息表*/
	static public string[] LABSUMMARY = new string[]{
		
		"dwYearTerm",		/*学期编号*/
		
		"dwReportStat",		/*报表状态*/
		
		"dwLabNum",		/*实验室个数*/
		
		"dwLabArea",		/*实验室面积*/
		
		"dwDevNum",		/*仪器数量*/
		
		"dwDevMoney",		/*仪器金额*/
		
		"dwBigDevNum",		/*大型仪器数量*/
		
		"dwBigMoney",		/*大型仪器金额*/
		
		"dwTItemNum",		/*教学实验项目*/
		
		"dwTUseTime",		/*教学实验人时数*/
		
		"dwDUseTime",		/*博士人时数*/
		
		"dwMUseTime",		/*硕士人时数*/
		
		"dwUUseTime",		/*本科人时数*/
		
		"dwJUseTime",		/*专科人时数*/
		
		"dwRItemNum",		/*科研实验项目*/
		
		"dwHTStaff",		/*高级教师工作人员*/
		
		"dwHSStaff",		/*高级实验技术人员*/
		
		"dwMTStaff",		/*中级教师工作人员*/
		
		"dwMSStaff",		/*中级实验技术人员*/
		
		"dwOtherStaff",		/*其他人员*/
		
		"dwPartTimeStaff",		/*兼职人员*/
		
		"dwPaperNum",		/*论文数*/
		
		"dwTReward",		/*教师获奖数*/
		
		"dwSReward",		/*学生获奖数*/
		 ""};

	/*高等学校实验室综合信息表2*/
	static public string[] LABSUMMARY2REQ = new string[]{
		
		"dwYearTerm",		/*学期编号*/
		
		"dwReportStat",		/*报表状态*/
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
		"dwUnitPrice",		/*设备单价*/
		 ""};

	/*高等学校实验室综合信息表2*/
	static public string[] LABSUMMARY2 = new string[]{
		
		"dwYearTerm",		/*学期编号*/
		
		"dwReportStat",		/*报表状态*/
		
		"dwLabNum",		/*实验室个数*/
		
		"dwLabArea",		/*实验室面积*/
		
		"dwDevNum",		/*仪器数量*/
		
		"dwDevMoney",		/*仪器金额*/
		
		"dwBigDevNum",		/*大型仪器数量*/
		
		"dwBigMoney",		/*大型仪器金额*/
		 ""};

}
	/*结束数据结构*/

	/*开始数据结构*/
public partial class Struct_ST
{

	/*获取配置项请求包*/
	static public string[] CFGREQ = new string[]{
		
      "dwGetType",		/*获取配置类别*/
		
		"szGetKey",		/*获取条件值*/
	 ""};

	/*配置项内容*/
	static public string[] CFGINFO = new string[]{
		
		"dwKindSN",		/*配置类型名称*/
		
      "dwCfgSN",		/*配置项名称*/
		
      "dwValueKind",		/*值类型*/
		
		"dwNumberValue",		/*配置项值，数值型有效*/
		
		"szStringValue",		/*配置项值，字符串型有效*/
	
		"szMemo",		/*说明*/
	 ""};

	/*获取信用类别*/
	static public string[] CREDITTYPEREQ = new string[]{
		
		"dwCTSN",		/*信用类别编号*/
		
		"dwCTStat",		/*状态*/
		 ""};

	/*独立的信用类别*/
	static public string[] CREDITTYPE = new string[]{
		
		"dwCTSN",		/*信用类别编号*/
		
		"szCTName",		/*信用类别名称*/
	
		"dwForClsKind",		/*适用设备类别*/
		
		"dwUsePurpose",		/*用途*/
		
		"dwMaxScore",		/*最大信用积分*/
		
      "dwScoreCycle",		/*信用计分周期*/
		
		"dwForbidUseTime",		/*信用积分为0禁止使用时间（天）*/
		
      "dwCTStat",		/*状态*/
		
		"szMemo",		/*说明*/
	 ""};

	/*信用类型*/
	static public string[] CREDITKIND = new string[]{
		
      "dwCreditSN",		/*信用类型编号*/
		
      "dwScoreType",		/*积分处理方式*/
		
      "dwCKStat",		/*状态*/
		
		"szCreditName",		/*信用类型名称*/
	
		"szMemo",		/*说明*/
	 ""};

	/*获取信用计分表*/
	static public string[] CREDITSCOREREQ = new string[]{
		
		"dwID",		/*ID*/
		
		"dwCTSN",		/*信用类别编号*/
		
		"dwCreditSN",		/*信用类型编号*/
		
		"dwForClsKind",		/*适用设备类别*/
		
		"dwUsePurpose",		/*用途*/
		 ""};

	/*信用计分表*/
	static public string[] CREDITSCORE = new string[]{
		
		"dwID",		/*ID*/
		
		"dwCTSN",		/*信用类别编号*/
		
		"szCTName",		/*信用类别名称*/
	
		"dwForClsKind",		/*适用设备类别*/
		
		"dwUsePurpose",		/*用途*/
		
		"dwMaxScore",		/*最大信用积分*/
		
		"dwScoreCycle",		/*信用计分周期*/
		
		"dwForbidUseTime",		/*信用积分为0禁止使用时间（天）*/
		
		"dwCreditSN",		/*信用类型编号*/
		
		"szCreditName",		/*信用类型名称*/
	
		"dwScoreType",		/*积分处理方式*/
		
		"dwUseNum",		/*条件启用段数*/
		
		"dwMinValue1",		/*满足条件最小值1*/
		
		"dwMaxValue1",		/*满足条件最大值1*/
		
		"dwCreditScore1",		/*扣或奖积分1*/
		
		"dwMinValue2",		/*满足条件最小值2*/
		
		"dwMaxValue2",		/*满足条件最大值2*/
		
		"dwCreditScore2",		/*扣或奖积分2*/
		
		"dwMinValue3",		/*满足条件最小值3*/
		
		"dwMaxValue3",		/*满足条件最大值3*/
		
		"dwCreditScore3",		/*扣或奖积分3*/
		
		"dwMinValue4",		/*满足条件最小值4*/
		
		"dwMaxValue4",		/*满足条件最大值4*/
		
		"dwCreditScore4",		/*扣或奖积分4*/
		
		"dwMinValue5",		/*满足条件最小值5*/
		
		"dwMaxValue5",		/*满足条件最大值5*/
		
		"dwCreditScore5",		/*扣或奖积分*/
		
		"dwMinValue6",		/*满足条件最小值6*/
		
		"dwMaxValue6",		/*满足条件最大值6*/
		
		"dwCreditScore6",		/*扣或奖积分6*/
		
		"szMemo",		/*说明*/
	 ""};

	/*获取我的信用积分*/
	static public string[] MYCREDITSCOREREQ = new string[]{
		
		"dwAccNo",		/*账号*/
		
		"dwCTSN",		/*信用类别编号*/
		 ""};

	/*我的信用积分*/
	static public string[] MYCREDITSCORE = new string[]{
		
		"dwAccNo",		/*账号*/
		
		"szPID",		/*学工号*/
	
		"szTrueName",		/*姓名*/
	
		"dwCTSN",		/*信用类别编号*/
		
		"szCTName",		/*信用类别名称*/
	
		"dwForClsKind",		/*适用设备类别*/
		
		"dwUsePurpose",		/*用途*/
		
		"dwMaxScore",		/*最大信用积分*/
		
		"dwScoreCycle",		/*信用计分周期*/
		
		"dwForbidUseTime",		/*信用积分为0禁止使用时间（天）*/
		
		"dwLeftCScore",		/*剩余积分*/
		
		"dwForbidStartDate",		/*禁用开始日期*/
		
		"dwForbidEndDate",		/*禁用结束日期*/
		
		"szMemo",		/*说明*/
	 ""};

	/*人工信用管理*/
	static public string[] ADMINCREDIT = new string[]{
		
		"dwCTSN",		/*信用类别编号*/
		
		"dwCreditSN",		/*信用类型编号*/
		
		"dwCreditScore",		/*扣或奖积分*/
		
		"dwSubjectID",		/*关联的ID*/
		
		"dwAccNo",		/*账号*/
		
		"szTrueName",		/*姓名*/
	
		"szReason",		/*原因或理由*/
	
		"dwPara1",		/*参数1（扩展）*/
		
		"dwPara2",		/*参数2（扩展）*/
		
		"szMemo",		/*说明*/
	 ""};

	/*信用记录请求*/
	static public string[] CREDITRECREQ = new string[]{
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
		"dwSID",		/*流水号*/
		
		"dwAccNo",		/*账号*/
		
		"dwCTSN",		/*信用类别编号*/
		
		"dwCreditSN",		/*信用类型编号*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*信用记录*/
	static public string[] CREDITREC = new string[]{
		
		"dwSID",		/*SID*/
		
		"dwCTSN",		/*信用类别编号*/
		
		"szCTName",		/*信用类别名称*/
	
		"dwCreditSN",		/*信用类型编号*/
		
		"szCreditName",		/*信用类型名称*/
	
		"dwScoreType",		/*积分处理方式*/
		
		"dwAccNo",		/*账号*/
		
		"szPID",		/*学工号*/
	
		"szTrueName",		/*真实姓名*/
	
		"dwTutorID",		/*导师（帐号）*/
		
		"szTutorName",		/*导师姓名*/
	
		"dwDeptID",		/*部门ID*/
		
		"szDeptName",		/*部门*/
	
		"dwDevID",		/*设备ID*/
		
		"szDevName",		/*设备名称*/
	
		"dwRoomID",		/*房间ID*/
		
		"szRoomName",		/*房间称*/
	
		"dwLabID",		/*实验室ID*/
		
		"szLabName",		/*实验室名称*/
	
		"dwAttendantID",		/*值班员账号*/
		
		"szAttendantName",		/*值班员姓名*/
	
		"szAttendantTel",		/*值班员电话*/
	
		"dwSubjectID",		/*事由ID*/
		
		"dwOccurDate",		/*发生日期*/
		
		"dwOccurTime",		/*发生时间*/
		
		"dwThisUseCScore",		/*本次使用积分*/
		
		"dwLeftCScore",		/*累计分数*/
		
      "dwUserCStat",		/*用户信用状态*/
		
		"dwForbidStartDate",		/*禁用开始时间*/
		
		"dwForbidEndDate",		/*禁用结束时间*/
		
		"szMemo",		/*说明*/
	 ""};

	/*获取系统功能请求*/
	static public string[] SYSFUNCREQ = new string[]{
		
		"dwSFSN",		/*功能编号*/
		 ""};

	/*系统功能定义*/
	static public string[] SYSFUNC = new string[]{
		
		"dwSFSN",		/*功能编号*/
		
		"szSFName",		/*功能名称*/
	
		"szURL",		/*使用详细介绍的URL*/
	
		"szMemo",		/*说明*/
	 ""};

	/*获取资格类别*/
	static public string[] SYSFUNCRULEREQ = new string[]{
		
		"dwSFRuleID",		/*功能使用规则ID*/
		
		"dwLabID",		/*实验室ID*/
		
		"dwAuthType",		/*授权类别*/
		
		"dwScopeKind",		/*适用范围类型*/
		
		"dwScopeID",		/*范围ID(根据dwScopeKind含义不同)*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*系统功能使用规则*/
	static public string[] SYSFUNCRULE = new string[]{
		
		"dwSFRuleID",		/*功能使用规则ID*/
		
		"szSFRuleName",		/*规则名称*/
	
		"dwSFSN",		/*功能编号*/
		
		"szSFName",		/*功能名称*/
	
		"szSFURL",		/*使用详细介绍的URL*/
	
		"dwLabID",		/*实验室ID*/
		
		"szLabName",		/*实验室名称*/
	
      "dwScopeKind",		/*适用范围类型*/
		
		"dwScopeID",		/*范围ID(根据dwScopeKind含义不同)*/
		
		"dwIdent",		/*身份（0表示无限制）*/
		
		"dwDeptID",		/*部门（0表示无限制）*/
		
		"dwGroupID",		/*指定用户组（0表示无限制）*/
		
		"dwPriority",		/*优先级(数字大代表优先级高)*/
		
      "dwAuthType",		/*授权类别*/
		
      "dwAuthMode",		/*授权模式*/
		
		"szIntrInfo",		/*使用说明*/
	
		"dwDefaultPeriod",		/*缺损有效期限(天)*/
		
		"szMemo",		/*说明*/
	 ""};

	/*获取用户系统功能资格表*/
	static public string[] SFROLEINFOREQ = new string[]{
		
		"dwSFSN",		/*功能编号*/
		
		"dwSFRuleID",		/*功能使用规则ID*/
		
		"dwLabID",		/*实验室ID*/
		
		"dwScopeKind",		/*适用范围类型*/
		
		"dwScopeID",		/*范围ID(根据dwScopeKind含义不同)*/
		
		"dwStatus",		/*状态*/
		
		"dwAuthType",		/*授权类别*/
		
		"dwApplyID",		/*申请ID*/
		
		"dwAccNo",		/*账号*/
		
		"dwTargetID",		/*申请对象ID(使用人账号或科研项目ID号）*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*用户系统功能资格表*/
	static public string[] SFROLEINFO = new string[]{
		
		"dwSFSN",		/*功能编号*/
		
		"szSFName",		/*功能名称*/
	
		"szSFURL",		/*使用详细介绍的URL*/
	
		"dwSFRuleID",		/*功能使用规则ID*/
		
		"szSFRuleName",		/*规则名称*/
	
		"szIntrInfo",		/*使用说明*/
	
		"dwLabID",		/*实验室ID*/
		
		"szLabName",		/*实验室名称*/
	
		"dwAuthType",		/*授权类别*/
		
      "dwStatus",		/*状态（前8种管理员审核状态）*/
		
		"dwApplyID",		/*申请ID*/
		
		"dwAccNo",		/*账号*/
		
		"szPID",		/*学工号*/
	
		"szTrueName",		/*姓名*/
	
		"dwDeptID",		/*部门ID*/
		
		"szDeptName",		/*部门*/
	
		"szTel",		/*电话*/
	
		"szHandPhone",		/*手机*/
	
		"szEmail",		/*电邮*/
	
		"dwTutorID",		/*导师账号*/
		
		"szTutorName",		/*导师姓名*/
	
		"dwTargetID",		/*申请对象ID(使用人账号或科研项目ID号）*/
		
		"szTargetName",		/*申请对象名称(使用人姓名或科研项目名称）*/
	
		"dwApplyDate",		/*申请日期*/
		
		"dwApplyTime",		/*申请时间*/
		
		"dwApplyUseTime",		/*申请使用时间（分钟）*/
		
		"dwTesteeNum",		/*使用人数*/
		
		"dwUseTimes",		/*申请使用次数*/
		
		"dwUseMinATime",		/*申请每次使用时长(分钟)*/
		
		"szApplyInfo",		/*详细介绍*/
	
		"szApplyURL",		/*附带申请报告的URL*/
	
		"dwAdminID",		/*管理员账号*/
		
		"dwCheckDate",		/*审核日期*/
		
		"dwCheckTime",		/*审核时间*/
		
		"dwPermitUseTime",		/*允许使用时间（分钟）*/
		
		"dwDeadLine",		/*允许截止时间*/
		
		"szCheckInfo",		/*审核意见*/
	
		"dwUsedTimes",		/*已使用次数*/
		
		"dwUsedTime",		/*已经使用时间（分钟）*/
		
		"szMemo",		/*说明*/
	 ""};

	/*获取编码信息的请求包*/
	static public string[] CODINGTABLEREQ = new string[]{
		
		"dwCodeType",		/*编码类别*/
		
		"szCodeSN",		/*编码*/
	
		"szCodeName",		/*编码名称*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*编码信息表*/
	static public string[] CODINGTABLE = new string[]{
		
      "dwCodeType",		/*编码类别*/
		
		"szCodeSN",		/*编码*/
	
		"szCodeName",		/*编码名称*/
	
		"szExtValue",		/*扩展*/
	
		"szMemo",		/*备注*/
	 ""};

	/*获取多语言包的请求包*/
	static public string[] MULTILANLIBREQ = new string[]{
		
		"dwLanSN",		/*语言编号*/
		
		"dwSubSysSN",		/*子系统编号*/
		
		"dwTextID",		/*文字ID*/
		 ""};

	/*多语言包*/
	static public string[] UNIMULTILANLIB = new string[]{
		
      "dwLanSN",		/*语言编号*/
		
      "dwSubSysSN",		/*子系统编号*/
		
      "dwTextID",		/*文字ID*/
		
		"szTextInfo",		/*文字内容*/
	
		"szMemo",		/*备注*/
	 ""};

	/*系统刷新请求*/
	static public string[] SYSREFRESHREQ = new string[]{
		
		"dwRefreshMod",		/*刷新模块(扩展)*/
		 ""};

}
	/*结束数据结构*/

	/*开始数据结构*/
public partial class Struct_ST
{

	/*获取资产列表*/
	static public string[] ASSERTREQ = new string[]{
		
		"dwDevID",		/*设备ID*/
		
		"szDevName",		/*实验设备名称*/
	
		"szAssertSN",		/*资产编号*/
	
		"szTagID",		/*RFID标签ID*/
	
		"szLabIDs",		/*实验室ID,多个用逗号隔开*/
	
		"szRoomIDs",		/*房间ID,多个用逗号隔开*/
	
		"szKindIDs",		/*所属类型,多个用逗号隔开*/
	
		"szClassIDs",		/*所属功能类别ID,多个用逗号隔开*/
	
		"szDeptIDs",		/*学院ID,多个用逗号隔开*/
	
		"szBuildingIDs",		/*楼宇ID,多个用逗号隔开*/
	
		"szCampusIDs",		/*校区ID,多个用逗号隔开*/
	
		"dwDevStat",		/*设备状态*/
		
		"dwProperty",		/*设备属性*/
		
		"dwClassKind",		/*类别(见UNIDEVCLS的Kind定义)*/
		
		"dwMinUnitPrice",		/*最低价格*/
		
		"dwMaxUnitPrice",		/*最大价格*/
		
		"dwSPurchaseDate",		/*开始采购日期*/
		
		"dwEPurchaseDate",		/*截止采购日期*/
		
		"dwKeeperID",		/*责任人账号*/
		
		"szKeeperName",		/*责任人姓名*/
	
		"dwProducerID",		/*生产商ID*/
		
		"szProducerName",		/*生产商名称*/
	
		"dwSellerID",		/*供应商ID*/
		
		"szSellerName",		/*供应商名称*/
	
		"dwServiceID",		/*维保单位ID*/
		
		"szServiceName",		/*维保单位名称*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*资产房间变更*/
	static public string[] ROOMCHG = new string[]{
		
		"dwDevID",		/*设备ID*/
		
		"dwOldRoomID",		/*旧房间ID*/
		
		"szOldRoomName",		/*旧房间名称*/
	
		"dwNewRoomID",		/*新房间ID*/
		
		"szNewRoomName",		/*新房间名称*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*资产责任人变更*/
	static public string[] KEEPERCHG = new string[]{
		
		"dwDevID",		/*设备ID*/
		
		"dwOldKeeperID",		/*旧责任人账号*/
		
		"szOldKeeperName",		/*旧责任人姓名*/
	
		"dwNewKeeperID",		/*新责任人账号*/
		
		"szNewKeeperName",		/*新责任人姓名*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*资产信息*/
	static public string[] UNIASSERT = new string[]{
		
		"dwDevID",		/*设备ID*/
		
		"szAssertSN",		/*用户方资产编号*/
	
		"szTagID",		/*RFID标签ID*/
	
		"szOriginSN",		/*原厂系列号*/
	
		"szDevName",		/*实验设备名称*/
	
		"dwUnitPrice",		/*设备单价*/
		
		"dwPurchaseDate",		/*采购日期 YYYYMMDD*/
		
		"dwDevStat",		/*设备状态*/
		
		"dwClassID",		/*设备功能类别*/
		
		"szClassSN",		/*设备分类号*/
	
		"szClassName",		/*设备类别名称*/
	
		"dwClassKind",		/*类别(见UNIDEVCLS的Kind定义)*/
		
		"dwKindID",		/*设备类型*/
		
		"szKindName",		/*设备名称*/
	
		"szFuncCode",		/*设备功能用途编码*/
	
		"szModel",		/*设备型号*/
	
		"szSpecification",		/*设备规格*/
	
		"dwProperty",		/*设备属性（前16种为UNIDEVKIND定义*/
		
		"dwRoomID",		/*房间ID*/
		
		"szRoomNo",		/*房间号（不重复,用于关联门禁，格式B-F-N,建筑编号-楼层号-房间编号）*/
	
		"szRoomName",		/*房间名称*/
	
		"szFloorNo",		/*所在楼层*/
	
		"dwBuildingID",		/*楼宇ID*/
		
		"szBuildingNo",		/*大楼编号(*/
	
		"szBuildingName",		/*大楼名称(*/
	
		"dwLabID",		/*实验室ID*/
		
		"szLabSN",		/*实验室编号*/
	
		"szLabName",		/*实验室名称*/
	
		"dwDeptID",		/*部门ID*/
		
		"szDeptName",		/*部门*/
	
		"dwCampusID",		/*校区ID*/
		
		"szCampusName",		/*校区名称*/
	
		"dwCampusKind",		/*校区类型（扩展）*/
		
		"dwKeeperID",		/*责任人账号*/
		
		"szKeeperName",		/*责任人姓名*/
	
		"szKeeperTel",		/*责任人电话*/
	
		"dwProducerID",		/*生产商ID*/
		
		"szProducerName",		/*生产商名称*/
	
		"dwSellerID",		/*供应商ID*/
		
		"szSellerName",		/*供应商名称*/
	
		"dwServiceID",		/*维保单位ID*/
		
		"szServiceName",		/*维保单位名称*/
	
		"szServiceTel",		/*维保电话*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*资产发卡*/
	static public string[] RFIDBIND = new string[]{
		
		"dwLabID",		/*实验室ID*/
		
		"dwDevID",		/*设备ID*/
		
		"szTagID",		/*RFID标签ID*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*获取资产盘点表*/
	static public string[] STOCKTAKINGREQ = new string[]{
		
		"dwSTID",		/*资产盘点ID*/
		
		"dwSTStat",		/*盘点状态*/
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*资产盘点表*/
	static public string[] STOCKTAKING = new string[]{
		
		"dwSTID",		/*资产盘点ID*/
		
		"dwSTDate",		/*资产盘点日期*/
		
		"dwSTEndDate",		/*资产盘点结束日期*/
		
      "dwSTStat",		/*盘点状态*/
		
		"dwLabID",		/*实验室ID*/
		
		"szLabName",		/*实验室名称*/
	
		"dwRoomID",		/*房间ID*/
		
		"szRoomName",		/*房间名称*/
	
		"dwKindID",		/*所属类型*/
		
		"szKindName",		/*设备名称*/
	
		"dwClassID",		/*所属功能类别ID*/
		
		"szClassName",		/*设备类别名称*/
	
		"dwClassKind",		/*类别(见UNIDEVCLS的Kind定义)*/
		
		"dwAttendantID",		/*责任人ID*/
		
		"szAttendantName",		/*责任人姓名*/
	
		"dwMinUnitPrice",		/*最低价格*/
		
		"dwMaxUnitPrice",		/*最大价格*/
		
		"dwLeaderID",		/*盘点负责人ID*/
		
		"szLeaderName",		/*盘点负责人姓名*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*获取盘点资产明细表*/
	static public string[] STDETAILREQ = new string[]{
		
		"dwSTID",		/*资产盘点ID*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*资产盘点详细信息*/
	static public string[] STDETAIL = new string[]{
		
		"dwSTID",		/*资产盘点ID*/
		
		"dwDevID",		/*设备ID*/
		
      "dwSTStat",		/*盘点状态*/
		
		"dwSTDate",		/*资产盘点日期*/
		
		"dwLeaderID",		/*盘点负责人ID*/
		
		"szLeaderName",		/*盘点负责人姓名*/
	
		"dwRoomID",		/*房间ID*/
		
		"dwAttendantID",		/*责任人账号*/
		
		"szAttendantName",		/*责任人姓名*/
	
		"szSTInfo",		/*盘点情况描述*/
	
		"szAssertSN",		/*用户方资产编号*/
	
		"szTagID",		/*RFID标签ID*/
	
		"szOriginSN",		/*原厂系列号*/
	
		"szDevName",		/*实验设备名称*/
	
		"dwUnitPrice",		/*设备单价*/
		
		"dwPurchaseDate",		/*采购日期 YYYYMMDD*/
		
		"dwClassID",		/*设备功能类别*/
		
		"szClassSN",		/*设备分类号*/
	
		"szClassName",		/*设备类别名称*/
	
		"dwClassKind",		/*类别(见UNIDEVCLS的Kind定义)*/
		
		"dwKindID",		/*设备类型*/
		
		"szKindName",		/*设备名称*/
	
		"szFuncCode",		/*设备功能用途编码*/
	
		"szModel",		/*设备型号*/
	
		"szSpecification",		/*设备规格*/
	
		"dwProperty",		/*设备属性（前16种为UNIDEVKIND定义*/
		
		"szRoomNo",		/*房间号（不重复,用于关联门禁，格式B-F-N,建筑编号-楼层号-房间编号）*/
	
		"szRoomName",		/*房间名称*/
	
		"szFloorNo",		/*所在楼层*/
	
		"dwBuildingID",		/*楼宇ID*/
		
		"szBuildingNo",		/*大楼编号(*/
	
		"szBuildingName",		/*大楼名称(*/
	
		"dwLabID",		/*实验室ID*/
		
		"szLabSN",		/*实验室编号*/
	
		"szLabName",		/*实验室名称*/
	
		"dwDeptID",		/*部门ID*/
		
		"szDeptName",		/*部门*/
	
		"dwCampusID",		/*校区ID*/
		
		"szCampusName",		/*校区名称*/
	
		"dwCampusKind",		/*校区类型（扩展）*/
		
		"szMemo",		/*说明信息*/
	 ""};

	/*获取设备报废记录表*/
	static public string[] OUTOFSERVICEREQ = new string[]{
		
		"dwOOSID",		/*设备报废记录ID*/
		
		"dwOOSStat",		/*报废状态*/
		
		"dwOOSType",		/*报废类型*/
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*报废设备*/
	static public string[] OOSDEV = new string[]{
		
		"dwOOSID",		/*设备报废记录ID*/
		
		"dwDevID",		/*设备ID*/
		
		"szAssertSN",		/*用户方资产编号*/
	
		"szDevName",		/*实验设备名称*/
	
		"dwUnitPrice",		/*设备单价*/
		
		"dwPurchaseDate",		/*采购日期 YYYYMMDD*/
		
		"szKindName",		/*设备名称*/
	
		"szRoomName",		/*房间名称*/
	
		"dwLabID",		/*实验室ID*/
		
		"szLabName",		/*实验室名称*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*设备报废记录表*/
	static public string[] OUTOFSERVICE = new string[]{
		
		"dwOOSID",		/*设备报废记录ID*/
		
      "dwOOSStat",		/*报废状态*/
		
		"dwOOSType",		/*报废类型*/
		
		"szOOSInfo",		/*报废信息*/
	
		"dwApplyDate",		/*申请日期*/
		
		"dwApplyID",		/*申请人ID*/
		
		"szApplyName",		/*申请人姓名*/
	
		"dwApproveDate",		/*申请日期*/
		
		"dwApproveID",		/*申请人ID*/
		
		"szApproveName",		/*申请人姓名*/
	
		"szMemo",		/*说明信息*/
	
	"OOSDev",		/*CUniTable[OOSDEV]*/
	 ""};

	/*获取报废设备明细表*/
	static public string[] OOSDETAILREQ = new string[]{
		
		"dwOOSID",		/*设备报废记录ID*/
		
		"dwOOSStat",		/*报废状态*/
		
		"dwOOSType",		/*报废类型*/
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*报废设备明细*/
	static public string[] OOSDETAIL = new string[]{
		
		"dwOOSID",		/*设备报废记录ID*/
		
		"dwOOSStat",		/*报废状态*/
		
		"dwOOSType",		/*报废类型*/
		
		"szOOSInfo",		/*报废信息*/
	
		"dwApplyDate",		/*申请日期*/
		
		"dwApplyID",		/*申请人ID*/
		
		"szApplyName",		/*申请人姓名*/
	
		"dwApproveDate",		/*申请日期*/
		
		"dwApproveID",		/*申请人ID*/
		
		"szApproveName",		/*申请人姓名*/
	
		"dwDevID",		/*设备ID*/
		
		"szAssertSN",		/*用户方资产编号*/
	
		"szTagID",		/*RFID标签ID*/
	
		"szOriginSN",		/*原厂系列号*/
	
		"szDevName",		/*实验设备名称*/
	
		"dwUnitPrice",		/*设备单价*/
		
		"dwPurchaseDate",		/*采购日期 YYYYMMDD*/
		
		"dwClassID",		/*设备功能类别*/
		
		"szClassSN",		/*设备分类号*/
	
		"szClassName",		/*设备类别名称*/
	
		"dwClassKind",		/*类别(见UNIDEVCLS的Kind定义)*/
		
		"dwKindID",		/*设备类型*/
		
		"szKindName",		/*设备名称*/
	
		"szFuncCode",		/*设备功能用途编码*/
	
		"szModel",		/*设备型号*/
	
		"szSpecification",		/*设备规格*/
	
		"dwProperty",		/*设备属性（前16种为UNIDEVKIND定义*/
		
		"dwRoomID",		/*房间ID*/
		
		"szRoomNo",		/*房间号（不重复,用于关联门禁，格式B-F-N,建筑编号-楼层号-房间编号）*/
	
		"szRoomName",		/*房间名称*/
	
		"szFloorNo",		/*所在楼层*/
	
		"dwBuildingID",		/*楼宇ID*/
		
		"szBuildingNo",		/*大楼编号(*/
	
		"szBuildingName",		/*大楼名称(*/
	
		"dwLabID",		/*实验室ID*/
		
		"szLabSN",		/*实验室编号*/
	
		"szLabName",		/*实验室名称*/
	
		"dwDeptID",		/*部门ID*/
		
		"szDeptName",		/*部门*/
	
		"dwCampusID",		/*校区ID*/
		
		"szCampusName",		/*校区名称*/
	
		"dwCampusKind",		/*校区类型（扩展）*/
		
		"szMemo",		/*说明信息*/
	 ""};

	/*设备保修申请*/
	static public string[] REPAIRAPPLY = new string[]{
		
		"dwSID",		/*保修ID*/
		
		"dwDevID",		/*设备ID*/
		
		"szDevName",		/*设备名称*/
	
		"szAssertSN",		/*用户方资产编号*/
	
		"dwDamageDate",		/*损坏日期*/
		
		"dwDamageTime",		/*损坏时间*/
		
		"szDamageInfo",		/*损坏说明*/
	
		"dwManID",		/*经办人ID*/
		
		"szManName",		/*经办人姓名*/
	
		"szMemo",		/*说明*/
	 ""};

	/**/
	static public string[] REPAIROVER = new string[]{
		
		"dwSID",		/*SID*/
		
		"dwDevID",		/*设备ID*/
		
		"szDevName",		/*设备名称*/
	
		"szAssertSN",		/*用户方资产编号*/
	
		"dwStatus",		/*DEVDAMAGEREC定义*/
		
		"dwRepareDate",		/*维修日期*/
		
		"dwRepareTime",		/*维修时间*/
		
		"szRepareInfo",		/*维修说明*/
	
		"dwRepareCost",		/*维修费用*/
		
		"szFundsNo1",		/*经费卡编号1*/
	
		"dwPay1",		/*经费卡1支付*/
		
		"szFundsNo2",		/*经费卡编号2*/
	
		"dwPay2",		/*经费卡2支付*/
		
		"szRepareCom",		/*维修单位*/
	
		"szRepareComTel",		/*维修单位联系方式*/
	
		"szMemo",		/*说明*/
	 ""};

	/**/
	static public string[] REPAIRCANCEL = new string[]{
		
		"dwSID",		/*SID*/
		
		"dwDevID",		/*设备ID*/
		
		"szDevName",		/*设备名称*/
	
		"szAssertSN",		/*用户方资产编号*/
	
		"dwDevStat",		/*撤销后设备状态*/
		
		"szCancelInfo",		/*撤销说明*/
	
		"szMemo",		/*说明*/
	 ""};

	/**/
	static public string[] COMPANYREQ = new string[]{
		
		"dwComID",		/*单位ID*/
		
		"dwComKind",		/*单位类型*/
		
		"dwProperty",		/*属性*/
		
		"szComName",		/*单位名*/
	
		"szSearchKey",		/*搜索关键字*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/**/
	static public string[] UNICOMPANY = new string[]{
		
		"dwComID",		/*单位ID*/
		
		"szComName",		/*单位名*/
	
      "dwComKind",		/*单位类型*/
		
		"dwProperty",		/*属性*/
		
		"szTrueName",		/*联系人姓名*/
	
		"szJobTitle",		/*职务*/
	
		"szTel",		/*电话*/
	
		"szHandPhone",		/*手机*/
	
		"szEmail",		/*电邮*/
	
		"szQQ",		/*QQ*/
	
		"szAddress",		/*地址*/
	
		"szOtherContact",		/*其它联系方式*/
	
		"szKeyWords",		/*关键字*/
	
		"szMemo",		/*备注*/
	 ""};

	/*获取设备历史档案明细表*/
	static public string[] ASSERTLOGREQ = new string[]{
		
		"dwSID",		/*设备报废记录ID*/
		
		"dwDevID",		/*设备ID*/
		
		"dwOpKind",		/*日志类型*/
		
		"dwOperatorID",		/*操作员ID*/
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*设备历史档案*/
	static public string[] ASSERTLOG = new string[]{
		
		"dwSID",		/*日志ID*/
		
      "dwOpKind",		/*日志类型*/
		
		"dwOpDate",		/*日期*/
		
		"dwOpTime",		/*时间*/
		
		"szOpDetail",		/*详细信息*/
	
		"dwOperatorID",		/*操作员ID*/
		
		"szOperatorName",		/*操作员姓名*/
	
		"dwDevID",		/*设备ID*/
		
		"szAssertSN",		/*用户方资产编号*/
	
		"szDevName",		/*实验设备名称*/
	
		"dwClassID",		/*设备功能类别*/
		
		"szClassName",		/*设备类别名称*/
	
		"dwClassKind",		/*类别(见UNIDEVCLS的Kind定义)*/
		
		"dwKindID",		/*设备类型*/
		
		"szKindName",		/*设备名称*/
	
		"szFuncCode",		/*设备功能用途编码*/
	
		"szModel",		/*设备型号*/
	
		"szSpecification",		/*设备规格*/
	 ""};

}
	/*结束数据结构*/

	/*开始数据结构*/
public partial class Struct_ST
{

	/*考勤房间*/
	static public string[] ATTENDROOM = new string[]{
		
		"dwRoomID",		/*房间ID*/
		
		"szRoomName",		/*房间名称*/
	
		"szRoomNo",		/*房间号*/
	
		"szFloorNo",		/*所在楼层*/
	
		"dwBuildingID",		/*楼宇ID*/
		
		"szBuildingNo",		/*大楼编号(*/
	
		"szBuildingName",		/*大楼名称(*/
	
		"szMemo",		/*备注*/
	 ""};

	/*获取考勤规则的请求包*/
	static public string[] ATTENDRULEREQ = new string[]{
		
		"dwAttendID",		/*考勤规则ID*/
		
		"szAttendName",		/*考勤规则名称*/
	
		"dwKind",		/*考勤类型*/
		
		"szRoomIDs",		/*房间ID,多个用逗号隔开*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*考勤规则*/
	static public string[] ATTENDRULE = new string[]{
		
		"dwAttendID",		/*考勤规则ID*/
		
		"szAttendName",		/*考勤规则名称*/
	
		"dwKind",		/*考勤类型*/
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
		"dwGroupID",		/*组ID*/
		
		"dwOpenRuleSN",		/*开放规则编号*/
		
		"dwEarlyInTime",		/*最早进入时间(HHMM)*/
		
		"dwLateInTime",		/*最晚进入时间(HHMM)*/
		
		"dwEarlyOutTime",		/*最早离开时间(HHMM),小于进入时间表明跨天*/
		
		"dwLateOutTime",		/*最晚离开时间(HHMM),小于进入时间表明跨天*/
		
		"dwMinStayTime",		/*最少停留时间*/
		
	"AttendRoom",		/*ATTENDROOM表*/
	
		"szMemo",		/*备注*/
	 ""};

	/*获取考勤记录的请求包*/
	static public string[] ATTENDRECREQ = new string[]{
		
		"dwAttendID",		/*考勤规则ID*/
		
		"szAttendName",		/*考勤规则名称*/
	
		"dwKind",		/*考勤类型*/
		
		"szRoomIDs",		/*房间ID,多个用逗号隔开*/
	
		"dwAccNo",		/*账号*/
		
		"dwAttendStat",		/*考勤状态*/
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*考勤记录*/
	static public string[] ATTENDREC = new string[]{
		
		"dwSID",		/*流水号*/
		
		"dwAttendID",		/*考勤规则ID*/
		
		"szAttendName",		/*考勤规则名称*/
	
		"dwAccNo",		/*账号*/
		
		"szPID",		/*学工号*/
	
		"szTrueName",		/*姓名*/
	
		"dwSex",		/*性别*/
		
		"dwAttendDate",		/*考勤日期*/
		
		"dwAttendStat",		/*考勤状态(定义在UNIRESVREC)*/
		
		"dwRoomID",		/*房间ID*/
		
		"szRoomName",		/*房间名称*/
	
		"dwInTime",		/*进入时间(HHMM)*/
		
		"dwOutTime",		/*离开时间(HHMM)*/
		
		"dwLatestInTime",		/*最近进入时间*/
		
		"dwStayMin",		/*停留时间(分钟)*/
		
		"dwCardTimes",		/*刷卡次数*/
		
		"dwRFLID",		/*request for leave ID*/
		
		"szMemo",		/*备注*/
	 ""};

	/*考勤信息*/
	static public string[] ATTENDINFO = new string[]{
		
      "dwAttendMode",		/*考勤进出模式*/
		
		"dwAccNo",		/*账号*/
		
		"dwRoomID",		/*房间ID*/
		
		"dwAttendDate",		/*考勤日期*/
		
		"dwSID",		/*流水号*/
		
		"dwAttendID",		/*考勤规则ID*/
		
		"szAttendName",		/*考勤规则名称*/
	
		"dwAttendStat",		/*考勤状态(定义在UNIRESVREC)*/
		
		"dwInTime",		/*进入时间(HHMM)*/
		
		"dwOutTime",		/*离开时间(HHMM)*/
		
		"dwLatestInTime",		/*最近进入时间*/
		
		"dwStayMin",		/*停留时间(分钟)*/
		
		"dwCardTimes",		/*刷卡次数*/
		
		"dwRFLID",		/*request for leave ID*/
		
		"szMemo",		/*备注*/
	 ""};

	/*获取考勤统计的请求包*/
	static public string[] ATTENDSTATREQ = new string[]{
		
		"dwAttendID",		/*考勤规则ID*/
		
		"dwAccNo",		/*账号*/
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*考勤统计*/
	static public string[] ATTENDSTAT = new string[]{
		
		"dwAccNo",		/*账号*/
		
		"szPID",		/*学工号*/
	
		"szTrueName",		/*姓名*/
	
		"dwSex",		/*性别*/
		
		"dwTotalTimes",		/*总次数*/
		
		"dwAttendTimes",		/*出勤次数*/
		
		"dwAbsentTimes",		/*缺勤次数*/
		
		"dwLateTimes",		/*迟到次数*/
		
		"dwLeaveTimes",		/*早退次数*/
		
		"dwLLTimes",		/*迟到且早退次数*/
		
		"dwSickTimes",		/*病假次数*/
		
		"dwPrivateTimes",		/*事假次数*/
		
		"dwUseLessTimes",		/*使用时间不达标次数*/
		
		"dwLeaveNoCardTimes",		/*未刷卡离开次数*/
		
		"dwTotalMin",		/*出勤总时间（分钟）*/
		 ""};

}
	/*结束数据结构*/

	/*开始数据结构*/
public partial class Struct_ST
{

	/*子系统信息*/
	static public string[] SUBSYS = new string[]{
		
		"dwStaSN",		/*子系统编号*/
		
		"szSubSysName",		/*子系统名称*/
	
      "dwStatus",		/*状态*/
		
		"szVersion",		/*子系统服务器版本*/
	
		"szIP",		/*IP地址*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*子系统登录请求*/
	static public string[] SUBSYSLOGINREQ = new string[]{
		
		"dwStaSN",		/*子系统编号*/
		
		"szVersion",		/*子系统服务器版本*/
	
		"szKey",		/*密钥(扩展)*/
	
		"szIP",		/*IP地址*/
	
		"szMAC",		/*网卡地址*/
	
		"dwOldSessionID",		/*上次分配的session值*/
		
		"szMemo",		/*说明信息*/
	 ""};

	/*子系统登录应答*/
	static public string[] SUBSYSLOGINRES = new string[]{
		
		"dwSessionID",		/*服务器分配的SessionID*/
		
		"ExtInfo",		/*返回扩展信息*/
	
		"szMemo",		/*说明信息*/
	 ""};

	/*子系统退出请求*/
	static public string[] SUBSYSLOGOUTREQ = new string[]{
		
		"dwSessionID",		/*服务器分配的SessionID*/
		
		"dwStaSN",		/*子系统编号*/
		 ""};

	/*IC空间使用记录上传*/
	static public string[] ICUSERECUP = new string[]{
		
		"dwSID",		/*流水号*/
		
		"dwStaSN",		/*子系统编号*/
		
		"dwSubStaSN",		/*子站点编号*/
		
		"szLogonName",		/*学工号*/
	
		"szTrueName",		/*姓名*/
	
		"dwUseDate",		/*使用日期*/
		
		"dwResvBTime",		/*预约开始时间*/
		
		"dwResvETime",		/*预约结束时间*/
		
		"dwRealInTime",		/*实际开始时间*/
		
		"dwRealOutTime",		/*实际结束时间*/
		
		"dwUseMinutes",		/*使用时长(分钟)*/
		
		"szUseDev",		/*使用设备*/
	
		"dwDevClsKind",		/*类别类型(关联UNIDEVCLS:dwKind定义）*/
		
		"dwDevKind",		/*设备类型*/
		
		"dwUseMode",		/*使用方法（参考UNIRESERVE定义）*/
		
		"dwPurpose",		/*用途（参考UNIRESERVE定义）*/
		
		"dwRealCost",		/*实际缴纳费用(分)*/
		
		"szMemo",		/*说明信息*/
	 ""};

	/*打印复印扫描记录上传*/
	static public string[] PRINTRECUP = new string[]{
		
		"dwSID",		/*流水号*/
		
		"dwStaSN",		/*子系统编号*/
		
		"dwSubStaSN",		/*子站点编号*/
		
		"szLogonName",		/*学工号*/
	
		"szTrueName",		/*姓名*/
	
		"dwUseDate",		/*使用日期*/
		
		"dwUseTime",		/*使用时间*/
		
		"szUseDev",		/*使用设备*/
	
		"dwPages",		/*文印面数(或扫描大小）*/
		
		"dwPaperType",		/*纸型*/
		
		"dwPrintType",		/*文印类型*/
		
		"dwProperty",		/*属性*/
		
		"dwRealCost",		/*实际缴纳费用(分)*/
		
		"dwUnitFee",		/*单价*/
		
		"dwPaperNum",		/*纸张数*/
		
		"dwMaterialFee",		/*材料费*/
		
		"dwManualFee",		/*人工费*/
		
		"szMemo",		/*说明信息*/
	 ""};

	/*图书超期缴费记录上传*/
	static public string[] BOOKOVERDUERECUP = new string[]{
		
		"dwSID",		/*流水号*/
		
		"dwStaSN",		/*子系统编号*/
		
		"dwSubStaSN",		/*子站点编号*/
		
		"szLogonName",		/*学工号*/
	
		"szTrueName",		/*姓名*/
	
		"dwUseDate",		/*使用日期*/
		
		"dwUseTime",		/*使用时间*/
		
		"szUseDev",		/*使用设备*/
	
		"szBookName",		/*书名*/
	
		"dwRealCost",		/*实际缴纳费用(分)*/
		
		"szMemo",		/*说明信息*/
	 ""};

	/*违约记录上传*/
	static public string[] BREACHRECUP = new string[]{
		
		"dwSID",		/*流水号*/
		
		"dwStaSN",		/*子系统编号*/
		
		"dwSubStaSN",		/*子站点编号*/
		
		"szLogonName",		/*学工号*/
	
		"szTrueName",		/*姓名*/
	
		"dwOccurDate",		/*违约日期*/
		
		"dwOccurTime",		/*违约时间*/
		
		"szBreachName",		/*违约类型名*/
	
		"dwPunishScore",		/*本次罚分*/
		
		"dwTotalScore",		/*累计罚分*/
		
		"dwThresholdScore",		/*达到处罚标准的分数*/
		
      "dwStatus",		/*处罚状态*/
		
		"szPunishName",		/*处罚方式*/
	
		"dwPStartDate",		/*处罚开始时间*/
		
		"dwPEndDate",		/*处罚结束时间*/
		
		"szMemo",		/*说明信息*/
	 ""};

}
	/*结束数据结构*/

	/*开始数据结构*/
public partial class Struct_ST
{

	/*获取研修间当前状态请求*/
	static public string[] STUDYROOMSTATREQ = new string[]{
		
		"dwRoomKinds",		/*研修间类型组合*/
		
		"szBuildingNo",		/*大楼编号*/
	 ""};

	/*研修间当前状态*/
	static public string[] STUDYROOMSTAT = new string[]{
		
      "dwRoomKind",		/*研修间类型*/
		
		"dwTotalNum",		/*总数*/
		
		"dwIdleNum",		/*空闲数*/
		 ""};

	/*获取座位当前状态请求*/
	static public string[] SEATSTATREQ = new string[]{
		
		"szBuildingNo",		/*大楼编号*/
	
		"szFloorNo",		/*所在楼层*/
	 ""};

	/*座位当前状态*/
	static public string[] SEATSTAT = new string[]{
		
		"szRoomNo",		/*房间号*/
	
		"szRoomName",		/*房间名称*/
	
		"dwTotalNum",		/*总数*/
		
		"dwIdleNum",		/*空闲数*/
		 ""};

}
	/*结束数据结构*/

	/*开始数据结构*/
public partial class Struct_ST
{

	/*获取科研实验数据请求*/
	static public string[] RTDATAREQ = new string[]{
		
		"dwBeginDate",		/*预约开始日期*/
		
		"dwEndDate",		/*预约结束日期*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*获取科研实验数据*/
	static public string[] RTDATA = new string[]{
		
		"dwResvID",		/*预约号*/
		
		"dwProperty",		/*预约属性*/
		
		"dwDevID",		/*设备ID*/
		
		"szAssertSN",		/*资产编号*/
	
		"szTestName",		/*科研实验名称*/
	
		"dwBeginTime",		/*预约开始时间*/
		
		"dwEndTime",		/*预约结束时间*/
		
		"dwOwner",		/*使用人(创建者)*/
		
		"szPID",		/*使用人学工号*/
	
		"szOwnerName",		/*使用人姓名*/
	
		"dwIdent",		/*使用人身份（校内（本、研，教师和校外）*/
		
		"dwUserDeptID",		/*使用人部门ID*/
		
		"szUserDeptName",		/*使用人部门*/
	
		"szTel",		/*电话*/
	
		"szHandPhone",		/*手机*/
	
		"szEmail",		/*电邮*/
	
		"dwRTID",		/*科研实验项目ID*/
		
		"dwRTKind",		/*科研类型*/
		
		"szRTName",		/*科研实验名称*/
	
		"dwSampleNum",		/*样品数*/
		
		"dwManID",		/*管理员ID*/
		
		"szManName",		/*管理员姓名*/
	
		"dwReceivableCost",		/*应缴费用*/
		
		"dwRealCost",		/*实际缴纳费用*/
		
		"szDescription",		/*实验描述*/
	 ""};

}
	/*结束数据结构*/

	/*开始数据结构*/
public partial class Struct_ST
{

	/**/
	static public string[] STALOGINREQ = new string[]{
		
		"szLicSN",		/*许可系列号*/
	
		"szVersion",		/*版本*/
	
		"szIP",		/*IP地址*/
	
		"szMAC",		/*网卡地址*/
	
		"szKey",		/*密钥(扩展)*/
	
		"dwOldSessionID",		/*上次分配的session值*/
		 ""};

	/**/
	static public string[] STALOGINRES = new string[]{
		
		"dwStaID",		/*节点ID*/
		
		"dwSessionID",		/**/
		
	"LicInfo",		/*服务器授权信息UNILICENSE*/
	
		"szMemo",		/*备注*/
	 ""};

	/**/
	static public string[] STALOGOUTREQ = new string[]{
		
		"dwSessionID",		/**/
		
		"szLicSN",		/*许可系列号*/
	 ""};

	/**/
	static public string[] HANDSHAKEREQ = new string[]{
		
		"dwChgFlag",		/*本地保存的修改更新标志*/
		 ""};

	/**/
	static public string[] HANDSHAKERES = new string[]{
		
		"dwChgFlag",		/*服务器返回的的修改更新标志*/
		
		"szResChgStat",		/*返回的对应信息有无标志，字符0表示无，字符1表示有*/
	 ""};

	/*模块监控信息上传*/
	static public string[] MODMONIUP = new string[]{
		
		"dwModSN",		/*监控端编号（服务器为0，以下定义+StaSN*65536 + 监控端编号)*/
		
		"szModName",		/*监控端名称*/
	
		"dwStatus",		/*总状态*/
		
		"dwStartTime",		/*新状态开始时间*/
		
		"szStatInfo",		/*状态说明*/
	
		"szMemo",		/*备注*/
	 ""};

	/*监控指标上传*/
	static public string[] MONINDEXUP = new string[]{
		
		"dwModSN",		/*监控端编号（服务器为0，其余为定义的编号或ID号)*/
		
		"dwMoniSN",		/*监控指标编号*/
		
		"szIndexName",		/*监控指标名称*/
	
		"dwNormalValue",		/*正常值*/
		
		"dwCurValue",		/*当前值*/
		
		"dwStatus",		/*状态*/
		
		"dwNormalTime",		/*正常时间(分钟)*/
		
		"dwAbnormalTime",		/*异常时间(分钟)*/
		
		"dwAbnormalTimes",		/*异常次数*/
		
		"dwStartTime",		/*新状态开始时间*/
		
		"szStatInfo",		/*状态说明*/
	
		"szMemo",		/*备注*/
	 ""};

	/*监控记录上传*/
	static public string[] MONIRECUP = new string[]{
		
		"dwSID",		/*分配流水号*/
		
		"dwModSN",		/*监控端编号（服务器为0，其余为定义的编号或ID号)*/
		
		"dwMoniSN",		/*监控指标编号*/
		
		"szIndexName",		/*监控指标名称*/
	
		"dwCurValue",		/*当前值*/
		
		"dwOccurDate",		/*开始日期*/
		
		"dwOccurTime",		/*产生时间*/
		
		"dwStatus",		/*状态*/
		
		"dwNormalTime",		/*正常时间(分钟)*/
		
		"dwAbnormalTime",		/*异常时间(分钟)*/
		
		"dwAbnormalTimes",		/*异常次数*/
		
		"szStatInfo",		/*状态说明*/
	 ""};

}
	/*结束数据结构*/

	/*开始数据结构*/
public partial class Struct_ST
{

	/*获取监控信息缺省值*/
	static public string[] MONIREQ = new string[]{
		
		"dwModKind",		/*子模块类别（MODKIND_XXX定义)*/
		
		"dwStaSN",		/*站点编号*/
		
		"dwModSN",		/*监控端编号*/
		
		"dwStatus",		/*状态*/
		
      "dwReqProp",		/*请求附加属性*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*监控指标编号*/
	static public string[] MONINDEX = new string[]{
		
		"dwModSN",		/*监控端编号（服务器为0，其余为定义的编号或ID号)*/
		
		"szModName",		/*监控端名称*/
	
      "dwMoniSN",		/*监控指标编号*/
		
		"szIndexName",		/*监控指标名称*/
	
		"dwNormalValue",		/*正常值*/
		
		"dwCurValue",		/*当前值*/
		
      "dwStatus",		/*状态*/
		
		"dwNormalTime",		/*正常时间(分钟)*/
		
		"dwAbnormalTime",		/*异常时间(分钟)*/
		
		"dwAbnormalTimes",		/*异常次数*/
		
		"dwStartTime",		/*新状态开始时间*/
		
		"szStatInfo",		/*状态说明*/
	
		"szMemo",		/*备注*/
	 ""};

	/*模块监控信息*/
	static public string[] MODMONI = new string[]{
		
      "dwModSN",		/*监控端编号（服务器为0，以下定义+StaSN*65536 + 监控端编号)*/
		
		"szModName",		/*监控端名称*/
	
		"dwStatus",		/*总状态*/
		
		"dwStartTime",		/*新状态开始时间*/
		
		"szStatInfo",		/*状态说明*/
	
	"MoniIndexTbl",		/*指标列表*/
	
		"szMemo",		/*备注*/
	 ""};

	/*获取监控信息缺省值*/
	static public string[] MONIRECREQ = new string[]{
		
		"dwStartDate",		/*开始日期*/
		
		"dwEndDate",		/*结束日期*/
		
		"dwModKind",		/*子模块类别（MODKIND_XXX定义)*/
		
		"dwStaSN",		/*站点编号*/
		
		"dwModSN",		/*模块编号*/
		
		"dwMoniSN",		/*监控指标编号*/
		
		"dwStatus",		/*状态*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*监控记录*/
	static public string[] MONIREC = new string[]{
		
		"dwModSN",		/*监控端编号（服务器为0，其余为定义的编号或ID号)*/
		
		"szModName",		/*监控端名称*/
	
		"dwMoniSN",		/*监控指标编号*/
		
		"szIndexName",		/*监控指标名称*/
	
		"dwNormalValue",		/*正常值*/
		
		"dwCurValue",		/*当前值*/
		
		"dwOccurDate",		/*开始日期*/
		
		"dwOccurTime",		/*产生时间*/
		
		"dwStatus",		/*状态*/
		
		"dwNormalTime",		/*正常时间(分钟)*/
		
		"dwAbnormalTime",		/*异常时间(分钟)*/
		
		"dwAbnormalTimes",		/*异常次数*/
		
		"szStatInfo",		/*状态说明*/
	 ""};

	/*错误处理*/
	static public string[] MONIDEALERR = new string[]{
		
		"dwModSN",		/*监控端编号（服务器为0，其余为定义的编号或ID号)*/
		
		"dwMoniSN",		/*监控指标编号*/
		
		"dwNormalValue",		/*正常值*/
		
		"dwCurValue",		/*当前值*/
		
		"szDealInfo",		/*说明信息*/
	 ""};

}
	/*结束数据结构*/

	/*开始数据结构*/
public partial class Struct_ST
{

	/*联创服务与节点通信参数*/
	static public string[] UNISTAPARAM = new string[]{
		
		"dwReqUID",		/*请求UID*/
		
		"szReqData",		/*请求内容*/
	
		"dwResCode",		/*返回码，0表示成功*/
		
		"szResData",		/*返回数据*/
	 ""};

	/*许可功能模块信息*/
	static public string[] LICMOD = new string[]{
		
      "dwFuncSN",		/*功能模块编号*/
		
		"dwLicNum",		/*对应功能模块节点数*/
		
		"szModName",		/*授权模块名称*/
	 ""};

	/*许可信息*/
	static public string[] UNILICENSE = new string[]{
		
		"szLicSN",		/*许可编号*/
	
		"dwInstDate",		/*安装日期*/
		
		"dwLicExpDate",		/*许可到期日*/
		
		"dwServiceExpDate",		/*服务到期日*/
		
		"szLicTo",		/*授权客户名称*/
	
		"szLicProName",		/*授权产品名称*/
	
		"szCompanyName",		/*公司名称*/
	
      "dwWarrant",		/*与一卡通对接模式*/
		
		"dwLicStaNum",		/*许可站点数*/
		
	"LicMod",		/*LICMOD结构表*/
	
		"szCtrlCode",		/*控制码*/
	 ""};

	/*获取请求扩张信息*/
	static public string[] REQEXTINFO = new string[]{
		
		"dwStartLine",		/*开始行*/
		
		"dwNeedLines",		/*需获取行数*/
		
		"dwTotolLines",		/*服务端返回总行数*/
		
		"szOrderKey",		/*排序字段*/
	
		"szOrderMode",		/*排序方式(ASC或DESC)*/
	
	"ExtInfo",		/*根据不同的请求相关扩展信息*/
	 ""};

}
	/*结束数据结构*/

}
//