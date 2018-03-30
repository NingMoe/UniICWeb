<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="CheckBill.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">
    <div style="width: 100%">
            <div style="width: 120px; margin: 0 auto;">
                <label style="font-size:18px; font-weight: bold;">
                    预收费</label>
            </div>
        </div>
         <div align="center">
            <hr style="border: 1px; width: 100%; height: 1px" />
        </div>
           <div align="center">
            <hr style="border: 1px; width: 100%; height: 1px" />
        </div>
                <div class="resv_apply">
                    <table style="margin:10px auto;">
                        <tr>
                            <td>
                   <label style="font-size:14px; font-weight:bold;">预收费用:</label> </td>
                            <td><asp:TextBox ID="idYshou" runat="server"></asp:TextBox>   </td>
                            <td>
                        <asp:Button ID="btn" runat="server" Text="收取预收费" Width="100px" OnClick="btn_Click" Height="28px" />                   
                                </td>
                </tr>
                </table>
                    </div>
                   <div align="center">
            <hr style="border: 1px; width: 100%; height: 1px" />
        </div>
                    <div class="apply_item">
                    <table style="margin:10px auto;">
                    <tr>
                        <th>
                            收费类别</th>
                        <th>
                            费率</th>
                      <th>
                            预估费用
                        </th>
                    </tr>
                    <tr id="divUseDev" style="display: none;" runat="server">
                        <td>
                            使用费:</td>
                        <td>
                            <asp:Label ID="lblUseDevFee" runat="server"></asp:Label>
                           元/每小时
                        </td>
                       
                        <td>
                            <asp:Label ID="lblUseDevTotal" runat="server"></asp:Label>
                         </td>
                    </tr>
                    <tr id="divOccupy" style="display: none;" runat="server">
                        <td>
                            占用费:</td>
                        <td>
                             <asp:Label ID="lblOccupy" runat="server"></asp:Label>
                          元/每小时
                        </td>
                      
                        <td>
                            <asp:Label ID="lblOccupyTotal" runat="server"></asp:Label>
                           </td>
                    </tr>
                    <tr id="divASSIST" style="display: none;" runat="server">
                        <td>
                            协助费:</td>
                        <td>
                             <asp:Label ID="lblASSIS" runat="server"></asp:Label>
                            元/每小时
                        </td>
                       
                        <td>
                            <asp:Label ID="lblASSISTotal" runat="server"></asp:Label>
                           </td>
                    </tr>
                    <tr id="divTIMEOUT" style="display: none;" runat="server">
                        <td>
                            超时费:</td>
                        <td>
                            <asp:Label ID="lblTIMEOUT" runat="server"></asp:Label>
                           元(每小时)
                        </td>
                        
                        <td>
                            <asp:Label ID="lblTIMEOUTTotal" runat="server"></asp:Label>
                           </td>
                    </tr>
                    <tr id="divCONSUMABLE" style="display: none;" runat="server">
                        <td>
                            耗材费:</td>
                        <td>
                            <asp:Label ID="lblCONSUMABLE" runat="server"></asp:Label>
                        </td>
                       
                        <td>
                            <asp:Label ID="lblCONSUMABLETotal" runat="server"></asp:Label>
                            </td>
                    </tr>
                         <tr id="divSample" style="display: none;" runat="server">
                        <td>
                            样本费:</td>
                        <td>
                            <asp:Label ID="lblSample" runat="server"></asp:Label>
                        </td>
                       
                        <td>
                            <asp:Label ID="lblSampleTotal" runat="server"></asp:Label>
                            </td>
                    </tr>
                    <tr id="divRESVDEV" style="display: none;" runat="server">
                        <td>
                            预约使用费:</td>
                        <td>
                           <asp:Label ID="lblRESVDEV" runat="server"></asp:Label>元/每小时
                        </td>
                        
                        <td>
                            <asp:Label ID="lblRESVDEVTotal" runat="server"></asp:Label>
                           </td>
                    </tr>
                    <tr id="divENTRUST" style="display: none;" runat="server">
                        <td>
                            代检费:
                        </td>
                        <td>
                            <asp:Label ID="lblENTRUST" runat="server"></asp:Label>元/每小时
                        </td>
                       
                        <td>
                            <asp:Label ID="lblENTRUSTTotal" runat="server"></asp:Label>
                            </td>
                    </tr>
                    
                </table>
                    </div>
               
            <div class="apply_footer">
            </div>      
      
</form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .resv_apply table{border-bottom:1px solid #000;border-right:1px solid #000;width:400px;}
        .resv_apply table td{border-left:1px solid #000;height:30px; border-top:1px solid #000;text-align:center;height:35px;}

        .apply_item table{border-left:1px solid #000; border-top:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000;width:400px;}
        .apply_item table td{border-left:1px solid #000;height:30px; border-top:1px solid #000;text-align:center;height:35px;}
        .apply_item table th{width:157px;text-align:center;height:30px; vertical-align:middle;font-size:14px;font-weight:bold;background-color:#f5f5f5;}
        
    </style>
<script language="javascript" type="text/javascript" > 
    $(function () {
        $("#<%=btn.ClientID%>").button();
        setTimeout(function () {
           
        }, 1);
    });
</script>
</asp:Content>
