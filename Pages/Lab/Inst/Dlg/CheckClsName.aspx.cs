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
    protected string m_Title = "校正上课班级名称";
    protected void Page_Load(object sender, EventArgs e)
    {
        string szResvIDs = Request["id"];
        if (this.Page.IsPostBack)
        {
            string[] resvIDList = szResvIDs.Split(',');
            for (int i = 0; i < resvIDList.Length; i++)
            {
                if (resvIDList[i] == "")
                {
                    continue;
                }
                UNIRESERVE resv;
                if (GetResvByID(resvIDList[i], out resv))
                {
                    UNIGROUP[] groupList = GetGroupByID((uint)resv.dwMemberID);
                    if (groupList != null && groupList.Length == 1)
                    {
                        resv.szMemberName = groupList[0].szName;
                    }
                    REQUESTCODE uResponse=m_Request.Reserve.Set(resv, out resv);
                }
            }
            MessageBox("校正完成", "提示", MSGBOX.SUCCESS,MSGBOX_ACTION.OK);
            return;
        }
       
    }

}
