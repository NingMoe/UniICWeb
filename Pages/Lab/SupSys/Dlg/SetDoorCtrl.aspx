<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetDoorCtrl.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">
    <div class="formtitle"><%=m_Title %></div>
    <div class="formtable">        
        <input id="op" name="op" type="hidden" />
        <table>            
            <tr><th>集控器编号：</th><td><input id="dwCtrlSN" name="dwCtrlSN"  class="validate[required]" /></td><th>房间号：</th><td><input id="szRoomNo" name="szRoomNo" /></td></tr>
            <tr><th>类型：</th><td><select id="dwCtrlKind" name="dwCtrlKind"><%=m_dwKind %></select></td><th>所属集控器：</th><td><select id="dwDCSSN" name="dwDCSSN"><%=szDCS %></select></td></tr>
            <tr><th>附加属性：</th><td colspan="3"><%=m_KindProperty %></td></tr>
        
            <tr><td colspan="4" style="text-align:center"><button type="submit" id="OK">确定</button><button type="button" id="Cancel">取消</button></td></tr>
        </table>
    </div>
</form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">      
        .formtable table{
            text-align:center;
            margin:auto;
        }
        .formtable td label
        {
            margin-right:10px;
        }
        td {
         padding:6px;
        }
        input, select {
            width: 200px;
        }
    </style>
<script language="javascript" type="text/javascript" > 
    $(function () {      
       
       
        $("#OK").button();
        $("#Cancel").button().click(Dlg_Cancel);
      
    });
   
</script>
</asp:Content>
