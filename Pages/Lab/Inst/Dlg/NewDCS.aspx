<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewDCS.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">    
    <div class="formtitle"><%=m_Title %></div>
    <div class="formtable">
        <table>
            <tr><td>　　　编号：</td><td><input id="dwSN" name="dwSN" class="validate[required,validate[custom[onlyNumber]]"" /></td></tr>
            <tr><td>　　　名称：</td><td><input id="szName" name="szName" class="validate[required]" /></td></tr>
            <tr><td>　所属站点：</td><td><input type="hidden" id="szStaName" name="szStaName"/><select id="dwStaSN" name="dwStaSN"><%=m_szSta %></select></td></tr>
            <tr><td>　　IP地址：</td><td><input id="szIP" name="szIP" class="validate[required,validate[custom[ipv4]]"" /></td></tr>
            <tr><td>　　　备注：</td><td><input id="szMemo" name="szMemo"/></td></tr>
            <tr><td></td><td><button type="submit" id="OK">确定</button><button type="button" id="Cancel">取消</button></td></tr>
        </table>
    </div>            
</form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .formtitle {
            padding:6px;
            background: #d0d0d0;
            height:30px;
            color: #fff;
            font-size: 20px;
        }
        .formtable table{
            text-align:center;
            margin:auto;
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
