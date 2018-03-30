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

public partial class _Default :UniPage
{
    protected bool bSet = false;
    protected string m_Title = "";
    protected string m_szDept = "";
    protected string m_szLabKind = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        //DEPTREQ vrParameter = new DEPTREQ();
        UNIDEPT[] vrResult=GetAllDept();
        //vrParameter.dwGetType = (uint)DEPTREQ.DWGETTYPE.DEPTGET_BYALL;
       // vrParameter.dwKind = (uint)UNIDEPT.DWKIND.DEPTKIND_SERVICE;
        //if (m_Request.Account.DeptGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; vrResult != null&&i < vrResult.Length; i++)
            {
                m_szDept += "<option value='" + vrResult[i].dwID + "'>" + vrResult[i].szName + "</option>";
            }
        }
        {
          
        }

        LABREQ vrGetLab = new LABREQ();
        vrGetLab.dwLabID =Parse(Request["dwLabID"]);
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
            }
            m_Title = "查看" + ConfigConst.GCLabName;


        }
    }
}
