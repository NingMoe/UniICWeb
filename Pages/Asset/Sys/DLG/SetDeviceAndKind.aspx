<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetDeviceAndKind.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <input type="hidden" name="dwDevID" />        
        <input type="hidden" name="szAttendantName" id="szAttendantName" />
        <input type="hidden" name="dwMaxUsers" id="dwMaxUsers" value="1" />
        <input type="hidden" name="dwMinUsers" id="dwMinUsers"  value="1" />
         <input id="dwUsableNum" name="dwUsableNum" value="0" type="hidden" />
        <div class="formtable">
            <table>
                <tr>
                    <th>编号(*)：</th>
                    <td> <input id="szAssertSN" name="szAssertSN" class="validate[required]" /></td>
               
                    <th><%=ConfigConst.GCDevName %>名(*)：</th>
                    <td>
                        <input id="szDevName" name="szDevName" class="validate[required]" /></td>
                </tr>
                <tr>
                    <th>计算机名：</th>
                    <td>
                        <input id="szPCName" name="szPCName" /></td>               

                  <th>属性：</th>
                    <td><%=m_szPorperty%></td>
                </tr>  
                 <tr>
                    <th>管理员：</th>
                    <td>
                         <select id="dwAttendantID" name="dwAttendantID">
                            <%=m_szManager %>
                        </select></td>
                <th>管理员电话：</th>
                    <td>
                           <input id="szAttendantTel" name="szAttendantTel" /></td>
                </tr>   
                 <tr>
                       <th>型号：</th>
                    <td>
                        <input id="szModel" name="szModel"  /></td>
                    <th>规格：</th>
                    <td>
                        <input id="szSpecification" name="szSpecification" /></td>                                  
                </tr>  
                 <tr>                     
                    <th>预约属性：</th>
                    <td colspan="3">
                        <%=m_szKindPorperty %></td>                                  
                </tr>   
                 <tr>
                       <th>生产厂商：</th>
                    <td>
                        <input id="szProducer" name="szProducer"  /></td>
                    <th>国别：</th>
                    <td>
                        <input id="dwNationCode" name="dwNationCode" /></td>                                  
                </tr>            
                <tr>
                    <th>所属<%=ConfigConst.GCLabName %>：</th>
                    <td>
                        <select id="dwLabID" name="dwLabID">
                            <%=m_szLab %>
                        </select></td>
               
                    <th>所属<%=ConfigConst.GCRoomName %>：</th>
                    <td>
                        <select id="dwRoomID" name="dwRoomID">
                            <%=m_szRoom %>
                        </select></td>
                </tr>
                 <tr>
                    <th>所属类型：</th>
                    <td>
                        <select id="dwKindID" name="dwKindID">
                            <%=m_szDevKind %>
                        </select></td>
               
                    <th>控制方式：</th>
                    <td>
                        <select id="dwCtrlMode" name="dwCtrlMode">                              
                            <%=m_szCtrlMode %>
                        </select></td>
                </tr>
               <tr>
                    <th>单价(元)：</th>
                    <td>
                        <input id="dwUnitPrice" name="dwUnitPrice" class="validate[validate[custom[onlyNumber]]" /></td>
               
                    <th>购置日期：</th>
                    <td>
                        <input id="dwPurchaseDate" name="dwPurchaseDate" /></td>
                </tr>
              <tr>
                    <th>操作语言：</th>
                    <td>
                        <input id="szLanguage" name="szLanguage" /></td>
                   <th>主要应用：</th>
                    <td>
                        <textarea id="szFunc" name="szFunc" style="height:40px;width:200px"></textarea>
                    </td>                   
                </tr>
                <tr>
                    <th>性能指标：</th>
                    <td>
                         <textarea id="szPerform" name="szPerform" style="height:40px;width:200px"></textarea>

                    </td>
                    <th>
                        样品要求：
                    </th>
                    <td>
                         <textarea id="szSample" name="szSample" style="height:40px;width:200px"></textarea>
                    </td>
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
        <%if (bSet)
          {%>
        $("#dwSN").attr("readonly", "readonly").addClass("disabled");
        <%}%>
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
