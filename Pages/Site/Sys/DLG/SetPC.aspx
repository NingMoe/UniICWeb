<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetPC.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>                
        <input type="hidden" id="dwCtrlMode" name="dwCtrlMode" value="3" /><!--登陆控制-->
        <input type="hidden" id="dwRoomID" name="dwRoomID" />
        <input type="hidden" id="dwLabID" name="dwLabID" />
        <input type="hidden" id="dwKindID" name="dwKindID" />
        <input type="hidden" id="dwDevID" name="dwDevID" />
        <div class="formtable">
            <table>
              
                <tr>            
                     <th>设备序号  <%=abc %>：</th>
                    <td><input id="dwDevSN" name="dwDevSN" class="validate[validate[custom[onlyNumber]]" /></td>                          
                    <th>座位编号(*)：</th>
                    <td><input id="szDevName" name="szDevName" class="validate[required]" /></td>
                </tr>
                <tr>
                    <th>计算机名：</th>
                    <td><input id="szPCName" name="szPCName" /></td>               
                    <th>属性：</th>
                    <td><%=m_szPorperty%></td>
                </tr>                                         
                <tr>                   
                    <th>所属区域：</th>
                    <td>
                      <input type="text" name="szRoomName" id="szRoomName" class="validate[required]" /></td>
                     <th>所属<%=ConfigConst.GCKindName %>：</th>
                    <td><input type="text" name="szKindName" id="szKindName" class="validate[required]" /></td>
                </tr>               
               <tr>
                    <th>价格(元)：</th>
                    <td>
                        <input id="dwUnitPrice" name="dwUnitPrice" class="validate[validate[custom[onlyNumber]]" /></td>
               
                    <th>购置日期：</th>
                    <td>
                        <input id="dwPurchaseDate" name="dwPurchaseDate" /></td>
                </tr>               
   
                <tr>
                    <td colspan="4" class="tblBtn">
                        <button type="submit" id="OK">确定</button>
                        <button type="button" id="Cancel">取消</button></td>
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
        AutoDevKind($("#szKindName"), 2, $("#dwKindID"), 2, false);
        AutoRoom($("#szRoomName"),2,$("#dwRoomID"),2,$("#dwLabID"));
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
                    ui.content.push({ label: " 未找到配置项 " });
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
                    ui.content.push({ label: " 未找到配置项 " });
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
