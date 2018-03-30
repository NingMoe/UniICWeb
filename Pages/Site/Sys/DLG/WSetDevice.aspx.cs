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
            UNIROOM setRoom;
            setDevice.dwDevSN = Parse(setDevice.szAssertSN);
            GetHTTPObj(out setRoom);
            if (m_Request.Device.Set(setDevice, out setDevice) == REQUESTCODE.EXECUTE_SUCCESS)
            {
                if (GetRoomID(setDevice.dwRoomID.ToString(), out setRoom))
                {
                    setRoom.szRoomName = setDevice.szDevName;
                    setRoom.szRoomNo = setDevice.szAssertSN;
                    setRoom.dwLabID = setDevice.dwLabID;
                    setRoom.dwBuildingID = setDevice.dwBuildingID;
                    if (m_Request.Device.RoomSet(setRoom, out setRoom) == REQUESTCODE.EXECUTE_SUCCESS)
                    {
                        UNIDEVKIND setKind = new UNIDEVKIND();
                        if (GetDevKindByID(setDevice.dwKindID.ToString(), out setKind))
                        {
                            setKind.dwKindID = setDevice.dwKindID;
                            setKind.szKindName = setDevice.szDevName;
                            setKind.dwMinUsers = 1;
                            setKind.dwMaxUsers = setDevice.dwMaxUsers;
                            uint uNum = Parse(Request["num"]);
                            if (szIsOutDoor != null && uNum != 0)
                            {
                                setKind.dwProperty = (uint)setKind.dwProperty | (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_SHARE;
                                setKind.dwMaxUsers = (setKind.dwMaxUsers) * uNum;
                                setKind.dwNationCode = uNum;
                            }
                            else {
                                if (((uint)setKind.dwProperty & (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_SHARE) > 0)
                                {
                                    setKind.dwProperty = (uint)setKind.dwProperty - (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_SHARE;
                                }
                            }
                            setKind.dwClassID = setDevice.dwClassID;
                            if (m_Request.Device.DevKindSet(setKind, out setKind) == REQUESTCODE.EXECUTE_SUCCESS)
                            {
                                MessageBox("修改成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                                return;
                            }
                            else
                            {
                                MessageBox(m_Request.szErrMessage, "修改失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                                return;
                            }
                        }

                    }
                    else
                    {
                        MessageBox(m_Request.szErrMessage, "修改失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                        return;
                    }

                }
                else
                {
                    MessageBox(m_Request.szErrMessage, "修改失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                    return;
                }
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
}
