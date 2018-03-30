using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Text;

public partial class Pages_Site_SysCourse : System.Web.UI.Page
{
    string szIP = "127.0.0.1";
    int nzSocket = 8060;
    protected void Page_Load(object sender, EventArgs e)
    {
        SocketSend();
    }
  
    private void SocketSend()
    {
        //创建发送数据的Socket
        try
        {
            Socket sendsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //设置发送数据的地址
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(szIP), nzSocket);

            //链接目的地

            sendsocket.Connect(endPoint);

            //发送数据
            string szSendContant = "start";
            byte[] bt = Encoding.Default.GetBytes(szSendContant);
            sendsocket.Send(bt);

            //关闭发送数据的Socket

            sendsocket.Shutdown(SocketShutdown.Send);
            sendsocket.Close();
            Response.Write("开始同步");
            
        }
        catch(Exception ex) {
            Response.Write(ex.ToString());
            Response.End();
        }
    
    }
}