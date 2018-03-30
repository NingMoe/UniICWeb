<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewDevDemage.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">
    <div class="formtitle"><%=m_Title %></div>
    <input type="hidden" id="dwDevID" name="dwDevID" />
    <div class="formtable">
        <table>
            <tr><th><%=ConfigConst.GCDevName %>��ţ�</th><td><input id="dwDevSN" name="dwDevSN"/></td></tr>
            <!--<tr><th><%=ConfigConst.GCDevName %>��ţ�</th><td><input id="szAssertSN" name="szAssertSN"/></td></tr>-->
            <tr><th><%=ConfigConst.GCDevName %>���ƣ�</th><td><div id="szDevName" name="szDevName"/></td></tr>
            <tr><th><%=ConfigConst.GCDevName %>����<%=ConfigConst.GCRoomName %>��</th><td><div id="szRoomName" name="szRoomName"/></td></tr>
            <tr><th>������˵����</th><td><input id="szDamageInfo" name="szDamageInfo"/></td></tr>
<!--            <tr><th>��Ԥ��ֵ(Ԫ)��</th><td><input id="dwRepareCost" name="dwRepareCost" class="validate[custom[onlyFee]]" /></td></tr>-->
            <tr><th></th><td><button type="submit" id="OK">ȷ��</button><button type="button" id="Cancel">�ر�</button></td></tr>
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
       $("#dwDevSN").autocomplete({
           source: "../../data/searchdevice.aspx?Type=assertsn",
            minLength: 0,
            select: function (event, ui) {
                if (ui.item) {
                    if (ui.item.id && ui.item.id != "") {
                        $("#dwDevSN").val(ui.item.szAssertSN);
                        $("#szDevName").html(ui.item.label);
                        $("#szRoomName").html(ui.item.szRoom);
                        $("#dwDevID").val(ui.item.id);
                    }
                }
                return false;
            },
            response: function (event, ui) {
                if (ui.content.length == 0) {
                    ui.content.push({ label: " δ�ҵ������� " });
                }
            }
        }).blur(function () {
            if ($(this).val() == "") {
                $("#dwDevSN").val("");
            } else {

            }
        }).click(function () {
            $("#dwDevSN").autocomplete("search", "");
        });
        setTimeout(function () {
           
        }, 1);
    });
</script>
</asp:Content>
