using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using UniWebLib;

public partial class _Default : UniPage
{
    protected bool bSet = false;
    protected string m_Title = "";
    protected string m_dwCtrlMode = "";
    protected UNIDEVICE[] m_vrResult;
    protected void Page_Load(object sender, EventArgs e)
    {
        string szRoom = Request["room"];

        if (szRoom == null || szRoom == "")
        {
            MessageBox("请勾选房间", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
            return;
        }

       GetDev();
       GetSWCtrlClass();
       CTRLREQ CtrlReq = new CTRLREQ();

       if (IsPostBack)
       {
           GetHTTPObj(out CtrlReq);
         

         
           uint uCtrl = uint.Parse(Request["dwCtrlSN"]);
           CtrlReq.dwCtrl = uCtrl / 1000000;
           CtrlReq.dwCtrlParam = uCtrl % 1000000;
          
           uint uDate = GetDate(Request["dwEndDate"]);
           uint uTime = GetTime(Request["dwEndTime"]);
           string sDate = uDate / 10000 + "-" + uDate % 10000 / 100 + "-" + uDate % 10000 % 100;
           CtrlReq.dwEndTime = Get1970Seconds(sDate) + (uTime / 100 * 60 + uTime % 100);
          

           REQUESTCODE uRes = REQUESTCODE.EXECUTE_FAIL;
           if (m_vrResult ==null)
           {
               MessageBox("未选中房间", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
               return;
           }
           for (int i = 0; i < m_vrResult.GetLength(0);i++ )
           {
               CtrlReq.dwLabID = m_vrResult[i].dwLabID;
               CtrlReq.dwDevID = m_vrResult[i].dwDevID;
               uRes = m_Request.Device.SWCtrl(CtrlReq);
               if (uRes != REQUESTCODE.EXECUTE_SUCCESS)
               {
                   break;
               }
           }
          
           if (uRes != REQUESTCODE.EXECUTE_SUCCESS)
           {
               MessageBox(m_Request.szErrMessage, "设置" + "监控模式" + "失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
           }
           else
           {
               MessageBox("设置" + "监控模式" + "成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
               return;
           }
       }
        
    }

    public void GetSWCtrlClass()
    {
        CTRLCLASSREQ vrClassGet = new CTRLCLASSREQ();

        m_dwCtrlMode += "<option value='0'>" + "无限制" + " </option>";
        vrClassGet.dwCtrlKind = (uint)UNICTRLCLASS.DWCTRLKIND.CTRLKIND_SW;
        UNICTRLCLASS[] vtClass;
        REQUESTCODE uRes = m_Request.Control.GetCtrlClass(vrClassGet, out vtClass);
        if (uRes == REQUESTCODE.EXECUTE_SUCCESS && vtClass != null && vtClass.Length > 0)
        {
           for(int i=0;i<vtClass.Length;i++)
           {
               uint uCtrl = (uint)(vtClass[i].dwCtrlMode) * 1000000 + (uint)(vtClass[i].dwCtrlSN);
               if (((vtClass[i].dwCtrlMode) & (uint)UNICTRLCLASS.DWCTRLMODE.CTRLMODE_FORBID) == (uint)UNICTRLCLASS.DWCTRLMODE.CTRLMODE_FORBID)
               {
                   m_dwCtrlMode += "<option value='" + uCtrl.ToString() + "'>禁止 " + vtClass[i].szCtrlName + " </option>";
               }
               else
               {
                   m_dwCtrlMode += "<option value='" + uCtrl.ToString() + "'>允许 " + vtClass[i].szCtrlName + " </option>";
               }
               
           }
        }
    }

    public void GetDev()
    {
        string szRoom = Request["room"];
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        DEVREQ vrParameter = new DEVREQ();

        if (szRoom != null && szRoom != "")
        {
            vrParameter.szRoomIDs = szRoom;
        }

        vrParameter.szReqExtInfo.dwStartLine = 0;
        vrParameter.szReqExtInfo.dwNeedLines = 999999;
      
        GetPageCtrlValue(out vrParameter.szReqExtInfo);

        uResponse = m_Request.Device.Get(vrParameter, out m_vrResult);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
        {
            
        }
    }
}
