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

public partial class Sub_Room : UniPage
{
    protected MyString m_szOut = new MyString();

    protected void Page_Load(object sender, EventArgs e)
    {
        DOORCARDRECREQ vrParameter = new DOORCARDRECREQ();
        if (!IsPostBack)
        {
            dwStartDate.Value = DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd");
            dwEndDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
        }
        string szPID = Request["dwPID"];
        if (szPID != null && szPID != "")
        {
            UNIACCOUNT accno;
            if(GetAccByLogonName(szPID,out accno))
            {
                vrParameter.dwAccNo = accno.dwAccNo;
            }
        }
        string szKey = Request["szGetKey"];
        if (szKey != null && szKey != "")
        {
            vrParameter.dwGetType = (uint)DOORCARDRECREQ.DWGETTYPE.DOORCARDRECGET_BYROOMID;
            vrParameter.szGetKey =(szKey);
        }
        vrParameter.dwStartDate = DateToUint(dwStartDate.Value);
        vrParameter.dwEndDate = DateToUint(dwEndDate.Value);
        vrParameter.dwCardMode = ((uint)DOORCARDREQ.DWCARDMODE.DOORCARD_IN);
        DOORCARDREC[] vrResult;                      
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
       if (vrParameter.szReqExtInfo.szOrderKey == null || vrParameter.szReqExtInfo.szOrderKey == "")
        {
            vrParameter.szReqExtInfo.szOrderKey = "dwCardTime";
            vrParameter.szReqExtInfo.szOrderMode = "desc";
        }
        
        if (m_Request.Report.GetDoorCardRec(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-time=\"" + Get1970Date((uint)vrResult[i].dwCardTime) + "\" data-roomno=\"" + vrResult[i].szRoomNo + "\">" + vrResult[i].dwSID.ToString() + "</td>";
                m_szOut += "<td>" + vrResult[i].szTrueName + "(" + vrResult[i].szPID+ ")" + "</td>";                
                m_szOut += "<td>" + vrResult[i].szDeptName + "</td>";
                m_szOut += "<td>" + vrResult[i].szRoomName + "</td>";
                m_szOut += "<td>" + Get1970Date((uint)vrResult[i].dwCardTime) + "</td>";
                m_szOut += "<td>" + (vrResult[i].szMemo) + "</td>";
                if (((uint)vrResult[i].dwManMode & (uint)UNIROOM.DWMANMODE.ROOMMAN_CAMERA) > 0)
                {
                    m_szOut += "<td><div class='OPTD'></div></td>";
                }
                else
                {
                    m_szOut += "<td></td>";
                }
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Report);
        }

        PutBackValue();
    }   
}
