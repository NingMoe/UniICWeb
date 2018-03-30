
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
using System.Runtime.InteropServices;


namespace UniWebLib
{

	/*��ʼ���ݽṹ*/
public partial class Struct_ST
{

	/*�汾��Ϣ*/
	static public string[] UNIVERSION = new string[]{
		
		"szVersion",		/*�汾	XX.XX.XXXXXXXX*/
	
		"dwWarrant",		/*��һ��ͨ�Խ�ģʽ*/
		
	"szLicInfo",		/*��Ȩ��Ϣ(UNILICENSE�ṹ)*/
	 ""};

}
	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/
public partial class Struct_ST
{

	/*����Ա��¼����*/
	static public string[] ADMINLOGINREQ = new string[]{
		
		"dwStaSN",		/*վ����*/
		
      "dwLoginRole",		/*��¼ģʽ*/
		
		"szVersion",		/*�汾	XX.XX.XXXXXXXX,���°汾��������*/
	
		"szLogonName",		/*��¼��*/
	
		"szPassword",		/*����*/
	
		"szIP",		/*IP��ַ*/
	
		"szMSN",		/*΢�ź�*/
	 ""};

	/*����Ա��¼Ӧ��*/
	static public string[] ADMINLOGINRES = new string[]{
		
		"dwSessionID",		/*�����������SessionID*/
		
	"SrvVer",		/*UNIVERSION �ṹ*/
	
      "dwSupportSubSys",		/*��Ȩ��ϵͳ*/
		
      "dwManRole",		/*����Ա��ɫ*/
		
      "dwUserStat",		/*�û�״̬*/
		
	"AdminInfo",		/*UNIADMIN �ṹ*/
	
	"UserRole",		/*USERROLE��*/
	
	"StaInfo",		/*CUniTable[UNISTATION]*/
	
	"AccInfo",		/*�û���Ϣ(UNIACCOUNT�ṹ)*/
	
	"TutorInfo",		/*�û���Ϣ(UNITUTOR�ṹ)*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*�ֻ���¼����*/
	static public string[] MOBILELOGINREQ = new string[]{
		
		"dwStaSN",		/*վ����*/
		
      "dwLoginRole",		/*��¼ģʽ*/
		
		"szVersion",		/*�汾	XX.XX.XXXXXXXX*/
	
		"szLogonName",		/*��¼��*/
	
		"szPassword",		/*����*/
	
		"szIP",		/*IP��ַ*/
	
		"szMSN",		/*΢�ź�*/
	
      "dwProperty",		/*��չ����*/
		 ""};

	/*����̨�˳�����*/
	static public string[] ADMINLOGOUTREQ = new string[]{
		
		"dwAccNo",		/*�˺�*/
		
		"szLogonName",		/*��¼��*/
	
		"szIP",		/*IP��ַ*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*����̨�˳���Ӧ*/
	static public string[] ADMINLOGOUTRES = new string[]{
		
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*��ȡϵͳ֧�ֵ�UID*/
	static public string[] UIDINFOREQ = new string[]{
		
		"dwUidSN",		/*UID���*/
		
		"dwFuncSN",		/*��������ģ����*/
		
		"dwUIDType",		/*��������*/
		
		"szUIDName",		/*UID����*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*ϵͳ֧�ֵ�UID*/
	static public string[] UIDINFO = new string[]{
		
		"dwUidSN",		/*UID���*/
		
		"dwFuncSN",		/*��������ģ����*/
		
		"szFuncName",		/*��������ģ������*/
	
      "dwUIDType",		/*��������*/
		
		"szUIDName",		/*UID����*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*�ֶ�����*/
	static public string[] FIELDLIMIT = new string[]{
		
		"szFieldName",		/*�ֶ�����*/
	
		"szMask",		/*�Ը��ֶδ�����*/
	 ""};

	/*UIDȨ����ϸ*/
	static public string[] PRIVUID = new string[]{
		
		"dwPrivID",		/*Ȩ��ID*/
		
		"dwUidSN",		/*UID���*/
		
		"dwUIDType",		/*��������*/
		
		"dwFuncSN",		/*��������ģ����*/
		
		"szFuncName",		/*��������ģ������*/
	
		"szUIDName",		/*UID����*/
	
      "dwWarrantType",		/*��ɷ�ʽ*/
		
	"FieldLimit",		/*��ӦUID�ĸ��ֶ����ƹ���*/
	 ""};

	/*��ȡ����Ȩ������*/
	static public string[] OPPRIVREQ = new string[]{
		
		"dwOPID",		/*����Ȩ��ID*/
		
		"szOPName",		/*����Ȩ�����ƣ�ģ��ƥ�䣩*/
	
		"dwFuncSN",		/*��������ģ����*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*����Ȩ��*/
	static public string[] OPPRIV = new string[]{
		
		"dwOPID",		/*����Ȩ��ID*/
		
		"szOPName",		/*����Ȩ������*/
	
		"dwDefWarType",		/*ȱʡ��ɷ�ʽ�������PRIVUID��*/
		
		"dwSysFuncMask",		/*֧��ϵͳ����ģ��*/
		
		"dwFuncSN",		/*��������ģ����*/
		
		"szFuncName",		/*��������ģ������*/
	
	"PrivUID",		/*��UIDȨ����ϸ��(CUniTable<PRIVUID>)*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*��ȡ�û���ɫ*/
	static public string[] USERROLEREQ = new string[]{
		
		"dwRoleID",		/*��ɫID*/
		
		"szRoleName",		/*��ɫ���ƣ�ģ��ƥ�䣩*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*�û���ɫ*/
	static public string[] USERROLE = new string[]{
		
		"dwRoleID",		/*��ɫID*/
		
		"szRoleName",		/*��ɫ����*/
	
		"dwDefWarType",		/*ȱʡ��ɷ�ʽ�������PRIVUID��*/
		
		"dwSysFuncMask",		/*֧��ϵͳ����ģ��*/
		
	"OpPriv",		/*����Ȩ�ޱ�(CUniTable<OPPRIV>)*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*�ͻ���ж������ṹ*/
	static public string[] CLTPASSWD = new string[]{
		
		"dwPassWdCode",		/*����CODE*/
		
		"szPassword",		/*����*/
	
		"dwSetDate",		/*��������*/
		
		"szOperator",		/*���ù���Ա*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/**/
	static public string[] ADMINREQ = new string[]{
		
		"dwStaSN",		/*�ڵ���*/
		
		"dwAccNo",		/*�ʺ�*/
		
		"szLogonName",		/*��¼��*/
	
		"szTrueName",		/*����*/
	
		"dwDeptID",		/*����ID*/
		
		"szDeptName",		/*����*/
	
		"dwManRole",		/*����Ա��ɫ*/
		
		"dwIdent",		/*����Ա���*/
		
		"dwProperty",		/*����Ա���ԣ����¶���+������ͣ�*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*����Ա��Ϣ*/
	static public string[] UNIADMIN = new string[]{
		
		"dwStaSN",		/*�ڵ���*/
		
		"dwAccNo",		/*�ʺ�*/
		
		"dwManRole",		/*����Ա��ɫ*/
		
		"dwStatus",		/*״̬*/
		
      "dwProperty",		/*����Ա���ԣ����¶���+������ͣ�*/
		
		"dwExpDate",		/*������*/
		
		"szLogonName",		/*��¼��*/
	
		"szTrueName",		/*����*/
	
		"dwIdent",		/*����Ա���*/
		
      "dwManLevel",		/*����Ա���𣨱���ѧԺ����У����)*/
		
		"dwDeptID",		/*����ID*/
		
		"szDeptName",		/*����*/
	
		"szTel",		/*�绰*/
	
		"szHandPhone",		/*�ֻ�*/
	
		"szEmail",		/*����*/
	
	"UserRole",		/*��ɫ��(CUniTable<USERROLE>)*/
	
		"szMemo",		/*��ע*/
	 ""};

	/*��ȡ����������*/
	static public string[] MANROOMREQ = new string[]{
		
		"dwAccNo",		/*�ʺ�*/
		
		"dwManFlag",		/*�����־(�ձ�ʾ��ȡȫ����0��ȡδ�����䣬1��ȡ������)*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*����Ա������*/
	static public string[] MANROOM = new string[]{
		
		"dwRoomID",		/*����ID*/
		
		"szRoomNo",		/*������*/
	
		"szRoomName",		/*��������*/
	
		"dwManGroupID",		/*����Ա��ID*/
		
		"dwManFlag",		/*�����־(0û��Ȩ�ޣ�1�й���Ȩ��)*/
		 ""};

	/**/
	static public string[] ADMINCHECK = new string[]{
		
		"dwSubjectID",		/*ȷ������ID*/
		
      "dwSubjectType",		/*ȷ���������*/
		
      "dwCheckStat",		/*����Ա���״̬(��չ�ɸ�������ȷ��ʱ����)*/
		
		"dwApplicantID",		/*�������˺�*/
		
		"szApplicantName",		/*����������*/
	
		"szCheckDetail",		/*���˵��*/
	
		"szMemo",		/*��ע*/
	
		"szSubjectInfo",		/*��Ӧ��ȷ�����ɽṹ��ϸ��Ϣ*/
	 ""};

	/*�豸ʹ�������Ϣ*/
	static public string[] USEDDEVCHECKINFO = new string[]{
		
		"dwSID",		/*��ˮ��*/
		
		"dwAccNo",		/*ʹ����*/
		
		"szTrueName",		/*ʹ��������*/
	
		"dwLabID",		/*ʵ����ID*/
		
		"dwDevID",		/*�豸ID*/
		
		"dwCheckStat",		/*�豸״̬*/
		
		"dwCompensation",		/*�⳥���*/
		
		"dwPunishScore",		/*���ÿ۷�*/
		
		"szDamageInfo",		/*��˵��*/
	
		"szExtInfo",		/*�豸������*/
	 ""};

	/*��ȡ�����Ϣ*/
	static public string[] CHECKREQ = new string[]{
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
		"dwCheckStat",		/*״̬*/
		
		"dwSubjectID",		/*����ID*/
		
		"dwSubjectType",		/*�������*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*�����Ϣ*/
	static public string[] CHECKINFO = new string[]{
		
		"dwSubjectID",		/*ȷ������ID*/
		
		"dwSubjectType",		/*ȷ���������*/
		
		"dwCheckStat",		/*����Ա���״̬(��չ�ɸ�������ȷ��ʱ����)*/
		
		"dwOccurDate",		/*��ʼ����*/
		
		"dwOccurTime",		/*����ʱ��*/
		
		"dwApplicantID",		/*�������˺�*/
		
		"szApplicantName",		/*����������*/
	
		"szMemo",		/*��ע*/
	 ""};

	/*��ȡ�����Ϣ��־*/
	static public string[] CHECKLOGREQ = new string[]{
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
		"dwCheckStat",		/*״̬*/
		
		"dwSubjectID",		/*����ID*/
		
		"dwSubjectType",		/*�������*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*�����Ϣ��־*/
	static public string[] CHECKLOG = new string[]{
		
		"dwSID",		/*��ˮ��*/
		
		"dwCheckStat",		/*����Ա���״̬(��չ�ɸ�������ȷ��ʱ����)*/
		
		"dwSubjectID",		/*ȷ������ID*/
		
		"dwSubjectType",		/*ȷ���������*/
		
		"dwOccurDate",		/*��ʼ����*/
		
		"dwOccurTime",		/*���ʱ��*/
		
		"dwAdminID",		/*������ʺ�*/
		
		"szAdminName",		/*�����*/
	
		"dwApplicantID",		/*�������˺�*/
		
		"szApplicantName",		/*����������*/
	
		"szCheckDetail",		/*���˵��*/
	
		"szMemo",		/*��ע*/
	 ""};

	/*��ȡˢ�±�־����*/
	static public string[] REFRESHFLAGREQ = new string[]{
		
      "dwRefreshType",		/*ˢ�����*/
		 ""};

	/*��ȡˢ�±�־����*/
	static public string[] REFRESHFLAGINFO = new string[]{
		
		"dwRefreshType",		/*ˢ�����*/
		
		"dwRefreshFlag",		/*ˢ�±�־*/
		 ""};

	/*��ȡ�ڼ���*/
	static public string[] HOLIDAYREQ = new string[]{
		
		"szName",		/*���ƣ�ģ��ƥ�䣩*/
	
		"dwDate",		/*����*/
		 ""};

	/*�ڼ�����Ϣ*/
	static public string[] UNIHOLIDAY = new string[]{
		
		"szName",		/*���ƣ�ģ��ƥ�䣩*/
	
		"dwStartDay",		/*��ʼ����(MMDD��YYYYMMDD)*/
		
		"dwEndDay",		/*��������(MMDD��YYYYMMDD)*/
		
		"szMemo",		/*����˵��*/
	 ""};

	/*���ĳ��ֵ�Ƿ��������*/
	static public string[] CHECKEXISTREQ = new string[]{
		
		"dwUID",		/*�����½��޸ĵ�ID������MSREQ_ADMIN_SET*/
		
		"szName",		/*�жϵ��ֶ�����(�����Ӧ�Ľṹ���������ͬ,����szLogonName*/
	
		"szValue",		/*��Ҫ����ֵ�������ֵ�ת�����ַ���*/
	
		"szCon",		/*SQL�������ֵ����Ϊ��*/
	 ""};

	/*��ȡĳ���ֶε����ֵ����*/
	static public string[] MAXVALUEREQ = new string[]{
		
		"dwUID",		/*�����½��޸ĵ�ID������MSREQ_ADMIN_SET*/
		
		"szName",		/*�жϵ��ֶ�����(�����Ӧ�Ľṹ���������ͬ,����szLogonName*/
	
		"szCon",		/*SQL�������ֵ����Ϊ��*/
	 ""};

	/*�������ֵ*/
	static public string[] MAXVALUE = new string[]{
		
		"dwValue",		/*���ص����ֵ*/
		 ""};

	/*����������Ա���������Ϣ����*/
	static public string[] IFPARAMREQ = new string[]{
		
		"dwAdminID",		/*����ԱID*/
		 ""};

	/*����������Ա���������Ϣ*/
	static public string[] IFPARAM = new string[]{
		
		"dwAdminID",		/*����ԱID*/
		
		"szParam",		/*����*/
	
		"szMemo",		/*��ע*/
	 ""};

	/*��ȡ����Ա������־�����*/
	static public string[] ADMINLOGREQ = new string[]{
		
		"dwStaSN",		/*�ڵ���*/
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
		"dwAdminID",		/*����ԱID*/
		
		"szTrueName",		/*��ʵ������ģ��ƥ�䣩*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*����Ա������־*/
	static public string[] ADMINLOG = new string[]{
		
		"dwStaSN",		/*�ڵ���*/
		
		"dwAdminID",		/*����ԱID*/
		
		"szTrueName",		/*��ʵ����*/
	
		"dwOpDate",		/*��������*/
		
		"dwOpTime",		/*����ʱ��*/
		
		"dwOpUID",		/*�����ӿ�ID*/
		
		"szOpInfo",		/*��������*/
	
		"szOpDetail",		/*������ϸ˵��*/
	 ""};

	/*IP��ַ������*/
	static public string[] IPBLACKLIST = new string[]{
		
		"szIP",		/*ip��ַ*/
	
		"szTryAdmin",		/*�����˺�*/
	
		"dwTryTimes",		/*���Դ���*/
		
		"dwLockEndTime",		/*��������ʱ��*/
		 ""};

	/*����Ա�޸�����*/
	static public string[] ADMINCHGPASSWD = new string[]{
		
		"szCurAdminPw",		/*��ǰ��¼�Ĺ���Ա����*/
	
		"dwAdminID",		/*����Ա�ʺ�*/
		
		"szNewPw",		/*������*/
	
		"szMemo",		/*��ע����չ�ã�*/
	 ""};

	/*ϵͳ״̬*/
	static public string[] BSYSINFO = new string[]{
		
		"dwStatus",		/*��״̬*/
		
	"ParamStat",		/*���ָ��״̬*/
	
		"szStatInfo",		/*״̬˵��*/
	 ""};

	/*״̬ͳ����Ϣ*/
	static public string[] STATUSINFO = new string[]{
		
		"dwStatus",		/*״ֵ̬*/
		
		"dwNum",		/*����*/
		 ""};

	/*�����Ϣͳ��*/
	static public string[] BCHECKSTAT = new string[]{
		
		"dwStatus",		/*��״̬*/
		
	"CheckStatInfo",		/*���ͳ�Ʊ�*/
	
		"szStatInfo",		/*״̬˵��*/
	 ""};

	/*�豸��Ϣͳ��*/
	static public string[] BDEVSTAT = new string[]{
		
		"dwStatus",		/*��״̬*/
		
	"DevStatInfo",		/*�豸״̬ͳ�Ʊ�*/
	
		"szStatInfo",		/*״̬˵��*/
	 ""};

	/*������Ϣͳ��*/
	static public string[] BROOMSTAT = new string[]{
		
		"dwStatus",		/*��״̬*/
		
	"RoomStatInfo",		/*����״̬ͳ�Ʊ�*/
	
		"szStatInfo",		/*״̬˵��*/
	 ""};

	/*���տγ�ͳ��*/
	static public string[] BTODAYRESVSTAT = new string[]{
		
		"dwStatus",		/*��״̬*/
		
	"TodayResvStatInfo",		/*���տγ�״̬ͳ�Ʊ�*/
	
		"szStatInfo",		/*״̬˵��*/
	 ""};

	/*�����ϻ�ͳ��*/
	static public string[] BFREEUSESTAT = new string[]{
		
		"dwStatus",		/*��״̬*/
		
		"dwCurUsers",		/*��ǰ����*/
		
	"FreeTodayUseStat",		/*���������ϻ�ͳ��(��Сʱ)*/
	
		"szStatInfo",		/*״̬˵��*/
	 ""};

	/*������������Ϣ*/
	static public string[] BASICSTAT = new string[]{
		
		"dwChgNum",		/*��Ϣ�����ı��ͳ����Ŀ��*/
		
      "dwStatus",		/*״̬*/
		
	"SysStat",		/*ϵͳ״̬*/
	
	"CheckStat",		/*�����Ϣͳ��*/
	
	"DevStat",		/*�豸��Ϣͳ��*/
	
	"RoomStat",		/*������Ϣͳ��*/
	
	"TodayResvStat",		/*���տγ�ͳ��*/
	
	"FreeUseStat",		/*�����ϻ�ͳ��*/
	
		"szStatInfo",		/*״̬˵��*/
	 ""};

	/*����������*/
	static public string[] CHECKTYPEREQ = new string[]{
		
		"dwCheckKind",		/*�������(�ɶ��)*/
		
		"dwMainKind",		/*��˴���*/
		 ""};

	/*������*/
	static public string[] CHECKTYPE = new string[]{
		
		"dwCheckKind",		/*��˱�ţ��½�ʱ��ϵͳ�Զ����䣩*/
		
      "dwMainKind",		/*��˴���*/
		
		"szCheckName",		/*�������*/
	
		"dwCheckLevel",		/*��˼���(ͬUNIADMIN.dwManLevel���壩*/
		
		"dwDeptID",		/*���β���ID��ѧԺ�������ã������������Զ�ƥ�䣩*/
		
		"szDeptName",		/*���β���*/
	
		"szMemo",		/*״̬˵��*/
	 ""};

	/*��ȡ�û������������*/
	static public string[] USERFEEDBACKREQ = new string[]{
		
		"dwFeedKind",		/*��������*/
		
		"dwFeedStat",		/*״̬*/
		
		"dwPurpose",		/*��;*/
		
		"dwResvID",		/*ԤԼID*/
		
		"dwSNum",		/*��ˮ��*/
		
		"dwAccNo",		/*�ʺ�*/
		
		"dwDevID",		/*ʹ���豸*/
		
		"dwMinScore",		/*�û��������*/
		
		"dwMaxScore",		/*�û��������*/
		
		"dwBeginDate",		/*ԤԼ��ʼ����*/
		
		"dwEndDate",		/*ԤԼ��������*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*�û��������*/
	static public string[] USERFEEDBACK = new string[]{
		
		"dwSNum",		/*��ˮ��*/
		
		"dwAccNo",		/*�ʺ�*/
		
		"szTrueName",		/*����*/
	
		"szTel",		/*�绰*/
	
		"szHandPhone",		/*�ֻ�*/
	
		"szEmail",		/*����*/
	
		"dwUserDeptID",		/*����ID*/
		
		"szUserDeptName",		/*����*/
	
      "dwFeedKind",		/*��������*/
		
      "dwFeedStat",		/*״̬*/
		
		"dwPurpose",		/*��;*/
		
		"dwResvID",		/*ԤԼID*/
		
		"dwDevID",		/*ʹ���豸*/
		
		"dwDevKind",		/*�豸����*/
		
		"dwDevSN",		/*�豸���*/
		
		"szDevName",		/*�豸����*/
	
		"dwRoomID",		/*����ID*/
		
		"szRoomName",		/*��������*/
	
		"dwLabID",		/*ʵ����ID*/
		
		"szLabName",		/*ʵ��������*/
	
		"dwScore",		/*�û�����*/
		
		"dwOccurDate",		/*��������*/
		
		"dwOccurTime",		/*����ʱ��*/
		
		"szIntroInfo",		/*������Ϣ*/
	
		"szReplyInfo",		/*�ظ���Ϣ*/
	
		"dwReplyDate",		/*�ظ�����*/
		
		"dwReplyTime",		/*�ظ�ʱ��*/
		
		"dwAnswererID",		/*�ظ��ʺ�*/
		
		"szAnswerer",		/*�ظ���*/
	
		"szMemo",		/*״̬˵��*/
	 ""};

	/*�����������*/
	static public string[] SERVICETYPEREQ = new string[]{
		
		"dwServiceKind",		/*�������(�ɶ��)*/
		 ""};

	/*�������*/
	static public string[] UNISERVICETYPE = new string[]{
		
		"dwServiceKind",		/*�����ţ��½�ʱ��ϵͳ�Զ����䣩*/
		
		"szServiceName",		/*��������*/
	
		"dwServiceLevel",		/*�����ż���(ͬUNIADMIN.dwManLevel���壩*/
		
		"dwDeptID",		/*������ID��ѧԺ�������ã������������Զ�ƥ�䣩*/
		
		"szDeptName",		/*������*/
	
      "dwProperty",		/*�������*/
		
		"szMemo",		/*˵��*/
	 ""};

	/*��ȡ�û������������*/
	static public string[] POLLONLINEREQ = new string[]{
		
		"dwPollID",		/*��ˮ��*/
		
		"dwVoteStat",		/*ͶƱ״̬*/
		
		"dwBeginDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*ͶƱѡ��*/
	static public string[] POLLITEM = new string[]{
		
		"dwItemID",		/*ѡ���*/
		
		"szItemName",		/*ѡ������*/
	
		"dwGroupID",		/*��ID*/
		
		"dwPollKind",		/*�������*/
		
		"dwMaxTickItems",		/*���ɹ�ѡѡ��*/
		
		"dwVotes",		/*��Ʊ��*/
		
		"szMemo",		/*״̬˵��*/
	 ""};

	/*����ͶƱ��Ϣ*/
	static public string[] POLLONLINE = new string[]{
		
		"dwPollID",		/*ͶƱID*/
		
		"dwAccNo",		/*�����ˣ������ˣ��ʺ�*/
		
		"szTrueName",		/*����*/
	
		"szTel",		/*�绰*/
	
		"szHandPhone",		/*�ֻ�*/
	
		"szEmail",		/*����*/
	
      "dwVoteStat",		/*ͶƱ״̬*/
		
      "dwPollScope",		/*�����Χ*/
		
      "dwPollKind",		/*�������*/
		
		"dwMaxTickItems",		/*���ɹ�ѡѡ��*/
		
		"szPollSubject",		/*ͶƱ����*/
	
		"dwBeginDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��ֹ����*/
		
		"dwTotalUsers",		/*ͶƱ������*/
		
	"PollItems",		/*CUniTable[POLLITEM]*/
	
		"szMemo",		/*״̬˵��*/
	 ""};

	/*ͶƱ*/
	static public string[] POLLVOTE = new string[]{
		
		"dwPollID",		/*ͶƱ���*/
		
		"dwItemID",		/*ѡ���*/
		
		"szMemo",		/*״̬˵��*/
	 ""};

}
	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/
public partial class Struct_ST
{

	/*��ȡվ��������*/
	static public string[] STATIONREQ = new string[]{
		
      "dwGetType",		/*��ʽ*/
		
		"szGetKey",		/*����ֵ*/
	 ""};

	/*�豸��Ӳ��������Ϣ*/
	static public string[] DEVICECONFIG = new string[]{
		
		"dwStaSN",		/*վ����*/
		
      "dwCfgType",		/*��������*/
		
		"szCfgName",		/*��������*/
	
		"szBrand",		/*Ʒ��*/
	
		"szModel",		/*����ͺ�*/
	
		"szSpecification",		/*�豸���*/
	
		"szPurpose",		/*��;˵��*/
	
		"szIndicators",		/*��Ҫָ��˵��*/
	
		"dwPurchaseDate",		/*�ɹ����� YYYYMMDD*/
		
		"dwStartUseDate",		/*Ͷ��ʹ������ YYYYMMDD*/
		
		"dwDevLife",		/*�豸ʹ������*/
		
		"szCPU",		/*CPU����ͺ�*/
	
		"dwMemSize",		/*�ڴ��С��M��*/
		
		"dwDiskSize",		/*Ӳ�̴�С(G)*/
		
		"dwOsVer",		/*����ϵͳ�汾(dwMajorVersion*1000000 + dwMinorVersion*10000 + wProductType*100 +ϵͳ����(32λ��64λ) )*/
		
		"szMemo",		/*��ע*/
	
		"dwDelFlag",		/*ɾ����־*/
		 ""};

	/**/
	static public string[] UNISTATION = new string[]{
		
		"dwStaSN",		/*վ����*/
		
		"szStaName",		/*վ������*/
	
		"szLicSN",		/*��ɱ��*/
	
      "dwSubSysSN",		/*��ϵͳ���*/
		
		"dwStatus",		/*վ��״̬*/
		
		"dwOwnerDept",		/*��������*/
		
		"dwManagerID",		/*�������˺�*/
		
		"dwAttendantID",		/*ֵ��Ա�˺�*/
		
		"szDeptName",		/*��������*/
	
		"szManName",		/*����������*/
	
		"szAttendantName",		/*ֵ��Ա����*/
	
		"szMemo",		/*��ע*/
	 ""};

}
	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/
public partial class Struct_ST
{

	/**/
	static public string[] UNIDEPT = new string[]{
		
		"dwID",		/*ID*/
		
		"szDeptSN",		/*��λ���*/
	
		"szName",		/*����*/
	
      "dwKind",		/*����*/
		
		"szMemo",		/*��ע*/
	 ""};

	/**/
	static public string[] DEPTREQ = new string[]{
		
		"dwID",		/*ID*/
		
		"szName",		/*����*/
	
		"dwKind",		/*����(�������Ż�ѧԺ)*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*У��*/
	static public string[] UNICAMPUS = new string[]{
		
		"dwCampusID",		/*У��ID*/
		
		"szCampusName",		/*У������*/
	
		"dwCampusKind",		/*У�����ͣ���չ��*/
		
		"szMemo",		/*��ע*/
	 ""};

	/*��ȡУ������*/
	static public string[] CAMPUSREQ = new string[]{
		
		"dwCampusID",		/*У��ID*/
		
		"szCampusName",		/*У������*/
	
		"dwCampusKind",		/*У�����ͣ���չ��*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/**/
	static public string[] CLASSREQ = new string[]{
		
		"dwClassID",		/*ID*/
		
		"szClassName",		/*����*/
	
		"dwMajorID",		/*רҵID*/
		
		"dwEnrolYear",		/*��ѧ���*/
		
		"dwClassKind",		/*����*/
		
		"dwDeptID",		/*����ID*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/**/
	static public string[] UNICLASS = new string[]{
		
		"dwClassID",		/*ID*/
		
		"szClassSN",		/*�༶���*/
	
		"szClassName",		/*����*/
	
		"dwClassKind",		/*����*/
		
		"dwDeptID",		/*����ID*/
		
		"dwMajorID",		/*רҵID*/
		
		"dwEnrolYear",		/*��ѧ���*/
		
		"szMemo",		/*��ע*/
	 ""};

	/*��ȡ�˻��б��������*/
	static public string[] ACCREQ = new string[]{
		
		"dwCardID",		/*��ID��*/
		
		"dwAccNo",		/*�˺�*/
		
		"szPID",		/*ѧ����*/
	
		"szLogonName",		/*��¼��(ѧ����)*/
	
		"szCardNo",		/*����*/
	
		"szTrueName",		/*����*/
	
		"dwIdent",		/*���*/
		
		"dwStatus",		/*״̬*/
		
		"dwEnrolYear",		/*��ѧ���(XX��)*/
		
		"dwClassID",		/*�༶ID*/
		
		"dwDeptID",		/*����ID*/
		
		"szMSN",		/*MSN*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*�˻���Ϣ*/
	static public string[] UNIACCOUNT = new string[]{
		
		"dwAccNo",		/*�˺�*/
		
		"szPID",		/*ѧ����*/
	
		"szLogonName",		/*��¼��(ѧ����)*/
	
		"szCardNo",		/*����*/
	
		"dwCardID",		/*��ID��*/
		
		"szIDCard",		/*���֤��*/
	
		"szTrueName",		/*����*/
	
		"szPasswd",		/*��������*/
	
		"dwClassID",		/*�༶ID*/
		
		"szClassName",		/*�༶*/
	
		"dwDeptID",		/*����ID*/
		
		"szDeptName",		/*����*/
	
		"dwMajorID",		/*רҵID*/
		
		"szMajorName",		/*רҵ*/
	
		"dwSex",		/*�Ա��UniCommon.h*/
		
      "dwIdent",		/*��� ��UniCommon.h*/
		
      "dwKind",		/*����*/
		
		"dwBirthday",		/*��������*/
		
		"dwEnrolYear",		/*��ѧ���(XX��)*/
		
		"dwSchoolYears",		/*ѧ��*/
		
		"dwBalance",		/*���*/
		
		"dwSubsidy",		/*����*/
		
		"dwFreeTime",		/*���ʱ��(��ʱ)*/
		
		"dwUseQuota",		/*�����޶�*/
		
		"dwStatus",		/*״̬ �����UniCommon.h*/
		
		"dwExpiredDate",		/*������*/
		
		"szTel",		/*�绰*/
	
		"szHandPhone",		/*�ֻ�*/
	
		"szEmail",		/*����*/
	
		"szMSN",		/*MSN*/
	
		"szQQ",		/*QQ*/
	
		"szHomeAddr",		/*��ͥסַ*/
	
		"szCurAddr",		/*У��סַ*/
	
		"szMemo",		/*˵����Ϣ*/
	
		"szCurZip",		/*������ַ�ʱ�(��ʦ��Ҫ)*/
	
		"dwTutorID",		/*��ʦ�˺�*/
		
		"szTutorName",		/*��ʦ����*/
	 ""};

	/*��չ�˻���Ϣ*/
	static public string[] UNIACCEXTINFO = new string[]{
		
		"pPhoto",		/*��Ƭ*/
	 ""};

	/*��ȡ��ʦ*/
	static public string[] TUTORREQ = new string[]{
		
		"dwTutorID",		/*��ʦ�˺�*/
		
		"dwStudentAccNo",		/*ѧ���˺�*/
		
		"szTrueName",		/*��ʦ����*/
	
		"dwDeptID",		/*����ID*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*��ʦ��Ϣ*/
	static public string[] UNITUTOR = new string[]{
		
		"dwAccNo",		/*�˺�*/
		
		"szTrueName",		/*����*/
	
		"szTel",		/*�绰*/
	
		"szHandPhone",		/*�ֻ�*/
	
		"szEmail",		/*����*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*��ȡ��չ�����Ա��Ϣ*/
	static public string[] EXTIDENTACCREQ = new string[]{
		
		"dwIdent",		/*���*/
		
		"dwAccNo",		/*�˺�*/
		
		"szTrueName",		/*��ʦ����*/
	
		"dwDeptID",		/*����ID*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*��ȡ�û���Ϣ*/
	static public string[] ACCINFOREQ = new string[]{
		
		"dwAccNo",		/*�˺�*/
		
		"szLogonName",		/*��¼��*/
	 ""};

	/*��չ�����Ա��Ϣ*/
	static public string[] EXTIDENTACC = new string[]{
		
		"dwAccNo",		/*�˺�*/
		
		"dwIdent",		/*���*/
		
		"szPID",		/*ѧ����*/
	
		"szLogonName",		/*��¼��*/
	
		"szTrueName",		/*����*/
	
		"dwSex",		/*�Ա�*/
		
		"dwDeptID",		/*����ID*/
		
		"szDeptName",		/*����*/
	
		"szTel",		/*�绰*/
	
		"szHandPhone",		/*�ֻ�*/
	
		"szEmail",		/*����*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*��ȡ��ʦ��ѧ��*/
	static public string[] TUTORSTUDENTREQ = new string[]{
		
		"dwTutorID",		/*��ʦ�˺�*/
		
		"dwStatus",		/*ѧ��״̬*/
		
		"dwKind",		/*ѧ������*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*��ʦѧ����Ϣ*/
	static public string[] TUTORSTUDENT = new string[]{
		
		"dwTutorID",		/*��ʦ�˺�*/
		
		"szTutorName",		/*��ʦ����*/
	
		"dwStatus",		/*ѧ��״̬�����״̬��ADMINCHECK��*/
		
      "dwKind",		/*ѧ������*/
		
		"dwAccNo",		/*�˺�*/
		
		"szPID",		/*ѧ����*/
	
		"szTrueName",		/*����*/
	
		"dwEnrolYear",		/*��ѧ���(XX��)*/
		
		"szTel",		/*�绰*/
	
		"szHandPhone",		/*�ֻ�*/
	
		"szEmail",		/*����*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*��ʦ���ѧ��*/
	static public string[] TUTORSTUDENTCHECK = new string[]{
		
		"dwTutorID",		/*��ʦID*/
		
		"dwCheckStat",		/*���״̬(ADMINCHECK����)*/
		
		"dwStudentAccNo",		/*ѧ���˺�*/
		
		"szStudentName",		/*ѧ������*/
	
		"szCheckDetail",		/*���˵��*/
	
		"szMemo",		/*��ע*/
	 ""};

	/**/
	static public string[] ACCCHECKREQ = new string[]{
		
      "dwCheckType",		/*��ʽ*/
		
		"szCheckKey",		/*��֤�ؼ���*/
	
		"szCheckPW",		/*��֤����*/
	
	"szAccInfo",		/*(UNIACCOUNT�ṹ)*/
	 ""};

	/*���˿�ṹ*/
	static public string[] UNIDEPOSIT = new string[]{
		
      "dwKind",		/*���*/
		
		"dwAccNo",		/*�˺�*/
		
		"szPID",		/*ѧ����*/
	
		"szTrueName",		/*��ʵ����*/
	
		"dwAmount",		/*����*/
		
		"dwAdminID",		/*����Ա*/
		
		"szMemo",		/*��ע*/
	 ""};

	/*֧�������ύ������ˮ�ṹ*/
	static public string[] UNIPAYMENT = new string[]{
		
		"dwSID",		/*��ˮ��*/
		
		"dwAccNo",		/*�ʺ�*/
		
		"dwCostDate",		/*��������*/
		
		"dwCostTime",		/*����ʱ��*/
		
		"dwCardTime",		/*���۷�ʱ��*/
		
		"dwDealTime",		/*�ύ��ˮʱ��*/
		
		"szPID",		/*ѧ����*/
	
		"szCardNo",		/*���俨��*/
	
		"szTrueName",		/*��ʵ����*/
	
		"dwFeeType",		/*�������(FEEDETAIL����)*/
		
		"dwCostMoney",		/*���ѽ��*/
		
		"dwCostSubsidy",		/*���Ѳ���*/
		
		"dwCostFreeTime",		/*���Ѳ���ʱ��*/
		
		"szPosInfo",		/*��һ��ͨ��Ӧ���̻���Ϣ*/
	
		"szCardCostInfo",		/*���۷ѷ�����Ϣ����ͬ��һ��ͨ��ʽ�����ݶ���ͬ*/
	
		"dwRetStatus",		/*�ύ����״̬���ο�UniCommon.h����*/
		
		"dwRetDealSID",		/*���ص�������ˮ��*/
		
		"szRetDealInfo",		/*�ύ��ˮ������Ϣ����ͬ��һ��ͨ��ʽ�����ݶ���ͬ*/
	 ""};

	/*��ȡ������Ϣ*/
	static public string[] NOTICEREQ = new string[]{
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
		"dwSID",		/*��ˮ��*/
		
		"dwStatus",		/*״̬*/
		
		"dwSubjectID",		/*֪ͨ����ID*/
		
		"dwSubjectType",		/*֪ͨ�������*/
		
		"dwSender",		/*���ͷ�*/
		
		"dwRecipient",		/*���ܷ�*/
		 ""};

	/*������Ϣ*/
	static public string[] NOTICEINFO = new string[]{
		
		"dwSID",		/*��ˮ��*/
		
		"dwSubSysSN",		/*��ϵͳ���*/
		
		"dwStatus",		/*״̬*/
		
		"dwSubjectID",		/*֪ͨ����ID*/
		
		"dwSubjectType",		/*֪ͨ�������,��˲�����֪ͨ��������(CHECKINFO::dwSubjectType)ͬ*/
		
		"dwSender",		/*���ͷ��ʺ�*/
		
		"dwRecipient",		/*���շ��ʺ�*/
		
		"dwNoticeMode",		/*֪ͨ��ʽ*/
		
		"dwOccurTime",		/*����ʱ��*/
		
		"dwSendTime",		/*����ʱ��*/
		
		"dwAffirmTime",		/*ȷ��ʱ��*/
		
		"szMemo",		/*��ע*/
	
		"dwNoticeKind",		/*֪ͨ���*/
		
		"dwCheckStat",		/*���״̬*/
		
		"szRecvName",		/*����������*/
	
		"szRecvMobile",		/*�������ֻ�*/
	
		"szRecvMail",		/*����������*/
	
		"szSenderName",		/*����������*/
	
		"szSenderMobile",		/*�������ֻ�*/
	
		"szSenderMail",		/*����������*/
	
		"szSessionID",		/*SessionID*/
	
		"szReason",		/*ԭ��*/
	
		"szFullSendInfo",		/*��������*/
	 ""};

	/*֪ͨ����ȷ��*/
	static public string[] NOTICEAFFIRM = new string[]{
		
		"dwSID",		/*��ˮ��*/
		
		"dwAffirmStat",		/*ȷ��״̬*/
		
		"dwNoticeMode",		/*֪ͨ��ʽ*/
		
		"dwAffirmTime",		/*ȷ��ʱ��*/
		
		"szMemo",		/*��ע*/
	 ""};

	/**/
	static public string[] MAJORREQ = new string[]{
		
		"dwMajorID",		/*ID*/
		
		"szMajorSN",		/*���*/
	
		"szMajorName",		/*����*/
	
		"dwKind",		/*����*/
		
		"dwDeptID",		/*����ID*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/**/
	static public string[] UNIMAJOR = new string[]{
		
		"dwMajorID",		/*ID*/
		
		"szMajorSN",		/*���*/
	
		"szMajorName",		/*����*/
	
		"dwKind",		/*����*/
		
		"dwDeptID",		/*����ID*/
		
		"szDeptName",		/*��������*/
	
		"dwSchoolYears",		/*��ѧ���*/
		
		"szMemo",		/*��ע*/
	 ""};

	/**/
	static public string[] TESTDATAREQ = new string[]{
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
		"dwStatus",		/*����״̬*/
		
		"dwSID",		/*SID*/
		
		"dwAccNo",		/*�˺�*/
		
		"dwDevID",		/*�豸ID*/
		
		"dwRoomID",		/*����ID*/
		
		"dwLabID",		/*ʵ����ID*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/**/
	static public string[] UNITESTDATA = new string[]{
		
		"dwSID",		/*SID*/
		
		"dwDevID",		/*�豸ID*/
		
		"szDevName",		/*�豸����*/
	
		"dwLabID",		/*ʵ����ID*/
		
		"szLabName",		/*ʵ��������*/
	
		"dwRoomID",		/*����ID*/
		
		"szRoomName",		/*��������*/
	
		"dwAccNo",		/*�˺�*/
		
		"szPID",		/*ѧ����*/
	
		"szTrueName",		/*��ʵ����*/
	
		"dwSubmitDate",		/*�ύ����*/
		
		"dwSubmitTime",		/*�ύʱ��*/
		
		"dwFileSize",		/*�ļ���С*/
		
		"szDisplayName",		/*��ʾ����*/
	
		"szLocation",		/*���λ��*/
	
      "dwStatus",		/*״̬*/
		
		"szMemo",		/*˵��*/
	 ""};

	/*����Ա�ϴ�ʵ������*/
	static public string[] ADMINTESTDATA = new string[]{
		
		"szLogonName",		/*��¼��*/
	
		"szPassword",		/*����*/
	
	"TestData",		/*ʵ������(UNITESTDATA)*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/**/
	static public string[] CLOUDDISKREQ = new string[]{
		
		"dwAccNo",		/*�˺�*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/**/
	static public string[] CLOUDDISK = new string[]{
		
		"dwFileID",		/*�ļ�ID*/
		
		"szFileName",		/*�ļ�����*/
	
		"dwAccNo",		/*�˺�*/
		
		"szPID",		/*ѧ����*/
	
		"szTrueName",		/*��ʵ����*/
	
		"dwSubmitDate",		/*�ύ����*/
		
		"dwFileSize",		/*�ļ���С*/
		
		"szLocation",		/*���λ��*/
	
		"szMemo",		/*˵��*/
	 ""};

	/**/
	static public string[] CDISKSTATREQ = new string[]{
		
		"dwAccNo",		/*�˺�*/
		 ""};

	/**/
	static public string[] CDISKSTAT = new string[]{
		
		"dwAccNo",		/*�˺�*/
		
		"szPID",		/*ѧ����*/
	
		"szTrueName",		/*��ʵ����*/
	
		"dwTotalSize",		/*�ܴ�С*/
		
		"dwUsedSize",		/*���ÿռ�*/
		
		"dwFileNum",		/*�ļ�����*/
		
		"szMemo",		/*˵��*/
	 ""};

	/*��ȡ�ον�ʦ��Ϣ*/
	static public string[] UNITEACHERREQ = new string[]{
		
		"dwAccNo",		/*�˺�*/
		
		"szPID",		/*����*/
	
		"szTrueName",		/*��ʦ����(ģ��ƥ��)*/
	
		"dwDeptID",		/*����ID*/
		
		"dwCourseID",		/*�γ�ID*/
		
		"szCourseName",		/*�γ�����(ģ��ƥ��)*/
	
		"dwYearTerm",		/*ѧ�ڱ��*/
		
      "dwReqProp",		/*���󸽼�����*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*���ڿγ�*/
	static public string[] TEACHCOURSE = new string[]{
		
		"dwCourseID",		/*�γ�ID*/
		
		"szCourseCode",		/*�γ̴���*/
	
		"szCourseName",		/*�γ�����*/
	
		"szMemo",		/*��ע*/
	 ""};

	/*�ον�ʦ��Ϣ*/
	static public string[] UNITEACHER = new string[]{
		
		"dwAccNo",		/*�˺�*/
		
		"szPID",		/*ѧ����*/
	
		"szTrueName",		/*����*/
	
		"dwSex",		/*�Ա�*/
		
		"dwDeptID",		/*ѧԺ�����ţ�ID*/
		
		"szDeptName",		/*����ѧԺ*/
	
		"szTel",		/*�绰*/
	
		"szHandPhone",		/*�ֻ�*/
	
		"szEmail",		/*����*/
	
	"TeachCourse",		/*�е��γ�(CUniTable<TEACHCOURSE>)*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*�����û�ʹ����Ϣ*/
	static public string[] USERCURINFOREQ = new string[]{
		
		"dwAccNo",		/*�˺�*/
		 ""};

	/*�û�ʹ����Ϣ(ͬCONUSERINFO)*/
	static public string[] USERCURINFO = new string[]{
		
		"dwUserStat",		/*�û�״̬*/
		
	"AccInfo",		/*UNIACCOUNT �ṹ*/
	
	"ResvInfo",		/*UNIRESERVE �ṹ*/
	
	"DevInfo",		/*UNIDEVICE �ṹ*/
	
	"BillInfo",		/*�˵���(CUniTable<UNIBILL>)*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

}
	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/
public partial class Struct_ST
{

	/**/
	static public string[] UNILAB = new string[]{
		
		"dwLabID",		/*ʵ����ID*/
		
		"szLabSN",		/*ʵ���ұ��*/
	
		"szLabName",		/*ʵ��������*/
	
		"dwDeptID",		/*����ID*/
		
		"szDeptName",		/*��������*/
	
		"szLabKindCode",		/*ʵ�������ͱ���*/
	
		"szLabLevelCode",		/*ʵ���ҽ���ˮƽ����*/
	
		"szLabFromCode",		/*ʵ������Դ����*/
	
		"szAcademicSubjectCode",		/*����ѧ�Ʊ���*/
	
		"dwLabClass",		/*ʵ�������*/
		
		"dwManGroupID",		/*����Ա��ID*/
		
		"dwCreateDate",		/*��������*/
		
		"szLabURL",		/*ʵ���Ҽ��URL*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/**/
	static public string[] LABREQ = new string[]{
		
		"dwLabID",		/*ʵ����ID*/
		
		"szLabSN",		/*ʵ���ұ��*/
	
		"szLabName",		/*ʵ��������*/
	
		"dwDeptID",		/*����ID*/
		
		"dwLabClass",		/*ʵ���������*/
		
		"dwPurpose",		/*��;(��UNIRESERVE����)*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/**/
	static public string[] FULLLAB = new string[]{
		
		"dwLabID",		/*ʵ����ID*/
		
		"szLabSN",		/*ʵ���ұ��*/
	
		"szLabName",		/*ʵ��������*/
	
		"dwDeptID",		/*����ID*/
		
		"szDeptName",		/*��������*/
	
		"szLabKindCode",		/*ʵ�������ͱ���*/
	
		"szLabLevelCode",		/*ʵ���ҽ���ˮƽ����*/
	
		"szLabFromCode",		/*ʵ������Դ����*/
	
		"szAcademicSubjectCode",		/*����ѧ�Ʊ���*/
	
		"dwLabClass",		/*ʵ�������*/
		
		"szLabURL",		/*ʵ���Ҽ��URL*/
	
		"szMemo",		/*˵����Ϣ*/
	
		"dwAttendantID",		/*ֵ��Ա�˺�*/
		
		"szAttendantName",		/*ֵ��Ա����*/
	
		"dwTotalDevNum",		/*�豸����*/
		
		"dwUsableDevNum",		/*�����豸��*/
		
		"dwIdleDevNum",		/*�����豸��*/
		 ""};

	/**/
	static public string[] FULLLABREQ = new string[]{
		
		"dwLabID",		/*ʵ����ID*/
		
		"szLabSN",		/*ʵ���ұ��*/
	
		"szLabName",		/*ʵ��������*/
	
		"dwDeptID",		/*����ID*/
		
		"dwLabClass",		/*ʵ���������*/
		
		"dwPurpose",		/*��;(��UNIRESERVE����)*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/**/
	static public string[] DEVCLSREQ = new string[]{
		
		"dwClassID",		/*�豸(ʵ����)���ID*/
		
		"szClassName",		/*�豸(ʵ����)�������*/
	
		"dwKind",		/*���*/
		
		"dwPurpose",		/*��;(��UNIRESERVE����)*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*�豸���*/
	static public string[] UNIDEVCLS = new string[]{
		
		"dwClassID",		/*�豸(ʵ����)���ID*/
		
		"szClassSN",		/*�豸�����*/
	
		"szClassName",		/*�豸(ʵ����)�������*/
	
		"szMemo",		/*˵����Ϣ*/
	
      "dwKind",		/*�������*/
		
		"dwResv1",		/*�����ֶ�1*/
		
		"dwResv2",		/*�����ֶ�2*/
		
		"szDevClsURL",		/*���URL*/
	
		"szExtInfo",		/*��չ��Ϣ*/
	 ""};

	/**/
	static public string[] DEVKINDREQ = new string[]{
		
		"dwKindID",		/*�豸�������ID*/
		
		"szKindName",		/*�豸����*/
	
		"szClassName",		/*�����(*/
	
		"dwProperty",		/*�豸����*/
		
		"dwClassKind",		/*���(��UNIDEVCLS��Kind����)*/
		
		"szKindIDs",		/*��������,����ö��Ÿ���*/
	
		"dwExtRelatedID",		/*��չ����ID*/
		
		"dwPurpose",		/*��;(��UNIRESERVE����)*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*�豸�������*/
	static public string[] UNIDEVKIND = new string[]{
		
		"dwKindID",		/*�豸�������ID*/
		
		"szKindName",		/*�豸����*/
	
		"szFuncCode",		/*�豸������;����*/
	
		"szModel",		/*�豸�ͺ�*/
	
		"szSpecification",		/*�豸���*/
	
		"szProducer",		/*������*/
	
		"dwNationCode",		/*������*/
		
      "dwProperty",		/*�豸����*/
		
		"dwClassID",		/*�����������ID*/
		
		"szClassSN",		/*�豸�����*/
	
		"szClassName",		/*�����(*/
	
		"dwClassKind",		/*���(��UNIDEVCLS��Kind����)*/
		
		"dwMaxUsers",		/*��ԤԼ���ͬʱʹ������*/
		
		"dwMinUsers",		/*ÿ��ԤԼ����ͬʱʹ������*/
		
		"dwTotalNum",		/*�豸����*/
		
		"dwUsableNum",		/*�����豸����ԤԼ�ж��ã�*/
		
		"szOperaCert",		/*����֤��*/
	
		"szDevKindURL",		/*�豸��ϸ���ܵ�URL��ַ*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*��ȡ¥����Ϣ����*/
	static public string[] BUILDINGREQ = new string[]{
		
		"dwBuildingID",		/*¥��ID*/
		
		"szBuildingName",		/*¥������*/
	
		"szBuildingNo",		/*¥���*/
	
		"szCampusIDs",		/*У��ID,����ö��Ÿ���*/
	
		"dwActivitySN",		/*����ͱ��*/
		
		"dwPurpose",		/*��;(��UNIRESERVE����)*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*¥������*/
	static public string[] UNIBUILDING = new string[]{
		
		"dwBuildingID",		/*¥��ID*/
		
		"szBuildingName",		/*¥������*/
	
		"szBuildingNo",		/*¥���*/
	
		"szMapIndex",		/*��ͼ����*/
	
		"szBuildingURL",		/*��ϸ���ܵ�URL��ַ*/
	
		"dwCampusID",		/*У��ID*/
		
		"szCampusName",		/*У������*/
	
		"dwCampusKind",		/*У�����ͣ���չ��*/
		
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*��ȡ������Ϣ����*/
	static public string[] ROOMREQ = new string[]{
		
		"dwRoomID",		/*����ID*/
		
		"szRoomName",		/*��������*/
	
		"szRoomNo",		/*�����*/
	
		"dwLabClass",		/*ʵ���������*/
		
		"dwLabID",		/*ʵ����ID*/
		
		"szBuildingIDs",		/*¥��ID,����ö��Ÿ���*/
	
		"szCampusIDs",		/*У��ID,����ö��Ÿ���*/
	
		"dwInClassKind",		/*����豸���(��UNIDEVCLS��Kind����)*/
		
		"dwPurpose",		/*��;(��UNIRESERVE����)*/
		
		"dwProperty",		/*��չ����*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*��������*/
	static public string[] UNIROOM = new string[]{
		
		"dwRoomID",		/*����ID*/
		
		"szRoomName",		/*��������*/
	
		"szRoomNo",		/*�����*/
	
		"dwInClassKind",		/*����豸���(��UNIDEVCLS��Kind����)*/
		
		"szFloorNo",		/*����¥��*/
	
		"dwBuildingID",		/*¥��ID*/
		
		"szBuildingNo",		/*��¥���(*/
	
		"szBuildingName",		/*��¥����(*/
	
		"dwRoomSize",		/*�������(ƽ��)*/
		
		"szMapIndex",		/*��ͼ����*/
	
		"szRoomURL",		/*��ϸ���ܵ�URL��ַ*/
	
		"szSubRooms",		/*��������(�����ţ��ɶ�������Ÿ���)*/
	
		"szLabKindCode",		/*ʵ�������ͱ���*/
	
		"szLabLevelCode",		/*ʵ���ҽ���ˮƽ����*/
	
		"szLabFromCode",		/*ʵ������Դ����*/
	
		"szAcademicSubjectCode",		/*����ѧ�Ʊ���*/
	
		"dwLabClass",		/*ʵ�������*/
		
		"dwCreateDate",		/*��������*/
		
		"dwOpenRuleSN",		/*���Ź�����*/
		
		"szOpenRuleName",		/*���Ź�������*/
	
		"dwLabID",		/*ʵ����ID*/
		
		"szLabSN",		/*ʵ���ұ��*/
	
		"szLabName",		/*ʵ��������*/
	
		"szLabURL",		/*��ϸ���ܵ�URL��ַ*/
	
		"dwDeptID",		/*����ID*/
		
		"szDeptName",		/*��������*/
	
		"dwManGroupID",		/*�������Ա��ID*/
		
		"szManGroupName",		/*����Ա������*/
	
      "dwManMode",		/*���Ʒ�ʽ*/
		
		"dwCtrlSN",		/*���������*/
		
		"dwCampusID",		/*У��ID*/
		
		"szCampusName",		/*У������*/
	
		"dwCampusKind",		/*У�����ͣ���չ��*/
		
		"szMemo",		/*˵����Ϣ*/
	
		"szMAIP",		/*�ֻ�ǩ��IP��*/
	
      "dwProperty",		/*��չ����*/
		 ""};

	/*��ȡ������Ϣ����*/
	static public string[] FULLROOMREQ = new string[]{
		
		"dwRoomID",		/*����ID*/
		
		"szRoomName",		/*��������*/
	
		"szRoomNo",		/*�����*/
	
		"dwLabClass",		/*ʵ���������*/
		
		"dwLabID",		/*ʵ����ID*/
		
		"szBuildingIDs",		/*¥��ID,����ö��Ÿ���*/
	
		"szCampusIDs",		/*У��ID,����ö��Ÿ���*/
	
		"dwInClassKind",		/*����豸���(��UNIDEVCLS��Kind����)*/
		
		"dwPurpose",		/*��;(��UNIRESERVE����)*/
		
		"dwProperty",		/*��չ����*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*����������Ϣ*/
	static public string[] FULLROOM = new string[]{
		
		"dwRoomID",		/*����ID*/
		
		"szRoomName",		/*��������*/
	
		"szRoomNo",		/*�����*/
	
		"dwInClassKind",		/*����豸���(��UNIDEVCLS��Kind����)*/
		
		"szFloorNo",		/*����¥��*/
	
		"dwBuildingID",		/*¥��ID*/
		
		"szBuildingNo",		/*��¥���(*/
	
		"szBuildingName",		/*��¥����(*/
	
		"dwRoomSize",		/*�������(ƽ��)*/
		
		"szMapIndex",		/*��ͼ����*/
	
		"szRoomURL",		/*��ϸ���ܵ�URL��ַ*/
	
		"dwOpenRuleSN",		/*���Ź�����*/
		
		"szOpenRuleName",		/*���Ź�������*/
	
		"dwLabID",		/*ʵ����ID*/
		
		"szLabSN",		/*ʵ���ұ��*/
	
		"szLabName",		/*ʵ��������*/
	
		"szLabKindCode",		/*ʵ�������ͱ���*/
	
		"szLabLevelCode",		/*ʵ���ҽ���ˮƽ����*/
	
		"szLabFromCode",		/*ʵ������Դ����*/
	
		"szAcademicSubjectCode",		/*����ѧ�Ʊ���*/
	
		"dwLabClass",		/*ʵ�������*/
		
		"dwCreateDate",		/*��������*/
		
		"szLabURL",		/*��ϸ���ܵ�URL��ַ*/
	
		"dwDeptID",		/*����ID*/
		
		"szDeptName",		/*��������*/
	
		"dwManGroupID",		/*����Ա��ID*/
		
		"szManGroupName",		/*����Ա������*/
	
		"dwAttendantID",		/*ֵ��Ա�˺�*/
		
		"szAttendantName",		/*ֵ��Ա����*/
	
		"dwStatus",		/*�����Ž�״̬�� UNIDCS����*/
		
		"dwStatChgTime",		/*״̬�ı俪ʼʱ��(time������)*/
		
		"szStatInfo",		/*״̬����*/
	
		"dwManMode",		/*���Ʒ�ʽ*/
		
		"dwCtrlSN",		/*���������*/
		
		"dwCampusID",		/*У��ID*/
		
		"szCampusName",		/*У������*/
	
		"dwCampusKind",		/*У�����ͣ���չ��*/
		
		"dwTotalDevNum",		/*�豸����*/
		
		"dwUsableDevNum",		/*�����豸��*/
		
		"dwIdleDevNum",		/*�����豸��*/
		
		"szMemo",		/*˵����Ϣ*/
	
		"dwProperty",		/*��չ����*/
		 ""};

	/*��ȡ���������Ϣ����*/
	static public string[] BASICROOMREQ = new string[]{
		
		"dwRoomID",		/*����ID*/
		
		"dwLabID",		/*ʵ����ID*/
		
		"dwInClassKind",		/*����豸���(��UNIDEVCLS��Kind����)*/
		
		"dwDoorStat",		/*�����Ž�״̬�� UNIDCS����*/
		
      "dwUseStat",		/*����ʹ��״̬*/
		
		"dwUsePurpose",		/*��UNIDEVICE����*/
		
		"dwProperty",		/*��չ����*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*���������Ϣ*/
	static public string[] BASICROOM = new string[]{
		
		"dwRoomID",		/*����ID*/
		
		"szRoomName",		/*��������*/
	
		"szRoomNo",		/*�����*/
	 ""};

	/*��ȡͨ������Ϣ����*/
	static public string[] CHANNELGATEREQ = new string[]{
		
      "dwGetType",		/*��ʽ*/
		
		"szGetKey",		/*����ֵ*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*ͨ����*/
	static public string[] UNICHANNELGATE = new string[]{
		
		"dwChannelGateID",		/*ͨ����ID*/
		
		"szChannelGateName",		/*ͨ��������*/
	
		"szChannelGateNo",		/*ͨ���ű��*/
	
		"szRelatedRooms",		/*��������(�����ţ��ɶ�������Ÿ���)*/
	
		"szFloorNo",		/*����¥��*/
	
		"dwBuildingID",		/*¥��ID*/
		
		"szBuildingNo",		/*��¥���(*/
	
		"szBuildingName",		/*��¥����(*/
	
		"dwManGroupID",		/*����Ա��ID*/
		
		"szManGroupName",		/*����Ա������*/
	
		"dwUseGroupID",		/*�����û���ID*/
		
		"szUseGroupName",		/*�����û�������*/
	
		"dwOpenRuleSN",		/*���Ź�����*/
		
		"dwStatus",		/*�Ž�״̬�� UNIDCS����*/
		
		"dwStatChgTime",		/*״̬�ı俪ʼʱ��(time������)*/
		
		"szStatInfo",		/*״̬����*/
	
		"dwManMode",		/*������Ʒ�ʽ����UNIROOM*/
		
		"dwCtrlSN",		/*���������*/
		
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*���Ʒ�������*/
	static public string[] CHANNELGATECTRLINFO = new string[]{
		
		"dwChannelGateID",		/*ͨ����ID*/
		
		"szChannelGateNo",		/*ͨ���ű��*/
	
		"dwCmd",		/*��������,�ο�DEVCTRLINFO����*/
		
		"szParam",		/*���Ʋ���*/
	
		"szMemo",		/*��ע*/
	 ""};

	/*��ȡ�����������*/
	static public string[] ROOMGROUPREQ = new string[]{
		
		"dwRoomNum",		/*��Ϸ�����*/
		
		"dwRoomID",		/*����ID*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*������ϳ�Ա*/
	static public string[] RGMEMBER = new string[]{
		
		"dwRoomID",		/*����ID*/
		
		"szRoomNo",		/*�����*/
	
		"szRoomName",		/*��������*/
	
		"dwDevNum",		/*�豸��*/
		 ""};

	/*�������*/
	static public string[] ROOMGROUP = new string[]{
		
		"dwRGID",		/*�������ID*/
		
		"szRGName",		/*�����������*/
	
		"dwRoomNum",		/*��Ϸ�����*/
		
	"rgMember",		/*CUniTable[RGMEMBER]*/
	 ""};

	/*��ȡ�豸�б�*/
	static public string[] DEVREQ = new string[]{
		
		"dwDevID",		/*�豸ID*/
		
		"szSearchKey",		/*�����ؼ���*/
	
		"dwDevSN",		/*�豸���*/
		
		"szAssertSN",		/*�û����ʲ����*/
	
		"szTagID",		/*RFID��ǩID*/
	
		"szLabIDs",		/*ʵ����ID,����ö��Ÿ���*/
	
		"szRoomIDs",		/*����ID,����ö��Ÿ���*/
	
		"dwResvID",		/*ԤԼID*/
		
		"szKindIDs",		/*��������,����ö��Ÿ���*/
	
		"szClassIDs",		/*�����������ID,����ö��Ÿ���*/
	
		"szDeptIDs",		/*ѧԺID,����ö��Ÿ���*/
	
		"szBuildingIDs",		/*¥��ID,����ö��Ÿ���*/
	
		"szCampusIDs",		/*У��ID,����ö��Ÿ���*/
	
		"dwDevStat",		/*�豸״̬*/
		
		"dwRunStat",		/*�豸����״̬*/
		
		"dwUnNeedRunStat",		/*����������״̬*/
		
		"dwCtrlMode",		/*���Ʒ�ʽ*/
		
		"dwProperty",		/*�豸����*/
		
		"dwClassKind",		/*���(��UNIDEVCLS��Kind����)*/
		
		"dwManGroupID",		/*����Ա��ID*/
		
		"dwAttendantID",		/*ֵ��ԱID*/
		
		"szAttendantName",		/*ֵ��Ա����(ģ��)*/
	
		"dwMinUnitPrice",		/*��ͼ۸�*/
		
		"dwMaxUnitPrice",		/*���۸�*/
		
		"dwSPurchaseDate",		/*��ʼ�ɹ�����*/
		
		"dwEPurchaseDate",		/*��ֹ�ɹ�����*/
		
      "dwReqProp",		/*���󸽼�����*/
		
		"dwPurpose",		/*��;(��UNIRESERVE����)*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*�Ʒ���Ϣ��ϸ*/
	static public string[] FDINFO = new string[]{
		
		"dwBeginTime",		/*��ʼʱ��*/
		
		"dwEndTime",		/*����ʱ��*/
		
		"dwFeeType",		/*�շ����(FEEDETAIL����)*/
		
		"dwUsablePayKind",		/*���ýɷѷ�ʽ(��UNIBILL����)*/
		
		"dwDefaultCheckStat",		/*CHECKINFO����Ĺ���Ա���״̬*/
		
		"dwUnitFee",		/*����*/
		
		"dwUnitTime",		/*��λʱ��*/
		
		"dwRoundOff",		/*����ֽ��(С�ڵ�λʱ��)*/
		
		"dwIgnoreTime",		/*���Ʒ�ʱ��*/
		
		"dwHolidayCoef",		/*����ϵ��*/
		
		"szPosInfo",		/*��һ��ͨ��Ӧ���̻���Ϣ*/
	
		"dwUseTime",		/*ʹ��ʱ��*/
		
		"dwFeeTime",		/*�Ʒ�ʱ��*/
		
		"dwCostMoney",		/*����*/
		
		"dwCostSubsidy",		/*ʹ�ò���*/
		
		"dwCostFreeTime",		/*ʹ�����ʱ��(��ʱ)*/
		 ""};

	/*�Ʒ���Ϣ*/
	static public string[] UNIACCTINFO = new string[]{
		
		"dwBeginTime",		/*��ʼʱ��*/
		
		"dwEndTime",		/*����ʱ��*/
		
		"dwUseTime",		/*ʹ��ʱ��*/
		
		"dwIdent",		/*��ݣ�0��ʾ�����ƣ�*/
		
		"dwDeptID",		/*���ţ�0��ʾ�����ƣ�*/
		
		"dwDevKind",		/*�豸���ͣ�0��ʾ�����ƣ�*/
		
		"dwGroupID",		/*ָ���û��飨0��ʾ�����ƣ�*/
		
		"dwPurpose",		/*��;*/
		
		"dwResvID",		/*ԤԼ��*/
		
		"dwAccNo",		/*�û��˺�*/
		
		"szPID",		/*ѧ����*/
	
		"szLogonName",		/*��¼��*/
	
		"szCardNo",		/*����*/
	
		"szTrueName",		/*����*/
	
		"szClassName",		/*�༶*/
	
		"szDeptName",		/*����*/
	
		"dwSex",		/*�Ա�*/
		
		"dwDevID",		/*�豸ID*/
		
		"dwLabID",		/*ʵ����ID*/
		
		"dwDevSN",		/*�豸���*/
		
		"szLabSN",		/*ʵ���ұ��*/
	
		"dwSID",		/*������ˮ��*/
		
		"dwBalance",		/*���*/
		
		"dwSubsidy",		/*����*/
		
		"dwFreeTime",		/*���ʱ��*/
		
		"dwUseQuota",		/*�����޶�*/
		
		"dwFeeSN",		/*���ʱ��*/
		
		"dwDeadLine",		/*���û������ʱ��*/
		
	"szFDInfo",		/*CUniTable[FDINFO]*/
	
		"szMemo",		/*��ע����չ�ã�*/
	 ""};

	/*��ȡ�豸���������*/
	static public string[] DEVMONITORREQ = new string[]{
		
		"dwMonitorID",		/*�����ID*/
		
		"dwMonitorType",		/*��������*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*�豸�����*/
	static public string[] DEVMONITOR = new string[]{
		
		"dwMonitorID",		/*�����ID*/
		
      "dwMonitorType",		/*��������*/
		
		"szMonitorName",		/*���������*/
	
		"szIP",		/*IP��ַ*/
	
		"dwPort",		/*�˿�*/
		
		"szMemo",		/*��ע*/
	 ""};

	/*��ȡ��������豸�Ķ�Ӧ��ϵ����*/
	static public string[] MONDEVREQ = new string[]{
		
		"dwMonitorID",		/*�����ID*/
		
		"dwMonitorType",		/*��������*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*��������豸�Ķ�Ӧ��ϵ*/
	static public string[] MONDEV = new string[]{
		
		"dwMonitorID",		/*�����ID*/
		
		"dwMonitorType",		/*��������*/
		
		"szMonitorName",		/*���������*/
	
		"dwLabID",		/*�����ID*/
		
		"dwDevID",		/*�豸ID*/
		
		"dwDevSN",		/*�豸���*/
		
		"szTagID",		/*RFID��ǩID*/
	
		"szDevName",		/*ʵ���豸����*/
	
		"szMemo",		/*��ע*/
	 ""};

	/*�豸���״̬*/
	static public string[] DEVMONITORSTAT = new string[]{
		
		"dwLabID",		/*�����ID*/
		
		"dwDevID",		/*�豸ID*/
		
		"dwMonitorStat",		/*���״̬(dwRunStat ���������˶��壩*/
		 ""};

	/*�豸ԤԼ��ϸ*/
	static public string[] DEVRESV = new string[]{
		
		"dwResvID",		/*ԤԼID*/
		
		"dwUsePurpose",		/*ͬUNIRESERVE��dwPurpose*/
		
		"dwResvBeginTime",		/*ԤԼ��ʼʱ��*/
		
		"dwResvEndTime",		/*ԤԼ����ʱ��*/
		
		"szResvMemberName",		/*ԤԼ��Ա��*/
	
		"dwTeacherID",		/*�ον�ʦID*/
		
		"szTeacherName",		/*�ον�ʦ����*/
	 ""};

	/*�豸ʹ����Ϣ*/
	static public string[] DEVUSEINFO = new string[]{
		
		"dwAccNo",		/*�û��˺�*/
		
		"szTrueName",		/*����*/
	
		"dwResvID",		/*ԤԼ��*/
		
		"dwBeginTime",		/*��ʼʱ��*/
		
	"FeeInfo",		/*CUniStruct[UNIACCTINFO]*/
	
		"dwUserUseStat",		/*�û�ʹ��״̬*/
		
		"dwLeaveTime",		/*��ʱ�뿪ʱ��*/
		
		"dwLeaveHoldSec",		/*��ʱ�뿪����ʱ��(�룩*/
		
		"dwQuotaRule",		/*���ƹ���(���ۼƣ����ۼƣ�����æ��(ȱʡ0))*/
		
		"dwQuotaTime",		/*����ʹ��ʱ��(ȱʡ-1)*/
		
		"dwLoanAdminID",		/*������ԱID*/
		
		"szLoanAdminName",		/*������Ա����*/
	
		"dwReturnAdminID",		/*�黹����ԱID*/
		
		"szReturnAdminName",		/*�黹����Ա����*/
	
		"dwTutorID",		/*��ʦ�˺�*/
		
		"szTutorName",		/*��ʦ����*/
	
		"szMemo",		/*��ע����չ�ã�*/
	 ""};

	/*�豸��Ϣ*/
	static public string[] UNIDEVICE = new string[]{
		
		"dwDevID",		/*�豸ID*/
		
		"dwDevSN",		/*�豸���*/
		
		"szAssertSN",		/*�û����ʲ����*/
	
		"szTagID",		/*RFID��ǩID*/
	
		"szOriginSN",		/*ԭ��ϵ�к�*/
	
		"szDevName",		/*ʵ���豸����*/
	
		"dwUnitPrice",		/*�豸����*/
		
		"dwPurchaseDate",		/*�ɹ����� YYYYMMDD*/
		
      "dwDevStat",		/*�豸״̬*/
		
      "dwCtrlMode",		/*���Ʒ�ʽ*/
		
      "dwRunStat",		/*�豸״̬*/
		
		"dwStatChgTime",		/*״̬�ı俪ʼʱ��(time������)*/
		
		"szStatInfo",		/*״̬����*/
	
		"dwClassID",		/*�豸�������*/
		
		"szClassSN",		/*�豸�����*/
	
		"szClassName",		/*�豸�������*/
	
		"dwClassKind",		/*���(��UNIDEVCLS��Kind����)*/
		
		"dwKindID",		/*�豸����*/
		
		"szKindName",		/*�豸����*/
	
		"szFuncCode",		/*�豸������;����*/
	
		"szModel",		/*�豸�ͺ�*/
	
		"szSpecification",		/*�豸���*/
	
      "dwProperty",		/*�豸���ԣ�ǰ16��ΪUNIDEVKIND����*/
		
		"dwMaxUsers",		/*���ͬʱʹ������*/
		
		"dwMinUsers",		/*����ͬʱʹ������*/
		
		"szOperaCert",		/*����֤��*/
	
		"szDevKindURL",		/*�豸������ϸ���ܵ�URL��ַ*/
	
		"szDevURL",		/*�豸��ϸ���ܵ�URL��ַ*/
	
		"dwManGroupID",		/*����Ա��ID*/
		
		"szManGroupName",		/*����Ա������*/
	
		"dwUseGroupID",		/*�豸ʹ����ID*/
		
		"dwOpenRuleSN",		/*���Ź�����*/
		
		"szPCName",		/*��¼�豸������*/
	
		"szIP",		/*�����IP��ַ*/
	
		"szMAC",		/*������ַ*/
	
		"szMemo",		/*˵����Ϣ*/
	
		"dwRoomID",		/*����ID*/
		
		"szRoomNo",		/*����ţ����ظ�,���ڹ����Ž�����ʽB-F-N,�������-¥���-�����ţ�*/
	
		"szRoomName",		/*��������*/
	
		"dwManMode",		/*���Ʒ�ʽ(UNIROOM�ж���)*/
		
		"szFloorNo",		/*����¥��*/
	
		"dwBuildingID",		/*¥��ID*/
		
		"szBuildingNo",		/*��¥���(*/
	
		"szBuildingName",		/*��¥����(*/
	
		"dwLabID",		/*ʵ����ID*/
		
		"szLabSN",		/*ʵ���ұ��*/
	
		"szLabName",		/*ʵ��������*/
	
		"dwDeptID",		/*����ID*/
		
		"szDeptName",		/*����*/
	
		"dwCampusID",		/*У��ID*/
		
		"szCampusName",		/*У������*/
	
		"dwCampusKind",		/*У�����ͣ���չ��*/
		
		"dwVisitTimes",		/*�������*/
		
		"dwUsePurpose",		/*ͬUniFee��dwPurpose*/
		
		"szExtInfo",		/*��չ��Ϣ*/
	
		"dwURLCtrl",		/*�������ģʽ*/
		
		"dwURLCtrlParam",		/*��������趨ֵ�����ݲ�ͬ�ļ��ģʽ���岻һ��)*/
		
		"dwURLEndTime",		/*��ֹʱ��*/
		
		"szURLCtrlName",		/*�����������*/
	
		"dwSWCtrl",		/*������ģʽ*/
		
		"dwSWCtrlParam",		/*�������趨ֵ�����ݲ�ͬ�ļ��ģʽ���岻һ��)*/
		
		"dwSWEndTime",		/*��ؽ���ʱ��*/
		
		"szSWCtrlName",		/*����������*/
	
		"dwAttendantID",		/*ֵ��Ա�˺�*/
		
		"szAttendantName",		/*ֵ��Ա����*/
	
		"szAttendantTel",		/*ֵ��Ա�绰*/
	
	"DevResv",		/*CUniTable[DEVRESV](dwReqProp��DEVREQ_NEEDDEVRESVʱ�ŷ���)*/
	
	"DevUse",		/*CUniTable[DEVUSEINFO](dwReqProp��DEVREQ_NEEDDEVUSEʱ�ŷ���)*/
	
	"DevSample",		/*CUniTable[SAMPLEINFO](dwReqProp��DEVREQ_NEEDSAMPLEʱ�ŷ���)*/
	 ""};

	/*��ȡ�豸���ñ�����*/
	static public string[] DEVCFGREQ = new string[]{
		
		"dwMainDevID",		/*���豸ID*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*�豸���ñ�*/
	static public string[] DEVCFG = new string[]{
		
		"dwMainDevID",		/*���豸ID*/
		
      "dwSubDevType",		/*�����豸���*/
		
		"dwDevID",		/*�豸ID*/
		
		"dwDevSN",		/*�豸���*/
		
		"szAssertSN",		/*�û����ʲ����*/
	
		"szOriginSN",		/*ԭ��ϵ�к�*/
	
		"szDevName",		/*ʵ���豸����*/
	
		"dwUnitPrice",		/*�豸����*/
		
		"dwPurchaseDate",		/*�ɹ����� YYYYMMDD*/
		
		"dwDevStat",		/*�豸״̬*/
		
		"dwClassID",		/*�豸�������*/
		
		"szClassSN",		/*�豸�����*/
	
		"szClassName",		/*�豸�������*/
	
		"dwClassKind",		/*���(��UNIDEVCLS��Kind����)*/
		
		"dwKindID",		/*�豸����*/
		
		"szKindName",		/*�豸����*/
	
		"szFuncCode",		/*�豸������;����*/
	
		"szModel",		/*�豸�ͺ�*/
	
		"szSpecification",		/*�豸���*/
	
		"szDevKindURL",		/*�豸������ϸ���ܵ�URL��ַ*/
	
		"szDevURL",		/*�豸��ϸ���ܵ�URL��ַ*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*���ܼ����λ״̬*/
	static public string[] SEATDETECTSTAT = new string[]{
		
		"szFloorNo",		/*����¥��*/
	
		"szRoomNo",		/*�����*/
	
		"dwDevSN",		/*��λ���*/
		
		"szTagID",		/*RFID��ǩID*/
	
		"dwMonitorStat",		/*���״̬(dwRunStat ���������˶��壩*/
		
		"dwChangeTime",		/*״̬�ı�ʱ��*/
		
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*����Ա�˹������豸ʹ��*/
	static public string[] DEVMANUSE = new string[]{
		
		"dwLabID",		/*ʵ����ID*/
		
		"dwDevID",		/*�豸ID*/
		
		"dwResvID",		/*ԤԼID*/
		
      "dwMode",		/*����ģʽ*/
		
		"szExtInfo",		/*��չ��Ϣ*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*��ȡ�豸������������*/
	static public string[] DEVCFGKINDREQ = new string[]{
		
		"dwMainDevID",		/*���豸ID*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*�豸��������*/
	static public string[] DEVCFGKIND = new string[]{
		
		"dwMainDevID",		/*���豸ID*/
		
		"dwSubDevType",		/*�����豸���*/
		
		"dwSubDevNum",		/*�����豸����*/
		
		"dwKindID",		/*�豸����*/
		
		"szKindName",		/*�豸����*/
	
		"szFuncCode",		/*�豸������;����*/
	
		"szModel",		/*�豸�ͺ�*/
	
		"szSpecification",		/*�豸���*/
	
		"szDevKindURL",		/*�豸������ϸ���ܵ�URL��ַ*/
	
		"dwClassID",		/*�豸�������*/
		
		"szClassSN",		/*�豸�����*/
	
		"szClassName",		/*�豸�������*/
	
		"dwClassKind",		/*���(��UNIDEVCLS��Kind����)*/
		
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*�����豸ֵ��Ա*/
	static public string[] DEVATTENDANT = new string[]{
		
		"dwDevID",		/*�豸ID*/
		
		"dwLabID",		/*ʵ����ID*/
		
		"dwAttendantID",		/*ֵ��Ա�˺�*/
		
		"szAttendantName",		/*ֵ��Ա����*/
	
		"szAttendantTel",		/*ֵ��Ա�绰*/
	 ""};

	/*�豸ʹ�÷ѵľ��ѷ����������*/
	static public string[] DEVFARREQ = new string[]{
		
		"dwDevID",		/*�豸ID*/
		 ""};

	/*�豸ʹ�÷ѵľ��ѷ������*/
	static public string[] DEVFAR = new string[]{
		
		"dwDevID",		/*�豸ID*/
		
		"dwFeeType",		/*�շ����(FEEDETAIL����)*/
		
		"dwTestRate",		/*�������Էѱ���*/
		
		"dwOpenFundRate",		/*���Ż������*/
		
		"dwServiceRate",		/*����ѱ���*/
		
		"szMemo",		/*��ע*/
	 ""};

	/*��ȡԤԼ�豸�б�*/
	static public string[] DEVFORRESVREQ = new string[]{
		
      "dwGetType",		/*��ʽ*/
		
		"szKey",		/*��ѯ������*/
	
		"szSubKey",		/*��ѯ��������DEVFORRESV_DEVIDʱ��LabID*/
	
		"dwKindID",		/*��������*/
		
		"dwClassID",		/*�����������ID*/
		
		"dwResvPurpose",		/*ԤԼ��;*/
		
		"dwBeginTime",		/*��ʼʱ��*/
		
		"dwEndTime",		/*����ʱ��*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*�豸��Ϣ*/
	static public string[] DEVFORRESV = new string[]{
		
		"dwDevID",		/*�豸ID*/
		
		"dwDevSN",		/*�豸���*/
		
		"szDevName",		/*ʵ���豸����*/
	
		"dwClassID",		/*�豸�������*/
		
		"szClassName",		/*�豸�������*/
	
		"dwKindID",		/*�豸����*/
		
		"szKindName",		/*�豸��������*/
	
		"dwMaxUsers",		/*���ͬʱʹ������*/
		
		"dwMinUsers",		/*����ͬʱʹ������*/
		
		"dwLabID",		/*ʵ����ID*/
		
		"szLabSN",		/*ʵ���ұ��*/
	
		"szLabName",		/*ʵ��������*/
	
		"dwRoomID",		/*����ID*/
		
		"szRoomNo",		/*����ţ����ظ�,���ڹ����Ž�����ʽB-F-N,�������-¥���-�����ţ�*/
	
		"szRoomName",		/*��������*/
	
		"szDevKindURL",		/*�豸��ϸ���ܵ�URL��ַ*/
	
		"szDevURL",		/*�豸URL��ַ*/
	
		"szExtInfo",		/*��չ��Ϣ*/
	
		"dwResvRate",		/*ԤԼ��(1-100)*/
		
		"dwResvRuleSN",		/*����ԤԼ����*/
		
		"dwOpenRuleSN",		/*��������ʱ���*/
		
		"dwFeeSN",		/*�����շѱ�׼*/
		
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*��ѯ�豸ԤԼ״̬����*/
	static public string[] DEVRESVSTATREQ = new string[]{
		
		"dwDevID",		/*�豸ID*/
		
		"szDevName",		/*ʵ���豸����*/
	
		"szLabIDs",		/*ʵ����ID,����ö��Ÿ���*/
	
		"szRoomIDs",		/*����ID,����ö��Ÿ���*/
	
		"szKindIDs",		/*��������,����ö��Ÿ���*/
	
		"szClassIDs",		/*�����������ID,����ö��Ÿ���*/
	
		"szDeptIDs",		/*ѧԺID,����ö��Ÿ���*/
	
		"szBuildingIDs",		/*¥��ID,����ö��Ÿ���*/
	
		"szCampusIDs",		/*У��ID,����ö��Ÿ���*/
	
		"dwResvPurpose",		/*ԤԼ��;*/
		
		"dwProperty",		/*�豸����(�ο�UNIDEVKIND����)*/
		
		"szDates",		/*��ѯ����,����ö��Ÿ���*/
	
		"dwClassKind",		/*���(��UNIDEVCLS��Kind����)*/
		
		"dwResvUsers",		/*ԤԼ����*/
		
		"dwExtRelatedID",		/*��չ����ID*/
		
      "dwReqProp",		/*���󸽼�����*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*�豸ԤԼ��Ϣ*/
	static public string[] DEVRESVTIME = new string[]{
		
		"dwResvID",		/*ԤԼID*/
		
		"dwPurpose",		/*ԤԼ��;*/
		
		"dwStatus",		/*ԤԼ״̬*/
		
		"dwPreDate",		/*ԤԼ��ʼ����*/
		
		"dwBegin",		/*��ʼʱ��(HHMM��YYYYMMDD)*/
		
		"dwEnd",		/*����ʱ��(HHMM��YYYYMMDD)*/
		
		"dwOwner",		/*ԤԼ��(������)*/
		
		"szOwnerName",		/*ԤԼ������*/
	
		"szMemberName",		/*��Ա����*/
	
		"szTestName",		/*ʵ������*/
	
		"dwSex",		/*ԤԼ���Ա�*/
		 ""};

	/*�豸ԤԼ״̬*/
	static public string[] DEVRESVSTAT = new string[]{
		
		"dwDevID",		/*�豸ID*/
		
		"dwDevSN",		/*�豸���*/
		
		"szDevName",		/*ʵ���豸����*/
	
		"dwDevStat",		/*�豸״̬*/
		
		"dwRunStat",		/*����״̬*/
		
		"dwClassID",		/*�豸�������*/
		
		"szClassName",		/*�豸�������*/
	
		"dwKindID",		/*�豸����*/
		
		"szKindName",		/*�豸��������*/
	
		"dwMaxUsers",		/*���ͬʱʹ������*/
		
		"dwMinUsers",		/*����ͬʱʹ������*/
		
		"dwProperty",		/*�豸����(�ο�UNIDEVKIND����)*/
		
		"dwLabID",		/*ʵ����ID*/
		
		"szLabSN",		/*ʵ���ұ��*/
	
		"szLabName",		/*ʵ��������*/
	
		"dwRoomID",		/*����ID*/
		
		"szRoomNo",		/*����ţ����ظ�,���ڹ����Ž�����ʽB-F-N,�������-¥���-�����ţ�*/
	
		"szRoomName",		/*��������*/
	
		"szFloorNo",		/*����¥��*/
	
		"dwBuildingID",		/*¥��ID*/
		
		"szBuildingNo",		/*��¥���(*/
	
		"szBuildingName",		/*��¥����(*/
	
		"dwDeptID",		/*����ID*/
		
		"szDeptName",		/*����*/
	
		"dwCampusID",		/*У��ID*/
		
		"szCampusName",		/*У������*/
	
		"dwCampusKind",		/*У�����ͣ���չ��*/
		
		"szDevKindURL",		/*�豸��ϸ���ܵ�URL��ַ*/
	
		"szDevURL",		/*�豸URL��ַ*/
	
		"szExtInfo",		/*��չ��Ϣ*/
	
      "dwOpenLimit",		/*�������Ƽ�GROUPOPENRULE����+���涨��*/
		
	"szRuleInfo",		/*CUniStruct[UNIRESVRULE]*/
	
	"szOpenInfo",		/*CUniTable[DAYOPENRULE]*/
	
	"szResvInfo",		/*CUniTable[DEVRESVTIME]*/
	 ""};

	/*��ѯ�豸����ԤԼ״̬����*/
	static public string[] DEVLONGRESVSTATREQ = new string[]{
		
		"dwDevID",		/*�豸ID*/
		
		"szDevName",		/*ʵ���豸����*/
	
		"szLabIDs",		/*ʵ����ID,����ö��Ÿ���*/
	
		"szRoomIDs",		/*����ID,����ö��Ÿ���*/
	
		"szKindIDs",		/*��������,����ö��Ÿ���*/
	
		"szClassIDs",		/*�����������ID,����ö��Ÿ���*/
	
		"szDeptIDs",		/*ѧԺID,����ö��Ÿ���*/
	
		"szBuildingIDs",		/*¥��ID,����ö��Ÿ���*/
	
		"szCampusIDs",		/*У��ID,����ö��Ÿ���*/
	
		"dwResvPurpose",		/*ԤԼ��;*/
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��ʼ����*/
		
		"dwClassKind",		/*���(��UNIDEVCLS��Kind����)*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*�豸����ԤԼ״̬*/
	static public string[] DEVLONGRESVSTAT = new string[]{
		
		"dwDevID",		/*�豸ID*/
		
		"dwDevSN",		/*�豸���*/
		
		"szDevName",		/*ʵ���豸����*/
	
		"dwClassID",		/*�豸�������*/
		
		"szClassName",		/*�豸�������*/
	
		"dwKindID",		/*�豸����*/
		
		"szKindName",		/*�豸��������*/
	
		"dwMaxUsers",		/*���ͬʱʹ������*/
		
		"dwMinUsers",		/*����ͬʱʹ������*/
		
		"dwLabID",		/*ʵ����ID*/
		
		"szLabSN",		/*ʵ���ұ��*/
	
		"szLabName",		/*ʵ��������*/
	
		"dwRoomID",		/*����ID*/
		
		"szRoomNo",		/*����ţ����ظ�,���ڹ����Ž�����ʽB-F-N,�������-¥���-�����ţ�*/
	
		"szRoomName",		/*��������*/
	
		"szFloorNo",		/*����¥��*/
	
		"dwBuildingID",		/*¥��ID*/
		
		"szBuildingNo",		/*��¥���(*/
	
		"szBuildingName",		/*��¥����(*/
	
		"dwDeptID",		/*����ID*/
		
		"szDeptName",		/*����*/
	
		"dwCampusID",		/*У��ID*/
		
		"szCampusName",		/*У������*/
	
		"dwCampusKind",		/*У�����ͣ���չ��*/
		
		"szDevKindURL",		/*�豸��ϸ���ܵ�URL��ַ*/
	
		"szDevURL",		/*�豸URL��ַ*/
	
		"szExtInfo",		/*��չ��Ϣ*/
	
	"szRuleInfo",		/*CUniStruct[UNIRESVRULE]*/
	
	"szResvInfo",		/*CUniTable[DEVRESVTIME]*/
	 ""};

	/*��ѯʵ����ԤԼ״̬����*/
	static public string[] LABRESVSTATREQ = new string[]{
		
      "dwGetType",		/*��ʽ*/
		
		"szGetKey",		/*��ѯ������*/
	
		"dwDate",		/*��ѯ����*/
		 ""};

	/*��ѧԤԼ��ϸ��Ϣ*/
	static public string[] TEACHINGRESVINFO = new string[]{
		
		"dwTestItemID",		/*ʵ����ĿID*/
		
		"dwTestCardID",		/*ʵ����Ŀ��ID*/
		
		"szTestName",		/*ʵ������*/
	
		"dwTestPlanID",		/*ʵ��ƻ�ID*/
		
		"szTestPlanName",		/*ʵ��ƻ�����*/
	
		"dwTeacherID",		/*��ʦ���ʺţ�*/
		
		"szTeacherName",		/*��ʦ����*/
	
		"dwCourseID",		/*�γ�ID*/
		
		"szCourseName",		/*�γ�����*/
	
		"dwGroupID",		/*�Ͽΰ༶*/
		
		"szGroupName",		/*�Ͽΰ༶����*/
	
		"dwTeachingTime",		/*��ѧʱ��(��ʽ��UNIRESERVE)*/
		
		"dwResvStat",		/*ԤԼ״̬(��ʽ��UNIRESERVE)*/
		 ""};

	/*ʵ����ԤԼ״̬*/
	static public string[] LABRESVSTAT = new string[]{
		
		"dwLabID",		/*ʵ����ID*/
		
		"szLabSN",		/*ʵ���ұ��*/
	
		"szLabName",		/*ʵ��������*/
	
		"dwDevNum",		/*�豸��*/
		
	"szResvInfo",		/*CUniTable[TEACHINGRESVINFO]*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*��ѯ����ԤԼ״̬����*/
	static public string[] ROOMRESVSTATREQ = new string[]{
		
		"szRoomName",		/*��������*/
	
		"szLabIDs",		/*ʵ����ID,����ö��Ÿ���*/
	
		"szRoomIDs",		/*����ID,����ö��Ÿ���*/
	
		"szKindIDs",		/*��������,����ö��Ÿ���*/
	
		"dwRoomProp",		/*��������*/
		
		"dwUnNeedRoomProp",		/*��������������*/
		
		"dwMinDevNum",		/*�����豸��*/
		
		"dwMaxDevNum",		/*����豸��*/
		
		"dwDate",		/*��ѯ����*/
		 ""};

	/*����ԤԼ״̬*/
	static public string[] ROOMRESVSTAT = new string[]{
		
		"dwRoomID",		/*����ID*/
		
		"szRoomNo",		/*������*/
	
		"szRoomName",		/*��������*/
	
		"dwRoomProp",		/*��������*/
		
		"dwDevNum",		/*�豸��*/
		
	"szResvInfo",		/*CUniTable[TEACHINGRESVINFO]*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*��ѯ�������ԤԼ״̬����*/
	static public string[] RGRESVSTATREQ = new string[]{
		
		"dwMinDevNum",		/*�����豸��*/
		
		"dwMaxDevNum",		/*����豸��*/
		
		"dwDate",		/*��ѯ����*/
		 ""};

	/*�������ԤԼ״̬*/
	static public string[] RGRESVSTAT = new string[]{
		
		"dwRGID",		/*����ID*/
		
		"szRGName",		/*�����������*/
	
		"dwRoomNum",		/*��Ϸ�����*/
		
		"dwDevNum",		/*�豸��*/
		
	"szResvInfo",		/*CUniTable[TEACHINGRESVINFO]*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*��ȡԤԼ�豸�б�(������)*/
	static public string[] DEVKINDFORRESVREQ = new string[]{
		
		"szKindName",		/*ʵ���豸����*/
	
		"szLabIDs",		/*ʵ����ID,����ö��Ÿ���*/
	
		"szRoomIDs",		/*����ID,����ö��Ÿ���*/
	
		"szKindIDs",		/*��������,����ö��Ÿ���*/
	
		"szClassIDs",		/*�����������ID,����ö��Ÿ���*/
	
		"szDeptIDs",		/*ѧԺID,����ö��Ÿ���*/
	
		"szBuildingIDs",		/*¥��ID,����ö��Ÿ���*/
	
		"szCampusIDs",		/*У��ID,����ö��Ÿ���*/
	
		"dwClassKind",		/*���(��UNIDEVCLS��Kind����)*/
		
		"dwResvPurpose",		/*ԤԼ��;*/
		
		"dwProperty",		/*�豸����(�ο�UNIDEVKIND����)*/
		
		"dwDate",		/*��ѯ����*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*��ȡ����ԤԼ�豸�б�(������)*/
	static public string[] DEVKINDFORLONGRESVREQ = new string[]{
		
		"szKindName",		/*ʵ���豸����*/
	
		"szLabIDs",		/*ʵ����ID,����ö��Ÿ���*/
	
		"szRoomIDs",		/*����ID,����ö��Ÿ���*/
	
		"szKindIDs",		/*��������,����ö��Ÿ���*/
	
		"szClassIDs",		/*�����������ID,����ö��Ÿ���*/
	
		"szDeptIDs",		/*ѧԺID,����ö��Ÿ���*/
	
		"szBuildingIDs",		/*¥��ID,����ö��Ÿ���*/
	
		"szCampusIDs",		/*У��ID,����ö��Ÿ���*/
	
		"dwClassKind",		/*���(��UNIDEVCLS��Kind����)*/
		
		"dwResvPurpose",		/*ԤԼ��;*/
		
		"dwProperty",		/*�豸����(�ο�UNIDEVKIND����)*/
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��ʼ����*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*�豸��Ϣ*/
	static public string[] DEVKINDFORRESV = new string[]{
		
		"dwKindID",		/*�豸����*/
		
		"szKindName",		/*ʵ���豸����*/
	
		"dwClassID",		/*�豸�������*/
		
		"szClassName",		/*�豸�������*/
	
		"szFuncCode",		/*�豸������;����*/
	
		"szModel",		/*�豸�ͺ�*/
	
		"szSpecification",		/*�豸���*/
	
		"dwProperty",		/*�豸����*/
		
		"dwMaxUsers",		/*���ͬʱʹ������*/
		
		"dwMinUsers",		/*����ͬʱʹ������*/
		
		"dwTotalNum",		/*�豸����*/
		
		"szOperaCert",		/*����֤��*/
	
		"szDevKindURL",		/*�豸��ϸ���ܵ�URL��ַ*/
	
		"dwLabID",		/*ʵ����ID*/
		
		"szLabSN",		/*ʵ���ұ��*/
	
		"szLabName",		/*ʵ��������*/
	
		"dwRoomID",		/*����ID*/
		
		"szRoomNo",		/*����ţ����ظ�,���ڹ����Ž�����ʽB-F-N,�������-¥���-�����ţ�*/
	
		"szRoomName",		/*��������*/
	
		"szFloorNo",		/*����¥��*/
	
		"dwBuildingID",		/*¥��ID*/
		
		"szBuildingNo",		/*��¥���(*/
	
		"szBuildingName",		/*��¥����(*/
	
		"dwDeptID",		/*����ID*/
		
		"szDeptName",		/*����*/
	
		"dwCampusID",		/*У��ID*/
		
		"szCampusName",		/*У������*/
	
		"dwCampusKind",		/*У�����ͣ���չ��*/
		
		"szUsableNumArray",		/* ����ԤԼÿ�ֽڴ���0-24���Ӧ�ķ��ӵĿ����豸��(����1440),
            ����ԤԼ��ʾÿ��Ŀ����豸������Ϊ��ѯ������=A��ʾ����9̨�����豸,U��ʾ������ */
	
		"dwOpenLimit",		/*�������Ƽ�GROUPOPENRULE����+DEVRESVSTAT����*/
		
	"szRuleInfo",		/*CUniStruct[UNIRESVRULE]*/
	
	"szOpenInfo",		/*CUniTable[DAYOPENRULE]*/
	
	"szFeeInfo",		/*CUniStruct[UNIFEE]*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*��ȡԤԼ�豸�б�(������)*/
	static public string[] ROOMFORRESVREQ = new string[]{
		
		"szRoomName",		/*ʵ���豸����*/
	
		"szFloorNo",		/*����¥��*/
	
		"szRoomIDs",		/*����ID,����ö��Ÿ���*/
	
		"dwKindID",		/*��������*/
		
		"szBuildingIDs",		/*¥��ID,����ö��Ÿ���*/
	
		"szCampusIDs",		/*У��ID,����ö��Ÿ���*/
	
		"dwClassKind",		/*���(��UNIDEVCLS��Kind����)*/
		
		"dwResvPurpose",		/*ԤԼ��;*/
		
		"dwDate",		/*��ѯ����*/
		
		"dwBeginTime",		/*ԤԼ��ʼʱ��*/
		
		"dwEndTime",		/*ԤԼ����ʱ��*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*����ԤԼ��Ϣ*/
	static public string[] ROOMFORRESV = new string[]{
		
		"dwRoomID",		/*����ID*/
		
		"szRoomName",		/*��������*/
	
		"szRoomNo",		/*�����*/
	
		"szFloorNo",		/*����¥��*/
	
		"dwOpenBegin",		/*��ʼʱ��(HHMM)*/
		
		"dwOpenEnd",		/*����ʱ��(HHMM)*/
		
		"dwTotalNum",		/*�豸����*/
		
		"dwUsableNum",		/*�����豸��*/
		
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*��ȡԤԼ�����豸�����*/
	static public string[] RESVUSABLEDEVREQ = new string[]{
		
		"dwResvID",		/*ԤԼID*/
		 ""};

	/*����豸��ϸ*/
	static public string[] RESVUSABLEDEV = new string[]{
		
		"dwLabID",		/*ʵ����ID*/
		
		"dwDevID",		/*�豸ID*/
		
		"dwDevSN",		/*�豸SN*/
		
		"dwDevKind",		/*�豸����*/
		
		"szDevName",		/*�豸��*/
	
		"szKindName",		/*�豸�����*/
	
		"dwRoomID",		/*����ID*/
		
		"szRoomNo",		/*������*/
	
		"szRoomName",		/*��������*/
	
		"szExtInfo",		/*��չ��Ϣ*/
	 ""};

	/*�ͻ���ע�ᵽ������*/
	static public string[] DEVREGISTREQ = new string[]{
		
		"dwStaSN",		/*վ����*/
		
		"szCltVersion",		/*�ͻ��˰汾*/
	
		"szPCName",		/*��������*/
	
		"szIP",		/*IP��ַ*/
	
		"szMAC",		/*������ַ*/
	
	"szCfgInfo",		/*������ϢCUniTable[DEVICECONFIG]*/
	 ""};

	/*�������Կͻ���ע�����Ӧ*/
	static public string[] DEVREGISTRES = new string[]{
		
		"dwSessionID",		/*�����������SessionID*/
		
	"SrvVer",		/*UNIVERSION �ṹ*/
	
		"szCurTime",		/*������ʱ�� YYYY-MM-DD HH:MM:SS*/
	
		"dwDevSN",		/*���豸���*/
		
		"dwDevID",		/*���豸�ɣĺ�*/
		
		"dwLabID",		/*��ʵ���ңɣĺ�*/
		
		"dwFunc",		/*ʹ�ù���ģ��*/
		
      "dwParam",		/*��������*/
		
		"dwDevStat",		/*�豸״̬*/
		
		"dwRunStat",		/*����״̬*/
		
		"dwPasswdCode",		/*ж����������*/
		
		"szLabName",		/*ʵ��������*/
	
		"szDisplayInfo",		/*��¼������ʾ��Ϣ*/
	
		"szCastParam",		/*��Ļ�㲥����*/
	
	"DevInfo",		/*UNIDEVICE �ṹ*/
	 ""};

	/*�ͻ��˵�¼����*/
	static public string[] DEVLOGONREQ = new string[]{
		
		"dwDevID",		/*�ͻ����豸��ID��*/
		
		"dwLabID",		/*ʵ���ҵ�ID��*/
		
      "dwLogonType",		/*��¼���*/
		
		"szLogonName",		/*��¼��*/
	
		"szPasswd",		/*�û�����*/
	
		"szMemo",		/*��ע����չ�ã�*/
	 ""};

	/*�ͻ��˵�¼�������Ӧ*/
	static public string[] DEVLOGONRES = new string[]{
		
	"szAccInfo",		/*ʹ������Ϣ(UNIACCOUNT�ṹ)*/
	
		"dwPurpose",		/*��;*/
		
		"szDeclareInfo",		/*����ϵͳ��������Ϣ*/
	
	"ResvInfo",		/*ԤԼ��Ϣ*/
	 ""};

	/*�ͻ��˲�ѯ��ǰ��Ϣ*/
	static public string[] DEVQUERYREQ = new string[]{
		
		"dwDevID",		/*�ͻ����豸��ID��*/
		
		"dwLabID",		/*ʵ���ҵ�ID��*/
		
		"dwAccNo",		/*�ʺ�*/
		
		"szMemo",		/*��ע����չ�ã�*/
	 ""};

	/*�ͻ���ע��ʱ�û�ѡ��ļƷ���Ϣ*/
	static public string[] USERFEECHECK = new string[]{
		
		"dwResvID",		/*ԤԼ��*/
		
		"dwAccNo",		/*�û��˺�*/
		
		"dwFeeMode",		/*�շѷ�ʽ(������UNIRESVRULE)*/
		
	"ResvSample",		/*CUniTable[RESVSAMPLE]*/
	
	"FeeInfo",		/*CUniStruct[UNIACCTINFO]*/
	
		"szMemo",		/*��ע����չ�ã�*/
	 ""};

	/*�ͻ���ע������*/
	static public string[] DEVLOGOUTREQ = new string[]{
		
		"dwDevID",		/*�ͻ����豸��ID��*/
		
		"dwLabID",		/*ʵ���ҵ�ID��*/
		
		"dwAccNo",		/*�ʺ�*/
		
      "dwParam",		/*�˳��������*/
		
	"FeeCheck",		/*������Ϣ(ֻ��֧�� ע��ʱѡ���շ�ģʽ���豸��Ч)*/
	 ""};

	/*�ͻ���ע������*/
	static public string[] DEVLOGOUTRES = new string[]{
		
	"szAccInfo",		/*ʹ������Ϣ(UNIACCOUNT�ṹ)*/
	
      "dwParam",		/*�˳�����*/
		
		"szMemo",		/*��ע����չ�ã�*/
	 ""};

	/*�ͻ������������ʱ��������*/
	static public string[] DEVHANDSHAKEREQ = new string[]{
		
		"dwDevID",		/*�ͻ����豸��ID��*/
		
		"dwLabID",		/*ʵ���ҵ�ID��*/
		
		"dwAccNo",		/*�ʺ�*/
		
		"szDevChgInfo",		/*�豸��Ӳ�������Ϣ*/
	
		"szMemo",		/*��ע����չ�ã�*/
	 ""};

	/*�������Կͻ��˶�ʱ���ֵ���Ӧ*/
	static public string[] DEVHANDSHAKERES = new string[]{
		
		"dwFunc",		/*ʹ�ù���ģ��*/
		
		"dwDevStat",		/*�豸״̬*/
		
		"dwRunStat",		/*����״̬*/
		
		"dwUndoCmd",		/*δ���������*/
		
		"dwLockWaitTime",		/*�ȴ������ͻ���ʱ��*/
		
		"szMemo",		/*��ע*/
	 ""};

	/*�ͻ������������ʱ��������*/
	static public string[] CLTCHGPWINFO = new string[]{
		
		"dwDevID",		/*�ͻ����豸��ID��*/
		
		"dwLabID",		/*ʵ���ҵ�ID��*/
		
		"dwAccNo",		/*�ʺ�*/
		
		"szOldPw",		/*������*/
	
		"szNewPw",		/*������*/
	
		"szMemo",		/*��ע����չ�ã�*/
	 ""};

	/*�����豸����*/
	static public string[] DEVCTRLINFO = new string[]{
		
		"dwLabID",		/*ʵ����ID��*/
		
		"dwDevID",		/*�ͻ����豸��ID��*/
		
      "dwCmd",		/*��������*/
		
		"szParam",		/*���Ʋ���*/
	
		"szMemo",		/*��ע*/
	 ""};

	/*���Ʒ�������*/
	static public string[] ROOMCTRLINFO = new string[]{
		
		"dwCtrlSN",		/*���������*/
		
		"dwRoomID",		/*����ID��*/
		
		"dwCmd",		/*��������,�ο�DEVCTRLINFO����*/
		
		"szParam",		/*���Ʋ���*/
	
		"szMemo",		/*��ע*/
	 ""};

	/*�û��ɽ��뷿������*/
	static public string[] PERMITROOMREQ = new string[]{
		
		"dwAccNo",		/*�ʺ�*/
		 ""};

	/*�û��ɽ��뷿����Ϣ*/
	static public string[] PERMITROOMINFO = new string[]{
		
      "dwRoomKind",		/*��������*/
		
      "dwPermitMode",		/*����ģʽ*/
		
		"dwRoomID",		/*����ID*/
		
		"szRoomNo",		/*������*/
	
		"szRoomName",		/*��������*/
	 ""};

	/*��ȡ���������Ϣ�����*/
	static public string[] ROOMCTRLREQ = new string[]{
		
		"szRoomNo",		/*�����*/
	
		"dwDCSKind",		/*�Ž�����������*/
		 ""};

	/*�����豸����*/
	static public string[] CTRLREQ = new string[]{
		
		"dwLabID",		/*ʵ����ID��*/
		
		"dwDevID",		/*�ͻ����豸��ID��(Ϊ�ձ�����������ʵ����)*/
		
		"dwCtrl",		/*��ǰ���ģʽ*/
		
		"dwCtrlParam",		/*����趨ֵ�����ݲ�ͬ�ļ��ģʽ���岻һ����*/
		
		"dwEndTime",		/*��ֹʱ��*/
		
		"szCtrlName",		/*�����*/
	 ""};

	/*����豸��ϸ*/
	static public string[] LOANDEV = new string[]{
		
		"dwDevID",		/*�豸ID*/
		
		"dwDevSN",		/*�豸���*/
		
		"dwDevKind",		/*�豸����*/
		
		"szDevName",		/*�豸��*/
	
		"szRoomNo",		/*���ڷ���*/
	
		"dwDevClsKind",		/*�豸������*/
		 ""};

	/*��ȡ���г�������*/
	static public string[] RUNAPPREQ = new string[]{
		
		"dwLabID",		/*ʵ����ID*/
		
		"dwDevID",		/*�豸ID*/
		
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*���г���*/
	static public string[] RUNAPP = new string[]{
		
		"dwRunNum",		/*��ǰ���������ϼ�*/
		
		"dwProcID",		/*����ID*/
		
		"dwProperty",		/*��������*/
		
		"szProductName",		/*��Ʒ����*/
	
		"szExeName",		/*Exe�ļ���*/
	
		"szSWVersion",		/*����汾*/
	
		"szDispProductName",		/*��ʾ��������*/
	
		"szDispSWName",		/*��ʾ��Ʒ����*/
	
		"szDispSWCompany",		/*��ʾ��˾����*/
	
		"szInstName",		/*��װ����*/
	
		"szInstPath",		/*��װ·��*/
	
		"szIcon",		/*ͼ��*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*�ϴ����������Ϣ����*/
	static public string[] SWUPLOADEND = new string[]{
		
		"dwDevID",		/*�ͻ����豸��ID��*/
		
		"dwLabID",		/*ʵ���ҵ�ID��*/
		
		"dwTotalNum",		/*�ܼ�¼��*/
		
		"dwCollectSecond",		/*�ռ���ʱ(����)*/
		
		"dwUploadSecond",		/*�ϴ���ʱ(����)*/
		
		"szMemo",		/*��ע����չ�ã�*/
	 ""};

	/*�豸�������*/
	static public string[] DEVLOANREQ = new string[]{
		
		"dwLabID",		/*ʵ����ID*/
		
		"dwResvID",		/*ԤԼ��*/
		
		"dwLender",		/*�����*/
		
		"szLenderName",		/*���������*/
	
		"dwBeginTime",		/*��迪ʼʱ��*/
		
		"dwEndTime",		/*������ʱ��*/
		
	"szLoanDevs",		/*����豸��ϸ��(CUniTable<LOANDEV>)*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*�豸�黹����*/
	static public string[] DEVRETURNREQ = new string[]{
		
		"dwResvID",		/*ԤԼ��*/
		
		"dwLender",		/*�����*/
		
		"szLenderName",		/*���������*/
	
		"dwLabID",		/*ʵ����ID*/
		
		"dwDevID",		/*�豸ID*/
		
		"dwCheckStat",		/*�豸״̬*/
		
		"dwCompensation",		/*�⳥���*/
		
		"dwPunishScore",		/*���ÿ۷�*/
		
		"szDamageInfo",		/*��˵��*/
	
		"szExtInfo",		/*�豸������*/
	 ""};

	/**/
	static public string[] DEVDAMAGERECREQ = new string[]{
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
		"dwSID",		/*SID*/
		
		"dwDevID",		/*�豸ID*/
		
		"szLabIDs",		/*ʵ����ID,����ö��Ÿ���*/
	
		"szRoomIDs",		/*����ID,����ö��Ÿ���*/
	
		"szKindIDs",		/*��������,����ö��Ÿ���*/
	
		"szClassIDs",		/*�����������ID,����ö��Ÿ���*/
	
		"dwProperty",		/*�豸����*/
		
		"dwClassKind",		/*���(��UNIDEVCLS��Kind����)*/
		
		"dwUnitPrice",		/*���������۸����*/
		
		"dwStatus",		/*ά��״̬*/
		
		"dwManID",		/*������ID*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/**/
	static public string[] DEVDAMAGEREC = new string[]{
		
		"dwSID",		/*SID*/
		
		"dwLabID",		/*ʵ����ID*/
		
		"szLabName",		/*ʵ��������*/
	
		"dwKindID",		/*�豸����*/
		
		"szKindName",		/*�豸������*/
	
		"dwDevID",		/*�豸ID*/
		
		"dwDevSN",		/*�豸���*/
		
		"szDevName",		/*�豸����*/
	
		"szAssertSN",		/*�û����ʲ����*/
	
		"dwUnitPrice",		/*�豸����*/
		
		"dwPurchaseDate",		/*�ɹ�����*/
		
      "dwStatus",		/*��UNIBILL����+���¶���*/
		
		"dwDamageDate",		/*������*/
		
		"dwDamageTime",		/*��ʱ��*/
		
		"szDamageInfo",		/*��˵��*/
	
		"dwRepareDate",		/*ά������*/
		
		"dwRepareTime",		/*ά��ʱ��*/
		
		"szRepareInfo",		/*ά��˵��*/
	
		"dwRepareCost",		/*ά�޷���*/
		
		"szFundsNo1",		/*���ѿ����1*/
	
		"dwPay1",		/*���ѿ�1֧��*/
		
		"szFundsNo2",		/*���ѿ����2*/
	
		"dwPay2",		/*���ѿ�2֧��*/
		
		"szRepareCom",		/*ά�޵�λ*/
	
		"szRepareComTel",		/*ά�޵�λ��ϵ��ʽ*/
	
		"dwManID",		/*������ID*/
		
		"szManName",		/*����������*/
	
		"dwAccNo",		/*�˺�*/
		
		"szTrueName",		/*��ʵ����*/
	
		"dwCompensation",		/*�⳥���*/
		
		"dwPunishScore",		/*��������*/
		
		"szMemo",		/*˵��*/
	 ""};

	/**/
	static public string[] DEVOPENRULEREQ = new string[]{
		
		"dwRuleSN",		/*���*/
		
		"szRuleName",		/*����*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*ÿ�տ���ʱ���*/
	static public string[] DAYOPENRULE = new string[]{
		
		"dwDate",		/*����*/
		
		"dwOpenLimit",		/*�������Ƽ�GROUPOPENRULE����+DEVRESVSTAT����*/
		
		"dwOpenPurpose",		/*������;*/
		
		"dwBegin",		/*��ʼʱ��(HHMM)*/
		
		"dwEnd",		/*����ʱ��(HHMM)*/
		
		"szFixedTime",		/*ԤԼ�̶�ʱ���(HHMM)������ö��Ÿ���*/
	 ""};

	/*ָ���û��鿪��ʱ���*/
	static public string[] PERIODOPENRULEREQ = new string[]{
		
		"dwRuleSN",		/*���*/
		
		"dwGroupID",		/*��ID*/
		
		"dwStartDay",		/*��ʼ����(��һ������ ��0-6���ڼ�����8���ƶ�������YYYYMMDD)*/
		 ""};

	/*ʱ���ڼ俪�Ź���*/
	static public string[] PERIODOPENRULE = new string[]{
		
		"dwStartDay",		/*��ʼ����(��һ������ ��0-6���ڼ�����8���ƶ�������YYYYMMDD)*/
		
		"dwEndDay",		/*��������(ͬ dwStartDay����)*/
		
	"DayOpenRule",		/*����ʱ���(CUniTable[DAYOPENRULE])*/
	
		"szMemo",		/*����˵��*/
	 ""};

	/*ָ���û��鿪��ʱ���*/
	static public string[] GROUPOPENRULEREQ = new string[]{
		
		"dwRuleSN",		/*���*/
		
		"dwGroupID",		/*��ID*/
		 ""};

	/*ָ���û��鿪��ʱ���*/
	static public string[] GROUPOPENRULE = new string[]{
		
	"szGroup",		/*����Ϣ(CUniStruct[UNIGROUP])*/
	
		"dwPriority",		/*���ȼ�(���ִ�������ȼ���)*/
		
      "dwOpenLimit",		/*��������*/
		
	"PeriodOpenRule",		/*ʱ���ڼ俪�Ź���(CUniTable[PERIODOPENRULE])*/
	 ""};

	/*ָ���û��鿪��ʱ���*/
	static public string[] CHANGEGROUPOPENRULE = new string[]{
		
		"dwRuleSN",		/*���*/
		
		"dwOldGroupID",		/*ԭ��ID*/
		
		"dwGroupID",		/*��ID*/
		
		"dwPriority",		/*���ȼ�(���ִ�������ȼ���)*/
		
		"dwOpenLimit",		/*��������(GROUPOPENRULE)*/
		
	"PeriodOpenRule",		/*ʱ���ڼ俪�Ź���(CUniTable[PERIODOPENRULE])*/
	 ""};

	/*ʱ���ڼ俪�Ź���*/
	static public string[] CHANGEPERIODOPENRULE = new string[]{
		
		"dwRuleSN",		/*���*/
		
		"dwGroupID",		/*��ID*/
		
		"dwOldStartDay",		/*ԭ��ʼ����(��һ������ ��0-6���ڼ�����8���ƶ�������YYYYMMDD)*/
		
		"dwStartDay",		/*��ʼ����(��һ������ ��0-6���ڼ�����8���ƶ�������YYYYMMDD)*/
		
		"dwEndDay",		/*��������(ͬ dwStartDay����)*/
		
	"DayOpenRule",		/*����ʱ���(CUniTable[DAYOPENRULE])*/
	
		"szMemo",		/*����˵��*/
	 ""};

	/*�豸����ʱ���*/
	static public string[] DEVOPENRULE = new string[]{
		
		"dwRuleSN",		/*���*/
		
		"szRuleName",		/*����*/
	
	"GroupOpenRule",		/*ָ���û��鿪��ʱ���(CUniTable[GROUPOPENRULE])*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*��ǰ�豸ͳ����Ϣ*/
	static public string[] CURDEVSTAT = new string[]{
		
		"dwChgNum",		/*��Ϣ�����ı��ͳ����Ŀ��*/
		
	"TeachingDevStat",		/*�豸��Ϣͳ��*/
	 ""};

	/*��ѧ�豸ͳ��*/
	static public string[] TEACHINGDEVSTAT = new string[]{
		
		"dwTotalNum",		/*���豸��*/
		
		"dwUseNum",		/*����ʹ���豸��*/
		
		"dwIdleNum",		/*�����豸��*/
		 ""};

	/*��ȡ��ѧ���豸���ڴ�ͳ��*/
	static public string[] DEVFORTREQ = new string[]{
		
		"dwDate",		/*����*/
		
		"szRoomIDs",		/*����ID,����ö��Ÿ���*/
	 ""};

	/*�豸�ڴν�ѧʹ����Ϣ*/
	static public string[] DEVSECINFO = new string[]{
		
		"dwSecIndex",		/*�ڴα��(1��ʼ��)*/
		
		"szSecName",		/*�ڴ�����*/
	
		"dwResvDevs",		/*ԤԼ������*/
		
		"dwUseDevs",		/*ʵ���û���*/
		
		"dwResvUsers",		/*�Ͽ�������*/
		
		"dwRealUsers",		/*ʵ�ʵ�������*/
		 ""};

	/*��ȡ��ѧ���豸*/
	static public string[] TEACHINGDEVREQ = new string[]{
		
		"dwDevID",		/*�豸ID*/
		
		"szDevName",		/*ʵ���豸����*/
	
		"dwDevSN",		/*�豸���*/
		
		"szLabIDs",		/*ʵ����ID,����ö��Ÿ���*/
	
		"szRoomIDs",		/*����ID,����ö��Ÿ���*/
	
		"szResvIDs",		/*ԤԼID,����ö��Ÿ���*/
	
		"szKindIDs",		/*��������,����ö��Ÿ���*/
	
		"szClassIDs",		/*�����������ID,����ö��Ÿ���*/
	
		"dwDevStat",		/*�豸״̬*/
		
		"dwRunStat",		/*�豸״̬*/
		
		"dwUsePurpose",		/*��UNIDEVICE����*/
		
		"dwCtrlMode",		/*���Ʒ�ʽ*/
		
		"dwProperty",		/*�豸����*/
		
		"dwClassKind",		/*���(��UNIDEVCLS��Kind����)*/
		
		"dwManGroupID",		/*����Ա��ID*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*��ѧ�豸��Ϣ*/
	static public string[] TEACHINGDEV = new string[]{
		
		"dwDevID",		/*�豸ID*/
		
		"dwDevSN",		/*�豸���*/
		
		"szDevName",		/*ʵ���豸����*/
	
		"dwDevStat",		/*�豸״̬*/
		
		"dwCtrlMode",		/*���Ʒ�ʽ*/
		
		"dwRunStat",		/*�豸״̬*/
		
		"szKindName",		/*�豸����*/
	
		"szFuncCode",		/*�豸������;����*/
	
		"szModel",		/*�豸�ͺ�*/
	
		"szSpecification",		/*�豸���*/
	
		"dwProperty",		/*�豸���ԣ�ǰ16��ΪUNIDEVKIND����*/
		
		"dwMaxUsers",		/*���ͬʱʹ������*/
		
		"dwMinUsers",		/*����ͬʱʹ������*/
		
		"dwCurUsers",		/*��ǰʹ������*/
		
		"dwLabID",		/*ʵ����ID*/
		
		"szLabSN",		/*ʵ���ұ��*/
	
		"szLabName",		/*ʵ��������*/
	
		"dwRoomID",		/*����ID*/
		
		"szRoomNo",		/*����ţ����ظ�,���ڹ����Ž�����ʽB-F-N,�������-¥���-�����ţ�*/
	
		"szRoomName",		/*��������*/
	
		"dwUsePurpose",		/*ͬUniFee��dwPurpose*/
		
		"dwCurAccNo",		/*��ǰ�û��˺�*/
		
		"szCurTrueName",		/*��ǰ�û�����*/
	
		"dwResvID",		/*ԤԼID*/
		
		"dwTestItemID",		/*ʵ����ĿID*/
		
		"szTestName",		/*ʵ������*/
	
		"dwTestPlanID",		/*ʵ��ƻ�ID*/
		
		"szTestPlanName",		/*ʵ��ƻ�����*/
	
		"dwTeacherID",		/*��ʦ���ʺţ�*/
		
		"szTeacherName",		/*��ʦ����*/
	
		"dwCourseID",		/*�γ�ID*/
		
		"szCourseName",		/*�γ�����*/
	
		"dwGroupID",		/*�Ͽΰ༶*/
		
		"szGroupName",		/*�Ͽΰ༶����*/
	
		"dwTeachingTime",		/*��ѧʱ��(WWDSSEE)�ڼ��ܣ�WW�����ڼ�(D),��ʼ�ڴΣ�SS�������ڴΣ�EE)*/
		 ""};

	/*��ȡ�񽱼�¼*/
	static public string[] REWARDRECREQ = new string[]{
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
		"dwRewardID",		/*��ID*/
		
		"dwRTID",		/*����ʵ����ĿID*/
		
		"dwHolderID",		/*�����ˣ��ʺţ�*/
		
		"dwLeaderID",		/*������ID*/
		
		"dwOpID",		/*¼��ԱID*/
		
		"dwRewardType",		/*�񽱷���*/
		
		"dwRewardKind",		/*������*/
		
		"dwRewardLevel",		/*�񽱼���*/
		
		"dwDevID",		/*�豸ID*/
		
		"szLabIDs",		/*ʵ����ID,����ö��Ÿ���*/
	
		"szRoomIDs",		/*����ID,����ö��Ÿ���*/
	
		"szKindIDs",		/*��������,����ö��Ÿ���*/
	
		"szClassIDs",		/*�����������ID,����ö��Ÿ���*/
	
		"dwProperty",		/*�豸����*/
		
		"dwClassKind",		/*���(��UNIDEVCLS��Kind����)*/
		
		"dwUnitPrice",		/*���������۸����*/
		
      "dwReqProp",		/*���󸽼�����*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*��ʹ���豸*/
	static public string[] REWARDUSEDEV = new string[]{
		
		"dwRewardID",		/*��ID*/
		
		"dwDevID",		/*�豸ID*/
		
		"dwKindID",		/*�豸����*/
		
		"dwDevSN",		/*�豸���*/
		
		"szDevName",		/*�豸����*/
	
		"dwRoomID",		/*����ID*/
		
		"szRoomName",		/*��������*/
	
		"dwLabID",		/*ʵ����ID*/
		
		"szLabName",		/*ʵ��������*/
	
		"szAttendantName",		/*ֵ��Ա����*/
	
		"dwStatus",		/*�豸����Աȷ��״̬*/
		
		"dwTestTimes",		/*ʵ�����*/
		
		"dwTestHour",		/*ʵ��ѧʱ��*/
		
		"dwRelyRate",		/*������(�ٷֱ�)����չ*/
		
		"szMemo",		/*��ע*/
	 ""};

	/*�񽱼�¼*/
	static public string[] REWARDREC = new string[]{
		
		"dwRewardID",		/*��ID*/
		
		"dwRTID",		/*����ʵ����ĿID*/
		
		"szRTName",		/*����ʵ������*/
	
		"dwHolderID",		/*�����ˣ��ʺţ�*/
		
		"szHolderName",		/*����������*/
	
		"dwLeaderID",		/*������ID*/
		
		"szLeaderName",		/*����������*/
	
		"szMemberNames",		/*����Ա���������Ÿ�����*/
	
		"dwRewardDate",		/*������*/
		
		"dwOpDate",		/*¼������*/
		
		"dwOpID",		/*¼��ԱID*/
		
		"szOpName",		/*¼��Ա����*/
	
      "dwRewardType",		/*�񽱷���*/
		
      "dwRewardKind",		/*������*/
		
		"szRewardName",		/*��������*/
	
      "dwRewardLevel",		/*�񽱼���*/
		
		"szAuthOrg",		/*��֤����*/
	
		"szCertID",		/*֤����*/
	
		"szExtInfo",		/*��չ��Ϣ*/
	
	"UseDev",		/*CUniTable[REWARDUSEDEV]*/
	
		"szMemo",		/*��ע*/
	 ""};

	/*��ȡ���ü�¼*/
	static public string[] COSTRECREQ = new string[]{
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
		"dwCostType",		/*��������*/
		
		"szLabIDs",		/*ʵ����ID,����ö��Ÿ���*/
	
		"szRoomIDs",		/*����ID,����ö��Ÿ���*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*���ü�¼*/
	static public string[] COSTREC = new string[]{
		
		"dwSID",		/*��ˮ��*/
		
      "dwCostType",		/*��������*/
		
      "dwPurpose",		/*��;*/
		
		"dwRoomID",		/*����ID*/
		
		"szRoomName",		/*��������*/
	
		"dwSubID",		/*�����豸ID����չ��*/
		
		"dwCost",		/*���ã�Ԫ��*/
		
		"szExtInfo",		/*����������Ϣ*/
	
		"dwCostDate",		/*��������*/
		
		"dwOpTime",		/*¼��ʱ��*/
		
		"dwOpID",		/*¼�����ԱID*/
		
		"szOpName",		/*¼�����Ա����*/
	
		"szMemo",		/*��ע*/
	 ""};

}
	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/
public partial class Struct_ST
{

	/*��ȡ�Ž��������������*/
	static public string[] DCSREQ = new string[]{
		
      "dwGetType",		/*��ʽ*/
		
		"szGetKey",		/*����ֵ*/
	
		"dwDCSKind",		/*�Ž�����������*/
		 ""};

	/**/
	static public string[] UNIDCS = new string[]{
		
		"dwSN",		/*�Ž����������*/
		
      "dwDCSKind",		/*�Ž�����������*/
		
		"szName",		/*�Ž�����������*/
	
		"szRoomNo",		/*�Ž����ڷ����*/
	
		"szIP",		/*�Ž�������IP��ַ*/
	
      "dwStatus",		/*�Ž�������״̬*/
		
		"dwStaSN",		/*����վ����*/
		
		"szStaName",		/*վ������*/
	
		"dwStatChgTime",		/*״̬�ı�ʱ��*/
		
		"szStatInfo",		/*״̬����*/
	
		"szMemo",		/*��ע*/
	 ""};

	/*�Ž���Ϣ��*/
	static public string[] DOORCTRLINFO = new string[]{
		
		"dwCtrlSN",		/*���������*/
		
		"szCtrlModel",		/*���������ͣ�����汾�ȣ�*/
	
		"dwCtrlKind",		/*���������ͼ�UNIDOORCTRL����*/
		
		"szCtrlIP",		/*������IP��ַ*/
	 ""};

	/*��������¼����*/
	static public string[] DCSLOGINREQ = new string[]{
		
		"szVersion",		/*�汾	XX.XX.XXXXXXXX*/
	
		"szIP",		/*IP��ַ*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*��������¼��Ӧ*/
	static public string[] DCSLOGINRES = new string[]{
		
		"dwSessionID",		/*�����������SessionID*/
		
	"SrvVer",		/*UNIVERSION �ṹ*/
	
		"szCurTime",		/*������ʱ�� YYYY-MM-DD HH:MM:SS*/
	
		"dwDCSSN",		/*����̨���*/
		
		"szDCSName",		/*����̨����*/
	
		"szMemo",		/*˵����Ϣ*/
	
	"szManCtrls",		/*�����Ž��б�CUniTable[DOORCTRLINFO]*/
	 ""};

	/*�������˳�����*/
	static public string[] DCSLOGOUTREQ = new string[]{
		
		"dwDCSSN",		/*���������*/
		
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*������״̬*/
	static public string[] DOORCTRLSTAT = new string[]{
		
		"dwCtrlSN",		/*���������*/
		
		"dwStatus",		/*DCSSTAT_TROUBLE,DCSSTAT_DOOROPEN,DCSSTAT_DOORCLOSED*/
		
		"szMemo",		/*��ע*/
	 ""};

	/*��������ʱͨ������*/
	static public string[] DCSPULSEREQ = new string[]{
		
		"dwDCSSN",		/*����̨���*/
		
	"szControllerStat",		/*������״̬��ϢCUniTable[DOORCTRLSTAT]*/
	 ""};

	/*��������ʱͨ��Ӧ��*/
	static public string[] DCSPULSERES = new string[]{
		
		"dwChanged",		/*�������Ƿ�ı�*/
		
		"szMemo",		/*��ע*/
	 ""};

	/*�Ž�ˢ������*/
	static public string[] DOORCARDREQ = new string[]{
		
		"dwDCSSN",		/*���������*/
		
		"dwCtrlSN",		/*���������*/
		
      "dwCardMode",		/*����ˢ�����������(3�ֽڣ�4�ֽڵ�)*/
		
		"szCardNo",		/*����*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*�Ž�ˢ����Ӧ*/
	static public string[] DOORCARDRES = new string[]{
		
      "dwUserKind",		/*�û�����*/
		
		"dwSoundSN",		/*��������*/
		
		"dwDeadLine",		/*�����ֹʱ��*/
		
		"szPID",		/*ѧ����*/
	
		"szCardNo",		/*����*/
	
		"szTrueName",		/*����*/
	
		"szDispInfo",		/*��ʾ��Ϣ*/
	 ""};

	/*�ֻ���������*/
	static public string[] MOBILEOPENDOORREQ = new string[]{
		
		"szMSN",		/*MSN*/
	
		"szLogonName",		/*��¼��*/
	
		"szPassword",		/*����*/
	
		"szIP",		/*IP��ַ*/
	
      "dwProperty",		/*��չ����*/
		
		"dwDCSSN",		/*���������*/
		
		"dwCtrlSN",		/*���������*/
		
		"dwCardMode",		/*����ˢ��*/
		
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*�ֻ�������Ӧ*/
	static public string[] MOBILEOPENDOORRES = new string[]{
		
		"dwUserKind",		/*�û�����(DOORCARDRES����)*/
		
      "dwFailedType",		/*ʧ������*/
		
		"szPID",		/*ѧ����*/
	
		"szTrueName",		/*����*/
	
		"szDispInfo",		/*��ʾ��Ϣ*/
	 ""};

	/*��ȡ�Ž��������������*/
	static public string[] DOORCTRLREQ = new string[]{
		
      "dwGetType",		/*��ʽ*/
		
		"szGetKey",		/*����ֵ*/
	
		"dwDCSKind",		/*�Ž�����������*/
		 ""};

	/**/
	static public string[] UNIDOORCTRL = new string[]{
		
		"dwDCSSN",		/*���������*/
		
		"dwDCSKind",		/*�Ž�����������*/
		
		"szDCSName",		/*�Ž�����������*/
	
		"szDCSIP",		/*�Ž�������IP��ַ*/
	
		"dwCtrlSN",		/*���������*/
		
      "dwCtrlKind",		/*����������*/
		
		"szRoomNo",		/*�����*/
	
		"szCtrlName",		/*����������*/
	
		"szCtrlModel",		/*�������ͺţ�����汾�ȣ�*/
	
		"szCtrlIP",		/*������IP��ַ*/
	
		"dwStaSN",		/*����վ����*/
		
		"szStaName",		/*վ������*/
	
		"dwStatus",		/*�Ž�״̬�� UNIDCS����*/
		
		"dwStatChgTime",		/*״̬�ı�ʱ��*/
		
		"szStatInfo",		/*״̬����*/
	
		"szMODIP",		/*�����ֻ�����IP��*/
	
		"szMemo",		/*��ע*/
	
		"dwRoomID",		/*����ID*/
		
		"szRoomName",		/*��������*/
	
		"szFloorNo",		/*����¥��*/
	
		"dwBuildingID",		/*¥��ID*/
		
		"szBuildingNo",		/*��¥���(*/
	
		"szBuildingName",		/*��¥����(*/
	 ""};

	/*���Ž�������������*/
	static public string[] DOORCTRLCMD = new string[]{
		
		"dwCtrlSN",		/*���������*/
		
		"dwCmd",		/*��������(�ο�DEVCTRLINFO::dwCmd)*/
		
		"szParam",		/*���Ʋ���*/
	
		"szMemo",		/*��ע*/
	 ""};

}
	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/
public partial class Struct_ST
{

	/*��ȡ����Ϣ�������*/
	static public string[] GROUPREQ = new string[]{
		
		"dwGroupID",		/*��ID*/
		
		"szName",		/*����*/
	
		"dwKind",		/*����*/
		
		"dwAccNo",		/*���Ա�ʺ�*/
		
		"dwMinDeadLine",		/*��С��ֹ����*/
		
		"dwMaxDeadLine",		/*����ֹ����*/
		
      "dwReqProp",		/*���󸽼�����*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*���Ա*/
	static public string[] GROUPMEMBER = new string[]{
		
		"dwGroupID",		/*����*/
		
      "dwKind",		/*��Ա���*/
		
		"dwMemberID",		/*��ԱID*/
		
		"szName",		/*��Ա����*/
	
		"dwBeginDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��ֹ����*/
		
      "dwStatus",		/*״̬��ǰ8����������CHECKINFO����Ĺ���Ա���״̬)*/
		
		"szExtInfo",		/*����˵��*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/**/
	static public string[] UNIGROUP = new string[]{
		
		"dwGroupID",		/*��ID*/
		
		"szName",		/*����*/
	
      "dwKind",		/*����*/
		
		"dwMaxUsers",		/*����û���*/
		
		"dwMinUsers",		/*�����û���*/
		
		"dwDeadLine",		/*��ֹ����*/
		
		"dwEnrollDeadline",		/*��������ֹ��*/
		
		"dwAssociateID",		/*��չ�ã�����ID������ԤԼ���ԤԼID��*/
		
		"szGroupURL",		/*����*/
	
	"szMembers",		/*��Ա��ϸ��(CUniTable<GROUPMEMBER>)*/
	
		"szMemo",		/*��ע*/
	 ""};

	/*��ȡ���Ա��ϸ�������*/
	static public string[] GROUPMEMDETAILREQ = new string[]{
		
		"dwGroupKind",		/*�����*/
		
		"dwGroupID",		/*����*/
		
		"dwStatus",		/*���״̬*/
		
		"dwAccNo",		/*��Ա�˺�*/
		
      "dwReqProp",		/*���󸽼�����*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*���Ա��ϸ*/
	static public string[] GROUPMEMDETAIL = new string[]{
		
		"dwGroupID",		/*����*/
		
		"dwBeginDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��ֹ����*/
		
		"dwStatus",		/*���״̬*/
		
		"szExtInfo",		/*����˵��*/
	
		"dwAccNo",		/*��Ա�˺�*/
		
		"szPID",		/*ѧ����*/
	
		"szTrueName",		/*����*/
	
		"dwClassID",		/*�༶ID*/
		
		"szClassName",		/*�༶*/
	
		"dwDeptID",		/*����ID*/
		
		"szDeptName",		/*����*/
	
		"dwMajorID",		/*רҵID*/
		
		"szMajorName",		/*רҵ*/
	
		"dwSex",		/*�Ա��UniCommon.h*/
		
		"dwIdent",		/*��� ��UniCommon.h*/
		
		"dwEnrolYear",		/*��ѧ���(XX��)*/
		
		"dwSchoolYears",		/*ѧ��*/
		
		"szTel",		/*�绰*/
	
		"szHandPhone",		/*�ֻ�*/
	
		"szEmail",		/*����*/
	
		"dwTutorID",		/*��ʦ�˺�*/
		
		"szTutorName",		/*��ʦ����*/
	 ""};

}
	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/
public partial class Struct_ST
{

	/*��ȡԤԼ��Ϣ�������*/
	static public string[] RESVREQ = new string[]{
		
		"dwResvID",		/*ԤԼ��*/
		
		"dwMemberKind",		/*��Ա���*/
		
		"dwOwner",		/*ԤԼ��(������)*/
		
		"dwMemberID",		/*��ԱID*/
		
		"dwDevID",		/*�豸ID*/
		
		"dwManagerID",		/*����ԱID*/
		
		"dwAccNo",		/*��Ա�˺ţ���ȡ��Ա��ص�����ʵ�鰲��*/
		
		"dwTestPlanID",		/*��ȡTestPlan��ص�����ʵ�鰲��*/
		
		"dwCourseID",		/*��ȡ�γ���ص�����ʵ�鰲��*/
		
		"dwTestItemID",		/*��ȡTestItem��ص�����ʵ�鰲��*/
		
		"dwCheckStat",		/*ȷ��״̬*/
		
		"dwUnNeedStat",		/*������״̬*/
		
		"dwUseMode",		/*ʹ�÷���*/
		
		"dwPurpose",		/*ԤԼ��;*/
		
		"dwDevKind",		/*�豸����*/
		
		"dwClassKind",		/*���(��UNIDEVCLS��Kind����)*/
		
		"dwBeginDate",		/*ԤԼ��ʼ����*/
		
		"dwEndDate",		/*ԤԼ��������*/
		
		"szRoomNos",		/*������,����ö��Ÿ���*/
	
		"dwResvGroupID",		/*ԤԼ��ID*/
		
      "dwStatFlag",		/*״̬��־*/
		
		"dwKind",		/*ԤԼ����*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*ԤԼ��*/
	static public string[] UNIRESERVE = new string[]{
		
		"dwResvID",		/*ԤԼ��*/
		
      "dwMemberKind",		/*��Ա���*/
		
      "dwUseMode",		/*ʹ�÷���*/
		
      "dwPurpose",		/*ԤԼ��;*/
		
		"dwKind",		/*ԤԼ����(����ͳ����)*/
		
		"dwOwner",		/*ԤԼ��(������)*/
		
		"szOwnerName",		/*ԤԼ������*/
	
		"dwMemberID",		/*��ԱID*/
		
		"szMemberName",		/*��Ա����*/
	
		"dwResvRuleSN",		/*����ԤԼ����*/
		
		"dwFeeSN",		/*�������շѱ�׼*/
		
		"dwOpenRuleSN",		/*��������ʱ���*/
		
		"dwFeeMode",		/*�շѷ�ʽ*/
		
		"dwUseFee",		/*ԤԼ�ܷ���*/
		
		"dwPreDate",		/*ԤԼ��ʼ����*/
		
		"dwOccurTime",		/*ԤԼ����ʱ��*/
		
		"dwBeginTime",		/*ԤԼ��ʼʱ��*/
		
		"dwEndTime",		/*ԤԼ����ʱ��*/
		
		"dwYearTerm",		/*ѧ�ڱ��*/
		
		"dwTeachingTime",		/*��ѧʱ��(WWDSSEE)�ڼ��ܣ�WW�����ڼ�(D),��ʼ�ڴΣ�SS�������ڴΣ�EE)*/
		
		"dwCheckTime",		/*���ʱ��*/
		
		"dwAdvanceCheckTime",		/*��ǰ���ʱ��*/
		
		"dwResvGroupID",		/*��ID(��ʱ��ԤԼ��ID��ͬ������ΪԤԼID)*/
		
		"dwCheckKinds",		/*�������(�ο�CHECKTYPE���壬�ɶ����*/
		
		"dwLabID",		/*ʵ����ID*/
		
		"szLabName",		/*ʵ��������*/
	
	"ResvDev",		/*ԤԼ�豸��ϸ��(CUniTable<RESVDEV>)*/
	
      "dwStatus",		/*ԤԼ״̬(������飬�Ƿ���Ч���Ƿ���ȡ����)*/
		
      "dwProperty",		/*ԤԼ����*/
		
		"dwTestItemID",		/*ʵ����ĿID*/
		
		"szTestName",		/*ʵ������*/
	
		"dwTestHour",		/*ʵ��ѧʱ��*/
		
		"dwSignTime",		/*��ʦǩ��ʱ��*/
		
		"dwResvDevs",		/*ԤԼ������*/
		
		"dwUseDevs",		/*ʵ���û���*/
		
		"dwResvUsers",		/*�Ͽ�������*/
		
		"dwRealUsers",		/*ʵ�ʵ�������*/
		
		"szApplicationURL",		/*�ύ�������������*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*��ȡ����ʵ��ԤԼ�������*/
	static public string[] RTRESVREQ = new string[]{
		
		"dwResvID",		/*ԤԼ��*/
		
		"dwDevID",		/*�豸ID*/
		
		"dwDevKind",		/*�豸����*/
		
		"dwClassKind",		/*���(��UNIDEVCLS��Kind����)*/
		
		"dwRTID",		/*����ʵ��ƻ�ID*/
		
		"dwHolderID",		/*�����ˣ��ʺţ�*/
		
		"dwMAccNo",		/*���Ա�ʺ�*/
		
		"dwLeaderID",		/*������ID*/
		
		"dwCheckStat",		/*ȷ��״̬*/
		
		"dwUnNeedStat",		/*������״̬*/
		
		"dwUseMode",		/*ʹ�÷���*/
		
		"dwPurpose",		/*ԤԼ��;*/
		
		"dwBeginDate",		/*ԤԼ��ʼ����*/
		
		"dwEndDate",		/*ԤԼ��������*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*ԤԼ��Ʒ��*/
	static public string[] RESVSAMPLE = new string[]{
		
		"dwResvID",		/*ԤԼID*/
		
		"dwSampleSN",		/*��Ʒ���*/
		
		"szSampleName",		/*��Ʒ����*/
	
		"szUnitName",		/*�Ʒѵ�λ*/
	
		"dwUnitFee",		/*����*/
		
		"dwSampleNum",		/*����*/
		
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*ԤԼ��*/
	static public string[] RTRESV = new string[]{
		
		"dwResvID",		/*ԤԼ��*/
		
		"szTestName",		/*����ʵ������*/
	
		"dwUseMode",		/*ʹ�÷���*/
		
		"dwPurpose",		/*ԤԼ��;*/
		
		"dwProperty",		/*ԤԼ����*/
		
		"dwStatus",		/*ԤԼ״̬(������飬�Ƿ���Ч���Ƿ���ȡ����)*/
		
		"dwOwner",		/*ԤԼ��(������)*/
		
		"szOwnerName",		/*ԤԼ������*/
	
		"dwResvRuleSN",		/*����ԤԼ����*/
		
		"dwOpenRuleSN",		/*��������ʱ���*/
		
		"dwFeeSN",		/*����SN*/
		
		"dwFeeMode",		/*�շѷ�ʽ*/
		
		"dwUseFee",		/*ԤԼ�ܷ���*/
		
		"dwPreDate",		/*ԤԼ��ʼ����*/
		
		"dwOccurTime",		/*ԤԼ����ʱ��*/
		
		"dwBeginTime",		/*ԤԼ��ʼʱ��*/
		
		"dwEndTime",		/*ԤԼ����ʱ��*/
		
		"dwCheckTime",		/*���ʱ��*/
		
		"dwAdvanceCheckTime",		/*��ǰ���ʱ��*/
		
		"dwDevID",		/*�豸ID*/
		
		"dwDevKind",		/*�豸����*/
		
		"dwDevSN",		/*�豸���*/
		
		"szDevName",		/*�豸����*/
	
		"dwRoomID",		/*����ID*/
		
		"szRoomName",		/*��������*/
	
		"dwLabID",		/*ʵ����ID*/
		
		"szLabName",		/*ʵ��������*/
	
		"dwBuildingID",		/*¥��ID*/
		
		"szBuildingNo",		/*��¥���(*/
	
		"szBuildingName",		/*��¥����(*/
	
		"dwCampusID",		/*У��ID*/
		
		"szCampusName",		/*У������*/
	
		"dwRTID",		/*����ʵ����ĿID*/
		
		"szRTName",		/*����ʵ������*/
	
		"dwHolderID",		/*�����ˣ��ʺţ�*/
		
		"szHolderName",		/*����������*/
	
		"dwUserDeptID",		/*ʹ���˲���ID*/
		
		"szUserDeptName",		/*ʹ���˲���*/
	
		"dwLeaderID",		/*������ID*/
		
		"szLeaderName",		/*����������*/
	
		"dwGroupID",		/*��ID*/
		
		"dwManID",		/*����ԱID*/
		
		"szManName",		/*����Ա����*/
	
		"dwEstimatedTime",		/*Ԥ��ʱ��(����)*/
		
		"dwTestTimes",		/*ʵ�����*/
		
		"dwRealUseTime",		/*ʵ��ʵ��ʱ��(����)*/
		
		"dwReceivableCost",		/*Ӧ�ɷ���*/
		
		"dwRealCost",		/*ʵ�ʽ��ɷ���*/
		
		"dwPrepayment",		/*Ԥ�տ���*/
		
	"ResvSample",		/*CUniTable[RESVSAMPLE](��ȡһ��ԤԼʱ�ŷ���)*/
	
		"szConsumables",		/*����Ĳ��嵥*/
	
		"dwBeforePersons",		/*ǰ���Ŷ�����*/
		
		"szFundsNo",		/*���ѿ����*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*����ʵ�����*/
	static public string[] RTRESVCHECK = new string[]{
		
		"dwCheckStat",		/*����Ա���״̬(������ADMINCHECK)*/
		
		"szCheckDetail",		/*���˵��*/
	
	"RTResv",		/*CUniStruct[RTRESV]*/
	
	"BillInfo",		/*CUniTable[RTBILL]*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*����ʵ���˵����*/
	static public string[] RTPREPAY = new string[]{
		
		"dwResvID",		/*ԤԼ��*/
		
		"dwPrepayment",		/*Ԥ�տ���*/
		
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*��ȡ����ʵ���˵��������*/
	static public string[] RTRESVBILLREQ = new string[]{
		
		"dwResvID",		/*ԤԼ��*/
		 ""};

	/*����ʵ���˵�*/
	static public string[] RTRESVBILL = new string[]{
		
		"dwResvID",		/*ԤԼ��*/
		
		"dwPrepayment",		/*Ԥ�տ���*/
		
	"BillInfo",		/*CUniTable[RTBILL]*/
	 ""};

	/*����ʵ���˵�*/
	static public string[] RTBILL = new string[]{
		
		"dwSID",		/*��ˮ��*/
		
		"dwResvID",		/*ԤԼ��*/
		
		"dwFeeType",		/*�շ����(FEEDETAIL����)*/
		
		"dwUnitFee",		/*����*/
		
		"dwReceivableCost",		/*Ӧ�ɷ���*/
		
		"dwRealCost",		/*ʵ�ʽ��ɷ���*/
		
		"dwPayKind",		/*�ɷѷ�ʽ(UNIBILL����)*/
		
		"dwStatus",		/*CHECKINFO����Ĺ���Ա���״̬+UNIBILL����*/
		
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*����ʵ���˵����*/
	static public string[] RTBILLCHECK = new string[]{
		
		"dwResvID",		/*ԤԼ��*/
		
	"BillInfo",		/*CUniTable[RTBILL]*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*����ʵ���˵�����*/
	static public string[] RTBILLSETTLE = new string[]{
		
		"dwResvID",		/*ԤԼ��*/
		
		"dwPurpose",		/*ԤԼ��;(��UNIRESERVE����USEBY_XXX��*/
		
		"dwPayKind",		/*�ɷѷ�ʽ*/
		
		"dwTotalCost",		/*�ɷѺϼ�*/
		
		"szFundsNo",		/*���ѿ���ţ�����ö��Ÿ���)*/
	
		"szCostInfo",		/*�۷���Ϣ*/
	
	"BillInfo",		/*CUniTable[RTBILL]*/
	
	"ResvSample",		/*CUniTable[RESVSAMPLE]*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*����ʵ���˵�����*/
	static public string[] RTBILLRECEIVE = new string[]{
		
		"dwResvID",		/*ԤԼ��*/
		
		"dwReceiveDate",		/*��������*/
		
		"dwTotalCost",		/*�ɷѺϼ�*/
		
		"dwTestFee",		/*�������Է�*/
		
		"dwOpenFundFee",		/*���Ż���*/
		
		"dwServiceFee",		/*�����*/
		
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*���¼ԤԼ*/
	static public string[] ANONYMOUSRESV = new string[]{
		
		"dwResvID",		/*ԤԼ��*/
		
		"dwPurpose",		/*ԤԼ��;*/
		
		"dwPreDate",		/*ԤԼ��ʼ����*/
		
		"dwBeginTime",		/*ԤԼ��ʼʱ��*/
		
		"dwEndTime",		/*ԤԼ����ʱ��*/
		
		"dwYearTerm",		/*ѧ�ڱ��*/
		
		"dwTeachingTime",		/*��ѧʱ��(WWDSSEE)�ڼ��ܣ�WW�����ڼ�(D),��ʼ�ڴΣ�SS�������ڴΣ�EE)*/
		
		"dwLabID",		/*ʵ����ID*/
		
		"szLabName",		/*ʵ��������*/
	
		"szTestName",		/*ʵ������*/
	
	"ResvDev",		/*ԤԼ�豸��ϸ��(CUniTable<RESVDEV>)*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*ȫ��ѧ��ԤԼ*/
	static public string[] ALLUSERRESV = new string[]{
		
		"dwResvID",		/*ԤԼ��*/
		
		"dwPurpose",		/*ԤԼ��;*/
		
		"dwPreDate",		/*ԤԼ��ʼ����*/
		
		"dwBeginTime",		/*ԤԼ��ʼʱ��*/
		
		"dwEndTime",		/*ԤԼ����ʱ��*/
		
		"dwYearTerm",		/*ѧ�ڱ��*/
		
		"dwTeachingTime",		/*��ѧʱ��(WWDSSEE)�ڼ��ܣ�WW�����ڼ�(D),��ʼ�ڴΣ�SS�������ڴΣ�EE)*/
		
		"dwLabID",		/*ʵ����ID*/
		
		"szLabName",		/*ʵ��������*/
	
		"szTestName",		/*ʵ������*/
	
	"ResvDev",		/*ԤԼ�豸��ϸ��(CUniTable<RESVDEV>)*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*��ȡԤԼ�б�������վ��ʾ�������*/
	static public string[] RESVSHOWREQ = new string[]{
		
		"dwBeginDate",		/*ԤԼ��ʼ����*/
		
		"dwEndDate",		/*ԤԼ��������*/
		
		"dwDevKind",		/*�豸����*/
		
		"dwClassKind",		/*���(��UNIDEVCLS��Kind����)*/
		
		"dwCheckStat",		/*ȷ��״̬*/
		
		"dwPurpose",		/*ԤԼ��;*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*ԤԼ��*/
	static public string[] RESVSHOW = new string[]{
		
		"dwResvID",		/*ԤԼ��*/
		
		"dwPurpose",		/*ԤԼ��;*/
		
		"dwKind",		/*ԤԼ����(����ͳ����)*/
		
		"dwOwner",		/*ԤԼ��(������)*/
		
		"szOwnerName",		/*ԤԼ������*/
	
		"dwPreDate",		/*ԤԼ��ʼ����*/
		
		"dwBeginTime",		/*ԤԼ��ʼʱ��*/
		
		"dwEndTime",		/*ԤԼ����ʱ��*/
		
		"dwStatus",		/*ԤԼ״̬(������飬�Ƿ���Ч���Ƿ���ȡ����)*/
		
		"dwProperty",		/*ԤԼ����*/
		
		"szTestName",		/*ʵ������*/
	
		"szRoomNo",		/*�����*/
	
		"dwDevSN",		/*�豸���*/
		
		"szDevName",		/*�豸��*/
	 ""};

	/*��ȡ��ѧԤԼ�б�������*/
	static public string[] TEACHINGRESVREQ = new string[]{
		
		"dwBeginDate",		/*ԤԼ��ʼ����*/
		
		"dwEndDate",		/*ԤԼ��������*/
		
		"dwTeacherID",		/*��ʦ���ʺţ�*/
		
		"dwAccNo",		/*��Ա�˺ţ���ȡ��Ա��ص�����ʵ�鰲��*/
		
		"dwTestPlanID",		/*��ȡTestPlan��ص�����ʵ�鰲��*/
		
		"dwCourseID",		/*��ȡ�γ���ص�����ʵ�鰲��*/
		
		"dwTestItemID",		/*��ȡTestItem��ص�����ʵ�鰲��*/
		
		"dwYearTerm",		/*ѧ�ڱ��*/
		
		"dwPlanKind",		/*�ƻ�����*/
		
		"dwResvStat",		/*ԤԼ״̬*/
		
		"szRoomNo",		/*�����*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*��ѧԤԼ*/
	static public string[] TEACHINGRESV = new string[]{
		
		"dwTestItemID",		/*ʵ����ĿID*/
		
		"szTestName",		/*ʵ������*/
	
		"dwTestPlanID",		/*ʵ��ƻ�ID*/
		
		"szTestPlanName",		/*ʵ��ƻ�����*/
	
		"dwPlanKind",		/*�ƻ�����*/
		
		"dwTeacherID",		/*��ʦ���ʺţ�*/
		
		"szTeacherName",		/*��ʦ����*/
	
		"dwCourseID",		/*�γ�ID*/
		
		"szCourseName",		/*�γ�����*/
	
		"dwGroupID",		/*�Ͽΰ༶*/
		
		"szGroupName",		/*�Ͽΰ༶����*/
	
		"dwGroupUsers",		/*���û���*/
		
		"dwResvID",		/*ԤԼID*/
		
		"dwResvStat",		/*ԤԼ״̬*/
		
		"dwPreDate",		/*ԤԼ����*/
		
		"dwBeginTime",		/*ԤԼ��ʼʱ��*/
		
		"dwEndTime",		/*ԤԼ����ʱ��*/
		
		"dwTeachingTime",		/*��ѧʱ��(WWDSSEE)�ڼ��ܣ�WW�����ڼ�(D),��ʼ�ڴΣ�SS�������ڴΣ�EE)*/
		
		"dwLabID",		/*ʵ����ID*/
		
		"szLabName",		/*ʵ��������*/
	
	"ResvDev",		/*ԤԼ�豸��ϸ��(CUniTable<RESVDEV>)*/
	
		"dwCurUsers",		/*��ǰ����������Ŀǰ��Ч��ԤԼ��Ч��*/
		 ""};

	/*�żٵ���*/
	static public string[] HOLIDAYSHIFT = new string[]{
		
		"dwOldDate",		/*ԭ�Ͽ�����*/
		
		"dwNewDate",		/*���Ͽ�����*/
		
		"dwNoticeFlag",		/*֪ͨ��־*/
		
		"szMemo",		/*˵��*/
	 ""};

	/*ԤԼ���Ա*/
	static public string[] RESVMEMBER = new string[]{
		
		"dwResvID",		/*ԤԼ��*/
		
		"dwAccNo",		/*��Ա�˺�*/
		
		"szTrueName",		/*����*/
	 ""};

	/*ԤԼ��ʼ��Ϣ*/
	static public string[] RESVSTARTINFO = new string[]{
		
		"dwForceLogoff",		/*�Ƿ�ע���û�*/
		
		"dwNoLogon",		/*���¼��־*/
		
		"dwPurpose",		/*ԤԼĿ��*/
		
		"szClassName",		/*ԤԼ�༶*/
	
		"szUsers",		/*ԤԼѧ��*/
	 ""};

	/*ԤԼ��ʼ��Ϣ*/
	static public string[] RESVENDINFO = new string[]{
		
      "dwEndCmd",		/*������ʽ*/
		
		"szMsg",		/*��Ϣ*/
	 ""};

	/*�Զ�ԤԼ����ϵͳ�Զ������豸����ʱ�������ȴ��û�ȷ��*/
	static public string[] AUTORESVREQ = new string[]{
		
		"dwOwner",		/*ԤԼ��(������)*/
		
		"dwKind",		/*ԤԼ��Ա���*/
		
		"dwPurpose",		/*ԤԼ��;*/
		
		"dwPreDate",		/*ԤԼ��������*/
		
		"dwEarlyBeginTime",		/*ԤԼ���翪ʼʱ��*/
		
		"dwLateBeginTime",		/*ԤԼ����ʼʱ��*/
		
		"dwUseMin",		/*ʹ��ʱ��*/
		
		"dwRoomID",		/*����ID*/
		
		"dwDevKind",		/*�豸����*/
		
		"szUserLimit",		/*ʹ����Լ������δʹ��)*/
	
		"szDateLimit",		/*ʱ��Լ������δʹ��)*/
	
		"szDevLimit",		/*�豸Լ������δʹ��)*/
	 ""};

	/*ԤԼ�豸��ϸ*/
	static public string[] RESVDEV = new string[]{
		
		"dwLabID",		/*ʵ����ID*/
		
		"szLabName",		/*ʵ��������*/
	
		"dwRoomID",		/*����ID*/
		
		"szRoomNo",		/*������*/
	
		"szRoomName",		/*��������*/
	
		"dwDevKind",		/*�豸����*/
		
		"dwDevNum",		/*�豸����*/
		
		"dwDevStart",		/*�豸��ʼ���*/
		
		"dwDevEnd",		/*�豸�������*/
		
		"szDevName",		/*�豸��*/
	
		"szModel",		/*�豸�ͺ�*/
	
		"szSpecification",		/*�豸���*/
	
		"dwDevClsKind",		/*�豸������*/
		
		"szMemo",		/*��ע*/
	 ""};

	/*ԤԼʱ�����ϸ*/
	static public string[] RESVTIMEREQ = new string[]{
		
		"dwResvID",		/*ԤԼ��*/
		
		"dwResvGroupID",		/*��ID*/
		 ""};

	/*ԤԼʱ�����ϸ*/
	static public string[] RESVTIME = new string[]{
		
		"dwResvID",		/*ԤԼ��*/
		
		"dwResvGroupID",		/*��ID*/
		
		"dwPreDate",		/*ԤԼ��������*/
		
		"dwBeginTime",		/*ԤԼ��ʼʱ��*/
		
		"dwEndTime",		/*ԤԼ����ʱ��*/
		
		"dwTeachingTime",		/*��ѧʱ��(WWDSSEE)�ڼ��ܣ�WW�����ڼ�(D),��ʼ�ڴΣ�SS�������ڴΣ�EE)*/
		
		"szMemo",		/*��ע*/
	 ""};

	/*ԤԼ���ú���*/
	static public string[] RESVCOSTADJUST = new string[]{
		
		"dwResvID",		/*ԤԼ��*/
		
		"dwAdjustFee",		/*��������*/
		
		"dwSampleFee",		/*��Ʒ��*/
		
		"dwConfirmorID",		/*����Ա�ʺ�*/
		
		"szConfirmor",		/*����Ա����*/
	
		"szMemo",		/*��ע��������õ�����˵��)*/
	 ""};

	/*ԤԼ���ý���*/
	static public string[] RESVCHECKOUT = new string[]{
		
		"dwResvID",		/*ԤԼ��*/
		
		"dwBillID",		/*�˵���*/
		
		"dwRealCheckFee",		/*ʵ�ʽ������*/
		
		"dwConfirmorID",		/*������Ա�ʺ�*/
		
		"szConfirmor",		/*������Ա����*/
	
		"szMemo",		/*��ע�����㸽�ӷ��õ�˵��)*/
	 ""};

	/*ԤԼ����ʱ��*/
	static public string[] RESVENDTIME = new string[]{
		
		"dwResvID",		/*ԤԼID*/
		
		"dwEndTime",		/*����ʱ��*/
		 ""};

	/*��ȡ�豸ԤԼ��Ϣ�������*/
	static public string[] DEVRESVREQ = new string[]{
		
      "dwGetType",		/*��ʽ*/
		
		"szGetKey",		/*����ֵ*/
	
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*�豸ԤԼ��Ϣ*/
	static public string[] DEVRESVINFO = new string[]{
		
		"dwDevID",		/*�豸ID*/
		
		"dwResvID",		/*ԤԼ��*/
		
		"dwBeginTime",		/*ԤԼ��ʼʱ��*/
		
		"dwEndTime",		/*ԤԼ����ʱ��*/
		 ""};

	/*��ȡ��Ϣʱ��������*/
	static public string[] CTSREQ = new string[]{
		
		"dwSN",		/*��Ϣ����*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*��Ϣʱ���*/
	static public string[] CLASSTIMETABLE = new string[]{
		
		"dwSN",		/*��Ϣ����*/
		
		"dwSecIndex",		/*�ڴα��(1��ʼ��)*/
		
		"szSecName",		/*�ڴ�����*/
	
		"dwBeginTime",		/*��ʼʱ��(HHMM)*/
		
		"dwEndTime",		/*����ʱ��(HHMM)*/
		 ""};

	/*��ȡѧ����Ϣ�������*/
	static public string[] TERMREQ = new string[]{
		
		"dwYearTerm",		/*ѧ�ڱ�ţ�20091001 2009��10���һѧ�ڣ�*/
		
		"dwStatus",		/*ѧ��״̬*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/**/
	static public string[] UNITERM = new string[]{
		
		"dwYearTerm",		/*ѧ�ڱ�ţ�20091001 2009��10���һѧ�ڣ�*/
		
      "dwStatus",		/*ѧ��״̬*/
		
		"dwBeginDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
		"dwFirstWeekDays",		/*��ʼ������*/
		
		"dwTotalWeeks",		/*һ���ж�����*/
		
		"dwSecNum",		/*ÿ�սڴ�����*/
		
	"szCTS1",		/*��Ϣʱ���1(CUniTable<CLASSTIMETABLE>)*/
	
		"dwCTS1Begin",		/*ʱ���1��ʼ��Ч����*/
		
		"dwCTS1End",		/*ʱ���1������Ч����*/
		
	"szCTS2",		/*��Ϣʱ���2(CUniTable<CLASSTIMETABLE>)*/
	
		"dwCTS2Begin",		/*ʱ���2��ʼ��Ч����*/
		
		"dwCTS2End",		/*ʱ���2������Ч����*/
		
		"szMemo",		/*��ע*/
	 ""};

	/*��ȡ�γ̵������*/
	static public string[] COURSEREQ = new string[]{
		
		"dwCourseID",		/*�γ�ID*/
		
		"szCourseCode",		/*�γ̴���*/
	
		"szCourseName",		/*�γ�����*/
	
		"dwOwnerDept",		/*����ѧԺ*/
		
		"dwMajorID",		/*����רҵID*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*�γ�*/
	static public string[] UNICOURSE = new string[]{
		
		"dwCourseID",		/*�γ�ID*/
		
		"szCourseCode",		/*�γ̴���*/
	
		"szCourseName",		/*�γ�����*/
	
      "dwCourseProperty",		/*�γ�����*/
		
		"dwOwnerDept",		/*����ѧԺ*/
		
		"szDeptName",		/*����ѧԺ����*/
	
		"dwMajorID",		/*����רҵID*/
		
		"szMajorName",		/*����רҵ����*/
	
		"szType",		/*�γ����ѡ�ޣ����޵ȣ�*/
	
		"dwHardCoef",		/*�Ѷ�ϵ��*/
		
		"dwCreditHour",		/*ѧ��*/
		
		"dwTheoryHour",		/*����ѧʱ��*/
		
		"dwTestHour",		/*ʵ��ѧʱ��*/
		
		"dwPracticeHour",		/*ʵ��ѧʱ��*/
		
		"dwTestNum",		/*ʵ�����*/
		
		"szMemo",		/*��ע*/
	 ""};

	/*��ȡʵ��ƻ������*/
	static public string[] TESTPLANREQ = new string[]{
		
      "dwGetType",		/*��ʽ*/
		
		"szGetKey",		/*����ֵ*/
	
		"dwYearTerm",		/*ѧ�ڱ��*/
		
		"dwKind",		/*ʵ��ƻ�����*/
		
		"dwStatus",		/*ʵ��ƻ�״̬*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/**/
	static public string[] UNITESTPLAN = new string[]{
		
		"dwYearTerm",		/*ѧ�ڱ��*/
		
		"dwTestPlanID",		/*ʵ��ƻ�ID*/
		
		"szTestPlanName",		/*ʵ��ƻ�����*/
	
		"szAcademicSubjectCode",		/*����ѧ�Ʊ���*/
	
      "dwTesteeKind",		/*ʵ�������*/
		
		"dwTeacherID",		/*��ʦ���ʺţ�*/
		
		"szTeacherName",		/*��ʦ����*/
	
		"dwTeacherDeptID",		/*��ʦ��������ID*/
		
		"szTeacherDeptName",		/*��ʦ��������*/
	
		"dwCourseID",		/*�γ�ID*/
		
		"szCourseCode",		/*�γ̴���*/
	
		"szCourseName",		/*�γ�����*/
	
		"dwCourseProperty",		/*�γ�����*/
		
		"dwTheoryHour",		/*����ѧʱ��*/
		
		"dwTestHour",		/*ʵ��ѧʱ��*/
		
		"dwPracticeHour",		/*ʵ��ѧʱ��*/
		
		"dwTestNum",		/*ʵ����Ŀ��*/
		
		"dwGroupID",		/*�Ͽΰ༶*/
		
		"szGroupName",		/*�Ͽΰ༶����*/
	
		"dwMaxUsers",		/*����û���*/
		
		"dwMinUsers",		/*�����û���*/
		
		"dwGroupUsers",		/*�Ͽ�ѧ����*/
		
		"dwEnrollDeadline",		/*��������ֹ��*/
		
      "dwKind",		/*ʵ��ƻ�����*/
		
      "dwStatus",		/*ʵ��ƻ�״̬��ǰ8����������CHECKINFO����Ĺ���Ա���״̬)*/
		
		"szTestPlanURL",		/*��ϸ����*/
	
		"dwTotalTestHour",		/*��ѧʱ��*/
		
		"dwResvTestHour",		/*��ԤԼѧʱ��*/
		
		"dwDoneTestHour",		/*�����ѧʱ��*/
		
		"szUsableLab",		/*����ʵ����ID������ö��Ÿ���)*/
	
		"szMemo",		/*��ע*/
	 ""};

	/*��ȡʵ����Ŀ�������*/
	static public string[] TESTCARDREQ = new string[]{
		
		"dwTestCardID",		/*ʵ����Ŀ��ID*/
		
		"szTestName",		/*ʵ�����ƣ�ģ��ƥ��)*/
	
		"dwTestClass",		/*ʵ�����*/
		
		"dwTestKind",		/*ʵ������*/
		
		"dwRequirement",		/*ʵ��Ҫ��*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*ʵ����Ŀ��*/
	static public string[] TESTCARD = new string[]{
		
		"dwTestCardID",		/*ʵ����Ŀ��ID*/
		
		"szTestSN",		/*ʵ����*/
	
		"szTestName",		/*ʵ������*/
	
		"szCategoryName",		/*�����(*/
	
		"dwGroupPeopleNum",		/*ÿ������*/
		
		"dwTestHour",		/*��ʵ����Ŀѧʱ��*/
		
      "dwTestClass",		/*ʵ������ǰ�ʵ����Ŀ����������ʷ�������������������רҵ��������רҵ�����С�������������Ϊ��ҵ���ġ���ҵ��ơ����������������������ʵ�飩��*/
		
      "dwTestKind",		/*ʵ�����ͣ���ʾ����֤���ۺϡ���ƣ�*/
		
      "dwRequirement",		/*ʵ��Ҫ������ޡ�ѡ�ޡ����������в��ԡ��������񡢼��������ȣ���*/
		
		"szConstraints",		/*Լ������������ʱ�����ƺ���Ҫ���豸��,��ʽ��ר���ļ�����*/
	
		"szTestItemURL",		/*��ϸ����*/
	
		"szMemo",		/*��ע*/
	 ""};

	/*��ȡʵ����Ŀ�����*/
	static public string[] TESTITEMREQ = new string[]{
		
      "dwGetType",		/*��ʽ*/
		
		"szGetKey",		/*����ֵ*/
	
		"dwStatus",		/*״̬��ǰ8�������������״̬)*/
		
		"dwCourseID",		/*�γ�ID*/
		
		"dwTeacherID",		/*��ʦ���ʺţ�*/
		
		"dwAccNo",		/*�ʺ�*/
		
		"dwPlanKind",		/*�ƻ�����*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*ʵ�鰲����Ŀ��*/
	static public string[] UNITESTITEM = new string[]{
		
		"dwTestItemID",		/*ʵ����ĿID*/
		
		"dwTestPlanID",		/*ʵ��ƻ�ID*/
		
		"szTestPlanName",		/*ʵ��ƻ�����*/
	
		"szAcademicSubjectCode",		/*����ѧ�Ʊ���*/
	
		"dwTesteeKind",		/*ʵ�������*/
		
		"dwTotalTestHour",		/*��ʵ��ƻ���ѧʱ��*/
		
		"dwTeacherID",		/*��ʦ���ʺţ�*/
		
		"szTeacherName",		/*��ʦ����*/
	
		"dwCourseID",		/*�γ�ID*/
		
		"szCourseCode",		/*�γ̴���*/
	
		"szCourseName",		/*�γ�����*/
	
		"dwCourseProperty",		/*�γ�����*/
		
		"dwGroupID",		/*�Ͽΰ༶*/
		
		"szGroupName",		/*�Ͽΰ༶����*/
	
		"dwGroupUsers",		/*���û���*/
		
		"dwPlanKind",		/*�ƻ�����*/
		
		"dwPlanStatus",		/*�ƻ�״̬*/
		
		"dwTestCardID",		/*ʵ����Ŀ��ID*/
		
		"szTestSN",		/*ʵ����*/
	
		"szTestName",		/*ʵ������*/
	
		"szCategoryName",		/*�����(*/
	
      "dwStatus",		/*״̬��ǰ8�������������״̬)*/
		
		"dwGroupPeopleNum",		/*ÿ������*/
		
		"dwTestHour",		/*��ʵ����Ŀѧʱ��*/
		
		"dwTestClass",		/*ʵ�����*/
		
		"dwTestKind",		/*ʵ������*/
		
		"dwRequirement",		/*ʵ��Ҫ��*/
		
		"szTestItemURL",		/*��ϸ����*/
	
	"ResvInfo",		/*ԤԼ��ϸ��Ϣ*/
	
		"dwMaxResvTimes",		/*���ԤԼ����*/
		
		"dwResvTestHour",		/*��ԤԼѧʱ��*/
		
		"dwDoneTestHour",		/*�����ѧʱ��*/
		
		"szReportFormURL",		/*ʵ�鱨��ģ��*/
	
		"szConstraints",		/*Լ��������������Ҫ���豸�ȣ�*/
	
		"szMemo",		/*��ע*/
	 ""};

	/*��ȡʵ����Ŀ������ԤԼ��Ϣ�����*/
	static public string[] TESTITEMMEMRESVREQ = new string[]{
		
		"dwTestPlanID",		/*ʵ��ƻ�ID*/
		
		"dwTestItemID",		/*ʵ����ĿID*/
		
		"dwGroupID",		/*�Ͽΰ༶*/
		
		"dwResvTestHour",		/*��ԤԼѧʱ��*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*ʵ����Ŀ������ԤԼ��Ϣ*/
	static public string[] TESTITEMMEMRESV = new string[]{
		
		"dwAccNo",		/*�ʺ�*/
		
		"szPID",		/*ѧ����*/
	
		"szTrueName",		/*����*/
	
		"dwResvTestHour",		/*��ԤԼѧʱ��*/
		 ""};

	/*��ȡʵ����Ŀ��ϸ��Ϣ�����*/
	static public string[] TESTITEMINFOREQ = new string[]{
		
		"dwSID",		/*��¼ID*/
		
		"dwTeacherID",		/*��ʦ���ʺţ�*/
		
		"dwAccNo",		/*�ʺ�*/
		
		"dwTestPlanID",		/*ʵ��ƻ�ID*/
		
		"dwTestItemID",		/*ʵ����ĿID*/
		
		"dwYearTerm",		/*ѧ�ڱ��*/
		
		"dwPlanKind",		/*�ƻ�����*/
		
		"dwStatus",		/*״̬��ǰ8�������������״̬)*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*ʵ����Ŀ��ϸ��Ϣ*/
	static public string[] TESTITEMINFO = new string[]{
		
		"dwSID",		/*��¼ID*/
		
		"dwStatus",		/*��UNITESTITEM����*/
		
		"dwAccNo",		/*�ʺ�*/
		
		"szPID",		/*ѧ����*/
	
		"szTrueName",		/*����*/
	
		"dwClassID",		/*�༶ID*/
		
		"szClassName",		/*�༶*/
	
		"dwDeptID",		/*����ID*/
		
		"szDeptName",		/*����*/
	
		"dwMaxResvTimes",		/*���ԤԼ����*/
		
		"szReportFormURL",		/*ʵ�鱨��ģ��*/
	
		"szReportURL",		/*�ύ��ʵ�鱨��*/
	
		"dwReportScore",		/*ʵ�鱨������*/
		
		"szReportMarkInfo",		/*ʵ�鱨��������Ϣ*/
	
		"dwYearTerm",		/*ѧ�ڱ��*/
		
		"dwTestPlanID",		/*ʵ��ƻ�ID*/
		
		"szTestPlanName",		/*ʵ��ƻ�����*/
	
		"dwPlanKind",		/*�ƻ�����*/
		
		"dwPlanStatus",		/*�ƻ�״̬*/
		
		"szAcademicSubjectCode",		/*����ѧ�Ʊ���*/
	
		"dwTesteeKind",		/*ʵ�������*/
		
		"dwTotalTestHour",		/*��ʵ��ƻ���ѧʱ��*/
		
		"dwTeacherID",		/*��ʦ���ʺţ�*/
		
		"szTeacherName",		/*��ʦ����*/
	
		"dwCourseID",		/*�γ�ID*/
		
		"szCourseName",		/*�γ�����*/
	
		"dwTestItemID",		/*ʵ����ĿID*/
		
		"szConstraints",		/*Լ��������������Ҫ���豸�ȣ�*/
	
		"dwTestCardID",		/*ʵ����Ŀ��ID*/
		
		"szTestName",		/*ʵ������*/
	
		"szCategoryName",		/*�����(*/
	
		"dwGroupPeopleNum",		/*ÿ������*/
		
		"dwTestHour",		/*��ʵ����Ŀѧʱ��*/
		
		"dwTestClass",		/*ʵ�����*/
		
		"dwTestKind",		/*ʵ������*/
		
		"dwRequirement",		/*ʵ��Ҫ��*/
		
		"szTestItemURL",		/*��ϸ����*/
	
		"dwResvTestHour",		/*��ԤԼѧʱ��*/
		
		"dwDoneTestHour",		/*�����ѧʱ��*/
		
	"ResvInfo",		/*ԤԼ��ϸ��Ϣ*/
	
		"szMemo",		/*��ע*/
	 ""};

	/*�ύʵ�鱨��ģ��*/
	static public string[] REPORTFORMUPLOAD = new string[]{
		
		"dwTestItemID",		/*ʵ����ĿID*/
		
		"dwTeacherID",		/*��ʦ���ʺţ�*/
		
		"szReportFormURL",		/*ʵ�鱨��ģ��*/
	 ""};

	/*�ύʵ�鱨��*/
	static public string[] REPORTUPLOAD = new string[]{
		
		"dwSID",		/*��¼ID*/
		
		"dwAccNo",		/*�ʺ�*/
		
		"szReportURL",		/*�ύ��ʵ�鱨��*/
	 ""};

	/*����ʵ�鱨��*/
	static public string[] REPORTCORRECT = new string[]{
		
		"dwSID",		/*��¼ID*/
		
		"dwAccNo",		/*�ʺ�*/
		
		"dwTeacherID",		/*��ʦ���ʺţ�*/
		
		"dwReportScore",		/*ʵ�鱨������*/
		
		"szReportMarkInfo",		/*ʵ�鱨��������Ϣ*/
	 ""};

	/*�豸���*/
	static public string[] DEVGROUP = new string[]{
		
		"dwParentID",		/*������ID(����ʵ����ĿID)*/
		
		"dwDevKind",		/*�豸����*/
		
		"szDevName",		/*�豸����*/
	
		"dwDevNum",		/*�豸����*/
		
		"dwProperty",		/*�豸���ԣ�������ϵ�������ѡ����ѡ�ȣ�*/
		
		"szMemo",		/*��ע*/
	 ""};

	/*��ȡ����������*/
	static public string[] ACTIVITYPLANREQ = new string[]{
		
      "dwGetType",		/*��ʽ*/
		
		"szGetKey",		/*����ֵ*/
	
		"dwStartDate",		/*��ʼ����*/
		
		"dwKind",		/*���������*/
		
		"dwStatus",		/*�����״̬*/
		
		"dwOwner",		/*ԤԼ��(������)*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/**/
	static public string[] UNIACTIVITYPLAN = new string[]{
		
		"dwActivityPlanID",		/*�����ID*/
		
		"szActivityPlanName",		/*���������*/
	
		"szHostUnit",		/*���쵥λ*/
	
		"szOrganizer",		/*�а쵥λ*/
	
		"szPresenter",		/*������*/
	
		"szDesiredUser",		/*������Ҫ��*/
	
      "dwCheckRequirment",		/*���������Ҫ��*/
		
		"dwOwner",		/*ԤԼ��(������)*/
		
		"szContact",		/*��ϵ��*/
	
		"szTel",		/*��ϵ�绰*/
	
		"szHandPhone",		/*��ϵ�ֻ�*/
	
		"szEmail",		/*��ϵ��������*/
	
		"dwResvID",		/*ԤԼ��*/
		
		"dwGroupID",		/*��ID�����ڻ�ȡ���Ա��ϸ��*/
		
		"dwMaxUsers",		/*�����������*/
		
		"dwMinUsers",		/*������������*/
		
		"dwEnrollUsers",		/*����������*/
		
		"dwEnrollDeadline",		/*��������ֹ��*/
		
		"dwPublishDate",		/*��������*/
		
		"dwActivityDate",		/*�����*/
		
		"dwBeginTime",		/*��ʼʱ��(HHMM)*/
		
		"dwEndTime",		/*����ʱ��(HHMM)*/
		
		"szSite",		/*����ص�*/
	
		"dwDevID",		/*�ռ䣨����ص㣩ID*/
		
		"dwDevSN",		/*�豸���*/
		
		"szDevName",		/*�豸����*/
	
		"dwRoomID",		/*����ID*/
		
		"szRoomNo",		/*������*/
	
		"szRoomName",		/*��������*/
	
		"dwLabID",		/*ʵ����ID*/
		
		"szLabName",		/*ʵ��������*/
	
		"dwBuildingID",		/*¥��ID*/
		
		"szBuildingNo",		/*��¥���(*/
	
		"szBuildingName",		/*��¥����(*/
	
		"dwCampusID",		/*У��ID*/
		
		"szCampusName",		/*У������*/
	
      "dwKind",		/*���������*/
		
      "dwStatus",		/*�����״̬��ǰ8����������CHECKINFO����Ĺ���Ա���״̬)*/
		
		"szIntroInfo",		/*�����*/
	
		"szActivityPlanURL",		/*��ϸ����URL*/
	
		"szApplicationURL",		/*�ύ�������������*/
	
		"dwRealUsers",		/*ʵ������*/
		
		"szMemo",		/*��ע*/
	 ""};

	/*��ȡ����ŵ���λ�����*/
	static public string[] APSEATREQ = new string[]{
		
		"dwActivityPlanID",		/*�����ID*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*����ŵ���λ��Ϣ*/
	static public string[] APSEAT = new string[]{
		
		"dwActivityPlanID",		/*�����ID*/
		
		"dwDevID",		/*�豸ID*/
		
		"dwDevSN",		/*�豸���*/
		
		"szDevName",		/*�豸����*/
	
      "dwStatus",		/*��λ״̬*/
		
		"dwAccNo",		/*�˺�*/
		
		"szTrueName",		/*��Ա����*/
	
		"szMemo",		/*��ע*/
	 ""};

	/*����μӻ*/
	static public string[] ACTIVITYENROLL = new string[]{
		
		"dwActivityPlanID",		/*�����ID*/
		
		"dwDevID",		/*�豸ID*/
		
		"dwDevSN",		/*�豸���*/
		
		"dwAccNo",		/*�˺�*/
		
		"szTrueName",		/*��Ա����*/
	
		"szMemo",		/*��ע*/
	 ""};

	/*�˳������*/
	static public string[] ACTIVITYEXIT = new string[]{
		
		"dwActivityPlanID",		/*�����ID*/
		
		"dwAccNo",		/*�˺�*/
		
		"szTrueName",		/*��Ա����*/
	
		"szMemo",		/*��ע*/
	 ""};

	/*ǩ����Ա����*/
	static public string[] ASIGNUSER = new string[]{
		
		"dwCardID",		/*��ID��*/
		
		"dwInTime",		/*ǩ��ʱ��(HHMM)*/
		
      "dwRetStat",		/*����״̬*/
		
		"dwAccNo",		/*�˺�*/
		
		"szLogonName",		/*��¼��(ѧ����)*/
	
		"szCardNo",		/*����*/
	
		"szTrueName",		/*��Ա����*/
	
		"szMemo",		/*��ע*/
	 ""};

	/*ǩ����Ա����*/
	static public string[] AOFFLINESIGN = new string[]{
		
		"dwActivityPlanID",		/*�����ID*/
		
		"dwResvID",		/*ԤԼ��*/
		
	"SignUser",		/*ǩ����CUniTable[ASIGNUSER]*/
	
		"szMemo",		/*��ע*/
	 ""};

	/**/
	static public string[] RESVRULEREQ = new string[]{
		
		"dwRuleSN",		/*�豸������*/
		
		"dwDevClass",		/*�豸���0��ʾ�����ƣ�*/
		
		"dwDevKind",		/*�豸���ͣ�0��ʾ�����ƣ�*/
		
		"dwDevID",		/*�豸ID��0��ʾ�����ƣ�*/
		
		"dwResvPurpose",		/*ԤԼ��;*/
		
		"dwClassKind",		/*���(��UNIDEVCLS��Kind����)*/
		
		"dwExtValue",		/*��ͬԤԼ���Ͷ������չֵ��0��ʾ�����ƣ�*/
		 ""};

	/**/
	static public string[] RESVRULEADMINREQ = new string[]{
		
		"dwRuleSN",		/*�豸������*/
		
		"dwDevClass",		/*�豸���0��ʾ�����ƣ�*/
		
		"dwDevKind",		/*�豸���ͣ�0��ʾ�����ƣ�*/
		
		"dwDevID",		/*�豸ID��0��ʾ�����ƣ�*/
		
		"dwResvPurpose",		/*ԤԼ��;*/
		
		"dwClassKind",		/*���(��UNIDEVCLS��Kind����)*/
		
		"dwExtValue",		/*��ͬԤԼ���Ͷ������չֵ��0��ʾ�����ƣ�*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*��˱�*/
	static public string[] RULECHECKINFO = new string[]{
		
		"dwResvRuleSN",		/*�豸������*/
		
		"dwCheckKind",		/*������ͣ��½�ʱ��ϵͳ�Զ����䣩*/
		
		"dwBeforeKind",		/*��������������(�ɶ����*/
		
		"dwNeedMinTime",		/*�����Ҫ�����ʱ��*/
		
		"dwMapValue",		/*�������ֵ����ԤԼ������أ�*/
		
		"dwMainKind",		/*��˴���*/
		
		"szCheckName",		/*�������*/
	
		"dwCheckLevel",		/*��˼���(ͬUNIADMIN.dwManLevel���壩*/
		
		"dwDeptID",		/*���β���ID��ѧԺ�������ã������������Զ�ƥ�䣩*/
		
		"szDeptName",		/*���β���*/
	
      "dwProperty",		/*�������*/
		
		"szMemo",		/*״̬˵��*/
	 ""};

	/*�豸ʹ�ù���ṹ*/
	static public string[] UNIRESVRULE = new string[]{
		
		"dwRuleSN",		/*�豸������*/
		
		"szRuleName",		/*�豸��������*/
	
		"dwIdent",		/*��ݣ�0��ʾ�����ƣ�*/
		
		"dwDeptID",		/*���ţ�0��ʾ�����ƣ�*/
		
		"dwDevClass",		/*�豸���0��ʾ�����ƣ�*/
		
		"dwDevKind",		/*�豸���ͣ�0��ʾ�����ƣ�*/
		
		"dwDevID",		/*�豸ID��0��ʾ�����ƣ�*/
		
		"dwGroupID",		/*ָ���û��飨0��ʾ�����ƣ�*/
		
		"dwResvPurpose",		/*ԤԼ��;*/
		
		"dwExtValue",		/*��ͬԤԼ���Ͷ������չֵ��0��ʾ�����ƣ�*/
		
		"dwCreditRating",		/*���õȼ�*/
		
		"dwPriority",		/*���ȼ�(���ִ�������ȼ���)*/
		
      "dwLimit",		/*ԤԼ����*/
		
		"dwEarlyInTime",		/*������ǰ����ʱ��(����)*/
		
		"dwEarliestResvTime",		/*������ǰԤԼʱ��(����)�����ֱ������*/
		
		"dwLatestResvTime",		/*�����ǰԤԼʱ��(����)�����ֱ�����С*/
		
		"dwMinResvTime",		/*���ԤԼʱ��(����)*/
		
		"dwMaxResvTime",		/*�ԤԼʱ��(����)*/
		
		"dwResvEndNewTime",		/*��ǰԤԼ����ǰָ��ʱ��(����)�ڿ��½�ԤԼ*/
		
		"dwResvBeforeNoticeTime",		/*ԤԼ��Ч��ǰ֪ͨʱ��(����)*/
		
		"dwResvAfterNoticeTime",		/*ԤԼ��Ч����֪ͨʱ��(����)*/
		
		"dwResvEndNoticeTime",		/*ԤԼ������ǰ֪ͨʱ��(����)*/
		
		"dwSeriesTimeLimit",		/*����ԤԼʱ����(����)*/
		
		"dwTimeLimitForPurpose",		/*ʱ������ص�ԤԼ����(������λ�������������ң����޼�)*/
		
		"dwTimeConflictForPurpose",		/*ʱ���ͻ��ԤԼ����(������λ�������������ң����޼�ԤԼʱ�䲻���໥��ͻ)*/
		
		"dwLatestSensorTime",		/*����Ƶ�ԤԼ������ʱ��(����)*/
		
		"dwCancelTime",		/*ԤԼ�����Զ�ȡ��ԤԼʱ��(����)*/
		
		"dwMinUseRate",		/*Ҫ�����ʹ����(%)*/
		
      "dwFeeMode",		/*�շѷ�ʽ*/
		
		"dwMaxDevKind",		/*��ԤԼ�豸����*/
		
		"dwMaxDevNum",		/*��ԤԼ�豸��*/
		
		"szOtherCons",		/*����Լ��������ʱ�����ƺ���Ҫ���豸��,��ʽ��ר���ļ����壩*/
	
	"CheckTbl",		/*��˱�CUniTable[RULECHECKINFO]*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*��ȡ����ʵ�������*/
	static public string[] RESEARCHTESTREQ = new string[]{
		
		"dwRTID",		/*����ʵ��ID*/
		
		"dwHolderID",		/*�����ˣ��ʺţ�*/
		
		"dwMemberID",		/*��Ա���ʺţ�*/
		
		"dwLeaderID",		/*������ID*/
		
		"dwDeptID",		/*����ID*/
		
		"szRTName",		/*����ʵ������*/
	
		"dwRTLevel",		/*���м���*/
		
		"dwStatus",		/*״̬*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*����ʵ���Ա*/
	static public string[] RTMEMBER = new string[]{
		
		"dwRTID",		/*����ʵ��ID*/
		
		"dwGroupID",		/*��ID*/
		
		"dwStatus",		/*��Ա״̬(GROUPMEMBER����)*/
		
		"dwAccNo",		/*�˺�*/
		
		"szTrueName",		/*��Ա����*/
	 ""};

	/*����ʵ��*/
	static public string[] RESEARCHTEST = new string[]{
		
		"dwRTID",		/*����ʵ��ID*/
		
		"szRTSN",		/*����ʵ����*/
	
		"szRTName",		/*����ʵ������*/
	
		"szFromUnit",		/*�·���λ*/
	
      "dwRTKind",		/*��������*/
		
      "dwRTLevel",		/*���м���*/
		
		"dwBeginDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��ֹ����*/
		
		"dwHolderID",		/*�����ˣ��ʺţ�*/
		
		"szHolderName",		/*����������*/
	
		"dwLeaderID",		/*������ID*/
		
		"szLeaderName",		/*����������*/
	
		"dwDeptID",		/*����ID*/
		
		"szDeptName",		/*����*/
	
		"dwTestTimes",		/*ʵ�����*/
		
		"dwTestMinutes",		/*ʵ���ۼ�ʱ��*/
		
		"dwBalance",		/*�������*/
		
		"dwTotalFee",		/*�ۼƷ���*/
		
		"dwUnpayFee",		/*δ�������*/
		
		"dwGroupID",		/*��ID*/
		
		"dwGroupUsers",		/*���Ա����*/
		
      "dwStatus",		/*״̬��ǰ8����������CHECKINFO����Ĺ���Ա���״̬)*/
		
	"RTMembers",		/*��Ա��ϸ��(CUniTable<RTMEMBER>)*/
	
		"szOtherCons",		/*����Լ��������ʱ�����ƺ���Ҫ���豸��,��ʽ��ר���ļ����壩*/
	
		"szFundsNo",		/*���ѿ���ţ�����ö��Ÿ���)*/
	
		"szMemo",		/*��ע*/
	 ""};

	/*��ȡ��Ʒ��Ϣ�����*/
	static public string[] SAMPLEINFOREQ = new string[]{
		
		"dwSampleSN",		/*��Ʒ���*/
		
		"szSampleName",		/*��Ʒ����*/
	
		"dwSamStat",		/*��Ʒ״̬*/
		
		"dwDevID",		/*�豸ID����ȡĳ�豸ר�ã�*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*��Ʒ��Ϣ*/
	static public string[] SAMPLEINFO = new string[]{
		
		"dwSampleSN",		/*��Ʒ���*/
		
		"szSampleName",		/*��Ʒ����*/
	
		"szUnitName",		/*�Ʒѵ�λ*/
	
		"dwUnitFee1",		/*����1*/
		
		"dwUnitFee2",		/*����2*/
		
		"dwUnitFee3",		/*����3*/
		
		"dwUnitFee4",		/*����4*/
		
		"dwUnitFee5",		/*����5*/
		
      "dwSamStat",		/*��Ʒ״̬*/
		
		"dwDevID",		/*�豸ID����һ�豸ר�ø�ֵ�豸ID��ͨ��Ϊ0��*/
		
		"szMemo",		/*��ע*/
	 ""};

	/*��ȡ����ԤԼ�������*/
	static public string[] YARDRESVREQ = new string[]{
		
		"dwResvID",		/*ԤԼ��*/
		
		"dwApplicantID",		/*�������˺�*/
		
		"dwDevID",		/*�豸ID*/
		
		"dwDevKind",		/*�豸����*/
		
		"dwClassKind",		/*���(��UNIDEVCLS��Kind����)*/
		
		"dwClassID",		/*�豸���ID*/
		
		"dwCheckStat",		/*ȷ��״̬*/
		
		"dwUnNeedStat",		/*������״̬*/
		
		"dwBeginDate",		/*ԤԼ��ʼ����*/
		
		"dwEndDate",		/*ԤԼ��������*/
		
		"dwResvGroupID",		/*ԤԼ��ID*/
		
		"dwStatFlag",		/*״̬��־*/
		
		"szLabIDs",		/*ʵ����ID,����ö��Ÿ���*/
	
		"szRoomIDs",		/*����ID,����ö��Ÿ���*/
	
		"szBuildingIDs",		/*¥��ID,����ö��Ÿ���*/
	
		"szCampusIDs",		/*У��ID,����ö��Ÿ���*/
	
		"dwActivitySN",		/*����ͱ��*/
		
		"dwProperty",		/*����*/
		
		"dwUnNeedProperty",		/*����Ҫ����*/
		
		"szResvName",		/*ʹ����;����*/
	
      "dwReqProp",		/*���󸽼�����*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*����ԤԼ*/
	static public string[] YARDRESV = new string[]{
		
		"dwResvID",		/*ԤԼ��*/
		
		"dwResvGroupID",		/*��ID(��ʱ��ԤԼ��ID��ͬ������ΪԤԼID)*/
		
		"szResvName",		/*ʹ����;����*/
	
		"dwPurpose",		/*ԤԼ��;*/
		
		"dwProperty",		/*ԤԼ���ԣ�������Ҫ����*/
		
		"dwActivitySN",		/*����ͱ��*/
		
		"szActivityName",		/*���������*/
	
		"szOrganization",		/*��֯*/
	
		"szOrganiger",		/*��֯��*/
	
		"szHostUnit",		/*���쵥λ*/
	
		"szPresenter",		/*������*/
	
		"szDesiredUser",		/*������Ҫ��*/
	
		"szContact",		/*��ϵ��*/
	
		"szTel",		/*��ϵ�绰*/
	
		"szHandPhone",		/*��ϵ�ֻ�*/
	
		"szEmail",		/*��ϵ��������*/
	
		"dwKind",		/*����*/
		
		"szIntroInfo",		/*�����*/
	
		"szCycRule",		/*ԤԼʱ���������*/
	
		"dwActivityLevel",		/*����𣨺͹���Ա����һ�£�*/
		
		"dwCheckKinds",		/*�������(�ο�CHECKTYPE���壬�ɶ����*/
		
		"dwSecurityLevel",		/*�������𣨲ο�CHECKTYPE�������Ƿ��ύ��������ˣ�*/
		
		"dwMinAttendance",		/*���ٲμ�������Ԥ����*/
		
		"dwMaxAttendance",		/*���μ�������Ԥ����*/
		
		"dwStatus",		/*ԤԼ״̬(������飬�Ƿ���Ч���Ƿ���ȡ����)*/
		
		"dwPreDate",		/*ԤԼ��ʼ����*/
		
		"dwOccurTime",		/*ԤԼ����ʱ��*/
		
		"dwBeginTime",		/*ԤԼ��ʼʱ��*/
		
		"dwEndTime",		/*ԤԼ����ʱ��*/
		
		"dwCheckTime",		/*���ʱ��(���½�ԤԼָ��RESVPROP_BYTHIRDʱ����ʾdwThirdResvID)*/
		
		"dwAdvanceCheckTime",		/*��ǰ���ʱ��*/
		
		"dwDevID",		/*�豸ID*/
		
		"dwDevKind",		/*�豸����*/
		
		"dwDevSN",		/*�豸���*/
		
		"szDevName",		/*�豸����*/
	
		"dwRoomID",		/*����ID*/
		
		"szRoomName",		/*��������*/
	
		"dwLabID",		/*ʵ����ID*/
		
		"szLabName",		/*ʵ��������*/
	
		"dwBuildingID",		/*¥��ID*/
		
		"szBuildingNo",		/*��¥���(*/
	
		"szBuildingName",		/*��¥����(*/
	
		"dwCampusID",		/*У��ID*/
		
		"szCampusName",		/*У������*/
	
		"dwDeptID",		/*������������ID*/
		
		"szDeptName",		/*����������������*/
	
		"dwApplicantID",		/*�������˺�*/
		
		"szApplicantName",		/*����������*/
	
		"dwUserDeptID",		/*�����˲���ID*/
		
		"szUserDeptName",		/*�����˲���*/
	
		"dwResvRuleSN",		/*����ԤԼ����*/
		
		"dwOpenRuleSN",		/*��������ʱ���*/
		
		"dwFeeSN",		/*����SN*/
		
		"szApplicationURL",		/*�ύ�������������*/
	
		"szSpareDevIDs",		/*��ѡ�豸ID��������Ÿ�����*/
	
		"szMemo",		/*˵����Ϣ*/
	
		"dwFeedStat",		/*״̬*/
		
		"dwFeedKind",		/*��������*/
		
		"dwScore",		/*�û�����*/
		
		"szFeedInfo",		/*������Ϣ*/
	
		"szReplyInfo",		/*�ظ���Ϣ*/
	 ""};

	/*��ȡ����ԤԼ�����Ϣ�������*/
	static public string[] YARDRESVCHECKINFOREQ = new string[]{
		
		"dwCheckID",		/*���ID*/
		
		"dwResvID",		/*ԤԼ��*/
		
		"dwResvGroupID",		/*ԤԼ��ID*/
		
		"szResvName",		/*ԤԼ����*/
	
		"dwCheckDeptID",		/*��˲���ID*/
		
		"dwApplicantID",		/*�������˺�*/
		
		"dwCheckStat",		/*ȷ��״̬*/
		
		"dwNeedYardResv",		/*��Ҫ��ȡ����ԤԼ����*/
		
		"dwBeginDate",		/*ԤԼ��ʼ����*/
		
		"dwEndDate",		/*ԤԼ��������*/
		
		"dwKind",		/*����ԤԼ����*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*����ԤԼ�����Ϣ*/
	static public string[] YARDRESVCHECKINFO = new string[]{
		
		"dwCheckID",		/*���ID*/
		
		"dwResvID",		/*ԤԼ��*/
		
		"dwCheckKind",		/*�������*/
		
		"dwCheckLevel",		/*��˼���(ͬUNIADMIN.dwManLevel���壩*/
		
		"dwCheckDeptID",		/*��˲���ID*/
		
		"dwWaitKind",		/*�ȴ�������(�ɶ����*/
		
		"szCheckName",		/*�������*/
	
		"dwBeforeKind",		/*��������������(�ɶ����*/
		
		"dwNeedMinTime",		/*�����Ҫ�����ʱ��*/
		
		"dwCheckStat",		/*����Ա���״̬(������ADMINCHECK)*/
		
		"szCheckDetail",		/*���˵��*/
	
		"dwCheckBeginDate",		/*��˿�ʼ����*/
		
		"dwCheckDeadLine",		/*��˽�ֹ����*/
		
		"dwCheckDate",		/*�������*/
		
		"dwCheckTime",		/*���ʱ��*/
		
		"dwAdminID",		/*������ʺ�*/
		
		"szAdminName",		/*�����*/
	
		"dwApplicantID",		/*�������˺�*/
		
		"szApplicantName",		/*����������*/
	
	"YardResv",		/*CUniStruct[YARDRESV]*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*����ԤԼ���*/
	static public string[] YARDRESVCHECK = new string[]{
		
		"dwCheckID",		/*���ID*/
		
		"dwResvID",		/*ԤԼ��*/
		
		"dwCheckKind",		/*�������*/
		
		"dwCheckStat",		/*����Ա���״̬(������ADMINCHECK)*/
		
		"szCheckDetail",		/*���˵��*/
	
	"YardResv",		/*CUniStruct[YARDRESV]*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*��ȡԤԼ�����Ϣ�������*/
	static public string[] RESVCHECKINFOREQ = new string[]{
		
		"dwCheckID",		/*���ID*/
		
		"dwResvID",		/*ԤԼ��*/
		
		"dwCheckDeptID",		/*��˲���ID*/
		
		"dwApplicantID",		/*�������˺�*/
		
		"dwCheckStat",		/*ȷ��״̬*/
		
		"dwNeedResv",		/*��Ҫ��ȡԤԼ����*/
		
		"dwBeginDate",		/*ԤԼ��ʼ����*/
		
		"dwEndDate",		/*ԤԼ��������*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*ԤԼ�����Ϣ*/
	static public string[] RESVCHECKINFO = new string[]{
		
		"dwCheckID",		/*���ID*/
		
		"dwResvID",		/*ԤԼ��*/
		
		"dwCheckKind",		/*�������*/
		
		"dwCheckLevel",		/*��˼���(ͬUNIADMIN.dwManLevel���壩*/
		
		"dwCheckDeptID",		/*��˲���ID*/
		
		"dwWaitKind",		/*�ȴ�������(�ɶ����*/
		
		"szCheckName",		/*�������*/
	
		"dwBeforeKind",		/*��������������(�ɶ����*/
		
		"dwNeedMinTime",		/*�����Ҫ�����ʱ��*/
		
		"dwCheckStat",		/*����Ա���״̬(������ADMINCHECK)*/
		
		"szCheckDetail",		/*���˵��*/
	
		"dwCheckBeginDate",		/*��˿�ʼ����*/
		
		"dwCheckDeadLine",		/*��˽�ֹ����*/
		
		"dwCheckDate",		/*�������*/
		
		"dwCheckTime",		/*���ʱ��*/
		
		"dwAdminID",		/*������ʺ�*/
		
		"szAdminName",		/*�����*/
	
		"dwApplicantID",		/*�������˺�*/
		
		"szApplicantName",		/*����������*/
	
	"ResvInfo",		/*CUniStruct[UNIRESERVE]*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*ԤԼ���*/
	static public string[] RESVCHECK = new string[]{
		
		"dwCheckID",		/*���ID*/
		
		"dwResvID",		/*ԤԼ��*/
		
		"dwCheckKind",		/*�������*/
		
		"dwCheckStat",		/*����Ա���״̬(������ADMINCHECK)*/
		
		"szCheckDetail",		/*���˵��*/
	
	"ResvInfo",		/*CUniStruct[UNIRESERVE]*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*��ȡ���ݻ�������*/
	static public string[] YARDACTIVITYREQ = new string[]{
		
		"dwActivitySN",		/*����ͱ��*/
		
		"dwActivityLevel",		/*����𣨺͹���Ա����һ�£�*/
		
		"dwCheckKinds",		/*�������(�ο�CHECKTYPE���壬�ɶ����*/
		
		"dwSecurityLevel",		/*�������𣨲ο�CHECKTYPE�������Ƿ��ύ��������ˣ�*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*���ݻ�����豸����*/
	static public string[] YADEVKIND = new string[]{
		
		"dwActivitySN",		/*����ͱ��*/
		
		"dwKindID",		/*�豸����ID*/
		 ""};

	/*���ݻ*/
	static public string[] YARDACTIVITY = new string[]{
		
		"dwActivitySN",		/*����ͱ��*/
		
		"szActivityName",		/*���������*/
	
		"dwActivityLevel",		/*����𣨺͹���Ա����һ�£�*/
		
		"dwCheckKinds",		/*�������(�ο�CHECKTYPE���壬�ɶ����*/
		
		"szCheckNames",		/*�����������,����ö��Ÿ���*/
	
      "dwSecurityLevel",		/*�������𣨲ο�CHECKTYPE�������Ƿ��ύ��������ˣ�*/
		
	"UsableDevKind",		/*CUniTable[YADEVKIND]*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*������ԤԼ�豸*/
	static public string[] TRESVDEV = new string[]{
		
		"szAssertSN",		/*�ʲ����*/
	 ""};

	/*������ԤԼʱ���*/
	static public string[] TRESVTIME = new string[]{
		
		"dwResvDate",		/*ԤԼ����*/
		
		"dwStartHM",		/*ԤԼ��ʼʱ��*/
		
		"dwEndHM",		/*ԤԼ����ʱ��*/
		 ""};

	/*������ԤԼ�����豸*/
	static public string[] THIRDRESVSHAREDEV = new string[]{
		
		"dwThirdResvID",		/*������ԤԼID*/
		
		"szResvTitle",		/*ԤԼ����*/
	
	"DevTbl",		/*ԤԼ�豸��*/
	
	"TimeTbl",		/*ԤԼʱ���*/
	 ""};

	/*������ɾ��ԤԼ*/
	static public string[] THIRDRESVDEL = new string[]{
		
		"dwThirdResvID",		/*������ԤԼID*/
		 ""};

	/*��ȡ������ԤԼ�������*/
	static public string[] THIRDRESVREQ = new string[]{
		
		"dwThirdResvID",		/*������ԤԼID*/
		
		"szPID",		/*ѧ����*/
	
		"dwStatus",		/*״̬*/
		
		"dwBeginDate",		/*ԤԼ��ʼ����*/
		
		"dwEndDate",		/*ԤԼ��������*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*������ԤԼ*/
	static public string[] THIRDRESV = new string[]{
		
		"dwThirdResvID",		/*������ԤԼID*/
		
		"szResvTitle",		/*ԤԼ����*/
	
		"dwResvDate",		/*ԤԼ����*/
		
		"dwStartHM",		/*ԤԼ��ʼʱ��*/
		
		"dwEndHM",		/*ԤԼ����ʱ��*/
		
		"szOrganization",		/*��֯*/
	
		"szOrganiger",		/*��֯��*/
	
		"szHostUnit",		/*���쵥λ*/
	
		"szPresenter",		/*������*/
	
		"szDesiredUser",		/*������Ҫ��*/
	
		"szIntroInfo",		/*�����*/
	
		"szPID",		/*������ѧ����*/
	
		"szTrueName",		/*����������*/
	
		"szTel",		/*��ϵ�绰*/
	
		"szHandPhone",		/*��ϵ�ֻ�*/
	
		"szEmail",		/*��ϵ��������*/
	
		"dwMinAttendance",		/*���ٲμ�������Ԥ����*/
		
		"dwMaxAttendance",		/*���μ�������Ԥ����*/
		
		"dwStatus",		/*ԤԼ״̬((0��ʾδԤԼ��������飬�Ƿ���Ч���Ƿ���ȡ����)*/
		
		"szAssertSN",		/*�ʲ����*/
	
		"dwResvID",		/*ԤԼ��*/
		
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*�豸ԤԼ��Ϣ��*/
	static public string[] DEVICERESV = new string[]{
		
		"dwDevID",		/*�豸ID*/
		
      "dwResvFrom",		/*��Դ*/
		
		"dwResvID",		/*ԤԼID*/
		
		"dwResvDate",		/*ԤԼ����*/
		
		"dwStartHM",		/*ԤԼ��ʼʱ��*/
		
		"dwEndHM",		/*ԤԼ����ʱ��*/
		
		"dwResvMin",		/*ԤԼʱ��*/
		
		"dwAccNo",		/*ԤԼ���ʺ�*/
		
		"dwSex",		/*ԤԼ���Ա�*/
		
		"szPID",		/*������ѧ����*/
	
		"szTrueName",		/*����������*/
	
		"szMemberName",		/*����*/
	
		"szResvTitle",		/*ԤԼ����*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

}
	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/
public partial class Struct_ST
{

	/*��ȡ��ط����������*/
	static public string[] CTRLCLASSREQ = new string[]{
		
		"dwCtrlSN",		/*��ط������*/
		
		"dwCtrlKind",		/*���Ʒ���*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*��ط����ṹ*/
	static public string[] UNICTRLCLASS = new string[]{
		
		"dwCtrlSN",		/*��ط������*/
		
      "dwCtrlKind",		/*���Ʒ���*/
		
		"dwCtrlLevel",		/*���Ʒ��༶�𣬿��Զ���*/
		
		"szCtrlName",		/*��ط��������*/
	
      "dwCtrlMode",		/*���Ʒ�ʽ*/
		
		"dwForAges",		/*���������(FFTT 0713��ʾ7-13����)*/
		
		"szMemo",		/*��ע*/
	 ""};

	/*��ȡ��ַ��������*/
	static public string[] CTRLURLREQ = new string[]{
		
		"dwCtrlLevel",		/*���Ʒ��༶�𣬿��Զ���*/
		
		"dwClassSN",		/*��ط������*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*��ַ��ṹ*/
	static public string[] UNICTRLURL = new string[]{
		
		"dwClassSN",		/*��ط������*/
		
		"dwCtrlLevel",		/*���Ʒ��༶�𣬿��Զ���*/
		
		"szCtrlName",		/*��ַ������*/
	
		"dwCtrlMode",		/*���Ʒ�ʽ*/
		
		"dwForAges",		/*���������(FFTT 0713��ʾ7-13����)*/
		
		"dwID",		/*��ַID*/
		
		"szURL",		/*URL(֧��ͨ���)*/
	
		"dwPort",		/*�˿�*/
		
      "dwStatus",		/*״̬*/
		
		"szMemo",		/*��ע*/
	 ""};

	/*��ȡ�����������*/
	static public string[] CTRLSWREQ = new string[]{
		
		"dwCtrlLevel",		/*���Ʒ��༶�𣬿��Զ���*/
		
		"dwClassSN",		/*��ط������*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*�����ṹ*/
	static public string[] UNICTRLSW = new string[]{
		
		"dwClassSN",		/*��ط������*/
		
		"dwCtrlLevel",		/*���Ʒ��༶�𣬿��Զ���*/
		
		"szCtrlName",		/*���������*/
	
		"dwCtrlMode",		/*���Ʒ�ʽ*/
		
		"dwForAges",		/*���������(FFTT 0713��ʾ7-13����)*/
		
		"dwID",		/*key*/
		
		"szName",		/*������Ա����*/
	
		"dwMemberID",		/*���ݲ�ͬ����ʾ��ͬ����*/
		
      "dwKind",		/*��Ա����*/
		
      "dwStatus",		/*״̬*/
		
		"szMemo",		/*��ע*/
	 ""};

	/*��ȡ����������*/
	static public string[] SOFTWAREREQ = new string[]{
		
      "dwGetType",		/*��ʽ*/
		
		"szGetKey",		/*����ֵ*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*����ṹ*/
	static public string[] UNISOFTWARE = new string[]{
		
		"dwSWID",		/*���ID*/
		
      "dwKind",		/*��Ա����*/
		
		"szSWName",		/*�������*/
	
		"szSWVersion",		/*����汾*/
	
		"szSWCompany",		/*��˾*/
	
		"szDispSWName",		/*��ʾ��Ʒ���ƣ����޸�*/
	
		"szDispSWCompany",		/*��ʾ��˾���ƣ����޸�*/
	
		"dwFrom",		/*�������ͬ����Σ�*/
		
		"dwChgFlag",		/*�޸ĸ��±�־*/
		
		"szMemo",		/*��ע*/
	 ""};

	/*��ȡ����������*/
	static public string[] PROGRAMREQ = new string[]{
		
      "dwGetType",		/*��ʽ*/
		
		"szGetKey",		/*����ֵ*/
	
		"dwKind",		/*��Ա����*/
		
		"dwProperty",		/*��������*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*����ṹ*/
	static public string[] UNIPROGRAM = new string[]{
		
		"dwID",		/*����ID*/
		
		"dwSubID",		/*��ID*/
		
		"dwSWID",		/*�������ID*/
		
		"dwKind",		/*�������*/
		
      "dwProperty",		/*��������*/
		
		"szProductName",		/*��Ʒ����*/
	
		"szExeName",		/*Exe�ļ���*/
	
		"szSWVersion",		/*����汾*/
	
		"szDispProductName",		/*��ʾ�������ƣ����޸�*/
	
		"szDispSWName",		/*��ʾ��Ʒ���ƣ����޸�*/
	
		"szDispSWCompany",		/*��ʾ��˾���ƣ����޸�*/
	
		"dwFrom",		/*�������ͬ����Σ�*/
		
		"dwChgFlag",		/*�޸ĸ��±�־*/
		
		"szIcon",		/*ͼ��*/
	
		"szMemo",		/*��ע*/
	 ""};

	/*��ȡ��������������*/
	static public string[] PCSWINFOREQ = new string[]{
		
      "dwGetType",		/*��ʽ*/
		
		"szGetKey",		/*����ֵ*/
	
		"dwKind",		/*�������*/
		
		"dwProperty",		/*��������*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*��������ṹ*/
	static public string[] UNIPCSWINFO = new string[]{
		
		"szProgramInfo",		/*CUniStruct(<UNIPROGRAM>)*/
	
		"dwPCID",		/*����ID*/
		
		"szInstName",		/*��װ����*/
	
		"szInstPath",		/*��װ·��*/
	
		"dwRunLatestDate",		/*�����������*/
		
		"dwRunTimes",		/*���д���*/
		
		"dwRunMinutes",		/*�ۼ����з�����*/
		
		"szMemo",		/*��ע*/
	 ""};

	/*��ȡ��������������*/
	static public string[] ROOMSWINFOREQ = new string[]{
		
      "dwGetType",		/*��ʽ*/
		
		"szGetKey",		/*����ֵ*/
	
		"dwKind",		/*�������*/
		
		"dwProperty",		/*��������*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*��������ṹ*/
	static public string[] UNIROOMSWINFO = new string[]{
		
		"szProgramInfo",		/*CUniStruct(<UNIPROGRAM>)*/
	
		"dwRoomID",		/*����ID*/
		
		"dwInstSWNum",		/*��װ�����������*/
		
		"dwRunTimes",		/*���д���*/
		
		"dwRunMinutes",		/*�ۼ����з�����*/
		
		"szMemo",		/*��ע*/
	 ""};

	/*��������ṹ���ϴ��ã�*/
	static public string[] PCPROGRAM = new string[]{
		
		"szProgramInfo",		/*CUniStruct(<UNIPROGRAM>)*/
	
		"dwDevID",		/*�ͻ����豸��ID��*/
		
		"dwLabID",		/*ʵ���ҵ�ID��*/
		
		"dwPID",		/*����ID*/
		
		"szInstName",		/*��װ����*/
	
		"szInstPath",		/*��װ·��*/
	
		"szMemo",		/*��ע*/
	 ""};

	/*�������������Ϣ���ϴ��ã�*/
	static public string[] PROGEND = new string[]{
		
		"dwDevID",		/*�ͻ����豸��ID��*/
		
		"dwLabID",		/*ʵ���ҵ�ID��*/
		
		"dwProcID",		/*�����ID��*/
		
		"dwPID",		/*����ID*/
		 ""};

	/*�˳�������Ϣ*/
	static public string[] QUITAPPINFO = new string[]{
		
		"dwProcID",		/*����ID*/
		
		"szMemo",		/*��ע*/
	 ""};

	/*��ַ��Ϣ*/
	static public string[] URLCHECKINFO = new string[]{
		
		"dwDevID",		/*�ͻ����豸��ID��*/
		
		"dwLabID",		/*ʵ���ҵ�ID��*/
		
		"dwAccNo",		/*�ʺ�*/
		
		"dwRemoteIp",		/*����IP*/
		
		"dwPort",		/*���ʶ˿�*/
		
		"szDomainName",		/*����*/
	
		"szURL",		/*��ַ*/
	
		"szMemo",		/*��ע*/
	 ""};

}
	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/
public partial class Struct_ST
{

	/*��¼�����*/
	static public string[] THIRDLOGINREQ = new string[]{
		
		"dwSysID",		/*ϵͳ���*/
		
		"szVersion",		/*�汾*/
	
		"szExtInfo",		/*��չ��Ϣ*/
	 ""};

	/*��¼Ӧ���*/
	static public string[] THIRDLOGINRES = new string[]{
		
		"szVersion",		/*�汾*/
	
		"szExtInfo",		/*��չ��Ϣ*/
	 ""};

	/*��ȡ�˻��б��������*/
	static public string[] THIRDACCREQ = new string[]{
		
      "dwGetType",		/*��ʽ*/
		
		"szParam",		/*����*/
	 ""};

	/*ͬ���ʻ������*/
	static public string[] SYNCACCREQ = new string[]{
		
      "dwType",		/*ͬ����ʽ*/
		
		"szMemo",		/*��չ��Ϣ*/
	 ""};

	/*��ȡ�����ʻ���Ϣ״̬*/
	static public string[] SYNCACCINFO = new string[]{
		
      "dwStatus",		/*��ǰ״̬*/
		
		"dwStartTime",		/*��ʼʱ��(time����)*/
		
		"dwUseTime",		/*����ʱ��(��)*/
		
		"dwEstmateTime",		/*����������ʱ��(��)*/
		
		"dwTotalAcc",		/*���û���*/
		
		"dwDealAcc",		/*�Ѵ�������û���*/
		
		"dwDiffAcc",		/*��Ϣ�뱾�ز�ͬ�û���*/
		
		"szInfo",		/*��չ��Ϣ*/
	 ""};

}
	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/
public partial class Struct_ST
{

	/**/
	static public string[] PERIODFEE = new string[]{
		
		"dwPStart",		/*ʱ�ο�ʼʱ��*/
		
		"dwPEnd",		/*ʱ�ν���ʱ��*/
		
		"dwPUnitFee",		/*ʱ�ε�λ����*/
		
		"dwPAssistFee",		/*ʱ�ι���Աָ����*/
		 ""};

	/**/
	static public string[] FEEREQ = new string[]{
		
		"dwFeeSN",		/*����SN*/
		
		"dwIdent",		/*��ݣ�0��ʾ�����ƣ�*/
		
		"dwDeptID",		/*���ţ�0��ʾ�����ƣ�*/
		
		"dwDevKind",		/*�豸���ͣ�0��ʾ�����ƣ�*/
		
		"dwGroupID",		/*ָ���û��飨0��ʾ�����ƣ�*/
		
		"dwPurpose",		/*��;*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/**/
	static public string[] RTDEVFEEREQ = new string[]{
		
		"dwRTID",		/*����ʵ����ĿID*/
		
		"dwDevID",		/*�豸ID*/
		 ""};

	/*�շѱ�׼��ϸ��Ϣ*/
	static public string[] FEEDETAIL = new string[]{
		
      "dwFeeType",		/*�շ����*/
		
		"dwUsablePayKind",		/*���ýɷѷ�ʽ(��UNIBILL����)*/
		
		"dwDefaultCheckStat",		/*CHECKINFO����Ĺ���Ա���״̬*/
		
		"dwUnitFee",		/*��λʹ�÷���(Сʱ ȱʡ100)*/
		
		"dwUnitTime",		/*��λʱ��(ȱʡ1)*/
		
		"dwRoundOff",		/*����ֽ��(С�ڵ�λʱ��)*/
		
		"dwIgnoreTime",		/*���Ʒ�ʱ��(ȱʡ0,����ʾ���Ʒ�ʱ�䣬����ʾ����ʹ��ʱ��)*/
		
		"dwHolidayCoef",		/*����ϵ��*/
		
		"szPosInfo",		/*��һ��ͨ��Ӧ���̻���Ϣ*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*�շѱ�׼��ϸ��Ϣ*/
	static public string[] UNIFEE = new string[]{
		
		"dwFeeSN",		/*����SN*/
		
		"szFeeName",		/*����*/
	
		"dwPriority",		/*���ȼ�(���ִ�������ȼ���)*/
		
		"dwIdent",		/*��ݣ�0��ʾ�����ƣ�*/
		
		"dwDeptID",		/*���ţ�0��ʾ�����ƣ�*/
		
		"dwDevKind",		/*�豸���ͣ�0��ʾ�����ƣ�*/
		
		"dwGroupID",		/*ָ���û��飨0��ʾ�����ƣ�*/
		
		"dwPurpose",		/*ԤԼ��;*/
		
		"dwOverDraft",		/*����͸֧��*/
		
		"dwMinInTime",		/*���������û���Ϳ���ʱ��*/
		
      "dwQuotaRule",		/*���ƹ���(���ۼƣ����ۼƣ�����æ��(ȱʡ0))*/
		
		"dwQuotaTime",		/*����ʹ��ʱ��(ȱʡ-1)*/
		
	"szFeeDetail",		/*�շѱ�׼��ϸ��CUniTable[FEEDETAIL]*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/**/
	static public string[] RTDEVSAMPLEREQ = new string[]{
		
		"dwRTID",		/*����ʵ����ĿID*/
		
		"dwDevID",		/*�豸ID*/
		 ""};

	/*������Ŀ��Ӧ���豸����Ʒ�����ʱ�*/
	static public string[] RTDEVSAMPLE = new string[]{
		
		"dwDevID",		/*�豸ID*/
		
		"dwSampleSN",		/*��Ʒ���*/
		
		"szSampleName",		/*��Ʒ����*/
	
		"szUnitName",		/*�Ʒѵ�λ*/
	
		"dwUnitFee",		/*����*/
		
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*��ȡ�˵�����*/
	static public string[] BILLREQ = new string[]{
		
      "dwGetType",		/*��ʽ*/
		
		"szGetKey",		/*����ֵ*/
	
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
		"dwFeeType",		/*�շ����(FEEDETAIL����)*/
		
		"dwPayKind",		/*�ɷѷ�ʽ*/
		
		"dwStatus",		/*״̬*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*�û��˵�*/
	static public string[] UNIBILL = new string[]{
		
		"dwSID",		/*��ˮ��*/
		
		"dwResvID",		/*ԤԼ��*/
		
		"dwCostSID",		/*ʹ����ˮ��*/
		
		"szPosInfo",		/*��һ��ͨ��Ӧ���̻���Ϣ*/
	
		"dwAccNo",		/*�ʺ�*/
		
		"szPID",		/*ѧ����*/
	
		"szTrueName",		/*����*/
	
		"dwDevKind",		/*�豸����*/
		
		"szKindName",		/*�豸����*/
	
		"szModel",		/*�豸�ͺ�*/
	
		"szSpecification",		/*�豸���*/
	
		"dwFeeType",		/*�շ����(FEEDETAIL����)*/
		
		"dwBeginTime",		/*��ʼʱ��*/
		
		"dwEndTime",		/*����ʱ��*/
		
		"dwUnitFee",		/*����*/
		
		"dwUnitTime",		/*��λʱ��*/
		
		"dwRoundOff",		/*����ֽ��(С�ڵ�λʱ��)*/
		
		"dwIgnoreTime",		/*���Ʒ�ʱ��*/
		
		"dwHolidayCoef",		/*����ϵ��*/
		
		"dwUseTime",		/*ʹ��ʱ��*/
		
		"dwFeeTime",		/*�Ʒ�ʱ��*/
		
		"dwCostMoney",		/*Ӧ�ɷ���*/
		
		"dwCostSubsidy",		/*����*/
		
		"dwCostFreeTime",		/*��ʱ*/
		
		"dwRealCost",		/*ʵ�ʽ��ɷ���*/
		
      "dwUsablePayKind",		/*���ýɷѷ�ʽ*/
		
		"dwUsedPayKind",		/*ʵ�ʽɷѷ�ʽ*/
		
      "dwStatus",		/*CHECKINFO����Ĺ���Ա���״̬+���¶���*/
		
		"dwBillDate",		/*�˵�����*/
		
		"dwBillTime",		/*�˵�ʱ��*/
		
		"dwAuditorID",		/*���Ա*/
		
		"dwTollID",		/*�շ�Ա��һ��ͨtblThirdSyncCost����ˮ��*/
		
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*�˵��ɷ�*/
	static public string[] BILLPAY = new string[]{
		
		"dwPayKind",		/*�ɷѷ�ʽ*/
		
		"dwTotalCost",		/*�ɷѺϼ�*/
		
		"dwOneCardSID",		/*һ��ͨ��ˮ��*/
		
		"szCardCostInfo",		/*��ͨ���۷���Ϣ����ͬ��һ��ͨ��ʽ�����ݶ���ͬ*/
	
	"szBillInfo",		/*CUniTable[UNIBILL]*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*��ʱʹ�ù�������*/
	static public string[] FTRULEREQ = new string[]{
		
      "dwGetType",		/*��ʽ*/
		
		"szGetKey",		/*����ֵ*/
	
		"szSubKey",		/*ѡ��רҵ��ʽʱ������Ϊ��ѧ���*/
	 ""};

	/*��ʱʹ�ù���*/
	static public string[] FREETIMERULE = new string[]{
		
		"dwSN",		/*��ʱ����SN*/
		
		"szName",		/*����*/
	
		"dwFTType",		/*��ʱ���*/
		
		"dwMajorID",		/*רҵ*/
		
		"szMajorName",		/*רҵ����*/
	
		"dwEnrolYear",		/*��ѧ���*/
		
      "dwPeriod",		/*���ڣ�ѧ�ڣ�ѧ�꣬������ѧ�ڼ䣩*/
		
		"dwPlanFT",		/*�ƻ���ʱ��*/
		
		"dwDayLimit",		/*ÿ��ʹ���޶�*/
		
		"dwPlanUseTimes",		/*�ƻ���ʹ�ô���*/
		
		"szMemo",		/*˵����Ϣ*/
	 ""};

}
	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/
public partial class Struct_ST
{

	/*��ȡ����̨�������*/
	static public string[] CONREQ = new string[]{
		
		"dwConsoleSN",		/*����̨���*/
		
		"szConsoleName",		/*����̨����*/
	
		"dwKind",		/*����̨����*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*����̨��Ϣ*/
	static public string[] UNICONSOLE = new string[]{
		
		"dwConsoleSN",		/*����̨���*/
		
		"szConsoleName",		/*����̨����*/
	
      "dwKind",		/*����̨����*/
		
		"dwStatus",		/*����̨״̬���ο�CommonIF.xmlCONSTAT_XXX����)*/
		
		"dwOpenTime",		/*��ʼʱ��*/
		
		"dwCloseTime",		/*�ر�ʱ��*/
		
		"szIP",		/*IP��ַ*/
	
		"szManRooms",		/*������(�����ţ��ɶ�������Ÿ���)*/
	
		"szDispInfoURL",		/*��ʾ��Ϣ����*/
	
		"szLocation",		/*����̨���λ��*/
	
		"szMemo",		/*˵����Ϣ*/
	
	"MoniInfo",		/*�����Ϣ*/
	 ""};

	/*����̨��¼����*/
	static public string[] CONLOGINREQ = new string[]{
		
		"szVersion",		/*�汾	XX.XX.XXXXXXXX*/
	
		"dwStaSN",		/*վ����*/
		
		"szIP",		/*IP��ַ*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*����̨��¼��Ӧ*/
	static public string[] CONLOGINRES = new string[]{
		
		"dwSessionID",		/*�����������SessionID*/
		
	"SrvVer",		/*UNIVERSION �ṹ*/
	
		"szCurTime",		/*������ʱ�� YYYY-MM-DD HH:MM:SS*/
	
		"dwConsoleSN",		/*����̨���*/
		
		"szConsoleName",		/*����̨����*/
	
		"dwKind",		/*����̨����*/
		
		"dwOpenTime",		/*����ʱ��*/
		
		"dwCloseTime",		/*�ر�ʱ��*/
		
		"szDispInfoURL",		/*��ʾ��Ϣ����*/
	
		"szMemo",		/*˵����Ϣ*/
	
		"szManRooms",		/*������(�����ţ�������Ÿ���)*/
	
	"szManDevs",		/*�����豸�б�CUniTable[UNIDEVICE]*/
	 ""};

	/*����̨�˳�����*/
	static public string[] CONLOGOUTREQ = new string[]{
		
		"dwConsoleSN",		/*����̨���*/
		
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*����̨��ʱͨ������*/
	static public string[] CONPULSEREQ = new string[]{
		
		"dwConsoleSN",		/*����̨���*/
		
		"dwStatus",		/*����̨״̬*/
		
		"szStatInfo",		/*״̬��Ϣ*/
	 ""};

	/*����̨��ʱͨ����Ӧ*/
	static public string[] CONPULSERES = new string[]{
		
		"dwChanged",		/*����̨�Ƿ��Ѹ���*/
		
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*����̨��ʾ��Ϣ�ṹ*/
	static public string[] CONMESSAGE = new string[]{
		
      "dwMsgKind",		/*��Ϣ����*/
		
		"MsgInfo",		/*��Ϣ���ݣ����ݲ�ͬ�����Ͷ�Ӧ��ͬ������*/
	 ""};

	/*����̨ˢ�������û���Ϣ*/
	static public string[] CONUSERINFO = new string[]{
		
      "dwUserStat",		/*�û�״̬*/
		
	"AccInfo",		/*UNIACCOUNT �ṹ*/
	
	"ResvInfo",		/*UNIRESERVE �ṹ*/
	
	"DevInfo",		/*UNIDEVICE �ṹ*/
	
	"BillInfo",		/*�˵���(CUniTable<UNIBILL>)*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*����̨��ʦ��¼������Ϣ*/
	static public string[] CONTEACHERINFO = new string[]{
		
	"AccInfo",		/*UNIACCOUNT �ṹ*/
	
	"ResvInfo",		/*UNIRESERVE �ṹ*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*ˢ���ϻ�����*/
	static public string[] CARDFORPCREQ = new string[]{
		
		"dwDevKind",		/*�豸����(Ϊ���ɺ�̨�Զ����䣩*/
		
		"dwLabID",		/*ʵ����ID��(Ϊ���ɺ�̨�Զ����䣩*/
		
		"dwRoomID",		/*��ѡ�����ID��(Ϊ���ɺ�̨�Զ����䣩*/
		
		"dwDevID",		/*�ͻ����豸��ID��(Ϊ���ɺ�̨�Զ����䣩*/
		
	"CheckReq",		/*(ACCCHECKREQ�ṹ)*/
	 ""};

	/*ˢ���ϻ�Ӧ��*/
	static public string[] CARDFORPCRES = new string[]{
		
      "dwMode",		/*��������*/
		
	"ExtInfo",		/*���ݲ�ͬ�ķ������Ͷ�Ӧ��ͬ������*/
	 ""};

	/*ͨ����ˢ������*/
	static public string[] AUTOGATECARDREQ = new string[]{
		
      "dwCardMode",		/*����ˢ��*/
		
		"szLogonName",		/*��¼��(ѧ���ţ�*/
	
		"szCardNo",		/*����*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*ͨ����ˢ����Ӧ*/
	static public string[] AUTOGATECARDRES = new string[]{
		
		"szTrueName",		/*����*/
	
		"szInfo",		/*��ʾ��Ϣ*/
	 ""};

	/*����̨ˢ������*/
	static public string[] CONUSERINREQ = new string[]{
		
      "dwInType",		/*��������*/
		
		"dwResvID",		/*ԤԼID��*/
		
		"dwLabID",		/*ʵ����ID��*/
		
		"dwDevID",		/*�ͻ����豸��ID��*/
		
		"dwDevKind",		/*�豸����ID*/
		
		"dwEndTime",		/*ʹ�ý���ʱ��*/
		
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*����̨ˢ������*/
	static public string[] CONUSERINRES = new string[]{
		
	"ResvInfo",		/*UNIRESERVE �ṹ*/
	
	"DevInfo",		/*UNIDEVICE �ṹ*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*����̨ˢ���˳�����*/
	static public string[] CONUSEROUTREQ = new string[]{
		
      "dwOutType",		/*�뿪����*/
		
		"dwLabID",		/*ʵ����ID��*/
		
		"dwDevID",		/*�ͻ����豸��ID��*/
		 ""};

	/*����̨ˢ���˳�Ӧ��*/
	static public string[] CONUSEROUTRES = new string[]{
		
	"AcctInfo",		/*ʹ����Ϣ��UNIACCTINFO �ṹ*/
	
	"BillInfo",		/*�˵���(CUniTable<UNIBILL>)*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*�ֻ�ɨ������*/
	static public string[] MOBILESCANREQ = new string[]{
		
		"szMSN",		/*MSN*/
	
		"szLogonName",		/*��¼��*/
	
		"szPassword",		/*����*/
	
		"szIP",		/*IP��ַ*/
	
      "dwProperty",		/*��չ����*/
		
		"dwStaSN",		/*վ����*/
		
		"dwLabID",		/*ʵ����ID*/
		
		"dwDevID",		/*�豸ID*/
		
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*�ֻ�ɨ����Ӧ*/
	static public string[] MOBILESCANRES = new string[]{
		
		"dwSessionID",		/*�����������SessionID*/
		
      "dwUserStat",		/*�û�״̬*/
		
		"dwMinUseMin",		/*����ʹ��ʱ��(����)*/
		
		"dwMaxUseMin",		/*�ʹ��ʱ��(����)*/
		
		"szPID",		/*ѧ����*/
	
		"szTrueName",		/*����*/
	
		"dwLabID",		/*ʵ����ID*/
		
		"dwDevID",		/*�豸ID*/
		
		"szDevName",		/*�豸����*/
	
		"szDispInfo",		/*��ʾ��Ϣ*/
	 ""};

	/*�ֻ���ʼʹ������*/
	static public string[] MOBILEUSERINREQ = new string[]{
		
		"dwUseMin",		/*ʹ��ʱ��(����)*/
		
		"szMemo",		/*��ע*/
	 ""};

	/*�ֻ�������Ӧ*/
	static public string[] MOBILEUSERINRES = new string[]{
		
		"szDispInfo",		/*��ʾ��Ϣ*/
	 ""};

	/*�ֻ���ʼʹ������*/
	static public string[] MOBILEDELAYREQ = new string[]{
		
		"dwDelayMin",		/*�ӳ�ʱ��(����)*/
		
		"szMemo",		/*��ע*/
	 ""};

	/*�ֻ�������Ӧ*/
	static public string[] MOBILEDELAYRES = new string[]{
		
		"szDispInfo",		/*��ʾ��Ϣ*/
	 ""};

	/*�ֻ��˳�����*/
	static public string[] MOBILEUSEROUTREQ = new string[]{
		
      "dwOutType",		/*�뿪����*/
		
		"dwDelayMin",		/*��ʱʱ��(����)*/
		
		"szMemo",		/*��ע*/
	 ""};

	/*�ֻ��˳���Ӧ*/
	static public string[] MOBILEUSEROUTRES = new string[]{
		
		"szDispInfo",		/*��ʾ��Ϣ*/
	 ""};

	/*�ֻ���¼ǩ������*/
	static public string[] RESVUSERCOMEINREQ = new string[]{
		
      "dwInType",		/*��������*/
		
		"dwResvID",		/*ԤԼID��*/
		
		"dwLabID",		/*ʵ����ID��*/
		
		"dwDevID",		/*�ͻ����豸��ID��*/
		
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*�ֻ���¼ǩ����Ӧ*/
	static public string[] RESVUSERCOMEINRES = new string[]{
		
		"szDispInfo",		/*��ʾ��Ϣ*/
	 ""};

	/*�ֻ���¼��ʱ����*/
	static public string[] RESVUSERDELAYREQ = new string[]{
		
		"dwResvID",		/*ԤԼID��*/
		
		"dwLabID",		/*ʵ����ID��*/
		
		"dwDevID",		/*�ͻ����豸��ID��*/
		
		"dwMaxDelayMin",		/*����ӳ�ʱ��(����)*/
		
		"szMemo",		/*��ע*/
	 ""};

	/*�ֻ���¼��ʱ��Ӧ*/
	static public string[] RESVUSERDELAYRES = new string[]{
		
		"szDispInfo",		/*��ʾ��Ϣ*/
	 ""};

	/*�ֻ���¼�˳�����*/
	static public string[] RESVUSERGOOUTREQ = new string[]{
		
      "dwOutType",		/*�뿪����*/
		
		"dwResvID",		/*ԤԼID��*/
		
		"dwLabID",		/*ʵ����ID��*/
		
		"dwDevID",		/*�ͻ����豸��ID��*/
		
		"szMemo",		/*��ע*/
	 ""};

	/*�ֻ���¼�˳���Ӧ*/
	static public string[] RESVUSERGOOUTRES = new string[]{
		
		"szDispInfo",		/*��ʾ��Ϣ*/
	 ""};

}
	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/
public partial class Struct_ST
{

	/*��ȡԤԼ��¼*/
	static public string[] RESVRECREQ = new string[]{
		
      "dwGetType",		/*��ʽ*/
		
		"szGetKey",		/*����ֵ*/
	
		"dwAccNo",		/*�˺�*/
		
		"dwUseMode",		/*ʹ��ģʽ*/
		
		"dwPurpose",		/*ԤԼ��;*/
		
		"dwDevKind",		/*�豸����*/
		
		"dwStatus",		/*״̬*/
		
		"dwCheckStat",		/*���״̬*/
		
		"dwCommentStat",		/*����״̬*/
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*�豸ԤԼ��Ϣ*/
	static public string[] UNIRESVREC = new string[]{
		
		"dwSID",		/*��ˮ��*/
		
		"dwResvID",		/*ԤԼ��*/
		
		"dwPreDate",		/*ԤԼ����*/
		
		"dwPreBegin",		/*ԤԼ��ʼʱ��*/
		
		"dwPreEnd",		/*ԤԼ����ʱ��*/
		
		"dwUseMode",		/*ʹ��ģʽ*/
		
		"dwPurpose",		/*ԤԼ��;*/
		
		"dwAccNo",		/*�˺�*/
		
		"szPID",		/*ѧ����*/
	
		"szTrueName",		/*����*/
	
		"dwDevID",		/*�豸ID*/
		
		"dwDevKind",		/*�豸����*/
		
		"dwDevSN",		/*�豸���*/
		
		"szDevName",		/*�豸����*/
	
		"dwRoomID",		/*����ID*/
		
		"szRoomName",		/*��������*/
	
		"dwLabID",		/*ʵ����ID*/
		
		"szLabName",		/*ʵ��������*/
	
      "dwStatus",		/*״̬*/
		
		"dwResvTime",		/*ԤԼ��ʱ��*/
		
		"dwUseTime",		/*ʹ����ʱ��*/
		
		"dwCheckStat",		/*���״̬*/
		
      "dwCommentStat",		/*�û�����״̬*/
		
		"dwTotalFee",		/*���úϼ�*/
		
		"szMemo",		/*��ע*/
	
		"dwInTime",		/*ǩ��ʱ��*/
		
      "dwInMode",		/*ǩ����ʽ*/
		
		"dwOutTime",		/*�˳�ʱ��*/
		
		"dwOutMode",		/*�˳���ʽ*/
		
		"dwLeaveTime",		/*��ʱ�뿪ʱ��*/
		
		"dwLeaveMode",		/*��ʱ�뿪��ʽ*/
		
		"dwBackTime",		/*����ʱ��*/
		
		"dwBackMode",		/*���ط�ʽ*/
		 ""};

	/*��ȡԤԼ����ͳ�Ƽ�¼*/
	static public string[] RESVKINDSTATREQ = new string[]{
		
		"dwPurpose",		/*ԤԼ��;*/
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*ԤԼ����ͳ��*/
	static public string[] RESVKINDSTAT = new string[]{
		
		"dwKind",		/*ԤԼ����*/
		
		"dwResvTimes",		/*ԤԼ����*/
		
		"dwResvMinutes",		/*ԤԼ��ʱ��(����)*/
		
		"dwTestHour",		/*ʵ��ѧʱ��*/
		
		"dwResvDevs",		/*ԤԼ������*/
		
		"dwUseDevs",		/*ʵ���û���*/
		
		"dwResvUsers",		/*�Ͽ�������*/
		
		"dwRealUsers",		/*ʵ�ʵ�������*/
		 ""};

	/*��ȡԤԼ��ʽͳ�Ƶ������*/
	static public string[] RESVMODESTATREQ = new string[]{
		
		"dwOwner",		/*ԤԼ��(������)*/
		
		"dwMemberID",		/*��ԱID*/
		
		"dwDevID",		/*�豸ID*/
		
		"dwUseMode",		/*ʹ�÷���*/
		
		"dwPurpose",		/*ԤԼ��;*/
		
		"dwDevKind",		/*�豸����*/
		
		"dwClassKind",		/*���(��UNIDEVCLS��Kind����)*/
		
		"dwBeginDate",		/*ԤԼ��ʼ����*/
		
		"dwEndDate",		/*ԤԼ��������*/
		
		"szRoomNos",		/*������,����ö��Ÿ���*/
	
		"dwKind",		/*ԤԼ����*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*ԤԼ��ʽͳ��*/
	static public string[] RESVMODESTAT = new string[]{
		
		"dwUseMode",		/*ԤԼ��ʽ*/
		
		"dwUsers",		/*ԤԼ����*/
		
		"dwResvTimes",		/*ԤԼ����*/
		
		"dwResvMinutes",		/*ԤԼ��ʱ��(����)*/
		 ""};

	/*��ѯͳ�Ƶ� ����*/
	static public string[] REPORTREQ = new string[]{
		
      "dwGetType",		/*��ʽ*/
		
		"szGetKey",		/*����ֵ*/
	
		"dwAccNo",		/*�˺�*/
		
		"dwPurpose",		/*��;*/
		
		"dwDevKind",		/*�豸����*/
		
		"dwClassKind",		/*���(��UNIDEVCLS��Kind����)*/
		
		"dwCheckStat",		/*����Ա���״̬(CHECKINFO����)*/
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
		"dwMinPrice",		/*��͵���*/
		
		"dwMaxPrice",		/*��ߵ���*/
		
		"dwStartPurchaseDate",		/*���繺������*/
		
		"dwEndPurchaseDate",		/*��ֹ��������*/
		
		"szRoomIDs",		/*����ID,����ö��Ÿ���*/
	
		"szBuildingIDs",		/*¥��ID,����ö��Ÿ���*/
	
		"szCampusIDs",		/*У��ID,����ö��Ÿ���*/
	
		"dwActivitySN",		/*����ͱ��*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*�豸ʹ�ü�¼ ����*/
	static public string[] USERECREQ = new string[]{
		
		"dwDevID",		/*�豸ID*/
		
		"dwRoomID",		/*����ID*/
		
		"dwLabID",		/*ʵ����ID*/
		
		"dwAccNo",		/*�˺�*/
		
		"dwDeptID",		/*����ID*/
		
		"dwPurpose",		/*��;*/
		
		"dwDevKind",		/*�豸����*/
		
		"dwClassKind",		/*���(��UNIDEVCLS��Kind����)*/
		
		"dwCheckStat",		/*����Ա���״̬(CHECKINFO����)*/
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
		"dwMinPrice",		/*��͵���*/
		
		"dwMaxPrice",		/*��ߵ���*/
		
		"dwStartPurchaseDate",		/*���繺������*/
		
		"dwEndPurchaseDate",		/*��ֹ��������*/
		
		"dwAttendantID",		/*ֵ��Ա�˺�*/
		
		"szMAC",		/*������ַ*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*�豸ʹ�ü�¼*/
	static public string[] DEVUSEREC = new string[]{
		
		"dwSID",		/*��ˮ��*/
		
		"dwAccNo",		/*�ʺ�*/
		
		"szPID",		/*ѧ����*/
	
		"szTrueName",		/*����*/
	
		"dwTutorID",		/*��ʦ���ʺţ�*/
		
		"szTutorName",		/*��ʦ����*/
	
		"dwDeptID",		/*����ID*/
		
		"szDeptName",		/*����*/
	
		"dwDevID",		/*�豸ID*/
		
		"dwDevSN",		/*�豸���*/
		
		"szDevName",		/*�豸����*/
	
		"szMAC",		/*������ַ*/
	
		"szKindName",		/*�豸����*/
	
		"szClassName",		/*�豸�������*/
	
		"szModel",		/*�豸�ͺ�*/
	
		"szSpecification",		/*�豸���*/
	
		"dwUnitPrice",		/*�豸����(Ԫ)*/
		
		"dwPurchaseDate",		/*�ɹ����� YYYYMMDD*/
		
		"dwRoomID",		/*����ID*/
		
		"szRoomName",		/*�����*/
	
		"dwLabID",		/*ʵ����ID*/
		
		"szLabName",		/*ʵ��������*/
	
		"dwAttendantID",		/*ֵ��Ա�˺�*/
		
		"szAttendantName",		/*ֵ��Ա����*/
	
		"dwPurpose",		/*��;*/
		
		"dwBeginTime",		/*��ʼʱ��*/
		
		"dwEndTime",		/*����ʱ��*/
		
		"dwUseTime",		/*ʹ��ʱ��*/
		
		"dwTotalCost",		/*�ܷ���*/
		
		"dwBeginAdmin",		/*������ԱID*/
		
		"szBeginAdminName",		/*������Ա����*/
	
		"dwEndAdmin",		/*�黹����ԱID*/
		
		"szEndAdminName",		/*�黹����Ա����*/
	
		"dwCheckStat",		/*����Ա���״̬*/
		
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*��ѯ��ϸ������*/
	static public string[] DOORCARDRECREQ = new string[]{
		
      "dwGetType",		/*��ʽ*/
		
		"szGetKey",		/*����ֵ*/
	
		"dwAccNo",		/*�˺�*/
		
		"dwCardMode",		/*��DOORCARDREQ�ṹ����*/
		
		"dwUserKind",		/*��DOORCARDRES����*/
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
		"dwStartTime",		/*��ʼʱ��*/
		
		"dwEndTime",		/*����ʱ��*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*�Ž�ˢ����¼*/
	static public string[] DOORCARDREC = new string[]{
		
		"dwSID",		/*��ˮ��*/
		
		"dwAccNo",		/*�ʺ�*/
		
		"szPID",		/*ѧ����*/
	
		"szCardNo",		/*����*/
	
		"szTrueName",		/*����*/
	
		"dwTutorID",		/*��ʦ���ʺţ�*/
		
		"szTutorName",		/*��ʦ����*/
	
		"dwDeptID",		/*����ID*/
		
		"szDeptName",		/*����*/
	
		"dwUsedDate",		/*ˢ������*/
		
		"dwCardTime",		/*ˢ��ʱ��*/
		
		"dwLabID",		/*ʵ����ID*/
		
		"szLabName",		/*ʵ��������*/
	
		"dwRoomID",		/*����ID*/
		
		"szRoomNo",		/*������*/
	
		"szRoomName",		/*��������*/
	
		"dwManMode",		/*���Ʒ�ʽ(UNIROOM�ж���)*/
		
		"dwCardMode",		/*ˢ��ģʽ*/
		
		"dwUserKind",		/*�û�����*/
		
		"dwResvID",		/*ԤԼID*/
		
		"szMemo",		/*��չ��Ϣ*/
	 ""};

	/*����ʹ��ͳ��*/
	static public string[] USERSTAT = new string[]{
		
		"dwAccNo",		/*�ʺ�*/
		
		"szPID",		/*ѧ����*/
	
		"szTrueName",		/*����*/
	
		"szDeptName",		/*����*/
	
		"dwUseTimes",		/*ʹ���˴�*/
		
		"dwUseTime",		/*ʹ����ʱ��*/
		 ""};

	/*ʵ����ʹ����ͳ��*/
	static public string[] LABSTAT = new string[]{
		
		"dwLabID",		/*ʵ����ID*/
		
		"szLabSN",		/*���*/
	
		"szLabName",		/*����*/
	
		"dwTotalNum",		/*����(�������豸��)*/
		
		"dwTotalTestHour",		/*ʹ������ѧʱ��*/
		
		"dwPIDNum",		/*����ʹ������*/
		
		"dwUseTimes",		/*����ʹ���˴�*/
		
		"dwTotalUseTime",		/*����ʹ����ʱ��*/
		 ""};

	/*ʵ����(����)ʹ����ͳ��*/
	static public string[] ROOMSTAT = new string[]{
		
		"dwRoomID",		/*����ID*/
		
		"szLabSN",		/*ʵ���ұ��*/
	
		"szLabName",		/*ʵ��������*/
	
		"szRoomNo",		/*�����*/
	
		"szRoomName",		/*��������*/
	
		"dwTotalNum",		/*����(�豸��)*/
		
		"dwTotalTestHour",		/*ʹ������ѧʱ��*/
		
		"dwTestUseTimes",		/*��ѧʵ��ʹ���˴�*/
		
		"dwUseTimes",		/*����ʹ���˴�*/
		
		"dwTotalUseTime",		/*����ʹ����ʱ��*/
		 ""};

	/*�豸����ʹ����ͳ��*/
	static public string[] DEVKINDSTAT = new string[]{
		
		"dwKindID",		/*�豸����ID*/
		
		"szKindName",		/*�豸����*/
	
		"szModel",		/*�豸�ͺ�*/
	
		"szSpecification",		/*�豸���*/
	
		"dwTotalNum",		/*����(�������豸��)*/
		
		"dwTotalTestHour",		/*ʹ������ѧʱ��*/
		
		"dwPIDNum",		/*����ʹ������*/
		
		"dwUseTimes",		/*����ʹ���˴�*/
		
		"dwTotalUseTime",		/*����ʹ����ʱ��*/
		 ""};

	/*�豸���ʹ����ͳ��*/
	static public string[] DEVCLASSSTAT = new string[]{
		
		"dwClassID",		/*�豸���ID*/
		
		"dwClassKind",		/*���(��UNIDEVCLS��Kind����)*/
		
		"szClassName",		/*�豸�������*/
	
		"dwTotalNum",		/*����(�������豸��)*/
		
		"dwTotalTestHour",		/*ʹ������ѧʱ��*/
		
		"dwPIDNum",		/*����ʹ������*/
		
		"dwUseTimes",		/*����ʹ���˴�*/
		
		"dwTotalUseTime",		/*����ʹ����ʱ��*/
		 ""};

	/*�豸ʹ����ͳ��*/
	static public string[] DEVSTAT = new string[]{
		
		"dwDevID",		/*�豸ID*/
		
		"dwDevSN",		/*�豸���*/
		
		"szDevName",		/*�豸����*/
	
		"szKindName",		/*�豸�������*/
	
		"szClassName",		/*�豸�������*/
	
		"szModel",		/*�豸�ͺ�*/
	
		"szSpecification",		/*�豸���*/
	
		"dwUnitPrice",		/*�豸����*/
		
		"dwPurchaseDate",		/*�ɹ����� YYYYMMDD*/
		
		"dwLabID",		/*ʵ����ID*/
		
		"szLabSN",		/*ʵ���ұ��*/
	
		"szLabName",		/*ʵ��������*/
	
		"dwDeptID",		/*����ID*/
		
		"szDeptName",		/*����*/
	
		"dwAttendantID",		/*ֵ��Ա�˺�*/
		
		"szAttendantName",		/*ֵ��Ա����*/
	
		"dwTotalTestHour",		/*ʹ������ѧʱ��*/
		
		"dwPIDNum",		/*����ʹ������*/
		
		"dwUseTimes",		/*����ʹ���˴�*/
		
		"dwTotalUseTime",		/*����ʹ����ʱ��*/
		
		"dwTotalCost",		/*�ܷ���*/
		 ""};

	/*ѧԺʹ��ͳ��*/
	static public string[] DEPTSTAT = new string[]{
		
		"dwDeptID",		/*ѧԺID*/
		
		"szDeptSN",		/*ѧԺ���*/
	
		"szDeptName",		/*ѧԺ����*/
	
		"dwTotalUsers",		/*ѧԺ����*/
		
		"dwPIDNum",		/*ʹ������*/
		
		"dwUseTimes",		/*ʹ���˴�*/
		
		"dwTotalUseTime",		/*ʹ����ʱ��*/
		 ""};

	/*��ѯ���ͳ�Ƶ� ����*/
	static public string[] IDENTSTATREQ = new string[]{
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
		"dwPurpose",		/*��;*/
		
		"dwDevKind",		/*�豸����*/
		
		"dwClassKind",		/*���(��UNIDEVCLS��Kind����)*/
		
		"dwCheckStat",		/*����Ա���״̬(CHECKINFO����)*/
		
		"dwDeptID",		/*��Ա��������ID*/
		
		"dwActivitySN",		/*����ͱ��*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*���ʹ��ͳ��*/
	static public string[] IDENTSTAT = new string[]{
		
		"dwIdent",		/*���*/
		
		"dwTotalUsers",		/*������*/
		
		"dwPIDNum",		/*ʹ������*/
		
		"dwUseTimes",		/*ʹ���˴�*/
		
		"dwTotalUseTime",		/*ʹ����ʱ��*/
		 ""};

	/*��ȡʵ����Ŀ��*/
	static public string[] TESTITEMSTATREQ = new string[]{
		
		"dwTeacherID",		/*��ʦ���ʺţ�*/
		
		"dwCourseID",		/*�γ�ID*/
		
		"dwLabID",		/*ʵ����ID*/
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*ʵ����Ŀ��*/
	static public string[] TESTITEMSTAT = new string[]{
		
		"dwTestItemID",		/*ʵ����ĿID*/
		
		"dwTestCardID",		/*ʵ����Ŀ��ID*/
		
		"szTestName",		/*ʵ������*/
	
		"dwGroupPeopleNum",		/*ÿ������*/
		
		"dwTestHour",		/*��ʵ����Ŀѧʱ��*/
		
		"dwTestClass",		/*ʵ�����*/
		
		"dwTestKind",		/*ʵ������*/
		
		"dwRequirement",		/*ʵ��Ҫ��*/
		
		"dwTestPlanID",		/*ʵ��ƻ�ID*/
		
		"szAcademicSubjectCode",		/*����ѧ�Ʊ���*/
	
		"dwTesteeKind",		/*ʵ�������*/
		
		"dwTeacherID",		/*��ʦ���ʺţ�*/
		
		"szTeacherName",		/*��ʦ����*/
	
		"dwCourseID",		/*�γ�ID*/
		
		"szCourseCode",		/*�γ̴���*/
	
		"szCourseName",		/*�γ�����*/
	
		"dwCourseProperty",		/*�γ�����*/
		
		"dwGroupID",		/*�Ͽΰ༶*/
		
		"szGroupName",		/*�Ͽΰ༶����*/
	
		"dwGroupUsers",		/*���û���*/
		
		"dwLabID",		/*ʵ����ID*/
		
		"szLabSN",		/*ʵ���ұ��*/
	
		"szLabName",		/*ʵ��������*/
	
		"dwDevNum",		/*ԤԼ�豸��*/
		 ""};

	/*��ȡ��ѧԤԼ��¼*/
	static public string[] TEACHINGRESVRECREQ = new string[]{
		
		"dwTeacherID",		/*��ʦ���ʺţ�*/
		
		"dwCourseID",		/*�γ�ID*/
		
		"dwLabID",		/*ʵ����ID*/
		
		"dwTestPlanID",		/*ʵ��ƻ�ID*/
		
		"dwYearTerm",		/*ѧ�ڱ��*/
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
		"dwMinUseRate",		/*ʵ�����ʹ����*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*ÿ����ʵ������*/
	static public string[] USERSPERMINUTE = new string[]{
		
		"dwUsers",		/*ʵ������*/
		 ""};

	/*��ѧԤԼ��¼*/
	static public string[] TEACHINGRESVREC = new string[]{
		
		"dwTestItemID",		/*ʵ����ĿID*/
		
		"dwTestCardID",		/*ʵ����Ŀ��ID*/
		
		"szTestName",		/*ʵ������*/
	
		"dwGroupPeopleNum",		/*ÿ������*/
		
		"dwTestHour",		/*��ʵ����Ŀѧʱ��*/
		
		"dwTestClass",		/*ʵ�����*/
		
		"dwTestKind",		/*ʵ������*/
		
		"dwRequirement",		/*ʵ��Ҫ��*/
		
		"dwTestPlanID",		/*ʵ��ƻ�ID*/
		
		"szAcademicSubjectCode",		/*����ѧ��*/
	
		"dwTesteeKind",		/*ʵ�������*/
		
		"dwTeacherID",		/*��ʦ���ʺţ�*/
		
		"szTeacherName",		/*��ʦ����*/
	
		"dwCourseID",		/*�γ�ID*/
		
		"szCourseCode",		/*�γ̴���*/
	
		"szCourseName",		/*�γ�����*/
	
		"dwCourseProperty",		/*�γ�����*/
		
		"dwGroupID",		/*�Ͽΰ༶*/
		
		"szGroupName",		/*�Ͽΰ༶����*/
	
		"dwGroupUsers",		/*���û���*/
		
		"dwLabID",		/*ʵ����ID*/
		
		"szLabSN",		/*ʵ���ұ��*/
	
		"szLabName",		/*ʵ��������*/
	
		"dwResvID",		/*ԤԼID*/
		
		"dwResvStat",		/*ԤԼ״̬*/
		
		"dwPreDate",		/*ԤԼ��ʼ����*/
		
		"dwBeginTime",		/*ԤԼ��ʼʱ��*/
		
		"dwEndTime",		/*ԤԼ����ʱ��*/
		
		"dwTeachingTime",		/*��ѧʱ��(��ʽ��UNIRESERVE)*/
		
		"dwDevNum",		/*ԤԼ�豸��*/
		
		"dwAttendUsers",		/*ʵ������*/
		
	"UsersPerMinute",		/*CUniTable[USERSPERMINUTE]*/
	 ""};

	/*��ȡ�豸ʹ��������*/
	static public string[] DEVUSINGRATEREQ = new string[]{
		
      "dwGetType",		/*��ʽ*/
		
		"szGetKey",		/*����ֵ*/
	
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
		"dwClassKind",		/*���(��UNIDEVCLS��Kind����)*/
		
		"szRoomIDs",		/*����ID,����ö��Ÿ���*/
	
		"szBuildingIDs",		/*¥��ID,����ö��Ÿ���*/
	
		"szCampusIDs",		/*У��ID,����ö��Ÿ���*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*�豸ʹ�������ݱ�*/
	static public string[] DEVUSINGTABLE = new string[]{
		
		"dwUseTimes",		/*�豸ʹ���ܴ���*/
		
		"dwResvTimes",		/*�豸ԤԼ�ܴ���*/
		 ""};

	/*�豸ʹ����ͳ�Ʊ�*/
	static public string[] DEVUSINGRATE = new string[]{
		
		"dwDevNums",		/*ͳ���豸����*/
		
		"dwDays",		/*ͳ������*/
		
	"szUsingTable",		/*�豸ʹ�������ݱ�(CUniTable[DEVUSINGTABLE]),ά��Ϊ24*60*/
	 ""};

	/*��ȡ�豸��ʹ��������*/
	static public string[] DEVWEEKUSINGRATEREQ = new string[]{
		
      "dwGetType",		/*��ʽ*/
		
		"szGetKey",		/*����ֵ*/
	
		"dwStartDate",		/*��ʼ����*/
		
		"dwWeeks",		/*��ѯ����*/
		
		"dwClassKind",		/*���(��UNIDEVCLS��Kind����)*/
		
		"szRoomIDs",		/*����ID,����ö��Ÿ���*/
	
		"szBuildingIDs",		/*¥��ID,����ö��Ÿ���*/
	
		"szCampusIDs",		/*У��ID,����ö��Ÿ���*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*�豸��ʹ����ͳ�Ʊ�*/
	static public string[] DEVWEEKUSINGRATE = new string[]{
		
		"dwDevNums",		/*ͳ���豸����*/
		
		"dwWeeks",		/*ͳ������*/
		
	"szUsingTable",		/*�豸ʹ�������ݱ�(CUniTable[DEVUSINGTABLE]),ά��Ϊ7*/
	 ""};

	/*���ݻ����ͳ�� ����*/
	static public string[] YARDACTIVITYSTATREQ = new string[]{
		
		"dwDevKind",		/*�豸����*/
		
		"dwClassKind",		/*���(��UNIDEVCLS��Kind����)*/
		
		"dwCheckStat",		/*����Ա���״̬(CHECKINFO����)*/
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
		"szRoomIDs",		/*����ID,����ö��Ÿ���*/
	
		"szBuildingIDs",		/*¥��ID,����ö��Ÿ���*/
	
		"szCampusIDs",		/*У��ID,����ö��Ÿ���*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*���ݻ����ͳ��*/
	static public string[] YARDACTIVITYSTAT = new string[]{
		
		"dwActivitySN",		/*����ͱ��*/
		
		"szActivityName",		/*���������*/
	
		"dwPIDNum",		/*ʹ������*/
		
		"dwUseTimes",		/*ʹ���˴�*/
		
		"dwTotalUseTime",		/*ʹ����ʱ��*/
		 ""};

	/*��ȡ�豸��ʹ��ͳ��*/
	static public string[] DEVMONTHSTATREQ = new string[]{
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*�豸��ʹ��ͳ��*/
	static public string[] DEVMONTHSTAT = new string[]{
		
		"dwYearMonth",		/*ͳ���·�*/
		
		"dwWResvTime",		/*�������豸ԤԼ��ʱ��(����)*/
		
		"dwRResvTime",		/*�������豸ԤԼ��ʱ��(����)*/
		
		"dwWUseTime",		/*�������豸ʹ����ʱ��(����)*/
		
		"dwRUseTime",		/*�ǹ������豸ʹ����ʱ��(����)*/
		 ""};

	/*��ȡ�豸����ʵ��ͳ������*/
	static public string[] RTUSESTATREQ = new string[]{
		
      "dwStatType",		/*ͳ�Ʒ�ʽ*/
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
		"dwDeptID",		/*�豸��������ID*/
		
		"szLabIDs",		/*ʵ����ID,����ö��Ÿ���*/
	
		"szRoomIDs",		/*����ID,����ö��Ÿ���*/
	
		"szKindIDs",		/*��������,����ö��Ÿ���*/
	
		"szClassIDs",		/*�����������ID,����ö��Ÿ���*/
	
		"szDevIDs",		/*�豸ID,����ö��Ÿ���*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO],szExtInfo����RTUSESTAT�ϼ�*/
	 ""};

	/*�豸����ʵ��ͳ��*/
	static public string[] RTUSESTAT = new string[]{
		
		"dwStatID",		/*ͳ�ƶ���ID*/
		
		"szStatName",		/*ͳ�ƶ�������*/
	
		"szExtName",		/*��չ��Ϣ(������������Ա��*/
	
		"dwResvTimes",		/*ԤԼ����*/
		
		"dwResvMinutes",		/*ԤԼ��ʱ��(����)*/
		
		"dwUseTimes",		/*ʹ�ô���*/
		
		"dwUseMinutes",		/*�豸ʹ����ʱ��(����)*/
		
		"dwSampleNum",		/*������Ʒ��*/
		
		"dwReceivableCost",		/*Ӧ�ɷ���*/
		
		"dwUseFee",		/*ϵͳ�Զ����㣨Ӧ�ɷ��ã�*/
		
		"dwRealCost",		/*�������*/
		
		"dwDevUseFee",		/*�豸ʹ�÷�*/
		
		"dwSampleFee",		/*��Ʒ��*/
		
		"dwAssistFee",		/*Э����*/
		
		"dwEntrustFee",		/*�����*/
		
		"dwNegotiationFee",		/*Э���շ�*/
		 ""};

	/*��ȡ�豸����ʵ����ϸ����*/
	static public string[] RTUSEDETAILREQ = new string[]{
		
		"dwDevID",		/*�豸ID*/
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*�豸����ʵ����ϸ*/
	static public string[] RTUSEDETAIL = new string[]{
		
		"dwDevID",		/*�豸ID*/
		
		"szDevName",		/*�豸����*/
	
		"szAttendantName",		/*��������Ա*/
	
		"dwResvID",		/*ԤԼ��*/
		
		"szTestName",		/*����ʵ������*/
	
		"dwOwner",		/*ԤԼ��(������)*/
		
		"szOwnerName",		/*ԤԼ������*/
	
		"dwPreDate",		/*ԤԼ��ʼ����*/
		
		"dwBeginTime",		/*ԤԼ��ʼʱ��*/
		
		"dwRTID",		/*����ʵ����ĿID*/
		
		"szRTName",		/*����ʵ������*/
	
		"dwHolderID",		/*�����ˣ��ʺţ�*/
		
		"szHolderName",		/*����������*/
	
		"dwManID",		/*����ԱID*/
		
		"szManName",		/*����Ա����*/
	
		"dwResvMinutes",		/*ԤԼ��ʱ��(����)*/
		
		"dwUseMinutes",		/*�豸ʹ����ʱ��(����)*/
		
		"dwSampleNum",		/*������Ʒ��*/
		
		"dwReceivableCost",		/*Ӧ�ɷ���*/
		
		"dwUseFee",		/*ϵͳ�Զ����㣨Ӧ�ɷ��ã�*/
		
		"dwRealCost",		/*�������*/
		
		"dwDevUseFee",		/*�豸ʹ�÷�*/
		
		"dwSampleFee",		/*��Ʒ��*/
		
		"dwAssistFee",		/*Э����*/
		
		"dwEntrustFee",		/*�����*/
		
		"dwNegotiationFee",		/*Э���շ�*/
		 ""};

	/*��ȡ�豸����ʵ�龭�ѷ���ͳ������*/
	static public string[] RTFASTATREQ = new string[]{
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
		"dwDeptID",		/*�豸��������ID*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO],szExtInfo����RTFASTAT�ϼ�*/
	 ""};

	/*�豸����ʵ�龭�ѷ���ͳ��*/
	static public string[] RTFASTAT = new string[]{
		
		"dwDevID",		/*�豸ID*/
		
		"szDevName",		/*�豸����*/
	
		"szAttendantName",		/*��������Ա*/
	
		"dwResvTimes",		/*ԤԼ����*/
		
		"dwResvMinutes",		/*ԤԼ��ʱ��(����)*/
		
		"dwUseTimes",		/*ʹ�ô���*/
		
		"dwUseMinutes",		/*�豸ʹ����ʱ��(����)*/
		
		"dwSampleNum",		/*������Ʒ��*/
		
		"dwTotalFee",		/*�շ��ܽ��*/
		
		"dwTestFee",		/*�������Է�*/
		
		"dwOpenFundFee",		/*���Ż���*/
		
		"dwServiceFee",		/*�����*/
		 ""};

	/*��ȡ�豸����ʵ�龭�ѷ�����ϸ����*/
	static public string[] RTFADETAILREQ = new string[]{
		
		"dwDevID",		/*�豸ID*/
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*�豸����ʵ�龭�ѷ�����ϸ*/
	static public string[] RTFADETAIL = new string[]{
		
		"dwDevID",		/*�豸ID*/
		
		"szDevName",		/*�豸����*/
	
		"szAttendantName",		/*��������Ա*/
	
		"dwResvID",		/*ԤԼ��*/
		
		"szTestName",		/*����ʵ������*/
	
		"dwOwner",		/*ԤԼ��(������)*/
		
		"szOwnerName",		/*ԤԼ������*/
	
		"dwPreDate",		/*ԤԼ��ʼ����*/
		
		"dwBeginTime",		/*ԤԼ��ʼʱ��*/
		
		"dwRTID",		/*����ʵ����ĿID*/
		
		"szRTName",		/*����ʵ������*/
	
		"dwHolderID",		/*�����ˣ��ʺţ�*/
		
		"szHolderName",		/*����������*/
	
		"dwManID",		/*����ԱID*/
		
		"szManName",		/*����Ա����*/
	
		"dwResvMinutes",		/*ԤԼ��ʱ��(����)*/
		
		"dwUseMinutes",		/*�豸ʹ����ʱ��(����)*/
		
		"dwSampleNum",		/*������Ʒ��*/
		
		"dwTotalFee",		/*�շ��ܽ��*/
		
		"dwTestFee",		/*�������Է�*/
		
		"dwOpenFundFee",		/*���Ż���*/
		
		"dwServiceFee",		/*�����*/
		 ""};

	/*��ѯΥԼͳ�Ƶ�����*/
	static public string[] DEFAULTSTATREQ = new string[]{
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
		"dwCTSN",		/*���������*/
		
		"dwUsePurpose",		/*��;*/
		
		"dwForClsKind",		/*�����豸���*/
		
		"dwDeptID",		/*��Ա��������ID*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*ΥԼͳ��*/
	static public string[] DEFAULTSTAT = new string[]{
		
		"dwCreditSN",		/*�������ͱ��*/
		
		"szCreditName",		/*������������*/
	
		"dwResvTimes",		/*ԤԼ����*/
		
		"dwDefaultTimes",		/*ΥԼ����*/
		 ""};

	/*��ѧ���������豸��*/
	static public string[] DEVLISTREQ = new string[]{
		
		"dwYearTerm",		/*ѧ�ڱ��*/
		
		"dwReportStat",		/*����״̬*/
		
		"szLabIDs",		/*ʵ����ID,����ö��Ÿ���*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*��ѧ���������豸�嵥*/
	static public string[] DEVLIST = new string[]{
		
		"dwYearTerm",		/*ѧ�ڱ��*/
		
      "dwReportStat",		/*����״̬*/
		
		"dwDevID",		/*�豸ID*/
		
		"szAssertSN",		/*�û����ʲ����*/
	
		"szClassSN",		/*�豸�����*/
	
		"szDevName",		/*ʵ���豸����*/
	
		"szModel",		/*�豸�ͺ�*/
	
		"szSpecification",		/*�豸���*/
	
		"dwComeFrom",		/*������Դ*/
		
		"dwNationCode",		/*������*/
		
		"dwUnitPrice",		/*�豸����*/
		
		"dwPurchaseDate",		/*�ɹ�����*/
		
		"dwStatCode",		/*��״��*/
		
		"dwUseFor",		/*ʹ�÷���*/
		
		"dwLabID",		/*ʵ����ID*/
		
		"szLabSN",		/*ʵ���ұ��*/
	
		"szLabName",		/*ʵ��������*/
	
		"dwDeptID",		/*����ID*/
		
		"szDeptSN",		/*���ű��*/
	
		"szDeptName",		/*����*/
	 ""};

	/*��ѧ���������豸�����䶯�����*/
	static public string[] DEVCHGREQ = new string[]{
		
		"dwYearTerm",		/*ѧ�ڱ��*/
		
		"dwReportStat",		/*����״̬*/
		
		"dwUnitPrice",		/*���������۸����*/
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		 ""};

	/*��ѧ���������豸�����䶯�����*/
	static public string[] DEVCHG = new string[]{
		
		"dwYearTerm",		/*ѧ�ڱ��*/
		
		"dwReportStat",		/*����״̬*/
		
		"dwBDevNum",		/*�ڳ�����*/
		
		"dwBMoney",		/*�ڳ����*/
		
		"dwBBigDevNum",		/*���������ڳ�����*/
		
		"dwBBigMoney",		/*���������ڳ����*/
		
		"dwIncDevNum",		/*��������*/
		
		"dwIncMoney",		/*���ӽ��*/
		
		"dwIncBigDevNum",		/*����������������*/
		
		"dwIncBigMoney",		/*�����������ӽ��*/
		
		"dwDecDevNum",		/*��������*/
		
		"dwDecMoney",		/*���ٽ��*/
		
		"dwDecBigDevNum",		/*����������������*/
		
		"dwDecBigMoney",		/*�����������ٽ��*/
		
		"dwEDevNum",		/*��ĩ����*/
		
		"dwEMoney",		/*��ĩ���*/
		
		"dwEBigDevNum",		/*����������ĩ����*/
		
		"dwEBigMoney",		/*����������ĩ���*/
		 ""};

	/*���������豸��*/
	static public string[] BIGDEVREQ = new string[]{
		
		"dwYearTerm",		/*ѧ�ڱ��*/
		
		"dwReportStat",		/*����״̬*/
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
		"dwUnitPrice",		/*���������۸����*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*���������豸��*/
	static public string[] BIGDEV = new string[]{
		
		"dwYearTerm",		/*ѧ�ڱ��*/
		
		"dwReportStat",		/*����״̬*/
		
		"dwDevID",		/*�豸ID*/
		
		"szAssertSN",		/*�û����ʲ����*/
	
		"szClassSN",		/*�豸�����*/
	
		"szDevName",		/*ʵ���豸����*/
	
		"dwUnitPrice",		/*�豸����*/
		
		"szModel",		/*�豸�ͺ�*/
	
		"szSpecification",		/*�豸���*/
	
		"szAttendantName",		/*����������*/
	
		"dwSampleNum",		/*������*/
		
		"dwTUseTime",		/*��ѧ��ʱ*/
		
		"dwRUseTime",		/*���л�ʱ*/
		
		"dwSUseTime",		/*����ʱ*/
		
		"dwOUseTime",		/*���Ż�ʱ*/
		
		"dwUseTeachers",		/*ʹ�ý�ʦ����*/
		
		"dwUseStudents",		/*ʹ��ѧ������*/
		
		"dwUseOthers",		/*ʹ����������*/
		
		"dwTItemNum",		/*��ѧʵ����Ŀ*/
		
		"dwRItemNum",		/*����ʵ����Ŀ*/
		
		"dwSItemNum",		/*���ʵ����Ŀ*/
		
		"dwNReward",		/*���Ҽ�����*/
		
		"dwPReward",		/*ʡ������*/
		
		"dwTPatent",		/*��ʦר��*/
		
		"dwSPatent",		/*ѧ��ר��*/
		
		"dwThreeIndex",		/*�������*/
		
		"dwKernelJournal",		/*���Ŀ���*/
		 ""};

	/*��ȡʵ����Ŀ��*/
	static public string[] TESTITEMREPORTREQ = new string[]{
		
		"dwYearTerm",		/*ѧ�ڱ��*/
		
		"dwReportStat",		/*����״̬*/
		
		"szLabIDs",		/*ʵ����ID,����ö��Ÿ���*/
	
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*ʵ����Ŀ��*/
	static public string[] TESTITEMREPORT = new string[]{
		
		"dwYearTerm",		/*ѧ�ڱ��*/
		
		"dwReportStat",		/*����״̬*/
		
		"dwTestCardID",		/*ʵ����Ŀ��ID*/
		
		"szTestSN",		/*ʵ����*/
	
		"szTestName",		/*ʵ������*/
	
		"dwTestClass",		/*ʵ�����*/
		
		"dwTestKind",		/*ʵ������*/
		
		"dwRequirement",		/*ʵ��Ҫ��*/
		
		"szAcademicSubjectCode",		/*����ѧ�Ʊ���*/
	
		"dwTesteeKind",		/*ʵ�������*/
		
		"dwGroupPeopleNum",		/*ÿ������*/
		
		"dwTestHour",		/*��ʵ����Ŀѧʱ��*/
		
		"dwTesteeNum",		/*����������*/
		
		"dwLabID",		/*ʵ����ID*/
		
		"szLabSN",		/*ʵ���ұ��*/
	
		"szLabName",		/*ʵ��������*/
	 ""};

	/*ר��ʵ������Ա��*/
	static public string[] STAFFINFOREQ = new string[]{
		
		"dwYearTerm",		/*ѧ�ڱ��*/
		
		"dwReportStat",		/*����״̬*/
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
		"szLabIDs",		/*ʵ����ID,����ö��Ÿ���*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*ר��ʵ������Ա��*/
	static public string[] STAFFINFO = new string[]{
		
		"dwYearTerm",		/*ѧ�ڱ��*/
		
		"dwReportStat",		/*����״̬*/
		
		"dwAccNo",		/*�ʺ�*/
		
		"szPID",		/*��Ա���(ѧ����)*/
	
		"dwLabID",		/*ʵ����ID*/
		
		"szLabSN",		/*ʵ���ұ��*/
	
		"szLabName",		/*ʵ��������*/
	
		"szTrueName",		/*����*/
	
		"dwSex",		/*�Ա��UniCommon.h*/
		
		"dwBirthDate",		/*��������*/
		
      "dwJobTitle",		/*ְ�Ʊ���*/
		
      "dwDuty",		/*ְ��*/
		
      "dwJobType",		/*��������*/
		
		"szAcademicSubjectCode",		/*����ѧ�Ʊ���*/
	
		"dwProfessionalTitle",		/*רҵ����ְ��*/
		
		"dwEducation",		/*�Ļ��̶�*/
		
		"dwExpertType",		/*ר�����*/
		
		"dwInlandUduTime",		/*����ѧ������ʱ��*/
		
		"dwInlandOtherTime",		/*���ڷ�ѧ������ʱ��*/
		
		"dwAbroadUduTime",		/*����ѧ������ʱ��*/
		
		"dwAbroadOtherTime",		/*�����ѧ������ʱ��*/
		
		"szMemo",		/*��ע*/
	 ""};

	/*ʵ���һ��������*/
	static public string[] LABINFOREQ = new string[]{
		
		"dwYearTerm",		/*ѧ�ڱ��*/
		
		"dwReportStat",		/*����״̬*/
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
		"szLabIDs",		/*ʵ����ID,����ö��Ÿ���*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*ʵ���һ��������*/
	static public string[] LABINFO = new string[]{
		
		"dwYearTerm",		/*ѧ�ڱ��*/
		
		"dwReportStat",		/*����״̬*/
		
		"dwLabID",		/*ʵ����ID*/
		
		"szLabSN",		/*ʵ���ұ��*/
	
		"szLabName",		/*ʵ��������*/
	
		"szLabKindCode",		/*ʵ�������ͱ���*/
	
		"szLabLevelCode",		/*ʵ���ҽ���ˮƽ����*/
	
		"szLabFromCode",		/*ʵ������Դ����*/
	
		"szAcademicSubjectCode",		/*����ѧ�Ʊ���*/
	
		"dwLabClass",		/*ʵ�������*/
		
		"dwCreateDate",		/*�������*/
		
		"dwTNReward",		/*��ʦ���Ҽ�����*/
		
		"dwTPReward",		/*��ʦʡ������*/
		
		"dwTPatent",		/*��ʦר��*/
		
		"dwSNReward",		/*ѧ�����Ҽ�����*/
		
		"dwSPReward",		/*ѧ��ʡ������*/
		
		"dwSPatent",		/*ѧ��ר��*/
		
		"dwTThreeIndex",		/*��ѧ�������*/
		
		"dwTKernelJournal",		/*��ѧ�����ڿ�*/
		
		"dwRThreeIndex",		/*�����������*/
		
		"dwRKernelJournal",		/*���к����ڿ�*/
		
		"dwTestBookNum",		/*ʵ��̲���*/
		
		"dwTItemNum",		/*��ѧʵ����Ŀ*/
		
		"dwRItemNum",		/*����ʵ����Ŀ*/
		
		"dwPTItemNum",		/*ʡ�������Ͻ�ѧʵ����Ŀ*/
		
		"dwPRItemNum",		/*ʡ�������Ͽ���ʵ����Ŀ*/
		
		"dwSItemNum",		/*���ʵ����Ŀ*/
		
		"dwZKThesisUsers",		/*ר����������*/
		
		"dwBKThesisUsers",		/*������������*/
		
		"dwSSThesisUsers",		/*˶ʿ�о�����������*/
		
		"dwBSThesisUsers",		/*��ʿ�о�����������*/
		
		"dwItemNum",		/*ʵ�����*/
		
		"dwOtherItemNum",		/*У��ʵ�����*/
		
		"dwUseUsers",		/*ʵ������*/
		
		"dwOtherUsers",		/*У��ʵ������*/
		
		"dwUseTime",		/*ʵ����ʱ��*/
		
		"dwOtherTime",		/*У��ʵ����ʱ��*/
		
		"dwPartTimeUsers",		/*��ְ��Ա��*/
		
		"dwTotalCost",		/*���з���*/
		
		"dwConsumeCost",		/*�Ĳķ�*/
		 ""};

	/*ʵ���Ҿ��������*/
	static public string[] LABALLCOSTREQ = new string[]{
		
		"dwYearTerm",		/*ѧ�ڱ��*/
		
		"dwReportStat",		/*����״̬*/
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		 ""};

	/*ʵ���Ҿ��������*/
	static public string[] LABALLCOST = new string[]{
		
		"dwYearTerm",		/*ѧ�ڱ��*/
		
		"dwReportStat",		/*����״̬*/
		
		"dwLabNum",		/*ʵ���Ҹ���*/
		
		"dwLabArea",		/*ʵ�������*/
		
		"dwTotalCost",		/*�ܼ�(Ԫ)*/
		
		"dwBuyCost",		/*���÷�(Ԫ)*/
		
		"dwTBuyCost",		/*���ǹ��÷�(Ԫ)*/
		
		"dwKeepCost",		/*ά����(Ԫ)*/
		
		"dwTKeepCost",		/*����ά����(Ԫ)*/
		
		"dwRunCost",		/*���з�(Ԫ)*/
		
		"dwCRunCost",		/*�Ĳķ�(Ԫ)*/
		
		"dwBuildCost",		/*�����(Ԫ)*/
		
		"dwRAndRCost",		/*�о���ĸ��(Ԫ)*/
		
		"dwOtherCost",		/*������(Ԫ)*/
		 ""};

	/*�ߵ�ѧУʵ�����ۺ���Ϣ��*/
	static public string[] LABSUMMARYREQ = new string[]{
		
		"dwYearTerm",		/*ѧ�ڱ��*/
		
		"dwReportStat",		/*����״̬*/
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
		"dwUnitPrice",		/*�豸����*/
		 ""};

	/*�ߵ�ѧУʵ�����ۺ���Ϣ��*/
	static public string[] LABSUMMARY = new string[]{
		
		"dwYearTerm",		/*ѧ�ڱ��*/
		
		"dwReportStat",		/*����״̬*/
		
		"dwLabNum",		/*ʵ���Ҹ���*/
		
		"dwLabArea",		/*ʵ�������*/
		
		"dwDevNum",		/*��������*/
		
		"dwDevMoney",		/*�������*/
		
		"dwBigDevNum",		/*������������*/
		
		"dwBigMoney",		/*�����������*/
		
		"dwTItemNum",		/*��ѧʵ����Ŀ*/
		
		"dwTUseTime",		/*��ѧʵ����ʱ��*/
		
		"dwDUseTime",		/*��ʿ��ʱ��*/
		
		"dwMUseTime",		/*˶ʿ��ʱ��*/
		
		"dwUUseTime",		/*������ʱ��*/
		
		"dwJUseTime",		/*ר����ʱ��*/
		
		"dwRItemNum",		/*����ʵ����Ŀ*/
		
		"dwHTStaff",		/*�߼���ʦ������Ա*/
		
		"dwHSStaff",		/*�߼�ʵ�鼼����Ա*/
		
		"dwMTStaff",		/*�м���ʦ������Ա*/
		
		"dwMSStaff",		/*�м�ʵ�鼼����Ա*/
		
		"dwOtherStaff",		/*������Ա*/
		
		"dwPartTimeStaff",		/*��ְ��Ա*/
		
		"dwPaperNum",		/*������*/
		
		"dwTReward",		/*��ʦ����*/
		
		"dwSReward",		/*ѧ������*/
		 ""};

	/*�ߵ�ѧУʵ�����ۺ���Ϣ��2*/
	static public string[] LABSUMMARY2REQ = new string[]{
		
		"dwYearTerm",		/*ѧ�ڱ��*/
		
		"dwReportStat",		/*����״̬*/
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
		"dwUnitPrice",		/*�豸����*/
		 ""};

	/*�ߵ�ѧУʵ�����ۺ���Ϣ��2*/
	static public string[] LABSUMMARY2 = new string[]{
		
		"dwYearTerm",		/*ѧ�ڱ��*/
		
		"dwReportStat",		/*����״̬*/
		
		"dwLabNum",		/*ʵ���Ҹ���*/
		
		"dwLabArea",		/*ʵ�������*/
		
		"dwDevNum",		/*��������*/
		
		"dwDevMoney",		/*�������*/
		
		"dwBigDevNum",		/*������������*/
		
		"dwBigMoney",		/*�����������*/
		 ""};

}
	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/
public partial class Struct_ST
{

	/*��ȡ�����������*/
	static public string[] CFGREQ = new string[]{
		
      "dwGetType",		/*��ȡ�������*/
		
		"szGetKey",		/*��ȡ����ֵ*/
	 ""};

	/*����������*/
	static public string[] CFGINFO = new string[]{
		
		"dwKindSN",		/*������������*/
		
      "dwCfgSN",		/*����������*/
		
      "dwValueKind",		/*ֵ����*/
		
		"dwNumberValue",		/*������ֵ����ֵ����Ч*/
		
		"szStringValue",		/*������ֵ���ַ�������Ч*/
	
		"szMemo",		/*˵��*/
	 ""};

	/*��ȡ�������*/
	static public string[] CREDITTYPEREQ = new string[]{
		
		"dwCTSN",		/*���������*/
		
		"dwCTStat",		/*״̬*/
		 ""};

	/*�������������*/
	static public string[] CREDITTYPE = new string[]{
		
		"dwCTSN",		/*���������*/
		
		"szCTName",		/*�����������*/
	
		"dwForClsKind",		/*�����豸���*/
		
		"dwUsePurpose",		/*��;*/
		
		"dwMaxScore",		/*������û���*/
		
      "dwScoreCycle",		/*���üƷ�����*/
		
		"dwForbidUseTime",		/*���û���Ϊ0��ֹʹ��ʱ�䣨�죩*/
		
      "dwCTStat",		/*״̬*/
		
		"szMemo",		/*˵��*/
	 ""};

	/*��������*/
	static public string[] CREDITKIND = new string[]{
		
      "dwCreditSN",		/*�������ͱ��*/
		
      "dwScoreType",		/*���ִ���ʽ*/
		
      "dwCKStat",		/*״̬*/
		
		"szCreditName",		/*������������*/
	
		"szMemo",		/*˵��*/
	 ""};

	/*��ȡ���üƷֱ�*/
	static public string[] CREDITSCOREREQ = new string[]{
		
		"dwID",		/*ID*/
		
		"dwCTSN",		/*���������*/
		
		"dwCreditSN",		/*�������ͱ��*/
		
		"dwForClsKind",		/*�����豸���*/
		
		"dwUsePurpose",		/*��;*/
		 ""};

	/*���üƷֱ�*/
	static public string[] CREDITSCORE = new string[]{
		
		"dwID",		/*ID*/
		
		"dwCTSN",		/*���������*/
		
		"szCTName",		/*�����������*/
	
		"dwForClsKind",		/*�����豸���*/
		
		"dwUsePurpose",		/*��;*/
		
		"dwMaxScore",		/*������û���*/
		
		"dwScoreCycle",		/*���üƷ�����*/
		
		"dwForbidUseTime",		/*���û���Ϊ0��ֹʹ��ʱ�䣨�죩*/
		
		"dwCreditSN",		/*�������ͱ��*/
		
		"szCreditName",		/*������������*/
	
		"dwScoreType",		/*���ִ���ʽ*/
		
		"dwUseNum",		/*�������ö���*/
		
		"dwMinValue1",		/*����������Сֵ1*/
		
		"dwMaxValue1",		/*�����������ֵ1*/
		
		"dwCreditScore1",		/*�ۻ򽱻���1*/
		
		"dwMinValue2",		/*����������Сֵ2*/
		
		"dwMaxValue2",		/*�����������ֵ2*/
		
		"dwCreditScore2",		/*�ۻ򽱻���2*/
		
		"dwMinValue3",		/*����������Сֵ3*/
		
		"dwMaxValue3",		/*�����������ֵ3*/
		
		"dwCreditScore3",		/*�ۻ򽱻���3*/
		
		"dwMinValue4",		/*����������Сֵ4*/
		
		"dwMaxValue4",		/*�����������ֵ4*/
		
		"dwCreditScore4",		/*�ۻ򽱻���4*/
		
		"dwMinValue5",		/*����������Сֵ5*/
		
		"dwMaxValue5",		/*�����������ֵ5*/
		
		"dwCreditScore5",		/*�ۻ򽱻���*/
		
		"dwMinValue6",		/*����������Сֵ6*/
		
		"dwMaxValue6",		/*�����������ֵ6*/
		
		"dwCreditScore6",		/*�ۻ򽱻���6*/
		
		"szMemo",		/*˵��*/
	 ""};

	/*��ȡ�ҵ����û���*/
	static public string[] MYCREDITSCOREREQ = new string[]{
		
		"dwAccNo",		/*�˺�*/
		
		"dwCTSN",		/*���������*/
		 ""};

	/*�ҵ����û���*/
	static public string[] MYCREDITSCORE = new string[]{
		
		"dwAccNo",		/*�˺�*/
		
		"szPID",		/*ѧ����*/
	
		"szTrueName",		/*����*/
	
		"dwCTSN",		/*���������*/
		
		"szCTName",		/*�����������*/
	
		"dwForClsKind",		/*�����豸���*/
		
		"dwUsePurpose",		/*��;*/
		
		"dwMaxScore",		/*������û���*/
		
		"dwScoreCycle",		/*���üƷ�����*/
		
		"dwForbidUseTime",		/*���û���Ϊ0��ֹʹ��ʱ�䣨�죩*/
		
		"dwLeftCScore",		/*ʣ�����*/
		
		"dwForbidStartDate",		/*���ÿ�ʼ����*/
		
		"dwForbidEndDate",		/*���ý�������*/
		
		"szMemo",		/*˵��*/
	 ""};

	/*�˹����ù���*/
	static public string[] ADMINCREDIT = new string[]{
		
		"dwCTSN",		/*���������*/
		
		"dwCreditSN",		/*�������ͱ��*/
		
		"dwCreditScore",		/*�ۻ򽱻���*/
		
		"dwSubjectID",		/*������ID*/
		
		"dwAccNo",		/*�˺�*/
		
		"szTrueName",		/*����*/
	
		"szReason",		/*ԭ�������*/
	
		"dwPara1",		/*����1����չ��*/
		
		"dwPara2",		/*����2����չ��*/
		
		"szMemo",		/*˵��*/
	 ""};

	/*���ü�¼����*/
	static public string[] CREDITRECREQ = new string[]{
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
		"dwSID",		/*��ˮ��*/
		
		"dwAccNo",		/*�˺�*/
		
		"dwCTSN",		/*���������*/
		
		"dwCreditSN",		/*�������ͱ��*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*���ü�¼*/
	static public string[] CREDITREC = new string[]{
		
		"dwSID",		/*SID*/
		
		"dwCTSN",		/*���������*/
		
		"szCTName",		/*�����������*/
	
		"dwCreditSN",		/*�������ͱ��*/
		
		"szCreditName",		/*������������*/
	
		"dwScoreType",		/*���ִ���ʽ*/
		
		"dwAccNo",		/*�˺�*/
		
		"szPID",		/*ѧ����*/
	
		"szTrueName",		/*��ʵ����*/
	
		"dwTutorID",		/*��ʦ���ʺţ�*/
		
		"szTutorName",		/*��ʦ����*/
	
		"dwDeptID",		/*����ID*/
		
		"szDeptName",		/*����*/
	
		"dwDevID",		/*�豸ID*/
		
		"szDevName",		/*�豸����*/
	
		"dwRoomID",		/*����ID*/
		
		"szRoomName",		/*�����*/
	
		"dwLabID",		/*ʵ����ID*/
		
		"szLabName",		/*ʵ��������*/
	
		"dwAttendantID",		/*ֵ��Ա�˺�*/
		
		"szAttendantName",		/*ֵ��Ա����*/
	
		"szAttendantTel",		/*ֵ��Ա�绰*/
	
		"dwSubjectID",		/*����ID*/
		
		"dwOccurDate",		/*��������*/
		
		"dwOccurTime",		/*����ʱ��*/
		
		"dwThisUseCScore",		/*����ʹ�û���*/
		
		"dwLeftCScore",		/*�ۼƷ���*/
		
      "dwUserCStat",		/*�û�����״̬*/
		
		"dwForbidStartDate",		/*���ÿ�ʼʱ��*/
		
		"dwForbidEndDate",		/*���ý���ʱ��*/
		
		"szMemo",		/*˵��*/
	 ""};

	/*��ȡϵͳ��������*/
	static public string[] SYSFUNCREQ = new string[]{
		
		"dwSFSN",		/*���ܱ��*/
		 ""};

	/*ϵͳ���ܶ���*/
	static public string[] SYSFUNC = new string[]{
		
		"dwSFSN",		/*���ܱ��*/
		
		"szSFName",		/*��������*/
	
		"szURL",		/*ʹ����ϸ���ܵ�URL*/
	
		"szMemo",		/*˵��*/
	 ""};

	/*��ȡ�ʸ����*/
	static public string[] SYSFUNCRULEREQ = new string[]{
		
		"dwSFRuleID",		/*����ʹ�ù���ID*/
		
		"dwLabID",		/*ʵ����ID*/
		
		"dwAuthType",		/*��Ȩ���*/
		
		"dwScopeKind",		/*���÷�Χ����*/
		
		"dwScopeID",		/*��ΧID(����dwScopeKind���岻ͬ)*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*ϵͳ����ʹ�ù���*/
	static public string[] SYSFUNCRULE = new string[]{
		
		"dwSFRuleID",		/*����ʹ�ù���ID*/
		
		"szSFRuleName",		/*��������*/
	
		"dwSFSN",		/*���ܱ��*/
		
		"szSFName",		/*��������*/
	
		"szSFURL",		/*ʹ����ϸ���ܵ�URL*/
	
		"dwLabID",		/*ʵ����ID*/
		
		"szLabName",		/*ʵ��������*/
	
      "dwScopeKind",		/*���÷�Χ����*/
		
		"dwScopeID",		/*��ΧID(����dwScopeKind���岻ͬ)*/
		
		"dwIdent",		/*��ݣ�0��ʾ�����ƣ�*/
		
		"dwDeptID",		/*���ţ�0��ʾ�����ƣ�*/
		
		"dwGroupID",		/*ָ���û��飨0��ʾ�����ƣ�*/
		
		"dwPriority",		/*���ȼ�(���ִ�������ȼ���)*/
		
      "dwAuthType",		/*��Ȩ���*/
		
      "dwAuthMode",		/*��Ȩģʽ*/
		
		"szIntrInfo",		/*ʹ��˵��*/
	
		"dwDefaultPeriod",		/*ȱ����Ч����(��)*/
		
		"szMemo",		/*˵��*/
	 ""};

	/*��ȡ�û�ϵͳ�����ʸ��*/
	static public string[] SFROLEINFOREQ = new string[]{
		
		"dwSFSN",		/*���ܱ��*/
		
		"dwSFRuleID",		/*����ʹ�ù���ID*/
		
		"dwLabID",		/*ʵ����ID*/
		
		"dwScopeKind",		/*���÷�Χ����*/
		
		"dwScopeID",		/*��ΧID(����dwScopeKind���岻ͬ)*/
		
		"dwStatus",		/*״̬*/
		
		"dwAuthType",		/*��Ȩ���*/
		
		"dwApplyID",		/*����ID*/
		
		"dwAccNo",		/*�˺�*/
		
		"dwTargetID",		/*�������ID(ʹ�����˺Ż������ĿID�ţ�*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*�û�ϵͳ�����ʸ��*/
	static public string[] SFROLEINFO = new string[]{
		
		"dwSFSN",		/*���ܱ��*/
		
		"szSFName",		/*��������*/
	
		"szSFURL",		/*ʹ����ϸ���ܵ�URL*/
	
		"dwSFRuleID",		/*����ʹ�ù���ID*/
		
		"szSFRuleName",		/*��������*/
	
		"szIntrInfo",		/*ʹ��˵��*/
	
		"dwLabID",		/*ʵ����ID*/
		
		"szLabName",		/*ʵ��������*/
	
		"dwAuthType",		/*��Ȩ���*/
		
      "dwStatus",		/*״̬��ǰ8�ֹ���Ա���״̬��*/
		
		"dwApplyID",		/*����ID*/
		
		"dwAccNo",		/*�˺�*/
		
		"szPID",		/*ѧ����*/
	
		"szTrueName",		/*����*/
	
		"dwDeptID",		/*����ID*/
		
		"szDeptName",		/*����*/
	
		"szTel",		/*�绰*/
	
		"szHandPhone",		/*�ֻ�*/
	
		"szEmail",		/*����*/
	
		"dwTutorID",		/*��ʦ�˺�*/
		
		"szTutorName",		/*��ʦ����*/
	
		"dwTargetID",		/*�������ID(ʹ�����˺Ż������ĿID�ţ�*/
		
		"szTargetName",		/*�����������(ʹ���������������Ŀ���ƣ�*/
	
		"dwApplyDate",		/*��������*/
		
		"dwApplyTime",		/*����ʱ��*/
		
		"dwApplyUseTime",		/*����ʹ��ʱ�䣨���ӣ�*/
		
		"dwTesteeNum",		/*ʹ������*/
		
		"dwUseTimes",		/*����ʹ�ô���*/
		
		"dwUseMinATime",		/*����ÿ��ʹ��ʱ��(����)*/
		
		"szApplyInfo",		/*��ϸ����*/
	
		"szApplyURL",		/*�������뱨���URL*/
	
		"dwAdminID",		/*����Ա�˺�*/
		
		"dwCheckDate",		/*�������*/
		
		"dwCheckTime",		/*���ʱ��*/
		
		"dwPermitUseTime",		/*����ʹ��ʱ�䣨���ӣ�*/
		
		"dwDeadLine",		/*�����ֹʱ��*/
		
		"szCheckInfo",		/*������*/
	
		"dwUsedTimes",		/*��ʹ�ô���*/
		
		"dwUsedTime",		/*�Ѿ�ʹ��ʱ�䣨���ӣ�*/
		
		"szMemo",		/*˵��*/
	 ""};

	/*��ȡ������Ϣ�������*/
	static public string[] CODINGTABLEREQ = new string[]{
		
		"dwCodeType",		/*�������*/
		
		"szCodeSN",		/*����*/
	
		"szCodeName",		/*��������*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*������Ϣ��*/
	static public string[] CODINGTABLE = new string[]{
		
      "dwCodeType",		/*�������*/
		
		"szCodeSN",		/*����*/
	
		"szCodeName",		/*��������*/
	
		"szExtValue",		/*��չ*/
	
		"szMemo",		/*��ע*/
	 ""};

	/*��ȡ�����԰��������*/
	static public string[] MULTILANLIBREQ = new string[]{
		
		"dwLanSN",		/*���Ա��*/
		
		"dwSubSysSN",		/*��ϵͳ���*/
		
		"dwTextID",		/*����ID*/
		 ""};

	/*�����԰�*/
	static public string[] UNIMULTILANLIB = new string[]{
		
      "dwLanSN",		/*���Ա��*/
		
      "dwSubSysSN",		/*��ϵͳ���*/
		
      "dwTextID",		/*����ID*/
		
		"szTextInfo",		/*��������*/
	
		"szMemo",		/*��ע*/
	 ""};

	/*ϵͳˢ������*/
	static public string[] SYSREFRESHREQ = new string[]{
		
		"dwRefreshMod",		/*ˢ��ģ��(��չ)*/
		 ""};

}
	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/
public partial class Struct_ST
{

	/*��ȡ�ʲ��б�*/
	static public string[] ASSERTREQ = new string[]{
		
		"dwDevID",		/*�豸ID*/
		
		"szDevName",		/*ʵ���豸����*/
	
		"szAssertSN",		/*�ʲ����*/
	
		"szTagID",		/*RFID��ǩID*/
	
		"szLabIDs",		/*ʵ����ID,����ö��Ÿ���*/
	
		"szRoomIDs",		/*����ID,����ö��Ÿ���*/
	
		"szKindIDs",		/*��������,����ö��Ÿ���*/
	
		"szClassIDs",		/*�����������ID,����ö��Ÿ���*/
	
		"szDeptIDs",		/*ѧԺID,����ö��Ÿ���*/
	
		"szBuildingIDs",		/*¥��ID,����ö��Ÿ���*/
	
		"szCampusIDs",		/*У��ID,����ö��Ÿ���*/
	
		"dwDevStat",		/*�豸״̬*/
		
		"dwProperty",		/*�豸����*/
		
		"dwClassKind",		/*���(��UNIDEVCLS��Kind����)*/
		
		"dwMinUnitPrice",		/*��ͼ۸�*/
		
		"dwMaxUnitPrice",		/*���۸�*/
		
		"dwSPurchaseDate",		/*��ʼ�ɹ�����*/
		
		"dwEPurchaseDate",		/*��ֹ�ɹ�����*/
		
		"dwKeeperID",		/*�������˺�*/
		
		"szKeeperName",		/*����������*/
	
		"dwProducerID",		/*������ID*/
		
		"szProducerName",		/*����������*/
	
		"dwSellerID",		/*��Ӧ��ID*/
		
		"szSellerName",		/*��Ӧ������*/
	
		"dwServiceID",		/*ά����λID*/
		
		"szServiceName",		/*ά����λ����*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*�ʲ�������*/
	static public string[] ROOMCHG = new string[]{
		
		"dwDevID",		/*�豸ID*/
		
		"dwOldRoomID",		/*�ɷ���ID*/
		
		"szOldRoomName",		/*�ɷ�������*/
	
		"dwNewRoomID",		/*�·���ID*/
		
		"szNewRoomName",		/*�·�������*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*�ʲ������˱��*/
	static public string[] KEEPERCHG = new string[]{
		
		"dwDevID",		/*�豸ID*/
		
		"dwOldKeeperID",		/*���������˺�*/
		
		"szOldKeeperName",		/*������������*/
	
		"dwNewKeeperID",		/*���������˺�*/
		
		"szNewKeeperName",		/*������������*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*�ʲ���Ϣ*/
	static public string[] UNIASSERT = new string[]{
		
		"dwDevID",		/*�豸ID*/
		
		"szAssertSN",		/*�û����ʲ����*/
	
		"szTagID",		/*RFID��ǩID*/
	
		"szOriginSN",		/*ԭ��ϵ�к�*/
	
		"szDevName",		/*ʵ���豸����*/
	
		"dwUnitPrice",		/*�豸����*/
		
		"dwPurchaseDate",		/*�ɹ����� YYYYMMDD*/
		
		"dwDevStat",		/*�豸״̬*/
		
		"dwClassID",		/*�豸�������*/
		
		"szClassSN",		/*�豸�����*/
	
		"szClassName",		/*�豸�������*/
	
		"dwClassKind",		/*���(��UNIDEVCLS��Kind����)*/
		
		"dwKindID",		/*�豸����*/
		
		"szKindName",		/*�豸����*/
	
		"szFuncCode",		/*�豸������;����*/
	
		"szModel",		/*�豸�ͺ�*/
	
		"szSpecification",		/*�豸���*/
	
		"dwProperty",		/*�豸���ԣ�ǰ16��ΪUNIDEVKIND����*/
		
		"dwRoomID",		/*����ID*/
		
		"szRoomNo",		/*����ţ����ظ�,���ڹ����Ž�����ʽB-F-N,�������-¥���-�����ţ�*/
	
		"szRoomName",		/*��������*/
	
		"szFloorNo",		/*����¥��*/
	
		"dwBuildingID",		/*¥��ID*/
		
		"szBuildingNo",		/*��¥���(*/
	
		"szBuildingName",		/*��¥����(*/
	
		"dwLabID",		/*ʵ����ID*/
		
		"szLabSN",		/*ʵ���ұ��*/
	
		"szLabName",		/*ʵ��������*/
	
		"dwDeptID",		/*����ID*/
		
		"szDeptName",		/*����*/
	
		"dwCampusID",		/*У��ID*/
		
		"szCampusName",		/*У������*/
	
		"dwCampusKind",		/*У�����ͣ���չ��*/
		
		"dwKeeperID",		/*�������˺�*/
		
		"szKeeperName",		/*����������*/
	
		"szKeeperTel",		/*�����˵绰*/
	
		"dwProducerID",		/*������ID*/
		
		"szProducerName",		/*����������*/
	
		"dwSellerID",		/*��Ӧ��ID*/
		
		"szSellerName",		/*��Ӧ������*/
	
		"dwServiceID",		/*ά����λID*/
		
		"szServiceName",		/*ά����λ����*/
	
		"szServiceTel",		/*ά���绰*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*�ʲ�����*/
	static public string[] RFIDBIND = new string[]{
		
		"dwLabID",		/*ʵ����ID*/
		
		"dwDevID",		/*�豸ID*/
		
		"szTagID",		/*RFID��ǩID*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*��ȡ�ʲ��̵��*/
	static public string[] STOCKTAKINGREQ = new string[]{
		
		"dwSTID",		/*�ʲ��̵�ID*/
		
		"dwSTStat",		/*�̵�״̬*/
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*�ʲ��̵��*/
	static public string[] STOCKTAKING = new string[]{
		
		"dwSTID",		/*�ʲ��̵�ID*/
		
		"dwSTDate",		/*�ʲ��̵�����*/
		
		"dwSTEndDate",		/*�ʲ��̵��������*/
		
      "dwSTStat",		/*�̵�״̬*/
		
		"dwLabID",		/*ʵ����ID*/
		
		"szLabName",		/*ʵ��������*/
	
		"dwRoomID",		/*����ID*/
		
		"szRoomName",		/*��������*/
	
		"dwKindID",		/*��������*/
		
		"szKindName",		/*�豸����*/
	
		"dwClassID",		/*�����������ID*/
		
		"szClassName",		/*�豸�������*/
	
		"dwClassKind",		/*���(��UNIDEVCLS��Kind����)*/
		
		"dwAttendantID",		/*������ID*/
		
		"szAttendantName",		/*����������*/
	
		"dwMinUnitPrice",		/*��ͼ۸�*/
		
		"dwMaxUnitPrice",		/*���۸�*/
		
		"dwLeaderID",		/*�̵㸺����ID*/
		
		"szLeaderName",		/*�̵㸺��������*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*��ȡ�̵��ʲ���ϸ��*/
	static public string[] STDETAILREQ = new string[]{
		
		"dwSTID",		/*�ʲ��̵�ID*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*�ʲ��̵���ϸ��Ϣ*/
	static public string[] STDETAIL = new string[]{
		
		"dwSTID",		/*�ʲ��̵�ID*/
		
		"dwDevID",		/*�豸ID*/
		
      "dwSTStat",		/*�̵�״̬*/
		
		"dwSTDate",		/*�ʲ��̵�����*/
		
		"dwLeaderID",		/*�̵㸺����ID*/
		
		"szLeaderName",		/*�̵㸺��������*/
	
		"dwRoomID",		/*����ID*/
		
		"dwAttendantID",		/*�������˺�*/
		
		"szAttendantName",		/*����������*/
	
		"szSTInfo",		/*�̵��������*/
	
		"szAssertSN",		/*�û����ʲ����*/
	
		"szTagID",		/*RFID��ǩID*/
	
		"szOriginSN",		/*ԭ��ϵ�к�*/
	
		"szDevName",		/*ʵ���豸����*/
	
		"dwUnitPrice",		/*�豸����*/
		
		"dwPurchaseDate",		/*�ɹ����� YYYYMMDD*/
		
		"dwClassID",		/*�豸�������*/
		
		"szClassSN",		/*�豸�����*/
	
		"szClassName",		/*�豸�������*/
	
		"dwClassKind",		/*���(��UNIDEVCLS��Kind����)*/
		
		"dwKindID",		/*�豸����*/
		
		"szKindName",		/*�豸����*/
	
		"szFuncCode",		/*�豸������;����*/
	
		"szModel",		/*�豸�ͺ�*/
	
		"szSpecification",		/*�豸���*/
	
		"dwProperty",		/*�豸���ԣ�ǰ16��ΪUNIDEVKIND����*/
		
		"szRoomNo",		/*����ţ����ظ�,���ڹ����Ž�����ʽB-F-N,�������-¥���-�����ţ�*/
	
		"szRoomName",		/*��������*/
	
		"szFloorNo",		/*����¥��*/
	
		"dwBuildingID",		/*¥��ID*/
		
		"szBuildingNo",		/*��¥���(*/
	
		"szBuildingName",		/*��¥����(*/
	
		"dwLabID",		/*ʵ����ID*/
		
		"szLabSN",		/*ʵ���ұ��*/
	
		"szLabName",		/*ʵ��������*/
	
		"dwDeptID",		/*����ID*/
		
		"szDeptName",		/*����*/
	
		"dwCampusID",		/*У��ID*/
		
		"szCampusName",		/*У������*/
	
		"dwCampusKind",		/*У�����ͣ���չ��*/
		
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*��ȡ�豸���ϼ�¼��*/
	static public string[] OUTOFSERVICEREQ = new string[]{
		
		"dwOOSID",		/*�豸���ϼ�¼ID*/
		
		"dwOOSStat",		/*����״̬*/
		
		"dwOOSType",		/*��������*/
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*�����豸*/
	static public string[] OOSDEV = new string[]{
		
		"dwOOSID",		/*�豸���ϼ�¼ID*/
		
		"dwDevID",		/*�豸ID*/
		
		"szAssertSN",		/*�û����ʲ����*/
	
		"szDevName",		/*ʵ���豸����*/
	
		"dwUnitPrice",		/*�豸����*/
		
		"dwPurchaseDate",		/*�ɹ����� YYYYMMDD*/
		
		"szKindName",		/*�豸����*/
	
		"szRoomName",		/*��������*/
	
		"dwLabID",		/*ʵ����ID*/
		
		"szLabName",		/*ʵ��������*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*�豸���ϼ�¼��*/
	static public string[] OUTOFSERVICE = new string[]{
		
		"dwOOSID",		/*�豸���ϼ�¼ID*/
		
      "dwOOSStat",		/*����״̬*/
		
		"dwOOSType",		/*��������*/
		
		"szOOSInfo",		/*������Ϣ*/
	
		"dwApplyDate",		/*��������*/
		
		"dwApplyID",		/*������ID*/
		
		"szApplyName",		/*����������*/
	
		"dwApproveDate",		/*��������*/
		
		"dwApproveID",		/*������ID*/
		
		"szApproveName",		/*����������*/
	
		"szMemo",		/*˵����Ϣ*/
	
	"OOSDev",		/*CUniTable[OOSDEV]*/
	 ""};

	/*��ȡ�����豸��ϸ��*/
	static public string[] OOSDETAILREQ = new string[]{
		
		"dwOOSID",		/*�豸���ϼ�¼ID*/
		
		"dwOOSStat",		/*����״̬*/
		
		"dwOOSType",		/*��������*/
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*�����豸��ϸ*/
	static public string[] OOSDETAIL = new string[]{
		
		"dwOOSID",		/*�豸���ϼ�¼ID*/
		
		"dwOOSStat",		/*����״̬*/
		
		"dwOOSType",		/*��������*/
		
		"szOOSInfo",		/*������Ϣ*/
	
		"dwApplyDate",		/*��������*/
		
		"dwApplyID",		/*������ID*/
		
		"szApplyName",		/*����������*/
	
		"dwApproveDate",		/*��������*/
		
		"dwApproveID",		/*������ID*/
		
		"szApproveName",		/*����������*/
	
		"dwDevID",		/*�豸ID*/
		
		"szAssertSN",		/*�û����ʲ����*/
	
		"szTagID",		/*RFID��ǩID*/
	
		"szOriginSN",		/*ԭ��ϵ�к�*/
	
		"szDevName",		/*ʵ���豸����*/
	
		"dwUnitPrice",		/*�豸����*/
		
		"dwPurchaseDate",		/*�ɹ����� YYYYMMDD*/
		
		"dwClassID",		/*�豸�������*/
		
		"szClassSN",		/*�豸�����*/
	
		"szClassName",		/*�豸�������*/
	
		"dwClassKind",		/*���(��UNIDEVCLS��Kind����)*/
		
		"dwKindID",		/*�豸����*/
		
		"szKindName",		/*�豸����*/
	
		"szFuncCode",		/*�豸������;����*/
	
		"szModel",		/*�豸�ͺ�*/
	
		"szSpecification",		/*�豸���*/
	
		"dwProperty",		/*�豸���ԣ�ǰ16��ΪUNIDEVKIND����*/
		
		"dwRoomID",		/*����ID*/
		
		"szRoomNo",		/*����ţ����ظ�,���ڹ����Ž�����ʽB-F-N,�������-¥���-�����ţ�*/
	
		"szRoomName",		/*��������*/
	
		"szFloorNo",		/*����¥��*/
	
		"dwBuildingID",		/*¥��ID*/
		
		"szBuildingNo",		/*��¥���(*/
	
		"szBuildingName",		/*��¥����(*/
	
		"dwLabID",		/*ʵ����ID*/
		
		"szLabSN",		/*ʵ���ұ��*/
	
		"szLabName",		/*ʵ��������*/
	
		"dwDeptID",		/*����ID*/
		
		"szDeptName",		/*����*/
	
		"dwCampusID",		/*У��ID*/
		
		"szCampusName",		/*У������*/
	
		"dwCampusKind",		/*У�����ͣ���չ��*/
		
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*�豸��������*/
	static public string[] REPAIRAPPLY = new string[]{
		
		"dwSID",		/*����ID*/
		
		"dwDevID",		/*�豸ID*/
		
		"szDevName",		/*�豸����*/
	
		"szAssertSN",		/*�û����ʲ����*/
	
		"dwDamageDate",		/*������*/
		
		"dwDamageTime",		/*��ʱ��*/
		
		"szDamageInfo",		/*��˵��*/
	
		"dwManID",		/*������ID*/
		
		"szManName",		/*����������*/
	
		"szMemo",		/*˵��*/
	 ""};

	/**/
	static public string[] REPAIROVER = new string[]{
		
		"dwSID",		/*SID*/
		
		"dwDevID",		/*�豸ID*/
		
		"szDevName",		/*�豸����*/
	
		"szAssertSN",		/*�û����ʲ����*/
	
		"dwStatus",		/*DEVDAMAGEREC����*/
		
		"dwRepareDate",		/*ά������*/
		
		"dwRepareTime",		/*ά��ʱ��*/
		
		"szRepareInfo",		/*ά��˵��*/
	
		"dwRepareCost",		/*ά�޷���*/
		
		"szFundsNo1",		/*���ѿ����1*/
	
		"dwPay1",		/*���ѿ�1֧��*/
		
		"szFundsNo2",		/*���ѿ����2*/
	
		"dwPay2",		/*���ѿ�2֧��*/
		
		"szRepareCom",		/*ά�޵�λ*/
	
		"szRepareComTel",		/*ά�޵�λ��ϵ��ʽ*/
	
		"szMemo",		/*˵��*/
	 ""};

	/**/
	static public string[] REPAIRCANCEL = new string[]{
		
		"dwSID",		/*SID*/
		
		"dwDevID",		/*�豸ID*/
		
		"szDevName",		/*�豸����*/
	
		"szAssertSN",		/*�û����ʲ����*/
	
		"dwDevStat",		/*�������豸״̬*/
		
		"szCancelInfo",		/*����˵��*/
	
		"szMemo",		/*˵��*/
	 ""};

	/**/
	static public string[] COMPANYREQ = new string[]{
		
		"dwComID",		/*��λID*/
		
		"dwComKind",		/*��λ����*/
		
		"dwProperty",		/*����*/
		
		"szComName",		/*��λ��*/
	
		"szSearchKey",		/*�����ؼ���*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/**/
	static public string[] UNICOMPANY = new string[]{
		
		"dwComID",		/*��λID*/
		
		"szComName",		/*��λ��*/
	
      "dwComKind",		/*��λ����*/
		
		"dwProperty",		/*����*/
		
		"szTrueName",		/*��ϵ������*/
	
		"szJobTitle",		/*ְ��*/
	
		"szTel",		/*�绰*/
	
		"szHandPhone",		/*�ֻ�*/
	
		"szEmail",		/*����*/
	
		"szQQ",		/*QQ*/
	
		"szAddress",		/*��ַ*/
	
		"szOtherContact",		/*������ϵ��ʽ*/
	
		"szKeyWords",		/*�ؼ���*/
	
		"szMemo",		/*��ע*/
	 ""};

	/*��ȡ�豸��ʷ������ϸ��*/
	static public string[] ASSERTLOGREQ = new string[]{
		
		"dwSID",		/*�豸���ϼ�¼ID*/
		
		"dwDevID",		/*�豸ID*/
		
		"dwOpKind",		/*��־����*/
		
		"dwOperatorID",		/*����ԱID*/
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*�豸��ʷ����*/
	static public string[] ASSERTLOG = new string[]{
		
		"dwSID",		/*��־ID*/
		
      "dwOpKind",		/*��־����*/
		
		"dwOpDate",		/*����*/
		
		"dwOpTime",		/*ʱ��*/
		
		"szOpDetail",		/*��ϸ��Ϣ*/
	
		"dwOperatorID",		/*����ԱID*/
		
		"szOperatorName",		/*����Ա����*/
	
		"dwDevID",		/*�豸ID*/
		
		"szAssertSN",		/*�û����ʲ����*/
	
		"szDevName",		/*ʵ���豸����*/
	
		"dwClassID",		/*�豸�������*/
		
		"szClassName",		/*�豸�������*/
	
		"dwClassKind",		/*���(��UNIDEVCLS��Kind����)*/
		
		"dwKindID",		/*�豸����*/
		
		"szKindName",		/*�豸����*/
	
		"szFuncCode",		/*�豸������;����*/
	
		"szModel",		/*�豸�ͺ�*/
	
		"szSpecification",		/*�豸���*/
	 ""};

}
	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/
public partial class Struct_ST
{

	/*���ڷ���*/
	static public string[] ATTENDROOM = new string[]{
		
		"dwRoomID",		/*����ID*/
		
		"szRoomName",		/*��������*/
	
		"szRoomNo",		/*�����*/
	
		"szFloorNo",		/*����¥��*/
	
		"dwBuildingID",		/*¥��ID*/
		
		"szBuildingNo",		/*��¥���(*/
	
		"szBuildingName",		/*��¥����(*/
	
		"szMemo",		/*��ע*/
	 ""};

	/*��ȡ���ڹ���������*/
	static public string[] ATTENDRULEREQ = new string[]{
		
		"dwAttendID",		/*���ڹ���ID*/
		
		"szAttendName",		/*���ڹ�������*/
	
		"dwKind",		/*��������*/
		
		"szRoomIDs",		/*����ID,����ö��Ÿ���*/
	
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*���ڹ���*/
	static public string[] ATTENDRULE = new string[]{
		
		"dwAttendID",		/*���ڹ���ID*/
		
		"szAttendName",		/*���ڹ�������*/
	
		"dwKind",		/*��������*/
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
		"dwGroupID",		/*��ID*/
		
		"dwOpenRuleSN",		/*���Ź�����*/
		
		"dwEarlyInTime",		/*�������ʱ��(HHMM)*/
		
		"dwLateInTime",		/*�������ʱ��(HHMM)*/
		
		"dwEarlyOutTime",		/*�����뿪ʱ��(HHMM),С�ڽ���ʱ���������*/
		
		"dwLateOutTime",		/*�����뿪ʱ��(HHMM),С�ڽ���ʱ���������*/
		
		"dwMinStayTime",		/*����ͣ��ʱ��*/
		
	"AttendRoom",		/*ATTENDROOM��*/
	
		"szMemo",		/*��ע*/
	 ""};

	/*��ȡ���ڼ�¼�������*/
	static public string[] ATTENDRECREQ = new string[]{
		
		"dwAttendID",		/*���ڹ���ID*/
		
		"szAttendName",		/*���ڹ�������*/
	
		"dwKind",		/*��������*/
		
		"szRoomIDs",		/*����ID,����ö��Ÿ���*/
	
		"dwAccNo",		/*�˺�*/
		
		"dwAttendStat",		/*����״̬*/
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*���ڼ�¼*/
	static public string[] ATTENDREC = new string[]{
		
		"dwSID",		/*��ˮ��*/
		
		"dwAttendID",		/*���ڹ���ID*/
		
		"szAttendName",		/*���ڹ�������*/
	
		"dwAccNo",		/*�˺�*/
		
		"szPID",		/*ѧ����*/
	
		"szTrueName",		/*����*/
	
		"dwSex",		/*�Ա�*/
		
		"dwAttendDate",		/*��������*/
		
		"dwAttendStat",		/*����״̬(������UNIRESVREC)*/
		
		"dwRoomID",		/*����ID*/
		
		"szRoomName",		/*��������*/
	
		"dwInTime",		/*����ʱ��(HHMM)*/
		
		"dwOutTime",		/*�뿪ʱ��(HHMM)*/
		
		"dwLatestInTime",		/*�������ʱ��*/
		
		"dwStayMin",		/*ͣ��ʱ��(����)*/
		
		"dwCardTimes",		/*ˢ������*/
		
		"dwRFLID",		/*request for leave ID*/
		
		"szMemo",		/*��ע*/
	 ""};

	/*������Ϣ*/
	static public string[] ATTENDINFO = new string[]{
		
      "dwAttendMode",		/*���ڽ���ģʽ*/
		
		"dwAccNo",		/*�˺�*/
		
		"dwRoomID",		/*����ID*/
		
		"dwAttendDate",		/*��������*/
		
		"dwSID",		/*��ˮ��*/
		
		"dwAttendID",		/*���ڹ���ID*/
		
		"szAttendName",		/*���ڹ�������*/
	
		"dwAttendStat",		/*����״̬(������UNIRESVREC)*/
		
		"dwInTime",		/*����ʱ��(HHMM)*/
		
		"dwOutTime",		/*�뿪ʱ��(HHMM)*/
		
		"dwLatestInTime",		/*�������ʱ��*/
		
		"dwStayMin",		/*ͣ��ʱ��(����)*/
		
		"dwCardTimes",		/*ˢ������*/
		
		"dwRFLID",		/*request for leave ID*/
		
		"szMemo",		/*��ע*/
	 ""};

	/*��ȡ����ͳ�Ƶ������*/
	static public string[] ATTENDSTATREQ = new string[]{
		
		"dwAttendID",		/*���ڹ���ID*/
		
		"dwAccNo",		/*�˺�*/
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*����ͳ��*/
	static public string[] ATTENDSTAT = new string[]{
		
		"dwAccNo",		/*�˺�*/
		
		"szPID",		/*ѧ����*/
	
		"szTrueName",		/*����*/
	
		"dwSex",		/*�Ա�*/
		
		"dwTotalTimes",		/*�ܴ���*/
		
		"dwAttendTimes",		/*���ڴ���*/
		
		"dwAbsentTimes",		/*ȱ�ڴ���*/
		
		"dwLateTimes",		/*�ٵ�����*/
		
		"dwLeaveTimes",		/*���˴���*/
		
		"dwLLTimes",		/*�ٵ������˴���*/
		
		"dwSickTimes",		/*���ٴ���*/
		
		"dwPrivateTimes",		/*�¼ٴ���*/
		
		"dwUseLessTimes",		/*ʹ��ʱ�䲻������*/
		
		"dwLeaveNoCardTimes",		/*δˢ���뿪����*/
		
		"dwTotalMin",		/*������ʱ�䣨���ӣ�*/
		 ""};

}
	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/
public partial class Struct_ST
{

	/*��ϵͳ��Ϣ*/
	static public string[] SUBSYS = new string[]{
		
		"dwStaSN",		/*��ϵͳ���*/
		
		"szSubSysName",		/*��ϵͳ����*/
	
      "dwStatus",		/*״̬*/
		
		"szVersion",		/*��ϵͳ�������汾*/
	
		"szIP",		/*IP��ַ*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*��ϵͳ��¼����*/
	static public string[] SUBSYSLOGINREQ = new string[]{
		
		"dwStaSN",		/*��ϵͳ���*/
		
		"szVersion",		/*��ϵͳ�������汾*/
	
		"szKey",		/*��Կ(��չ)*/
	
		"szIP",		/*IP��ַ*/
	
		"szMAC",		/*������ַ*/
	
		"dwOldSessionID",		/*�ϴη����sessionֵ*/
		
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*��ϵͳ��¼Ӧ��*/
	static public string[] SUBSYSLOGINRES = new string[]{
		
		"dwSessionID",		/*�����������SessionID*/
		
		"ExtInfo",		/*������չ��Ϣ*/
	
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*��ϵͳ�˳�����*/
	static public string[] SUBSYSLOGOUTREQ = new string[]{
		
		"dwSessionID",		/*�����������SessionID*/
		
		"dwStaSN",		/*��ϵͳ���*/
		 ""};

	/*IC�ռ�ʹ�ü�¼�ϴ�*/
	static public string[] ICUSERECUP = new string[]{
		
		"dwSID",		/*��ˮ��*/
		
		"dwStaSN",		/*��ϵͳ���*/
		
		"dwSubStaSN",		/*��վ����*/
		
		"szLogonName",		/*ѧ����*/
	
		"szTrueName",		/*����*/
	
		"dwUseDate",		/*ʹ������*/
		
		"dwResvBTime",		/*ԤԼ��ʼʱ��*/
		
		"dwResvETime",		/*ԤԼ����ʱ��*/
		
		"dwRealInTime",		/*ʵ�ʿ�ʼʱ��*/
		
		"dwRealOutTime",		/*ʵ�ʽ���ʱ��*/
		
		"dwUseMinutes",		/*ʹ��ʱ��(����)*/
		
		"szUseDev",		/*ʹ���豸*/
	
		"dwDevClsKind",		/*�������(����UNIDEVCLS:dwKind���壩*/
		
		"dwDevKind",		/*�豸����*/
		
		"dwUseMode",		/*ʹ�÷������ο�UNIRESERVE���壩*/
		
		"dwPurpose",		/*��;���ο�UNIRESERVE���壩*/
		
		"dwRealCost",		/*ʵ�ʽ��ɷ���(��)*/
		
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*��ӡ��ӡɨ���¼�ϴ�*/
	static public string[] PRINTRECUP = new string[]{
		
		"dwSID",		/*��ˮ��*/
		
		"dwStaSN",		/*��ϵͳ���*/
		
		"dwSubStaSN",		/*��վ����*/
		
		"szLogonName",		/*ѧ����*/
	
		"szTrueName",		/*����*/
	
		"dwUseDate",		/*ʹ������*/
		
		"dwUseTime",		/*ʹ��ʱ��*/
		
		"szUseDev",		/*ʹ���豸*/
	
		"dwPages",		/*��ӡ����(��ɨ���С��*/
		
		"dwPaperType",		/*ֽ��*/
		
		"dwPrintType",		/*��ӡ����*/
		
		"dwProperty",		/*����*/
		
		"dwRealCost",		/*ʵ�ʽ��ɷ���(��)*/
		
		"dwUnitFee",		/*����*/
		
		"dwPaperNum",		/*ֽ����*/
		
		"dwMaterialFee",		/*���Ϸ�*/
		
		"dwManualFee",		/*�˹���*/
		
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*ͼ�鳬�ڽɷѼ�¼�ϴ�*/
	static public string[] BOOKOVERDUERECUP = new string[]{
		
		"dwSID",		/*��ˮ��*/
		
		"dwStaSN",		/*��ϵͳ���*/
		
		"dwSubStaSN",		/*��վ����*/
		
		"szLogonName",		/*ѧ����*/
	
		"szTrueName",		/*����*/
	
		"dwUseDate",		/*ʹ������*/
		
		"dwUseTime",		/*ʹ��ʱ��*/
		
		"szUseDev",		/*ʹ���豸*/
	
		"szBookName",		/*����*/
	
		"dwRealCost",		/*ʵ�ʽ��ɷ���(��)*/
		
		"szMemo",		/*˵����Ϣ*/
	 ""};

	/*ΥԼ��¼�ϴ�*/
	static public string[] BREACHRECUP = new string[]{
		
		"dwSID",		/*��ˮ��*/
		
		"dwStaSN",		/*��ϵͳ���*/
		
		"dwSubStaSN",		/*��վ����*/
		
		"szLogonName",		/*ѧ����*/
	
		"szTrueName",		/*����*/
	
		"dwOccurDate",		/*ΥԼ����*/
		
		"dwOccurTime",		/*ΥԼʱ��*/
		
		"szBreachName",		/*ΥԼ������*/
	
		"dwPunishScore",		/*���η���*/
		
		"dwTotalScore",		/*�ۼƷ���*/
		
		"dwThresholdScore",		/*�ﵽ������׼�ķ���*/
		
      "dwStatus",		/*����״̬*/
		
		"szPunishName",		/*������ʽ*/
	
		"dwPStartDate",		/*������ʼʱ��*/
		
		"dwPEndDate",		/*��������ʱ��*/
		
		"szMemo",		/*˵����Ϣ*/
	 ""};

}
	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/
public partial class Struct_ST
{

	/*��ȡ���޼䵱ǰ״̬����*/
	static public string[] STUDYROOMSTATREQ = new string[]{
		
		"dwRoomKinds",		/*���޼��������*/
		
		"szBuildingNo",		/*��¥���*/
	 ""};

	/*���޼䵱ǰ״̬*/
	static public string[] STUDYROOMSTAT = new string[]{
		
      "dwRoomKind",		/*���޼�����*/
		
		"dwTotalNum",		/*����*/
		
		"dwIdleNum",		/*������*/
		 ""};

	/*��ȡ��λ��ǰ״̬����*/
	static public string[] SEATSTATREQ = new string[]{
		
		"szBuildingNo",		/*��¥���*/
	
		"szFloorNo",		/*����¥��*/
	 ""};

	/*��λ��ǰ״̬*/
	static public string[] SEATSTAT = new string[]{
		
		"szRoomNo",		/*�����*/
	
		"szRoomName",		/*��������*/
	
		"dwTotalNum",		/*����*/
		
		"dwIdleNum",		/*������*/
		 ""};

}
	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/
public partial class Struct_ST
{

	/*��ȡ����ʵ����������*/
	static public string[] RTDATAREQ = new string[]{
		
		"dwBeginDate",		/*ԤԼ��ʼ����*/
		
		"dwEndDate",		/*ԤԼ��������*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*��ȡ����ʵ������*/
	static public string[] RTDATA = new string[]{
		
		"dwResvID",		/*ԤԼ��*/
		
		"dwProperty",		/*ԤԼ����*/
		
		"dwDevID",		/*�豸ID*/
		
		"szAssertSN",		/*�ʲ����*/
	
		"szTestName",		/*����ʵ������*/
	
		"dwBeginTime",		/*ԤԼ��ʼʱ��*/
		
		"dwEndTime",		/*ԤԼ����ʱ��*/
		
		"dwOwner",		/*ʹ����(������)*/
		
		"szPID",		/*ʹ����ѧ����*/
	
		"szOwnerName",		/*ʹ��������*/
	
		"dwIdent",		/*ʹ������ݣ�У�ڣ������У���ʦ��У�⣩*/
		
		"dwUserDeptID",		/*ʹ���˲���ID*/
		
		"szUserDeptName",		/*ʹ���˲���*/
	
		"szTel",		/*�绰*/
	
		"szHandPhone",		/*�ֻ�*/
	
		"szEmail",		/*����*/
	
		"dwRTID",		/*����ʵ����ĿID*/
		
		"dwRTKind",		/*��������*/
		
		"szRTName",		/*����ʵ������*/
	
		"dwSampleNum",		/*��Ʒ��*/
		
		"dwManID",		/*����ԱID*/
		
		"szManName",		/*����Ա����*/
	
		"dwReceivableCost",		/*Ӧ�ɷ���*/
		
		"dwRealCost",		/*ʵ�ʽ��ɷ���*/
		
		"szDescription",		/*ʵ������*/
	 ""};

}
	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/
public partial class Struct_ST
{

	/**/
	static public string[] STALOGINREQ = new string[]{
		
		"szLicSN",		/*���ϵ�к�*/
	
		"szVersion",		/*�汾*/
	
		"szIP",		/*IP��ַ*/
	
		"szMAC",		/*������ַ*/
	
		"szKey",		/*��Կ(��չ)*/
	
		"dwOldSessionID",		/*�ϴη����sessionֵ*/
		 ""};

	/**/
	static public string[] STALOGINRES = new string[]{
		
		"dwStaID",		/*�ڵ�ID*/
		
		"dwSessionID",		/**/
		
	"LicInfo",		/*��������Ȩ��ϢUNILICENSE*/
	
		"szMemo",		/*��ע*/
	 ""};

	/**/
	static public string[] STALOGOUTREQ = new string[]{
		
		"dwSessionID",		/**/
		
		"szLicSN",		/*���ϵ�к�*/
	 ""};

	/**/
	static public string[] HANDSHAKEREQ = new string[]{
		
		"dwChgFlag",		/*���ر�����޸ĸ��±�־*/
		 ""};

	/**/
	static public string[] HANDSHAKERES = new string[]{
		
		"dwChgFlag",		/*���������صĵ��޸ĸ��±�־*/
		
		"szResChgStat",		/*���صĶ�Ӧ��Ϣ���ޱ�־���ַ�0��ʾ�ޣ��ַ�1��ʾ��*/
	 ""};

	/*ģ������Ϣ�ϴ�*/
	static public string[] MODMONIUP = new string[]{
		
		"dwModSN",		/*��ض˱�ţ�������Ϊ0�����¶���+StaSN*65536 + ��ض˱��)*/
		
		"szModName",		/*��ض�����*/
	
		"dwStatus",		/*��״̬*/
		
		"dwStartTime",		/*��״̬��ʼʱ��*/
		
		"szStatInfo",		/*״̬˵��*/
	
		"szMemo",		/*��ע*/
	 ""};

	/*���ָ���ϴ�*/
	static public string[] MONINDEXUP = new string[]{
		
		"dwModSN",		/*��ض˱�ţ�������Ϊ0������Ϊ����ı�Ż�ID��)*/
		
		"dwMoniSN",		/*���ָ����*/
		
		"szIndexName",		/*���ָ������*/
	
		"dwNormalValue",		/*����ֵ*/
		
		"dwCurValue",		/*��ǰֵ*/
		
		"dwStatus",		/*״̬*/
		
		"dwNormalTime",		/*����ʱ��(����)*/
		
		"dwAbnormalTime",		/*�쳣ʱ��(����)*/
		
		"dwAbnormalTimes",		/*�쳣����*/
		
		"dwStartTime",		/*��״̬��ʼʱ��*/
		
		"szStatInfo",		/*״̬˵��*/
	
		"szMemo",		/*��ע*/
	 ""};

	/*��ؼ�¼�ϴ�*/
	static public string[] MONIRECUP = new string[]{
		
		"dwSID",		/*������ˮ��*/
		
		"dwModSN",		/*��ض˱�ţ�������Ϊ0������Ϊ����ı�Ż�ID��)*/
		
		"dwMoniSN",		/*���ָ����*/
		
		"szIndexName",		/*���ָ������*/
	
		"dwCurValue",		/*��ǰֵ*/
		
		"dwOccurDate",		/*��ʼ����*/
		
		"dwOccurTime",		/*����ʱ��*/
		
		"dwStatus",		/*״̬*/
		
		"dwNormalTime",		/*����ʱ��(����)*/
		
		"dwAbnormalTime",		/*�쳣ʱ��(����)*/
		
		"dwAbnormalTimes",		/*�쳣����*/
		
		"szStatInfo",		/*״̬˵��*/
	 ""};

}
	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/
public partial class Struct_ST
{

	/*��ȡ�����Ϣȱʡֵ*/
	static public string[] MONIREQ = new string[]{
		
		"dwModKind",		/*��ģ�����MODKIND_XXX����)*/
		
		"dwStaSN",		/*վ����*/
		
		"dwModSN",		/*��ض˱��*/
		
		"dwStatus",		/*״̬*/
		
      "dwReqProp",		/*���󸽼�����*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*���ָ����*/
	static public string[] MONINDEX = new string[]{
		
		"dwModSN",		/*��ض˱�ţ�������Ϊ0������Ϊ����ı�Ż�ID��)*/
		
		"szModName",		/*��ض�����*/
	
      "dwMoniSN",		/*���ָ����*/
		
		"szIndexName",		/*���ָ������*/
	
		"dwNormalValue",		/*����ֵ*/
		
		"dwCurValue",		/*��ǰֵ*/
		
      "dwStatus",		/*״̬*/
		
		"dwNormalTime",		/*����ʱ��(����)*/
		
		"dwAbnormalTime",		/*�쳣ʱ��(����)*/
		
		"dwAbnormalTimes",		/*�쳣����*/
		
		"dwStartTime",		/*��״̬��ʼʱ��*/
		
		"szStatInfo",		/*״̬˵��*/
	
		"szMemo",		/*��ע*/
	 ""};

	/*ģ������Ϣ*/
	static public string[] MODMONI = new string[]{
		
      "dwModSN",		/*��ض˱�ţ�������Ϊ0�����¶���+StaSN*65536 + ��ض˱��)*/
		
		"szModName",		/*��ض�����*/
	
		"dwStatus",		/*��״̬*/
		
		"dwStartTime",		/*��״̬��ʼʱ��*/
		
		"szStatInfo",		/*״̬˵��*/
	
	"MoniIndexTbl",		/*ָ���б�*/
	
		"szMemo",		/*��ע*/
	 ""};

	/*��ȡ�����Ϣȱʡֵ*/
	static public string[] MONIRECREQ = new string[]{
		
		"dwStartDate",		/*��ʼ����*/
		
		"dwEndDate",		/*��������*/
		
		"dwModKind",		/*��ģ�����MODKIND_XXX����)*/
		
		"dwStaSN",		/*վ����*/
		
		"dwModSN",		/*ģ����*/
		
		"dwMoniSN",		/*���ָ����*/
		
		"dwStatus",		/*״̬*/
		
	"szReqExtInfo",		/*CUniStruct[REQEXTINFO]*/
	 ""};

	/*��ؼ�¼*/
	static public string[] MONIREC = new string[]{
		
		"dwModSN",		/*��ض˱�ţ�������Ϊ0������Ϊ����ı�Ż�ID��)*/
		
		"szModName",		/*��ض�����*/
	
		"dwMoniSN",		/*���ָ����*/
		
		"szIndexName",		/*���ָ������*/
	
		"dwNormalValue",		/*����ֵ*/
		
		"dwCurValue",		/*��ǰֵ*/
		
		"dwOccurDate",		/*��ʼ����*/
		
		"dwOccurTime",		/*����ʱ��*/
		
		"dwStatus",		/*״̬*/
		
		"dwNormalTime",		/*����ʱ��(����)*/
		
		"dwAbnormalTime",		/*�쳣ʱ��(����)*/
		
		"dwAbnormalTimes",		/*�쳣����*/
		
		"szStatInfo",		/*״̬˵��*/
	 ""};

	/*������*/
	static public string[] MONIDEALERR = new string[]{
		
		"dwModSN",		/*��ض˱�ţ�������Ϊ0������Ϊ����ı�Ż�ID��)*/
		
		"dwMoniSN",		/*���ָ����*/
		
		"dwNormalValue",		/*����ֵ*/
		
		"dwCurValue",		/*��ǰֵ*/
		
		"szDealInfo",		/*˵����Ϣ*/
	 ""};

}
	/*�������ݽṹ*/

	/*��ʼ���ݽṹ*/
public partial class Struct_ST
{

	/*����������ڵ�ͨ�Ų���*/
	static public string[] UNISTAPARAM = new string[]{
		
		"dwReqUID",		/*����UID*/
		
		"szReqData",		/*��������*/
	
		"dwResCode",		/*�����룬0��ʾ�ɹ�*/
		
		"szResData",		/*��������*/
	 ""};

	/*��ɹ���ģ����Ϣ*/
	static public string[] LICMOD = new string[]{
		
      "dwFuncSN",		/*����ģ����*/
		
		"dwLicNum",		/*��Ӧ����ģ��ڵ���*/
		
		"szModName",		/*��Ȩģ������*/
	 ""};

	/*�����Ϣ*/
	static public string[] UNILICENSE = new string[]{
		
		"szLicSN",		/*��ɱ��*/
	
		"dwInstDate",		/*��װ����*/
		
		"dwLicExpDate",		/*��ɵ�����*/
		
		"dwServiceExpDate",		/*��������*/
		
		"szLicTo",		/*��Ȩ�ͻ�����*/
	
		"szLicProName",		/*��Ȩ��Ʒ����*/
	
		"szCompanyName",		/*��˾����*/
	
      "dwWarrant",		/*��һ��ͨ�Խ�ģʽ*/
		
		"dwLicStaNum",		/*���վ����*/
		
	"LicMod",		/*LICMOD�ṹ��*/
	
		"szCtrlCode",		/*������*/
	 ""};

	/*��ȡ����������Ϣ*/
	static public string[] REQEXTINFO = new string[]{
		
		"dwStartLine",		/*��ʼ��*/
		
		"dwNeedLines",		/*���ȡ����*/
		
		"dwTotolLines",		/*����˷���������*/
		
		"szOrderKey",		/*�����ֶ�*/
	
		"szOrderMode",		/*����ʽ(ASC��DESC)*/
	
	"ExtInfo",		/*���ݲ�ͬ�����������չ��Ϣ*/
	 ""};

}
	/*�������ݽṹ*/

}
//