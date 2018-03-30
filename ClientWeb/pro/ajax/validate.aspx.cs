using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_pro_ajax_validate : UniClientAjax
{
    string fieldId;
    string fieldValue;
    string data;
    protected void Page_Load(object sender, EventArgs e)
    {     
        fieldId = Request["fieldId"];
        fieldValue = Request["fieldValue"];
        if (LoadPage())
        {
            data = Request["extraData"];
            //检查用户名
            if (act == "v_is_exist_fail")
            {
                IdIsExist("false");
            }
            else if (act == "v_is_exist_ok")
            {
                IdIsExist("true");
            }
            // 检查联系方式
            else if (act == "v_is_reg_ok")
            {
                IdIsRegister();
            }
        }
        else
        {
            Response.Clear();
            Response.Write("{\"rlt\":[\"" + fieldId + "\",true,\"包含危险字符\"]}");
        }
    }

    private void IdIsRegister()
    {
        ACCREQ req = new ACCREQ();
        UNIACCOUNT[] rlt;
        req.szPID = fieldValue;
        if (m_Request.Account.Get(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS && rlt.Length > 0)
        {
            UNIACCOUNT acc = rlt[0];
            if (!string.IsNullOrEmpty(acc.szHandPhone) && !string.IsNullOrEmpty(acc.szEmail))
            {
                Response.Write("{\"rlt\":[\"" + fieldId + "\",false,\"" + data + "\"]}");//已激活 联系方式全
                return;
            }
        }
        Response.Write("{\"rlt\":[\"" + fieldId + "\",true,\"" + data + "\"]}");//未激活或账户不存在
    }
    void IdIsExist(string jg)
    {
        string rejg = jg=="true" ? "false" : "true";
        ACCREQ req = new ACCREQ();
        UNIACCOUNT[] rlt;
        req.szPID = fieldValue;
        if (m_Request.Account.Get(req, out rlt) == REQUESTCODE.EXECUTE_SUCCESS && rlt.Length > 0)
        {
            Response.Write("{\"rlt\":[\"" + fieldId + "\","+jg+",\"" + data + "\"]}");
        }
        else
        {
            Response.Write("{\"rlt\":[\"" + fieldId + "\","+rejg+",\"" + data + "\"]}");
        }
    }
}