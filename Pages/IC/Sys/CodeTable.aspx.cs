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

public partial class Sub_Lab : UniPage
{
    protected string m_szOpts = "";
    protected MyString m_szOut = new MyString();
    protected string szTitleName = "";
    protected string szType = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["delID"] != null)
        {
            DelLab(Request["delID"]);
        }

        szTitleName = GetJustNameEqual(Parse(Request["dwCodeType"]), "CodeType", false);

        szType += GetInputItemHtml(CONSTHTML.option, "", "全部", "0");
        szType += GetInputItemHtml(CONSTHTML.option, "", "预约类型", ((uint)CODINGTABLE.DWCODETYPE.CODE_RESVKIND).ToString());
        szType += GetInputItemHtml(CONSTHTML.option, "", "活动类型", ((uint)CODINGTABLE.DWCODETYPE.CODE_ACTIVITYKIND).ToString());
        szType += GetInputItemHtml(CONSTHTML.option, "", "服务类型", ((uint)CODINGTABLE.DWCODETYPE.CODE_RESVSEIVICE).ToString());
        

        CODINGTABLEREQ vrParameter = new CODINGTABLEREQ();
        uint uType = Parse(Request["dwType"]);
        if (uType != 0)
        {
            vrParameter.dwCodeType = uType;
        }
        CODINGTABLE[] vrResult;
      
        GetPageCtrlValue(out vrParameter.szReqExtInfo);
        if (m_Request.System.GetCodingTable(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            UpdatePageCtrl(m_Request.System);
            for (int i = 0; i < vrResult.Length; i++)
            {
              
                m_szOut += "<tr>";
                m_szOut += "<td data-type=\"" + vrResult[i].dwCodeType.ToString() + "\" data-id=\"" + vrResult[i].szCodeSN.ToString() + "\">" + vrResult[i].szCodeSN + "</td>";
                m_szOut += "<td>" + vrResult[i].szCodeName + "</td>";
                string szTypeRes = "";
                uint uTypeRes = (uint)vrResult[i].dwCodeType;
                if(uTypeRes==(uint)CODINGTABLE.DWCODETYPE.CODE_RESVKIND)
                {
                    szTypeRes = "预约类型";
                }
                else if (uTypeRes == (uint)CODINGTABLE.DWCODETYPE.CODE_ACTIVITYKIND)
                {
                    szTypeRes = "活动类型";
                }
                else if (uTypeRes == (uint)CODINGTABLE.DWCODETYPE.CODE_RESVSEIVICE)
                {
                    szTypeRes = "服务类型";
                }
                m_szOut += "<td>" + szTypeRes + "</td>";
                m_szOut += "<td><div class='OPTD'></div></td>";
                m_szOut += "</tr>";
            }
          
        }
        PutBackValue();
    }
    private void DelLab(string szID)
    {
        CODINGTABLEREQ vrget = new CODINGTABLEREQ();
        vrget.szCodeSN = szID;
        CODINGTABLE[] vtRes;
        m_Request.System.GetCodingTable(vrget, out vtRes);
        if (vtRes != null && vtRes.Length > 0)
        {
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            CODINGTABLE value = new CODINGTABLE();
            value = vtRes[0];
            uResponse = m_Request.System.DelCodingTable(value);
            if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "提示", MSGBOX.ERROR);
            }
        }
    }
}
