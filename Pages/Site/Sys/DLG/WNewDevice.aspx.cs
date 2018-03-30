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
    protected string sz_building = "";
    protected string sz_DevCls = "";
    protected string sz_Lab = "";
    protected string sz_Share = "0";
    protected void Page_Load(object sender, EventArgs e)
    {
        UNIBUILDING[] vtBuliding = getAllBuilding();
        string szIsOutDoor = Request["isOutDoor"];
        for (int i = 0; i < vtBuliding.Length; i++)
        {
            sz_building += GetInputItemHtml(CONSTHTML.option, "", vtBuliding[i].szBuildingName, vtBuliding[i].dwBuildingID.ToString());
        }
        UNIDEVCLS[] vtClass = GetAllDevCls();
        for (int i = 0; i < vtClass.Length; i++)
        {
            sz_DevCls += GetInputItemHtml(CONSTHTML.option, "", vtClass[i].szClassName, vtClass[i].dwClassID.ToString());
        }
        UNILAB[] vtLab = GetAllLab();
        for (int i = 0; i < vtLab.Length; i++)
        {
            sz_Lab += GetInputItemHtml(CONSTHTML.option, "", vtLab[i].szLabName, vtLab[i].dwLabID.ToString());
        }
        if (IsPostBack)
        {
            UNIDEVICE setDevice;
            GetHTTPObj(out setDevice);
            UNIROOM setRoom = new UNIROOM();
            setRoom.dwLabID = setDevice.dwLabID;
            setRoom.szRoomName = setDevice.szDevName;
            setRoom.szRoomNo = setDevice.szAssertSN;
            setDevice.dwDevSN =Parse(setDevice.szAssertSN);
            setRoom.dwCampusID = GetBuilding((uint)setDevice.dwBuildingID);
            setRoom.dwBuildingID = setDevice.dwBuildingID;
            UNIGROUP mangroup = new UNIGROUP();
            if (NewGroup(setDevice.szDevName.ToString(), (uint)UNIGROUP.DWKIND.GROUPKIND_MAN, out mangroup))
            {
                setRoom.dwManGroupID = mangroup.dwGroupID;
            }
            setRoom.dwOpenRuleSN = 1404;
            if (m_Request.Device.RoomSet(setRoom, out setRoom) == REQUESTCODE.EXECUTE_SUCCESS)
            {

            }
            setDevice.dwRoomID = setRoom.dwRoomID;
            UNIDEVKIND newKind = new UNIDEVKIND();
            newKind.szKindName = setDevice.szDevName;
            newKind.dwProperty = 1;
            newKind.dwMinUsers = 1;
            newKind.dwMaxUsers = setDevice.dwMaxUsers;
            if (m_Request.Device.DevKindSet(newKind, out newKind) == REQUESTCODE.EXECUTE_SUCCESS)
            {

            }
            setDevice.dwKindID = newKind.dwKindID;
            if (m_Request.Device.Set(setDevice, out setDevice) == REQUESTCODE.EXECUTE_SUCCESS)
            {

                MessageBox("设置成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;

            }
            else
            {
                MessageBox(m_Request.szErrMessage, "设置失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                return;
            }


        }

        if (Request["op"] == "set")
        {
            bSet = true;

            DEVREQ vrGet = new DEVREQ();
            string devID = Request["id"];
            vrGet.dwDevID = Parse(Request["id"]);
            UNIDEVICE[] vtDev;
            if (m_Request.Device.Get(vrGet, out vtDev) != REQUESTCODE.EXECUTE_SUCCESS)
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
                  
                    UNIDEVKIND devKind;
                    if (GetDevKindByID(vtDev[0].dwKindID.ToString(), out devKind))
                    {
                        if (((uint)devKind.dwProperty & (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_SHARE) > 0)
                        {
                            PutMemberValue("isOutDoor", "1");
                            sz_Share = "1";
                            if (devKind.dwNationCode != null)
                            {
                                PutMemberValue("num", devKind.dwNationCode.ToString());
                            }
                            else
                            {
                                PutMemberValue("num", "0");
                            }
                            vtDev[0].dwMaxUsers = ((uint)vtDev[0].dwMaxUsers) / (uint)devKind.dwNationCode;
                        }
                        
                        PutMemberValue("isOutDoor", "0");
                    }
                  
                    PutJSObj(vtDev[0]);
                    m_Title = "修改【" + vtDev[0].szDevName + "】";
                }
            }
        }
        else
        {
            m_Title = "新建";

        }
    }
    public uint GetBuilding(uint buildingID)
    {
        BUILDINGREQ req = new BUILDINGREQ();
        req.dwBuildingID = buildingID;
        UNIBUILDING[] vtBuilding;
        if (m_Request.Device.BuildingGet(req, out vtBuilding) == REQUESTCODE.EXECUTE_SUCCESS && vtBuilding != null && vtBuilding.Length > 0)
        {
            return (uint)vtBuilding[0].dwCampusID;
        }
        return 0;
    }
}
