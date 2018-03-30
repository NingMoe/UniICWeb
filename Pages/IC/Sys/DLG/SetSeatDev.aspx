<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetSeatDev.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>                
        <input type="hidden" id="dwRoomID" name="dwRoomID" />
        <input type="hidden" id="dwLabID" name="dwLabID" />
        <input type="hidden" id="dwKindID" name="dwKindID" />
        <input type="hidden" id="dwDevID" name="dwDevID" />
        <div class="formtable">
            <table>
                <tr>   
                     <th><%=ConfigConst.GCSysKindSeat %>��ţ�</th>
                    <td><input id="dwDevSN" name="dwDevSN" class="validate[validate[custom[onlyNumber]]" /></td>                                    
                    <th><%=ConfigConst.GCSysKindSeat %>��(*)��</th>
                    <td><input id="szDevName" name="szDevName" class="validate[required]" /></td>
                </tr>                                                  
                <tr>                   
                    <th>��������</th>
                    <td>
                      <input type="text" name="szRoomName" id="szRoomName" class="validate[required]" /></td>
                     <th>����<%=ConfigConst.GCKindName %>��</th>
                    <td><input type="text" name="szKindName" id="szKindName" class="validate[required]" /></td>
                </tr>  
                 <tr>
                    <th>���Ʒ�ʽ��</th>
                    <td><select id="dwCtrlMode" name="dwCtrlMode"><%=m_szCtrlMode %></select></td>
                    <th>�ʲ��ţ�</th>
                    <td><input type="text" name="szAssertSN" id="szAssertSN" title="�����ܿ���" /></td>
              
                </tr>         
  <tr>
                    
                    <th>FID���ţ�</th>
                    <td><input type="text" name="szTagID" id="szTagID" title="FID����" /></td>
                 <td colspan="2" style="text-align:left"><label><input class="enum" type="checkbox" id="chkopen" name="chkopen" value="1" >�����⿪��</label></td>
               
                </tr>                               
                <tr>
                    <td colspan="4" class="tblBtn">
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
            $("#dwPurchaseDate").datepicker({
                changeMonth: true,
                changeYear: true
            });
        $("#dwOwnerDept").change(function () {
            $("#szDeptName").val($(this).find("option:selected").text());
        });
        AutoDevKind($("#szKindName"), 2, $("#dwKindID"), 8, false);
        AutoRoom($("#szRoomName"),2,$("#dwRoomID"),8,$("#dwLabID"));
        $("#OK").button();
        $("#Cancel").button().click(Dlg_Cancel);
        $("#dwPurchaseDate").datepicker();
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