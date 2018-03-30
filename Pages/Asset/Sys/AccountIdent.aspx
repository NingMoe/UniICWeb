<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="AccountIdent.aspx.cs" Inherits="Sub_Device" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; font-weight: bold"><%=ConfigConst.GCTutorName%>管理</h2>
        <div>
           
             <div>
                 <a id="btnResvRule">新建<%=ConfigConst.GCTutorName%></a>
                <a id="importTutorStu">导入<%=ConfigConst.GCTutorName%></a>
                 </div>
            <div  class="tb_infoInLine"  style="margin:5px 0px">
                
                       <table style="width: 99%">
                    <tr>
                        <th>学工号:</th>
                        <td>
                            <input type="text" name="szPID" id="szPID" /></td>
                        <th>姓名:</th>
                        
                        <td>
                            <input type="text" name="szTrueName" id="szTrueName" /></td>
                        <th>
                            <input type="submit" id="btn" value="查询" /></th>
                    </tr>
                </table>`
   
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
                $("#btnResvRule,#importTutorStu").button();
                $("#btnResvRule").click(function () {
                    $.lhdialog({
                        title: '新建<%=ConfigConst.GCTutorName%>',
                        width: '750px',
                        height: '380px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewAccountIdent.aspx?op=new'
                    });
                 });
                $("#importTutorStu").click(function () {
                    $.lhdialog({
                        title: '导入<%=ConfigConst.GCTutorName%>',
                        width: '750px',
                        height: '420px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/importTutor.aspx?op=new'
                    });
                });
                var deptid = "";
                $(".OPTD").html('<div class="OPTDBtn">\
                            <a  class="setDevkindBtn" href="#" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "175", minHeight: "25", maxHeight: "25", speed: 50
                });
                $(".setDevkindBtn").click(function () {
                    var dwID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("确定删除?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwID);
                    }, '提示', 1, function () { });
                });
                //$(".ListTbl").UniTable();
                $("#btn").button();
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
        <style>
            .tb_infoInLine table tr th {
                text-align: center;
            }

            .tb_infoInLine table tr td input {
                margin-left: 5px;
            }
        </style>
    </form>
</asp:Content>
