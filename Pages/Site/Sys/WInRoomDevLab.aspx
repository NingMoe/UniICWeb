<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="WInRoomDevLab.aspx.cs" Inherits="Sub_Device" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>室内资源管理</h2>
        <div class="toolbar">
            <div class="tb_info">
                <div class="UniTab" id="tabl">
                    <a href="WInRoomDev.aspx" id="WDevTeachingRoom">室内资源管理</a>
                    <a href="WInRoomDevKind.aspx" id="WInRoomDevKind">室内资源类型管理</a>
                    <a href="WInRoomDevLab.aspx" id="WInRoomDevLab">室内资源楼宇管理</a>
                </div>
            </div>
            <div class="FixBtn">
                <a id="btnNew" class="btnClss">新建楼宇</a>
            </div>
        </div>
        <div>
            <div class="tb_infoInLine" style="margin: 5px 0px">                
            </div>
        </div>
       <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th name="szLabSN">编号</th>
                        <th name="szLabName">楼宇名称</th>
                        <th>所属<%=ConfigConst.GCDeptName %></th>
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
                var tabl = $(".UniTab").UniTab();
                $(".OPTD").html('<div class="OPTDBtn">\
                       <a class="setLabBtn" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\
                       <a class="delLabBtn"  href="#" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
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
                        content: 'url:Dlg/WSetLabInRoom.aspx?op=set&dwLabID=' + dwLabID
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
                        content: 'url:../../ueditor/default.aspx?id=' + dwLabID + "&type=LabInfo"
                    });
                });
                $(".delLabBtn").click(function () {
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
                        content: 'url:Dlg/WNewLabInRoom.aspx?op=new&dwLabClass=513'
                    });
                });
                $(".ListTbl").UniTable();

            });
        </script>
    </form>
</asp:Content>
