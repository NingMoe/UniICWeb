<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="ResearchCheck.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">
    <div class="formtitle"><%=m_Title %></div>
    <div class="formtable">
        <input id="dwLabID" name="dwLabID" type="hidden" />
        <input type="hidden" name="dwApplyID" id="dwApplyID" />
          <table class="DlgListTbl">
            <tr>
         <th>申请人姓名:</th>
            <td><div id="szTrueName"></div></td>
                 <th>申请人所在部门:</th>
            <td><div id="szDeptName"></div></td>
                </tr>
             <tr>
            <th>手机:</th>
            <td><div id="szHandPhone"></div></td>
                 <th>电话:</th>
            <td><div id="szEmail"></div></td>
                </tr>
             <tr>
            <th>导师:</th>
            <td><div id="szTutorName"></div></td>
                 <th>科研项目:</th>
            <td><div id="szTargetName"></div></td>
                 </tr>
              <tr>
            <th>申请时长（分钟）:</th>
            <td><div id="dwApplyUseTime"></div></td>
            <th>申请说明:</th>
            <td><div id="szApplyInfo"></div></td>
                 </tr>
              <tr>
                  <td style="text-align:center" colspan="4"><%if(szFileName!="") { %>
                   <%=szFileName %>
                      <%} %></td>
              </tr>
            <tr><td colspan="4" style="text-align:center"><button type="submit" id="OK">审核通过</button><button type="button" id="Cancel">审核不通过</button></td></tr>
        </table>
        <div id="divNoOK">
           <table>
               <tr>
                   <td>
                       审核意见：
                        <input type="text" name="szCheckInfo" id="szCheckInfo" title="审核不通过必须输入原因" />
                      <label> <input type="checkbox" value="1" id="applayAggin" name="applayAggin"  />不能再次申请</label>
                        <input type="button" id="btnNOOK" value="确定不通过" style="width:90px" />
                       <input type="button" id="btnClose" value="关闭" style="width:80px" />
                   </td>
               </tr>
           </table>
        </div>
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
        $("#Cancel").click(function () {
            $("#divNoOK").show();
        });
        $("#divNoOK").hide();
        $("#OK").button();
        $("#btnClose").button().click(Dlg_Cancel);
        $("#btnNOOK").button();
        $("#btnNOOK").click(function () {
            var szCheckInfo=$("#szCheckInfo").val();
            if(szCheckInfo=="")
            {
                return;
            }
            var id = $("#dwApplyID").val();
            var vApplyAgain = 0;
            
            if ($("#applayAggin").prop("checked") == true)
            {
                var vApplyAgain = 1;
            }
            $.get(
                     "../../ajaxpage/checkReserach.aspx",
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
