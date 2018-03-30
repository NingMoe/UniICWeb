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
    protected string m_szSta = "";
    protected string m_szRoom= "";
	protected void Page_Load(object sender, EventArgs e)
	{

        UNIROOM[] roomlist=GetAllRoom();
        for(int i=0;i<roomlist.Length;i++)
        {
            m_szRoom+=GetInputItemHtml(CONSTHTML.option,"",roomlist[i].szRoomName,roomlist[i].dwRoomID.ToString());
        }
        string szOpExt ="";
        if (Request["opext"] == "ff")
        {
            szOpExt = "变更";
        }
        if (Request["opext"] == "lc")
        {
            szOpExt = "变更";
        }

        if (IsPostBack)
        {
            REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
            if (Parse(Request["dwNewRoomID"]) != 0)
            {
                ROOMCHG roomChange = new ROOMCHG();
                roomChange.dwDevID = Parse(Request["id"]);
                roomChange.dwNewRoomID = Parse(Request["dwNewRoomID"]);
                roomChange.dwOldRoomID = Parse(Request["dwOldRoomID"]);
                UNIROOM setRoom;
                if(GetRoomID(roomChange.dwNewRoomID.ToString(),out setRoom))
                {
                    roomChange.szNewRoomName =setRoom.szRoomName.ToString();
                }

                roomChange.szOldRoomName = Request["szOlderRoomName"];
                uResponse = m_Request.Assert.AssertChgRoom(roomChange);

            }
            if (Parse(Request["dwNewKeeperID"]) != 0)
            {
                KEEPERCHG keepchange = new KEEPERCHG();
                keepchange.dwDevID = Parse(Request["id"]);
                keepchange.dwNewKeeperID = Parse(Request["dwNewKeeperID"]);
                keepchange.szNewKeeperName =Request["szLogonName"];
                keepchange.szOldKeeperName = Request["szOlderName"];
                keepchange.dwOldKeeperID = Parse(Request["dwOldKeeperID"]);
                keepchange.szMemo = "";
                uResponse = m_Request.Assert.AssertChgKeeper(keepchange); ;

            }
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox("变更成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                UNIDEVICE dev = new UNIDEVICE();

                if (Request.Files.Count > 0 && getDevByID(Request["id"], out dev))
                {
                    string fileName = Request.Files["fileurl"].FileName;
                    string szFileExtName = "";
                    szFileExtName = fileName.Substring(fileName.LastIndexOf('.'));
                    string szTempPath = MyVPath + "Upload/Assert/" + dev.szDevName.ToString() + dev.dwDevID.ToString() + szFileExtName;
                    dev.szDevURL = szTempPath;
                    m_Request.Device.Set(dev, out dev);
                    string szTempRawPath = Server.MapPath(szTempPath);
                    Request.Files[0].SaveAs(szTempRawPath);
                }
            }
            else
            {
                MessageBox(m_Request.szErrMessage, "变更失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                return;
            }
        }

     
        if (Request["op"] == "set")
        {
            bSet = true;
            ASSERTREQ vrDevReq = new ASSERTREQ();
            vrDevReq.dwDevID= Parse(Request["id"]);
            UNIASSERT[] vtDev;
            if (m_Request.Assert.AssertGet(vrDevReq, out vtDev) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else
            {
                if (vtDev.Length == 0)
                {
                    MessageBox("获取失败", "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
                }
                else
                {
                    PutJSObj(vtDev[0]); 
                    m_Title = szOpExt+"【" + vtDev[0].szDevName + "】";
                    PutMemberValue("dwOldRoomID",vtDev[0].dwRoomID.ToString());
                    PutMemberValue("dwNewRoomID", vtDev[0].dwRoomID.ToString());
                    PutMemberValue("dwOldKeeperID", vtDev[0].dwKeeperID.ToString());
                    PutMemberValue("szOlderName", vtDev[0].szKeeperName.ToString());
                    PutMemberValue("szOlderRoomName", vtDev[0].szRoomName.ToString());
                }
            }
        }
        else
        {
            m_Title = "变更";
        }
	}
}
