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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            YARDRESVREQ vrGet = new YARDRESVREQ();
            string szResvID = Request["id"];
            string szText = Request["szMessageInfo"];
            if (szResvID == null || szResvID == "")
            {                
                MessageBox("设置失败", "提示", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
                return;
            }
            vrGet.dwResvID = Parse(szResvID);
            YARDRESV[] vtRes;
            if (m_Request.Reserve.GetYardResv(vrGet,out vtRes)==REQUESTCODE.EXECUTE_SUCCESS&& vtRes!=null&&vtRes.Length>0)
            {
                YARDRESV outResv = new YARDRESV();
                outResv = vtRes[0];
                outResv.szHostUnit = szText;
                if (m_Request.Reserve.DelYardResv(outResv) == REQUESTCODE.EXECUTE_SUCCESS)
                {
                    MessageBox("取消成功", "提示", MSGBOX.SUCCESS, MSGBOX_ACTION.OK);
                }
                else
                {
                    MessageBox(m_Request.szErrMessage.ToString(), "提示", MSGBOX.ERROR, MSGBOX_ACTION.NONE);
 
                }
            }
        }

    }
}
