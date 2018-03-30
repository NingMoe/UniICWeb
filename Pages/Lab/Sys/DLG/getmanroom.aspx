<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="getmanroom.aspx.cs" Inherits="_Default"%>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">
    <div class="formtitle"><%=m_Title %></div>
    <div class="formtable">
        <input id="dwLabID" name="dwLabID" type="hidden" />
        <table class="ListTbl2">
            <tr><th>π§∫≈£∫</th><td><label id="szLogonName" ><%=szLogonName %></label></td>
                <th>–’√˚£∫</th><td><label id="szTruename" ><%=szTruename%></label></td>

            </tr>
           <tr>
               <td colspan="4"><%=m_szExt %></td>
           </tr>
            <tr><td colspan="4" class="btnRow"><button type="button" id="Cancel">πÿ±’</button></td></tr>
        </table>
    </div>
</form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
       
    </style>
     <script language="javascript" type="text/javascript">
         $(function () {
        <%if (bSet)
          {%>
             $("#dwSN").attr("readonly", "readonly").addClass("disabled");
        <%}%>
             $("#dwOwnerDept").change(function () {
                 $("#szDeptName").val($(this).find("option:selected").text());
             });
             $("#OK").button();
             $("#Cancel").button().click(Dlg_Cancel);
             $("#szManName2").autocomplete({
                 source: "searchAccount.aspx",
                 select: function (event, ui) {
                     if (ui.item) {
                         if (ui.item.id && ui.item.id != "") {
                             $("#szManName").val(ui.item.label);
                             $("#szManName2").val(ui.item.label);
                             $("#dwManagerID").val(ui.item.id);
                         }
                     }
                     return false;
                 },
                 response: function (event, ui) {
                     if (ui.content.length == 0) {
                         $("#dwManagerID").val("");
                         $("#szManName").val("");
                         ui.content.push({ label: " Œ¥’“µΩ≈‰÷√œÓ " });
                     }
                 }
             }).blur(function () {
                 if ($("#dwManagerID").val() == "") {
                     $(this).val("");
                 } else {
                     $(this).val($("#szManName").val());
                 }
             });

             $("#szAttendantName2").autocomplete({
                 source: "searchAccount.aspx",
                 select: function (event, ui) {
                     if (ui.item) {
                         if (ui.item.id && ui.item.id != "") {
                             $("#szAttendantName").val(ui.item.label);
                             $("#szAttendantName2").val(ui.item.label);
                             $("#dwAttendantID").val(ui.item.id);
                         }
                     }
                     return false;
                 },
                 response: function (event, ui) {
                     if (ui.content.length == 0) {
                         $("#dwAttendantID").val("");
                         $("#szAttendantName").val("");
                         ui.content.push({ label: " Œ¥’“µΩ≈‰÷√œÓ " });
                     }
                 }
             }).blur(function () {
                 if ($("#dwAttendantID").val() == "") {
                     $(this).val("");
                 } else {
                     $(this).val($("#szAttendantName").val());
                 }
             });

             setTimeout(function () {
                 if ($("#dwManagerID").val() == "") {
                     $("#szManName2").val("");
                 } else {
                     $("#szManName2").val($("#szManName").val());
                 }

                 if ($("#dwAttendantID").val() == "") {
                     $("#szAttendantName2").val("");
                 } else {
                     $("#szAttendantName2").val($("#szAttendantName").val());
                 }
             }, 1);
         });
    </script>
</asp:Content>
