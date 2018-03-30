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
using UniLibrary;
public partial class _Default : UniPage
{
    protected bool bSet = false;
    protected string m_Title = "";
    protected string m_KindProperty = "";
    protected string m_dwKind = "";
    protected string szDCS = "";
    uint uDCSKIND=(uint)UNIDCS.DWDCSKIND.DCSKIND_DOORCTRL;
	protected void Page_Load(object sender, EventArgs e)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        UNIDOORCTRL setDoorCtrl;
        string szOp = Request["op"];
        uint? uMax = 0;
        uint uID = PRDevice.DEVICE_BASE | PRDevice.MSREQ_LAB_SET;
        if (GetMaxValue(ref uMax, uID, "dwLabID"))
        {

        }
        if (IsPostBack)
        {
            GetHTTPObj(out setDoorCtrl);
            uint uCtrlKind=(uint)setDoorCtrl.dwCtrlKind;
            uint uCtrlKind2 = CharListToUint(Request["dwCtrlKind2"]);
            setDoorCtrl.dwCtrlKind = uCtrlKind + uCtrlKind2;
            UNIDCS dcs = new UNIDCS();
            if(GetDCSBySN(setDoorCtrl.dwDCSSN.ToString(),out dcs))
            {
                setDoorCtrl.dwStaSN = dcs.dwStaSN;
                setDoorCtrl.szStaName = dcs.szStaName;
                setDoorCtrl.dwDCSKind = dcs.dwDCSKind;
            }
            if (szOp == "new")
            {
                if (m_Request.DoorCtrlSrv.SetDoorCtrl(setDoorCtrl, out setDoorCtrl) != REQUESTCODE.EXECUTE_SUCCESS)
                {
                    MessageBox(m_Request.szErrMessage, "新建失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                }
                else
                {
                    MessageBox("新建成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                    return;
                }
            }
            else
            {
                if (m_Request.DoorCtrlSrv.SetDoorCtrl(setDoorCtrl, out setDoorCtrl) != REQUESTCODE.EXECUTE_SUCCESS)
                {
                    MessageBox(m_Request.szErrMessage, "修改失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                }
                else
                {
                    MessageBox("修改成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                    return;
                }
 
            }
        }
        m_KindProperty = GetAllInputHtml(CONSTHTML.checkBox, "dwCtrlKind2", "UNIDOORCTRL_CtrlKind2");
        m_dwKind = GetAllInputHtml(CONSTHTML.option, "", "UNIDOORCTRL_CtrlKind");
        DCSREQ vrDcsReq = new DCSREQ();
        vrDcsReq.dwDCSKind = uDCSKIND;
        UNIDCS[] vtDcs;
        uResponse=m_Request.DoorCtrlSrv.GetDCS(vrDcsReq, out vtDcs);
        if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && vtDcs != null && vtDcs.Length > 0)
        {
            for (int i = 0; i < vtDcs.Length; i++)
            {
                szDCS += GetInputItemHtml(CONSTHTML.option, "", vtDcs[i].szName.ToString(), vtDcs[i].dwSN.ToString());
            }
        }
        if (Request["op"] == "set")
        {
            DOORCTRLREQ doorReq = new DOORCTRLREQ();
            doorReq.dwDCSKind = uDCSKIND;
            doorReq.dwGetType = (uint)DOORCTRLREQ.DWGETTYPE.DOORCTRLGET_BYSN;
            doorReq.szGetKey = Request["dwSN"];
            UNIDOORCTRL[] door;
            uResponse = m_Request.DoorCtrlSrv.GetDoorCtrl(doorReq, out door);
            if (uResponse == REQUESTCODE.EXECUTE_SUCCESS && door != null && door.Length > 0)
            {
                uint uCtrlKind = (uint)door[0].dwCtrlKind;
                string szCtrlKind1 = "";
                if ((uCtrlKind & (uint)UNIDOORCTRL.DWCTRLKIND.DCKIND_ROOM) > 0)
                {
                    szCtrlKind1 = ((uint)UNIDOORCTRL.DWCTRLKIND.DCKIND_ROOM).ToString();
                }
                else if ((uCtrlKind & (uint)UNIDOORCTRL.DWCTRLKIND.DCKIND_CHANNELGATE) > 0)
                {
                    szCtrlKind1 = ((uint)UNIDOORCTRL.DWCTRLKIND.DCKIND_CHANNELGATE).ToString();
                }
                else if ((uCtrlKind & (uint)UNIDOORCTRL.DWCTRLKIND.DCKIND_POWERCTRL) > 0)
                {
                    szCtrlKind1 = ((uint)UNIDOORCTRL.DWCTRLKIND.DCKIND_POWERCTRL).ToString();
                }

                ViewState["szCtrlKind1"] = szCtrlKind1;
                string szCtrlKind2 = "";
                ArrayList list2 = GetListFromXml("UNIDOORCTRL_CtrlKind2", uCtrlKind, false);
                for (int i = 0; i < list2.Count; i++)
                {
                    CStatue temp = (CStatue)list2[i];
                    szCtrlKind2 += temp.szValue + ",";
                }
                ViewState["szCtrlKind2"] = szCtrlKind2;
                PutHTTPObj(door[0]);
            }
            m_Title = "修改控制器";
        }
        else
        {
            m_Title = "新建控制器";
        }
    }
    private bool GetDCSBySN(string szSN,out UNIDCS dcs)
    {
        REQUESTCODE uResponse = REQUESTCODE.EXECUTE_FAIL;
        dcs = new UNIDCS();
        DCSREQ vrDcsReq = new DCSREQ();
        vrDcsReq.dwDCSKind = uDCSKIND;
        vrDcsReq.dwGetType = (uint)DCSREQ.DWGETTYPE.DCSGET_BYSN;
        vrDcsReq.szGetKey = szSN;
        UNIDCS[] vtDcs;
        uResponse = m_Request.DoorCtrlSrv.GetDCS(vrDcsReq, out vtDcs);
        if(uResponse==REQUESTCODE.EXECUTE_SUCCESS&&vtDcs!=null&&vtDcs.Length>0)
        {
            dcs = vtDcs[0];
            return true;
        }
        return false;
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreLoad(e);
        object szCtrlKind1 = ViewState["szCtrlKind1"];
        if (szCtrlKind1 != null && szCtrlKind1.ToString() != "")
        {
            PutMemberValue2("dwCtrlKind", szCtrlKind1.ToString());
        }
        object szCtrlKind2 = ViewState["szCtrlKind2"];
        if (szCtrlKind2 != null && szCtrlKind2.ToString() != "")
        {
            PutMemberValue2("dwCtrlKind2", szCtrlKind2.ToString());
        }
      
    }
}
