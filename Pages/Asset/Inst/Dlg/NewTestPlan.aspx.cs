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
    protected string m_szTerm = "";
    protected string m_szLabKind = "";
    protected uint dwKind = (uint)UNITESTPLAN.DWKIND.TESTPLANKIND_TEACHING;
    protected void Page_Load(object sender, EventArgs e)
    {
        UNITESTPLAN newTestPlan;

        TERMREQ termGet = new TERMREQ();

        UNITERM[] vtTerm;
        if (m_Request.Reserve.GetTerm(termGet, out vtTerm) == REQUESTCODE.EXECUTE_SUCCESS && vtTerm != null && vtTerm.Length > 0)
        {
            for (int i = 0; i < vtTerm.Length; i++)
            {
                if ((((uint)vtTerm[i].dwStatus & (uint)UNITERM.DWSTATUS.TERMSTAT_FORCE) > 0))
                {
                    m_szTerm += "<option selected=\"selected\" value=\"" + vtTerm[i].dwYearTerm + "\"> " + vtTerm[i].szMemo + "</option>";
                }
                else
                {
                    m_szTerm += GetInputItemHtml(CONSTHTML.option, "", vtTerm[i].szMemo, vtTerm[i].dwYearTerm.ToString());
                }
            }



            if (IsPostBack)
            {
                GetHTTPObj(out newTestPlan);
                if (m_Request.Reserve.SetTestPlan(newTestPlan, out newTestPlan) != REQUESTCODE.EXECUTE_SUCCESS)
                {
                    MessageBox(m_Request.szErrMessage, "新建实验计划失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                }
                else
                {
                    MessageBox("新建实验计划成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                    return;
                }
            }
           

            if (Request["op"] == "set")
            {
                bSet = true;

                LABREQ vrGetLab = new LABREQ();
                vrGetLab.dwLabID = Parse(Request["dwLabID"]);
                UNILAB[] vtLab;
                if (m_Request.Device.LabGet(vrGetLab, out vtLab) != REQUESTCODE.EXECUTE_SUCCESS)
                {
                    MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                }
                else
                {
                    if (vtLab.Length == 0)
                    {
                        MessageBox("获取失败", "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                    }
                    else
                    {
                        PutJSObj(vtLab[0]);
                        m_Title = "修改站点【" + vtLab[0].szLabName + "】";
                    }
                }
            }
            else
            {
                m_Title = "新建" + ConfigConst.GCLabName;

            }
        }
    }
}
