<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="BillRecevie.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">
    <div style="width: 100%">
            <div style="width: 120px; margin: 0 auto;">
                <label style="font-size:18px; font-weight: bold;">����</label>
            </div>
        <div style="width: 99%; margin: 0 auto;font-size:16px">
             ���������ܼ�<label style="font-size:18px;font-weight:bolder"><%=szTotalHtml %></label>
            </div>
        </div>
         <div class="apply_item">
                    <table style="margin:10px auto;" width="800">
                        
                   <tr>
                <th>�շ����</th>
                <th>����</th>
                <th>�������Էѱ���(%)</th>
                <th>���Ż������(%)</th>
                <th>����ѱ�����%��</th>
            </tr>
            <tr>
                <td>ʹ�÷�</td>
                 <td><asp:Label ID="devUseTotal" runat="server"></asp:Label></td>
                <td><asp:Label ID="useUseRate" runat="server"></asp:Label></td>
                <td><asp:Label ID="useOpenRate" runat="server"></asp:Label></td>
                <td><asp:Label ID="useServiceRate" runat="server"></asp:Label></td>
            </tr>  
             <tr>
                <td>������</td>
                    <td><asp:Label ID="sampleTotal" runat="server"></asp:Label></td>
                <td><asp:Label ID="sampleTestRate" runat="server"></asp:Label></td>
                <td><asp:Label ID="sampleOpenRate" runat="server"></asp:Label></td>
                <td><asp:Label ID="sampleServiceRate" runat="server"></asp:Label></td>
            </tr> 
                         <tr>
                <td>�Ĳķ�</td>
                    <td><asp:Label ID="conTotal" runat="server"></asp:Label></td>
                <td><asp:Label ID="conTestRate" runat="server"></asp:Label></td>
                <td><asp:Label ID="conOpenRate" runat="server"></asp:Label></td>
                <td><asp:Label ID="conServiceRate" runat="server"></asp:Label></td>
            </tr> 
             <tr>
                <td>�����</td>
                  <td><asp:Label ID="entTotal" runat="server"></asp:Label></td>
                <td><asp:Label ID="entTestRate" runat="server"></asp:Label></td>
                <td><asp:Label ID="entOpenRate" runat="server"></asp:Label></td>
                <td><asp:Label ID="entServiceRate" runat="server"></asp:Label></td>
            </tr>
                        <tr>
                <td>�ϼ�</td>
                             <td><asp:Label ID="Total" runat="server" Font-Size="14px" Font-Bold="true"></asp:Label></td>
                <td><asp:Label ID="UseTotal" runat="server"></asp:Label></td>
                <td><asp:Label ID="TestTotal" runat="server"></asp:Label></td>
                <td><asp:Label ID="ServicesTotal" runat="server"></asp:Label></td>
           </tr>   
                </table>
                    </div>
               
            <div class="apply_footer">
                <div style="margin:10px auto; text-align:center">
                 <asp:Button ID="btn" runat="server" Text="����" Width="100px" OnClick="btn_Click" Height="28px" /> 
                    </div>
            </div>      
      
</form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .resv_apply table{border-bottom:1px solid #000;border-right:1px solid #000;width:650px;}
        .resv_apply table td{border-left:1px solid #000;height:30px; border-top:1px solid #000;text-align:center;height:35px;}

        .apply_item table{border-left:1px solid #000; border-top:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000;width:650px;}
        .apply_item table td{width:200px;border-left:1px solid #000;border-top:1px solid #000;text-align:center;height:35px;}
        .apply_item table th{width:200px;text-align:center;vertical-align:middle;font-size:14px;font-weight:bold;background-color:#f5f5f5;}
        
    </style>
<script language="javascript" type="text/javascript" > 
    $(function () {
        $("#<%=btn.ClientID%>").button();
        setTimeout(function () {
           
        }, 1);
    });
</script>
</asp:Content>

