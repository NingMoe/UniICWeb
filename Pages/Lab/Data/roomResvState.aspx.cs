using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class Resv_searchCls :UniPage
{
    protected void Page_Load(object sender, EventArgs e)
    {


        string date = Request["date"];
        uint dwType =Parse(Request["dwType"]);
        uint uRoomID = Parse(Request["dwRoomID"]);
        Response.CacheControl = "no-cache";

        /*
        RGRESVSTATREQ vrTest = new RGRESVSTATREQ();
        vrTest.dwDate = Parse(date) + 100;
        RGRESVSTAT[] vtTest;
        m_Request.Device.GetRGResvStat(vrTest, out vtTest);
        */

        UNIROOM[] vtRoom;
        if (Application["roomInfo"] != null)
        {

            vtRoom = (UNIROOM[])Application["roomInfo"];
        }
        else
        {
             ROOMREQ roomReq = new ROOMREQ();
          
            if (m_Request.Device.RoomGet(roomReq, out vtRoom) == REQUESTCODE.EXECUTE_SUCCESS && vtRoom != null && vtRoom.Length > 0)
            {
                Application["roomInfo"] = vtRoom;
                //没什么好写的 如果获取不到 下面的状态更加获取不到
            }
        }
        ROOMRESVSTATREQ vrGet = new ROOMRESVSTATREQ();
        vrGet.dwDate = Parse(date)+100;
        if (uRoomID != 0)
        {
            //vrGet.dwGetType = (uint)ROOMRESVSTATREQ.DWGETTYPE.ROOMRESVSTAT_ROOMID;
            vrGet.szRoomIDs = uRoomID.ToString();
        }
        else {
          //  vrGet.dwGetType = (uint)DEVRESVSTATREQ.DWGETTYPE.DEVRESVSTAT_ALL;
        }
       
        ROOMRESVSTAT[] vtRes;
        CLASSTIMETABLE[] classTimeTable = GetTermClasTimeTable();
        bool bIsExt = true;//默认纯在缩写
        if (m_Request.Device.GetRoomResvStat(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS && vtRes != null && vtRes.Length > 0)
        {
            
            string szDevInfo = "[";
            string szOpenTime = "";
            string szOut="";
            szOut += "[";
            for (int i = 0; i < vtRes.Length; i++)
            {
                string szDevNameExt = "";
                if (bIsExt)
                {
                    for (int k = 0; k < vtRoom.Length; k++)
                    {
                        if ((!string.IsNullOrEmpty(vtRoom[k].szRoomURL))&&vtRes[i].dwRoomID == vtRoom[k].dwRoomID)
                        {
                            szDevNameExt = vtRoom[k].szRoomURL;
                        }
                        if (k == (vtRoom.Length - 1))
                        {
                          //  bIsExt = false;
                        }
                    }
                }
                string szResvInfo = "[";
                szOpenTime ="";
                szOpenTime = classTimeTable.Length.ToString();
                
                string devNameTemp = vtRes[i].szRoomName+"("+vtRes[i].dwDevNum.ToString()+")";

                string devIDTemp = vtRes[i].dwRoomID.ToString();
                TEACHINGRESVINFO[] resvInfo=vtRes[i].szResvInfo;
                /*
                int uStatue = i % 5;

                for (int j = 0; j < 1; j++)
                {
                    int m = i;
                    if (m > 9)
                    {
                        m = m - 8;
                    }
                    int uResvTime = 123 * 10000 + (m + 1) * 100 + (m + 3);
                    string szResvName = "这就是我的课不许碰";// resvInfo[j].szCourseName + "_" + resvInfo[j].szGroupName + "_" + resvInfo[j].szTeacherName;
                    
                    szResvInfo = szResvInfo + "{" + "\"szResvName\":\"" + szResvName + "\","+ "\"devID\":\"" + devIDTemp + "\"," + "\"value\":" + uResvTime.ToString() + "}";
                    if (j < 1 - 1)
                    {
                        szResvInfo += ",";
                    }
                }
                */
                if (resvInfo != null && resvInfo.Length > 0)
                {  
                   
                    for (int j = 0; j < resvInfo.Length; j++)
                    {
                        uint uStatue = (uint)resvInfo[j].dwResvStat;
                        if ((uStatue & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_UNDO) > 0)
                        {
                            uStatue = 1;
                        }
                        else if ((uStatue & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DOING) > 0)
                        {
                            uStatue = 2;
                        }
                        else if ((uStatue & (uint)UNIRESERVE.DWSTATUS.RESVSTAT_DONE) > 0)
                        {
                            uStatue = 4;
                        }
                  
                        uint uResvTime = (uint)resvInfo[j].dwTeachingTime;
                        
                        string szResvName = resvInfo[j].szGroupName;// +"_" + resvInfo[j].szGroupName + "_" + resvInfo[j].szTeacherName;； resvInfo[j].szTestName;// +"_" + resvInfo[j].szGroupName + "_" + resvInfo[j].szTeacherName;
                        szResvInfo = szResvInfo + "{" + "\"szResvName\":\"" + szResvName + "\"," + "\"devID\":\"" + devIDTemp + "\"," + "\"status\":\"" + uStatue + "\"," + "\"value\":" + uResvTime.ToString() + "}";
                        if (j < resvInfo.Length - 1)
                        {
                            szResvInfo += ",";
                        }
                    }
                     
                }
                szResvInfo += "]";
                szDevInfo = szDevInfo + "{" + "\"devNameExt\":\"" + szDevNameExt + "\"," + "\"devName\":\"" + devNameTemp + "\","+ "\"devID\":\"" + devIDTemp + "\"," + "\"resvInfo\":" + szResvInfo + "}";
                if (i < vtRes.Length - 1)
                {
                    szDevInfo += ",";
                }
            }
            szDevInfo += "]";
         
            szOut = "{\"DevList\":" + szDevInfo + ",\"OpenTime\":" + szOpenTime +"}";
            Response.Write(szOut);
        
        }
        else
        {
            Response.Write("[ ]");
        }
    }
}