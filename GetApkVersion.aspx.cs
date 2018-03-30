using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
public partial class GetApvVserion : System.Web.UI.Page
{
    public class apkversion
    {
        public string szVersion;
        public string szApkpath;
    };
    public class DoorctrlInfi
    {
        public string szDCSNO;
        public string szDoorCtrlNo;
        public string szCusNo;
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
        apkversion version = new apkversion();
        version.szVersion = System.Web.Configuration.WebConfigurationManager.AppSettings["apkversion"];
        version.szApkpath = MyVPath + "downfile/" + System.Web.Configuration.WebConfigurationManager.AppSettings["apkversionName"];
        Response.Write(JsonConvert.SerializeObject(version));
        Response.End();
    }
    protected string GetDoorCtrl(string szRoomNo)
    {
        DoorctrlInfi doorCtrl = new DoorctrlInfi();
        doorCtrl.szCusNo = System.Web.Configuration.WebConfigurationManager.AppSettings["apkversion"];
        return (JsonConvert.SerializeObject(doorCtrl));
        
    }
}