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
    protected string m_Property = "";
    protected string m_szCamp = "";
	protected void Page_Load(object sender, EventArgs e)
    {
        UNIBUILDING newCourse;
        UNICAMPUS[] camp = GetAllCampus();
        if (camp != null && camp.Length > 0)
        {

           // m_szCamp += "<option value='0'>" + "全部" + "</option>";
            for (int i = 0; i < camp.Length; i++)
            {
                m_szCamp += "<option value='" + camp[i].dwCampusID + "'";
                m_szCamp += ">" + camp[i].szCampusName + "</option>";
            }
        }
        if (IsPostBack)
        {
            GetHTTPObj(out newCourse);
            if (m_Request.Device.BuildingSet(newCourse, out newCourse) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "修改失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox("修改成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
        }
        if (Request["op"] == "set")
        {
            bSet = true;

            BUILDINGREQ vrGet = new BUILDINGREQ();
            vrGet.dwBuildingID= Parse(Request["dwID"]);
            UNIBUILDING[] vtRes;
            if (m_Request.Device.BuildingGet(vrGet, out vtRes) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else
            {
                if (vtRes.Length == 0)
                {
                    MessageBox("获取失败", "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                }
                else
                {
                    PutJSObj(vtRes[0]);
                    m_Title = "修改【" + vtRes[0].szBuildingName + "】";
                }
            }
        }
        else
        {
            m_Title = "修改";

        }
    }
}
