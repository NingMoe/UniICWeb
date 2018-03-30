using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniWebLib;
using Newtonsoft.Json;
using System.Reflection;

public partial class MobileClient_Ajax_ALogin : PageBase
{
    public class DevKindMin
    {
        public uint kindID;
        public string KindName;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        uint uClassKind = Parse(Request["classKind"]);
        uint uKindID = Parse(Request["KindID"]);
        if (m_Request == null)
        {
            Response.Write("{\"error\":\"\"}");
            return;
        }
        DEVKINDREQ vrGet = new DEVKINDREQ();
        if (uKindID != 0)
        {
            vrGet.dwKindID = uKindID;
        }
        vrGet.dwClassKind = uClassKind;
        vrGet.szReqExtInfo.dwNeedLines = 10000;
        vrGet.szReqExtInfo.dwStartLine = 0;
        UNIDEVKIND[] vtRes;
        m_Request.m_UniDCom.StaSN = 1;
        
        if (m_Request.Device.DevKindGet(vrGet, out vtRes) == REQUESTCODE.EXECUTE_SUCCESS&&vtRes!=null)
        {
            if (uKindID != 0)
            {
                DevKindMin temp = new DevKindMin();
                temp.kindID = (uint)vtRes[0].dwKindID;
                temp.KindName = vtRes[0].szKindName;
                string szRes = JsonConvert.SerializeObject(temp);
                Response.Write(szRes);
                return;
            }
            else
            {
                List<DevKindMin> list = new List<DevKindMin>();
                for (int i = 0; i < vtRes.Length; i++)
                {
                    DevKindMin temp = new DevKindMin();
                    temp.kindID = (uint)vtRes[i].dwKindID;
                    temp.KindName = vtRes[i].szKindName;
                    list.Add(temp);
                }
                string szRes = JsonConvert.SerializeObject(list);
                Response.Write(szRes);
                return;
            }
        }
        else {
            Response.Write("{\"error\":\"" + m_Request.szErrMessage + "\"}");
        }


    }
    public string GetHourStr(uint uMin)
    {
        if (uMin < 60)
        {
            return uMin + "分钟";
        }
        else if (uMin < (60 * 24) && uMin>=60)
        {
            if ((uMin % 60) == 0)
            {
                return uMin / 60 + "小时";
            }
            return uMin / 60 + "小时" + uMin % 60 + "分钟";
        }
        else if (uMin >= (60 * 24))
        {
            return GetTimeForSecond(uMin*60);
        }
        return "";
    }
}