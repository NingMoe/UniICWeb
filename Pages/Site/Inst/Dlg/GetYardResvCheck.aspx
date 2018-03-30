<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="GetYardResvCheck.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">
    <div class="formtitle"><%=m_Title %></div>
    <div class="formtable">
        <input id="dwLabID" name="dwLabID" type="hidden" />
        <input type="hidden" name="dwApplyID" id="dwApplyID" />
          <table class="DlgListTbl">
           <tr>
                    <th>申请人姓名:</th>
                    <td>
                        <div id="szTrueName"></div>
                    </td>
                    <th>申请人所在部门:</th>
                    <td>
                        <div id="szDeptName"></div>
                    </td>
                </tr>
                <tr>
                    <th>手机:</th>
                    <td>
                        <div id="szHandPhone"></div>
                    </td>
                    <th>邮件:</th>
                    <td>
                        <div id="szEmail"></div>
                    </td>
                </tr>
                <tr>
                    <th>申请资源:</th>
                    <td>
                        <div id="szDevName"><%=szResvDevName %></div>
                    </td>
                    <th>资源所在部门:</th>
                    <td>
                        <div id=""><%=szResvDevDept %></div>
                    </td>
                </tr>
               <tr>

                    <th>预估参加人数:</th>
                    <td>
                        <div><%=szPeople %></div>
                    </td>
                    <th>活动类型:</th>
                    <td>
                        <div><%=szActivity %></div>
                    </td>
                    <!--
                    <th>是否摄像:</th>
                    <td>
                        <div id="Div2"><%=szNeedCameor %></div>
                    </td>
                    -->
                </tr>
                <!--
                <tr>

                    <th>主管部门:</th>
                    <td>
                         <%if (szDirectors == "")
                           {%>

                        <label><input type="checkbox" name="dwDirectors" value="1024" class="enum" />团委</label>
                         <label><input type="checkbox" name="dwDirectors" value="2048" class="enum" />学工部</label>

                        <%} else {%>
                        <%=szDirectors %>
                         <%}%>

                      </td>
                    <th>安保级别:</th>
                    <td>
                           <%if (szSecurityLevel == "")
                             {%>
                        <select id="dwSecurityLevel" name="dwSecurityLevel">
                            <option value="1">不需要安保</option>
                            <option value="6">需要安保</option>
                        </select>
                        <%} else {%>
                        <%=szSecurityLevel %>
                         <%}%>
                       

                    </td>
                </tr>
                -->
                 <tr>
                   
                      <th>申请时间:</th>
                    <td>
                        <div id="dwApplyUseTime"><%=szResvTime %></div>
                    </td>
                    <th>活动名称:</th>
                    <td>
                        <div id="szApplyInfo"><%=szApplyName %></div>
                    </td>
                </tr>
              
                <tr>
                   
                      <th>活动团体:</th>
                    <td>
                        <div id="szOrganization"><%=szOrganization %></div>
                    </td>
                    <th>负责人:</th>
                    <td>
                        <div id="szOrganiger"><%=szOrganiger %></div>
                    </td>
                </tr>
                <tr>
                    <th>活动介绍:</th>
                    <td colspan="3">
                        <div><%=szMemo %></div>
                    </td>
                </tr>
            <tr><td colspan="4" style="text-align:center">  <input type="button" id="btnClose" value="关闭" style="width:80px" /></td></tr>
        </table>
       
    </div>
</form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
       
    </style>
<script language="javascript" type="text/javascript" > 
    $(function () {
      
        <%if(bSet){%>
        $("#dwSN").attr("readonly", "readonly").addClass("disabled");
        <%}%>
        $("#dwOwnerDept").change(function () {
            $("#szDeptName").val($(this).find("option:selected").text());
        });
        $("#btnClose").button().click(Dlg_Cancel);
       
        $("#btnNOOK").click(function () {
            var szCheckInfo=$("#szCheckInfo").val();
            if(szCheckInfo=="")
            {
                return;
            }
            var id = $("#dwApplyID").val();
            var vApplyAgain=$("#applayAggin").val();
            $.get(
                     "../../ajaxpage/YearResvCheck.aspx",
                     { szCheckInfo: szCheckInfo, id: id, vApplyAgain: vApplyAgain },
                     function (data) {
                         if (data == "success") {
                             MessageBox("审核不通过", "提示", 3, function () { Dlg_OK() });
                         }
                         else {
                             MessageBox("审核失败" + data, "提示", 3, function () { Dlg_OK() });
                         }

                     }
                   );

        });
        $("#Cancel").button();
        $("#szManName2").autocomplete({
            source: "../../data/searchAccount.aspx",
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
            source: "../../data/searchAccount.aspx",
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
