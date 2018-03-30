<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="TestCard.aspx.cs" Inherits="Sub_Device"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formAdvOpts" runat="server">
    <h2>实验项目卡管理</h2>
    <div class="toolbar">
        <div class="tb_info">             
        </div>
        <div class="FixBtn"><a id="btnImport">导入实验项目卡</a><a id="btnNew">新建实验项目卡</a></div>
        <div class="tb_btn">
          
        </div>
    </div>
    <div class="content">
        <table class="ListTbl">
            <thead>
                <tr><th name="szTestName">实验项目名称</th><th name="szCategoryName">类别</th><th name="dwGroupPeopleNum">每组人数</th><th name="dwTestHour">学时</th><th>备注</th><th width="25px">操作</th></tr>
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
            
            $(".OPTD").html('<div class="OPTDBtn">\
                        <a href="#" class="btnSet" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" class="btnDel" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "50", minHeight: "25", maxHeight: "25", speed: 50
            });
            $("#btnNew").click(function () {
                $.lhdialog({
                    title: '新建',
                    width: '600px',
                    lock: true,
                    content: 'url:Dlg/SetTestCard.aspx?op=new'
                });
            });

            $("#btnImport").click(function () {
                $.lhdialog({
                    title: '导入实验项目卡',
                    width: '600px',
                    lock: true,
                    content: 'url:../Import.aspx?szDestName=TESTCARD&szTitle=' + escape("实验项目卡") + '&szTemplateFile=<%=MyVPath%>Upload/TestCard_Template.csv'
                        + '&szDestFieldList=szTestName,szCategoryName,dwGroupPeopleNum,dwTestHour,dwTestClass,dwTestKind,dwRequirement,szConstraints,szMemo'
                });
            });

            $(".btnSet").click(function () {
                var dwID = $(this).parents("tr").children().first().attr("data-id");
                $.lhdialog({
                    title: '修改',
                    width: '600px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/SetTestCard.aspx?op=set&id=' + dwID
                });
            });
            $(".btnDel").click(function () {
                var dwID = $(this).parents("tr").children().first().attr("data-id");
                ConfirmBox("确定删除?", function () {
                    ShowWait();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwID);
                }, '提示', 1, function () { });
              });
        });
    </script>
</form>
</asp:Content>