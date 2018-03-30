<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="ResvBeforeDone.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">
    <input type="hidden" id="dwDCSKind" name="dwDCSKind" value="1"/>
 
    <div class="formtable">
        <table>
            <tr><td style="text-align:center">  <div class="formtitle"><%=m_Title %> </div></td></tr>
         <!--   <tr><td style="text-align:center"><label><input class="enum" type="checkbox" name="redit" value="1" checked="checked">添加违约记录</label></td></tr>-->
            <tr><td style="text-align:center"><button type="submit" id="OK">确定</button><button type="button" id="Cancel">取消</button></td></tr>
        </table>
    </div>
</form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
    </style>
<script language="javascript" type="text/javascript" >
   $(function () {  
     $('#szStaName').val($("#dwStaSN").find("option:selected").text());        
      $("#dwStaSN").change(function () {
            $("#szStaName").val($(this).find("option:selected").text());
        });  
        $("#OK").button();
        $("#Cancel").button().click(Dlg_Cancel);
        setTimeout(function () {
           
        }, 1);
    });
</script>
</asp:Content>
