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
    protected string m_szRoom="";
    protected string m_szDev = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        m_Title="全体预约";
        if (!IsPostBack)
        {
            UNIROOM[] roomList = GetAllRoom();
            if (roomList != null && roomList.Length > 0)
            {
                for (int i = 0; i < roomList.Length; i++)
                {
                    string szCheck = "";
                    if (i == 0)
                    {
                        szCheck = " checked=\"true\"";
                    }
                    m_szRoom += "<input class=\"enum\"" + szCheck + " type=\"radio\" name=\"" + "roomID" + "\" id='" + roomList[i].dwRoomID.ToString() + "' /> <label for=\"" + roomList[i].dwRoomID.ToString() + "\">" + roomList[i].szRoomName + "</label>";
                }
                UNIDEVICE[] devList = GetDevByRoomId(roomList[0].dwRoomID);
                if (devList != null && devList.Length > 0)
                {
                    for (int i = 0; i < devList.Length; i++)
                    {
                        m_szDev += "<label><input class=\"enum\" type=\"checkbox\" name=\"" + "devID" + "\" value=\"" + devList[i].dwDevID.ToString() + "\" /> " + devList[i].szDevName + "</label>,";

                    }
                }
            }
        }
        else if (Request["op"] == "set")
        {
            string szError = "";
            string szDevID = Request["selectID"];
            string[] szDevIDList = szDevID.Split(',');
            if (szDevIDList.Length == 0)
            {
                return;
            }
            for (int i = 0; i < szDevIDList.Length; i++)
            {
                if (szDevIDList[i] == "")
                {
                    continue;
                }
                UNIDEVICE devCtrl = new UNIDEVICE();
                uint uBegInTime = Get1970Seconds(Request["dwBeginTime"]);
                uint uEndTime = Get1970Seconds(Request["dwEndTime"]);
                if (getDevByID(szDevIDList[i], out devCtrl))
                {
                    uint uDevID = Parse(szDevIDList[i]);

                    ALLUSERRESV setValue = new ALLUSERRESV();
                    setValue.dwBeginTime = uBegInTime;
                    setValue.dwEndTime =uEndTime;
                    setValue.dwLabID = devCtrl.dwLabID;
                    setValue.szLabName = devCtrl.szLabName;
                    setValue.szTestName = "全体人员预约";
                    setValue.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_TEACHING + (uint)UNIRESERVE.DWPURPOSE.USEFOR_ALLUSER+(uint)UNIRESERVE.DWPURPOSE.USEFOR_PC;
                    RESVDEV[] resvDev = new RESVDEV[1];
                    resvDev[0].dwDevEnd = devCtrl.dwDevSN;
                    resvDev[0].dwDevKind = devCtrl.dwKindID;
                    resvDev[0].dwDevStart = devCtrl.dwDevSN;
                    resvDev[0].dwDevNum = 1;
                    resvDev[0].szRoomNo = devCtrl.szRoomNo;
                    setValue.ResvDev = resvDev;
                    if (m_Request.Reserve.AllUserResvSet(setValue, out setValue) != REQUESTCODE.EXECUTE_SUCCESS)
                    {
                        szError += m_Request.szErrMessage + ",";
                    }
                }
                if (szError != "")
                {
                    MessageBox(szError, "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.CANCEL);
                }
                else { MessageBox("设置成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.CANCEL); }
              
            }

          
        }
    }
}