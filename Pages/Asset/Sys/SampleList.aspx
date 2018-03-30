<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="SampleList.aspx.cs" Inherits="Sub_Lab" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>样品库管理</h2>
        <div class="toolbar">
            <div class="tb_info"></div>
            <div class="FixBtn"><a id="btnNewLab">新建样品</a></div>
            <div class="tb_btn">
            </div>
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>样品名称</th>
                        <th>计费单位</th>
                        <th>校内(元)</th>
                        <th>校外(元)</th>
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
                $(".OPTD").html('<div class="OPTDBtn">\
                       <a class="setLabBtn" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\
                       <a class="delLabBtn"  href="#" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "75", minHeight: "25", maxHeight: "25", speed: 50
                });
                $(".setLabBtn").click(function () {
                    var dwID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '修改样品',
                        width: '700px',
                        height: '300px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/setSample.aspx?op=set&dwSampleSN=' + dwID
                    });
                });
                $(".delLabBtn").click(function () {
                    var sampleID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("确定删除?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + sampleID);
                }, '提示', 1, function () { });
            });
            $("#btnNewLab").click(function () {
                $.lhdialog({
                    title: '新建样品',
                    width: '700px',
                    height: '300px',
                    lock: true,
                    content: 'url:Dlg/newSample.aspx?op=new'
                });
            });
            //$(".ListTbl").UniTable();

        });
        </script>
    </form>
</asp:Content>
