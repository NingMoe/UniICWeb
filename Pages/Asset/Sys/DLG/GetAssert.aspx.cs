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
    protected string m_szPorperty = "";   
    protected string szOpName = "查看";
    protected string szFunction = "";
    protected string szDevCLS = "";
    protected string szRoom = "";
    protected string szDEVCLSHtml = "";
    protected string szDevSN = "";
    protected string szOp2 = "";
    protected string szUrl = "";
	protected void Page_Load(object sender, EventArgs e)
	{
        szDevSN = GetDevSN().ToString();
        string szOp=Request["op"];
        CODINGTABLE[] vtCodeTable = getCodeTableByType((uint)CODINGTABLE.DWCODETYPE.CODE_DEVFUNC);
        if (vtCodeTable != null && vtCodeTable.Length > 0)
        {
            for (int i = 0; i < vtCodeTable.Length; i++)
            {
                szFunction += GetInputItemHtml(CONSTHTML.option, "", vtCodeTable[i].szCodeName.ToString(), vtCodeTable[i].szCodeSN.ToString());
            }
        }
        m_szPorperty = GetInputHtmlFromXml(0, CONSTHTML.checkBox, "dwProperty", "UNIDEVICE_Property", true);
        UNIDEVCLS[] vtDevCls = GetAllDevCls();
        if (vtDevCls != null && vtDevCls.Length > 0)
        {
            for (int i = 0; i < vtDevCls.Length; i++)
            {
                szDevCLS += GetInputItemHtml(CONSTHTML.option, "", vtDevCls[i].szClassName, vtDevCls[i].dwClassID.ToString());
                szDEVCLSHtml += vtDevCls[i].dwClassID.ToString() + ":" + vtDevCls[i].dwKind.ToString() + ";";
            }
        }
        UNIROOM[] vtRoom = GetAllRoom();
        if (vtRoom != null && vtRoom.Length > 0)
        {
            for (int i = 0; i < vtRoom.Length; i++)
            {
                szRoom += GetInputItemHtml(CONSTHTML.option, "", vtRoom[i].szRoomName, vtRoom[i].dwRoomID.ToString());
            }
        }
        if (IsPostBack)
        {
            if (szOp == "set")
            {
                szOpName = "查看";
            }
            UNIASSERT newAssert;
            GetHTTPObj(out newAssert);
            newAssert.dwPurchaseDate = GetDate(Request["dwPurchaseDate"]);
            if (szOp != "set")
            {
                string szKindName = newAssert.szDevName + "-" + newAssert.szModel + "-" + newAssert.szFuncCode;
                uint uKindID = GetDevKindByName(szKindName);
                if (uKindID > 0)
                {
                    newAssert.dwKindID = uKindID;
                }
                else
                {
                    UNIDEVKIND kindValue;
                    GetHTTPObj(out kindValue);
                    kindValue.dwClassID = newAssert.dwClassID;
                    uint uClassKind = Parse(Request["dwClassKind"]);
                    if ((uClassKind & (uint)UNIDEVCLS.DWKIND.CLSKIND_LOAN) > 0)
                    {
                        kindValue.dwProperty = (uint)kindValue.dwProperty | (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_LEASE;
                    }
                    else
                    {
                        kindValue.dwProperty = (uint)kindValue.dwProperty & (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_LEASE;
                    }
                    kindValue.dwProperty = (uint)kindValue.dwProperty | (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_EXCLUSIVE;
                    kindValue.szKindName = newAssert.szDevName + "-" + newAssert.szModel + "-" + newAssert.szFuncCode;
                    if (m_Request.Device.DevKindSet(kindValue, out kindValue) == REQUESTCODE.EXECUTE_SUCCESS)
                    {
                        newAssert.dwKindID = kindValue.dwKindID;
                        if (m_Request.Assert.AssertWarehousing(newAssert, out newAssert) != REQUESTCODE.EXECUTE_SUCCESS)
                        {
                            MessageBox(m_Request.szErrMessage, "提示", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                            return;
                        }
                        else
                        {
                            MessageBox(szOpName + "成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox(szOpName + "成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                        return;
                    }

                }
            }
            else
            {
                UNIDEVKIND kindValue;
                GetHTTPObj(out kindValue);
                kindValue.dwClassID = newAssert.dwClassID;
                kindValue.dwMinUsers = 1;
                kindValue.dwMaxUsers = 1;
                uint uClassKind = Parse(Request["dwClassKind"]);
                if (kindValue.dwProperty == null)
                {
                    kindValue.dwProperty = 0;
                }
                if ((uClassKind & (uint)UNIDEVCLS.DWKIND.CLSKIND_LOAN) > 0)
                {
                    kindValue.dwProperty = (uint)kindValue.dwProperty | (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_LEASE;
                }
                else
                {
                    kindValue.dwProperty = (uint)kindValue.dwProperty & (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_LEASE;
                }
                kindValue.dwProperty = (uint)kindValue.dwProperty | (uint)UNIDEVKIND.DWPROPERTY.DEVPROP_EXCLUSIVE;
                kindValue.szKindName = newAssert.szDevName + "-" + newAssert.szModel + "-" + newAssert.szFuncCode;
                if (m_Request.Device.DevKindSet(kindValue, out kindValue) == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    newAssert.dwPurchaseDate = GetDate(Request["dwPurchaseDate"]);
                    if (m_Request.Assert.AssertWarehousing(newAssert, out newAssert) != REQUESTCODE.EXECUTE_SUCCESS)
                    {
                        MessageBox(m_Request.szErrMessage, "提示", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                        return;
                    }
                    else
                    {
                        MessageBox(szOpName + "成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                        return;
                    }
                }
            }
            

        }
        if (szOp == "set")
        {
            bSet = true;
            szOp2 = "set";
            ASSERTREQ vrGet = new ASSERTREQ();
            vrGet.dwDevID = Parse(Request["id"]);
            UNIASSERT[] vtRes;
            if (m_Request.Assert.AssertGet(vrGet, out vtRes) != REQUESTCODE.EXECUTE_SUCCESS || vtRes == null || vtRes.Length < 1)
            {
                MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else
            {
                szUrl = "../../../../ClientWeb/pro/page/editContent.aspx?h=500&w=720&id=" + vtRes[0].dwKindID.ToString() + "&type=hard3&toolbars=false";
                XmlCtrl xmlCtrl = new XmlCtrl("ics_data", Server.MapPath(MyVPath + "clientweb/upload/info/xmlData/"));
                XmlCtrl.XmlInfo info = xmlCtrl.GetXmlContent(vtRes[0].dwKindID.ToString(), "hard3");
                if (info.content != null && info.content.Trim() != "")
                {
                    szUrl = info.content;
                }
                else
                {
                    szUrl = "";
                }

                szDevSN = vtRes[0].szAssertSN.ToString();
                PutJSObj(vtRes[0]);
                m_Title = "查看资产【" + vtRes[0].szDevName + "】";
            }
        }
        else
        {
            m_Title = "查看";

        }
      
	}
    public uint GetDevKindByName(string szKindName)
    {
        DEVKINDREQ vrGet = new DEVKINDREQ();
        vrGet.szKindName = szKindName;
        UNIDEVKIND[] vtDevKind;
        if (m_Request.Device.DevKindGet(vrGet, out vtDevKind)==REQUESTCODE.EXECUTE_SUCCESS&&vtDevKind!=null&&vtDevKind.Length>0)
        {
            return (uint)vtDevKind[0].dwKindID;
        }
        return 0;
    }
}