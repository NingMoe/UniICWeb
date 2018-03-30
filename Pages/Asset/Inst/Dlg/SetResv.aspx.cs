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
using System.Collections.Specialized;

public partial class _Default : UniPage
{
    protected string m_TermText = "";
    protected string m_szRoomName = "";

	protected void Page_Load(object sender, EventArgs e)
	{
        m_szRoomName = Request["szRoomName"];

        TESTITEMREQ vrTestItemReq = new TESTITEMREQ();
        UNITESTITEM[] vrTestItemResult;
        vrTestItemReq.dwGetType = (uint)TESTITEMREQ.DWGETTYPE.TESTITEMGET_BYID;
        vrTestItemReq.szGetKey = Request["dwTestItemID"];
        if (m_Request.Reserve.GetTestItem(vrTestItemReq, out vrTestItemResult) == REQUESTCODE.EXECUTE_SUCCESS && vrTestItemResult.Length > 0)
        {
        }
        else
        {
           // MessageBox("实验安排失败,无效的实验项目," + m_Request.szErrMsg, "实验安排失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
           // return;
        }
        ROOMREQ vrGetRoom = new ROOMREQ();
        UNIROOM[] vrRoomResult;
        vrGetRoom.dwRoomID = ToUint(Request["RoomID"]);
        if (m_Request.Device.RoomGet(vrGetRoom, out vrRoomResult) == REQUESTCODE.EXECUTE_SUCCESS && vrRoomResult.Length > 0)
        {
            m_szRoomName = vrRoomResult[0].szRoomName;
        }
        else
        {
            //MessageBox("实验安排失败,无效的房间号," + m_Request.szErrMsg, "实验安排失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            return;
        }
        UNIRESERVE newresv = new UNIRESERVE();
        newresv.dwMemberKind = (uint)UNIRESERVE.DWMEMBERKIND.MEMBERKIND_GROUP;
        newresv.dwUseMode = (uint)UNIRESERVE.DWUSEMODE.RESVUSE_USEDEV;
        newresv.dwPurpose = (uint)UNIRESERVE.DWPURPOSE.USEFOR_TEACHING;
        newresv.dwPreDate = ToUint(Request["dwDate"]);
        newresv.dwOwner =ToUint(Request["dwOwner"]);
        newresv.dwLabID = vrRoomResult[0].dwLabID;
        newresv.ResvDev = new RESVDEV[1];
        newresv.ResvDev[0].szRoomNo = vrRoomResult[0].szRoomNo;
       // newresv.szResvDevs[0].dwDevKind = 1403;
        newresv.ResvDev[0].dwDevStart = 0;
        newresv.ResvDev[0].dwDevEnd = 99999;
        newresv.dwProperty = (uint)UNIRESERVE.DWPROPERTY.RESVPROP_LOCKROOM;
        newresv.dwTestItemID =  ToUint(Request["dwTestItemID"]);
        TESTITEMREQ testItemGet = new TESTITEMREQ();
        testItemGet.dwGetType=((uint)TESTITEMREQ.DWGETTYPE.TESTITEMGET_BYID);
        testItemGet.szGetKey = Request["dwTestItemID"];
        UNITESTITEM[] vtTestItem;
        m_Request.Reserve.GetTestItem(testItemGet, out vtTestItem);
        if (vtTestItem != null && vtTestItem.Length > 0)
        {
            newresv.dwMemberID = vtTestItem[0].dwGroupID;
            newresv.szMemberName = vtTestItem[0].szGroupName;
           
        }
        newresv.dwYearTerm = GetTerm(null);

        uint? dwBeginSec = ToUint(Request["dwBeginSec"]);
        uint? dwEndSec = ToUint(Request["dwEndSec"]);
        CLASSTIMETABLE[] vtSec;
        CTSREQ vrCtsReq = new CTSREQ();
        
        m_Request.Reserve.GetClassTimeTable(vrCtsReq, out vtSec);
         GetSecTime(vtSec, ref dwBeginSec, ref dwEndSec, out newresv.dwBeginTime, out newresv.dwEndTime);

        newresv.dwOccurTime = Get1970Seconds(DateTime.Now.ToString());
        newresv.dwBeginTime = Get1970Seconds(Request["dwDate"]  + " " + newresv.dwBeginTime / 100 + ":" + newresv.dwBeginTime % 100);
        newresv.dwEndTime = Get1970Seconds(Request["dwDate"] + " " + newresv.dwEndTime / 100 + ":" + newresv.dwEndTime % 100);

        //newresv.dwMemberID = 1071;// vrTestItemResult[0].dwGroupID;
        //newresv.szMemberName = "12日语本衔接班";// vrTestItemResult[0].szGroupName;
       // newresv.dwYearTerm = 20131401;
       // newresv.dwTeachingTime = 261212;

        if (Request["IsSubmit"] == "true")
        {
            if (m_Request.Reserve.Set(newresv, out newresv) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox("实验安排成功", "实验安排成功", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
            }
            else
            {
               // MessageBox("实验安排失败," + m_Request.szErrMsg, "实验安排失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
        }

        m_TermText = GetTermText(0);

        PutBackValue();
	}

    bool GetSecTime(CLASSTIMETABLE[] vtSec, ref uint? dwBeginSec, ref uint? dwEndSec, out uint? dwBeginTime, out uint? dwEndTime)
    {
        dwBeginTime = 0;
        dwEndTime = 0;
        int dwBegin_I = -1;
        int dwEnd_I = -1;
        for (int i = 0; i < vtSec.Length; i++)
        {
            if (vtSec[i].dwSecIndex == dwBeginSec)
            {
                dwBegin_I = i;
            }
            if (vtSec[i].dwSecIndex == dwEndSec)
            {
                dwEnd_I = i;
            }
        }
        if (dwBegin_I == -1 || dwEnd_I == -1)
        {
            return false;
        }
        dwBeginTime = vtSec[dwBegin_I].dwBeginTime;
        dwEndTime = vtSec[dwEnd_I].dwEndTime;
        dwBeginSec = (uint)dwBegin_I; //TODO:
        dwEndSec = (uint)dwEnd_I;//TODO:
        return true;
    }
}
