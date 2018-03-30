<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="CheckClsName.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">
    <div style="width: 100%">
           <div class="formtitle"><%=m_Title %></div>  
               
          <div class="formtable">
        <input id="dwLabID" name="dwLabID" type="hidden" />
        <table>
                 <tr><td></td><td><button type="submit" id="OK">确定</button><button type="button" id="Cancel">取消</button></td></tr>
        </table>
    </div>
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
        $("#OK").button();
        $("#Cancel").button().click(Dlg_Cancel);
    });
</script>
</asp:Content>
