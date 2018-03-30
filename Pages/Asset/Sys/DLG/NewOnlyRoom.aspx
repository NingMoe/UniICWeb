<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewOnlyRoom.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
         <input id="dwDeptID" name="dwDeptID" type="hidden" />
        <div class="formtable">
            <table>
                <tr>
                    <th>��ţ�</th>
                    <td>
                        <input type="text" id="szRoomNo" name="szRoomNo" /></td>               
                    <th>���ƣ�</th>
                    <td>
                        <input id="szRoomName" name="szRoomName" class="validate[required]" /></td>
                </tr>               
                <tr>                   
                    <th>����<%=ConfigConst.GCLabName %>��</th>
                    <td>
                        <select id="dwLabID" name="dwLabID"><%=m_szLab %></select></td>
                     <th>���Ź���</th>
                    <td>
                        <select id="dwOpenRuleSN" name="dwOpenRuleSN">
                            <%=m_szOpenRule %>
                        </select></td>
                </tr>
                <tr>
                  <th>����:</th>
                  <td><select id="dwLabKind" name="dwLabKind"><%=m_szLabKind %></select></td>
                    <th>��Դ:</th>
                  <td><select id="dwLabClass" name="dwLabClass"><%=m_szLabClass %></select></td>
              </tr> 
                <tr>                  
                    <th>�����</th>
                    <td>
                        <input type="text" id="dwRoomSize" name="dwRoomSize" /></td>
                     <th>λ�ã�</th>
                    <td>
                        <input type="text" id="szBuildingName" name="szBuildingName" /></td>
                    </tr> 
                <tr>
                     <th>����ѧ�ƣ�</th>
                    <td><select id="dwAcademicSubject" name="dwAcademicSubject"><%=m_dwDecam %></select></td>
                     <th>����ʱ�䣺</th>
                    <td>
                        <input type="text" id="dwCreateDate" name="dwCreateDate" /></td>
                 
                </tr>    
                    <tr>  
                     <th>��ע��</th>
                     
                    <td colspan="3">
                     <input type="text" id="szMemo" name="szMemo" />
                        
                    </td>
                </tr>                                   
                <tr>
                    <td colspan="4" class="btnRow">
                        <button type="submit" id="OK">ȷ��</button>
                        <button type="button" id="Cancel">ȡ��</button></td>
                </tr>
            </table>
        </div>
    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
   <style type="text/css">
     .ui-datepicker select.ui-datepicker-year { width: 43%;}
              .tb_infoInLine td input {
            width:120px;
            }
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {
            $("#dwCreateDate").datepicker({
                changeMonth: true,
                changeYear: true
            });
            AutoDept($("#szDeptName"), 2, $("#dwDeptID"), false);
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
                    ui.content.push({ label: " δ�ҵ������� " });
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
                    ui.content.push({ label: " δ�ҵ������� " });
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
