using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.IO;
using UniWebLib;

public partial class GetApvVserion : System.Web.UI.Page
{
    public class ctrlRoom
    {
        public string szDcsNO;
        public string szCtrlNo;
        public string szRoomNo;
    };
    public class consoleRoom
    {
        public string szRoomNo;
        public string szIP;
    };
    public class CtrlRoomInfo
    {
        public string szDcsNO;
        public string szCtrlNo;
        public string szRoomNo;
        public string szConsoleIP;
        public string szServerIP;
        public string szServerPort;
        public string szCustNo;
    };
    public class CtrlRoomInfoExt
    {
        public string szDcsNO;
        public string szCtrlNo;
        public string szRoomNo;
        public string szConsoleIP;
        public string szServerIP;
        public string szServerPort;
        public string szCustNo;
        public string szimgURL;
    };
    protected void Page_Load(object sender, EventArgs e)
    {
        string MyVPath = "";
        if (Request.ApplicationPath != "/")
        {
            MyVPath = Request.ApplicationPath + "/";
        }
        else
        {
            MyVPath = "/";
        }
        string szImg = Request["img"];
        string szRoomStruc = "";
        if (string.IsNullOrEmpty(szImg))
        {
            szRoomStruc = RoomReq();
        }
        else {
            szRoomStruc =RoomExtReq();
        }
        Response.Write(szRoomStruc);
        Response.End();
    }
    protected string RoomReq()
    {
        string szRoomNo = Request["roomno"];
        CtrlRoomInfo roomInfo = new CtrlRoomInfo();
        roomInfo.szCustNo = System.Web.Configuration.WebConfigurationManager.AppSettings["customNo"];
        ctrlRoom ctrlRoom = GetDoorCtrl(szRoomNo);
        if (ctrlRoom != null)
        {
            roomInfo.szCtrlNo = ctrlRoom.szCtrlNo;
            roomInfo.szDcsNO = ctrlRoom.szDcsNO;
        }
        consoleRoom cosole = GetConsole(szRoomNo);
        if (cosole != null)
        {
            roomInfo.szConsoleIP = cosole.szIP;
        }
        roomInfo.szServerIP = System.Web.Configuration.WebConfigurationManager.AppSettings["ServerIP"];
        roomInfo.szServerPort = System.Web.Configuration.WebConfigurationManager.AppSettings["ServerPort"];
        roomInfo.szRoomNo = szRoomNo;
        return JsonConvert.SerializeObject(roomInfo);
        
    }
    protected string RoomExtReq()
    {
        UniClientPage clinetPage = new UniClientPage();
        string szRoomNo = Request["roomno"];
        CtrlRoomInfoExt roomInfo = new CtrlRoomInfoExt();
        roomInfo.szCustNo = System.Web.Configuration.WebConfigurationManager.AppSettings["customNo"];
        ctrlRoom ctrlRoom = GetDoorCtrl(szRoomNo);
        if (ctrlRoom != null)
        {
            roomInfo.szCtrlNo = ctrlRoom.szCtrlNo;
            roomInfo.szDcsNO = ctrlRoom.szDcsNO;
        }
        consoleRoom cosole = GetConsole(szRoomNo);
        if (cosole != null)
        {
            roomInfo.szConsoleIP = cosole.szIP;
        }
   
        XmlCtrl.XmlInfo info = clinetPage.GetDftXmlInfo(szRoomNo, "RoomPadImg");
        List<string> imgList = clinetPage.GetSrcFromHtml(info.content);
        string szImgUrl = "";
        for (int i = 0; i < imgList.Count; i++)
        {
            string szTemp = imgList[i];
            szTemp = szTemp.Replace("~", "");
            szTemp = szTemp.Replace("\r\n", "");
            
            szImgUrl = szImgUrl+ szTemp+";";   
        }
        roomInfo.szServerIP = System.Web.Configuration.WebConfigurationManager.AppSettings["ServerIP"];
        roomInfo.szServerPort = System.Web.Configuration.WebConfigurationManager.AppSettings["ServerPort"];
        roomInfo.szRoomNo = szRoomNo;
        roomInfo.szimgURL = szImgUrl;
        return JsonConvert.SerializeObject(roomInfo);

    }
    protected ctrlRoom GetDoorCtrl(string szRoomNo)
    {
        ctrlRoom res = new ctrlRoom();
        string path = Server.MapPath("~/") + ("padtxt\\dcsRoom.txt");
        string str2 = File.ReadAllText(path, System.Text.Encoding.UTF8);

        List<ctrlRoom> list = JsonConvert.DeserializeObject<List<ctrlRoom>>(str2);
        if (list != null)
        {
            for (int i = 0; i < list.Count; i++)
            {
                ctrlRoom temp = new ctrlRoom();
                temp = list[i];
                if (temp.szRoomNo == szRoomNo)
                {
                    return temp;
                }
            }
        }
        return res;
    }
    protected consoleRoom GetConsole(string szRoomNo)
    {
        consoleRoom res = new consoleRoom();
        string path = Server.MapPath("~/") + ("padtxt\\ctrlRoom.txt");
        string str2 = File.ReadAllText(path, System.Text.Encoding.UTF8);

        List<consoleRoom> list = JsonConvert.DeserializeObject<List<consoleRoom>>(str2);
        if (list != null)
        {
            for (int i = 0; i < list.Count; i++)
            {
                consoleRoom temp = new consoleRoom();
                temp = list[i];
                if (temp.szRoomNo.IndexOf(","+szRoomNo+",")>-1)
                {
                    return temp;
                }
            }
        }
        return res;
    }
}