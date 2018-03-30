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
    protected string m_szDoorCtrl = "";
    protected string m_szLab = "";
    protected string m_szRoom = "";
    protected string m_szDevKind = "";
    protected string m_szPorperty = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        if (IsPostBack)
        {
            uint uNum = Parse(Request["dwNum"]);
            int NextLen = IntParse(Request["dwNextLen"]);
            string LenFix = Request["LenFix"];
            string szPreName = Request["szPreName"];
            string szPreNameTemp = szPreName;
            uint uNextName = Parse(Request["szNextName"]);
            uint uRoomID = Parse(Request["dwRoomID"]);
            uint uLabID = Parse(Request["dwLabID"]);
            uint uKindID = Parse(Request["dwKindID"]);
            uint uStartNum = Parse(Request["dwStartNum"]);
            uint uCtrlMode = Parse(Request["dwCtrlMode"]);
          
            for (uint i = uNextName; i <= (uNum + uNextName-1); i++)
            {
                UNIDEVICE newDev = new UNIDEVICE();
                if (LenFix == "true")
                {
                    szPreName = szPreNameTemp + i.ToString().PadLeft(NextLen, '0');
                }
                else
                {
                    szPreName = szPreNameTemp + i.ToString();
                }
            
                newDev.szDevName = szPreName;
                newDev.dwLabID = uLabID;
                newDev.dwKindID = uKindID;
                newDev.dwRoomID = uRoomID;
                newDev.dwDevSN = uStartNum;
                newDev.dwCtrlMode = uCtrlMode;
                if(m_Request.Device.Set(newDev, out newDev)==REQUESTCODE.EXECUTE_SUCCESS)
                {
                    uStartNum = uStartNum + 1;
                }
            }
            MessageBox("批量新建设备成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
            return;
        }
        m_szPorperty = GetInputHtmlFromXml(0, CONSTHTML.checkBox, "dwProperty", "UNIDEVICE_Property", true);
        m_Title = "批量新建设备";
    }
    protected override void OnPreRender(EventArgs e)
    {       
    }
}
