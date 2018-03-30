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
using UniLibrary;
public partial class _Default : UniPage
{
    protected bool bSet = false;
    protected string m_Title = "";
    protected string szMondev = "";
    protected string szDevID = "";
    protected string m_szRoom = "";
    protected string m_szDev = "";
	protected void Page_Load(object sender, EventArgs e)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        
        string szOp = Request["op"];
        UNIROOM[] roomList = GetRoomByClassKind((uint)UNIDEVCLS.DWKIND.CLSKIND_SEAT);
        if (roomList != null && roomList.Length > 0)
        {
            for (int i = 0; i < roomList.Length; i++)
            {
                string szCheck = "";
                if (i == 0)
                {
                    szCheck = " checked=\"true\"";
                }
                m_szRoom += "<label><input class=\"enum\"" + szCheck + "type=\"radio\" name=\"" + "roomID" + "\" value=\"" + roomList[i].dwRoomID.ToString() + "\" /> " + roomList[i].szRoomName + "</label>";
            }
            UNIDEVICE[] devList = GetDevByRoomId(roomList[0].dwRoomID);
            if (devList != null && devList.Length > 0)
            {
                for (int i = 0; i < devList.Length; i++)
                {
                    m_szDev += "<label><input class=\"enum\" type=\"checkbox\" name=\"" + "devID" + "\" value=\"" + devList[i].dwDevID.ToString() + "\" /> " + devList[i].szDevName + "</label>";

                }
            }
        }
        if (IsPostBack)
        {
            MONDEV temp;
            GetHTTPObj(out temp);
            string szDevIDHtml = Request["selectID"];
            string szselectIDOld = Request["selectIDOld"];
            if (szDevIDHtml != "")
            {
                string[] szDevList = szDevIDHtml.Split(',');
                for (int i = 0; i < szDevList.Length; i++)
                {
                    if (szDevList[i] != "")
                    {
                        MONDEV setValue = new MONDEV();
                        setValue = temp;
                        setValue.dwDevID = Parse(szDevList[i]);
                        if (szselectIDOld.IndexOf(szDevList[i]+",")==-1)
                        {
                            m_Request.Device.MonDevSet(setValue, out setValue);
                        }
                    }
                }
            }
            if (szselectIDOld != "")
            {
                string[] szDevOldList = szselectIDOld.Split(',');
                for (int i = 0; i < szDevOldList.Length; i++)
                {
                    if (szDevOldList[i] != "")
                    {
                        MONDEV setValue = new MONDEV();
                        setValue = temp;
                        setValue.dwDevID = Parse(szDevOldList[i]);
                        if (szDevIDHtml.IndexOf(szDevOldList[i] + ",") == -1)
                        {
                            m_Request.Device.MonDevDel(setValue);
                        }
                    }
                }
            }
            if (szOp == "set")
            {
                MessageBox("修改成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
            else {
                MessageBox("新建成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
        }


        if (Request["op"] == "set")
        {
            m_Title = "修改控制对象";
            MONDEVREQ vrParameter = new MONDEVREQ();
            vrParameter.dwMonitorID = Parse(Request["dwMonitorID"]);
            MONDEV[] vrResult;

            if (m_Request.Device.MonDevGet(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS&&vrResult!=null)
            {
                string szMondev = "";
                for (int i = 0; i < vrResult.Length; i++)
                {
                    szMondev += vrResult[i].dwDevID.ToString() + ",";
                }
                PutMemberValue2("selectID", szMondev);
                PutMemberValue2("devID", szMondev);
                PutMemberValue2("selectIDOld", szMondev);
                
            }
        }
        else
        {
            m_Title = "新建控制对象";
        }
    }
   
}
