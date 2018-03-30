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

public partial class Sub_Course : UniPage
{
    protected MyString m_szOut = new MyString();
    protected string szLab = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        UNILAB[] lab = GetAllLab();
        szLab = GetInputItemHtml(CONSTHTML.option, "", "全部", "0");
        for (int i = 0; i < lab.Length; i++)
        {
            szLab =szLab+ GetInputItemHtml(CONSTHTML.option, "", lab[i].szLabName, lab[i].dwLabID.ToString());
        }

        REPORTREQ vrParameter = new REPORTREQ();
        ROOMSTAT[] vrResult;
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (!IsPostBack)
        {
            dwStartDate.Value = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
            dwEndDate.Value = DateTime.Now.ToString("yyyy-MM-dd");

        }

        uint uLabid = Parse(Request["dwLabID"]);
        if (uLabid != 0)
        {
            vrParameter.dwGetType = (uint)REPORTREQ.DWGETTYPE.USERECGET_BYLABID;
            vrParameter.szGetKey = uLabid.ToString();
        }
        vrParameter.dwStartDate = DateToUint(dwStartDate.Value);
        vrParameter.dwEndDate = DateToUint(dwEndDate.Value);

        vrParameter.dwPurpose = ((uint)UNIRESERVE.DWPURPOSE.USEFOR_PERSONNAL + (uint)UNIRESERVE.DWPURPOSE.USEFOR_RESEACH + (uint)UNIRESERVE.DWPURPOSE.USEFOR_TEACHING);
        if (vrParameter.szReqExtInfo.szOrderKey == null)
        {
            vrParameter.szReqExtInfo.szOrderKey = "dwTotalUseTime";
            vrParameter.szReqExtInfo.szOrderMode= "desc";
        }
        if (m_Request.Report.GetRoomStat(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id=" + vrResult[i].dwRoomID.ToString() + ">" + vrResult[i].szLabSN + "</td>";
                m_szOut += "<td>" + vrResult[i].szLabName + "</td>";
                m_szOut += "<td>" + vrResult[i].szRoomNo + "</td>";
                m_szOut += "<td>" + vrResult[i].szRoomName + "</td>";
                m_szOut += "<td>" + vrResult[i].dwTestUseTimes + "</td>";
                m_szOut += "<td>" + vrResult[i].dwTotalNum.ToString() + "</td>";
                m_szOut += "<td>" + (vrResult[i].dwTestUseTimes*vrResult[i].dwTotalTestHour).ToString() + "</td>";              
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Report);
        }

        PutBackValue();
    }
}
