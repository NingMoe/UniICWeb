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
                            <td colspan="2" style="text-align:center">�㽭��ҽҩ��ѧ</td>                           
                        </tr>  
                         <tr>
                            <td colspan="2" style="text-align:center">У�ڽ���ת��ƾ֤��<%=ConfigConst.GCSysName %>��</td>                           
                        </tr>   
                        <tr>
                            <td colspan="2">ת�����ݣ�<%=szTestName %>����������:<%=szDevName %></td>
                        </tr>   
                        <tr>
                            <td colspan="2">ת�˽�����д����<%=szCostBig %> ��<%=szCost %></td>
                        </tr> 
                        <tr>
                            <td colspan="2">��Ŀ���ƣ�<%=szResearchName %>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��Ŀ���:<%=szResearchSN %> </td>
                        </tr>
                          <tr>
                            <td colspan="2">���ڲ���:<%=szDeptName %></td>
                        </tr>  
                        <tr>
                            <td style="text-align:center">ת��</td>
                              <td style="text-align:center">ת��</td>
                        </tr> 


                         <tr>
                            <td>ת����λ��<%=szDeptName %></td>
                              <td>ת�뵥λ��<input type="text" name="inProjectFee" id="Text1" class="txt" /></td>
                        </tr> 
                         <tr>
                            <td>������Ŀ��<%=szFoundNO %></td>
                              <td>������Ŀ��<input type="text" name="outProjectFee" id="outProjectFee" class="txt" value="" /></td>
                        </tr>    
                          <tr>
                            <td>�����ˣ�<input type="text" name="inPerson" id="inPerson" class="txt" /></td>
                              <td>�����ˣ�<input type="text" name="outPerson" id="outPerson" class="txt" /></td>
                        </tr>   
                        <tr>
                            <td>�����ˣ�<input type="text" name="inManaer" id="inManaer" class="txt" /></td>
                              <td>�����ˣ�<input type="text" name="outManaer" id="outManaer" class="txt" /></td>
                        </tr>    
                         <tr>
                            <td colspan="2">���£�</td>
                        </tr> 
                        </table>
                    <div style="margin:5px auto">
                        <input type="button" id="print" value="��ӡ" />
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
