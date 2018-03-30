
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
using UniStruct;
using UniWebLib;

/// <summary>
/// UniInterface 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public partial class UniInterface : UniBaseService
{
    public UniInterface () {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

}
//