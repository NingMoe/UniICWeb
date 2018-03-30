<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="ChangEndTime.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">
    <div style="width: 100%">
            <div style="width: 200px; margin: 0 auto;">
                <label style="font-size: 20px; font-weight: bold;">
                    调整结束时间</label>
            </div>
        </div>
         <div align="center">
            <hr style="border: 1px; width: 100%; height: 1px" />
        </div>
        <div>
            <div class="tem_panel">
                <div class="apply_head">
                </div>
                <div class="resv_apply">
                    <table style="margin:10px auto;">
                        <td>
                    新的结束时间:
                            </td><td><asp:TextBox ID="dwEndTime" runat="server"></asp:TextBox> 
                                </td>
                        <td>
                        <asp:Button ID="btn" runat="server" Text="调整" OnClick="btn_Click"  Width="100px" Height="28px" />                   
                            </td>
                        </table>
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
        .resv_apply table td{border-left:1px solid #000;height:30px; border-top:1px solid #000;text-align:center;height:35px;}
      
    </style>
<script language="javascript" type="text/javascript" > 
    $(function () {
        $("#<%=btn.ClientID%>").button();
        $("#<%=dwEndTime.ClientID%>").datetimepicker({
            stepHour: 1,
            stepMinute: 2
        });
        setTimeout(function () {
           
        }, 1);
    });
</script>
</asp:Content>
