<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewOnlyRoom.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
         <input id="dwDeptID" name="dwDeptID" type="hidden" />
        <input type="hidden" id="dwInClassKind" name="dwInClassKind" value="2"/>
        <div class="formtable">
            <table>
                <tr>
                    <th>编号：</th>
                    <td>
                        <input type="text" id="szRoomNo" name="szRoomNo" /></td>               
                    <th>名称：</th>
                    <td>
                        <input id="szRoomName" name="szRoomName" class="validate[required]" /></td>
                </tr>               
                <tr>                   
                    <th>所属<%=ConfigConst.GCLabName %>：</th>
                    <td>
                        <select id="dwLabID" name="dwLabID"><%=m_szLab %></select></td>
                     <th>开放规则：</th>
                    <td>
                        <select id="dwOpenRuleSN" name="dwOpenRuleSN">
                            <%=m_szOpenRule %>
                        </select></td>
                </tr> 
                <tr>                  
                    <th>面积：</th>
                    <td>
                        <input type="text" id="dwRoomSize" name="dwRoomSize" /></td>
                    <th title="当名称太长的时候可以缩写，方便刷卡段显示">名称缩写：</th>
                    <td>
                     <input type="text" id="szRoomURL" name="szRoomURL" />
                        
                    </td>
                </tr>   
                 <%if(ConfigConst.GroomNumMode==1) {%>
                  <tr>
                      <th>管理员组</th>
                      <td><select id="dwManGroupID2" name="dwManGroupID2"><%=szManGroup %></select></td>
                      </tr>
                  <%} %>  
                  <tr>                  
                    <th>控制方式：</th>
                    <td colspan="3">
                        <%=szManModeList %>
                    </td>
                </tr>                                 
                <tr>
                    <td colspan="4" class="btnRow">
                        <button type="submit" id="OK">确定</button>
                        <button type="button" id="Cancel">取消</button></td>
                </tr>
            </table>
        </div>
    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {
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
