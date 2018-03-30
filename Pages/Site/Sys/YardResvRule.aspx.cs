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
    UNIDEVCLS[] devCls;
    
    protected string m_szOut = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        RESVRULEADMINREQ vrParameter = new RESVRULEADMINREQ();
     
        UNIRESVRULE[] vrResult;
        if (Request["delID"] != null)
        {

            DelResvRule(Request["delID"]);
        }
        if (devCls == null || devCls.Length > 0)
        {
            devCls=GetDevCLS(0);
        }
        string szExtValue = Request["ExtValue"];
        if (szExtValue != null&&szExtValue!="")
        {
            vrParameter.dwExtValue = Parse(szExtValue);
            PutMemberValue("extValue", szExtValue);
        }

        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (m_Request.Reserve.ResvRuleAdminGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szOut += "<tr>";
                m_szOut += "<td data-id=\"" + vrResult[i].dwRuleSN+ "\">" + vrResult[i].szRuleName + "</td>";
                uint? uIdent=vrResult[i].dwIdent;
                string szIdent=uIdent==0?"全部":GetJustNameEqual(uIdent, "Ident");
                m_szOut += "<td>" + szIdent + "</td>";
                m_szOut += "<td>" + GetDevClassName((uint)vrResult[i].dwDevClass) + "</td>";
                m_szOut += "<td>" + GetMinToStr(vrResult[i].dwMinResvTime) + "到" + GetMinToStr(vrResult[i].dwMaxResvTime) + "</td>";
              
                m_szOut += "<td>" + GetMinToStr(vrResult[i].dwLatestResvTime) +"到" + GetMinToStr(vrResult[i].dwEarliestResvTime) +"</td>";             
                uint? uLimit = vrResult[i].dwLimit;
              
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
            UpdatePageCtrl(m_Request.Reserve);
        }
        PutBackValue();        
    }
    private string GetDevClassName(uint uDevCls)
    {
        if(uDevCls==0)
        {
            return "全部";
        }
        for(int i=0;i<devCls.Length;i++)
        {
            if ((uint)devCls[i].dwClassID == uDevCls)
            {
                return devCls[i].szClassName.ToString();
            }
        }
        return "";
    }
    private void DelResvRule(string szID)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        UNIRESVRULE delRule = new UNIRESVRULE();
        delRule.dwRuleSN = Parse(szID);
        uResponse=m_Request.Reserve.ResvRuleDel(delRule);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {

            MessageBox(m_Request.szErrMessage, "提示", MSGBOX.ERROR);
        }
    }
}
