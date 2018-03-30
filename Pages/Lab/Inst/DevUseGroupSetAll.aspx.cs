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

public partial class Sub_Device : UniPage
{
    protected string m_szOpts = "";
    protected MyString m_szOut = new MyString();

    protected void Page_Load(object sender, EventArgs e)
    {


        DEVREQ vrParameter = new DEVREQ();
        UNIDEVICE[] vrResult;
      //  vrParameter.dwProperty = (uint)(UNIDEVKIND.DWPROPERTY.DEVPROP_EXCLUSIVE | UNIDEVKIND.DWPROPERTY.DEVPROP_SHARE);
       // GetPageCtrlValue(out vrParameter.szReqExtInfo);

        if (m_Request.Device.Get(vrParameter, out vrResult) == REQUESTCODE.EXECUTE_SUCCESS)
        {
            for (int i = 0; i < vrResult.Length; i++)
            {
                UNIDEVICE setDev = new UNIDEVICE();
                setDev = vrResult[i];
                uint uUseGroup = (uint)vrResult[i].dwUseGroupID;
                if (uUseGroup == null || uUseGroup == 0)
                {
                    
                    string szDevName = vrResult[i].szDevName;
                    Logger.trace(szDevName+"使用组为空");
                    UNIGROUP useGroup = new UNIGROUP();
                    if (NewGroup(szDevName + "使用组", (uint)UNIGROUP.DWKIND.GROUPKIND_DEV, out useGroup))
                    {
                        setDev.dwUseGroupID = useGroup.dwGroupID;
                        m_Request.Device.Set(setDev, out setDev);
                    }
                }
            }
            
        }
        PutBackValue();
    }
}
