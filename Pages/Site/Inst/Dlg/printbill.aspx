<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="printbill.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">
    <div style="width: 100%">           
        </div>
         <div align="center">
            <hr style="border: 1px; width: 100%; height: 1px" />
        </div>
    <div id="hiden" style="height:150px">

    </div>
        <div>
            <div class="tem_panel">
                <div class="apply_head">
                </div>
                <div class="resv_apply" style="text-align:center">
                    <table style="width:650px;margin:5px auto">  
                        <tr>
                            <td colspan="2" style="text-align:center">浙江中医药大学</td>                           
                        </tr>  
                         <tr>
                            <td colspan="2" style="text-align:center">校内结算转账凭证（<%=ConfigConst.GCSysName %>）</td>                           
                        </tr>   
                        <tr>
                            <td colspan="2">转账内容：<%=szTestName %>，仪器名称:<%=szDevName %></td>
                        </tr>   
                        <tr>
                            <td colspan="2">转账金额：（大写）：<%=szCostBig %> ￥<%=szCost %></td>
                        </tr> 
                        <tr>
                            <td colspan="2">项目名称：<%=szResearchName %>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;项目编号:<%=szResearchSN %> </td>
                        </tr>
                          <tr>
                            <td colspan="2">所在部门:<%=szDeptName %></td>
                        </tr>  
                        <tr>
                            <td style="text-align:center">转出</td>
                              <td style="text-align:center">转入</td>
                        </tr> 


                         <tr>
                            <td>转出单位：<%=szDeptName %></td>
                              <td>转入单位：<input type="text" name="inProjectFee" id="Text1" class="txt" /></td>
                        </tr> 
                         <tr>
                            <td>经费项目：<%=szFoundNO %></td>
                              <td>经费项目：<input type="text" name="outProjectFee" id="outProjectFee" class="txt" value="" /></td>
                        </tr>    
                          <tr>
                            <td>经办人：<input type="text" name="inPerson" id="inPerson" class="txt" /></td>
                              <td>经办人：<input type="text" name="outPerson" id="outPerson" class="txt" /></td>
                        </tr>   
                        <tr>
                            <td>主管人：<input type="text" name="inManaer" id="inManaer" class="txt" /></td>
                              <td>主管人：<input type="text" name="outManaer" id="outManaer" class="txt" /></td>
                        </tr>    
                         <tr>
                            <td colspan="2">盖章：</td>
                        </tr> 
                        </table>
                    <div style="margin:5px auto">
                        <input type="button" id="print" value="打印" />
                    </div>
                </div>
            </div>
            <div class="apply_footer">
            </div>      
        </div>
</form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
       .resv_apply table{border-bottom:1px solid #000;border-right:1px solid #000;width:400px;}
        .resv_apply table td{border-left:1px solid #000;height:30px; border-top:1px solid #000;font-size:13px;font-weight:bold; text-align:left;height:35px;}
        .txt
        {
            width:150px;
        }
    </style>
<script language="javascript" type="text/javascript" > 
    $(function () {
        $("#hiden").hide();
        $("#print").button()
           .click(function () {
               $("#print").hide();
               $("#hiden").show();
               setTimeout(function () {
                   $("#print").show();
                   $("#hiden").hide();
               }, 1);
              
               window.print();
           });
        setTimeout(function () {
          
        }, 2000);
       
    });
</script>
</asp:Content>
