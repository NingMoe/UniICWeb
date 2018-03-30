using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;

public partial class ClientWeb_xcus_all_chooseseat : UniClientPage
{
    protected string rmImg;
    protected void Page_Load(object sender, EventArgs e)
    {
        string path = ToUploadUrl("DevImg/FloorPlan/rm" + Request["roomId"] + ".jpg");
        if (!File.Exists(Server.MapPath(path)))
        {
            MsgBoxH("缺少平面图<br/>" + path);
        }
        else
        {
            rmImg = path;
        }
    }
}