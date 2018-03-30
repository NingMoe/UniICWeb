<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Turtor.aspx.cs" Inherits="Sub_Device" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2><%=ConfigConst.GCTutorName%>管理</h2>
        <div class="toolbar">
            <div class="tb_info"></div>
            <div class="FixBtn"><a id="btnResvRule">新建<%=ConfigConst.GCTutorName%></a>
                <a id="importTutorStu">导入学生<%=ConfigConst.GCTutorName%>对应表</a>
            </div>
            <div class="tb_btn">
            </div>
        </div>
         <input type="hidden" name="dwDeptID" id="dwDeptID" />
        <div class="toolbar" style="margin: 10px">
            <div class="tb_infoInLine">
                <table style="width: 99%">
                    <tr>
                        <th style="width:10%">姓名:</th>
                        <td style="width:30%"><input type="text" name="szTrueName" id="szTrueName" style="width:200px" /></td>
                        <td style="width:60%;text-align:left;">
                            <input type="submit" id="btn" value="查询" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th >姓名</th>
                        <th >电话</th>
                        <th >手机</th>
                        <th>邮箱</th>
                        <th>备注</th>
                        <th width="25px">操作</th>
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
                $(".ListTbl").UniTable();
                $("#btn").button();
                $(".OPTD").html('<div class="OPTDBtn">\
<a href="#" class="delResvRuleBtn" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "50", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#btnResvRule").click(function () {
                    $.lhdialog({
                        title: '新建<%=ConfigConst.GCTutorName%>',
                        width: '750px',
                        height: '380px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewTutor.aspx?op=new'
                    });
                });
                $("#importTutorStu").click(function () {
                    $.lhdialog({
                        title: '导入学生<%=ConfigConst.GCTutorName%>对应',
                    width: '750px',
                    height: '420px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/importTutorStu.aspx?op=new'
                });
                  });
                $(".delResvRuleBtn").click(function () {
                    var dwID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("确定删除?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwID);
                }, '提示', 1, function () { });
            });
        });
        </script>
        <style>
          
        </style>
    </form>
</asp:Content>
