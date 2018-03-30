<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="ServiceType.aspx.cs" Inherits="Sub_Lab" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>服务管理</h2>
        <div class="toolbar">
            <div class="tb_info"></div>
            <div class="FixBtn"><a id="btnNew">新建服务</a></div>
            <div class="tb_btn">
            </div>
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>名称</th>
                      
                        <th>服务<%=ConfigConst.GCDeptName %></th>
                        <th width="25px">操作</th>
                    </tr>
                </thead>
                <tbody id="ListTbl">
                    <%=m_szOut %>
                </tbody>
            </table>
         
        </div>
        <script type="text/javascript">

            $(function () {
                $(".OPTD").html('<div class="OPTDBtn">\
                       <a class="setLabBtn" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "75", minHeight: "25", maxHeight: "25", speed: 50
            });
            $(".setLabBtn").click(function () {
                var szLabSN = $(this).parents("tr").children().first().text();
                var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                $.lhdialog({
                    title: '修改',
                    width: '660px',
                    height: '300px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/NewSerCheckType.aspx?op=set&dwID=' + dwLabID
                });
            });
            $(".setLabManager").click(function () {
                var szLabSN = $(this).parents("tr").children().first().text();
                var dwMangGroupID = $(this).parents("tr").children().first().siblings().attr("data-manGroupID");
                $.lhdialog({
                    title: '设置<%=ConfigConst.GCLabName %>管理员',
                    width: '660px',
                    height: '500px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/ManGroupList.aspx?op=set&dwID=' + dwMangGroupID
                });
            });

            $(".InfoLabBtn").click(function () {
                var szLabSN = $(this).parents("tr").children().first().text();
                var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                $.lhdialog({
                    title: '介绍',
                    width: '760px',
                    height: '550px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:../../ueditor/default.aspx?delID=' + dwLabID + "&type=LabInfo"
                });
            });
            $(".delBtn").click(function () {
                var szLabSN = $(this).parents("tr").children().first().text();
                var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                ConfirmBox("确定删除?", function () {
                    ShowWait();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwLabID);
                }, '提示', 1, function () { });
            });
            $("#btnNew").click(function () {
                $.lhdialog({
                    title: '新建',
                    width: '660px',
                    height: '300px',
                    lock: true,
                    content: 'url:Dlg/NewSerCheckType.aspx?op=new'
                });
            });
            $(".ListTbl").UniTable({ ShowCheck: false, HeaderIndex: false });

        });
        </script>
    </form>
</asp:Content>
