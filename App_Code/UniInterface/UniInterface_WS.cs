
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
/// UniInterface ��ժҪ˵��
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public partial class UniInterface : UniBaseService
{
    public UniInterface () {

        //���ʹ����Ƶ��������ȡ��ע�������� 
        //InitializeComponent(); 
    }

}
//