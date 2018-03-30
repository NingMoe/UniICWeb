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
    protected string m_KindProperty = "";
    protected string m_dwKind = "";
    protected string m_dwDevClass= "";
    protected string szDownLoadUrl = "";
    protected string szOpDownload = "";
	protected void Page_Load(object sender, EventArgs e)
    {
        UNIDEVKIND newDevKind;
        int uNew = ConfigConst.GCKindAndClass;
        if (IsPostBack)
        {
            GetHTTPObj(out newDevKind);
            UNIDEVCLS newDevClass = new UNIDEVCLS();
            string szIsOpen = Request["isOpen"];
            if (uNew == 1)
            {
                newDevClass.dwClassID = newDevKind.dwClassID;
                newDevClass.dwKind = newDevKind.dwClassKind;
                newDevClass.szClassName = newDevKind.szKindName;
                newDevClass.dwResv1 = Parse(Request["dwResv1"]);
                if (szIsOpen != null && szIsOpen == "1")
                {
                    newDevClass.szMemo = "false";
                    newDevKind.dwProperty = (uint)newDevKind.dwProperty | (uint)(UNIDEVICE.DWPROPERTY.DEVPROP_NORESV);
                }
                else
                {
                     if ((((uint)newDevKind.dwProperty) & ((uint)(UNIDEVICE.DWPROPERTY.DEVPROP_NORESV))) > 0)
                    {
                        newDevKind.dwProperty = (uint)newDevKind.dwProperty -(uint)(UNIDEVICE.DWPROPERTY.DEVPROP_NORESV);
                    }
                    newDevClass.szMemo = "";
                }
                if (((uint)newDevKind.dwMinUsers) > 1)
                {
                    newDevClass.dwKind = (uint)UNIDEVCLS.DWKIND.CLSCOMMONS_MULTIPLE;
                }
                REQUESTCODE uRes = m_Request.Device.DevClsSet(newDevClass, out newDevClass);
                if (uRes != REQUESTCODE.EXECUTE_SUCCESS)
                {
                    MessageBox("修改" + ConfigConst.GCKindName + "成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                    return;
                }
                else
                {
                    newDevKind.dwClassID = newDevClass.dwClassID;
                    newDevKind.szClassName = newDevClass.szClassName;
                }
            }
            newDevKind.dwProperty = CharListToUint(Request["dwProperty"]);
            if (szIsOpen != null && szIsOpen == "1")
            {
              
                newDevKind.dwProperty = (uint)newDevKind.dwProperty | (uint)(UNIDEVICE.DWPROPERTY.DEVPROP_NORESV);
            }
            else
            {
                if ((((uint)newDevKind.dwProperty) & ((uint)(UNIDEVICE.DWPROPERTY.DEVPROP_NORESV))) > 0)
                {
                    newDevKind.dwProperty = (uint)newDevKind.dwProperty - (uint)(UNIDEVICE.DWPROPERTY.DEVPROP_NORESV);
                }
             
            }
            {
                string fileName = Request.Files["fileurl"].FileName;
                if (fileName != null && fileName != "")
                {
                    string szFileExtName = "";
                    szFileExtName = fileName.Substring(fileName.LastIndexOf('.'));
                    string szTempPath = MyVPath + "Upload/ic/" + fileName.Substring(0, fileName.LastIndexOf('.')) + newDevKind.szKindName.ToString() + newDevKind.dwKindID.ToString() + szFileExtName;

                    string szTempRawPath = Server.MapPath(szTempPath);
                    Request.Files[0].SaveAs(szTempRawPath);
                    newDevKind.szDevKindURL = szTempPath;
                }
                string szIsUpload = Request["isUpload"];
                if (szIsUpload != null && szIsUpload == "1")
                {
                    newDevKind.szDevKindURL = "";
                }
            }
            newDevKind.dwNationCode = Parse(Request["dwResv1"]);
            if (m_Request.Device.DevKindSet(newDevKind, out newDevKind) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "修改" + ConfigConst.GCKindName + "失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox("修改" + ConfigConst.GCKindName + "成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);

                return;
            }
        }
        UNIDEVCLS[] vtDevCls = GetDevClsByKind((uint)UNIDEVCLS.DWKIND.CLSKIND_COMMONS);
        if (uNew == 0)
        {
            if (vtDevCls != null && vtDevCls.Length > 0)
            {
                for (int i = 0; i < vtDevCls.Length; i++)
                {
                    m_dwDevClass += GetInputItemHtml(CONSTHTML.option, "", vtDevCls[i].szClassName, vtDevCls[i].dwClassID.ToString());
                }

            }

        }
        m_KindProperty = GetAllInputHtml(CONSTHTML.checkBox, "dwProperty", "DevKind_dwProperty");
        m_dwKind = GetAllInputHtml(CONSTHTML.option, "", "DevClass_dwKind");
        if (Request["op"] == "set")
        {
            UNIDEVKIND devKind;
            if (GetDevKindByID(Request["id"], out devKind))
            {
                PutHTTPObj(devKind);
                szDownLoadUrl = devKind.szDevKindURL;
                if (szDownLoadUrl != null && szDownLoadUrl != "")
                {
                    szOpDownload = "可下载";
                }
                UNIDEVCLS devCls;
                if(GetDevCLSByID(devKind.dwClassID.ToString(),out devCls))
                {
                    if (devCls.szMemo == "false")
                    {
                        PutMemberValue("isOpen", "1");
                    }
                    PutMemberValue("dwResv1", devCls.dwResv1.ToString());
                }
            }
            bSet = true;
        }
        else
        {
            m_Title = "修改" + ConfigConst.GCKindName;
        }
    }

}
