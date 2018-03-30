<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Account.aspx.cs" Inherits="Sub_Device" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; font-weight: bold">账户管理</h2>
        <input type="hidden" name="dwClassID" id="dwClassID" />
        <input type="hidden" name="dwDeptID" id="dwDeptID" />
        <div style="float:right"><a id="btnNew">新建本地账户</a></div>    
        <div class="toolbar" style="margin: 20px">
            <div class="tb_infoInLine">
                <table style="width: 99%">
                    <tr>
                        <th>学工号:</th>
                        <td>
                            <input type="text" name="szPID" id="szPID" /></td>
                        <th>姓名:</th>
                        <td>
                            <input type="text" name="szTrueName" id="szTrueName" /></td>
                        <th><%=ConfigConst.GCDeptName %>:</th>
                        <td>
                            <input type="text" name="dwDeptName" id="dwDeptName" /></td>
                        <th>班级:</th>
                        <td>
                            <input type="text" name="dwClassName" id="dwClassName" /></td>
                        <th>
                            <input type="submit" id="btn" value="查询" /></th>
                    </tr>
                </table>
            </div>
            
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th name="szLogonName">学工号</th>
                        <th name="szTrueName">姓名</th>
                        <th name="dwSex">性别</th>
                        <th name="dwIdent">身份</th>
                        <th name="szClassName">班级</th>
                        <th name="szDeptName"><%=ConfigConst.GCDeptName %></th>
                       
                        <th name="szHandPhone">手机</th>
                        <th name="szEmail">邮箱</th>
                        <th name="dwSubsidy">补助</th>
                        <th>备注</th>
                        <th style="width: 25px"></th>
                    </tr>
                </thead>
                <tbody id="ListTbl">
                    <%=m_szOut %>
                </tbody>
            </table>
            <uc1:PageCtrl runat="server" ID="PageCtrl" />
        </div>
        <script type="text/javascript">
            $(function () {
               
                $(".OPTD").html('<div class="OPTDBtn">\
                        <a href="#" class="setAccnoInfo" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" class="setPasswd" title="重置密码"><img src="../../../themes/iconpage/psrest.png"/></a>\</div>');
                $(".OPTDSet").html('<div class="OPTDBtn">\
                        <a href="#" class="setAccnoInfo" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "75", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#btnNew").button().click(function () {
                    $.lhdialog({
                        title: '新建账户',
                        width: '700px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewAccount.aspx'
                    });
                });

                $(".setAccnoInfo").click(function () {
                    var dwID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '修改账户',
                        width: '750px',
                        height: '520px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewAccount.aspx?op=set&dwID=' + dwID
                    });
                });
                $(".setPasswd").click(function () {
                    var dwID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '重置密码',
                        width: '550px',
                        height: '320px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/setPasswd.aspx?op=set&dwID=' + dwID
                    });
                });
                
                $("#btn").button();
              
                $(".ListTbl").UniTable();
                $("#dwDeptName").autocomplete({
                    source: "../data/searchDept.aspx",
                    minLength: 0,
                    select: function (event, ui) {
                        if (ui.item) {
                            if (ui.item.id && ui.item.id != "") {
                                $("#dwDeptName").val(ui.item.label);
                                $("#dwDeptID").val(ui.item.id);
                                deptid = $("#dwDeptID").val();
                            }
                        }
                        return false;
                    },
                    response: function (event, ui) {
                        if (ui.content.length == 0) {
                            ui.content.push({ label: " 未找到配置项 " });
                        }
                    }
                }).blur(function () {
                    if ($(this).val() == "") {
                        $("#dwDeptID").val("");
                        deptid = "";
                    } else {

                    }
                }).click(function () {
                    $("#dwDeptName").autocomplete("search", "");
                });

                $("#dwClassName").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: "../data/searchcls.aspx",
                            dataType: "json",
                            data: { 'deptid': deptid, 'term': request.term },
                            success: function (data) {
                                response(data);
                            }
                        });
                    },
                    minLength: 0,
                    select: function (event, ui) {
                        if (ui.item) {
                            if (ui.item.id && ui.item.id != "") {
                                $("#dwClassName").val(ui.item.label);
                                $("#dwClassID").val(ui.item.id);
                            }
                        }
                        return false;
                    },
                    response: function (event, ui) {
                        if (ui.content.length == 0) {
                            ui.content.push({ label: " 未找到配置项 " });
                        }
                    }
                }).blur(function () {
                    if ($(this).val() == "") {
                        $("#dwClassID").val("");
                    } else {

                    }
                }).click(function () {
                    deptid = $("#dwDeptID").val();
                    $("#dwClassName").autocomplete("search", "");

                });
            });

        </script>
      
    </form>
</asp:Content>
