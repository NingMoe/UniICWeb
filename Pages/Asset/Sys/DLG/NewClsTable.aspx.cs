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
    protected string m_szIdent = "";
    protected string m_szDevKind = "";
    protected string m_szResvPurpose= "";
    uint uSnN = 1;
    protected string m_dwPriority = "";
	protected void Page_Load(object sender, EventArgs e)
    {
        CLASSTIMETABLE newTimeTable;      
        if (IsPostBack)
        {
            GetHTTPObj(out newTimeTable);
            newTimeTable.dwSN = uSnN;
            newTimeTable.dwBeginTime = GetTime(Request["dwBeginTime"]);
            newTimeTable.dwEndTime = GetTime(Request["dwEndTime"]);
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            CTSREQ vrParameter = new CTSREQ();
            CLASSTIMETABLE[] vrResult;
            if (m_Request.Reserve.GetClassTimeTable(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                string szID = Request["dwID"];
                bool bNew = true;
                if (szID != null && szID != "")
                {
                    bNew = false;
                }
                ArrayList list = new ArrayList();
                if (bNew)
                {
                    for (int i = 0; i < vrResult.Length; i++)
                    {
                        list.Add(vrResult[i]);                      
                    }
                    list.Add(newTimeTable);
                    CLASSTIMETABLE[] res2 = new CLASSTIMETABLE[(list.Count)];
                    for (int i = 0; i < list.Count; i++)
                    {
                        res2[i] = new CLASSTIMETABLE();
                        res2[i] = (CLASSTIMETABLE)list[i];
                    }
                    uResponse = m_Request.Reserve.SetClassTimeTable(res2);
                    if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
                    {
                        MessageBox(m_Request.szErrMessage, "新建作息时间失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                    }
                    else
                    {
                        MessageBox("新建作息时间成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                        return;
                    }
                }
                else {
                    for (int i = 0; i < vrResult.Length; i++)
                    {
                        if (vrResult[i].dwSecIndex.ToString() != newTimeTable.dwSecIndex.ToString())
                        {
                            list.Add(vrResult[i]);
                        }
                    }
                    list.Add(newTimeTable);
                    CLASSTIMETABLE[] res2 = new CLASSTIMETABLE[(list.Count)];
                    for (int i = 0; i < list.Count; i++)
                    {
                        res2[i] = new CLASSTIMETABLE();
                        res2[i] = (CLASSTIMETABLE)list[i];
                    }
                    uResponse = m_Request.Reserve.SetClassTimeTable(res2);
                    if (uResponse != REQUESTCODE.EXECUTE_SUCCESS)
                    {
                        MessageBox(m_Request.szErrMessage, "修改作息时间失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                    }
                    else
                    {
                        MessageBox("修改作息时间成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                        return;
                    }
                }                
            }           
        }       
        if (Request["op"] == "set")
        {
            string szID = Request["dwID"];
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            CTSREQ vrParameter = new CTSREQ();
            CLASSTIMETABLE[] vrResult;
            if (m_Request.Reserve.GetClassTimeTable(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                ArrayList list = new ArrayList();
                for (int i = 0; i < vrResult.Length; i++)
                {
                    if (vrResult[i].dwSecIndex.ToString() == szID)
                    {
                        PutJSObj(vrResult[i]);
                        break;
                    }
                }               
            }
        }
        else
        {
           
        }
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
    }

}
