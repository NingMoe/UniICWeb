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
using System.Data.OracleClient;
public partial class Sub_Device : UniPage
{
      
    protected void Page_Load(object sender, EventArgs e)
    {
        string ConnectionString = "Data Source=IDC_U_DC;user=idc_u_cs;password=idc_u_cs;";//写连接串
        OracleConnection conn = new OracleConnection(ConnectionString);//创建一个新连接
        DataSet dsHourse = new DataSet();
        dsHourse = getHourse(conn);
       
    }
   
    private DataSet getHourse(OracleConnection conn)
    {
        Logger.trace("cmd.CommandText1");
        DataSet ds = new DataSet();
        try
        {
            Logger.trace("cmd.CommandText2");
            conn.Open();
            Logger.trace("cmd.CommandText3");
            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"select *
from idc_u_dc.v_ast_house_infor
where  (house_code in ('1001','1002','1003','1004','2002','3001','3002','3005','4001','4002')) or (HOUSE_SMALL_CLASSIFY_CODE not in('3.1','3.2','3.3','3.4','1.2') and HOUSE_BIG_CLASSIFY_CODE<>4 and CAMPUS_NAME<>'其他校区' and HOUSE_code   in(select distinct HOUSE_code from  idc_u_dc.V_AST_ROOM_INFOR))";//在这儿写sql语句
            Logger.trace("cmd.CommandText4" + cmd.CommandText);
            cmd.Connection = conn;
            OracleDataAdapter adpt = new OracleDataAdapter(cmd);
           
            adpt.Fill(ds);
            return ds;
        }
        catch (Exception ee)
        {
            Response.Write("获取楼宇"+ee.Message); //如果有错误，输出错误信息
        }
        finally
        {
            Response.Write("获取楼宇"+ ds.Tables[0].Rows.Count);
              conn.Close(); //关闭连接
            
        }
        return null;
    }
    
    
}
