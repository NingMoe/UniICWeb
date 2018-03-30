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

public partial class _Default : UniWebLib.UniPage
{
    protected bool bSet = false;
    protected string m_Title = "";
    protected string m_szDept = "";
	protected void Page_Load(object sender, EventArgs e)
	{
        if (IsPostBack)
        {
            UNISTATION newStation;
            GetHTTPObj(out newStation);
            if (m_Request.Station.SetStation(newStation, out newStation) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "修改失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox("修改成功", "修改成功", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
        }

        DEPTREQ vrParameter = new DEPTREQ();
        UNIDEPT[] vrResult;
        //vrParameter.dwGetType = (uint)DEPTREQ.DWGETTYPE.DEPTGET_BYALL;
        vrParameter.dwKind = (uint)ConfigConst.GCDeptKind;
        if (m_Request.Account.DeptGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                m_szDept += "<option value='" + vrResult[i].dwID + "'>" + vrResult[i].szName + "</option>";
            }
        }

        if (Request["op"] == "set")
        {
            bSet = true;

            STATIONREQ vrGetStation = new STATIONREQ();
            vrGetStation.dwGetType = (uint)STATIONREQ.DWGETTYPE.STATIONGET_BYSN;
            vrGetStation.szGetKey = Request["dwStaSN"];
            UNISTATION[] vrResultStation;
            if (m_Request.Station.GetStation(vrGetStation, out vrResultStation) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else
            {
                if (vrResultStation.Length == 0)
                {
                    MessageBox("获取失败", "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                }
                else
                {
                    PutJSObj(vrResultStation[0]);
                    m_Title = "修改站点【" + vrResultStation[0].szStaName + "】";
                }
            }
        }
        else
        {
            m_Title = "新建站点";
        }
	}
}
