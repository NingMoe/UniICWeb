﻿using System;
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

public partial class Sub_Lab : UniPage
{
    protected string m_szOpts = "";
    protected MyString m_szOut = new MyString();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        RESVREQ vrParameter = new RESVREQ();
        string szCheckStat = Request["dwCheckStat"];
        string szKey = Request["szGetKey"];
        if (szKey != null && szKey != "")
        {
            //vrParameter.dwGetType = (uint)RESVREQ.DWGETTYPE.RESVGET_BYDEVID;
            vrParameter.dwResvID=Parse(szKey);
        }
        string szResvID = Request["delID"];
        if (szResvID != null && szResvID != "")
        {
            UNIRESERVE delResv = new UNIRESERVE();
            delResv.dwResvID = Parse(szResvID);
            m_Request.Reserve.Del(delResv);
        }
        if (!IsPostBack)
        {
            vrParameter.dwBeginDate = GetDate(DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"));
            vrParameter.dwEndDate = GetDate(DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"));

            dwStartDate.Value = DateTime.Now.AddDays(0).ToString("yyyy-MM-dd");
            dwEndDate.Value = DateTime.Now.AddDays(0).ToString("yyyy-MM-dd");
        }
        if (dwStartDate.Value != "" && dwEndDate.Value != "")
        {
            vrParameter.dwBeginDate = GetDate(dwStartDate.Value);
            vrParameter.dwEndDate = GetDate(dwEndDate.Value);
        }
      //  vrParameter.dwClassKind = (uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS;
        vrParameter.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL;
        if (!(szCheckStat == null || szCheckStat == "" || szCheckStat == "0"))
        {
            vrParameter.dwCheckStat = Parse(szCheckStat);
        }
        UNIRESERVE[] vrResult;       
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (m_Request.Reserve.Get(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            int count = 0;
            uint uTimeNow = Get1970Seconds(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            for (int i = 0; i < vrResult.Length; i++)
            {
                uint uState = (uint)vrResult[i].dwStatus;                               
                m_szOut += "<tr>";
                m_szOut += "<td data-id=\"" + vrResult[i].dwResvID.ToString() + "\">" + vrResult[i].dwResvID.ToString() + "</td>";
                m_szOut += "<td class='lnkAccount' data-id='" + vrResult[i].dwOwner.ToString() + "' title='查看个人信息'><a href=\"#\">" + vrResult[i].szOwnerName.ToString() + "</a></td>";                
                string szResvObject = "";
                if ((vrResult[i].dwProperty & (uint)UNIRESERVE.DWPROPERTY.RESVPROP_LOCKROOM) > 0)
                {
                    szResvObject = vrResult[i].ResvDev[0].szRoomName;
                }
                else
                {
                    szResvObject = vrResult[i].ResvDev[0].szDevName;
                }
                m_szOut += "<td>" + szResvObject + "</td>"; 
                m_szOut += "<td>" + GetJustName((vrResult[i].dwStatus), "Reserve_Status") + "</td>";
                m_szOut += "<td>" + Get1970Date((uint)vrResult[i].dwOccurTime, "MM-dd HH:mm") + "</td>";
                m_szOut += "<td>" + Get1970Date((uint)vrResult[i].dwBeginTime, "MM-dd HH:mm") + "到" + Get1970Date((uint)vrResult[i].dwEndTime, "MM-dd HH:mm") + "</td>";
                string szOp = "";
               
                if ((!(((uState & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNSETTLE) > 0)) || !(((uState & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNDO) > 0))))
                {
                    szOp = "'OPTD OPTD" + uState + "'";
                }
                else
                {
                    szOp = "";
                }
                szOp = "'OPTD'";
                m_szOut += "<td><div class=" + szOp + "></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Reserve);
        }
        PutBackValue();
    }   
}
