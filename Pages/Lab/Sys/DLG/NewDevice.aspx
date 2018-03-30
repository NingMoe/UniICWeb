<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewDevice.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <input type="hidden" name="dwDevID" />        
            <input type="hidden" name="dwCtrlMode" id="dwCtrlMode" value="2" />
        <div class="formtable">
            <table>
                <tr>
                    <th>编号(*)：</th>
                    <td> <input id="dwDevSN" name="dwDevSN" class="validate[required]" title="大于等于默认值" /></td>
               
                    <th><%=ConfigConst.GCDevName %>名(*)：</th>
                    <td>
                        <input id="szDevName" name="szDevName" class="validate[required]" /></td>
                </tr>
                <tr>
                    <th>计算机名：</th>
                    <td colspan="3">
                        <input id="szPCName" name="szPCName" /></td>                               
                 
                </tr>  
               
                 <tr>
                    <th>所属类型：</th>
                    <td>
                        <select id="dwKindID" name="dwKindID">
                            <%=m_szDevKind %>
                        </select></td>
                 <th>所属<%=ConfigConst.GCRoomName %>：</th>
                    <td>
                        <select id="dwRoomID" name="dwRoomID">
                            <%=m_szRoom %>
                        </select></td>
                  
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
                    <th>属性：</th>
                    <td colspan="3"><%=m_szPorperty%></td>
                </tr>
                <!--
                 <tr>
                       <th>生产厂商：</th>
                    <td>
                        <input id="szManufacturers" name="szManufacturers"  /></td>
                      <th>操作语言：</th>
                    <td>
                        <input id="szLanguage" name="szLanguage" /></td>                                
                </tr>    
              <tr>
                  
                   <th>主要应用：</th>
                    <td>
                        <textarea id="szFunc" name="szFunc" style="height:40px;width:200px"></textarea>
                    </td>   
                   <th>性能指标：</th>
                    <td>
                         <textarea id="szPerform" name="szPerform" style="height:40px;width:200px"></textarea>

                    </td>                
                </tr>
                <tr>
                   
                    <th>
                        样品要求：
                    </th>
                    <td>
                         <textarea id="szSample" name="szSample" style="height:40px;width:200px"></textarea>
                    </td>
                    <th>国别码</th>
                    <td>  <input id="Text1" name="szLanguage" /></td>
                </tr>
                -->
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
