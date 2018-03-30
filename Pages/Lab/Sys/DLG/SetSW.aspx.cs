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
    protected uint m_szSWID;
    protected string m_szSWName;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (IsPostBack)
        {
           // GetHTTPObj(out setUrl);

            uint dwClassSN = 0;
            uint.TryParse(Request["id"], out dwClassSN);
            uint dwID = 0;
            string szName;

            string[] arrayGroupName = Request["GroupListName"].Split(new char[] { ',' });
            string[] arrayGroup = Request["GroupList"].Split(new char[] { ',' });
            bool bRet = true;
            for (int i = 0; i < arrayGroup.Length; i++)
            { 
                uint.TryParse(arrayGroup[i], out dwID);
                
                szName = arrayGroupName[i];
                if (szName == "")
                {
                    continue;
                }
                if (!SetProg(dwID, dwClassSN, szName))
                {
                    bRet = false;
                }
            }

            if (!bRet)
            {
                MessageBox(m_Request.szErrMessage, "设置" + "名单" + "失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox("设置" + "名单" + "成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }

        }
    }

    protected bool SetProg(uint dwID, uint dwClassID,string szName)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;

        UNICTRLSW setvalue = new UNICTRLSW();
        setvalue.dwMemberID = dwID;
        setvalue.szName = szName;

        setvalue.dwKind = (uint)(UNICTRLSW.DWKIND.CSWKIND_PROGRAM);

        setvalue.dwID = null;
        setvalue.dwStatus = (uint)(UNICTRLSW.DWSTATUS.SWSTAT_CHECKED);
        //setvalue.dwFrom = 0;
        setvalue.dwClassSN = dwClassID;
        uResponse = m_Request.Control.SetCtrlSW(setvalue, out setvalue);
        if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
        {
            return false;
        }
        return true;
    }
}
