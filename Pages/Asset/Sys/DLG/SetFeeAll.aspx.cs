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
    protected string m_szFee = "";
    protected int nIsAdminSup = 1;
	protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginResult"] != null)
        {
            ADMINLOGINRES adminAcc = (ADMINLOGINRES)Session["LoginResult"];
            uint uManRole = (uint)adminAcc.dwManRole;
            if ((uManRole & (uint)ADMINLOGINRES.DWMANROLE.MANSCOPE_LABCTR) > 0)
            {
                nIsAdminSup = 0;
            }
            else if ((uManRole & (uint)ADMINLOGINRES.DWMANROLE.MANSCOPE_LAB) > 0)
            {
                nIsAdminSup = 0;
            }
            else if ((uManRole & (uint)ADMINLOGINRES.DWMANROLE.MANSCOPE_ROOM) > 0)
            {
                nIsAdminSup = 0;
            }
        }


        UNIFEE newFee;
        uint? uMax = 0;
        uint uID = PRDevice.DEVICE_BASE | PRDevice.MSREQ_LAB_SET;
        if (GetMaxValue(ref uMax, uID, "dwLabID"))
        {

        }
        if (IsPostBack)
        {
            GetHTTPObj(out newFee);
            if (m_Request.Fee.Set(newFee, out newFee) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "修改收费标准失败", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
            }
            else
            {
                MessageBox("修改收费标准成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                return;
            }
        }
       
        if (Request["op"] == "set")
        {
            bSet = true;

            FEEREQ vrFeeGet = new FEEREQ();
            string szKindID = Request["kindid"];
            string  szID=Request["dwID"];
            if (szKindID != null && szKindID != "")
            {
                vrFeeGet.dwDevKind = Parse(szKindID);
            }
            else if (szID != null && szID != "")
            {
                vrFeeGet.dwFeeSN = Parse(szID);
            }
            UNIFEE[] vtFee;
            if (m_Request.Fee.Get(vrFeeGet, out vtFee) != REQUESTCODE.EXECUTE_SUCCESS)
            {
                MessageBox(m_Request.szErrMessage, "获取失败", MSGBOX.ERROR, MSGBOX_ACTION.CANCEL);
            }
            else
            {
                for (int i = 0; i < vtFee.Length; i++)
                {
                    if (vtFee[i].dwIdent != null && vtFee[i].dwIdent != (uint)UNIACCOUNT.DWIDENT.EXTIDENT_INNER)
                    {
                        string szCheck = "";
                        if (i == 0)
                        {
                            szCheck = " checked=\"true\"";

                            int uFeeDetailLen = 0;
                            ViewState["feeSN"] = vtFee[i].dwFeeSN.ToString();
                            ViewState["ident"] = vtFee[i].dwIdent.ToString();
                            if (vtFee[i].szFeeDetail != null)
                            {
                                uFeeDetailLen = vtFee[i].szFeeDetail.Length;
                                for (int k = 0; k < uFeeDetailLen; k++)
                                {
                                    uint uFeetType = (uint)vtFee[i].szFeeDetail[k].dwFeeType;
                                    uint? uFeeUint = vtFee[i].szFeeDetail[k].dwUnitFee;
                                    uint? uFeeTime = vtFee[i].szFeeDetail[k].dwUnitTime;
                                    if (uFeetType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_USEDEV)
                                    {
                                        ViewState["useFeeUint"] = uFeeUint.ToString();
                                        ViewState["useTimeUint"] = uFeeTime.ToString();
                                    }
                                    else if (uFeetType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_SAMPLE)
                                    {
                                        ViewState["conFeeUint"] = uFeeUint.ToString();
                                        ViewState["conTimeUint"] = uFeeTime.ToString();
                                    }
                                    else if (uFeetType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_ENTRUST)
                                    {
                                        ViewState["entFeeUint"] = uFeeUint.ToString();
                                        ViewState["entTimeUint"] = uFeeTime.ToString();
                                    }
                                    else if (uFeetType == (uint)FEEDETAIL.DWFEETYPE.FEETYPE_SAMPLE)
                                    {
                                        ViewState["sampleFeeUint"] = uFeeUint.ToString();
                                        ViewState["sampleTimeUint"] = uFeeTime.ToString();
                                    }
                                }
                            }
                        }
                        m_szFee += "<input class=\"enum\"" + szCheck + " type=\"radio\" name=\"" + "feeSN" + "\" id='" + vtFee[i].dwFeeSN.ToString() + "' /> <label for=\"" + vtFee[i].dwFeeSN.ToString() + "\">" + GetJustNameEqual(vtFee[i].dwIdent, "Fee_Ident") + "</label>";
                    }
                }
            }
        }
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        if (ViewState["dwKind"] != null)
        {
            PutMemberValue("dwKind", UintToCharList(Parse(ViewState["dwKind"].ToString()), "Console_Kind"));
            PutMemberValue("dwKindListObject", UintToCharList(Parse(ViewState["dwKind"].ToString()), "Console_Kind_Object"));
        }

        if (ViewState["useFeeUint"] != null)
        {
            PutMemberValue("useFeeUint", ViewState["useFeeUint"].ToString());
            PutMemberValue("useTimeUint", ViewState["useTimeUint"].ToString());
        }
        if (ViewState["conFeeUint"] != null)
        {
            PutMemberValue("conFeeUint", ViewState["conFeeUint"].ToString());
            PutMemberValue("conTimeUint", ViewState["conTimeUint"].ToString());
        }
        if (ViewState["entFeeUint"] != null)
        {
            PutMemberValue("entFeeUint", ViewState["entFeeUint"].ToString());
            PutMemberValue("entTimeUint", ViewState["entTimeUint"].ToString());
        }
        if (ViewState["sampleFeeUint"] != null)
        {
            PutMemberValue("sampleFeeUint", ViewState["sampleFeeUint"].ToString());
            PutMemberValue("sampleTimeUint", ViewState["sampleTimeUint"].ToString());
        }
        if (ViewState["feeSN"] != null)
        {
            PutMemberValue("feeSN", ViewState["feeSN"].ToString());
        }
        
    }
}
