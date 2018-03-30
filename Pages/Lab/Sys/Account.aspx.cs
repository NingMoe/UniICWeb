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

public partial class Sub_Device : UniPage
{
    protected string m_szOpts = "";
    protected MyString m_szOut = new MyString();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        ACCREQ vrParameter = new ACCREQ();
        UNIACCOUNT[] vrResult;
        string delID = Request["delID"];
        if (delID != null && delID != "")
        {
            setUnAble(delID);
        }
        string delUableID = Request["delUableID"];
        if (delUableID != null && delUableID != "")
        {
            setAble(delUableID);
        }
        GetHTTPObj(out vrParameter);
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (m_Request.Account.Get(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id='" + vrResult[i].dwAccNo.ToString()+"'>" + vrResult[i].szLogonName + "</td>";
                m_szOut += "<td>" + vrResult[i].szTrueName + "</td>";
                m_szOut += "<td>" + GetJustNameEqual((uint)vrResult[i].dwSex, "ACCNO_sex") + "</td>";
                uint uIdent = (uint)vrResult[i].dwIdent;
                /*
                if ((uIdent & (uint)UNIACCOUNT.DWIDENT.EXTIDENT_INNER) > 0)
                {
                    uIdent = uIdent - (uint)UNIACCOUNT.DWIDENT.EXTIDENT_INNER;
                }
                else if ((uIdent & (uint)UNIACCOUNT.DWIDENT.EXTIDENT_OUTER) > 0)
                {
                    uIdent = uIdent - (uint)UNIACCOUNT.DWIDENT.EXTIDENT_OUTER;
                }
                else if ((uIdent & (uint)UNIACCOUNT.DWIDENT.EXTIDENT_MANAGER) > 0)
                {
                    uIdent = uIdent - (uint)UNIACCOUNT.DWIDENT.EXTIDENT_MANAGER;
                }*/
                m_szOut += "<td>" + GetJustName(uIdent, "ACCNO_IDENT") + "</td>";
                m_szOut += "<td>" +vrResult[i].szClassName + "</td>";
                m_szOut += "<td>" + vrResult[i].szDeptName + "</td>";
                m_szOut += "<td>" + vrResult[i].szTutorName + "</td>";
                m_szOut += "<td>" + vrResult[i].szHandPhone + "</td>";
                m_szOut += "<td>" + vrResult[i].szEmail + "</td>";
                uint uStatue=(uint)vrResult[i].dwStatus;
                if ((uStatue & 0x40000) > 0)
                {
                    m_szOut += "<td>" + "禁用" + "</td>";
                }
                else
                {
                    m_szOut += "<td>" + "正常"+ "</td>";
                }
                m_szOut += "<td>" + vrResult[i].szMemo + "</td>";
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Account);
        }
        PutBackValue();
    }
    public void setUnAble(string delID)
    {
        UNIACCOUNT setAccno;
        if (GetAccByAccno(delID, out setAccno))
        {
            //#define USERSTAT_LOCALFORBIDDEN			0x20000			//本地禁止
            setAccno.dwStatus = setAccno.dwStatus + 0x40000;
            m_Request.Account.Set(setAccno, out setAccno);
        }
    }
    public void setAble(string delID)
    {
        UNIACCOUNT setAccno;
        if (GetAccByAccno(delID, out setAccno))
        {
            //#define USERSTAT_LOCALFORBIDDEN			0x20000			//本地禁止
            if (((uint)setAccno.dwStatus & 0x40000) > 0)
            {
                setAccno.dwStatus = setAccno.dwStatus - 0x40000;
                m_Request.Account.Set(setAccno, out setAccno);
            }
        }
    }
}
