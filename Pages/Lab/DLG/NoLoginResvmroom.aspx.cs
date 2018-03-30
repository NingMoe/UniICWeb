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

        m_Title="免登陆预约";
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
            ArrayList list = new ArrayList();
            uint uBegInTime = Get1970Seconds(Request["dwBeginTime"]);
            uint uEndTime = Get1970Seconds(Request["dwEndTime"]);
            uint uLabID=0;
            string szLabName = "";
            for (int i = 0; i < szDevIDList.Length; i++)
            {
                if (szDevIDList[i] == "")
                {
                    continue;
                }
                UNIDEVICE devCtrl = new UNIDEVICE();
                 
                if (getDevByID(szDevIDList[i], out devCtrl))
                {
                    uLabID = (uint)devCtrl.dwLabID;
                    szLabName = devCtrl.szLabName;
                    uint uDevID = Parse(szDevIDList[i]);
                    RESVDEV temp = new RESVDEV();
                    temp.dwDevEnd = devCtrl.dwDevSN;
                    temp.dwDevKind = devCtrl.dwKindID;
                    temp.dwDevStart = devCtrl.dwDevSN;
                    temp.dwDevNum = 1;
                    temp.szRoomNo = devCtrl.szRoomNo;
                    list.Add(temp);    
                }
            }
            RESVDEV[] resvdev=new RESVDEV[list.Count];
            for (int m = 0; m < list.Count; m++)
            {
                resvdev[m] = new RESVDEV();
                resvdev[m] = (RESVDEV)list[m];
            }
            ANONYMOUSRESV setValue = new ANONYMOUSRESV();
            setValue.dwBeginTime = uBegInTime;
            setValue.dwEndTime = uEndTime;
            setValue.dwLabID = uLabID;
            setValue.szLabName = szLabName;
            setValue.szTestName = "免登陆教学预约";
            setValue.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_TEACHING;
            setValue.ResvDev = resvdev;
            if (m_Request.Reserve.AnonymousResvSet(setValue, out setValue) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                szError += m_Request.szErrMessage + ",";
            }

            if (szError != "")
            {
                MessageBox(szError, "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.CANCEL);
            }
            else { MessageBox("设置成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.CANCEL); }
        }
    }
}